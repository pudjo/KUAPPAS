namespace KUAPPAS
{
    partial class frmSumberDana
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gridRekap = new KUAPPAS.TreeGridView();
            this.Uraian = new KUAPPAS.TreeGridColumn();
            this.Jumlah = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeProgramKegiatan1 = new KUAPPAS.TreeProgramKegiatan();
            this.cmdSumbeDanaAPBD = new System.Windows.Forms.Button();
            this.lblJumlah = new System.Windows.Forms.Label();
            this.cmdSimpan = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblKegiatan = new System.Windows.Forms.Label();
            this.lblProgram = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUrusan = new System.Windows.Forms.Label();
            this.gridSumberDana = new System.Windows.Forms.DataGridView();
            this.IDRekeningSumberDana = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KodeRekeningSumberDana = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaRekeningSD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.APBD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.APBDP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ctrlJenisAnggaran1 = new KUAPPAS.ctrlJenisAnggaran();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdrefresh = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRekap)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSumberDana)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(198, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Jenis DPA";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 155);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1024, 376);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridRekap);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1016, 350);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Rekap";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gridRekap
            // 
            this.gridRekap.AllowUserToAddRows = false;
            this.gridRekap.AllowUserToDeleteRows = false;
            this.gridRekap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridRekap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Uraian,
            this.Jumlah});
            this.gridRekap.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridRekap.ImageList = null;
            this.gridRekap.Location = new System.Drawing.Point(3, 6);
            this.gridRekap.Name = "gridRekap";
            this.gridRekap.Size = new System.Drawing.Size(1007, 335);
            this.gridRekap.TabIndex = 2;
            // 
            // Uraian
            // 
            this.Uraian.DefaultNodeImage = null;
            this.Uraian.HeaderText = "Uraian";
            this.Uraian.Name = "Uraian";
            this.Uraian.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Uraian.Width = 600;
            // 
            // Jumlah
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Jumlah.DefaultCellStyle = dataGridViewCellStyle1;
            this.Jumlah.HeaderText = "Jumlah";
            this.Jumlah.Name = "Jumlah";
            this.Jumlah.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Jumlah.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Jumlah.Width = 200;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1016, 350);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Sumber Dana";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeProgramKegiatan1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cmdSumbeDanaAPBD);
            this.splitContainer1.Panel2.Controls.Add(this.lblJumlah);
            this.splitContainer1.Panel2.Controls.Add(this.cmdSimpan);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.lblKegiatan);
            this.splitContainer1.Panel2.Controls.Add(this.lblProgram);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.lblUrusan);
            this.splitContainer1.Panel2.Controls.Add(this.gridSumberDana);
            this.splitContainer1.Size = new System.Drawing.Size(1010, 344);
            this.splitContainer1.SplitterDistance = 336;
            this.splitContainer1.TabIndex = 2;
            // 
            // treeProgramKegiatan1
            // 
            this.treeProgramKegiatan1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeProgramKegiatan1.Location = new System.Drawing.Point(0, 0);
            this.treeProgramKegiatan1.Name = "treeProgramKegiatan1";
            this.treeProgramKegiatan1.Size = new System.Drawing.Size(336, 344);
            this.treeProgramKegiatan1.TabIndex = 0;
            this.treeProgramKegiatan1.ProgramChanged += new KUAPPAS.TreeProgramKegiatan.SelectedProgramEventHandler(this.treeProgramKegiatan1_KegiatanChanged);
            this.treeProgramKegiatan1.KegiatanChanged += new KUAPPAS.TreeProgramKegiatan.SelectedKegiatanEventHandler(this.treeProgramKegiatan1_KegiatanChanged);
            // 
            // cmdSumbeDanaAPBD
            // 
            this.cmdSumbeDanaAPBD.Location = new System.Drawing.Point(527, 74);
            this.cmdSumbeDanaAPBD.Name = "cmdSumbeDanaAPBD";
            this.cmdSumbeDanaAPBD.Size = new System.Drawing.Size(95, 22);
            this.cmdSumbeDanaAPBD.TabIndex = 10;
            this.cmdSumbeDanaAPBD.Text = "Jadikan APBD sebagai Sumber Dana Sisa";
            this.cmdSumbeDanaAPBD.UseVisualStyleBackColor = true;
            this.cmdSumbeDanaAPBD.Click += new System.EventHandler(this.cmdSumbeDanaAPBD_Click);
            // 
            // lblJumlah
            // 
            this.lblJumlah.AutoSize = true;
            this.lblJumlah.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJumlah.Location = new System.Drawing.Point(480, 14);
            this.lblJumlah.Name = "lblJumlah";
            this.lblJumlah.Size = new System.Drawing.Size(36, 19);
            this.lblJumlah.TabIndex = 9;
            this.lblJumlah.Text = "000";
            // 
            // cmdSimpan
            // 
            this.cmdSimpan.Location = new System.Drawing.Point(28, 74);
            this.cmdSimpan.Name = "cmdSimpan";
            this.cmdSimpan.Size = new System.Drawing.Size(75, 23);
            this.cmdSimpan.TabIndex = 8;
            this.cmdSimpan.Text = "Simpan";
            this.cmdSimpan.UseVisualStyleBackColor = true;
            this.cmdSimpan.Click += new System.EventHandler(this.cmdSimpan_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Kegiatan";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Program";
            // 
            // lblKegiatan
            // 
            this.lblKegiatan.AutoSize = true;
            this.lblKegiatan.Location = new System.Drawing.Point(170, 48);
            this.lblKegiatan.Name = "lblKegiatan";
            this.lblKegiatan.Size = new System.Drawing.Size(16, 13);
            this.lblKegiatan.TabIndex = 5;
            this.lblKegiatan.Text = "...";
            // 
            // lblProgram
            // 
            this.lblProgram.AutoSize = true;
            this.lblProgram.Location = new System.Drawing.Point(170, 31);
            this.lblProgram.Name = "lblProgram";
            this.lblProgram.Size = new System.Drawing.Size(0, 13);
            this.lblProgram.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Urusan Pemerintahan";
            // 
            // lblUrusan
            // 
            this.lblUrusan.AutoSize = true;
            this.lblUrusan.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUrusan.Location = new System.Drawing.Point(167, 14);
            this.lblUrusan.Name = "lblUrusan";
            this.lblUrusan.Size = new System.Drawing.Size(41, 13);
            this.lblUrusan.TabIndex = 2;
            this.lblUrusan.Text = "label2";
            // 
            // gridSumberDana
            // 
            this.gridSumberDana.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSumberDana.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSumberDana.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDRekeningSumberDana,
            this.KodeRekeningSumberDana,
            this.NamaRekeningSD,
            this.APBD,
            this.APBDP});
            this.gridSumberDana.Location = new System.Drawing.Point(3, 103);
            this.gridSumberDana.Name = "gridSumberDana";
            this.gridSumberDana.Size = new System.Drawing.Size(662, 235);
            this.gridSumberDana.TabIndex = 1;
            this.gridSumberDana.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSumberDana_CellContentClick);
            this.gridSumberDana.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSumberDana_CellEndEdit);
            this.gridSumberDana.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSumberDana_CellEnter);
            this.gridSumberDana.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSumberDana_CellLeave);
            // 
            // IDRekeningSumberDana
            // 
            this.IDRekeningSumberDana.HeaderText = "IDRek";
            this.IDRekeningSumberDana.Name = "IDRekeningSumberDana";
            this.IDRekeningSumberDana.Visible = false;
            // 
            // KodeRekeningSumberDana
            // 
            this.KodeRekeningSumberDana.HeaderText = "Kode Rekening";
            this.KodeRekeningSumberDana.Name = "KodeRekeningSumberDana";
            this.KodeRekeningSumberDana.ReadOnly = true;
            this.KodeRekeningSumberDana.Width = 120;
            // 
            // NamaRekeningSD
            // 
            this.NamaRekeningSD.HeaderText = "Nama Rekening";
            this.NamaRekeningSD.Name = "NamaRekeningSD";
            this.NamaRekeningSD.ReadOnly = true;
            this.NamaRekeningSD.Width = 250;
            // 
            // APBD
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.APBD.DefaultCellStyle = dataGridViewCellStyle2;
            this.APBD.HeaderText = "APBD";
            this.APBD.Name = "APBD";
            this.APBD.Width = 150;
            // 
            // APBDP
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.APBDP.DefaultCellStyle = dataGridViewCellStyle3;
            this.APBDP.HeaderText = "APBD Perubahan";
            this.APBDP.Name = "APBDP";
            this.APBDP.Width = 150;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 121);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 28);
            this.button1.TabIndex = 6;
            this.button1.Text = "Import APBD";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 58);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "bersihkanKegiatan";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ctrlJenisAnggaran1
            // 
            this.ctrlJenisAnggaran1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlJenisAnggaran1.Location = new System.Drawing.Point(294, 101);
            this.ctrlJenisAnggaran1.Name = "ctrlJenisAnggaran1";
            this.ctrlJenisAnggaran1.Size = new System.Drawing.Size(429, 21);
            this.ctrlJenisAnggaran1.TabIndex = 3;
            this.ctrlJenisAnggaran1.OnChanged += new KUAPPAS.ctrlJenisAnggaran.ValueChangedEventHandler(this.ctrlJenisAnggaran1_OnChanged);
            this.ctrlJenisAnggaran1.Load += new System.EventHandler(this.ctrlJenisAnggaran1_Load);
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(198, 58);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(647, 23);
            this.ctrlDinas1.TabIndex = 2;
            this.ctrlDinas1.OnChanged += new KUAPPAS.ctrlDinas.ValueChangedEventHandler(this.ctrlDinas1_OnChanged);
            this.ctrlDinas1.Load += new System.EventHandler(this.ctrlDinas1_Load);
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1024, 52);
            this.ctrlHeader1.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "IDRek";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Kode Rekening";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 120;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Nama Rekening";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 250;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn4.HeaderText = "APBD";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // cmdrefresh
            // 
            this.cmdrefresh.Location = new System.Drawing.Point(128, 121);
            this.cmdrefresh.Name = "cmdrefresh";
            this.cmdrefresh.Size = new System.Drawing.Size(94, 28);
            this.cmdrefresh.TabIndex = 8;
            this.cmdrefresh.Text = "Refresh";
            this.cmdrefresh.UseVisualStyleBackColor = true;
            this.cmdrefresh.Click += new System.EventHandler(this.cmdrefresh_Click);
            // 
            // frmSumberDana
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 530);
            this.Controls.Add(this.cmdrefresh);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlJenisAnggaran1);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmSumberDana";
            this.Text = "frmSumberDana";
            this.Load += new System.EventHandler(this.frmSumberDana_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridRekap)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSumberDana)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlHeader ctrlHeader1;
        private ctrlDinas ctrlDinas1;
        private ctrlJenisAnggaran ctrlJenisAnggaran1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private TreeProgramKegiatan treeProgramKegiatan1;
        private System.Windows.Forms.Label lblJumlah;
        private System.Windows.Forms.Button cmdSimpan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblKegiatan;
        private System.Windows.Forms.Label lblProgram;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUrusan;
        private System.Windows.Forms.DataGridView gridSumberDana;
        private TreeGridView gridRekap;
        private TreeGridColumn Uraian;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jumlah;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button cmdSumbeDanaAPBD;
        private System.Windows.Forms.Button cmdrefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDRekeningSumberDana;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodeRekeningSumberDana;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaRekeningSD;
        private System.Windows.Forms.DataGridViewTextBoxColumn APBD;
        private System.Windows.Forms.DataGridViewTextBoxColumn APBDP;
    }
}