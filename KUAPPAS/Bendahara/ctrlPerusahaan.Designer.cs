namespace KUAPPAS.Bendahara
{
    partial class ctrlPerusahaan
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtNamaPerusahaan = new System.Windows.Forms.TextBox();
            this.cmbBentuk = new System.Windows.Forms.ComboBox();
            this.txtAlamat = new System.Windows.Forms.TextBox();
            this.txtNamaPimpinan = new System.Windows.Forms.TextBox();
            this.txtNoRekening = new System.Windows.Forms.TextBox();
            this.txtNPWP = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdTambahPerusahaan = new System.Windows.Forms.Button();
            this.cmdCariPerusahaan = new System.Windows.Forms.Button();
            this.cmdUbahPerusahaan = new System.Windows.Forms.Button();
            this.cmdSimpan = new System.Windows.Forms.Button();
            this.txtNamaDalamRekening = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cmdBatal = new System.Windows.Forms.Button();
            this.ctrlDaftarBank1 = new KUAPPAS.Bendahara.ctrlDaftarBank();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNamaPerusahaan
            // 
            this.txtNamaPerusahaan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNamaPerusahaan.Enabled = false;
            this.txtNamaPerusahaan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNamaPerusahaan.Location = new System.Drawing.Point(2, 32);
            this.txtNamaPerusahaan.Name = "txtNamaPerusahaan";
            this.txtNamaPerusahaan.Size = new System.Drawing.Size(537, 21);
            this.txtNamaPerusahaan.TabIndex = 0;
            // 
            // cmbBentuk
            // 
            this.cmbBentuk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBentuk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbBentuk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBentuk.FormattingEnabled = true;
            this.cmbBentuk.Location = new System.Drawing.Point(0, 0);
            this.cmbBentuk.Name = "cmbBentuk";
            this.cmbBentuk.Size = new System.Drawing.Size(192, 23);
            this.cmbBentuk.TabIndex = 1;
            // 
            // txtAlamat
            // 
            this.txtAlamat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAlamat.Enabled = false;
            this.txtAlamat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAlamat.Location = new System.Drawing.Point(2, 75);
            this.txtAlamat.Name = "txtAlamat";
            this.txtAlamat.Size = new System.Drawing.Size(537, 21);
            this.txtAlamat.TabIndex = 2;
            // 
            // txtNamaPimpinan
            // 
            this.txtNamaPimpinan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNamaPimpinan.Enabled = false;
            this.txtNamaPimpinan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNamaPimpinan.Location = new System.Drawing.Point(2, 96);
            this.txtNamaPimpinan.Name = "txtNamaPimpinan";
            this.txtNamaPimpinan.Size = new System.Drawing.Size(537, 21);
            this.txtNamaPimpinan.TabIndex = 3;
            // 
            // txtNoRekening
            // 
            this.txtNoRekening.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoRekening.Enabled = false;
            this.txtNoRekening.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoRekening.Location = new System.Drawing.Point(2, 159);
            this.txtNoRekening.Name = "txtNoRekening";
            this.txtNoRekening.Size = new System.Drawing.Size(537, 21);
            this.txtNoRekening.TabIndex = 4;
            this.txtNoRekening.TextChanged += new System.EventHandler(this.txtNoRekening_TextChanged);
            // 
            // txtNPWP
            // 
            this.txtNPWP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNPWP.Enabled = false;
            this.txtNPWP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNPWP.Location = new System.Drawing.Point(2, 117);
            this.txtNPWP.Name = "txtNPWP";
            this.txtNPWP.Size = new System.Drawing.Size(537, 21);
            this.txtNPWP.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cmbBentuk);
            this.panel1.Location = new System.Drawing.Point(2, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(221, 22);
            this.panel1.TabIndex = 8;
            // 
            // cmdTambahPerusahaan
            // 
            this.cmdTambahPerusahaan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmdTambahPerusahaan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdTambahPerusahaan.Location = new System.Drawing.Point(0, 206);
            this.cmdTambahPerusahaan.Name = "cmdTambahPerusahaan";
            this.cmdTambahPerusahaan.Size = new System.Drawing.Size(99, 28);
            this.cmdTambahPerusahaan.TabIndex = 10;
            this.cmdTambahPerusahaan.Text = "Perusahaan Baru";
            this.cmdTambahPerusahaan.UseVisualStyleBackColor = false;
            this.cmdTambahPerusahaan.Click += new System.EventHandler(this.cmdTambahPerusahaan_Click);
            // 
            // cmdCariPerusahaan
            // 
            this.cmdCariPerusahaan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cmdCariPerusahaan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCariPerusahaan.Location = new System.Drawing.Point(4, 2);
            this.cmdCariPerusahaan.Name = "cmdCariPerusahaan";
            this.cmdCariPerusahaan.Size = new System.Drawing.Size(140, 28);
            this.cmdCariPerusahaan.TabIndex = 11;
            this.cmdCariPerusahaan.Text = "Cari Pihak ketiga";
            this.cmdCariPerusahaan.UseVisualStyleBackColor = false;
            this.cmdCariPerusahaan.Click += new System.EventHandler(this.cmdCariPerusahaan_Click);
            // 
            // cmdUbahPerusahaan
            // 
            this.cmdUbahPerusahaan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdUbahPerusahaan.Location = new System.Drawing.Point(114, 207);
            this.cmdUbahPerusahaan.Name = "cmdUbahPerusahaan";
            this.cmdUbahPerusahaan.Size = new System.Drawing.Size(109, 28);
            this.cmdUbahPerusahaan.TabIndex = 12;
            this.cmdUbahPerusahaan.Text = "Ubah Perusahaan";
            this.cmdUbahPerusahaan.UseVisualStyleBackColor = true;
            this.cmdUbahPerusahaan.Click += new System.EventHandler(this.cmdUbahPerusahaan_Click);
            // 
            // cmdSimpan
            // 
            this.cmdSimpan.Enabled = false;
            this.cmdSimpan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSimpan.Location = new System.Drawing.Point(240, 206);
            this.cmdSimpan.Name = "cmdSimpan";
            this.cmdSimpan.Size = new System.Drawing.Size(134, 28);
            this.cmdSimpan.TabIndex = 13;
            this.cmdSimpan.Text = "Simpan Perusahaan";
            this.cmdSimpan.UseVisualStyleBackColor = true;
            this.cmdSimpan.Click += new System.EventHandler(this.cmdSimpan_Click);
            // 
            // txtNamaDalamRekening
            // 
            this.txtNamaDalamRekening.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNamaDalamRekening.Enabled = false;
            this.txtNamaDalamRekening.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNamaDalamRekening.Location = new System.Drawing.Point(2, 180);
            this.txtNamaDalamRekening.Name = "txtNamaDalamRekening";
            this.txtNamaDalamRekening.Size = new System.Drawing.Size(537, 21);
            this.txtNamaDalamRekening.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(376, 205);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 28);
            this.button1.TabIndex = 15;
            this.button1.Text = "Cek Nomor Rekening Bank";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmdBatal
            // 
            this.cmdBatal.Location = new System.Drawing.Point(3, 234);
            this.cmdBatal.Name = "cmdBatal";
            this.cmdBatal.Size = new System.Drawing.Size(75, 23);
            this.cmdBatal.TabIndex = 16;
            this.cmdBatal.Text = "Batal";
            this.cmdBatal.UseVisualStyleBackColor = true;
            this.cmdBatal.Visible = false;
            this.cmdBatal.Click += new System.EventHandler(this.cmdBatal_Click);
            // 
            // ctrlDaftarBank1
            // 
            this.ctrlDaftarBank1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlDaftarBank1.Enabled = false;
            this.ctrlDaftarBank1.KeteranganNamaBank = "";
            this.ctrlDaftarBank1.Location = new System.Drawing.Point(2, 138);
            this.ctrlDaftarBank1.Name = "ctrlDaftarBank1";
            this.ctrlDaftarBank1.Size = new System.Drawing.Size(537, 21);
            this.ctrlDaftarBank1.TabIndex = 9;
            // 
            // ctrlPerusahaan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmdBatal);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtNamaDalamRekening);
            this.Controls.Add(this.cmdSimpan);
            this.Controls.Add(this.cmdUbahPerusahaan);
            this.Controls.Add(this.cmdCariPerusahaan);
            this.Controls.Add(this.cmdTambahPerusahaan);
            this.Controls.Add(this.ctrlDaftarBank1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtNPWP);
            this.Controls.Add(this.txtNoRekening);
            this.Controls.Add(this.txtNamaPimpinan);
            this.Controls.Add(this.txtAlamat);
            this.Controls.Add(this.txtNamaPerusahaan);
            this.Name = "ctrlPerusahaan";
            this.Size = new System.Drawing.Size(582, 283);
            this.Load += new System.EventHandler(this.ctrlPerusahaan_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNamaPerusahaan;
        private System.Windows.Forms.ComboBox cmbBentuk;
        private System.Windows.Forms.TextBox txtAlamat;
        private System.Windows.Forms.TextBox txtNamaPimpinan;
        private System.Windows.Forms.TextBox txtNoRekening;
        private System.Windows.Forms.TextBox txtNPWP;
        private System.Windows.Forms.Panel panel1;
        private ctrlDaftarBank ctrlDaftarBank1;
        private System.Windows.Forms.Button cmdTambahPerusahaan;
        private System.Windows.Forms.Button cmdCariPerusahaan;
        private System.Windows.Forms.Button cmdUbahPerusahaan;
        private System.Windows.Forms.Button cmdSimpan;
        private System.Windows.Forms.TextBox txtNamaDalamRekening;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button cmdBatal;
    }
}
