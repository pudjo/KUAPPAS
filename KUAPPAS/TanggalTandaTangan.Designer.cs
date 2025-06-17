namespace KUAPPAS
{
    partial class TanggalTandaTangan
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
            this.tanggal = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // tanggal
            // 
            this.tanggal.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tanggal.CustomFormat = "dd MMM yyyy";
            this.tanggal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tanggal.Location = new System.Drawing.Point(3, 0);
            this.tanggal.Name = "tanggal";
            this.tanggal.Size = new System.Drawing.Size(129, 20);
            this.tanggal.TabIndex = 0;
            this.tanggal.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // TanggalTandaTangan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tanggal);
            this.Name = "TanggalTandaTangan";
            this.Size = new System.Drawing.Size(155, 24);
            this.Load += new System.EventHandler(this.TanggalTandaTangan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker tanggal;
    }
}
