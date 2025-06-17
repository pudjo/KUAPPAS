namespace KUAPPAS.Bendahara
{
    partial class ctrlJenisBelanja
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
            this.cmbJenisBelanja = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbJenisBelanja
            // 
            this.cmbJenisBelanja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbJenisBelanja.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbJenisBelanja.FormattingEnabled = true;
            this.cmbJenisBelanja.Location = new System.Drawing.Point(3, 5);
            this.cmbJenisBelanja.Name = "cmbJenisBelanja";
            this.cmbJenisBelanja.Size = new System.Drawing.Size(142, 23);
            this.cmbJenisBelanja.TabIndex = 0;
            this.cmbJenisBelanja.SelectedIndexChanged += new System.EventHandler(this.cmbJenisBelanja_SelectedIndexChanged);
            // 
            // ctrlJenisBelanja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbJenisBelanja);
            this.Name = "ctrlJenisBelanja";
            this.Size = new System.Drawing.Size(337, 150);
            this.Load += new System.EventHandler(this.ctrlJenisBelanja_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbJenisBelanja;
    }
}
