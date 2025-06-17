namespace KUAPPAS
{
    partial class TambahRekeningKesehatan
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
            this.txtIDSUBKegiatan = new System.Windows.Forms.TextBox();
            this.idrekening = new System.Windows.Forms.TextBox();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtIDSUBKegiatan
            // 
            this.txtIDSUBKegiatan.Location = new System.Drawing.Point(155, 62);
            this.txtIDSUBKegiatan.Name = "txtIDSUBKegiatan";
            this.txtIDSUBKegiatan.Size = new System.Drawing.Size(233, 20);
            this.txtIDSUBKegiatan.TabIndex = 0;
            // 
            // idrekening
            // 
            this.idrekening.Location = new System.Drawing.Point(159, 95);
            this.idrekening.Name = "idrekening";
            this.idrekening.Size = new System.Drawing.Size(228, 20);
            this.idrekening.TabIndex = 1;
            // 
            // txtJumlah
            // 
            this.txtJumlah.Location = new System.Drawing.Point(165, 140);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(100, 20);
            this.txtJumlah.TabIndex = 2;
            // 
            // TambahRekeningKesehatan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 322);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.idrekening);
            this.Controls.Add(this.txtIDSUBKegiatan);
            this.Name = "TambahRekeningKesehatan";
            this.Text = "TambahRekeningKesehatan";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIDSUBKegiatan;
        private System.Windows.Forms.TextBox idrekening;
        private System.Windows.Forms.TextBox txtJumlah;
    }
}