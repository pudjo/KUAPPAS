namespace KUAPPAS
{
    partial class treeWilayah
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
            this.tvWilayah = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // tvWilayah
            // 
            this.tvWilayah.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvWilayah.Location = new System.Drawing.Point(0, 24);
            this.tvWilayah.Name = "tvWilayah";
            this.tvWilayah.Size = new System.Drawing.Size(260, 199);
            this.tvWilayah.TabIndex = 0;
            // 
            // treeWilayah
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvWilayah);
            this.Name = "treeWilayah";
            this.Size = new System.Drawing.Size(261, 224);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvWilayah;
    }
}
