using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DTO.Bendahara;
using Formatting;
using BP;


namespace BP.Akuntansi
{
    public class ProsesJurnal2Logic: BP
    {
       
        private const string m_sNamatabelJurnalRekening=  "TJURNAL";
        private const string m_sNamatabelJurnal= "TJURNALREKENING";

        private const int SETOR_STS = 1;
        private const int SETOR_UYHD = 2;
        private const int SETOR_CP = 3;
        private const int SETOR_PAJAK = 4;
        private const int TERIMA_SETOR_PAJAK = 5;

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
       Kasda m_oKasda;

//Private m_arNamaTable(3) As String


       public ProsesJurnal2Logic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;


        }

       public ProsesJurnal2Logic(int _pTahun, Kasda oKasda)
            : base(_pTahun)
        {

                 
                Tahun = _pTahun;
                m_iRekKasda = oKasda.RekKasda; //m_oKasda.Re  IIf(IsNull(rsKasda!iIDRekeningBUD), 0, rsKasda!iIDRekeningBUD)
                m_iRekSILPATB =oKasda.RekSILPATB; //IIf(IsNull(rsKasda!iIDRekeningSilpaTB), 0, rsKasda!iIDRekeningSilpaTB)
                m_iRekRKPPKD =oKasda.RekRKPPKD;// IIf(IsNull(rsKasda!iiDRekeningRKPPKD), 0, rsKasda!iiDRekeningRKPPKD)
                m_iRekSILPA =oKasda.RekSILPA; //IIf(IsNull(rsKasda!iIDRekeningSilpa), 0, rsKasda!iIDRekeningSilpa)
          
                m_iKodeEstimasiPadaSAL =oKasda.KodeEstimasiPadaSAL;// IIf(IsNull(rsKasda!IIDRekEstimasiSAL), 0, rsKasda!IIDRekEstimasiSAL)
                m_iKodeEkuitas = 31101001;


        }
      
       public bool JurnalSKRSKPD(SKRSKPD oSKRSKPD)
       {

           return true;
       }
       public bool JurnalSTS(STS oSTS)
       {

           return true;
       }
       public bool JurnalSPP(SPP oSPP)
       {

           
           return true;
       }
       public bool JurnalSetor(Setor oSetor)
       {

           return true;
       }
       //public  bool JurnalBAST(BAST  oBAST)
       //{

       //    return true;
       //}
       public bool JurnalPengeluaran(Pengeluaran oPengeluaran)
       {

           return true;
       }
       public bool Jurnal(BAST oBAST)
       {

           return true;
       }

