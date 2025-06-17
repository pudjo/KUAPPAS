using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using DTO;
using DTO.Bendahara;
using BP;
using Formatting;

namespace BP.Bendahara
{
    public class KoreksiLogic:BP 
    {
        IDbConnection m_connection;
        IDbTransaction m_objTrans;

        public KoreksiLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tKoreksi";
        }
        



        public bool Simpan(Koreksi koreksi)
        {
            List<BKU> lstBKU = new List<BKU>();
            BKU MaxNoBKU = new BKU();

            BKULogic oBKULogic = new BKULogic(Tahun);
            List<long> lstNoUrut = new List<long>();

            lstNoUrut.Add(koreksi.NoUrut);

            lstBKU = oBKULogic.GetBKUByNoUrutSumber(lstNoUrut,2);
            MaxNoBKU = oBKULogic.GetBKUDenganMaxNoBKU(koreksi.IDDInas, koreksi.KodeUK);

            m_connection = _dbHelper.CreateCOnnection();
            m_objTrans = m_connection.BeginTransaction();

            try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();

                 long mNoUrut = 0;
                if (koreksi.NoUrut == 0 ){
              

                    mNoUrut =ReadNo(E_KOLOM_NOURUT.CON_URUT_KOREKSI , koreksi.IDDInas)+1;
                    SSQL = "INSERT tKoreksi (inourut , IDDinas, btKodeUK,iTahun, dtKoreksi , sUraian , iStatus , sNobukti , btJenisBelanja , inourutsumber, iJenisSumber, bBedaKegiatan,bDikembalikan,idBank, UnitAnggaran) values (" +
                               " @NoUrut,@IDDInas,@KodeUK,@Tahun,@DtKoreksi,@Uraian,0,@NoBukti,@JenisBelanja,@NoUrutSumber,@JenisSumber,@BedaKegiatan,0 ,@idBank,@UnitAnggaran)";
   
                    paramCollection.Add(new DBParameter("@NoUrut",mNoUrut));
                    paramCollection.Add(new DBParameter("@IDDInas",koreksi.IDDInas));
                    paramCollection.Add(new DBParameter("@KodeUK", koreksi.KodeUK));
                    paramCollection.Add(new DBParameter("@Tahun",koreksi.Tahun));
                    paramCollection.Add(new DBParameter("@DtKoreksi", koreksi.DtKoreksi, DbType.Date));
                    paramCollection.Add(new DBParameter("@Uraian",koreksi.Uraian));
                    paramCollection.Add(new DBParameter("@NoBukti",koreksi.NoBukti));
                    paramCollection.Add(new DBParameter("@JenisBelanja",koreksi.JenisBelanja));
                    paramCollection.Add(new DBParameter("@NoUrutSumber",koreksi.NoUrutSumber));
                    paramCollection.Add(new DBParameter("@JenisSumber",koreksi.JenisSumber));
                    paramCollection.Add(new DBParameter("@BedaKegiatan",koreksi.BedaKegiatan));
                    paramCollection.Add(new DBParameter("@idBank", 1));

                    paramCollection.Add(new DBParameter("@UnitAnggaran", koreksi.UnitAnggaran));


                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);
                    
                    koreksi.NoUrut = mNoUrut;
                 } else {
                            
                    mNoUrut= koreksi.NoUrut;

                    SSQL = "UPDATE tKoreksi set IDDInas = @IDDInas, btKodeUK=@KodeUK," + 
                            "dtKoreksi=@DtKoreksi, sUraian=@Uraian, iStatus =@Status, sNobukti=@NoBukti,btJenisBelanja=@JenisBelanja , iJenisSumber =@JenisSumber,inourutsumber=@NoUrutSumber"  +
                            ", bbedakegiatan =@BedaKegiatan, UnitAnggaran =@UnitAnggaran   WHERE inourut =@NoUrut AND iTAHUN =@Tahun";

                    paramCollection.Add(new DBParameter("@IDDInas", koreksi.IDDInas));
                    paramCollection.Add(new DBParameter("@KodeUK", koreksi.KodeUK));
                    paramCollection.Add(new DBParameter("@DtKoreksi",koreksi.DtKoreksi,DbType.Date));
                    paramCollection.Add(new DBParameter("@Uraian", koreksi.Uraian));
                    paramCollection.Add(new DBParameter("@Status", koreksi.Status));
                    paramCollection.Add(new DBParameter("@NoBukti",koreksi.NoBukti));
                    paramCollection.Add(new DBParameter("@JenisBelanja", koreksi.JenisBelanja));
                    paramCollection.Add(new DBParameter("@JenisSumber",koreksi.JenisSumber));
                    paramCollection.Add(new DBParameter("@NoUrutSumber",koreksi.NoUrutSumber));
                    paramCollection.Add(new DBParameter("@BedaKegiatan", koreksi.BedaKegiatan));
                    paramCollection.Add(new DBParameter("@UnitAnggaran", koreksi.UnitAnggaran));
                    paramCollection.Add(new DBParameter("@NoUrut",koreksi.NoUrut));
                    paramCollection.Add(new DBParameter("@Tahun",koreksi.Tahun));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);

