namespace KUAPPAS.SP2DOnline
{
    partial class frmRiwayat
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.gridRiwayat = new System.Windows.Forms.DataGridView();
            this.Waktu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pesan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdTutup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridRiwayat)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(684, 41);
            this.ctrlHeader1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 329);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(684, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // gridRiwayat
            // 
            this.gridRiwayat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRiwayat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Waktu,
            this.Kode,
            this.Pesan});
            this.gridRiwayat.Location = new System.Drawing.Point(0, 47);
            this.gridRiwayat.Name = "gridRiwayat";
            this.gridRiwayat.Size = new System.Drawing.Size(684, 237);
            this.gridRiwayat.TabIndex = 2;
            // 
            // Waktu
            // 
            this.Waktu.HeaderText = "Waktu";
            this.Waktu.Name = "Waktu";
            this.Waktu.ReadOnly = true;
            // 
            // Kode
            // 
            this.Kode.HeaderText = "Kode";
            this.Kode.Name = "Kode";
            this.Kode.ReadOnly = true;
            this.Kode.Width = 30;
            // 
            // Pesan
            // 
            this.Pesan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Pesan.HeaderText = "Pesan";
            this.Pesan.Name = "Pesan";
            this.Pesan.ReadOnly = true;
            // 
            // cmdTutup
            // 
            this.cmdTutup.Location = new System.Drawing.Point(13, 291);
            this.cmdTutup.Name = "cmdTutup";
            this.cmdTutup.Size = new System.Drawing.Size(75, 35);
            this.cmdTutup.TabIndex = 3;
            this.cmdTutup.Text = "Tutup";
            this.cmdTutup.UseVisualStyleBackColor = true;
            // 
            // frmRiwayat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 351);
            this.Controls.Add(this.cmdTutup);
            this.Controls.Add(this.gridRiwayat);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmRiwayat";
            this.Text = "Riwayat Transaksi SP2D Online";
            this.Load += new System.EventHandler(this.frmRiwayat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridRiwayat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridView gridRiwayat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Waktu;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pesan;
        private System.Windows.Forms.Button cmdTutup;
    }
}