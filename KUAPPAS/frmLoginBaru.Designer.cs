namespace KUAPPAS
{
    partial class frmLoginBaru
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
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.rbCloud = new System.Windows.Forms.RadioButton();
            this.rbInternet = new System.Windows.Forms.RadioButton();
            this.rbJaringan = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbTahunAnggaran = new System.Windows.Forms.ComboBox();
            this.cmdBatal = new System.Windows.Forms.Button();
            this.cmdLogin = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTahun = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Pelayanan = new System.Windows.Forms.Button();
            this.cmdVerifikator = new System.Windows.Forms.Button();
            this.cmdSP2DOnlie = new System.Windows.Forms.Button();
            this.cmdCetak = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRefresh.Location = new System.Drawing.Point(136, 224);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(75, 23);
            this.cmdRefresh.TabIndex = 28;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Visible = false;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // rbCloud
            // 
            this.rbCloud.AutoSize = true;
            this.rbCloud.BackColor = System.Drawing.Color.Transparent;
            this.rbCloud.Location = new System.Drawing.Point(30, 249);
            this.rbCloud.Name = "rbCloud";
            this.rbCloud.Size = new System.Drawing.Size(77, 17);
            this.rbCloud.TabIndex = 27;
            this.rbCloud.TabStop = true;
            this.rbCloud.Text = "Jalur Cloud";
            this.rbCloud.UseVisualStyleBackColor = false;
            this.rbCloud.Visible = false;
            // 
            // rbInternet
            // 
            this.rbInternet.AutoSize = true;
            this.rbInternet.BackColor = System.Drawing.Color.Transparent;
            this.rbInternet.Checked = true;
            this.rbInternet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbInternet.Location = new System.Drawing.Point(30, 226);
            this.rbInternet.Name = "rbInternet";
            this.rbInternet.Size = new System.Drawing.Size(100, 17);
            this.rbInternet.TabIndex = 26;
            this.rbInternet.TabStop = true;
            this.rbInternet.Text = "Jalur Internet";
            this.rbInternet.UseVisualStyleBackColor = false;
            this.rbInternet.Visible = false;
            // 
            // rbJaringan
            // 
            this.rbJaringan.AutoSize = true;
            this.rbJaringan.BackColor = System.Drawing.Color.Transparent;
            this.rbJaringan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbJaringan.Location = new System.Drawing.Point(28, 201);
            this.rbJaringan.Name = "rbJaringan";
            this.rbJaringan.Size = new System.Drawing.Size(104, 17);
            this.rbJaringan.TabIndex = 25;
            this.rbJaringan.Text = "Jalur Jaringan";
            this.rbJaringan.UseVisualStyleBackColor = false;
            this.rbJaringan.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(25, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Tahun Anggaran";
            // 
            // cmbTahunAnggaran
            // 
            this.cmbTahunAnggaran.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.cmbTahunAnggaran.FormattingEnabled = true;
            this.cmbTahunAnggaran.Location = new System.Drawing.Point(30, 92);
            this.cmbTahunAnggaran.Name = "cmbTahunAnggaran";
            this.cmbTahunAnggaran.Size = new System.Drawing.Size(119, 24);
            this.cmbTahunAnggaran.TabIndex = 23;
            this.cmbTahunAnggaran.SelectedIndexChanged += new System.EventHandler(this.cmbTahunAnggaran_SelectedIndexChanged);
            // 
            // cmdBatal
            // 
            this.cmdBatal.BackColor = System.Drawing.Color.White;
            this.cmdBatal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdBatal.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.cmdBatal.FlatAppearance.BorderSize = 0;
            this.cmdBatal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBatal.Location = new System.Drawing.Point(103, 267);
            this.cmdBatal.Name = "cmdBatal";
            this.cmdBatal.Size = new System.Drawing.Size(69, 43);
            this.cmdBatal.TabIndex = 22;
            this.cmdBatal.Text = "Batal";
            this.cmdBatal.UseVisualStyleBackColor = false;
            this.cmdBatal.Click += new System.EventHandler(this.cmdBatal_Click);
            // 
            // cmdLogin
            // 
            this.cmdLogin.BackColor = System.Drawing.Color.White;
            this.cmdLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdLogin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.cmdLogin.FlatAppearance.BorderSize = 0;
            this.cmdLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLogin.Location = new System.Drawing.Point(28, 267);
            this.cmdLogin.Name = "cmdLogin";
            this.cmdLogin.Size = new System.Drawing.Size(69, 43);
            this.cmdLogin.TabIndex = 21;
            this.cmdLogin.Text = "Login";
            this.cmdLogin.UseVisualStyleBackColor = false;
            this.cmdLogin.Click += new System.EventHandler(this.cmdLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Location = new System.Drawing.Point(30, 174);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(119, 20);
            this.txtPassword.TabIndex = 20;
            // 
            // txtUserID
            // 
            this.txtUserID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserID.Location = new System.Drawing.Point(30, 135);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(119, 20);
            this.txtUserID.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Nama User ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 26);
            this.label3.TabIndex = 29;
            this.label3.Text = "Sila Login..";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 25);
            this.button1.TabIndex = 30;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.MistyRose;
            this.label4.Location = new System.Drawing.Point(362, 326);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 28);
            this.label4.TabIndex = 31;
            this.label4.Text = "SIKUAT";
            // 
            // lblTahun
            // 
            this.lblTahun.AutoSize = true;
            this.lblTahun.BackColor = System.Drawing.Color.Transparent;
            this.lblTahun.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTahun.ForeColor = System.Drawing.Color.Coral;
            this.lblTahun.Location = new System.Drawing.Point(445, 326);
            this.lblTahun.Name = "lblTahun";
            this.lblTahun.Size = new System.Drawing.Size(26, 28);
            this.lblTahun.TabIndex = 32;
            this.lblTahun.Text = "..";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 337);
            this.progressBar1.MarqueeAnimationSpeed = 50;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(344, 10);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 33;
            this.progressBar1.Visible = false;
            // 
            // Pelayanan
            // 
            this.Pelayanan.Location = new System.Drawing.Point(287, 91);
            this.Pelayanan.Name = "Pelayanan";
            this.Pelayanan.Size = new System.Drawing.Size(104, 25);
            this.Pelayanan.TabIndex = 34;
            this.Pelayanan.Text = "Pelayanan";
            this.Pelayanan.UseVisualStyleBackColor = true;
            this.Pelayanan.Click += new System.EventHandler(this.Pelayanan_Click);
            // 
            // cmdVerifikator
            // 
            this.cmdVerifikator.Location = new System.Drawing.Point(289, 125);
            this.cmdVerifikator.Name = "cmdVerifikator";
            this.cmdVerifikator.Size = new System.Drawing.Size(101, 29);
            this.cmdVerifikator.TabIndex = 35;
            this.cmdVerifikator.Text = "Verifikator";
            this.cmdVerifikator.UseVisualStyleBackColor = true;
            this.cmdVerifikator.Visible = false;
            this.cmdVerifikator.Click += new System.EventHandler(this.cmdVerifikator_Click);
            // 
            // cmdSP2DOnlie
            // 
            this.cmdSP2DOnlie.Location = new System.Drawing.Point(291, 166);
            this.cmdSP2DOnlie.Name = "cmdSP2DOnlie";
            this.cmdSP2DOnlie.Size = new System.Drawing.Size(98, 35);
            this.cmdSP2DOnlie.TabIndex = 36;
            this.cmdSP2DOnlie.Text = "SP2DOnline";
            this.cmdSP2DOnlie.UseVisualStyleBackColor = true;
            this.cmdSP2DOnlie.Visible = false;
            this.cmdSP2DOnlie.Click += new System.EventHandler(this.cmdSP2DOnlie_Click);
            // 
            // cmdCetak
            // 
            this.cmdCetak.Location = new System.Drawing.Point(293, 211);
            this.cmdCetak.Name = "cmdCetak";
            this.cmdCetak.Size = new System.Drawing.Size(95, 31);
            this.cmdCetak.TabIndex = 37;
            this.cmdCetak.Text = "Cetak";
            this.cmdCetak.UseVisualStyleBackColor = true;
            this.cmdCetak.Visible = false;
            this.cmdCetak.Click += new System.EventHandler(this.cmdCetak_Click);
            // 
            // frmLoginBaru
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackgroundImage = global::KUAPPAS.Properties.Resources.Splach;
            this.ClientSize = new System.Drawing.Size(483, 360);
            this.Controls.Add(this.cmdCetak);
            this.Controls.Add(this.cmdSP2DOnlie);
            this.Controls.Add(this.cmdVerifikator);
            this.Controls.Add(this.Pelayanan);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblTahun);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.rbCloud);
            this.Controls.Add(this.rbInternet);
            this.Controls.Add(this.rbJaringan);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbTahunAnggaran);
            this.Controls.Add(this.cmdBatal);
            this.Controls.Add(this.cmdLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.HelpButton = true;
            this.Name = "frmLoginBaru";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Lagin";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLoginBaru_FormClosed);
            this.Load += new System.EventHandler(this.frmLoginBaru_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.RadioButton rbCloud;
        private System.Windows.Forms.RadioButton rbInternet;
        private System.Windows.Forms.RadioButton rbJaringan;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbTahunAnggaran;
        private System.Windows.Forms.Button cmdBatal;
        private System.Windows.Forms.Button cmdLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTahun;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button Pelayanan;
        private System.Windows.Forms.Button cmdVerifikator;
        private System.Windows.Forms.Button cmdSP2DOnlie;
        private System.Windows.Forms.Button cmdCetak;
    }
}