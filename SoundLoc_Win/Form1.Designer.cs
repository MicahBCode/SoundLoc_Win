namespace SoundLoc_Win
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.cB_COMPort = new System.Windows.Forms.ComboBox();
            this.b_Connect = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.sS_Status = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.bW_ReadData = new System.ComponentModel.BackgroundWorker();
            this.sP_SerialCOM = new System.IO.Ports.SerialPort(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tB_mic2_x = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tB_mic3_x = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tB_mic3_y = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.sS_Status.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(16, 8);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(380, 400);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // cB_COMPort
            // 
            this.cB_COMPort.FormattingEnabled = true;
            this.cB_COMPort.Location = new System.Drawing.Point(141, 53);
            this.cB_COMPort.Name = "cB_COMPort";
            this.cB_COMPort.Size = new System.Drawing.Size(92, 21);
            this.cB_COMPort.TabIndex = 1;
            // 
            // b_Connect
            // 
            this.b_Connect.BackColor = System.Drawing.Color.White;
            this.b_Connect.FlatAppearance.BorderSize = 0;
            this.b_Connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b_Connect.Font = new System.Drawing.Font("Century Gothic", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b_Connect.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.b_Connect.Location = new System.Drawing.Point(16, 39);
            this.b_Connect.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.b_Connect.Name = "b_Connect";
            this.b_Connect.Size = new System.Drawing.Size(110, 32);
            this.b_Connect.TabIndex = 0;
            this.b_Connect.Text = "Verbinden";
            this.b_Connect.UseVisualStyleBackColor = false;
            this.b_Connect.Click += new System.EventHandler(this.b_Connect_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.cB_COMPort);
            this.panel1.Controls.Add(this.b_Connect);
            this.panel1.Location = new System.Drawing.Point(-6, -28);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(820, 123);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.sS_Status);
            this.panel2.Controls.Add(this.richTextBox1);
            this.panel2.Location = new System.Drawing.Point(-6, 96);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(820, 445);
            this.panel2.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(401, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 400);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // sS_Status
            // 
            this.sS_Status.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sS_Status.AutoSize = false;
            this.sS_Status.Dock = System.Windows.Forms.DockStyle.None;
            this.sS_Status.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.sS_Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.sS_Status.Location = new System.Drawing.Point(6, 414);
            this.sS_Status.Name = "sS_Status";
            this.sS_Status.Padding = new System.Windows.Forms.Padding(0, 0, 7, 0);
            this.sS_Status.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.sS_Status.Size = new System.Drawing.Size(808, 24);
            this.sS_Status.SizingGrip = false;
            this.sS_Status.TabIndex = 3;
            this.sS_Status.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(97, 19);
            this.toolStripStatusLabel1.Text = "nicht verbunden!";
            // 
            // bW_ReadData
            // 
            this.bW_ReadData.WorkerSupportsCancellation = true;
            this.bW_ReadData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bW_ReadData_DoWork);
            // 
            // sP_SerialCOM
            // 
            this.sP_SerialCOM.BaudRate = 115200;
            this.sP_SerialCOM.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.sP_SerialCOM_DataReceived);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tB_mic2_x);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Location = new System.Drawing.Point(395, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 51);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mikrofon 2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "x-Koordinate";
            // 
            // tB_mic2_x
            // 
            this.tB_mic2_x.Location = new System.Drawing.Point(94, 17);
            this.tB_mic2_x.Name = "tB_mic2_x";
            this.tB_mic2_x.Size = new System.Drawing.Size(100, 20);
            this.tB_mic2_x.TabIndex = 1;
            this.tB_mic2_x.Text = "100";
            this.tB_mic2_x.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tB_mic2_x.TextChanged += new System.EventHandler(this.micCoordsChanged);
            this.tB_mic2_x.Leave += new System.EventHandler(this.micCoordsFocusLeft);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tB_mic3_y);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tB_mic3_x);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Location = new System.Drawing.Point(601, 37);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 72);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mikrofon 3";
            // 
            // tB_mic3_x
            // 
            this.tB_mic3_x.Location = new System.Drawing.Point(94, 17);
            this.tB_mic3_x.Name = "tB_mic3_x";
            this.tB_mic3_x.Size = new System.Drawing.Size(100, 20);
            this.tB_mic3_x.TabIndex = 1;
            this.tB_mic3_x.Text = "0";
            this.tB_mic3_x.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tB_mic3_x.TextChanged += new System.EventHandler(this.micCoordsChanged);
            this.tB_mic3_x.Leave += new System.EventHandler(this.micCoordsFocusLeft);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "x-Koordinate";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "y-Koordinate";
            // 
            // tB_mic3_y
            // 
            this.tB_mic3_y.Location = new System.Drawing.Point(94, 44);
            this.tB_mic3_y.Name = "tB_mic3_y";
            this.tB_mic3_y.Size = new System.Drawing.Size(100, 20);
            this.tB_mic3_y.TabIndex = 3;
            this.tB_mic3_y.Text = "100";
            this.tB_mic3_y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tB_mic3_y.TextChanged += new System.EventHandler(this.micCoordsChanged);
            this.tB_mic3_y.Leave += new System.EventHandler(this.micCoordsFocusLeft);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(142, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "COM-Port";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(807, 534);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.sS_Status.ResumeLayout(false);
            this.sS_Status.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ComboBox cB_COMPort;
        private System.Windows.Forms.Button b_Connect;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.StatusStrip sS_Status;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.ComponentModel.BackgroundWorker bW_ReadData;
        private System.IO.Ports.SerialPort sP_SerialCOM;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tB_mic2_x;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tB_mic3_y;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tB_mic3_x;
        private System.Windows.Forms.Label label2;
    }
}

