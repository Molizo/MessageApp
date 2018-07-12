using System;
using System.Collections.Generic;
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
        private System.Diagnostics.Process backend = new System.Diagnostics.Process(); //The backend process objects
        private System.Diagnostics.ProcessStartInfo backendStartInfo = new System.Diagnostics.ProcessStartInfo();
        private List<string> users = new List<string>(); //This is a list of all users who sent and received messages

        public ServerMain()
        {
            InitializeComponent();
            fileSystemWatcherMessages.Path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            labelIP.Text = LocalIPAddress().ToString();
            createUsersFolder();
            backendServerStart();
            loadUsers();
            foreach (string user in users) //Regenerates the message tables for each user
            {
                generateUserMessageFiles(user);
            }
        }

        private void createUsersFolder()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c mkdir Users";
            process.StartInfo = startInfo;
            process.Start();
        }

        private void ServerMain_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'messageDbDataSet1.Table' table. You can move, or remove it, as needed.
            this.tableTableAdapter.Fill(this.messageDbDataSet1.Table);
        }

        private IPAddress LocalIPAddress() //Gets the IP address of the current PC for use in the backend and for convenience.
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

        private void fileSystemWatcherMessages_Created(object sender, FileSystemEventArgs e) //This searches for .msgdat files containing data such as the 'Sender','Receiver','Message',adds it to the master table and redoes the message tables for each user.
        {
            Console.WriteLine("Received message file.");  //Read the message file
            System.Threading.Thread.Sleep(300);
            string fileContents = System.IO.File.ReadAllText(new FileInfo(e.FullPath).Name);
            System.Threading.Thread.Sleep(30);
            System.IO.File.Delete(new FileInfo(e.FullPath).Name);

            string[] content = fileContents.Split(',');  //Adds users to user list dynamically.
            users.Add(content[0].Trim('\''));
            users.Add(content[1].Trim('\''));

            string query = "INSERT INTO [dbo].[Table]([ID],[TimeStamp], [Sender], [Receiver], [Message]) Values('" +    //Add new messages into the master table
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

            foreach (string user in users) //Regenerates the message tables for each user
            {
                generateUserMessageFiles(user);
            }
        }

        private void backendServerStart()  //Starts the backend server that listens for outside connections
        {
            backendStartInfo.FileName = "cmd.exe";
            backendStartInfo.Arguments = "/C NTserver.exe -ha " + LocalIPAddress().ToString() + " \"" + System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\"";
            backend.StartInfo = backendStartInfo;
            backend.Start();
        }

        private void backendServerStop()   //Stops the backend server
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c taskkill /im NTserver.exe /F";
            process.StartInfo = startInfo;
            process.Start();
        }

        private void ServerMain_FormClosing(object sender, FormClosingEventArgs e)  //What to do when the app is closing
        {
            backendServerStop();
            saveUsers();
            Properties.Settings.Default.Save();
        }

        private void saveUsers() //This saves a list of all users who sent and received messages
        {
            System.IO.File.Delete(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/users.dat");
            users.ForEach(delegate (String user)
            {
                System.IO.File.AppendAllText(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/users.dat", user + Environment.NewLine);
            });
        }

        private void loadUsers() //This loads a list of all users who sent and received messages
        {
            try
            {
                string user;
                System.IO.StreamReader userList = new System.IO.StreamReader(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/users.dat");
                while ((user = userList.ReadLine()) != null)
                {
                    users.Add(user);
                }
                userList.Close();
            }
            catch
            {
                Console.WriteLine("Unable to locate file users.dat");
                Console.WriteLine("Maybe first start or corrupted");
            }
        }

        private void generateUserMessageFiles(string user)  // This generates the received message list for a user and stores it in the Users folder with the username corresponding to each filename
        {
            string query = "SELECT [Timestamp],[Sender],[Message] FROM [dbo].[Table] WHERE [Receiver]='" + user + "'";
            string filePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/Users/" + user + ".msgusr";

            //create connection
            SqlCommand comm = new SqlCommand();
            comm.Connection = new SqlConnection(Properties.Settings.Default.messageDbConnectionString);

            comm.CommandText = query;
            comm.Connection.Open();

            SqlDataReader sqlReader = comm.ExecuteReader();

            // Open the file for write operations.  If exists, it will overwrite due to the "false" parameter
            using (StreamWriter file = new StreamWriter(filePath, false))
            {
                while (sqlReader.Read())
                {
                    string message = sqlReader["Message"].ToString().Replace(Environment.NewLine, "\"\\n\"");
                    file.WriteLine("'" + sqlReader["Timestamp"] + "','" + sqlReader["Sender"] + "','" + message + "'");
                    //The message column fuss on the row above is so that a new line is replaced by \n and \n by
                }
            }

            sqlReader.Close();
            comm.Connection.Close();
        }

        private void buttonClearDatabase_Click(object sender, EventArgs e)  //This clears the database of any records
        {
            var confirmationResult = MessageBox.Show("Are you sure you want to clear the database?", "Confirmation", MessageBoxButtons.YesNo);
            if (confirmationResult == DialogResult.Yes)
            {
                string query = "DELETE FROM [dbo].[Table]";
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.messageDbConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                this.tableTableAdapter.Fill(this.messageDbDataSet1.Table);
                foreach (string user in users) //Regenerates the message tables for each user
                {
                    generateUserMessageFiles(user);
                }
            }
        }
    }
}