namespace KUAPPAS
{
    partial class frmRekapBelanjaLangsung
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
            this.gridRekap = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.cmdCetak = new System.Windows.Forms.Button();
            this.dtCetak = new System.Windows.Forms.DateTimePicker();
            this.IDUrusan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDProgram = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDKegiatan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Urusan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Program = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kegiatan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jumlah = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hapus = new System.Windows.Forms.DataGridViewButtonColumn();
            this.XRefresh = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridRekap)).BeginInit();
            this.SuspendLayout();
            // 
            // gridRekap
            // 
            this.gridRekap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRekap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDUrusan,
            this.IDProgram,
            this.IDKegiatan,
            this.Urusan,
            this.Program,
            this.Kegiatan,
            this.Nama,
            this.Jumlah,
            this.Hapus,
            this.XRefresh});
            this.gridRekap.Location = new System.Drawing.Point(2, 93);
            this.gridRekap.Name = "gridRekap";
            this.gridRekap.Size = new System.Drawing.Size(941, 371);
            this.gridRekap.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 469);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(941, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Location = new System.Drawing.Point(38, 62);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(632, 25);
            this.ctrlDinas1.TabIndex = 2;
            this.ctrlDinas1.OnChanged += new KUAPPAS.ctrlDinas.ValueChangedEventHandler(this.ctrlDinas1_OnChanged);
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(941, 37);
            this.ctrlHeader1.TabIndex = 3;
            // 
            // cmdCetak
            // 
            this.cmdCetak.Location = new System.Drawing.Point(828, 43);
            this.cmdCetak.Name = "cmdCetak";
            this.cmdCetak.Size = new System.Drawing.Size(101, 36);
            this.cmdCetak.TabIndex = 4;
            this.cmdCetak.Text = "Cetak";
            this.cmdCetak.UseVisualStyleBackColor = true;
            this.cmdCetak.Click += new System.EventHandler(this.cmdCetak_Click);
            // 
            // dtCetak
            // 
            this.dtCetak.Location = new System.Drawing.Point(710, 59);
            this.dtCetak.Name = "dtCetak";
            this.dtCetak.Size = new System.Drawing.Size(112, 20);
            this.dtCetak.TabIndex = 5;
            // 
            // IDUrusan
            // 
            this.IDUrusan.HeaderText = "IDUrusan";
            this.IDUrusan.Name = "IDUrusan";
            this.IDUrusan.ReadOnly = true;
            this.IDUrusan.Visible = false;
            // 
            // IDProgram
            // 
            this.IDProgram.HeaderText = "IDProgran";
            this.IDProgram.Name = "IDProgram";
            this.IDProgram.Visible = false;
            // 
            // IDKegiatan
            // 
            this.IDKegiatan.HeaderText = "IDKegiatan";
            this.IDKegiatan.Name = "IDKegiatan";
            this.IDKegiatan.Visible = false;
            // 
            // Urusan
            // 
            this.Urusan.HeaderText = "Kode Urusan";
            this.Urusan.Name = "Urusan";
            this.Urusan.ReadOnly = true;
            this.Urusan.Width = 40;
            // 
            // Program
            // 
            this.Program.HeaderText = "Kode Program";
            this.Program.Name = "Program";
            this.Program.ReadOnly = true;
            this.Program.Width = 40;
            // 
            // Kegiatan
            // 
            this.Kegiatan.HeaderText = "Kode Kegiatan";
            this.Kegiatan.Name = "Kegiatan";
            this.Kegiatan.ReadOnly = true;
            this.Kegiatan.Width = 50;
            // 
            // Nama
            // 
            this.Nama.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Nama.HeaderText = "Nama";
            this.Nama.Name = "Nama";
            this.Nama.ReadOnly = true;
            // 
            // Jumlah
            // 
            this.Jumlah.HeaderText = "Jumlah";
            this.Jumlah.Name = "Jumlah";
            this.Jumlah.ReadOnly = true;
            // 
            // Hapus
            // 
            this.Hapus.HeaderText = "Hapus";
            this.Hapus.Name = "Hapus";
            this.Hapus.ReadOnly = true;
            this.Hapus.Width = 40;
            // 
            // XRefresh
            // 
            this.XRefresh.HeaderText = "Refresh";
            this.XRefresh.Name = "XRefresh";
            this.XRefresh.ReadOnly = true;
            this.XRefresh.Width = 40;
            // 
            // frmRekapBelanjaLangsung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 491);
            this.Controls.Add(this.dtCetak);
            this.Controls.Add(this.cmdCetak);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gridRekap);
            this.Name = "frmRekapBelanjaLangsung";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".Rekap Belanja Langsung";
            this.Load += new System.EventHandler(this.frmRekapBelanjaLangsung_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridRekap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridRekap;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private ctrlDinas ctrlDinas1;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.Button cmdCetak;
        private System.Windows.Forms.DateTimePicker dtCetak;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDUrusan;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDProgram;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDKegiatan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Urusan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Program;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kegiatan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jumlah;
        private System.Windows.Forms.DataGridViewButtonColumn Hapus;
        private System.Windows.Forms.DataGridViewButtonColumn XRefresh;
    }
}