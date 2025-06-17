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
    public class TrxBankLogic:BP
    {

        IDbConnection m_connection;
        IDbTransaction m_objTrans;

        public TrxBankLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tTrxBank";
           
        }
              
        public List<TrxBank> GetByDinas(int iddinas, int tahun, int iPPKD)
        {
            List<TrxBank> _lst = new List<TrxBank>();
            try{
            
            SSQL = " SELECT * from tTrxBank AS A WHERE 1>0 AND A.IDDInas =@Dinas " ;
            SSQL = SSQL + " AND iTahun =@Tahun  ORDER BY A.dTrx, A.inOurut";

              
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@Dinas",iddinas)) ;
                paramCollection.Add(new DBParameter("@Tahun",tahun));
            


            
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                  if (dt != null)
                  {
                      if (dt.Rows.Count > 0)
                      {
                          _lst = (from DataRow dr in dt.Rows
                                  select new TrxBank()
                                  {

                                      ID = DataFormat.GetLong(dr["inourut"]),
                                      Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                      IDDinas  = DataFormat.GetInteger(dr["IDDInas"]),
                                        Bank = DataFormat.GetInteger(dr["iBank"]),
                                        jenis = DataFormat.GetInteger(dr["iJenis"]),
                                        Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                        BankTujuan = DataFormat.GetInteger(dr["bankTujuan"]),
                                        cCreate = DataFormat.GetString(dr["cCreate"]),
                                        cUpdate = DataFormat.GetString(dr["cUpdate"]),
                                        Uraian  = DataFormat.GetString(dr["sUraian"]),
                                        DTrx = DataFormat.GetDateTime(dr["dTrx"]),
                                        NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                        JenisBelanja = DataFormat.GetInteger(dr["JenisBelanja"]),
                                        PPKD = DataFormat.GetInteger(dr["PPKD"]),

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
        public TrxBank GetByID(long pID)
        {
            TrxBank oTRXBank = new TrxBank();
            try
            {

                SSQL = " SELECT * from tTrxBank AS A WHERE iNourut = @ID";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@ID", pID));
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                                oTRXBank= new TrxBank()
                                {

                                    ID = DataFormat.GetLong(dr["inourut"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Bank = DataFormat.GetInteger(dr["iBank"]),
                                    jenis = DataFormat.GetInteger(dr["iJenis"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    BankTujuan = DataFormat.GetInteger(dr["bankTujuan"]),
                                    cCreate = DataFormat.GetString(dr["cCreate"]),
                                    cUpdate = DataFormat.GetString(dr["cUpdate"]),
                                    Uraian = DataFormat.GetString(dr["sUraian"]),
                                    DTrx = DataFormat.GetDateTime(dr["dTrx"]),
                                    NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                    JenisBelanja = DataFormat.GetInteger(dr["JenisBelanja"]),
                                    PPKD = DataFormat.GetInteger(dr["PPKD"]),

                                };

                    }
                }
                return oTRXBank;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return oTRXBank;
            }
            
        }
        
        public long Simpan(TrxBank tb)
        {
            try
            {

                List<BKU> lstBKU = new List<BKU>();
                List<BKU> lstMaxNoBKU = new List<BKU>();

                BKULogic oBKULogic = new BKULogic(Tahun);
                List<long> lstNoUrut = new List<long>();

                if (tb.ID > 0)
                {
                    lstNoUrut.Add(tb.ID);

                }

                lstBKU = oBKULogic.GetBKUByNoUrutSumber(lstNoUrut,2);

                lstMaxNoBKU = oBKULogic.GetMaxNoBKU();



                m_connection = _dbHelper.CreateCOnnection();
                m_objTrans = m_connection.BeginTransaction();

                SSQL = "";
                long m_noUrut;

                if (tb.ID==0 ){
                    long NoUrut = 0;

                    //m_noUrut = GetNoUrut(c);
                    NoUrut = DataFormat.GetLong(
                                   GetNoUrut(E_KOLOM_NOURUT.CON_URUT_KAS, Tahun, tb.IDDinas));

                    SSQL = "INSERT INTO tTrxBank (iNoUrut,iTahun,IDDinas,  btKodeKategori,btKodeUrusan,btKodeSKPD, btKodeUK,iBank,dTrx,iJenis,sNoBukti,sUraian,cJumlah) values (" +
                         "@NoUrut,@Tahun,@DDinas,  @KodeKategori,@KodeUrusan,@KodeSKPD, @KodeUK,@Bank,@Tanggal,@Jenis,@NoBukti,@Uraian,@Jumlah)";
                    tb.ID = NoUrut;
                           DBParameterCollection paramCollection = new DBParameterCollection();
                           paramCollection.Add(new DBParameter("@NoUrut",tb.ID));
                           paramCollection.Add(new DBParameter("@Tahun",tb.Tahun));
                           paramCollection.Add(new DBParameter("@DDinas",tb.IDDinas));  
                           paramCollection.Add(new DBParameter("@KodeKategori",tb.IDDinas.ToString().ToKodeKategori()));
                           paramCollection.Add(new DBParameter("@KodeUrusan",tb.IDDinas.ToString().ToKodeUrusan()));
                           paramCollection.Add(new DBParameter("@KodeSKPD",tb.IDDinas.ToString().ToKodeSKPD())); 
                           paramCollection.Add(new DBParameter("@KodeUK",tb.KodeUK));
                           paramCollection.Add(new DBParameter("@Bank",1));
                           paramCollection.Add(new DBParameter("@Tanggal",tb.DTrx,DbType.Date));
                           paramCollection.Add(new DBParameter("@Jenis",tb.jenis));
                           paramCollection.Add(new DBParameter("@NoBukti",tb.NoBukti));
                           paramCollection.Add(new DBParameter("@Uraian",tb.Uraian));
                           paramCollection.Add(new DBParameter("@Jumlah", tb.Jumlah,DbType.Decimal));
                           _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);
                 
                } else {
                    SSQL = "UPDATE tTrxBank SET iTahun=@Tahun,IDDinas=@DDinas,btKodeKategori=@KodeKategori,btKodeUrusan=@KodeUrusan" +
                           ",btKodeSKPD=@KodeSKPD, btKodeUK=@KodeUK,iBank=@Bank,dTrx=@Tanggal,iJenis=@Jenis,sNoBukti=@NoBukti,sUraian=@Uraian,cJumlah=@Jumlah " +
                           " where inourut =@NoUrut";
                    
                    DBParameterCollection paramCollection = new DBParameterCollection();
           
                    paramCollection.Add(new DBParameter("@Tahun", tb.Tahun));
                    paramCollection.Add(new DBParameter("@DDinas", tb.IDDinas));
                    paramCollection.Add(new DBParameter("@KodeKategori", tb.IDDinas.ToString().ToKodeKategori()));
                    paramCollection.Add(new DBParameter("@KodeUrusan", tb.IDDinas.ToString().ToKodeUrusan()));
                    paramCollection.Add(new DBParameter("@KodeSKPD", tb.IDDinas.ToString().ToKodeSKPD()));
                    paramCollection.Add(new DBParameter("@KodeUK", tb.KodeUK));
                    paramCollection.Add(new DBParameter("@Bank", 1));
                    paramCollection.Add(new DBParameter("@Tanggal", tb.DTrx,DbType.Date));
                    paramCollection.Add(new DBParameter("@Jenis", tb.jenis));
                    paramCollection.Add(new DBParameter("@NoBukti", tb.NoBukti));
                    paramCollection.Add(new DBParameter("@Uraian", tb.Uraian));
                    paramCollection.Add(new DBParameter("@Jumlah", tb.Jumlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@NoUrut", tb.ID));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);

                }



              
                        
               m_objTrans.Commit();
               m_connection.Close();
                return tb.ID;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return 0;
            }
        }

        public bool Haous (long noUrut )
        {
            try
            {

                SSQL = "";
                
                SSQL = "DELETE tTrxBank  where inourut =@NoUrut";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@NoUrut", noUrut));
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                SSQL = "DELETE tBKU WHERE inourutSUmber =@noUrut ";
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                
                return true ;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false ;
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


        public bool CatatBKU(ref TrxBank p)
        {

            try
            {
                BKULogic oBKULogic = new BKULogic(Tahun);
                List<BKU> lstBKU = new List<BKU>();
                BKU MaxNoBKU = new BKU();
                List<long> lstNoUrut = new List<long>();
                if (p.ID > 0)
                {
                    lstNoUrut.Add(p.ID);

                }
                lstBKU = oBKULogic.GetBKUByNoUrutSumber(lstNoUrut, 2);
                MaxNoBKU = oBKULogic.GetBKUDenganMaxNoBKU(p.IDDinas, p.KodeUK);
                m_connection = _dbHelper.CreateCOnnection();
                m_objTrans = m_connection.BeginTransaction();

                bool berhasilBKU = true;
                if (SimpanBKU(ref p, lstBKU, ref MaxNoBKU, m_connection, m_objTrans,1) == false)
                //  ada daftar bku nya
                {
                    berhasilBKU = berhasilBKU && false;

                }
                else
                {
                      if (SimpanBKU(ref p, lstBKU, ref MaxNoBKU, m_connection, m_objTrans,0) == false)
                      {
                          berhasilBKU = berhasilBKU && false;

                      }
                }
              
                if (berhasilBKU == true)
                {
                    m_objTrans.Commit();
                }
                else
                {
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
        public bool SimpanBKU(ref TrxBank p, List<BKU> lstBKU,
                                ref BKU MaxNoBKU,
                                IDbConnection connection, IDbTransaction odbTrans,int bank 
                              )
        {
            try
            {

                BKULogic oLogic = new BKULogic(Tahun);
                BKU oBKU = new BKU();
                BKU oldBKU = new BKU();
                bool berhasilBKU = true;
                int debet = bank == 0 ? 1 : -1;

                  oBKU.CreateFormTrxBank(p, 2);
                    oldBKU = GetOldBKU(p,
                                        lstBKU,
                                 E_JENISBENDAHARA.BENDAHARA_PENGELUARAN,
                                 oBKU.JenisSumber, debet);
                   
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
                    oBKU.Debet =bank == 0 ? 1 : -1;
                    oBKU.Kodebank = bank;

                    if (oLogic.Simpan(ref oBKU, connection, odbTrans) == false)
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
     private BKU GetOldBKU(TrxBank p, 
                            List<BKU> lstBKU, 
                            E_JENISBENDAHARA JenisBendahara, 
                            int JenisSumber, int debet)
        {
            

            BKU oldBKU = lstBKU.FirstOrDefault(b => b.NourutSumber ==  p.ID  && 
                                                        b.IDDinas== p.IDDinas && 
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

    }
}
