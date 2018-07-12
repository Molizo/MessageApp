using System;
using System.Net.Sockets;
using System.Windows.Forms;

namespace MessageApp
{
    public partial class ClientLogin : Form
    {
        public ClientLogin()
        {
            InitializeComponent();
            textBoxIP.Text = Properties.Settings.Default.lastIP;
            textBoxUsername.Text = Properties.Settings.Default.lastUsername;
            createMessagesFolder();
        }

        private void createMessagesFolder()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c mkdir Messages";
            process.StartInfo = startInfo;
            process.Start();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (IsMachineOnline(textBoxIP.Text) && CheckLogin(textBoxUsername.Text, textBoxPassword.Text))
            {
                Properties.Settings.Default.lastIP = textBoxIP.Text;
                Properties.Settings.Default.lastUsername = textBoxUsername.Text;
                Properties.Settings.Default.Save();
                var clientMain = new ClientMain();
                this.Hide();
                clientMain.Closed += (s, args) => this.Close();
                clientMain.Show();
            }
            else
                MessageBox.Show("Cannot connect to server.\nPlease check your connection details.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool IsMachineOnline(string host)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    var result = client.BeginConnect(host, 21, null, null);
                    var success = result.AsyncWaitHandle.WaitOne(Properties.Settings.Default.connectionTimeout);
                    if (!success)
                    {
                        return false;
                    }

                    client.EndConnect(result);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private static bool CheckLogin(string username, string password)
        {
            return true;
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageListViewer messageListViewer = new MessageListViewer();
            messageListViewer.OpenFile(openFileDialog.FileName);
            messageListViewer.Show();
            messageListViewer.Focus();
        }
    }
}