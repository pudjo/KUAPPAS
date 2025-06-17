namespace KUAPPAS
{
    partial class frmListDinas
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
            this.gridSKPD = new System.Windows.Forms.DataGridView();
            this.Detail = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kategori = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Urusan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkSemuaUrusan = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.cmdTampilkan = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlUrusan1 = new KUAPPAS.ctrlUrusan();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            ((System.ComponentModel.ISupportInitialize)(this.gridSKPD)).BeginInit();
            this.SuspendLayout();
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
            this.gridSKPD.Location = new System.Drawing.Point(4, 136);
            this.gridSKPD.Name = "gridSKPD";
            this.gridSKPD.Size = new System.Drawing.Size(899, 353);
            this.gridSKPD.TabIndex = 0;
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
            // chkSemuaUrusan
            // 
            this.chkSemuaUrusan.AutoSize = true;
            this.chkSemuaUrusan.Checked = true;
            this.chkSemuaUrusan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSemuaUrusan.Location = new System.Drawing.Point(163, 71);
            this.chkSemuaUrusan.Name = "chkSemuaUrusan";
            this.chkSemuaUrusan.Size = new System.Drawing.Size(59, 17);
            this.chkSemuaUrusan.TabIndex = 2;
            this.chkSemuaUrusan.Text = "Semua";
            this.chkSemuaUrusan.UseVisualStyleBackColor = true;
            this.chkSemuaUrusan.CheckedChanged += new System.EventHandler(this.chkSemuaUrusan_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Urusan Pemerintahan";
            // 
            // cmdAdd
            // 
            this.cmdAdd.BackColor = System.Drawing.Color.Lime;
            this.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdAdd.Location = new System.Drawing.Point(126, 101);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(147, 28);
            this.cmdAdd.TabIndex = 6;
            this.cmdAdd.Text = "Tambah";
            this.cmdAdd.UseVisualStyleBackColor = false;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdTampilkan
            // 
            this.cmdTampilkan.BackColor = System.Drawing.Color.Blue;
            this.cmdTampilkan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdTampilkan.Location = new System.Drawing.Point(4, 101);
            this.cmdTampilkan.Name = "cmdTampilkan";
            this.cmdTampilkan.Size = new System.Drawing.Size(122, 28);
            this.cmdTampilkan.TabIndex = 8;
            this.cmdTampilkan.Text = "Tampilkan";
            this.cmdTampilkan.UseVisualStyleBackColor = false;
            this.cmdTampilkan.Click += new System.EventHandler(this.cmdTampilkan_Click);
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
            this.dataGridViewTextBoxColumn2.HeaderText = "Kode";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 70;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "Nama";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Kategori";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Urusan";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "SKPD";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn6.Visible = false;
            // 
            // ctrlUrusan1
            // 
            this.ctrlUrusan1.Location = new System.Drawing.Point(228, 72);
            this.ctrlUrusan1.Name = "ctrlUrusan1";
            this.ctrlUrusan1.Size = new System.Drawing.Size(583, 21);
            this.ctrlUrusan1.TabIndex = 4;
            this.ctrlUrusan1.OnChanged += new KUAPPAS.ctrlUrusan.ValueChangedEventHandler(this.ctrlUrusan1_OnChanged);
            this.ctrlUrusan1.Load += new System.EventHandler(this.ctrlUrusan1_Load);
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(898, 63);
            this.ctrlHeader1.TabIndex = 9;
            this.ctrlHeader1.Load += new System.EventHandler(this.ctrlHeader1_Load);
            // 
            // frmListDinas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 501);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.cmdTampilkan);
            this.Controls.Add(this.cmdAdd);
            this.Controls.Add(this.ctrlUrusan1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkSemuaUrusan);
            this.Controls.Add(this.gridSKPD);
            this.Name = "frmListDinas";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmListDinas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSKPD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridSKPD;
        private System.Windows.Forms.CheckBox chkSemuaUrusan;
        private System.Windows.Forms.Label label1;
        private ctrlUrusan ctrlUrusan1;
        private System.Windows.Forms.DataGridViewButtonColumn Detail;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kategori;
        private System.Windows.Forms.DataGridViewTextBoxColumn Urusan;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKPD;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Button cmdTampilkan;
        private ctrlHeader ctrlHeader1;
    }
}

