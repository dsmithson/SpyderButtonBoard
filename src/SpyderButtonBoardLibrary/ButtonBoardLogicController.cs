using Spyder.Client.Peripherals.ButtonBoards.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spyder.Client.Peripherals.ButtonBoards
{
    public delegate Task<bool> IsRegisterPopulatedHandler(int pageIndex, int registerID);
    public delegate void ButtonStateChangeHandler(object sender, ButtonCommand command, ButtonState state);
    public delegate void PageChangedHandler(object sender, int pageID);
    public delegate void CommandKeyRecallHandler(object sender, int pageIndex, int registerID, int cueIndex);

    /// <summary>
    /// Class which handles logical input and raises events as necessary 
    /// </summary>
    public class ButtonBoardLogicController
    {
        protected enum RecallMode {  Off = -1, Preview = 0, Program = 1 }

        protected enum ButtonStateFlags : byte
        {
            None = 0x00,
            IsOffPressed = 0x01,
            IsPreviewPressed = 0x02,
            IsProgramPressed= 0x04,
            IsLockPressed = 0x08,
            IsGroupPressed = 0x10,
            IsPreviousPagePressed = 0x20,
            IsNextPagePressed = 0x40
        }

        protected readonly Dictionary<ButtonCommand, ButtonState> buttonStates = new Dictionary<ButtonCommand, ButtonState>();
        protected readonly List<int> groupRegisterIds = new List<int>();
        protected readonly List<int> pressedRegisterIDs = new List<int>();
        protected int commandKeysRecalledWhileModeDown;
        protected RecallMode recallMode = RecallMode.Program;
        protected ButtonStateFlags buttonStateFlags;
        protected int pageCount;
        protected IsRegisterPopulatedHandler isRegisterPopulatedHandler;

        #region Events

        public event PageChangedHandler PageChanged;
        protected void OnPageChanged()
        {
            if (PageChanged != null)
                PageChanged(this, Page);
        }

        public event CommandKeyRecallHandler CommandKeyRecall;
        protected void OnCommandKeyRecall(int pageIndex, int registerID, int cueIndex)
        {
            if (CommandKeyRecall != null)
                CommandKeyRecall(this, pageIndex, registerID, cueIndex);
        }

        public event ButtonStateChangeHandler ButtonStateChanged;
        protected void OnButtonStateChanged(ButtonCommand command, ButtonState state)
        {
            if (ButtonStateChanged != null)
                ButtonStateChanged(this, command, state);
        }

        #endregion

        #region Public Properties

        public bool IsRunning { get; private set; }

        private int page;
        public int Page
        {
            get { return page; }
            set
            {
                if (page != value)
                {
                    page = value;
                    OnPageChanged();
                }
            }
        }

        #endregion

        #region Protected Properties

        protected bool IsGroupPressed
        {
            get { return GetButtonStateFlag(ButtonStateFlags.IsGroupPressed); }
            set { SetButtonStateFlag(ButtonStateFlags.IsGroupPressed, value); }
        }

        protected bool IsLockPressed
        {
            get { return GetButtonStateFlag(ButtonStateFlags.IsLockPressed); }
            set { SetButtonStateFlag(ButtonStateFlags.IsLockPressed, value); }
        }

        protected bool IsOffPressed
        {
            get { return GetButtonStateFlag(ButtonStateFlags.IsOffPressed); }
            set { SetButtonStateFlag(ButtonStateFlags.IsOffPressed, value); }
        }

        protected bool IsPreviewPressed
        {
            get { return GetButtonStateFlag(ButtonStateFlags.IsPreviewPressed); }
            set { SetButtonStateFlag(ButtonStateFlags.IsPreviewPressed, value); }
        }

        protected bool IsProgramPressed
        {
            get { return GetButtonStateFlag(ButtonStateFlags.IsProgramPressed); }
            set { SetButtonStateFlag(ButtonStateFlags.IsProgramPressed, value); }
        }

        protected bool IsPreviousPagePressed
        {
            get { return GetButtonStateFlag(ButtonStateFlags.IsPreviousPagePressed); }
            set { SetButtonStateFlag(ButtonStateFlags.IsPreviousPagePressed, value); }
        }

        protected bool IsNextPagePressed
        {
            get { return GetButtonStateFlag(ButtonStateFlags.IsNextPagePressed); }
            set { SetButtonStateFlag(ButtonStateFlags.IsNextPagePressed, value); }
        }

        private bool GetButtonStateFlag(ButtonStateFlags flag)
        {
            return (buttonStateFlags & flag) > 0;
        }

        private void SetButtonStateFlag(ButtonStateFlags flag, bool isSet)
        {
            if (isSet)
                buttonStateFlags |= flag;
            else
                buttonStateFlags &= ~flag;
        }
        #endregion

        public async Task<bool> Startup(IsRegisterPopulatedHandler isRegisterPopulatedHandler, int registerButtonCount = 100, int pageCount = 20)
        {
            Shutdown();
            IsRunning = true;
            this.pageCount = pageCount;
            this.isRegisterPopulatedHandler = isRegisterPopulatedHandler;

            await InitializeButtons(registerButtonCount, pageCount);
            await RefreshAllButtons();

            return true;
        }

        public void Shutdown()
        {
            IsRunning = false;
            isRegisterPopulatedHandler = null;

            buttonStateFlags = ButtonStateFlags.None;

            buttonStates.Clear();
            groupRegisterIds.Clear();
            pressedRegisterIDs.Clear();
        }

        protected async Task<bool> IsRegisterPopulated(int pageIndex, int registerID)
        {
            if (isRegisterPopulatedHandler == null)
                return false;

            return await isRegisterPopulatedHandler(pageIndex, registerID);
        }

        private async Task InitializeButtons(int registerButtonCount, int pageCount)
        {
            buttonStates.Clear();

            buttonStates.Add(new ButtonCommand(ButtonCommandType.Lock), ButtonState.Inactive);
            buttonStates.Add(new ButtonCommand(ButtonCommandType.Group), ButtonState.Inactive);

            //Registers
            for(int i=0 ; i<registerButtonCount ; i++)
            {
                buttonStates.Add(new ButtonCommand(ButtonCommandType.ButtonRegister, i), await IsRegisterPopulated(page, i) ? ButtonState.Inactive : ButtonState.Off);
            }
            buttonStates.Add(new ButtonCommand(ButtonCommandType.ExecuteOff), (recallMode == RecallMode.Off ? ButtonState.Active : ButtonState.Inactive));
            buttonStates.Add(new ButtonCommand(ButtonCommandType.ExecutePreview), (recallMode == RecallMode.Preview ? ButtonState.Active : ButtonState.Inactive));
            buttonStates.Add(new ButtonCommand(ButtonCommandType.ExecuteProgram), (recallMode == RecallMode.Program ? ButtonState.Active : ButtonState.Inactive));
            buttonStates.Add(new ButtonCommand(ButtonCommandType.PreviousCue), ButtonState.Off);
            buttonStates.Add(new ButtonCommand(ButtonCommandType.NextCue), ButtonState.Off);

            //Pages
            for(int i=0 ; i<pageCount ; i++)
            {
                buttonStates.Add(new ButtonCommand(ButtonCommandType.Page, i), i == page ? ButtonState.Active : ButtonState.Inactive);
            }
            buttonStates.Add(new ButtonCommand(ButtonCommandType.NextPage), ButtonState.Off);
            buttonStates.Add(new ButtonCommand(ButtonCommandType.PreviousPage), ButtonState.Off);
            RefreshPageButtons();
        }

        private void UpdateButtonState(ButtonCommandType type, Func<ButtonState> getState)
        {
            UpdateButtonState(type, (targetID) => getState());
        }

        private void UpdateButtonState(ButtonCommandType type, Func<bool> isTargetIdActive, ButtonState inactiveState = ButtonState.Inactive, ButtonState activeState = ButtonState.Active)
        {
            UpdateButtonState(type, (targetId) => isTargetIdActive());
        }

        private void UpdateButtonState(ButtonCommandType type, Func<int, bool> isTargetIdActive, ButtonState inactiveState = ButtonState.Inactive, ButtonState activeState = ButtonState.Active)
        {
            UpdateButtonState(type, (targetID) => isTargetIdActive(targetID) ? activeState : inactiveState);
        }

        private void UpdateButtonState(ButtonCommandType type, Func<int, ButtonState> getState)
        {
            //Collect changes
            var changes = new Dictionary<ButtonCommand, ButtonState>();
            foreach (var entry in buttonStates.Where(bs => bs.Key.CommandType == type))
            {
                var state = getState(entry.Key.TargetID);
                if(entry.Value != state)
                {
                    changes.Add(entry.Key, state);
                }
            }

            //Apply changes
            foreach(var change in changes)
            {
                buttonStates[change.Key] = change.Value;
                OnButtonStateChanged(change.Key, change.Value);
            }
        }

        private void UpdateButtonState(ButtonCommandType type, ButtonState state, int? targetID = null)
        {
            //Find matching command(s)
            var entries = buttonStates.Where(bs => bs.Key.CommandType == type && (targetID == null || targetID.Value == bs.Key.TargetID) && bs.Value != state).ToList();
            foreach(var entry in entries)
            {
                buttonStates[entry.Key] = state;
                OnButtonStateChanged(entry.Key, state);
            }
        }

        /// <summary>
        /// Causes the ButtonStateChanged event to be raised for all buttons
        /// </summary>
        public Task RefreshAllButtons()
        {
            foreach(var button in buttonStates)
            {
                OnButtonStateChanged(button.Key, button.Value);
            }
            return Task.FromResult(true);
        }

        public async Task ProcessKeyAction(ButtonCommand command, bool isPressed)
        {
            var existing = buttonStates.Keys.FirstOrDefault(k => k.CommandType == command.CommandType && k.TargetID == command.TargetID);
            if (existing == null || buttonStates[existing] == ButtonState.Off)
                return;
            
            if (command.CommandType == ButtonCommandType.Page || command.CommandType == ButtonCommandType.PreviousPage || command.CommandType == ButtonCommandType.NextPage)
            {
                //Don't allow page change selection while group assignments are in progress
                if (IsGroupPressed)
                    return;

                if (command.CommandType == ButtonCommandType.PreviousPage)
                    IsPreviousPagePressed = isPressed;
                else if (command.CommandType == ButtonCommandType.NextPage)
                    IsNextPagePressed = isPressed;

                if (isPressed)
                {
                    int newPage;
                    if (command.CommandType == ButtonCommandType.PreviousPage)
                        newPage = this.Page - 1;
                    else if (command.CommandType == ButtonCommandType.NextPage)
                        newPage = this.Page + 1;
                    else
                        newPage = command.TargetID;

                    //Sanity check bounds
                    if (newPage < 0)
                        newPage = 0;
                    if (newPage >= pageCount)
                        newPage = pageCount - 1;

                    //Update Page and button states
                    if (this.page != newPage)
                    {
                        this.Page = newPage;
                        RefreshPageButtons();
                        await RefreshRegisterButtons();
                    }
                }
            }
            else if(command.CommandType == ButtonCommandType.Group)
            {
                IsGroupPressed = isPressed;

                if(isPressed)
                {
                    UpdateButtonState(ButtonCommandType.Group, ButtonState.Active);
                    groupRegisterIds.Clear();
                }
                else
                {
                    //De-Select any buttons previously grouped
                    foreach(int registerID in groupRegisterIds)
                    {
                        UpdateButtonState(ButtonCommandType.ButtonRegister, ButtonState.Inactive, registerID);
                    }
                    groupRegisterIds.Clear();
                    UpdateButtonState(ButtonCommandType.Group, ButtonState.Inactive);
                }
            }
            else if(command.CommandType == ButtonCommandType.ButtonRegister)
            {
                int registerID = command.TargetID;

                if(isPressed)
                {
                    if(!pressedRegisterIDs.Contains(registerID))
                        pressedRegisterIDs.Add(registerID);

                    if(IsGroupPressed)
                    {
                        //Toggle membership in the current group
                        if (groupRegisterIds.Contains(registerID))
                        {
                            groupRegisterIds.Remove(registerID);
                            UpdateButtonState(ButtonCommandType.ButtonRegister, ButtonState.Inactive, registerID);
                        }
                        else
                        {
                            groupRegisterIds.Add(registerID);
                            UpdateButtonState(ButtonCommandType.ButtonRegister, ButtonState.Active, registerID);
                        }
                    }
                    else if (IsOffPressed)
                    {
                        commandKeysRecalledWhileModeDown++;
                        OnCommandKeyRecall(this.page, registerID, -1);
                    }
                    else if(IsPreviewPressed)
                    {
                        commandKeysRecalledWhileModeDown++;
                        OnCommandKeyRecall(this.page, registerID, 0);
                    }
                    else if(IsProgramPressed)
                    {
                        commandKeysRecalledWhileModeDown++;
                        OnCommandKeyRecall(this.page, registerID, 1);
                    }
                    else
                    {
                        //Recall the default command key mode
                        OnCommandKeyRecall(this.page, registerID, (int)this.recallMode);
                    }
                }
                else
                {
                    if(pressedRegisterIDs.Contains(registerID))
                        pressedRegisterIDs.Remove(registerID);
                }
            }
            else if(command.CommandType == ButtonCommandType.ExecuteOff || command.CommandType == ButtonCommandType.ExecutePreview || command.CommandType == ButtonCommandType.ExecuteProgram)
            {
                if(isPressed)
                {
                    UpdateButtonState(command.CommandType, ButtonState.Active);

                    if (command.CommandType == ButtonCommandType.ExecuteOff)
                        IsOffPressed = true;
                    else if (command.CommandType == ButtonCommandType.ExecutePreview)
                        IsPreviewPressed = true;
                    else if (command.CommandType == ButtonCommandType.ExecuteProgram)
                        IsProgramPressed = true;

                    if (IsGroupPressed)
                    {
                        //Execute selected registers in the group now
                        foreach (int registerID in groupRegisterIds)
                        {
                            if (command.CommandType == ButtonCommandType.ExecuteOff)
                                OnCommandKeyRecall(page, registerID, -1);
                            else if (command.CommandType == ButtonCommandType.ExecutePreview)
                                OnCommandKeyRecall(page, registerID, 0);
                            else if (command.CommandType == ButtonCommandType.ExecuteProgram)
                                OnCommandKeyRecall(page, registerID, 1);
                        }
                        commandKeysRecalledWhileModeDown = -1;
                    }
                    else
                    {
                        //Keep count of the number of keys 
                        commandKeysRecalledWhileModeDown = 0;
                    }
                }
                else
                {
                    if (command.CommandType == ButtonCommandType.ExecuteOff)
                        IsOffPressed = false;
                    else if (command.CommandType == ButtonCommandType.ExecutePreview)
                        IsPreviewPressed = false;
                    else if (command.CommandType == ButtonCommandType.ExecuteProgram)
                        IsProgramPressed = false;

                    if(!IsGroupPressed && commandKeysRecalledWhileModeDown == 0)
                    {
                        //Switch recall mode
                        if (command.CommandType == ButtonCommandType.ExecuteOff)
                            recallMode = RecallMode.Off;
                        else if (command.CommandType == ButtonCommandType.ExecutePreview)
                            recallMode = RecallMode.Preview;
                        else if (command.CommandType == ButtonCommandType.ExecuteProgram)
                            recallMode = RecallMode.Program;
                    }

                    //Update button states
                    UpdateButtonState(ButtonCommandType.ExecuteOff, () => recallMode == RecallMode.Off || IsOffPressed);
                    UpdateButtonState(ButtonCommandType.ExecutePreview, () => recallMode == RecallMode.Preview || IsPreviewPressed);
                    UpdateButtonState(ButtonCommandType.ExecuteProgram, () => recallMode == RecallMode.Program || IsProgramPressed);
                }
            }
        }

        private void RefreshPageButtons()
        {
            //Current page
            UpdateButtonState(ButtonCommandType.Page, (targetID) =>
                {
                    if (targetID < 0 || targetID >= pageCount)
                        return ButtonState.Off;
                    else if (targetID == page)
                        return ButtonState.Active;
                    else
                        return ButtonState.Inactive;
                });

            //Previous Page
            UpdateButtonState(ButtonCommandType.PreviousPage, () =>
                {
                    if (page <= 0)
                        return ButtonState.Off;
                    else if (IsPreviousPagePressed)
                        return ButtonState.Active;
                    else
                        return ButtonState.Inactive;
                });

            //Next Page
            UpdateButtonState(ButtonCommandType.NextPage, () =>
                {
                    if (page >= pageCount)
                        return ButtonState.Off;
                    else if (IsNextPagePressed)
                        return ButtonState.Active;
                    else
                        return ButtonState.Inactive;
                });
        }

        private async Task RefreshRegisterButtons()
        {
            //Collect changes
            var newStates = new Dictionary<int, ButtonState>();
            foreach(ButtonCommand cmd in buttonStates.Keys.Where(k => k.CommandType == ButtonCommandType.ButtonRegister))
            {
                bool isRegisterPopulated = await IsRegisterPopulated(this.page, cmd.TargetID);
                ButtonState state = (isRegisterPopulated ? ButtonState.Inactive : ButtonState.Off);
                if(isRegisterPopulated && (groupRegisterIds.Contains(cmd.TargetID) || pressedRegisterIDs.Contains(cmd.TargetID)))
                    state = ButtonState.Active;

                newStates.Add(cmd.TargetID, state);
            }

            //Apply updates
            foreach(var newState in newStates)
            {
                UpdateButtonState(ButtonCommandType.ButtonRegister, newState.Value, newState.Key);
            }
        }
    }
}
