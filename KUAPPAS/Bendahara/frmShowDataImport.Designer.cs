namespace KUAPPAS.Bendahara
{
    partial class frmShowDataImport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridSTS = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.NamaSheet = new System.Windows.Forms.Label();
            this.lblJumlah = new System.Windows.Forms.Label();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.cmdClose = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoUrutSTS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TanggalSTS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoSTS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KeteranganSTS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JumlahSTS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdBersihkan = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridSTS)).BeginInit();
            this.SuspendLayout();
            // 
            // gridSTS
            // 
            this.gridSTS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSTS.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridSTS.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gridSTS.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NoUrutSTS,
            this.TanggalSTS,
            this.NoSTS,
            this.KeteranganSTS,
            this.JumlahSTS});
            this.gridSTS.Location = new System.Drawing.Point(-5, 48);
            this.gridSTS.Name = "gridSTS";
            this.gridSTS.Size = new System.Drawing.Size(722, 336);
            this.gridSTS.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nama Sheet";
            // 
            // NamaSheet
            // 
            this.NamaSheet.AutoSize = true;
            this.NamaSheet.Location = new System.Drawing.Point(141, 13);
            this.NamaSheet.Name = "NamaSheet";
            this.NamaSheet.Size = new System.Drawing.Size(35, 13);
            this.NamaSheet.TabIndex = 4;
            this.NamaSheet.Text = "Sheet";
            // 
            // lblJumlah
            // 
            this.lblJumlah.AutoSize = true;
            this.lblJumlah.Location = new System.Drawing.Point(305, 395);
            this.lblJumlah.Name = "lblJumlah";
            this.lblJumlah.Size = new System.Drawing.Size(40, 13);
            this.lblJumlah.TabIndex = 5;
            this.lblJumlah.Text = "Jumlah";
            // 
            // txtJumlah
            // 
            this.txtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlah.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlah.Location = new System.Drawing.Point(361, 390);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(231, 22);
            this.txtJumlah.TabIndex = 6;
            this.txtJumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 442);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(718, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(582, 408);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(103, 31);
            this.cmdClose.TabIndex = 8;
            this.cmdClose.Text = "Tutup";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "NoUrut";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Tanggal";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "No STS";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn4.HeaderText = "Keterangan";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 200;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn5.HeaderText = "Jumlah";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // NoUrutSTS
            // 
            this.NoUrutSTS.HeaderText = "NoUrut";
            this.NoUrutSTS.Name = "NoUrutSTS";
            this.NoUrutSTS.ReadOnly = true;
            this.NoUrutSTS.Visible = false;
            // 
            // TanggalSTS
            // 
            this.TanggalSTS.HeaderText = "Tanggal";
            this.TanggalSTS.Name = "TanggalSTS";
            this.TanggalSTS.ReadOnly = true;
            // 
            // NoSTS
            // 
            this.NoSTS.HeaderText = "No STS";
            this.NoSTS.Name = "NoSTS";
            this.NoSTS.ReadOnly = true;
            // 
            // KeteranganSTS
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.KeteranganSTS.DefaultCellStyle = dataGridViewCellStyle1;
            this.KeteranganSTS.HeaderText = "Keterangan";
            this.KeteranganSTS.Name = "KeteranganSTS";
            this.KeteranganSTS.ReadOnly = true;
            this.KeteranganSTS.Width = 200;
            // 
            // JumlahSTS
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.JumlahSTS.DefaultCellStyle = dataGridViewCellStyle2;
            this.JumlahSTS.HeaderText = "Jumlah";
            this.JumlahSTS.Name = "JumlahSTS";
            this.JumlahSTS.ReadOnly = true;
            // 
            // cmdBersihkan
            // 
            this.cmdBersihkan.Location = new System.Drawing.Point(562, 0);
            this.cmdBersihkan.Name = "cmdBersihkan";
            this.cmdBersihkan.Size = new System.Drawing.Size(123, 42);
            this.cmdBersihkan.TabIndex = 9;
            this.cmdBersihkan.Text = "Bersihkan";
            this.cmdBersihkan.UseVisualStyleBackColor = true;
            this.cmdBersihkan.Click += new System.EventHandler(this.cmdBersihkan_Click);
            // 
            // frmShowDataImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 464);
            this.Controls.Add(this.cmdBersihkan);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.lblJumlah);
            this.Controls.Add(this.NamaSheet);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridSTS);
            this.Name = "frmShowDataImport";
            this.Text = "Data Hasil Import";
            this.Load += new System.EventHandler(this.frmShowDataImport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSTS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridSTS;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoUrutSTS;
        private System.Windows.Forms.DataGridViewTextBoxColumn TanggalSTS;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoSTS;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeteranganSTS;
        private System.Windows.Forms.DataGridViewTextBoxColumn JumlahSTS;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label NamaSheet;
        private System.Windows.Forms.Label lblJumlah;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdBersihkan;
    }
}