namespace KUAPPAS
{
    partial class treeSKPD
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
            this.components = new System.ComponentModel.Container();
            this.tvDinas = new System.Windows.Forms.TreeView();
            this.menuDinas = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuTambahSKPD = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSepDKPD = new System.Windows.Forms.ToolStripSeparator();
            this.menuEditSKPD = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHapusSKPD = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSepUK = new System.Windows.Forms.ToolStripSeparator();
            this.menuTambahUK = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditUK = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHapusUnitKerja = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSepSubUK = new System.Windows.Forms.ToolStripSeparator();
            this.menuTambahSubUnit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditSubUnit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHapusSubUnit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDinas.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvDinas
            // 
            this.tvDinas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvDinas.Location = new System.Drawing.Point(0, 19);
            this.tvDinas.Name = "tvDinas";
            this.tvDinas.Size = new System.Drawing.Size(149, 69);
            this.tvDinas.TabIndex = 0;
            // 
            // menuDinas
            // 
            this.menuDinas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTambahSKPD,
            this.menuSepDKPD,
            this.menuEditSKPD,
            this.menuHapusSKPD,
            this.menuSepUK,
            this.menuTambahUK,
            this.menuEditUK,
            this.menuHapusUnitKerja,
            this.menuSepSubUK,
            this.menuTambahSubUnit,
            this.menuEditSubUnit,
            this.menuHapusSubUnit});
            this.menuDinas.Name = "menuDinas";
            this.menuDinas.Size = new System.Drawing.Size(196, 220);
            // 
            // menuTambahSKPD
            // 
            this.menuTambahSKPD.Name = "menuTambahSKPD";
            this.menuTambahSKPD.Size = new System.Drawing.Size(195, 22);
            this.menuTambahSKPD.Text = "Tambah SKPD";
            // 
            // menuSepDKPD
            // 
            this.menuSepDKPD.Name = "menuSepDKPD";
            this.menuSepDKPD.Size = new System.Drawing.Size(192, 6);
            // 
            // menuEditSKPD
            // 
            this.menuEditSKPD.Name = "menuEditSKPD";
            this.menuEditSKPD.Size = new System.Drawing.Size(195, 22);
            this.menuEditSKPD.Text = "Edit SKPD";
            // 
            // menuHapusSKPD
            // 
            this.menuHapusSKPD.Name = "menuHapusSKPD";
            this.menuHapusSKPD.Size = new System.Drawing.Size(195, 22);
            this.menuHapusSKPD.Text = "Hapus SKPD";
            // 
            // menuSepUK
            // 
            this.menuSepUK.Name = "menuSepUK";
            this.menuSepUK.Size = new System.Drawing.Size(192, 6);
            // 
            // menuTambahUK
            // 
            this.menuTambahUK.Name = "menuTambahUK";
            this.menuTambahUK.Size = new System.Drawing.Size(195, 22);
            this.menuTambahUK.Text = "Tambah Unit Kerja";
            // 
            // menuEditUK
            // 
            this.menuEditUK.Name = "menuEditUK";
            this.menuEditUK.Size = new System.Drawing.Size(195, 22);
            this.menuEditUK.Text = "Edit Unit Kerja";
            // 
            // menuHapusUnitKerja
            // 
            this.menuHapusUnitKerja.Name = "menuHapusUnitKerja";
            this.menuHapusUnitKerja.Size = new System.Drawing.Size(195, 22);
            this.menuHapusUnitKerja.Text = "Hapus Unit Kerja";
            // 
            // menuSepSubUK
            // 
            this.menuSepSubUK.Name = "menuSepSubUK";
            this.menuSepSubUK.Size = new System.Drawing.Size(192, 6);
            // 
            // menuTambahSubUnit
            // 
            this.menuTambahSubUnit.Name = "menuTambahSubUnit";
            this.menuTambahSubUnit.Size = new System.Drawing.Size(195, 22);
            this.menuTambahSubUnit.Text = "Tambah Sub Unit Kerja";
            // 
            // menuEditSubUnit
            // 
            this.menuEditSubUnit.Name = "menuEditSubUnit";
            this.menuEditSubUnit.Size = new System.Drawing.Size(195, 22);
            this.menuEditSubUnit.Text = "Edit Sub Unit";
            // 
            // menuHapusSubUnit
            // 
            this.menuHapusSubUnit.Name = "menuHapusSubUnit";
            this.menuHapusSubUnit.Size = new System.Drawing.Size(195, 22);
            this.menuHapusSubUnit.Text = "Hapus Sub Unit";
            // 
            // treeSKPD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvDinas);
            this.Name = "treeSKPD";
            this.Size = new System.Drawing.Size(150, 91);
            this.Load += new System.EventHandler(this.treeSKPD_Load);
            this.menuDinas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvDinas;
        private System.Windows.Forms.ContextMenuStrip menuDinas;
        private System.Windows.Forms.ToolStripMenuItem menuTambahSKPD;
        private System.Windows.Forms.ToolStripSeparator menuSepDKPD;
        private System.Windows.Forms.ToolStripMenuItem menuEditSKPD;
        private System.Windows.Forms.ToolStripMenuItem menuHapusSKPD;
        private System.Windows.Forms.ToolStripSeparator menuSepUK;
        private System.Windows.Forms.ToolStripMenuItem menuTambahUK;
        private System.Windows.Forms.ToolStripMenuItem menuEditUK;
        private System.Windows.Forms.ToolStripMenuItem menuHapusUnitKerja;
        private System.Windows.Forms.ToolStripSeparator menuSepSubUK;
        private System.Windows.Forms.ToolStripMenuItem menuTambahSubUnit;
        private System.Windows.Forms.ToolStripMenuItem menuEditSubUnit;
        private System.Windows.Forms.ToolStripMenuItem menuHapusSubUnit;
    }
}
