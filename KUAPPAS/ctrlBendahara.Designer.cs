namespace KUAPPAS
{
    partial class ctrlBendahara
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
            this.cmbBendahara = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbBendahara
            // 
            this.cmbBendahara.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbBendahara.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbBendahara.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBendahara.FormattingEnabled = true;
            this.cmbBendahara.Location = new System.Drawing.Point(0, 0);
            this.cmbBendahara.Margin = new System.Windows.Forms.Padding(1);
            this.cmbBendahara.Name = "cmbBendahara";
            this.cmbBendahara.Size = new System.Drawing.Size(148, 24);
            this.cmbBendahara.TabIndex = 0;
            this.cmbBendahara.SelectedIndexChanged += new System.EventHandler(this.cmbBendahara_SelectedIndexChanged);
            // 
            // ctrlBendahara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbBendahara);
            this.Name = "ctrlBendahara";
            this.Size = new System.Drawing.Size(148, 28);
            this.Load += new System.EventHandler(this.ctrlBendahara_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbBendahara;
    }
}
