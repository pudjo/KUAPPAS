using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP;
using DTO;
using Formatting;
using System.Data;

namespace BP
{
    public class MapUrusanBaruLogic:BP 
    {
        public MapUrusanBaruLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel="MapUrusanBaru";
        }
        public bool Simpan(List<int> lstrusanBaru, int _urusanLama)
        {
            try
            {
                SSQL = "DELETE from " + m_sNamaTabel + " where idUrusan=" + _urusanLama.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                foreach (int iUrusanBaru in lstrusanBaru)
                {
                    SSQL = "INSERT into " + m_sNamaTabel + "(idUrusan , idUrusanBaru) values (" + _urusanLama.ToString() + "," + iUrusanBaru.ToString() + ")";
                    _dbHelper.ExecuteNonQuery(SSQL);

                }
                return true;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }
        }
        public bool Hapus(int _urusanLama)
        {
            try
            {
                SSQL = "DELETE from " + m_sNamaTabel + " where idUrusan=" + _urusanLama.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
             
                return true;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }

        }
        public bool Hapus(MapUrusanUrusanBaru _map)
        {
            try
            {
                SSQL = "DELETE from " + m_sNamaTabel + " where idUrusan=" + _map.UrusanLama.ToString() + " AND idUrusanBAru=" + _map.UrusanBaru.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);

                return true;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }

        }
        public List<MapUrusanUrusanBaru> GetByUrusanLama(int _urusanLama)
        {
            List<MapUrusanUrusanBaru> lstRet = new List<MapUrusanUrusanBaru>();

            try
            {
                SSQL = "SELECT " + m_sNamaTabel + ".*, mUrusanBaru.sNamaUrusan, mUrusan.sNamaUrusan as NamaLama  from " + m_sNamaTabel + " INNER JOIN mUrusanBaru ON mUrusanBaru.ID =MapUrusanBaru.idUrusanBAru " +
                    " INNER JOIN mUrusan ON MapUrusanBaru.IDUrusan = mUrusan.ID ";
                if (_urusanLama> 0 )
                    SSQL = SSQL + " where MapUrusanBaru.idUrusan=" + _urusanLama.ToString();

                SSQL = SSQL + " ORDER BY MapUrusanBaru.IDUrusan";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lstRet  = (from DataRow dr in dt.Rows
                                   select new MapUrusanUrusanBaru()
                                {
                                    UrusanBaru = DataFormat.GetInteger (dr["idUrusanBaru"]),
                                    UrusanLama = DataFormat.GetInteger(dr["idUrusan"]),
                                    Nama = DataFormat.GetString(dr["sNamaUrusan"]),
                                    NamaLama = DataFormat.GetString(dr["NamaLama"]),

                                }).ToList();
                    }
                }

                return lstRet;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }

        }

    }
}
