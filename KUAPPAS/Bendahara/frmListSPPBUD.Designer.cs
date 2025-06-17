namespace KUAPPAS.Bendahara
{
    partial class frmListSPPBUD
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gridSPP = new System.Windows.Forms.DataGridView();
            this.NoUrut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UbahSPP = new System.Windows.Forms.DataGridViewButtonColumn();
            this.PilihSPP = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NoSPP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TanggalSPP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoSPM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TanggalSPM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TanggalCair = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoSP2D = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Uraian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jumlah = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TerbitSPM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtNoSP2D = new System.Windows.Forms.TextBox();
            this.txtNoSPM = new System.Windows.Forms.TextBox();
            this.txtNoSPP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlJenisSPP1 = new KUAPPAS.ctrlJenisSPP();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.cmdAddSPP = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSPP)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(-4, 95);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gridSPP);
            this.splitContainer1.Size = new System.Drawing.Size(789, 327);
            this.splitContainer1.SplitterDistance = 206;
            this.splitContainer1.TabIndex = 0;
            // 
            // gridSPP
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridSPP.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridSPP.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridSPP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridSPP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NoUrut,
            this.UbahSPP,
            this.PilihSPP,
            this.NoSPP,
            this.TanggalSPP,
            this.NoSPM,
            this.TanggalSPM,
            this.TanggalCair,
            this.NoSP2D,
            this.Uraian,
            this.Jumlah,
            this.TerbitSPM});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridSPP.DefaultCellStyle = dataGridViewCellStyle4;
            this.gridSPP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSPP.Location = new System.Drawing.Point(0, 0);
            this.gridSPP.Name = "gridSPP";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridSPP.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gridSPP.Size = new System.Drawing.Size(789, 206);
            this.gridSPP.TabIndex = 1;
            this.gridSPP.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSPP_CellContentClick);
            // 
            // NoUrut
            // 
            this.NoUrut.HeaderText = "No Urut";
            this.NoUrut.Name = "NoUrut";
            this.NoUrut.ReadOnly = true;
            this.NoUrut.Visible = false;
            // 
            // UbahSPP
            // 
            this.UbahSPP.HeaderText = "Ubah SPP";
            this.UbahSPP.Name = "UbahSPP";
            this.UbahSPP.ReadOnly = true;
            this.UbahSPP.Width = 40;
            // 
            // PilihSPP
            // 
            this.PilihSPP.HeaderText = "Pilih";
            this.PilihSPP.Name = "PilihSPP";
            this.PilihSPP.Width = 40;
            // 
            // NoSPP
            // 
            this.NoSPP.HeaderText = "No SPP";
            this.NoSPP.Name = "NoSPP";
            this.NoSPP.ReadOnly = true;
            this.NoSPP.Width = 80;
            // 
            // TanggalSPP
            // 
            this.TanggalSPP.HeaderText = "Tanggal SPP";
            this.TanggalSPP.Name = "TanggalSPP";
            this.TanggalSPP.ReadOnly = true;
            this.TanggalSPP.Width = 80;
            // 
            // NoSPM
            // 
            this.NoSPM.HeaderText = "No SPM";
            this.NoSPM.Name = "NoSPM";
            this.NoSPM.ReadOnly = true;
            this.NoSPM.Width = 80;
            // 
            // TanggalSPM
            // 
            this.TanggalSPM.HeaderText = "Tanggal SPM";
            this.TanggalSPM.Name = "TanggalSPM";
            this.TanggalSPM.ReadOnly = true;
            this.TanggalSPM.Width = 80;
            // 
            // TanggalCair
            // 
            this.TanggalCair.HeaderText = "Tanggal Cair";
            this.TanggalCair.Name = "TanggalCair";
            this.TanggalCair.ReadOnly = true;
            this.TanggalCair.Width = 80;
            // 
            // NoSP2D
            // 
            this.NoSP2D.HeaderText = "No SP2D";
            this.NoSP2D.Name = "NoSP2D";
            this.NoSP2D.ReadOnly = true;
            this.NoSP2D.Width = 80;
            // 
            // Uraian
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Uraian.DefaultCellStyle = dataGridViewCellStyle2;
            this.Uraian.HeaderText = "Uraian";
            this.Uraian.Name = "Uraian";
            this.Uraian.ReadOnly = true;
            this.Uraian.Width = 400;
            // 
            // Jumlah
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = null;
            this.Jumlah.DefaultCellStyle = dataGridViewCellStyle3;
            this.Jumlah.HeaderText = "Jumlah";
            this.Jumlah.Name = "Jumlah";
            this.Jumlah.ReadOnly = true;
            // 
            // TerbitSPM
            // 
            this.TerbitSPM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TerbitSPM.HeaderText = "";
            this.TerbitSPM.Name = "TerbitSPM";
            this.TerbitSPM.ReadOnly = true;
            this.TerbitSPM.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TerbitSPM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtNoSP2D
            // 
            this.txtNoSP2D.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoSP2D.Location = new System.Drawing.Point(370, 25);
            this.txtNoSP2D.Name = "txtNoSP2D";
            this.txtNoSP2D.Size = new System.Drawing.Size(64, 20);
            this.txtNoSP2D.TabIndex = 20;
            // 
            // txtNoSPM
            // 
            this.txtNoSPM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoSPM.Location = new System.Drawing.Point(247, 27);
            this.txtNoSPM.Name = "txtNoSPM";
            this.txtNoSPM.Size = new System.Drawing.Size(50, 20);
            this.txtNoSPM.TabIndex = 19;
            // 
            // txtNoSPP
            // 
            this.txtNoSPP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoSPP.Location = new System.Drawing.Point(119, 25);
            this.txtNoSPP.Name = "txtNoSPP";
            this.txtNoSPP.Size = new System.Drawing.Size(60, 20);
            this.txtNoSPP.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(312, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "No SP2D";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(194, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "No SPM";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "No. SPP";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Jenis SPP";
            // 
            // ctrlJenisSPP1
            // 
            this.ctrlJenisSPP1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlJenisSPP1.ID = 0;
            this.ctrlJenisSPP1.Location = new System.Drawing.Point(119, 1);
            this.ctrlJenisSPP1.Name = "ctrlJenisSPP1";
            this.ctrlJenisSPP1.Size = new System.Drawing.Size(315, 21);
            this.ctrlJenisSPP1.TabIndex = 12;
            // 
            // cmdLoad
            // 
            this.cmdLoad.BackColor = System.Drawing.SystemColors.HotTrack;
            this.cmdLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLoad.ForeColor = System.Drawing.Color.White;
            this.cmdLoad.Image = global::KUAPPAS.Properties.Resources.search;
            this.cmdLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdLoad.Location = new System.Drawing.Point(61, 51);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(133, 40);
            this.cmdLoad.TabIndex = 13;
            this.cmdLoad.Text = "Panggil Data";
            this.cmdLoad.UseVisualStyleBackColor = false;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // cmdAddSPP
            // 
            this.cmdAddSPP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.cmdAddSPP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdAddSPP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdAddSPP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAddSPP.ForeColor = System.Drawing.Color.White;
            this.cmdAddSPP.Image = global::KUAPPAS.Properties.Resources.edit_add;
            this.cmdAddSPP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdAddSPP.Location = new System.Drawing.Point(200, 60);
            this.cmdAddSPP.Name = "cmdAddSPP";
            this.cmdAddSPP.Size = new System.Drawing.Size(147, 31);
            this.cmdAddSPP.TabIndex = 21;
            this.cmdAddSPP.Text = "SPP Baru";
            this.cmdAddSPP.UseVisualStyleBackColor = false;
            this.cmdAddSPP.Click += new System.EventHandler(this.cmdAddSPP_Click);
            // 
            // frmListSPPBUD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 416);
            this.Controls.Add(this.cmdAddSPP);
            this.Controls.Add(this.txtNoSP2D);
            this.Controls.Add(this.txtNoSPM);
            this.Controls.Add(this.txtNoSPP);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlJenisSPP1);
            this.Controls.Add(this.cmdLoad);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmListSPPBUD";
            this.Text = "frmListSPPBUD";
            this.Load += new System.EventHandler(this.frmListSPPBUD_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSPP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView gridSPP;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoUrut;
        private System.Windows.Forms.DataGridViewButtonColumn UbahSPP;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PilihSPP;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoSPP;
        private System.Windows.Forms.DataGridViewTextBoxColumn TanggalSPP;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoSPM;
        private System.Windows.Forms.DataGridViewTextBoxColumn TanggalSPM;
        private System.Windows.Forms.DataGridViewTextBoxColumn TanggalCair;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoSP2D;
        private System.Windows.Forms.DataGridViewTextBoxColumn Uraian;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jumlah;
        private System.Windows.Forms.DataGridViewTextBoxColumn TerbitSPM;
        private System.Windows.Forms.TextBox txtNoSP2D;
        private System.Windows.Forms.TextBox txtNoSPM;
        private System.Windows.Forms.TextBox txtNoSPP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ctrlJenisSPP ctrlJenisSPP1;
        private System.Windows.Forms.Button cmdLoad;
        private System.Windows.Forms.Button cmdAddSPP;
    }
}