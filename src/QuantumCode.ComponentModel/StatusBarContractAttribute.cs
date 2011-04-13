using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace QuantumCode.ComponentModel
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class StatusBarContractAttribute : ExportAttribute
    {
        private string m_StatusBarID;

        public StatusBarContractAttribute(string id)
            : base(typeof(IStatusBar))
        {
            m_StatusBarID = id;
        }

        public string StatusBarID
        {
            get
            {
                return m_StatusBarID;
            }
        }
    }
}
