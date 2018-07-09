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
        private string ip, user;

        public ClientMain()
        {
            InitializeComponent();
            Console.WriteLine("Connected to " + ip + " as " + user);
        }
    }
}