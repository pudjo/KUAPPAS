namespace KUAPPAS.Bendahara
{
    partial class ctrlPenandaTanganSP2D
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
            this.cmbPenandaTangan = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbPenandaTangan
            // 
            this.cmbPenandaTangan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPenandaTangan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPenandaTangan.FormattingEnabled = true;
            this.cmbPenandaTangan.Location = new System.Drawing.Point(4, 3);
            this.cmbPenandaTangan.Name = "cmbPenandaTangan";
            this.cmbPenandaTangan.Size = new System.Drawing.Size(388, 23);
            this.cmbPenandaTangan.TabIndex = 0;
            // 
            // ctrlPenandaTanganSP2D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbPenandaTangan);
            this.Name = "ctrlPenandaTanganSP2D";
            this.Size = new System.Drawing.Size(393, 87);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPenandaTangan;
    }
}
