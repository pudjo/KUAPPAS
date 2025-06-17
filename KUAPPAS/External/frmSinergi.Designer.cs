namespace KUAPPAS.External
{
    partial class frmSinergi
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
            this.cmdDTH = new System.Windows.Forms.Button();
            this.lblBulan = new System.Windows.Forms.Label();
            this.ctrlBulan1 = new KUAPPAS.ctrlBulan();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.cmdEditBTBT = new System.Windows.Forms.Button();
            this.cmdCetakDTH = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdDTH
            // 
            this.cmdDTH.Location = new System.Drawing.Point(202, 92);
            this.cmdDTH.Name = "cmdDTH";
            this.cmdDTH.Size = new System.Drawing.Size(182, 36);
            this.cmdDTH.TabIndex = 0;
            this.cmdDTH.Text = "ExportDTH";
            this.cmdDTH.UseVisualStyleBackColor = true;
            this.cmdDTH.Click += new System.EventHandler(this.cmdDTH_Click);
            // 
            // lblBulan
            // 
            this.lblBulan.AutoSize = true;
            this.lblBulan.Location = new System.Drawing.Point(134, 64);
            this.lblBulan.Name = "lblBulan";
            this.lblBulan.Size = new System.Drawing.Size(34, 13);
            this.lblBulan.TabIndex = 2;
            this.lblBulan.Text = "Bulan";
            // 
            // ctrlBulan1
            // 
            this.ctrlBulan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlBulan1.Location = new System.Drawing.Point(202, 64);
            this.ctrlBulan1.Name = "ctrlBulan1";
            this.ctrlBulan1.Size = new System.Drawing.Size(204, 22);
            this.ctrlBulan1.TabIndex = 1;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(581, 41);
            this.ctrlHeader1.TabIndex = 3;
            // 
            // cmdEditBTBT
            // 
            this.cmdEditBTBT.Location = new System.Drawing.Point(182, 240);
            this.cmdEditBTBT.Name = "cmdEditBTBT";
            this.cmdEditBTBT.Size = new System.Drawing.Size(201, 23);
            this.cmdEditBTBT.TabIndex = 4;
            this.cmdEditBTBT.Text = "Betulkan kode Kode";
            this.cmdEditBTBT.UseVisualStyleBackColor = true;
            this.cmdEditBTBT.Click += new System.EventHandler(this.cmdEditBTBT_Click);
            // 
            // cmdCetakDTH
            // 
            this.cmdCetakDTH.Location = new System.Drawing.Point(209, 136);
            this.cmdCetakDTH.Name = "cmdCetakDTH";
            this.cmdCetakDTH.Size = new System.Drawing.Size(174, 42);
            this.cmdCetakDTH.TabIndex = 5;
            this.cmdCetakDTH.Text = "Cetak DTH";
            this.cmdCetakDTH.UseVisualStyleBackColor = true;
            this.cmdCetakDTH.Click += new System.EventHandler(this.cmdCetakDTH_Click);
            // 
            // frmSinergi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 446);
            this.Controls.Add(this.cmdCetakDTH);
            this.Controls.Add(this.cmdEditBTBT);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.lblBulan);
            this.Controls.Add(this.ctrlBulan1);
            this.Controls.Add(this.cmdDTH);
            this.Name = "frmSinergi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSinergi";
            this.Load += new System.EventHandler(this.frmSinergi_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdDTH;
        private ctrlBulan ctrlBulan1;
        private System.Windows.Forms.Label lblBulan;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.Button cmdEditBTBT;
        private System.Windows.Forms.Button cmdCetakDTH;
    }
}