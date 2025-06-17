namespace KUAPPAS
{
    partial class frmUrusanDinas
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
            this.ctrlUrusan2 = new KUAPPAS.ctrlUrusan();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNamaSKPD = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.chkTampilkanTerkait = new System.Windows.Forms.CheckBox();
            this.cmdSimpan = new System.Windows.Forms.Button();
            this.gridUrusan = new System.Windows.Forms.DataGridView();
            this.Pilih = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlSKPD1 = new KUAPPAS.ctrlSKPD();
            ((System.ComponentModel.ISupportInitialize)(this.gridUrusan)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlUrusan2
            // 
            this.ctrlUrusan2.Location = new System.Drawing.Point(171, 159);
            this.ctrlUrusan2.Name = "ctrlUrusan2";
            this.ctrlUrusan2.Size = new System.Drawing.Size(381, 21);
            this.ctrlUrusan2.TabIndex = 54;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(56, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 53;
            this.label3.Text = "Nama SKPD";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(56, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 14);
            this.label2.TabIndex = 52;
            this.label2.Text = "Urusan";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(56, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 14);
            this.label1.TabIndex = 51;
            this.label1.Text = "ID";
            // 
            // txtNamaSKPD
            // 
            this.txtNamaSKPD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNamaSKPD.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNamaSKPD.Location = new System.Drawing.Point(171, 187);
            this.txtNamaSKPD.Multiline = true;
            this.txtNamaSKPD.Name = "txtNamaSKPD";
            this.txtNamaSKPD.Size = new System.Drawing.Size(381, 53);
            this.txtNamaSKPD.TabIndex = 50;
            // 
            // txtID
            // 
            this.txtID.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtID.Enabled = false;
            this.txtID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Location = new System.Drawing.Point(171, 132);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(381, 22);
            this.txtID.TabIndex = 49;
            // 
            // chkTampilkanTerkait
            // 
            this.chkTampilkanTerkait.AutoSize = true;
            this.chkTampilkanTerkait.Location = new System.Drawing.Point(8, 246);
            this.chkTampilkanTerkait.Name = "chkTampilkanTerkait";
            this.chkTampilkanTerkait.Size = new System.Drawing.Size(241, 17);
            this.chkTampilkanTerkait.TabIndex = 57;
            this.chkTampilkanTerkait.Text = "Tampilkan Urusan Yang dilaksanakan SKPD ";
            this.chkTampilkanTerkait.UseVisualStyleBackColor = true;
            // 
            // cmdSimpan
            // 
            this.cmdSimpan.Location = new System.Drawing.Point(8, 455);
            this.cmdSimpan.Name = "cmdSimpan";
            this.cmdSimpan.Size = new System.Drawing.Size(91, 24);
            this.cmdSimpan.TabIndex = 56;
            this.cmdSimpan.Text = "Simpan";
            this.cmdSimpan.UseVisualStyleBackColor = true;
            // 
            // gridUrusan
            // 
            this.gridUrusan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridUrusan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Pilih,
            this.ID,
            this.Kode,
            this.Nama});
            this.gridUrusan.Location = new System.Drawing.Point(8, 281);
            this.gridUrusan.Name = "gridUrusan";
            this.gridUrusan.Size = new System.Drawing.Size(642, 168);
            this.gridUrusan.TabIndex = 55;
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
            // ctrlSKPD1
            // 
            this.ctrlSKPD1.Location = new System.Drawing.Point(171, 85);
            this.ctrlSKPD1.Name = "ctrlSKPD1";
            this.ctrlSKPD1.Size = new System.Drawing.Size(381, 25);
            this.ctrlSKPD1.TabIndex = 58;
            this.ctrlSKPD1.OnChanged += new KUAPPAS.ctrlSKPD.ValueChangedEventHandler(this.ctrlSKPD1_OnChanged);
            this.ctrlSKPD1.Load += new System.EventHandler(this.ctrlSKPD1_Load);
            // 
            // frmUrusanDinas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 491);
            this.Controls.Add(this.ctrlSKPD1);
            this.Controls.Add(this.chkTampilkanTerkait);
            this.Controls.Add(this.cmdSimpan);
            this.Controls.Add(this.gridUrusan);
            this.Controls.Add(this.ctrlUrusan2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNamaSKPD);
            this.Controls.Add(this.txtID);
            this.Name = "frmUrusanDinas";
            this.Text = "frmUrusanDinas";
            this.Load += new System.EventHandler(this.frmUrusanDinas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridUrusan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlUrusan ctrlUrusan2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNamaSKPD;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.CheckBox chkTampilkanTerkait;
        private System.Windows.Forms.Button cmdSimpan;
        private System.Windows.Forms.DataGridView gridUrusan;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Pilih;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private ctrlSKPD ctrlSKPD1;

    }
}