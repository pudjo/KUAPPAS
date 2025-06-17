namespace KUAPPAS
{
    partial class frmMappingUrusanBaru
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
            this.cmdBaru = new System.Windows.Forms.Button();
            this.ctrlUrusan1 = new KUAPPAS.ctrlUrusan();
            this.ctrlUrusanBaru1 = new KUAPPAS.ctrlUrusanBaru();
            this.cmdSimpan = new System.Windows.Forms.Button();
            this.gidMappingUrusan = new System.Windows.Forms.DataGridView();
            this.Hapus = new System.Windows.Forms.DataGridViewButtonColumn();
            this.IDURusanBaru = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDUrusanLama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KodeLama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaURusanLama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KodeBaru = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaBaru = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gidMappingUrusan)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdBaru
            // 
            this.cmdBaru.Location = new System.Drawing.Point(112, 46);
            this.cmdBaru.Name = "cmdBaru";
            this.cmdBaru.Size = new System.Drawing.Size(126, 26);
            this.cmdBaru.TabIndex = 0;
            this.cmdBaru.Text = "Mapping Baru";
            this.cmdBaru.UseVisualStyleBackColor = true;
            this.cmdBaru.Click += new System.EventHandler(this.cmdBaru_Click);
            // 
            // ctrlUrusan1
            // 
            this.ctrlUrusan1.Location = new System.Drawing.Point(180, 93);
            this.ctrlUrusan1.Name = "ctrlUrusan1";
            this.ctrlUrusan1.Size = new System.Drawing.Size(549, 25);
            this.ctrlUrusan1.TabIndex = 1;
            // 
            // ctrlUrusanBaru1
            // 
            this.ctrlUrusanBaru1.Location = new System.Drawing.Point(184, 130);
            this.ctrlUrusanBaru1.Name = "ctrlUrusanBaru1";
            this.ctrlUrusanBaru1.Size = new System.Drawing.Size(544, 24);
            this.ctrlUrusanBaru1.TabIndex = 2;
            // 
            // cmdSimpan
            // 
            this.cmdSimpan.Location = new System.Drawing.Point(187, 162);
            this.cmdSimpan.Name = "cmdSimpan";
            this.cmdSimpan.Size = new System.Drawing.Size(106, 33);
            this.cmdSimpan.TabIndex = 3;
            this.cmdSimpan.Text = "Simpan";
            this.cmdSimpan.UseVisualStyleBackColor = true;
            // 
            // gidMappingUrusan
            // 
            this.gidMappingUrusan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gidMappingUrusan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Hapus,
            this.IDURusanBaru,
            this.IDUrusanLama,
            this.KodeLama,
            this.NamaURusanLama,
            this.KodeBaru,
            this.NamaBaru});
            this.gidMappingUrusan.Location = new System.Drawing.Point(42, 206);
            this.gidMappingUrusan.Name = "gidMappingUrusan";
            this.gidMappingUrusan.Size = new System.Drawing.Size(782, 274);
            this.gidMappingUrusan.TabIndex = 4;
            // 
            // Hapus
            // 
            this.Hapus.HeaderText = "Hapus";
            this.Hapus.Name = "Hapus";
            // 
            // IDURusanBaru
            // 
            this.IDURusanBaru.HeaderText = "IDUrusanBaru";
            this.IDURusanBaru.Name = "IDURusanBaru";
            this.IDURusanBaru.ReadOnly = true;
            this.IDURusanBaru.Visible = false;
            // 
            // IDUrusanLama
            // 
            this.IDUrusanLama.HeaderText = "Urusan Lama";
            this.IDUrusanLama.Name = "IDUrusanLama";
            this.IDUrusanLama.ReadOnly = true;
            this.IDUrusanLama.Visible = false;
            // 
            // KodeLama
            // 
            this.KodeLama.HeaderText = "Kode Urusan Lama";
            this.KodeLama.Name = "KodeLama";
            this.KodeLama.ReadOnly = true;
            this.KodeLama.Width = 50;
            // 
            // NamaURusanLama
            // 
            this.NamaURusanLama.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NamaURusanLama.HeaderText = "Nama Urusan";
            this.NamaURusanLama.Name = "NamaURusanLama";
            this.NamaURusanLama.ReadOnly = true;
            // 
            // KodeBaru
            // 
            this.KodeBaru.HeaderText = "Kode Baru";
            this.KodeBaru.Name = "KodeBaru";
            this.KodeBaru.ReadOnly = true;
            this.KodeBaru.Width = 50;
            // 
            // NamaBaru
            // 
            this.NamaBaru.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NamaBaru.HeaderText = "Nama Urusan Baru";
            this.NamaBaru.Name = "NamaBaru";
            this.NamaBaru.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Urusan Lama";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Urusan Baru";
            // 
            // frmMappingUrusanBaru
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 489);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gidMappingUrusan);
            this.Controls.Add(this.cmdSimpan);
            this.Controls.Add(this.ctrlUrusanBaru1);
            this.Controls.Add(this.ctrlUrusan1);
            this.Controls.Add(this.cmdBaru);
            this.Name = "frmMappingUrusanBaru";
            this.Text = "frmMappingUrusan";
            this.Load += new System.EventHandler(this.frmMappingUrusanBaru_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gidMappingUrusan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdBaru;
        private ctrlUrusan ctrlUrusan1;
        private ctrlUrusanBaru ctrlUrusanBaru1;
        private System.Windows.Forms.Button cmdSimpan;
        private System.Windows.Forms.DataGridView gidMappingUrusan;
        private System.Windows.Forms.DataGridViewButtonColumn Hapus;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDURusanBaru;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDUrusanLama;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodeLama;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaURusanLama;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodeBaru;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaBaru;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}