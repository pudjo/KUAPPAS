namespace KUAPPAS.Akunting
{
    partial class ctrlDaftarRekeningJurnal
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridJurnaRekening = new System.Windows.Forms.DataGridView();
            this.menuCopy = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtJumlahKredit = new System.Windows.Forms.TextBox();
            this.txtJumlahDebet = new System.Windows.Forms.TextBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KodeRekening = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Debet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hapus = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridJurnaRekening)).BeginInit();
            this.menuCopy.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridJurnaRekening
            // 
            this.gridJurnaRekening.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridJurnaRekening.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridJurnaRekening.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridJurnaRekening.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KodeRekening,
            this.Nama,
            this.Debet,
            this.Kredit,
            this.Hapus});
            this.gridJurnaRekening.ContextMenuStrip = this.menuCopy;
            this.gridJurnaRekening.Location = new System.Drawing.Point(-5, 25);
            this.gridJurnaRekening.Name = "gridJurnaRekening";
            this.gridJurnaRekening.Size = new System.Drawing.Size(651, 125);
            this.gridJurnaRekening.TabIndex = 5;
            this.gridJurnaRekening.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridJurnaRekening_CellContentClick);
            // 
            // menuCopy
            // 
            this.menuCopy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCopy});
            this.menuCopy.Name = "menuCopy";
            this.menuCopy.Size = new System.Drawing.Size(103, 26);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Name = "mnuCopy";
            this.mnuCopy.Size = new System.Drawing.Size(102, 22);
            this.mnuCopy.Text = "Copy";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Kredit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Debet";
            // 
            // txtJumlahKredit
            // 
            this.txtJumlahKredit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlahKredit.Location = new System.Drawing.Point(295, 1);
            this.txtJumlahKredit.Name = "txtJumlahKredit";
            this.txtJumlahKredit.Size = new System.Drawing.Size(166, 20);
            this.txtJumlahKredit.TabIndex = 7;
            this.txtJumlahKredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtJumlahDebet
            // 
            this.txtJumlahDebet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJumlahDebet.Location = new System.Drawing.Point(50, 0);
            this.txtJumlahDebet.Name = "txtJumlahDebet";
            this.txtJumlahDebet.Size = new System.Drawing.Size(165, 20);
            this.txtJumlahDebet.TabIndex = 6;
            this.txtJumlahDebet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Kode Rekening";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 120;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn2.HeaderText = "Nama Rekening";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 250;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn3.HeaderText = "Debet";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 120;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn4.HeaderText = "Kredit";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 120;
            // 
            // KodeRekening
            // 
            this.KodeRekening.HeaderText = "Kode Rekening";
            this.KodeRekening.Name = "KodeRekening";
            this.KodeRekening.ReadOnly = true;
            this.KodeRekening.Width = 120;
            // 
            // Nama
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Nama.DefaultCellStyle = dataGridViewCellStyle1;
            this.Nama.HeaderText = "Nama Rekening";
            this.Nama.Name = "Nama";
            this.Nama.ReadOnly = true;
            this.Nama.Width = 250;
            // 
            // Debet
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Debet.DefaultCellStyle = dataGridViewCellStyle2;
            this.Debet.HeaderText = "Debet";
            this.Debet.Name = "Debet";
            this.Debet.Width = 120;
            // 
            // Kredit
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Kredit.DefaultCellStyle = dataGridViewCellStyle3;
            this.Kredit.HeaderText = "Kredit";
            this.Kredit.Name = "Kredit";
            this.Kredit.Width = 120;
            // 
            // Hapus
            // 
            this.Hapus.HeaderText = "Hapus";
            this.Hapus.Name = "Hapus";
            this.Hapus.ReadOnly = true;
            this.Hapus.Visible = false;
            this.Hapus.Width = 80;
            // 
            // ctrlDaftarRekeningJurnal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridJurnaRekening);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtJumlahKredit);
            this.Controls.Add(this.txtJumlahDebet);
            this.Name = "ctrlDaftarRekeningJurnal";
            this.Size = new System.Drawing.Size(649, 150);
            this.Load += new System.EventHandler(this.ctrlDaftarRekeningJurnal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridJurnaRekening)).EndInit();
            this.menuCopy.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridJurnaRekening;
        private System.Windows.Forms.ContextMenuStrip menuCopy;
        private System.Windows.Forms.ToolStripMenuItem mnuCopy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtJumlahKredit;
        private System.Windows.Forms.TextBox txtJumlahDebet;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodeRekening;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn Debet;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kredit;
        private System.Windows.Forms.DataGridViewButtonColumn Hapus;
    }
}
