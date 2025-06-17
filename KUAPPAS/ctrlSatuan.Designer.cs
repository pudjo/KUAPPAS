namespace KUAPPAS
{
    partial class ctrlSatuan
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
            this.cmbSatuan = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbSatuan
            // 
            this.cmbSatuan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSatuan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSatuan.FormattingEnabled = true;
            this.cmbSatuan.Location = new System.Drawing.Point(0, 0);
            this.cmbSatuan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbSatuan.Name = "cmbSatuan";
            this.cmbSatuan.Size = new System.Drawing.Size(491, 24);
            this.cmbSatuan.TabIndex = 0;
            this.cmbSatuan.SelectedIndexChanged += new System.EventHandler(this.cmbSatuan_SelectedIndexChanged);
            // 
            // ctrlSatuan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbSatuan);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ctrlSatuan";
            this.Size = new System.Drawing.Size(491, 33);
            this.Load += new System.EventHandler(this.ctrlSatuan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSatuan;
    }
}
