using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using System.Data;
using DataAccess;
using Formatting;

namespace BP
{
    public class RekapKUALogic:BP 
    {
        public RekapKUALogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
        }
        public List<REKAPKUA> Get(int iJenis)
        {
            List<REKAPKUA> _lst = new List<REKAPKUA>();

            try
            {

                SSQL = "Select mSKPD.ID, mSKPD.sNamaSKPD as Nama, SUM(tKUA.JumlahMurni) as Jumlah,SUM(tKUA.JumlahRKPD) as JumlahRKPD" +
                         " FROM tKUA INNER JOIN mSKPD ON tKUA.IDDInas= mSKPD.ID " +
                         " where btJenis = " + iJenis.ToString() + " and tKUA.iTahun = " + Tahun.ToString() + " AND tKUA.IDlokasi=0  AND isnull(tKUA.Status,0)<9 " +
                        " GROUP BY mSKPD.ID, mSKPD.sNamaSKPD ";
                        

                SSQL=SSQL+      " ORDER BY mSKPD.ID ";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new REKAPKUA()
                                {
                                    
                                      NamaDinas = DataFormat.GetString (dr["Nama"]),
                                      JumlahPagu=DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
                                      Kode = DataFormat.GetInteger(dr["ID"]).ToKodeDinas(),
                                      DJumlahPagu = DataFormat.GetDecimal(dr["Jumlah"])


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
        
        
    }
}
