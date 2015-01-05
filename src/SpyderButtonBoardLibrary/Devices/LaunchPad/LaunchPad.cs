using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Knightware.IO.Devices;

namespace Spyder.Client.Peripherals.ButtonBoards.Devices.LaunchPad
{
    public class LaunchPad : IButtonBoardDevice, IButtonMapProvider, IDisposable
    {
        private bool isDisposed;
        private LaunchPadDevice launchPad;

        public event ButtonActionHandler ButtonAction;
        protected void OnButtonAction(ButtonActionEventArgs e)
        {
            if (ButtonAction != null)
                ButtonAction(this, e);
        }

        public bool IsRunning { get; private set; }

        public bool SetKeyStates(Dictionary<int, ButtonState> states)
        {
            //TODO:  Optimize with one interop call for all states
            bool success = true;
            foreach (var state in states)
            {
                if (!SetKeyState(state.Key, state.Value))
                    success = false;
            }
            return success;
        }

        public bool SetKeyState(int buttonID, ButtonState state)
        {
            if (!IsRunning)
                return false;

            //Determine our target color based on the provided state
            LaunchPadColor color;
            switch (state)
            {
                case ButtonState.Off:
                    color = LaunchPadColors.Off;
                    break;
                case ButtonState.Inactive:
                    color = LaunchPadColors.LightGreen;
                    break;
                case ButtonState.Active:
                    color = LaunchPadColors.BrightGreen;
                    break;
                case ButtonState.Preview:
                    color = LaunchPadColors.Yellow;
                    break;
                case ButtonState.Program:
                    color = LaunchPadColors.Red;
                    break;
                default:
                    Debug.WriteLine("No color mapped in LaunchPadDevice for specified color: " + state);
                    return false;
            }

            //Shift out the key type from the actual button ID
            LaunchPadKeyType keyType;
            byte keyId;
            DecodeKey(buttonID, out keyType, out keyId);

            return launchPad.SetKeyColor(keyType, keyId, color);
        }

        public bool Startup()
        {
            Shutdown();
            IsRunning = true;

            launchPad = new Knightware.IO.Devices.LaunchPadDevice();
            if (!launchPad.Startup(launchPad_KeyPressed))
            {
                launchPad.Dispose();
                launchPad = null;
                return false;
            }
            return true;
        }

        public void Shutdown()
        {
            IsRunning = false;

            if (launchPad != null)
            {
                launchPad.Shutdown();
                launchPad.Dispose();
                launchPad = null;
            }
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposed)
                return;

            if (isDisposing)
            {
                //Clean up managed resources
                if (launchPad != null)
                {
                    launchPad.Dispose();
                    launchPad = null;
                }
            }

            //Clean up unmanaged resources

            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void launchPad_KeyPressed(object sender, LaunchPadKeyPressEventArgs e)
        {
            OnButtonAction(new ButtonActionEventArgs()
                {
                    ButtonID = EncodeKey(e.KeyType, e.KeyId),
                    IsPressed = e.IsPressed
                });
        }

        protected int EncodeKey(LaunchPadKeyType keyType, byte keyId)
        {
            int response = keyId | ((int)keyType << 8);
            return response;
        }

        protected void DecodeKey(int encodedKey, out LaunchPadKeyType keyType, out byte keyId)
        {
            keyType = (LaunchPadKeyType)(byte)(encodedKey >> 8);
            keyId = (byte)encodedKey;
        }

        public ButtonMap GetDefaultButtonMap()
        {
            const int buttonAreaColumns = 8;
            const int buttonAreaRows = 8;

            var response = new ButtonMap()
            {
                HorizontalButtonCount = 9,
                VerticalButtonCount = 9
            };

            //Pages (right side of button board)
            for (int i = 0; i < buttonAreaRows; i++)
            {
                byte keyId = (byte)(0x08 | (0x10 * i));
                AddButtonMapping(response, LaunchPadKeyType.MainArea, keyId, ButtonCommandType.Page, i);
            }

            //Top Menu buttons
            AddButtonMapping(response, LaunchPadKeyType.TopMenu, 0x68, ButtonCommandType.PreviousCue);
            AddButtonMapping(response, LaunchPadKeyType.TopMenu, 0x69, ButtonCommandType.NextCue);
            AddButtonMapping(response, LaunchPadKeyType.TopMenu, 0x6A, ButtonCommandType.PreviousPage);
            AddButtonMapping(response, LaunchPadKeyType.TopMenu, 0x6B, ButtonCommandType.NextPage);
            AddButtonMapping(response, LaunchPadKeyType.TopMenu, 0x6C, ButtonCommandType.Group);
            AddButtonMapping(response, LaunchPadKeyType.TopMenu, 0x6D, ButtonCommandType.ExecuteOff);
            AddButtonMapping(response, LaunchPadKeyType.TopMenu, 0x6E, ButtonCommandType.ExecutePreview);
            AddButtonMapping(response, LaunchPadKeyType.TopMenu, 0x6F, ButtonCommandType.ExecuteProgram);

            //Add register buttons (Main Area)
            for (int row = 0; row < buttonAreaRows; row++)
            {
                int rowPrefix = (0x10 * row);
                for (int column = 0; column < buttonAreaColumns; column++)
                {
                    byte keyID = (byte)(rowPrefix + column);
                    int registerID = (row * buttonAreaColumns) + column;
                    AddButtonMapping(response, LaunchPadKeyType.MainArea, keyID, ButtonCommandType.ButtonRegister, registerID);
                }
            }

            return response;
        }

        private void AddButtonMapping(ButtonMap map, LaunchPadKeyType keyType, byte keyId, ButtonCommandType command, int targetID = 0)
        {
            int buttonId = EncodeKey(keyType, keyId);
            map.Mappings.Add(buttonId, new ButtonMapping(buttonId, command, targetID));
        }
    }
}
