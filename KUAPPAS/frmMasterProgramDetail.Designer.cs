namespace KUAPPAS
{
    partial class frmMasterProgramDetail
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
            this.ctrlUrusan1 = new KUAPPAS.ctrlUrusan();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Detail = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ToLeft = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Rincian = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PerentKode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pagu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlUrusan1
            // 
            this.ctrlUrusan1.Location = new System.Drawing.Point(188, 113);
            this.ctrlUrusan1.Name = "ctrlUrusan1";
            this.ctrlUrusan1.Size = new System.Drawing.Size(542, 27);
            this.ctrlUrusan1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(188, 139);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(58, 20);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(188, 165);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(539, 20);
            this.textBox2.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Detail,
            this.ToLeft,
            this.Rincian,
            this.Nama,
            this.Kode,
            this.PerentKode,
            this.Pagu,
            this.Pos});
            this.dataGridView1.Location = new System.Drawing.Point(1, 218);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(952, 285);
            this.dataGridView1.TabIndex = 3;
            // 
            // Detail
            // 
            this.Detail.HeaderText = "Detail";
            this.Detail.Name = "Detail";
            this.Detail.Width = 50;
            // 
            // ToLeft
            // 
            this.ToLeft.HeaderText = "<<";
            this.ToLeft.Name = "ToLeft";
            this.ToLeft.Width = 40;
            // 
            // Rincian
            // 
            this.Rincian.HeaderText = ">>";
            this.Rincian.Name = "Rincian";
            this.Rincian.Width = 30;
            // 
            // Nama
            // 
            this.Nama.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Nama.HeaderText = "Nama";
            this.Nama.Name = "Nama";
            // 
            // Kode
            // 
            this.Kode.HeaderText = "Kode";
            this.Kode.Name = "Kode";
            this.Kode.Width = 40;
            // 
            // PerentKode
            // 
            this.PerentKode.HeaderText = "ParentKode";
            this.PerentKode.Name = "PerentKode";
            this.PerentKode.Visible = false;
            // 
            // Pagu
            // 
            this.Pagu.HeaderText = "Pagu";
            this.Pagu.Name = "Pagu";
            this.Pagu.Width = 150;
            // 
            // Pos
            // 
            this.Pos.HeaderText = "Pos";
            this.Pos.Name = "Pos";
            this.Pos.Visible = false;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ctrlHeader1.Location = new System.Drawing.Point(1, 1);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(952, 51);
            this.ctrlHeader1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Urusan Pemerintahan";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Kode Program";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Nama Program";
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(76, 58);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(670, 49);
            this.ctrlDinas1.TabIndex = 8;
            // 
            // frmMasterProgramDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 529);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ctrlUrusan1);
            this.Name = "frmMasterProgramDetail";
            this.Text = "Peogram Kegiatan Detail";
            this.Load += new System.EventHandler(this.frmMasterProgramDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlUrusan ctrlUrusan1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewButtonColumn Detail;
        private System.Windows.Forms.DataGridViewButtonColumn ToLeft;
        private System.Windows.Forms.DataGridViewButtonColumn Rincian;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kode;
        private System.Windows.Forms.DataGridViewTextBoxColumn PerentKode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pagu;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pos;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private ctrlDinas ctrlDinas1;
    }
}