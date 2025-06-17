namespace KUAPPAS
{
    partial class ctrlDesa
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
            this.cmbDesa = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbDesa
            // 
            this.cmbDesa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbDesa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDesa.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDesa.FormattingEnabled = true;
            this.cmbDesa.Location = new System.Drawing.Point(0, 0);
            this.cmbDesa.Name = "cmbDesa";
            this.cmbDesa.Size = new System.Drawing.Size(148, 24);
            this.cmbDesa.TabIndex = 0;
            this.cmbDesa.SelectedIndexChanged += new System.EventHandler(this.cmbDesa_SelectedIndexChanged);
            this.cmbDesa.Click += new System.EventHandler(this.cmbDesa_Click);
            // 
            // ctrlDesa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbDesa);
            this.Name = "ctrlDesa";
            this.Size = new System.Drawing.Size(148, 32);
            this.Load += new System.EventHandler(this.ctrlDesa_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDesa;
    }
}
