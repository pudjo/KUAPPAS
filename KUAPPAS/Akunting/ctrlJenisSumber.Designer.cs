namespace KUAPPAS.Akunting
{
    partial class ctrlJenisSumber
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
            this.cmbJenisSumber = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbJenisSumber
            // 
            this.cmbJenisSumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbJenisSumber.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbJenisSumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbJenisSumber.FormattingEnabled = true;
            this.cmbJenisSumber.Location = new System.Drawing.Point(0, 0);
            this.cmbJenisSumber.Name = "cmbJenisSumber";
            this.cmbJenisSumber.Size = new System.Drawing.Size(214, 23);
            this.cmbJenisSumber.TabIndex = 0;
            // 
            // ctrlJenisSumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbJenisSumber);
            this.Name = "ctrlJenisSumber";
            this.Size = new System.Drawing.Size(214, 41);
            this.Load += new System.EventHandler(this.ctrlJenisSumber_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbJenisSumber;
    }
}
