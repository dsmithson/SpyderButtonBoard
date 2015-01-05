using Spyder.Client.Peripherals.ButtonBoards.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spyder.Client.Peripherals.ButtonBoards
{
    /// <summary>
    /// Maintains a list of button LED states
    /// </summary>
    public class StateMonitoringButtonMap
    {
        private readonly ButtonMap map;

        private readonly object invalidatedButtonsLock = new object();
        private Dictionary<int, ButtonState> invalidatedButtons = new Dictionary<int, ButtonState>();

        /// <summary>
        /// Raised when the button state of one or more buttons has changed
        /// </summary>
        public event EventHandler ButtonsInvalidated;
        protected void OnButtonsInvalidated(EventArgs e)
        {
            if (ButtonsInvalidated != null)
                ButtonsInvalidated(this, e);
        }

        public StateMonitoringButtonMap(ButtonMap map)
        {
            this.map = map;
        }

        public Dictionary<int, ButtonState> GetInvalidatedButtons()
        {
            Dictionary<int, ButtonState> response;
            lock(invalidatedButtonsLock)
            {
                response = invalidatedButtons;
                invalidatedButtons = new Dictionary<int, ButtonState>();
            }
            return response;
        }
        
        public void SetButtonState(int buttonId, ButtonState state)
        {
            lock(invalidatedButtonsLock)
            {
                if (invalidatedButtons.ContainsKey(buttonId))
                    invalidatedButtons[buttonId] = state;
                else
                    invalidatedButtons.Add(buttonId, state);
            }
        }
    }
}
