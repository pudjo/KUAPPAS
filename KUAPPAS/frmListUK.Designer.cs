namespace KUAPPAS
{
    partial class frmListUK
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
            this.ctrlSKPD1 = new KUAPPAS.ctrlSKPD();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdTambah = new System.Windows.Forms.Button();
            this.gridSKPD = new System.Windows.Forms.DataGridView();
            this.Detail = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kategori = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Urusan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.cmdLoad = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridSKPD)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlSKPD1
            // 
            this.ctrlSKPD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSKPD1.Location = new System.Drawing.Point(105, 71);
            this.ctrlSKPD1.Name = "ctrlSKPD1";
            this.ctrlSKPD1.Size = new System.Drawing.Size(577, 24);
            this.ctrlSKPD1.TabIndex = 2;
            this.ctrlSKPD1.OnChanged += new KUAPPAS.ctrlSKPD.ValueChangedEventHandler(this.ctrlSKPD1_OnChanged);
            this.ctrlSKPD1.Load += new System.EventHandler(this.ctrlSKPD1_Load_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "SKPD";
            // 
            // cmdTambah
            // 
            this.cmdTambah.Location = new System.Drawing.Point(124, 97);
            this.cmdTambah.Name = "cmdTambah";
            this.cmdTambah.Size = new System.Drawing.Size(155, 26);
            this.cmdTambah.TabIndex = 4;
            this.cmdTambah.Text = "Tambah";
            this.cmdTambah.UseVisualStyleBackColor = true;
            this.cmdTambah.Click += new System.EventHandler(this.cmdTambah_Click);
            // 
            // gridSKPD
            // 
            this.gridSKPD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSKPD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSKPD.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Detail,
            this.ID,
            this.Kode,
            this.Nama,
            this.Kategori,
            this.Urusan,
            this.SKPD});
            this.gridSKPD.Location = new System.Drawing.Point(0, 130);
            this.gridSKPD.Name = "gridSKPD";
            this.gridSKPD.Size = new System.Drawing.Size(775, 313);
            this.gridSKPD.TabIndex = 5;
            this.gridSKPD.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSKPD_CellContentClick);
            // 
            // Detail
            // 
            this.Detail.HeaderText = "Detail";
            this.Detail.Name = "Detail";
            this.Detail.Width = 50;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // Kode
            // 
            this.Kode.HeaderText = "Kode";
            this.Kode.Name = "Kode";
            this.Kode.ReadOnly = true;
            this.Kode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Kode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Kode.Width = 70;
            // 
            // Nama
            // 
            this.Nama.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Nama.HeaderText = "Nama";
            this.Nama.Name = "Nama";
            this.Nama.ReadOnly = true;
            this.Nama.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Nama.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Kategori
            // 
            this.Kategori.HeaderText = "Kategori";
            this.Kategori.Name = "Kategori";
            this.Kategori.ReadOnly = true;
            this.Kategori.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Kategori.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Kategori.Visible = false;
            // 
            // Urusan
            // 
            this.Urusan.HeaderText = "Urusan";
            this.Urusan.Name = "Urusan";
            this.Urusan.ReadOnly = true;
            this.Urusan.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Urusan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Urusan.Visible = false;
            // 
            // SKPD
            // 
            this.SKPD.HeaderText = "SKPD";
            this.SKPD.Name = "SKPD";
            this.SKPD.ReadOnly = true;
            this.SKPD.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SKPD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SKPD.Visible = false;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(775, 62);
            this.ctrlHeader1.TabIndex = 6;
            // 
            // cmdLoad
            // 
            this.cmdLoad.Location = new System.Drawing.Point(13, 97);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(105, 26);
            this.cmdLoad.TabIndex = 7;
            this.cmdLoad.Text = "Tampilkan";
            this.cmdLoad.UseVisualStyleBackColor = true;
            // 
            // frmListUK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 445);
            this.Controls.Add(this.cmdLoad);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.gridSKPD);
            this.Controls.Add(this.cmdTambah);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlSKPD1);
            this.Name = "frmListUK";
            this.Text = "frmListUK";
            this.Load += new System.EventHandler(this.frmListUK_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSKPD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlSKPD ctrlSKPD1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdTambah;
        private System.Windows.Forms.DataGridView gridSKPD;
        private System.Windows.Forms.DataGridViewButtonColumn Detail;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kategori;
        private System.Windows.Forms.DataGridViewTextBoxColumn Urusan;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKPD;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.Button cmdLoad;
    }
}