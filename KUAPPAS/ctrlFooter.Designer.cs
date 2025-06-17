namespace KUAPPAS
{
    partial class ctrlFooter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblidcrt = new System.Windows.Forms.Label();
            this.lbldcrt = new System.Windows.Forms.Label();
            this.lbldupdate = new System.Windows.Forms.Label();
            this.lblidupdate = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dibuat Oleh";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(228, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tanggal";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(439, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Terakhir diUbah oleh ";
            // 
            // lblidcrt
            // 
            this.lblidcrt.AutoSize = true;
            this.lblidcrt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblidcrt.Location = new System.Drawing.Point(139, 2);
            this.lblidcrt.Name = "lblidcrt";
            this.lblidcrt.Size = new System.Drawing.Size(11, 13);
            this.lblidcrt.TabIndex = 3;
            this.lblidcrt.Text = "-";
            // 
            // lbldcrt
            // 
            this.lbldcrt.AutoSize = true;
            this.lbldcrt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldcrt.Location = new System.Drawing.Point(278, 2);
            this.lbldcrt.Name = "lbldcrt";
            this.lbldcrt.Size = new System.Drawing.Size(11, 13);
            this.lbldcrt.TabIndex = 4;
            this.lbldcrt.Text = "-";
            // 
            // lbldupdate
            // 
            this.lbldupdate.AutoSize = true;
            this.lbldupdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldupdate.Location = new System.Drawing.Point(663, 2);
            this.lbldupdate.Name = "lbldupdate";
            this.lbldupdate.Size = new System.Drawing.Size(11, 13);
            this.lbldupdate.TabIndex = 5;
            this.lbldupdate.Text = "-";
            // 
            // lblidupdate
            // 
            this.lblidupdate.AutoSize = true;
            this.lblidupdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblidupdate.Location = new System.Drawing.Point(555, 2);
            this.lblidupdate.Name = "lblidupdate";
            this.lblidupdate.Size = new System.Drawing.Size(11, 13);
            this.lblidupdate.TabIndex = 6;
            this.lblidupdate.Text = "-";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Location = new System.Drawing.Point(-12, -15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(79, 47);
            this.panel1.TabIndex = 7;
            // 
            // ctrlFooter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblidupdate);
            this.Controls.Add(this.lbldupdate);
            this.Controls.Add(this.lbldcrt);
            this.Controls.Add(this.lblidcrt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ctrlFooter";
            this.Size = new System.Drawing.Size(814, 21);
            this.Load += new System.EventHandler(this.ctrlFooter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblidcrt;
        private System.Windows.Forms.Label lbldcrt;
        private System.Windows.Forms.Label lbldupdate;
        private System.Windows.Forms.Label lblidupdate;
        private System.Windows.Forms.Panel panel1;
    }
}
