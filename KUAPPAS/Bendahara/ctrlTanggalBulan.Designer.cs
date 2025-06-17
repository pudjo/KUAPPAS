namespace KUAPPAS.Bendahara
{
    partial class ctrlTanggalBulan
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
            this.ctrlPeriode1 = new KUAPPAS.Bendahara.ctrlPeriode();
            this.rbTanggal = new System.Windows.Forms.RadioButton();
            this.rbBulan = new System.Windows.Forms.RadioButton();
            this.ctrlBulan1 = new KUAPPAS.ctrlBulan();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ctrlPeriode1);
            this.groupBox1.Controls.Add(this.rbTanggal);
            this.groupBox1.Controls.Add(this.rbBulan);
            this.groupBox1.Controls.Add(this.ctrlBulan1);
            this.groupBox1.Location = new System.Drawing.Point(1, -5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(748, 48);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // ctrlPeriode1
            // 
            this.ctrlPeriode1.Location = new System.Drawing.Point(96, 14);
            this.ctrlPeriode1.Margin = new System.Windows.Forms.Padding(0);
            this.ctrlPeriode1.Name = "ctrlPeriode1";
            this.ctrlPeriode1.Size = new System.Drawing.Size(284, 26);
            this.ctrlPeriode1.TabIndex = 21;
            this.ctrlPeriode1.TanggalAkhir = new System.DateTime(2024, 4, 19, 0, 0, 0, 0);
            this.ctrlPeriode1.TanggalAwaal = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            this.ctrlPeriode1.Load += new System.EventHandler(this.ctrlPeriode1_Load);
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
            this.rbTanggal.CheckedChanged += new System.EventHandler(this.rbTanggal_CheckedChanged);
            // 
            // rbBulan
            // 
            this.rbBulan.AutoSize = true;
            this.rbBulan.Location = new System.Drawing.Point(410, 18);
            this.rbBulan.Name = "rbBulan";
            this.rbBulan.Size = new System.Drawing.Size(52, 17);
            this.rbBulan.TabIndex = 20;
            this.rbBulan.Text = "Bulan";
            this.rbBulan.UseVisualStyleBackColor = true;
            this.rbBulan.CheckedChanged += new System.EventHandler(this.tvBulan_CheckedChanged);
            // 
            // ctrlBulan1
            // 
            this.ctrlBulan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlBulan1.Location = new System.Drawing.Point(468, 16);
            this.ctrlBulan1.Name = "ctrlBulan1";
            this.ctrlBulan1.Size = new System.Drawing.Size(213, 23);
            this.ctrlBulan1.TabIndex = 17;
            this.ctrlBulan1.Load += new System.EventHandler(this.ctrlBulan1_Load);
            // 
            // ctrlTanggalBulan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ctrlTanggalBulan";
            this.Size = new System.Drawing.Size(759, 46);
            this.Load += new System.EventHandler(this.ctrlTanggalBulan_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private ctrlPeriode ctrlPeriode1;
        private System.Windows.Forms.RadioButton rbTanggal;
        private System.Windows.Forms.RadioButton rbBulan;
        private ctrlBulan ctrlBulan1;
    }
}
