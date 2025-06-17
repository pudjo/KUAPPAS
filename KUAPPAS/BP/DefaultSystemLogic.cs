using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using BP;
using Formatting;
using System.Data;
namespace BP
{
    public class DefaultSystemLogic:BP
    {
        

        public DefaultSystemLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "defaultsystem";
        }
        public DefaultSystem Get()
        {
            DefaultSystem _object = new DefaultSystem();
            try
            {

                SSQL = "SELECT top 1 from " + m_sNamaTabel;
            
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    DataRow dr = null;


                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];

                        _object = new DefaultSystem()
                        {

                            DigitSKPD = DataFormat.GetInteger(dr["DigitSKPD"]),
                            DigitUK = DataFormat.GetInteger(dr["DigitUK"]),
                            DigitProgram = DataFormat.GetInteger(dr["DigitProgram"]),
                            DigitKegiatan = DataFormat.GetInteger(dr["DigitKegiatan"])

                        };
                    }
                }
                return _object;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _object;
            }



        }

    }
    
}
