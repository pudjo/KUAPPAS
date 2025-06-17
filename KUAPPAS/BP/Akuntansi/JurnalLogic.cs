using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Akuntansi;
using BP;
using DataAccess;
using System.Data;
using Formatting;


namespace BP.Akuntansi
{
    public class JurnalLogic:BP 
    {
        public JurnalLogic(int tahun)
            : base(tahun)
        {

        }
        public List<JurnalRekeningShow> Get(int dinas, int ppkd,
            int jenissumber, DateTime tanggalawal, DateTime tanggalakhir, int potongan =0 )
        {
            try
            {
                List<JurnalRekeningShow> lst = new List<JurnalRekeningShow>();
                SSQL="";
                DBParameterCollection paramCollection = new DBParameterCollection();

                if (potongan == 0)
                {

                    SSQL = " Select mSKPD.sNamaSKPD,tJurnalRekening.* ,tJurnal.inojurnal , tJurnal.dtJurnal,tJurnalRekening.sKeterangan, tJurnal.sNoBukti, mRekening.snamaRekening,tJurnal.dttanggalbukti  FROM tJurnalRekening INNER JOIN mRekening ON tJurnalRekening.IIDRekening " +
                          " = mRekening.IIDRekening INNER JOIN tJurnal ON tJurnalRekening.inojurnal = tJurnal.inojurnal " +
                          "  INNER JOIN mSKPD on mSKPD.ID= tJurnal.IDDInas  WHERE  bPotongan =0 ";
                    
                    if (ppkd > -1)
                    {
                        SSQL = SSQL + " AND tJurnal.bPPKD=@PPKD";
                        paramCollection.Add(new DBParameter("@PPKD", ppkd));
                    }
                    if (dinas > 0)
                    {

                        SSQL = SSQL + " AND tJurnal.IDDInas=@DINAS";
                        paramCollection.Add(new DBParameter("@DINAS", dinas));

                    }


                    SSQL = SSQL + " AND (dttanggalbukti between @TANGGALAWAL AND  @TANGGALAKHIR)";
                    paramCollection.Add(new DBParameter("@TANGGALAWAL", tanggalawal, DbType.Date));
                    paramCollection.Add(new DBParameter("@TANGGALAKHIR", tanggalakhir, DbType.Date));

                    SSQL = SSQL + " AND tJurnal.btJenisJurnal <> 10 AND tJurnal.iJenisSumber = @JENISSUMBER ";
                    paramCollection.Add(new DBParameter("@JENISSUMBER", jenissumber));


                    SSQL = SSQL + " ORDER BY dttanggalbukti, tJurnalRekening.inojurnal,tJurnalRekening.idebet desc, tJurnalRekening.iKelompok,tJurnalRekening.inourut,tJurnalRekening.IIDRekening  ";
                }
                else
                {
                    // pajak 
                    SSQL = " Select mSKPD.sNamaSKPD,tJurnalRekening.* , tJurnal.inojurnal , tJurnal.dtJurnal,tJurnalRekening.sKeterangan, tJurnal.sNoBukti, mRekening.snamaRekening,tJurnal.dttanggalbukti  FROM tJurnalRekening INNER JOIN mRekening ON tJurnalRekening.IIDRekening " +
                          " = mRekening.IIDRekening INNER JOIN tJurnal ON tJurnalRekening.inojurnal = tJurnal.inojurnal " +
                          "  INNER JOIN mSKPD on mSKPD.ID= tJurnal.IDDInas   WHERE bPotongan =1 ";
                    
                    
                    if (dinas > 0)
                    {

                        SSQL = SSQL + " AND tJurnal.IDDInas=@DINAS";
                        paramCollection.Add(new DBParameter("@DINAS", dinas));

                    }


                    SSQL = SSQL + " AND (dttanggalbukti between @TANGGALAWAL AND  @TANGGALAKHIR)";
                    paramCollection.Add(new DBParameter("@TANGGALAWAL", tanggalawal, DbType.Date));
                    paramCollection.Add(new DBParameter("@TANGGALAKHIR", tanggalakhir, DbType.Date));

                    SSQL = SSQL + " AND tJurnal.btJenisJurnal <> 10 AND tJurnal.iJenisSumber in (3,6) ";
                    paramCollection.Add(new DBParameter("@JENISSUMBER", jenissumber));


                    SSQL = SSQL + " ORDER BY dttanggalbukti, tJurnalRekening.inojurnal,tJurnalRekening.idebet desc, tJurnalRekening.iKelompok,tJurnalRekening.inourut,tJurnalRekening.IIDRekening  ";
                }
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new JurnalRekeningShow()
                               {

                                   
                                   NoJurnal = DataFormat.GetLong(dr["InoJurnal"]),
                                   Tanggal = DataFormat.GetDate(dr["dttanggalbukti"]),
                                   NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                   Debet = DataFormat.GetInteger(dr["iDebet"]),
                                   Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                   Keterangan  = DataFormat.GetString(dr["sketerangan"]),
                                   NamaRekening = DataFormat.GetString(dr["sNamaRekening"]),
                                   IIDRekening = DataFormat.GetLong(dr["iidrekening"]),
                                   NamaSKPD = DataFormat.GetString(dr["sNamaSKPD"]),

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
        public List<JurnalRekeningShow> GetByJenis(int dinas, int ppkd,
            int jenis, DateTime tanggalawal, DateTime tanggalakhir, int potongan = 0)
        {
            try
            {
                List<JurnalRekeningShow> lst = new List<JurnalRekeningShow>();
                SSQL = "";
                DBParameterCollection paramCollection = new DBParameterCollection();

                //if (potongan == 0)
                //{

                    SSQL = " Select mSKPD.sNamaSKPD,tJurnalRekening.* ,tJurnal.inojurnal , tJurnal.dtJurnal,tJurnalRekening.sKeterangan, tJurnal.sNoBukti, mRekening.snamaRekening,tJurnal.dttanggalbukti  FROM tJurnalRekening INNER JOIN mRekening ON tJurnalRekening.IIDRekening " +
                          " = mRekening.IIDRekening INNER JOIN tJurnal ON tJurnalRekening.inojurnal = tJurnal.inojurnal " +
                          " INNER JOIN mSKPD on mSKPD.ID= tJurnal.IDDInas  WHERE  bPotongan =0 ";

                    if (ppkd > -1)
                    {
                        SSQL = SSQL + " AND tJurnal.bPPKD=@PPKD";
                        paramCollection.Add(new DBParameter("@PPKD", ppkd));
                    }
                    if (dinas > 0)
                    {

                        SSQL = SSQL + " AND tJurnal.IDDInas=@DINAS";
                        paramCollection.Add(new DBParameter("@DINAS", dinas));

                    }


                    SSQL = SSQL + " AND (dttanggalbukti between @TANGGALAWAL AND  @TANGGALAKHIR)";
                    paramCollection.Add(new DBParameter("@TANGGALAWAL", tanggalawal, DbType.Date));
                    paramCollection.Add(new DBParameter("@TANGGALAKHIR", tanggalakhir, DbType.Date));

                    SSQL = SSQL + " AND tJurnal.btJenisJurnal= @JENISSUMBER ";
                    paramCollection.Add(new DBParameter("@JENISSUMBER", jenis));


                    SSQL = SSQL + " ORDER BY dttanggalbukti, tJurnalRekening.inojurnal,tJurnalRekening.idebet desc, tJurnalRekening.iKelompok,tJurnalRekening.inourut,tJurnalRekening.IIDRekening  ";
                //}
                //else
                //{
                //    // pajak 
                //    SSQL = " Select tJurnalRekening.*,tJurnal.inojurnal , tJurnal.dtJurnal,tJurnalRekening.sKeterangan, tJurnal.sNoBukti, mRekening.snamaRekening,tJurnal.dttanggalbukti  FROM tJurnalRekening INNER JOIN mRekening ON tJurnalRekening.IIDRekening " +
                //          " = mRekening.IIDRekening INNER JOIN tJurnal ON tJurnalRekening.inojurnal = tJurnal.inojurnal " +
                //          "  WHERE bPotongan =1 ";


                //    if (dinas > 0)
                //    {

                //        SSQL = SSQL + " AND tJurnal.IDDInas=@DINAS";
                //        paramCollection.Add(new DBParameter("@DINAS", dinas));

                //    }


                //    SSQL = SSQL + " AND (dttanggalbukti between @TANGGALAWAL AND  @TANGGALAKHIR)";
                //    paramCollection.Add(new DBParameter("@TANGGALAWAL", tanggalawal, DbType.Date));
                //    paramCollection.Add(new DBParameter("@TANGGALAKHIR", tanggalakhir, DbType.Date));

                //    SSQL = SSQL + " AND tJurnal.btJenis  <> 10 AND tJurnal.iJenisSumber in (3,6) ";
                //    paramCollection.Add(new DBParameter("@JENISSUMBER", jenis));


                //    SSQL = SSQL + " ORDER BY dttanggalbukti, tJurnalRekening.inojurnal,tJurnalRekening.idebet desc, tJurnalRekening.iKelompok,tJurnalRekening.inourut,tJurnalRekening.IIDRekening  ";
                //}
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new JurnalRekeningShow()
                               {


                                   NoJurnal = DataFormat.GetLong(dr["InoJurnal"]),
                                   Tanggal = DataFormat.GetDate(dr["dttanggalbukti"]),
                                   NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                   Debet = DataFormat.GetInteger(dr["iDebet"]),
                                   Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                   Keterangan = DataFormat.GetString(dr["sketerangan"]),
                                   NamaRekening = DataFormat.GetString(dr["sNamaRekening"]),
                                   IIDRekening = DataFormat.GetLong(dr["iidrekening"]),
                                   NamaSKPD = DataFormat.GetString(dr["sNamaSKPD"]),

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
        public List<Jurnal> GetByJenis(int dinas, int jenis ,
            DateTime tanggalawal, DateTime tanggalakhir)
        {
            try
            {
                List<Jurnal> lst = new List<Jurnal>();
                SSQL = "";
                DBParameterCollection paramCollection = new DBParameterCollection();



                SSQL = " Select tJurnal.*  FROM tJurnal " +
                      " WHERE 1>0 ";

                
                if (dinas > 0)
                {

                    SSQL = SSQL + " AND tJurnal.IDDInas=@DINAS";
                    paramCollection.Add(new DBParameter("@DINAS", dinas));

                }
              

                SSQL = SSQL + " AND (dttanggalbukti between @TANGGALAWAL AND  @TANGGALAKHIR)";
                paramCollection.Add(new DBParameter("@TANGGALAWAL", tanggalawal, DbType.Date));
                paramCollection.Add(new DBParameter("@TANGGALAKHIR", tanggalakhir, DbType.Date));

               



                SSQL = SSQL + " ORDER BY dttanggalbukti, tJurnal.inojurnal";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new Jurnal()
                               {

                                   IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                   KodeUK=DataFormat.GetInteger(dr["btKodeUK"]),
                                   NoJurnal = DataFormat.GetLong(dr["InoJurnal"]),
                                   TanggalBukti = DataFormat.GetDate(dr["dttanggalbukti"]),
                                   NoBukti = DataFormat.GetString(dr["sNoBukti"]),

                                   Uraian = DataFormat.GetString(dr["sketerangan"]),
                                   JenisSumber = DataFormat.GetInteger(dr["iJenisSumber"]),

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
        public List<JurnalPajak> GetPajakUntukJurnal(int Tahun, int dinas,
             DateTime tanggalawal, DateTime tanggalakhir)
        {
            try
            {
                List<JurnalPajak> lst = new List<JurnalPajak>();
                SSQL = "";
                DBParameterCollection paramCollection = new DBParameterCollection();



                SSQL = " Select *  FROM dbo.fnGetPotongan(@TAHUN,@IDDINAS, @TANGGALAWAL , @TANGGALAKHIR)  ORDER BY  Tanggal , kelompok,sNoBukti";

                paramCollection.Add(new DBParameter("@TAHUN", Tahun));
                paramCollection.Add(new DBParameter("@IDDINAS", dinas));
                paramCollection.Add(new DBParameter("@TANGGALAWAL", tanggalawal, DbType.Date));
                paramCollection.Add(new DBParameter("@TANGGALAKHIR", tanggalakhir, DbType.Date));





                
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new JurnalPajak()
                               {


                                    NoUrut= DataFormat.GetLong(dr["NoUrut"]),
                                   Tanggal = DataFormat.GetDate(dr["tanggal"]),
                                   NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                   Kelompok =  DataFormat.GetInteger(dr["Kelompok"]),
                                    Pungut = DataFormat.GetDecimal(dr["jumlahpungut"]),
                                   Setor = DataFormat.GetDecimal(dr["JumlahSetor"]),
                                   Keterangan = DataFormat.GetString(dr["sketerangan"]),
                                    Dijurnal = DataFormat.GetInteger(dr["dijurnal"]),
                              



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
        public List<SelisihTrxBB> GetSelisihTrxBB(long IDRekening,
                                                   int dinas,
                                                    DateTime tanggal)
        {

            try{

                List<SelisihTrxBB> lst = new List<SelisihTrxBB>();
                if (IDRekening.ToString().Substring(0,1)=="4"){
                    SSQL= "Select RealisasiSTS.inourut as NoUrut,RealisasiSTS.NoBukti,RealisasiSTS.dtBukukas,  RealisasiSTS.cJumlah as Trx , " + 
                          " (Select sum(cJumlah) from tBukuBesar where iSUmber = Realisasists.inourut and iIDRekening= Realisasists.IIDRekening)  as BB "+
                           " from RealisasiSTS Where RealisasiSTS.dtBukukas <=@TANGGAL and RealisasiSTS.IDDInas= @DINAS and RealisasiSTS.IIDRekening= @REKENING ";

                } else {
                    SSQL = "Select REALISASI04AK.inourut as NoUrut,REALISASI04AK.NoBukti,REALISASI04AK.dtBukukas, SUM(REALISASI04AK.cJumlah) as Trx , " +
                          " (Select sum(cJumlah) from tBukuBesar where iSUmber = REALISASI04AK.inourut and iIDRekening= REALISASI04AK.IIDRekening )  as BB " +
                           " from REALISASI04AK Where REALISASI04AK.dtBukukas <=@TANGGAL and REALISASI04AK.IDDInas= @DINAS and REALISASI04AK.IIDRekening= @REKENING "+
                    " group by REALISASI04AK.inourut ,REALISASI04AK.NoBukti,REALISASI04AK.dtBukukas,REALISASI04AK.IIDRekening ";
                }

            
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@TANGGAL", tanggal,DbType.Date));
                paramCollection.Add(new DBParameter("@DINAS", dinas));
                paramCollection.Add(new DBParameter("@REKENING", IDRekening));

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                                select new SelisihTrxBB()
                                {

                                    NoUrut = DataFormat.GetLong(dr["NoUrut"]),
                 
                                    Tanggal= DataFormat.GetDate(dr["dtBukukas"]),
                                    NoBukti = DataFormat.GetString(dr["NoBukti"]),
                                    Trx= DataFormat.GetDecimal(dr["Trx"]),
                                    BB = DataFormat.GetDecimal(dr["BB"]),


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
        public List<SelisihTrxBB> GetSampahBB(long IDRekening,
                                                   int dinas,
                                                    DateTime tanggal)
        {

            try
            {

                List<SelisihTrxBB> lst = new List<SelisihTrxBB>();
                if (IDRekening.ToString().Trim().Substring(0, 1) == "4")
                {
                    SSQL = "Select tBukubesar.iSumber as NoUrut, tBukuBesar.sNoBukti as NoBukti," +
                        "tBukuBesar.dtTransaksi as dtBukukas,tBukuBesar.IDSUbKegiatan,  0 as Trx ,  tBukuBesar.cJumlah  as BB " +
                        "from tBukuBesar where iSumber not in (select inourut from  RealisasiSTS where iddinas=@DINAS and IIDRekening= @REKENING)" +
                        "and tBukuBesar.IDDInas= @DINAS and tBukuBesar.IIDRekening= @REKENING and tBukubesar.dtTransaksi  <=@TANGGAL ";

                }
                else
                {
                    SSQL = "Select tBukubesar.iSumber as NoUrut, tBukuBesar.sNoBukti as NoBukti," +
                        "tBukuBesar.dtTransaksi as dtBukukas,tBukuBesar.IDSUbKegiatan,  0 as Trx ,  tBukuBesar.cJumlah  as BB " +
                        "from tBukuBesar where iSumber not in (select inourut from  REALISASI04AK where iddinas=@DINAS )" +
                        "and tBukuBesar.IDDInas= @DINAS and tBukuBesar.IIDRekening= @REKENING and tBukubesar.dtTransaksi  <=@TANGGAL ";
                    SSQL = SSQL + " UNION " +
                      "Select REALISASI04AK.inourut as NoUrut,REALISASI04AK.NoBukti,REALISASI04AK.dtBukukas,REALISASI04AK.IdSubKegiatan , SUM(REALISASI04AK.cJumlah) as Trx , " +
                    "(Select sum(cJumlah) from tBukuBesar where iSUmber = REALISASI04AK.inourut and iIDRekening= REALISASI04AK.IIDRekening )  as BB " +
                    "from REALISASI04AK Where REALISASI04AK.dtBukukas <=@TANGGAL and REALISASI04AK.IDDInas= @DINAS and REALISASI04AK.IIDRekening= @REKENING " +
                    "group by REALISASI04AK.inourut ,REALISASI04AK.NoBukti,REALISASI04AK.dtBukukas,REALISASI04AK.IdSubKegiatan, REALISASI04AK.IIDRekening " +
                    "having SUM(REALISASI04AK.cJumlah)<(Select sum(cJumlah) from tBukuBesar where iSUmber = REALISASI04AK.inourut and iIDRekening= REALISASI04AK.IIDRekening )";

;

                }


                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@TANGGAL", tanggal, DbType.Date));
                paramCollection.Add(new DBParameter("@DINAS", dinas));
                paramCollection.Add(new DBParameter("@REKENING", IDRekening));

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new SelisihTrxBB()
                               {

                                   NoUrut = DataFormat.GetLong(dr["NoUrut"]),

                                   Tanggal = DataFormat.GetDate(dr["dtBukukas"]),
                                   NoBukti = DataFormat.GetString(dr["NoBukti"]),
                                   Trx = DataFormat.GetDecimal(dr["Trx"]),
                                   BB = DataFormat.GetDecimal(dr["BB"]),


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
        
        public List<Jurnal> GetJurnalByNoUrutSumber(long noUrutSumber)
        {


            try{
                List<Jurnal> lst = new List<Jurnal>();
                
                SSQL ="SELECT * from tJurnal where inourutSumber= @NoURUTSUMBER";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@NoURUTSUMBER", noUrutSumber));
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                                select new Jurnal()
                                {

                                    NoJurnal = DataFormat.GetLong(dr["inojurnal"]),
                                    IDDinas =DataFormat.GetInteger(dr["IDDInas"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeuk"]),
                                    UnitAnggaran = DataFormat.GetInteger(dr["UnitAnggaran"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenisJurnal"]),
                                    TanggalJurnal = DataFormat.GetDate(dr["dtJurnal"]),
                                    NoUrutSumber = DataFormat.GetLong(dr["iSumber"]),
                                    JenisSumber = DataFormat.GetInteger(dr["iJenisSumber"]),
                                    NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                    

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
        public bool Simpan(Jurnal j)
        {


            try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                long NoJurnal = 0;
                if (j.NoJurnal == 0)
                {
                
                    NoJurnal =ReadNo(E_KOLOM_NOURUT.CON_URUT_JURNAL, j.IDDinas); 
                    if (NoJurnal ==0){
                        _lastError="Salah mengambil data key ";
                        _isError = true;
                        return false ;
                    }
       
                    SSQL ="INSERT INTO tJurnal (iNoJurnal, iTahun,IDDinas, btKodeUK, dtInput, dtJurnal, iStatus, sNoBukti, " +
                      "btJenisJurnal, dtTanggalBukti, sNoBukuKas, iSumber, iJenisSumber, btPeruntukan, bPPKD,iKelompok, bPotongan ,sKeterangan) values (" +
                      "@NoJurnal, @Tahun,@Dinas, @KodeUK, @dtInput, @dtJurnal, @Status, @NoBukti, " +
                      "@JenisJurnal, @TanggalBukti, @NoBukuKas, @Sumber, @JenisSumber, @Peruntukan, @PPKD,@Kelompok, @Potongan,@Keterangan)";

                        paramCollection.Add(new DBParameter("@NoJurnal", NoJurnal));
                        paramCollection.Add(new DBParameter("@Tahun",j.Tahun));
                        paramCollection.Add(new DBParameter("@Dinas", j.IDDinas ));
                        paramCollection.Add(new DBParameter("@KodeUK", j.KodeUK));
                        paramCollection.Add(new DBParameter("@dtInput", DateTime.Now.Date,DbType.Date));
                        paramCollection.Add(new DBParameter("@dtJurnal", DateTime.Now.Date,DbType.Date));
                        paramCollection.Add(new DBParameter("@Status",0));
                        paramCollection.Add(new DBParameter("@NoBukti", j.NoBukti));
                          paramCollection.Add(new DBParameter("@JenisJurnal", j.Jenis));
                        paramCollection.Add(new DBParameter("@TanggalBukti",j.TanggalBukti));
                        paramCollection.Add(new DBParameter("@NoBukuKas", j.NoBukti));
                        paramCollection.Add(new DBParameter("@Sumber",0));
                        paramCollection.Add(new DBParameter("@JenisSumber", j.JenisSumber)); 
                        paramCollection.Add(new DBParameter("@Peruntukan",0));
                        paramCollection.Add(new DBParameter("@PPKD",0));
                        paramCollection.Add(new DBParameter("@Kelompok",0));
                        paramCollection.Add(new DBParameter("@Potongan", 0));
                        paramCollection.Add(new DBParameter("@Keterangan", j.Uraian));

                         _dbHelper.ExecuteDataTable(SSQL, paramCollection);

                }
                else
                {
                    NoJurnal = j.NoJurnal;
                    SSQL = "UPDATE  tJurnal SET  iTahun=@Tahun,IDDinas= @Dinas, btKodeUK=@KodeUK,   sNoBukti= @NoBukti, " +
                   "btJenisJurnal=@JenisJurnal, iJenisSumber =@JenisSumber, dtTanggalBukti=@TanggalBukti, sNoBukuKas=@NoBukuKas,sKeterangan=@Keterangan where iNoJurnal=@NoJurnal ";

                    paramCollection.Add(new DBParameter("@NoJurnal", NoJurnal));
                    paramCollection.Add(new DBParameter("@Tahun", j.Tahun));
                    paramCollection.Add(new DBParameter("@Dinas", j.IDDinas));
                    paramCollection.Add(new DBParameter("@KodeUK", j.KodeUK));
      
                    paramCollection.Add(new DBParameter("@NoBukti", j.NoBukti));
                    paramCollection.Add(new DBParameter("@JenisJurnal", j.Jenis));
                    paramCollection.Add(new DBParameter("@JenisSumber", j.JenisSumber)); 
                    paramCollection.Add(new DBParameter("@TanggalBukti", j.TanggalBukti,DbType.Date));
                    paramCollection.Add(new DBParameter("@NoBukuKas", j.NoBukti));
                    paramCollection.Add(new DBParameter("@Keterangan", j.Uraian));
     
                    _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                }
                SSQL = "DELETE tJurnalRekening WHERE inoJurnal =@NOJURNAL";
                paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@NOJURNAL", NoJurnal));
                _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                foreach(JurnalRekeningShow jr in j.Details ){

                        paramCollection = new DBParameterCollection();
                        SSQL = "INSERT INTO tJurnalRekening (iNoJurnal, IDUrusan, IDProgram, IDKegiatan, " +
                        "IDSubKegiatan,  iIDRekening, iNoUrut, iDebet, cJumlah,  iKelompok,sKeterangan) values ( " +
                         "@NoJurnal, @IDUrusan, @IDProgram, @IDKegiatan, " +
                         "@IDSubKegiatan,  @IDRekening, @NoUrut, @Debet, @Jumlah,  @Kelompok,@Keterangan)";
                         paramCollection.Add(new DBParameter("@NoJurnal", NoJurnal));
                        paramCollection.Add(new DBParameter("@IDUrusan",0)); 
                        paramCollection.Add(new DBParameter("@IDProgram",0)); 
                        paramCollection.Add(new DBParameter("@IDKegiatan", 0));
                         paramCollection.Add(new DBParameter("@IDSubKegiatan",0)); 
                        paramCollection.Add(new DBParameter("@IDRekening", jr.IIDRekening));
                        paramCollection.Add(new DBParameter("@NoUrut", 0));
                        paramCollection.Add(new DBParameter("@Debet", jr.Debet));
                        paramCollection.Add(new DBParameter("@Jumlah",  jr.Jumlah, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@Kelompok", 0));
                        paramCollection.Add(new DBParameter("@Keterangan", j.Uraian));

                         _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                    



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
        public bool Hapus(long noJurnal)
        {


            try
            {
                    
                    SSQL = "delete from tJurnal where iNoJurnal="+ noJurnal.ToString();

                    _dbHelper.ExecuteDataTable(SSQL);


                    SSQL = "DELETE tJurnalRekening WHERE inoJurnal =" + noJurnal.ToString();

                    _dbHelper.ExecuteDataTable(SSQL);
                
                return true;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }


        }
        public List<JurnalRekeningShow> GetByNoUrutSumber(long noUrutSumber, int bPotongan=0)
        {


            try
            {
                List<JurnalRekeningShow> lst = new List<JurnalRekeningShow>();

                SSQL = "Select tJurnal.InoJurnal, tJurnalRekening.idebet, tJurnalRekening.IIDRekening, tJurnalRekening.cJumlah, mRekening.sNamaRekening,tJurnal.bPPKD " +
                " from tJurnal Inner join tJurnalRekening on tJurnal.inoJurnal = tJurnalRekening.inojurnal " +
                " Left join mRekening on tJurnalRekening.IIdRekening = mRekening.IIDrekening" +
               "   where tJurnal.iSumber= @NoURUTSUMBER AND bPotongan =@BPOTONGAN ORDER by tJurnal.InoJurnal asc, tJurnalRekening.iDebet desc";
            

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@NoURUTSUMBER", noUrutSumber));
                paramCollection.Add(new DBParameter("@BPOTONGAN", bPotongan));
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new JurnalRekeningShow()
                               {
                                   NoJurnal = DataFormat.GetLong(dr["inojurnal"]),
                                   IIDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                   Debet= DataFormat.GetInteger(dr["iDebet"]),
                                   PPKD = DataFormat.GetInteger(dr["bPPKD"]),
                                   NamaRekening = DataFormat.GetString(dr["sNamaRekening"]),
                                   Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),


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
        public List<JurnalRekeningShow> GetByNoJurnal(long NoJurnal)
        {


            try
            {
                List<JurnalRekeningShow> lst = new List<JurnalRekeningShow>();

                SSQL = "Select tJurnal.InoJurnal, tJurnalRekening.idebet, tJurnalRekening.IIDRekening, tJurnalRekening.cJumlah, mRekening.sNamaRekening " +
                " from tJurnal Inner join tJurnalRekening on tJurnal.inoJurnal = tJurnalRekening.inojurnal " +
                " Left join mRekening on tJurnalRekening.IIdRekening = mRekening.IIDrekening" +
               "   where tJurnal.iNoJurnal= @NoJURNAL ORDER by tJurnal.InoJurnal asc, tJurnalRekening.iDebet desc";


                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@NoJURNAL", NoJurnal));
      
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new JurnalRekeningShow()
                               {
                                   NoJurnal = DataFormat.GetLong(dr["inojurnal"]),
                                   IIDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                   Debet = DataFormat.GetInteger(dr["iDebet"]),
                                   NamaRekening = DataFormat.GetString(dr["sNamaRekening"]),
                                   Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),


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
    }
}
