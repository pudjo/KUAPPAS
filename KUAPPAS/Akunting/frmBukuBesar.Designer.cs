namespace KUAPPAS.Akunting
{
    partial class frmBukuBesar
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
            this.txtNamaRekening = new System.Windows.Forms.TextBox();
            this.txtIDrekening = new System.Windows.Forms.TextBox();
            this.gridBukuBesar = new System.Windows.Forms.DataGridView();
            this.Tanggal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoBukti = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Keterangan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Debet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoJurnal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Inourutsumbar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdPanggil = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlPencarian1 = new KUAPPAS.ctrlPencarian();
            this.treeRekening1 = new KUAPPAS.treeRekening();
            this.ctrlTanggalBulanVertikal1 = new KUAPPAS.Bendahara.ctrlTanggalBulanVertikal();
            this.ctrlSKPD1 = new KUAPPAS.ctrlSKPD();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSaldoAwal = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridBukuBesar)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNamaRekening
            // 
            this.txtNamaRekening.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNamaRekening.Location = new System.Drawing.Point(141, 199);
            this.txtNamaRekening.Multiline = true;
            this.txtNamaRekening.Name = "txtNamaRekening";
            this.txtNamaRekening.Size = new System.Drawing.Size(321, 41);
            this.txtNamaRekening.TabIndex = 8;
            // 
            // txtIDrekening
            // 
            this.txtIDrekening.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIDrekening.Location = new System.Drawing.Point(141, 176);
            this.txtIDrekening.Name = "txtIDrekening";
            this.txtIDrekening.Size = new System.Drawing.Size(169, 20);
            this.txtIDrekening.TabIndex = 7;
            // 
            // gridBukuBesar
            // 
            this.gridBukuBesar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridBukuBesar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridBukuBesar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Tanggal,
            this.NoBukti,
            this.Keterangan,
            this.Debet,
            this.Kredit,
            this.NoJurnal,
            this.Inourutsumbar});
            this.gridBukuBesar.Location = new System.Drawing.Point(480, 81);
            this.gridBukuBesar.Name = "gridBukuBesar";
            this.gridBukuBesar.Size = new System.Drawing.Size(597, 385);
            this.gridBukuBesar.TabIndex = 6;
            // 
            // Tanggal
            // 
            this.Tanggal.HeaderText = "Tanggal";
            this.Tanggal.Name = "Tanggal";
            this.Tanggal.ReadOnly = true;
            this.Tanggal.Width = 80;
            // 
            // NoBukti
            // 
            this.NoBukti.HeaderText = "No Bukti";
            this.NoBukti.Name = "NoBukti";
            this.NoBukti.ReadOnly = true;
            this.NoBukti.Width = 150;
            // 
            // Keterangan
            // 
            this.Keterangan.HeaderText = "Keterangan";
            this.Keterangan.Name = "Keterangan";
            this.Keterangan.ReadOnly = true;
            this.Keterangan.Width = 250;
            // 
            // Debet
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Debet.DefaultCellStyle = dataGridViewCellStyle1;
            this.Debet.HeaderText = "Debet";
            this.Debet.Name = "Debet";
            this.Debet.ReadOnly = true;
            this.Debet.Width = 120;
            // 
            // Kredit
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Kredit.DefaultCellStyle = dataGridViewCellStyle2;
            this.Kredit.HeaderText = "Kredit";
            this.Kredit.Name = "Kredit";
            this.Kredit.ReadOnly = true;
            this.Kredit.Width = 120;
            // 
            // NoJurnal
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.NoJurnal.DefaultCellStyle = dataGridViewCellStyle3;
            this.NoJurnal.HeaderText = "Saldo";
            this.NoJurnal.Name = "NoJurnal";
            this.NoJurnal.ReadOnly = true;
            this.NoJurnal.Width = 120;
            // 
            // Inourutsumbar
            // 
            this.Inourutsumbar.HeaderText = "No Urut Sumber";
            this.Inourutsumbar.Name = "Inourutsumbar";
            this.Inourutsumbar.ReadOnly = true;
            // 
            // cmdPanggil
            // 
            this.cmdPanggil.Location = new System.Drawing.Point(490, 52);
            this.cmdPanggil.Name = "cmdPanggil";
            this.cmdPanggil.Size = new System.Drawing.Size(95, 23);
            this.cmdPanggil.TabIndex = 5;
            this.cmdPanggil.Text = "Panggil Data";
            this.cmdPanggil.UseVisualStyleBackColor = true;
            this.cmdPanggil.Click += new System.EventHandler(this.cmdPanggil_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Tanggal";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 80;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "No Bukti";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Keterangan";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 300;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Debet";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Kredit";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 150;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "No Jurnal";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 150;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "No Urut Sumber";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // ctrlPencarian1
            // 
            this.ctrlPencarian1.Location = new System.Drawing.Point(750, 52);
            this.ctrlPencarian1.Name = "ctrlPencarian1";
            this.ctrlPencarian1.Size = new System.Drawing.Size(327, 28);
            this.ctrlPencarian1.TabIndex = 9;
            this.ctrlPencarian1.Load += new System.EventHandler(this.ctrlPencarian1_Load);
            // 
            // treeRekening1
            // 
            this.treeRekening1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeRekening1.BackColor = System.Drawing.SystemColors.Control;
            this.treeRekening1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeRekening1.Location = new System.Drawing.Point(7, 258);
            this.treeRekening1.Name = "treeRekening1";
            this.treeRekening1.Profile = 1;
            this.treeRekening1.Size = new System.Drawing.Size(614, 208);
            this.treeRekening1.TabIndex = 4;
            this.treeRekening1.DoubleClicking += new KUAPPAS.treeRekening.ValueChangedEventHandler(this.treeRekening1_DoubleClicking);
            this.treeRekening1.Load += new System.EventHandler(this.treeRekening1_Load);
            this.treeRekening1.DoubleClick += new System.EventHandler(this.treeRekening1_DoubleClick);
            // 
            // ctrlTanggalBulanVertikal1
            // 
            this.ctrlTanggalBulanVertikal1.Bulan = 5;
            this.ctrlTanggalBulanVertikal1.JenisPeriode = 1;
            this.ctrlTanggalBulanVertikal1.Location = new System.Drawing.Point(26, 84);
            this.ctrlTanggalBulanVertikal1.Name = "ctrlTanggalBulanVertikal1";
            this.ctrlTanggalBulanVertikal1.Size = new System.Drawing.Size(404, 75);
            this.ctrlTanggalBulanVertikal1.TabIndex = 3;
            this.ctrlTanggalBulanVertikal1.TanggalAkhir = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            this.ctrlTanggalBulanVertikal1.TanggalAwal = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            // 
            // ctrlSKPD1
            // 
            this.ctrlSKPD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSKPD1.Location = new System.Drawing.Point(109, 52);
            this.ctrlSKPD1.Name = "ctrlSKPD1";
            this.ctrlSKPD1.Size = new System.Drawing.Size(365, 26);
            this.ctrlSKPD1.TabIndex = 2;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1077, 46);
            this.ctrlHeader1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 183);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "ID Rekening ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Nama";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(199, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Ketik atau cari dan  double klik di bawah";
            // 
            // txtSaldoAwal
            // 
            this.txtSaldoAwal.Location = new System.Drawing.Point(591, 54);
            this.txtSaldoAwal.Name = "txtSaldoAwal";
            this.txtSaldoAwal.Size = new System.Drawing.Size(153, 20);
            this.txtSaldoAwal.TabIndex = 13;
            // 
            // frmBukuBesar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 478);
            this.Controls.Add(this.txtSaldoAwal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlPencarian1);
            this.Controls.Add(this.txtNamaRekening);
            this.Controls.Add(this.txtIDrekening);
            this.Controls.Add(this.gridBukuBesar);
            this.Controls.Add(this.cmdPanggil);
            this.Controls.Add(this.treeRekening1);
            this.Controls.Add(this.ctrlTanggalBulanVertikal1);
            this.Controls.Add(this.ctrlSKPD1);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmBukuBesar";
            this.Text = "Buku Besar";
            this.Load += new System.EventHandler(this.frmBukuBesar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridBukuBesar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlHeader ctrlHeader1;
        private ctrlSKPD ctrlSKPD1;
        private Bendahara.ctrlTanggalBulanVertikal ctrlTanggalBulanVertikal1;
        private treeRekening treeRekening1;
        private System.Windows.Forms.Button cmdPanggil;
        private System.Windows.Forms.DataGridView gridBukuBesar;
        private System.Windows.Forms.TextBox txtIDrekening;
        private System.Windows.Forms.TextBox txtNamaRekening;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private ctrlPencarian ctrlPencarian1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tanggal;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoBukti;
        private System.Windows.Forms.DataGridViewTextBoxColumn Keterangan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Debet;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kredit;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoJurnal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Inourutsumbar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSaldoAwal;
    }
}