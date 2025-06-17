namespace KUAPPAS.Bendahara
{
    partial class ctrlPanjar
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
            this.cmbPanjar = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbPanjar
            // 
            this.cmbPanjar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbPanjar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPanjar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPanjar.FormattingEnabled = true;
            this.cmbPanjar.Location = new System.Drawing.Point(0, 0);
            this.cmbPanjar.Name = "cmbPanjar";
            this.cmbPanjar.Size = new System.Drawing.Size(374, 23);
            this.cmbPanjar.TabIndex = 0;
            this.cmbPanjar.SelectedIndexChanged += new System.EventHandler(this.cmbPanjar_SelectedIndexChanged);
            // 
            // ctrlPanjar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbPanjar);
            this.Name = "ctrlPanjar";
            this.Size = new System.Drawing.Size(374, 59);
            this.Load += new System.EventHandler(this.ctrlPanjar_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPanjar;
    }
}
