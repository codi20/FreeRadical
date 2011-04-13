using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using QuantumCode.ComponentModel;

namespace QuantumCode.WinForm.Samples.MainForm
{
    [MainFormContact("SampleMainForm")]
    public class SampleMainFormProvider : IMainForm
    {
        private SampleMainForm m_MaiForm;

        private void BuildMainForm()
        {
            string mainFormConfigName = WinFormContext.Current.Configs["Defines"]["MainForm"];
            m_MaiForm.Text = WinFormContext.Current.Configs[mainFormConfigName]["Title"];
            m_MaiForm.StartPosition = (FormStartPosition)Enum.Parse(typeof(FormStartPosition),
                WinFormContext.Current.Configs[mainFormConfigName]["StartPosition"]);
            m_MaiForm.WindowState = (FormWindowState)Enum.Parse(typeof(FormWindowState),
                WinFormContext.Current.Configs[mainFormConfigName]["WindowState"]);
        }

        public SampleMainFormProvider()
        {
            m_MaiForm = new SampleMainForm();
            m_MaiForm.IsMdiContainer = true;
            BuildMainForm();
        }

        #region IMainForm Members

        public void AddSubMenu(string menuPath, string subMenuName, EventHandler clickEvent)
        {
            throw new NotImplementedException();
        }

        public void AddTopMenu(string menuName)
        {
            throw new NotImplementedException();
        }

        public string FormContract
        {
            get { return "SampleMainForm"; }
        }

        public System.Windows.Forms.Form MainForm
        {
            get { return m_MaiForm; }
        }

        #endregion
    }
}
