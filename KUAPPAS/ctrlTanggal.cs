using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Formatting;

namespace KUAPPAS
{
    public partial class ctrlTanggal : UserControl
    {
        public delegate void ValueChangedEventHandler(DateTime pTanggal);
        public event ValueChangedEventHandler OnChanged;
        

        public ctrlTanggal()
        {
            InitializeComponent();
        }

        private void ctrlTanggal_Load(object sender, EventArgs e)
        {

        }
      

        public DateTime Tanggal
        {
            set
            {
                dtp.Value = value;
            }
            get
            {
                return dtp.Value.Date;
            }
        }

        private void dtp_ValueChanged(object sender, EventArgs e)
        {
            if (OnChanged != null)
                OnChanged(Tanggal);

        }
        public string TextTanggalLengkap
        {
            get
            {
                return dtp.Value.ToTanggalIndonesiaLengkap();
            }
        }

    }
}
