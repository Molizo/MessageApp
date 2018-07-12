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
                Console.WriteLine("Message file corrupted!");
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
                Console.WriteLine("Message file corrupted!");
            }
            this.Focus();
        }
    }
}