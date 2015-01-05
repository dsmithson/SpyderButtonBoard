using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spyder.Client.Peripherals.ButtonBoards
{
    public class ButtonActionEventArgs : EventArgs
    {
        public int ButtonID { get; set; }
        public bool IsPressed { get; set; }
    }
}
