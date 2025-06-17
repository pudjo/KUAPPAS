namespace KUAPPAS.SP2DOnline
{
    partial class frmSP2DOnlne510Respond
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
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.txtNoSP2D = new System.Windows.Forms.TextBox();
            this.txtNoSPM = new System.Windows.Forms.TextBox();
            this.txtRefNo = new System.Windows.Forms.TextBox();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.txtTanggalTrx = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.gridPotongan = new System.Windows.Forms.DataGridView();
            this.idbilling = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Refer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nominal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NTPN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label8 = new System.Windows.Forms.Label();
            this.cmdTutup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridPotongan)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(695, 41);
            this.ctrlHeader1.TabIndex = 0;
            // 
            // txtNoSP2D
            // 
            this.txtNoSP2D.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoSP2D.Location = new System.Drawing.Point(170, 53);
            this.txtNoSP2D.Name = "txtNoSP2D";
            this.txtNoSP2D.Size = new System.Drawing.Size(445, 20);
            this.txtNoSP2D.TabIndex = 2;
            // 
            // txtNoSPM
            // 
            this.txtNoSPM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoSPM.Location = new System.Drawing.Point(170, 80);
            this.txtNoSPM.Name = "txtNoSPM";
            this.txtNoSPM.Size = new System.Drawing.Size(445, 20);
            this.txtNoSPM.TabIndex = 3;
            // 
            // txtRefNo
            // 
            this.txtRefNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRefNo.Location = new System.Drawing.Point(170, 107);
            this.txtRefNo.Name = "txtRefNo";
            this.txtRefNo.Size = new System.Drawing.Size(445, 20);
            this.txtRefNo.TabIndex = 4;
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReferenceNo.Location = new System.Drawing.Point(170, 156);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(445, 20);
            this.txtReferenceNo.TabIndex = 6;
            // 
            // txtTanggalTrx
            // 
            this.txtTanggalTrx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTanggalTrx.Location = new System.Drawing.Point(170, 130);
            this.txtTanggalTrx.Name = "txtTanggalTrx";
            this.txtTanggalTrx.Size = new System.Drawing.Size(445, 20);
            this.txtTanggalTrx.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Nomor SP2D";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Nomor SPM";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Reference No";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 164);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Nominal Transaksi";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 132);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Tanggal Transaksi";
            // 
            // gridPotongan
            // 
            this.gridPotongan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPotongan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idbilling,
            this.Refer,
            this.Nominal,
            this.Status,
            this.NTPN});
            this.gridPotongan.Location = new System.Drawing.Point(40, 291);
            this.gridPotongan.Name = "gridPotongan";
            this.gridPotongan.Size = new System.Drawing.Size(573, 141);
            this.gridPotongan.TabIndex = 15;
            // 
            // idbilling
            // 
            this.idbilling.HeaderText = "Id Billing";
            this.idbilling.Name = "idbilling";
            this.idbilling.ReadOnly = true;
            // 
            // Refer
            // 
            this.Refer.HeaderText = "Reference No";
            this.Refer.Name = "Refer";
            this.Refer.ReadOnly = true;
            // 
            // Nominal
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Nominal.DefaultCellStyle = dataGridViewCellStyle1;
            this.Nominal.HeaderText = "Nominal";
            this.Nominal.Name = "Nominal";
            this.Nominal.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status Pembayaran MPN";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // NTPN
            // 
            this.NTPN.HeaderText = "NTPN";
            this.NTPN.Name = "NTPN";
            this.NTPN.ReadOnly = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 448);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(695, 22);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(37, 250);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Potongan MPN";
            // 
            // cmdTutup
            // 
            this.cmdTutup.Location = new System.Drawing.Point(540, 424);
            this.cmdTutup.Name = "cmdTutup";
            this.cmdTutup.Size = new System.Drawing.Size(75, 23);
            this.cmdTutup.TabIndex = 18;
            this.cmdTutup.Text = "Tutup";
            this.cmdTutup.UseVisualStyleBackColor = true;
            this.cmdTutup.Click += new System.EventHandler(this.cmdTutup_Click);
            // 
            // frmSP2DOnlne510Respond
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 470);
            this.Controls.Add(this.cmdTutup);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gridPotongan);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTanggalTrx);
            this.Controls.Add(this.txtReferenceNo);
            this.Controls.Add(this.txtRefNo);
            this.Controls.Add(this.txtNoSPM);
            this.Controls.Add(this.txtNoSP2D);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmSP2DOnlne510Respond";
            this.Text = "frmSP2DOnlne510Respond";
            this.Load += new System.EventHandler(this.frmSP2DOnlne510Respond_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridPotongan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.TextBox txtNoSP2D;
        private System.Windows.Forms.TextBox txtNoSPM;
        private System.Windows.Forms.TextBox txtRefNo;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.TextBox txtTanggalTrx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView gridPotongan;
        private System.Windows.Forms.DataGridViewTextBoxColumn idbilling;
        private System.Windows.Forms.DataGridViewTextBoxColumn Refer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nominal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn NTPN;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button cmdTutup;
    }
}