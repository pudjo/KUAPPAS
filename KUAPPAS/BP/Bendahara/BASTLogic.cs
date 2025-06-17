using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OleDb;
using DTO;
using DTO.Bendahara;
using Formatting;
using BP;
using DataAccess;

namespace BP.Bendahara
{
    public class BASTLogic:BP
    {
        public BASTLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tBAST";
          //  PerbaikiTable();
        }
        private void PerbaikiTable()
        {
           

          
        }
        public bool Simpan(ref BAST dh){
              try
              {

                  long _noUrut = 0;
                  if (dh.NoUrut == 0)
                  {

                      _noUrut = ReadNo(E_KOLOM_NOURUT.CON_URUT_BAST, dh.IDDInas);


                      SSQL = "INSERT INTO tBAST (iTahun ,IDDInas, IDUrusan, IDProgram, IDKegiatan, IDSUbKegiatan,inourut ,  " +
                           " sNoBAST ,  dtBAST , iPihakKetiga ,iStatus, sUraian,bPPKD ,inokontrak," +
                           " btKodekategori, btKodeUrusan, btKodeSKPD, btKodeUK,btKodekategoriPelaksana, btKodeUrusanPelaksana, "+
                           " btIDProgram, btIDkegiatan, btIDSUbKegiatan) values (" +
                           " @Tahun ,@IDDInas, @IDUrusan, @IDProgram, @IDKegiatan, @IDSUbKegiatan,@inourut ,  " +
                           " @NoBAST ,  @TanggalBAST , @PihakKetiga ,@Status, @Uraian,@PPKD ,@nokontrak,"+
                           " @Kodekategori, @KodeUrusan, @KodeSKPD, @KodeUK,@KodekategoriPelaksana, @KodeUrusanPelaksana, "+
                           " @KodeProgram, @Kodekegiatan, @KodeSUbKegiatan)";

                      DBParameterCollection paramCollection = new DBParameterCollection();
                      paramCollection.Add(new DBParameter("@Tahun",dh.Tahun));
                      paramCollection.Add(new DBParameter("@IDDInas",dh.IDDInas));
                      paramCollection.Add(new DBParameter("@IDUrusan",dh.IDUrusan));
                      paramCollection.Add(new DBParameter("@IDProgram",dh.IDProgram));
                      paramCollection.Add(new DBParameter("@IDKegiatan",dh.IDKegiatan));
                      paramCollection.Add(new DBParameter("@IDSUbKegiatan",dh.IDSubKegiatan));
                      paramCollection.Add(new DBParameter("@inourut",_noUrut));
                      paramCollection.Add(new DBParameter("@NoBAST",dh.NoBAST)); 
                      paramCollection.Add(new DBParameter("@TanggalBAST",dh.dtBAST,DbType.Date));
                      paramCollection.Add(new DBParameter("@PihakKetiga",dh.PihakKetiga));
                      paramCollection.Add(new DBParameter("@Status",0));
                      paramCollection.Add(new DBParameter("@Uraian",dh.Uraian));
                      paramCollection.Add(new DBParameter("@PPKD",0));
                      paramCollection.Add(new DBParameter("@nokontrak",dh.NoUrutKontrak));
                      paramCollection.Add(new DBParameter("@Kodekategori",dh.Kodekategori));
                      paramCollection.Add(new DBParameter("@KodeUrusan",dh.KodeUrusan));
                      paramCollection.Add(new DBParameter("@KodeSKPD",dh.KodeSKPD));
                      paramCollection.Add(new DBParameter("@KodeUK",dh.KodeUk));
                      paramCollection.Add(new DBParameter("@KodekategoriPelaksana",dh.KodekategoriPelaksana));
                      paramCollection.Add(new DBParameter("@KodeUrusanPelaksana",dh.KodeUrusanPelaksana));
                      paramCollection.Add(new DBParameter("@KodeProgram",dh.KodeProgram));
                      paramCollection.Add(new DBParameter("@Kodekegiatan",dh.KodeKegiatan));
                      paramCollection.Add(new DBParameter("@KodeSUbKegiatan", dh.KodeSubKegiatan));

                      _dbHelper.ExecuteNonQuery(SSQL,paramCollection);
                      dh.NoUrut = _noUrut;
                  }
                  else
                  {

                      _noUrut = dh.NoUrut;
                      SSQL = "UPDATE tBAST SET iTahun=@Tahun ,IDDInas=@IDDInas, IDUrusan=@IDUrusan, IDProgram=@IDProgram, " +
                           " IDKegiatan=@IDKegiatan, IDSUbKegiatan=@IDSUbKegiatan,  " +
                           " sNoBAST =@NoBAST,  dtBAST =@TanggalBAST, iPihakKetiga =@PihakKetiga, iStatus=@Status, sUraian=@Uraian,inokontrak=@nokontrak," +
                           " btKodekategori=@Kodekategori, btKodeUrusan=@KodeUrusan, btKodeSKPD=@KodeSKPD," +
                           " btKodeUK=@KodeUK,btKodekategoriPelaksana=@KodekategoriPelaksana, btKodeUrusanPelaksana=@KodeUrusanPelaksana, " +
                           " btIDProgram=@KodeProgram, btIDkegiatan=@Kodekegiatan, btIDSUbKegiatan=@KodeSUbKegiatan " +
                           " WHERE inourut = @inourut";
                  
                      DBParameterCollection paramCollection = new DBParameterCollection();
                      paramCollection.Add(new DBParameter("@Tahun", dh.Tahun));
                      paramCollection.Add(new DBParameter("@IDDInas", dh.IDDInas));
                      paramCollection.Add(new DBParameter("@IDUrusan", dh.IDUrusan));
                      paramCollection.Add(new DBParameter("@IDProgram", dh.IDProgram));
                      paramCollection.Add(new DBParameter("@IDKegiatan", dh.IDKegiatan));
                      paramCollection.Add(new DBParameter("@IDSUbKegiatan", dh.IDSubKegiatan));
                      paramCollection.Add(new DBParameter("@NoBAST", dh.NoBAST));
                      paramCollection.Add(new DBParameter("@TanggalBAST", dh.dtBAST, DbType.Date));
                      paramCollection.Add(new DBParameter("@PihakKetiga", dh.PihakKetiga));
                      paramCollection.Add(new DBParameter("@Status", dh.Status));
                      paramCollection.Add(new DBParameter("@Uraian", dh.Uraian));
                      paramCollection.Add(new DBParameter("@nokontrak", dh.NoUrutKontrak));
                      paramCollection.Add(new DBParameter("@Kodekategori", dh.Kodekategori));
                      paramCollection.Add(new DBParameter("@KodeUrusan", dh.KodeUrusan));
                      paramCollection.Add(new DBParameter("@KodeSKPD", dh.KodeSKPD));
                      paramCollection.Add(new DBParameter("@KodeUK", dh.KodeUk));
                      paramCollection.Add(new DBParameter("@KodekategoriPelaksana", dh.KodekategoriPelaksana));
                      paramCollection.Add(new DBParameter("@KodeUrusanPelaksana", dh.KodeUrusanPelaksana));
                      paramCollection.Add(new DBParameter("@KodeProgram", dh.KodeProgram));
                      paramCollection.Add(new DBParameter("@Kodekegiatan", dh.KodeKegiatan));
                      paramCollection.Add(new DBParameter("@KodeSUbKegiatan", dh.KodeSubKegiatan));
                      paramCollection.Add(new DBParameter("@inourut", _noUrut));

                      _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                      SSQL = "DELETE tBASTRekening WHERE iNoUrut =" + dh.NoUrut.ToString();
                      _dbHelper.ExecuteNonQuery(SSQL);


                  }

                  foreach (BASTRekening kr in dh.Rekening)
                  {
                      SSQL = "INSERT INTO tBASTRekening (iNoUrut,IIDRekening,cJumlah) values ( @NoUrut,@IDRekening,@Jumlah )";

                      DBParameterCollection paramCollectiondeteil = new DBParameterCollection();
                      paramCollectiondeteil.Add(new DBParameter("@NoUrut", dh.NoUrut));
                      paramCollectiondeteil.Add(new DBParameter("@IDRekening", kr.IDRekening));
                      paramCollectiondeteil.Add(new DBParameter("@Jumlah", kr.Jumlah,DbType.Decimal));


                      _dbHelper.ExecuteNonQuery(SSQL, paramCollectiondeteil);


                  }
                  return true;


              }
              catch (Exception ex)
              {
                  _lastError = ex.Message;
                  _isError = true;

                  return false;
              }


          }
        public  List<BASTRekening> GetDetail(long inoUrut)
        {
            KontrakRekening oKontrak = new KontrakRekening();
            List<BASTRekening> _lst = new List<BASTRekening>();
            try
            {
                SSQL = "SELECT tBASTRekening.* , mRekening.sNamaRekening as Nama FROM tBASTRekening INNER JOIN mRekening on tBASTRekening.iidRekening = mRekening.IIDRekening WHERE tBASTRekening.iNourut = " + inoUrut.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new BASTRekening()
                                {
                                    NoUrut = DataFormat.GetString(dr["iNourut"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    NamaRekening = DataFormat.GetString(dr["Nama"]),
                                }).ToList();
                    }
                }
                return _lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }
        }
        public List<BAST> GetByIDDInas(int idDinas)
        {


            List<BAST> _lst = new List<BAST>();
            try
            {
                SSQL = "SELECT tBAST.*, tSPP.sNoSP2D, tSPP.dtBukukas, tSPP.inourut as NoUrutSPP, tSPP.cJumlah as jumlahSP2D ,tKontrak.sNoKontrak, tKontrak.inourut as NoUrutKontrak, " +
                    " mPerusahaan.sNamaPerusahaan as PihakKetiga FROM " + m_sNamaTabel +
                    " LEFT OUTER JOIN tSPP ON tSPP.inoBAST = tBAST.inourut LEFT JOIN tKontrak On tBAST.iNoKontrak = tKontrak.inourut " +
                    " LEFT JOIN mPerusahaan On mPerusahaan.IDPerusahaan = tBAST.iPihakKetiga where tBAST.iTahun = @Tahun and tBAST.IDDInas = @DINAS  Order by inourut ";//


                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@Tahun", Tahun));
                paramCollection.Add(new DBParameter("@DINAS", idDinas));


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new BAST()
                                {

                                    NoUrut = DataFormat.GetLong(dr["iNourut"]),
                                    Tahun = DataFormat.GetSingle(dr["iTahun"]),
                                    //Kodekategori = DataFormat.GetInteger(dr["btKodekategori"]),
                                    //KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    //KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUk = DataFormat.GetInteger(dr["btKodeUK"]),
                                    //KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodekategoriPelaksana"]),
                                    //KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    //KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    //KodeKegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                                    dtBAST = DataFormat.GetDateTime(dr["dtBAST"]),

                                    Status = DataFormat.GetSingle(dr["iStatus"]),
                                    NoBAST = DataFormat.GetString(dr["sNoBAST"]),
                                    PihakKetiga = DataFormat.GetInteger(dr["iPihakKetiga"]),
                                    NamaPihakKetiga = DataFormat.GetString(dr["PihakKetiga"]),
                                    //public Perusahaan oPerusahaan = DataFormat.GetInteger(dr["IIDRekeningBAST"]),
                                    PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                                    Uraian = DataFormat.GetString(dr["sUraian"]),
                                    NOKontrak = DataFormat.GetString(dr["sNoKontrak"]),
                                    NoUrutKontrak = DataFormat.GetLong(dr["iNoKontrak"]),
                                    NoSP2D = DataFormat.GetString(dr["sNoSP2D"]),
                                    TanggalSP2D = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    NoUrutSP2D = DataFormat.GetString(dr["NoUrutSPP"]),
                                    JumlahSP2D = DataFormat.GetDecimal(dr["JumlahSP2D"]).ToRupiahInReport(),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKEgiatan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    Rekening = GetDetail(DataFormat.GetLong(dr["iNourut"])),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    //      oKontrak = GetKontrak(DataFormat.GetLong(dr["iNoKontrak"]))


                                }).ToList();
                    }
                }
                return _lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }

        }
        public List<BAST> GetByIDDInasAndNoKonrak(int idDinas, long NoUrutKontrak)
        {


            List<BAST> _lst = new List<BAST>();
            try
            {
                SSQL = "SELECT tBAST.*, tSPP.sNoSP2D, tSPP.dtBukukas, tSPP.inourut as NoUrutSPP, tSPP.cJumlah as jumlahSP2D ,tKontrak.sNoKontrak, tKontrak.inourut as NoUrutKontrak, " +
                    " mPerusahaan.sNamaPerusahaan as PihakKetiga FROM " + m_sNamaTabel +
                    " LEFT OUTER JOIN tSPP ON tSPP.inoBAST = tBAST.inourut LEFT JOIN tKontrak On tBAST.iNoKontrak = tKontrak.inourut " +
                    " LEFT JOIN mPerusahaan On mPerusahaan.IDPerusahaan = tBAST.iPihakKetiga where tBAST.iTahun = @Tahun and tBAST.IDDInas = @DINAS  And tBAST.iNoKontrak=@NOURUTKONTRAK Order by inourut ";//


                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@Tahun", Tahun));
                paramCollection.Add(new DBParameter("@DINAS", idDinas));
                paramCollection.Add(new DBParameter("@NOURUTKONTRAK", NoUrutKontrak));


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new BAST()
                                {

                                    NoUrut = DataFormat.GetLong(dr["iNourut"]),
                                    Tahun = DataFormat.GetSingle(dr["iTahun"]),
                                    //Kodekategori = DataFormat.GetInteger(dr["btKodekategori"]),
                                    //KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    //KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUk = DataFormat.GetInteger(dr["btKodeUK"]),
                                    //KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodekategoriPelaksana"]),
                                    //KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    //KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    //KodeKegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                                    dtBAST = DataFormat.GetDateTime(dr["dtBAST"]),

                                    Status = DataFormat.GetSingle(dr["iStatus"]),
                                    NoBAST = DataFormat.GetString(dr["sNoBAST"]),
                                    PihakKetiga = DataFormat.GetInteger(dr["iPihakKetiga"]),
                                    NamaPihakKetiga = DataFormat.GetString(dr["PihakKetiga"]),
                                    //public Perusahaan oPerusahaan = DataFormat.GetInteger(dr["IIDRekeningBAST"]),
                                    PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                                    Uraian = DataFormat.GetString(dr["sUraian"]),
                                    NOKontrak = DataFormat.GetString(dr["sNoKontrak"]),
                                    NoUrutKontrak = DataFormat.GetLong(dr["iNoKontrak"]),
                                    NoSP2D = DataFormat.GetString(dr["sNoSP2D"]),
                                    TanggalSP2D = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    NoUrutSP2D = DataFormat.GetString(dr["NoUrutSPP"]),
                                    JumlahSP2D = DataFormat.GetDecimal(dr["JumlahSP2D"]).ToRupiahInReport(),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKEgiatan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    Rekening = GetDetail(DataFormat.GetLong(dr["iNourut"])),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    //      oKontrak = GetKontrak(DataFormat.GetLong(dr["iNoKontrak"]))


                                }).ToList();
                    }
                }
                return _lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }

        }
        public List<BAST> GetByIDDInasDanBatas(int idDinas, DateTime dAwal, DateTime dAkhir)
        {


            List<BAST> _lst = new List<BAST>();
            try
            {
                SSQL = "SELECT tBAST.*,tKontrak.sNoKontrak, tKontrak.inourut as NoUrutKontrak, " +
                    " mPerusahaan.sNamaPerusahaan as PihakKetiga FROM " + m_sNamaTabel +
                    " inner JOIN tKontrak On tBAST.iNoKontrak = tKontrak.inourut " +
                    " inner JOIN mPerusahaan On mPerusahaan.IDPerusahaan = tBAST.iPihakKetiga where tBAST.iTahun = @Tahun and tBAST.IDDInas = @DINAS  and " +
                    " dtBAST between @DAWAL and @DAKHIR Order by inourut ";//


                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@Tahun", Tahun));
                paramCollection.Add(new DBParameter("@DINAS", idDinas));
                paramCollection.Add(new DBParameter("@DAWAL", dAwal,DbType.Date));
paramCollection.Add(new DBParameter("@DAKHIR", dAkhir,DbType.Date));




                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new BAST()
                                {

                                    NoUrut = DataFormat.GetLong(dr["iNourut"]),
                                    Tahun = DataFormat.GetSingle(dr["iTahun"]),
                                    //Kodekategori = DataFormat.GetInteger(dr["btKodekategori"]),
                                    //KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    //KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUk = DataFormat.GetInteger(dr["btKodeUK"]),
                                    //KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodekategoriPelaksana"]),
                                    //KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    //KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    //KodeKegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                                    dtBAST = DataFormat.GetDateTime(dr["dtBAST"]),

                                    Status = DataFormat.GetSingle(dr["iStatus"]),
                                    NoBAST = DataFormat.GetString(dr["sNoBAST"]),
                                    PihakKetiga = DataFormat.GetInteger(dr["iPihakKetiga"]),
                                    NamaPihakKetiga = DataFormat.GetString(dr["PihakKetiga"]),
                                    //public Perusahaan oPerusahaan = DataFormat.GetInteger(dr["IIDRekeningBAST"]),
                                    PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                                    Uraian = DataFormat.GetString(dr["sUraian"]),
                                    NOKontrak = DataFormat.GetString(dr["sNoKontrak"]),
                                    NoUrutKontrak = DataFormat.GetLong(dr["iNoKontrak"]),
                                   // NoSP2D = DataFormat.GetString(dr["sNoSP2D"]),
                                    //TanggalSP2D = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    //NoUrutSP2D = DataFormat.GetString(dr["NoUrutSPP"]),
                                    //JumlahSP2D = DataFormat.GetDecimal(dr["JumlahSP2D"]).ToRupiahInReport(),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKEgiatan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                   IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                  //  Rekening = GetDetail(DataFormat.GetLong(dr["iNourut"])),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    //      oKontrak = GetKontrak(DataFormat.GetLong(dr["iNoKontrak"]))


                                }).ToList();
                    }
                }
                return _lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }

        }
        public List<BAST> GetByIDDInasForJurnal(int idDinas )
          {

              
              List<BAST> _lst = new List<BAST>();
              try
              {
                  SSQL = "SELECT tBAST.*, " +
                      "  dbo.IsInJurnal(tBAST.inourut) as InJurnal   FROM " + m_sNamaTabel + 
                      " where tBAST.iTahun = @Tahun and tBAST.IDDInas = @DINAS  Order by inourut ";//


               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@Tahun", Tahun));
               paramCollection.Add(new DBParameter("@DINAS", idDinas));


                  DataTable dt = new DataTable();
                  dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                  if (dt != null)
                  {
                      if (dt.Rows.Count > 0)
                      {
                          _lst = (from DataRow dr in dt.Rows
                                  select new BAST()
                                  {

                                      NoUrut = DataFormat.GetLong(dr["iNourut"]),
                                      Tahun = DataFormat.GetSingle(dr["iTahun"]),
                                      
                                      KodeUk = DataFormat.GetInteger(dr["btKodeUK"]),
                                      dtBAST = DataFormat.GetDateTime(dr["dtBAST"]),

                                      Status = DataFormat.GetSingle(dr["InJurnal"]),
                                      NoBAST = DataFormat.GetString(dr["sNoBAST"]),
                                      Uraian = DataFormat.GetString(dr["sUraian"]),
                                      IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                      IDKegiatan = DataFormat.GetInteger(dr["IDKEgiatan"]),
                                      IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                      IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                      

                                  }).ToList();
                      }
                  }
                  return _lst;
              }
              catch (Exception ex)
              {
                  _isError = true;
                  _lastError = ex.Message;
                  return _lst;
              }

          }

          private Kontrak GetKontrak(long noUrutKontrak)
          {
              KontrakLogic oLogic = new KontrakLogic(Tahun);

              Kontrak oKontrak = oLogic.Get(noUrutKontrak);
              
              return oKontrak;
          }
          public List<BAST> GetByKontrak(int idDinas , long iNoKontrak)
          {
              List<BAST> _lst = new List<BAST>();
              try
              {
                  DBParameterCollection paramCollection = new DBParameterCollection();
       


                  if (iNoKontrak > 0)
                  {
                      SSQL = "SELECT tBAST.*  FROM " + m_sNamaTabel +
                        " where IDDInas = @DINAS AND tBAST.iNoKontrak = @NOKONTRAK  Order by inourut ";//
                      paramCollection.Add(new DBParameter("@DINAS", idDinas));
                      paramCollection.Add(new DBParameter("@NOKONTRAK", iNoKontrak));
                  }
                  else
                  {
                      SSQL = "SELECT tBAST.*  FROM " + m_sNamaTabel +
                      " where IDDInas = @DINAS  Order by inourut ";//
                      paramCollection.Add(new DBParameter("@DINAS", idDinas));
                  }
                  DataTable dt = new DataTable();
                  dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                  if (dt != null)
                  {
                      if (dt.Rows.Count > 0)
                      {
                          _lst = (from DataRow dr in dt.Rows
                                  select new BAST()
                                  {

                                      NoUrut = DataFormat.GetLong(dr["iNourut"]),
                                      Tahun = DataFormat.GetSingle(dr["iTahun"]),
                                      Kodekategori = DataFormat.GetInteger(dr["btKodekategori"]),
                                      KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                      KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                      KodeUk = DataFormat.GetInteger(dr["btKodeUK"]),
                                      KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodekategoriPelaksana"]),
                                      KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                      KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                      KodeKegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                                      dtBAST = DataFormat.GetDateTime(dr["dtBAST"]),

                                      Status = DataFormat.GetSingle(dr["iStatus"]),
                                      NoBAST = DataFormat.GetString(dr["sNoBAST"]),
                                      PihakKetiga = DataFormat.GetInteger(dr["iPihakKetiga"]),
                                   //   NamaPihakKetiga = DataFormat.GetString(dr["PihakKetiga"]),
                                      //public Perusahaan oPerusahaan = DataFormat.GetInteger(dr["IIDRekeningBAST"]),
                                      PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                                      Uraian = DataFormat.GetString(dr["sUraian"]),
                                      NoUrutKontrak = DataFormat.GetLong(dr["iNoKontrak"]),
                                      IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                      IDKegiatan = DataFormat.GetInteger(dr["IDKEgiatan"]),
                                      IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                      IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                      IDSubKegiatan=DataFormat.GetLong(dr["IDSubKegiatan"]),
                                       oKontrak = GetKontrak(DataFormat.GetLong(dr["iNoKontrak"]))
                                  
                                  }).ToList();
                      }
                  }
                  return _lst;
              }
              catch (Exception ex)
              {
                  _isError = true;
                  _lastError = ex.Message;
                  return _lst;
              }

          }
          public BAST GetByID(long nourut)
          {
              BAST oBAST = new BAST();
              try
              {
                  SSQL = "SELECT tBAST.*, tSPP.sNoSP2D, tSPP.dtBukukas, tSPP.inourut as NoUrutSPP, tSPP.cJumlah as jumlahSP2D FROM " + m_sNamaTabel + " LEFT OUTER JOIN tSPP ON tSPP.inoBAST = tBAST.inourut WHERE tBAST.inourut = @NOURUT";
                   DBParameterCollection paramCollection = new DBParameterCollection();
                   paramCollection.Add(new DBParameter("@NOURUT", nourut));
                  //
                  DataTable dt = new DataTable();
                  dt = _dbHelper.ExecuteDataTable(SSQL,paramCollection);
                  if (dt != null)
                  {
                      if (dt.Rows.Count > 0)
                      {
                          DataRow dr = dt.Rows[0];

                          oBAST = new BAST()
                          {
                              NoUrut = DataFormat.GetLong(dr["iNourut"]),
                              Tahun = DataFormat.GetSingle(dr["iTahun"]),
                              dtBAST = DataFormat.GetDateTime(dr["dtBAST"]),

                              Status = DataFormat.GetSingle(dr["iStatus"]),
                              NoBAST = DataFormat.GetString(dr["sNoBAST"]),
                              PihakKetiga = DataFormat.GetInteger(dr["iPihakKetiga"]),
                              PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                              Uraian = DataFormat.GetString(dr["sUraian"]),
                              NOKontrak = DataFormat.GetString(dr["inkontrak"]),
                              NoSP2D = DataFormat.GetString(dr["sNoSP2D"]),
                              TanggalSP2D = DataFormat.GetDateTime(dr["dtSP2D"]),
                              NoUrutSP2D = DataFormat.GetString(dr["NoUrutSPP"]),
                              JumlahSP2D = DataFormat.GetDecimal(dr["JumlahSP2D"]).ToRupiahInReport(),
                            IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                            IDKegiatan = DataFormat.GetInteger(dr["IDKEgiatan"]),
                            IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                            IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                              
                              IDSubKegiatan=DataFormat.GetLong(dr["IDSubKegiatan"]),
                              oKontrak = GetKontrak(DataFormat.GetLong(dr["iNoKontrak"]))
                          };
                      }
                  }
                  return oBAST;
              }
              catch (Exception ex)
              {
                  _isError = true;
                  _lastError = ex.Message;
                  return oBAST;
              }

          }
          public bool Hapus(long NoUrut)
          {

              try
              {
                  SSQL = "DELETE  FROM TBASTRekening  WHERE  inourut = @NoUrut";
                   DBParameterCollection paramCollection = new DBParameterCollection();
                   paramCollection.Add(new DBParameter("@NoUrut", NoUrut));


                   _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                   SSQL = "DELETE  FROM TBAST  WHERE  inourut = @NoUrut";
                  


                   _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                  return true;

              }
              catch (Exception ex)
              {
                  _isError = true;
                  _lastError = ex.Message;
                  return false;
              }

          }

    }
}
