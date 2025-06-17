namespace KUAPPAS
{
    partial class ctrlTahapRBA
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
            this.cmbTahapRBA = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbTahapRBA
            // 
            this.cmbTahapRBA.FormattingEnabled = true;
            this.cmbTahapRBA.Location = new System.Drawing.Point(4, 1);
            this.cmbTahapRBA.Name = "cmbTahapRBA";
            this.cmbTahapRBA.Size = new System.Drawing.Size(384, 21);
            this.cmbTahapRBA.TabIndex = 0;
            // 
            // ctrlTahapRBA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbTahapRBA);
            this.Name = "ctrlTahapRBA";
            this.Size = new System.Drawing.Size(389, 44);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbTahapRBA;
    }
}
