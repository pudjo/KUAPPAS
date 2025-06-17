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
    public class DesaLogic:BP 
    {
        public DesaLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mDesa";
        }
        public List<Desa> Get()
        {
            List<Desa> _lst = new List<Desa>();
            try
            {
                SSQL = "SELECT mDesa.*, mKecamatan.Nama as NamaKecamatan FROM mDesa INNER JOIN mKecamatan on mDesa.Kecamatan = mKecamatan.ID ORDER BY mDesa.ID";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new Desa()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Kecamatan= DataFormat.GetInteger(dr["Kecamatan"]),
                                    Nama= DataFormat.GetString(dr["Nama"]),                                    
                                    Kode =DataFormat.GetInteger(dr["Kode"]),
                                    Tampilan = DataFormat.GetString(dr["Nama"]),
                                    TampilanLengkap = DataFormat.GetString(dr["Nama"]),
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
        public Desa GetByID(int pID)
        {
            Desa _object = new Desa();
            try
            {
                    SSQL = "SELECT * FROM mDesa Where ID =" + pID.ToString();
                

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    //DataRow dr = null;

                    
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        _object = new Desa()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Kecamatan = DataFormat.GetInteger(dr["Kecamatan"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Kode = DataFormat.GetInteger(dr["Kode"]),
                                    Tampilan = DataFormat.GetString(dr["Nama"]),
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
        public List<Desa> GetByKecamatan(int _idKEcamatan)
        {
            List<Desa> _lst = new List<Desa>();
            try
            {
                SSQL = "SELECT mDesa.*, mKecamatan.Nama as NamaKecamatan FROM mDesa INNER JOIN mKecamatan on mDesa.Kecamatan = mKecamatan.ID WHERE mDesa.Kecamatan =" + _idKEcamatan.ToString() + " ORDER BY mDesa.ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Desa()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Kecamatan = DataFormat.GetInteger(dr["Kecamatan"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Kode = DataFormat.GetInteger(dr["Kode"]),
                                    Tampilan = DataFormat.GetString(dr["Nama"]),
                                    TampilanLengkap = DataFormat.GetString(dr["Nama"]),
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
        public bool Simpan(ref Desa _pDesa)
        {
                

            try
            {
                int _newID;
                if (_pDesa.ID== 0)
                {
                    //'string sID = DataFormat.IntToStringWithLeftPad(_pDesa.Kecamatan, 2) + DataFormat.IntToStringWithLeftPad(_pDesa.Kode, 2);
                    _newID = GetMaxID()+1;
                    //_pDesa.ID = _newID;

                    SSQL = "INSERT INTO mDesa(ID, Kecamatan, Kode,Nama) values (" +
                        "@pID, @pKecamatan, @pKode,@pNama)";

                }
                else
                {
                    _newID= _pDesa.ID;
                    SSQL = "UPDATE mDesa SET Nama= @pNama, Kode=@pKode,Kecamatan=@pKecamatan WHERE ID=@pID";

                }

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _newID));
                paramCollection.Add(new DBParameter("@pKEcamatan", _pDesa.Kecamatan));
                paramCollection.Add(new DBParameter("@pKode", _pDesa.Kode));
                paramCollection.Add(new DBParameter("@pNama", _pDesa.Nama));                

                _dbHelper.ExecuteNonQuery(SSQL,paramCollection);
                
                    return true;
                

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool SimpanImport(List<Desa> _lst)
        {


            try
            {
                if ((Hapus(0)) == true)
                {
                    foreach (Desa d in _lst)
                    {

                        SSQL = "INSERT INTO mDesa(ID, Kecamatan, Kode,Nama) values (" +
                                "@pID, @pKecamatan, @pKode,@pNama)";


                        DBParameterCollection paramCollection = new DBParameterCollection();

                        paramCollection.Add(new DBParameter("@pID", d.ID));
                        paramCollection.Add(new DBParameter("@pKEcamatan", d.Kecamatan));
                        paramCollection.Add(new DBParameter("@pKode", d.ID));
                        paramCollection.Add(new DBParameter("@pNama", d.Nama));

                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    }
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
        public List<Dusun> GetDusun(int _pDesa)
        {
            DusunLogic oLogic = new DusunLogic(Tahun);
            List<Dusun> _lst = new List<Dusun>();            
            _lst = oLogic.GetByDesa(_pDesa);
            return null;

        }
        public bool Hapus(int _pIDDesa)
        {
            try
            {
                if (_pIDDesa > 0)
                    SSQL = "DELETE FROM mDesa WHERE ID=@pID";
                else
                    SSQL = "DELETE FROM mDesa";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDDesa));
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
        private int GetMaxID()
        {
            int _maxID = 0;
            try
            {
                SSQL = "SELECT max(ID) as MAXID FROM mDesa ";


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