       public string GetNextNo(){
           return "";
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
                       ", " + GetCurrentDate() + " AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoSP2D as sNobukti, " + JENIS_JURNAL.JENIS_JURNALPENGELUARAN + " as btJenisJurnal," +
                       "dtbukukas as  dtTanggalBukti,  sNoSP2D as sNoBukukas, inourut  as iSumber , " + JENIS_SUMBERJURNAL.E_SUMBER_SP2D + "  as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 1 as ikelompok , 0 as bPotongan, sPeruntukan as sUraian   FROM tSPP  WHERE inourut =" + sNourut;
               _dbHelper.ExecuteNonQuery(SSQL);

               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.IDDInas, " +
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
       //private  bool Jurnal(long NoUrut,,int bppkd)
       public bool Jurnal (string sNourut , JENIS_TABLE iTable, int  iJenisSPP = -1, Single bPotongan = 0, int bppkd = 0, bool bTHL = false, int fromSKPD = 0)
       {

           
         
         
         if (bppkd == 0){ 
            
             SSQL = "DELETE tBukubesar FROM tBukubesar INNER JOIN tJurnal ON tJurnal.inoJurnal= tBukubesar.inojurnal WHERE tJurnal.iSumber= " + sNourut + " AND tBukuBesar.bPPKD=0 and isnull(tBukubesar.bPotongan,0)=" + bPotongan.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);
             SSQL = "DELETE tJurnalRekening FROM tJurnalRekening INNER JOIN tJurnal ON tJurnal.inoJurnal= tJurnalRekening.inojurnal WHERE tJurnal.iSumber= " + sNourut + " AND tJurnal.bPPKD=0 and  isnull(tJurnal.bPotongan,0)=" + bPotongan.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = "DELETE FROM tJurnal WHERE tJurnal.iSumber= " + sNourut + " AND bPPKD=0 and  isnull(tJurnal.bPotongan,0)=" + bPotongan.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = "DELETE tBukubesar FROM tBukubesar INNER JOIN tJurnal ON tJurnal.inoJurnal= tBukubesar.inojurnal WHERE tJurnal.iSumber= " + sNourut + " AND tBukuBesar.bPPKD=1 and isnull(tBukubesar.bPotongan,0)=" + bPotongan.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = "DELETE tJurnalRekening FROM tJurnalRekening INNER JOIN tJurnal ON tJurnal.inoJurnal= tJurnalRekening.inojurnal WHERE tJurnal.iSumber= " + sNourut + " AND tJurnal.bPPKD=1  and  isnull(tJurnal.bPotongan,0)=" + bPotongan.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = "DELETE FROM tJurnal WHERE tJurnal.iSumber= " + sNourut + " AND bPPKD=1  and  isnull(tJurnal.bPotongan,0)=" + bPotongan.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);

         } else {
            
         
            SSQL = "DELETE tBukubesar FROM tBukubesar INNER JOIN tJurnal ON tJurnal.inoJurnal= tBukubesar.inojurnal WHERE tJurnal.iSumber= " + sNourut + " AND tBukuBesar.bPPKD=1 and isnull(tBukubesar.bPotongan,0)=" + bPotongan.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = "DELETE tJurnalRekening FROM tJurnalRekening INNER JOIN tJurnal ON tJurnal.inoJurnal= tJurnalRekening.inojurnal WHERE tJurnal.iSumber= " + sNourut + " AND tJurnal.bPPKD=1  and  isnull(tJurnal.bPotongan,0)=" + bPotongan.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = "DELETE FROM tJurnal WHERE tJurnal.iSumber= " + sNourut + " AND bPPKD=1  and  isnull(tJurnal.bPotongan,0)=" + bPotongan.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);
         }

         switch (iTable){
             case  JENIS_TABLE.TABLE_STS:
                    break;
                        
             case JENIS_TABLE.TABLE_SPP :
                 //
                      ProcessJurnalSPP (sNourut, iJenisSPP, bTHL, fromSKPD);
                
                    break;
                        
             case JENIS_TABLE.TABLE_SETOR:

                    if (bPotongan == 1)
                    {
                        ProcessJurnalSetorPajak(sNourut);
                        //'ProcessJurnalSetorPajakBKU sNourut
                    }
                    else
                        ProcessJurnalSETOR(sNourut, iJenisSPP, fromSKPD);
                        //End If
                    break;                      
             case JENIS_TABLE.TABLE_PANJAR:
                
                    /*
                        if (bPotongan == 1) 
                           ///ProcessJurnalPotonganPANJAR13 (sNourut);
                         
                        else
                            ///ProcessJurnalPANJAR13 (sNourut, fromSKPD);
                      */  
                    
                    break;
             case JENIS_TABLE.TABLE_SKR:
                        ///ProcessJurnalSKR13 (sNourut);
                        break;
             case JENIS_TABLE.TABLE_BAST:
                        //ProcessJurnalBAST(sNourut);
                        break;
             case JENIS_TABLE.TABLE_KOREKSI:

                    //ProcessJurnalKoreksi (sNourut);
                 break;   
                   
        }
            return true;


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
       private bool JurnalUPGU13(string sNourut , int bFronSKPD){

          string  sNoJurnal ;
          sNoJurnal =GetNextNo();
          try
          {
              if (bFronSKPD == 0)
              {

                  SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                          "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,  bFromSKPD ) SELECT " +
                          sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                          ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoSP2D as sNobukti, " + JENIS_JURNAL.JENIS_JURNALPENGELUARAN.ToString() + " as btJenisJurnal," +
                          "dtbukukas as  dtTanggalBukti,  sNoSP2D as sNoBukukas, inourut  as iSumber , " + JENIS_SUMBERJURNAL.E_SUMBER_SP2D.ToString() + "  as iJenisSumber,0 as btPeruntukan, bPPKD as bPPKD, 1 as ikelompok , 0 as bPotongan ," + bFronSKPD.ToString() + " FROM tSPP  WHERE inourut =" + sNourut;
                  _dbHelper.ExecuteNonQuery(SSQL);

                  SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                        "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori " +
                        ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,tSPP.btKodekategori as btKodeKategoriPelaksana " +
                        ",tSPP.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                        "mKasBendahara.IIDrekening AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSPP.cJumlah,'Jurnal SP2D no  ' + sNoSP2D, 1 as iKelompok FROM tSPP INNER JOIN mKasBendahara " +
                        " ON tSPP.btKodekategori = mkasbendahara.btKodekategori and tSPP.btKodeUrusan = mkasbendahara.btKodeUrusan " +
                        " and tSPP.btKodeSKPD = mkasbendahara.btKodeSKPD and tSPP.bPPKD = mkasbendahara.bPPKD WHERE tSPP.inourut = " + sNourut +
                        " AND mkasbendahara.btJenis=2 ";

                  _dbHelper.ExecuteNonQuery(SSQL);
                  SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan,iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori" +
                     ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,tSPP.btKodekategori as btKodeKategoriPelaksana " +
                     ",tSPP.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                     m_iRekRKPPKD + " AS iiDRekening , 1 as iNoUrut , -1 As iDebet , cJumlah,'Jurnal SP2D no  ' + sNoSP2D, 1 as Ikelompok FROM tSPP WHERE inourut = " + sNourut;

                  _dbHelper.ExecuteNonQuery(SSQL);
              }
              else
              {

                  SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                          "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, sUraian, bFromSKPD ) SELECT " +
                          sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                          ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoSP2D as sNobukti, " + JENIS_JURNAL.JENIS_JURNALPENGELUARAN.ToString() + " as btJenisJurnal," +
                          "dtbukukas as  dtTanggalBukti,  sNoSP2D as sNoBukukas, inourut  as iSumber , " + JENIS_SUMBERJURNAL.E_SUMBER_SP2D.ToString() + "  as iJenisSumber,0 as btPeruntukan, bPPKD as bPPKD, 1 as ikelompok , 0 as bPotongan , sPeruntukan as sUraian ," + bFronSKPD.ToString() + " FROM tSPP  WHERE inourut =" + sNourut;
                  _dbHelper.ExecuteNonQuery(SSQL);

                  SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                        "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori " +
                        ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,tSPP.btKodekategori as btKodeKategoriPelaksana " +
                        ",tSPP.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                        "mKasBendahara.IIDrekening AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSPP.cJumlah,'Jurnal SP2D no  ' + sNoSP2D, 1 as iKelompok FROM tSPP INNER JOIN mKasBendahara " +
                        " ON tSPP.btKodekategori = mkasbendahara.btKodekategori and tSPP.btKodeUrusan = mkasbendahara.btKodeUrusan " +
                        " and tSPP.btKodeSKPD = mkasbendahara.btKodeSKPD and tSPP.bPPKD = mkasbendahara.bPPKD WHERE tSPP.inourut = " + sNourut +
                        " AND mkasbendahara.btJenis=2 ";

                  _dbHelper.ExecuteNonQuery(SSQL);

                  SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                        "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan,iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori" +
                        ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,tSPP.btKodekategori as btKodeKategoriPelaksana " +
                        ",tSPP.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
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
    
            sNoJurnal = GetNextNo();
            try
            {

                sNoJurnal = GetNextNo();
                SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                    "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, bFromSKPD ) SELECT " +
                    sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                    ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSPP.dtBukukas as dtJurnal, 0 as iStatus, sNoSP2D as sNobukti, " + JENIS_JURNAL.JENIS_JURNALPENGELUARAN.ToString() + " as btJenisJurnal," +
                    " dtBukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + JENIS_SUMBERJURNAL.E_SUMBER_SP2D.ToString() + " as iJenisSumber,1 as btPeruntukan, 1 as bPPKD, 1 as iKelompok , 0 as bPotongan," + bFromSKPD.ToString() + "  FROM tSPP WHERE inourut =" + sNourut + " AND tSPP.bPPKD=0 ";

                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                    "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal,tSPP.btKodekategori" +
                    ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,tSPP.btKodekategori as btKodeKategoriPelaksana " +
                    ",tSPP.btKodeurusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan, " +
                    " mkasbendahara.iidRekening AS iiDRekening  , 1 as iNoUrut , 1 As iDebet , tSPP.cJumlah as cJumlah ,'Jurnal SP2D no  ' + tSPP.sNoSP2d ,1 as iKelompok FROM tSPP " +
                    " INNER JOIN mKasBEndahara ON  tSPP.btKodekategori = mkasbendahara.btKodekategori and tSPP.btKodeUrusan = mkasbendahara.btKodeUrusan " +
                    " and tSPP.btKodeSKPD = mkasbendahara.btKodeSKPD and tSPP.bPPKD = mkasbendahara.bPPKD WHERE tSPP.inourut = " + sNourut + " AND mkasbendahara.btJenis=0  AND tSPP.bPPKD=0 ";

                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan,iKelompok) SELECT " + sNoJurnal + " as inojurnal,btKodekategori" +
                      ",btKodeUrusan, btKodeSKPD, btKodeUK ,btKodekategori as btKodeKategoriPelaksana " +
                      ",btKodeurusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan, " +
                      m_iRekKasda + " AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSPP.cJumlah as cJumlah ,'Jurnal SP2D No.' + tSPP.sNoSP2D ,1 as iKelompok FROM tSPP WHERE tSPP.inourut = " + sNourut + " AND tSPP.bPPKD=0 ";

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


                SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                          "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok,btUrut, bPotongan,bFRomSKPD ) SELECT " +
                          sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                          ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoSP2D as sNobukti, " + JENIS_JURNAL.JENIS_JURNALPENGELUARAN.ToString() + " as btJenisJurnal," +
                          " dtbukukas as  dtTanggalBukti,  sNoSP2D as sNoBukukas, inourut  as iSumber , " + JENIS_SUMBERJURNAL.E_SUMBER_SP2D.ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 2 as ikelompok ,2 as btUrut , 0 as bPotongan," + bFromSKPD.ToString() + "  FROM tSPP  WHERE inourut =" + sNourut;


                _dbHelper.ExecuteNonQuery(SSQL);


                //' Jika menggunakan BAST maka -> Utang terhadap RK PPKD
                //' Lihat WHERE clause....Jika tidak memenuhi maka akan menghasilkan 0 baris dan tidak melakukan penyimpanan apapun


                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, IDDINAS, IDUrusan, IDProgram, IDKegiatan, " +
                      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori " +
                      ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPPRekening.btKodeUK ,tSPPRekening.btKodekategoriPelaksana as btKodeKategoriPelaksana " +
                      ",tSPPRekening.btKodeUrusanPelaksana as btKodeUrusanPelaksana , tSPPRekening.btIDProgram as btIDProgram , tSPPRekening.btIDKegiatan as btIDKegiatan , " +
                      "korpermenpiutang.iiDpiutang AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSPPRekening.cJumlah, 'Jurnal SP2D No  ' + sNoSP2D , 2 as iKelompok  FROM tSPP INNER JOIN tSPPRekening " +
                      " ON tSPP.inourut= tSPPRekening.inourut  inner Join korpermenpiutang on korpermenpiutang.IIDrekening= tSPPRekening.IIDREkening64 WHERE tSPP.inourut = " + sNourut + " AND tSPP.inobast>0";

                _dbHelper.ExecuteNonQuery(SSQL);


                if (bTHL == true)
                {
                    SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                      "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori " +
                      ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPPRekening.btKodeUK ,tSPPRekening.btKodekategoriPelaksana as btKodeKategoriPelaksana " +
                      ",tSPPRekening.btKodeUrusanPelaksana as btKodeUrusanPelaksana , tSPPRekening.btIDProgram as btIDProgram , tSPPRekening.btIDKegiatan as btIDKegiatan , " +
                      "korpermenpiutang.iiDpiutang AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSPPRekening.cJumlah, 'Jurnal SP2D No  ' + sNoSP2D , 2 as iKelompok  FROM tSPP INNER JOIN tSPPRekening " +
                      " ON tSPP.inourut= tSPPRekening.inourut  inner Join korpermenpiutang on korpermenpiutang.IIDrekening= tSPPRekening.IIDREkening64 WHERE tSPP.inourut = " + sNourut;

                    _dbHelper.ExecuteNonQuery(SSQL);


                }
                else
                {




                    SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                          "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                          "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori " +
                          ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPPRekening.btKodeUK ,tSPPRekening.btKodekategoriPelaksana as btKodeKategoriPelaksana " +
                          ",tSPPRekening.btKodeUrusanPelaksana as btKodeUrusanPelaksana , tSPPRekening.btIDProgram as btIDProgram , tSPPRekening.btIDKegiatan as btIDKegiatan , " +
                          "KOR_LRA_LO.IIDRekeningLO AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSPPRekening.cJumlah, 'Jurnal SP2D No ' + sNoSP2D , 2 as iKelompok  FROM tSPP INNER JOIN tSPPRekening " +
                          " ON tSPP.inourut= tSPPRekening.inourut  inner Join KOR_LRA_LO on KOR_LRA_LO.IIDrekening= tSPPRekening.IIDREkening64 WHERE tSPP.inourut = " + sNourut + " AND tSPP.inobast =0";

                    _dbHelper.ExecuteNonQuery(SSQL);

                }


                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                      "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori" +
                      ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,tSPP.btKodekategori as btKodeKategoriPelaksana " +
                      ",tSPP.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                      m_iRekRKPPKD + " AS iiDRekening , 1 as iNoUrut , -1 As iDebet , tSPP.cJumlah,'Jurnal SP2D No ' + sNoSP2D, 2 as iKelompok  FROM tSPP WHERE inourut = " + sNourut;

                _dbHelper.ExecuteNonQuery(SSQL);





                sNoJurnal = GetNextNo();


                SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                          "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok,btUrut, bPotongan , bFromSKPD) SELECT " +
                          sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                          ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoSP2D as sNobukti, " + JENIS_JURNAL.JENIS_JURNALPENGELUARAN.ToString() + " as btJenisJurnal," +
                          " dtbukukas as  dtTanggalBukti,  sNoSP2D as sNoBukukas, inourut  as iSumber , " + JENIS_SUMBERJURNAL.E_SUMBER_SP2D.ToString() + "  as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 2 as ikelompok ,1 as btUrut , 0 as bPotongan ," + bFromSKPD.ToString() + "  FROM tSPP  WHERE inourut =" + sNourut;


                _dbHelper.ExecuteNonQuery(SSQL);




                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                      "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori " +
                      ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPPRekening.btKodeUK ,tSPPRekening.btKodekategoriPelaksana as btKodeKategoriPelaksana " +
                      ",tSPPRekening.btKodeUrusanPelaksana as btKodeUrusanPelaksana , tSPPRekening.btIDProgram as btIDProgram , tSPPRekening.btIDKegiatan as btIDKegiatan , " +
                      "tSPPRekening.IIDrekening64 AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSPPRekening.cJumlah, 'Jurnal SP2D No  ' + sNoSP2D , 1 as iKelompok FROM tSPP INNER JOIN tSPPRekening " +
                      " ON tSPP.inourut= tSPPRekening.inourut  WHERE tSPP.inourut = " + sNourut;

                _dbHelper.ExecuteNonQuery(SSQL);


                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                      "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori" +
                      ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,tSPP.btKodekategori as btKodeKategoriPelaksana " +
                      ",tSPP.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                      m_iKodeEstimasiPadaSAL + " AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSPP.cJumlah,'Jurnal SP2D No  ' + sNoSP2D , 1 as iKelompok FROM tSPP WHERE inourut = " + sNourut;

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

               SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                     "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD ) SELECT " +
                     sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                     ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSPP.dtBukukas as dtJurnal, 0 as iStatus, sNoSP2D as sNobukti, " + JENIS_JURNAL.JENIS_JURNALPENGELUARAN.ToString() + " as btJenisJurnal," +
                     "dtBukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + JENIS_SUMBERJURNAL.E_SUMBER_SP2D.ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 2 as iKelompok , " +
                     "0 as bPotongan," + bFromSKPD.ToString() + "  FROM tSPP WHERE inourut =" + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);

               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                     "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori" +
                     ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,isnull(tSPPRekening.btKodekategoriPelaksana ,0) as btKodeKategoriPelaksana " +
                     ",isnull(tSPPRekening.btKodeUrusanPelaksana,0) as btKodeUrusanPelaksana,isnull( tSPPRekening.btIDProgram,0) as btIDProgram , isnull(tSPPRekening.btIDkegiatan,0) as btIDKegiatan , " +
                     " KOR_LRA_LO.IIDRekeningLO AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSPPRekening.cJumlah,'Jurnal SP2D No.  ' + tSPP.sNoSP2D , 2 as iKelompok  FROM tSPP INNER JOIN tSPPRekening " +
                     " ON tSPP.inourut = tSPPRekening.inourut  INNER JOIN KOR_LRA_LO ON KOR_LRA_LO.IIDRekening = tSPPRekening.IIDRekening64 WHERE tSPP.inourut = " + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);

               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                     "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan,iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori" +
                     ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,isnull(tSPP.btKodekategoriPelaksana ,0) as btKodeKategoriPelaksana " +
                     ",isnull(tSPP.btKodeUrusanPelaksana ,0) as btKodeUrusanPelaksana , isnull(tSPP.btIDProgram ,0) as btIDProgram , isnull(tSPP.btIDkegiatan,0) as btIDKegiatan , " +
                      m_iRekRKPPKD + "  AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSPP. cJumlah,'Jurnal SP2D No.  ' + tSPP.sNoSP2d , 2 as iKelompok FROM tSPP WHERE tSPP.inourut = " + sNourut;


               _dbHelper.ExecuteNonQuery(SSQL);


               sNoJurnal = GetNextNo();
               SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                       "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan,bFromSKPD ) SELECT " +
                       sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                       ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSPP.dtBukukas as dtJurnal, 0 as iStatus, sNoSP2D as sNobukti, " + JENIS_JURNAL.JENIS_JURNALPENGELUARAN.ToString() + " as btJenisJurnal," +
                       "dtBukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + JENIS_SUMBERJURNAL.E_SUMBER_SP2D.ToString() + " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 1 as iKelompok , 0 as bPotongan," + bFromSKPD.ToString() + "  FROM tSPP WHERE inourut =" + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);

               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                     "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori" +
                     ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,isnull(tSPP.btKodekategoriPelaksana,0) as btKodeKategoriPelaksana " +
                     ",isnull(tSPPRekening.btKodeUrusanPelaksana,0) as btKodeUrusanPelaksana , isnull(tSPP.btIDProgram,0) as btIDProgram , isnull(tSPP.btIDkegiatan,0) as btIDKegiatan , " +
                     " tSPPRekening.IIDRekening64 AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSPPRekening.cJumlah,'Jurnal SP2DNo No. ' + tSPP.sNoSP2D, 1 as iKelompok FROM tSPP INNER JOIN tSPPRekening " +
                     " ON tSPP.inourut = tSPPRekening.inourut WHERE tSPP.inourut = " + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);


               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                     "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan,iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori" +
                     ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,isnull(tSPP.btKodekategoriPelaksana,0) as btKodeKategoriPelaksana " +
                     ",isnull(tSPP.btKodeUrusanPelaksana,0) as btKodeUrusanPelaksana , isnull(tSPP.btIDProgram,0) as btIDProgram , isnull(tSPP.btIDkegiatan,0) as btIDKegiatan , " +
                     m_iKodeEstimasiPadaSAL + " AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSPP.cJumlah,'Jurnal SP2D No. ' + tSPP.sNoSP2D ,1 as iKelompok FROM tSPP  WHERE tSPP.inourut = " + sNourut;

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

               SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                       "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan ,bFromSKPD) SELECT " +
                       sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                       ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSPP.dtBukukas as dtJurnal, 0 as iStatus, sNoSP2D as sNobukti, " + JENIS_JURNAL.JENIS_JURNALPENGELUARAN.ToString() + " as btJenisJurnal," +
                       "dtBukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + JENIS_SUMBERJURNAL.E_SUMBER_SP2D.ToString() + " as iJenisSumber,0 as btPeruntukan, bPPKD as bPPKD, " +
                       "1 as iKelompok , 0 as bPotongan,1 as bFromSKPD  FROM tSPP WHERE inourut =" + sNourut;


               _dbHelper.ExecuteNonQuery(SSQL);


               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                     "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori" +
                     ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,isnull(tSPP.btKodekategoriPelaksana,0) as btKodeKategoriPelaksana " +
                     ",isnull(tSPPRekening.btKodeUrusanPelaksana,0) as btKodeUrusanPelaksana , isnull(tSPP.btIDProgram,0) as btIDProgram , isnull(tSPP.btIDkegiatan,0) as btIDKegiatan , " +
                     "tSPPRekening.IIDRekening64 AS iiDRekening  , 1 as iNoUrut , 1 As iDebet , tSPPRekening.cJumlah,'Jurnal SP2D No. ' + tSPP.sNoSP2D, 1 as iKelompok FROM tSPP INNER JOIN tSPPRekening  ON tSPP.inourut = tSPPRekening.inourut " +
                     "  WHERE tSPP.inourut = " + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);



               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                     "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori" +
                     ",tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,isnull(tSPPRekening.btKodekategoriPelaksana ,0) as btKodeKategoriPelaksana " +
                     ",isnull(tSPPRekening.btKodeUrusanPelaksana,0) as btKodeUrusanPelaksana,isnull( tSPPRekening.btIDProgram,0) as btIDProgram , isnull(tSPPRekening.btIDkegiatan,0) as btIDKegiatan , " +
                      m_iKodeEstimasiPadaSAL.ToString() + " AS IIDRekening, 2 as iNoUrut , -1 As iDebet , SUM(tSPPRekening.cJumlah), tSPP.sKeteranganPekerjaan +' ' + tSPP.sNoSP2D as sKeterangan , 2 as iKelompok  FROM tSPP INNER JOIN tSPPRekening " +
                     " ON tSPP.inourut = tSPPRekening.inourut  WHERE tSPP.inourut = " + sNourut +
                     " GROUP BY  tSPP.btKodekategori" +
                     ",tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,isnull(tSPPRekening.btKodekategoriPelaksana ,0) " +
                     ",isnull(tSPPRekening.btKodeUrusanPelaksana,0) ,isnull( tSPPRekening.btIDProgram,0) , isnull(tSPPRekening.btIDkegiatan,0) , " +
                     " tSPP.sKeteranganPekerjaan , tSPP.sNoSP2D ";

               _dbHelper.ExecuteNonQuery(SSQL);




               sNoJurnal = GetNextNo();


               SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                     "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan ,bFromSKPD) SELECT " +
                     sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                     ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSPP.dtBukukas as dtJurnal, 0 as iStatus, sNobukti as sNobukti, " + JENIS_JURNAL.JENIS_JURNALPENGELUARAN.ToString() + " as btJenisJurnal," +
                     "dtBukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + JENIS_SUMBERJURNAL.E_SUMBER_SP2D.ToString() + " as iJenisSumber,0 as btPeruntukan, bPPKD as bPPKD, 2 as iKelompok , " +
                     "0 as bPotongan,1 as bFromSKPD FROM tSPP WHERE inourut =" + sNourut;


               _dbHelper.ExecuteNonQuery(SSQL);

               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                       "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                       "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori " +
                       ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPPRekening.btKodeUK ,tSPPRekening.btKodekategoriPelaksana as btKodeKategoriPelaksana " +
                       ",tSPPRekening.btKodeUrusanPelaksana as btKodeUrusanPelaksana , tSPPRekening.btIDProgram as btIDProgram , tSPPRekening.btIDKegiatan as btIDKegiatan , " +
                       "KOR_LRA_LO.IIDRekeningLO AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSPPRekening.cJumlah, 'Jurnal SP2D no  ' + sNoSP2D , 2 as iKelompok  FROM tSPP INNER JOIN tSPPRekening " +
                       " ON tSPP.inourut= tSPPRekening.inourut  inner Join KOR_LRA_LO on KOR_LRA_LO.IIDrekening= tSPPRekening.IIDREkening64 WHERE tSPP.inourut = " + sNourut + " AND tSPP.inobast =0";


               _dbHelper.ExecuteNonQuery(SSQL);

               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                     "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan,iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSPP.btKodekategori" +
                     ", tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK ,isnull(tSPP.btKodekategoriPelaksana ,0) as btKodeKategoriPelaksana " +
                     ",isnull(tSPP.btKodeUrusanPelaksana ,0) as btKodeUrusanPelaksana , isnull(tSPP.btIDProgram ,0) as btIDProgram , isnull(tSPP.btIDkegiatan,0) as btIDKegiatan , " +
                      m_iRekKasda.ToString() + "  AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSPP. cJumlah,'Jurnal SP2D No. ' + tSPP.sNoSP2d , 2 as iKelompok FROM tSPP WHERE tSPP.inourut = " + sNourut;

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

                SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                      "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan) SELECT " +
                      sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                      ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tSetor.dtBukukas as dtJurnal, 0 as iStatus, sNobukti as sNobukti, " + JENIS_JURNAL.JENIS_JURNALPENGELUARAN.ToString() + "  as btJenisJurnal," +
                      "dtBukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + JENIS_SUMBERJURNAL.E_SUMBER_SETOR.ToString() +
                      " as iJenisSumber,0 as btPeruntukan, bPPKD, 1 as iKelompok, 1 as bPotongan FROM tSetor WHERE inourut =" + sNourut;

                _dbHelper.ExecuteNonQuery(SSQL);




                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                      "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSetor.btKodekategori" +
                      ", tSetor.btKodeUrusan, tSetor.btKodeSKPD, tSetor.btKodeUK ,tSetor.btKodekategoriPelaksana as btKodeKategoriPelaksana " +
                      ",tSetor.btKodeUrusanPelaksana as btKodeUrusanPelaksana , tSetor.btIDProgram as btIDProgram , tSetor.btIDkegiatan as btIDKegiatan , " +
                      " tSetorRekening.IIDRekening AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tSetorRekening.cJumlah,'Jurnal Setor Pajak no  ' + tSetor.sNoBukti ,1 as iKelompok FROM tSetor INNER JOIN tSetorRekening " +
                      " ON tSetor.inourut = tSetorRekening.inourut  WHERE tSetor.inourut = " + sNourut;

                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                      "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSetor.btKodekategori" +
                      ", tSetor.btKodeUrusan, tSetor.btKodeSKPD, tSetor.btKodeUK ,tSetor.btKodekategoriPelaksana as btKodeKategoriPelaksana " +
                      ",tSetor.btKodeUrusanPelaksana as btKodeUrusanPelaksana , tSetor.btIDProgram as btIDProgram , tSetor.btIDkegiatan as btIDKegiatan , " +
                      " mKasBendahara.IIDrekening AS iiDRekening , 2 as iNoUrut , -1 As iDebet , sum(tSetorRekening.cJumlah) as cJumlah ,'Jurnal Setor pajak no  ' + tSetor.sNoBukti , 2 as iKelompok FROM tSetor INNER JOIN mkasbendahara " +
                      " ON tSetor.btKodekategori = mkasbendahara.btKodekategori and tSetor.btKodeUrusan = mkasbendahara.btKodeUrusan " +
                      " and tSetor.btKodeSKPD = mkasbendahara.btKodeSKPD and tSetor.bPPKD = mkasbendahara.bPPKD inner join tSetorRekening ON tSetor.inourut= tSetorRekening.inourut WHERE tSetor.inourut = " + sNourut +
                      " AND mkasbendahara.btJenis=2 " +
                      " GROUP BY tSetor.btKodekategori,tSetor.btKodeUrusan, tSetor.btKodeSKPD, tSetor.btKodeUK ,tSetor.btKodekategoriPelaksana " +
                      ",tSetor.btKodeUrusanPelaksana , tSetor.btIDProgram , tSetor.btIDkegiatan , " +
                      " mKasBendahara.IIDrekening , tSetor.sNoBukti ";


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
            //                JurnalSetorPendapatan13 sNourut, bFromSKPD
            //                JurnalPPKDSetorPendapatan sNourut, bFromSKPD
                            break;      
                    case SETOR_UYHD:
                            JurnalUYHD13 (sNourut, bFromSKPD);
                        break;

                    case SETOR_CP:
                        if (bFromSKPD == 0)
                            JurnalCP13(sNourut, bFromSKPD);

                        else
                            JurnalCP13PPKD(sNourut, bFromSKPD);
                        
                                break;
            //        Case SETOR_PAJAK
            //                JurnalSetorpajak sNourut, bFromSKPD
                  
                
                }

                  
            //End Select

                return true;
    
    

       }
       private bool JurnalUYHD13(string sNourut , int bFromSKPD ){
    
            string  sNoJurnal ;
            try{
    
                sNoJurnal = GetNextNo();
                SSQL = "INSERT INTO "  +m_sNamatabelJurnal  +" (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                         "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, bFromSKPD) SELECT " +
                         sNoJurnal  +" as inojurnal, "  +Tahun.ToString()  +" AS iTahun " +
                         ", "  + DateTime.Now.Date.ToSQLFormat()  +" AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, "  +JENIS_JURNAL.JENIS_JURNALPENGELUARAN  +" as btJenisJurnal," +
                         "dtbukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , "  +JENIS_SUMBERJURNAL.E_SUMBER_SETOR  +" as iJenisSumber,0 as btPeruntukan,0 as  bPPKD, 1 as ikelompok , 0 as bPotongan, "  +  bFromSKPD.ToString()  +" FROM tSetor WHERE inourut ="  +sNourut;
        
                _dbHelper.ExecuteNonQuery(SSQL);
                
                if (m_iAlsoForBUD == true ){

    
                    SSQL = "INSERT INTO "  +m_sNamatabelJurnalRekening  +" (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                          "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                          "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT "  +sNoJurnal  +" as inojurnal, tsetor.btKodekategori" +
                          ", tsetor.btKodeUrusan, tsetor.btKodeSKPD, tsetor.btKodeUK ,tsetor.btKodekategori as btKodeKategoriPelaksana " +
                          ",tsetor.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                          m_iRekRKPPKD.ToString()  +" AS iiDRekening , 1 as iNoUrut , 1 As iDebet , cJumlah, 'Jurnal Setor   ' + sNoBukti , 1 as Ikelompok FROM tsetor WHERE inourut = "  +sNourut;
          
                           _dbHelper.ExecuteNonQuery(SSQL);
      

                    } else {

                    SSQL = "INSERT INTO "  +m_sNamatabelJurnalRekening  +" (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                          "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                          "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT "  +sNoJurnal  +" as inojurnal, tsetor.btKodekategori" +
                          ", tsetor.btKodeUrusan, tsetor.btKodeSKPD, tsetor.btKodeUK ,tsetor.btKodekategori as btKodeKategoriPelaksana " +
                          ",tsetor.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                          m_iRekRKPPKD.ToString()  +" AS iiDRekening , 1 as iNoUrut , 1 As iDebet , cJumlah, 'Jurnal Setor   ' + sNoBukti , 1 as Ikelompok FROM tsetor WHERE inourut = "  +sNourut;
          
                           _dbHelper.ExecuteNonQuery(SSQL);
                    }
                 
                    SSQL = "INSERT INTO "  +m_sNamatabelJurnalRekening  +" (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                          "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                          "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok ) SELECT "  +sNoJurnal  +" as inojurnal, tsetor.btKodekategori " +
                          ", tsetor.btKodeUrusan, tsetor.btKodeSKPD, tsetor.btKodeUK ,tsetor.btKodekategori as btKodeKategoriPelaksana " +
                          ",tsetor.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                          "mKasBendahara.IIDrekening AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tsetor.cJumlah,'Jurnal Setor no  ' + sNoBukti, 1 as iKelompok FROM tsetor INNER JOIN mKasBendahara " +
                          " ON tsetor.btKodekategori = mkasbendahara.btKodekategori and tsetor.btKodeUrusan = mkasbendahara.btKodeUrusan " +
                          " and tsetor.btKodeSKPD = mkasbendahara.btKodeSKPD and tsetor.bPPKD= mkasbendahara.bPPKD WHERE tsetor.inourut = "  +sNourut +
                          " AND mkasbendahara.btJenis=2 ";
                  _dbHelper.ExecuteNonQuery(SSQL);
               
                   if (m_iAlsoForBUD == true ){
        
                           sNoJurnal = GetNextNo();

                           SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                                "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, sUraian,bFromSKPD ) SELECT " +
                                sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                                ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, " + JENIS_JURNAL.JENIS_JURNALPENGELUARAN.ToString() + " as btJenisJurnal," +
                                "dtbukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + JENIS_SUMBERJURNAL.E_SUMBER_SETOR.ToString() + " as iJenisSumber,1 as btPeruntukan,1 as  bPPKD, 1 as ikelompok ," +
                                "0 as bPotongan,sKeterangan as sUraian, " + bFromSKPD.ToString() + "   FROM tSetor WHERE inourut =" + sNourut;
                                 _dbHelper.ExecuteNonQuery(SSQL);
        
                            SSQL = "INSERT INTO "  +m_sNamatabelJurnalRekening  +" (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                                "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                                "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT "  +sNoJurnal  +" as inojurnal,"  +m_KodeKategoriPPKD  +" AS btKodekategori" +
                                ", "  +m_KodeUrusanPPKD.ToString()  +" AS btKodeUrusan, "  +m_KodeSKPDPPKD.ToString()  +" AS  btKodeSKPD, "  +m_KodeUKPPKD.ToString()  +" AS  btKodeUK ,"  +m_KodeKategoriPPKD.ToString()  +" as btKodeKategoriPelaksana " +
                                ","  +m_KodeUrusanPPKD.ToString()  +" as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                                m_iRekKasda.ToString()  +" AS iiDRekening , 1 as iNoUrut , 1 As iDebet , cJumlah, 'Jurnal Setor   ' + sNoBukti , 1 as Ikelompok FROM tsetor WHERE inourut = "  +sNourut;
                 
                             _dbHelper.ExecuteNonQuery(SSQL);
                                ;
            
                              SSQL = "INSERT INTO "  +m_sNamatabelJurnalRekening  +" (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                                "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                                "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok ) SELECT "  +sNoJurnal  +" as inojurnal,"  +m_KodeKategoriPPKD  +" AS btKodekategori" +
                                ", "  +m_KodeUrusanPPKD.ToString()  +" AS btKodeUrusan, "  +m_KodeSKPDPPKD.ToString()  +" AS  btKodeSKPD, "  +m_KodeUKPPKD.ToString()  +" AS  btKodeUK ,"  +m_KodeKategoriPPKD.ToString()  +" as btKodeKategoriPelaksana " +
                                ","  +m_KodeUrusanPPKD.ToString()  +" as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                                "mKasBendahara.IIDrekening AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tsetor.cJumlah,'Jurnal Setor no  ' + sNoBukti, 1 as iKelompok FROM tsetor INNER JOIN mKasBendahara " +
                                " ON tsetor.btKodekategori = mkasbendahara.btKodekategori and tsetor.btKodeUrusan = mkasbendahara.btKodeUrusan " +
                                " and tsetor.btKodeSKPD = mkasbendahara.btKodeSKPD and tsetor.bPPKD = mkasbendahara.bPPKD WHERE tsetor.inourut = "  +sNourut +
                                " AND mkasbendahara.btJenis=0";
            
        
        
            
            
                             _dbHelper.ExecuteNonQuery(SSQL);
                            }
                            return true;

                    } catch(Exception ex){
                        _lastError = ex.Message;
                        return false;
                    }
       }


       private bool  JurnalCP13(string sNourut , int bFromSKPD ){
    
            string sNoJurnal ;
            try
            {
                sNoJurnal = GetNextNo();

                SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                          "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, sUraian, bFromSKPD ) SELECT " +
                       sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                      ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, " + JENIS_JURNAL.JENIS_JURNALPENGELUARAN.ToString() + " as btJenisJurnal," +
                       "dtbukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + JENIS_SUMBERJURNAL.E_SUMBER_SETOR.ToString() +
                       " as iJenisSumber,0 as btPeruntukan, bPPKD as bPPKD, 1 as ikelompok , 0 as bPotongan,sKeterangan as sUraian , " +
                       bFromSKPD.ToString() + "   FROM tSetor WHERE inourut =" + sNourut;


                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                      "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tsetor.btKodekategori" +
                      ", tsetor.btKodeUrusan, tsetor.btKodeSKPD, tsetor.btKodeUK ,tsetor.btKodekategori as btKodeKategoriPelaksana " +
                      ",tsetor.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                      m_iKodeEstimasiPadaSAL.ToString() + " AS iiDRekening , 1 as iNoUrut , 1 As iDebet , cJumlah, 'Jurnal Setor No.' + sNoBukti , " +
                      "1 as iKelompok FROM tsetor WHERE inourut = " + sNourut;

                _dbHelper.ExecuteNonQuery(SSQL);


                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                      "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tsetor.btKodekategori " +
                      ", tsetor.btKodeUrusan, tsetor.btKodeSKPD, tsetor.btKodeUK ,tsetor.btKodekategori as btKodeKategoriPelaksana " +
                      ",tsetor.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                      "tSetorRekening.IIDrekening64 AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSetorRekening.cJumlah,'Jurnal Setor No  ' + sNoBukti , " +
                      "1 as iKelompok FROM tsetor INNER JOIN tSetorRekening " +
                      " ON tsetor.inourut = tSetorRekening.inourut WHERE tsetor.inourut = " + sNourut;



                _dbHelper.ExecuteNonQuery(SSQL);

                // '====================================================================

                sNoJurnal = GetNextNo();

                //'RB Peng ><LO

                SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                          "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, bFromSKPD ) SELECT " +
                          sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                        ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, " + JENIS_JURNAL.JENIS_JURNALPENGELUARAN.ToString() +
                        " as btJenisJurnal," +
                        "dtbukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + JENIS_SUMBERJURNAL.E_SUMBER_SETOR.ToString() +
                        " as iJenisSumber,0 as btPeruntukan, bPPKD as bPPKD, 2 as ikelompok , 0 as bPotongan, " + bFromSKPD.ToString() +
                        "   FROM tSetor WHERE inourut =" + sNourut;


                _dbHelper.ExecuteNonQuery(SSQL);


                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                      "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tsetor.btKodekategori " +
                      ", tsetor.btKodeUrusan, tsetor.btKodeSKPD, tsetor.btKodeUK ,tsetor.btKodekategori as btKodeKategoriPelaksana " +
                      ",tsetor.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                      "mKasBendahara.IIDrekening AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tsetor.cJumlah,'Jurnal Pengembalian No. ' + sNoBukti ,2 as iKelompok FROM tsetor INNER JOIN mKasBendahara " +
                      " ON tsetor.btKodekategori = mkasbendahara.btKodekategori and tsetor.btKodeUrusan = mkasbendahara.btKodeUrusan " +
                      " and tsetor.btKodeSKPD = mkasbendahara.btKodeSKPD  and tsetor.bPPKD = mkasbendahara.bPPKD WHERE tsetor.inourut = " + sNourut +
                      " AND mkasbendahara.btJenis=2 ";

                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                      "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                      " iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tsetor.btKodekategori" +
                      ", tsetor.btKodeUrusan, tsetor.btKodeSKPD, tsetor.btKodeUK ,tsetor.btKodekategori as btKodeKategoriPelaksana " +
                      ",tsetor.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                      " KOR_LRA_LO.IIDRekeningLO  AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSetorRekening.cJumlah, 'Jurnal Pengembalian No ' + sNoBukti , 2 as iKelompok FROM tsetor INNER JOIN tSetorRekening " +
                      " ON tsetor.inourut = tsetorRekening.inourut INNER join KOR_LRA_LO ON tSetorRekening.IIDRekening64=KOR_LRA_LO.iiDRekening " +
                      "WHERE tSetor.inourut = " + sNourut;

                _dbHelper.ExecuteNonQuery(SSQL);



                sNoJurnal = GetNextNo();
                //' Jurnal SKPD
                //' Kas di BP
                //'     RKPPKD >< Kas di BP



                SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                          "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, sUraian , bFromSKPD ) SELECT " +
                           sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                          ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, " +
                          JENIS_JURNAL.JENIS_JURNALPENGELUARAN.ToString() + " as btJenisJurnal," +
                           "dtbukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + JENIS_SUMBERJURNAL.E_SUMBER_SETOR.ToString() +
                           " as iJenisSumber,0 as btPeruntukan, 0 as bPPKD, 1 as ikelompok , 0 as bPotongan,sKeterangan as sUraian, " +
                           bFromSKPD.ToString() + "    FROM tSetor WHERE inourut =" + sNourut;


                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                      "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tsetor.btKodekategori" +
                      ", tsetor.btKodeUrusan, tsetor.btKodeSKPD, tsetor.btKodeUK ,tsetor.btKodekategori as btKodeKategoriPelaksana " +
                      ",tsetor.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                      m_iRekRKPPKD.ToString() + " AS iiDRekening , 1 as iNoUrut , 1 As iDebet , cJumlah, 'Jurnal Pengembalian No. ' + sNoBukti , " +
                      "1 as iKelompok FROM tsetor WHERE inourut = " + sNourut;

                _dbHelper.ExecuteNonQuery(SSQL);




                SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                      "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tsetor.btKodekategori " +
                      ", tsetor.btKodeUrusan, tsetor.btKodeSKPD, tsetor.btKodeUK ,tsetor.btKodekategori as btKodeKategoriPelaksana " +
                      ",tsetor.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                      "mKasBendahara.IIDrekening AS iiDRekening , 1 as iNoUrut , -1 As iDebet , tsetor.cJumlah,'Jurnal Pengembalian No. ' + sNoBukti ,2 as iKelompok FROM tsetor INNER JOIN mKasBendahara " +
                      " ON tsetor.btKodekategori = mkasbendahara.btKodekategori and tsetor.btKodeUrusan = mkasbendahara.btKodeUrusan " +
                      " and tsetor.btKodeSKPD = mkasbendahara.btKodeSKPD and tsetor.bPPKD = mkasbendahara.bPPKD WHERE tsetor.inourut = " + sNourut +
                      " AND mkasbendahara.btJenis=2 ";

                _dbHelper.ExecuteNonQuery(SSQL);


                if (m_iAlsoForBUD == true)
                {

                    sNoJurnal = GetNextNo();
                    SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                        "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, sUraian, bFromSKPD ) SELECT " +
                        sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                        ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, " + JENIS_JURNAL.JENIS_JURNALPENGELUARAN.ToString() + " as btJenisJurnal," +
                        "dtbukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + JENIS_SUMBERJURNAL.E_SUMBER_SETOR.ToString() + " as iJenisSumber,1 as btPeruntukan, " +
                        "1 as bPPKD, 1 as ikelompok , 0 as bPotongan , sKeterangan as sUraian, " + bFromSKPD.ToString() + "   FROM tSetor WHERE inourut =" + sNourut;


                    _dbHelper.ExecuteNonQuery(SSQL);

                    SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                          "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                          "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal," +
                           m_KodeKategoriPPKD.ToString() + " AS btKodekategori " +
                           ", " + m_KodeUrusanPPKD.ToString() + " AS btKodeUrusan, " + m_KodeSKPDPPKD.ToString() + " AS  btKodeSKPD, " + m_KodeUKPPKD.ToString() +
                           " AS  btKodeUK ," + m_KodeKategoriPPKD.ToString() + " as btKodeKategoriPelaksana " +
                            "," + m_KodeUrusanPPKD.ToString() + " as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                          m_iRekKasda.ToString() + " AS iiDRekening , 1 as iNoUrut , 1 As iDebet , cJumlah, 'Jurnal Pengembalian No. ' + sNoBukti , " +
                          "1 as iKelompok FROM tsetor WHERE inourut = " + sNourut;

                    _dbHelper.ExecuteNonQuery(SSQL);

                    SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                        "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                        "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal," + m_KodeKategoriPPKD.ToString() + " AS btKodekategori" +
                          ", " + m_KodeUrusanPPKD.ToString() + " AS btKodeUrusan, " + m_KodeSKPDPPKD.ToString() + " AS  btKodeSKPD, " + m_KodeUKPPKD.ToString() + " AS  btKodeUK ," +
                          m_KodeKategoriPPKD.ToString() + " as btKodeKategoriPelaksana " +
                          "," + m_KodeUrusanPPKD.ToString() + " as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                        "mKasBendahara.IIDrekening AS iiDRekening , 1 as iNoUrut , -1 As iDebet , tsetor.cJumlah,'Jurnal Pengembalian No. ' + sNoBukti ,2 as iKelompok FROM tsetor INNER JOIN mKasBendahara " +
                        " ON tsetor.btKodekategori = mkasbendahara.btKodekategori and tsetor.btKodeUrusan = mkasbendahara.btKodeUrusan " +
                        " and tsetor.btKodeSKPD = mkasbendahara.btKodeSKPD and tsetor.bPPKD = mkasbendahara.bPPKD WHERE tsetor.inourut = " + sNourut +
                        " AND mkasbendahara.btJenis=0";



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
                        SSQL = "INSERT INTO "  +m_sNamatabelJurnal  +" (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                                  "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, sUraian, bFromSKPD ) SELECT " +
                                   sNoJurnal  +" as inojurnal, "  +Tahun.ToString()  +" AS iTahun " +
                                  ", "  +DateTime.Now.Date.ToSQLFormat()  +" AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, "  +JENIS_JURNAL.JENIS_JURNALPENGELUARAN.ToString()  +" as btJenisJurnal," +
                                   "dtbukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , "  +JENIS_SUMBERJURNAL.E_SUMBER_SETOR.ToString()  + 
                                   " as iJenisSumber,0 as btPeruntukan, 1 as bPPKD, 1 as ikelompok , 0 as bPotongan,sKeterangan as sUraian , "  + 
                                   bFromSKPD.ToString()  +"   FROM tSetor WHERE inourut ="  +sNourut;
            
    
                     _dbHelper.ExecuteNonQuery(SSQL);
    
                        SSQL = "INSERT INTO "  +m_sNamatabelJurnalRekening  +" (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                              "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                              "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT "  +sNoJurnal  +" as inojurnal, tsetor.btKodekategori" +
                              ", tsetor.btKodeUrusan, tsetor.btKodeSKPD, tsetor.btKodeUK ,tsetor.btKodekategori as btKodeKategoriPelaksana " +
                              ",tsetor.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                              m_iKodeEstimasiPadaSAL  +" AS iiDRekening , 1 as iNoUrut , 1 As iDebet , cJumlah, 'Jurnal Setor No.' + sNoBukti , " +
                              "1 as iKelompok FROM tsetor WHERE inourut = "  +sNourut;
          
                           _dbHelper.ExecuteNonQuery(SSQL);
          
    
                        SSQL = "INSERT INTO "  +m_sNamatabelJurnalRekening  +" (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                              "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                              "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT "  +sNoJurnal  +" as inojurnal, tsetor.btKodekategori " +
                              ", tsetor.btKodeUrusan, tsetor.btKodeSKPD, tsetor.btKodeUK ,tsetor.btKodekategori as btKodeKategoriPelaksana " +
                              ",tsetor.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                              "tSetorRekening.IIDrekening64 AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSetorRekening.cJumlah,'Jurnal Setor No  ' + sNoBukti , 1 as iKelompok FROM tsetor INNER JOIN tSetorRekening " +
                              " ON tsetor.inourut = tSetorRekening.inourut WHERE tsetor.inourut = "  +sNourut;
          
          
            
                     _dbHelper.ExecuteNonQuery(SSQL);
                   
                        //'====================================================================
    
                        sNoJurnal = GetNextNo();
    
                       // 'RB Peng ><LO
    
                        SSQL = "INSERT INTO "  +m_sNamatabelJurnal  +" (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                                  "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, bFromSKPD ) SELECT " +
                                  sNoJurnal  +" as inojurnal, "  +Tahun.ToString()  +" AS iTahun " +
                                ", "  + DateTime.Now.Date.ToSQLFormat()  +" AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, "  +JENIS_JURNAL.JENIS_JURNALPENGELUARAN.ToString()  +" as btJenisJurnal," +
                                "dtbukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , "  +JENIS_SUMBERJURNAL.E_SUMBER_SETOR.ToString()  + 
                                " as iJenisSumber,0 as btPeruntukan, bPPKD as bPPKD, 2 as ikelompok , 0 as bPotongan, "  + bFromSKPD.ToString()  + 
                                "   FROM tSetor WHERE inourut ="  +sNourut;
            
    
                     _dbHelper.ExecuteNonQuery(SSQL);
    
     
                        SSQL = "INSERT INTO "  +m_sNamatabelJurnalRekening  +" (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                              "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                              "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT "  +sNoJurnal  +" as inojurnal, tsetor.btKodekategori " +
                              ", tsetor.btKodeUrusan, tsetor.btKodeSKPD, tsetor.btKodeUK ,tsetor.btKodekategori as btKodeKategoriPelaksana " +
                              ",tsetor.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                              "mKasBendahara.IIDrekening AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tsetor.cJumlah,'Jurnal Pengembalian No. ' + sNoBukti ,2 as iKelompok FROM tsetor INNER JOIN mKasBendahara " +
                              " ON tsetor.btKodekategori = mkasbendahara.btKodekategori and tsetor.btKodeUrusan = mkasbendahara.btKodeUrusan " +
                              " and tsetor.btKodeSKPD = mkasbendahara.btKodeSKPD  and tsetor.bPPKD = mkasbendahara.bPPKD WHERE tsetor.inourut = "  +sNourut +
                              " AND mkasbendahara.btJenis=2 ";
          
                     _dbHelper.ExecuteNonQuery(SSQL);
    
                        SSQL = "INSERT INTO "  +m_sNamatabelJurnalRekening  +" (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                              "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                              " iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT "  +sNoJurnal  +" as inojurnal, tsetor.btKodekategori" +
                              ", tsetor.btKodeUrusan, tsetor.btKodeSKPD, tsetor.btKodeUK ,tsetor.btKodekategori as btKodeKategoriPelaksana " +
                              ",tsetor.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                              " KOR_LRA_LO.IIDRekeningLO  AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSetorRekening.cJumlah, 'Jurnal Pengembalian No ' + sNoBukti , 2 as iKelompok FROM tsetor INNER JOIN tSetorRekening " +
                              " ON tsetor.inourut = tsetorRekening.inourut INNER join KOR_LRA_LO ON tSetorRekening.IIDRekening64=KOR_LRA_LO.iiDRekening WHERE tSetor.inourut = "  +sNourut;
      
                     _dbHelper.ExecuteNonQuery(SSQL);
      
      
      
                        sNoJurnal = GetNextNo();
                        //' Jurnal SKPD
                        //' Kas di BP
                        //'     RKPPKD >< Kas di BP
    
        
    
                        SSQL = "INSERT INTO "  +m_sNamatabelJurnal  +" (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                                  "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, sUraian , bFromSKPD ) SELECT " +
                                   sNoJurnal  +" as inojurnal, "  +Tahun.ToString()  +" AS iTahun " +
                                  ", "  + DateTime.Now.Date.ToSQLFormat()  +" AS  dtInput, dtbukukas as dtJurnal, 0 as iStatus, sNoBukti as sNobukti, "  +JENIS_JURNAL.JENIS_JURNALPENGELUARAN.ToString()  +" as btJenisJurnal," +
                                   "dtbukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , "  +JENIS_SUMBERJURNAL.E_SUMBER_SETOR.ToString()  +" as iJenisSumber,0 as btPeruntukan, 1 as bPPKD," +
                                   "1 as ikelompok , 0 as bPotongan,sKeterangan as sUraian, "  +bFromSKPD.ToString()  +"    FROM tSetor WHERE inourut ="  +sNourut;
            
    
                        _dbHelper.ExecuteNonQuery(SSQL);
    
                        SSQL = "INSERT INTO "  +m_sNamatabelJurnalRekening  +" (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                              "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                              "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT "  +sNoJurnal  +" as inojurnal, tsetor.btKodekategori" +
                              ", tsetor.btKodeUrusan, tsetor.btKodeSKPD, tsetor.btKodeUK ,tsetor.btKodekategori as btKodeKategoriPelaksana " +
                              ",tsetor.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                              m_iRekKasda.ToString()  +" AS iiDRekening , 1 as iNoUrut , 1 As iDebet , cJumlah, 'Jurnal Pengembalian No. ' + sNoBukti , " +
                              "1 as iKelompok FROM tsetor WHERE inourut = "  +sNourut;
          
                           _dbHelper.ExecuteNonQuery(SSQL);
     
    
     
                        SSQL = "INSERT INTO "  +m_sNamatabelJurnalRekening  +" (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                              "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                              "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT "  +sNoJurnal  +" as inojurnal, tsetor.btKodekategori " +
                              ", tsetor.btKodeUrusan, tsetor.btKodeSKPD, tsetor.btKodeUK ,tsetor.btKodekategori as btKodeKategoriPelaksana " +
                              ",tsetor.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                              "mKasBendahara.IIDrekening AS iiDRekening , 1 as iNoUrut , -1 As iDebet , tsetor.cJumlah,'Jurnal Pengembalian No. ' + sNoBukti ,2 as iKelompok FROM tsetor INNER JOIN mKasBendahara " +
                              " ON tsetor.btKodekategori = mkasbendahara.btKodekategori and tsetor.btKodeUrusan = mkasbendahara.btKodeUrusan " +
                              " and tsetor.btKodeSKPD = mkasbendahara.btKodeSKPD and tsetor.bPPKD = mkasbendahara.bPPKD WHERE tsetor.inourut = "  +sNourut +
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

               SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                     "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan) SELECT " +
                     sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                     ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tPanjar.dtBukukas as dtJurnal, 0 as iStatus, sNobukti as sNobukti, " +
                     JENIS_JURNAL.JENIS_JURNALPENGELUARAN.ToString() + "  as btJenisJurnal," +
                     "dtBukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + JENIS_SUMBERJURNAL.E_SUMBER_PANJAR.ToString() +
                     " as iJenisSumber,0 as btPeruntukan, bPPKD, 1 as iKelompok, 1 as bPotongan FROM tPanjar WHERE inourut =" + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);


               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                    "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                    "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tPanjar.btKodekategori" +
                    ", tPanjar.btKodeUrusan, tPanjar.btKodeSKPD, tPanjar.btKodeUK ,tPanjar.btKodekategoriPelaksana as btKodeKategoriPelaksana " +
                    ",tPanjar.btKodeUrusanPelaksana as btKodeUrusanPelaksana , tPanjar.btIDProgram as btIDProgram , tPanjar.btIDkegiatan as btIDKegiatan , " +
                    " mKasBendahara.IIDrekening AS iiDRekening , 1 as iNoUrut , 1 As iDebet , sum(tPanjarPotongan.cJumlah) as cJumlah ,'Jurnal pungut pajak no  ' + tPanjar.sNoBukti , 2 as iKelompok FROM tPanjar INNER JOIN mkasbendahara " +
                    " ON tPanjar.btKodekategori = mkasbendahara.btKodekategori and tPanjar.btKodeUrusan = mkasbendahara.btKodeUrusan " +
                    " and tPanjar.btKodeSKPD = mkasbendahara.btKodeSKPD and tPanjar.bPPKD= mkasbendahara.bPPKD inner join tPanjarPotongan ON " +
                    "tPanjar.inourut= tPanjarPotongan.inourut WHERE tPanjar.inourut = " + sNourut + " AND mkasbendahara.btJenis=2 " +
                    " GROUP BY tPanjar.btKodekategori,tPanjar.btKodeUrusan, tPanjar.btKodeSKPD, tPanjar.btKodeUK ,tPanjar.btKodekategoriPelaksana " +
                    ",tPanjar.btKodeUrusanPelaksana , tPanjar.btIDProgram , tPanjar.btIDkegiatan , " +
                    " mKasBendahara.IIDrekening , tPanjar.sNoBukti";


               _dbHelper.ExecuteNonQuery(SSQL);

               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                     "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tPanjar.btKodekategori" +
                     ", tPanjar.btKodeUrusan, tPanjar.btKodeSKPD, tPanjar.btKodeUK ,tPanjar.btKodekategoriPelaksana as btKodeKategoriPelaksana " +
                     ",tPanjar.btKodeUrusanPelaksana as btKodeUrusanPelaksana , tPanjar.btIDProgram as btIDProgram , tPanjar.btIDkegiatan as btIDKegiatan , " +
                     " tPanjarPotongan.IIDRekening AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tPanjarPotongan.cJumlah,'Jurnal Pungut Pajak no  ' + tPanjar.sNoBukti ,1 as iKelompok FROM tPanjar INNER JOIN tPanjarPotongan " +
                     " ON tPanjar.inourut = tPanjarPotongan.inourut  WHERE tPanjar.inourut = " + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);
               return true;
           }
           catch (Exception ex)
           {
               _lastError = ex.Message + "\n  " + SSQL;
               return false;
           }

       }
       public bool ProcessJurnalPANJAR13(string sNourut, int bFromSKPD )
       {

           try
           {

               string sNoJurnal;

               sNoJurnal = GetNextNo();

               SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                     "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan ,bFromSKPD) SELECT " +
                     sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                     ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tPanjar.dtBukukas as dtJurnal, 0 as iStatus, sNobukti as sNobukti, " +
                     JENIS_JURNAL.JENIS_JURNALPENGELUARAN.ToString() + "  as btJenisJurnal," +
                     "dtBukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + JENIS_SUMBERJURNAL.E_SUMBER_PANJAR.ToString() +
                     " as iJenisSumber,0 as btPeruntukan, bPPKD, 1 as iKelompok , 0 as bPotongan, " + bFromSKPD.ToString() + "  FROM tPanjar WHERE inourut =" + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);




               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
               "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
               "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tPanjar.btKodekategori" +
               ", tPanjar.btKodeUrusan, tPanjar.btKodeSKPD, tPanjar.btKodeUK ,tPanjar.btKodekategoriPelaksana as btKodeKategoriPelaksana " +
               ",tPanjar.btKodeUrusanPelaksana as btKodeUrusanPelaksana , tPanjar.btIDProgram as btIDProgram , tPanjar.btIDkegiatan as btIDKegiatan , " +
               " tPanjarRekening.IIDRekening64 AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tPanjarRekening.cJumlah,'Jurnal SPJ  no  ' + tPanjar.sNoBukti, 1 as iKelompok " +
               " FROM tPanjar INNER JOIN tPanjarRekening " +
               " ON tPanjar.inourut = tPanjarRekening.inourut WHERE tPanjar.inourut = " + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);


               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                     "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tPanjar.btKodekategori" +
                     ", tPanjar.btKodeUrusan, tPanjar.btKodeSKPD, tPanjar.btKodeUK ,tPanjar.btKodekategoriPelaksana as btKodeKategoriPelaksana " +
                     ",tPanjar.btKodeUrusanPelaksana as btKodeUrusanPelaksana , tPanjar.btIDProgram as btIDProgram , tPanjar.btIDkegiatan as btIDKegiatan , " +
                     m_iKodeEstimasiPadaSAL.ToString() + " AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tPanjar.cJumlah,'Jurnal SPJ  no  ' + tPanjar.sNoBukti ," +
                     "1 as iKelompok FROM tPanjar  WHERE tPanjar.inourut = " + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);


               sNoJurnal = GetNextNo();


               SSQL = "INSERT INTO " + m_sNamatabelJurnal + " (iNoJurnal, iTahun, dtInput, dtJurnal, iStatus, sNoBukti, " +
                     "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan, bFromSKPD ) SELECT " +
                     sNoJurnal + " as inojurnal, " + Tahun.ToString() + " AS iTahun " +
                     ", " + DateTime.Now.Date.ToSQLFormat() + " AS  dtInput, tPanjar.dtBukukas as dtJurnal, 0 as iStatus, sNobukti as sNobukti, " +
                     JENIS_JURNAL.JENIS_JURNALPENGELUARAN.ToString() + " as btJenisJurnal," +
                     "dtBukukas as  dtTanggalBukti,  sNoBukti as sNoBukukas, inourut  as iSumber , " + JENIS_SUMBERJURNAL.E_SUMBER_PANJAR.ToString() +
                     " as iJenisSumber,0 as btPeruntukan, bPPKD, 2 as iKelompok , 0 as bPotongan , " + bFromSKPD.ToString() + " FROM tPanjar WHERE inourut =" + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);

               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                     "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                     "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tPanjar.btKodekategori" +
                     ", tPanjar.btKodeUrusan, tPanjar.btKodeSKPD, tPanjar.btKodeUK ,tPanjar.btKodekategoriPelaksana as btKodeKategoriPelaksana " +
                     ",tPanjar.btKodeUrusanPelaksana as btKodeUrusanPelaksana , tPanjar.btIDProgram as btIDProgram , tPanjar.btIDkegiatan as btIDKegiatan , " +
                     " KOR_LRA_LO.IIDRekeningLO AS iiDRekening , 1 as iNoUrut , 1 As iDebet , tPanjarRekening.cJumlah,'Jurnal SPJ  no  ' + tPanjar.sNoBukti , " +
                     " 2 as iKelompok FROM tPanjar INNER JOIN tPanjarRekening " +
                     " ON tPanjar.inourut = tPanjarRekening.inourut  INNER JOIN KOR_LRA_LO ON KOR_LRA_LO.IIDRekening = tPanjarRekening.IIDRekening64 WHERE tPanjar.inourut = " + sNourut;


               _dbHelper.ExecuteNonQuery(SSQL);



               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                    "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                    "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tPanjar.btKodekategori" +
                    ", tPanjar.btKodeUrusan, tPanjar.btKodeSKPD, tPanjar.btKodeUK ,tPanjar.btKodekategoriPelaksana as btKodeKategoriPelaksana " +
                    ",tPanjar.btKodeUrusanPelaksana as btKodeUrusanPelaksana , tPanjar.btIDProgram as btIDProgram , tPanjar.btIDkegiatan as btIDKegiatan , " +
                     "mKasBendahara.IIDrekening AS iiDRekening , 2 as iNoUrut , -1 As iDebet , cJumlah,'Jurnal SPJ no  ' + tPanjar.sNoBukti , 2 as iKelompok FROM tPanjar INNER JOIN mkasbendahara " +
                    " ON tPanjar.btKodekategori = mkasbendahara.btKodekategori and tPanjar.btKodeUrusan = mkasbendahara.btKodeUrusan " +
                    " and tPanjar.btKodeSKPD = mkasbendahara.btKodeSKPD and mkasbendahara.bPPKD = tPanjar.bPPKD WHERE tPanjar.inourut = " + sNourut +
                    " AND mkasbendahara.btJenis=2 and mKasBendahara.bPPKD = tPanjar.bPPKD";

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
                       "dtSKRSKPD as  dtTanggalBukti,  tSKRSKPD.sNoBukti as sNoBukukas, inourut  as iSumber , " + JENIS_SUMBERJURNAL.E_SUMBER_SKR.ToString() +
                       " as iJenisSumber,0 as btPeruntukan,  bPPKD, 1 as iKelompok FROM tSKRSKPD WHERE inourut =" + sNourut;

               _dbHelper.ExecuteNonQuery(SSQL);



               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                      "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSKRSKPD.btKodekategori" +
                      ", tSKRSKPD.btKodeUrusan, tSKRSKPD.btKodeSKPD, tSKRSKPD.btKodeUK ,tSKRSKPD.btKodekategori as btKodeKategoriPelaksana " +
                      ",tSKRSKPD.btKodeUrusan as btKodeUrusanPelaksana , 0  as btIDProgram , 0 AS btIDKegiatan , " +
                      "korpermenpiutang.iidpiutang AS iiDRekening , 1 as iNoUrut , 1 As iDebet , SUM(tSKRSKPDRekening.cJumlah) as cJumlah, 'Jurnal Penerimaan SKR ' + tSKRSKPD.sNoBukti, 1 as iKelompok FROM tSKRSKPD INNER JOIN tSKRSKPDRekening" +
                      " ON tSKRSKPD.inourut= tSKRSKPDRekening.inourut Inner JOIN korpermenpiutang ON korpermenpiutang.IIDRekening= tSKRSKPDRekening.IIDRekening64 WHERE tSKRSKPD.inourut = " + sNourut +
                      " GROUP BY tSKRSKPD.btKodekategori,tSKRSKPD.btKodeUrusan, tSKRSKPD.btKodeSKPD, tSKRSKPD.btKodeUK ,korpermenpiutang.iidpiutang , tSKRSKPD.sNoBukti ";

               _dbHelper.ExecuteNonQuery(SSQL);
               SSQL = "INSERT INTO " + m_sNamatabelJurnalRekening + " (iNoJurnal, btKodeKategori, btKodeUrusan, btKodeSKPD, " +
                      "btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram, btIDKegiatan, " +
                      "iIDRekening, iNoUrut, iDebet, cJumlah, sKeterangan, iKelompok) SELECT " + sNoJurnal + " as inojurnal, tSKRSKPD.btKodekategori" +
                      ", tSKRSKPD.btKodeUrusan, tSKRSKPD.btKodeSKPD, tSKRSKPD.btKodeUK ,tSKRSKPD.btKodekategori as btKodeKategoriPelaksana " +
                      ",tSKRSKPD.btKodeUrusan as btKodeUrusanPelaksana , 0 as btIDProgram , 0 as btIDKegiatan , " +
                      " KOR_LRA_LO.IIDRekeningLO AS iiDRekening , 2 as iNoUrut , -1 As iDebet , tSKRSKPDRekening.cJumlah,'Jurnal Penerimaan SKR ' + tSKRSKPD.sNoBukti, " +
                      " 1 as iKelompok FROM tSKRSKPD INNER JOIN tSKRSKPDRekening " +
                      " ON tSKRSKPD.inourut = tSKRSKPDRekening.inourut  INNER JOIN KOR_LRA_LO ON KOR_LRA_LO.IIDRekening = tSKRSKPDRekening.IIDRekening64 " +
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
