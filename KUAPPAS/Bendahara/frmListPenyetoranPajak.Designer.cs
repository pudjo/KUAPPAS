namespace KUAPPAS.Bendahara
{
    partial class frmListPenyetoranPajak
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ctrlPanelPencarian1 = new KUAPPAS.Bendahara.ctrlPanelPencarian();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.gridSetorPajak = new System.Windows.Forms.DataGridView();
            this.noUrutSetorPajak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoUrutClient = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ubah = new System.Windows.Forms.DataGridViewButtonColumn();
            this.BKU = new System.Windows.Forms.DataGridViewButtonColumn();
            this.TanggalSetorPajak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoBuktisetorPajak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Keterangansetorpajak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NONTPN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KodeBilling = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JumlahSetorPajak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdd5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupPencarian = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.cmdCariLagi = new System.Windows.Forms.Button();
            this.cmdCari = new System.Windows.Forms.Button();
            this.cmdLanjut = new System.Windows.Forms.Button();
            this.cmdBKU = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridSetorPajak)).BeginInit();
            this.groupPencarian.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlPanelPencarian1
            // 
            this.ctrlPanelPencarian1.Location = new System.Drawing.Point(1, 69);
            this.ctrlPanelPencarian1.Name = "ctrlPanelPencarian1";
            this.ctrlPanelPencarian1.Size = new System.Drawing.Size(586, 127);
            this.ctrlPanelPencarian1.TabIndex = 1;
            this.ctrlPanelPencarian1.TanggalAkhir = new System.DateTime(2025, 6, 17, 14, 0, 41, 181);
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
            this.ctrlHeader1.Size = new System.Drawing.Size(893, 69);
            this.ctrlHeader1.TabIndex = 2;
            // 
            // gridSetorPajak
            // 
            this.gridSetorPajak.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSetorPajak.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridSetorPajak.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridSetorPajak.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridSetorPajak.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSetorPajak.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.noUrutSetorPajak,
            this.NoUrutClient,
            this.Ubah,
            this.BKU,
            this.TanggalSetorPajak,
            this.NoBuktisetorPajak,
            this.Keterangansetorpajak,
            this.NONTPN,
            this.KodeBilling,
            this.JumlahSetorPajak,
            this.colAdd5});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridSetorPajak.DefaultCellStyle = dataGridViewCellStyle4;
            this.gridSetorPajak.Location = new System.Drawing.Point(1, 215);
            this.gridSetorPajak.Name = "gridSetorPajak";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridSetorPajak.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gridSetorPajak.Size = new System.Drawing.Size(892, 142);
            this.gridSetorPajak.TabIndex = 3;
            this.gridSetorPajak.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSetorPajak_CellContentClick);
            // 
            // noUrutSetorPajak
            // 
            this.noUrutSetorPajak.HeaderText = "NoUrut";
            this.noUrutSetorPajak.Name = "noUrutSetorPajak";
            this.noUrutSetorPajak.ReadOnly = true;
            this.noUrutSetorPajak.Visible = false;
            // 
            // NoUrutClient
            // 
            this.NoUrutClient.HeaderText = "nourutpanjar";
            this.NoUrutClient.Name = "NoUrutClient";
            this.NoUrutClient.ReadOnly = true;
            this.NoUrutClient.Visible = false;
            // 
            // Ubah
            // 
            this.Ubah.HeaderText = "Ubah";
            this.Ubah.Name = "Ubah";
            this.Ubah.ReadOnly = true;
            // 
            // BKU
            // 
            this.BKU.HeaderText = "BKU Kan";
            this.BKU.Name = "BKU";
            this.BKU.Width = 50;
            // 
            // TanggalSetorPajak
            // 
            this.TanggalSetorPajak.HeaderText = "Tanggal";
            this.TanggalSetorPajak.Name = "TanggalSetorPajak";
            this.TanggalSetorPajak.ReadOnly = true;
            this.TanggalSetorPajak.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TanggalSetorPajak.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TanggalSetorPajak.Width = 89;
            // 
            // NoBuktisetorPajak
            // 
            this.NoBuktisetorPajak.HeaderText = "No Bukti";
            this.NoBuktisetorPajak.Name = "NoBuktisetorPajak";
            this.NoBuktisetorPajak.ReadOnly = true;
            this.NoBuktisetorPajak.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NoBuktisetorPajak.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Keterangansetorpajak
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Keterangansetorpajak.DefaultCellStyle = dataGridViewCellStyle2;
            this.Keterangansetorpajak.HeaderText = "Keterangan";
            this.Keterangansetorpajak.Name = "Keterangansetorpajak";
            this.Keterangansetorpajak.ReadOnly = true;
            this.Keterangansetorpajak.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Keterangansetorpajak.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Keterangansetorpajak.Width = 600;
            // 
            // NONTPN
            // 
            this.NONTPN.HeaderText = "No NTPN";
            this.NONTPN.Name = "NONTPN";
            this.NONTPN.ReadOnly = true;
            this.NONTPN.Width = 120;
            // 
            // KodeBilling
            // 
            this.KodeBilling.HeaderText = "Kode Billing";
            this.KodeBilling.Name = "KodeBilling";
            this.KodeBilling.ReadOnly = true;
            this.KodeBilling.Width = 120;
            // 
            // JumlahSetorPajak
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.JumlahSetorPajak.DefaultCellStyle = dataGridViewCellStyle3;
            this.JumlahSetorPajak.HeaderText = "Jumlah";
            this.JumlahSetorPajak.Name = "JumlahSetorPajak";
            this.JumlahSetorPajak.ReadOnly = true;
            this.JumlahSetorPajak.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.JumlahSetorPajak.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colAdd5
            // 
            this.colAdd5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colAdd5.HeaderText = "";
            this.colAdd5.Name = "colAdd5";
            // 
            // groupPencarian
            // 
            this.groupPencarian.Controls.Add(this.button1);
            this.groupPencarian.Controls.Add(this.txtCari);
            this.groupPencarian.Controls.Add(this.cmdCariLagi);
            this.groupPencarian.Controls.Add(this.cmdCari);
            this.groupPencarian.Location = new System.Drawing.Point(430, 147);
            this.groupPencarian.Name = "groupPencarian";
            this.groupPencarian.Size = new System.Drawing.Size(463, 46);
            this.groupPencarian.TabIndex = 34;
            this.groupPencarian.TabStop = false;
            this.groupPencarian.Text = "Pencarian";
            this.groupPencarian.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(462, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 33;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
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
            // cmdLanjut
            // 
            this.cmdLanjut.Location = new System.Drawing.Point(806, 118);
            this.cmdLanjut.Name = "cmdLanjut";
            this.cmdLanjut.Size = new System.Drawing.Size(75, 23);
            this.cmdLanjut.TabIndex = 35;
            this.cmdLanjut.Text = "Lebih Detail";
            this.cmdLanjut.UseVisualStyleBackColor = true;
            this.cmdLanjut.Click += new System.EventHandler(this.cmdLanjut_Click);
            // 
            // cmdBKU
            // 
            this.cmdBKU.Location = new System.Drawing.Point(10, 185);
            this.cmdBKU.Name = "cmdBKU";
            this.cmdBKU.Size = new System.Drawing.Size(102, 23);
            this.cmdBKU.TabIndex = 36;
            this.cmdBKU.Text = "BKU Kan Semua";
            this.cmdBKU.UseVisualStyleBackColor = true;
            this.cmdBKU.Click += new System.EventHandler(this.cmdBKU_Click);
            // 
            // frmListPenyetoranPajak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 360);
            this.Controls.Add(this.cmdBKU);
            this.Controls.Add(this.cmdLanjut);
            this.Controls.Add(this.groupPencarian);
            this.Controls.Add(this.gridSetorPajak);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.ctrlPanelPencarian1);
            this.Name = "frmListPenyetoranPajak";
            this.Text = "Daftar Penyetoran Pajak ";
            this.Load += new System.EventHandler(this.frmListPenyetoranPajak_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSetorPajak)).EndInit();
            this.groupPencarian.ResumeLayout(false);
            this.groupPencarian.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlPanelPencarian ctrlPanelPencarian1;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.DataGridView gridSetorPajak;
        private System.Windows.Forms.GroupBox groupPencarian;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Button cmdCariLagi;
        private System.Windows.Forms.Button cmdCari;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button cmdLanjut;
        private System.Windows.Forms.DataGridViewTextBoxColumn noUrutSetorPajak;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoUrutClient;
        private System.Windows.Forms.DataGridViewButtonColumn Ubah;
        private System.Windows.Forms.DataGridViewButtonColumn BKU;
        private System.Windows.Forms.DataGridViewTextBoxColumn TanggalSetorPajak;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoBuktisetorPajak;
        private System.Windows.Forms.DataGridViewTextBoxColumn Keterangansetorpajak;
        private System.Windows.Forms.DataGridViewTextBoxColumn NONTPN;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodeBilling;
        private System.Windows.Forms.DataGridViewTextBoxColumn JumlahSetorPajak;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdd5;
        private System.Windows.Forms.Button cmdBKU;
    }
}