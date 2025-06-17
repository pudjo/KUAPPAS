namespace KUAPPAS
{
    partial class frmListDesa
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
            this.gridDesa = new System.Windows.Forms.DataGridView();
            this.Detail = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kecamatan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaKecamatan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdTambah = new System.Windows.Forms.Button();
            this.cmdKeluar = new System.Windows.Forms.Button();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            ((System.ComponentModel.ISupportInitialize)(this.gridDesa)).BeginInit();
            this.SuspendLayout();
            // 
            // gridDesa
            // 
            this.gridDesa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridDesa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDesa.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Detail,
            this.ID,
            this.Kecamatan,
            this.Kode,
            this.Nama,
            this.NamaKecamatan});
            this.gridDesa.Location = new System.Drawing.Point(0, 75);
            this.gridDesa.Name = "gridDesa";
            this.gridDesa.Size = new System.Drawing.Size(748, 333);
            this.gridDesa.TabIndex = 0;
            this.gridDesa.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDesa_CellContentClick);
            // 
            // Detail
            // 
            this.Detail.HeaderText = "Detail";
            this.Detail.Name = "Detail";
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // Kecamatan
            // 
            this.Kecamatan.HeaderText = "Kecamatan";
            this.Kecamatan.Name = "Kecamatan";
            this.Kecamatan.ReadOnly = true;
            this.Kecamatan.Visible = false;
            // 
            // Kode
            // 
            this.Kode.HeaderText = "Kode";
            this.Kode.Name = "Kode";
            this.Kode.ReadOnly = true;
            this.Kode.Width = 50;
            // 
            // Nama
            // 
            this.Nama.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Nama.HeaderText = "Nama Desa";
            this.Nama.Name = "Nama";
            this.Nama.ReadOnly = true;
            // 
            // NamaKecamatan
            // 
            this.NamaKecamatan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NamaKecamatan.HeaderText = "Nama Kecamatan";
            this.NamaKecamatan.Name = "NamaKecamatan";
            this.NamaKecamatan.ReadOnly = true;
            // 
            // cmdTambah
            // 
            this.cmdTambah.Location = new System.Drawing.Point(0, 44);
            this.cmdTambah.Name = "cmdTambah";
            this.cmdTambah.Size = new System.Drawing.Size(92, 29);
            this.cmdTambah.TabIndex = 1;
            this.cmdTambah.Text = "Tambah";
            this.cmdTambah.UseVisualStyleBackColor = true;
            this.cmdTambah.Click += new System.EventHandler(this.cmdTambah_Click);
            // 
            // cmdKeluar
            // 
            this.cmdKeluar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdKeluar.Location = new System.Drawing.Point(648, 45);
            this.cmdKeluar.Name = "cmdKeluar";
            this.cmdKeluar.Size = new System.Drawing.Size(88, 29);
            this.cmdKeluar.TabIndex = 2;
            this.cmdKeluar.Text = "Keluar";
            this.cmdKeluar.UseVisualStyleBackColor = true;
            this.cmdKeluar.Click += new System.EventHandler(this.cmdKeluar_Click);
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(748, 41);
            this.ctrlHeader1.TabIndex = 3;
            // 
            // frmListDesa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 415);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.cmdKeluar);
            this.Controls.Add(this.cmdTambah);
            this.Controls.Add(this.gridDesa);
            this.Name = "frmListDesa";
            this.Text = "frmListDesa";
            this.Load += new System.EventHandler(this.frmListDesa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridDesa)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridDesa;
        private System.Windows.Forms.Button cmdTambah;
        private System.Windows.Forms.Button cmdKeluar;
        private System.Windows.Forms.DataGridViewButtonColumn Detail;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kecamatan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaKecamatan;
        private ctrlHeader ctrlHeader1;
    }
}