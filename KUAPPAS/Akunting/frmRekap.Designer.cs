namespace KUAPPAS.Akunting
{
    partial class frmRekap
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtIDRekening = new System.Windows.Forms.TextBox();
            this.cmdExcell = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtIDRekening
            // 
            this.txtIDRekening.Location = new System.Drawing.Point(108, 96);
            this.txtIDRekening.Name = "txtIDRekening";
            this.txtIDRekening.Size = new System.Drawing.Size(267, 20);
            this.txtIDRekening.TabIndex = 0;
            // 
            // cmdExcell
            // 
            this.cmdExcell.Location = new System.Drawing.Point(108, 133);
            this.cmdExcell.Name = "cmdExcell";
            this.cmdExcell.Size = new System.Drawing.Size(96, 28);
            this.cmdExcell.TabIndex = 1;
            this.cmdExcell.Text = "Excell";
            this.cmdExcell.UseVisualStyleBackColor = true;
            // 
            // frmRekap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 415);
            this.Controls.Add(this.cmdExcell);
            this.Controls.Add(this.txtIDRekening);
            this.Name = "frmRekap";
            this.Text = "frmRekap";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIDRekening;
        private System.Windows.Forms.Button cmdExcell;
    }
}