namespace KUAPPAS
{
    partial class ctrlSessionRKA
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
            this.cmbSessionRKA = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbSessionRKA
            // 
            this.cmbSessionRKA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSessionRKA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSessionRKA.FormattingEnabled = true;
            this.cmbSessionRKA.Location = new System.Drawing.Point(0, 0);
            this.cmbSessionRKA.Name = "cmbSessionRKA";
            this.cmbSessionRKA.Size = new System.Drawing.Size(417, 21);
            this.cmbSessionRKA.TabIndex = 0;
            this.cmbSessionRKA.SelectedIndexChanged += new System.EventHandler(this.cmbSessionRKA_SelectedIndexChanged);
            // 
            // ctrlSessionRKA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbSessionRKA);
            this.Name = "ctrlSessionRKA";
            this.Size = new System.Drawing.Size(417, 35);
            this.Load += new System.EventHandler(this.ctrlSessionRKA_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSessionRKA;
    }
}
