using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Management;
using System.Drawing.Drawing2D;

namespace SoundLoc_Win
{
    public partial class Form1 : Form
    {
        struct s_Microphone
        {
            public double d_xCoords;
            public double d_yCoords;

            public double d_time;
        }

        struct s_Position
        {
            public double d_xCoords;
            public double d_yCoords;
            public double d_rValue;
        }

        s_Microphone Microphone_1;
        s_Microphone Microphone_2;
        s_Microphone Microphone_3;

        s_Position resultPosition;

        bool getData = false;
        bool newData = false;
        string data = "1:47us;2:378us;3:0s;";

        public Form1()
        {
            InitializeComponent();

            Microphone_1.d_xCoords = 0;
            Microphone_1.d_yCoords = 0;

            Microphone_2.d_xCoords = 100;
            Microphone_2.d_yCoords = 0;

            Microphone_3.d_xCoords = 0;
            Microphone_3.d_yCoords = 100;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cB_COMPort.Items.Clear();
            foreach (string comport in SerialPort.GetPortNames())
            {
                cB_COMPort.Items.Add(comport);
            }

            ManagementObjectCollection ManObjReturn;
            ManagementObjectSearcher ManObjSearch;
            ManObjSearch = new ManagementObjectSearcher("SELECT * FROM Win32_SerialPort WHERE PNPDeviceID=\"USB\\\\VID_0FC8&PID_0001\\\\208335C0414E\"");
            ManObjReturn = ManObjSearch.Get();
            foreach (ManagementObject ManObj in ManObjReturn)
            {
                string description = ManObj["DeviceID"].ToString();
                cB_COMPort.Text = description;
                b_Connect_Click(null, new EventArgs());
            }
        }

