namespace KUAPPAS
{
    partial class frmInputPlafonPerRekening
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlJenisAnggaran1 = new KUAPPAS.ctrlJenisAnggaran();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeProgramKegiatan1 = new KUAPPAS.TreeProgramKegiatan();
            this.cmdTambahRekening = new System.Windows.Forms.Button();
            this.cmdCetakDPA22 = new System.Windows.Forms.Button();
            this.cmdCetakDPASKPD = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtJumlahPergeseranPerubahan = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtJumlahPergeseran = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtJumlahMurni = new System.Windows.Forms.TextBox();
            this.lblNamaProgram = new System.Windows.Forms.Label();
            this.lbllNamaSubKegiatan = new System.Windows.Forms.Label();
            this.lblKodeKegiatan = new System.Windows.Forms.Label();
            this.lblSubKegiatan = new System.Windows.Forms.Label();
            this.lblUrusan = new System.Windows.Forms.Label();
            this.lblKegiatan = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtJumlahPerubahan = new System.Windows.Forms.TextBox();
            this.txtpaguSub = new System.Windows.Forms.TextBox();
            this.lblJumlah = new System.Windows.Forms.Label();
            this.cmdSimpan = new System.Windows.Forms.Button();
            this.lblProgram = new System.Windows.Forms.Label();
            this.gridSumberDana = new System.Windows.Forms.DataGridView();
            this.IDRekeningSumberDana = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KodeRekeningSumberDana = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaRekeningSD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.APBD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.APBDPergeseran = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.APBDP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PergeseranAPBDPerubahan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Realisasi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.txtidrekening = new System.Windows.Forms.TextBox();
            this.txtIDSUBKegiatan = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cmdTambahSubKegiatan = new System.Windows.Forms.Button();
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
            this.splitContainer1.Panel2.Controls.Add(this.cmdTambahSubKegiatan);
            this.splitContainer1.Panel2.Controls.Add(this.cmdTambahRekening);
            this.splitContainer1.Panel2.Controls.Add(this.cmdCetakDPA22);
            this.splitContainer1.Panel2.Controls.Add(this.cmdCetakDPASKPD);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.txtJumlahPergeseranPerubahan);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.txtJumlahPergeseran);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.txtJumlahMurni);
            this.splitContainer1.Panel2.Controls.Add(this.lblNamaProgram);
            this.splitContainer1.Panel2.Controls.Add(this.lbllNamaSubKegiatan);
            this.splitContainer1.Panel2.Controls.Add(this.lblKodeKegiatan);
            this.splitContainer1.Panel2.Controls.Add(this.lblSubKegiatan);
            this.splitContainer1.Panel2.Controls.Add(this.lblUrusan);
            this.splitContainer1.Panel2.Controls.Add(this.lblKegiatan);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.txtJumlahPerubahan);
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
            // cmdTambahRekening
            // 
            this.cmdTambahRekening.Location = new System.Drawing.Point(288, 167);
            this.cmdTambahRekening.Name = "cmdTambahRekening";
            this.cmdTambahRekening.Size = new System.Drawing.Size(128, 33);
            this.cmdTambahRekening.TabIndex = 38;
            this.cmdTambahRekening.Text = "Tambah Rekening";
            this.cmdTambahRekening.UseVisualStyleBackColor = true;
            this.cmdTambahRekening.Click += new System.EventHandler(this.cmdTambahRekening_Click);
            // 
            // cmdCetakDPA22
            // 
            this.cmdCetakDPA22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.cmdCetakDPA22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCetakDPA22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCetakDPA22.Location = new System.Drawing.Point(263, 122);
            this.cmdCetakDPA22.Name = "cmdCetakDPA22";
            this.cmdCetakDPA22.Size = new System.Drawing.Size(112, 33);
            this.cmdCetakDPA22.TabIndex = 10;
            this.cmdCetakDPA22.Text = "Cetak DPPA 2.2 ";
            this.cmdCetakDPA22.UseVisualStyleBackColor = false;
            this.cmdCetakDPA22.Visible = false;
            this.cmdCetakDPA22.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmdCetakDPASKPD
            // 
            this.cmdCetakDPASKPD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.cmdCetakDPASKPD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCetakDPASKPD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCetakDPASKPD.Location = new System.Drawing.Point(129, 122);
            this.cmdCetakDPASKPD.Name = "cmdCetakDPASKPD";
            this.cmdCetakDPASKPD.Size = new System.Drawing.Size(109, 33);
            this.cmdCetakDPASKPD.TabIndex = 11;
            this.cmdCetakDPASKPD.Text = "DPPA SKPD";
            this.cmdCetakDPASKPD.UseVisualStyleBackColor = false;
            this.cmdCetakDPASKPD.Visible = false;
            this.cmdCetakDPASKPD.Click += new System.EventHandler(this.cmdCetakDPASKPD_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(412, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(194, 16);
            this.label6.TabIndex = 37;
            this.label6.Text = "Jumlah Pergeseran Perubahan";
            // 
            // txtJumlahPergeseranPerubahan
            // 
            this.txtJumlahPergeseranPerubahan.Location = new System.Drawing.Point(606, 162);
            this.txtJumlahPergeseranPerubahan.Name = "txtJumlahPergeseranPerubahan";
            this.txtJumlahPergeseranPerubahan.Size = new System.Drawing.Size(216, 20);
            this.txtJumlahPergeseranPerubahan.TabIndex = 36;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(412, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 16);
            this.label5.TabIndex = 35;
            this.label5.Text = "Jumlah Pergeseran";
            // 
            // txtJumlahPergeseran
            // 
            this.txtJumlahPergeseran.Location = new System.Drawing.Point(606, 117);
            this.txtJumlahPergeseran.Name = "txtJumlahPergeseran";
            this.txtJumlahPergeseran.Size = new System.Drawing.Size(216, 20);
            this.txtJumlahPergeseran.TabIndex = 34;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(412, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 16);
            this.label4.TabIndex = 33;
            this.label4.Text = "Jumlah Perubahan ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(412, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 16);
            this.label3.TabIndex = 32;
            this.label3.Text = "Jumlah Murni";
            // 
            // txtJumlahMurni
            // 
            this.txtJumlahMurni.Location = new System.Drawing.Point(606, 93);
            this.txtJumlahMurni.Name = "txtJumlahMurni";
            this.txtJumlahMurni.Size = new System.Drawing.Size(216, 20);
            this.txtJumlahMurni.TabIndex = 31;
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
            // txtJumlahPerubahan
            // 
            this.txtJumlahPerubahan.Location = new System.Drawing.Point(606, 140);
            this.txtJumlahPerubahan.Name = "txtJumlahPerubahan";
            this.txtJumlahPerubahan.Size = new System.Drawing.Size(216, 20);
            this.txtJumlahPerubahan.TabIndex = 11;
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
            this.gridSumberDana.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridSumberDana.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridSumberDana.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSumberDana.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDRekeningSumberDana,
            this.KodeRekeningSumberDana,
            this.NamaRekeningSD,
            this.APBD,
            this.APBDPergeseran,
            this.APBDP,
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
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NamaRekeningSD.DefaultCellStyle = dataGridViewCellStyle7;
            this.NamaRekeningSD.HeaderText = "Nama Rekening";
            this.NamaRekeningSD.Name = "NamaRekeningSD";
            this.NamaRekeningSD.ReadOnly = true;
            this.NamaRekeningSD.Width = 350;
            // 
            // APBD
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.APBD.DefaultCellStyle = dataGridViewCellStyle8;
            this.APBD.HeaderText = "Anggaran Murni";
            this.APBD.Name = "APBD";
            this.APBD.Width = 150;
            // 
            // APBDPergeseran
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.APBDPergeseran.DefaultCellStyle = dataGridViewCellStyle9;
            this.APBDPergeseran.HeaderText = "Anggaran Pergeseran";
            this.APBDPergeseran.Name = "APBDPergeseran";
            // 
            // APBDP
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.APBDP.DefaultCellStyle = dataGridViewCellStyle10;
            this.APBDP.HeaderText = "Anggaran Perubahan";
            this.APBDP.Name = "APBDP";
            this.APBDP.Width = 150;
            // 
            // PergeseranAPBDPerubahan
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.PergeseranAPBDPerubahan.DefaultCellStyle = dataGridViewCellStyle11;
            this.PergeseranAPBDPerubahan.HeaderText = "Anggaran Pergeseran Perubahan";
            this.PergeseranAPBDPerubahan.Name = "PergeseranAPBDPerubahan";
            this.PergeseranAPBDPerubahan.Width = 150;
            // 
            // Realisasi
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Realisasi.DefaultCellStyle = dataGridViewCellStyle12;
            this.Realisasi.HeaderText = "Realisasi";
            this.Realisasi.Name = "Realisasi";
            this.Realisasi.ReadOnly = true;
            this.Realisasi.Width = 150;
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
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(108, 53);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(610, 45);
            this.ctrlDinas1.TabIndex = 10;
            this.ctrlDinas1.OnChanged += new KUAPPAS.ctrlDinas.ValueChangedEventHandler(this.ctrlDinas1_OnChanged);
            this.ctrlDinas1.Load += new System.EventHandler(this.ctrlDinas1_Load_1);
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
            // cmdTambahSubKegiatan
            // 
            this.cmdTambahSubKegiatan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTambahSubKegiatan.Location = new System.Drawing.Point(110, 167);
            this.cmdTambahSubKegiatan.Name = "cmdTambahSubKegiatan";
            this.cmdTambahSubKegiatan.Size = new System.Drawing.Size(172, 33);
            this.cmdTambahSubKegiatan.TabIndex = 39;
            this.cmdTambahSubKegiatan.Text = "Tambah Sub Kegiatan";
            this.cmdTambahSubKegiatan.UseVisualStyleBackColor = true;
            this.cmdTambahSubKegiatan.Click += new System.EventHandler(this.cmdTambahSubKegiatan_Click);
            // 
            // frmInputPlafonPerRekening
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1356, 611);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.txtidrekening);
            this.Controls.Add(this.txtIDSUBKegiatan);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlJenisAnggaran1);
            this.Name = "frmInputPlafonPerRekening";
            this.Text = "Input Plafon";
            this.Load += new System.EventHandler(this.frmInputPlafonPerRekening_Load);
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
        private System.Windows.Forms.Button cmdCetakDPA22;
        private System.Windows.Forms.TextBox txtJumlahPerubahan;
        private System.Windows.Forms.TextBox txtpaguSub;
        private System.Windows.Forms.Label lblNamaProgram;
        private System.Windows.Forms.Label lbllNamaSubKegiatan;
        private System.Windows.Forms.Label lblKodeKegiatan;
        private System.Windows.Forms.Label lblSubKegiatan;
        private System.Windows.Forms.Label lblUrusan;
        private System.Windows.Forms.Label lblKegiatan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtJumlahMurni;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtJumlahPergeseran;
        private System.Windows.Forms.Button cmdCetakDPASKPD;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtJumlahPergeseranPerubahan;
        private ctrlDinas ctrlDinas1;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.TextBox txtidrekening;
        private System.Windows.Forms.TextBox txtIDSUBKegiatan;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button cmdTambahRekening;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDRekeningSumberDana;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodeRekeningSumberDana;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaRekeningSD;
        private System.Windows.Forms.DataGridViewTextBoxColumn APBD;
        private System.Windows.Forms.DataGridViewTextBoxColumn APBDPergeseran;
        private System.Windows.Forms.DataGridViewTextBoxColumn APBDP;
        private System.Windows.Forms.DataGridViewTextBoxColumn PergeseranAPBDPerubahan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Realisasi;
        private System.Windows.Forms.Button cmdTambahSubKegiatan;
    }
}