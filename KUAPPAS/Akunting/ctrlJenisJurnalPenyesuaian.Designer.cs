namespace KUAPPAS.Akunting
{
    partial class ctrlJenisJurnalPenyesuaian
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
            this.cmbJenisJurnalPenyesuaian = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbJenisJurnalPenyesuaian
            // 
            this.cmbJenisJurnalPenyesuaian.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbJenisJurnalPenyesuaian.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbJenisJurnalPenyesuaian.FormattingEnabled = true;
            this.cmbJenisJurnalPenyesuaian.Location = new System.Drawing.Point(0, 0);
            this.cmbJenisJurnalPenyesuaian.Name = "cmbJenisJurnalPenyesuaian";
            this.cmbJenisJurnalPenyesuaian.Size = new System.Drawing.Size(338, 21);
            this.cmbJenisJurnalPenyesuaian.TabIndex = 0;
            this.cmbJenisJurnalPenyesuaian.SelectedIndexChanged += new System.EventHandler(this.cmbJenisJurnalPenyesuaian_SelectedIndexChanged);
            // 
            // ctrlJenisJurnalPenyesuaian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbJenisJurnalPenyesuaian);
            this.Name = "ctrlJenisJurnalPenyesuaian";
            this.Size = new System.Drawing.Size(338, 24);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbJenisJurnalPenyesuaian;
    }
}
