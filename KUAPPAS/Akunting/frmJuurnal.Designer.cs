namespace KUAPPAS.Akunting
{
    partial class frmJuurnal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmJuurnal));
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.ctrlNavigation1 = new KUAPPAS.ctrlNavigation();
            this.txtNoBukti = new System.Windows.Forms.TextBox();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.ctrlTanggal1 = new KUAPPAS.ctrlTanggal();
            this.txtIDRekening = new System.Windows.Forms.TextBox();
            this.txtNamaRekening = new System.Windows.Forms.TextBox();
            this.cmdCari = new System.Windows.Forms.Button();
            this.optDebet = new System.Windows.Forms.RadioButton();
            this.optKredit = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.ctrlJurnalRekening1 = new KUAPPAS.Akunting.ctrlDaftarRekeningJurnal();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.cmdTambahkan = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "O P D";
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlDinas1.Location = new System.Drawing.Point(143, 49);
            this.ctrlDinas1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(559, 53);
            this.ctrlDinas1.TabIndex = 5;
            // 
            // ctrlNavigation1
            // 
            this.ctrlNavigation1.BackColor = System.Drawing.Color.DimGray;
            this.ctrlNavigation1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlNavigation1.Location = new System.Drawing.Point(0, 0);
            this.ctrlNavigation1.Name = "ctrlNavigation1";
            this.ctrlNavigation1.Size = new System.Drawing.Size(746, 35);
            this.ctrlNavigation1.TabIndex = 4;
            this.ctrlNavigation1.OnAdd += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnAdd);
            this.ctrlNavigation1.OnSave += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnSave);
            this.ctrlNavigation1.Load += new System.EventHandler(this.ctrlNavigation1_Load);
            // 
            // txtNoBukti
            // 
            this.txtNoBukti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoBukti.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoBukti.Location = new System.Drawing.Point(142, 101);
            this.txtNoBukti.Name = "txtNoBukti";
            this.txtNoBukti.Size = new System.Drawing.Size(560, 22);
            this.txtNoBukti.TabIndex = 7;
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKeterangan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKeterangan.Location = new System.Drawing.Point(143, 127);
            this.txtKeterangan.Multiline = true;
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(559, 33);
            this.txtKeterangan.TabIndex = 8;
            this.txtKeterangan.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // ctrlTanggal1
            // 
            this.ctrlTanggal1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlTanggal1.Location = new System.Drawing.Point(143, 163);
            this.ctrlTanggal1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlTanggal1.Name = "ctrlTanggal1";
            this.ctrlTanggal1.Size = new System.Drawing.Size(348, 23);
            this.ctrlTanggal1.TabIndex = 9;
            this.ctrlTanggal1.Tanggal = new System.DateTime(2024, 9, 12, 0, 0, 0, 0);
            // 
            // txtIDRekening
            // 
            this.txtIDRekening.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIDRekening.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIDRekening.Location = new System.Drawing.Point(143, 194);
            this.txtIDRekening.Name = "txtIDRekening";
            this.txtIDRekening.Size = new System.Drawing.Size(298, 22);
            this.txtIDRekening.TabIndex = 10;
            // 
            // txtNamaRekening
            // 
            this.txtNamaRekening.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNamaRekening.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNamaRekening.Location = new System.Drawing.Point(143, 220);
            this.txtNamaRekening.Multiline = true;
            this.txtNamaRekening.Name = "txtNamaRekening";
            this.txtNamaRekening.ReadOnly = true;
            this.txtNamaRekening.Size = new System.Drawing.Size(559, 50);
            this.txtNamaRekening.TabIndex = 11;
            // 
            // cmdCari
            // 
            this.cmdCari.Location = new System.Drawing.Point(447, 193);
            this.cmdCari.Name = "cmdCari";
            this.cmdCari.Size = new System.Drawing.Size(44, 20);
            this.cmdCari.TabIndex = 12;
            this.cmdCari.Text = "Cari";
            this.cmdCari.UseVisualStyleBackColor = true;
            this.cmdCari.Click += new System.EventHandler(this.cmdCari_Click);
            // 
            // optDebet
            // 
            this.optDebet.AutoSize = true;
            this.optDebet.Checked = true;
            this.optDebet.Location = new System.Drawing.Point(143, 286);
            this.optDebet.Name = "optDebet";
            this.optDebet.Size = new System.Drawing.Size(54, 17);
            this.optDebet.TabIndex = 13;
            this.optDebet.TabStop = true;
            this.optDebet.Text = "Debet";
            this.optDebet.UseVisualStyleBackColor = true;
            // 
            // optKredit
            // 
            this.optKredit.AutoSize = true;
            this.optKredit.Location = new System.Drawing.Point(213, 286);
            this.optKredit.Name = "optKredit";
            this.optKredit.Size = new System.Drawing.Size(52, 17);
            this.optKredit.TabIndex = 14;
            this.optKredit.Text = "Kredit";
            this.optKredit.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "No Bukti";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(35, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "Keterangan";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(35, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "Tanggal";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(35, 194);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "Kode Rekening";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(35, 223);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 16);
            this.label6.TabIndex = 19;
            this.label6.Text = "Nama Rekening";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(35, 290);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 16);
            this.label7.TabIndex = 20;
            this.label7.Text = "Debet /Kredit ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(35, 317);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 16);
            this.label8.TabIndex = 21;
            this.label8.Text = "Jumlah";
            // 
            // txtJumlah
            // 
            this.txtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlah.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlah.Location = new System.Drawing.Point(143, 313);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(298, 22);
            this.txtJumlah.TabIndex = 22;
            // 
            // ctrlJurnalRekening1
            // 
            this.ctrlJurnalRekening1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlJurnalRekening1.Judul = "";
          //  this.ctrlJurnalRekening1.ListJrnalRekening = ((System.Collections.Generic.List<DTO.Akuntansi.JurnalRekeningShow>)(resources.GetObject("ctrlJurnalRekening1.ListJrnalRekening")));
            this.ctrlJurnalRekening1.Location = new System.Drawing.Point(0, 375);
            this.ctrlJurnalRekening1.Name = "ctrlJurnalRekening1";
            this.ctrlJurnalRekening1.Size = new System.Drawing.Size(744, 173);
            this.ctrlJurnalRekening1.TabIndex = 23;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 559);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(746, 22);
            this.statusStrip1.TabIndex = 24;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // cmdTambahkan
            // 
            this.cmdTambahkan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdTambahkan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTambahkan.Location = new System.Drawing.Point(145, 341);
            this.cmdTambahkan.Name = "cmdTambahkan";
            this.cmdTambahkan.Size = new System.Drawing.Size(296, 28);
            this.cmdTambahkan.TabIndex = 25;
            this.cmdTambahkan.Text = "Tambahkan ke Daftar";
            this.cmdTambahkan.UseVisualStyleBackColor = true;
            this.cmdTambahkan.Click += new System.EventHandler(this.cmdTambahkan_Click);
            // 
            // frmJuurnal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 581);
            this.Controls.Add(this.cmdTambahkan);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ctrlJurnalRekening1);
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
            this.Controls.Add(this.ctrlTanggal1);
            this.Controls.Add(this.txtKeterangan);
            this.Controls.Add(this.txtNoBukti);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.ctrlNavigation1);
            this.Name = "frmJuurnal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmJuurnal";
            this.Load += new System.EventHandler(this.frmJuurnal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private ctrlDinas ctrlDinas1;
        private ctrlNavigation ctrlNavigation1;
        private System.Windows.Forms.TextBox txtNoBukti;
        private System.Windows.Forms.TextBox txtKeterangan;
        private ctrlTanggal ctrlTanggal1;
        private System.Windows.Forms.TextBox txtIDRekening;
        private System.Windows.Forms.TextBox txtNamaRekening;
        private System.Windows.Forms.Button cmdCari;
        private System.Windows.Forms.RadioButton optDebet;
        private System.Windows.Forms.RadioButton optKredit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtJumlah;
        private ctrlDaftarRekeningJurnal ctrlJurnalRekening1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button cmdTambahkan;
    }
}