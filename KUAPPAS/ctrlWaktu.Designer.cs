namespace KUAPPAS
{
    partial class ctrlWaktu
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
            this.ctrlPeriode1 = new KUAPPAS.Bendahara.ctrlPeriode();
            this.ctrlTriwulan1 = new KUAPPAS.ctrlTriwulan();
            this.SuspendLayout();
            // 
            // ctrlPeriode1
            // 
            this.ctrlPeriode1.Location = new System.Drawing.Point(0, 36);
            this.ctrlPeriode1.Margin = new System.Windows.Forms.Padding(0);
            this.ctrlPeriode1.Name = "ctrlPeriode1";
            this.ctrlPeriode1.Size = new System.Drawing.Size(272, 27);
            this.ctrlPeriode1.TabIndex = 0;
            this.ctrlPeriode1.TanggalAkhir = new System.DateTime(2024, 1, 24, 0, 0, 0, 0);
            this.ctrlPeriode1.TanggalAwaal = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            // 
            // ctrlTriwulan1
            // 
            this.ctrlTriwulan1.Location = new System.Drawing.Point(0, 3);
            this.ctrlTriwulan1.Name = "ctrlTriwulan1";
            this.ctrlTriwulan1.Size = new System.Drawing.Size(272, 23);
            this.ctrlTriwulan1.TabIndex = 1;
            // 
            // ctrlWaktu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctrlTriwulan1);
            this.Controls.Add(this.ctrlPeriode1);
            this.Name = "ctrlWaktu";
            this.Size = new System.Drawing.Size(309, 66);
            this.Load += new System.EventHandler(this.ctrlWaktu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Bendahara.ctrlPeriode ctrlPeriode1;
        private ctrlTriwulan ctrlTriwulan1;
    }
}
