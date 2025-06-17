namespace KUAPPAS
{
    partial class ctrlSKPD
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
            this.cmbSKPD = new System.Windows.Forms.ComboBox();
            this.cmbLevel = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbSKPD
            // 
            this.cmbSKPD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSKPD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSKPD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSKPD.FormattingEnabled = true;
            this.cmbSKPD.Location = new System.Drawing.Point(0, 0);
            this.cmbSKPD.Name = "cmbSKPD";
            this.cmbSKPD.Size = new System.Drawing.Size(365, 23);
            this.cmbSKPD.TabIndex = 0;
            this.cmbSKPD.SelectedIndexChanged += new System.EventHandler(this.cmbSKPD_SelectedIndexChanged);
            // 
            // cmbLevel
            // 
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Location = new System.Drawing.Point(27, 29);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(109, 21);
            this.cmbLevel.TabIndex = 1;
            // 
            // ctrlSKPD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbLevel);
            this.Controls.Add(this.cmbSKPD);
            this.Name = "ctrlSKPD";
            this.Size = new System.Drawing.Size(365, 77);
            this.Load += new System.EventHandler(this.ctrSKPD_Load);
            this.Resize += new System.EventHandler(this.ctrlSKPD_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSKPD;
        private System.Windows.Forms.ComboBox cmbLevel;
    }
}
