namespace KUAPPAS
{
    partial class frmRekap
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridRekap = new System.Windows.Forms.DataGridView();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.Kode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaSKPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BTL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jumlah = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Graph = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBTL = new System.Windows.Forms.TextBox();
            this.txtBL = new System.Windows.Forms.TextBox();
            this.txtJ = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridRekap)).BeginInit();
            this.SuspendLayout();
            // 
            // gridRekap
            // 
            this.gridRekap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridRekap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRekap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Kode,
            this.NamaSKPD,
            this.BTL,
            this.BL,
            this.Jumlah,
            this.Graph});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridRekap.DefaultCellStyle = dataGridViewCellStyle4;
            this.gridRekap.Location = new System.Drawing.Point(4, 139);
            this.gridRekap.Name = "gridRekap";
            this.gridRekap.Size = new System.Drawing.Size(918, 357);
            this.gridRekap.TabIndex = 0;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(926, 43);
            this.ctrlHeader1.TabIndex = 1;
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Location = new System.Drawing.Point(4, 86);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(117, 31);
            this.cmdRefresh.TabIndex = 2;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // Kode
            // 
            this.Kode.HeaderText = "Kode";
            this.Kode.Name = "Kode";
            this.Kode.ReadOnly = true;
            this.Kode.Width = 50;
            // 
            // NamaSKPD
            // 
            this.NamaSKPD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NamaSKPD.HeaderText = "Nama SKPD";
            this.NamaSKPD.Name = "NamaSKPD";
            this.NamaSKPD.ReadOnly = true;
            // 
            // BTL
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.BTL.DefaultCellStyle = dataGridViewCellStyle1;
            this.BTL.HeaderText = "Belanja Tdk Langsung";
            this.BTL.Name = "BTL";
            this.BTL.ReadOnly = true;
            this.BTL.Width = 150;
            // 
            // BL
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.BL.DefaultCellStyle = dataGridViewCellStyle2;
            this.BL.HeaderText = "Belanja Langsung";
            this.BL.Name = "BL";
            this.BL.ReadOnly = true;
            this.BL.Width = 150;
            // 
            // Jumlah
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.Jumlah.DefaultCellStyle = dataGridViewCellStyle3;
            this.Jumlah.HeaderText = "Jumlah";
            this.Jumlah.Name = "Jumlah";
            this.Jumlah.ReadOnly = true;
            this.Jumlah.Width = 150;
            // 
            // Graph
            // 
            this.Graph.HeaderText = "";
            this.Graph.Name = "Graph";
            this.Graph.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(271, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Jumlah Belanja Tidak Langsung";
            // 
            // txtBTL
            // 
            this.txtBTL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBTL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBTL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBTL.Location = new System.Drawing.Point(524, 48);
            this.txtBTL.Name = "txtBTL";
            this.txtBTL.Size = new System.Drawing.Size(246, 26);
            this.txtBTL.TabIndex = 4;
            this.txtBTL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBL
            // 
            this.txtBL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBL.Location = new System.Drawing.Point(524, 74);
            this.txtBL.Name = "txtBL";
            this.txtBL.Size = new System.Drawing.Size(246, 26);
            this.txtBL.TabIndex = 5;
            this.txtBL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtJ
            // 
            this.txtJ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJ.Location = new System.Drawing.Point(524, 98);
            this.txtJ.Name = "txtJ";
            this.txtJ.Size = new System.Drawing.Size(246, 26);
            this.txtJ.TabIndex = 6;
            this.txtJ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(271, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Jumlah Belanja Langsung";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(271, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Jumlah";
            // 
            // frmRekap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 536);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtJ);
            this.Controls.Add(this.txtBL);
            this.Controls.Add(this.txtBTL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.ctrlHeader1);
            this.Controls.Add(this.gridRekap);
            this.Name = "frmRekap";
            this.Text = "frmRekap";
            this.Load += new System.EventHandler(this.frmRekap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridRekap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridRekap;
        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kode;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaSKPD;
        private System.Windows.Forms.DataGridViewTextBoxColumn BTL;
        private System.Windows.Forms.DataGridViewTextBoxColumn BL;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jumlah;
        private System.Windows.Forms.DataGridViewTextBoxColumn Graph;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBTL;
        private System.Windows.Forms.TextBox txtBL;
        private System.Windows.Forms.TextBox txtJ;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}