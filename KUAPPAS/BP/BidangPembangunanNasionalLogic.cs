using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using DTO;
using Formatting;


namespace BP
{
    public class BidangPembangunanNasionalLogic: BP 
    {
        public BidangPembangunanNasionalLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "BidangPembangunanNasional";
        }
        public List<BidangPembangunanNasional> Get()
        {
            List<BidangPembangunanNasional> _lst = new List<BidangPembangunanNasional>();
            try
            {
                SSQL = "SELECT * FROM BidangPembangunanNasional ";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new BidangPembangunanNasional()
                                {
                                   Tahun= DataFormat.GetInteger(dr["Tahun"]),
                                    ID= DataFormat.GetInteger(dr["ID"]),
                                    Nama= DataFormat.GetString(dr["Nama"])
 
                                }).ToList();
                    }
                }
                return _lst;
            } catch(Exception ex){
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        
        public BidangPembangunanNasional GetByID(int pID)
        {
            BidangPembangunanNasional _object = new BidangPembangunanNasional();
            try
            {
                
                  SSQL = "SELECT * FROM BidangPembangunanNasional Where ID =" + pID.ToString();
                

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    DataRow dr = null;
                    
                    
                    if (dt.Rows.Count > 0)
                    {
                        dr= dt.Rows[0];

                        _object = new BidangPembangunanNasional()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Tahun= DataFormat.GetInteger(dr["Tahun"]),
                                    Nama = DataFormat.GetString(dr["Nama"])
                                };
                    }
                }
                return _object;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _object;
            }


        }
       
        public bool Simpan(ref BidangPembangunanNasional _pBidangPembangunanNasional)
        {
            try
            {
                int _newID;
                if (_pBidangPembangunanNasional.ID== 0)
                {
                    
                    _newID = GetMaxID()+1 ;
                    
                    SSQL = "INSERT INTO BidangPembangunanNasional(ID, Tahun, Nama) values (" +
                        "@pID, @pTahun, @pNama)";

                }
                else
                {
                    _newID= _pBidangPembangunanNasional.ID;
                    SSQL = "UPDATE BidangPembangunanNasional SET Nama= @pNama WHERE ID=@pID";

                }

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _newID));
                paramCollection.Add(new DBParameter("@pTahun", _pBidangPembangunanNasional.Tahun));
                paramCollection.Add(new DBParameter("@pNama", _pBidangPembangunanNasional.Nama));

                if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
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
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        
        public bool Hapus(int _pIDBidangPembangunanNasional)
        {
            try
            {
                
                SSQL = "DELETE FROM BidangPembangunanNasional WHERE ID=@pID";                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDBidangPembangunanNasional));
                if (_dbHelper.ExecuteNonQuery(SSQL) > 0)
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
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool Hapus()
        {
            try
            {

                SSQL = "DELETE FROM mBidangPembangunanNasional ";
                _dbHelper.ExecuteNonQuery(SSQL);
        
                    return true;
                
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
    }
}
