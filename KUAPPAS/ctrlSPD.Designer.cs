namespace KUAPPAS
{
    partial class ctrlSPD
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
            this.cmbSPD = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbSPD
            // 
            this.cmbSPD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSPD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSPD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSPD.FormattingEnabled = true;
            this.cmbSPD.Location = new System.Drawing.Point(0, 0);
            this.cmbSPD.Name = "cmbSPD";
            this.cmbSPD.Size = new System.Drawing.Size(311, 23);
            this.cmbSPD.TabIndex = 0;
            this.cmbSPD.SelectedIndexChanged += new System.EventHandler(this.cmbSPD_SelectedIndexChanged);
            this.cmbSPD.Click += new System.EventHandler(this.cmbSPD_Click);
            // 
            // ctrlSPD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbSPD);
            this.Name = "ctrlSPD";
            this.Size = new System.Drawing.Size(311, 33);
            this.Load += new System.EventHandler(this.ctrlSPD_Load);
            this.Enter += new System.EventHandler(this.ctrlSPD_Enter);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSPD;
    }
}
