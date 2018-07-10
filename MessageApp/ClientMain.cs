using System;
using System.Windows.Forms;

namespace MessageApp
{
    public partial class ClientMain : Form
    {
        public ClientMain()
        {
            InitializeComponent();
            Properties.Settings.Default.Reload();
            labelConnectionStatus.Text = "Connected to " + Properties.Settings.Default.lastIP + " as " + Properties.Settings.Default.lastUsername;
            cleanupMessagesFolder();
            System.IO.File.WriteAllText("script.dat", "cd Users\nget " + Properties.Settings.Default.lastUsername + ".msgusr msg.msgusr\nquit"); // Get messages script
            executeFtpScript();
            this.Text = "Welcome " + Properties.Settings.Default.lastUsername + " - Messaging Client";
        }

        private void buttonNewMessage_Click(object sender, EventArgs e)
        {
            ClientNewMessage clientNewMessage = new ClientNewMessage();
            clientNewMessage.ShowDialog();
        }

        public void executeFtpScript()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c ftp.exe -A -s:script.dat " + Properties.Settings.Default.lastIP;
            process.StartInfo = startInfo;
            process.Start();
        }

        private void cleanupMessagesFolder()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c del /Q Messages";
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}