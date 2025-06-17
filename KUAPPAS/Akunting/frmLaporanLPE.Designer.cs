namespace KUAPPAS.Akunting
{
    partial class frmLaporanLPE
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.manu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdCetak = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.gridLPE = new System.Windows.Forms.DataGridView();
            this.Nomor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Uraian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TahunIni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TahunLalu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.ctrlTanggalBulanVertikal1 = new KUAPPAS.Bendahara.ctrlTanggalBulanVertikal();
            this.chkSemuaDinas = new System.Windows.Forms.CheckBox();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.chkPPKD = new System.Windows.Forms.CheckBox();
            this.ctrlTanggal1 = new KUAPPAS.ctrlTanggal();
            this.label1 = new System.Windows.Forms.Label();
            this.colorPickerButton1 = new Syncfusion.Windows.Forms.ColorPickerButton();
            this.cmdExcell = new System.Windows.Forms.Button();
            this.manu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLPE)).BeginInit();
            this.SuspendLayout();
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
            // 
            // cmdCetak
            // 
            this.cmdCetak.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCetak.Location = new System.Drawing.Point(810, 52);
            this.cmdCetak.Name = "cmdCetak";
            this.cmdCetak.Size = new System.Drawing.Size(87, 31);
            this.cmdCetak.TabIndex = 50;
            this.cmdCetak.Text = "Cetak";
            this.cmdCetak.UseVisualStyleBackColor = true;
            this.cmdCetak.Click += new System.EventHandler(this.cmdCetak_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 45;
            this.label3.Text = "O P D";
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1163, 46);
            this.ctrlHeader1.TabIndex = 43;
            // 
            // gridLPE
            // 
            this.gridLPE.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridLPE.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridLPE.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLPE.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nomor,
            this.Uraian,
            this.TahunIni,
            this.TahunLalu});
            this.gridLPE.Location = new System.Drawing.Point(402, 90);
            this.gridLPE.Name = "gridLPE";
            this.gridLPE.Size = new System.Drawing.Size(761, 447);
            this.gridLPE.TabIndex = 47;
            this.gridLPE.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridLPE_CellContentClick);
            // 
            // Nomor
            // 
            this.Nomor.HeaderText = "No";
            this.Nomor.Name = "Nomor";
            this.Nomor.ReadOnly = true;
            // 
            // Uraian
            // 
            this.Uraian.HeaderText = "Uraian";
            this.Uraian.Name = "Uraian";
            this.Uraian.ReadOnly = true;
            this.Uraian.Width = 300;
            // 
            // TahunIni
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.TahunIni.DefaultCellStyle = dataGridViewCellStyle3;
            this.TahunIni.HeaderText = "TahunIni";
            this.TahunIni.Name = "TahunIni";
            this.TahunIni.ReadOnly = true;
            this.TahunIni.Width = 150;
            // 
            // TahunLalu
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TahunLalu.DefaultCellStyle = dataGridViewCellStyle4;
            this.TahunLalu.HeaderText = "TahunLalu";
            this.TahunLalu.Name = "TahunLalu";
            this.TahunLalu.ReadOnly = true;
            this.TahunLalu.Width = 150;
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.BackColor = System.Drawing.SystemColors.Control;
            this.ctrlDinas1.Location = new System.Drawing.Point(15, 108);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(381, 56);
            this.ctrlDinas1.TabIndex = 44;
            this.ctrlDinas1.UK = 0;
            // 
            // ctrlTanggalBulanVertikal1
            // 
            this.ctrlTanggalBulanVertikal1.Bulan = 5;
            this.ctrlTanggalBulanVertikal1.JenisPeriode = 1;
            this.ctrlTanggalBulanVertikal1.Location = new System.Drawing.Point(8, 182);
            this.ctrlTanggalBulanVertikal1.Name = "ctrlTanggalBulanVertikal1";
            this.ctrlTanggalBulanVertikal1.Size = new System.Drawing.Size(388, 75);
            this.ctrlTanggalBulanVertikal1.TabIndex = 49;
            this.ctrlTanggalBulanVertikal1.TanggalAkhir = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            this.ctrlTanggalBulanVertikal1.TanggalAwal = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            this.ctrlTanggalBulanVertikal1.Load += new System.EventHandler(this.ctrlTanggalBulanVertikal1_Load);
            // 
            // chkSemuaDinas
            // 
            this.chkSemuaDinas.AutoSize = true;
            this.chkSemuaDinas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSemuaDinas.Location = new System.Drawing.Point(61, 65);
            this.chkSemuaDinas.Name = "chkSemuaDinas";
            this.chkSemuaDinas.Size = new System.Drawing.Size(100, 17);
            this.chkSemuaDinas.TabIndex = 48;
            this.chkSemuaDinas.Text = "Semua Dinas";
            this.chkSemuaDinas.UseVisualStyleBackColor = true;
            // 
            // cmdLoad
            // 
            this.cmdLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLoad.Location = new System.Drawing.Point(390, 48);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(89, 31);
            this.cmdLoad.TabIndex = 46;
            this.cmdLoad.Text = "Panggil Data";
            this.cmdLoad.UseVisualStyleBackColor = true;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // chkPPKD
            // 
            this.chkPPKD.AutoSize = true;
            this.chkPPKD.Location = new System.Drawing.Point(177, 62);
            this.chkPPKD.Name = "chkPPKD";
            this.chkPPKD.Size = new System.Drawing.Size(55, 17);
            this.chkPPKD.TabIndex = 51;
            this.chkPPKD.Text = "PPKD";
            this.chkPPKD.UseVisualStyleBackColor = true;
            // 
            // ctrlTanggal1
            // 
            this.ctrlTanggal1.Location = new System.Drawing.Point(698, 56);
            this.ctrlTanggal1.Name = "ctrlTanggal1";
            this.ctrlTanggal1.Size = new System.Drawing.Size(106, 25);
            this.ctrlTanggal1.TabIndex = 52;
            this.ctrlTanggal1.Tanggal = new System.DateTime(2025, 1, 26, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(597, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 53;
            this.label1.Text = "Tanggal Cetaak";
            // 
            // colorPickerButton1
            // 
            this.colorPickerButton1.Location = new System.Drawing.Point(818, 60);
            this.colorPickerButton1.Name = "colorPickerButton1";
            this.colorPickerButton1.Size = new System.Drawing.Size(75, 23);
            this.colorPickerButton1.TabIndex = 54;
            this.colorPickerButton1.Text = "colorPickerButton1";
            // 
            // cmdExcell
            // 
            this.cmdExcell.Location = new System.Drawing.Point(486, 52);
            this.cmdExcell.Name = "cmdExcell";
            this.cmdExcell.Size = new System.Drawing.Size(75, 23);
            this.cmdExcell.TabIndex = 55;
            this.cmdExcell.Text = "Excell";
            this.cmdExcell.UseVisualStyleBackColor = true;
            this.cmdExcell.Click += new System.EventHandler(this.cmdExcell_Click);
            // 
            // frmLaporanLPE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 537);
            this.Controls.Add(this.cmdExcell);
            this.Controls.Add(this.colorPickerButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlTanggal1);
            this.Controls.Add(this.chkPPKD);
            this.Controls.Add(this.cmdCetak);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.gridLPE);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.ctrlTanggalBulanVertikal1);
            this.Controls.Add(this.chkSemuaDinas);
            this.Controls.Add(this.cmdLoad);
            this.Name = "frmLaporanLPE";
            this.Text = "frmLaporanLPE";
            this.Load += new System.EventHandler(this.frmLaporanLPE_Load);
            this.manu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridLPE)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip manu;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.Button cmdCetak;
        private System.Windows.Forms.Label label3;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.DataGridView gridLPE;
        private ctrlDinas ctrlDinas1;
        private Bendahara.ctrlTanggalBulanVertikal ctrlTanggalBulanVertikal1;
        private System.Windows.Forms.CheckBox chkSemuaDinas;
        private System.Windows.Forms.Button cmdLoad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nomor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Uraian;
        private System.Windows.Forms.DataGridViewTextBoxColumn TahunIni;
        private System.Windows.Forms.DataGridViewTextBoxColumn TahunLalu;
        private System.Windows.Forms.CheckBox chkPPKD;
        private ctrlTanggal ctrlTanggal1;
        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.ColorPickerButton colorPickerButton1;
        private System.Windows.Forms.Button cmdExcell;
    }
}