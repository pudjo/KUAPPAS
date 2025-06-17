namespace KUAPPAS.Akunting
{
    partial class frmLaporanRealisasi
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
            this.cmdExcell = new System.Windows.Forms.Button();
            this.cmdLoadData = new System.Windows.Forms.Button();
            this.cmdCetak = new System.Windows.Forms.Button();
            this.ctrlBulan1 = new KUAPPAS.ctrlBulan();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.gridRealisasi = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KodeRekening = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Uraian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Anggaran = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RealisasiSebelum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RealisasiPeriode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jumlah = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridRealisasi)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdExcell
            // 
            this.cmdExcell.Location = new System.Drawing.Point(329, 66);
            this.cmdExcell.Name = "cmdExcell";
            this.cmdExcell.Size = new System.Drawing.Size(137, 28);
            this.cmdExcell.TabIndex = 9;
            this.cmdExcell.Text = "Excell";
            this.cmdExcell.UseVisualStyleBackColor = true;
            // 
            // cmdLoadData
            // 
            this.cmdLoadData.Location = new System.Drawing.Point(113, 66);
            this.cmdLoadData.Name = "cmdLoadData";
            this.cmdLoadData.Size = new System.Drawing.Size(105, 28);
            this.cmdLoadData.TabIndex = 5;
            this.cmdLoadData.Text = "Panggil Data";
            this.cmdLoadData.UseVisualStyleBackColor = true;
            this.cmdLoadData.Click += new System.EventHandler(this.cmdLoadData_Click);
            // 
            // cmdCetak
            // 
            this.cmdCetak.Location = new System.Drawing.Point(224, 66);
            this.cmdCetak.Name = "cmdCetak";
            this.cmdCetak.Size = new System.Drawing.Size(99, 28);
            this.cmdCetak.TabIndex = 6;
            this.cmdCetak.Text = "Cetak";
            this.cmdCetak.UseVisualStyleBackColor = true;
            // 
            // ctrlBulan1
            // 
            this.ctrlBulan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlBulan1.Location = new System.Drawing.Point(113, 36);
            this.ctrlBulan1.Name = "ctrlBulan1";
            this.ctrlBulan1.Size = new System.Drawing.Size(499, 24);
            this.ctrlBulan1.TabIndex = 7;
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(21, 12);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(660, 28);
            this.ctrlDinas1.TabIndex = 8;
            // 
            // gridRealisasi
            // 
            this.gridRealisasi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridRealisasi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRealisasi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.KodeRekening,
            this.Uraian,
            this.Anggaran,
            this.RealisasiSebelum,
            this.RealisasiPeriode,
            this.Jumlah});
            this.gridRealisasi.Location = new System.Drawing.Point(-8, 100);
            this.gridRealisasi.Name = "gridRealisasi";
            this.gridRealisasi.Size = new System.Drawing.Size(1114, 381);
            this.gridRealisasi.TabIndex = 10;
            // 
            // No
            // 
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.Width = 50;
            // 
            // KodeRekening
            // 
            this.KodeRekening.HeaderText = "Kode Rekening";
            this.KodeRekening.Name = "KodeRekening";
            this.KodeRekening.Width = 150;
            // 
            // Uraian
            // 
            this.Uraian.HeaderText = "Uraian";
            this.Uraian.Name = "Uraian";
            this.Uraian.ReadOnly = true;
            this.Uraian.Width = 500;
            // 
            // Anggaran
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Anggaran.DefaultCellStyle = dataGridViewCellStyle1;
            this.Anggaran.HeaderText = "Anggaran";
            this.Anggaran.Name = "Anggaran";
            this.Anggaran.ReadOnly = true;
            this.Anggaran.Width = 150;
            // 
            // RealisasiSebelum
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.RealisasiSebelum.DefaultCellStyle = dataGridViewCellStyle2;
            this.RealisasiSebelum.HeaderText = "Realisasi Sebelum Periode";
            this.RealisasiSebelum.Name = "RealisasiSebelum";
            this.RealisasiSebelum.ReadOnly = true;
            this.RealisasiSebelum.Width = 150;
            // 
            // RealisasiPeriode
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.RealisasiPeriode.DefaultCellStyle = dataGridViewCellStyle3;
            this.RealisasiPeriode.HeaderText = "Realisasi Periode";
            this.RealisasiPeriode.Name = "RealisasiPeriode";
            this.RealisasiPeriode.ReadOnly = true;
            this.RealisasiPeriode.Width = 150;
            // 
            // Jumlah
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Jumlah.DefaultCellStyle = dataGridViewCellStyle4;
            this.Jumlah.HeaderText = "Jumlah Realisasi";
            this.Jumlah.Name = "Jumlah";
            this.Jumlah.ReadOnly = true;
            this.Jumlah.Width = 150;
            // 
            // frmLaporanRealisasi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 493);
            this.Controls.Add(this.gridRealisasi);
            this.Controls.Add(this.cmdExcell);
            this.Controls.Add(this.cmdLoadData);
            this.Controls.Add(this.cmdCetak);
            this.Controls.Add(this.ctrlBulan1);
            this.Controls.Add(this.ctrlDinas1);
            this.Name = "frmLaporanRealisasi";
            this.Text = "Realisasi";
            this.Load += new System.EventHandler(this.frmLaporanRealisasi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridRealisasi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdExcell;
        private System.Windows.Forms.Button cmdLoadData;
        private System.Windows.Forms.Button cmdCetak;
        private ctrlBulan ctrlBulan1;
        private ctrlDinas ctrlDinas1;
        private System.Windows.Forms.DataGridView gridRealisasi;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodeRekening;
        private System.Windows.Forms.DataGridViewTextBoxColumn Uraian;
        private System.Windows.Forms.DataGridViewTextBoxColumn Anggaran;
        private System.Windows.Forms.DataGridViewTextBoxColumn RealisasiSebelum;
        private System.Windows.Forms.DataGridViewTextBoxColumn RealisasiPeriode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jumlah;
    }
}