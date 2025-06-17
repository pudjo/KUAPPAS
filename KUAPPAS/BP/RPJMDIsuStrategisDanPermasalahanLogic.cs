using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DTO;
using DataAccess;
using Formatting;

namespace BP
{
    public class RPJMDIsuStrategisDanPermasalahanLogic : BP
    {
        public RPJMDIsuStrategisDanPermasalahanLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "IsuStrategisDanPermasalahanLogic ";
        }

        public List<RPJMDIsuStrategisDanPermasalahan> Get()
        {
            List<RPJMDIsuStrategisDanPermasalahan> _lst = new List<RPJMDIsuStrategisDanPermasalahan>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE Tahun =" + Tahun.ToString() + " ORDER BY No ";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RPJMDIsuStrategisDanPermasalahan()
                                {
                                    
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDIsuStrategis = DataFormat.GetInteger(dr["IDIsuStrategis"]),
                                    IDPermasalahan= DataFormat.GetInteger(dr["IDPermasalahan"])                                    
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
        public List<RPJMDIsuStrategisDanPermasalahan> GetByIDSTrategis(int IDSTrategis)
        {
            List<RPJMDIsuStrategisDanPermasalahan> _lst = new List<RPJMDIsuStrategisDanPermasalahan>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE IDIsuStrategis =" + IDSTrategis.ToString() + " ORDER BY ID ";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RPJMDIsuStrategisDanPermasalahan()
                                {

                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDIsuStrategis = DataFormat.GetInteger(dr["IDIsuStrategis"]),
                                    IDPermasalahan = DataFormat.GetInteger(dr["IDPermasalahan"])
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



        public bool Simpan(ref RPJMDIsuStrategisDanPermasalahan _pIsuStrategisDanPermasalahan)
        {


            try
            {
                int _newID;
                _newID = GetMaxID();

                _pIsuStrategisDanPermasalahan.ID = _newID;
                SSQL = "INSERT INTO IsuStrategisDanPermasalahan(ID, IDIsuStrategis,IDPermasalahan) values (" +
                    "@pID, @pIDIsuStrategis,@pIDPermasalahan)";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@ID", _pIsuStrategisDanPermasalahan.ID));
                paramCollection.Add(new DBParameter("@pIDIsuStrategis", _pIsuStrategisDanPermasalahan.IDIsuStrategis));
                paramCollection.Add(new DBParameter("@pIDPermasalahan", _pIsuStrategisDanPermasalahan.IDPermasalahan));






                if (_dbHelper.ExecuteNonQuery(SSQL) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool Hapus(RPJMDIsuStrategisDanPermasalahan _pIsuStrategisDanPermasalahan)
        {
            try
            {

                SSQL = "DELETE FROM IsuStrategisDanPermasalahan WHERE  IDIsuStrategis=@pIDIsuStrategis and IDPermasalahan=@pIDPermasalahan";                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pIDIsuStrategis", _pIsuStrategisDanPermasalahan.IDIsuStrategis));
                paramCollection.Add(new DBParameter("@pIDPermasalahan", _pIsuStrategisDanPermasalahan.IDPermasalahan));
                
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
    }
}
