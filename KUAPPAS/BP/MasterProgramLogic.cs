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
    public class MasterProgramLogic:BP
    {
        public MasterProgramLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mProgram";
        }
        public List<MasterProgram> Get()
        {

            List<MasterProgram> _lst = new List<MasterProgram>();
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
                                select new MasterProgram()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    KategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    UrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    Kode = DataFormat.GetInteger(dr["btIDprogram"]),
                                    Nama = DataFormat.GetString(dr["sNamaPRogram"])                                   
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
        public List<MasterProgram> GetByUrusan(int _pUrusan)
        {

            List<MasterProgram> _lst = new List<MasterProgram>();
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
                                select new MasterProgram()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    KategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    UrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    Kode = DataFormat.GetInteger(dr["btIDprogram"]),
                                    Nama = DataFormat.GetString(dr["sNamaPRogram"])
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
        public List<MasterProgram> GetByUrusanAndAll(int _pUrusan)
        {

            List<MasterProgram> _lst = new List<MasterProgram>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE IDurusan=" + _pUrusan.ToString() + " or IDurusan=0 ORDER BY ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new MasterProgram()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    KategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    UrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    Kode = DataFormat.GetInteger(dr["btIDprogram"]),
                                    Nama = DataFormat.GetString(dr["sNamaPRogram"])
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
        public MasterProgram GetByID( int _pID)
        {

            MasterProgram _o= new MasterProgram();
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

                        _o = new MasterProgram()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    KategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    UrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    Kode = DataFormat.GetInteger(dr["btIDprogram"]),
                                    Nama = DataFormat.GetString(dr["sNamaPRogram"])
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
         public int  CekAda( MasterProgram _pID)
        {

            MasterProgram _o= new MasterProgram();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + "  WHERE ID=" + _pID.ID.ToString() + " AND IDUrusan = " + _pID.IDUrusan.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    return dt.Rows.Count;
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
        public List<SKPD> CheckKUA(int idURusan, int idProgram, int _iTahun)
        {
            List<SKPD> _lst = new List<SKPD>();
            try
            {
                SSQL = "SELECT * FROM mSKPD WHERE ID in (SELECT IDDinas FROM tKUA WHERE iTahun =" + _iTahun.ToString() + " AND IDUrusan =" + idURusan.ToString() + " AND IDProgram=" + idProgram.ToString() + ")";

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
        public bool Simpan(ref MasterProgram _pProgram)
        {
            try
            {
                int _newID;
                if (CekAda (_pProgram) == 0)
                {

                    _newID = Convert.ToInt32(
                            DataFormat.IntToStringWithLeftPad((int)_pProgram.IDUrusan, 3) +                            
                            DataFormat.IntToStringWithLeftPad((int)_pProgram.Kode, 2));                          

                    SSQL = "INSERT INTO mProgram(ID,IDUrusan, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram,sNamaProgram) values (" +
                        "@pID, @pIDUrusan,@pbtKodeKategoriPelaksana, @pbtKodeUrusanPelaksana, @pbtIDProgram, @psNama)";
                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pID", _newID));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pProgram.IDUrusan));
                    paramCollection.Add(new DBParameter("@pbtKodeKategoriPelaksana", _pProgram.KategoriPelaksana));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _pProgram.UrusanPelaksana));
                    paramCollection.Add(new DBParameter("@pbtIDProgram", _pProgram.Kode));
                    paramCollection.Add(new DBParameter("@psNama", _pProgram.Nama));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    _pProgram.ID = _newID;

                }
                else
                {
                    SSQL = "UPDATE mProgram SET ID= " + _pProgram.ID.ToString() + ", sNamaProgram ='" + _pProgram.Nama + "' WHERE ID=" + _pProgram.IDLama.ToString();
                    _dbHelper.ExecuteNonQuery(SSQL);

                    SSQL = "UPDATE mProgram SET sNamaProgram ='" + _pProgram.Nama + "'  WHERE ID=" + _pProgram.ID.ToString();
                    _dbHelper.ExecuteNonQuery(SSQL);


                    //SSQL = "UPDATE mKegiatan  SET IDProgram = " + _pProgram.ID.ToString() + ",ID = " + _pProgram.ID.ToString() + " * 1000 + ID % 1000 WHERE IDProgram=" + _pProgram.IDLama.ToString();

                    //_dbHelper.ExecuteNonQuery(SSQL);

                   // SSQL = "UPDATE mKegiatan  SET IDProgram = " + _pProgram.ID.ToString() + ",ID = " + _pProgram.ID.ToString() + " * 1000 + ID % 1000 WHERE IDProgram=" + _pProgram.IDLama.ToString();

                    //_dbHelper.ExecuteNonQuery(SSQL);




                    //SSQL = "UPDATE tPrograms_A SET IDProgram = sNamaProgram ='" + _pProgram.Nama + "' WHERE iTahun =" + Tahun.ToString() + " AND  IDProgram=" + _pProgram.ID.ToString() + " AND IDUrusan =" + _pProgram.IDUrusan.ToString();
  //                  _dbHelper.ExecuteNonQuery(SSQL);
//

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
      
        public bool SimpanImport(ref MasterProgram _pProgram)
        {
            try
            {
                int _newID = 0;
                    _newID = _pProgram.ID;

                    SSQL = "INSERT INTO mProgram(ID,IDUrusan, btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram,sNamaProgram) values (" +
                        "@pID, @pIDUrusan,@pbtKodeKategoriPelaksana, @pbtKodeUrusanPelaksana, @pbtIDProgram, @psNama)";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pID", _newID));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pProgram.IDUrusan));
                    paramCollection.Add(new DBParameter("@pbtKodeKategoriPelaksana", _pProgram.KategoriPelaksana));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _pProgram.UrusanPelaksana));
                    paramCollection.Add(new DBParameter("@pbtIDProgram", _pProgram.Kode));
                    paramCollection.Add(new DBParameter("@psNama", _pProgram.Nama));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    _pProgram.ID = _newID;

                

                return true;


            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }

        public bool HapusSemuaPerUrusan(int _pUrusan)
        {
            try
            {
                SSQL = "DELETE FROM mProgram WHERE IDUrusan=@pID";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pUrusan));
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
        public bool Hapus(int _pIDKegiatan)
        {
            try
            {
                SSQL = "DELETE FROM mProgram WHERE ID=@pID";
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
