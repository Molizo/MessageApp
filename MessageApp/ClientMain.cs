using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MessageApp
{
    public partial class ClientMain : Form
    {
        public ClientMain()
        {
            InitializeComponent();
            Properties.Settings.Default.Reload();
            labelConnectionStatus.Text = "Connected to " + Properties.Settings.Default.lastIP + " as " + Properties.Settings.Default.lastUsername;
            getMessages();
            this.Text = "Welcome " + Properties.Settings.Default.lastUsername + " - Messaging Client";
        }

        private void buttonNewMessage_Click(object sender, EventArgs e)
        {
            ClientNewMessage clientNewMessage = new ClientNewMessage();
            clientNewMessage.ShowDialog();
        }

        private void getMessages()
        {
            System.IO.File.WriteAllText("script.dat", "cd Users\nget " + Properties.Settings.Default.lastUsername + ".msgusr msg.msgusr\nquit");

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c ftp.exe -A -s:script.dat " + Properties.Settings.Default.lastIP;
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}