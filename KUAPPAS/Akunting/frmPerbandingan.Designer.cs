namespace KUAPPAS.Akunting
{
    partial class frmPerbandingan
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridBKUBB = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.cmdPanggilBukubesar = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlPeriode1 = new KUAPPAS.Bendahara.ctrlPeriode();
            this.ctrlSKPD1 = new KUAPPAS.ctrlSKPD();
            this.NoUrutSUmberBKU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TanggalBKU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JumlahAnggaran = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JumlahBKU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaldoBKU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoSumberBB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoBuktiBB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TanggalBB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DebetBB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KreditBB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saldoBB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridBKUBB)).BeginInit();
            this.SuspendLayout();
            // 
            // gridBKUBB
            // 
            this.gridBKUBB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridBKUBB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridBKUBB.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NoUrutSUmberBKU,
            this.kode,
            this.TanggalBKU,
            this.JumlahAnggaran,
            this.JumlahBKU,
            this.SaldoBKU,
            this.NoSumberBB,
            this.NoBuktiBB,
            this.TanggalBB,
            this.DebetBB,
            this.KreditBB,
            this.saldoBB});
            this.gridBKUBB.Location = new System.Drawing.Point(-22, 80);
            this.gridBKUBB.Name = "gridBKUBB";
            this.gridBKUBB.Size = new System.Drawing.Size(1203, 379);
            this.gridBKUBB.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(526, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Panggil BKU";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmdPanggilBukubesar
            // 
            this.cmdPanggilBukubesar.Location = new System.Drawing.Point(617, 47);
            this.cmdPanggilBukubesar.Name = "cmdPanggilBukubesar";
            this.cmdPanggilBukubesar.Size = new System.Drawing.Size(128, 23);
            this.cmdPanggilBukubesar.TabIndex = 4;
            this.cmdPanggilBukubesar.Text = "Penggil BUKU BESAR";
            this.cmdPanggilBukubesar.UseVisualStyleBackColor = true;
            this.cmdPanggilBukubesar.Click += new System.EventHandler(this.cmdPanggilBukubesar_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "kodeUK";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "kode";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 250;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "JumkahAnggaran";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "JumlahBKU";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 200;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Sldo BKU";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 200;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "No Bukti";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn7.HeaderText = "NoBukt BBi";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn8.HeaderText = "Debet Buku Besar";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dataGridViewTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn9.HeaderText = "Kredit BB";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn10.HeaderText = "Saldo BB";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Tanggal BB";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn12
            // 
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dataGridViewTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn12.HeaderText = "oSumberBB";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            // 
            // ctrlPeriode1
            // 
            this.ctrlPeriode1.Location = new System.Drawing.Point(206, 47);
            this.ctrlPeriode1.Margin = new System.Windows.Forms.Padding(0);
            this.ctrlPeriode1.Name = "ctrlPeriode1";
            this.ctrlPeriode1.Size = new System.Drawing.Size(284, 26);
            this.ctrlPeriode1.TabIndex = 3;
            this.ctrlPeriode1.TanggalAkhir = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            this.ctrlPeriode1.TanggalAwaal = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            // 
            // ctrlSKPD1
            // 
            this.ctrlSKPD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSKPD1.Location = new System.Drawing.Point(206, 12);
            this.ctrlSKPD1.Name = "ctrlSKPD1";
            this.ctrlSKPD1.Size = new System.Drawing.Size(518, 28);
            this.ctrlSKPD1.TabIndex = 1;
            // 
            // NoUrutSUmberBKU
            // 
            this.NoUrutSUmberBKU.HeaderText = "No UrutSumber BKU";
            this.NoUrutSUmberBKU.Name = "NoUrutSUmberBKU";
            // 
            // kode
            // 
            this.kode.HeaderText = "No Bukti";
            this.kode.Name = "kode";
            this.kode.Width = 250;
            // 
            // TanggalBKU
            // 
            this.TanggalBKU.HeaderText = "Tanggal BKU";
            this.TanggalBKU.Name = "TanggalBKU";
            this.TanggalBKU.ReadOnly = true;
            // 
            // JumlahAnggaran
            // 
            this.JumlahAnggaran.HeaderText = "Debet BKU";
            this.JumlahAnggaran.Name = "JumlahAnggaran";
            this.JumlahAnggaran.ReadOnly = true;
            this.JumlahAnggaran.Width = 200;
            // 
            // JumlahBKU
            // 
            this.JumlahBKU.HeaderText = "krdit BKU";
            this.JumlahBKU.Name = "JumlahBKU";
            this.JumlahBKU.ReadOnly = true;
            this.JumlahBKU.Width = 200;
            // 
            // SaldoBKU
            // 
            this.SaldoBKU.HeaderText = "Sldo BKU";
            this.SaldoBKU.Name = "SaldoBKU";
            this.SaldoBKU.ReadOnly = true;
            // 
            // NoSumberBB
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.NoSumberBB.DefaultCellStyle = dataGridViewCellStyle1;
            this.NoSumberBB.HeaderText = "NoSumberBB";
            this.NoSumberBB.Name = "NoSumberBB";
            this.NoSumberBB.ReadOnly = true;
            // 
            // NoBuktiBB
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.NoBuktiBB.DefaultCellStyle = dataGridViewCellStyle2;
            this.NoBuktiBB.HeaderText = "NoBukt BBi";
            this.NoBuktiBB.Name = "NoBuktiBB";
            this.NoBuktiBB.ReadOnly = true;
            // 
            // TanggalBB
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.TanggalBB.DefaultCellStyle = dataGridViewCellStyle3;
            this.TanggalBB.HeaderText = "Tanggal BB";
            this.TanggalBB.Name = "TanggalBB";
            this.TanggalBB.ReadOnly = true;
            // 
            // DebetBB
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.DebetBB.DefaultCellStyle = dataGridViewCellStyle4;
            this.DebetBB.HeaderText = "Debet Buku Besar";
            this.DebetBB.Name = "DebetBB";
            this.DebetBB.ReadOnly = true;
            // 
            // KreditBB
            // 
            this.KreditBB.HeaderText = "Kredit BB";
            this.KreditBB.Name = "KreditBB";
            this.KreditBB.ReadOnly = true;
            // 
            // saldoBB
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.saldoBB.DefaultCellStyle = dataGridViewCellStyle5;
            this.saldoBB.HeaderText = "Saldo BB";
            this.saldoBB.Name = "saldoBB";
            this.saldoBB.ReadOnly = true;
            // 
            // frmPerbandingan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1177, 471);
            this.Controls.Add(this.cmdPanggilBukubesar);
            this.Controls.Add(this.ctrlPeriode1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ctrlSKPD1);
            this.Controls.Add(this.gridBKUBB);
            this.Name = "frmPerbandingan";
            this.Text = "frmPerbandingan";
            this.Load += new System.EventHandler(this.frmPerbandingan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridBKUBB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridBKUBB;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private ctrlSKPD ctrlSKPD1;
        private System.Windows.Forms.Button button1;
        private Bendahara.ctrlPeriode ctrlPeriode1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.Button cmdPanggilBukubesar;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoUrutSUmberBKU;
        private System.Windows.Forms.DataGridViewTextBoxColumn kode;
        private System.Windows.Forms.DataGridViewTextBoxColumn TanggalBKU;
        private System.Windows.Forms.DataGridViewTextBoxColumn JumlahAnggaran;
        private System.Windows.Forms.DataGridViewTextBoxColumn JumlahBKU;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaldoBKU;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoSumberBB;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoBuktiBB;
        private System.Windows.Forms.DataGridViewTextBoxColumn TanggalBB;
        private System.Windows.Forms.DataGridViewTextBoxColumn DebetBB;
        private System.Windows.Forms.DataGridViewTextBoxColumn KreditBB;
        private System.Windows.Forms.DataGridViewTextBoxColumn saldoBB;
    }
}