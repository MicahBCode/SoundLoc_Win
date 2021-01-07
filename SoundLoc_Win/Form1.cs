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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cB_COMPort.Items.Clear();
            foreach (string comport in SerialPort.GetPortNames())
            {
                cB_COMPort.Items.Add(comport);
            }
        }
    }
}
