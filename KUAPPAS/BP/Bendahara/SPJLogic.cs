using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DTO.Bendahara;
using BP;
using BP.Bendahara;
using DataAccess;
using System.Data;
using Formatting;
using Bendahara;

//using Excel = Microsoft.Office.Interop.Excel;

namespace BP.Bendahara
{
    public class SPJLogic : BP
    {
        IDbConnection m_connection;
        IDbTransaction m_objTrans;
        public SPJLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tSPJ";
        }

        public bool Simpan(ref SPJ oSPJ)
        {



            m_connection = _dbHelper.CreateCOnnection();
            m_objTrans = m_connection.BeginTransaction();
            try
            {
                if (oSPJ.NoUrut == 0)
                {

                    long lNoUrut = DataFormat.GetLong(GetNoUrut(E_KOLOM_NOURUT.CON_URUT_SPJ, Tahun, oSPJ.IDDinas));


                    SSQL = "INSERT INTO tSPJ (iNoUrut, iTahun,IDDinas, sNoBukti, iBulan, " +
                        "cJumlah, iStatus, dtBukuKas, sKeterangan, iNoBPP,dawalperiode,dAkhirperiode,btJenis,inourutClient) " +
                        " VALUES  ( @piNoUrut, @piTahun,  @pIDDinas,  @psNoBukti, @piBulan, " +
                        "@pcJumlah, 0, @pdtBukuKas, @psKeterangan, @piNoBPP," +
                        "@pdawalperiode,@pdAkhirperiode,@pbtJenis,@inourutClient )";

                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("@piNoUrut", lNoUrut, DbType.Int64));
                    paramCollection.Add(new DBParameter("@piTahun", oSPJ.Tahun, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeUK", oSPJ.KodeUk, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDDinas", oSPJ.IDDinas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@psNoBukti", oSPJ.NoSPJ, DbType.String));

                    paramCollection.Add(new DBParameter("@piBulan", oSPJ.Bulan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pcJumlah", oSPJ.Jumlah, DbType.Decimal));
                    //  paramCollection.Add(new DBParameter("@piStatus,", 0, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pdtBukuKas", oSPJ.DtSPJ, DbType.Date));
                    paramCollection.Add(new DBParameter("@psKeterangan", oSPJ.Keterangan, DbType.String));
                    paramCollection.Add(new DBParameter("@piNoBPP", 0, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pdawalperiode", oSPJ.DtAwal, DbType.Date));
                    paramCollection.Add(new DBParameter("@pdAkhirperiode", oSPJ.DtAkhir, DbType.Date));
                    paramCollection.Add(new DBParameter("@pbtJenis", oSPJ.Jenis, DbType.Int32));
                    paramCollection.Add(new DBParameter("@inourutClient", oSPJ.NoUrutClient, DbType.Int64));



                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);
                    oSPJ.NoUrut = lNoUrut;


                }
                else
                {

                    SSQL = "UPDATE tSPJ SET iTahun=@piTahun, " +
                        "IDDinas=@pIDDinas,  sNoBukti=@psNoBukti, iBulan=@piBulan, cJumlah=@pcJumlah,  dtBukuKas=@pdtBukuKas, " +
                        "sKeterangan=@psKeterangan,  iNoBPP=@piNoBPP,dawalperiode=@pdawalperiode,dAkhirperiode=@pdAkhirperiode" +
                       ",btJenis=@pbtJenis,bPPKD=@pbPPKD,inourutClient=@inourutClient WHERE inourut =@piNoUrut  ";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@piTahun", oSPJ.Tahun, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeUK", oSPJ.KodeUk, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDDinas", oSPJ.IDDinas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@psNoBukti", oSPJ.NoSPJ, DbType.String));

                    paramCollection.Add(new DBParameter("@piBulan", oSPJ.Bulan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pcJumlah", oSPJ.Jumlah, DbType.Decimal));

                    paramCollection.Add(new DBParameter("@pdtBukuKas", oSPJ.DtSPJ, DbType.Date));
                    paramCollection.Add(new DBParameter("@psKeterangan", oSPJ.Keterangan, DbType.String));
                    paramCollection.Add(new DBParameter("@piNoBPP", 0, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pdawalperiode", oSPJ.DtAwal, DbType.Date));
                    paramCollection.Add(new DBParameter("@pdAkhirperiode", oSPJ.DtAkhir, DbType.Date));
                    paramCollection.Add(new DBParameter("@pbtJenis", oSPJ.Jenis, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbPPKD", 0, DbType.Int32));
                    paramCollection.Add(new DBParameter("@inourutClient", oSPJ.NoUrutClient, DbType.Int64));
                    paramCollection.Add(new DBParameter("@piNoUrut", oSPJ.NoUrut, DbType.Int64));



                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);


                }

                SSQL = "DELETE tSPJRekening WHERE iNoUrut= @NoUrut";

                DBParameterCollection DeleteParem = new DBParameterCollection();
                DeleteParem.Add(new DBParameter("@NoUrut", oSPJ.NoUrut));
                _dbHelper.ExecuteNonQuery(SSQL, DeleteParem, m_connection, m_objTrans);




                foreach (SPJRekening sr in oSPJ.Rekenings)
                {

                    SSQL = "INSERT INTO tSPJRekening (iNoUrut, IDDInas,btKodeUK,  IDUrusan, IDProgram, IDkegiatan,IDSubKegiatan, IIDRekening,cJumlah," +
                    "btKodekategori, btKodeurusan, btKodeskpd,btKodekategoriPelaksana, btKodeurusanPelaksana, btIDprogram, btIDKegiatan, btIDsubkegiatan) values (" +
                          "@NoUrut, @IDDInas, @KOdeUK, @IDUrusan, @IDProgram, @IDkegiatan,@IDSubKegiatan, @IIDRekeing,@cJumlah,0,0,0,0,0,0,0,0)";
                    DBParameterCollection paramDetail = new DBParameterCollection();

                    paramDetail.Add(new DBParameter("@NoUrut", oSPJ.NoUrut, DbType.Int64));
                    paramDetail.Add(new DBParameter("@IDDInas", oSPJ.IDDinas, DbType.Int32));
                    paramDetail.Add(new DBParameter("@KOdeUK", sr.UnitKerja, DbType.Int32));
                    paramDetail.Add(new DBParameter("@IDUrusan", sr.IDUrusan, DbType.Int32));
                    paramDetail.Add(new DBParameter("@IDProgram", sr.IDProgram, DbType.Int32));
                    paramDetail.Add(new DBParameter("@IDkegiatan", sr.IDKegiatan, DbType.Int32));
                    paramDetail.Add(new DBParameter("@IDSubKegiatan", sr.IDSubKegiatan, DbType.Int64));
                    paramDetail.Add(new DBParameter("@IIDRekeing", sr.IDRekening, DbType.Int64));
                    paramDetail.Add(new DBParameter("@cJumlah", sr.Jumlah, DbType.Decimal));
                    _dbHelper.ExecuteNonQuery(SSQL, paramDetail, m_connection, m_objTrans);




                }
                m_objTrans.Commit();
                m_connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                m_objTrans.Rollback();
                m_connection.Close();
                _isError = true;
                _lastError = ex.Message;
                return false;
            }
            
        }
        public bool KunciSPJ(long NoUrut)
        {
            try
            {
                //Cek apakah ada di SPP
                SPPLogic oSPPLogic = new SPPLogic(Tahun);
                SPP oSPP = new SPP();
                SSQL = "UPDATE  tSPJ set iSTatus = 1 WHERE inourut =@piNoUrut  ";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@piNoUrut", NoUrut, DbType.Int64));

                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }


        }
        public List<FungsionalDanLRA> GetListLRA(int idDInas, long idsubkegiatan, long idrekening, DateTime batasTanggal)
        {
            try
            {
                List<FungsionalDanLRA> lst = new List<FungsionalDanLRA>();

                SSQL = "select f.*  from Realisasi04AK f " +
                    " where iddinas= @DINAS AND  IDSUBKegiatan=@IDSUBKEGIATAN AND IIDREkening=@IDREKENING AND dtBukukas<=@TANGGAL " +
                    "Order by dtBukukas,iNoUrut ";

                    
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@DINAS", idDInas));
                paramCollection.Add(new DBParameter("@TANGGAL", batasTanggal,DbType.Date));
                paramCollection.Add(new DBParameter("@IDSUBKEGIATAN", idsubkegiatan));
                paramCollection.Add(new DBParameter("@IDREKENING", idrekening));


                DataTable dt = new DataTable();

                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new FungsionalDanLRA()
                               {
                                   
                                   IDDubKegiatanLRA = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                   IDRekeningLRA = DataFormat.GetLong(dr["IIDRekening"]),
                                   Tanggal = DataFormat.GetDate(dr["dtBukukas"]),
                                   NoBukti = DataFormat.GetString(dr["NoBukti"]),
                                   NoUrut= DataFormat.GetLong(dr["iNoUrut"]),
                                   LRA = DataFormat.GetDecimal(dr["cJumlah"]),

                                   Tabel = DataFormat.GetInteger(dr["Tabel"])==1?"SPP":(
                                            DataFormat.GetInteger(dr["Tabel"])==2?"SPJ":(
                                            DataFormat.GetInteger(dr["Tabel"])==3?"Pengembalian Belanja":"Koreksi"))

                               }).ToList();
                    }
                }




                return lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;

            }

        }
        public List<FungsionalDanLRA> GetListSPJ(int idDInas, long idsubkegiatan, long idrekening, DateTime batasTanggal)
        {
            try
            {
                List<FungsionalDanLRA> lst = new List<FungsionalDanLRA>();

                SSQL = "select f.*  from vwBKUBelanja f " +
                    " where iddinas= @DINAS AND  IDSUBKegiatan=@IDSUBKEGIATAN AND IIDREkening=@IDREKENING AND dtBukti<=@TANGGAL " +
                    " Order by dtBukti,NoUrut ";


                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@DINAS", idDInas));
                paramCollection.Add(new DBParameter("@TANGGAL", batasTanggal, DbType.Date));
                paramCollection.Add(new DBParameter("@IDSUBKEGIATAN", idsubkegiatan));
                paramCollection.Add(new DBParameter("@IDREKENING", idrekening));


                DataTable dt = new DataTable();

                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new FungsionalDanLRA()
                               {

                                   Tanggal = DataFormat.GetDate(dr["dtBukti"]),
                                   NoBukti = DataFormat.GetString(dr["NoBukti"]),
                                   NoUrut = DataFormat.GetLong(dr["NoUrut"]),
                                   NoBKU = DataFormat.GetInteger(dr["NoBKUSKPD"]),
                                   BKU = DataFormat.GetDecimal(dr["cJumlah"]),

                               }).ToList();
                    }
                }




                return lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;

            }

        }
        public List<FungsionalDanLRA> PerbandinganSPJdanLRA(int idDInas, DateTime batasTanggal)
        {
            try
            {
                List<FungsionalDanLRA> lst= new List<FungsionalDanLRA>();

                SSQL = "select f.* , m.sNamaRekening, p.NamaSubKegiatan from dbo.fnGetLRAKUPerRekening(@DINAS,@TANGGAL) f " +
                    " INNER JOIN ProgramKegiatan p ON p.IDDInas= f.IDDInas and p.IDSUBKegiatan = f.IDSUbKegiatan "+
                    " INNER JOIN MRekening m on m.IIDrekening = f.IIDRekening order by f.idsubkegiatan, f.iidrekening ";

                   DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@DINAS", idDInas));
                paramCollection.Add(new DBParameter("@TANGGAL",batasTanggal,DbType.Date));

                DataTable dt = new DataTable();
                
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new FungsionalDanLRA()
                                {
                                   KdeUK = DataFormat.GetInteger(dr["UnitAnggaran"]),
                                   IDDubKegiatanLRA =DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    IDRekeningLRA=DataFormat.GetLong(dr["IIDRekening"]),
                                    IDSubKegiatanF = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                   
                                   IDRekeningF= DataFormat.GetLong(dr["IIDRekening"]),

                                   LRA = DataFormat.GetDecimal(dr["LRA"]),
                                    BKU= DataFormat.GetDecimal(dr["BKU"]),
                                    NamaRekening = DataFormat.GetString(dr["sNamaRekening"]),
                                    NamaSubKegiatan = DataFormat.GetString(dr["NamaSubKegiatan"]),


                                }).ToList();
                    }
                }




