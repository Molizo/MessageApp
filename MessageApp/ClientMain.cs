using System;
using System.Threading;
using System.Windows.Forms;

namespace MessageApp
{
    public partial class ClientMain : Form
    {
        private Thread messageCheck = new Thread(new ThreadStart(MessageCheck));

        public ClientMain() //Initializes stuff
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

        private void buttonNewMessage_Click(object sender, EventArgs e) //Creates a new instance of the message composer
        {
            ClientNewMessage clientNewMessage = new ClientNewMessage();
            clientNewMessage.ShowDialog();
        }

        private static void executeFtpScript() //Executes script.dat
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c ftp.exe -A -s:script.dat " + Properties.Settings.Default.lastIP;
            process.StartInfo = startInfo;
            process.Start();
        }

        private void cleanupMessagesFolder() //Deletes messages in Messages folder at startup
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c del /Q Messages";
            process.StartInfo = startInfo;
            process.Start();
        }

        public static void MessageCheck() //Checks for new messages every 3000 ms (Defined in Settings.settings as refreshInterval)
        {
            while (true)
            {
                System.IO.File.WriteAllText("script.dat", "cd Users\nget " + Properties.Settings.Default.lastUsername + ".msgusr msg.msgusr\nquit");
                executeFtpScript();
                System.Threading.Thread.Sleep(Properties.Settings.Default.refreshInterval);
            }
        }

        private void ClientMain_FormClosing(object sender, FormClosingEventArgs e) //Closes working threads at app closing
        {
            messageCheck.Abort();
        }

        private void refreshMessages() //Gets messages saved in msg.msgusr and adds them to dataGridViewMessages
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

        private void fileSystemWatcherMessages_Changed(object sender, System.IO.FileSystemEventArgs e) //Refreshes messages when a new msg.msgusr is downloaded via MessageCheck().
        {
            refreshMessages();
            System.Threading.Thread.Sleep(30);
            Console.WriteLine("Refreshing messages!");
        }

        private void buttonRefresh_Click(object sender, EventArgs e) //Manually refreshes messages
        {
            refreshMessages();
            Console.WriteLine("Refreshing messages!");
        }

        private void openFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e) //Opens message file viewer form
        {
            MessageListViewer messageListViewer = new MessageListViewer();
            messageListViewer.OpenFile(openFileDialog.FileName);
            messageListViewer.Show();
            messageListViewer.Focus();
        }

        private void buttonOpen_Click(object sender, EventArgs e) //Pops up an openFileDialog file selection dialog for selecting a file to be opened in messageListViewer
        {
            openFileDialog.ShowDialog();
        }
    }
}