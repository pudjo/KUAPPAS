namespace KUAPPAS
{
    partial class frmSettingProfileRekening
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
            this.gridProfile = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Keterangan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdSimpan = new System.Windows.Forms.Button();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.txtNumSegment = new System.Windows.Forms.TextBox();
            this.txtSegmen1 = new System.Windows.Forms.TextBox();
            this.txtSegmen2 = new System.Windows.Forms.TextBox();
            this.txtSegmen3 = new System.Windows.Forms.TextBox();
            this.txtSegmen4 = new System.Windows.Forms.TextBox();
            this.txtSegmen5 = new System.Windows.Forms.TextBox();
            this.txtSegmen6 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cmdTambah = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            ((System.ComponentModel.ISupportInitialize)(this.gridProfile)).BeginInit();
            this.SuspendLayout();
            // 
            // gridProfile
            // 
            this.gridProfile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridProfile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProfile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Nama,
            this.Keterangan});
            this.gridProfile.Location = new System.Drawing.Point(5, 53);
            this.gridProfile.Name = "gridProfile";
            this.gridProfile.Size = new System.Drawing.Size(343, 371);
            this.gridProfile.TabIndex = 0;
            this.gridProfile.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridProfile_CellContentClick);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // Nama
            // 
            this.Nama.HeaderText = "Nama";
            this.Nama.Name = "Nama";
            this.Nama.ReadOnly = true;
            // 
            // Keterangan
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Keterangan.DefaultCellStyle = dataGridViewCellStyle1;
            this.Keterangan.HeaderText = "Keterangan";
            this.Keterangan.Name = "Keterangan";
            this.Keterangan.ReadOnly = true;
            this.Keterangan.Width = 200;
            // 
            // cmdSimpan
            // 
            this.cmdSimpan.Location = new System.Drawing.Point(612, 367);
            this.cmdSimpan.Name = "cmdSimpan";
            this.cmdSimpan.Size = new System.Drawing.Size(71, 31);
            this.cmdSimpan.TabIndex = 1;
            this.cmdSimpan.Text = "Simpan";
            this.cmdSimpan.UseVisualStyleBackColor = true;
            // 
            // txtNama
            // 
            this.txtNama.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNama.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNama.Location = new System.Drawing.Point(532, 53);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(264, 21);
            this.txtNama.TabIndex = 2;
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKeterangan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKeterangan.Location = new System.Drawing.Point(532, 83);
            this.txtKeterangan.Multiline = true;
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(264, 47);
            this.txtKeterangan.TabIndex = 3;
            // 
            // txtNumSegment
            // 
            this.txtNumSegment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNumSegment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumSegment.Location = new System.Drawing.Point(532, 136);
            this.txtNumSegment.Name = "txtNumSegment";
            this.txtNumSegment.Size = new System.Drawing.Size(60, 21);
            this.txtNumSegment.TabIndex = 4;
            // 
            // txtSegmen1
            // 
            this.txtSegmen1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSegmen1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSegmen1.Location = new System.Drawing.Point(532, 166);
            this.txtSegmen1.Name = "txtSegmen1";
            this.txtSegmen1.Size = new System.Drawing.Size(60, 21);
            this.txtSegmen1.TabIndex = 5;
            // 
            // txtSegmen2
            // 
            this.txtSegmen2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSegmen2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSegmen2.Location = new System.Drawing.Point(532, 196);
            this.txtSegmen2.Name = "txtSegmen2";
            this.txtSegmen2.Size = new System.Drawing.Size(60, 21);
            this.txtSegmen2.TabIndex = 6;
            // 
            // txtSegmen3
            // 
            this.txtSegmen3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSegmen3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSegmen3.Location = new System.Drawing.Point(532, 231);
            this.txtSegmen3.Name = "txtSegmen3";
            this.txtSegmen3.Size = new System.Drawing.Size(60, 21);
            this.txtSegmen3.TabIndex = 7;
            // 
            // txtSegmen4
            // 
            this.txtSegmen4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSegmen4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSegmen4.Location = new System.Drawing.Point(532, 262);
            this.txtSegmen4.Name = "txtSegmen4";
            this.txtSegmen4.Size = new System.Drawing.Size(60, 21);
            this.txtSegmen4.TabIndex = 8;
            // 
            // txtSegmen5
            // 
            this.txtSegmen5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSegmen5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSegmen5.Location = new System.Drawing.Point(532, 293);
            this.txtSegmen5.Name = "txtSegmen5";
            this.txtSegmen5.Size = new System.Drawing.Size(60, 21);
            this.txtSegmen5.TabIndex = 9;
            // 
            // txtSegmen6
            // 
            this.txtSegmen6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSegmen6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSegmen6.Location = new System.Drawing.Point(532, 324);
            this.txtSegmen6.Name = "txtSegmen6";
            this.txtSegmen6.Size = new System.Drawing.Size(60, 21);
            this.txtSegmen6.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(386, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Nama";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(386, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Keterangan";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(386, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Jumlah Segmen";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(386, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Jumlah Angka Segmen 1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(386, 197);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(125, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Jumlah Angka Segmen 2";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(386, 234);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(125, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Jumlah Angka Segmen 3";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(386, 266);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(125, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "Jumlah Angka Segmen 4";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(386, 297);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(125, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "Jumlah Angka Segmen 5";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(386, 328);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(125, 13);
            this.label14.TabIndex = 24;
            this.label14.Text = "Jumlah Angka Segmen 6";
            // 
            // cmdTambah
            // 
            this.cmdTambah.Location = new System.Drawing.Point(532, 365);
            this.cmdTambah.Name = "cmdTambah";
            this.cmdTambah.Size = new System.Drawing.Size(59, 32);
            this.cmdTambah.TabIndex = 25;
            this.cmdTambah.Text = "Tambah";
            this.cmdTambah.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 435);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(831, 22);
            this.statusStrip1.TabIndex = 26;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Location = new System.Drawing.Point(4, 2);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(826, 45);
            this.ctrlHeader1.TabIndex = 27;
            // 
            // frmSettingProfileRekening
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 457);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cmdTambah);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSegmen6);
            this.Controls.Add(this.txtSegmen5);
            this.Controls.Add(this.txtSegmen4);
            this.Controls.Add(this.txtSegmen3);
            this.Controls.Add(this.txtSegmen2);
            this.Controls.Add(this.txtSegmen1);
            this.Controls.Add(this.txtNumSegment);
            this.Controls.Add(this.txtKeterangan);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.cmdSimpan);
            this.Controls.Add(this.gridProfile);
            this.Name = "frmSettingProfileRekening";
            this.Text = "Setting Profile Kode Rekening";
            this.Load += new System.EventHandler(this.frmSettingProfileRekening_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridProfile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridProfile;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn Keterangan;
        private System.Windows.Forms.Button cmdSimpan;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.TextBox txtKeterangan;
        private System.Windows.Forms.TextBox txtNumSegment;
        private System.Windows.Forms.TextBox txtSegmen1;
        private System.Windows.Forms.TextBox txtSegmen2;
        private System.Windows.Forms.TextBox txtSegmen3;
        private System.Windows.Forms.TextBox txtSegmen4;
        private System.Windows.Forms.TextBox txtSegmen5;
        private System.Windows.Forms.TextBox txtSegmen6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button cmdTambah;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private ctrlHeader ctrlHeader1;
    }
}