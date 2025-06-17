using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using DTO;
using DTO.Bendahara;
using BP;
using Formatting;

namespace BP.Bendahara
{
    public class RefSetorLogic:BP 
    {
        public RefSetorLogic(int _pTahun)
            : base(_pTahun)
        {

        }
        public List<RefSetor> Get(char KodeMap)
        {
            List<RefSetor> _lst = new List<RefSetor>();
            try
            {
                SSQL = "select * from Refsetor order by KodeMap";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RefSetor()
                                {
                                    KodeMap = DataFormat.GetString(dr["KodeMap"]),
                                    KodeSetor = DataFormat.GetString(dr["KodeSetor"]),

                                    NamaMap= DataFormat.GetString(dr["NamaMap"]),
                                    NamaSetor = DataFormat.GetString(dr["NamaSetor"]),

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
