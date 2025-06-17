namespace KUAPPAS.Bendahara
{
    partial class frmPenerimaanSPM
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmdBaru = new System.Windows.Forms.Button();
            this.txtNoUrut = new System.Windows.Forms.TextBox();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.cmdSimpan = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gridTerima = new System.Windows.Forms.DataGridView();
            this.cmdPanggil = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdHariIni = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.cmdExcell = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.Tanggal = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlTanggal2 = new KUAPPAS.ctrlTanggal();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.ctrlPencarian1 = new KUAPPAS.ctrlPencarian();
            this.ctrlTanggal1 = new KUAPPAS.ctrlTanggal();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomorUrut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoSPM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Keterngan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jumlah = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tanngal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlSPP1 = new KUAPPAS.Bendahara.ctrlSPP();
            this.ctrlSKPD1 = new KUAPPAS.ctrlSKPD();
            this.Timex = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.gridTerima)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdBaru
            // 
            this.cmdBaru.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBaru.Location = new System.Drawing.Point(15, 50);
            this.cmdBaru.Name = "cmdBaru";
            this.cmdBaru.Size = new System.Drawing.Size(137, 67);
            this.cmdBaru.TabIndex = 3;
            this.cmdBaru.Text = "Baru";
            this.cmdBaru.UseVisualStyleBackColor = true;
            this.cmdBaru.Click += new System.EventHandler(this.cmdBaru_Click);
            // 
            // txtNoUrut
            // 
            this.txtNoUrut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoUrut.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoUrut.Location = new System.Drawing.Point(285, 158);
            this.txtNoUrut.Name = "txtNoUrut";
            this.txtNoUrut.Size = new System.Drawing.Size(127, 29);
            this.txtNoUrut.TabIndex = 4;
            this.txtNoUrut.Visible = false;
            this.txtNoUrut.TextChanged += new System.EventHandler(this.txtNoUrut_TextChanged);
            // 
            // txtJumlah
            // 
            this.txtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlah.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlah.Location = new System.Drawing.Point(285, 195);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(310, 29);
            this.txtJumlah.TabIndex = 5;
            this.txtJumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKeterangan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKeterangan.Location = new System.Drawing.Point(285, 232);
            this.txtKeterangan.Multiline = true;
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(504, 103);
            this.txtKeterangan.TabIndex = 6;
            // 
            // cmdSimpan
            // 
            this.cmdSimpan.Enabled = false;
            this.cmdSimpan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSimpan.Location = new System.Drawing.Point(285, 346);
            this.cmdSimpan.Name = "cmdSimpan";
            this.cmdSimpan.Size = new System.Drawing.Size(127, 42);
            this.cmdSimpan.TabIndex = 7;
            this.cmdSimpan.Text = "Simpan";
            this.cmdSimpan.UseVisualStyleBackColor = true;
            this.cmdSimpan.Click += new System.EventHandler(this.cmdSimpan_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(173, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "S K P D";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(173, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "Nomor SPM";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(173, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "Jumlah";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(173, 242);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Keterangan";
            // 
            // gridTerima
            // 
            this.gridTerima.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridTerima.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTerima.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.NomorUrut,
            this.SKPD,
            this.NoSPM,
            this.Keterngan,
            this.Jumlah,
            this.Tanngal});
            this.gridTerima.Location = new System.Drawing.Point(0, 481);
            this.gridTerima.Name = "gridTerima";
            this.gridTerima.Size = new System.Drawing.Size(847, 146);
            this.gridTerima.TabIndex = 12;
            // 
            // cmdPanggil
            // 
            this.cmdPanggil.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPanggil.Location = new System.Drawing.Point(250, 447);
            this.cmdPanggil.Name = "cmdPanggil";
            this.cmdPanggil.Size = new System.Drawing.Size(107, 31);
            this.cmdPanggil.TabIndex = 13;
            this.cmdPanggil.Text = "Panggil";
            this.cmdPanggil.UseVisualStyleBackColor = true;
            this.cmdPanggil.Click += new System.EventHandler(this.cmdPanggil_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 458);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 15;
            this.label5.Text = "Tanggal";
            // 
            // cmdHariIni
            // 
            this.cmdHariIni.Location = new System.Drawing.Point(193, 454);
            this.cmdHariIni.Name = "cmdHariIni";
            this.cmdHariIni.Size = new System.Drawing.Size(51, 25);
            this.cmdHariIni.TabIndex = 17;
            this.cmdHariIni.Text = "Hari Ini";
            this.cmdHariIni.UseVisualStyleBackColor = true;
            this.cmdHariIni.Click += new System.EventHandler(this.cmdHariIni_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 616);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(847, 22);
            this.statusStrip1.TabIndex = 18;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // cmdExcell
            // 
            this.cmdExcell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExcell.Location = new System.Drawing.Point(363, 447);
            this.cmdExcell.Name = "cmdExcell";
            this.cmdExcell.Size = new System.Drawing.Size(114, 31);
            this.cmdExcell.TabIndex = 20;
            this.cmdExcell.Text = "Excell";
            this.cmdExcell.UseVisualStyleBackColor = true;
            this.cmdExcell.Click += new System.EventHandler(this.cmdExcell_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(173, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 15);
            this.label6.TabIndex = 22;
            this.label6.Text = "Nomor Urut";
            this.label6.Visible = false;
            // 
            // Tanggal
            // 
            this.Tanggal.AutoSize = true;
            this.Tanggal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tanggal.Location = new System.Drawing.Point(173, 62);
            this.Tanggal.Name = "Tanggal";
            this.Tanggal.Size = new System.Drawing.Size(59, 15);
            this.Tanggal.TabIndex = 24;
            this.Tanggal.Text = "Tanggal";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(158, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1, 285);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "No";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 30;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "No Urut";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn3.HeaderText = "SKPD";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 120;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn4.HeaderText = "No SPM";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn5.HeaderText = "Keterangan";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 300;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn6.HeaderText = "Jumlah";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 120;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Tanggal/Jam Terima";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 120;
            // 
            // ctrlTanggal2
            // 
            this.ctrlTanggal2.Location = new System.Drawing.Point(285, 62);
            this.ctrlTanggal2.Name = "ctrlTanggal2";
            this.ctrlTanggal2.Size = new System.Drawing.Size(165, 25);
            this.ctrlTanggal2.TabIndex = 23;
            this.ctrlTanggal2.Tanggal = new System.DateTime(2024, 9, 26, 0, 0, 0, 0);
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(847, 41);
            this.ctrlHeader1.TabIndex = 21;
            // 
            // ctrlPencarian1
            // 
            this.ctrlPencarian1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlPencarian1.Location = new System.Drawing.Point(512, 447);
            this.ctrlPencarian1.Name = "ctrlPencarian1";
            this.ctrlPencarian1.Size = new System.Drawing.Size(335, 28);
            this.ctrlPencarian1.TabIndex = 19;
            // 
            // ctrlTanggal1
            // 
            this.ctrlTanggal1.Location = new System.Drawing.Point(71, 456);
            this.ctrlTanggal1.Name = "ctrlTanggal1";
            this.ctrlTanggal1.Size = new System.Drawing.Size(116, 25);
            this.ctrlTanggal1.TabIndex = 16;
            this.ctrlTanggal1.Tanggal = new System.DateTime(2024, 9, 26, 0, 0, 0, 0);
            // 
            // No
            // 
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.Width = 30;
            // 
            // NomorUrut
            // 
            this.NomorUrut.HeaderText = "No Urut";
            this.NomorUrut.Name = "NomorUrut";
            this.NomorUrut.ReadOnly = true;
            this.NomorUrut.Width = 50;
            // 
            // SKPD
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SKPD.DefaultCellStyle = dataGridViewCellStyle1;
            this.SKPD.HeaderText = "SKPD";
            this.SKPD.Name = "SKPD";
            this.SKPD.ReadOnly = true;
            this.SKPD.Width = 120;
            // 
            // NoSPM
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NoSPM.DefaultCellStyle = dataGridViewCellStyle2;
            this.NoSPM.HeaderText = "No SPM";
            this.NoSPM.Name = "NoSPM";
            this.NoSPM.ReadOnly = true;
            // 
            // Keterngan
            // 
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Keterngan.DefaultCellStyle = dataGridViewCellStyle3;
            this.Keterngan.HeaderText = "Keterangan";
            this.Keterngan.Name = "Keterngan";
            this.Keterngan.ReadOnly = true;
            this.Keterngan.Width = 300;
            // 
            // Jumlah
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Jumlah.DefaultCellStyle = dataGridViewCellStyle4;
            this.Jumlah.HeaderText = "Jumlah";
            this.Jumlah.Name = "Jumlah";
            this.Jumlah.ReadOnly = true;
            this.Jumlah.Width = 120;
            // 
            // Tanngal
            // 
            this.Tanngal.HeaderText = "Tanggal/Jam Terima";
            this.Tanngal.Name = "Tanngal";
            this.Tanngal.ReadOnly = true;
            this.Tanngal.Width = 120;
            // 
            // ctrlSPP1
            // 
            this.ctrlSPP1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSPP1.ID = ((long)(0));
            this.ctrlSPP1.Location = new System.Drawing.Point(285, 126);
            this.ctrlSPP1.Name = "ctrlSPP1";
            this.ctrlSPP1.Size = new System.Drawing.Size(505, 24);
            this.ctrlSPP1.TabIndex = 2;
            this.ctrlSPP1.OnChanged += new KUAPPAS.Bendahara.ctrlSPP.ValueChangedEventHandler(this.ctrlSPP1_OnChanged);
            this.ctrlSPP1.Load += new System.EventHandler(this.ctrlSPP1_Load);
            // 
            // ctrlSKPD1
            // 
            this.ctrlSKPD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSKPD1.Location = new System.Drawing.Point(285, 94);
            this.ctrlSKPD1.Name = "ctrlSKPD1";
            this.ctrlSKPD1.Size = new System.Drawing.Size(505, 26);
            this.ctrlSKPD1.TabIndex = 1;
            this.ctrlSKPD1.OnChanged += new KUAPPAS.ctrlSKPD.ValueChangedEventHandler(this.ctrlSKPD1_OnChanged);
            this.ctrlSKPD1.Load += new System.EventHandler(this.ctrlSKPD1_Load);
            // 
            // Timex
            // 
            this.Timex.Location = new System.Drawing.Point(456, 62);
            this.Timex.Name = "Timex";
            this.Timex.Size = new System.Drawing.Size(117, 20);
            this.Timex.TabIndex = 26;
            // 
            // frmPenerimaanSPM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 638);
            this.Controls.Add(this.Timex);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Tanggal);
            this.Controls.Add(this.ctrlTanggal2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.cmdExcell);
            this.Controls.Add(this.ctrlPencarian1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cmdHariIni);
            this.Controls.Add(this.ctrlTanggal1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmdPanggil);
            this.Controls.Add(this.gridTerima);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdSimpan);
            this.Controls.Add(this.txtKeterangan);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.txtNoUrut);
            this.Controls.Add(this.cmdBaru);
            this.Controls.Add(this.ctrlSPP1);
            this.Controls.Add(this.ctrlSKPD1);
            this.Name = "frmPenerimaanSPM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Terima Berkas";
            this.Load += new System.EventHandler(this.frmPenerimaanSPM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridTerima)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlSKPD ctrlSKPD1;
        private ctrlSPP ctrlSPP1;
        private System.Windows.Forms.Button cmdBaru;
        private System.Windows.Forms.TextBox txtNoUrut;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.TextBox txtKeterangan;
        private System.Windows.Forms.Button cmdSimpan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView gridTerima;
        private System.Windows.Forms.Button cmdPanggil;
        private System.Windows.Forms.Label label5;
        private ctrlTanggal ctrlTanggal1;
        private System.Windows.Forms.Button cmdHariIni;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomorUrut;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKPD;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoSPM;
        private System.Windows.Forms.DataGridViewTextBoxColumn Keterngan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jumlah;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tanngal;
        private ctrlPencarian ctrlPencarian1;
        private System.Windows.Forms.Button cmdExcell;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.Label label6;
        private ctrlTanggal ctrlTanggal2;
        private System.Windows.Forms.Label Tanggal;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker Timex;
    }
}