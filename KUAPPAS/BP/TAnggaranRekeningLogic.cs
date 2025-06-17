using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DTO.Bendahara;
using DataAccess;
using Formatting;
using System.Data;
using KUAPPAS;
using NPOI.SS.Formula.Functions;
using System.Drawing;

namespace BP
{
    public class TAnggaranRekeningLogic:BP
    {
        private Single m_iTahap;
        private int mprofile;
        public TAnggaranRekeningLogic(int _pTahun, int _perbaikan=0,int profile=2)
            : base(_pTahun, _perbaikan,profile)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tAnggaranRekening_A";// "tAnggaranRekening_A";
            mprofile = profile;
            SetProfileRekening(profile);
        }


        public List<AnggaranDInkes> GetAnggaranDinkes()
        {
            List<AnggaranDInkes> lst = new List<AnggaranDInkes>();
            SSQL = " select * from programdinkes   order by No";
            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    lst = (from DataRow dr in dt.Rows
                            select new AnggaranDInkes()
                            {

                                Kode = DataFormat.GetString (dr["Kode"]),
                                Keterangan = DataFormat.GetString(dr["Nama"]),
                                Nilai  = DataFormat.GetDecimal(dr["Nilai"])

                            }).ToList();
                }
                return lst; 
            }
            else
            {
                return null;
            }
            



        }
        public List<RekapAnggaran> GetRekeningAllLevel(int _pTahun, int _IDDinas , int _jenis, Single _pTahap, int iJenis = 4)
        {
            string sKolom = GetKolomx(_pTahap);
            SKPDLogic oLogic = new SKPDLogic(_pTahun);
            List<SKPD> lstSKPD = new List<SKPD>();
            lstSKPD = oLogic.GetByParent(_IDDinas);
            string strSKPD = "(";
            if (lstSKPD.Count > 1)
            {
                foreach (SKPD d in lstSKPD)
                {
                    strSKPD = strSKPD + d.ID.ToString() + ",";
                }
                strSKPD = strSKPD + "99)";
            }
            else
            {
                strSKPD = strSKPD + _IDDinas.ToString() + ")";

            }


            List<RekapAnggaran> _lst = new List<RekapAnggaran>();
            try{
            
                string sLevel = "5";
                if (Tahun > 2020)
                {
                     sLevel = "6";
                
                }
                SSQL = "";
                SSQL = "Select 1 as Level, " + _IDDinas + " AS IDDinas, 0 as IDUrusan, 0 as IDProgram,0 as IDkegiatan,0 as idsubkegiatan, 0 as btIDSubkegiatan,0 as IDRekening, '' AS Nama, 0 as btJenis, " +
                    " SUM(B." + sKolom + ")AS JumlahOlah, " +
                    " SUM(B." + sKolom + ") AS Jumlah , " +
                    " SUM(B." + sKolom + ")  AS JumlahMurni from mSKPD A  " +
                    " INNER JOIN tANGGARANREKENING_A B ON A.ID = B.IDDInas " +
                    " WHERE B.iTahun = " + _pTahun.ToString() + " AND B.IDDInas in " + strSKPD;
                    if (_jenis == 3)
                    {
                        SSQL = SSQL + " AND B.btJenis in (3,5) ";
                    }
                    else
                    {
                        SSQL = SSQL + " AND B.btJenis= " + _jenis.ToString();
                    }


                    SSQL = SSQL + " UNION ALL ";


                    SSQL = SSQL +
                        " Select 2 as Level, " + _IDDinas + " AS IDDInas  , A.ID as IDUrusan, 0  as IDProgram,0 as IDkegiatan, 0 as idsubkegiatan,0 as btIDSubkegiatan, 0 as IDRekening, A.sNamaUrusan AS Nama, 3 as btJenis ," +
                        " SUM(B." + sKolom + ")AS JumlahOlah, " +
                        " SUM(B." + sKolom + ") AS Jumlah , " +
                        " SUM(B." + sKolom + ")  AS JumlahMurni from mUrusan A " +
                        " INNER JOIN tANGGARANREKENING_A B ON A.ID= B.IDUrusan " +
                        " WHERE B.iTahun = " + _pTahun.ToString() + " AND B.IDDInas in " + strSKPD;
                        if (_jenis == 3)
                        {
                            SSQL = SSQL + " AND B.btJenis in (3,5) ";
                        }
                        else
                        {
                            SSQL = SSQL + " AND B.btJenis= " + _jenis.ToString();
                        }

                        SSQL = SSQL + " GROUP BY  A.ID , A.sNamaUrusan ";//, B.btJenis ";
                SSQL = SSQL + " UNION ALL " +
                    " Select 3 as Level, " + _IDDinas + " AS IDDinas, A.IDUrusan as IDUrusan, A.IDProgram  as IDProgram,0 as IDkegiatan,0 as idsubkegiatan, 0 as btIDSubkegiatan, 0 as IDRekening,  " +
                    " A.sNamaProgram AS Nama, B.btJenis, " +
                    " SUM(B." + sKolom + ")AS JumlahOlah, " +
                    " SUM(B." + sKolom + ") AS Jumlah , " +
                    " SUM(B." + sKolom + ")  AS JumlahMurni  from tPrograms_A A " +
                    " INNER JOIN tANGGARANREKENING_A B ON A.iTahun = B.iTahun AND A.IDUrusan= B.IDUrusan AND A.IDDinas= B.IDDInas AND A.IDProgram= B.IDProgram and A.btJenis=B.btJenis " +
                    " WHERE B.iTahun =" + _pTahun.ToString() + " AND B.IDDInas in " + strSKPD;
                if (_jenis == 3)
                {
                    SSQL = SSQL + " AND B.btJenis in (3,5) ";
                }
                else
                {
                    SSQL = SSQL + " AND B.btJenis= " + _jenis.ToString();
                }

                SSQL = SSQL + " GROUP BY  A.IDUrusan , A.IDProgram  ,A.sNamaProgram,B.btJenis  ";

                if (iJenis > 1)
                // UNTUK uO dan GU Tidak sampai detail  
                {
                    SSQL = SSQL + "  UNION ALL " +
                        " Select 4 as Level, " + _IDDinas + "  AS IDDinas,A.IDUrusan as IDUrusan, A.IDProgram  as IDProgram,A.IDkegiatan as IDkegiatan,0 as idsubkegiatan, 0 as btIDSubkegiatan, 0 as IDRekening,  " +
                        " A.sNama AS Nama, B.btJenis as btJenis," +
                        " SUM(B." + sKolom + ")AS JumlahOlah, " +
                        " SUM(B." + sKolom + ") AS Jumlah , " +
                          " SUM(B." + sKolom + ")  AS JumlahMurni  from tKegiatan_A A " +
                        " INNER JOIN tANGGARANREKENING_A B ON A.iTahun = B.iTahun AND A.IDUrusan= B.IDUrusan AND A.IDDinas= B.IDDInas AND A.IDProgram= B.IDProgram " +
                         " AND A.IDKegiatan=B.IDKegiatan AND A.btJenis=B.btJenis  " +
                        " WHERE B.iTahun =" + _pTahun.ToString() + " AND B.IDDInas in " + strSKPD;
                    if (_jenis == 3)
                    {
                        SSQL = SSQL + " AND B.btJenis in (3,5) ";
                    }
                    else
                    {
                        SSQL = SSQL + " AND B.btJenis= " + _jenis.ToString();
                    }

                    SSQL = SSQL + " GROUP BY   A.IDUrusan , A.IDProgram ,A.IDKegiatan ,A.sNama,B.btJenis  ";
                   
                       // SUbKegiatan 
                        SSQL = SSQL + "  UNION ALL " +
                            " Select 5 as Level, " + _IDDinas + "  AS IDDinas,A.IDUrusan as IDUrusan, A.IDProgram  as IDProgram,A.IDkegiatan as IDkegiatan,A.IDSubKegiatan as idsubkegiatan, A.IDSubKegiatan as btIDSubkegiatan, 0 as IDRekening,  " +
                            " A.Nama AS Nama, B.btJenis as btJenis," +
                            " SUM(B." + sKolom + ")AS JumlahOlah, " +
                            " SUM(B." + sKolom + ") AS Jumlah , " +
                              " SUM(B." + sKolom + ")  AS JumlahMurni  from tSubKegiatan A " +
                            " INNER JOIN tANGGARANREKENING_A B ON A.iTahun = B.iTahun AND A.IDUrusan= B.IDUrusan AND A.IDDinas= B.IDDInas AND A.IDProgram= B.IDProgram " +
                             " AND A.IDKegiatan=B.IDKegiatan AND A.IDSUbKegiatan = B.IDSubKegiatan" +
                            " WHERE B.iTahun =" + _pTahun.ToString() + " AND B.IDDInas in " + strSKPD + " AND B.btJenis= " + _jenis.ToString() +
                            " GROUP BY  A.IDUrusan , A.IDProgram ,A.IDKegiatan ,A.IDSubKegiatan,A.Nama,B.btJenis  ";
                    

                    SSQL = SSQL + "  UNION ALL " +
                        " Select " + sLevel + "  as Level, " + _IDDinas + "  AS IDDinas,  B.IDUrusan as IDUrusan, B.IDProgram  as IDProgram,B.IDkegiatan as IDkegiatan,B.IDSubkegiatan as IDSubkegiatan, B.IDSubkegiatan as btIDSubkegiatan, A.IIDRekening as IDRekening,  " +
                        " A.sNamaRekening AS Nama,B.btJenis," +
                        " SUM(B." + sKolom + ") AS JumlahOlah, " +
                        " SUM(B." + sKolom + ") AS Jumlah , " +
                        " SUM(B." + sKolom + ")  AS JumlahMurni  from mRekening  A " +
                        " INNER JOIN tANGGARANREKENING_A B ON A.IIDRekening= B.IIDRekening  " +
                        " WHERE B.iTahun =" + _pTahun.ToString() + "  AND B.IDDInas in " + strSKPD ;
                        if (_jenis == 3)
                        {
                            SSQL = SSQL + " AND B.btJenis in (3,5) ";
                        }
                        else
                        {
                            SSQL = SSQL + " AND B.btJenis= " + _jenis.ToString();
                        }

                        SSQL = SSQL + " GROUP BY   B.IDUrusan , B.IDProgram  ,B.IDkegiatan ,B.IDSubkegiatan, A.IIDRekening,  A.sNamaRekening,B.btJenis  ";
                }
                SSQL = SSQL + " ORDER BY btJenis,IDUrusan , IDProgram  ,IDkegiatan ,IDSUbKegiatan, IDRekening,btIDSUBKegiatan ";
                
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RekapAnggaran()
                                {
                                    Level = DataFormat.GetInteger(dr["Level"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Kode = GetKodeSPD(DataFormat.GetInteger(dr["Level"]),
                                            DataFormat.GetInteger(dr["IDDInas"]),
                                            DataFormat.GetInteger(dr["IDUrusan"]),
                                            DataFormat.GetInteger(dr["IDProgram"]),
                                            DataFormat.GetInteger(dr["IDKegiatan"]),
                                            DataFormat.GetLong(dr["IDSubkegiatan"]),
                                            DataFormat.GetLong(dr["IDRekening"])),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubkegiatan"]), 

                                    IDRekening = DataFormat.GetLong(dr["IDRekening"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Jumlah = DataFormat.GetDecimal(dr["JumlahMurni"]) != DataFormat.GetDecimal(dr["JumlahOlah"]) ? DataFormat.GetDecimal(dr["JumlahOlah"]) : DataFormat.GetDecimal(dr["JumlahMurni"]),
                                    //DataFormat.GetDecimal(dr["Jumlah"]),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]),
                                    JumlahOlah = DataFormat.GetDecimal(dr["JumlahOlah"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    Tahap = _pTahap


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
        public List<RekapAnggaran> GetRekeningAllLevelSampaiUK(int _pTahun, int pIDDinas, int _jenis, Single _pTahap, int iJenis = 4)
        {
            string sKolom = GetKolomx(_pTahap);
            SKPDLogic oLogic = new SKPDLogic(_pTahun);
            List<SKPD> lstSKPD = new List<SKPD>();
            

            DBParameterCollection paramCollection = new DBParameterCollection();





            List<RekapAnggaran> _lst = new List<RekapAnggaran>();
            try
            {

                string sLevel = "5";
                if (Tahun > 2020)
                {
                    sLevel = "6";

                }
                SSQL = "";
                SSQL = "Select 1 as Level, " + pIDDinas + " AS IDDinas, 0 as KodeUnit, 0 as IDUrusan, 0 as IDProgram,0 as IDkegiatan,0 as idsubkegiatan, 0 as btIDSubkegiatan,0 as IDRekening, '' AS Nama, 0 as btJenis, " +
                    " SUM(B." + sKolom + ")AS JumlahOlah, " +
                    " SUM(B." + sKolom + ") AS Jumlah , " +
                    " SUM(B." + sKolom + ")  AS JumlahMurni from mSKPD A  " +
                    " INNER JOIN tANGGARANREKENING_A B ON A.ID = B.IDDInas " +
                    " INNER JOIN tSUbKegiatan  C ON B.iTahun = C.iTahun and  B.IDDInas = C.IDDInas and B.IDSUBKegiatan = C.IDSUBKegiatan " +
                    " WHERE B.iTahun =  @Tahun AND B.IDDInas =@IDDinas";
                if (_jenis == 3)
                {
                    SSQL = SSQL + " AND B.btJenis in (3,5) ";
                }
                else
                {
                    SSQL = SSQL + " AND B.btJenis= " + _jenis.ToString();
                }




                SSQL = SSQL + " UNION ALL ";


                SSQL = SSQL +
                    " Select 2 as Level, " + pIDDinas + " AS IDDInas  , 0 as KodeUnit, A.ID as IDUrusan, 0  as IDProgram,0 as IDkegiatan, 0 as idsubkegiatan,0 as btIDSubkegiatan, 0 as IDRekening, A.sNamaUrusan AS Nama, 3 as btJenis ," +
                    " SUM(B." + sKolom + ")AS JumlahOlah, " +
                    " SUM(B." + sKolom + ") AS Jumlah , " +
                    " SUM(B." + sKolom + ")  AS JumlahMurni from mUrusan A " +
                    " INNER JOIN tANGGARANREKENING_A B ON A.ID= B.IDUrusan " +
                    " INNER JOIN tSUbKegiatan  C ON B.iTahun = C.iTahun and  B.IDDInas = C.IDDInas and B.IDSUBKegiatan = C.IDSUBKegiatan " +
                    " WHERE B.iTahun = @Tahun AND B.IDDInas = @IDDinas " ;
                if (_jenis == 3)
                {
                    SSQL = SSQL + " AND B.btJenis in (3,5) ";
                }
                else
                {
                    SSQL = SSQL + " AND B.btJenis= " + _jenis.ToString();
                }
           
                SSQL = SSQL + " GROUP BY  A.ID , A.sNamaUrusan ";//, B.btJenis ";
                SSQL = SSQL + " UNION ALL " +
                    " Select 3 as Level, " + pIDDinas + " AS IDDinas, 0  as KodeUnit, A.IDUrusan as IDUrusan, A.IDProgram  as IDProgram,0 as IDkegiatan,0 as idsubkegiatan, 0 as btIDSubkegiatan, 0 as IDRekening,  " +
                    " A.sNamaProgram AS Nama, B.btJenis, " +
                    " SUM(B." + sKolom + ")AS JumlahOlah, " +
                    " SUM(B." + sKolom + ") AS Jumlah , " +
                    " SUM(B." + sKolom + ")  AS JumlahMurni  from tPrograms_A A " +
                    " INNER JOIN tANGGARANREKENING_A B ON A.iTahun = B.iTahun AND A.IDUrusan= B.IDUrusan AND A.IDDinas= B.IDDInas AND A.IDProgram= B.IDProgram and A.btJenis=B.btJenis " +
                    " INNER JOIN tSUbKegiatan  C ON B.iTahun = C.iTahun and B.IDDInas = C.IDDInas and B.IDSUBKegiatan = C.IDSUBKegiatan " +
                    
                    " WHERE B.iTahun =@Tahun AND B.IDDInas =@IDDInas ";
                if (_jenis == 3)
                {
                    SSQL = SSQL + " AND B.btJenis in (3,5) ";
                }
                else
                {
                    SSQL = SSQL + " AND B.btJenis= " + _jenis.ToString();
                }

                SSQL = SSQL + " GROUP BY A.IDUrusan , A.IDProgram  ,A.sNamaProgram,B.btJenis  ";
                
                if (iJenis > 1)
                // UNTUK uO dan GU Tidak sampai detail  
                {
                    SSQL = SSQL + " UNION ALL  " +
                        " Select 4 as Level, " + pIDDinas + "  AS IDDinas,0 as KodeUnit,  A.IDUrusan as IDUrusan, A.IDProgram  as IDProgram,A.IDkegiatan as IDkegiatan,0 as idsubkegiatan, 0 as btIDSubkegiatan, 0 as IDRekening,  " +
                        " A.sNama AS Nama, B.btJenis as btJenis," +
                        " SUM(B." + sKolom + ")AS JumlahOlah, " +
                        " SUM(B." + sKolom + ") AS Jumlah , " +
                          " SUM(B." + sKolom + ")  AS JumlahMurni  from tKegiatan_A A " +
                        " INNER JOIN tANGGARANREKENING_A B ON A.iTahun = B.iTahun AND A.IDUrusan= B.IDUrusan AND A.IDDinas= B.IDDInas AND A.btKOdeUK= B.btKOdeUK  AND A.IDProgram= B.IDProgram " +
                         " AND A.IDKegiatan=B.IDKegiatan AND A.btJenis=B.btJenis  " +
                        " WHERE B.iTahun =@Tahun  AND B.IDDInas= @IDDInas";
                    if (_jenis == 3)
                    {
                        SSQL = SSQL + " AND B.btJenis in (3,5) ";
                    }
                    else
                    {
                        SSQL = SSQL + " AND B.btJenis= " + _jenis.ToString();
                    }

                    SSQL = SSQL + " GROUP BY   A.IDUrusan , A.IDProgram ,A.IDKegiatan ,A.sNama,B.btJenis  ";

                    // SUbKegiatan 
                    SSQL = SSQL + "  UNION ALL " +
                        " Select 5 as Level, " + pIDDinas + "  AS IDDinas,B.btKodeUK as KodeUnit,  A.IDUrusan as IDUrusan, A.IDProgram  as IDProgram,A.IDkegiatan as IDkegiatan,A.IDSubKegiatan as idsubkegiatan, A.IDSubKegiatan as btIDSubkegiatan, 0 as IDRekening,  " +
                        " A.Nama AS Nama, B.btJenis as btJenis," +
                        " SUM(B." + sKolom + ")AS JumlahOlah, " +
                        " SUM(B." + sKolom + ") AS Jumlah , " +
                          " SUM(B." + sKolom + ")  AS JumlahMurni  from tSubKegiatan A " +
                        " INNER JOIN tANGGARANREKENING_A B ON A.iTahun = B.iTahun AND A.IDUrusan= B.IDUrusan AND A.IDDinas= B.IDDInas AND A.IDProgram= B.IDProgram " +
                         " AND A.btKodeuk = B.btKodeuk and A.IDKegiatan=B.IDKegiatan AND A.IDSUbKegiatan = B.IDSubKegiatan" +
                        "  WHERE B.iTahun =@Tahun  AND B.IDDInas = @IDDInas  AND B.btJenis= " + _jenis.ToString() +
                        " GROUP BY  B.btKodeUK , A.IDUrusan , A.IDProgram ,A.IDKegiatan ,A.IDSubKegiatan,A.Nama,B.btJenis  ";


                    SSQL = SSQL + "  UNION ALL " +
                        " Select " + sLevel + "  as Level, " + pIDDinas + "  AS IDDinas,B.btKodeUK as KodeUnit,  B.IDUrusan as IDUrusan, B.IDProgram  as IDProgram,B.IDkegiatan as IDkegiatan,B.IDSubkegiatan as IDSubkegiatan, B.IDSubkegiatan as btIDSubkegiatan, A.IIDRekening as IDRekening,  " +
                        " A.sNamaRekening AS Nama,B.btJenis," +
                        " SUM(B." + sKolom + ") AS JumlahOlah, " +
                        " SUM(B." + sKolom + ") AS Jumlah , " +
                        " SUM(B." + sKolom + ")  AS JumlahMurni  from mRekening  A " +
                        " INNER JOIN tANGGARANREKENING_A B ON A.IIDRekening= B.IIDRekening  " +
                        "  WHERE B.iTahun =@Tahun  AND B.IDDInas =@IDDInas ";
                    if (_jenis == 3)
                    {
                        SSQL = SSQL + " AND B.btJenis in (3,5) ";
                    }
                    else
                    {
                        SSQL = SSQL + " AND B.btJenis= " + _jenis.ToString();
                    }

                    SSQL = SSQL + " GROUP BY  B.btKodeUK , B.IDUrusan , B.IDProgram  ,B.IDkegiatan ,B.IDSubkegiatan, A.IIDRekening,  A.sNamaRekening,B.btJenis  ";
                }
                SSQL = SSQL + " ORDER BY btJenis,  KodeUnit, IDUrusan , IDProgram  ,IDkegiatan ,IDSUbKegiatan, IDRekening,btIDSUBKegiatan ";
                paramCollection.Add(new DBParameter("@Tahun", Tahun));  
                paramCollection.Add(new DBParameter("@IDDinas", pIDDinas));
                
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL,paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RekapAnggaran()
                                {
                                    Level = DataFormat.GetInteger(dr["Level"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Kode = GetKodeSPD(DataFormat.GetInteger(dr["Level"]),
                                            DataFormat.GetInteger(dr["IDDInas"]),
                                            DataFormat.GetInteger(dr["IDUrusan"]),
                                            DataFormat.GetInteger(dr["IDProgram"]),
                                            DataFormat.GetInteger(dr["IDKegiatan"]),
                                            DataFormat.GetLong(dr["IDSubkegiatan"]),
                                            DataFormat.GetLong(dr["IDRekening"])),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubkegiatan"]),
                                    KodeUnit = DataFormat.GetInteger (dr["KodeUnit"]),
                                    IDRekening = DataFormat.GetLong(dr["IDRekening"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Jumlah = DataFormat.GetDecimal(dr["JumlahMurni"]) != DataFormat.GetDecimal(dr["JumlahOlah"]) ? DataFormat.GetDecimal(dr["JumlahOlah"]) : DataFormat.GetDecimal(dr["JumlahMurni"]),
                                    //DataFormat.GetDecimal(dr["Jumlah"]),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]),
                                    JumlahOlah = DataFormat.GetDecimal(dr["JumlahOlah"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    Tahap = _pTahap


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

        public bool SimpanRealisasiBL(List<TAnggaranRekening> _lst, int _iTahun, int _idDinas, int idUrusan, int idProgram, int _idKegiatan, long _idsubkegiatan,int _iJenis)
        {

            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;
            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSubKegiatan;
            int _KodeSKPD;
            int _KodeUK;
         
            IDbTransaction transaction = _dbHelper.GetConnObject().BeginTransaction();

            try
            {
                foreach (TAnggaranRekening o in _lst)
                {
                    if (o.Jenis == 3)
                    {
                        _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 3));
                        _KodeSubKegiatan = DataFormat.GetInteger(o.IDSubKegiatan.ToString().Substring(8, 2));
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                    }
                    else
                    {
                        _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                        _KodeSubKegiatan = 0; 
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                        o.IDUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 3));
                    }

                    _KodeKategori = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                    _KodeUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    _KodeSKPD = DataFormat.GetInteger(o.IDDinas.ToString().Substring(3, 2));

                    if (o.IDDinas.ToString().Length > 5)
                    {
                        _KodeUK = DataFormat.GetInteger(o.IDDinas.ToString().Substring(5, 2));
                    }
                    else
                    {
                        _KodeUK = 0;
                    }
                    if (o.StatusUpdate == 0)
                    {
                        SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                               " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btIDsubKegiatan,btTahapInput, " +
                               "IIDRekening, cJumlahMUrni, cJumlahGeser,cJumlahRKAP,cDPA,cJumlahABT,bPPKD)  values ( " +
                                "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                "@pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtIDsubKegiatan,@pbtTahapInput,@pIIDRekening," +
                                "@pcJumlahMUrni, @pcJumlahGeser, @pcJumlahRKAP,@pcDPA,@pcJumlahABT,@bppkd)";


                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pTahun", o.Tahun));
                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                        paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana));
                        paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana));
                        paramCollection.Add(new DBParameter("@pbtIDsubKegiatan",_KodeSubKegiatan));
                        paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput));
                        paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori));
                        paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan));
                        paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD));
                        paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK));
                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                        paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram));
                        paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan));
                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                        paramCollection.Add(new DBParameter("@pcJumlahMUrni", o.JumlahMurni, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pcJumlahGeser", o.JumlahPergeseran, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pcJumlahRKAP", o.JumlahRKAP, DbType.Decimal));
                       // paramCollection.Add(new DBParameter("@pcJumlahRKAP", o.PlafonABT, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pcDPA", o.PlafonABT, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pcJumlahABT", o.PlafonABT, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@bppkd", o.PPKD));


                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    }
                    else
                    {
                    
                        string SSQL = "UPDATE " + m_sNamaTabel + " SET cJumlahMurni = " + ToSQLUang(o.JumlahMurni) + ", cJumlahGeser = " + ToSQLUang(o.JumlahPergeseran) +
                              ",  cJumlahRKAP= " + ToSQLUang(o.JumlahRKAP) + ", cJumlahABT= " + ToSQLUang(o.PlafonABT) + ", cPlafon = " + ToSQLUang(o.Plafon) + "  WHERE iTahun=" + o.Tahun.ToString() + "  AND IDDInas=" + 
                              o.IDDinas.ToString() +" AND IDProgram=" + o.IDProgram.ToString() + " AND IDkegiatan=" + o.IDKegiatan.ToString() + " and IDSUbKegiatan=" + 
                              o.IDSubKegiatan.ToString()  +
                             " AND IDUrusan=" + o.IDUrusan.ToString() + " and IIDRekening=" + o.IDRekening.ToString () ;//and isnull(bPPKD,0)=@pbPPKD ";

                       _dbHelper.ExecuteNonQuery(SSQL);


                  
                    }

                }
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _isError = true;
                _lastError = ex.Message;
                return false;


            }
        }
        public bool SimpanRealisasi(List<TAnggaranRekening> _lst, int _iTahun, int _idDinas, int KodeUK ,int idUrusan, int idProgram, int _idKegiatan, long _idsubkegiatan, int _iJenis)
        {

            IDbTransaction transaction = _dbHelper.GetConnObject().BeginTransaction();

            try
            {
                foreach (TAnggaranRekening o in _lst)
                {
                    string SSQL = "UPDATE tAnggaranRekening_A  SET cRealisasi = " + ToSQLUang(o.Realisasi) + "WHERE iTahun=" + o.Tahun.ToString() + "  AND IDDInas=" +
                              o.IDDinas.ToString() + " AND btKodeUK="+ KodeUK.ToString()+" AND IDProgram=" + o.IDProgram.ToString() + " AND IDkegiatan=" + o.IDKegiatan.ToString() + " and IDSUbKegiatan=" +
                              o.IDSubKegiatan.ToString() +" AND IDUrusan=" + o.IDUrusan.ToString() + " and IIDRekening=" + o.IDRekening.ToString();//and isnull(bPPKD,0)=@pbPPKD ";

                    _dbHelper.ExecuteNonQuery(SSQL);


                    
                }
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _isError = true;
                _lastError = ex.Message;
                return false;


            }
        }

        private string ToSQLUang(decimal o)
        {
            return o.ToString().Replace(",", ".");
        }
        
        public void PerbaikiABT(int dinas)
        {
            List<TAnggaranRekening> lstNull = new List<TAnggaranRekening>();
            
            SSQL = "SELECT iTahun, btJenis,iddinas,btKodeuK,idurusan , IDProgram ,IDKegiatan ,IDSubKegiatan " +
                ", iIDRekening, cJumlahABT from tAnggaranRekening_A  where " +
                " iddinas = " + dinas.ToString() + " and cJumlahRKAP is null";


            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    lstNull = (from DataRow dr in dt.Rows
                            select new TAnggaranRekening()
                            {
                                Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                IDSubKegiatan = DataFormat.GetLong(dr["IdSubKegiatan"]),
                                IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                JumlahABT = DataFormat.GetDecimal(dr["cJumlahABT"]),
                                KodeUK = DataFormat.GetInteger(dr["btKodeuk"]),
                                Jenis = DataFormat.GetInteger(dr["btJenis"]),
                            }).ToList();
                }
            }
     
            foreach (TAnggaranRekening o in lstNull)
            {

                if (CekAda(o) == 0)
                {
                    SSQL = "INSERT INTO tAnggaranRekenig_A (iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,IDSubKegiatan ," +
                                  "  btKodeuK,btJenis,  " +
                                  "IIDRekening, cJumlahMurni,cJumlahRKA,cJumlahGeser, cJumlahRKAP,cJumlahABT,bPPKD)  values ( " +
                                   "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pIDSubKegiatan ," +
                                   "@pbtKodeuK,3,@pIIDRekening," +
                                   "0,0,0,0,@pcJumlahRKAP,0)";
                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pTahun", o.Tahun));
                paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                paramCollection.Add(new DBParameter("@pIDSubKegiatan", o.IDSubKegiatan));
                paramCollection.Add(new DBParameter("@pbtKodeuK", o.KodeUK));
                paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                paramCollection.Add(new DBParameter("@pcJumlahABT", o.JumlahRKAP));

                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


            }
                else
            {


                SSQL = "UPDATE TANGGARANREKENING_A SET  cJumlahABT= @JUMLAHABT WHERE iTahun=2024 AND " +
                    " IDDInas=@pIDDInas AND IDUrusan =@pIDUrusan AND IDProgram=@pIDProgram AND " +
                    "IDkegiatan=@pIDkegiatan and IDSUbKegiatan=@pIDSUbKegiatan " +
                    "  and  iidrekening = @pIIDRekening and btKodeUK =@kodeuk and cjumlahABT=0 and cjumlahRKAP is not null";


                DBParameterCollection paramCollection = new DBParameterCollection();


                paramCollection.Add(new DBParameter("@JUMLAHABT", o.JumlahABT));

                paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                paramCollection.Add(new DBParameter("@pIDSUbKegiatan", o.IDSubKegiatan));
                paramCollection.Add(new DBParameter("@kodeuk", o.KodeUK));


            }



            }



        }
        public bool SImpanSIPD (List<TAnggaranRekening> _lst)
        {

            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;
            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSubKegiatan;
            int _KodeSKPD;
            int _KodeUK;

            IDbTransaction transaction = _dbHelper.GetConnObject().BeginTransaction();
            try
            {
                foreach (TAnggaranRekening o in _lst)
                {
                    if (o.KodeUK > 1)
                    {
                        _KodeUK = o.KodeUK;
                    }
                    if (o.Jenis == 3 && o.IDProgram> 0 && o.IDRekening > 0 )
                    {
                        _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 3));
                        _KodeSubKegiatan = DataFormat.GetInteger(o.IDSubKegiatan.ToString().Substring(8, 2));
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                    }
                    else
                    {
                        _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                        _KodeSubKegiatan = 0;
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                        //o.IDUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 3));
                    }

                    _KodeKategori = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                    _KodeUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    _KodeSKPD = DataFormat.GetInteger(o.IDDinas.ToString().Substring(3, 2));

                    //if (o.IDDinas.ToString().Length > 5)
                    //{
                    //    _KodeUK = DataFormat.GetInteger(o.IDDinas.ToString().Substring(5, 2));
                    //}
                    //else
                    //{
                    //    _KodeUK = 0;
                    //}
                    _KodeUK = o.KodeUK;
                    //if (o.IDRekening == 510201010001 && o.IDUnit == 1020110)
                    //{
                    //    o.IDRekening = 510201010001; 
                    //}
                    if (CekAda(o) == 0)
                    {

                        if (o.Tahap == 1)
                        {
                            SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,IDSubKegiatan ,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btIDsubKegiatan,btTahapInput, " +
                                   "IIDRekening, cJumlahMurni,cJumlahRKA,cJumlahGeser, cJumlahRKAP,cDPA,cJumlahABT,cPlafon,bPPKD,idUnit)  values ( " +
                                    "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pIDSubKegiatan ,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                    " @pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtIDsubKegiatan,@pbtTahapInput,@pIIDRekening," +
                                    "@pcJumlahRKAP,@pcJumlahRKAP,@pcJumlahRKAP,@pcJumlahRKAP,@pcDPA,@pcJumlahABT,@pcPlafon,@bppkd,@idUnit)";
                        }


                        if (o.Tahap == 3)
                        {
                            SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,IDSubKegiatan ,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btIDsubKegiatan,btTahapInput, " +
                                   "IIDRekening, cJumlahMurni,cJumlahRKA,cJumlahGeser, cJumlahRKAP,cDPA,cJumlahABT,cPlafon,bPPKD,idUnit)  values ( " +
                                    "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pIDSubKegiatan ,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                    " @pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtIDsubKegiatan,@pbtTahapInput,@pIIDRekening," +
                                    "0,0,@pcJumlahRKAP,@pcJumlahRKAP,@pcJumlahRKAP,@pcJumlahRKAP,@pcPlafon,@bppkd,@idUnit)";
                        }
                        if (o.Tahap == 4)
                        {
                            SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,IDSubKegiatan ,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btIDsubKegiatan,btTahapInput, " +
                                   "IIDRekening, cJumlahMurni,cJumlahRKA,cJumlahGeser, cJumlahRKAP,cDPA,cJumlahABT,cPlafon,bPPKD,idUnit)  values ( " +
                                    "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pIDSubKegiatan ,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                    " @pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtIDsubKegiatan,@pbtTahapInput,@pIIDRekening," +
                                    "0,0,0,@pcJumlahRKAP,@pcJumlahRKAP,@pcJumlahRKAP,@pcPlafon,@bppkd,@idUnit)";
                        }
                        if (o.Tahap == 5)
                        {
                            SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,IDSubKegiatan ," +
                                   "  btKodeuK,btJenis,  " +
                                   "IIDRekening, cJumlahMurni,cJumlahRKA,cJumlahGeser, cJumlahRKAP,cJumlahABT,bPPKD)  values ( " +
                                    "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pIDSubKegiatan ," +
                                    "@pbtKodeuK,@pbtJenis,@pIIDRekening," +
                                    "0,0,0,@pcJumlahRKAP,@pcJumlahRKAP,@bppkd)";
                        }
                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pTahun", o.Tahun));
                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                        paramCollection.Add(new DBParameter("@pIDSubKegiatan", o.IDSubKegiatan));
                        paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK));
                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                    //    paramCollection.Add(new DBParameter("@pbtIDSUbKegiatan", o.SubKegiatan));
                        paramCollection.Add(new DBParameter("@pcJumlahRKAP", o.JumlahRKAP));
                        paramCollection.Add(new DBParameter("@pcJumlahABT", o.JumlahRKAP));
                                    

                        paramCollection.Add(new DBParameter("@bppkd", o.PPKD));
                       
                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    }
                    else
                    {

                        if (o.Tahap == 1)
                        {
                            SSQL = "UPDATE " + m_sNamaTabel + " SET cJumlahRKA= @pcJumlahRKAP ,cJumlahMurni= @pcJumlahRKAP, cJumlahGeser = @pcJumlahRKAP,cJumlahRKAP= @pcJumlahRKAP, cJumlahABT= @pcPlafon, cDPA =@pcDPA,cPlafon=@pcJumlahRKAP WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan and IDSUbKegiatan=@pbtIDSUbKegiatan " +
                             " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and btJenis=@pbtJenis and IDUnit =@pIDUnit and cJumlahRKAP=0";//and isnull(bPPKD,0)=@pbPPKD ";

                        }
                        if (o.Tahap == 3)
                        {
                            SSQL = "UPDATE " + m_sNamaTabel + " SET  cJumlahGeser = @pcJumlahRKAP,cJumlahRKAP= @pcJumlahRKAP, cJumlahABT= @pcPlafon, cDPA =@pcDPA,cPlafon=@pcJumlahRKAP WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan and IDSUbKegiatan=@pbtIDSUbKegiatan " +
                             " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and IDUnit =@pIDUnit ";//and isnull(bPPKD,0)=@pbPPKD ";

                        }

                        
                        if (o.Tahap == 4)
                        {
                            SSQL = "UPDATE " + m_sNamaTabel + " SET cJumlahRKAP= @pcJumlahRKAP, cJumlahABT= @pcPlafon, cDPA =@pcDPA,cPlafon=@pcJumlahRKAP WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan and IDSUbKegiatan=@pbtIDSUbKegiatan " +
                             " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and IDUnit =@pIDUnit and btJenis=@pbtJenis and cJumlahRKAP=0";//and isnull(bPPKD,0)=@pbPPKD ";

                        }
                        if (o.Tahap == 5)
                        {
                            SSQL = "UPDATE " + m_sNamaTabel + " SET  cJumlahABT= @pcPlafon, cDPA =@pcDPA,cPlafon=@pcJumlahRKAP WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan and IDSUbKegiatan=@pbtIDSUbKegiatan " +
                             " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and IDUnit =@pIDUnit and btJenis=@pbtJenis and cJumlahRKAP=0";//and isnull(bPPKD,0)=@pbPPKD ";

                        }


                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pcDPA", o.JumlahRKAP));
                        paramCollection.Add(new DBParameter("@pcJumlahRKAP", o.JumlahRKAP));
                        paramCollection.Add(new DBParameter("@pcJumlahABT", o.JumlahABT));
                        paramCollection.Add(new DBParameter("@pcPlafon", o.JumlahABT));
                        paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                        paramCollection.Add(new DBParameter("@pIDUnit", o.IDUnit));

                        paramCollection.Add(new DBParameter("@pbtIDSUbKegiatan", o.IDSubKegiatan, DbType.Int64));
                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                        //      paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));

                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    }

                }
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _isError = true;
                _lastError = ex.Message;
                return false;


            }
        }
        public bool TempToAnggaranRekening(int jenis)
        {
            try
            {
                // dari tempAnggaranPergeseran 
                if (jenis == 3)
                {
                    List<TAnggaranRekening> ListSumber = new List<TAnggaranRekening>();
                    SSQL = "SELECT 2025 as itahun,IDDINAs, btKOdeUK, IDUrusan, IDPRogram, IDKegiatan, IDSUBKegiatan, IIDREKENING, JUmlah from tempAnggaranPergeseran ";

                        //" AND IDDINas= 4010300";
                    DataTable dt = new DataTable();
                    dt = _dbHelper.ExecuteDataTable(SSQL);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            ListSumber = (from DataRow dr in dt.Rows
                                          select new TAnggaranRekening()
                                          {
                                              IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                              Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                              IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                              IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                              IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                              KodeUK = 1,//DataFormat.GetInteger(dr["btkodeuk"]),
                                              KodeKegiatan = DataFormat.GetInteger(dr["IDUrusan"]),
                                              IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                              IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                              Jumlah = DataFormat.GetDecimal(dr["Jumlah"]),
                                              PPKD = 0,//DataFormat.GetSingle(dr["bPPKD"]),
                                              Jenis = 3,//DataFormat.GetInteger(dr["btJenis"]),
                                              Tahap = 3,
                                          }).ToList();
                        }
                    }

                    SImpanSIPDDariTemp(ListSumber);
                }
                else
                {
                    List<TAnggaranRekening> ListSumber = new List<TAnggaranRekening>();
                    SSQL = "SELECT 2025 as itahun,IDDINAs, btKOdeUK, IDUrusan, IDPRogram, IDKegiatan, IDSUBKegiatan, IIDREKENING, JUmlah from tempAnggaranPergeseran " +
                          " where iidrekening like '4%'";
                    DataTable dt = new DataTable();
                    dt = _dbHelper.ExecuteDataTable(SSQL);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            ListSumber = (from DataRow dr in dt.Rows
                                          select new TAnggaranRekening()
                                          {
                                              IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                              Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                              IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                              IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                              IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                              KodeUK = DataFormat.GetInteger(dr["btkodeuk"]),
                                              KodeKegiatan = DataFormat.GetInteger(dr["IDUrusan"]),
                                              IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                              IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                              Jumlah = DataFormat.GetDecimal(dr["Jumlah"]),
                                              PPKD = 0,//DataFormat.GetSingle(dr["bPPKD"]),
                                              Jenis = 1,//DataFormat.GetInteger(dr["btJenis"]),
                                              Tahap = 3,
                                          }).ToList();
                        }
                    }
                    SImpanSIPDDariTemp(ListSumber);

                }

                return true;
            } catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }

        }
        public bool SImpanSIPDDariTemp(List<TAnggaranRekening> _lst)
        {

            IDbTransaction transaction = _dbHelper.GetConnObject().BeginTransaction();
            try
            {
                m_sNamaTabel = "tAnggaranRekening_A";
                foreach (TAnggaranRekening o in _lst)
                {
                    if (CekAda(o) == 0)
                    {

                        if (o.Tahap == 1)
                        {
                            SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,IDSubKegiatan ,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btIDsubKegiatan,btTahapInput, " +
                                   "IIDRekening, cJumlahMurni,cJumlahRKA,cJumlahGeser, cJumlahRKAP,cDPA,cJumlahABT,cPlafon,bPPKD,idUnit)  values ( " +
                                    "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pIDSubKegiatan ,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                    " @pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtIDsubKegiatan,@pbtTahapInput,@pIIDRekening," +
                                    "@pcJumlahRKAP,@pcJumlahRKAP,@pcJumlahRKAP,@pcJumlahRKAP,@pcDPA,@pcJumlahABT,@pcPlafon,@bppkd,@idUnit)";
                        }


                        if (o.Tahap == 3)
                        {
                            SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,btkodeuk,IDProgram, IDkegiatan,IDUrusan,IDSubKegiatan , " +
                                   "IIDRekening, cJumlahMurni,cJumlahGeser, cJumlahRKAP,cJumlahABT,bPPKD,btJenis)  values ( " +
                                   " @piTahun,@pIDDInas,@pbtKodeuK,@pIDProgram, @pIDkegiatan,@pIDUrusan,@pIDSubKegiatan , " +
                                   "@pIIDRekening, 0,@pcJumlahGeser, @pcJumlahRKAP,@pcJumlahABT,0,@pJenis)";
                        }
                        if (o.Tahap == 4)
                        {
                            SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,IDSubKegiatan ,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btIDsubKegiatan,btTahapInput, " +
                                   "IIDRekening, cJumlahMurni,cJumlahRKA,cJumlahGeser, cJumlahRKAP,cDPA,cJumlahABT,cPlafon,bPPKD,idUnit)  values ( " +
                                    "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pIDSubKegiatan ,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                    " @pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtIDsubKegiatan,@pbtTahapInput,@pIIDRekening," +
                                    "0,0,0,@pcJumlahRKAP,@pcJumlahRKAP,@pcJumlahRKAP,@pcPlafon,@bppkd,@idUnit)";
                        }
                        if (o.Tahap == 5)
                        {
                            SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,IDSubKegiatan ," +
                                   "  btKodeuK,btJenis,  " +
                                   "IIDRekening, cJumlahMurni,cJumlahRKA,cJumlahGeser, cJumlahRKAP,cJumlahABT,bPPKD)  values ( " +
                                    "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pIDSubKegiatan ," +
                                    "@pbtKodeuK,@pbtJenis,@pIIDRekening," +
                                    "0,0,0,@pcJumlahRKAP,@pcJumlahRKAP,@bppkd)";
                        }

                        //SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,IDSubKegiatan , " +
                        //         "IIDRekening, cJumlahMurni,cJumlahGeser, cJumlahRKAP,cJumlahABT,bPPKD,btJenis)  values ( " +
                        //         " @piTahun,@pIDDInas,@pIDProgram, @pIDkegiatan,@pIDUrusan,@pIDSubKegiatan , " +
                        //         "@pIIDRekening, 0,@pcJumlahGeser, @pcJumlahRKAP,@pcJumlahABT,0,@pJenis)";
                    
                    DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                        paramCollection.Add(new DBParameter("@pIDSubKegiatan", o.IDSubKegiatan));
                        paramCollection.Add(new DBParameter("@pbtKodeuK", o.KodeUK));
                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                        //    paramCollection.Add(new DBParameter("@pbtIDSUbKegiatan", o.SubKegiatan));
                        paramCollection.Add(new DBParameter("@pcJumlahRKAP", o.Jumlah));
                        paramCollection.Add(new DBParameter("@pcJumlahGeser", o.Jumlah));
                        paramCollection.Add(new DBParameter("@pcJumlahABT", o.JumlahRKAP));
                        paramCollection.Add(new DBParameter("@pJenis", o.Jenis));
                        

                        paramCollection.Add(new DBParameter("@bppkd", o.PPKD));

                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    }
                    else
                    {

                        //if (o.Tahap == 1)
                        //{
                        //    SSQL = "UPDATE " + m_sNamaTabel + " SET cJumlahRKA= @pcJumlahRKAP ,cJumlahMurni= @pcJumlahRKAP, cJumlahGeser = @pcJumlahRKAP,cJumlahRKAP= @pcJumlahRKAP, cJumlahABT= @pcPlafon, cDPA =@pcDPA,cPlafon=@pcJumlahRKAP WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan and IDSUbKegiatan=@pbtIDSUbKegiatan " +
                        //     " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and btJenis=@pbtJenis and IDUnit =@pIDUnit and cJumlahRKAP=0";//and isnull(bPPKD,0)=@pbPPKD ";

                        //}
                        if (o.Tahap == 3)
                        {
                            SSQL = "UPDATE tANggaranRekening_A SET  cJumlahGeser = @pcJumlahGeser,cJumlahRKAP= @pcJumlahRKAP, cJumlahABT= @pcJumlahABT " +
                                " WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan and IDSUbKegiatan=@pIDSUbKegiatan " +
                             " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and btKodeuk=@pKodeuK";


                        }

                        if (o.Tahap == 4)
                        {
                            SSQL = "UPDATE " + m_sNamaTabel + " SET cJumlahRKAP= @pcJumlahRKAP, cJumlahABT= @pcPlafon, cDPA =@pcDPA,cPlafon=@pcJumlahRKAP WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan and IDSUbKegiatan=@pbtIDSUbKegiatan " +
                             " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and IDUnit =@pIDUnit and btJenis=@pbtJenis and cJumlahRKAP=0";//and isnull(bPPKD,0)=@pbPPKD ";

                        }
                        if (o.Tahap == 5)
                        {
                            SSQL = "UPDATE " + m_sNamaTabel + " SET  cJumlahABT= @pcPlafon, cDPA =@pcDPA,cPlafon=@pcJumlahRKAP WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan and IDSUbKegiatan=@pbtIDSUbKegiatan " +
                             " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and IDUnit =@pIDUnit and btJenis=@pbtJenis and cJumlahRKAP=0";//and isnull(bPPKD,0)=@pbPPKD ";

                        }


                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                        paramCollection.Add(new DBParameter("@pIDSUbKegiatan", o.IDSubKegiatan));
                        paramCollection.Add(new DBParameter("@pKodeuK", o.KodeUK));
                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                        //    paramCollection.Add(new DBParameter("@pbtIDSUbKegiatan", o.SubKegiatan));
                        paramCollection.Add(new DBParameter("@pcJumlahRKAP", o.Jumlah));
                        paramCollection.Add(new DBParameter("@pcJumlahGeser", o.Jumlah));
                        paramCollection.Add(new DBParameter("@pcJumlahABT", o.JumlahRKAP));

                        //SSQL = "UPDATE " + m_sNamaTabel + " SET  cJumlahGeser = @pcJumlahGeser,cJumlahRKAP= @pcJumlahRKAP, cJumlahABT= @pcJumlahABT " +
                        //        " WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan and IDSUbKegiatan=@pIDSUbKegiatan " +
                        //     " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and btKodeuk=@btKodeuk";

                        paramCollection.Add(new DBParameter("@bppkd", o.PPKD));

                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                        
                    }

                }
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _isError = true;
                _lastError = ex.Message;
                return false;


            }
        }
        public bool SImpanSIPDTest(List<TAnggaranRekening> _lst)
        {

            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;
            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSubKegiatan;
            int _KodeSKPD;
            int _KodeUK;
            m_sNamaTabel = "tAnggaranRekening_ATest";
            IDbTransaction transaction = _dbHelper.GetConnObject().BeginTransaction();
            // SSQL = "UPDATE TANggaranRekening_A set cJumlahGeser=0 where iddinas = 1020100";
            //_dbHelper.ExecuteNonQuery(SSQL);
            int i = 0;
            try
            {
                int j = 0;
                foreach (TAnggaranRekening o in _lst)
                {
                    if (j== 253)
                    {
                        j = j;
                    }
                    j++;
                    if (o.KodeUK > 1)
                    {
                        _KodeUK = o.KodeUK;
                    }
                    if (o.Jenis == 3 && o.IDProgram > 0 && o.IDRekening > 0)
                    {
                        _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 3));
                        _KodeSubKegiatan = DataFormat.GetInteger(o.IDSubKegiatan.ToString().Substring(8, 2));
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                    }
                    else
                    {
                        _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                        _KodeSubKegiatan = 0;
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                        //o.IDUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 3));
                    }

                    _KodeKategori = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                    _KodeUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    _KodeSKPD = DataFormat.GetInteger(o.IDDinas.ToString().Substring(3, 2));

                    //if (o.IDDinas.ToString().Length > 5)
                    //{
                    //    _KodeUK = DataFormat.GetInteger(o.IDDinas.ToString().Substring(5, 2));
                    //}
                    //else
                    //{
                    //    _KodeUK = 0;
                    //}
                    _KodeUK = o.KodeUK;
                    //if (o.IDRekening == 510201010001 && o.IDUnit == 1020110)
                    //{
                    //    o.IDRekening = 510201010001; 
                    //}
                    //if (CekAda(o) == 0)
                    //{
                    i++;
                        if (o.Tahap == 1)
                        {
                            SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,IDSubKegiatan ,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btIDsubKegiatan,btTahapInput, " +
                                   "IIDRekening, cJumlahMurni,cJumlahRKA,cJumlahGeser, cJumlahRKAP,cDPA,cJumlahABT,cPlafon,bPPKD,idUnit)  values ( " +
                                    "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pIDSubKegiatan ,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                    " @pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtIDsubKegiatan,@pbtTahapInput,@pIIDRekening," +
                                    "@pcJumlahRKAP,@pcJumlahRKAP,@pcJumlahRKAP,@pcJumlahRKAP,@pcDPA,@pcJumlahABT,@pcPlafon,@bppkd,@idUnit)";
                        }


                        if (o.Tahap == 3)
                        {
                            SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,IDSubKegiatan ,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btIDsubKegiatan,btTahapInput, " +
                                   "IIDRekening, cJumlahMurni,cJumlahRKA,cJumlahGeser, cJumlahRKAP,cDPA,cJumlahABT,cPlafon,bPPKD,idUnit)  values ( " +
                                    "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pIDSubKegiatan ,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                    " @pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtIDsubKegiatan,@pbtTahapInput,@pIIDRekening," +
                                    "0,0,@pcJumlahRKAP,@pcJumlahRKAP,@pcJumlahRKAP,@pcJumlahRKAP,@pcPlafon,@bppkd,@idUnit)";
                        }
                        if (o.Tahap == 4)
                        {
                            SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,IDSubKegiatan ,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btIDsubKegiatan,btTahapInput, " +
                                   "IIDRekening, cJumlahMurni,cJumlahRKA,cJumlahGeser, cJumlahRKAP,cDPA,cJumlahABT,cPlafon,bPPKD,idUnit)  values ( " +
                                    "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pIDSubKegiatan ,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                    " @pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtIDsubKegiatan,@pbtTahapInput,@pIIDRekening," +
                                    "0,0,0,@pcJumlahRKAP,@pcJumlahRKAP,@pcJumlahRKAP,@pcPlafon,@bppkd,@idUnit)";
                        }
                        if (o.Tahap == 5)
                        {
                            SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,IDSubKegiatan ,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btIDsubKegiatan,btTahapInput, " +
                                   "IIDRekening, cJumlahMurni,cJumlahRKA,cJumlahGeser, cJumlahRKAP,cDPA,cJumlahABT,cPlafon,bPPKD,idUnit)  values ( " +
                                    "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pIDSubKegiatan ,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                    " @pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtIDsubKegiatan,@pbtTahapInput,@pIIDRekening," +
                                    "0,0,0,0,0,@pcJumlahABT,@pcPlafon,@bppkd,@idUnit)";
                        }
                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pTahun", o.Tahun));
                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                        paramCollection.Add(new DBParameter("@pIDSubKegiatan", o.IDSubKegiatan));
                        paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana));
                        paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana));
                        paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori));
                        paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan));
                        paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD));
                        paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK));
                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                        paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram));
                        paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan));
                        paramCollection.Add(new DBParameter("@pbtIDsubKegiatan", _KodeSubKegiatan));
                        paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput));
                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                        //    paramCollection.Add(new DBParameter("@pbtIDSUbKegiatan", o.SubKegiatan));
                        //paramCollection.Add(new DBParameter("@pcJumlahRKAP", o.JumlahRKAP));
                        //paramCollection.Add(new DBParameter("@pcDPA", o.JumlahRKAP));
                        paramCollection.Add(new DBParameter("@pcJumlahABT", o.JumlahRKAP));
                        paramCollection.Add(new DBParameter("@pcPlafon", o.JumlahRKAP));


                        paramCollection.Add(new DBParameter("@bppkd", o.PPKD));
                        paramCollection.Add(new DBParameter("@idUnit", o.IDUnit));

                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    //}
                    //else
                    //{

                    //    if (o.Tahap == 1)
                    //    {
                    //        SSQL = "UPDATE " + m_sNamaTabel + " SET cJumlahRKA= @pcJumlahRKAP ,cJumlahMurni= @pcJumlahRKAP, cJumlahGeser = @pcJumlahRKAP,cJumlahRKAP= @pcJumlahRKAP, cJumlahABT= @pcPlafon, cDPA =@pcDPA,cPlafon=@pcJumlahRKAP WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan and IDSUbKegiatan=@pbtIDSUbKegiatan " +
                    //         " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and btJenis=@pbtJenis and IDUnit =@pIDUnit and cJumlahRKAP=0";//and isnull(bPPKD,0)=@pbPPKD ";

                    //    }
                    //    if (o.Tahap == 3)
                    //    {
                    //        SSQL = "UPDATE " + m_sNamaTabel + " SET  cJumlahGeser = @pcJumlahRKAP,cJumlahRKAP= @pcJumlahRKAP, cJumlahABT= @pcPlafon, cDPA =@pcDPA,cPlafon=@pcJumlahRKAP WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan and IDSUbKegiatan=@pbtIDSUbKegiatan " +
                    //         " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and IDUnit =@pIDUnit ";//and isnull(bPPKD,0)=@pbPPKD ";

                    //    }


                    //    if (o.Tahap == 4)
                    //    {
                    //        SSQL = "UPDATE " + m_sNamaTabel + " SET cJumlahRKAP= @pcJumlahRKAP, cJumlahABT= @pcPlafon, cDPA =@pcDPA,cPlafon=@pcJumlahRKAP WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan and IDSUbKegiatan=@pbtIDSUbKegiatan " +
                    //         " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and IDUnit =@pIDUnit and btJenis=@pbtJenis and cJumlahRKAP=0";//and isnull(bPPKD,0)=@pbPPKD ";

                    //    }
                    //    if (o.Tahap == 5)
                    //    {
                    //        SSQL = "UPDATE " + m_sNamaTabel + " SET  cJumlahABT= @pcJumlahRKAP, cDPA =@pcJumlahRKAP,cPlafon=@pcJumlahRKAP WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan and IDSUbKegiatan=@pbtIDSUbKegiatan " +
                    //         " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and IDUnit =@pIDUnit and btJenis=@pbtJenis and cJumlahRKAP=0";//and isnull(bPPKD,0)=@pbPPKD ";

                    //    }


                    //    DBParameterCollection paramCollection = new DBParameterCollection();
                    //    paramCollection.Add(new DBParameter("@pcDPA", o.JumlahRKAP));
                    //    paramCollection.Add(new DBParameter("@pcJumlahRKAP", o.JumlahRKAP));
                    //    paramCollection.Add(new DBParameter("@pcJumlahABT", o.JumlahABT));
                    // //   paramCollection.Add(new DBParameter("@pcPlafon", o.JumlahABT));
                    //    paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                    //    paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                    //    paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                    //    paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                    //    paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                    //    paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                    //    paramCollection.Add(new DBParameter("@pIDUnit", o.IDUnit));

                    //    paramCollection.Add(new DBParameter("@pbtIDSUbKegiatan", o.IDSubKegiatan, DbType.Int64));
                    //    paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                    //    //      paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));

                    //    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    //}

                }
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _isError = true;
                _lastError = ex.Message;
                return false;


            }
        }
        public bool SimpanSIPDTemp(List<TAnggaranRekening> _lst, int jenis)
        {

            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;
            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSubKegiatan;
            int _KodeSKPD;
            int _KodeUK;
            m_sNamaTabel = "tAnggaranRekening_SIPD";
            IDbTransaction transaction = _dbHelper.GetConnObject().BeginTransaction();
            SSQL = "DELETE FROM tAnggaranRekening_SIPD ";

            _dbHelper.ExecuteNonQuery(SSQL);

            int i = 0;
            try
            {
                
                int j = 0;
                foreach (TAnggaranRekening o in _lst)
                {



                    SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,btKodeUK,iIDRekening,cJumlah,btJenis,IDDInas,IDUrusan,IDProgram," +
                            "IDKegiatan,IDSubKegiatan,Kodesumberdana,namasumberdana) values(" +
                            "@pTahun, @pbtKodeUK, @piIDRekening, @pcJumlah, @pbtJenis, @pIDDInas, @pIDUrusan, " +
                            "@pIDProgram, @pIDKegiatan, @pIDSubKegiatan, @pKodesumberdana, @pnamasumberdana)";


                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pTahun", o.Tahun));
                    paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                    paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                    paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                    paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                    paramCollection.Add(new DBParameter("@pIDSubKegiatan", o.IDSubKegiatan));
                    paramCollection.Add(new DBParameter("@pbtKodeuK", o.KodeUK));
                    paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                    paramCollection.Add(new DBParameter("@pcJumlah", o.JumlahRKA));
                    paramCollection.Add(new DBParameter("@pKodesumberdana", o.KodeSumberDana));
                    paramCollection.Add(new DBParameter("@pnamasumberdana", o.NamaSumberDana));




                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    
                }
                SSQL = "Delete  from  tempAnggaranPergeseran ";
                _dbHelper.ExecuteNonQuery(SSQL);
                
                    SSQL = "DROP TABLE tempAnggaranPergeseran";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "select iTahun,IDDINAS, btKODEUK, IDUrusan, idprogram, idKegiatan, idsubkegiatan,iidrekening ,SUM (cJumlah) as Jumlah into tempAnggaranPergeseran from tAnggaranRekening_SIPD where btjenis=" + jenis.ToString() + "group by iTahun,IDDINAS, btKODEUK, IDUrusan, idprogram, idKegiatan, idsubkegiatan,iidrekening ";
                _dbHelper.ExecuteNonQuery(SSQL);


                transaction.Commit();
                if (TempToAnggaranRekening(jenis)== true)
                {
                    _isError = false;
                    return true;
                }
                else
                {
                    _isError = true;
                    _lastError = "Kesalahan memasukkan dari table temporary.";
                    return false;
                }
                    
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _isError = true;
                _lastError = ex.Message;
                return false;


            }
        }
        public bool SimpanSumberDana(List<TAnggaranRekening> _lst)
        {

            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;
            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSubKegiatan;
            int _KodeSKPD;
            int _KodeUK;

            IDbTransaction transaction = _dbHelper.GetConnObject().BeginTransaction();

            try
            {
                foreach (TAnggaranRekening o in _lst)
                {
                    
                    
                    

                        SSQL = "UPDATE " + m_sNamaTabel + " SET sSumberDana = @SumberDana WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan and IDSUbKegiatan=@pbtIDSUbKegiatan " +
                             " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening ";

                    
                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pcDPA", o.JumlahRKAP));
                        paramCollection.Add(new DBParameter("@pcJumlahRKAP", o.JumlahRKAP));
                        paramCollection.Add(new DBParameter("@pcJumlahABT", o.JumlahRKAP));
                        paramCollection.Add(new DBParameter("@pcPlafon", o.JumlahRKAP));
                        paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                        paramCollection.Add(new DBParameter("@pbtIDSUbKegiatan", o.IDSubKegiatan, DbType.Int64));
                        paramCollection.Add(new DBParameter("@SumberDana", o.SumberDana));
    
                    
                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    

                }
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _isError = true;
                _lastError = ex.Message;
                return false;


            }
        }
        public List<RekapAnggaran> GetRekeningGajiAllLevel(int _pTahun, int _IDDinas, int _jenis, Single _pTahap )
        {
            string sKolom = GetKolomx(_pTahap);

            List<RekapAnggaran> _lst = new List<RekapAnggaran>();
            try
            {
                SetProfileRekening(mprofile);
                SSQL = "";
                if (Tahun <= 2020)
                {
                    SSQL = SSQL +
                    " Select 2 as Level, B.IDDInas  AS IDDinas, A.IIDParent as IDUrusan, B.IDProgram  as IDProgram,B.IDkegiatan as IDkegiatan,  A.IIDRekening as IDRekening, LEFT(B.IIDRekening," + m_ProfileRekening.LEN3.ToString() + " ) as Rek,  " +
                    " A.sNamaRekening AS Nama,B.btJenis," +
                    " SUM(B." + sKolom + ")AS JumlahOlah, " +
                    " SUM(B." + sKolom + ") AS Jumlah , " +
                    " SUM(B." + sKolom + ")  AS JumlahMurni , 0 as idsubkegiatan from mRekening  A " +
                    " INNER JOIN tANGGARANREKENING_A B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + " )= LEFT(B.IIDRekening," + m_ProfileRekening.LEN3.ToString() + " )  " + " AND B.btJenis= " + _jenis.ToString() +
                    " WHERE A.btRoot= 3 and B.iTahun =" + _pTahun.ToString() + " AND B.IDDInas=" + _IDDinas.ToString() + " AND B.btJenis= 2 AND B.IIDRekening like '511%' " +
                    " AND isnull(B.bPPKD,0)=0 GROUP BY B.IDDInas  , A.IIDParent  , B.IDProgram  ,B.IDkegiatan , A.IIDRekening , LEFT(B.IIDRekening," + m_ProfileRekening.LEN3.ToString() + " ), A.sNamaRekening,B.btJenis  ";

                    SSQL = SSQL + "  UNION ALL " +
                    " Select 3 as Level, B.IDDInas  AS IDDinas, A.IIDParent as IDUrusan, B.IDProgram  as IDProgram,B.IDkegiatan as IDkegiatan,  A.IIDRekening as IDRekening,  LEFT(B.IIDRekening," + m_ProfileRekening.LEN4.ToString() + " ) as Rek,  " +
                    " A.sNamaRekening AS Nama,B.btJenis," +
                    " SUM(B." + sKolom + ")AS JumlahOlah, " +
                    " SUM(B." + sKolom + ") AS Jumlah , " +
                    " SUM(B." + sKolom + ")  AS JumlahMurni, 0 as idsubkegiatan  from mRekening  A " +
                    " INNER JOIN tANGGARANREKENING_A B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN4.ToString() + ")= LEFT(B.IIDRekening," + m_ProfileRekening.LEN4.ToString() + ")  " + " AND B.btJenis= 2 " +
                    " WHERE A.btRoot=4 and B.iTahun =" + _pTahun.ToString() + " AND B.IDDInas=" + _IDDinas.ToString() + "  AND B.btJenis= 2  AND B.IIDRekening like '511%'  " +
                    " AND isnull(B.bPPKD,0)=0  GROUP BY B.IDDInas  , A.IIDParent  , B.IDProgram  ,B.IDkegiatan , A.IIDRekening , LEFT(B.IIDRekening," + m_ProfileRekening.LEN4.ToString() + "),A.sNamaRekening,B.btJenis  ";

                    SSQL = SSQL + "  UNION ALL " +
                    " Select 4 as Level, B.IDDInas  AS IDDinas, A.IIDParent as IDUrusan, B.IDProgram  as IDProgram,B.IDkegiatan as IDkegiatan,   LEFT(B.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ") as Rek, A.IIDRekening as IDRekening,  " +
                    " A.sNamaRekening AS Nama,B.btJenis," +
                    " SUM(B." + sKolom + ")AS JumlahOlah, " +
                    " SUM(B." + sKolom + ") AS Jumlah , " +
                    " SUM(B." + sKolom + ")  AS JumlahMurni , 0 as idsubkegiatan  from mRekening  A " +
                    " INNER JOIN tANGGARANREKENING_A B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ")= LEFT(B.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ")  " + " AND B.btJenis= 2 " +
                    " WHERE A.btRoot = 5 and B.iTahun =" + _pTahun.ToString() + " AND B.IDDInas=" + _IDDinas.ToString() + " AND B.btJenis= 2  AND B.IIDRekening like '511%'  " +
                    " AND isnull(B.bPPKD,0)=0  GROUP BY B.IDDInas  , A.IIDParent  , B.IDProgram  ,B.IDkegiatan , A.IIDRekening ,  LEFT(B.IIDRekening," + m_ProfileRekening.LEN5.ToString() + "), A.sNamaRekening,B.btJenis  " +
                    " ORDER BY IDDInas  , IDUrusan , IDProgram  ,IDkegiatan , IDRekening";
                }
                else
                {
                    SSQL = SSQL +
                " Select 1 as Level, B.IDDInas  AS IDDinas, A.IIDParent as IDUrusan, B.IDProgram  as IDProgram,B.IDkegiatan as IDkegiatan,B.IDSubKegiatan,  A.IIDRekening as IDRekening, LEFT(B.IIDRekening," + m_ProfileRekening.LEN3.ToString() + " ) as Rek,  " +
                " A.sNamaRekening AS Nama,B.btJenis," +
                " SUM(B." + sKolom + ")AS JumlahOlah, " +
                " SUM(B." + sKolom + ") AS Jumlah , " +
                " SUM(B." + sKolom + ")  AS JumlahMurni  from mRekening  A " +
                " INNER JOIN tANGGARANREKENING_A B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + " )= LEFT(B.IIDRekening," + m_ProfileRekening.LEN3.ToString() + " )  " +
                " WHERE B.idsubkegiatan % 10000000 =0120201 and  A.btRoot= 3 and B.iTahun =" + _pTahun.ToString() + " AND B.IDDInas=" + _IDDinas.ToString() + " " +
                " AND isnull(B.bPPKD,0)=0 GROUP BY B.IDDInas  , A.IIDParent  , B.IDProgram  ,B.IDkegiatan ,B.idsubkegiatan , A.IIDRekening , LEFT(B.IIDRekening," + m_ProfileRekening.LEN3.ToString() + " ), A.sNamaRekening,B.btJenis  ";

                    SSQL = SSQL + "  UNION ALL " +
                    " Select 2 as Level, B.IDDInas  AS IDDinas, A.IIDParent as IDUrusan, B.IDProgram  as IDProgram,B.IDkegiatan as IDkegiatan,B.IDSubKegiatan,  A.IIDRekening as IDRekening,  LEFT(B.IIDRekening," + m_ProfileRekening.LEN4.ToString() + " ) as Rek,  " +
                    " A.sNamaRekening AS Nama,B.btJenis," +
                    " SUM(B." + sKolom + ")AS JumlahOlah, " +
                    " SUM(B." + sKolom + ") AS Jumlah , " +
                    " SUM(B." + sKolom + ")  AS JumlahMurni  from mRekening  A " +
                    " INNER JOIN tANGGARANREKENING_A B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN4.ToString() + ")= LEFT(B.IIDRekening," + m_ProfileRekening.LEN4.ToString() + ")  " + " " +
                    " WHERE B.idsubkegiatan % 10000000 =0120201 and  A.btRoot=4 and B.iTahun =" + _pTahun.ToString() + " AND B.IDDInas=" + _IDDinas.ToString() + "  " +
                    " AND isnull(B.bPPKD,0)=0  GROUP BY B.IDDInas  , A.IIDParent  , B.IDProgram  ,B.IDkegiatan ,  B.idsubkegiatan ,A.IIDRekening , LEFT(B.IIDRekening," + m_ProfileRekening.LEN4.ToString() + "),A.sNamaRekening,B.btJenis  ";

                    SSQL = SSQL + "  UNION ALL " +
                    " Select 3 as Level, B.IDDInas  AS IDDinas, A.IIDParent as IDUrusan, B.IDProgram  as IDProgram,B.IDkegiatan as IDkegiatan,B.IDSubKegiatan,  A.IIDRekening as IDRekening,  LEFT(B.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ") as Rek,  " +
                    " A.sNamaRekening AS Nama,B.btJenis," +
                    " SUM(B." + sKolom + ")AS JumlahOlah, " +
                    " SUM(B." + sKolom + ") AS Jumlah , " +
                    " SUM(B." + sKolom + ")  AS JumlahMurni  from mRekening  A " +
                    " INNER JOIN tANGGARANREKENING_A B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ")= LEFT(B.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ")  " + "  " +
                    " WHERE B.idsubkegiatan % 10000000 =0120201 and  A.btRoot = 5 and B.iTahun =" + _pTahun.ToString() + " AND B.IDDInas=" + _IDDinas.ToString() + " " +
                    " AND isnull(B.bPPKD,0)=0  GROUP BY B.IDDInas  , A.IIDParent  , B.IDProgram  ,B.IDkegiatan ,B.IDSubKegiatan, A.IIDRekening ,  LEFT(B.IIDRekening," + m_ProfileRekening.LEN5.ToString() + "), A.sNamaRekening,B.btJenis  ";

                    SSQL = SSQL + "  UNION ALL " +
                    " Select 4 as Level, B.IDDInas  AS IDDinas, A.IIDParent as IDUrusan, B.IDProgram  as IDProgram,B.IDkegiatan as IDkegiatan ,B.IDSubKegiatan,   LEFT(B.IIDRekening," + m_ProfileRekening.LEN6.ToString() + ") as Rek, A.IIDRekening as IDRekening,  " +
                    " A.sNamaRekening AS Nama,B.btJenis," +
                    " SUM(B." + sKolom + ")AS JumlahOlah, " +
                    " SUM(B." + sKolom + ") AS Jumlah , " +
                    " SUM(B." + sKolom + ")  AS JumlahMurni  from mRekening  A " +
                    " INNER JOIN tANGGARANREKENING_A B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN6.ToString() + ")= LEFT(B.IIDRekening," + m_ProfileRekening.LEN6.ToString() + ")  " + " " +
                    " WHERE B.idsubkegiatan % 10000000 =0120201 and  A.btRoot = 6 and B.iTahun =" + _pTahun.ToString() + " AND B.IDDInas=" + _IDDinas.ToString() + " " +
                    " AND isnull(B.bPPKD,0)=0  GROUP BY B.IDDInas  , A.IIDParent  , B.IDProgram  ,B.IDkegiatan ,B.IDSubKegiatan, A.IIDRekening ,  LEFT(B.IIDRekening," + m_ProfileRekening.LEN6.ToString() + "), A.sNamaRekening,B.btJenis  " +
                    
                    " ORDER BY IDDInas  ,  IDProgram  ,IDkegiatan ,IDSUbKegiatan, IDRekening";

                }

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RekapAnggaran()
                                {

                                    Level = DataFormat.GetInteger(dr["Level"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Kode = GetKodeSPDBTL(DataFormat.GetInteger(dr["Level"]),
                                            DataFormat.GetInteger(dr["IDDInas"]),
                                            DataFormat.GetInteger(dr["IDUrusan"]),
                                            DataFormat.GetInteger(dr["IDProgram"]),
                                            DataFormat.GetInteger(dr["IDKegiatan"]),
                                            DataFormat.GetLong(dr["IDRekening"])),
                                    IDUrusan = DataFormat.GetLong(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                     IDSubKegiatan= DataFormat.GetLong(dr["idsubkegiatan"]),
                                    IDRekening = DataFormat.GetLong(dr["IDRekening"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Jumlah = DataFormat.GetDecimal(dr["JumlahMurni"])!= DataFormat.GetDecimal(dr["JumlahOlah"])? DataFormat.GetDecimal(dr["JumlahOlah"]) : DataFormat.GetDecimal(dr["JumlahMurni"]),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]),
                                    JumlahOlah = DataFormat.GetDecimal(dr["JumlahOlah"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    Tahap=_pTahap

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
        private string   GetKolomx(Single _pTahap)
        {
            string sKolom = "cJumlahMurni";

            switch ((int)_pTahap)
            {
                case 0:
                    sKolom = "cJumlahMurni";
                    break;
                case 1:
                    sKolom = "cJumlahGeser";
                    break;
                case 2:
                    sKolom = "cJumlahRKAP";
                    break;
                case 3:
                    sKolom = "cJumlahABT";
                    break;


            }
            return sKolom;

        }

        public List<RekapAnggaran> GetRekeningPPKDAllLevel(int _pTahun, int _IDDinas, int _jenis, Single _pTahap )
        {

            string sKolom = GetKolomx(_pTahap);
            SetProfileRekening(mprofile);
            List<RekapAnggaran> _lst = new List<RekapAnggaran>();
            try
            {
                
                
                SSQL = "";

                SSQL = SSQL +

                 "Select 1 as Level, B.IDDInas  AS IDDinas, A.IIDParent as IDUrusan, 0 as IDProgram,0 as IDkegiatan,0 as IDSUbKegiatan, 0 as btIDSubkegiatan,  " +
                 "LEFT(B.IIDRekening,1) as Rek, A.IIDRekening as IDRekening,   A.sNamaRekening AS Nama,B.btJenis, SUM(B." + sKolom + " )AS JumlahOlah,  " +
                  "SUM(B." + sKolom + ") AS Jumlah ,  SUM(B." + sKolom + " )  AS JumlahMurni  from mRekening  A  INNER JOIN tANGGARANREKENING_A B ON " +
                " LEFT(A.IIDRekening,1)= LEFT(B.IIDRekening,1)   WHERE A.btRoot = 1 and B.iTahun =" + _pTahun.ToString() + " AND B.IDDInas=" + _IDDinas.ToString() + " and b.IIDRekening like '62%'  " +
                    " GROUP BY B.IDDInas  , A.IIDParent  , A.IIDRekening ,LEFT(B.IIDRekening,1),A.sNamaRekening,B.btJenis";


                SSQL = SSQL + " UNION Select 2 as Level, B.IDDInas  AS IDDinas, A.IIDParent as IDUrusan, 0 as IDProgram,0 as IDkegiatan,0 as IDSUbKegiatan, 0 as btIDSubkegiatan,  " +
                "LEFT(B.IIDRekening,1) as Rek, A.IIDRekening as IDRekening,   A.sNamaRekening AS Nama,B.btJenis, SUM(B." + sKolom + " )AS JumlahOlah,  " +
                 "SUM(B." + sKolom + ") AS Jumlah ,  SUM(B." + sKolom + " )  AS JumlahMurni  from mRekening  A  INNER JOIN tANGGARANREKENING_A B ON " +
               " LEFT(A.IIDRekening,1)= LEFT(B.IIDRekening,1)   WHERE A.btRoot = 1 and B.iTahun =" + _pTahun.ToString() + " AND B.IDDInas=" + _IDDinas.ToString() + " and b.IIDRekening like '62%'  " +
                   " GROUP BY B.IDDInas  , A.IIDParent  , A.IIDRekening ,LEFT(B.IIDRekening,1),A.sNamaRekening,B.btJenis";


      


                    SSQL=SSQL + " UNION Select 3 as Level, B.IDDInas  AS IDDinas, A.IIDParent as IDUrusan, 0 as IDProgram,0 as IDkegiatan,0 as IDSUbKegiatan, 0 as btIDSubkegiatan,  " +
                 "LEFT(B.IIDRekening,2) as Rek, A.IIDRekening as IDRekening,   A.sNamaRekening AS Nama,B.btJenis, SUM(B." + sKolom + " )AS JumlahOlah,  " +
                  "SUM(B." + sKolom + ") AS Jumlah ,  SUM(B." + sKolom + " )  AS JumlahMurni  from mRekening  A  INNER JOIN tANGGARANREKENING_A B ON " +  
                " LEFT(A.IIDRekening,2)= LEFT(B.IIDRekening,2)   WHERE A.btRoot = 2 and B.iTahun =" + _pTahun.ToString() + " AND B.IDDInas=" + _IDDinas.ToString() + " and b.IIDRekening like '62%'  " + 
                    " GROUP BY B.IDDInas  , A.IIDParent  , A.IIDRekening ,LEFT(B.IIDRekening,2),A.sNamaRekening,B.btJenis";


                SSQL = SSQL +
                " UNION Select 4 as Level, B.IDDInas  AS IDDinas, A.IIDParent as IDUrusan, 0 as IDProgram,0 as IDkegiatan,0 as IDSUbKegiatan, 0 as btIDSubkegiatan,  " +
                 "LEFT(B.IIDRekening,12) as Rek, A.IIDRekening as IDRekening,   A.sNamaRekening AS Nama,B.btJenis, SUM(B." + sKolom + " )AS JumlahOlah,  " +
                  "SUM(B." + sKolom + ") AS Jumlah ,  SUM(B." + sKolom + " )  AS JumlahMurni  from mRekening  A  INNER JOIN tANGGARANREKENING_A B ON " +
                " LEFT(A.IIDRekening,12)= LEFT(B.IIDRekening,12)   WHERE A.btRoot = 6 and B.iTahun =" + _pTahun.ToString() + " AND B.IDDInas=" + _IDDinas.ToString() + " and b.IIDRekening like '62%'  " +
                    " GROUP BY B.IDDInas  , A.IIDParent  , A.IIDRekening ,LEFT(B.IIDRekening,12),A.sNamaRekening,B.btJenis";
 

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RekapAnggaran()
                                {

                                    Level = DataFormat.GetInteger(dr["Level"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Kode = GetKodeSPDBTL(DataFormat.GetInteger(dr["Level"]),
                                            DataFormat.GetInteger(dr["IDDInas"]),
                                            DataFormat.GetInteger(dr["IDUrusan"]),
                                            DataFormat.GetInteger(dr["IDProgram"]),
                                            DataFormat.GetInteger(dr["IDKegiatan"]),
                                            DataFormat.GetLong(dr["IDRekening"])),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubkegiatan"]),
                                    IDRekening = DataFormat.GetLong(dr["IDRekening"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Jumlah = DataFormat.GetDecimal(dr["JumlahMurni"]) != DataFormat.GetDecimal(dr["JumlahOlah"]) ? DataFormat.GetDecimal(dr["JumlahOlah"]) : DataFormat.GetDecimal(dr["JumlahMurni"]),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]),
                                    JumlahOlah = DataFormat.GetDecimal(dr["JumlahOlah"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    Tahap=_pTahap

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
        private string GetKode(int Level, int idDInas, int idUrusan, int idProgram, int idKegiatan, long idsubkegiatan,long idRekening)
        {
            string sRet = "";
            if (Tahun < 2022) {
                switch (Level)
                {
                    case 1:
                        sRet = idDInas.ToKodeDinas();
                        break;
                    case 2:
                        sRet = idUrusan.ToKodeUrusan();
                        break;
                    case 3:
                        sRet = idProgram.ToSimpleKodeProgram();
                        break;
                    case 4:
                        sRet = idKegiatan.ToSimpleKodeKegiatan();
                        break;
                    case 5:
                        sRet = idRekening.ToKodeRekening(m_ProfileRekening);
                        break;
                }
              
            } else {

                switch (Level)
                {
                    case 1:
                        sRet = idDInas.ToKodeDinas();
                        break;
                    case 2:
                        sRet = idUrusan.ToKodeUrusan();
                        break;
                    case 3:
                        sRet = idProgram.ToSimpleKodeProgram();
                        break;
                    case 4:
                        sRet = idKegiatan.ToSimpleKodeKegiatan();
                        break;
                    case 5:
                        sRet = idsubkegiatan.ToSimpleKodeSubKegiatan();
                        break;
                           case 6:
                        sRet = idRekening.ToKodeRekening(m_ProfileRekening);
                        break;
                }
            }
            
            return sRet;
        }
        private string GetKodeSPD(int Level, int idDInas, int idUrusan, int idProgram, int idKegiatan, long idsubkegiatan, long idRekening)
        {
            string sRet = "";

            switch (Level)
            {
                case 1:
                    sRet = idDInas.ToKodeDinas();
                    break;
                case 2:
                    sRet = idUrusan.ToKodeUrusan();
                    break;
                case 3:
                    sRet = idProgram.ToKodeProgram();
                    break;
                case 4:
                    sRet = idKegiatan.ToKodeKegiatan(m_ProfileProgKegiatan);
                    break;
                
                case 5:
                    sRet = idUrusan.ToKodeUrusan() + "." + idDInas.ToKodeDinas() + "." + idProgram.ToSimpleKodeProgram() + "." + idKegiatan.ToSimpleKodeKegiatan() + "." + idsubkegiatan.ToSimpleKodeSubKegiatan();

                    break;
                case 6:
                    string sRetx = idUrusan.ToKodeUrusan() + "." + idDInas.ToKodeDinas() + "." + idProgram.ToSimpleKodeProgram() + "." + idKegiatan.ToSimpleKodeKegiatan() + "." + idsubkegiatan.ToSimpleKodeSubKegiatan();
                    string rekening = idRekening.ToKodeRekening(m_ProfileRekening);
                    sRet = sRetx + "." + rekening;
                    break;

            }
            return sRet;
        }
        private string GetKodeSPDBTL(int Level, int idDInas, int idUrusan, int idProgram, int idKegiatan, long idRekening)
        {
            string sRet = "";
                
            sRet = idDInas.ToKodeDinas().Substring(0,4)  + "." + idDInas.ToKodeDinas() + ".00.000." + idRekening.ToKodeRekening(m_ProfileRekening);
            return sRet;
        }
        public bool Simpan(List<TAnggaranRekening> _lst, int _iTahun, int _idDinas, int idUrusan, int idProgram, int _idKegiatan, int _iJenis, int _pTahap, long idSubKegiatan = 0, int idunit =0 )
        {
            //Hanya dipanggil saat inout RKA

            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;
            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSKPD;
            int _KodeUK;
            //       IDbTransaction transaction = _dbHelper.GetConnObject().BeginTransaction();

            try
            {


                foreach (TAnggaranRekening o in _lst)
                {
                    if (o.Jenis == 3)
                    {
                        _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, m_ProfileProgKegiatan.KodeKegiatan));
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                    }
                    else
                    {
                        _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    }

                    _KodeKategori = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                    _KodeUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    _KodeSKPD = DataFormat.GetInteger(o.IDDinas.ToString().Substring(3, 2));
                    _KodeUK = 0;
                    if (o.IDUnit > 0)
                    {

                        _KodeUK = DataFormat.GetInteger(o.IDUnit.ToString().Substring(5, 2));

                    }
                    //if (o.IDDinas.ToString().Length > 5)
                    //{
                    //    _KodeUK = DataFormat.GetInteger(o.IDDinas.ToString().Substring(5, 2));
                    //}
                    //else
                    //{
                    //    _KodeUK = 0;
                    //}


                 //   if (o.StatusUpdate == 0)
                        if (CekKeberadaan(o)==false)
                    {

                        Single btidsubkegiatan = o.IDSubKegiatan % 100;

                        switch (_pTahap)
                        {

                            case -1:

                                SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDUnit,IDProgram, IDkegiatan,IDsubKegiatan,IDUrusan,btKodekategoriPelaksana, " +
                                       " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btTahapInput, " +
                                       "IIDRekening,cJumlahPraRKA,cJumlahOlah,cJumlahRKA,cJumlahMurni,cJumlahGeser,cJumlahRKAP, cJumlahABT,cDPA,bPPKD,cJumlahYAD, cAdministrasi,cFisik, iTahap,btidsubkegiatan)  values ( " +
                                        "@pTahun,@pIDDInas,@pIDunit,@pIDProgram,@pIDkegiatan,@pIDsubKegiatan,@pIDUrusan,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                        "@pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtTahapInput,@pIIDRekening," +
                                        "@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pbPPKD,@pcJumlahYAD, @pcAdministrasi,@pcFisik, @piTahap,@pbtIDSubKegiatan)";
                                break;

                            case 1:

                                SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDunit, IDProgram, IDkegiatan,IDsubKegiatan,IDUrusan,btKodekategoriPelaksana, " +
                                       " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btTahapInput, " +
                                       "IIDRekening,cJumlahOlah,cJumlahRKA,cJumlahMurni,cJumlahGeser,cJumlahRKAP, cJumlahABT,cDPA,bPPKD,cJumlahYAD, cAdministrasi,cFisik, iTahap,btidsubkegiatan)  values ( " +
                                        "@pTahun,@pIDDInas,@pIDunit,@pIDProgram,@pIDkegiatan,@pIDsubKegiatan, @pIDUrusan,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                        "@pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtTahapInput,@pIIDRekening," +
                                        "@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pbPPKD,@pcJumlahYAD, @pcAdministrasi,@pcFisik, @piTahap,@pbtIDSubKegiatan)";
                                break;
                            case 2:

                                SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDunit, IDProgram, IDkegiatan,IDSubKegiatan,IDUrusan,btKodekategoriPelaksana, " +
                                       " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btTahapInput, " +
                                       "IIDRekening,cJumlahOlah,cJumlahRKA,cJumlahMurni,cJumlahGeser,cJumlahRKAP, cJumlahABT,cDPA,bPPKD,cJumlahYAD, cAdministrasi,cFisik, iTahap,btidsubkegiatan)  values ( " +
                                        "@pTahun,@pIDDInas,@pIDunit, @pIDProgram,@pIDkegiatan,@pIDsubKegiatan,@pIDUrusan,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                        "@pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtTahapInput,@pIIDRekening," +
                                        "@pcJumlahOlah,0,@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pbPPKD,@pcJumlahYAD, @pcAdministrasi,@pcFisik, @piTahap,@pbtIDSubKegiatan)";
                                break;

                            case 3:
                                SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDunit, IDProgram, IDkegiatan,IDSubKegiatan,IDUrusan,btKodekategoriPelaksana, " +
                                       " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btTahapInput, " +
                                       "IIDRekening, cJumlahOlah,cJumlahRKA,cJumlahMurni,cJumlahGeser,cJumlahRKAP, cJumlahABT,cDPA,bPPKD,cJumlahYAD, cAdministrasi,cFisik, iTahap,btidsubkegiatan)  values ( " +
                                        "@pTahun,@pIDDInas,@pIDunit,@pIDProgram,@pIDkegiatan,@pIDsubKegiatan,@pIDUrusan,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                        "@pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtTahapInput,@pIIDRekening," +
                                        "@pcJumlahOlah,0,0,@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pbPPKD,@pcJumlahYAD, @pcAdministrasi,@pcFisik, @piTahap,@pbtIDSubKegiatan)";
                                break;
                            case 4:
                                SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDunit,IDProgram, IDkegiatan,IDSUbKegiatan,IDUrusan,btKodekategoriPelaksana, " +
                                       " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btTahapInput, " +
                                       "IIDRekening, cJumlahOlah,cJumlahRKA,cJumlahMurni,cJumlahGeser,cJumlahRKAP, cJumlahABT,cDPA,bPPKD,cJumlahYAD,cAdministrasi,cFisik, iTahap,btidsubkegiatan)  values ( " +
                                        "@pTahun,@pIDDInas,@pIDunit,@pIDProgram,@pIDkegiatan,@pIDsubKegiatan,@pIDUrusan,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                        "@pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtTahapInput,@pIIDRekening," +
                                        "@pcJumlahOlah,0,0,0,@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pbPPKD,@pcJumlahYAD, @pcAdministrasi,@pcFisik, @piTahap,@pbtIDSubKegiatan)";
                                break;

                        }
                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pTahun", o.Tahun, DbType.Int16));
                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pIDunit", o.IDUnit, DbType.Int32));



                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pIDsubKegiatan", idSubKegiatan, DbType.Int64));
                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis, DbType.Int16));
                        paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput, DbType.Int16));
                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening, DbType.Int64));
                        paramCollection.Add(new DBParameter("@pcJumlahOlah", o.JumlahOlah, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));
                        paramCollection.Add(new DBParameter("@pcJumlahYAD", o.JumlahYAD, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pbtIDSubKegiatan", btidsubkegiatan, DbType.Int16));
                        paramCollection.Add(new DBParameter("@pcAdministrasi", o.Administrasi, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pcFisik", o.Fisik, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@piTahap", o.Tahap, DbType.Int16));


                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                        //MUST UPDATE 

                    }
                    else
                    {

                        switch (_pTahap)
                        {

                            case 1:
                                //cJumlahRKA=@pcJumlahOlah,cJumlahMurni=@pcJumlahOlah,cJumlahGeser=@pcJumlahOlah,cJumlahRKAP=@pcJumlahOlah,cJumlahABT=@pcJumlahOlah
                                SSQL = "UPDATE " + m_sNamaTabel + " SET cJumlahRKA=@pcJumlahOlah,cJumlahMurni=@pcJumlahOlah,cJumlahGeser=@pcJumlahOlah,cJumlahRKAP=@pcJumlahOlah,cJumlahABT=@pcJumlahOlah, cJumlahOlah=@pcJumlahOlah,cJumlahYAD=@pcJumlahYAD, cFisik=@pcFisik,cAdministrasi=@pcAdministrasi WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
                                    " AND IDUnit=@pIDUnit  AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and btJenis=@pbtJenis and  isnull(bPPKD,0)=@pbPPKD AND IDSUbKegiatan=@pIDsubKegiatan ";
                                //SSQL = "UPDATE " + m_sNamaTabel + " SET cJumlahRKA=@pcJumlahOlah WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
                                //   " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and btJenis=@pbtJenis and isnull(bPPKD,0)=@pbPPKD AND IDSUbKegiatan=@pIDsubKegiatan ";
                                ////and isnull(btIDSubKegiatan,0) =@pbtIDSubKegiatan 

                                    break;
                            case 2:
                                //cJumlahRKA=@pcJumlahOlah,cJumlahMurni=@pcJumlahOlah,cJumlahGeser=@pcJumlahOlah,cJumlahRKAP=@pcJumlahOlah,cJumlahABT=@pcJumlahOlah
                                SSQL = "UPDATE " + m_sNamaTabel + " SET cJumlahMurni=@pcJumlahOlah,cJumlahGeser=@pcJumlahOlah,cJumlahRKAP=@pcJumlahOlah,cJumlahABT=@pcJumlahOlah, cJumlahOlah=@pcJumlahOlah,cJumlahYAD=@pcJumlahYAD, cFisik=@pcFisik,cAdministrasi=@pcAdministrasi WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
                                    " AND IDUnit=@pIDUnit   AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and btJenis=@pbtJenis  and isnull(bPPKD,0)=@pbPPKD  AND IDSUbKegiatan=@pIDsubKegiatan ";
                                break;
                            case 3:
                                //cJumlahGeser=@pcJumlahOlah,cJumlahRKAP=@pcJumlahOlah,cJumlahABT=@pcJumlahOlah, cJumlahOlah=@pcJumlahOlah
                                SSQL = "UPDATE " + m_sNamaTabel + " SET cJumlahGeser=@pcJumlahOlah,cJumlahRKAP=@pcJumlahOlah,cJumlahABT=@pcJumlahOlah, cJumlahOlah=@pcJumlahOlah,cJumlahYAD=@pcJumlahYAD, cFisik=@pcFisik,cAdministrasi=@pcAdministrasi WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
                                    " AND IDUnit=@pIDUnit  AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and btJenis=@pbtJenis and isnull(bPPKD,0)=@pbPPKD  AND IDSUbKegiatan=@pIDsubKegiatan";
                                break;
                            case 4:
                                //cJumlahRKAP=@pcJumlahOlah,cJumlahABT=@pcJumlahOlah,cJumlahYAD=@pcJumlahYAD
                                SSQL = "UPDATE " + m_sNamaTabel + " SET cJumlahRKAP=@pcJumlahOlah,cJumlahABT=@pcJumlahOlah,cJumlahYAD=@pcJumlahYAD, cFisik=@pcFisik,cAdministrasi=@pcAdministrasi WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
                                    " AND IDUnit=@pIDUnit   AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and btJenis=@pbtJenis and  isnull(bPPKD,0)=@pbPPKD  AND IDSUbKegiatan=@pIDsubKegiatan";
                                break;
                        }
                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pcJumlahOlah", o.JumlahOlah, DbType.Decimal));
                        ////paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));
                        paramCollection.Add(new DBParameter("@pcJumlahYAD", o.JumlahYAD, DbType.Decimal));

                        paramCollection.Add(new DBParameter("@pcAdministrasi", o.Administrasi, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pcFisik", o.Fisik, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@piTahun", o.Tahun, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pIDunit", o.IDUnit, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pIDsubKegiatan", o.SubKegiatan, DbType.Int64));
                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening, DbType.Int64));
                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis, DbType.Int16));
                       // paramCollection.Add(new DBParameter("@pbtIDSubKegiatan", o.SubKegiatan, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));
                        // paramCollection.Add(new DBParameter("@piTahap", o.Tahap, DbType.Int16));

                       // _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    }
                    TAnggaranUraianLogic oLogic = new TAnggaranUraianLogic(_idDinas, _iTahun, _pTahap,mprofile);
                    if (_iJenis != 3)
                    {

                        SSQL = "UPDATE tANGGARANURAIAN_A SET IDurusan = SubString(Replace(Convert(varchar(10),IDDinas),' ',''),1,3) where Jenis <> 3 and IDDInas=" + _idDinas.ToString();
                        _dbHelper.ExecuteNonQuery(SSQL);

                    }

                //    oLogic.Simpan(o.ListUraian, _idDinas, _iTahun, _pTahap);


                }
                //        transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                //      transaction.Rollback();
                _isError = true;
                _lastError = ex.Message;
                return false;


            }
        }
        // Cek keberadaan 
        private bool CekKeberadaan(TAnggaranRekening ta)
        {
            bool ret = true;
            if (ta.Tahun <= 2020)
            {


                SSQL = "SELECT count(*) from tANggaranRekening_A WHERE iTAhun =" + ta.Tahun.ToString() + " AND IDDInas =" + ta.IDDinas.ToString() +
                " AND IDUrusan = " + ta.IDUrusan.ToString() + " AND IDProgram =" + ta.IDProgram.ToString() + " AND IDKegiatan=" + ta.IDKegiatan.ToString() +
                " AND IDSUBKegiatan=0  AND IIDRekening=" + ta.IDRekening.ToString();
            }
            else
            {
                SSQL = "SELECT count(*) from tANggaranRekening_A WHERE iTAhun =" + ta.Tahun.ToString() + " AND IDDInas =" + ta.IDDinas.ToString() +
                " AND IDUnit = " + ta.IDUnit.ToString () + " AND IDUrusan = " + ta.IDUrusan.ToString() + " AND IDProgram =" + ta.IDProgram.ToString() + " AND IDKegiatan=" + ta.IDKegiatan.ToString() +
                " and btjenis= " + ta.Jenis.ToString()+" AND IDSUBKegiatan=" + ta.SubKegiatan.ToString() + " AND IIDRekening=" + ta.IDRekening.ToString();


            }
            object o = _dbHelper.ExecuteScalar(SSQL);
            if ((int)o == 0)
                return false;
            else
                return true;
        }
        public bool SimpanDariPerencanaan(List<TAnggaranRekening> _lst)
        {
            //Hanya dipanggil saat inout RKA

            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;
            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSKPD;
            int _KodeUK;
            
            try
            {
                SSQL = "DELETE from tANggaranRekening_A ";
                _dbHelper.ExecuteNonQuery(SSQL);


                foreach (TAnggaranRekening o in _lst)
                {
                    if (o.Jenis == 3)
                    {
                        _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 3));
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));

                    }
                    else
                    {
                        _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    }

                    _KodeKategori = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                    _KodeUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    _KodeSKPD = DataFormat.GetInteger(o.IDDinas.ToString().Substring(3, 2));

                    if (o.IDDinas.ToString().Length > 5)
                    {
                        _KodeUK = DataFormat.GetInteger(o.IDDinas.ToString().Substring(5, 2));
                    }
                    else
                    {
                        _KodeUK = 0;
                    }




                    SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                          " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btTahapInput, " +
                          "IIDRekening,cJumlahPraRKA,cJumlahOlah,cJumlahRKA,cJumlahMurni,cJumlahGeser,cJumlahRKAP, cJumlahABT,bPPKD,cJumlahYAD,btIDSubKegiatan, cAdministrasi,cFisik, iTahap)  values ( " +
                          "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                          "@pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtTahapInput,@pIIDRekening," +
                          "@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pbPPKD,@pcJumlahYAD,@pbtIDSubKegiatan, @pcAdministrasi,@pcFisik, @piTahap)";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pTahun", o.Tahun, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening, DbType.Int64));
                    paramCollection.Add(new DBParameter("@pcJumlahOlah", o.JumlahOlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pcJumlahYAD", o.JumlahYAD, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pbtIDSubKegiatan", o.SubKegiatan, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcAdministrasi", o.Administrasi, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcFisik", o.Fisik, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@piTahap", -1, DbType.Int16));


                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

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


        public bool SimpanImportBapeda(List<TAnggaranRekening> _lst, int _iTahun, int _pTahap)
        {
            //Hanya dipanggil saat inout RKA

            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;
            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSKPD;
            int _KodeUK;
            int _barisError = 0;

            TAnggaranRekening ta = new TAnggaranRekening();
            IDbTransaction transaction = _dbHelper.GetConnObject().BeginTransaction();
            int i = 0;
            try
            {


                foreach (TAnggaranRekening o in _lst)
                {
                    i++;
           
                    if (o.IDRekening > 5200000 && o.IDRekening < 6000000)
                    {
                        ta = o;
                        _barisError=1;
                        o.Jenis = 3;

                        _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _barisError=2;
                        _KodeKegiatan =DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, m_ProfileProgKegiatan.KodeKegiatan));
                        _barisError=3;
                        _KodeKategoriPelaksana =  DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                        _barisError=4;
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));

                    }
                    else
                    {


                        if (o.IDRekening == 0)
                            return true;
                        if (o.IDRekening > 5000000 && o.IDRekening < 5200000)
                            o.Jenis = 2;


                        _KodeProgram =  DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan =DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                        _barisError=6;
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                        _barisError=7;
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));

                    }

                    _barisError = 8;
                    _KodeKategori = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                    _barisError = 9;
                    _KodeUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    _barisError = 10;
                    _KodeSKPD = DataFormat.GetInteger(o.IDDinas.ToString().Substring(3, 2));

                    if (o.IDDinas.ToString().Length > 5)
                    {
                        _KodeUK = 0;// DataFormat.GetInteger(o.IDDinas.ToString().Substring(5, 2));
                    }
                    else
                    {
                        _KodeUK = 0;
                    }
                    

                    SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                                       " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btTahapInput, " +
                                       "IIDRekening,cJumlahOlah,cJumlahRKA,cJumlahMurni,cJumlahGeser,cJumlahRKAP, cJumlahABT,bPPKD,cJumlahYAD,btIDSubKegiatan, cAdministrasi,cFisik, iTahap,IDRincianBapeda,cJumlahRKABapeda)  values ( " +
                                        "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                        "@pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtTahapInput,@pIIDRekening," +
                                        "@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pcJumlahOlah,@pbPPKD,@pcJumlahYAD,@pbtIDSubKegiatan, @pcAdministrasi,@pcFisik, @piTahap,@pIDRincianBapeda,@pcJumlahRKABapeda)";
                    
                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pTahun", o.Tahun));
                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                        paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana));
                        paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana));
                        paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori));
                        paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan));
                        paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD));
                        paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK));
                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                        paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram));
                        paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan));
                        paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput));
                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                        paramCollection.Add(new DBParameter("@pcJumlahOlah", o.JumlahOlah, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pbPPKD", 0, DbType.Int16));
                        paramCollection.Add(new DBParameter("@pcJumlahYAD", 0, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pbtIDSubKegiatan", 0, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pcAdministrasi", 0, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pcFisik", 0, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@piTahap", 0));
                        paramCollection.Add(new DBParameter("@pIDRincianBapeda", o.rincian_ID, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pcJumlahRKABapeda", o.JumlahOlah, DbType.Decimal));
                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);   
                        
                    }
                    
            //    transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _isError = true;
                _lastError = ex.Message;
                return false;


            }
        }

        public bool RefreshJumlahImport(int pTahun )
        {

            List<TEMPJumlahUraian> _lst = new List<TEMPJumlahUraian>();
            try
            {
                SSQL = " Select IDDInas, IDUrusan,IDProgram, IDKegiatan,IIDRekening , SUM(JumlahRKA) as Jumlah from tAnggaranUraian_A WHERE iTahun =" + pTahun.ToString() + " group BY IDDInas, IDUrusan,IDProgram, IDKegiatan,IIDRekening ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TEMPJumlahUraian()
                                {
                                  
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"])

                                }).ToList();
                    }
                }

                foreach (TEMPJumlahUraian o in _lst)
                {
                    SSQL = " UPDATE tAnggaranRekening_A SET cJumlah =@pcJumlahOlah, cJumlahRKA=@pcJumlahOlah,cJumlahMurni=@pcJumlahOlah,cJumlahGeser=@pcJumlahOlah,cJumlahRKAP=@pcJumlahOlah,cJumlahABT=@pcJumlahOlah, cJumlahOlah=@pcJumlahOlah,cJumlahYAD=@pcJumlahOlah " +
                            " WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
                                        " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening ";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pcJumlahOlah", o.Jumlah));
                    paramCollection.Add(new DBParameter("@piTahun", pTahun));
                    paramCollection.Add(new DBParameter("@pIDDInas", o.IDDInas));
                    paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                    paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                    paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

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
        

        public bool SimpanRKA(List<TAnggaranRekening> _lst,int _iTahun, int _idDinas, int idUrusan, int idProgram, int _idKegiatan , int _iJenis, Single _pTahap)
        {
            //Hanya dipanggil saat inout RKA
           
            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;
            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSKPD;
            int _KodeUK;
            IDbTransaction transaction = _dbHelper.GetConnObject().BeginTransaction();

            try
            {
                foreach (TAnggaranRekening o in _lst)
                {
                    if (o.Jenis == 3)
                    {   _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 3));
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                    }
                    else
                    {
                        _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                        _KodeKategoriPelaksana =  DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    }

                    _KodeKategori = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                    _KodeUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    _KodeSKPD = DataFormat.GetInteger(o.IDDinas.ToString().Substring(3, 2));

                    if (o.IDDinas.ToString().Length > 5)
                    {
                        _KodeUK = DataFormat.GetInteger(o.IDDinas.ToString().Substring(5, 2));
                    }
                    else
                    {
                        _KodeUK = 0;
                    }
                    

                    if (o.StatusUpdate == 0)
                   {
                        SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                               "btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btTahapInput, " +
                               "IIDRekening,cRKA,cJumlahOlah,cJumlahMurni,cJumlahGeser, cJumlahRKAP, cJumlahABT,cJumlahGeserP,bPPKD,cJumlahYAD,btIDSubKegiatan, cAdministrasi,cFisik, iTahap)  values ( " +
                                "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                "@pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtTahapInput,@pIIDRekening," +
                                "@pcRKA, @pcJumlahOlah,@pcJumlahMurni,@pcJumlahGeser, @pcJumlahRKAP, @pcJumlahABT,@pcJumlahGeserP,@pbPPKD,@pcJumlahYAD,@pbtIDSubKegiatan, @pcAdministrasi,@pcFisik, @piTahap)";

                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pTahun", o.Tahun));
                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                        paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana));
                        paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana));
                        paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori));
                        paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan));
                        paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD));
                        paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK));
                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                        paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram));
                        paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan));
                        paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput));
                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                        paramCollection.Add(new DBParameter("@pcRKA", o.JumlahOlah, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pcJumlahOlah", o.JumlahOlah, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pcJumlahMurni", o.JumlahOlah, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pcJumlahGeser", o.JumlahOlah, DbType.Decimal)); 
                        paramCollection.Add(new DBParameter("@pcJumlahRKAP", o.JumlahOlah, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pcJumlahABT", o.JumlahOlah, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pcJumlahGeserP", o.JumlahOlah, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));                        
                        paramCollection.Add(new DBParameter("@pcJumlahYAD", o.JumlahYAD,DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pbtIDSubKegiatan", o.SubKegiatan,DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pcAdministrasi", o.Administrasi,DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pcFisik", o.Fisik, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@piTahap", o.Tahap));
                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                   }
                    else
                    {
                        SSQL = "UPDATE " + m_sNamaTabel + " SET cRKA=@pcRKA,cJumlahOlah=@pcJumlahOlah,cJumlahMurni=@pcJumlahMurni, cJumlahGeser=@pcJumlahGeser, cJumlahRKAP=@pcJumlahRKAP, cJumlahABT=@pcJumlahABT,cJumlahGeserP=@pcJumlahGeserP ,cJumlahYAD=@pcJumlahYAD, cFisik=@pcFisik,cAdministrasi=@pcAdministrasi WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
                              " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and btJenis=@pbtJenis and isnull(btIDSubKegiatan,0) =@pbtIDSubKegiatan and isnull(bPPKD,0)=@pbPPKD AND isnull(iTahap,0) =@piTahap";

                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pcRKA", o.JumlahOlah));
                        paramCollection.Add(new DBParameter("@pcJumlahOlah", o.JumlahOlah));
                        paramCollection.Add(new DBParameter("@pcJumlahMurni", o.JumlahOlah));
                        paramCollection.Add(new DBParameter("@pcJumlahGeser", o.JumlahOlah, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pcJumlahRKAP", o.JumlahOlah, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pcJumlahABT", o.JumlahOlah, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pcJumlahGeserP", o.JumlahOlah, DbType.Decimal));

                        paramCollection.Add(new DBParameter("@pcJumlahYAD", o.JumlahYAD));                        
                        paramCollection.Add(new DBParameter("@pcAdministrasi", o.Administrasi, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pcFisik", o.Fisik, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                        paramCollection.Add(new DBParameter("@pbtIDSubKegiatan", o.SubKegiatan, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));
                        paramCollection.Add(new DBParameter("@piTahap", o.Tahap));
                        
                        

                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    }                    
                    TAnggaranUraianLogic oLogic = new TAnggaranUraianLogic(_idDinas, _iTahun, _pTahap,mprofile);
                    if (_iJenis != 3)
                    {
                        SSQL = "UPDATE tANGGARANURAIAN_A SET IDurusan = SubString(Replace(Convert(varchar(10),IDDinas),' ',''),1,3) where Jenis <> 3 and IDDInas=" + _idDinas.ToString();
                        _dbHelper.ExecuteNonQuery(SSQL);

                    }

                    oLogic.SimpanRKA(o.ListUraian, _idDinas, _iTahun);


                }
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _isError = true;
                _lastError = ex.Message;
                return false;


            }
        }

        public bool SimpanMurni(List<TAnggaranRekening> _lst, int _iTahun, int _idDinas, int idUrusan, int idProgram, int _idKegiatan, int _iJenis, Single _pTahap)
        {
            try
            {

                
                foreach (TAnggaranRekening o in _lst)
                {
                    SSQL = "UPDATE " + m_sNamaTabel + " SET cJumlahRKA=@pcJumlahMurni, cJumlahMurni=@pcJumlahMurni WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
                              " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and btJenis=@pbtJenis and isnull(btIDSubKegiatan,0) =@pbtIDSubKegiatan and isnull(bPPKD,0)=@pbPPKD ";

                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pcJumlahMurni", o.JumlahMurni));
                        paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                        paramCollection.Add(new DBParameter("@pbtIDSubKegiatan", o.SubKegiatan, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));



                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);                    
                        TAnggaranUraianLogic oLogic = new TAnggaranUraianLogic(_idDinas, _iTahun, _pTahap,mprofile);
                     

                        oLogic.SimpanUraianMurni(o.ListUraian, _idDinas, _iTahun);


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

        public bool SimpanDPA(List<TAnggaranRekening> _lst, int _iTahun, int _idDinas, int idUrusan, int idProgram, int _idKegiatan, int _iJenis, Single _pTahap)
        {

            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;
            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSKPD;
            int _KodeUK;
            IDbTransaction transaction = _dbHelper.GetConnObject().BeginTransaction();

            try
            {
                foreach (TAnggaranRekening o in _lst)
                {
                    if (o.Jenis == 3)
                    {
                        _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 3));
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                    }
                    else
                    {
                        _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    }

                    _KodeKategori = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                    _KodeUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    _KodeSKPD = DataFormat.GetInteger(o.IDDinas.ToString().Substring(3, 2));

                    if (o.IDDinas.ToString().Length > 5)
                    {
                        _KodeUK = DataFormat.GetInteger(o.IDDinas.ToString().Substring(5, 2));
                    }
                    else
                    {
                        _KodeUK = 0;
                    }


                    if (o.StatusUpdate == 0)
                    {
                        SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                               " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btTahapInput, " +
                               "IIDRekening,cJumlahOlah,bPPKD,cJumlahYAD,btIDSubKegiatan, cAdministrasi,cFisik, iTahap)  values ( " +
                                "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                "@pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtTahapInput,@pIIDRekening," +
                                "@pcJumlahOlah,@pbPPKD,@pcJumlahYAD,@pbtIDSubKegiatan, @pcAdministrasi,@pcFisik, @piTahap)";

                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pTahun", o.Tahun));
                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                        paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana));
                        paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana));
                        paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori));
                        paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan));
                        paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD));
                        paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK));
                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                        paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram));
                        paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan));
                        paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput));
                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                        paramCollection.Add(new DBParameter("@pcJumlahOlah", o.JumlahOlah, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));
                        paramCollection.Add(new DBParameter("@pcJumlahYAD", o.JumlahYAD, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pbtIDSubKegiatan", o.SubKegiatan, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pcAdministrasi", o.Administrasi, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pcFisik", o.Fisik, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@piTahap", o.Tahap));


                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                    }
                    else
                    {
                        SSQL = "UPDATE " + m_sNamaTabel + " SET cDPA= @pcDPA,JumlahYADAPBD =@pJumlahYADAPBD  WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
                              " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and btJenis=@pbtJenis and isnull(btIDSubKegiatan,0) =@pbtIDSubKegiatan and isnull(bPPKD,0)=@pbPPKD AND iTahap =@piTahap";

                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pcDPA", o.JumlahDPA, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pJumlahYADAPBD", o.JumlahYADAPBD, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                        paramCollection.Add(new DBParameter("@pbtIDSubKegiatan", o.SubKegiatan, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));
                        paramCollection.Add(new DBParameter("@piTahap", o.Tahap));

                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                    }
                    TAnggaranUraianLogic oLogic = new TAnggaranUraianLogic(_idDinas, _iTahun, _pTahap,mprofile);
                    if (_iJenis != 3)
                    {
                        SSQL = "UPDATE tANGGARANURAIAN_A SET IDurusan = SubString(Replace(Convert(varchar(10),IDDinas),' ',''),1,3) where Jenis <> 3 and IDDInas=" + _idDinas.ToString();
                        _dbHelper.ExecuteNonQuery(SSQL);
                    }
                    oLogic.SimpanDPA(o.ListUraian, _idDinas, _iTahun);


                }
                //transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _isError = true;
                _lastError = ex.Message;
                return false;


            }
        }


        public bool SimpanPlafon(List<TAnggaranRekening> _lst, int _iTahun, int _idDinas, int pidunit, int idUrusan, int idProgram, int _idKegiatan, int _iJenis,Single _pTahap)
        {
            try
            {
                foreach (TAnggaranRekening o in _lst)
                {

                    if (CekAda(o) == 0)
                    {
                        int _KodeProgram;
                        int _KodeKegiatan;
                        int _KodeKategoriPelaksana;
                        int _kodeUrusanPelaksana;
                        int _KodeKategori;
                        int _KodeUrusan;
                        int _KodeSubKegiatan;
                        int _KodeSKPD;
                        int _KodeUK;
                      
                            
                      
                        if (o.Jenis == 3 && o.IDProgram > 0 && o.IDRekening > 0)
                        {
                            _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                            _KodeKegiatan = DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 3));
                            _KodeSubKegiatan = DataFormat.GetInteger(o.IDSubKegiatan.ToString().Substring(8, 2));
                            _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                            _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                            _KodeUK = o.KodeUK;
                        }
                        else
                        {
                            _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                            _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                            _KodeSubKegiatan = 0;
                            _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                            _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                            //o.IDUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 3));
                            _KodeUK = o.KodeUK;
                        }

                        _KodeKategori = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                        _KodeUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                        _KodeSKPD = DataFormat.GetInteger(o.IDDinas.ToString().Substring(3, 2));

                        if (o.Tahap == 2)
                        {
                            SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,IDSubKegiatan ,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btIDsubKegiatan,btTahapInput, " +
                                   "IIDRekening, cJumlahMurni,cJumlahRKA,cJumlahGeser, cJumlahRKAP,cDPA,cJumlahABT,cPlafon,bPPKD,idUnit)  values ( " +
                                    "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pIDSubKegiatan ,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                    " @pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtIDsubKegiatan,@pbtTahapInput,@pIIDRekening," +
                                    "@pcJumlahRKAP,@pcJumlahRKAP,@pcJumlahRKAP,@pcJumlahRKAP,@pcDPA,@pcJumlahABT,@pcPlafon,@bppkd,@idUnit)";
                        }


                        if (o.Tahap == 3)
                        {
                            SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,IDSubKegiatan ,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btIDsubKegiatan,btTahapInput, " +
                                   "IIDRekening, cJumlahMurni,cJumlahRKA,cJumlahGeser, cJumlahRKAP,cDPA,cJumlahABT,cPlafon,bPPKD,idUnit)  values ( " +
                                    "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pIDSubKegiatan ,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                    " @pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtIDsubKegiatan,@pbtTahapInput,@pIIDRekening," +
                                    "0,0,@pcJumlahRKAP,@pcJumlahRKAP,@pcJumlahRKAP,@pcJumlahRKAP,@pcPlafon,@bppkd,@idUnit)";
                        }
                        if (o.Tahap == 4)
                        {
                            SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,IDSubKegiatan ,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btIDsubKegiatan,btTahapInput, " +
                                   "IIDRekening, cJumlahMurni,cJumlahRKA,cJumlahGeser, cJumlahRKAP,cDPA,cJumlahABT,cPlafon,bPPKD,idUnit)  values ( " +
                                    "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pIDSubKegiatan ,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                    " @pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtIDsubKegiatan,@pbtTahapInput,@pIIDRekening," +
                                    "0,0,0,@pcJumlahRKAP,@pcJumlahRKAP,@pcJumlahRKAP,@pcPlafon,@bppkd,@idUnit)";
                        }
                        if (o.Tahap == 5)
                        {
                            SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,IDSubKegiatan ,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btIDsubKegiatan,btTahapInput, " +
                                   "IIDRekening, cJumlahMurni,cJumlahRKA,cJumlahGeser, cJumlahRKAP,cDPA,cJumlahABT,cPlafon,bPPKD,idUnit)  values ( " +
                                    "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pIDSubKegiatan ,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                    " @pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtIDsubKegiatan,@pbtTahapInput,@pIIDRekening," +
                                    "0,0,0,@pcJumlahRKAP,@pcJumlahRKAP,@pcJumlahRKAP,@pcPlafon,@bppkd,@idUnit)";
                        }
                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pTahun", o.Tahun));
                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                        paramCollection.Add(new DBParameter("@pIDSubKegiatan", o.IDSubKegiatan));
                        paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana));
                        paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana));
                        paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori));
                        paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan));
                        paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD));
                        paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK));
                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                        paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram));
                        paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan));
                        paramCollection.Add(new DBParameter("@pbtIDsubKegiatan", _KodeSubKegiatan));
                        paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput));
                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                        //    paramCollection.Add(new DBParameter("@pbtIDSUbKegiatan", o.SubKegiatan));
                        paramCollection.Add(new DBParameter("@pcJumlahRKAP", o.JumlahRKAP));
                        paramCollection.Add(new DBParameter("@pcDPA", o.JumlahRKAP));
                        paramCollection.Add(new DBParameter("@pcJumlahABT", o.JumlahRKAP));
                        paramCollection.Add(new DBParameter("@pcPlafon", o.JumlahRKAP));


                        paramCollection.Add(new DBParameter("@bppkd", o.PPKD));
                        paramCollection.Add(new DBParameter("@idUnit", o.IDUnit));

                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                    }
                    else
                    {

                        SSQL = "UPDATE " + m_sNamaTabel + " SET cJumlahGeser =@cJumlahGeser, cJumlahMurni =@cJumlahMurni, cJumlahRKAP =@pcJumlahRKAP ,cJumlahABT =@pcJumlahABT WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
                                 " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and btJenis=@pbtJenis and btKodeUK=@KodeUK and IDSubKegiatan= @pIDSubKegiatan ";

                            DBParameterCollection paramCollection = new DBParameterCollection();
                            //paramCollection.Add(new DBParameter("@cJumlahMurni", o.JumlahMurni, DbType.Decimal));
                            paramCollection.Add(new DBParameter("@cJumlahMurni", o.JumlahMurni, DbType.Decimal));
                     
                            paramCollection.Add(new DBParameter("@cJumlahGeser", o.JumlahPergeseran, DbType.Decimal));
                            paramCollection.Add(new DBParameter("@pcJumlahRKAP", o.JumlahRKAP, DbType.Decimal));
                            paramCollection.Add(new DBParameter("@pcJumlahABT", o.JumlahABT, DbType.Decimal));
                            paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                            paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                            paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                            paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                            paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                            paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                            paramCollection.Add(new DBParameter("@KodeUK", pidunit));
                            paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));

                            paramCollection.Add(new DBParameter("@pIDSubKegiatan", o.IDSubKegiatan, DbType.Int64));

                            _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                        

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

        public bool Perbaikandinkes()//TAnggaranRekening ta)
        {

            int lastid = 0;
            try
            {

                //SSQL = "UPDATE tANggaranRekening_A set cJumlahRKAP =0  WHERE IDDInas = 1020100 ";

               // _dbHelper.ExecuteNonQuery(SSQL);

                List <AnggaranKesehatan> lst = new List <AnggaranKesehatan>();
                SSQL="Select * from kesehatanperubahan where id>=32 and id<=54 order by ID ";

        
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new AnggaranKesehatan()
                               {
                                   ID = DataFormat.GetInteger(dr["ID"]),
                                   Kode = DataFormat.GetString(dr["Kode"]),
                                   Jummlah = DataFormat.GetDecimal(dr["Jumlah"]),
                                   UK = DataFormat.GetInteger(dr["UK"]),
                               }).ToList(); ;
                    }
                }
                string idKSubkegiatan = "";
                int affected = 0;
              
                foreach (AnggaranKesehatan ak in lst)
                {
                    lastid = ak.ID;
                    if (ak.ID ==52)
                    {
                        ak.ID = 52;
                    }

                    //if (ak.Kode.Trim().Length < 15)
                    //{
                    //    idKSubkegiatan = "";

                    //}
                    if (ak.Kode.Trim().Replace(" ", "").Replace(".", "").Length == 10)
                    {
                        idKSubkegiatan = ak.Kode.Trim().Replace(".", "");

                    }
                    if (ak.Kode.Trim().Replace(" ", "").Replace(".", "").Length == 12)
                    {
                      //  idKSubkegiatan = ak.Kode.Trim().Replace(".","");
                        
                        SSQL = "UPDATE tANggaranRekening_A set cJumlahRKAP =" + ak.Jummlah.ToString() + " WHERE IDDInas = 1020100 and IDSubKegiatan =" + idKSubkegiatan + " and iidrekening ="+ ak.Kode.Trim().Replace(".","") +" and  btKodeUk =" + ak.UK.ToString();

                        affected=_dbHelper.ExecuteNonQuery(SSQL);
                        Console.WriteLine(SSQL);
                        if (affected == 0)
                        {
                            if (CekAdaEx(2023, 1020100, 
                                        DataFormat.GetLong(idKSubkegiatan), 
                                        DataFormat.GetLong(ak.Kode.Trim().Replace(".","")),
                                        ak.UK) ==0) {
                                            TAnggaranRekening ta = new TAnggaranRekening();
                                            ta.Tahun = 2023;
                                            ta.IDDinas = 1020100;
                                            ta.IDUrusan = DataFormat.GetInteger(idKSubkegiatan.Substring(0, 3));
                                            ta.IDProgram = DataFormat.GetInteger(idKSubkegiatan.Substring(0, 5));
                                            ta.IDKegiatan = DataFormat.GetInteger(idKSubkegiatan.Substring(0, 8));
                                            ta.IDSubKegiatan= DataFormat.GetLong(idKSubkegiatan);
                                            ta.IDRekening = DataFormat.GetLong(ak.Kode.Trim().Replace(".", ""));
                                            ta.JumlahRKAP = ak.Jummlah;
                                            ta.IDUnit = 1020100 + ak.UK;
                                            ta.Tahap = 4;
                                            ta.TahapInput = 4;
                                            ta.Tahap = 4;
                                            ta.Jenis = 3;
                                List<TAnggaranRekening> lstTA = new List<TAnggaranRekening>();
                                lstTA.Add(ta);
                                SImpanSIPD(lstTA);
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
        public bool SimpanDisp(List<TAnggaranRekening> _lst, int _iTahun, int _idDinas, int idUrusan, int idProgram, int _idKegiatan, int _iJenis, Single _pTahap)
        {

            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;
            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSKPD;
            int _KodeUK;
            IDbTransaction transaction = _dbHelper.GetConnObject().BeginTransaction();

            try
            {
                foreach (TAnggaranRekening o in _lst)
                {
                    if (o.Jenis == 3)
                    {
                        _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 3));
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                    }
                    else
                    {
                        _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    }

                    _KodeKategori = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                    _KodeUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    _KodeSKPD = DataFormat.GetInteger(o.IDDinas.ToString().Substring(3, 2));

                    if (o.IDDinas.ToString().Length > 5)
                    {
                        _KodeUK = DataFormat.GetInteger(o.IDDinas.ToString().Substring(5, 2));
                    }
                    else
                    {
                        _KodeUK = 0;
                    }
                    if (o.StatusUpdate == 0)
                    {
                        SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                               " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btTahapInput, " +
                               "IIDRekening,btIDSUbKegiatan, cJumlahOlah,cJumlah,bPPKD,cPlafon, iTahap)  values ( " +
                                "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                "@pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtTahapInput,@pIIDRekening,@pbtIDSUbKegiatan," +
                                "@pcJumlahOlah,0,@pbPPKD,@pcPlafon, @piTahap)";


                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pTahun", o.Tahun));
                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                        paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana));
                        paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana));
                        paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori));
                        paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan));
                        paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD));
                        paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK));
                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                        paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram));
                        paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan));
                        paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput));
                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                        paramCollection.Add(new DBParameter("@pbtIDSUbKegiatan", o.SubKegiatan));
                        paramCollection.Add(new DBParameter("@pcJumlahOlah", o.JumlahOlah, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));
                        paramCollection.Add(new DBParameter("@pcPlafon", o.Plafon, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@piTahap", o.Tahap, DbType.Int16));

                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    }
                    else
                    {
                        SSQL = "UPDATE " + m_sNamaTabel + " SET cPlafon =@pcPlafon, cJumlah =@pcPlafon WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan and isnull(btIDSUbKegiatan,0)=@pbtIDSUbKegiatan " +
                             " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and btJenis=@pbtJenis and isnull(bPPKD,0)=@pbPPKD and isnull(iTahap,0)=@piTahap";


                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pcPlafon", o.Plafon, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                        paramCollection.Add(new DBParameter("@pbtIDSUbKegiatan", o.SubKegiatan));
                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                        paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));
                        paramCollection.Add(new DBParameter("@piTahap", o.Tahap));
                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    }
                    TAnggaranUraianLogic oLogic = new TAnggaranUraianLogic(_idDinas, _iTahun, _pTahap,mprofile);
                    if (_iJenis != 3)
                    {
                        SSQL = "UPDATE tANGGARANURAIAN_A SET IDurusan = SubString(Replace(Convert(varchar(10),IDDinas),' ',''),1,3) where Jenis <> 3and IDDInas=" + _idDinas.ToString();
                        _dbHelper.ExecuteNonQuery(SSQL);
                    }
                    if (o.ListUraian != null)
                        oLogic.SimpanPlafon(o.ListUraian, _idDinas, _iTahun, _pTahap);
                }
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _isError = true;
                _lastError = ex.Message;
                return false;


            }
        }

        //public bool SimpanImport(List<TAnggaranRekening> mListUnit, int pIDDinas, int _piTahun, Single _pTahap, bool hanyaPagu)
        //{


        //    int _KodeProgram;
        //    int _KodeKegiatan;
        //    int _KodeKategoriPelaksana;
        //    int _kodeUrusanPelaksana;
        //    int _KodeKategori;
        //    int _KodeUrusan;
        //    int _KodeSKPD;
        //    int _KodeUK;


        //   // IDbTransaction transaction = _dbHelper.GetConnObject().BeginTransaction();
        //    try
        //    {
        //        RekeningLogic oRekLogic = new RekeningLogic(Tahun,RekeningLogic.E_REKENING_TYPE.REKENING_13);

        //        if (_pTahap < 3)
        //        {
        //            if (hanyaPagu)
        //                SSQL = "UPDATE tANGGARANREKENING_A SET cPlafon = 0,cPlafonABT = 0 ,isImport=0 WHERE iTahun= " + _piTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString() + " AND iTahap=" + _pTahap.ToString();
        //            else 
        //                SSQL = "UPDATE tANGGARANREKENING_A SET cJumlah=0, cPlafon = 0,cJumlahRKA=0,cJumlahRKAP=0,cJumlahABT=0,   cJumlahMurni=0, cPlafonABT = 0 ,isImport=0 WHERE iTahun= " + _piTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString() + " AND iTahap=" + _pTahap.ToString();
        //            _dbHelper.ExecuteNonQuery(SSQL);
        //        }
        //        else
        //        {
        //            if (hanyaPagu)
        //                SSQL = "UPDATE tANGGARANREKENING_A SET  cPlafonABT = 0  WHERE iTahun= " + _piTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString();// +" AND iTahap=" + _pTahap.ToString();
        //            else  
        //                SSQL = "UPDATE tANGGARANREKENING_A SET  cJumlahRKAP=0, cJumlahABT=0, cPlafonABT = 0 ,isImport=0 WHERE iTahun= " + _piTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString();// +" AND iTahap=" + _pTahap.ToString();
        //            _dbHelper.ExecuteNonQuery(SSQL);

        //        }
        //        foreach (TAnggaranRekening o in mListUnit)
        //        {
                    
        //            if (o.Jenis == 3)
        //            {
        //                _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
        //                _KodeKegiatan = DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
        //                _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
        //                _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
        //            }
        //            else
        //            {
        //                _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
        //                _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
        //                _KodeKategoriPelaksana =  DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
        //                _kodeUrusanPelaksana =  DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
        //                o.IDKegiatan = 0;
        //                o.IDProgram = 0;
        //            }
        //            _KodeKategori = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
        //            _KodeUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
        //            _KodeSKPD = DataFormat.GetInteger(o.IDDinas.ToString().Substring(3, 2));
        //            if (o.IDDinas.ToString().Length > 5)
        //            {
        //                _KodeUK = DataFormat.GetInteger(o.IDDinas.ToString().Substring(5, 2));
        //            }
        //            else
        //            {
        //                _KodeUK = 0;
        //            }

        //            if (o.IDKegiatan==10216013)
        //            {
        //                _KodeUK = DataFormat.GetInteger(o.IDDinas.ToString().Substring(5, 2));
        //            }
        //            else
        //            {
        //                _KodeUK = 0;
        //            }
                    
        //            // Cek Apakah sudah ada?
        //            o.StatusUpdate=1;
        //            if (CekAda( o)==0){
        //                o.StatusUpdate=0;
        //            }
                    
        //            if (o.StatusUpdate == 0)
        //            {
        //                if (_pTahap < 3)
        //                {
        //                    DBParameterCollection paramCollection = new DBParameterCollection();
        //                    //if (hanyaPagu)
        //                    //{
        //                    //    SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
        //                    //           " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btTahapInput," +
        //                    //           "IIDRekening,cPlafon,cPlafonABT,bPPKD)  values ( " +
        //                    //            "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
        //                    //            "@pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtTahapInput,@pIIDRekening," +
        //                    //            "@pcPlafon,@pcPlafon,@pbPPKD)";

        //                    //    paramCollection.Add(new DBParameter("@pTahun", o.Tahun));
        //                    //    paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
        //                    //    paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
        //                    //    paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
        //                    //    paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
        //                    //    paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana));
        //                    //    paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana));
        //                    //    paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori));
        //                    //    paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan));
        //                    //    paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD));
        //                    //    paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK));
        //                    //    paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
        //                    //    paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram));
        //                    //    paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan));
        //                    //    paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput));
        //                    //    paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
        //                    //    paramCollection.Add(new DBParameter("@pcPlafon", o.Plafon, DbType.Decimal));
        //                    //    paramCollection.Add(new DBParameter("@pcPlafon", o.Plafon, DbType.Decimal));
        //                    //    paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));
                                
        //                    //}
        //                    //else
        //                    //{  //// Bukan plafon 
        //                        SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
        //                               " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btTahapInput," +
        //                               "IIDRekening,cJumlah,bPPKD,cJumlahRKA,cPlafon,cJumlahMurni,cJumlahGeser, cJumlahRKAP,cJumlahABT,cPlafonABT,iTahap,isImport)  values ( " +
        //                                "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
        //                                "@pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtTahapInput,@pIIDRekening," +
        //                                "@pcPlafon,@pbPPKD,@pcPlafon,@pcPlafon,@pcPlafon,@pcPlafon,@pcPlafon,@pcPlafon,@pcPlafonABT, @piTahap,1)";


                    
        //                        paramCollection.Add(new DBParameter("@pTahun", o.Tahun));
        //                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
        //                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
        //                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
        //                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
        //                        paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana));
        //                        paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana));
        //                        paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori));
        //                        paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan));
        //                        paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD));
        //                        paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK));
        //                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
        //                        paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram));
        //                        paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan));
        //                        paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput));
        //                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
        //                        paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));
        //                        paramCollection.Add(new DBParameter("@pcPlafon", o.Plafon, DbType.Decimal));
        //                        paramCollection.Add(new DBParameter("@pcPlafonABT", o.PlafonABT, DbType.Decimal));
        //                        paramCollection.Add(new DBParameter("@piTahap", _pTahap, DbType.Int16));

                            
        //                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
        //                }
        //                else // UNTUK PERUBAHAN 
        //                {
        //                    DBParameterCollection paramCollection = new DBParameterCollection();

        //                    //if (hanyaPagu)
        //                    //{
        //                    //    SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
        //                    //           " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btTahapInput," +
        //                    //        "IIDRekening, bPPKD,cPlafonABT)  values ( " +
        //                    //        "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
        //                    //        "@pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtTahapInput,@pIIDRekening," +
        //                    //        "@pbPPKD,@pcPlafonABT)";


        //                    //    paramCollection.Add(new DBParameter("@pTahun", o.Tahun));
        //                    //    paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
        //                    //    paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
        //                    //    paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
        //                    //    paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
        //                    //    paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana));
        //                    //    paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana));
        //                    //    paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori));
        //                    //    paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan));
        //                    //    paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD));
        //                    //    paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK));
        //                    //    paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
        //                    //    paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram));
        //                    //    paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan));
        //                    //    paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput));
        //                    //    paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
        //                    //    paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));
        //                    //    paramCollection.Add(new DBParameter("@pcPlafonABT", o.PlafonABT, DbType.Decimal));
                                
        //                    //}
        //                    //else
        //                    //{
        //                        SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
        //                               " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btTahapInput," +
        //                            "IIDRekening,cJumlah,bPPKD,cJumlahRKA,cJumlahMurni,cJumlahGeser, cJumlahRKAP,cJumlahABT,cPlafon, cPlafonABT,iTahap,isImport)  values ( " +
        //                            "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
        //                            "@pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtTahapInput,@pIIDRekening," +
        //                            "0,@pbPPKD,0,0,0,@pcPlafonABT,@pcPlafonABT,@pcPlafon,@pcPlafonABT, @piTahap,1)";


        //                        paramCollection.Add(new DBParameter("@pTahun", o.Tahun));
        //                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));///// ini di ex
        //                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
        //                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
        //                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
        //                        paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana));
        //                        paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana));
        //                        paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori));
        //                        paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan));
        //                        paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD));
        //                        paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK));
        //                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
        //                        paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram));
        //                        paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan));
        //                        paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput));
        //                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
        //                        paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));
        //                        paramCollection.Add(new DBParameter("@pcPlafon", o.Plafon, DbType.Decimal));
        //                        paramCollection.Add(new DBParameter("@pcPlafonABT", o.PlafonABT, DbType.Decimal));
        //                        paramCollection.Add(new DBParameter("@piTahap", _pTahap, DbType.Int16));
        //                   // }
        //                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

        //                }

        //            }
        //            else
        //            {



        //                if (_pTahap >= 3)
        //                {
        //                    DBParameterCollection paramCollection = new DBParameterCollection();

        //                    if (hanyaPagu)
        //                    {
        //                        SSQL = "UPDATE " + m_sNamaTabel + " SET cPlafonABT =@pcPlafonABT WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
        //                             " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and btJenis=@pbtJenis  and isnull(bPPKD,0)=@ptbPPKD ";


                            
        //                        paramCollection.Add(new DBParameter("@pcPlafonABT", o.PlafonABT));
        //                        paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
        //                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
        //                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
        //                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
        //                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
        //                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
        //                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
        //                        paramCollection.Add(new DBParameter("@ptbPPKD", o.PPKD));
                            
        //                    }
        //                    else {
        //                        SSQL = "UPDATE " + m_sNamaTabel + " SET cJumlahRKAP=@pcPlafonABT, CJumlahABT=@pcPlafonABT,cPlafonABT =@pcPlafonABT, isImport=1 WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
        //                             " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and btJenis=@pbtJenis  and isnull(bPPKD,0)=@ptbPPKD ";

        //                        if (o.Plafon != o.PlafonABT)
        //                        {
        //                            o.Plafon = o.Plafon;
        //                        }


        //                        paramCollection.Add(new DBParameter("@pcPlafonABT", o.PlafonABT));
        //                        // paramCollection.Add(new DBParameter("@pcPlafon1", o.Plafon));
        //                        paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
        //                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
        //                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
        //                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
        //                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
        //                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
        //                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
        //                        paramCollection.Add(new DBParameter("@ptbPPKD", o.PPKD));
                            
        //                    }
        //                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
        //                }
        //                else
        //                { // Murni
        //                    DBParameterCollection paramCollection = new DBParameterCollection();
        //                    if (hanyaPagu)
        //                    {

        //                        SSQL = "UPDATE " + m_sNamaTabel + " SET cPlafon =@pcPlafon WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
        //                             " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and btJenis=@pbtJenis  and isnull(bPPKD,0)=@ptbPPKD ";



        //                        paramCollection.Add(new DBParameter("@pcPlafon", o.Plafon));
        //                        paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
        //                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
        //                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
        //                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
        //                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
        //                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
        //                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
        //                        paramCollection.Add(new DBParameter("@ptbPPKD", o.PPKD));
                                
        //                    }
        //                    else
        //                    {
        //                        SSQL = "UPDATE " + m_sNamaTabel + " SET cJumlahMurni=@pcPlafon,cJumlahRKA=@pcPlafon, cPlafon =@pcPlafon,cPlafonABT =@pcPlafon, cJumlah = @pcPlafon1,isImport=1 WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
        //                             " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and btJenis=@pbtJenis  and isnull(bPPKD,0)=@ptbPPKD ";


        //                        paramCollection.Add(new DBParameter("@pcPlafon", o.Plafon));
        //                        paramCollection.Add(new DBParameter("@pcPlafonABT", o.PlafonABT));
        //                        paramCollection.Add(new DBParameter("@pcPlafon1", o.Plafon));
        //                        paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
        //                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
        //                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
        //                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
        //                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
        //                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
        //                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
        //                        paramCollection.Add(new DBParameter("@ptbPPKD", o.PPKD));
                                

        //                    }
        //                     _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

        //                }

        //            }

        //            Rekening oRek = new Rekening();
        //            oRek.ID = o.IDRekening;
        //            oRek.Nama = o.Nama;                    
        //            oRekLogic.ProsesImport(oRek);

        //        }
        //       // transaction.Commit();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //       // transaction.Rollback();
        //        _isError = true;
        //        _lastError = ex.Message;
        //        return false;


        //    }
        //}

        public bool SimpanImport2(List<TAnggaranRekening> _lst, int pIDDinas, int _piTahun, Single _pTahap, bool hanyaPagu)
        {


            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;
            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSKPD;
            int _KodeUK;


            // IDbTransaction transaction = _dbHelper.GetConnObject().BeginTransaction();
            try
            {
                RekeningLogic oRekLogic = new RekeningLogic(Tahun, RekeningLogic.E_REKENING_TYPE.REKENING_13);

                    //if (hanyaPagu)
                    SSQL = "DELETE tANGGARANREKENING_A  WHERE iTahun= " + _piTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString();// +" AND iTahap=" + _pTahap.ToString();
                    //else
                     //   SSQL = "UPDATE tANGGARANREKENING_A SET cJumlah=0, cPlafon = 0,cJumlahRKA=0,cJumlahRKAP=0,cJumlahABT=0,   cJumlahMurni=0, cPlafonABT = 0 ,isImport=0 WHERE iTahun= " + _piTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString() + " AND iTahap=" + _pTahap.ToString();
                    _dbHelper.ExecuteNonQuery(SSQL);
             
                foreach (TAnggaranRekening o in _lst)
                {

                    if (o.Jenis == 3)
                    {
                        _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                    }
                    else
                    {
                        _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                        o.IDKegiatan = 0;
                        o.IDProgram = 0;
                    }
                    _KodeKategori = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                    _KodeUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    _KodeSKPD = DataFormat.GetInteger(o.IDDinas.ToString().Substring(3, 2));
                    if (o.IDDinas.ToString().Length > 5)
                    {
                        _KodeUK = DataFormat.GetInteger(o.IDDinas.ToString().Substring(5, 2));
                    }
                    else
                    {
                        _KodeUK = 0;
                    }

                    if (o.IDKegiatan == 10216013)
                    {
                        _KodeUK = DataFormat.GetInteger(o.IDDinas.ToString().Substring(5, 2));
                    }
                    else
                    {
                        _KodeUK = 0;
                    }

                    // Cek Apakah sudah ada?
                    o.StatusUpdate = 1;
                    if (CekAda(o) == 0)
                    {
                        o.StatusUpdate = 0;
                    }

                    if (o.StatusUpdate == 0)
                    {
                        if (o.Tahap < 3)
                        {
                            DBParameterCollection paramCollection = new DBParameterCollection();
                            SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btTahapInput," +
                                   "IIDRekening,cJumlah,bPPKD,cJumlahRKA,cPlafon,cJumlahMurni,cJumlahGeser, cJumlahRKAP,cJumlahABT,cPlafonABT,iTahap,isImport)  values ( " +
                                    "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                    "@pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtTahapInput,@pIIDRekening," +
                                    "@pcPlafon,@pbPPKD,@pcPlafon,@pcPlafon,@pcPlafon,@pcPlafon,@pcPlafon,@pcPlafon,@pcPlafonABT, @piTahap,1)";



                            paramCollection.Add(new DBParameter("@pTahun", o.Tahun));
                            paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                            paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                            paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                            paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                            paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana));
                            paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana));
                            paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori));
                            paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan));
                            paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD));
                            paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK));
                            paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                            paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram));
                            paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan));
                            paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput));
                            paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                            paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));
                            paramCollection.Add(new DBParameter("@pcPlafon", o.Plafon, DbType.Decimal));
                            paramCollection.Add(new DBParameter("@pcPlafonABT", o.PlafonABT, DbType.Decimal));
                            paramCollection.Add(new DBParameter("@piTahap", _pTahap, DbType.Int16));


                            _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                        }
                        else // UNTUK PERUBAHAN 
                        {
                            DBParameterCollection paramCollection = new DBParameterCollection();

                            SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btTahapInput," +
                                "IIDRekening,cJumlah,bPPKD,cJumlahRKA,cJumlahMurni,cJumlahGeser, cJumlahRKAP,cJumlahABT,cPlafon, cPlafonABT,iTahap,isImport)  values ( " +
                                "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                "@pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtTahapInput,@pIIDRekening," +
                                "0,@pbPPKD,0,0,0,@pcPlafonABT,@pcPlafonABT,@pcPlafon,@pcPlafonABT, @piTahap,1)";


                            paramCollection.Add(new DBParameter("@pTahun", o.Tahun));
                            paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));///// ini di ex
                            paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                            paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                            paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                            paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana));
                            paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana));
                            paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori));
                            paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan));
                            paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD));
                            paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK));
                            paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                            paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram));
                            paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan));
                            paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput));
                            paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                            paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));
                            paramCollection.Add(new DBParameter("@pcPlafon", o.Plafon, DbType.Decimal));
                            paramCollection.Add(new DBParameter("@pcPlafonABT", o.PlafonABT, DbType.Decimal));
                            paramCollection.Add(new DBParameter("@piTahap", _pTahap, DbType.Int16));
                            // }
                            _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                        }

                    }
                    else
                    {



                        if (o.Tahap >= 3)
                        {
                            DBParameterCollection paramCollection = new DBParameterCollection();

                           
                                SSQL = "UPDATE " + m_sNamaTabel + " SET cJumlahRKAP=@pcPlafonABT, CJumlahABT=@pcPlafonABT,cPlafonABT =@pcPlafonABT, isImport=1 WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
                                     " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and btJenis=@pbtJenis  and isnull(bPPKD,0)=@ptbPPKD ";

                                if (o.Plafon != o.PlafonABT)
                                {
                                    o.Plafon = o.Plafon;
                                }


                                paramCollection.Add(new DBParameter("@pcPlafonABT", o.PlafonABT));
                                // paramCollection.Add(new DBParameter("@pcPlafon1", o.Plafon));
                                paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                                paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                                paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                                paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                                paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                                paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                                paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                                paramCollection.Add(new DBParameter("@ptbPPKD", o.PPKD));

                            
                            _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                        }
                        else
                        { // Murni
                            DBParameterCollection paramCollection = new DBParameterCollection();
                            
                                SSQL = "UPDATE " + m_sNamaTabel + " SET cJumlahMurni=@pcPlafon,cJumlahRKA=@pcPlafon, cPlafon =@pcPlafon,cPlafonABT =@pcPlafon, cJumlah = @pcPlafon1,isImport=1 WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
                                     " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and btJenis=@pbtJenis  and isnull(bPPKD,0)=@ptbPPKD ";


                                paramCollection.Add(new DBParameter("@pcPlafon", o.Plafon));
                                paramCollection.Add(new DBParameter("@pcPlafonABT", o.PlafonABT));
                                paramCollection.Add(new DBParameter("@pcPlafon1", o.Plafon));
                                paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                                paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                                paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                                paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                                paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                                paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                                paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                                paramCollection.Add(new DBParameter("@ptbPPKD", o.PPKD));


                            
                            _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                        }

                    }

                    Rekening oRek = new Rekening();
                    oRek.ID = o.IDRekening;
                    oRek.Nama = o.Nama;
                    oRekLogic.ProsesImport(oRek);

                }
                // transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                // transaction.Rollback();
                _isError = true;
                _lastError = ex.Message;
                return false;


            }
        }

        public bool SimpanImport3(List<TAnggaranRekening> _lst, int pIDDinas, int _piTahun, Single _pTahap, bool hanyaPagu)
        {


            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;
            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSKPD;
            int _KodeUK;


            // IDbTransaction transaction = _dbHelper.GetConnObject().BeginTransaction();
            try
            {
                RekeningLogic oRekLogic = new RekeningLogic(Tahun, RekeningLogic.E_REKENING_TYPE.REKENING_13);

    
                SSQL = "Update tANGGARANREKENING_A set cplafon = 0  WHERE iTahun= " + _piTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString();// +" AND iTahap=" + _pTahap.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);

                foreach (TAnggaranRekening o in _lst)
                {

                    if (o.Jenis == 3)
                    {
                        _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                        _KodeKategori = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                        _KodeUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                        _KodeSKPD = DataFormat.GetInteger(o.IDDinas.ToString().Substring(3, 2));
                    }
                    else
                    {
                        _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                        _KodeKategori = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                        _KodeUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                        o.IDKegiatan = 0;
                        o.IDProgram = 0;
                        o.IDSubKegiatan = 0;
                    }
          
                    if (o.IDDinas.ToString().Length > 5)
                    {
                        _KodeUK = DataFormat.GetInteger(o.IDDinas.ToString().Substring(5, 2));
                    }
                    else
                    {
                        _KodeUK = 0;
                    }

                    if (o.IDKegiatan == 10216013)
                    {
                        _KodeUK = DataFormat.GetInteger(o.IDDinas.ToString().Substring(5, 2));
                    }
                    else
                    {
                        _KodeUK = 0;
                    }

                    // Cek Apakah sudah ada?
                    o.StatusUpdate = 1;
                    if (CekAda3(o) == 0)
                    {
                        o.StatusUpdate = 0;
                    }

                    if (o.StatusUpdate == 0)
                    {
                        
                            DBParameterCollection paramCollection = new DBParameterCollection();

                            SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,IdSubkegiatan," +
                                   "IIDRekening,bPPKD,cPlafon,cJumlahRKA, cJumlahMurni, cJumlahGeser, cJumlahRKAP, cJumlahABT,btjenis)  values ( " +
                                    "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pIdSubkegiatan,@pIIDRekening," +
                                    "@pbPPKD,@pcPlafon,@pcPlafon,@pcPlafon,@pcPlafon,@pcPlafon,@pcPlafon @pbtjenis)";



                            paramCollection.Add(new DBParameter("@pTahun", o.Tahun));
                            paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                            paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                            paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                            paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                            paramCollection.Add(new DBParameter("@pIdSubkegiatan", o.IDSubKegiatan));
                            paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                            paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));
                            paramCollection.Add(new DBParameter("@pcPlafon", o.Plafon, DbType.Decimal));
                            paramCollection.Add(new DBParameter("@pbtjenis", o.Jenis, DbType.Int16));


                            _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                        

                    }
                    else
                    {



                            DBParameterCollection paramCollection = new DBParameterCollection();

                            SSQL = "UPDATE " + m_sNamaTabel + " SET CpLAFON =@pcPlafon WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
                                 " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and   idsubkegiatan = @pidsubkegiatan and isnull(bPPKD,0)=@ptbPPKD ";

                            //if (o.Plafon != o.PlafonABT)
                            //{
                            //    o.Plafon = o.Plafon;
                            //}


                            paramCollection.Add(new DBParameter("@pcPlafon", o.Plafon));
                            // paramCollection.Add(new DBParameter("@pcPlafon1", o.Plafon));
                            paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                            paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                            paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                            paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                            paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                            paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                            paramCollection.Add(new DBParameter("@pidsubkegiatan", o.IDSubKegiatan));

                            paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                            paramCollection.Add(new DBParameter("@ptbPPKD", o.PPKD));


                            _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                        

                    }


                }
                // transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                // transaction.Rollback();
                _isError = true;
                _lastError = ex.Message;
                return false;


            }
        }


        public bool ImportRKBMD(List<TAnggaranRekening> _lst, int pIDDinas, int _piTahun, Single _pTahap)
        {


            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;
            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSKPD;
            int _KodeUK;


            // IDbTransaction transaction = _dbHelper.GetConnObject().BeginTransaction();
            try
            {
                RekeningLogic oRekLogic = new RekeningLogic(Tahun, RekeningLogic.E_REKENING_TYPE.REKENING_13);

                
                foreach (TAnggaranRekening o in _lst)
                {

                    if (o.Jenis == 3)
                    {
                        _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                    }
                    else
                    {
                        _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                        o.IDKegiatan = 0;
                        o.IDProgram = 0;
                    }
                    _KodeKategori = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                    _KodeUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    _KodeSKPD = DataFormat.GetInteger(o.IDDinas.ToString().Substring(3, 2));
                    if (o.IDDinas.ToString().Length > 5)
                    {
                        _KodeUK = DataFormat.GetInteger(o.IDDinas.ToString().Substring(5, 2));
                    }
                    else
                    {
                        _KodeUK = 0;
                    }

                    // Cek Apakah sudah ada?
                    o.StatusUpdate = 1;
                    if (CekAda(o) == 0)
                    {
                        o.StatusUpdate = 0;
                    }

                    if (o.StatusUpdate == 0)
                    {
                        SSQL = "INSERT INTO " + m_sNamaTabel + "(iTahun,IDDInas,IDProgram, IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                               " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK,btJenis, btIDProgram, btIDKegiatan,btTahapInput," +
                               "IIDRekening,cJumlah,bPPKD,cJumlahRKA,cPlafon, cPlafonABT,iTahap,isImport)  values ( " +
                                "@pTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana," +
                                "@pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeuK,@pbtJenis,@pbtIDrogram,@pbtIDKegiatan,@pbtTahapInput,@pIIDRekening," +
                                "@pcPlafon,@pbPPKD,@pcPlafon,@pcPlafon,@pcPlafonABT, @piTahap,1)";


                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pTahun", o.Tahun));
                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                        paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana));
                        paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana));
                        paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori));
                        paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan));
                        paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD));
                        paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK));
                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                        paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram));
                        paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan));
                        paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput));
                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                        paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));
                        paramCollection.Add(new DBParameter("@pcPlafon", o.Plafon, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pcPlafonABT", o.PlafonABT, DbType.Decimal));
                        paramCollection.Add(new DBParameter("@piTahap", _pTahap, DbType.Int16));
                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    }
                    else
                    {
                        SSQL = "UPDATE " + m_sNamaTabel + " SET cJumlahRKA=@pcPlafon, cPlafon =@pcPlafon,cPlafonABT =@pcPlafonABT, cJumlah = @pcPlafon1,isImport=1 WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
                             " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and btJenis=@pbtJenis  and isnull(bPPKD,0)=@ptbPPKD ";


                        //SSQL = "UPDATE " + m_sNamaTabel + " SET cPlafon =8989 WHERE iTahun=2017 AND IDDInas=1160100 AND IDProgram=11606 AND IDkegiatan=1160603 " +
                        //    " AND IDUrusan=116 and IIDRekening=5210101 and btJenis=3";

                        //_dbHelper.ExecuteNonQuery(SSQL);

                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pcPlafon", o.Plafon));
                        paramCollection.Add(new DBParameter("@pcPlafonABT", o.PlafonABT));
                        paramCollection.Add(new DBParameter("@pcPlafon1", o.Plafon));
                        paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                        paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                        paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                        paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                        paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                        paramCollection.Add(new DBParameter("@ptbPPKD", o.PPKD));
                        //paramCollection.Add(new DBParameter("@piTahap", _pTahap, DbType.Int16));

                        //paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));
                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                    }

                    Rekening oRek = new Rekening();
                    oRek.ID = o.IDRekening;
                    oRek.Nama = o.Nama;
                    oRekLogic.ProsesImport(oRek);

                }
                // transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                // transaction.Rollback();
                _isError = true;
                _lastError = ex.Message;
                return false;


            }
        }
        public int CakApakegiatanAdaRekeningnya(int idDinas, int idKegiatan)
        {
            SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE iTahun =" + Tahun.ToString() + " AND IDDInas=" + idDinas.ToString() +
                         " AND IDKegiatan=" + idKegiatan.ToString() ;

            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL);
            if (dt != null)
            {
                return dt.Rows.Count;
            }
            else
            {
                return 0;
            }

        }
        private int CekAda(TAnggaranRekening o ){

            SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE iTahun =" + o.Tahun.ToString() + " AND IDDInas=" + o.IDDinas.ToString() +
                      " AND btKodeUK  = " + o.KodeUK.ToString() + " AND IDUrusan=" + o.IDUrusan.ToString() + " AND idprogram=" + o.IDProgram.ToString() + " AND IDKegiatan=" + o.IDKegiatan.ToString() + " AND IDSubKegiatan=" + o.IDSubKegiatan.ToString() + "  AND btJenis=" + o.Jenis.ToString() + " AND IIDRekening=" + o.IDRekening.ToString();

            //         " AND cJumlahABT = 0 and cJumlahRKAP is not Null"; 
                        
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    return dt.Rows.Count;
                } else{
                    return 0;
                }
        }
        private int CekAdaEx(int tahun ,int iddinas, long idsubkegiatan, long iidrekening, int kodeuk  )
        {
          
            SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE iTahun =" + tahun  + " AND IDDInas=" + iddinas.ToString() + " AND btKOdeuk =" + kodeuk.ToString() +
                      " AND IDSubKegiatan=" + idsubkegiatan.ToString() + "  AND IIDRekening=" + iidrekening.ToString();

            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL);
            if (dt != null)
            {
                return dt.Rows.Count;
            }
            else
            {
                return 0;
            }
        }
        private int CekAda3(TAnggaranRekening o)
        {

            SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE iTahun =" + o.Tahun.ToString() + " AND IDDInas=" + o.IDDinas.ToString() +
                  " AND IDUrusan=" + o.IDUrusan.ToString() + " AND idprogram=" + o.IDProgram.ToString() +
                  " AND IDKegiatan=" + o.IDKegiatan.ToString() + "  AND IDSubKegiatan=" + o.IDSubKegiatan.ToString() + 
                  " AND IIDRekening=" + o.IDRekening.ToString();

            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL);
            if (dt != null)
            {
                return dt.Rows.Count;
            }
            else
            {
                return 0;
            }
        }
        public bool Update (TAnggaranRekening o)
        {
            SSQL = "UPDATE " + m_sNamaTabel + " SET cJumlahOlah=@pcJumlahOlah WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
                    " AND IDUrusan=@pIDUrusan AND btKodekategoriPelaksana=@pbtKodekategoriPelaksana AND btKodeUrusanPelaksana=@pbtKodeUrusanPelaksana AND btKodeKategori =@pbtKodeKategori " +
                    " AND btKodeUrusan=@pbtKodeUrusan AND btKodeSKPD=@pbtKodeSKPD  AND btKodeUK=@pbtKodeUK AND btIDrogram=@pbtIDrogram " +
                    " AND btIDKegiatan=@pbtIDKegiatan, IIDRekening=@pIIDRekening,btJenis=@pbtJenis AND isnull(bPPKD,0)=@pbPPKD and iTahap=@piTahap";

            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@pcJumlahOlah",o.JumlahOlah));
            paramCollection.Add(new DBParameter("@piTahun",o.Tahun)); 
            paramCollection.Add(new DBParameter("@pIDDInas",o.IDDinas));
            paramCollection.Add(new DBParameter("@pIDProgram",o.IDProgram)); 
            paramCollection.Add(new DBParameter("@pIDkegiatan",o.IDKegiatan));
            paramCollection.Add(new DBParameter("@pIDUrusan",o.IDUrusan));
            paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana",o.KodeKategoriPelaksana));
            paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana",o.KodeUrusanPelaksana));
            paramCollection.Add(new DBParameter("@pbtKodeKategori",o.KodeKategori));
            paramCollection.Add(new DBParameter("@pbtKodeUrusan",o.KodeUrusan ));
            paramCollection.Add(new DBParameter("@pbtKodeSKPD",o.KodeSKPD ));
            paramCollection.Add(new DBParameter("@pbtKodeUK",o.KodeUK));
            paramCollection.Add(new DBParameter("@pbtIDrogram",o.KodeProgram));
            paramCollection.Add(new DBParameter("@pbtIDKegiatan",o.KodeKegiatan));
            paramCollection.Add(new DBParameter("@pIIDRekening",o.IDRekening));
            paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
            paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD));
            paramCollection.Add(new DBParameter("@piTahap", o.Tahap, DbType.Int16));
            if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

            
        }

        public List<TAnggaranRekening> GetMinRealisasi(int iTahun, int idDInas, int idUrusan,
                 int idProgram, long idKegiatan, long idSubKegiatan,
                     int pTahap, DateTime tanggal,
                          int idUnit =0 )
        {
            List<TAnggaranRekening> _lst = new List<TAnggaranRekening>();
            try
            {
                GetKolom(pTahap);

                SSQL = "SELECT A.iTahun,A.IDProgram,A.IDKegiatan,A.IDUrusan,A.IDDInas,A.btTahapInput,A.IIDRekening,A." + _namaKolom1 + " as JumlahMurni,A." + _namaKolom2 + " as  Jumlah,A.JumlahYADAPBD,A.cDPA,A.bPPKD,A.btJenis,A.cPlafon," +
                    "A.btTahapInput,A.cJumlahYAD, B.sNamaRekening as Nama,A.IDSubKegiatan, (SELECT SUM(cJumlah * DEBET) FROM Realisasi04 where itahun = a.itahun and IDUrusan = a.idurusan and iddinas = a.iddinas and IDSUbKegiatan = A.IDSUbKegiatan  and dtBukukas <=@Tanggal  " +
                      " AND IIDRekening = A.IIDRekening ) as Realisasi  FROM tANggaranRekening_A A INNER JOIN mRekening B on " +
                    " B.IIDrekening= A.IIDRekening WHERE A.iTahun =@TAHUN AND A.IDDInas=@IDDINAS "  +
                    " AND A.IDUrusan= @IDUrusan  AND A.idprogram=@IDProgram AND A.IDKegiatan=@IDKEGIATAN AND A.IDSubKegiatan = @IDSUBKEGIATAN  AND isnull(A.btKodeUK ,0)=@UNIT   ORDER BY A.IIDrekening";
                
             
               DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@Tanggal",tanggal,DbType.Date));
               paramCollection.Add(new DBParameter("@TAHUN", iTahun));
               paramCollection.Add(new DBParameter("@IDDINAS",idDInas));
               paramCollection.Add(new DBParameter("@IDUrusan",idUrusan));
                paramCollection.Add(new DBParameter("@IDProgram",idProgram));
                paramCollection.Add(new DBParameter("@IDKEGIATAN", idKegiatan));
                paramCollection.Add(new DBParameter("@IDSUBKEGIATAN", idSubKegiatan));
                paramCollection.Add(new DBParameter("@UNIT",idUnit));

                

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TAnggaranRekening()
                                {
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    TahapInput = DataFormat.GetInteger(dr["btTahapInput"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    JumlahOlah = DataFormat.GetDecimal(dr["Jumlah"]),
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"]),
                                    JumlahYAD = DataFormat.GetDecimal(dr["cJumlahYAD"]),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]),
                                    JumlahYADAPBD = DataFormat.GetDecimal(dr["JumlahYADAPBD"]),
                                    JumlahDPA = DataFormat.GetDecimal(dr["cDPA"]),
                                    PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    Plafon = DataFormat.GetDecimal(dr["cPlafon"]),
                                    Pagu = DataFormat.GetDecimal(dr["cPlafon"]),

                                    Tahap = DataFormat.GetSingle(dr["btTahapInput"]),
                                    IDSubKegiatan = DataFormat.GetInteger(dr["IDSubKegiatan"]),
                                    ListUraian = GetUraian(iTahun, idDInas, idUrusan, idProgram, idKegiatan, DataFormat.GetLong(dr["IIDRekening"]), pTahap, idSubKegiatan),
                                    StatusUpdate = 1,
                                    Realisasi = DataFormat.GetDecimal(dr["Realisasi"])


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
        
        public List<TAnggaranRekening> GetfromList(
            int Tahun, 
            int idDInas, 
            int kodeUnit,
            int idUrusan, 
            int idProgram, 
            int _idKegiatan, 
            long idSubKegiatan,
            int _pJenis, 
            int _pTahap
             )
        {
            List<TAnggaranRekening> _lst = new List<TAnggaranRekening>();
            try
            {
                GetKolom(_pTahap);


                DBParameterCollection paramCollection = new DBParameterCollection();
                if (_pJenis == 3)
                {

                    SSQL = "SELECT A.iTahun,A.IDProgram,A.IDKegiatan,A.IDUrusan,A.IDDInas,A.btTahapInput,A.IIDRekening,A." + _namaKolom1 + " as JumlahMurni,A." + _namaKolom2 + " as  Jumlah,A.JumlahYADAPBD,A.cDPA,A.bPPKD,A.btJenis,A.cPlafon," +
                    "A.btTahapInput,A.cJumlahYAD, B.sNamaRekening as Nama,A.IDSubKegiatan, (SELECT SUM(cJumlah * DEBET) FROM Realisasi04 where IDDInas = A.IDDinas " +
                    " AND IDKegiatan = A.IDKegiatan AND IIDRekening= A.IIDRekening) as Realisasi  FROM " + m_sNamaTabel + " A INNER JOIN mRekening B on " +
                    " B.IIDrekening= A.IIDRekening WHERE A.iTahun =@TAHUN AND btKodeUK=@KodeUK  AND IDDINAS= @DINAS AND IDSUBKegiatan =@IDSUBKEGiatan ";

                    paramCollection.Add(new DBParameter("@KodeUK", kodeUnit));
                 
                }




                else
                {
                    SSQL = "SELECT A.iTahun,A.IDProgram,A.IDKegiatan,A.IDUrusan,A.IDDInas,A.btTahapInput,A.IIDRekening,A." + _namaKolom1 + " as JumlahMurni,A." + _namaKolom2 + " as  Jumlah,A.JumlahYADAPBD,A.cDPA,A.bPPKD,A.btJenis,A.cPlafon," +
                    "A.btTahapInput,A.cJumlahYAD, B.sNamaRekening as Nama,A.IDSubKegiatan, (SELECT SUM(cJumlah * DEBET) FROM Realisasi04 where IDDInas = A.IDDinas " +
                    "  AND IIDRekening= A.IIDRekening) as Realisasi  FROM " + m_sNamaTabel + " A INNER JOIN mRekening B on " +
                    " B.IIDrekening= A.IIDRekening WHERE A.iTahun =@TAHUN AND IDDINAS= @DINAS AND IDSUBKegiatan =@IDSUBKEGiatan AND btJenis =@Jenis";

                }

                    paramCollection.Add(new DBParameter("@TAHUN", Tahun));
                    paramCollection.Add(new DBParameter("@DINAS", idDInas));
                    paramCollection.Add(new DBParameter("@IDSUBKEGiatan", idSubKegiatan));
                    paramCollection.Add(new DBParameter("@Jenis", _pJenis));

                        
                    DataTable dt = new DataTable();
                    dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            _lst = (from DataRow dr in dt.Rows
                                    select new TAnggaranRekening()
                                    {
                                        IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                        Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                        IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                        IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                        KodeKegiatan = DataFormat.GetInteger(dr["IDUrusan"]),
                                        IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                        TahapInput = DataFormat.GetInteger(dr["btTahapInput"]),
                                        Nama = DataFormat.GetString(dr["Nama"]),
                                        IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                        JumlahOlah = DataFormat.GetDecimal(dr["Jumlah"]),
                               
                                        JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]),
                                      
                                        JumlahDPA = DataFormat.GetDecimal(dr["cDPA"]),
                                        PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                                        Jenis = DataFormat.GetInteger(dr["btJenis"]),
                               
                                        Tahap = DataFormat.GetSingle(dr["btTahapInput"]),
                                        IDSubKegiatan = DataFormat.GetInteger(dr["IDSubKegiatan"]),
                                        ListUraian =null,// GetUraian(_iTahun, idDInas, idUrusan, idProgram, _idKegiatan, DataFormat.GetLong(dr["IIDRekening"]), _pJenis, _ppkd, _pTahap, _t, idSubKegiatan),
                                        StatusUpdate = 1,
                                        Realisasi = DataFormat.GetDecimal(dr["Realisasi"])


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
        //Get(GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram,
        //m_IDKegiatan, _iJenis, _bPPKD, 2, 1,0);

        public List<TAnggaranRekening> Get(int _iTahun, int  idDInas, int idUrusan,
            int idProgram, int _idKegiatan, int _pJenis, int _ppkd, int _pTahap, Single _t, 
            long idsubkegiatan, int idunit=0)
        {
            List<TAnggaranRekening> _lst = new List<TAnggaranRekening>();
            try
            {
                string strDinas;
                
                strDinas = "(" + idDInas.ToString()+")";


                if (_pJenis != 3)
                {
                }

                GetKolom(_pTahap);


                SSQL = "SELECT A.iTahun,A.IDProgram,A.IDKegiatan,A.IDSubKegiatan,A.IDUrusan,A.IDDInas,A.IIDRekening, SUM(A." + _namaKolom1 + ") as JumlahMurni,Sum(A." + _namaKolom2 + ") as  Jumlah, SUM(A.cDPA) as cDPA,A.bPPKD , B.sNamaRekening as Nama, A.btTahapInput FROM " + m_sNamaTabel + " A INNER JOIN mRekening B on " +
                           " B.IIDrekening= A.IIDRekening WHERE A.iTahun =" + _iTahun.ToString() + " AND A.IDDInas in " + strDinas +
                           " AND A.IDUrusan=" + idUrusan.ToString() + " AND A.idprogram=" + idProgram.ToString() + " AND A.IDKegiatan=" + _idKegiatan.ToString() + " AND IDSubKegiatan=" + idsubkegiatan.ToString () + " AND A.btJenis=" + _pJenis.ToString() + " AND isnull(A.bPPKD,0)=" + _ppkd.ToString()   +
                           " GROUP BY A.iTahun,A.IDProgram,A.IDKegiatan,A.IDSubKegiatan, A.IDUrusan,A.IDDInas,A.IIDRekening,A.bPPKD , B.sNamaRekening , A.btTahapInput " +                           
                           " ORDER BY A.IIDrekening";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TAnggaranRekening()
                                {
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSUBKegiatan"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    TahapInput = DataFormat.GetInteger(dr["btTahapInput"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    JumlahOlah = DataFormat.GetDecimal(dr["Jumlah"]),
                                    JumlahYAD =0,
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]),
                                    JumlahYADAPBD = 0,
                                    JumlahDPA = DataFormat.GetDecimal(dr["cDPA"]),
                                    PPKD =0,
                                    Jenis =3,
                                    Plafon = 0,
                                    Tahap = DataFormat.GetSingle(dr["btTahapInput"]),
                                    ListUraian = GetUraian(DataFormat.GetInteger(dr["iTahun"]),                                                
                                                DataFormat.GetInteger(dr["IDDInas"]),
                                                DataFormat.GetInteger(dr["IDUrusan"]),
                                                DataFormat.GetInteger(dr["IDProgram"]),
                                                DataFormat.GetInteger(dr["IDKegiatan"]),
                                                DataFormat.GetLong(dr["IIDRekening"]),
                                                _pTahap, DataFormat.GetLong(dr["IDSUBKegiatan"])),
                                    StatusUpdate = 1


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
        public List<TAnggaranRekening> Get(int _iTahun, bool byPass= false)
        {
            List<TAnggaranRekening> _lst = new List<TAnggaranRekening>();
            try
            {

                if (byPass == false)
                {
                    SSQL = "SELECT A.iTahun,A.IDProgram,A.IDKegiatan, A.IDSubKegiatan,A.IDUrusan,A.IDDInas,A.IIDRekening, cJumlahMurni, cJumlahGeser, cJumlahRKAP, cJumlahABT, A.cDPA,A.bPPKD , a.btJenis,B.sNamaRekening as Nama, A.btTahapInput FROM tAnggaranRekening_A " +
                    " A INNER JOIN mRekening B on " +
                     " B.IIDrekening= A.IIDRekening WHERE A.iTahun =" + _iTahun.ToString() +
                     " ORDER BY A.IIDrekening";
                }
                else
                {
                    SSQL = "SELECT 2024 as iTahun,0 as IDProgram,0 as IDKegiatan, 0 as IDSubKegiatan,0 as IDUrusan,0 as IDDInas,A.IIDRekening, cJumlahMurni, cJumlahGeser, cJumlahRKAP, cJumlahABT, 0 as cDPA,0 as bPPKD , 0 as btJenis,B.sNamaRekening as Nama, 0 as btTahapInput FROM tANggaranByPass "+
                             " A INNER JOIN mRekening B on " +
                              " B.IIDrekening= A.IIDRekening WHERE A.iTahun =" + _iTahun.ToString() +
                              " ORDER BY A.IIDrekening";
            
                }

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TAnggaranRekening()
                                {
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IdSubKegiatan"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    TahapInput = DataFormat.GetInteger(dr["btTahapInput"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    JumlahMurni = DataFormat.GetDecimal(dr["cJumlahMurni"]),
                                    JumlahPergeseran = DataFormat.GetDecimal(dr["cJumlahGeser"]),
                                    JumlahRKAP = DataFormat.GetDecimal(dr["cJumlahRKAP"]),
                                    JumlahABT = DataFormat.GetDecimal(dr["cJumlahABT"]),
                                    PPKD = 0,
                                    Jenis  = DataFormat.GetInteger(dr["btJenis"]),
                                    Tahap = DataFormat.GetSingle(dr["btTahapInput"]),


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
        public List<TAnggaranRekening> Get(int _iTahun, List<SKPD> idDInas, int idUrusan, int idProgram, int _idKegiatan, int _pJenis, int _ppkd, int _pTahap, Single _t, long idsubkegiatan)
        {
            List<TAnggaranRekening> _lst = new List<TAnggaranRekening>();
            try
            {
                string strDinas ="(";
                foreach (SKPD d in idDInas)
                {
                    strDinas = strDinas + d.ID.ToString() + ",";
                }
                strDinas = strDinas + "99)";


                if (_pJenis != 3)
                {
                }

                GetKolom(_pTahap);


                SSQL = "SELECT A.iTahun,A.IDProgram,A.IDKegiatan,A.IDUrusan,A.IDDInas,A.IIDRekening, SUM(A." + _namaKolom1 + ") as JumlahMurni,Sum(A." + _namaKolom2 + ") as  Jumlah, SUM(A.cDPA) as cDPA,A.bPPKD  B.sNamaRekening as Nama FROM " + m_sNamaTabel + " A INNER JOIN mRekening B on " +
                           " B.IIDrekening= A.IIDRekening WHERE A.iTahun =" + _iTahun.ToString() + " AND A.IDDInas in " + strDinas +
                           " AND A.IDUrusan=" + idUrusan.ToString() + " AND A.idprogram=" + idProgram.ToString() + " AND A.IDKegiatan=" + _idKegiatan.ToString() + " AND IDSubKegiatan=" + idsubkegiatan.ToString () + " AND A.btJenis=" + _pJenis.ToString() + " AND isnull(A.bPPKD,0)=" + _ppkd.ToString() + " AND A.btTahapInput<= " + _t.ToString() +
                           " GROUP BY A.iTahun,A.IDProgram,A.IDKegiatan,A.IDUrusan,A.IDDInas,A.IIDRekening,A.bPPKD  B.sNamaRekening " +                           
                           " ORDER BY A.IIDrekening";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TAnggaranRekening()
                                {
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    TahapInput = DataFormat.GetInteger(dr["btTahapInput"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    JumlahOlah = DataFormat.GetDecimal(dr["Jumlah"]),
                                    JumlahYAD = DataFormat.GetDecimal(dr["cJumlahYAD"]),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]),
                                    JumlahYADAPBD = DataFormat.GetDecimal(dr["JumlahYADAPBD"]),
                                    JumlahDPA = DataFormat.GetDecimal(dr["cDPA"]),
                                    PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    Plafon = DataFormat.GetDecimal(dr["cPlafon"]),
                                    Tahap = DataFormat.GetSingle(dr["btTahapInput"]),
                                    ListUraian = GetUraianEx(_iTahun, idDInas, idUrusan, idProgram, _idKegiatan, DataFormat.GetInteger(dr["IIDRekening"]), _pJenis, _ppkd, _pTahap, _t,idsubkegiatan),
                                    StatusUpdate = 1


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

        public List<TAnggaranRekening> GetDariPerencanaan(int _iTahun, RemoteConnection sCOn)
        {
            List<TAnggaranRekening> _lst = new List<TAnggaranRekening>();
            //string sRemoteConnection ="Data Source=" + sCOn.Server + ";Initial Catalog=" + sCOn.Database + ";User ID=sa;Password=" + sCOn.Password + ";";
            //DBHelper remoteHelper = new DBHelper();
            try
            {
                SSQL = "SELECT A.iTahun,A.IDProgram,A.IDKegiatan,A.IDUrusan,A.IDDInas,A.btTahapInput,A.IIDRekening,A.cJumlahRKA as JumlahMurni,A.cJumlahRKAP as  Jumlah,A.JumlahYADAPBD,A.cDPA,A.bPPKD,A.btJenis,A.cPlafon,A.btTahapInput,A.cJumlahYAD, B.sNamaRekening as Nama FROM " + m_sNamaTabel + " A INNER JOIN mRekening B on " +
                           " B.IIDrekening= A.IIDRekening WHERE A.iTahun =" + _iTahun.ToString();

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, sCOn.GetConnection());
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TAnggaranRekening()
                                {
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    TahapInput = DataFormat.GetInteger(dr["btTahapInput"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    JumlahOlah = DataFormat.GetDecimal(dr["Jumlah"]),
                                    JumlahYAD = DataFormat.GetDecimal(dr["cJumlahYAD"]),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]),
                                    JumlahYADAPBD = DataFormat.GetDecimal(dr["JumlahYADAPBD"]),
                                    JumlahDPA = DataFormat.GetDecimal(dr["cDPA"]),
                                    PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    Plafon = DataFormat.GetDecimal(dr["cPlafon"]),
                                    Tahap = DataFormat.GetSingle(dr["btTahapInput"])
                                    
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

        public List<TAnggaranRekening> GetByKegiatan(int _iTahun, int idDInas, int idUrusan, int idProgram, int _idKegiatan, int _pJenis, int _ppkd, int _pTahap, Single _t)
        {
            List<TAnggaranRekening> _lst = new List<TAnggaranRekening>();
            try
            {
                SSQL = "SELECT A.iTahun,A.IDProgram,A.IDKegiatan,A.IDUrusan,A.IDDInas,A.btTahapInput,A.IIDRekening,A.cDPA as DPA,A.bPPKD, B.sNamaRekening as Nama FROM tAnggaranRekening_A A INNER JOIN mRekening B on " +
                           " B.IIDrekening= A.IIDRekening WHERE A.iTahun =" + _iTahun.ToString() + " AND A.IDDInas=" + idDInas.ToString() +
                           " AND A.IDUrusan=" + idUrusan.ToString() + " AND A.idprogram=" + idProgram.ToString() + " AND A.IDKegiatan=" + _idKegiatan.ToString() + " AND A.btJenis=" + _pJenis.ToString() + " AND isnull(A.bPPKD,0)=" + _ppkd.ToString() + "  ORDER BY A.IIDrekening";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TAnggaranRekening()
                                {
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    TahapInput = DataFormat.GetInteger(dr["btTahapInput"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    Jumlah = DataFormat.GetDecimal(dr["DPA"]),
                                    JumlahYAD = DataFormat.GetDecimal(dr["cJumlahYAD"]),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]),
                                    JumlahYADAPBD = DataFormat.GetDecimal(dr["JumlahYADAPBD"]),
                                    JumlahDPA = DataFormat.GetDecimal(dr["cDPA"]),
                                    PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                

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
        public List<TAnggaranRekening> GetBySubKegiatan(int _iTahun, int idDInas,  long _idAUbKegiatan)
        {
            List<TAnggaranRekening> _lst = new List<TAnggaranRekening>();
            try
            {
                SSQL = "SELECT A.iTahun,A.IDProgram,A.IDKegiatan,A.IDUrusan,A.IDDInas,A.btTahapInput,A.IIDRekening,A.cDPA as DPA,A.bPPKD, B.sNamaRekening as Nama FROM tAnggaranRekening_A A INNER JOIN mRekening B on " +
                           " B.IIDrekening= A.IIDRekening WHERE A.iTahun =" + _iTahun.ToString() + " AND A.IDDInas=" + idDInas.ToString() +
                           " AND A.IDSubKegiatan=" + _idAUbKegiatan.ToString() + "  ORDER BY A.IIDrekening";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TAnggaranRekening()
                                {
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    TahapInput = DataFormat.GetInteger(dr["btTahapInput"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    Jumlah = DataFormat.GetDecimal(dr["DPA"]),
                                  
                                    
                                   
                                 
                                    PPKD = DataFormat.GetSingle(dr["bPPKD"]),


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
        
        public List<TAnggaranRekening> GetByKegiatanMinusRealisasi(int _iTahun, int idDInas, int idUrusan, int idProgram, int _idKegiatan, int _pJenis, int _ppkd, DateTime pTanggal)
        {
            List<TAnggaranRekening> _lst = new List<TAnggaranRekening>();
            try
            {
                SSQL = "SELECT A.iTahun,A.IDProgram,A.IDKegiatan,A.IDUrusan,A.IDDInas,A.btTahapInput,A.IIDRekening,A.cDPA as DPA,A.bPPKD, B.sNamaRekening as Nama FROM tAnggaranRekening_A A INNER JOIN mRekening B on " +
                           " B.IIDrekening= A.IIDRekening WHERE A.iTahun =" + _iTahun.ToString() + " AND A.IDDInas=" + idDInas.ToString() +
                           " AND A.IDUrusan=" + idUrusan.ToString() + " AND A.idprogram=" + idProgram.ToString() + " AND A.IDKegiatan=" + _idKegiatan.ToString() + " AND A.btJenis=" + _pJenis.ToString() + " AND isnull(A.bPPKD,0)=" + _ppkd.ToString() + "  ORDER BY A.IIDrekening";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TAnggaranRekening()
                                {
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    TahapInput = DataFormat.GetInteger(dr["btTahapInput"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    Jumlah = DataFormat.GetDecimal(dr["DPA"]),
                                    JumlahYAD = DataFormat.GetDecimal(dr["cJumlahYAD"]),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]),
                                    JumlahYADAPBD = DataFormat.GetDecimal(dr["JumlahYADAPBD"]),
                                    JumlahDPA = DataFormat.GetDecimal(dr["cDPA"]),
                                    PPKD = DataFormat.GetSingle(dr["bPPKD"]),


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





        public List<TAnggaranRekening> GetPlafon(int _iTahun, int idDInas, int idUrusan, int idProgram, int _idKegiatan, int _pJenis, int _ppkd, int _pTahap, Single _t)
        {
            List<TAnggaranRekening> _lst = new List<TAnggaranRekening>();

            try
            {
                if (_pJenis != 3)
                {
                    
                }

                
                //iTahun,IDProgram,IDKegiatan,IDUrusan,IDDInas,btTahapInput,Nama,IIDRekening,cJumlahOlah,cJumlahYAD,cJumlahMurni,cJumlahGeser,JumlahYADAPBD,cDPA,bPPKD,btJenis,cPlafon,cJumlahRKAP,cJumlahRKA,iTahap

                SSQL = "SELECT A.iTahun,A.IDProgram,A.IDKegiatan,A.IDUrusan,A.IDDInas,A.btTahapInput,A.IIDRekening,A.cPlafon, A.cJumlahRKAP as  cJumlahRKAP ,A.cRealisasi ,B.sNamaRekening as Nama FROM " + m_sNamaTabel + " A INNER JOIN mRekening B on " +
                           " B.IIDrekening= A.IIDRekening WHERE A.iTahun =" + _iTahun.ToString() + " AND A.IDDInas=" + idDInas.ToString() +
                           " AND A.IDUrusan=" + idUrusan.ToString() + " AND A.idprogram=" + idProgram.ToString() + " AND A.IDKegiatan=" + _idKegiatan.ToString() + " AND A.btJenis=" + _pJenis.ToString() + " AND isnull(A.bPPKD,0)=" + _ppkd.ToString()  + " ORDER BY A.IIDrekening";

                //SSQL = "SELECT tAnggaranRekening_A.*, mRekening.sNamaRekening as Nama FROM " + m_sNamaTabel + " INNER JOIN mRekening on " +
                //   " mRekening.IIDrekening= tAnggaranRekening_A.IIDRekening WHERE iTahun =" + _iTahun.ToString() +" AND IDDInas="+ idDInas.ToString() +
                //   " AND IDUrusan=" + idUrusan.ToString() + " AND idprogram=" + idProgram.ToString() + " AND IDKegiatan=" + _idKegiatan.ToString() + " AND btJenis=" + _pJenis.ToString() + " AND isnull(bPPKD,0)=" + _ppkd.ToString() + " AND btTahapInput<= " + _t.ToString()  + " ORDER BY tAnggaranRekening_A.IIDrekening";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TAnggaranRekening()
                                {
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    TahapInput = DataFormat.GetInteger(dr["btTahapInput"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    PlafonABT = DataFormat.GetLong(dr["cJumlahRKAP"]),
                                    Realisasi= DataFormat.GetDecimal(dr["cRealisasi"]),
                                  //  PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                                  //  Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    Plafon = DataFormat.GetDecimal(dr["cJumlahRKAP"])
                                    //ListUraian = GetUraian(_iTahun, idDInas, idUrusan, idProgram, _idKegiatan, DataFormat.GetLong(dr["IIDRekening"]), _pJenis, _ppkd, _pTahap, _t),
                                    //StatusUpdate = 1


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

        public List<TAnggaranRekening> GetANggaranDanRealisasi(int _iTahun, 
            int idDInas, 
            int iKudeUK,
            int idUrusan, 
            int idProgram, 
            int _idKegiatan, 
            long idsubkegiatan, 
            int _pJenis)
        {
            List<TAnggaranRekening> _lst = new List<TAnggaranRekening>();
            string tanggalRealisasi = DateTime.Now.ToSQLFormat();
            try
            {
                if (_pJenis != 3)
                {

                }
                SSQL = "SELECT A.iTahun,A.IDProgram,A.IDKegiatan,A.IDUrusan,A.IDDInas,A.btTahapInput,A.IIDRekening,A.cJumlahMurni,A.cJumlahGeser, A.cJumlahRKAP ,A.cJumlahABT, A.cRealisasi as  cRealisasi ,B.sNamaRekening as Nama FROM " + m_sNamaTabel + " A INNER JOIN mRekening B on " +
                           " B.IIDrekening= A.IIDRekening WHERE A.iTahun =" + _iTahun.ToString() + " AND A.IDDInas=" + idDInas.ToString() + " AND btKOdeUK= "+ iKudeUK.ToString() +
                           " AND A.IDUrusan=" + idUrusan.ToString() + " AND A.idprogram=" + idProgram.ToString() + " AND A.IDKegiatan=" + _idKegiatan.ToString() + " AND A.btJenis=" + _pJenis.ToString() +
                           " AND IDSubkegiatan =" + idsubkegiatan.ToString() + " ORDER BY A.IIDrekening";

        
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TAnggaranRekening()
                                {
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    TahapInput = DataFormat.GetInteger(dr["btTahapInput"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    JumlahPergeseran= DataFormat.GetLong(dr["cJumlahGeser"]),

                                    Plafon = DataFormat.GetLong(dr["cJumlahMurni"]),
                                    JumlahRKAP = DataFormat.GetLong(dr["cJumlahRKAP"]),
                                    PlafonABT = DataFormat.GetLong(dr["cJumlahABT"]),
                                    JumlahABT = DataFormat.GetLong(dr["cJumlahABT"]),
                                    Realisasi = DataFormat.GetDecimal(dr["cRealisasi"]),
                           

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

        public List<TAnggaranRekening> GetPlafon50(int _iTahun, int idDInas, int iUnit, int idUrusan, int idProgram, int _idKegiatan,long idsubkegiatan, int _pJenis, int _ppkd, int _pTahap, Single _t)
        {
            List<TAnggaranRekening> _lst = new List<TAnggaranRekening>();

            try
            {
                if (_pJenis != 3)
                {

                }

               
                    SSQL = "SELECT A.iTahun,A.IDProgram,A.IDKegiatan,A.IDUrusan,A.IDDInas,A.btTahapInput,A.IIDRekening,A.cPlafon, A.cJumlahMurni,A.cJumlahGeser, A.cJumlahRKAP, A.cJumlahABT, A.cPlafon as  cPlafonABT ,0 as cRealisasi ,B.sNamaRekening as Nama FROM " + m_sNamaTabel + " A INNER JOIN mRekening B on " +
                               " B.IIDrekening= A.IIDRekening WHERE A.iTahun =" + _iTahun.ToString() + " AND A.IDDInas=" + idDInas.ToString() +
                               " AND A.IDUrusan=" + idUrusan.ToString() + " AND A.idprogram=" + idProgram.ToString() + " AND A.IDKegiatan=" + _idKegiatan.ToString() + " AND A.btJenis=" + _pJenis.ToString() +
                               " AND btKodeUK =" + iUnit.ToString() + "  AND IDSubkegiatan =" + idsubkegiatan.ToString() + " ORDER BY A.IIDrekening";

                    //SSQL = "SELECT tAnggaranRekening_A.*, mRekening.sNamaRekening as Nama FROM " + m_sNamaTabel + " INNER JOIN mRekening on " +
                    //   " mRekening.IIDrekening= tAnggaranRekening_A.IIDRekening WHERE iTahun =" + _iTahun.ToString() +" AND IDDInas="+ idDInas.ToString() +
                    //   " AND IDUrusan=" + idUrusan.ToString() + " AND idprogram=" + idProgram.ToString() + " AND IDKegiatan=" + _idKegiatan.ToString() + " AND btJenis=" + _pJenis.ToString() + " AND isnull(bPPKD,0)=" + _ppkd.ToString() + " AND btTahapInput<= " + _t.ToString()  + " ORDER BY tAnggaranRekening_A.IIDrekening";

                    DataTable dt = new DataTable();
                    dt = _dbHelper.ExecuteDataTable(SSQL);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            _lst = (from DataRow dr in dt.Rows
                                    select new TAnggaranRekening()
                                    {
                                        IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                        Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                        IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                        IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                        KodeKegiatan = DataFormat.GetInteger(dr["IDUrusan"]),
                                        IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                        TahapInput = DataFormat.GetInteger(dr["btTahapInput"]),
                                        Nama = DataFormat.GetString(dr["Nama"]),
                                        IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                        JumlahMurni = DataFormat.GetDecimal(dr["cJumlahMurni"]),
                                        JumlahPergeseran = DataFormat.GetDecimal(dr["cJumlahGeser"]),
                                        JumlahRKAP = DataFormat.GetDecimal(dr["cJumlahRKAP"]),

                                        JumlahABT = DataFormat.GetDecimal(dr["cJumlahABT"]),

                                        //ListUraian = GetUraian(_iTahun, idDInas, idUrusan, idProgram, _idKegiatan, DataFormat.GetLong(dr["IIDRekening"]), _pJenis, _ppkd, _pTahap, _t),
                                        //StatusUpdate = 1


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

        public TAnggaranRekening GetByID(int _iTahun, int idDInas, int idUrusan, int idProgram, int _idKegiatan,long IDRekening, int _pJenis, int _ppkd )
        {
            TAnggaranRekening o= new TAnggaranRekening();

            try {
                if (_pJenis != 3)
                {
                    //SSQL = "UPDATE tANGGARANREKENING_A SET IDurusan = SubString(Replace(Convert(varchar(10),IDDinas),' ',''),1,3) where btJenis <> 3and IDDInas=" + idDInas.ToString() + " AND iTahap=" + _pTahap.ToString();
                    //_dbHelper.ExecuteNonQuery(SSQL);
                    //SSQL = "UPDATE tANGGARANURAIAN_A SET IDurusan = SubString(Replace(Convert(varchar(10),IDDinas),' ',''),1,3) where Jenis <> 3 and IDDInas=" + idDInas.ToString() + " AND iTahap=" + _pTahap.ToString();
                    //_dbHelper.ExecuteNonQuery(SSQL);
                }

                

                         //iTahun,IDProgram,IDKegiatan,IDUrusan,IDDInas,btTahapInput,Nama,IIDRekening,cJumlahOlah,cJumlahYAD,cJumlahMurni,cJumlahGeser,JumlahYADAPBD,cDPA,bPPKD,btJenis,cPlafon,cJumlahRKAP,cJumlahRKA,iTahap

                SSQL = "SELECT A.* FROM tAnggaranRekening_A A INNER JOIN mRekening B on " +
                           " B.IIDrekening= A.IIDRekening WHERE A.iTahun =" + _iTahun.ToString() + " AND A.IDDInas=" + idDInas.ToString() +
                           " AND A.IDUrusan=" + idUrusan.ToString() + " AND A.idprogram=" + idProgram.ToString() + " AND A.IDKegiatan=" + _idKegiatan.ToString() + " AND A.IIDRekening= " + IDRekening.ToString() + " A.btJenis=" + _pJenis.ToString();

                        //SSQL = "SELECT tAnggaranRekening_A.*, mRekening.sNamaRekening as Nama FROM " + m_sNamaTabel + " INNER JOIN mRekening on " +
                        //   " mRekening.IIDrekening= tAnggaranRekening_A.IIDRekening WHERE iTahun =" + _iTahun.ToString() +" AND IDDInas="+ idDInas.ToString() +
                        //   " AND IDUrusan=" + idUrusan.ToString() + " AND idprogram=" + idProgram.ToString() + " AND IDKegiatan=" + _idKegiatan.ToString() + " AND btJenis=" + _pJenis.ToString() + " AND isnull(bPPKD,0)=" + _ppkd.ToString() + " AND btTahapInput<= " + _t.ToString()  + " ORDER BY tAnggaranRekening_A.IIDrekening";
                    
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        o = new TAnggaranRekening()
                                {
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                   
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    TahapInput = DataFormat.GetInteger(dr["btTahapInput"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    JumlahOlah = DataFormat.GetDecimal(dr["cJumlahOlah"]),
                                    PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    Plafon = DataFormat.GetDecimal(dr["cPlafon"]),
                                    Tahap = DataFormat.GetSingle(dr["iTahap"]),
                                    //ListUraian = GetUraian(_iTahun, idDInas, idUrusan, idProgram, _idKegiatan, DataFormat.GetLong(dr["IIDRekening"]), _pJenis, _ppkd, _pTahap, _t),
                                    StatusUpdate = 1


                                };
                    }
                }
                return o;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return o;
            }


        }

        private List<TAnggaranUraian> GetUraianEx(int iTahun, int idDinas, int idUrusan, int idProgram, int _idKegiatan, long  idRekening, int _pJenis, Single _ppkd, Single pTahap, Single _t,long idsubkegiatan)
        {
            List<TAnggaranUraian> lst = new List<TAnggaranUraian>();
            //foreach (SKPD s in idDinas)
            //{

                TAnggaranUraian uraian = new TAnggaranUraian();
                //uraian.Uraian= s.Nama;
                uraian.NoUrut=0;
                //uraian.IDDinas = s.ID;
                uraian.IDUrusan = idUrusan;
                uraian.IDProgram = idProgram;
                uraian.IDKegiatan = _idKegiatan;
                uraian.IDSubKegiatan = idsubkegiatan;

                uraian.Level = 0;

                lst.Add(uraian);

                List<TAnggaranUraian> _lst = new List<TAnggaranUraian>();
                _lst = GetUraian(iTahun, idDinas, idUrusan, idProgram, _idKegiatan, idRekening, pTahap, idsubkegiatan);
                
                   foreach(TAnggaranUraian ta in _lst){
                        lst.Add (ta);
                       }


                
                

            //}
            return lst;


        }
        private List<TAnggaranUraian> GetUraianEx(int iTahun, List<SKPD> idDinas, int idUrusan, int idProgram, int _idKegiatan, int idRekening, int _pJenis, Single _ppkd, Single pTahap, Single _t, long idsubkegiatan)
        {
            List<TAnggaranUraian> lst = new List<TAnggaranUraian>();
            foreach (SKPD s in idDinas)
            {

                TAnggaranUraian uraian = new TAnggaranUraian();
                uraian.Uraian = s.Nama;
                uraian.NoUrut = 0;
                uraian.IDDinas = s.ID;
                uraian.IDUrusan = idUrusan;
                uraian.IDProgram = idProgram;
                uraian.IDKegiatan = _idKegiatan;
                uraian.IDSubKegiatan = idsubkegiatan;

                uraian.Level = 0;

                lst.Add(uraian);

                List<TAnggaranUraian> _lst = new List<TAnggaranUraian>();
                _lst = GetUraian(iTahun, s.ID, idUrusan, idProgram, _idKegiatan, idRekening, pTahap, idsubkegiatan);

                foreach (TAnggaranUraian ta in _lst)
                {
                    lst.Add(ta);
                }
            }





            //}
            return lst;


        }
        // DataFormat.GetLong(dr["IIDRekening"]), pTahap, idSubKegiatan),
        private List<TAnggaranUraian> GetUraian(int iTahun, int idDinas, int idUrusan, int idProgram, long _idKegiatan, long idRekening, Single pTahap,long idSubKegiatan)
        {
            List<TAnggaranUraian> _lst = new List<TAnggaranUraian>();
            try
            {
                
                GetKolom((int)pTahap);
                string sNamaKolomPPN = "PPNOlah"; ;
                if (pTahap == 1)
                    sNamaKolomPPN = "PPNRKA";
                if (pTahap == 2)
                    sNamaKolomPPN = "PPNMurni";
                if (pTahap == 3)
                    sNamaKolomPPN = "PPNRKAP";
                if (pTahap == 4)
                    sNamaKolomPPN = "PPNABT";
                

                SSQL = "SELECT A.btTahapInput,A.btUrut,A.ID,A.IDLokasi,A.Level,A.IIDRekening,A.IDSubKegiatan,A.sSatuan,A." + _namaKolomvolume2 + 
                    " as VolOlah,A.Vol,A." + _namaKolomvolume1 + " as VolMurni,A." + _namaKolomharga2 + " as cHargaOlah,A." + sNamaKolomPPN + 
                    " as PPN,A.cHarga,A." + _namaKolomharga1 + " as cHargaMurni,A." + _namaKolomUraian1 + " as sUraianMurni, A." + _namaKolomUraian2 + 
                    " as sUraian,A.sUraianAPBD,A." + _namaKolomjumlahuraian2 + " as JumlahOlah,A.JumlahMurni,A.Jumlah,A.iID,A.bPPKD,A.showinreport," +
                    "A.sLabel,A.cPlafon,A.cJumlahYAD,A.JumlahYADAPBD,A.btUrutDPA,A.sSatuanDPA,A.iLevelDPA,A.sLabelDPA, A.btTahapInput,A.IDstandardHarga," +
                    "A.cStandardHarga , A.IDBarang, A.IDRKBMD, A.IDRKBMDBARANG FROM tANggaranUraian_A A WHERE A.iTahun =" + iTahun.ToString() + " AND A.IDDInas=" + idDinas.ToString() +
                            " AND  A.IDUrusan=" + idUrusan.ToString() +
                            " AND A.idprogram=" + idProgram.ToString() + " AND A.IDKegiatan=" + _idKegiatan.ToString() + " AND A.iidRekening=" + idRekening.ToString() + " AND A.IDSubKegiatan = " + idSubKegiatan.ToString() + "  ORDER BY A.btUrut";
             
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                            select new  TAnggaranUraian()
                            {
                                    
                                TahapInput = DataFormat.GetSingle(dr["btTahapInput"]), 
                                NoUrut = DataFormat.GetInteger(dr["btUrut"]),
                                IDUraian = DataFormat.GetInteger(dr["ID"]), 
                                IDLokasi = DataFormat.GetInteger(dr["IDLokasi"]), 
                                IDSubKegiatan =  DataFormat.GetInteger(dr["IDSubKegiatan"]), 
                                Level = DataFormat.GetSingle(dr["Level"]),
                                IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                Satuan = DataFormat.GetString(dr["sSatuan"]),                               
                                VolOlah = DataFormat.GetDouble(dr["VolOlah"]),
                                Vol = DataFormat.GetDouble(dr["Vol"]),
                                VolMurni = DataFormat.GetDouble(dr["VolMurni"]),                                
                                HargaOlah = DataFormat.GetDecimal(dr["cHargaOlah"]),                                
                                HargaMurni = DataFormat.GetDecimal(dr["cHargaMurni"]),                                
                                Uraian = DataFormat.GetString(dr["sUraian"]),
                                UraianMurni= DataFormat.GetString(dr["sUraianMurni"]),
                                JumlahOlah = DataFormat.GetDecimal(dr["JumlahOlah"]),
                                JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]),
                                ID = DataFormat.GetInteger(dr["iID"]),
                                PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                                ShowInReport = DataFormat.BitToSingle(dr["showinreport"]),
                                Label =DataFormat.GetString(dr["sLabel"]),
                                Plafon = DataFormat.GetDecimal(dr["cPlafon"]),
                                JumlahYAD = DataFormat.GetDecimal(dr["cJumlahYAD"]),
                                JumlahYADAPBD = DataFormat.GetDecimal(dr["JumlahYADAPBD"]),
                                NoUrutDPA = DataFormat.GetInteger(dr["btUrutDPA"]),
                                SatuanDPA = DataFormat.GetString(dr["sSatuanDPA"]),
                                LevelDPA = DataFormat.GetInteger(dr["iLevelDPA"]),
                                LabelDPA = DataFormat.GetString(dr["sLabelDPA"]),
                                IDStandardHarga = DataFormat.GetString(dr["IDstandardHarga"]),
                                StandardHarga = DataFormat.GetDecimal(dr["cStandardHarga"]),
                                PPNOlah =  DataFormat.GetDecimal(dr["PPN"]),
                              
                                
                                Tahap= DataFormat.GetInteger(dr["btTahapinput"]),
                                IDBarang  = DataFormat.GetLong(dr["IDBarang"]),
                                IDRKBMD = DataFormat.GetInteger(dr["IDRKBMD"]),
                                IDRKBMDBArang = DataFormat.GetInteger(dr["IDRKBMDBArang"]),



                                StatusUpdate=1
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

        public List<TAnggaranUraian> GetUraianPerencanaan(int iTahun, RemoteConnection sCOn)
        {
            List<TAnggaranUraian> _lst = new List<TAnggaranUraian>();

            try
            {

                
                
                DBHelper dbhelper = new DBHelper();
                               
                SSQL = "SELECT A.btTahapInput,A.btUrut,A.ID,A.IDLokasi,A.Level,A.IIDRekening,A.sSatuan,A.VolOlah,A.Vol,A.VolMurni,A.cHargaOlah,A.cHarga,A.cHargaMurni,A.sUraianMurni, A.sUraian,A.JumlahMurni,A.iID,A.bPPKD,A.showinreport,A.sLabel,A.cPlafon,A.cJumlahYAD,A.JumlahYADAPBD,A.btUrutDPA,A.sSatuanDPA,A.iLevelDPA,A.sLabelDPA, A.btTahapInput,A.IDstandardHarga,A.cStandardHarga FROM tANggaranUraian_A A WHERE A.iTahun =" + iTahun.ToString();
                
                DataTable dt = new DataTable();
                dt = dbhelper.ExecuteDataTable(SSQL,sCOn.GetConnection());
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TAnggaranUraian()
                                {

                                    TahapInput = DataFormat.GetSingle(dr["btTahapInput"]),
                                    NoUrut = DataFormat.GetInteger(dr["btUrut"]),
                                    IDUraian = DataFormat.GetInteger(dr["ID"]),
                                    IDLokasi = DataFormat.GetInteger(dr["IDLokasi"]),
                                    Level = DataFormat.GetSingle(dr["Level"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    Satuan = DataFormat.GetString(dr["sSatuan"]),
                                    VolOlah = DataFormat.GetDouble(dr["VolOlah"]),
                                    Vol = DataFormat.GetDouble(dr["Vol"]),
                                    VolMurni = DataFormat.GetDouble(dr["VolMurni"]),
                                    HargaOlah = DataFormat.GetDecimal(dr["cHargaOlah"]),
                                    HargaMurni = DataFormat.GetDecimal(dr["cHargaMurni"]),
                                    Uraian = DataFormat.GetString(dr["sUraian"]),
                                    UraianMurni = DataFormat.GetString(dr["sUraianMurni"]),
                                    JumlahOlah = DataFormat.GetDecimal(dr["JumlahOlah"]),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]),
                                    ID = DataFormat.GetInteger(dr["iID"]),
                                    PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                                    ShowInReport = DataFormat.BitToSingle(dr["showinreport"]),
                                    Label = DataFormat.GetString(dr["sLabel"]),
                                    Plafon = DataFormat.GetDecimal(dr["cPlafon"]),
                                    JumlahYAD = DataFormat.GetDecimal(dr["cJumlahYAD"]),
                                    JumlahYADAPBD = DataFormat.GetDecimal(dr["JumlahYADAPBD"]),
                                    NoUrutDPA = DataFormat.GetInteger(dr["btUrutDPA"]),
                                    SatuanDPA = DataFormat.GetString(dr["sSatuanDPA"]),
                                    LevelDPA = DataFormat.GetInteger(dr["iLevelDPA"]),
                                    LabelDPA = DataFormat.GetString(dr["sLabelDPA"]),
                                    IDStandardHarga = DataFormat.GetString(dr["IDstandardHarga"]),
                                    StandardHarga = DataFormat.GetDecimal(dr["cStandardHarga"]),

                                    Tahap = DataFormat.GetInteger(dr["btTahapinput"]),
                                    StatusUpdate = 1
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
        private List<TAnggaranUraian> GetUraianAPBD(int iTahun, int idDinas, int idUrusan, int idProgram, int _idKegiatan, long idRekening, int _pJenis, Single _ppkd, Single _pTahap)
        {
            List<TAnggaranUraian> _lst = new List<TAnggaranUraian>();
            try
            {
                //if (_pJenis == 3)
                //{
                SSQL = "SELECT tANggaranUraian_A.* FROM tANggaranUraian_A WHERE iTahun =" + iTahun.ToString() + " AND IDDInas=" + idDinas.ToString() +
                        " AND  IDUrusan=" + idUrusan.ToString() +
                        " AND idprogram=" + idProgram.ToString() + " AND IDKegiatan=" + _idKegiatan.ToString() + " AND iidRekening=" + idRekening.ToString() + " AND Jenis=" + _pJenis.ToString() + " AND isnull(bPPKD,0)=" + _ppkd.ToString() + " AND iTahap=" + _pTahap.ToString() + " ORDER BY btUrut";
                //}
                //else
                //{
                //    SSQL = "SELECT tANggaranUraian_A.* FROM tANggaranUraian_A WHERE iTahun =" + iTahun.ToString() + " AND IDDInas=" + idDinas.ToString() +                            
                //            " AND idprogram=" + idProgram.ToString() + " AND IDKegiatan=" + _idKegiatan.ToString() + " AND iidRekening=" + idRekening.ToString() + " AND Jenis=" + _pJenis.ToString() + " ORDER BY btUrut";
                //}
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TAnggaranUraian()
                                {

                                    TahapInput = DataFormat.GetSingle(dr["btTahapInput"]),
                                    NoUrut = DataFormat.GetInteger(dr["btUrut"]),
                                    IDUraian = DataFormat.GetSingle(dr["ID"]),
                                    IDLokasi = DataFormat.GetInteger(dr["IDLokasi"]),
                                    Level = DataFormat.GetSingle(dr["Level"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    Satuan = DataFormat.GetString(dr["sSatuan"]),
                                    VolOlah = DataFormat.GetDouble(dr["VolOlah"]),
                                    HargaOlah = DataFormat.GetDecimal(dr["cHargaOlah"]),
                                    Vol = DataFormat.GetDouble(dr["Vol"]),
                                    Harga = DataFormat.GetDecimal(dr["cHarga"]),
                                    Uraian = DataFormat.GetString(dr["sUraianAPBD"]),
                                    JumlahOlah = DataFormat.GetDecimal(dr["JumlahOlah"]),
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"]),
                                    ID = DataFormat.GetInteger(dr["iID"]),
                                    PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                                    ShowInReport = DataFormat.BitToSingle(dr["showinreport"]),
                                    Label = DataFormat.GetString(dr["sLabel"]),
                                    Plafon = DataFormat.GetDecimal(dr["cPlafon"]),
                                    JumlahYAD = DataFormat.GetDecimal(dr["cJumlahYAD"]),
                                    JumlahYADAPBD = DataFormat.GetDecimal(dr["JumlahYADAPBD"]),
                                    Tahap = _pTahap,
                                    StatusUpdate = 1
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
        public bool Hapus(TAnggaranRekening o)
        {
            try
            {
                SSQL = "DELETE FROM " + m_sNamaTabel + " WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
                            " AND IDUrusan=@pIDUrusan and IIDRekening=@pIIDRekening and IDSUbKegiatan = @pIDSUbKegiatan and btJenis=@pbtJenis ";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                paramCollection.Add(new DBParameter("@pIDSUbKegiatan", o.IDSubKegiatan));
                paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));

                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }
        public bool SamakanDPAdenganRKA( int _Tahun, int IDUrusan, int _idDinas, int _IDProgram, int _IDKegatan, int _pTahap)
        {
            try
            {
                
                switch (_pTahap) { 
                    case 2:
                        SSQL = "UPDATE tAnggaranRekening_A SET cJumlahMurni= cJumlahRKA, cJumlahGeser = cJumlahRKA, cJumlahRKAP = cJumlahRKA, cJumlahABT= cJumlahRKA,cDPA =cJumlahRKA WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString() +
                            " AND IDProgram = " + _IDProgram.ToString() + " AND IDKegiatan =" + _IDKegatan.ToString();
                        _dbHelper.ExecuteNonQuery(SSQL);
                
                        SSQL = "UPDATE tAnggaranUraian_A SET sUraianAPBD=sUraianOlah, VOlMurni = VolRKA,VolGEser=VolRKA,VOLRKAP= VolRKA,VolABT= VolRKA,cHargaMurni= cHargaRKA,cHargaGeser= cHargaRKA,cHargaRKAP = cHargaRKA,cHargaABT= cHargaRKA,JumlahMurni = JumlahRKA,JumlahGeser= JumlahRKA,JumlahRKAP =JumlahRKA,JumlahABT= JumlahRKA " +
                            " WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString() +
                            " AND IDProgram = " + _IDProgram.ToString() + " AND IDKegiatan =" + _IDKegatan.ToString();
                        _dbHelper.ExecuteNonQuery(SSQL);
                        break;
                    case 3 :
                        SSQL = "UPDATE tAnggaranRekening_A SET cDPA= cJumlahGeser , cJumlahRKAP = cJumlahGeser, cJumlahABT= cJumlahGeser  WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString() +
                            " AND IDProgram = " + _IDProgram.ToString() + " AND IDKegiatan =" + _IDKegatan.ToString();
                        _dbHelper.ExecuteNonQuery(SSQL);

                        
                        //SSQL = "UPDATE tAnggaranUraian_A SET  VOl = VolRKA,VolGEser=VolRKA,VOLRKAP= VolRKA,VolABT= VolRKA,cHargaMurni= cHargaRKA,cHargaGeser= cHargaRKA,cHargaRKAP = cHargaRKA,cHargaABT= cHargaRKA,JumlahMurni = JumlahRKA,JumlahGeser= JumlahRKA,JumlahRKAP =JumlahRKA,JumlahABT= JumlahRKA " +
                        //    " WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString() +
                        //    " AND IDProgram = " + _IDProgram.ToString() + " AND IDKegiatan =" + _IDKegatan.ToString();
                        //_dbHelper.ExecuteNonQuery(SSQL);
                        break;

                    case 4  :
                        SSQL = "UPDATE tAnggaranRekening_A SET cDPA= cJumlahRKAP , cJumlahABT= cJumlahRKAP  WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString() +
                            " AND IDProgram = " + _IDProgram.ToString() + " AND IDKegiatan =" + _IDKegatan.ToString();
                        _dbHelper.ExecuteNonQuery(SSQL);
                        
                         SSQL = "UPDATE tAnggaranUraian_A SET  VOlABT = VolRKAP,cHargaABT= cHargaRKAP,cHargaABT= cHargaRKAP,JumlahABT = JumlahRKAP " +
                            " WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString() +
                            " AND IDProgram = " + _IDProgram.ToString() + " AND IDKegiatan =" + _IDKegatan.ToString();
                        _dbHelper.ExecuteNonQuery(SSQL);
                        
                        break;

                    case 5:
                        SSQL = "UPDATE tAnggaranRekening_A SET cDPA= cJumlahRKAP , cJumlahABT= cJumlahRKAP  WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString() +
                            " AND IDProgram = " + _IDProgram.ToString() + " AND IDKegiatan =" + _IDKegatan.ToString();
                        _dbHelper.ExecuteNonQuery(SSQL);
                        SSQL = "UPDATE tAnggaranUraian_A SET  VOlABT = VolRKAP,cHargaABT= cHargaRKAP,cHargaABT= cHargaRKAP,JumlahABT = JumlahRKAP " +
                            " WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString() +
                            " AND IDProgram = " + _IDProgram.ToString() + " AND IDKegiatan =" + _IDKegatan.ToString();
                        _dbHelper.ExecuteNonQuery(SSQL);
                        

                        break;


                        //SSQL = "UPDATE tAnggaranUraian_A SET  VOlMurni = VolRKA,VolGEser=VolRKA,VOLRKAP= VolRKA,VolABT= VolRKA,cHargaMurni= cHargaRKA,cHargaGeser= cHargaRKA,cHargaRKAP = cHargaRKA,cHargaABT= cHargaRKA,JumlahMurni = JumlahRKA,JumlahGeser= JumlahRKA,JumlahRKAP =JumlahRKA,JumlahABT= JumlahRKA " +
                        //    " WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString() +
                        //    " AND IDProgram = " + _IDProgram.ToString() + " AND IDKegiatan =" + _IDKegatan.ToString();
                        break;                
                    //_dbHelper.ExecuteNonQuery(SSQL);
                }
            
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return _isError;
            }

                
        }

        public bool SamakanRKAdenganDPA(int _Tahun, int IDUrusan, int _idDinas, int _IDProgram, int _IDKegatan, int _pTahap)
        {
            try
            {

                switch (_pTahap)
                {
                    case 2:
                        SSQL = "UPDATE tAnggaranRekening_A SET cJumlahRKA= cJumlahMurni WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString() +
                            " AND IDProgram = " + _IDProgram.ToString() + " AND IDKegiatan =" + _IDKegatan.ToString();
                        _dbHelper.ExecuteNonQuery(SSQL);

                        SSQL = "UPDATE tAnggaranUraian_A SET sUraianRKA= sUraianMurni, VolRKA=VOlMurni, cHargaRKA= cHargaMurni ,JumlahRKA= JumlahMurni  " +
                            " WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString() +
                            " AND IDProgram = " + _IDProgram.ToString() + " AND IDKegiatan =" + _IDKegatan.ToString();
                        _dbHelper.ExecuteNonQuery(SSQL);
                        break;

                    case 3:
                        SSQL = "UPDATE tAnggaranRekening_A SET cDPA= cJumlahGeser , cJumlahRKAP = cJumlahGeser, cJumlahABT= cJumlahGeser  WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString() +
                            " AND IDProgram = " + _IDProgram.ToString() + " AND IDKegiatan =" + _IDKegatan.ToString();
                        //_dbHelper.ExecuteNonQuery(SSQL);


                        //SSQL = "UPDATE tAnggaranUraian_A SET  VOl = VolRKA,VolGEser=VolRKA,VOLRKAP= VolRKA,VolABT= VolRKA,cHargaMurni= cHargaRKA,cHargaGeser= cHargaRKA,cHargaRKAP = cHargaRKA,cHargaABT= cHargaRKA,JumlahMurni = JumlahRKA,JumlahGeser= JumlahRKA,JumlahRKAP =JumlahRKA,JumlahABT= JumlahRKA " +
                        //    " WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString() +
                        //    " AND IDProgram = " + _IDProgram.ToString() + " AND IDKegiatan =" + _IDKegatan.ToString();
                        //_dbHelper.ExecuteNonQuery(SSQL);
                        break;

                    case 4:
                        SSQL = "UPDATE tAnggaranRekening_A SET cDPA= cJumlahRKAP , cJumlahABT= cJumlahRKAP  WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString() +
                            " AND IDProgram = " + _IDProgram.ToString() + " AND IDKegiatan =" + _IDKegatan.ToString();
                        //_dbHelper.ExecuteNonQuery(SSQL);

                        SSQL = "UPDATE tAnggaranUraian_A SET  VOlABT = VolRKAP,cHargaABT= cHargaRKAP,cHargaABT= cHargaRKAP,JumlahABT = JumlahRKAP " +
                           " WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString() +
                           " AND IDProgram = " + _IDProgram.ToString() + " AND IDKegiatan =" + _IDKegatan.ToString();
                        //_dbHelper.ExecuteNonQuery(SSQL);

                        break;

                    case 5:
                        SSQL = "UPDATE tAnggaranRekening_A SET cDPA= cJumlahRKAP , cJumlahABT= cJumlahRKAP  WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString() +
                            " AND IDProgram = " + _IDProgram.ToString() + " AND IDKegiatan =" + _IDKegatan.ToString();
                        _dbHelper.ExecuteNonQuery(SSQL);
                        SSQL = "UPDATE tAnggaranUraian_A SET  VOlABT = VolRKAP,cHargaABT= cHargaRKAP,cHargaABT= cHargaRKAP,JumlahABT = JumlahRKAP " +
                            " WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString() +
                            " AND IDProgram = " + _IDProgram.ToString() + " AND IDKegiatan =" + _IDKegatan.ToString();
                        _dbHelper.ExecuteNonQuery(SSQL);


                        break;


                        //SSQL = "UPDATE tAnggaranUraian_A SET  VOlMurni = VolRKA,VolGEser=VolRKA,VOLRKAP= VolRKA,VolABT= VolRKA,cHargaMurni= cHargaRKA,cHargaGeser= cHargaRKA,cHargaRKAP = cHargaRKA,cHargaABT= cHargaRKA,JumlahMurni = JumlahRKA,JumlahGeser= JumlahRKA,JumlahRKAP =JumlahRKA,JumlahABT= JumlahRKA " +
                        //    " WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString() +
                        //    " AND IDProgram = " + _IDProgram.ToString() + " AND IDKegiatan =" + _IDKegatan.ToString();
                        break;
                    //_dbHelper.ExecuteNonQuery(SSQL);
                }

                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return _isError;
            }


        }

        public bool SamakanDPAdenganRKA(int _Tahun, int _idDinas)
        {
            try
            {
                SSQL = "UPDATE tAnggaranRekening_A SET cJumlahMurni= cJumlahRKA, cJumlahGeser = cJumlahRKA, cJumlahRKAP = cJumlahRKA, cJumlahABT= cJumlahRKA,cDPA =cJumlahOlah WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString();                        
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = " UPDATE tAnggaranUraian_A SET  VOlMurni = VolRKA,VolGEser=VolRKA,VOLRKAP= VolRKA,VolABT= VolRKA,cHargaMurni= cHargaRKA,cHargaGeser= cHargaRKA,cHargaRKAP = cHargaRKA,cHargaABT= cHargaRKA,JumlahMurni = JumlahRKA,JumlahGeser= JumlahRKA,JumlahRKAP =JumlahRKA,JumlahABT= JumlahRKA, sUraianMurni= sUraianRKA, sUraianGeser= sUraianRKA, sUraianABT= sUraianRKA " +
                       " WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString();

                _dbHelper.ExecuteNonQuery(SSQL);

                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return _isError;
            }
        }
        public decimal HitungANggaran(int pIDDinas, int iKegiatan) {
            decimal lRet = 0L;
            try
            {

                SSQL = "SELECT SUM(cjumlahMurni ) as Jumlah from tANGGARANREKENING_A B " +
                      " WHERE B.iTahun = " + Tahun.ToString() + " AND B.IDDInas=" + pIDDinas.ToString() + " AND B.btJenis= 3 " +
                      " AND B.IDKEgiatan <> " + iKegiatan.ToString()  ;

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        lRet = DataFormat.GetDecimal(dr["Jumlah"]);
                    }
                }
                return lRet;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;

                return 0;
            }

        }
        public bool SamakanDPAdenganRKAUTKSPD(int _Tahun, int _idDinas, bool bAnggaranPerubahan)
        {
            try
            {

                SSQL= "UPDATE tAnggaranRekening_A SET cDPA= 0 WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString();

                _dbHelper.ExecuteNonQuery(SSQL);

                if (bAnggaranPerubahan == true)
                {

                    SSQL = "UPDATE tAnggaranRekening_A SET cDPA= cJumlahABT WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString();

                }
                else
                {

                    SSQL = "UPDATE tAnggaranRekening_A SET cDPA= cJumlahMurni WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString();

                }
                
                _dbHelper.ExecuteNonQuery(SSQL);

              

                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return _isError;
            }
        }

        public bool TetapkanPergeseran(int _Tahun, int IDUrusan, int _idDinas, int _IDProgram, int _IDKegatan, int _jenis)
        {
            try
            {
                
                SSQL = "UPDATE tAnggaranRekening_A SET cDPA = cJumlahGeser WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString() +
                        " AND IDProgram = " + _IDProgram.ToString() + " AND IDKegiatan =" + _IDKegatan.ToString() + " AND btJenis=" + _jenis.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);                
                return true;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return _isError;
            }

        }
        public bool SamakanDPPAdenganRKAP(int _Tahun, int _idDinas)
        {
            try
            {
                SSQL = "UPDATE tAnggaranRekening_A SET cDPA =cJumlahOlah  WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString() ;                        
                _dbHelper.ExecuteNonQuery(SSQL);

                //'"btUrut=@pbtUrut,VolOlah=@pVolOlah,,,,IDstandardHarga=@pIDstandardHarga,JumlahOlah =@pJumlahOlah,showinreport=@pshowinreport,sLabel=@psLabel,cJumlahYAD=@pcJumlahYAD WHERE " +
                SSQL = "UPDATE tAnggaranUraian_A SET  btUrutDPA = btUrut,cHarga= cHargaRKA, Vol=VolRKA,Jumlah= JumlahRKA,  " +
                    " WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString();

                _dbHelper.ExecuteNonQuery(SSQL);


                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return _isError;
            }


        }

        public bool PersiapkanRKAP(int _Tahun, int _idDinas)
        {
            try
            {
                if (_idDinas > 0)
                {

                    SSQL = "UPDATE tAnggaranRekening_A SET cJumlahRKAP=cJumlahmurni,  cJumlahABT = cJumlahMurni WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString();
                    _dbHelper.ExecuteNonQuery(SSQL);

                    SSQL = "UPDATE tAnggaranUraian_A SET  cHargaRKAP= cHargaMurni, VolRKAP=VolMurni,JumlahRKAP= JumlahMurni,sUraianRKAP= sUraianMurni ,cHargaABT= cHargaMurni, VolABT=VolMurni,JumlahABT= JumlahMurni,sUraianABT= sUraianMurni   " +
                        " WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString();

                    _dbHelper.ExecuteNonQuery(SSQL);
                }
                else
                {

                    SSQL = "UPDATE tAnggaranRekening_A SET cJumlahRKAP=cJumlahGeser,  cJumlahABT = cJumlahGeser WHERE iTahun =" + _Tahun.ToString();
                    _dbHelper.ExecuteNonQuery(SSQL);

                    SSQL = "UPDATE tAnggaranUraian_A SET  cHargaRKAP= cHargaGeser, VolRKAP=VolGeser,JumlahRKAP= JumlahGeser,sUraianRKAP= sUraianGeser ,cHargaABT= cHargaGeser, VolABT=VolGeser,JumlahABT= JumlahGeser,sUraianABT= sUraianGeser   " +
                        " WHERE iTahun =" + _Tahun.ToString() ;

                    _dbHelper.ExecuteNonQuery(SSQL);


                }

                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return _isError;
            }

        }
        public bool SamakanABTdenganRKAP(int _Tahun, int _idDinas)
        {
            try
            {
                SSQL = "UPDATE tAnggaranRekening_A SET cJumlahABT= cJumlahRKAP ,cDPA =cJumlahRKAP WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = " UPDATE tAnggaranUraian_A SET  VOlABT = VolRKAP,cHargaABT=cHargaRKAP,JumlahABT = JumlahRKAP,sUraianABT= sUraianRKAP " +
                       " WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString();

                _dbHelper.ExecuteNonQuery(SSQL);

                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return _isError;
            }
        }

        public bool BersihkanDouble(int _tahun)
        {
            List<TAnggaranRekening> _lst = new List<TAnggaranRekening>();
            try
            {
                SSQL = "SELECT * INTO tempRekeningSEP from tANggaranRekening_A where iTahun = " + _tahun.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "SELECT  iTahun,IDUrusan,IDDinas, IDProgram, IDKegiatan , IIDRekening, btJenis,count(*) FROM tANggaranRekening_A WHERE iTAhun =" + _tahun.ToString() +
                        " GROUP BY iTahun,IDUrusan,IDDinas, IDProgram, IDKegiatan,IIDRekening,btJenis HAVING count(*)>1";
                _dbHelper.ExecuteNonQuery(SSQL);
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        _lst = (from DataRow dr in dt.Rows
                                select new TAnggaranRekening()
                                {
                                    Tahun = DataFormat.GetSingle(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    IDRekening= DataFormat.GetInteger(dr["IIDRekening"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"])
                                }).ToList();
                        

                    }

                }
                foreach (TAnggaranRekening t in _lst)
                {
                    List<TAnggaranRekening>lst = new List<TAnggaranRekening>();

                    TAnggaranRekening o = new TAnggaranRekening();
                    o = GetByID((int)t.Tahun , t.IDDinas, t.IDUrusan, t.IDProgram, t.IDKegiatan,t.IDRekening, (int)t.Jenis,0);
                    lst.Add(o);

                    SSQL = "DELETE tAnggaranRekening_A WHERE iTAhun =" + _tahun.ToString() + " AND IDDInas =" + t.IDDinas.ToString() +
                       " AND IDUrusan =" + t.IDUrusan.ToString() + " AND IDProgram = " + t.IDProgram.ToString() +
                       " AND IDKegiatan=" + t.IDKegiatan.ToString() + " AND btJenis=" + t.Jenis.ToString() + " AND IIDRekening=" + t.IDRekening.ToString();
                    _dbHelper.ExecuteNonQuery(SSQL);
                    SimpanImportBapeda(lst, _tahun, 1);

                    
                }
                SSQL = "DROP TABLE tempRekeningSEP";
                _dbHelper.ExecuteNonQuery(SSQL);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        


        private void CekTable()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }
        public decimal JunlahDPA(SPP oSPP)
        {

            SSQL = "SELECT sum(cDPA) from tAnggaranRekening_A WHERE iTahun =" + oSPP.Tahun +
                    " AND IDDInas =" + oSPP.IDDInas.ToString();
            switch (oSPP.Jenis)
            {
                case 0:
                    SSQL = SSQL + "AND btJenis= 3";
                    break;
                case 1:
                    SSQL = SSQL + "AND btJenis= 3";
                    break;
                case 2:
                    if (oSPP.PPKD == 1)
                        SSQL = SSQL + "AND btJenis in(2,5)";
                    else

                        SSQL = SSQL + "AND btJenis= 3";
                    break;
                case 3:
                    SSQL = SSQL + "AND btJenis= 3";
                    break;
                case 4:
                    SSQL = SSQL + "AND btJenis= 2";
                    break;
                case 5:
                    if (oSPP.JenisGaji == 62)// sebenarnyya jenisRekening
                    {
                        SSQL = SSQL + "AND btJenis= 5 and bPPKD=1";
                    }
                    else
                    {
                        SSQL = SSQL + "AND btJenis= 2 and bPPKD=1";
                    }
                    break;

            }
            DataTable dt = new DataTable();
            object obj = null;
            obj = _dbHelper.ExecuteScalar(SSQL);
            if (obj != null)
            {
                return (decimal)obj;
            }

            return 0;

        }

    }
}
