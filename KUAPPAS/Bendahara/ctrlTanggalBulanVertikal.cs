using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Formatting;

namespace KUAPPAS.Bendahara
{
    public partial class ctrlTanggalBulanVertikal : UserControl
    {
        private DateTime mTanggalAwal;
        private DateTime mTanggalAkhir;
        private string KeteranganWaktu;
        public ctrlTanggalBulanVertikal()
        {
            InitializeComponent();
            KeteranganWaktu = "";

        }
        public bool CekPilihan()
        {
            if (rbTanggal.Checked == true)
            {
                mTanggalAwal = ctrlPeriode1.TanggalAwaal;
                mTanggalAkhir = ctrlPeriode1.TanggalAkhir;
                if (mTanggalAwal > mTanggalAkhir)
                {
                    MessageBox.Show("Tanggal Awal Lebih Akhir dar Tanggal Akhir ");
                    return false;

                }
                return true;
                
            }
            else
            {
                if (ctrlBulan1.GetID() == 0)
                {
                    MessageBox.Show("Bulan belum dipilih..");
                    return false;

                    
                }
                return true;
            }

        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public int Bulan
        {
            set
            {
                ctrlBulan1.SetID(value);
            }
            get
            {
                return ctrlBulan1.GetID();
            }
        }
        public int JenisPeriode
        {
            set
            {
                if (value == 1)
                {
                    rbTanggal.Checked = true;
                }
                else
                {
                    rbBulan.Checked = true;
                }
            }
            get
            {

                if (rbTanggal.Checked == true)
                {
                    return 1;
                }
                else return 2;
            }
        }
        public DateTime TanggalAwal
        {
            get
            {
                GetTanggal();
                return mTanggalAwal;
            }
            set
            {
                mTanggalAwal = value;
                ctrlBulan1.SetID(value.Month);
                ctrlPeriode1.TanggalAwaal = value;

            }
        }
        public DateTime TanggalAkhir
        {
            get
            {
                GetTanggal();
                return mTanggalAkhir;
            }
            set{
                mTanggalAkhir = value;
                ctrlBulan1.SetID(value.Month);
                ctrlPeriode1.TanggalAkhir = value; 

            }
            
        }
        public string Waktu
        {
            get
            {
                GetTanggal();
                return KeteranganWaktu;
            }
        }
     
        private void GetTanggal()
        {
            if (rbTanggal.Checked == true)
            {
                mTanggalAwal = ctrlPeriode1.TanggalAwaal;
                mTanggalAkhir = ctrlPeriode1.TanggalAkhir;
                KeteranganWaktu = mTanggalAwal.ToTanggalIndonesia() + " s/d " + mTanggalAkhir.ToTanggalIndonesia();

            }
            else
            {
                if (ctrlBulan1.GetID() == 0)
                {
                    MessageBox.Show("Bulan belum dipilih..");
                    mTanggalAwal = new DateTime (GlobalVar.TahunAnggaran,1,1);
                    mTanggalAkhir = new DateTime(GlobalVar.TahunAnggaran,12 , 31);
                    return;
                }
                mTanggalAwal = ctrlBulan1.TanggalAwal;
                mTanggalAkhir = ctrlBulan1.TanggalAkhir;
                KeteranganWaktu = ctrlBulan1.GetNama(); ;

            }
        }
        public void Create()
        {
            try
            {
                ctrlBulan1.Create();
                DateTime tanggalSekarang = DateTime.Now.Date;
                ctrlPeriode1.TanggalAwaal = new DateTime(GlobalVar.TahunAnggaran, tanggalSekarang.Month, 1);
                ctrlPeriode1.TanggalAkhir = tanggalSekarang;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void ctrlTanggalBulanVertikal_Load(object sender, EventArgs e)
        {
        }
        //public string Waktu
        //{
        //    get
        //    {
        //        GetTanggal();
        //        return KeteranganWaktu;
        //    }
        //}
    }
}
