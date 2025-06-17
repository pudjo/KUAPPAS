namespace KUAPPAS
{
    partial class ctrlUrusanBaru
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
            this.cmbUrusanBaru = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbUrusanBaru
            // 
            this.cmbUrusanBaru.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbUrusanBaru.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUrusanBaru.FormattingEnabled = true;
            this.cmbUrusanBaru.Location = new System.Drawing.Point(0, 0);
            this.cmbUrusanBaru.Name = "cmbUrusanBaru";
            this.cmbUrusanBaru.Size = new System.Drawing.Size(435, 22);
            this.cmbUrusanBaru.TabIndex = 0;
            this.cmbUrusanBaru.SelectedIndexChanged += new System.EventHandler(this.cmbUrusanBaru_SelectedIndexChanged);
            this.cmbUrusanBaru.Click += new System.EventHandler(this.cmbUrusanBaru_Click);
            // 
            // ctrlUrusanBaru
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbUrusanBaru);
            this.Name = "ctrlUrusanBaru";
            this.Size = new System.Drawing.Size(435, 49);
            this.Load += new System.EventHandler(this.ctrlUrusanBaru_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbUrusanBaru;
    }
}
