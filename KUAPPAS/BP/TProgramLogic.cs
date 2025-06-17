using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using BP;
using DTO;
using DataAccess;
using Formatting;

namespace BP
{
    public class TProgramLogic:BP 
    {
        public TProgramLogic(int _pTahun, int profile)
            : base(_pTahun,0,profile)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tkua";

        }
        public List<TPrograms> GetByDinasAndUrusan(int _pDinas, int _pIDUrusan, List<SKPD> lst=null)
        {
            List<TPrograms> _lst = new List<TPrograms>();



            try
            {
                // ID Urusan untuk queri -> IDUrusan
                // IDUrusan untuk Nama -> IDUrusanMaster

              //  SSQL = "Select tKUA.IDProgram, tKUA.btIDprogram, tKUA.IDUrusan, mProgram.sNamaProgram,SUM(tKUA.JumlahOlah) as Jumlah " +
                        // " FROM tKUA INNER JOIN mProgram ON tKUA.IDUrusanMaster= mProgram.IDUrusan and tKUA.IDProgramMaster= mProgram.ID " +
                        // " where tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pDinas.ToString() + "  and tKUA.IDUrusan= " + _pIDUrusan.ToString() +"  AND isnull(tKUA.Status,0)<9 " +
                        //" GROUP BY tKUA.IDProgram, tKUA.btIDprogram, tKUA.IDUrusan, mProgram.sNamaProgram " +
                        //" ORDER BY tKUA.IDUrusan,tKUA.IDProgram, tKUA.btIDprogram,  mProgram.sNamaProgram ";
                if (lst ==null)
                    SSQL = "Select tPrograms_A.IDProgram, tPrograms_A.btIDprogram, tPrograms_A.IDUrusan, tPrograms_A.sNamaProgram as sNamaProgram,0 as Jumlah " +
                         " FROM tPrograms_A  where tPrograms_A.iTahun = " + Tahun.ToString () +"  and tPrograms_A.IDDInas= " + _pDinas.ToString() + "  and tPrograms_A.IDUrusan= " + _pIDUrusan.ToString() +
                        " GROUP BY tPrograms_A.IDProgram, tPrograms_A.btIDprogram, tPrograms_A.IDUrusan, tPrograms_A.sNamaProgram " +
                        " ORDER BY tPrograms_A.IDUrusan,tPrograms_A.IDProgram, tPrograms_A.btIDprogram,  tPrograms_A.sNamaProgram ";
                else
                {
                    string strDinas = "(";
                    foreach (SKPD s in lst)
                    {
                        strDinas = strDinas + s.ID.ToString() + ",";

                    }
                    strDinas = strDinas  + "99)";

                    SSQL = "Select distinct tPrograms_A.IDProgram, tPrograms_A.btIDprogram, tPrograms_A.IDUrusan, tPrograms_A.sNamaProgram as sNamaProgram,0 as Jumlah " +
                         " FROM tPrograms_A  where tPrograms_A.iTahun = " + Tahun.ToString() + "  and tPrograms_A.IDDInas in  " + strDinas + "  and tPrograms_A.IDUrusan= " + _pIDUrusan.ToString() +
                        " GROUP BY tPrograms_A.IDProgram, tPrograms_A.btIDprogram, tPrograms_A.IDUrusan, tPrograms_A.sNamaProgram " +
                        " ORDER BY tPrograms_A.IDUrusan,tPrograms_A.IDProgram, tPrograms_A.btIDprogram,  tPrograms_A.sNamaProgram ";

                }
        

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TPrograms()
                                {
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    Nama= DataFormat.GetString(dr["sNamaProgram"]),
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
        public bool CekProgramDinas(int _iTahun, int _pDinas)
        {
            

            try
            {
            
                        SSQL = " Select tKUA.IDProgram, tKUA.btIDprogram, tKUA.IDUrusan, mProgram.sNamaProgram,SUM(tKUA.JumlahOlah) as Jumlah " +
                         " FROM tKUA INNER JOIN mProgram ON tKUA.IDUrusanMaster= mProgram.IDUrusan and tKUA.IDProgramMaster= mProgram.ID " +
                         " where tKUA.iTahun=" + _iTahun.ToString() + " AND tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pDinas.ToString()  +                          
                          " GROUP BY tKUA.IDProgram, tKUA.btIDprogram, tKUA.IDUrusan, mProgram.sNamaProgram " +
                        " ORDER BY tKUA.IDUrusan,tKUA.IDProgram, tKUA.btIDprogram,  mProgram.sNamaProgram ";

                        DataTable dtkua = new DataTable();
                        dtkua= _dbHelper.ExecuteDataTable(SSQL);
                        if (dtkua != null)
                        {
                            if (dtkua.Rows.Count > 0)
                            {
                                foreach (DataRow drKUA in dtkua.Rows)
                                {

                                    // ID Urusan untuk queri -> IDUrusan
                                    // IDUrusan untuk Nama -> IDUrusanMaster

                                    SSQL = "Select * FROM tPrograms_A where iTahun=" + _iTahun.ToString() + " and IDDInas= " + _pDinas.ToString() + "  and IDUrusan= " +
                                        DataFormat.GetString(drKUA["IDUrusan"]) + " AND IDProgram=" + DataFormat.GetString(drKUA["IDProgram"]) ;
                                    DataTable dtx = new DataTable();
                                    dtx = _dbHelper.ExecuteDataTable(SSQL);
                                    if (dtx != null)
                                    {
                                        if (dtx.Rows.Count == 0)
                                        {
                                            SSQL = " Select tKUA.IDProgram, tKUA.btIDprogram, tKUA.IDUrusan, mProgram.sNamaProgram,SUM(tKUA.JumlahOlah) as Jumlah " +
                                             " FROM tKUA INNER JOIN mProgram ON tKUA.IDUrusanMaster= mProgram.IDUrusan and tKUA.IDProgramMaster= mProgram.ID " +
                                             " where tKUA.iTahun=" + _iTahun.ToString() + " AND tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pDinas.ToString() + "  and tKUA.IDUrusan= " + DataFormat.GetString(drKUA["IDUrusan"]) + " AND tKUA.IDProgram=" + DataFormat.GetString(drKUA["IDProgram"]) + "  AND isnull(tKUA.Status,0)<9 " +
                                              " GROUP BY tKUA.IDProgram, tKUA.btIDprogram, tKUA.IDUrusan, mProgram.sNamaProgram " +
                                              " ORDER BY tKUA.IDUrusan,tKUA.IDProgram, tKUA.btIDprogram,  mProgram.sNamaProgram ";

                                            DataTable dt = new DataTable();
                                            dt = _dbHelper.ExecuteDataTable(SSQL);
                                            if (dt != null)
                                            {
                                                if (dt.Rows.Count > 0)
                                                {
                                                    DataRow dr = dt.Rows[0];
                                                    SSQL = "INSERT INTO tPrograms_A (iTahun, IDDInas,IDUrusan,IDProgram, sNamaProgram, btJenis, btTahapInput) values ( " +
                                                           _iTahun.ToString() + "," + _pDinas.ToString() + "," + DataFormat.GetString(dr["IDUrusan"]) + "," + DataFormat.GetString(drKUA["IDProgram"]) + " ,'" + DataFormat.GetString(dr["sNamaProgram"]) + "',3,1)";//

                                                    _dbHelper.ExecuteNonQuery(SSQL);

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
        public bool CekProgramDinas(int _iTahun, int _pDinas, int _pIDUrusan, int _pIDProgram)
        {
            TPrograms _tP = new TPrograms();
            try
            {
                // ID Urusan untuk queri -> IDUrusan
                // IDUrusan untuk Nama -> IDUrusanMaster

                SSQL = "Select * FROM tPrograms_A where iTahun=" + _iTahun.ToString() + " and IDDInas= " + _pDinas.ToString() + "  and IDUrusan= " + _pIDUrusan.ToString() + " AND IDProgram=" + _pIDProgram.ToString();
                DataTable dtx = new DataTable();
                dtx = _dbHelper.ExecuteDataTable(SSQL);
                if (dtx != null)
                {
                    if (dtx.Rows.Count == 0)
                    {
                        SSQL=" Select tKUA.IDProgram, tKUA.btIDprogram, tKUA.IDUrusan, mProgram.sNamaProgram,SUM(tKUA.JumlahOlah) as Jumlah " +
                         " FROM tKUA INNER JOIN mProgram ON tKUA.IDUrusanMaster= mProgram.IDUrusan and tKUA.IDProgramMaster= mProgram.ID " +
                         " where tKUA.iTahun=" + _iTahun.ToString() + " AND tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pDinas.ToString() + "  and tKUA.IDUrusan= " + _pIDUrusan.ToString() + " AND tKUA.IDProgram=" + _pIDProgram.ToString() + "  AND isnull(tKUA.Status,0)<9 " +
                          " GROUP BY tKUA.IDProgram, tKUA.btIDprogram, tKUA.IDUrusan, mProgram.sNamaProgram " +
                        " ORDER BY tKUA.IDUrusan,tKUA.IDProgram, tKUA.btIDprogram,  mProgram.sNamaProgram ";

                            DataTable dt = new DataTable();
                            dt = _dbHelper.ExecuteDataTable(SSQL);
                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    DataRow dr = dt.Rows[0];
                                    SSQL="INSERT INTO tPrograms_A (iTahun, IDDInas,IDUrusan,IDProgram, sNamaProgram, btJenis, btTahapInput) values ( " +
                                           _iTahun.ToString() + "," + _pDinas.ToString() + "," + _pIDUrusan.ToString() + ","+ _pIDProgram.ToString() + ",'" + DataFormat.GetString(dr["sNamaProgram"]) + "',3,1)";//
                                    
                                    _dbHelper.ExecuteNonQuery(SSQL);

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

        public TPrograms GetByDinasAndUrusanProgram(int _iTahun, int _pDinas, int _pIDUrusan, int _pIDProgram)
        {
            TPrograms _tP = new TPrograms();

            try
            {
                // ID Urusan untuk queri -> IDUrusan
                // IDUrusan untuk Nama -> IDUrusanMaster

                SSQL = "Select tKUA.IDProgram, tKUA.btIDprogram, tKUA.IDUrusan, mProgram.sNamaProgram,SUM(tKUA.JumlahOlah) as Jumlah " +
                         " FROM tKUA INNER JOIN mProgram ON tKUA.IDUrusanMaster= mProgram.IDUrusan and tKUA.IDProgramMaster= mProgram.ID " +
                         " where tKUA.iTahun=" + _iTahun.ToString() + " AND tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pDinas.ToString() + "  and tKUA.IDUrusan= " + _pIDUrusan.ToString() + " AND tKUA.IDProgram=" + _pIDProgram.ToString() + "  AND isnull(tKUA.Status,0)<9 " +
                            " GROUP BY tKUA.IDProgram, tKUA.btIDprogram, tKUA.IDUrusan, mProgram.sNamaProgram " +
                        " ORDER BY tKUA.IDUrusan,tKUA.IDProgram, tKUA.btIDprogram,  mProgram.sNamaProgram ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        _tP = new TPrograms()
                                {
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    Nama = DataFormat.GetString(dr["sNamaProgram"]),
                                    Pagu = DataFormat.GetDecimal(dr["Jumlah"])
                                };
                    }
                }
                return _tP;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }
        }
        public TPrograms GetByID(int _pDinas,int _pIDProgram, List<SKPD> lst=null)
        {
            TPrograms oPrg = new TPrograms();

            try
            {
                // ID Urusan untuk queri -> IDUrusan
                // IDUrusan untuk Nama -> IDUrusanMaster

                //SSQL = "Select tKUA.IDProgram, tKUA.btIDprogram, tKUA.IDUrusan, mProgram.sNamaProgram,SUM(tKUA.JumlahOlah) as Jumlah " +
                //         " FROM tKUA INNER JOIN mProgram ON tKUA.IDUrusanMaster= mProgram.IDUrusan and tKUA.IDProgramMaster= mProgram.ID " +
                 //        " where tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pDinas.ToString() + "  and tKUA.IDProgram= " + _pIDProgram.ToString() + "  AND isnull(tKUA.Status,0)<9 " +
                 //       " GROUP BY tKUA.IDProgram, tKUA.btIDprogram, tKUA.IDUrusan, mProgram.sNamaProgram ";
                if (lst == null)

                    SSQL = "Select IDProgram, btIDprogram, IDUrusan, sNamaProgram,0  as Jumlah " +
                        " FROM tPrograms_A " +
                        " where IDDInas= " + _pDinas.ToString() + "  and IDProgram= " + _pIDProgram.ToString() +
                       " ";// GROUP BY tKUA.IDProgram, tKUA.btIDprogram, tKUA.IDUrusan, mProgram.sNamaProgram ";
                else
                {
                    string strDinas = "(";
                    foreach (SKPD s in lst)
                    {
                        strDinas = strDinas + s.ID.ToString() + ",";

                    }
                    strDinas = strDinas + "99)";
                    SSQL = "Select distinct IDProgram, btIDprogram, IDUrusan, sNamaProgram,0  as Jumlah, outcome,keluaran  " +
                        " FROM tPrograms_A " +
                        " where IDDInas in  " + strDinas + "  and IDProgram= " + _pIDProgram.ToString() +
                       " ";
                }

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        oPrg = new TPrograms()
                                {
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    Nama = DataFormat.GetString(dr["sNamaProgram"]),
                                    Pagu = DataFormat.GetDecimal(dr["Jumlah"]),
                                   
                                    TampilanKode = DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(0,1)+"." + 
                                                    DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(1,2)+"." +
                                                    _pDinas.ToString().Substring(0, 1) + "." +
                                                   _pDinas.ToString().Substring(1, 2) + "." +
                                                   _pDinas.ToString().Substring(3, 2) + "." + 
                                                    DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(3,2)

                                };
                    }
                }
                return oPrg;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }
        }
    }
}
