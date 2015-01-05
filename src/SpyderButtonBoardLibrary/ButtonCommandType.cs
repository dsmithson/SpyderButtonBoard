using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spyder.Client.Peripherals.ButtonBoards
{
    /// <summary>
    /// Byptes of available button actions for key mapping
    /// </summary>
    public enum ButtonCommandType
    {
        None,

        Lock,
        Group,

        /// <summary>
        /// Controls the command key page currently being displayed on the button board (typically 0-19)
        /// </summary>
        Page,

        /// <summary>
        /// Cycles the current page index down
        /// </summary>
        NextPage,

        /// <summary>
        /// Cycles the current page index up
        /// </summary>
        PreviousPage,

        /// <summary>
        /// Executes cue 0 
        /// </summary>
        ExecutePreview,

        /// <summary>
        /// Execues cue 1 
        /// </summary>
        ExecuteProgram,

        /// <summary>
        /// Executes an off 
        /// </summary>
        ExecuteOff,

        /// <summary>
        /// Executes the next cue 
        /// </summary>
        NextCue,

        /// <summary>
        /// Executes the previous cue 
        /// </summary>
        PreviousCue,

        /// <summary>
        /// Represents an individual Command key or function key button
        /// </summary>
        ButtonRegister
    }
}
