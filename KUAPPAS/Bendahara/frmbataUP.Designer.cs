namespace KUAPPAS.Bendahara
{
    partial class frmbataUP
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmdSimpan = new System.Windows.Forms.Button();
            this.gridBataUP = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaSKPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jumlah = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ctrlHeader1 = new KUAPPAS.ctrlHeader();
            ((System.ComponentModel.ISupportInitialize)(this.gridBataUP)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdSimpan
            // 
            this.cmdSimpan.Location = new System.Drawing.Point(12, 47);
            this.cmdSimpan.Name = "cmdSimpan";
            this.cmdSimpan.Size = new System.Drawing.Size(110, 38);
            this.cmdSimpan.TabIndex = 2;
            this.cmdSimpan.Text = "Simpan";
            this.cmdSimpan.UseVisualStyleBackColor = true;
            this.cmdSimpan.Click += new System.EventHandler(this.cmdSimpan_Click);
            // 
            // gridBataUP
            // 
            this.gridBataUP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridBataUP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridBataUP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.ID,
            this.NamaSKPD,
            this.Jumlah,
            this.Edit});
            this.gridBataUP.Location = new System.Drawing.Point(0, 91);
            this.gridBataUP.Name = "gridBataUP";
            this.gridBataUP.Size = new System.Drawing.Size(898, 380);
            this.gridBataUP.TabIndex = 1;
            this.gridBataUP.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridBataUP_CellEndEdit);
            // 
            // No
            // 
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.Width = 50;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // NamaSKPD
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NamaSKPD.DefaultCellStyle = dataGridViewCellStyle1;
            this.NamaSKPD.HeaderText = "Nama SKPD";
            this.NamaSKPD.Name = "NamaSKPD";
            this.NamaSKPD.ReadOnly = true;
            this.NamaSKPD.Width = 500;
            // 
            // Jumlah
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Jumlah.DefaultCellStyle = dataGridViewCellStyle2;
            this.Jumlah.HeaderText = "Jumlah";
            this.Jumlah.Name = "Jumlah";
            this.Jumlah.Width = 200;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Edit";
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Visible = false;
            // 
            // ctrlHeader1
            // 
            this.ctrlHeader1.BackColor = System.Drawing.Color.Gray;
            this.ctrlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctrlHeader1.Name = "ctrlHeader1";
            this.ctrlHeader1.Size = new System.Drawing.Size(898, 41);
            this.ctrlHeader1.TabIndex = 0;
            this.ctrlHeader1.Load += new System.EventHandler(this.ctrlHeader1_Load);
            // 
            // frmbataUP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 483);
            this.Controls.Add(this.cmdSimpan);
            this.Controls.Add(this.gridBataUP);
            this.Controls.Add(this.ctrlHeader1);
            this.Name = "frmbataUP";
            this.Text = "Batas UP";
            ((System.ComponentModel.ISupportInitialize)(this.gridBataUP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlHeader ctrlHeader1;
        private System.Windows.Forms.DataGridView gridBataUP;
        private System.Windows.Forms.Button cmdSimpan;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaSKPD;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jumlah;
        private System.Windows.Forms.DataGridViewButtonColumn Edit;
    }
}