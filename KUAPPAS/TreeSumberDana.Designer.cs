namespace KUAPPAS
{
    partial class TreeSumberDana
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
            this.tvSumberDana = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // tvSumberDana
            // 
            this.tvSumberDana.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSumberDana.Location = new System.Drawing.Point(0, 0);
            this.tvSumberDana.Name = "tvSumberDana";
            this.tvSumberDana.Size = new System.Drawing.Size(520, 390);
            this.tvSumberDana.TabIndex = 0;
            this.tvSumberDana.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvSumberDana_BeforeExpand);
            this.tvSumberDana.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSumberDana_AfterSelect);
            this.tvSumberDana.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvSumberDana_NodeMouseDoubleClick);
            // 
            // TreeSumberDana
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvSumberDana);
            this.Name = "TreeSumberDana";
            this.Size = new System.Drawing.Size(520, 390);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvSumberDana;
    }
}
