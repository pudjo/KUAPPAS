namespace KUAPPAS.Bendahara
{
    partial class ctrlBAST
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
            this.cmbBAST = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbBAST
            // 
            this.cmbBAST.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbBAST.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbBAST.FormattingEnabled = true;
            this.cmbBAST.Location = new System.Drawing.Point(0, 0);
            this.cmbBAST.Name = "cmbBAST";
            this.cmbBAST.Size = new System.Drawing.Size(673, 21);
            this.cmbBAST.TabIndex = 0;
            this.cmbBAST.SelectedIndexChanged += new System.EventHandler(this.cmbBAST_SelectedIndexChanged);
            // 
            // ctrlBAST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbBAST);
            this.Name = "ctrlBAST";
            this.Size = new System.Drawing.Size(673, 81);
            this.Load += new System.EventHandler(this.ctrlBAST_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbBAST;
    }
}
