namespace KUAPPAS
{
    partial class frmRekapKUAPPAS
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.cmdCetak = new System.Windows.Forms.Button();
            this.chkPendapatan = new System.Windows.Forms.CheckBox();
            this.chkBelanjaTidakLangsung = new System.Windows.Forms.CheckBox();
            this.chkBelanjaLangsung = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Location = new System.Drawing.Point(4, 2);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(754, 50);
            this.ctrlHeader1.TabIndex = 2;
            // 
            // cmdCetak
            // 
            this.cmdCetak.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCetak.Location = new System.Drawing.Point(82, 203);
            this.cmdCetak.Name = "cmdCetak";
            this.cmdCetak.Size = new System.Drawing.Size(166, 35);
            this.cmdCetak.TabIndex = 3;
            this.cmdCetak.Text = "Cetak";
            this.cmdCetak.UseVisualStyleBackColor = true;
            this.cmdCetak.Click += new System.EventHandler(this.cmdCetak_Click);
            // 
            // chkPendapatan
            // 
            this.chkPendapatan.AutoSize = true;
            this.chkPendapatan.Location = new System.Drawing.Point(57, 77);
            this.chkPendapatan.Name = "chkPendapatan";
            this.chkPendapatan.Size = new System.Drawing.Size(84, 17);
            this.chkPendapatan.TabIndex = 4;
            this.chkPendapatan.Text = "Pendapatan";
            this.chkPendapatan.UseVisualStyleBackColor = true;
            // 
            // chkBelanjaTidakLangsung
            // 
            this.chkBelanjaTidakLangsung.AutoSize = true;
            this.chkBelanjaTidakLangsung.Location = new System.Drawing.Point(57, 102);
            this.chkBelanjaTidakLangsung.Name = "chkBelanjaTidakLangsung";
            this.chkBelanjaTidakLangsung.Size = new System.Drawing.Size(141, 17);
            this.chkBelanjaTidakLangsung.TabIndex = 5;
            this.chkBelanjaTidakLangsung.Text = "Belanja Tidak Langsung";
            this.chkBelanjaTidakLangsung.UseVisualStyleBackColor = true;
            // 
            // chkBelanjaLangsung
            // 
            this.chkBelanjaLangsung.AutoSize = true;
            this.chkBelanjaLangsung.Location = new System.Drawing.Point(57, 132);
            this.chkBelanjaLangsung.Name = "chkBelanjaLangsung";
            this.chkBelanjaLangsung.Size = new System.Drawing.Size(111, 17);
            this.chkBelanjaLangsung.TabIndex = 6;
            this.chkBelanjaLangsung.Text = "Belanja Langsung";
            this.chkBelanjaLangsung.UseVisualStyleBackColor = true;
            // 
            // frmRekapKUAPPAS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 285);
            this.Controls.Add(this.chkBelanjaLangsung);
            this.Controls.Add(this.chkBelanjaTidakLangsung);
            this.Controls.Add(this.chkPendapatan);
            this.Controls.Add(this.cmdCetak);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmRekapKUAPPAS";
            this.Text = "Rekap pagu per Dinas";
            this.Load += new System.EventHandler(this.frmRekapKUAPPAS_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.Button cmdCetak;
        private System.Windows.Forms.CheckBox chkPendapatan;
        private System.Windows.Forms.CheckBox chkBelanjaTidakLangsung;
        private System.Windows.Forms.CheckBox chkBelanjaLangsung;
    }
}