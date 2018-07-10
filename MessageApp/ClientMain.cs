using System;
using System.Windows.Forms;
using System.Threading;

namespace MessageApp
{
    public partial class ClientMain : Form
    {
        private Thread messageCheck = new Thread(new ThreadStart(MessageCheck));

        public ClientMain()
        {
            InitializeComponent();
            Properties.Settings.Default.Reload();
            labelConnectionStatus.Text = "Connected to " + Properties.Settings.Default.lastIP + " as " + Properties.Settings.Default.lastUsername;
            cleanupMessagesFolder();
            this.Text = "Welcome " + Properties.Settings.Default.lastUsername + " - Messaging Client";
            messageCheck.Start();
        }

        private void buttonNewMessage_Click(object sender, EventArgs e)
        {
            ClientNewMessage clientNewMessage = new ClientNewMessage();
            clientNewMessage.ShowDialog();
        }

        private static void executeFtpScript()
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

        public static void MessageCheck()
        {
            while (true)
            {
                System.IO.File.WriteAllText("script.dat", "cd Users\nget " + Properties.Settings.Default.lastUsername + ".msgusr msg.msgusr\nquit");
                executeFtpScript();
                System.Threading.Thread.Sleep(Properties.Settings.Default.refreshInterval);
            }
        }

        private void ClientMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            messageCheck.Abort();
        }
    }
}