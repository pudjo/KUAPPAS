namespace KUAPPAS.Bendahara
{
    partial class frmLRAAnngaranKas
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
            this.gridAnggaranKas = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idRek = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AK1Thun = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AKPeriode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.ctrlTanggal1 = new KUAPPAS.ctrlTanggal();
            this.cmdPanggil = new System.Windows.Forms.Button();
            this.cmdExcell = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrlTahapAnggaran1 = new KUAPPAS.Anggaran.ctrlTahapAnggaran();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridAnggaranKas)).BeginInit();
            this.SuspendLayout();
            // 
            // gridAnggaranKas
            // 
            this.gridAnggaranKas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridAnggaranKas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.idRek,
            this.Nama,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.AK1Thun,
            this.AKPeriode});
            this.gridAnggaranKas.Location = new System.Drawing.Point(3, 188);
            this.gridAnggaranKas.Name = "gridAnggaranKas";
            this.gridAnggaranKas.Size = new System.Drawing.Size(716, 196);
            this.gridAnggaranKas.TabIndex = 0;
            // 
            // No
            // 
            this.No.HeaderText = "Nomor";
            this.No.Name = "No";
            this.No.Width = 30;
            // 
            // idRek
            // 
            this.idRek.HeaderText = "ID Rekaning";
            this.idRek.Name = "idRek";
            this.idRek.ReadOnly = true;
            // 
            // Nama
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Nama.DefaultCellStyle = dataGridViewCellStyle1;
            this.Nama.HeaderText = "Nama";
            this.Nama.Name = "Nama";
            this.Nama.ReadOnly = true;
            this.Nama.Width = 300;
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "ANggaran";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.HeaderText = "Anggaran2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column3.HeaderText = "Anggaran3";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column4.HeaderText = "Anggaran4";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // AK1Thun
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.AK1Thun.DefaultCellStyle = dataGridViewCellStyle6;
            this.AK1Thun.HeaderText = "Anggaran Kas 1 Tahun";
            this.AK1Thun.Name = "AK1Thun";
            this.AK1Thun.ReadOnly = true;
            // 
            // AKPeriode
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.AKPeriode.DefaultCellStyle = dataGridViewCellStyle7;
            this.AKPeriode.HeaderText = "AK Periode";
            this.AKPeriode.Name = "AKPeriode";
            this.AKPeriode.ReadOnly = true;
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(123, 32);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(539, 48);
            this.ctrlDinas1.TabIndex = 1;
            // 
            // ctrlTanggal1
            // 
            this.ctrlTanggal1.Location = new System.Drawing.Point(123, 78);
            this.ctrlTanggal1.Name = "ctrlTanggal1";
            this.ctrlTanggal1.Size = new System.Drawing.Size(207, 25);
            this.ctrlTanggal1.TabIndex = 2;
            this.ctrlTanggal1.Tanggal = new System.DateTime(2024, 10, 19, 0, 0, 0, 0);
            // 
            // cmdPanggil
            // 
            this.cmdPanggil.Location = new System.Drawing.Point(123, 132);
            this.cmdPanggil.Name = "cmdPanggil";
            this.cmdPanggil.Size = new System.Drawing.Size(138, 38);
            this.cmdPanggil.TabIndex = 3;
            this.cmdPanggil.Text = "Panggil";
            this.cmdPanggil.UseVisualStyleBackColor = true;
            this.cmdPanggil.Click += new System.EventHandler(this.cmdPanggil_Click);
            // 
            // cmdExcell
            // 
            this.cmdExcell.Location = new System.Drawing.Point(267, 133);
            this.cmdExcell.Name = "cmdExcell";
            this.cmdExcell.Size = new System.Drawing.Size(120, 37);
            this.cmdExcell.TabIndex = 4;
            this.cmdExcell.Text = "Excell";
            this.cmdExcell.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Dinas";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tanggal";
            // 
            // ctrlTahapAnggaran1
            // 
            this.ctrlTahapAnggaran1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlTahapAnggaran1.ID = 0;
            this.ctrlTahapAnggaran1.Location = new System.Drawing.Point(123, 103);
            this.ctrlTahapAnggaran1.Name = "ctrlTahapAnggaran1";
            this.ctrlTahapAnggaran1.Size = new System.Drawing.Size(208, 23);
            this.ctrlTahapAnggaran1.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tahap Anggaran";
            // 
            // frmLRAAnngaranKas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 397);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ctrlTahapAnggaran1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdExcell);
            this.Controls.Add(this.cmdPanggil);
            this.Controls.Add(this.ctrlTanggal1);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.gridAnggaranKas);
            this.Name = "frmLRAAnngaranKas";
            this.Text = "Realisasi Aggaran Kas";
            this.Load += new System.EventHandler(this.frmLRAAnngaranKas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridAnggaranKas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridAnggaranKas;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn idRek;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn AK1Thun;
        private System.Windows.Forms.DataGridViewTextBoxColumn AKPeriode;
        private ctrlDinas ctrlDinas1;
        private ctrlTanggal ctrlTanggal1;
        private System.Windows.Forms.Button cmdPanggil;
        private System.Windows.Forms.Button cmdExcell;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Anggaran.ctrlTahapAnggaran ctrlTahapAnggaran1;
        private System.Windows.Forms.Label label3;
    }
}