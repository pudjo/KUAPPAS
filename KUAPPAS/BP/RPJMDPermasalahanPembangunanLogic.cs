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
    public class RPJMDPermasalahanPembangunanLogic :BP 
    {

        public RPJMDPermasalahanPembangunanLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "PermaslahanPembangunan";
            CekTable();
        }
        private bool CekTable()
        {
            try
            {
                SSQL = "if OBJECT_ID('PermaslahanPembangunan') IS NULL  CREATE TABLE PermaslahanPembangunan(ID int , No int ,Tahun int ,IDUrusan int , Permaslahan varchar(1000), Keterangan varchar(1000), Status int )";
                _dbHelper.ExecuteNonQuery(SSQL);
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }
        public List<RPJMDPermasalahanPembangunan> GetByByUrusan(int pUrusan)
        {
            List<RPJMDPermasalahanPembangunan> _lst = new List<RPJMDPermasalahanPembangunan>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE Tahun =" + Tahun.ToString() + " AND IDUrusan =" + pUrusan.ToString() + " ORDER BY ID";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RPJMDPermasalahanPembangunan()
                                {
                                    No= DataFormat.GetInteger(dr["No"]),
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan= DataFormat.GetInteger(dr["IDurusan"]),
                                    Tahun = DataFormat.GetInteger(dr["Tahun"]),
                                    Permasalahan = DataFormat.GetString(dr["Permaslahan"]),
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

        public List<RPJMDPermasalahanPembangunan> Get()
        {
            List<RPJMDPermasalahanPembangunan> _lst = new List<RPJMDPermasalahanPembangunan>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " ORDER BY No";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RPJMDPermasalahanPembangunan()
                                {
                                    No = DataFormat.GetInteger(dr["No"]),
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                                    Tahun = DataFormat.GetInteger(dr["Tahun"]),
                                    Permasalahan = DataFormat.GetString(dr["Permaslahan"]),
                                    Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                    Status = DataFormat.GetInteger(dr["Status"]),
                                    
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
        public bool Simpan(ref RPJMDPermasalahanPembangunan _pPermasalahanPembangunan)
        {
                

            try
            {
                if (_pPermasalahanPembangunan.ID== 0)
                {
                    int _newID;
                    _newID = GetMaxID();

                    _pPermasalahanPembangunan.ID = _newID;
                    SSQL = "INSERT INTO PermaslahanPembangunan(ID, No,Tahun,IDUrusan, Permaslahan, Keterangan, Status) values (" +
                        "@ID, @pNo,@pTahun,@pIDUrusan, @pPermaslahan, @pKeterangan, @pStatus)";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@ID", _pPermasalahanPembangunan.ID));
                    paramCollection.Add(new DBParameter("@pNo", _pPermasalahanPembangunan.No));                    
                    paramCollection.Add(new DBParameter("@pTahun", _pPermasalahanPembangunan.Tahun));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pPermasalahanPembangunan.IDUrusan));
                    paramCollection.Add(new DBParameter("@pPermaslahan", _pPermasalahanPembangunan.Permasalahan));
                    paramCollection.Add(new DBParameter("@pKeterangan", _pPermasalahanPembangunan.Keterangan));
                    paramCollection.Add(new DBParameter("@pStatus", _pPermasalahanPembangunan.Status));

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

                    SSQL = "UPDATE PermaslahanPembangunan SET Permaslahan= @pPermasalahan,No = @pNo , IDurusan= @pIDUrusan,Keterangan=@pKeterangan,Status=@pStatus WHERE ID=@pID";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pPermasalahan", _pPermasalahanPembangunan.Permasalahan));
                    paramCollection.Add(new DBParameter("@pNo", _pPermasalahanPembangunan.No));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pPermasalahanPembangunan.IDUrusan));
                    paramCollection.Add(new DBParameter("@pKeterangan", _pPermasalahanPembangunan.Keterangan));
                    paramCollection.Add(new DBParameter("@pStatus", _pPermasalahanPembangunan.Status));
                    paramCollection.Add(new DBParameter("@pID", _pPermasalahanPembangunan.ID));
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
        public bool Hapus(int _pIDPermasalahanPembangunan)
        {
            try
            {

                SSQL = "DELETE FROM PermaslahanPembangunan WHERE ID=" + _pIDPermasalahanPembangunan.ToString();                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDPermasalahanPembangunan));

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
