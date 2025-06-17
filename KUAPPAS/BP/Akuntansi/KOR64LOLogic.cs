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
    public class KOR64LOLogic:BP
    {
        public KOR64LOLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "KOR_LRA_LO";
//            PerbaikiTable();
        }
         public bool Simpan(List<KOR64LO> lst, long iIDREKENING){
               try
               {

                   // bERSIHKAN DULU 
                   SSQL = "DELETE KOR_LRA_LO WHERE IIDRekening ";
                   _dbHelper.ExecuteNonQuery(SSQL);
                   foreach(KOR64LO dh in lst){
                       SSQL = "INSERT INTO KOR_LRA_LO (IIDREKENING, IIDREKENINGLO) values(" + dh.IIDRekening.ToString() +
                           "," + dh.IIDRekeningLO.ToString() + ")";//," + dh.Default.ToString() + ") ";


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
        public List<KOR64LO> GetKorOf(long iidRekening)
        {

            List<KOR64LO> lst = new List<KOR64LO>();
            try
            {
                SSQL = "Select distinct A.IIDRekening,A.sNamaRekening ,isnull(KOR_LRA_LO.IIDrekeningLO,-1)as RekeningLO, " +
                     " B.sNamaRekening as NamaRekeningLO " +
                        " FROM mRekening_SAP A  LEFT JOIN KOR_LRA_LO ON KOR_LRA_LO.IIDrekening= A.IIDRekening  INNER JOIN mRekening_SAP B " +
                        " ON KOR_LRA_LO.IIDrekeningLO= B.IIDRekening WHERE A.IIDRekening=" + iidRekening.ToString();

                
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                       lst = (from DataRow dr in dt.Rows                                
                                select new KOR64LO()
                                
                        {
                            IIDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                            NamaRekening = DataFormat.GetString(dr["sNamaRekening"]),
                            IIDRekeningLO= DataFormat.GetLong(dr["RekeningLO"]),
                            NamaRekeningLO = DataFormat.GetString(dr["NamaRekeningLO"])
                            
                            
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
        public List<KOR64LO> Get()
        {

            List<KOR64LO> lst = new List<KOR64LO>();
            try
            {
                SSQL = "Select distinct A.IIDRekening,A.sNamaRekening ,isnull(KOR_LRA_LO.IIDrekeningLO,-1)as RekeningLO, " +
                     " B.sNamaRekening as NamaRekeningLO " +
                        " FROM mRekening_SAP A  LEFT JOIN KOR_LRA_LO ON KOR_LRA_LO.IIDrekening= A.IIDRekening  INNER JOIN mRekening_SAP B " +
                        " ON KOR_LRA_LO.IIDrekeningLO= B.IIDRekening ";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new KOR64LO()

                               {
                                   IIDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                   NamaRekening = DataFormat.GetString(dr["sNamaRekening"]),
                                   IIDRekeningLO = DataFormat.GetLong(dr["RekeningLO"]),
                                   NamaRekeningLO = DataFormat.GetString(dr["NamaRekeningLO"])
                
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
          public bool Hapus(int _idRekeningKOR64LO)
          {

              try
              {
                  SSQL = "DELETE  FROM " + m_sNamaTabel + " WHERE WHERE IIDRekening = " + _idRekeningKOR64LO.ToString();
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