                return lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;

            }
        }
        public bool Hapus(SPJ oSPJ)
        {
            try
            {
                //Cek apakah ada di SPP
                SPPLogic oSPPLogic = new SPPLogic(Tahun);
                SPP oSPP = new SPP();

                //if (oSPPLogic.ApaAdaDiSPP(oSPJ.NoUrut) == true)
                //{
                //    _lastError = "Sudah dipakai di SPP No..";
                //    return false;

                //}
                SSQL = "DELETE tSPJ WHERE inourut =@piNoUrut  ";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@piNoUrut", oSPJ.NoUrut, DbType.Int64));



                if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
                {
                    SSQL = "DELETE tSPJRekening WHERE iNoUrut= @NoUrut";
                    DBParameterCollection DeleteParem = new DBParameterCollection();
                    DeleteParem.Add(new DBParameter("@NoUrut", oSPJ.NoUrut));
                    _dbHelper.ExecuteNonQuery(SSQL, DeleteParem);

                    SSQL = "UPDATE tPanjar Set  inourutSPJUP =0, iSTatus = 0 where inourutSPJUP  =@NoUrut  ";
                    _dbHelper.ExecuteNonQuery(SSQL, DeleteParem);
                    SSQL = "UPDATE tKoreksi Set  inourutSPJUP =0, iSTatus = 0 where inourutSPJUP  =@NoUrut  ";
                    _dbHelper.ExecuteNonQuery(SSQL, DeleteParem);

                }

                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
           

        }
     
        public List<SPJ> GetByDInasAndJenis(int iddinas, int iJenis)
        {
            //KontrakRekening oKontrak = new KontrakRekening();
             
               

            List<SPJ> _lst = new List<SPJ>();
            try
            {
                SSQL = "SELECT tspj.* FROM TSPJ WHERE iTahun = @Tahun and iddinas = @Dinas AND btJenis=  @Jenis order by dtBukukas";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@Tahun", Tahun));
                paramCollection.Add(new DBParameter("@Dinas", iddinas));
                paramCollection.Add(new DBParameter("@Jenis", iJenis));
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL,paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SPJ()
                                {
                                    NoUrut = DataFormat.GetLong(dr["iNourut"]),
                                    NoSPJ = DataFormat.GetString(dr["sNoBukti"]),
                                    DtSPJ= DataFormat.GetDate(dr["dtbukukas"]),
                                    Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                    DtAwal= DataFormat.GetDateTime(dr["dAwalPeriode"]),
                                    DtAkhir = DataFormat.GetDateTime(dr["dAkhirPeriode"]),
                                    Status = DataFormat.GetInteger(dr["iStatus"])
                                                                                              
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
        public List<BelanjaLPJUP> GetBelanjaUntukLPJ(
           int iddinas,
           DateTime tanggalAwal,
           DateTime tanggalAkhir,
           long NoUrutSPJ = 0 // 0 untuk Baru

           )
        {
            List<BelanjaLPJUP> lst = new List<BelanjaLPJUP>();
            try
            {

                PengeluaranLogic oLogic = new PengeluaranLogic(Tahun);
                List<PengeluaranDanRekening> lstPengeluaran = new List<PengeluaranDanRekening>();
                ParameterBendahara pb = new ParameterBendahara(Tahun);
                pb.IDDInas = iddinas;// ctrlSKPD1.GetID();
                pb.NoUrutSPJ = NoUrutSPJ;
                pb.TanggalAwal = tanggalAwal;
                pb.TanggalAkhir = tanggalAkhir;
                int Jenis = 0;

                pb.Jenis = Jenis;
                lstPengeluaran = oLogic.GetUntukLPJ(pb);
                foreach (PengeluaranDanRekening p in lstPengeluaran)
                {
                    BelanjaLPJUP b = new BelanjaLPJUP();
                    b.Sumber = 1;
                    b.NoUrut = p.NoUrut;
                    b.KeteranganBelanja = p.Uraian;
                    b.Nilai = p.Jumlah;
                    b.Kode = p.NoBukti;
                    b.IDRekening = p.IDRekening;
                    b.KodeUK = p.KodeUK;
                    b.IDProgram = p.IDProgram;
                    b.IDkegiatan = p.IDKegiatan;
                    b.IDSubKegiatan = p.IDSUbKegiatan;
                    b.Tanggal = p.Tanggal;
                    b.Jumlah = p.Jumlah;
                    b.NamaRekening = p.Nama;



                    lst.Add(b);
                }

                return lst;

            }
            catch (Exception ex)
            {

                _lastError = ex.Message;
                return null;


            }
        }
       
        public List<BelanjaLPJUP> GetBelanjaUntukLPJTU(
            int iddinas,
            long noUrutP2d,
            long NoUrutSPJ=0 // 0 untuk Baru

            )
        {
            List<BelanjaLPJUP> lst = new List<BelanjaLPJUP>();
            try
            {

                PengeluaranLogic oLogic = new PengeluaranLogic(Tahun);
                List<PengeluaranDanRekening> lstPengeluaran = new List<PengeluaranDanRekening>();
                 ParameterBendahara pb = new ParameterBendahara(Tahun);
                 pb.IDDInas = iddinas;// ctrlSKPD1.GetID();
                 pb.NoUrutSPJ = NoUrutSPJ;
                 pb.NoUrutSP2D = noUrutP2d;
                 int Jenis=0;
                 
                 pb.Jenis = Jenis;
                 lstPengeluaran = oLogic.GetUntukLPJ(pb);
                 foreach (PengeluaranDanRekening p in lstPengeluaran)
                 {
                        BelanjaLPJUP b = new BelanjaLPJUP();
                        b.Sumber = 1;
                        b.NoUrut = p.NoUrut;
                        b.KeteranganBelanja = p.Uraian;
                        b.Nilai = p.Jumlah;
                        b.Kode = p.NoBukti;
                        b.IDRekening = p.IDRekening;
                        b.KodeUK =p.KodeUK;
                        b.IDProgram =p.IDProgram;
                        b.IDkegiatan =p.IDKegiatan ;
                        b.IDSubKegiatan =p.IDSUbKegiatan ;
                        b.Tanggal = p.Tanggal;
                        b.Jumlah = p.Jumlah;
                        b.NamaRekening = p.Nama;
                       


                        lst.Add(b);
                    }

                 return lst;

            }
            catch (Exception ex)
            {
                
                _lastError = ex.Message;
                return null;


            }
        }
       
        public List<FungsionalRekening> GetFungsionalDariBKU(int iddinas, DateTime tanggalAwal, DateTime tanggalAkhir)
        {
            List<FungsionalRekening> lst = new List<FungsionalRekening>();
            try
            {
                DateTime tanggalAwalTahun = new DateTime(Tahun,1,1);
                ////SSQL = " Select IDDInas, UnitAnggaran as btKodeUK, IDurusan,IDprogram,IDKegiatan,IDSubKegiatan,IIDrekening, " +
                ////     " case when dtbukti >= @TANGGALAWALTAHUN  and dtbukti < @TANGGALAWAL  and cJenisBelanja =4  Then  SUM(-1* (iDebet * cJumlah)) else 0 end as gajilalu," +
                ////     " case when dtbukti >= @TANGGALAWAL  and dtbukti <= @TANGGALAKHIR  and cJenisBelanja =4 Then SUM(-1* (iDebet * cJumlah)) else 0 end as gajikini," +
                ////     " case when dtbukti >= @TANGGALAWALTAHUN and dtbukti < @TANGGALAWAL  and cJenisBelanja =3 Then SUM(-1* (iDebet * cJumlah)) else 0 end as lslalu," +
                ////     " case when dtbukti >= @TANGGALAWAL  and dtbukti <= @TANGGALAKHIR  and cJenisBelanja =3 Then SUM(-1* (iDebet * cJumlah)) else 0 end as lskini," +
                ////     " case when dtbukti >= @TANGGALAWALTAHUN  and dtbukti < @TANGGALAWAL  and cJenisBelanja <3 Then SUM(-1* (iDebet * cJumlah)) else 0 end as uplalu," +
                ////     " case when dtbukti >= @TANGGALAWAL  and dtbukti <= @TANGGALAKHIR  and cJenisBelanja <3 Then SUM(-1* (iDebet * cJumlah)) else 0 end as upkini " +
                ////    " from vwBKUBelanja WHERE IDDInas =@DINAS " + 
                ////    " AND inourut=24072090100044699" +
                ////    " group by IDDInas, UnitAnggaran , IDurusan,IDprogram,IDKegiatan,IDSubKegiatan,IIDrekening, dtBukti, cJenisBelanja";
                ////     //" group by IDDInas, btKodeUK, IDurusan,IDprogram,IDKegiatan,IDSubKegiatan,IIDrekening,dtbukti,cJenisBelanja";
                ////DBParameterCollection paramCollection = new DBParameterCollection();

                ////paramCollection.Add(new DBParameter("@TANGGALAWALTAHUN", tanggalAwalTahun, DbType.Date));
                ////paramCollection.Add(new DBParameter("@TANGGALAWAL", tanggalAwal,DbType.Date));
                ////paramCollection.Add(new DBParameter("@TANGGALAKHIR", tanggalAkhir, DbType.Date));
                ////paramCollection.Add(new DBParameter("@DINAS", iddinas));

                SSQL = " Select IDDInas, UnitAnggaran as btKodeUK, IDurusan,IDprogram,IDKegiatan,IDSubKegiatan,IIDrekening, " +
                     " case when dtbukti >=" +tanggalAwalTahun.ToSQLFormat() + "  and dtbukti < " +tanggalAwal.ToSQLFormat() + "  and cJenisBelanja =4  Then  SUM(-1* (iDebet * cJumlah)) else 0 end as gajilalu," +
                     " case when dtbukti >= " +tanggalAwal.ToSQLFormat() + "  and dtbukti <= " +tanggalAkhir.ToSQLFormat() + "  and cJenisBelanja =4 Then SUM(-1* (iDebet * cJumlah)) else 0 end as gajikini," +
                     " case when dtbukti >=" +tanggalAwalTahun.ToSQLFormat() + " and dtbukti < " +tanggalAwal.ToSQLFormat() + "  and cJenisBelanja =3 Then SUM(-1* (iDebet * cJumlah)) else 0 end as lslalu," +
                     " case when dtbukti >= " +tanggalAwal.ToSQLFormat() + "  and dtbukti <= " +tanggalAkhir.ToSQLFormat() + "  and cJenisBelanja =3 Then SUM(-1* (iDebet * cJumlah)) else 0 end as lskini," +
                     " case when dtbukti >=" +tanggalAwalTahun.ToSQLFormat() + "  and dtbukti < " +tanggalAwal.ToSQLFormat() + "  and cJenisBelanja <3 Then SUM(-1* (iDebet * cJumlah)) else 0 end as uplalu," +
                     " case when dtbukti >= " +tanggalAwal.ToSQLFormat() + "  and dtbukti <= " +tanggalAkhir.ToSQLFormat() + "  and cJenisBelanja <3 Then SUM(-1* (iDebet * cJumlah)) else 0 end as upkini " +
                    " from vwBKUBelanja WHERE IDDInas =" + iddinas.ToString() + " " +
                                       // " AND idsubkegiatan =325042040002" +
                    " group by IDDInas, UnitAnggaran , IDurusan,IDprogram,IDKegiatan,IDSubKegiatan,IIDrekening, dtBukti, cJenisBelanja";

                //" group by IDDInas, btKodeUK, IDurusan,IDprogram,IDKegiatan,IDSubKegiatan,IIDrekening,dtbukti,cJenisBelanja";
              

                DataTable dt = new DataTable();
                List<FungsionalRekening> _lst = new List<FungsionalRekening>();


                dt = _dbHelper.ExecuteDataTable(SSQL);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{ 
                        _lst = (from DataRow dr in dt.Rows
                                select new FungsionalRekening()
                                {
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    GL =DataFormat.GetDecimal(dr["GajiLalu"]),
                                    GK = DataFormat.GetDecimal(dr["gajikini"]),
                                    BL = DataFormat.GetDecimal(dr["lslalu"]),
                                    BK = DataFormat.GetDecimal(dr["lskini"]),
                                    UL = DataFormat.GetDecimal(dr["uplalu"]),
                                    UK = DataFormat.GetDecimal(dr["upkini"]),
                            }).ToList();
                    }
                }

                return _lst;
            }
            catch (Exception ex)
            {

            }
            return lst;

        }
  
        public List<FungsionalRekening> GetFungsionalPenerimaanPengeluarandariBKU(int iddinas, DateTime tanggalAwal, DateTime tanggalAkhir)
        {
            List<FungsionalRekening> lst = new List<FungsionalRekening>();
            try
            {


                //SSQL = " Select 1 as NoUrut,1 as IDrekening, 'SP2D' as Nama, " +
                //     " case when dtbukti >= @TANGGALAWALTAHUN  and dtbukti < @TANGGALAWAL  and cJenisBelanja =4  Then  cJumlah else 0 end  as  gajilalu," +
                //     " case when dtbukti >= @TANGGALAWAL  and dtbukti <= @TANGGALAKHIR  and cJenisBelanja =4 Then  cJumlah else 0   end as gajikini," +
                //     " case when dtbukti >= @TANGGALAWALTAHUN and dtbukti < @TANGGALAWAL  and cJenisBelanja =3 Then  cJumlah else 0 end  as  lslalu," +
                //     " case when dtbukti >= @TANGGALAWAL  and dtbukti <= @TANGGALAKHIR  and cJenisBelanja =3 Then  cJumlah else 0 end  as  lskini," +
                //     " case when dtbukti >= @TANGGALAWALTAHUN  and dtbukti < @TANGGALAWAL  and cJenisBelanja <3 Then  cJumlah else   0  end as  uplalu," +
                //     " case when dtbukti >= @TANGGALAWAL  and dtbukti <= @TANGGALAKHIR  and cJenisBelanja <3 Then  cJumlah else 0 end  as  upkini " +
                //    " from tBKU WHERE IDDInas =@DINAS and tBKU.JenisSumber = 1  and tBKU.iDebet= 1 and btJenisBendahara = 2";//

                //SSQL = SSQL + " UNION ALL  Select 2 as NoUrut,tBKURekening.IIDrekening as idRekening, mPotongan.sNamaPotongan as Nama, " +
                //     " case when tBKU.dtbukti >= @TANGGALAWALTAHUN  and tBKU.dtbukti < @TANGGALAWAL  and tBKU.cJenisBelanja =4  Then  tBKURekening.cJumlah else 0 end as   gajilalu," +
                //     " case when tBKU.dtbukti >= @TANGGALAWAL  and tBKU.dtbukti <= @TANGGALAKHIR  and tBKU.cJenisBelanja =4 Then  tBKURekening.cJumlah else 0 end as   gajikini," +
                //     " case when tBKU.dtbukti >= @TANGGALAWALTAHUN and tBKU.dtbukti < @TANGGALAWAL  and tBKU.cJenisBelanja =3 Then  tBKURekening.cJumlah else 0 end as   lslalu," +
                //     " case when tBKU.dtbukti >= @TANGGALAWAL  and tBKU.dtbukti <= @TANGGALAKHIR  and tBKU.cJenisBelanja =3 Then  tBKURekening.cJumlah else 0 end as   lskini," +
                //     " case when tBKU.dtbukti >= @TANGGALAWALTAHUN  and tBKU.dtbukti < @TANGGALAWAL  and tBKU.cJenisBelanja <3 Then  tBKURekening.cJumlah else 0 end as  uplalu," +
                //     " case when tBKU.dtbukti >= @TANGGALAWAL  and tBKU.dtbukti <= @TANGGALAKHIR  and tBKU.cJenisBelanja <3 Then  tBKURekening.cJumlah else 0 end as   upkini " +
                //    " from tBKU INNER JOIN tBKUREkening On tBKU.inourut = tBKURekening.inourut " +
                //    " inner join mPotongan on mPotongan.IIDrekeningPotongan = tBKURekening.IIDrekening WHERE tBKU.IDDInas =@DINAS and btJenisBendahara = 2  and tBKU.iDebet = 1";//

                //SSQL = SSQL + " UNION ALL Select 4 as NoUrut,1 as IDrekening, 'SPJ' as Nama, " +
                //     " case when dtbukti >= @TANGGALAWALTAHUN  and dtbukti < @TANGGALAWAL  and cJenisBelanja =4 and tBKU.JenisSumber = 1  Then  cJumlah else 0 end  as  gajilalu," +
                //     " case when dtbukti >= @TANGGALAWAL  and dtbukti <= @TANGGALAKHIR  and cJenisBelanja =4 and tBKU.JenisSumber = 1 Then  cJumlah else 0   end as gajikini," +
                //     " case when dtbukti >= @TANGGALAWALTAHUN and dtbukti < @TANGGALAWAL  and cJenisBelanja =3 and tBKU.JenisSumber = 1  Then  cJumlah else 0 end  as  lslalu," +
                //     " case when dtbukti >= @TANGGALAWAL  and dtbukti <= @TANGGALAKHIR  and cJenisBelanja =3 Then  cJumlah else 0 end  as  lskini," +
                //     " case when dtbukti >= @TANGGALAWALTAHUN  and dtbukti < @TANGGALAWAL  and cJenisBelanja <3  and JenisSUmber in (5,25)  Then  cJumlah else   0  end as  uplalu," +
                //     " case when dtbukti >= @TANGGALAWAL  and dtbukti <= @TANGGALAKHIR  and cJenisBelanja <3  and JenisSUmber in (5,25) Then  cJumlah else 0 end  as  upkini " +
                //    " from tBKU WHERE IDDInas =@DINAS  and tBKU.JenisSumber = 1 and tBKU.iDebet= -1 and btJenisBendahara = 2 ";//

                //SSQL = SSQL + " UNION ALL  Select 5 as NoUrut,tBKURekening.IIDrekening as idRekening, mPotongan.sNamaPotongan as Nama, " +
                //     " case when tBKU.dtbukti >= @TANGGALAWALTAHUN  and tBKU.dtbukti < @TANGGALAWAL  and tBKU.cJenisBelanja =4  Then  tBKURekening.cJumlah else 0 end as   gajilalu," +
                //     " case when tBKU.dtbukti >= @TANGGALAWAL  and tBKU.dtbukti <= @TANGGALAKHIR  and tBKU.cJenisBelanja =4 Then  tBKURekening.cJumlah else 0 end as   gajikini," +
                //     " case when tBKU.dtbukti >= @TANGGALAWALTAHUN and tBKU.dtbukti < @TANGGALAWAL  and tBKU.cJenisBelanja =3 Then  tBKURekening.cJumlah else 0 end as   lslalu," +
                //     " case when tBKU.dtbukti >= @TANGGALAWAL  and tBKU.dtbukti <= @TANGGALAKHIR  and tBKU.cJenisBelanja =3 Then  tBKURekening.cJumlah else 0 end as   lskini," +
                //     " case when tBKU.dtbukti >= @TANGGALAWALTAHUN  and tBKU.dtbukti < @TANGGALAWAL  and tBKU.cJenisBelanja <3 Then  tBKURekening.cJumlah else 0 end as  uplalu," +
                //     " case when tBKU.dtbukti >= @TANGGALAWAL  and tBKU.dtbukti <= @TANGGALAKHIR  and tBKU.cJenisBelanja <3 Then  tBKURekening.cJumlah else 0 end as   upkini " +
                //    " from tBKU INNER JOIN tBKUREkening On tBKU.inourut = tBKURekening.inourut " +
                //    " inner join mPotongan on mPotongan.IIDrekeningPotongan = tBKURekening.IIDrekening WHERE tBKU.IDDInas =@DINAS  and btJenisBendahara = 2  and tBKU.iDebet = -1 ";//



                DateTime tanggalAwalTahun = new DateTime(Tahun, 1, 1);
                SSQL = "SELECT * from dbo.fnPenerimaanDanPengeluaran (@TANGGALAWALTAHUN,@TANGGALAWAL,@TANGGALAKHIR,@DINAS) order by NoUrut, IDrekening";
                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("@TANGGALAWALTAHUN", tanggalAwalTahun, DbType.Date));
                paramCollection.Add(new DBParameter("@TANGGALAWAL", tanggalAwal,DbType.Date));
                paramCollection.Add(new DBParameter("@TANGGALAKHIR", tanggalAkhir, DbType.Date));
                paramCollection.Add(new DBParameter("@DINAS", iddinas));

                DataTable dt = new DataTable();
                List<FungsionalRekening> _lst = new List<FungsionalRekening>();


                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new FungsionalRekening()
                                {
                                    IDDInas = DataFormat.GetInteger(dr["NoUrut"]),
                                    IDRekening = DataFormat.GetLong(dr["IDRekening"]),
                                    Uraian = DataFormat.GetString(dr["nama"]),
                                    GL =DataFormat.GetDecimal(dr["GajiLalu"]),
                                    GK = DataFormat.GetDecimal(dr["gajikini"]),
                                    BL = DataFormat.GetDecimal(dr["lslalu"]),
                                    BK = DataFormat.GetDecimal(dr["lskini"]),
                                    UL = DataFormat.GetDecimal(dr["uplalu"]),
                                    UK = DataFormat.GetDecimal(dr["upkini"]),
                            }).ToList();
                    }
                }

                return _lst;
            }
            catch (Exception ex)
            {

            }
            return lst;

        }
        public List<SPJRekening> GetSPJRekening(long noUrut, long noUrutSPD)
        {
            List<SPJRekening> _lst = new List<SPJRekening>();
            try
            {

                //SSQL = "select sum(tSpdKegiatan.cjumlah) as JumlahSPD, TSPJRekening.inourut,TSPJRekening.IDUrusan, TSPJRekening.idProgram,TSPJRekening.idkegiatan,  TSPJRekening.idsubkegiatan, tSPJRekening.iIDRekening, tSPJRekening.cJumlah, mRekening.sNamaRekening as Nama " +
                //        " FROM TSPJRekening inner join mRekening on tSPJRekening.IIDrekening = mRekening.IIDRekening " +
                //        " full outer join tSPDKegiatan on tSPDKegiatan.idsubkegiatan = tSPJRekening.idsubkegiatan and " +
                //        " tSPDKegiatan.iddinas = tSPJRekening.iddinas and  tSPDKegiatan.iIDRekening = tSPJRekening.iIDRekening " +
                //        " WHERE tSPJRekening.inourut = @NoUrut and tSPDKegiatan.inourut <= @nourutSPD" +
                //        " group by TSPJRekening.inourut,TSPJRekening.IDUrusan, TSPJRekening.idProgram,TSPJRekening.idkegiatan,  TSPJRekening.idsubkegiatan, tSPJRekening.iIDRekening, tSPJRekening.cJumlah, mRekening.sNamaRekening";

                SSQL = "select TSPJRekening.cJumlah as JumlahSPD, TSPJRekening.inourut,TSPJRekening.IDUrusan, TSPJRekening.idProgram,TSPJRekening.idkegiatan,  TSPJRekening.idsubkegiatan, tSPJRekening.iIDRekening, tSPJRekening.cJumlah, mRekening.sNamaRekening as Nama " +
                        " FROM TSPJRekening inner join mRekening on tSPJRekening.IIDrekening = mRekening.IIDRekening " +
          //              " full outer join tSPDKegiatan on tSPDKegiatan.idsubkegiatan = tSPJRekening.idsubkegiatan and " +
            //            " tSPDKegiatan.iddinas = tSPJRekening.iddinas and  tSPDKegiatan.iIDRekening = tSPJRekening.iIDRekening " +
                        " WHERE tSPJRekening.inourut = @NoUrut " +
                        " group by TSPJRekening.inourut,TSPJRekening.IDUrusan, TSPJRekening.idProgram,TSPJRekening.idkegiatan,  TSPJRekening.idsubkegiatan, tSPJRekening.iIDRekening, tSPJRekening.cJumlah, mRekening.sNamaRekening";


                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@NoUrut", noUrut));
                paramCollection.Add(new DBParameter("@nourutSPD",noUrutSPD));

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SPJRekening()
                                {
                                    NoUrut = DataFormat.GetLong(dr["iNourut"]),
                                    IDUrusan= DataFormat.GetInteger (dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    JumlahSPD= DataFormat.GetDecimal(dr["JumlahSPD"]),
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
