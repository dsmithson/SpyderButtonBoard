using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spyder.Client.Peripherals.ButtonBoards
{
    /// <summary>
    /// Represents the mapping of physical buttons on a button board to logical actions
    /// </summary>
    public class ButtonMap
    {
        private int horizontalButtonCount = 10;
        public int HorizontalButtonCount
        {
            get { return horizontalButtonCount; }
            set { horizontalButtonCount = value; }
        }

        private int verticalButtonCount = 10;
        public int VerticalButtonCount
        {
            get { return verticalButtonCount; }
            set { verticalButtonCount = value; }
        }

        private Dictionary<int, ButtonMapping> mappings = new Dictionary<int, ButtonMapping>();
        public Dictionary<int, ButtonMapping> Mappings
        {
            get { return mappings; }
            set { mappings = value; }
        }
        
        public async Task<bool> SaveAsync()
        {
            //TODO:  Wire me up
            await Task.Delay(20);
            return false;
        }

        public async Task<bool> LoadAsync()
        {
            //TODO:  Wire me up
            await Task.Delay(20);
            return false;
        }
    }
}
