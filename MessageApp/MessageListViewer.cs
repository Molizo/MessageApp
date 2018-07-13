using System.IO;
using System.Windows.Forms;

namespace MessageApp
{
    public partial class MessageListViewer : Form
    {
        public MessageListViewer() //Initializes stuff
        {
            InitializeComponent();
        }

        public void OpenFile(string fileName) //Opens a file for viewing
        {
            this.Text = "Message file viewer - " + Path.GetFileName(fileName) + " - Messaging Client";
            if (Path.GetExtension(fileName) == ".msgusr") //Add columns used in a .msgusr file
            {
                dataGridViewMessages.Columns.Add("columnTimestamp", "Timestamp");
                dataGridViewMessages.Columns.Add("columnSender", "From");
                dataGridViewMessages.Columns.Add("columnMessage", "Message");
            }
            else if (Path.GetExtension(fileName) == ".msgdat") //Add columns used in a .msgdat file
            {
                dataGridViewMessages.Columns.Add("columnSender", "Sender");
                dataGridViewMessages.Columns.Add("columnReceiver", "Receiver");
                dataGridViewMessages.Columns.Add("columnMessage", "Message");
            }
            else
                MessageBox.Show("Error opening the file.", "File operations Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            try
            {
                //Reads the file
                System.IO.StreamReader file = new System.IO.StreamReader(fileName);
                string line;
                while ((line = file.ReadLine()) != null) //Processes the data and adds it to the dataGridViewMessages table
                {
                    string[] data = line.Split(',');
                    data[0] = data[0].Trim('\'');
                    data[1] = data[1].Trim('\'');
                    data[2] = data[2].Trim('\'').Replace("\"\\n\"", "  ");
                    dataGridViewMessages.Rows.Add(data);
                    dataGridViewMessages.Update();
                }
                file.Close();
            }
            catch
            {
                MessageBox.Show("Error opening the file.", "File operations Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Focus();
        }
    }
}