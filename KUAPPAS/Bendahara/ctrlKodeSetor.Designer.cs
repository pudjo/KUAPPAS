﻿namespace KUAPPAS.Bendahara
{
    partial class ctrlKodeSetor
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
            this.cmbKodeSetor = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbKodeSetor
            // 
            this.cmbKodeSetor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbKodeSetor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbKodeSetor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbKodeSetor.FormattingEnabled = true;
            this.cmbKodeSetor.Location = new System.Drawing.Point(0, 0);
            this.cmbKodeSetor.Name = "cmbKodeSetor";
            this.cmbKodeSetor.Size = new System.Drawing.Size(480, 24);
            this.cmbKodeSetor.TabIndex = 0;
            this.cmbKodeSetor.SelectedIndexChanged += new System.EventHandler(this.cmbKodeSetor_SelectedIndexChanged);
            // 
            // ctrlKodeSetor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbKodeSetor);
            this.Name = "ctrlKodeSetor";
            this.Size = new System.Drawing.Size(480, 43);
            this.Load += new System.EventHandler(this.KodeSetor_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbKodeSetor;
    }
}
