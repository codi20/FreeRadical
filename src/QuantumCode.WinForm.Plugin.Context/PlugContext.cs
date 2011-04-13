using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuantumCode.ComponentModel;
using System.ComponentModel.Composition;

namespace QuantumCode.WinForm.Plugin.Context
{
    public class PlugContext : BaseComponentPart
    {
        private static PlugContext m_Instance;
        private static object m_locker = new object();

        [ImportMany]
        private Lazy<IMainForm, IMainFormContractCapabilites>[] m_MainFormProvider { get; set; }

        [Import(typeof(IMessageManager))]
        private IMessageManager m_MessageManager { get; set; }

        [ImportMany]
        private Lazy<IStatusBar, IStatusBarContractCapabilities>[] m_StatusBars { get; set; }

        [ImportMany]
        private Lazy<IMainMenu, IMainMenuContractCapabilities>[] m_MainMenus { get; set; }

        [ImportMany(typeof(IMenuLoader))]
        private List<IMenuLoader> m_MenuLoaders { get; set; }

        private Form m_MainForm;
        private IStatusBar m_StatusBar;
        private IMainMenu m_MainMenu;

        private PlugContext()
        {
            m_MainForm = GetMainForm();
        }

        private Form GetMainForm()
        {
            string mainFormConfigName = WinFormContext.Current.Configs["Defines"]["MainForm"];
            string formContract = WinFormContext.Current.Configs[mainFormConfigName]["FormContract"];

            Lazy<IMainForm, IMainFormContractCapabilites> target = 
                m_MainFormProvider.SingleOrDefault(p => p.Metadata.FormID == formContract);

            Form retValue = null;

            if (null != target)
                retValue = target.Value.MainForm;

            return retValue;
        }

        private IStatusBar GetMainFormStatusBar()
        {
            string mainFormConfigName = WinFormContext.Current.Configs["Defines"]["MainForm"];
            string statusBarName = WinFormContext.Current.Configs[mainFormConfigName]["StatusBar"];

            Lazy<IStatusBar, IStatusBarContractCapabilities> target =
                m_StatusBars.SingleOrDefault(s => s.Metadata.StatusBarID == statusBarName);

            IStatusBar retValue = null;

            if (null != target)
            {
                retValue = target.Value;
            }

            return retValue;
        }

        private IMainMenu GetMainMenu()
        {
            Lazy<IMainMenu, IMainMenuContractCapabilities> target =
                m_MainMenus.SingleOrDefault(m => m.Metadata.ID == WinFormContext.Current.MainMenu);

            IMainMenu retValue = null;

            if (null != target)
            {
                retValue = target.Value;
            }

            return retValue;
        }

        public static PlugContext Current
        {
            get
            {
                if (null == m_Instance)
                {
                    lock (m_locker)
                    {
                        if (null == m_Instance)
                        {
                            m_Instance = new PlugContext();
                        }
                    }
                }
                return m_Instance;
            }
        }

        public Form MainForm
        {
            get
            {
                return m_MainForm;
            }
            set
            {
                m_MainForm = value;
            }
        }

        public IStatusBar MainFormStatusBar
        {
            get
            {
                if (null == m_StatusBar)
                {
                    m_StatusBar = GetMainFormStatusBar();
                }

                return m_StatusBar;
            }
        }

        public IMainMenu MainMenu
        {
            get
            {
                if (null == m_MainMenu)
                    m_MainMenu = GetMainMenu();

                return m_MainMenu;
            }
        }

        public List<IMenuLoader> MenuLoaders
        {
            get
            {
                return m_MenuLoaders;
            }
        }

        public bool IsMessageManagerLoaded
        {
            get
            {
                return null != m_MessageManager;
            }
        }

        public Func<string, object, int> RegistMessagePulisher(IPublisher publisher)
        {
            Func<string, object, int> retValue = null;

            if (IsMessageManagerLoaded)
            {
                retValue = m_MessageManager.RegistPublisher(publisher);
            }

            return retValue;
        }

        public void RegistMessageSubscriber(ISubscriber subscriber)
        {
            if (IsMessageManagerLoaded)
            {
                m_MessageManager.RegistSubscriber(subscriber);
            }
        }
    }
}
