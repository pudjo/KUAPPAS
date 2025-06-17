namespace KUAPPAS
{
    partial class frmPerbaikiKodeProgramKegiatan
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
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.ctrlUrusanPemerintahan1 = new KUAPPAS.ctrlUrusanPemerintahan();
            this.ctrlKegiatanAPBD2 = new KUAPPAS.ctrlKegiatanAPBD();
            this.ctrlProgram1 = new KUAPPAS.ctrlProgram();
            this.cmdPerbaiki = new System.Windows.Forms.Button();
            this.lblUrusan = new System.Windows.Forms.Label();
            this.lblKegiatan = new System.Windows.Forms.Label();
            this.lblProgram = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.treeProgramKegiatan1 = new KUAPPAS.TreeProgramKegiatan();
            this.ctrlUrusan1 = new KUAPPAS.ctrlUrusan();
            this.ctrlProgram2 = new KUAPPAS.ctrlProgram();
            this.ctrlKegiatan1 = new KUAPPAS.ctrlKegiatan();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Pilih = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(92, 30);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(658, 26);
            this.ctrlDinas1.TabIndex = 0;
            this.ctrlDinas1.OnChanged += new KUAPPAS.ctrlDinas.ValueChangedEventHandler(this.ctrlDinas1_OnChanged);
            // 
            // ctrlUrusanPemerintahan1
            // 
            this.ctrlUrusanPemerintahan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlUrusanPemerintahan1.Location = new System.Drawing.Point(575, 225);
            this.ctrlUrusanPemerintahan1.Name = "ctrlUrusanPemerintahan1";
            this.ctrlUrusanPemerintahan1.Size = new System.Drawing.Size(502, 34);
            this.ctrlUrusanPemerintahan1.TabIndex = 3;
            this.ctrlUrusanPemerintahan1.OnChanged += new KUAPPAS.ctrlUrusanPemerintahan.ValueChangedEventHandler(this.ctrlUrusanPemerintahan1_OnChanged);
            // 
            // ctrlKegiatanAPBD2
            // 
            this.ctrlKegiatanAPBD2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlKegiatanAPBD2.Location = new System.Drawing.Point(575, 302);
            this.ctrlKegiatanAPBD2.Name = "ctrlKegiatanAPBD2";
            this.ctrlKegiatanAPBD2.Size = new System.Drawing.Size(502, 33);
            this.ctrlKegiatanAPBD2.TabIndex = 4;
            // 
            // ctrlProgram1
            // 
            this.ctrlProgram1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlProgram1.Location = new System.Drawing.Point(575, 265);
            this.ctrlProgram1.Name = "ctrlProgram1";
            this.ctrlProgram1.Size = new System.Drawing.Size(502, 31);
            this.ctrlProgram1.TabIndex = 5;
            this.ctrlProgram1.OnChanged += new KUAPPAS.ctrlProgram.ValueChangedEventHandler(this.ctrlProgram1_OnChanged);
            // 
            // cmdPerbaiki
            // 
            this.cmdPerbaiki.Location = new System.Drawing.Point(585, 367);
            this.cmdPerbaiki.Name = "cmdPerbaiki";
            this.cmdPerbaiki.Size = new System.Drawing.Size(95, 34);
            this.cmdPerbaiki.TabIndex = 6;
            this.cmdPerbaiki.Text = "Perbaiki";
            this.cmdPerbaiki.UseVisualStyleBackColor = true;
            this.cmdPerbaiki.Click += new System.EventHandler(this.cmdPerbaiki_Click);
            // 
            // lblUrusan
            // 
            this.lblUrusan.AutoSize = true;
            this.lblUrusan.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUrusan.Location = new System.Drawing.Point(572, 116);
            this.lblUrusan.Name = "lblUrusan";
            this.lblUrusan.Size = new System.Drawing.Size(138, 14);
            this.lblUrusan.TabIndex = 10;
            this.lblUrusan.Text = "Urusan Pemerintahan";
            // 
            // lblKegiatan
            // 
            this.lblKegiatan.AutoSize = true;
            this.lblKegiatan.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKegiatan.Location = new System.Drawing.Point(572, 157);
            this.lblKegiatan.Name = "lblKegiatan";
            this.lblKegiatan.Size = new System.Drawing.Size(57, 16);
            this.lblKegiatan.TabIndex = 9;
            this.lblKegiatan.Text = "Kegiatan";
            // 
            // lblProgram
            // 
            this.lblProgram.AutoSize = true;
            this.lblProgram.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgram.Location = new System.Drawing.Point(572, 134);
            this.lblProgram.Name = "lblProgram";
            this.lblProgram.Size = new System.Drawing.Size(57, 16);
            this.lblProgram.TabIndex = 8;
            this.lblProgram.Text = "Program";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(9, 53);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(560, 518);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.treeProgramKegiatan1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(552, 492);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Controls.Add(this.ctrlKegiatan1);
            this.tabPage2.Controls.Add(this.ctrlProgram2);
            this.tabPage2.Controls.Add(this.ctrlUrusan1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(552, 492);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // treeProgramKegiatan1
            // 
            this.treeProgramKegiatan1.Location = new System.Drawing.Point(0, 6);
            this.treeProgramKegiatan1.Name = "treeProgramKegiatan1";
            this.treeProgramKegiatan1.Size = new System.Drawing.Size(553, 480);
            this.treeProgramKegiatan1.TabIndex = 3;
            this.treeProgramKegiatan1.KegiatanChanged += new KUAPPAS.TreeProgramKegiatan.SelectedKegiatanEventHandler(this.treeProgramKegiatan1_KegiatanChanged);
            // 
            // ctrlUrusan1
            // 
            this.ctrlUrusan1.Location = new System.Drawing.Point(57, 15);
            this.ctrlUrusan1.Name = "ctrlUrusan1";
            this.ctrlUrusan1.Size = new System.Drawing.Size(452, 26);
            this.ctrlUrusan1.TabIndex = 0;
            this.ctrlUrusan1.OnChanged += new KUAPPAS.ctrlUrusan.ValueChangedEventHandler(this.ctrlUrusan1_OnChanged);
            // 
            // ctrlProgram2
            // 
            this.ctrlProgram2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlProgram2.Location = new System.Drawing.Point(57, 41);
            this.ctrlProgram2.Name = "ctrlProgram2";
            this.ctrlProgram2.Size = new System.Drawing.Size(452, 26);
            this.ctrlProgram2.TabIndex = 1;
            // 
            // ctrlKegiatan1
            // 
            this.ctrlKegiatan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlKegiatan1.Location = new System.Drawing.Point(57, 76);
            this.ctrlKegiatan1.Name = "ctrlKegiatan1";
            this.ctrlKegiatan1.Size = new System.Drawing.Size(451, 22);
            this.ctrlKegiatan1.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Pilih,
            this.ID,
            this.SKPD,
            this.Nama});
            this.dataGridView1.Location = new System.Drawing.Point(10, 113);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(526, 366);
            this.dataGridView1.TabIndex = 3;
            // 
            // Pilih
            // 
            this.Pilih.HeaderText = "Pilih";
            this.Pilih.Name = "Pilih";
            this.Pilih.Width = 40;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 70;
            // 
            // SKPD
            // 
            this.SKPD.HeaderText = "SKPD";
            this.SKPD.Name = "SKPD";
            this.SKPD.ReadOnly = true;
            this.SKPD.Width = 50;
            // 
            // Nama
            // 
            this.Nama.HeaderText = "Nama";
            this.Nama.Name = "Nama";
            this.Nama.ReadOnly = true;
            this.Nama.Width = 3000;
            // 
            // frmPerbaikiKodeProgramKegiatan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 573);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblUrusan);
            this.Controls.Add(this.lblKegiatan);
            this.Controls.Add(this.lblProgram);
            this.Controls.Add(this.cmdPerbaiki);
            this.Controls.Add(this.ctrlProgram1);
            this.Controls.Add(this.ctrlKegiatanAPBD2);
            this.Controls.Add(this.ctrlUrusanPemerintahan1);
            this.Controls.Add(this.ctrlDinas1);
            this.Name = "frmPerbaikiKodeProgramKegiatan";
            this.Text = "frmPerbaikiKodeProgramKegiatan";
            this.Load += new System.EventHandler(this.frmPerbaikiKodeProgramKegiatan_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlDinas ctrlDinas1;
        private ctrlUrusanPemerintahan ctrlUrusanPemerintahan1;
        private ctrlKegiatanAPBD ctrlKegiatanAPBD2;
        private ctrlProgram ctrlProgram1;
        private System.Windows.Forms.Button cmdPerbaiki;
        private System.Windows.Forms.Label lblUrusan;
        private System.Windows.Forms.Label lblKegiatan;
        private System.Windows.Forms.Label lblProgram;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private TreeProgramKegiatan treeProgramKegiatan1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Pilih;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKPD;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private ctrlKegiatan ctrlKegiatan1;
        private ctrlProgram ctrlProgram2;
        private ctrlUrusan ctrlUrusan1;
    }
}