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
    public class RPJMDMisiLogic:BP 
    {
        public RPJMDMisiLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "Misi";
            CekTable();
        }
        private bool CekTable()
        {
            try
            {
                SSQL = "if OBJECT_ID('Misi') IS NULL  CREATE TABLE Misi(ID int , No int,Misi varchar(3000))";
                _dbHelper.ExecuteNonQuery(SSQL);
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }
        public List<RPJMDMisi> Get()
        {
            List<RPJMDMisi> _lst = new List<RPJMDMisi>();
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
                                select new RPJMDMisi()
                                {
                                    No= DataFormat.GetInteger(dr["No"]),
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Misi = DataFormat.GetString(dr["Misi"]),
                                    
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
        public RPJMDMisi GetByID( int pID)
        {
            RPJMDMisi o = new RPJMDMisi();
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

                        o = new RPJMDMisi()
                                {
                                    No = DataFormat.GetInteger(dr["No"]),
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Misi = DataFormat.GetString(dr["Misi"]),

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


        public bool Simpan(ref RPJMDMisi _pMisi)
        {
                

            try
            {
                if (_pMisi.ID == 0)
                {
                    int _newID;
                    _newID = GetMaxID();

                    _pMisi.ID = _newID;
                    SSQL = "INSERT INTO Misi(ID, No,Misi) values (" +
                        "@ID, @pNo,@pMisi)";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@ID", _pMisi.ID));
                    paramCollection.Add(new DBParameter("@pNo", _pMisi.No));
                    paramCollection.Add(new DBParameter("@pMisi", _pMisi.Misi));
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

                    SSQL = "UPDATE Misi SET Misi= @pMisi,No = @pNo WHERE ID=@pID";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pMisi", _pMisi.Misi));
                    paramCollection.Add(new DBParameter("@pNo", _pMisi.No));
                    paramCollection.Add(new DBParameter("@pID", _pMisi.ID));




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
        public bool Hapus(int _pIDMisi)
        {
            try
            {

                SSQL = "DELETE FROM Misi WHERE ID=@pID";                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDMisi));
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
