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
    public class PelaksanaUrusanLogic:BP
    {
        public PelaksanaUrusanLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mPelaksanaUrusan";
        }
        public List<PelaksanaUrusan> Get()
        {
            List<PelaksanaUrusan> _lst = new List<PelaksanaUrusan>();
            try
            {
                SSQL = "SELECT s.* FROM mPelaksanaUrusan ORDER BY ID";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new PelaksanaUrusan()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    Dinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Urusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    KodeKategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"])
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
        public List<PelaksanaUrusan> GetWithNamaDinas(int tahun)
        {
            List<PelaksanaUrusan> _lst = new List<PelaksanaUrusan>();
            try
            {
                SSQL = "SELECT m.*, mSKPD.sNamaSKPD, mSKPD.Kode FROM mPelaksanaUrusan m inner join mSKPD "+
                     " on mSKPD.ID = m.IDDInas  where m.iTahun="+ tahun.ToString() +"  ORDER BY mSKPD.ID";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PelaksanaUrusan()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    Dinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Urusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    KodeKategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    NamaDinas = DataFormat.GetString(dr["sNamaSKPD"]),
                                    KodeDinas = DataFormat.GetString(dr["Kode"]),
                                    KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"])
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
        public List<PelaksanaUrusan> GetByIDDinas(int _pDinas, RemoteConnection rCon = null)
        {
            List<PelaksanaUrusan> _lst = new List<PelaksanaUrusan>();
            try
            {
               
                DataTable dt = new DataTable();
                //dt = _dbHelper.ExecuteDataTable(SSQL);

                if (rCon == null)
                {
                    SSQL = "SELECT * FROM mPelaksanaUrusan WHERE IDDInas=" + _pDinas.ToString() + " ORDER BY IDUrusan";
                    dt = _dbHelper.ExecuteDataTable(SSQL);
                }
                else
                {
                    SSQL = "SELECT * FROM mPelaksanaUrusan WHERE IDDInas=" + _pDinas.ToString() + " ANd profile = 2 ORDER BY IDUrusan";
                    dt = _dbHelper.ExecuteDataTable(SSQL, rCon.GetConnection());
                }

                if (dt != null)

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PelaksanaUrusan()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    Dinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Urusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    //KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    //KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    //KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    //KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    //KodeKategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    //KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"])
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
        public PelaksanaUrusan GetByID(int pID)
        {
            PelaksanaUrusan olRet = new PelaksanaUrusan();
            try
            {
                SSQL = "SELECT s.* FROM mPelaksanaUrusan where ID =" + pID.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        olRet = new PelaksanaUrusan()
                        {
                            Tahun = DataFormat.GetInteger(dr["iTahun"]),
                            Dinas = DataFormat.GetInteger(dr["IDDInas"]),
                            Urusan = DataFormat.GetInteger(dr["IDUrusan"]),
                            KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                            KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                            KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                            KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                            KodeKategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                            KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"])

                        };          
                                
                    }
                }
                return olRet;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return olRet;
            }
        }
        public bool Bersihkan(int iddinas , int profile )
        {
            try
            {
                SSQL = "DELETE mPelaksanaUrusan where IDDInas =" + iddinas.ToString() + " ANd profile=" + profile.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }
        public bool Simpan(PelaksanaUrusan _pPelaksanaUrusan)
        {
                

            try
            {
                //SSQL = "DELETE FROM mPelaksanaUrusan WHERE iTAHUN=" + _pPelaksanaUrusan.Tahun.ToString() + " AND Dinas=" + _pPelaksanaUrusan.Dinas.ToString() + " AND " +
                //    " Urusan =" + _pPelaksanaUrusan.Urusan.ToString() + " AND btKodeKategori=" + _pPelaksanaUrusan.KodeKategori.ToString() + " AND btKodeUrusan=" + _pPelaksanaUrusan.KodeUrusan.ToString() +
                //    " AND btKodeSKPD=" + _pPelaksanaUrusan.KodeSKPD.ToString() + " AND btKodeUK=" + _pPelaksanaUrusan.KodeUK.ToString() +
                //    " AND btKodeKategoriPelaksana=" + _pPelaksanaUrusan.KodeKategoriPelaksana.ToString() + " AND btKOdeURusanPelaksana=" +
                //    _pPelaksanaUrusan.KodeUrusanPelaksana.ToString();
                string isPokok;
                SSQL = "DELETE FROM mPelaksanaUrusan WHERE iTAHUN=" + _pPelaksanaUrusan.Tahun.ToString() +
                    " AND IDUrusan= " + _pPelaksanaUrusan.Urusan.ToString() +  
                    " AND IDDinas=" + _pPelaksanaUrusan.Dinas.ToString();

                if (_pPelaksanaUrusan.Urusan == _pPelaksanaUrusan.Dinas / 10000)
                    isPokok = "0";
                else
                    isPokok = "1";

                if (_dbHelper.ExecuteNonQuery(SSQL) > 0)
                {

                    SSQL = "INSERT INTO mPelaksanaUrusan (iTahun,IDDinas,IDUrusan,profile, isPokok) values ( " +
                     _pPelaksanaUrusan.Tahun.ToString() + "," + _pPelaksanaUrusan.Dinas.ToString() + "," +
                     _pPelaksanaUrusan.Urusan.ToString() + ",2," + isPokok + ")";//

                    if (_dbHelper.ExecuteNonQuery(SSQL) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    // 7010000
              
                    SSQL = "INSERT INTO mPelaksanaUrusan (iTahun,IDDinas,IDUrusan,isPokok) values ( " +
                     _pPelaksanaUrusan.Tahun.ToString() + "," + _pPelaksanaUrusan.Dinas.ToString() + "," +
                     _pPelaksanaUrusan.Urusan.ToString() + "," + isPokok + ")";//
                    if (_dbHelper.ExecuteNonQuery(SSQL) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }           

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }

        public bool Hapus(PelaksanaUrusan _pPelaksanaUrusan)
        {


            try
            {

                SSQL = "DELETE FROM mPelaksanaUrusan WHERE iTAHUN=" + _pPelaksanaUrusan.Tahun.ToString() + " AND Dinas=" + _pPelaksanaUrusan.Dinas.ToString() + " AND " +
                    " Urusan =" + _pPelaksanaUrusan.Urusan.ToString() + " AND btKodeKategori=" + _pPelaksanaUrusan.KodeKategori.ToString() + " AND btKodeUrusan=" + _pPelaksanaUrusan.KodeUrusan.ToString() +
                    " AND btKodeSKPD=" + _pPelaksanaUrusan.KodeSKPD.ToString() + " AND btKodeUK=" + _pPelaksanaUrusan.KodeUK.ToString() +
                    " AND btKodeKategoriPelaksana=" + _pPelaksanaUrusan.KodeKategoriPelaksana.ToString() + " AND btKOdeURusanPelaksana=" +
                    _pPelaksanaUrusan.KodeUrusanPelaksana.ToString();
                
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

    }
}
