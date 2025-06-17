namespace KUAPPAS
{
    partial class ctrlProgram
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
            this.cmbProgram = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbProgram
            // 
            this.cmbProgram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbProgram.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cmbProgram.FormattingEnabled = true;
            this.cmbProgram.Location = new System.Drawing.Point(0, 0);
            this.cmbProgram.Name = "cmbProgram";
            this.cmbProgram.Size = new System.Drawing.Size(434, 23);
            this.cmbProgram.TabIndex = 0;
            this.cmbProgram.SelectedIndexChanged += new System.EventHandler(this.cmbProgram_SelectedIndexChanged);
            // 
            // ctrlProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbProgram);
            this.Name = "ctrlProgram";
            this.Size = new System.Drawing.Size(434, 24);
            this.Load += new System.EventHandler(this.ctrlProgram_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbProgram;
    }
}
