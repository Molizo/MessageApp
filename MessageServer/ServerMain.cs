using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace MessageServer
{
    public partial class ServerMain : Form
    {
        private System.Diagnostics.Process backend = new System.Diagnostics.Process();
        private System.Diagnostics.ProcessStartInfo backendStartInfo = new System.Diagnostics.ProcessStartInfo();

        public ServerMain()
        {
            InitializeComponent();
            fileSystemWatcherMessages.Path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            labelIP.Text = LocalIPAddress().ToString();
            backendServer();
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
            System.Threading.Thread.Sleep(300);
            string fileContents = System.IO.File.ReadAllText(new FileInfo(e.FullPath).Name);
            System.Threading.Thread.Sleep(30);
            System.IO.File.Delete(new FileInfo(e.FullPath).Name);
            string query = "INSERT INTO [dbo].[Table]([ID],[TimeStamp], [Sender], [Receiver], [Message]) Values('" +
                            Properties.Settings.Default.CurrentID + "', '" + DateTime.Now + "', " + fileContents + ")";
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.messageDbConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            this.tableTableAdapter.Fill(this.messageDbDataSet1.Table);
            Properties.Settings.Default.CurrentID++;
        }

        private void backendServer()
        {
            backendStartInfo.FileName = "cmd.exe";
            backendStartInfo.Arguments = "/C NTserver.exe -ha " + LocalIPAddress().ToString() + " \"" + System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\"";
            backend.StartInfo = backendStartInfo;
            backend.Start();
        }

        private void ServerMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c taskkill /im NTserver.exe /F";
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}