        private s_Position calculateSoundCoords(s_Microphone mic_1, s_Microphone mic_2, s_Microphone mic_3)
        {
            s_Position result;

            double d_v = 340.0; // 340 m/s

            double x1 = mic_2.d_xCoords;
            double r1 = (mic_2.d_time - mic_1.d_time) * d_v / 10;

            double x2 = mic_3.d_xCoords;
            double y2 = mic_3.d_yCoords;
            double r2 = (mic_3.d_time - mic_1.d_time) * d_v / 10;

            double x1_2 = x1 * x1;
            double x1_3 = Math.Pow(x1, 3);
            double x1_4 = Math.Pow(x1, 4);
            double r1_2 = r1 * r1;
            double r1_3 = Math.Pow(r1, 3);
            double r1_4 = Math.Pow(r1, 4);

            double x2_2 = x2 * x2;
            double x2_3 = Math.Pow(x2, 3);
            double x2_4 = Math.Pow(x2, 4);
            double y2_2 = y2 * y2;
            double y2_3 = Math.Pow(y2, 3);
            double y2_4 = Math.Pow(y2, 4);
            double r2_2 = r2 * r2;
            double r2_3 = Math.Pow(r2, 3);
            double r2_4 = Math.Pow(r2, 4);

            result.d_rValue = (-1 * r1_3 * x2_2 - r1_3 * y2_2 + r1_2 * r2 * x1 * x2 + r1 * r2_2 * x1 * x2 + r1 * x1_2 * x2_2 + r1 * x1_2 * y2_2 - r1 * x1 * x2_3 - r1 * x1 * x2 * y2_2 - r2_3 * x1_2 - r2 * x1_3 * x2 + r2 * x1_2 * x2_2 + r2 * x1_2 * y2_2 + x1 * y2 * Math.Sqrt(-1 * r1_4 * r2_2 + r1_4 * x2_2 + r1_4 * y2_2 + 2 * r1_3 * r2_3 - 2 * r1_3 * r2 * x2_2 - 2 * r1_3 * r2 * y2_2 - r1_2 * r2_4 + 2 * r1_2 * r2_2 * x1_2 - 2 * r1_2 * r2_2 * x1 * x2 + 2 * r1_2 * r2_2 * x2_2 + 2 * r1_2 * r2_2 * y2_2 - 2 * r1_2 * x1_2 * x2_2 - 2 * r1_2 * x1_2 * y2_2 + 2 * r1_2 * x1 * x2_3 + 2 * r1_2 * x1 * x2 * y2_2 - r1_2 * x2_4 - 2 * r1_2 * x2_2 * y2_2 - r1_2 * y2_4 - 2 * r1 * r2_3 * x1_2 + 2 * r1 * r2 * x1_2 * x2_2 + 2 * r1 * r2 * x1_2 * y2_2 + r2_4 * x1_2 - r2_2 * x1_4 + 2 * r2_2 * x1_3 * x2 - 2 * r2_2 * x1_2 * x2_2 - 2 * r2_2 * x1_2 * y2_2 + x1_4 * x2_2 + x1_4 * y2_2 - 2 * x1_3 * x2_3 - 2 * x1_3 * x2 * y2_2 + x1_2 * x2_4 + 2 * x1_2 * x2_2 * y2_2 + x1_2 * y2_4)) / (2 * r1_2 * x2_2 + 2 * r1_2 * y2_2 - 4 * r1 * r2 * x1 * x2 + 2 * r2_2 * x1_2 - 2 * x1_2 * y2_2);
            if(result.d_rValue < 0)
            {
                result.d_rValue  = (-1 * r1_3 * x2_2 - r1_3 * y2_2 + r1_2 * r2 * x1 * x2 + r1 * r2_2 * x1 * x2 + r1 * x1_2 * x2_2 + r1 * x1_2 * y2_2 - r1 * x1 * x2_3 - r1 * x1 * x2 * y2_2 - r2_3 * x1_2 - r2 * x1_3 * x2 + r2 * x1_2 * x2_2 + r2 * x1_2 * y2_2 - x1 * y2 * Math.Sqrt(-1 * r1_4 * r2_2 + r1_4 * x2_2 + r1_4 * y2_2 + 2 * r1_3 * r2_3 - 2 * r1_3 * r2 * x2_2 - 2 * r1_3 * r2 * y2_2 - r1_2 * r2_4 + 2 * r1_2 * r2_2 * x1_2 - 2 * r1_2 * r2_2 * x1 * x2 + 2 * r1_2 * r2_2 * x2_2 + 2 * r1_2 * r2_2 * y2_2 - 2 * r1_2 * x1_2 * x2_2 - 2 * r1_2 * x1_2 * y2_2 + 2 * r1_2 * x1 * x2_3 + 2 * r1_2 * x1 * x2 * y2_2 - r1_2 * x2_4 - 2 * r1_2 * x2_2 * y2_2 - r1_2 * y2_4 - 2 * r1 * r2_3 * x1_2 + 2 * r1 * r2 * x1_2 * x2_2 + 2 * r1 * r2 * x1_2 * y2_2 + r2_4 * x1_2 - r2_2 * x1_4 + 2 * r2_2 * x1_3 * x2 - 2 * r2_2 * x1_2 * x2_2 - 2 * r2_2 * x1_2 * y2_2 + x1_4 * x2_2 + x1_4 * y2_2 - 2 * x1_3 * x2_3 - 2 * x1_3 * x2 * y2_2 + x1_2 * x2_4 + 2 * x1_2 * x2_2 * y2_2 + x1_2 * y2_4)) / (2 * r1_2 * x2_2 + 2 * r1_2 * y2_2 - 4 * r1 * r2 * x1 * x2 + 2 * r2_2 * x1_2 - 2 * x1_2 * y2_2);
                result.d_xCoords = (r1_3 * r2 * x2 - r1_2 * r2_2 * x1 - r1_2 * r2_2 * x2 + r1_2 * x1 * y2_2 + r1_2 * x2_3 + r1_2 * x2 * y2_2 + r1 * r2_3 * x1 - r1 * r2 * x1_2 * x2 - r1 * r2 * x1 * x2_2 - r1 * r2 * x1 * y2_2 + r2_2 * x1_3 - x1_3 * y2_2 + r1 * y2 * Math.Sqrt(-1 * r1_4 * r2_2 + r1_4 * x2_2 + r1_4 * y2_2 + 2 * r1_3 * r2_3 - 2 * r1_3 * r2 * x2_2 - 2 * r1_3 * r2 * y2_2 - r1_2 * r2_4 + 2 * r1_2 * r2_2 * x1_2 - 2 * r1_2 * r2_2 * x1 * x2 + 2 * r1_2 * r2_2 * x2_2 + 2 * r1_2 * r2_2 * y2_2 - 2 * r1_2 * x1_2 * x2_2 - 2 * r1_2 * x1_2 * y2_2 + 2 * r1_2 * x1 * x2_3 + 2 * r1_2 * x1 * x2 * y2_2 - r1_2 * x2_4 - 2 * r1_2 * x2_2 * y2_2 - r1_2 * y2_4 - 2 * r1 * r2_3 * x1_2 + 2 * r1 * r2 * x1_2 * x2_2 + 2 * r1 * r2 * x1_2 * y2_2 + r2_4 * x1_2 - r2_2 * x1_4 + 2 * r2_2 * x1_3 * x2 - 2 * r2_2 * x1_2 * x2_2 - 2 * r2_2 * x1_2 * y2_2 + x1_4 * x2_2 + x1_4 * y2_2 - 2 * x1_3 * x2_3 - 2 * x1_3 * x2 * y2_2 + x1_2 * x2_4 + 2 * x1_2 * x2_2 * y2_2 + x1_2 * y2_4)) / (2 * r1_2 * x2_2 + 2 * r1_2 * y2_2 - 4 * r1 * r2 * x1 * x2 + 2 * r2_2 * x1_2 - 2 * x1_2  * y2_2);
                result.d_yCoords = (r1_3 * r2 * y2 - r1_2 * r2_2 * y2 - r1_2 * x1 * x2 * y2 + r1_2 * x2_2 * y2 + r1_2 * y2_3 - r1 * r2 * x1_2 * y2 + r2_2 * x1_2 * y2 + x1_3 * x2 * y2 - x1_2 * x2_2 * y2 - x1_2 * y2_3 + (-1 * r1 * x2 + r2 * x1) * Math.Sqrt(-1 * r1_4 * r2_2 + r1_4 * x2_2 + r1_4 * y2_2 + 2 * r1_3 * r2_3 - 2 * r1_3 * r2 * x2_2 - 2 * r1_3 * r2 * y2_2 - r1_2 * r2_4 + 2 * r1_2 * r2_2 * x1_2 - 2 * r1_2 * r2_2 * x1 * x2 + 2 * r1_2 * r2_2 * x2_2 + 2 * r1_2 * r2_2 * y2_2 - 2 * r1_2 * x1_2 * x2_2 - 2 * r1_2 * x1_2 * y2_2 + 2 * r1_2 * x1 * x2_3 + 2 * r1_2 * x1 * x2 * y2_2 - r1_2 * x2_4 - 2 * r1_2 * x2_2 * y2_2 - r1_2 * y2_4 - 2 * r1 * r2_3 * x1_2 + 2 * r1 * r2 * x1_2 * x2_2 + 2 * r1 * r2 * x1_2 * y2_2 + r2_4 * x1_2 - r2_2 * x1_4 + 2 * r2_2 * x1_3 * x2 - 2 * r2_2 * x1_2 * x2_2 - 2 * r2_2 * x1_2 * y2_2 + x1_4 * x2_2 + x1_4 * y2_2 - 2 * x1_3 * x2_3 - 2 * x1_3 * x2 * y2_2 + x1_2 * x2_4 + 2 * x1_2 * x2_2 * y2_2 + x1_2 * y2_4)) / (2 * r1_2 * x2_2 + 2 * r1_2 * y2_2 - 4 * r1 * r2 * x1 * x2 + 2 * r2_2 * x1_2 - 2 * x1_2 * y2_2);
            } else
            {
                result.d_xCoords = (r1_3 * r2 * x2 - r1_2 * r2_2 * x1 - r1_2 * r2_2 * x2 + r1_2 * x1 * y2_2 + r1_2 * x2_3 + r1_2 * x2 * y2_2 + r1 * r2_3 * x1 - r1 * r2 * x1_2 * x2 - r1 * r2 * x1 * x2_2 - r1 * r2 * x1 * y2_2 + r2_2 * x1_3 - x1_3 * y2_2 - r1 * y2 * Math.Sqrt(-1 * r1_4 * r2_2 + r1_4 * x2_2 + r1_4 * y2_2 + 2 * r1_3 * r2_3 - 2 * r1_3 * r2 * x2_2 - 2 * r1_3 * r2 * y2_2 - r1_2 * r2_4 + 2 * r1_2 * r2_2 * x1_2 - 2 * r1_2 * r2_2 * x1 * x2 + 2 * r1_2 * r2_2 * x2_2 + 2 * r1_2 * r2_2 * y2_2 - 2 * r1_2 * x1_2 * x2_2 - 2 * r1_2 * x1_2 * y2_2 + 2 * r1_2 * x1 * x2_3 + 2 * r1_2 * x1 * x2 * y2_2 - r1_2 * x2_4 - 2 * r1_2 * x2_2 * y2_2 - r1_2 * y2_4 - 2 * r1 * r2_3 * x1_2 + 2 * r1 * r2 * x1_2 * x2_2 + 2 * r1 * r2 * x1_2 * y2_2 + r2_4 * x1_2 - r2_2 * x1_4 + 2 * r2_2 * x1_3 * x2 - 2 * r2_2 * x1_2 * x2_2 - 2 * r2_2 * x1_2 * y2_2 + x1_4 * x2_2 + x1_4 * y2_2 - 2 * x1_3 * x2_3 - 2 * x1_3 * x2 * y2_2 + x1_2 * x2_4 + 2 * x1_2 * x2_2 * y2_2 + x1_2 * y2_4)) / (2 * r1_2 * x2_2 + 2 * r1_2 * y2_2 - 4 * r1 * r2 * x1 * x2 + 2 * r2_2 * x1_2 - 2 * x1_2 * y2_2);
                result.d_yCoords = (r1_3 * r2 * y2 - r1_2 * r2_2 * y2 - r1_2 * x1 * x2 * y2 + r1_2 * x2_2 * y2 + r1_2 * y2_3 - r1 * r2 * x1_2 * y2 + r2_2 * x1_2 * y2 + x1_3 * x2 * y2 - x1_2 * x2_2 * y2 - x1_2 * y2_3 + (r1 * x2 - r2 * x1) * Math.Sqrt(-1 * r1_4 * r2_2 + r1_4 * x2_2 + r1_4 * y2_2 + 2 * r1_3 * r2_3 - 2 * r1_3 * r2 * x2_2 - 2 * r1_3 * r2 * y2_2 - r1_2 * r2_4 + 2 * r1_2 * r2_2 * x1_2 - 2 * r1_2 * r2_2 * x1 * x2 + 2 * r1_2 * r2_2 * x2_2 + 2 * r1_2 * r2_2 * y2_2 - 2 * r1_2 * x1_2 * x2_2 - 2 * r1_2 * x1_2 * y2_2 + 2 * r1_2 * x1 * x2_3 + 2 * r1_2 * x1 * x2 * y2_2 - r1_2 * x2_4 - 2 * r1_2 * x2_2 * y2_2 - r1_2 * y2_4 - 2 * r1 * r2_3 * x1_2 + 2 * r1 * r2 * x1_2 * x2_2 + 2 * r1 * r2 * x1_2 * y2_2 + r2_4 * x1_2 - r2_2 * x1_4 + 2 * r2_2 * x1_3 * x2 - 2 * r2_2 * x1_2 * x2_2 - 2 * r2_2 * x1_2 * y2_2 + x1_4 * x2_2 + x1_4 * y2_2 - 2 * x1_3 * x2_3 - 2 * x1_3 * x2 * y2_2 + x1_2 * x2_4 + 2 * x1_2 * x2_2 * y2_2 + x1_2 * y2_4)) / (2 * r1_2 * x2_2 + 2 * r1_2 * y2_2 - 4 * r1 * r2 * x1 * x2 + 2 * r2_2 * x1_2 - 2 * x1_2 * y2_2);
            }

            richTextBox1.Invoke((MethodInvoker)delegate {
                richTextBox1.AppendText("r: " + result.d_rValue.ToString() + "cm\r\n");
                richTextBox1.AppendText("x: " + result.d_xCoords.ToString() + "cm\r\n");
                richTextBox1.AppendText("y: " + result.d_yCoords.ToString() + "cm\r\n");
                richTextBox1.ScrollToCaret();
            });


            DateTime currentTime = DateTime.Now;
            toolStripStatusLabel1.Text = "Letzte Position von: " + currentTime;

            pictureBox1.CreateGraphics();

            return result;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            PictureBox test = sender as PictureBox;
            Bitmap bitmap = new Bitmap(1050, 1050);
            Graphics g = Graphics.FromImage(bitmap);
            g.PageUnit = GraphicsUnit.Pixel;
            Pen blackPen = new Pen(Color.Black, 2);
            SolidBrush pointBrush = new SolidBrush(Color.Black);

            Pen arrowPen = new Pen(Color.Black, 2);
            GraphicsPath capPath = new GraphicsPath();

            // Create font and brush.
            Font drawFont = new Font("Arial", 25);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            SolidBrush resultBrush = new SolidBrush(Color.Red);

            // Set format of string.
            StringFormat drawFormat = new StringFormat();

            // A triangle
            capPath.AddLine(-5, 0, 5, 0);
            capPath.AddLine(-5, 0, 0, 5);
            capPath.AddLine(0, 5, 5, 0);
            arrowPen.CustomEndCap = new CustomLineCap(null, capPath);

            g.DrawLine(arrowPen, 25, 25, 25, 1025);
            g.DrawLine(arrowPen, 25, 25, 1025, 25);

            g.FillEllipse(pointBrush, (float)Microphone_1.d_xCoords * 10 - 6 + 25, (float)Microphone_1.d_yCoords * 10 - 6 + 25, 12, 12);

            g.FillEllipse(pointBrush, (float)Microphone_2.d_xCoords * 10 - 6 + 25, (float)Microphone_2.d_yCoords * 10 - 6 + 25, 12, 12);
            g.FillEllipse(pointBrush, (float)Microphone_3.d_xCoords * 10 - 6 + 25, (float)Microphone_3.d_yCoords * 10 - 6 + 25, 12, 12);

            if(resultPosition.d_xCoords > 0  && resultPosition.d_yCoords > 0)
            {
                g.FillEllipse(resultBrush, (float)resultPosition.d_xCoords * 10 - 6 + 25, (float)resultPosition.d_yCoords * 10 - 6 + 25, 12, 12);
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                g.DrawString("(" + Math.Round(resultPosition.d_xCoords, 2).ToString() + "|" + Math.Round(resultPosition.d_yCoords, 2).ToString() + ")", drawFont, resultBrush, (float)resultPosition.d_xCoords * 10 + 12 + 25, 1025 - ((float)resultPosition.d_yCoords * 10 + 12 + 25), drawFormat);
            } else
            {
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }

            g.DrawString("M1", drawFont, drawBrush, (float)Microphone_1.d_xCoords * 10 + 12 + 25, 1025 - ((float)Microphone_1.d_yCoords * 10 + 12 + 25), drawFormat);
            g.DrawString("M2", drawFont, drawBrush, (float)Microphone_2.d_xCoords * 10 + 12 + 25, 1025 - ((float)Microphone_2.d_yCoords * 10 + 12 + 25), drawFormat);
            g.DrawString("M3", drawFont, drawBrush, (float)Microphone_3.d_xCoords * 10 + 12 + 25, 1025 - ((float)Microphone_3.d_yCoords * 10 + 12 + 25), drawFormat);

            e.Graphics.DrawImage(bitmap, 0, 0, test.Size.Width, test.Size.Height);
        }

