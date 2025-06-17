namespace KUAPPAS.Bendahara
{
    partial class frmDashBoardBendahara
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridSP2D = new System.Windows.Forms.DataGridView();
            this.NoUrut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoSP2D = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomorSP2D = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TanggalSP2D = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jenis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jumlah = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BKUKan = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.cmdPanggil = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridSP2D)).BeginInit();
            this.SuspendLayout();
            // 
            // gridSP2D
            // 
            this.gridSP2D.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSP2D.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NoUrut,
            this.NoSP2D,
            this.NomorSP2D,
            this.TanggalSP2D,
            this.Jenis,
            this.Jumlah,
            this.BKUKan});
            this.gridSP2D.Location = new System.Drawing.Point(142, 110);
            this.gridSP2D.Name = "gridSP2D";
            this.gridSP2D.Size = new System.Drawing.Size(790, 155);
            this.gridSP2D.TabIndex = 1;
            // 
            // NoUrut
            // 
            this.NoUrut.HeaderText = "No Urut";
            this.NoUrut.Name = "NoUrut";
            this.NoUrut.ReadOnly = true;
            this.NoUrut.Visible = false;
            // 
            // NoSP2D
            // 
            this.NoSP2D.HeaderText = "No";
            this.NoSP2D.Name = "NoSP2D";
            this.NoSP2D.ReadOnly = true;
            this.NoSP2D.Width = 50;
            // 
            // NomorSP2D
            // 
            this.NomorSP2D.HeaderText = "No SP2D";
            this.NomorSP2D.Name = "NomorSP2D";
            this.NomorSP2D.ReadOnly = true;
            this.NomorSP2D.Width = 200;
            // 
            // TanggalSP2D
            // 
            this.TanggalSP2D.HeaderText = "Tanggal SP2D";
            this.TanggalSP2D.Name = "TanggalSP2D";
            this.TanggalSP2D.ReadOnly = true;
            this.TanggalSP2D.Width = 120;
            // 
            // Jenis
            // 
            this.Jenis.HeaderText = "Jenis";
            this.Jenis.Name = "Jenis";
            this.Jenis.ReadOnly = true;
            this.Jenis.Width = 80;
            // 
            // Jumlah
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomRight;
            this.Jumlah.DefaultCellStyle = dataGridViewCellStyle1;
            this.Jumlah.HeaderText = "Jumlah";
            this.Jumlah.Name = "Jumlah";
            this.Jumlah.ReadOnly = true;
            this.Jumlah.Width = 150;
            // 
            // BKUKan
            // 
            this.BKUKan.HeaderText = "BKU Kan";
            this.BKUKan.Name = "BKUKan";
            this.BKUKan.ReadOnly = true;
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(142, 22);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(678, 43);
            this.ctrlDinas1.TabIndex = 0;
            // 
            // cmdPanggil
            // 
            this.cmdPanggil.Location = new System.Drawing.Point(243, 76);
            this.cmdPanggil.Name = "cmdPanggil";
            this.cmdPanggil.Size = new System.Drawing.Size(75, 23);
            this.cmdPanggil.TabIndex = 2;
            this.cmdPanggil.Text = "Panggil";
            this.cmdPanggil.UseVisualStyleBackColor = true;
            this.cmdPanggil.Click += new System.EventHandler(this.cmdPanggil_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(142, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "SP2D Belum BKU ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(88, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "OPD";
            // 
            // frmDashBoardBendahara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 446);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdPanggil);
            this.Controls.Add(this.gridSP2D);
            this.Controls.Add(this.ctrlDinas1);
            this.Name = "frmDashBoardBendahara";
            this.Text = "frmDashBoardBendahara";
            this.Load += new System.EventHandler(this.frmDashBoardBendahara_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSP2D)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlDinas ctrlDinas1;
        private System.Windows.Forms.DataGridView gridSP2D;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoUrut;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoSP2D;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomorSP2D;
        private System.Windows.Forms.DataGridViewTextBoxColumn TanggalSP2D;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jenis;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jumlah;
        private System.Windows.Forms.DataGridViewButtonColumn BKUKan;
        private System.Windows.Forms.Button cmdPanggil;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}