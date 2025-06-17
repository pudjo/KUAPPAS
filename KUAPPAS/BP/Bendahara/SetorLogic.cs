using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using DataAccess;
using DTO.Bendahara;
using Formatting;
using BP;


namespace BP.Bendahara
{
    public class SetorLogic:BP
    {

        IDbConnection m_connection;
        IDbTransaction m_objTrans;
         public SetorLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tSetor";
            //    PerbaikiTable();

        }
         private void PerbaikiTable()
         {
             

             SSQL = "IF NOT EXISTS (SELECT * FROM syscolumns WHERE ID=OBJECT_ID('[dbo].[TSETOR]') AND NAME='IDDInas') " +
                     " ALTER TABLE [dbo].[TSETOR] ADD [IDDInas] int, [IDUrusan] int ,[IDProgram] int, [IDKegiatan] bigint";
             _dbHelper.ExecuteNonQuery(SSQL);

             SSQL = "Update tSetor  SET IDDInas =(btKodeKategori * 1000000 + btKodeUrusan * 10000 + btKodeSKPD * 100 ), " +
                     "IDProgram = (btKodeKategoriPelaksana * 10000 + btKodeUrusanPelaksana * 100 + btIDProgram )  ," +
                     " IDUrusan =(btKodeKategoriPelaksana * 100 + btKodeUrusanPelaksana )  , " +
                     " IDKegiatan =btKodeKategoriPelaksana * 10000000 + btKodeUrusanPelaksana * 100000 + btIDProgram * 1000 +  btIDKegiatan " +
                     " WHERE IDDINAS is NULL";
             _dbHelper.ExecuteNonQuery(SSQL);
             
         }
         public Setor GetByID(long _inourut)
         {
             Setor oSetor = new Setor();
             try
             {
                 SSQL = "SELECT tSetor.* FROM " + m_sNamaTabel + " WHERE tSetor.inourut = " + _inourut.ToString();

                 DataTable dt = new DataTable();
                 dt = _dbHelper.ExecuteDataTable(SSQL);
                 if (dt != null)
                 {
                     if (dt.Rows.Count > 0)
                     {
                         DataRow dr = dt.Rows[0];

                         oSetor = new Setor()
                         {
                             Tahun = DataFormat.GetInteger(dr["iTahun"]),
                             KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                             KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                             KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),


                             KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                             IDDinas = DataFormat.GetInteger(dr["IDDinas"]),
                             KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),//), DataFormat.GetInteger(dr["btKodeKategori, DataFormat.GetInteger(dr["btKodeKategoriPelaksana)
                             KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                             NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                             dtBukuKas = DataFormat.GetDateTime(dr["dtBukukas"]),
                             Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                             Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                             Jenis = DataFormat.GetInteger(dr["btJenis"]),
                             Status = DataFormat.GetInteger(dr["iStatus"]),
                             JenisSP2D = DataFormat.GetInteger(dr["btJenisSP2D"]),
                             KodeKegiatan = DataFormat.GetInteger(dr["btIDKegiatan"]),
                             KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                             KodeSubKegiatan = DataFormat.GetInteger(dr["btIdSubKegiatan"]),
                             IDProgram = DataFormat.GetInteger(dr["IdProgram"]),
                             IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                             IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                             IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                             PPKD = DataFormat.GetInteger(dr["bppkd"]),
                             NoUrutClient = DataFormat.GetLong(dr["iNoUrutClient"]),
                             //  NobuktiClient = DataFormat.GetString(dr["NobuktiClient"]),
                             JenisBendahara = DataFormat.GetInteger(dr["btJenisBendahara"]),
                             BankBUD = DataFormat.GetInteger(dr["btJenisBendahara"]),
                             Kodebank = DataFormat.GetInteger(dr["btIDbank"]),
                             NamaBank = DataFormat.GetString(dr["sNamaBank"]),
                             dtInput = DataFormat.GetDateTime(dr["dtInput"]),
                             NoNTPN = DataFormat.GetString(dr["sNoNTPN"]),
                             SetorKeKasda = DataFormat.GetInteger(dr["iSetorKasda"]),
                             TahunLalu = DataFormat.GetInteger(dr["bTahunlalu"]),
                             NoUrut = DataFormat.GetLong(dr["iNourut"]),
                             KodeBilling = DataFormat.GetString(dr["KodeBilling"]),

                         };
                     }
                 }
                 return oSetor;
             }
             catch (Exception ex)
             {
                 _isError = true;
                 _lastError = ex.Message;
                 return oSetor;
             }

         }
        public Setor GetByIDClient(long inourutCliect)
        {
            Setor oSetor = new Setor();
            try
            {
                SSQL = "SELECT tSetor.* FROM " + m_sNamaTabel + " WHERE tSetor.inourutClient= " + inourutCliect.ToString();

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        oSetor = new Setor()
                        {
                            Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),


                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDinas"]),
                                    KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),//), DataFormat.GetInteger(dr["btKodeKategori, DataFormat.GetInteger(dr["btKodeKategoriPelaksana)
                                    KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                    dtBukuKas = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    Status = DataFormat.GetInteger(dr["iStatus"]),
                                    JenisSP2D = DataFormat.GetInteger(dr["btJenisSP2D"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIDKegiatan"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    KodeSubKegiatan = DataFormat.GetInteger(dr["btIdSubKegiatan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IdProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    PPKD = DataFormat.GetInteger(dr["bppkd"]),
                                    NoUrutClient = DataFormat.GetLong(dr["iNoUrutClient"]),
                                  //  NobuktiClient = DataFormat.GetString(dr["NobuktiClient"]),
                                    JenisBendahara = DataFormat.GetInteger(dr["btJenisBendahara"]),
                                    BankBUD = DataFormat.GetInteger(dr["btJenisBendahara"]),
                                    Kodebank = DataFormat.GetInteger(dr["btIDbank"]),
                                    NamaBank = DataFormat.GetString(dr["sNamaBank"]),
                                    dtInput = DataFormat.GetDateTime(dr["dtInput"]),
                                    NoNTPN = DataFormat.GetString(dr["sNoNTPN"]),
                                    SetorKeKasda = DataFormat.GetInteger(dr["iSetorKasda"]),
                                    TahunLalu = DataFormat.GetInteger(dr["bTahunlalu"]),
                                    NoUrut = DataFormat.GetLong(dr["iNourut"]),
                                    KodeBilling = DataFormat.GetString(dr["KodeBilling"]),          

                        };
                    }
                }
                return oSetor;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return oSetor;
            }

        }
        public List<Setor> GetByDinas(int iddinas, E_JENIS_SETOR iJenis, DateTime tanggalAwal, DateTime tanggalAkhir)
        {
            List<Setor> _lst = new List<Setor>();
            try
            {
                if (iJenis == E_JENIS_SETOR.E_SETOR_PAJAK)
                    SSQL = "SELECT tSetor.*, '' as NobuktiClient ,'' as NoNTPN,'' as KodeBilling FROM " + m_sNamaTabel + "  WHERE " +
                         " tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis= " + ((int)iJenis).ToString();

                if (iJenis == E_JENIS_SETOR.E_SETOR_CP)
                    SSQL = "SELECT tSetor.*, tSPP.sNoSP2D as NoBuktiClient,'' as NoNTPN,'' as KodeBilling  FROM " + m_sNamaTabel + " Left Join tSPP On tSPP.inourut= tSetor.iNoUrutClient WHERE " +
                         " tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis= " + ((int)iJenis).ToString();

                if (iJenis == E_JENIS_SETOR.E_SETOR_UYHD)
                    SSQL = "SELECT tSetor.*, tSPP.sNoSP2D as NoBuktiClient ,'' as NoNTPN,'' as KodeBilling   FROM " + m_sNamaTabel + " Left Join tSPP On tSPP.inourut= tSetor.iNoUrutClient WHERE " +
                         " tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis= " + ((int)iJenis).ToString();

                if (iJenis == E_JENIS_SETOR.E_SETOR_STS)
                    SSQL = "SELECT tSetor.*, '' as NobuktiClient  ,'' as NoNTPN,'' as KodeBilling    FROM " + m_sNamaTabel + "  WHERE " +
                         " tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis= " + ((int)iJenis).ToString();
                //
                if (iJenis == E_JENIS_SETOR.E_ALL)
                    SSQL = "SELECT tSetor.*, tSPP.sNoSP2D as NoBuktiClient FROM " + m_sNamaTabel + " Left Join tSPP On tSPP.inourut= tSetor.iNoUrutClient WHERE " +
                     " tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis in (2,3,6)";


                SSQL = SSQL + " AND tSetor.dtBukukas between " + tanggalAwal.ToSQLFormat() + " AND " + tanggalAkhir.ToSQLFormat();
                SSQL = SSQL + " ORDER BY tSetor.dtBukukas, tSetor.inourut ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Setor()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),


                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDinas"]),
                                    KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),//), DataFormat.GetInteger(dr["btKodeKategori, DataFormat.GetInteger(dr["btKodeKategoriPelaksana)
                                    KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                    dtBukuKas = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    Status = DataFormat.GetInteger(dr["iStatus"]),
                                    JenisSP2D = DataFormat.GetInteger(dr["btJenisSP2D"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIDKegiatan"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    KodeSubKegiatan = DataFormat.GetInteger(dr["btIdSubKegiatan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IdProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    PPKD = DataFormat.GetInteger(dr["bppkd"]),
                                    NoUrutClient = DataFormat.GetLong(dr["iNoUrutClient"]),
                                    NobuktiClient = DataFormat.GetString(dr["NobuktiClient"]),
                                    JenisBendahara = DataFormat.GetInteger(dr["btJenisBendahara"]),
                                    BankBUD = DataFormat.GetInteger(dr["btJenisBendahara"]),
                                    Kodebank = DataFormat.GetInteger(dr["btIDbank"]),
                                    NamaBank = DataFormat.GetString(dr["sNamaBank"]),
                                    dtInput = DataFormat.GetDateTime(dr["dtInput"]),
                                    NoNTPN = DataFormat.GetString(dr["sNoNTPN"]),
                                    SetorKeKasda = DataFormat.GetInteger(dr["iSetorKasda"]),
                                    TahunLalu = DataFormat.GetInteger(dr["bTahunlalu"]),
                                    NoUrut = DataFormat.GetLong(dr["iNourut"]),
                                    KodeBilling = DataFormat.GetString(dr["KodeBilling"]),




                                }).ToList(); ;
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
        public List<Setor> Get (ParameterBendahara parameter)
        {
            List<Setor> _lst = new List<Setor>();
            try
            {
                int iddinas = parameter.IDDInas;
                int jenis = parameter.Jenis;
                DateTime tanggalawal = parameter.TanggalAwal;
                DateTime tanggalakhir = parameter.TanggalAkhir;
                if (jenis == (int)E_JENIS_SETOR.E_SETOR_PAJAK)
                    SSQL = "SELECT tSetor.*, '' as NobuktiClient ,'' as NoNTPN,'' as KodeBilling FROM " + m_sNamaTabel + "  WHERE " +
                         " tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis= " + (jenis).ToString();

                if (jenis == (int)E_JENIS_SETOR.E_SETOR_CP)
                    SSQL = "SELECT tSetor.*, tSPP.sNoSP2D as NoBuktiClient,'' as NoNTPN,'' as KodeBilling  FROM " + m_sNamaTabel + " Left Join tSPP On tSPP.inourut= tSetor.iNoUrutClient WHERE " +
                         " tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis= " + (jenis).ToString();

                if (jenis == (int)E_JENIS_SETOR.E_SETOR_UYHD || jenis == (int)E_JENIS_SETOR.E_SETOR_SISATU)
                    SSQL = "SELECT tSetor.*, tSPP.sNoSP2D as NoBuktiClient ,'' as NoNTPN,'' as KodeBilling   FROM " + m_sNamaTabel + " Left Join tSPP On tSPP.inourut= tSetor.iNoUrutClient WHERE " +
                         " tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis= " + ((jenis)).ToString();

                if (jenis == (int)E_JENIS_SETOR.E_SETOR_STS)
                    SSQL = "SELECT tSetor.*, '' as NobuktiClient  ,'' as NoNTPN,'' as KodeBilling    FROM " + m_sNamaTabel + "  WHERE " +
                         " tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis= " + ((jenis)).ToString();
                //
                if (jenis == (int)E_JENIS_SETOR.E_ALL)
                    SSQL = "SELECT tSetor.*, tSPP.sNoSP2D as NoBuktiClient FROM " + m_sNamaTabel + " Left Join tSPP On tSPP.inourut= tSetor.iNoUrutClient WHERE " +
                     " tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis in (2,3,6)";


                SSQL = SSQL + " AND tSetor.dtBukukas between " + tanggalawal.ToSQLFormat() + " AND " + tanggalakhir.ToSQLFormat();
                SSQL = SSQL + " ORDER BY tSetor.dtBukukas, tSetor.inourut ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Setor()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),


                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDinas"]),
                                    KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),//), DataFormat.GetInteger(dr["btKodeKategori, DataFormat.GetInteger(dr["btKodeKategoriPelaksana)
                                    KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                    dtBukuKas = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    Status = DataFormat.GetInteger(dr["iStatus"]),
                                    JenisSP2D = DataFormat.GetInteger(dr["btJenisSP2D"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIDKegiatan"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    KodeSubKegiatan = DataFormat.GetInteger(dr["btIdSubKegiatan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IdProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    PPKD = DataFormat.GetInteger(dr["bppkd"]),
                                    NoUrutClient = DataFormat.GetLong(dr["iNoUrutClient"]),
                                    NobuktiClient = DataFormat.GetString(dr["NobuktiClient"]),
                                    JenisBendahara = DataFormat.GetInteger(dr["btJenisBendahara"]),
                                    BankBUD = DataFormat.GetInteger(dr["btJenisBendahara"]),
                                    Kodebank = DataFormat.GetInteger(dr["btIDbank"]),
                                    NamaBank = DataFormat.GetString(dr["sNamaBank"]),
                                    dtInput = DataFormat.GetDateTime(dr["dtInput"]),
                                    NoNTPN = DataFormat.GetString(dr["sNoNTPN"]),
                                    SetorKeKasda = DataFormat.GetInteger(dr["iSetorKasda"]),
                                    TahunLalu = DataFormat.GetInteger(dr["bTahunlalu"]),
                                    NoUrut = DataFormat.GetLong(dr["iNourut"]),
                                    KodeBilling = DataFormat.GetString(dr["KodeBilling"]),




                                }).ToList(); ;
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
        public List<Setor> GetByDinasForJurnal(int iddinas, 
            E_JENIS_SETOR iJenis, 
            DateTime tanggalAwal, 
            DateTime tanggalAkhir)
        {
            List<Setor> _lst = new List<Setor>();
            try
            {
                if (iJenis == E_JENIS_SETOR.E_SETOR_PAJAK)
                    SSQL = "SELECT tSetor.*, '' as NobuktiClient ,'' as NoNTPN,'' as KodeBilling, dbo.IsInJurnal(tSetor.inourut) as InJurnal   FROM " + m_sNamaTabel + "  WHERE " +
                         " tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis= " + ((int)iJenis).ToString();

                if (iJenis == E_JENIS_SETOR.E_SETOR_CP)
                    SSQL = "SELECT tSetor.*, tSPP.sNoSP2D as NoBuktiClient,'' as NoNTPN,'' as KodeBilling, dbo.IsInJurnal(tSetor.inourut) as InJurnal    FROM " + m_sNamaTabel + " Left Join tSPP On tSPP.inourut= tSetor.iNoUrutClient WHERE " +
                         " tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis= " + ((int)iJenis).ToString();

                if (iJenis == E_JENIS_SETOR.E_SETOR_UYHD)
                    SSQL = "SELECT tSetor.*, tSPP.sNoSP2D as NoBuktiClient ,'' as NoNTPN,'' as KodeBilling, dbo.IsInJurnal(tSetor.inourut) as InJurnal    FROM " + m_sNamaTabel + " Left Join tSPP On tSPP.inourut= tSetor.iNoUrutClient WHERE " +
                         " tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis= " + ((int)iJenis).ToString();

                if (iJenis == E_JENIS_SETOR.E_SETOR_STS)
                    SSQL = "SELECT tSetor.*, '' as NobuktiClient  ,'' as NoNTPN,'' as KodeBilling, dbo.IsInJurnal(tSetor.inourut) as InJurnal      FROM " + m_sNamaTabel + "  WHERE " +
                         " tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis= " + ((int)iJenis).ToString();
                //
                if (iJenis == E_JENIS_SETOR.E_ALL)
                    SSQL = "SELECT tSetor.*, tSPP.sNoSP2D as NoBuktiClient ,'' as NoNTPN,'' as KodeBilling , dbo.IsInJurnal(tSetor.inourut) as InJurnal  FROM " + m_sNamaTabel + " Left Join tSPP On tSPP.inourut= tSetor.iNoUrutClient WHERE " +
                     " tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis in (2,3)";


                SSQL = SSQL + " AND tSetor.dtBukukas between " + tanggalAwal.ToSQLFormat() + " AND " + tanggalAkhir.ToSQLFormat();
                SSQL = SSQL + " ORDER BY tSetor.dtBukukas, tSetor.inourut ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Setor()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),


                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDinas"]),
                                    KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),//), DataFormat.GetInteger(dr["btKodeKategori, DataFormat.GetInteger(dr["btKodeKategoriPelaksana)
                                    KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                    dtBukuKas = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    Status = DataFormat.GetInteger(dr["InJurnal"]),

                                    JenisSP2D = DataFormat.GetInteger(dr["btJenisSP2D"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIDKegiatan"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    KodeSubKegiatan = DataFormat.GetInteger(dr["btIdSubKegiatan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IdProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    PPKD = DataFormat.GetInteger(dr["bppkd"]),
                                    NoUrutClient = DataFormat.GetLong(dr["iNoUrutClient"]),
                                    NobuktiClient = DataFormat.GetString(dr["NobuktiClient"]),
                                    JenisBendahara = DataFormat.GetInteger(dr["btJenisBendahara"]),
                                    BankBUD = DataFormat.GetInteger(dr["btJenisBendahara"]),
                                    Kodebank = DataFormat.GetInteger(dr["btIDbank"]),
                                    NamaBank = DataFormat.GetString(dr["sNamaBank"]),
                                    dtInput = DataFormat.GetDateTime(dr["dtInput"]),
                                    NoNTPN = DataFormat.GetString(dr["sNoNTPN"]),
                                    SetorKeKasda = DataFormat.GetInteger(dr["iSetorKasda"]),
                                    TahunLalu = DataFormat.GetInteger(dr["bTahunlalu"]),
                                    NoUrut = DataFormat.GetLong(dr["iNourut"]),
                                    KodeBilling = DataFormat.GetString(dr["KodeBilling"]),




                                }).ToList(); ;
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
        public List<Setor> GetByDinasUntukJurnal(int iddinas,E_JENIS_SETOR iJenis, DateTime tanggalAwal, DateTime tanggalAkhir)
        {
            List<Setor> _lst = new List<Setor>();
            try
            {
                if (iJenis == E_JENIS_SETOR.E_SETOR_PAJAK)
                    SSQL = "SELECT tSetor.*, '' as NobuktiClient ,'' as NoNTPN,'' as KodeBilling,dbo.IsInJurnal(tSetor.inourut) as InJurnal  FROM " + m_sNamaTabel + "  WHERE " +
                         " tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis= " + ((int)iJenis).ToString();

                if (iJenis == E_JENIS_SETOR.E_SETOR_CP)
                    SSQL = "SELECT tSetor.*, tSPP.sNoSP2D as NoBuktiClient,'' as NoNTPN,'' as KodeBilling ,dbo.IsInJurnal(tSetor.inourut) as InJurnal   FROM " + m_sNamaTabel + " Left Join tSPP On tSPP.inourut= tSetor.iNoUrutClient WHERE " +
                         " tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis= " + ((int)iJenis).ToString();

                if (iJenis == E_JENIS_SETOR.E_SETOR_UYHD)
                    SSQL = "SELECT tSetor.*, tSPP.sNoSP2D as NoBuktiClient ,'' as NoNTPN,'' as KodeBilling  ,dbo.IsInJurnal(tSetor.inourut) as InJurnal   FROM " + m_sNamaTabel + " Left Join tSPP On tSPP.inourut= tSetor.iNoUrutClient WHERE " +
                         " tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis= " + ((int)iJenis).ToString();
             
                if (iJenis == E_JENIS_SETOR.E_SETOR_STS)
                    SSQL = "SELECT tSetor.*, '' as NobuktiClient  ,'' as NoNTPN,'' as KodeBilling ,dbo.IsInJurnal(tSetor.inourut) as InJurnal     FROM " + m_sNamaTabel + "  WHERE " +
                         " tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis= " + ((int)iJenis).ToString();
                //
                if (iJenis == E_JENIS_SETOR.E_ALL)
                    SSQL = "SELECT tSetor.*, tSPP.sNoSP2D as NoBuktiClient ,dbo.IsInJurnal(tSetor.inourut) as InJurnal  FROM " + m_sNamaTabel + " Left Join tSPP On tSPP.inourut= tSetor.iNoUrutClient WHERE " +
                     " tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis in (2,3)";


                SSQL = SSQL  + " AND tSetor.dtBukukas between " + tanggalAwal.ToSQLFormat() + " AND " + tanggalAkhir.ToSQLFormat();
                SSQL = SSQL + " ORDER BY tSetor.dtBukukas, tSetor.inourut ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                         _lst = (from DataRow dr in dt.Rows
                               select new Setor()
                               {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                               
                                     
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDinas"]),
                                    KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),//), DataFormat.GetInteger(dr["btKodeKategori, DataFormat.GetInteger(dr["btKodeKategoriPelaksana)
                                    KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                    dtBukuKas = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    Status = DataFormat.GetInteger(dr["InJurnal"]),
                                    JenisSP2D = DataFormat.GetInteger(dr["btJenisSP2D"]),                                    
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIDKegiatan"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    KodeSubKegiatan = DataFormat.GetInteger(dr["btIdSubKegiatan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IdProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong (dr["IDSubKegiatan"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    PPKD = DataFormat.GetInteger(dr["bppkd"]),
                                    NoUrutClient = DataFormat.GetLong(dr["iNoUrutClient"]),
                                    NobuktiClient = DataFormat.GetString(dr["NobuktiClient"]),
                                    JenisBendahara = DataFormat.GetInteger(dr["btJenisBendahara"]),
                                    BankBUD = DataFormat.GetInteger(dr["btJenisBendahara"]),
                                    Kodebank = DataFormat.GetInteger(dr["btIDbank"]),
                                    NamaBank = DataFormat.GetString(dr["sNamaBank"]),
                                    dtInput = DataFormat.GetDateTime(dr["dtInput"]),
                                    NoNTPN = DataFormat.GetString(dr["sNoNTPN"]),                                  
                                    SetorKeKasda = DataFormat.GetInteger(dr["iSetorKasda"]),
                                    TahunLalu = DataFormat.GetInteger(dr["bTahunlalu"]),                             
                                    NoUrut = DataFormat.GetLong(dr["iNourut"]),
                                    KodeBilling = DataFormat.GetString(dr["KodeBilling"]), 
                         
                          
     

                        }).ToList();;
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
        public List<SetorRekening> GetDetailByDinas(int iddinas, E_JENIS_SETOR iJenis, DateTime tanggalAwal, DateTime tanggalAkhir)
        {
            List<SetorRekening> _lst = new List<SetorRekening>();
            try
            {
                if (iJenis == E_JENIS_SETOR.E_SETOR_PAJAK)
                    SSQL = "SELECT tSetorRekening.*, mRekening.sNamaRekening  as NamaRekening FROM tSetorRekening INNER JOIN tSetor " +
                        " ON tSetorRekening.inourut = tSetor.inourut inner join mRekening on tSetorRekening.IIDrekening = mRekening.IIDRekening  " +
                        "WHERE tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis= " + ((int)iJenis).ToString();

                if (iJenis == E_JENIS_SETOR.E_SETOR_CP)
                    SSQL = "SELECT tSetorRekening.*, mRekening.sNamaRekening  as NamaRekening FROM tSetorRekening INNER JOIN tSetor " +
                       " ON tSetorRekening.inourut = tSetor.inourut inner join mRekening on tSetorRekening.IIDrekening = mRekening.IIDRekening  " +
                       "WHERE tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis= " + ((int)iJenis).ToString();

                if (iJenis == E_JENIS_SETOR.E_SETOR_STS)
                    SSQL = "SELECT tSetorRekening.*, mRekening.sNamaRekening  as NamaRekening FROM tSetorRekening INNER JOIN tSetor " +
                        " ON tSetorRekening.inourut = tSetor.inourut inner join mRekening on tSetorRekening.IIDrekening = mRekening.IIDRekening  " +
                        "WHERE tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis= " + ((int)iJenis).ToString();
                if (iJenis == E_JENIS_SETOR.E_ALL)
                    SSQL = "SELECT tSetorRekening.*, mRekening.sNamaRekening  as NamaRekening FROM tSetorRekening INNER JOIN tSetor " +
                        " ON tSetorRekening.inourut = tSetor.inourut inner join mRekening on tSetorRekening.IIDrekening = mRekening.IIDRekening  " +
                        "WHERE tSetor.IDDInas  = " + iddinas.ToString() + " AND tSetor.btJenis in (2,3,6)";
              

                SSQL = SSQL + " AND tSetor.dtBukukas between " + tanggalAwal.ToSQLFormat() + " AND " + tanggalAkhir.ToSQLFormat();

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SetorRekening()
                                {
                                    IDRekening= DataFormat.GetLong(dr["IIDRekening"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    NamaRekening = DataFormat.GetString(dr["NamaRekening"]),
                                    NoUrut = DataFormat.GetLong(dr["InoUrut"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSUBKEGIATAN"]),
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
        public bool CatatBKU(Setor str,int jenisBendahara = 2) {

            try
            {
                List<BKU> lstBKU = new List<BKU>();
                BKU MaxNoBKU = new BKU();
                BKU MaxNoBKUKasda = new BKU();


                BKULogic oBKULogic = new BKULogic(Tahun);
                List<long> lstNoUrut = new List<long>();

                lstNoUrut.Add(str.NoUrut);
                Console.WriteLine (str.NoUrut.ToString());
                if (str.NoUrut == 24072170100028952)
                {
                    Console.WriteLine(str.NoUrut.ToString());
                }
                lstBKU = oBKULogic.GetBKUByNoUrutSumber(lstNoUrut, -1);

                MaxNoBKU = oBKULogic.GetBKUDenganMaxNoBKU(str.IDDinas, str.KodeUK, jenisBendahara);//
                MaxNoBKUKasda = oBKULogic.GetBKUDenganMaxNoBKU(str.IDDinas, str.KodeUK, 0);//


                m_connection = _dbHelper.CreateCOnnection();
                m_objTrans = m_connection.BeginTransaction();

                int jenisSumber = 0;
                int JenisBendahara = 0;
                int LevleTampilan = 0;
                int debet = 1;
                switch (str.Jenis)
                {
                    case 0:

                    case 2:
                        debet = -1;
                        JenisBendahara = (int)E_JENISBENDAHARA.BENDAHARA_PENGELUARAN;
                        jenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_TCP;
                        LevleTampilan = 1;
                        break;
                    case 3:
                        debet = 1;
                        LevleTampilan = 2;
                        JenisBendahara = (int)E_JENISBENDAHARA.BENDAHARA_PENGELUARAN;
                        jenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_TCP;
                        break;
                    case 4:
                        debet = -1;
                        jenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_PENYETORANPAJAK;
                        JenisBendahara = (int)E_JENISBENDAHARA.BENDAHARA_PENGELUARAN;
                        LevleTampilan = 2;
                        break;

                    case (int)E_JENIS_SETOR.E_SETOR_STS:

                        debet = -1;
                        LevleTampilan = 2;
                        jenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_SETORSTS;
                        JenisBendahara = (int)E_JENISBENDAHARA.BENDAHARA_PENERIMAAN;

                        break;

                }
                #region penyimpanan bku

                if (jenisSumber == (int)E_JENIS_REFERENSIBKU.REFERENSI_TCP)
                {
                    bool berhasilBKU = true;
                    if (str.Jenis == 2)
                    {
                        if (SimpanBKU(str, -1, lstBKU, MaxNoBKU, jenisSumber,
                                  m_connection, m_objTrans) == false)
                        {
                            berhasilBKU = berhasilBKU && false;
                        }
                    }
                    else
                    {

                        if (SimpanBKU(str, 1, lstBKU, MaxNoBKU, jenisSumber,
                                      m_connection, m_objTrans) == false)
                        {
                            berhasilBKU = berhasilBKU && false;
                        }

                    }
                    if (berhasilBKU == false)
                    {
                        m_objTrans.Rollback();
                    }
                    else
                    {
                        m_objTrans.Commit();
                    }
                }
                if (jenisSumber == (int)E_JENIS_REFERENSIBKU.REFERENSI_SETORSTS)
                {
                    bool berhasilBKU = true;

                    if (SimpanBKU(str, -1, lstBKU, MaxNoBKU, jenisSumber,
                                  m_connection, m_objTrans) == false)
                    {
                        berhasilBKU = berhasilBKU && false;
                    }
                    else
                    {
                        
                            berhasilBKU = berhasilBKU && false;

                        
                    }


                    if (berhasilBKU == false)
                    {
                        m_objTrans.Rollback();
                    }
                    else
                    {
                        m_objTrans.Commit();
                    }
                }
                if (jenisSumber == (int)E_JENIS_REFERENSIBKU.REFERENSI_PENYETORANPAJAK)
                {
                    bool berhasilBKU = true;

                    if (SimpanBKU(str, -1, lstBKU, MaxNoBKU, jenisSumber,
                                  m_connection, m_objTrans) == false)

                        berhasilBKU = berhasilBKU && false;

                    if (berhasilBKU == false)
                    {
                        m_objTrans.Rollback();
                    }
                    else
                    {
                        m_objTrans.Commit();
                    }


                }

                #endregion penyimpanan bku 

                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }

        public long Simpan(Setor str, int jenisBendahara = 2, bool headerOnly = false )
        {

            List<BKU> lstBKU = new List<BKU>();
            BKU MaxNoBKU = new BKU();
            BKU MaxNoBKUKasda = new BKU(); 


            BKULogic oBKULogic = new BKULogic(Tahun);
            List<long> lstNoUrut = new List<long>();

            lstNoUrut.Add(str.NoUrut);

            lstBKU = oBKULogic.GetBKUByNoUrutSumber(lstNoUrut,-1);

            MaxNoBKU = oBKULogic.GetBKUDenganMaxNoBKU(str.IDDinas, str.KodeUK, jenisBendahara);//
            MaxNoBKUKasda = oBKULogic.GetBKUDenganMaxNoBKU(str.IDDinas, str.KodeUK,  0);//


            m_connection = _dbHelper.CreateCOnnection();
            m_objTrans = m_connection.BeginTransaction();


            try
            {
                 if (str.NoUrut == 0)
                {

                    //long lNoUrut = ReadNo(E_KOLOM_NOURUT.CON_URUT_SETOR, str.IDDinas);
                    //m_noUrut = DataFormat.GetLong(
                    //       GetNoUrut(E_KOLOM_NOURUT.CON_URUT_PANJAR, Tahun, pengeluaran.IDDInas));

                    long lNoUrut = DataFormat.GetLong(
                         GetNoUrut(E_KOLOM_NOURUT.CON_URUT_PANJAR, Tahun, str.IDDinas));
                    SSQL = "INSERT INTO tSetor (iNOurut ,IDDInas, IDUrusan, IDProgram, IDKegiatan, iTahun,btKodekategori,btKodeUrusan,btKodeSKPD,btKodeUK,btKodekategoriPelaksana," +
                        " btKodeUrusanPelaksana,sNoBukti,dtBukuKas,sKeterangan,cJumlah,btJenis,iStatus,btJenisSP2D,btIDProgram,btIDKegiatan, " +
                        " bPPKD,iNoUrutClient,btJenisBendahara,sNoRek,sNamaBank,sNamaPenerima,sAlamat,sNPWP,iBankBUD, btIDSubkegiatan,btIDbank," +
                         "IDImport, sNoNTPN, iSetorKasda, btahunlalu ,KodeBilling,UnitANggaran) values (" +
                         "@piNOurut ,@pIDDInas, @pIDUrusan, @pIDProgram, @pIDKegiatan, @piTahun,@pbtKodekategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeUK,@pbtKodekategoriPelaksana," +
                        " @pbtKodeUrusanPelaksana,@psNoBukti,@pdtBukuKas,@psKeterangan,@pcJumlah,@pbtJenis,@piStatus,@pbtJenisSP2D,@pbtIDProgram,@pbtIDKegiatan, " +
                        " @pbPPKD,@piNoUrutClient,@pbtJenisBendahara,@psNoRek,@psNamaBank,@psNamaPenerima,@psAlamat,@psNPWP,@piBankBUD, @pbtIDSubkegiatan,@pbtIDbank," +
                         "@pIDImport, @psNoNTPN, @piSetorKasda, @pbtahunlalu,@KodeBilling,@UnitAnggaran )";


                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@piNOurut", lNoUrut,DbType.Int64));
                     paramCollection.Add(new DBParameter("@pIDDInas",str.IDDinas,DbType.Int32));
                     paramCollection.Add(new DBParameter("@pIDUrusan", str.IDUrusan, DbType.Int32));
                     paramCollection.Add(new DBParameter("@pIDProgram", str.IDProgram, DbType.Int32));
                     paramCollection.Add(new DBParameter("@pIDKegiatan", str.IDKegiatan));

                     paramCollection.Add(new DBParameter("@piTahun", str.Tahun, DbType.Int32));
                     paramCollection.Add(new DBParameter("@pbtKodekategori", str.KodeKategori, DbType.Int32));
                     paramCollection.Add(new DBParameter("@pbtKodeUrusan", str.KodeUrusan, DbType.Int32));
                     paramCollection.Add(new DBParameter("@pbtKodeSKPD", str.KodeSKPD, DbType.Int32));
                     paramCollection.Add(new DBParameter("@pbtKodeUK", str.KodeUK, DbType.Int32));
                     paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", str.KodekategoriPelaksana, DbType.Int32));
                     paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", str.KodeUrusanPelaksana, DbType.Int32));
                     paramCollection.Add(new DBParameter("@psNoBukti", str.NoBukti, DbType.String));
                    paramCollection.Add(new DBParameter("@pdtBukuKas", str.dtBukuKas,DbType.Date ));
                    paramCollection.Add(new DBParameter("@psKeterangan", str.Keterangan, DbType.String));
                    paramCollection.Add(new DBParameter("@pcJumlah", str.Jumlah,DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pbtJenis", str.Jenis, DbType.Int32));
                    paramCollection.Add(new DBParameter("@piStatus", 0));
                    paramCollection.Add(new DBParameter("@pbtJenisSP2D", str.JenisSP2D, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtIDProgram", str.KodeProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtIDKegiatan", str.KodeKategori, DbType.Int32));


                    paramCollection.Add(new DBParameter("@pbPPKD", str.PPKD, DbType.Int32));
                    paramCollection.Add(new DBParameter("@piNoUrutClient", str.NoUrutClient, DbType.Int64));
                    paramCollection.Add(new DBParameter("@pbtJenisBendahara", str.JenisBendahara, DbType.Int32));
                    paramCollection.Add(new DBParameter("@psNoRek", str.NoRekening, DbType.String));
                    paramCollection.Add(new DBParameter("@psNamaBank", str.NamaBank, DbType.String));
                    paramCollection.Add(new DBParameter("@psNamaPenerima", str.Penerima, DbType.String));
                    paramCollection.Add(new DBParameter("@psAlamat", "", DbType.String));
                    paramCollection.Add(new DBParameter("@psNPWP", str.NPWP, DbType.String));
                    paramCollection.Add(new DBParameter("@piBankBUD", str.BankBUD));

                    paramCollection.Add(new DBParameter("@pbtIDSubkegiatan", str.KodeSubKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtIDbank", str.Kodebank));
                    paramCollection.Add(new DBParameter("@pIDImport", str.IDImport));

                    paramCollection.Add(new DBParameter("@psNoNTPN", str.NoNTPN));
                    paramCollection.Add(new DBParameter("@piSetorKasda", str.SetorKeKasda));
                    paramCollection.Add(new DBParameter("@pbtahunlalu", str.TahunLalu));
                    paramCollection.Add(new DBParameter("@KodeBilling",str.KodeBilling));
                    paramCollection.Add(new DBParameter("@UnitAnggaran", str.UnitAnggaran));


                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection,m_objTrans);
                    str.NoUrut = lNoUrut;

                }
                else
                {
                    

                    SSQL = "UPDATE tSetor SET iTahun=@piTahun,btKodekategori=@pbtKodekategori,btKodeUrusan=@pbtKodeUrusan,btKodeSKPD=@pbtKodeSKPD" +
                         ",btKodeUK=@pbtKodeUK,btKodekategoriPelaksana=@pbtKodekategoriPelaksana," +
                         " btKodeUrusanPelaksana=@pbtKodeUrusanPelaksana,sNoBukti=@psNoBukti,dtBukuKas=@pdtBukuKas,sKeterangan=@psKeterangan," +
                         "cJumlah=@pcJumlah,btJenis=@pbtJenis,iStatus=@piStatus,btJenisSP2D=@pbtJenisSP2D,btIDProgram=@pbtIDProgram,btIDKegiatan=@pbtIDKegiatan ," +
                         " bPPKD=@pbPPKD,iNoUrutClient=@piNoUrutClient,btJenisBendahara=@pbtJenisBendahara,sNoRek=@psNoRek," +
                         "sNamaBank=@psNamaBank,sNamaPenerima=@psNamaPenerima, " +
                         " sAlamat=@psAlamat,sNPWP=@psNPWP,iBankBUD=@piBankBUD, " +
                         " btIDSubkegiatan=@pbtIDSubkegiatan,btIDbank=@pbtIDbank," +
                         "IDImport=@pIDImport, IDUrusan =@pIDUrusan,idDinas =@pidDinas,idProgram =@pidProgram,IDkegiatan=@pIDkegiatan," +

                        " IDSubKegiatan =@pIDSubKegiatan,sNoNTPN=@psNoNTPN, iSetorKasda=@piSetorKasda, btahunlalu=@pbtahunlalu,KodeBilling=@KodeBilling " +
                        " WHERE iNoUrut= @piNoUrurt";


                    //The parameterized query '(@piTahun nvarchar(4000),@pbtKodekategori nvarchar(4000),@pbtKod' expects the parameter '@psAlamat', which was not supplied."

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@piTahun", str.Tahun,DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodekategori", str.KodeKategori,DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusan", str.KodeUrusan,DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeSKPD", str.KodeSKPD,DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeUK", str.KodeUK,DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", str.KodekategoriPelaksana,DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", str.KodeUrusanPelaksana,DbType.Int32));
                    paramCollection.Add(new DBParameter("@psNoBukti", str.NoBukti,DbType.String));
                    paramCollection.Add(new DBParameter("@pdtBukuKas", str.dtBukuKas,DbType.Date));
                    paramCollection.Add(new DBParameter("@psKeterangan", str.Keterangan,DbType.String));
                    paramCollection.Add(new DBParameter("@pcJumlah", str.Jumlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pbtJenis", str.Jenis,DbType.Int32 ));
                    paramCollection.Add(new DBParameter("@piStatus", str.Status, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtJenisSP2D", str.JenisSP2D, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtIDProgram", str.KodeProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtIDKegiatan", str.KodeKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbPPKD", 0, DbType.Int32));
                    paramCollection.Add(new DBParameter("@piNoUrutClient", str.NoUrutClient, DbType.Int64));
                    paramCollection.Add(new DBParameter("@pbtJenisBendahara", str.JenisBendahara, DbType.Int32));
                    paramCollection.Add(new DBParameter("@psNoRek", str.NoRekening, DbType.String));
                    paramCollection.Add(new DBParameter("@psNamaBank", str.NamaBank, DbType.String));
                    paramCollection.Add(new DBParameter("@psNamaPenerima", str.Penerima, DbType.String));
                    paramCollection.Add(new DBParameter("@psAlamat", str.Alamat, DbType.String));
                    paramCollection.Add(new DBParameter("@psNPWP", str.NPWP, DbType.String));
                    paramCollection.Add(new DBParameter("@piBankBUD", str.BankBUD, DbType.Int32));
                   
                    paramCollection.Add(new DBParameter("@pbtIDSubkegiatan", str.KodeSubKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtIDbank", str.Kodebank, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDImport", str.IDImport, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDUrusan", str.IDUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pidDinas", str.IDDinas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pidProgram", str.IDProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", str.IDKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDSubKegiatan", str.IDSubKegiatan, DbType.Int64));
                    paramCollection.Add(new DBParameter("@psNoNTPN", str.NoNTPN, DbType.String));
                    paramCollection.Add(new DBParameter("@piSetorKasda", str.SetorKeKasda, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtahunlalu", str.TahunLalu, DbType.Int32));
                    paramCollection.Add(new DBParameter("@piNoUrurt", str.NoUrut, DbType.Int64));
                    paramCollection.Add(new DBParameter("@KodeBilling", str.KodeBilling));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);

                    
                }
                 if (headerOnly == false)
                 {
                     // SIMPAN DETAIL 
                     SSQL = "";
                     SSQL = "DELETE tSetorRekening WHERE iNoUrut = @NOURUT";
                     DBParameterCollection paramDelete = new DBParameterCollection();
                     paramDelete.Add(new DBParameter("@NOURUT", str.NoUrut));
                     _dbHelper.ExecuteNonQuery(SSQL, paramDelete, m_connection, m_objTrans);


                     if (str.JenisSP2D >2  && (str.Jenis != (int)E_JENIS_SETOR.E_SETOR_UYHD || str.Jenis != (int)E_JENIS_SETOR.E_SETOR_SISATU) ||
                          str.Jenis == (int)E_JENIS_SETOR.E_SETOR_STS || str.Jenis == (int)E_JENIS_SETOR.E_SETOR_PAJAK)
                     {
                         if (str.Jenis == (int)E_JENIS_SETOR.E_SETOR_PAJAK)
                         {// nolkan nilai niali nya
                             SSQL = "UPDATE tPanjarPotongan SET  iNoSetorPajak =0,iStatusPajak = 0 where iNoSetorPajak=@NoUrut";

                             DBParameterCollection paramNormalkanUpdatePanjarPotongan = new DBParameterCollection();
                             paramNormalkanUpdatePanjarPotongan.Add(new DBParameter("@NoUrut", str.NoUrut));
                             _dbHelper.ExecuteNonQuery(SSQL, paramNormalkanUpdatePanjarPotongan, m_connection, m_objTrans);
                         }
                         foreach (SetorRekening sr in str.Details)
                         {
                             SSQL = "INSERT INTO tsetorRekening (iNoUrut, iIDRekening, cJumlah,KodeUK, IDSubKegiatan) values ( " +
                                  "@NoUrut, @IDRekening, @Jumlah,@KodeUK, @IDSubKegiatan)";

                             DBParameterCollection paramInsert = new DBParameterCollection();
                             paramInsert.Add(new DBParameter("@NoUrut", str.NoUrut));
                             paramInsert.Add(new DBParameter("@IDRekening", sr.IDRekening));
                             paramInsert.Add(new DBParameter("@Jumlah", sr.Jumlah, DbType.Decimal));
                             paramInsert.Add(new DBParameter("@KodeUK", sr.KodeuK));
                             paramInsert.Add(new DBParameter("@IDSubKegiatan", sr.IDSubKegiatan));
                             _dbHelper.ExecuteNonQuery(SSQL, paramInsert, m_connection, m_objTrans);
                             if (str.Jenis == (int)E_JENIS_SETOR.E_SETOR_PAJAK)
                             {
                                 SSQL = "UPDATE tPanjarPotongan SET  iNoSetorPajak =@NoUrut,iStatusPajak = 1 where inourut=@NoUrutBelanja and IIDRekening =@IDrekening ";

                                 DBParameterCollection paramUpdatePanjarPotongan = new DBParameterCollection();
                                 paramUpdatePanjarPotongan.Add(new DBParameter("@NoUrut", str.NoUrut));
                                 paramUpdatePanjarPotongan.Add(new DBParameter("@IDrekening", sr.IDRekening));
                                 paramUpdatePanjarPotongan.Add(new DBParameter("@NoUrutBelanja", sr.NoUrutBelanja));
                                 _dbHelper.ExecuteNonQuery(SSQL, paramUpdatePanjarPotongan, m_connection, m_objTrans);
                             }

                         }
                         if (str.Jenis == (int)E_JENIS_SETOR.E_SETOR_STS)
                         {
                             SSQL = "UPDATE tSTS SET iStatus = 0, inourutSetor =0 where inourutSetor =@NOURUTSETOR";
                             DBParameterCollection paramUpdateSTSToUnsetor = new DBParameterCollection();
                             paramUpdateSTSToUnsetor.Add(new DBParameter("@NOURUTSETOR", str.NoUrut));
                             _dbHelper.ExecuteNonQuery(SSQL, paramUpdateSTSToUnsetor, m_connection, m_objTrans);



                             foreach (STSDisetor stsdisetor in str.STSDisetors)
                             {
                                 SSQL = "UPDATE tSTS SET inourutSetor =@NOURUTSETOR, iStatus= 3 where inourut =@NOURUTSTS";
                                 DBParameterCollection paramUpdateSTS = new DBParameterCollection();
                                 paramUpdateSTS.Add(new DBParameter("@NOURUTSETOR", str.NoUrut));
                                 paramUpdateSTS.Add(new DBParameter("@NOURUTSTS", stsdisetor.NoUrut));

                                 _dbHelper.ExecuteNonQuery(SSQL, paramUpdateSTS, m_connection, m_objTrans);
                             }
                         }
                     }
                 }

                 int jenisSumber = 0;
                 int JenisBendahara = 0;
                 int LevleTampilan = 0;
                 int debet = 1;
                 switch (str.Jenis)
                 {
                     case 0:

                     case 2:
                         debet = -1;
                         JenisBendahara = (int)E_JENISBENDAHARA.BENDAHARA_PENGELUARAN;
                         jenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_TCP;
                         LevleTampilan = 1;
                         break;
                     case 3:
                         debet = 1;
                         LevleTampilan = 2;
                         JenisBendahara = (int)E_JENISBENDAHARA.BENDAHARA_PENGELUARAN;
                         jenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_TCP;
                         break;
                     case 4:
                         debet = -1;
                         jenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_PENYETORANPAJAK;
                         JenisBendahara = (int)E_JENISBENDAHARA.BENDAHARA_PENGELUARAN;
                         LevleTampilan = 2;
                         break;

                     case (int)E_JENIS_SETOR.E_SETOR_STS:

                         debet = -1;
                         LevleTampilan = 2;
                         jenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_SETORSTS;
                         JenisBendahara = (int)E_JENISBENDAHARA.BENDAHARA_PENERIMAAN;
                         break;
                     case 6:
                         debet = -1;
                         JenisBendahara = (int)E_JENISBENDAHARA.BENDAHARA_PENGELUARAN;
                         jenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_TCP;
                         LevleTampilan = 1;
                         break;
                        

                 }
#region penyimpanan bku       

                    if (jenisSumber == (int)E_JENIS_REFERENSIBKU.REFERENSI_TCP)
                    { 
                        bool berhasilBKU= true ;
                        if (str.Jenis == 2 || str.Jenis == 6)
                        {
                            if (SimpanBKU(str, -1, lstBKU, MaxNoBKU, jenisSumber,
                                      m_connection, m_objTrans) == false)
                            {
                                berhasilBKU = berhasilBKU && false;
                            }
                        }
                        else
                        {

                            if (SimpanBKU(str, 1, lstBKU, MaxNoBKU, jenisSumber,
                                          m_connection, m_objTrans) == false)
                            {
                                berhasilBKU = berhasilBKU && false;
                            }
                           
                        }
                        if (berhasilBKU == false)
                        {
                            m_objTrans.Rollback();
                        }
                        else
                        {
                            m_objTrans.Commit();
                        }
                     }
                    if (jenisSumber == (int)E_JENIS_REFERENSIBKU.REFERENSI_SETORSTS)
                    {
                        bool berhasilBKU = true;

                        if (SimpanBKU(str, -1, lstBKU, MaxNoBKU, jenisSumber,
                                      m_connection, m_objTrans) == false)
                        {
                            berhasilBKU = berhasilBKU && false;
                        }
                        else
                        {
                            
                                berhasilBKU = berhasilBKU && true;

                            
                        }


                        if (berhasilBKU == false)
                        {
                            m_objTrans.Rollback();
                        }
                        else
                        {
                            m_objTrans.Commit();
                        }
                    }
                    if (jenisSumber == (int)E_JENIS_REFERENSIBKU.REFERENSI_PENYETORANPAJAK )
                    {
                        bool berhasilBKU = true;

                        if (SimpanBKU(str, -1, lstBKU, MaxNoBKU, jenisSumber,
                                      m_connection, m_objTrans) == false)
                        
                            berhasilBKU = berhasilBKU && false;

                        if (berhasilBKU == false)
                        {
                            m_objTrans.Rollback();
                        }
                        else
                        {
                            m_objTrans.Commit();
                        }
                            
                        
                    }
                     
#endregion penyimpanan bku 
                
                return str.NoUrut;
            }

            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                m_objTrans.Rollback();
                m_connection.Close();
                return 0;


            }

        }

        public bool  Hapus(long noUrut, E_JENIS_SETOR jenissetor )
        {

            try
            {


                m_connection = _dbHelper.CreateCOnnection();
                m_objTrans = m_connection.BeginTransaction();

                    SSQL = "DELETE  tSetor WHERE iNoUrut= @piNoUrurt";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@piNoUrurt", noUrut, DbType.Int64));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);
                    SSQL = "";
                    SSQL = "DELETE tSetorRekening WHERE iNoUrut = @NOURUT";
                    DBParameterCollection paramDelete = new DBParameterCollection();
                    paramDelete.Add(new DBParameter("@NOURUT", noUrut));
                    _dbHelper.ExecuteNonQuery(SSQL, paramDelete, m_connection, m_objTrans);

                    SSQL = "DELETE  tBKuRekening  from tBKU INNER JOIN tBKUREkening ON tBKU.inourut = tBKUREkening.inourut WHERE tBKU.iNoUrutSumber= @NOURUT";
                    _dbHelper.ExecuteNonQuery(SSQL, paramDelete, m_connection, m_objTrans);

                    SSQL = "DELETE  tBKU WHERE tBKU.iNoUrutSumber= @NOURUT";
                    _dbHelper.ExecuteNonQuery(SSQL, paramDelete, m_connection, m_objTrans);

                    if (jenissetor == E_JENIS_SETOR.E_SETOR_STS)
                        {
                            SSQL = "UPDATE tSTS SET iStatus = 0, inourutSetor =0 where inourutSetor =@NOURUTSETOR";
                            DBParameterCollection paramUpdateSTSToUnsetor = new DBParameterCollection();
                            paramUpdateSTSToUnsetor.Add(new DBParameter("@NOURUTSETOR", noUrut));
                            _dbHelper.ExecuteNonQuery(SSQL, paramUpdateSTSToUnsetor, m_connection, m_objTrans);    

                    
                     }
                    if (jenissetor == E_JENIS_SETOR.E_SETOR_PAJAK)
                    {
                        SSQL = "UPDATE tPanjarPotongan SET iStatusPajak = 0, iNoSetorPajak = 0 where iNoSetorPajak =@NOURUTSETOR";
                        DBParameterCollection paramUpdateSTSToUnsetor = new DBParameterCollection();
                        paramUpdateSTSToUnsetor.Add(new DBParameter("@NOURUTSETOR", noUrut));
                        _dbHelper.ExecuteNonQuery(SSQL, paramUpdateSTSToUnsetor, m_connection, m_objTrans);


                    }

                    m_objTrans.Commit();
                    m_connection.Close();
             
                return true ;
            }

            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                m_objTrans.Rollback();
                m_connection.Close();
                return false ;


            }

        }
         private BKU GetOldBKU(Setor setor , 
                            List<BKU> lstBKU, 
                            E_JENISBENDAHARA JenisBendahara, 
                            int JenisSumber, int debet)
        {
            

            BKU oldBKU = lstBKU.FirstOrDefault(b => b.NourutSumber ==  setor.NoUrut && 
                                                        b.IDDinas== setor.IDDinas && 
                                                        b.JenisBendahara == JenisBendahara &&
                                                        b.JenisSumber == JenisSumber  && 
                                                        b.Debet == debet 
                                                        );

            return oldBKU;

        }

        private bool SimpanBKUBUD(Setor setor, int debet , 
                               List<BKU> lstBKU, BKU MaxNoBKU, 
                              int  JenisSumber,
                              int noBKU,
                              IDbConnection connection, IDbTransaction odbTrans)
         {
             try
             {
                 BKU oBKU = new BKU();
                 BKULogic oBKULogic = new BKULogic(Tahun);


                 oBKU.CreateFormSetorInKasda(setor, 1, JenisSumber, (int)E_JENISBENDAHARA.BENDAHARA_BUD);
                 BKU oldBKU = lstBKU.FirstOrDefault(b => b.NourutSumber == setor.NoUrut &&
                                                         b.IDDinas == setor.IDDinas &&
                                                         b.JenisBendahara == E_JENISBENDAHARA.BENDAHARA_BUD  &&
                                                         b.JenisSumber == JenisSumber &&
                                                         b.Debet == 1);

                 oBKU.JenisBendahara = E_JENISBENDAHARA.BENDAHARA_BUD;
                 oBKU.Debet = 1;
                 if (oldBKU != null)
                 {
                         oBKU.NoUrut = oldBKU.NoUrut;
                         oBKU.NoBKU = noBKU;
                         oBKU.NoBKUSKPD = noBKU;

                 }
                 else
                 {
                         oBKU.NoUrut = 0;
                         oBKU.NoBKU = noBKU;//MaxNoBKU.NoBKU + 1;
                         oBKU.NoBKUSKPD = noBKU;////MaxNoBKU.NoBKUSKPD + 1;
                         oBKU.NoUrutSaja = MaxNoBKU.NoUrutSaja + 1;
                 }
                 bool hasilSimpan = true;
                 if (oBKULogic.Simpan(ref oBKU, connection, odbTrans) == false)
                 {
                     hasilSimpan= false ;
                 }
                 return hasilSimpan;
                 
             }catch (Exception ex)
             {
                _lastError = ex.Message;
                _isError=true;
                return false;
            }
        }
        
        

        private bool SimpanBKU(Setor setor, int debet , 
                               List<BKU> lstBKU, BKU MaxNoBKU, 
                              int  JenisSumber,
                              IDbConnection connection, IDbTransaction odbTrans)
        {
            try
            { // BKU 
                BKU oBKU = new BKU();
                BKULogic oBKULogic = new BKULogic(Tahun);

                if (setor.Jenis == (int)E_JENIS_SETOR.E_SETOR_STS)
                {
                    oBKU.CreateFormSetorSTS(setor, -1, JenisSumber, (int)E_JENISBENDAHARA.BENDAHARA_PENERIMAAN);
                   BKU oldBKU = lstBKU.FirstOrDefault(b => b.NourutSumber == setor.NoUrut &&
                                                       b.IDDinas == setor.IDDinas &&
                                                       b.JenisBendahara == E_JENISBENDAHARA.BENDAHARA_PENERIMAAN &&
                                                       b.JenisSumber == JenisSumber &&
                                                       b.Debet == -1 );

                    oBKU.JenisBendahara = E_JENISBENDAHARA.BENDAHARA_PENERIMAAN;

                    if (oldBKU !=null)
                    {
                        oBKU.NoUrut = oldBKU.NoUrut;
                        oBKU.NoBKU = oldBKU.NoBKU;
                        oBKU.NoBKUSKPD = oldBKU.NoBKUSKPD;

                    }
                    else
                    {
                        oBKU.NoUrut = 0;
                        oBKU.NoBKU = MaxNoBKU.NoBKU + 1;
                        oBKU.NoBKUSKPD = MaxNoBKU.NoBKUSKPD + 1;
                        oBKU.NoUrutSaja = MaxNoBKU.NoUrutSaja + 1;

                        

                    }
                    if ( oBKULogic.Simpan(ref oBKU, connection, odbTrans ) ==  true ){
                        return true;
                    } else {
                        return false;
                    }


                }
                if (setor.Jenis == (int)E_JENIS_SETOR.E_SETOR_UYHD)
                {
                    oBKU.CreateFormPengembalian (setor, -1, JenisSumber, (int)E_JENISBENDAHARA.BENDAHARA_PENERIMAAN);
                    BKU oldBKU = lstBKU.FirstOrDefault(b => b.NourutSumber == setor.NoUrut &&
                                                        b.IDDinas == setor.IDDinas &&
                                                        b.JenisBendahara == E_JENISBENDAHARA.BENDAHARA_PENGELUARAN &&
                                                        b.JenisSumber == JenisSumber &&
                                                        b.Debet == -1);

                    oBKU.JenisBendahara = E_JENISBENDAHARA.BENDAHARA_PENGELUARAN;

                    if (oldBKU != null)
                    {
                        oBKU.NoUrut = oldBKU.NoUrut;
                        oBKU.NoBKU = oldBKU.NoBKU;
                        oBKU.NoBKUSKPD = oldBKU.NoBKUSKPD;

                    }
                    else
                    {
                        oBKU.NoUrut = 0;
                        oBKU.NoBKU = MaxNoBKU.NoBKU + 1;
                        oBKU.NoBKUSKPD = MaxNoBKU.NoBKUSKPD + 1;
                        oBKU.NoUrutSaja = MaxNoBKU.NoUrutSaja + 1;



                    }
                    if (oBKULogic.Simpan(ref oBKU, connection, odbTrans) == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }
                if (setor.Jenis == (int)E_JENIS_SETOR.E_SETOR_SISATU)
                {
                    oBKU.CreateFormPengembalian(setor, -1, JenisSumber, (int)E_JENISBENDAHARA.BENDAHARA_PENERIMAAN);
                    BKU oldBKU = lstBKU.FirstOrDefault(b => b.NourutSumber == setor.NoUrut &&
                                                        b.IDDinas == setor.IDDinas &&
                                                        b.JenisBendahara == E_JENISBENDAHARA.BENDAHARA_PENGELUARAN &&
                                                        b.JenisSumber == JenisSumber &&
                                                        b.Debet == -1);

                    oBKU.JenisBendahara = E_JENISBENDAHARA.BENDAHARA_PENGELUARAN;

                    if (oldBKU != null)
                    {
                        oBKU.NoUrut = oldBKU.NoUrut;
                        oBKU.NoBKU = oldBKU.NoBKU;
                        oBKU.NoBKUSKPD = oldBKU.NoBKUSKPD;

                    }
                    else
                    {
                        oBKU.NoUrut = 0;
                        oBKU.NoBKU = MaxNoBKU.NoBKU + 1;
                        oBKU.NoBKUSKPD = MaxNoBKU.NoBKUSKPD + 1;
                        oBKU.NoUrutSaja = MaxNoBKU.NoUrutSaja + 1;



                    }
                    if (oBKULogic.Simpan(ref oBKU, connection, odbTrans) == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }

                if (setor.Jenis == (int)E_JENIS_SETOR.E_SETOR_CP )
                {
                    oBKU.CreateFormPengembalian(setor, 1, JenisSumber, (int)E_JENISBENDAHARA.BENDAHARA_PENERIMAAN);
                    BKU oldBKU = lstBKU.FirstOrDefault(b => b.NourutSumber == setor.NoUrut &&
                                                        b.IDDinas == setor.IDDinas &&
                                                        b.JenisBendahara == E_JENISBENDAHARA.BENDAHARA_PENGELUARAN &&
                                                        b.JenisSumber == JenisSumber &&
                                                        b.Debet == 1);

                    oBKU.JenisBendahara = E_JENISBENDAHARA.BENDAHARA_PENGELUARAN;

                    if (oldBKU != null)
                    {
                        oBKU.NoUrut = oldBKU.NoUrut;
                        oBKU.NoBKU = oldBKU.NoBKU;
                        oBKU.NoBKUSKPD = oldBKU.NoBKUSKPD;

                    }
                    else
                    {
                        oBKU.NoUrut = 0;
                        MaxNoBKU.NoBKU++;
                        MaxNoBKU.NoBKUSKPD++;
                        MaxNoBKU.NoUrutSaja++;

                        oBKU.NoUrut = 0;
                        oBKU.NoBKU = MaxNoBKU.NoBKU;
                        oBKU.NoBKUSKPD = MaxNoBKU.NoBKUSKPD;
                        oBKU.NoUrutSaja = MaxNoBKU.NoUrutSaja;



                    }
                    if (oBKULogic.Simpan(ref oBKU, connection, odbTrans) == true)
                    {
                        
                        oBKU.CreateFormPengembalian(setor, -1, JenisSumber, (int)E_JENISBENDAHARA.BENDAHARA_PENERIMAAN);
                        oldBKU = lstBKU.FirstOrDefault(b => b.NourutSumber == setor.NoUrut &&
                                                            b.IDDinas == setor.IDDinas &&
                                                            b.JenisBendahara == E_JENISBENDAHARA.BENDAHARA_PENGELUARAN &&
                                                            b.JenisSumber == JenisSumber &&
                                                            b.Debet == -1);

                        oBKU.JenisBendahara = E_JENISBENDAHARA.BENDAHARA_PENGELUARAN;

                        if (oldBKU != null)
                        {
                            oBKU.NoUrut = oldBKU.NoUrut;
                            oBKU.NoBKU = oldBKU.NoBKU;
                            oBKU.NoBKUSKPD = oldBKU.NoBKUSKPD;

                        }
                        else
                        {
                            MaxNoBKU.NoBKU++;
                            MaxNoBKU.NoBKUSKPD++;
                            MaxNoBKU.NoUrutSaja++;
                            oBKU.NoUrut = 0;
                            oBKU.NoBKU = MaxNoBKU.NoBKU ;
                            oBKU.NoBKUSKPD = MaxNoBKU.NoBKUSKPD ;
                            oBKU.NoUrutSaja = MaxNoBKU.NoUrutSaja ;



                        }




                        return oBKULogic.Simpan(ref oBKU, connection, odbTrans) ;
                    }
                    else
                    {
                        return false;
                    }


                }
                if (setor.Jenis == (int)E_JENIS_SETOR.E_SETOR_PAJAK)
                {
                    if (setor.Details == null)
                    {
                       
                    }
                    oBKU.CreateFormPengembalian(setor, -1, JenisSumber, (int)E_JENISBENDAHARA.BENDAHARA_PENERIMAAN);
                    BKU oldBKU = lstBKU.FirstOrDefault(b => b.NourutSumber == setor.NoUrut &&
                                                        b.IDDinas == setor.IDDinas &&
                                                        b.JenisBendahara == E_JENISBENDAHARA.BENDAHARA_PENGELUARAN &&
                                                        b.JenisSumber == JenisSumber &&
                                                        b.Debet == -1);

                    oBKU.JenisBendahara = E_JENISBENDAHARA.BENDAHARA_PENGELUARAN;

                    if (oldBKU != null)
                    {
                        oBKU.NoUrut = oldBKU.NoUrut;
                        oBKU.NoBKU = oldBKU.NoBKU;
                        oBKU.NoBKUSKPD = oldBKU.NoBKUSKPD;

                    }
                    else
                    {
                        oBKU.NoUrut = 0;
                        MaxNoBKU.NoBKU++;
                        MaxNoBKU.NoBKUSKPD++;
                        MaxNoBKU.NoUrutSaja++;

                        oBKU.NoUrut = 0;
                        oBKU.NoBKU = MaxNoBKU.NoBKU;
                        oBKU.NoBKUSKPD = MaxNoBKU.NoBKUSKPD;
                        oBKU.NoUrutSaja = MaxNoBKU.NoUrutSaja;



                    }
                    if (oBKULogic.Simpan(ref oBKU, connection, odbTrans) == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }
                return true;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError=true;
                return false;
            }
        }
        public List<SetorRekening> GetDetail(long NoUrut)
        {
            List<SetorRekening> _lst = new List<SetorRekening>();
            try
            {
                SSQL = "SELECT tSetorRekening.*, mRekening.snamaRekening as Nama FROM tSetorRekening INNER JOIN mRekening ON tSetorRekening.IIDRekening = mRekening.IIDRekening WHERE tSetorRekening.inourut = " + NoUrut.ToString();

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SetorRekening()
                                {
                                    NoUrut = DataFormat.GetLong(dr["inourut"]),
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
                return _lst;
            }

        }

        public bool TerimaKasda(long NoUrut, int noUrutKasda)
        {
            try
            {
                //SSQL = "SELECT TSTSRekening.*, mRekening.snamaRekening as Nama FROM tSTSREkening INNER JOIN mRekening ON tSTSRekening.IIDRekening = mRekening.IIDRekening WHERE tSTSRekening.inourut = " + NoUrut.ToString();
                SSQL = "UPDATE tSetor set inourutkasda = " + noUrutKasda.ToString() + ", iStatus = 5 WHERE inourut = " + NoUrut.ToString();
                if (_dbHelper.ExecuteNonQuery(SSQL) > 0)
                {
                    if (SimpanBKUKasda(NoUrut, noUrutKasda) == false)
                    {
                        _lastError = " Gagal BKU kasda " + _lastError;
                        return false;
                    }
                }


                return true;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }

        }
        private bool SimpanBKUKasda(long noUrut,int NoBKUKASDA)
        {

            int jenisSumber = 0;
          
            int debet = 1;
            bool berhasilBKU = false;
            try
            {
                BKU bku = new BKU();
                BKULogic oBKULogic = new BKULogic(Tahun);
                Setor str = new Setor();
                SetorLogic setorLogic = new SetorLogic(Tahun);
          

                List<BKU> lstBKU = new List<BKU>();
                BKU MaxNoBKU = new BKU();
                BKU MaxNoBKUKasda = new BKU();

                str = setorLogic.GetByID(noUrut);
                if (str == null || str.NoUrut==0)
                {
                    _lastError = setorLogic.LastError();
                    return false;

                }
                str.Details = setorLogic.GetDetail(noUrut);
               List<long> lstNoUrut= new List<long>();
                lstNoUrut.Add(noUrut);
                lstBKU = oBKULogic.GetBKUByNoUrutSumber(lstNoUrut, 0);



                m_connection = _dbHelper.CreateCOnnection();
                m_objTrans = m_connection.BeginTransaction();


                
                   

                   
                    switch (str.Jenis)
                    {
                        case 0:

                        case 2:
                            debet = 1;
                          
                            jenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_TCP;
                          
                            break;
                        
                        case (int)E_JENIS_SETOR.E_SETOR_STS:

                            debet = 1;
                          
                            jenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_SETORSTS;
                          
                            break;

                    }
                    #region penyimpanan bku
                    berhasilBKU = true;
                    if (jenisSumber == (int)E_JENIS_REFERENSIBKU.REFERENSI_SETORSTS ||
                        jenisSumber == (int)E_JENIS_REFERENSIBKU.REFERENSI_TCP)
                    {
              

                        MaxNoBKUKasda.NoUrutSaja = MaxNoBKU.NoUrutSaja + 1;

                        if (SimpanBKUBUD(str, 1,lstBKU, MaxNoBKUKasda, jenisSumber, NoBKUKASDA, m_connection, m_objTrans) == false)
                        {
                            berhasilBKU = berhasilBKU && false;

                        }
                    }


                        if (berhasilBKU == false)
                        {
                            m_objTrans.Rollback();
                        }
                        else
                        {
                            m_objTrans.Commit();
                        }

                        return berhasilBKU;
                    #endregion penyimpanan bku

                   
                

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
            }
            return berhasilBKU;
        }
        public decimal GeJumlahSetor(int idDinas, int JenisSumber, DateTime tanggalAwal, DateTime tanggalakhir)
        {
            decimal dRet = 0;
            try
            {

                SSQL = "SELECT sum(tSetor.cJumlah) as Jumlah from tSetor " +
                    " WHERE tSetor.IDDInas =" + idDinas.ToString() + "AND tSetor.btJenis=4 " +
                     " AND tSetor.dtBukukas >=" + tanggalAwal.ToSQLFormat() + " AND tSetor.dtBukukas <=" + tanggalakhir.ToSQLFormat();

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        dRet = DataFormat.GetDecimal(dr["Jumlah"]);
                    }
                }
                return dRet;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return dRet;

            }
        }

        public decimal GeJumlahSetorDetail(int idDinas, int JenisSumber, DateTime tanggalAwal, DateTime tanggalakhir)
        {
            decimal dRet = 0;
            try
            {

                SSQL = "SELECT sum(tSetorRekening.cJumlah) as Jumlah from tSetor " +
                    " INNER JOIN tSetorRekening on tSetor.inourut = tSetorRekening.inoUrut " +
                    " WHERE tSetor.IDDInas =" + idDinas.ToString() + "AND tSetor.btJenis=4 " +
                     " AND tSetor.dtBukukas >=" + tanggalAwal.ToSQLFormat() + " AND tSetor.dtBukukas <=" + tanggalakhir.ToSQLFormat();

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        dRet = DataFormat.GetDecimal(dr["Jumlah"]);
                    }
                }
                return dRet;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return dRet;

            }
        }


    }
}
