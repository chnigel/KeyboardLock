using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace KeyboardLock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll", EntryPoint = "GetKeyboardState")]

        public static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("user32.dll")]

        public static extern short GetAsyncKeyState(int key);

        [DllImport("user32.dll", EntryPoint = "GetKeyState")]

        public static extern short GetKeyState(int key);

        public static bool CapsLockStatus
        {
            get
            {
                byte[] bs = new byte[256];
                GetKeyboardState(bs);
                return (bs[0x14] == 1);
            }
        }

        public static bool NumLockStatus
        {
            get
            {
                byte[] bs = new byte[256];
                GetKeyboardState(bs);
                return (bs[0x90] == 1);
            }
        }

        bool caps = false;
        bool num = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (GetAsyncKeyState(20) == -32768)
            //{ caps = true; }
            //else { caps = false; }

            //if (GetAsyncKeyState(144) == -32768)
            //{ num = true; }
            //else { num = false; }

            short capsState = GetKeyState(20);
            short numState = GetKeyState(144);

            if (capsState == 1 || capsState==-127)
            { caps = true; }
            else { caps = false; }

            if (numState == 1 || capsState == -127)
            { num = true; }
            else { num = false; }


            if (caps)
            { Notify_Caps.Icon = new Icon("icons/CapsLock.ico"); }
            else
            { Notify_Caps.Icon = new Icon("icons/CapsUnlock.ico"); }


            if (num)
                Notify_Num.Icon = new Icon("icons/NumLock.ico");
            else
                Notify_Num.Icon = new Icon("icons/NumUnlock.ico");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.WindowState = FormWindowState.Minimized;
            if (e.CloseReason == CloseReason.UserClosing)
            {
                //是否取消close操作
                e.Cancel = true;
                this.Hide();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Minimized;
            //this.Hide();

            if (CapsLockStatus)
            { caps = true; }
            else
            { caps = false; }


            if (NumLockStatus)
            { num = true; }
            else
            { num = false; }

            //this.Hide();
        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
