namespace KUAPPAS.Bendahara
{
    partial class frmBAST
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
            this.ctrlNavigation1 = new KUAPPAS.ctrlNavigation();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNoBAST = new System.Windows.Forms.TextBox();
            this.txtKeteranganBAST = new System.Windows.Forms.TextBox();
            this.txtKeteranganKontrak = new System.Windows.Forms.TextBox();
            this.ctrlKontrak1 = new KUAPPAS.Bendahara.ctrlKontrak();
            this.TanggalBAST = new KUAPPAS.ctrlTanggal();
            this.TanggalKontrak = new KUAPPAS.ctrlTanggal();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.ctrlProgramKegiatan1 = new KUAPPAS.ctrlProgramKegiatan();
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
            this.ctrlFooter1 = new KUAPPAS.ctrlFooter();
            this.cmdCari = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlNavigation1
            // 
            this.ctrlNavigation1.BackColor = System.Drawing.Color.DimGray;
            this.ctrlNavigation1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlNavigation1.Location = new System.Drawing.Point(0, 0);
            this.ctrlNavigation1.Name = "ctrlNavigation1";
            this.ctrlNavigation1.Size = new System.Drawing.Size(912, 35);
            this.ctrlNavigation1.TabIndex = 1;
            this.ctrlNavigation1.OnAdd += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnAdd);
            this.ctrlNavigation1.OnSave += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnSave);
            this.ctrlNavigation1.OnDelete += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnDelete);
            this.ctrlNavigation1.Load += new System.EventHandler(this.ctrlNavigation1_Load);
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlDinas1.Location = new System.Drawing.Point(181, 40);
            this.ctrlDinas1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(698, 53);
            this.ctrlDinas1.TabIndex = 2;
            this.ctrlDinas1.OnChanged += new KUAPPAS.ctrlDinas.ValueChangedEventHandler(this.ctrlDinas1_OnChanged);
            this.ctrlDinas1.Load += new System.EventHandler(this.ctrlDinas1_Load_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "O P D";
            // 
            // txtNoBAST
            // 
            this.txtNoBAST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoBAST.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoBAST.Location = new System.Drawing.Point(181, 99);
            this.txtNoBAST.Name = "txtNoBAST";
            this.txtNoBAST.Size = new System.Drawing.Size(698, 22);
            this.txtNoBAST.TabIndex = 4;
            // 
            // txtKeteranganBAST
            // 
            this.txtKeteranganBAST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKeteranganBAST.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKeteranganBAST.Location = new System.Drawing.Point(181, 125);
            this.txtKeteranganBAST.Multiline = true;
            this.txtKeteranganBAST.Name = "txtKeteranganBAST";
            this.txtKeteranganBAST.Size = new System.Drawing.Size(698, 73);
            this.txtKeteranganBAST.TabIndex = 5;
            // 
            // txtKeteranganKontrak
            // 
            this.txtKeteranganKontrak.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKeteranganKontrak.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKeteranganKontrak.Location = new System.Drawing.Point(181, 253);
            this.txtKeteranganKontrak.Multiline = true;
            this.txtKeteranganKontrak.Name = "txtKeteranganKontrak";
            this.txtKeteranganKontrak.Size = new System.Drawing.Size(698, 52);
            this.txtKeteranganKontrak.TabIndex = 6;
            // 
            // ctrlKontrak1
            // 
            this.ctrlKontrak1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlKontrak1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlKontrak1.Location = new System.Drawing.Point(181, 226);
            this.ctrlKontrak1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlKontrak1.Name = "ctrlKontrak1";
            this.ctrlKontrak1.Size = new System.Drawing.Size(393, 23);
            this.ctrlKontrak1.TabIndex = 7;
            this.ctrlKontrak1.OnChanged += new KUAPPAS.Bendahara.ctrlKontrak.ValueChangedEventHandler(this.ctrlKontrak1_OnChanged);
            this.ctrlKontrak1.Load += new System.EventHandler(this.ctrlKontrak1_Load);
            // 
            // TanggalBAST
            // 
            this.TanggalBAST.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TanggalBAST.Location = new System.Drawing.Point(181, 201);
            this.TanggalBAST.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TanggalBAST.Name = "TanggalBAST";
            this.TanggalBAST.Size = new System.Drawing.Size(698, 22);
            this.TanggalBAST.TabIndex = 8;
            this.TanggalBAST.Tanggal = new System.DateTime(2023, 11, 25, 0, 0, 0, 0);
            this.TanggalBAST.OnChanged += new KUAPPAS.ctrlTanggal.ValueChangedEventHandler(this.TanggalBAST_OnChanged);
            this.TanggalBAST.Load += new System.EventHandler(this.TanggalBAST_Load);
            // 
            // TanggalKontrak
            // 
            this.TanggalKontrak.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TanggalKontrak.Location = new System.Drawing.Point(759, 226);
            this.TanggalKontrak.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TanggalKontrak.Name = "TanggalKontrak";
            this.TanggalKontrak.Size = new System.Drawing.Size(120, 24);
            this.TanggalKontrak.TabIndex = 9;
            this.TanggalKontrak.Tanggal = new System.DateTime(2023, 11, 25, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "No BAST";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Keteragan BAST";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Tanggal BAST";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 226);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Kontrak/SPK";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(666, 227);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Tanggal Kontrak";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(44, 253);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Keterangan Kontrak";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(-6, 313);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(916, 327);
            this.tabControl1.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.ctrlProgramKegiatan1);
            this.tabPage1.Controls.Add(this.ctrlRekeningKegiatan1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(908, 301);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Program Kegiatan Rekening";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(46, 84);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 13);
            this.label15.TabIndex = 16;
            this.label15.Text = "Sub Kegiatan";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(46, 56);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 13);
            this.label14.TabIndex = 15;
            this.label14.Text = "Kegiatan";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(46, 28);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(46, 13);
            this.label13.TabIndex = 14;
            this.label13.Text = "Program";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(44, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(109, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "Urusan Pemerintahan";
            // 
            // ctrlProgramKegiatan1
            // 
            this.ctrlProgramKegiatan1.Location = new System.Drawing.Point(183, 0);
            this.ctrlProgramKegiatan1.Name = "ctrlProgramKegiatan1";
            this.ctrlProgramKegiatan1.Size = new System.Drawing.Size(698, 114);
            this.ctrlProgramKegiatan1.TabIndex = 5;
            // 
            // ctrlRekeningKegiatan1
            // 
            this.ctrlRekeningKegiatan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlRekeningKegiatan1.Location = new System.Drawing.Point(75, 120);
            this.ctrlRekeningKegiatan1.Name = "ctrlRekeningKegiatan1";
            this.ctrlRekeningKegiatan1.Size = new System.Drawing.Size(806, 157);
            this.ctrlRekeningKegiatan1.TabIndex = 4;
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
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(908, 301);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Pihak ke tiga";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ctrlPerusahaan1
            // 
            this.ctrlPerusahaan1.Location = new System.Drawing.Point(197, 17);
            this.ctrlPerusahaan1.Name = "ctrlPerusahaan1";
            this.ctrlPerusahaan1.Size = new System.Drawing.Size(617, 259);
            this.ctrlPerusahaan1.TabIndex = 82;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(46, 205);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(145, 13);
            this.label25.TabIndex = 81;
            this.label25.Text = "Nama Dalam Rekening Bank";
            // 
            // lblNoRekeningPihakIII
            // 
            this.lblNoRekeningPihakIII.AutoSize = true;
            this.lblNoRekeningPihakIII.Location = new System.Drawing.Point(46, 184);
            this.lblNoRekeningPihakIII.Name = "lblNoRekeningPihakIII";
            this.lblNoRekeningPihakIII.Size = new System.Drawing.Size(87, 13);
            this.lblNoRekeningPihakIII.TabIndex = 80;
            this.lblNoRekeningPihakIII.Text = "Nomor Rekening";
            // 
            // lblBankPihakIII
            // 
            this.lblBankPihakIII.AutoSize = true;
            this.lblBankPihakIII.Location = new System.Drawing.Point(46, 165);
            this.lblBankPihakIII.Name = "lblBankPihakIII";
            this.lblBankPihakIII.Size = new System.Drawing.Size(32, 13);
            this.lblBankPihakIII.TabIndex = 79;
            this.lblBankPihakIII.Text = "Bank";
            // 
            // lblNPWP
            // 
            this.lblNPWP.AutoSize = true;
            this.lblNPWP.Location = new System.Drawing.Point(46, 141);
            this.lblNPWP.Name = "lblNPWP";
            this.lblNPWP.Size = new System.Drawing.Size(40, 13);
            this.lblNPWP.TabIndex = 78;
            this.lblNPWP.Text = "NPWP";
            // 
            // lblNamaPimpinan
            // 
            this.lblNamaPimpinan.AutoSize = true;
            this.lblNamaPimpinan.Location = new System.Drawing.Point(46, 117);
            this.lblNamaPimpinan.Name = "lblNamaPimpinan";
            this.lblNamaPimpinan.Size = new System.Drawing.Size(81, 13);
            this.lblNamaPimpinan.TabIndex = 77;
            this.lblNamaPimpinan.Text = "Nama Pimpinan";
            // 
            // lblAlamatPerusahaan
            // 
            this.lblAlamatPerusahaan.AutoSize = true;
            this.lblAlamatPerusahaan.Location = new System.Drawing.Point(46, 97);
            this.lblAlamatPerusahaan.Name = "lblAlamatPerusahaan";
            this.lblAlamatPerusahaan.Size = new System.Drawing.Size(39, 13);
            this.lblAlamatPerusahaan.TabIndex = 76;
            this.lblAlamatPerusahaan.Text = "Alamat";
            // 
            // lblBentukPerusahaan
            // 
            this.lblBentukPerusahaan.AutoSize = true;
            this.lblBentukPerusahaan.Location = new System.Drawing.Point(46, 74);
            this.lblBentukPerusahaan.Name = "lblBentukPerusahaan";
            this.lblBentukPerusahaan.Size = new System.Drawing.Size(101, 13);
            this.lblBentukPerusahaan.TabIndex = 75;
            this.lblBentukPerusahaan.Text = "Bentuk Perusahaan";
            // 
            // lblPerusahaan
            // 
            this.lblPerusahaan.AutoSize = true;
            this.lblPerusahaan.Location = new System.Drawing.Point(46, 54);
            this.lblPerusahaan.Name = "lblPerusahaan";
            this.lblPerusahaan.Size = new System.Drawing.Size(95, 13);
            this.lblPerusahaan.TabIndex = 74;
            this.lblPerusahaan.Text = "Nama Perusahaan";
            // 
            // ctrlFooter1
            // 
            this.ctrlFooter1.BackColor = System.Drawing.Color.Silver;
            this.ctrlFooter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctrlFooter1.Location = new System.Drawing.Point(0, 653);
            this.ctrlFooter1.Name = "ctrlFooter1";
            this.ctrlFooter1.Size = new System.Drawing.Size(912, 21);
            this.ctrlFooter1.TabIndex = 17;
            // 
            // cmdCari
            // 
            this.cmdCari.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCari.Location = new System.Drawing.Point(581, 225);
            this.cmdCari.Name = "cmdCari";
            this.cmdCari.Size = new System.Drawing.Size(75, 23);
            this.cmdCari.TabIndex = 18;
            this.cmdCari.Text = "Cari";
            this.cmdCari.UseVisualStyleBackColor = true;
            this.cmdCari.Click += new System.EventHandler(this.cmdCari_Click);
            // 
            // frmBAST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 674);
            this.Controls.Add(this.cmdCari);
            this.Controls.Add(this.ctrlFooter1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TanggalKontrak);
            this.Controls.Add(this.TanggalBAST);
            this.Controls.Add(this.ctrlKontrak1);
            this.Controls.Add(this.txtKeteranganKontrak);
            this.Controls.Add(this.txtKeteranganBAST);
            this.Controls.Add(this.txtNoBAST);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.ctrlNavigation1);
            this.Name = "frmBAST";
            this.Text = "B A S T";
            this.Load += new System.EventHandler(this.frmBAST_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlNavigation ctrlNavigation1;
        private ctrlDinas ctrlDinas1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNoBAST;
        private System.Windows.Forms.TextBox txtKeteranganBAST;
        private System.Windows.Forms.TextBox txtKeteranganKontrak;
        private ctrlKontrak ctrlKontrak1;
        private ctrlTanggal TanggalBAST;
        private ctrlTanggal TanggalKontrak;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private ctrlRekeningKegiatan ctrlRekeningKegiatan1;
        private ctrlProgramKegiatan ctrlProgramKegiatan1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lblNoRekeningPihakIII;
        private System.Windows.Forms.Label lblBankPihakIII;
        private System.Windows.Forms.Label lblNPWP;
        private System.Windows.Forms.Label lblNamaPimpinan;
        private System.Windows.Forms.Label lblAlamatPerusahaan;
        private System.Windows.Forms.Label lblBentukPerusahaan;
        private System.Windows.Forms.Label lblPerusahaan;
        private ctrlPerusahaan ctrlPerusahaan1;
        private ctrlFooter ctrlFooter1;
        private System.Windows.Forms.Button cmdCari;
    }
}