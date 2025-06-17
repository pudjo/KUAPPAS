using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KUAPPAS.Bendahara
{
    public partial class ctrlPeriode : UserControl
    {
        public ctrlPeriode()
        {
            InitializeComponent();
        }

        private void ctrlPeriode_Load(object sender, EventArgs e)
        {
            //int year = DateTime.Now.Year;
            //DateTime firstDay = new DateTime(year, 1, 1);
            //
            try
            {
                //dtAwal.Tanggal = new DateTime(GlobalVar.TahunAnggaran, 1, 1);

                //dtAkhir.Tanggal = DateTime.Now.Date;
                //if (DateTime.Now.Date.Year > GlobalVar.TahunAnggaran)
                //    dtAwal.Tanggal = new DateTime(GlobalVar.TahunAnggaran, 12, 31);

            } catch(Exception ex)
            {

            }
        }
        public DateTime GetDateAwal()
        {
            return dtAwal.Tanggal;
        }
        public DateTime GetDateAkhir()
        {
            return dtAkhir.Tanggal;

        }
        public DateTime TanggalAwaal
        {
            get { return dtAwal.Tanggal; }
            set { if (value > DateTime.MinValue) 
                dtAwal.Tanggal = value; 
            }
        }
        public DateTime TanggalAkhir
        {
            set
            {
                if (value > DateTime.MinValue)
                {
                    dtAkhir.Tanggal = value;
                }

            }
            get { return dtAkhir.Tanggal; }

        }

        private void dtAkhir_Load(object sender, EventArgs e)
        {

        }
    }
}
