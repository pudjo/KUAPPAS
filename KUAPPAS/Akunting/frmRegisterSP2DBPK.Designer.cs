namespace KUAPPAS.Akunting
{
    partial class frmRegisterSP2DBPK
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
            this.cmdPanggilData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Location = new System.Drawing.Point(-1, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(710, 41);
            this.ctrlHeader1.TabIndex = 0;
            // 
            // cmdPanggilData
            // 
            this.cmdPanggilData.Location = new System.Drawing.Point(44, 47);
            this.cmdPanggilData.Name = "cmdPanggilData";
            this.cmdPanggilData.Size = new System.Drawing.Size(106, 32);
            this.cmdPanggilData.TabIndex = 1;
            this.cmdPanggilData.Text = "Panggil Data";
            this.cmdPanggilData.UseVisualStyleBackColor = true;
            this.cmdPanggilData.Click += new System.EventHandler(this.cmdPanggilData_Click);
            // 
            // frmRegisterSP2DBPK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 398);
            this.Controls.Add(this.cmdPanggilData);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmRegisterSP2DBPK";
            this.Text = "frmRegisterSP2DBPK";
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.Button cmdPanggilData;
    }
}