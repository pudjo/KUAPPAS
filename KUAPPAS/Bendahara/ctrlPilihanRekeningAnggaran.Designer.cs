namespace KUAPPAS.Bendahara
{
    partial class ctrlPilihanRekeningAnggaran
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
            this.cmbRekening = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbRekening
            // 
            this.cmbRekening.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbRekening.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRekening.FormattingEnabled = true;
            this.cmbRekening.Location = new System.Drawing.Point(0, 0);
            this.cmbRekening.Name = "cmbRekening";
            this.cmbRekening.Size = new System.Drawing.Size(536, 21);
            this.cmbRekening.TabIndex = 0;
            this.cmbRekening.SelectedIndexChanged += new System.EventHandler(this.cmbRekening_SelectedIndexChanged);
            this.cmbRekening.Click += new System.EventHandler(this.cmbRekening_Click);
            // 
            // ctrlPilihanRekeningAnggaran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbRekening);
            this.Name = "ctrlPilihanRekeningAnggaran";
            this.Size = new System.Drawing.Size(536, 148);
            this.Load += new System.EventHandler(this.ctrlComboAnggaran_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbRekening;
    }
}
