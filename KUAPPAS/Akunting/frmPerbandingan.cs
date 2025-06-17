using BP.Akuntansi;
using BP.Bendahara;
using DTO.Akuntansi;
using DTO.Bendahara;
using Formatting;
using KUAPPAS.Bendahara;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace KUAPPAS.Akunting
{
    public partial class frmPerbandingan : Form
    {
        public frmPerbandingan()
        {
            InitializeComponent();
        }

        private void frmPerbandingan_Load(object sender, EventArgs e)
        {
            ctrlSKPD1.Create();
            if(GlobalVar.Pengguna.SKPD > 0){
                ctrlSKPD1.SetID(GlobalVar.Pengguna.SKPD);
            }
            DateTime hariini = DateTime.Now.Date;
            DateTime akhirTahun = new DateTime(GlobalVar.TahunAnggaran, 12, 31);
            DateTime avaltahun= new DateTime(GlobalVar.TahunAnggaran,1, 1 );
            ctrlPeriode1.TanggalAwaal = avaltahun;
            ctrlPeriode1.TanggalAkhir = hariini > akhirTahun ? akhirTahun : hariini;



        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                //panggil BKU penentu kas
                List<BKU> lstBKU = new List<BKU>();
                List<BukuBesar> lstBB = new List<BukuBesar>();
                int dinas = ctrlSKPD1.GetID();
                DateTime tanggal = ctrlPeriode1.TanggalAkhir;
                BKULogic obkulogic = new BKULogic(GlobalVar.TahunAnggaran);
                lstBKU = obkulogic.GetByDinas (dinas, tanggal);
                gridBKUBB.Rows.Clear();


                int col = 5;

                decimal debet = 0;
                decimal kredit = 0;
                decimal saldo = 0;
                foreach (BKU b in lstBKU)
                {
                    if (b.JenisBelanja < 3 && b.JenisSumber != 9 && b.JenisSumber != 17)
                    {

                        if (b.Debet == 1)
                        {
                            debet = b.Jumlah;
                            kredit = 0;
                        }
                        else
                        {
                            debet = 0;
                            kredit = b.Jumlah; ;
                        }
                        saldo = saldo + (b.Debet * b.Jumlah);
                        string[] item = {b.NourutSumber.ToString(),
                        b.NoBukti, b.TanggalTransaksi.ToTanggalIndonesia(),
                        debet.ToRupiahInReport(),
                        kredit.ToRupiahInReport(),
                        saldo.ToRupiahInReport()};
                        gridBKUBB.Rows.Add(item);
                    }

                }



            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cmdPanggilBukubesar_Click(object sender, EventArgs e)
        {
            //panggil BKU penentu kas
            List<BKU> lstBKU = new List<BKU>();
            List<BukuBesar> lstBB = new List<BukuBesar>();
            int dinas = ctrlSKPD1.GetID();
            DateTime tanggal = ctrlPeriode1.TanggalAkhir;
          
            // bukubesar 
            List<BukuBesar> lstBukuBesar;


            lstBukuBesar = new List<BukuBesar>();
            BukuBesarLogic oLogic = new BukuBesarLogic(GlobalVar.TahunAnggaran);

            lstBukuBesar = oLogic.GetBukuBesar(dinas, tanggal, 110103010001);


            if (lstBukuBesar == null)
            {
                MessageBox.Show(oLogic.LastError());
                return;
            }

            int row = 0;

            decimal saldo = 0l;

            foreach (BukuBesar v in lstBukuBesar)
            {


                decimal debet;
                decimal kredit;
                debet = v.Debet == 1 ? v.Jumlah : 0;
                kredit = v.Debet == -1 ? v.Jumlah : 0;
                saldo = saldo + debet - kredit;

                if (row < gridBKUBB.Rows.Count)
                {
                    gridBKUBB.Rows[row].Cells[6].Value = v.NoSumber.ToString();
                    gridBKUBB.Rows[row].Cells[7].Value = v.NoBukti;
                    gridBKUBB.Rows[row].Cells[8].Value = v.TanggalTransaksi.ToTanggalIndonesia();
                    gridBKUBB.Rows[row].Cells[9].Value = debet.ToRupiahInReport();
                    gridBKUBB.Rows[row].Cells[10].Value = kredit.ToRupiahInReport();
                    gridBKUBB.Rows[row].Cells[11].Value = saldo.ToRupiahInReport();
                    row++;
                }
                else
                {

                    string[] rowx = {
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        v.NoSumber.ToString(),
                        v.NoBukti,
                        v.TanggalTransaksi.ToTanggalIndonesia(),
                        debet.ToRupiahInReport(),
                        kredit.ToRupiahInReport(),
                        saldo.ToRupiahInReport()
                    };
                    gridBKUBB.Rows.Add(rowx);
                }
            }
        }
    }
}
