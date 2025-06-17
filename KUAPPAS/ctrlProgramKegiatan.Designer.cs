namespace KUAPPAS
{
    partial class ctrlProgramKegiatan
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
            this.ctrlUrusanPemerintahan1 = new KUAPPAS.ctrlUrusanPemerintahan();
            this.ctrlProgram1 = new KUAPPAS.ctrlProgram();
            this.ctrlSubKegiatan1 = new KUAPPAS.ctrlSubKegiatan();
            this.ctrlKegiatanAPBD1 = new KUAPPAS.ctrlKegiatanAPBD();
            this.SuspendLayout();
            // 
            // ctrlUrusanPemerintahan1
            // 
            this.ctrlUrusanPemerintahan1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlUrusanPemerintahan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlUrusanPemerintahan1.Location = new System.Drawing.Point(0, 4);
            this.ctrlUrusanPemerintahan1.Name = "ctrlUrusanPemerintahan1";
            this.ctrlUrusanPemerintahan1.Size = new System.Drawing.Size(547, 21);
            this.ctrlUrusanPemerintahan1.TabIndex = 2;
            this.ctrlUrusanPemerintahan1.OnChanged += new KUAPPAS.ctrlUrusanPemerintahan.ValueChangedEventHandler(this.ctrlUrusanPemerintahan1_OnChanged);
            this.ctrlUrusanPemerintahan1.Load += new System.EventHandler(this.ctrlUrusanPemerintahan1_Load);
            // 
            // ctrlProgram1
            // 
            this.ctrlProgram1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlProgram1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlProgram1.Location = new System.Drawing.Point(0, 26);
            this.ctrlProgram1.Name = "ctrlProgram1";
            this.ctrlProgram1.Size = new System.Drawing.Size(547, 24);
            this.ctrlProgram1.TabIndex = 4;
            this.ctrlProgram1.OnChanged += new KUAPPAS.ctrlProgram.ValueChangedEventHandler(this.ctrlProgram1_OnChanged);
            // 
            // ctrlSubKegiatan1
            // 
            this.ctrlSubKegiatan1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlSubKegiatan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSubKegiatan1.Location = new System.Drawing.Point(0, 75);
            this.ctrlSubKegiatan1.Name = "ctrlSubKegiatan1";
            this.ctrlSubKegiatan1.Profile = 1;
            this.ctrlSubKegiatan1.Size = new System.Drawing.Size(547, 24);
            this.ctrlSubKegiatan1.TabIndex = 6;
            this.ctrlSubKegiatan1.OnChanged += new KUAPPAS.ctrlSubKegiatan.ValueChangedEventHandler(this.ctrlSubKegiatan1_OnChanged);
            // 
            // ctrlKegiatanAPBD1
            // 
            this.ctrlKegiatanAPBD1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlKegiatanAPBD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlKegiatanAPBD1.Location = new System.Drawing.Point(0, 51);
            this.ctrlKegiatanAPBD1.Name = "ctrlKegiatanAPBD1";
            this.ctrlKegiatanAPBD1.Profile = 1;
            this.ctrlKegiatanAPBD1.Size = new System.Drawing.Size(547, 22);
            this.ctrlKegiatanAPBD1.TabIndex = 7;
            this.ctrlKegiatanAPBD1.OnChanged += new KUAPPAS.ctrlKegiatanAPBD.ValueChangedEventHandler(this.ctrlKegiatanAPBD1_OnChanged);
            // 
            // ctrlProgramKegiatan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctrlKegiatanAPBD1);
            this.Controls.Add(this.ctrlSubKegiatan1);
            this.Controls.Add(this.ctrlProgram1);
            this.Controls.Add(this.ctrlUrusanPemerintahan1);
            this.Name = "ctrlProgramKegiatan";
            this.Size = new System.Drawing.Size(565, 128);
            this.Load += new System.EventHandler(this.ctrlProgramKegiatan_Load);
            this.Resize += new System.EventHandler(this.ctrlProgramKegiatan_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlUrusanPemerintahan ctrlUrusanPemerintahan1;
        private ctrlProgram ctrlProgram1;
        private ctrlSubKegiatan ctrlSubKegiatan1;
        private ctrlKegiatanAPBD ctrlKegiatanAPBD1;

    }
}
