namespace KUAPPAS.Bendahara
{
    partial class frmRegisterSPP
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
            this.cmdCariLagi = new System.Windows.Forms.Button();
            this.cmdCari = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.lblPencarian = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdTampilkan = new System.Windows.Forms.Button();
            this.gridSPP = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tanggal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoSPP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TglSPM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoSPM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TglSP2D = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoSP2D = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Uraian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LSBJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LSGJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlPeriode1 = new KUAPPAS.Bendahara.ctrlPeriode();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.cmdCetak = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txtSpasi = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridSPP)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdCariLagi
            // 
            this.cmdCariLagi.Location = new System.Drawing.Point(636, 141);
            this.cmdCariLagi.Name = "cmdCariLagi";
            this.cmdCariLagi.Size = new System.Drawing.Size(75, 23);
            this.cmdCariLagi.TabIndex = 34;
            this.cmdCariLagi.Text = "Cari Lagi";
            this.cmdCariLagi.UseVisualStyleBackColor = true;
            this.cmdCariLagi.Visible = false;
            this.cmdCariLagi.Click += new System.EventHandler(this.cmdCariLagi_Click);
            // 
            // cmdCari
            // 
            this.cmdCari.Location = new System.Drawing.Point(577, 142);
            this.cmdCari.Name = "cmdCari";
            this.cmdCari.Size = new System.Drawing.Size(62, 23);
            this.cmdCari.TabIndex = 33;
            this.cmdCari.Text = "Cari...";
            this.cmdCari.UseVisualStyleBackColor = true;
            this.cmdCari.Visible = false;
            this.cmdCari.Click += new System.EventHandler(this.cmdCari_Click);
            // 
            // txtCari
            // 
            this.txtCari.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCari.Location = new System.Drawing.Point(378, 142);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(202, 20);
            this.txtCari.TabIndex = 32;
            this.txtCari.Visible = false;
            // 
            // lblPencarian
            // 
            this.lblPencarian.AutoSize = true;
            this.lblPencarian.Location = new System.Drawing.Point(300, 145);
            this.lblPencarian.Name = "lblPencarian";
            this.lblPencarian.Size = new System.Drawing.Size(55, 13);
            this.lblPencarian.TabIndex = 31;
            this.lblPencarian.Text = "Pencarian";
            this.lblPencarian.Visible = false;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(717, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(147, 16);
            this.label7.TabIndex = 30;
            this.label7.Text = "Jumlah yang Tampil";
            // 
            // txtJumlah
            // 
            this.txtJumlah.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlah.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlah.Location = new System.Drawing.Point(870, 139);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(185, 26);
            this.txtJumlah.TabIndex = 29;
            this.txtJumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Periode\r\n";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "O P D";
            // 
            // cmdTampilkan
            // 
            this.cmdTampilkan.Location = new System.Drawing.Point(13, 135);
            this.cmdTampilkan.Name = "cmdTampilkan";
            this.cmdTampilkan.Size = new System.Drawing.Size(95, 29);
            this.cmdTampilkan.TabIndex = 4;
            this.cmdTampilkan.Text = "Tampilkan";
            this.cmdTampilkan.UseVisualStyleBackColor = true;
            this.cmdTampilkan.Click += new System.EventHandler(this.cmdTampilkan_Click);
            // 
            // gridSPP
            // 
            this.gridSPP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSPP.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridSPP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSPP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.Detail,
            this.Tanggal,
            this.NoSPP,
            this.TglSPM,
            this.NoSPM,
            this.TglSP2D,
            this.NoSP2D,
            this.Uraian,
            this.UP,
            this.GU,
            this.TU,
            this.LSBJ,
            this.LSGJ});
            this.gridSPP.Location = new System.Drawing.Point(0, 174);
            this.gridSPP.Name = "gridSPP";
            this.gridSPP.Size = new System.Drawing.Size(1068, 303);
            this.gridSPP.TabIndex = 3;
            // 
            // No
            // 
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.Visible = false;
            // 
            // Detail
            // 
            this.Detail.HeaderText = "Jenis";
            this.Detail.Name = "Detail";
            this.Detail.ReadOnly = true;
            this.Detail.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Detail.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Detail.Width = 50;
            // 
            // Tanggal
            // 
            this.Tanggal.HeaderText = "Tanggal SPP";
            this.Tanggal.Name = "Tanggal";
            this.Tanggal.ReadOnly = true;
            this.Tanggal.Width = 60;
            // 
            // NoSPP
            // 
            this.NoSPP.HeaderText = "No SPP";
            this.NoSPP.Name = "NoSPP";
            this.NoSPP.ReadOnly = true;
            this.NoSPP.Width = 180;
            // 
            // TglSPM
            // 
            this.TglSPM.HeaderText = "Tanggal SPM";
            this.TglSPM.Name = "TglSPM";
            this.TglSPM.ReadOnly = true;
            this.TglSPM.Width = 60;
            // 
            // NoSPM
            // 
            this.NoSPM.HeaderText = "No SPM";
            this.NoSPM.Name = "NoSPM";
            this.NoSPM.ReadOnly = true;
            this.NoSPM.Width = 180;
            // 
            // TglSP2D
            // 
            this.TglSP2D.HeaderText = "Tanggal SP2D";
            this.TglSP2D.Name = "TglSP2D";
            this.TglSP2D.ReadOnly = true;
            this.TglSP2D.Width = 60;
            // 
            // NoSP2D
            // 
            this.NoSP2D.HeaderText = "No SP2D";
            this.NoSP2D.Name = "NoSP2D";
            this.NoSP2D.ReadOnly = true;
            this.NoSP2D.Width = 180;
            // 
            // Uraian
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Uraian.DefaultCellStyle = dataGridViewCellStyle1;
            this.Uraian.HeaderText = "Uraian";
            this.Uraian.Name = "Uraian";
            this.Uraian.ReadOnly = true;
            this.Uraian.Width = 400;
            // 
            // UP
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.UP.DefaultCellStyle = dataGridViewCellStyle2;
            this.UP.HeaderText = "UP";
            this.UP.Name = "UP";
            this.UP.ReadOnly = true;
            this.UP.Width = 120;
            // 
            // GU
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GU.DefaultCellStyle = dataGridViewCellStyle3;
            this.GU.HeaderText = "GU";
            this.GU.Name = "GU";
            this.GU.ReadOnly = true;
            this.GU.Width = 120;
            // 
            // TU
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TU.DefaultCellStyle = dataGridViewCellStyle4;
            this.TU.HeaderText = "TU";
            this.TU.Name = "TU";
            this.TU.ReadOnly = true;
            this.TU.Width = 120;
            // 
            // LSBJ
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.LSBJ.DefaultCellStyle = dataGridViewCellStyle5;
            this.LSBJ.HeaderText = "LS Barang Jasa";
            this.LSBJ.Name = "LSBJ";
            this.LSBJ.ReadOnly = true;
            this.LSBJ.Width = 120;
            // 
            // LSGJ
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.LSGJ.DefaultCellStyle = dataGridViewCellStyle6;
            this.LSGJ.HeaderText = "LS Gaji/Tunjangan";
            this.LSGJ.Name = "LSGJ";
            this.LSGJ.ReadOnly = true;
            this.LSGJ.Width = 120;
            // 
            // ctrlPeriode1
            // 
            this.ctrlPeriode1.Location = new System.Drawing.Point(138, 97);
            this.ctrlPeriode1.Margin = new System.Windows.Forms.Padding(0);
            this.ctrlPeriode1.Name = "ctrlPeriode1";
            this.ctrlPeriode1.Size = new System.Drawing.Size(318, 26);
            this.ctrlPeriode1.TabIndex = 2;
            this.ctrlPeriode1.TanggalAkhir = new System.DateTime(2024, 3, 12, 0, 0, 0, 0);
            this.ctrlPeriode1.TanggalAwaal = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1068, 41);
            this.ctrlHeader1.TabIndex = 1;
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(138, 47);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(539, 47);
            this.ctrlDinas1.TabIndex = 0;
            // 
            // cmdCetak
            // 
            this.cmdCetak.Location = new System.Drawing.Point(115, 137);
            this.cmdCetak.Name = "cmdCetak";
            this.cmdCetak.Size = new System.Drawing.Size(75, 23);
            this.cmdCetak.TabIndex = 35;
            this.cmdCetak.Text = "Cetak";
            this.cmdCetak.UseVisualStyleBackColor = true;
            this.cmdCetak.Click += new System.EventHandler(this.cmdCetak_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(205, 131);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 13);
            this.label12.TabIndex = 56;
            this.label12.Text = "Spassi Cetak";
            // 
            // txtSpasi
            // 
            this.txtSpasi.BackColor = System.Drawing.Color.Bisque;
            this.txtSpasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSpasi.Location = new System.Drawing.Point(208, 144);
            this.txtSpasi.Name = "txtSpasi";
            this.txtSpasi.Size = new System.Drawing.Size(44, 20);
            this.txtSpasi.TabIndex = 55;
            this.txtSpasi.Text = "0";
            // 
            // frmRegisterSPP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 476);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtSpasi);
            this.Controls.Add(this.cmdCetak);
            this.Controls.Add(this.cmdCariLagi);
            this.Controls.Add(this.cmdCari);
            this.Controls.Add(this.txtCari);
            this.Controls.Add(this.lblPencarian);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdTampilkan);
            this.Controls.Add(this.gridSPP);
            this.Controls.Add(this.ctrlPeriode1);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.ctrlDinas1);
            this.Name = "frmRegisterSPP";
            this.Text = "Register SPP";
            this.Load += new System.EventHandler(this.frmRegisterSPP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSPP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlDinas ctrlDinas1;
        private ctrlHeader ctrlHeader1;
        private ctrlPeriode ctrlPeriode1;
        private System.Windows.Forms.DataGridView gridSPP;
        private System.Windows.Forms.Button cmdTampilkan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdCariLagi;
        private System.Windows.Forms.Button cmdCari;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Label lblPencarian;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Detail;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tanggal;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoSPP;
        private System.Windows.Forms.DataGridViewTextBoxColumn TglSPM;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoSPM;
        private System.Windows.Forms.DataGridViewTextBoxColumn TglSP2D;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoSP2D;
        private System.Windows.Forms.DataGridViewTextBoxColumn Uraian;
        private System.Windows.Forms.DataGridViewTextBoxColumn UP;
        private System.Windows.Forms.DataGridViewTextBoxColumn GU;
        private System.Windows.Forms.DataGridViewTextBoxColumn TU;
        private System.Windows.Forms.DataGridViewTextBoxColumn LSBJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn LSGJ;
        private System.Windows.Forms.Button cmdCetak;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtSpasi;
    }
}