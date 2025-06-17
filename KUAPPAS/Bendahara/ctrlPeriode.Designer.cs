namespace KUAPPAS.Bendahara
{
    partial class ctrlPeriode
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
            this.label1 = new System.Windows.Forms.Label();
            this.dtAkhir = new KUAPPAS.ctrlTanggal();
            this.dtAwal = new KUAPPAS.ctrlTanggal();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "s/d";
            // 
            // dtAkhir
            // 
            this.dtAkhir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtAkhir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtAkhir.Location = new System.Drawing.Point(161, 0);
            this.dtAkhir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtAkhir.Name = "dtAkhir";
            this.dtAkhir.Size = new System.Drawing.Size(120, 25);
            this.dtAkhir.TabIndex = 4;
            this.dtAkhir.Tanggal = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            this.dtAkhir.Load += new System.EventHandler(this.dtAkhir_Load);
            // 
            // dtAwal
            // 
            this.dtAwal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtAwal.Location = new System.Drawing.Point(0, 0);
            this.dtAwal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtAwal.Name = "dtAwal";
            this.dtAwal.Size = new System.Drawing.Size(127, 26);
            this.dtAwal.TabIndex = 3;
            this.dtAwal.Tanggal = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            // 
            // ctrlPeriode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtAkhir);
            this.Controls.Add(this.dtAwal);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ctrlPeriode";
            this.Size = new System.Drawing.Size(284, 26);
            this.Load += new System.EventHandler(this.ctrlPeriode_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private ctrlTanggal dtAwal;
        private ctrlTanggal dtAkhir;
    }
}
