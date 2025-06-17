namespace KUAPPAS.Bendahara
{
    partial class ctrlAlasanPenolakan
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbAlasan = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbAlasan
            // 
            this.cmbAlasan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbAlasan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAlasan.FormattingEnabled = true;
            this.cmbAlasan.Location = new System.Drawing.Point(0, 0);
            this.cmbAlasan.Name = "cmbAlasan";
            this.cmbAlasan.Size = new System.Drawing.Size(529, 21);
            this.cmbAlasan.TabIndex = 0;
            this.cmbAlasan.SelectedIndexChanged += new System.EventHandler(this.cmbAlasan_SelectedIndexChanged);
            this.cmbAlasan.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.cmbAlasan_MouseDoubleClick);
            // 
            // ctrlAlasanPenolakan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbAlasan);
            this.Name = "ctrlAlasanPenolakan";
            this.Size = new System.Drawing.Size(529, 35);
            this.Load += new System.EventHandler(this.ctrlAlasanPenolakan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbAlasan;
    }
}
