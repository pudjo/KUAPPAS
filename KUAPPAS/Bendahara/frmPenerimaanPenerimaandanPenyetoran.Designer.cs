namespace KUAPPAS.Bendahara
{
    partial class frmPenerimaanPenerimaandanPenyetoran
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPenerimaanPenerimaandanPenyetoran));
            this.label12 = new System.Windows.Forms.Label();
            this.txtSpasi = new System.Windows.Forms.TextBox();
            this.cmdPanggilData = new System.Windows.Forms.Button();
            this.cmsCetak = new System.Windows.Forms.Button();
            this.gridPenerimaanPenyetoran = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tanggal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoBukti = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cara = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KodeRek = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Uraian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jumlah = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tanggal2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoSetor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jumlah2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Keterangan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlTanggalBulanVertikal1 = new KUAPPAS.Bendahara.ctrlTanggalBulanVertikal();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlSKPD1 = new KUAPPAS.ctrlSKPD();
            this.cmdExcell = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSaldo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBank = new System.Windows.Forms.TextBox();
            this.txtTunai = new System.Windows.Forms.TextBox();
            this.txtPenyetoran = new System.Windows.Forms.TextBox();
            this.txtPenerimaan = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtCetak = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.gridPenerimaanPenyetoran)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(310, 148);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 13);
            this.label12.TabIndex = 60;
            this.label12.Text = "Spassi Cetak";
            // 
            // txtSpasi
            // 
            this.txtSpasi.BackColor = System.Drawing.Color.Bisque;
            this.txtSpasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSpasi.Location = new System.Drawing.Point(315, 161);
            this.txtSpasi.Name = "txtSpasi";
            this.txtSpasi.Size = new System.Drawing.Size(50, 20);
            this.txtSpasi.TabIndex = 59;
            this.txtSpasi.Text = "0";
            // 
            // cmdPanggilData
            // 
            this.cmdPanggilData.BackColor = System.Drawing.Color.White;
            this.cmdPanggilData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdPanggilData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPanggilData.Image = global::KUAPPAS.Properties.Resources.search;
            this.cmdPanggilData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdPanggilData.Location = new System.Drawing.Point(11, 148);
            this.cmdPanggilData.Name = "cmdPanggilData";
            this.cmdPanggilData.Size = new System.Drawing.Size(127, 33);
            this.cmdPanggilData.TabIndex = 58;
            this.cmdPanggilData.Text = "Panggil Data";
            this.cmdPanggilData.UseVisualStyleBackColor = false;
            this.cmdPanggilData.Click += new System.EventHandler(this.cmdPanggilData_Click);
            // 
            // cmsCetak
            // 
            this.cmsCetak.BackColor = System.Drawing.Color.White;
            this.cmsCetak.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmsCetak.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsCetak.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmsCetak.Image = global::KUAPPAS.Properties.Resources.print;
            this.cmsCetak.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmsCetak.Location = new System.Drawing.Point(144, 148);
            this.cmsCetak.Name = "cmsCetak";
            this.cmsCetak.Size = new System.Drawing.Size(158, 33);
            this.cmsCetak.TabIndex = 57;
            this.cmsCetak.Text = "Cetak";
            this.cmsCetak.UseVisualStyleBackColor = false;
            this.cmsCetak.Click += new System.EventHandler(this.cmsCetak_Click);
            // 
            // gridPenerimaanPenyetoran
            // 
            this.gridPenerimaanPenyetoran.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPenerimaanPenyetoran.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridPenerimaanPenyetoran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPenerimaanPenyetoran.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.Tanggal,
            this.NoBukti,
            this.Cara,
            this.KodeRek,
            this.Uraian,
            this.Jumlah,
            this.Tanggal2,
            this.NoSetor,
            this.Jumlah2,
            this.Keterangan});
            this.gridPenerimaanPenyetoran.Location = new System.Drawing.Point(0, 192);
            this.gridPenerimaanPenyetoran.Name = "gridPenerimaanPenyetoran";
            this.gridPenerimaanPenyetoran.Size = new System.Drawing.Size(1127, 250);
            this.gridPenerimaanPenyetoran.TabIndex = 4;
            // 
            // No
            // 
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            // 
            // Tanggal
            // 
            this.Tanggal.HeaderText = "Tanggal";
            this.Tanggal.Name = "Tanggal";
            this.Tanggal.ReadOnly = true;
            // 
            // NoBukti
            // 
            this.NoBukti.HeaderText = "No Bukti";
            this.NoBukti.Name = "NoBukti";
            this.NoBukti.ReadOnly = true;
            // 
            // Cara
            // 
            this.Cara.HeaderText = "Cara";
            this.Cara.Name = "Cara";
            this.Cara.ReadOnly = true;
            // 
            // KodeRek
            // 
            this.KodeRek.HeaderText = "Kode Rekening";
            this.KodeRek.Name = "KodeRek";
            this.KodeRek.ReadOnly = true;
            // 
            // Uraian
            // 
            this.Uraian.HeaderText = "Uraian";
            this.Uraian.Name = "Uraian";
            this.Uraian.ReadOnly = true;
            // 
            // Jumlah
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Jumlah.DefaultCellStyle = dataGridViewCellStyle1;
            this.Jumlah.HeaderText = "Jumlah";
            this.Jumlah.Name = "Jumlah";
            this.Jumlah.ReadOnly = true;
            // 
            // Tanggal2
            // 
            this.Tanggal2.HeaderText = "Tanggal";
            this.Tanggal2.Name = "Tanggal2";
            this.Tanggal2.ReadOnly = true;
            // 
            // NoSetor
            // 
            this.NoSetor.HeaderText = "Bo STS";
            this.NoSetor.Name = "NoSetor";
            this.NoSetor.ReadOnly = true;
            // 
            // Jumlah2
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Jumlah2.DefaultCellStyle = dataGridViewCellStyle2;
            this.Jumlah2.HeaderText = "Jumlah";
            this.Jumlah2.Name = "Jumlah2";
            this.Jumlah2.ReadOnly = true;
            // 
            // Keterangan
            // 
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Keterangan.DefaultCellStyle = dataGridViewCellStyle3;
            this.Keterangan.HeaderText = "Keterangan";
            this.Keterangan.Name = "Keterangan";
            this.Keterangan.ReadOnly = true;
            this.Keterangan.Width = 250;
            // 
            // ctrlTanggalBulanVertikal1
            // 
            this.ctrlTanggalBulanVertikal1.Bulan = 5;
            this.ctrlTanggalBulanVertikal1.JenisPeriode = 1;
            this.ctrlTanggalBulanVertikal1.Location = new System.Drawing.Point(12, 73);
            this.ctrlTanggalBulanVertikal1.Name = "ctrlTanggalBulanVertikal1";
            this.ctrlTanggalBulanVertikal1.Size = new System.Drawing.Size(404, 73);
            this.ctrlTanggalBulanVertikal1.TabIndex = 3;
            this.ctrlTanggalBulanVertikal1.TanggalAkhir = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            this.ctrlTanggalBulanVertikal1.TanggalAwal = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1127, 41);
            this.ctrlHeader1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "OPD";
            // 
            // ctrlSKPD1
            // 
            this.ctrlSKPD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSKPD1.Location = new System.Drawing.Point(111, 47);
            this.ctrlSKPD1.Name = "ctrlSKPD1";
            this.ctrlSKPD1.Size = new System.Drawing.Size(529, 24);
            this.ctrlSKPD1.TabIndex = 0;
            this.ctrlSKPD1.OnChanged += new KUAPPAS.ctrlSKPD.ValueChangedEventHandler(this.ctrlSKPD1_OnChanged);
            // 
            // cmdExcell
            // 
            this.cmdExcell.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdExcell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExcell.Image = ((System.Drawing.Image)(resources.GetObject("cmdExcell.Image")));
            this.cmdExcell.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdExcell.Location = new System.Drawing.Point(512, 153);
            this.cmdExcell.Name = "cmdExcell";
            this.cmdExcell.Size = new System.Drawing.Size(87, 33);
            this.cmdExcell.TabIndex = 61;
            this.cmdExcell.Text = "Excell";
            this.cmdExcell.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdExcell.UseVisualStyleBackColor = true;
            this.cmdExcell.Click += new System.EventHandler(this.cmdExcell_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtSaldo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtBank);
            this.panel1.Controls.Add(this.txtTunai);
            this.panel1.Controls.Add(this.txtPenyetoran);
            this.panel1.Controls.Add(this.txtPenerimaan);
            this.panel1.Location = new System.Drawing.Point(674, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(462, 150);
            this.panel1.TabIndex = 62;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(85, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "Saldo Bank";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(85, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Saldo Tunai";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(45, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Saldo";
            // 
            // txtSaldo
            // 
            this.txtSaldo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaldo.Location = new System.Drawing.Point(202, 56);
            this.txtSaldo.Name = "txtSaldo";
            this.txtSaldo.Size = new System.Drawing.Size(243, 21);
            this.txtSaldo.TabIndex = 6;
            this.txtSaldo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(29, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Jumlah Penyetoran";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Jumlah Penerimaan";
            // 
            // txtBank
            // 
            this.txtBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBank.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBank.Location = new System.Drawing.Point(202, 111);
            this.txtBank.Name = "txtBank";
            this.txtBank.Size = new System.Drawing.Size(243, 21);
            this.txtBank.TabIndex = 3;
            this.txtBank.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTunai
            // 
            this.txtTunai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTunai.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTunai.Location = new System.Drawing.Point(202, 88);
            this.txtTunai.Name = "txtTunai";
            this.txtTunai.Size = new System.Drawing.Size(243, 21);
            this.txtTunai.TabIndex = 2;
            this.txtTunai.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPenyetoran
            // 
            this.txtPenyetoran.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPenyetoran.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPenyetoran.Location = new System.Drawing.Point(202, 30);
            this.txtPenyetoran.Name = "txtPenyetoran";
            this.txtPenyetoran.Size = new System.Drawing.Size(243, 21);
            this.txtPenyetoran.TabIndex = 1;
            this.txtPenyetoran.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPenerimaan
            // 
            this.txtPenerimaan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPenerimaan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPenerimaan.Location = new System.Drawing.Point(202, 7);
            this.txtPenerimaan.Name = "txtPenerimaan";
            this.txtPenerimaan.Size = new System.Drawing.Size(243, 21);
            this.txtPenerimaan.TabIndex = 0;
            this.txtPenerimaan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(398, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 64;
            this.label7.Text = "Tanggal Cetak";
            // 
            // dtCetak
            // 
            this.dtCetak.CustomFormat = "dd MMM yyyy";
            this.dtCetak.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtCetak.Location = new System.Drawing.Point(398, 158);
            this.dtCetak.Name = "dtCetak";
            this.dtCetak.Size = new System.Drawing.Size(108, 20);
            this.dtCetak.TabIndex = 63;
            // 
            // frmPenerimaanPenerimaandanPenyetoran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1127, 454);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtCetak);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmdExcell);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtSpasi);
            this.Controls.Add(this.cmdPanggilData);
            this.Controls.Add(this.cmsCetak);
            this.Controls.Add(this.gridPenerimaanPenyetoran);
            this.Controls.Add(this.ctrlTanggalBulanVertikal1);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlSKPD1);
            this.Name = "frmPenerimaanPenerimaandanPenyetoran";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPenerimaanPenerimaandanPenyetoran";
            this.Load += new System.EventHandler(this.frmPenerimaanPenerimaandanPenyetoran_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridPenerimaanPenyetoran)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlSKPD ctrlSKPD1;
        private System.Windows.Forms.Label label1;
        private ctrlHeader ctrlHeader1;
        private ctrlTanggalBulanVertikal ctrlTanggalBulanVertikal1;
        private System.Windows.Forms.DataGridView gridPenerimaanPenyetoran;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtSpasi;
        private System.Windows.Forms.Button cmdPanggilData;
        private System.Windows.Forms.Button cmsCetak;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tanggal;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoBukti;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cara;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodeRek;
        private System.Windows.Forms.DataGridViewTextBoxColumn Uraian;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jumlah;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tanggal2;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoSetor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jumlah2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Keterangan;
        private System.Windows.Forms.Button cmdExcell;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSaldo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBank;
        private System.Windows.Forms.TextBox txtTunai;
        private System.Windows.Forms.TextBox txtPenyetoran;
        private System.Windows.Forms.TextBox txtPenerimaan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtCetak;
    }
}