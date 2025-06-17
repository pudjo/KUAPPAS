namespace KUAPPAS
{
    partial class ctrlPejabat
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
            this.cmbPejabat = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbPejabat
            // 
            this.cmbPejabat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbPejabat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPejabat.FormattingEnabled = true;
            this.cmbPejabat.Location = new System.Drawing.Point(0, 0);
            this.cmbPejabat.Name = "cmbPejabat";
            this.cmbPejabat.Size = new System.Drawing.Size(496, 21);
            this.cmbPejabat.TabIndex = 0;
            this.cmbPejabat.SelectedIndexChanged += new System.EventHandler(this.cmbPejabat_SelectedIndexChanged);
            // 
            // ctrlPejabat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbPejabat);
            this.Name = "ctrlPejabat";
            this.Size = new System.Drawing.Size(496, 24);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPejabat;

    }
}
