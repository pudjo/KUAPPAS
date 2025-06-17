namespace KUAPPAS.Akunting
{
    partial class frmBukuBesarDlg
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
            this.cmdTutup = new System.Windows.Forms.Button();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.ctrlBukuBesar1 = new KUAPPAS.Akunting.ctrlBukuBesar();
            this.SuspendLayout();
            // 
            // cmdTutup
            // 
            this.cmdTutup.Location = new System.Drawing.Point(984, 486);
            this.cmdTutup.Name = "cmdTutup";
            this.cmdTutup.Size = new System.Drawing.Size(75, 23);
            this.cmdTutup.TabIndex = 2;
            this.cmdTutup.Text = "Tutup";
            this.cmdTutup.UseVisualStyleBackColor = true;
            this.cmdTutup.Click += new System.EventHandler(this.cmdTutup_Click);
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1087, 41);
            this.ctrlHeader1.TabIndex = 1;
            // 
            // ctrlBukuBesar1
            // 
            this.ctrlBukuBesar1.Location = new System.Drawing.Point(12, 38);
            this.ctrlBukuBesar1.Name = "ctrlBukuBesar1";
            this.ctrlBukuBesar1.Size = new System.Drawing.Size(1072, 442);
            this.ctrlBukuBesar1.TabIndex = 0;
            this.ctrlBukuBesar1.Load += new System.EventHandler(this.ctrlBukuBesar1_Load);
            // 
            // frmBukuBesarDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 521);
            this.Controls.Add(this.cmdTutup);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.ctrlBukuBesar1);
            this.Name = "frmBukuBesarDlg";
            this.Text = "Buku Besar";
            this.Load += new System.EventHandler(this.frmBukuBesarDlg_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlBukuBesar ctrlBukuBesar1;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.Button cmdTutup;
    }
}