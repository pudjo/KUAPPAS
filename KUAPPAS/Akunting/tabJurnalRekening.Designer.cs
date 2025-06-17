namespace KUAPPAS.Akunting
{
    partial class tabJurnalRekening
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab1 = new System.Windows.Forms.TabPage();
            this.tab2 = new System.Windows.Forms.TabPage();
            this.tab3 = new System.Windows.Forms.TabPage();
            this.tab4 = new System.Windows.Forms.TabPage();
            this.jr1 = new KUAPPAS.Akunting.ctrlDaftarRekeningJurnal();
            this.jr2 = new KUAPPAS.Akunting.ctrlDaftarRekeningJurnal();
            this.jr3 = new KUAPPAS.Akunting.ctrlDaftarRekeningJurnal();
            this.jr4 = new KUAPPAS.Akunting.ctrlDaftarRekeningJurnal();
            this.tabControl1.SuspendLayout();
            this.tab1.SuspendLayout();
            this.tab2.SuspendLayout();
            this.tab3.SuspendLayout();
            this.tab4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tab1);
            this.tabControl1.Controls.Add(this.tab2);
            this.tabControl1.Controls.Add(this.tab3);
            this.tabControl1.Controls.Add(this.tab4);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(713, 228);
            this.tabControl1.TabIndex = 0;
            // 
            // tab1
            // 
            this.tab1.Controls.Add(this.jr1);
            this.tab1.Location = new System.Drawing.Point(4, 22);
            this.tab1.Name = "tab1";
            this.tab1.Padding = new System.Windows.Forms.Padding(3);
            this.tab1.Size = new System.Drawing.Size(705, 202);
            this.tab1.TabIndex = 0;
            this.tab1.Text = "tabPage1";
            this.tab1.UseVisualStyleBackColor = true;
            this.tab1.Click += new System.EventHandler(this.tab1_Click);
            // 
            // tab2
            // 
            this.tab2.Controls.Add(this.jr2);
            this.tab2.Location = new System.Drawing.Point(4, 22);
            this.tab2.Name = "tab2";
            this.tab2.Padding = new System.Windows.Forms.Padding(3);
            this.tab2.Size = new System.Drawing.Size(705, 202);
            this.tab2.TabIndex = 1;
            this.tab2.Text = "tabPage2";
            this.tab2.UseVisualStyleBackColor = true;
            // 
            // tab3
            // 
            this.tab3.Controls.Add(this.jr3);
            this.tab3.Location = new System.Drawing.Point(4, 22);
            this.tab3.Name = "tab3";
            this.tab3.Padding = new System.Windows.Forms.Padding(3);
            this.tab3.Size = new System.Drawing.Size(705, 202);
            this.tab3.TabIndex = 2;
            this.tab3.Text = "tabPage3";
            this.tab3.UseVisualStyleBackColor = true;
            // 
            // tab4
            // 
            this.tab4.Controls.Add(this.jr4);
            this.tab4.Location = new System.Drawing.Point(4, 22);
            this.tab4.Name = "tab4";
            this.tab4.Padding = new System.Windows.Forms.Padding(3);
            this.tab4.Size = new System.Drawing.Size(705, 202);
            this.tab4.TabIndex = 3;
            this.tab4.Text = "tabPage4";
            this.tab4.UseVisualStyleBackColor = true;
            // 
            // jr1
            // 
            this.jr1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jr1.Judul = "";
            this.jr1.Location = new System.Drawing.Point(3, 3);
            this.jr1.Name = "jr1";
            this.jr1.Size = new System.Drawing.Size(699, 196);
            this.jr1.TabIndex = 0;
            this.jr1.Load += new System.EventHandler(this.jr1_Load);
            // 
            // jr2
            // 
            this.jr2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jr2.Judul = "";
            this.jr2.Location = new System.Drawing.Point(3, 3);
            this.jr2.Name = "jr2";
            this.jr2.Size = new System.Drawing.Size(699, 196);
            this.jr2.TabIndex = 0;
            // 
            // jr3
            // 
            this.jr3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jr3.Judul = "";
            this.jr3.Location = new System.Drawing.Point(3, 3);
            this.jr3.Name = "jr3";
            this.jr3.Size = new System.Drawing.Size(699, 196);
            this.jr3.TabIndex = 0;
            // 
            // jr4
            // 
            this.jr4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jr4.Judul = "";
            this.jr4.Location = new System.Drawing.Point(3, 3);
            this.jr4.Name = "jr4";
            this.jr4.Size = new System.Drawing.Size(699, 196);
            this.jr4.TabIndex = 0;
            this.jr4.Load += new System.EventHandler(this.jr4_Load);
            // 
            // tabJurnalRekening
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "tabJurnalRekening";
            this.Size = new System.Drawing.Size(719, 234);
            this.Load += new System.EventHandler(this.tabJurnalRekening_Load);
            this.tabControl1.ResumeLayout(false);
            this.tab1.ResumeLayout(false);
            this.tab2.ResumeLayout(false);
            this.tab3.ResumeLayout(false);
            this.tab4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab1;
        private ctrlDaftarRekeningJurnal jr1;
        private System.Windows.Forms.TabPage tab2;
        private ctrlDaftarRekeningJurnal jr2;
        private System.Windows.Forms.TabPage tab3;
        private ctrlDaftarRekeningJurnal jr3;
        private System.Windows.Forms.TabPage tab4;
        private ctrlDaftarRekeningJurnal jr4;
    }
}
