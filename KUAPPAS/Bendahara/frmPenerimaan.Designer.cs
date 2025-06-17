namespace KUAPPAS.Bendahara
{
    partial class frmPenerimaan
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.txtNoBukti = new System.Windows.Forms.TextBox();
            this.gridPenerimaan = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPenyetor = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmdCari = new System.Windows.Forms.Button();
            this.txtIDRekening = new System.Windows.Forms.TextBox();
            this.txtNamaRekening = new System.Windows.Forms.TextBox();
            this.txtJumlahPerRekening = new System.Windows.Forms.TextBox();
            this.cmdAddToList = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tanggal = new KUAPPAS.ctrlTanggal();
            this.ctrlComboAnggaran1 = new KUAPPAS.Bendahara.ctrlPilihanRekeningAnggaran();
            this.ctrlVia1 = new KUAPPAS.ctrlVia();
            this.ctrlSKPDSKPD1 = new KUAPPAS.Bendahara.ctrlSKPDSKPD();
            this.ctrlNavigation1 = new KUAPPAS.ctrlNavigation();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.chkTransferLangsungKasda = new System.Windows.Forms.CheckBox();
            this.ctrlFooter1 = new KUAPPAS.ctrlFooter();
            this.IDRekening = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JumlahKetetapan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JumlahTerima = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridPenerimaan)).BeginInit();
            this.SuspendLayout();
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKeterangan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKeterangan.Location = new System.Drawing.Point(193, 213);
            this.txtKeterangan.Multiline = true;
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(580, 38);
            this.txtKeterangan.TabIndex = 4;
            // 
            // txtNoBukti
            // 
            this.txtNoBukti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoBukti.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoBukti.Location = new System.Drawing.Point(192, 188);
            this.txtNoBukti.Name = "txtNoBukti";
            this.txtNoBukti.Size = new System.Drawing.Size(580, 22);
            this.txtNoBukti.TabIndex = 5;
            // 
            // gridPenerimaan
            // 
            this.gridPenerimaan.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridPenerimaan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridPenerimaan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPenerimaan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDRekening,
            this.Nama,
            this.JumlahKetetapan,
            this.JumlahTerima,
            this.ID});
            this.gridPenerimaan.Location = new System.Drawing.Point(0, 401);
            this.gridPenerimaan.Name = "gridPenerimaan";
            this.gridPenerimaan.Size = new System.Drawing.Size(809, 141);
            this.gridPenerimaan.TabIndex = 8;
            this.gridPenerimaan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPenerimaan_CellContentClick);
            this.gridPenerimaan.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPenerimaan_CellEndEdit);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(64, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Jenis Penerimaan";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(64, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Via Bak atau tunai";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(64, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "No SKR/SKPD";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(64, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "No Bukti";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(64, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Tanggal";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(64, 215);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "Katerangan";
            // 
            // txtPenyetor
            // 
            this.txtPenyetor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPenyetor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPenyetor.Location = new System.Drawing.Point(193, 253);
            this.txtPenyetor.Name = "txtPenyetor";
            this.txtPenyetor.Size = new System.Drawing.Size(580, 22);
            this.txtPenyetor.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(64, 258);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 16);
            this.label7.TabIndex = 16;
            this.label7.Text = "Penyetor";
            // 
            // txtJumlah
            // 
            this.txtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlah.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlah.Location = new System.Drawing.Point(600, 545);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(209, 26);
            this.txtJumlah.TabIndex = 17;
            this.txtJumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(511, 549);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 20);
            this.label8.TabIndex = 18;
            this.label8.Text = "Jumlah";
            // 
            // cmdCari
            // 
            this.cmdCari.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCari.Image = global::KUAPPAS.Properties.Resources.search;
            this.cmdCari.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdCari.Location = new System.Drawing.Point(192, 301);
            this.cmdCari.Name = "cmdCari";
            this.cmdCari.Size = new System.Drawing.Size(313, 23);
            this.cmdCari.TabIndex = 20;
            this.cmdCari.Text = "Cari KodeRekening/Tidak di Anggarkan";
            this.cmdCari.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCari.UseVisualStyleBackColor = true;
            this.cmdCari.Click += new System.EventHandler(this.cmdCari_Click);
            // 
            // txtIDRekening
            // 
            this.txtIDRekening.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIDRekening.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIDRekening.Location = new System.Drawing.Point(194, 325);
            this.txtIDRekening.Name = "txtIDRekening";
            this.txtIDRekening.Size = new System.Drawing.Size(155, 22);
            this.txtIDRekening.TabIndex = 21;
            this.txtIDRekening.Visible = false;
            // 
            // txtNamaRekening
            // 
            this.txtNamaRekening.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNamaRekening.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNamaRekening.Location = new System.Drawing.Point(348, 325);
            this.txtNamaRekening.Name = "txtNamaRekening";
            this.txtNamaRekening.Size = new System.Drawing.Size(425, 22);
            this.txtNamaRekening.TabIndex = 22;
            this.txtNamaRekening.Visible = false;
            // 
            // txtJumlahPerRekening
            // 
            this.txtJumlahPerRekening.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlahPerRekening.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlahPerRekening.Location = new System.Drawing.Point(194, 347);
            this.txtJumlahPerRekening.Name = "txtJumlahPerRekening";
            this.txtJumlahPerRekening.Size = new System.Drawing.Size(155, 22);
            this.txtJumlahPerRekening.TabIndex = 23;
            this.txtJumlahPerRekening.Visible = false;
            // 
            // cmdAddToList
            // 
            this.cmdAddToList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.cmdAddToList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdAddToList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdAddToList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAddToList.Image = global::KUAPPAS.Properties.Resources.arrow_down;
            this.cmdAddToList.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdAddToList.Location = new System.Drawing.Point(193, 369);
            this.cmdAddToList.Name = "cmdAddToList";
            this.cmdAddToList.Size = new System.Drawing.Size(178, 29);
            this.cmdAddToList.TabIndex = 24;
            this.cmdAddToList.Text = "Tambahkan ke daftar";
            this.cmdAddToList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdAddToList.UseVisualStyleBackColor = false;
            this.cmdAddToList.Click += new System.EventHandler(this.cmdAddToList_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(64, 284);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(128, 16);
            this.label9.TabIndex = 25;
            this.label9.Text = "Cari Kode Rekening";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(65, 324);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 16);
            this.label10.TabIndex = 26;
            this.label10.Text = "Kode Rekening";
            this.label10.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(65, 349);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 16);
            this.label11.TabIndex = 27;
            this.label11.Text = "Jumlah";
            this.label11.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(64, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(30, 13);
            this.label12.TabIndex = 30;
            this.label12.Text = "OPD";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Kode Rekening";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn2.HeaderText = "Nama Rekening";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 250;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn3.HeaderText = "Jumlah Ketetapan";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Visible = false;
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn4.HeaderText = "Jumlah Diterima";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // Tanggal
            // 
            this.Tanggal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Tanggal.Location = new System.Drawing.Point(193, 139);
            this.Tanggal.Name = "Tanggal";
            this.Tanggal.Size = new System.Drawing.Size(580, 21);
            this.Tanggal.TabIndex = 28;
            this.Tanggal.Tanggal = new System.DateTime(2020, 5, 2, 0, 0, 0, 0);
            this.Tanggal.OnChanged += new KUAPPAS.ctrlTanggal.ValueChangedEventHandler(this.Tanggal_OnChanged);
            // 
            // ctrlComboAnggaran1
            // 
            this.ctrlComboAnggaran1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlComboAnggaran1.Location = new System.Drawing.Point(193, 280);
            this.ctrlComboAnggaran1.Name = "ctrlComboAnggaran1";
            this.ctrlComboAnggaran1.Size = new System.Drawing.Size(579, 21);
            this.ctrlComboAnggaran1.TabIndex = 19;
            this.ctrlComboAnggaran1.OnChanged += new KUAPPAS.Bendahara.ctrlPilihanRekeningAnggaran.ValueChangedEventHandler(this.ctrlComboAnggaran1_OnChanged);
            this.ctrlComboAnggaran1.Load += new System.EventHandler(this.ctrlComboAnggaran1_Load);
            // 
            // ctrlVia1
            // 
            this.ctrlVia1.Bank = 0;
            this.ctrlVia1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlVia1.Location = new System.Drawing.Point(193, 113);
            this.ctrlVia1.Name = "ctrlVia1";
            this.ctrlVia1.Size = new System.Drawing.Size(580, 23);
            this.ctrlVia1.TabIndex = 6;
            // 
            // ctrlSKPDSKPD1
            // 
            this.ctrlSKPDSKPD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSKPDSKPD1.Location = new System.Drawing.Point(192, 163);
            this.ctrlSKPDSKPD1.Name = "ctrlSKPDSKPD1";
            this.ctrlSKPDSKPD1.Size = new System.Drawing.Size(580, 24);
            this.ctrlSKPDSKPD1.TabIndex = 2;
            // 
            // ctrlNavigation1
            // 
            this.ctrlNavigation1.BackColor = System.Drawing.Color.DimGray;
            this.ctrlNavigation1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlNavigation1.Location = new System.Drawing.Point(0, 0);
            this.ctrlNavigation1.Name = "ctrlNavigation1";
            this.ctrlNavigation1.Size = new System.Drawing.Size(809, 35);
            this.ctrlNavigation1.TabIndex = 1;
            this.ctrlNavigation1.OnAdd += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnAdd);
            this.ctrlNavigation1.OnSave += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnSave);
            this.ctrlNavigation1.OnDelete += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnDelete);
            this.ctrlNavigation1.Load += new System.EventHandler(this.ctrlNavigation1_Load);
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(192, 37);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(581, 44);
            this.ctrlDinas1.TabIndex = 0;
            this.ctrlDinas1.UK = 0;
            this.ctrlDinas1.OnChanged += new KUAPPAS.ctrlDinas.ValueChangedEventHandler(this.ctrlDinas1_OnChanged);
            this.ctrlDinas1.Load += new System.EventHandler(this.ctrlDinas1_Load);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.Red;
            this.textBox1.Location = new System.Drawing.Point(13, 549);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(224, 31);
            this.textBox1.TabIndex = 31;
            this.textBox1.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.Blue;
            this.textBox2.Location = new System.Drawing.Point(294, 548);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(211, 31);
            this.textBox2.TabIndex = 32;
            this.textBox2.Visible = false;
            // 
            // chkTransferLangsungKasda
            // 
            this.chkTransferLangsungKasda.AutoSize = true;
            this.chkTransferLangsungKasda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTransferLangsungKasda.Location = new System.Drawing.Point(193, 87);
            this.chkTransferLangsungKasda.Name = "chkTransferLangsungKasda";
            this.chkTransferLangsungKasda.Size = new System.Drawing.Size(410, 20);
            this.chkTransferLangsungKasda.TabIndex = 33;
            this.chkTransferLangsungKasda.Text = "Penerimaan Di transfer Langsung Ke Rekening KASDA. ";
            this.chkTransferLangsungKasda.UseVisualStyleBackColor = true;
            this.chkTransferLangsungKasda.CheckedChanged += new System.EventHandler(this.chkTransferLangsungKasda_CheckedChanged);
            // 
            // ctrlFooter1
            // 
            this.ctrlFooter1.BackColor = System.Drawing.Color.Silver;
            this.ctrlFooter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctrlFooter1.Location = new System.Drawing.Point(0, 590);
            this.ctrlFooter1.Name = "ctrlFooter1";
            this.ctrlFooter1.Size = new System.Drawing.Size(809, 21);
            this.ctrlFooter1.TabIndex = 34;
            // 
            // IDRekening
            // 
            this.IDRekening.HeaderText = "Kode Rekening";
            this.IDRekening.Name = "IDRekening";
            // 
            // Nama
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Nama.DefaultCellStyle = dataGridViewCellStyle1;
            this.Nama.HeaderText = "Nama Rekening";
            this.Nama.Name = "Nama";
            this.Nama.ReadOnly = true;
            this.Nama.Width = 350;
            // 
            // JumlahKetetapan
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.JumlahKetetapan.DefaultCellStyle = dataGridViewCellStyle2;
            this.JumlahKetetapan.HeaderText = "Jumlah Ketetapan";
            this.JumlahKetetapan.Name = "JumlahKetetapan";
            this.JumlahKetetapan.ReadOnly = true;
            this.JumlahKetetapan.Visible = false;
            this.JumlahKetetapan.Width = 150;
            // 
            // JumlahTerima
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.JumlahTerima.DefaultCellStyle = dataGridViewCellStyle3;
            this.JumlahTerima.HeaderText = "Jumlah Diterima";
            this.JumlahTerima.Name = "JumlahTerima";
            this.JumlahTerima.Width = 150;
            // 
            // ID
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ID.DefaultCellStyle = dataGridViewCellStyle4;
            this.ID.HeaderText = "koderekening";
            this.ID.Name = "ID";
            // 
            // frmPenerimaan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 611);
            this.Controls.Add(this.ctrlFooter1);
            this.Controls.Add(this.chkTransferLangsungKasda);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.Tanggal);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmdAddToList);
            this.Controls.Add(this.txtJumlahPerRekening);
            this.Controls.Add(this.txtNamaRekening);
            this.Controls.Add(this.txtIDRekening);
            this.Controls.Add(this.cmdCari);
            this.Controls.Add(this.ctrlComboAnggaran1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtPenyetor);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridPenerimaan);
            this.Controls.Add(this.ctrlVia1);
            this.Controls.Add(this.txtNoBukti);
            this.Controls.Add(this.txtKeterangan);
            this.Controls.Add(this.ctrlSKPDSKPD1);
            this.Controls.Add(this.ctrlNavigation1);
            this.Controls.Add(this.ctrlDinas1);
            this.Name = "frmPenerimaan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Penerimaan ";
            this.Load += new System.EventHandler(this.frmSTS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridPenerimaan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlDinas ctrlDinas1;
        private ctrlNavigation ctrlNavigation1;
        private ctrlSKPDSKPD ctrlSKPDSKPD1;
        private System.Windows.Forms.TextBox txtKeterangan;
        private System.Windows.Forms.TextBox txtNoBukti;
        private ctrlVia ctrlVia1;
        private System.Windows.Forms.DataGridView gridPenerimaan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPenyetor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private ctrlPilihanRekeningAnggaran ctrlComboAnggaran1;
        private System.Windows.Forms.Button cmdCari;
        private System.Windows.Forms.TextBox txtIDRekening;
        private System.Windows.Forms.TextBox txtNamaRekening;
        private System.Windows.Forms.TextBox txtJumlahPerRekening;
        private System.Windows.Forms.Button cmdAddToList;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private ctrlTanggal Tanggal;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckBox chkTransferLangsungKasda;
        private ctrlFooter ctrlFooter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDRekening;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn JumlahKetetapan;
        private System.Windows.Forms.DataGridViewTextBoxColumn JumlahTerima;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
    }
}