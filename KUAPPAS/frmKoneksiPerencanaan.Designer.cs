namespace KUAPPAS
{
    partial class frmKoneksiPerencanaan
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
            this.Perencanaan = new System.Windows.Forms.GroupBox();
            this.cmdSimpan = new System.Windows.Forms.Button();
            this.cmdTestConnection = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDatabaseAset = new System.Windows.Forms.TextBox();
            this.txtUserIDAset = new System.Windows.Forms.TextBox();
            this.txtServerAset = new System.Windows.Forms.TextBox();
            this.PortServerAset = new System.Windows.Forms.TextBox();
            this.txtPasswordAset = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUserIDInternet = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtServerInternet = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtPasswordInternet = new System.Windows.Forms.TextBox();
            this.txtDatabaseInternet = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtDatabaseAsetInternet = new System.Windows.Forms.TextBox();
            this.txtUserIDAsetInternet = new System.Windows.Forms.TextBox();
            this.txtServerAsetInternet = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.txtPasswordAsetInternet = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.Perencanaan.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Perencanaan
            // 
            this.Perencanaan.Controls.Add(this.label22);
            this.Perencanaan.Controls.Add(this.label21);
            this.Perencanaan.Controls.Add(this.textBox1);
            this.Perencanaan.Controls.Add(this.label1);
            this.Perencanaan.Controls.Add(this.txtUserIDInternet);
            this.Perencanaan.Controls.Add(this.label12);
            this.Perencanaan.Controls.Add(this.label13);
            this.Perencanaan.Controls.Add(this.txtServerInternet);
            this.Perencanaan.Controls.Add(this.label14);
            this.Perencanaan.Controls.Add(this.label15);
            this.Perencanaan.Controls.Add(this.txtPasswordInternet);
            this.Perencanaan.Controls.Add(this.txtDatabaseInternet);
            this.Perencanaan.Controls.Add(this.cmdSimpan);
            this.Perencanaan.Controls.Add(this.cmdTestConnection);
            this.Perencanaan.Controls.Add(this.txtPort);
            this.Perencanaan.Controls.Add(this.label6);
            this.Perencanaan.Controls.Add(this.txtUserID);
            this.Perencanaan.Controls.Add(this.label5);
            this.Perencanaan.Controls.Add(this.label4);
            this.Perencanaan.Controls.Add(this.txtServer);
            this.Perencanaan.Controls.Add(this.label3);
            this.Perencanaan.Controls.Add(this.label2);
            this.Perencanaan.Controls.Add(this.txtPassword);
            this.Perencanaan.Controls.Add(this.txtDatabase);
            this.Perencanaan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Perencanaan.Location = new System.Drawing.Point(19, 53);
            this.Perencanaan.Name = "Perencanaan";
            this.Perencanaan.Size = new System.Drawing.Size(693, 258);
            this.Perencanaan.TabIndex = 37;
            this.Perencanaan.TabStop = false;
            this.Perencanaan.Text = "Perencanaan";
            this.Perencanaan.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // cmdSimpan
            // 
            this.cmdSimpan.Image = global::KUAPPAS.Properties.Resources.action_save;
            this.cmdSimpan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSimpan.Location = new System.Drawing.Point(516, 209);
            this.cmdSimpan.Name = "cmdSimpan";
            this.cmdSimpan.Size = new System.Drawing.Size(84, 28);
            this.cmdSimpan.TabIndex = 36;
            this.cmdSimpan.Text = "Simpan";
            this.cmdSimpan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSimpan.UseVisualStyleBackColor = true;
            this.cmdSimpan.Click += new System.EventHandler(this.cmdSimpan_Click);
            // 
            // cmdTestConnection
            // 
            this.cmdTestConnection.Image = global::KUAPPAS.Properties.Resources.action_refresh;
            this.cmdTestConnection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdTestConnection.Location = new System.Drawing.Point(552, 119);
            this.cmdTestConnection.Name = "cmdTestConnection";
            this.cmdTestConnection.Size = new System.Drawing.Size(106, 28);
            this.cmdTestConnection.TabIndex = 29;
            this.cmdTestConnection.Text = "Test Koneksi";
            this.cmdTestConnection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdTestConnection.UseVisualStyleBackColor = true;
            this.cmdTestConnection.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(121, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 15);
            this.label6.TabIndex = 35;
            this.label6.Text = "Database";
            // 
            // txtUserID
            // 
            this.txtUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserID.Location = new System.Drawing.Point(255, 46);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(239, 21);
            this.txtUserID.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(121, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 15);
            this.label5.TabIndex = 34;
            this.label5.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(121, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 15);
            this.label4.TabIndex = 33;
            this.label4.Text = "User ID";
            // 
            // txtServer
            // 
            this.txtServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServer.Location = new System.Drawing.Point(255, 22);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(143, 21);
            this.txtServer.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(404, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 15);
            this.label3.TabIndex = 32;
            this.label3.Text = "Port";
            // 
            // txtPort
            // 
            this.txtPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPort.Location = new System.Drawing.Point(436, 19);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(58, 21);
            this.txtPort.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(121, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 15);
            this.label2.TabIndex = 31;
            this.label2.Text = "Server";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(255, 70);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(239, 21);
            this.txtPassword.TabIndex = 27;
            // 
            // txtDatabase
            // 
            this.txtDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDatabase.Location = new System.Drawing.Point(255, 93);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(239, 21);
            this.txtDatabase.TabIndex = 28;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(743, 47);
            this.ctrlHeader1.TabIndex = 38;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 582);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(743, 22);
            this.statusStrip1.TabIndex = 39;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.txtDatabaseAsetInternet);
            this.groupBox2.Controls.Add(this.txtUserIDAsetInternet);
            this.groupBox2.Controls.Add(this.txtServerAsetInternet);
            this.groupBox2.Controls.Add(this.textBox5);
            this.groupBox2.Controls.Add(this.txtPasswordAsetInternet);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtDatabaseAset);
            this.groupBox2.Controls.Add(this.txtUserIDAset);
            this.groupBox2.Controls.Add(this.txtServerAset);
            this.groupBox2.Controls.Add(this.PortServerAset);
            this.groupBox2.Controls.Add(this.txtPasswordAset);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(19, 317);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(693, 262);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Koneksi Ke Server  Aset";
            // 
            // button1
            // 
            this.button1.Image = global::KUAPPAS.Properties.Resources.action_save;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(509, 213);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 28);
            this.button1.TabIndex = 60;
            this.button1.Text = "Simpan";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(113, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 15);
            this.label7.TabIndex = 59;
            this.label7.Text = "Database";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(114, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 15);
            this.label8.TabIndex = 58;
            this.label8.Text = "Password";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(114, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 15);
            this.label9.TabIndex = 57;
            this.label9.Text = "User ID";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(394, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 15);
            this.label10.TabIndex = 56;
            this.label10.Text = "Port";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(114, 26);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 15);
            this.label11.TabIndex = 55;
            this.label11.Text = "Server";
            // 
            // txtDatabaseAset
            // 
            this.txtDatabaseAset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDatabaseAset.Location = new System.Drawing.Point(249, 102);
            this.txtDatabaseAset.Name = "txtDatabaseAset";
            this.txtDatabaseAset.Size = new System.Drawing.Size(238, 21);
            this.txtDatabaseAset.TabIndex = 54;
            // 
            // txtUserIDAset
            // 
            this.txtUserIDAset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserIDAset.Location = new System.Drawing.Point(249, 50);
            this.txtUserIDAset.Name = "txtUserIDAset";
            this.txtUserIDAset.Size = new System.Drawing.Size(238, 21);
            this.txtUserIDAset.TabIndex = 51;
            // 
            // txtServerAset
            // 
            this.txtServerAset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServerAset.Location = new System.Drawing.Point(249, 26);
            this.txtServerAset.Name = "txtServerAset";
            this.txtServerAset.Size = new System.Drawing.Size(142, 21);
            this.txtServerAset.TabIndex = 50;
            // 
            // PortServerAset
            // 
            this.PortServerAset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PortServerAset.Location = new System.Drawing.Point(429, 26);
            this.PortServerAset.Name = "PortServerAset";
            this.PortServerAset.Size = new System.Drawing.Size(58, 21);
            this.PortServerAset.TabIndex = 52;
            // 
            // txtPasswordAset
            // 
            this.txtPasswordAset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPasswordAset.Location = new System.Drawing.Point(249, 76);
            this.txtPasswordAset.Name = "txtPasswordAset";
            this.txtPasswordAset.PasswordChar = '*';
            this.txtPasswordAset.Size = new System.Drawing.Size(238, 21);
            this.txtPasswordAset.TabIndex = 53;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(436, 139);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(58, 21);
            this.textBox1.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(121, 219);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 46;
            this.label1.Text = "Database";
            // 
            // txtUserIDInternet
            // 
            this.txtUserIDInternet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserIDInternet.Location = new System.Drawing.Point(255, 166);
            this.txtUserIDInternet.Name = "txtUserIDInternet";
            this.txtUserIDInternet.Size = new System.Drawing.Size(239, 21);
            this.txtUserIDInternet.TabIndex = 38;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(121, 190);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 15);
            this.label12.TabIndex = 45;
            this.label12.Text = "Password";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(121, 166);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 15);
            this.label13.TabIndex = 44;
            this.label13.Text = "User ID";
            // 
            // txtServerInternet
            // 
            this.txtServerInternet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServerInternet.Location = new System.Drawing.Point(255, 142);
            this.txtServerInternet.Name = "txtServerInternet";
            this.txtServerInternet.Size = new System.Drawing.Size(143, 21);
            this.txtServerInternet.TabIndex = 37;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(404, 142);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 15);
            this.label14.TabIndex = 43;
            this.label14.Text = "Port";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(121, 142);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(42, 15);
            this.label15.TabIndex = 42;
            this.label15.Text = "Server";
            // 
            // txtPasswordInternet
            // 
            this.txtPasswordInternet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPasswordInternet.Location = new System.Drawing.Point(255, 190);
            this.txtPasswordInternet.Name = "txtPasswordInternet";
            this.txtPasswordInternet.PasswordChar = '*';
            this.txtPasswordInternet.Size = new System.Drawing.Size(239, 21);
            this.txtPasswordInternet.TabIndex = 40;
            // 
            // txtDatabaseInternet
            // 
            this.txtDatabaseInternet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDatabaseInternet.Location = new System.Drawing.Point(255, 213);
            this.txtDatabaseInternet.Name = "txtDatabaseInternet";
            this.txtDatabaseInternet.Size = new System.Drawing.Size(239, 21);
            this.txtDatabaseInternet.TabIndex = 41;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(113, 220);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(60, 15);
            this.label16.TabIndex = 70;
            this.label16.Text = "Database";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(114, 194);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(61, 15);
            this.label17.TabIndex = 69;
            this.label17.Text = "Password";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(114, 168);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(48, 15);
            this.label18.TabIndex = 68;
            this.label18.Text = "User ID";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(394, 147);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(29, 15);
            this.label19.TabIndex = 67;
            this.label19.Text = "Port";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(114, 144);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(42, 15);
            this.label20.TabIndex = 66;
            this.label20.Text = "Server";
            // 
            // txtDatabaseAsetInternet
            // 
            this.txtDatabaseAsetInternet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDatabaseAsetInternet.Location = new System.Drawing.Point(249, 220);
            this.txtDatabaseAsetInternet.Name = "txtDatabaseAsetInternet";
            this.txtDatabaseAsetInternet.Size = new System.Drawing.Size(238, 21);
            this.txtDatabaseAsetInternet.TabIndex = 65;
            // 
            // txtUserIDAsetInternet
            // 
            this.txtUserIDAsetInternet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserIDAsetInternet.Location = new System.Drawing.Point(249, 168);
            this.txtUserIDAsetInternet.Name = "txtUserIDAsetInternet";
            this.txtUserIDAsetInternet.Size = new System.Drawing.Size(238, 21);
            this.txtUserIDAsetInternet.TabIndex = 62;
            // 
            // txtServerAsetInternet
            // 
            this.txtServerAsetInternet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServerAsetInternet.Location = new System.Drawing.Point(249, 144);
            this.txtServerAsetInternet.Name = "txtServerAsetInternet";
            this.txtServerAsetInternet.Size = new System.Drawing.Size(142, 21);
            this.txtServerAsetInternet.TabIndex = 61;
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.Location = new System.Drawing.Point(429, 144);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(58, 21);
            this.textBox5.TabIndex = 63;
            // 
            // txtPasswordAsetInternet
            // 
            this.txtPasswordAsetInternet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPasswordAsetInternet.Location = new System.Drawing.Point(249, 194);
            this.txtPasswordAsetInternet.Name = "txtPasswordAsetInternet";
            this.txtPasswordAsetInternet.PasswordChar = '*';
            this.txtPasswordAsetInternet.Size = new System.Drawing.Size(238, 21);
            this.txtPasswordAsetInternet.TabIndex = 64;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(32, 142);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(75, 15);
            this.label21.TabIndex = 47;
            this.label21.Text = "INTERNET";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(29, 25);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(74, 15);
            this.label22.TabIndex = 48;
            this.label22.Text = "JARINGAN";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(16, 31);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(84, 16);
            this.label23.TabIndex = 72;
            this.label23.Text = "JARINGAN";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(19, 148);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(85, 16);
            this.label24.TabIndex = 71;
            this.label24.Text = "INTERNET";
            // 
            // frmKoneksiPerencanaan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 604);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.Perencanaan);
            this.Name = "frmKoneksiPerencanaan";
            this.Text = "frmKoneksiPerencanaan";
            this.Load += new System.EventHandler(this.frmKoneksiPerencanaan_Load);
            this.Perencanaan.ResumeLayout(false);
            this.Perencanaan.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox Perencanaan;
        private System.Windows.Forms.Button cmdSimpan;
        private System.Windows.Forms.Button cmdTestConnection;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtDatabase;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDatabaseAset;
        private System.Windows.Forms.TextBox txtUserIDAset;
        private System.Windows.Forms.TextBox txtServerAset;
        private System.Windows.Forms.TextBox PortServerAset;
        private System.Windows.Forms.TextBox txtPasswordAset;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserIDInternet;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtServerInternet;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtPasswordInternet;
        private System.Windows.Forms.TextBox txtDatabaseInternet;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtDatabaseAsetInternet;
        private System.Windows.Forms.TextBox txtUserIDAsetInternet;
        private System.Windows.Forms.TextBox txtServerAsetInternet;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox txtPasswordAsetInternet;
    }
}