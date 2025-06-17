namespace KUAPPAS.Bendahara
{
    partial class ctrlDaftarBank
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
            this.cmbDaftarBank = new System.Windows.Forms.ComboBox();
            this.txtKeteranganBank = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmbDaftarBank
            // 
            this.cmbDaftarBank.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDaftarBank.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDaftarBank.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDaftarBank.FormattingEnabled = true;
            this.cmbDaftarBank.Location = new System.Drawing.Point(0, 0);
            this.cmbDaftarBank.Name = "cmbDaftarBank";
            this.cmbDaftarBank.Size = new System.Drawing.Size(255, 23);
            this.cmbDaftarBank.TabIndex = 0;
            this.cmbDaftarBank.SelectedIndexChanged += new System.EventHandler(this.cmbDaftarBank_SelectedIndexChanged);
            // 
            // txtKeteranganBank
            // 
            this.txtKeteranganBank.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKeteranganBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKeteranganBank.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKeteranganBank.Location = new System.Drawing.Point(261, 0);
            this.txtKeteranganBank.Name = "txtKeteranganBank";
            this.txtKeteranganBank.Size = new System.Drawing.Size(372, 22);
            this.txtKeteranganBank.TabIndex = 1;
            // 
            // ctrlDaftarBank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.txtKeteranganBank);
            this.Controls.Add(this.cmbDaftarBank);
            this.Name = "ctrlDaftarBank";
            this.Size = new System.Drawing.Size(632, 32);
            this.Load += new System.EventHandler(this.ctrlDaftarBank_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDaftarBank;
        private System.Windows.Forms.TextBox txtKeteranganBank;
    }
}
