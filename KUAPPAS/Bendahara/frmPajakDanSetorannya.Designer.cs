namespace KUAPPAS.Bendahara
{
    partial class frmPajakDanSetorannya
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupPencarian = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.cmdCariLagi = new System.Windows.Forms.Button();
            this.cmdCari = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbStatusSetor = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdTampilkan = new System.Windows.Forms.Button();
            this.ctrlTanggalBulan1 = new KUAPPAS.Bendahara.ctrlTanggalBulan();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.gridPajakDanSetor = new System.Windows.Forms.DataGridView();
            this.InoueurPanjar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoBukti = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TanggalSPJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KeteranganBelanja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaPajak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jumlah = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoUrutSetor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuktiSetor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tanggal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KeteranganSetor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaPajaksetor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JumlahSetor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdSetor = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Hapus = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.picBelumSetor = new System.Windows.Forms.PictureBox();
            this.picSudahSetor = new System.Windows.Forms.PictureBox();
            this.picSampaj = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtJumlahPungut = new System.Windows.Forms.TextBox();
            this.txtJumlahSetor = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmdCekBKU = new System.Windows.Forms.Button();
            this.groupPencarian.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPajakDanSetor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBelumSetor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSudahSetor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSampaj)).BeginInit();
            this.SuspendLayout();
            // 
            // groupPencarian
            // 
            this.groupPencarian.Controls.Add(this.button1);
            this.groupPencarian.Controls.Add(this.txtCari);
            this.groupPencarian.Controls.Add(this.cmdCariLagi);
            this.groupPencarian.Controls.Add(this.cmdCari);
            this.groupPencarian.Location = new System.Drawing.Point(285, 157);
            this.groupPencarian.Name = "groupPencarian";
            this.groupPencarian.Size = new System.Drawing.Size(463, 46);
            this.groupPencarian.TabIndex = 35;
            this.groupPencarian.TabStop = false;
            this.groupPencarian.Text = "Pencarian";
            this.groupPencarian.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(462, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 33;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txtCari
            // 
            this.txtCari.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCari.Location = new System.Drawing.Point(49, 19);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(239, 20);
            this.txtCari.TabIndex = 30;
            // 
            // cmdCariLagi
            // 
            this.cmdCariLagi.Location = new System.Drawing.Point(372, 16);
            this.cmdCariLagi.Name = "cmdCariLagi";
            this.cmdCariLagi.Size = new System.Drawing.Size(75, 23);
            this.cmdCariLagi.TabIndex = 32;
            this.cmdCariLagi.Text = "Cari Lagi";
            this.cmdCariLagi.UseVisualStyleBackColor = true;
            this.cmdCariLagi.Click += new System.EventHandler(this.cmdCariLagi_Click);
            // 
            // cmdCari
            // 
            this.cmdCari.Location = new System.Drawing.Point(304, 16);
            this.cmdCari.Name = "cmdCari";
            this.cmdCari.Size = new System.Drawing.Size(62, 23);
            this.cmdCari.TabIndex = 31;
            this.cmdCari.Text = "Cari...";
            this.cmdCari.UseVisualStyleBackColor = true;
            this.cmdCari.Click += new System.EventHandler(this.cmdCari_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Status";
            // 
            // cmbStatusSetor
            // 
            this.cmbStatusSetor.FormattingEnabled = true;
            this.cmbStatusSetor.Location = new System.Drawing.Point(97, 142);
            this.cmbStatusSetor.Name = "cmbStatusSetor";
            this.cmbStatusSetor.Size = new System.Drawing.Size(170, 21);
            this.cmbStatusSetor.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tanggal Belanja";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "O P D";
            // 
            // cmdTampilkan
            // 
            this.cmdTampilkan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTampilkan.Location = new System.Drawing.Point(97, 168);
            this.cmdTampilkan.Name = "cmdTampilkan";
            this.cmdTampilkan.Size = new System.Drawing.Size(170, 28);
            this.cmdTampilkan.TabIndex = 3;
            this.cmdTampilkan.Text = "Tampilkan Data";
            this.cmdTampilkan.UseVisualStyleBackColor = true;
            this.cmdTampilkan.Click += new System.EventHandler(this.cmdTampilkan_Click);
            // 
            // ctrlTanggalBulan1
            // 
            this.ctrlTanggalBulan1.Bulan = 1;
            this.ctrlTanggalBulan1.JenisPeriode = 1;
            this.ctrlTanggalBulan1.Location = new System.Drawing.Point(97, 94);
            this.ctrlTanggalBulan1.Name = "ctrlTanggalBulan1";
            this.ctrlTanggalBulan1.Size = new System.Drawing.Size(759, 46);
            this.ctrlTanggalBulan1.TabIndex = 2;
            this.ctrlTanggalBulan1.TanggalAkhir = new System.DateTime(2024, 4, 23, 0, 0, 0, 0);
            this.ctrlTanggalBulan1.TanggalAwal = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(97, 48);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(539, 44);
            this.ctrlDinas1.TabIndex = 1;
            // 
            // gridPajakDanSetor
            // 
            this.gridPajakDanSetor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPajakDanSetor.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridPajakDanSetor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPajakDanSetor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InoueurPanjar,
            this.NoBukti,
            this.TanggalSPJ,
            this.KeteranganBelanja,
            this.NamaPajak,
            this.Jumlah,
            this.NoUrutSetor,
            this.BuktiSetor,
            this.Tanggal,
            this.KeteranganSetor,
            this.NamaPajaksetor,
            this.JumlahSetor,
            this.cmdSetor,
            this.Hapus});
            this.gridPajakDanSetor.Location = new System.Drawing.Point(2, 209);
            this.gridPajakDanSetor.Name = "gridPajakDanSetor";
            this.gridPajakDanSetor.Size = new System.Drawing.Size(1356, 374);
            this.gridPajakDanSetor.TabIndex = 0;
            this.gridPajakDanSetor.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPajakDanSetor_CellContentClick);
            // 
            // InoueurPanjar
            // 
            this.InoueurPanjar.HeaderText = "NoUrutPanjar";
            this.InoueurPanjar.Name = "InoueurPanjar";
            this.InoueurPanjar.ReadOnly = true;
            this.InoueurPanjar.Visible = false;
            // 
            // NoBukti
            // 
            this.NoBukti.HeaderText = "No BPK/SPJ";
            this.NoBukti.Name = "NoBukti";
            this.NoBukti.ReadOnly = true;
            this.NoBukti.Width = 80;
            // 
            // TanggalSPJ
            // 
            this.TanggalSPJ.HeaderText = "Tanggal SPJ";
            this.TanggalSPJ.Name = "TanggalSPJ";
            this.TanggalSPJ.ReadOnly = true;
            this.TanggalSPJ.Width = 80;
            // 
            // KeteranganBelanja
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.KeteranganBelanja.DefaultCellStyle = dataGridViewCellStyle1;
            this.KeteranganBelanja.HeaderText = "KeteranganBelanja";
            this.KeteranganBelanja.Name = "KeteranganBelanja";
            this.KeteranganBelanja.ReadOnly = true;
            this.KeteranganBelanja.Width = 250;
            // 
            // NamaPajak
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NamaPajak.DefaultCellStyle = dataGridViewCellStyle2;
            this.NamaPajak.HeaderText = "Nama Pajak";
            this.NamaPajak.Name = "NamaPajak";
            this.NamaPajak.ReadOnly = true;
            // 
            // Jumlah
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Jumlah.DefaultCellStyle = dataGridViewCellStyle3;
            this.Jumlah.HeaderText = "Jumlah Potongan";
            this.Jumlah.Name = "Jumlah";
            this.Jumlah.ReadOnly = true;
            // 
            // NoUrutSetor
            // 
            this.NoUrutSetor.HeaderText = "NoSetor";
            this.NoUrutSetor.Name = "NoUrutSetor";
            this.NoUrutSetor.ReadOnly = true;
            this.NoUrutSetor.Visible = false;
            // 
            // BuktiSetor
            // 
            this.BuktiSetor.HeaderText = "No Bukti Setor";
            this.BuktiSetor.Name = "BuktiSetor";
            this.BuktiSetor.ReadOnly = true;
            this.BuktiSetor.Width = 50;
            // 
            // Tanggal
            // 
            this.Tanggal.HeaderText = "TanggalSetor";
            this.Tanggal.Name = "Tanggal";
            this.Tanggal.ReadOnly = true;
            this.Tanggal.Width = 80;
            // 
            // KeteranganSetor
            // 
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.KeteranganSetor.DefaultCellStyle = dataGridViewCellStyle4;
            this.KeteranganSetor.HeaderText = "Keterangan setor";
            this.KeteranganSetor.Name = "KeteranganSetor";
            this.KeteranganSetor.ReadOnly = true;
            this.KeteranganSetor.Width = 200;
            // 
            // NamaPajaksetor
            // 
            this.NamaPajaksetor.HeaderText = "Nama Pajak";
            this.NamaPajaksetor.Name = "NamaPajaksetor";
            this.NamaPajaksetor.ReadOnly = true;
            // 
            // JumlahSetor
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.JumlahSetor.DefaultCellStyle = dataGridViewCellStyle5;
            this.JumlahSetor.HeaderText = "JumlahDiseor";
            this.JumlahSetor.Name = "JumlahSetor";
            this.JumlahSetor.ReadOnly = true;
            // 
            // cmdSetor
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Turquoise;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetor.DefaultCellStyle = dataGridViewCellStyle6;
            this.cmdSetor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSetor.HeaderText = "Setor";
            this.cmdSetor.Name = "cmdSetor";
            this.cmdSetor.ReadOnly = true;
            this.cmdSetor.Width = 80;
            // 
            // Hapus
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Red;
            this.Hapus.DefaultCellStyle = dataGridViewCellStyle7;
            this.Hapus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Hapus.HeaderText = "Hapus Data";
            this.Hapus.Name = "Hapus";
            this.Hapus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Hapus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1370, 41);
            this.ctrlHeader1.TabIndex = 36;
            // 
            // picBelumSetor
            // 
            this.picBelumSetor.Location = new System.Drawing.Point(1145, 81);
            this.picBelumSetor.Name = "picBelumSetor";
            this.picBelumSetor.Size = new System.Drawing.Size(19, 16);
            this.picBelumSetor.TabIndex = 37;
            this.picBelumSetor.TabStop = false;
            // 
            // picSudahSetor
            // 
            this.picSudahSetor.Location = new System.Drawing.Point(1145, 102);
            this.picSudahSetor.Name = "picSudahSetor";
            this.picSudahSetor.Size = new System.Drawing.Size(19, 16);
            this.picSudahSetor.TabIndex = 38;
            this.picSudahSetor.TabStop = false;
            // 
            // picSampaj
            // 
            this.picSampaj.Location = new System.Drawing.Point(1145, 121);
            this.picSampaj.Name = "picSampaj";
            this.picSampaj.Size = new System.Drawing.Size(19, 16);
            this.picSampaj.TabIndex = 39;
            this.picSampaj.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1180, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 16);
            this.label4.TabIndex = 40;
            this.label4.Text = "Belum disetor";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1180, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 16);
            this.label5.TabIndex = 41;
            this.label5.Text = "Sudah disetor";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1180, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(164, 16);
            this.label6.TabIndex = 42;
            this.label6.Text = "Data Sampaj (Sila Hapus)";
            // 
            // txtJumlahPungut
            // 
            this.txtJumlahPungut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlahPungut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlahPungut.Location = new System.Drawing.Point(906, 155);
            this.txtJumlahPungut.Name = "txtJumlahPungut";
            this.txtJumlahPungut.Size = new System.Drawing.Size(223, 22);
            this.txtJumlahPungut.TabIndex = 43;
            this.txtJumlahPungut.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtJumlahSetor
            // 
            this.txtJumlahSetor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlahSetor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlahSetor.Location = new System.Drawing.Point(906, 176);
            this.txtJumlahSetor.Name = "txtJumlahSetor";
            this.txtJumlahSetor.Size = new System.Drawing.Size(223, 22);
            this.txtJumlahSetor.TabIndex = 44;
            this.txtJumlahSetor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(786, 159);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 45;
            this.label7.Text = "Jumlah Pungut";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(786, 183);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 46;
            this.label8.Text = "Jumlah Setor";
            // 
            // cmdCekBKU
            // 
            this.cmdCekBKU.Location = new System.Drawing.Point(1145, 157);
            this.cmdCekBKU.Name = "cmdCekBKU";
            this.cmdCekBKU.Size = new System.Drawing.Size(75, 39);
            this.cmdCekBKU.TabIndex = 47;
            this.cmdCekBKU.Text = "Cek BKU";
            this.cmdCekBKU.UseVisualStyleBackColor = true;
            this.cmdCekBKU.Click += new System.EventHandler(this.cmdCekBKU_Click);
            // 
            // frmPajakDanSetorannya
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 595);
            this.Controls.Add(this.cmdCekBKU);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtJumlahSetor);
            this.Controls.Add(this.txtJumlahPungut);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.picSampaj);
            this.Controls.Add(this.picSudahSetor);
            this.Controls.Add(this.picBelumSetor);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.groupPencarian);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbStatusSetor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdTampilkan);
            this.Controls.Add(this.ctrlTanggalBulan1);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.gridPajakDanSetor);
            this.Name = "frmPajakDanSetorannya";
            this.Text = "frmPajakDanSetorannya";
            this.Load += new System.EventHandler(this.frmPajakDanSetorannya_Load);
            this.groupPencarian.ResumeLayout(false);
            this.groupPencarian.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPajakDanSetor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBelumSetor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSudahSetor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSampaj)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridPajakDanSetor;
        private ctrlDinas ctrlDinas1;
        private ctrlTanggalBulan ctrlTanggalBulan1;
        private System.Windows.Forms.Button cmdTampilkan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbStatusSetor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupPencarian;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Button cmdCariLagi;
        private System.Windows.Forms.Button cmdCari;
        private System.Windows.Forms.DataGridViewTextBoxColumn InoueurPanjar;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoBukti;
        private System.Windows.Forms.DataGridViewTextBoxColumn TanggalSPJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeteranganBelanja;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaPajak;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jumlah;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoUrutSetor;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuktiSetor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tanggal;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeteranganSetor;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaPajaksetor;
        private System.Windows.Forms.DataGridViewTextBoxColumn JumlahSetor;
        private System.Windows.Forms.DataGridViewButtonColumn cmdSetor;
        private System.Windows.Forms.DataGridViewButtonColumn Hapus;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.PictureBox picBelumSetor;
        private System.Windows.Forms.PictureBox picSudahSetor;
        private System.Windows.Forms.PictureBox picSampaj;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtJumlahPungut;
        private System.Windows.Forms.TextBox txtJumlahSetor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button cmdCekBKU;
    }
}