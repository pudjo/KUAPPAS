namespace KUAPPAS.Bendahara
{
    partial class ctrlSumberDanaLengkap
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
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.ctrlSubSumberDana1 = new KUAPPAS.Bendahara.ctrlSubSumberDana();
            this.ctrlSumberDana1 = new KUAPPAS.ctrlSumberDana();
            this.SuspendLayout();
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKeterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKeterangan.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKeterangan.Location = new System.Drawing.Point(301, 0);
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(270, 24);
            this.txtKeterangan.TabIndex = 2;
            // 
            // ctrlSubSumberDana1
            // 
            this.ctrlSubSumberDana1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSubSumberDana1.Location = new System.Drawing.Point(159, 0);
            this.ctrlSubSumberDana1.Name = "ctrlSubSumberDana1";
            this.ctrlSubSumberDana1.Size = new System.Drawing.Size(136, 25);
            this.ctrlSubSumberDana1.TabIndex = 1;
            this.ctrlSubSumberDana1.OnChanged += new KUAPPAS.Bendahara.ctrlSubSumberDana.ValueChangedEventHandler(this.ctrlSubSumberDana1_OnChanged);
            // 
            // ctrlSumberDana1
            // 
            this.ctrlSumberDana1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSumberDana1.Location = new System.Drawing.Point(0, 0);
            this.ctrlSumberDana1.Name = "ctrlSumberDana1";
            this.ctrlSumberDana1.Size = new System.Drawing.Size(147, 25);
            this.ctrlSumberDana1.TabIndex = 0;
            this.ctrlSumberDana1.OnChanged += new KUAPPAS.ctrlSumberDana.ValueChangedEventHandler(this.ctrlSumberDana1_OnChanged);
            // 
            // ctrlSumberDanaLengkap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.txtKeterangan);
            this.Controls.Add(this.ctrlSubSumberDana1);
            this.Controls.Add(this.ctrlSumberDana1);
            this.Name = "ctrlSumberDanaLengkap";
            this.Size = new System.Drawing.Size(574, 23);
            this.Load += new System.EventHandler(this.ctrlSumberDanaLengkap_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlSumberDana ctrlSumberDana1;
        private ctrlSubSumberDana ctrlSubSumberDana1;
        private System.Windows.Forms.TextBox txtKeterangan;
    }
}
