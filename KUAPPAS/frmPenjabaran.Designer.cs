namespace KUAPPAS
{
    partial class frmPenjabaran
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
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.cmdCetak = new System.Windows.Forms.Button();
            this.chkKhususGaji = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(896, 41);
            this.ctrlHeader1.TabIndex = 0;
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(30, 71);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(595, 26);
            this.ctrlDinas1.TabIndex = 1;
            // 
            // cmdCetak
            // 
            this.cmdCetak.Location = new System.Drawing.Point(120, 200);
            this.cmdCetak.Name = "cmdCetak";
            this.cmdCetak.Size = new System.Drawing.Size(102, 26);
            this.cmdCetak.TabIndex = 3;
            this.cmdCetak.Text = "Cetak";
            this.cmdCetak.UseVisualStyleBackColor = true;
            this.cmdCetak.Click += new System.EventHandler(this.cmdCetak_Click);
            // 
            // chkKhususGaji
            // 
            this.chkKhususGaji.AutoSize = true;
            this.chkKhususGaji.Location = new System.Drawing.Point(129, 127);
            this.chkKhususGaji.Name = "chkKhususGaji";
            this.chkKhususGaji.Size = new System.Drawing.Size(78, 17);
            this.chkKhususGaji.TabIndex = 4;
            this.chkKhususGaji.Text = "Hanya Gaji";
            this.chkKhususGaji.UseVisualStyleBackColor = true;
            // 
            // frmPenjabaran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 461);
            this.Controls.Add(this.chkKhususGaji);
            this.Controls.Add(this.cmdCetak);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmPenjabaran";
            this.Text = "frmPenjabaran";
            this.Load += new System.EventHandler(this.frmPenjabaran_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlHeader ctrlHeader1;
        private ctrlDinas ctrlDinas1;
        private System.Windows.Forms.Button cmdCetak;
        private System.Windows.Forms.CheckBox chkKhususGaji;
    }
}