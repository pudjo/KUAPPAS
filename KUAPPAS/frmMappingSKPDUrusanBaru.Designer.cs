namespace KUAPPAS
{
    partial class frmMappingSKPDUrusanBaru
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
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDUrusan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaDinas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UrusanBaru = new System.Windows.Forms.DataGridViewComboBoxColumn();
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
            this.ID,
            this.IDUrusan,
            this.NamaDinas,
            this.UrusanBaru});
            this.gridSKPD.Location = new System.Drawing.Point(2, 50);
            this.gridSKPD.Name = "gridSKPD";
            this.gridSKPD.Size = new System.Drawing.Size(805, 370);
            this.gridSKPD.TabIndex = 0;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // IDUrusan
            // 
            this.IDUrusan.HeaderText = "Urusan Lama";
            this.IDUrusan.Name = "IDUrusan";
            this.IDUrusan.ReadOnly = true;
            // 
            // NamaDinas
            // 
            this.NamaDinas.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NamaDinas.HeaderText = "Nama SKPD";
            this.NamaDinas.Name = "NamaDinas";
            this.NamaDinas.ReadOnly = true;
            // 
            // UrusanBaru
            // 
            this.UrusanBaru.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UrusanBaru.HeaderText = "Urusan Baru";
            this.UrusanBaru.Name = "UrusanBaru";
            this.UrusanBaru.ReadOnly = true;
            this.UrusanBaru.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UrusanBaru.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(807, 44);
            this.ctrlHeader1.TabIndex = 1;
            // 
            // frmMappingSKPDUrusanBaru
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 443);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.gridSKPD);
            this.Name = "frmMappingSKPDUrusanBaru";
            this.Text = "frmMappingSKPDUrusanBaru";
            this.Load += new System.EventHandler(this.frmMappingSKPDUrusanBaru_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSKPD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridSKPD;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDUrusan;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaDinas;
        private System.Windows.Forms.DataGridViewComboBoxColumn UrusanBaru;
        private ctrlHeader ctrlHeader1;
    }
}