        private void b_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                if(b_Connect.Text == "Verbinden")
                {
                    //sP_SerialCOM.PortName = cB_COMPort.Text;
                    //sP_SerialCOM.Open();
                    b_Connect.Text = "Trennen";
                    cB_COMPort.Enabled = false;
                    bW_ReadData.RunWorkerAsync();
                } else
                {
                    b_Connect.Text = "Verbinden";
                    cB_COMPort.Enabled = true;
                    //sP_SerialCOM.Close();
                    bW_ReadData.CancelAsync();
                }
            } catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void bW_ReadData_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true && bW_ReadData.CancellationPending == false)
            {
                if(newData == true)
                {
                    Dictionary<string, string> dataDict = new Dictionary<string, string>();
                    dataDict.Clear();
                    string[] informations = data.Split(';');
                    newData = false;
                    foreach (string item in informations)
                    {
                        if (item != "")
                        {
                            dataDict.Add(item.Split(':')[0], item.Split(':')[1]);
                        }
                    }
                    for (uint ui_cnt = 1; ui_cnt < 4; ui_cnt++)
                    {
                        var value = 0.0;
                        var numAlpha = new Regex("(?<Numeric>[0-9]*)(?<Alpha>[a-zA-Z]*)");
                        var match = numAlpha.Match(dataDict[Convert.ToString(ui_cnt)]);
                        var unit = match.Groups["Alpha"].Value;
                        switch (unit)
                        {
                            case "ns":
                                value = Convert.ToDouble(match.Groups["Numeric"].Value) / 1000000;
                                break;
                            case "us":
                                value = Convert.ToDouble(match.Groups["Numeric"].Value) / 1000;
                                break;
                            case "s":
                                value = Convert.ToDouble(match.Groups["Numeric"].Value) * 1000;
                                break;
                            default:
                                value = Convert.ToDouble(match.Groups["Numeric"].Value);
                                break;
                        }
                        switch (ui_cnt)
                        {
                            case 1:
                                Microphone_1.d_time = value;
                                break;
                            case 2:
                                Microphone_2.d_time = value;
                                break;
                            case 3:
                                Microphone_3.d_time = value;
                                break;
                            default:
                                break;
                        }
                    }
                    resultPosition = calculateSoundCoords(Microphone_1, Microphone_2, Microphone_3);
                    pictureBox1.Invoke((MethodInvoker)delegate {
                        pictureBox1.Refresh();
                    });
                }
            }
        }

        private void sP_SerialCOM_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if(newData == false)
            {
                data = sP_SerialCOM.ReadLine();
                newData = true;
            }           
        }
    }
}
