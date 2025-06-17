namespace KUAPPAS.Akunting
{
    partial class frmLaporanJurnal
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.gridLJurnal = new System.Windows.Forms.DataGridView();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.cmdCetak = new System.Windows.Forms.Button();
            this.ctrlTanggalBulanVertikal1 = new KUAPPAS.Bendahara.ctrlTanggalBulanVertikal();
            this.chkSemuaDinas = new System.Windows.Forms.CheckBox();
            this.txtJumlahDebet = new System.Windows.Forms.TextBox();
            this.txtJumlahKredit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrlJenisSumber1 = new KUAPPAS.Akunting.ctrlJenisSumber();
            this.label4 = new System.Windows.Forms.Label();
            this.gridTidakBalance = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tgl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoUrut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.manu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdExcell = new System.Windows.Forms.Button();
            this.IDRekening = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaSKPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoBukti = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Anggaran = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Realisasi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RealisasiBB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridLJurnal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTidakBalance)).BeginInit();
            this.manu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdLoad
            // 
            this.cmdLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLoad.Location = new System.Drawing.Point(408, 54);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(89, 31);
            this.cmdLoad.TabIndex = 30;
            this.cmdLoad.Text = "Panggil Data";
            this.cmdLoad.UseVisualStyleBackColor = true;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 29;
            this.label3.Text = "O P D";
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1254, 46);
            this.ctrlHeader1.TabIndex = 27;
            // 
            // gridLJurnal
            // 
            this.gridLJurnal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridLJurnal.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridLJurnal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLJurnal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDRekening,
            this.NamaSKPD,
            this.Kode,
            this.NoBukti,
            this.Anggaran,
            this.Realisasi,
            this.RealisasiBB,
            this.Level});
            this.gridLJurnal.Location = new System.Drawing.Point(408, 92);
            this.gridLJurnal.Name = "gridLJurnal";
            this.gridLJurnal.Size = new System.Drawing.Size(834, 388);
            this.gridLJurnal.TabIndex = 31;
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.BackColor = System.Drawing.SystemColors.Control;
            this.ctrlDinas1.Location = new System.Drawing.Point(67, 92);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(331, 56);
            this.ctrlDinas1.TabIndex = 28;
            this.ctrlDinas1.UK = 0;
            // 
            // cmdCetak
            // 
            this.cmdCetak.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCetak.Location = new System.Drawing.Point(498, 54);
            this.cmdCetak.Name = "cmdCetak";
            this.cmdCetak.Size = new System.Drawing.Size(87, 31);
            this.cmdCetak.TabIndex = 34;
            this.cmdCetak.Text = "Cetak";
            this.cmdCetak.UseVisualStyleBackColor = true;
            this.cmdCetak.Click += new System.EventHandler(this.cmdCetak_Click);
            // 
            // ctrlTanggalBulanVertikal1
            // 
            this.ctrlTanggalBulanVertikal1.Bulan = 5;
            this.ctrlTanggalBulanVertikal1.JenisPeriode = 1;
            this.ctrlTanggalBulanVertikal1.Location = new System.Drawing.Point(10, 140);
            this.ctrlTanggalBulanVertikal1.Name = "ctrlTanggalBulanVertikal1";
            this.ctrlTanggalBulanVertikal1.Size = new System.Drawing.Size(388, 75);
            this.ctrlTanggalBulanVertikal1.TabIndex = 33;
            this.ctrlTanggalBulanVertikal1.TanggalAkhir = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            this.ctrlTanggalBulanVertikal1.TanggalAwal = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            // 
            // chkSemuaDinas
            // 
            this.chkSemuaDinas.AutoSize = true;
            this.chkSemuaDinas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSemuaDinas.Location = new System.Drawing.Point(79, 69);
            this.chkSemuaDinas.Name = "chkSemuaDinas";
            this.chkSemuaDinas.Size = new System.Drawing.Size(100, 17);
            this.chkSemuaDinas.TabIndex = 32;
            this.chkSemuaDinas.Text = "Semua Dinas";
            this.chkSemuaDinas.UseVisualStyleBackColor = true;
            // 
            // txtJumlahDebet
            // 
            this.txtJumlahDebet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJumlahDebet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlahDebet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlahDebet.Location = new System.Drawing.Point(727, 54);
            this.txtJumlahDebet.Name = "txtJumlahDebet";
            this.txtJumlahDebet.ReadOnly = true;
            this.txtJumlahDebet.Size = new System.Drawing.Size(226, 26);
            this.txtJumlahDebet.TabIndex = 35;
            this.txtJumlahDebet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtJumlahKredit
            // 
            this.txtJumlahKredit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJumlahKredit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlahKredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlahKredit.Location = new System.Drawing.Point(1033, 56);
            this.txtJumlahKredit.Name = "txtJumlahKredit";
            this.txtJumlahKredit.ReadOnly = true;
            this.txtJumlahKredit.Size = new System.Drawing.Size(209, 26);
            this.txtJumlahKredit.TabIndex = 36;
            this.txtJumlahKredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtJumlahKredit.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(663, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 20);
            this.label1.TabIndex = 37;
            this.label1.Text = "Debet";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(971, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 38;
            this.label2.Text = "Kredit";
            // 
            // ctrlJenisSumber1
            // 
            this.ctrlJenisSumber1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlJenisSumber1.Location = new System.Drawing.Point(0, 272);
            this.ctrlJenisSumber1.Name = "ctrlJenisSumber1";
            this.ctrlJenisSumber1.Size = new System.Drawing.Size(398, 24);
            this.ctrlJenisSumber1.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 256);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "Janis  Sumber";
            // 
            // gridTidakBalance
            // 
            this.gridTidakBalance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridTidakBalance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTidakBalance.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.Tgl,
            this.NoUrut});
            this.gridTidakBalance.ContextMenuStrip = this.manu;
            this.gridTidakBalance.Location = new System.Drawing.Point(0, 332);
            this.gridTidakBalance.Name = "gridTidakBalance";
            this.gridTidakBalance.Size = new System.Drawing.Size(398, 148);
            this.gridTidakBalance.TabIndex = 41;
            this.gridTidakBalance.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTidakBalance_CellContentClick);
            // 
            // No
            // 
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.Width = 200;
            // 
            // Tgl
            // 
            this.Tgl.HeaderText = "Tanggal";
            this.Tgl.Name = "Tgl";
            this.Tgl.ReadOnly = true;
            // 
            // NoUrut
            // 
            this.NoUrut.HeaderText = "NoUrut";
            this.NoUrut.Name = "NoUrut";
            // 
            // manu
            // 
            this.manu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.manu.Name = "manuSPJ";
            this.manu.Size = new System.Drawing.Size(103, 26);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 316);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(197, 13);
            this.label5.TabIndex = 42;
            this.label5.Text = "Tidak Balance (Sila Jurnal Ulang)";
            // 
            // cmdExcell
            // 
            this.cmdExcell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExcell.Location = new System.Drawing.Point(587, 54);
            this.cmdExcell.Name = "cmdExcell";
            this.cmdExcell.Size = new System.Drawing.Size(75, 31);
            this.cmdExcell.TabIndex = 43;
            this.cmdExcell.Text = "Excell";
            this.cmdExcell.UseVisualStyleBackColor = true;
            this.cmdExcell.Click += new System.EventHandler(this.cmdExcell_Click);
            // 
            // IDRekening
            // 
            this.IDRekening.HeaderText = "MoJurnal";
            this.IDRekening.Name = "IDRekening";
            this.IDRekening.ReadOnly = true;
            this.IDRekening.Visible = false;
            // 
            // NamaSKPD
            // 
            this.NamaSKPD.HeaderText = "Nama SKPD";
            this.NamaSKPD.Name = "NamaSKPD";
            this.NamaSKPD.ReadOnly = true;
            this.NamaSKPD.Width = 200;
            // 
            // Kode
            // 
            this.Kode.HeaderText = "Tanggal";
            this.Kode.Name = "Kode";
            this.Kode.ReadOnly = true;
            this.Kode.Width = 150;
            // 
            // NoBukti
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NoBukti.DefaultCellStyle = dataGridViewCellStyle1;
            this.NoBukti.HeaderText = "No Nukti";
            this.NoBukti.Name = "NoBukti";
            this.NoBukti.ReadOnly = true;
            this.NoBukti.Width = 300;
            // 
            // Anggaran
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Anggaran.DefaultCellStyle = dataGridViewCellStyle2;
            this.Anggaran.HeaderText = "Kode Rekening";
            this.Anggaran.Name = "Anggaran";
            this.Anggaran.ReadOnly = true;
            this.Anggaran.Width = 150;
            // 
            // Realisasi
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Realisasi.DefaultCellStyle = dataGridViewCellStyle3;
            this.Realisasi.HeaderText = "Nama Rekening";
            this.Realisasi.Name = "Realisasi";
            this.Realisasi.ReadOnly = true;
            this.Realisasi.Width = 150;
            // 
            // RealisasiBB
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.RealisasiBB.DefaultCellStyle = dataGridViewCellStyle4;
            this.RealisasiBB.HeaderText = "Debet";
            this.RealisasiBB.Name = "RealisasiBB";
            this.RealisasiBB.Width = 150;
            // 
            // Level
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Level.DefaultCellStyle = dataGridViewCellStyle5;
            this.Level.HeaderText = "Kredit";
            this.Level.Name = "Level";
            this.Level.ReadOnly = true;
            // 
            // frmLaporanJurnal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 489);
            this.Controls.Add(this.cmdExcell);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gridTidakBalance);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ctrlJenisSumber1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtJumlahKredit);
            this.Controls.Add(this.txtJumlahDebet);
            this.Controls.Add(this.cmdLoad);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.gridLJurnal);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.cmdCetak);
            this.Controls.Add(this.ctrlTanggalBulanVertikal1);
            this.Controls.Add(this.chkSemuaDinas);
            this.Name = "frmLaporanJurnal";
            this.Text = "Laporan Jurnal";
            this.Load += new System.EventHandler(this.frmLaporanJurnal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridLJurnal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTidakBalance)).EndInit();
            this.manu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdLoad;
        private System.Windows.Forms.Label label3;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.DataGridView gridLJurnal;
        private ctrlDinas ctrlDinas1;
        private System.Windows.Forms.Button cmdCetak;
        private Bendahara.ctrlTanggalBulanVertikal ctrlTanggalBulanVertikal1;
        private System.Windows.Forms.CheckBox chkSemuaDinas;
        private System.Windows.Forms.TextBox txtJumlahDebet;
        private System.Windows.Forms.TextBox txtJumlahKredit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private ctrlJenisSumber ctrlJenisSumber1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView gridTidakBalance;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tgl;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoUrut;
        private System.Windows.Forms.ContextMenuStrip manu;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.Button cmdExcell;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDRekening;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaSKPD;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kode;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoBukti;
        private System.Windows.Forms.DataGridViewTextBoxColumn Anggaran;
        private System.Windows.Forms.DataGridViewTextBoxColumn Realisasi;
        private System.Windows.Forms.DataGridViewTextBoxColumn RealisasiBB;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level;
    }
}