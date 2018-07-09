﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Reflection;

namespace MessageServer
{
    public partial class ServerMain : Form
    {
        public ServerMain()
        {
            InitializeComponent();
            fileSystemWatcherMessages.Path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            labelIP.Text = LocalIPAddress().ToString();
        }

        private void ServerMain_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'messageDbDataSet1.Table' table. You can move, or remove it, as needed.
            this.tableTableAdapter.Fill(this.messageDbDataSet1.Table);
        }

        private IPAddress LocalIPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }

        private void fileSystemWatcherMessages_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Received message file.");
        }
    }
}