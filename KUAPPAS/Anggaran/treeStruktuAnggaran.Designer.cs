namespace KUAPPAS.Anggaran
{
    partial class treeStruktuAnggaran
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TreeRingkasan = new KUAPPAS.TreeGridView();
            this.Kode = new KUAPPAS.TreeGridColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Murni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Geser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RKAP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ABT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.TreeRingkasan)).BeginInit();
            this.SuspendLayout();
            // 
            // TreeRingkasan
            // 
            this.TreeRingkasan.AllowUserToAddRows = false;
            this.TreeRingkasan.AllowUserToDeleteRows = false;
            this.TreeRingkasan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeRingkasan.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.TreeRingkasan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Kode,
            this.Nama,
            this.Murni,
            this.Geser,
            this.RKAP,
            this.ABT,
            this.Level});
            this.TreeRingkasan.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.TreeRingkasan.ImageList = null;
            this.TreeRingkasan.Location = new System.Drawing.Point(3, 3);
            this.TreeRingkasan.Name = "TreeRingkasan";
            this.TreeRingkasan.Size = new System.Drawing.Size(690, 392);
            this.TreeRingkasan.TabIndex = 3;
            this.TreeRingkasan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TreeRingkasan_CellContentClick);
            // 
            // Kode
            // 
            this.Kode.DefaultNodeImage = null;
            this.Kode.HeaderText = "Kode";
            this.Kode.Name = "Kode";
            this.Kode.ReadOnly = true;
            this.Kode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Kode.Width = 200;
            // 
            // Nama
            // 
            this.Nama.HeaderText = "Nama";
            this.Nama.Name = "Nama";
            this.Nama.ReadOnly = true;
            this.Nama.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Nama.Width = 600;
            // 
            // Murni
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Murni.DefaultCellStyle = dataGridViewCellStyle1;
            this.Murni.HeaderText = "Anggaran Murni";
            this.Murni.Name = "Murni";
            this.Murni.ReadOnly = true;
            this.Murni.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Murni.Width = 150;
            // 
            // Geser
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Geser.DefaultCellStyle = dataGridViewCellStyle2;
            this.Geser.HeaderText = "Anggaran Geser";
            this.Geser.Name = "Geser";
            this.Geser.ReadOnly = true;
            this.Geser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Geser.Width = 150;
            // 
            // RKAP
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.RKAP.DefaultCellStyle = dataGridViewCellStyle3;
            this.RKAP.HeaderText = "Anggaran Perubahan";
            this.RKAP.Name = "RKAP";
            this.RKAP.ReadOnly = true;
            this.RKAP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RKAP.Width = 150;
            // 
            // ABT
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ABT.DefaultCellStyle = dataGridViewCellStyle4;
            this.ABT.HeaderText = "Anggaran Pergeseran Perubahan";
            this.ABT.Name = "ABT";
            this.ABT.ReadOnly = true;
            this.ABT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ABT.Width = 150;
            // 
            // Level
            // 
            this.Level.HeaderText = "Level";
            this.Level.Name = "Level";
            this.Level.ReadOnly = true;
            this.Level.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Level.Width = 80;
            // 
            // treeStruktuAnggaran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TreeRingkasan);
            this.Name = "treeStruktuAnggaran";
            this.Size = new System.Drawing.Size(696, 398);
            this.Load += new System.EventHandler(this.treeStruktuAnggaran_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TreeRingkasan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TreeGridView TreeRingkasan;
        private TreeGridColumn Kode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn Murni;
        private System.Windows.Forms.DataGridViewTextBoxColumn Geser;
        private System.Windows.Forms.DataGridViewTextBoxColumn RKAP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ABT;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level;
    }
}
