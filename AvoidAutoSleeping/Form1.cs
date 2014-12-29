using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvoidAutoSleep
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        private static extern uint SetThreadExecutionState(uint esFlags);

        private const uint ES_SYSTEM_REQUIRED = 0x00000001;
        //const uint ES_DISPLAY_REQUIRED = 0x00000002;
        private const uint ES_CONTINUOUS = 0x80000000;
        private bool closingApplication = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void AvoidSleep(bool flag)
        {
            if (flag)
            {
                SetThreadExecutionState(ES_CONTINUOUS | ES_SYSTEM_REQUIRED);
            }
            else
            {
                SetThreadExecutionState(ES_CONTINUOUS);
            }
            avoidAutoSleepToolStripMenuItem.Enabled = !flag;
            autoSleepToolStripMenuItem.Enabled = flag;
            button1.Enabled = !flag;
            button2.Enabled = flag;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AvoidSleep(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AvoidSleep(false);
        }

        private void avoidAutoSleepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AvoidSleep(true);
        }

        private void autoSleepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AvoidSleep(false);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closingApplication = true;
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!closingApplication)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.Visible)
            {
                this.Hide();
            }
            else
            {
                this.Show();
            }
        }
    }
}
