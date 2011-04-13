namespace QuantumCode.WinForm.Samples.SubForms
{
    partial class SubscriberForm
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
            this.lblSubscriberInfo = new System.Windows.Forms.Label();
            this.lblStatusInfo = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblSubscriberInfo
            // 
            this.lblSubscriberInfo.AutoSize = true;
            this.lblSubscriberInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSubscriberInfo.Location = new System.Drawing.Point(0, 0);
            this.lblSubscriberInfo.Name = "lblSubscriberInfo";
            this.lblSubscriberInfo.Size = new System.Drawing.Size(41, 12);
            this.lblSubscriberInfo.TabIndex = 0;
            this.lblSubscriberInfo.Text = "label1";
            // 
            // lblStatusInfo
            // 
            this.lblStatusInfo.AutoSize = true;
            this.lblStatusInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatusInfo.Location = new System.Drawing.Point(0, 250);
            this.lblStatusInfo.Name = "lblStatusInfo";
            this.lblStatusInfo.Size = new System.Drawing.Size(41, 12);
            this.lblStatusInfo.TabIndex = 1;
            this.lblStatusInfo.Text = "label1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(47, 90);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(191, 21);
            this.textBox1.TabIndex = 2;
            // 
            // SubscriberForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblStatusInfo);
            this.Controls.Add(this.lblSubscriberInfo);
            this.Name = "SubscriberForm";
            this.Text = "SubscriberForm";
            this.Load += new System.EventHandler(this.SubscriberForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SubscriberForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSubscriberInfo;
        private System.Windows.Forms.Label lblStatusInfo;
        private System.Windows.Forms.TextBox textBox1;
    }
}