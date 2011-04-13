using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuantumCode.WinForm.Plugin
{
    public interface IPlugForm
    {
        Form Form { get; }
        bool IsDialog { get; }
        bool IsMdiChild { get; }
        Form ParentForm { get; set; }
    }
}
