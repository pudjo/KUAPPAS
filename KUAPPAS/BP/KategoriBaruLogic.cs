using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DataAccess;
using System.Data;
using Formatting;

namespace BP
{
    public class KategoriBaruLogic:BP 
    {
        public KategoriBaruLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mKategori";
            CekTabel();
        }
        private void CekTabel()
        {
            try{
                SSQL="SELECT * from " + m_sNamaTabel;
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (_isError== true){
                    SSQL = "CREATE TABLE " + m_sNamaTabel + " (btKodekategori smallint, sNama varchar(100))";
                    _dbHelper.ExecuteNonQuery(SSQL);
                }
            }

            catch (Exception ex)
            {
                SSQL = "CREATE TABLE " + m_sNamaTabel + " (btKodekategori smallint, sNama varchar(100))";
                _dbHelper.ExecuteNonQuery(SSQL);

                _lastError = ex.Message;
            }
        }
        public List<KategoriBaru> Get(){
            List<KategoriBaru> _lst = new List<KategoriBaru>();
            try
            {
                SSQL = "SELECT * FROM mKategoriBaru  ORDER BY btKodekategori";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new KategoriBaru()
                                {
                                    ID = DataFormat.GetInteger(dr["btKodekategori"]),                                    
                                    Nama= DataFormat.GetString(dr["sNama"])                                    
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
        public KategoriBaru GetByID(int pID)
        {
            KategoriBaru _object = new KategoriBaru();
            try
            {
                    SSQL = "SELECT * FROM mKategori Where btKodekategori =" + pID.ToString();
                

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    //DataRow dr = null;

                    
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        _object = new KategoriBaru()
                                {
                                    ID = DataFormat.GetInteger(dr["btKodekategori"]),                                    
                                    Nama = DataFormat.GetString(dr["sNamaKategori"])                                   
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

        public bool Simpan(ref KategoriBaru _kb)
        {
            // Data harus lengkap
            try
            {
                KategoriBaru kb = GetByID(_kb.ID);
                if (kb.ID ==0 ||kb.Nama=="" || kb ==null)
                //if (GetByID(_kb.ID)==null)
                {
                     SSQL = "INSERT INTO mKategoriBaru(btKodekategori, sNama) values (" +
                       _kb.ID.ToString()+ ",'" +_kb.Nama +"')";

                }
                else
                {
                    SSQL = "UPDATE  mKategoriBaru SET sNama= '" + _kb.Nama + "' WHERE btKodekategori=" + _kb.ID.ToString();


                }                
                _dbHelper.ExecuteNonQuery(SSQL);                
                return true;
                

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        
        public bool Hapus(int _pID)
        {
            try
            {
                
                SSQL = "DELETE FROM mKategoriBAru WHERE btKodekategori=@pID";                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pID));
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                
                return true;
                
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;
            }
        }        
    }
}
