namespace KUAPPAS.Bendahara
{
    partial class frmKontrak
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
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ctrlProgramKegiatan1 = new KUAPPAS.ctrlProgramKegiatan();
            this.label2 = new System.Windows.Forms.Label();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.ctrlRekeningKegiatan1 = new KUAPPAS.ctrlRekeningKegiatan();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ctrlPerusahaan1 = new KUAPPAS.Bendahara.ctrlPerusahaan();
            this.label25 = new System.Windows.Forms.Label();
            this.lblNoRekeningPihakIII = new System.Windows.Forms.Label();
            this.lblBankPihakIII = new System.Windows.Forms.Label();
            this.lblNPWP = new System.Windows.Forms.Label();
            this.lblNamaPimpinan = new System.Windows.Forms.Label();
            this.lblAlamatPerusahaan = new System.Windows.Forms.Label();
            this.lblBentukPerusahaan = new System.Windows.Forms.Label();
            this.lblPerusahaan = new System.Windows.Forms.Label();
            this.txtNoKontrak = new System.Windows.Forms.TextBox();
            this.txtWaktuPelaksanaan = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlTanggal1 = new KUAPPAS.ctrlTanggal();
            this.ctrlNavigation1 = new KUAPPAS.ctrlNavigation();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.ctrlFooter1 = new KUAPPAS.ctrlFooter();
            this.ctrlPeriode1 = new KUAPPAS.Bendahara.ctrlPeriode();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKeterangan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKeterangan.Location = new System.Drawing.Point(179, 140);
            this.txtKeterangan.Multiline = true;
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(612, 64);
            this.txtKeterangan.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 267);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(835, 388);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ctrlProgramKegiatan1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtJumlah);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.ctrlRekeningKegiatan1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(827, 360);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Kegiatan dan Kode Rekening";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ctrlProgramKegiatan1
            // 
            this.ctrlProgramKegiatan1.Location = new System.Drawing.Point(155, 0);
            this.ctrlProgramKegiatan1.Name = "ctrlProgramKegiatan1";
            this.ctrlProgramKegiatan1.Size = new System.Drawing.Size(643, 117);
            this.ctrlProgramKegiatan1.TabIndex = 15;
            this.ctrlProgramKegiatan1.OnChanged += new KUAPPAS.ctrlProgramKegiatan.ValueChangedEventHandler(this.ctrlProgramKegiatan1_OnChanged);
            this.ctrlProgramKegiatan1.Load += new System.EventHandler(this.ctrlProgramKegiatan1_Load);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(486, 325);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "Jumlah";
            // 
            // txtJumlah
            // 
            this.txtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlah.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlah.Location = new System.Drawing.Point(564, 319);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(226, 22);
            this.txtJumlah.TabIndex = 13;
            this.txtJumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(19, 88);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(81, 15);
            this.label15.TabIndex = 12;
            this.label15.Text = "Sub Kegiatan";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(19, 60);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 15);
            this.label14.TabIndex = 9;
            this.label14.Text = "Kegiatan";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(19, 32);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(55, 15);
            this.label13.TabIndex = 8;
            this.label13.Text = "Program";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(17, 8);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(128, 15);
            this.label12.TabIndex = 7;
            this.label12.Text = "Urusan Pemerintahan";
            // 
            // ctrlRekeningKegiatan1
            // 
            this.ctrlRekeningKegiatan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlRekeningKegiatan1.Location = new System.Drawing.Point(28, 129);
            this.ctrlRekeningKegiatan1.Name = "ctrlRekeningKegiatan1";
            this.ctrlRekeningKegiatan1.Size = new System.Drawing.Size(771, 184);
            this.ctrlRekeningKegiatan1.TabIndex = 6;
            this.ctrlRekeningKegiatan1.OnChanged += new KUAPPAS.ctrlRekeningKegiatan.ValueChangedEventHandler(this.ctrlRekeningKegiatan1_OnChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ctrlPerusahaan1);
            this.tabPage2.Controls.Add(this.label25);
            this.tabPage2.Controls.Add(this.lblNoRekeningPihakIII);
            this.tabPage2.Controls.Add(this.lblBankPihakIII);
            this.tabPage2.Controls.Add(this.lblNPWP);
            this.tabPage2.Controls.Add(this.lblNamaPimpinan);
            this.tabPage2.Controls.Add(this.lblAlamatPerusahaan);
            this.tabPage2.Controls.Add(this.lblBentukPerusahaan);
            this.tabPage2.Controls.Add(this.lblPerusahaan);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(827, 360);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Pihak Ketiga";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ctrlPerusahaan1
            // 
            this.ctrlPerusahaan1.Location = new System.Drawing.Point(180, 3);
            this.ctrlPerusahaan1.Name = "ctrlPerusahaan1";
            this.ctrlPerusahaan1.Size = new System.Drawing.Size(617, 310);
            this.ctrlPerusahaan1.TabIndex = 74;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(14, 219);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(168, 15);
            this.label25.TabIndex = 73;
            this.label25.Text = "Nama Dalam Rekening Bank";
            // 
            // lblNoRekeningPihakIII
            // 
            this.lblNoRekeningPihakIII.AutoSize = true;
            this.lblNoRekeningPihakIII.Location = new System.Drawing.Point(11, 194);
            this.lblNoRekeningPihakIII.Name = "lblNoRekeningPihakIII";
            this.lblNoRekeningPihakIII.Size = new System.Drawing.Size(101, 15);
            this.lblNoRekeningPihakIII.TabIndex = 72;
            this.lblNoRekeningPihakIII.Text = "Nomor Rekening";
            // 
            // lblBankPihakIII
            // 
            this.lblBankPihakIII.AutoSize = true;
            this.lblBankPihakIII.Location = new System.Drawing.Point(14, 168);
            this.lblBankPihakIII.Name = "lblBankPihakIII";
            this.lblBankPihakIII.Size = new System.Drawing.Size(35, 15);
            this.lblBankPihakIII.TabIndex = 71;
            this.lblBankPihakIII.Text = "Bank";
            // 
            // lblNPWP
            // 
            this.lblNPWP.AutoSize = true;
            this.lblNPWP.Location = new System.Drawing.Point(14, 144);
            this.lblNPWP.Name = "lblNPWP";
            this.lblNPWP.Size = new System.Drawing.Size(43, 15);
            this.lblNPWP.TabIndex = 70;
            this.lblNPWP.Text = "NPWP";
            // 
            // lblNamaPimpinan
            // 
            this.lblNamaPimpinan.AutoSize = true;
            this.lblNamaPimpinan.Location = new System.Drawing.Point(14, 115);
            this.lblNamaPimpinan.Name = "lblNamaPimpinan";
            this.lblNamaPimpinan.Size = new System.Drawing.Size(97, 15);
            this.lblNamaPimpinan.TabIndex = 69;
            this.lblNamaPimpinan.Text = "Nama Pimpinan";
            // 
            // lblAlamatPerusahaan
            // 
            this.lblAlamatPerusahaan.AutoSize = true;
            this.lblAlamatPerusahaan.Location = new System.Drawing.Point(14, 90);
            this.lblAlamatPerusahaan.Name = "lblAlamatPerusahaan";
            this.lblAlamatPerusahaan.Size = new System.Drawing.Size(45, 15);
            this.lblAlamatPerusahaan.TabIndex = 68;
            this.lblAlamatPerusahaan.Text = "Alamat";
            // 
            // lblBentukPerusahaan
            // 
            this.lblBentukPerusahaan.AutoSize = true;
            this.lblBentukPerusahaan.Location = new System.Drawing.Point(14, 65);
            this.lblBentukPerusahaan.Name = "lblBentukPerusahaan";
            this.lblBentukPerusahaan.Size = new System.Drawing.Size(115, 15);
            this.lblBentukPerusahaan.TabIndex = 67;
            this.lblBentukPerusahaan.Text = "Bentuk Perusahaan";
            // 
            // lblPerusahaan
            // 
            this.lblPerusahaan.AutoSize = true;
            this.lblPerusahaan.Location = new System.Drawing.Point(14, 39);
            this.lblPerusahaan.Name = "lblPerusahaan";
            this.lblPerusahaan.Size = new System.Drawing.Size(111, 15);
            this.lblPerusahaan.TabIndex = 66;
            this.lblPerusahaan.Text = "Nama Perusahaan";
            // 
            // txtNoKontrak
            // 
            this.txtNoKontrak.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoKontrak.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoKontrak.Location = new System.Drawing.Point(179, 91);
            this.txtNoKontrak.Name = "txtNoKontrak";
            this.txtNoKontrak.Size = new System.Drawing.Size(612, 22);
            this.txtNoKontrak.TabIndex = 5;
            // 
            // txtWaktuPelaksanaan
            // 
            this.txtWaktuPelaksanaan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWaktuPelaksanaan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWaktuPelaksanaan.Location = new System.Drawing.Point(179, 235);
            this.txtWaktuPelaksanaan.Name = "txtWaktuPelaksanaan";
            this.txtWaktuPelaksanaan.Size = new System.Drawing.Size(612, 21);
            this.txtWaktuPelaksanaan.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "No Kontrak/SPK";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(26, 117);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 15);
            this.label9.TabIndex = 10;
            this.label9.Text = "Tanggal Kontrak";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(26, 146);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 15);
            this.label10.TabIndex = 11;
            this.label10.Text = "Keterangan";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(26, 237);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 15);
            this.label11.TabIndex = 12;
            this.label11.Text = "Waktu Pelaksanaan";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "OPD";
            // 
            // ctrlTanggal1
            // 
            this.ctrlTanggal1.Location = new System.Drawing.Point(179, 112);
            this.ctrlTanggal1.Name = "ctrlTanggal1";
            this.ctrlTanggal1.Size = new System.Drawing.Size(612, 25);
            this.ctrlTanggal1.TabIndex = 17;
            this.ctrlTanggal1.Tanggal = new System.DateTime(2023, 11, 21, 0, 0, 0, 0);
            // 
            // ctrlNavigation1
            // 
            this.ctrlNavigation1.BackColor = System.Drawing.Color.DimGray;
            this.ctrlNavigation1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlNavigation1.Location = new System.Drawing.Point(0, 0);
            this.ctrlNavigation1.Name = "ctrlNavigation1";
            this.ctrlNavigation1.Size = new System.Drawing.Size(832, 39);
            this.ctrlNavigation1.TabIndex = 13;
            this.ctrlNavigation1.OnAdd += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnAdd);
            this.ctrlNavigation1.OnSave += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnSave);
            this.ctrlNavigation1.OnDelete += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnDelete);
            this.ctrlNavigation1.Load += new System.EventHandler(this.ctrlNavigation1_Load);
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(179, 45);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(612, 46);
            this.ctrlDinas1.TabIndex = 7;
            this.ctrlDinas1.OnChanged += new KUAPPAS.ctrlDinas.ValueChangedEventHandler(this.ctrlDinas1_OnChanged);
            this.ctrlDinas1.Load += new System.EventHandler(this.ctrlDinas1_Load);
            // 
            // ctrlFooter1
            // 
            this.ctrlFooter1.BackColor = System.Drawing.Color.Silver;
            this.ctrlFooter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctrlFooter1.Location = new System.Drawing.Point(0, 666);
            this.ctrlFooter1.Name = "ctrlFooter1";
            this.ctrlFooter1.Size = new System.Drawing.Size(832, 21);
            this.ctrlFooter1.TabIndex = 18;
            // 
            // ctrlPeriode1
            // 
            this.ctrlPeriode1.Location = new System.Drawing.Point(178, 211);
            this.ctrlPeriode1.Margin = new System.Windows.Forms.Padding(0);
            this.ctrlPeriode1.Name = "ctrlPeriode1";
            this.ctrlPeriode1.Size = new System.Drawing.Size(284, 26);
            this.ctrlPeriode1.TabIndex = 19;
            this.ctrlPeriode1.TanggalAkhir = new System.DateTime(2024, 3, 10, 0, 0, 0, 0);
            this.ctrlPeriode1.TanggalAwaal = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 15);
            this.label3.TabIndex = 20;
            this.label3.Text = "Tanggal Pelaksanaan";
            // 
            // frmKontrak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(832, 687);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ctrlPeriode1);
            this.Controls.Add(this.ctrlFooter1);
            this.Controls.Add(this.ctrlTanggal1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlNavigation1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtWaktuPelaksanaan);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.txtNoKontrak);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtKeterangan);
            this.Name = "frmKontrak";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kontrak";
            this.Load += new System.EventHandler(this.frmKontrak_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtKeterangan;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtNoKontrak;
        private ctrlDinas ctrlDinas1;
        private ctrlRekeningKegiatan ctrlRekeningKegiatan1;
        private System.Windows.Forms.TextBox txtWaktuPelaksanaan;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private ctrlNavigation ctrlNavigation1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lblNoRekeningPihakIII;
        private System.Windows.Forms.Label lblBankPihakIII;
        private System.Windows.Forms.Label lblNPWP;
        private System.Windows.Forms.Label lblNamaPimpinan;
        private System.Windows.Forms.Label lblAlamatPerusahaan;
        private System.Windows.Forms.Label lblBentukPerusahaan;
        private System.Windows.Forms.Label lblPerusahaan;
        private ctrlPerusahaan ctrlPerusahaan1;
        private System.Windows.Forms.Label label1;
        private ctrlTanggal ctrlTanggal1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtJumlah;
        private ctrlProgramKegiatan ctrlProgramKegiatan1;
        private ctrlFooter ctrlFooter1;
        private ctrlPeriode ctrlPeriode1;
        private System.Windows.Forms.Label label3;
    }
}