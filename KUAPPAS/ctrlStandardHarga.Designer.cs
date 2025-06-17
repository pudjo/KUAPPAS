namespace KUAPPAS
{
    partial class ctrlStandardHarga
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tvHarga = new System.Windows.Forms.TreeView();
            this.cmdCari = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.gridStandardBiaya = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDSH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaSH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TextDisplay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SatuanSH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HargaSH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PilihSH = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStandardBiaya)).BeginInit();
            this.SuspendLayout();
            // 
            // tvHarga
            // 
            this.tvHarga.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvHarga.Location = new System.Drawing.Point(3, 27);
            this.tvHarga.Name = "tvHarga";
            this.tvHarga.Size = new System.Drawing.Size(472, 334);
            this.tvHarga.TabIndex = 0;
            this.tvHarga.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvHarga_BeforeExpand);
            this.tvHarga.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeHarga_AfterSelect);
            this.tvHarga.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvHarga_NodeMouseDoubleClick_1);
            // 
            // cmdCari
            // 
            this.cmdCari.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCari.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCari.Location = new System.Drawing.Point(349, 6);
            this.cmdCari.Name = "cmdCari";
            this.cmdCari.Size = new System.Drawing.Size(54, 27);
            this.cmdCari.TabIndex = 2;
            this.cmdCari.Text = "Cari";
            this.cmdCari.UseVisualStyleBackColor = true;
            this.cmdCari.Click += new System.EventHandler(this.cmdCari_Click_1);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(486, 390);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cmdRefresh);
            this.tabPage1.Controls.Add(this.tvHarga);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(478, 364);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tree";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Location = new System.Drawing.Point(397, 3);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(75, 23);
            this.cmdRefresh.TabIndex = 1;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click_1);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtCari);
            this.tabPage2.Controls.Add(this.gridStandardBiaya);
            this.tabPage2.Controls.Add(this.cmdCari);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(478, 364);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Pencarian";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtCari
            // 
            this.txtCari.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCari.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCari.Location = new System.Drawing.Point(4, 6);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(339, 22);
            this.txtCari.TabIndex = 5;
            // 
            // gridStandardBiaya
            // 
            this.gridStandardBiaya.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridStandardBiaya.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridStandardBiaya.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDSH,
            this.NamaSH,
            this.TextDisplay,
            this.SatuanSH,
            this.HargaSH,
            this.PilihSH,
            this.ID});
            this.gridStandardBiaya.Location = new System.Drawing.Point(-1, 36);
            this.gridStandardBiaya.Name = "gridStandardBiaya";
            this.gridStandardBiaya.Size = new System.Drawing.Size(480, 322);
            this.gridStandardBiaya.TabIndex = 4;
            this.gridStandardBiaya.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridStandardBiaya_CellContentClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Nama";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "Satuan";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Harga";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn5.HeaderText = "Harga";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 150;
            // 
            // IDSH
            // 
            this.IDSH.HeaderText = "ID";
            this.IDSH.Name = "IDSH";
            this.IDSH.ReadOnly = true;
            this.IDSH.Visible = false;
            // 
            // NamaSH
            // 
            this.NamaSH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NamaSH.HeaderText = "Nama";
            this.NamaSH.Name = "NamaSH";
            this.NamaSH.ReadOnly = true;
            // 
            // TextDisplay
            // 
            this.TextDisplay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TextDisplay.HeaderText = "Taxt  Yg Tampil(Kelompok)";
            this.TextDisplay.Name = "TextDisplay";
            this.TextDisplay.ReadOnly = true;
            // 
            // SatuanSH
            // 
            this.SatuanSH.HeaderText = "Satuan";
            this.SatuanSH.Name = "SatuanSH";
            this.SatuanSH.ReadOnly = true;
            this.SatuanSH.Visible = false;
            // 
            // HargaSH
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.HargaSH.DefaultCellStyle = dataGridViewCellStyle1;
            this.HargaSH.HeaderText = "Harga";
            this.HargaSH.Name = "HargaSH";
            this.HargaSH.ReadOnly = true;
            this.HargaSH.Width = 120;
            // 
            // PilihSH
            // 
            this.PilihSH.HeaderText = "Pilih";
            this.PilihSH.Name = "PilihSH";
            this.PilihSH.ReadOnly = true;
            this.PilihSH.Width = 30;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // ctrlStandardHarga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "ctrlStandardHarga";
            this.Size = new System.Drawing.Size(486, 390);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStandardBiaya)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvHarga;
        private System.Windows.Forms.Button cmdCari;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.DataGridView gridStandardBiaya;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDSH;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaSH;
        private System.Windows.Forms.DataGridViewTextBoxColumn TextDisplay;
        private System.Windows.Forms.DataGridViewTextBoxColumn SatuanSH;
        private System.Windows.Forms.DataGridViewTextBoxColumn HargaSH;
        private System.Windows.Forms.DataGridViewButtonColumn PilihSH;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
    }
}
