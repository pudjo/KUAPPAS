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
    class UrusanLogic:BP
    {
        public UrusanLogic(int _pTahun, int pproifile=1)
            : base(_pTahun, 0, pproifile)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mUrusan";
            
        }
        public List<Urusan> Get()
        {
            List<Urusan> _lst = new List<Urusan>();
            try
            {
                SSQL = "SELECT u.*, f.sNamaFungsi FROM mUrusan as U  inner join mFungsi as F ON U.btKodeFungsi = f.btKodeFungsi ORDER BY u.ID";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new Urusan()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    KodeKategori= DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan= DataFormat.GetInteger(dr["btKodeURusan"]),
                                    Nama= DataFormat.GetString(dr["sNamaUrusan"]),
                                    NamaFungsi= DataFormat.GetString(dr["sNamaFungsi"]),
                                    Fungsi= DataFormat.GetInteger(dr["btKodeFungsi"]),
                                    Tampilan =DataFormat.GetInteger(dr["ID"]).ToString().Substring(0,1) +"." + DataFormat.GetInteger(dr["ID"]).ToString().Substring(1,2) 
            
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
        public Urusan GetByID(int _pID )
        {
            Urusan oUrusan= new Urusan();
            try
            {
//                SSQL = "SELECT u.*, f.sNamaFungsi FROM mUrusan as U  inner join mFungsi as F ON U.btKodeFungsi = f.btKodeFungsi WHERE ID=" + _pID.ToString() +" ORDER BY ID";
                //SSQL = "SELECT u.*, f.sNamaFungsi FROM mUrusan as U  inner join mFungsi as F ON U.btKodeFungsi = f.btKodeFungsi WHERE ID=" + _pID.ToString() + " ORDER BY ID";
                SSQL = "SELECT u.* FROM mUrusan as U  WHERE ID=" + _pID.ToString() + " ORDER BY ID";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        oUrusan = new Urusan()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeURusan"]),
                                    Nama = DataFormat.GetString(dr["sNamaUrusan"]),
                                    NamaFungsi ="",// DataFormat.GetString(dr["sNamaFungsi"]),
                                    Fungsi = DataFormat.GetInteger(dr["btKodeFungsi"]),
                                    Tampilan = DataFormat.GetInteger(dr["ID"]).ToString().Substring(0, 1) + "." + DataFormat.GetInteger(dr["ID"]).ToString().Substring(1, 2)
                                };
                    }
                }
                return oUrusan;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return oUrusan;
            }
        }
    }
}
