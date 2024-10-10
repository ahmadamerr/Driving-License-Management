namespace WFADVLD
{
    partial class frmLDLApplicationInfo
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
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlDLApplicationInfo1 = new WFADVLD.Controls.ctrlDLApplicationInfo();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Image = global::WFADVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(596, 304);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(126, 37);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlDLApplicationInfo1
            // 
            this.ctrlDLApplicationInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlDLApplicationInfo1.Location = new System.Drawing.Point(-1, 12);
            this.ctrlDLApplicationInfo1.Name = "ctrlDLApplicationInfo1";
            this.ctrlDLApplicationInfo1.Size = new System.Drawing.Size(723, 284);
            this.ctrlDLApplicationInfo1.TabIndex = 19;
            // 
            // frmLDLApplicationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(726, 351);
            this.Controls.Add(this.ctrlDLApplicationInfo1);
            this.Controls.Add(this.btnClose);
            this.Name = "frmLDLApplicationInfo";
            this.Text = "frmLDLApplicationInfo";
            this.Load += new System.EventHandler(this.frmLDLApplicationInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private Controls.ctrlDLApplicationInfo ctrlDLApplicationInfo1;
    }
}