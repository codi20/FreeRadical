namespace QuantumCode.WinForm.Samples.SubForms
{
    partial class PublisherForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblSubscriberInfo = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInfo.Location = new System.Drawing.Point(0, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(41, 12);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "label1";
            // 
            // lblSubscriberInfo
            // 
            this.lblSubscriberInfo.AutoSize = true;
            this.lblSubscriberInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSubscriberInfo.Location = new System.Drawing.Point(0, 287);
            this.lblSubscriberInfo.Name = "lblSubscriberInfo";
            this.lblSubscriberInfo.Size = new System.Drawing.Size(41, 12);
            this.lblSubscriberInfo.TabIndex = 1;
            this.lblSubscriberInfo.Text = "label1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(57, 102);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(207, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // PublisherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 299);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblSubscriberInfo);
            this.Controls.Add(this.lblInfo);
            this.Name = "PublisherForm";
            this.Text = "发布者窗体";
            this.Load += new System.EventHandler(this.PublisherForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PublisherForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblSubscriberInfo;
        private System.Windows.Forms.TextBox textBox1;
    }
}