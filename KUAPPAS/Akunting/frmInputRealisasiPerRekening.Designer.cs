namespace KUAPPAS
{
    partial class frmInputRealisasiPerRekening
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeProgramKegiatan1 = new KUAPPAS.TreeProgramKegiatan();
            this.label5 = new System.Windows.Forms.Label();
            this.txtJumlahPergeseran = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtJumlahMurni = new System.Windows.Forms.TextBox();
            this.lblNamaProgram = new System.Windows.Forms.Label();
            this.lbllNamaSubKegiatan = new System.Windows.Forms.Label();
            this.lblKodeKegiatan = new System.Windows.Forms.Label();
            this.lblSubKegiatan = new System.Windows.Forms.Label();
            this.lblUrusan = new System.Windows.Forms.Label();
            this.lblKegiatan = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtpaguSub = new System.Windows.Forms.TextBox();
            this.lblJumlah = new System.Windows.Forms.Label();
            this.cmdSimpan = new System.Windows.Forms.Button();
            this.lblProgram = new System.Windows.Forms.Label();
            this.gridSumberDana = new System.Windows.Forms.DataGridView();
            this.IDRekeningSumberDana = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KodeRekeningSumberDana = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaRekeningSD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PergeseranAPBDPerubahan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Realisasi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.txtidrekening = new System.Windows.Forms.TextBox();
            this.txtIDSUBKegiatan = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.ctrlJenisAnggaran1 = new KUAPPAS.ctrlJenisAnggaran();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSumberDana)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Jenis DPA";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 138);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1356, 442);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1348, 416);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Program Kegiatan";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeProgramKegiatan1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.txtJumlahPergeseran);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.txtJumlahMurni);
            this.splitContainer1.Panel2.Controls.Add(this.lblNamaProgram);
            this.splitContainer1.Panel2.Controls.Add(this.lbllNamaSubKegiatan);
            this.splitContainer1.Panel2.Controls.Add(this.lblKodeKegiatan);
            this.splitContainer1.Panel2.Controls.Add(this.lblSubKegiatan);
            this.splitContainer1.Panel2.Controls.Add(this.lblUrusan);
            this.splitContainer1.Panel2.Controls.Add(this.lblKegiatan);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.txtpaguSub);
            this.splitContainer1.Panel2.Controls.Add(this.lblJumlah);
            this.splitContainer1.Panel2.Controls.Add(this.cmdSimpan);
            this.splitContainer1.Panel2.Controls.Add(this.lblProgram);
            this.splitContainer1.Panel2.Controls.Add(this.gridSumberDana);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(1342, 410);
            this.splitContainer1.SplitterDistance = 444;
            this.splitContainer1.TabIndex = 2;
            // 
            // treeProgramKegiatan1
            // 
            this.treeProgramKegiatan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeProgramKegiatan1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeProgramKegiatan1.Location = new System.Drawing.Point(0, 0);
            this.treeProgramKegiatan1.Name = "treeProgramKegiatan1";
            this.treeProgramKegiatan1.Profile = 2;
            this.treeProgramKegiatan1.Size = new System.Drawing.Size(444, 410);
            this.treeProgramKegiatan1.TabIndex = 0;
            this.treeProgramKegiatan1.KegiatanChanged += new KUAPPAS.TreeProgramKegiatan.SelectedKegiatanEventHandler(this.treeProgramKegiatan1_KegiatanChanged);
            this.treeProgramKegiatan1.SubKegiatanChanged += new KUAPPAS.TreeProgramKegiatan.SelectedSubKegiatanEventHandler(this.treeProgramKegiatan1_SubKegiatanChanged);
            this.treeProgramKegiatan1.Load += new System.EventHandler(this.treeProgramKegiatan1_Load);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(513, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 16);
            this.label5.TabIndex = 35;
            this.label5.Text = "Jumlah Realisassi";
            // 
            // txtJumlahPergeseran
            // 
            this.txtJumlahPergeseran.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlahPergeseran.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlahPergeseran.Location = new System.Drawing.Point(647, 174);
            this.txtJumlahPergeseran.Name = "txtJumlahPergeseran";
            this.txtJumlahPergeseran.Size = new System.Drawing.Size(216, 22);
            this.txtJumlahPergeseran.TabIndex = 34;
            this.txtJumlahPergeseran.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(375, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(256, 16);
            this.label3.TabIndex = 32;
            this.label3.Text = "Jumlah Anggaran Pergeseran Perubahan";
            // 
            // txtJumlahMurni
            // 
            this.txtJumlahMurni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlahMurni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlahMurni.Location = new System.Drawing.Point(647, 150);
            this.txtJumlahMurni.Name = "txtJumlahMurni";
            this.txtJumlahMurni.Size = new System.Drawing.Size(216, 22);
            this.txtJumlahMurni.TabIndex = 31;
            this.txtJumlahMurni.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblNamaProgram
            // 
            this.lblNamaProgram.AutoSize = true;
            this.lblNamaProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNamaProgram.Location = new System.Drawing.Point(63, 33);
            this.lblNamaProgram.Name = "lblNamaProgram";
            this.lblNamaProgram.Size = new System.Drawing.Size(17, 16);
            this.lblNamaProgram.TabIndex = 29;
            this.lblNamaProgram.Text = "...";
            // 
            // lbllNamaSubKegiatan
            // 
            this.lbllNamaSubKegiatan.AutoSize = true;
            this.lbllNamaSubKegiatan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbllNamaSubKegiatan.Location = new System.Drawing.Point(67, 71);
            this.lbllNamaSubKegiatan.Name = "lbllNamaSubKegiatan";
            this.lbllNamaSubKegiatan.Size = new System.Drawing.Size(17, 16);
            this.lbllNamaSubKegiatan.TabIndex = 30;
            this.lbllNamaSubKegiatan.Text = "...";
            // 
            // lblKodeKegiatan
            // 
            this.lblKodeKegiatan.AutoSize = true;
            this.lblKodeKegiatan.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKodeKegiatan.Location = new System.Drawing.Point(17, 53);
            this.lblKodeKegiatan.Name = "lblKodeKegiatan";
            this.lblKodeKegiatan.Size = new System.Drawing.Size(40, 16);
            this.lblKodeKegiatan.TabIndex = 27;
            this.lblKodeKegiatan.Text = "Kode";
            this.lblKodeKegiatan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSubKegiatan
            // 
            this.lblSubKegiatan.AutoSize = true;
            this.lblSubKegiatan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubKegiatan.Location = new System.Drawing.Point(17, 76);
            this.lblSubKegiatan.Name = "lblSubKegiatan";
            this.lblSubKegiatan.Size = new System.Drawing.Size(44, 16);
            this.lblSubKegiatan.TabIndex = 28;
            this.lblSubKegiatan.Text = "Kode";
            // 
            // lblUrusan
            // 
            this.lblUrusan.AutoSize = true;
            this.lblUrusan.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUrusan.Location = new System.Drawing.Point(15, 11);
            this.lblUrusan.Name = "lblUrusan";
            this.lblUrusan.Size = new System.Drawing.Size(138, 14);
            this.lblUrusan.TabIndex = 26;
            this.lblUrusan.Text = "Urusan Pemerintahan";
            // 
            // lblKegiatan
            // 
            this.lblKegiatan.AutoSize = true;
            this.lblKegiatan.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKegiatan.Location = new System.Drawing.Point(67, 52);
            this.lblKegiatan.Name = "lblKegiatan";
            this.lblKegiatan.Size = new System.Drawing.Size(20, 16);
            this.lblKegiatan.TabIndex = 25;
            this.lblKegiatan.Text = "...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 16);
            this.label2.TabIndex = 24;
            this.label2.Text = "Prog";
            // 
            // txtpaguSub
            // 
            this.txtpaguSub.Location = new System.Drawing.Point(828, 90);
            this.txtpaguSub.Name = "txtpaguSub";
            this.txtpaguSub.Size = new System.Drawing.Size(35, 20);
            this.txtpaguSub.TabIndex = 10;
            this.txtpaguSub.Visible = false;
            // 
            // lblJumlah
            // 
            this.lblJumlah.AutoSize = true;
            this.lblJumlah.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJumlah.Location = new System.Drawing.Point(828, 115);
            this.lblJumlah.Name = "lblJumlah";
            this.lblJumlah.Size = new System.Drawing.Size(36, 19);
            this.lblJumlah.TabIndex = 9;
            this.lblJumlah.Text = "000";
            this.lblJumlah.Visible = false;
            // 
            // cmdSimpan
            // 
            this.cmdSimpan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmdSimpan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSimpan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSimpan.Location = new System.Drawing.Point(14, 167);
            this.cmdSimpan.Name = "cmdSimpan";
            this.cmdSimpan.Size = new System.Drawing.Size(90, 33);
            this.cmdSimpan.TabIndex = 8;
            this.cmdSimpan.Text = "Simpan";
            this.cmdSimpan.UseVisualStyleBackColor = false;
            this.cmdSimpan.Click += new System.EventHandler(this.cmdSimpan_Click);
            // 
            // lblProgram
            // 
            this.lblProgram.AutoSize = true;
            this.lblProgram.Location = new System.Drawing.Point(170, 31);
            this.lblProgram.Name = "lblProgram";
            this.lblProgram.Size = new System.Drawing.Size(0, 13);
            this.lblProgram.TabIndex = 4;
            // 
            // gridSumberDana
            // 
            this.gridSumberDana.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSumberDana.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridSumberDana.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSumberDana.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDRekeningSumberDana,
            this.KodeRekeningSumberDana,
            this.NamaRekeningSD,
            this.PergeseranAPBDPerubahan,
            this.Realisasi});
            this.gridSumberDana.Location = new System.Drawing.Point(3, 206);
            this.gridSumberDana.Name = "gridSumberDana";
            this.gridSumberDana.Size = new System.Drawing.Size(878, 201);
            this.gridSumberDana.TabIndex = 1;
            this.gridSumberDana.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSumberDana_CellContentClick);
            this.gridSumberDana.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSumberDana_CellEndEdit);
            // 
            // IDRekeningSumberDana
            // 
            this.IDRekeningSumberDana.HeaderText = "IDRek";
            this.IDRekeningSumberDana.Name = "IDRekeningSumberDana";
            this.IDRekeningSumberDana.Visible = false;
            // 
            // KodeRekeningSumberDana
            // 
            this.KodeRekeningSumberDana.HeaderText = "Kode Rekening";
            this.KodeRekeningSumberDana.Name = "KodeRekeningSumberDana";
            this.KodeRekeningSumberDana.ReadOnly = true;
            this.KodeRekeningSumberDana.Width = 120;
            // 
            // NamaRekeningSD
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NamaRekeningSD.DefaultCellStyle = dataGridViewCellStyle1;
            this.NamaRekeningSD.HeaderText = "Nama Rekening";
            this.NamaRekeningSD.Name = "NamaRekeningSD";
            this.NamaRekeningSD.ReadOnly = true;
            this.NamaRekeningSD.Width = 400;
            // 
            // PergeseranAPBDPerubahan
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.PergeseranAPBDPerubahan.DefaultCellStyle = dataGridViewCellStyle2;
            this.PergeseranAPBDPerubahan.HeaderText = "Anggaran Pergeseran Perubahan";
            this.PergeseranAPBDPerubahan.Name = "PergeseranAPBDPerubahan";
            this.PergeseranAPBDPerubahan.ReadOnly = true;
            this.PergeseranAPBDPerubahan.Width = 150;
            // 
            // Realisasi
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Realisasi.DefaultCellStyle = dataGridViewCellStyle3;
            this.Realisasi.HeaderText = "Realisasi";
            this.Realisasi.Name = "Realisasi";
            this.Realisasi.Width = 150;
            // 
            // txtJumlah
            // 
            this.txtJumlah.Location = new System.Drawing.Point(816, 112);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(163, 20);
            this.txtJumlah.TabIndex = 13;
            this.txtJumlah.Visible = false;
            // 
            // txtidrekening
            // 
            this.txtidrekening.Location = new System.Drawing.Point(816, 86);
            this.txtidrekening.Name = "txtidrekening";
            this.txtidrekening.Size = new System.Drawing.Size(228, 20);
            this.txtidrekening.TabIndex = 12;
            this.txtidrekening.Visible = false;
            // 
            // txtIDSUBKegiatan
            // 
            this.txtIDSUBKegiatan.Location = new System.Drawing.Point(811, 60);
            this.txtIDSUBKegiatan.Name = "txtIDSUBKegiatan";
            this.txtIDSUBKegiatan.Size = new System.Drawing.Size(233, 20);
            this.txtIDSUBKegiatan.TabIndex = 11;
            this.txtIDSUBKegiatan.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1078, 58);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "IDRek";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Kode Rekening";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 120;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn3.HeaderText = "Nama Rekening";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 400;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn4.HeaderText = "Anggaran Pergeseran Perubahan";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn5.HeaderText = "Realisasi";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 150;
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(108, 53);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(610, 45);
            this.ctrlDinas1.TabIndex = 10;
            this.ctrlDinas1.OnChanged += new KUAPPAS.ctrlDinas.ValueChangedEventHandler(this.ctrlDinas1_OnChanged);
            this.ctrlDinas1.Load += new System.EventHandler(this.ctrlDinas1_Load_1);
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1356, 47);
            this.ctrlHeader1.TabIndex = 9;
            // 
            // ctrlJenisAnggaran1
            // 
            this.ctrlJenisAnggaran1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlJenisAnggaran1.Location = new System.Drawing.Point(108, 99);
            this.ctrlJenisAnggaran1.Name = "ctrlJenisAnggaran1";
            this.ctrlJenisAnggaran1.Size = new System.Drawing.Size(610, 21);
            this.ctrlJenisAnggaran1.TabIndex = 6;
            this.ctrlJenisAnggaran1.OnChanged += new KUAPPAS.ctrlJenisAnggaran.ValueChangedEventHandler(this.ctrlJenisAnggaran1_OnChanged);
            this.ctrlJenisAnggaran1.Load += new System.EventHandler(this.ctrlJenisAnggaran1_Load);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(39, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "O P D";
            // 
            // frmInputRealisasiPerRekening
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1356, 611);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.txtidrekening);
            this.Controls.Add(this.txtIDSUBKegiatan);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlJenisAnggaran1);
            this.Name = "frmInputRealisasiPerRekening";
            this.Text = "Input Plafon";
            this.Load += new System.EventHandler(this.frmInputRealisasiPerRekening_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSumberDana)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private ctrlJenisAnggaran ctrlJenisAnggaran1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private TreeProgramKegiatan treeProgramKegiatan1;
        private System.Windows.Forms.Label lblJumlah;
        private System.Windows.Forms.Button cmdSimpan;
        private System.Windows.Forms.Label lblProgram;
        private System.Windows.Forms.DataGridView gridSumberDana;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.TextBox txtpaguSub;
        private System.Windows.Forms.Label lblNamaProgram;
        private System.Windows.Forms.Label lbllNamaSubKegiatan;
        private System.Windows.Forms.Label lblKodeKegiatan;
        private System.Windows.Forms.Label lblSubKegiatan;
        private System.Windows.Forms.Label lblUrusan;
        private System.Windows.Forms.Label lblKegiatan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtJumlahMurni;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtJumlahPergeseran;
        private ctrlDinas ctrlDinas1;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.TextBox txtidrekening;
        private System.Windows.Forms.TextBox txtIDSUBKegiatan;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDRekeningSumberDana;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodeRekeningSumberDana;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaRekeningSD;
        private System.Windows.Forms.DataGridViewTextBoxColumn PergeseranAPBDPerubahan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Realisasi;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Label label4;
    }
}