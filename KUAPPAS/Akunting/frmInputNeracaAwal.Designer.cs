namespace KUAPPAS.Akunting
{
    partial class frmInputNeracaAwal
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmdSimpan = new System.Windows.Forms.Button();
            this.cmdPanggil = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.treeRekening1 = new KUAPPAS.treeRekening();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.ctrlSKPD1 = new KUAPPAS.ctrlSKPD();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtJumlahKredit = new System.Windows.Forms.TextBox();
            this.txtJumlahDebet = new System.Windows.Forms.TextBox();
            this.gridNeracaAwal = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Debet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataLama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.CmdSimpanLO = new System.Windows.Forms.Button();
            this.gridNeracaAwalLO = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdExcel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridNeracaAwal)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridNeracaAwalLO)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdSimpan
            // 
            this.cmdSimpan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdSimpan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSimpan.Image = global::KUAPPAS.Properties.Resources.action_save;
            this.cmdSimpan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSimpan.Location = new System.Drawing.Point(3, 12);
            this.cmdSimpan.Name = "cmdSimpan";
            this.cmdSimpan.Size = new System.Drawing.Size(138, 41);
            this.cmdSimpan.TabIndex = 6;
            this.cmdSimpan.Text = "Simpan";
            this.cmdSimpan.UseVisualStyleBackColor = true;
            this.cmdSimpan.Click += new System.EventHandler(this.cmdSimpan_Click);
            // 
            // cmdPanggil
            // 
            this.cmdPanggil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdPanggil.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPanggil.Image = global::KUAPPAS.Properties.Resources.search;
            this.cmdPanggil.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdPanggil.Location = new System.Drawing.Point(524, 64);
            this.cmdPanggil.Name = "cmdPanggil";
            this.cmdPanggil.Size = new System.Drawing.Size(116, 27);
            this.cmdPanggil.TabIndex = 5;
            this.cmdPanggil.Text = "Panggil Data";
            this.cmdPanggil.UseVisualStyleBackColor = true;
            this.cmdPanggil.Click += new System.EventHandler(this.cmdPanggil_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "O P D";
            // 
            // treeRekening1
            // 
            this.treeRekening1.BackColor = System.Drawing.SystemColors.Control;
            this.treeRekening1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeRekening1.Location = new System.Drawing.Point(0, 97);
            this.treeRekening1.Name = "treeRekening1";
            this.treeRekening1.Profile = 1;
            this.treeRekening1.Size = new System.Drawing.Size(517, 494);
            this.treeRekening1.TabIndex = 2;
            this.treeRekening1.DoubleClicking += new KUAPPAS.treeRekening.ValueChangedEventHandler(this.treeRekening1_DoubleClicking);
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1012, 41);
            this.ctrlHeader1.TabIndex = 1;
            // 
            // ctrlSKPD1
            // 
            this.ctrlSKPD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSKPD1.Location = new System.Drawing.Point(79, 63);
            this.ctrlSKPD1.Name = "ctrlSKPD1";
            this.ctrlSKPD1.Size = new System.Drawing.Size(438, 22);
            this.ctrlSKPD1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(533, 97);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(479, 386);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.cmdSimpan);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtJumlahKredit);
            this.tabPage1.Controls.Add(this.txtJumlahDebet);
            this.tabPage1.Controls.Add(this.gridNeracaAwal);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(471, 360);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Neraca";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(142, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Jumlah kredit";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Jumlah debet";
            // 
            // txtJumlahKredit
            // 
            this.txtJumlahKredit.Location = new System.Drawing.Point(230, 33);
            this.txtJumlahKredit.Name = "txtJumlahKredit";
            this.txtJumlahKredit.Size = new System.Drawing.Size(233, 20);
            this.txtJumlahKredit.TabIndex = 7;
            // 
            // txtJumlahDebet
            // 
            this.txtJumlahDebet.Location = new System.Drawing.Point(227, 7);
            this.txtJumlahDebet.Name = "txtJumlahDebet";
            this.txtJumlahDebet.Size = new System.Drawing.Size(236, 20);
            this.txtJumlahDebet.TabIndex = 6;
            // 
            // gridNeracaAwal
            // 
            this.gridNeracaAwal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridNeracaAwal.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridNeracaAwal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridNeracaAwal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Kode,
            this.Nama,
            this.Debet,
            this.Kredit,
            this.DataLama});
            this.gridNeracaAwal.Location = new System.Drawing.Point(6, 55);
            this.gridNeracaAwal.Name = "gridNeracaAwal";
            this.gridNeracaAwal.Size = new System.Drawing.Size(452, 292);
            this.gridNeracaAwal.TabIndex = 5;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // Kode
            // 
            this.Kode.HeaderText = "Kode";
            this.Kode.Name = "Kode";
            // 
            // Nama
            // 
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Nama.DefaultCellStyle = dataGridViewCellStyle22;
            this.Nama.HeaderText = "Nama";
            this.Nama.Name = "Nama";
            this.Nama.ReadOnly = true;
            this.Nama.Width = 350;
            // 
            // Debet
            // 
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Debet.DefaultCellStyle = dataGridViewCellStyle23;
            this.Debet.HeaderText = "Debet";
            this.Debet.Name = "Debet";
            this.Debet.Width = 150;
            // 
            // Kredit
            // 
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Kredit.DefaultCellStyle = dataGridViewCellStyle24;
            this.Kredit.HeaderText = "Kredit";
            this.Kredit.Name = "Kredit";
            this.Kredit.Width = 150;
            // 
            // DataLama
            // 
            this.DataLama.HeaderText = "DataLama";
            this.DataLama.Name = "DataLama";
            this.DataLama.ReadOnly = true;
            this.DataLama.Width = 20;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.CmdSimpanLO);
            this.tabPage2.Controls.Add(this.gridNeracaAwalLO);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(471, 360);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Laporan  Opersional";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // CmdSimpanLO
            // 
            this.CmdSimpanLO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CmdSimpanLO.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmdSimpanLO.Location = new System.Drawing.Point(6, 5);
            this.CmdSimpanLO.Name = "CmdSimpanLO";
            this.CmdSimpanLO.Size = new System.Drawing.Size(136, 33);
            this.CmdSimpanLO.TabIndex = 7;
            this.CmdSimpanLO.Text = "Simpan LO";
            this.CmdSimpanLO.UseVisualStyleBackColor = true;
            this.CmdSimpanLO.Click += new System.EventHandler(this.CmdSimpanLO_Click);
            // 
            // gridNeracaAwalLO
            // 
            this.gridNeracaAwalLO.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridNeracaAwalLO.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridNeracaAwalLO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridNeracaAwalLO.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.gridNeracaAwalLO.Location = new System.Drawing.Point(3, 44);
            this.gridNeracaAwalLO.Name = "gridNeracaAwalLO";
            this.gridNeracaAwalLO.Size = new System.Drawing.Size(462, 316);
            this.gridNeracaAwalLO.TabIndex = 6;
            this.gridNeracaAwalLO.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridNeracaAwalLO_CellContentClick);
            this.gridNeracaAwalLO.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridNeracaAwalLO_CellEndEdit);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Kode";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle19;
            this.dataGridViewTextBoxColumn3.HeaderText = "Nama";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 350;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle20;
            this.dataGridViewTextBoxColumn4.HeaderText = "Debet";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle21;
            this.dataGridViewTextBoxColumn5.HeaderText = "Kredit";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 150;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "DataLama";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 20;
            // 
            // cmdExcel
            // 
            this.cmdExcel.Location = new System.Drawing.Point(646, 67);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Size = new System.Drawing.Size(111, 25);
            this.cmdExcel.TabIndex = 8;
            this.cmdExcel.Text = "Excel";
            this.cmdExcel.UseVisualStyleBackColor = true;
            this.cmdExcel.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmInputNeracaAwal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 478);
            this.Controls.Add(this.cmdExcel);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cmdPanggil);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeRekening1);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.ctrlSKPD1);
            this.Name = "frmInputNeracaAwal";
            this.Text = "frmInputSaldoAwal";
            this.Load += new System.EventHandler(this.frmInputSaldoAwal_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridNeracaAwal)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridNeracaAwalLO)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlSKPD ctrlSKPD1;
        private ctrlHeader ctrlHeader1;
        private treeRekening treeRekening1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdPanggil;
        private System.Windows.Forms.Button cmdSimpan;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView gridNeracaAwal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn Debet;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kredit;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataLama;
        private System.Windows.Forms.DataGridView gridNeracaAwalLO;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtJumlahKredit;
        private System.Windows.Forms.TextBox txtJumlahDebet;
        private System.Windows.Forms.Button CmdSimpanLO;
        private System.Windows.Forms.Button cmdExcel;
    }
}