                    SSQL= "DELETE tKoreksiDetail WHERE inourut = @NoUrut";
                     DBParameterCollection paramHapus = new DBParameterCollection();
                        paramHapus.Add(new DBParameter("@nourut", mNoUrut));


                        _dbHelper.ExecuteNonQuery(SSQL, paramHapus, m_connection, m_objTrans);

                }

                
                
                foreach(KoreksiDetail koreksidetail in koreksi.Detail){
                    string sidsubkegian= koreksidetail.IDSubKegiatan.ToString ();
                                       
                    int Kodekategoripelaksana =sidsubkegian.ToKodeKategoriPelaksana();
                    int Kodeurusanpelaksana =sidsubkegian.ToKodeUrusanPelaksana();
                    int KodeProgram =sidsubkegian.ToKodeProgram();
                    int Kodekegiatan =sidsubkegian.ToKodeKegiatan();
                    int KodeSubkegiatan =sidsubkegian.ToKodeSubKegiatan();

                    DBParameterCollection paramDetail = new DBParameterCollection();


                   SSQL = "INSERT INTO tKoreksiDetail (inourut , IDurusan, IDProgram, IDKegiatan,IDSUbKegiatan,"+ 
                       "btKodekategoripelaksana1, btkodeurusanPelaksana1,btIDProgram1, btIDkegiatan1, btIdSubKegiatan, btKodeUK1 , iidRekening1 , cJumlah1 ,iDebet1 ) values (" + 
                       "@nourut , @IDurusan, @IDProgram, @IDKegiatan,@IDSUbKegiatan,"+ 
                       "@Kodekategoripelaksana1, @kodeurusanPelaksana1,@KodeIProgram1, @kodekegiatan1, @KodeSubKegiatan, " + 
                       "@KodeUK1 , @idRekening1 , @Jumlah1 ,@Debet1)";
                    
                    paramDetail.Add(new DBParameter("@nourut", mNoUrut));
                    paramDetail.Add(new DBParameter("@IDurusan", koreksidetail.IDurusan));
                    paramDetail.Add(new DBParameter("@IDProgram",koreksidetail.IDProgram)); 
                    paramDetail.Add(new DBParameter("@IDKegiatan",koreksidetail.IDKegiatan));
                    paramDetail.Add(new DBParameter("@IDSUbKegiatan", koreksidetail.IDSubKegiatan));
                    paramDetail.Add(new DBParameter("@Kodekategoripelaksana1", Kodekategoripelaksana));

                    paramDetail.Add(new DBParameter("@kodeurusanPelaksana1",Kodeurusanpelaksana));
                    paramDetail.Add(new DBParameter("@KodeIProgram1", KodeProgram));
                    paramDetail.Add(new DBParameter("@kodekegiatan1", Kodekegiatan));
                    paramDetail.Add(new DBParameter("@KodeSubKegiatan", KodeSubkegiatan));
                    paramDetail.Add(new DBParameter("@KodeUK1",koreksidetail.KodeUK1)); 
                    paramDetail.Add(new DBParameter("@idRekening1" , koreksidetail.IDRekening1));
                    paramDetail.Add(new DBParameter("@Jumlah1",koreksidetail.Jumlah1, DbType.Decimal ));
                    paramDetail.Add(new DBParameter("@Debet1", koreksidetail.Debet1));
                    _dbHelper.ExecuteNonQuery(SSQL, paramDetail, m_connection, m_objTrans);



                }


               bool statusBKU = true; 

               foreach(KoreksiDetail koreksidetail in koreksi.Detail){
                   
                   if ( SimpanBKU(koreksi, koreksidetail, lstBKU,  MaxNoBKU,
                                      m_connection, m_objTrans) == false )
                   {
                       statusBKU = statusBKU && false;
                   }
                   else
                   {
                       MaxNoBKU.NoBKU++;
                       MaxNoBKU.NoBKUSKPD++;
                       MaxNoBKU.NoUrutSaja++;

                   }
                         
               }

               if (statusBKU == true)
               {

                   m_objTrans.Commit();
                   m_connection.Close();
               }
               else
               {
                   m_objTrans.Rollback();
                   m_connection.Close();

               }
               


            return true;    
            }
            catch (Exception ex)
            {
                m_objTrans.Rollback();
                m_connection.Close();

                _lastError = ex.Message;
                _isError = true;

                return false;
            }


        }
        public bool SPJKanKoreksi(List<BelanjaLPJUP> listBelanjaDipiliih, long noSPJ)
        {
            try
            {
                foreach (BelanjaLPJUP b in listBelanjaDipiliih)
                {
                    SSQL = "UPDATE tKoreksi set iStatus= 5, inourutspjup =@NoURutSPJ WHERE inourut =@NoUrut";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@NoURutSPJ", noSPJ));
                    paramCollection.Add(new DBParameter("@NoUrut", b.NoUrut));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                }
                return true;

            }
            catch (Exception ex
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

                SSQL = "UPDATE tKoreksi  set iStatus= 0, inourutspjup =0 WHERE inourut =@NoURutSPJ";

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
        private bool SimpanBKU(Koreksi koreksi, KoreksiDetail kd, List<BKU> lstBKU, BKU MaxNoBKU,
                             IDbConnection connection, IDbTransaction odbTrans)
        {
            try
            { // BKU 
                BKU oBKU = new BKU();

                BKULogic oBKULogic = new BKULogic(Tahun);
                oBKU.CreateFromKoreksi(koreksi, kd, 2);

                BKU oldBKU = lstBKU.FirstOrDefault(x => x.IDDinas == koreksi.IDDInas && 
                                              x.JenisSumber == (int)E_JENIS_REFERENSIBKU.REFERENSI_KOREKSI &&
                                              x.JenisBendahara == E_JENISBENDAHARA.BENDAHARA_PENGELUARAN &&
                                              x.Debet == kd.Debet1);



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
                
                return oBKULogic.Simpan(ref oBKU, connection, odbTrans);
                


            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return false;
            }
        }

        public List<Koreksi> GetByDinas(int pIDDInas ,int ptahun)
        {
            List<Koreksi> _lst = new List<Koreksi>();
            try
            {
                SSQL = "SELECT tKoreksi.*, mSKPD.snamaSKPD FROM tKoreksi INNER Join mSKPD ON mSKPD.iTahun = tKoreksi.iTahun and tKoreksi.IDDInas = mSKPD.ID " +
                    " WHERE tKoreksi.iTahun = @tahun AND IDDInas=@IDDInas";                
                
                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("@tahun", ptahun));
                paramCollection.Add(new DBParameter("@IDDInas", pIDDInas));


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL,paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Koreksi()
                                {
                                    NoUrut =DataFormat.GetLong(dr["iNourut"]),
                                    IDDInas =DataFormat.GetInteger(dr["IDDInas"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    Tahun =DataFormat.GetInteger(dr["iTahun"]),

                                    DtKoreksi = DataFormat.GetDateTime(dr["dtKoreksi"]),
                                    Uraian =DataFormat.GetString(dr["sUraian"]),
                                    Status =DataFormat.GetInteger(dr["iStatus"]),
                                    NoBukti = DataFormat.GetString(dr["sNobukti"]),
                                    JenisBelanja =DataFormat.GetInteger(dr["btJenisBelanja"]),
                                   
                                    NoUrutSumber = DataFormat.GetLong(dr["inoUrutSumber"]),
                                    JenisSumber = DataFormat.GetInteger(dr["iJenisSUmber"]),
                                    NourutSPJUP =DataFormat.GetInteger(dr["inourutspjup"]),
                                    BedaKegiatan =DataFormat.GetInteger(dr["bBedakegiatan"]),
                                    Detail = GetDetail(DataFormat.GetLong(dr["iNourut"])),
                                  
                                    
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
        private List<KoreksiDetail> GetDetail(long pnourut)
        {

            List<KoreksiDetail> _lst = new List<KoreksiDetail>();
            try
            {
                SSQL = "SELECT tKoreksiDetail.*, mRekening.sNamaRekening FROM tKoreksiDetail INNER Join mRekening ON tKoreksiDetail.IIDRekening1 = mRekening.IIDRekening " +
                        " WHERE inourut = @inourut ORDER BY IIDRekening1";

                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("@inourut", pnourut));

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new KoreksiDetail()
                                {
                                        NoUrut =DataFormat.GetLong(dr["iNourut"]),
                                        IDurusan=DataFormat.GetInteger(dr["IDUrusan"]),
                                        IDProgram=DataFormat.GetInteger(dr["IDProgram"]),
                                        IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                        IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),

                                        KodeUK1 =DataFormat.GetInteger (dr["btKodeUK1"]),
                                        IDRekening1=DataFormat.GetLong(dr["IIDRekening1"]),
                                        Jumlah1=DataFormat.GetDecimal(dr["cJumlah1"]),
                                        Debet1 =DataFormat.GetInteger(dr["iDebet1"]),
                                        NamaRekening=DataFormat.GetString(dr["sNamaRekening"])


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


        

        public Koreksi GetByID(long iNoUrut)
        {
            Koreksi oKoreksi = new Koreksi();
            try
            {

                SSQL = "SELECT * FROM mKoreksi where inourut = @NoUrut";
   

                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("@NoUrut", iNoUrut));

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        //nk (btKodeKoreksi,sNamaKoreksi,sNoRekening, btKodekategori, btKodeUrusan, btKodeSKPD, btKodeUK,btJenisBendahara,IIDRekening) 
                        oKoreksi = new Koreksi()
                        {
                            NoUrut = DataFormat.GetLong(dr["iNourut"]),
                            IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                            Tahun = DataFormat.GetInteger(dr["iTahun"]),

                            DtKoreksi = DataFormat.GetDateTime(dr["dtKoreksi"]),
                            Uraian = DataFormat.GetString(dr["sUraian"]),
                            Status = DataFormat.GetInteger(dr["btKodeKoreksi"]),
                            NoBukti = DataFormat.GetString(dr["iStatus"]),
                            JenisBelanja = DataFormat.GetInteger(dr["btJenisBelanja"]),
                            NoUrutSumber = DataFormat.GetLong(dr["inoUrutSumber"]),
                            JenisSumber = DataFormat.GetInteger(dr["btJenisBelanja"]),
                            NourutSPJUP = DataFormat.GetInteger(dr["btKodeKoreksi"]),
                            BedaKegiatan = DataFormat.GetInteger(dr["btKodeKoreksi"]),
                            Detail = GetDetail(DataFormat.GetLong(dr["iNourut"])),

                        };
                    }
                }
                return oKoreksi;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return oKoreksi;
            }

        }
        public bool Hapus(long inourut)
        {

            try{
                m_connection = _dbHelper.CreateCOnnection();
                m_objTrans = m_connection.BeginTransaction();

                DBParameterCollection paramCollection = new DBParameterCollection();

               
                SSQL = "DELETE from tKoreksi where inourut =@nourut";
                paramCollection.Add(new DBParameter("@nourut", inourut));
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);
                SSQL = "DELETE from tKoreksiDetail where inourut =@nourut";
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);

                SSQL = "DELETE from tJurnal where iSUmber=@nourut";
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);

                SSQL = "DELETE from tBukubesar where iSUmber=@nourut";
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);
                SSQL = "DELETE from tBKURekening  from tBKURekening inner join tBKU on tBKU.inourut= tBKURekening.inourut  where tbku.iNoUrutSUmber=@nourut";
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);
                SSQL = "DELETE from tBKU  where tbku.iNoUrutSUmber=@nourut";
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);
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
    }
}
