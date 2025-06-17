namespace KUAPPAS.Akunting
{
    partial class frmPenjurnalan
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
            this.ctrlPanelPencarian1 = new KUAPPAS.Bendahara.ctrlPanelPencarian();
            this.chkSKRSKPD = new System.Windows.Forms.CheckBox();
            this.chkSTS = new System.Windows.Forms.CheckBox();
            this.chkSetor = new System.Windows.Forms.CheckBox();
            this.chkBAST = new System.Windows.Forms.CheckBox();
            this.chkSP2D = new System.Windows.Forms.CheckBox();
            this.chkBPK = new System.Windows.Forms.CheckBox();
            this.chkPengembalianBelanja = new System.Windows.Forms.CheckBox();
            this.chkKoreksi = new System.Windows.Forms.CheckBox();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.SuspendLayout();
            // 
            // ctrlPanelPencarian1
            // 
            this.ctrlPanelPencarian1.Location = new System.Drawing.Point(0, 43);
            this.ctrlPanelPencarian1.Name = "ctrlPanelPencarian1";
            this.ctrlPanelPencarian1.Size = new System.Drawing.Size(571, 445);
            this.ctrlPanelPencarian1.TabIndex = 1;
            this.ctrlPanelPencarian1.OnDisplay += new KUAPPAS.Bendahara.ctrlPanelPencarian.ValueChangedEventHandler(this.ctrlPanelPencarian1_OnDisplay);
            this.ctrlPanelPencarian1.Load += new System.EventHandler(this.ctrlPanelPencarian1_Load);
            // 
            // chkSKRSKPD
            // 
            this.chkSKRSKPD.AutoSize = true;
            this.chkSKRSKPD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSKRSKPD.Location = new System.Drawing.Point(31, 132);
            this.chkSKRSKPD.Name = "chkSKRSKPD";
            this.chkSKRSKPD.Size = new System.Drawing.Size(123, 19);
            this.chkSKRSKPD.TabIndex = 2;
            this.chkSKRSKPD.Text = "S K R ?S K P D";
            this.chkSKRSKPD.UseVisualStyleBackColor = true;
            this.chkSKRSKPD.Visible = false;
            // 
            // chkSTS
            // 
            this.chkSTS.AutoSize = true;
            this.chkSTS.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSTS.Location = new System.Drawing.Point(31, 156);
            this.chkSTS.Name = "chkSTS";
            this.chkSTS.Size = new System.Drawing.Size(108, 19);
            this.chkSTS.TabIndex = 3;
            this.chkSTS.Text = "Penerimaan ";
            this.chkSTS.UseVisualStyleBackColor = true;
            // 
            // chkSetor
            // 
            this.chkSetor.AutoSize = true;
            this.chkSetor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSetor.Location = new System.Drawing.Point(31, 180);
            this.chkSetor.Name = "chkSetor";
            this.chkSetor.Size = new System.Drawing.Size(165, 19);
            this.chkSetor.TabIndex = 4;
            this.chkSetor.Text = "Penyetoran ke KASDA";
            this.chkSetor.UseVisualStyleBackColor = true;
            this.chkSetor.Visible = false;
            // 
            // chkBAST
            // 
            this.chkBAST.AutoSize = true;
            this.chkBAST.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBAST.Location = new System.Drawing.Point(31, 231);
            this.chkBAST.Name = "chkBAST";
            this.chkBAST.Size = new System.Drawing.Size(72, 19);
            this.chkBAST.TabIndex = 5;
            this.chkBAST.Text = "B A S T";
            this.chkBAST.UseVisualStyleBackColor = true;
            // 
            // chkSP2D
            // 
            this.chkSP2D.AutoSize = true;
            this.chkSP2D.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSP2D.Location = new System.Drawing.Point(31, 255);
            this.chkSP2D.Name = "chkSP2D";
            this.chkSP2D.Size = new System.Drawing.Size(74, 19);
            this.chkSP2D.TabIndex = 6;
            this.chkSP2D.Text = "S P 2 D";
            this.chkSP2D.UseVisualStyleBackColor = true;
            // 
            // chkBPK
            // 
            this.chkBPK.AutoSize = true;
            this.chkBPK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBPK.Location = new System.Drawing.Point(31, 279);
            this.chkBPK.Name = "chkBPK";
            this.chkBPK.Size = new System.Drawing.Size(79, 19);
            this.chkBPK.TabIndex = 7;
            this.chkBPK.Text = "Belanja ";
            this.chkBPK.UseVisualStyleBackColor = true;
            // 
            // chkPengembalianBelanja
            // 
            this.chkPengembalianBelanja.AutoSize = true;
            this.chkPengembalianBelanja.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPengembalianBelanja.Location = new System.Drawing.Point(31, 303);
            this.chkPengembalianBelanja.Name = "chkPengembalianBelanja";
            this.chkPengembalianBelanja.Size = new System.Drawing.Size(172, 19);
            this.chkPengembalianBelanja.TabIndex = 8;
            this.chkPengembalianBelanja.Text = "Pengembalian Belanja";
            this.chkPengembalianBelanja.UseVisualStyleBackColor = true;
            // 
            // chkKoreksi
            // 
            this.chkKoreksi.AutoSize = true;
            this.chkKoreksi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkKoreksi.Location = new System.Drawing.Point(31, 327);
            this.chkKoreksi.Name = "chkKoreksi";
            this.chkKoreksi.Size = new System.Drawing.Size(74, 19);
            this.chkKoreksi.TabIndex = 9;
            this.chkKoreksi.Text = "Koreksi";
            this.chkKoreksi.UseVisualStyleBackColor = true;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1065, 41);
            this.ctrlHeader1.TabIndex = 10;
            // 
            // frmPenjurnalan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 509);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.chkKoreksi);
            this.Controls.Add(this.chkPengembalianBelanja);
            this.Controls.Add(this.chkBPK);
            this.Controls.Add(this.chkSP2D);
            this.Controls.Add(this.chkBAST);
            this.Controls.Add(this.chkSetor);
            this.Controls.Add(this.chkSTS);
            this.Controls.Add(this.chkSKRSKPD);
            this.Controls.Add(this.ctrlPanelPencarian1);
            this.KeyPreview = true;
            this.Name = "frmPenjurnalan";
            this.Text = "Jurnal Transaski ";
            this.Load += new System.EventHandler(this.frmPenjurnalan_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

       // private MdiTabControl.TabControl tabMDI;
        private Bendahara.ctrlPanelPencarian ctrlPanelPencarian1;
        private System.Windows.Forms.CheckBox chkSKRSKPD;
        private System.Windows.Forms.CheckBox chkSTS;
        private System.Windows.Forms.CheckBox chkSetor;
        private System.Windows.Forms.CheckBox chkBAST;
        private System.Windows.Forms.CheckBox chkSP2D;
        private System.Windows.Forms.CheckBox chkBPK;
        private System.Windows.Forms.CheckBox chkPengembalianBelanja;
        private System.Windows.Forms.CheckBox chkKoreksi;
        private ctrlHeader ctrlHeader1;
    }
}