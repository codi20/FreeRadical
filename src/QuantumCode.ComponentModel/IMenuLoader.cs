using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuantumCode.ComponentModel
{
    public interface IMenuLoader
    {
        List<QCMenuItem> LoadMenus();
    }
}
