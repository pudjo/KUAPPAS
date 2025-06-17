namespace KUAPPAS.Bendahara
{
    partial class frmListPengembalianBelanja
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ctrlPanelPencarian1 = new KUAPPAS.Bendahara.ctrlPanelPencarian();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.gridPengembalian = new System.Windows.Forms.DataGridView();
            this.IDCP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailCP = new System.Windows.Forms.DataGridViewButtonColumn();
            this.PilihCP = new System.Windows.Forms.DataGridViewButtonColumn();
            this.NoBuktiCP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TanggalCP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KeteranganCP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JenisSPP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoSP2DCP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JumlahCP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdd3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdCariLagi = new System.Windows.Forms.Button();
            this.cmdCari = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.groupPencarian = new System.Windows.Forms.GroupBox();
            this.cmdBKU = new System.Windows.Forms.Button();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridPengembalian)).BeginInit();
            this.groupPencarian.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlPanelPencarian1
            // 
            this.ctrlPanelPencarian1.Location = new System.Drawing.Point(2, 73);
            this.ctrlPanelPencarian1.Name = "ctrlPanelPencarian1";
            this.ctrlPanelPencarian1.Size = new System.Drawing.Size(586, 124);
            this.ctrlPanelPencarian1.TabIndex = 0;
            this.ctrlPanelPencarian1.TanggalAkhir = new System.DateTime(2025, 6, 17, 13, 57, 13, 173);
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
            this.ctrlHeader1.Size = new System.Drawing.Size(1080, 75);
            this.ctrlHeader1.TabIndex = 1;
            // 
            // gridPengembalian
            // 
            this.gridPengembalian.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPengembalian.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridPengembalian.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPengembalian.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridPengembalian.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPengembalian.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDCP,
            this.DetailCP,
            this.PilihCP,
            this.NoBuktiCP,
            this.TanggalCP,
            this.KeteranganCP,
            this.JenisSPP,
            this.NoSP2DCP,
            this.JumlahCP,
            this.colAdd3});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridPengembalian.DefaultCellStyle = dataGridViewCellStyle6;
            this.gridPengembalian.Location = new System.Drawing.Point(2, 233);
            this.gridPengembalian.Name = "gridPengembalian";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPengembalian.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gridPengembalian.Size = new System.Drawing.Size(1081, 135);
            this.gridPengembalian.TabIndex = 2;
            this.gridPengembalian.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPengembalian_CellContentClick);
            // 
            // IDCP
            // 
            this.IDCP.HeaderText = "ID";
            this.IDCP.Name = "IDCP";
            this.IDCP.Visible = false;
            // 
            // DetailCP
            // 
            this.DetailCP.HeaderText = "Detail";
            this.DetailCP.Name = "DetailCP";
            this.DetailCP.Width = 50;
            // 
            // PilihCP
            // 
            this.PilihCP.HeaderText = "BKU";
            this.PilihCP.Name = "PilihCP";
            this.PilihCP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // NoBuktiCP
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NoBuktiCP.DefaultCellStyle = dataGridViewCellStyle2;
            this.NoBuktiCP.HeaderText = "No Bukti";
            this.NoBuktiCP.Name = "NoBuktiCP";
            this.NoBuktiCP.ReadOnly = true;
            this.NoBuktiCP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NoBuktiCP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NoBuktiCP.Width = 200;
            // 
            // TanggalCP
            // 
            this.TanggalCP.HeaderText = "Tanggal";
            this.TanggalCP.Name = "TanggalCP";
            this.TanggalCP.ReadOnly = true;
            this.TanggalCP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TanggalCP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // KeteranganCP
            // 
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.KeteranganCP.DefaultCellStyle = dataGridViewCellStyle3;
            this.KeteranganCP.HeaderText = "Keterangan";
            this.KeteranganCP.Name = "KeteranganCP";
            this.KeteranganCP.ReadOnly = true;
            this.KeteranganCP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.KeteranganCP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KeteranganCP.Width = 300;
            // 
            // JenisSPP
            // 
            this.JenisSPP.HeaderText = "Jenis";
            this.JenisSPP.Name = "JenisSPP";
            this.JenisSPP.ReadOnly = true;
            this.JenisSPP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.JenisSPP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // NoSP2DCP
            // 
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NoSP2DCP.DefaultCellStyle = dataGridViewCellStyle4;
            this.NoSP2DCP.HeaderText = "No SP2D";
            this.NoSP2DCP.Name = "NoSP2DCP";
            this.NoSP2DCP.ReadOnly = true;
            this.NoSP2DCP.Width = 150;
            // 
            // JumlahCP
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.JumlahCP.DefaultCellStyle = dataGridViewCellStyle5;
            this.JumlahCP.HeaderText = "Jumlah";
            this.JumlahCP.Name = "JumlahCP";
            this.JumlahCP.ReadOnly = true;
            // 
            // colAdd3
            // 
            this.colAdd3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colAdd3.HeaderText = "";
            this.colAdd3.Name = "colAdd3";
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
            this.groupPencarian.Location = new System.Drawing.Point(143, 187);
            this.groupPencarian.Name = "groupPencarian";
            this.groupPencarian.Size = new System.Drawing.Size(463, 46);
            this.groupPencarian.TabIndex = 33;
            this.groupPencarian.TabStop = false;
            this.groupPencarian.Text = "Pencarian";
            // 
            // cmdBKU
            // 
            this.cmdBKU.Location = new System.Drawing.Point(18, 200);
            this.cmdBKU.Name = "cmdBKU";
            this.cmdBKU.Size = new System.Drawing.Size(101, 23);
            this.cmdBKU.TabIndex = 34;
            this.cmdBKU.Text = "BKU kan semua";
            this.cmdBKU.UseVisualStyleBackColor = true;
            // 
            // txtJumlah
            // 
            this.txtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlah.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlah.Location = new System.Drawing.Point(899, 203);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(181, 22);
            this.txtJumlah.TabIndex = 35;
            this.txtJumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // frmListPengembalianBelanja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 361);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.cmdBKU);
            this.Controls.Add(this.groupPencarian);
            this.Controls.Add(this.gridPengembalian);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.ctrlPanelPencarian1);
            this.Name = "frmListPengembalianBelanja";
            this.Text = "Daftar Pengembalian Belanja";
            this.Load += new System.EventHandler(this.frmListPengembalianBelanja_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridPengembalian)).EndInit();
            this.groupPencarian.ResumeLayout(false);
            this.groupPencarian.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlPanelPencarian ctrlPanelPencarian1;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.DataGridView gridPengembalian;
        private System.Windows.Forms.Button cmdCariLagi;
        private System.Windows.Forms.Button cmdCari;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.GroupBox groupPencarian;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDCP;
        private System.Windows.Forms.DataGridViewButtonColumn DetailCP;
        private System.Windows.Forms.DataGridViewButtonColumn PilihCP;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoBuktiCP;
        private System.Windows.Forms.DataGridViewTextBoxColumn TanggalCP;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeteranganCP;
        private System.Windows.Forms.DataGridViewTextBoxColumn JenisSPP;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoSP2DCP;
        private System.Windows.Forms.DataGridViewTextBoxColumn JumlahCP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdd3;
        private System.Windows.Forms.Button cmdBKU;
        private System.Windows.Forms.TextBox txtJumlah;
    }
}