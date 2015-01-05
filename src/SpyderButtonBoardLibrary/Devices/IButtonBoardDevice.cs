using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spyder.Client.Peripherals.ButtonBoards.Devices
{
    public delegate void ButtonActionHandler(object sender, ButtonActionEventArgs e);

    public interface IButtonBoardDevice
    {
        event ButtonActionHandler ButtonAction;

        bool IsRunning { get; }

        bool SetKeyState(int buttonID, ButtonState state);

        bool SetKeyStates(Dictionary<int, ButtonState> states);

        bool Startup();

        void Shutdown();
    }
}
