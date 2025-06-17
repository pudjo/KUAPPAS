namespace KUAPPAS.Bendahara
{
    partial class frmListPengeluaran
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mnuSPJ = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Panjar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPengembalianPanjar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBPK = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSPJPanjar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuADD = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbJenis = new System.Windows.Forms.ComboBox();
            this.cmdLoadData = new System.Windows.Forms.Button();
            this.ctrlTanggalBulanVertikal1 = new KUAPPAS.Bendahara.ctrlTanggalBulanVertikal();
            this.label4 = new System.Windows.Forms.Label();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdCariLagi = new System.Windows.Forms.Button();
            this.cmdCari = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.lblPencarian = new System.Windows.Forms.Label();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.chkBLUD = new System.Windows.Forms.CheckBox();
            this.chkTU = new System.Windows.Forms.CheckBox();
            this.chkUPGU = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.gridBPK = new System.Windows.Forms.DataGridView();
            this.NoUrutBPK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailBPK = new System.Windows.Forms.DataGridViewButtonColumn();
            this.piihspj = new System.Windows.Forms.DataGridViewButtonColumn();
            this.NomorBPK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TanggalBPK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KeteranganBPK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PenerimaBPK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JumlahBPK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdd2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdBKU = new System.Windows.Forms.Button();
            this.mnuSPJ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBPK)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuSPJ
            // 
            this.mnuSPJ.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Panjar,
            this.mnuPengembalianPanjar,
            this.mnuBPK,
            this.mnuSPJPanjar,
            this.mnuADD});
            this.mnuSPJ.Name = "mnuSPJ";
            this.mnuSPJ.Size = new System.Drawing.Size(228, 114);
            // 
            // Panjar
            // 
            this.Panjar.Name = "Panjar";
            this.Panjar.Size = new System.Drawing.Size(227, 22);
            this.Panjar.Text = "Panjar";
            this.Panjar.Click += new System.EventHandler(this.Panjar_Click);
            // 
            // mnuPengembalianPanjar
            // 
            this.mnuPengembalianPanjar.Name = "mnuPengembalianPanjar";
            this.mnuPengembalianPanjar.Size = new System.Drawing.Size(227, 22);
            this.mnuPengembalianPanjar.Text = "PengembalianSisa Panjar";
            this.mnuPengembalianPanjar.Click += new System.EventHandler(this.mnuPengembalianPanjar_Click);
            // 
            // mnuBPK
            // 
            this.mnuBPK.Name = "mnuBPK";
            this.mnuBPK.Size = new System.Drawing.Size(227, 22);
            this.mnuBPK.Text = "SPJ/Bukti Pengeluaran Kas";
            this.mnuBPK.Click += new System.EventHandler(this.mnuBPK_Click);
            // 
            // mnuSPJPanjar
            // 
            this.mnuSPJPanjar.Name = "mnuSPJPanjar";
            this.mnuSPJPanjar.Size = new System.Drawing.Size(227, 22);
            this.mnuSPJPanjar.Text = "Pertanggung Jawaban Panjar";
            this.mnuSPJPanjar.Click += new System.EventHandler(this.mnuSPJPanjar_Click);
            // 
            // mnuADD
            // 
            this.mnuADD.Name = "mnuADD";
            this.mnuADD.Size = new System.Drawing.Size(227, 22);
            this.mnuADD.Text = "Belanja ADD";
            this.mnuADD.Click += new System.EventHandler(this.mnuADD_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 47;
            this.label5.Text = "Jenis";
            // 
            // cmbJenis
            // 
            this.cmbJenis.FormattingEnabled = true;
            this.cmbJenis.Location = new System.Drawing.Point(86, 137);
            this.cmbJenis.Name = "cmbJenis";
            this.cmbJenis.Size = new System.Drawing.Size(343, 21);
            this.cmbJenis.TabIndex = 46;
            // 
            // cmdLoadData
            // 
            this.cmdLoadData.BackColor = System.Drawing.Color.White;
            this.cmdLoadData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdLoadData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLoadData.Location = new System.Drawing.Point(12, 334);
            this.cmdLoadData.Name = "cmdLoadData";
            this.cmdLoadData.Size = new System.Drawing.Size(172, 41);
            this.cmdLoadData.TabIndex = 45;
            this.cmdLoadData.Text = "Panggil Data";
            this.cmdLoadData.UseVisualStyleBackColor = false;
            this.cmdLoadData.Click += new System.EventHandler(this.cmdLoadData_Click);
            // 
            // ctrlTanggalBulanVertikal1
            // 
            this.ctrlTanggalBulanVertikal1.Bulan = 0;
            this.ctrlTanggalBulanVertikal1.JenisPeriode = 1;
            this.ctrlTanggalBulanVertikal1.Location = new System.Drawing.Point(-1, 163);
            this.ctrlTanggalBulanVertikal1.Name = "ctrlTanggalBulanVertikal1";
            this.ctrlTanggalBulanVertikal1.Size = new System.Drawing.Size(435, 103);
            this.ctrlTanggalBulanVertikal1.TabIndex = 44;
            this.ctrlTanggalBulanVertikal1.TanggalAkhir = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            this.ctrlTanggalBulanVertikal1.TanggalAwal = new System.DateTime(2020, 5, 23, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 16);
            this.label4.TabIndex = 43;
            this.label4.Text = "O P D";
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.BackColor = System.Drawing.SystemColors.Control;
            this.ctrlDinas1.Location = new System.Drawing.Point(86, 91);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(343, 49);
            this.ctrlDinas1.TabIndex = 42;
            this.ctrlDinas1.UK = 0;
            // 
            // cmdAdd
            // 
            this.cmdAdd.BackColor = System.Drawing.Color.White;
            this.cmdAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAdd.Location = new System.Drawing.Point(190, 334);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(172, 41);
            this.cmdAdd.TabIndex = 41;
            this.cmdAdd.Text = "Tambah Data SPJ";
            this.cmdAdd.UseVisualStyleBackColor = false;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1068, 79);
            this.ctrlHeader1.TabIndex = 39;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(848, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 38;
            this.label3.Text = "Jumlah";
            // 
            // cmdCariLagi
            // 
            this.cmdCariLagi.Location = new System.Drawing.Point(909, 94);
            this.cmdCariLagi.Name = "cmdCariLagi";
            this.cmdCariLagi.Size = new System.Drawing.Size(75, 23);
            this.cmdCariLagi.TabIndex = 37;
            this.cmdCariLagi.Text = "Cari Lagi";
            this.cmdCariLagi.UseVisualStyleBackColor = true;
            this.cmdCariLagi.Visible = false;
            this.cmdCariLagi.Click += new System.EventHandler(this.cmdCariLagi_Click);
            // 
            // cmdCari
            // 
            this.cmdCari.Location = new System.Drawing.Point(844, 94);
            this.cmdCari.Name = "cmdCari";
            this.cmdCari.Size = new System.Drawing.Size(62, 23);
            this.cmdCari.TabIndex = 36;
            this.cmdCari.Text = "Cari...";
            this.cmdCari.UseVisualStyleBackColor = true;
            this.cmdCari.Visible = false;
            this.cmdCari.Click += new System.EventHandler(this.cmdCari_Click);
            // 
            // txtCari
            // 
            this.txtCari.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCari.Location = new System.Drawing.Point(621, 95);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(221, 20);
            this.txtCari.TabIndex = 35;
            this.txtCari.Visible = false;
            // 
            // lblPencarian
            // 
            this.lblPencarian.AutoSize = true;
            this.lblPencarian.Location = new System.Drawing.Point(557, 99);
            this.lblPencarian.Name = "lblPencarian";
            this.lblPencarian.Size = new System.Drawing.Size(55, 13);
            this.lblPencarian.TabIndex = 34;
            this.lblPencarian.Text = "Pencarian";
            this.lblPencarian.Visible = false;
            // 
            // txtJumlah
            // 
            this.txtJumlah.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlah.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlah.Location = new System.Drawing.Point(911, 95);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(157, 22);
            this.txtJumlah.TabIndex = 33;
            this.txtJumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkBLUD
            // 
            this.chkBLUD.AutoSize = true;
            this.chkBLUD.Location = new System.Drawing.Point(287, 299);
            this.chkBLUD.Name = "chkBLUD";
            this.chkBLUD.Size = new System.Drawing.Size(55, 17);
            this.chkBLUD.TabIndex = 32;
            this.chkBLUD.Text = "BLUD";
            this.chkBLUD.UseVisualStyleBackColor = true;
            // 
            // chkTU
            // 
            this.chkTU.AutoSize = true;
            this.chkTU.Location = new System.Drawing.Point(221, 299);
            this.chkTU.Name = "chkTU";
            this.chkTU.Size = new System.Drawing.Size(47, 17);
            this.chkTU.TabIndex = 31;
            this.chkTU.Text = "T U ";
            this.chkTU.UseVisualStyleBackColor = true;
            // 
            // chkUPGU
            // 
            this.chkUPGU.AutoSize = true;
            this.chkUPGU.Location = new System.Drawing.Point(137, 298);
            this.chkUPGU.Name = "chkUPGU";
            this.chkUPGU.Size = new System.Drawing.Size(62, 17);
            this.chkUPGU.TabIndex = 30;
            this.chkUPGU.Text = "UP/GU";
            this.chkUPGU.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 268);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 28;
            this.label2.Text = "No Bukti";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 298);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 16);
            this.label1.TabIndex = 27;
            this.label1.Text = "Jenis Belanja";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(87, 268);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(343, 20);
            this.textBox1.TabIndex = 22;
            // 
            // gridBPK
            // 
            this.gridBPK.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridBPK.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridBPK.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridBPK.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gridBPK.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NoUrutBPK,
            this.DetailBPK,
            this.piihspj,
            this.NomorBPK,
            this.TanggalBPK,
            this.KeteranganBPK,
            this.PenerimaBPK,
            this.JumlahBPK,
            this.colAdd2});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridBPK.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridBPK.Location = new System.Drawing.Point(441, 126);
            this.gridBPK.Name = "gridBPK";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridBPK.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridBPK.Size = new System.Drawing.Size(627, 349);
            this.gridBPK.TabIndex = 8;
            this.gridBPK.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridBPK_CellContentClick);
            // 
            // NoUrutBPK
            // 
            this.NoUrutBPK.HeaderText = "Column1";
            this.NoUrutBPK.Name = "NoUrutBPK";
            this.NoUrutBPK.Visible = false;
            // 
            // DetailBPK
            // 
            this.DetailBPK.HeaderText = "Detail";
            this.DetailBPK.Name = "DetailBPK";
            this.DetailBPK.ReadOnly = true;
            this.DetailBPK.Width = 60;
            // 
            // piihspj
            // 
            this.piihspj.HeaderText = "BKU";
            this.piihspj.Name = "piihspj";
            this.piihspj.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.piihspj.Width = 70;
            // 
            // NomorBPK
            // 
            this.NomorBPK.HeaderText = "No Bukti";
            this.NomorBPK.Name = "NomorBPK";
            this.NomorBPK.ReadOnly = true;
            this.NomorBPK.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NomorBPK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TanggalBPK
            // 
            this.TanggalBPK.HeaderText = "Tanggal";
            this.TanggalBPK.Name = "TanggalBPK";
            this.TanggalBPK.ReadOnly = true;
            this.TanggalBPK.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TanggalBPK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TanggalBPK.Width = 80;
            // 
            // KeteranganBPK
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.KeteranganBPK.DefaultCellStyle = dataGridViewCellStyle1;
            this.KeteranganBPK.HeaderText = "Keterangan";
            this.KeteranganBPK.Name = "KeteranganBPK";
            this.KeteranganBPK.ReadOnly = true;
            this.KeteranganBPK.Width = 300;
            // 
            // PenerimaBPK
            // 
            this.PenerimaBPK.HeaderText = "Penerima";
            this.PenerimaBPK.Name = "PenerimaBPK";
            this.PenerimaBPK.ReadOnly = true;
            // 
            // JumlahBPK
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.JumlahBPK.DefaultCellStyle = dataGridViewCellStyle2;
            this.JumlahBPK.HeaderText = "Jumlah";
            this.JumlahBPK.Name = "JumlahBPK";
            this.JumlahBPK.ReadOnly = true;
            // 
            // colAdd2
            // 
            this.colAdd2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colAdd2.HeaderText = "";
            this.colAdd2.Name = "colAdd2";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "No Bukti";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Tanggal";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 80;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Penerima";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 200;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn5.HeaderText = "Keterangan";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 450;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn6.HeaderText = "Jumlah";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn7.HeaderText = "";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // cmdBKU
            // 
            this.cmdBKU.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBKU.Location = new System.Drawing.Point(441, 94);
            this.cmdBKU.Name = "cmdBKU";
            this.cmdBKU.Size = new System.Drawing.Size(112, 23);
            this.cmdBKU.TabIndex = 48;
            this.cmdBKU.Text = "BKU kan Semua";
            this.cmdBKU.UseVisualStyleBackColor = true;
            this.cmdBKU.Click += new System.EventHandler(this.cmdBKU_Click);
            // 
            // frmListPengeluaran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 471);
            this.Controls.Add(this.cmdBKU);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbJenis);
            this.Controls.Add(this.cmdLoadData);
            this.Controls.Add(this.ctrlTanggalBulanVertikal1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.cmdAdd);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdCariLagi);
            this.Controls.Add(this.cmdCari);
            this.Controls.Add(this.txtCari);
            this.Controls.Add(this.lblPencarian);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.chkBLUD);
            this.Controls.Add(this.chkTU);
            this.Controls.Add(this.chkUPGU);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.gridBPK);
            this.Name = "frmListPengeluaran";
            this.Text = "Daftar Pengeluaran";
            this.Load += new System.EventHandler(this.frmListBPK_Load);
            this.mnuSPJ.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridBPK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView gridBPK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkUPGU;
        private System.Windows.Forms.CheckBox chkTU;
        private System.Windows.Forms.CheckBox chkBLUD;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.Button cmdCariLagi;
        private System.Windows.Forms.Button cmdCari;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Label lblPencarian;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.ContextMenuStrip mnuSPJ;
        private System.Windows.Forms.ToolStripMenuItem Panjar;
        private System.Windows.Forms.ToolStripMenuItem mnuBPK;
        private System.Windows.Forms.ToolStripMenuItem mnuSPJPanjar;
        private ctrlDinas ctrlDinas1;
        private System.Windows.Forms.Label label4;
        private ctrlTanggalBulanVertikal ctrlTanggalBulanVertikal1;
        private System.Windows.Forms.Button cmdLoadData;
        private System.Windows.Forms.ComboBox cmbJenis;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoUrutBPK;
        private System.Windows.Forms.DataGridViewButtonColumn DetailBPK;
        private System.Windows.Forms.DataGridViewButtonColumn piihspj;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomorBPK;
        private System.Windows.Forms.DataGridViewTextBoxColumn TanggalBPK;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeteranganBPK;
        private System.Windows.Forms.DataGridViewTextBoxColumn PenerimaBPK;
        private System.Windows.Forms.DataGridViewTextBoxColumn JumlahBPK;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdd2;
        private System.Windows.Forms.Button cmdBKU;
        private System.Windows.Forms.ToolStripMenuItem mnuPengembalianPanjar;
        private System.Windows.Forms.ToolStripMenuItem mnuADD;
    }
}