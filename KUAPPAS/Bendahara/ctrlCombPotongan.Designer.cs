namespace KUAPPAS.Bendahara
{
    partial class ctrlCombPotongan
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
            this.cmbPotongan = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbPotongan
            // 
            this.cmbPotongan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbPotongan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPotongan.FormattingEnabled = true;
            this.cmbPotongan.Location = new System.Drawing.Point(0, 0);
            this.cmbPotongan.Name = "cmbPotongan";
            this.cmbPotongan.Size = new System.Drawing.Size(388, 21);
            this.cmbPotongan.TabIndex = 0;
            // 
            // ctrlCombPotongan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbPotongan);
            this.Name = "ctrlCombPotongan";
            this.Size = new System.Drawing.Size(388, 148);
            this.Load += new System.EventHandler(this.ctrlCombPotongan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPotongan;
    }
}
