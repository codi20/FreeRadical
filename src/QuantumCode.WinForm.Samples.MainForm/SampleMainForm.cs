using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuantumCode.WinForm.Plugin.Context;

namespace QuantumCode.WinForm.Samples.MainForm
{
    public partial class SampleMainForm : Form
    {
        public SampleMainForm()
        {
            InitializeComponent();
        }

        private void SampleMainForm_Load(object sender, EventArgs e)
        {
            if (PlugContext.Current.IsMessageManagerLoaded)
            {
                PlugContext.Current.MainFormStatusBar.ShowMessage(WinFormContext.Current.DefaultStatusBarItem, 
                    "消息管理器已经加载");
            }
            else
            {
                PlugContext.Current.MainFormStatusBar.ShowMessage(WinFormContext.Current.DefaultStatusBarItem, 
                    "消息管理器未能加载");
            }
        }
    }
}
