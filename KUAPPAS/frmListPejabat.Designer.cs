namespace KUAPPAS
{
    partial class frmListPejabat
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
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.gridPejabat = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detail = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NPWP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jabatan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TanggalAktive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlJabatan1 = new KUAPPAS.ctrlJabatan();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdTampilkan = new System.Windows.Forms.Button();
            this.cmdTambah = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridPejabat)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(835, 41);
            this.ctrlHeader1.TabIndex = 0;
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(118, 47);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(539, 45);
            this.ctrlDinas1.TabIndex = 1;
            this.ctrlDinas1.OnChanged += new KUAPPAS.ctrlDinas.ValueChangedEventHandler(this.ctrlDinas1_OnChanged);
            // 
            // gridPejabat
            // 
            this.gridPejabat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPejabat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPejabat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Detail,
            this.Nama,
            this.NIP,
            this.NPWP,
            this.Jabatan,
            this.TanggalAktive});
            this.gridPejabat.Location = new System.Drawing.Point(0, 181);
            this.gridPejabat.Name = "gridPejabat";
            this.gridPejabat.Size = new System.Drawing.Size(835, 213);
            this.gridPejabat.TabIndex = 2;
            this.gridPejabat.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPejabat_CellContentClick);
            // 
            // ID
            // 
            this.ID.HeaderText = "Id";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Detail
            // 
            this.Detail.HeaderText = "Detail";
            this.Detail.Name = "Detail";
            this.Detail.ReadOnly = true;
            // 
            // Nama
            // 
            this.Nama.HeaderText = "Nama";
            this.Nama.Name = "Nama";
            this.Nama.ReadOnly = true;
            this.Nama.Width = 200;
            // 
            // NIP
            // 
            this.NIP.HeaderText = "NIP";
            this.NIP.Name = "NIP";
            this.NIP.ReadOnly = true;
            this.NIP.Width = 250;
            // 
            // NPWP
            // 
            this.NPWP.HeaderText = "NPWP";
            this.NPWP.Name = "NPWP";
            this.NPWP.ReadOnly = true;
            this.NPWP.Width = 150;
            // 
            // Jabatan
            // 
            this.Jabatan.HeaderText = "Jabatan";
            this.Jabatan.Name = "Jabatan";
            this.Jabatan.ReadOnly = true;
            this.Jabatan.Width = 200;
            // 
            // TanggalAktive
            // 
            this.TanggalAktive.HeaderText = "Tanggal Mulai AKtif";
            this.TanggalAktive.Name = "TanggalAktive";
            this.TanggalAktive.ReadOnly = true;
            this.TanggalAktive.Width = 200;
            // 
            // ctrlJabatan1
            // 
            this.ctrlJabatan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlJabatan1.ID = 0;
            this.ctrlJabatan1.Location = new System.Drawing.Point(118, 92);
            this.ctrlJabatan1.Name = "ctrlJabatan1";
            this.ctrlJabatan1.Size = new System.Drawing.Size(539, 24);
            this.ctrlJabatan1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(52, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "O P D";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(52, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Jabatan";
            // 
            // cmdTampilkan
            // 
            this.cmdTampilkan.Location = new System.Drawing.Point(26, 142);
            this.cmdTampilkan.Name = "cmdTampilkan";
            this.cmdTampilkan.Size = new System.Drawing.Size(148, 33);
            this.cmdTampilkan.TabIndex = 6;
            this.cmdTampilkan.Text = "Tampilkan";
            this.cmdTampilkan.UseVisualStyleBackColor = true;
            this.cmdTampilkan.Click += new System.EventHandler(this.cmdTampilkan_Click);
            // 
            // cmdTambah
            // 
            this.cmdTambah.Location = new System.Drawing.Point(180, 142);
            this.cmdTambah.Name = "cmdTambah";
            this.cmdTambah.Size = new System.Drawing.Size(141, 33);
            this.cmdTambah.TabIndex = 7;
            this.cmdTambah.Text = "Tambah Pejabat Baru";
            this.cmdTambah.UseVisualStyleBackColor = true;
            this.cmdTambah.Click += new System.EventHandler(this.cmdTambah_Click);
            // 
            // frmListPejabat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 406);
            this.Controls.Add(this.cmdTambah);
            this.Controls.Add(this.cmdTampilkan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlJabatan1);
            this.Controls.Add(this.gridPejabat);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmListPejabat";
            this.Text = "Daftar Pejabat";
            this.Load += new System.EventHandler(this.frmListPejabat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridPejabat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlHeader ctrlHeader1;
        private ctrlDinas ctrlDinas1;
        private System.Windows.Forms.DataGridView gridPejabat;
        private ctrlJabatan ctrlJabatan1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdTampilkan;
        private System.Windows.Forms.Button cmdTambah;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewButtonColumn Detail;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn NIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn NPWP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jabatan;
        private System.Windows.Forms.DataGridViewTextBoxColumn TanggalAktive;
    }
}