namespace KUAPPAS
{
    partial class ctrlKodeRekening
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
            this.ctrlKodeRekeningTerpisah1 = new KUAPPAS.ctrlKodeRekeningTerpisah();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ctrlKodeRekeningTerpisah1
            // 
            this.ctrlKodeRekeningTerpisah1.Location = new System.Drawing.Point(0, 3);
            this.ctrlKodeRekeningTerpisah1.Name = "ctrlKodeRekeningTerpisah1";
            this.ctrlKodeRekeningTerpisah1.Size = new System.Drawing.Size(347, 29);
            this.ctrlKodeRekeningTerpisah1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // ctrlKodeRekening
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlKodeRekeningTerpisah1);
            this.Name = "ctrlKodeRekening";
            this.Size = new System.Drawing.Size(521, 69);
            this.Load += new System.EventHandler(this.ctrlKodeRekening_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlKodeRekeningTerpisah ctrlKodeRekeningTerpisah1;
        private System.Windows.Forms.Label label1;
    }
}
