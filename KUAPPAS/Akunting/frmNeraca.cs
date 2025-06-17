using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP;
using BP.Anggaran;
using DTO;
using DTO.Akuntansi;
using DTO.Anggaran;
using DTO.Laporan;
using BP.Akuntansi;
using Formatting;
using System.Diagnostics;
using System.IO;
using BP.Akuntansi;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

using System.IO;
using BP.Bendahara;
using DTO.Bendahara;
namespace KUAPPAS.Akunting
{
    public partial class frmNeraca : ChildForm
    {
        private const int COL_IDREKEINING=0;
        private const int COL_KDEREKEINING = 2;
        private const int COL_NAMAREKEINING =3;
        private const int COL_JUMLAHLALU = 5;
        private const int COL_JUMLAHBAYANGANLALU = 6;
        private bool mgabunganPPKDdanBukan;
        private const int COL_JUMLAHKINI = 4;

        private int m_iSKPD;
        private int m_iPPKD;
        DateTime mTanggalAkhir;
        DateTime mTanggalAwal;
        private decimal JumahAset;
        private decimal JumahKewajiban;
        private decimal JumahEkuitas;


        private const long CON_BATAS_ATAS_PENDAPATAN = 500000000000;
        private const long CON_BATAS_ATAS_BELANJA = 600000000000;
        private const long CON_BATAS_ATAS_PENERIMAANPEMBIAYAAN = 620000000000;
        private const long CON_REKENING_SAL = 310205010001;

        private const string CON_STRING_JUMLAHASET = "JA";
        private const string CON_STRING_JUMLAHKEWAJIBAN = "JK";
        private const string CON_STRING_JUMLAHEKUITAS = "JE";

        List<Realisasi04AK> lstNeracaAwal;
        private List<Rekening> m_lstRekening;
        CetakPDF oCetakPDF;
        PdfPage previousPage;
        int m_iJenisBendahara;
        bool SaatnyacetakKesimpulan;
        float PosisiTerakhir;

        public frmNeraca()
        {
            InitializeComponent();
            lstNeracaAwal = new List<Realisasi04AK>();
            mgabunganPPKDdanBukan = false;
        }

        private void frmNeraca_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Neraca");
            ctrlDinas1.Create();
            ctrlHeader1.SetCaption("Laporan Neraca");
            ctrlDinas1.Create();
            gridNeraca.FormatHeader();
            ListItemData li = new ListItemData("Akun Utama", 1);
            cmbLevelRekening.Items.Add(li);
            if (GlobalVar.TahunAnggaran == 2023)
            {
                cmdJadikanSaldoAwal.Visible = true;
            }
            else
            {
                cmdJadikanSaldoAwal.Visible = false;
            }
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
    
           ctrlTanggalBulanVertikal1.TanggalAwal=new DateTime(GlobalVar.TahunAnggaran,1,1);
           ctrlTanggalBulanVertikal1.TanggalAkhir = new DateTime(GlobalVar.TahunAnggaran, 12, 31);

          // ctrlTanggalBulanVertikal1.TanggalAkhir = new DateTime(GlobalVar.TahunAnggaran,DateTime.Now.Month, DateTime.Now.Day);
            JumahAset=0;
        JumahKewajiban=0;
        JumahEkuitas=0;

        }
        private int GetLevelRekening()
        {

            int SelectedIndex = cmbLevelRekening.SelectedIndex;
            if (SelectedIndex > -1)
            {
                ListItemData li = (ListItemData)cmbLevelRekening.Items[SelectedIndex];
                return li.Itemdata;
            }
            else
            {
                return 0;
            }


        }
        private bool LoadRekening()
        {
            try
            {
                m_lstRekening = new List<Rekening>();
                RekeningLogic oRekeningLogic = new RekeningLogic(GlobalVar.TahunAnggaran);
                m_lstRekening = oRekeningLogic.Get().Where(r => r.ID >= 100000000000 && r.ID < 500000000000 && r.Root<= GetLevelRekening()).ToList();


                ////m_lstRekeningAset = new List<Rekening>();
                ////m_lstRekeningKewajiban = new List<Rekening>();
                ////m_lstRekeningEkuitas = new List<Rekening>();

                ////m_lstRekeningAset = m_lstRekening.FindAll(x => x.ID > CON_BATAS_ATAS_PENDAPATAN && x.ID < CON_BATAS_ATAS_BELANJA);
                ////m_lstRekeningKewajiban = m_lstRekening.FindAll(x => x.ID > CON_BATAS_ATAS_BELANJA && x.ID < CON_BATAS_ATAS_PENERIMAANPEMBIAYAAN);
                ////m_lstRekeningEkuitas = m_lstRekening.FindAll(x => x.ID > CON_BATAS_ATAS_PENERIMAANPEMBIAYAAN);





                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }

        }
        private int GetPPKDLama()
        {
            int iPPKD = 0;

            if (chkGabunganPPKDDanBukan.Checked == true)
            {
                iPPKD = -1;

            }
            if (chkPPKD.Checked == true)
            {
                iPPKD = 1;
            }
            return iPPKD;
        }
        private int GetPPKD()
        {
            int iPPKD = 0;

             // If chkGabunganPPKDdanBukan.Value = vbUnchecked Then
            if (chkGabunganPPKDDanBukan.Checked == false)
            {
                //If chkPPKD.Value = vbChecked Then
                if (chkPPKD.Checked == true)
                {
                    iPPKD = 1;
                }
                else
                {
                    iPPKD = 0;

                }
            }
            else
            {
                iPPKD = 0;

            }


            return iPPKD;
        }
        private void Panggil(){
            
                JumahAset = 0;
                JumahKewajiban = 0;
                JumahEkuitas = 0;

             
                m_iPPKD = GetPPKD();
                mTanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
                mTanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;

                // Untk ekitas

                ProsessEkuitas();
                if (LoadRekening() == true)
                {
                    DisplayRekening();

                    LoadneracaAwal();
                    JumahAset = 0;
                    JumahKewajiban = 0;
                    JumahEkuitas = 0;
                    LoadBukubesar();
              
                }

        }
    
