using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP.Akuntansi;
using Formatting;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using DTO;
using System.IO;
using BP;
using BP.Anggaran;
using DTO;
using DTO.Akuntansi;
using DTO.Anggaran;
using DTO.Laporan;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

namespace KUAPPAS.Akunting
{
    public partial class frmLO : ChildForm 
    {
       
        private List<Realisasi04AK> m_lstLO;

        DateTime mTanggalAkhir;
        DateTime mTanggalAwal;
        string FolderPath;
        private List<Rekening> m_lstRekening;
        private List<Realisasi04AK> m_lstNeracaAwal;

        private int m_iSKPD;
        private int m_iKodeUK;



        private const int COL_KODEREKENING = 0;
        private const int COL_NAMAREKENING = 1;
        private const int COL_LOKINI = 2;
        private const int COL_LOLALU = 3;
        private const int COL_SELISIH = 4;
        private const int COL_LEVEL = 5;
        private const int COL_IDREKENING = 6;
      
       

        private const string CON_STRING_JUMLAHPENDAPATAN = "JP";
        private const string CON_STRING_JUMLAHBELANJA = "JB";
        private const string CON_STRING_SURPLUSDEFISIT = "SD";



   

        decimal JumlahNABeban = 0l;
        decimal JumlahNAPendapatan = 0L;
        int Tahun ;

        CetakPDF oCetakPDF;
        PdfPage previousPage;
        int m_iJenisBendahara;
        bool SaatnyacetakKesimpulan;
        float PosisiTerakhir;
        public frmLO()
        {
            InitializeComponent();
            Tahun = GlobalVar.TahapAnggaran;
            FolderPath = "D\\";
        }

        private void frmLO_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Laporan Operasional");
            ctrlDinas1.Create();
            ListItemData li = new ListItemData("Akun Utama", 1);
            cmbLevelRekening.Items.Add(li);
            if (GlobalVar.TahunAnggaran == 2023)
            {
                cmdJadikanSaldoAwal.Visible = true;
            }
            else {
                cmdJadikanSaldoAwal.Visible = false;
            }
            gridLRATrx.FormatHeader();
            li = new ListItemData("Akun Kelompok", 2);
            cmbLevelRekening.Items.Add(li);
            li = new ListItemData("Akun Jenis", 3);
            cmbLevelRekening.Items.Add(li);
            li = new ListItemData("Akun Objek", 4);
            cmbLevelRekening.Items.Add(li);
            li = new ListItemData("Akun Rincian Objek", 5);
            cmbLevelRekening.Items.Add(li);
            li = new ListItemData("Akun Sub Rincian Objek", 6);
            cmbLevelRekening.Items.Add(li);



            gridLRATrx.FormatHeader();
            ctrlTanggalBulanVertikal1.TanggalAwal = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
            ctrlTanggalBulanVertikal1.TanggalAkhir = new DateTime(GlobalVar.TahunAnggaran, 12, 31);

