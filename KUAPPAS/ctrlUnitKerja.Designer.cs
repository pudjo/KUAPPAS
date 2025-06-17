namespace KUAPPAS
{
    partial class ctrlUnitKerja
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
            this.cmbUnitKerja = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbUnitKerja
            // 
            this.cmbUnitKerja.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbUnitKerja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbUnitKerja.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cmbUnitKerja.FormattingEnabled = true;
            this.cmbUnitKerja.Location = new System.Drawing.Point(0, 0);
            this.cmbUnitKerja.Name = "cmbUnitKerja";
            this.cmbUnitKerja.Size = new System.Drawing.Size(424, 22);
            this.cmbUnitKerja.TabIndex = 1;
            this.cmbUnitKerja.SelectedIndexChanged += new System.EventHandler(this.cmbUnitKerja_SelectedIndexChanged);
            // 
            // ctrlUnitKerja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbUnitKerja);
            this.Name = "ctrlUnitKerja";
            this.Size = new System.Drawing.Size(424, 26);
            this.Load += new System.EventHandler(this.ctrlUnitKerja_Load);
            this.Resize += new System.EventHandler(this.ctrlUnitKerja_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbUnitKerja;
    }
}
