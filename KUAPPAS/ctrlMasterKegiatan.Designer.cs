namespace KUAPPAS
{
    partial class ctrlMasterKegiatan
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
            this.cmbKegiatan = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbKegiatan
            // 
            this.cmbKegiatan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbKegiatan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbKegiatan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbKegiatan.FormattingEnabled = true;
            this.cmbKegiatan.Location = new System.Drawing.Point(0, 0);
            this.cmbKegiatan.Name = "cmbKegiatan";
            this.cmbKegiatan.Size = new System.Drawing.Size(282, 23);
            this.cmbKegiatan.TabIndex = 0;
            // 
            // ctrlMasterKegiatan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbKegiatan);
            this.Name = "ctrlMasterKegiatan";
            this.Size = new System.Drawing.Size(282, 148);
            this.Load += new System.EventHandler(this.ctrlMasterKegiatan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbKegiatan;
    }
}
