namespace KUAPPAS
{
    partial class frmDasarHukum
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
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeRekening1 = new KUAPPAS.treeRekening();
            this.rbPerkada = new System.Windows.Forms.RadioButton();
            this.rbPerda = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gridSasarHukum = new System.Windows.Forms.DataGridView();
            this.IDRekaning = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KodeRekening = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DasarHukum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.ctrlKodeRekeningTerpisah1 = new KUAPPAS.ctrlKodeRekeningTerpisah();
            this.ctrlNavigation1 = new KUAPPAS.ctrlNavigation();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSasarHukum)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1017, 37);
            this.ctrlHeader1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 37);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeRekening1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rbPerkada);
            this.splitContainer1.Panel2.Controls.Add(this.rbPerda);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.txtNo);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.gridSasarHukum);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.txtKeterangan);
            this.splitContainer1.Panel2.Controls.Add(this.ctrlKodeRekeningTerpisah1);
            this.splitContainer1.Panel2.Controls.Add(this.ctrlNavigation1);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(1017, 452);
            this.splitContainer1.SplitterDistance = 470;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeRekening1
            // 
            this.treeRekening1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.treeRekening1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeRekening1.Location = new System.Drawing.Point(0, 0);
            this.treeRekening1.Name = "treeRekening1";
            this.treeRekening1.Size = new System.Drawing.Size(470, 452);
            this.treeRekening1.TabIndex = 0;
            this.treeRekening1.Changed += new KUAPPAS.treeRekening.ValueChangedEventHandler(this.treeRekening1_Changed);
            // 
            // rbPerkada
            // 
            this.rbPerkada.AutoSize = true;
            this.rbPerkada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPerkada.Location = new System.Drawing.Point(267, 46);
            this.rbPerkada.Name = "rbPerkada";
            this.rbPerkada.Size = new System.Drawing.Size(221, 20);
            this.rbPerkada.TabIndex = 11;
            this.rbPerkada.TabStop = true;
            this.rbPerkada.Text = "Tercantum dalam PERKADA";
            this.rbPerkada.UseVisualStyleBackColor = true;
            this.rbPerkada.Click += new System.EventHandler(this.rbPerkada_Click);
            // 
            // rbPerda
            // 
            this.rbPerda.AutoSize = true;
            this.rbPerda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPerda.Location = new System.Drawing.Point(36, 46);
            this.rbPerda.Name = "rbPerda";
            this.rbPerda.Size = new System.Drawing.Size(202, 20);
            this.rbPerda.TabIndex = 10;
            this.rbPerda.TabStop = true;
            this.rbPerda.Text = "Tercantum dalam PERDA";
            this.rbPerda.UseVisualStyleBackColor = true;
            this.rbPerda.Click += new System.EventHandler(this.rbPerda_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(22, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Rekening";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "No Urut";
            // 
            // txtNo
            // 
            this.txtNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNo.Location = new System.Drawing.Point(101, 171);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(100, 23);
            this.txtNo.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 283);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Daftar Dasar Hukum";
            // 
            // gridSasarHukum
            // 
            this.gridSasarHukum.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSasarHukum.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSasarHukum.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDRekaning,
            this.KodeRekening,
            this.Nama,
            this.No,
            this.DasarHukum});
            this.gridSasarHukum.Location = new System.Drawing.Point(7, 302);
            this.gridSasarHukum.Name = "gridSasarHukum";
            this.gridSasarHukum.Size = new System.Drawing.Size(533, 138);
            this.gridSasarHukum.TabIndex = 0;
            this.gridSasarHukum.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSasarHukum_CellClick);
            // 
            // IDRekaning
            // 
            this.IDRekaning.HeaderText = "IDRekening";
            this.IDRekaning.Name = "IDRekaning";
            this.IDRekaning.Visible = false;
            // 
            // KodeRekening
            // 
            this.KodeRekening.HeaderText = "Kode Rekening";
            this.KodeRekening.Name = "KodeRekening";
            this.KodeRekening.ReadOnly = true;
            // 
            // Nama
            // 
            this.Nama.HeaderText = "Nama";
            this.Nama.Name = "Nama";
            this.Nama.ReadOnly = true;
            // 
            // No
            // 
            this.No.HeaderText = "No Urut";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.Width = 50;
            // 
            // DasarHukum
            // 
            this.DasarHukum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DasarHukum.HeaderText = "Dasar Hukum";
            this.DasarHukum.Name = "DasarHukum";
            this.DasarHukum.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Dasar Hukum";
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKeterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKeterangan.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKeterangan.Location = new System.Drawing.Point(3, 212);
            this.txtKeterangan.Multiline = true;
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(528, 68);
            this.txtKeterangan.TabIndex = 2;
            // 
            // ctrlKodeRekeningTerpisah1
            // 
            this.ctrlKodeRekeningTerpisah1.Location = new System.Drawing.Point(101, 100);
            this.ctrlKodeRekeningTerpisah1.Name = "ctrlKodeRekeningTerpisah1";
            this.ctrlKodeRekeningTerpisah1.Size = new System.Drawing.Size(304, 65);
            this.ctrlKodeRekeningTerpisah1.TabIndex = 1;
            // 
            // ctrlNavigation1
            // 
            this.ctrlNavigation1.BackColor = System.Drawing.Color.DimGray;
            this.ctrlNavigation1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlNavigation1.Location = new System.Drawing.Point(0, 0);
            this.ctrlNavigation1.Name = "ctrlNavigation1";
            this.ctrlNavigation1.Size = new System.Drawing.Size(543, 35);
            this.ctrlNavigation1.TabIndex = 0;
            this.ctrlNavigation1.OnAdd += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnAdd);
            this.ctrlNavigation1.OnSave += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnSave);
            this.ctrlNavigation1.OnDelete += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnDelete);
            this.ctrlNavigation1.Load += new System.EventHandler(this.ctrlNavigation1_Load);
            // 
            // frmDasarHukum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 489);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmDasarHukum";
            this.Text = "Dasar Hukum ";
            this.Load += new System.EventHandler(this.frmDasarHukum_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSasarHukum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtKeterangan;
        private ctrlKodeRekeningTerpisah ctrlKodeRekeningTerpisah1;
        private ctrlNavigation ctrlNavigation1;
        private System.Windows.Forms.DataGridView gridSasarHukum;
        private System.Windows.Forms.Label label1;
        private treeRekening treeRekening1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDRekaning;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodeRekening;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn DasarHukum;
        private System.Windows.Forms.RadioButton rbPerkada;
        private System.Windows.Forms.RadioButton rbPerda;
    }
}