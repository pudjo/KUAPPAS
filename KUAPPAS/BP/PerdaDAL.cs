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
    public class PerdaDAL:BP
    {
        public PerdaDAL(int _pTahun, int p = 0 )
            : base(_pTahun, p)
        {
            //Tahun = _pTahun;
            m_sNamaTabel = "mPerda";
            CekTabel();
        }
        public bool Simpan(Perda oPerda)
        {
            try
            {
                
                SSQL = "DELETE from " + m_sNamaTabel + " WHERE Tahun =" +oPerda.Tahun.ToString() + " and jenis="+ oPerda.Jenis.ToString()+ " and tahap="+ oPerda.Tahap.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO mPerda (Tahun,Nomor, Tahap,Keterangan, Tanggal, Jenis) values ( " +
                        "@pTahun,@pNomor, @pTahap,@pKeterangan, @pTanggal, @pJenis)";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pTahun",oPerda.Tahun));
                paramCollection.Add(new DBParameter("@pNomor",oPerda.Nomor)); 
                paramCollection.Add(new DBParameter("@pTahap",oPerda.Tahap));
                paramCollection.Add(new DBParameter("@pKeterangan",oPerda.Keterangan)); 
                paramCollection.Add(new DBParameter("@pTanggal", oPerda.Tanggal));
                paramCollection.Add(new DBParameter("@pJenis", oPerda.Jenis));
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

        public Perda Get(int tahun, int jenis, int tahap)
        {
            Perda oretPerda = new Perda();
            try
            {


                SSQL = "SELECT * from " + m_sNamaTabel + " WHERE Tahun =" + tahun.ToString() + " and jenis=" + jenis.ToString() + " and tahap=" + tahap.ToString();

                _dbHelper.ExecuteNonQuery(SSQL);
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    DataRow dr = null;
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        oretPerda = new Perda()
                        {
                            Nomor = DataFormat.GetString(dr["Nomor"]),
                            Keterangan = DataFormat.GetString(dr["Keterangan"]),
                            Tahun = DataFormat.GetInteger(dr["Tahun"]),
                            Tahap = DataFormat.GetInteger(dr["Tahap"]),
                            Jenis = DataFormat.GetInteger(dr["Jenis"]),
                            Tanggal = DataFormat.GetDateTime(dr["Tanggal"])

                        };
                    }
                    else
                    {
                        oretPerda = new Perda()
                        {
                            Nomor = "",
                            Keterangan = "",
                            Tahun = Tahun,
                            Tahap = 1,
                            Jenis = 1,
                            Tanggal = new DateTime(2000, 1, 1)

                        };
                    }
                }
                return oretPerda;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return null;
            }
        }

        public List<Perda> Get()
        {
            List<Perda> _lst =new List<Perda>();
            try
            {

                
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
                        _lst.Add(new Perda()
                         {
                             Nomor= DataFormat.GetString(dr["Nomor"]),
                             Keterangan = DataFormat.GetString(dr["Keterangan"]),
                             Tahun = DataFormat.GetInteger(dr["Tahun"]),
                             Tahap= DataFormat.GetInteger(dr["Tahap"]),
                             Jenis = DataFormat.GetInteger(dr["Jenis"]),
                             Tanggal = DataFormat.GetDateTime(dr["Tanggal"])                            

                         });
                    }
                    
                }
                return _lst;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return null;

            }


        }
        public Perda Get(Perda oPerda)
        {
            Perda oretPerda = new Perda();
            try
            {


                SSQL = "SELECT * from " + m_sNamaTabel + " WHERE Tahun =" +oPerda.Tahun.ToString() + " and jenis="+ oPerda.Jenis.ToString()+ " and tahap="+ oPerda.Tahap.ToString();

                _dbHelper.ExecuteNonQuery(SSQL);
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    DataRow dr = null;
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        oretPerda = new Perda()
                        {
                            Nomor = DataFormat.GetString(dr["Nomor"]),
                            Keterangan = DataFormat.GetString(dr["Keterangan"]),
                            Tahun = DataFormat.GetInteger(dr["Tahun"]),
                            Tahap = DataFormat.GetInteger(dr["Tahap"]),
                            Jenis = DataFormat.GetInteger(dr["Jenis"]),
                            Tanggal = DataFormat.GetDateTime(dr["Tanggal"])

                        };
                    }
                    else
                    {
                        oretPerda = new Perda()
                        {
                            Nomor = "",
                            Keterangan = "",
                            Tahun = Tahun,
                            Tahap = 1,
                            Jenis = 1,
                            Tanggal = new DateTime(2000,1,1)

                        };
                    }
            }
            return oretPerda;
         }
         catch (Exception ex){
                _lastError = ex.Message;
                _isError = true;
                return null;
         }
        }

         
        private void CekTabel()
        {
            try
            {
                SSQL = "SELECT Nomor from  " + m_sNamaTabel;
                _dbHelper.ExecuteDataTable(SSQL);
            }
            catch (Exception ex)
            {
                SSQL = "CREATE TABLE [dbo].[mPerda]([Tahun] [smallint] NOT NULL,	[Tahap] [smallint] NULL,	[Nomor] [varchar](50) NOT NULL,	[Keterangan] [varchar(150)] NOT NULL,	[Tanggal] [smalldatetime] NOT NULL,	[Jenis] [smallint] NULL) ON [PRIMARY]";
                _dbHelper.ExecuteDataTable(SSQL);
                _lastError = ex.Message;

                

            }
        }
    }
}
