namespace KUAPPAS
{
    partial class ctrlMPrioritas
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
            this.treePrioritas = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treePrioritas
            // 
            this.treePrioritas.Location = new System.Drawing.Point(3, 3);
            this.treePrioritas.Name = "treePrioritas";
            this.treePrioritas.Size = new System.Drawing.Size(142, 142);
            this.treePrioritas.TabIndex = 0;
            this.treePrioritas.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treePrioritas_AfterSelect);
            // 
            // ctrlMPrioritas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treePrioritas);
            this.Name = "ctrlMPrioritas";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treePrioritas;
    }
}
