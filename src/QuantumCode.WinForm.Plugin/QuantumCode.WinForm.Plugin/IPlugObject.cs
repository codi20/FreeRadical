using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuantumCode.WinForm.Plugin
{
    public interface IPlugObject<ServiceType> : IPlug
        where ServiceType : class
    {
        ServiceType Service { get; }
    }
}
