namespace KUAPPAS.Bendahara
{
    partial class frmListLPJUP
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
            this.ctrlPanelPencarian1 = new KUAPPAS.Bendahara.ctrlPanelPencarian();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.NoUrut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detail = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Nomor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tanggal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Keterangan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlPanelPencarian1
            // 
            this.ctrlPanelPencarian1.Location = new System.Drawing.Point(24, 12);
            this.ctrlPanelPencarian1.Name = "ctrlPanelPencarian1";
            this.ctrlPanelPencarian1.Size = new System.Drawing.Size(586, 150);
            this.ctrlPanelPencarian1.TabIndex = 0;
            this.ctrlPanelPencarian1.OnDisplay += new KUAPPAS.Bendahara.ctrlPanelPencarian.ValueChangedEventHandler(this.ctrlPanelPencarian1_OnDisplay);
            this.ctrlPanelPencarian1.Load += new System.EventHandler(this.ctrlPanelPencarian1_Load);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NoUrut,
            this.Detail,
            this.Nomor,
            this.Tanggal,
            this.Keterangan});
            this.dataGridView1.Location = new System.Drawing.Point(12, 176);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(833, 259);
            this.dataGridView1.TabIndex = 1;
            // 
            // NoUrut
            // 
            this.NoUrut.HeaderText = "NoUrut";
            this.NoUrut.Name = "NoUrut";
            this.NoUrut.ReadOnly = true;
            this.NoUrut.Visible = false;
            // 
            // Detail
            // 
            this.Detail.HeaderText = "Detail";
            this.Detail.Name = "Detail";
            this.Detail.ReadOnly = true;
            this.Detail.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Detail.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Nomor
            // 
            this.Nomor.HeaderText = "Nomor";
            this.Nomor.Name = "Nomor";
            this.Nomor.ReadOnly = true;
            // 
            // Tanggal
            // 
            this.Tanggal.HeaderText = "Tanggal";
            this.Tanggal.Name = "Tanggal";
            this.Tanggal.ReadOnly = true;
            // 
            // Keterangan
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Keterangan.DefaultCellStyle = dataGridViewCellStyle1;
            this.Keterangan.HeaderText = "Keterangan";
            this.Keterangan.Name = "Keterangan";
            this.Keterangan.ReadOnly = true;
            this.Keterangan.Width = 450;
            // 
            // frmListLPJUP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 466);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ctrlPanelPencarian1);
            this.Name = "frmListLPJUP";
            this.Text = "Daftar LPJ UP ";
            this.Load += new System.EventHandler(this.frmListLPJUP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlPanelPencarian ctrlPanelPencarian1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoUrut;
        private System.Windows.Forms.DataGridViewButtonColumn Detail;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nomor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tanggal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Keterangan;
    }
}