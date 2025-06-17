using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP;
using DTO;
using DataAccess;
using Formatting;
using System.Data;


namespace BP
{
    public class TKegiatanLogic:BP 
    {
        public TKegiatanLogic(int _pTahun, int profile)
            : base(_pTahun, 0 , profile)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tkua";

        }

        public List<TKegiatan> GetByDinasAndUrusanAndIDProgram(int _pDinas, int _pIDUrusan,int _pIDProgram)
        {
            List<TKegiatan> _lst = new List<TKegiatan>();
            try
            {
                // ID Urusan untuk queri -> IDUrusan
                // IDUrusan untuk Nama -> IDUrusanMaster

                SSQL = "Select tKUA.iTahun, tKUA.IDDInas, tKUA.IDProgram,tKUA.IDkegiatan, tKUA.IDProgram, tKUA.btIDprogram, tKUA.btIDkegiatan, tKUA.IDUrusan, mKegiatan.sNamaKegiatan as Nama ,SUM(tKUA.JumlahOlah) as Jumlah " +
                         " FROM tKUA INNER JOIN mKegiatan ON tKUA.IDUrusanMaster= mKegiatan.IDUrusan and tKUA.IDProgramMaster= mKegiatan.IDProgram and tKUA.IDkegiatanMaster= mKegiatan.ID " +
                         " where tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pDinas.ToString() + "  and tKUA.IDUrusan= " + _pIDUrusan.ToString() + " AND tKUA.IDProgram=" + _pIDProgram.ToString() + "  AND isnull(tKUA.Status,0)<9 " +
                        " GROUP BY tKUA.iTahun, tKUA.IDDInas, tKUA.IDProgram,tKUA.IDkegiatan, tKUA.IDProgram, tKUA.btIDprogram, tKUA.btIDkegiatan, tKUA.IDUrusan, mKegiatan.sNamaKEgiatan " +
                        " ORDER BY tKUA.IDUrusan,tKUA.IDProgram, tKUA.IDkegiatan, tKUA.btIDprogram, tKUA.btIDkegiatan,  mKegiatan.sNamaKEgiatan ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TKegiatan()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Pagu = DataFormat.GetDecimal(dr["Jumlah"])
                                }).ToList();
                    }
                }
                return _lst;
            }catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        public List<TKegiatan> GetByDinasAndUrusanAndIDProgramDrRenja(int _pDinas, int _pIDUrusan, int _pIDProgram)
        {
            List<TKegiatan> _lst = new List<TKegiatan>();
            try
            {
                // ID Urusan untuk queri -> IDUrusan
                // IDUrusan untuk Nama -> IDUrusanMaster


              /*  SSQL = "select distinct mKegiatan.ID as IDKegiatan, mKegiatan.IDUrusan,renja.IDDInas, mKegiatan.IDProgram, mKegiatan.sNamaKegiatan as Nama " +
                        " from mKegiatan inner join renja on mKegiatan.ID = renja.IDKegiatan  where mKegiatan.IDUrusan =" + _pIDUrusan.ToString() +
                        " and renja.IDProgram =" + _pIDProgram.ToString() + " AND renja.IDDinas =" + _pDinas.ToString() + "  order by mKegiatan.ID ";
                */
                SSQL="select distinct musrenbang.IDKegiatan, mKegiatan.IDUrusan,musrenbang.IDDInas,  " +
                    " mKegiatan.IDProgram, mKegiatan.sNamaKegiatan as Nama  from mKegiatan inner join musrenbang on  " +
                    " ((mKegiatan.ID = musrenbang.IDKegiatan and mKegiatan.IDUrusan =" + _pIDUrusan.ToString() + ") or (musrenbang.idkegiatan %100000 = mKegiatan.ID and mKegiatan.IDurusan =0 ) ) where " +
                        " musrenbang.IDProgram =" + _pIDProgram.ToString() + " AND musrenbang.IDDinas =" + _pDinas.ToString() + "  order by musrenbang.IDKegiatan ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TKegiatan()
                                {
                                    
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    //KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    //KodeKegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                                    Nama = DataFormat.GetString(dr["Nama"])
                                  //  Pagu = DataFormat.GetDecimal(dr["Jumlah"])
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

        public List<TKegiatan> GetByDinasAndName(int _pDinas, string sSearch)
        {
            List<TKegiatan> _lst = new List<TKegiatan>();
            try
            {
                // ID Urusan untuk queri -> IDUrusan
                // IDUrusan untuk Nama -> IDUrusanMaster

                SSQL = "Select tKUA.iTahun, tKUA.IDDInas, tKUA.IDProgram,tKUA.IDkegiatan, tKUA.IDProgram, tKUA.btIDprogram, tKUA.btIDkegiatan, tKUA.IDUrusan, mKegiatan.sNamaKegiatan as Nama ,SUM(tKUA.JumlahOlah) as Jumlah " +
                         " FROM tKUA INNER JOIN mKegiatan ON tKUA.IDUrusanMaster= mKegiatan.IDUrusan and tKUA.IDProgramMaster= mKegiatan.IDProgram and tKUA.IDkegiatanMaster= mKegiatan.ID " +
                         " where tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pDinas.ToString() + " AND mKegiatan.sNamaKegiatan like '%" + sSearch + "%' AND isnull(tKUA.Status,0)<9 " +
                        " GROUP BY tKUA.iTahun, tKUA.IDDInas, tKUA.IDProgram,tKUA.IDkegiatan, tKUA.IDProgram, tKUA.btIDprogram, tKUA.btIDkegiatan, tKUA.IDUrusan, mKegiatan.sNamaKEgiatan " +
                        " ORDER BY tKUA.IDUrusan,tKUA.IDProgram, tKUA.IDkegiatan, tKUA.btIDprogram, tKUA.btIDkegiatan,  mKegiatan.sNamaKEgiatan ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TKegiatan()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Pagu = DataFormat.GetDecimal(dr["Jumlah"])
                                    
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
        public TKegiatan GetByID(int _pDinas, int _idUrusan, int _idProgram, int _idKegiatan)
        {
            TKegiatan tK = new TKegiatan();
            try
            {
                // ID Urusan untuk queri -> IDUrusan
                // IDUrusan untuk Nama -> IDUrusanMaster

                //SSQL = "Select tKUA.iTahun, tKUA.IDDInas, tKUA.IDProgram,tKUA.IDkegiatan, tKUA.IDProgram, tKUA.btIDprogram, tKUA.btIDkegiatan, tKUA.IDUrusan, mKegiatan.sNamaKegiatan as Nama ,SUM(tKUA.JumlahPerubahan) as Jumlah " +
                //         " FROM tKUA INNER JOIN mKegiatan ON tKUA.IDUrusanMaster= mKegiatan.IDUrusan and tKUA.IDProgramMaster= mKegiatan.IDProgram and tKUA.IDkegiatanMaster= mKegiatan.ID " +
                //         " where tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pDinas.ToString() + "  and tKUA.IDUrusan= " + _idUrusan.ToString() + " AND tKUA.IDProgram=" + _idProgram.ToString() + "  AND tKUA.IDkegiatan =" + _idKegiatan.ToString() + "  AND isnull(tKUA.Status,0)<9 " +
                //         " GROUP BY tKUA.iTahun, tKUA.IDDInas, tKUA.IDProgram,tKUA.IDkegiatan, tKUA.IDProgram, tKUA.btIDprogram, tKUA.btIDkegiatan, tKUA.IDUrusan, mKegiatan.sNamaKEgiatan " +
                //        " ORDER BY tKUA.IDUrusan,tKUA.IDProgram, tKUA.IDkegiatan, tKUA.btIDprogram, tKUA.btIDkegiatan,  mKegiatan.sNamaKEgiatan ";

                SSQL = "Select * FROM tKegiatan_A  " +
                        " where IDDInas= " + _pDinas.ToString() + "   and IDkegiatan =" + _idKegiatan.ToString();

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {

                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        tK = new TKegiatan()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),                                    
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                                    Nama = DataFormat.GetString(dr["sNama"]),
                                    Pagu = 0,//DataFormat.GetDecimal(dr["Jumlah"])
                                };
                    }
                }
                return tK;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }

        }
        public List<TKegiatan> GetByFormMaster(int _idUrusan, int _idProgram)
        {

            List<TKegiatan> _lst = new List<TKegiatan>();
            try
            {
                // ID Urusan untuk queri -> IDUrusan
                // IDUrusan untuk Nama -> IDUrusanMaster

                SSQL = "Select * " +
                         " FROM mKegiatan " +
                         " where idUrusan = " + _idUrusan.ToString () +" and  idProgram = " + _idProgram.ToString() + " ORDER BY ID" ;



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TKegiatan()
                                {
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["ID"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                                    Nama = DataFormat.GetString(dr["sNamaKegiatan"])
                                 

                                }).ToList();
                    }
                }
                return _lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }

        }

        public bool CekKegiatanDinas(int _iTahun, int _pDinas)
        {


            try
            {

                SSQL = " Select tKUA.iTahun, tKUA.IDurusan,tKUA.IDDInas, tKUA.IDProgram,tKUA.IDkegiatan, mKegiatan.sNamaKegiatan as sNama,btJenis,tKUA.JumlahOlah as cPlafon  " +
                         " FROM tKUA INNER JOIN mKegiatan ON tKUA.IDUrusanMaster= mKegiatan.IDUrusan and tKUA.IDProgramMaster= mKegiatan.IDProgram and tKUA.IDkegiatanMaster= mKegiatan.ID " +
                         " where tKUA.iTahun= " + _iTahun.ToString() + " AND tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pDinas.ToString() + " AND isnull(tKUA.Status,0)<9 " +                         
                        " ORDER BY tKUA.IDUrusan,tKUA.IDProgram, tKUA.IDkegiatan, tKUA.btIDprogram, tKUA.btIDkegiatan,  mKegiatan.sNamaKEgiatan ";

                DataTable dtkua = new DataTable();
                dtkua = _dbHelper.ExecuteDataTable(SSQL);
                if (dtkua != null)
                {
                    if (dtkua.Rows.Count > 0)
                    {
                        foreach (DataRow drKUA in dtkua.Rows)
                        {

                        
                            SSQL = "Select * FROM tKegiatan_A where iTahun=" + _iTahun.ToString() + " and IDDInas= " + _pDinas.ToString() + "  and IDUrusan= " +                                
                                DataFormat.GetString(drKUA["IDUrusan"]) + " AND IDProgram=" + DataFormat.GetString(drKUA["IDProgram"]) + " AND IDKegiatan=" + DataFormat.GetString(drKUA["IDKEgiatan"]) ;

                            DataTable dtx = new DataTable();
                            dtx = _dbHelper.ExecuteDataTable(SSQL);
                            if (dtx != null)
                            {
                                if (dtx.Rows.Count == 0)
                                {
                                    SSQL = "INSERT INTO " + m_sNamaTabel + " (iTahun, IDDinas,IDUrusan," +
                                            " IDProgram,IDkegiatan ,btJenis,sNama) values (" + _iTahun.ToString() + "," + _pDinas.ToString() + ", " + DataFormat.GetString(drKUA["IDUrusan"]) +
                                            DataFormat.GetString(drKUA["IDProgram"]) + "," + DataFormat.GetString(drKUA["IDKEgiatan"]) + ",3,'" + DataFormat.GetString(drKUA["sNama"]) + "')";
                                    _dbHelper.ExecuteNonQuery(SSQL);


                                } else {

                                    SSQL = " Select tKUA.iTahun, tKUA.IDurusan,tKUA.IDDInas, tKUA.IDProgram,tKUA.IDkegiatan, mKegiatan.sNamaKegiatan as sNama,btJenis,tKUA.JumlahOlah as cPlafon  " +
                                        " FROM tKUA INNER JOIN mKegiatan ON tKUA.IDUrusanMaster= mKegiatan.IDUrusan and tKUA.IDProgramMaster= mKegiatan.IDProgram and tKUA.IDkegiatanMaster= mKegiatan.ID " +
                                        " where tKUA.iTahun= " + _iTahun.ToString() + " AND tKUA.btJenis=3 AND tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pDinas.ToString() + "  and tKUA.IDUrusan= " + DataFormat.GetString(drKUA["IDUrusan"]) + " AND tKUA.IDProgram=" + DataFormat.GetString(drKUA["IDProgram"]) + " AND IDKegiatan=" + DataFormat.GetString(drKUA["IDKEgiatan"])  +"  AND isnull(tKUA.Status,0)<9 " +
                                        " ORDER BY tKUA.IDUrusan,tKUA.IDProgram, tKUA.IDkegiatan, tKUA.btIDprogram, tKUA.btIDkegiatan,  mKegiatan.sNamaKEgiatan ";
                                    
                                    DataTable dt = new DataTable();
                                    dt = _dbHelper.ExecuteDataTable(SSQL);
                                    if (dt != null)
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            DataRow dr = dt.Rows[0];

                                            SSQL = "Update tKegiatan_A SET sNama ='" + DataFormat.GetString(dr["sNama"]) + "' WHERE iTAhun =" + _iTahun.ToString() + " AND IDDInas =" + _pDinas.ToString() +
                                                    " AND IDUrusan =" + DataFormat.GetString(dr["IDUrusan"]) + " AND IDProgram = " + DataFormat.GetString(dr["IDProgram"])  +
                                                    " AND IDKegiatan=" + DataFormat.GetString(dr["IDkegiatan"]) + " AND isnull(sNama,'')=''";// +" AND btJenis= " + oKegiatan.Jenis.ToString();

                                            _dbHelper.ExecuteNonQuery(SSQL);

                                        }
                                        else
                                        {

                                        }
                                    } 
                                }
                            }
                        }
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }
        }


        public bool  ExportToAPBD(int _iTahun, int _pDinas, int _idUrusan, int _idProgram, int _idKegiatan, int _iJenis)
        {
            TKegiatan tK = new TKegiatan();
            try
            {
                // ID Urusan untuk queri -> IDUrusan
                // IDUrusan untuk Nama -> IDUrusanMaster

                SSQL = "INSERT INTO tKegiatan_A (iTahun, IDUrusan,IDDInas, IDProgram, IDkegiatan,sNama,btJenis, cPlafon) " +
                        " Select tKUA.iTahun, tKUA.IDurusan,tKUA.IDDInas, tKUA.IDProgram,tKUA.IDkegiatan, mKegiatan.sNamaKegiatan as sNama,btJenis,tKUA.JumlahOlah as cPlafon  " +
                         " FROM tKUA INNER JOIN mKegiatan ON tKUA.IDUrusanMaster= mKegiatan.IDUrusan and tKUA.IDProgramMaster= mKegiatan.IDProgram and tKUA.IDkegiatanMaster= mKegiatan.ID " +
                         " where tKUA.iTahun= " + _iTahun.ToString() + " AND tKUA.btJenis=" + _iJenis.ToString() + " AND tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pDinas.ToString() + "  and tKUA.IDUrusan= " + _idUrusan.ToString() + " AND tKUA.IDProgram=" + _idProgram.ToString() + "  AND tKUA.IDkegiatan =" + _idKegiatan.ToString() + "  AND isnull(tKUA.Status,0)<9 " +                         
                        " ORDER BY tKUA.IDUrusan,tKUA.IDProgram, tKUA.IDkegiatan, tKUA.btIDprogram, tKUA.btIDkegiatan,  mKegiatan.sNamaKEgiatan ";

                
                _dbHelper.ExecuteNonQuery (SSQL);

               return true ;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }

        }
    }
}
