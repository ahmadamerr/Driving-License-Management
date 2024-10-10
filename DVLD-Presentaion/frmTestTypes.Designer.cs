namespace WFADVLD
{
    partial class frmTestTypes
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.editApplicationTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbApplicationTypesmage = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbApplicationTypesmage)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(16, 227);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(772, 214);
            this.dataGridView1.TabIndex = 120;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editApplicationTypeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(212, 42);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(147, 163);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(446, 39);
            this.lblTitle.TabIndex = 118;
            this.lblTitle.Text = "Manage Application Types";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsCount.Location = new System.Drawing.Point(114, 457);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(26, 18);
            this.lblRecordsCount.TabIndex = 123;
            this.lblRecordsCount.Text = "??";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 454);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 20);
            this.label2.TabIndex = 122;
            this.label2.Text = "# Records:";
            // 
            // editApplicationTypeToolStripMenuItem
            // 
            this.editApplicationTypeToolStripMenuItem.Image = global::WFADVLD.Properties.Resources.edit_32;
            this.editApplicationTypeToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.editApplicationTypeToolStripMenuItem.Name = "editApplicationTypeToolStripMenuItem";
            this.editApplicationTypeToolStripMenuItem.Size = new System.Drawing.Size(211, 38);
            this.editApplicationTypeToolStripMenuItem.Text = "Edit Application Type";
            this.editApplicationTypeToolStripMenuItem.Click += new System.EventHandler(this.editApplicationTypeToolStripMenuItem_Click);
            // 
            // pbApplicationTypesmage
            // 
            this.pbApplicationTypesmage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbApplicationTypesmage.Image = global::WFADVLD.Properties.Resources.TestType_512;
            this.pbApplicationTypesmage.InitialImage = null;
            this.pbApplicationTypesmage.Location = new System.Drawing.Point(284, 2);
            this.pbApplicationTypesmage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbApplicationTypesmage.Name = "pbApplicationTypesmage";
            this.pbApplicationTypesmage.Size = new System.Drawing.Size(202, 156);
            this.pbApplicationTypesmage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbApplicationTypesmage.TabIndex = 119;
            this.pbApplicationTypesmage.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnClose.Image = global::WFADVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(694, 447);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 37);
            this.btnClose.TabIndex = 121;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmTestTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 502);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.pbApplicationTypesmage);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblRecordsCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Name = "frmTestTypes";
            this.Text = "Test Types";
            this.Load += new System.EventHandler(this.frmTestTypes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbApplicationTypesmage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.PictureBox pbApplicationTypesmage;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblRecordsCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolStripMenuItem editApplicationTypeToolStripMenuItem;
    }
}