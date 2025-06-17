namespace KUAPPAS
{
    partial class ctrlSumberDana
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
            this.cmbSumberDana = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbSumberDana
            // 
            this.cmbSumberDana.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSumberDana.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSumberDana.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSumberDana.FormattingEnabled = true;
            this.cmbSumberDana.Location = new System.Drawing.Point(0, 0);
            this.cmbSumberDana.Name = "cmbSumberDana";
            this.cmbSumberDana.Size = new System.Drawing.Size(352, 23);
            this.cmbSumberDana.TabIndex = 0;
            this.cmbSumberDana.SelectedIndexChanged += new System.EventHandler(this.cmbSumberDana_SelectedIndexChanged);
            this.cmbSumberDana.Click += new System.EventHandler(this.cmbSumberDana_Click);
            // 
            // ctrlSumberDana
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbSumberDana);
            this.Name = "ctrlSumberDana";
            this.Size = new System.Drawing.Size(352, 24);
            this.Load += new System.EventHandler(this.ctrlSumberDana_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSumberDana;
    }
}
