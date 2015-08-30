using Knightware.Net;
using Spyder.Client.Net.DrawingData;
using Spyder.Client.Peripherals.ButtonBoards.Devices;
using Knightware.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spyder.Client.Net;

namespace Spyder.Client.Peripherals.ButtonBoards
{
    public class ButtonBoard
    {
        private IButtonBoardDevice buttonBoard;
        private ButtonMap buttonMap;
        private List<int> buttonMapRegisterIDs;
        private BindableSpyderClient server;
        private ButtonBoardLogicController logicController;
        private AutoResetWorker buttonBoardRefreshWorker;

        private DrawingData lastDrawingData;
        private Dictionary<int, ButtonState> registerLiveStatesByCommandKeyID;
        private Dictionary<int, ButtonState> registerButtonBoardStatesByButtonID;

        private readonly object invalidatedButtonsLock = new object();
        private Dictionary<int, ButtonState> invalidatedButtons;
        
        public bool IsRunning { get; private set; }
        
        public async Task<bool> Startup(IButtonBoardDevice buttonBoard, ButtonMap buttonMap, BindableSpyderClient server)
        {
            Shutdown();

            if (buttonBoard == null || buttonMap == null || server == null | !server.IsRunning)
                return false;

            this.IsRunning = true;
            this.buttonMap = buttonMap;
            this.buttonBoard = buttonBoard;
            this.server = server;

            this.invalidatedButtons = new Dictionary<int, ButtonState>();
            this.registerLiveStatesByCommandKeyID = new Dictionary<int, ButtonState>();
            this.registerButtonBoardStatesByButtonID = new Dictionary<int, ButtonState>();
            this.buttonMapRegisterIDs = buttonMap.Mappings.Where(m => m.Value.CommandType == ButtonCommandType.ButtonRegister).Select(m => m.Key).ToList();

            //Setup the worker which will write button LED states periodically
            buttonBoardRefreshWorker = new AutoResetWorker();
            buttonBoardRefreshWorker.PeriodicSignallingTime = TimeSpan.FromMilliseconds(100);
            buttonBoardRefreshWorker.Startup(buttonBoardRefreshWorker_DoWork, null, () => IsRunning && invalidatedButtons.Count > 0);

            logicController = new ButtonBoardLogicController();
            logicController.ButtonStateChanged += logicController_ButtonStateChanged;
            logicController.CommandKeyRecall += logicController_CommandKeyRecall;
            await logicController.Startup(GetRegisterPopulated);

            this.buttonBoard.ButtonAction += buttonBoard_ButtonAction;
            this.server.DrawingDataReceived += server_DrawingDataReceived;

            return true;
        }

        public void Shutdown()
        {
            IsRunning = false;

            if(buttonBoardRefreshWorker != null)
            {
                buttonBoardRefreshWorker.Shutdown();
                buttonBoardRefreshWorker = null;
            }

            if(logicController != null)
            {
                logicController.ButtonStateChanged -= logicController_ButtonStateChanged;
                logicController.CommandKeyRecall -= logicController_CommandKeyRecall;
                logicController.Shutdown();
                logicController = null;
            }

            if(buttonBoard != null)
            {
                this.buttonBoard.ButtonAction -= buttonBoard_ButtonAction;
                this.buttonBoard = null;
            }

            if(server != null)
            {
                this.server.DrawingDataReceived -= server_DrawingDataReceived;
                this.server = null;
            }

            buttonMapRegisterIDs = null;
            registerLiveStatesByCommandKeyID = null;
            registerButtonBoardStatesByButtonID = null;
            invalidatedButtons = null;
        }

        async void logicController_CommandKeyRecall(object sender, int pageIndex, int registerID, int cueIndex)
        {
            int fullRegID = (pageIndex * 1000) + registerID;
            if (cueIndex == -1)
            {
                //Need to recall one cue higher than the highest element to effectively take the key off screen
                var cmdKey = await server.GetCommandKey(fullRegID);
                if(cmdKey != null)
                    await server.RecallCommandKey(fullRegID, cmdKey.CueCount - 1);
            }
            else
            {
                //Recall command key
                await server.RecallCommandKey(fullRegID, cueIndex);
            }
        }

        private async Task<bool> GetRegisterPopulated(int pageIndex, int registerID)
        {
            if (server == null || server.CommandKeys == null)
                return false;

            int fullRegisterID = pageIndex * 1000 + registerID;
            var cmdKey = await server.GetCommandKey(fullRegisterID);
            return cmdKey != null;
        }

