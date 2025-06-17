using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO.Bendahara;
using DataAccess;
using Formatting;
using BP;
using System.Data;
using System.Data.OleDb;

namespace BP.Bendahara
{
    public class SubSumberDanaLogic:BP 
    {
        public SubSumberDanaLogic(int _pTahun)
            : base(_pTahun)

        {
            Tahun = _pTahun;
            m_sNamaTabel = "mSubSumberDana";
        }
        public List<SubSumberDana> Get(int iSmbrDana)
        {
            List<SubSumberDana> _lst = new List<SubSumberDana>();
            try
            {
                //SSQL = "SELECT mSubSumberDana.*,mRekening.sNamaRekening from mSubSumberDana LEFT OUTER JOIN mRekening ON mSubSumberDana.IIDRekening = mRekening.IIDRekening  ORDER BY ID";
                SSQL = "SELECT * FROM mSubSumberDana where IDSumberDana =" + iSmbrDana.ToString() + " ORDER BY IDDetail";
                 DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SubSumberDana()
                                {
                                    IDSumberDana = DataFormat.GetInteger(dr["IDSumberDana"]),
                                   IDDetail= DataFormat.GetInteger(dr["IDDetail"]),
                                    Nama = DataFormat.GetString(dr["sNama"])                          
                                
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
        public bool Simpan(List<SubSumberDana> _lst)
        {
            
            try
            {
                foreach (SubSumberDana sd in _lst)
                {
                    SSQL = "";
                    if (sd.IDDetail== 0)
                    {
                        if (sd.Nama.Length > 0)
                        {
                            int id = GetMaxID();
                            SSQL = "INSERT into mSubSumberDana (IDDetail,sNama, IDSumberDana)values( " +
                                id.ToString() + ",'" + sd.Nama + "'," + sd.IDSumberDana.ToString() + ")";
                        }
                    }
                    else
                    {
                        SSQL = "UPDATE mSubSumberDana SET sNama='" + sd.Nama + "', IDSumberDana=" + sd.IDSumberDana.ToString() + " WHERE IDDetail=" + sd.IDDetail.ToString();
                    }
                    if (SSQL.Length>0)
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
        public bool Hapus(SubSumberDana sd)
        {
            try
            {
                //if (sd.ID == 0)
                //{
                //    SSQL = "INSERT into mSubSumberDana (ID,sNama, IIDRekening)values " +
                //        sd.ID.ToString() + ",'" + sd.Nama + "'," + sd.IDRekening.ToString() + ")";
                //}
                //else
                //{
                SSQL = "DELETE mSubSumberDana  WHERE IDDetail=" + sd.IDDetail.ToString();
                //}
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
        
    }
}
