namespace KUAPPAS
{
    partial class frmKUAPPASTerintegrasi
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.cmdCetak = new System.Windows.Forms.Button();
            this.cmdSimpan = new System.Windows.Forms.Button();
            this.txtJumlahRKPD = new System.Windows.Forms.TextBox();
            this.txtJumlahKUA = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCOnnection = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chkDenganRKPD = new System.Windows.Forms.CheckBox();
            this.txtPaguSKPD = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdCekKUA = new System.Windows.Forms.Button();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.chkGabungan = new System.Windows.Forms.CheckBox();
            this.txtJumlahKUAP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdSmaakanDenganKUAAWAL = new System.Windows.Forms.Button();
            this.cmdCetakSemua = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.gridRKPD = new KUAPPAS.TreeGridView();
            this.TNama = new KUAPPAS.TreeGridColumn();
            this.TRKPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRKPDP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TKUA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TKUAP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIDUrusan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIDProgram = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIDKegiatan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDUrusanMaster = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDProgramMaster = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IKEgiatanMaster = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaAsli = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idSUb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idmasterSUb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Keluaran = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Outcome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prioritas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.target = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rpjmd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tmin1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tplus1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRKPD)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 571);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1080, 22);
            this.statusStrip1.TabIndex = 19;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // cmdCetak
            // 
            this.cmdCetak.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.cmdCetak.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdCetak.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.cmdCetak.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCetak.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCetak.Image = global::KUAPPAS.Properties.Resources.action_print;
            this.cmdCetak.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCetak.Location = new System.Drawing.Point(206, 44);
            this.cmdCetak.Name = "cmdCetak";
            this.cmdCetak.Size = new System.Drawing.Size(92, 47);
            this.cmdCetak.TabIndex = 20;
            this.cmdCetak.Text = "Cetak";
            this.cmdCetak.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdCetak.UseVisualStyleBackColor = false;
            this.cmdCetak.Click += new System.EventHandler(this.cmdCetak_Click);
            // 
            // cmdSimpan
            // 
            this.cmdSimpan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmdSimpan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdSimpan.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.cmdSimpan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSimpan.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSimpan.Image = global::KUAPPAS.Properties.Resources.action_save;
            this.cmdSimpan.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdSimpan.Location = new System.Drawing.Point(118, 41);
            this.cmdSimpan.Name = "cmdSimpan";
            this.cmdSimpan.Size = new System.Drawing.Size(86, 50);
            this.cmdSimpan.TabIndex = 11;
            this.cmdSimpan.Text = "Simpan";
            this.cmdSimpan.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdSimpan.UseVisualStyleBackColor = false;
            this.cmdSimpan.Click += new System.EventHandler(this.cmdSimpan_Click);
            // 
            // txtJumlahRKPD
            // 
            this.txtJumlahRKPD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJumlahRKPD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlahRKPD.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlahRKPD.Location = new System.Drawing.Point(869, 8);
            this.txtJumlahRKPD.Name = "txtJumlahRKPD";
            this.txtJumlahRKPD.Size = new System.Drawing.Size(199, 24);
            this.txtJumlahRKPD.TabIndex = 21;
            this.txtJumlahRKPD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtJumlahKUA
            // 
            this.txtJumlahKUA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJumlahKUA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlahKUA.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlahKUA.Location = new System.Drawing.Point(870, 61);
            this.txtJumlahKUA.Name = "txtJumlahKUA";
            this.txtJumlahKUA.Size = new System.Drawing.Size(198, 24);
            this.txtJumlahKUA.TabIndex = 22;
            this.txtJumlahKUA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(751, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 18);
            this.label1.TabIndex = 23;
            this.label1.Text = "Jumlah RKPD";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(747, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 24;
            this.label2.Text = "Jumlah K U A";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblCOnnection);
            this.panel1.Location = new System.Drawing.Point(428, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(197, 41);
            this.panel1.TabIndex = 17;
            // 
            // lblCOnnection
            // 
            this.lblCOnnection.AutoSize = true;
            this.lblCOnnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCOnnection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblCOnnection.Location = new System.Drawing.Point(3, 9);
            this.lblCOnnection.Name = "lblCOnnection";
            this.lblCOnnection.Size = new System.Drawing.Size(19, 20);
            this.lblCOnnection.TabIndex = 0;
            this.lblCOnnection.Text = "--";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chkDenganRKPD
            // 
            this.chkDenganRKPD.AutoSize = true;
            this.chkDenganRKPD.Location = new System.Drawing.Point(669, 0);
            this.chkDenganRKPD.Name = "chkDenganRKPD";
            this.chkDenganRKPD.Size = new System.Drawing.Size(97, 17);
            this.chkDenganRKPD.TabIndex = 25;
            this.chkDenganRKPD.Text = "Dengan RKPD";
            this.chkDenganRKPD.UseVisualStyleBackColor = true;
            this.chkDenganRKPD.Visible = false;
            // 
            // txtPaguSKPD
            // 
            this.txtPaguSKPD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPaguSKPD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPaguSKPD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaguSKPD.Location = new System.Drawing.Point(869, 33);
            this.txtPaguSKPD.Name = "txtPaguSKPD";
            this.txtPaguSKPD.Size = new System.Drawing.Size(201, 26);
            this.txtPaguSKPD.TabIndex = 26;
            this.txtPaguSKPD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label3.Location = new System.Drawing.Point(751, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 18);
            this.label3.TabIndex = 27;
            this.label3.Text = "Pagu SKPD";
            // 
            // cmdCekKUA
            // 
            this.cmdCekKUA.Location = new System.Drawing.Point(631, 42);
            this.cmdCekKUA.Name = "cmdCekKUA";
            this.cmdCekKUA.Size = new System.Drawing.Size(75, 46);
            this.cmdCekKUA.TabIndex = 28;
            this.cmdCekKUA.Text = "Cek Kua";
            this.cmdCekKUA.UseVisualStyleBackColor = true;
            // 
            // cmdLoad
            // 
            this.cmdLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.cmdLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLoad.Location = new System.Drawing.Point(12, 41);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(100, 50);
            this.cmdLoad.TabIndex = 29;
            this.cmdLoad.Text = "Panggil Data";
            this.cmdLoad.UseVisualStyleBackColor = false;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // chkGabungan
            // 
            this.chkGabungan.AutoSize = true;
            this.chkGabungan.Location = new System.Drawing.Point(669, 23);
            this.chkGabungan.Name = "chkGabungan";
            this.chkGabungan.Size = new System.Drawing.Size(76, 17);
            this.chkGabungan.TabIndex = 30;
            this.chkGabungan.Text = "Gabungan";
            this.chkGabungan.UseVisualStyleBackColor = true;
            // 
            // txtJumlahKUAP
            // 
            this.txtJumlahKUAP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlahKUAP.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txtJumlahKUAP.Location = new System.Drawing.Point(874, 86);
            this.txtJumlahKUAP.Name = "txtJumlahKUAP";
            this.txtJumlahKUAP.Size = new System.Drawing.Size(196, 24);
            this.txtJumlahKUAP.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(751, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 18);
            this.label4.TabIndex = 32;
            this.label4.Text = "Jumlah K U A P";
            // 
            // cmdSmaakanDenganKUAAWAL
            // 
            this.cmdSmaakanDenganKUAAWAL.Location = new System.Drawing.Point(712, 44);
            this.cmdSmaakanDenganKUAAWAL.Name = "cmdSmaakanDenganKUAAWAL";
            this.cmdSmaakanDenganKUAAWAL.Size = new System.Drawing.Size(120, 41);
            this.cmdSmaakanDenganKUAAWAL.TabIndex = 33;
            this.cmdSmaakanDenganKUAAWAL.Text = "Samakan Perubahn dengan Murni";
            this.cmdSmaakanDenganKUAAWAL.UseVisualStyleBackColor = true;
            this.cmdSmaakanDenganKUAAWAL.Visible = false;
            this.cmdSmaakanDenganKUAAWAL.Click += new System.EventHandler(this.cmdSmaakanDenganKUAAWAL_Click);
            // 
            // cmdCetakSemua
            // 
            this.cmdCetakSemua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cmdCetakSemua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCetakSemua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCetakSemua.Location = new System.Drawing.Point(304, 43);
            this.cmdCetakSemua.Name = "cmdCetakSemua";
            this.cmdCetakSemua.Size = new System.Drawing.Size(118, 45);
            this.cmdCetakSemua.TabIndex = 34;
            this.cmdCetakSemua.Text = "Cetak Semua";
            this.cmdCetakSemua.UseVisualStyleBackColor = false;
            this.cmdCetakSemua.Click += new System.EventHandler(this.cmdCetakSemua_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Gray;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.LawnGreen;
            this.label5.Location = new System.Drawing.Point(829, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 37);
            this.label5.TabIndex = 35;
            this.label5.Text = "label5";
            // 
            // gridRKPD
            // 
            this.gridRKPD.AllowUserToAddRows = false;
            this.gridRKPD.AllowUserToDeleteRows = false;
            this.gridRKPD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridRKPD.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridRKPD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridRKPD.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TNama,
            this.TRKPD,
            this.TRKPDP,
            this.TKUA,
            this.TKUAP,
            this.TIDUrusan,
            this.TIDProgram,
            this.TIDKegiatan,
            this.IDUrusanMaster,
            this.IDProgramMaster,
            this.IKEgiatanMaster,
            this.NamaAsli,
            this.idSUb,
            this.idmasterSUb,
            this.Keluaran,
            this.Outcome,
            this.prioritas,
            this.target,
            this.rpjmd,
            this.tmin1,
            this.tplus1});
            this.gridRKPD.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridRKPD.ImageList = null;
            this.gridRKPD.Location = new System.Drawing.Point(4, 116);
            this.gridRKPD.Name = "gridRKPD";
            this.gridRKPD.Size = new System.Drawing.Size(1076, 452);
            this.gridRKPD.TabIndex = 18;
            this.gridRKPD.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.gridRKPD_CellBeginEdit);
            this.gridRKPD.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridRKPD_CellContentClick);
            this.gridRKPD.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridRKPD_CellEndEdit);
            this.gridRKPD.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridRKPD_CellEnter);
            // 
            // TNama
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TNama.DefaultCellStyle = dataGridViewCellStyle1;
            this.TNama.DefaultNodeImage = null;
            this.TNama.HeaderText = "Nama";
            this.TNama.Name = "TNama";
            this.TNama.ReadOnly = true;
            this.TNama.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TNama.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TNama.Width = 600;
            // 
            // TRKPD
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TRKPD.DefaultCellStyle = dataGridViewCellStyle2;
            this.TRKPD.HeaderText = "Pagu Program";
            this.TRKPD.Name = "TRKPD";
            this.TRKPD.ReadOnly = true;
            this.TRKPD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TRKPD.Width = 150;
            // 
            // TRKPDP
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TRKPDP.DefaultCellStyle = dataGridViewCellStyle3;
            this.TRKPDP.HeaderText = "RKPD";
            this.TRKPDP.Name = "TRKPDP";
            this.TRKPDP.ReadOnly = true;
            this.TRKPDP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TKUA
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TKUA.DefaultCellStyle = dataGridViewCellStyle4;
            this.TKUA.HeaderText = "K U A";
            this.TKUA.Name = "TKUA";
            this.TKUA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TKUA.Width = 200;
            // 
            // TKUAP
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TKUAP.DefaultCellStyle = dataGridViewCellStyle5;
            this.TKUAP.HeaderText = "K U A P";
            this.TKUAP.Name = "TKUAP";
            this.TKUAP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TKUAP.Width = 200;
            // 
            // TIDUrusan
            // 
            this.TIDUrusan.HeaderText = "TIDurusan";
            this.TIDUrusan.Name = "TIDUrusan";
            this.TIDUrusan.ReadOnly = true;
            this.TIDUrusan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TIDUrusan.Visible = false;
            this.TIDUrusan.Width = 50;
            // 
            // TIDProgram
            // 
            this.TIDProgram.HeaderText = "IDProgram";
            this.TIDProgram.Name = "TIDProgram";
            this.TIDProgram.ReadOnly = true;
            this.TIDProgram.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TIDProgram.Visible = false;
            // 
            // TIDKegiatan
            // 
            this.TIDKegiatan.HeaderText = "IDKegiatan";
            this.TIDKegiatan.Name = "TIDKegiatan";
            this.TIDKegiatan.ReadOnly = true;
            this.TIDKegiatan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TIDKegiatan.Visible = false;
            // 
            // IDUrusanMaster
            // 
            this.IDUrusanMaster.HeaderText = "IUM";
            this.IDUrusanMaster.Name = "IDUrusanMaster";
            this.IDUrusanMaster.ReadOnly = true;
            this.IDUrusanMaster.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.IDUrusanMaster.Visible = false;
            this.IDUrusanMaster.Width = 30;
            // 
            // IDProgramMaster
            // 
            this.IDProgramMaster.HeaderText = "IPM";
            this.IDProgramMaster.Name = "IDProgramMaster";
            this.IDProgramMaster.ReadOnly = true;
            this.IDProgramMaster.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.IDProgramMaster.Visible = false;
            this.IDProgramMaster.Width = 30;
            // 
            // IKEgiatanMaster
            // 
            this.IKEgiatanMaster.HeaderText = "IKM";
            this.IKEgiatanMaster.Name = "IKEgiatanMaster";
            this.IKEgiatanMaster.ReadOnly = true;
            this.IKEgiatanMaster.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.IKEgiatanMaster.Visible = false;
            this.IKEgiatanMaster.Width = 30;
            // 
            // NamaAsli
            // 
            this.NamaAsli.HeaderText = "Nama";
            this.NamaAsli.Name = "NamaAsli";
            this.NamaAsli.ReadOnly = true;
            this.NamaAsli.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NamaAsli.Visible = false;
            // 
            // idSUb
            // 
            this.idSUb.HeaderText = "idsub";
            this.idSUb.Name = "idSUb";
            this.idSUb.ReadOnly = true;
            this.idSUb.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.idSUb.Width = 50;
            // 
            // idmasterSUb
            // 
            this.idmasterSUb.HeaderText = "idmasterSUb";
            this.idmasterSUb.Name = "idmasterSUb";
            this.idmasterSUb.ReadOnly = true;
            this.idmasterSUb.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.idmasterSUb.Width = 50;
            // 
            // Keluaran
            // 
            this.Keluaran.HeaderText = "Keluaran";
            this.Keluaran.Name = "Keluaran";
            this.Keluaran.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Outcome
            // 
            this.Outcome.HeaderText = "Outcome";
            this.Outcome.Name = "Outcome";
            this.Outcome.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // prioritas
            // 
            this.prioritas.HeaderText = "prioritas";
            this.prioritas.Name = "prioritas";
            this.prioritas.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // target
            // 
            this.target.HeaderText = "target";
            this.target.Name = "target";
            this.target.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // rpjmd
            // 
            this.rpjmd.HeaderText = "rpjmd";
            this.rpjmd.Name = "rpjmd";
            this.rpjmd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tmin1
            // 
            this.tmin1.HeaderText = "tmin1";
            this.tmin1.Name = "tmin1";
            this.tmin1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tplus1
            // 
            this.tplus1.HeaderText = "tplus1";
            this.tplus1.Name = "tplus1";
            this.tplus1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(29, 12);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(647, 22);
            this.ctrlDinas1.TabIndex = 15;
            this.ctrlDinas1.OnChanged += new KUAPPAS.ctrlDinas.ValueChangedEventHandler(this.ctrlDinas1_OnChanged);
            this.ctrlDinas1.Load += new System.EventHandler(this.ctrlDinas1_Load);
            // 
            // frmKUAPPASTerintegrasi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 593);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmdCetakSemua);
            this.Controls.Add(this.cmdSmaakanDenganKUAAWAL);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtJumlahKUAP);
            this.Controls.Add(this.chkGabungan);
            this.Controls.Add(this.cmdLoad);
            this.Controls.Add(this.cmdCekKUA);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPaguSKPD);
            this.Controls.Add(this.chkDenganRKPD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtJumlahKUA);
            this.Controls.Add(this.txtJumlahRKPD);
            this.Controls.Add(this.cmdCetak);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gridRKPD);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.cmdSimpan);
            this.Name = "frmKUAPPASTerintegrasi";
            this.Text = "KUA PPAS";
            this.Load += new System.EventHandler(this.frmKUAPPASTerintegrasi_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRKPD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlDinas ctrlDinas1;
        private System.Windows.Forms.Button cmdSimpan;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button cmdCetak;
        private TreeGridView gridRKPD;
        private System.Windows.Forms.TextBox txtJumlahRKPD;
        private System.Windows.Forms.TextBox txtJumlahKUA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblCOnnection;
        private System.Windows.Forms.CheckBox chkDenganRKPD;
        private System.Windows.Forms.TextBox txtPaguSKPD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdCekKUA;
        private System.Windows.Forms.Button cmdLoad;
        private System.Windows.Forms.CheckBox chkGabungan;
        private System.Windows.Forms.TextBox txtJumlahKUAP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdSmaakanDenganKUAAWAL;
        private TreeGridColumn TNama;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRKPD;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRKPDP;
        private System.Windows.Forms.DataGridViewTextBoxColumn TKUA;
        private System.Windows.Forms.DataGridViewTextBoxColumn TKUAP;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIDUrusan;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIDProgram;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIDKegiatan;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDUrusanMaster;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDProgramMaster;
        private System.Windows.Forms.DataGridViewTextBoxColumn IKEgiatanMaster;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaAsli;
        private System.Windows.Forms.DataGridViewTextBoxColumn idSUb;
        private System.Windows.Forms.DataGridViewTextBoxColumn idmasterSUb;
        private System.Windows.Forms.DataGridViewTextBoxColumn Keluaran;
        private System.Windows.Forms.DataGridViewTextBoxColumn Outcome;
        private System.Windows.Forms.DataGridViewTextBoxColumn prioritas;
        private System.Windows.Forms.DataGridViewTextBoxColumn target;
        private System.Windows.Forms.DataGridViewTextBoxColumn rpjmd;
        private System.Windows.Forms.DataGridViewTextBoxColumn tmin1;
        private System.Windows.Forms.DataGridViewTextBoxColumn tplus1;
        private System.Windows.Forms.Button cmdCetakSemua;
        private System.Windows.Forms.Label label5;
    }
}