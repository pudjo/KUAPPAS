using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using Formatting;
using DataAccess;
using BP;
using System.Data;


namespace BP
{
    public class GroupLogic:BP 
    {
        public GroupLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mGroup";
        }
        public List<Group> Get()
        {
            List<Group> _lst = new List<Group>();
            try
            {
                SSQL = "SELECT * FROM mGroup ORDER BY ID";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new Group()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),                                    
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
        public Group GetByID(int idKota, int pID)
        {
            Group _object = new Group();
            try
            {
                SSQL = "SELECT * FROM mGroup Where Kota =" + idKota.ToString() + " and ID ="+pID.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    DataRow dr = dt.NewRow();
                    
                    if (dt.Rows.Count > 0)
                    {
                        dr= dt.Rows[0];

                        _object = new Group()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),                
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
        public bool Simpan(ref Group _pGroup)
        {      
            try
            {
                int _newID;
                if (_pGroup.ID== 0)
                {
                    _newID = GetMaxID() + 1;                 

                    SSQL = "INSERT INTO mGroup(ID,Nama) values (" +
                        "@pID, @pNama)";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pID", _newID));
                    paramCollection.Add(new DBParameter("@pNama", _pGroup.Nama));                

                    _dbHelper.ExecuteNonQuery(SSQL,paramCollection);


                }
                else
                {
                    _newID= _pGroup.ID;
                    SSQL = "UPDATE mGroup SET Nama= @pNama, WHERE ID=@pID";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pID", _newID));
                    paramCollection.Add(new DBParameter("@pNama", _pGroup.Nama));                
                    _dbHelper.ExecuteNonQuery(SSQL,paramCollection);
                }
                return true;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;
            }
        }
        
        public bool Hapus(int _pIDGroup)
        {
            try
            {
                
                SSQL = "DELETE FROM mGroup WHERE ID=@pID";                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDGroup));
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
        private int GetMaxID()
        {
            int _maxID = 0;
            try
            {
                SSQL = "SELECT max(ID) as MAXID FROM mGroup ";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    DataRow dr = null;
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        _maxID = DataFormat.GetInteger(dr["MAXID"]);


                    }
                }
                return _maxID;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return 0;
            }
        }
    }
}
