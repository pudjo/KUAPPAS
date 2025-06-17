namespace KUAPPAS
{
    partial class ctrlSubKegiatan
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
            this.cmbSubKegiatan = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbSubKegiatan
            // 
            this.cmbSubKegiatan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSubKegiatan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSubKegiatan.FormattingEnabled = true;
            this.cmbSubKegiatan.Location = new System.Drawing.Point(0, 0);
            this.cmbSubKegiatan.Name = "cmbSubKegiatan";
            this.cmbSubKegiatan.Size = new System.Drawing.Size(311, 21);
            this.cmbSubKegiatan.TabIndex = 0;
            this.cmbSubKegiatan.SelectedIndexChanged += new System.EventHandler(this.cmbSubKegiatan_SelectedIndexChanged);
            // 
            // ctrlSubKegiatan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbSubKegiatan);
            this.Name = "ctrlSubKegiatan";
            this.Size = new System.Drawing.Size(311, 24);
            this.Load += new System.EventHandler(this.ctrlSubKegiatan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSubKegiatan;
    }
}
