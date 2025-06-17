namespace KUAPPAS
{
    partial class frmIputTimTAPD
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmdSimpanTIMTAPD = new System.Windows.Forms.Button();
            this.gridTimDPA = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.chkSemuaDinas = new System.Windows.Forms.CheckBox();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.cmdHapusTimTAPD = new System.Windows.Forms.Button();
            this.ctrlJenisAnggaran1 = new KUAPPAS.ctrlJenisAnggaran();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.gridTimViewer = new System.Windows.Forms.DataGridView();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdSimpanTimViewer = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.No = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jabatan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridTimDPA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTimViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdSimpanTIMTAPD
            // 
            this.cmdSimpanTIMTAPD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmdSimpanTIMTAPD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSimpanTIMTAPD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSimpanTIMTAPD.Location = new System.Drawing.Point(2, 343);
            this.cmdSimpanTIMTAPD.Name = "cmdSimpanTIMTAPD";
            this.cmdSimpanTIMTAPD.Size = new System.Drawing.Size(144, 28);
            this.cmdSimpanTIMTAPD.TabIndex = 7;
            this.cmdSimpanTIMTAPD.Text = "Simpan TIM TAPD";
            this.cmdSimpanTIMTAPD.UseVisualStyleBackColor = false;
            this.cmdSimpanTIMTAPD.Click += new System.EventHandler(this.cmdSimpanTIMTAPD_Click);
            // 
            // gridTimDPA
            // 
            this.gridTimDPA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridTimDPA.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTimDPA.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridTimDPA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTimDPA.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.Nama,
            this.NIP,
            this.Jabatan,
            this.ID});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridTimDPA.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridTimDPA.Location = new System.Drawing.Point(2, 163);
            this.gridTimDPA.Name = "gridTimDPA";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTimDPA.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridTimDPA.Size = new System.Drawing.Size(841, 177);
            this.gridTimDPA.TabIndex = 6;
            this.gridTimDPA.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTimDPA_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 14);
            this.label1.TabIndex = 8;
            this.label1.Text = "Dinas/SKPD/Unit Organisasi";
            // 
            // chkSemuaDinas
            // 
            this.chkSemuaDinas.AutoSize = true;
            this.chkSemuaDinas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSemuaDinas.Location = new System.Drawing.Point(12, 47);
            this.chkSemuaDinas.Name = "chkSemuaDinas";
            this.chkSemuaDinas.Size = new System.Drawing.Size(162, 20);
            this.chkSemuaDinas.TabIndex = 9;
            this.chkSemuaDinas.Text = "Untuk Semua Dinas";
            this.chkSemuaDinas.UseVisualStyleBackColor = true;
            this.chkSemuaDinas.Visible = false;
            this.chkSemuaDinas.CheckedChanged += new System.EventHandler(this.chkSemuaDinas_CheckedChanged);
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(99, 69);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(660, 23);
            this.ctrlDinas1.TabIndex = 10;
            this.ctrlDinas1.OnChanged += new KUAPPAS.ctrlDinas.ValueChangedEventHandler(this.ctrlDinas1_OnChanged);
            this.ctrlDinas1.Load += new System.EventHandler(this.ctrlDinas1_Load);
            // 
            // cmdHapusTimTAPD
            // 
            this.cmdHapusTimTAPD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cmdHapusTimTAPD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdHapusTimTAPD.Location = new System.Drawing.Point(656, 128);
            this.cmdHapusTimTAPD.Name = "cmdHapusTimTAPD";
            this.cmdHapusTimTAPD.Size = new System.Drawing.Size(161, 29);
            this.cmdHapusTimTAPD.TabIndex = 11;
            this.cmdHapusTimTAPD.Text = "Bersihkan Tim TAPD Dinas";
            this.cmdHapusTimTAPD.UseVisualStyleBackColor = false;
            this.cmdHapusTimTAPD.Click += new System.EventHandler(this.cmdHapusTimTAPD_Click);
            // 
            // ctrlJenisAnggaran1
            // 
            this.ctrlJenisAnggaran1.Location = new System.Drawing.Point(193, 98);
            this.ctrlJenisAnggaran1.Name = "ctrlJenisAnggaran1";
            this.ctrlJenisAnggaran1.Size = new System.Drawing.Size(444, 24);
            this.ctrlJenisAnggaran1.TabIndex = 12;
            this.ctrlJenisAnggaran1.OnChanged += new KUAPPAS.ctrlJenisAnggaran.ValueChangedEventHandler(this.ctrlJenisAnggaran1_OnChanged);
            this.ctrlJenisAnggaran1.Load += new System.EventHandler(this.ctrlJenisAnggaran1_Load);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 14);
            this.label2.TabIndex = 13;
            this.label2.Text = "Jenis Anggaran";
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Location = new System.Drawing.Point(2, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(841, 47);
            this.ctrlHeader1.TabIndex = 14;
            // 
            // gridTimViewer
            // 
            this.gridTimViewer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridTimViewer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTimViewer.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridTimViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTimViewer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewButtonColumn1,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridTimViewer.DefaultCellStyle = dataGridViewCellStyle5;
            this.gridTimViewer.Location = new System.Drawing.Point(2, 405);
            this.gridTimViewer.Name = "gridTimViewer";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTimViewer.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gridTimViewer.Size = new System.Drawing.Size(841, 73);
            this.gridTimViewer.TabIndex = 15;
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.HeaderText = "HapusT";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewButtonColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewButtonColumn1.Width = 60;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Nama";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "NIP";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "Jabatan";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // cmdSimpanTimViewer
            // 
            this.cmdSimpanTimViewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.cmdSimpanTimViewer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSimpanTimViewer.Location = new System.Drawing.Point(3, 481);
            this.cmdSimpanTimViewer.Name = "cmdSimpanTimViewer";
            this.cmdSimpanTimViewer.Size = new System.Drawing.Size(241, 34);
            this.cmdSimpanTimViewer.TabIndex = 16;
            this.cmdSimpanTimViewer.Text = "Simpan Tim Viewer Inspektorat";
            this.cmdSimpanTimViewer.UseVisualStyleBackColor = false;
            this.cmdSimpanTimViewer.Click += new System.EventHandler(this.cmdSimpanTimViewer_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "Tim TAPD";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(2, 380);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(171, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Tim Viewer Inspektorat";
            // 
            // No
            // 
            this.No.HeaderText = "HapusT";
            this.No.Name = "No";
            this.No.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.No.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.No.Width = 60;
            // 
            // Nama
            // 
            this.Nama.HeaderText = "Nama";
            this.Nama.Name = "Nama";
            this.Nama.Width = 200;
            // 
            // NIP
            // 
            this.NIP.HeaderText = "NIP";
            this.NIP.Name = "NIP";
            this.NIP.Width = 200;
            // 
            // Jabatan
            // 
            this.Jabatan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Jabatan.HeaderText = "Jabatan";
            this.Jabatan.Name = "Jabatan";
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // frmIputTimTAPD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 538);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdSimpanTimViewer);
            this.Controls.Add(this.gridTimViewer);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ctrlJenisAnggaran1);
            this.Controls.Add(this.cmdHapusTimTAPD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.chkSemuaDinas);
            this.Controls.Add(this.cmdSimpanTIMTAPD);
            this.Controls.Add(this.gridTimDPA);
            this.Name = "frmIputTimTAPD";
            this.Text = "frmInoutTimDPA";
            this.Load += new System.EventHandler(this.frmInoutTimDPA_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridTimDPA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTimViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdSimpanTIMTAPD;
        private System.Windows.Forms.DataGridView gridTimDPA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkSemuaDinas;
        private ctrlDinas ctrlDinas1;
        private System.Windows.Forms.Button cmdHapusTimTAPD;
        private ctrlJenisAnggaran ctrlJenisAnggaran1;
        private System.Windows.Forms.Label label2;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.DataGridView gridTimViewer;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Button cmdSimpanTimViewer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewButtonColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn NIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jabatan;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
    }
}