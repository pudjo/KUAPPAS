namespace KUAPPAS
{
    partial class frmSKPD
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
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKode = new System.Windows.Forms.TextBox();
            this.txtNamaSKPD = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cmdSimpan = new System.Windows.Forms.Button();
            this.chkTampilkanTerkait = new System.Windows.Forms.CheckBox();
            this.gridUrusan = new System.Windows.Forms.DataGridView();
            this.Pilih = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlNavigation3 = new KUAPPAS.ctrlNavigation();
            this.ctrlUrusan2 = new KUAPPAS.ctrlUrusan();
            this.ctrlSKPD1 = new KUAPPAS.ctrlSKPD();
            this.lblSKPD = new System.Windows.Forms.Label();
            this.txtKodeUnit = new System.Windows.Forms.TextBox();
            this.lblUK = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridUrusan)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 14);
            this.label4.TabIndex = 32;
            this.label4.Text = "Nomor SKPD";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 31;
            this.label3.Text = "Nama SKPD";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 14);
            this.label2.TabIndex = 30;
            this.label2.Text = "Urusan";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 14);
            this.label1.TabIndex = 29;
            this.label1.Text = "ID";
            // 
            // txtKode
            // 
            this.txtKode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKode.Location = new System.Drawing.Point(140, 187);
            this.txtKode.Name = "txtKode";
            this.txtKode.Size = new System.Drawing.Size(381, 22);
            this.txtKode.TabIndex = 28;
            // 
            // txtNamaSKPD
            // 
            this.txtNamaSKPD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNamaSKPD.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNamaSKPD.Location = new System.Drawing.Point(140, 131);
            this.txtNamaSKPD.Multiline = true;
            this.txtNamaSKPD.Name = "txtNamaSKPD";
            this.txtNamaSKPD.Size = new System.Drawing.Size(381, 53);
            this.txtNamaSKPD.TabIndex = 27;
            // 
            // txtID
            // 
            this.txtID.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtID.Enabled = false;
            this.txtID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Location = new System.Drawing.Point(140, 41);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(381, 22);
            this.txtID.TabIndex = 26;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(-2, 241);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(659, 264);
            this.tabControl1.TabIndex = 46;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cmdSimpan);
            this.tabPage1.Controls.Add(this.chkTampilkanTerkait);
            this.tabPage1.Controls.Add(this.gridUrusan);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(651, 238);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Urusan Pemerintahan";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cmdSimpan
            // 
            this.cmdSimpan.Location = new System.Drawing.Point(500, 203);
            this.cmdSimpan.Name = "cmdSimpan";
            this.cmdSimpan.Size = new System.Drawing.Size(145, 28);
            this.cmdSimpan.TabIndex = 1;
            this.cmdSimpan.Text = "Simpan";
            this.cmdSimpan.UseVisualStyleBackColor = true;
            this.cmdSimpan.Click += new System.EventHandler(this.cmdSimpan_Click);
            // 
            // chkTampilkanTerkait
            // 
            this.chkTampilkanTerkait.AutoSize = true;
            this.chkTampilkanTerkait.Location = new System.Drawing.Point(13, 9);
            this.chkTampilkanTerkait.Name = "chkTampilkanTerkait";
            this.chkTampilkanTerkait.Size = new System.Drawing.Size(241, 17);
            this.chkTampilkanTerkait.TabIndex = 2;
            this.chkTampilkanTerkait.Text = "Tampilkan Urusan Yang dilaksanakan SKPD ";
            this.chkTampilkanTerkait.UseVisualStyleBackColor = true;
            this.chkTampilkanTerkait.CheckedChanged += new System.EventHandler(this.chkTampilkanTerkait_CheckedChanged);
            // 
            // gridUrusan
            // 
            this.gridUrusan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridUrusan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Pilih,
            this.ID,
            this.Kode,
            this.Nama});
            this.gridUrusan.Location = new System.Drawing.Point(3, 35);
            this.gridUrusan.Name = "gridUrusan";
            this.gridUrusan.Size = new System.Drawing.Size(642, 142);
            this.gridUrusan.TabIndex = 0;
            // 
            // Pilih
            // 
            this.Pilih.HeaderText = "Pilih";
            this.Pilih.Name = "Pilih";
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ID.Visible = false;
            // 
            // Kode
            // 
            this.Kode.HeaderText = "Kode";
            this.Kode.Name = "Kode";
            this.Kode.ReadOnly = true;
            this.Kode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Kode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Kode.Visible = false;
            this.Kode.Width = 50;
            // 
            // Nama
            // 
            this.Nama.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Nama.HeaderText = "Nama";
            this.Nama.Name = "Nama";
            this.Nama.ReadOnly = true;
            this.Nama.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Nama.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 450);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(661, 22);
            this.statusStrip1.TabIndex = 47;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Kode";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Visible = false;
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "Nama";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ctrlNavigation3
            // 
            this.ctrlNavigation3.BackColor = System.Drawing.SystemColors.Highlight;
            this.ctrlNavigation3.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlNavigation3.Location = new System.Drawing.Point(0, 0);
            this.ctrlNavigation3.Name = "ctrlNavigation3";
            this.ctrlNavigation3.Size = new System.Drawing.Size(661, 35);
            this.ctrlNavigation3.TabIndex = 49;
            this.ctrlNavigation3.OnAdd += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation3_OnAdd);
            this.ctrlNavigation3.OnSave += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation3_OnSave);
            this.ctrlNavigation3.OnDelete += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation3_OnDelete);
            this.ctrlNavigation3.Load += new System.EventHandler(this.ctrlNavigation3_Load);
            // 
            // ctrlUrusan2
            // 
            this.ctrlUrusan2.Location = new System.Drawing.Point(140, 68);
            this.ctrlUrusan2.Name = "ctrlUrusan2";
            this.ctrlUrusan2.Size = new System.Drawing.Size(381, 21);
            this.ctrlUrusan2.TabIndex = 48;
            // 
            // ctrlSKPD1
            // 
            this.ctrlSKPD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSKPD1.Location = new System.Drawing.Point(142, 94);
            this.ctrlSKPD1.Name = "ctrlSKPD1";
            this.ctrlSKPD1.Size = new System.Drawing.Size(378, 28);
            this.ctrlSKPD1.TabIndex = 54;
            // 
            // lblSKPD
            // 
            this.lblSKPD.AutoSize = true;
            this.lblSKPD.Location = new System.Drawing.Point(28, 94);
            this.lblSKPD.Name = "lblSKPD";
            this.lblSKPD.Size = new System.Drawing.Size(36, 13);
            this.lblSKPD.TabIndex = 55;
            this.lblSKPD.Text = "SKPD";
            // 
            // txtKodeUnit
            // 
            this.txtKodeUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKodeUnit.Location = new System.Drawing.Point(142, 215);
            this.txtKodeUnit.Name = "txtKodeUnit";
            this.txtKodeUnit.Size = new System.Drawing.Size(64, 20);
            this.txtKodeUnit.TabIndex = 56;
            // 
            // lblUK
            // 
            this.lblUK.AutoSize = true;
            this.lblUK.Location = new System.Drawing.Point(25, 222);
            this.lblUK.Name = "lblUK";
            this.lblUK.Size = new System.Drawing.Size(112, 13);
            this.lblUK.TabIndex = 57;
            this.lblUK.Text = "Nomor Unit Organisasi";
            // 
            // frmSKPD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 472);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblUK);
            this.Controls.Add(this.txtKodeUnit);
            this.Controls.Add(this.lblSKPD);
            this.Controls.Add(this.ctrlSKPD1);
            this.Controls.Add(this.ctrlNavigation3);
            this.Controls.Add(this.ctrlUrusan2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtKode);
            this.Controls.Add(this.txtNamaSKPD);
            this.Controls.Add(this.txtID);
            this.Name = "frmSKPD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SKPD";
            this.Load += new System.EventHandler(this.frmSKPD_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridUrusan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlNavigation ctrlNavigation1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKode;
        private System.Windows.Forms.TextBox txtNamaSKPD;
        private System.Windows.Forms.TextBox txtID;
        private ctrlUrusan ctrlUrusan1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button cmdSimpan;
        private System.Windows.Forms.DataGridView gridUrusan;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Pilih;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private ctrlUrusan ctrlUrusan2;
        private ctrlNavigation ctrlNavigation3;
        private System.Windows.Forms.CheckBox chkTampilkanTerkait;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private ctrlSKPD ctrlSKPD1;
        private System.Windows.Forms.Label lblSKPD;
        private System.Windows.Forms.TextBox txtKodeUnit;
        private System.Windows.Forms.Label lblUK;
    }
}