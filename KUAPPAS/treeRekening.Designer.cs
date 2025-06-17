namespace KUAPPAS
{
    partial class treeRekening
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /// 
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(treeRekening));
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cmdCari = new System.Windows.Forms.Button();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tvRekening = new KUAPPAS.TreeStateTreeView(this.components);
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Location = new System.Drawing.Point(2, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(364, 20);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.Visible = false;
            // 
            // cmdCari
            // 
            this.cmdCari.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCari.Image = global::KUAPPAS.Properties.Resources.search;
            this.cmdCari.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdCari.Location = new System.Drawing.Point(372, 3);
            this.cmdCari.Name = "cmdCari";
            this.cmdCari.Size = new System.Drawing.Size(50, 23);
            this.cmdCari.TabIndex = 3;
            this.cmdCari.Text = "Cari";
            this.cmdCari.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCari.UseVisualStyleBackColor = true;
            this.cmdCari.Visible = false;
            this.cmdCari.Click += new System.EventHandler(this.cmdCari_Click);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRefresh.ForeColor = System.Drawing.Color.Black;
            this.cmdRefresh.Image = ((System.Drawing.Image)(resources.GetObject("cmdRefresh.Image")));
            this.cmdRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdRefresh.Location = new System.Drawing.Point(483, 4);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(55, 23);
            this.cmdRefresh.TabIndex = 1;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // tvRekening
            // 
            this.tvRekening.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvRekening.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvRekening.Location = new System.Drawing.Point(0, 30);
            this.tvRekening.Margin = new System.Windows.Forms.Padding(0);
            this.tvRekening.Name = "tvRekening";
            this.tvRekening.Size = new System.Drawing.Size(541, 464);
            this.tvRekening.TabIndex = 4;
            this.tvRekening.TriStateStyleProperty = KUAPPAS.TreeStateTreeView.TriStateStyles.Standard;
            this.tvRekening.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvRekening_BeforeExpand);
            this.tvRekening.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvRekening_AfterSelect);
            this.tvRekening.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvRekening_NodeMouseDoubleClick);
            this.tvRekening.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvRekening_MouseDown);
            // 
            // treeRekening
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tvRekening);
            this.Controls.Add(this.cmdCari);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.cmdRefresh);
            this.Name = "treeRekening";
            this.Size = new System.Drawing.Size(541, 494);
            this.Load += new System.EventHandler(this.treeRekening_Load);
            this.Resize += new System.EventHandler(this.treeRekening_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button cmdCari;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private TreeStateTreeView tvRekening;
    }
}
