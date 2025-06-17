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
using DTO;


namespace BP.Bendahara
{
    public class BKULogic:BP

    {
        private DateTime TanggalAkhirTahun; 
        public BKULogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tBKU";

            TanggalAkhirTahun = new DateTime(Tahun, 12, 31);

        }

        public decimal CekNoUrutSumberDiBKU(long nourut, int Jenis, int jenisBendahara)
        {
            try
            {
                List<long> lstNoUurut = new List<long>();
                lstNoUurut.Add (nourut);
                List<BKU> lstBKU = new List<BKU>();
                List<BKU> lstBKUJenisIni = new List<BKU>();

                lstBKU = GetBKUByNoUrutSumber(lstNoUurut, jenisBendahara);
                if (lstBKU != null)
                {
                    lstBKUJenisIni = lstBKU.FindAll(x => x.JenisSumber == Jenis);
                    return lstBKUJenisIni.Sum(x => x.Jumlah);

                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return 0;
            }
        }
        public bool BersihkanBKUPendapatanImport(int iddinas ,string namaSheet)
        {
            try
            {

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@NAMAFILE", namaSheet,DbType.String));
                paramCollection.Add(new DBParameter("@DINAS", iddinas, DbType.String));
                
                SSQL = "DELETE tBKUREkening from tbkurekening inner join tbku on tbkurekening.inourut = tbku.inourut where tbku.inourutsumber in (select inourut from tsts where IDDInas=@DINAS and namafile = @NAMAFILE)";


                _dbHelper.ExecuteNonQuery(SSQL,paramCollection);
                SSQL = "DELETE tBKU where tbku.inourutsumber in (select inourut from tsts where  IDDInas=@DINAS and  namafile =@NAMAFILE)";

                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                return true;

            }
            catch (Exception ex)
            {
                _lastError = "Kesalahan membersihkan data BKU Imporet Pendapatan " + ex.Message;

                return false;
            }
        }

        public List<BKU> GetBKUByNoUrutSumber(List<long> lstNoUrut, int jenisBendahara)
        {
            List<BKU> _lst = new List<BKU>();
            try
            {
                //SSQL = " SELECT tSPPRekening.*, mRekening.sNamaRekening as Nama from tSPPRekening INNER jOIN mRekening ON tSPPRekening.IIDRekening = mRekening.IIDRekening WHERE tSPPRekening.inourut = " + iNoUrut.ToString() ;
                //SSQL = SSQL + " ORDER BY IDKegiatan, IIDRekening";
                int id =99;
                string sNamaParameter = "";
                DBParameterCollection paramCollection = new DBParameterCollection();
                
                SSQL = "select tBKU.* from tBKU ";
                SSQL = SSQL + " where 1>0 and  inourutSumber in ( ";
                foreach (long nu in lstNoUrut)
                {
                    sNamaParameter = "@NoUrut" + nu.ToString();
                    SSQL = SSQL + sNamaParameter + ",";
                    paramCollection.Add(new DBParameter(sNamaParameter, nu, DbType.Int64));
                    id++;
                }

                sNamaParameter = "@NoUrut" + id.ToString();
                SSQL = SSQL + sNamaParameter + ")";
                SSQL = SSQL + "  AND tBKU.inourutSumber <> 99 ";
                if (jenisBendahara > -1)
                {
                    SSQL=SSQL + " and btJenisBendahara=@JenisBendahara";
                    paramCollection.Add(new DBParameter("@JenisBendahara", jenisBendahara));
                }
                
                paramCollection.Add(new DBParameter(sNamaParameter, 99, DbType.Int64));                
                SSQL = SSQL + " ORDER BY tBKU.inourutSumber";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new BKU()
                                {
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    NoUrut = DataFormat.GetLong(dr["iNOurut"]),
                                    NoBKUSKPD = DataFormat.GetInteger(dr["NoBKUSKPD"]),
                                    NoBKU = DataFormat.GetInteger(dr["iNoBKU"]),
                                    NourutSumber=  DataFormat.GetLong(dr["iNOurutSumber"]),
                                    Debet = DataFormat.GetInteger(dr["iDebet"]),
                                    JenisSumber =  DataFormat.GetInteger(dr["JenisSumber"]),
                                    JenisBendahara = (E_JENISBENDAHARA)DataFormat.GetInteger(dr["btJenisBendahara"]),
                                    Kodebank = DataFormat.GetInteger(dr["btIDbank"]),
                                   

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

        public List<BKU> GetMaxNoBKU(int IDDInas =0)
        {
            List<BKU> _lst = new List<BKU>();
            try
            {
                
                DBParameterCollection paramCollection = new DBParameterCollection();

                SSQL = "SELECT A.SKPD,A.btKodeUK, (Select max(isnull(NoBKUSKPD,0)) from tBKU where IDDInas =A.SKPD)  as MasNoBKUSKPD ," +
                            " (SELECT MAX(isnull(inobku,0)) from tBKU WHERE IDDInas=A.SKPD AND btKodeUK =A.btKOdeUK )  AS MaxBKU from vWOrganisasi A";
                if (IDDInas > 0)
                {
                    SSQL = SSQL + " WHERE A.SKPD=@DINAS";
                    paramCollection.Add(new DBParameter("@DINAS", IDDInas));

                }

                SSQL = SSQL + " ORDER BY A.SKPD, A.btKodeUK";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new BKU()
                                {
                                    IDDinas = DataFormat.GetInteger(dr["SKPD"]),
                                    KodeUk= DataFormat.GetInteger(dr["btKodeUK"]),
                                    NoUrut = 0,
                                    NoBKUSKPD = DataFormat.GetInteger(dr["MasNoBKUSKPD"]),
                                    NoBKU = DataFormat.GetInteger(dr["MaxBKU"]),


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
        public BKU GetBKUSaldoAwal(int iddinas, int jenisBendahara)
        {
            BKU bku= new BKU();
            try
            {
                SSQL = "Select * from tBKU where inourut =@NOURUT and btJenisBendahara=" + jenisBendahara.ToString() ;
                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("@NOURUT", iddinas));
                //paramCollection.Add(new DBParameter("@JenisBendahara", jenisBendahara));
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        bku = new BKU()
                        {
                            IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                            Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                            Kodebank = DataFormat.GetInteger(dr["btIDbank"])


                        };
                       
                    }
                }
                return bku;
            }
            catch (Exception ex)
            {
                return null;
            }


        }
        public bool  SimpanSebagaiSaldoAwal(BKU saldoawalbku)
        {
            BKU bku = new BKU();
            try
            {
                SSQL = "Select * from tBKU where inourut =@NOURUT";
                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("@NOURUT", saldoawalbku.IDDinas));
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count == 0)
                    {
                        SSQL = "INSERT INTO tBKU(iTahun, IDDInas, inourut,JenisSumber, iNoUrutSumber, iDebet) Values (@TAHUN,@DINAS,@NOURUT,0,0,1)";
                        paramCollection.Add(new DBParameter("@TAHUN", saldoawalbku.Tahun));
                        paramCollection.Add(new DBParameter("@DINAS", saldoawalbku.IDDinas));

                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                    }                  
                        

                    
                }
                saldoawalbku.JenisBendahara = E_JENISBENDAHARA.BENDAHARA_PENGELUARAN ;
                saldoawalbku.Tahun = Tahun;
                saldoawalbku.Keterangan = "Saldo Awal";
                saldoawalbku.TanggalTransaksi = new DateTime(Tahun - 1, 12, 31);
                saldoawalbku.LevelTampilan = E_LEVLETAMPILANBKU.eBKUHeader;
                

                saldoawalbku.NoUrut = saldoawalbku.IDDinas;
                if (Simpan(ref saldoawalbku) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                   
                
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }


        }
        public BKU GetBKUDenganMaxNoBKU(int IDDInas, int KodeUK, int jenisBendahara = 2, Nullable<DateTime> dtBatas = null)
        {
            BKU bku = new BKU();
            try
            {

                DBParameterCollection paramCollection = new DBParameterCollection();

                   SSQL = "SELECT A.IDDInas,A.btKodeUK, " +
                        "(Select max(inourut % 1000000 ) from tBKU where IDDInas =@DINAS  )  as ShortNoUrut ," +
                        " (Select max(isnull(NoBKUSKPD,0)) from tBKU where IDDInas =@DINAS and btJenisBendahara =@JenisBendahara ";
                       if (dtBatas != null)
                        {
                       
                          SSQL=SSQL + " AND dtBukti<= @TANGGAL ";
                       }
                       SSQL = SSQL + ")  as MasNoBKUSKPD ," +
                         " (Select max(isnull(iNoBKU ,0)) from tBKU where IDDInas =@DINAS and btKodeUK =@KodeUK and  A.btJenisBendahara =@JenisBendahara)  as MaxBKU " +
                        " from tBKU A ";

                       SSQL = SSQL + " WHERE  A.IDDInas=@DINAS and btKodeUK =@KodeUK";
                        paramCollection.Add(new DBParameter("@DINAS", IDDInas));
                        paramCollection.Add(new DBParameter("@KodeUK", KodeUK));
                        paramCollection.Add(new DBParameter("@JenisBendahara", jenisBendahara));
                        if (dtBatas != null)
                        {
                            SSQL = SSQL + " AND A.dtBukti<= @TANGGAL";
                            paramCollection.Add(new DBParameter("@TANGGAL", dtBatas,DbType.Date));
                        }
              
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                         DataRow dr = dt.Rows[0];
                         bku = new BKU()
                         {
                             IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                             KodeUk = DataFormat.GetInteger(dr["btKodeUK"]),
                             NoUrutSaja = DataFormat.GetInteger(dr["ShortNoUrut"]),
                             NoBKUSKPD = DataFormat.GetInteger(dr["MasNoBKUSKPD"]),
                             NoBKU = DataFormat.GetInteger(dr["MaxBKU"]),


                         };
                         return bku;
                    }
                    else
                    {
                        bku = new BKU()
                         {
                             IDDinas = IDDInas,
                             KodeUk = KodeUK,
                             NoUrutSaja = 1,
                             NoBKUSKPD = 1,
                             NoBKU = 1,


                         };
                         return bku;

                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }

        }
        
        public int GetNoUrutBKU(int Tahun,int IDDInas=0)
        {
            
            DBParameterCollection paramCollection = new DBParameterCollection();

            try
            {

               

                SSQL = "SELECT mNoUrutBKU.iNoBKU  from   mNoUrutBKU where IDDInas =@IDDINAS AND iTahun =@TAHUN";

                paramCollection.Add(new DBParameter("@IDDINAS", IDDInas));
                paramCollection.Add(new DBParameter("@TAHUN",Tahun));
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    { 
                        int i =DataFormat.GetInteger(dt.Rows[0]["iNoBKU"]);
                        SSQL = "UPDATE mNoUrutBKU SET inobku = inobku+1 where iTahun=@TAHUN  AND IDDInas=@IDDINAS ";
                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                        return i;
                    }
                    else
                    {
                        SSQL = "INSERT INTO mNoUrutBKU (iTahun, IDDInas, iNOBKU ) Values (@TAHUN ,@IDDINAS,1) ";
                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                        return 1;

                    }
                }
                return 0;
                
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                SSQL = "INSERT INTO mNoUrutBKU (iTahun, IDDInas, iNOBKU ) Values (@TAHUN ,@DINAS,1) ";
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                
                return 1;
            }

        }
        public int GetNoBKU(int Tahun,  int jenisBendahara, int IDDInas = 0)
        {

            DBParameterCollection paramCollection = new DBParameterCollection();
            try
            {



                SSQL = "SELECT Max(NoBKUSKPD) as iNoBKU  from   tBKU where IDDInas =@IDDINAS AND iTahun =@TAHUN and btJenisBendahara = @jenisBendahara";

                paramCollection.Add(new DBParameter("@IDDINAS", IDDInas));
                paramCollection.Add(new DBParameter("@TAHUN", Tahun));
                paramCollection.Add(new DBParameter("@jenisBendahara", jenisBendahara));
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        int i = DataFormat.GetInteger(dt.Rows[0]["iNoBKU"]);
                        if (i == 0)
                            i = 1;
                        return i;
                    }
                    else
                    {
                       
                        return 1;

                    }
                }
                return 0;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
              

                return 1;
            }

        }
        private BKU GetDataBKU(int idDInas, int KodeUK, long inourutSumber, int jenisSumber, int debet, int JenisBendahara)
        {
            BKU oBKU = new BKU();
            DBParameterCollection paramCollection = new DBParameterCollection();
            DataTable dt = new DataTable();
            try
            {
                SSQL = "SELECT * from tBKU where IDDInas  =@DINAS AND JenisSumber=@JENISSUMBER " +
                       " AND iNoUrutSUmber=@NOURUTSUMBER AND iDebet=@DEBET  AND btJenisBendahara=@JENISBENDAHARA";
               
                paramCollection.Add(new DBParameter("@DINAS", idDInas));
                paramCollection.Add(new DBParameter("@JENISSUMBER", jenisSumber));
                paramCollection.Add(new DBParameter("@NOURUTSUMBER", inourutSumber));
                paramCollection.Add(new DBParameter("@DEBET", debet));
                paramCollection.Add(new DBParameter("@JENISBENDAHARA", JenisBendahara));     

            
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        oBKU = new BKU()
                        {
                            Tahun = DataFormat.GetInteger(dr["iTahun"]),
                            IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                            Position = DataFormat.GetInteger(dr["Position"]),
                            JenisBendahara = (E_JENISBENDAHARA)DataFormat.GetInteger(dr["btJenisBendahara"]),

                            NourutSumber = DataFormat.GetLong(dr["iNoUrutSumber"]),
                            //  public  int JenisBaru As E_JENISBARU_REFERENSI
                            KodeRekening = DataFormat.GetInteger(dr["IIDRekening"]),

                            No = DataFormat.GetInteger(dr["iNo"]),//' Number ...-> Misalnya untuk BP :1 Pengeluaran, 2 Penerimaan
                          
                            NoBKU = DataFormat.GetInteger(dr["inobku"]),
                            NoBKUSKPD = DataFormat.GetInteger(dr["NoBKUSKPD"]),
                            nobkuBUD = DataFormat.GetInteger(dr["iNourut"]),

                            NoUrut = DataFormat.GetLong(dr["iNourut"]),


                        };
                    }
                    else
                    {
                        SSQL = "SELECT max(NoBKUSKPD) as MasNoBKUSKPD," +
                            " (SELECT MAX(inobku) from tBKU WHERE IDDInas=@IDDINAS AND btKodeUK =@KodeUK )  AS MaxBKU from tBKU where IDDInas =@IDDINAS  ";


                        paramCollection.Add(new DBParameter("@IDDINAS", idDInas));
                        paramCollection.Add(new DBParameter("@KodeUK", KodeUK));
                   
                        dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.Rows[0];
                                oBKU = new BKU()
                                {

                                    NoBKU = DataFormat.GetInteger(dr["MaxBKU"]),
                                    NoBKUSKPD = DataFormat.GetInteger(dr["MasNoBKUSKPD"]),                                    
                                    NoUrut = 0,
                                    No = DataFormat.GetInteger(dr["MasNoBKUSKPD"])
                                    
                                };
                                
                            }
                        }
                    }
                }
                return oBKU;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return oBKU;
            }
        }
        //public List<BKU> BukuKas(int mode, int dinas,
        //                         int JenisBendahara,
        //                         DateTime tanggalAwal, 
        //                         DateTime tanggalAkhir)
        //{
        //    SSQL=" SELECT SUM(cJumlah*iDebet) AS Jumlah FROM tBKU " +
        //        " WHERE iTahun=@Tahun  And btIDBank =@mode AND dtBukti <  @tnggalAwal " + 
        //        " AND IDDInas =@dinas  AND btJenisBEndahara =@JENISBENAHARA";

    
            
        //} 

        public bool Simpan(ref BKU bku, IDbConnection con, IDbTransaction oDBTrasaction)
        {

            try
            {


                if (bku.NoUrut == 0 )
                {

                  
                        // tahun 
                        // 2310101010100000
                        //230000000000
                    string sNoUrut = "";
                    long lNoUrut = 0;
                    //if (bku.JenisBendahara > 0)
                    //{
                        sNoUrut = Tahun.ToString().Substring(2, 2);
                        sNoUrut = sNoUrut + (bku.IDDinas + bku.KodeUk).ToString();
                        sNoUrut = sNoUrut + (bku.JenisSumber).ToString("00");

                        sNoUrut = sNoUrut + ((int)bku.JenisBendahara).ToString("0");

                        lNoUrut = DataFormat.GetLong(sNoUrut + "000000") + bku.NoUrutSaja;
                        bku.NoUrut = lNoUrut;
                    //}
                    //else
                    //{
                    //    sNoUrut = Tahun.ToString().Substring(2, 2);
                    //    sNoUrut = sNoUrut + "99";
                    //    lNoUrut = DataFormat.GetLong(sNoUrut + "000000") + bku.NoUrutSaja;
                    //    bku.NoUrut = lNoUrut;
                    //}
                        

                    

                    SSQL = "INSERT INTO tBKU (Position ,iNo ,JenisSumber ,iNoUrutSumber ,iTahun ,iNoBKU ,NoBukti ,dtBukti ,sUraian ,btKodekategori , " +
                      " btKodeUrusan , btKodeSKPD , btKodeUK , btKodeKategoriPelaksana , btKodeurusanPelaksana ,btIDProgram ,btIDkegiatan,iDebet," +
                      "cJumlah,btIDbank ,IIDrekening,btFungsi,bPPKD,NoBKUSKPD,cJenisBelanja ,iLevelTampilan ,iNoUrut," +
                      "bPajak,bBKU, btJenisBendahara,iNoUrutOnSameNumber, btIDSubKegiatan,iKelompokSPJ, OnSPJ , IdImport , IDUrusan ,idDinas ,idProgram ,IDkegiatan " +
                      ",iNoUrutManual,ShortNoUrut,UnitAnggaran) values ( @pPosition ,@pNo, @pJenisRef,@pNourutReferensi,@pTahun,@pNoBKU " +
                      ", @pNoBukti ,@pTanggalTransaksi ,@pKeterangan ,@pKodekategori ,@pKodeUrusan ,@pKodeSKPD ,@pKodeUk ,@pKodekategoriPelaksana ,@pKodeUrusanPelaksana  ,@pKodeProgram " +
                       ",@pKodeKegiatan ,@pDebet ,@pJumlah ,@pKodebank  ,@pKodeRekening ,1,@pPPKD ,@pNoBKUSKPD ," +
                       "@pJenisBelanja ,@pLevelTampilan ,@pNoUrut ,@pPajak ,@pIsBKU ," +
                       "@pJenisBendahara ,@pNoUrutOnSameNumber ,@pKodeSubKegiatan ,@pKelompokSPJ ,@pOnSPJ ," +
                       "@pIDImport ,@pIDUrusan ,@pIDDinas ,@piIDProgram ,@pIDkegiatan ,@pnourutManual,@NoUrutSaja,@UnitAnggaran )";


                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pPosition", bku.Position,DbType.Int32));
                    paramCollection.Add(new DBParameter("@pNo", bku.NoBKUSKPD, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pJenisRef", (int)bku.JenisSumber, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pNourutReferensi", bku.NourutSumber, DbType.Int64));
                    paramCollection.Add(new DBParameter("@pTahun", bku.Tahun, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pNoBKU", bku.NoBKU, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pNoBukti", bku.NoBukti, DbType.String));
                    paramCollection.Add(new DBParameter("@pTanggalTransaksi", bku.TanggalTransaksi,DbType.Date ));
                    paramCollection.Add(new DBParameter("@pKeterangan", bku.Keterangan, DbType.String));
                    paramCollection.Add(new DBParameter("@pKodekategori", bku.Kodekategori, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeUrusan", bku.KodeUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeSKPD", bku.KodeSKPD, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeUk", bku.KodeUk, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodekategoriPelaksana", bku.KodekategoriPelaksana, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeUrusanPelaksana", bku.KodeUrusanPelaksana, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeProgram", bku.KodeProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeKegiatan", bku.KodeKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pDebet", bku.Debet, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pJumlah", bku.Jumlah,DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pKodebank", bku.Kodebank));
                    paramCollection.Add(new DBParameter("@pKodeRekening", bku.KodeRekening));
                    paramCollection.Add(new DBParameter("@pPPKD", bku.PPKD, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pNoBKUSKPD", bku.NoBKUSKPD, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pJenisBelanja", bku.JenisBelanja, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pLevelTampilan", (int)bku.LevelTampilan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pNoUrut", bku.NoUrut, DbType.Int64));
                    paramCollection.Add(new DBParameter("@pPajak", bku.Pajak, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIsBKU", bku.IsBKU, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pJenisBendahara", (int)bku.JenisBendahara, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pNoUrutOnSameNumber", bku.NoUrutOnSameNumber, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeSubKegiatan", bku.KodeSubKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKelompokSPJ", bku.KelompokSPJ, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pOnSPJ", bku.OnSPJ, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDImport", bku.IDImport, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDUrusan", bku.IDUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDDinas", bku.IDDinas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@piIDProgram", bku.iIDProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", bku.IDkegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pnourutManual", bku.nourutManual, DbType.Int32));
                    paramCollection.Add(new DBParameter("@NoUrutSaja", bku.NoUrutSaja + 1, DbType.Int32));
                    paramCollection.Add(new DBParameter("@UnitAnggaran", bku.UnitAnggaran, DbType.Int32));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection, con, oDBTrasaction);

                

                }
                else
                {
                    SSQL = "UPDATE tBKU SET  iTahun=@Tahun,NoBukti=@pNoBukti ,dtBukti=@pTanggalTransaksi ,sUraian= @pKeterangan,btKodekategori=@pKodekategori , " +
                           " btKodeUrusan=@pKodeUrusan , btKodeSKPD=@pKodeSKPD , btKodeUK=@pKodeUk , btKodeKategoriPelaksana=@pKodekategoriPelaksana" +
                            ", btKodeurusanPelaksana=@pKodeUrusanPelaksana ,btIDProgram=@pKodeProgram ,btIDkegiatan=@pKodeKegiatan,iDebet=@pDebet," +
                            "cJumlah=@pJumlah,btIDbank=@pKodebank ,IIDrekening=@pKodeRekening,btFungsi=1,bPPKD=@pPPKD,NoBKUSKPD=@pNoBKUSKPD,iNoBKU=@NoBKU,cJenisBelanja=@pJenisBelanja" +
                            ",iLevelTampilan=@pLevelTampilan ,bPajak=@pPajak ,bBKU=@pIsBKU, btJenisBendahara=@pJenisBendahara," +
                            "iNoUrutOnSameNumber=@pNoUrutOnSameNumber, btIDSubKegiatan=@pKodeSubKegiatan,iKelompokSPJ=@pKelompokSPJ, OnSPJ=@pOnSPJ" +
                            ", IdImport=@pIDImport , IDUrusan =@pIDUrusan,idDinas=@pIDDinas ,idProgram=@piIDProgram ,IDkegiatan =@pIDkegiatan" +
                             ",iNoUrutManual=@pnourutManual WHERE iNourut = @pNoUrut"; //) values ( @pPosition ,@pNo, @pJenisRef,@pNourutReferensi,@pTahun,@pNoBKU " +



                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@Tahun", bku.Tahun,DbType.Int32));
                    paramCollection.Add(new DBParameter("@pNoBukti", bku.NoBukti,DbType.String));
                    paramCollection.Add(new DBParameter("@pTanggalTransaksi", bku.TanggalTransaksi,DbType.Date));
                    paramCollection.Add(new DBParameter("@pKeterangan", bku.Keterangan,DbType.String ));
                    paramCollection.Add(new DBParameter("@pKodekategori", bku.Kodekategori,DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeUrusan", bku.KodeUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeSKPD", bku.KodeSKPD, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeUk", bku.KodeUk, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodekategoriPelaksana", bku.KodekategoriPelaksana, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeUrusanPelaksana", bku.KodeUrusanPelaksana, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeProgram", bku.KodeProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeKegiatan", bku.KodeKegiatan, DbType.Int32));



                    paramCollection.Add(new DBParameter("@pDebet", bku.Debet, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pJumlah", bku.Jumlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pKodebank", bku.Kodebank, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeRekening", bku.KodeRekening, DbType.Int64));
                    paramCollection.Add(new DBParameter("@pPPKD", 0));
                    paramCollection.Add(new DBParameter("@pNoBKUSKPD", bku.NoBKUSKPD, DbType.Int32));
                    paramCollection.Add(new DBParameter("@NoBKU", bku.NoBKU, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pJenisBelanja", (int)bku.JenisBelanja, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pLevelTampilan", (int)bku.LevelTampilan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pPajak", bku.Pajak, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIsBKU", bku.IsBKU, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pJenisBendahara", (int)bku.JenisBendahara, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pNoUrutOnSameNumber", bku.NoUrutOnSameNumber, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeSubKegiatan", bku.KodeSubKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKelompokSPJ", bku.KelompokSPJ, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pOnSPJ", bku.OnSPJ, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDImport", bku.IDImport, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDUrusan", bku.IDUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDDinas", bku.IDDinas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@piIDProgram", bku.iIDProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", bku.IDkegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pnourutManual", bku.nourutManual, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pNoUrut", bku.NoUrut, DbType.Int64));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection, con, oDBTrasaction);
                    
                }
                SSQL = "DELETE TBKUREKENING WHERE inourut =@NOURUT";
                DBParameterCollection paramDelete = new DBParameterCollection();
                paramDelete.Add(new DBParameter("@NOURUT", bku.NoUrut));
                _dbHelper.ExecuteNonQuery(SSQL, paramDelete, con, oDBTrasaction);
                SSQL = string.Empty;
                if (bku.Details != null)
                {
                    foreach (BKURekening br in bku.Details)
                    {
                        SSQL = "INSERT INTO tBKURekening(inourut,IDUrusan, IDProgram, IDKegiatan, IDSUbKegiatan,btKodekategoriPelaksana, " +
                             " btKodeUrusanPelaksana,btKodeUK, btIDProgram,btIDkegiatan,btidsubkegiatan,IIDrekening,cJumlah) " +
                              " values ( @nourut, @IDUrusan, @IDProgram, @IDKegiatan, @IDSUbKegiatan,@KodekategoriPelaksana, " +
                              " @KodeUrusanPelaksana,@KodeUK, @KodeProgram,@Kodekegiatan,@Kodesubkegiatan,@Koderekening,@Jumlah)";
                        DBParameterCollection paramDetail = new DBParameterCollection();
                        paramDetail.Add(new DBParameter("@nourut", bku.NoUrut, DbType.Int64));
                        paramDetail.Add(new DBParameter("@IDUrusan", br.IDUrusan, DbType.Int32));
                        paramDetail.Add(new DBParameter("@IDProgram", br.iIDProgram, DbType.Int32));
                        paramDetail.Add(new DBParameter("@IDKegiatan", br.IDkegiatan, DbType.Int32));
                        paramDetail.Add(new DBParameter("@IDSUbKegiatan", br.IDSubkegiatan, DbType.Int64));
                        paramDetail.Add(new DBParameter("@KodekategoriPelaksana", br.KodekategoriPelaksana, DbType.Int32));
                        paramDetail.Add(new DBParameter("@KodeUrusanPelaksana", br.KodeUrusanPelaksana, DbType.Int32));
                        paramDetail.Add(new DBParameter("@KodeUK", br.KodeUk, DbType.Int32));
                        paramDetail.Add(new DBParameter("@KodeProgram", br.KodeProgram, DbType.Int32));
                        paramDetail.Add(new DBParameter("@Kodekegiatan", br.KodeKegiatan, DbType.Int32));
                        paramDetail.Add(new DBParameter("@Kodesubkegiatan", br.KodeSubKegiatan, DbType.Int32));
                        paramDetail.Add(new DBParameter("@Koderekening", br.idRekening, DbType.Int64));
                        paramDetail.Add(new DBParameter("@Jumlah", br.Jumlah, DbType.Decimal));
                        if (br.Jumlah > 0)
                        _dbHelper.ExecuteNonQuery(SSQL, paramDetail, con, oDBTrasaction);
                    }
                }

                return true;
            }catch(Exception ex){
                    _lastError = ex.Message;
                    return false ;
             }
                

            
        }
        public bool Simpan(ref BKU bku)
        {

            try
            {


                if (bku.NoUrut == 0 )
                {

                    if (bku.NoUrut == 0)
                    {
                     
                        string sNoUrut = Tahun.ToString().Substring(2, 2);
                        sNoUrut = sNoUrut + (bku.IDDinas + bku.KodeUk).ToString();
                        sNoUrut = sNoUrut + bku.JenisSumber.ToString("00");
               
                        long lNoUrut = DataFormat.GetLong(sNoUrut + "000000") + bku.NoUrutSaja;
                        bku.NoUrut = lNoUrut;
                        //int NoBKU = GetNoBKU(bku.Tahun, bku.IDDinas, (int)bku.JenisBendahara);
                        //bku.NoBKUSKPD = NoBKU;

                    }

                    SSQL = "INSERT INTO tBKU (Position ,iNo ,JenisSumber ,iNoUrutSumber ,iTahun ,iNoBKU ,NoBukti ,dtBukti ,sUraian ,btKodekategori , " +
                      " btKodeUrusan , btKodeSKPD , btKodeUK , btKodeKategoriPelaksana , btKodeurusanPelaksana ,btIDProgram ,btIDkegiatan,iDebet," +
                      "cJumlah,btIDbank ,IIDrekening,btFungsi,bPPKD,NoBKUSKPD,cJenisBelanja ,iLevelTampilan ,iNoUrut," +
                      "bPajak,bBKU, btJenisBendahara,iNoUrutOnSameNumber, btIDSubKegiatan,iKelompokSPJ, OnSPJ , IdImport , IDUrusan ,idDinas ,idProgram ,IDkegiatan " +
                      ",iNoUrutManual,ShortNoUrut) values ( @pPosition ,@pNo, @pJenisRef,@pNourutReferensi,@pTahun,@pNoBKU " +
                      ", @pNoBukti ,@pTanggalTransaksi ,@pKeterangan ,@pKodekategori ,@pKodeUrusan ,@pKodeSKPD ,@pKodeUk ,@pKodekategoriPelaksana ,@pKodeUrusanPelaksana  ,@pKodeProgram " +
                       ",@pKodeKegiatan ,@pDebet ,@pJumlah ,@pKodebank  ,@pKodeRekening ,1,@pPPKD ,@pNoBKUSKPD ," +
                       "@pJenisBelanja ,@pLevelTampilan ,@pNoUrut ,@pPajak ,@pIsBKU ," +
                       "@pJenisBendahara ,@pNoUrutOnSameNumber ,@pKodeSubKegiatan ,@pKelompokSPJ ,@pOnSPJ ," +
                       "@pIDImport ,@pIDUrusan ,@pIDDinas ,@piIDProgram ,@pIDkegiatan ,@pnourutManual,@NoUrutSaja )";


                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pPosition", bku.Position));
                    paramCollection.Add(new DBParameter("@pNo", bku.NoBKUSKPD));
                    paramCollection.Add(new DBParameter("@pJenisRef", (int)bku.JenisSumber));
                    paramCollection.Add(new DBParameter("@pNourutReferensi", bku.NourutSumber));
                    paramCollection.Add(new DBParameter("@pTahun", bku.Tahun));
                    paramCollection.Add(new DBParameter("@pNoBKU", bku.NoBKU));
                    paramCollection.Add(new DBParameter("@pNoBukti", bku.NoBukti));
                    paramCollection.Add(new DBParameter("@pTanggalTransaksi", bku.TanggalTransaksi, DbType.Date));
                    paramCollection.Add(new DBParameter("@pKeterangan", bku.Keterangan));
                    paramCollection.Add(new DBParameter("@pKodekategori", bku.Kodekategori));
                    paramCollection.Add(new DBParameter("@pKodeUrusan", bku.KodeUrusan));
                    paramCollection.Add(new DBParameter("@pKodeSKPD", bku.KodeSKPD));
                    paramCollection.Add(new DBParameter("@pKodeUk", bku.KodeUk));
                    paramCollection.Add(new DBParameter("@pKodekategoriPelaksana", bku.KodekategoriPelaksana));
                    paramCollection.Add(new DBParameter("@pKodeUrusanPelaksana", bku.KodeUrusanPelaksana));
                    paramCollection.Add(new DBParameter("@pKodeProgram", bku.KodeProgram));
                    paramCollection.Add(new DBParameter("@pKodeKegiatan", bku.KodeKegiatan));
                    paramCollection.Add(new DBParameter("@pDebet", bku.Debet));
                    paramCollection.Add(new DBParameter("@pJumlah", bku.Jumlah,DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pKodebank", bku.Kodebank));
                    paramCollection.Add(new DBParameter("@pKodeRekening", bku.KodeRekening));
                    paramCollection.Add(new DBParameter("@pPPKD", bku.PPKD));
                    paramCollection.Add(new DBParameter("@pNoBKUSKPD", bku.NoBKUSKPD));
                    paramCollection.Add(new DBParameter("@pJenisBelanja", bku.JenisBelanja));
                    paramCollection.Add(new DBParameter("@pLevelTampilan", (int)bku.LevelTampilan));
                    paramCollection.Add(new DBParameter("@pNoUrut", bku.NoUrut));
                    paramCollection.Add(new DBParameter("@pPajak", bku.Pajak));
                    paramCollection.Add(new DBParameter("@pIsBKU", bku.IsBKU));
                    paramCollection.Add(new DBParameter("@pJenisBendahara", (int)bku.JenisBendahara));
                    paramCollection.Add(new DBParameter("@pNoUrutOnSameNumber", bku.NoUrutOnSameNumber));
                    paramCollection.Add(new DBParameter("@pKodeSubKegiatan", bku.KodeSubKegiatan));
                    paramCollection.Add(new DBParameter("@pKelompokSPJ", bku.KelompokSPJ));
                    paramCollection.Add(new DBParameter("@pOnSPJ", bku.OnSPJ));
                    paramCollection.Add(new DBParameter("@pIDImport", bku.IDImport));
                    paramCollection.Add(new DBParameter("@pIDUrusan", bku.IDUrusan));
                    paramCollection.Add(new DBParameter("@pIDDinas", bku.IDDinas));
                    paramCollection.Add(new DBParameter("@piIDProgram", bku.iIDProgram));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", bku.IDkegiatan));
                    paramCollection.Add(new DBParameter("@pnourutManual", bku.nourutManual));
                    paramCollection.Add(new DBParameter("@NoUrutSaja", bku.NoUrutSaja + 1));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);



                }
                else
                {
                    SSQL = "UPDATE tBKU SET  iTahun=@Tahun,NoBukti=@pNoBukti ,dtBukti=@pTanggalTransaksi ,sUraian= @pKeterangan,"+
                         "btKodekategori=@pKodekategori , " +
                           " btKodeUrusan=@pKodeUrusan , btKodeSKPD=@pKodeSKPD , btKodeUK=@pKodeUk , btKodeKategoriPelaksana=@pKodekategoriPelaksana" +
                            ", btKodeurusanPelaksana=@pKodeUrusanPelaksana ,btIDProgram=@pKodeProgram ,btIDkegiatan=@pKodeKegiatan,iDebet=@pDebet," +
                            "cJumlah=@pJumlah,btIDbank=@pKodebank ,IIDrekening=@pKodeRekening,btFungsi=1,bPPKD=@pPPKD,NoBKUSKPD=@pNoBKUSKPD,iNoBKU=@NoBKU,cJenisBelanja=@pJenisBelanja" +
                            ",iLevelTampilan=@pLevelTampilan ,bPajak=@pPajak ,bBKU=@pIsBKU, btJenisBendahara=@pJenisBendahara," +
                            "iNoUrutOnSameNumber=@pNoUrutOnSameNumber, btIDSubKegiatan=@pKodeSubKegiatan,iKelompokSPJ=@pKelompokSPJ, OnSPJ=@pOnSPJ" +
                            ", IdImport=@pIDImport , IDUrusan =@pIDUrusan,idDinas=@pIDDinas ,idProgram=@piIDProgram ,IDkegiatan =@pIDkegiatan" +
                             ",iNoUrutManual=@pnourutManual " +
                             " WHERE iNourut = @pNoUrut"; //) values ( @pPosition ,@pNo, @pJenisRef,@pNourutReferensi,@pTahun,@pNoBKU " +



                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@Tahun", bku.Tahun, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pNoBukti", bku.NoBukti,DbType.String));
                    paramCollection.Add(new DBParameter("@pTanggalTransaksi", bku.TanggalTransaksi, DbType.Date));
                    paramCollection.Add(new DBParameter("@pKeterangan", bku.Keterangan, DbType.String));
                    paramCollection.Add(new DBParameter("@pKodekategori", bku.Kodekategori, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeUrusan", bku.KodeUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeSKPD", bku.KodeSKPD, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeUk", bku.KodeUk, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodekategoriPelaksana", bku.KodekategoriPelaksana, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeUrusanPelaksana", bku.KodeUrusanPelaksana, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeProgram", bku.KodeProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeKegiatan", bku.KodeKegiatan, DbType.Int32));



                    paramCollection.Add(new DBParameter("@pDebet", bku.Debet, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pJumlah", bku.Jumlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pKodebank", bku.Kodebank, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeRekening", bku.KodeRekening, DbType.Int64));
                    paramCollection.Add(new DBParameter("@pPPKD", 0, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pNoBKUSKPD", bku.NoBKUSKPD, DbType.Int32));
                    paramCollection.Add(new DBParameter("@NoBKU", bku.NoBKU, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pJenisBelanja", (int)bku.JenisBelanja, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pLevelTampilan", (int)bku.LevelTampilan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pPajak", bku.Pajak, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIsBKU", bku.IsBKU, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pJenisBendahara", (int)bku.JenisBendahara, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pNoUrutOnSameNumber", bku.NoUrutOnSameNumber, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKodeSubKegiatan", bku.KodeSubKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pKelompokSPJ", bku.KelompokSPJ, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pOnSPJ", bku.OnSPJ, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDImport", bku.IDImport, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDUrusan", bku.IDUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDDinas", bku.IDDinas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@piIDProgram", bku.iIDProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", bku.IDkegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pnourutManual", bku.nourutManual, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pNoUrut", bku.NoUrut, DbType.Int64));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                }
                SSQL = "DELETE TBKUREKENING WHERE inourut =@NOURUT";
                DBParameterCollection paramDelete = new DBParameterCollection();
                paramDelete.Add(new DBParameter("@NOURUT", bku.NoUrut));
                _dbHelper.ExecuteNonQuery(SSQL, paramDelete);
                SSQL = string.Empty;
                if (bku.Details != null)
                {
                    foreach (BKURekening br in bku.Details)
                    {
                        SSQL = "INSERT INTO tBKURekening(inourut,IDUrusan, IDProgram, IDKegiatan, IDSUbKegiatan,btKodekategoriPelaksana, " +
                             " btKodeUrusanPelaksana,btKodeUK, btIDProgram,btIDkegiatan,btidsubkegiatan,IIDrekening,cJumlah) " +
                              " values ( @nourut, @IDUrusan, @IDProgram, @IDKegiatan, @IDSUbKegiatan,@KodekategoriPelaksana, " +
                              " @KodeUrusanPelaksana,@KodeUK, @KodeProgram,@Kodekegiatan,@Kodesubkegiatan,@Koderekening,@Jumlah)";
                        DBParameterCollection paramDetail = new DBParameterCollection();
                        paramDetail.Add(new DBParameter("@nourut", bku.NoUrut));
                        paramDetail.Add(new DBParameter("@IDUrusan", br.IDUrusan));
                        paramDetail.Add(new DBParameter("@IDProgram", br.iIDProgram));
                        paramDetail.Add(new DBParameter("@IDKegiatan", br.IDkegiatan));
                        paramDetail.Add(new DBParameter("@IDSUbKegiatan", br.IDSubkegiatan));
                        paramDetail.Add(new DBParameter("@KodekategoriPelaksana", br.KodekategoriPelaksana));
                        paramDetail.Add(new DBParameter("@KodeUrusanPelaksana", br.KodeUrusanPelaksana));
                        paramDetail.Add(new DBParameter("@KodeUK", br.KodeUk));
                        paramDetail.Add(new DBParameter("@KodeProgram", br.KodeProgram));
                        paramDetail.Add(new DBParameter("@Kodekegiatan", br.KodeKegiatan));
                        paramDetail.Add(new DBParameter("@Kodesubkegiatan", br.KodeSubKegiatan));
                        paramDetail.Add(new DBParameter("@Koderekening", br.idRekening));
                        paramDetail.Add(new DBParameter("@Jumlah", br.Jumlah, DbType.Decimal));
                        _dbHelper.ExecuteNonQuery(SSQL, paramDetail);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }



        }
        public  bool UpdateMxBKU(ref List<BKU> lstMaxNoBKU,
                               BKU bku)
        {
            try
            {
                int index = lstMaxNoBKU.FindIndex(b => b.IDDinas == bku.IDDinas && b.KodeUk == bku.KodeUk);
                if (index >= 0)
                {
                    lstMaxNoBKU[index] = bku;
                }
                else
                {
                    lstMaxNoBKU.Add(bku);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool UbahKeBank(long noUrutSumber,
                               int bank)
        {
            try
            {

                SSQL = "Update tBKU set btIDBank ="+ bank.ToString() +" where inourutSumber= " + noUrutSumber.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        

        public BKU GetOldBKU(Setor setor, List<BKU> lstBKU, 
                               E_JENISBENDAHARA JenisBendahara, E_JENIS_REFERENSIBKU JenisSumber, int debet)
        {

            BKU oldBKU = lstBKU.FirstOrDefault(b => b.NourutSumber == setor.NoUrut &&
                                                        b.JenisBendahara == JenisBendahara &&
                                                        b.JenisSumber == (int)JenisSumber &&
                                                        b.Debet == debet);

            return oldBKU;


        }
       
        public  bool BersihkanBKU(long NoUrut, int jenisSumber,  IDbConnection connection, IDbTransaction odbTrans)
        {
            try
            {
                SSQL = "DELETE tBKU WHERE inourutSUmber =@noUrut AND JENISSUMBER = @JENISSUMBER ";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@noUrut", NoUrut));
                paramCollection.Add(new DBParameter("@JENISSUMBER", jenisSumber));

                _dbHelper.ExecuteNonQuery(SSQL, paramCollection, connection, odbTrans);

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

          }

        public List<clsObject> GetJumlah(int idDinas, int JenisSumber, DateTime tanggalAwal, DateTime tanggalakhir)
        {
            decimal dRet = 0; 
            try
            {

                List<clsObject> lst = new List<clsObject>();

                SSQL = "SELECT inourutSumber as NoUrut, NoBukti , dtBukti,sum( tBKU.cJumlah) as Jumlah from tBKU WHERE IDDInas =" + idDinas.ToString() + " and btJenisBendahara=2 " +
                     " and JenisSumber= " + JenisSumber.ToString() + " AND dtBukti >=" + tanggalAwal.ToSQLFormat() + " AND dtBukti <=" + tanggalakhir.ToSQLFormat();
                if (JenisSumber == 1)
                {
                    SSQL = SSQL + " AND tBKU.iDEbet=-1";
                }
                SSQL = SSQL + " Group by inourutSumber , NoBukti , dtBukti order by inourutSUmber";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    lst = (from DataRow dr in dt.Rows
                           select new clsObject()
                           {
                               NoUrut = DataFormat.GetLong(dr["nourut"]),
                               NoBukti = DataFormat.GetString(dr["NoBukti"]),

                               Jumlah = DataFormat.GetLong(dr["Jumlah"]),
                               Tanggal = DataFormat.GetDate(dr["dtBukti"]),
                           }).ToList();

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
        public decimal GetJumlahDetail(int idDinas, int JenisSumber, DateTime tanggalAwal, DateTime tanggalakhir)
        {
            decimal dRet = 0;
            try
            {

                SSQL = "SELECT sum(tBKURekening.cJumlah) as Jumlah from tBKU "+
                    " INNER JOIN tBKURekening on tBKU.inourut= tBKURekening.inourut WHERE tBKU.IDDInas =" + idDinas.ToString() + " and btJenisBendahara=2 " +
                     " and tBKU.JenisSumber= " + JenisSumber.ToString() + " AND tBKU.dtBukti >=" + tanggalAwal.ToSQLFormat() + " AND tBKU.dtBukti <=" + tanggalakhir.ToSQLFormat();

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
                _isError = true;
                _lastError = ex.Message;
                return dRet;

            }
        }
          public List<BKU> GetByDinas( int idDinas, DateTime tanggal)
          {
              List<BKU> _lst = new List<BKU>();
              try
              {
                  SSQL = "SELECT tBKU.* FROM " + m_sNamaTabel + " WHERE IDDInas =" + idDinas.ToString() + " and btJenisBendahara=2 "+
                      " and btJenisBendahara =2 AND dtBukti <=" + tanggal.ToSQLFormat() +"Order by NoBKUSKPD,iNoBKU";//
                  
                  DataTable dt = new DataTable();
                  
                  dt = _dbHelper.ExecuteDataTable(SSQL);
                  if (dt != null)
                  {
                      if (dt.Rows.Count > 0)
                      {
                          _lst = (from DataRow dr in dt.Rows
                                  select new BKU()
                                  {
                                      
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Position  = DataFormat.GetInteger(dr["Position"]),
                                    // POSITION : 1 -> BUD, 2->SKPD, 3-> BENDAHARA PENERIMAAN
                                    Kodekategori  = DataFormat.GetInteger(dr["btKodekategori"]),
                                    KodeUrusan  = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD  = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUk  = DataFormat.GetInteger(dr["btKodeUk"]),
                                    KodekategoriPelaksana  = DataFormat.GetInteger(dr["btKodekategoriPelaksana"]),
                                    KodeUrusanPelaksana  = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    KodeProgram  = DataFormat.GetInteger(dr["btIDProgram"]),
                                    KodeKegiatan  = DataFormat.GetInteger(dr["btIDKegiatan"]),
                                    KodeSubKegiatan  = DataFormat.GetInteger(dr["btIDSubKegiatan"]),                                   
                                    JenisBendahara = (E_JENISBENDAHARA)DataFormat.GetInteger(dr["btJenisBendahara"]),
                                    NourutSumber  = DataFormat.GetLong(dr["iNoUrutSumber"]),
                                    KodeRekening  = DataFormat.GetInteger(dr["IIDRekening"]),
                                    No   = DataFormat.GetInteger(dr["iNo"]),
                                    NoBKU  = DataFormat.GetInteger(dr["inobku"]),
                                    NoBKUSKPD  = DataFormat.GetInteger(dr["NoBKUSKPD"]),
                                    nobkuBUD  = DataFormat.GetInteger(dr["iNourut"]),
                                    TanggalTransaksi  = DataFormat.GetDateTime(dr["dtBukti"]),
                                    NoBukti  = DataFormat.GetString(dr["NoBukti"]), 
                                    Keterangan  = DataFormat.GetString(dr["sUraian"]),
                                    Debet  = DataFormat.GetInteger(dr["iDebet"]),
                                    Jumlah  = DataFormat.GetDecimal(dr["cJumlah"]),
                                    JenisBelanja  = DataFormat.GetSingle(dr["cJenisBelanja"]),
                                    JenisSumber = DataFormat.GetInteger(dr["JenisSumber"]),
                                    Pajak  = DataFormat.GetSingle(dr["bPajak"]),
                                    IsBKU  = DataFormat.GetInteger(dr["bBKU"]),
                                    Hitung   = DataFormat.GetInteger(dr["bHitung"]),
                                    LevelTampilan  = (E_LEVLETAMPILANBKU) DataFormat.GetInteger(dr["iLevelTampilan"]),
                                    NoUrut  = DataFormat.GetLong(dr["iNourut"]),
                                    NoUrutOnSameNumber  = DataFormat.GetInteger(dr["iNoUrutOnSameNumber"]),
                                    Kodebank  = DataFormat.GetInteger(dr["btIDbank"]),//' Posisi -> 0 Kas, > 0 btKodebank
                                    PPKD  = DataFormat.GetInteger(dr["bPPKD"]),
                                    KelompokSPJ=(KELOMPOK_SPJ)DataFormat.GetInteger(dr["iKelompokSPJ"]),
                                    OnSPJ  = DataFormat.GetInteger(dr["OnSPJ"]),
                                    BNew  = DataFormat.GetInteger (dr["iNourut"])==1? true : false,
                                    IDImport  = DataFormat.GetInteger(dr["iNourut"]),
                                    
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
        public List<BKU> GetByDinasBank(int idDinas, DateTime tanggal, int bank , int bendahara, string tanda="=", int debet=0)
        {
            List<BKU> _lst = new List<BKU>();
            try
            {
                if (bank > -1)
                {
                    SSQL = "SELECT tBKU.* FROM " + m_sNamaTabel + " WHERE IDDInas =" + idDinas.ToString() + " and btJenisBendahara=2 " +
                        " and btJenisBendahara =2 AND dtBukti <=" + tanggal.ToSQLFormat() +
                        "  AND btIDBank " + bank.ToString() + " and btKodeuk " + tanda + bendahara.ToString();
                    if (tanda == "=")
                    {
                        SSQL = "SELECT tBKU.* FROM " + m_sNamaTabel + " WHERE IDDInas =" + idDinas.ToString() + " and btJenisBendahara=2 " +
                        " and btJenisBendahara =2 AND dtBukti <=" + tanggal.ToSQLFormat() +
                        "  AND btIDBank " + bank.ToString();
                    }
                }
                else
                {
                    SSQL = "SELECT tBKU.* FROM " + m_sNamaTabel + " WHERE IDDInas =" + idDinas.ToString() + " and btJenisBendahara=2 " +
                        " and btJenisBendahara =2 AND dtBukti <=" + tanggal.ToSQLFormat() +
                        "  and btKodeuk " + tanda + bendahara.ToString();

                    if (tanda == "=")
                    {
                        SSQL = "SELECT tBKU.* FROM " + m_sNamaTabel + " WHERE IDDInas =" + idDinas.ToString() + " and btJenisBendahara=2 " +
                        " and btJenisBendahara =2 AND dtBukti <=" + tanggal.ToSQLFormat();


                    }
                }
                    if (debet != 0)
                {
                    SSQL = SSQL + " AND idebet =" + debet.ToString(); 
                }
                if (tanda == ">")
                {
                    SSQL = SSQL + " AND JenisSumber in (1,10)";
                }
                else
                {

                }
                    SSQL = SSQL + " Order by NoBKUSKPD,iNoBKU";//

                DataTable dt = new DataTable();

                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new BKU()
                                {

                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Position = DataFormat.GetInteger(dr["Position"]),
                                    // POSITION : 1 -> BUD, 2->SKPD, 3-> BENDAHARA PENERIMAAN
                                    Kodekategori = DataFormat.GetInteger(dr["btKodekategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUk = DataFormat.GetInteger(dr["btKodeUk"]),
                                    KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodekategoriPelaksana"]),
                                    KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIDKegiatan"]),
                                    KodeSubKegiatan = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                                    JenisBendahara = (E_JENISBENDAHARA)DataFormat.GetInteger(dr["btJenisBendahara"]),
                                    NourutSumber = DataFormat.GetLong(dr["iNoUrutSumber"]),
                                    KodeRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    No = DataFormat.GetInteger(dr["iNo"]),
                                    NoBKU = DataFormat.GetInteger(dr["inobku"]),
                                    NoBKUSKPD = DataFormat.GetInteger(dr["NoBKUSKPD"]),
                                    nobkuBUD = DataFormat.GetInteger(dr["iNourut"]),
                                    TanggalTransaksi = DataFormat.GetDateTime(dr["dtBukti"]),
                                    NoBukti = DataFormat.GetString(dr["NoBukti"]),
                                    Keterangan = DataFormat.GetString(dr["sUraian"]),
                                    Debet = DataFormat.GetInteger(dr["iDebet"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    JenisBelanja = DataFormat.GetSingle(dr["cJenisBelanja"]),
                                    JenisSumber = DataFormat.GetInteger(dr["JenisSumber"]),
                                    Pajak = DataFormat.GetSingle(dr["bPajak"]),
                                    IsBKU = DataFormat.GetInteger(dr["bBKU"]),
                                    Hitung = DataFormat.GetInteger(dr["bHitung"]),
                                    LevelTampilan = (E_LEVLETAMPILANBKU)DataFormat.GetInteger(dr["iLevelTampilan"]),
                                    NoUrut = DataFormat.GetLong(dr["iNourut"]),
                                    NoUrutOnSameNumber = DataFormat.GetInteger(dr["iNoUrutOnSameNumber"]),
                                    Kodebank = DataFormat.GetInteger(dr["btIDbank"]),//' Posisi -> 0 Kas, > 0 btKodebank
                                    PPKD = DataFormat.GetInteger(dr["bPPKD"]),
                                    KelompokSPJ = (KELOMPOK_SPJ)DataFormat.GetInteger(dr["iKelompokSPJ"]),
                                    OnSPJ = DataFormat.GetInteger(dr["OnSPJ"]),
                                    BNew = DataFormat.GetInteger(dr["iNourut"]) == 1 ? true : false,
                                    IDImport = DataFormat.GetInteger(dr["iNourut"]),

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
        public BKU Get(int _inourut)
          {
              BKU oBKU = new BKU();
              try
              {
                  SSQL = "SELECT tBKU.*, tSPP.sNoSP2D, tSPP.dtBukukas, tSPP.inourut as NoUrutSPP, tSPP.cJumlah as jumlahSP2D FROM " + m_sNamaTabel + " LEFT OUTER JOIN tSPP ON tSPP.inoBKU = tBKU.inourut WHERE tBKU.inourut = " + _inourut.ToString();
                  
                  //

                  DataTable dt = new DataTable();
                  dt = _dbHelper.ExecuteDataTable(SSQL);
                  if (dt != null)
                  {
                      if (dt.Rows.Count > 0)
                      {
                          DataRow dr = dt.Rows[0];

                          oBKU = new BKU()
                          {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Position  = DataFormat.GetInteger(dr["Position"]),
                                    // POSITION : 1 -> BUD, 2->SKPD, 3-> BENDAHARA PENERIMAAN
                                    Kodekategori  = DataFormat.GetInteger(dr["btKodekategori"]),
                                    KodeUrusan  = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD  = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUk  = DataFormat.GetInteger(dr["btKodeUk"]),
                                    KodekategoriPelaksana  = DataFormat.GetInteger(dr["btKodekategoriPelaksana"]),
                                    KodeUrusanPelaksana  = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    KodeProgram  = DataFormat.GetInteger(dr["btIDProgram"]),
                                    KodeKegiatan  = DataFormat.GetInteger(dr["btIDKegiatan"]),
                                    KodeSubKegiatan  = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                                   
                                    JenisBendahara = (E_JENISBENDAHARA)DataFormat.GetInteger(dr["btJenisBendahara"]),

                                    NourutSumber = DataFormat.GetLong(dr["iNoUrutSumber"]),
                                  //  public  int JenisBaru As E_JENISBARU_REFERENSI
                                    KodeRekening  = DataFormat.GetInteger(dr["IIDRekening"]),
                                
                                    No   = DataFormat.GetInteger(dr["iNo"]),//' Number ...-> Misalnya untuk BP :1 Pengeluaran, 2 Penerimaan
                                                              //'           -> Untul BPP : 1 BPP DInas, 2, BPP BOS
                                    NoBKU  = DataFormat.GetInteger(dr["inobku"]),
                                    NoBKUSKPD  = DataFormat.GetInteger(dr["NoBKUSKPD"]),
                                    nobkuBUD  = DataFormat.GetInteger(dr["iNourut"]),

                                    TanggalTransaksi  = DataFormat.GetDateTime(dr["dtBukti"]),
                                    NoBukti  = DataFormat.GetString(dr["NoBukti"]), 
                                    Keterangan  = DataFormat.GetString(dr["sUraian"]),
                                    Debet  = DataFormat.GetInteger(dr["iDebet"]),
                                    Jumlah  = DataFormat.GetDecimal(dr["cJumlah"]),
                                    JenisBelanja  = DataFormat.GetSingle(dr["cJenisBelanja"]),
                                    JenisSumber = DataFormat.GetInteger(dr["JenisSumber"]),
                                    Pajak  = DataFormat.GetSingle(dr["bPajak"]),
                                    IsBKU  = DataFormat.GetInteger(dr["bBKU"]),
                                    Hitung   = DataFormat.GetInteger(dr["bHitung"]),
                                    LevelTampilan  = (E_LEVLETAMPILANBKU) DataFormat.GetInteger(dr["iLevelTampilan"]),
                                    NoUrut  = DataFormat.GetLong(dr["iNourut"]),
                                    NoUrutOnSameNumber  = DataFormat.GetInteger(dr["iNoUrutOnSameNumber"]),

                                    Kodebank  = DataFormat.GetInteger(dr["btIDbank"]),//' Posisi -> 0 Kas, > 0 btKodebank
                                    PPKD  = DataFormat.GetInteger(dr["bPPKD"]),
                                    
                                    KelompokSPJ=(KELOMPOK_SPJ)DataFormat.GetInteger(dr["iKelompokSPJ"]),
                                    OnSPJ  = DataFormat.GetInteger(dr["OnSPJ"]),
                                    BNew  = DataFormat.GetInteger (dr["iNourut"])==1? true : false,
                                    IDImport  = DataFormat.GetInteger(dr["iNourut"]),
                                    nourutManual  = DataFormat.GetInteger(dr["iNourut"])

                          };
                      }
                  }
                  return oBKU;
              }
              catch (Exception ex)
              {
                  _isError = true;
                  _lastError = ex.Message;
                  return oBKU;
              }

          }
          public bool Hapus(long NoUrut, long NoUrutSumber, int JenisSumber, int JenisBendahara)
          {

              try
              {
                  SSQL = "DELETE  FROM " + m_sNamaTabel + " WHERE  inourut = @NoUrut " + 
                      " AND inourutSumber=@NoUrutSUmber AND btJenisBendahara=@JenisBendahara";
                  DBParameterCollection paramCollection = new DBParameterCollection();
                  paramCollection.Add(new DBParameter("@NoUrut", NoUrut));
                  paramCollection.Add(new DBParameter("@NoUrutSUmber", NoUrutSumber));
              
                  paramCollection.Add(new DBParameter("@JenisBendahara", JenisBendahara));

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

          public bool PerbaikiNoBKU(long NoUrut, int NoBKU ,long NoUrutSumber, int JenisSumber, int JenisBendahara)
          {

              try
              {
                  SSQL = "UPDATE  tBKU Set NoBKUSKPD=@NOBKU WHERE  inourut = @NoUrut " +
                      " AND btJenisBendahara=@JenisBendahara";

                  DBParameterCollection paramCollection = new DBParameterCollection();
                  paramCollection.Add(new DBParameter("@NOBKU", NoBKU));
                  paramCollection.Add(new DBParameter("@NoUrut", NoUrut));
                  //paramCollection.Add(new DBParameter("@JenisSumber", JenisSumber));
                  paramCollection.Add(new DBParameter("@JenisBendahara", JenisBendahara));

                  //paramCollection.Add(new DBParameter("@NoUrutSUmber", NoUrutSumber));

                 
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
        public List<BKU> CariBKU(int pTahun , int pIDDInas, int pKodeKategori , int pKodeUrusan , int  pKodeSKPD ,
                            int pKodeUK , long iNoUrutReferensi , E_JENIS_REFERENSIBKU pJenisRef , int PosUser , int pnoBpp , Single pDebet = 0,
                            E_JENISBENDAHARA  iJenisBandahara = E_JENISBENDAHARA.BENDAHARA_PENGELUARAN) {
                            

               List<BKU> _lst = new List<BKU>();
                try{
                     SSQL = "SELECT * from tBKU where iTahun = " + pTahun.ToString() + " and btKodeKategori = " + pKodeKategori.ToString() + " AND btKodeUrusan =" +
                        pKodeUrusan.ToString() + " AND btKodeSKPD =" + pKodeSKPD.ToString() + " AND btKodeUK =" + pKodeUK.ToString() + " AND JenisSumber=" + ((int)pJenisRef).ToString() +
                        " AND iNoUrutSumber=" + iNoUrutReferensi.ToString() + " AND btJenisBendahara =" + ((int)iJenisBandahara).ToString();

                    if (pDebet != 0 )
                             SSQL = SSQL + " AND idebet=" + pDebet.ToString();

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new BKU()
                                {
                          

                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Position  = DataFormat.GetInteger(dr["Position"]),
                                    // POSITION : 1 -> BUD, 2->SKPD, 3-> BENDAHARA PENERIMAAN
                                    Kodekategori  = DataFormat.GetInteger(dr["btKodekategori"]),
                                    KodeUrusan  = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD  = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUk  = DataFormat.GetInteger(dr["btKodeUk"]),
                                    KodekategoriPelaksana  = DataFormat.GetInteger(dr["btKodekategoriPelaksana"]),
                                    KodeUrusanPelaksana  = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    KodeProgram  = DataFormat.GetInteger(dr["btIDProgram"]),
                                    KodeKegiatan  = DataFormat.GetInteger(dr["btIDKegiatan"]),
                                    KodeSubKegiatan  = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                                   
                                    JenisBendahara = (E_JENISBENDAHARA)DataFormat.GetInteger(dr["btJenisBendahara"]),

                                    NourutSumber = DataFormat.GetLong(dr["iNoUrutSumber"]),
                                  //  public  int JenisBaru As E_JENISBARU_REFERENSI
                                    KodeRekening  = DataFormat.GetInteger(dr["IIDRekening"]),
                                
                                    No   = DataFormat.GetInteger(dr["iNo"]),//' Number ...-> Misalnya untuk BP :1 Pengeluaran, 2 Penerimaan
                                                              //'           -> Untul BPP : 1 BPP DInas, 2, BPP BOS
                                    NoBKU  = DataFormat.GetInteger(dr["inobku"]),
                                    NoBKUSKPD  = DataFormat.GetInteger(dr["NoBKUSKPD"]),
                                    nobkuBUD  = DataFormat.GetInteger(dr["iNourut"]),

                                    TanggalTransaksi  = DataFormat.GetDateTime(dr["dtBukti"]),
                                    NoBukti  = DataFormat.GetString(dr["NoBukti"]), 
                                    Keterangan  = DataFormat.GetString(dr["sUraian"]),
                                    Debet  = DataFormat.GetInteger(dr["iDebet"]),
                                    Jumlah  = DataFormat.GetDecimal(dr["cJumlah"]),
                                    JenisBelanja  = DataFormat.GetSingle(dr["cJenisBelanja"]),
                                    JenisSumber = DataFormat.GetInteger(dr["JenisSumber"]),
                                    Pajak  = DataFormat.GetSingle(dr["bPajak"]),
                                    IsBKU  = DataFormat.GetInteger(dr["bBKU"]),
                                    Hitung   = DataFormat.GetInteger(dr["bHitung"]),
                                    LevelTampilan  = (E_LEVLETAMPILANBKU) DataFormat.GetInteger(dr["iLevelTampilan"]),
                                    NoUrut  = DataFormat.GetLong(dr["iNourut"]),
                                    NoUrutOnSameNumber  = DataFormat.GetInteger(dr["iNoUrutOnSameNumber"]),

                                    Kodebank  = DataFormat.GetInteger(dr["btIDbank"]),//' Posisi -> 0 Kas, > 0 btKodebank
                                    PPKD  = DataFormat.GetInteger(dr["bPPKD"]),
                                    
                                    KelompokSPJ=(KELOMPOK_SPJ)DataFormat.GetInteger(dr["iKelompokSPJ"]),
                                    OnSPJ  = DataFormat.GetInteger(dr["OnSPJ"]),
                                    BNew  = DataFormat.GetInteger (dr["iNourut"])==1? true : false,
                                    IDImport  = DataFormat.GetInteger(dr["iNourut"]),
                                    nourutManual  = DataFormat.GetInteger(dr["iNourut"])
                        }).ToList();
                    
                 
                    }    
                }
                return _lst;
            }catch(Exception ex){
                _lastError = ex.Message;
                 return null;
            }
        }
        public decimal GetSaldo(int idDInas, int jenisBendahara, 
                                DateTime tanggalawal, 
                                DateTime tanggalAkhir)
        {
            try
            {
                decimal d;
                SSQL = "SELECT sum(idebet * cjumlah) as saldo from tBKU where iddinas= @dinas and dtbukti>=@TANGGALAWAL and dtbukti<=@TANGGALAKHIR and btJenisBendahara=2 ";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@dinas", idDInas));
                paramCollection.Add(new DBParameter("@TANGGALAWAL", tanggalawal, DbType.Date));
                paramCollection.Add(new DBParameter("@TANGGALAKHIR", tanggalAkhir, DbType.Date));
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                decimal saldo =0;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        saldo = DataFormat.GetDecimal(dr["saldo"]);
                    }
                }
                return saldo;
                

                
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;

                return 0;
            }

        }
                                
        
        public List<BKUDISPLAY> GetBKU(int idDInas, int jenisBendahara, 
                                DateTime tanggalawal, 
                                DateTime tanggalAkhir, 
                                int KodeUK, List<int> lstJenisSumber,
                                long nOrotSUmber =0,int bank =-1, long idrekening=0)
        {

            try
            {

                SSQL = "SELECT IDDInas,"+
                     "inobku,inourut,JenisSumber," +
                    "[iNoBKU],[NoBKUSKPD], 0 as URUT, [dtBukti], [NoBukti], [IIDrekening], [sUraian] , [btIDbank], [iDebet], [cJumlah] ," +
                    "[iNoUrutSumber], [iLevelTampilan], 1 as [ilevel], [cJenisBelanja] , tBKU.bPajak ,iNoUrutOnSameNumber,0 as IdSubKegiatan,btJenisBendahara from [tBKU]  where " +
                    " [iTahun] =@TAHUN   AND btJenisBendahara= @JENISBENDAHARA ";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@TAHUN", Tahun));
                paramCollection.Add(new DBParameter("@JENISBENDAHARA", jenisBendahara));
                if (nOrotSUmber > 0)
                {
                    SSQL = SSQL + " AND iNoUrutSumber=@NOURUTSUMBER";
                    paramCollection.Add(new DBParameter("@NOURUTSUMBER", nOrotSUmber));
                }


                if (idDInas != 0)
                {
                    SSQL = SSQL + " AND IDDInas =@DINAS " ;
                    paramCollection.Add(new DBParameter("@DINAS", idDInas));

                    if (KodeUK > 0)
                    {
                        SSQL = SSQL + " AND btKodeUK =@KodeUK";
                        paramCollection.Add(new DBParameter("@KodeUK", KodeUK));
                    }
                }

                if (lstJenisSumber.Count > 0)
                {
                    string sNamaParameter="";
                    SSQL = SSQL + " AND JenisSumber in ( ";
                    foreach (int js in lstJenisSumber)
                    {   
                        sNamaParameter ="@JenisSumber"+js.ToString ();
                        SSQL= SSQL + sNamaParameter +",";
                          paramCollection.Add(new DBParameter(sNamaParameter, js));
                    }

                    SSQL = SSQL + "@99)";
                    paramCollection.Add(new DBParameter("@99", 99));


                }
                
                SSQL = SSQL + " AND [dtBukti]  >= @TANGGALAWAL AND [dtBukti] <= @TANGGALAKHIR";
                paramCollection.Add(new DBParameter("@TANGGALAWAL", tanggalawal,DbType.Date));
                paramCollection.Add(new DBParameter("@TANGGALAKHIR", tanggalAkhir, DbType.Date));

                if (jenisBendahara == 0)
                {
                    SSQL = SSQL + " AND isnull(inobku,0)> 0 ";
                }
                if (bank > -1)
                {
                    SSQL = SSQL + " AND btIDbank= @BANK ";
                    paramCollection.Add(new DBParameter("@BANK", bank, DbType.Int32));
                }
                //if (idrekening > 0)
                //{
                //    SSQL = SSQL + " AND btIDbank= @BANK ";
                //    paramCollection.Add(new DBParameter("@BANK", bank, DbType.Int32));

                //}
                SSQL = SSQL + " UNION ALL ";
                SSQL = SSQL + " SELECT tbku.IDDInas, tbku.inobku,[tBKU].[inourut],[tBKU].[JenisSumber], "+
                              " [tBKU].[iNoBKU],[tBKU].[NoBKUSKPD], 1 as URUT, [tBKU].[dtBukti], [tBKU].[NoBukti], [tBKURekening].[IIDrekening],  " +
                              " [mRekening].[sNamaRekening] as sUraian , [tBKU].[btIDbank], [tBKU].[iDebet], [tBKURekening].[cJumlah] ,  " +
                              " [tBKU].[iNoUrutSumber],[tBKU].[iLevelTampilan], 2 as [ilevel], [tBKU].[cJenisBelanja] , tBKU.bPajak,iNoUrutOnSameNumber,tBKURekening.IdSubKegiatan,tBKU.btJenisBendahara " +
                              " from (([tBKU] INNER JOIN [tBKURekening]  ON [TBKU].[inourut]= [tBKURekening].[inourut] )  " +
                              "  INNER JOIN mRekening ON [tBKURekening].[IIDrekening]= mRekening.IIDRekening)   " +
                              " Where iLevelTampilan = 2 AND " +
                              " [tBKU].[iTahun] =  @TAHUN AND btJenisBendahara= @JENISBENDAHARA ";

                //if (jenisBendahara != 0)
                //{
                //    SSQL = SSQL + " AND IDDInas = @DINAS ";// +idDInas.ToString();


                //}
                if (idDInas != 0)
                {

                    SSQL = SSQL + " AND tBKU.IDDInas =@DINAS ";

                    if (KodeUK > 0)
                    {
                        SSQL = SSQL + " AND tBKU.btKodeUK =@KodeUK";
                        
                    }
                }
                if (nOrotSUmber > 0)
                {
                    SSQL = SSQL + " AND iNoUrutSumber=@NOURUTSUMBER";
                
                }


                if (lstJenisSumber.Count > 0)
                {
                    string sNamaParameter = "";
                    SSQL = SSQL + " AND JenisSumber in ( ";
                    foreach (int js in lstJenisSumber)
                    {
                        sNamaParameter = "@JenisSumber" + js.ToString();
                        SSQL = SSQL + sNamaParameter + ",";
                      
                    }

                    SSQL = SSQL + "@99)";
                 


                }

                SSQL = SSQL + " AND [dtBukti]  >=@TANGGALAWAL AND [dtBukti] <= @TANGGALAKHIR  ";// +tanggalawal.ToSQLFormat() + " AND [dtBukti] <= " + tanggalAkhir.ToSQLFormat();

                if (jenisBendahara == 0)
                {
                    SSQL = SSQL + " AND isnull(inobku,0)> 0 ";
                }
                if (bank > -1)
                {
                    SSQL = SSQL + " AND tBKU.btIDbank= @BANK ";
        
                }
                SSQL = SSQL + " ORDER BY tBKU.NoBKUSKPD,tBKU.iNoBKU, [inourutsumber],JenisSumber, iNoUrutOnSameNumber,iLevel,tBKU.btJenisBendahara, IIDRekening";
                //SSQL = SSQL + " ORDER BY tBKU.inobku,Urut,[inourutsumber], JenisSumber,iNoUrutOnSameNumber,iLevel, IIDRekening";

                List<BKUDISPLAY> lst = new List<BKUDISPLAY>();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new BKUDISPLAY()
                               {
                                   IDDinas = DataFormat.GetInteger(dr["IDDinas"]),
                                   NoBkU = DataFormat.GetInteger(dr["NoBKUSKPD"]),
                                   NoBkUSKPD = DataFormat.GetInteger(dr["NoBKUSKPD"]),
                                   NoBukti = DataFormat.GetString(dr["NoBukti"]),
                                   Tanggal = DataFormat.GetDateTime(dr["dtBukti"]),//.ToString ("dd MMM yyyy"),
                                   NoUrut = DataFormat.GetLong(dr["iNourut"]),
                                   NoUrutSumber = DataFormat.GetLong(dr["iNourutSumber"]),
                                   JenisSumber = DataFormat.GetInteger(dr["JenisSumber"]),
                                   Uraian = DataFormat.GetString(dr["sUraian"]),
                                   Bank = DataFormat.GetInteger(dr["btIDbank"]),
                                 //  IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                   IDRekening = DataFormat.GetLong(dr["iIDRekening"]),
                                   Debet = DataFormat.GetInteger(dr["iDebet"]),
                                   Penerimaan = DataFormat.GetInteger(dr["iDebet"]) == 1 ? DataFormat.GetDecimal(dr["cJumlah"]) : 0,
                                   Pengeluaran = DataFormat.GetInteger(dr["iDebet"]) == -1 ? DataFormat.GetDecimal(dr["cJumlah"]) : 0,
                                 //  KodeUK = DataFormat.GetInteger(dr["btIDSubkegiatan"]),
                                   Level = DataFormat.GetInteger(dr["iLevel"]),
                                   LevelTampilan = DataFormat.GetInteger(dr["iLevelTampilan"]),
                                   IdSubKegiatan = DataFormat.GetLong(dr["IdSubKegiatan"]),
                                   JenisBendahara = DataFormat.GetInteger(dr["btJenisBendahara"]),
                                   Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),

                               }).ToList();


                    }
                }
                foreach (BKUDISPLAY b in lst)
                {
                    if (b.NoUrut==242120100172100370)
                    {
                        jenisBendahara = 1;
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return null;

            }

        }
        public bool UpdateKelompok(int iddinas, int JenisBendahara ,DateTime tanggalAwal, DateTime TanggalAkhir)
        {

            try
            {
                SSQL = "";
                DBParameterCollection paramCollection = new DBParameterCollection();

                SSQL = " UPDATE tBKU SET KElompok = 1 where JenisSumber in (1,3,5,6, 2,21,22) AND  iTahun = @TAHUN ";
                paramCollection.Add(new DBParameter("@TAHUN", Tahun));

                if (JenisBendahara > 0)
                {
                    SSQL = SSQL + " AND tBKU.IDDInas =@DINAS ";
                    paramCollection.Add(new DBParameter("@DINAS", iddinas));

                }                                             //     SSQL = SSQL & " AND btKodekategori = " & g_nKodeKategori & _
                SSQL = SSQL + " AND tBKU.btJenisBendahara  =@JenisBendahara";                                                             //            " AND btKodeUrusan = " & g_nKodeUrusan & " AND btKodeSKPD = " & g_nKodeSKPD
                SSQL = SSQL + " AND dtBukti  >= @TANGGALAWAL AND dtBukti <= @TANGGALAKHIR";

                paramCollection.Add(new DBParameter("@JenisBendahara", JenisBendahara));
                
                
                paramCollection.Add(new DBParameter("@TANGGALAWAL", tanggalAwal, DbType.Date));
                paramCollection.Add(new DBParameter("@TANGGALAKHIR", TanggalAkhir, DbType.Date));
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);






                SSQL = " UPDATE tBKU SET KElompok = 2 where JenisSumber in (10,9,24, 13,20,12,13,22,23) and  iTahun = @TAHUN";
                if (JenisBendahara > 0)
                {
                    SSQL = SSQL + " AND tBKU.IDDInas =@DINAS ";
                    //paramCollection.Add(new DBParameter("@DINAS", Tahun));

                }

                SSQL = SSQL + " AND tBKU.btJenisBendahara  =@JenisBendahara";                                                             //            " AND btKodeUrusan = " & g_nKodeUrusan & " AND btKodeSKPD = " & g_nKodeSKPD
                SSQL = SSQL + " AND dtBukti  >= @TANGGALAWAL AND dtBukti <= @TANGGALAKHIR";


                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);



                SSQL = " UPDATE tBKU SET KElompok = 3 where JenisSumber in (7,20) and  iTahun = @TAHUN";
                if (JenisBendahara > 0)
                {
                    SSQL = SSQL + " AND tBKU.IDDInas =@DINAS ";
                    //paramCollection.Add(new DBParameter("@DINAS", Tahun));

                }

                SSQL = SSQL + " AND tBKU.btJenisBendahara  =@JenisBendahara";                                                             //            " AND btKodeUrusan = " & g_nKodeUrusan & " AND btKodeSKPD = " & g_nKodeSKPD
                SSQL = SSQL + " AND dtBukti  >= @TANGGALAWAL AND dtBukti <= @TANGGALAKHIR";

                
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                //SSQL = " UPDATE tBKU SET KElompok = 4 where JenisSumber in (5,14,17,16,8,9,11,25) and  iTahun = @TAHUN";

                //if (JenisBendahara > 0)
                //{
                //    SSQL = SSQL + " AND tBKU.IDDInas =@DINAS ";
                //    //paramCollection.Add(new DBParameter("@DINAS", Tahun));

                //}

                //SSQL = SSQL + " AND tBKU.btJenisBendahara  =@JenisBendahara";                                                             //            " AND btKodeUrusan = " & g_nKodeUrusan & " AND btKodeSKPD = " & g_nKodeSKPD
                //SSQL = SSQL + " AND dtBukti  >= @TANGGALAWAL AND dtBukti <= @TANGGALAKHIR";


                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);





                SSQL = " UPDATE tBKU SET KElompok = 5 where JenisSumber in (17) and  iTahun = @TAHUN";

                if (JenisBendahara > 0)
                {
                    SSQL = SSQL + " AND tBKU.IDDInas =@DINAS ";
       

                }

                SSQL = SSQL + " AND tBKU.btJenisBendahara  =@JenisBendahara";                                                             //            " AND btKodeUrusan = " & g_nKodeUrusan & " AND btKodeSKPD = " & g_nKodeSKPD
                SSQL = SSQL + " AND dtBukti  >= @TANGGALAWAL AND dtBukti <= @TANGGALAKHIR";


                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

            
        }

        public List<BKUDISPLAY> GetBKUUntukDiUrutkan(int iddinas, int JenisBendahara, DateTime tanggalAwal, DateTime TanggalAkhir)
        {
            List<BKUDISPLAY> lst = new List<BKUDISPLAY>();

            try
            {
                SSQL = "";
                DBParameterCollection paramCollection = new DBParameterCollection();

                SSQL = " Select * From tBKU where iTahun = @TAHUN ";
                paramCollection.Add(new DBParameter("@TAHUN", Tahun));

                if (JenisBendahara > 0)
                {
                    SSQL = SSQL + " AND tBKU.IDDInas =@DINAS ";
                    paramCollection.Add(new DBParameter("@DINAS", iddinas));

                }                                             //     SSQL = SSQL & " AND btKodekategori = " & g_nKodeKategori & _
                SSQL = SSQL + " AND tBKU.btJenisBendahara  =@JenisBendahara";                                                             //            " AND btKodeUrusan = " & g_nKodeUrusan & " AND btKodeSKPD = " & g_nKodeSKPD
                SSQL = SSQL + " AND dtBukti  >= @TANGGALAWAL AND dtBukti <= @TANGGALAKHIR";
                paramCollection.Add(new DBParameter("@JenisBendahara", JenisBendahara));
                
                paramCollection.Add(new DBParameter("@TANGGALAWAL", tanggalAwal, DbType.Date));
                paramCollection.Add(new DBParameter("@TANGGALAKHIR", TanggalAkhir, DbType.Date));
                SSQL = SSQL + " ORDER BY  dtBukti asc,  inourutsumber asc, Kelompok asc,  iDebet desc, JenisSumber asc, " +
                 " iNoUrutOnSameNumber asc";
                //ORDER BY dtBukti ASC ,Kelompok, REPLACE(Nobukti, ' ', ''),inourutsumber ,JenisSumber,iNoUrutOnSameNumber asc 

                DataTable dt = new DataTable();

                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new BKUDISPLAY()
                               {
                                   IDDinas = DataFormat.GetInteger(dr["IDDinas"]),
                                   NoBkU = DataFormat.GetInteger(dr["iNoBKU"]),
                                   NoBkUSKPD = DataFormat.GetInteger(dr["NoBKUSKPD"]),
                                   NoBukti = DataFormat.GetString(dr["NoBukti"]),
                                   Tanggal = DataFormat.GetDateTime(dr["dtBukti"]),//.ToString ("dd MMM yyyy"),
                                   NoUrut = DataFormat.GetLong(dr["iNourut"]),
                                   NoUrutSumber = DataFormat.GetLong(dr["iNourutSumber"]),
                                   JenisSumber = DataFormat.GetInteger(dr["iNoBKU"]),
                                   Uraian = DataFormat.GetString(dr["sUraian"]),
                                   Bank = DataFormat.GetInteger(dr["btIDbank"]),
                                  Level= 1,
                                   Debet = DataFormat.GetInteger(dr["iDebet"]),
                                   Penerimaan = DataFormat.GetInteger(dr["iDebet"]) == 1 ? DataFormat.GetDecimal(dr["cJumlah"]) : 0,
                                   Pengeluaran = DataFormat.GetInteger(dr["iDebet"]) == -1 ? DataFormat.GetDecimal(dr["cJumlah"]) : 0,
                                   KodeUK = DataFormat.GetInteger(dr["btIDSubkegiatan"]),
                                 
                                   LevelTampilan = DataFormat.GetInteger(dr["iLevelTampilan"]),
                            
                                   JenisBendahara = DataFormat.GetInteger(dr["btJenisBendahara"]),
                                   Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                               }).ToList();


                    }
                }
                return lst;

            }
            catch (Exception ex)
            {
                return lst;
            }


        }

          public BKUINFO GetInfo(int idDInas, E_JENISBENDAHARA iJenisendahara, DateTime tanggalawal, DateTime tanggalakhir, int bPPKD, int jenisSumber = -1)
          {
              
              BKUINFO bi = new BKUINFO();
              //decimal JumlahTerima = 0;
              //decimal JumlahKeluar = 0;
              //decimal JumlahTerimalalu = 0;
              //decimal JumlahKeluarLalu = 0;
              //decimal JumlahBank = 0;




              SSQL = "SELECT SUM(case when dtBukti < @TANGGALAWALTAHUN THEN cJumlah ELSE 0 END) as saldoAwal," +
                      "SUM(case when dtBukti>= @TANGGALAWALTAHUN and dtBukti <  @TANGGALAWAL AND iDebet =1 THEN cJumlah ELSE 0 END) as PenerimaanLalu, " +
                     "SUM(case when dtBukti >=  @TANGGALAKHIR and dtBukti <  @TANGGALAWAL  AND iDebet =-1 THEN cJumlah ELSE 0  END)as PengeluaranLalu, " +
                     "SUM(case when dtBukti>= @TANGGALAWAL AND dtBukti<=  @TANGGALAKHIR   AND iDebet =1 THEN cJumlah ELSE  0 END) as PenerimaanKini, " +
                     "SUM(case when dtBukti>= @TANGGALAWAL AND dtBukti<= @TANGGALAKHIR AND iDebet =-1  THEN cJumlah ELSE  0 END) as PengeluaranKini, " +
                     "SUM(case when dtBukti>= @TANGGALAKHIR AND dtBukti<= @TANGGALAKHIR AND btIDbank =1  THEN idebet * cJumlah ELSE  0 END) as Saldobank, " +
                     "SUM(case when dtBukti>= @TANGGALAKHIR AND dtBukti<= @TANGGALAKHIR AND btIDbank =0  THEN idebet * cJumlah ELSE  0 END) as SaldoTunai " +
              " from [tBKU]  where " +
              " [iTahun] =@TAHUN AND btJenisBendahara= @JENISBENDAHARA";
              if (iJenisendahara != E_JENISBENDAHARA.BENDAHARA_BUD)
              {
                  SSQL = SSQL + " AND IDDInas = @DINAS";
                  

              }     

              if (iJenisendahara == E_JENISBENDAHARA.BENDAHARA_BUD)
              {
                  SSQL = SSQL + " AND isnull(inobku,0)> 0 ";
              }


                List<BKUDISPLAY> lst = new List<BKUDISPLAY>();
                DataTable dt = new DataTable();

                DateTime awalTahun = new DateTime(Tahun, 1, 1);
               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@TANGGALAWALTAHUN", awalTahun,DbType.Date ));
               paramCollection.Add(new DBParameter("@TANGGALAWAL", tanggalawal, DbType.Date));
               paramCollection.Add(new DBParameter("@TANGGALAKHIR", tanggalakhir, DbType.Date));
               paramCollection.Add(new DBParameter("@TAHUN", Tahun));
               paramCollection.Add(new DBParameter("@JENISBENDAHARA",(int)iJenisendahara));
               paramCollection.Add(new DBParameter("@DINAS", idDInas));


               dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        BKUINFO bkuinfo = new BKUINFO()
                               {
                                   SaldoAwal = DataFormat.GetDecimal(dr["saldoAwal"]),
                                   JumlahBank = DataFormat.GetDecimal(dr["Saldobank"]),
                                   JumlahTerima = DataFormat.GetDecimal(dr["PenerimaanKini"]),
                                   JumlahKeluar = DataFormat.GetDecimal(dr["PengeluaranKini"]),
                                   JumlahKeluarLalu = DataFormat.GetDecimal(dr["PengeluaranLalu"]),
                                   JumlahTerimalalu = DataFormat.GetDecimal(dr["PenerimaanLalu"]),
                                   JumlahTunai = DataFormat.GetDecimal(dr["SaldoTunai"])

                               };
                        return bkuinfo;
                    }

                }
                return null;
          }
    }
}
