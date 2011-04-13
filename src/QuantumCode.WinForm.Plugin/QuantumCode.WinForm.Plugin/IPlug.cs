using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuantumCode.WinForm.Plugin.Context;

namespace QuantumCode.WinForm.Plugin
{
    public interface IPlug : IDisposable
    {
        string PlugID { get; }

        string[] DependencePlugs { get; }

        void OnPlugInitialize(PlugContext context);

        void OnPlugDesitroing(PlugContext context);
    }
}
