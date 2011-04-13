using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Hosting = System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using QuantumCode.ComponentModel;
using QuantumCode.Common;
using QuantumCode.WinForm.Plugin;
using QuantumCode.WinForm.Plugin.Context;

namespace QuantumCode.WinForm.Launcher
{
    class Program : BaseComponentPart
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Program p = new Program();
            p.Run();
        }

        public void Run()
        {
            if (null != PlugContext.Current.MainFormStatusBar)
            {
                PlugContext.Current.MainFormStatusBar.OnStatusBarInitialize();
                PlugContext.Current.MainForm.Controls.Add(PlugContext.Current.MainFormStatusBar.StatusBar);
            }

            if (null != PlugContext.Current.MenuLoaders && 0 != PlugContext.Current.MenuLoaders.Count)
            {
                foreach (IMenuLoader loader in PlugContext.Current.MenuLoaders)
                {
                    List<QCMenuItem> menus = loader.LoadMenus();

                    foreach (QCMenuItem item in menus)
                    {
                        if (String.IsNullOrEmpty(item.ParentName))
                        {
                            PlugContext.Current.MainMenu.AddTopMenu(item.Name, item.Text, item.Click);
                        }
                        else
                        {
                            PlugContext.Current.MainMenu.AddSubMenu(item.ParentName, item.Name, item.Text, item.Click);
                        }
                    }
                }
            }

            PlugContext.Current.MainForm.Controls.Add(PlugContext.Current.MainMenu.MainMenu);

            PlugContext.Current.MainForm.FormClosing += new FormClosingEventHandler(mainForm_FormClosing);

            Application.Run(PlugContext.Current.MainForm);
        }

        void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null != PlugContext.Current.MainFormStatusBar)
            {
                PlugContext.Current.MainFormStatusBar.OnStatusBarDestroy();
                PlugContext.Current.MainFormStatusBar.Dispose();
            }
        }
    }
}
