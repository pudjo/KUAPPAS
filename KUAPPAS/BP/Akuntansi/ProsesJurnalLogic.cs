using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DTO.Bendahara;
using Formatting;
using BP;
using DTO.Akuntansi;


//alter table tJurnal  add
//IDDInas int

//alter table tJurnalRekening add
//IDUrusan int,
//IDProgran int,
//IDKegiatan int,
//IDSUbKegiatan bigint


namespace BP.Akuntansi
{
    
        //static public string NanaDaerah;
        public enum JENIS_TABLE{
            TABLE_STS = 1,
            TABLE_SPP = 2,
            TABLE_SETOR = 3,
            TABLE_PANJAR = 4,
            TABLE_SKR = 5,
            TABLE_BAST = 6,
            TABLE_ASET = 7,
            TABLE_KOREKSI = 8,
        }
   
   public class ProsesJurnalLogic: BP
    {

       private const string m_sNamatabelJurnalRekening = "TJURNALREKENING";
       private const string m_sNamatabelJurnal = "TJURNAL";

        private const int SETOR_STS = 1;
        private const int SETOR_UYHD = 2;
        private const int SETOR_CP = 3;
        private const int SETOR_PAJAK = 4;
        private const int TERIMA_SETOR_PAJAK = 5;
       private const long m_KasBendaharaPenerimaan=  110102010001;
       private const long m_KasBendaharaPengeluaran = 110103010001;
       private const long m_KasBendaharaRKPPKD = 111301010001;
       private const long m_sRekeningRKSKPD = 111301010001;
        private const string INESRT_TJURNAL= " INSERT INTO tJurnal  (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, btJenisJurnal,iSumber, iJenisSumber, bPPKD,iKelompok,btUrut, bPotongan,bFRomSKPD ) ";
    //   private const string INSERT_TJURNALREKENING = "INSERT INTO tJurnalRekening (iNoJurnal,IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan, IDSubKegiatan,  iIDRekening, iNoUrut, iDebet, cJumlah,  iKelompok)";

       private const int PEMBAGI_DINAS_KE_URUSAN =100000;

       //Private m_rsKasBendahara As ADODB.Recordset
//Private m_lCountKasBendahara As Long

            private long m_iRekKasda ;
            private long m_iRekSILPATB;
            private long  m_iRekRKPPKD ;
            private long  m_iRekSILPA ;
          

            private long  m_KodeKategoriPPKD ;
            private long  m_KodeUrusanPPKD ;
            private long  m_KodeSKPDPPKD ;
            private long  m_KodeUKPPKD ;
            private long  m_iKodeEkuitas;

            private long m_iKodeEstimasiPadaSAL ;

            private bool  m_iAlsoForBUD ;

           private int m_IDDInas ;
       Kasda m_oKasda;

//Private m_arNamaTable(3) As String


       public ProsesJurnalLogic(int _pTahun, int dinas )
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_IDDInas = dinas;
            Kasda oKasda = new Kasda();
            KasdaLogic oKasdaLogic = new KasdaLogic(_pTahun);
            oKasda = oKasdaLogic.Get();

            if (oKasdaLogic.IsError() == false)
            {
                m_iRekKasda = oKasda.RekKasda;
                m_iRekSILPATB = oKasda.RekSILPATB;
                m_iRekRKPPKD = oKasda.RekRKPPKD;
                m_iRekSILPA = oKasda.RekSILPA;

                m_iKodeEstimasiPadaSAL = oKasda.KodeEstimasiPadaSAL;// IIf(IsNull(rsKasda!IIDRekEstimasiSAL), 0, rsKasda!IIDRekEstimasiSAL)
                m_iKodeEkuitas = 31101001;
            }
            else {
                _lastError = oKasdaLogic.LastError();
              
            }
           


        }

       public ProsesJurnalLogic(int _pTahun, Kasda oKasda, int dinas )
            : base(_pTahun)
        {

                 
           Tahun = _pTahun;           
                
           m_iRekKasda = oKasda.RekKasda; //m_oKasda.Re  IIf(IsNull(rsKasda!iIDRekeningBUD), 0, rsKasda!iIDRekeningBUD)
           m_iRekSILPATB =oKasda.RekSILPATB; //IIf(IsNull(rsKasda!iIDRekeningSilpaTB), 0, rsKasda!iIDRekeningSilpaTB)
           m_iRekRKPPKD =oKasda.RekRKPPKD;// IIf(IsNull(rsKasda!iiDRekeningRKPPKD), 0, rsKasda!iiDRekeningRKPPKD)
           m_iRekSILPA =oKasda.RekSILPA; //IIf(IsNull(rsKasda!iIDRekeningSilpa), 0, rsKasda!iIDRekeningSilpa)
          
           m_iKodeEstimasiPadaSAL =oKasda.KodeEstimasiPadaSAL;// IIf(IsNull(rsKasda!IIDRekEstimasiSAL), 0, rsKasda!IIDRekEstimasiSAL)
           m_iKodeEkuitas = 31101001;
           
           m_IDDInas = dinas;


        }
      
       

       public string GetNextNo(){
           long sNoBaru = 0;
           sNoBaru = ReadNo(E_KOLOM_NOURUT.CON_URUT_JURNAL, m_IDDInas);

           return sNoBaru.ToString();
       }
       private bool JurnalUPGU(string sNourut)
       {

           //     string  SSQL As String
           string sNoJurnal;
           try
           {

               sNoJurnal = GetNextNo();
               SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                         "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, sUraian  ) SELECT " +
                         sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                       ", " + GetCurrentDate() + " AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoSP2D as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN) + " as btJenisJurnal," +
                       "dtbukukas as  dtTanggalBukti,  sNoSP2D as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SP2D).ToString() + "  as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 1 as ikelompok , 0 as bPotongan, sPeruntukan as sUraian   FROM tSPP  WHERE inourut =" + sNourut;
               _dbHelper.ExecuteNonQuery(SSQL);

               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDUrusan, IDProgram, IDKegiatan, " +
                     "IDSubKegiatan,  iIDRekening, iNoUrut, iDebet, cJumlah,  iKelompok) SELECT " + sNoJurnal + " as inojurnal, " +
                    " tSPP.IDUrusan,  0 as IDProgram , 0 as IDKegiatan , " +
                     "mKasBendahara.IIDrekening AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSPP.cJumlah,'Jurnal SP2D no  ' + sNoSP2D, 1 as iKelompok FROM tSPP INNER JOIN mKasBendahara " +
                     " ON tSPP.IDDInas = mKasBendahara.IDDinas AND tSPP.bPPKD = mkasbendahara.bPPKD WHERE tSPP.inourut = " + sNourut +
                     " AND mkasbendahara.btJenis=2 ";

               _dbHelper.ExecuteNonQuery(SSQL);


               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan,iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori" +
                     ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,tSPP.btKodekategori as btKodeKategoriPelaksana " +
                     ",tSPP.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                     m_iRekRKPPKD + " AS iiDRekening , 1 as iNoUrut , -1 As iDebet , cJumlah,'Jurnal SPP no  ' + sNoSP2D, 1 as Ikelompok FROM tSPP WHERE inourut = " + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);
               return true;
           }
           catch (Exception ex)
           {
               _isError = true;
               _lastError = ex.Message + "\n " + SSQL;
               
               return false;

           }
       }
       public bool BersihkanJurnal(string sNourut, int bPotongan)
       {
           try
           {
               //if (bppkd == 0)
               //{
               SSQL = "DELETE tBukubesar FROM tBukubesar INNER JOIN tJurnal ON tJurnal.inoJurnal= tBukubesar.inojurnal WHERE tJurnal.iSumber= " + sNourut + " and isnull(tBukubesar.bPotongan,0)=" + bPotongan.ToString();// +" AND tBukuBesar.bPPKD=0 and isnull(tBukubesar.bPotongan,0)=" + bPotongan.ToString();
                   _dbHelper.ExecuteNonQuery(SSQL);
                   SSQL = "DELETE tJurnalRekening FROM tJurnalRekening INNER JOIN tJurnal ON tJurnal.inoJurnal= tJurnalRekening.inojurnal WHERE tJurnal.iSumber= " + sNourut + " and  isnull(tJurnal.bPotongan,0)=" + bPotongan.ToString(); // +" AND tJurnal.bPPKD=0 and  isnull(tJurnal.bPotongan,0)=" + bPotongan.ToString();
                   _dbHelper.ExecuteNonQuery(SSQL);
                   SSQL = "DELETE FROM tJurnal WHERE tJurnal.iSumber= " + sNourut + " and  isnull(tJurnal.bPotongan,0)=" + bPotongan.ToString(); ;// +" AND bPPKD=0 
                   _dbHelper.ExecuteNonQuery(SSQL);
                   SSQL = "DELETE tBukubesar FROM tBukubesar INNER JOIN tJurnal ON tJurnal.inoJurnal= tBukubesar.inojurnal WHERE tJurnal.iSumber= " + sNourut + " and isnull(tBukubesar.bPotongan,0)=" + bPotongan.ToString();// +" AND tBukuBesar.bPPKD=1 and isnull(tBukubesar.bPotongan,0)=" + bPotongan.ToString();
                   _dbHelper.ExecuteNonQuery(SSQL);
                   SSQL = "DELETE tJurnalRekening FROM tJurnalRekening INNER JOIN tJurnal ON tJurnal.inoJurnal= tJurnalRekening.inojurnal WHERE tJurnal.iSumber= " + sNourut +  " and  isnull(tJurnal.bPotongan,0)=" + bPotongan.ToString();// +" AND tJurnal.bPPKD=1  and  isnull(tJurnal.bPotongan,0)=" + bPotongan.ToString();
                   _dbHelper.ExecuteNonQuery(SSQL);
                   SSQL = "DELETE FROM tJurnal WHERE tJurnal.iSumber= " + sNourut + "  and  isnull(tJurnal.bPotongan,0)=" + bPotongan.ToString() + " and  isnull(tJurnal.bPotongan,0)=" + bPotongan.ToString(); // +" AND bPPKD=1  and  isnull(tJurnal.bPotongan,0)=" + bPotongan.ToString();
                   _dbHelper.ExecuteNonQuery(SSQL);
               //}
               //else
               //{


               //    SSQL = "DELETE tBukubesar FROM tBukubesar INNER JOIN tJurnal ON tJurnal.inoJurnal= tBukubesar.inojurnal WHERE tJurnal.iSumber= " + sNourut + " AND tBukuBesar.bPPKD=1 and isnull(tBukubesar.bPotongan,0)=" + bPotongan.ToString();
               //    _dbHelper.ExecuteNonQuery(SSQL);
               //    SSQL = "DELETE tJurnalRekening FROM tJurnalRekening INNER JOIN tJurnal ON tJurnal.inoJurnal= tJurnalRekening.inojurnal WHERE tJurnal.iSumber= " + sNourut + " AND tJurnal.bPPKD=1  and  isnull(tJurnal.bPotongan,0)=" + bPotongan.ToString();
               //    _dbHelper.ExecuteNonQuery(SSQL);
               //    SSQL = "DELETE FROM tJurnal WHERE tJurnal.iSumber= " + sNourut + " AND bPPKD=1  and  isnull(tJurnal.bPotongan,0)=" + bPotongan.ToString();
               //    _dbHelper.ExecuteNonQuery(SSQL);
               //}
               return true;
           }
           catch (Exception ex)
           {
               _lastError = ex.Message;
               return false;
           }

       }
       public bool HapusJurnalBukuBesarOfRekening(long idRekening, int dinas)
       {
           try
           {


               SSQL = "DELETE tJurnalRekening FROM tJurnalRekening inner join tJurnal on tJurnal.inoJurnal = tJurnalRekening.inojurnal " +
                   " inner join  tBukubesar on tBukubesar.iSumber= tJurnal.iSumber  WHERE tBukubesar.IDDInas= "+ dinas.ToString() +" and tBukubesar.IIDrekening= " + idRekening.ToString();
               _dbHelper.ExecuteNonQuery(SSQL);

               SSQL = "DELETE tJurnal FROM tJurnal inner join  tBukubesar on tBukubesar.iSumber= tJurnal.iSumber  WHERE tBukubesar.IDDInas= " + dinas.ToString() + " and  tBukubesar.IIDrekening= " + idRekening.ToString();
               _dbHelper.ExecuteNonQuery(SSQL);


               SSQL = "DELETE tBukubesar FROM tBukubesar where iSumber in (select iSumber from tBukubesar WHERE tBukubesar.IDDInas= " + dinas.ToString() + " and tBukubesar.IIDrekening= " + idRekening.ToString() + ")";

               _dbHelper.ExecuteNonQuery(SSQL);

               
            
               return true;
           }
           catch (Exception ex)
           {
               _lastError = ex.Message;
               return false;
           }

       }
       public bool Posting()
       {
           try
           {
               if (m_IDDInas == 0)
               {
                   _lastError = "Kode OPD belum diberi nilai...";
                   return false;
               }

               SSQL = "DELETE tBukubesar where iddinas = " + m_IDDInas.ToString();
               _dbHelper.ExecuteNonQuery(SSQL);

               //SSQL = "INSERT INTO tBUKUBESAR (iNoJurnal, iTahun, btKodekategori, btKodeUrusan,btKodeSKPD,btKodeUK, " +
               // "btKodekategoriPelaksana,btKodeUrusanPelaksana, btIdProgram,btIDKegiatan, IIDRekening,  dttransaksi, " +
               // " iDebet,cJumlah,btJenisJurnal,sNoBukti,sKeterangan,iJenisSumber,iSumber,bPPKD " +
               // ",btIDSubKegiatan,bPotongan,  IDDInas, UnitAnggaran, IDUrusan, IDProgram, IDKegiatan, IdSubKegiatan)Select A.iNoJurnal, A.iTahun, " +
               // "dbo.ToKodeKategori(B.IDDInas) as btKodeKategori ," +
               // " dbo.ToKodeUrusan(A.IDDInas) as btKodeUrusan," +
               // "dbo.ToKodeSKPD(A.IDDInas) as btKodeSKPD," +
               //  "  b.btKodeUK, " +
               // "dbo.ToKodeKategoriPelaksana(B.IDUrusan) as btKodekategoriPelaksana, " +
               // "dbo.ToKodeUrusanPelaksana(B.IDUrusan) as btKodeUrusanPelaksana, " +
               // "dbo.ToKodeProgram(IdProgram) as btIdProgram," +
               // "dbo.ToKodeKegiatan(B.IDKegiatan) as btIDKegiatan, B.IIDRekening, A.dtTanggalBukti as dttransaksi, " +
               // " B.iDebet,b.cJumlah,A.btJenisJurnal,A.sNoBukti,Convert(varchar(200),B.sKeterangan) as sKeterangan,A.iJenisSumber,A.iSumber,A.bPPKD " +
               // ",dbo.ToKodeSubKegiatan(B.IDSUbKegiatan)  as btIDSubKegiatan,bPotongan, A.IDDInas, B.UnitAnggaran, B.IDUrusan, B.IDProgram," +
               // " B.IDKegiatan, B.IdSubKegiatan FROM tJurnal A INNER JOIN tJurnalRekening B ON A.inojurnal=b.inojurnal WHERE  iTahun=" + Tahun.ToString() + " AND A.IDDInas = " + m_IDDInas.ToString();

               SSQL = "INSERT INTO tBUKUBESAR (iNoJurnal, iTahun, btKodeUK, " +
                " IIDRekening,  dttransaksi, " +
                " iDebet,cJumlah,btJenisJurnal,sNoBukti,sKeterangan,iJenisSumber,iSumber,bPPKD ," +
                "bPotongan,  IDDInas, UnitAnggaran, IDUrusan, IDProgram, IDKegiatan, IdSubKegiatan)" +
                " Select A.iNoJurnal, A.iTahun, " +
                "  b.btKodeUK, " +
                 " B.IIDRekening, A.dtTanggalBukti as dttransaksi, " +
                " B.iDebet,b.cJumlah,A.btJenisJurnal,A.sNoBukti,Convert(varchar(200),B.sKeterangan) as sKeterangan,A.iJenisSumber,A.iSumber,A.bPPKD, " +
                "bPotongan, A.IDDInas, B.UnitAnggaran, B.IDUrusan, B.IDProgram," +
                " B.IDKegiatan, B.IdSubKegiatan FROM tJurnal A INNER JOIN tJurnalRekening B ON A.inojurnal=b.inojurnal WHERE  iTahun=" + Tahun.ToString() + " AND A.IDDInas = " + m_IDDInas.ToString();

               // " and a.isumber in (select inourut from Realisasi04AK where iTahun=" + Tahun.ToString() + " AND IDDInas = " + m_IDDInas.ToString()  +") ";

               _dbHelper.ExecuteNonQuery(SSQL);

               return true;



           }
           catch (Exception ex)
           {
               _lastError = ex.Message;
               return false;
           }

       }
       public  bool JurnalInThread(JurnalData data)
       {
           return Jurnal(data.Nourut, data.Table, data.iJenisSPP, data.bPotongan, data.bppkd, data.bTHL, data.fromSKPD); 

       }
       //private  bool Jurnal(long NoUrut,,int bppkd)
       public bool Jurnal (string sNourut , JENIS_TABLE iTable, int  iJenisSPP = -1, int bPotongan = 0, int bppkd = 0, bool bTHL = false, int fromSKPD = 0)
       {


           if (BersihkanJurnal(sNourut, bPotongan) == false)
           {
               return false;
           }
         
         

         switch (iTable){
             case  JENIS_TABLE.TABLE_STS:
                 if (iJenisSPP ==0)
                     ProcessJurnalSTS(sNourut);
                 else
                     ProcessJurnalSTSLangsung(sNourut);
                 break;
                        
             case JENIS_TABLE.TABLE_SPP :
                    ProcessJurnalSPP (sNourut, iJenisSPP, bTHL, fromSKPD);
                    break;
                        
             case JENIS_TABLE.TABLE_SETOR:
                   if (bPotongan == 1)
                    {
                        ProcessJurnalSetorPajak(sNourut);
                    }
                    else
                        ProcessJurnalSETOR(sNourut, iJenisSPP, fromSKPD);
                    break;                      
             case JENIS_TABLE.TABLE_PANJAR:
                
                    
                        if (bPotongan == 1) 
                           ProcessJurnalPotonganPANJAR13 (sNourut);
                         
                        else
                            ProcessJurnalPANJAR13 (sNourut, fromSKPD);
                    
                    break;
             case JENIS_TABLE.TABLE_SKR:
                        break;
             case JENIS_TABLE.TABLE_BAST:
                        ProcessJurnalBAST(sNourut);
                        break;
             case JENIS_TABLE.TABLE_KOREKSI:

                     ProcessJurnalKoreksi (sNourut,1,0);
                     ProcessJurnalKoreksi(sNourut, -1, 0);

                 break;   
                   
        }
            return true;


       }
       //
       //
       // Jurnsllig BAST 
       //
       //

       // Ekuitas akhir: akuitas Awal +/- RKPPKD 

