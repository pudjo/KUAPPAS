﻿namespace KUAPPAS
{
    partial class ctrlRPJMDTujuan
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
            this.cmbTujuan = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbTujuan
            // 
            this.cmbTujuan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTujuan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTujuan.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTujuan.FormattingEnabled = true;
            this.cmbTujuan.Location = new System.Drawing.Point(0, 0);
            this.cmbTujuan.Name = "cmbTujuan";
            this.cmbTujuan.Size = new System.Drawing.Size(609, 22);
            this.cmbTujuan.TabIndex = 0;
            // 
            // ctrlRPJMDTujuan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbTujuan);
            this.Name = "ctrlRPJMDTujuan";
            this.Size = new System.Drawing.Size(609, 57);
            this.Load += new System.EventHandler(this.ctrlRPJMDTujuan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbTujuan;
    }
}
