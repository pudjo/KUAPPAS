namespace KUAPPAS.Akunting
{
    partial class frmRealisasiPerUrusan
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.cmdGenerateData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(152, 128);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(182, 20);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // cmdGenerateData
            // 
            this.cmdGenerateData.Location = new System.Drawing.Point(158, 165);
            this.cmdGenerateData.Name = "cmdGenerateData";
            this.cmdGenerateData.Size = new System.Drawing.Size(83, 32);
            this.cmdGenerateData.TabIndex = 1;
            this.cmdGenerateData.Text = "Excell";
            this.cmdGenerateData.UseVisualStyleBackColor = true;
            this.cmdGenerateData.Click += new System.EventHandler(this.cmdGenerateData_Click);
            // 
            // frmRealisasiPerUrusan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 454);
            this.Controls.Add(this.cmdGenerateData);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "frmRealisasiPerUrusan";
            this.Text = "frmRealisasiPerUrusan";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button cmdGenerateData;
    }
}