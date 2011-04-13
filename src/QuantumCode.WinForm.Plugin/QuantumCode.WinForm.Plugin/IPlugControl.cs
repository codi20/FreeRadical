using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuantumCode.WinForm.Plugin
{
    public interface IPlugControl : IPlug
    {
        Control Control { get; }
    }
}
