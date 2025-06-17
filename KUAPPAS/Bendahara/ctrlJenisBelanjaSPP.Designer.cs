namespace KUAPPAS.Bendahara
{
    partial class ctrlJenisBelanjaSPP
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
            this.cmbJenisKegiatan = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbJenisKegiatan
            // 
            this.cmbJenisKegiatan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbJenisKegiatan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbJenisKegiatan.FormattingEnabled = true;
            this.cmbJenisKegiatan.Location = new System.Drawing.Point(0, 0);
            this.cmbJenisKegiatan.Name = "cmbJenisKegiatan";
            this.cmbJenisKegiatan.Size = new System.Drawing.Size(384, 21);
            this.cmbJenisKegiatan.TabIndex = 0;
            // 
            // ctrlJenisBelanjaSPP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbJenisKegiatan);
            this.Name = "ctrlJenisBelanjaSPP";
            this.Size = new System.Drawing.Size(384, 29);
            this.Load += new System.EventHandler(this.ctrlJenisBelanjaSPPcs_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbJenisKegiatan;
    }
}
