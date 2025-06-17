using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;

using BP;
using DTO;
using DTO.Akuntansi;
using Formatting;

namespace BP.Akuntansi
{
    public class KOR6413Logic:BP 
    {
        public KOR6413Logic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "KOR6413";
//            PerbaikiTable();
        }
         public bool Simpan(List<KOR6413> lst, long iIDREKENING){
               try
               {

                   // bERSIHKAN DULU 
                   SSQL = "DELETE KOR5413 WHERE IIDRekening ";
                   _dbHelper.ExecuteNonQuery(SSQL);
                   foreach(KOR6413 dh in lst){
                       SSQL = "INSERT INTO KOR6413 (IIDREKENING, IIDREKENING64, iDefault) values(" + dh.IIDRekening13.ToString() +
                           "," + dh.IIDRekening64.ToString() + "," + dh.Default.ToString() + ") ";


                       _dbHelper.ExecuteNonQuery(SSQL);
                   }

                   if (lst.Count == 1)
                   {
                       SSQL = "UPDATE tANggaranRekening_A SET IIDRekening64 =" + lst[0].IIDRekening64.ToString() + " WHERE IIDREKENING =" + lst[0].IIDRekening13.ToString();
                       _dbHelper.ExecuteNonQuery(SSQL);

                   }

                   return true;


               }
               catch (Exception ex)
               {
                   _lastError = ex.Message;
                   _isError = true;

                   return false;
               }


           }
        public List<KOR6413> GetKorOf(long iidRekening)
        {

            List<KOR6413> lst = new List<KOR6413>();
            try
            {
                SSQL = "Select distinct vwAnggaran.IIDRekening,mRekening.sNamaRekening ,isnull(KOR6413.IIDrekening64,-1)as Rekening64, mRekening_SAP.sNamaRekening as NamaRekening64,KOR6413.iDefault  " +
                        " FROM vwAnggaran INNER JOIN mRekening  ON vwAnggaran.IIDrekening= mRekening.IIDRekening  LEFT JOIN KOR6413 ON KOR6413.IIDrekening= vwAnggaran.IIDRekening INNER JOIN mRekening_SAP  " +
                        " ON KOR6413.IIDrekening64= mRekening_SAP.IIDRekening WHERE vwAnggaran.IIDRekening=" + iidRekening.ToString();

                
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                       lst = (from DataRow dr in dt.Rows                                
                                select new KOR6413()
                                
                        {
                            IIDRekening13 = DataFormat.GetLong(dr["IIDRekening"]),
                            NamaRekening13 = DataFormat.GetString(dr["sNamaRekening"]),
                            IIDRekening64= DataFormat.GetLong(dr["Rekening64"]),
                            NamaRekening64 = DataFormat.GetString(dr["NamaRekening64"]),
                            Default= DataFormat.GetSingle(dr["iDefault"])
                            
                        }).ToList();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }



        }
        public List<KOR6413> Get()
        {

            List<KOR6413> lst = new List<KOR6413>();
            try
            {
                SSQL = "Select distinct vwAnggaran.IIDRekening,mRekening.sNamaRekening ,isnull(KOR6413.IIDrekening64,-1)as Rekening64, mRekening_SAP.sNamaRekening as NamaRekening64,KOR6413.iDefault  " +
                        " FROM vwAnggaran INNER JOIN mRekening  ON vwAnggaran.IIDrekening= mRekening.IIDRekening  LEFT JOIN KOR6413 ON KOR6413.IIDrekening= vwAnggaran.IIDRekening INNER JOIN mRekening_SAP  " +
                        " ON KOR6413.IIDrekening64= mRekening_SAP.IIDRekening ORDER BY vwAnggaran.IIDRekening asc,KOR6413.iDefault desc";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new KOR6413()

                               {
                                   IIDRekening13 = DataFormat.GetLong(dr["IIDRekening"]),
                                   NamaRekening13 = DataFormat.GetString(dr["sNamaRekening"]),
                                   IIDRekening64 = DataFormat.GetLong(dr["Rekening64"]),
                                   NamaRekening64 = DataFormat.GetString(dr["NamaRekening64"]),
                                   Default = DataFormat.GetSingle(dr["iDefault"])

                               }).ToList();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }



        } 
          public bool Hapus(int _idRekeningKOR6413)
          {

              try
              {
                  SSQL = "DELETE  FROM " + m_sNamaTabel + " WHERE WHERE IIDRekening = " + _idRekeningKOR6413.ToString();
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
