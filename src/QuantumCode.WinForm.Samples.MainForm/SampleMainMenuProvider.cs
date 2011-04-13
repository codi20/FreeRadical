using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuantumCode.ComponentModel;

namespace QuantumCode.WinForm.Samples.MainForm
{
    [MainMenuContract("SampleMainMenu")]
    public class SampleMainMenuProvider : IMainMenu
    {
        private MenuStrip m_MainMenu;

        public SampleMainMenuProvider()
        {
            m_MainMenu = new MenuStrip();
        }

        #region IMainMenu Members

        public void AddSubMenu(string parentMenuPath, string menuName, string text, EventHandler click)
        {
            string[] paths = parentMenuPath.Split(new char[] { '.' });

            ToolStripMenuItem target = null;

            if (m_MainMenu.Items.ContainsKey(paths[0]))
            {
                target = m_MainMenu.Items[paths[0]] as ToolStripMenuItem;
            }

            if (null != target)
            {
                if (1 != paths.Length)
                {
                    for (int i = 1; i < paths.Length; i++)
                    {
                        if (target.HasDropDownItems)
                        {
                            if (target.DropDownItems.ContainsKey(paths[i]))
                            {
                                target = target.DropDownItems[paths[i]] as ToolStripMenuItem;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (target.Name == paths[paths.Length - 1])
                {
                    ToolStripMenuItem newMenu = new ToolStripMenuItem(text);
                    newMenu.Name = menuName;
                    if (null != click)
                        newMenu.Click += click;

                    target.DropDownItems.Add(newMenu);
                }
            }
        }

        public void AddTopMenu(string menuName, string text, EventHandler click)
        {
            ToolStripMenuItem newMenu = new ToolStripMenuItem(text);
            newMenu.Name = menuName;

            if (null != click)
                newMenu.Click += click;

            m_MainMenu.Items.Add(newMenu);
        }

        public Control MainMenu
        {
            get { return m_MainMenu; }
        }

        #endregion
    }
}
