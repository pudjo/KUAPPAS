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
    public class DusunLogic:BP 
    {
        public DusunLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mDusun";
        }
        public List<Dusun> Get()
        {
            List<Dusun> _lst = new List<Dusun>();
            try
            {
                SSQL = "SELECT *, mDesa.Nama as NamaDesa, mKecamatan.Nama as NamaKecamatan FROM mDusun INNER JOIN mDESA on  mDUsun.Desa= mDesa.ID INNER JOIN mKecamatan on mDesa.Kecamatan= mKecamatan.ID ORDER BY mKecamatan.ID, mDesa.ID, mDusun.ID";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new Dusun()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Desa= DataFormat.GetInteger(dr["Desa"]),
                                    Nama= DataFormat.GetString(dr["Nama"]),                                    
                                    Kode =DataFormat.GetInteger(dr["Kode"]),
                                    Tampilan = "",
                                    TampilanLengkap = DataFormat.GetString(dr["Nama"]),
                                    Kecamatan = GetKEcamatan(DataFormat.GetInteger(dr["Kecamatan"])),
                                    NamaDesa= DataFormat.GetString(dr["NamaDesa"]),
                                    NamaKecamatan = DataFormat.GetString(dr["NamaKecamatan"])
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
        public List<Dusun> Get(int idKecamatan, int idDesa)
        {
            List<Dusun> _lst = new List<Dusun>();
            try
            {
                SSQL = "SELECT *, mDesa.Nama as NamaDesa, mKecamatan.Nama as NamaKecamatan FROM mDusun INNER JOIN mDESA on  mDUsun.Desa= mDesa.ID INNER JOIN mKecamatan on mDesa.Kecamatan= mKecamatan.ID where mDUsun.Desa= " + idDesa.ToString () + " AND mdusun.Kecamatan =" + idKecamatan.ToString () + "  ORDER BY mKecamatan.ID, mDesa.ID, mDusun.ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Dusun()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Desa = DataFormat.GetInteger(dr["Desa"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Kode = DataFormat.GetInteger(dr["Kode"]),
                                    Tampilan = "",
                                    TampilanLengkap = DataFormat.GetString(dr["Nama"]),
                                    Kecamatan = GetKEcamatan(DataFormat.GetInteger(dr["Kecamatan"])),
                                    NamaDesa = DataFormat.GetString(dr["NamaDesa"]),
                                    NamaKecamatan = DataFormat.GetString(dr["NamaKecamatan"])
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
        public Dusun GetByID(int idDesa, int pID)
        {
            Dusun _object = new Dusun();
            try
            {
                
                  SSQL = "SELECT * FROM mDusun Where ID =" + pID.ToString();
                

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    DataRow dr = null;
                    
                    
                    if (dt.Rows.Count > 0)
                    {
                        dr= dt.Rows[0];

                        _object = new Dusun()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Desa = DataFormat.GetInteger(dr["Desa"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Kode = DataFormat.GetInteger(dr["Kode"]),
                                    Tampilan = "",
                                    TampilanLengkap = DataFormat.GetString(dr["Nama"]),
                                    Kecamatan = GetKEcamatan(DataFormat.GetInteger(dr["Desa"]))
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
       public List<Dusun>GetByDesa(int idDesa)     
         
        {
            List<Dusun> _lst = new List<Dusun>();
            try
            {
                SSQL = "SELECT * FROM mDusun Where Desa=" + idDesa.ToString() +" ORDER BY ID";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new Dusun()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Desa= DataFormat.GetInteger(dr["Desa"]),
                                    Nama= DataFormat.GetString(dr["Nama"]),                                    
                                    Kode =DataFormat.GetInteger(dr["Kode"]),
                                    Tampilan = DataFormat.GetString(dr["Nama"]), 
                                    TampilanLengkap = DataFormat.GetString(dr["Nama"]),
                                    Kecamatan = GetKEcamatan(DataFormat.GetInteger(dr["Desa"]))
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
       private int GetKEcamatan(int pDesa)
       {
           DesaLogic oLogic = new DesaLogic(Tahun);
           Desa oDesa = new Desa();
           oDesa = oLogic.GetByID(pDesa);
           if (oDesa != null)
           {
               return oDesa.Kecamatan;
           }
           else
           {
               return 0;
           }

       }
        public bool Simpan(ref Dusun _pDusun)
        {
            try
            {
                int _newID;
                if (_pDusun.ID== 0)
                {
                    
                    _newID = Convert.ToInt32(DataFormat.IntToStringWithLeftPad(_pDusun.Desa, 2) + DataFormat.IntToStringWithLeftPad(_pDusun.Kode, 2)) ;
                    
                    SSQL = "INSERT INTO mDusun(ID, Desa, Kode,Nama) values (" +
                        "@pID, @pDesa, @pKode,@pNama)";

                }
                else
                {
                    _newID= _pDusun.ID;
                    SSQL = "UPDATE mDusun SET Nama= @pNama, Kode=@pKode WHERE ID=@pID";

                }

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _newID));
                paramCollection.Add(new DBParameter("@pDesa", _pDusun.Desa));
                paramCollection.Add(new DBParameter("@pKode", _pDusun.Kode));
                paramCollection.Add(new DBParameter("@pNama", _pDusun.Nama));

                if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool SimpanImport(List<Dusun> _lst)
        {
            try
            {
                if (Hapus() == false)
                    return false;

                foreach(Dusun d in _lst){

                    SSQL = "INSERT INTO mDusun(ID, Desa, Kode,Kecamatan,Nama) values (" +
                        "@pID, @pDesa, @pKode,@pKecamatan,@pNama)";

                

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID",d.ID));
                paramCollection.Add(new DBParameter("@pDesa", d.Desa));
                paramCollection.Add(new DBParameter("@pKode", d.ID ));
                paramCollection.Add(new DBParameter("@pKecamatan", d.Kecamatan));
                paramCollection.Add(new DBParameter("@pNama", d.Nama));

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
        public List<Dusun> GetDusun(int _pDusun)
        {
            return null;

        }
        public bool Hapus(int _pIDDusun)
        {
            try
            {
                
                SSQL = "DELETE FROM mDusun WHERE ID=@pID";                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDDusun));
                if (_dbHelper.ExecuteNonQuery(SSQL) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool Hapus()
        {
            try
            {

                SSQL = "DELETE FROM mDusun ";
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
    }
}
