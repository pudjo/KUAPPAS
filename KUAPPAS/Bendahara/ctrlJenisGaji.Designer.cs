namespace KUAPPAS.Bendahara
{
    partial class ctrlJenisGaji
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
            this.cmbJenisGaji = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbJenisGaji
            // 
            this.cmbJenisGaji.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbJenisGaji.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJenisGaji.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbJenisGaji.FormattingEnabled = true;
            this.cmbJenisGaji.Location = new System.Drawing.Point(0, 0);
            this.cmbJenisGaji.Name = "cmbJenisGaji";
            this.cmbJenisGaji.Size = new System.Drawing.Size(367, 21);
            this.cmbJenisGaji.TabIndex = 0;
            this.cmbJenisGaji.SelectedIndexChanged += new System.EventHandler(this.cmbJenisGaji_SelectedIndexChanged);
            // 
            // ctrlJenisGaji
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbJenisGaji);
            this.Name = "ctrlJenisGaji";
            this.Size = new System.Drawing.Size(367, 25);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbJenisGaji;
    }
}
