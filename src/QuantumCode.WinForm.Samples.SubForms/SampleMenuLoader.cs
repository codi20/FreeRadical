using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuantumCode.ComponentModel;
using System.ComponentModel.Composition;
using QuantumCode.WinForm.Plugin.Context;

namespace QuantumCode.WinForm.Samples.SubForms
{
    [Export(typeof(IMenuLoader))]
    public class SampleMenuLoader : IMenuLoader
    {
        #region IMenuLoader Members

        public List<QCMenuItem> LoadMenus()
        {
            List<QCMenuItem> retValue = new List<QCMenuItem>();

            QCMenuItem top = new QCMenuItem("TopTest", "测试菜单一", "", null);
            retValue.Add(top);
            top = new QCMenuItem("TopTest1", "测试菜单二", "", null);
            retValue.Add(top);

            QCMenuItem sub = new QCMenuItem("SubTest1", "测试子菜单一", "TopTest", null);
            retValue.Add(sub);

            sub = new QCMenuItem("SubTest12", "测试子菜单一二", "TopTest.SubTest1", null);
            retValue.Add(sub);

            sub = new QCMenuItem("SubTest2", "测试子菜单一三", "TopTest", null);
            retValue.Add(sub);

            sub = new QCMenuItem("SubTest3", "测试子菜单三", "TopTest1", null);
            retValue.Add(sub);

            sub = new QCMenuItem("OpenForm1", "打开Form1", "TopTest", Form1_Open);
            retValue.Add(sub);

            sub = new QCMenuItem("OpenPublisherForm", "打开发布者窗体", "TopTest", Form2_Open);
            retValue.Add(sub);

            sub = new QCMenuItem("OpenPublisherForm", "打开订阅者窗体", "TopTest", Form3_Open);
            retValue.Add(sub);


            return retValue;
        }

        private void Form1_Open(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.MdiParent = PlugContext.Current.MainForm;
            f.Show();
        }

        private void Form2_Open(object sender, EventArgs e)
        {
            PublisherForm f = new PublisherForm();
            f.MdiParent = PlugContext.Current.MainForm;
            f.Show();
        }

        private void Form3_Open(object sender, EventArgs e)
        {
            SubscriberForm f = new SubscriberForm();
            f.MdiParent = PlugContext.Current.MainForm;
            f.Show();
        }

        #endregion
    }
}
