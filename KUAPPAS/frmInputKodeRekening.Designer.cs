namespace KUAPPAS
{
    partial class frmInputKodeRekening
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtKodeRekening = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cmdCek = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.treeRekening1 = new KUAPPAS.treeRekening();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNama = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 364);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kode Rekening yang dipilih";
            // 
            // txtKodeRekening
            // 
            this.txtKodeRekening.Location = new System.Drawing.Point(216, 360);
            this.txtKodeRekening.Name = "txtKodeRekening";
            this.txtKodeRekening.Size = new System.Drawing.Size(212, 20);
            this.txtKodeRekening.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(25, 406);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmdCek
            // 
            this.cmdCek.Location = new System.Drawing.Point(484, 368);
            this.cmdCek.Name = "cmdCek";
            this.cmdCek.Size = new System.Drawing.Size(75, 23);
            this.cmdCek.TabIndex = 3;
            this.cmdCek.Text = "Cek";
            this.cmdCek.UseVisualStyleBackColor = true;
            this.cmdCek.Visible = false;
            this.cmdCek.Click += new System.EventHandler(this.cmdCek_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(565, 406);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Batal";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // treeRekening1
            // 
            this.treeRekening1.BackColor = System.Drawing.SystemColors.Control;
            this.treeRekening1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeRekening1.Location = new System.Drawing.Point(-3, 34);
            this.treeRekening1.Name = "treeRekening1";
            this.treeRekening1.Profile = 1;
            this.treeRekening1.Size = new System.Drawing.Size(666, 320);
            this.treeRekening1.TabIndex = 5;
            this.treeRekening1.DoubleClicking += new KUAPPAS.treeRekening.ValueChangedEventHandler(this.treeRekening1_DoubleClicking);
            this.treeRekening1.Load += new System.EventHandler(this.treeRekening1_Load);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(284, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Double klik Rekening yang dipilih. Harus yang paling ujung";
            // 
            // lblNama
            // 
            this.lblNama.AutoSize = true;
            this.lblNama.Location = new System.Drawing.Point(216, 387);
            this.lblNama.Name = "lblNama";
            this.lblNama.Size = new System.Drawing.Size(15, 13);
            this.lblNama.TabIndex = 7;
            this.lblNama.Text = "\'\'\'\'";
            // 
            // frmInputKodeRekening
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 455);
            this.Controls.Add(this.lblNama);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.treeRekening1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.cmdCek);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtKodeRekening);
            this.Controls.Add(this.label1);
            this.Name = "frmInputKodeRekening";
            this.Text = "Input Kode Rekening";
            this.Load += new System.EventHandler(this.frmInputKodeRekening_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKodeRekening;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button cmdCek;
        private System.Windows.Forms.Button button2;
        private treeRekening treeRekening1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNama;
    }
}