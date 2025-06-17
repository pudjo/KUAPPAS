namespace KUAPPAS
{
    partial class ctrlJenisSPP
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
            this.cmbJenisSPP = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbJenisSPP
            // 
            this.cmbJenisSPP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbJenisSPP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbJenisSPP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbJenisSPP.FormattingEnabled = true;
            this.cmbJenisSPP.Location = new System.Drawing.Point(0, 0);
            this.cmbJenisSPP.Name = "cmbJenisSPP";
            this.cmbJenisSPP.Size = new System.Drawing.Size(177, 23);
            this.cmbJenisSPP.TabIndex = 0;
            this.cmbJenisSPP.SelectedIndexChanged += new System.EventHandler(this.cmbJenisSPP_SelectedIndexChanged);
            // 
            // ctrlJenisSPP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbJenisSPP);
            this.Name = "ctrlJenisSPP";
            this.Size = new System.Drawing.Size(177, 30);
            this.Load += new System.EventHandler(this.ctrlJenisSPP_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbJenisSPP;

    }
}
