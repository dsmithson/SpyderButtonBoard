using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spyder.Client.Peripherals.ButtonBoards
{
    public class ButtonCommand
    {
        /// <summary>
        /// Type of logical action associated with this button
        /// </summary>
        public ButtonCommandType CommandType { get; set; }

        /// <summary>
        /// ID associated with this action.  For a command key this would be a Register ID, and for a page this would be a page index
        /// </summary>
        public int TargetID { get; set; }

        public ButtonCommand()
        {

        }

        public ButtonCommand(ButtonCommandType commandType, int targetID = 0)
        {
            this.CommandType = commandType;
            this.TargetID = targetID;
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as ButtonCommand;
            if (compareTo == null)
                return false;

            return compareTo.CommandType == this.CommandType && compareTo.TargetID == this.TargetID;
        }

        public override int GetHashCode()
        {
            //TODO:  add a hash algorithm
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} - Target ID: {1}", CommandType, TargetID);
        }
    }
}
