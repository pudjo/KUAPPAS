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
    public class KecamatanLogic:BP
    {
        public KecamatanLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mKecamatan";
        }
        public List<Kecamatan> Get()
        {
            List<Kecamatan> _lst = new List<Kecamatan>();
            try
            {
                SSQL = "SELECT * FROM mKecamatan ORDER BY ID";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new Kecamatan()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Kota = DataFormat.GetInteger(dr["Kota"]),
                                    Nama= DataFormat.GetString(dr["Nama"]),                                    
                                    Kode =DataFormat.GetInteger(dr["Kode"]),
                                    Tampilan = "",
                                    TampilanLengkap = DataFormat.GetString(dr["Nama"])
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
        public Kecamatan GetByID(int idKota, int pID)
        {
            Kecamatan _object = new Kecamatan();
            try
            {
                SSQL = "SELECT * FROM mKecamatan Where Kota =" + idKota.ToString() + " and ID ="+pID.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    DataRow dr = dt.NewRow();
                    
                    if (dt.Rows.Count > 0)
                    {
                        dr= dt.Rows[0];

                        _object = new Kecamatan()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Kota = DataFormat.GetInteger(dr["Kota"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Kode = DataFormat.GetInteger(dr["Kode"]),
                                    Tampilan = "",
                                    TampilanLengkap = DataFormat.GetString(dr["Nama"])
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
        public bool Simpan(ref Kecamatan _pKecamatan)
        {
            try
            {
                int _newID;
                if (_pKecamatan.ID== 0)
                {
                    _pKecamatan.Kota = 1;
            
                    _newID = GetMaxID() + 1;
                    SSQL = "INSERT INTO mKecamatan(ID, Kota, Kode,Nama) values (" +
                        "@pID, @pKota, @pKode,@pNama)";

                }
                else
                {
                    _newID= _pKecamatan.ID;
                    SSQL = "UPDATE mKecamatan SET Nama= @pNama, Kode=@pKode WHERE ID=@pID";

                }

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _newID));
                paramCollection.Add(new DBParameter("@pKota", _pKecamatan.Kota));
                paramCollection.Add(new DBParameter("@pKode", _pKecamatan.Kode));
                paramCollection.Add(new DBParameter("@pNama", _pKecamatan.Nama));                

                if (_dbHelper.ExecuteNonQuery(SSQL,paramCollection) > 0)
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
        public bool SimpanImport(List<Kecamatan> _lst )
        {


            try
            {
                if (Hapus() == false)
                {
                    return false;

                }
                foreach (Kecamatan k in _lst)
                {


                    SSQL = "INSERT INTO mKecamatan(ID, Kota, Kode,Nama) values (" +
                        "@pID, @pKota, @pKode,@pNama)";



                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pID", k.ID));
                    paramCollection.Add(new DBParameter("@pKota", 1));
                    paramCollection.Add(new DBParameter("@pKode", k.ID));
                    paramCollection.Add(new DBParameter("@pNama", k.Nama));

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
        public List<Desa> GetDesa(int _pKecamatan)
        {
            return null;

        }
        public bool Hapus(int _pIDKecamatan)
        {
            try
            {
                
                SSQL = "DELETE FROM mKecamatan WHERE ID=@pID";                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDKecamatan));
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
        public bool Hapus()
        {
            try
            {

                SSQL = "DELETE FROM mKecamatan ";
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
        private int GetMaxID()
        {
            int _maxID = 0;
            try
            {
                SSQL = "SELECT max(ID) as MAXID FROM mKEcamatan ";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    DataRow dr = null;
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        _maxID = DataFormat.GetInteger(dr["MAXID"]);


                    }
                }
                return _maxID;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return 0;
            }
        }
    }
}
