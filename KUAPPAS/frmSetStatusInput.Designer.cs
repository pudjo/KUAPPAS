namespace KUAPPAS
{
    partial class frmSetStatusInput
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
            this.cmdKunciRKA = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.rbRKA = new System.Windows.Forms.RadioButton();
            this.rbPenyempurnaan = new System.Windows.Forms.RadioButton();
            this.rbRKAPErubahan = new System.Windows.Forms.RadioButton();
            this.rbDPA = new System.Windows.Forms.RadioButton();
            this.cmdInputPenyempurnaan = new System.Windows.Forms.Button();
            this.cmdInputRKAP = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cmdTetapkanABT = new System.Windows.Forms.Button();
            this.cmdSimpan = new System.Windows.Forms.Button();
            this.chkSemuaDinas = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdBukaKunci = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.cmdKucniInput = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.btnMulaiTahunANggaranBaru = new System.Windows.Forms.Button();
            this.gridTahap = new System.Windows.Forms.DataGridView();
            this.Dinas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tahap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kunci = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDDInas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdBukaInput = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button7 = new System.Windows.Forms.Button();
            this.rbRKAPergeseranPerubahan = new System.Windows.Forms.RadioButton();
            this.button8 = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlSKPD1 = new KUAPPAS.ctrlSKPD();
            this.button9 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTahap)).BeginInit();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdKunciRKA
            // 
            this.cmdKunciRKA.BackColor = System.Drawing.Color.Silver;
            this.cmdKunciRKA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdKunciRKA.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdKunciRKA.Location = new System.Drawing.Point(244, 3);
            this.cmdKunciRKA.Margin = new System.Windows.Forms.Padding(4);
            this.cmdKunciRKA.Name = "cmdKunciRKA";
            this.cmdKunciRKA.Size = new System.Drawing.Size(204, 32);
            this.cmdKunciRKA.TabIndex = 2;
            this.cmdKunciRKA.Text = "Tetapkan RKA menjadi DPA";
            this.cmdKunciRKA.UseVisualStyleBackColor = false;
            this.cmdKunciRKA.Click += new System.EventHandler(this.cmdKunciRKA_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Coral;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(880, 96);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 34);
            this.button1.TabIndex = 3;
            this.button1.Text = "Kunci Anggaran Kas";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rbRKA
            // 
            this.rbRKA.AutoSize = true;
            this.rbRKA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbRKA.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRKA.Location = new System.Drawing.Point(13, 9);
            this.rbRKA.Margin = new System.Windows.Forms.Padding(4);
            this.rbRKA.Name = "rbRKA";
            this.rbRKA.Size = new System.Drawing.Size(131, 20);
            this.rbRKA.TabIndex = 4;
            this.rbRKA.TabStop = true;
            this.rbRKA.Text = "Input RKA Murni";
            this.rbRKA.UseVisualStyleBackColor = true;
            // 
            // rbPenyempurnaan
            // 
            this.rbPenyempurnaan.AutoSize = true;
            this.rbPenyempurnaan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbPenyempurnaan.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPenyempurnaan.Location = new System.Drawing.Point(13, 9);
            this.rbPenyempurnaan.Margin = new System.Windows.Forms.Padding(4);
            this.rbPenyempurnaan.Name = "rbPenyempurnaan";
            this.rbPenyempurnaan.Size = new System.Drawing.Size(169, 20);
            this.rbPenyempurnaan.TabIndex = 5;
            this.rbPenyempurnaan.TabStop = true;
            this.rbPenyempurnaan.Text = "Input Penyempurnaan";
            this.rbPenyempurnaan.UseVisualStyleBackColor = true;
            // 
            // rbRKAPErubahan
            // 
            this.rbRKAPErubahan.AutoSize = true;
            this.rbRKAPErubahan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbRKAPErubahan.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRKAPErubahan.Location = new System.Drawing.Point(13, 8);
            this.rbRKAPErubahan.Margin = new System.Windows.Forms.Padding(4);
            this.rbRKAPErubahan.Name = "rbRKAPErubahan";
            this.rbRKAPErubahan.Size = new System.Drawing.Size(233, 20);
            this.rbRKAPErubahan.TabIndex = 6;
            this.rbRKAPErubahan.TabStop = true;
            this.rbRKAPErubahan.Text = "Input RKA Perubahan Anggaran";
            this.rbRKAPErubahan.UseVisualStyleBackColor = true;
            // 
            // rbDPA
            // 
            this.rbDPA.AutoSize = true;
            this.rbDPA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbDPA.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDPA.Location = new System.Drawing.Point(13, 8);
            this.rbDPA.Margin = new System.Windows.Forms.Padding(4);
            this.rbDPA.Name = "rbDPA";
            this.rbDPA.Size = new System.Drawing.Size(92, 20);
            this.rbDPA.TabIndex = 7;
            this.rbDPA.TabStop = true;
            this.rbDPA.Text = "DPA Murni";
            this.rbDPA.UseVisualStyleBackColor = true;
            this.rbDPA.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // cmdInputPenyempurnaan
            // 
            this.cmdInputPenyempurnaan.BackColor = System.Drawing.Color.Silver;
            this.cmdInputPenyempurnaan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdInputPenyempurnaan.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInputPenyempurnaan.Location = new System.Drawing.Point(244, 3);
            this.cmdInputPenyempurnaan.Margin = new System.Windows.Forms.Padding(4);
            this.cmdInputPenyempurnaan.Name = "cmdInputPenyempurnaan";
            this.cmdInputPenyempurnaan.Size = new System.Drawing.Size(204, 33);
            this.cmdInputPenyempurnaan.TabIndex = 8;
            this.cmdInputPenyempurnaan.Text = "Mulai Input  Penyempurnaan";
            this.cmdInputPenyempurnaan.UseVisualStyleBackColor = false;
            this.cmdInputPenyempurnaan.Click += new System.EventHandler(this.cmdInputPenyempurnaan_Click);
            // 
            // cmdInputRKAP
            // 
            this.cmdInputRKAP.BackColor = System.Drawing.Color.Silver;
            this.cmdInputRKAP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdInputRKAP.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInputRKAP.Location = new System.Drawing.Point(244, 1);
            this.cmdInputRKAP.Margin = new System.Windows.Forms.Padding(4);
            this.cmdInputRKAP.Name = "cmdInputRKAP";
            this.cmdInputRKAP.Size = new System.Drawing.Size(204, 31);
            this.cmdInputRKAP.TabIndex = 9;
            this.cmdInputRKAP.Text = "Input RKA Perubahan";
            this.cmdInputRKAP.UseVisualStyleBackColor = false;
            this.cmdInputRKAP.Click += new System.EventHandler(this.cmdInputRKAP_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(732, 400);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(186, 62);
            this.button2.TabIndex = 10;
            this.button2.Text = "Siapkan Anggaran Kas Perubahan";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmdTetapkanABT
            // 
            this.cmdTetapkanABT.BackColor = System.Drawing.Color.Silver;
            this.cmdTetapkanABT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdTetapkanABT.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTetapkanABT.Location = new System.Drawing.Point(244, 4);
            this.cmdTetapkanABT.Margin = new System.Windows.Forms.Padding(4);
            this.cmdTetapkanABT.Name = "cmdTetapkanABT";
            this.cmdTetapkanABT.Size = new System.Drawing.Size(204, 29);
            this.cmdTetapkanABT.TabIndex = 11;
            this.cmdTetapkanABT.Text = "Tatapkan APBD Perubahan";
            this.cmdTetapkanABT.UseVisualStyleBackColor = false;
            this.cmdTetapkanABT.Click += new System.EventHandler(this.cmdTetapkanABT_Click);
            // 
            // cmdSimpan
            // 
            this.cmdSimpan.Location = new System.Drawing.Point(1143, 141);
            this.cmdSimpan.Margin = new System.Windows.Forms.Padding(4);
            this.cmdSimpan.Name = "cmdSimpan";
            this.cmdSimpan.Size = new System.Drawing.Size(128, 39);
            this.cmdSimpan.TabIndex = 13;
            this.cmdSimpan.Text = "Simpan";
            this.cmdSimpan.UseVisualStyleBackColor = true;
            this.cmdSimpan.Visible = false;
            this.cmdSimpan.Click += new System.EventHandler(this.cmdSimpan_Click);
            // 
            // chkSemuaDinas
            // 
            this.chkSemuaDinas.AutoSize = true;
            this.chkSemuaDinas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSemuaDinas.Location = new System.Drawing.Point(488, 31);
            this.chkSemuaDinas.Margin = new System.Windows.Forms.Padding(4);
            this.chkSemuaDinas.Name = "chkSemuaDinas";
            this.chkSemuaDinas.Size = new System.Drawing.Size(135, 24);
            this.chkSemuaDinas.TabIndex = 15;
            this.chkSemuaDinas.Text = "Semua Dinas";
            this.chkSemuaDinas.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.cmdBukaKunci);
            this.panel1.Controls.Add(this.rbRKA);
            this.panel1.Controls.Add(this.cmdKunciRKA);
            this.panel1.Location = new System.Drawing.Point(492, 152);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(655, 39);
            this.panel1.TabIndex = 17;
            // 
            // cmdBukaKunci
            // 
            this.cmdBukaKunci.Location = new System.Drawing.Point(457, 1);
            this.cmdBukaKunci.Name = "cmdBukaKunci";
            this.cmdBukaKunci.Size = new System.Drawing.Size(220, 36);
            this.cmdBukaKunci.TabIndex = 15;
            this.cmdBukaKunci.Text = "Kambalikan Tahap RKA";
            this.cmdBukaKunci.UseVisualStyleBackColor = true;
            this.cmdBukaKunci.Click += new System.EventHandler(this.cmdBukaKunci_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.rbDPA);
            this.panel2.Controls.Add(this.cmdInputPenyempurnaan);
            this.panel2.Location = new System.Drawing.Point(492, 201);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(655, 39);
            this.panel2.TabIndex = 18;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(457, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(220, 36);
            this.button3.TabIndex = 16;
            this.button3.Text = "Kambalikan Tahap DPA";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // cmdKucniInput
            // 
            this.cmdKucniInput.BackColor = System.Drawing.Color.OrangeRed;
            this.cmdKucniInput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdKucniInput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdKucniInput.Image = global::KUAPPAS.Properties.Resources.flag_orange;
            this.cmdKucniInput.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdKucniInput.Location = new System.Drawing.Point(488, 97);
            this.cmdKucniInput.Margin = new System.Windows.Forms.Padding(4);
            this.cmdKucniInput.Name = "cmdKucniInput";
            this.cmdKucniInput.Size = new System.Drawing.Size(202, 33);
            this.cmdKucniInput.TabIndex = 14;
            this.cmdKucniInput.Text = "Kunci Input RKA/RKAP";
            this.cmdKucniInput.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdKucniInput.UseVisualStyleBackColor = false;
            this.cmdKucniInput.Click += new System.EventHandler(this.cmdKucniInput_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Lime;
            this.panel3.Controls.Add(this.button4);
            this.panel3.Controls.Add(this.rbPenyempurnaan);
            this.panel3.Controls.Add(this.cmdInputRKAP);
            this.panel3.Location = new System.Drawing.Point(492, 250);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(655, 39);
            this.panel3.TabIndex = 19;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(457, 1);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(220, 36);
            this.button4.TabIndex = 16;
            this.button4.Text = "Kambalikan Tahap Pergeseran";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.panel4.Controls.Add(this.button5);
            this.panel4.Controls.Add(this.rbRKAPErubahan);
            this.panel4.Controls.Add(this.cmdTetapkanABT);
            this.panel4.Location = new System.Drawing.Point(492, 299);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(655, 39);
            this.panel4.TabIndex = 20;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(455, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(220, 34);
            this.button5.TabIndex = 16;
            this.button5.Text = "Kambalikan Tahap RKAP";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 489);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1142, 22);
            this.statusStrip1.TabIndex = 21;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // btnMulaiTahunANggaranBaru
            // 
            this.btnMulaiTahunANggaranBaru.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnMulaiTahunANggaranBaru.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMulaiTahunANggaranBaru.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMulaiTahunANggaranBaru.Location = new System.Drawing.Point(12, 12);
            this.btnMulaiTahunANggaranBaru.Name = "btnMulaiTahunANggaranBaru";
            this.btnMulaiTahunANggaranBaru.Size = new System.Drawing.Size(249, 36);
            this.btnMulaiTahunANggaranBaru.TabIndex = 22;
            this.btnMulaiTahunANggaranBaru.Text = "Mulai Tahun Anggaran Baru";
            this.btnMulaiTahunANggaranBaru.UseVisualStyleBackColor = false;
            this.btnMulaiTahunANggaranBaru.Click += new System.EventHandler(this.btnMulaiTahunANggaranBaru_Click);
            // 
            // gridTahap
            // 
            this.gridTahap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridTahap.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridTahap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridTahap.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MediumPurple;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTahap.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridTahap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTahap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Dinas,
            this.Tahap,
            this.Kunci,
            this.IDDInas});
            this.gridTahap.Location = new System.Drawing.Point(0, 54);
            this.gridTahap.Name = "gridTahap";
            this.gridTahap.Size = new System.Drawing.Size(470, 426);
            this.gridTahap.TabIndex = 23;
            // 
            // Dinas
            // 
            this.Dinas.HeaderText = "Dinas";
            this.Dinas.Name = "Dinas";
            this.Dinas.ReadOnly = true;
            this.Dinas.Width = 200;
            // 
            // Tahap
            // 
            this.Tahap.HeaderText = "Tahap";
            this.Tahap.Name = "Tahap";
            this.Tahap.ReadOnly = true;
            // 
            // Kunci
            // 
            this.Kunci.HeaderText = "Kunci Input";
            this.Kunci.Name = "Kunci";
            this.Kunci.ReadOnly = true;
            this.Kunci.Width = 200;
            // 
            // IDDInas
            // 
            this.IDDInas.HeaderText = "IDdinas";
            this.IDDInas.Name = "IDDInas";
            this.IDDInas.Visible = false;
            // 
            // cmdBukaInput
            // 
            this.cmdBukaInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmdBukaInput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdBukaInput.Location = new System.Drawing.Point(697, 96);
            this.cmdBukaInput.Name = "cmdBukaInput";
            this.cmdBukaInput.Size = new System.Drawing.Size(160, 34);
            this.cmdBukaInput.TabIndex = 24;
            this.cmdBukaInput.Text = "Buka Input";
            this.cmdBukaInput.UseVisualStyleBackColor = false;
            this.cmdBukaInput.Visible = false;
            this.cmdBukaInput.Click += new System.EventHandler(this.cmdBukaInput_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(505, 400);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(210, 62);
            this.button6.TabIndex = 25;
            this.button6.Text = "siapkan ANggaran Kas Pergeseran";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.panel5.Controls.Add(this.button7);
            this.panel5.Controls.Add(this.rbRKAPergeseranPerubahan);
            this.panel5.Controls.Add(this.button8);
            this.panel5.Location = new System.Drawing.Point(492, 344);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(655, 39);
            this.panel5.TabIndex = 26;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(455, 2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(220, 34);
            this.button7.TabIndex = 16;
            this.button7.Text = "Kambalikan Tahap RKAP";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Visible = false;
            // 
            // rbRKAPergeseranPerubahan
            // 
            this.rbRKAPergeseranPerubahan.AutoSize = true;
            this.rbRKAPergeseranPerubahan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbRKAPergeseranPerubahan.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRKAPergeseranPerubahan.Location = new System.Drawing.Point(13, 5);
            this.rbRKAPergeseranPerubahan.Margin = new System.Windows.Forms.Padding(4);
            this.rbRKAPergeseranPerubahan.Name = "rbRKAPergeseranPerubahan";
            this.rbRKAPergeseranPerubahan.Size = new System.Drawing.Size(233, 20);
            this.rbRKAPergeseranPerubahan.TabIndex = 6;
            this.rbRKAPergeseranPerubahan.TabStop = true;
            this.rbRKAPergeseranPerubahan.Text = "Input RKA Perubahan Anggaran";
            this.rbRKAPergeseranPerubahan.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.Silver;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.Location = new System.Drawing.Point(244, -4);
            this.button8.Margin = new System.Windows.Forms.Padding(4);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(204, 43);
            this.button8.TabIndex = 11;
            this.button8.Text = "Input APBD Pnyempurnaan Perubahan";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "IDDInas";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Nama Dinas";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Tahap";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "IDdinas";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // ctrlSKPD1
            // 
            this.ctrlSKPD1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlSKPD1.Location = new System.Drawing.Point(488, 63);
            this.ctrlSKPD1.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlSKPD1.Name = "ctrlSKPD1";
            this.ctrlSKPD1.Size = new System.Drawing.Size(560, 26);
            this.ctrlSKPD1.TabIndex = 16;
            this.ctrlSKPD1.OnChanged += new KUAPPAS.ctrlSKPD.ValueChangedEventHandler(this.ctrlSKPD1_OnChanged);
            this.ctrlSKPD1.Load += new System.EventHandler(this.ctrlSKPD1_Load);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(926, 400);
            this.button9.Margin = new System.Windows.Forms.Padding(4);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(186, 62);
            this.button9.TabIndex = 27;
            this.button9.Text = "Siapkan Anggaran Kas Penyempurnaan Perubahan";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // frmSetStatusInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1142, 511);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.cmdBukaInput);
            this.Controls.Add(this.gridTahap);
            this.Controls.Add(this.btnMulaiTahunANggaranBaru);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cmdKucniInput);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ctrlSKPD1);
            this.Controls.Add(this.chkSemuaDinas);
            this.Controls.Add(this.cmdSimpan);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmSetStatusInput";
            this.Text = "Set Status Input";
            this.Load += new System.EventHandler(this.frmSetStatusInput_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTahap)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdKunciRKA;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton rbRKA;
        private System.Windows.Forms.RadioButton rbPenyempurnaan;
        private System.Windows.Forms.RadioButton rbRKAPErubahan;
        private System.Windows.Forms.RadioButton rbDPA;
        private System.Windows.Forms.Button cmdInputPenyempurnaan;
        private System.Windows.Forms.Button cmdInputRKAP;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button cmdTetapkanABT;
        private System.Windows.Forms.Button cmdSimpan;
        private System.Windows.Forms.Button cmdKucniInput;
        private System.Windows.Forms.CheckBox chkSemuaDinas;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private ctrlSKPD ctrlSKPD1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnMulaiTahunANggaranBaru;
        private System.Windows.Forms.Button cmdBukaKunci;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGridView gridTahap;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dinas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tahap;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kunci;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDDInas;
        private System.Windows.Forms.Button cmdBukaInput;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.RadioButton rbRKAPergeseranPerubahan;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
    }
}