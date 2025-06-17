namespace KUAPPAS.KasDaerah
{
    partial class frmCatatTanggalCair
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
            this.cmdBKU = new System.Windows.Forms.Button();
            this.txtJenis = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ctrlTanggal3 = new KUAPPAS.ctrlTanggal();
            this.cmdpdateStatus = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.chkCair = new System.Windows.Forms.CheckBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNoSP2D = new System.Windows.Forms.TextBox();
            this.ctrlTanggal2 = new KUAPPAS.ctrlTanggal();
            this.ctrlTanggal1 = new KUAPPAS.ctrlTanggal();
            this.cmdTampilkan = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gridSP2D = new System.Windows.Forms.DataGridView();
            this.NoUrut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pilih = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Nomor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoSP2D = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Keterangan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jumlah = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JumlahPotongan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TanggalTerbit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TanggalCair = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JenisCair = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.lblTanggalCair = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTanggalTerbit = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblJumlah = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNoSP2D = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.txtDikirmKeSilakan = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSP2D)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdBKU
            // 
            this.cmdBKU.Location = new System.Drawing.Point(907, 51);
            this.cmdBKU.Name = "cmdBKU";
            this.cmdBKU.Size = new System.Drawing.Size(89, 42);
            this.cmdBKU.TabIndex = 46;
            this.cmdBKU.Text = "BKU";
            this.cmdBKU.UseVisualStyleBackColor = true;
            this.cmdBKU.Click += new System.EventHandler(this.cmdBKU_Click);
            // 
            // txtJenis
            // 
            this.txtJenis.Location = new System.Drawing.Point(75, 73);
            this.txtJenis.Name = "txtJenis";
            this.txtJenis.Size = new System.Drawing.Size(30, 20);
            this.txtJenis.TabIndex = 45;
            this.txtJenis.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(463, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 16);
            this.label8.TabIndex = 44;
            this.label8.Text = "s/d";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(16, 395);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 20);
            this.label7.TabIndex = 43;
            this.label7.Text = "Tanggal Cair";
            // 
            // ctrlTanggal3
            // 
            this.ctrlTanggal3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ctrlTanggal3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlTanggal3.Location = new System.Drawing.Point(122, 396);
            this.ctrlTanggal3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlTanggal3.Name = "ctrlTanggal3";
            this.ctrlTanggal3.Size = new System.Drawing.Size(111, 28);
            this.ctrlTanggal3.TabIndex = 42;
            this.ctrlTanggal3.Tanggal = new System.DateTime(2023, 12, 9, 0, 0, 0, 0);
            // 
            // cmdpdateStatus
            // 
            this.cmdpdateStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdpdateStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdpdateStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdpdateStatus.Location = new System.Drawing.Point(240, 392);
            this.cmdpdateStatus.Name = "cmdpdateStatus";
            this.cmdpdateStatus.Size = new System.Drawing.Size(164, 33);
            this.cmdpdateStatus.TabIndex = 41;
            this.cmdpdateStatus.Text = "Set Tanggal Cair";
            this.cmdpdateStatus.UseVisualStyleBackColor = true;
            this.cmdpdateStatus.Click += new System.EventHandler(this.cmdpdateStatus_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(608, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 16);
            this.label6.TabIndex = 40;
            this.label6.Text = "Status";
            this.label6.Visible = false;
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(659, 48);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(71, 21);
            this.cmbStatus.TabIndex = 39;
            this.cmbStatus.Visible = false;
            // 
            // chkCair
            // 
            this.chkCair.AutoSize = true;
            this.chkCair.Location = new System.Drawing.Point(90, 48);
            this.chkCair.Name = "chkCair";
            this.chkCair.Size = new System.Drawing.Size(81, 17);
            this.chkCair.TabIndex = 38;
            this.chkCair.Text = "Sudah  Cair";
            this.chkCair.UseVisualStyleBackColor = true;
            this.chkCair.CheckedChanged += new System.EventHandler(this.chkCair_CheckedChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(201, 47);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(135, 16);
            this.lblStatus.TabIndex = 37;
            this.lblStatus.Text = "Tanggal Terbit SP2D";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(237, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 16);
            this.label4.TabIndex = 36;
            this.label4.Text = "Nomor SP2D";
            // 
            // txtNoSP2D
            // 
            this.txtNoSP2D.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoSP2D.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoSP2D.Location = new System.Drawing.Point(345, 70);
            this.txtNoSP2D.Name = "txtNoSP2D";
            this.txtNoSP2D.Size = new System.Drawing.Size(256, 26);
            this.txtNoSP2D.TabIndex = 35;
            // 
            // ctrlTanggal2
            // 
            this.ctrlTanggal2.Location = new System.Drawing.Point(484, 46);
            this.ctrlTanggal2.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlTanggal2.Name = "ctrlTanggal2";
            this.ctrlTanggal2.Size = new System.Drawing.Size(117, 23);
            this.ctrlTanggal2.TabIndex = 34;
            this.ctrlTanggal2.Tanggal = new System.DateTime(2023, 9, 5, 0, 0, 0, 0);
            // 
            // ctrlTanggal1
            // 
            this.ctrlTanggal1.Location = new System.Drawing.Point(343, 46);
            this.ctrlTanggal1.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlTanggal1.Name = "ctrlTanggal1";
            this.ctrlTanggal1.Size = new System.Drawing.Size(121, 23);
            this.ctrlTanggal1.TabIndex = 33;
            this.ctrlTanggal1.Tanggal = new System.DateTime(2023, 9, 5, 0, 0, 0, 0);
            // 
            // cmdTampilkan
            // 
            this.cmdTampilkan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.cmdTampilkan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdTampilkan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTampilkan.Image = global::KUAPPAS.Properties.Resources.search;
            this.cmdTampilkan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdTampilkan.Location = new System.Drawing.Point(749, 46);
            this.cmdTampilkan.Margin = new System.Windows.Forms.Padding(4);
            this.cmdTampilkan.Name = "cmdTampilkan";
            this.cmdTampilkan.Size = new System.Drawing.Size(122, 44);
            this.cmdTampilkan.TabIndex = 32;
            this.cmdTampilkan.Text = "Tampilkan";
            this.cmdTampilkan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdTampilkan.UseVisualStyleBackColor = false;
            this.cmdTampilkan.Click += new System.EventHandler(this.cmdTampilkan_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 99);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gridSP2D);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1054, 280);
            this.splitContainer1.SplitterDistance = 807;
            this.splitContainer1.TabIndex = 1;
            // 
            // gridSP2D
            // 
            this.gridSP2D.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridSP2D.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSP2D.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NoUrut,
            this.Pilih,
            this.Nomor,
            this.NoSP2D,
            this.OPD,
            this.Keterangan,
            this.Jumlah,
            this.JumlahPotongan,
            this.TanggalTerbit,
            this.TanggalCair,
            this.JenisCair,
            this.Status});
            this.gridSP2D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSP2D.Location = new System.Drawing.Point(0, 0);
            this.gridSP2D.Name = "gridSP2D";
            this.gridSP2D.Size = new System.Drawing.Size(807, 280);
            this.gridSP2D.TabIndex = 0;
            this.gridSP2D.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSP2D_CellContentClick);
            // 
            // NoUrut
            // 
            this.NoUrut.HeaderText = "NoUrut";
            this.NoUrut.Name = "NoUrut";
            this.NoUrut.ReadOnly = true;
            this.NoUrut.Visible = false;
            // 
            // Pilih
            // 
            this.Pilih.HeaderText = "Pilih";
            this.Pilih.Name = "Pilih";
            this.Pilih.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Pilih.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Pilih.Width = 50;
            // 
            // Nomor
            // 
            this.Nomor.HeaderText = "Nomor";
            this.Nomor.Name = "Nomor";
            this.Nomor.Width = 80;
            // 
            // NoSP2D
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NoSP2D.DefaultCellStyle = dataGridViewCellStyle1;
            this.NoSP2D.HeaderText = "Nomor SP2D";
            this.NoSP2D.Name = "NoSP2D";
            this.NoSP2D.ReadOnly = true;
            this.NoSP2D.Width = 150;
            // 
            // OPD
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.OPD.DefaultCellStyle = dataGridViewCellStyle2;
            this.OPD.HeaderText = "Penerima & OPD";
            this.OPD.Name = "OPD";
            this.OPD.ReadOnly = true;
            this.OPD.Width = 150;
            // 
            // Keterangan
            // 
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Keterangan.DefaultCellStyle = dataGridViewCellStyle3;
            this.Keterangan.HeaderText = "Keterangan";
            this.Keterangan.Name = "Keterangan";
            this.Keterangan.ReadOnly = true;
            this.Keterangan.Width = 350;
            // 
            // Jumlah
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Jumlah.DefaultCellStyle = dataGridViewCellStyle4;
            this.Jumlah.HeaderText = "Jumlah Bersih";
            this.Jumlah.Name = "Jumlah";
            this.Jumlah.ReadOnly = true;
            this.Jumlah.Width = 120;
            // 
            // JumlahPotongan
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.JumlahPotongan.DefaultCellStyle = dataGridViewCellStyle5;
            this.JumlahPotongan.HeaderText = "Jumlah Potongan";
            this.JumlahPotongan.Name = "JumlahPotongan";
            this.JumlahPotongan.ReadOnly = true;
            // 
            // TanggalTerbit
            // 
            this.TanggalTerbit.HeaderText = "Tanggal Terbit";
            this.TanggalTerbit.Name = "TanggalTerbit";
            this.TanggalTerbit.ReadOnly = true;
            // 
            // TanggalCair
            // 
            this.TanggalCair.HeaderText = "Tanggal Cair";
            this.TanggalCair.Name = "TanggalCair";
            this.TanggalCair.ReadOnly = true;
            // 
            // JenisCair
            // 
            this.JenisCair.HeaderText = "Jenis Pencairan";
            this.JenisCair.Name = "JenisCair";
            this.JenisCair.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 40;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtKeterangan);
            this.panel1.Controls.Add(this.lblTanggalCair);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblTanggalTerbit);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblJumlah);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblNoSP2D);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(243, 280);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKeterangan.BackColor = System.Drawing.SystemColors.Control;
            this.txtKeterangan.Location = new System.Drawing.Point(12, 177);
            this.txtKeterangan.Multiline = true;
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(219, 52);
            this.txtKeterangan.TabIndex = 8;
            // 
            // lblTanggalCair
            // 
            this.lblTanggalCair.AutoSize = true;
            this.lblTanggalCair.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTanggalCair.Location = new System.Drawing.Point(8, 150);
            this.lblTanggalCair.Name = "lblTanggalCair";
            this.lblTanggalCair.Size = new System.Drawing.Size(88, 24);
            this.lblTanggalCair.TabIndex = 7;
            this.lblTanggalCair.Text = "12/12/12";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Tanggal Cair";
            // 
            // lblTanggalTerbit
            // 
            this.lblTanggalTerbit.AutoSize = true;
            this.lblTanggalTerbit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTanggalTerbit.Location = new System.Drawing.Point(7, 101);
            this.lblTanggalTerbit.Name = "lblTanggalTerbit";
            this.lblTanggalTerbit.Size = new System.Drawing.Size(88, 24);
            this.lblTanggalTerbit.TabIndex = 5;
            this.lblTanggalTerbit.Text = "12/12/12";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tanggal Terbit";
            // 
            // lblJumlah
            // 
            this.lblJumlah.AutoSize = true;
            this.lblJumlah.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJumlah.Location = new System.Drawing.Point(8, 62);
            this.lblJumlah.Name = "lblJumlah";
            this.lblJumlah.Size = new System.Drawing.Size(43, 24);
            this.lblJumlah.TabIndex = 3;
            this.lblJumlah.Text = "000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Jumlah";
            // 
            // lblNoSP2D
            // 
            this.lblNoSP2D.AutoSize = true;
            this.lblNoSP2D.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoSP2D.Location = new System.Drawing.Point(8, 17);
            this.lblNoSP2D.Name = "lblNoSP2D";
            this.lblNoSP2D.Size = new System.Drawing.Size(60, 24);
            this.lblNoSP2D.TabIndex = 1;
            this.lblNoSP2D.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nomor SP2D";
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1066, 41);
            this.ctrlHeader1.TabIndex = 0;
            // 
            // txtDikirmKeSilakan
            // 
            this.txtDikirmKeSilakan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDikirmKeSilakan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDikirmKeSilakan.Location = new System.Drawing.Point(611, 385);
            this.txtDikirmKeSilakan.Multiline = true;
            this.txtDikirmKeSilakan.Name = "txtDikirmKeSilakan";
            this.txtDikirmKeSilakan.Size = new System.Drawing.Size(455, 51);
            this.txtDikirmKeSilakan.TabIndex = 47;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(455, 399);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(146, 16);
            this.label9.TabIndex = 48;
            this.label9.Text = "Status dikim ke Silakan";
            // 
            // frmCatatTanggalCair
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 437);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtDikirmKeSilakan);
            this.Controls.Add(this.cmdBKU);
            this.Controls.Add(this.txtJenis);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ctrlTanggal3);
            this.Controls.Add(this.cmdpdateStatus);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.chkCair);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNoSP2D);
            this.Controls.Add(this.ctrlTanggal2);
            this.Controls.Add(this.ctrlTanggal1);
            this.Controls.Add(this.cmdTampilkan);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmCatatTanggalCair";
            this.Text = "Pencatatan Tanggal Cair";
            this.Load += new System.EventHandler(this.frmCatatTanggalCair_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSP2D)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView gridSP2D;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTanggalCair;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTanggalTerbit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblJumlah;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNoSP2D;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKeterangan;
        private System.Windows.Forms.CheckBox chkCair;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNoSP2D;
        private ctrlTanggal ctrlTanggal2;
        private ctrlTanggal ctrlTanggal1;
        private System.Windows.Forms.Button cmdTampilkan;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button cmdpdateStatus;
        private ctrlTanggal ctrlTanggal3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtJenis;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoUrut;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Pilih;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nomor;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoSP2D;
        private System.Windows.Forms.DataGridViewTextBoxColumn OPD;
        private System.Windows.Forms.DataGridViewTextBoxColumn Keterangan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jumlah;
        private System.Windows.Forms.DataGridViewTextBoxColumn JumlahPotongan;
        private System.Windows.Forms.DataGridViewTextBoxColumn TanggalTerbit;
        private System.Windows.Forms.DataGridViewTextBoxColumn TanggalCair;
        private System.Windows.Forms.DataGridViewTextBoxColumn JenisCair;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.Button cmdBKU;
        private System.Windows.Forms.TextBox txtDikirmKeSilakan;
        private System.Windows.Forms.Label label9;
    }
}