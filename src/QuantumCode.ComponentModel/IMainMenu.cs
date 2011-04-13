using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuantumCode.ComponentModel
{
    public interface IMainMenu
    {
        void AddTopMenu(string menuName, string text, EventHandler click);

        void AddSubMenu(string parentMenuPath, string menuName, string text, EventHandler click);

        Control MainMenu { get; }
    }
}
