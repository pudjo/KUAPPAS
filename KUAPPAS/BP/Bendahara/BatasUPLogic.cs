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
    public class BatasUPLogic:BP 
    {
     
        public BatasUPLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mBatasUP";
        }
        



        public bool Simpan(BatasUP bUP)
        {
            try
            {

                SSQL = "DELETE  mBatasUP WHERE iTahun=@Tahun  and IDDInas=@DInas and btKodeUK=@KodeUK";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@Tahun", bUP.Tahun));
                paramCollection.Add(new DBParameter("@DInas", bUP.IDDinas));
                paramCollection.Add(new DBParameter("@KodeUK", bUP.KodeUK));

               _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
               
              SSQL = "INSERT INTO mBatasUP (iTahun ,IDDInas,btKodeUK ,cJumlah) values (" +
                     "@Tahun ,@DInas,@KodeUK ,@Jumlah)";

              
            
               paramCollection.Add(new DBParameter("@Jumlah", bUP.Jumlah,DbType.Decimal));
               _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                   return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;

                return false;
            }


        }

        public List<BatasUP> Get(int _tahun)
        {
            List<BatasUP> _lst = new List<BatasUP>();
            try
            {
                SSQL = "SELECT mSKPD.ID as IDDInaas, mSKPD.snamaSKPD, mBatasUP.cJumlah " +
                " FROM mSKPD Left Join  mBatasUP ON mBatasUP.IDDInas = mSKPD.ID and mBatasUP.itahun = mskpd.itahun  WHERE mSKPD.iTahun = @TAHUN Order by mSKPD.ID";                
                
               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@TAHUN", _tahun));
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new BatasUP()
                                {

                                    IDDinas = DataFormat.GetInteger(dr["IDDInaas"]),
                                    NamaSKPD = DataFormat.GetString(dr["sNamaSKPD"]),
                                    Jumlah= DataFormat.GetDecimal(dr["cJumlah"]),
                                    
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

        public BatasUP GetByDinas(int IDDInas, int _iTahun)
        {
            BatasUP oBatasUP = new BatasUP();
            try
            {

                SSQL = "SELECT * FROM mBatasUP where IDDinas = @DINAS AND  iTahun=@TAHUN";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@DINAS", IDDInas));
                paramCollection.Add(new DBParameter("@TAHUN", _iTahun));
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        //nk (btKodeBatasUP,sNamaBatasUP,sNoRekening, btKodekategori, btKodeUrusan, btKodeSKPD, btKodeUK,btJenisBendahara,IIDRekening) 
                        oBatasUP = new BatasUP()
                        {
                            IDDinas = DataFormat.GetInteger(dr["IDDinas"]),
                            //NamaSKPD = DataFormat.GetString(dr["sNamaSKPD"]),
                            Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),

                        };
                    }
                }
                return oBatasUP;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return oBatasUP;
            }

        }
        public bool Hapus(int IDDInas, int _iTahun)
        {

            try{

               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@DINAS",IDDInas ));
               paramCollection.Add(new DBParameter("@TAHUN",_iTahun ));

               SSQL = "DELETE from mBatasUP where IDDinas = @DINAS AND  iTahun=@TAHUN";

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
