namespace KUAPPAS.Bendahara
{
    partial class ctrlJenisPenerimaan
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
            this.cmbJenisPenerimaan = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbJenisPenerimaan
            // 
            this.cmbJenisPenerimaan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbJenisPenerimaan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbJenisPenerimaan.FormattingEnabled = true;
            this.cmbJenisPenerimaan.Location = new System.Drawing.Point(0, 0);
            this.cmbJenisPenerimaan.Name = "cmbJenisPenerimaan";
            this.cmbJenisPenerimaan.Size = new System.Drawing.Size(479, 21);
            this.cmbJenisPenerimaan.TabIndex = 0;
            this.cmbJenisPenerimaan.SelectedIndexChanged += new System.EventHandler(this.cmbJenisPenerimaan_SelectedIndexChanged);
            // 
            // ctrlJenisPenerimaan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbJenisPenerimaan);
            this.Name = "ctrlJenisPenerimaan";
            this.Size = new System.Drawing.Size(479, 27);
            this.Load += new System.EventHandler(this.ctrlJenisPenerimaan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbJenisPenerimaan;
    }
}