       private bool ProcessJurnalBAST(string sNourut)
       {

           try
           {
               string sNoJurnal;
               sNoJurnal = GetNextNo();

               //   SSQL = "INSERT INTO " & m_sNamatabelJurnal & " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " & _
               //"btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok) SELECT " & _
               // sNoJurnal & " as inojurnal, " & g_nTahun & " AS iTahun " & _
               // ", tBAST.dtBAST AS  dtInput, tBAST.dtBAST as dtJurnal, 0 as iStatus, sNoBAST as sNobukti, 1 as btJenisJurnal," & _
               // "dtBAST as  dtTanggalBukti,  tBAST.sNoBAST as sNoBukukas, inourut  as iSumber , " & JENIS_SUMBERJURNAL.S_SUMBER_BAST & " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 1 as iKelompok FROM tBAST WHERE inourut =" & sNourut
      

               SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                     "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                     sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                     ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tBAST.dtBAST as dtJurnal, 0 as iStatus, sNoBAST as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                     "tBAST.dtBAST  as  dtTanggalBukti,  sNoBAST  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.S_SUMBER_BAST).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 1 as iKelompok , " +
                     "0 as bPotongan,0 , 1 as btUrut  FROM tBAST WHERE inourut =" + sNourut;
               _dbHelper.ExecuteNonQuery(SSQL);

               //SSQL = "INSERT INTO " & m_sNamatabelJurnalRekening & " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " & _
               //"btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, btIDSUbKegiatan," & _
               //"iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " & sNoJurnal & " as inojurnal, tBAST.btKodekategori" & _
               //", tBAST.btKodeUrusan, tBAST.btKodeSKPD, tBAST.btKodeUK ,tBAST.btKodekategoriPelaksana " & _
               //",tBAST.btKodeUrusanPelaksana , btIDProgram , btIDKegiatan ,btIDSUbKegiatan, " & _
               //"KOR_LRA_LO.iiDRekeningLO AS iiDRekening , 1 as iNoUrut , 1 As iDebet , SUM(tBASTRekening.cJumlah) as cJumlah, 'Jurnal Utang ' + tBAST.sNOBAST, 1 as iKelompok FROM tBAST INNER JOIN tBASTRekening" & _
               //" ON tBAST.inourut= tBASTRekening.inourut Inner JOIN KOR_LRA_LO ON KOR_LRA_LO.IIDRekening= tBASTRekening.IIDRekening WHERE tBAST.inourut = " & sNourut & _
               //" GROUP BY tBAST.btKodekategori,tBAST.btKodeUrusan, tBAST.btKodeSKPD, tBAST.btKodeUK ,tBAST.btKodekategoriPelaksana,tBAST.btKodeUrusanPelaksana,tBAST.btIDProgram,tBAST.btIDkegiatan,tBAST.btIDSUbKegiatan, KOR_LRA_LO.IIDRekeningLO, tBAST.sNoBAST"


               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tBAST.IDDInas ," +
                     "tBAST.btKodeUK ," +
                     "tBAST.IDUrusan ,tBAST.IDProgram , tBAST.IDkegiatan ,tBAST.IDSubkegiatan,  " +
                     " KOR_LRA_LO.IIDRekeningLO AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tBASTRekening.cJumlah,'Jurnal BAST No.  ' + tBAST.sNOBAST , 1 as iKelompok, tBAST.UnitAnggaran  FROM tBAST INNER JOIN tBASTRekening " +
                     " ON tBAST.inourut = tBASTRekening.inourut  INNER JOIN KOR_LRA_LO ON KOR_LRA_LO.IIDRekening = tBASTRekening.IIDRekening WHERE tBAST.inourut = " + sNourut;

               
               _dbHelper.ExecuteNonQuery(SSQL);

               //    SSQL = "INSERT INTO " & m_sNamatabelJurnalRekening & " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " & _
               //"btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan,btIDSUbKegiatan, " & _
               //"iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " & sNoJurnal & " as inojurnal, tBAST.btKodekategori" & _
               //", tBAST.btKodeUrusan, tBAST.btKodeSKPD, tBAST.btKodeUK ,tBAST.btKodekategoriPelaksana " & _
               //",tBAST.btKodeUrusanPelaksana , btIDProgram , btIDKegiatan , btIDSUbKegiatan," & _
               //"korpermenpiutang.iiDpiutang AS iiDRekening , 1 as iNoUrut , -1 As iDebet , SUM(tBASTRekening.cJumlah) as cJumlah, 'Jurnal Utang ' + tBAST.sNOBAST, 1 as iKelompok FROM tBAST INNER JOIN tBASTRekening" & _
               //" ON tBAST.inourut= tBASTRekening.inourut Inner JOIN korpermenpiutang ON korpermenpiutang.IIDRekening= tBASTRekening.IIDRekening WHERE tBAST.inourut = " & sNourut & _
               //" GROUP BY tBAST.btKodekategori,tBAST.btKodeUrusan, tBAST.btKodeSKPD, tBAST.btKodeUK ,tBAST.btKodekategoriPelaksana,tBAST.btKodeUrusanPelaksana,tBAST.btIDProgram,tBAST.btIDkegiatan,tBAST.btIDSubkegiatan, korpermenpiutang.IIDPiutang, tBAST.sNoBAST"

               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tBAST.IDDInas ," +
                     "tBAST.btKodeUK ," +
                     "tBAST.IDUrusan ,tBAST.IDProgram , tBAST.IDkegiatan ,tBAST.IDSubkegiatan,  " +
                     " korpermenpiutang.iiDpiutang AS iiDRekening , 1 as iNoUrut , -1 As iDebet , tBASTRekening.cJumlah,'Jurnal BAST No.  ' + tBAST.sNOBAST , 1 as iKelompok, tBAST.UnitAnggaran  FROM tBAST INNER JOIN tBASTRekening " +
                     " ON tBAST.inourut = tBASTRekening.inourut  INNER JOIN korpermenpiutang ON korpermenpiutang.IIDRekening= tBASTRekening.IIDRekening WHERE tBAST.inourut = " + sNourut;
               _dbHelper.ExecuteNonQuery(SSQL);
               return true;

           }
           catch (Exception ex)
           {
               _lastError = ex.Message;
               return false;
           }

       }


       /// <summary>
       /// Jurnalling koreksi 
       /// </summary>
       /// <param name="sNourut"></param>
       /// <param name="debet"></param>
       /// <param name="bFromSKPD"></param>
       /// <returns></returns>


//       private bool ProsesJurnalKoreksiPanjar(string  sNourut ,int iDebet)
//       {
           
//     string sNoJurnal;

//               sNoJurnal = GetNextNo();

//               //SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
//               //      "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
//               //      sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
//               //      ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSPP.dtBukukas as dtJurnal, 0 as iStatus, sNoSP2D as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
//               //      "dtBukukas as  dtTanggalBukti,  sNoSP2D  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SP2D).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 2 as iKelompok , " +
//               //      "0 as bPotongan," + bFromSKPD.ToString() + " , 1 as btUrut  FROM tSPP WHERE inourut =" + sNourut;





//    SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti,  " + 
//      "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan ) SELECT " + 
//      sNoJurnal + " as inojurnal, " +  Tahun.ToString() + " AS iTahun,IDDInas, btKodeUK, " + 
//       DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tKoreksi.dtKoreksi as dtJurnal, 0 as iStatus, sNobukti  as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
//      "dtKoreksi as  dtTanggalBukti,  sNobukti  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_KOREKSI).ToString() +  
//      " as iJenisSumber,0 as btPeruntukan, bPPKD, 1 as iKelompok , 0 as bPotongan  FROM tKoreksi WHERE inourut =" + sNourut;

//               _dbHelper.ExecuteNonQuery(SSQL);
      



      
      
//      SSQL = "INSERT INTO tJurnalRekening (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD,IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKegiatan, " + 
//       " btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan,btIDSUbKegiatan, " + 
//      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, dbo.ToKodeKategori(tKoreksi.IDDInas) as btKodekategori" + 
//      ", dbo.ToKodeUrusan(tKoreksi.IDDInas) as  btKodeUrusan, dbo.ToKodeSKPD(tKoreksi.IDDInas) as  btKodeSKPD,tKoreksi.IDDInas, tKoreksi.btKodeUK,  "+
//      " tKoreksiDetail.IDUrusan, tKoreksiDetail.IDProgram, tKoreksiDetail.IDKegiatan,tKoreksiDetail.IDSubKegiatan,"+
//       "dbo.ToKodeKategoriPelaksana(tKoreksiDetail.IDUrusan) as btKodeKategoriPelaksana " + 
//      " dbo.ToKodeUrusanPelaksana(tKoreksiDetail.IDUrusan) as btKodeUrusanPelaksana , tKoreksiDetail.btIDProgram1 as btIDProgram , tKoreksiDetail.btIDkegiatan1 as btIDKegiatan , tKoreksiDetail.btIDSubKegiatan as btIDSubKegiatan , " + 
//      " tKoreksidetail.IIDRekening1 AS iiDRekening , 1 as iNoUrut , 1 As iDebet , -1 *  tKoreksiDetail.idebet1 *  tKoreksiDetail.cJumlah1 as cjumlah ,'Jurnal Koreksi no  ' + tKoreksi.sNoBukti, 1 as iKelompok FROM tKoreksi INNER JOIN tKoreksiDetail " + 
//      " ON tKoreksi.inourut = tKoreksiDetail.inourut WHERE tKoreksi.inourut = " + sNourut + " AND tKoreksiDetail.iDebet1= " + iDebet.ToString();
 
//           _dbHelper.ExecuteNonQuery(SSQL);

      
          
//               //SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
//               //      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori" +
//               //      ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,isnull(tSPP.IDurusan,0) as IDurusan " +
//               //      ",isnull(tSPPRekening.btKodeUrusanPelaksana,0) as btKodeUrusanPelaksana , isnull(tSPP.btIDProgram,0) as btIDProgram , isnull(tSPP.btIDkegiatan,0) as btIDKegiatan , " +
//               //      "tSPPRekening.IIDRekening AS iiDRekening  , 1 as iNoUrut , 1 As iDebet , tSPPRekening.cJumlah,'Jurnal SP2D No. ' + tSPP.sNoSP2D, 1 as iKelompok FROM tSPP INNER JOIN tSPPRekening  ON tSPP.inourut = tSPPRekening.inourut " +
//               //      "  WHERE tSPP.inourut = " + sNourut;

//               //_dbHelper.ExecuteNonQuery(SSQL);

      
      
////SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " + _
////      "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan,btIDSubKegiatan, " + _
////      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tKoreksi.btKodekategori" + _
////      ", tKoreksi.btKodeUrusan, tKoreksi.btKodeSKPD, tKoreksi.btKodeUK ,tKoreksiDetail.btKodekategoriPelaksana1 as btKodeKategoriPelaksana " + _
////      ",tKoreksiDetail.btKodeUrusanPelaksana1 as btKodeUrusanPelaksana , tKoreksiDetail.btIDProgram1 as btIDProgram , tKoreksiDetail.btIDkegiatan1 as btIDKegiatan , tKoreksiDetail.btIDSubKegiatan as btIDSubKegiatan , " + _
////      m_iKodeEstimasiPadaSAL + " AS iiDRekening , 2 as iNoUrut , -1 As iDebet , -1 *  tKoreksiDetail.iDebet1  * tKoreksiDetail.cJumlah1 as cJumlah ,'Jurnal SPJ  no  ' + tKoreksi.sNoBukti , 1 as iKelompok FROM tKoreksi  INNER JOIN tKoreksiDetail ON tKoreksi.inourut = tKoreksiDetail.inourut  WHERE tKoreksi.inourut = " + sNourut + " AND tKoreksiDetail.iDebet1= " + iDebet
      
   
////   ExecuteEx SSQL

//      SSQL = "INSERT INTO tJurnalRekening (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD,IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKegiatan, " + 
//       " btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan,btIDSUbKegiatan, " + 
//      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, dbo.ToKodeKategori(tKoreksi.IDDInas) as btKodekategori" + 
//      ", dbo.ToKodeUrusan(tKoreksi.IDDInas) as  btKodeUrusan, dbo.ToKodeSKPD(tKoreksi.IDDInas) as  btKodeSKPD,tKoreksi.IDDInas, tKoreksi.btKodeUK,  "+
//      " tKoreksiDetail.IDUrusan, tKoreksiDetail.IDProgram, tKoreksiDetail.IDKegiatan,tKoreksiDetail.IDSubKegiatan,"+
//       "dbo.ToKodeKategoriPelaksana(tKoreksiDetail.IDUrusan) as btKodeKategoriPelaksana " + 
//      " dbo.ToKodeUrusanPelaksana(tKoreksiDetail.IDUrusan) as btKodeUrusanPelaksana , tKoreksiDetail.btIDProgram1 as btIDProgram , tKoreksiDetail.btIDkegiatan1 as btIDKegiatan , tKoreksiDetail.btIDSubKegiatan as btIDSubKegiatan , " + 
//      m_iKodeEstimasiPadaSAL + "  AS iiDRekening , 2 as iNoUrut , -1 As iDebet , -1 *  tKoreksiDetail.idebet1 *  tKoreksiDetail.cJumlah1 as cjumlah ,'Jurnal Koreksi no  ' + tKoreksi.sNoBukti, 1 as iKelompok FROM tKoreksi INNER JOIN tKoreksiDetail " + 
//      " ON tKoreksi.inourut = tKoreksiDetail.inourut WHERE tKoreksi.inourut = " + sNourut + " AND tKoreksiDetail.iDebet1= " + iDebet.ToString();
 
//     _dbHelper.ExecuteNonQuery(SSQL);
  

//           sNoJurnal = GetNextNo();
           
//    SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti,  " + 
//      "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan ) SELECT " + 
//      sNoJurnal + " as inojurnal, " +  Tahun.ToString() + " AS iTahun,IDDInas, btKodeUK, " + 
//       DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tKoreksi.dtKoreksi as dtJurnal, 0 as iStatus, sNobukti  as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
//      "dtKoreksi as  dtTanggalBukti,  sNobukti  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_KOREKSI).ToString() +  
//      " as iJenisSumber,0 as btPeruntukan, bPPKD, 2 as iKelompok , 0 as bPotongan  FROM tKoreksi WHERE inourut =" + sNourut;

//      _dbHelper.ExecuteNonQuery(SSQL);
      

      
               
////SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " + _
////      "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan,btIDSubKegiatan, " + _
////      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tKoreksi.btKodekategori" + _
////      ", tKoreksi.btKodeUrusan, tKoreksi.btKodeSKPD, tKoreksi.btKodeUK ,tKoreksiDetail.btKodekategoriPelaksana1 as btKodeKategoriPelaksana " + _
////      ", tKoreksiDetail.btKodeUrusanPelaksana1 as btKodeUrusanPelaksana , tKoreksiDetail.btIDProgram1 as btIDProgram , tKoreksiDetail.btIDkegiatan1 as btIDKegiatan , tKoreksiDetail.btIDSubkegiatan as btIDSubKegiatan , " + _
////      " KOR_LRA_LO.IIDRekeningLO AS iiDRekening , 1 as iNoUrut , 1 As iDebet , -1 * tKoreksiDetail.idebet1 *  tKoreksiDetail.cJumlah1,'Jurnal Koreksi  no  ' + tKoreksi.sNoBukti ,2 as iKelompok FROM tKoreksi INNER JOIN tKoreksiDetail " + _
////      " ON tKoreksi.inourut = tKoreksiDetail.inourut  INNER JOIN KOR_LRA_LO ON KOR_LRA_LO.IIDRekening = tKoreksiDetail.IIDRekening1 WHERE tKoreksi.inourut = " + sNourut + " AND tKoreksiDetail.iDebet1= " + iDebet
   
//             SSQL = "INSERT INTO tJurnalRekening (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD,IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKegiatan, " + 
//       " btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan,btIDSUbKegiatan, " + 
//      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, dbo.ToKodeKategori(tKoreksi.IDDInas) as btKodekategori" + 
//      ", dbo.ToKodeUrusan(tKoreksi.IDDInas) as  btKodeUrusan, dbo.ToKodeSKPD(tKoreksi.IDDInas) as  btKodeSKPD,tKoreksi.IDDInas, tKoreksi.btKodeUK,  "+
//      " tKoreksiDetail.IDUrusan, tKoreksiDetail.IDProgram, tKoreksiDetail.IDKegiatan,tKoreksiDetail.IDSubKegiatan,"+
//       "dbo.ToKodeKategoriPelaksana(tKoreksiDetail.IDUrusan) as btKodeKategoriPelaksana " + 
//      " dbo.ToKodeUrusanPelaksana(tKoreksiDetail.IDUrusan) as btKodeUrusanPelaksana , tKoreksiDetail.btIDProgram1 as btIDProgram , tKoreksiDetail.btIDkegiatan1 as btIDKegiatan , tKoreksiDetail.btIDSubKegiatan as btIDSubKegiatan , " + 
//      " KOR_LRA_LO.IIDRekeningLO AS iiDRekening , 1 as iNoUrut , 1 As iDebet , -1 *  tKoreksiDetail.idebet1 *  tKoreksiDetail.cJumlah1 as cjumlah ,'Jurnal Koreksi no  ' + tKoreksi.sNoBukti, 1 as iKelompok FROM tKoreksi INNER JOIN tKoreksiDetail " + 
//      " ON tKoreksi.inourut = tKoreksiDetail.inourut  INNER JOIN KOR_LRA_LO ON KOR_LRA_LO.IIDRekening = tKoreksiDetail.IIDRekening1  WHERE tKoreksi.inourut = " + sNourut + " AND tKoreksiDetail.iDebet1= " + iDebet.ToString();
 
//           _dbHelper.ExecuteNonQuery(SSQL);

 
      

// //SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " + _
// //     "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan,btIDSubKegiatan, " + _
// //     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tKoreksi.btKodekategori" + _
// //     ", tKoreksi.btKodeUrusan, tKoreksi.btKodeSKPD, tKoreksi.btKodeUK ,tKoreksiDetail.btKodekategoriPelaksana1 as btKodeKategoriPelaksana " + _
// //     ",tKoreksiDetail.btKodeUrusanPelaksana1 as btKodeUrusanPelaksana , tKoreksiDetail.btIDProgram1 as btIDProgram , tKoreksiDetail.btIDkegiatan1 as btIDKegiatan , tKoreksiDetail.btIDSubKegiatan as btIDSubKegiatan , " + _
// //      " mKasBendahara.IIDrekening AS iiDRekening , 2 as iNoUrut , -1 As iDebet ,-1 * tKoreksiDetail.idebet1 * tKoreksiDetail.cJumlah1,'Jurnal Koreksi no  ' + tKoreksi.sNoBukti , 2 as iKelompok FROM tKoreksi INNER JOIN mkasbendahara " + _
// //     " ON tKoreksi.btKodekategori = mkasbendahara.btKodekategori and tKoreksi.btKodeUrusan = mkasbendahara.btKodeUrusan " + _
// //     " and tKoreksi.btKodeSKPD = mkasbendahara.btKodeSKPD and mkasbendahara.bPPKD = tKoreksi.bPPKD inner join tKoreksiDetail on tKOreksi.inourut = tKoreksiDetail.inourut WHERE tKoreksi.inourut = " + sNourut + " AND tKoreksiDetail.iDebet1= " + iDebet + _
// //     " AND mkasbendahara.btJenis=2 and mKasBendahara.bPPKD = tKoreksi.bPPKD  "
   
