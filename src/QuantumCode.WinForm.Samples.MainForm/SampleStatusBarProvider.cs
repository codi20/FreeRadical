using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuantumCode.WinForm.Plugin;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel.Composition;
using QuantumCode.WinForm.Plugin.Context;
using QuantumCode.ComponentModel;

namespace QuantumCode.WinForm.Samples.MainForm
{
    [StatusBarContract("SampleStatusBar")]
    public class SampleStatusBarProvider : IStatusBar
    {
        private System.Windows.Forms.StatusStrip m_StatusBar;

        private Thread m_updateDateTimeThread;

        private ToolStripStatusLabel m_DateTimePanel;

        private ToolStripStatusLabel m_DefaultPanel;

        private delegate void UpdateDateTimePanelHandler(string dateTimeStrign);

        private UpdateDateTimePanelHandler m_OnUpdateTimePanel;

        public SampleStatusBarProvider()
        {
            m_OnUpdateTimePanel = UpdateDateTimePanel;
        }

        void m_StatusBar_HandleCreated(object sender, EventArgs e)
        {
            m_updateDateTimeThread = new Thread(UpdateDateTime);
            m_updateDateTimeThread.Start();
        }

        private void UpdateDateTime()
        {
            while (true)
            {
                m_StatusBar.Invoke(m_OnUpdateTimePanel, 
                    new object[]{DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒")});

                m_updateDateTimeThread.Join(1000);
            }
        }

        private void UpdateDateTimePanel(string dateTimeString)
        {
            m_StatusBar.Items["DateTimePanel"].Text = dateTimeString;
        }

        #region IDisposable Members

        public void Dispose()
        {
            m_StatusBar.Dispose();
        }

        #endregion

        #region IStatusBar Members

        public void OnStatusBarInitialize()
        {
            //初始化一个状态栏
            m_StatusBar = new StatusStrip();
            m_StatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;

            //初始化一个时间显示栏
            m_DateTimePanel = new ToolStripStatusLabel();

            m_DateTimePanel.DisplayStyle = ToolStripItemDisplayStyle.Text;
            m_DateTimePanel.Alignment = ToolStripItemAlignment.Right;
            m_DateTimePanel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            m_DateTimePanel.Spring = true;
            m_DateTimePanel.Name = "DateTimePanel";

            m_StatusBar.Items.Add(m_DateTimePanel);

            m_DefaultPanel = new ToolStripStatusLabel();

            m_DefaultPanel.DisplayStyle = ToolStripItemDisplayStyle.Text;
            m_DefaultPanel.Dock = DockStyle.Left;
            m_DefaultPanel.Name = WinFormContext.Current.DefaultStatusBarItem;

            m_StatusBar.Items.Insert(0, m_DefaultPanel);

            //关联创建事件
            m_StatusBar.HandleCreated += new EventHandler(m_StatusBar_HandleCreated);
        }

        public void OnStatusBarDestroy()
        {
            m_updateDateTimeThread.Abort();
            m_updateDateTimeThread = null;
        }

        public bool AddItem(string name)
        {
            bool retValue = false;
            bool matched = false;
            foreach (ToolStripItem item in m_StatusBar.Items)
            {
                if (item.Name == name)
                {
                    matched = true;
                    break;
                }
            }

            if (matched)
                retValue = true;
            else
            {
                ToolStripStatusLabel newItem = new ToolStripStatusLabel();
                newItem.Name = name;
                newItem.Dock = DockStyle.Left;
                m_StatusBar.Items.Insert(1, newItem);

                retValue = true;
            }

            return retValue;
        }

        public bool ShowMessage(string itemName, string msg)
        {
            bool retValue = false;

            foreach (ToolStripItem item in m_StatusBar.Items)
            {
                if (item.Name == itemName)
                {
                    item.Text = msg;
                    retValue = true;
                    break;
                }
            }

            return retValue;
        }

        public Control StatusBar
        {
            get { return m_StatusBar; }
        }

        #endregion
    }
}
