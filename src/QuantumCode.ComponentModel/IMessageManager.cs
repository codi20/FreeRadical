using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace QuantumCode.ComponentModel
{
    public interface IMessageManager
    {
        Func<string, object, int> RegistPublisher(IPublisher publisher);

        void RegistSubscriber(ISubscriber subscriber);

        List<IPublisher> Publishers { get; }

        IPublisher GetPublisher(string messageType);

        List<ISubscriber> GetSubscriber(string messageType);
    }
}
