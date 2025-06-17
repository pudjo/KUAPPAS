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
    public class RPJMDSasaranLogic:BP 
    {
         public RPJMDSasaranLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "Sasaran";
            CekTable();
        }
        private bool CekTable()
        {
            try
            {
                SSQL = "if OBJECT_ID('Sasaran') IS NULL  CREATE TABLE Sasaran(ID int , No int,Misi int ,Tujuan int,Sasaran varchar(3000))";
                _dbHelper.ExecuteNonQuery(SSQL);
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }
        public List<RPJMDSasaran> Get()
        {
            List<RPJMDSasaran> _lst = new List<RPJMDSasaran>();
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
                                select new RPJMDSasaran()
                                {
                                    No= DataFormat.GetInteger(dr["No"]),
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Sasaran = DataFormat.GetString(dr["Sasaran"]),
                                    Misi = DataFormat.GetInteger(dr["Misi"]),
                                    Tujuan = DataFormat.GetInteger(dr["Tujuan"])
                                    
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
        public List<RPJMDSasaran> GetByMisi(int _MisiID)
        {
            List<RPJMDSasaran> _lst = new List<RPJMDSasaran>();
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
                                select new RPJMDSasaran()
                                {
                                    No = DataFormat.GetInteger(dr["No"]),
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Sasaran = DataFormat.GetString(dr["Sasaran"]),
                                    Tujuan= DataFormat.GetInteger(dr["Tujuan"]),
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
        public RPJMDSasaran GetByID( int pID)
        {
            RPJMDSasaran o = new RPJMDSasaran();
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

                        o = new RPJMDSasaran()
                                {
                                    No = DataFormat.GetInteger(dr["No"]),
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Sasaran = DataFormat.GetString(dr["Sasaran"]),
                                    Tujuan= DataFormat.GetInteger(dr["Tujuan"]),
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


        public bool Simpan(ref RPJMDSasaran _pSasaran)
        {
                

            try
            {
                if (_pSasaran.ID == 0)
                {
                    int _newID;
                    _newID = GetMaxID();

                    _pSasaran.ID = _newID;
                    SSQL = "INSERT INTO Sasaran(ID, No,Misi,Tujuan,Sasaran) values (" +
                        "@ID, @pNo,@pMisi,@pTujuan,@pSasaran)";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@ID", _pSasaran.ID));
                    paramCollection.Add(new DBParameter("@pNo", _pSasaran.No));
                    paramCollection.Add(new DBParameter("@pMisi", _pSasaran.Misi));
                    paramCollection.Add(new DBParameter("@pTujuan", _pSasaran.Misi));
                    paramCollection.Add(new DBParameter("@pSasaran", _pSasaran.Sasaran));
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

                    SSQL = "UPDATE Sasaran SET Sasaran= @pSasaran,No = @pNo,Misi=@pMisi,Tujuan=@pTujuan WHERE ID=@pID";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pSasaran", _pSasaran.Sasaran));
                    paramCollection.Add(new DBParameter("@pNo", _pSasaran.No));
                    paramCollection.Add(new DBParameter("@pMisi", _pSasaran.Misi));
                    paramCollection.Add(new DBParameter("@pTujuan", _pSasaran.Tujuan));
                    paramCollection.Add(new DBParameter("@pID", _pSasaran.ID));




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
        public bool Hapus(int _pIDSasaran)
        {
            try
            {

                SSQL = "DELETE FROM Sasaran WHERE ID=@pID";                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDSasaran));
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
