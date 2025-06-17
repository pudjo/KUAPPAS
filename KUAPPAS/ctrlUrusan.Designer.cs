namespace KUAPPAS
{
    partial class ctrlUrusan
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
            this.cmbUrusan = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbUrusan
            // 
            this.cmbUrusan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbUrusan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUrusan.FormattingEnabled = true;
            this.cmbUrusan.Location = new System.Drawing.Point(0, 0);
            this.cmbUrusan.Name = "cmbUrusan";
            this.cmbUrusan.Size = new System.Drawing.Size(153, 23);
            this.cmbUrusan.TabIndex = 0;
            this.cmbUrusan.SelectedIndexChanged += new System.EventHandler(this.cmbUrusan_SelectedIndexChanged);
            // 
            // ctrlUrusan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbUrusan);
            this.Name = "ctrlUrusan";
            this.Size = new System.Drawing.Size(153, 25);
            this.Load += new System.EventHandler(this.ctrlUrusan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbUrusan;
    }
}
