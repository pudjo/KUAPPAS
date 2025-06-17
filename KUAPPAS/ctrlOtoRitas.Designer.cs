namespace KUAPPAS
{
    partial class ctrlOtoRitas
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
            this.cmbOtoritas = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbOtoritas
            // 
            this.cmbOtoritas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbOtoritas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbOtoritas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOtoritas.FormattingEnabled = true;
            this.cmbOtoritas.Location = new System.Drawing.Point(0, 0);
            this.cmbOtoritas.Name = "cmbOtoritas";
            this.cmbOtoritas.Size = new System.Drawing.Size(419, 23);
            this.cmbOtoritas.TabIndex = 0;
            this.cmbOtoritas.SelectedIndexChanged += new System.EventHandler(this.cmbOtoritas_SelectedIndexChanged);
            // 
            // ctrlOtoRitas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbOtoritas);
            this.Name = "ctrlOtoRitas";
            this.Size = new System.Drawing.Size(419, 23);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbOtoritas;
    }
}