          // ctrlTanggalBulanVertikal1.TanggalAkhir = new DateTime(GlobalVar.TahunAnggaran,DateTime.Now.Month, DateTime.Now.Day);


        }
          private int GetLevelRekening()
        {

            int SelectedIndex=cmbLevelRekening.SelectedIndex;
            ListItemData li = (ListItemData)cmbLevelRekening.Items[SelectedIndex];
            return li.Itemdata;


        }
          private void cmdLoad_Click(object sender, EventArgs e)
          {

              try
              {
                  mTanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
                  mTanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;
                  RealisasiAKLogic oLogic = new RealisasiAKLogic(GlobalVar.TahunAnggaran);
                  if (chkSemuaDinas.Checked == true)
                  {
                      m_iSKPD = 0;
                  }
                  else
                  {
                      m_iSKPD = ctrlDinas1.GetID();
                      if (m_iSKPD == 0)
                      {
                          MessageBox.Show("Belum pilih dinas.");
                          return;

                      }
                  }
                  if (LoadRekening() == true)
                  {
                      DisplayRekening();
                    
                  }
                  else
                  {
                      return;
                  }
                
             
                  LoadneracaAwal();

                  LoadLOTahunBerjalan();

                  HilangkanBarisKosong();
                  IsiSelisihDanProsentas();
                for (int i = 0; i < gridLRATrx.Rows.Count; i++)
                {
                    if (DataFormat.GetString(gridLRATrx.Rows[i].Cells[0].Value).Trim().Length > 14)
                    {
                        gridLRATrx.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                    }

                }
                return;
                
              }
              catch (Exception ex)
              {

              }
          }
          private void HilangkanBarisKosong()
          {
              for (int idx = gridLRATrx.Rows.Count - 2; idx >= 0; idx--)
              {
                  if (DataFormat.FormatUangReportKeDecimal(gridLRATrx.Rows[idx].Cells[COL_LOKINI].Value) == 0 &&
                      DataFormat.FormatUangReportKeDecimal(gridLRATrx.Rows[idx].Cells[COL_LOLALU].Value) == 0 &&
                      DataFormat.GetString(gridLRATrx.Rows[idx].Cells[COL_IDREKENING].Value) != "J" &&
                         DataFormat.GetString(gridLRATrx.Rows[idx].Cells[COL_IDREKENING].Value) !=  CON_STRING_JUMLAHPENDAPATAN &&
                        DataFormat.GetString(gridLRATrx.Rows[idx].Cells[COL_IDREKENING].Value) != CON_STRING_JUMLAHBELANJA &&
                        DataFormat.GetString(gridLRATrx.Rows[idx].Cells[COL_IDREKENING].Value) != CON_STRING_SURPLUSDEFISIT

                                      )
                  {
                      gridLRATrx.Rows.RemoveAt(idx);
                  }
              }
          }
          private void IsiSelisihDanProsentas()
          {
              for (int idx =0 ; idx < gridLRATrx.Rows.Count - 1; idx++)
              {
                  decimal selisih = 0l;
                  string prosentase = "";
                  selisih = DataFormat.FormatUangReportKeDecimal(gridLRATrx.Rows[idx].Cells[COL_LOKINI].Value) -
                            DataFormat.FormatUangReportKeDecimal(gridLRATrx.Rows[idx].Cells[COL_LOLALU].Value);
                  prosentase = GetProsentase(DataFormat.FormatUangReportKeDecimal(gridLRATrx.Rows[idx].Cells[COL_LOKINI].Value),
                     DataFormat.FormatUangReportKeDecimal(gridLRATrx.Rows[idx].Cells[COL_LOLALU].Value));
                  gridLRATrx.Rows[idx].Cells[COL_SELISIH].Value = selisih.ToRupiahInReport();
                  gridLRATrx.Rows[idx].Cells[COL_LEVEL].Value = prosentase;


              }  
          }
          private string GetProsentase( decimal a, decimal b)
          {
              decimal p;
              if (b > 0)
              {
                  p = a / b;

              }
              else
              {
                  p = 1;
              }
              if (p == 0)
                  return "0.00";

              return (p * 100).ToString("#.##");
          }
          private void LoadneracaAwal()
          {
              try
              {
                JumlahNABeban = 0l;
                JumlahNAPendapatan = 0L;

                m_lstNeracaAwal = new List<Realisasi04AK>();
                  List<Realisasi04AK> lstInRpt = new List<Realisasi04AK>();
                  RealisasiAKLogic oLogic = new RealisasiAKLogic(GlobalVar.TahunAnggaran);
                  m_iSKPD = ctrlDinas1.GetID();
                  m_lstNeracaAwal = oLogic.GetSaldoWAwalBukubesar(m_iSKPD, 3);
                  int mx_Level = GetLevelRekening();
                  if (GetLevelRekening() == 6)
                  {

                      mx_Level = 5;
                  }

                  if (m_lstNeracaAwal != null)
                  {
                      for (int l = 1; l <= mx_Level; l++)
                      {
                          List<Realisasi04AK> lstTambahan = ProsesLOLevel1(m_lstNeracaAwal, l);
                          if (lstTambahan != null)
                          {
                              foreach (Realisasi04AK v in lstTambahan)
                              {
                                  if (l == 1)
                                  {
                                      if (v.idRekening == 700000000000)
                                      {
                                          JumlahNAPendapatan = v.Jumlah;
                                      }
                                      if (v.idRekening == 800000000000)
                                      {
                                          JumlahNABeban = v.Jumlah;
                                      }


                                  }
                                  lstInRpt.Add(v);
                              }
                          }
                      }

                  }

                  int pengali = 1;
                  if (GetLevelRekening() == 6)
                  {
                      foreach (Realisasi04AK v in m_lstNeracaAwal)
                      {
                          if (v.idRekening < 800000000000)
                          {
                              pengali = -1;
                          }
                          else
                          {
                              pengali = 1;
                          }
                          Realisasi04AK ditambahkan = v;
                          ditambahkan.Jumlah = pengali * ditambahkan.Jumlah;

                          lstInRpt.Add(ditambahkan);
                      }

                  }

                  lstInRpt.OrderBy(x => x.idRekening);
                  for (int i = 0; i < lstInRpt.Count; i++)
                  {
                      for (int idx = 0; idx < gridLRATrx.Rows.Count; idx++)
                      {
                          if (DataFormat.GetLong(gridLRATrx.Rows[idx].Cells[COL_IDREKENING].Value) == lstInRpt[i].idRekening)
                          {
                              gridLRATrx.Rows[idx].Cells[COL_LOLALU].Value = lstInRpt[i].Jumlah.ToRupiahInReport();


                          }
                      }
                  }

                  for (int r = 0; r < gridLRATrx.Rows.Count; r++)
                  {
                      if (DataFormat.GetString(gridLRATrx.Rows[r].Cells[COL_IDREKENING].Value) == CON_STRING_JUMLAHPENDAPATAN)
                      {

                          gridLRATrx.Rows[r].Cells[COL_LOLALU].Value = JumlahNAPendapatan.ToRupiahInReport();
                      }

                      if (DataFormat.GetString(gridLRATrx.Rows[r].Cells[COL_IDREKENING].Value) == CON_STRING_JUMLAHBELANJA)
                      {

                          gridLRATrx.Rows[r].Cells[COL_LOLALU].Value = JumlahNABeban.ToRupiahInReport();
                      }
                      if (DataFormat.GetString(gridLRATrx.Rows[r].Cells[COL_IDREKENING].Value) == CON_STRING_SURPLUSDEFISIT)
                      {

                          gridLRATrx.Rows[r].Cells[COL_LOLALU].Value = (JumlahNAPendapatan - JumlahNABeban).ToRupiahInReport();
                      }
                  }

              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message);
              }
          }
          private void LoadLOTahunBerjalan()
          {
              try
              {
                JumlahNABeban = 0l;
                JumlahNAPendapatan = 0L;
               

                m_lstLO = new List<Realisasi04AK>();




                  int iLevelRekening = GetLevelRekening();
     
                  List<Realisasi04AK> lstInRpt = new List<Realisasi04AK>();
                  RealisasiAKLogic oLogic = new RealisasiAKLogic(GlobalVar.TahunAnggaran);
                  

                  m_lstLO = oLogic.GetLO(m_iSKPD, mTanggalAkhir);

                  int mx_Level = iLevelRekening;
                  if (iLevelRekening == 6)
                  {

                      mx_Level = 5;
                  }

                  if (m_lstLO != null)
                  {
                      for (int l = 1; l <= mx_Level; l++)
                      {
                          List<Realisasi04AK> lstTambahan = ProsesLOLevel1(m_lstLO, l);
                          if (lstTambahan != null)
                          {
                              foreach (Realisasi04AK v in lstTambahan)
                              {
                                  if (l == 1)
                                  {
                                      if (v.idRekening == 700000000000)
                                      {
                                          JumlahNAPendapatan = v.Jumlah;
                                      }
                                      if (v.idRekening == 800000000000)
                                      {
                                          JumlahNABeban = v.Jumlah;
                                      }
                                   

                                  }
                                  lstInRpt.Add(v);
                              }
                          }
                      }

                  }

                  int pengali;
                  if (iLevelRekening == 6)
                  {
                      foreach (Realisasi04AK v in m_lstLO)
                      {
                          if (v.idRekening < 800000000000){
                              pengali = -1;}else{
                          pengali = 1;}

                          Realisasi04AK ditambahkan = v;
                          ditambahkan.Jumlah = pengali * ditambahkan.Jumlah;
                          lstInRpt.Add(v);
                      }

                  }

                  lstInRpt.OrderBy(x => x.idRekening);
                  for (int i = 0; i < lstInRpt.Count; i++)
                  {
                      for (int idx = 0; idx < gridLRATrx.Rows.Count; idx++)
                      {
                          if (DataFormat.GetLong(gridLRATrx.Rows[idx].Cells[COL_IDREKENING].Value) == lstInRpt[i].idRekening)
                          {
                              gridLRATrx.Rows[idx].Cells[COL_LOKINI].Value = lstInRpt[i].Jumlah.ToRupiahInReport();
                         

                          }
                      }
                  }
  
                  for (int r = 0; r < gridLRATrx.Rows.Count; r++)
                  {
                      if (DataFormat.GetString(gridLRATrx.Rows[r].Cells[COL_IDREKENING].Value) == CON_STRING_JUMLAHPENDAPATAN)
                      {

                          gridLRATrx.Rows[r].Cells[COL_LOKINI].Value = JumlahNAPendapatan.ToRupiahInReport();
                      }

                      if (DataFormat.GetString(gridLRATrx.Rows[r].Cells[COL_IDREKENING].Value) == CON_STRING_JUMLAHBELANJA)
                      {

                          gridLRATrx.Rows[r].Cells[COL_LOKINI].Value = JumlahNABeban.ToRupiahInReport();
                      }
                      if (DataFormat.GetString(gridLRATrx.Rows[r].Cells[COL_IDREKENING].Value) == CON_STRING_SURPLUSDEFISIT)
                      {

                          gridLRATrx.Rows[r].Cells[COL_LOKINI].Value = (JumlahNAPendapatan - JumlahNABeban).ToRupiahInReport();
                      }
                  }

              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message);
              }
          }
          private void ExportSbgSaldoAwalTahundeapn()
          {
              try
              {
                  m_lstLO = new List<Realisasi04AK>();




                  int iLevelRekening = GetLevelRekening();
                  m_lstNeracaAwal = new List<Realisasi04AK>();
                  List<Realisasi04AK> lstInRpt = new List<Realisasi04AK>();
                  RealisasiAKLogic oLogic = new RealisasiAKLogic(GlobalVar.TahunAnggaran);
                  m_iSKPD = ctrlDinas1.GetID();

                  m_lstLO = oLogic.GetLO(m_iSKPD, mTanggalAkhir);


                  if (m_lstLO == null)
                  {
                      MessageBox.Show(oLogic.LastError());
                      return;
                  }


                  SaldoAwalRehLogic oSALogic = new SaldoAwalRehLogic(GlobalVar.TahunAnggaran);
                foreach (Realisasi04AK v in m_lstLO)
                {
                    SaldoAwalRek sa = new SaldoAwalRek();
                    sa.IDRekening = v.idRekening;
                    sa.Tanggal = new DateTime(GlobalVar.TahunAnggaran, 12, 31);
                    sa.Jumlah = v.Jumlah < 0 ? -1 * v.Jumlah : v.Jumlah;
                    sa.Debet = v.Jumlah > 0 ? 1 : -1;
                    sa.IDDinas = m_iSKPD;
                    if (sa.IDRekening == 110101010001)
                    {
                        MessageBox.Show(sa.IDRekening.ToString());
                    }
                    oSALogic.SimpanSebagaiSaldoAwalTahunBerikut(sa);
                }


                MessageBox.Show("Penympanan Selesai..");
                  
              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message);
              }
          }
          private List<Realisasi04AK> ProsesLOLevel1(List<Realisasi04AK> lst, int level, int col = 3)
          {
             
            try
            {
                int lenKode = 12;
                switch (level)
                {
                    case 1:
                        lenKode = 1;
                        break;
                    case 2:
                        lenKode = 2;
                        break;
                    case 3:
                        lenKode = 4;
                        break;
                    case 4:
                        lenKode = 6;
                        break;
                    case 5:
                        lenKode = 8;
                        break;
                }


                   List<Rekening> lstRekening;

                lstRekening = m_lstRekening.FindAll(x => x.Root== level);
                var lstJumlah = lst.Where(l => l.Level == 6)
                      .GroupBy(c => c.idRekening.ToString().Substring(0, lenKode))

                .Select(x => new
                {
                    IDRekening = x.Key,                    
                    Jumlah = x.Sum (y=>y.Jumlah)

                }).ToList();



                List<Realisasi04AK> lstKi = (from t in
                                                 lstRekening
                                            join j in lstJumlah
                                            on t.ID.ToString().Substring(0, lenKode) equals j.IDRekening.Substring(0, lenKode)

                                             select new Realisasi04AK
                                            {
                                                 idRekening= t.ID,
                                                Level = level,
                                    
                                                Jumlah = j.Jumlah * t.Debet,
                                            }).ToList<Realisasi04AK>();






                return lstKi;
            }
            catch (Exception ex)
            {
                return null;
            }


         }
        private bool LoadRekening()
        {
            try
            {
                m_lstRekening = new List<Rekening>();
                RekeningLogic oRekeningLogic = new RekeningLogic(GlobalVar.TahunAnggaran);
                m_lstRekening = oRekeningLogic.Get().Where(r => r.ID >= 700000000000 && r.ID < 999999999999).ToList();


               
                return true;

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }            

        }
        private void DisplayRekening()
        {

            try
            {
                string[] Bariskosong = { "", "", "", "","","","J" };
                if (m_lstRekening == null)
                {
                    LoadRekening();
                }
                gridLRATrx.Rows.Clear();

                foreach (Rekening Rek in m_lstRekening)
                {
                    if (Rek.ID == 800000000000)
                    {
                  
                        gridLRATrx.Rows.Add(Bariskosong);
                        string[] rowAset1 = { "", "JUMLAH PENDAPATAN", "", "", "", "", CON_STRING_JUMLAHPENDAPATAN };
                        gridLRATrx.Rows.Add(rowAset1);

                        gridLRATrx.Rows.Add(Bariskosong);
                    }
                    string[] row = { Rek.ID.ToString().ToKodeRekening(Rek.Root) ,  Rek.Nama,"","","",Rek.Root.ToString(),Rek.ID.ToString() };
                    gridLRATrx.Rows.Add(row);


                }
              
                gridLRATrx.Rows.Add(Bariskosong);
                string[] rowAset2 = {  "",  "JUMLAH BEBAN" , "", "", "", "", CON_STRING_JUMLAHBELANJA }; 
                gridLRATrx.Rows.Add(rowAset2);
                gridLRATrx.Rows.Add(Bariskosong);
                string[] rowAset3 = { "", "SURPLUS/DEFISIT LO", "", "", "", "", CON_STRING_SURPLUSDEFISIT };
                gridLRATrx.Rows.Add(rowAset3);
                gridLRATrx.Rows.Add(Bariskosong);
                gridLRATrx.Rows.Add(Bariskosong);



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
      
        private bool DisplayNeracaAwal(int Level , long IdParent)
        {
            //return true;

            int lenKode = 12;
            switch (Level)
            {
                case 1:
                    lenKode = 1;
                    break;
                case 2:
                    lenKode = 2;
                    break;
                case 3:
                    lenKode = 4;
                    break;
                case 4:
                    lenKode = 6;
                    break;
                case 5:
                    lenKode = 8;
                    break;
                case 6:
                    lenKode = 12;
                    break;
            }
            var lstJumlah = m_lstNeracaAwal.Where(l => l.idRekening.ToString().Length == 12).GroupBy(x => x.idRekening.ToString().Substring(0, lenKode))
              .Select(x => new
              {
                  IDRekening= x.Key,              
                  Jumlah = x.Sum(y => y.Debet * y.Jumlah),

              }).ToList();
            List<Laporan> lstLaporan = (from t in m_lstRekening
                                                  join j in lstJumlah
                                                 on t.ID.ToString().Substring(0, lenKode) equals j.IDRekening
                                                  where t.Root == Level && t.IDParent == IdParent
                                                  select new Laporan
                                                  {
                                                 
                                                      IDRekening = t.ID,
                                                      Jumlah = (int)t.Debet * j.Jumlah,
                                          
                                                      

                                                  }).ToList<Laporan>();

            foreach (Laporan lp in lstLaporan)
            {
                decimal anggaran = 0L;
               


                string[] dataLRA = { lp.Kode, lp.Nama,"0", lp.Jumlah.ToRupiahInReport(),Level.ToString(), lp.IDRekening.ToString(), lp.SaldoNormal.ToString() };

                gridLRATrx.Rows.Add(dataLRA);

                if (GetLevelRekening() > Level)
                {
                    DisplayNeracaAwal (Level + 1, lp.IDRekening);
                }



            }


            return true;

        }

        private void ctrlTanggalBulanVertikal1_Load(object sender, EventArgs e)
        {

        }

        private void cmdJadikanSaldoAwal_Click(object sender, EventArgs e)
        {
            try
            {
                mTanggalAwal = new DateTime (GlobalVar.TahunAnggaran ,1 ,1);
                mTanggalAkhir = new DateTime(GlobalVar.TahunAnggaran, 12, 31);
                RealisasiAKLogic oLogic = new RealisasiAKLogic(GlobalVar.TahunAnggaran);
                if (chkSemuaDinas.Checked == true)
                {
                    m_iSKPD = 0;
                }
                else
                {
                    m_iSKPD = ctrlDinas1.GetID();
                    if (m_iSKPD == 0)
                    {
                        MessageBox.Show("Belum pilih dinas.");
                        return;

                    }
                }

                ExportSbgSaldoAwalTahundeapn();
        
                return;

            }
            catch (Exception ex)
            {

            }
        }
        private void Pages_PageAdded(object sender, PageAddedEventArgs args)
        {
            float yPos = PosisiTerakhir + 5;

            float setengah = (previousPage.GetClientSize().Width / 2) - 20;
            float posisiTengah = (previousPage.GetClientSize().Width / 2) + 10;
            PdfStringFormat stringFormat = new PdfStringFormat();


            CetakPDF oCetakPDF = new CetakPDF();


            if (SaatnyacetakKesimpulan == true)
            {





                Pejabat bendahara = new Pejabat();
                Pejabat pimpinan = new Pejabat();
             
                bendahara = ctrlDinas1.GetBendaharaPengeluaran(ctrlTanggal1.Tanggal);
                
                pimpinan = ctrlDinas1.GetPimpinan(ctrlTanggal1.Tanggal);
                yPos = yPos + 10;
                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, GlobalVar.gPemda.Ibukota + "," + ctrlTanggal1.Tanggal.ToTanggalIndonesia(), 10, posisiTengah, yPos, setengah, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, pimpinan.Jabatan, 10, 30, yPos, setengah, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, bendahara.Jabatan, 10, posisiTengah, yPos, setengah, stringFormat, true, false, true);
                yPos = yPos + 30;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, pimpinan.Nama, 10, 30, yPos, setengah, stringFormat, false, true, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, bendahara.Nama, 10, posisiTengah, yPos, setengah, stringFormat, true, true, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "NIP " + pimpinan.NIP, 10, 30, yPos, setengah, stringFormat, false);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "NIP " + bendahara.NIP, 10, posisiTengah, yPos, setengah, stringFormat, true);




            }






            previousPage = args.Page;
        }
        private void cmdCetak_Click(object sender, EventArgs e)
        {
            try
            {
                PdfDocument document = new PdfDocument();
                PdfSection section = document.Sections.Add();
                document.PageSettings.Margins.Left = 8;
                document.PageSettings.Margins.Top = 5;
                document.PageSettings.Margins.Right = 2;
                document.PageSettings.Margins.Bottom = 8;
                section.PageSettings.Width = 612;// = PdfPageSize.Legal;
                section.PageSettings.Height = 935;// = PdfPageSize.Legal;
                section.PageSettings.Orientation = PdfPageOrientation.Portrait;

                float yPos = 0;
                SaatnyacetakKesimpulan = false;
                PdfPage page = section.Pages.Add();

                PdfGraphics graphics = page.Graphics;
                previousPage = page;
                document.Pages.PageAdded += Pages_PageAdded;

                yPos = 10;
                PdfPen pen = new PdfPen(PdfBrushes.Black, 0.2f);


                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                stringFormat.LineAlignment = PdfVerticalAlignment.Middle;

                //stringFormat.CharacterSpacing = 2f;
                oCetakPDF = new CetakPDF();
                //SizeF size = font12.MeasureString("xxx");


                Pejabat pimpinan = new Pejabat();
                Pejabat bendahara = new Pejabat();
                //pimpinan = ctrlDinas1.GetPimpinan(mTanggalAkhir);
                //bendahara = ctrlDinas1.GetBendaharaPengeluaran(mTanggalAkhir);

                //if (pimpinan == null)
                //{
                //    MessageBox.Show("Data Pimpinan belum di setting di master pejabat");
                //    return ;

                //}

                //if (bendahara == null)
                //{
                //    MessageBox.Show("Data Bndahara belum di setting di master pejabat");
                //    return ;

                //}


                float kiri = 15;
                float posTitikdua = 150;
                float posNama = 155;

                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = 10;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "PEMERINTAH KABUPATEN KETAPANG", 10, kiri, yPos,
                page.GetClientSize().Width, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "LAPORAN OPERASIONAL", 10, kiri, yPos,
                page.GetClientSize().Width, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "PERIODE " + ctrlTanggalBulanVertikal1.TanggalAwal.ToTanggalIndonesia()  + " s/d " + ctrlTanggalBulanVertikal1.TanggalAkhir.ToTanggalIndonesia(), 10, kiri, yPos,
                page.GetClientSize().Width, stringFormat, true, false, true);
                yPos = yPos + 20;




                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = yPos + 20;
                if (m_iSKPD > 0)
                {
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics, "SKPD "
                              , 10, kiri, yPos,
                              page.GetClientSize().Width, stringFormat, false, false, true);
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                              page.GetClientSize().Width, stringFormat, false, false, true);
                    yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                              ctrlDinas1.GetNamaSKPD(), 10, 155, yPos,
                              page.GetClientSize().Width, stringFormat, true, false, true);
                }


                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Periode "
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          ctrlTanggalBulanVertikal1.Waktu, 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Tahun Anggaran"
                          , 10, kiri, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ":", 10, 150, yPos,
                          page.GetClientSize().Width, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          GlobalVar.TahunAnggaran.ToString(), 10, 155, yPos,
                          page.GetClientSize().Width, stringFormat, true, false, true);
                #region gridKas
                PdfGrid pdfGrid = new PdfGrid();

                int count = 0;
                //Create a DataTable
                DataTable table = new DataTable();
                //Add columns to table
                table.Columns.Add("Kode Rekening");
                table.Columns.Add("Uraian");
                table.Columns.Add("Tahun  " + GlobalVar.TahunAnggaran.ToString());
                table.Columns.Add("Tahun  " + (GlobalVar.TahunAnggaran - 1).ToString());
                table.Columns.Add("Kenaikan/Penurunan");
                table.Columns.Add("%");


                int columnCount = table.Columns.Count;
                List<object> data = new List<object>();




                for (int idx = 0; idx <  gridLRATrx.Rows.Count; idx++)
                {

                    table.Rows.Add(new string[]
                    {

                       DataFormat.GetString(gridLRATrx.Rows[idx].Cells[COL_KODEREKENING].Value).ReplaceUnicode(),
                       DataFormat.GetString(gridLRATrx.Rows[idx].Cells[COL_NAMAREKENING].Value).ReplaceUnicode(),
                       DataFormat.GetString(gridLRATrx.Rows[idx].Cells[COL_LOKINI].Value).ReplaceUnicode(),
                       DataFormat.GetString(gridLRATrx.Rows[idx].Cells[COL_LOLALU].Value).ReplaceUnicode(),
                      DataFormat.GetString(gridLRATrx.Rows[idx].Cells[COL_SELISIH].Value).ReplaceUnicode(),
                       DataFormat.GetString(gridLRATrx.Rows[idx].Cells[COL_LEVEL].Value).ReplaceUnicode(),

                    });
                }

                pdfGrid.DataSource = table; //data
                pdfGrid.Columns[0].Width = 80;
                pdfGrid.Columns[1].Width = 160;
                pdfGrid.Columns[2].Width = 80;
                pdfGrid.Columns[3].Width = 80;
                pdfGrid.Columns[4].Width = 70;
                pdfGrid.Columns[5].Width = 30;


                PdfGridStyle gridStyle = new PdfGridStyle();
                //Adding cell padding
                gridStyle.CellPadding = new PdfPaddings(5, 5, DataFormat.GetInteger(txtSpasi.Text), DataFormat.GetInteger(txtSpasi.Text));
                //gridStyle.CellPadding = new PdfPaddings(5, 5, 3, 3);

                pdfGrid.Style = gridStyle;


                PdfStringFormat formatKolomAngka = new PdfStringFormat();
                PdfStringFormat formatKolomTengah = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

                pdfGrid.Columns[3].Format = formatKolomAngka;
                pdfGrid.Columns[2].Format = formatKolomAngka;
                pdfGrid.Columns[4].Format = formatKolomAngka;



                formatKolomTengah.Alignment = PdfTextAlignment.Center;
                pdfGrid.Columns[5].Format = formatKolomTengah;










                PdfFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 8));

                PdfGridCellStyle cellStyle = new PdfGridCellStyle();
                PdfGridCellStyle cellHeaderStyle = new PdfGridCellStyle();

                pdfGrid.RepeatHeader = true;
                PdfStringFormat stringFormatHeader = new PdfStringFormat();
                stringFormatHeader.Alignment = PdfTextAlignment.Center;
                stringFormatHeader.LineAlignment = PdfVerticalAlignment.Middle;

                font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", font.Size, FontStyle.Bold)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f); PdfTrueTypeFont(new System.Drawing.Font("Arial", 10));

                cellHeaderStyle.Font = font;

                cellHeaderStyle.StringFormat = stringFormatHeader;
                for (int c = 0; c < pdfGrid.Headers.Count; c++)
                {
                    pdfGrid.Headers[c].ApplyStyle(cellHeaderStyle);
                    pdfGrid.Headers[c].Height = 30;

                }


                for (int idx = 0; idx < pdfGrid.Rows.Count; idx++)
                {
                    pdfGrid.Rows[idx].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 7,
                        FontStyle.Regular)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f);

                    for (int c = 0; c < pdfGrid.Columns.Count; c++)
                    {
                        if (c == 4)
                        {
                            pdfGrid.Rows[idx].Cells[c].Style.CellPadding = new PdfPaddings(1, 1, 1, 0);
                        }
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Bottom.Width = 0.05F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Top.Width = 0.05F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Left.Width = 0.05F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Right.Width = 0.05F;

                    }

                }

                //Draw grid on the page of PDF document and store the grid position in PdfGridLayoutResult.

                PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(page, new PointF(kiri, yPos));
                #endregion gridKas
                //  pdfGridLayoutResult = pdfGridRingkasan.Draw(pdfGridLayoutResult.Page, new PointF(kiri, pdfGridLayoutResult.Bounds.Bottom));

                yPos = pdfGridLayoutResult.Bounds.Bottom + 10;

                PejabatLogic oLogicPejabat = new PejabatLogic(GlobalVar.TahunAnggaran);

                Pejabat oKada = new Pejabat();

                DateTime d = DateTime.Now;
                if (chkSemuaDinas.Checked == true)
                {
                    oKada = oLogicPejabat.GetKepalaDaerah();
                }
                else
                {
                    oKada = ctrlDinas1.GetPimpinan(d);
                }

                float setengah = page.GetClientSize().Width / 2;
                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                           "Ketapang, " + ctrlTanggal1.TextTanggalLengkap, 9, kiri + setengah, yPos,
                           setengah, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                          oKada.Jabatan, 9, kiri + setengah, yPos,
                          setengah, stringFormat, true, false, true);
                yPos = yPos + 30;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                         oKada.Nama, 9, kiri + setengah, yPos,
                         setengah, stringFormat, true, true, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics,
                    oKada.NIP, 9, kiri + setengah, yPos,
                    setengah, stringFormat, true, false, true);





                //PosisiTerakhir = pdfGridLayoutResult.Bounds.Bottom;
                //SaatnyacetakKesimpulan = true;
                //page = document.Pages.Add();

                using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../LO.pdf"), FileMode.Create, FileAccess.ReadWrite))
                {
                    //Save the PDF document to file stream.
                    document.Save(outputFileStream);

                }

                //Close the document.
                document.Close(true);
                pdfViewer pV = new pdfViewer();
                pV.Document = Path.GetFullPath(@"../../../LO.pdf");
                pV.Show();


                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void cmdExcell_Click(object sender, EventArgs e)
        {
try
            {

                Microsoft.Office.Interop.Excel.Application excel;
                Microsoft.Office.Interop.Excel.Workbook excelworkBook;
                Microsoft.Office.Interop.Excel.Worksheet excelSheet;
                Microsoft.Office.Interop.Excel.Range excelCellrange;
                // Start Excel and get Application object.
                excel = new Microsoft.Office.Interop.Excel.Application();

                // Make Excel invisible and disable alerts.
                excel.Visible = false;
                excel.DisplayAlerts = false;

                // Create a new Workbook.
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Create a Worksheet.
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
                if (chkSemuaDinas.Checked == true)
                {
                    excelSheet.Name = "LO" + ctrlDinas1.GetNamaSKPD();
                }
                else
                {
                    excelSheet.Name = "LO";
                }

                // header
                for (int i = 1; i < gridLRATrx.Columns.Count + 1; i++)
                {
                    excelSheet.Cells[1, i] = gridLRATrx.Columns[i - 1].HeaderText;
                }

                for (int row = 0; row < gridLRATrx.Rows.Count; row++)
                {
                    //for (int col = 0; col < gridLRATrx.Columns.Count; col++)
                        for (int col = 0; col < 4; col++)
                        {
                        if (col > 1)
                        {

                            string s = DataFormat.GetString(gridLRATrx.Rows[row].Cells[col].Value);
                            if (s == "")
                            {
                                s = "0";
                            }
                            excelSheet.Cells[row + 2, col + 1] = DataFormat.FormatUangReportKeDecimal(s);


                        }
                        else
                        {
                            excelSheet.Cells[row + 2, col + 1] = DataFormat.GetString(gridLRATrx.Rows[row].Cells[col].Value);
                        }


                    }
                }


                // now we resize the columns
                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[excelSheet.Rows.Count, excelSheet.Columns.Count]];
                //excelCellrange.EntireColumn.AutoFit();
                //excelSheet.Range (“G:G”).NumberFormat = “0.00”;
                //Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
                //border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //border.Weight = 2d;
                string namaFile = BuatFile();
                if (namaFile.Trim().Length == 0)
                {
                    MessageBox.Show("Nama Masih Kosong ");
                    return;
                }

                excelworkBook.SaveAs(namaFile);
                MessageBox.Show("File sudah disimpan di " + namaFile);


                excelworkBook.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal export ke excell" + ex.Message);
            }
        } 
        private string BuatFile()
        {

            string sRet = "";
            SaveFileDialog fdlg = new SaveFileDialog();
            fdlg.Filter = "Excel|*.xlsx;*.xls";
            fdlg.Title = "Save an Image File";
            fdlg.ShowDialog();

            fdlg.Title = "Buat File file";
            fdlg.InitialDirectory = @"c:\";

            //fdlg.FileName = txtFileName.Text;
            fdlg.Filter = "Excel|*.xlsx;*.xls";
            fdlg.RestoreDirectory = true;




            //if (fdlg.FileName != "")
            //{
            // Saves the Image via a FileStream created by the OpenFile method.  

            sRet = fdlg.FileName;


            //  }
            return sRet;
        }

        private void tanggalTandaTangan1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void gridLRATrx_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex>=0  && e.RowIndex< gridLRATrx.Rows.Count)
                {

                    if (e.ColumnIndex == 8)
                    {
                        frmBukuBesarDlg fBukubesarDlg = new frmBukuBesarDlg();
                        fBukubesarDlg.SKPD = m_iSKPD;
                        fBukubesarDlg.Tanggal = mTanggalAkhir;
                        fBukubesarDlg.IDRekening = DataFormat.GetLong(DataFormat.GetString(gridLRATrx.Rows[e.RowIndex].Cells[0].Value).Replace(".", ""));
                        fBukubesarDlg.NamaRekening = DataFormat.GetString(gridLRATrx.Rows[e.RowIndex].Cells[1].Value);
                        
                        if (lblFolderPath.Text =="" || lblFolderPath.Text == "label4")
                        {
                            MessageBox.Show("direktori tidak di kenal");
                            return;
                        }
                        fBukubesarDlg.Direktori = lblFolderPath.Text;
                        fBukubesarDlg.Excell = chkExcelkanBB.Checked;
                        fBukubesarDlg.LoadData();

                        if (fBukubesarDlg.IsDisposed == false)
                        {
                            fBukubesarDlg.Show();
                        }



                    }
                }

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void cmdDirektori_Click(object sender, EventArgs e)
        {
            
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            lblFolderPath.Text = dialog.SelectedPath;

        }

        private void lblFolderPath_Click(object sender, EventArgs e)
        {

        }
    }
}
