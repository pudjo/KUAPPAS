namespace KUAPPAS
{
    partial class frmMasterSumberDana
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
            this.gridSumberDana = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusUpdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.cmdTambah = new System.Windows.Forms.Button();
            this.cmdSimpan = new System.Windows.Forms.Button();
            this.cmdHapus = new System.Windows.Forms.Button();
            this.treeSumberDana1 = new KUAPPAS.TreeSumberDana();
            this.txtIDParent = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRoot = new System.Windows.Forms.TextBox();
            this.chkLeaf = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridSumberDana)).BeginInit();
            this.SuspendLayout();
            // 
            // gridSumberDana
            // 
            this.gridSumberDana.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSumberDana.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.StatusUpdate,
            this.Nama});
            this.gridSumberDana.Location = new System.Drawing.Point(434, 48);
            this.gridSumberDana.Name = "gridSumberDana";
            this.gridSumberDana.Size = new System.Drawing.Size(274, 24);
            this.gridSumberDana.TabIndex = 0;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // StatusUpdate
            // 
            this.StatusUpdate.HeaderText = "StatusUpdate";
            this.StatusUpdate.Name = "StatusUpdate";
            this.StatusUpdate.ReadOnly = true;
            this.StatusUpdate.Visible = false;
            // 
            // Nama
            // 
            this.Nama.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Nama.HeaderText = "Nama";
            this.Nama.Name = "Nama";
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1148, 41);
            this.ctrlHeader1.TabIndex = 1;
            // 
            // cmdTambah
            // 
            this.cmdTambah.Location = new System.Drawing.Point(655, 304);
            this.cmdTambah.Name = "cmdTambah";
            this.cmdTambah.Size = new System.Drawing.Size(102, 37);
            this.cmdTambah.TabIndex = 2;
            this.cmdTambah.Text = "Tambah Rincian";
            this.cmdTambah.UseVisualStyleBackColor = true;
            this.cmdTambah.Click += new System.EventHandler(this.cmdTambah_Click);
            // 
            // cmdSimpan
            // 
            this.cmdSimpan.Location = new System.Drawing.Point(774, 304);
            this.cmdSimpan.Name = "cmdSimpan";
            this.cmdSimpan.Size = new System.Drawing.Size(102, 34);
            this.cmdSimpan.TabIndex = 3;
            this.cmdSimpan.Text = "Simpan";
            this.cmdSimpan.UseVisualStyleBackColor = true;
            this.cmdSimpan.Click += new System.EventHandler(this.cmdSimpan_Click);
            // 
            // cmdHapus
            // 
            this.cmdHapus.Location = new System.Drawing.Point(1006, 307);
            this.cmdHapus.Name = "cmdHapus";
            this.cmdHapus.Size = new System.Drawing.Size(81, 34);
            this.cmdHapus.TabIndex = 4;
            this.cmdHapus.Text = "Hapus";
            this.cmdHapus.UseVisualStyleBackColor = true;
            this.cmdHapus.Click += new System.EventHandler(this.cmdHapus_Click);
            // 
            // treeSumberDana1
            // 
            this.treeSumberDana1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeSumberDana1.Location = new System.Drawing.Point(0, 90);
            this.treeSumberDana1.Name = "treeSumberDana1";
            this.treeSumberDana1.Size = new System.Drawing.Size(619, 343);
            this.treeSumberDana1.TabIndex = 5;
            this.treeSumberDana1.Changed += new KUAPPAS.TreeSumberDana.ValueChangedEventHandler(this.treeSumberDana1_Changed);
            this.treeSumberDana1.Load += new System.EventHandler(this.treeSumberDana1_Load);
            // 
            // txtIDParent
            // 
            this.txtIDParent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIDParent.Location = new System.Drawing.Point(751, 135);
            this.txtIDParent.Name = "txtIDParent";
            this.txtIDParent.Size = new System.Drawing.Size(150, 26);
            this.txtIDParent.TabIndex = 6;
            // 
            // txtID
            // 
            this.txtID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Location = new System.Drawing.Point(751, 162);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(150, 26);
            this.txtID.TabIndex = 7;
            // 
            // txtNama
            // 
            this.txtNama.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNama.Location = new System.Drawing.Point(751, 189);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(385, 26);
            this.txtNama.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(652, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 18);
            this.label1.TabIndex = 9;
            this.label1.Text = "Kode Induk";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(652, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 18);
            this.label2.TabIndex = 10;
            this.label2.Text = "ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(652, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 18);
            this.label3.TabIndex = 11;
            this.label3.Text = "Nama";
            // 
            // txtRoot
            // 
            this.txtRoot.Location = new System.Drawing.Point(751, 233);
            this.txtRoot.Name = "txtRoot";
            this.txtRoot.Size = new System.Drawing.Size(100, 20);
            this.txtRoot.TabIndex = 12;
            // 
            // chkLeaf
            // 
            this.chkLeaf.AutoSize = true;
            this.chkLeaf.Location = new System.Drawing.Point(751, 260);
            this.chkLeaf.Name = "chkLeaf";
            this.chkLeaf.Size = new System.Drawing.Size(95, 17);
            this.chkLeaf.TabIndex = 13;
            this.chkLeaf.Text = "Punya Rincian";
            this.chkLeaf.UseVisualStyleBackColor = true;
            // 
            // frmMasterSumberDana
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 445);
            this.Controls.Add(this.chkLeaf);
            this.Controls.Add(this.txtRoot);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.txtIDParent);
            this.Controls.Add(this.treeSumberDana1);
            this.Controls.Add(this.cmdHapus);
            this.Controls.Add(this.cmdSimpan);
            this.Controls.Add(this.cmdTambah);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.gridSumberDana);
            this.Name = "frmMasterSumberDana";
            this.Text = "Sumber Dana";
            this.Load += new System.EventHandler(this.frmMasterSumberDana_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSumberDana)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridSumberDana;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.Button cmdTambah;
        private System.Windows.Forms.Button cmdSimpan;
        private System.Windows.Forms.Button cmdHapus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private TreeSumberDana treeSumberDana1;
        private System.Windows.Forms.TextBox txtIDParent;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRoot;
        private System.Windows.Forms.CheckBox chkLeaf;
    }
}