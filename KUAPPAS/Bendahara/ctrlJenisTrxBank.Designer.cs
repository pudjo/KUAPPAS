namespace KUAPPAS.Bendahara
{
    partial class ctrlJenisTrxBank
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
            this.cmbJenisTrxBank = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbJenisTrxBank
            // 
            this.cmbJenisTrxBank.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbJenisTrxBank.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbJenisTrxBank.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbJenisTrxBank.FormattingEnabled = true;
            this.cmbJenisTrxBank.Location = new System.Drawing.Point(0, 0);
            this.cmbJenisTrxBank.Name = "cmbJenisTrxBank";
            this.cmbJenisTrxBank.Size = new System.Drawing.Size(370, 23);
            this.cmbJenisTrxBank.TabIndex = 0;
            // 
            // ctrlJenisTrxBank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbJenisTrxBank);
            this.Name = "ctrlJenisTrxBank";
            this.Size = new System.Drawing.Size(370, 80);
            this.Load += new System.EventHandler(this.ctrlJenisTrxBank_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbJenisTrxBank;
    }
}
