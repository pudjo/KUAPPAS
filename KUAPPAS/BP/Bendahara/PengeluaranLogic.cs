using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DTO.Bendahara;
using System.Text;
using System.Data;
using System.Data.OleDb;
using Formatting;
using BP;
using DataAccess;







namespace BP.Bendahara
{
    public class PengeluaranLogic:BP 
    {
        IDbConnection m_connection;
        IDbTransaction m_objTrans ;

        public PengeluaranLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tPanjar";
            m_connection = null;
            m_objTrans = null;

        }
        public bool Hapus(long nourut)
        {
           
            m_connection = _dbHelper.CreateCOnnection();
            m_objTrans = m_connection.BeginTransaction();

            SSQL = "";
            long m_noUrut;
            try
            {
              
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    SSQL = "DELETE tPanjar WHERE iNoUrut=@NoUrut";                
                    paramCollection.Add(new DBParameter("@NoUrut", nourut));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);

                     SSQL = "DELETE tPanjarRekening WHERE iNoUrut=@NoUrut";                



                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);
                    
                         SSQL = "DELETE tPanjarPotongan WHERE iNoUrut=@NoUrut";

                         _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);
                   SSQL = "DELETE tBKURekening from tBKU INNER JOIN tBKURekening on TBKU.inourut = tBKUREkening.inourut " + 
                       " WHERE tBKU.inourutSumber =@NoUrut";
                   
                  _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);
                   SSQL = "DELETE tBKU " + 
                       " WHERE tBKU.inourutSumber =@NoUrut";                   
                  _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);
                

                  SSQL = "DELETE tJurnalRekening from tJurnal INNER JOIN tJurnalRekening  on tJurnalRekening.inojurnal = tJurnal.inojurnal " + 
                       " WHERE iSumber =@NoUrut";                   
                  _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);

              
                     SSQL = "DELETE tBukubesar  WHERE tBukuBesar.iSumber =@NoUrut";  
                  _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);
                
                     SSQL = "DELETE tJurnal  WHERE iSumber =@NoUrut";            
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);
                



              m_objTrans.Commit();
              m_connection.Close();
              return true;
            }
               
            catch (Exception ex)
            {
                m_objTrans.Rollback();
                m_connection.Close();
                return false;
            }
        }
        public bool SPJKanBelanja(List<BelanjaLPJUP> listBelanjaDipiliih , long noSPJ)
        {
            try
            {
                foreach (BelanjaLPJUP b in listBelanjaDipiliih)
                {
                    SSQL = "UPDATE tPanjar set iStatus= 1, inourutspjup =@NoURutSPJ WHERE inourut =@NoUrut";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@NoURutSPJ", noSPJ));
                    paramCollection.Add(new DBParameter("@NoUrut", b.NoUrut));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                
                }
                return true;

            } catch( Exception ex
                )
            {
                _lastError = ex.Message;
                return false;
            }
            
        }
        public bool BatalkanSPJUP(long noSPJ)
        {
            try
            {

                SSQL = "UPDATE tPanjar set iStatus= 0, inourutspjup =0 WHERE inourut =@NoURutSPJ";
                    
                DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@NoURutSPJ", noSPJ));
                    

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                
                return true;

            }
            catch (Exception ex
                )
            {
                _lastError = ex.Message;
                return false;
            }

        }
        public List<PotonganPanjar> GetPotongan(long pNoUrut)
        {
            try
            {
                List<PotonganPanjar> lst = new List<PotonganPanjar>();
                SSQL = "SELECT tPanjarPotongan.*, tSetor.sNoBukti FROM tPanjarPotongan Left Join tSetor On tPanjarPotongan.iNoSetorPajak = tSetor.inourut "+
                        " where tPanjarPotongan.inourut =@NoUrut";
                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("@NoUrut", pNoUrut));
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        lst = (from DataRow dr in dt.Rows
                               select new PotonganPanjar()
                                {
                                 NoUrut = DataFormat.GetLong(dr["inourut"]),
                                 NoBuktiSetor= DataFormat.GetString(dr["sNoBukti"]),
                                 IIDRekening= DataFormat.GetLong(dr["IIDrekening"]),
                                 Jumlah = DataFormat.GetLong(dr["cJumlah"]),
                                 NTPN = DataFormat.GetString(dr["sNoNTPN"]),
                                 KodeBilling = DataFormat.GetString(dr["KodeBilling"]),
                                 NoUrutSetor = DataFormat.GetLong(dr["INoSetorPajak"]),
                                 StatusPajak = DataFormat.GetInteger(dr["iStatusPajak"]),
                        }).ToList();
                        
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }

        }

        public Pengeluaran GetByID(long lNoUrut)
        {
            try
            {
                Pengeluaran oPenggeluaran = new Pengeluaran();            
                SSQL = "SELECT * FROM tPanjar where inourut =@NoUrut";
                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("@NoUrut", lNoUrut));
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);     
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        DataRow dr = dt.Rows[0];
                        oPenggeluaran = new Pengeluaran
                        {
                            NoUrut = DataFormat.GetLong(dr["inourut"]),
                            Tahun = DataFormat.GetInteger(dr["iTahun"]),
                            Jenis =(E_JENISPENGELUARAN)DataFormat.GetInteger(dr["btJenis"]),
                            Kodekategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                            KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                            KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                            KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                            KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                            KodeurusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                            KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                            Kodekegiatan = DataFormat.GetInteger(dr["btIDKegiatan"]),
                            KodeSubKegiatan = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                            Tanggal = DataFormat.GetDateTime(dr["dtBukukas"]),
                            NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                            Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                            Uraian = DataFormat.GetString(dr["sUraian"]),
                            Penerima = DataFormat.GetString(dr["sNama"]),
                            NoUrutSPJUP = DataFormat.GetLong(dr["inourutspjup"]),
                            PPKD = DataFormat.GetInteger(dr["bppkd"]),
                            Status = DataFormat.GetInteger(dr["iStatus"]),
                            JenisBelanja = DataFormat.GetInteger(dr["btJenisBelanja"]),
                            Global = DataFormat.GetInteger(dr["bGlobal"]),
                            JumlahDikembalikan = DataFormat.GetDecimal(dr["cJumlahDikembalikan"]),
                            Kodebank = DataFormat.GetInteger(dr["btIDbank"]),
                            NoReferensi = DataFormat.GetInteger(dr["iNoRef"]),
                            StatusPajak = DataFormat.GetInteger(dr["iStatusPajak"]),
                            NoUrutSetorPajak = DataFormat.GetInteger(dr["iNoSetorPajak"]),
                            NoPungut = DataFormat.GetString(dr["sNopungut"]),
                            IDBank = DataFormat.GetInteger(dr["btIDbank"]),
                        //    nourutManual = DataFormat.GetInteger(dr["rsPanjar!inourutmanual"]),
                            IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                            IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                            IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                            IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                            IDSUbKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                            UnitAnggaran = DataFormat.GetInteger(dr["UnitAnggaran"]),
                            idcrt = DataFormat.GetInteger(dr["idcrt"]),
                            tcrt = DataFormat.GetDateTime(dr["dcrt"]),
                            tahap=  DataFormat.GetInteger(dr["tahap"]),
                        };
                        return oPenggeluaran;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }

        }

        public List<PengeluaranDanRekening> GetUntukLPJ(ParameterBendahara p)
        {
            List<PengeluaranDanRekening> _lst = new List<PengeluaranDanRekening>();
            try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();

                 SSQL= " SELECT tPanjar.inourut,tPanjar.iTahun,tPanjar.btJenis,tPanjar.dtBukukas,tPanjar.sNoBukti "+ 
                ",tPanjarRekening.cJumlah as Jumlah,tPanjar.sUraian,tPanjar.sNama,tPanjar.inourutspjup,tPanjar.iStatus,tPanjar.btKodeUK," +
                 " tPanjar.IDDInas,tPanjar.IDUrusan,tPanjar.IDProgram,tPanjar.IDKegiatan,tPanjar.IDSubKegiatan,tPanjarRekening.IIDRekening" + 
                 ",mRekening.sNamaRekening as NamaRekening from tPanjar INNER Join tPanjarRekening on tPanjar.inourut = tPanjarRekening.Inourut "+
                    " INNER JOIN mRekening on mRekening.IIDRekening=tPanjarRekening.IIDRekening WHERE tPanjar.IDDInas = @IDDInas ";
                
                paramCollection.Add(new DBParameter("@IDDInas", p.IDDInas));
                

                if (p.NoUrutSPJ == 0)
                {
                    if (p.NoUrutSP2D> 0)
                    {
                        SSQL = SSQL + " AND tPanjar.iNoUrutSPP=@nourutP2d and btJenisBelanja=2 ";
                        paramCollection.Add(new DBParameter("@nourutP2d", p.NoUrutSP2D, DbType.Int64));

                    }
                    else
                    {
                        SSQL = SSQL + " AND  tPanjar.btJenisBelanja<=1  AND dtBukukas between @TanggalAwal and  @TanggalAkhir and tPanjar.iStatus=0 ";
                        paramCollection.Add(new DBParameter("@TanggalAwal", p.TanggalAwal, DbType.Date));
                        paramCollection.Add(new DBParameter("@TanggalAkhir", p.TanggalAkhir, DbType.Date));
                    }
                }
                else
                {
                    SSQL = SSQL + " AND tPanjar.iNoUrutSPJUP=@NOURUTSPJ ";
                    paramCollection.Add(new DBParameter("@NOURUTSPJ", p.NoUrutSPJ,DbType.Int64 ));
                    

                }
                SSQL = SSQL + "AND btJenis in (3,4)";
        
                    SSQL = SSQL + " Union All SELECT tKoreksi.inourut,tKoreksi.iTahun,0 as btJenis,tKoreksi.dtKoreksi as dtBukukas,tKoreksi.sNoBukti " +
                    ",-1 * (tKoreksiDetail.iDebet1  *  tKoreksiDetail.cJumlah1 ) as Jumlah,tKoreksi.sUraian,'' as sNama,tKoreksi.inourutspjup,tKoreksi.iStatus,tKoreksi.btKodeUK," +
                     " tKoreksi.IDDInas,tKoreksiDetail.IDUrusan,tKoreksiDetail.IDProgram,tKoreksiDetail.IDKegiatan,tKoreksiDetail.IDSubKegiatan," +
                      " tKoreksiDetail.IIDRekening1 as IIDrekening, " +
                     " mRekening.sNamaRekening as NamaRekening from tKoreksi INNER Join tKoreksiDetail on tKoreksi.inourut = tKoreksiDetail.Inourut " +
                      " INNER JOIN mRekening on mRekening.IIDRekening=tKoreksiDetail.IIDRekening1 WHERE tKoreksi.IDDInas = @IDDInas ";

                    if (p.NoUrutSPJ == 0)
                    {
                        if (p.NoUrutSP2D > 0)
                        {

                            SSQL = SSQL + " and tKoreksi.inourutSumber in( select inourut from Tpanjar where ";
                            SSQL = SSQL + " tPanjar.iNoUrutSPP=@nourutP2d  ) ";
                         //   paramCollection.Add(new DBParameter("@nourutP2d", p.NoUrutSP2D, DbType.Int64));

                        }
                        else
                        {
                            SSQL = SSQL + "   AND tKoreksi.dtKoreksi between @TanggalAwal and  @TanggalAkhir and tKoreksi.iStatus=0 ";
                        }
                    }
                    else
                    {
                        SSQL = SSQL + " AND tKoreksi.iNoUrutSPJUP=@NOURUTSPJ ";


                    }




                SSQL = SSQL + " ORDER BY dtBukukas, sNobukti";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PengeluaranDanRekening()
                                {
                                    NoUrut = DataFormat.GetLong(dr["inourut"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    Jenis = (E_JENISPENGELUARAN)DataFormat.GetInteger(dr["btJenis"]),
                                  
                                    Tanggal = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"]),
                                    Uraian = DataFormat.GetString(dr["sUraian"]),
                                    Penerima = DataFormat.GetString(dr["sNama"]),
                                    NoUrutSPJUP = DataFormat.GetLong(dr["inourutspjup"]),
                                    Status = DataFormat.GetInteger(dr["iStatus"]),
                                    KodeUK =DataFormat.GetInteger(dr["btKodeUK"]),
              
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSUbKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),                                   
                                    Nama= DataFormat.GetString(dr["NamaRekening"]),
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

        public List<Pengeluaran> Get(ParameterBendahara p, bool withPotongan= false )
        {
            List<Pengeluaran> _lst = new List<Pengeluaran>();
            try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                SSQL = "SELECT * from tPanjar WHERE IDDInas = @IDDInas  ";
                paramCollection.Add(new DBParameter("@IDDInas", p.IDDInas));

                
                if (p.NoUrut > 0)
                {
                    SSQL = SSQL + " AND inourut =@NoUrut ";
                    paramCollection.Add(new DBParameter("@NoUrut", p.NoUrut));
                    

                }
                else
                {

                    SSQL = SSQL + " AND dtBukukas between @TanggalAwal and  @TanggalAkhir ";

                    paramCollection.Add(new DBParameter("@TanggalAwal", p.TanggalAwal,DbType.Date));
                    paramCollection.Add(new DBParameter("@TanggalAkhir", p.TanggalAkhir, DbType.Date));
                   // SSQL = SSQL + " AND tPanjar.inourut in(24074020400043732,24074020400043733,24074020400043734,24074020400043735,24074020400043736)";
                    if (p.IDKegiatan > 0)
                    {
                        SSQL = SSQL + "AND IDKegiatan=@pIDKegiatan";
                        paramCollection.Add(new DBParameter("@pIDKegiatan", p.IDKegiatan));
                    }
                    
                    if (p.Jenis > -1)
                    {
                        SSQL = SSQL + "AND btJenis =@Jenis";
                        paramCollection.Add(new DBParameter("@Jenis", p.Jenis));// p.Jenis));
                    }
                    if (p.LstJenis != null)
                    {
                        if (p.LstJenis.Count > 0)
                        {
                            string sNamaParameter;
                            SSQL = SSQL + " and btJenis in (";
                            foreach (int jenis in p.LstJenis)
                            {
                                sNamaParameter = "@jenis" + jenis.ToString();
                                SSQL = SSQL + sNamaParameter + ",";

                                paramCollection.Add(new DBParameter(sNamaParameter, jenis, DbType.Int32));

                            }

                            paramCollection.Add(new DBParameter("@status99", 99, DbType.Int32));
                            SSQL = SSQL + "@status99)";
                        }
                    }
                }
               
                SSQL=SSQL+ " ORDER BY dtBukukas, sNobukti";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Pengeluaran()
                                {
                                    NoUrut = DataFormat.GetLong(dr["inourut"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    Jenis = (E_JENISPENGELUARAN)DataFormat.GetInteger(dr["btJenis"]),
                                    Kodekategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    KodeurusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    Kodekegiatan = DataFormat.GetInteger(dr["btIDKegiatan"]),
                                    KodeSubKegiatan = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                                    Tanggal = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Uraian = DataFormat.GetString(dr["sUraian"]),
                                    Penerima = DataFormat.GetString(dr["sNama"]),
                                    NoUrutSPJUP = DataFormat.GetLong(dr["inourutspjup"]),
                                    NoUrutSPP = DataFormat.GetLong(dr["iNoUrutSPP"]),
                                    PPKD = DataFormat.GetInteger(dr["bppkd"]),
                                    Status = DataFormat.GetInteger(dr["iStatus"]),
                                    IDBank = DataFormat.GetInteger(dr["btIDbank"]),
                                    JenisBelanja = DataFormat.GetInteger(dr["btJenisBelanja"]),
                                    Global = DataFormat.GetInteger(dr["bGlobal"]),
                                    JumlahDikembalikan = DataFormat.GetDecimal(dr["cJumlahDikembalikan"]),
                                    Kodebank = DataFormat.GetInteger(dr["btIDbank"]),
                                    NoReferensi = DataFormat.GetLong(dr["iNoRef"]),
                                    StatusPajak = DataFormat.GetInteger(dr["iStatusPajak"]),
                                    NoUrutSetorPajak = DataFormat.GetInteger(dr["iNoSetorPajak"]),
                                    NoPungut = DataFormat.GetString(dr["sNopungut"]),
                                    nourutManual = DataFormat.GetInteger(dr["inourutmanual"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSUbKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                            idcrt = DataFormat.GetInteger(dr["idcrt"]),
                            tcrt =  DataFormat.GetDateTime(dr["dcrt"]),
                                    UnitAnggaran = DataFormat.GetInteger(dr["UnitAnggaran"]),
                                    tahap = DataFormat.GetInteger(dr["tahap"]),

                                }).ToList();
                    }
                }
                if (withPotongan== true){
                    int id = 0;
                    foreach(Pengeluaran pengeluaran in _lst){
                        Console.WriteLine(pengeluaran.NoUrut.ToString());
                        _lst[id].Potongans = GetPotongan(pengeluaran.NoUrut);
                        id++;

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

        public List<Pengeluaran> GetUntukDiPertanggungjawabkan(int iddinas)
        {
            List<Pengeluaran> _lst = new List<Pengeluaran>();
            try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                SSQL = "SELECT * from tPanjar WHERE IDDInas = @IDDInas  and btJenis=1  ";
                paramCollection.Add(new DBParameter("@IDDInas", iddinas));


                //////if (p.NoUrut > 0)
                //////{
                //////    SSQL = SSQL + " AND inourut =@NoUrut ";
                //////    paramCollection.Add(new DBParameter("@NoUrut", p.NoUrut));


                //////}
                //////else
                //////{

                //////    SSQL = SSQL + " AND dtBukukas between @TanggalAwal and  @TanggalAkhir ";

                //////    paramCollection.Add(new DBParameter("@TanggalAwal", p.TanggalAwal, DbType.Date));
                //////    paramCollection.Add(new DBParameter("@TanggalAkhir", p.TanggalAkhir, DbType.Date));
                
                //////}

                SSQL = SSQL + " ORDER BY dtBukukas, sNobukti";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Pengeluaran()
                                {
                                    NoUrut = DataFormat.GetLong(dr["inourut"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    Jenis = (E_JENISPENGELUARAN)DataFormat.GetInteger(dr["btJenis"]),
                                    Kodekategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    KodeurusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    Kodekegiatan = DataFormat.GetInteger(dr["btIDKegiatan"]),
                                    KodeSubKegiatan = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                                    Tanggal = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Uraian = DataFormat.GetString(dr["sUraian"]),
                                    Penerima = DataFormat.GetString(dr["sNama"]),
                                    NoUrutSPJUP = DataFormat.GetLong(dr["inourutspjup"]),
                                    NoUrutSPP = DataFormat.GetLong(dr["iNoUrutSPP"]),
                                    PPKD = DataFormat.GetInteger(dr["bppkd"]),
                                    Status = DataFormat.GetInteger(dr["iStatus"]),
                                    IDBank = DataFormat.GetInteger(dr["btIDbank"]),
                                    JenisBelanja = DataFormat.GetInteger(dr["btJenisBelanja"]),
                                    Global = DataFormat.GetInteger(dr["bGlobal"]),
                                    JumlahDikembalikan = DataFormat.GetDecimal(dr["cJumlahDikembalikan"]),
                                    Kodebank = DataFormat.GetInteger(dr["btIDbank"]),
                                    NoReferensi = DataFormat.GetLong(dr["iNoRef"]),
                                    StatusPajak = DataFormat.GetInteger(dr["iStatusPajak"]),
                                    NoUrutSetorPajak = DataFormat.GetInteger(dr["iNoSetorPajak"]),
                                    NoPungut = DataFormat.GetString(dr["sNopungut"]),
                                    nourutManual = DataFormat.GetInteger(dr["inourutmanual"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSUbKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    idcrt = DataFormat.GetInteger(dr["idcrt"]),
                                    tcrt = DataFormat.GetDateTime(dr["dcrt"]),
                                    UnitAnggaran = DataFormat.GetInteger(dr["UnitAnggaran"]),
                                    tahap = DataFormat.GetInteger(dr["tahap"]),

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


        public List<Pengeluaran> GetUntukKoreksi(ParameterBendahara p, bool withPotongan = false)
        {
            List<Pengeluaran> _lst = new List<Pengeluaran>();
            try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                SSQL = "SELECT * from tPanjar WHERE IDDInas = @IDDInas  ";
                paramCollection.Add(new DBParameter("@IDDInas", p.IDDInas));


                if (p.NoUrut > 0)
                {
                    SSQL = SSQL + " AND inourut =@NoUrut ";
                    paramCollection.Add(new DBParameter("@NoUrut", p.NoUrut));


                }
                else
                {

                    SSQL = SSQL + " AND dtBukukas between @TanggalAwal and  @TanggalAkhir ";

                    paramCollection.Add(new DBParameter("@TanggalAwal", p.TanggalAwal, DbType.Date));
                    paramCollection.Add(new DBParameter("@TanggalAkhir", p.TanggalAkhir, DbType.Date));
                    // SSQL = SSQL + " AND tPanjar.inourut in(24074020400043732,24074020400043733,24074020400043734,24074020400043735,24074020400043736)";
                    if (p.IDKegiatan > 0)
                    {
                        SSQL = SSQL + "AND IDKegiatan=@pIDKegiatan";
                        paramCollection.Add(new DBParameter("@pIDKegiatan", p.IDKegiatan));
                    }

                    if (p.Jenis > -1)
                    {
                        SSQL = SSQL + "AND btJenis  in (3,4)";// untuk koreksi
                        
                    }
                    if (p.LstJenis != null)
                    {
                        if (p.LstJenis.Count > 0)
                        {
                            string sNamaParameter;
                            SSQL = SSQL + " and btJenis in (";
                            foreach (int jenis in p.LstJenis)
                            {
                                sNamaParameter = "@jenis" + jenis.ToString();
                                SSQL = SSQL + sNamaParameter + ",";

                                paramCollection.Add(new DBParameter(sNamaParameter, jenis, DbType.Int32));

                            }

                            paramCollection.Add(new DBParameter("@status99", 99, DbType.Int32));
                            SSQL = SSQL + "@status99)";
                        }
                    }
                }
               
                SSQL = SSQL + " ORDER BY dtBukukas, sNobukti";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Pengeluaran()
                                {
                                    NoUrut = DataFormat.GetLong(dr["inourut"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    Jenis = (E_JENISPENGELUARAN)DataFormat.GetInteger(dr["btJenis"]),
                                    Kodekategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    KodeurusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    Kodekegiatan = DataFormat.GetInteger(dr["btIDKegiatan"]),
                                    KodeSubKegiatan = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                                    Tanggal = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Uraian = DataFormat.GetString(dr["sUraian"]),
                                    Penerima = DataFormat.GetString(dr["sNama"]),
                                    NoUrutSPJUP = DataFormat.GetLong(dr["inourutspjup"]),
                                    NoUrutSPP = DataFormat.GetLong(dr["iNoUrutSPP"]),
                                    PPKD = DataFormat.GetInteger(dr["bppkd"]),
                                    Status = DataFormat.GetInteger(dr["iStatus"]),
                                    IDBank = DataFormat.GetInteger(dr["btIDbank"]),
                                    JenisBelanja = DataFormat.GetInteger(dr["btJenisBelanja"]),
                                    Global = DataFormat.GetInteger(dr["bGlobal"]),
                                    JumlahDikembalikan = DataFormat.GetDecimal(dr["cJumlahDikembalikan"]),
                                    Kodebank = DataFormat.GetInteger(dr["btIDbank"]),
                                    NoReferensi = DataFormat.GetLong(dr["iNoRef"]),
                                    StatusPajak = DataFormat.GetInteger(dr["iStatusPajak"]),
                                    NoUrutSetorPajak = DataFormat.GetInteger(dr["iNoSetorPajak"]),
                                    NoPungut = DataFormat.GetString(dr["sNopungut"]),
                                    nourutManual = DataFormat.GetInteger(dr["inourutmanual"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSUbKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    idcrt = DataFormat.GetInteger(dr["idcrt"]),
                                    tcrt = DataFormat.GetDateTime(dr["dcrt"]),
                                    UnitAnggaran = DataFormat.GetInteger(dr["UnitAnggaran"]),
                                    tahap = DataFormat.GetInteger(dr["tahap"]),
                                }).ToList();
                    }
                }
                if (withPotongan == true)
                {
                    int id = 0;
                    foreach (Pengeluaran pengeluaran in _lst)
                    {
                        Console.WriteLine(pengeluaran.NoUrut.ToString());
                        _lst[id].Potongans = GetPotongan(pengeluaran.NoUrut);
                        id++;

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

        public List<Pengeluaran> GetForJurnal(ParameterBendahara p, bool withPotongan = false)
        {
            List<Pengeluaran> _lst = new List<Pengeluaran>();
            try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                SSQL = "SELECT tPanjar.*, vwJurnalRekeningAnggaran.KodeSKPD as InJurnal FROM tPanjar  LEFT JOIN vwJurnalRekeningAnggaran "+
                      " ON tPanjar.inourut = vwJurnalRekeningAnggaran.iSumber WHERE tPanjar.IDDInas = @IDDInas  ";
                paramCollection.Add(new DBParameter("@IDDInas", p.IDDInas));


                if (p.NoUrut > 0)
                {
                    SSQL = SSQL + " AND tPanjar.inourut =@NoUrut ";
                    paramCollection.Add(new DBParameter("@NoUrut", p.NoUrut));


                }
                else
                {

                    SSQL = SSQL + " AND tPanjar.dtBukukas between @TanggalAwal and  @TanggalAkhir ";

                    paramCollection.Add(new DBParameter("@TanggalAwal", p.TanggalAwal, DbType.Date));
                    paramCollection.Add(new DBParameter("@TanggalAkhir", p.TanggalAkhir, DbType.Date));
                    if (p.IDKegiatan > 0)
                    {
                        SSQL = SSQL + "AND tPanjar.IDKegiatan=@pIDKegiatan";
                        paramCollection.Add(new DBParameter("@pIDKegiatan", p.IDKegiatan));
                    }

                    if (p.Jenis > -1)
                    {
                        SSQL = SSQL + "AND tPanjar.btJenis =@Jenis";
                        paramCollection.Add(new DBParameter("@Jenis", p.Jenis));// p.Jenis));
                    }
                    if (p.LstJenis != null)
                    {
                        if (p.LstJenis.Count > 0)
                        {
                            string sNamaParameter;
                            SSQL = SSQL + " and tPanjar.btJenis in (";
                            foreach (int jenis in p.LstJenis)
                            {
                                sNamaParameter = "@jenis" + jenis.ToString();
                                SSQL = SSQL + sNamaParameter + ",";

                                paramCollection.Add(new DBParameter(sNamaParameter, jenis, DbType.Int32));

                            }

                            paramCollection.Add(new DBParameter("@status99", 99, DbType.Int32));
                            SSQL = SSQL + "@status99)";
                        }
                    }
                }

                SSQL = SSQL + " ORDER BY tPanjar.dtBukukas,tPanjar.inourut, tPanjar.sNobukti";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Pengeluaran()
                                {
                                    NoUrut = DataFormat.GetLong(dr["inourut"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    Jenis = (E_JENISPENGELUARAN)DataFormat.GetInteger(dr["btJenis"]),
                                    Tanggal = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Uraian = DataFormat.GetString(dr["sUraian"]),
                                    PPKD = DataFormat.GetInteger(dr["bppkd"]),
                                    Status = DataFormat.GetInteger(dr["InJurnal"]),
                                    IDBank = DataFormat.GetInteger(dr["btIDbank"]),
                                    JenisBelanja = DataFormat.GetInteger(dr["btJenisBelanja"]),
                                    Global = DataFormat.GetInteger(dr["bGlobal"]),
                                    JumlahDikembalikan = DataFormat.GetDecimal(dr["cJumlahDikembalikan"]),
                                    Kodebank = DataFormat.GetInteger(dr["btIDbank"]),
                                    NoReferensi = DataFormat.GetLong(dr["iNoRef"]),
                                    StatusPajak = DataFormat.GetInteger(dr["iStatusPajak"]),
                                    NoUrutSetorPajak = DataFormat.GetInteger(dr["iNoSetorPajak"]),
                                    NoPungut = DataFormat.GetString(dr["sNopungut"]),
                                    nourutManual = DataFormat.GetInteger(dr["inourutmanual"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSUbKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    idcrt = DataFormat.GetInteger(dr["idcrt"]),
                                    tcrt = DataFormat.GetDateTime(dr["dcrt"]),
                                    UnitAnggaran = DataFormat.GetInteger(dr["UnitAnggaran"]),

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
        public List<Pengeluaran> GetUntukBukuPanjar(ParameterBendahara p)
        {
            List<Pengeluaran> _lst = new List<Pengeluaran>();
            try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                SSQL = "SELECT * from tPanjar WHERE IDDInas = @IDDInas  ";
                paramCollection.Add(new DBParameter("@IDDInas", p.IDDInas));

                if (p.KodeUK > 0)
                {
                    SSQL = SSQL + " AND btKodeUK = @KODEUK";
                    paramCollection.Add(new DBParameter("@KODEUK", p.KodeUK));

                }

                if (p.NoUrut > 0)
                {
                    SSQL = SSQL + " AND inourut =@NoUrut ";
                    paramCollection.Add(new DBParameter("@NoUrut", p.NoUrut));


                }
                else
                {

                    SSQL = SSQL + " AND dtBukukas between @TanggalAwal and  @TanggalAkhir ";

                    paramCollection.Add(new DBParameter("@TanggalAwal", p.TanggalAwal, DbType.Date));
                    paramCollection.Add(new DBParameter("@TanggalAkhir", p.TanggalAkhir, DbType.Date));
                    if (p.IDKegiatan > 0)
                    {
                        SSQL = SSQL + "AND IDKegiatan=@pIDKegiatan";
                        paramCollection.Add(new DBParameter("@pIDKegiatan", p.IDKegiatan));
                    }
                    //if (p.Jenis > -1)
                    //{
                        SSQL = SSQL + "AND btJenis in (1,2,4)";
                        //paramCollection.Add(new DBParameter("@Jenis", 1));// p.Jenis));
                   // }


                }

                SSQL = SSQL + " ORDER BY dtBukukas, sNobukti";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Pengeluaran()
                                {
                                    NoUrut = DataFormat.GetLong(dr["inourut"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    Jenis = (E_JENISPENGELUARAN)DataFormat.GetInteger(dr["btJenis"]),
                                    Kodekategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    KodeurusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    Kodekegiatan = DataFormat.GetInteger(dr["btIDKegiatan"]),
                                    KodeSubKegiatan = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                                    Tanggal = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Uraian = DataFormat.GetString(dr["sUraian"]),
                                    Penerima = DataFormat.GetString(dr["sNama"]),
                                    NoUrutSPJUP = DataFormat.GetLong(dr["inourutspjup"]),
                                    PPKD = DataFormat.GetInteger(dr["bppkd"]),
                                    Status = DataFormat.GetInteger(dr["iStatus"]),
                                    IDBank = DataFormat.GetInteger(dr["btIDbank"]),
                                    JenisBelanja = DataFormat.GetInteger(dr["btJenisBelanja"]),
                                    Global = DataFormat.GetInteger(dr["bGlobal"]),
                                    JumlahDikembalikan = DataFormat.GetDecimal(dr["cJumlahDikembalikan"]),
                                    Kodebank = DataFormat.GetInteger(dr["btIDbank"]),
                                    NoReferensi = DataFormat.GetLong(dr["iNoRef"]),
                                    StatusPajak = DataFormat.GetInteger(dr["iStatusPajak"]),
                                    NoUrutSetorPajak = DataFormat.GetInteger(dr["iNoSetorPajak"]),
                                    NoPungut = DataFormat.GetString(dr["sNopungut"]),
                                    nourutManual = DataFormat.GetInteger(dr["inourutmanual"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSUbKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    idcrt = DataFormat.GetInteger(dr["idcrt"]),
                                    tcrt = DataFormat.GetDateTime(dr["dcrt"]),
                                    UnitAnggaran = DataFormat.GetInteger(dr["UnitAnggaran"]),

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

        public List<Pengeluaran> GetPanjarDanPertanggungJawabannya(ParameterBendahara p)
        {
            List<Pengeluaran> _lst = new List<Pengeluaran>();
            try
            {
             
                SSQL = "SELECT inourut as U, * from tPanjar WHERE IDDInas = " + p.IDDInas.ToString() + " AND btJenis = " + p.Jenis.ToString() +
                        " UNION SELECT inoref as U, * from tPanjar WHERE IDDInas = " + p.IDDInas.ToString() + " AND btJenis = 4 " +
                        " ORDER BY U, sNobukti";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Pengeluaran()
                                {
                                    NoReferensi = DataFormat.GetLong(dr["U"]),
                                    NoUrut = DataFormat.GetLong(dr["inourut"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    Jenis = (E_JENISPENGELUARAN)DataFormat.GetInteger(dr["btJenis"]),
                                     
                                    Kodekategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    KodeurusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    Kodekegiatan = DataFormat.GetInteger(dr["btIDKegiatan"]),
                                    KodeSubKegiatan = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                                    Tanggal = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Uraian = DataFormat.GetString(dr["sUraian"]),
                                    Penerima = DataFormat.GetString(dr["sNama"]),
                                    NoUrutSPJUP = DataFormat.GetLong(dr["inourutspjup"]),
                                    PPKD = DataFormat.GetInteger(dr["bppkd"]),
                                    IDBank = DataFormat.GetInteger(dr["btIDbank"]),
                                    Status = DataFormat.GetInteger(dr["iStatus"]),
                                    JenisBelanja = DataFormat.GetInteger(dr["btJenisBelanja"]),
                                    Global = DataFormat.GetInteger(dr["bGlobal"]),
                                    JumlahDikembalikan = DataFormat.GetDecimal(dr["cJumlahDikembalikan"]),
                                    Kodebank = DataFormat.GetInteger(dr["btIDbank"]),
                                  //  NoReferensi = DataFormat.GetInteger(dr["iNoRef"]),
                                    StatusPajak = DataFormat.GetInteger(dr["iStatusPajak"]),
                                    NoUrutSetorPajak = DataFormat.GetInteger(dr["iNoSetorPajak"]),
                                    NoPungut = DataFormat.GetString(dr["sNopungut"]),
                                    //    nourutManual = DataFormat.GetInteger(dr["rsPanjar!inourutmanual"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                 //   IDSUbKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),

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
        public List<PengeluaranRekening> GetDetail(long inourut)
        {
            List<PengeluaranRekening> _lst = new List<PengeluaranRekening>();
            try
            {
                SSQL = "SELECT tPanjar.IDSUBkegiatan ,  tPanjarRekening.IIDRekening, mRekening.sNamaRekening, tPanjarRekening.cJumlah FROM tPanjarRekening "+
                    " INNER JOIN tPanjar on tPanjar.inourut= tPanjarRekening.inoUrut " +
                 " INNER JOIN mRekening ON tPanjarRekening.IIDRekening = mRekening.IIDRekening where  tPanjar.iNourut= " + inourut.ToString() +
                    " ORDER BY IIDRekening ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PengeluaranRekening()
                                {
                                    
                                    Nama= DataFormat.GetString(dr["sNamaRekening"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    IDRekening= DataFormat.GetLong(dr["IIDRekening"]),
                                    IDSUbKegiatan = DataFormat.GetLong(dr["IDSUBkegiatan"]),
                                    
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
        private BKU GetOldBKU(Pengeluaran p, 
                            List<BKU> lstBKU, 
                            E_JENISBENDAHARA JenisBendahara, 
                            int JenisSumber, int debet)
        {
            

            BKU oldBKU = lstBKU.FirstOrDefault(b => b.NourutSumber ==  p.NoUrut && 
                                                        b.IDDinas== p.IDDInas  && 
                                                        b.JenisBendahara == JenisBendahara &&
                                                        b.JenisSumber == JenisSumber  &&
                                                        b.Debet ==debet 
                                                        );

            return oldBKU;

        }
        private bool UpdateMxBKU(List<BKU> lstMaxNoBKU,
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

        //public bool CatatBKU(Pengeluaran  pengeluaran)
        //{
        //    BKULogic oBKULogic = new BKULogic(Tahun);
        //    List<BKU> lstBKU = new List<BKU>();           
        //    List<long> lstNoUrut = new List<long>();

        //    lstNoUrut.Add(pengeluaran.NoUrut);
        //    lstBKU = oBKULogic.GetBKUByNoUrutSumber(lstNoUrut,2);
            
        //    //BKU Terakhir
        //    // mengambi no bku yang sudah ada... 
        //    BKU MaxNoBKU = new BKU();
        //    MaxNoBKU = oBKULogic.GetBKUDenganMaxNoBKU(pengeluaran.IDDInas, pengeluaran.KodeUK);
        //    // Jika beklum ada BKU
        //    if (MaxNoBKU == null)
        //    {
        //        MaxNoBKU.NoBKUSKPD = 1;
        //        MaxNoBKU.NoBKU = 1;
        //        MaxNoBKU.NoUrutSaja = 1;

        //    }
        //     SimpanBKU(ref pengeluaran, lstBKU, MaxNoBKU);               


        //    return true;

        //}

        //public  bool  SimpanBKU(ref Pengeluaran p , List<BKU> lstBKU, 
        //                       BKU MaxNoBKU//, 
        //                       //IDbConnection connection, IDbTransaction odbTrans
        //                       )
        //{ 
        //    try{

        //        BKULogic oLogic = new BKULogic(Tahun);   
             
        //        BKU oBKU = new BKU ();
        //        oBKU.CreateFormPengeluaran(p, 2);
        //        BKU oldBKU = GetOldBKU(p,
        //                            lstBKU, 
                             
        //                     E_JENISBENDAHARA.BENDAHARA_PENGELUARAN,
        //                     oBKU.JenisSumber);
        //        oBKU.JenisBendahara = E_JENISBENDAHARA.BENDAHARA_PENGELUARAN;
                
        //        if (oldBKU != null)
        //        {
        //            oBKU.NoUrut = oldBKU.NoUrut;
        //            oBKU.NoBKU = oldBKU.NoBKU;
        //            oBKU.NoBKUSKPD = oldBKU.NoBKUSKPD;
        //        }
        //        else
        //        {
        //            oBKU.NoUrut = 0;
        //            oBKU.NoBKU = MaxNoBKU.NoBKU + 1;
        //            oBKU.NoBKUSKPD = MaxNoBKU.NoBKUSKPD + 1;
        //            oBKU.NoUrutSaja = MaxNoBKU.NoUrutSaja + 1;

                    
        //        }


        //        if (oLogic.Simpan(ref oBKU) == true)
        //        {
        //            lstBKU.Add(oBKU);
        //            return true;
        //        }
        //        else
        //        {
        //            return false ;
        //        }

                


        //    } catch(Exception ex){
        //        return false;
        //    }

        //}
        public bool SimpanBKU(ref Pengeluaran p, List<BKU> lstBKU,
                                 ref BKU MaxNoBKU, 
                                 IDbConnection connection, IDbTransaction odbTrans
                               )
        {
            try
            {

                BKULogic oLogic = new BKULogic(Tahun);
                BKU oBKU = new BKU();
                BKU oldBKU = new BKU();
                bool berhasilBKU = true;
               if (p.Jenis == E_JENISPENGELUARAN.PERTANGGUNGJAWABAN_PANJAR )
                {
                    oBKU.CreateFormPengeluaran(p, 2,true);
                    oldBKU = GetOldBKU(p,
                                        lstBKU,
                                 E_JENISBENDAHARA.BENDAHARA_PENGELUARAN,
                                 oBKU.JenisSumber,1);
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
                        oBKU.NoBKU = MaxNoBKU.NoBKU;
                        oBKU.NoBKUSKPD = MaxNoBKU.NoBKUSKPD;
                        oBKU.NoUrutSaja = MaxNoBKU.NoUrutSaja;


                    }
                    if (oLogic.Simpan(ref oBKU, connection, odbTrans) == false)
                    {
                        berhasilBKU = berhasilBKU && false;
                    }



                }
                int debet = -1;
                if (p.Jenis == E_JENISPENGELUARAN.PENGEMBALIAN_PANJAR)
                {
                    debet = 1;
                }
                oBKU.CreateFormPengeluaran(p, 2);
                oldBKU = GetOldBKU(p,
                                    lstBKU,
                                   E_JENISBENDAHARA.BENDAHARA_PENGELUARAN,
                             oBKU.JenisSumber,debet );
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
                    oBKU.NoBKU = MaxNoBKU.NoBKU;
                    oBKU.NoBKUSKPD = MaxNoBKU.NoBKUSKPD;
                    oBKU.NoUrutSaja = MaxNoBKU.NoUrutSaja;


                }


                if (oLogic.Simpan(ref oBKU,connection, odbTrans) == false)
                {
                    berhasilBKU = berhasilBKU && false;
                }

                return berhasilBKU;
                
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        private bool SimpanBKUPotongan(ref Pengeluaran p, List<BKU> lstBKU,
                               ref  BKU MaxNoBKU,
                               IDbConnection connection, IDbTransaction odbTrans
                               )
        {
            try
            {

                BKULogic oLogic = new BKULogic(Tahun);
                BKU oBKU = new BKU();
                oBKU.CreateFormPotonganPengeluaran(p, 2);
               
                BKU oldBKU = GetOldBKU(p,
                                    lstBKU,                                 
                             E_JENISBENDAHARA.BENDAHARA_PENGELUARAN,
                             oBKU.JenisSumber,1);

                oBKU.JenisBendahara = E_JENISBENDAHARA.BENDAHARA_PENGELUARAN;
                if (oldBKU != null)
                {
                    oBKU.NoUrut = oldBKU.NoUrut;
                    oBKU.NoBKU = oldBKU.NoBKU;
                    oBKU.NoBKUSKPD = oldBKU.NoBKUSKPD;
                }
                else
                {
                    oBKU.NoUrut =0;
                    oBKU.NoBKU = MaxNoBKU.NoBKU + 1;
                    oBKU.NoBKUSKPD = MaxNoBKU.NoBKUSKPD + 1;
                    oBKU.NoUrutSaja = MaxNoBKU.NoUrutSaja+1;
                    
                }


     
                if (oLogic.Simpan(ref oBKU, connection, odbTrans) == true)
                {
                    lstBKU.Add(oBKU);

                    return true;
                } else 
                   return false;
          }
          catch (Exception ex)
          {
                return false;
          }

        }
        private bool Jurnal()
        {
            return true;

        }
        public List<clsObject> GetJumlah(int idDinas, int JenisSumber, DateTime tanggalAwal, DateTime tanggalakhir)
        {
            decimal dRet = 0;
            try
            {
                List<clsObject> lst = new List<clsObject>();
                SSQL = "SELECT Inourut, sNoBukti, dtBukukas ,sum(tPanjar.cJumlah) as Jumlah from tPanjar   " +
                    " WHERE tPanjar.IDDInas =" + idDinas.ToString() +
                     " AND tPanjar.dtBukukas >=" + tanggalAwal.ToSQLFormat() + " AND tPanjar. dtBukukas <=" + tanggalakhir.ToSQLFormat() +
                     " Group by Inourut ,sNoBukti, dtBukukas ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new clsObject()
                               {
                                   NoUrut = DataFormat.GetLong(dr["inourut"]),
                                   NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                   
                                   Jumlah = DataFormat.GetLong(dr["Jumlah"]),
                                   Tanggal= DataFormat.GetDate(dr["dtBukukas"]),                              
                               }).ToList();

                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;

            }
        }
        public decimal GetJumlahDetail(int idDinas, int JenisSumber, DateTime tanggalAwal, DateTime tanggalakhir)
        {
            decimal dRet = 0;
            try
            {

                SSQL = "SELECT sum(tPanjarRekening.cJumlah) as Jumlah from tPanjar   inner join tPanjarRekening "+
                    " ON tPanjar.inourut= tPanjarRekening.inourut WHERE tPanjar.IDDInas =" + idDinas.ToString() +
                     " AND tPanjar.dtBukukas >=" + tanggalAwal.ToSQLFormat() + " AND tPanjar.dtBukukas <=" + tanggalakhir.ToSQLFormat();

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
        public decimal GetJumlahPungutPajak(int idDinas, int JenisSumber, DateTime tanggalAwal, DateTime tanggalakhir)
        {
            decimal dRet = 0;
            try
            {

                SSQL = "SELECT sum(tPanjarPotongan.cJumlah) as Jumlah from tPanjar inner join tPanjarPotongan on tPanjar.inourut = tPanjarPotongan.inourut "+ 
                    " WHERE tPanjar.IDDInas =" + idDinas.ToString() +
                     " AND tPanjar.dtBukukas >=" + tanggalAwal.ToSQLFormat() + " AND tPanjar.dtBukukas <=" + tanggalakhir.ToSQLFormat();

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
        public decimal GetJumlahPungutPajakDetail(int idDinas, int JenisSumber, DateTime tanggalAwal, DateTime tanggalakhir)
        {
            decimal dRet = 0;
            try
            {

                SSQL = "SELECT sum(tPanjarPotongan.cJumlah) as Jumlah from tPanjar inner join tPanjarPotongan on tPanjar.inourut = tPanjarPotongan.inourut " +
                    " WHERE tPanjar.IDDInas =" + idDinas.ToString() +
                     " AND tPanjar.dtBukukas >=" + tanggalAwal.ToSQLFormat() + " AND tPanjar.dtBukukas <=" + tanggalakhir.ToSQLFormat();

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
        public long  Simpan(ref Pengeluaran pengeluaran)
        {

            
            BKULogic oBKULogic = new BKULogic(Tahun);
            List<BKU> lstBKU = new List<BKU>();
            BKU MaxNoBKU = new BKU();    
            List<long> lstNoUrut = new List<long>();          
            if (pengeluaran.NoUrut > 0)
            {
                lstNoUrut.Add(pengeluaran.NoUrut);

            }
            lstBKU = oBKULogic.GetBKUByNoUrutSumber(lstNoUrut,2);
            MaxNoBKU = oBKULogic.GetBKUDenganMaxNoBKU(pengeluaran.IDDInas, pengeluaran.KodeUK);
            m_connection = _dbHelper.CreateCOnnection();
            m_objTrans = m_connection.BeginTransaction();

            SSQL = "";
            long m_noUrut;
            try
            {
                if (pengeluaran.NoUrut == 0)
                {
                    
                    //m_noUrut = GetNoUrut(c);
                    m_noUrut = DataFormat.GetLong(
                                   GetNoUrut(E_KOLOM_NOURUT.CON_URUT_PANJAR, Tahun, pengeluaran.IDDInas));

                    SSQL = "INSERT INTO tPanjar(iNoUrut,sNoBukti,dtBukuKas,cJumlah,iTahun,btKodeKategori,btKodeUrusan,btKodeSKPD" +
                                    ",btKodeUK,btKodeKategoriPelaksana,btKodeUrusanPelaksana,btIDProgram,btIDKegiatan,sNama," +
                                    "iStatus,btJenis,sUraian,iNoUrutSPP,bPPKD, inourutBAST, btJenisBelanja,btIDSUbKegiatan," +
                                    "cJumlahDikembalikan,btIDbank,iNoRef,iStatusPajak,IDUrusan ,idDinas ,idProgram ,IDkegiatan," +
                                    "IDSubkegiatan,sNoPungut, TahapEM, RealisasiFisik,idcrt,dcrt ,UnitAnggaran, tahap ) values (" +
                                    " @NoUrut,@NoBukti,@dtBukuKas,@Jumlah,@Tahun,@KodeKategori,@KodeUrusan,@KodeSKPD" +
                                    ",@KodeUK,@KodeKategoriPelaksana,@KodeUrusanPelaksana,@btIDProgram,@btIDKegiatan,@Nama," +
                                    "@Status,@Jenis,@Uraian,@NoUrutSPP,@PPKD, @nourutBAST, @JenisBelanja,@btIDSUbKegiatan, @JumlahDikembalikan," +
                                    "@IDbank,@iNoRef,@iStatusPajak,@IDUrusan ,@idDinas ,@idProgram ,@IDkegiatan,@IDSubkegiatan,@sNoPungut, @TahapEM, @RealisasiFisik,@idcrt,@dcrt,@UnitAnggaran,@tahap)";

                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("@NoUrut", m_noUrut, DbType.Int64));
                    paramCollection.Add(new DBParameter("@NoBukti", pengeluaran.NoBukti, DbType.String));
                    paramCollection.Add(new DBParameter("@dtBukuKas", pengeluaran.Tanggal,DbType.Date));
                    paramCollection.Add(new DBParameter("@Jumlah", pengeluaran.Jumlah,DbType.Decimal));
                    paramCollection.Add(new DBParameter("@Tahun", pengeluaran.Tahun, DbType.Int32));
                    paramCollection.Add(new DBParameter("@KodeKategori", pengeluaran.Kodekategori, DbType.Int32));
                    paramCollection.Add(new DBParameter("@KodeUrusan", pengeluaran.KodeUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@KodeSKPD", pengeluaran.KodeSKPD, DbType.Int32));
                    paramCollection.Add(new DBParameter("@KodeUK", pengeluaran.KodeUK, DbType.Int32));
                    paramCollection.Add(new DBParameter("@KodeKategoriPelaksana", pengeluaran.KodekategoriPelaksana, DbType.Int32));
                    paramCollection.Add(new DBParameter("@KodeUrusanPelaksana", pengeluaran.KodeurusanPelaksana, DbType.Int32));
                    paramCollection.Add(new DBParameter("@btIDProgram", pengeluaran.KodeProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@btIDKegiatan", pengeluaran.Kodekegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@Nama", pengeluaran.Penerima, DbType.String));
                    paramCollection.Add(new DBParameter("@Status", 0, DbType.Int32));
                    paramCollection.Add(new DBParameter("@Jenis", (int)pengeluaran.Jenis, DbType.Int32));
                    paramCollection.Add(new DBParameter("@Uraian", pengeluaran.Uraian, DbType.String));
                    paramCollection.Add(new DBParameter("@NoUrutSPP", pengeluaran.NoUrutSPP, DbType.Int64));
                    paramCollection.Add(new DBParameter("@PPKD", 0, DbType.Int32));
                    paramCollection.Add(new DBParameter("@nourutBAST", pengeluaran.NoUrutBAST, DbType.Int64));
                    paramCollection.Add(new DBParameter("@JenisBelanja", pengeluaran.JenisBelanja, DbType.Int32));
                    paramCollection.Add(new DBParameter("@btIDSUbKegiatan", pengeluaran.KodeSubKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@JumlahDikembalikan", pengeluaran.JumlahDikembalikan, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@IDbank", pengeluaran.IDBank, DbType.Int32));
                    paramCollection.Add(new DBParameter("@iNoRef", pengeluaran.NoReferensi, DbType.Int64));
                    paramCollection.Add(new DBParameter("@iStatusPajak", pengeluaran.StatusPajak, DbType.Int32));
                    paramCollection.Add(new DBParameter("@IDUrusan", pengeluaran.IDUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@idDinas", pengeluaran.IDDInas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@idProgram", pengeluaran.IDProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@IDkegiatan", pengeluaran.IDKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@IDSubkegiatan", pengeluaran.IDSUbKegiatan, DbType.Int64));
                    
                    paramCollection.Add(new DBParameter("@sNoPungut", pengeluaran.NoPungut, DbType.String));
                    paramCollection.Add(new DBParameter("@TahapEM", pengeluaran.TahapEM, DbType.Int32));
                    paramCollection.Add(new DBParameter("@RealisasiFisik", pengeluaran.RealisasiFisik, DbType.Int32));
                    paramCollection.Add(new DBParameter("@idcrt", pengeluaran.idcrt, DbType.Int32));
                    paramCollection.Add(new DBParameter("@dcrt", DateTime.Now.Date, DbType.DateTime));
                    paramCollection.Add(new DBParameter("@UnitAnggaran", pengeluaran.UnitAnggaran, DbType.Int32));
                    paramCollection.Add(new DBParameter("@tahap", pengeluaran.tahap, DbType.Int32));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);
                    pengeluaran.NoUrut = m_noUrut;





                }
                else
                {
                    m_noUrut = pengeluaran.NoUrut;

                    SSQL = "UPDATE tPanjar SET sNoBukti=@NoBukti,dtBukuKas=@dtBukuKas,cJumlah=@Jumlah,iTahun=@Tahun,btKodeKategori=@KodeKategori,btKodeUrusan=@KodeUrusan,btKodeSKPD= @KodeSKPD ," +
                                    "btKodeUK=@KodeUK,btKodeKategoriPelaksana=@KodeKategoriPelaksana,btKodeUrusanPelaksana=@KodeUrusanPelaksana," +
                                    "btIDProgram=@btIDProgram,btIDKegiatan=@btIDKegiatan,sNama=@Nama," +
                                    "iStatus=@Status,btJenis=@Jenis,sUraian=@Uraian,iNoUrutSPP=@NoUrutSPP,bPPKD=0," +
                                    "inourutBAST=@nourutBAST, btJenisBelanja=@JenisBelanja,btIDSUbKegiatan=@btIDSUbKegiatan," +
                                    "cJumlahDikembalikan= @JumlahDikembalikan,btIDbank=@IDbank,iNoRef=@iNoRef," +
                                    "iStatusPajak=@iStatusPajak,IDUrusan =@IDUrusan,idDinas=@idDinas ,idProgram =@idProgram," +
                                    "IDkegiatan=@IDkegiatan,IDSubkegiatan=@IDSubkegiatan,sNoPungut=@sNoPungut, TahapEM=@TahapEM," +
                                    "RealisasiFisik=@RealisasiFisik ,UnitAnggaran = @UnitAnggaran WHERE iNoUrut=@NoUrut";

                    DBParameterCollection paramCollection = new DBParameterCollection();


                    paramCollection.Add(new DBParameter("@NoUrut", m_noUrut, DbType.Int64));
                    paramCollection.Add(new DBParameter("@NoBukti", pengeluaran.NoBukti, DbType.String));
                    paramCollection.Add(new DBParameter("@dtBukuKas", pengeluaran.Tanggal, DbType.Date));
                    paramCollection.Add(new DBParameter("@Jumlah", pengeluaran.Jumlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@Tahun", pengeluaran.Tahun, DbType.Int32));
                    paramCollection.Add(new DBParameter("@KodeKategori", pengeluaran.Kodekategori, DbType.Int32));
                    paramCollection.Add(new DBParameter("@KodeUrusan", pengeluaran.KodeUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@KodeSKPD", pengeluaran.KodeSKPD, DbType.Int32));
                    paramCollection.Add(new DBParameter("@KodeUK", pengeluaran.KodeUK, DbType.Int32));
                    paramCollection.Add(new DBParameter("@KodeKategoriPelaksana", pengeluaran.KodekategoriPelaksana, DbType.Int32));
                    paramCollection.Add(new DBParameter("@KodeUrusanPelaksana", pengeluaran.KodeurusanPelaksana, DbType.Int32));
                    paramCollection.Add(new DBParameter("@btIDProgram", pengeluaran.KodeProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@btIDKegiatan", pengeluaran.Kodekegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@Nama", pengeluaran.Penerima, DbType.String));
                    paramCollection.Add(new DBParameter("@Status", 0, DbType.Int32));
                    paramCollection.Add(new DBParameter("@Jenis", (int)pengeluaran.Jenis, DbType.Int32));
                    paramCollection.Add(new DBParameter("@Uraian", pengeluaran.Uraian, DbType.String));
                    paramCollection.Add(new DBParameter("@NoUrutSPP", pengeluaran.NoUrutSPP, DbType.Int64));
                    paramCollection.Add(new DBParameter("@PPKD", 0, DbType.Int32));
                    paramCollection.Add(new DBParameter("@nourutBAST", pengeluaran.NoUrutBAST, DbType.Int64));
                    paramCollection.Add(new DBParameter("@JenisBelanja", pengeluaran.JenisBelanja, DbType.Int32));
                    paramCollection.Add(new DBParameter("@btIDSUbKegiatan", pengeluaran.KodeSubKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@JumlahDikembalikan", pengeluaran.JumlahDikembalikan, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@IDbank", pengeluaran.IDBank, DbType.Int32));
                    paramCollection.Add(new DBParameter("@iNoRef", pengeluaran.NoReferensi, DbType.Int64));
                    paramCollection.Add(new DBParameter("@iStatusPajak", pengeluaran.StatusPajak, DbType.Int32));
                    paramCollection.Add(new DBParameter("@IDUrusan", pengeluaran.IDUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@idDinas", pengeluaran.IDDInas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@idProgram", pengeluaran.IDProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@IDkegiatan", pengeluaran.IDKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@IDSubkegiatan", pengeluaran.IDSUbKegiatan, DbType.Int64));

                    paramCollection.Add(new DBParameter("@sNoPungut", pengeluaran.NoPungut, DbType.String));
                    paramCollection.Add(new DBParameter("@TahapEM", pengeluaran.TahapEM, DbType.Int32));
                    paramCollection.Add(new DBParameter("@RealisasiFisik", pengeluaran.RealisasiFisik, DbType.Int32));
                    paramCollection.Add(new DBParameter("@UnitAnggaran", pengeluaran.UnitAnggaran, DbType.Int32));


                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);
                }
                if (pengeluaran.Jenis > E_JENISPENGELUARAN.PENGEMBALIAN_PANJAR)
                    // tidak untuk Panjar 
                {
                    SSQL = "DELETE tPanjarRekening where inourut =@NOURUT";
                    DBParameterCollection paramDelete = new DBParameterCollection();
                    paramDelete.Add(new DBParameter("@NOURUT", m_noUrut));
                    _dbHelper.ExecuteNonQuery(SSQL, paramDelete, m_connection, m_objTrans);
                    foreach (PengeluaranRekening pr in pengeluaran.Details)
                    {
                        if (pr.Jumlah > 0)
                        {
                            SSQL = "INSERT INTO tPanjarRekening (Inourut, IIDRekening, cJumlah) values (@NOURUT, @IDRekening, @Jumlah)";
                            DBParameterCollection paramDetail = new DBParameterCollection();
                            paramDetail.Add(new DBParameter("@NOURUT", m_noUrut,DbType.Int64));
                            paramDetail.Add(new DBParameter("@IDRekening", pr.IDRekening, DbType.Int64));
                            paramDetail.Add(new DBParameter("@Jumlah", pr.Jumlah, DbType.Decimal));
                            if (pr.Jumlah>0)
                            _dbHelper.ExecuteNonQuery(SSQL, paramDetail, m_connection, m_objTrans);
                        }
                    }
                    //if (pengeluaran.Potongans.Count > 0)
                    //{
                        SSQL = "DELETE tPanjarPotongan where inourut =@NOURUT ";
                        DBParameterCollection paramDeletePotongan = new DBParameterCollection();
                        paramDeletePotongan.Add(new DBParameter("@NOURUT", m_noUrut));

                        _dbHelper.ExecuteNonQuery(SSQL, paramDeletePotongan, m_connection, m_objTrans);

                        SSQL = "DELETE tBKU where inourutSumber =@NOURUT and JenisSUmber =@JENISSUMBER";
                        paramDeletePotongan.Add(new DBParameter("@JENISSUMBER", (int)E_JENIS_REFERENSIBKU.REFERENSI_POTONGANSPJPANJAR));
                        _dbHelper.ExecuteNonQuery(SSQL, paramDeletePotongan, m_connection, m_objTrans);



                    
                        foreach (PotonganPanjar pp in pengeluaran.Potongans)
                        {

                            if (pp.Jumlah > 0)
                            {
                                SSQL = "INSERT INTO tPanjarPotongan (Inourut, IIDRekening, cJumlah, iNoSetorPajak,istatuspajak ) values " +
                                     "(@NOURUT, @IDRekening, @Jumlah,@NoSetorPajak,@StatusPajak)";
                                DBParameterCollection paramDetail = new DBParameterCollection();
                                paramDetail.Add(new DBParameter("@NOURUT", m_noUrut, DbType.Int64));
                                paramDetail.Add(new DBParameter("@IDRekening", pp.IIDRekening, DbType.Int64));
                                paramDetail.Add(new DBParameter("@Jumlah", pp.Jumlah, DbType.Decimal));
                                paramDetail.Add(new DBParameter("@NoSetorPajak", pp.NoUrutSetor, DbType.Int64));
                                paramDetail.Add(new DBParameter("@StatusPajak", pp.StatusPajak, DbType.Int32));


                                _dbHelper.ExecuteNonQuery(SSQL, paramDetail, m_connection, m_objTrans);
                            }
                        }

                    //}
                }
                pengeluaran.NoUrut = m_noUrut;
                bool berhasilBKU= true;
                if (pengeluaran.Jenis < E_JENISPENGELUARAN.PENGELUARAN_ADD)
                {


                    if (SimpanBKU(ref pengeluaran, lstBKU, ref MaxNoBKU, m_connection, m_objTrans) == false)
                    //  ada daftar bku nya
                    {
                        berhasilBKU = berhasilBKU && false;

                    }
                    else
                    {
                        if (pengeluaran.Potongans.Count > 0)
                        {
                            if (SimpanBKUPotongan(ref pengeluaran, lstBKU, ref MaxNoBKU, m_connection, m_objTrans) == false)
                            {
                                berhasilBKU = berhasilBKU && false;
                            }
                        }
                    }
                }
                if (berhasilBKU==true){
                    m_objTrans.Commit();
                } else {
                    m_objTrans.Rollback();
                }
                
               
                m_connection.Close();


            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                m_objTrans.Rollback();
                m_connection.Close();

            }
            return pengeluaran.NoUrut;

        }


        public bool CatatBKU(ref Pengeluaran pengeluaran)
        {
            
            try{
                if (pengeluaran== null)
                {
                    _lastError = "Tidak ada data aset";
                    return false;
                }
            BKULogic oBKULogic = new BKULogic(Tahun);
            List<BKU> lstBKU = new List<BKU>();
            BKU MaxNoBKU = new BKU();    
            List<long> lstNoUrut = new List<long>();          
            if (pengeluaran.NoUrut > 0)
            {
                lstNoUrut.Add(pengeluaran.NoUrut);

            }
            lstBKU = oBKULogic.GetBKUByNoUrutSumber(lstNoUrut,2);
            MaxNoBKU = oBKULogic.GetBKUDenganMaxNoBKU(pengeluaran.IDDInas, pengeluaran.KodeUK);
            pengeluaran.Potongans= GetPotongan(pengeluaran.NoUrut);
            pengeluaran.Details=GetDetail(pengeluaran.NoUrut);


            m_connection = _dbHelper.CreateCOnnection();
            m_objTrans = m_connection.BeginTransaction();
            bool berhasilBKU= true;
            if (SimpanBKU(ref pengeluaran, lstBKU,ref MaxNoBKU, m_connection, m_objTrans) == false)
                    //  ada daftar bku nya
                {
                    berhasilBKU= berhasilBKU && false;

                }
                else
                {
                    if (pengeluaran.Potongans.Count > 0)
                    {
                        if (SimpanBKUPotongan(ref pengeluaran, lstBKU, ref MaxNoBKU, m_connection, m_objTrans) == false)
                        {
                            berhasilBKU= berhasilBKU && false;
                        }
                    }
                }
                if (berhasilBKU==true){
                    m_objTrans.Commit();
                } else {
                    m_objTrans.Rollback();
                }
                m_connection.Close();
                return true;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                m_objTrans.Rollback();
                m_connection.Close();
                return false;
            }
            

        
        }
        private bool BersihkanBKU(long NoUrut, IDbConnection connection, IDbTransaction odbTrans)
        {
            try
            {
                SSQL = "DELETE tBKU WHERE inourutSUmber =@noUrut ";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@noUrut", NoUrut));
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }


    }
    
}
