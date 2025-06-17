using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DTO;
using DTO.Bendahara;
using BP;
using Formatting;
using System.Data;

namespace BP.Bendahara
{
    public class JenisGajiLogic:BP 
    {
        public JenisGajiLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;

        }
        public List<JenisGaji> Get()
        {
            List<JenisGaji> _lst = new List<JenisGaji>();
            try
            {
                SSQL = "SELECT * from mJenisGaji Order by ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new JenisGaji()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Kode= DataFormat.GetString(dr["Singkatan"]),
                                    Nama = DataFormat.GetString(dr["sNama"]),
                                    
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
    }
}
