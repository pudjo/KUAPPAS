namespace KUAPPAS
{
    partial class ctrlDusun
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
            this.cmbDusun = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbDusun
            // 
            this.cmbDusun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbDusun.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDusun.FormattingEnabled = true;
            this.cmbDusun.Location = new System.Drawing.Point(0, 0);
            this.cmbDusun.Name = "cmbDusun";
            this.cmbDusun.Size = new System.Drawing.Size(150, 22);
            this.cmbDusun.TabIndex = 0;
            this.cmbDusun.SelectedIndexChanged += new System.EventHandler(this.cmbDusun_SelectedIndexChanged);
            this.cmbDusun.Click += new System.EventHandler(this.cmbDusun_Click);
            // 
            // ctrlDusun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbDusun);
            this.Name = "ctrlDusun";
            this.Size = new System.Drawing.Size(150, 24);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDusun;
    }
}
