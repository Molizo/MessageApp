using System;
using System.Threading;
using System.Windows.Forms;

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
            System.IO.File.Delete("msg.msgusr");
            fileSystemWatcherMessages.Path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            this.Text = "Welcome " + Properties.Settings.Default.lastUsername + " - Messaging Client";
            messageCheck.Start();
            System.Threading.Thread.Sleep(100);
            refreshMessages();
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

        private void refreshMessages()
        {
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader("msg.msgusr");
                string line;
                long counter = 0;
                dataGridViewMessages.Rows.Clear();
                while ((line = file.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    data[0] = data[0].Trim('\'');
                    data[1] = data[1].Trim('\'');
                    data[2] = data[2].Trim('\'').Replace("\"\\n\"", "  ");
                    dataGridViewMessages.Rows.Add(data);
                    dataGridViewMessages.Update();
                    counter++;
                }
                file.Close();
            }
            catch
            {
                Console.WriteLine("No message list file!");
                refreshMessages();
            }
        }

        private void fileSystemWatcherMessages_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            refreshMessages();
            System.Threading.Thread.Sleep(30);
            Console.WriteLine("Refreshing messages!");
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            refreshMessages();
            Console.WriteLine("Refreshing messages!");
        }
    }
}