using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuantumCode.ComponentModel;
using System.ComponentModel.Composition;
using System.Collections;
using System.Threading;

namespace QuantumCode.WinForm.MessageManager
{
    [Export(typeof(IMessageManager))]
    public class DefaultMessageManager : IMessageManager
    {
        private Func<string, object, int> m_publisherAction;

        private Dictionary<string, List<ISubscriber>> m_Subscibers;

        private Dictionary<string, IPublisher> m_Publishers;

        private static ReaderWriterLockSlim m_Locker = new ReaderWriterLockSlim();

        private int PublishMessage(string messageType, object msg)
        {
            List<ISubscriber> subscribers = GetSubscriber(messageType);

            m_Locker.TryEnterWriteLock(50);

            int counter = 0;

            foreach (ISubscriber subscriber in subscribers)
            {
                if (subscriber.MessageReceiveAction.Invoke(msg))
                    counter++;
            }

            m_Locker.ExitWriteLock();

            return counter;
        }

        private void PublisherDispose(string disposeTarget)
        {
            if (m_Publishers.ContainsKey(disposeTarget))
            {
                m_Locker.TryEnterWriteLock(50);

                if (m_Subscibers.ContainsKey(disposeTarget))
                {
                    foreach (ISubscriber s in m_Subscibers[disposeTarget])
                    {
                        s.OnPublisherDeactive();
                    }

                    m_Subscibers[disposeTarget].Clear();
                }

                m_Publishers.Remove(disposeTarget);

                m_Locker.ExitWriteLock();
            }
        }

        private void SubscriberDispose(string messageType, string disposeSubscriber)
        {
            m_Locker.TryEnterReadLock(50);

            ISubscriber target = null;
            IPublisher publisher = null;

            if (m_Subscibers.ContainsKey(messageType))
            {
                target = m_Subscibers[messageType].SingleOrDefault(s => s.ID == disposeSubscriber);

                if(m_Publishers.ContainsKey(messageType))
                    publisher = m_Publishers[target.MessageType];
            }

            m_Locker.ExitReadLock();

            if (null != target)
            {
                m_Locker.TryEnterWriteLock(50);

                m_Subscibers[messageType].Remove(target);

                if(null != publisher)
                    publisher.OnSubscriberDeactive();

                m_Locker.ExitWriteLock();
            }
        }

        public DefaultMessageManager()
        {
            m_publisherAction = PublishMessage;

            m_Subscibers = new Dictionary<string, List<ISubscriber>>();

            m_Publishers = new Dictionary<string, IPublisher>();
        }

        #region IMessageManager Members

        public IPublisher GetPublisher(string messageType)
        {
            if (m_Publishers.ContainsKey(messageType))
                return m_Publishers[messageType];
            else
                return null;
        }

        public List<ISubscriber> GetSubscriber(string messageType)
        {
            m_Locker.TryEnterWriteLock(50);

            List<ISubscriber> retValue = new List<ISubscriber>();
    
            if(m_Subscibers.ContainsKey(messageType))
                retValue = m_Subscibers[messageType];

            m_Locker.ExitWriteLock();

            return retValue;
        }

        public List<IPublisher> Publishers
        {
            get 
            {
                List<IPublisher> retValue = new List<IPublisher>();

                m_Locker.TryEnterReadLock(50);

                retValue = m_Publishers.Values.ToList();

                m_Locker.ExitReadLock();

                return retValue;
            }
        }

        public Func<string, object, int> RegistPublisher(IPublisher publisher)
        {
            Func<string, object, int> retValue = null;

            string checkedKey = null;

            if (0 != m_Publishers.Count)
            {    
                m_Locker.TryEnterReadLock(50);
                checkedKey = m_Publishers.Keys.SingleOrDefault(p => p == publisher.MessageType);
                m_Locker.ExitReadLock();
            }

            if (null == checkedKey)
            {
                m_Locker.TryEnterWriteLock(50);
                m_Publishers.Add(publisher.MessageType, publisher);
                publisher.ID = Guid.NewGuid().ToString();
                publisher.RegistDisposeAction(PublisherDispose);
                m_Locker.ExitWriteLock();

                publisher.OnRegistSuccess();

                retValue = m_publisherAction;
            }
            else
            {
                publisher.OnRegistFault("存在相同的消息类型的发布者");
            }

            return retValue;
        }

        public void RegistSubscriber(ISubscriber subscriber)
        {
            m_Locker.TryEnterReadLock(50);
            IPublisher publisher = null;
            if(m_Publishers.ContainsKey(subscriber.MessageType))
                publisher = m_Publishers[subscriber.MessageType];
            m_Locker.ExitReadLock();

            if (null == publisher)
            {
                subscriber.OnRegistFault("没有对应的消息发布者");
            }
            else
            {
                m_Locker.TryEnterWriteLock(50);

                subscriber.ID = Guid.NewGuid().ToString();

                subscriber.RegistDisposeAction(SubscriberDispose);

                if (!m_Subscibers.ContainsKey(subscriber.MessageType))
                {
                    m_Subscibers.Add(subscriber.MessageType, new List<ISubscriber>());
                }

                m_Subscibers[subscriber.MessageType].Add(subscriber);

                subscriber.OnRegistSuccess();

                publisher.OnSubscriberActive(subscriber);

                m_Locker.ExitWriteLock();
            }
        }

        #endregion
    }
}
