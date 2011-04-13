using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace QuantumCode.ComponentModel
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MainFormContactAttribute : ExportAttribute
    {
        private string m_FormID;

        public MainFormContactAttribute(string id) : base(typeof(IMainForm))
        {
            m_FormID = id;
        }

        public string FormID
        {
            get
            {
                return m_FormID;
            }
        }
    }
}
