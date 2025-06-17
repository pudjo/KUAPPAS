namespace KUAPPAS.Bendahara
{
    partial class frmBendahara2
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
            this.ctrlSKPD1 = new KUAPPAS.ctrlSKPD();
            this.SuspendLayout();
            // 
            // ctrlSKPD1
            // 
            this.ctrlSKPD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSKPD1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlSKPD1.Location = new System.Drawing.Point(0, 0);
            this.ctrlSKPD1.Name = "ctrlSKPD1";
            this.ctrlSKPD1.Size = new System.Drawing.Size(765, 28);
            this.ctrlSKPD1.TabIndex = 1;
            // 
            // frmBendahara2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 362);
            this.Controls.Add(this.ctrlSKPD1);
            this.KeyPreview = true;
            this.Name = "frmBendahara2";
            this.Text = "frmBendahara2";
            this.Load += new System.EventHandler(this.frmBendahara2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlSKPD ctrlSKPD1;
    }
}