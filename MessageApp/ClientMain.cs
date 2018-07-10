using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MessageApp
{
    public partial class ClientMain : Form
    {
        private FTPClient ftpClient = new FTPClient("ftp://" + Properties.Settings.Default.lastIP + "/", "anonymous", "anonymous");

        public ClientMain()
        {
            InitializeComponent();
            Properties.Settings.Default.Reload();
            labelConnectionStatus.Text = "Connected to " + Properties.Settings.Default.lastIP + " as " + Properties.Settings.Default.lastUsername;
            ftpClient.download("/Users/" + Properties.Settings.Default.lastUsername, "msg.msgdat");
            this.Text = "Welcome " + Properties.Settings.Default.lastUsername + " - Messaging Client";
        }

        private void buttonNewMessage_Click(object sender, EventArgs e)
        {
            ClientNewMessage clientNewMessage = new ClientNewMessage();
            clientNewMessage.ShowDialog();
        }
    }
}