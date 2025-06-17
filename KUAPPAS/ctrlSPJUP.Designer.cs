namespace KUAPPAS
{
    partial class ctrlSPJUP
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
            this.cmbSPJ = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbSPJ
            // 
            this.cmbSPJ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSPJ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSPJ.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSPJ.FormattingEnabled = true;
            this.cmbSPJ.Location = new System.Drawing.Point(0, 0);
            this.cmbSPJ.Name = "cmbSPJ";
            this.cmbSPJ.Size = new System.Drawing.Size(386, 24);
            this.cmbSPJ.TabIndex = 0;
            this.cmbSPJ.SelectedIndexChanged += new System.EventHandler(this.cmbSPJ_SelectedIndexChanged);
            // 
            // ctrlSPJUP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbSPJ);
            this.Name = "ctrlSPJUP";
            this.Size = new System.Drawing.Size(386, 27);
            this.Load += new System.EventHandler(this.ctrlSPJUP_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSPJ;
    }
}
