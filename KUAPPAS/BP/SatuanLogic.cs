using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using System.Data;
using DataAccess;
using Formatting;

namespace BP
{
    public class SatuanLogic:BP 
    {
         public SatuanLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mSatuan";
        }
        public List<Satuan> Get()
        {
            List<Satuan> _lst = new List<Satuan>();
            try
            {
                SSQL = "SELECT * FROM Satuan ORDER BY ID";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new Satuan()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Nama= DataFormat.GetString(dr["Nama"]),
                                    Simbol = DataFormat.GetString(dr["Simbol"])
                                    
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
        public Satuan GetByID(int pID)
        {
            Satuan _object = new Satuan();
            try
            {
                SSQL = "SELECT * FROM Satuan Where ID ="+pID.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    DataRow dr = dt.NewRow();
                    
                    if (dt.Rows.Count > 0)
                    {
                        dr= dt.Rows[0];

                        _object = new Satuan()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Simbol = DataFormat.GetString(dr["Simbol"])
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
        public bool Simpan(ref Satuan _pSatuan)
        {
            try
            {
                int _newID;
                if (_pSatuan.ID== 0)
                {
                  
                    _newID = GetMaxID() + 1;
                    SSQL = "INSERT INTO Satuan(ID, Nama, Simbol) values (" +
                        "@pID, @pKota, @pKode,@pNama)";

                }
                else
                {
                    _newID= _pSatuan.ID;
                    SSQL = "UPDATE mSatuan SET Nama= @pNama, Kode=@pKode WHERE ID=@pID";

                }

                //DBParameterCollection paramCollection = new DBParameterCollection();
                //paramCollection.Add(new DBParameter("@pID", _newID));
                //paramCollection.Add(new DBParameter("@pKota", _pSatuan.Kota));
                //paramCollection.Add(new DBParameter("@pKode", _pSatuan.Kode));
                //paramCollection.Add(new DBParameter("@pNama", _pSatuan.Nama));                

                //if (_dbHelper.ExecuteNonQuery(SSQL,paramCollection) > 0)
                //{
                //    return true;
                //}
                //else
                //{
                    
                //    return false;
                //}
                return true;


            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool SimpanImport(List<Satuan> _lst )
        {


            try
            {
                if (Hapus() == false)
                {
                    return false;

                }
                foreach (Satuan k in _lst)
                {


                    SSQL = "INSERT INTO mSatuan(ID, Kota, Kode,Nama) values (" +
                        "@pID, @pKota, @pKode,@pNama)";



                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pID", k.ID));
                    paramCollection.Add(new DBParameter("@pKota", 1));
                    paramCollection.Add(new DBParameter("@pKode", k.ID));
                    paramCollection.Add(new DBParameter("@pNama", k.Nama));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
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
        public List<Desa> GetDesa(int _pSatuan)
        {
            return null;

        }
        public bool Hapus(int _pIDSatuan)
        {
            try
            {
                
                SSQL = "DELETE FROM mSatuan WHERE ID=@pID";                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDSatuan));
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
        public bool Hapus()
        {
            try
            {

                SSQL = "DELETE FROM mSatuan ";
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
        private int GetMaxID()
        {
            int _maxID = 0;
            try
            {
                SSQL = "SELECT max(ID) as MAXID FROM Satuan ";


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
