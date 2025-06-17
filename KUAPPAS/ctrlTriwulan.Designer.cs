namespace KUAPPAS
{
    partial class ctrlTriwulan
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
            this.cmbTriwulan = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbTriwulan
            // 
            this.cmbTriwulan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTriwulan.FormattingEnabled = true;
            this.cmbTriwulan.Location = new System.Drawing.Point(0, 0);
            this.cmbTriwulan.Name = "cmbTriwulan";
            this.cmbTriwulan.Size = new System.Drawing.Size(285, 21);
            this.cmbTriwulan.TabIndex = 0;
            this.cmbTriwulan.SelectedIndexChanged += new System.EventHandler(this.cmbTriwulan_SelectedIndexChanged);
            // 
            // ctrlTriwulan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbTriwulan);
            this.Name = "ctrlTriwulan";
            this.Size = new System.Drawing.Size(285, 23);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbTriwulan;
    }
}
