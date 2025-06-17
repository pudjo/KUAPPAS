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
    public class BankLogic:BP
    {
        public BankLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mBank";
        }
        



        public bool Simpan(TBank bk)
        {
            try
            {

                DBParameterCollection paramCollection = new DBParameterCollection();
                if (bk.ID==0)
                {
                    int newID = GetMaxID("btKodebank")+1;


                    SSQL = "INSERT INTO mBank (btKodebank,sNamabank,sNoRekening, btKodekategori, btKodeUrusan, btKodeSKPD, btKodeUK,btJenisBendahara,IIDRekening) values ( " +
                           "@ID, @Nama,@NoRekening,@KodeKategori,@KodeUrusan, @KodeSKPD,@KodeUK,@JenisBendahara,@IDRekening)";

                   
        
               
                   paramCollection.Add(new DBParameter("@ID", newID));
                    paramCollection.Add(new DBParameter("@Nama",bk.Nama ));
                    paramCollection.Add(new DBParameter("@NoRekening",bk.NoRekening ));
                    paramCollection.Add(new DBParameter("@KodeKategori",bk.KodeKategori ));
                    paramCollection.Add(new DBParameter("@KodeUrusan",bk.KodeUrusan  ));
                    paramCollection.Add(new DBParameter("@KodeSKPD",bk.KodeSKPD  ));
                    paramCollection.Add(new DBParameter("@KodeUK",bk.KodeUK  ));
                    paramCollection.Add(new DBParameter("@JenisBendahara",bk.JenisBendahara));
                    paramCollection.Add(new DBParameter("@IDRekening",bk.IDRekening));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                   
                }
                else
                {
                    SSQL = "UPDATE mBank SET sNamaBank=@Nama, IIDRekening =@IDrekening , sNoRekening=@NoRekening where btKodebank=@kodebank";

                    paramCollection.Add(new DBParameter("@Nama",bk.Nama )); 
                    paramCollection.Add(new DBParameter("@IDrekening",bk.IDRekening));
                    paramCollection.Add(new DBParameter("@NoRekening",bk.NoRekening));
                    paramCollection.Add(new DBParameter("@kodebank", bk.ID));



                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

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

        public List<TBank> Get()
        {
            List<TBank> _lst = new List<TBank>();
            try
            {
                SSQL = "SELECT mbank.*, mSKPD.snamaSKPD FROM mBank Left Join mSKPD ON mBank.IDDInas = mSKPD.ID  Order by IDDInas";                
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TBank()
                                {
                                    ID = DataFormat.GetInteger(dr["btKodeBank"]),
                                    IDDinas = DataFormat.GetInteger(dr["btKodeBank"]),
                                    NamaSKPD = DataFormat.GetString(dr["sNamaSKPD"]),
                                    Nama = DataFormat.GetString(dr["sNamabank"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),

                                    NoRekening = DataFormat.GetString(dr["sNoRekening"]),
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    JenisBendahara = DataFormat.GetInteger(dr["btJenisBendahara"]),
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
        public TBank GetBankKasda()
        {
            TBank oBank = new TBank();
            try
            {

                SSQL = "SELECT * FROM mBank where IDDinas = 0 AND  AND btJenisBendahara =0";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        //nk (btKodebank,sNamabank,sNoRekening, btKodekategori, btKodeUrusan, btKodeSKPD, btKodeUK,btJenisBendahara,IIDRekening) 
                        oBank = new TBank()
                        {
                            ID = DataFormat.GetInteger(dr["btKodeBank"]),
                            IDDinas = DataFormat.GetInteger(dr["btKodeBank"]),

                            Nama = DataFormat.GetString(dr["sNamabank"]),
                            KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                            KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                            KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                            KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),

                            NoRekening = DataFormat.GetString(dr["sNoRekening"]),
                            IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                            JenisBendahara = DataFormat.GetInteger(dr["btJenisBendahara"]),

                        };
                    }
                }
                return oBank;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return oBank;
            }

        }
        public TBank GetByDinas(int IDDInas, int iJenisBendahara)
        {
            TBank oBank = new TBank();
            try
            {
                
                SSQL = "SELECT * FROM mBank where IDDinas = @IDDInas  AND  AND btJenisBendahara =@jenisbendahara";

               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@IDDInas", IDDInas));
               paramCollection.Add(new DBParameter("@jenisbendahara", iJenisBendahara));



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        //nk (btKodebank,sNamabank,sNoRekening, btKodekategori, btKodeUrusan, btKodeSKPD, btKodeUK,btJenisBendahara,IIDRekening) 
                        oBank = new TBank()
                        {
                            ID = DataFormat.GetInteger(dr["btKodeBank"]),
                            IDDinas = DataFormat.GetInteger(dr["btKodeBank"]),

                            Nama = DataFormat.GetString(dr["sNamabank"]),
                            KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                            KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                            KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                            KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),

                            NoRekening = DataFormat.GetString(dr["sNoRekening"]),
                            IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                            JenisBendahara = DataFormat.GetInteger(dr["btJenisBendahara"]),

                        };
                    }
                }
                return oBank;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return oBank;
            }

        }
        public bool Hapus(int pkodebank )
        {

            try{


                SSQL = "DELETE from mBank  where btKodebank= @kodebank";

                
       

               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@kodebank", pkodebank));


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
    }
}
