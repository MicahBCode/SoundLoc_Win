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

        s_Microphone Microphone_1;
        s_Microphone Microphone_2;
        s_Microphone Microphone_3;

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

            Microphone_1.d_time = 1.0; // time in ms
            Microphone_2.d_time = 0;
            Microphone_3.d_time = 0.5;

            int i_v = 340; // 340 m/s

            richTextBox1.AppendText("Input Time microphone 1: " + Microphone_1.d_time.ToString() + "ms\r\n");
            richTextBox1.AppendText("Input Time microphone 2: " + Microphone_2.d_time.ToString() + "ms\r\n");
            richTextBox1.AppendText("Input Time microphone 3: " + Microphone_3.d_time.ToString() + "ms\r\n");

            richTextBox1.AppendText("\n");

            //richTextBox1.AppendText("Distance 1: " + Microphone_1.d_radius.ToString() + "cm\r\n");
            //richTextBox1.AppendText("Distance 2: " + Microphone_2.d_radius.ToString() + "cm\r\n");
            //richTextBox1.AppendText("Distance 3: " + Microphone_3.d_radius.ToString() + "cm\r\n");

            calculateSoundCoords(Microphone_1, Microphone_2, Microphone_3);
        }

        private void calculateSoundCoords(s_Microphone mic_1, s_Microphone mic_2, s_Microphone mic_3)
        {
            double x = 0;
            double y = 0;
            /*double r = (Math.Sqrt(-1 * (Math.Pow(mic_1.d_radius, 2) - 2 * mic_1.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 20000) * (Math.Pow(mic_1.d_radius, 2) - 2 * mic_1.d_radius * mic_2.d_radius + Math.Pow(mic_2.d_radius, 2) - 10000) * (Math.Pow(mic_2.d_radius, 2) - 2 * mic_2.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 10000)) * Math.Abs(mic_1.d_radius - mic_2.d_radius) - Math.Pow(mic_1.d_radius, 4) + 2 * Math.Pow(mic_1.d_radius, 3) * mic_2.d_radius + 10000 * Math.Pow(mic_1.d_radius, 2) - mic_1.d_radius * (3 * Math.Pow(mic_2.d_radius, 3) - Math.Pow(mic_2.d_radius, 2) * mic_3.d_radius - mic_2.d_radius * (Math.Pow(mic_3.d_radius, 2) - 10000) + mic_3.d_radius * (Math.Pow(mic_3.d_radius, 2) - 10000)) + mic_2.d_radius * (2 * Math.Pow(mic_2.d_radius, 3) - Math.Pow(mic_2.d_radius, 2) * mic_3.d_radius - mic_2.d_radius * Math.Pow(mic_3.d_radius, 3) + mic_3.d_radius * (Math.Pow(mic_3.d_radius, 2) - 10000))) / (2 * (mic_1.d_radius - mic_2.d_radius) * (Math.Pow(mic_1.d_radius, 2) - 2 * mic_1.d_radius * mic_2.d_radius + 2 * Math.Pow(mic_2.d_radius, 2) - 2 * mic_2.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 10000));
            if( r < 0)
            {
                r = -1 * (Math.Sqrt(-1 * (Math.Pow(mic_1.d_radius, 2) - 2 * mic_1.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 20000) * (Math.Pow(mic_1.d_radius, 2) - 2 * mic_1.d_radius * mic_2.d_radius + Math.Pow(mic_2.d_radius, 2) - 10000) * (Math.Pow(mic_2.d_radius, 2) - 2 * mic_2.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 10000)) * Math.Abs(mic_1.d_radius - mic_2.d_radius) + Math.Pow(mic_1.d_radius, 4) - 2 * Math.Pow(mic_1.d_radius, 3) * mic_2.d_radius - 10000 * Math.Pow(mic_1.d_radius, 2) + mic_1.d_radius * (3 * Math.Pow(mic_2.d_radius, 3) - Math.Pow(mic_2.d_radius, 2) * mic_3.d_radius - mic_2.d_radius * (Math.Pow(mic_3.d_radius, 2) - 10000) + mic_3.d_radius * (Math.Pow(mic_3.d_radius, 2) - 10000)) - mic_2.d_radius * (2 * Math.Pow(mic_2.d_radius, 3) - Math.Pow(mic_2.d_radius, 2) * mic_3.d_radius - mic_2.d_radius * Math.Pow(mic_3.d_radius, 2) + mic_3.d_radius * (Math.Pow(mic_3.d_radius, 2) - 10000))) / (2 * (mic_1.d_radius - mic_2.d_radius) * (Math.Pow(mic_1.d_radius, 2) - 2 * mic_1.d_radius * mic_2.d_radius + 2 * Math.Pow(mic_2.d_radius, 2) - 2 * mic_2.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 10000));

                x = -1 * (Math.Sqrt(-1 * (Math.Pow(mic_1.d_radius, 2) - 2 * mic_1.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 20000) * (Math.Pow(mic_1.d_radius, 2) - 2 * mic_1.d_radius * mic_2.d_radius + Math.Pow(mic_2.d_radius, 2) - 10000) * (Math.Pow(mic_2.d_radius, 2) - 2 * mic_2.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 10000)) * Math.Abs(mic_1.d_radius - mic_2.d_radius) * (mic_2.d_radius - mic_3.d_radius) + Math.Pow(mic_1.d_radius, 4) * (mic_2.d_radius - mic_3.d_radius) - Math.Pow(mic_1.d_radius, 3) * (3 * Math.Pow(mic_2.d_radius, 2) - 2 * mic_2.d_radius * mic_3.d_radius - Math.Pow(mic_3.d_radius, 2) + 10000) + Math.Pow(mic_1.d_radius, 2) * (3 * Math.Pow(mic_2.d_radius, 3) - mic_2.d_radius * (3 * Math.Pow(mic_3.d_radius, 2) - 20000) + 10000 * mic_3.d_radius) - mic_1.d_radius * (Math.Pow(mic_2.d_radius, 4) + 2 * Math.Pow(mic_2.d_radius, 3) * mic_3.d_radius - Math.Pow(mic_2.d_radius, 2) * (3 * Math.Pow(mic_3.d_radius, 2) - 20000) + 10000 * (Math.Pow(mic_3.d_radius, 2) - 10000)) + mic_2.d_radius * (Math.Pow(mic_2.d_radius, 3) * mic_3.d_radius - Math.Pow(mic_2.d_radius, 2) * (Math.Pow(mic_3.d_radius, 2) - 10000) - 10000 * mic_2.d_radius * mic_3.d_radius + 10000 * (Math.Pow(mic_3.d_radius, 2) - 10000)));
                x /= 200 * (mic_1.d_radius - mic_2.d_radius) * (Math.Pow(mic_1.d_radius, 2) - 2 * mic_1.d_radius * mic_2.d_radius + 2 * Math.Pow(mic_2.d_radius, 2) - 2 * mic_2.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 10000);

                y = Math.Sqrt(-1 * (Math.Pow(mic_1.d_radius, 2) - 2 * mic_1.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 20000) * (Math.Pow(mic_1.d_radius, 2) - 2 * mic_1.d_radius * mic_2.d_radius + Math.Pow(mic_2.d_radius, 2) - 10000) * (Math.Pow(mic_2.d_radius, 2) - 2 * mic_2.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 10000)) * Math.Abs(mic_1.d_radius - mic_2.d_radius) - (Math.Pow(mic_1.d_radius, 2) - mic_1.d_radius * (mic_2.d_radius + mic_3.d_radius) + mic_2.d_radius * mic_3.d_radius - 10000) * (Math.Pow(mic_2.d_radius, 2) - 2 * mic_2.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 10000);
                y /= 200 * (Math.Pow(mic_1.d_radius, 2) - 2 * mic_1.d_radius * mic_2.d_radius + 2 * Math.Pow(mic_2.d_radius, 2) - 2 * mic_2.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 10000);
            } else
            {
                x = (Math.Sqrt(-1 * (Math.Pow(mic_1.d_radius, 2) - 2 * mic_1.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 20000) * (Math.Pow(mic_1.d_radius, 2) - 2 * mic_1.d_radius * mic_2.d_radius + Math.Pow(mic_2.d_radius, 2) - 10000) * (Math.Pow(mic_2.d_radius, 2) - 2 * mic_2.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 10000)) * Math.Abs(mic_1.d_radius - mic_2.d_radius) * (mic_2.d_radius - mic_3.d_radius) - Math.Pow(mic_1.d_radius, 4) * (mic_2.d_radius - mic_3.d_radius) + Math.Pow(mic_1.d_radius, 3) * (3 * Math.Pow(mic_2.d_radius, 2) - 2 * mic_2.d_radius * mic_3.d_radius - Math.Pow(mic_3.d_radius, 2) + 10000) - Math.Pow(mic_1.d_radius, 2) * (3 * Math.Pow(mic_2.d_radius, 3) - mic_2.d_radius * (3 * Math.Pow(mic_3.d_radius, 2) - 20000) + 10000 * mic_3.d_radius) + mic_1.d_radius * (Math.Pow(mic_2.d_radius, 4) + 2 * Math.Pow(mic_2.d_radius, 3) * mic_3.d_radius - Math.Pow(mic_2.d_radius, 3) - 20000) + 10000 * (Math.Pow(mic_3.d_radius, 2) - 10000)) - mic_2.d_radius * (Math.Pow(mic_2.d_radius, 3) * mic_3.d_radius - Math.Pow(mic_2.d_radius, 2) * (Math.Pow(mic_3.d_radius, 2) - 10000) - 10000 * mic_2.d_radius * mic_3.d_radius + 10000 * (Math.Pow(mic_3.d_radius, 2) - 10000));
                x /= 200 * (mic_1.d_radius - mic_2.d_radius) * (Math.Pow(mic_1.d_radius, 2) - 2 * mic_1.d_radius * mic_2.d_radius + 2 * Math.Pow(mic_2.d_radius, 2) - 2 * mic_2.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 10000);

                y = -1 * (Math.Sqrt(-1 * (Math.Pow(mic_1.d_radius, 2) - 2 * mic_1.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 20000) * (Math.Pow(mic_1.d_radius, 2) - 2 * mic_1.d_radius * mic_2.d_radius + Math.Pow(mic_2.d_radius, 2) - 10000) * (Math.Pow(mic_2.d_radius, 2) - 2 * mic_2.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 10000)) * Math.Abs(mic_1.d_radius - mic_2.d_radius) + (Math.Pow(mic_1.d_radius, 2) - mic_1.d_radius * (mic_2.d_radius + mic_3.d_radius) + mic_2.d_radius * mic_3.d_radius - 10000) * (Math.Pow(mic_2.d_radius, 2) - 2 * mic_2.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 10000));
                y /= 200 * (Math.Pow(mic_1.d_radius, 2) - 2 * mic_1.d_radius + mic_2.d_radius + 2 * Math.Pow(mic_2.d_radius, 2) - 2 * mic_2.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 10000);
            }*/

            double x1 = mic_1.d_xCoords;
            double y1 = mic_1.d_yCoords;
            double r1 = (mic_2.d_time - mic_1.d_time) * 340 / 10;

            double x2 = mic_2.d_xCoords;
            double y2 = mic_2.d_yCoords;
            double r2 = (mic_3.d_time - mic_1.d_time) * 340 / 10;

            double x3 = mic_3.d_xCoords;
            double y3 = mic_3.d_yCoords;

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
            double y2_4 = Math.Pow(y2, 4);
            double r2_2 = r2 * r2;
            double r2_3 = Math.Pow(r2, 3);
            double r2_4 = Math.Pow(r2, 4);

            double r = (-1 * r1_3 * x2_2 - r1_3 * y2_2 + r1_2 * r2 * x1 * x2 + r1 * r2_2 * x1 * x2 + r1 * x1_2 * x2_2 + r1 * x1_2 * y2_2 - r1 * x1 * x2_3 - r1 * x1 * x2 * y2_2 - r2_3 * x1_2 - r2 * x1_3 * x2 + r2 * x1_2 * x2_2 + r2 * x1_2 * y2_2 + x1 * y2 * Math.Sqrt(-1 * r1_4 * r2_2 + r1_4 * x2_2 + r1_4 * y2_2 + 2 * r1_3 * r2_3 - 2 * r1_3 * r2 * x2_2 - 2 * r1_3 * r2 * y2_2 - r1_2 * r2_4 + 2 * r1_2 * r2_2 * x1_2 - 2 * r1_2 * r2_2 * x1 * x2 + 2 * r1_2 * r2_2 * x2_2 + 2 * r1_2 * r2_2 * y2_2 - 2 * r1_2 * x1_2 * x2_2 - 2 * r1_2 * x1_2 * y2_2 + 2 * r1_2 * x1 * x2_3 + 2 * r1_2 * x1 * x2 * y2_2 - r1_2 * x2_4 - 2 * r1_2 * x2_2 * y2_2 - r1_2 * y2_4 - 2 * r1 * r2_3 * x1_2 + 2 * r1 * r2 * x1_2 * x2_2 + 2 * r1 * r2 * x1_2 * y2_2 + r2_4 * x1_2 - r2_2 * x1_4 + 2 * r2_2 * x1_3 * x2 - 2 * r2_2 * x1_2 * x2_2 - 2 * r2_2 * x1_2 * y2_2 + x1_4 * x2_2 + x1_4 * y2_2 - 2 * x1_3 * x2_3 - 2 * x1_3 * x2 * y2_2 + x1_2 * x2_4 + 2 * x1_2 * x2_2 * y2_2 + x1_2 * y2_4)) / (2 * r1_2 * x2_2 + 2 * r1_2 * y2_2 - 4 * r1 * r2 * x1 * x2 + 2 * r2_2 * x1_2 - 2 * x1_2 * y2_2);
            if(r < 0)
            {
                r = (-1 * r1_3 * x2_2 - r1_3 * y2_2 + r1_2 * r2 * x1 * x2 + r1 * r2_2 * x1 * x2 + r1 * x1_2 * x2_2 + r1 * x1_2 * y2_2 - r1 * x1 * x2_3 - r1 * x1 * x2 * y2_2 - r2_3 * x1_2 - r2 * x1_3 * x2 + r2 * x1_2 * x2_2 + r2 * x1_2 * y2_2 - x1 * y2 * Math.Sqrt(-1 * r1_4 * r2_2 + r1_4 * x2_2 + r1_4 * y2_2 + 2 * r1_3 * r2_3 - 2 * r1_3 * r2 * x2_2 - 2 * r1_3 * r2 * y2_2 - r1_2 * r2_4 + 2 * r1_2 * r2_2 * x1_2 - 2 * r1_2 * r2_2 * x1 * x2 + 2 * r1_2 * r2_2 * x2_2 + 2 * r1_2 * r2_2 * y2_2 - 2 * r1_2 * x1_2 * x2_2 - 2 * r1_2 * x1_2 * y2_2 + 2 * r1_2 * x1 * x2_3 + 2 * r1_2 * x1 * x2 * y2_2 - r1_2 * x2_4 - 2 * r1_2 * x2_2 * y2_2 - r1_2 * y2_4 - 2 * r1 * r2_3 * x1_2 + 2 * r1 * r2 * x1_2 * x2_2 + 2 * r1 * r2 * x1_2 * y2_2 + r2_4 * x1_2 - r2_2 * x1_4 + 2 * r2_2 * x1_3 * x2 - 2 * r2_2 * x1_2 * x2_2 - 2 * r2_2 * x1_2 * y2_2 + x1_4 * x2_2 + x1_4 * y2_2 - 2 * x1_3 * x2_3 - 2 * x1_3 * x2 * y2_2 + x1_2 * x2_4 + 2 * x1_2 * x2_2 * y2_2 + x1_2 * y2_4)) / (2 * r1_2 * x2_2 + 2 * r1_2 * y2_2 - 4 * r1 * r2 * x1 * x2 + 2 * r2_2 * x1_2 - 2 * x1_2 * y2_2);
            } else
            {
                x = (r1_3 * r2 * x2 - r1_2 * r2_2 * x1 - r1_2 * r2_2 * x2 + r1_2 * x1 * y2_2 + r1_2 * x2_3 + r1_2 * x2 * y2_2 + r1 * r2_3 * x1 - r1 * r2 * x1_2 * x2 - r1 * r2 * x1 * x2_2 - r1 * r2 * x1 * y2_2 + r2_2 * x1_3 - x1_3 * y2_2 - r1 * y2 * Math.Sqrt(-1 * r1_4 * r2_2 + r1_4 * x2_2 + r1_4 * y2_2 + 2 * r1_3 * r2_3 - 2 * r1_3 * r2 * x2_2 - 2 * r1_3 * r2 * y2_2 - r1_2 * r2_4 + 2 * r1_2 * r2_2 * x1_2 - 2 * r1_2 * r2_2 * x1 * x2 + 2 * r1_2 * r2_2 * x2_2 + 2 * r1_2 * r2_2 * y2_2 - 2 * r1_2 * x1_2 * x2_2 - 2 * r1_2 * x1_2 * y2_2 + 2 * r1_2 * x1 * x2_3 + 2 * r1_2 * x1 * x2 * y2_2 - r1_2 * x2_4 - 2 * r1_2 * x2_2 * y2_2 - r1_2 * y2_4 - 2 * r1 * r2_3 * x1_2 + 2 * r1 * r2 * x1_2 * x2_2 + 2 * r1 * r2 * x1_2 * y2_2 + r2_4 * x1_2 - r2_2 * x1_4 + 2 * r2_2 * x1_3 * x2 - 2 * r2_2 * x1_2 * x2_2 - 2 * r2_2 * x1_2 * y2_2 + x1_4 * x2_2 + x1_4 * y2_2 - 2 * x1_3 * x2_3 - 2 * x1_3 * x2 * y2_2 + x1_2 * x2_4 + 2 * x1_2 * x2_2 * y2_2 + x1_2 * y2_4)) / (2 * r1_2 * x2_2 + 2 * r1_2 * y2_2 - 4 * r1 * r2 * x1 * x2 + 2 * r2_2 * x1_2 - 2 * x1_2 * y2_2);
            }
            
            richTextBox1.AppendText("Got r of: " + r.ToString() + "cm\r\n");
            richTextBox1.AppendText("x: " + x.ToString() + "\r\n");
            richTextBox1.AppendText("y: " + y.ToString() + "\r\n");


            DateTime currentTime = DateTime.Now;
            toolStripStatusLabel1.Text = "Letzte Position von: " + currentTime;
        }
    }
}
