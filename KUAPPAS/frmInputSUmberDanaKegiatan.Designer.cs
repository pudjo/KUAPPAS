namespace KUAPPAS
{
    partial class frmInputSUmberDanaKegiatan
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeProgramKegiatan1 = new KUAPPAS.TreeProgramKegiatan();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.treeSumberDana1 = new KUAPPAS.TreeSumberDana();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.cmdSimpanSUmberDana = new System.Windows.Forms.Button();
            this.lbllNamaSubKegiatan = new System.Windows.Forms.Label();
            this.lblSubKegiatan = new System.Windows.Forms.Label();
            this.gridSumberDana = new System.Windows.Forms.DataGridView();
            this.IDSD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pilih = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NamaSumberDana = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JumlahSD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.txtDPA = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSumberDana)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 80);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeProgramKegiatan1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1164, 429);
            this.splitContainer1.SplitterDistance = 579;
            this.splitContainer1.TabIndex = 8;
            // 
            // treeProgramKegiatan1
            // 
            this.treeProgramKegiatan1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeProgramKegiatan1.Location = new System.Drawing.Point(0, 0);
            this.treeProgramKegiatan1.Name = "treeProgramKegiatan1";
            this.treeProgramKegiatan1.Profile = 2;
            this.treeProgramKegiatan1.Size = new System.Drawing.Size(579, 429);
            this.treeProgramKegiatan1.TabIndex = 3;
            this.treeProgramKegiatan1.SubKegiatanChanged += new KUAPPAS.TreeProgramKegiatan.SelectedSubKegiatanEventHandler(this.treeProgramKegiatan1_SubKegiatanChanged);
            this.treeProgramKegiatan1.Load += new System.EventHandler(this.treeProgramKegiatan1_Load);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.treeSumberDana1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Panel2.Controls.Add(this.txtDPA);
            this.splitContainer2.Panel2.Controls.Add(this.txtJumlah);
            this.splitContainer2.Panel2.Controls.Add(this.cmdSimpanSUmberDana);
            this.splitContainer2.Panel2.Controls.Add(this.lbllNamaSubKegiatan);
            this.splitContainer2.Panel2.Controls.Add(this.lblSubKegiatan);
            this.splitContainer2.Panel2.Controls.Add(this.gridSumberDana);
            this.splitContainer2.Size = new System.Drawing.Size(581, 429);
            this.splitContainer2.SplitterDistance = 164;
            this.splitContainer2.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(337, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Sila Pilih Sumber Dana dengan double klik  pada tingkatan paling rinci";
            // 
            // treeSumberDana1
            // 
            this.treeSumberDana1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeSumberDana1.Location = new System.Drawing.Point(3, 25);
            this.treeSumberDana1.Name = "treeSumberDana1";
            this.treeSumberDana1.Size = new System.Drawing.Size(575, 136);
            this.treeSumberDana1.TabIndex = 6;
            this.treeSumberDana1.DoubleClicking += new KUAPPAS.TreeSumberDana.ValueChangedEventHandler(this.treeSumberDana1_DoubleClicking);
            this.treeSumberDana1.Load += new System.EventHandler(this.treeSumberDana1_Load);
            // 
            // txtJumlah
            // 
            this.txtJumlah.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJumlah.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlah.Location = new System.Drawing.Point(381, 38);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(197, 26);
            this.txtJumlah.TabIndex = 27;
            this.txtJumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmdSimpanSUmberDana
            // 
            this.cmdSimpanSUmberDana.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSimpanSUmberDana.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmdSimpanSUmberDana.Location = new System.Drawing.Point(381, 7);
            this.cmdSimpanSUmberDana.Name = "cmdSimpanSUmberDana";
            this.cmdSimpanSUmberDana.Size = new System.Drawing.Size(197, 31);
            this.cmdSimpanSUmberDana.TabIndex = 26;
            this.cmdSimpanSUmberDana.Text = "Simpan";
            this.cmdSimpanSUmberDana.UseVisualStyleBackColor = false;
            this.cmdSimpanSUmberDana.Click += new System.EventHandler(this.cmdSimpanSUmberDana_Click);
            // 
            // lbllNamaSubKegiatan
            // 
            this.lbllNamaSubKegiatan.AutoSize = true;
            this.lbllNamaSubKegiatan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbllNamaSubKegiatan.Location = new System.Drawing.Point(73, 7);
            this.lbllNamaSubKegiatan.Name = "lbllNamaSubKegiatan";
            this.lbllNamaSubKegiatan.Size = new System.Drawing.Size(17, 16);
            this.lbllNamaSubKegiatan.TabIndex = 25;
            this.lbllNamaSubKegiatan.Text = "...";
            // 
            // lblSubKegiatan
            // 
            this.lblSubKegiatan.AutoSize = true;
            this.lblSubKegiatan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubKegiatan.Location = new System.Drawing.Point(23, 7);
            this.lblSubKegiatan.Name = "lblSubKegiatan";
            this.lblSubKegiatan.Size = new System.Drawing.Size(44, 16);
            this.lblSubKegiatan.TabIndex = 24;
            this.lblSubKegiatan.Text = "Kode";
            // 
            // gridSumberDana
            // 
            this.gridSumberDana.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSumberDana.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridSumberDana.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSumberDana.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDSD,
            this.Pilih,
            this.NamaSumberDana,
            this.JumlahSD});
            this.gridSumberDana.Location = new System.Drawing.Point(3, 78);
            this.gridSumberDana.Name = "gridSumberDana";
            this.gridSumberDana.Size = new System.Drawing.Size(575, 180);
            this.gridSumberDana.TabIndex = 18;
            this.gridSumberDana.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSumberDana_CellEndEdit);
            // 
            // IDSD
            // 
            this.IDSD.HeaderText = "ID";
            this.IDSD.Name = "IDSD";
            this.IDSD.ReadOnly = true;
            this.IDSD.Visible = false;
            // 
            // Pilih
            // 
            this.Pilih.HeaderText = "Pilih";
            this.Pilih.Name = "Pilih";
            this.Pilih.Width = 40;
            // 
            // NamaSumberDana
            // 
            this.NamaSumberDana.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NamaSumberDana.HeaderText = "Nama Sumber Dana";
            this.NamaSumberDana.Name = "NamaSumberDana";
            this.NamaSumberDana.ReadOnly = true;
            // 
            // JumlahSD
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.JumlahSD.DefaultCellStyle = dataGridViewCellStyle3;
            this.JumlahSD.HeaderText = "Jumlah";
            this.JumlahSD.Name = "JumlahSD";
            this.JumlahSD.Width = 200;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Nama Sumber Dana";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn3.HeaderText = "Jumlah";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(0, 35);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(566, 44);
            this.ctrlDinas1.TabIndex = 7;
            this.ctrlDinas1.OnChanged += new KUAPPAS.ctrlDinas.ValueChangedEventHandler(this.ctrlDinas1_OnChanged);
            // 
            // txtDPA
            // 
            this.txtDPA.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDPA.Location = new System.Drawing.Point(76, 39);
            this.txtDPA.Name = "txtDPA";
            this.txtDPA.Size = new System.Drawing.Size(240, 26);
            this.txtDPA.TabIndex = 28;
            this.txtDPA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "DPA";
            // 
            // frmInputSUmberDanaKegiatan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 521);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ctrlDinas1);
            this.Name = "frmInputSUmberDanaKegiatan";
            this.Text = "frmInputSUmberDanaKegiatan";
            this.Load += new System.EventHandler(this.frmInputSUmberDanaKegiatan_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSumberDana)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TreeSumberDana treeSumberDana1;
        private ctrlDinas ctrlDinas1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private TreeProgramKegiatan treeProgramKegiatan1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView gridSumberDana;
        private System.Windows.Forms.Label lbllNamaSubKegiatan;
        private System.Windows.Forms.Label lblSubKegiatan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdSimpanSUmberDana;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDSD;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Pilih;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaSumberDana;
        private System.Windows.Forms.DataGridViewTextBoxColumn JumlahSD;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.TextBox txtDPA;
        private System.Windows.Forms.Label label2;
    }
}