namespace KUAPPAS
{
    partial class ctrlBulan
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
            this.cmbBulan = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbBulan
            // 
            this.cmbBulan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbBulan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbBulan.FormattingEnabled = true;
            this.cmbBulan.Location = new System.Drawing.Point(0, 0);
            this.cmbBulan.Name = "cmbBulan";
            this.cmbBulan.Size = new System.Drawing.Size(204, 21);
            this.cmbBulan.TabIndex = 0;
            this.cmbBulan.SelectedIndexChanged += new System.EventHandler(this.cmbBulan_SelectedIndexChanged);
            // 
            // ctrlBulan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbBulan);
            this.Name = "ctrlBulan";
            this.Size = new System.Drawing.Size(204, 22);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbBulan;
    }
}
