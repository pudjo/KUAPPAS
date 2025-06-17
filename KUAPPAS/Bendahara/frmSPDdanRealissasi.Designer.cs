namespace KUAPPAS.Bendahara
{
    partial class frmSPDdanRealissasi
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.ctrlProgramKegiatan1 = new KUAPPAS.ctrlProgramKegiatan();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoSPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tanggal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jumlah = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.NoRealisai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoSP2d = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TannggalR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JumlahR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdPanggilSPD = new System.Windows.Forms.Button();
            this.cmdPanggilRealisasi = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.ctrlPilihanRekeningAnggaran1 = new KUAPPAS.Bendahara.ctrlPilihanRekeningAnggaran();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(230, 25);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(539, 47);
            this.ctrlDinas1.TabIndex = 0;
            // 
            // ctrlProgramKegiatan1
            // 
            this.ctrlProgramKegiatan1.Location = new System.Drawing.Point(230, 69);
            this.ctrlProgramKegiatan1.Name = "ctrlProgramKegiatan1";
            this.ctrlProgramKegiatan1.Size = new System.Drawing.Size(558, 108);
            this.ctrlProgramKegiatan1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.NoSPD,
            this.Tanggal,
            this.Jumlah});
            this.dataGridView1.Location = new System.Drawing.Point(48, 242);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(434, 290);
            this.dataGridView1.TabIndex = 2;
            // 
            // No
            // 
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.Width = 50;
            // 
            // NoSPD
            // 
            this.NoSPD.HeaderText = "No SPD";
            this.NoSPD.Name = "NoSPD";
            this.NoSPD.ReadOnly = true;
            // 
            // Tanggal
            // 
            this.Tanggal.HeaderText = "Tanggal SPD";
            this.Tanggal.Name = "Tanggal";
            this.Tanggal.ReadOnly = true;
            // 
            // Jumlah
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Jumlah.DefaultCellStyle = dataGridViewCellStyle3;
            this.Jumlah.HeaderText = "Jumlah";
            this.Jumlah.Name = "Jumlah";
            this.Jumlah.ReadOnly = true;
            this.Jumlah.Width = 150;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "SKPD";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Urusan";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Program";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(112, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Kegiatan";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(112, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Sub Kegiatan";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NoRealisai,
            this.NoSP2d,
            this.TannggalR,
            this.JumlahR});
            this.dataGridView2.Location = new System.Drawing.Point(488, 242);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(419, 290);
            this.dataGridView2.TabIndex = 8;
            // 
            // NoRealisai
            // 
            this.NoRealisai.HeaderText = "No";
            this.NoRealisai.Name = "NoRealisai";
            this.NoRealisai.ReadOnly = true;
            this.NoRealisai.Width = 50;
            // 
            // NoSP2d
            // 
            this.NoSP2d.HeaderText = "No Bukti";
            this.NoSP2d.Name = "NoSP2d";
            this.NoSP2d.ReadOnly = true;
            // 
            // TannggalR
            // 
            this.TannggalR.HeaderText = "Tanggal";
            this.TannggalR.Name = "TannggalR";
            this.TannggalR.ReadOnly = true;
            // 
            // JumlahR
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.JumlahR.DefaultCellStyle = dataGridViewCellStyle4;
            this.JumlahR.HeaderText = "Jumlah";
            this.JumlahR.Name = "JumlahR";
            this.JumlahR.ReadOnly = true;
            this.JumlahR.Width = 250;
            // 
            // cmdPanggilSPD
            // 
            this.cmdPanggilSPD.Location = new System.Drawing.Point(48, 213);
            this.cmdPanggilSPD.Name = "cmdPanggilSPD";
            this.cmdPanggilSPD.Size = new System.Drawing.Size(89, 23);
            this.cmdPanggilSPD.TabIndex = 9;
            this.cmdPanggilSPD.Text = "Panggil SPD";
            this.cmdPanggilSPD.UseVisualStyleBackColor = true;
            // 
            // cmdPanggilRealisasi
            // 
            this.cmdPanggilRealisasi.Location = new System.Drawing.Point(488, 204);
            this.cmdPanggilRealisasi.Name = "cmdPanggilRealisasi";
            this.cmdPanggilRealisasi.Size = new System.Drawing.Size(97, 23);
            this.cmdPanggilRealisasi.TabIndex = 10;
            this.cmdPanggilRealisasi.Text = "Panggil Realisasi";
            this.cmdPanggilRealisasi.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(112, 185);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 79;
            this.label8.Text = "Rekening";
            // 
            // ctrlPilihanRekeningAnggaran1
            // 
            this.ctrlPilihanRekeningAnggaran1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlPilihanRekeningAnggaran1.Enabled = false;
            this.ctrlPilihanRekeningAnggaran1.Location = new System.Drawing.Point(233, 176);
            this.ctrlPilihanRekeningAnggaran1.Name = "ctrlPilihanRekeningAnggaran1";
            this.ctrlPilihanRekeningAnggaran1.Size = new System.Drawing.Size(555, 22);
            this.ctrlPilihanRekeningAnggaran1.TabIndex = 78;
            // 
            // frmSPDdanRealissasi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 544);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ctrlPilihanRekeningAnggaran1);
            this.Controls.Add(this.cmdPanggilRealisasi);
            this.Controls.Add(this.cmdPanggilSPD);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ctrlProgramKegiatan1);
            this.Controls.Add(this.ctrlDinas1);
            this.Name = "frmSPDdanRealissasi";
            this.Text = "SPD dsn Realisasi";
            this.Load += new System.EventHandler(this.frmSPDdanRealissasi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlDinas ctrlDinas1;
        private ctrlProgramKegiatan ctrlProgramKegiatan1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoSPD;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tanggal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jumlah;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoRealisai;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoSP2d;
        private System.Windows.Forms.DataGridViewTextBoxColumn TannggalR;
        private System.Windows.Forms.DataGridViewTextBoxColumn JumlahR;
        private System.Windows.Forms.Button cmdPanggilSPD;
        private System.Windows.Forms.Button cmdPanggilRealisasi;
        private System.Windows.Forms.Label label8;
        private ctrlPilihanRekeningAnggaran ctrlPilihanRekeningAnggaran1;
    }
}