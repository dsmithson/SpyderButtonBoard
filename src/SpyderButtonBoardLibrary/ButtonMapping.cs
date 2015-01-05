using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spyder.Client.Peripherals.ButtonBoards
{
    public class ButtonMapping : ButtonCommand
    {
        /// <summary>
        /// Physical button keycode
        /// </summary>
        public int ButtonID { get; set; }

        public ButtonMapping()
        {

        }

        public ButtonMapping(int buttonID, ButtonCommandType commandType, int targetID = 0)
            : base(commandType, targetID)
        {
            this.ButtonID = buttonID;
        }

        public override string ToString()
        {
            return string.Format("{0} - TargetID: {1}, ButtonID: {2}", CommandType, TargetID, ButtonID);
        }
    }
}