//     SSQL = "INSERT INTO tJurnalRekening (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD,IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKegiatan, " + 
//       " btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan,btIDSUbKegiatan, " + 
//      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, dbo.ToKodeKategori(tKoreksi.IDDInas) as btKodekategori" + 
//      ", dbo.ToKodeUrusan(tKoreksi.IDDInas) as  btKodeUrusan, dbo.ToKodeSKPD(tKoreksi.IDDInas) as  btKodeSKPD,tKoreksi.IDDInas, tKoreksi.btKodeUK,  "+
//      " tKoreksiDetail.IDUrusan, tKoreksiDetail.IDProgram, tKoreksiDetail.IDKegiatan,tKoreksiDetail.IDSubKegiatan,"+
//       "dbo.ToKodeKategoriPelaksana(tKoreksiDetail.IDUrusan) as btKodeKategoriPelaksana " + 
//      " dbo.ToKodeUrusanPelaksana(tKoreksiDetail.IDUrusan) as btKodeUrusanPelaksana , tKoreksiDetail.btIDProgram1 as btIDProgram , tKoreksiDetail.btIDkegiatan1 as btIDKegiatan , tKoreksiDetail.btIDSubKegiatan as btIDSubKegiatan , " + 
//        m_KasBendaharaPengeluaran.ToString() + " AS iiDRekening , 1 as iNoUrut , -1 As iDebet , -1 *  tKoreksiDetail.idebet1 *  tKoreksiDetail.cJumlah1 as cjumlah ,'Jurnal Koreksi no  ' + tKoreksi.sNoBukti, 1 as iKelompok FROM tKoreksi INNER JOIN tKoreksiDetail " + 
//      " ON tKoreksi.inourut = tKoreksiDetail.inourut WHERE tKoreksi.inourut = " + sNourut + " AND tKoreksiDetail.iDebet1= " + iDebet.ToString();
 


//    ExecuteEx SSQL
    

//       }


       private bool ProcessJurnalKoreksi(string sNourut, int debet, int bFromSKPD)
       {
           try{

           string sNoJurnal;

          sNoJurnal = GetNextNo();

  
        SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tKoreksi.dtKoreksi as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                "dtKoreksi as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_KOREKSI).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 1 as iKelompok , " +
                "0 as bPotongan," + bFromSKPD.ToString() + " , 1 as btUrut  FROM tKoreksi WHERE inourut =" + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);



      
      
      //SSQL = "INSERT INTO " & m_sNamatabelJurnalRekening & " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " & _
      //"btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan,btIDSUbKegiatan, " & _
      //"iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " & sNoJurnal & " as inojurnal, tKoreksi.btKodekategori" & _
      //", tKoreksi.btKodeUrusan, tKoreksi.btKodeSKPD, tKoreksi.btKodeUK ,tKoreksiDetail.btKodekategoriPelaksana1 as btKodeKategoriPelaksana " & _
      //",tKoreksiDetail.btKodeUrusanPelaksana1 as btKodeUrusanPelaksana , tKoreksiDetail.btIDProgram1 as btIDProgram , tKoreksiDetail.btIDkegiatan1 as btIDKegiatan , tKoreksiDetail.btIDSubKegiatan as btIDSubKegiatan , " & _
      //" tKoreksidetail.IIDRekening1 AS iiDRekening , 1 as iNoUrut , 1 As iDebet , -1 *  tKoreksiDetail.idebet1 *  tKoreksiDetail.cJumlah1 as cjumlah ,'Jurnal Koreksi no  ' + tKoreksi.sNoBukti, 1 as iKelompok FROM tKoreksi INNER JOIN tKoreksiDetail " & _
      //" ON tKoreksi.inourut = tKoreksiDetail.inourut WHERE tKoreksi.inourut = " & sNourut & " AND tKoreksiDetail.iDebet1= " & iDebet
      
          SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tKoreksi.IDDInas ," +
                     "tKoreksi.btKodeUK ," +
                     "tKoreksiDetail.IDUrusan ,tKoreksiDetail.IDProgram , tKoreksiDetail.IDkegiatan ,tKoreksiDetail.IDSubkegiatan,  " +
                     " tKoreksiDetail.IIDRekening1 AS iiDRekening , 1 as iNoUrut , 1 As iDebet ,  -1 *  tKoreksiDetail.idebet1 *  tKoreksiDetail.cJumlah1 as cJumlah,'Jurnal Koreksi No.  ' + tKoreksi.sNoBukti , 1 as iKelompok, tKoreksi.UnitAnggaran  FROM tKoreksi INNER JOIN tKoreksiDetail " +
                     " ON tKoreksi.inourut = tKoreksiDetail.inourut  WHERE tKoreksi.inourut = " + sNourut + " AND tKoreksiDetail.iDebet1= " + debet.ToString();

         _dbHelper.ExecuteNonQuery(SSQL);

      
      
//SSQL = "INSERT INTO " & m_sNamatabelJurnalRekening & " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " & _
//      "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan,btIDSubKegiatan, " & _
//      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " & sNoJurnal & " as inojurnal, tKoreksi.btKodekategori" & _
//      ", tKoreksi.btKodeUrusan, tKoreksi.btKodeSKPD, tKoreksi.btKodeUK ,tKoreksiDetail.btKodekategoriPelaksana1 as btKodeKategoriPelaksana " & _
//      ",tKoreksiDetail.btKodeUrusanPelaksana1 as btKodeUrusanPelaksana , tKoreksiDetail.btIDProgram1 as btIDProgram , tKoreksiDetail.btIDkegiatan1 as btIDKegiatan , tKoreksiDetail.btIDSubKegiatan as btIDSubKegiatan , " & _
//      m_iKodeEstimasiPadaSAL & " AS iiDRekening , 2 as iNoUrut , -1 As iDebet , -1 *  tKoreksiDetail.iDebet1  * tKoreksiDetail.cJumlah1 as cJumlah ,'Jurnal SPJ  no  ' + tKoreksi.sNoBukti , 1 as iKelompok FROM tKoreksi  INNER JOIN tKoreksiDetail ON tKoreksi.inourut = tKoreksiDetail.inourut  WHERE tKoreksi.inourut = " & sNourut & " AND tKoreksiDetail.iDebet1= " & iDebet
      
           SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tKoreksi.IDDInas ," +
                     "tKoreksi.btKodeUK ," +
                     "tKoreksiDetail.IDUrusan ,tKoreksiDetail.IDProgram , tKoreksiDetail.IDkegiatan ,tKoreksiDetail.IDSubkegiatan,  " +
                     m_iKodeEstimasiPadaSAL + "  AS iiDRekening , 2 as iNoUrut , -1 As iDebet ,   -1 *  tKoreksiDetail.iDebet1  * tKoreksiDetail.cJumlah1 as cJumlah,"+
                     "'Jurnal Koreksi No.  ' + tKoreksi.sNoBukti , 1 as iKelompok, tKoreksi.UnitAnggaran  FROM tKoreksi INNER JOIN tKoreksiDetail " +
                     " ON tKoreksi.inourut = tKoreksiDetail.inourut  WHERE tKoreksi.inourut = " + sNourut + " AND tKoreksiDetail.iDebet1= " + debet.ToString();

   
         _dbHelper.ExecuteNonQuery(SSQL);
   
      
sNoJurnal = GetNextNo();
      
      
//SSQL = "INSERT INTO " & m_sNamatabelJurnal & " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " & _
//      "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, bFromSKPD ) SELECT " & _
//      sNoJurnal & " as inojurnal, " & g_nTahun & " AS iTahun " & _
//      ", " & SQLDateFormat(Date) & " AS  dtInput, tKoreksi.dtKoreksi as dtJurnal, 0 as iStatus, sNobukti as sNobukti, " & JENIS_JURNAL.JENIS_JURNALPENGELUARAN & " as btJenisJurnal," & _
//      "dtKoreksi as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " & JENIS_SUMBERJURNAL.E_SUMBER_PANJAR & _
//      " as iJenisSumber, 0 as btPeruntukan, bPPKD, 2 as iKelompok , 0 as bPotongan , " & CStr(bFromSKPD) & " FROM tKoreksi WHERE inourut =" & sNourut
      
//ExecuteEx SSQL

      SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tKoreksi.dtKoreksi as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                "dtKoreksi as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_KOREKSI).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 2 as iKelompok , " +
                "0 as bPotongan," + bFromSKPD.ToString() + " , 1 as btUrut  FROM tKoreksi WHERE inourut =" + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);


               
