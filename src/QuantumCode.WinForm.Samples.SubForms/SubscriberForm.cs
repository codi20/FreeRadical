using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuantumCode.ComponentModel;
using QuantumCode.WinForm.Plugin.Context;

namespace QuantumCode.WinForm.Samples.SubForms
{
    public partial class SubscriberForm : Form, ISubscriber
    {
        private string m_ID;

        private Action<string, string> m_DisposeAction;

        public SubscriberForm()
        {
            InitializeComponent();
        }

        private bool ReceiveMessage(object msg)
        {
            textBox1.Text = msg.ToString();
            return true;
        }

        private void SubscriberForm_Load(object sender, EventArgs e)
        {
            PlugContext.Current.RegistMessageSubscriber(this);
        }

        #region ISubscriber Members

        public string ID
        {
            get
            {
                return m_ID;
            }
            set
            {
                m_ID = value;
            }
        }

        public Func<object, bool> MessageReceiveAction
        {
            get { return ReceiveMessage; }
        }

        public string MessageType
        {
            get { return "NumberTest"; }
        }

        public void OnPublisherDeactive()
        {
            lblStatusInfo.Text = "消息发布者下线";
        }

        public void OnRegistFault(string msg)
        {
            lblSubscriberInfo.Text = "消息订阅失败：" + msg;
        }

        public void OnRegistSuccess()
        {
            lblSubscriberInfo.Text = "消息订阅成功";
        }

        public void RegistDisposeAction(Action<string, string> disposeAction)
        {
            m_DisposeAction = disposeAction;
        }

        #endregion

        private void SubscriberForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null != m_DisposeAction)
                m_DisposeAction.Invoke(MessageType, ID);
        }
    }
}
