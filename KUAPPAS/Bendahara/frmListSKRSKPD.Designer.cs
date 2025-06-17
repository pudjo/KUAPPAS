namespace KUAPPAS.Bendahara
{
    partial class frmListSKRSKPD
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
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridSKRSKPD = new System.Windows.Forms.DataGridView();
            this.IDSKRSKPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailSKRSKPD = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Pilih = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NoSKRSKPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TanggalSKRSKPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KeteranganSKRSKPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InstitusiSKRSKPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JumlahSKRSKPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.ctrlPanelPencarian1 = new KUAPPAS.Bendahara.ctrlPanelPencarian();
            ((System.ComponentModel.ISupportInitialize)(this.gridSKRSKPD)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Nomor";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Tanggal";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn4.HeaderText = "Keterangan";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 400;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn5.HeaderText = "Institusi/Alamat";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 300;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn6.HeaderText = "Jumlah";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 150;
            // 
            // gridSKRSKPD
            // 
            this.gridSKRSKPD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSKRSKPD.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridSKRSKPD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridSKRSKPD.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridSKRSKPD.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gridSKRSKPD.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDSKRSKPD,
            this.DetailSKRSKPD,
            this.Pilih,
            this.NoSKRSKPD,
            this.TanggalSKRSKPD,
            this.KeteranganSKRSKPD,
            this.InstitusiSKRSKPD,
            this.JumlahSKRSKPD});
            this.gridSKRSKPD.Location = new System.Drawing.Point(-2, 192);
            this.gridSKRSKPD.Name = "gridSKRSKPD";
            this.gridSKRSKPD.Size = new System.Drawing.Size(1004, 241);
            this.gridSKRSKPD.TabIndex = 15;
            // 
            // IDSKRSKPD
            // 
            this.IDSKRSKPD.HeaderText = "ID";
            this.IDSKRSKPD.Name = "IDSKRSKPD";
            this.IDSKRSKPD.Visible = false;
            // 
            // DetailSKRSKPD
            // 
            this.DetailSKRSKPD.HeaderText = "Detail";
            this.DetailSKRSKPD.Name = "DetailSKRSKPD";
            this.DetailSKRSKPD.ReadOnly = true;
            this.DetailSKRSKPD.Width = 40;
            // 
            // Pilih
            // 
            this.Pilih.HeaderText = "Pilih";
            this.Pilih.Name = "Pilih";
            this.Pilih.Width = 40;
            // 
            // NoSKRSKPD
            // 
            this.NoSKRSKPD.HeaderText = "Nomor";
            this.NoSKRSKPD.Name = "NoSKRSKPD";
            this.NoSKRSKPD.ReadOnly = true;
            this.NoSKRSKPD.Width = 150;
            // 
            // TanggalSKRSKPD
            // 
            this.TanggalSKRSKPD.HeaderText = "Tanggal";
            this.TanggalSKRSKPD.Name = "TanggalSKRSKPD";
            this.TanggalSKRSKPD.ReadOnly = true;
            // 
            // KeteranganSKRSKPD
            // 
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.KeteranganSKRSKPD.DefaultCellStyle = dataGridViewCellStyle4;
            this.KeteranganSKRSKPD.HeaderText = "Keterangan";
            this.KeteranganSKRSKPD.Name = "KeteranganSKRSKPD";
            this.KeteranganSKRSKPD.ReadOnly = true;
            this.KeteranganSKRSKPD.Width = 400;
            // 
            // InstitusiSKRSKPD
            // 
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InstitusiSKRSKPD.DefaultCellStyle = dataGridViewCellStyle5;
            this.InstitusiSKRSKPD.HeaderText = "Institusi/Alamat";
            this.InstitusiSKRSKPD.Name = "InstitusiSKRSKPD";
            this.InstitusiSKRSKPD.ReadOnly = true;
            this.InstitusiSKRSKPD.Width = 300;
            // 
            // JumlahSKRSKPD
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.JumlahSKRSKPD.DefaultCellStyle = dataGridViewCellStyle6;
            this.JumlahSKRSKPD.HeaderText = "Jumlah";
            this.JumlahSKRSKPD.Name = "JumlahSKRSKPD";
            this.JumlahSKRSKPD.ReadOnly = true;
            this.JumlahSKRSKPD.Width = 150;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1001, 70);
            this.ctrlHeader1.TabIndex = 16;
            // 
            // ctrlPanelPencarian1
            // 
            this.ctrlPanelPencarian1.Location = new System.Drawing.Point(-2, 76);
            this.ctrlPanelPencarian1.Name = "ctrlPanelPencarian1";
            this.ctrlPanelPencarian1.Size = new System.Drawing.Size(586, 124);
            this.ctrlPanelPencarian1.TabIndex = 17;
            this.ctrlPanelPencarian1.TanggalAkhir = new System.DateTime(2025, 6, 17, 14, 3, 4, 83);
            this.ctrlPanelPencarian1.TanggalAwal = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
            this.ctrlPanelPencarian1.OnDisplay += new KUAPPAS.Bendahara.ctrlPanelPencarian.ValueChangedEventHandler(this.ctrlPanelPencarian1_OnDisplay);
            // 
            // frmListSKRSKPD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 433);
            this.Controls.Add(this.ctrlPanelPencarian1);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.gridSKRSKPD);
            this.Name = "frmListSKRSKPD";
            this.Text = "frmListSKRSKPD";
            this.Load += new System.EventHandler(this.frmListSKRSKPD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSKRSKPD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridView gridSKRSKPD;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDSKRSKPD;
        private System.Windows.Forms.DataGridViewButtonColumn DetailSKRSKPD;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Pilih;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoSKRSKPD;
        private System.Windows.Forms.DataGridViewTextBoxColumn TanggalSKRSKPD;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeteranganSKRSKPD;
        private System.Windows.Forms.DataGridViewTextBoxColumn InstitusiSKRSKPD;
        private System.Windows.Forms.DataGridViewTextBoxColumn JumlahSKRSKPD;
        private ctrlHeader ctrlHeader1;
        private ctrlPanelPencarian ctrlPanelPencarian1;
    }
}