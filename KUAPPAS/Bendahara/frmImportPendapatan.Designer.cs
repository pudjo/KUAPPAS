namespace KUAPPAS.Bendahara
{
    partial class frmImportPendapatan
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
            this.cmdTampilkam = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.worksheetsComboBox = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.ctrlSKPD1 = new KUAPPAS.ctrlSKPD();
            this.label3 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.gridExcell = new System.Windows.Forms.DataGridView();
            this.Mode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.cmdImport = new System.Windows.Forms.Button();
            this.cmdCek = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.gridExcell)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdTampilkam
            // 
            this.cmdTampilkam.Location = new System.Drawing.Point(166, 131);
            this.cmdTampilkam.Name = "cmdTampilkam";
            this.cmdTampilkam.Size = new System.Drawing.Size(124, 38);
            this.cmdTampilkam.TabIndex = 0;
            this.cmdTampilkam.Text = "Tampilkan";
            this.cmdTampilkam.UseVisualStyleBackColor = true;
            this.cmdTampilkam.Click += new System.EventHandler(this.cmdTampilkam_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(72, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 14);
            this.label2.TabIndex = 13;
            this.label2.Text = "Work sheets";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(72, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 14);
            this.label1.TabIndex = 12;
            this.label1.Text = "File";
            // 
            // worksheetsComboBox
            // 
            this.worksheetsComboBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.worksheetsComboBox.FormattingEnabled = true;
            this.worksheetsComboBox.Location = new System.Drawing.Point(166, 103);
            this.worksheetsComboBox.Name = "worksheetsComboBox";
            this.worksheetsComboBox.Size = new System.Drawing.Size(432, 22);
            this.worksheetsComboBox.TabIndex = 11;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(604, 76);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 22);
            this.button2.TabIndex = 10;
            this.button2.Text = "Cari FIle";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFileName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileName.Location = new System.Drawing.Point(165, 76);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(433, 22);
            this.txtFileName.TabIndex = 9;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(862, 41);
            this.ctrlHeader1.TabIndex = 14;
            // 
            // ctrlSKPD1
            // 
            this.ctrlSKPD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSKPD1.Location = new System.Drawing.Point(166, 48);
            this.ctrlSKPD1.Name = "ctrlSKPD1";
            this.ctrlSKPD1.Size = new System.Drawing.Size(432, 26);
            this.ctrlSKPD1.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(72, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 14);
            this.label3.TabIndex = 16;
            this.label3.Text = "OPD";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // gridExcell
            // 
            this.gridExcell.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridExcell.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Mode,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.gridExcell.Location = new System.Drawing.Point(0, 175);
            this.gridExcell.Name = "gridExcell";
            this.gridExcell.Size = new System.Drawing.Size(862, 336);
            this.gridExcell.TabIndex = 17;
            // 
            // Mode
            // 
            this.Mode.HeaderText = "Mode";
            this.Mode.Name = "Mode";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Keterangan Mode";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Keterangan ";
            this.Column3.Name = "Column3";
            this.Column3.Width = 200;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "No Bukti";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Tanggal";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Kode Rekening";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Jumlah";
            this.Column7.Name = "Column7";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 531);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(862, 22);
            this.statusStrip1.TabIndex = 18;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // cmdImport
            // 
            this.cmdImport.Location = new System.Drawing.Point(498, 131);
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.Size = new System.Drawing.Size(126, 37);
            this.cmdImport.TabIndex = 19;
            this.cmdImport.Text = "Import";
            this.cmdImport.UseVisualStyleBackColor = true;
            this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
            // 
            // cmdCek
            // 
            this.cmdCek.Location = new System.Drawing.Point(297, 131);
            this.cmdCek.Name = "cmdCek";
            this.cmdCek.Size = new System.Drawing.Size(195, 37);
            this.cmdCek.TabIndex = 20;
            this.cmdCek.Text = "Cek Apakah Sudah diimport";
            this.cmdCek.UseVisualStyleBackColor = true;
            this.cmdCek.Click += new System.EventHandler(this.cmdCek_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 518);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(859, 10);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 21;
            this.progressBar1.Visible = false;
            // 
            // frmImportPendapatan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 553);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.cmdCek);
            this.Controls.Add(this.cmdImport);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gridExcell);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ctrlSKPD1);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.worksheetsComboBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.cmdTampilkam);
            this.Name = "frmImportPendapatan";
            this.Text = "Import Data Pendapatan";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmImportPendapatan_FormClosed);
            this.Load += new System.EventHandler(this.frmImportPendapatan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridExcell)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdTampilkam;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox worksheetsComboBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtFileName;
        private ctrlHeader ctrlHeader1;
        private ctrlSKPD ctrlSKPD1;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView gridExcell;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button cmdImport;
        private System.Windows.Forms.Button cmdCek;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}