        private void cmdLoad_Click(object sender, EventArgs e)
        {
            try
            {
                mgabunganPPKDdanBukan = chkGabunganPPKDDanBukan.Checked ? true : false;

                m_iPPKD = GetPPKD();
                if (GetLevelRekening() == 0)
                {
                    MessageBox.Show("Belum memilih Level Rekening.");
                    return;
                }
           
                if (chkSemuaDinas.Checked == false)
                {
                    if (ctrlDinas1.GetID() == 0)
                    {
                        MessageBox.Show("Belum memilih OPD.");
                        return;
                    }
                    m_iSKPD = ctrlDinas1.GetID();
                }
                else
                {
                    m_iSKPD = 0;
                }
                Panggil();

                HilangkanBarisKosong();
                for (int i = 0; i < gridNeraca.Rows.Count; i++)
                {
                    if (DataFormat.GetString(gridNeraca.Rows[i].Cells[2].Value).Trim().Length > 14)
                    {
                        gridNeraca.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void Export(int col)
        {

            string NamaFile = "";
   
            try
            {
                string namaFile = "" ;//= BuatFile();
                if (namaFile.Trim().Length == 0)
                {
                    MessageBox.Show("Nama Masih Kosong ");
                    return;
                }
              //  KillSpecificExcelFileProcess(namaFile);

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
                excelSheet.Name = "Neraca";

                //// storing header part in Excel  

                //for (int i = 1; i < 4; i++)
                //{
                   
                //            excelSheet.Cells[1, i] = gridNeraca.Columns[i - 1].HeaderText;
                       

                //}
                //// storing Each row and column value to excel sheet  
                //List<int> lstColToCetak = new List<int>();
                //for (int i = 0; i < gridNeraca.Rows.Count - 1; i++)
                //{
                //    int c = col;
                //    int awal=0;
                //    int akhir = 3;
                //    if (col >3 )
                //    {
                //        awal=2; 
                //    }
                //    for (int j = awal; j <= 4; j++)
                //    {
                //        if (gridNeraca.Columns[j].Visible == true)
                //        {
                //            ++c;
                //            if (gridNeraca.Rows[i].Cells[j].Value != null)
                //            {
                //                string s = "";
                //                if (j >= 2)
                //                {
                //                    s = DataFormat.FormatUangReportKeDecimal(gridNeraca.Rows[i].Cells[j].Value).ToString("###.##");
                //                }
                //                else
                              
                //                excelSheet.Cells[i + 2, c] = s;



                //            }
                //        }
                //    }

                //}

                // now we resize the columns
                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1],
                                  excelSheet.Cells[excelSheet.Rows.Count - 1,
                                  excelSheet.Columns.Count - 1]];
                //    excelSheet.Columns.AutoFit();
                excelSheet.Columns.ColumnWidth = 20;
                excelSheet.Columns[2].ColumnWidth = 50;
                excelSheet.Columns[2].WrapText = true;

             
                excelworkBook.SaveAs(namaFile);
                MessageBox.Show("File sudah disimpan di " + namaFile);


                excelworkBook.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal export ke excell" + ex.Message);
            }
        }
        private void WritrToExcel ( Microsoft.Office.Interop.Excel.Worksheet excelSheet, int col)
        {
            // storing header part in Excel  

            for (int i = 1; i < 4; i++)
            {

                excelSheet.Cells[1, i] = ctrlDinas1.GetNamaSKPD();


            }
            // storing Each row and column value to excel sheet  
            List<int> lstColToCetak = new List<int>();

            if (col == 0)
            {
                lstColToCetak.Add(COL_IDREKEINING);
                lstColToCetak.Add(COL_NAMAREKEINING);
              
            }
            lstColToCetak.Add(COL_JUMLAHKINI);
            for (int i = 0; i < gridNeraca.Rows.Count - 1; i++)
            {
                int c = 0;
                foreach (int j in lstColToCetak)
                {
                    
                        if (gridNeraca.Rows[i].Cells[j].Value != null)
                        {
                            string s = "";
                            if (j == COL_JUMLAHKINI)
                            {
                                s = DataFormat.FormatUangReportKeDecimal(gridNeraca.Rows[i].Cells[j].Value).ToString("###.##");
                            }
                            else

                                s = gridNeraca.Rows[i].Cells[j].Value.ToString();

                                excelSheet.Cells[i + 2, c] = s;



                        }
                    
                }

            }
        }

        private string GetKeteranganDinas()
        {
            string sRet= "";

            //If chkGabunganPPKDdanBukan.Value = vbChecked Then

            if (chkGabunganPPKDDanBukan.Checked == true) {
                sRet = " AND ((IDDINAS = 5020200  and bPPKD =0) or  bPPKD=1) ";


            }
            else { 
                if (chkSemuaDinas.Checked == false) {
                sRet = " AND IDDINAS = " + ctrlDinas1.GetID().ToString();

                if (chkPPKD.Checked == true) {
                    sRet = sRet + " AND bPPKD =1 ";
                } else {
                    sRet = sRet + " AND bPPKD =0 ";

                }
            }
                    
            //If chkPPKD.Value = vbChecked Then
            //        sRet = " AND bPPKD =1 "
            //Else
            //        sRet = sRet & " AND bPPKD =0 "
                
            //End If

            
         }


            return sRet;


            

        }
        private void ProsessEkuitas(  )
        {
            try
            {
                
    
     decimal cJumlahEkuitasKini=0l;
    decimal cJumlahEkuitasDulu=0L;
    
    
    decimal  cEquitas  =0l;

    decimal  cEquitasLalu  =0l;
    
   
    string sketDinas = GetKeteranganDinas();
    BukuBesarLogic oLogic = new BukuBesarLogic(GlobalVar.TahunAnggaran);
    int iPPKD = chkPPKD.Checked == true ? 1 : 0;
    if (chkGabunganPPKDDanBukan.Checked)
    {
        iPPKD = -1;
    }

    if (oLogic.Bersihkan1_dan2(m_iSKPD, iPPKD, sketDinas))
    {
        oLogic.MasukkanSurplusdefisit(m_iSKPD, iPPKD, mTanggalAwal, mTanggalAkhir, mgabunganPPKDdanBukan);
    }
    
    
    
    //ExecuteEx SSQL
           
           
////////'    SSQL = "DELETE FROM tBukubesar where inojurnal in (1,2) and year(dtTransaksi) =" & g_nTahun
////////'    SSQL = SSQL & " AND btKodeKategori =" & m_iKK & " AND btKodeUrusan =" & m_iKU & " AND btKODESKPD=" & m_iKSKPD & " AND btKodeUK =" & m_iKUK
////////'
////////'ExecuteEx SSQL

////////'    SSQL = "DELETE FROM tBukubesar where inojurnal=1 and year(dtTransaksi) =" & g_nTahun
////////'    SSQL = SSQL & " AND btKodeKategori =" & m_iKK & " AND btKodeUrusan =" & m_iKU & " AND btKODESKPD=" & m_iKSKPD & " AND btKodeUK =" & m_iKUK
           
////////    '  sKetDinas = GetKeteanganDinasEkuitas("")
////////    cEquitas = GetSurplusDefisit
////////    cEquitasLalu = GetEkuitasAwal
////////    Dim cJumlahRKSKPDTahunLalu As Currency
////////    cJumlahRKSKPDTahunLalu = GetRKSKPDTAhunLalu
    
////////    Dim IDDinas As Integer
    
      
////////    SSQL = "insert into tBukuBesar values (1," & g_nTahun & "," & m_iKK & "," & m_iKU & "," & m_iKSKPD & "," & m_iKUK & "," & m_iKK & "," & m_iKU & ",0,0," + CStr(m_KodeSurplusDefisit) + "," & SQLDateFormat(DateSerial(g_nTahun, 1, 1)) & ",-1," & Replace(CStr(cEquitas), ",", ".") & ",90,'','',90," & m_iPPKD & ",0,0,0,0)"
////////            ExecuteEx SSQL
////////    '310101010001
////////    SSQL = "insert into tBukuBesar values (2," & g_nTahun & "," & m_iKK & "," & m_iKU & "," & m_iKSKPD & "," & m_iKUK & "," & m_iKK & "," & m_iKU & ",0,0,310101010001 ," & SQLDateFormat(DateSerial(g_nTahun, 1, 1)) & ",-1," & Replace(CStr(cEquitasLalu - cJumlahRKSKPDTahunLalu), ",", ".") & ",90,'','',90," & m_iPPKD & ",0,0,0,0)"
////////            ExecuteEx SSQL
            
              

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private  void DisplayRekening(){

            try
            {
                string[] Bariskosong = { "J", ".", "", "" };
                if (m_lstRekening == null)
                {
                    LoadRekening();
                }
                gridNeraca.Rows.Clear();

                foreach(Rekening Rek in m_lstRekening){
                    if (Rek.ID == 200000000000)
                    {
                     
                        gridNeraca.Rows.Add(Bariskosong);
                        string[] rowAset2 = { CON_STRING_JUMLAHASET, ".", "", "JUMLAH ASET","","","0" };
                        gridNeraca.Rows.Add(rowAset2);

                        gridNeraca.Rows.Add(Bariskosong);
                    }
                    string[] row = { Rek.ID.ToString(), "BB", Rek.ID.ToString().ToKodeRekening(Rek.Root), Rek.Nama, "", "", Rek.Root.ToString()};
                    gridNeraca.Rows.Add(row);

                   
                }
                gridNeraca.Rows.Add(Bariskosong);
                string[] rowEKUTAS = { CON_STRING_JUMLAHEKUITAS, ".", "", "JUMLAH KUWAJIBAN  DAN EKUITAS", "", "", "0" };
                gridNeraca.Rows.Add(rowEKUTAS);
                gridNeraca.Rows.Add(Bariskosong);
            


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ctrlTahapAnggaran1_Load(object sender, EventArgs e)
        {

        }
        private List<Realisasi04AK> ProcessPerLevel(List<Realisasi04AK> lst, int level, bool isSaldoAwal= false)
        {
            string idRekening = "";
          
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
                if (lenKode == 5)
                {
                    MessageBox.Show("Test");
                }
                var lstJumlah = lst.Where(l => l.Level == 6)
                      .GroupBy(c => c.idRekening.ToString().Substring(0, lenKode))

                .Select(x => new
                {
                    IDRekening = x.Key,                    
                    Jumlah = x.Sum (y=>y.Jumlah)

                }).ToList();
                List<Realisasi04AK> lstKi = new List<Realisasi04AK>(); 

             
                     lstKi = (from t in
                                                     lstRekening
                                                 join j in lstJumlah
                                                 on t.ID.ToString().Substring(0, lenKode) equals j.IDRekening.Substring(0, lenKode)

                                                 select new Realisasi04AK
                                                {
                                                    idRekening = t.ID,
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
          private void LoadBukubesar()
        {

            try
            {
                List<Realisasi04AK> lst = new List<Realisasi04AK>();
                List<Realisasi04AK> lstRpt = new List<Realisasi04AK>();
                List<Realisasi04AK> lstInRpt = new List<Realisasi04AK>();
                RealisasiAKLogic oLogic = new RealisasiAKLogic(GlobalVar.TahunAnggaran);
                int iPPKD;
                if (chkGabunganPPKDDanBukan.Checked)
                {
                    iPPKD = -1;
                } else
                {
                    if (chkPPKD.Checked== true)
                    {
                        iPPKD = 1;
                    }
                    else
                    {
                        iPPKD = 0;
                    }
                }
                lst = oLogic.GetNeraca(m_iSKPD, mTanggalAkhir, iPPKD,chkELiminasiRK.Checked);
                int id = 0;
               
                if (lst != null)
                {
                   
                    //
                    foreach (Realisasi04AK r in lst){
                        lstRpt.Add ( r);
                  
                        foreach (Realisasi04AK rdulu in lstNeracaAwal)
                        {
                            //jika ada di lst Rekening  
                            if (r.idRekening == rdulu.idRekening)
                            {
                                if ((rdulu.idRekening < 300000000000))
                              //      if (r.idRekening != 310101010001)
                                {
                                    lstRpt[id].Jumlah = lstRpt[id].Jumlah + rdulu.Jumlah;
                                }

                                //continue;
                            }
                        }
                        id++;
                    }
                   
                }
                foreach (Realisasi04AK rdulu in lstNeracaAwal)
                {
                    bool bFound = false;
                    bFound = false;
                    if (rdulu.idRekening == 130101010001)
                    {
                        bFound = false;
                    }

                    if (rdulu.idRekening < 300000000000)
                    {
                        Realisasi04AK r = lstRpt.FirstOrDefault(x => x.idRekening == rdulu.idRekening);
                        if (r == null)
                        {

                            lstRpt.Add(rdulu);
                        }
                    }
             
                 
                }

                int mx_Level = GetLevelRekening();
                if (GetLevelRekening() == 6)
                {

                    mx_Level = 5;
                }

           
                if (lstRpt != null)
                {
                    for (int l = 1; l <= mx_Level; l++)
                    {
                        List<Realisasi04AK> lstTambahan = ProcessPerLevel(lstRpt, l);
                        if (lstTambahan != null)
                        {
                            foreach (Realisasi04AK v in lstTambahan)
                            {
                                if (l == 1)
                                {
                                    if (v.idRekening == 100000000000)
                                    {
                                        JumahAset = v.Jumlah;
                                    }
                                    if (v.idRekening == 200000000000)
                                    {
                                        JumahKewajiban = v.Jumlah;
                                    }
                                    if (v.idRekening == 300000000000)
                                    {
                                        JumahEkuitas = v.Jumlah;
                                    }


                                }
                                lstInRpt.Add(v);
                            }
                        }
                    }

                }
                if (GetLevelRekening() == 6)
                {

                    foreach (Realisasi04AK v in lst)
                    {
                        Rekening r = m_lstRekening.FirstOrDefault(x => x.ID == v.idRekening);
                        if (r!=null)
                        {
                            v.Jumlah = v.Jumlah * r.Debet;
                        }
                        lstInRpt.Add(v);
                    }
                }
               
                List<Realisasi04AK> lstAkhir = new List<Realisasi04AK>();
                foreach (Realisasi04AK r in lstInRpt.OrderBy(x => x.idRekening))
                {
                 
                    lstAkhir.Add(r);
                }
   
                 foreach (Realisasi04AK r in lstAkhir)
                {
                    for (int idx = 0; idx < gridNeraca.Rows.Count; idx++)
                    {

                        
                   
                        if (DataFormat.GetLong(gridNeraca.Rows[idx].Cells[COL_IDREKEINING].Value) == r.idRekening)
                        {
                            gridNeraca.Rows[idx].Cells[COL_JUMLAHKINI].Value = r.Jumlah.ToRupiahInReport();
                       
                        }
                    }
                }
      
                var notInAkhir = lstNeracaAwal.Where(p => !lstAkhir.Any(p2 => p2.idRekening == p.idRekening));
                foreach (Realisasi04AK rDulu in notInAkhir)
                {
                  
                    for (int idx = 0; idx < gridNeraca.Rows.Count; idx++)
                    {
                        if (DataFormat.GetLong(gridNeraca.Rows[idx].Cells[COL_IDREKEINING].Value) == rDulu.idRekening)
                        {
                            gridNeraca.Rows[idx].Cells[COL_JUMLAHKINI].Value = rDulu.Jumlah.ToRupiahInReport();

                        }
                    }

                }
                for (int r = 0; r < gridNeraca.Rows.Count; r++)
                {
                    if (DataFormat.GetString(gridNeraca.Rows[r].Cells[0].Value) == CON_STRING_JUMLAHASET)
                    {
                     
                        gridNeraca.Rows[r].Cells[COL_JUMLAHKINI].Value = JumahAset.ToRupiahInReport();
                    }
                 
                    if (DataFormat.GetString(gridNeraca.Rows[r].Cells[0].Value) == CON_STRING_JUMLAHEKUITAS)
                    {
                    
                        gridNeraca.Rows[r].Cells[COL_JUMLAHKINI].Value = (JumahKewajiban + JumahEkuitas).ToRupiahInReport();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateSaldoAwalTahunBerikutnya()
        {

            try
            {
                List<Realisasi04AK> lst = new List<Realisasi04AK>();
                List<Realisasi04AK> lstRpt = new List<Realisasi04AK>();
                List<Realisasi04AK> lstInRpt = new List<Realisasi04AK>();
                RealisasiAKLogic oLogic = new RealisasiAKLogic(GlobalVar.TahunAnggaran);

                lst = oLogic.GetNeraca(m_iSKPD, mTanggalAkhir,m_iPPKD, chkELiminasiRK.Checked);
                              
                int id = 0;

                if (lst != null)
                {
                    bool bFound = false;
                    
                    foreach (Realisasi04AK r in lst){
                        lstRpt.Add ( r);
                 
                        foreach (Realisasi04AK rdulu in lstNeracaAwal)
                        {
                            //jika ada 
                            if (r.idRekening == rdulu.idRekening)
                            {
                                //if ((r.idRekening < 300000000000))
                                    if (r.idRekening == 310101010001)
                                {
                                    lstRpt[id].Jumlah = lstRpt[id].Jumlah + rdulu.Jumlah;
                                }

                                //continue;
                            }
                        }
                        id++;
                    }
                    foreach (Realisasi04AK rdulu in lstNeracaAwal)
                    {
                        bFound = false;
                        foreach (Realisasi04AK r in lst)
                        {
                            //jika ada 
                            if (r.idRekening == rdulu.idRekening)
                            {
                            
                                bFound = true ;
                                continue;
                            }
                        }
                        if (bFound == false && rdulu.idRekening < 300000000000)
                        {
                            lstRpt.Add(rdulu);
                        }
                    }
                }


                SaldoAwalRehLogic oSALogic = new SaldoAwalRehLogic(GlobalVar.TahunAnggaran);
                foreach (Realisasi04AK v in lstRpt)
                {

                    SaldoAwalRek sa = new SaldoAwalRek();
                    sa.IDRekening = v.idRekening;
                    sa.Tanggal = new DateTime(GlobalVar.TahunAnggaran, 12, 31);
                    sa.Jumlah = v.Jumlah < 0 ? -1 * v.Jumlah : v.Jumlah;
                    sa.Debet = v.Jumlah > 0 ? 1 : -1;
                    sa.IDDinas = m_iSKPD;
                    if (sa.IDRekening == 210601010017)
                    {
                  //     MessageBox.Show(sa.IDRekening.ToString());
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
        private void LoadneracaAwal(bool ForSaldoAwal=false)
        {
            try
            {
                lstNeracaAwal = new List<Realisasi04AK>();
                List<Realisasi04AK> lstInRpt = new List<Realisasi04AK>();
                RealisasiAKLogic oLogic = new RealisasiAKLogic(GlobalVar.TahunAnggaran);

                lstNeracaAwal = oLogic.GetSaldoWAwalNeraca(m_iSKPD);
                if (lstNeracaAwal == null)
                {
                    MessageBox.Show(oLogic.LastError());
                }
                int mx_Level = GetLevelRekening();
                if (GetLevelRekening() == 6)
                {

                    mx_Level = 5;
                }

                if (lstNeracaAwal != null)
                {
                    for (int l = 1; l <= mx_Level; l++)
                    {
                        List<Realisasi04AK> lstTambahan = ProcessPerLevel(lstNeracaAwal, l);
                        if (lstTambahan != null)
                        {
                            foreach (Realisasi04AK v in lstTambahan)
                            {
                                if (l == 1)
                                {
                                    if (v.idRekening == 100000000000)
                                    {
                                        JumahAset = v.Jumlah;
                                    }
                                    if (v.idRekening == 200000000000)
                                    {
                                        JumahKewajiban = v.Jumlah;
                                    }
                                    if (v.idRekening == 300000000000)
                                    {
                                        JumahEkuitas = v.Jumlah;
                                    }
                               

                                }
                                lstInRpt.Add(v);
                            }
                        }
                    }

                }

             
                if (GetLevelRekening() == 6)
                {
                    foreach (Realisasi04AK v in lstNeracaAwal)
                    {
                        lstInRpt.Add(v);
                    }
                 

                }
                if (ForSaldoAwal == true)
                    return;

                lstInRpt.OrderBy(x => x.idRekening);

                for (int i = 0; i < lstInRpt.Count; i++)
                {
                    for (int idx = 0; idx < gridNeraca.Rows.Count; idx++)
                    {
                        if (DataFormat.GetLong(gridNeraca.Rows[idx].Cells[COL_IDREKEINING].Value) == lstInRpt[i].idRekening)
                        {
                            gridNeraca.Rows[idx].Cells[COL_JUMLAHLALU].Value = lstInRpt[i].Jumlah.ToRupiahInReport();
                         //   gridNeraca.Rows[idx].Cells[COL_JUMLAHBAYANGANLALU].Value = 0;// lstInRpt[i].Jumlah.ToString();
                            
                        }
                    }
                }

                for (int r = 0; r < gridNeraca.Rows.Count; r++)
                {
                    if (DataFormat.GetString(gridNeraca.Rows[r].Cells[0].Value) == CON_STRING_JUMLAHASET)
                    {
                      //  gridNeraca.Rows[r].Cells[COL_JUMLAHBAYANGANLALU].Value = 0;// JumahAset.ToString();
                        gridNeraca.Rows[r].Cells[COL_JUMLAHLALU].Value = JumahAset.ToRupiahInReport();
                    }
                    //if (DataFormat.GetString(gridNeraca.Rows[r].Cells[0].Value) == CON_STRING_JUMLAHKEWAJIBAN)
                    //{
                    //    gridNeraca.Rows[r].Cells[COL_JUMLAHLALU].Value = JumahKewajiban.ToRupiahInReport();
                    //}
                    if (DataFormat.GetString(gridNeraca.Rows[r].Cells[0].Value) == CON_STRING_JUMLAHEKUITAS)
                    {
                    //    gridNeraca.Rows[r].Cells[COL_JUMLAHBAYANGANLALU].Value = 0;// (JumahKewajiban + JumahEkuitas).ToString();
                        gridNeraca.Rows[r].Cells[COL_JUMLAHLALU].Value = (JumahKewajiban + JumahEkuitas).ToRupiahInReport();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            if (sRet.Length == 0)
            {
                MessageBox.Show("Nama File Tidak boleh kosong..");
                return "";

            }

            //  }
            return sRet;
        }
        private void KillSpecificExcelFileProcess(string excelFileName)
        {
            var processes = from p in Process.GetProcessesByName("EXCEL")
                            select p;

            foreach (var process in processes)
            {
                if (process.MainWindowTitle == "Microsoft Excel - " + excelFileName)
                    process.Kill();
            }
        }
        private void HilangkanBarisKosong()
        {
            for (int idx = gridNeraca.Rows.Count-2; idx >= 0; idx--)
            {
                if (DataFormat.FormatUangReportKeDecimal(gridNeraca.Rows[idx].Cells[COL_JUMLAHKINI].Value) ==0 &&
                    DataFormat.FormatUangReportKeDecimal(gridNeraca.Rows[idx].Cells[COL_JUMLAHLALU].Value) == 0 &&
                    DataFormat.GetString(gridNeraca.Rows[idx].Cells[0].Value) != "J" 
                    )
                {
                    gridNeraca.Rows.RemoveAt(idx);
                }
            }
        }
        private void ctrlTanggalBulanVertikal1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SKPDLogic oLogic = new SKPDLogic(GlobalVar.TahunAnggaran);
            List<SKPD> lstSKPD = oLogic.Get(GlobalVar.TahunAnggaran);
            int poss = 2;
            foreach (SKPD skpd in lstSKPD)
            {
                ctrlDinas1.SetID(skpd.ID);
                Panggil();
                Export(poss);
                poss = poss + 2;
            }
            //Workbook wb = new Workbook("workbook.xlsx");

            //// Dapatkan referensi lembar kerja
            //Worksheet sheet = wb.Worksheets[0];

            //// Hapus 2 baris pada indeks 1
            //sheet.Cells.DeleteRows(1, 2);

            //// Simpan file yang diperbarui
            //wb.Save("updated_workbook.xlsx");
        }

        private void cmdJadikanSaldoAwal_Click(object sender, EventArgs e)
        {
            if (GetLevelRekening() == 0)
            {
                MessageBox.Show("Belum memilih Level Rekening.");
                return;
            }

            if (chkSemuaDinas.Checked == false)
            {
                if (ctrlDinas1.GetID() == 0)
                {
                    MessageBox.Show("Belum memilih OPD.");
                    return;
                }
                m_iSKPD = ctrlDinas1.GetID();
            }
            else
            {
                m_iSKPD = 0;
            }
            JumahAset = 0;
            JumahKewajiban = 0;
            JumahEkuitas = 0;


            m_iPPKD = GetPPKD();
            mTanggalAwal = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
            mTanggalAkhir = new DateTime(GlobalVar.TahunAnggaran, 12, 31);
            // Untk ekitas

            ProsessEkuitas();
            if (LoadRekening() == true)
            {
                DisplayRekening();

                LoadneracaAwal(true);

           
                UpdateSaldoAwalTahunBerikutnya();

            }

            HilangkanBarisKosong();
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            string namaFile = "";
            namaFile = BuatFile();

            KillSpecificExcelFileProcess(namaFile);
            if (namaFile.Length == 0)
            {
                return;
            }
            try
            {
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(_objWorkSheet);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(_objWorkBook);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(_objWorkBooks);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(_objAppln);
                //_objWorkSheet = null; _objWorkBooks = null; _objWorkBooks = null; _objAppln = null;


                Microsoft.Office.Interop.Excel.Application excel;
                Microsoft.Office.Interop.Excel.Workbook excelworkBook;
                Microsoft.Office.Interop.Excel.Worksheet excelSheet;
                Microsoft.Office.Interop.Excel.Range excelCellrange;
                // Start Excel and get Application object.
                excel = new Microsoft.Office.Interop.Excel.Application();
                while (excel.Interactive == true)
                {
                    // If Excel is currently busy, try until go thru
                    excel.Interactive = false;
                }

                // Make Excel invisible and disable alerts.
                excel.Visible = false;
                excel.DisplayAlerts = false;

                // Create a new Workbook.
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Create a Worksheet.
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;

                excelSheet.Name = "Neraca ";
                // header
                for (int ix = 1; ix < gridNeraca.Columns.Count + 1; ix++)
                {
                    excelSheet.Cells[1, ix] = gridNeraca.Columns[ix - 1].HeaderText;
                }


                int i = 0;
                bool bFound = false;
                for (int row = 0; row < gridNeraca.Rows.Count; row++)
                {
                    for (int col = 0; col < gridNeraca.Columns.Count; col++)
                    {
                        if (DataFormat.GetInteger(gridNeraca.Rows[row].Cells[COL_JUMLAHBAYANGANLALU].Value) == 6)
                        {
                            if (col >= 3)
                            {
                                excelSheet.Cells[i + 2, col + 1] = DataFormat.GetString(gridNeraca.Rows[row].Cells[col].Value).ReplaceUnicode();  //DataFormat.FormatUangReportKeDecimal(gridNeraca.Rows[row].Cells[col].Value).ToString();
                            }
                            else
                            {
                                excelSheet.Cells[i + 2, col + 1] = DataFormat.GetString(gridNeraca.Rows[row].Cells[col].Value).ReplaceUnicode();
                            }
                            bFound = true;
                        }

                    }
                    if (bFound)
                        i++;
                    bFound = false;
                }
             

                // now we resize the columns
                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[excelSheet.Rows.Count, excelSheet.Columns.Count]];
                //excelCellrange.EntireColumn.AutoFit();
                //excelSheet.Range (“G:G”).NumberFormat = “0.00”;
                //Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
                //border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //border.Weight = 2d;

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

                MessageBox.Show("Gagal export ke excell " + namaFile + "   " + ex.Message);
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
                if (m_iJenisBendahara == 2)
                {
                    bendahara = ctrlDinas1.GetBendaharaPengeluaran(ctrlTanggal1.Tanggal);
                }
                else
                {
                    bendahara = ctrlDinas1.GetBendaharaPenerimaan(ctrlTanggal1.Tanggal);
                }
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
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "LAPORAN NERACA", 10, kiri, yPos,
                page.GetClientSize().Width, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "UNTUK TAHUN YANG BERAKHIR SAMPAI DENGAN " + ctrlTanggalBulanVertikal1.TanggalAkhir.ToTanggalIndonesia(), 10, kiri, yPos,
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
                table.Columns.Add("Tahun  " + (GlobalVar.TahunAnggaran-1).ToString());
             


                int columnCount = table.Columns.Count;
                List<object> data = new List<object>();


           

                for (int idx = 0; idx < gridNeraca .Rows.Count; idx++)
                {
                  
                    table.Rows.Add(new string[]
                    {
                        
                       DataFormat.GetString(gridNeraca.Rows[idx].Cells[COL_KDEREKEINING].Value),                      
                       DataFormat.GetString(gridNeraca.Rows[idx].Cells[COL_NAMAREKEINING].Value),
                       DataFormat.GetString(gridNeraca.Rows[idx].Cells[COL_JUMLAHKINI].Value),
                       DataFormat.GetString(gridNeraca.Rows[idx].Cells[COL_JUMLAHLALU].Value),
                    
                  
                    });
                }

                pdfGrid.DataSource = table; //data
                pdfGrid.Columns[0].Width = 80;
                pdfGrid.Columns[1].Width = 250;
                pdfGrid.Columns[2].Width = 90;
                pdfGrid.Columns[3].Width = 90;
            

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
                

                //formatKolomTengah.Alignment = PdfTextAlignment.Center;
                //pdfGrid.Columns[5].Format = formatKolomTengah;










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
                           "Ketapang, " +  ctrlTanggal1.TextTanggalLengkap , 9, kiri + setengah, yPos,
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

                using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../LRA.pdf"), FileMode.Create, FileAccess.ReadWrite))
                {
                    //Save the PDF document to file stream.
                    document.Save(outputFileStream);

                }

                //Close the document.
                document.Close(true);
                pdfViewer pV = new pdfViewer();
                pV.Document = Path.GetFullPath(@"../../../LRA.pdf");
                pV.Show();


                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void cmdSaldoBKU_Click(object sender, EventArgs e)
        {

            BKULogic oLogic = new BKULogic(GlobalVar.TahunAnggaran);
            m_iSKPD= ctrlDinas1.GetID();
              m_iPPKD = GetPPKD();
                mTanggalAwal = ctrlTanggalBulanVertikal1.TanggalAwal;
               mTanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;
                // Untk ekitas
            List<BKUDISPLAY> lstBKU = new List<BKUDISPLAY>();
            txtSaldoBKU.Text = "0";
            decimal saldo = oLogic.GetSaldo(m_iSKPD, 2, mTanggalAwal, mTanggalAkhir);
            txtSaldoBKU.Text = saldo.ToRupiahInReport();


        }

        private void cmdCek_Click(object sender, EventArgs e)
        {
            frmPerbandingan fbanding = new frmPerbandingan();
            fbanding.Show();
        }

        private void chkELiminasiRK_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmdDirektori_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            lblFolderPath.Text = dialog.SelectedPath;

        }

        private void gridNeraca_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < gridNeraca.Rows.Count)
                {

                    if (e.ColumnIndex == 7)
                    {
                        frmBukuBesarDlg fBukubesarDlg = new frmBukuBesarDlg();
                        fBukubesarDlg.SKPD = m_iSKPD;
                        fBukubesarDlg.Tanggal = mTanggalAkhir;
                        fBukubesarDlg.IDRekening = DataFormat.GetLong(DataFormat.GetString(gridNeraca.Rows[e.RowIndex].Cells[2].Value).Replace(".", ""));
                        fBukubesarDlg.NamaRekening = DataFormat.GetString(gridNeraca.Rows[e.RowIndex].Cells[3].Value);

                        if (lblFolderPath.Text == "" || lblFolderPath.Text == "label4")
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void ExcellKanBukuBesar(int i)
        {
            frmBukuBesarDlg fBukubesarDlg = new frmBukuBesarDlg();
            fBukubesarDlg.SKPD = m_iSKPD;
            fBukubesarDlg.Tanggal = mTanggalAkhir;
            fBukubesarDlg.IDRekening = DataFormat.GetLong(DataFormat.GetString(gridNeraca.Rows[i].Cells[2].Value).Replace(".", ""));
            fBukubesarDlg.NamaRekening = DataFormat.GetString(gridNeraca.Rows[i].Cells[3].Value);

            if (lblFolderPath.Text == "" || lblFolderPath.Text == "label4")
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
        private void cmdExcellkanBukuBesar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridNeraca.Rows.Count; i++)
            {
                if (DataFormat.GetString(gridNeraca.Rows[i].Cells[2].Value).Trim().Length > 14)
                {
                    ExcellKanBukuBesar(i);

                }

            }
        }
    }
}
