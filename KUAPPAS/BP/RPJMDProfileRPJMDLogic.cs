using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using Formatting;
using DataAccess;
using System.Data;

namespace BP
{
    public class RPJMDProfileRPJMDLogic:BP 
    {
        public RPJMDProfileRPJMDLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "ProfileRPJMD";

            CekTable();
            
        }
        public bool Simpan(RPJMDProfileRPJMD oProfileRPJMD)
        {
            try
            {
                //int lID = 1;
                
                SSQL = "DELETE from " + m_sNamaTabel;
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO ProfileRPJMD (Nama,TahunAwal,TahunAkhir,NamaPemda,NomorPerda,TanggalPerda, Status,Visi ) values (" +
                    " @pNama,@pTahunAwal,@pTahunAkhir,@pNamaPemda,@pNomorPerda,@pTanggalPerda, @pStatus, @pVisi )";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pNama", oProfileRPJMD.NamaProfile));
                paramCollection.Add(new DBParameter("@pTahunAwal",oProfileRPJMD.TahunAwal));
                paramCollection.Add(new DBParameter("@pTahunAkhir",oProfileRPJMD.TahunAkhir));
                paramCollection.Add(new DBParameter("@pNamaPemda",oProfileRPJMD.NamaPemda));
                paramCollection.Add(new DBParameter("@pNomorPerda",oProfileRPJMD.NomorPerda));
                paramCollection.Add(new DBParameter("@pTanggalPerda",oProfileRPJMD.TanggalPerda));
                paramCollection.Add(new DBParameter("@pStatus", oProfileRPJMD.Status));
                paramCollection.Add(new DBParameter("@pVisi", oProfileRPJMD.Visi));

                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return false;
            }            
        }

        public RPJMDProfileRPJMD Get()
        {
            RPJMDProfileRPJMD oProfileRPJMD = new RPJMDProfileRPJMD();
            try
            {
                //if (CekTable() == false)
                //{
                //    return null;
                //}
                                
                SSQL = "SELECT * from " + m_sNamaTabel;
                _dbHelper.ExecuteNonQuery(SSQL);

                

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    DataRow dr = null;
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        oProfileRPJMD = new RPJMDProfileRPJMD()
                         {
                             NamaProfile = DataFormat.GetString(dr["Nama"]),
                             //Periode = DataFormat.GetInteger(dr["Periode"]),
                             TahunAwal = DataFormat.GetInteger(dr["TahunAwal"]),
                             TahunAkhir  = DataFormat.GetInteger(dr["TahunAkhir"]),
                             NamaPemda = DataFormat.GetString(dr["NamaPemda"]),
                             NomorPerda = DataFormat.GetString(dr["NomorPerda"]),
                             TanggalPerda = DataFormat.GetDateTime(dr["TanggalPerda"]),
                            Status  = DataFormat.GetInteger(dr["Status"]),
                             Visi = DataFormat.GetString(dr["Visi"]),
                          //  Keterangan = DataFormat.GetString(dr["Keterangan"])
                       };
                    }
                    
                }
                return oProfileRPJMD;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return null;

            }


        }
        private bool CekTable()
        {

            try
            {
                SSQL = "if OBJECT_ID('ProfileRPJMD') IS NULL  CREATE TABLE ProfileRPJMD (Nama char(100),TahunAwal int,TahunAkhir int ,NamaPemda char(100),NomorPerda varchar(100),TanggalPerda DateTime, Status int ,Visi varchar(1000))";
                _dbHelper.ExecuteNonQuery(SSQL);
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }

        }
              
    }
}
