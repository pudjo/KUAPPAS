using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DataAccess;
using System.Data;
using Formatting;
using BP;

namespace BP
{
    public class TahapRBALogic:BP 
    {
        public TahapRBALogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "TahapanRBA";

        }
        public List<TahapRBA> Get(Single pTahun)
        {
            List<TahapRBA> _lst = new List<TahapRBA>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel +" WHERE TAhun =" + pTahun.ToString() + "  ORDER BY Tahap";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TahapRBA()
                                {
 
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
        public TahapRBA GetByID(int pID)
        {
            TahapRBA _object = new TahapRBA();
            try
            {
                    SSQL = "SELECT * FROM TahapRBA Where ID =" + pID.ToString();
                

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    //DataRow dr = null;

                    
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        _object = new TahapRBA()
                                {
                                //    ID = DataFormat.GetInteger(dr["btKodekategori"]),                                    
                                //    Nama = DataFormat.GetString(dr["sNama"])                                   
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

        public bool Simpan(TahapRBA tr)
        {
            // Data harus lengkap
            try
            {

                if (tr.ID == 0)
                {

                    SSQL = "INSERT into TahapRBA (Unit,Tahun,Tahap,AmbangBatas,Nama,Status ) values ( " +
                        " @pUnit,@pTahun,@pTahap,@pAmbangBatas,@pNama,@pStatus) ";


                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pUnit",tr.Unit));
                    paramCollection.Add(new DBParameter("@pTahun",tr.Tahun));
                    paramCollection.Add(new DBParameter("@pTahap",tr.Tahap));
                    paramCollection.Add(new DBParameter("@pAmbangBatas",tr.AmbangBatas));
                    paramCollection.Add(new DBParameter("@pNama",tr.Nama));
                    paramCollection.Add(new DBParameter("@pStatus", tr.Status));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                }
                else
                {

                    SSQL = "INSERT into TahapRBA (Unit,Tahun,Tahap,AmbangBatas,Nama,Status ) values ( " +
                        " @pUnit,@pTahun,@pTahap,@pAmbangBatas,@pNama,@pStatus) ";


                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pUnit", tr.Unit));
                    paramCollection.Add(new DBParameter("@pTahun", tr.Tahun));
                    paramCollection.Add(new DBParameter("@pTahap", tr.Tahap));
                    paramCollection.Add(new DBParameter("@pAmbangBatas", tr.AmbangBatas));
                    paramCollection.Add(new DBParameter("@pNama", tr.Nama));
                    paramCollection.Add(new DBParameter("@pStatus", tr.Status));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                }





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