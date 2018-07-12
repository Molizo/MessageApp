using System.IO;
using System.Windows.Forms;

namespace MessageApp
{
    public partial class MessageListViewer : Form
    {
        public MessageListViewer()
        {
            InitializeComponent();
        }

        public void OpenFile(string fileName)
        {
            this.Text = "Message file viewer - " + Path.GetFileName(fileName) + " - Messaging Client";
            if (Path.GetExtension(fileName) == ".msgusr")
            {
                dataGridViewMessages.Columns.Add("columnTimestamp", "Timestamp");
                dataGridViewMessages.Columns.Add("columnSender", "From");
                dataGridViewMessages.Columns.Add("columnMessage", "Message");
            }
            else if (Path.GetExtension(fileName) == ".msgdat")
            {
                dataGridViewMessages.Columns.Add("columnSender", "Sender");
                dataGridViewMessages.Columns.Add("columnReceiver", "Receiver");
                dataGridViewMessages.Columns.Add("columnMessage", "Message");
            }
            else
                MessageBox.Show("Error opening the file.", "File operations Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(fileName);
                string line;
                while ((line = file.ReadLine()) != null)
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