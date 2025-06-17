namespace KUAPPAS
{
    partial class TreeProgramKegiatan
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
            this.cmdCari = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.tvProgramKegiatan = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdCari
            // 
            this.cmdCari.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCari.BackColor = System.Drawing.Color.White;
            this.cmdCari.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdCari.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.cmdCari.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCari.Image = global::KUAPPAS.Properties.Resources.search;
            this.cmdCari.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdCari.Location = new System.Drawing.Point(430, 7);
            this.cmdCari.Name = "cmdCari";
            this.cmdCari.Size = new System.Drawing.Size(51, 22);
            this.cmdCari.TabIndex = 1;
            this.cmdCari.Text = "Cari";
            this.cmdCari.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCari.UseVisualStyleBackColor = false;
            this.cmdCari.Visible = false;
            this.cmdCari.Click += new System.EventHandler(this.cmdCari_Click);
            // 
            // txtCari
            // 
            this.txtCari.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCari.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCari.Location = new System.Drawing.Point(409, 7);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(15, 20);
            this.txtCari.TabIndex = 2;
            this.txtCari.Visible = false;
            this.txtCari.TextChanged += new System.EventHandler(this.txtCari_TextChanged);
            // 
            // tvProgramKegiatan
            // 
            this.tvProgramKegiatan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvProgramKegiatan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tvProgramKegiatan.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvProgramKegiatan.Location = new System.Drawing.Point(-2, 33);
            this.tvProgramKegiatan.Name = "tvProgramKegiatan";
            this.tvProgramKegiatan.Size = new System.Drawing.Size(502, 448);
            this.tvProgramKegiatan.TabIndex = 3;
            this.tvProgramKegiatan.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvProgramKegiatan_BeforeExpand);
            this.tvProgramKegiatan.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.tvProgramKegiatan_DrawNode);
            this.tvProgramKegiatan.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvProgramKegiatan_AfterSelect);
            this.tvProgramKegiatan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvProgramKegiatan_MouseDown);
            this.tvProgramKegiatan.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvProgramKegiatan_MouseUp);
            this.tvProgramKegiatan.Validating += new System.ComponentModel.CancelEventHandler(this.tvProgramKegiatan_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Program Kegiatan Sub Kegiatan";
            // 
            // TreeProgramKegiatan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tvProgramKegiatan);
            this.Controls.Add(this.txtCari);
            this.Controls.Add(this.cmdCari);
            this.Name = "TreeProgramKegiatan";
            this.Size = new System.Drawing.Size(503, 484);
            this.Load += new System.EventHandler(this.TreeProgramKegiatan_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCari;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.TreeView tvProgramKegiatan;
        private System.Windows.Forms.Label label1;
    }
}
