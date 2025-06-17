namespace KUAPPAS
{
    partial class ctrlJenisAnggaran
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
            this.cmbJenisAnggaran = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbJenisAnggaran
            // 
            this.cmbJenisAnggaran.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbJenisAnggaran.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbJenisAnggaran.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbJenisAnggaran.FormattingEnabled = true;
            this.cmbJenisAnggaran.Location = new System.Drawing.Point(0, 0);
            this.cmbJenisAnggaran.Name = "cmbJenisAnggaran";
            this.cmbJenisAnggaran.Size = new System.Drawing.Size(150, 22);
            this.cmbJenisAnggaran.TabIndex = 0;
            this.cmbJenisAnggaran.SelectedIndexChanged += new System.EventHandler(this.cmbJenisAnggaran_SelectedIndexChanged);
            // 
            // ctrlJenisAnggaran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbJenisAnggaran);
            this.Name = "ctrlJenisAnggaran";
            this.Size = new System.Drawing.Size(150, 49);
            this.Load += new System.EventHandler(this.ctrlJenisAnggaran_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbJenisAnggaran;
    }
}
