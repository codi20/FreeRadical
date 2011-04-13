using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuantumCode.ComponentModel
{
    public class QCMenuItem
    {
        private string m_Name;
        private string m_Text;
        private EventHandler m_Click;
        private string m_ParentName;

        public QCMenuItem(string name, string text, string parentName, EventHandler click)
        {
            m_Name = name;
            m_Text = text;
            m_ParentName = parentName;
            m_Click = click;
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        public string Text
        {
            get
            {
                return m_Text;
            }
        }

        public string ParentName
        {
            get
            {
                return m_ParentName;
            }
        }

        public EventHandler Click
        {
            get
            {
                return m_Click;
            }
        }
    }
}
