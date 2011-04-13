using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuantumCode.ComponentModel
{
    public interface IPublisher
    {
        void RegistMessageManagerAction(Func<string, object, int> msgPublishAction);
        string MessageType { get; }
        void RegistDisposeAction(Action<string> disposeAction);
        string ID { get; set; }
        void OnRegistFault(string msg);
        void OnRegistSuccess();
        void OnSubscriberActive(ISubscriber subscriber);
        void OnSubscriberDeactive();
    }
}
