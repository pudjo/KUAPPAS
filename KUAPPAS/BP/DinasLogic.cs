using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using Formatting;
using BP;
using System.Data;
using DataAccess;

namespace BP
{
    public class DinasLogic:BP
    {
        public DinasLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mDinas";
        }
        public List<Dinas> Get()
        {

            List<Dinas> _lst = new List<Dinas>();
            try
            {
                SSQL = "select ID,sNamaSKPD as NAma,ID as SKPD, btKodekategori, btKodeUrusan, btKodeSKPD,0 as btKodeUK from mSKPD UNION " +
                    "  select ID,sNamaUK as Nama,SKPD ,btKodekategori, btKodeUrusan, btKodeSKPD,btKodeUK FROM mUnitKerja order by ID";

                
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new Dinas()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    SKPD = DataFormat.GetInteger(dr["SKPD"]),
                                    KodeKategori= DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan= DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    Nama= DataFormat.GetString(dr["Nama"]),
                                    Tampilan = DataFormat.GetInteger(dr["ID"]).ToKodeDinas()

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

        public Dinas GetByID(int idDesa, int pID)
        {
            Dinas _object = new Dinas();
            try
            {
                
               SSQL = "SELECT * FROM mSKPD Where ID =" + pID.ToString();
               

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    DataRow dr = null;
                    
                    
                    if (dt.Rows.Count > 0)
                    {
                        dr= dt.Rows[0];

                        _object = new Dinas()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),                                    
                                    Nama = DataFormat.GetString(dr["sNamaSKPD"])
                                    
                
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
       public List<Dinas>GetByDesa(int idDesa)     
         
        {
            List<Dinas> _lst = new List<Dinas>();
            try
            {
                SSQL = "SELECT * FROM mDinas Where Desa=" + idDesa.ToString() +" ORDER BY ID";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new Dinas()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    SKPD = DataFormat.GetInteger(dr["SKPD"]),
                                    Nama = DataFormat.GetString(dr["Nama"]) 
                
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
        public bool Simpan(ref Dinas _pDinas)
        {
            return true;
            //try
            //{
            //    int _newID;
            //    if (_pDinas.ID== 0)
            //    {
                    
            //        _newID = Convert.ToInt32(DataFormat.IntToStringWithLeftPad(_pDinas.Desa, 2) + DataFormat.IntToStringWithLeftPad(_pDinas.Kode, 2)) ;
                    
            //        SSQL = "INSERT INTO mDinas(ID, Desa, Kode,Nama) values (" +
            //            "@pID, @pDesa, @pKode,@pNama)";

            //    }
            //    else
            //    {
            //        _newID= _pDinas.ID;
            //        SSQL = "UPDATE mDinas SET Nama= @pNama, Kode=@pKode WHERE ID=@pID";

            //    }

            //    DBParameterCollection paramCollection = new DBParameterCollection();
            //    paramCollection.Add(new DBParameter("@pID", _newID));
            //    paramCollection.Add(new DBParameter("@pDesa", _pDinas.Desa));
            //    paramCollection.Add(new DBParameter("@pKode", _pDinas.Kode));
            //    paramCollection.Add(new DBParameter("@pNama", _pDinas.Nama));

            //    if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    _isError = true;
            //    _lastError = ex.Message + " " + SSQL;
            //    return false;

            //}

        }
        public List<Dinas> GetDinas(int _pDinas)
        {
            return null;

        }
        public bool Hapus(int _pIDDinas)
        {
            return true;
            //try
            //{
                
            //    SSQL = "DELETE FROM mDinas WHERE ID=@pID";                
            //    DBParameterCollection paramCollection = new DBParameterCollection();
            //    paramCollection.Add(new DBParameter("@pID", _pIDDinas));
            //    if (_dbHelper.ExecuteNonQuery(SSQL) > 0)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _isError = true;
            //    _lastError = ex.Message + " " + SSQL;
            //    return false;

            //}

        }
    }
}
