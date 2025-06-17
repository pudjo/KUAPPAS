namespace KUAPPAS.Bendahara
{
    partial class frmPengeluaranLaporanPenutupanKas
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
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNoPerbup = new System.Windows.Forms.TextBox();
            this.gridPenutupanKas = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoHuruf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Uraian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jumlah = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bold = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdPanggilData = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ctrlPeriode1 = new KUAPPAS.Bendahara.ctrlPeriode();
            this.rbTanggal = new System.Windows.Forms.RadioButton();
            this.tvBulan = new System.Windows.Forms.RadioButton();
            this.ctrlBulan1 = new KUAPPAS.ctrlBulan();
            this.cmdCetak = new System.Windows.Forms.Button();
            this.ctrlSKPD1 = new KUAPPAS.ctrlSKPD();
            this.label5 = new System.Windows.Forms.Label();
            this.dtCetak = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.gridPenutupanKas)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(848, 41);
            this.ctrlHeader1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "OPD";
            // 
            // txtNoPerbup
            // 
            this.txtNoPerbup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoPerbup.Location = new System.Drawing.Point(101, 136);
            this.txtNoPerbup.Name = "txtNoPerbup";
            this.txtNoPerbup.Size = new System.Drawing.Size(539, 20);
            this.txtNoPerbup.TabIndex = 5;
            // 
            // gridPenutupanKas
            // 
            this.gridPenutupanKas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPenutupanKas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPenutupanKas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.NoHuruf,
            this.Uraian,
            this.Jumlah,
            this.bold});
            this.gridPenutupanKas.Location = new System.Drawing.Point(102, 190);
            this.gridPenutupanKas.Name = "gridPenutupanKas";
            this.gridPenutupanKas.Size = new System.Drawing.Size(746, 296);
            this.gridPenutupanKas.TabIndex = 6;
            // 
            // No
            // 
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.Width = 20;
            // 
            // NoHuruf
            // 
            this.NoHuruf.HeaderText = "NomerHuruf";
            this.NoHuruf.Name = "NoHuruf";
            this.NoHuruf.ReadOnly = true;
            this.NoHuruf.Width = 20;
            // 
            // Uraian
            // 
            this.Uraian.HeaderText = "Keterangan";
            this.Uraian.Name = "Uraian";
            this.Uraian.ReadOnly = true;
            this.Uraian.Width = 500;
            // 
            // Jumlah
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Jumlah.DefaultCellStyle = dataGridViewCellStyle1;
            this.Jumlah.HeaderText = "Jumlah";
            this.Jumlah.Name = "Jumlah";
            this.Jumlah.ReadOnly = true;
            this.Jumlah.Width = 150;
            // 
            // bold
            // 
            this.bold.HeaderText = "Column1";
            this.bold.Name = "bold";
            this.bold.Visible = false;
            // 
            // cmdPanggilData
            // 
            this.cmdPanggilData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdPanggilData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPanggilData.Location = new System.Drawing.Point(103, 158);
            this.cmdPanggilData.Name = "cmdPanggilData";
            this.cmdPanggilData.Size = new System.Drawing.Size(106, 26);
            this.cmdPanggilData.TabIndex = 7;
            this.cmdPanggilData.Text = "Panggil Data";
            this.cmdPanggilData.UseVisualStyleBackColor = true;
            this.cmdPanggilData.Click += new System.EventHandler(this.cmdPanggilData_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ctrlPeriode1);
            this.groupBox1.Controls.Add(this.rbTanggal);
            this.groupBox1.Controls.Add(this.tvBulan);
            this.groupBox1.Controls.Add(this.ctrlBulan1);
            this.groupBox1.Location = new System.Drawing.Point(30, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(748, 48);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Periode";
            // 
            // ctrlPeriode1
            // 
            this.ctrlPeriode1.Location = new System.Drawing.Point(96, 19);
            this.ctrlPeriode1.Margin = new System.Windows.Forms.Padding(0);
            this.ctrlPeriode1.Name = "ctrlPeriode1";
            this.ctrlPeriode1.Size = new System.Drawing.Size(284, 26);
            this.ctrlPeriode1.TabIndex = 21;
            this.ctrlPeriode1.TanggalAkhir = new System.DateTime(2024, 3, 26, 0, 0, 0, 0);
            this.ctrlPeriode1.TanggalAwaal = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            // 
            // rbTanggal
            // 
            this.rbTanggal.AutoSize = true;
            this.rbTanggal.Checked = true;
            this.rbTanggal.Location = new System.Drawing.Point(29, 24);
            this.rbTanggal.Name = "rbTanggal";
            this.rbTanggal.Size = new System.Drawing.Size(64, 17);
            this.rbTanggal.TabIndex = 19;
            this.rbTanggal.TabStop = true;
            this.rbTanggal.Text = "Tanggal";
            this.rbTanggal.UseVisualStyleBackColor = true;
            // 
            // tvBulan
            // 
            this.tvBulan.AutoSize = true;
            this.tvBulan.Location = new System.Drawing.Point(442, 19);
            this.tvBulan.Name = "tvBulan";
            this.tvBulan.Size = new System.Drawing.Size(52, 17);
            this.tvBulan.TabIndex = 20;
            this.tvBulan.Text = "Bulan";
            this.tvBulan.UseVisualStyleBackColor = true;
            // 
            // ctrlBulan1
            // 
            this.ctrlBulan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlBulan1.Location = new System.Drawing.Point(514, 20);
            this.ctrlBulan1.Name = "ctrlBulan1";
            this.ctrlBulan1.Size = new System.Drawing.Size(213, 23);
            this.ctrlBulan1.TabIndex = 17;
            // 
            // cmdCetak
            // 
            this.cmdCetak.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdCetak.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCetak.Location = new System.Drawing.Point(216, 158);
            this.cmdCetak.Name = "cmdCetak";
            this.cmdCetak.Size = new System.Drawing.Size(123, 26);
            this.cmdCetak.TabIndex = 39;
            this.cmdCetak.Text = "Cetak";
            this.cmdCetak.UseVisualStyleBackColor = true;
            this.cmdCetak.Click += new System.EventHandler(this.cmdCetak_Click);
            // 
            // ctrlSKPD1
            // 
            this.ctrlSKPD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSKPD1.Location = new System.Drawing.Point(107, 47);
            this.ctrlSKPD1.Name = "ctrlSKPD1";
            this.ctrlSKPD1.Size = new System.Drawing.Size(592, 26);
            this.ctrlSKPD1.TabIndex = 40;
            this.ctrlSKPD1.Load += new System.EventHandler(this.ctrlSKPD1_Load);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(345, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 60;
            this.label5.Text = "Tanggal Cetak";
            // 
            // dtCetak
            // 
            this.dtCetak.CustomFormat = "dd MMM yyyy";
            this.dtCetak.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtCetak.Location = new System.Drawing.Point(441, 161);
            this.dtCetak.Name = "dtCetak";
            this.dtCetak.Size = new System.Drawing.Size(108, 20);
            this.dtCetak.TabIndex = 59;
            // 
            // frmPengeluaranLaporanPenutupanKas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 498);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtCetak);
            this.Controls.Add(this.ctrlSKPD1);
            this.Controls.Add(this.cmdCetak);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdPanggilData);
            this.Controls.Add(this.gridPenutupanKas);
            this.Controls.Add(this.txtNoPerbup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmPengeluaranLaporanPenutupanKas";
            this.Text = "Laporan Penutupan Kas";
            this.Load += new System.EventHandler(this.frmLaporanPenutupanKas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridPenutupanKas)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNoPerbup;
        private System.Windows.Forms.DataGridView gridPenutupanKas;
        private System.Windows.Forms.Button cmdPanggilData;
        private System.Windows.Forms.GroupBox groupBox1;
        private ctrlPeriode ctrlPeriode1;
        private System.Windows.Forms.RadioButton rbTanggal;
        private System.Windows.Forms.RadioButton tvBulan;
        private ctrlBulan ctrlBulan1;
        private System.Windows.Forms.Button cmdCetak;
        private ctrlSKPD ctrlSKPD1;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoHuruf;
        private System.Windows.Forms.DataGridViewTextBoxColumn Uraian;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jumlah;
        private System.Windows.Forms.DataGridViewTextBoxColumn bold;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtCetak;
    }
}