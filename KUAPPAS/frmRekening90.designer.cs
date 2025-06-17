namespace KUAPPAS
{
    partial class frmRekening90
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
            this.Profile = new System.Windows.Forms.Label();
            this.ctrlProfileRekening1 = new KUAPPAS.ctrlProfileRekening();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeRekening1 = new KUAPPAS.treeRekening();
            this.button1 = new System.Windows.Forms.Button();
            this.cmdTambahAnak = new System.Windows.Forms.Button();
            this.cmdHapus = new System.Windows.Forms.Button();
            this.cmdSimpan = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chkPunyaAnak = new System.Windows.Forms.CheckBox();
            this.txtRoot = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlKodeRekeningParent = new KUAPPAS.ctrlKodeRekeningTerpisah();
            this.txtNamaRekening = new System.Windows.Forms.TextBox();
            this.ctrlKodeRekening = new KUAPPAS.ctrlKodeRekeningTerpisah();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Profile
            // 
            this.Profile.AutoSize = true;
            this.Profile.Location = new System.Drawing.Point(13, 63);
            this.Profile.Name = "Profile";
            this.Profile.Size = new System.Drawing.Size(36, 13);
            this.Profile.TabIndex = 4;
            this.Profile.Text = "Profile";
            this.Profile.Visible = false;
            // 
            // ctrlProfileRekening1
            // 
            this.ctrlProfileRekening1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlProfileRekening1.Location = new System.Drawing.Point(99, 63);
            this.ctrlProfileRekening1.Name = "ctrlProfileRekening1";
            this.ctrlProfileRekening1.Profile = 2;
            this.ctrlProfileRekening1.Size = new System.Drawing.Size(237, 25);
            this.ctrlProfileRekening1.TabIndex = 3;
            this.ctrlProfileRekening1.Visible = false;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(900, 66);
            this.ctrlHeader1.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 94);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeRekening1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.cmdTambahAnak);
            this.splitContainer1.Panel2.Controls.Add(this.cmdHapus);
            this.splitContainer1.Panel2.Controls.Add(this.cmdSimpan);
            this.splitContainer1.Panel2.Controls.Add(this.btnAdd);
            this.splitContainer1.Panel2.Controls.Add(this.chkPunyaAnak);
            this.splitContainer1.Panel2.Controls.Add(this.txtRoot);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.ctrlKodeRekeningParent);
            this.splitContainer1.Panel2.Controls.Add(this.txtNamaRekening);
            this.splitContainer1.Panel2.Controls.Add(this.ctrlKodeRekening);
            this.splitContainer1.Size = new System.Drawing.Size(897, 328);
            this.splitContainer1.SplitterDistance = 333;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeRekening1
            // 
            this.treeRekening1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.treeRekening1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeRekening1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeRekening1.Location = new System.Drawing.Point(0, 0);
            this.treeRekening1.Name = "treeRekening1";
            this.treeRekening1.Profile = 1;
            this.treeRekening1.Size = new System.Drawing.Size(333, 328);
            this.treeRekening1.TabIndex = 0;
            this.treeRekening1.Changed += new KUAPPAS.treeRekening.ValueChangedEventHandler(this.treeRekening1_Changed);
            this.treeRekening1.Load += new System.EventHandler(this.treeRekening1_Load);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(120, 327);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 45);
            this.button1.TabIndex = 14;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmdTambahAnak
            // 
            this.cmdTambahAnak.BackColor = System.Drawing.Color.White;
            this.cmdTambahAnak.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdTambahAnak.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTambahAnak.Location = new System.Drawing.Point(120, 260);
            this.cmdTambahAnak.Name = "cmdTambahAnak";
            this.cmdTambahAnak.Size = new System.Drawing.Size(101, 29);
            this.cmdTambahAnak.TabIndex = 13;
            this.cmdTambahAnak.Text = "Tambah Rincian";
            this.cmdTambahAnak.UseVisualStyleBackColor = false;
            this.cmdTambahAnak.Click += new System.EventHandler(this.cmdTambahAnak_Click);
            // 
            // cmdHapus
            // 
            this.cmdHapus.BackColor = System.Drawing.Color.White;
            this.cmdHapus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdHapus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHapus.Location = new System.Drawing.Point(120, 291);
            this.cmdHapus.Name = "cmdHapus";
            this.cmdHapus.Size = new System.Drawing.Size(113, 29);
            this.cmdHapus.TabIndex = 12;
            this.cmdHapus.Text = "Hapus";
            this.cmdHapus.UseVisualStyleBackColor = false;
            this.cmdHapus.Click += new System.EventHandler(this.cmdHapus_Click);
            // 
            // cmdSimpan
            // 
            this.cmdSimpan.BackColor = System.Drawing.Color.White;
            this.cmdSimpan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdSimpan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSimpan.Location = new System.Drawing.Point(319, 229);
            this.cmdSimpan.Name = "cmdSimpan";
            this.cmdSimpan.Size = new System.Drawing.Size(134, 29);
            this.cmdSimpan.TabIndex = 11;
            this.cmdSimpan.Text = "Simpan";
            this.cmdSimpan.UseVisualStyleBackColor = false;
            this.cmdSimpan.Click += new System.EventHandler(this.cmdSimpan_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.White;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(119, 229);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(179, 29);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "Tambah Rekening Sama Header";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // chkPunyaAnak
            // 
            this.chkPunyaAnak.AutoSize = true;
            this.chkPunyaAnak.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPunyaAnak.Location = new System.Drawing.Point(120, 205);
            this.chkPunyaAnak.Name = "chkPunyaAnak";
            this.chkPunyaAnak.Size = new System.Drawing.Size(116, 18);
            this.chkPunyaAnak.TabIndex = 9;
            this.chkPunyaAnak.Text = "Punya Sub/Anak";
            this.chkPunyaAnak.UseVisualStyleBackColor = true;
            // 
            // txtRoot
            // 
            this.txtRoot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRoot.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoot.Location = new System.Drawing.Point(120, 178);
            this.txtRoot.Name = "txtRoot";
            this.txtRoot.Size = new System.Drawing.Size(282, 22);
            this.txtRoot.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(46, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "Root";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(44, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "Nama";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(44, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "Kode";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(44, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "Induk";
            // 
            // ctrlKodeRekeningParent
            // 
            this.ctrlKodeRekeningParent.Location = new System.Drawing.Point(118, 11);
            this.ctrlKodeRekeningParent.Name = "ctrlKodeRekeningParent";
            this.ctrlKodeRekeningParent.Size = new System.Drawing.Size(287, 64);
            this.ctrlKodeRekeningParent.TabIndex = 2;
            // 
            // txtNamaRekening
            // 
            this.txtNamaRekening.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNamaRekening.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNamaRekening.Location = new System.Drawing.Point(118, 100);
            this.txtNamaRekening.Multiline = true;
            this.txtNamaRekening.Name = "txtNamaRekening";
            this.txtNamaRekening.Size = new System.Drawing.Size(282, 72);
            this.txtNamaRekening.TabIndex = 1;
            // 
            // ctrlKodeRekening
            // 
            this.ctrlKodeRekening.Location = new System.Drawing.Point(118, 76);
            this.ctrlKodeRekening.Name = "ctrlKodeRekening";
            this.ctrlKodeRekening.Size = new System.Drawing.Size(287, 25);
            this.ctrlKodeRekening.TabIndex = 0;
            // 
            // frmRekening90
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 450);
            this.Controls.Add(this.Profile);
            this.Controls.Add(this.ctrlProfileRekening1);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmRekening90";
            this.Text = "Kode Rekening";
            this.Load += new System.EventHandler(this.frmRekening_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private treeRekening treeRekening1;
        private ctrlKodeRekeningTerpisah ctrlKodeRekeningParent;
        private System.Windows.Forms.TextBox txtNamaRekening;
        private ctrlKodeRekeningTerpisah ctrlKodeRekening;
        private System.Windows.Forms.CheckBox chkPunyaAnak;
        private System.Windows.Forms.TextBox txtRoot;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button cmdSimpan;
        private System.Windows.Forms.Button cmdHapus;
        private System.Windows.Forms.Button cmdTambahAnak;
        private ctrlHeader ctrlHeader1;
        private ctrlProfileRekening ctrlProfileRekening1;
        private System.Windows.Forms.Label Profile;
        private System.Windows.Forms.Button button1;
    }
}