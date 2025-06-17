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
    public class KegiatanMusrenmbangLogic:BP
    {
        public KegiatanMusrenmbangLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mKegiatanmusrenmbang";
        }
        public List<KegiatanMusrenmbang> Get()
        {

            List<KegiatanMusrenmbang> _lst = new List<KegiatanMusrenmbang>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " ORDER BY ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new KegiatanMusrenmbang()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    Nama = DataFormat.GetString(dr["Nama"])

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
        public List<KegiatanMusrenmbang> GetByUrusan(int _pUrusan)
        {

            List<KegiatanMusrenmbang> _lst = new List<KegiatanMusrenmbang>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE IDurusan=" + _pUrusan.ToString() + " ORDER BY IDKegiatan";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new KegiatanMusrenmbang()
                                {
                                    ID= DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    Nama= DataFormat.GetString(dr["Nama"])

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
        public KegiatanMusrenmbang GetByID(int _pID)
        {

            KegiatanMusrenmbang _o = new KegiatanMusrenmbang();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + "  WHERE IDKegiatan=" + _pID.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        _o = new KegiatanMusrenmbang()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    Nama= DataFormat.GetString(dr["Nama"])
                                };
                    }
                }
                return _o;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _o;
            }
        }
        public bool Simpan(ref KegiatanMusrenmbang _pKegiatan)
        {
            try
            {
                //int _newID;
                //if (_pKegiatan.ID == 0)
                //{

                    
                    //_pKegiatan.ID = _newID;
                    SSQL = "INSERT INTO mKegiatanmusrenmbang(ID,IDUrusan,IDProgram,iTahun, IDKegiatan, Nama) values (" +
                        "@pID,@pIDUrusan,@pIDProgram,@piTahun, @pIDKegiatan, @pNama)";
                //}
                //else
                //{
                //    SSQL = "UPDATE mKegiatanmusrenmbang SET IDUrusan=@pIDUrusan,IDProgram=@pIDProgram,iTahun=@piTahun, IDKegiatan=@pIDKegiatan, Nama=@pNama WHERE ID=@pID ";

                //}
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pKegiatan.ID));
                paramCollection.Add(new DBParameter("@pIDUrusan", _pKegiatan.IDUrusan));
                paramCollection.Add(new DBParameter("@pIDKegiatan", _pKegiatan.IDKegiatan));
                paramCollection.Add(new DBParameter("@pIDProgram", _pKegiatan.IDProgram));
                paramCollection.Add(new DBParameter("@piTahun", _pKegiatan.Tahun));
                paramCollection.Add(new DBParameter("@pNama", _pKegiatan.Nama));
                
                if (_dbHelper.ExecuteNonQuery(SSQL,paramCollection) > 0)
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
        public bool Hapus(int _pIDKegiatan)
        {
            try
            {
                SSQL = "DELETE FROM mkegiatanmusrenmbang WHERE ID=@pID";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDKegiatan));
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
