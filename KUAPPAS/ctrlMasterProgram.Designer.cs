namespace KUAPPAS
{
    partial class ctrlMasterProgram
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
            this.cmbMasterProgram = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbMasterProgram
            // 
            this.cmbMasterProgram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbMasterProgram.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMasterProgram.FormattingEnabled = true;
            this.cmbMasterProgram.Location = new System.Drawing.Point(0, 0);
            this.cmbMasterProgram.Name = "cmbMasterProgram";
            this.cmbMasterProgram.Size = new System.Drawing.Size(291, 22);
            this.cmbMasterProgram.TabIndex = 0;
            this.cmbMasterProgram.SelectedIndexChanged += new System.EventHandler(this.cmbMasterProgram_SelectedIndexChanged);
            // 
            // ctrlMasterProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbMasterProgram);
            this.Name = "ctrlMasterProgram";
            this.Size = new System.Drawing.Size(291, 65);
            this.Load += new System.EventHandler(this.ctrlMasterProgram_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbMasterProgram;
    }
}
