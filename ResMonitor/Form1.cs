using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResMonitor
{

    public partial class Form1 : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        PerformanceCounter cpuCounter = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
        PerformanceCounter ramCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
        PerformanceCounter d1Counter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "0 C: D: E:");
        PerformanceCounter d2Counter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "1");

        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            timer1.Enabled = true;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                int ram = (int)ramCounter.NextValue();
                int cpu = (int)cpuCounter.NextValue();
                int d1 = (int)d1Counter.NextValue();
                int d2 = (int)d2Counter.NextValue();



                progressBar1.Value = (int)cpu;
                progressBar2.Value = (int)ram;
                progressBar3.Value = (int)d1;
                progressBar4.Value = (int)d2;
            }
            catch (Exception)
            {
                ;
            }
            


        }
    }
}
