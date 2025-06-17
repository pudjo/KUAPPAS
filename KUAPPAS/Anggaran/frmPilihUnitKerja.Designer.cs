namespace KUAPPAS.Anggaran
{
    partial class frmPilihUnitKerja
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
            this.ctrlUnitKerja1 = new KUAPPAS.ctrlUnitKerja();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdBatal = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ctrlUnitKerja1
            // 
            this.ctrlUnitKerja1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlUnitKerja1.Location = new System.Drawing.Point(66, 78);
            this.ctrlUnitKerja1.Name = "ctrlUnitKerja1";
            this.ctrlUnitKerja1.Size = new System.Drawing.Size(424, 26);
            this.ctrlUnitKerja1.TabIndex = 0;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(552, 41);
            this.ctrlHeader1.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 339);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(552, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // cmdOK
            // 
            this.cmdOK.BackColor = System.Drawing.Color.GreenYellow;
            this.cmdOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdOK.Location = new System.Drawing.Point(399, 279);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(119, 40);
            this.cmdOK.TabIndex = 3;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = false;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdBatal
            // 
            this.cmdBatal.BackColor = System.Drawing.Color.Orange;
            this.cmdBatal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdBatal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdBatal.Location = new System.Drawing.Point(23, 279);
            this.cmdBatal.Name = "cmdBatal";
            this.cmdBatal.Size = new System.Drawing.Size(119, 40);
            this.cmdBatal.TabIndex = 4;
            this.cmdBatal.Text = "Batal";
            this.cmdBatal.UseVisualStyleBackColor = false;
            this.cmdBatal.Click += new System.EventHandler(this.cmdBatal_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Pilih Unit Kerja";
            // 
            // frmPilihUnitKerja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 361);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdBatal);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.ctrlUnitKerja1);
            this.Name = "frmPilihUnitKerja";
            this.Text = "Pilih Unit Kerja";
            this.Load += new System.EventHandler(this.frmPilihUnitKerja_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlUnitKerja ctrlUnitKerja1;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdBatal;
        private System.Windows.Forms.Label label1;
    }
}