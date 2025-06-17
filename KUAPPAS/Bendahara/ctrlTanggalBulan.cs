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
    public partial class ctrlTanggalBulan : UserControl
    {
        private DateTime mTanggalAwal;
        private DateTime mTanggalAkhir;
        private string KeteranganWaktu;
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
        public void Create()
        {
            try
            {
                ctrlBulan1.Create();
                DateTime tanggalSekarang = DateTime.Now.Date;
                //ctrlPeriode1.TanggalAwaal = new DateTime(GlobalVar.TahunAnggaran, tanggalSekarang.Month, 1);
                //ctrlPeriode1.TanggalAkhir = tanggalSekarang;
                ctrlPeriode1.TanggalAwaal = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
                if (DateTime.Now.Year > GlobalVar.TahunAnggaran)
                {
                    ctrlPeriode1.TanggalAkhir = new DateTime(GlobalVar.TahunAnggaran, 12, 31);

                } else
                {
                    ctrlPeriode1.TanggalAkhir = tanggalSekarang;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public ctrlTanggalBulan()
        {
            InitializeComponent();
            KeteranganWaktu = "";
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
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        public DateTime TanggalAwal
        {
            get {
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
            get {
                GetTanggal();
                return mTanggalAkhir; }
            set
            {
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
        private void ctrlTanggalBulan_Load(object sender, EventArgs e)
        {
        
            
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
                mTanggalAwal = ctrlBulan1.TanggalAwal;
                mTanggalAkhir = ctrlBulan1.TanggalAkhir;
                KeteranganWaktu = ctrlBulan1.GetNama(); ;

            }
        }

        private void ctrlPeriode1_Load(object sender, EventArgs e)
        {

        }

        private void rbTanggal_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tvBulan_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ctrlBulan1_Load(object sender, EventArgs e)
        {

        }

    }
}
