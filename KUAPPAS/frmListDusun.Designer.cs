namespace KUAPPAS
{
    partial class frmListDusun
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
            this.gridDusun = new System.Windows.Forms.DataGridView();
            this.Detail = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaDusun = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaDesa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaKecamatan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdTambah = new System.Windows.Forms.Button();
            this.cmdKeluar = new System.Windows.Forms.Button();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            ((System.ComponentModel.ISupportInitialize)(this.gridDusun)).BeginInit();
            this.SuspendLayout();
            // 
            // gridDusun
            // 
            this.gridDusun.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridDusun.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDusun.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Detail,
            this.ID,
            this.Kode,
            this.NamaDusun,
            this.NamaDesa,
            this.NamaKecamatan});
            this.gridDusun.Location = new System.Drawing.Point(2, 89);
            this.gridDusun.Name = "gridDusun";
            this.gridDusun.Size = new System.Drawing.Size(732, 365);
            this.gridDusun.TabIndex = 0;
            this.gridDusun.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDusun_CellContentClick);
            // 
            // Detail
            // 
            this.Detail.HeaderText = "Detail";
            this.Detail.Name = "Detail";
            this.Detail.Width = 40;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            this.ID.Width = 40;
            // 
            // Kode
            // 
            this.Kode.HeaderText = "Kode";
            this.Kode.Name = "Kode";
            this.Kode.ReadOnly = true;
            this.Kode.Width = 50;
            // 
            // NamaDusun
            // 
            this.NamaDusun.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NamaDusun.HeaderText = "Nama Dusun";
            this.NamaDusun.Name = "NamaDusun";
            this.NamaDusun.ReadOnly = true;
            // 
            // NamaDesa
            // 
            this.NamaDesa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NamaDesa.HeaderText = "NamaDesa";
            this.NamaDesa.Name = "NamaDesa";
            this.NamaDesa.ReadOnly = true;
            // 
            // NamaKecamatan
            // 
            this.NamaKecamatan.HeaderText = "Nama Kecamatan";
            this.NamaKecamatan.Name = "NamaKecamatan";
            this.NamaKecamatan.ReadOnly = true;
            this.NamaKecamatan.Width = 200;
            // 
            // cmdTambah
            // 
            this.cmdTambah.Location = new System.Drawing.Point(2, 50);
            this.cmdTambah.Name = "cmdTambah";
            this.cmdTambah.Size = new System.Drawing.Size(102, 33);
            this.cmdTambah.TabIndex = 1;
            this.cmdTambah.Text = "Tambah";
            this.cmdTambah.UseVisualStyleBackColor = true;
            this.cmdTambah.Click += new System.EventHandler(this.cmdTambah_Click);
            // 
            // cmdKeluar
            // 
            this.cmdKeluar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdKeluar.Location = new System.Drawing.Point(641, 49);
            this.cmdKeluar.Name = "cmdKeluar";
            this.cmdKeluar.Size = new System.Drawing.Size(84, 31);
            this.cmdKeluar.TabIndex = 2;
            this.cmdKeluar.Text = "Keluar";
            this.cmdKeluar.UseVisualStyleBackColor = true;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(736, 43);
            this.ctrlHeader1.TabIndex = 3;
            // 
            // frmListDusun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 455);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.cmdKeluar);
            this.Controls.Add(this.cmdTambah);
            this.Controls.Add(this.gridDusun);
            this.Name = "frmListDusun";
            this.Text = "frmListDusun";
            this.Load += new System.EventHandler(this.frmListDusun_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridDusun)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridDusun;
        private System.Windows.Forms.Button cmdTambah;
        private System.Windows.Forms.Button cmdKeluar;
        private System.Windows.Forms.DataGridViewButtonColumn Detail;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kode;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaDusun;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaDesa;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaKecamatan;
        private ctrlHeader ctrlHeader1;
    }
}