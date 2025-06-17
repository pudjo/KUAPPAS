namespace KUAPPAS.Akunting
{
    partial class frmListJurnal
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
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.ctrlPanelPencarian1 = new KUAPPAS.Bendahara.ctrlPanelPencarian();
            this.gridListJurnal = new System.Windows.Forms.DataGridView();
            this.NoUrut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detail = new System.Windows.Forms.DataGridViewButtonColumn();
            this.NoBukti = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tanggal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Keterangan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlPencarian1 = new KUAPPAS.ctrlPencarian();
            this.ctrlJurnalRekening1 = new KUAPPAS.Akunting.ctrlDaftarRekeningJurnal();
            ((System.ComponentModel.ISupportInitialize)(this.gridListJurnal)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(908, 47);
            this.ctrlHeader1.TabIndex = 2;
            // 
            // ctrlPanelPencarian1
            // 
            this.ctrlPanelPencarian1.Location = new System.Drawing.Point(26, 53);
            this.ctrlPanelPencarian1.Name = "ctrlPanelPencarian1";
            this.ctrlPanelPencarian1.Size = new System.Drawing.Size(705, 138);
            this.ctrlPanelPencarian1.TabIndex = 3;
            this.ctrlPanelPencarian1.OnDisplay += new KUAPPAS.Bendahara.ctrlPanelPencarian.ValueChangedEventHandler(this.ctrlPanelPencarian1_OnDisplay);
            this.ctrlPanelPencarian1.OnAdd += new KUAPPAS.Bendahara.ctrlPanelPencarian.ValueChangedEventHandler(this.ctrlPanelPencarian1_OnAdd);
            this.ctrlPanelPencarian1.Load += new System.EventHandler(this.ctrlPanelPencarian1_Load);
            // 
            // gridListJurnal
            // 
            this.gridListJurnal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridListJurnal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridListJurnal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NoUrut,
            this.Detail,
            this.NoBukti,
            this.Tanggal,
            this.Keterangan});
            this.gridListJurnal.Location = new System.Drawing.Point(0, 193);
            this.gridListJurnal.Name = "gridListJurnal";
            this.gridListJurnal.Size = new System.Drawing.Size(908, 235);
            this.gridListJurnal.TabIndex = 4;
            this.gridListJurnal.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridListJurnal_CellContentClick);
            // 
            // NoUrut
            // 
            this.NoUrut.HeaderText = "No Urut";
            this.NoUrut.Name = "NoUrut";
            this.NoUrut.ReadOnly = true;
            this.NoUrut.Visible = false;
            // 
            // Detail
            // 
            this.Detail.HeaderText = "Detail";
            this.Detail.Name = "Detail";
            this.Detail.ReadOnly = true;
            this.Detail.Width = 50;
            // 
            // NoBukti
            // 
            this.NoBukti.HeaderText = "No Bukti";
            this.NoBukti.Name = "NoBukti";
            this.NoBukti.ReadOnly = true;
            this.NoBukti.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NoBukti.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NoBukti.Width = 150;
            // 
            // Tanggal
            // 
            this.Tanggal.HeaderText = "Tanggal";
            this.Tanggal.Name = "Tanggal";
            this.Tanggal.ReadOnly = true;
            this.Tanggal.Width = 120;
            // 
            // Keterangan
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Keterangan.DefaultCellStyle = dataGridViewCellStyle1;
            this.Keterangan.HeaderText = "Keterangan";
            this.Keterangan.Name = "Keterangan";
            this.Keterangan.ReadOnly = true;
            this.Keterangan.Width = 600;
            // 
            // ctrlPencarian1
            // 
            this.ctrlPencarian1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlPencarian1.Location = new System.Drawing.Point(573, 148);
            this.ctrlPencarian1.Name = "ctrlPencarian1";
            this.ctrlPencarian1.Size = new System.Drawing.Size(335, 28);
            this.ctrlPencarian1.TabIndex = 5;
            // 
            // ctrlJurnalRekening1
            // 
            this.ctrlJurnalRekening1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlJurnalRekening1.Judul = "";
            this.ctrlJurnalRekening1.Location = new System.Drawing.Point(0, 434);
            this.ctrlJurnalRekening1.Name = "ctrlJurnalRekening1";
            this.ctrlJurnalRekening1.Size = new System.Drawing.Size(908, 158);
            this.ctrlJurnalRekening1.TabIndex = 6;
            // 
            // frmListJurnal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 565);
            this.Controls.Add(this.ctrlJurnalRekening1);
            this.Controls.Add(this.ctrlPencarian1);
            this.Controls.Add(this.gridListJurnal);
            this.Controls.Add(this.ctrlPanelPencarian1);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmListJurnal";
            this.Text = "frmListJurnal";
            this.Load += new System.EventHandler(this.frmListJurnal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridListJurnal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlHeader ctrlHeader1;
        private Bendahara.ctrlPanelPencarian ctrlPanelPencarian1;
        private System.Windows.Forms.DataGridView gridListJurnal;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoUrut;
        private System.Windows.Forms.DataGridViewButtonColumn Detail;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoBukti;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tanggal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Keterangan;
        private ctrlPencarian ctrlPencarian1;
        private ctrlDaftarRekeningJurnal ctrlJurnalRekening1;
    }
}