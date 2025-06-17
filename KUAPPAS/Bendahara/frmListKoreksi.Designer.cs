namespace KUAPPAS.Bendahara
{
    partial class frmListKoreksi
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridkoreksi = new System.Windows.Forms.DataGridView();
            this.NoUrut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detail = new System.Windows.Forms.DataGridViewButtonColumn();
            this.NoBukti = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tanggal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Uraian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlPanelPencarian1 = new KUAPPAS.Bendahara.ctrlPanelPencarian();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.groupPencarian = new System.Windows.Forms.GroupBox();
            this.cmdCariLagi = new System.Windows.Forms.Button();
            this.cmdCari = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridkoreksi)).BeginInit();
            this.groupPencarian.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridkoreksi
            // 
            this.gridkoreksi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridkoreksi.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridkoreksi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridkoreksi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NoUrut,
            this.Detail,
            this.NoBukti,
            this.Tanggal,
            this.Uraian,
            this.Status});
            this.gridkoreksi.Location = new System.Drawing.Point(0, 200);
            this.gridkoreksi.Name = "gridkoreksi";
            this.gridkoreksi.Size = new System.Drawing.Size(943, 268);
            this.gridkoreksi.TabIndex = 3;
            this.gridkoreksi.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridkoreksi_CellContentClick);
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
            this.Detail.Width = 75;
            // 
            // NoBukti
            // 
            this.NoBukti.HeaderText = "Nomor";
            this.NoBukti.Name = "NoBukti";
            this.NoBukti.ReadOnly = true;
            this.NoBukti.Width = 150;
            // 
            // Tanggal
            // 
            this.Tanggal.HeaderText = "Tanggal Koreksi";
            this.Tanggal.Name = "Tanggal";
            this.Tanggal.ReadOnly = true;
            // 
            // Uraian
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Uraian.DefaultCellStyle = dataGridViewCellStyle2;
            this.Uraian.HeaderText = "Uraian";
            this.Uraian.Name = "Uraian";
            this.Uraian.ReadOnly = true;
            this.Uraian.Width = 750;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // ctrlPanelPencarian1
            // 
            this.ctrlPanelPencarian1.Location = new System.Drawing.Point(7, 78);
            this.ctrlPanelPencarian1.Name = "ctrlPanelPencarian1";
            this.ctrlPanelPencarian1.Size = new System.Drawing.Size(611, 129);
            this.ctrlPanelPencarian1.TabIndex = 2;
            this.ctrlPanelPencarian1.TanggalAkhir = new System.DateTime(2025, 6, 17, 13, 55, 8, 800);
            this.ctrlPanelPencarian1.TanggalAwal = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
            this.ctrlPanelPencarian1.OnDisplay += new KUAPPAS.Bendahara.ctrlPanelPencarian.ValueChangedEventHandler(this.ctrlPanelPencarian1_OnDisplay);
            this.ctrlPanelPencarian1.OnAdd += new KUAPPAS.Bendahara.ctrlPanelPencarian.ValueChangedEventHandler(this.ctrlPanelPencarian1_OnAdd);
            this.ctrlPanelPencarian1.Load += new System.EventHandler(this.ctrlPanelPencarian1_Load);
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(943, 77);
            this.ctrlHeader1.TabIndex = 1;
            // 
            // txtCari
            // 
            this.txtCari.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCari.Location = new System.Drawing.Point(49, 19);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(239, 20);
            this.txtCari.TabIndex = 30;
            this.txtCari.Visible = false;
            // 
            // groupPencarian
            // 
            this.groupPencarian.Controls.Add(this.txtCari);
            this.groupPencarian.Controls.Add(this.cmdCariLagi);
            this.groupPencarian.Controls.Add(this.cmdCari);
            this.groupPencarian.Location = new System.Drawing.Point(368, 144);
            this.groupPencarian.Name = "groupPencarian";
            this.groupPencarian.Size = new System.Drawing.Size(463, 46);
            this.groupPencarian.TabIndex = 34;
            this.groupPencarian.TabStop = false;
            this.groupPencarian.Text = "Pencarian";
            this.groupPencarian.Visible = false;
            // 
            // cmdCariLagi
            // 
            this.cmdCariLagi.Location = new System.Drawing.Point(372, 16);
            this.cmdCariLagi.Name = "cmdCariLagi";
            this.cmdCariLagi.Size = new System.Drawing.Size(75, 23);
            this.cmdCariLagi.TabIndex = 32;
            this.cmdCariLagi.Text = "Cari Lagi";
            this.cmdCariLagi.UseVisualStyleBackColor = true;
            this.cmdCariLagi.Visible = false;
            this.cmdCariLagi.Click += new System.EventHandler(this.cmdCariLagi_Click);
            // 
            // cmdCari
            // 
            this.cmdCari.Location = new System.Drawing.Point(304, 16);
            this.cmdCari.Name = "cmdCari";
            this.cmdCari.Size = new System.Drawing.Size(62, 23);
            this.cmdCari.TabIndex = 31;
            this.cmdCari.Text = "Cari...";
            this.cmdCari.UseVisualStyleBackColor = true;
            this.cmdCari.Visible = false;
            this.cmdCari.Click += new System.EventHandler(this.cmdCari_Click);
            // 
            // frmListKoreksi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 474);
            this.Controls.Add(this.groupPencarian);
            this.Controls.Add(this.gridkoreksi);
            this.Controls.Add(this.ctrlPanelPencarian1);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmListKoreksi";
            this.Text = "Daftar Koreksi Belanja";
            this.Load += new System.EventHandler(this.frmListKoreksi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridkoreksi)).EndInit();
            this.groupPencarian.ResumeLayout(false);
            this.groupPencarian.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlHeader ctrlHeader1;
        private ctrlPanelPencarian ctrlPanelPencarian1;
        private System.Windows.Forms.DataGridView gridkoreksi;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.GroupBox groupPencarian;
        private System.Windows.Forms.Button cmdCariLagi;
        private System.Windows.Forms.Button cmdCari;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoUrut;
        private System.Windows.Forms.DataGridViewButtonColumn Detail;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoBukti;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tanggal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Uraian;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
    }
}