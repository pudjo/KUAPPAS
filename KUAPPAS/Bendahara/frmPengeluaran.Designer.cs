namespace KUAPPAS.Bendahara
{
    partial class frmPengeluaran
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
            this.chkUPGU = new System.Windows.Forms.RadioButton();
            this.chkTU = new System.Windows.Forms.RadioButton();
            this.txtNoBukti = new System.Windows.Forms.TextBox();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPenerima = new System.Windows.Forms.TextBox();
            this.TabRekening = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ctrlRekeningKegiatan1 = new KUAPPAS.ctrlRekeningKegiatan();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ctrlPotongan2 = new KUAPPAS.ctrlPotongan();
            this.txtNoBuktiPungut = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtJumlahPotongan = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblTU = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.ctrlNavigation1 = new KUAPPAS.ctrlNavigation();
            this.ctrlTanggal1 = new KUAPPAS.ctrlTanggal();
            this.ctrlVia1 = new KUAPPAS.ctrlVia();
            this.ctrlSPP1 = new KUAPPAS.Bendahara.ctrlSPP();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.lblPanjar = new System.Windows.Forms.Label();
            this.ctrlPanjar1 = new KUAPPAS.Bendahara.ctrlPanjar();
            this.panelPanjar = new System.Windows.Forms.Panel();
            this.txtKeteranganPanjar = new System.Windows.Forms.TextBox();
            this.panelProgram = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.ctrlProgramKegiatan1 = new KUAPPAS.ctrlProgramKegiatan();
            this.txtJumlahPanjar = new System.Windows.Forms.TextBox();
            this.lblJumlahPanjar = new System.Windows.Forms.Label();
            this.panelJenisBelanja = new System.Windows.Forms.Panel();
            this.ctrlFooter1 = new KUAPPAS.ctrlFooter();
            this.TabRekening.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panelPanjar.SuspendLayout();
            this.panelProgram.SuspendLayout();
            this.panelJenisBelanja.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkUPGU
            // 
            this.chkUPGU.AutoSize = true;
            this.chkUPGU.Checked = true;
            this.chkUPGU.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUPGU.Location = new System.Drawing.Point(4, 0);
            this.chkUPGU.Name = "chkUPGU";
            this.chkUPGU.Size = new System.Drawing.Size(83, 24);
            this.chkUPGU.TabIndex = 5;
            this.chkUPGU.TabStop = true;
            this.chkUPGU.Text = "UP/GU";
            this.chkUPGU.UseVisualStyleBackColor = true;
            this.chkUPGU.CheckedChanged += new System.EventHandler(this.chkUPGU_CheckedChanged_1);
            this.chkUPGU.Click += new System.EventHandler(this.chkUPGU_CheckedChanged);
            // 
            // chkTU
            // 
            this.chkTU.AutoSize = true;
            this.chkTU.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTU.Location = new System.Drawing.Point(4, 21);
            this.chkTU.Name = "chkTU";
            this.chkTU.Size = new System.Drawing.Size(50, 24);
            this.chkTU.TabIndex = 6;
            this.chkTU.Text = "TU";
            this.chkTU.UseVisualStyleBackColor = true;
            this.chkTU.CheckedChanged += new System.EventHandler(this.chkTU_CheckedChanged);
            // 
            // txtNoBukti
            // 
            this.txtNoBukti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoBukti.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoBukti.Location = new System.Drawing.Point(149, 91);
            this.txtNoBukti.Name = "txtNoBukti";
            this.txtNoBukti.Size = new System.Drawing.Size(387, 22);
            this.txtNoBukti.TabIndex = 7;
            // 
            // txtJumlah
            // 
            this.txtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlah.Enabled = false;
            this.txtJumlah.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlah.Location = new System.Drawing.Point(494, 192);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(185, 22);
            this.txtJumlah.TabIndex = 11;
            this.txtJumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(398, 198);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 16;
            this.label1.Text = "Jumlah";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPenerima
            // 
            this.txtPenerima.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPenerima.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPenerima.Location = new System.Drawing.Point(148, 382);
            this.txtPenerima.Name = "txtPenerima";
            this.txtPenerima.Size = new System.Drawing.Size(790, 21);
            this.txtPenerima.TabIndex = 18;
            // 
            // TabRekening
            // 
            this.TabRekening.Controls.Add(this.tabPage1);
            this.TabRekening.Controls.Add(this.tabPage2);
            this.TabRekening.Location = new System.Drawing.Point(147, 434);
            this.TabRekening.Name = "TabRekening";
            this.TabRekening.SelectedIndex = 0;
            this.TabRekening.Size = new System.Drawing.Size(800, 219);
            this.TabRekening.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ctrlRekeningKegiatan1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtJumlah);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 193);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Kode Rekening";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ctrlRekeningKegiatan1
            // 
            this.ctrlRekeningKegiatan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlRekeningKegiatan1.Location = new System.Drawing.Point(0, 3);
            this.ctrlRekeningKegiatan1.Name = "ctrlRekeningKegiatan1";
            this.ctrlRekeningKegiatan1.Size = new System.Drawing.Size(789, 178);
            this.ctrlRekeningKegiatan1.TabIndex = 10;
            this.ctrlRekeningKegiatan1.OnChanged += new KUAPPAS.ctrlRekeningKegiatan.ValueChangedEventHandler(this.ctrlRekeningKegiatan1_OnChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ctrlPotongan2);
            this.tabPage2.Controls.Add(this.txtNoBuktiPungut);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.txtJumlahPotongan);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 193);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Potongan";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ctrlPotongan2
            // 
            this.ctrlPotongan2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlPotongan2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlPotongan2.Location = new System.Drawing.Point(3, 32);
            this.ctrlPotongan2.Name = "ctrlPotongan2";
            this.ctrlPotongan2.Size = new System.Drawing.Size(781, 148);
            this.ctrlPotongan2.TabIndex = 22;
            // 
            // txtNoBuktiPungut
            // 
            this.txtNoBuktiPungut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoBuktiPungut.Location = new System.Drawing.Point(306, 6);
            this.txtNoBuktiPungut.Name = "txtNoBuktiPungut";
            this.txtNoBuktiPungut.Size = new System.Drawing.Size(223, 20);
            this.txtNoBuktiPungut.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(388, 199);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 16);
            this.label2.TabIndex = 19;
            this.label2.Text = "Jumlah Potongan";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtJumlahPotongan
            // 
            this.txtJumlahPotongan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlahPotongan.Enabled = false;
            this.txtJumlahPotongan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlahPotongan.Location = new System.Drawing.Point(528, 191);
            this.txtJumlahPotongan.Name = "txtJumlahPotongan";
            this.txtJumlahPotongan.Size = new System.Drawing.Size(213, 22);
            this.txtJumlahPotongan.TabIndex = 18;
            this.txtJumlahPotongan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 384);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 15);
            this.label6.TabIndex = 23;
            this.label6.Text = "Penerima";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 97);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 15);
            this.label7.TabIndex = 28;
            this.label7.Text = "No Bukti";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(542, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 15);
            this.label8.TabIndex = 29;
            this.label8.Text = "Tanggal";
            // 
            // lblTU
            // 
            this.lblTU.AutoSize = true;
            this.lblTU.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTU.Location = new System.Drawing.Point(93, 25);
            this.lblTU.Name = "lblTU";
            this.lblTU.Size = new System.Drawing.Size(91, 16);
            this.lblTU.TabIndex = 31;
            this.lblTU.Text = "SP2D TU No: ";
            this.lblTU.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(12, 353);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(103, 15);
            this.label13.TabIndex = 34;
            this.label13.Text = "Tunai atau Bank?";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(12, 44);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(42, 16);
            this.label15.TabIndex = 36;
            this.label15.Text = "U P D";
            // 
            // ctrlNavigation1
            // 
            this.ctrlNavigation1.BackColor = System.Drawing.Color.DimGray;
            this.ctrlNavigation1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlNavigation1.Location = new System.Drawing.Point(0, 0);
            this.ctrlNavigation1.Name = "ctrlNavigation1";
            this.ctrlNavigation1.Size = new System.Drawing.Size(947, 35);
            this.ctrlNavigation1.TabIndex = 26;
            this.ctrlNavigation1.OnAdd += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnAdd);
            this.ctrlNavigation1.OnSave += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnSave);
            this.ctrlNavigation1.OnDelete += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnDelete);
            this.ctrlNavigation1.Load += new System.EventHandler(this.ctrlNavigation1_Load);
            // 
            // ctrlTanggal1
            // 
            this.ctrlTanggal1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlTanggal1.Location = new System.Drawing.Point(618, 90);
            this.ctrlTanggal1.Name = "ctrlTanggal1";
            this.ctrlTanggal1.Size = new System.Drawing.Size(318, 23);
            this.ctrlTanggal1.TabIndex = 24;
            this.ctrlTanggal1.Tanggal = new System.DateTime(2020, 6, 29, 0, 0, 0, 0);
            // 
            // ctrlVia1
            // 
            this.ctrlVia1.Bank = 0;
            this.ctrlVia1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlVia1.Location = new System.Drawing.Point(149, 357);
            this.ctrlVia1.Name = "ctrlVia1";
            this.ctrlVia1.Size = new System.Drawing.Size(788, 23);
            this.ctrlVia1.TabIndex = 13;
            // 
            // ctrlSPP1
            // 
            this.ctrlSPP1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSPP1.ID = ((long)(0));
            this.ctrlSPP1.Location = new System.Drawing.Point(191, 21);
            this.ctrlSPP1.Name = "ctrlSPP1";
            this.ctrlSPP1.Size = new System.Drawing.Size(269, 25);
            this.ctrlSPP1.TabIndex = 4;
            this.ctrlSPP1.Visible = false;
            this.ctrlSPP1.OnChanged += new KUAPPAS.Bendahara.ctrlSPP.ValueChangedEventHandler(this.ctrlSPP1_OnChanged);
            this.ctrlSPP1.Load += new System.EventHandler(this.ctrlSPP1_Load);
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(148, 44);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(794, 45);
            this.ctrlDinas1.TabIndex = 1;
            this.ctrlDinas1.UK = 0;
            this.ctrlDinas1.OnChanged += new KUAPPAS.ctrlDinas.ValueChangedEventHandler(this.ctrlDinas1_OnChanged);
            this.ctrlDinas1.Load += new System.EventHandler(this.ctrlDinas1_Load);
            // 
            // lblPanjar
            // 
            this.lblPanjar.AutoSize = true;
            this.lblPanjar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPanjar.Location = new System.Drawing.Point(13, 119);
            this.lblPanjar.Name = "lblPanjar";
            this.lblPanjar.Size = new System.Drawing.Size(62, 15);
            this.lblPanjar.TabIndex = 31;
            this.lblPanjar.Text = "No Panjar";
            // 
            // ctrlPanjar1
            // 
            this.ctrlPanjar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlPanjar1.Location = new System.Drawing.Point(0, 3);
            this.ctrlPanjar1.Name = "ctrlPanjar1";
            this.ctrlPanjar1.Size = new System.Drawing.Size(244, 24);
            this.ctrlPanjar1.TabIndex = 32;
            this.ctrlPanjar1.OnChanged += new KUAPPAS.Bendahara.ctrlPanjar.ValueChangedEventHandler(this.ctrlPanjar1_OnChanged);
            this.ctrlPanjar1.Load += new System.EventHandler(this.ctrlPanjar1_Load);
            // 
            // panelPanjar
            // 
            this.panelPanjar.Controls.Add(this.txtKeteranganPanjar);
            this.panelPanjar.Controls.Add(this.ctrlPanjar1);
            this.panelPanjar.Location = new System.Drawing.Point(149, 113);
            this.panelPanjar.Name = "panelPanjar";
            this.panelPanjar.Size = new System.Drawing.Size(261, 77);
            this.panelPanjar.TabIndex = 39;
            // 
            // txtKeteranganPanjar
            // 
            this.txtKeteranganPanjar.BackColor = System.Drawing.SystemColors.Control;
            this.txtKeteranganPanjar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtKeteranganPanjar.Location = new System.Drawing.Point(-132, 28);
            this.txtKeteranganPanjar.Multiline = true;
            this.txtKeteranganPanjar.Name = "txtKeteranganPanjar";
            this.txtKeteranganPanjar.Size = new System.Drawing.Size(390, 43);
            this.txtKeteranganPanjar.TabIndex = 34;
            // 
            // panelProgram
            // 
            this.panelProgram.Controls.Add(this.label14);
            this.panelProgram.Controls.Add(this.label10);
            this.panelProgram.Controls.Add(this.label9);
            this.panelProgram.Controls.Add(this.label12);
            this.panelProgram.Controls.Add(this.label11);
            this.panelProgram.Controls.Add(this.label5);
            this.panelProgram.Controls.Add(this.label4);
            this.panelProgram.Controls.Add(this.label3);
            this.panelProgram.Controls.Add(this.txtKeterangan);
            this.panelProgram.Controls.Add(this.ctrlProgramKegiatan1);
            this.panelProgram.Location = new System.Drawing.Point(2, 190);
            this.panelProgram.Name = "panelProgram";
            this.panelProgram.Size = new System.Drawing.Size(939, 166);
            this.panelProgram.TabIndex = 40;
            this.panelProgram.Paint += new System.Windows.Forms.PaintEventHandler(this.panelProgram_Paint);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(12, 113);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 15);
            this.label14.TabIndex = 42;
            this.label14.Text = "Keterangan";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(11, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 15);
            this.label10.TabIndex = 41;
            this.label10.Text = "Kegiatan";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 15);
            this.label9.TabIndex = 40;
            this.label9.Text = "Program";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(10, 82);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 15);
            this.label12.TabIndex = 39;
            this.label12.Text = "Sub Kegiatan";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, -132);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 15);
            this.label11.TabIndex = 38;
            this.label11.Text = "Kegiatan";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, -82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 15);
            this.label5.TabIndex = 37;
            this.label5.Text = "Keterangan";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, -162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 15);
            this.label4.TabIndex = 36;
            this.label4.Text = "Program";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 15);
            this.label3.TabIndex = 35;
            this.label3.Text = "Urusan Pemerintahan ";
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKeterangan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKeterangan.Location = new System.Drawing.Point(146, 111);
            this.txtKeterangan.Multiline = true;
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(787, 53);
            this.txtKeterangan.TabIndex = 34;
            // 
            // ctrlProgramKegiatan1
            // 
            this.ctrlProgramKegiatan1.Location = new System.Drawing.Point(147, 2);
            this.ctrlProgramKegiatan1.Name = "ctrlProgramKegiatan1";
            this.ctrlProgramKegiatan1.Size = new System.Drawing.Size(787, 110);
            this.ctrlProgramKegiatan1.TabIndex = 33;
            this.ctrlProgramKegiatan1.OnChanged += new KUAPPAS.ctrlProgramKegiatan.ValueChangedEventHandler(this.ctrlProgramKegiatan1_OnChanged);
            this.ctrlProgramKegiatan1.Load += new System.EventHandler(this.ctrlProgramKegiatan1_Load_1);
            // 
            // txtJumlahPanjar
            // 
            this.txtJumlahPanjar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlahPanjar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlahPanjar.Location = new System.Drawing.Point(148, 406);
            this.txtJumlahPanjar.Name = "txtJumlahPanjar";
            this.txtJumlahPanjar.Size = new System.Drawing.Size(183, 22);
            this.txtJumlahPanjar.TabIndex = 41;
            // 
            // lblJumlahPanjar
            // 
            this.lblJumlahPanjar.AutoSize = true;
            this.lblJumlahPanjar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJumlahPanjar.Location = new System.Drawing.Point(12, 412);
            this.lblJumlahPanjar.Name = "lblJumlahPanjar";
            this.lblJumlahPanjar.Size = new System.Drawing.Size(92, 16);
            this.lblJumlahPanjar.TabIndex = 42;
            this.lblJumlahPanjar.Text = "Jumlah Panjar";
            // 
            // panelJenisBelanja
            // 
            this.panelJenisBelanja.Controls.Add(this.chkUPGU);
            this.panelJenisBelanja.Controls.Add(this.chkTU);
            this.panelJenisBelanja.Controls.Add(this.lblTU);
            this.panelJenisBelanja.Controls.Add(this.ctrlSPP1);
            this.panelJenisBelanja.Location = new System.Drawing.Point(412, 117);
            this.panelJenisBelanja.Name = "panelJenisBelanja";
            this.panelJenisBelanja.Size = new System.Drawing.Size(523, 74);
            this.panelJenisBelanja.TabIndex = 43;
            // 
            // ctrlFooter1
            // 
            this.ctrlFooter1.BackColor = System.Drawing.Color.Silver;
            this.ctrlFooter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctrlFooter1.Location = new System.Drawing.Point(0, 666);
            this.ctrlFooter1.Name = "ctrlFooter1";
            this.ctrlFooter1.Size = new System.Drawing.Size(947, 21);
            this.ctrlFooter1.TabIndex = 44;
            // 
            // frmPengeluaran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 687);
            this.Controls.Add(this.ctrlFooter1);
            this.Controls.Add(this.panelJenisBelanja);
            this.Controls.Add(this.TabRekening);
            this.Controls.Add(this.lblJumlahPanjar);
            this.Controls.Add(this.txtJumlahPanjar);
            this.Controls.Add(this.panelProgram);
            this.Controls.Add(this.lblPanjar);
            this.Controls.Add(this.panelPanjar);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ctrlNavigation1);
            this.Controls.Add(this.ctrlTanggal1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtPenerima);
            this.Controls.Add(this.ctrlVia1);
            this.Controls.Add(this.txtNoBukti);
            this.Controls.Add(this.ctrlDinas1);
            this.Name = "frmPengeluaran";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "engeluaran";
            this.Load += new System.EventHandler(this.frmPengeluaran_Load);
            this.Click += new System.EventHandler(this.chkTU_CheckedChanged);
            this.TabRekening.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panelPanjar.ResumeLayout(false);
            this.panelPanjar.PerformLayout();
            this.panelProgram.ResumeLayout(false);
            this.panelProgram.PerformLayout();
            this.panelJenisBelanja.ResumeLayout(false);
            this.panelJenisBelanja.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlDinas ctrlDinas1;
        private ctrlSPP ctrlSPP1;
        private System.Windows.Forms.RadioButton chkUPGU;
        private System.Windows.Forms.RadioButton chkTU;
        private System.Windows.Forms.TextBox txtNoBukti;
        private ctrlRekeningKegiatan ctrlRekeningKegiatan1;
        private System.Windows.Forms.TextBox txtJumlah;
        private ctrlPilihanPotongan ctrlPotongan1;
        private ctrlVia ctrlVia1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPenerima;
        private System.Windows.Forms.TabControl TabRekening;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtJumlahPotongan;
        private System.Windows.Forms.Label label6;
        private ctrlTanggal ctrlTanggal1;
        private ctrlNavigation ctrlNavigation1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTU;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtNoBuktiPungut;
        private System.Windows.Forms.Label lblPanjar;
        private ctrlPanjar ctrlPanjar1;
        private System.Windows.Forms.Panel panelPanjar;
        private System.Windows.Forms.Panel panelProgram;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKeterangan;
        private ctrlProgramKegiatan ctrlProgramKegiatan1;
        private System.Windows.Forms.TextBox txtJumlahPanjar;
        private System.Windows.Forms.Label lblJumlahPanjar;
        private System.Windows.Forms.TextBox txtKeteranganPanjar;
        private System.Windows.Forms.Panel panelJenisBelanja;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private ctrlPotongan ctrlPotongan2;
        private ctrlFooter ctrlFooter1;
    }
}