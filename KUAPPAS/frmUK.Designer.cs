namespace KUAPPAS
{
    partial class frmUK
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
            this.ctrlSKPD1 = new KUAPPAS.ctrlSKPD();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.ctrlNavigation1 = new KUAPPAS.ctrlNavigation();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKode = new System.Windows.Forms.TextBox();
            this.lblKode = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.SuspendLayout();
            // 
            // ctrlSKPD1
            // 
            this.ctrlSKPD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSKPD1.Location = new System.Drawing.Point(142, 80);
            this.ctrlSKPD1.Name = "ctrlSKPD1";
            this.ctrlSKPD1.Size = new System.Drawing.Size(424, 25);
            this.ctrlSKPD1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(52, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "SKPD";
            // 
            // txtNama
            // 
            this.txtNama.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNama.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNama.Location = new System.Drawing.Point(142, 136);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(424, 21);
            this.txtNama.TabIndex = 2;
            // 
            // ctrlNavigation1
            // 
            this.ctrlNavigation1.BackColor = System.Drawing.Color.Gray;
            this.ctrlNavigation1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlNavigation1.Location = new System.Drawing.Point(0, 0);
            this.ctrlNavigation1.Name = "ctrlNavigation1";
            this.ctrlNavigation1.Size = new System.Drawing.Size(671, 38);
            this.ctrlNavigation1.TabIndex = 3;
            this.ctrlNavigation1.OnAdd += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnAdd);
            this.ctrlNavigation1.OnSave += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnSave);
            this.ctrlNavigation1.OnDelete += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnDelete);
            this.ctrlNavigation1.Load += new System.EventHandler(this.ctrlNavigation1_Load);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(52, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "Unit Kerja";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtKode
            // 
            this.txtKode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKode.Location = new System.Drawing.Point(142, 109);
            this.txtKode.Name = "txtKode";
            this.txtKode.Size = new System.Drawing.Size(424, 21);
            this.txtKode.TabIndex = 5;
            // 
            // lblKode
            // 
            this.lblKode.AutoSize = true;
            this.lblKode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKode.Location = new System.Drawing.Point(52, 109);
            this.lblKode.Name = "lblKode";
            this.lblKode.Size = new System.Drawing.Size(35, 14);
            this.lblKode.TabIndex = 6;
            this.lblKode.Text = "Kode";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 465);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(671, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // frmUK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 487);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblKode);
            this.Controls.Add(this.txtKode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ctrlNavigation1);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlSKPD1);
            this.Name = "frmUK";
            this.Text = "Unit Kerja";
            this.Load += new System.EventHandler(this.frmUK_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlSKPD ctrlSKPD1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNama;
        private ctrlNavigation ctrlNavigation1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKode;
        private System.Windows.Forms.Label lblKode;
        private System.Windows.Forms.StatusStrip statusStrip1;
    }
}