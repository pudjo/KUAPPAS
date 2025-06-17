using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Formatting;
using System.Data;
using DataAccess;
using DTO;
namespace BP
{
    class PenggunaGroupLogic: BP 
    {
        
         public PenggunaGroupLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "PenggunaGroup";
        }
        
        public List<PenggunaGroup> Get()
        {

            List<PenggunaGroup> _lst = new List<PenggunaGroup>();
            try
            {
                SSQL = "SELECT UserID,iGroup from mPenggunaGroup";

                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new PenggunaGroup()
                                {
                                    UserID  = DataFormat.GetInteger(dr["UserID"]),
                                    Group = DataFormat.GetInteger(dr["iGroup"]),
                                    
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

        public List<PenggunaGroup> GetDinasByUserID( int pID)
        {
        List<PenggunaGroup> lst = new List<PenggunaGroup>();
            try
            {
                SSQL = "SELECT UserID,iGroup from PenggunaGroup where UserID =" + pID.ToString();

                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows                                
                                select new PenggunaGroup()
                                {
                                    UserID = DataFormat.GetInteger(dr["UserID"]),
                                    Group = DataFormat.GetInteger(dr["iGroup"]),
                                    
                                }).ToList();
                    }
                }
                return lst;
            } catch(Exception ex){
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }


        }
        public bool Simpan(PenggunaGroup _pPenggunaGroup)
        {
            
            try
            {


                SSQL = "DELETE PenggunaGroup where UserID=@pUserID AND iGroup=@pGroup";
              
                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pUserID", _pPenggunaGroup.UserID));
                paramCollection.Add(new DBParameter("@pGroup", _pPenggunaGroup.Group));
                
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);



                SSQL = "INSERT INTO PenggunaGroup(UserID,iGroup) values (" +
                "@pUserID,@pGroup)";               

                
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                return true;
                
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool Hapus(PenggunaGroup pg)
        {

            try
            {


                SSQL = "DELETE PenggunaGroup where UserID=@pUserID AND iGroup=@pGroup";


                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pUserID", pg.UserID));
                paramCollection.Add(new DBParameter("@pGroup", pg.Group));
                

                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
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
