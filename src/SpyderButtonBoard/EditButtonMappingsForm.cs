using Spyder.Client.Peripherals.ButtonBoards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spyder.Client.ButtonBoardUI
{
    public partial class EditButtonMappingsForm : Form
    {
        private ButtonMap map;

        public EditButtonMappingsForm()
        {
            InitializeComponent();

            map = new ButtonMap();
            UpdateControlsFromMap();
        }

        private void UpdateActionTypesList()
        {
            bool includeAllocated = chkShowAssignedActions.Checked;

            foreach(var cmdType in Enum.GetValues(typeof(ButtonCommandType)))
            {

            }
        }

        private void UpdateControlsFromMap(bool fullRefresh = false)
        {
            //Set button grid width and height
            udGridWidth.Value = map.HorizontalButtonCount;
            udGridHeight.Value = map.VerticalButtonCount;
            buttonEditor.HorizontalCount = map.HorizontalButtonCount;
            buttonEditor.VerticalCount = map.VerticalButtonCount;

            //Update assigned actions list
            if(fullRefresh)
            {
                lbActions.BeginUpdate();
                foreach (var cmdType in Enum.GetValues(typeof(ButtonCommandType)))
                {
                    //TODO:  Don't populate items that are already mapped
                }
                lbActions.EndUpdate();
            }
        }

        private void UpdateButtonBoardMode()
        {
            if(tabControl1.SelectedTab == tabButtons)
            {
                //Setup board for physical actions mode
                if(rbLearnMode.Checked)
                {
                    btnPhysicalButtonSkip.Visible = true;
                }
                else if(rbTestMode.Checked)
                {
                    btnPhysicalButtonSkip.Visible = false;
                }
            }
            else if(tabControl1.SelectedTab == tabActions)
            {
                //Setup board for drag/drop actions

            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void udGridWidth_ValueChanged(object sender, EventArgs e)
        {
            map.HorizontalButtonCount = (int)udGridWidth.Value;
            UpdateControlsFromMap();
        }

        private void udGridHeight_ValueChanged(object sender, EventArgs e)
        {
            map.VerticalButtonCount = (int)udGridHeight.Value;
            UpdateControlsFromMap();
        }

        private void btnPhysicalButtonSkip_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonBoardMode();
        }
    }
}
