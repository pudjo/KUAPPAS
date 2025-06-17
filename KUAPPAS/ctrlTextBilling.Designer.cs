namespace KUAPPAS
{
    partial class ctrlTextBilling
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
            this.txtIDBilling = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtIDBilling
            // 
            this.txtIDBilling.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtIDBilling.Location = new System.Drawing.Point(0, 0);
            this.txtIDBilling.Name = "txtIDBilling";
            this.txtIDBilling.Size = new System.Drawing.Size(238, 20);
            this.txtIDBilling.TabIndex = 0;
            // 
            // ctrlTextBilling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtIDBilling);
            this.Name = "ctrlTextBilling";
            this.Size = new System.Drawing.Size(238, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIDBilling;
    }
}
