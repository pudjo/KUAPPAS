namespace KUAPPAS.Bendahara
{
    partial class frmListPenerimaan
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.lblJenis = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.cmdBKU = new System.Windows.Forms.Button();
            this.cmdCariLagi = new System.Windows.Forms.Button();
            this.cmdCari = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.lblPencarian = new System.Windows.Forms.Label();
            this.gridSTS = new System.Windows.Forms.DataGridView();
            this.DetailSTS = new System.Windows.Forms.DataGridViewButtonColumn();
            this.BKU = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pilihSTS = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Setor = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlNamaFileImportSTS1 = new KUAPPAS.Bendahara.ctrlNamaFileImportSTS();
            this.ctrlJenisPenerimaan1 = new KUAPPAS.Bendahara.ctrlJenisPenerimaan();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.ctrlPanelPencarian1 = new KUAPPAS.Bendahara.ctrlPanelPencarian();
            this.NoUrutSTS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TanggalSTS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoSTS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KeteranganSTS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PenyetorSTS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JumlahSTS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridSTS)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "File";
            // 
            // lblJenis
            // 
            this.lblJenis.AutoSize = true;
            this.lblJenis.Location = new System.Drawing.Point(4, 119);
            this.lblJenis.Name = "lblJenis";
            this.lblJenis.Size = new System.Drawing.Size(31, 13);
            this.lblJenis.TabIndex = 49;
            this.lblJenis.Text = "Jenis";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(781, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 47;
            this.label1.Text = "Jumlah";
            // 
            // txtJumlah
            // 
            this.txtJumlah.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlah.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlah.Location = new System.Drawing.Point(838, 200);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(189, 26);
            this.txtJumlah.TabIndex = 46;
            this.txtJumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmdBKU
            // 
            this.cmdBKU.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBKU.Location = new System.Drawing.Point(41, 209);
            this.cmdBKU.Name = "cmdBKU";
            this.cmdBKU.Size = new System.Drawing.Size(140, 23);
            this.cmdBKU.TabIndex = 37;
            this.cmdBKU.Text = "BKU kan Semua";
            this.cmdBKU.UseVisualStyleBackColor = true;
            this.cmdBKU.Click += new System.EventHandler(this.cmdBKU_Click);
            // 
            // cmdCariLagi
            // 
            this.cmdCariLagi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCariLagi.Location = new System.Drawing.Point(700, 203);
            this.cmdCariLagi.Name = "cmdCariLagi";
            this.cmdCariLagi.Size = new System.Drawing.Size(75, 23);
            this.cmdCariLagi.TabIndex = 36;
            this.cmdCariLagi.Text = "Cari Lagi";
            this.cmdCariLagi.UseVisualStyleBackColor = true;
            this.cmdCariLagi.Click += new System.EventHandler(this.cmdCariLagi_Click);
            // 
            // cmdCari
            // 
            this.cmdCari.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCari.Location = new System.Drawing.Point(632, 204);
            this.cmdCari.Name = "cmdCari";
            this.cmdCari.Size = new System.Drawing.Size(62, 23);
            this.cmdCari.TabIndex = 35;
            this.cmdCari.Text = "Cari...";
            this.cmdCari.UseVisualStyleBackColor = true;
            this.cmdCari.Click += new System.EventHandler(this.cmdCari_Click);
            // 
            // txtCari
            // 
            this.txtCari.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCari.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCari.Location = new System.Drawing.Point(387, 206);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(239, 20);
            this.txtCari.TabIndex = 34;
            // 
            // lblPencarian
            // 
            this.lblPencarian.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPencarian.AutoSize = true;
            this.lblPencarian.Location = new System.Drawing.Point(331, 210);
            this.lblPencarian.Name = "lblPencarian";
            this.lblPencarian.Size = new System.Drawing.Size(55, 13);
            this.lblPencarian.TabIndex = 33;
            this.lblPencarian.Text = "Pencarian";
            // 
            // gridSTS
            // 
            this.gridSTS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSTS.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridSTS.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gridSTS.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NoUrutSTS,
            this.DetailSTS,
            this.BKU,
            this.pilihSTS,
            this.TanggalSTS,
            this.NoSTS,
            this.KeteranganSTS,
            this.PenyetorSTS,
            this.JumlahSTS,
            this.Setor});
            this.gridSTS.Location = new System.Drawing.Point(0, 235);
            this.gridSTS.Name = "gridSTS";
            this.gridSTS.Size = new System.Drawing.Size(1029, 185);
            this.gridSTS.TabIndex = 1;
            this.gridSTS.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSTS_CellContentClick);
            // 
            // DetailSTS
            // 
            this.DetailSTS.HeaderText = "Detail";
            this.DetailSTS.Name = "DetailSTS";
            this.DetailSTS.ReadOnly = true;
            this.DetailSTS.Width = 70;
            // 
            // BKU
            // 
            this.BKU.HeaderText = "BKU kan";
            this.BKU.Name = "BKU";
            this.BKU.Width = 70;
            // 
            // pilihSTS
            // 
            this.pilihSTS.HeaderText = "Pilih";
            this.pilihSTS.Name = "pilihSTS";
            this.pilihSTS.Width = 40;
            // 
            // Setor
            // 
            this.Setor.HeaderText = "Setor Ke Kasda";
            this.Setor.Name = "Setor";
            this.Setor.ReadOnly = true;
            this.Setor.Width = 80;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "NoUrut";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Tanggal";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "No STS";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Keterangan";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 450;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Penyetor";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 250;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn6.HeaderText = "Jumlah";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // ctrlNamaFileImportSTS1
            // 
            this.ctrlNamaFileImportSTS1.Location = new System.Drawing.Point(68, 143);
            this.ctrlNamaFileImportSTS1.Name = "ctrlNamaFileImportSTS1";
            this.ctrlNamaFileImportSTS1.Size = new System.Drawing.Size(182, 23);
            this.ctrlNamaFileImportSTS1.TabIndex = 51;
            // 
            // ctrlJenisPenerimaan1
            // 
            this.ctrlJenisPenerimaan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlJenisPenerimaan1.Location = new System.Drawing.Point(71, 116);
            this.ctrlJenisPenerimaan1.Name = "ctrlJenisPenerimaan1";
            this.ctrlJenisPenerimaan1.Size = new System.Drawing.Size(538, 23);
            this.ctrlJenisPenerimaan1.TabIndex = 50;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1030, 41);
            this.ctrlHeader1.TabIndex = 3;
            // 
            // ctrlPanelPencarian1
            // 
            this.ctrlPanelPencarian1.Location = new System.Drawing.Point(-2, 41);
            this.ctrlPanelPencarian1.Name = "ctrlPanelPencarian1";
            this.ctrlPanelPencarian1.Size = new System.Drawing.Size(611, 180);
            this.ctrlPanelPencarian1.TabIndex = 2;
            this.ctrlPanelPencarian1.OnDisplay += new KUAPPAS.Bendahara.ctrlPanelPencarian.ValueChangedEventHandler(this.ctrlPanelPencarian1_OnDisplay);
            this.ctrlPanelPencarian1.OnAdd += new KUAPPAS.Bendahara.ctrlPanelPencarian.ValueChangedEventHandler(this.ctrlPanelPencarian1_OnAdd);
            this.ctrlPanelPencarian1.TanggalBerubah += new KUAPPAS.Bendahara.ctrlPanelPencarian.ValueChangedEventHandler(this.ctrlPanelPencarian1_TanggalBerubah);
            this.ctrlPanelPencarian1.DinasBerubah += new KUAPPAS.Bendahara.ctrlPanelPencarian.ValueChangedEventHandler(this.ctrlPanelPencarian1_DinasBerubah);
            this.ctrlPanelPencarian1.Load += new System.EventHandler(this.ctrlPanelPencarian1_Load);
            // 
            // NoUrutSTS
            // 
            this.NoUrutSTS.HeaderText = "NoUrut";
            this.NoUrutSTS.Name = "NoUrutSTS";
            this.NoUrutSTS.ReadOnly = true;
            this.NoUrutSTS.Visible = false;
            // 
            // TanggalSTS
            // 
            this.TanggalSTS.HeaderText = "Tanggal";
            this.TanggalSTS.Name = "TanggalSTS";
            this.TanggalSTS.ReadOnly = true;
            // 
            // NoSTS
            // 
            this.NoSTS.HeaderText = "No STS";
            this.NoSTS.Name = "NoSTS";
            this.NoSTS.ReadOnly = true;
            // 
            // KeteranganSTS
            // 
            this.KeteranganSTS.HeaderText = "Keterangan";
            this.KeteranganSTS.Name = "KeteranganSTS";
            this.KeteranganSTS.ReadOnly = true;
            this.KeteranganSTS.Width = 450;
            // 
            // PenyetorSTS
            // 
            this.PenyetorSTS.HeaderText = "Penyetor";
            this.PenyetorSTS.Name = "PenyetorSTS";
            this.PenyetorSTS.ReadOnly = true;
            this.PenyetorSTS.Width = 250;
            // 
            // JumlahSTS
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.JumlahSTS.DefaultCellStyle = dataGridViewCellStyle1;
            this.JumlahSTS.HeaderText = "Jumlah";
            this.JumlahSTS.Name = "JumlahSTS";
            this.JumlahSTS.ReadOnly = true;
            // 
            // frmListPenerimaan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 424);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ctrlNamaFileImportSTS1);
            this.Controls.Add(this.ctrlJenisPenerimaan1);
            this.Controls.Add(this.lblJenis);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.cmdBKU);
            this.Controls.Add(this.cmdCariLagi);
            this.Controls.Add(this.cmdCari);
            this.Controls.Add(this.txtCari);
            this.Controls.Add(this.lblPencarian);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.ctrlPanelPencarian1);
            this.Controls.Add(this.gridSTS);
            this.Name = "frmListPenerimaan";
            this.Text = "Daftar Penerimaan";
            this.Load += new System.EventHandler(this.frmListPenerimaan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSTS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridSTS;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private ctrlPanelPencarian ctrlPanelPencarian1;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.Button cmdCariLagi;
        private System.Windows.Forms.Button cmdCari;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Label lblPencarian;
        private System.Windows.Forms.Button cmdBKU;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtJumlah;
     //   private ctrlJenisPenerimaan ctrlJenisPenerimaan1;
        private System.Windows.Forms.Label lblJenis;
        private ctrlJenisPenerimaan ctrlJenisPenerimaan1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoUrutSTS;
        private System.Windows.Forms.DataGridViewButtonColumn DetailSTS;
        private System.Windows.Forms.DataGridViewButtonColumn BKU;
        private System.Windows.Forms.DataGridViewCheckBoxColumn pilihSTS;
        private System.Windows.Forms.DataGridViewTextBoxColumn TanggalSTS;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoSTS;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeteranganSTS;
        private System.Windows.Forms.DataGridViewTextBoxColumn PenyetorSTS;
        private System.Windows.Forms.DataGridViewTextBoxColumn JumlahSTS;
        private System.Windows.Forms.DataGridViewButtonColumn Setor;
        private ctrlNamaFileImportSTS ctrlNamaFileImportSTS1;
        private System.Windows.Forms.Label label2;
    }
}