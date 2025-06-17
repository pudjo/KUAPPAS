using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using DTO;
using BP;
using Formatting;
using System.Data;



namespace BP
{
    public class JenisAnggaranLogic:BP 
    {

        public JenisAnggaranLogic(int _pTahun)
            : base(_pTahun)
        {

        }
        public List<JenisAnggaran> Get(int Tahap) { 

            List<JenisAnggaran> _lst = new List<JenisAnggaran>();
            try
            {
                SSQL = "SELECT distinct * FROM JenisAnggaran where iTahun =" + Tahun.ToString() + " AND Tahap =" + Tahap.ToString() + "  Order BY Jenis";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new JenisAnggaran()
                                {

                                    //CREATE TABLE JenisAnggaran ( iTahun int,Jenis,Tahap smallint, Nama varchar(60),Status smallint)
                                    Tahun =DataFormat.GetInteger(dr["iTahun"]),
                                    Tahap =DataFormat.GetInteger(dr["Tahap"]),
                                    Jenis =DataFormat.GetInteger(dr["Jenis"]),
                                    Nama =DataFormat.GetString(dr["Nama"]),
                                    Status = DataFormat.GetInteger(dr["Status"])

                                    //ID = DataFormat.GetSingle(dr["ID"]),
                                    //Nama = DataFormat.GetString(dr["Nama"])                                    
                                    
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
        public bool UpdateStatus(JenisAnggaran j)
        {
            try
            {
                SSQL = "UPDATE JenisAnggaran SET Status = " + j.Status.ToString() + " where iTahun =" + j.Tahun.ToString() + " AND Tahap =" + j.Tahap.ToString() + "  AND Jenis=" + j.Jenis.ToString();
                DataTable dt = new DataTable();
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
