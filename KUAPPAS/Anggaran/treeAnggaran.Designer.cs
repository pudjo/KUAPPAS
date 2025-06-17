namespace KUAPPAS.Anggaran
{
    partial class treeAnggaran
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
            this.treeGridProgram = new KUAPPAS.TreeGridView();
            this.Detail = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Kode = new KUAPPAS.TreeGridColumn();
            this.Murni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Geser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RKAP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ABT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idsub = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kodeuk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SetUK = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.treeGridProgram)).BeginInit();
            this.SuspendLayout();
            // 
            // treeGridProgram
            // 
            this.treeGridProgram.AllowUserToAddRows = false;
            this.treeGridProgram.AllowUserToDeleteRows = false;
            this.treeGridProgram.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.treeGridProgram.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeGridProgram.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Detail,
            this.Kode,
            this.Murni,
            this.Geser,
            this.RKAP,
            this.ABT,
            this.Level,
            this.idsub,
            this.kodeuk,
            this.SetUK});
            this.treeGridProgram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeGridProgram.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.treeGridProgram.ImageList = null;
            this.treeGridProgram.Location = new System.Drawing.Point(0, 0);
            this.treeGridProgram.Name = "treeGridProgram";
            this.treeGridProgram.Size = new System.Drawing.Size(977, 438);
            this.treeGridProgram.TabIndex = 2;
            this.treeGridProgram.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.treeGridProgram_CellContentClick);
            // 
            // Detail
            // 
            this.Detail.HeaderText = "Detail";
            this.Detail.Name = "Detail";
            this.Detail.ReadOnly = true;
            this.Detail.Width = 50;
            // 
            // Kode
            // 
            this.Kode.DefaultNodeImage = null;
            this.Kode.HeaderText = "Kode dan Nama (Program/Kegiatan/Sub Kaegiatan";
            this.Kode.Name = "Kode";
            this.Kode.ReadOnly = true;
            this.Kode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Kode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Kode.Width = 800;
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
            this.Geser.HeaderText = "Pergeseran";
            this.Geser.Name = "Geser";
            this.Geser.ReadOnly = true;
            this.Geser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Geser.Width = 150;
            // 
            // RKAP
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.RKAP.DefaultCellStyle = dataGridViewCellStyle3;
            this.RKAP.HeaderText = "Pergeseran";
            this.RKAP.Name = "RKAP";
            this.RKAP.ReadOnly = true;
            this.RKAP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RKAP.Width = 150;
            // 
            // ABT
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ABT.DefaultCellStyle = dataGridViewCellStyle4;
            this.ABT.HeaderText = "Pergeseran Perubahan";
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
            this.Level.Visible = false;
            this.Level.Width = 20;
            // 
            // idsub
            // 
            this.idsub.HeaderText = "idsub";
            this.idsub.Name = "idsub";
            this.idsub.ReadOnly = true;
            this.idsub.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.idsub.Visible = false;
            this.idsub.Width = 80;
            // 
            // kodeuk
            // 
            this.kodeuk.HeaderText = "KodeUk";
            this.kodeuk.Name = "kodeuk";
            this.kodeuk.ReadOnly = true;
            this.kodeuk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.kodeuk.Visible = false;
            this.kodeuk.Width = 50;
            // 
            // SetUK
            // 
            this.SetUK.HeaderText = "SetUK";
            this.SetUK.Name = "SetUK";
            this.SetUK.ReadOnly = true;
            this.SetUK.Width = 80;
            // 
            // treeAnggaran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeGridProgram);
            this.Name = "treeAnggaran";
            this.Size = new System.Drawing.Size(977, 438);
            this.Load += new System.EventHandler(this.treeAnggaran_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeGridProgram)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TreeGridView treeGridProgram;
        private System.Windows.Forms.DataGridViewButtonColumn Detail;
        private TreeGridColumn Kode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Murni;
        private System.Windows.Forms.DataGridViewTextBoxColumn Geser;
        private System.Windows.Forms.DataGridViewTextBoxColumn RKAP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ABT;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level;
        private System.Windows.Forms.DataGridViewTextBoxColumn idsub;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodeuk;
        private System.Windows.Forms.DataGridViewButtonColumn SetUK;
    }
}