        void logicController_ButtonStateChanged(object sender, ButtonCommand command, ButtonState state)
        {
            //De-reference key(s) - Unlikely that multiple buttons will be mapped to the same command, but possible
            var buttonIDs = this.buttonMap.Mappings
                .Where(m => m.Value.Equals(command))
                .Select(m => m.Key)
                .ToList();

            //Set buttons as invalid
            if (buttonIDs.Count > 0)
            {
                foreach (var buttonID in buttonIDs)
                {
                    if (command.CommandType == ButtonCommandType.ButtonRegister 
                        && (state == ButtonState.Off || state == ButtonState.Inactive) 
                        && registerLiveStatesByCommandKeyID.ContainsKey(command.TargetID))
                    {
                        //User has presumably released the register button, so let's refresh it to it's drawing data state
                        var liveState = registerLiveStatesByCommandKeyID[command.TargetID];
                        invalidatedButtons.Add(buttonID, liveState);
                    }
                    else
                    {
                        invalidatedButtons.Add(buttonID, state);
                    }
                }

                buttonBoardRefreshWorker.Set();
            }
        }

        private async Task<ButtonState?> GetRegisterButtonLiveStateFromDrawingData(DrawingData drawingData, int pageIndex, int registerID)
        {
            int fullID = (pageIndex * 1000) + registerID;
            var cmdKey = await server.GetCommandKey(fullID);
            if (cmdKey == null)
            {
                //Invalid command key
                return ButtonState.Off;
            }

            if (drawingData == null)
                return null;

            bool pvw, pgm;
            drawingData.GetCommandKeyState(cmdKey.ScriptID, out pvw, out pgm);
            if (pgm)
                return ButtonState.Program;
            else if (pvw)
                return ButtonState.Preview;
            else
                return ButtonState.Inactive;
        }

        private async Task UpdateRegisterButtonLiveStatesFromDrawingData(bool invalidateBoardIfChangesExist = true)
        {
            var drawingData = lastDrawingData;
            if (drawingData == null)
                return;

            //Update our visible command key buttons
            bool changesExist = false;
            var refreshedIDs = new List<int>();
            var newStates = new Dictionary<int, ButtonState>();
            foreach(var mapping in buttonMap.Mappings.Values.Where(key => key.CommandType == ButtonCommandType.ButtonRegister))
            {
                ButtonState? newState = await GetRegisterButtonLiveStateFromDrawingData(drawingData, logicController.Page, mapping.TargetID);
            }

            foreach (int registerID in buttonMapRegisterIDs)
            {
                //Offset for the current page
                int fullRegisterID = registerID + (logicController.Page * 1000);
                
                bool pvw = false;
                bool pgm = false;
                if (server.CommandKeys != null)
                {
                    var commandKey = server.CommandKeys.FirstOrDefault(c => c.RegisterID == fullRegisterID);
                    if (commandKey != null)
                    {
                        drawingData.GetCommandKeyState(commandKey.ScriptID, out pvw, out pgm);
                    }
                }

                ButtonState state = ButtonState.Off;
                if (pgm)
                    state = ButtonState.Program;
                else if (pvw)
                    state = ButtonState.Preview;

                if (registerLiveStatesByCommandKeyID.ContainsKey(registerID))
                {
                    ButtonState lastState = registerLiveStatesByCommandKeyID[registerID];
                    if (lastState != state)
                    {
                        registerLiveStatesByCommandKeyID[registerID] = state;
                        changesExist = true;
                    }
                }
                else
                {
                    registerLiveStatesByCommandKeyID.Add(registerID, state);
                    changesExist = true;
                }
                refreshedIDs.Add(registerID);
            }

            //Clear state on any buttons in the list which were not refreshed above
            foreach(var entry in registerLiveStatesByCommandKeyID.Where(r => !refreshedIDs.Contains(r.Key) && r.Value != ButtonState.Off))
            {
                registerLiveStatesByCommandKeyID[entry.Key] = ButtonState.Off;
                changesExist = true;                
            }

            //Do we need to refresh the board?
            if (invalidateBoardIfChangesExist && changesExist)
                buttonBoardRefreshWorker.Set();
        }

        async void server_DrawingDataReceived(object sender, Net.Notifications.DrawingDataReceivedEventArgs e)
        {
            lastDrawingData = e.DrawingData;
            await UpdateRegisterButtonLiveStatesFromDrawingData();
        }

        async void buttonBoard_ButtonAction(object sender, ButtonActionEventArgs e)
        {
            //Push button action down to the logic controller
            if (IsRunning && buttonMap.Mappings.ContainsKey(e.ButtonID))
            {
                var buttonCommand = buttonMap.Mappings[e.ButtonID];
                await logicController.ProcessKeyAction(buttonCommand, e.IsPressed);
            }
        }

        private Task buttonBoardRefreshWorker_DoWork(object state)
        {
            //Reference swap the invalidated buttons that we need to process
            Dictionary<int, ButtonState> buttonsToRefresh;
            lock(invalidatedButtonsLock)
            {
                buttonsToRefresh = invalidatedButtons;
                invalidatedButtons = new Dictionary<int, ButtonState>();
            }
            
            //Push key refreshes
            buttonBoard.SetKeyStates(buttonsToRefresh);

            return Task.FromResult(true);
        }
    }
}
