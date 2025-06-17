using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;

using Formatting;
using DataAccess;
using System.Data;

namespace BP.Akuntansi
{
    public class LaporanAPMLogic:BP
    {
        public List<PerdaSPM> GetPerdaSPM050(ParameterLaporan _p)
        {
            List<PerdaSPM> _lst = new List<PerdaSPM>();
            try
            {
                SSQL = "";
                if (_p.Tahun < 2022)
                    SSQL = "select * from vwPerdaSPM order by IDUrusan ,Level, IDkegiatan ";
                else

                    SSQL = "select level,Abjad, Nama, NamaKegiatan, NamaSubKegiatan, SUM(Anggaran) as Anggaran ,"+
                           "SUM(Realisasi) as Realisasi from dbo.fnPerdaSPM() group by level,Abjad, " +
                           "Nama, NamaKegiatan, NamaSubKegiatan   having SUM(Anggaran) <>0   order by   abjad,level";



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaSPM()
                                {

                                    


                                    No = DataFormat.GetString(dr["Abjad"]),  //DataFormat.GetInteger(dr["Level"]) < 5 ? DataFormat.GetString(dr["Nama"]).ToUpper() : DataFormat.GetString(dr["Nama"]),
                                    
                                    Nama = DataFormat.GetString(dr["Nama"]),  
                                    NamaKegiatan = DataFormat.GetString(dr["NamaKegiatan"]),
                                    NamaSubKegiatan = DataFormat.GetString(dr["NamaSubKegiatan"]),

                                    Anggaran = DataFormat.GetDecimal(dr["Anggaran"]),
                                    Realisasi = DataFormat.GetDecimal(dr["Realisasi"]),
                                    
                                    


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
