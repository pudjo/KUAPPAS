namespace KUAPPAS.Bendahara
{
    partial class frmPerbaikiNoBKU
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmdTampilkan = new System.Windows.Forms.Button();
            this.gridBKU = new System.Windows.Forms.DataGridView();
            this.NoUrut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pilih = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NoBKU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tanggal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoBukti = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Uraia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Penerimaan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pengeluaran = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JenisSumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoUrutSUmber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdPerbaiki = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.cmdHapus = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.chkMulaidariNol = new System.Windows.Forms.CheckBox();
            this.cmdTampilkaAsli = new System.Windows.Forms.Button();
            this.lblJenisBendahara = new System.Windows.Forms.Label();
            this.cmdPilihSemua = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlSKPD1 = new KUAPPAS.ctrlSKPD();
            this.ctrlTanggalBulanVertikal1 = new KUAPPAS.Bendahara.ctrlTanggalBulanVertikal();
            this.cmdCariLagi = new System.Windows.Forms.Button();
            this.cmdCari = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.lblPencarian = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridBKU)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdTampilkan
            // 
            this.cmdTampilkan.Location = new System.Drawing.Point(445, 128);
            this.cmdTampilkan.Name = "cmdTampilkan";
            this.cmdTampilkan.Size = new System.Drawing.Size(209, 35);
            this.cmdTampilkan.TabIndex = 0;
            this.cmdTampilkan.Text = "Tampilkan Denagn Perbaikan No Urut";
            this.cmdTampilkan.UseVisualStyleBackColor = true;
            this.cmdTampilkan.Click += new System.EventHandler(this.cmdTampilkan_Click);
            // 
            // gridBKU
            // 
            this.gridBKU.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridBKU.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridBKU.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NoUrut,
            this.Pilih,
            this.NoBKU,
            this.Tanggal,
            this.NoBukti,
            this.Uraia,
            this.Penerimaan,
            this.Pengeluaran,
            this.JenisSumber,
            this.NoUrutSUmber});
            this.gridBKU.Location = new System.Drawing.Point(13, 170);
            this.gridBKU.Name = "gridBKU";
            this.gridBKU.Size = new System.Drawing.Size(973, 297);
            this.gridBKU.TabIndex = 1;
            // 
            // NoUrut
            // 
            this.NoUrut.HeaderText = "NoUrut";
            this.NoUrut.Name = "NoUrut";
            this.NoUrut.ReadOnly = true;
            this.NoUrut.Visible = false;
            // 
            // Pilih
            // 
            this.Pilih.HeaderText = "Pilih";
            this.Pilih.Name = "Pilih";
            this.Pilih.Width = 50;
            // 
            // NoBKU
            // 
            this.NoBKU.HeaderText = "No BKU";
            this.NoBKU.Name = "NoBKU";
            this.NoBKU.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NoBKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NoBKU.Width = 80;
            // 
            // Tanggal
            // 
            this.Tanggal.HeaderText = "Tanggal";
            this.Tanggal.Name = "Tanggal";
            this.Tanggal.ReadOnly = true;
            this.Tanggal.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Tanggal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // NoBukti
            // 
            this.NoBukti.HeaderText = "No Bukti";
            this.NoBukti.Name = "NoBukti";
            this.NoBukti.ReadOnly = true;
            this.NoBukti.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NoBukti.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Uraia
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Uraia.DefaultCellStyle = dataGridViewCellStyle1;
            this.Uraia.HeaderText = "Uraian";
            this.Uraia.Name = "Uraia";
            this.Uraia.ReadOnly = true;
            this.Uraia.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Uraia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Uraia.Width = 400;
            // 
            // Penerimaan
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Penerimaan.DefaultCellStyle = dataGridViewCellStyle2;
            this.Penerimaan.HeaderText = "Penerimaan";
            this.Penerimaan.Name = "Penerimaan";
            this.Penerimaan.ReadOnly = true;
            // 
            // Pengeluaran
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Pengeluaran.DefaultCellStyle = dataGridViewCellStyle3;
            this.Pengeluaran.HeaderText = "Pengeluaran";
            this.Pengeluaran.Name = "Pengeluaran";
            this.Pengeluaran.ReadOnly = true;
            // 
            // JenisSumber
            // 
            this.JenisSumber.HeaderText = "JenisSumber";
            this.JenisSumber.Name = "JenisSumber";
            this.JenisSumber.ReadOnly = true;
            // 
            // NoUrutSUmber
            // 
            this.NoUrutSUmber.HeaderText = "NoUrutSumer";
            this.NoUrutSUmber.Name = "NoUrutSUmber";
            this.NoUrutSUmber.ReadOnly = true;
            // 
            // cmdPerbaiki
            // 
            this.cmdPerbaiki.Location = new System.Drawing.Point(660, 129);
            this.cmdPerbaiki.Name = "cmdPerbaiki";
            this.cmdPerbaiki.Size = new System.Drawing.Size(186, 35);
            this.cmdPerbaiki.TabIndex = 2;
            this.cmdPerbaiki.Text = "Simpan dengan Penomoran Ini";
            this.cmdPerbaiki.UseVisualStyleBackColor = true;
            this.cmdPerbaiki.Click += new System.EventHandler(this.cmdPerbaiki_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 474);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(984, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // cmdHapus
            // 
            this.cmdHapus.Location = new System.Drawing.Point(843, 129);
            this.cmdHapus.Name = "cmdHapus";
            this.cmdHapus.Size = new System.Drawing.Size(129, 35);
            this.cmdHapus.TabIndex = 4;
            this.cmdHapus.Text = "Hapus";
            this.cmdHapus.UseVisualStyleBackColor = true;
            this.cmdHapus.Click += new System.EventHandler(this.cmdHapus_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(30, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 15);
            this.label9.TabIndex = 56;
            this.label9.Text = "OPD";
            // 
            // chkMulaidariNol
            // 
            this.chkMulaidariNol.AutoSize = true;
            this.chkMulaidariNol.Location = new System.Drawing.Point(356, 139);
            this.chkMulaidariNol.Name = "chkMulaidariNol";
            this.chkMulaidariNol.Size = new System.Drawing.Size(83, 17);
            this.chkMulaidariNol.TabIndex = 59;
            this.chkMulaidariNol.Text = "Mulai dari 0 ";
            this.chkMulaidariNol.UseVisualStyleBackColor = true;
            // 
            // cmdTampilkaAsli
            // 
            this.cmdTampilkaAsli.Location = new System.Drawing.Point(23, 129);
            this.cmdTampilkaAsli.Name = "cmdTampilkaAsli";
            this.cmdTampilkaAsli.Size = new System.Drawing.Size(150, 35);
            this.cmdTampilkaAsli.TabIndex = 60;
            this.cmdTampilkaAsli.Text = "Tampilakn Niolai yang Ada";
            this.cmdTampilkaAsli.UseVisualStyleBackColor = true;
            this.cmdTampilkaAsli.Click += new System.EventHandler(this.cmdTampilkaAsli_Click);
            // 
            // lblJenisBendahara
            // 
            this.lblJenisBendahara.AutoSize = true;
            this.lblJenisBendahara.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJenisBendahara.Location = new System.Drawing.Point(94, 8);
            this.lblJenisBendahara.Name = "lblJenisBendahara";
            this.lblJenisBendahara.Size = new System.Drawing.Size(47, 15);
            this.lblJenisBendahara.TabIndex = 61;
            this.lblJenisBendahara.Text = "label1";
            // 
            // cmdPilihSemua
            // 
            this.cmdPilihSemua.Location = new System.Drawing.Point(180, 131);
            this.cmdPilihSemua.Name = "cmdPilihSemua";
            this.cmdPilihSemua.Size = new System.Drawing.Size(103, 32);
            this.cmdPilihSemua.TabIndex = 62;
            this.cmdPilihSemua.Text = "Pilih Semua";
            this.cmdPilihSemua.UseVisualStyleBackColor = true;
            this.cmdPilihSemua.Click += new System.EventHandler(this.cmdPilihSemua_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "NoUrut";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "No BKU";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Tanggal";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "No Bukti";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn5.HeaderText = "Uraian";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 400;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn6.HeaderText = "Penerimaan";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn7.HeaderText = "Pengeluaran";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "JenisSumber";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "NoUrutSumer";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // ctrlSKPD1
            // 
            this.ctrlSKPD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSKPD1.Location = new System.Drawing.Point(94, 27);
            this.ctrlSKPD1.Name = "ctrlSKPD1";
            this.ctrlSKPD1.Size = new System.Drawing.Size(644, 24);
            this.ctrlSKPD1.TabIndex = 58;
            // 
            // ctrlTanggalBulanVertikal1
            // 
            this.ctrlTanggalBulanVertikal1.Bulan = 0;
            this.ctrlTanggalBulanVertikal1.JenisPeriode = 1;
            this.ctrlTanggalBulanVertikal1.Location = new System.Drawing.Point(68, 52);
            this.ctrlTanggalBulanVertikal1.Name = "ctrlTanggalBulanVertikal1";
            this.ctrlTanggalBulanVertikal1.Size = new System.Drawing.Size(404, 70);
            this.ctrlTanggalBulanVertikal1.TabIndex = 57;
            this.ctrlTanggalBulanVertikal1.TanggalAkhir = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            this.ctrlTanggalBulanVertikal1.TanggalAwal = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            // 
            // cmdCariLagi
            // 
            this.cmdCariLagi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCariLagi.Location = new System.Drawing.Point(897, 99);
            this.cmdCariLagi.Name = "cmdCariLagi";
            this.cmdCariLagi.Size = new System.Drawing.Size(75, 23);
            this.cmdCariLagi.TabIndex = 66;
            this.cmdCariLagi.Text = "Cari Lagi";
            this.cmdCariLagi.UseVisualStyleBackColor = true;
            this.cmdCariLagi.Click += new System.EventHandler(this.cmdCariLagi_Click);
            // 
            // cmdCari
            // 
            this.cmdCari.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCari.Location = new System.Drawing.Point(838, 98);
            this.cmdCari.Name = "cmdCari";
            this.cmdCari.Size = new System.Drawing.Size(62, 23);
            this.cmdCari.TabIndex = 65;
            this.cmdCari.Text = "Cari...";
            this.cmdCari.UseVisualStyleBackColor = true;
            this.cmdCari.Click += new System.EventHandler(this.cmdCari_Click);
            // 
            // txtCari
            // 
            this.txtCari.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCari.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCari.Location = new System.Drawing.Point(591, 99);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(239, 20);
            this.txtCari.TabIndex = 64;
            // 
            // lblPencarian
            // 
            this.lblPencarian.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPencarian.AutoSize = true;
            this.lblPencarian.Location = new System.Drawing.Point(512, 106);
            this.lblPencarian.Name = "lblPencarian";
            this.lblPencarian.Size = new System.Drawing.Size(55, 13);
            this.lblPencarian.TabIndex = 63;
            this.lblPencarian.Text = "Pencarian";
            // 
            // frmPerbaikiNoBKU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 496);
            this.Controls.Add(this.cmdCariLagi);
            this.Controls.Add(this.cmdCari);
            this.Controls.Add(this.txtCari);
            this.Controls.Add(this.lblPencarian);
            this.Controls.Add(this.cmdPilihSemua);
            this.Controls.Add(this.lblJenisBendahara);
            this.Controls.Add(this.cmdTampilkaAsli);
            this.Controls.Add(this.chkMulaidariNol);
            this.Controls.Add(this.ctrlSKPD1);
            this.Controls.Add(this.cmdTampilkan);
            this.Controls.Add(this.ctrlTanggalBulanVertikal1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmdHapus);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cmdPerbaiki);
            this.Controls.Add(this.gridBKU);
            this.Name = "frmPerbaikiNoBKU";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rapikan No BKU ";
            this.Load += new System.EventHandler(this.frmPerbaikiNoBKU_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridBKU)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdTampilkan;
        private System.Windows.Forms.DataGridView gridBKU;
        private System.Windows.Forms.Button cmdPerbaiki;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.Button cmdHapus;
        private System.Windows.Forms.Label label9;
        private ctrlTanggalBulanVertikal ctrlTanggalBulanVertikal1;
        private ctrlSKPD ctrlSKPD1;
        private System.Windows.Forms.CheckBox chkMulaidariNol;
        private System.Windows.Forms.Button cmdTampilkaAsli;
        private System.Windows.Forms.Label lblJenisBendahara;
        private System.Windows.Forms.Button cmdPilihSemua;
        private System.Windows.Forms.Button cmdCariLagi;
        private System.Windows.Forms.Button cmdCari;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Label lblPencarian;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoUrut;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Pilih;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoBKU;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tanggal;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoBukti;
        private System.Windows.Forms.DataGridViewTextBoxColumn Uraia;
        private System.Windows.Forms.DataGridViewTextBoxColumn Penerimaan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pengeluaran;
        private System.Windows.Forms.DataGridViewTextBoxColumn JenisSumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoUrutSUmber;
    }
}