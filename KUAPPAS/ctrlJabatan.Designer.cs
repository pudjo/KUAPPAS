namespace KUAPPAS
{
    partial class ctrlJabatan
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
            this.cmbJabatan = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbJabatan
            // 
            this.cmbJabatan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbJabatan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbJabatan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbJabatan.FormattingEnabled = true;
            this.cmbJabatan.Location = new System.Drawing.Point(0, 0);
            this.cmbJabatan.Name = "cmbJabatan";
            this.cmbJabatan.Size = new System.Drawing.Size(503, 24);
            this.cmbJabatan.TabIndex = 0;
            this.cmbJabatan.SelectedIndexChanged += new System.EventHandler(this.cmbJabatan_SelectedIndexChanged);
            // 
            // ctrlJabatan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbJabatan);
            this.Name = "ctrlJabatan";
            this.Size = new System.Drawing.Size(503, 24);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbJabatan;
    }
}
