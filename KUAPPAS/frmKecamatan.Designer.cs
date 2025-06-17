namespace KUAPPAS
{
    partial class frmKecamatan
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
            this.gridKecamatan = new System.Windows.Forms.DataGridView();
            this.Detail = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.cmdKeluar = new System.Windows.Forms.Button();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            ((System.ComponentModel.ISupportInitialize)(this.gridKecamatan)).BeginInit();
            this.SuspendLayout();
            // 
            // gridKecamatan
            // 
            this.gridKecamatan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridKecamatan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridKecamatan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Detail,
            this.ID,
            this.Nama});
            this.gridKecamatan.Location = new System.Drawing.Point(1, 86);
            this.gridKecamatan.Name = "gridKecamatan";
            this.gridKecamatan.Size = new System.Drawing.Size(705, 365);
            this.gridKecamatan.TabIndex = 0;
            this.gridKecamatan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridKecamatan_CellContentClick);
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
            this.ID.Visible = false;
            // 
            // Nama
            // 
            this.Nama.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Nama.HeaderText = "Nama";
            this.Nama.Name = "Nama";
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(0, 50);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(80, 30);
            this.cmdAdd.TabIndex = 1;
            this.cmdAdd.Text = "Tambah";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdKeluar
            // 
            this.cmdKeluar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdKeluar.Location = new System.Drawing.Point(619, 50);
            this.cmdKeluar.Name = "cmdKeluar";
            this.cmdKeluar.Size = new System.Drawing.Size(75, 30);
            this.cmdKeluar.TabIndex = 3;
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
            this.ctrlHeader1.Size = new System.Drawing.Size(706, 44);
            this.ctrlHeader1.TabIndex = 4;
            // 
            // frmKecamatan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 458);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.cmdKeluar);
            this.Controls.Add(this.cmdAdd);
            this.Controls.Add(this.gridKecamatan);
            this.Name = "frmKecamatan";
            this.Text = "frmKecamatan";
            this.Load += new System.EventHandler(this.frmKecamatan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridKecamatan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridKecamatan;
        private System.Windows.Forms.DataGridViewButtonColumn Detail;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.Button cmdKeluar;
        private ctrlHeader ctrlHeader1;
    }
}