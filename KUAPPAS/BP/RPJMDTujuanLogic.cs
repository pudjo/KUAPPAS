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
    public class RPJMDTujuanLogic:BP 
    {
         public RPJMDTujuanLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "Tujuan";
            CekTable();
        }
        private bool CekTable()
        {
            try
            {
                SSQL = "if OBJECT_ID('Tujuan') IS NULL  CREATE TABLE Tujuan(ID int , No int,Misi int ,Tujuan varchar(3000))";
                _dbHelper.ExecuteNonQuery(SSQL);
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }
        public List<RPJMDTujuan> Get()
        {
            List<RPJMDTujuan> _lst = new List<RPJMDTujuan>();
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
                                select new RPJMDTujuan()
                                {
                                    No= DataFormat.GetInteger(dr["No"]),
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Tujuan = DataFormat.GetString(dr["Tujuan"]),
                                    Misi = DataFormat.GetInteger(dr["Misi"])
                                    
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
        public List<RPJMDTujuan> GetByMisi(int _MisiID)
        {
            List<RPJMDTujuan> _lst = new List<RPJMDTujuan>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE Misi =" + _MisiID.ToString() + " ORDER BY No ";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RPJMDTujuan()
                                {
                                    No = DataFormat.GetInteger(dr["No"]),
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Tujuan = DataFormat.GetString(dr["Tujuan"]),
                                    Misi = DataFormat.GetInteger(dr["Misi"])

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
        public RPJMDTujuan GetByID( int pID)
        {
            RPJMDTujuan o = new RPJMDTujuan();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE ID= " + pID.ToString() ;


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        o = new RPJMDTujuan()
                                {
                                    No = DataFormat.GetInteger(dr["No"]),
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Tujuan = DataFormat.GetString(dr["Tujuan"]),
                                    Misi = DataFormat.GetInteger(dr["Misi"])

                                };

                    }
                }
                return o;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }
        }


        public bool Simpan(ref RPJMDTujuan _pTujuan)
        {
                

            try
            {
                if (_pTujuan.ID == 0)
                {
                    int _newID;
                    _newID = GetMaxID();

                    _pTujuan.ID = _newID;
                    SSQL = "INSERT INTO Tujuan(ID, No,Misi,Tujuan) values (" +
                        "@ID, @pNo,@pMisi,@pTujuan)";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@ID", _pTujuan.ID));
                    paramCollection.Add(new DBParameter("@pNo", _pTujuan.No));
                    paramCollection.Add(new DBParameter("@pMisi", _pTujuan.Misi));
                    paramCollection.Add(new DBParameter("@pTujuan", _pTujuan.Tujuan));
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

                    SSQL = "UPDATE Tujuan SET Tujuan= @pTujuan,No = @pNo,Misi=@pMisi WHERE ID=@pID";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pTujuan", _pTujuan.Tujuan));
                    paramCollection.Add(new DBParameter("@pNo", _pTujuan.No));
                    paramCollection.Add(new DBParameter("@pMisi", _pTujuan.Misi));
                    paramCollection.Add(new DBParameter("@pID", _pTujuan.ID));




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
        public bool Hapus(int _pIDTujuan)
        {
            try
            {

                SSQL = "DELETE FROM Tujuan WHERE ID=@pID";                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDTujuan));
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