//SSQL = "INSERT INTO " & m_sNamatabelJurnalRekening & " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " & _
//      "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan,btIDSubKegiatan, " & _
//      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " & sNoJurnal & " as inojurnal, tKoreksi.btKodekategori" & _
//      ", tKoreksi.btKodeUrusan, tKoreksi.btKodeSKPD, tKoreksi.btKodeUK ,tKoreksiDetail.btKodekategoriPelaksana1 as btKodeKategoriPelaksana " & _
//      ", tKoreksiDetail.btKodeUrusanPelaksana1 as btKodeUrusanPelaksana , tKoreksiDetail.btIDProgram1 as btIDProgram , tKoreksiDetail.btIDkegiatan1 as btIDKegiatan , tKoreksiDetail.btIDSubkegiatan as btIDSubKegiatan , " & _
//      " KOR_LRA_LO.IIDRekeningLO AS iiDRekening , 1 as iNoUrut , 1 As iDebet , -1 * tKoreksiDetail.idebet1 *  tKoreksiDetail.cJumlah1,'Jurnal Koreksi  no  ' + tKoreksi.sNoBukti ,2 as iKelompok FROM tKoreksi INNER JOIN tKoreksiDetail " & _
//      " ON tKoreksi.inourut = tKoreksiDetail.inourut  INNER JOIN KOR_LRA_LO ON KOR_LRA_LO.IIDRekening = tKoreksiDetail.IIDRekening1 WHERE tKoreksi.inourut = " & sNourut & " AND tKoreksiDetail.iDebet1= " & iDebet
      
      SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tKoreksi.IDDInas ," +
                     "tKoreksi.btKodeUK ," +
                     "tKoreksiDetail.IDUrusan ,tKoreksiDetail.IDProgram , tKoreksiDetail.IDkegiatan ,tKoreksiDetail.IDSubkegiatan,  " +
                     " KOR_LRA_LO.IIDRekeningLO AS iiDRekening , 1 as iNoUrut , 1 As iDebet ,  -1 * tKoreksiDetail.idebet1 *  tKoreksiDetail.cJumlah1 as cJumlah,'Jurnal Koreksi No.  ' + tKoreksi.sNoBukti , 1 as iKelompok, tKoreksi.UnitAnggaran  FROM tKoreksi INNER JOIN tKoreksiDetail " +
                     " ON tKoreksi.inourut = tKoreksiDetail.inourut INNER JOIN KOR_LRA_LO ON KOR_LRA_LO.IIDRekening = tKoreksiDetail.IIDRekening1  WHERE tKoreksi.inourut = " + sNourut + " AND tKoreksiDetail.iDebet1= " + debet.ToString();

         _dbHelper.ExecuteNonQuery(SSQL);


      
      

 //SSQL = "INSERT INTO " & m_sNamatabelJurnalRekening & " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " & _
 //     "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan,btIDSubKegiatan, " & _
 //     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " & sNoJurnal & " as inojurnal, tKoreksi.btKodekategori" & _
 //     ", tKoreksi.btKodeUrusan, tKoreksi.btKodeSKPD, tKoreksi.btKodeUK ,tKoreksiDetail.btKodekategoriPelaksana1 as btKodeKategoriPelaksana " & _
 //     ",tKoreksiDetail.btKodeUrusanPelaksana1 as btKodeUrusanPelaksana , tKoreksiDetail.btIDProgram1 as btIDProgram , tKoreksiDetail.btIDkegiatan1 as btIDKegiatan , tKoreksiDetail.btIDSubKegiatan as btIDSubKegiatan , " & _
 //      " mKasBendahara.IIDrekening AS iiDRekening , 2 as iNoUrut , -1 As iDebet ,-1 * tKoreksiDetail.idebet1 * tKoreksiDetail.cJumlah1,'Jurnal Koreksi no  ' + tKoreksi.sNoBukti , 2 as iKelompok FROM tKoreksi INNER JOIN mkasbendahara " & _
 //     " ON tKoreksi.btKodekategori = mkasbendahara.btKodekategori and tKoreksi.btKodeUrusan = mkasbendahara.btKodeUrusan " & _
 //     " and tKoreksi.btKodeSKPD = mkasbendahara.btKodeSKPD and mkasbendahara.bPPKD = tKoreksi.bPPKD inner join tKoreksiDetail on tKOreksi.inourut = tKoreksiDetail.inourut WHERE tKoreksi.inourut = " & sNourut & " AND tKoreksiDetail.iDebet1= " & iDebet & _
 //     " AND mkasbendahara.btJenis=2 and mKasBendahara.bPPKD = tKoreksi.bPPKD  "
   
          
           SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tKoreksi.IDDInas ," +
                     "tKoreksi.btKodeUK ," +
                     "tKoreksiDetail.IDUrusan ,tKoreksiDetail.IDProgram , tKoreksiDetail.IDkegiatan ,tKoreksiDetail.IDSubkegiatan,  " +
                     " (Select iidrekening from mKasBendahara where btJenis= 2)  AS iiDRekening , 2 as iNoUrut , -1 As iDebet ,   -1 * tKoreksiDetail.idebet1 * tKoreksiDetail.cJumlah1 as cJumlah,"+
                     "'Jurnal Koreksi No.  ' + tKoreksi.sNoBukti , 1 as iKelompok, tKoreksi.UnitAnggaran  FROM tKoreksi INNER JOIN tKoreksiDetail " +
                     " ON tKoreksi.inourut = tKoreksiDetail.inourut  WHERE tKoreksi.inourut = " + sNourut + " AND tKoreksiDetail.iDebet1= " + debet.ToString();



           _dbHelper.ExecuteNonQuery(SSQL);

           return true;

           } catch(Exception ex){
               return false;

           }




       }
       /****
        * * *************************************************************************************************************
        * *** JURNAL SPP 
        * ***************************************************************************************************************
        * */
       private bool ProcessJurnalSPP(string sNourut , int  iJenisSPP , bool bTHL = false, int bFromSKPD = 0){
            switch (iJenisSPP){
                case 0:
                    JurnalUPGU13 (sNourut, bFromSKPD);
                    JurnalPPKDSP2DNonPPKD (sNourut, bFromSKPD);

                    break;
                case 1: 
                    JurnalUPGU13 (sNourut, bFromSKPD);
                    JurnalPPKDSP2DNonPPKD (sNourut, bFromSKPD);
                break;
                case 2:
                    if (bFromSKPD == 1 ){
                        JurnalUPGU13 (sNourut, bFromSKPD);
                    } else {
                    
                    
                        JurnalUPGU13 (sNourut, bFromSKPD);
                        JurnalPPKDSP2DNonPPKD (sNourut, bFromSKPD);
                    }
                    break;
                case 3:
                    JurnalLSBarangJasa13 (sNourut, false, bFromSKPD);
                    JurnalPPKDSP2DNonPPKD (sNourut, bFromSKPD);
                    
                    break;
                case 4:
                    if( bTHL == true) {
                        JurnalLSBarangJasa13 (sNourut, true, bFromSKPD);
                        JurnalPPKDSP2DNonPPKD (sNourut, bFromSKPD);
                    } else {
                       // JurnalLSBarangJasa13(sNourut, false ,bFromSKPD);
                        JurnalPembayaranLS13 (sNourut, bFromSKPD);
                        JurnalPPKDSP2DNonPPKD (sNourut, bFromSKPD);
                    }
                    break;
                case 5:
                    JurnalPembayaranLS13PPKD(sNourut);
                    break;
           }
     
           return true;
       }
       private bool JurnalUPGU13(string sNourut , int bFromSKPD){

          string  sNoJurnal ;
          sNoJurnal =GetNextNo();
          try
          {
              if (bFromSKPD == 0)
              {

              
                  SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                     "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                     sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                     ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSPP.dtBukukas as dtJurnal, 0 as iStatus, sNoSP2D as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                     "dtBukukas as  dtTanggalBukti,  sNoSP2D  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SP2D).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 2 as iKelompok , " +
                     " 0 as bPotongan," + bFromSKPD.ToString() + " , 1 as btUrut  FROM tSPP WHERE inourut =" + sNourut;
                  
                  _dbHelper.ExecuteNonQuery(SSQL);

                  

                  SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                        "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSPP.IDDInas ," +
                        "tSPP.btKodeUK ," +
                        "0 as IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                        " (Select IIDrekening from mKasBendahara where btJenis= 2 ) AS iiDRekening  , 1 as iNoUrut , 1 As iDebet , tSPP.cJumlah,'Jurnal SP2D No.  ' + tSPP.sNoSP2D , 1 as iKelompok, tSPP.UnitAnggaran  FROM tSPP " +
                        " WHERE tSPP.inourut = " + sNourut;

                  

                  _dbHelper.ExecuteNonQuery(SSQL);
             
                  SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                          "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSPP.IDDInas ," +
                          "tSPP.btKodeUK ," +
                          "0 as IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                          m_iRekRKPPKD + "  AS iiDRekening  , 1 as iNoUrut , -1 As iDebet , tSPP.cJumlah,'Jurnal SP2D No.  ' + tSPP.sNoSP2D , 1 as iKelompok, tSPP.UnitAnggaran  FROM tSPP " +
                          " WHERE tSPP.inourut = " + sNourut;

                  _dbHelper.ExecuteNonQuery(SSQL);
              }
              else
              {

                  SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                          "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, sUraian, bFromSKPD ) SELECT " +
                          sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                          ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoSP2D as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                          "dtbukukas as  dtTanggalBukti,  sNoSP2D as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SP2D).ToString() + "  as iJenisSumber,0 as btPeruntukan, bPPKD as bPPKD, 1 as ikelompok , 0 as bPotongan , sPeruntukan as sUraian ," + bFromSKPD.ToString() + " FROM tSPP  WHERE inourut =" + sNourut;
                  _dbHelper.ExecuteNonQuery(SSQL);

                  SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                        "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.IDDInas," +
                        "tSPP.IDurusan,0 as IDProgram , 0 as IDKegiatan , " +
                        "mKasBendahara.IIDrekening AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSPP.cJumlah,'Jurnal SP2D no  ' + sNoSP2D, 1 as iKelompok FROM tSPP INNER JOIN mKasBendahara " +
                        " ON tSPP.IDDInas = mkasbendahara.IDDInas and tSPP.bPPKD = mkasbendahara.bPPKD WHERE tSPP.inourut = " + sNourut +
                        " AND mkasbendahara.btJenis=2 ";

                  _dbHelper.ExecuteNonQuery(SSQL);

                  SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                        "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan,iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.IDDInas," +
                        " tSPP.IDUrusan, 0 as IDProgram , 0 as IDKegiatan , " +
                        m_iRekKasda + " AS iiDRekening , 1 as iNoUrut , -1 As iDebet , cJumlah,'Jurnal SP2D no  ' + sNoSP2D, 1 as Ikelompok FROM tSPP WHERE inourut = " + sNourut;

                  _dbHelper.ExecuteNonQuery(SSQL);

              }
              return true;
          }
          catch (Exception ex)
          {
              _lastError = ex.Message + "\n " + SSQL;
              return false;
          }
       }

        
       private bool JurnalPPKDSP2DNonPPKD(string sNourut ,int  bFromSKPD ){

             string sNoJurnal;  
    
            //sNoJurnal = GetNextNo();
            try
            {

                sNoJurnal = GetNextNo();
            

                SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                      "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                      sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                      ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSPP.dtBukukas as dtJurnal, 0 as iStatus, sNoSP2D as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                      "dtBukukas as  dtTanggalBukti,  sNoSP2D  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SP2D).ToString() + " as iJenisSumber,1 as btPeruntukan, 1 as bPPKD, 1 as iKelompok , " +
                      "0 as bPotongan," + bFromSKPD.ToString() + " , 1 as btUrut  FROM tSPP WHERE inourut =" + sNourut;



                _dbHelper.ExecuteNonQuery(SSQL);

             
                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSPP.IDDInas ," +
                     "tSPP.btKodeUK ," +
                     "0 as IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                     " mkasbendahara.iidRekening AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSPP .cJumlah,'Jurnal SP2D No.  ' + tSPP.sNoSP2D , 1 as iKelompok , tSPP.UnitAnggaran FROM tSPP " +
                     " INNER JOIN mKasBEndahara ON  tSPP.bPPKD = mkasbendahara.bPPKD  WHERE tSPP.inourut = " + sNourut + " AND mkasbendahara.btJenis=0  AND tSPP.bPPKD=0" ;


                _dbHelper.ExecuteNonQuery(SSQL);

               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSPP.IDDInas ," +
                     "tSPP.btKodeUK ," +
                     "0 as IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                     m_iRekKasda.ToString() + " AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSPP .cJumlah,'Jurnal SP2D No.  ' + tSPP.sNoSP2D , 1 as iKelompok , tSPP.UnitAnggaran FROM tSPP " +
                     " WHERE tSPP.inourut = " + sNourut + "  AND tSPP.bPPKD=0";



                
                _dbHelper.ExecuteNonQuery(SSQL);


                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message + "\n " + SSQL;
                return false;
            }

       }

       private bool JurnalLSBarangJasa13(string sNourut , bool bTHL = false, int bFromSKPD = 0){
            
            string sNoJurnal ;
            try
            {
                sNoJurnal = GetNextNo();
                string SQLSelect = "";


                SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                     "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                     sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                     ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSPP.dtBukukas as dtJurnal, 0 as iStatus, sNoSP2D as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                     "dtBukukas as  dtTanggalBukti,  sNoSP2D  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SP2D).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 2 as iKelompok , " +
                     "0 as bPotongan," + bFromSKPD.ToString() + ",2 as btUrut   FROM tSPP WHERE inourut =" + sNourut;
                
                _dbHelper.ExecuteNonQuery(SSQL);


         
                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSPP.IDDInas ," +
                                     "tSPP.btKodeUK ," +
                                     "tSPPRekening.IDUrusan ,tSPPRekening.IDProgram , tSPPRekening.IDkegiatan ,tSPPRekening.IDSubkegiatan,  " +
                                     " korpermenpiutang.iiDpiutang   AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSPPRekening.cJumlah,'Jurnal SP2D No.  ' + tSPP.sNoSP2D , 2 as iKelompok, tSPP.UnitAnggaran  FROM tSPP INNER JOIN tSPPRekening " +
                                     " ON tSPP.inourut = tSPPRekening.inourut   inner Join korpermenpiutang on korpermenpiutang.IIDrekening= tSPPRekening.IIDREkening   WHERE tSPP.inourut = " + sNourut + " AND tSPP.inobast>0 ";



                _dbHelper.ExecuteNonQuery(SSQL);


                if (bTHL == true)
                {
                    
                    SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok,UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSPP.IDDInas ," +
                                     "tSPP.btKodeUK ," +
                                     "tSPPRekening.IDUrusan ,tSPPRekening.IDProgram , tSPPRekening.IDkegiatan ,tSPPRekening.IDSubkegiatan,  " +
                                     " korpermenpiutang.iiDpiutang   AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSPPRekening.cJumlah,'Jurnal SP2D No.  ' + tSPP.sNoSP2D , 2 as iKelompok, tSPP.UnitAnggaran FROM tSPP INNER JOIN tSPPRekening " +
                                     " ON tSPP.inourut = tSPPRekening.inourut   inner Join korpermenpiutang on korpermenpiutang.IIDrekening= tSPPRekening.IIDREkening   WHERE tSPP.inourut = " + sNourut + "";



                    _dbHelper.ExecuteNonQuery(SSQL);


                }
                else
                {


                    SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSPP.IDDInas ," +
                                     "tSPP.btKodeUK ," +
                                     "tSPPRekening.IDUrusan ,tSPPRekening.IDProgram , tSPPRekening.IDkegiatan ,tSPPRekening.IDSubkegiatan,  " +
                                     " KOR_LRA_LO.IIDRekeningLO   AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSPPRekening.cJumlah,'Jurnal SP2D No.  ' + tSPP.sNoSP2D , 2 as iKelompok,tSPP.UnitAnggaran  FROM tSPP INNER JOIN tSPPRekening " +
                                     " ON tSPP.inourut = tSPPRekening.inourut   inner Join KOR_LRA_LO on KOR_LRA_LO.IIDrekening= tSPPRekening.IIDREkening   WHERE tSPP.inourut = " + sNourut +" AND tSPP.inobast =0";
                    //catatatn



                    _dbHelper.ExecuteNonQuery(SSQL);

                }

                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitANggaran) SELECT " + sNoJurnal + " as inojurnal, tSPP.IDDInas ," +
                                     "tSPP.btKodeUK ," +
                                     "0 as IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                                      m_iRekRKPPKD.ToString() + " AS iiDRekening , 1 as iNoUrut , -1 As iDebet , tSPP.cJumlah,'Jurnal SP2D No.  ' + tSPP.sNoSP2D , 2 as iKelompok ,tSPP.UnitAnggaran FROM tSPP WHERE tSPP.inourut = " + sNourut + "";

                
                _dbHelper.ExecuteNonQuery(SSQL);





                sNoJurnal = GetNextNo();



                SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                         "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,btUrut ,bFromSKPD ) SELECT " +
                         sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                         ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSPP.dtBukukas as dtJurnal, 0 as iStatus, sNoSP2D as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                         "dtBukukas as  dtTanggalBukti,  sNoSP2D  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SP2D).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 2 as iKelompok ,0 as bPotongan,1 as btUrut, " +
                         "" + bFromSKPD.ToString() + "  FROM tSPP WHERE inourut =" + sNourut;

                _dbHelper.ExecuteNonQuery(SSQL);



                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                        "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok,UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSPP.IDDInas ," +
                        "tSPP.btKodeUK ," +
                        "tSPPRekening.IDUrusan ,tSPPRekening.IDProgram , tSPPRekening.IDkegiatan ,tSPPRekening.IDSubkegiatan,  " +
                        " tSPPRekening.IIDrekening AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSPPRekening.cJumlah,'Jurnal SP2D No.  ' + tSPP.sNoSP2D , 1 as iKelompok ,tSPP.UnitAnggaran FROM tSPP INNER JOIN tSPPRekening " +
                        " ON tSPP.inourut = tSPPRekening.inourut   WHERE tSPP.inourut = " + sNourut + "";




                _dbHelper.ExecuteNonQuery(SSQL);


                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok,UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSPP.IDDInas ," +
                                     "tSPP.btKodeUK ," +
                                     "0 as IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                                      m_iKodeEstimasiPadaSAL.ToString() + " AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSPP.cJumlah,'Jurnal SP2D No.  ' + tSPP.sNoSP2D , 1 as iKelompok, tSPP.UnitAnggaran FROM tSPP WHERE tSPP.inourut = " + sNourut + "";

                
                _dbHelper.ExecuteNonQuery(SSQL);

                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }            
                  

       }


       private bool JurnalPembayaranLS13(string sNourut, int bFromSKPD)
       {

           try
           {
               string sNoJurnal;

               sNoJurnal = GetNextNo();

               SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                     "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                     sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                     ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSPP.dtBukukas as dtJurnal, 0 as iStatus, sNoSP2D as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                     "dtBukukas as  dtTanggalBukti,  sNoSP2D  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SP2D).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 2 as iKelompok , " +
                     "0 as bPotongan," + bFromSKPD.ToString() + " , 1 as btUrut  FROM tSPP WHERE inourut =" + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);

               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSPP.IDDInas ," +
                     "tSPP.btKodeUK ," +
                     "tSPPRekening.IDUrusan ,tSPPRekening.IDProgram , tSPPRekening.IDkegiatan ,tSPPRekening.IDSubkegiatan,  " +
                     " KOR_LRA_LO.IIDRekeningLO AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSPPRekening.cJumlah,'Jurnal SP2D No.  ' + tSPP.sNoSP2D , 2 as iKelompok, tSPP.UnitAnggaran  FROM tSPP INNER JOIN tSPPRekening " +
                     " ON tSPP.inourut = tSPPRekening.inourut  INNER JOIN KOR_LRA_LO ON KOR_LRA_LO.IIDRekening = tSPPRekening.IIDRekening WHERE tSPP.inourut = " + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);

               //SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
               //      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan,iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori" +
               //      ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,isnull(tSPP.IDUrusan ,0) as IDUrusan" +
               //      ",isnull(tSPP.btKodeUrusanPelaksana ,0) as btKodeUrusanPelaksana , isnull(tSPP.btIDProgram ,0) as btIDProgram , isnull(tSPP.btIDkegiatan,0) as btIDKegiatan , " +
               //       m_iRekRKPPKD + "  AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSPP. cJumlah,'Jurnal SP2D No.  ' + tSPP.sNoSP2d , 2 as iKelompok FROM tSPP WHERE tSPP.inourut = " + sNourut;

               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSPP.IDDInas ," +
                     "tSPP.btKodeUK ," +
                     "0 as IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                     m_iRekRKPPKD.ToString() + "  AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSPP.cJumlah as cJumlah ,'Jurnal SP2D No.  ' + tSPP.sNoSP2D , 2 as iKelompok, tSPP.UnitAnggaran  FROM tSPP " +
                     "  WHERE tSPP.inourut = " + sNourut;



               _dbHelper.ExecuteNonQuery(SSQL);


               sNoJurnal = GetNextNo();
               //SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
               //        "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD ) SELECT " +
               //        sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
               //        ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSPP.dtBukukas as dtJurnal, 0 as iStatus, sNoSP2D as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
               //        "dtBukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SP2D).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 1 as iKelompok , 0 as bPotongan," + bFromSKPD.ToString() + "  FROM tSPP WHERE inourut =" + sNourut;


               SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                 "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                 sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                 ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSPP.dtBukukas as dtJurnal, 0 as iStatus, sNoSP2D as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                 "dtBukukas as  dtTanggalBukti,  sNoSP2D  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SP2D).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 1 as iKelompok , " +
                 "0 as bPotongan," + bFromSKPD.ToString() + " , 2 as btUrut  FROM tSPP WHERE inourut =" + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);

               //SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
               //      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori" +
               //      ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,isnull(tSPP.IDUrusan,0) as IDUrusan " +
               //      ",isnull(tSPPRekening.btKodeUrusanPelaksana,0) as btKodeUrusanPelaksana , isnull(tSPP.btIDProgram,0) as btIDProgram , isnull(tSPP.btIDkegiatan,0) as btIDKegiatan , " +
               //      " tSPPRekening.IIDRekening AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSPPRekening.cJumlah,'Jurnal SP2DNo No. ' + tSPP.sNoSP2D, 1 as iKelompok FROM tSPP INNER JOIN tSPPRekening " +
               //      " ON tSPP.inourut = tSPPRekening.inourut WHERE tSPP.inourut = " + sNourut;
               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSPP.IDDInas ," +
                     "tSPP.btKodeUK ," +
                     "tSPPRekening.IDUrusan ,tSPPRekening.IDProgram , tSPPRekening.IDkegiatan ,tSPPRekening.IDSubkegiatan,  " +
                     " tSPPRekening.IIDRekening AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSPPRekening.cJumlah,'Jurnal SP2D No.  ' + tSPP.sNoSP2D , 1 as iKelompok , tSPP.UnitAnggaran FROM tSPP INNER JOIN tSPPRekening " +
                     " ON tSPP.inourut = tSPPRekening.inourut  WHERE tSPP.inourut = " + sNourut;



               _dbHelper.ExecuteNonQuery(SSQL);


               //SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
               //      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan,iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori" +
               //      ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,isnull(tSPP.IDurusan,0) as IDurusan" +
               //      ",isnull(tSPP.btKodeUrusanPelaksana,0) as btKodeUrusanPelaksana , isnull(tSPP.btIDProgram,0) as btIDProgram , isnull(tSPP.btIDkegiatan,0) as btIDKegiatan , " +
               //      m_iKodeEstimasiPadaSAL + " AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSPP.cJumlah,'Jurnal SP2D No. ' + tSPP.sNoSP2D ,1 as iKelompok FROM tSPP  WHERE tSPP.inourut = " + sNourut;

               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitANggaran) SELECT " + sNoJurnal + " as inojurnal, tSPP.IDDInas ," +
                     "tSPP.btKodeUK ," +
                     "0 as IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                     m_iKodeEstimasiPadaSAL.ToString() + "  AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSPP.cJumlah as cJumlah ,'Jurnal SP2D No.  ' + tSPP.sNoSP2D , 1 as iKelompok , tSPP.UnitAnggaran  FROM tSPP " +
                     "  WHERE tSPP.inourut = " + sNourut;




               _dbHelper.ExecuteNonQuery(SSQL);
               return true;

           } catch (Exception ex){
               _lastError = ex.Message;
               return false;
           }

       } 
       private bool JurnalPembayaranLS13PPKD(string sNourut ){


           try
           {
               string sNoJurnal;
               sNoJurnal = GetNextNo();

               //SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
               //        "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan ,bFromSKPD) SELECT " +
               //        sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
               //        ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSPP.dtBukukas as dtJurnal, 0 as iStatus, sNoSP2D as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
               //        "dtBukukas as  dtTanggalBukti,  snoSP2D as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SP2D).ToString() + " as iJenisSumber,0 as btPeruntukan, 1 as bPPKD, " +
               //        "1 as iKelompok , 0 as bPotongan,1 as bFromSKPD  FROM tSPP WHERE inourut =" + sNourut;

               SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                     "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                     sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                     ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSPP.dtBukukas as dtJurnal, 0 as iStatus, sNoSP2D as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                     "dtBukukas as  dtTanggalBukti,  sNoSP2D  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SP2D).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 2 as iKelompok , " +
                     "0 as bPotongan,0 , 1 as btUrut  FROM tSPP WHERE inourut =" + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);


               //SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
               //      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori" +
               //      ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,isnull(tSPP.IDurusan,0) as IDurusan " +
               //      ",isnull(tSPPRekening.btKodeUrusanPelaksana,0) as btKodeUrusanPelaksana , isnull(tSPP.btIDProgram,0) as btIDProgram , isnull(tSPP.btIDkegiatan,0) as btIDKegiatan , " +
               //      "tSPPRekening.IIDRekening AS iiDRekening  , 1 as iNoUrut , 1 As iDebet , tSPPRekening.cJumlah,'Jurnal SP2D No. ' + tSPP.sNoSP2D, 1 as iKelompok FROM tSPP INNER JOIN tSPPRekening  ON tSPP.inourut = tSPPRekening.inourut " +
               //      "  WHERE tSPP.inourut = " + sNourut;

               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                    "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSPP.IDDInas ," +
                    "tSPP.btKodeUK ," +
                    "tSPPRekening.IDUrusan ,tSPPRekening.IDProgram , tSPPRekening.IDkegiatan ,tSPPRekening.IDSubkegiatan,  " +
                    " tSPPRekening.IIDRekening AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSPPRekening.cJumlah,'Jurnal SP2D No.  ' + tSPP.sNoSP2D , 2 as iKelompok, tSPP.UnitAnggaran  FROM tSPP INNER JOIN tSPPRekening " +
                    " ON tSPP.inourut = tSPPRekening.inourut   WHERE tSPP.inourut = " + sNourut;


               _dbHelper.ExecuteNonQuery(SSQL);



               //SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
               //      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori" +
               //      ",tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,isnull(tSPPRekening.IDurusan,0) as IDurusan " +
               //      ",isnull(tSPPRekening.btKodeUrusanPelaksana,0) as btKodeUrusanPelaksana,isnull( tSPPRekening.btIDProgram,0) as btIDProgram , isnull(tSPPRekening.btIDkegiatan,0) as btIDKegiatan , " +
               //       m_iKodeEstimasiPadaSAL.ToString() + " AS IIDRekening, 2 as iNoUrut , -1 As iDebet , SUM(tSPPRekening.cJumlah), tSPP.sKeteranganPekerjaan +' ' + tSPP.sNoSP2D as sKeterangan , 2 as iKelompok  FROM tSPP INNER JOIN tSPPRekening " +
               //      " ON tSPP.inourut = tSPPRekening.inourut  WHERE tSPP.inourut = " + sNourut +
               //      " GROUP BY  tSPP.btKodekategori" +
               //      ",tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,isnull(tSPPRekening.IDurusan ,0) " +
               //      ",isnull(tSPPRekening.btKodeUrusanPelaksana,0) ,isnull( tSPPRekening.btIDProgram,0) , isnull(tSPPRekening.btIDkegiatan,0) , " +
               //      " tSPP.sKeteranganPekerjaan , tSPP.sNoSP2D ";
               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                   "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSPP.IDDInas ," +
                   "tSPP.btKodeUK ," +
                   "tSPPRekening.IDUrusan ,tSPPRekening.IDProgram , tSPPRekening.IDkegiatan ,tSPPRekening.IDSubkegiatan,  " +
                   m_iKodeEstimasiPadaSAL.ToString() + " AS iiDRekening , 1 as iNoUrut , -1 As iDebet , tSPPRekening.cJumlah,'Jurnal SP2D No.  ' + tSPP.sNoSP2D , 2 as iKelompok, tSPP.UnitAnggaran  FROM tSPP INNER JOIN tSPPRekening " +
                   " ON tSPP.inourut = tSPPRekening.inourut   WHERE tSPP.inourut = " + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);




               sNoJurnal = GetNextNo();


               //SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
               //      "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan ,bFromSKPD) SELECT " +
               //      sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
               //      ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSPP.dtBukukas as dtJurnal, 0 as iStatus, sNobukti as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
               //      "dtBukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SP2D).ToString() + " as iJenisSumber,0 as btPeruntukan, bPPKD as bPPKD, 2 as iKelompok , " +
               //      "0 as bPotongan,1 as bFromSKPD FROM tSPP WHERE inourut =" + sNourut;
               SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                    "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                    sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                    ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSPP.dtBukukas as dtJurnal, 0 as iStatus, sNoSP2D as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                    "dtBukukas as  dtTanggalBukti,  sNoSP2D  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SP2D).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 2 as iKelompok , " +
                    "0 as bPotongan,0 , 1 as btUrut  FROM tSPP WHERE inourut =" + sNourut;


               _dbHelper.ExecuteNonQuery(SSQL);

               //SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
               //        "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori " +
               //        ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPPRekening.btKodeUK ,tSPPRekening.IDurusan as IDurusan " +
               //        ",tSPPRekening.btKodeUrusanPelaksana as btKodeUrusanPelaksana , tSPPRekening.btIDProgram as btIDProgram , tSPPRekening.btIDKegiatan as btIDKegiatan , " +
               //        "KOR_LRA_LO.IIDRekeningLO AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSPPRekening.cJumlah, 'Jurnal SP2D no  ' + sNoSP2D , 2 as iKelompok  FROM tSPP INNER JOIN tSPPRekening " +
               //        " ON tSPP.inourut= tSPPRekening.inourut  inner Join KOR_LRA_LO on KOR_LRA_LO.IIDrekening= tSPPRekening.IIDRekening WHERE tSPP.inourut = " + sNourut + " AND tSPP.inobast =0";
               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
           "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSPP.IDDInas ," +
           "tSPP.btKodeUK ," +
           "tSPPRekening.IDUrusan ,tSPPRekening.IDProgram , tSPPRekening.IDkegiatan ,tSPPRekening.IDSubkegiatan,  " +
           "KOR_LRA_LO.IIDRekeningLO   AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSPPRekening.cJumlah,'Jurnal SP2D No.  ' + tSPP.sNoSP2D , 2 as iKelompok, tSPP.UnitAnggaran  FROM tSPP INNER JOIN tSPPRekening " +
           " ON tSPP.inourut = tSPPRekening.inourut inner Join KOR_LRA_LO on KOR_LRA_LO.IIDrekening= tSPPRekening.IIDRekening  WHERE tSPP.inourut = " + sNourut;
           
               _dbHelper.ExecuteNonQuery(SSQL);

               //SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
               //      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan,iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori" +
               //      ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,isnull(tSPP.IDurusan,0) as IDurusan " +
               //      ",isnull(tSPP.btKodeUrusanPelaksana ,0) as btKodeUrusanPelaksana , isnull(tSPP.btIDProgram ,0) as btIDProgram , isnull(tSPP.btIDkegiatan,0) as btIDKegiatan , " +
               //       m_iRekKasda.ToString() + "  AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSPP. cJumlah,'Jurnal SP2D No. ' + tSPP.sNoSP2d , 2 as iKelompok FROM tSPP WHERE tSPP.inourut = " + sNourut;
               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
         "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSPP.IDDInas ," +
         "tSPP.btKodeUK ," +
         "tSPPRekening.IDUrusan ,tSPPRekening.IDProgram , tSPPRekening.IDkegiatan ,tSPPRekening.IDSubkegiatan,  " +
         m_iRekKasda.ToString() + "  AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSPP.cJumlah,'Jurnal SP2D No.  ' + tSPP.sNoSP2D , 2 as iKelompok, tSPP.UnitAnggaran  FROM tSPP INNER JOIN tSPPRekening " +
         " ON tSPP.inourut = tSPPRekening.inourut   WHERE tSPP.inourut = " + sNourut;
           
               _dbHelper.ExecuteNonQuery(SSQL);
               return true;
           }
           catch (Exception ex)
           {
               _lastError = ex.Message;
               return false;
           }

          }

       
        private  bool ProcessJurnalSetorPajak(string sNourut){

            try
            {
                string sNoJurnal;

                sNoJurnal = GetNextNo();

                //SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                //      "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan) SELECT " +
                //      sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                //      ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSetor.dtBukukas as dtJurnal, 0 as iStatus, sNobukti as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + "  as btJenisJurnal," +
                //      "dtBukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SETOR).ToString() +
                //      " as iJenisSumber,0 as btPeruntukan, bPPKD, 1 as iKelompok, 1 as bPotongan FROM tSetor WHERE inourut =" + sNourut;

                SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                  "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                  sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                  ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSetor.dtBukukas as dtJurnal, 0 as iStatus, sNobukti as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                  "dtBukukas as  dtTanggalBukti,  sNobukti  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SETOR).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 1 as iKelompok , " +
                  "1 as bPotongan,0 , 1 as btUrut  FROM tSetor WHERE inourut =" + sNourut;


                _dbHelper.ExecuteNonQuery(SSQL);




                //SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                //      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSetor.btKodekategori" +
                //      ", tSetor.btKodeUrusan, tSetor.btKodeSKPD, tSetor.btKodeUK ,tSetor.IDurusan as IDurusan " +
                //      ",tSetor.btKodeUrusanPelaksana as btKodeUrusanPelaksana , tSetor.btIDProgram as btIDProgram , tSetor.btIDkegiatan as btIDKegiatan , " +
                //      " tSetorRekening.IIDRekening AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSetorRekening.cJumlah,'Jurnal Setor Pajak no  ' + tSetor.sNoBukti ,1 as iKelompok FROM tSetor INNER JOIN tSetorRekening " +
                //      " ON tSetor.inourut = tSetorRekening.inourut  WHERE tSetor.inourut = " + sNourut;

                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSetor.IDDInas ," +
                     "tSetor.btKodeUK ," +
                     "tSetor.IDUrusan ,tSetor.IDProgram , tSetor.IDkegiatan ,tSetor.IDSubkegiatan,  " +
                     " MapPotongan.IDNeraca AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSetorRekening.cJumlah,'Jurnal Setor Pajak No.  ' + tSetor.snoBukti, 1 as iKelompok, tSetor.UnitAnggaran  FROM tSetor INNER JOIN tSetorRekening " +
                     " ON tSetor.inourut = tSetorRekening.inourut INNER JOIN MapPotongan on MapPotongan.IdPotongan= tSetorRekening.iidRekening  WHERE tSetor.inourut = " + sNourut;

               


                _dbHelper.ExecuteNonQuery(SSQL);

                //SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                //      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSetor.btKodekategori" +
                //      ", tSetor.btKodeUrusan, tSetor.btKodeSKPD, tSetor.btKodeUK ,tSetor.IDurusan as IDurusan" +
                //      ",tSetor.btKodeUrusanPelaksana as btKodeUrusanPelaksana , tSetor.btIDProgram as btIDProgram , tSetor.btIDkegiatan as btIDKegiatan , " +
                //      " mKasBendahara.IIDrekening AS iiDRekening , 2 as iNoUrut , -1 As iDebet , sum(tSetorRekening.cJumlah) as cJumlah ,'Jurnal Setor pajak no  ' + tSetor.sNoBukti , 2 as iKelompok FROM tSetor INNER JOIN mkasbendahara " +
                //      " ON tSetor.IDDInas = mkasbendahara.IDDInas and tSetor.bPPKD = mkasbendahara.bPPKD inner join tSetorRekening ON tSetor.inourut= tSetorRekening.inourut WHERE tSetor.inourut = " + sNourut +
                //      " AND mkasbendahara.btJenis=2 " +
                //      " GROUP BY tSetor.btKodekategori,tSetor.btKodeUrusan, tSetor.btKodeSKPD, tSetor.btKodeUK ,tSetor.IDurusan " +
                //      ",tSetor.btKodeUrusanPelaksana , tSetor.btIDProgram , tSetor.btIDkegiatan , " +
                //      " mKasBendahara.IIDrekening , tSetor.sNoBukti ";

                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSetor.IDDInas ," +
                     "tSetor.btKodeUK ," +
                     "tSetor.IDUrusan ,tSetor.IDProgram , tSetor.IDkegiatan ,tSetor.IDSubkegiatan,  " +
                     m_KasBendaharaPengeluaran.ToString() + "  AS iiDRekening , 2 as iNoUrut , -1 As iDebet ,"+
                     " tSetor.cJumlah,'Jurnal Setor Pajak No.  ' + tSetor.snoBukti, 2 as iKelompok, tSetor.UnitAnggaran  FROM tSetor " +
                     "  WHERE tSetor.inourut = " + sNourut;



                _dbHelper.ExecuteNonQuery(SSQL);
                return true;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }

       private bool ProcessJurnalSETOR(string sNourut , int iJenis,int bFromSKPD ){
            
                Setor oSetor = new Setor();
                
                switch (iJenis) { 
            
                    case SETOR_STS:
                        JurnalSetorPendapatan(sNourut, bFromSKPD);
                        break;      
                    case SETOR_UYHD:
                            JurnalUYHD13 (sNourut, bFromSKPD);
                        break;

                    case SETOR_CP:
                        if (bFromSKPD == 0)
                            JurnalCP13(sNourut,true, bFromSKPD);

                        else
                            JurnalCP13PPKD(sNourut, bFromSKPD);
                        
                        break;
            //        Case SETOR_PAJAK
            //                JurnalSetorpajak sNourut, bFromSKPD
                  
                
                }

                  
            //End Select

                return true;
    
    

       }
       private bool JurnalSetorPendapatan(string sNourut, int bFromSKPD)
       {
           try
           {
    //                  
               string sNoJurnal;
                sNoJurnal = GetNextNo();
               SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                    "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                    sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                    ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSetor.dtbukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENERIMAAN).ToString() + " as btJenisJurnal," +
                    "dtbukukas as  dtTanggalBukti,  sNoBukti  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SETOR).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 2 as iKelompok , " +
                    "0 as bPotongan,0 , 1 as btUrut  FROM tSetor  WHERE inourut =" + sNourut;
               _dbHelper.ExecuteNonQuery(SSQL);

          SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "btKodekategoripelaksana,btKodeUrusanPelaksana,btKodekategori,btKodeUrusan,btKodeSKPD,"+
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSetor.IDDInas ," +
                     "0 as btKodeUK ," +
                     "tSetor.IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                      " dbo.ToKodeKategoriPelaksana(tSetor.IDDInas) as btKodekategoripelaksana," +
                     "dbo.ToKodeUrusanPelaksana (tSetor.IDDInas) as btKodeUrusanPelaksana," +
                     "dbo.ToKodekaTegori(tSetor.IDDInas) as btKodekategori," +
                     "dbo.ToKodeUrusan (tSetor.IDDInas) as btKodeUrusan," +
                   "dbo.ToKodeSKPD (tSetor.IDDInas) as btKodeSKPD," +
                     m_iRekRKPPKD.ToString() + "  AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSetor.cJumlah,'Jurnal Setor STS TBP  ' + sNoBukti, 2 as iKelompok, 0 as UnitAnggaran  FROM tSetor " +
                     " WHERE tSetor.inourut = " + sNourut;

         _dbHelper.ExecuteNonQuery(SSQL);
      

      
          SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                         "btKodekategoripelaksana,btKodeUrusanPelaksana,btKodekategori,btKodeUrusan,btKodeSKPD," +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSetor.IDDInas ," +
                     "0 as btKodeUK ," +
                     "tSetor.IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                     " dbo.ToKodekategoriPelaksana(tSetor.IDDInas) as btKodekategoripelaksana," +
                     "dbo.ToKodeUrusanPelaksana (tSetor.IDDInas) as btKodeUrusanPelaksana," +
                     "dbo.ToKodekaTegori(tSetor.IDDInas) as btKodekategori," +
                     "dbo.ToKodeUrusan (tSetor.IDDInas) as btKodeUrusan," +
                   "dbo.ToKodeSKPD (tSetor.IDDInas) as btKodeSKPD," +
                      m_KasBendaharaPenerimaan.ToString() + "  AS iiDRekening , 1 as iNoUrut , -1 As iDebet , tSetor.cJumlah,'Jurnal Setor STS TBP  ' + sNoBukti, 2 as iKelompok, 0 as UnitAnggaran  FROM tSetor " +
                     " WHERE tSetor.inourut = " + sNourut;

          _dbHelper.ExecuteNonQuery(SSQL);

      // PPKD 


      
          sNoJurnal = GetNextNo();
          SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
               "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
               sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
               ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSetor.dtbukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENERIMAAN).ToString() + " as btJenisJurnal," +
               "dtbukukas as  dtTanggalBukti,  sNoBukti  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SETOR).ToString() + " as iJenisSumber,0 as btPeruntukan, 1 as bPPKD, 2 as iKelompok , " +
               "0 as bPotongan,0 , 1 as btUrut  FROM tSetor  WHERE inourut =" + sNourut;
          _dbHelper.ExecuteNonQuery(SSQL);

          SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "btKodekategoripelaksana,btKodeUrusanPelaksana,btKodekategori,btKodeUrusan,btKodeSKPD," +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSetor.IDDInas ," +
                     "0 as btKodeUK ," +
                     "tSetor.IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                      " dbo.ToKodeKategoriPelaksana(tSetor.IDDInas) as btKodekategoripelaksana," +
                     "dbo.ToKodeUrusanPelaksana (tSetor.IDDInas) as btKodeUrusanPelaksana," +
                     "dbo.ToKodekaTegori(tSetor.IDDInas) as btKodekategori," +
                     "dbo.ToKodeUrusan (tSetor.IDDInas) as btKodeUrusan," +
                   "dbo.ToKodeSKPD (tSetor.IDDInas) as btKodeSKPD," +
                   m_iRekKasda.ToString() + "  AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSetor.cJumlah,'Jurnal Setor STS TBP  ' + sNoBukti, 2 as iKelompok, 0 as UnitAnggaran  FROM tSetor " +
                     " WHERE tSetor.inourut = " + sNourut;

          _dbHelper.ExecuteNonQuery(SSQL);



          SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                         "btKodekategoripelaksana,btKodeUrusanPelaksana,btKodekategori,btKodeUrusan,btKodeSKPD," +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSetor.IDDInas ," +
                     "0 as btKodeUK ," +
                     "tSetor.IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                     " dbo.ToKodekategoriPelaksana(tSetor.IDDInas) as btKodekategoripelaksana," +
                     "dbo.ToKodeUrusanPelaksana (tSetor.IDDInas) as btKodeUrusanPelaksana," +
                     "dbo.ToKodekaTegori(tSetor.IDDInas) as btKodekategori," +
                     "dbo.ToKodeUrusan (tSetor.IDDInas) as btKodeUrusan," +
                   "dbo.ToKodeSKPD (tSetor.IDDInas) as btKodeSKPD," +
                        m_KasBendaharaRKPPKD.ToString() + "  AS iiDRekening , 1 as iNoUrut , -1 As iDebet , tSetor.cJumlah,'Jurnal Setor STS TBP  ' + sNoBukti, 2 as iKelompok, 0 as UnitAnggaran  FROM tSetor " +
                     " WHERE tSetor.inourut = " + sNourut;

          _dbHelper.ExecuteNonQuery(SSQL);

     
               
      




               return true;
           }
           catch (Exception ex)
           {
               return false;
           }

       }
       private bool JurnalUYHD13(string sNourut , int bFromSKPD ){
    
            string  sNoJurnal ;
            try{
    
                sNoJurnal = GetNextNo();
                SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun,IDDInas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                         "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, bFromSKPD) SELECT " +
                         sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun,IDDInas, btKodeUK " +
                         ", "  + DateTime.Now.Date.ToSQLFormat()  +" AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, "  + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString()  +" as btJenisJurnal," +
                         "dtbukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , "  +((int)JENIS_SUMBERJURNAL.E_SUMBER_SETOR).ToString()  +" as iJenisSumber,0 as btPeruntukan,0 as  bPPKD, 1 as ikelompok , 0 as bPotongan, "  +  bFromSKPD.ToString()  +" FROM tSetor WHERE inourut ="  +sNourut;
        
                _dbHelper.ExecuteNonQuery(SSQL);
                
              
                        SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                        "btKodekategoripelaksana,btKodeUrusanPelaksana,btKodekategori,btKodeUrusan,btKodeSKPD," +
                        "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSetor.IDDInas ," +
                        "0 as btKodeUK ," +
                        "tSetor.IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                         " dbo.ToKodeKategoriPelaksana(tSetor.IDDInas) as btKodekategoripelaksana," +
                        "dbo.ToKodeUrusanPelaksana (tSetor.IDDInas) as btKodeUrusanPelaksana," +
                        "dbo.ToKodekaTegori(tSetor.IDDInas) as btKodekategori," +
                        "dbo.ToKodeUrusan (tSetor.IDDInas) as btKodeUrusan," +
                      "dbo.ToKodeSKPD (tSetor.IDDInas) as btKodeSKPD," +
                        m_iRekRKPPKD.ToString() + "  AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSetor.cJumlah,'Jurnal Setor STS TBP  ' + sNoBukti, 2 as iKelompok, 0 as UnitAnggaran  FROM tSetor " +
                        " WHERE tSetor.inourut = " + sNourut;

                     //   SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                     //     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT "  +sNoJurnal  +" as inojurnal, tsetor.btKodekategori" +
                     //     ", tsetor.btKodeUrusan, tsetor.btKodeSKPD, tsetor.btKodeUK ,tsetor.IDurusan " +
                     //     ",tsetor.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                      //    m_iRekRKPPKD.ToString()  +" AS iiDRekening , 1 as iNoUrut , 1 As iDebet , cJumlah, 'Jurnal Setor   ' + sNoBukti , 1 as Ikelompok FROM tsetor WHERE inourut = "  +sNourut;
          
                           _dbHelper.ExecuteNonQuery(SSQL);
                    
                

           
                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                       "btKodekategoripelaksana,btKodeUrusanPelaksana,btKodekategori,btKodeUrusan,btKodeSKPD," +
                       "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSetor.IDDInas ," +
                       "0 as btKodeUK ," +
                       "tSetor.IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                        " dbo.ToKodeKategoriPelaksana(tSetor.IDDInas) as btKodekategoripelaksana," +
                       "dbo.ToKodeUrusanPelaksana (tSetor.IDDInas) as btKodeUrusanPelaksana," +
                       "dbo.ToKodekaTegori(tSetor.IDDInas) as btKodekategori," +
                       "dbo.ToKodeUrusan (tSetor.IDDInas) as btKodeUrusan," +
                     "dbo.ToKodeSKPD (tSetor.IDDInas) as btKodeSKPD," +
                       m_KasBendaharaPengeluaran.ToString() + "  AS iiDRekening , 1 as iNoUrut , -1 As iDebet , tSetor.cJumlah,'Jurnal Setor STS TBP  ' + sNoBukti, 2 as iKelompok, 0 as UnitAnggaran  FROM tSetor " +
                       " WHERE tSetor.inourut = " + sNourut;
                  _dbHelper.ExecuteNonQuery(SSQL);
               
                
                           sNoJurnal = GetNextNo();

                           SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                                "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, sUraian,bFromSKPD ) SELECT " +
                                sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                                ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                                "dtbukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SETOR).ToString() + " as iJenisSumber,1 as btPeruntukan,1 as  bPPKD, 1 as ikelompok ," +
                                "0 as bPotongan,sKeterangan as sUraian, " + bFromSKPD.ToString() + "   FROM tSetor WHERE inourut =" + sNourut;
                                 _dbHelper.ExecuteNonQuery(SSQL);

                             //    SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                              //  "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT "  +sNoJurnal  +" as inojurnal,IDDinas, IDurusan " +
                              //  ", 0 as btIDProgram , 0 as btIDKegiatan , " +
                              //  m_iRekKasda.ToString()  +" AS iiDRekening , 1 as iNoUrut , 1 As iDebet , cJumlah, 'Jurnal Setor   ' + sNoBukti , 1 as Ikelompok FROM tsetor WHERE inourut = "  +sNourut;
                                 SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                                     "btKodekategoripelaksana,btKodeUrusanPelaksana,btKodekategori,btKodeUrusan,btKodeSKPD," +
                                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSetor.IDDInas ," +
                                     "0 as btKodeUK ," +
                                     "tSetor.IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                                      " dbo.ToKodeKategoriPelaksana(tSetor.IDDInas) as btKodekategoripelaksana," +
                                     "dbo.ToKodeUrusanPelaksana (tSetor.IDDInas) as btKodeUrusanPelaksana," +
                                     "dbo.ToKodekaTegori(tSetor.IDDInas) as btKodekategori," +
                                     "dbo.ToKodeUrusan (tSetor.IDDInas) as btKodeUrusan," +
                                   "dbo.ToKodeSKPD (tSetor.IDDInas) as btKodeSKPD," +
                                     m_iRekKasda.ToString() + "  AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSetor.cJumlah,'Jurnal Setor STS TBP  ' + sNoBukti, 2 as iKelompok, 0 as UnitAnggaran  FROM tSetor " +
                                     " WHERE tSetor.inourut = " + sNourut; 

                             _dbHelper.ExecuteNonQuery(SSQL);
                             SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                                "btKodekategoripelaksana,btKodeUrusanPelaksana,btKodekategori,btKodeUrusan,btKodeSKPD," +
                                "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSetor.IDDInas ," +
                                "0 as btKodeUK ," +
                                "tSetor.IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                                 " dbo.ToKodeKategoriPelaksana(tSetor.IDDInas) as btKodekategoripelaksana," +
                                "dbo.ToKodeUrusanPelaksana (tSetor.IDDInas) as btKodeUrusanPelaksana," +
                                "dbo.ToKodekaTegori(tSetor.IDDInas) as btKodekategori," +
                                "dbo.ToKodeUrusan (tSetor.IDDInas) as btKodeUrusan," +
                              "dbo.ToKodeSKPD (tSetor.IDDInas) as btKodeSKPD," +
                                m_KasBendaharaRKPPKD.ToString() + "  AS iiDRekening , 1 as iNoUrut , -1 As iDebet , tSetor.cJumlah,'Jurnal Setor STS TBP  ' + sNoBukti, 2 as iKelompok, 0 as UnitAnggaran  FROM tSetor " +
                                " WHERE tSetor.inourut = " + sNourut; 


                               // SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                               // "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok ) SELECT "  +sNoJurnal  +" as inojurnal,IDDInas,IDUrusan," +
                               // " , 0 as btIDProgram , 0 as btIDKegiatan , " +
                                //"mKasBendahara.IIDrekening AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tsetor.cJumlah,'Jurnal Setor no  ' + sNoBukti, 1 as iKelompok FROM tsetor INNER JOIN mKasBendahara " +
                                //" ON tsetor.IDDInas= mkasbendahara.IDDInas and tsetor.bPPKD = mkasbendahara.bPPKD WHERE tsetor.inourut = "  +sNourut +
                                //" AND mkasbendahara.btJenis=0";
            
        
        
            
            
                             _dbHelper.ExecuteNonQuery(SSQL);
                            
                            return true;

                    } catch(Exception ex){
                        _lastError = ex.Message;
                        return false;
                    }
       }


       private bool  JurnalCP13(string sNourut , bool m_AlsoBUD, int bFromSKPD ){
    
            string sNoJurnal ;
            try
            {
                sNoJurnal = GetNextNo();

                //SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                //          "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, sUraian, bFromSKPD ) SELECT " +
                //       sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                //      ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                //       "dtbukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SETOR).ToString() +
                //       " as iJenisSumber,0 as btPeruntukan, bPPKD as bPPKD, 1 as ikelompok , 0 as bPotongan,sKeterangan as sUraian , " +
                //       bFromSKPD.ToString() + "   FROM tSetor WHERE inourut =" + sNourut;

                SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                    "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                    sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                    ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSetor.dtBukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                    "dtBukukas as  dtTanggalBukti,  sNoBukti  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SETOR).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 1 as iKelompok , " +
                    "0 as bPotongan," + bFromSKPD.ToString() + " , 1 as btUrut  FROM tSetor WHERE inourut =" + sNourut;

                _dbHelper.ExecuteNonQuery(SSQL);


                //SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                //      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tsetor.IDDInas ," +
                //      "tsetor.IDUrusan, 0 as btIDProgram , 0 as btIDKegiatan , " +
                //      m_iKodeEstimasiPadaSAL.ToString() + " AS iiDRekening , 1 as iNoUrut , 1 As iDebet , cJumlah, 'Jurnal Setor No.' + sNoBukti , " +
                //      "1 as iKelompok FROM tsetor WHERE inourut = " + sNourut;

                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                    "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSetor.IDDInas ," +
                    "tSetor.btKodeUK ," +
                    "tSetor.IDUrusan ,tSetor.IDProgram , tSetor.IDkegiatan ,tSetor.IDSubkegiatan,  " +
                     m_iKodeEstimasiPadaSAL.ToString() + "  AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSetor.cJumlah,'Jurnal Setoor No.  ' + tSetor.sNoBukti, "+
                     " 1 as iKelompok, tSetor.UnitAnggaran  FROM tSetor  WHERE tSetor.inourut = " + sNourut;


                _dbHelper.ExecuteNonQuery(SSQL);


                //SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                //      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tsetor.IDDInas ," + 
                //      "tsetor.IDurusan , 0 as btIDProgram , 0 as btIDKegiatan , " +
                //      "tSetorRekening.IIDRekening AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSetorRekening.cJumlah,'Jurnal Setor No  ' + sNoBukti , " +
                //      "1 as iKelompok FROM tsetor INNER JOIN tSetorRekening " +
                //      " ON tsetor.inourut = tSetorRekening.inourut WHERE tsetor.inourut = " + sNourut;

                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSetor.IDDInas ," +
                     "tSetor.btKodeUK ," +
                     "tSetor.IDUrusan ,tSetor.IDProgram , tSetor.IDkegiatan ,tSetor.IDSubkegiatan,  " +
                     " tSetorRekening.IIDrekening AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSetorRekening.cJumlah,'Jurnal Setor No .  ' + tSetor.sNoBukti ,"+
                     "1 as iKelompok, tSetor.UnitAnggaran  FROM tSetor INNER JOIN tSetorRekening " +
                     " ON tSetor.inourut = tSetorRekening.inourut  WHERE tSetor.inourut = " + sNourut;



                _dbHelper.ExecuteNonQuery(SSQL);

                // '====================================================================

                sNoJurnal = GetNextNo();

                //'RB Peng ><LO


                //SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                //          "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, bFromSKPD ) SELECT " +
                //          sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                //        ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() +
                //        " as btJenisJurnal," +
                //        "dtbukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SETOR).ToString() +
                //        " as iJenisSumber,0 as btPeruntukan, bPPKD as bPPKD, 2 as ikelompok , 0 as bPotongan, " + bFromSKPD.ToString() +
                //        "   FROM tSetor WHERE inourut =" + sNourut;
                SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                    "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                    sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                    ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSetor.dtBukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                    "dtBukukas as  dtTanggalBukti,  sNoBukti  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SETOR).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 2 as iKelompok , " +
                    "0 as bPotongan," + bFromSKPD.ToString() + " , 2 as btUrut  FROM tSetor WHERE inourut =" + sNourut;



                _dbHelper.ExecuteNonQuery(SSQL);


                //SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                //      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tsetor.IDdinas ,tsetor.IDurusan," +
                //      "0 as btIDProgram , 0 as btIDKegiatan , " +
                //      "mKasBendahara.IIDrekening AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tsetor.cJumlah,'Jurnal Pengembalian No. ' + sNoBukti ,2 as iKelompok FROM tsetor INNER JOIN mKasBendahara " +
                //      " ON tsetor.IDDInas = mkasbendahara.IDDInas and tsetor.bPPKD = mkasbendahara.bPPKD WHERE tsetor.inourut = " + sNourut +
                //      " AND mkasbendahara.btJenis=2 ";

                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSetor.IDDInas ," +
                     "tSetor.btKodeUK ," +
                     "tSetor.IDUrusan ,tSetor.IDProgram , tSetor.IDkegiatan ,tSetor.IDSubkegiatan,  " +
                     "(Select IIDRekening from mkasbendahara where btJEnis=2 )  AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSetor.cJumlah,'Jurnal Pengembalian No. ' + sNoBukti , 2 as iKelompok , tSetor.UnitAnggaran FROM tSetor "+
                     " WHERE tSetor.inourut = " + sNourut;



                _dbHelper.ExecuteNonQuery(SSQL);

                //SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                //      " iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tsetor.IDDInas" +
                //      ", tsetor.IDUrusan, 0 as btIDProgram , 0 as btIDKegiatan , " +
                //      " KOR_LRA_LO.IIDRekeningLO  AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSetorRekening.cJumlah, 'Jurnal Pengembalian No ' + sNoBukti , 2 as iKelompok FROM tsetor INNER JOIN tSetorRekening " +
                //      " ON tsetor.inourut = tsetorRekening.inourut INNER join KOR_LRA_LO ON tSetorRekening.IIDRekening=KOR_LRA_LO.iiDRekening " +
                //      "WHERE tSetor.inourut = " + sNourut;


                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSetor.IDDInas ," +
                     "tSetor.btKodeUK ," +
                     "tSetor.IDUrusan ,tSetor.IDProgram , tSetor.IDkegiatan ,tSetor.IDSubkegiatan,  " +
                     " KOR_LRA_LO.IIDRekeningLO AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSetorRekening.cJumlah,'Jurnal Pengembalian  No.  ' + tSetor.sNoBukti , 2 as iKelompok, tSetor.UnitAnggaran  FROM tSetor INNER JOIN tSetorRekening " +
                     " ON tSetor.inourut = tSetorRekening.inourut  INNER JOIN KOR_LRA_LO ON KOR_LRA_LO.IIDRekening = tSetorRekening.IIDRekening WHERE tSetor.inourut = " + sNourut;



                _dbHelper.ExecuteNonQuery(SSQL);



                sNoJurnal = GetNextNo();
                //' Jurnal SKPD
                //' Kas di BP
                //'     RKPPKD >< Kas di BP



                //SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                //          "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, sUraian , bFromSKPD ) SELECT " +
                //           sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                //          ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, " +
                //          ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                //           "dtbukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SETOR).ToString() +
                //           " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 1 as ikelompok , 0 as bPotongan,sKeterangan as sUraian, " +
                //           bFromSKPD.ToString() + "    FROM tSetor WHERE inourut =" + sNourut;

                SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                    "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                    sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                    ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSetor.dtBukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                    "dtBukukas as  dtTanggalBukti,  sNoBukti  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SETOR).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 1 as iKelompok , " +
                    "0 as bPotongan," + bFromSKPD.ToString() + " , 3 as btUrut  FROM tSetor WHERE inourut =" + sNourut;



                _dbHelper.ExecuteNonQuery(SSQL);
                //(((((
                //SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                //      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tsetor.IDDInas " +
                //      ", tsetor.IDurusan, 0 as btIDProgram , 0 as btIDKegiatan , " +
                //      m_iRekRKPPKD.ToString() + " AS iiDRekening , 1 as iNoUrut , 1 As iDebet , cJumlah, 'Jurnal Pengembalian No. ' + sNoBukti , " +
                //      "1 as iKelompok FROM tsetor WHERE inourut = " + sNourut;

                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                   "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSetor.IDDInas ," +
                   "tSetor.btKodeUK ," +
                   "tSetor.IDUrusan ,tSetor.IDProgram , tSetor.IDkegiatan ,tSetor.IDSubkegiatan,  " +
                   m_iRekRKPPKD.ToString() + "  AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSetor.cJumlah,'Jurnal Pengembalian No. ' + sNoBukti , "+
                   " 1 as iKelompok , tSetor.UnitAnggaran FROM tSetor  WHERE tSetor.inourut = " + sNourut;

                _dbHelper.ExecuteNonQuery(SSQL);




                //SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                //      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tsetor.IDDinas" +
                //      ", tsetor.IDurusan, 0 as btIDProgram , 0 as btIDKegiatan , " +
                //      "mKasBendahara.IIDrekening AS iiDRekening , 1 as iNoUrut , -1 As iDebet , tsetor.cJumlah,'Jurnal Pengembalian No. ' + sNoBukti ,2 as iKelompok FROM tsetor INNER JOIN mKasBendahara " +
                //      " ON tsetor.IDDInas = mkasbendahara.IDDInas AND tsetor.bPPKD = mkasbendahara.bPPKD WHERE tsetor.inourut = " + sNourut +
                //      " AND mkasbendahara.btJenis=2 ";

                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                   "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSetor.IDDInas ," +
                   "tSetor.btKodeUK ," +
                   "tSetor.IDUrusan ,tSetor.IDProgram , tSetor.IDkegiatan ,tSetor.IDSubkegiatan,  " +
                   "(Select IIDRekening from mkasbendahara where btJEnis=2 )   AS iiDRekening , 1 as iNoUrut , -1 As iDebet , tSetor.cJumlah,'Jurnal Pengembalian No. ' + sNoBukti , " +
                   " 2 as iKelompok , tSetor.UnitAnggaran FROM tSetor  WHERE tSetor.inourut = " + sNourut;



                _dbHelper.ExecuteNonQuery(SSQL);


                if (m_AlsoBUD == true)
                {

                    sNoJurnal = GetNextNo();
                    //SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                    //    "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, sUraian, bFromSKPD ) SELECT " +
                    //    sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                    //    ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                    //    "dtbukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SETOR).ToString() + " as iJenisSumber,1 as btPeruntukan, " +
                    //    "1 as bPPKD, 1 as ikelompok , 0 as bPotongan , sKeterangan as sUraian, " + bFromSKPD.ToString() + "   FROM tSetor WHERE inourut =" + sNourut;

                    SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSetor.dtBukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                "dtBukukas as  dtTanggalBukti,  sNoBukti  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SETOR).ToString() + " as iJenisSumber,0 as btPeruntukan, 1 as bPPKD, 2 as iKelompok , " +
                "0 as bPotongan," + bFromSKPD.ToString() + " , 3 as btUrut  FROM tSetor WHERE inourut =" + sNourut;



                    _dbHelper.ExecuteNonQuery(SSQL);



                    //SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                    //      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal," +
                    //       "tSetor.IDDinas,tSetor.IDurusan, 0 as btIDProgram , 0 as btIDKegiatan , " +
                    //      m_iRekKasda.ToString() + " AS iiDRekening , 1 as iNoUrut , 1 As iDebet , cJumlah, 'Jurnal Pengembalian No. ' + sNoBukti , " +
                    //      "1 as iKelompok FROM tsetor WHERE inourut = " + sNourut;

                    SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                   "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSetor.IDDInas ," +
                   "tSetor.btKodeUK ," +
                   "tSetor.IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                   m_iRekKasda.ToString() + "  AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSetor.cJumlah,'Jurnal Pengembalian No. ' + sNoBukti , " +
                   " 1 as iKelompok , tSetor.UnitAnggaran FROM tSetor  WHERE tSetor.inourut = " + sNourut;

                    _dbHelper.ExecuteNonQuery(SSQL);


                 

                    //SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                    //    "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSetor.IDDInas," +
                    //      " tSetor.IDUrusan, 0 as btIDProgram , 0 as btIDKegiatan , " +
                    //    "mKasBendahara.IIDrekening AS iiDRekening , 1 as iNoUrut , -1 As iDebet , tsetor.cJumlah,'Jurnal Pengembalian No. ' + sNoBukti ,2 as iKelompok FROM tsetor INNER JOIN mKasBendahara " +
                    //    " ON tsetor.IDDInas = mkasbendahara.IDDInas and tsetor.bPPKD = mkasbendahara.bPPKD WHERE tsetor.inourut = " + sNourut +
                    //    " AND mkasbendahara.btJenis=0";

                    SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                   "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSetor.IDDInas ," +
                   "tSetor.btKodeUK ," +
                   "tSetor.IDUrusan ,0 AS IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                   "(Select IIDRekening from mkasbendahara where btJenis=0 )   AS iiDRekening , 1 as iNoUrut , -1 As iDebet , tSetor.cJumlah,'Jurnal Pengembalian No. ' + sNoBukti , " +
                   " 2 as iKelompok , tSetor.UnitAnggaran FROM tSetor  WHERE tSetor.inourut = " + sNourut;



                    _dbHelper.ExecuteNonQuery(SSQL);





                }
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
      
        }

       private bool JurnalCP13PPKD(string sNourut , int bFromSKPD ){
    
           try{
                        string  sNoJurnal ;
                        sNoJurnal = GetNextNo();
                        SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun,IDDInas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                                  "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, sUraian, bFromSKPD ) SELECT " +
                                   sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun,IDDInas, btKodeUK " +
                                  ", "  +DateTime.Now.Date.ToSQLFormat()  +" AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, "  + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString()  +" as btJenisJurnal," +
                                   "dtbukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , "  +((int)JENIS_SUMBERJURNAL.E_SUMBER_SETOR).ToString()  + 
                                   " as iJenisSumber,0 as btPeruntukan, 1 as bPPKD, 1 as ikelompok , 0 as bPotongan,sKeterangan as sUraian , "  + 
                                   bFromSKPD.ToString()  +"   FROM tSetor WHERE inourut ="  +sNourut;
            
    
                     _dbHelper.ExecuteNonQuery(SSQL);

                     SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                              "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT "  +sNoJurnal  +" as inojurnal, tsetor.IDDInas" +
                              ", tsetor.IDurusan, 0 as btIDProgram , 0 as btIDKegiatan , " +
                              m_iKodeEstimasiPadaSAL  +" AS iiDRekening , 1 as iNoUrut , 1 As iDebet , cJumlah, 'Jurnal Setor No.' + sNoBukti , " +
                              "1 as iKelompok FROM tsetor WHERE inourut = "  +sNourut;
          
                           _dbHelper.ExecuteNonQuery(SSQL);


                           SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                              "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT "  +sNoJurnal  +" as inojurnal, tsetor.IDDInas" +
                              ", tsetor.IDurusan, 0 as btIDProgram , 0 as btIDKegiatan , " +
                              "tSetorRekening.IIDRekening AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSetorRekening.cJumlah,'Jurnal Setor No  ' + sNoBukti , 1 as iKelompok FROM tsetor INNER JOIN tSetorRekening " +
                              " ON tsetor.inourut = tSetorRekening.inourut WHERE tsetor.inourut = "  +sNourut;
          
          
            
                     _dbHelper.ExecuteNonQuery(SSQL);
                   
                        //'====================================================================
    
                        sNoJurnal = GetNextNo();
    
                       // 'RB Peng ><LO
    
                        SSQL = "INSERT INTO "  +m_sNamatabelJurnal  +" (iNoJurnal, iTahun,IDDInas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                                  "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, bFromSKPD ) SELECT " +
                                  sNoJurnal  +" as inojurnal, "  +Tahun.ToString()  +" AS iTahun , IDDInas, btKodeUK" +
                                ", "  + DateTime.Now.Date.ToSQLFormat()  +" AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, "  + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString()  +" as btJenisJurnal," +
                                "dtbukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , "  +((int)JENIS_SUMBERJURNAL.E_SUMBER_SETOR).ToString()  + 
                                " as iJenisSumber,0 as btPeruntukan, bPPKD as bPPKD, 2 as ikelompok , 0 as bPotongan, "  + bFromSKPD.ToString()  + 
                                "   FROM tSetor WHERE inourut ="  +sNourut;
            
    
                     _dbHelper.ExecuteNonQuery(SSQL);


                     SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                              "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT "  +sNoJurnal  +" as inojurnal, tsetor.IDDInas" +
                              ", tsetor.IDurusan, 0 as IDProgram , 0 as IDKegiatan , " +
                              "mKasBendahara.IIDrekening AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tsetor.cJumlah,'Jurnal Pengembalian No. ' + sNoBukti ,2 as iKelompok FROM tsetor INNER JOIN mKasBendahara " +
                              " ON tsetor.IDDInas = mkasbendahara.IDDInas and tsetor.bPPKD = mkasbendahara.bPPKD WHERE tsetor.inourut = "  +sNourut +
                              " AND mkasbendahara.btJenis=2 ";
          
                     _dbHelper.ExecuteNonQuery(SSQL);

                     SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                              " iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT "  +sNoJurnal  +" as inojurnal, tsetor.IDDInas" +
                              ", tsetor.IDurusan, 0 as IDProgram , 0 as IDKegiatan , " +
                              " KOR_LRA_LO.IIDRekeningLO  AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSetorRekening.cJumlah, 'Jurnal Pengembalian No ' + sNoBukti , 2 as iKelompok FROM tsetor INNER JOIN tSetorRekening " +
                              " ON tsetor.inourut = tsetorRekening.inourut INNER join KOR_LRA_LO ON tSetorRekening.IIDRekening=KOR_LRA_LO.iiDRekening WHERE tSetor.inourut = "  +sNourut;
      
                     _dbHelper.ExecuteNonQuery(SSQL);
      
      
      
                        sNoJurnal = GetNextNo();
                        //' Jurnal SKPD
                        //' Kas di BP
                        //'     RKPPKD >< Kas di BP



                        SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun,IDDInas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                                  "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, sUraian , bFromSKPD ) SELECT " +
                                   sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun,IDDInas, btKodeUK " +
                                  ", "  + DateTime.Now.Date.ToSQLFormat()  +" AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, "  + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString()  +" as btJenisJurnal," +
                                   "dtbukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , "  +((int)JENIS_SUMBERJURNAL.E_SUMBER_SETOR).ToString()  +" as iJenisSumber,0 as btPeruntukan, 1 as bPPKD," +
                                   "1 as ikelompok , 0 as bPotongan,sKeterangan as sUraian, "  +bFromSKPD.ToString()  +"    FROM tSetor WHERE inourut ="  +sNourut;
            
    
                        _dbHelper.ExecuteNonQuery(SSQL);

                        SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                              "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT "  +sNoJurnal  +" as inojurnal, tsetor.IDDInas" +
                              ", tsetor.IDurusan , 0 as btIDProgram , 0 as btIDKegiatan , " +
                              m_iRekKasda.ToString()  +" AS iiDRekening , 1 as iNoUrut , 1 As iDebet , cJumlah, 'Jurnal Pengembalian No. ' + sNoBukti , " +
                              "1 as iKelompok FROM tsetor WHERE inourut = "  +sNourut;
          
                           _dbHelper.ExecuteNonQuery(SSQL);



                           SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                              "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT "  +sNoJurnal  +" as inojurnal, tsetor.IDDInas " +
                              ", tsetor.IDurusan, 0 as IDProgram , 0 as IDKegiatan , " +
                              "mKasBendahara.IIDrekening AS iiDRekening , 1 as iNoUrut , -1 As iDebet , tsetor.cJumlah,'Jurnal Pengembalian No. ' + sNoBukti ,2 as iKelompok FROM tsetor INNER JOIN mKasBendahara " +
                              " ON tsetor.IDDInas = mkasbendahara.IDDInas  and tsetor.bPPKD = mkasbendahara.bPPKD WHERE tsetor.inourut = "  +sNourut +
                              " AND mkasbendahara.btJenis=2 ";
          
                     _dbHelper.ExecuteNonQuery(SSQL);
                                return true;
      
           } catch(Exception ex)
           {
               _lastError = ex.Message;
               return false;
           }
       }


       public bool ProcessJurnalPotonganPANJAR13(string sNourut)
       {
           try
           {
               string sNoJurnal;

               sNoJurnal = GetNextNo();

               SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, IDDInas , btKodeUK,iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                     "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan) SELECT " +
                     sNoJurnal + " as inojurnal, tPanjar.IDDInas ,tPanjar.btKodeUK ," + Tahun.ToString() + " AS iTahun " +
                     ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tPanjar.dtBukukas as dtJurnal, 0 as iStatus, sNobukti as sNobukti, " +
                    ((int) JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + "  as btJenisJurnal," +
                     "dtBukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_PANJAR).ToString() +
                     " as iJenisSumber,0 as btPeruntukan, bPPKD, 1 as iKelompok, 1 as bPotongan FROM tPanjar WHERE inourut =" + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);


               //SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan,IDSUbKegiatan, " +
               //     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tPanjar.IDDInas" +
               //     ", tPanjar.IDUrusan, tPanjar.IDProgram , tPanjar.IDkegiatan ,tPanjar.IDSubkegiatan , " +
               //     " mKasBendahara.IIDrekening AS iiDRekening , 1 as iNoUrut , 1 As iDebet , sum(tPanjarPotongan.cJumlah) as cJumlah ,'Jurnal pungut pajak no  ' + tPanjar.sNoBukti , 2 as iKelompok FROM tPanjar INNER JOIN mkasbendahara " +
               //     " ON tPanjar.IDDInas = mkasbendahara.IDDInas AND tPanjar.bPPKD= mkasbendahara.bPPKD inner join tPanjarPotongan ON " +
               //     "tPanjar.inourut= tPanjarPotongan.inourut WHERE tPanjar.inourut = " + sNourut + " AND mkasbendahara.btJenis=2 " +
               //     " GROUP BY tPanjar.IDDInas ,tPanjar.IDUrusan , tPanjar.IDProgram , tPanjar.IDkegiatan , " +
               //     " mKasBendahara.IIDrekening , tPanjar.sNoBukti";
               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                                    "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tPanjar.IDDInas ," +
                                    "tPanjar.btKodeUK ," +
                                    "tPanjar.IDUrusan ,tPanjar.IDProgram , tPanjar.IDkegiatan ,tPanjar.IDSubkegiatan,  " +
                                   m_KasBendaharaPengeluaran.ToString () +"  AS iiDRekening , 1 as iNoUrut , 1 As iDebet ,"+
                                    " (select  sum(tPanjarPotongan.cJumlah) from tPanjarPotongan where inourut =tPanjar.inourut ) as cJumlah ,'Jurnal pungut pajak no  ' + tPanjar.sNoBukti ," +
                                    "2 as iKelompok, tPanjar.UnitAnggaran  FROM tPanjar  WHERE tPanjar.inourut = " + sNourut   ;



               _dbHelper.ExecuteNonQuery(SSQL);

               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan, IDSubkegiatan," +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok,UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tPanjar.IDDInas," +
                     "tPanjar.btKodeUK ," +
                     "tPanjar.IDurusan,  tPanjar.IDProgram , tPanjar.IDkegiatan , tPanjar.IDSubkegiatan, " +
                     " MapPotongan.IDNeraca AS iiDRekening  , 2 as iNoUrut , -1 As iDebet , tPanjarPotongan.cJumlah,"+
                     "'Jurnal Pungut Pajak no  ' + tPanjar.sNoBukti ,1 as iKelompok,tPanjar.UnitAnggaran FROM tPanjar INNER JOIN tPanjarPotongan " +
                     " ON tPanjar.inourut = tPanjarPotongan.inourut  " +
                     " INNER JOIN MapPotongan ON MapPotongan.IdPotongan = tPanjarPotongan.IIDRekening" +
                      " WHERE tPanjar.inourut = " + sNourut ;

               _dbHelper.ExecuteNonQuery(SSQL);
               return true;
           }
           catch (Exception ex)
           {
               _lastError = ex.Message + "\n  " + SSQL;
               return false;
           }

       }
       public bool ProcessJurnalPANJAR13(string sNourut, int bFromSKPD)
       {

           try
           {

               string sNoJurnal;

               sNoJurnal = GetNextNo();

               //SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
               //      "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan ,bFromSKPD) SELECT " +
               //      sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
               //      ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tPanjar.dtBukukas as dtJurnal, 0 as iStatus, sNobukti as sNobukti, " +
               //      ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + "  as btJenisJurnal," +
               //      "dtBukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_PANJAR).ToString() +
               //      " as iJenisSumber,0 as btPeruntukan, bPPKD, 1 as iKelompok , 0 as bPotongan, " + bFromSKPD.ToString() + "  FROM tPanjar WHERE inourut =" + sNourut;

               SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                    "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                    sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                    ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tPanjar.dtBukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                    "dtBukukas as  dtTanggalBukti,  sNoBukti  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_PANJAR).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 1 as iKelompok , " +
                    "0 as bPotongan," + bFromSKPD.ToString() + " , 1 as btUrut  FROM tPanjar  WHERE inourut =" + sNourut;




               _dbHelper.ExecuteNonQuery(SSQL);





               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tPanjar.IDDInas ," +
                     "tPanjar.btKodeUK ," +
                     "tPanjar.IDUrusan ,tPanjar.IDProgram , tPanjar.IDkegiatan ,tPanjar.IDSubkegiatan,  " +
                     " tPanjarRekening.IIDRekening AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tPanjarRekening.cJumlah,'Jurnal SPJ No.  ' + tPanjar.sNoBukti, 1 as iKelompok, tPanjar.UnitAnggaran  FROM tPanjar INNER JOIN tPanjarRekening " +
                     " ON tPanjar.inourut = tPanjarRekening.inourut  WHERE tPanjar.inourut = " + sNourut;



               _dbHelper.ExecuteNonQuery(SSQL);



               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tPanjar.IDDInas ," +
                     "tPanjar.btKodeUK ," +
                     "tPanjar.IDUrusan ,tPanjar.IDProgram , tPanjar.IDkegiatan ,tPanjar.IDSubkegiatan,  " +
                     m_iKodeEstimasiPadaSAL.ToString() + " AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tPanjar.cJumlah, 'Jurnal SPJ No.  ' + tPanjar.sNoBukti, " +
                     " 1 as iKelompok, tPanjar.UnitAnggaran  FROM tPanjar WHERE tPanjar.inourut = " + sNourut;
               _dbHelper.ExecuteNonQuery(SSQL);


               sNoJurnal = GetNextNo();


               SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                    "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                    sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                    ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tPanjar.dtBukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENGELUARAN).ToString() + " as btJenisJurnal," +
                    "dtBukukas as  dtTanggalBukti,  sNoBukti  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_PANJAR).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 2 as iKelompok , " +
                    "0 as bPotongan," + bFromSKPD.ToString() + " , 2 as btUrut  FROM tPanjar  WHERE inourut =" + sNourut;


               _dbHelper.ExecuteNonQuery(SSQL);


               //SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
               //      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tPanjar.IDDInas " +
               //      ", tPanjar.IDurusan, tPanjar.IDProgram , tPanjar.IDkegiatan , " +
               //      " KOR_LRA_LO.IIDRekeningLO AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tPanjarRekening.cJumlah,'Jurnal SPJ  no  ' + tPanjar.sNoBukti , " +
               //      " 2 as iKelompok FROM tPanjar INNER JOIN tPanjarRekening " +
               //      " ON tPanjar.inourut = tPanjarRekening.inourut  INNER JOIN KOR_LRA_LO ON KOR_LRA_LO.IIDRekening = tPanjarRekening.IIDRekening WHERE tPanjar.inourut = " + sNourut;
               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tPanjar.IDDInas ," +
                     "tPanjar.btKodeUK ," +
                     "tPanjar.IDUrusan ,tPanjar.IDProgram , tPanjar.IDkegiatan ,tPanjar.IDSubkegiatan,  " +
                     "  KOR_LRA_LO.IIDRekeningLO  AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tPanjarRekening.cJumlah,'Jurnal SPJ No.  ' + tPanjar.sNoBukti, 2 as iKelompok, tPanjar.UnitAnggaran  FROM tPanjar INNER JOIN tPanjarRekening " +
                     " ON tPanjar.inourut = tPanjarRekening.inourut   INNER JOIN KOR_LRA_LO ON KOR_LRA_LO.IIDRekening = tPanjarRekening.IIDRekening  WHERE tPanjar.inourut = " + sNourut;



               _dbHelper.ExecuteNonQuery(SSQL);



               //SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
               //     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tPanjar.IDDInas" +
               //     ", tPanjar.IDurusan,tPanjar.IDProgram , tPanjar.IDkegiatan , " +
               //      "mKasBendahara.IIDrekening AS iiDRekening , 2 as iNoUrut , -1 As iDebet , cJumlah,'Jurnal SPJ no  ' + tPanjar.sNoBukti , 2 as iKelompok FROM tPanjar INNER JOIN mkasbendahara " +
               //     " ON tPanjar.IDDInas= mkasbendahara.IDDInas AND mkasbendahara.bPPKD = tPanjar.bPPKD WHERE tPanjar.inourut = " + sNourut +
               //     " AND mkasbendahara.btJenis=2 and mKasBendahara.bPPKD = tPanjar.bPPKD";

               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tPanjar.IDDInas ," +
                     "tPanjar.btKodeUK ," +
                     "tPanjar.IDUrusan ,tPanjar.IDProgram , tPanjar.IDkegiatan ,tPanjar.IDSubkegiatan,  " +
                     "(Select mKasBendahara.IIDrekening from mKasBendahara where btJenis= 2 )  AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tPanjar.cJumlah, 'Jurnal SPJ No.  ' + tPanjar.sNoBukti, " +
                     " 2 as iKelompok, tPanjar.UnitAnggaran  FROM tPanjar WHERE tPanjar.inourut = " + sNourut;





               _dbHelper.ExecuteNonQuery(SSQL);


               return true;

           }
           catch (Exception ex)
           {
               _lastError = ex.Message + "\n " + SSQL;
               return false;
           }



       }
       public bool ProcessJurnalSTS(string sNourut)
       {

           try
           {

               string sNoJurnal;

               sNoJurnal = GetNextNo();

       

               SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                    "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                    sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                    ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSTS.dtSTS as dtJurnal, 0 as iStatus, sNoSTS as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENERIMAAN).ToString() + " as btJenisJurnal," +
                    "dtSTS as  dtTanggalBukti,  sNoSTS  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_STS).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 1 as iKelompok , " +
                    "0 as bPotongan,0 , 1 as btUrut  FROM tSTS  WHERE inourut =" + sNourut;




               _dbHelper.ExecuteNonQuery(SSQL);


               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     "btKodekategoripelaksana, btKodeUrusanPelaksana, btKodekategori, btKodeUrusan, btKodeSKPD, iidrekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSTS.IDDInas ," +
                     "tSTS.btKodeUK ," +
                     "tSTS.IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan," +
                     " dbo.ToKodekategoriPelaksana(tsts.IDDInas) as btKodekategoripelaksana," +
                     "dbo.ToKodeUrusanPelaksana (tsts.IDDInas) as btKodeUrusanPelaksana," +
                     "dbo.ToKodekaTegori(tsts.IDDInas) as btKodekategori," +
                     "dbo.ToKodeUrusan (tsts.IDDInas) as btKodeUrusan," +
                   "dbo.ToKodeSKPD (tsts.IDDInas) as btKodeSKPD," +
                     m_KasBendaharaPenerimaan.ToString() +" AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSTS.cJumlah,'Jurnal STS No.  ' + tSTS.sNoSTS, 1 as iKelompok, 0 as  UnitAnggaran  FROM tSTS " +
                     " WHERE tSTS.inourut = " + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);

                //SSQL = "INSERT INTO " & m_sNamatabelJurnalRekening & " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " & _
                //                "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " & _
                //                "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " & sNoJurnal & " as inojurnal, tSTS.btKodekategori" & _
                //                ", tSTS.btKodeUrusan, tSTS.btKodeSKPD, tSTS.btKodeUK ,tSTS.btKodekategori as btKodeKategoriPelaksana " & _
                //                ",tSTS.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " & _
                //                " KOR_LRA_LO.IIDRekeningLO AS iiDRekening ,  " & iUrut & " as iNoUrut , " & iDebet & "  As iDebet , tSTSRekening.cJumlah,'Jurnal STS No ' + tSTS.sNoSTS," & _
                //                iKelompok & " as iKelompok FROM tSTS INNER JOIN tSTSRekening  ON tSTS.inourut = tSTSRekening.inourut " & _
                //                " INNER JOIN KOR_LRA_LO ON KOR_LRA_LO.IIDRekening = tSTSRekening.IIDRekening  " & _
                //                " WHERE tSTS.inourut = " & pNoUrutSumber
                                 


               
               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                     " btKodekategoripelaksana, btKodeUrusanPelaksana, btKodekategori, btKodeUrusan, btKodeSKPD, IIDRekening,iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSTS.IDDInas ," +
                     "tSTS.btKodeUK ,tSTS.IDDInas/10000 ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                     " dbo.ToKodekategoriPelaksana(tsts.IDDInas) as btKodekategoripelaksana," +
                     "dbo.ToKodeUrusanPelaksana (tsts.IDDInas) as btKodeUrusanPelaksana," +
                     "dbo.ToKodekaTegori(tsts.IDDInas) as btKodekategori," +
                     "dbo.ToKodeUrusan (tsts.IDDInas) as btKodeUrusan," +
               "dbo.ToKodeSKPD (tsts.IDDInas) as btKodeSKPD," +

                     " KOR_LRA_LO.IIDRekeningLO  AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSTSRekening.cJumlah, 'Jurnal SPJ No.  ' + tSTS.sNoSTS, " +
                     " 1 as iKelompok, 0 as UnitAnggaran  FROM tSTS INNER JOIN tSTSRekening  ON tSTS.inourut = tSTSRekening.inourut  " +
                     " INNER JOIN KOR_LRA_LO ON KOR_LRA_LO.IIDRekening = tSTSRekening.IIDRekening   WHERE tSTS.inourut = " + sNourut;
               _dbHelper.ExecuteNonQuery(SSQL);


               sNoJurnal = GetNextNo();


               
               SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                    "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                    sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                    ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSTS.dtSTS as dtJurnal, 0 as iStatus, sNoSTS as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENERIMAAN).ToString() + " as btJenisJurnal," +
                    "dtSTS as  dtTanggalBukti,  sNoSTS  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_STS).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 1 as iKelompok , " +
                    "0 as bPotongan,0 , 2 as btUrut  FROM tSTS  WHERE inourut =" + sNourut;
               
               _dbHelper.ExecuteNonQuery(SSQL);


 

               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                   "btKodekategoripelaksana, btKodeUrusanPelaksana, btKodekategori, btKodeUrusan, btKodeSKPD,"+
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSTS.IDDInas ," +
                     "tSTS.btKodeUK ," +
                     "tSTS.IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                     " dbo.ToKodekategoriPelaksana(tsts.IDDInas) as btKodekategoripelaksana," +
                     "dbo.ToKodeUrusanPelaksana (tsts.IDDInas) as btKodeUrusanPelaksana," +
                     "dbo.ToKodekaTegori(tsts.IDDInas) as btKodekategori," +
                     "dbo.ToKodeUrusan (tsts.IDDInas) as btKodeUrusan," +
               "dbo.ToKodeSKPD (tsts.IDDInas) as btKodeSKPD," +
                     m_iKodeEstimasiPadaSAL.ToString() + "  AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSTS.cJumlah,'Jurnal STS No.  ' + tSTS.sNoSTS, 1 as iKelompok, 0 as UnitAnggaran  FROM tSTS " +
                     " WHERE tSTS.inourut = " + sNourut;
                             
               _dbHelper.ExecuteNonQuery(SSQL);

    
               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                   "btKodekategoripelaksana, btKodeUrusanPelaksana, btKodekategori, btKodeUrusan, btKodeSKPD," +  
                   "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSTS.IDDInas ," +
                     "tSTS.btKodeUK ," +
                     "tSTS.IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                     " dbo.ToKodekategoriPelaksana(tsts.IDDInas) as btKodekategoripelaksana," +
                     "dbo.ToKodeUrusanPelaksana (tsts.IDDInas) as btKodeUrusanPelaksana," +
                     "dbo.ToKodekaTegori(tsts.IDDInas) as btKodekategori," +
                     "dbo.ToKodeUrusan (tsts.IDDInas) as btKodeUrusan," +
               "dbo.ToKodeSKPD (tsts.IDDInas) as btKodeSKPD," +
                     "tSTSRekening.IIDRekening  AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSTSRekening.cJumlah, 'Jurnal SPJ No.  ' + tSTS.sNoSTS, " +
                     " 2 as iKelompok, 0 as UnitAnggaran  FROM tSTS   INNER JOIN tSTSRekening ON tSTS.inourut= tSTSRekening.inourut WHERE tSTS.inourut = " + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);


               sNoJurnal = GetNextNo();

               SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                    "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                    sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                    ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSTS.dtSTS as dtJurnal, 0 as iStatus, sNoSTS as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENERIMAAN).ToString() + " as btJenisJurnal," +
                    "dtSTS as  dtTanggalBukti,  sNoSTS  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_STS).ToString() + " as iJenisSumber,0 as btPeruntukan, 1 as bPPKD, 1 as iKelompok , " +
                    "0 as bPotongan,0 , 3 as btUrut  FROM tSTS  WHERE inourut =" + sNourut + "  AND tSTS.bPPKD=0 "; // bukan ppkd 


               _dbHelper.ExecuteNonQuery(SSQL);




    

               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                   "btKodekategoripelaksana, btKodeUrusanPelaksana, btKodekategori, btKodeUrusan, btKodeSKPD, " +
                           "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSTS.IDDInas ," +
                           "tSTS.btKodeUK ," +
                           "tSTS.IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan, " +
                             " dbo.ToKodekategoriPelaksana(tsts.IDDInas) as btKodekategoripelaksana," +
                     "dbo.ToKodeUrusanPelaksana (tsts.IDDInas) as btKodeUrusanPelaksana," +
                     "dbo.ToKodekaTegori(tsts.IDDInas) as btKodekategori," +
                     "dbo.ToKodeUrusan (tsts.IDDInas) as btKodeUrusan," +
                   "dbo.ToKodeSKPD (tsts.IDDInas) as btKodeSKPD," +
                           m_iRekKasda.ToString() + " AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSTS.cJumlah,'Jurnal STS No.  ' + tSTS.sNoSTS, 1 as iKelompok, 0 as UnitAnggaran  FROM tSTS " +
                           " WHERE tSTS.inourut = " + sNourut + "  AND tSTS.bPPKD=0 ";

               _dbHelper.ExecuteNonQuery(SSQL);


               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                   "btKodekategoripelaksana, btKodeUrusanPelaksana, btKodekategori, btKodeUrusan, btKodeSKPD, " +
                           "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSTS.IDDInas ," +
                           "tSTS.btKodeUK ," +
                           "tSTS.IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan, " +
                             " dbo.ToKodekategoriPelaksana(tsts.IDDInas) as btKodekategoripelaksana," +
                     "dbo.ToKodeUrusanPelaksana (tsts.IDDInas) as btKodeUrusanPelaksana," +
                     "dbo.ToKodekaTegori(tsts.IDDInas) as btKodekategori," +
                     "dbo.ToKodeUrusan (tsts.IDDInas) as btKodeUrusan," +
                   "dbo.ToKodeSKPD (tsts.IDDInas) as btKodeSKPD," +
                   //    vv
                         m_sRekeningRKSKPD.ToString() + "  AS iiDRekening , 1 as iNoUrut , -1 As iDebet , tSTS.cJumlah,'Jurnal STS No.  ' + tSTS.sNoSTS, 1 as iKelompok, 0 as UnitAnggaran  FROM tSTS " +
                           " WHERE tSTS.inourut = " + sNourut + "  AND tSTS.bPPKD=0 ";
               _dbHelper.ExecuteNonQuery(SSQL);

               return true;

           }
           catch (Exception ex)
           {
               _lastError = ex.Message + "\n " + SSQL;
               return false;
           }
         
        

       }
       public bool ProcessJurnalSTSLangsung(string sNourut)
       {

           try
           {

               string sNoJurnal;
               sNoJurnal = GetNextNo();
               SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                    "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                    sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                    ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSTS.dtSTS as dtJurnal, 0 as iStatus, sNoSTS as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENERIMAAN).ToString() + " as btJenisJurnal," +
                    "dtSTS as  dtTanggalBukti,  sNoSTS  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_STS).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 1 as iKelompok , " +
                    "0 as bPotongan,0 , 1 as btUrut  FROM tSTS  WHERE inourut =" + sNourut;
               _dbHelper.ExecuteNonQuery(SSQL);


               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                      "btKodekategoripelaksana, btKodeUrusanPelaksana, btKodekategori, btKodeUrusan, btKodeSKPD, " +
                      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSTS.IDDInas ," +
                     "tSTS.btKodeUK ," +
                     "tSTS.IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                       " dbo.ToKodekategoriPelaksana(tsts.IDDInas) as btKodekategoripelaksana," +
                     "dbo.ToKodeUrusanPelaksana (tsts.IDDInas) as btKodeUrusanPelaksana," +
                     "dbo.ToKodekaTegori(tsts.IDDInas) as btKodekategori," +
                     "dbo.ToKodeUrusan (tsts.IDDInas) as btKodeUrusan," +
                   "dbo.ToKodeSKPD (tsts.IDDInas) as btKodeSKPD," +
                     m_iKodeEstimasiPadaSAL.ToString() + "  AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSTS.cJumlah,'Jurnal STS No.  ' + tSTS.sNoSTS, 1 as iKelompok, 0 as UnitAnggaran  FROM tSTS " +
                     " WHERE tSTS.inourut = " + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);

               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                   "btKodekategoripelaksana, btKodeUrusanPelaksana, btKodekategori, btKodeUrusan, btKodeSKPD, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSTS.IDDInas ," +
                     "tSTS.btKodeUK ," +
                     "tSTS.IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                       " dbo.ToKodekategoriPelaksana(tsts.IDDInas) as btKodekategoripelaksana," +
                     "dbo.ToKodeUrusanPelaksana (tsts.IDDInas) as btKodeUrusanPelaksana," +
                     "dbo.ToKodekaTegori(tsts.IDDInas) as btKodekategori," +
                     "dbo.ToKodeUrusan (tsts.IDDInas) as btKodeUrusan," +
                   "dbo.ToKodeSKPD (tsts.IDDInas) as btKodeSKPD," +
                     "tSTSRekening.IIDRekening  AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSTSRekening.cJumlah, 'Jurnal SPJ No.  ' + tSTS.sNoSTS, " +
                     " 2 as iKelompok, 0 as UnitAnggaran  FROM tSTS   INNER JOIN tSTSRekening ON tSTS.inourut= tSTSRekening.inourut WHERE tSTS.inourut = " + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);



               

               

               sNoJurnal = GetNextNo();

               SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                    "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                    sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                    ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSTS.dtSTS as dtJurnal, 0 as iStatus, sNoSTS as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENERIMAAN).ToString() + " as btJenisJurnal," +
                    "dtSTS as  dtTanggalBukti,  sNoSTS  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_STS).ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 1 as iKelompok , " +
                    "0 as bPotongan,0 , 2 as btUrut  FROM tSTS  WHERE inourut =" + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);

               
               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                   "btKodekategoripelaksana, btKodeUrusanPelaksana, btKodekategori, btKodeUrusan, btKodeSKPD, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSTS.IDDInas ," +
                     "tSTS.btKodeUK ," +
                     "tSTS.IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                       " dbo.ToKodekategoriPelaksana(tsts.IDDInas) as btKodekategoripelaksana," +
                     "dbo.ToKodeUrusanPelaksana (tsts.IDDInas) as btKodeUrusanPelaksana," +
                     "dbo.ToKodekaTegori(tsts.IDDInas) as btKodekategori," +
                     "dbo.ToKodeUrusan (tsts.IDDInas) as btKodeUrusan," +
                   "dbo.ToKodeSKPD (tsts.IDDInas) as btKodeSKPD," +
                     m_iRekRKPPKD.ToString() + " AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSTS.cJumlah,'Jurnal STS No.  ' + tSTS.sNoSTS, 1 as iKelompok, 0 as UnitAnggaran  FROM tSTS " +
                     " WHERE tSTS.inourut = " + sNourut;
               _dbHelper.ExecuteNonQuery(SSQL);

               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                   "btKodekategoripelaksana, btKodeUrusanPelaksana, btKodekategori, btKodeUrusan, btKodeSKPD, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSTS.IDDInas ," +
                     "tSTS.btKodeUK ,tSTS.IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan,  " +
                       " dbo.ToKodekategoriPelaksana(tsts.IDDInas) as btKodekategoripelaksana," +
                     "dbo.ToKodeUrusanPelaksana (tsts.IDDInas) as btKodeUrusanPelaksana," +
                     "dbo.ToKodekaTegori(tsts.IDDInas) as btKodekategori," +
                     "dbo.ToKodeUrusan (tsts.IDDInas) as btKodeUrusan," +
                   "dbo.ToKodeSKPD (tsts.IDDInas) as btKodeSKPD," +
                     " KOR_LRA_LO.IIDRekeningLO  AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSTSRekening.cJumlah, 'Jurnal SPJ No.  ' + tSTS.sNoSTS, " +
                     " 1 as iKelompok, 0 as UnitAnggaran  FROM tSTS INNER JOIN tSTSRekening  ON tSTS.inourut = tSTSRekening.inourut  " +
                     " INNER JOIN KOR_LRA_LO ON KOR_LRA_LO.IIDRekening = tSTSRekening.IIDRekening   WHERE tSTS.inourut = " + sNourut;
               _dbHelper.ExecuteNonQuery(SSQL);
               
               
               // Jika bukan PPKD'

                   //sNoJurnal = ReadNo(CON_URUT_JURNAL)
    
    //SSQL = "INSERT INTO " & m_sNamatabelJurnal & " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " & _
    //        "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD, iKelompok, bPotongan ) SELECT " & _
    //        sNoJurnal & " as inojurnal, " & g_nTahun & " AS iTahun " & _
    //        ", dtsts AS  dtInput, dtSTS as dtJurnal, 0 as iStatus, snoSTS as sNobukti, " & JENIS_JURNAL.JENIS_JURNALPENERIMAAN & " as btJenisJurnal," & _
    //        " dtsts as  dtTanggalBukti,  sNoSTS as sNoBukukas, inourut  as iSumber , " & JENIS_SUMBERJURNAL.E_SUMBER_STS & " as iJenisSumber,0 as btPeruntukan, 1 as  bPPKD, 1 as iKelompok , 0 as bPotongan FROM tSTS WHERE inourut =" & sNourut & " AND tSTS.bPPKD=0 "
    
    //ExecuteEx SSQL
    sNoJurnal = GetNextNo();

               SSQL = "INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                    "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD, btUrut ) SELECT " +
                    sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun ,IDDInas, btKodeUK" +
                    ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSTS.dtSTS as dtJurnal, 0 as iStatus, sNoSTS as sNobukti, " + ((int)JENIS_JURNAL.JENIS_JURNALPENERIMAAN).ToString() + " as btJenisJurnal," +
                    "dtSTS as  dtTanggalBukti,  sNoSTS  as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_STS).ToString() + " as iJenisSumber,0 as btPeruntukan, 1 as bPPKD, 1 as iKelompok , " +
                    "0 as bPotongan,0 , 3 as btUrut  FROM tSTS  WHERE inourut =" + sNourut +"  AND tSTS.bPPKD=0 "; // bukan ppkd 


               _dbHelper.ExecuteNonQuery(SSQL);

    
            
            
    //    SSQL = "INSERT INTO " & m_sNamatabelJurnalRekening & " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " & _
    //              "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " & _
    //              "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan,iKelompok) SELECT " & sNoJurnal & " as inojurnal,btKodekategori" & _
    //                ", btKodeUrusan, btKodeSKPD, btKodeUK ,btKodekategori as btKodeKategoriPelaksana " & _
    //                ",btKodeurusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan, " & _
    //                m_iRekKasda & " AS iiDRekening , 1 as iNoUrut , 1 As iDebet , sum(tSTSRekening.cJumlah) as cJumlah ,'Jurnal Pembayaran  Setor Pendapatan no  ' + tSTS.sNoBukti ,1 as iKelompok FROM tSTS inner join tSTSRekening ON tSTS.inourut= tSTSRekening.inourut WHERE tSTS.inourut = " & sNourut & " AND tSTS.bPPKD=0 " & _
    //                " GROUP BY btKodekategori, btKodeUrusan, btKodeSKPD, btKodeUK ,tSTS.sNoBukti"
       //        ExecuteEx SSQL

               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                   "btKodekategoripelaksana, btKodeUrusanPelaksana, btKodekategori, btKodeUrusan, btKodeSKPD, " +
                           "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSTS.IDDInas ," +
                           "tSTS.btKodeUK ," +
                           "tSTS.IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan, " +
                             " dbo.ToKodekategoriPelaksana(tsts.IDDInas) as btKodekategoripelaksana," +
                     "dbo.ToKodeUrusanPelaksana (tsts.IDDInas) as btKodeUrusanPelaksana," +
                     "dbo.ToKodekaTegori(tsts.IDDInas) as btKodekategori," +
                     "dbo.ToKodeUrusan (tsts.IDDInas) as btKodeUrusan," +
                   "dbo.ToKodeSKPD (tsts.IDDInas) as btKodeSKPD," +
                           m_iRekKasda.ToString() + " AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSTS.cJumlah,'Jurnal STS No.  ' + tSTS.sNoSTS, 1 as iKelompok, 0 as UnitAnggaran  FROM tSTS " +
                           " WHERE tSTS.inourut = " + sNourut + "  AND tSTS.bPPKD=0 ";

               _dbHelper.ExecuteNonQuery(SSQL);




    //        SSQL = "INSERT INTO " & m_sNamatabelJurnalRekening & " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " & _
    //              "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " & _
    //              "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " & sNoJurnal & " as inojurnal,tSTS.btKodekategori" & _
    //              ", tSTS.btKodeUrusan, tSTS.btKodeSKPD, tSTS.btKodeUK ,tSTS.btKodekategori as btKodeKategoriPelaksana " & _
    //              ",tSTS.btKodeurusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan, " & _
    //              " mkasbendahara.iidRekening AS iiDRekening  , 1 as iNoUrut , -1 As iDebet , sum(tSTSRekening.cJumlah) as cJumlah ,'Jurnal Pembayaran  Setor Pendapatan no  ' + tSTS.sNoBukti ,1 as iKelompok FROM tSTS inner join tSTSRekening ON tSTS.inourut= tSTSRekening.inourut " & " AND tSTS.bPPKD=0 " & _
    //              " INNER JOIN mKasBEndahara ON  tSTS.btKodekategori = mkasbendahara.btKodekategori and tSTS.btKodeUrusan = mkasbendahara.btKodeUrusan " & _
    //              " and tSTS.btKodeSKPD = mkasbendahara.btKodeSKPD  WHERE tSTS.inourut = " & sNourut & " AND mkasbendahara.btJenis=0 " & _
    //                " GROUP BY tSTS.btKodekategori, tSTS.btKodeUrusan, tSTS.btKodeSKPD, tSTS.btKodeUK ,tSTS.sNoBukti,mkasbendahara.iidRekening"
    //        ExecuteEx SSQL


               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS,btKodeUK, IDUrusan, IDProgram, IDKegiatan,IDSubKEgiatan, " +
                   "btKodekategoripelaksana, btKodeUrusanPelaksana, btKodekategori, btKodeUrusan, btKodeSKPD, " +
                           "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok, UnitAnggaran) SELECT " + sNoJurnal + " as inojurnal, tSTS.IDDInas ," +
                           "tSTS.btKodeUK ," +
                           "tSTS.IDUrusan ,0 as IDProgram , 0 as IDkegiatan ,0 as IDSubkegiatan, " +
                             " dbo.ToKodekategoriPelaksana(tsts.IDDInas) as btKodekategoripelaksana," +
                     "dbo.ToKodeUrusanPelaksana (tsts.IDDInas) as btKodeUrusanPelaksana," +
                     "dbo.ToKodekaTegori(tsts.IDDInas) as btKodekategori," +
                     "dbo.ToKodeUrusan (tsts.IDDInas) as btKodeUrusan," +
                   "dbo.ToKodeSKPD (tsts.IDDInas) as btKodeSKPD," +
               //    vv
                         m_sRekeningRKSKPD.ToString() + "  AS iiDRekening , 1 as iNoUrut , -1 As iDebet , tSTS.cJumlah,'Jurnal STS No.  ' + tSTS.sNoSTS, 1 as iKelompok, 0 as UnitAnggaran  FROM tSTS " +
                           " WHERE tSTS.inourut = " + sNourut + "  AND tSTS.bPPKD=0 ";
               _dbHelper.ExecuteNonQuery(SSQL);

               
               
               
               
               
               
               return true;




           }
           catch (Exception ex)
           {
               _lastError = ex.Message + "\n " + SSQL;
               return false;
           }



       }

       private bool ProcessJurnalSKR13(string sNourut){

           try
           {
               string sNoJurnal;
               sNoJurnal = GetNextNo();



               SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                      "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok) SELECT " +
                       sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                       ", tSKRSKPD.dtskrskpd AS  dtInput, tSKRSKPD.dtSKRSKPD as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, 1 as btJenisJurnal," +
                       "dtSKRSKPD as  dtTanggalBukti,  tSKRSKPD.sNoBukti as sNoBukukas, inourut  as iSumber , " + ((int)JENIS_SUMBERJURNAL.E_SUMBER_SKR).ToString() +
                       " as iJenisSumber,0 as btPeruntukan,  bPPKD, 1 as iKelompok FROM tSKRSKPD WHERE inourut =" + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);



               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSKRSKPD.IDDInas" +
                      ", tSKRSKPD.IDurusan, 0  as IDProgram , 0 AS IDKegiatan , " +
                      "korpermenpiutang.iidpiutang AS iiDRekening , 1 as iNoUrut , 1 As iDebet , SUM(tSKRSKPDRekening.cJumlah) as cJumlah, 'Jurnal Penerimaan SKR ' + tSKRSKPD.sNoBukti, 1 as iKelompok FROM tSKRSKPD INNER JOIN tSKRSKPDRekening" +
                      " ON tSKRSKPD.inourut= tSKRSKPDRekening.inourut Inner JOIN korpermenpiutang ON korpermenpiutang.IIDRekening= tSKRSKPDRekening.IIDRekening WHERE tSKRSKPD.inourut = " + sNourut +
                      " GROUP BY tSKRSKPD.IDDInas,korpermenpiutang.iidpiutang , tSKRSKPD.sNoBukti ";

               _dbHelper.ExecuteNonQuery(SSQL);
               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSKRSKPD.IDDInas" +
                      ", tSKRSKPD.IDUrusan, 0 as IDProgram , 0 as IDKegiatan , " +
                      " KOR_LRA_LO.IIDRekeningLO AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSKRSKPDRekening.cJumlah,'Jurnal Penerimaan SKR ' + tSKRSKPD.sNoBukti, " +
                      " 1 as iKelompok FROM tSKRSKPD INNER JOIN tSKRSKPDRekening " +
                      " ON tSKRSKPD.inourut = tSKRSKPDRekening.inourut  INNER JOIN KOR_LRA_LO ON KOR_LRA_LO.IIDRekening = tSKRSKPDRekening.IIDRekening " +
                     " WHERE tSKRSKPD.inourut = " + sNourut;
               _dbHelper.ExecuteNonQuery(SSQL);
               return true;

           }
           catch (Exception ex)
           {
               _lastError = ex.Message + "\n" + SSQL;
                
               return false;
           }
       
       }
    }
}
