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
    public class rptKUALogic:BP
    {
        public rptKUALogic(int _pTahun, int profile)
            : base(_pTahun, 0, profile )
        {
            Tahun = _pTahun;
        }
        public List<rptKUA> GetByDinas(int _pDInas, bool _denganRincian = true, int _gabungan=0 )
        {
            List<rptKUA> _lst = new List<rptKUA>();

            try
            {




                if (_gabungan == 1)
                {
                    List<SKPD> lstSKPD = new List<SKPD>();
                    SKPDLogic oSKPDLogic = new SKPDLogic(Tahun);
                    lstSKPD = oSKPDLogic.GetByParent(_pDInas);

                    string strDinas = "(";
                    foreach (SKPD d in lstSKPD)
                    {
                        strDinas = strDinas + d.ID.ToString() + ",";
                    }
                    strDinas = strDinas + "99)";

                    SSQL = "Select 1 as Level, A.IDUrusan,A.IDProgram,A.IDKegiatan, 0 as IDKegiatan,  A.sNamaProgram  as  Uraian,0 as IDLokasi, "+
                          " (select SUM(tKUA.JumlahMurni) from tKUA where iTahun= " + Tahun.ToString() + " AND IDProgram = A.IDProgram AND IDDInas in " + strDinas + " AND JumlahMurni> 0 and IDLokasi=0 ) as Jumlah, 0 as JumlahRKPD,0 as Nourut " +
                         " FROM fGetDistinctProgram (" + Tahun.ToString() + "," + _pDInas.ToString() + ") A ";

                    SSQL = SSQL + " UNION ";
                    SSQL = SSQL + " Select 2 as Level, A.IDUrusan,A.IDProgram,A.IDKegiatan, 0 as IDKegiatan,  A.sNamaKegiatan  as  Uraian,0 as IDLokasi, " +
                          " (select SUM(tKUA.JumlahMurni) from tKUA where iTahun= " + Tahun.ToString() + " AND IDProgram = A.IDProgram AND IDKegiatan = A.IDKegiatan AND IDDInas in " + strDinas + " AND JumlahMurni> 0 and IDLokasi=0  ) as Jumlah, 0 as JumlahRKPD,0 as Nourut " +
                         " FROM fGetDistinctKegiatan (" + Tahun.ToString() + "," + _pDInas.ToString() + ") A ";

                    SSQL = SSQL + " ORDER BY A.IDUrusan,A.IDProgram,A.IDKegiatan, NoUrut,IDLokasi ";
                    
                    //" UNION  " +
                        //" Select 2 as Level,tKUA.IDUrusan,tKUA.IDProgram,tKUA.IDKegiatan, mKegiatan.sNamaKegiatan as Uraian, 0 as IDLokasi,SUM(tKUA.JumlahMurni)  as Jumlah,SUM(tKUA.JumlahRKPD) as JumlahRKPD,0 as Nourut  " +
                        //" FROM tKUA INNER JOIN mKegiatan ON  mKegiatan.ID=tKUA.IDKegiatanMaster " +
                        //" where tKUA.iTahun = " + Tahun.ToString() + " AND tKUA.IDlokasi=0 and tKUA.IDDInas= " + _pDInas.ToString() + " AND isnull(tKUA.Status,0)<9 and tKUA.JumlahMurni>0 and IDDInas in " + strDinas + 
                        //" GROUP BY tKUA.IDUrusan,tKUA.IDProgram,tKUA.IDKegiatan, IDUrusanMaster,IDProgramMaster,IDKegiatanMaster,mKegiatan.sNamaKegiatan ";
                    




                } else 
                {

                    SSQL = "Select 1 as Level, tKUA.IDUrusan,tKUA.IDProgram,0 as  IDKegiatan,  IDUrusanMaster,IDProgramMaster,0 as IDKegiatanMaster,mProgram.sNamaProgram as  Uraian,0 as IDLokasi, SUM(tKUA.JumlahMurni) as Jumlah,SUM(tKUA.JumlahRKPD) as JumlahRKPD,0 as Nourut " +
                         " FROM tKUA INNER JOIN mProgram ON tKUA.IDUrusanMaster= mProgram.IDUrusan and tKUA.IDProgramMaster= mProgram.ID " +
                         " where tKUA.iTahun = " + Tahun.ToString() + " AND tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pDInas.ToString() + " AND isnull(tKUA.Status,0)<9 and tKUA.JumlahMurni>0" +
                        " GROUP BY tKUA.IDUrusan,tKUA.IDProgram,IDUrusanMaster,IDProgramMaster,mProgram.sNamaProgram  " +
                        " UNION  " +
                        " Select 2 as Level,tKUA.IDUrusan,tKUA.IDProgram,tKUA.IDKegiatan, IDUrusanMaster,IDProgramMaster,IDKegiatanMaster as IDKegiatanMaster,mKegiatan.sNamaKegiatan as Uraian, 0 as IDLokasi,SUM(tKUA.JumlahMurni)  as Jumlah,SUM(tKUA.JumlahRKPD) as JumlahRKPD,0 as Nourut  " +
                        " FROM tKUA INNER JOIN mKegiatan ON  mKegiatan.ID=tKUA.IDKegiatanMaster " +
                        " where tKUA.iTahun = " + Tahun.ToString() + " AND tKUA.IDlokasi=0 and tKUA.IDDInas= " + _pDInas.ToString() + " AND isnull(tKUA.Status,0)<9 and tKUA.JumlahMurni>0 " +
                        " GROUP BY tKUA.IDUrusan,tKUA.IDProgram,tKUA.IDKegiatan, IDUrusanMaster,IDProgramMaster,IDKegiatanMaster,mKegiatan.sNamaKegiatan ";
                  SSQL=SSQL+      " ORDER BY IDUrusan,IDProgram,IDKegiatan, NoUrut,IDLokasi ";
            }
                //if (_denganRincian) {

                //    SSQL = SSQL + " UNION " +
                //     " Select 3 as Level, tKUA.IDUrusan,tKUA.IDProgram,tKUA.IDKegiatan,IDUrusanMaster,IDProgramMaster,IDKegiatanMaster as IDKegiatanMaster,Usulan as Uraian, IDLokasi,tKUA.JumlahOlah  as Jumlah,NoUrut " +
                //     " FROM tKUA  " +
                //     " where tKUA.iTahun = " + Tahun.ToString() + " AND  tKUA.IDlokasi>0  and tKUA.IDDInas= " + _pDInas.ToString() + " AND isnull(tKUA.Status,0)<9 ";

                //}

             //   
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new rptKUA()
                                {
                                     IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                     IDProgram= DataFormat.GetInteger(dr["IDProgram"]),
                                     IDKegiatan= DataFormat.GetInteger(dr["IDKegiatan"]),
                                     Kode=GetKode(DataFormat.GetInteger(dr["IDProgram"]),DataFormat.GetInteger(dr["IDKegiatan"]),DataFormat.GetInteger(dr["Level"])),
                                     Uraian= DataFormat.GetString (dr["Uraian"]),
                                     Pagu=DataFormat.GetDecimal(dr["Jumlah"]),
                                     Level = DataFormat.GetSingle(dr["Level"]),
                                     IDLokasi = DataFormat.GetSingle(dr["NoUrut"]),
                                     RKPD = DataFormat.GetDecimal(dr["JumlahRKPD"]),


                                }).ToList();
                    }
                }
                List<rptKUA> rtList = new List<rptKUA>();

                foreach (rptKUA k in _lst){
                    if (k.Pagu > 0)
                        rtList.Add(k);
                }
                return rtList;


            } catch(Exception ex){
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }

        public List<rptKUA> GetByDinas90(int _pDInas, bool _denganRincian = true, int _gabungan = 0)
        {
            List<rptKUA> _lst = new List<rptKUA>();

            try
            {
               
                SSQL = GetQuery(_pDInas);

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new rptKUA()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    Kode = GetKode90(DataFormat.GetInteger(dr["ID"]),
                                                DataFormat.GetInteger(dr["IDUrusan"]),
                                                DataFormat.GetInteger(dr["IDDinas"]),
                                                DataFormat.GetInteger(dr["IDProgram"]), 
                                                DataFormat.GetInteger(dr["IDKegiatan"]),
                                                DataFormat.GetLong(dr["IDSubKegiatan"]),
                                                DataFormat.GetInteger(dr["Level"])),
                                    Uraian = DataFormat.GetString(dr["Uraian"]),
                                    Pagu = DataFormat.GetDecimal(dr["Murni"]),
                                    Level = DataFormat.GetSingle(dr["Level"]),
                               //     IDLokasi = DataFormat.GetSingle(dr["NoUrut"]),
                                 //   RKPD = DataFormat.GetDecimal(dr["JumlahRKPD"]),


                                }).ToList();
                    }
                }
                List<rptKUA> rtList = new List<rptKUA>();

                foreach (rptKUA k in _lst)
                {
                    if (k.Pagu > 0)
                        rtList.Add(k);
                }
                return rtList;


            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        public decimal  GetJumlahByDinas90(int _pDInas, bool _denganRincian = true, int _gabungan = 0)
        {
            //List<rptKUA> mListUnit = new List<rptKUA>();
            decimal djumlah = 0l;

            try
            {

                SSQL = "SELECT SUM (JUmlahmurni ) as j from tKUA where iddinas=" + _pDInas.ToString() + " and IDSubKegiatan>0 ";
            //    SSQL = GetQuery(_pDInas);

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach( DataRow dr in dt.Rows){
                           // if(DataFormat.GetInteger(dr["level"])==6)
                                djumlah=djumlah + DataFormat.GetDecimal(dr["j"]);
                        }
                            
                    }
                }
        

                return djumlah;


            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return djumlah;
            }
        }
        private string  GetQuery(int skpd =0){
            
            string SaratSKPD="";
                if (skpd > 0)
                {
                    SaratSKPD = " AND  B.IDDInas =" + skpd.ToString();
                }

                SSQL = " select 1 as Level,A.btKodeKategori as ID  , 0 as idUrusan, 0 as iddinas, 0 as idProgram, 0 as IDKegiatan, 0 as idSUbKegiatan";
                SSQL = SSQL + " ,A.sNamaKategori as Uraian , SUM (B.JumlahMurni) as Murni,";
                SSQL = SSQL + " SUM (B.JumlahPerubahan) as Perubahan from mKategori A ";
                SSQL = SSQL + " INNER JOIN tKUA B ON B.IDUrusan/100= A.btKodeKategori WHERE B.idsubkegiatan>0 and  B.iTAhun = " + Tahun.ToString() + SaratSKPD ;
                SSQL = SSQL + " GROUP BY A.btKodeKategori , A.sNamaKategori ";
                SSQL = SSQL + " UNION  ";
                SSQL = SSQL + " select 2 as Level, A.ID/100 as ID  , A.ID as idUrusan, 0 as iddinas, 0 as idProgram, 0 as IDlegiatan, 0 as IDKegiatan ";
                SSQL = SSQL + " ,A.sNamaUrusan as Uraian , SUM (B.JumlahMurni) as Murni, ";
                SSQL = SSQL + " SUM (B.JumlahPerubahan) as Perubahan from mUrusan A INNER JOIN tKUA  B ";
                SSQL = SSQL + " ON B.IDUrusan = A.ID  INNER JOIN mPelaksanaUrusan pu ON  pu.IDDInas = B.IDDInas and pu.IDUrusan = B.IDUrusan WHERE   B.idsubkegiatan>0 and   B.iTahun = " + Tahun.ToString() + SaratSKPD + " Group BY A.ID, A.sNamaUrusan  ";
                SSQL = SSQL + " UNION  ";
                SSQL = SSQL + " select 3 as Level, B.IDUrusan/100 as ID  , B.IDUrusan as idUrusan, B.IDDinas as iddinas, 0 as idProgram, 0 as IDKegiatan, 0 as idSUbKegiatan ";
                SSQL = SSQL + " ,A.sNamaSKPD as Uraian , SUM (B.JumlahMurni) as Murni, ";
                SSQL = SSQL + " (Select SUM (B.JumlahPerubahan) )  as Perubahan from mSKPD A INNER JOIN tKUA B ON A.ID= B.IDDInas  ";
                SSQL = SSQL + " INNER JOIN mPelaksanaUrusan pu ON  pu.IDDInas = B.IDDInas and pu.IDUrusan = B.IDUrusan  WHERE  B.idsubkegiatan>0 and   B.iTahun =" + Tahun.ToString() + SaratSKPD + " GROUP BY B.IDUrusan, B.IDDInas, A.sNamaSKPD ";
                SSQL = SSQL + " UNION  ";
                SSQL = SSQL + " select 4 as Level, B.IDUrusan/100 as ID  , B.IDUrusan as idUrusan, B.IDDinas as iddinas, B.IDProgram as idProgram, 0 as IDKegiatan, 0 as idSUbKegiatan ";
                SSQL = SSQL + " ,A.sNamaProgram as Uraian , SUM (B.JumlahMurni) as Murni, ";
                SSQL = SSQL + " (Select SUM (B.JumlahPerubahan) )  as Perubahan from tPrograms_A A INNER JOIN tKUA B ON A.iTahun = B.ITahun AND A.IDDInas= B.IDDInas  ";
                SSQL = SSQL + " AND A.IDProgram = B.IDProgram  INNER JOIN mPelaksanaUrusan pu ON  pu.IDDInas = B.IDDInas and pu.IDUrusan = B.IDUrusan  WHERE B.iTahun =" + Tahun.ToString() + SaratSKPD;
                SSQL = SSQL + " GROUP BY B.IDUrusan, B.IDDInas, B.IDProgram , A.sNamaProgram ";
                SSQL = SSQL + " UNION  ";
                SSQL = SSQL + " select 5 as Level, B.IDUrusan/100 as ID  , B.IDUrusan as idUrusan, B.IDDinas as iddinas, B.IDProgram as idProgram, B.IDKegiatan, 0 as idSUbKegiatan ";
                SSQL = SSQL + " ,A.sNama as Uraian , SUM (B.JumlahMurni) as Murni, ";
                SSQL = SSQL + " (Select SUM (B.JumlahPerubahan) )  as Perubahan from tKegiatan_A A INNER JOIN tKUA B ON A.iTahun = B.ITahun AND A.IDDInas= B.IDDInas  ";
                SSQL = SSQL + " AND A.IDProgram = B.IDProgram  AND A.iDKegiatan= B.IDKegiatan INNER JOIN mPelaksanaUrusan pu ON  pu.IDDInas = B.IDDInas and pu.IDUrusan = B.IDUrusan  WHERE B.iTahun =" + Tahun.ToString() + SaratSKPD + " ";
                SSQL = SSQL + " GROUP BY B.IDUrusan, B.IDDInas, B.IDProgram , A.sNama, B.IDKegiatan ";
                SSQL = SSQL + " UNION  ";
                SSQL = SSQL + " select 6 as Level, B.IDUrusan/100 as ID  , B.IDUrusan as idUrusan, B.IDDinas as iddinas, B.IDProgram as idProgram, B.IDKegiatan, B.idSUbKegiatan ";
                SSQL = SSQL + " ,A.Nama as Uraian , SUM (B.JumlahMurni) as Murni, ";
                SSQL = SSQL + " (Select SUM (B.JumlahPerubahan) )  as Perubahan from TSUbKegiatan A INNER JOIN tKUA B ON A.iTahun = B.ITahun AND A.IDDInas= B.IDDInas  ";
                SSQL = SSQL + " AND A.IDProgram = B.IDProgram  AND A.iDKegiatan= B.IDKegiatan  AND A.IDSubKegiatan= B.IDSubKegiatan  ";
                SSQL = SSQL + " INNER JOIN mPelaksanaUrusan pu ON  pu.IDDInas = B.IDDInas and pu.IDUrusan = B.IDUrusan  WHERE B.iTahun =" + Tahun.ToString() + SaratSKPD + " GROUP BY B.IDUrusan, B.IDDInas, B.IDProgram , A.Nama, B.IDKegiatan, B.IDSUbKegiatan ";
                SSQL = SSQL + " ORDER BY ID,IDUrusan,IDDinas, IDProgram, IDKegiatan, IDSUbKegiatan";
                return SSQL;

        }
        public List<RKPDKUA> GetRKPDKUAByDinas(int _pDInas, bool _denganRincian = true)
        {
            List<RKPDKUA> _lst = new List<RKPDKUA>();

            try
            {

                SSQL = "Select 1 as Level, tKUA.IDUrusan,tKUA.IDProgram,0 as  IDKegiatan,  IDUrusanMaster,IDProgramMaster,0 as IDKegiatanMaster,mProgram.sNamaProgram as  Uraian,0 as IDLokasi, SUM(tKUA.JumlahMurni) as Jumlah,SUM(tKUA.JumlahRKPD) as JumlahRKPD,0 as Nourut " +
                         " FROM tKUA INNER JOIN mProgram ON tKUA.IDUrusanMaster= mProgram.IDUrusan and tKUA.IDProgramMaster= mProgram.ID " +
                         " where tKUA.iTahun = " + Tahun.ToString() + " AND tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pDInas.ToString() + " AND isnull(tKUA.Status,0)<9 " +
                        " GROUP BY tKUA.IDUrusan,tKUA.IDProgram,IDUrusanMaster,IDProgramMaster,mProgram.sNamaProgram  " +
                        " UNION  " +
                        " Select 2 as Level,tKUA.IDUrusan,tKUA.IDProgram,tKUA.IDKegiatan, IDUrusanMaster,IDProgramMaster,IDKegiatanMaster as IDKegiatanMaster,mKegiatan.sNamaKegiatan as Uraian, 0 as IDLokasi,SUM(tKUA.JumlahMurni)  as Jumlah,SUM(tKUA.JumlahRKPD) as JumlahRKPD,0 as Nourut  " +
                        " FROM tKUA INNER JOIN mKegiatan ON  mKegiatan.ID=tKUA.IDKegiatanMaster " +
                        " where tKUA.iTahun = " + Tahun.ToString() + " AND tKUA.IDlokasi=0 and tKUA.IDDInas= " + _pDInas.ToString() + " AND isnull(tKUA.Status,0)<9 " +
                        " GROUP BY tKUA.IDUrusan,tKUA.IDProgram,tKUA.IDKegiatan, IDUrusanMaster,IDProgramMaster,IDKegiatanMaster,mKegiatan.sNamaKegiatan ";

                //if (_denganRincian) {

                //    SSQL = SSQL + " UNION " +
                //     " Select 3 as Level, tKUA.IDUrusan,tKUA.IDProgram,tKUA.IDKegiatan,IDUrusanMaster,IDProgramMaster,IDKegiatanMaster as IDKegiatanMaster,Usulan as Uraian, IDLokasi,tKUA.JumlahOlah  as Jumlah,NoUrut " +
                //     " FROM tKUA  " +
                //     " where tKUA.iTahun = " + Tahun.ToString() + " AND  tKUA.IDlokasi>0  and tKUA.IDDInas= " + _pDInas.ToString() + " AND isnull(tKUA.Status,0)<9 ";

                //}

                SSQL = SSQL + " ORDER BY IDUrusan,IDProgram,IDKegiatan, NoUrut,IDLokasi ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RKPDKUA()
                                {
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    Kode = GetKode(DataFormat.GetInteger(dr["IDProgram"]), DataFormat.GetInteger(dr["IDKegiatan"]), DataFormat.GetInteger(dr["Level"])),
                                    KUA = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
                                    Nama = DataFormat.GetString (dr["Uraian"]),
                                    Level = DataFormat.GetSingle(dr["Level"]),
                                    RKPD = DataFormat.GetDecimal(dr["JumlahRKPD"]).ToRupiahInReport(),
                                    DKUA = DataFormat.GetDecimal(dr["Jumlah"]),
                                    DRKPD = DataFormat.GetDecimal(dr["JumlahRKPD"]),
                                    DSelisih = DataFormat.GetDecimal(dr["JumlahRKPD"]) - DataFormat.GetDecimal(dr["Jumlah"]),
                                    Selisih = (DataFormat.GetDecimal(dr["JumlahRKPD"]) - DataFormat.GetDecimal(dr["Jumlah"])).ToRupiahInReport()
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
        private string GetKode(int idProgram,int idKegiatan, int _level)
        {
            try
            {
                string s;
                if (_level == 3)
                {
                    return "";
                }
                s = idProgram.ToString().Substring(0, 1) + "." + idProgram.ToString().Substring(1, 2) + "." + idProgram.ToString().Substring(3, 2);

                if (_level == 2)
                {
                    s = s + "." + idKegiatan.ToString().Substring(5);
                }
                return s;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return "";
            }

        }
        private string GetKode90(int ID, int IDurusan, int iddinas, int idProgram, int idKegiatan, long idSubKegiatan , int _level)
        {
            try
            {
                string s;
                if (_level == 1)
                {
                    return ID.ToString();
                }
                if (_level == 2)
                {
                    return IDurusan.ToKodeUrusan();
                }

                if (_level == 3)
                {
                    return IDurusan.ToKodeUrusan() + "." + iddinas.ToKodeDinas()  ;
                }
             //   s = idProgram.ToString().Substring(0, 1) + "." + idProgram.ToString().Substring(1, 2) + "." + idProgram.ToString().Substring(3, 2);

                if (_level == 4)
                {
                    return IDurusan.ToKodeUrusan() + "." + iddinas.ToKodeDinas() + "." + (idProgram % 100).ToString();
                    
                }
                if (_level == 5)
                {
                    return IDurusan.ToKodeUrusan() + "." + iddinas.ToKodeDinas() + "." + (idProgram % 100).ToString() + "." +
                        (idKegiatan % 1000).ToString().Substring(0, 1) + "." + (idKegiatan % 1000).ToString().Substring(1);  

                }
                if (_level == 6)
                {
                    string ssubkegiatan = idSubKegiatan.ToString();
                    int len = ssubkegiatan.Length;

                    return IDurusan.ToKodeUrusan() + "." + iddinas.ToKodeDinas() + "." + (idProgram % 100).ToString() + "." +
                        (idKegiatan % 1000).ToString().Substring(0, 1) + "." + (idKegiatan % 1000).ToString().Substring(1) + "." + ssubkegiatan.Substring(len - 2);

                }

                return "";
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return "";
            }

        }
       
    }
}
