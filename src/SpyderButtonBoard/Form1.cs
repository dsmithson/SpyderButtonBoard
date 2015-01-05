using PCLStorage;
using Spyder.Client;
using Spyder.Client.ButtonBoardUI;
using Spyder.Client.Net;
using Spyder.Client.Peripherals.ButtonBoards;
using Spyder.Client.Peripherals.ButtonBoards.Devices;
using Spyder.Client.Peripherals.ButtonBoards.Devices.LaunchPad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpyderButtonBoard
{
    public partial class Form1 : Form
    {
        private SpyderClientManager clientMgr;
        private ButtonBoard buttonBoard;
        private BindableSpyderClient server;
        private IButtonBoardDevice launchPad;

        public Form1()
        {
            InitializeComponent();
        }

        private void lnkEditButtons_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new EditButtonMappingsForm();
            form.Show();
        }

        private async void btnGo_Click(object sender, EventArgs e)
        {
            var server = cbServers.SelectedItem as BindableSpyderClient;
            if (server != null)
            {
                btnGo.Enabled = false;

                launchPad = new LaunchPad();
                if(!launchPad.Startup())
                {
                    MessageBox.Show(this, "Failed to startup launchpad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                buttonBoard = new ButtonBoard();
                if(!await buttonBoard.Startup(launchPad, ((IButtonMapProvider)launchPad).GetDefaultButtonMap(), server))
                {
                    MessageBox.Show(this, "Failed to start button board", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            IFolder localCacheRoot = await FileSystem.Current.LocalStorage.CreateFolderAsync("Servers", PCLStorage.CreationCollisionOption.OpenIfExists);
            clientMgr = new SpyderClientManager(localCacheRoot);
            clientMgr.ServerListChanged += clientMgr_ServerListChanged;
            await clientMgr.Startup();
        }

        void clientMgr_ServerListChanged(object sender, EventArgs e)
        {
            Invoke((Action)(() =>
            {
                cbServers.DataSource = clientMgr.GetServers();
                cbServers.DisplayMember = "ServerDisplayName";
            }));
        }
    }
}
