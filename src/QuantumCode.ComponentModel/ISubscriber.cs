using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuantumCode.ComponentModel
{
    public interface ISubscriber
    {
        Func<object, bool> MessageReceiveAction { get; }
        string MessageType { get; }
        string ID { get; set; }
        void RegistDisposeAction(Action<string, string> disposeAction);
        void OnRegistFault(string msg);
        void OnRegistSuccess();
        void OnPublisherDeactive();
    }
}
