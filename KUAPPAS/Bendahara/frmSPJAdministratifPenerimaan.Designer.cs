namespace KUAPPAS.Bendahara
{
    partial class frmSPJAdministratifPenerimaan
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
            this.ctrlSKPD1 = new KUAPPAS.ctrlSKPD();
            this.gridPenutupanKas = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoHuruf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Uraian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jumlah = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bold = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jumlah2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrlBulan1 = new KUAPPAS.ctrlBulan();
            this.ctrlTanggal1 = new KUAPPAS.ctrlTanggal();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdCetak = new System.Windows.Forms.Button();
            this.cmdPanggilData = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dtCetak = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.gridPenutupanKas)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlSKPD1
            // 
            this.ctrlSKPD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSKPD1.Location = new System.Drawing.Point(125, 52);
            this.ctrlSKPD1.Name = "ctrlSKPD1";
            this.ctrlSKPD1.Size = new System.Drawing.Size(592, 22);
            this.ctrlSKPD1.TabIndex = 48;
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
            this.bold,
            this.Jumlah2});
            this.gridPenutupanKas.Location = new System.Drawing.Point(125, 163);
            this.gridPenutupanKas.Name = "gridPenutupanKas";
            this.gridPenutupanKas.Size = new System.Drawing.Size(720, 328);
            this.gridPenutupanKas.TabIndex = 44;
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
            // Jumlah2
            // 
            this.Jumlah2.HeaderText = "Jumlah";
            this.Jumlah2.Name = "Jumlah2";
            this.Jumlah2.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "OPD";
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(845, 41);
            this.ctrlHeader1.TabIndex = 41;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Bulan";
            // 
            // ctrlBulan1
            // 
            this.ctrlBulan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlBulan1.Location = new System.Drawing.Point(125, 76);
            this.ctrlBulan1.Name = "ctrlBulan1";
            this.ctrlBulan1.Size = new System.Drawing.Size(241, 22);
            this.ctrlBulan1.TabIndex = 49;
            // 
            // ctrlTanggal1
            // 
            this.ctrlTanggal1.Location = new System.Drawing.Point(125, 100);
            this.ctrlTanggal1.Name = "ctrlTanggal1";
            this.ctrlTanggal1.Size = new System.Drawing.Size(241, 25);
            this.ctrlTanggal1.TabIndex = 51;
            this.ctrlTanggal1.Tanggal = new System.DateTime(2024, 2, 8, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(31, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "Tanggal Cetak";
            // 
            // cmdCetak
            // 
            this.cmdCetak.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdCetak.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCetak.Location = new System.Drawing.Point(454, 131);
            this.cmdCetak.Name = "cmdCetak";
            this.cmdCetak.Size = new System.Drawing.Size(123, 26);
            this.cmdCetak.TabIndex = 47;
            this.cmdCetak.Text = "Cetak";
            this.cmdCetak.UseVisualStyleBackColor = true;
            this.cmdCetak.Click += new System.EventHandler(this.cmdCetak_Click);
            // 
            // cmdPanggilData
            // 
            this.cmdPanggilData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdPanggilData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPanggilData.Location = new System.Drawing.Point(121, 131);
            this.cmdPanggilData.Name = "cmdPanggilData";
            this.cmdPanggilData.Size = new System.Drawing.Size(106, 26);
            this.cmdPanggilData.TabIndex = 45;
            this.cmdPanggilData.Text = "Panggil Data";
            this.cmdPanggilData.UseVisualStyleBackColor = true;
            this.cmdPanggilData.Click += new System.EventHandler(this.cmdPanggilData_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(251, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 60;
            this.label5.Text = "Tanggal Cetak";
            // 
            // dtCetak
            // 
            this.dtCetak.CustomFormat = "dd MMM yyyy";
            this.dtCetak.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtCetak.Location = new System.Drawing.Point(333, 135);
            this.dtCetak.Name = "dtCetak";
            this.dtCetak.Size = new System.Drawing.Size(108, 20);
            this.dtCetak.TabIndex = 59;
            // 
            // frmSPJAdministratifPenerimaan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 497);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtCetak);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ctrlTanggal1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ctrlBulan1);
            this.Controls.Add(this.ctrlSKPD1);
            this.Controls.Add(this.cmdCetak);
            this.Controls.Add(this.cmdPanggilData);
            this.Controls.Add(this.gridPenutupanKas);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmSPJAdministratifPenerimaan";
            this.Text = "frmSPJAdministratifPenerimaan";
            this.Load += new System.EventHandler(this.frmSPJAdministratifPenerimaan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridPenutupanKas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlSKPD ctrlSKPD1;
        private System.Windows.Forms.DataGridView gridPenutupanKas;
        private System.Windows.Forms.Label label1;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.Label label2;
        private ctrlBulan ctrlBulan1;
        private ctrlTanggal ctrlTanggal1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdCetak;
        private System.Windows.Forms.Button cmdPanggilData;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoHuruf;
        private System.Windows.Forms.DataGridViewTextBoxColumn Uraian;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jumlah;
        private System.Windows.Forms.DataGridViewTextBoxColumn bold;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jumlah2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtCetak;
    }
}