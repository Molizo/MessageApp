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
    public partial class ClientNewMessage : Form
    {
        public ClientNewMessage()
        {
            InitializeComponent();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            string fileName = "newMessage" + Properties.Settings.Default.lastUsername + random.Next(0, 21999999) + ".msgdat";
            string message = "'" + Properties.Settings.Default.lastUsername + "','" + textBoxTo.Text + "','" + textBoxMessage.Text + "'";
            System.IO.File.WriteAllText(fileName, message);
            this.Close();
        }
    }
}