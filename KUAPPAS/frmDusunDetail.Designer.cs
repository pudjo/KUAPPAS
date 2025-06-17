namespace KUAPPAS
{
    partial class frmDusunDetail
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
            this.label3 = new System.Windows.Forms.Label();
            this.ctrlKecamatan1 = new KUAPPAS.ctrlKecamatan();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKode = new System.Windows.Forms.TextBox();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.ctrlNavigation1 = new KUAPPAS.ctrlNavigation();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ctrlDesa1 = new KUAPPAS.ctrlDesa();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(28, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 14);
            this.label3.TabIndex = 20;
            this.label3.Text = "Kecamatan";
            // 
            // ctrlKecamatan1
            // 
            this.ctrlKecamatan1.Location = new System.Drawing.Point(126, 119);
            this.ctrlKecamatan1.Name = "ctrlKecamatan1";
            this.ctrlKecamatan1.Size = new System.Drawing.Size(356, 22);
            this.ctrlKecamatan1.TabIndex = 19;
            this.ctrlKecamatan1.OnChanged += new KUAPPAS.ctrlKecamatan.ValueChangedEventHandler(this.ctrlKecamatan1_OnChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(38, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 14);
            this.label2.TabIndex = 18;
            this.label2.Text = "Nama";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 186);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 17;
            this.label1.Text = "Kode";
            // 
            // txtKode
            // 
            this.txtKode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKode.Location = new System.Drawing.Point(126, 180);
            this.txtKode.Name = "txtKode";
            this.txtKode.Size = new System.Drawing.Size(354, 22);
            this.txtKode.TabIndex = 16;
            // 
            // txtNama
            // 
            this.txtNama.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNama.Location = new System.Drawing.Point(126, 206);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(354, 22);
            this.txtNama.TabIndex = 15;
            // 
            // ctrlNavigation1
            // 
            this.ctrlNavigation1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlNavigation1.Location = new System.Drawing.Point(0, 0);
            this.ctrlNavigation1.Name = "ctrlNavigation1";
            this.ctrlNavigation1.Size = new System.Drawing.Size(567, 48);
            this.ctrlNavigation1.TabIndex = 14;
            this.ctrlNavigation1.OnAdd += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnAdd);
            this.ctrlNavigation1.OnSave += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnSave);
            this.ctrlNavigation1.OnDelete += new KUAPPAS.ctrlNavigation.MyDelegate(this.ctrlNavigation1_OnDelete);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 372);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(567, 22);
            this.statusStrip1.TabIndex = 21;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ctrlDesa1
            // 
            this.ctrlDesa1.Location = new System.Drawing.Point(128, 149);
            this.ctrlDesa1.Name = "ctrlDesa1";
            this.ctrlDesa1.Size = new System.Drawing.Size(351, 22);
            this.ctrlDesa1.TabIndex = 22;
            this.ctrlDesa1.Load += new System.EventHandler(this.ctrlDesa1_Load);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label4.Location = new System.Drawing.Point(36, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 14);
            this.label4.TabIndex = 23;
            this.label4.Text = "Desa";
            // 
            // frmDusunDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 394);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ctrlDesa1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ctrlKecamatan1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtKode);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.ctrlNavigation1);
            this.Name = "frmDusunDetail";
            this.Text = "Detail Dusun";
            this.Load += new System.EventHandler(this.frmDusunDetail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private ctrlKecamatan ctrlKecamatan1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKode;
        private System.Windows.Forms.TextBox txtNama;
        private ctrlNavigation ctrlNavigation1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private ctrlDesa ctrlDesa1;
        private System.Windows.Forms.Label label4;
    }
}