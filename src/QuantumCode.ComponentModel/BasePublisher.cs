using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuantumCode.ComponentModel
{
    public abstract class BasePublisher : IPublisher, IDisposable
    {
        private Func<string, object, int> m_publishAction;
        private Action<string> m_disposeAction;
        private string m_ID;

        #region IPublisher Members

        public void RegistMessageManagerAction(Func<string, object, int> msgPublishAction)
        {
            m_publishAction = msgPublishAction;
        }

        public abstract string MessageType
        {
            get;
        }

        public void RegistDisposeAction(Action<string> disposeAction)
        {
            m_disposeAction = disposeAction;
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

        public abstract void OnRegistFault(string msg);

        public abstract void OnRegistSuccess();

        public abstract void OnSubscriberActive(ISubscriber subscriber);

        public abstract void OnSubscriberDeactive();

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (null != m_disposeAction)
                m_disposeAction.Invoke(MessageType);
        }

        #endregion
    }
}
