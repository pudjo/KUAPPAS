namespace KUAPPAS.SP2DOnline
{
    partial class frmReportSP2DOnline
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
            this.rbLaporanBPN = new System.Windows.Forms.RadioButton();
            this.rbLaporanCreateBilling = new System.Windows.Forms.RadioButton();
            this.tanggalAwal = new System.Windows.Forms.DateTimePicker();
            this.tanggalAkhir = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdProses = new System.Windows.Forms.Button();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.SuspendLayout();
            // 
            // rbLaporanBPN
            // 
            this.rbLaporanBPN.AutoSize = true;
            this.rbLaporanBPN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLaporanBPN.Location = new System.Drawing.Point(61, 105);
            this.rbLaporanBPN.Name = "rbLaporanBPN";
            this.rbLaporanBPN.Size = new System.Drawing.Size(100, 17);
            this.rbLaporanBPN.TabIndex = 1;
            this.rbLaporanBPN.TabStop = true;
            this.rbLaporanBPN.Text = "Laporan BPN";
            this.rbLaporanBPN.UseVisualStyleBackColor = true;
            // 
            // rbLaporanCreateBilling
            // 
            this.rbLaporanCreateBilling.AutoSize = true;
            this.rbLaporanCreateBilling.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLaporanCreateBilling.Location = new System.Drawing.Point(61, 128);
            this.rbLaporanCreateBilling.Name = "rbLaporanCreateBilling";
            this.rbLaporanCreateBilling.Size = new System.Drawing.Size(150, 17);
            this.rbLaporanCreateBilling.TabIndex = 2;
            this.rbLaporanCreateBilling.TabStop = true;
            this.rbLaporanCreateBilling.Text = "Laporan Create Billing";
            this.rbLaporanCreateBilling.UseVisualStyleBackColor = true;
            // 
            // tanggalAwal
            // 
            this.tanggalAwal.CustomFormat = "dd MMM yyyy";
            this.tanggalAwal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tanggalAwal.Location = new System.Drawing.Point(160, 170);
            this.tanggalAwal.Name = "tanggalAwal";
            this.tanggalAwal.Size = new System.Drawing.Size(200, 20);
            this.tanggalAwal.TabIndex = 3;
            // 
            // tanggalAkhir
            // 
            this.tanggalAkhir.CustomFormat = "dd MMM yyyy";
            this.tanggalAkhir.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tanggalAkhir.Location = new System.Drawing.Point(160, 197);
            this.tanggalAkhir.Name = "tanggalAkhir";
            this.tanggalAkhir.Size = new System.Drawing.Size(200, 20);
            this.tanggalAkhir.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Tanggal Awal";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 204);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tanggal Akhir";
            // 
            // cmdProses
            // 
            this.cmdProses.Location = new System.Drawing.Point(74, 264);
            this.cmdProses.Name = "cmdProses";
            this.cmdProses.Size = new System.Drawing.Size(75, 23);
            this.cmdProses.TabIndex = 7;
            this.cmdProses.Text = "Proses";
            this.cmdProses.UseVisualStyleBackColor = true;
            this.cmdProses.Click += new System.EventHandler(this.cmdProses_Click);
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(661, 51);
            this.ctrlHeader1.TabIndex = 0;
            // 
            // frmReportSP2DOnline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 407);
            this.Controls.Add(this.cmdProses);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tanggalAkhir);
            this.Controls.Add(this.tanggalAwal);
            this.Controls.Add(this.rbLaporanCreateBilling);
            this.Controls.Add(this.rbLaporanBPN);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmReportSP2DOnline";
            this.Text = "frmReportSP2DOnline";
            this.Load += new System.EventHandler(this.frmReportSP2DOnline_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.RadioButton rbLaporanBPN;
        private System.Windows.Forms.RadioButton rbLaporanCreateBilling;
        private System.Windows.Forms.DateTimePicker tanggalAwal;
        private System.Windows.Forms.DateTimePicker tanggalAkhir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdProses;
    }
}