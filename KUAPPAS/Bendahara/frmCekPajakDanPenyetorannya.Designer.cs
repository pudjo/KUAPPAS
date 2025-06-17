namespace KUAPPAS.Bendahara
{
    partial class frmCekPajakDanPenyetorannya
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
            this.cmdCariLagi = new System.Windows.Forms.Button();
            this.cmdCari = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.lblPencarian = new System.Windows.Forms.Label();
            this.ctrlSKPD1 = new KUAPPAS.ctrlSKPD();
            this.ctrlTanggalBulanVertikal1 = new KUAPPAS.Bendahara.ctrlTanggalBulanVertikal();
            this.label9 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdCariLagi
            // 
            this.cmdCariLagi.Location = new System.Drawing.Point(779, 78);
            this.cmdCariLagi.Name = "cmdCariLagi";
            this.cmdCariLagi.Size = new System.Drawing.Size(27, 23);
            this.cmdCariLagi.TabIndex = 73;
            this.cmdCariLagi.Text = "Cari Lagi";
            this.cmdCariLagi.UseVisualStyleBackColor = true;
            // 
            // cmdCari
            // 
            this.cmdCari.Location = new System.Drawing.Point(734, 81);
            this.cmdCari.Name = "cmdCari";
            this.cmdCari.Size = new System.Drawing.Size(39, 23);
            this.cmdCari.TabIndex = 72;
            this.cmdCari.Text = "Cari...";
            this.cmdCari.UseVisualStyleBackColor = true;
            // 
            // txtCari
            // 
            this.txtCari.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCari.Location = new System.Drawing.Point(538, 81);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(183, 20);
            this.txtCari.TabIndex = 71;
            // 
            // lblPencarian
            // 
            this.lblPencarian.AutoSize = true;
            this.lblPencarian.Location = new System.Drawing.Point(477, 83);
            this.lblPencarian.Name = "lblPencarian";
            this.lblPencarian.Size = new System.Drawing.Size(55, 13);
            this.lblPencarian.TabIndex = 70;
            this.lblPencarian.Text = "Pencarian";
            // 
            // ctrlSKPD1
            // 
            this.ctrlSKPD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSKPD1.Location = new System.Drawing.Point(66, 11);
            this.ctrlSKPD1.Name = "ctrlSKPD1";
            this.ctrlSKPD1.Size = new System.Drawing.Size(510, 24);
            this.ctrlSKPD1.TabIndex = 69;
            // 
            // ctrlTanggalBulanVertikal1
            // 
            this.ctrlTanggalBulanVertikal1.Bulan = 0;
            this.ctrlTanggalBulanVertikal1.JenisPeriode = 1;
            this.ctrlTanggalBulanVertikal1.Location = new System.Drawing.Point(66, 41);
            this.ctrlTanggalBulanVertikal1.Name = "ctrlTanggalBulanVertikal1";
            this.ctrlTanggalBulanVertikal1.Size = new System.Drawing.Size(390, 70);
            this.ctrlTanggalBulanVertikal1.TabIndex = 68;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(14, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 15);
            this.label9.TabIndex = 67;
            this.label9.Text = "OPD";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(-1, 151);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(979, 364);
            this.dataGridView1.TabIndex = 74;
            // 
            // frmCekPajakDanPenyetorannya
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 552);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmdCariLagi);
            this.Controls.Add(this.cmdCari);
            this.Controls.Add(this.txtCari);
            this.Controls.Add(this.lblPencarian);
            this.Controls.Add(this.ctrlSKPD1);
            this.Controls.Add(this.ctrlTanggalBulanVertikal1);
            this.Controls.Add(this.label9);
            this.Name = "frmCekPajakDanPenyetorannya";
            this.Text = "frmCekPajakDanPenyetorannya";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCariLagi;
        private System.Windows.Forms.Button cmdCari;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Label lblPencarian;
        private ctrlSKPD ctrlSKPD1;
        private ctrlTanggalBulanVertikal ctrlTanggalBulanVertikal1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dataGridView1;

    }
}