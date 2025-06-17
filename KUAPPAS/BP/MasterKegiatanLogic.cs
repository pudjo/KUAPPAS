using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using Formatting;
using BP;
using DataAccess;
using System.Data;


namespace BP
{
    public class MasterKegiatanLogic : BP
    {
        public MasterKegiatanLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mKegiatan";
        }
        public List<MasterKegiatan> Get()
        {

            List<MasterKegiatan> _lst = new List<MasterKegiatan>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " ORDER BY ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new MasterKegiatan()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    KategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    UrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    Program = DataFormat.GetInteger(dr["btIDprogram"]),
                                    Kode = DataFormat.GetInteger(dr["btIDKegiatan"]),
                                    Nama = DataFormat.GetString(dr["sNamaKEgiatan"])                                   
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
        public List<MasterKegiatan> GetByUrusan(int _pUrusan)
        {

            List<MasterKegiatan> _lst = new List<MasterKegiatan>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE IDurusan=" + _pUrusan.ToString() + " ORDER BY ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new MasterKegiatan()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),                                    
                                    KategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    UrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    Program = DataFormat.GetInteger(dr["btIDprogram"]),
                                    Kode = DataFormat.GetInteger(dr["btIDKegiatan"]),
                                    Nama = DataFormat.GetString(dr["sNamaKEgiatan"]) 
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
        public List<MasterKegiatan> GetByProgram(int _pProgram)
        {

            List<MasterKegiatan> _lst = new List<MasterKegiatan>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE IDProgram=" + _pProgram.ToString() + " ORDER BY ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new MasterKegiatan()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    KategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    UrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    Program = DataFormat.GetInteger(dr["btIDprogram"]),
                                    Kode = DataFormat.GetInteger(dr["btIDKegiatan"]),
                                    Nama = DataFormat.GetString(dr["sNamaKEgiatan"])
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
        public MasterKegiatan GetByID(int _pID)
        {

            MasterKegiatan _o = new MasterKegiatan();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + "  WHERE ID=" + _pID.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        _o = new MasterKegiatan()
                        {
                            ID = DataFormat.GetInteger(dr["ID"]),
                            IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                            IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),                            
                            KategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                            UrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                            Program = DataFormat.GetInteger(dr["btIDprogram"]),
                            Kode = DataFormat.GetInteger(dr["btIDKegiatan"]),
                            Nama = DataFormat.GetString(dr["sNamaKEgiatan"]) 
                        };
                    }
                }
                return _o;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _o;
            }
        }
        public bool HapusSemuaPerProgram(int _idProgram)
        {
            try
            {

                SSQL = "DELETE FROM mKegiatan WHERE idProgram=" + _idProgram.ToString();
                DBParameterCollection paramCollection = new DBParameterCollection();                
                _dbHelper.ExecuteNonQuery(SSQL) ;
               
                    return true;
               
                
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }
        }
        public bool SimpanImportBapeda(ref MasterKegiatan _pKegiatan)
        {
            try
            {
                int _newID;

                _newID = _pKegiatan.ID;



                SSQL = "INSERT INTO mKegiatan(ID, IDUrusan,IDProgram,btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram,btIDkegiatan,sNamaKegiatan) values (" +
                    "@pID, @pIDUrusan,@pIDProgram, @pbtKodeKategoriPelaksana, @pbtKodeUrusanPelaksana, @pbtIDProgram,@pbtIDKegiatan, @psNama)";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _newID));
                paramCollection.Add(new DBParameter("@pIDProgram", _pKegiatan.IDProgram));
                paramCollection.Add(new DBParameter("@pIDUrusan", _pKegiatan.IDUrusan));
                paramCollection.Add(new DBParameter("@pbtKodeKategoriPelaksana", _pKegiatan.KategoriPelaksana));
                paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _pKegiatan.UrusanPelaksana));
                paramCollection.Add(new DBParameter("@pbtIDProgram", _pKegiatan.Program));
                paramCollection.Add(new DBParameter("@pbtIDKegiatan", _pKegiatan.Kode));
                paramCollection.Add(new DBParameter("@psNama", _pKegiatan.Nama));
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
        private int CekAda(MasterKegiatan _pKegiatan)
        {
            try{
                SSQL = "SELECT * FROM " + m_sNamaTabel + "  WHERE ID=" + _pKegiatan.ID.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        return dt.Rows.Count ;

                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return 0;
            }
        }
        public bool Simpan(ref MasterKegiatan _pKegiatan)
        {
            try
            {
                int _newID;
                if (CekAda(_pKegiatan) == 0)
                {
                    
                    _newID = Convert.ToInt32(                            
                            DataFormat.IntToStringWithLeftPad((int)_pKegiatan.IDProgram, 5) +
                            DataFormat.IntToStringWithLeftPad((int)_pKegiatan.Kode, m_ProfileProgKegiatan.KodeKegiatan )) ;

                    
                    SSQL = "INSERT INTO mKegiatan(ID, IDUrusan,IDProgram,btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram,btIDkegiatan,sNamaKegiatan) values (" +
                        "@pID, @pIDUrusan,@pIDProgram, @pbtKodeKategoriPelaksana, @pbtKodeUrusanPelaksana, @pbtIDProgram,@pbtIDKegiatan, @psNama)";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pID", _newID));
                    paramCollection.Add(new DBParameter("@pIDProgram", _pKegiatan.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pKegiatan.IDUrusan));                
                    paramCollection.Add(new DBParameter("@pbtKodeKategoriPelaksana", _pKegiatan.KategoriPelaksana));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _pKegiatan.UrusanPelaksana));
                    paramCollection.Add(new DBParameter("@pbtIDProgram", _pKegiatan.Program));
                    paramCollection.Add(new DBParameter("@pbtIDKegiatan", _pKegiatan.Kode));
                    paramCollection.Add(new DBParameter("@psNama", _pKegiatan.Nama));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    _pKegiatan.ID = _newID;
                

                }
                else
                {
                    _newID = _pKegiatan.ID;
                    SSQL = "UPDATE mKegiatan SET sNamaKegiatan = '" + _pKegiatan.Nama +"' WHERE ID=" + _pKegiatan.ID.ToString();
                    _dbHelper.ExecuteNonQuery(SSQL);

                    SSQL = "UPDATE tKegiatan_A SET sNama ='" + _pKegiatan.Nama + "' WHERE iTahun =" + Tahun.ToString () + " AND IDProgram=" + _pKegiatan.IDProgram.ToString() + " AND IDUrusan =" + _pKegiatan.IDUrusan.ToString() + " AND IDKegiatan = " + _pKegiatan.ID.ToString();
                    _dbHelper.ExecuteNonQuery(SSQL);


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
        public List<SKPD> CheckKUA(int idURusan, int idKegiatan,int _iTahun)
        {
            List<SKPD> _lst = new List<SKPD>();
            try
            {
                SSQL = "SELECT * FROM mSKPD WHERE ID in (SELECT IDDinas FROM tKUA WHERE iTahun =" + _iTahun.ToString() + " AND IDUrusan =" + idURusan.ToString() + " AND idKegiatan=" + idKegiatan.ToString() + ")";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SKPD()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeURusan"]),
                                    Kode = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    Nama = DataFormat.GetString(dr["sNamaSKPD"]),
                                    Tampilan = DataFormat.GetInteger(dr["btKodeKategori"]).ToString("0") + "." + DataFormat.GetInteger(dr["btKodeUrusan"]).ToString("00") + "." + DataFormat.GetInteger(dr["btKodeSKPD"]).ToString("00")
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
        public bool Hapus(int _pIDKegiatan)
        {
            try
            {

                SSQL = "DELETE FROM mKegiatan WHERE ID=@pID";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDKegiatan));
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
