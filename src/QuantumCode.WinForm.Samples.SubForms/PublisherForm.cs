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
    public partial class PublisherForm : Form, IPublisher
    {
        private string m_ID;

        private int m_SubscriberCount;

        private Action<string> m_OnDispose;

        private Func<string, object, int> m_OnPublishMessage;

        public PublisherForm()
        {
            InitializeComponent();

            m_SubscriberCount = 0;
        }

        #region IPublisher Members

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

        public string MessageType
        {
            get { return "NumberTest"; }
        }

        public void OnRegistFault(string msg)
        {
            lblInfo.Text = "注册发布者失败：" + msg;
        }

        public void OnRegistSuccess()
        {
            lblInfo.Text = "注册发布者成功";
        }

        public void OnSubscriberActive(ISubscriber subscriber)
        {
            m_SubscriberCount += 1;
            lblSubscriberInfo.Text = "当前有：" + m_SubscriberCount.ToString() + "个订阅者";
            subscriber.MessageReceiveAction.Invoke(textBox1.Text);
        }

        public void OnSubscriberDeactive()
        {
            m_SubscriberCount -= 1;
            lblSubscriberInfo.Text = "当前有：" + m_SubscriberCount.ToString() + "个订阅者";
        }

        public void RegistDisposeAction(Action<string> disposeAction)
        {
            m_OnDispose = disposeAction;
        }

        public void RegistMessageManagerAction(Func<string, object, int> msgPublishAction)
        {
            m_OnPublishMessage = msgPublishAction;
        }

        #endregion

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (null != m_OnPublishMessage)
                m_OnPublishMessage.Invoke(MessageType, textBox1.Text);
        }

        private void PublisherForm_Load(object sender, EventArgs e)
        {
            m_OnPublishMessage = PlugContext.Current.RegistMessagePulisher(this);
        }

        private void PublisherForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null != m_OnDispose)
                m_OnDispose.Invoke(MessageType);
        }
    }
}
