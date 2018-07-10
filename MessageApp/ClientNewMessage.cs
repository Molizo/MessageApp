using System;
using System.Windows.Forms;

namespace MessageApp
{
    public partial class ClientNewMessage : Form
    {
        public ClientNewMessage()
        {
            InitializeComponent();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            string fileName = "./Messages/newMessage" + Properties.Settings.Default.lastUsername + random.Next(0, 21999999) + ".msgdat";
            string message = "'" + Properties.Settings.Default.lastUsername + "','" + textBoxTo.Text + "','" + textBoxMessage.Text.Replace(',', ' ') + "'";
            System.IO.File.WriteAllText(fileName, message);
            System.Threading.Thread.Sleep(30);
            uploadFile(fileName);
            this.Close();
        }

        private void uploadFile(string fileName)
        {
            System.IO.File.WriteAllText("script.dat", "send " + fileName + "\nquit");

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