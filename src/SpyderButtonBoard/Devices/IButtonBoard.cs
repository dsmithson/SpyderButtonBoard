using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spyder.Client.Peripherals.ButtonBoards
{
    public delegate void ButtonActionHandler(object sender, ButtonActionEventArgs e);

    public interface IButtonBoard
    {
        event ButtonActionHandler ButtonAction;

        bool SetKeyState(int buttonID, ButtonState state);

        bool Startup();

        void Shutdown();
    }
}
