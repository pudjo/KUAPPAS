namespace KUAPPAS.SP2DOnline
{
    partial class frmMPN
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
            this.gridMPN = new System.Windows.Forms.DataGridView();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nilai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.cmdClose = new System.Windows.Forms.Button();
            this.lblPesan = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridMPN)).BeginInit();
            this.SuspendLayout();
            // 
            // gridMPN
            // 
            this.gridMPN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMPN.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nama,
            this.Nilai});
            this.gridMPN.Location = new System.Drawing.Point(12, 90);
            this.gridMPN.Name = "gridMPN";
            this.gridMPN.Size = new System.Drawing.Size(670, 361);
            this.gridMPN.TabIndex = 0;
            // 
            // Nama
            // 
            this.Nama.HeaderText = "Nama Item";
            this.Nama.Name = "Nama";
            this.Nama.ReadOnly = true;
            this.Nama.Width = 250;
            // 
            // Nilai
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Nilai.DefaultCellStyle = dataGridViewCellStyle1;
            this.Nilai.HeaderText = "Nilai";
            this.Nilai.Name = "Nilai";
            this.Nilai.ReadOnly = true;
            this.Nilai.Width = 400;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(682, 41);
            this.ctrlHeader1.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 500);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(682, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(534, 457);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(136, 41);
            this.cmdClose.TabIndex = 3;
            this.cmdClose.Text = "Tuttup";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // lblPesan
            // 
            this.lblPesan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPesan.Location = new System.Drawing.Point(12, 47);
            this.lblPesan.Multiline = true;
            this.lblPesan.Name = "lblPesan";
            this.lblPesan.Size = new System.Drawing.Size(670, 37);
            this.lblPesan.TabIndex = 4;
            this.lblPesan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frmMPN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 522);
            this.Controls.Add(this.lblPesan);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.gridMPN);
            this.Name = "frmMPN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "M P N";
            this.Load += new System.EventHandler(this.frmMPN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridMPN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridMPN;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nilai;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.TextBox lblPesan;
    }
}