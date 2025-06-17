namespace KUAPPAS.Bendahara
{
    partial class frmSUbKegiatan
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
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtKode = new System.Windows.Forms.TextBox();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ctrlNavigation1 = new KUAPPAS.ctrlNavigation();
            this.ctrlKegiatanAPBD1 = new KUAPPAS.ctrlKegiatanAPBD();
            this.ctrlProgram1 = new KUAPPAS.ctrlProgram();
            this.ctrlUrusanPemerintahan1 = new KUAPPAS.ctrlUrusanPemerintahan();
            this.SuspendLayout();
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(122, 44);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(561, 51);
            this.ctrlDinas1.TabIndex = 0;
            this.ctrlDinas1.OnChanged += new KUAPPAS.ctrlDinas.ValueChangedEventHandler(this.ctrlDinas1_OnChanged);
            this.ctrlDinas1.Load += new System.EventHandler(this.ctrlDinas1_Load);
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Location = new System.Drawing.Point(3, -6);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(710, 41);
            this.ctrlHeader1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "SKPD";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Urusan";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Ptogram";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Kegiatan";
            // 
            // txtKode
            // 
            this.txtKode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKode.Location = new System.Drawing.Point(126, 184);
            this.txtKode.Name = "txtKode";
            this.txtKode.Size = new System.Drawing.Size(143, 20);
            this.txtKode.TabIndex = 9;
            // 
            // txtNama
            // 
            this.txtNama.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNama.Location = new System.Drawing.Point(126, 215);
            this.txtNama.Multiline = true;
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(564, 52);
            this.txtNama.TabIndex = 10;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 323);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(748, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Kode";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(49, 215);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Nama";
            // 
            // ctrlNavigation1
            // 
            this.ctrlNavigation1.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.ctrlNavigation1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlNavigation1.Location = new System.Drawing.Point(0, 0);
            this.ctrlNavigation1.Name = "ctrlNavigation1";
            this.ctrlNavigation1.Size = new System.Drawing.Size(748, 35);
            this.ctrlNavigation1.TabIndex = 14;
            this.ctrlNavigation1.OnSave += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnSave);
            // 
            // ctrlKegiatanAPBD1
            // 
            this.ctrlKegiatanAPBD1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlKegiatanAPBD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlKegiatanAPBD1.Location = new System.Drawing.Point(126, 140);
            this.ctrlKegiatanAPBD1.Name = "ctrlKegiatanAPBD1";
            this.ctrlKegiatanAPBD1.Profile = 1;
            this.ctrlKegiatanAPBD1.Size = new System.Drawing.Size(557, 22);
            this.ctrlKegiatanAPBD1.TabIndex = 17;
            this.ctrlKegiatanAPBD1.OnChanged += new KUAPPAS.ctrlKegiatanAPBD.ValueChangedEventHandler(this.ctrlKegiatanAPBD1_OnChanged);
            // 
            // ctrlProgram1
            // 
            this.ctrlProgram1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlProgram1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlProgram1.Location = new System.Drawing.Point(126, 115);
            this.ctrlProgram1.Name = "ctrlProgram1";
            this.ctrlProgram1.Size = new System.Drawing.Size(557, 24);
            this.ctrlProgram1.TabIndex = 16;
            this.ctrlProgram1.OnChanged += new KUAPPAS.ctrlProgram.ValueChangedEventHandler(this.ctrlProgram1_OnChanged);
            // 
            // ctrlUrusanPemerintahan1
            // 
            this.ctrlUrusanPemerintahan1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlUrusanPemerintahan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlUrusanPemerintahan1.Location = new System.Drawing.Point(126, 93);
            this.ctrlUrusanPemerintahan1.Name = "ctrlUrusanPemerintahan1";
            this.ctrlUrusanPemerintahan1.Size = new System.Drawing.Size(557, 21);
            this.ctrlUrusanPemerintahan1.TabIndex = 15;
            this.ctrlUrusanPemerintahan1.OnChanged += new KUAPPAS.ctrlUrusanPemerintahan.ValueChangedEventHandler(this.ctrlUrusanPemerintahan1_OnChanged);
            // 
            // frmSUbKegiatan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 345);
            this.Controls.Add(this.ctrlKegiatanAPBD1);
            this.Controls.Add(this.ctrlProgram1);
            this.Controls.Add(this.ctrlUrusanPemerintahan1);
            this.Controls.Add(this.ctrlNavigation1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.txtKode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.ctrlDinas1);
            this.Name = "frmSUbKegiatan";
            this.Text = "SubKegiatan";
            this.Load += new System.EventHandler(this.frmSUbKegiatan_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlDinas ctrlDinas1;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtKode;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private ctrlNavigation ctrlNavigation1;
        private ctrlKegiatanAPBD ctrlKegiatanAPBD1;
        private ctrlProgram ctrlProgram1;
        private ctrlUrusanPemerintahan ctrlUrusanPemerintahan1;
    }
}