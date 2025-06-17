namespace KUAPPAS.Bendahara
{
    partial class frmPengeluaranPengembalian
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
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.txtNoBukti = new System.Windows.Forms.TextBox();
            this.ctrlTanggal1 = new KUAPPAS.ctrlTanggal();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.ctrlJenisSPP1 = new KUAPPAS.ctrlJenisSPP();
            this.ctrlSPP1 = new KUAPPAS.Bendahara.ctrlSPP();
            this.groupRekening = new System.Windows.Forms.GroupBox();
            this.gridSPPRekening = new System.Windows.Forms.DataGridView();
            this.KodeUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KodeSub = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDRekening = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KodeRekening = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nilai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiKembalikan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaUK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaSub = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ctrlProgram1 = new KUAPPAS.ctrlProgram();
            this.ctrlSubKegiatan1 = new KUAPPAS.ctrlSubKegiatan();
            this.ctrlKegiatanAPBD1 = new KUAPPAS.ctrlKegiatanAPBD();
            this.ctrlUrusanPemerintahan1 = new KUAPPAS.ctrlUrusanPemerintahan();
            this.label5 = new System.Windows.Forms.Label();
            this.lblNoSP2D = new System.Windows.Forms.Label();
            this.lblKeteranganAPP = new System.Windows.Forms.TextBox();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.chkUPTahunLalu = new System.Windows.Forms.CheckBox();
            this.ctrlNavigation1 = new KUAPPAS.ctrlNavigation();
            this.label7 = new System.Windows.Forms.Label();
            this.chkBank = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupRekening.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSPPRekening)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 16);
            this.label4.TabIndex = 19;
            this.label4.Text = "Keterangan";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "No Bukti";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 16);
            this.label2.TabIndex = 17;
            this.label2.Text = "Tanggal Pengembalian";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 16;
            this.label1.Text = "OPD";
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKeterangan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKeterangan.Location = new System.Drawing.Point(179, 139);
            this.txtKeterangan.Multiline = true;
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(695, 23);
            this.txtKeterangan.TabIndex = 15;
            // 
            // txtNoBukti
            // 
            this.txtNoBukti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoBukti.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoBukti.Location = new System.Drawing.Point(179, 90);
            this.txtNoBukti.Name = "txtNoBukti";
            this.txtNoBukti.Size = new System.Drawing.Size(280, 22);
            this.txtNoBukti.TabIndex = 14;
            // 
            // ctrlTanggal1
            // 
            this.ctrlTanggal1.Location = new System.Drawing.Point(179, 114);
            this.ctrlTanggal1.Name = "ctrlTanggal1";
            this.ctrlTanggal1.Size = new System.Drawing.Size(280, 25);
            this.ctrlTanggal1.TabIndex = 13;
            this.ctrlTanggal1.Tanggal = new System.DateTime(2023, 11, 9, 0, 0, 0, 0);
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(179, 45);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(695, 55);
            this.ctrlDinas1.TabIndex = 12;
            this.ctrlDinas1.OnChanged += new KUAPPAS.ctrlDinas.ValueChangedEventHandler(this.ctrlDinas1_OnChanged);
            // 
            // ctrlJenisSPP1
            // 
            this.ctrlJenisSPP1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlJenisSPP1.ID = 0;
            this.ctrlJenisSPP1.Location = new System.Drawing.Point(179, 165);
            this.ctrlJenisSPP1.Name = "ctrlJenisSPP1";
            this.ctrlJenisSPP1.Size = new System.Drawing.Size(280, 22);
            this.ctrlJenisSPP1.TabIndex = 20;
            this.ctrlJenisSPP1.OnChanged += new KUAPPAS.ctrlJenisSPP.ValueChangedEventHandler(this.ctrlJenisSPP1_OnChanged);
            this.ctrlJenisSPP1.Load += new System.EventHandler(this.ctrlJenisSPP1_Load);
            // 
            // ctrlSPP1
            // 
            this.ctrlSPP1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSPP1.ID = ((long)(0));
            this.ctrlSPP1.Location = new System.Drawing.Point(179, 189);
            this.ctrlSPP1.Name = "ctrlSPP1";
            this.ctrlSPP1.Size = new System.Drawing.Size(280, 23);
            this.ctrlSPP1.TabIndex = 21;
            this.ctrlSPP1.Visible = false;
            this.ctrlSPP1.OnChanged += new KUAPPAS.Bendahara.ctrlSPP.ValueChangedEventHandler(this.ctrlSPP1_OnChanged);
            this.ctrlSPP1.Load += new System.EventHandler(this.ctrlSPP1_Load);
            // 
            // groupRekening
            // 
            this.groupRekening.Controls.Add(this.gridSPPRekening);
            this.groupRekening.Controls.Add(this.label18);
            this.groupRekening.Controls.Add(this.label17);
            this.groupRekening.Controls.Add(this.label16);
            this.groupRekening.Controls.Add(this.label6);
            this.groupRekening.Controls.Add(this.ctrlProgram1);
            this.groupRekening.Controls.Add(this.ctrlSubKegiatan1);
            this.groupRekening.Controls.Add(this.ctrlKegiatanAPBD1);
            this.groupRekening.Controls.Add(this.ctrlUrusanPemerintahan1);
            this.groupRekening.Location = new System.Drawing.Point(12, 253);
            this.groupRekening.Name = "groupRekening";
            this.groupRekening.Size = new System.Drawing.Size(882, 352);
            this.groupRekening.TabIndex = 22;
            this.groupRekening.TabStop = false;
            // 
            // gridSPPRekening
            // 
            this.gridSPPRekening.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridSPPRekening.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSPPRekening.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KodeUnit,
            this.KodeSub,
            this.IDRekening,
            this.KodeRekening,
            this.Nama,
            this.Nilai,
            this.DiKembalikan,
            this.NamaUK,
            this.NamaSub});
            this.gridSPPRekening.Location = new System.Drawing.Point(31, 128);
            this.gridSPPRekening.Name = "gridSPPRekening";
            this.gridSPPRekening.Size = new System.Drawing.Size(831, 185);
            this.gridSPPRekening.TabIndex = 35;
            this.gridSPPRekening.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.gridSPPRekening_CellBeginEdit);
            this.gridSPPRekening.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSPPRekening_CellEndEdit);
            // 
            // KodeUnit
            // 
            this.KodeUnit.HeaderText = "Unit/Bagian";
            this.KodeUnit.Name = "KodeUnit";
            this.KodeUnit.ReadOnly = true;
            this.KodeUnit.Width = 50;
            // 
            // KodeSub
            // 
            this.KodeSub.HeaderText = "Sub Kegiatan";
            this.KodeSub.Name = "KodeSub";
            this.KodeSub.ReadOnly = true;
            this.KodeSub.Width = 80;
            // 
            // IDRekening
            // 
            this.IDRekening.HeaderText = "IDRekening";
            this.IDRekening.Name = "IDRekening";
            this.IDRekening.ReadOnly = true;
            this.IDRekening.Visible = false;
            this.IDRekening.Width = 80;
            // 
            // KodeRekening
            // 
            this.KodeRekening.HeaderText = "Kode Rekening";
            this.KodeRekening.Name = "KodeRekening";
            this.KodeRekening.ReadOnly = true;
            this.KodeRekening.Width = 150;
            // 
            // Nama
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Nama.DefaultCellStyle = dataGridViewCellStyle1;
            this.Nama.HeaderText = "Nama";
            this.Nama.Name = "Nama";
            this.Nama.ReadOnly = true;
            this.Nama.Width = 200;
            // 
            // Nilai
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Nilai.DefaultCellStyle = dataGridViewCellStyle2;
            this.Nilai.HeaderText = "Nilai SP2D";
            this.Nilai.Name = "Nilai";
            this.Nilai.ReadOnly = true;
            this.Nilai.Width = 150;
            // 
            // DiKembalikan
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiKembalikan.DefaultCellStyle = dataGridViewCellStyle3;
            this.DiKembalikan.HeaderText = "Jumlah Dikembalikan";
            this.DiKembalikan.Name = "DiKembalikan";
            this.DiKembalikan.Width = 150;
            // 
            // NamaUK
            // 
            this.NamaUK.HeaderText = "Nama Unit";
            this.NamaUK.Name = "NamaUK";
            this.NamaUK.ReadOnly = true;
            // 
            // NamaSub
            // 
            this.NamaSub.HeaderText = "NamaSubKegiatan";
            this.NamaSub.Name = "NamaSub";
            this.NamaSub.ReadOnly = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(28, 97);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(88, 16);
            this.label18.TabIndex = 34;
            this.label18.Text = "Sub Kegiatan";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(28, 72);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(61, 16);
            this.label17.TabIndex = 33;
            this.label17.Text = "Kegiatan";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(28, 47);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(60, 16);
            this.label16.TabIndex = 32;
            this.label16.Text = "Program";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(28, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 16);
            this.label6.TabIndex = 31;
            this.label6.Text = "Urusan Pemerintahan";
            // 
            // ctrlProgram1
            // 
            this.ctrlProgram1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlProgram1.Location = new System.Drawing.Point(167, 45);
            this.ctrlProgram1.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlProgram1.Name = "ctrlProgram1";
            this.ctrlProgram1.Size = new System.Drawing.Size(695, 24);
            this.ctrlProgram1.TabIndex = 30;
            this.ctrlProgram1.OnChanged += new KUAPPAS.ctrlProgram.ValueChangedEventHandler(this.ctrlProgram1_OnChanged);
            // 
            // ctrlSubKegiatan1
            // 
            this.ctrlSubKegiatan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSubKegiatan1.Location = new System.Drawing.Point(167, 97);
            this.ctrlSubKegiatan1.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlSubKegiatan1.Name = "ctrlSubKegiatan1";
            this.ctrlSubKegiatan1.Profile = 1;
            this.ctrlSubKegiatan1.Size = new System.Drawing.Size(695, 24);
            this.ctrlSubKegiatan1.TabIndex = 29;
            this.ctrlSubKegiatan1.OnChanged += new KUAPPAS.ctrlSubKegiatan.ValueChangedEventHandler(this.ctrlSubKegiatan1_OnChanged);
            // 
            // ctrlKegiatanAPBD1
            // 
            this.ctrlKegiatanAPBD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlKegiatanAPBD1.Location = new System.Drawing.Point(167, 70);
            this.ctrlKegiatanAPBD1.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlKegiatanAPBD1.Name = "ctrlKegiatanAPBD1";
            this.ctrlKegiatanAPBD1.Profile = 1;
            this.ctrlKegiatanAPBD1.Size = new System.Drawing.Size(695, 26);
            this.ctrlKegiatanAPBD1.TabIndex = 28;
            this.ctrlKegiatanAPBD1.OnChanged += new KUAPPAS.ctrlKegiatanAPBD.ValueChangedEventHandler(this.ctrlKegiatanAPBD1_OnChanged);
            // 
            // ctrlUrusanPemerintahan1
            // 
            this.ctrlUrusanPemerintahan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlUrusanPemerintahan1.Location = new System.Drawing.Point(167, 20);
            this.ctrlUrusanPemerintahan1.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlUrusanPemerintahan1.Name = "ctrlUrusanPemerintahan1";
            this.ctrlUrusanPemerintahan1.Size = new System.Drawing.Size(695, 24);
            this.ctrlUrusanPemerintahan1.TabIndex = 27;
            this.ctrlUrusanPemerintahan1.OnChanged += new KUAPPAS.ctrlUrusanPemerintahan.ValueChangedEventHandler(this.ctrlUrusanPemerintahan1_OnChanged);
            this.ctrlUrusanPemerintahan1.Load += new System.EventHandler(this.ctrlUrusanPemerintahan1_Load_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(25, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 16);
            this.label5.TabIndex = 23;
            this.label5.Text = "Jenis SP2D";
            // 
            // lblNoSP2D
            // 
            this.lblNoSP2D.AutoSize = true;
            this.lblNoSP2D.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoSP2D.Location = new System.Drawing.Point(25, 193);
            this.lblNoSP2D.Name = "lblNoSP2D";
            this.lblNoSP2D.Size = new System.Drawing.Size(64, 16);
            this.lblNoSP2D.TabIndex = 24;
            this.lblNoSP2D.Text = "No SP2D";
            this.lblNoSP2D.Visible = false;
            // 
            // lblKeteranganAPP
            // 
            this.lblKeteranganAPP.BackColor = System.Drawing.SystemColors.Control;
            this.lblKeteranganAPP.Location = new System.Drawing.Point(179, 216);
            this.lblKeteranganAPP.Multiline = true;
            this.lblKeteranganAPP.Name = "lblKeteranganAPP";
            this.lblKeteranganAPP.Size = new System.Drawing.Size(695, 47);
            this.lblKeteranganAPP.TabIndex = 25;
            // 
            // txtJumlah
            // 
            this.txtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlah.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlah.Location = new System.Drawing.Point(628, 193);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(246, 22);
            this.txtJumlah.TabIndex = 26;
            this.txtJumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkUPTahunLalu
            // 
            this.chkUPTahunLalu.AutoSize = true;
            this.chkUPTahunLalu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUPTahunLalu.Location = new System.Drawing.Point(486, 165);
            this.chkUPTahunLalu.Name = "chkUPTahunLalu";
            this.chkUPTahunLalu.Size = new System.Drawing.Size(236, 20);
            this.chkUPTahunLalu.TabIndex = 27;
            this.chkUPTahunLalu.Text = "Pengembalian Sisa UP Tahun Lalu";
            this.chkUPTahunLalu.UseVisualStyleBackColor = true;
            this.chkUPTahunLalu.CheckedChanged += new System.EventHandler(this.chkUPTahunLalu_CheckedChanged);
            // 
            // ctrlNavigation1
            // 
            this.ctrlNavigation1.BackColor = System.Drawing.Color.DimGray;
            this.ctrlNavigation1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlNavigation1.Location = new System.Drawing.Point(0, 0);
            this.ctrlNavigation1.Name = "ctrlNavigation1";
            this.ctrlNavigation1.Size = new System.Drawing.Size(906, 35);
            this.ctrlNavigation1.TabIndex = 28;
            this.ctrlNavigation1.OnAdd += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnAdd);
            this.ctrlNavigation1.OnSave += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnSave);
            this.ctrlNavigation1.OnDelete += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnDelete);
            this.ctrlNavigation1.Load += new System.EventHandler(this.ctrlNavigation1_Load);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(483, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(142, 16);
            this.label7.TabIndex = 29;
            this.label7.Text = "Jumlah Pengembalian";
            // 
            // chkBank
            // 
            this.chkBank.AutoSize = true;
            this.chkBank.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBank.Location = new System.Drawing.Point(486, 114);
            this.chkBank.Name = "chkBank";
            this.chkBank.Size = new System.Drawing.Size(165, 17);
            this.chkBank.TabIndex = 30;
            this.chkBank.Text = "Transaksi Transfer Bank";
            this.chkBank.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 600);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(906, 22);
            this.statusStrip1.TabIndex = 37;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // frmPengeluaranPengembalian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 622);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.chkBank);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ctrlNavigation1);
            this.Controls.Add(this.chkUPTahunLalu);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.lblKeteranganAPP);
            this.Controls.Add(this.lblNoSP2D);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupRekening);
            this.Controls.Add(this.ctrlSPP1);
            this.Controls.Add(this.ctrlJenisSPP1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtKeterangan);
            this.Controls.Add(this.txtNoBukti);
            this.Controls.Add(this.ctrlTanggal1);
            this.Controls.Add(this.ctrlDinas1);
            this.Name = "frmPengeluaranPengembalian";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pengembalian Belanja..";
            this.Load += new System.EventHandler(this.frmPengembalian_Load);
            this.groupRekening.ResumeLayout(false);
            this.groupRekening.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSPPRekening)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKeterangan;
        private System.Windows.Forms.TextBox txtNoBukti;
        private ctrlTanggal ctrlTanggal1;
        private ctrlDinas ctrlDinas1;
        private ctrlJenisSPP ctrlJenisSPP1;
        private ctrlSPP ctrlSPP1;
        private System.Windows.Forms.GroupBox groupRekening;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label6;
        private ctrlProgram ctrlProgram1;
        private ctrlSubKegiatan ctrlSubKegiatan1;
        private ctrlKegiatanAPBD ctrlKegiatanAPBD1;
        private ctrlUrusanPemerintahan ctrlUrusanPemerintahan1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblNoSP2D;
        private System.Windows.Forms.DataGridView gridSPPRekening;
        private System.Windows.Forms.TextBox lblKeteranganAPP;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.CheckBox chkUPTahunLalu;
        private ctrlNavigation ctrlNavigation1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkBank;
     //   private System.Windows.Forms.DataGridViewTextBoxColumn NamaUnit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodeUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodeSub;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDRekening;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodeRekening;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nilai;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiKembalikan;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaUK;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaSub;
    }
}