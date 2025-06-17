namespace KUAPPAS.Bendahara
{
    partial class ctrlPanelPencarian
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrlPanelPencarian));
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtAwal = new System.Windows.Forms.DateTimePicker();
            this.dtAkhir = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdUbah = new System.Windows.Forms.Button();
            this.cmdHapus = new System.Windows.Forms.Button();
            this.cmdtambah = new System.Windows.Forms.Button();
            this.cmdTampilkan = new System.Windows.Forms.Button();
            this.ctrlDinas1 = new KUAPPAS.ctrlDinas();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 16);
            this.label10.TabIndex = 25;
            this.label10.Text = "Tanggal";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 27;
            this.label1.Text = "O P D";
            // 
            // dtAwal
            // 
            this.dtAwal.CustomFormat = "dd MMM yyyy";
            this.dtAwal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtAwal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtAwal.Location = new System.Drawing.Point(73, 50);
            this.dtAwal.Name = "dtAwal";
            this.dtAwal.Size = new System.Drawing.Size(112, 22);
            this.dtAwal.TabIndex = 30;
            this.dtAwal.Value = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
            this.dtAwal.ValueChanged += new System.EventHandler(this.dtAwal_ValueChanged);
            // 
            // dtAkhir
            // 
            this.dtAkhir.CustomFormat = "dd MMM yyyy";
            this.dtAkhir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtAkhir.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtAkhir.Location = new System.Drawing.Point(224, 50);
            this.dtAkhir.Name = "dtAkhir";
            this.dtAkhir.Size = new System.Drawing.Size(108, 22);
            this.dtAkhir.TabIndex = 31;
            this.dtAkhir.ValueChanged += new System.EventHandler(this.dtAkhir_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(191, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 16);
            this.label2.TabIndex = 32;
            this.label2.Text = "s/d";
            // 
            // cmdUbah
            // 
            this.cmdUbah.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdUbah.BackColor = System.Drawing.Color.White;
            this.cmdUbah.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdUbah.FlatAppearance.BorderColor = System.Drawing.Color.Fuchsia;
            this.cmdUbah.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmdUbah.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdUbah.Image = ((System.Drawing.Image)(resources.GetObject("cmdUbah.Image")));
            this.cmdUbah.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdUbah.Location = new System.Drawing.Point(338, 108);
            this.cmdUbah.Name = "cmdUbah";
            this.cmdUbah.Size = new System.Drawing.Size(103, 40);
            this.cmdUbah.TabIndex = 29;
            this.cmdUbah.Text = "Ubah";
            this.cmdUbah.UseVisualStyleBackColor = false;
            this.cmdUbah.Visible = false;
            this.cmdUbah.Click += new System.EventHandler(this.cmdUbah_Click);
            // 
            // cmdHapus
            // 
            this.cmdHapus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdHapus.BackColor = System.Drawing.Color.White;
            this.cmdHapus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdHapus.FlatAppearance.BorderColor = System.Drawing.Color.Fuchsia;
            this.cmdHapus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmdHapus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHapus.Image = ((System.Drawing.Image)(resources.GetObject("cmdHapus.Image")));
            this.cmdHapus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdHapus.Location = new System.Drawing.Point(447, 108);
            this.cmdHapus.Name = "cmdHapus";
            this.cmdHapus.Size = new System.Drawing.Size(108, 40);
            this.cmdHapus.TabIndex = 28;
            this.cmdHapus.Text = "Hapus";
            this.cmdHapus.UseVisualStyleBackColor = false;
            this.cmdHapus.Visible = false;
            this.cmdHapus.Click += new System.EventHandler(this.cmdHapus_Click);
            // 
            // cmdtambah
            // 
            this.cmdtambah.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdtambah.BackColor = System.Drawing.Color.White;
            this.cmdtambah.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdtambah.FlatAppearance.BorderColor = System.Drawing.Color.Fuchsia;
            this.cmdtambah.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmdtambah.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdtambah.ForeColor = System.Drawing.Color.Black;
            this.cmdtambah.Image = global::KUAPPAS.Properties.Resources.edit_add;
            this.cmdtambah.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdtambah.Location = new System.Drawing.Point(160, 108);
            this.cmdtambah.Name = "cmdtambah";
            this.cmdtambah.Size = new System.Drawing.Size(172, 40);
            this.cmdtambah.TabIndex = 23;
            this.cmdtambah.Text = "Tambah Data Baru";
            this.cmdtambah.UseVisualStyleBackColor = false;
            this.cmdtambah.Click += new System.EventHandler(this.cmdtambah_Click);
            // 
            // cmdTampilkan
            // 
            this.cmdTampilkan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdTampilkan.BackColor = System.Drawing.Color.White;
            this.cmdTampilkan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdTampilkan.FlatAppearance.BorderColor = System.Drawing.Color.Fuchsia;
            this.cmdTampilkan.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmdTampilkan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTampilkan.ForeColor = System.Drawing.Color.Black;
            this.cmdTampilkan.Image = global::KUAPPAS.Properties.Resources.search;
            this.cmdTampilkan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdTampilkan.Location = new System.Drawing.Point(3, 108);
            this.cmdTampilkan.Name = "cmdTampilkan";
            this.cmdTampilkan.Size = new System.Drawing.Size(151, 40);
            this.cmdTampilkan.TabIndex = 22;
            this.cmdTampilkan.Text = "Tampilkan Data ";
            this.cmdTampilkan.UseVisualStyleBackColor = false;
            this.cmdTampilkan.Click += new System.EventHandler(this.cmdTampilkan_Click);
            // 
            // ctrlDinas1
            // 
            this.ctrlDinas1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlDinas1.Location = new System.Drawing.Point(73, 4);
            this.ctrlDinas1.Name = "ctrlDinas1";
            this.ctrlDinas1.Size = new System.Drawing.Size(629, 44);
            this.ctrlDinas1.TabIndex = 33;
            this.ctrlDinas1.UK = 0;
            this.ctrlDinas1.OnChanged += new KUAPPAS.ctrlDinas.ValueChangedEventHandler(this.ctrlDinas1_OnChanged);
            // 
            // ctrlPanelPencarian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctrlDinas1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtAkhir);
            this.Controls.Add(this.dtAwal);
            this.Controls.Add(this.cmdUbah);
            this.Controls.Add(this.cmdHapus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdtambah);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cmdTampilkan);
            this.Name = "ctrlPanelPencarian";
            this.Size = new System.Drawing.Size(705, 161);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdtambah;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button cmdTampilkan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdHapus;
        private System.Windows.Forms.Button cmdUbah;
        private System.Windows.Forms.DateTimePicker dtAwal;
        private System.Windows.Forms.DateTimePicker dtAkhir;
        private System.Windows.Forms.Label label2;
        private ctrlDinas ctrlDinas1;
    }
}
