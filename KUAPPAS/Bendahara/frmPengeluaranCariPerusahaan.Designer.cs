namespace KUAPPAS.Bendahara
{
    partial class frmPengeluaranCariPerusahaan
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
            this.gridPerusahaan = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bentuk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALamat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pilih = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAlamat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdCari = new System.Windows.Forms.Button();
            this.cmdPilih = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtNamaPimpinan = new System.Windows.Forms.TextBox();
            this.txtAlamatPerusahaan = new System.Windows.Forms.TextBox();
            this.txtNamaPerusahaan = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdBatal = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridPerusahaan)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridPerusahaan
            // 
            this.gridPerusahaan.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridPerusahaan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPerusahaan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Bentuk,
            this.Nama,
            this.ALamat,
            this.Pilih,
            this.Column1});
            this.gridPerusahaan.Location = new System.Drawing.Point(3, 142);
            this.gridPerusahaan.Name = "gridPerusahaan";
            this.gridPerusahaan.Size = new System.Drawing.Size(861, 253);
            this.gridPerusahaan.TabIndex = 0;
            this.gridPerusahaan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPerusahaan_CellContentClick);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            this.ID.Width = 50;
            // 
            // Bentuk
            // 
            this.Bentuk.HeaderText = "Bentuk";
            this.Bentuk.Name = "Bentuk";
            this.Bentuk.ReadOnly = true;
            this.Bentuk.Width = 80;
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
            // ALamat
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ALamat.DefaultCellStyle = dataGridViewCellStyle2;
            this.ALamat.HeaderText = "Alamat";
            this.ALamat.Name = "ALamat";
            this.ALamat.ReadOnly = true;
            this.ALamat.Width = 500;
            // 
            // Pilih
            // 
            this.Pilih.HeaderText = "Pilih";
            this.Pilih.Name = "Pilih";
            this.Pilih.ReadOnly = true;
            this.Pilih.Text = "Pilih";
            this.Pilih.Visible = false;
            this.Pilih.Width = 30;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(864, 42);
            this.ctrlHeader1.TabIndex = 1;
            // 
            // txtNama
            // 
            this.txtNama.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNama.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNama.Location = new System.Drawing.Point(157, 48);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(303, 21);
            this.txtNama.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1008, 339);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nama";
            // 
            // txtAlamat
            // 
            this.txtAlamat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAlamat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAlamat.Location = new System.Drawing.Point(157, 73);
            this.txtAlamat.Name = "txtAlamat";
            this.txtAlamat.Size = new System.Drawing.Size(303, 21);
            this.txtAlamat.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(81, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Nama";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(81, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Alamat";
            // 
            // cmdCari
            // 
            this.cmdCari.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cmdCari.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCari.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCari.Location = new System.Drawing.Point(157, 100);
            this.cmdCari.Name = "cmdCari";
            this.cmdCari.Size = new System.Drawing.Size(105, 36);
            this.cmdCari.TabIndex = 7;
            this.cmdCari.Text = "Cari";
            this.cmdCari.UseVisualStyleBackColor = false;
            this.cmdCari.Click += new System.EventHandler(this.cmdCari_Click);
            // 
            // cmdPilih
            // 
            this.cmdPilih.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmdPilih.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPilih.Location = new System.Drawing.Point(56, 29);
            this.cmdPilih.Name = "cmdPilih";
            this.cmdPilih.Size = new System.Drawing.Size(101, 47);
            this.cmdPilih.TabIndex = 9;
            this.cmdPilih.Text = "Pilih";
            this.cmdPilih.UseVisualStyleBackColor = false;
            this.cmdPilih.Click += new System.EventHandler(this.cmdPilih_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 617);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(864, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtNamaPimpinan
            // 
            this.txtNamaPimpinan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNamaPimpinan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNamaPimpinan.Location = new System.Drawing.Point(173, 465);
            this.txtNamaPimpinan.Name = "txtNamaPimpinan";
            this.txtNamaPimpinan.Size = new System.Drawing.Size(664, 21);
            this.txtNamaPimpinan.TabIndex = 14;
            // 
            // txtAlamatPerusahaan
            // 
            this.txtAlamatPerusahaan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAlamatPerusahaan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAlamatPerusahaan.Location = new System.Drawing.Point(173, 443);
            this.txtAlamatPerusahaan.Name = "txtAlamatPerusahaan";
            this.txtAlamatPerusahaan.Size = new System.Drawing.Size(664, 21);
            this.txtAlamatPerusahaan.TabIndex = 13;
            // 
            // txtNamaPerusahaan
            // 
            this.txtNamaPerusahaan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNamaPerusahaan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNamaPerusahaan.Location = new System.Drawing.Point(173, 418);
            this.txtNamaPerusahaan.Name = "txtNamaPerusahaan";
            this.txtNamaPerusahaan.Size = new System.Drawing.Size(664, 21);
            this.txtNamaPerusahaan.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 422);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Nama Perusahaan";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 443);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Alamat";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 473);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Nama Pimpinan";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdBatal);
            this.groupBox1.Controls.Add(this.cmdPilih);
            this.groupBox1.Location = new System.Drawing.Point(3, 509);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(870, 105);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            // 
            // cmdBatal
            // 
            this.cmdBatal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cmdBatal.Location = new System.Drawing.Point(750, 29);
            this.cmdBatal.Name = "cmdBatal";
            this.cmdBatal.Size = new System.Drawing.Size(84, 52);
            this.cmdBatal.TabIndex = 9;
            this.cmdBatal.Text = "Batal";
            this.cmdBatal.UseVisualStyleBackColor = false;
            // 
            // frmPengeluaranCariPerusahaan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 639);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNamaPimpinan);
            this.Controls.Add(this.txtAlamatPerusahaan);
            this.Controls.Add(this.txtNamaPerusahaan);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cmdCari);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAlamat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.gridPerusahaan);
            this.Name = "frmPengeluaranCariPerusahaan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cari Perusahaan";
            this.Load += new System.EventHandler(this.frmCariPerusahaan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridPerusahaan)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridPerusahaan;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAlamat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdCari;
        private System.Windows.Forms.Button cmdPilih;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox txtNamaPimpinan;
        private System.Windows.Forms.TextBox txtAlamatPerusahaan;
        private System.Windows.Forms.TextBox txtNamaPerusahaan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bentuk;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALamat;
        private System.Windows.Forms.DataGridViewButtonColumn Pilih;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdBatal;
    }
}