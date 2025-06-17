namespace KUAPPAS
{
    partial class ctrlDinas
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
            this.chkPPKD = new System.Windows.Forms.CheckBox();
            this.ctrlUnitKerja1 = new KUAPPAS.ctrlUnitKerja();
            this.ctrlSKPD1 = new KUAPPAS.ctrlSKPD();
            this.SuspendLayout();
            // 
            // chkPPKD
            // 
            this.chkPPKD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkPPKD.AutoSize = true;
            this.chkPPKD.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPPKD.Location = new System.Drawing.Point(480, 3);
            this.chkPPKD.Name = "chkPPKD";
            this.chkPPKD.Size = new System.Drawing.Size(59, 18);
            this.chkPPKD.TabIndex = 3;
            this.chkPPKD.Text = "PPKD ";
            this.chkPPKD.UseVisualStyleBackColor = true;
            this.chkPPKD.CheckedChanged += new System.EventHandler(this.chkPPKD_CheckedChanged);
            // 
            // ctrlUnitKerja1
            // 
            this.ctrlUnitKerja1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlUnitKerja1.Location = new System.Drawing.Point(0, 22);
            this.ctrlUnitKerja1.Name = "ctrlUnitKerja1";
            this.ctrlUnitKerja1.Size = new System.Drawing.Size(474, 22);
            this.ctrlUnitKerja1.TabIndex = 4;
            this.ctrlUnitKerja1.OnChanged += new KUAPPAS.ctrlUnitKerja.ValueChangedEventHandler(this.ctrlUnitKerja1_OnChanged);
            this.ctrlUnitKerja1.Load += new System.EventHandler(this.ctrlUnitKerja1_Load_1);
            // 
            // ctrlSKPD1
            // 
            this.ctrlSKPD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSKPD1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlSKPD1.Location = new System.Drawing.Point(0, 0);
            this.ctrlSKPD1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ctrlSKPD1.Name = "ctrlSKPD1";
            this.ctrlSKPD1.Size = new System.Drawing.Size(473, 21);
            this.ctrlSKPD1.TabIndex = 0;
            this.ctrlSKPD1.OnChanged += new KUAPPAS.ctrlSKPD.ValueChangedEventHandler(this.ctrlSKPD1_OnChanged);
            this.ctrlSKPD1.Load += new System.EventHandler(this.ctrlSKPD1_Load);
            // 
            // ctrlDinas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.ctrlUnitKerja1);
            this.Controls.Add(this.chkPPKD);
            this.Controls.Add(this.ctrlSKPD1);
            this.Name = "ctrlDinas";
            this.Size = new System.Drawing.Size(539, 56);
            this.Load += new System.EventHandler(this.ctrlDinas_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlSKPD ctrlSKPD1;

        private System.Windows.Forms.CheckBox chkPPKD;
        private ctrlUnitKerja ctrlUnitKerja1;
    }
}
