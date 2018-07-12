namespace MessageApp
{
    partial class ClientMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.labelConnectionStatus = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.labelNewMessageNumber = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonNewMessage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonRefresh = new System.Windows.Forms.ToolStripButton();
            this.dataGridViewMessages = new System.Windows.Forms.DataGridView();
            this.columnTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileSystemWatcherMessages = new System.IO.FileSystemWatcher();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonOpen = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMessages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherMessages)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelConnectionStatus,
            this.toolStripSeparator1,
            this.labelNewMessageNumber,
            this.toolStripSeparator2,
            this.buttonNewMessage,
            this.toolStripSeparator3,
            this.buttonRefresh,
            this.toolStripSeparator4,
            this.buttonOpen});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // labelConnectionStatus
            // 
            this.labelConnectionStatus.Name = "labelConnectionStatus";
            this.labelConnectionStatus.Size = new System.Drawing.Size(226, 22);
            this.labelConnectionStatus.Text = "Connected to DUMMYIP as DUMMYUSER";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // labelNewMessageNumber
            // 
            this.labelNewMessageNumber.Name = "labelNewMessageNumber";
            this.labelNewMessageNumber.Size = new System.Drawing.Size(144, 22);
            this.labelNewMessageNumber.Text = "You have X new messages";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonNewMessage
            // 
            this.buttonNewMessage.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonNewMessage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonNewMessage.Image = ((System.Drawing.Image)(resources.GetObject("buttonNewMessage.Image")));
            this.buttonNewMessage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonNewMessage.Name = "buttonNewMessage";
            this.buttonNewMessage.Size = new System.Drawing.Size(62, 22);
            this.buttonNewMessage.Text = "Compose";
            this.buttonNewMessage.ToolTipText = "Send a new message";
            this.buttonNewMessage.Click += new System.EventHandler(this.buttonNewMessage_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefresh.Image")));
            this.buttonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(50, 22);
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // dataGridViewMessages
            // 
            this.dataGridViewMessages.AllowUserToAddRows = false;
            this.dataGridViewMessages.AllowUserToDeleteRows = false;
            this.dataGridViewMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMessages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnTimestamp,
            this.columnSender,
            this.columnMessage});
            this.dataGridViewMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMessages.Location = new System.Drawing.Point(0, 25);
            this.dataGridViewMessages.Name = "dataGridViewMessages";
            this.dataGridViewMessages.RowHeadersVisible = false;
            this.dataGridViewMessages.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMessages.Size = new System.Drawing.Size(800, 425);
            this.dataGridViewMessages.TabIndex = 3;
            // 
            // columnTimestamp
            // 
            this.columnTimestamp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.columnTimestamp.HeaderText = "Timestamp";
            this.columnTimestamp.Name = "columnTimestamp";
            this.columnTimestamp.ReadOnly = true;
            this.columnTimestamp.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnTimestamp.Width = 83;
            // 
            // columnSender
            // 
            this.columnSender.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.columnSender.HeaderText = "From";
            this.columnSender.Name = "columnSender";
            this.columnSender.ReadOnly = true;
            this.columnSender.Width = 55;
            // 
            // columnMessage
            // 
            this.columnMessage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.columnMessage.HeaderText = "Message";
            this.columnMessage.Name = "columnMessage";
            this.columnMessage.ReadOnly = true;
            this.columnMessage.Width = 75;
            // 
            // fileSystemWatcherMessages
            // 
            this.fileSystemWatcherMessages.EnableRaisingEvents = true;
            this.fileSystemWatcherMessages.Filter = "*msg.msgusr*";
            this.fileSystemWatcherMessages.SynchronizingObject = this;
            this.fileSystemWatcherMessages.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcherMessages_Changed);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonOpen
            // 
            this.buttonOpen.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonOpen.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpen.Image")));
            this.buttonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(110, 22);
            this.buttonOpen.Text = "Open Message File";
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Message Files|*.msgdat|Message User|*.msgusr";
            this.openFileDialog.Title = "Open message file - Messaging Client";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // ClientMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridViewMessages);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ClientMain";
            this.ShowIcon = false;
            this.Text = "Welcome DUMMYUSER - Messaging Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientMain_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMessages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherMessages)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel labelConnectionStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton buttonNewMessage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel labelNewMessageNumber;
        private System.Windows.Forms.DataGridView dataGridViewMessages;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTimestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSender;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnMessage;
        private System.Windows.Forms.ToolStripButton buttonRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.IO.FileSystemWatcher fileSystemWatcherMessages;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton buttonOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}