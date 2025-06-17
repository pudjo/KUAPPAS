namespace KUAPPAS.Bendahara
{
    partial class frmListBAST
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmdCariLagi = new System.Windows.Forms.Button();
            this.cmdCari = new System.Windows.Forms.Button();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.lblPencarian = new System.Windows.Forms.Label();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.ctrlPanelPencarian1 = new KUAPPAS.Bendahara.ctrlPanelPencarian();
            this.gridBAST = new System.Windows.Forms.DataGridView();
            this.IDBAST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UbahBAST = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pilihbast = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NoBAST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TanggalBAST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UraianBAST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoKontrakBAST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PihakIIIBAST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoSP2DBAST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JumlahBAST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlKontrak1 = new KUAPPAS.Bendahara.ctrlKontrak();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridBAST)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdCariLagi
            // 
            this.cmdCariLagi.Location = new System.Drawing.Point(779, 164);
            this.cmdCariLagi.Name = "cmdCariLagi";
            this.cmdCariLagi.Size = new System.Drawing.Size(75, 23);
            this.cmdCariLagi.TabIndex = 32;
            this.cmdCariLagi.Text = "Cari Lagi";
            this.cmdCariLagi.UseVisualStyleBackColor = true;
            this.cmdCariLagi.Visible = false;
            this.cmdCariLagi.Click += new System.EventHandler(this.cmdCariLagi_Click);
            // 
            // cmdCari
            // 
            this.cmdCari.Location = new System.Drawing.Point(711, 164);
            this.cmdCari.Name = "cmdCari";
            this.cmdCari.Size = new System.Drawing.Size(62, 23);
            this.cmdCari.TabIndex = 31;
            this.cmdCari.Text = "Cari...";
            this.cmdCari.UseVisualStyleBackColor = true;
            this.cmdCari.Visible = false;
            this.cmdCari.Click += new System.EventHandler(this.cmdCari_Click);
            // 
            // txtCari
            // 
            this.txtCari.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCari.Location = new System.Drawing.Point(454, 170);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(254, 20);
            this.txtCari.TabIndex = 30;
            this.txtCari.Visible = false;
            // 
            // lblPencarian
            // 
            this.lblPencarian.AutoSize = true;
            this.lblPencarian.Location = new System.Drawing.Point(391, 171);
            this.lblPencarian.Name = "lblPencarian";
            this.lblPencarian.Size = new System.Drawing.Size(55, 13);
            this.lblPencarian.TabIndex = 29;
            this.lblPencarian.Text = "Pencarian";
            this.lblPencarian.Visible = false;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(1208, 59);
            this.ctrlHeader1.TabIndex = 13;
            // 
            // ctrlPanelPencarian1
            // 
            this.ctrlPanelPencarian1.Location = new System.Drawing.Point(12, 57);
            this.ctrlPanelPencarian1.Name = "ctrlPanelPencarian1";
            this.ctrlPanelPencarian1.Size = new System.Drawing.Size(694, 152);
            this.ctrlPanelPencarian1.TabIndex = 12;
            this.ctrlPanelPencarian1.TanggalAkhir = new System.DateTime(2025, 6, 17, 13, 53, 24, 305);
            this.ctrlPanelPencarian1.TanggalAwal = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
            this.ctrlPanelPencarian1.OnDisplay += new KUAPPAS.Bendahara.ctrlPanelPencarian.ValueChangedEventHandler(this.ctrlPanelPencarian1_OnDisplay);
            this.ctrlPanelPencarian1.OnAdd += new KUAPPAS.Bendahara.ctrlPanelPencarian.ValueChangedEventHandler(this.ctrlPanelPencarian1_OnAdd);
            // 
            // gridBAST
            // 
            this.gridBAST.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridBAST.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridBAST.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridBAST.ColumnHeadersHeight = 30;
            this.gridBAST.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDBAST,
            this.UbahBAST,
            this.pilihbast,
            this.NoBAST,
            this.TanggalBAST,
            this.UraianBAST,
            this.NoKontrakBAST,
            this.PihakIIIBAST,
            this.NoSP2DBAST,
            this.JumlahBAST});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridBAST.DefaultCellStyle = dataGridViewCellStyle5;
            this.gridBAST.Location = new System.Drawing.Point(-2, 209);
            this.gridBAST.Name = "gridBAST";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridBAST.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gridBAST.Size = new System.Drawing.Size(1198, 360);
            this.gridBAST.TabIndex = 11;
            this.gridBAST.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridBAST_CellContentClick_1);
            // 
            // IDBAST
            // 
            this.IDBAST.HeaderText = "Column1";
            this.IDBAST.Name = "IDBAST";
            this.IDBAST.Visible = false;
            // 
            // UbahBAST
            // 
            this.UbahBAST.HeaderText = "Detail";
            this.UbahBAST.Name = "UbahBAST";
            this.UbahBAST.Width = 50;
            // 
            // pilihbast
            // 
            this.pilihbast.HeaderText = "Pilih";
            this.pilihbast.Name = "pilihbast";
            this.pilihbast.Width = 40;
            // 
            // NoBAST
            // 
            this.NoBAST.HeaderText = "No BAST";
            this.NoBAST.Name = "NoBAST";
            this.NoBAST.ReadOnly = true;
            this.NoBAST.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NoBAST.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NoBAST.Width = 200;
            // 
            // TanggalBAST
            // 
            this.TanggalBAST.HeaderText = "Tanggal BAST";
            this.TanggalBAST.Name = "TanggalBAST";
            this.TanggalBAST.ReadOnly = true;
            this.TanggalBAST.Width = 50;
            // 
            // UraianBAST
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.UraianBAST.DefaultCellStyle = dataGridViewCellStyle2;
            this.UraianBAST.HeaderText = "Uraian BAST";
            this.UraianBAST.Name = "UraianBAST";
            this.UraianBAST.ReadOnly = true;
            this.UraianBAST.Width = 300;
            // 
            // NoKontrakBAST
            // 
            this.NoKontrakBAST.HeaderText = "No Kontrak";
            this.NoKontrakBAST.Name = "NoKontrakBAST";
            this.NoKontrakBAST.ReadOnly = true;
            this.NoKontrakBAST.Width = 150;
            // 
            // PihakIIIBAST
            // 
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PihakIIIBAST.DefaultCellStyle = dataGridViewCellStyle3;
            this.PihakIIIBAST.HeaderText = "Pihak Ketiga";
            this.PihakIIIBAST.Name = "PihakIIIBAST";
            this.PihakIIIBAST.ReadOnly = true;
            this.PihakIIIBAST.Width = 300;
            // 
            // NoSP2DBAST
            // 
            this.NoSP2DBAST.HeaderText = "No SP2D";
            this.NoSP2DBAST.Name = "NoSP2DBAST";
            // 
            // JumlahBAST
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.JumlahBAST.DefaultCellStyle = dataGridViewCellStyle4;
            this.JumlahBAST.HeaderText = "Nilai";
            this.JumlahBAST.Name = "JumlahBAST";
            this.JumlahBAST.ReadOnly = true;
            // 
            // ctrlKontrak1
            // 
            this.ctrlKontrak1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlKontrak1.Location = new System.Drawing.Point(86, 121);
            this.ctrlKontrak1.Name = "ctrlKontrak1";
            this.ctrlKontrak1.Size = new System.Drawing.Size(501, 22);
            this.ctrlKontrak1.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Kontrak/SPK";
            this.label1.Visible = false;
            // 
            // frmListBAST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 554);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlKontrak1);
            this.Controls.Add(this.cmdCariLagi);
            this.Controls.Add(this.cmdCari);
            this.Controls.Add(this.txtCari);
            this.Controls.Add(this.lblPencarian);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.ctrlPanelPencarian1);
            this.Controls.Add(this.gridBAST);
            this.Name = "frmListBAST";
            this.Text = "Daftar BAST";
            this.Load += new System.EventHandler(this.frmListBAST_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridBAST)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridBAST;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDBAST;
        private System.Windows.Forms.DataGridViewButtonColumn UbahBAST;
        private System.Windows.Forms.DataGridViewCheckBoxColumn pilihbast;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoBAST;
        private System.Windows.Forms.DataGridViewTextBoxColumn TanggalBAST;
        private System.Windows.Forms.DataGridViewTextBoxColumn UraianBAST;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoKontrakBAST;
        private System.Windows.Forms.DataGridViewTextBoxColumn PihakIIIBAST;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoSP2DBAST;
        private System.Windows.Forms.DataGridViewTextBoxColumn JumlahBAST;
        private ctrlPanelPencarian ctrlPanelPencarian1;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.Button cmdCariLagi;
        private System.Windows.Forms.Button cmdCari;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Label lblPencarian;
        private ctrlKontrak ctrlKontrak1;
        private System.Windows.Forms.Label label1;
    }
}