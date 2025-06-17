namespace KUAPPAS.Akunting
{
    partial class frmSelisihTrxBB
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridSelisih = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIDRekening = new System.Windows.Forms.TextBox();
            this.cmdTampilkan = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdHapusRekeningIni = new System.Windows.Forms.Button();
            this.chkTampilkanYangbeda = new System.Windows.Forms.CheckBox();
            this.NoUrut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoBukti = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tanggal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Transaksi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Beda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridSelisih)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridSelisih
            // 
            this.gridSelisih.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSelisih.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NoUrut,
            this.NoBukti,
            this.Tanggal,
            this.Transaksi,
            this.BB,
            this.Beda});
            this.gridSelisih.Location = new System.Drawing.Point(6, 35);
            this.gridSelisih.Name = "gridSelisih";
            this.gridSelisih.Size = new System.Drawing.Size(668, 253);
            this.gridSelisih.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // txtIDRekening
            // 
            this.txtIDRekening.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIDRekening.Location = new System.Drawing.Point(42, 61);
            this.txtIDRekening.Name = "txtIDRekening";
            this.txtIDRekening.Size = new System.Drawing.Size(206, 20);
            this.txtIDRekening.TabIndex = 2;
            // 
            // cmdTampilkan
            // 
            this.cmdTampilkan.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cmdTampilkan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTampilkan.Location = new System.Drawing.Point(42, 100);
            this.cmdTampilkan.Name = "cmdTampilkan";
            this.cmdTampilkan.Size = new System.Drawing.Size(87, 23);
            this.cmdTampilkan.TabIndex = 3;
            this.cmdTampilkan.Text = "Tampilkan";
            this.cmdTampilkan.UseVisualStyleBackColor = false;
            this.cmdTampilkan.Click += new System.EventHandler(this.cmdTampilkan_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Kode Rekening";
            // 
            // cmdClose
            // 
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(580, 435);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(89, 39);
            this.cmdClose.TabIndex = 5;
            this.cmdClose.Text = "Tutup";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 491);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(690, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 128);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(690, 301);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkTampilkanYangbeda);
            this.tabPage1.Controls.Add(this.gridSelisih);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(682, 275);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tidak Sama I";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "NoIUrut";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "No  Bukti";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Tanggal";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn4.HeaderText = "Transaksi";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn5.HeaderText = "Buku Besar";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 150;
            // 
            // cmdHapusRekeningIni
            // 
            this.cmdHapusRekeningIni.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cmdHapusRekeningIni.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdHapusRekeningIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHapusRekeningIni.Location = new System.Drawing.Point(277, 54);
            this.cmdHapusRekeningIni.Name = "cmdHapusRekeningIni";
            this.cmdHapusRekeningIni.Size = new System.Drawing.Size(137, 31);
            this.cmdHapusRekeningIni.TabIndex = 8;
            this.cmdHapusRekeningIni.Text = "Hapus Rekening Ini";
            this.cmdHapusRekeningIni.UseVisualStyleBackColor = false;
            this.cmdHapusRekeningIni.Click += new System.EventHandler(this.cmdHapusRekeningIni_Click);
            // 
            // chkTampilkanYangbeda
            // 
            this.chkTampilkanYangbeda.AutoSize = true;
            this.chkTampilkanYangbeda.Location = new System.Drawing.Point(19, 10);
            this.chkTampilkanYangbeda.Name = "chkTampilkanYangbeda";
            this.chkTampilkanYangbeda.Size = new System.Drawing.Size(158, 17);
            this.chkTampilkanYangbeda.TabIndex = 1;
            this.chkTampilkanYangbeda.Text = "TampilkancYang Beda Saja";
            this.chkTampilkanYangbeda.UseVisualStyleBackColor = true;
            this.chkTampilkanYangbeda.CheckedChanged += new System.EventHandler(this.chkTampilkanYangbeda_CheckedChanged);
            // 
            // NoUrut
            // 
            this.NoUrut.HeaderText = "NoIUrut";
            this.NoUrut.Name = "NoUrut";
            this.NoUrut.ReadOnly = true;
            this.NoUrut.Visible = false;
            // 
            // NoBukti
            // 
            this.NoBukti.HeaderText = "No  Bukti";
            this.NoBukti.Name = "NoBukti";
            this.NoBukti.ReadOnly = true;
            this.NoBukti.Width = 200;
            // 
            // Tanggal
            // 
            this.Tanggal.HeaderText = "Tanggal";
            this.Tanggal.Name = "Tanggal";
            this.Tanggal.ReadOnly = true;
            // 
            // Transaksi
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Transaksi.DefaultCellStyle = dataGridViewCellStyle1;
            this.Transaksi.HeaderText = "Transaksi";
            this.Transaksi.Name = "Transaksi";
            this.Transaksi.ReadOnly = true;
            this.Transaksi.Width = 150;
            // 
            // BB
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.BB.DefaultCellStyle = dataGridViewCellStyle2;
            this.BB.HeaderText = "Buku Besar";
            this.BB.Name = "BB";
            this.BB.ReadOnly = true;
            this.BB.Width = 150;
            // 
            // Beda
            // 
            this.Beda.HeaderText = "Column1";
            this.Beda.Name = "Beda";
            this.Beda.ReadOnly = true;
            this.Beda.Visible = false;
            this.Beda.Width = 10;
            // 
            // frmSelisihTrxBB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 513);
            this.Controls.Add(this.cmdHapusRekeningIni);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdTampilkan);
            this.Controls.Add(this.txtIDRekening);
            this.Controls.Add(this.label1);
            this.Name = "frmSelisihTrxBB";
            this.Text = "Selisih Transaksi dan Buku Besar";
            this.Load += new System.EventHandler(this.frmSelisihTrxBB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSelisih)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridSelisih;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIDRekening;
        private System.Windows.Forms.Button cmdTampilkan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button cmdHapusRekeningIni;
        private System.Windows.Forms.CheckBox chkTampilkanYangbeda;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoUrut;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoBukti;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tanggal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Transaksi;
        private System.Windows.Forms.DataGridViewTextBoxColumn BB;
        private System.Windows.Forms.DataGridViewTextBoxColumn Beda;
    }
}