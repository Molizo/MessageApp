﻿using System;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace MessageApp
{
    public partial class ClientLogin : Form
    {
        public ClientLogin()
        {
            InitializeComponent();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (IsMachineOnline(textBoxIP.Text) && CheckLogin(textBoxUsername.Text, textBoxPassword.Text))
            {
                this.Hide();
                var clientMain = new ClientMain();
                System.IO.File.WriteAllText(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase.ToString()), textBoxIP.Text + "\n" + textBoxUsername.Text);
                System.Threading.Thread.Sleep(15);
                clientMain.Closed += (s, args) => this.Close();
                clientMain.Show();
            }
            else
                labelError.Visible = true;
        }

        private static bool IsMachineOnline(string hostName)
        {
            bool retVal = false;
            try
            {
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();
                // Use the default Ttl value which is 128,
                // but change the fragmentation behavior.
                options.DontFragment = true;
                // Create a buffer of 32 bytes of data to be transmitted.
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;

                PingReply reply = pingSender.Send(hostName, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                Console.WriteLine(ex.Message);
            }
            return retVal;
        }

        private static bool CheckLogin(string username, string password)
        {
            return true;
        }
    }
}