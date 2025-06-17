using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using DTO.Bendahara;
using Formatting;
using DTO;
using BP;

namespace BP.Bendahara
{
    public class KelengkapanSPMLogic:BP 
    {
        public KelengkapanSPMLogic(int thn)
            : base(thn)
        {

        }
        public List<KelengkapanSPM> GetByJenisSPP(int jenisSPP)
        {
            List<KelengkapanSPM> _lst = new List<KelengkapanSPM>();
            try
            {
                SSQL = "SELECT mKelengkapanSPM.*  FROM mKelengkapanSPM where btJenisSPP= " + jenisSPP.ToString() + " Order by ID";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new KelengkapanSPM()
                                {
                                    No= DataFormat.GetInteger(dr["ID"]),
                                    JenisSPP= DataFormat.GetInteger(dr["btJenisSPP"]),
                                    Uraian= DataFormat.GetString(dr["sUraian"])

                                   
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
