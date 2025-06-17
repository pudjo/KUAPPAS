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
using System.Data;

using Formatting;

namespace BP.Bendahara
{
    public class KontrakLogic:BP 
    {
        public KontrakLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tKontrak";
   
        }
        private void PerbaikIDDInas()
        {

         
             
            

        }
        public long  Simpan(Kontrak dh)
        {
            try
            {

                long _noUrut=0;
                if (dh.NoUrut == 0)
                {

                    _noUrut = ReadNo(E_KOLOM_NOURUT.CON_URUT_Kontrak, dh.IDDInas);


                    SSQL = "INSERT INTO tKontrak (iTahun , IDDInas, IDUrusan, IDProgram, IDKegiatan,IDSubKegiatan,inourut , btKodekategori , btKodeUrusan , " +
                        " btKodeSKPD , btKodeUK , btKodekategoriPelaksana , btKodeUrusanPelaksana , " +
                        " btIDProgram , btIDkegiatan ,btIDSUbKegiatan, sNoKontrak ,  dtKontrak , iPihakKetiga ,iStatus, sUraian,bPPKD ,swaktuPelaksanaan, dAwal, dAkhir  ) values (" +
                        " @Tahun , @IDDInas, @IDUrusan, @IDProgram,  @IDKegiatan,@IDSubKegiatan,@nourut , @Kodekategori , @KodeUrusan , " +
                        " @KodeSKPD , @KodeUK , @KodekategoriPelaksana , @KodeUrusanPelaksana , " +
                        " @KodeProgram , @Kodekegiatan ,@KodeSUbKegiatan, @NoKontrak ,  @dtKontrak , @PihakKetiga ,@Status, @Uraian,@PPKD ,@waktuPelaksanaan, @dAwal, @dAkhir )";


               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@Tahun", dh.Tahun));
                 paramCollection.Add(new DBParameter("@IDDInas",dh.IDDInas)); 
                    paramCollection.Add(new DBParameter("@IDUrusan",dh.IDUrusan)); 
                    paramCollection.Add(new DBParameter("@IDProgram",dh.IDProgram)); 
                    paramCollection.Add(new DBParameter("@IDKegiatan",dh.IDKegiatan));
                    paramCollection.Add(new DBParameter("@IDSubKegiatan", dh.IDSubKegiatan));
                    paramCollection.Add(new DBParameter("@nourut",_noUrut));
                    paramCollection.Add(new DBParameter("@Kodekategori",dh.Kodekategori));
                    paramCollection.Add(new DBParameter("@KodeUrusan",dh.KodeUrusan));
                        paramCollection.Add(new DBParameter("@KodeSKPD",dh.KodeSKPD));
                    paramCollection.Add(new DBParameter("@KodeUK",dh.KodeUk));

                    paramCollection.Add(new DBParameter("@KodekategoriPelaksana",dh.KodekategoriPelaksana)); 
                    paramCollection.Add(new DBParameter("@KodeUrusanPelaksana",dh.KodeUrusanPelaksana));
                    
                    paramCollection.Add(new DBParameter("@KodeProgram",dh.KodeProgram));
                    paramCollection.Add(new DBParameter("@Kodekegiatan",dh.KodeKegiatan));
                    paramCollection.Add(new DBParameter("@KodeSUbKegiatan",dh.KodeSubKegiatan));


                    paramCollection.Add(new DBParameter("@NoKontrak",dh.NoKontrak)); 
                    paramCollection.Add(new DBParameter("@dtKontrak",dh.DtKontrak,DbType.Date));
                    paramCollection.Add(new DBParameter("@PihakKetiga",dh.PihakKetiga));
                    paramCollection.Add(new DBParameter("@Status",0));
                    paramCollection.Add(new DBParameter("@Uraian",dh.Uraian));
                    paramCollection.Add(new DBParameter("@PPKD",0));
                    paramCollection.Add(new DBParameter("@waktuPelaksanaan",dh.WaktuPelaksanaan)); 
                    paramCollection.Add(new DBParameter("@dAwal", dh.dAwal,DbType.Date));
                    paramCollection.Add(new DBParameter("@dAkhir", dh.dAkhir, DbType.Date));
 
                    //paramCollection.Add(new DBParameter("@Awal",dh.dAwal));

                    //paramCollection.Add(new DBParameter("@Akhir", dh.dAkhir));



                    _dbHelper.ExecuteNonQuery(SSQL,paramCollection);
                    dh.NoUrut = _noUrut;
                   
                }
                else
                {
                    _noUrut = dh.NoUrut;
                    SSQL = "UPDATE tKontrak SET itahun =@Tahun , btKodekategori=@Kodekategori , btKodeUrusan =@KodeUrusan, " +
                        " btKodeSKPD =@KodeSKPD, btKodeUK =@KodeUK, btKodekategoriPelaksana =@KodekategoriPelaksana " +
                        ", btKodeUrusanPelaksana=@KodeUrusanPelaksana , " +
                        " btIDProgram =@KodeProgram, btIDkegiatan =@Kodekegiatan,btIDSUbKegiatan=@KodeSUbKegiatan," +
                        " sNoKontrak =@NoKontrak,  dtKontrak =@dtKontrak, iPihakKetiga =@PihakKetiga" +
                        ",iStatus=@Status, sUraian=@Uraian,bPPKD =0" +
                        ",swaktuPelaksanaan =@waktuPelaksanaan, IDDInas=@IDDInas, IDUrusan=@IDUrusan, IDProgram=@IDProgram," +
                        " IDKegiatan=@IDKegiatan,IDSUbKegiatan=@IDSubKegiatan " +
                        " WHERE inourut =@nourut ";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@Tahun", dh.Tahun));
                    
                    paramCollection.Add(new DBParameter("@Kodekategori", dh.Kodekategori));
                    paramCollection.Add(new DBParameter("@KodeUrusan", dh.KodeUrusan));
                    paramCollection.Add(new DBParameter("@KodeSKPD", dh.KodeSKPD));
                    paramCollection.Add(new DBParameter("@KodeUK", dh.KodeUk));

                    paramCollection.Add(new DBParameter("@KodekategoriPelaksana", dh.KodekategoriPelaksana));
                    paramCollection.Add(new DBParameter("@KodeUrusanPelaksana", dh.KodeUrusanPelaksana));

                    paramCollection.Add(new DBParameter("@KodeProgram", dh.KodeProgram));
                    paramCollection.Add(new DBParameter("@Kodekegiatan", dh.KodeKegiatan));
                    paramCollection.Add(new DBParameter("@KodeSUbKegiatan", dh.KodeSubKegiatan));


                    paramCollection.Add(new DBParameter("@NoKontrak", dh.NoKontrak));
                    paramCollection.Add(new DBParameter("@dtKontrak", dh.DtKontrak,DbType.Date));
                    paramCollection.Add(new DBParameter("@PihakKetiga", dh.PihakKetiga));
                    paramCollection.Add(new DBParameter("@Status", dh.Status));
                    paramCollection.Add(new DBParameter("@Uraian", dh.Uraian));
                    paramCollection.Add(new DBParameter("@PPKD", 0));
                    paramCollection.Add(new DBParameter("@waktuPelaksanaan", dh.WaktuPelaksanaan));
                    paramCollection.Add(new DBParameter("@IDDInas", dh.IDDInas));
                    paramCollection.Add(new DBParameter("@IDUrusan", dh.IDUrusan));
                    paramCollection.Add(new DBParameter("@IDProgram", dh.IDProgram));
                    paramCollection.Add(new DBParameter("@IDKegiatan", dh.IDKegiatan));
                    paramCollection.Add(new DBParameter("@IDSubKegiatan", dh.IDSubKegiatan));
                    paramCollection.Add(new DBParameter("@nourut", _noUrut));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    
                    SSQL = "DELETE tKontrakRekening WHERE iNoUrut =" + dh.NoUrut.ToString();
                    _dbHelper.ExecuteNonQuery(SSQL);


                }
                
                foreach (KontrakRekening kr in dh.Rekening)
                {
                    SSQL = "INSERT INTO tKontrakRekening (iNoUrut,IIDRekening,cJumlah) values (" +
                           "@NOURUT,@IDREKENING,@JUMLAH)";
                    DBParameterCollection paramCollectionDetail = new DBParameterCollection();
                    paramCollectionDetail.Add(new DBParameter("@NOURUT", _noUrut));

                    paramCollectionDetail.Add(new DBParameter("@IDREKENING",kr.IDRekening));
                    paramCollectionDetail.Add(new DBParameter("@JUMLAH", kr.Jumlah,DbType.Decimal));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollectionDetail);


                }
                return _noUrut;


            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;

                return 0;
            }
            

        }
        
        public List<Kontrak> GetByIDDinas(int iddinas, DateTime dbatas,DateTime tanggalAkhir)
        {
            List<Kontrak> _lst = new List<Kontrak>();
            try
            {



                SSQL = "SELECT tKontrak.*, mPerusahaan.sNamaPerusahaan as NamaPerusahaan  " +
                    " FROM tKontrak left join mPerusahaan on mPerusahaan.IDPerusahaan = tKontrak.iPihakKetiga where tKontrak.IDDInas =@DINAS ";
                //" AND cast( tKontrak.dtKontrak as DATE)  >=@TANGGALAWAL and cast( tKontrak.dtKontrak as DATE)  <=@TANGGALAKHIR";


               DBParameterCollection paramCollection = new DBParameterCollection();

               paramCollection.Add(new DBParameter("@DINAS", iddinas));
               paramCollection.Add(new DBParameter("@TANGGALAWAL", dbatas, DbType.Date));
               paramCollection.Add(new DBParameter("@TANGGALAKHIR", tanggalAkhir, DbType.Date));



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Kontrak()
                                {

                                    NoUrut = DataFormat.GetLong(dr["iNourut"]),
                                    Tahun = DataFormat.GetSingle(dr["iTahun"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSUBKegiatan"]),
                                    IDLokasi = DataFormat.GetInteger(dr["IDLokasi"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDinas"]),

                                    Kodekategori = DataFormat.GetInteger(dr["btKodekategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUk = DataFormat.GetInteger(dr["btKodeUK"]),
                                    KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodekategoriPelaksana"]),
                                    KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                                    DtKontrak = DataFormat.GetDateTime(dr["dtKontrak"]),
                                    Status = DataFormat.GetSingle(dr["iStatus"]),
                                    NoKontrak = DataFormat.GetString(dr["sNoKontrak"]),
                                    PihakKetiga = DataFormat.GetInteger(dr["iPihakKetiga"]),
                                    NamaPerusahaan = DataFormat.GetString(dr["NamaPerusahaan"]),
                                    //public Perusahaan oPerusahaan = DataFormat.GetInteger(dr["IIDRekeningKontrak"]),
                                    PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                                    Uraian = DataFormat.GetString(dr["sUraian"]),
                                    WaktuPelaksanaan = DataFormat.GetString(dr["swaktuPelaksanaan"]),
                                    dAwal = DataFormat.GetDateTime(dr["dAwal"]),
                                    dAkhir = DataFormat.GetDateTime(dr["dAkhir"]),
                                 //   Rekening = GetDetail(DataFormat.GetLong(dr["iNourut"]))
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
        public List<Kontrak> GetByIDDinas(int iddinas)
        {
            List<Kontrak> _lst = new List<Kontrak>();
            try
            {



                SSQL = "SELECT tKontrak.*, mPerusahaan.sNamaPerusahaan as NamaPerusahaan,  " +
                     " (Select SUM(tKontrakRekening.cJumlah) from tKontrakRekening where inourut =  tKontrak.inourut  ) as Jumlah " + 
                     " FROM tKontrak left join mPerusahaan on mPerusahaan.IDPerusahaan = tKontrak.iPihakKetiga where tKontrak.IDDInas =@DINAS ";


                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("@DINAS", iddinas));
               



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Kontrak()
                                {

                                    NoUrut = DataFormat.GetLong(dr["iNourut"]),
                                    Tahun = DataFormat.GetSingle(dr["iTahun"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSUBKegiatan"]),
                                    IDLokasi = DataFormat.GetInteger(dr["IDLokasi"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDinas"]),

                                    Kodekategori = DataFormat.GetInteger(dr["btKodekategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUk = DataFormat.GetInteger(dr["btKodeUK"]),
                                    KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodekategoriPelaksana"]),
                                    KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                                    DtKontrak = DataFormat.GetDateTime(dr["dtKontrak"]),
                                    Status = DataFormat.GetSingle(dr["iStatus"]),
                                    NoKontrak = DataFormat.GetString(dr["sNoKontrak"]),
                                    PihakKetiga = DataFormat.GetInteger(dr["iPihakKetiga"]),
                                    NamaPerusahaan = DataFormat.GetString(dr["NamaPerusahaan"]),
                                    //public Perusahaan oPerusahaan = DataFormat.GetInteger(dr["IIDRekeningKontrak"]),
                                    PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                                    Uraian = DataFormat.GetString(dr["sUraian"]),
                                    WaktuPelaksanaan = DataFormat.GetString(dr["swaktuPelaksanaan"]),
                                    dAwal = DataFormat.GetDateTime(dr["dAwal"]),
                                    dAkhir = DataFormat.GetDateTime(dr["dAkhir"]),
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"]),
                                    Rekening = GetDetail(DataFormat.GetLong(dr["iNourut"]))
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
        public List<Kontrak> GetByIDDinasDanBatas(int iddinas , DateTime dBatas)
        {
            List<Kontrak> _lst = new List<Kontrak>();
            try
            {



                SSQL = "SELECT tKontrak.*, mPerusahaan.sNamaPerusahaan as NamaPerusahaan,  " +
                     " (Select SUM(tKontrakRekening.cJumlah) from tKontrakRekening where inourut =  tKontrak.inourut  ) as Jumlah " +
                     " FROM tKontrak left join mPerusahaan on mPerusahaan.IDPerusahaan = tKontrak.iPihakKetiga where tKontrak.IDDInas =@DINAS " +
                     " and dtkontrak<=@DBATAS";


                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("@DINAS", iddinas));

                paramCollection.Add(new DBParameter("@DBATAS", dBatas, DbType.Date));


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Kontrak()
                                {

                                    NoUrut = DataFormat.GetLong(dr["iNourut"]),
                                    Tahun = DataFormat.GetSingle(dr["iTahun"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSUBKegiatan"]),
                                    IDLokasi = DataFormat.GetInteger(dr["IDLokasi"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDinas"]),

                                    Kodekategori = DataFormat.GetInteger(dr["btKodekategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUk = DataFormat.GetInteger(dr["btKodeUK"]),
                                    KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodekategoriPelaksana"]),
                                    KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                                    DtKontrak = DataFormat.GetDateTime(dr["dtKontrak"]),
                                    Status = DataFormat.GetSingle(dr["iStatus"]),
                                    NoKontrak = DataFormat.GetString(dr["sNoKontrak"]),
                                    PihakKetiga = DataFormat.GetInteger(dr["iPihakKetiga"]),
                                    NamaPerusahaan = DataFormat.GetString(dr["NamaPerusahaan"]),
                                    //public Perusahaan oPerusahaan = DataFormat.GetInteger(dr["IIDRekeningKontrak"]),
                                    PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                                    Uraian = DataFormat.GetString(dr["sUraian"]),
                                    WaktuPelaksanaan = DataFormat.GetString(dr["swaktuPelaksanaan"]),
                                    dAwal = DataFormat.GetDateTime(dr["dAwal"]),
                                    dAkhir = DataFormat.GetDateTime(dr["dAkhir"]),
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"]),
     //                               Rekening = GetDetail(DataFormat.GetLong(dr["iNourut"]))
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
     
       
        
        public Kontrak Get(long  inoUrut)
        {
            Kontrak oKontrak= new Kontrak();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE iNourut = " + inoUrut.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        oKontrak= new Kontrak()
                        {
                            NoUrut = DataFormat.GetLong(dr["iNourut"]),
                            Tahun = DataFormat.GetSingle(dr["iTahun"]),
                            ////Kodekategori = DataFormat.GetInteger(dr["btKodekategori"]),
                            ////KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                            ////KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                            KodeUk = DataFormat.GetInteger(dr["btKodeUK"]),
                            //KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodekategoriPelaksana"]),
                            //KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                            //KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                            //KodeKegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                            DtKontrak = DataFormat.GetDateTime(dr["dtKontrak"]),
                            Status = DataFormat.GetSingle(dr["iStatus"]),
                            NoKontrak = DataFormat.GetString(dr["sNoKontrak"]),
                            PihakKetiga = DataFormat.GetInteger(dr["iPihakKetiga"]),
                            //public Perusahaan oPerusahaan = DataFormat.GetInteger(dr["IIDRekeningKontrak"]),
                            PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                            Uraian = DataFormat.GetString(dr["sUraian"]),
                            WaktuPelaksanaan = DataFormat.GetString(dr["swaktuPelaksanaan"]),
                            dAwal = DataFormat.GetDateTime(dr["dAwal"]),
                            dAkhir = DataFormat.GetDateTime(dr["dAkhir"]),
                            Rekening = GetDetail(DataFormat.GetLong(dr["iNourut"])),
                            IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                            IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                            IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                            IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                            IDLokasi = DataFormat.GetInteger(dr["IDLokasi"]),
                            IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
                               
                        };
                    }
                }
                return oKontrak;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return oKontrak;
            }

        }
       
        public bool Hapus(long NoUrut)
        {
            
            try
            {
                SSQL = "DELETE  FROM tKontrakRekening WHERE INOUrut  = " + NoUrut.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);

                
                SSQL = "DELETE  FROM " + m_sNamaTabel + " WHERE INOUrut  = " + NoUrut.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                return true;
                
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }

        }
        private List<KontrakRekening> GetDetail(long inoUrut)
        {
            KontrakRekening oKontrak = new KontrakRekening();
            List<KontrakRekening> _lst = new List<KontrakRekening>();
            try
            {
                SSQL = "SELECT tKontrakRekening.* , mRekening.sNamaRekening as Nama FROM TKONTRAKRekening INNER JOIN mRekening on tKontrakRekening.iidRekening = mRekening.IIDRekening WHERE tKontrakRekening.iNourut = " + inoUrut.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new KontrakRekening()
                                {  
                                   NoUrut = DataFormat.GetString(dr["iNourut"]),
                                   IDRekening= DataFormat.GetLong(dr["IIDRekening"]),
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
    }
}
