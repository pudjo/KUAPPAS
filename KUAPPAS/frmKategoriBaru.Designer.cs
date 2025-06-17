namespace KUAPPAS
{
    partial class frmKategoriBaru
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
            this.gridKategori = new System.Windows.Forms.DataGridView();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.cmdSimpan = new System.Windows.Forms.Button();
            this.Hapus = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridKategori)).BeginInit();
            this.SuspendLayout();
            // 
            // gridKategori
            // 
            this.gridKategori.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridKategori.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Hapus,
            this.ID,
            this.Nama});
            this.gridKategori.Location = new System.Drawing.Point(1, 107);
            this.gridKategori.Name = "gridKategori";
            this.gridKategori.Size = new System.Drawing.Size(730, 230);
            this.gridKategori.TabIndex = 0;
            this.gridKategori.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridKategori_CellContentClick);
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(731, 52);
            this.ctrlHeader1.TabIndex = 1;
            // 
            // cmdSimpan
            // 
            this.cmdSimpan.Location = new System.Drawing.Point(12, 78);
            this.cmdSimpan.Name = "cmdSimpan";
            this.cmdSimpan.Size = new System.Drawing.Size(93, 23);
            this.cmdSimpan.TabIndex = 2;
            this.cmdSimpan.Text = "Simpan";
            this.cmdSimpan.UseVisualStyleBackColor = true;
            this.cmdSimpan.Click += new System.EventHandler(this.cmdSimpan_Click);
            // 
            // Hapus
            // 
            this.Hapus.HeaderText = "Hapus";
            this.Hapus.Name = "Hapus";
            this.Hapus.Width = 50;
            // 
            // ID
            // 
            this.ID.HeaderText = "Kode";
            this.ID.Name = "ID";
            this.ID.Width = 50;
            // 
            // Nama
            // 
            this.Nama.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Nama.HeaderText = "Nama";
            this.Nama.Name = "Nama";
            // 
            // frmKategoriBaru
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 349);
            this.Controls.Add(this.cmdSimpan);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.gridKategori);
            this.Name = "frmKategoriBaru";
            this.Text = "Daftar Kelompok UrusanBaru";
            this.Load += new System.EventHandler(this.frmKategoriBaru_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridKategori)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridKategori;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.Button cmdSimpan;
        private System.Windows.Forms.DataGridViewButtonColumn Hapus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
    }
}