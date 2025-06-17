namespace KUAPPAS.Bendahara
{
    partial class ctrlSKPDSKPD
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
            this.cmbSKRSKPD = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbSKRSKPD
            // 
            this.cmbSKRSKPD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSKRSKPD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSKRSKPD.FormattingEnabled = true;
            this.cmbSKRSKPD.Location = new System.Drawing.Point(0, 0);
            this.cmbSKRSKPD.Name = "cmbSKRSKPD";
            this.cmbSKRSKPD.Size = new System.Drawing.Size(478, 21);
            this.cmbSKRSKPD.TabIndex = 0;
            this.cmbSKRSKPD.SelectedIndexChanged += new System.EventHandler(this.cmbSKRSKPD_SelectedIndexChanged);
            // 
            // ctrlSKPDSKPD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbSKRSKPD);
            this.Name = "ctrlSKPDSKPD";
            this.Size = new System.Drawing.Size(478, 64);
            this.Load += new System.EventHandler(this.ctrlSKPDSKPD_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSKRSKPD;
    }
}
