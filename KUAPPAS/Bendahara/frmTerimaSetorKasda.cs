using BP.Bendahara;
using DTO;
using DTO.Bendahara;
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
    public partial class frmTerimaSetorKasda : ChildForm
    {
        public frmTerimaSetorKasda()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void frmTerimaSetorKasda_Load(object sender, EventArgs e)
        {
            ctrlSKPD1.Create();
            ctrlTanggalBulan1.Create();
            ctrlHeader1.SetCaption("Penerimaan Penyetoran Brndahara Penerimaan", "");
            gridSetor.FormatHeader();
            gridRekening.FormatHeader();
            DateTime hariini = DateTime.Now.Date;
            ctrlTanggalBulan1.TanggalAwal = new DateTime(GlobalVar.TahunAnggaran, hariini.Month, 1);
            ctrlTanggalBulan1.TanggalAkhir = new DateTime(GlobalVar.TahunAnggaran, hariini.Month, hariini.Day);

        }

        private void gridRekening_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        private void cmdBaru_Click(object sender, EventArgs e)
        {
            try
            {
                LoadSTS();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool LoadSTS()
        {
            try
            {
                
                gridSetor.Rows.Clear();
                STSLogic oLogic = new STSLogic(GlobalVar.TahunAnggaran);
                int iddinas = ctrlSKPD1.GetID();
                DateTime tanggalAwal= ctrlTanggalBulan1.TanggalAwal;
                DateTime tanggalAkhir = ctrlTanggalBulan1.TanggalAkhir;
                List<STS> lst = new List<STS>();

                lst =  oLogic.GetPenerimaanDiKasda(iddinas, tanggalAwal, tanggalAkhir);
                if (lst != null)
                {
                    foreach (STS sts in lst)
                    {
                        string[] row = {sts.NoUrut.ToString(), 
                                          false.ToString(),
                                          sts.NoUrutKasda.ToString(),
                                          sts.NamaSKPD,
                                           sts.dtBukuKas.ToTanggalIndonesia(),
                                            sts.NoBukti,
                                            sts.Jumlah.ToRupiahInReport(),
                                            sts.Keterangan, 
                                            sts.Jenis.ToString()
                                            
                                      };
                        gridSetor.Rows.Add(row);
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void gridSetor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 1 && e.ColumnIndex  < gridSetor.ColumnCount && e.RowIndex >= 0 && e.RowIndex < gridSetor.Rows.Count)
            {
                long inourut = DataFormat.GetLong(gridSetor.Rows[e.RowIndex].Cells[0].Value);
               
                STSLogic oLogic = new STSLogic(GlobalVar.TahunAnggaran);
                List<STSRekening> lst = new List<STSRekening>();
                gridRekening.Rows.Clear();
                lst = oLogic.GetDetail(inourut);
                if (lst != null)
                {
                    foreach (STSRekening sr in lst)
                    {
                        string[] row = {sr.IDRekening.ToKodeRekening(6),
                                      sr.Nama,

                                      sr.Jumlah.ToRupiahInReport()};
                        gridRekening.Rows.Add(row);
                    }
                }

                SetorLogic oSetorLogic = new SetorLogic(GlobalVar.TahunAnggaran);
                List<SetorRekening> lst2 = new List<SetorRekening>();
                lst2 = oSetorLogic.GetDetail(inourut);
                if (lst2 != null)
                {
                    foreach (SetorRekening sr in lst2)
                    {
                        string[] row = {sr.IDRekening.ToKodeRekening(6),
                                      sr.NamaRekening ,

                                      sr.Jumlah.ToRupiahInReport()};
                        gridRekening.Rows.Add(row);
                    }
                }
            }
        }

        private void cmdSImpan_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (DataGridViewRow row in gridSetor.Rows)
                {
                    bool dipilih = Convert.ToBoolean(row.Cells[1].Value);
                    // yang dipilih saja

                   
                    if (dipilih)
                    {
                        int jenis = DataFormat.GetInteger(row.Cells[8].Value);
                        long noUrut = DataFormat.GetLong(row.Cells[0].Value);
                        int noUrutKasda = DataFormat.GetInteger(row.Cells[2].Value);

                        if (jenis==2)
                        {
                           
                                STSLogic oLogic = new STSLogic(GlobalVar.TahunAnggaran);
                                oLogic.TerimaKasda(noUrut, noUrutKasda);
                        } else{
                        
                                SetorLogic oLogicsetor1 = new SetorLogic(GlobalVar.TahunAnggaran);
                                oLogicsetor1.TerimaKasda(noUrut, noUrutKasda);                         
                        
                        }
                    }

                }
                


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
