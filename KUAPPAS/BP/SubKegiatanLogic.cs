using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP;
using DTO;
using System.Data;
using DataAccess;
using Formatting;

namespace BP
{
    public class SubKegiatanLogic:BP 
    {
        
        public SubKegiatanLogic(int _pTahun, int profile)
            : base(_pTahun, 0,profile)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "SubKegiatan";
        }
        public List<SubKegiatan> Get()
        {

            List<SubKegiatan> _lst = new List<SubKegiatan>();
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
                                select new SubKegiatan()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    KategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    UrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    Program = DataFormat.GetInteger(dr["btIDprogram"]),
                                    Kegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                                    Kode = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                                    Nama = DataFormat.GetString(dr["NamaK"])
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

        

        public List<SubKegiatan> GetByUrusan(int _pUrusan)
        {

            List<SubKegiatan> _lst = new List<SubKegiatan>();
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
                                select new SubKegiatan()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    KategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    UrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    Program = DataFormat.GetInteger(dr["btIDprogram"]),
                                    Kegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                                    Kode = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                                    Nama = DataFormat.GetString(dr["NamaK"])
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
        public List<SubKegiatan> GetByProgramKegiatan(int _pProgram, long idKegiatan)
        {

            List<SubKegiatan> _lst = new List<SubKegiatan>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE IDProgram=" + _pProgram.ToString() + " AND IDKegiatan= " + idKegiatan.ToString() + "  ORDER BY ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SubKegiatan()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    KategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    UrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    Program = DataFormat.GetInteger(dr["btIDprogram"]),
                                    Kegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                                    Kode = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Tampilan = DataFormat.GetInteger(dr["ID"]).ToString().Insert(1, ".").Insert(3, ".").Insert(5, ".").Insert(8, ".")


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
        public SubKegiatan GetByID(int _pID)
        {

            SubKegiatan _o = new SubKegiatan();
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

                        _o = new SubKegiatan()
                        {
                            ID = DataFormat.GetInteger(dr["ID"]),
                            IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                            IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                            IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                            KategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                            UrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                            Program = DataFormat.GetInteger(dr["btIDprogram"]),
                            Kegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                            Kode = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                            Nama = DataFormat.GetString(dr["NamaK"])
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

                SSQL = "DELETE FROM SubKegiatan WHERE idProgram=" + _idProgram.ToString();
                DBParameterCollection paramCollection = new DBParameterCollection();
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
        public bool SimpanImportBapeda(ref SubKegiatan _pKegiatan)
        {
            try
            {
                int _newID;

                _newID = _pKegiatan.ID;



                SSQL = "INSERT INTO SubKegiatan(ID, IDUrusan,IDProgram,IDKegiatan,btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram,btIDkegiatan,btIDSubkegiatan,Nama) values (" +
                    "@pID, @pIDUrusan,@pIDProgram,@pIDKegiatan, @pbtKodeKategoriPelaksana, @pbtKodeUrusanPelaksana, @pbtIDProgram,@pbtIDKegiatan,@pbtIDSubkegiatan, @psNama)";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _newID));
                paramCollection.Add(new DBParameter("@pIDUrusan", _pKegiatan.IDUrusan));
                paramCollection.Add(new DBParameter("@pIDProgram", _pKegiatan.IDProgram));
                paramCollection.Add(new DBParameter("@pIDKegiatan", _pKegiatan.IDKegiatan));


                paramCollection.Add(new DBParameter("@pbtKodeKategoriPelaksana", _pKegiatan.KategoriPelaksana));
                paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _pKegiatan.UrusanPelaksana));
                paramCollection.Add(new DBParameter("@pbtIDProgram", _pKegiatan.Program));
                paramCollection.Add(new DBParameter("@pbtIDKegiatan", _pKegiatan.Kegiatan));
                paramCollection.Add(new DBParameter("@pbtIDSubkegiatan", _pKegiatan.Kode));
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
        private int CekAda(SubKegiatan _pKegiatan)
        {
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + "  WHERE ID=" + _pKegiatan.ID.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        return dt.Rows.Count;

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
        public bool Simpan(ref SubKegiatan _pKegiatan)
        {
            try
            {
                long _newID;
                if (CekAda(_pKegiatan) == 0)
                {

                    _newID = Convert.ToInt64(
                            DataFormat.IntToStringWithLeftPad((int)_pKegiatan.IDKegiatan, 8) +
                            DataFormat.IntToStringWithLeftPad((int)_pKegiatan.Kode, 3));//m_ProfileProgKegiatan.KodeKegiatan )) ;



                    SSQL = "INSERT INTO SubKegiatan(ID, IDUrusan,IDProgram,IDKegiatan,btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram,btIDkegiatan,btIDSubkegiatan,Nama) values (" +
                        "@pID, @pIDUrusan,@pIDProgram,@pIDKegiatan, @pbtKodeKategoriPelaksana, @pbtKodeUrusanPelaksana, @pbtIDProgram,@pbtIDKegiatan,@pbtIDSubkegiatan, @psNama)";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pID", _newID));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pKegiatan.IDUrusan));
                    paramCollection.Add(new DBParameter("@pIDProgram", _pKegiatan.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDKegiatan", _pKegiatan.IDKegiatan));


                    paramCollection.Add(new DBParameter("@pbtKodeKategoriPelaksana", _pKegiatan.KategoriPelaksana));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _pKegiatan.UrusanPelaksana));
                    paramCollection.Add(new DBParameter("@pbtIDProgram", _pKegiatan.Program));
                    paramCollection.Add(new DBParameter("@pbtIDKegiatan", _pKegiatan.Kegiatan));
                    paramCollection.Add(new DBParameter("@pbtIDSubkegiatan", _pKegiatan.Kode));
                    paramCollection.Add(new DBParameter("@psNama", _pKegiatan.Nama));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);



                }
                else
                {
                    _newID = _pKegiatan.ID;
                    SSQL = "UPDATE mKegiatan SET sNamaKegiatan = '" + _pKegiatan.Nama + "' WHERE ID=" + _pKegiatan.ID.ToString();
                    _dbHelper.ExecuteNonQuery(SSQL);

                    SSQL = "UPDATE tKegiatan_A SET sNama ='" + _pKegiatan.Nama + "' WHERE iTahun =" + Tahun.ToString() + " AND IDProgram=" + _pKegiatan.IDProgram.ToString() + " AND IDUrusan =" + _pKegiatan.IDUrusan.ToString() + " AND IDKegiatan = " + _pKegiatan.ID.ToString();
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
        public bool SimpanIndikator(TSubKegiatan _pKegiatan)
        {
            try
            {

                SSQL = "UPDATE TSubKegiatan SET Outcome  = '" + _pKegiatan.Outcome + "', Keluaran='" + _pKegiatan.Keluaran + "', Target=" + _pKegiatan.Target.ToString() + ", SatuanTarget='" + _pKegiatan.SatuanTarget+ "'  WHERE IDSubKegiatan=" + _pKegiatan.IDSubKegiatan.ToString() + " and IDDInas = " + _pKegiatan.IDDinas.ToString();
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
        public bool KunciInputan(TSubKegiatan _pKegiatan)
        {
            try
            {

                SSQL = "UPDATE TSubKegiatan SET status= 1 WHERE IDSubKegiatan=" + _pKegiatan.IDSubKegiatan.ToString() + " and IDDInas = " + _pKegiatan.IDDinas.ToString();
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
        public bool BukaKunciInputan(TSubKegiatan _pKegiatan)
        {
            try
            {

                SSQL = "UPDATE TSubKegiatan SET status= 0 WHERE IDSubKegiatan=" + _pKegiatan.IDSubKegiatan.ToString() + " and IDDInas = " + _pKegiatan.IDDinas.ToString();
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
        public bool SimpanDataUmum(TSubKegiatan _pKegiatan)
        {
            try
            {

                SSQL = "UPDATE TSubKegiatan SET Mulai  = '" + _pKegiatan.Mulai + "', Akhir='" + _pKegiatan.Akhir + "'   WHERE IDSubKegiatan=" + _pKegiatan.IDSubKegiatan.ToString() + " and IDDInas = " + _pKegiatan.IDDinas.ToString();
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

        public bool Simpanimport(ref SubKegiatan _pKegiatan)
        {
            try
            {


                SSQL = "INSERT INTO SubKegiatan(ID, IDUrusan,IDProgram,IDKegiatan,btKodeKategoriPelaksana, btKodeUrusanPelaksana, btIDProgram,btIDkegiatan,btIDSubkegiatan,Nama) values (" +
                    "@pID, @pIDUrusan,@pIDProgram,@pIDKegiatan, @pbtKodeKategoriPelaksana, @pbtKodeUrusanPelaksana, @pbtIDProgram,@pbtIDKegiatan,@pbtIDSubkegiatan, @psNama)";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pKegiatan.ID));
                paramCollection.Add(new DBParameter("@pIDUrusan", _pKegiatan.IDUrusan));
                paramCollection.Add(new DBParameter("@pIDProgram", _pKegiatan.IDProgram));
                paramCollection.Add(new DBParameter("@pIDKegiatan", _pKegiatan.IDKegiatan));


                paramCollection.Add(new DBParameter("@pbtKodeKategoriPelaksana", _pKegiatan.KategoriPelaksana));
                paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _pKegiatan.UrusanPelaksana));
                paramCollection.Add(new DBParameter("@pbtIDProgram", _pKegiatan.Program));
                paramCollection.Add(new DBParameter("@pbtIDKegiatan", _pKegiatan.Kegiatan));
                paramCollection.Add(new DBParameter("@pbtIDSubkegiatan", _pKegiatan.Kode));
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
        public List<SKPD> CheckKUA(int idURusan, int idKegiatan, int _iTahun)
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
