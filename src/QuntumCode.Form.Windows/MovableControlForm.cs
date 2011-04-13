using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace QuntumCode.Form.Windows
{
    /// <summary>
    /// 一个窗体，其中容纳的控件可随鼠标移动
    /// </summary>
    public partial class MovableControlForm : System.Windows.Forms.Form
    {
        private const int WM_NCHITTEST = 0x0084;
        private const int HTCLIENT = 1;
        private const int HTCAPTION = 2;

        /// <summary>
        /// 
        /// </summary>
        public const int WM_NCLBUTTONDOWN = 0xA1;
        /// <summary>
        /// 
        /// </summary>
        public const int HT_CAPTION = 0x2;

        /// <summary>
        /// 发送一个窗体消息
        /// </summary>
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        /// <summary>
        /// 释放控制
        /// </summary>
        /// <returns></returns>
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        /// <summary>
        /// 构造函数
        /// </summary>
        public MovableControlForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 添加一个可移动的控件
        /// </summary>
        /// <param name="theControl"></param>
        public void AddDraggingControl(System.Windows.Forms.Control theControl)
        {
            theControl.MouseMove += new System.Windows.Forms.MouseEventHandler(OnControlMouseMove);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    if (m.Result == (IntPtr)HTCLIENT)
                    {
                        m.Result = (IntPtr)HTCAPTION;
                    }
                    break;
            }
        }

        /// <summary>
        /// 当鼠标移动时
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (WindowState == FormWindowState.Normal)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
        }

        private void OnControlMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (WindowState == FormWindowState.Normal)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
        }
    }
}
