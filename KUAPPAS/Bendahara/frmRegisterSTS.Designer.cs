namespace KUAPPAS.Bendahara
{
    partial class frmRegisterSTS
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
            this.cmdExcell = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSaldoAwal = new System.Windows.Forms.TextBox();
            this.gridSTS = new System.Windows.Forms.DataGridView();
            this.chkSetor = new System.Windows.Forms.CheckBox();
            this.chkLangsung = new System.Windows.Forms.CheckBox();
            this.chkTBP = new System.Windows.Forms.CheckBox();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrlTanggalBulanVertikal1 = new KUAPPAS.Bendahara.ctrlTanggalBulanVertikal();
            this.cmdCetak = new System.Windows.Forms.Button();
            this.cmdPanggilData = new System.Windows.Forms.Button();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.NoBukuKas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TanggalBK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoBuktiBK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KeteranganBK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PenerimaanBK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridSTS)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdExcell
            // 
            this.cmdExcell.Location = new System.Drawing.Point(665, 48);
            this.cmdExcell.Name = "cmdExcell";
            this.cmdExcell.Size = new System.Drawing.Size(75, 39);
            this.cmdExcell.TabIndex = 64;
            this.cmdExcell.Text = "Excell";
            this.cmdExcell.UseVisualStyleBackColor = true;
            this.cmdExcell.Click += new System.EventHandler(this.cmdExcell_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(615, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 63;
            this.label3.Text = "Jumlah";
            // 
            // txtJumlah
            // 
            this.txtJumlah.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlah.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlah.Location = new System.Drawing.Point(665, 95);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(160, 21);
            this.txtJumlah.TabIndex = 62;
            this.txtJumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(435, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 61;
            this.label1.Text = "Saldo Awal";
            // 
            // txtSaldoAwal
            // 
            this.txtSaldoAwal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSaldoAwal.Location = new System.Drawing.Point(501, 98);
            this.txtSaldoAwal.Name = "txtSaldoAwal";
            this.txtSaldoAwal.Size = new System.Drawing.Size(119, 20);
            this.txtSaldoAwal.TabIndex = 60;
            this.txtSaldoAwal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // gridSTS
            // 
            this.gridSTS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSTS.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridSTS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSTS.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NoBukuKas,
            this.TanggalBK,
            this.NoBuktiBK,
            this.KeteranganBK,
            this.PenerimaanBK});
            this.gridSTS.Location = new System.Drawing.Point(432, 121);
            this.gridSTS.Name = "gridSTS";
            this.gridSTS.Size = new System.Drawing.Size(393, 319);
            this.gridSTS.TabIndex = 59;
            // 
            // chkSetor
            // 
            this.chkSetor.AutoSize = true;
            this.chkSetor.Location = new System.Drawing.Point(48, 240);
            this.chkSetor.Name = "chkSetor";
            this.chkSetor.Size = new System.Drawing.Size(93, 17);
            this.chkSetor.TabIndex = 58;
            this.chkSetor.Text = "Register Setor";
            this.chkSetor.UseVisualStyleBackColor = true;
            // 
            // chkLangsung
            // 
            this.chkLangsung.AutoSize = true;
            this.chkLangsung.Location = new System.Drawing.Point(48, 216);
            this.chkLangsung.Name = "chkLangsung";
            this.chkLangsung.Size = new System.Drawing.Size(224, 17);
            this.chkLangsung.TabIndex = 57;
            this.chkLangsung.Text = "REgister Penerimaan Langsung Ke Kasda";
            this.chkLangsung.UseVisualStyleBackColor = true;
            // 
            // chkTBP
            // 
            this.chkTBP.AutoSize = true;
            this.chkTBP.Location = new System.Drawing.Point(48, 192);
            this.chkTBP.Name = "chkTBP";
            this.chkTBP.Size = new System.Drawing.Size(86, 17);
            this.chkTBP.TabIndex = 56;
            this.chkTBP.Text = "Regsier TBP";
            this.chkTBP.UseVisualStyleBackColor = true;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(837, 41);
            this.ctrlHeader1.TabIndex = 55;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 54;
            this.label2.Text = "OPD";
            // 
            // ctrlTanggalBulanVertikal1
            // 
            this.ctrlTanggalBulanVertikal1.Location = new System.Drawing.Point(22, 107);
            this.ctrlTanggalBulanVertikal1.Name = "ctrlTanggalBulanVertikal1";
            this.ctrlTanggalBulanVertikal1.Size = new System.Drawing.Size(404, 79);
            this.ctrlTanggalBulanVertikal1.TabIndex = 53;
            // 
            // cmdCetak
            // 
            this.cmdCetak.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdCetak.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCetak.Image = global::KUAPPAS.Properties.Resources.print;
            this.cmdCetak.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdCetak.Location = new System.Drawing.Point(554, 47);
            this.cmdCetak.Name = "cmdCetak";
            this.cmdCetak.Size = new System.Drawing.Size(101, 40);
            this.cmdCetak.TabIndex = 52;
            this.cmdCetak.Text = "Cetak ";
            this.cmdCetak.UseVisualStyleBackColor = true;
            this.cmdCetak.Visible = false;
            // 
            // cmdPanggilData
            // 
            this.cmdPanggilData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdPanggilData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPanggilData.Image = global::KUAPPAS.Properties.Resources.search;
            this.cmdPanggilData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdPanggilData.Location = new System.Drawing.Point(438, 47);
            this.cmdPanggilData.Name = "cmdPanggilData";
            this.cmdPanggilData.Size = new System.Drawing.Size(111, 40);
            this.cmdPanggilData.TabIndex = 51;
            this.cmdPanggilData.Text = "Panggil Data";
            this.cmdPanggilData.UseVisualStyleBackColor = true;
            this.cmdPanggilData.Click += new System.EventHandler(this.cmdPanggilData_Click);
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(61, 58);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(365, 43);
            this.ctrlDinas1.TabIndex = 50;
            // 
            // NoBukuKas
            // 
            this.NoBukuKas.HeaderText = "No";
            this.NoBukuKas.Name = "NoBukuKas";
            this.NoBukuKas.ReadOnly = true;
            this.NoBukuKas.Width = 50;
            // 
            // TanggalBK
            // 
            this.TanggalBK.HeaderText = "Tanggal";
            this.TanggalBK.Name = "TanggalBK";
            this.TanggalBK.ReadOnly = true;
            // 
            // NoBuktiBK
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NoBuktiBK.DefaultCellStyle = dataGridViewCellStyle1;
            this.NoBuktiBK.HeaderText = "Nomor Bukti";
            this.NoBuktiBK.Name = "NoBuktiBK";
            this.NoBuktiBK.ReadOnly = true;
            // 
            // KeteranganBK
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.KeteranganBK.DefaultCellStyle = dataGridViewCellStyle2;
            this.KeteranganBK.HeaderText = "Keterangan";
            this.KeteranganBK.Name = "KeteranganBK";
            this.KeteranganBK.ReadOnly = true;
            this.KeteranganBK.Width = 300;
            // 
            // PenerimaanBK
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.PenerimaanBK.DefaultCellStyle = dataGridViewCellStyle3;
            this.PenerimaanBK.HeaderText = "Jumlah";
            this.PenerimaanBK.Name = "PenerimaanBK";
            this.PenerimaanBK.ReadOnly = true;
            // 
            // frmRegisterSTS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 444);
            this.Controls.Add(this.cmdExcell);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSaldoAwal);
            this.Controls.Add(this.gridSTS);
            this.Controls.Add(this.chkSetor);
            this.Controls.Add(this.chkLangsung);
            this.Controls.Add(this.chkTBP);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ctrlTanggalBulanVertikal1);
            this.Controls.Add(this.cmdCetak);
            this.Controls.Add(this.cmdPanggilData);
            this.Controls.Add(this.ctrlDinas1);
            this.Name = "frmRegisterSTS";
            this.Text = "frmRegisterSTS";
            this.Load += new System.EventHandler(this.frmRegisterSTS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSTS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private ctrlTanggalBulanVertikal ctrlTanggalBulanVertikal1;
        private System.Windows.Forms.Button cmdCetak;
        private System.Windows.Forms.Button cmdPanggilData;
        private ctrlDinas ctrlDinas1;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.CheckBox chkTBP;
        private System.Windows.Forms.CheckBox chkLangsung;
        private System.Windows.Forms.CheckBox chkSetor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSaldoAwal;
        private System.Windows.Forms.DataGridView gridSTS;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdExcell;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoBukuKas;
        private System.Windows.Forms.DataGridViewTextBoxColumn TanggalBK;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoBuktiBK;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeteranganBK;
        private System.Windows.Forms.DataGridViewTextBoxColumn PenerimaanBK;
    }
}