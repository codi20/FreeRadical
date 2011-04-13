using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuantumCode.ComponentModel
{
    public interface IMainForm
    {
        string FormContract { get; }
        Form MainForm { get; }
        void AddTopMenu(string menuName);
        void AddSubMenu(string menuPath, string subMenuName, EventHandler clickEvent);
    }
}
