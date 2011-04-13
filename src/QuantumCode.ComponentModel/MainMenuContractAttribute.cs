using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace QuantumCode.ComponentModel
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MainMenuContractAttribute : ExportAttribute
    {
        private string m_MainMenuID;

        public MainMenuContractAttribute(string id)
            : base(typeof(IMainMenu))
        {
            m_MainMenuID = id;
        }

        public string ID
        {
            get { return m_MainMenuID; }
            set { m_MainMenuID = value; }
        }
    }
}
