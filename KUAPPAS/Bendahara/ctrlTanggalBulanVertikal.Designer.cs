namespace KUAPPAS.Bendahara
{
    partial class ctrlTanggalBulanVertikal
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbTanggal = new System.Windows.Forms.RadioButton();
            this.rbBulan = new System.Windows.Forms.RadioButton();
            this.ctrlBulan1 = new KUAPPAS.ctrlBulan();
            this.ctrlPeriode1 = new KUAPPAS.Bendahara.ctrlPeriode();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ctrlPeriode1);
            this.groupBox1.Controls.Add(this.rbTanggal);
            this.groupBox1.Controls.Add(this.rbBulan);
            this.groupBox1.Controls.Add(this.ctrlBulan1);
            this.groupBox1.Location = new System.Drawing.Point(3, -5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(396, 75);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // rbTanggal
            // 
            this.rbTanggal.AutoSize = true;
            this.rbTanggal.Checked = true;
            this.rbTanggal.Location = new System.Drawing.Point(29, 20);
            this.rbTanggal.Name = "rbTanggal";
            this.rbTanggal.Size = new System.Drawing.Size(64, 17);
            this.rbTanggal.TabIndex = 19;
            this.rbTanggal.TabStop = true;
            this.rbTanggal.Text = "Tanggal";
            this.rbTanggal.UseVisualStyleBackColor = true;
            // 
            // rbBulan
            // 
            this.rbBulan.AutoSize = true;
            this.rbBulan.Location = new System.Drawing.Point(29, 43);
            this.rbBulan.Name = "rbBulan";
            this.rbBulan.Size = new System.Drawing.Size(52, 17);
            this.rbBulan.TabIndex = 20;
            this.rbBulan.Text = "Bulan";
            this.rbBulan.UseVisualStyleBackColor = true;
            // 
            // ctrlBulan1
            // 
            this.ctrlBulan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlBulan1.Location = new System.Drawing.Point(96, 43);
            this.ctrlBulan1.Name = "ctrlBulan1";
            this.ctrlBulan1.Size = new System.Drawing.Size(284, 23);
            this.ctrlBulan1.TabIndex = 17;
            // 
            // ctrlPeriode1
            // 
            this.ctrlPeriode1.Location = new System.Drawing.Point(100, 15);
            this.ctrlPeriode1.Margin = new System.Windows.Forms.Padding(0);
            this.ctrlPeriode1.Name = "ctrlPeriode1";
            this.ctrlPeriode1.Size = new System.Drawing.Size(284, 26);
            this.ctrlPeriode1.TabIndex = 21;
            this.ctrlPeriode1.TanggalAkhir = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            this.ctrlPeriode1.TanggalAwaal = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            // 
            // ctrlTanggalBulanVertikal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ctrlTanggalBulanVertikal";
            this.Size = new System.Drawing.Size(404, 75);
            this.Load += new System.EventHandler(this.ctrlTanggalBulanVertikal_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbTanggal;
        private System.Windows.Forms.RadioButton rbBulan;
        private ctrlBulan ctrlBulan1;
        private ctrlPeriode ctrlPeriode1;
    }
}
