using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuantumCode.Common;
using System.ComponentModel.Composition;

namespace QuantumCode.WinForm
{
    public class WinFormContext
    {
        private static WinFormContext m_Instance;
        private static object m_Locker = new object();

        private WinFormContext()
            : base()
        {
            INIFile ini = INIFile.ReadINI("launcher.ini");

            Configs = ini;
        }

        public static WinFormContext Current
        {
            get
            {
                if (null == m_Instance)
                {
                    lock (m_Locker)
                    {
                        if (null == m_Instance)
                            m_Instance = new WinFormContext();
                    }
                }
                return m_Instance;
            }
        }

        private INIFile m_Configs;

        public INIFile Configs
        {
            get
            {
                return m_Configs;
            }
            set
            {
                m_Configs = value;
            }
        }

        public string MainForSectionName
        {
            get
            {
                return Configs["Defines"]["MainForm"];
            }
        }

        public string[] ExtensionDirectoies
        {
            get
            {
                return Configs["Defines"]["ExtensionDirectory"].Split(new char[] { ';' });
            }
        }

        public string StatusBar
        {
            get
            {
                return Configs[MainForSectionName]["StatusBar"];
            }
        }

        public string DefaultStatusBarItem
        {
            get
            {
                return Configs[MainForSectionName]["DefaultStatusBarItem"];
            }
        }

        public string MainMenu
        {
            get
            {
                return Configs[MainForSectionName]["MainMenu"];
            }
        }
    }
}
