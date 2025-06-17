using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using Formatting;
using DataAccess;
using System.Data;

namespace BP
{
    public class VisiLogic:BP 
    {
          public VisiLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mPermaslahanPembangunan";
        }

        public List<Visi> GetByByUrusan(int pUrusan)
        {
            List<Visi> _lst = new List<Visi>();
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
                                select new Visi()
                                {
                                    No= DataFormat.GetInteger(dr["No"]),
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Periode = DataFormat.GetInteger(dr["Periode"]),
                                    Uraian = DataFormat.GetString(dr["Uraian"]),
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

        public List<Visi> Get()
        {
            List<Visi> _lst = new List<Visi>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " Where Tahun = " + Tahun.ToString() + " ORDER BY ID";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new Visi()
                                {
                                    No = DataFormat.GetInteger(dr["No"]),
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Periode = DataFormat.GetInteger(dr["Periode"]),
                                    Uraian = DataFormat.GetString(dr["Uraian"]),
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
        public bool Simpan(ref Visi _pVisi)
        {
                

            try{
            //{
            //    if (_pVisi.ID== 0)
            //    {
            //        int _newID;
            //        _newID = GetMaxID();

            //        _pVisi.ID = _newID;
            //        SSQL = "INSERT INTO Visi(ID, No,Tahun,IDUrusan, Permaslahan, Keterangan, Status) values (" +
            //            "@ID, @pNo,@pTahun,@pIDUrusan, @pPermaslahan, @pKeterangan, @pStatus)";

            //        DBParameterCollection paramCollection = new DBParameterCollection();
            //        paramCollection.Add(new DBParameter("@ID", _pVisi.ID));
            //        paramCollection.Add(new DBParameter("@pNo", _pVisi.No));                    
            //        paramCollection.Add(new DBParameter("@pTahun", _pVisi.Tahun));
            //        paramCollection.Add(new DBParameter("@pIDUrusan", _pVisi.IDUrusan));
            //        paramCollection.Add(new DBParameter("@pPermaslahan", _pVisi.Permasalahan));
            //        paramCollection.Add(new DBParameter("@pKeterangan", _pVisi.Keterangan));
            //        paramCollection.Add(new DBParameter("@pStatus", _pVisi.Status));

            //    }
            //    else
            //    {

            //        SSQL = "UPDATE tVisi_A SET Permaslahan= @pPermasalahan,No = @pNo , IDurusan= @pIDUrusan,Keterangan=@pKeterangan,Status=@pStatus WHERE ID=@pID";
            //        DBParameterCollection paramCollection = new DBParameterCollection();
            //        paramCollection.Add(new DBParameter("@pPermaslahan", _pVisi.Permasalahan));
            //        paramCollection.Add(new DBParameter("@pNo", _pVisi.No));
            //        paramCollection.Add(new DBParameter("@pIDUrusan", _pVisi.IDUrusan));
            //        paramCollection.Add(new DBParameter("@pKeterangan", _pVisi.Keterangan));
            //        paramCollection.Add(new DBParameter("@pStatus", _pVisi.Status));
            //        paramCollection.Add(new DBParameter("@ID", _pVisi.ID));
                    

            //    }
                
            //    if (_dbHelper.ExecuteNonQuery(SSQL) > 0)
            //    {
                    return true;
                //}
                //else
                //{
                //    return false;
                //}

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool Hapus(int _pIDVisi)
        {
            try
            {

                SSQL = "DELETE FROM tVisi_A WHERE ID=@pID";                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDVisi));
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
