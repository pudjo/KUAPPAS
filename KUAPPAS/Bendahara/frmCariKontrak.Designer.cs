namespace KUAPPAS.Bendahara
{
    partial class frmCariKontrak
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtNoKontrak = new System.Windows.Forms.TextBox();
            this.cmdCariLagi = new System.Windows.Forms.Button();
            this.cmdCari = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.lblPencarian = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdtampilkan = new System.Windows.Forms.Button();
            this.gridKontrak = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdbatal = new System.Windows.Forms.Button();
            this.lblNo = new System.Windows.Forms.Label();
            this.lblKeterangan = new System.Windows.Forms.Label();
            this.lblJumlah = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlTanggalBulan1 = new KUAPPAS.Bendahara.ctrlTanggalBulan();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tanggal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Keterangan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Perusahaan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nilai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridKontrak)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNoKontrak
            // 
            this.txtNoKontrak.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoKontrak.Location = new System.Drawing.Point(150, 87);
            this.txtNoKontrak.Name = "txtNoKontrak";
            this.txtNoKontrak.Size = new System.Drawing.Size(660, 20);
            this.txtNoKontrak.TabIndex = 54;
            // 
            // cmdCariLagi
            // 
            this.cmdCariLagi.Location = new System.Drawing.Point(735, 163);
            this.cmdCariLagi.Name = "cmdCariLagi";
            this.cmdCariLagi.Size = new System.Drawing.Size(75, 23);
            this.cmdCariLagi.TabIndex = 53;
            this.cmdCariLagi.Text = "Cari Lagi";
            this.cmdCariLagi.UseVisualStyleBackColor = true;
            // 
            // cmdCari
            // 
            this.cmdCari.Location = new System.Drawing.Point(667, 163);
            this.cmdCari.Name = "cmdCari";
            this.cmdCari.Size = new System.Drawing.Size(62, 23);
            this.cmdCari.TabIndex = 52;
            this.cmdCari.Text = "Cari...";
            this.cmdCari.UseVisualStyleBackColor = true;
            // 
            // txtCari
            // 
            this.txtCari.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCari.Location = new System.Drawing.Point(422, 166);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(239, 20);
            this.txtCari.TabIndex = 51;
            // 
            // lblPencarian
            // 
            this.lblPencarian.AutoSize = true;
            this.lblPencarian.Location = new System.Drawing.Point(361, 173);
            this.lblPencarian.Name = "lblPencarian";
            this.lblPencarian.Size = new System.Drawing.Size(55, 13);
            this.lblPencarian.TabIndex = 50;
            this.lblPencarian.Text = "Pencarian";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(41, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 16);
            this.label2.TabIndex = 49;
            this.label2.Text = "No Kontrak";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 48;
            this.label1.Text = "O P D";
            // 
            // cmdtampilkan
            // 
            this.cmdtampilkan.BackColor = System.Drawing.SystemColors.HotTrack;
            this.cmdtampilkan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdtampilkan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdtampilkan.ForeColor = System.Drawing.Color.White;
            this.cmdtampilkan.Location = new System.Drawing.Point(150, 153);
            this.cmdtampilkan.Name = "cmdtampilkan";
            this.cmdtampilkan.Size = new System.Drawing.Size(139, 33);
            this.cmdtampilkan.TabIndex = 44;
            this.cmdtampilkan.Text = "Tampilkan Data";
            this.cmdtampilkan.UseVisualStyleBackColor = false;
            this.cmdtampilkan.Click += new System.EventHandler(this.cmdtampilkan_Click);
            // 
            // gridKontrak
            // 
            this.gridKontrak.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridKontrak.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridKontrak.BackgroundColor = System.Drawing.Color.White;
            this.gridKontrak.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridKontrak.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gridKontrak.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridKontrak.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.No,
            this.Tanggal,
            this.Keterangan,
            this.Perusahaan,
            this.Nilai});
            this.gridKontrak.Location = new System.Drawing.Point(2, 203);
            this.gridKontrak.MultiSelect = false;
            this.gridKontrak.Name = "gridKontrak";
            this.gridKontrak.ReadOnly = true;
            this.gridKontrak.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gridKontrak.Size = new System.Drawing.Size(926, 230);
            this.gridKontrak.TabIndex = 43;
            this.gridKontrak.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridKontrak_CellContentClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 502);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(940, 22);
            this.statusStrip1.TabIndex = 56;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // cmdOK
            // 
            this.cmdOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOK.Location = new System.Drawing.Point(8, 459);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(80, 40);
            this.cmdOK.TabIndex = 57;
            this.cmdOK.Text = "Pilih";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdbatal
            // 
            this.cmdbatal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdbatal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdbatal.ForeColor = System.Drawing.Color.Red;
            this.cmdbatal.Location = new System.Drawing.Point(853, 449);
            this.cmdbatal.Name = "cmdbatal";
            this.cmdbatal.Size = new System.Drawing.Size(75, 46);
            this.cmdbatal.TabIndex = 58;
            this.cmdbatal.Text = "Batal";
            this.cmdbatal.UseVisualStyleBackColor = true;
            this.cmdbatal.Click += new System.EventHandler(this.cmdbatal_Click);
            // 
            // lblNo
            // 
            this.lblNo.AutoSize = true;
            this.lblNo.Location = new System.Drawing.Point(147, 437);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(16, 13);
            this.lblNo.TabIndex = 59;
            this.lblNo.Text = "...";
            // 
            // lblKeterangan
            // 
            this.lblKeterangan.AutoSize = true;
            this.lblKeterangan.Location = new System.Drawing.Point(147, 459);
            this.lblKeterangan.Name = "lblKeterangan";
            this.lblKeterangan.Size = new System.Drawing.Size(16, 13);
            this.lblKeterangan.TabIndex = 60;
            this.lblKeterangan.Text = "...";
            // 
            // lblJumlah
            // 
            this.lblJumlah.AutoSize = true;
            this.lblJumlah.Location = new System.Drawing.Point(147, 482);
            this.lblJumlah.Name = "lblJumlah";
            this.lblJumlah.Size = new System.Drawing.Size(16, 13);
            this.lblJumlah.TabIndex = 61;
            this.lblJumlah.Text = "...";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "No Kontrak";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Tanggal";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn4.HeaderText = "Keterangan";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 400;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "NamaPerusahaan";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn6.HeaderText = "Nilai";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn7.HeaderText = "";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // ctrlTanggalBulan1
            // 
            this.ctrlTanggalBulan1.Bulan = 1;
            this.ctrlTanggalBulan1.JenisPeriode = 1;
            this.ctrlTanggalBulan1.Location = new System.Drawing.Point(51, 108);
            this.ctrlTanggalBulan1.Name = "ctrlTanggalBulan1";
            this.ctrlTanggalBulan1.Size = new System.Drawing.Size(759, 46);
            this.ctrlTanggalBulan1.TabIndex = 55;
            this.ctrlTanggalBulan1.TanggalAkhir = new System.DateTime(2024, 4, 19, 0, 0, 0, 0);
            this.ctrlTanggalBulan1.TanggalAwal = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(940, 41);
            this.ctrlHeader1.TabIndex = 47;
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(150, 41);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(660, 54);
            this.ctrlDinas1.TabIndex = 46;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // No
            // 
            this.No.HeaderText = "No Kontrak";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            // 
            // Tanggal
            // 
            this.Tanggal.HeaderText = "Tanggal";
            this.Tanggal.Name = "Tanggal";
            this.Tanggal.ReadOnly = true;
            // 
            // Keterangan
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Keterangan.DefaultCellStyle = dataGridViewCellStyle1;
            this.Keterangan.HeaderText = "Keterangan";
            this.Keterangan.Name = "Keterangan";
            this.Keterangan.ReadOnly = true;
            this.Keterangan.Width = 400;
            // 
            // Perusahaan
            // 
            this.Perusahaan.HeaderText = "NamaPerusahaan";
            this.Perusahaan.Name = "Perusahaan";
            this.Perusahaan.ReadOnly = true;
            // 
            // Nilai
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Nilai.DefaultCellStyle = dataGridViewCellStyle2;
            this.Nilai.HeaderText = "Nilai";
            this.Nilai.Name = "Nilai";
            this.Nilai.ReadOnly = true;
            // 
            // frmCariKontrak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 524);
            this.Controls.Add(this.lblJumlah);
            this.Controls.Add(this.lblKeterangan);
            this.Controls.Add(this.lblNo);
            this.Controls.Add(this.cmdbatal);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ctrlTanggalBulan1);
            this.Controls.Add(this.txtNoKontrak);
            this.Controls.Add(this.cmdCariLagi);
            this.Controls.Add(this.cmdCari);
            this.Controls.Add(this.txtCari);
            this.Controls.Add(this.lblPencarian);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.cmdtampilkan);
            this.Controls.Add(this.gridKontrak);
            this.Name = "frmCariKontrak";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pencarian Kontrak/SPK";
            this.Load += new System.EventHandler(this.frmCariKontrak_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridKontrak)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNoKontrak;
        private System.Windows.Forms.Button cmdCariLagi;
        private System.Windows.Forms.Button cmdCari;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Label lblPencarian;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ctrlHeader ctrlHeader1;
        private ctrlDinas ctrlDinas1;
        private System.Windows.Forms.Button cmdtampilkan;
        private System.Windows.Forms.DataGridView gridKontrak;
        private ctrlTanggalBulan ctrlTanggalBulan1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdbatal;
        private System.Windows.Forms.Label lblNo;
        private System.Windows.Forms.Label lblKeterangan;
        private System.Windows.Forms.Label lblJumlah;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tanggal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Keterangan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Perusahaan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nilai;
    }
}