namespace KUAPPAS
{
    partial class frmPejabatDetail
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBank = new System.Windows.Forms.GroupBox();
            this.ctrlDaftarBank1 = new KUAPPAS.Bendahara.ctrlDaftarBank();
            this.txtNoNPWP = new System.Windows.Forms.TextBox();
            this.txtNoRekening = new System.Windows.Forms.TextBox();
            this.NPWP = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNamaDalamBank = new System.Windows.Forms.TextBox();
            this.cmdCekNamaDalamRekening = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ctrlJabatan1 = new KUAPPAS.ctrlJabatan();
            this.txtNamaJabatan = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ctrlTanggal1 = new KUAPPAS.ctrlTanggal();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNIP = new System.Windows.Forms.TextBox();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ctrlNavigation1 = new KUAPPAS.ctrlNavigation();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBank.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBank);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.ctrlJabatan1);
            this.groupBox2.Controls.Add(this.txtNamaJabatan);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.ctrlTanggal1);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtNIP);
            this.groupBox2.Controls.Add(this.txtNama);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 106);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(702, 391);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // groupBank
            // 
            this.groupBank.Controls.Add(this.ctrlDaftarBank1);
            this.groupBank.Controls.Add(this.txtNoNPWP);
            this.groupBank.Controls.Add(this.txtNoRekening);
            this.groupBank.Controls.Add(this.NPWP);
            this.groupBank.Controls.Add(this.label2);
            this.groupBank.Controls.Add(this.label1);
            this.groupBank.Controls.Add(this.txtNamaDalamBank);
            this.groupBank.Controls.Add(this.cmdCekNamaDalamRekening);
            this.groupBank.Controls.Add(this.label20);
            this.groupBank.Location = new System.Drawing.Point(12, 221);
            this.groupBank.Name = "groupBank";
            this.groupBank.Size = new System.Drawing.Size(677, 179);
            this.groupBank.TabIndex = 48;
            this.groupBank.TabStop = false;
            this.groupBank.Text = "Data Bank";
            // 
            // ctrlDaftarBank1
            // 
            this.ctrlDaftarBank1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlDaftarBank1.Location = new System.Drawing.Point(229, 20);
            this.ctrlDaftarBank1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.ctrlDaftarBank1.Name = "ctrlDaftarBank1";
            this.ctrlDaftarBank1.Size = new System.Drawing.Size(413, 23);
            this.ctrlDaftarBank1.TabIndex = 37;
            // 
            // txtNoNPWP
            // 
            this.txtNoNPWP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoNPWP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoNPWP.Location = new System.Drawing.Point(230, 92);
            this.txtNoNPWP.Name = "txtNoNPWP";
            this.txtNoNPWP.Size = new System.Drawing.Size(412, 21);
            this.txtNoNPWP.TabIndex = 31;
            // 
            // txtNoRekening
            // 
            this.txtNoRekening.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoRekening.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoRekening.Location = new System.Drawing.Point(230, 45);
            this.txtNoRekening.Name = "txtNoRekening";
            this.txtNoRekening.Size = new System.Drawing.Size(412, 21);
            this.txtNoRekening.TabIndex = 33;
            // 
            // NPWP
            // 
            this.NPWP.AutoSize = true;
            this.NPWP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NPWP.Location = new System.Drawing.Point(10, 93);
            this.NPWP.Name = "NPWP";
            this.NPWP.Size = new System.Drawing.Size(43, 15);
            this.NPWP.TabIndex = 34;
            this.NPWP.Text = "NPWP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 35;
            this.label2.Text = "Bank ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 15);
            this.label1.TabIndex = 36;
            this.label1.Text = "Nomor Rekeing";
            // 
            // txtNamaDalamBank
            // 
            this.txtNamaDalamBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNamaDalamBank.Location = new System.Drawing.Point(230, 68);
            this.txtNamaDalamBank.Name = "txtNamaDalamBank";
            this.txtNamaDalamBank.Size = new System.Drawing.Size(412, 21);
            this.txtNamaDalamBank.TabIndex = 39;
            // 
            // cmdCekNamaDalamRekening
            // 
            this.cmdCekNamaDalamRekening.Location = new System.Drawing.Point(229, 114);
            this.cmdCekNamaDalamRekening.Name = "cmdCekNamaDalamRekening";
            this.cmdCekNamaDalamRekening.Size = new System.Drawing.Size(211, 52);
            this.cmdCekNamaDalamRekening.TabIndex = 41;
            this.cmdCekNamaDalamRekening.Text = "Cek Nama dlm Rek Bank";
            this.cmdCekNamaDalamRekening.UseVisualStyleBackColor = true;
            this.cmdCekNamaDalamRekening.Click += new System.EventHandler(this.cmdCekNamaDalamRekening_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(10, 68);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(137, 15);
            this.label20.TabIndex = 40;
            this.label20.Text = "Nama Dalam Rek Bank";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(23, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 15);
            this.label6.TabIndex = 47;
            this.label6.Text = "Jabatan";
            // 
            // ctrlJabatan1
            // 
            this.ctrlJabatan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlJabatan1.ID = 0;
            this.ctrlJabatan1.Location = new System.Drawing.Point(243, 20);
            this.ctrlJabatan1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ctrlJabatan1.Name = "ctrlJabatan1";
            this.ctrlJabatan1.Size = new System.Drawing.Size(412, 22);
            this.ctrlJabatan1.TabIndex = 46;
            // 
            // txtNamaJabatan
            // 
            this.txtNamaJabatan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNamaJabatan.Location = new System.Drawing.Point(242, 96);
            this.txtNamaJabatan.Name = "txtNamaJabatan";
            this.txtNamaJabatan.Size = new System.Drawing.Size(412, 21);
            this.txtNamaJabatan.TabIndex = 45;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(23, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 15);
            this.label4.TabIndex = 44;
            this.label4.Text = "Nama Jabatan";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 15);
            this.label3.TabIndex = 43;
            this.label3.Text = "Tanggal Mulai Menjabat";
            // 
            // ctrlTanggal1
            // 
            this.ctrlTanggal1.Location = new System.Drawing.Point(242, 124);
            this.ctrlTanggal1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ctrlTanggal1.Name = "ctrlTanggal1";
            this.ctrlTanggal1.Size = new System.Drawing.Size(412, 25);
            this.ctrlTanggal1.TabIndex = 42;
            this.ctrlTanggal1.Tanggal = new System.DateTime(2023, 11, 12, 0, 0, 0, 0);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(23, 75);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "NIP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(23, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Nama";
            // 
            // txtNIP
            // 
            this.txtNIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNIP.Location = new System.Drawing.Point(242, 71);
            this.txtNIP.Name = "txtNIP";
            this.txtNIP.Size = new System.Drawing.Size(412, 21);
            this.txtNIP.TabIndex = 1;
            // 
            // txtNama
            // 
            this.txtNama.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNama.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNama.Location = new System.Drawing.Point(242, 48);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(412, 21);
            this.txtNama.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 500);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(702, 22);
            this.statusStrip1.TabIndex = 20;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ctrlNavigation1
            // 
            this.ctrlNavigation1.BackColor = System.Drawing.Color.DimGray;
            this.ctrlNavigation1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlNavigation1.Location = new System.Drawing.Point(0, 0);
            this.ctrlNavigation1.Name = "ctrlNavigation1";
            this.ctrlNavigation1.Size = new System.Drawing.Size(702, 46);
            this.ctrlNavigation1.TabIndex = 21;
            this.ctrlNavigation1.OnAdd += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnAdd);
            this.ctrlNavigation1.OnSave += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnSave);
            this.ctrlNavigation1.OnDelete += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnDelete);
            this.ctrlNavigation1.Load += new System.EventHandler(this.ctrlNavigation1_Load);
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(241, 53);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(414, 47);
            this.ctrlDinas1.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "OPD";
            // 
            // frmPejabatDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 522);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.ctrlNavigation1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Name = "frmPejabatDetail";
            this.Text = "Detail Pejabat";
            this.Load += new System.EventHandler(this.frmPejabatDetail_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBank.ResumeLayout(false);
            this.groupBank.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdCekNamaDalamRekening;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtNamaDalamBank;
        private Bendahara.ctrlDaftarBank ctrlDaftarBank1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label NPWP;
        private System.Windows.Forms.TextBox txtNoRekening;
        private System.Windows.Forms.TextBox txtNoNPWP;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNIP;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.TextBox txtNamaJabatan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private ctrlTanggal ctrlTanggal1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private ctrlNavigation ctrlNavigation1;
        private System.Windows.Forms.Label label6;
        private ctrlJabatan ctrlJabatan1;
        private System.Windows.Forms.GroupBox groupBank;
        private ctrlDinas ctrlDinas1;
        private System.Windows.Forms.Label label7;
    }
}