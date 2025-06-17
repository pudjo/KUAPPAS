namespace KUAPPAS
{
    partial class frmCariRekening
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
            this.txtIDRekening = new System.Windows.Forms.TextBox();
            this.txtNamaRekening = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.treeRekening1 = new KUAPPAS.treeRekening();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdBatal = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.SuspendLayout();
            // 
            // txtIDRekening
            // 
            this.txtIDRekening.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIDRekening.Location = new System.Drawing.Point(82, 392);
            this.txtIDRekening.Name = "txtIDRekening";
            this.txtIDRekening.Size = new System.Drawing.Size(100, 20);
            this.txtIDRekening.TabIndex = 1;
            // 
            // txtNamaRekening
            // 
            this.txtNamaRekening.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNamaRekening.Location = new System.Drawing.Point(197, 392);
            this.txtNamaRekening.Name = "txtNamaRekening";
            this.txtNamaRekening.Size = new System.Drawing.Size(355, 20);
            this.txtNamaRekening.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 392);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Yang Dipilih";
            // 
            // treeRekening1
            // 
            this.treeRekening1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.treeRekening1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeRekening1.Location = new System.Drawing.Point(4, 42);
            this.treeRekening1.Name = "treeRekening1";
            this.treeRekening1.Profile = 1;
            this.treeRekening1.Size = new System.Drawing.Size(558, 344);
            this.treeRekening1.TabIndex = 0;
            this.treeRekening1.Changed += new KUAPPAS.treeRekening.ValueChangedEventHandler(this.treeRekening1_Changed);
            this.treeRekening1.Load += new System.EventHandler(this.treeRekening1_Load);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(448, 431);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(104, 30);
            this.cmdOK.TabIndex = 4;
            this.cmdOK.Text = "Pilih";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdBatal
            // 
            this.cmdBatal.Location = new System.Drawing.Point(12, 431);
            this.cmdBatal.Name = "cmdBatal";
            this.cmdBatal.Size = new System.Drawing.Size(90, 34);
            this.cmdBatal.TabIndex = 5;
            this.cmdBatal.Text = "Batal";
            this.cmdBatal.UseVisualStyleBackColor = true;
            this.cmdBatal.Click += new System.EventHandler(this.cmdBatal_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 473);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(564, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // frmCariRekening
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 495);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cmdBatal);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNamaRekening);
            this.Controls.Add(this.txtIDRekening);
            this.Controls.Add(this.treeRekening1);
            this.Name = "frmCariRekening";
            this.Text = "Pencarian Rekening";
            this.Load += new System.EventHandler(this.frmCariRekening_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private treeRekening treeRekening1;
        private System.Windows.Forms.TextBox txtIDRekening;
        private System.Windows.Forms.TextBox txtNamaRekening;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdBatal;
        private System.Windows.Forms.StatusStrip statusStrip1;
    }
}