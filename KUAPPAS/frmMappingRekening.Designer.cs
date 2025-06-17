namespace KUAPPAS
{
    partial class frmMappingRekening
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridMapping = new System.Windows.Forms.DataGridView();
            this.ID13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kodelra64 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama64 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Default64 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Cari64 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.KodeLO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaLO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CariLO = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            ((System.ComponentModel.ISupportInitialize)(this.gridMapping)).BeginInit();
            this.SuspendLayout();
            // 
            // gridMapping
            // 
            this.gridMapping.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridMapping.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridMapping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMapping.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID13,
            this.Nama13,
            this.Kodelra64,
            this.Nama64,
            this.Default64,
            this.Cari64,
            this.KodeLO,
            this.NamaLO,
            this.CariLO});
            this.gridMapping.Location = new System.Drawing.Point(0, 75);
            this.gridMapping.Name = "gridMapping";
            this.gridMapping.Size = new System.Drawing.Size(988, 434);
            this.gridMapping.TabIndex = 0;
            // 
            // ID13
            // 
            this.ID13.HeaderText = "Kode 13";
            this.ID13.Name = "ID13";
            this.ID13.ReadOnly = true;
            this.ID13.Width = 80;
            // 
            // Nama13
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Nama13.DefaultCellStyle = dataGridViewCellStyle1;
            this.Nama13.HeaderText = "Nama Rekening Permen 13";
            this.Nama13.Name = "Nama13";
            this.Nama13.ReadOnly = true;
            this.Nama13.Width = 150;
            // 
            // Kodelra64
            // 
            this.Kodelra64.HeaderText = "Kode Rek 64";
            this.Kodelra64.Name = "Kodelra64";
            this.Kodelra64.ReadOnly = true;
            this.Kodelra64.Width = 80;
            // 
            // Nama64
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Nama64.DefaultCellStyle = dataGridViewCellStyle2;
            this.Nama64.HeaderText = "Nama Rek LRA 64";
            this.Nama64.Name = "Nama64";
            this.Nama64.ReadOnly = true;
            this.Nama64.Width = 200;
            // 
            // Default64
            // 
            this.Default64.HeaderText = "Default?";
            this.Default64.Name = "Default64";
            this.Default64.ReadOnly = true;
            this.Default64.Width = 50;
            // 
            // Cari64
            // 
            this.Cari64.HeaderText = "Cari 6";
            this.Cari64.Name = "Cari64";
            this.Cari64.ReadOnly = true;
            this.Cari64.Width = 50;
            // 
            // KodeLO
            // 
            this.KodeLO.HeaderText = "Kode Rek LO";
            this.KodeLO.Name = "KodeLO";
            this.KodeLO.ReadOnly = true;
            this.KodeLO.Width = 80;
            // 
            // NamaLO
            // 
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NamaLO.DefaultCellStyle = dataGridViewCellStyle3;
            this.NamaLO.HeaderText = "Nama Rek LO";
            this.NamaLO.Name = "NamaLO";
            this.NamaLO.ReadOnly = true;
            this.NamaLO.Width = 200;
            // 
            // CariLO
            // 
            this.CariLO.HeaderText = "Cari Kode LO";
            this.CariLO.Name = "CariLO";
            this.CariLO.ReadOnly = true;
            this.CariLO.Width = 50;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Kode 13";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 80;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn2.HeaderText = "Nama Rekening Permen 13";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Kode Rek 64";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 80;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn4.HeaderText = "Nama Rek LRA 64";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 200;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Kode Rek LO";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 80;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn6.HeaderText = "Nama Rek LO";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 200;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(986, 51);
            this.ctrlHeader1.TabIndex = 1;
            // 
            // frmMappingRekening
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 585);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.gridMapping);
            this.Name = "frmMappingRekening";
            this.Text = "frmMappingRekening";
            this.Load += new System.EventHandler(this.frmMappingRekening_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridMapping)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridMapping;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kodelra64;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama64;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Default64;
        private System.Windows.Forms.DataGridViewButtonColumn Cari64;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodeLO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaLO;
        private System.Windows.Forms.DataGridViewButtonColumn CariLO;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private ctrlHeader ctrlHeader1;
    }
}