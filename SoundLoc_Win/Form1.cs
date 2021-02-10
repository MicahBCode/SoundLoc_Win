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

namespace SoundLoc_Win
{
    public partial class Form1 : Form
    {
        struct s_Microphone
        {
            public double d_xCoords;
            public double d_yCoords;

            public double d_time;
            public double d_radius;
        }

        s_Microphone Microphone_1;
        s_Microphone Microphone_2;
        s_Microphone Microphone_3;

        public Form1()
        {
            InitializeComponent();

            Microphone_1.d_xCoords = 0;
            Microphone_1.d_yCoords = 100;

            Microphone_2.d_xCoords = 0;
            Microphone_2.d_yCoords = 0;

            Microphone_3.d_xCoords = 100;
            Microphone_3.d_yCoords = 0;
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

            Microphone_1.d_radius = i_v * ( Microphone_1.d_time / 10 );
            Microphone_2.d_radius = i_v * ( Microphone_2.d_time / 10 );
            Microphone_3.d_radius = i_v * ( Microphone_3.d_time / 10 );

            richTextBox1.AppendText("\n");

            richTextBox1.AppendText("Distance 1: " + Microphone_1.d_radius.ToString() + "cm\r\n");
            richTextBox1.AppendText("Distance 2: " + Microphone_2.d_radius.ToString() + "cm\r\n");
            richTextBox1.AppendText("Distance 3: " + Microphone_3.d_radius.ToString() + "cm\r\n");

            calculateSoundCoords(Microphone_1, Microphone_2, Microphone_3);
        }

        private void calculateSoundCoords(s_Microphone mic_1, s_Microphone mic_2, s_Microphone mic_3)
        {
            double x;
            double y;
            double r = (Math.Sqrt(-1 * (Math.Pow(mic_1.d_radius, 2) - 2 * mic_1.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 20000) * (Math.Pow(mic_1.d_radius, 2) - 2 * mic_1.d_radius * mic_2.d_radius + Math.Pow(mic_2.d_radius, 2) - 10000) * (Math.Pow(mic_2.d_radius, 2) - 2 * mic_2.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 10000)) * Math.Abs(mic_1.d_radius - mic_2.d_radius) - Math.Pow(mic_1.d_radius, 4) + 2 * Math.Pow(mic_1.d_radius, 3) * mic_2.d_radius + 10000 * Math.Pow(mic_1.d_radius, 2) - mic_1.d_radius * (3 * Math.Pow(mic_2.d_radius, 3) - Math.Pow(mic_2.d_radius, 2) * mic_3.d_radius - mic_2.d_radius * (Math.Pow(mic_3.d_radius, 2) - 10000) + mic_3.d_radius * (Math.Pow(mic_3.d_radius, 2) - 10000)) + mic_2.d_radius * (2 * Math.Pow(mic_2.d_radius, 3) - Math.Pow(mic_2.d_radius, 2) * mic_3.d_radius - mic_2.d_radius * Math.Pow(mic_3.d_radius, 3) + mic_3.d_radius * (Math.Pow(mic_3.d_radius, 2) - 10000))) / (2 * (mic_1.d_radius - mic_2.d_radius) * (Math.Pow(mic_1.d_radius, 2) - 2 * mic_1.d_radius * mic_2.d_radius + 2 * Math.Pow(mic_2.d_radius, 2) - 2 * mic_2.d_radius * mic_3.d_radius + Math.Pow(mic_3.d_radius, 2) - 10000));
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
            }
            richTextBox1.AppendText("Got r of: " + r.ToString() + "cm\r\n");
        }
    }
}
