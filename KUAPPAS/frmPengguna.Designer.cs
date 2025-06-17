namespace KUAPPAS
{
    partial class frmPengguna
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
            this.label1 = new System.Windows.Forms.Label();
            this.chkSemuaDinas = new System.Windows.Forms.CheckBox();
            this.cmdLoadData = new System.Windows.Forms.Button();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.cmdTambah = new System.Windows.Forms.Button();
            this.cmdCariLagi = new System.Windows.Forms.Button();
            this.cmdCari = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.gridPengguna = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detail = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NIK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Otoritas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RefreshPasword = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlOtoRitas1 = new KUAPPAS.ctrlOtoRitas();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridPengguna)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "OPD";
            // 
            // chkSemuaDinas
            // 
            this.chkSemuaDinas.AutoSize = true;
            this.chkSemuaDinas.Location = new System.Drawing.Point(110, 59);
            this.chkSemuaDinas.Name = "chkSemuaDinas";
            this.chkSemuaDinas.Size = new System.Drawing.Size(89, 17);
            this.chkSemuaDinas.TabIndex = 29;
            this.chkSemuaDinas.Text = "Semua Dinas";
            this.chkSemuaDinas.UseVisualStyleBackColor = true;
            // 
            // cmdLoadData
            // 
            this.cmdLoadData.BackColor = System.Drawing.Color.Khaki;
            this.cmdLoadData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdLoadData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdLoadData.Location = new System.Drawing.Point(13, 166);
            this.cmdLoadData.Name = "cmdLoadData";
            this.cmdLoadData.Size = new System.Drawing.Size(136, 32);
            this.cmdLoadData.TabIndex = 28;
            this.cmdLoadData.Text = "Panggil Data";
            this.cmdLoadData.UseVisualStyleBackColor = false;
            this.cmdLoadData.Click += new System.EventHandler(this.cmdLoadData_Click);
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(110, 77);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(619, 44);
            this.ctrlDinas1.TabIndex = 27;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(941, 41);
            this.ctrlHeader1.TabIndex = 26;
            // 
            // cmdTambah
            // 
            this.cmdTambah.BackColor = System.Drawing.Color.PaleTurquoise;
            this.cmdTambah.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdTambah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdTambah.Location = new System.Drawing.Point(161, 166);
            this.cmdTambah.Name = "cmdTambah";
            this.cmdTambah.Size = new System.Drawing.Size(149, 32);
            this.cmdTambah.TabIndex = 25;
            this.cmdTambah.Text = "User Baru";
            this.cmdTambah.UseVisualStyleBackColor = false;
            this.cmdTambah.Click += new System.EventHandler(this.cmdTambah_Click);
            // 
            // cmdCariLagi
            // 
            this.cmdCariLagi.Location = new System.Drawing.Point(815, 174);
            this.cmdCariLagi.Name = "cmdCariLagi";
            this.cmdCariLagi.Size = new System.Drawing.Size(75, 23);
            this.cmdCariLagi.TabIndex = 24;
            this.cmdCariLagi.Text = "Cari Lagi";
            this.cmdCariLagi.UseVisualStyleBackColor = true;
            this.cmdCariLagi.Click += new System.EventHandler(this.cmdCariLagi_Click);
            // 
            // cmdCari
            // 
            this.cmdCari.Location = new System.Drawing.Point(734, 172);
            this.cmdCari.Name = "cmdCari";
            this.cmdCari.Size = new System.Drawing.Size(75, 23);
            this.cmdCari.TabIndex = 23;
            this.cmdCari.Text = "Cari";
            this.cmdCari.UseVisualStyleBackColor = true;
            this.cmdCari.Click += new System.EventHandler(this.cmdCari_Click);
            // 
            // txtCari
            // 
            this.txtCari.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCari.Location = new System.Drawing.Point(351, 174);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(378, 20);
            this.txtCari.TabIndex = 22;
            // 
            // gridPengguna
            // 
            this.gridPengguna.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPengguna.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridPengguna.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridPengguna.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPengguna.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Detail,
            this.Nama,
            this.UserID,
            this.NIK,
            this.SKPD,
            this.Otoritas,
            this.RefreshPasword});
            this.gridPengguna.Location = new System.Drawing.Point(0, 204);
            this.gridPengguna.Name = "gridPengguna";
            this.gridPengguna.Size = new System.Drawing.Size(941, 289);
            this.gridPengguna.TabIndex = 1;
            this.gridPengguna.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPengguna_CellClick);
            this.gridPengguna.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPengguna_CellContentClick);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 50;
            // 
            // Detail
            // 
            this.Detail.HeaderText = "Detail";
            this.Detail.Name = "Detail";
            this.Detail.Width = 50;
            // 
            // Nama
            // 
            this.Nama.HeaderText = "Nama";
            this.Nama.Name = "Nama";
            this.Nama.ReadOnly = true;
            this.Nama.Width = 300;
            // 
            // UserID
            // 
            this.UserID.HeaderText = "User ID";
            this.UserID.Name = "UserID";
            // 
            // NIK
            // 
            this.NIK.HeaderText = "NIK";
            this.NIK.Name = "NIK";
            this.NIK.ReadOnly = true;
            this.NIK.Width = 120;
            // 
            // SKPD
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SKPD.DefaultCellStyle = dataGridViewCellStyle1;
            this.SKPD.HeaderText = "SKPD";
            this.SKPD.Name = "SKPD";
            this.SKPD.ReadOnly = true;
            this.SKPD.Width = 350;
            // 
            // Otoritas
            // 
            this.Otoritas.HeaderText = "Otoritas";
            this.Otoritas.Name = "Otoritas";
            this.Otoritas.ReadOnly = true;
            this.Otoritas.Width = 150;
            // 
            // RefreshPasword
            // 
            this.RefreshPasword.HeaderText = "Refresh Password";
            this.RefreshPasword.Name = "RefreshPasword";
            this.RefreshPasword.ReadOnly = true;
            this.RefreshPasword.Width = 50;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Nama";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "User ID";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "NIK";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 120;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "ID";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.HeaderText = "Nama Dinas";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // ctrlOtoRitas1
            // 
            this.ctrlOtoRitas1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlOtoRitas1.Location = new System.Drawing.Point(110, 122);
            this.ctrlOtoRitas1.Name = "ctrlOtoRitas1";
            this.ctrlOtoRitas1.Otoritas = 0;
            this.ctrlOtoRitas1.Size = new System.Drawing.Size(619, 23);
            this.ctrlOtoRitas1.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Otoritas";
            // 
            // frmPengguna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 498);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ctrlOtoRitas1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkSemuaDinas);
            this.Controls.Add(this.cmdLoadData);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.cmdTambah);
            this.Controls.Add(this.cmdCariLagi);
            this.Controls.Add(this.cmdCari);
            this.Controls.Add(this.txtCari);
            this.Controls.Add(this.gridPengguna);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmPengguna";
            this.Text = "Daftar Pengguna";
            this.Load += new System.EventHandler(this.frmPengguna_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridPengguna)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridPengguna;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Button cmdCari;
        private System.Windows.Forms.Button cmdCariLagi;
        private System.Windows.Forms.Button cmdTambah;
        private ctrlHeader ctrlHeader1;
        private ctrlDinas ctrlDinas1;
        private System.Windows.Forms.Button cmdLoadData;
        private System.Windows.Forms.CheckBox chkSemuaDinas;
        private System.Windows.Forms.Label label1;
        private ctrlOtoRitas ctrlOtoRitas1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewButtonColumn Detail;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserID;
        private System.Windows.Forms.DataGridViewTextBoxColumn NIK;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKPD;
        private System.Windows.Forms.DataGridViewTextBoxColumn Otoritas;
        private System.Windows.Forms.DataGridViewButtonColumn RefreshPasword;
    }
}