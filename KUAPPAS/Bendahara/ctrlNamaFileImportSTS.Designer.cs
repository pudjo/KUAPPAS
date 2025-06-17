namespace KUAPPAS.Bendahara
{
    partial class ctrlNamaFileImportSTS
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
            this.cmbNamaFile = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbNamaFile
            // 
            this.cmbNamaFile.FormattingEnabled = true;
            this.cmbNamaFile.Location = new System.Drawing.Point(3, 0);
            this.cmbNamaFile.Name = "cmbNamaFile";
            this.cmbNamaFile.Size = new System.Drawing.Size(178, 21);
            this.cmbNamaFile.TabIndex = 0;
            this.cmbNamaFile.SelectedIndexChanged += new System.EventHandler(this.cmbNamaFile_SelectedIndexChanged);
            // 
            // ctrlNamaFileImportSTS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbNamaFile);
            this.Name = "ctrlNamaFileImportSTS";
            this.Size = new System.Drawing.Size(182, 23);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbNamaFile;
    }
}
