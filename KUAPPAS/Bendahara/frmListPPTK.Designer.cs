namespace KUAPPAS.Bendahara
{
    partial class frmListPPTK
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
            this.gridPPTK = new System.Windows.Forms.DataGridView();
            this.NoUrut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jabatan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.txtNIP = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNamaJabatan = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlNavigation1 = new KUAPPAS.ctrlNavigation();
            this.ctrlSKPD1 = new KUAPPAS.ctrlSKPD();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            ((System.ComponentModel.ISupportInitialize)(this.gridPPTK)).BeginInit();
            this.SuspendLayout();
            // 
            // gridPPTK
            // 
            this.gridPPTK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridPPTK.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPPTK.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NoUrut,
            this.No,
            this.Nama,
            this.NIP,
            this.Jabatan});
            this.gridPPTK.Location = new System.Drawing.Point(0, 129);
            this.gridPPTK.Name = "gridPPTK";
            this.gridPPTK.Size = new System.Drawing.Size(530, 389);
            this.gridPPTK.TabIndex = 0;
            this.gridPPTK.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPPTK_CellClick);
            this.gridPPTK.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPPTK_CellContentClick);
            // 
            // NoUrut
            // 
            this.NoUrut.HeaderText = "Column1";
            this.NoUrut.Name = "NoUrut";
            this.NoUrut.Visible = false;
            // 
            // No
            // 
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.Width = 30;
            // 
            // Nama
            // 
            this.Nama.HeaderText = "Nama";
            this.Nama.Name = "Nama";
            this.Nama.ReadOnly = true;
            this.Nama.Width = 150;
            // 
            // NIP
            // 
            this.NIP.HeaderText = "NIP";
            this.NIP.Name = "NIP";
            this.NIP.ReadOnly = true;
            this.NIP.Width = 150;
            // 
            // Jabatan
            // 
            this.Jabatan.HeaderText = "Jabatan";
            this.Jabatan.Name = "Jabatan";
            this.Jabatan.ReadOnly = true;
            this.Jabatan.Width = 150;
            // 
            // txtNama
            // 
            this.txtNama.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNama.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNama.Location = new System.Drawing.Point(646, 208);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(412, 21);
            this.txtNama.TabIndex = 48;
            // 
            // txtNIP
            // 
            this.txtNIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNIP.Location = new System.Drawing.Point(646, 231);
            this.txtNIP.Name = "txtNIP";
            this.txtNIP.Size = new System.Drawing.Size(412, 21);
            this.txtNIP.TabIndex = 49;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(547, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 50;
            this.label5.Text = "Nama";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(547, 235);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 13);
            this.label11.TabIndex = 51;
            this.label11.Text = "NIP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(547, 257);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 15);
            this.label4.TabIndex = 52;
            this.label4.Text = "Nama Jabatan";
            // 
            // txtNamaJabatan
            // 
            this.txtNamaJabatan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNamaJabatan.Location = new System.Drawing.Point(646, 256);
            this.txtNamaJabatan.Name = "txtNamaJabatan";
            this.txtNamaJabatan.Size = new System.Drawing.Size(412, 20);
            this.txtNamaJabatan.TabIndex = 53;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 54;
            this.label1.Text = "OPD";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "No";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 30;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Nama";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "NIP";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Jabatan";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 150;
            // 
            // ctrlNavigation1
            // 
            this.ctrlNavigation1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlNavigation1.BackColor = System.Drawing.Color.DimGray;
            this.ctrlNavigation1.Location = new System.Drawing.Point(536, 129);
            this.ctrlNavigation1.Name = "ctrlNavigation1";
            this.ctrlNavigation1.Size = new System.Drawing.Size(610, 35);
            this.ctrlNavigation1.TabIndex = 55;
            this.ctrlNavigation1.OnAdd += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnAdd);
            this.ctrlNavigation1.OnSave += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnSave);
            this.ctrlNavigation1.OnDelete += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnDelete);
            this.ctrlNavigation1.Load += new System.EventHandler(this.ctrlNavigation1_Load);
            // 
            // ctrlSKPD1
            // 
            this.ctrlSKPD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSKPD1.Location = new System.Drawing.Point(147, 96);
            this.ctrlSKPD1.Name = "ctrlSKPD1";
            this.ctrlSKPD1.Size = new System.Drawing.Size(536, 27);
            this.ctrlSKPD1.TabIndex = 2;
            this.ctrlSKPD1.OnChanged += new KUAPPAS.ctrlSKPD.ValueChangedEventHandler(this.ctrlSKPD1_OnChanged);
            this.ctrlSKPD1.Load += new System.EventHandler(this.ctrlSKPD1_Load);
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1058, 83);
            this.ctrlHeader1.TabIndex = 1;
            // 
            // frmListPPTK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 525);
            this.Controls.Add(this.ctrlNavigation1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNamaJabatan);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtNIP);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.ctrlSKPD1);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.gridPPTK);
            this.Name = "frmListPPTK";
            this.Text = "frmListPPTK";
            this.Load += new System.EventHandler(this.frmListPPTK_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridPPTK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridPPTK;
        private ctrlHeader ctrlHeader1;
        private ctrlSKPD ctrlSKPD1;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.TextBox txtNIP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNamaJabatan;
        private System.Windows.Forms.Label label1;
        private ctrlNavigation ctrlNavigation1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoUrut;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn NIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jabatan;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    }
}