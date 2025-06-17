namespace KUAPPAS.Bendahara
{
    partial class frmCekBKU
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmdCariLagi = new System.Windows.Forms.Button();
            this.cmdCari = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.lblPencarian = new System.Windows.Forms.Label();
            this.ctrlTanggalBulanVertikal1 = new KUAPPAS.Bendahara.ctrlTanggalBulanVertikal();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbSUmber = new System.Windows.Forms.ComboBox();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gridBKU2 = new System.Windows.Forms.DataGridView();
            this.NoUrut2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoBukti2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tanggal2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Keterangan2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jumlah2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hapus = new System.Windows.Forms.DataGridViewButtonColumn();
            this.gridBKU1 = new System.Windows.Forms.DataGridView();
            this.NoUrut1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoBukti1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tanggal1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Keteranngan1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nilaiu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BKU1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cmdPanggilBKU = new System.Windows.Forms.Button();
            this.lblJenisBendahara = new System.Windows.Forms.Label();
            this.txtJumlahTrx = new System.Windows.Forms.TextBox();
            this.txtJunlahBKU = new System.Windows.Forms.TextBox();
            this.txtJumlahBKUDetail = new System.Windows.Forms.TextBox();
            this.txtJumlahTrxDetail = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBKU2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBKU1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdCariLagi
            // 
            this.cmdCariLagi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCariLagi.Location = new System.Drawing.Point(924, 175);
            this.cmdCariLagi.Name = "cmdCariLagi";
            this.cmdCariLagi.Size = new System.Drawing.Size(75, 23);
            this.cmdCariLagi.TabIndex = 73;
            this.cmdCariLagi.Text = "Cari Lagi";
            this.cmdCariLagi.UseVisualStyleBackColor = true;
            // 
            // cmdCari
            // 
            this.cmdCari.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCari.Location = new System.Drawing.Point(856, 175);
            this.cmdCari.Name = "cmdCari";
            this.cmdCari.Size = new System.Drawing.Size(62, 23);
            this.cmdCari.TabIndex = 72;
            this.cmdCari.Text = "Cari...";
            this.cmdCari.UseVisualStyleBackColor = true;
            // 
            // txtCari
            // 
            this.txtCari.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCari.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCari.Location = new System.Drawing.Point(611, 175);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(239, 20);
            this.txtCari.TabIndex = 71;
            // 
            // lblPencarian
            // 
            this.lblPencarian.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPencarian.AutoSize = true;
            this.lblPencarian.Location = new System.Drawing.Point(550, 175);
            this.lblPencarian.Name = "lblPencarian";
            this.lblPencarian.Size = new System.Drawing.Size(55, 13);
            this.lblPencarian.TabIndex = 70;
            this.lblPencarian.Text = "Pencarian";
            // 
            // ctrlTanggalBulanVertikal1
            // 
            this.ctrlTanggalBulanVertikal1.Bulan = 5;
            this.ctrlTanggalBulanVertikal1.JenisPeriode = 1;
            this.ctrlTanggalBulanVertikal1.Location = new System.Drawing.Point(207, 53);
            this.ctrlTanggalBulanVertikal1.Name = "ctrlTanggalBulanVertikal1";
            this.ctrlTanggalBulanVertikal1.Size = new System.Drawing.Size(404, 70);
            this.ctrlTanggalBulanVertikal1.TabIndex = 68;
            this.ctrlTanggalBulanVertikal1.TanggalAkhir = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            this.ctrlTanggalBulanVertikal1.TanggalAwal = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(193, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 15);
            this.label9.TabIndex = 67;
            this.label9.Text = "OPD";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(253, 123);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 75;
            this.label11.Text = "Sumber";
            // 
            // cmbSUmber
            // 
            this.cmbSUmber.FormattingEnabled = true;
            this.cmbSUmber.Location = new System.Drawing.Point(306, 118);
            this.cmbSUmber.Name = "cmbSUmber";
            this.cmbSUmber.Size = new System.Drawing.Size(285, 21);
            this.cmbSUmber.TabIndex = 74;
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(237, 10);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(525, 43);
            this.ctrlDinas1.TabIndex = 76;
            this.ctrlDinas1.UK = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 249);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1013, 354);
            this.tabControl1.TabIndex = 77;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.gridBKU2);
            this.tabPage1.Controls.Add(this.gridBKU1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1005, 328);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Belum BKU";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Data Sampah";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Belum BKU";
            // 
            // gridBKU2
            // 
            this.gridBKU2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridBKU2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NoUrut2,
            this.NoBukti2,
            this.Tanggal2,
            this.Keterangan2,
            this.Jumlah2,
            this.Hapus});
            this.gridBKU2.Location = new System.Drawing.Point(9, 215);
            this.gridBKU2.Name = "gridBKU2";
            this.gridBKU2.Size = new System.Drawing.Size(993, 122);
            this.gridBKU2.TabIndex = 1;
            // 
            // NoUrut2
            // 
            this.NoUrut2.HeaderText = "NoUrut";
            this.NoUrut2.Name = "NoUrut2";
            this.NoUrut2.ReadOnly = true;
            // 
            // NoBukti2
            // 
            this.NoBukti2.HeaderText = "No Bukti";
            this.NoBukti2.Name = "NoBukti2";
            this.NoBukti2.ReadOnly = true;
            // 
            // Tanggal2
            // 
            this.Tanggal2.HeaderText = "Tanggal";
            this.Tanggal2.Name = "Tanggal2";
            this.Tanggal2.ReadOnly = true;
            // 
            // Keterangan2
            // 
            this.Keterangan2.HeaderText = "Keterangan";
            this.Keterangan2.Name = "Keterangan2";
            this.Keterangan2.ReadOnly = true;
            this.Keterangan2.Width = 400;
            // 
            // Jumlah2
            // 
            this.Jumlah2.HeaderText = "Jumlah";
            this.Jumlah2.Name = "Jumlah2";
            this.Jumlah2.ReadOnly = true;
            // 
            // Hapus
            // 
            this.Hapus.HeaderText = "Hapus";
            this.Hapus.Name = "Hapus";
            this.Hapus.ReadOnly = true;
            // 
            // gridBKU1
            // 
            this.gridBKU1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridBKU1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridBKU1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NoUrut1,
            this.NoBukti1,
            this.Tanggal1,
            this.Keteranngan1,
            this.Nilaiu,
            this.BKU1});
            this.gridBKU1.Location = new System.Drawing.Point(670, 31);
            this.gridBKU1.Name = "gridBKU1";
            this.gridBKU1.Size = new System.Drawing.Size(335, 145);
            this.gridBKU1.TabIndex = 0;
            // 
            // NoUrut1
            // 
            this.NoUrut1.HeaderText = "NoUrut";
            this.NoUrut1.Name = "NoUrut1";
            this.NoUrut1.ReadOnly = true;
            // 
            // NoBukti1
            // 
            this.NoBukti1.HeaderText = "Nomor Bukti";
            this.NoBukti1.Name = "NoBukti1";
            this.NoBukti1.ReadOnly = true;
            // 
            // Tanggal1
            // 
            this.Tanggal1.HeaderText = "Tanggal";
            this.Tanggal1.Name = "Tanggal1";
            this.Tanggal1.ReadOnly = true;
            // 
            // Keteranngan1
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Keteranngan1.DefaultCellStyle = dataGridViewCellStyle1;
            this.Keteranngan1.HeaderText = "Keterangan";
            this.Keteranngan1.Name = "Keteranngan1";
            this.Keteranngan1.ReadOnly = true;
            this.Keteranngan1.Width = 400;
            // 
            // Nilaiu
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Nilaiu.DefaultCellStyle = dataGridViewCellStyle2;
            this.Nilaiu.HeaderText = "Nilai";
            this.Nilaiu.Name = "Nilaiu";
            this.Nilaiu.ReadOnly = true;
            // 
            // BKU1
            // 
            this.BKU1.HeaderText = "BKU Kan";
            this.BKU1.Name = "BKU1";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1005, 328);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Lanjut";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cmdPanggilBKU
            // 
            this.cmdPanggilBKU.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdPanggilBKU.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPanggilBKU.Location = new System.Drawing.Point(237, 150);
            this.cmdPanggilBKU.Name = "cmdPanggilBKU";
            this.cmdPanggilBKU.Size = new System.Drawing.Size(126, 33);
            this.cmdPanggilBKU.TabIndex = 78;
            this.cmdPanggilBKU.Text = "Cek  BKU ";
            this.cmdPanggilBKU.UseVisualStyleBackColor = true;
            this.cmdPanggilBKU.Click += new System.EventHandler(this.cmdPanggilBKU_Click);
            // 
            // lblJenisBendahara
            // 
            this.lblJenisBendahara.AutoSize = true;
            this.lblJenisBendahara.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJenisBendahara.Location = new System.Drawing.Point(16, 10);
            this.lblJenisBendahara.Name = "lblJenisBendahara";
            this.lblJenisBendahara.Size = new System.Drawing.Size(47, 15);
            this.lblJenisBendahara.TabIndex = 79;
            this.lblJenisBendahara.Text = "label1";
            // 
            // txtJumlahTrx
            // 
            this.txtJumlahTrx.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlahTrx.Location = new System.Drawing.Point(117, 189);
            this.txtJumlahTrx.Name = "txtJumlahTrx";
            this.txtJumlahTrx.Size = new System.Drawing.Size(165, 26);
            this.txtJumlahTrx.TabIndex = 80;
            this.txtJumlahTrx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtJunlahBKU
            // 
            this.txtJunlahBKU.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJunlahBKU.Location = new System.Drawing.Point(342, 189);
            this.txtJunlahBKU.Name = "txtJunlahBKU";
            this.txtJunlahBKU.Size = new System.Drawing.Size(220, 26);
            this.txtJunlahBKU.TabIndex = 81;
            this.txtJunlahBKU.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtJumlahBKUDetail
            // 
            this.txtJumlahBKUDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlahBKUDetail.Location = new System.Drawing.Point(342, 221);
            this.txtJumlahBKUDetail.Name = "txtJumlahBKUDetail";
            this.txtJumlahBKUDetail.Size = new System.Drawing.Size(220, 26);
            this.txtJumlahBKUDetail.TabIndex = 83;
            this.txtJumlahBKUDetail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtJumlahTrxDetail
            // 
            this.txtJumlahTrxDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlahTrxDetail.Location = new System.Drawing.Point(117, 221);
            this.txtJumlahTrxDetail.Name = "txtJumlahTrxDetail";
            this.txtJumlahTrxDetail.Size = new System.Drawing.Size(165, 26);
            this.txtJumlahTrxDetail.TabIndex = 82;
            this.txtJumlahTrxDetail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // frmCekBKU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 615);
            this.Controls.Add(this.txtJumlahBKUDetail);
            this.Controls.Add(this.txtJumlahTrxDetail);
            this.Controls.Add(this.txtJunlahBKU);
            this.Controls.Add(this.txtJumlahTrx);
            this.Controls.Add(this.lblJenisBendahara);
            this.Controls.Add(this.cmdPanggilBKU);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbSUmber);
            this.Controls.Add(this.cmdCariLagi);
            this.Controls.Add(this.cmdCari);
            this.Controls.Add(this.txtCari);
            this.Controls.Add(this.lblPencarian);
            this.Controls.Add(this.ctrlTanggalBulanVertikal1);
            this.Controls.Add(this.label9);
            this.Name = "frmCekBKU";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cek BKU";
            this.Load += new System.EventHandler(this.frmCekBKU_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBKU2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBKU1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCariLagi;
        private System.Windows.Forms.Button cmdCari;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Label lblPencarian;
        private ctrlTanggalBulanVertikal ctrlTanggalBulanVertikal1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbSUmber;
        private ctrlDinas ctrlDinas1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView gridBKU1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoUrut1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoBukti1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tanggal1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Keteranngan1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nilaiu;
        private System.Windows.Forms.DataGridViewButtonColumn BKU1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button cmdPanggilBKU;
        private System.Windows.Forms.Label lblJenisBendahara;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gridBKU2;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoUrut2;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoBukti2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tanggal2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Keterangan2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jumlah2;
        private System.Windows.Forms.DataGridViewButtonColumn Hapus;
        private System.Windows.Forms.TextBox txtJumlahTrx;
        private System.Windows.Forms.TextBox txtJunlahBKU;
        private System.Windows.Forms.TextBox txtJumlahBKUDetail;
        private System.Windows.Forms.TextBox txtJumlahTrxDetail;
    }
}