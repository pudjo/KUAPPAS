namespace KUAPPAS.Bendahara
{
    partial class frmSPJFungsionalPenerimaan
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label12 = new System.Windows.Forms.Label();
            this.txtSpasi = new System.Windows.Forms.TextBox();
            this.gridSPJPenerimaan = new System.Windows.Forms.DataGridView();
            this.Kode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AnggaranMurni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AnggaranGeser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AnggaranPerubahan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AnggaraABT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PenerimaanLalu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PenyetoranLalu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SisaSebelum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PenerimaanKini = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PenyetoranKini = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SisaKini = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Penerimaan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Penyetoran = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sisa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SisaAnggaran = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdLoadData = new System.Windows.Forms.Button();
            this.cmdExcell = new System.Windows.Forms.Button();
            this.cmdCetak = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlBulan1 = new KUAPPAS.ctrlBulan();
            this.ctrlSKPD1 = new KUAPPAS.ctrlSKPD();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.ctrlTahapAnggaran1 = new KUAPPAS.Anggaran.ctrlTahapAnggaran();
            this.label5 = new System.Windows.Forms.Label();
            this.dtCetak = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.gridSPJPenerimaan)).BeginInit();
            this.SuspendLayout();
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(264, 154);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 13);
            this.label12.TabIndex = 56;
            this.label12.Text = "Spassi Cetak";
            // 
            // txtSpasi
            // 
            this.txtSpasi.BackColor = System.Drawing.Color.Bisque;
            this.txtSpasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSpasi.Location = new System.Drawing.Point(267, 167);
            this.txtSpasi.Name = "txtSpasi";
            this.txtSpasi.Size = new System.Drawing.Size(44, 20);
            this.txtSpasi.TabIndex = 55;
            this.txtSpasi.Text = "0";
            // 
            // gridSPJPenerimaan
            // 
            this.gridSPJPenerimaan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSPJPenerimaan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSPJPenerimaan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Kode,
            this.Nama,
            this.AnggaranMurni,
            this.AnggaranGeser,
            this.AnggaranPerubahan,
            this.AnggaraABT,
            this.PenerimaanLalu,
            this.PenyetoranLalu,
            this.SisaSebelum,
            this.PenerimaanKini,
            this.PenyetoranKini,
            this.SisaKini,
            this.Penerimaan,
            this.Penyetoran,
            this.Sisa,
            this.SisaAnggaran});
            this.gridSPJPenerimaan.Location = new System.Drawing.Point(0, 194);
            this.gridSPJPenerimaan.Name = "gridSPJPenerimaan";
            this.gridSPJPenerimaan.Size = new System.Drawing.Size(1159, 271);
            this.gridSPJPenerimaan.TabIndex = 45;
            // 
            // Kode
            // 
            this.Kode.HeaderText = "Kode";
            this.Kode.Name = "Kode";
            this.Kode.ReadOnly = true;
            // 
            // Nama
            // 
            this.Nama.HeaderText = "Nama";
            this.Nama.Name = "Nama";
            this.Nama.ReadOnly = true;
            this.Nama.Width = 150;
            // 
            // AnggaranMurni
            // 
            this.AnggaranMurni.HeaderText = "Anggaran Murni";
            this.AnggaranMurni.Name = "AnggaranMurni";
            this.AnggaranMurni.ReadOnly = true;
            // 
            // AnggaranGeser
            // 
            this.AnggaranGeser.HeaderText = "Anggaran Pergeseran";
            this.AnggaranGeser.Name = "AnggaranGeser";
            this.AnggaranGeser.ReadOnly = true;
            this.AnggaranGeser.Visible = false;
            // 
            // AnggaranPerubahan
            // 
            this.AnggaranPerubahan.HeaderText = "Anggaran";
            this.AnggaranPerubahan.Name = "AnggaranPerubahan";
            this.AnggaranPerubahan.ReadOnly = true;
            this.AnggaranPerubahan.Visible = false;
            // 
            // AnggaraABT
            // 
            this.AnggaraABT.HeaderText = "Anggaran";
            this.AnggaraABT.Name = "AnggaraABT";
            this.AnggaraABT.ReadOnly = true;
            this.AnggaraABT.Visible = false;
            // 
            // PenerimaanLalu
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.PenerimaanLalu.DefaultCellStyle = dataGridViewCellStyle10;
            this.PenerimaanLalu.HeaderText = "Penerimaan Sebelum";
            this.PenerimaanLalu.Name = "PenerimaanLalu";
            this.PenerimaanLalu.ReadOnly = true;
            // 
            // PenyetoranLalu
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.PenyetoranLalu.DefaultCellStyle = dataGridViewCellStyle11;
            this.PenyetoranLalu.HeaderText = "Penyetoran Sebelum";
            this.PenyetoranLalu.Name = "PenyetoranLalu";
            this.PenyetoranLalu.ReadOnly = true;
            // 
            // SisaSebelum
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SisaSebelum.DefaultCellStyle = dataGridViewCellStyle12;
            this.SisaSebelum.HeaderText = "Sisa Sebelum";
            this.SisaSebelum.Name = "SisaSebelum";
            this.SisaSebelum.ReadOnly = true;
            // 
            // PenerimaanKini
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.PenerimaanKini.DefaultCellStyle = dataGridViewCellStyle13;
            this.PenerimaanKini.HeaderText = "Penerimaan Bulan Ini";
            this.PenerimaanKini.Name = "PenerimaanKini";
            this.PenerimaanKini.ReadOnly = true;
            // 
            // PenyetoranKini
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.PenyetoranKini.DefaultCellStyle = dataGridViewCellStyle14;
            this.PenyetoranKini.HeaderText = "Penyetoran Bulan Ini";
            this.PenyetoranKini.Name = "PenyetoranKini";
            this.PenyetoranKini.ReadOnly = true;
            // 
            // SisaKini
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SisaKini.DefaultCellStyle = dataGridViewCellStyle15;
            this.SisaKini.HeaderText = "Sisa Bulan Ini";
            this.SisaKini.Name = "SisaKini";
            this.SisaKini.ReadOnly = true;
            // 
            // Penerimaan
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Penerimaan.DefaultCellStyle = dataGridViewCellStyle16;
            this.Penerimaan.HeaderText = "Penerimaan";
            this.Penerimaan.Name = "Penerimaan";
            this.Penerimaan.ReadOnly = true;
            // 
            // Penyetoran
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Penyetoran.DefaultCellStyle = dataGridViewCellStyle17;
            this.Penyetoran.HeaderText = "Penyetoran";
            this.Penyetoran.Name = "Penyetoran";
            this.Penyetoran.ReadOnly = true;
            // 
            // Sisa
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Sisa.DefaultCellStyle = dataGridViewCellStyle18;
            this.Sisa.HeaderText = "Sisa";
            this.Sisa.Name = "Sisa";
            this.Sisa.ReadOnly = true;
            // 
            // SisaAnggaran
            // 
            this.SisaAnggaran.HeaderText = "Sisa Anggaran";
            this.SisaAnggaran.Name = "SisaAnggaran";
            this.SisaAnggaran.ReadOnly = true;
            // 
            // cmdLoadData
            // 
            this.cmdLoadData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdLoadData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLoadData.Location = new System.Drawing.Point(11, 155);
            this.cmdLoadData.Name = "cmdLoadData";
            this.cmdLoadData.Size = new System.Drawing.Size(135, 33);
            this.cmdLoadData.TabIndex = 44;
            this.cmdLoadData.Text = "Panggil Data";
            this.cmdLoadData.UseVisualStyleBackColor = true;
            this.cmdLoadData.Click += new System.EventHandler(this.cmdLoadData_Click);
            // 
            // cmdExcell
            // 
            this.cmdExcell.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdExcell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExcell.Location = new System.Drawing.Point(492, 155);
            this.cmdExcell.Name = "cmdExcell";
            this.cmdExcell.Size = new System.Drawing.Size(137, 33);
            this.cmdExcell.TabIndex = 43;
            this.cmdExcell.Text = "Excell";
            this.cmdExcell.UseVisualStyleBackColor = true;
            this.cmdExcell.Click += new System.EventHandler(this.cmdExcell_Click);
            // 
            // cmdCetak
            // 
            this.cmdCetak.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdCetak.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCetak.Location = new System.Drawing.Point(152, 155);
            this.cmdCetak.Name = "cmdCetak";
            this.cmdCetak.Size = new System.Drawing.Size(109, 33);
            this.cmdCetak.TabIndex = 42;
            this.cmdCetak.Text = "Cetak";
            this.cmdCetak.UseVisualStyleBackColor = true;
            this.cmdCetak.Click += new System.EventHandler(this.cmdCetak_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(67, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "Jenis Anggaran";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(67, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Bulan";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(67, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "O P D";
            // 
            // ctrlBulan1
            // 
            this.ctrlBulan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlBulan1.Location = new System.Drawing.Point(194, 81);
            this.ctrlBulan1.Name = "ctrlBulan1";
            this.ctrlBulan1.Size = new System.Drawing.Size(148, 22);
            this.ctrlBulan1.TabIndex = 19;
            // 
            // ctrlSKPD1
            // 
            this.ctrlSKPD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSKPD1.Location = new System.Drawing.Point(194, 53);
            this.ctrlSKPD1.Name = "ctrlSKPD1";
            this.ctrlSKPD1.Size = new System.Drawing.Size(707, 25);
            this.ctrlSKPD1.TabIndex = 18;
            this.ctrlSKPD1.OnChanged += new KUAPPAS.ctrlSKPD.ValueChangedEventHandler(this.ctrlSKPD1_OnChanged);
            this.ctrlSKPD1.Load += new System.EventHandler(this.ctrlSKPD1_Load);
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1159, 48);
            this.ctrlHeader1.TabIndex = 17;
            // 
            // ctrlTahapAnggaran1
            // 
            this.ctrlTahapAnggaran1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlTahapAnggaran1.ID = 0;
            this.ctrlTahapAnggaran1.Location = new System.Drawing.Point(194, 109);
            this.ctrlTahapAnggaran1.Name = "ctrlTahapAnggaran1";
            this.ctrlTahapAnggaran1.Size = new System.Drawing.Size(148, 23);
            this.ctrlTahapAnggaran1.TabIndex = 57;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(354, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 60;
            this.label5.Text = "Tanggal Cetak";
            // 
            // dtCetak
            // 
            this.dtCetak.CustomFormat = "dd MMM yyyy";
            this.dtCetak.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtCetak.Location = new System.Drawing.Point(354, 167);
            this.dtCetak.Name = "dtCetak";
            this.dtCetak.Size = new System.Drawing.Size(108, 20);
            this.dtCetak.TabIndex = 59;
            // 
            // frmSPJFungsionalPenerimaan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 467);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtCetak);
            this.Controls.Add(this.ctrlTahapAnggaran1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtSpasi);
            this.Controls.Add(this.gridSPJPenerimaan);
            this.Controls.Add(this.cmdLoadData);
            this.Controls.Add(this.cmdExcell);
            this.Controls.Add(this.cmdCetak);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlBulan1);
            this.Controls.Add(this.ctrlSKPD1);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmSPJFungsionalPenerimaan";
            this.Text = "SPJFungsionalPenerimaan";
            this.Load += new System.EventHandler(this.frmSPJFungsionalPenerimaan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSPJPenerimaan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlHeader ctrlHeader1;
        private ctrlSKPD ctrlSKPD1;
        private ctrlBulan ctrlBulan1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdLoadData;
        private System.Windows.Forms.Button cmdExcell;
        private System.Windows.Forms.Button cmdCetak;
        private System.Windows.Forms.DataGridView gridSPJPenerimaan;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtSpasi;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn AnggaranMurni;
        private System.Windows.Forms.DataGridViewTextBoxColumn AnggaranGeser;
        private System.Windows.Forms.DataGridViewTextBoxColumn AnggaranPerubahan;
        private System.Windows.Forms.DataGridViewTextBoxColumn AnggaraABT;
        private System.Windows.Forms.DataGridViewTextBoxColumn PenerimaanLalu;
        private System.Windows.Forms.DataGridViewTextBoxColumn PenyetoranLalu;
        private System.Windows.Forms.DataGridViewTextBoxColumn SisaSebelum;
        private System.Windows.Forms.DataGridViewTextBoxColumn PenerimaanKini;
        private System.Windows.Forms.DataGridViewTextBoxColumn PenyetoranKini;
        private System.Windows.Forms.DataGridViewTextBoxColumn SisaKini;
        private System.Windows.Forms.DataGridViewTextBoxColumn Penerimaan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Penyetoran;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sisa;
        private System.Windows.Forms.DataGridViewTextBoxColumn SisaAnggaran;
        private Anggaran.ctrlTahapAnggaran ctrlTahapAnggaran1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtCetak;
    }
}