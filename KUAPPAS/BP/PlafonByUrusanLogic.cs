using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP;
using DTO;
using System.Data;
using DataAccess;
using Formatting;
namespace BP
{
    class PlafonByUrusanLogic:BP 
    {
        public PlafonByUrusanLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
        }
        public List<PlafonByUrusan> Get(decimal _batasBTL = -1)
        {

            List<PlafonByUrusan> _lst = new List<PlafonByUrusan>();
            try
            {
            

                //SSQL = " Select IDurusan, 0 as IDDinas, mUrusan.sNamaUrusan as Nama ,SUM(JUmlahBTL) as JumlahBTL, SUM(JumlahBL) as JumlahBL, " +
                //        " 0 as IIDRekening,1 as Level from  " +
                //        " vwRekapPerDinasPerJenis INNER JOIN mUrusan ON mUrusan.ID=vwRekapPerDinasPerJenis.IDurusan GROUP BY IDUrusan,mUrusan.sNamaUrusan  " +
                //        " UNION  " 
                SSQL = " Select IDDinas, Nama,SUM(JUmlahBTL) as JumlahBTL, SUM(JumlahBL) as JumlahBL,0 as IIDRekening,2 as Level from  " +
                        " vwRekapPerDinasPerJenis GROUP BY IDDinas, Nama  " +
                        " UNION  " +
                        "Select tKUA.IDDinas, mRekening.sNamaRekening as Nama, JumlahOlah as JumlahBTL,  " +
                            " 0 as JumlahBL, tKUA.IIDRekening, 3 as Level  " +
                        "FROM tKUA INNER JOIN mRekening ON tKUA.IIDRekening = mRekening.IIDRekening  " +
                            " WHere tKUA.btJenis=2  " +
                            " and IDDInas in (Select IDDInas from tKUA where btJenis=2 GROUP by IDDInas ";
                if (_batasBTL > -1)
                {
                    SSQL = SSQL + " having SUM(JumlahOlah)>1000000000 ) ";
                }
                else
                {
                    SSQL = SSQL + " ) ";
                }
                SSQL = SSQL + " Order BY IDDInas,IIDRekening ";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PlafonByUrusan()
                                {
                                    BL = DataFormat.GetDecimal(dr["JumlahBL"]),
                                    BTL = DataFormat.GetDecimal(dr["JumlahBTL"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Level = DataFormat.GetSingle(dr["Level"]),
                                    Jumlah = DataFormat.GetDecimal(dr["JumlahBL"]) + DataFormat.GetDecimal(dr["JumlahBTL"]),
                                 Nama = DataFormat.GetString(dr["Nama"]),
                                    
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
//SSQL = " Select IDDinas, Nama,SUM(JUmlahBTL) as JumlahBTL, SUM(JumlahBL) as JumlahBL,0 as IIDRekening,2 as Level from  " +
//                        " vwRekapPerDinasPerJenis GROUP BY IDDinas, Nama  " +
//                        " UNION  " +
//                        "Select Substring(Convert(varchar(7),IDDInas),0,4) as IDUrusan, tKUA.IDDinas, mRekening.sNamaRekening as Nama, JumlahOlah as JumlahBTL,  " +
//                            " 0 as JumlahBL, tKUA.IIDRekening, 3 as Level  " +
//                        "FROM tKUA INNER JOIN mRekening ON tKUA.IIDRekening = mRekening.IIDRekening  " +
//                            " WHere tKUA.btJenis=2  " +
//                            " and IDDInas in (Select IDDInas from tKUA where btJenis=2 GROUP by Substring(Convert(varchar(7),IDDInas),0,4),IDDInas ";