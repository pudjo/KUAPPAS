using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DTO.Akuntansi;
using BP;
using DataAccess;
using System.Data;

using Formatting;


namespace BP.Akuntansi
{
    public class RealisasiAKLogic:BP
    {

        public RealisasiAKLogic(int _pTahun) :
            base(_pTahun)
        {

        }
        public List<Realisasi04AK> GetRealisasi(int iddinas, DateTime tanggal, bool belanjaModel= false)
        {
            List<Realisasi04AK> _lst = new List<Realisasi04AK>();
            try
            {
               DBParameterCollection paramCollection = new DBParameterCollection();

               SSQL = "SELECT  Tabel, JenisSP2D, IIDrekening, REALISASI04AK.IdUrusan,cJumlah,debet,IdDInas,IdProgram,IdKegiatan," +
                   " IdSubKegiatan  FROM REALISASI04AK inner join mSKPD on mSKPD.ID= REALISASI04AK.IDDInas WHERE dtBukukas<= @TANGGAL"; 
               if (iddinas > 0)
               {
                   SSQL = SSQL + " AND  IDDInas =@DINAS  ";
                   paramCollection.Add(new DBParameter("@DINAS", iddinas));
               }
               if (belanjaModel == true)
               {
                   SSQL = SSQL + " AND  iidrekening like '52%'";
               }

               SSQL = SSQL + " UNION ALL SELECT 0 as Tabel, 0 as JenisSP2D, IIDrekening,0 as IdUrusan,cJumlah,1 as debet,IdDInas,0 as IdProgram," +
                                  "0 as IdKegiatan ,0 as IdSubKegiatan from RealisasiSTS WHERE dtBukukas<= @TANGGAL";
               if (iddinas > 0)
               {
                   SSQL = SSQL + " AND  IDDInas =@DINAS  ";
                 
               }
 
                paramCollection.Add(new DBParameter("@TANGGAL", tanggal,DbType.Date));


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Realisasi04AK()
                                {
                                    Tabel = DataFormat.GetInteger(dr["Tabel"]),
                                    JenisSP2D = DataFormat.GetInteger(dr["JenisSP2D"]),
                                    idRekening= DataFormat.GetLong(dr["IIDrekening"]),
                                    IdUrusan = DataFormat.GetInteger(dr["IdUrusan"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Debet  = DataFormat.GetSingle(dr["debet"]),
                                    IDDInas = DataFormat.GetInteger(dr["IdDInas"]),
                                    IdProgram = DataFormat.GetInteger(dr["IdProgram"]),
                                    IdKegiatan = DataFormat.GetInteger(dr["IdKegiatan"]),
                                    IdSubKegiatan =DataFormat.GetLong(dr["IdSubKegiatan"]),
                                    

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
        public List<RealisasiPerUrusan> GetRealisasiPerUrusan(DateTime tanggal)
        {
            List<RealisasiPerUrusan> _lst = new List<RealisasiPerUrusan>();
            try
            {
                
                SSQL = "select * from dbo.fnPerdaPerUrusan(" + Tahun.ToString() + "," + tanggal.ToSQLFormat() + " )" +
                      " where Level <=5 order by KOdekategori, KodeUrusan ,KodeSKPD, kodeProgram ";



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RealisasiPerUrusan()
                                {
                                     kelompok=  DataFormat.GetInteger(dr["Kelompok"]),
                                     level = DataFormat.GetInteger(dr["level"]),
                                     IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                     IDurusan = DataFormat.GetInteger(dr["IDurusan"]),

                                     IDProgram = DataFormat.GetInteger(dr["IDProgram"]),

                                     idkegiatan = DataFormat.GetInteger(dr["idkegiatan"]),
                                     idsubkegiatan = DataFormat.GetLong(dr["idsubkegiatan"]),
                                     iidrekening = DataFormat.GetLong(dr["iidrekening"]),
                                     kodeKategori = DataFormat.GetInteger(dr["kodeKategori"]),
                                     KodeUrusan = DataFormat.GetInteger(dr["KodeUrusan"]),
                                     Kodeskpd = DataFormat.GetInteger(dr["Kodeskpd"]),
                                     Kode = DataFormat.GetString(dr["Kode"]),
                                     kodeprogram = DataFormat.GetInteger(dr["kodeprogram"]),
                                     kodekegiatan = DataFormat.GetInteger(dr["kodekegiatan"]),
                                     kodesubkegiatan = DataFormat.GetInteger(dr["kodesubkegiatan"]),
                                     Rek = DataFormat.GetInteger(dr["Rek"]),
                                     Nama = DataFormat.GetString(dr["Nama"]),
                                     AnggaranOperasi = DataFormat.GetDecimal(dr["AnggaranOperasi"]),
                                     RealisasiOperasi = DataFormat.GetDecimal(dr["RealisasiOperasi"]),
                                     AnggaranModal = DataFormat.GetDecimal(dr["AnggaranModal"]),
                                     RealisasiModal = DataFormat.GetDecimal(dr["RealisasiModal"]),
                                     AnggaranTakTerduga = DataFormat.GetDecimal(dr["AnggaranTakTerduga"]),
                                     RealisasiTakTerduga = DataFormat.GetDecimal(dr["RealisasiTakTerduga"]),
                                     AnggaranTransfer = DataFormat.GetDecimal(dr["AnggaranTransfer"]),
                                     RealisasiTransfer = DataFormat.GetDecimal(dr["RealisasiTransfer"]),
                                     AnggaranOperasiPegawai = DataFormat.GetDecimal(dr["AnggaranOperasiPegawai"]),
                                     AnggaranOperasiBarangJasa = DataFormat.GetDecimal(dr["AnggaranOperasiBarangJasa"]),
                                     AnggaranOperasiHibah = DataFormat.GetDecimal(dr["AnggaranOperasiHibah"]),
                                     AnggaranOperasiBantuanSosial = DataFormat.GetDecimal(dr["AnggaranOperasiBantuanSosial"]),
                                     AnggaranModalTanah = DataFormat.GetDecimal(dr["AnggaranModalTanah"]),
                                     AnggaranModalPeralatanMesin = DataFormat.GetDecimal(dr["AnggaranModalPeralatanMesin"]),
                                     AnggaranModalBangunan = DataFormat.GetDecimal(dr["AnggaranModalBangunan"]),
                                     AnggaranModalJIJ = DataFormat.GetDecimal(dr["AnggaranModalJIJ"]),
                                     AnggaranModalAsetTetapLainnya = DataFormat.GetDecimal(dr["AnggaranModalAsetTetapLainnya"]),
                                     AnggaranBagiHasil = DataFormat.GetDecimal(dr["AnggaranBagiHasil"]),
                                     AnggaranBantuanKeuangan = DataFormat.GetDecimal(dr["AnggaranBantuanKeuangan"]),
                                     Anggaran = DataFormat.GetDecimal(dr["Anggaran"]),
                                     RealisasiOperasiPegawai = DataFormat.GetDecimal(dr["RealisasiOperasiPegawai"]),
                                     RealisasiOperasiBarangJasa = DataFormat.GetDecimal(dr["RealisasiOperasiBarangJasa"]),
                                     RealisasiOperasiHibah = DataFormat.GetDecimal(dr["RealisasiOperasiHibah"]),
                                     RealisasiOperasiBantuanSosial = DataFormat.GetDecimal(dr["RealisasiOperasiBantuanSosial"]),
                                     RealisasiModalTanah = DataFormat.GetDecimal(dr["RealisasiModalTanah"]),
                                     RealisasiModalPeralatanMesin = DataFormat.GetDecimal(dr["RealisasiModalPeralatanMesin"]),
                                     RealisasiModalBangunan = DataFormat.GetDecimal(dr["RealisasiModalBangunan"]),
                                     RealisasiModalJIJ = DataFormat.GetDecimal(dr["RealisasiModalJIJ"]),
                                     RealisasiModalAsetTetapLainnya = DataFormat.GetDecimal(dr["RealisasiModalAsetTetapLainnya"]),
                                     RealisasiBagiHasil = DataFormat.GetDecimal(dr["RealisasiBagiHasil"]),
                                     RealisasiBantuanKeuangan = DataFormat.GetDecimal(dr["RealisasiBantuanKeuangan"]),
                                     Realisasi = DataFormat.GetDecimal(dr["="]), 
            Keluaran =  DataFormat.GetString(dr["Keluaran"]), 


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

        public List<Realisasi04AK> GetRealisasiBukubesar(int iddinas, DateTime tanggal, int jenis=2)
        {
            List<Realisasi04AK> _lst = new List<Realisasi04AK>();
            try
            {
         
                DBParameterCollection paramCollection = new DBParameterCollection();

                SSQL = "SELECT tBukubesar.*,  mRekening.iDebet  as SaldoNormal FROM tBukubesar inner join mRekening On tBukubesar.IIDrekening = mRekening.IIDRekening   WHERE dtTransaksi <= @TANGGAL ";//

                if (iddinas > 0)
                {
                    SSQL = SSQL + " AND  IDDInas =@DINAS  ";
                    paramCollection.Add(new DBParameter("@DINAS", iddinas));
                }
      
        
                paramCollection.Add(new DBParameter("@TANGGAL", tanggal,DbType.Date));
                switch(jenis){
                    case 1:
                        SSQL = SSQL + " and tBukubesar.iidrekening<400000000000 ";
                        break;
                    case 2:
                        SSQL = SSQL + " and tBukubesar.iidrekening >=400000000000  and tBukubesar.iidrekening <700000000000 ";
                        break;
                    case 3:
                        SSQL = SSQL + " and tBukubesar.iidrekening >=700000000000 ";
                        break;

                }
                
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Realisasi04AK()
                                {

                                    idRekening = DataFormat.GetLong(dr["IIDrekening"]),
                                    IdUrusan = DataFormat.GetInteger(dr["IdUrusan"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]) * DataFormat.GetInteger(dr["SaldoNormal"]),
                                    Debet = DataFormat.GetSingle(dr["idebet"]),
                                    IDDInas = DataFormat.GetInteger(dr["IdDInas"]),
                                    IdProgram = DataFormat.GetInteger(dr["IdProgram"]),
                                    IdKegiatan = DataFormat.GetInteger(dr["IdKegiatan"]),
                                    IdSubKegiatan = DataFormat.GetLong(dr["IdKegiatan"]),
                                    Level=6,


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

        public List<Realisasi04AK> GetNeraca(int iddinas, DateTime tanggal, int ppkd, bool eliminasRK)
        {
            List<Realisasi04AK> _lst = new List<Realisasi04AK>();
            try
            {
                string NamaView = "";
                if (eliminasRK== true)
                {
                    NamaView = "vwBukubesarAllEliminasiRK";
                }
                else
                {
                    NamaView = "vwBukubesarAll";
                }

                    DBParameterCollection paramCollection = new DBParameterCollection();

                SSQL = "";
                if (ppkd == -1)
                {
                    SSQL = "SELECT " + NamaView + ".iidrekening , sum(iDebet * cJumlah) as Jumlah FROM " + NamaView + " WHERE dtTransaksi <=" + tanggal.ToSQLFormat() + " " +
                     "  AND " + NamaView + ".IIDREKENING>100000000000 AND " + NamaView + ".IIDREKENING <> 310205010001  and ((iddinas= 5020200 and bPPKD=0) or bPPKD=1) ";

                    //SSQL = SSQL + " AND  IDDInas =@DINAS  ";
                    //paramCollection.Add(new DBParameter("@DINAS", iddinas));
                    


                    SSQL = SSQL + " and " + NamaView + ".iidrekening<400000000000 ";
                } else { 

                    if (iddinas > 0)
                   {
                    SSQL = "SELECT " + NamaView + ".iidrekening , sum(iDebet * cJumlah) as Jumlah FROM " + NamaView + " WHERE dtTransaksi <=" + tanggal.ToSQLFormat() + " " +
                     "  AND " + NamaView + ".IIDREKENING>100000000000 AND " + NamaView + ".IIDREKENING <> 310205010001  and bPPKD=@PPKD ";

                    SSQL = SSQL + " AND  IDDInas =@DINAS  ";
                    paramCollection.Add(new DBParameter("@DINAS", iddinas));
                    paramCollection.Add(new DBParameter("@PPKD", ppkd));
                    
                    SSQL = SSQL + " and " + NamaView + ".iidrekening<400000000000 ";
                }
                else
                {
                    SSQL = "SELECT " + NamaView + ".iidrekening , sum(iDebet * cJumlah) as Jumlah FROM " + NamaView + " WHERE dtTransaksi <= " + tanggal.ToSQLFormat() + " " +//
                    "  AND " + NamaView + ".IIDREKENING>100000000000 AND " + NamaView + ".IIDREKENING <> 310205010001  ";

                    

                    paramCollection.Add(new DBParameter("@PPKD", ppkd));

                    SSQL = SSQL + " and " + NamaView + ".iidrekening<400000000000 ";
                }

                }
       

                  SSQL = SSQL + " group by IIDRekening ORDER BY IIDRekening ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Realisasi04AK()
                                {

                                    idRekening = DataFormat.GetLong(dr["IIDrekening"]),
                                    IdUrusan =0,// DataFormat.GetInteger(dr["IdUrusan"]),
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"]) ,//* DataFormat.GetInteger(dr["iDebet"]),
                                    //Debet = DataFormat.GetSingle(dr["idebet"]),
                                    //IDDInas = DataFormat.GetInteger(dr["IdDInas"]),
                                    //IdProgram = DataFormat.GetInteger(dr["IdProgram"]),
                                    //IdKegiatan = DataFormat.GetInteger(dr["IdKegiatan"]),
                                    //IdSubKegiatan = DataFormat.GetLong(dr["IdKegiatan"]),
                                    Level = 6,


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
        public List<Realisasi04AK> GetSaldoWAwalBukubesar(int iddinas,  int jenis = 2)
        {
            List<Realisasi04AK> _lst = new List<Realisasi04AK>();
            try
            {

                DBParameterCollection paramCollection = new DBParameterCollection();

                SSQL = "SELECT tSaldoAwalRek.*, mRekening.iDebet as saldonormal FROM tSaldoAwalRek inner join " +
                    " mRekening On tSaldoAwalRek.IIDrekening = mRekening.IIDRekening   WHERE 1>0";//

                if (iddinas > 0)
                {
                    SSQL = SSQL + " AND  tSaldoAwalRek.IDDInas =@DINAS  ";
                    paramCollection.Add(new DBParameter("@DINAS", iddinas));
                }


                switch (jenis)
                {
                    case 1:
                        SSQL = SSQL + " and tSaldoAwalRek.iidrekening<400000000000 ";
                        break;
                    case 2:
                        SSQL = SSQL + " and tSaldoAwalRek.iidrekening >=400000000000  and tBukubesar.iidrekening <700000000000 ";
                        break;
                    case 3:
                        SSQL = SSQL + " and tSaldoAwalRek.iidrekening >=700000000000 ";
                        break;

                }
                SSQL = SSQL + " ORDER BY IIDRekening";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Realisasi04AK()
                                {

                                    idRekening = DataFormat.GetLong(dr["IIDrekening"]),
                                    KpdeRekening = DataFormat.GetString(dr["IIDrekening"]),
                                    //IdUrusan = DataFormat.GetInteger(dr["IdUrusan"]),
                                    Jumlah = jenis==1? DataFormat.GetDecimal(dr["cJumlah"]) * DataFormat.GetInteger(dr["iDebet"]):
                                            DataFormat.GetDecimal(dr["cJumlah"]) * DataFormat.GetInteger(dr["iDebet"]),
                                    //Debet = DataFormat.GetSingle(dr["idebet"]),
                                    //IDDInas = DataFormat.GetInteger(dr["IdDInas"]),
                                    //IdProgram = DataFormat.GetInteger(dr["IdProgram"]),
                                    //IdKegiatan = DataFormat.GetInteger(dr["IdKegiatan"]),
                                    //IdSubKegiatan = DataFormat.GetLong(dr["IdKegiatan"]),
                                    Level = 6,


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
        public List<Realisasi04AK> GetSaldoWAwalNeraca(int iddinas)
        {
            List<Realisasi04AK> _lst = new List<Realisasi04AK>();
            try
            {

                DBParameterCollection paramCollection = new DBParameterCollection();

                SSQL = "SELECT tSaldoAwalRek.IIDRekening, tSaldoAwalRek.iDebet, sum(tSaldoAwalRek.cjumlah) as Jumlah  " +
                    " FROM tSaldoAwalRek WHERE 1>0 and tSaldoAwalRek.iidrekening<400000000000 ";//

                if (iddinas > 0)
                {
                    SSQL = SSQL + " AND  tSaldoAwalRek.IDDInas =@DINAS  ";
                    paramCollection.Add(new DBParameter("@DINAS", iddinas));
                }
                SSQL = SSQL + " group by tSaldoAwalRek.IIDRekening, tSaldoAwalRek.iDebet  ORDER BY IIDRekening";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Realisasi04AK()
                                {

                                    idRekening = DataFormat.GetLong(dr["IIDrekening"]),
                                    KpdeRekening = DataFormat.GetString(dr["IIDrekening"]),
                                    //IdUrusan = DataFormat.GetInteger(dr["IdUrusan"]),
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"]) * DataFormat.GetInteger(dr["iDebet"]) ,
                                   
                                    //Debet = DataFormat.GetSingle(dr["idebet"]),
                                    //IDDInas = DataFormat.GetInteger(dr["IdDInas"]),
                                    //IdProgram = DataFormat.GetInteger(dr["IdProgram"]),
                                    //IdKegiatan = DataFormat.GetInteger(dr["IdKegiatan"]),
                                    //IdSubKegiatan = DataFormat.GetLong(dr["IdKegiatan"]),
                                    Level = 6,


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
        public List<Realisasi04AK> GetLOBukubesar(int iddinas, DateTime tanggal)
        {
            List<Realisasi04AK> _lst = new List<Realisasi04AK>();
            try
            {

                DBParameterCollection paramCollection = new DBParameterCollection();

                
                SSQL = "SELECT tBukubesar.*, mRekening.iDebet  as  iDebet FROM tBukubesar INNER JOIN mRekening on mRekening.iidrekening = tBukuBesar.IIDRekening WHERE tBukubesar.dtTransaksi<= @TANGGAL ";//

                if (iddinas > 0)
                {
                    SSQL = SSQL + " AND  IDDInas =@DINAS  ";
                    paramCollection.Add(new DBParameter("@DINAS", iddinas));
                }


                paramCollection.Add(new DBParameter("@TANGGAL", tanggal));


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Realisasi04AK()
                                {

                                    idRekening = DataFormat.GetLong(dr["IIDrekening"]),
                                    IdUrusan = DataFormat.GetInteger(dr["IdUrusan"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Debet = DataFormat.GetSingle(dr["idebet"]),
                                    IDDInas = DataFormat.GetInteger(dr["IdDInas"]),
                                    IdProgram = DataFormat.GetInteger(dr["IdProgram"]),
                                    IdKegiatan = DataFormat.GetInteger(dr["IdKegiatan"]),
                                    IdSubKegiatan = DataFormat.GetLong(dr["IdKegiatan"]),


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
        public List<Realisasi04AK> GetLO(int iddinas, DateTime tanggal)
        {
            List<Realisasi04AK> _lst = new List<Realisasi04AK>();
            try
            {

                DBParameterCollection paramCollection = new DBParameterCollection();


                SSQL = "SELECT tBukubesar.iidRekening , sum(tBukubesar.cJumlah * tBukubesar.iDebet) as Jumlah FROM tBukubesar INNER JOIN mRekening on mRekening.iidrekening = tBukuBesar.IIDRekening WHERE tBukubesar.dtTransaksi<=" + tanggal.ToSQLFormat() ;//
                SSQL = SSQL + " AND tBukubesar.iidRekening >700000000000";
                if (iddinas > 0)
                {
                    SSQL = SSQL + " AND  IDDInas =@DINAS  ";
                    paramCollection.Add(new DBParameter("@DINAS", iddinas));
                }


                SSQL = SSQL + " GROUP BY tBukubesar.iidRekening  order by tBukubesar.iidRekening ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Realisasi04AK()
                                {

                                    idRekening = DataFormat.GetLong(dr["IIDrekening"]),
                          
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"]),
                              
                                    Level=6,

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
