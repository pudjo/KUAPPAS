namespace KUAPPAS.Bendahara
{
    partial class frmBUDRegisterSP2D
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
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.ctrlSKPD1 = new KUAPPAS.ctrlSKPD();
            this.chkSemuaDinas = new System.Windows.Forms.CheckBox();
            this.rbBulan = new System.Windows.Forms.RadioButton();
            this.rbTanggal = new System.Windows.Forms.RadioButton();
            this.ctrlBulan1 = new KUAPPAS.ctrlBulan();
            this.ctrlPeriode1 = new KUAPPAS.Bendahara.ctrlPeriode();
            this.gridRegister = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaSKPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoSP2d = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Keterangan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jumlah = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TanggalTerbit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TanggalCair = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Penerima = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SumberDana = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdPanggilData = new System.Windows.Forms.Button();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdExportExcell = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridRegister)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(791, 41);
            this.ctrlHeader1.TabIndex = 0;
            // 
            // ctrlSKPD1
            // 
            this.ctrlSKPD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSKPD1.Location = new System.Drawing.Point(166, 43);
            this.ctrlSKPD1.Name = "ctrlSKPD1";
            this.ctrlSKPD1.Size = new System.Drawing.Size(585, 24);
            this.ctrlSKPD1.TabIndex = 1;
            // 
            // chkSemuaDinas
            // 
            this.chkSemuaDinas.AutoSize = true;
            this.chkSemuaDinas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSemuaDinas.Location = new System.Drawing.Point(64, 47);
            this.chkSemuaDinas.Name = "chkSemuaDinas";
            this.chkSemuaDinas.Size = new System.Drawing.Size(100, 17);
            this.chkSemuaDinas.TabIndex = 2;
            this.chkSemuaDinas.Text = "Semua Dinas";
            this.chkSemuaDinas.UseVisualStyleBackColor = true;
            // 
            // rbBulan
            // 
            this.rbBulan.AutoSize = true;
            this.rbBulan.Location = new System.Drawing.Point(64, 125);
            this.rbBulan.Name = "rbBulan";
            this.rbBulan.Size = new System.Drawing.Size(52, 17);
            this.rbBulan.TabIndex = 24;
            this.rbBulan.TabStop = true;
            this.rbBulan.Text = "Bulan";
            this.rbBulan.UseVisualStyleBackColor = true;
            // 
            // rbTanggal
            // 
            this.rbTanggal.AutoSize = true;
            this.rbTanggal.Location = new System.Drawing.Point(64, 102);
            this.rbTanggal.Name = "rbTanggal";
            this.rbTanggal.Size = new System.Drawing.Size(64, 17);
            this.rbTanggal.TabIndex = 23;
            this.rbTanggal.TabStop = true;
            this.rbTanggal.Text = "Tanggal";
            this.rbTanggal.UseVisualStyleBackColor = true;
            // 
            // ctrlBulan1
            // 
            this.ctrlBulan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlBulan1.Location = new System.Drawing.Point(254, 125);
            this.ctrlBulan1.Name = "ctrlBulan1";
            this.ctrlBulan1.Size = new System.Drawing.Size(318, 22);
            this.ctrlBulan1.TabIndex = 22;
            // 
            // ctrlPeriode1
            // 
            this.ctrlPeriode1.Location = new System.Drawing.Point(254, 101);
            this.ctrlPeriode1.Margin = new System.Windows.Forms.Padding(0);
            this.ctrlPeriode1.Name = "ctrlPeriode1";
            this.ctrlPeriode1.Size = new System.Drawing.Size(318, 21);
            this.ctrlPeriode1.TabIndex = 21;
            this.ctrlPeriode1.TanggalAkhir = new System.DateTime(2024, 2, 8, 0, 0, 0, 0);
            this.ctrlPeriode1.TanggalAwaal = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            // 
            // gridRegister
            // 
            this.gridRegister.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridRegister.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridRegister.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRegister.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.NamaSKPD,
            this.NoSP2d,
            this.Keterangan,
            this.Jumlah,
            this.TanggalTerbit,
            this.TanggalCair,
            this.Penerima,
            this.SumberDana});
            this.gridRegister.Location = new System.Drawing.Point(0, 202);
            this.gridRegister.Name = "gridRegister";
            this.gridRegister.Size = new System.Drawing.Size(791, 256);
            this.gridRegister.TabIndex = 25;
            // 
            // No
            // 
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.Width = 50;
            // 
            // NamaSKPD
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NamaSKPD.DefaultCellStyle = dataGridViewCellStyle1;
            this.NamaSKPD.HeaderText = "SKPD ";
            this.NamaSKPD.Name = "NamaSKPD";
            this.NamaSKPD.ReadOnly = true;
            this.NamaSKPD.Width = 200;
            // 
            // NoSP2d
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NoSP2d.DefaultCellStyle = dataGridViewCellStyle2;
            this.NoSP2d.HeaderText = "Nomor SP2D";
            this.NoSP2d.Name = "NoSP2d";
            this.NoSP2d.ReadOnly = true;
            this.NoSP2d.Width = 150;
            // 
            // Keterangan
            // 
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Keterangan.DefaultCellStyle = dataGridViewCellStyle3;
            this.Keterangan.HeaderText = "Keterangan";
            this.Keterangan.Name = "Keterangan";
            this.Keterangan.ReadOnly = true;
            this.Keterangan.Width = 300;
            // 
            // Jumlah
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Jumlah.DefaultCellStyle = dataGridViewCellStyle4;
            this.Jumlah.HeaderText = "Jumlah";
            this.Jumlah.Name = "Jumlah";
            this.Jumlah.ReadOnly = true;
            this.Jumlah.Width = 120;
            // 
            // TanggalTerbit
            // 
            this.TanggalTerbit.HeaderText = "Tanggal Terbit";
            this.TanggalTerbit.Name = "TanggalTerbit";
            this.TanggalTerbit.ReadOnly = true;
            // 
            // TanggalCair
            // 
            this.TanggalCair.HeaderText = "Tanggal Cair";
            this.TanggalCair.Name = "TanggalCair";
            this.TanggalCair.ReadOnly = true;
            // 
            // Penerima
            // 
            this.Penerima.HeaderText = "Penerima";
            this.Penerima.Name = "Penerima";
            this.Penerima.ReadOnly = true;
            // 
            // SumberDana
            // 
            this.SumberDana.HeaderText = "Sumber Dana";
            this.SumberDana.Name = "SumberDana";
            this.SumberDana.ReadOnly = true;
            // 
            // cmdPanggilData
            // 
            this.cmdPanggilData.BackColor = System.Drawing.Color.White;
            this.cmdPanggilData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdPanggilData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPanggilData.ForeColor = System.Drawing.Color.Black;
            this.cmdPanggilData.Image = global::KUAPPAS.Properties.Resources.search;
            this.cmdPanggilData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdPanggilData.Location = new System.Drawing.Point(14, 166);
            this.cmdPanggilData.Name = "cmdPanggilData";
            this.cmdPanggilData.Size = new System.Drawing.Size(145, 30);
            this.cmdPanggilData.TabIndex = 26;
            this.cmdPanggilData.Text = "Panggil Data";
            this.cmdPanggilData.UseVisualStyleBackColor = false;
            this.cmdPanggilData.Click += new System.EventHandler(this.cmdPanggilData_Click);
            // 
            // cmbStatus
            // 
            this.cmbStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(164, 73);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(585, 23);
            this.cmbStatus.TabIndex = 27;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Status";
            // 
            // cmdExportExcell
            // 
            this.cmdExportExcell.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdExportExcell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExportExcell.Image = global::KUAPPAS.Properties.Resources.excel_all;
            this.cmdExportExcell.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdExportExcell.Location = new System.Drawing.Point(166, 166);
            this.cmdExportExcell.Name = "cmdExportExcell";
            this.cmdExportExcell.Size = new System.Drawing.Size(154, 30);
            this.cmdExportExcell.TabIndex = 29;
            this.cmdExportExcell.Text = "Export ke Excell";
            this.cmdExportExcell.UseVisualStyleBackColor = true;
            this.cmdExportExcell.Click += new System.EventHandler(this.cmdExportExcell_Click);
            // 
            // frmBUDRegisterSP2D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 470);
            this.Controls.Add(this.cmdExportExcell);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.cmdPanggilData);
            this.Controls.Add(this.gridRegister);
            this.Controls.Add(this.rbBulan);
            this.Controls.Add(this.rbTanggal);
            this.Controls.Add(this.ctrlBulan1);
            this.Controls.Add(this.ctrlPeriode1);
            this.Controls.Add(this.chkSemuaDinas);
            this.Controls.Add(this.ctrlSKPD1);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmBUDRegisterSP2D";
            this.Text = "Register SP2D";
            this.Load += new System.EventHandler(this.frmRegisterSP2D_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridRegister)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlHeader ctrlHeader1;
        private ctrlSKPD ctrlSKPD1;
        private System.Windows.Forms.CheckBox chkSemuaDinas;
        private System.Windows.Forms.RadioButton rbBulan;
        private System.Windows.Forms.RadioButton rbTanggal;
        private ctrlBulan ctrlBulan1;
        private ctrlPeriode ctrlPeriode1;
        private System.Windows.Forms.DataGridView gridRegister;
        private System.Windows.Forms.Button cmdPanggilData;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaSKPD;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoSP2d;
        private System.Windows.Forms.DataGridViewTextBoxColumn Keterangan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jumlah;
        private System.Windows.Forms.DataGridViewTextBoxColumn TanggalTerbit;
        private System.Windows.Forms.DataGridViewTextBoxColumn TanggalCair;
        private System.Windows.Forms.DataGridViewTextBoxColumn Penerima;
        private System.Windows.Forms.DataGridViewTextBoxColumn SumberDana;
        private System.Windows.Forms.Button cmdExportExcell;
    }
}