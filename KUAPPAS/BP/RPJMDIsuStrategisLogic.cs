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
    public class RPJMDIsuStrategisLogic:BP 
    {
        public RPJMDIsuStrategisLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "IsuStrategis";
            CekTable();
        }
        private bool CekTable()
        {
            try
            {
                SSQL = "if OBJECT_ID('IsuStrategis') IS NULL  CREATE TABLE IsuStrategis(ID int , No int,Tahun int ,IsuStrategis varchar(1200), Keterangan  varchar(1200), Status int )";
                _dbHelper.ExecuteNonQuery(SSQL);
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }
        public List<RPJMDIsuStrategis> Get()
        {
            List<RPJMDIsuStrategis> _lst = new List<RPJMDIsuStrategis>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + "  ORDER BY No ";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RPJMDIsuStrategis()
                                {
                                    No= DataFormat.GetInteger(dr["No"]),
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Tahun = DataFormat.GetInteger(dr["Tahun"]),
                                    Isu = DataFormat.GetString(dr["IsuStrategis"]),
                                    Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                    Status = DataFormat.GetInteger(dr["Status"]),
                                    
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


        public bool Simpan(ref RPJMDIsuStrategis _pIsuStrategis)
        {
                

            try
            {
                if (_pIsuStrategis.ID == 0)
                {
                    int _newID;
                    _newID = GetMaxID();

                    _pIsuStrategis.ID = _newID;
                    SSQL = "INSERT INTO IsuStrategis(ID, No,Tahun,IsuStrategis, Keterangan, Status) values (" +
                        "@ID, @pNo,@pTahun,@pIsuStrategis, @pKeterangan, @pStatus)";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@ID", _pIsuStrategis.ID));
                    paramCollection.Add(new DBParameter("@pNo", _pIsuStrategis.No));
                    paramCollection.Add(new DBParameter("@pTahun", _pIsuStrategis.Tahun));
                    paramCollection.Add(new DBParameter("@pIsuStrategis", _pIsuStrategis.Isu));
                    paramCollection.Add(new DBParameter("@pKeterangan", _pIsuStrategis.Keterangan));
                    paramCollection.Add(new DBParameter("@pStatus", _pIsuStrategis.Status));
                    if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {

                    SSQL = "UPDATE IsuStrategis SET IsuStrategis= @pIsuStrategis,No = @pNo , Keterangan=@pKeterangan,Status=@pStatus WHERE ID=@pID";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pIsuStrategis", _pIsuStrategis.Isu));
                    paramCollection.Add(new DBParameter("@pNo", _pIsuStrategis.No));
                    paramCollection.Add(new DBParameter("@pKeterangan", _pIsuStrategis.Keterangan));
                    paramCollection.Add(new DBParameter("@pStatus", _pIsuStrategis.Status));
                    paramCollection.Add(new DBParameter("@pID", _pIsuStrategis.ID));




                    if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool Hapus(int _pIDIsuStrategis)
        {
            try
            {

                SSQL = "DELETE FROM IsuStrategis WHERE ID=@pID";                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDIsuStrategis));
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
    }
}
