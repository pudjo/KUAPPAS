namespace KUAPPAS.Bendahara
{
    partial class ctrlKontrak
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
            this.cmbKontrak = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbKontrak
            // 
            this.cmbKontrak.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbKontrak.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbKontrak.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbKontrak.Location = new System.Drawing.Point(0, 0);
            this.cmbKontrak.Name = "cmbKontrak";
            this.cmbKontrak.Size = new System.Drawing.Size(309, 23);
            this.cmbKontrak.TabIndex = 0;
            this.cmbKontrak.SelectedIndexChanged += new System.EventHandler(this.cmbKontrak_SelectedIndexChanged_1);
            // 
            // ctrlKontrak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbKontrak);
            this.Name = "ctrlKontrak";
            this.Size = new System.Drawing.Size(309, 28);
            this.Load += new System.EventHandler(this.ctrlKontrak_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbKontrak;

    }
}
