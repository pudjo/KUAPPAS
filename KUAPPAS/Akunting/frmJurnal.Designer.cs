namespace KUAPPAS.Akunting
{
    partial class frmJurnal
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
            this.cmdTambahkan = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.optKredit = new System.Windows.Forms.RadioButton();
            this.optDebet = new System.Windows.Forms.RadioButton();
            this.cmdCari = new System.Windows.Forms.Button();
            this.txtNamaRekening = new System.Windows.Forms.TextBox();
            this.txtIDRekening = new System.Windows.Forms.TextBox();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.txtNoBukti = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlNavigation1 = new KUAPPAS.ctrlNavigation();
            this.ctrlDaftarRekeningJurnal1 = new KUAPPAS.Akunting.ctrlDaftarRekeningJurnal();
            this.ctrlTanggal1 = new KUAPPAS.ctrlTanggal();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.ctrlJenisJurnalPenyesuaian1 = new KUAPPAS.Akunting.ctrlJenisJurnalPenyesuaian();
            this.lblJenis = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdTambahkan
            // 
            this.cmdTambahkan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdTambahkan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTambahkan.Location = new System.Drawing.Point(148, 325);
            this.cmdTambahkan.Name = "cmdTambahkan";
            this.cmdTambahkan.Size = new System.Drawing.Size(296, 28);
            this.cmdTambahkan.TabIndex = 43;
            this.cmdTambahkan.Text = "Tambahkan ke Daftar";
            this.cmdTambahkan.UseVisualStyleBackColor = true;
            this.cmdTambahkan.Click += new System.EventHandler(this.cmdTambahkan_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 575);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(700, 22);
            this.statusStrip1.TabIndex = 42;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtJumlah
            // 
            this.txtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlah.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlah.Location = new System.Drawing.Point(146, 297);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(298, 22);
            this.txtJumlah.TabIndex = 41;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(20, 301);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 16);
            this.label8.TabIndex = 40;
            this.label8.Text = "Jumlah";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(20, 277);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 16);
            this.label7.TabIndex = 39;
            this.label7.Text = "Debet /Kredit ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(20, 235);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 16);
            this.label6.TabIndex = 38;
            this.label6.Text = "Nama Rekening";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 16);
            this.label5.TabIndex = 37;
            this.label5.Text = "Kode Rekening";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(20, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 16);
            this.label4.TabIndex = 36;
            this.label4.Text = "Tanggal";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 16);
            this.label3.TabIndex = 35;
            this.label3.Text = "Keterangan";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 16);
            this.label2.TabIndex = 34;
            this.label2.Text = "No Bukti";
            // 
            // optKredit
            // 
            this.optKredit.AutoSize = true;
            this.optKredit.Location = new System.Drawing.Point(216, 278);
            this.optKredit.Name = "optKredit";
            this.optKredit.Size = new System.Drawing.Size(52, 17);
            this.optKredit.TabIndex = 33;
            this.optKredit.Text = "Kredit";
            this.optKredit.UseVisualStyleBackColor = true;
            // 
            // optDebet
            // 
            this.optDebet.AutoSize = true;
            this.optDebet.Checked = true;
            this.optDebet.Location = new System.Drawing.Point(146, 278);
            this.optDebet.Name = "optDebet";
            this.optDebet.Size = new System.Drawing.Size(54, 17);
            this.optDebet.TabIndex = 32;
            this.optDebet.TabStop = true;
            this.optDebet.Text = "Debet";
            this.optDebet.UseVisualStyleBackColor = true;
            // 
            // cmdCari
            // 
            this.cmdCari.Location = new System.Drawing.Point(433, 205);
            this.cmdCari.Name = "cmdCari";
            this.cmdCari.Size = new System.Drawing.Size(44, 20);
            this.cmdCari.TabIndex = 31;
            this.cmdCari.Text = "Cari";
            this.cmdCari.UseVisualStyleBackColor = true;
            this.cmdCari.Click += new System.EventHandler(this.cmdCari_Click);
            // 
            // txtNamaRekening
            // 
            this.txtNamaRekening.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNamaRekening.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNamaRekening.Location = new System.Drawing.Point(146, 232);
            this.txtNamaRekening.Multiline = true;
            this.txtNamaRekening.Name = "txtNamaRekening";
            this.txtNamaRekening.ReadOnly = true;
            this.txtNamaRekening.Size = new System.Drawing.Size(547, 36);
            this.txtNamaRekening.TabIndex = 30;
            // 
            // txtIDRekening
            // 
            this.txtIDRekening.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIDRekening.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIDRekening.Location = new System.Drawing.Point(146, 206);
            this.txtIDRekening.Name = "txtIDRekening";
            this.txtIDRekening.Size = new System.Drawing.Size(298, 22);
            this.txtIDRekening.TabIndex = 29;
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKeterangan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKeterangan.Location = new System.Drawing.Point(146, 144);
            this.txtKeterangan.Multiline = true;
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(547, 33);
            this.txtKeterangan.TabIndex = 28;
            // 
            // txtNoBukti
            // 
            this.txtNoBukti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoBukti.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoBukti.Location = new System.Drawing.Point(145, 121);
            this.txtNoBukti.Name = "txtNoBukti";
            this.txtNoBukti.Size = new System.Drawing.Size(547, 22);
            this.txtNoBukti.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "O P D";
            // 
            // ctrlNavigation1
            // 
            this.ctrlNavigation1.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.ctrlNavigation1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlNavigation1.Location = new System.Drawing.Point(0, 0);
            this.ctrlNavigation1.Name = "ctrlNavigation1";
            this.ctrlNavigation1.Size = new System.Drawing.Size(700, 35);
            this.ctrlNavigation1.TabIndex = 44;
            this.ctrlNavigation1.OnAdd += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnAdd);
            this.ctrlNavigation1.OnSave += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnSave);
            this.ctrlNavigation1.OnDelete += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnDelete);
            this.ctrlNavigation1.Load += new System.EventHandler(this.ctrlNavigation1_Load);
            // 
            // ctrlDaftarRekeningJurnal1
            // 
            this.ctrlDaftarRekeningJurnal1.Judul = "";
            this.ctrlDaftarRekeningJurnal1.Location = new System.Drawing.Point(0, 371);
            this.ctrlDaftarRekeningJurnal1.Name = "ctrlDaftarRekeningJurnal1";
            this.ctrlDaftarRekeningJurnal1.Size = new System.Drawing.Size(708, 196);
            this.ctrlDaftarRekeningJurnal1.TabIndex = 45;
            this.ctrlDaftarRekeningJurnal1.Load += new System.EventHandler(this.ctrlDaftarRekeningJurnal1_Load);
            // 
            // ctrlTanggal1
            // 
            this.ctrlTanggal1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlTanggal1.Location = new System.Drawing.Point(148, 181);
            this.ctrlTanggal1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlTanggal1.Name = "ctrlTanggal1";
            this.ctrlTanggal1.Size = new System.Drawing.Size(348, 23);
            this.ctrlTanggal1.TabIndex = 46;
            this.ctrlTanggal1.Tanggal = new System.DateTime(2024, 9, 12, 0, 0, 0, 0);
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlDinas1.Location = new System.Drawing.Point(145, 43);
            this.ctrlDinas1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(547, 53);
            this.ctrlDinas1.TabIndex = 47;
            this.ctrlDinas1.UK = 0;
            // 
            // ctrlJenisJurnalPenyesuaian1
            // 
            this.ctrlJenisJurnalPenyesuaian1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlJenisJurnalPenyesuaian1.Location = new System.Drawing.Point(145, 95);
            this.ctrlJenisJurnalPenyesuaian1.Name = "ctrlJenisJurnalPenyesuaian1";
            this.ctrlJenisJurnalPenyesuaian1.Size = new System.Drawing.Size(547, 22);
            this.ctrlJenisJurnalPenyesuaian1.TabIndex = 48;
            // 
            // lblJenis
            // 
            this.lblJenis.AutoSize = true;
            this.lblJenis.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJenis.Location = new System.Drawing.Point(20, 93);
            this.lblJenis.Name = "lblJenis";
            this.lblJenis.Size = new System.Drawing.Size(122, 16);
            this.lblJenis.TabIndex = 49;
            this.lblJenis.Text = "Jenis Penyesuaian";
            // 
            // frmJurnal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 597);
            this.Controls.Add(this.lblJenis);
            this.Controls.Add(this.ctrlJenisJurnalPenyesuaian1);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.ctrlTanggal1);
            this.Controls.Add(this.ctrlDaftarRekeningJurnal1);
            this.Controls.Add(this.ctrlNavigation1);
            this.Controls.Add(this.cmdTambahkan);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.optKredit);
            this.Controls.Add(this.optDebet);
            this.Controls.Add(this.cmdCari);
            this.Controls.Add(this.txtNamaRekening);
            this.Controls.Add(this.txtIDRekening);
            this.Controls.Add(this.txtKeterangan);
            this.Controls.Add(this.txtNoBukti);
            this.Controls.Add(this.label1);
            this.Name = "frmJurnal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Penjurnalan";
            this.Load += new System.EventHandler(this.frmJurnal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdTambahkan;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton optKredit;
        private System.Windows.Forms.RadioButton optDebet;
        private System.Windows.Forms.Button cmdCari;
        private System.Windows.Forms.TextBox txtNamaRekening;
        private System.Windows.Forms.TextBox txtIDRekening;
        private System.Windows.Forms.TextBox txtKeterangan;
        private System.Windows.Forms.TextBox txtNoBukti;
        private System.Windows.Forms.Label label1;
        private ctrlNavigation ctrlNavigation1;
        private ctrlDaftarRekeningJurnal ctrlDaftarRekeningJurnal1;
        private ctrlTanggal ctrlTanggal1;
        private ctrlDinas ctrlDinas1;
        private ctrlJenisJurnalPenyesuaian ctrlJenisJurnalPenyesuaian1;
        private System.Windows.Forms.Label lblJenis;
    }
}