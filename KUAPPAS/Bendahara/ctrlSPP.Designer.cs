namespace KUAPPAS.Bendahara
{
    partial class ctrlSPP
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
            this.cmbSPP = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbSPP
            // 
            this.cmbSPP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSPP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSPP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSPP.FormattingEnabled = true;
            this.cmbSPP.Location = new System.Drawing.Point(0, 0);
            this.cmbSPP.Name = "cmbSPP";
            this.cmbSPP.Size = new System.Drawing.Size(454, 23);
            this.cmbSPP.TabIndex = 0;
            this.cmbSPP.SelectedIndexChanged += new System.EventHandler(this.cmbSPP_SelectedIndexChanged);
            // 
            // ctrlSPP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbSPP);
            this.Name = "ctrlSPP";
            this.Size = new System.Drawing.Size(454, 24);
            this.Load += new System.EventHandler(this.ctrlSPP_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSPP;
    }
}
