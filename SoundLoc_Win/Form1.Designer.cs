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
            this.sS_Status = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.bW_ReadData = new System.ComponentModel.BackgroundWorker();
            this.sP_SerialCOM = new System.IO.Ports.SerialPort(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.sS_Status.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(35, 16);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(590, 565);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // cB_COMPort
            // 
            this.cB_COMPort.FormattingEnabled = true;
            this.cB_COMPort.Location = new System.Drawing.Point(271, 78);
            this.cB_COMPort.Margin = new System.Windows.Forms.Padding(6);
            this.cB_COMPort.Name = "cB_COMPort";
            this.cB_COMPort.Size = new System.Drawing.Size(180, 33);
            this.cB_COMPort.TabIndex = 1;
            // 
            // b_Connect
            // 
            this.b_Connect.BackColor = System.Drawing.Color.White;
            this.b_Connect.FlatAppearance.BorderSize = 0;
            this.b_Connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b_Connect.Font = new System.Drawing.Font("Century Gothic", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b_Connect.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.b_Connect.Location = new System.Drawing.Point(40, 72);
            this.b_Connect.Margin = new System.Windows.Forms.Padding(4);
            this.b_Connect.Name = "b_Connect";
            this.b_Connect.Size = new System.Drawing.Size(201, 56);
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
            this.panel1.Controls.Add(this.cB_COMPort);
            this.panel1.Controls.Add(this.b_Connect);
            this.panel1.Location = new System.Drawing.Point(-13, -53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1277, 155);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.sS_Status);
            this.panel2.Controls.Add(this.richTextBox1);
            this.panel2.Location = new System.Drawing.Point(-13, 99);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1277, 654);
            this.panel2.TabIndex = 4;
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
            this.sS_Status.Location = new System.Drawing.Point(12, 594);
            this.sS_Status.Name = "sS_Status";
            this.sS_Status.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.sS_Status.Size = new System.Drawing.Size(1253, 47);
            this.sS_Status.TabIndex = 3;
            this.sS_Status.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(238, 37);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1250, 738);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.sS_Status.ResumeLayout(false);
            this.sS_Status.PerformLayout();
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
    }
}

