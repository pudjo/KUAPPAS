namespace KUAPPAS
{
    partial class frmMapUrusanBAruProgram
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
            this.ctrlUrusanPemerintahan1 = new KUAPPAS.ctrlUrusanPemerintahan();
            this.ctrlProgram1 = new KUAPPAS.ctrlProgram();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrlUrusanBaru1 = new KUAPPAS.ctrlUrusanBaru();
            this.cmdSImpan = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.gridProgram = new System.Windows.Forms.DataGridView();
            this.IDUrusanLama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDProgram = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDUrusanBaru = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaUrusanBaru = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridProgram)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(93, 35);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(812, 28);
            this.ctrlDinas1.TabIndex = 0;
            this.ctrlDinas1.OnChanged += new KUAPPAS.ctrlDinas.ValueChangedEventHandler(this.ctrlDinas1_OnChanged);
            // 
            // ctrlUrusanPemerintahan1
            // 
            this.ctrlUrusanPemerintahan1.Location = new System.Drawing.Point(184, 61);
            this.ctrlUrusanPemerintahan1.Name = "ctrlUrusanPemerintahan1";
            this.ctrlUrusanPemerintahan1.Size = new System.Drawing.Size(704, 21);
            this.ctrlUrusanPemerintahan1.TabIndex = 1;
            this.ctrlUrusanPemerintahan1.OnChanged += new KUAPPAS.ctrlUrusanPemerintahan.ValueChangedEventHandler(this.ctrlUrusanPemerintahan1_OnChanged);
            // 
            // ctrlProgram1
            // 
            this.ctrlProgram1.Location = new System.Drawing.Point(186, 88);
            this.ctrlProgram1.Name = "ctrlProgram1";
            this.ctrlProgram1.Size = new System.Drawing.Size(702, 23);
            this.ctrlProgram1.TabIndex = 2;
            this.ctrlProgram1.OnChanged += new KUAPPAS.ctrlProgram.ValueChangedEventHandler(this.ctrlProgram1_OnChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Urusan Pemerintahan Lama";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(121, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Program";
            // 
            // ctrlUrusanBaru1
            // 
            this.ctrlUrusanBaru1.Location = new System.Drawing.Point(184, 117);
            this.ctrlUrusanBaru1.Name = "ctrlUrusanBaru1";
            this.ctrlUrusanBaru1.Size = new System.Drawing.Size(704, 26);
            this.ctrlUrusanBaru1.TabIndex = 5;
            // 
            // cmdSImpan
            // 
            this.cmdSImpan.Location = new System.Drawing.Point(184, 149);
            this.cmdSImpan.Name = "cmdSImpan";
            this.cmdSImpan.Size = new System.Drawing.Size(84, 32);
            this.cmdSImpan.TabIndex = 6;
            this.cmdSImpan.Text = "Simpan";
            this.cmdSImpan.UseVisualStyleBackColor = true;
            this.cmdSImpan.Click += new System.EventHandler(this.cmdSImpan_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(90, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Urusan Baru";
            // 
            // gridProgram
            // 
            this.gridProgram.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProgram.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDUrusanLama,
            this.IDProgram,
            this.Nama,
            this.IDUrusanBaru,
            this.NamaUrusanBaru});
            this.gridProgram.Location = new System.Drawing.Point(26, 199);
            this.gridProgram.Name = "gridProgram";
            this.gridProgram.Size = new System.Drawing.Size(1056, 309);
            this.gridProgram.TabIndex = 8;
            // 
            // IDUrusanLama
            // 
            this.IDUrusanLama.HeaderText = "ID Lama";
            this.IDUrusanLama.Name = "IDUrusanLama";
            this.IDUrusanLama.ReadOnly = true;
            // 
            // IDProgram
            // 
            this.IDProgram.HeaderText = "ID Program";
            this.IDProgram.Name = "IDProgram";
            this.IDProgram.ReadOnly = true;
            // 
            // Nama
            // 
            this.Nama.HeaderText = "Nama";
            this.Nama.Name = "Nama";
            this.Nama.ReadOnly = true;
            this.Nama.Width = 500;
            // 
            // IDUrusanBaru
            // 
            this.IDUrusanBaru.HeaderText = "Urusan Baru";
            this.IDUrusanBaru.Name = "IDUrusanBaru";
            this.IDUrusanBaru.ReadOnly = true;
            // 
            // NamaUrusanBaru
            // 
            this.NamaUrusanBaru.HeaderText = "Nama Urusan Baru";
            this.NamaUrusanBaru.Name = "NamaUrusanBaru";
            this.NamaUrusanBaru.ReadOnly = true;
            this.NamaUrusanBaru.Width = 250;
            // 
            // frmMapUrusanBAruProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 581);
            this.Controls.Add(this.gridProgram);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdSImpan);
            this.Controls.Add(this.ctrlUrusanBaru1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlProgram1);
            this.Controls.Add(this.ctrlUrusanPemerintahan1);
            this.Controls.Add(this.ctrlDinas1);
            this.Name = "frmMapUrusanBAruProgram";
            this.Text = "frmMapUrusanBAruProgram";
            this.Load += new System.EventHandler(this.frmMapUrusanBAruProgram_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridProgram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlDinas ctrlDinas1;
        private ctrlUrusanPemerintahan ctrlUrusanPemerintahan1;
        private ctrlProgram ctrlProgram1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private ctrlUrusanBaru ctrlUrusanBaru1;
        private System.Windows.Forms.Button cmdSImpan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView gridProgram;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDUrusanLama;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDProgram;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDUrusanBaru;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaUrusanBaru;
    }
}