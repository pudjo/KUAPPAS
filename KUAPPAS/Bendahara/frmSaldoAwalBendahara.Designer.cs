namespace KUAPPAS.Bendahara
{
    partial class frmSaldoAwalBendahara
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
            this.cmdSimpan = new System.Windows.Forms.Button();
            this.cmdBatal = new System.Windows.Forms.Button();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkBank = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.SuspendLayout();
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(131, 59);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(654, 43);
            this.ctrlDinas1.TabIndex = 0;
            this.ctrlDinas1.OnChanged += new KUAPPAS.ctrlDinas.ValueChangedEventHandler(this.ctrlDinas1_OnChanged_1);
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(829, 41);
            this.ctrlHeader1.TabIndex = 1;
            // 
            // cmdSimpan
            // 
            this.cmdSimpan.Location = new System.Drawing.Point(61, 441);
            this.cmdSimpan.Name = "cmdSimpan";
            this.cmdSimpan.Size = new System.Drawing.Size(126, 45);
            this.cmdSimpan.TabIndex = 2;
            this.cmdSimpan.Text = "Simpan";
            this.cmdSimpan.UseVisualStyleBackColor = true;
            this.cmdSimpan.Click += new System.EventHandler(this.cmdSimpan_Click);
            // 
            // cmdBatal
            // 
            this.cmdBatal.Location = new System.Drawing.Point(659, 441);
            this.cmdBatal.Name = "cmdBatal";
            this.cmdBatal.Size = new System.Drawing.Size(126, 45);
            this.cmdBatal.TabIndex = 3;
            this.cmdBatal.Text = "Batal";
            this.cmdBatal.UseVisualStyleBackColor = true;
            // 
            // txtJumlah
            // 
            this.txtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlah.Location = new System.Drawing.Point(131, 108);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(187, 20);
            this.txtJumlah.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "OPD";
            // 
            // chkBank
            // 
            this.chkBank.AutoSize = true;
            this.chkBank.Location = new System.Drawing.Point(131, 153);
            this.chkBank.Name = "chkBank";
            this.chkBank.Size = new System.Drawing.Size(81, 17);
            this.chkBank.TabIndex = 6;
            this.chkBank.Text = "Saldo Bank";
            this.chkBank.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Jumlah";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 489);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(829, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // frmSaldoAwalBendahara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 511);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkBank);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.cmdBatal);
            this.Controls.Add(this.cmdSimpan);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.ctrlDinas1);
            this.Name = "frmSaldoAwalBendahara";
            this.Text = "frmSaldoAwalBendahara";
            this.Load += new System.EventHandler(this.frmSaldoAwalBendahara_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlDinas ctrlDinas1;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.Button cmdSimpan;
        private System.Windows.Forms.Button cmdBatal;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkBank;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip1;
    }
}