using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO.Bendahara;
using System.Data;
using System.Data.SqlClient;
using Formatting;
using DataAccess;

namespace BP.Bendahara
{
    public class PotonganPanjarLogic : BP
    {
        
        public PotonganPanjarLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tPanjarPotongan";
        }
        

        public bool Simpan(long inourut,List<PotonganPanjar> lst)
        {
            try
            {

                SSQL = "UPDATE " + m_sNamaTabel + " SET cJumlah=0 WHERE = " + inourut.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);


                

                foreach (PotonganPanjar dh in lst)
                {
                    PotonganPanjar oCek = new PotonganPanjar();
                    oCek = Cek(dh);

                    if (oCek == null  && dh.Jumlah>0)
                    {
                        SSQL = "INSERT INTO " + m_sNamaTabel + " (inourut,IIDRekening, cJumlah,iNoSetorPajak, iStatusPajak) values ( " +
                                 " @pinourut,@pIIDRekening, @pcJumlah,0,0) ";//, @piNo, @pOnPerda)";

                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pinourut", dh.NoUrut, DbType.Int64));
                        paramCollection.Add(new DBParameter("@pIIDRekening", dh.IIDRekening, DbType.Int64));
                        paramCollection.Add(new DBParameter("@pcJumlah", dh.Jumlah, DbType.Decimal));



                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    }
                    else
                    {

                        SSQL = "UPDATE " + m_sNamaTabel + " SET  cJumlah= @pcJumlah WHERE inourut =@pinourut AND IIDrEKENING=@pIIDRekening ";//,iNoSetorPajak, iStatusPajak) values ( " +


                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pcJumlah", dh.Jumlah, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pinourut", dh.NoUrut, DbType.Int64));
                        paramCollection.Add(new DBParameter("@pIIDRekening", dh.IIDRekening, DbType.Int64));
                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                        if (oCek.Jumlah != dh.Jumlah && oCek.StatusPajak==1)
                        {
                            //TODO 
                            
                            _lastError = "Potongan sudah di setor... Harus dibetulkan setorannya..  ";

                            //SSQL = "UPDATE " + m_sNamaTabel + " SET  iStatusPajak= 0, WHERE inourut =@pinourut AND IIDrEKENING=@pIIDRekening ";//,iNoSetorPajak, iStatusPajak) values ( " +
                            ///DBParameterCollection paramCollectionS = new DBParameterCollection();

                            //paramCollectionS.Add(new DBParameter("@pinourut", dh.NoUrut, DbType.Int64));
                            //paramCollectionS.Add(new DBParameter("@pIIDRekening", dh.IIDRekening, DbType.Int64));
                            //_dbHelper.ExecuteNonQuery(SSQL, paramCollectionS);

                            //// UBAH TSETOR rEKENING
                            // UBAH TSETOR
                            //UBAH BKU //


                            SSQL = "UPDATE tSetorRekening set cJumlah = @pCJumlah WHERE inourut=@pinourut and IIDRekening=@pIIDRekening ";
                            DBParameterCollection paramCollectiont = new DBParameterCollection();
                            paramCollectiont.Add(new DBParameter("@pCJumlah", dh.Jumlah, DbType.Decimal));
                            paramCollectiont.Add(new DBParameter("@pinourut", dh.NoUrutSetor, DbType.Int64));
                            paramCollectiont.Add(new DBParameter("@pIIDRekening", dh.IIDRekening, DbType.Int64));
                            //paramCollectiont.Add(new DBParameter("@pIIDRekening", dh.IIDRekening, DbType.Int64));
                            _dbHelper.ExecuteNonQuery(SSQL, paramCollectiont);


                            SSQL = "UPDATE tSetor  set cJumlah = (Select sum(cJumlah) from tSetorRekening where inourut=@pinourut) where inourut =@pinourut";
                            DBParameterCollection paramCollectionX = new DBParameterCollection();
                            paramCollectionX.Add(new DBParameter("@pinourut", dh.NoUrutSetor, DbType.Int64));

                            _dbHelper.ExecuteNonQuery(SSQL, paramCollectionX);


                            //SSQL = "UPDATE tBKUREKENING set cJumlah = @pCJumlah WHERE inourut=@pinourut and IIDRekening=@pIIDRekening ";
                            //DBParameterCollection paramCollectiont = new DBParameterCollection();
                            //paramCollectiont.Add(new DBParameter("@pCJumlah", dh.Jumlah, DbType.Decimal));
                            //paramCollectiont.Add(new DBParameter("@pinourut", dh.NoUrutSetor, DbType.Int64));
                            //paramCollectiont.Add(new DBParameter("@pIIDRekening", dh.IIDRekening, DbType.Int64));
                            ////paramCollectiont.Add(new DBParameter("@pIIDRekening", dh.IIDRekening, DbType.Int64));
                            //_dbHelper.ExecuteNonQuery(SSQL, paramCollectiont);


                            //SSQL = "UPDATE tSetor  set cJumlah = (Select sum(cJumlah) from tSetorRekening where inourut=@pinourut) where inourut =@pinourut";
                            //DBParameterCollection paramCollectionX = new DBParameterCollection();
                            //paramCollectionX.Add(new DBParameter("@pinourut", dh.NoUrutSetor, DbType.Int64));

                            //_dbHelper.ExecuteNonQuery(SSQL, paramCollectionX);



                            



                        }
   

                    }
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
        public List<PotonganPanjar> Get(long NoUrut)
        {
            List<PotonganPanjar> _lst = new List<PotonganPanjar>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE inourut =@NOURUT  Order by IIDRekening ";//
                
               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@NOURUT", NoUrut));

                
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PotonganPanjar()
                                {
                                    IIDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    NoUrut = DataFormat.GetLong(dr["Inourut"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    NoUrutSetor = DataFormat.GetLong(dr["iNoSetorPajak"]),
                                    StatusPajak = DataFormat.GetInteger(dr["iStatusPajak"])
                         
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
        public PotonganPanjar Cek(PotonganPanjar p)
        {
            PotonganPanjar oPotongan = new PotonganPanjar();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE Inourut =" + p.NoUrut.ToString() + " AND IIDRekening = " + p.IIDRekening.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        oPotongan = new PotonganPanjar()
                        {
                            IIDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    NoUrut = DataFormat.GetLong(dr["Inourut"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    NoUrutSetor = DataFormat.GetLong(dr["iNoSetorPajak"]),
                                    StatusPajak = DataFormat.GetInteger(dr["iStatusPajak"])

                        };
                        return oPotongan;
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
        public bool Hapus(long inourut)
        {

            try
            {
                SSQL = "DELETE  FROM " + m_sNamaTabel + " WHERE WHERE inourut = " + inourut.ToString();
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
        public List<PembayaranPajak> GetPajakUntukPembayaran(int iddinas, int KodeUK)
        {
            try
            {
                List<PembayaranPajak> lstPajak = new List<PembayaranPajak>();
                SSQL = "select tPanjar.iTahun, tPanjar.iStatus,tPanjar.IDDInas,tPanjar.btKodeUK," +
                      "  isnull(tPanjarPotongan.iNoSetorPajak,0) as iNoSetorPajak,iNoRef, 1 as Jenis,tPanjar.dtBukukas, tPanjar.sUraian, " +
                      "   isnull(tPanjarPotongan.iStatusPajak,0) as iStatusPajak, " +
                      "   tPanjar.sNoBukti, tPanjarPotongan.inourut,  " +
                       "  tPanjarPotongan.cJumlah,tPanjarPotongan.sNoNTPN, " +
                        " tPanjarPotongan.KodeBilling,tPanjarPotongan.IIDRekening,  " +
                        " mPotongan.sNamaPotongan,mPotongan.bInformasi ,"+
                        "TPanjar.IDUrusan, tPanjar.IDProgram, tPanjar.IDKegiatan, tPanjar.IDSubkegiatan " +
                        " from tPanjarPotongan inner join mPotongan ON mPotongan.IIDrekeningPotongan = tPanjarPotongan.IIDrekening   " +
                        " INNER JOIN tPanjar ON tPanjarPotongan.inourut= tPanjar.inourut " +
                        " WHERE IDDInas =@IDDINAS ";

               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@IDDINAS", iddinas));
              // paramCollection.Add(new DBParameter("@KODEUK", KodeUK));

               DataTable dt = new DataTable();
               dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
               if (dt != null)
               {
                   if (dt.Rows.Count > 0)
                   {
                       lstPajak = (from DataRow dr in dt.Rows
                                   select new PembayaranPajak()
                               {
                                
                                   
                                   IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    NoUrutBelanja = DataFormat.GetLong(dr["Inourut"]),
                                    NoUrutSetorPajak = DataFormat.GetLong(dr["iNoSetorPajak"]),
                                    NoBuktiBelanja = DataFormat.GetString(dr["sNoBukti"]),
                                    KeteranganBelanja = DataFormat.GetString(dr["sUraian"]),
                                    TanggalBelanja = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    IDPotongan = DataFormat.GetInteger(dr["IIDRekening"]),
                                    NamaPotongan = DataFormat.GetString(dr["sNamaPotongan"]),     
                                    JenisBelanja = DataFormat.GetInteger(dr["iStatusPajak"]),    
                                    Jumlah= DataFormat.GetDecimal(dr["cJumlah"]),
                                    StatusPajak = DataFormat.GetInteger(dr["iStatusPajak"]),
                                  //  NoBukti= DataFormat.GetString(dr["iStatusPajak"]),
                                    NTPN = DataFormat.GetString(dr["iStatusPajak"]),
                                    KodeBilling= DataFormat.GetString(dr["iStatusPajak"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IdKegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IdSubKegiatan"]),
        
                               }).ToList();
                   }
               }
               return lstPajak;

                
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;

                return null;
            }
        }
    }
}
