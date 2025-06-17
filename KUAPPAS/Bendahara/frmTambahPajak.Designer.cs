namespace KUAPPAS.Bendahara
{
    partial class frmTambahPajak
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
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.cmdOK = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.xmsBatal = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.txtkota = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtnomorObjekPajak = new System.Windows.Forms.TextBox();
            this.txtnomorSk = new System.Windows.Forms.TextBox();
            this.txttahunPajak = new System.Windows.Forms.TextBox();
            this.txtmasaPajak = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtalamatWajibPajak = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtnamawajibpajak = new System.Windows.Forms.TextBox();
            this.txtnomorPokokWajibPajakPenyetor = new System.Windows.Forms.TextBox();
            this.txtnik = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtnomorSPM = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtnomorSKPD = new System.Windows.Forms.TextBox();
            this.txtnomorFakturPajak = new System.Windows.Forms.TextBox();
            this.txtnikRekanan = new System.Windows.Forms.TextBox();
            this.txtnomorPokokWajibPajakRekanan = new System.Windows.Forms.TextBox();
            this.cmdCekNPWP = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.cmdInquiryMPN = new System.Windows.Forms.Button();
            this.cmdTutup = new System.Windows.Forms.Button();
            this.cmdTest = new System.Windows.Forms.Button();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.cmdGenerateReport = new System.Windows.Forms.Button();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.ctrlKodeSetor1 = new KUAPPAS.Bendahara.ctrlKodeSetor();
            this.ctrlPotongan1 = new KUAPPAS.Bendahara.ctrlPilihanPotongan();
            this.SuspendLayout();
            // 
            // txtJumlah
            // 
            this.txtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlah.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlah.Location = new System.Drawing.Point(205, 99);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(335, 21);
            this.txtJumlah.TabIndex = 3;
            this.txtJumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtJumlah.TextChanged += new System.EventHandler(this.txtJumlah_TextChanged);
            this.txtJumlah.Enter += new System.EventHandler(this.txtJumlah_Enter);
            this.txtJumlah.Leave += new System.EventHandler(this.txtJumlah_Leave);
            // 
            // cmdOK
            // 
            this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdOK.Location = new System.Drawing.Point(272, 527);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(122, 29);
            this.cmdOK.TabIndex = 4;
            this.cmdOK.Text = "OK ";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 573);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(777, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Kode Potongan";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Kode Setor";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Jumlah";
            // 
            // xmsBatal
            // 
            this.xmsBatal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xmsBatal.Location = new System.Drawing.Point(182, 527);
            this.xmsBatal.Name = "xmsBatal";
            this.xmsBatal.Size = new System.Drawing.Size(75, 29);
            this.xmsBatal.TabIndex = 9;
            this.xmsBatal.Text = "Batal";
            this.xmsBatal.UseVisualStyleBackColor = true;
            this.xmsBatal.Click += new System.EventHandler(this.xmsBatal_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(13, 328);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(29, 13);
            this.label18.TabIndex = 96;
            this.label18.Text = "Kota";
            // 
            // txtkota
            // 
            this.txtkota.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtkota.Location = new System.Drawing.Point(205, 328);
            this.txtkota.Name = "txtkota";
            this.txtkota.Size = new System.Drawing.Size(335, 20);
            this.txtkota.TabIndex = 95;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(13, 126);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(63, 13);
            this.label17.TabIndex = 94;
            this.label17.Text = "Masa Pajak";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(13, 152);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(68, 13);
            this.label16.TabIndex = 93;
            this.label16.Text = "Tahun Pajak";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 222);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 13);
            this.label15.TabIndex = 92;
            this.label15.Text = "Nomor  SK";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 198);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(99, 13);
            this.label14.TabIndex = 91;
            this.label14.Text = "Nomor Objek Pajak";
            // 
            // txtnomorObjekPajak
            // 
            this.txtnomorObjekPajak.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtnomorObjekPajak.Location = new System.Drawing.Point(205, 198);
            this.txtnomorObjekPajak.Name = "txtnomorObjekPajak";
            this.txtnomorObjekPajak.Size = new System.Drawing.Size(335, 20);
            this.txtnomorObjekPajak.TabIndex = 90;
            this.txtnomorObjekPajak.TextChanged += new System.EventHandler(this.txtnomorObjekPajak_TextChanged);
            // 
            // txtnomorSk
            // 
            this.txtnomorSk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtnomorSk.Location = new System.Drawing.Point(205, 224);
            this.txtnomorSk.Name = "txtnomorSk";
            this.txtnomorSk.Size = new System.Drawing.Size(335, 20);
            this.txtnomorSk.TabIndex = 89;
            this.txtnomorSk.TextChanged += new System.EventHandler(this.txtnomorSk_TextChanged);
            // 
            // txttahunPajak
            // 
            this.txttahunPajak.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttahunPajak.Location = new System.Drawing.Point(205, 149);
            this.txttahunPajak.Name = "txttahunPajak";
            this.txttahunPajak.Size = new System.Drawing.Size(335, 20);
            this.txttahunPajak.TabIndex = 88;
            this.txttahunPajak.Text = "2021";
            this.txttahunPajak.TextChanged += new System.EventHandler(this.txttahunPajak_TextChanged);
            // 
            // txtmasaPajak
            // 
            this.txtmasaPajak.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtmasaPajak.Location = new System.Drawing.Point(205, 123);
            this.txtmasaPajak.Name = "txtmasaPajak";
            this.txtmasaPajak.Size = new System.Drawing.Size(335, 20);
            this.txtmasaPajak.TabIndex = 87;
            this.txtmasaPajak.Text = "0909";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 302);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(99, 13);
            this.label11.TabIndex = 86;
            this.label11.Text = "Alamat Wajib Pajak";
            // 
            // txtalamatWajibPajak
            // 
            this.txtalamatWajibPajak.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtalamatWajibPajak.Location = new System.Drawing.Point(205, 302);
            this.txtalamatWajibPajak.Name = "txtalamatWajibPajak";
            this.txtalamatWajibPajak.Size = new System.Drawing.Size(335, 20);
            this.txtalamatWajibPajak.TabIndex = 85;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 276);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 13);
            this.label10.TabIndex = 84;
            this.label10.Text = "Nama Wajib Pajak";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 253);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(143, 13);
            this.label9.TabIndex = 83;
            this.label9.Text = "Nomor Wajib Pajak Penyetor";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 354);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 81;
            this.label4.Text = "N I K";
            // 
            // txtnamawajibpajak
            // 
            this.txtnamawajibpajak.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtnamawajibpajak.Location = new System.Drawing.Point(205, 276);
            this.txtnamawajibpajak.Name = "txtnamawajibpajak";
            this.txtnamawajibpajak.Size = new System.Drawing.Size(335, 20);
            this.txtnamawajibpajak.TabIndex = 80;
            // 
            // txtnomorPokokWajibPajakPenyetor
            // 
            this.txtnomorPokokWajibPajakPenyetor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtnomorPokokWajibPajakPenyetor.Location = new System.Drawing.Point(205, 250);
            this.txtnomorPokokWajibPajakPenyetor.Name = "txtnomorPokokWajibPajakPenyetor";
            this.txtnomorPokokWajibPajakPenyetor.Size = new System.Drawing.Size(335, 20);
            this.txtnomorPokokWajibPajakPenyetor.TabIndex = 79;
            this.txtnomorPokokWajibPajakPenyetor.Text = "147542823701000";
            this.txtnomorPokokWajibPajakPenyetor.TextChanged += new System.EventHandler(this.txtnomorPokokWajibPajakPenyetor_TextChanged);
            this.txtnomorPokokWajibPajakPenyetor.Leave += new System.EventHandler(this.txtnomorPokokWajibPajakPenyetor_Leave);
            // 
            // txtnik
            // 
            this.txtnik.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtnik.Location = new System.Drawing.Point(205, 354);
            this.txtnik.Name = "txtnik";
            this.txtnik.Size = new System.Drawing.Size(335, 20);
            this.txtnik.TabIndex = 77;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(13, 492);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(64, 13);
            this.label19.TabIndex = 106;
            this.label19.Text = "Nomor SPM";
            // 
            // txtnomorSPM
            // 
            this.txtnomorSPM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtnomorSPM.Location = new System.Drawing.Point(205, 492);
            this.txtnomorSPM.Name = "txtnomorSPM";
            this.txtnomorSPM.Size = new System.Drawing.Size(335, 20);
            this.txtnomorSPM.TabIndex = 105;
            this.txtnomorSPM.Text = "1234567890123456";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 469);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 104;
            this.label7.Text = "Nomor SKPD";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 443);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 13);
            this.label5.TabIndex = 103;
            this.label5.Text = "Nomor Faktur Pajak";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 414);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 102;
            this.label6.Text = "NIK Rekanan";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 388);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(179, 13);
            this.label12.TabIndex = 101;
            this.label12.Text = "Nomor Pokok Wajib Pajak Rekanan";
            // 
            // txtnomorSKPD
            // 
            this.txtnomorSKPD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtnomorSKPD.Location = new System.Drawing.Point(205, 466);
            this.txtnomorSKPD.Name = "txtnomorSKPD";
            this.txtnomorSKPD.Size = new System.Drawing.Size(335, 20);
            this.txtnomorSKPD.TabIndex = 100;
            this.txtnomorSKPD.Text = "1234567890123456";
            // 
            // txtnomorFakturPajak
            // 
            this.txtnomorFakturPajak.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtnomorFakturPajak.Location = new System.Drawing.Point(205, 440);
            this.txtnomorFakturPajak.Name = "txtnomorFakturPajak";
            this.txtnomorFakturPajak.Size = new System.Drawing.Size(335, 20);
            this.txtnomorFakturPajak.TabIndex = 99;
            this.txtnomorFakturPajak.TextChanged += new System.EventHandler(this.txtnomorFakturPajak_TextChanged);
            // 
            // txtnikRekanan
            // 
            this.txtnikRekanan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtnikRekanan.Location = new System.Drawing.Point(205, 414);
            this.txtnikRekanan.Name = "txtnikRekanan";
            this.txtnikRekanan.Size = new System.Drawing.Size(335, 20);
            this.txtnikRekanan.TabIndex = 98;
            this.txtnikRekanan.TextChanged += new System.EventHandler(this.txtnikRekanan_TextChanged);
            // 
            // txtnomorPokokWajibPajakRekanan
            // 
            this.txtnomorPokokWajibPajakRekanan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtnomorPokokWajibPajakRekanan.Location = new System.Drawing.Point(205, 388);
            this.txtnomorPokokWajibPajakRekanan.Name = "txtnomorPokokWajibPajakRekanan";
            this.txtnomorPokokWajibPajakRekanan.Size = new System.Drawing.Size(335, 20);
            this.txtnomorPokokWajibPajakRekanan.TabIndex = 97;
            this.txtnomorPokokWajibPajakRekanan.TextChanged += new System.EventHandler(this.txtnomorPokokWajibPajakRekanan_TextChanged);
            // 
            // cmdCekNPWP
            // 
            this.cmdCekNPWP.Location = new System.Drawing.Point(561, 85);
            this.cmdCekNPWP.Name = "cmdCekNPWP";
            this.cmdCekNPWP.Size = new System.Drawing.Size(77, 26);
            this.cmdCekNPWP.TabIndex = 107;
            this.cmdCekNPWP.Text = "Cek NPWP";
            this.cmdCekNPWP.UseVisualStyleBackColor = true;
            this.cmdCekNPWP.Click += new System.EventHandler(this.cmdCekNPWP_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(546, 227);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 13);
            this.label13.TabIndex = 108;
            this.label13.Text = "15 digit";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(547, 201);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(41, 13);
            this.label20.TabIndex = 109;
            this.label20.Text = "18 digit";
            this.label20.Click += new System.EventHandler(this.label20_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(547, 257);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(41, 13);
            this.label21.TabIndex = 110;
            this.label21.Text = "15 digit";
            this.label21.Click += new System.EventHandler(this.label21_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(547, 395);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 13);
            this.label22.TabIndex = 111;
            this.label22.Text = "15 digit";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(549, 417);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(41, 13);
            this.label23.TabIndex = 112;
            this.label23.Text = "16 digit";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(549, 443);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(41, 13);
            this.label24.TabIndex = 113;
            this.label24.Text = "18 digit";
            // 
            // cmdInquiryMPN
            // 
            this.cmdInquiryMPN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdInquiryMPN.Location = new System.Drawing.Point(541, 527);
            this.cmdInquiryMPN.Name = "cmdInquiryMPN";
            this.cmdInquiryMPN.Size = new System.Drawing.Size(65, 32);
            this.cmdInquiryMPN.TabIndex = 114;
            this.cmdInquiryMPN.Text = "MPN";
            this.cmdInquiryMPN.UseVisualStyleBackColor = true;
            this.cmdInquiryMPN.Visible = false;
            this.cmdInquiryMPN.Click += new System.EventHandler(this.cmdInquiryMPN_Click);
            // 
            // cmdTutup
            // 
            this.cmdTutup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdTutup.Location = new System.Drawing.Point(655, 441);
            this.cmdTutup.Name = "cmdTutup";
            this.cmdTutup.Size = new System.Drawing.Size(122, 41);
            this.cmdTutup.TabIndex = 115;
            this.cmdTutup.Text = "Tutup";
            this.cmdTutup.UseVisualStyleBackColor = true;
            this.cmdTutup.Visible = false;
            this.cmdTutup.Click += new System.EventHandler(this.cmdTutup_Click);
            // 
            // cmdTest
            // 
            this.cmdTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTest.Location = new System.Drawing.Point(409, 524);
            this.cmdTest.Name = "cmdTest";
            this.cmdTest.Size = new System.Drawing.Size(113, 32);
            this.cmdTest.TabIndex = 116;
            this.cmdTest.Text = "Buat IDBilling";
            this.cmdTest.UseVisualStyleBackColor = true;
            this.cmdTest.Click += new System.EventHandler(this.cmdTest_Click);
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(777, 41);
            this.ctrlHeader1.TabIndex = 0;
            // 
            // cmdGenerateReport
            // 
            this.cmdGenerateReport.Location = new System.Drawing.Point(655, 529);
            this.cmdGenerateReport.Name = "cmdGenerateReport";
            this.cmdGenerateReport.Size = new System.Drawing.Size(97, 30);
            this.cmdGenerateReport.TabIndex = 117;
            this.cmdGenerateReport.Text = "Generate Repot";
            this.cmdGenerateReport.UseVisualStyleBackColor = true;
            this.cmdGenerateReport.Click += new System.EventHandler(this.button1_Click);
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(205, 5);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(539, 21);
            this.ctrlDinas1.TabIndex = 118;
            this.ctrlDinas1.OnChanged += new KUAPPAS.ctrlDinas.ValueChangedEventHandler(this.ctrlDinas1_OnChanged);
            this.ctrlDinas1.Load += new System.EventHandler(this.ctrlDinas1_Load);
            // 
            // ctrlKodeSetor1
            // 
            this.ctrlKodeSetor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlKodeSetor1.Location = new System.Drawing.Point(205, 73);
            this.ctrlKodeSetor1.Name = "ctrlKodeSetor1";
            this.ctrlKodeSetor1.Size = new System.Drawing.Size(335, 23);
            this.ctrlKodeSetor1.TabIndex = 2;
            this.ctrlKodeSetor1.OnChanged += new KUAPPAS.Bendahara.ctrlKodeSetor.ValueChangedEventHandler(this.ctrlKodeSetor1_OnChanged);
            this.ctrlKodeSetor1.Load += new System.EventHandler(this.ctrlKodeSetor1_Load);
            // 
            // ctrlPotongan1
            // 
            this.ctrlPotongan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlPotongan1.Location = new System.Drawing.Point(205, 47);
            this.ctrlPotongan1.Name = "ctrlPotongan1";
            this.ctrlPotongan1.Size = new System.Drawing.Size(335, 22);
            this.ctrlPotongan1.TabIndex = 1;
            this.ctrlPotongan1.OnChanged += new KUAPPAS.Bendahara.ctrlPilihanPotongan.ValueChangedEventHandler(this.ctrlPotongan1_OnChanged);
            this.ctrlPotongan1.Load += new System.EventHandler(this.ctrlPotongan1_Load);
            // 
            // frmTambahPajak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 595);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.cmdGenerateReport);
            this.Controls.Add(this.cmdTest);
            this.Controls.Add(this.cmdTutup);
            this.Controls.Add(this.cmdInquiryMPN);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cmdCekNPWP);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtnomorSPM);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtnomorSKPD);
            this.Controls.Add(this.txtnomorFakturPajak);
            this.Controls.Add(this.txtnikRekanan);
            this.Controls.Add(this.txtnomorPokokWajibPajakRekanan);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtkota);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtnomorObjekPajak);
            this.Controls.Add(this.txtnomorSk);
            this.Controls.Add(this.txttahunPajak);
            this.Controls.Add(this.txtmasaPajak);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtalamatWajibPajak);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtnamawajibpajak);
            this.Controls.Add(this.txtnomorPokokWajibPajakPenyetor);
            this.Controls.Add(this.txtnik);
            this.Controls.Add(this.xmsBatal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.ctrlKodeSetor1);
            this.Controls.Add(this.ctrlPotongan1);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmTambahPajak";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Penambahan Kode Pajak/Potongan";
            this.Load += new System.EventHandler(this.frmTambahPajak_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlHeader ctrlHeader1;
        private ctrlPilihanPotongan ctrlPotongan1;
        private ctrlKodeSetor ctrlKodeSetor1;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button xmsBatal;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtkota;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtnomorObjekPajak;
        private System.Windows.Forms.TextBox txtnomorSk;
        private System.Windows.Forms.TextBox txttahunPajak;
        private System.Windows.Forms.TextBox txtmasaPajak;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtalamatWajibPajak;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtnamawajibpajak;
        private System.Windows.Forms.TextBox txtnomorPokokWajibPajakPenyetor;
        private System.Windows.Forms.TextBox txtnik;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtnomorSPM;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtnomorSKPD;
        private System.Windows.Forms.TextBox txtnomorFakturPajak;
        private System.Windows.Forms.TextBox txtnikRekanan;
        private System.Windows.Forms.TextBox txtnomorPokokWajibPajakRekanan;
        private System.Windows.Forms.Button cmdCekNPWP;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button cmdInquiryMPN;
        private System.Windows.Forms.Button cmdTutup;
        private System.Windows.Forms.Button cmdTest;
        private System.Windows.Forms.Button cmdGenerateReport;
        private ctrlDinas ctrlDinas1;

    }
}