using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuantumCode.Form.Windows.Test
{
    public partial class Form1 : QuntumCode.Form.Windows.MovableControlForm
    {
        public Form1()
        {
            InitializeComponent();

            AddDraggingControl(this.button1);
            AddDraggingControl(this.label1);
        }
    }
}
