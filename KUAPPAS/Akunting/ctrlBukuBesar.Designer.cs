namespace KUAPPAS.Akunting
{
    partial class ctrlBukuBesar
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIDrekening = new System.Windows.Forms.TextBox();
            this.gridBukuBesar = new System.Windows.Forms.DataGridView();
            this.TanggalTransaksi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoBukti = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Keterangan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Debet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoJurnal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Inourutsumbar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tanggal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdExcell = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlPencarian1 = new KUAPPAS.ctrlPencarian();
            this.ctrlSKPD1 = new KUAPPAS.ctrlSKPD();
            this.lblNamaRekening = new System.Windows.Forms.Label();
            this.txtSaldoAwal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridBukuBesar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "ID Rekening ";
            // 
            // txtIDrekening
            // 
            this.txtIDrekening.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIDrekening.Location = new System.Drawing.Point(106, 36);
            this.txtIDrekening.Name = "txtIDrekening";
            this.txtIDrekening.Size = new System.Drawing.Size(140, 20);
            this.txtIDrekening.TabIndex = 11;
            // 
            // gridBukuBesar
            // 
            this.gridBukuBesar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridBukuBesar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridBukuBesar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TanggalTransaksi,
            this.SKPD,
            this.NoBukti,
            this.Keterangan,
            this.Debet,
            this.Kredit,
            this.NoJurnal,
            this.Inourutsumbar});
            this.gridBukuBesar.Location = new System.Drawing.Point(-12, 105);
            this.gridBukuBesar.Name = "gridBukuBesar";
            this.gridBukuBesar.Size = new System.Drawing.Size(937, 334);
            this.gridBukuBesar.TabIndex = 13;
            this.gridBukuBesar.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridBukuBesar_CellContentClick);
            // 
            // TanggalTransaksi
            // 
            this.TanggalTransaksi.HeaderText = "Tanggal";
            this.TanggalTransaksi.Name = "TanggalTransaksi";
            this.TanggalTransaksi.ReadOnly = true;
            this.TanggalTransaksi.Width = 80;
            // 
            // SKPD
            // 
            this.SKPD.HeaderText = "SKPD";
            this.SKPD.Name = "SKPD";
            this.SKPD.Width = 200;
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
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Debet.DefaultCellStyle = dataGridViewCellStyle13;
            this.Debet.HeaderText = "Debet";
            this.Debet.Name = "Debet";
            this.Debet.ReadOnly = true;
            this.Debet.Width = 120;
            // 
            // Kredit
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Kredit.DefaultCellStyle = dataGridViewCellStyle14;
            this.Kredit.HeaderText = "Kredit";
            this.Kredit.Name = "Kredit";
            this.Kredit.ReadOnly = true;
            this.Kredit.Width = 120;
            // 
            // NoJurnal
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.NoJurnal.DefaultCellStyle = dataGridViewCellStyle15;
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
            // tanggal
            // 
            this.tanggal.CustomFormat = "dd MMM yyyy";
            this.tanggal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tanggal.Location = new System.Drawing.Point(106, 58);
            this.tanggal.Name = "tanggal";
            this.tanggal.Size = new System.Drawing.Size(102, 20);
            this.tanggal.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Sampai Tanggal";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "OPD";
            // 
            // cmdExcell
            // 
            this.cmdExcell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExcell.Location = new System.Drawing.Point(508, 71);
            this.cmdExcell.Name = "cmdExcell";
            this.cmdExcell.Size = new System.Drawing.Size(75, 23);
            this.cmdExcell.TabIndex = 19;
            this.cmdExcell.Text = "Excell";
            this.cmdExcell.UseVisualStyleBackColor = true;
            this.cmdExcell.Click += new System.EventHandler(this.cmdExcell_Click);
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
            this.dataGridViewTextBoxColumn2.HeaderText = "SKPD";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "No Bukti";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Keterangan";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 250;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle16;
            this.dataGridViewTextBoxColumn5.HeaderText = "Debet";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 120;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle17;
            this.dataGridViewTextBoxColumn6.HeaderText = "Kredit";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 120;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle18;
            this.dataGridViewTextBoxColumn7.HeaderText = "Saldo";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 120;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "No Urut Sumber";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // ctrlPencarian1
            // 
            this.ctrlPencarian1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlPencarian1.Location = new System.Drawing.Point(589, 71);
            this.ctrlPencarian1.Name = "ctrlPencarian1";
            this.ctrlPencarian1.Size = new System.Drawing.Size(327, 28);
            this.ctrlPencarian1.TabIndex = 18;
            // 
            // ctrlSKPD1
            // 
            this.ctrlSKPD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSKPD1.Location = new System.Drawing.Point(106, 9);
            this.ctrlSKPD1.Name = "ctrlSKPD1";
            this.ctrlSKPD1.Size = new System.Drawing.Size(390, 26);
            this.ctrlSKPD1.TabIndex = 14;
            this.ctrlSKPD1.Load += new System.EventHandler(this.ctrlSKPD1_Load);
            // 
            // lblNamaRekening
            // 
            this.lblNamaRekening.AutoSize = true;
            this.lblNamaRekening.Location = new System.Drawing.Point(253, 42);
            this.lblNamaRekening.Name = "lblNamaRekening";
            this.lblNamaRekening.Size = new System.Drawing.Size(35, 13);
            this.lblNamaRekening.TabIndex = 20;
            this.lblNamaRekening.Text = "label4";
            // 
            // txtSaldoAwal
            // 
            this.txtSaldoAwal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSaldoAwal.Location = new System.Drawing.Point(106, 84);
            this.txtSaldoAwal.Name = "txtSaldoAwal";
            this.txtSaldoAwal.Size = new System.Drawing.Size(153, 20);
            this.txtSaldoAwal.TabIndex = 21;
            this.txtSaldoAwal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSaldoAwal.TextChanged += new System.EventHandler(this.txtSaldoAwal_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Saldio Awal";
            // 
            // ctrlBukuBesar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSaldoAwal);
            this.Controls.Add(this.lblNamaRekening);
            this.Controls.Add(this.cmdExcell);
            this.Controls.Add(this.ctrlPencarian1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tanggal);
            this.Controls.Add(this.ctrlSKPD1);
            this.Controls.Add(this.gridBukuBesar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIDrekening);
            this.Name = "ctrlBukuBesar";
            this.Size = new System.Drawing.Size(928, 437);
            this.Load += new System.EventHandler(this.ctrlBukuBesar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridBukuBesar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIDrekening;
        private System.Windows.Forms.DataGridView gridBukuBesar;
        private ctrlSKPD ctrlSKPD1;
        private System.Windows.Forms.DateTimePicker tanggal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private ctrlPencarian ctrlPencarian1;
        private System.Windows.Forms.Button cmdExcell;
        private System.Windows.Forms.DataGridViewTextBoxColumn TanggalTransaksi;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKPD;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoBukti;
        private System.Windows.Forms.DataGridViewTextBoxColumn Keterangan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Debet;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kredit;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoJurnal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Inourutsumbar;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.Label lblNamaRekening;
        private System.Windows.Forms.TextBox txtSaldoAwal;
        private System.Windows.Forms.Label label4;
    }
}
