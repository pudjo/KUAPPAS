namespace KUAPPAS
{
    partial class ctrlMisi
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
            this.cmbMisi = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbMisi
            // 
            this.cmbMisi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbMisi.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMisi.FormattingEnabled = true;
            this.cmbMisi.Location = new System.Drawing.Point(0, 0);
            this.cmbMisi.Name = "cmbMisi";
            this.cmbMisi.Size = new System.Drawing.Size(150, 24);
            this.cmbMisi.TabIndex = 1;
            this.cmbMisi.SelectedIndexChanged += new System.EventHandler(this.cmbMisi_SelectedIndexChanged);
            // 
            // ctrlMisi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbMisi);
            this.Name = "ctrlMisi";
            this.Size = new System.Drawing.Size(150, 35);
            this.Load += new System.EventHandler(this.ctrlMisi_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbMisi;
    }
}
