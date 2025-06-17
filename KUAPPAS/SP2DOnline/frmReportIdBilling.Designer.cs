namespace KUAPPAS.SP2DOnline
{
    partial class frmReportIdBilling
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
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.dtAwal = new System.Windows.Forms.DateTimePicker();
            this.dtAkhir = new System.Windows.Forms.DateTimePicker();
            this.cmbJenisReport = new System.Windows.Forms.ComboBox();
            this.cmbFormat = new System.Windows.Forms.ComboBox();
            this.cmdGenerateReport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtExpiredDate = new System.Windows.Forms.TextBox();
            this.txtLinkLaporan = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNoReference = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmdDownload = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(693, 41);
            this.ctrlHeader1.TabIndex = 0;
            // 
            // dtAwal
            // 
            this.dtAwal.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtAwal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtAwal.Location = new System.Drawing.Point(166, 107);
            this.dtAwal.Name = "dtAwal";
            this.dtAwal.Size = new System.Drawing.Size(200, 20);
            this.dtAwal.TabIndex = 1;
            // 
            // dtAkhir
            // 
            this.dtAkhir.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtAkhir.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtAkhir.Location = new System.Drawing.Point(166, 134);
            this.dtAkhir.Name = "dtAkhir";
            this.dtAkhir.Size = new System.Drawing.Size(200, 20);
            this.dtAkhir.TabIndex = 2;
            // 
            // cmbJenisReport
            // 
            this.cmbJenisReport.FormattingEnabled = true;
            this.cmbJenisReport.Location = new System.Drawing.Point(166, 81);
            this.cmbJenisReport.Name = "cmbJenisReport";
            this.cmbJenisReport.Size = new System.Drawing.Size(200, 21);
            this.cmbJenisReport.TabIndex = 3;
            // 
            // cmbFormat
            // 
            this.cmbFormat.FormattingEnabled = true;
            this.cmbFormat.Location = new System.Drawing.Point(166, 160);
            this.cmbFormat.Name = "cmbFormat";
            this.cmbFormat.Size = new System.Drawing.Size(200, 21);
            this.cmbFormat.TabIndex = 4;
            // 
            // cmdGenerateReport
            // 
            this.cmdGenerateReport.Location = new System.Drawing.Point(166, 187);
            this.cmdGenerateReport.Name = "cmdGenerateReport";
            this.cmdGenerateReport.Size = new System.Drawing.Size(200, 30);
            this.cmdGenerateReport.TabIndex = 5;
            this.cmdGenerateReport.Text = "Generate Report";
            this.cmdGenerateReport.UseVisualStyleBackColor = true;
            this.cmdGenerateReport.Click += new System.EventHandler(this.cmdGenerateReport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Jenis Laporan";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Format Laporan";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tanggal Awal";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(61, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Tanggal Akhir";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 460);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(693, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtExpiredDate
            // 
            this.txtExpiredDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExpiredDate.Location = new System.Drawing.Point(166, 270);
            this.txtExpiredDate.Name = "txtExpiredDate";
            this.txtExpiredDate.Size = new System.Drawing.Size(446, 20);
            this.txtExpiredDate.TabIndex = 11;
            // 
            // txtLinkLaporan
            // 
            this.txtLinkLaporan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLinkLaporan.Location = new System.Drawing.Point(166, 223);
            this.txtLinkLaporan.Multiline = true;
            this.txtLinkLaporan.Name = "txtLinkLaporan";
            this.txtLinkLaporan.Size = new System.Drawing.Size(446, 41);
            this.txtLinkLaporan.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(64, 229);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Link Laporan";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(64, 277);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Tanggal Expired";
            // 
            // txtNoReference
            // 
            this.txtNoReference.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoReference.Location = new System.Drawing.Point(168, 58);
            this.txtNoReference.Name = "txtNoReference";
            this.txtNoReference.Size = new System.Drawing.Size(198, 20);
            this.txtNoReference.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(64, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "No Reference";
            // 
            // cmdDownload
            // 
            this.cmdDownload.Location = new System.Drawing.Point(166, 297);
            this.cmdDownload.Name = "cmdDownload";
            this.cmdDownload.Size = new System.Drawing.Size(193, 33);
            this.cmdDownload.TabIndex = 17;
            this.cmdDownload.Text = "Down Load dan Lihat ";
            this.cmdDownload.UseVisualStyleBackColor = true;
            this.cmdDownload.Click += new System.EventHandler(this.cmdDownload_Click);
            // 
            // frmReportIdBilling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 482);
            this.Controls.Add(this.cmdDownload);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtNoReference);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtLinkLaporan);
            this.Controls.Add(this.txtExpiredDate);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdGenerateReport);
            this.Controls.Add(this.cmbFormat);
            this.Controls.Add(this.cmbJenisReport);
            this.Controls.Add(this.dtAkhir);
            this.Controls.Add(this.dtAwal);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmReportIdBilling";
            this.Text = "Generate Laporan Id Billing";
            this.Load += new System.EventHandler(this.frmReportIdBilling_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.DateTimePicker dtAwal;
        private System.Windows.Forms.DateTimePicker dtAkhir;
        private System.Windows.Forms.ComboBox cmbJenisReport;
        private System.Windows.Forms.ComboBox cmbFormat;
        private System.Windows.Forms.Button cmdGenerateReport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox txtExpiredDate;
        private System.Windows.Forms.TextBox txtLinkLaporan;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNoReference;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button cmdDownload;
    }
}