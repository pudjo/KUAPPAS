namespace KUAPPAS.Akunting
{
    partial class frmPosting
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.chkSemuaDinas = new System.Windows.Forms.CheckBox();
            this.cmdPosting = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.ctrlSKPD1 = new KUAPPAS.ctrlSKPD();
            this.cmdTutup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 175);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(628, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // chkSemuaDinas
            // 
            this.chkSemuaDinas.AutoSize = true;
            this.chkSemuaDinas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSemuaDinas.Location = new System.Drawing.Point(42, 57);
            this.chkSemuaDinas.Name = "chkSemuaDinas";
            this.chkSemuaDinas.Size = new System.Drawing.Size(112, 19);
            this.chkSemuaDinas.TabIndex = 2;
            this.chkSemuaDinas.Text = "Semua Dinas";
            this.chkSemuaDinas.UseVisualStyleBackColor = true;
            this.chkSemuaDinas.CheckedChanged += new System.EventHandler(this.chkSemuaDinas_CheckedChanged);
            // 
            // cmdPosting
            // 
            this.cmdPosting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdPosting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPosting.Location = new System.Drawing.Point(414, 109);
            this.cmdPosting.Name = "cmdPosting";
            this.cmdPosting.Size = new System.Drawing.Size(145, 32);
            this.cmdPosting.TabIndex = 3;
            this.cmdPosting.Text = "Posting";
            this.cmdPosting.UseVisualStyleBackColor = true;
            this.cmdPosting.Click += new System.EventHandler(this.cmdPosting_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "O P D";
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(628, 41);
            this.ctrlHeader1.TabIndex = 4;
            // 
            // ctrlSKPD1
            // 
            this.ctrlSKPD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSKPD1.Location = new System.Drawing.Point(91, 80);
            this.ctrlSKPD1.Name = "ctrlSKPD1";
            this.ctrlSKPD1.Size = new System.Drawing.Size(468, 23);
            this.ctrlSKPD1.TabIndex = 0;
            // 
            // cmdTutup
            // 
            this.cmdTutup.Location = new System.Drawing.Point(91, 109);
            this.cmdTutup.Name = "cmdTutup";
            this.cmdTutup.Size = new System.Drawing.Size(145, 32);
            this.cmdTutup.TabIndex = 6;
            this.cmdTutup.Text = "Tutup";
            this.cmdTutup.UseVisualStyleBackColor = true;
            this.cmdTutup.Click += new System.EventHandler(this.cmdTutup_Click);
            // 
            // frmPosting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 197);
            this.Controls.Add(this.cmdTutup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.cmdPosting);
            this.Controls.Add(this.chkSemuaDinas);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ctrlSKPD1);
            this.Name = "frmPosting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Posting";
            this.Load += new System.EventHandler(this.frmPosting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlSKPD ctrlSKPD1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.CheckBox chkSemuaDinas;
        private System.Windows.Forms.Button cmdPosting;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdTutup;
    }
}