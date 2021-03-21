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
        /// <summary>
        /// Struktur für die Daten jedes Mikrofons
        /// </summary>
        struct s_Microphone
        {
            public double d_xCoords;
            public double d_yCoords;

            public double d_time;
        }

        /// <summary>
        /// Struktur eines Kreises im Koordinatensystem
        /// </summary>
        struct s_Position
        {
            public double d_xCoords;
            public double d_yCoords;
            public double d_rValue;
        }

        // Deklaration der Variablen
        s_Microphone Microphone_1;
        s_Microphone Microphone_2;
        s_Microphone Microphone_3;

        s_Position resultPosition;

        // Initialisierung der Variablen
        bool getData = false;
        bool newData = false;
        string data = "1:47us;2:378us;3:0s;";

        /// <summary>
        /// Initialisierung des Windows-Fensters
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            Microphone_1.d_xCoords = 0;
            Microphone_1.d_yCoords = 0;

            Microphone_2.d_xCoords = Convert.ToDouble(tB_mic2_x.Text);
            Microphone_2.d_yCoords = 0;

            Microphone_3.d_xCoords = Convert.ToDouble(tB_mic3_x.Text);
            Microphone_3.d_yCoords = Convert.ToDouble(tB_mic3_y.Text);
        }

        /// <summary>
        /// Initialisierung der Komponenten nach dem Laden des Fensters
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            // Comports laden
            cB_COMPort.Items.Clear();
            foreach (string comport in SerialPort.GetPortNames())
            {
                cB_COMPort.Items.Add(comport);
            }

            // Automatisches verbinden zum gerät mit der PNPDeviceID von "USB\\VID_0FC8&PID_0001\\208335C0414E"
            ManagementObjectCollection ManObjReturn;
            ManagementObjectSearcher ManObjSearch;
            ManObjSearch = new ManagementObjectSearcher("SELECT * FROM Win32_SerialPort WHERE PNPDeviceID=\"USB\\\\VID_0FC8&PID_0001\\\\208335C0414E\"");
            ManObjReturn = ManObjSearch.Get();
            foreach (ManagementObject ManObj in ManObjReturn)
            {
                string description = ManObj["DeviceID"].ToString();
                cB_COMPort.Text = description;
                // Verbinden zum gefundenen Gerät
                b_Connect_Click(null, new EventArgs());
            }
        }

        /// <summary>
        /// Berechnen der Koordinaten der Geräuschquelle
        /// </summary>
        /// <param name="mic_1">Instanz der Struktur des Mikrofons 1</param>
        /// <param name="mic_2">Instanz der Struktur des Mikrofons 2</param>
        /// <param name="mic_3">Instanz der Struktur des Mikrofons 3</param>
        /// <returns>Struktur der berechneten Position</returns>
        private s_Position calculateSoundCoords(s_Microphone mic_1, s_Microphone mic_2, s_Microphone mic_3)
        {
            s_Position result;

            // Ausbreitungsgeschwindigkeit in Luft
            double d_v = 340.0; // 340 m/s

            // Initialisierung der Variablen zur übersichtlicheren Berechnung in den Thermen (unten)
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

            // Berechnen des 1. Radiuses
            result.d_rValue = (-1 * r1_3 * x2_2 - r1_3 * y2_2 + r1_2 * r2 * x1 * x2 + r1 * r2_2 * x1 * x2 + r1 * x1_2 * x2_2 + r1 * x1_2 * y2_2 - r1 * x1 * x2_3 - r1 * x1 * x2 * y2_2 - r2_3 * x1_2 - r2 * x1_3 * x2 + r2 * x1_2 * x2_2 + r2 * x1_2 * y2_2 + x1 * y2 * Math.Sqrt(-1 * r1_4 * r2_2 + r1_4 * x2_2 + r1_4 * y2_2 + 2 * r1_3 * r2_3 - 2 * r1_3 * r2 * x2_2 - 2 * r1_3 * r2 * y2_2 - r1_2 * r2_4 + 2 * r1_2 * r2_2 * x1_2 - 2 * r1_2 * r2_2 * x1 * x2 + 2 * r1_2 * r2_2 * x2_2 + 2 * r1_2 * r2_2 * y2_2 - 2 * r1_2 * x1_2 * x2_2 - 2 * r1_2 * x1_2 * y2_2 + 2 * r1_2 * x1 * x2_3 + 2 * r1_2 * x1 * x2 * y2_2 - r1_2 * x2_4 - 2 * r1_2 * x2_2 * y2_2 - r1_2 * y2_4 - 2 * r1 * r2_3 * x1_2 + 2 * r1 * r2 * x1_2 * x2_2 + 2 * r1 * r2 * x1_2 * y2_2 + r2_4 * x1_2 - r2_2 * x1_4 + 2 * r2_2 * x1_3 * x2 - 2 * r2_2 * x1_2 * x2_2 - 2 * r2_2 * x1_2 * y2_2 + x1_4 * x2_2 + x1_4 * y2_2 - 2 * x1_3 * x2_3 - 2 * x1_3 * x2 * y2_2 + x1_2 * x2_4 + 2 * x1_2 * x2_2 * y2_2 + x1_2 * y2_4)) / (2 * r1_2 * x2_2 + 2 * r1_2 * y2_2 - 4 * r1 * r2 * x1 * x2 + 2 * r2_2 * x1_2 - 2 * x1_2 * y2_2);
            if(result.d_rValue < 0)
            {
                // Berechnen des 2. Radiuses
                result.d_rValue  = (-1 * r1_3 * x2_2 - r1_3 * y2_2 + r1_2 * r2 * x1 * x2 + r1 * r2_2 * x1 * x2 + r1 * x1_2 * x2_2 + r1 * x1_2 * y2_2 - r1 * x1 * x2_3 - r1 * x1 * x2 * y2_2 - r2_3 * x1_2 - r2 * x1_3 * x2 + r2 * x1_2 * x2_2 + r2 * x1_2 * y2_2 - x1 * y2 * Math.Sqrt(-1 * r1_4 * r2_2 + r1_4 * x2_2 + r1_4 * y2_2 + 2 * r1_3 * r2_3 - 2 * r1_3 * r2 * x2_2 - 2 * r1_3 * r2 * y2_2 - r1_2 * r2_4 + 2 * r1_2 * r2_2 * x1_2 - 2 * r1_2 * r2_2 * x1 * x2 + 2 * r1_2 * r2_2 * x2_2 + 2 * r1_2 * r2_2 * y2_2 - 2 * r1_2 * x1_2 * x2_2 - 2 * r1_2 * x1_2 * y2_2 + 2 * r1_2 * x1 * x2_3 + 2 * r1_2 * x1 * x2 * y2_2 - r1_2 * x2_4 - 2 * r1_2 * x2_2 * y2_2 - r1_2 * y2_4 - 2 * r1 * r2_3 * x1_2 + 2 * r1 * r2 * x1_2 * x2_2 + 2 * r1 * r2 * x1_2 * y2_2 + r2_4 * x1_2 - r2_2 * x1_4 + 2 * r2_2 * x1_3 * x2 - 2 * r2_2 * x1_2 * x2_2 - 2 * r2_2 * x1_2 * y2_2 + x1_4 * x2_2 + x1_4 * y2_2 - 2 * x1_3 * x2_3 - 2 * x1_3 * x2 * y2_2 + x1_2 * x2_4 + 2 * x1_2 * x2_2 * y2_2 + x1_2 * y2_4)) / (2 * r1_2 * x2_2 + 2 * r1_2 * y2_2 - 4 * r1 * r2 * x1 * x2 + 2 * r2_2 * x1_2 - 2 * x1_2 * y2_2);
                // Berechnen der x- und y-Koordinate
                result.d_xCoords = (r1_3 * r2 * x2 - r1_2 * r2_2 * x1 - r1_2 * r2_2 * x2 + r1_2 * x1 * y2_2 + r1_2 * x2_3 + r1_2 * x2 * y2_2 + r1 * r2_3 * x1 - r1 * r2 * x1_2 * x2 - r1 * r2 * x1 * x2_2 - r1 * r2 * x1 * y2_2 + r2_2 * x1_3 - x1_3 * y2_2 + r1 * y2 * Math.Sqrt(-1 * r1_4 * r2_2 + r1_4 * x2_2 + r1_4 * y2_2 + 2 * r1_3 * r2_3 - 2 * r1_3 * r2 * x2_2 - 2 * r1_3 * r2 * y2_2 - r1_2 * r2_4 + 2 * r1_2 * r2_2 * x1_2 - 2 * r1_2 * r2_2 * x1 * x2 + 2 * r1_2 * r2_2 * x2_2 + 2 * r1_2 * r2_2 * y2_2 - 2 * r1_2 * x1_2 * x2_2 - 2 * r1_2 * x1_2 * y2_2 + 2 * r1_2 * x1 * x2_3 + 2 * r1_2 * x1 * x2 * y2_2 - r1_2 * x2_4 - 2 * r1_2 * x2_2 * y2_2 - r1_2 * y2_4 - 2 * r1 * r2_3 * x1_2 + 2 * r1 * r2 * x1_2 * x2_2 + 2 * r1 * r2 * x1_2 * y2_2 + r2_4 * x1_2 - r2_2 * x1_4 + 2 * r2_2 * x1_3 * x2 - 2 * r2_2 * x1_2 * x2_2 - 2 * r2_2 * x1_2 * y2_2 + x1_4 * x2_2 + x1_4 * y2_2 - 2 * x1_3 * x2_3 - 2 * x1_3 * x2 * y2_2 + x1_2 * x2_4 + 2 * x1_2 * x2_2 * y2_2 + x1_2 * y2_4)) / (2 * r1_2 * x2_2 + 2 * r1_2 * y2_2 - 4 * r1 * r2 * x1 * x2 + 2 * r2_2 * x1_2 - 2 * x1_2  * y2_2);
                result.d_yCoords = (r1_3 * r2 * y2 - r1_2 * r2_2 * y2 - r1_2 * x1 * x2 * y2 + r1_2 * x2_2 * y2 + r1_2 * y2_3 - r1 * r2 * x1_2 * y2 + r2_2 * x1_2 * y2 + x1_3 * x2 * y2 - x1_2 * x2_2 * y2 - x1_2 * y2_3 + (-1 * r1 * x2 + r2 * x1) * Math.Sqrt(-1 * r1_4 * r2_2 + r1_4 * x2_2 + r1_4 * y2_2 + 2 * r1_3 * r2_3 - 2 * r1_3 * r2 * x2_2 - 2 * r1_3 * r2 * y2_2 - r1_2 * r2_4 + 2 * r1_2 * r2_2 * x1_2 - 2 * r1_2 * r2_2 * x1 * x2 + 2 * r1_2 * r2_2 * x2_2 + 2 * r1_2 * r2_2 * y2_2 - 2 * r1_2 * x1_2 * x2_2 - 2 * r1_2 * x1_2 * y2_2 + 2 * r1_2 * x1 * x2_3 + 2 * r1_2 * x1 * x2 * y2_2 - r1_2 * x2_4 - 2 * r1_2 * x2_2 * y2_2 - r1_2 * y2_4 - 2 * r1 * r2_3 * x1_2 + 2 * r1 * r2 * x1_2 * x2_2 + 2 * r1 * r2 * x1_2 * y2_2 + r2_4 * x1_2 - r2_2 * x1_4 + 2 * r2_2 * x1_3 * x2 - 2 * r2_2 * x1_2 * x2_2 - 2 * r2_2 * x1_2 * y2_2 + x1_4 * x2_2 + x1_4 * y2_2 - 2 * x1_3 * x2_3 - 2 * x1_3 * x2 * y2_2 + x1_2 * x2_4 + 2 * x1_2 * x2_2 * y2_2 + x1_2 * y2_4)) / (2 * r1_2 * x2_2 + 2 * r1_2 * y2_2 - 4 * r1 * r2 * x1 * x2 + 2 * r2_2 * x1_2 - 2 * x1_2 * y2_2);
            } else
            {
                // Berechnen der x- und y-Koordinate
                result.d_xCoords = (r1_3 * r2 * x2 - r1_2 * r2_2 * x1 - r1_2 * r2_2 * x2 + r1_2 * x1 * y2_2 + r1_2 * x2_3 + r1_2 * x2 * y2_2 + r1 * r2_3 * x1 - r1 * r2 * x1_2 * x2 - r1 * r2 * x1 * x2_2 - r1 * r2 * x1 * y2_2 + r2_2 * x1_3 - x1_3 * y2_2 - r1 * y2 * Math.Sqrt(-1 * r1_4 * r2_2 + r1_4 * x2_2 + r1_4 * y2_2 + 2 * r1_3 * r2_3 - 2 * r1_3 * r2 * x2_2 - 2 * r1_3 * r2 * y2_2 - r1_2 * r2_4 + 2 * r1_2 * r2_2 * x1_2 - 2 * r1_2 * r2_2 * x1 * x2 + 2 * r1_2 * r2_2 * x2_2 + 2 * r1_2 * r2_2 * y2_2 - 2 * r1_2 * x1_2 * x2_2 - 2 * r1_2 * x1_2 * y2_2 + 2 * r1_2 * x1 * x2_3 + 2 * r1_2 * x1 * x2 * y2_2 - r1_2 * x2_4 - 2 * r1_2 * x2_2 * y2_2 - r1_2 * y2_4 - 2 * r1 * r2_3 * x1_2 + 2 * r1 * r2 * x1_2 * x2_2 + 2 * r1 * r2 * x1_2 * y2_2 + r2_4 * x1_2 - r2_2 * x1_4 + 2 * r2_2 * x1_3 * x2 - 2 * r2_2 * x1_2 * x2_2 - 2 * r2_2 * x1_2 * y2_2 + x1_4 * x2_2 + x1_4 * y2_2 - 2 * x1_3 * x2_3 - 2 * x1_3 * x2 * y2_2 + x1_2 * x2_4 + 2 * x1_2 * x2_2 * y2_2 + x1_2 * y2_4)) / (2 * r1_2 * x2_2 + 2 * r1_2 * y2_2 - 4 * r1 * r2 * x1 * x2 + 2 * r2_2 * x1_2 - 2 * x1_2 * y2_2);
                result.d_yCoords = (r1_3 * r2 * y2 - r1_2 * r2_2 * y2 - r1_2 * x1 * x2 * y2 + r1_2 * x2_2 * y2 + r1_2 * y2_3 - r1 * r2 * x1_2 * y2 + r2_2 * x1_2 * y2 + x1_3 * x2 * y2 - x1_2 * x2_2 * y2 - x1_2 * y2_3 + (r1 * x2 - r2 * x1) * Math.Sqrt(-1 * r1_4 * r2_2 + r1_4 * x2_2 + r1_4 * y2_2 + 2 * r1_3 * r2_3 - 2 * r1_3 * r2 * x2_2 - 2 * r1_3 * r2 * y2_2 - r1_2 * r2_4 + 2 * r1_2 * r2_2 * x1_2 - 2 * r1_2 * r2_2 * x1 * x2 + 2 * r1_2 * r2_2 * x2_2 + 2 * r1_2 * r2_2 * y2_2 - 2 * r1_2 * x1_2 * x2_2 - 2 * r1_2 * x1_2 * y2_2 + 2 * r1_2 * x1 * x2_3 + 2 * r1_2 * x1 * x2 * y2_2 - r1_2 * x2_4 - 2 * r1_2 * x2_2 * y2_2 - r1_2 * y2_4 - 2 * r1 * r2_3 * x1_2 + 2 * r1 * r2 * x1_2 * x2_2 + 2 * r1 * r2 * x1_2 * y2_2 + r2_4 * x1_2 - r2_2 * x1_4 + 2 * r2_2 * x1_3 * x2 - 2 * r2_2 * x1_2 * x2_2 - 2 * r2_2 * x1_2 * y2_2 + x1_4 * x2_2 + x1_4 * y2_2 - 2 * x1_3 * x2_3 - 2 * x1_3 * x2 * y2_2 + x1_2 * x2_4 + 2 * x1_2 * x2_2 * y2_2 + x1_2 * y2_4)) / (2 * r1_2 * x2_2 + 2 * r1_2 * y2_2 - 4 * r1 * r2 * x1 * x2 + 2 * r2_2 * x1_2 - 2 * x1_2 * y2_2);
            }

            // Ausgabe als Text
            richTextBox1.Invoke((MethodInvoker)delegate {
                richTextBox1.AppendText("r: " + result.d_rValue.ToString() + "cm\r\n");
                richTextBox1.AppendText("x: " + result.d_xCoords.ToString() + "cm\r\n");
                richTextBox1.AppendText("y: " + result.d_yCoords.ToString() + "cm\r\n");
                richTextBox1.ScrollToCaret();
            });

            // Ausgabe des Zeitpunktes der letzten Berechnung
            DateTime currentTime = DateTime.Now;
            toolStripStatusLabel1.Text = "Letzte Position von: " + currentTime;

            // Visuelle Darstellung initialisieren
            pictureBox1.CreateGraphics();

            // berechnete Position ausgeben
            return result;
        }

        /// <summary>
        /// Zeichnet die Visuelle Darstellung
        /// </summary>
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            PictureBox test = sender as PictureBox;

            // Bitmap mit der Auflösung 1050px X 1050px erstellen
            Bitmap bitmap = new Bitmap(1050, 1050);
            Graphics g = Graphics.FromImage(bitmap);
            g.PageUnit = GraphicsUnit.Pixel;
            Pen blackPen = new Pen(Color.Black, 2);
            SolidBrush pointBrush = new SolidBrush(Color.Black);

            Pen arrowPen = new Pen(Color.Black, 2);
            GraphicsPath capPath = new GraphicsPath();

            // Schrift erstellen
            Font drawFont = new Font("Arial", 25);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            SolidBrush resultBrush = new SolidBrush(Color.Red);

            // String Format setzen
            StringFormat drawFormat = new StringFormat();

            // Pfeil
            capPath.AddLine(-5, 0, 5, 0);
            capPath.AddLine(-5, 0, 0, 5);
            capPath.AddLine(0, 5, 5, 0);
            arrowPen.CustomEndCap = new CustomLineCap(null, capPath);

            // Achsen des Koordinatensystems zeichnen
            g.DrawLine(arrowPen, 25, 25, 25, 1025);
            g.DrawLine(arrowPen, 25, 25, 1025, 25);

            // Punkte der Mikrofone zeichnen
            g.FillEllipse(pointBrush, (float)Microphone_1.d_xCoords * 10 - 6 + 25, (float)Microphone_1.d_yCoords * 10 - 6 + 25, 12, 12);
            g.FillEllipse(pointBrush, (float)Microphone_2.d_xCoords * 10 - 6 + 25, (float)Microphone_2.d_yCoords * 10 - 6 + 25, 12, 12);
            g.FillEllipse(pointBrush, (float)Microphone_3.d_xCoords * 10 - 6 + 25, (float)Microphone_3.d_yCoords * 10 - 6 + 25, 12, 12);

            // Zeichnen der errechneten Position
            if(resultPosition.d_xCoords > 0  && resultPosition.d_yCoords > 0)
            {
                g.FillEllipse(resultBrush, (float)resultPosition.d_xCoords * 10 - 6 + 25, (float)resultPosition.d_yCoords * 10 - 6 + 25, 12, 12);
                // Bild spiegeln für 0-Punkt in der linken unteren Ecke
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                // Anzeige der Koordinaten
                g.DrawString("(" + Math.Round(resultPosition.d_xCoords, 2).ToString() + "|" + Math.Round(resultPosition.d_yCoords, 2).ToString() + ")", drawFont, resultBrush, (float)resultPosition.d_xCoords * 10 + 12 + 25, 1025 - ((float)resultPosition.d_yCoords * 10 + 12 + 25), drawFormat);
            } else
            {
                // Bild spiegeln für 0-Punkt in der linken unteren Ecke
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }

            // Beschriftung Mikrofone 1 bis 3
            g.DrawString("M1", drawFont, drawBrush, (float)Microphone_1.d_xCoords * 10 + 12 + 25, 1025 - ((float)Microphone_1.d_yCoords * 10 + 12 + 25), drawFormat);
            g.DrawString("M2", drawFont, drawBrush, (float)Microphone_2.d_xCoords * 10 + 12 + 25, 1025 - ((float)Microphone_2.d_yCoords * 10 + 12 + 25), drawFormat);
            g.DrawString("M3", drawFont, drawBrush, (float)Microphone_3.d_xCoords * 10 + 12 + 25, 1025 - ((float)Microphone_3.d_yCoords * 10 + 12 + 25), drawFormat);

            // Bild ausgeben
            e.Graphics.DrawImage(bitmap, 0, 0, test.Size.Width, test.Size.Height);
        }

        /// <summary>
        /// Verbinden-Button wurde betätigt
        /// </summary>
        private void b_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                if(b_Connect.Text == "Verbinden")
                {
                    // Verbinden
                    sP_SerialCOM.PortName = cB_COMPort.Text;
                    sP_SerialCOM.Open();
                    b_Connect.Text = "Trennen";
                    cB_COMPort.Enabled = false;
                    bW_ReadData.RunWorkerAsync();
                } else
                {
                    // Trennen
                    b_Connect.Text = "Verbinden";
                    cB_COMPort.Enabled = true;
                    sP_SerialCOM.Close();
                    bW_ReadData.CancelAsync();
                }
            } catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        /// <summary>
        /// Hintergrundprozess zur Verarbeitung der empfangenen Daten
        /// </summary>
        private void bW_ReadData_DoWork(object sender, DoWorkEventArgs e)
        {
            // Solange Prozess nicht gestoppt
            while(true && bW_ReadData.CancellationPending == false)
            {
                // Neue Daten?
                if(newData == true)
                {
                    Dictionary<string, string> dataDict = new Dictionary<string, string>();
                    dataDict.Clear();

                    // Daten parsen
                    string[] informations = data.Split(';');
                    newData = false;
                    foreach (string item in informations)
                    {
                        if (item != "")
                        {
                            // Daten in Dictionary hinzufügen
                            dataDict.Add(item.Split(':')[0], item.Split(':')[1]);
                        }
                    }

                    // Für jeden Wert umrechnen in ms
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

                    // Position berechnen
                    resultPosition = calculateSoundCoords(Microphone_1, Microphone_2, Microphone_3);

                    // visuelle Darstellung aktualisieren
                    pictureBox1.Invoke((MethodInvoker)delegate {
                        pictureBox1.Refresh();
                    });
                }
            }
        }

        /// <summary>
        /// Daten wurdem empfangen
        /// </summary>
        private void sP_SerialCOM_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if(newData == false)
            {
                data = sP_SerialCOM.ReadLine();
                newData = true;
                richTextBox1.Invoke((MethodInvoker)delegate {
                    richTextBox1.AppendText(data + "\r\n");
                    richTextBox1.ScrollToCaret();
                });
            }           
        }

        /// <summary>
        /// Validieren, ob die Eingabe ins Koordinaten-Feld erlaubt ist
        /// </summary>
        private void micCoordsFocusLeft(object sender, EventArgs e)
        {
            TextBox tB_Changed = sender as TextBox;
            if (tB_Changed.Text != "")
            {
                try
                {
                    double d_2_xCoords = Convert.ToDouble(tB_mic2_x.Text);
                    double d_3_xCoords = Convert.ToDouble(tB_mic3_x.Text);
                    double d_3_yCoords = Convert.ToDouble(tB_mic3_y.Text);
                    Microphone_2.d_xCoords = d_2_xCoords;
                    Microphone_3.d_xCoords = d_3_xCoords;
                    Microphone_3.d_yCoords = d_3_yCoords;
                    pictureBox1.Refresh();
                }
                catch
                {
                    tB_mic2_x.Text = Microphone_2.d_xCoords.ToString();
                    tB_mic3_x.Text = Microphone_3.d_xCoords.ToString();
                    tB_mic3_y.Text = Microphone_3.d_yCoords.ToString();
                }
            }
        }
    }
}
