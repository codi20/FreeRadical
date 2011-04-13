using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuantumCode.ComponentModel
{
    public abstract class BaseSubscriber : ISubscriber, IDisposable
    {
        private string m_MessageType;

        private string m_ID;

        private Action<string, string> m_OnDispose;

        protected Func<object, bool> m_OnMessageReceive;

        public BaseSubscriber(string messageType)
        {
            m_MessageType = messageType;

            m_OnMessageReceive = OnMessageReceive;
        }

        public abstract bool OnMessageReceive(object msg);

        #region ISubscriber Members

        public Func<object, bool> MessageReceiveAction
        {
            get { return m_OnMessageReceive; }
        }

        public string MessageType
        {
            get { return m_MessageType; }
        }

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

        public void RegistDisposeAction(Action<string, string> disposeAction)
        {
            m_OnDispose = disposeAction;
        }

        public abstract void OnRegistFault(string msg);

        public abstract void OnRegistSuccess();

        public virtual void OnPublisherDeactive()
        {}

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (null != m_OnDispose)
                m_OnDispose.Invoke(MessageType, ID);
        }

        #endregion
    }
}
