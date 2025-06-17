namespace KUAPPAS.Bendahara
{
    partial class frmTerimaSPM
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
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.ctrlSPP1 = new KUAPPAS.Bendahara.ctrlSPP();
            this.label1 = new System.Windows.Forms.Label();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.ctrlRekeningKegiatan1 = new KUAPPAS.ctrlRekeningKegiatan();
            this.SuspendLayout();
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(64, 8);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(641, 30);
            this.ctrlDinas1.TabIndex = 0;
            this.ctrlDinas1.OnChanged += new KUAPPAS.ctrlDinas.ValueChangedEventHandler(this.ctrlDinas1_OnChanged);
            // 
            // ctrlSPP1
            // 
            this.ctrlSPP1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSPP1.Location = new System.Drawing.Point(158, 31);
            this.ctrlSPP1.Name = "ctrlSPP1";
            this.ctrlSPP1.Size = new System.Drawing.Size(432, 24);
            this.ctrlSPP1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "No SPM";
            // 
            // txtJumlah
            // 
            this.txtJumlah.Location = new System.Drawing.Point(158, 61);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(432, 20);
            this.txtJumlah.TabIndex = 3;
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.Location = new System.Drawing.Point(158, 99);
            this.txtKeterangan.Multiline = true;
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(720, 48);
            this.txtKeterangan.TabIndex = 4;
            // 
            // ctrlRekeningKegiatan1
            // 
            this.ctrlRekeningKegiatan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlRekeningKegiatan1.Location = new System.Drawing.Point(158, 153);
            this.ctrlRekeningKegiatan1.Name = "ctrlRekeningKegiatan1";
            this.ctrlRekeningKegiatan1.Size = new System.Drawing.Size(720, 220);
            this.ctrlRekeningKegiatan1.TabIndex = 5;
            // 
            // frmTerimaSPM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 619);
            this.Controls.Add(this.ctrlRekeningKegiatan1);
            this.Controls.Add(this.txtKeterangan);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlSPP1);
            this.Controls.Add(this.ctrlDinas1);
            this.Name = "frmTerimaSPM";
            this.Text = "frmTerimaSPM";
            this.Load += new System.EventHandler(this.frmTerimaSPM_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlDinas ctrlDinas1;
        private ctrlSPP ctrlSPP1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.TextBox txtKeterangan;
        private ctrlRekeningKegiatan ctrlRekeningKegiatan1;
    }
}