using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using BP;
using Formatting;
using DataAccess;
using System.Data;

namespace BP
{
    public class SingkronisasiLogic:BP 
    {
        public SingkronisasiLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "Singkronisasi";
        }

        public List<Singkronisasi> GetProgram(int IDUrusan)
        {

            List<Singkronisasi> _lst = new List<Singkronisasi>();
           
            try
            {
               
                if (IDUrusan ==0 )
                    SSQL = "Select tPrograms_A.IDProgram %100 as IDProgram,UPPER(tPrograms_A.sNamaProgram ) as Nama  , sum(tANggaranRekening_A.cJumlahRKAP)  as Jumlah " +
                    " from tPrograms_A inner join tANggaranRekening_A on tPrograms_A.Itahun = tANggaranRekening_A.iTahun  " +
                    " and tPrograms_A.IDUrusan= tANggaranRekening_A.IDurusan  " +
                    " and tPrograms_A.IDProgram= tANggaranRekening_A.IDProgram  where tPrograms_A.iTahun =2018  AND tPrograms_A.IDProgram %100 <15 " +
                    " Group by tPrograms_A.IDProgram %100,UPPER(tPrograms_A.sNamaProgram )  ORDER BY tPrograms_A.IDProgram %100 ";


                else

                SSQL = "Select tPrograms_A.IDProgram,UPPER(tPrograms_A.sNamaProgram ) as Nama  , sum(tANggaranRekening_A.cJumlahRKAP)  as Jumlah " +
                        " from tPrograms_A inner join tANggaranRekening_A on tPrograms_A.Itahun = tANggaranRekening_A.iTahun  " +
                        " and tPrograms_A.IDUrusan= tANggaranRekening_A.IDurusan  " +
                        " and tPrograms_A.IDProgram= tANggaranRekening_A.IDProgram  where tPrograms_A.iTahun =2018  AND tPrograms_A.IDUrusan =" +IDUrusan.ToString() +
                        " Group by tPrograms_A.IDProgram,UPPER(tPrograms_A.sNamaProgram ) oRDER BY tPrograms_A.IDProgram ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Singkronisasi()
                                {
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    CJumlah = DataFormat.GetDecimal (dr["Jumlah"]).ToRupiahInReport(),
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
