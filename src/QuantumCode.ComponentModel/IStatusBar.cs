using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuantumCode.ComponentModel
{
    public interface IStatusBar : IDisposable
    {
        bool AddItem(string name);

        bool ShowMessage(string itemName, string msg);

        Control StatusBar { get; }

        void OnStatusBarInitialize();

        void OnStatusBarDestroy();
    }
}
