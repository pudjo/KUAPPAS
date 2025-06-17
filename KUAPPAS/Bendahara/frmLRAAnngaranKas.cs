using BP;
using BP.Bendahara;
using DTO;
using DTO.Laporan;
using Formatting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUAPPAS.Bendahara
{
    public partial class frmLRAAnngaranKas : Form
    {
        private DateTime mTanggal;
        private int m_IDSKPD;
        private List<Rekening> m_lstRekening;
        private List<AnggaranKas> m_lstAnggaranKas;
        public frmLRAAnngaranKas()
        {
            InitializeComponent();
        }

        
        private void cmdPanggil_Click(object sender, EventArgs e)
        {
            try
            {
                   mTanggal= ctrlTanggal1.Tanggal;
                   m_IDSKPD = ctrlDinas1.GetID();


           
                gridAnggaranKas.Rows.Clear();



                LoadData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }
        private void LoadData()
        {//Panggil data
            try
            {
                //Panggil data prog..sub
                if (LoadRekening())
                {

                    DisplayAnggaranKas();
                    //  FormatGrid();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private bool LoadRekening()
        {
            try
            {
                m_lstRekening = new List<Rekening>();
                RekeningLogic oRekeningLogic = new RekeningLogic(GlobalVar.TahunAnggaran);

                m_lstRekening = oRekeningLogic.GetOnLevel(3);
                if (m_lstRekening != null)
                {
                    int i=0;
                    foreach (Rekening rek in m_lstRekening)
                    {
                        string[] row = { (++i).ToString(), rek.ID.ToString(), rek.Nama };
                        gridAnggaranKas.Rows.Add(rek);

                    }

                }



                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }

        }
        private bool DisplayAnggaranKas()
        {
            //return true;
            try
            {

                m_lstAnggaranKas = new List<AnggaranKas>();

                  AnggaranKasLogic oLogic = new AnggaranKasLogic(GlobalVar.TahunAnggaran);
                m_lstAnggaranKas= oLogic.GetOnPeriode(m_IDSKPD, 3);
                decimal akinperiode = 0;
                decimal akitahun = 0;
                
                int periode= 9;
                if (m_lstAnggaranKas != null)
                {
                    foreach (AnggaranKas ak in m_lstAnggaranKas)
                    {
                        akitahun= ak.Bulan1+ak.Bulan2+ak.Bulan3 + ak.Bulan4+ak.Bulan5+ak.Bulan6+
                            ak.Bulan6+ak.Bulan7+ak.Bulan8 +ak.Bulan9 + ak.Bulan10+ak.Bulan11+ak.Bulan12;
                        for (int r = 0; r <= gridAnggaranKas.Rows.Count; r++)
                        {
                            if (DataFormat.GetInteger(gridAnggaranKas.Rows[r].Cells[1].Value) == ak.IDRekening)
                            {
                                akinperiode=ak.Bulan1;
                                if (periode > 1)
                                {
                                    akinperiode = akinperiode + ak.Bulan1;
                                    if (periode > 1)
                                    {
                                        akinperiode = akinperiode + ak.Bulan2;
                                        if (periode > 2)
                                        {
                                            akinperiode = akinperiode + ak.Bulan3;
                                            if (periode > 3)
                                            {
                                                akinperiode = akinperiode + ak.Bulan4;
                                                if (periode > 4)
                                                {
                                                    akinperiode = akinperiode + ak.Bulan5;
                                                    if (periode > 5)
                                                    {
                                                        akinperiode = akinperiode + ak.Bulan6;
                                                        if (periode > 6)
                                                        {
                                                            akinperiode = akinperiode + ak.Bulan7;
                                                            if (periode > 7)
                                                            {
                                                                akinperiode = akinperiode + ak.Bulan8;
                                                                if (periode > 8)
                                                                {
                                                                    akinperiode = akinperiode + ak.Bulan9;
                                                                    if (periode > 9)
                                                                    {
                                                                        akinperiode = akinperiode + ak.Bulan10;
                                                                        if (periode > 10)
                                                                        {
                                                                            akinperiode = akinperiode + ak.Bulan11;
                                                                            if (periode > 11)
                                                                            {
                                                                                akinperiode = akinperiode + ak.Bulan12;

                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    
                                }
                                gridAnggaranKas.Rows[r].Cells[3].Value = ak.Anggaran.ToRupiahInReport();
                                gridAnggaranKas.Rows[r].Cells[3].Value = akinperiode.ToRupiahInReport();
                            }
                        }

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return false;
            }

        }

        private void frmLRAAnngaranKas_Load(object sender, EventArgs e)
        {
            ctrlDinas1.Create();
        }

    }
}
