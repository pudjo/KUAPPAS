namespace KUAPPAS
{
    partial class treeMasterProgramKegiatan
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
            this.tvProgramKegiatan = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // tvProgramKegiatan
            // 
            this.tvProgramKegiatan.Location = new System.Drawing.Point(5, 22);
            this.tvProgramKegiatan.Name = "tvProgramKegiatan";
            this.tvProgramKegiatan.Size = new System.Drawing.Size(469, 379);
            this.tvProgramKegiatan.TabIndex = 0;
            this.tvProgramKegiatan.Validating += new System.ComponentModel.CancelEventHandler(this.treeView1_Validating);
            // 
            // treeMasterProgramKegiatan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvProgramKegiatan);
            this.Name = "treeMasterProgramKegiatan";
            this.Size = new System.Drawing.Size(474, 402);
            this.Load += new System.EventHandler(this.treeMasterProgramKegiatan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvProgramKegiatan;
    }
}
