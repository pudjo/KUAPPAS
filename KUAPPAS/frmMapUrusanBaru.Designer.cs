namespace KUAPPAS
{
    partial class frmMapUrusanBaru
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grid1 = new System.Windows.Forms.DataGridView();
            this.cmdTambah = new System.Windows.Forms.Button();
            this.cmdHapus = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlUrusan1 = new KUAPPAS.ctrlUrusan();
            this.ctrlUrusanBaru1 = new KUAPPAS.ctrlUrusanBaru();
            this.IDUrusan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UrusanLama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDUrusanBaru = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaUrusanBaru = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDUrusan,
            this.UrusanLama,
            this.IDUrusanBaru,
            this.NamaUrusanBaru});
            this.dataGridView1.Location = new System.Drawing.Point(12, 293);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(751, 227);
            this.dataGridView1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(193, 256);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 31);
            this.button2.TabIndex = 2;
            this.button2.Text = "Simpan";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Urusan BAru";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Urusan Lama";
            // 
            // grid1
            // 
            this.grid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Nama});
            this.grid1.Location = new System.Drawing.Point(193, 162);
            this.grid1.Name = "grid1";
            this.grid1.Size = new System.Drawing.Size(471, 88);
            this.grid1.TabIndex = 8;
            // 
            // cmdTambah
            // 
            this.cmdTambah.Location = new System.Drawing.Point(193, 129);
            this.cmdTambah.Name = "cmdTambah";
            this.cmdTambah.Size = new System.Drawing.Size(75, 24);
            this.cmdTambah.TabIndex = 9;
            this.cmdTambah.Text = "Tambah";
            this.cmdTambah.UseVisualStyleBackColor = true;
            this.cmdTambah.Click += new System.EventHandler(this.cmdTambah_Click);
            // 
            // cmdHapus
            // 
            this.cmdHapus.Location = new System.Drawing.Point(285, 258);
            this.cmdHapus.Name = "cmdHapus";
            this.cmdHapus.Size = new System.Drawing.Size(121, 28);
            this.cmdHapus.TabIndex = 10;
            this.cmdHapus.Text = "Hapus Yang Dipilih";
            this.cmdHapus.UseVisualStyleBackColor = true;
            this.cmdHapus.Click += new System.EventHandler(this.cmdHapus_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "IDURusan";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Urusan Lama";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Kode Urusan Baru";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 50;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.HeaderText = "Nama Urusan Baru";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "ID";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 50;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.HeaderText = "Nama";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 50;
            // 
            // Nama
            // 
            this.Nama.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Nama.HeaderText = "Nama";
            this.Nama.Name = "Nama";
            this.Nama.ReadOnly = true;
            // 
            // ctrlUrusan1
            // 
            this.ctrlUrusan1.Location = new System.Drawing.Point(193, 67);
            this.ctrlUrusan1.Name = "ctrlUrusan1";
            this.ctrlUrusan1.Size = new System.Drawing.Size(471, 22);
            this.ctrlUrusan1.TabIndex = 7;
            this.ctrlUrusan1.OnChanged += new KUAPPAS.ctrlUrusan.ValueChangedEventHandler(this.ctrlUrusan1_OnChanged);
            this.ctrlUrusan1.Load += new System.EventHandler(this.ctrlUrusan1_Load);
            // 
            // ctrlUrusanBaru1
            // 
            this.ctrlUrusanBaru1.Location = new System.Drawing.Point(192, 99);
            this.ctrlUrusanBaru1.Name = "ctrlUrusanBaru1";
            this.ctrlUrusanBaru1.Size = new System.Drawing.Size(472, 24);
            this.ctrlUrusanBaru1.TabIndex = 4;
            // 
            // IDUrusan
            // 
            this.IDUrusan.HeaderText = "IDURusan";
            this.IDUrusan.Name = "IDUrusan";
            this.IDUrusan.ReadOnly = true;
            this.IDUrusan.Width = 50;
            // 
            // UrusanLama
            // 
            this.UrusanLama.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UrusanLama.HeaderText = "Urusan Lama";
            this.UrusanLama.Name = "UrusanLama";
            this.UrusanLama.ReadOnly = true;
            // 
            // IDUrusanBaru
            // 
            this.IDUrusanBaru.HeaderText = "Kode Urusan Baru";
            this.IDUrusanBaru.Name = "IDUrusanBaru";
            this.IDUrusanBaru.ReadOnly = true;
            this.IDUrusanBaru.Width = 50;
            // 
            // NamaUrusanBaru
            // 
            this.NamaUrusanBaru.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NamaUrusanBaru.HeaderText = "Nama Urusan Baru";
            this.NamaUrusanBaru.Name = "NamaUrusanBaru";
            this.NamaUrusanBaru.ReadOnly = true;
            // 
            // frmMapUrusanBaru
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 532);
            this.Controls.Add(this.cmdHapus);
            this.Controls.Add(this.cmdTambah);
            this.Controls.Add(this.grid1);
            this.Controls.Add(this.ctrlUrusan1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlUrusanBaru1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmMapUrusanBaru";
            this.Text = "frmMapUrusanBaru";
            this.Load += new System.EventHandler(this.frmMapUrusanBaru_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDUrusan;
        private System.Windows.Forms.DataGridViewTextBoxColumn UrusanLama;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDUrusanBaru;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaUrusanBaru;
        private System.Windows.Forms.Button button2;
        private ctrlUrusanBaru ctrlUrusanBaru1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private ctrlUrusan ctrlUrusan1;
        private System.Windows.Forms.DataGridView grid1;
        private System.Windows.Forms.Button cmdTambah;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Button cmdHapus;
    }
}