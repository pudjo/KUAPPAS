using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using Formatting;
using DataAccess;
using System.Data;


namespace BP
{
    public class RekapLogic: BP
    {
        public RekapLogic(int _pTahun)
            : base(_pTahun)
        {
            
        }
        public List<RekapProgramKegiatanUmum> GetRekap(int _pTahun, int _idDinas)
        {
            List<RekapProgramKegiatanUmum> _lst = new List<RekapProgramKegiatanUmum>();
            try
            {
                SSQL = "Select 1 as Visible,1 as Header,mProgram.ID as IDPRG, 0  as IDKEg,0 as KodeSKPD, 0 as IDRek, mProgram.sNamaProgram as Nama,SUM(tANggaranRekening_A.cJumlahRKA) as JumlahInput " +
                        ",SUM(tANggaranRekening_A.cPlafon) as JumlahPagu from mProgram INNER JOIN tAnggaranRekening_A ON mProgram.ID = tAnggaranRekening_A.IDProgram % 100 " +
                        " INNER JOIN tKegiatan_A A On tAnggaranRekening_A.iTahun = A.iTahun and tAnggaranRekening_A.IDDInas = A.IDDInas and tAnggaranRekening_A.IDUrusan =A.IDUrusan "+
                        " AND tAnggaranRekening_A.IDProgram = A.IDProgram and tAnggaranRekening_A.IDKegiatan = A.IDKegiatan and "+
                        " tAnggaranRekening_A.btJenis = A.btJenis WHERE tAnggaranRekening_A.iTAhun =" + _pTahun.ToString();
                if (_idDinas > 0)
                {
                    SSQL = SSQL + " AND tAnggaranRekening_A.IDDinas=" + _idDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY mProgram.ID, mProgram.sNamaProgram ";

                SSQL = SSQL + "  UNION ALL ";
                SSQL = SSQL + "  Select 0 as Visible,1 as Header, mKegiatan.IDProgram as IDPRG, mKegiatan.ID as IDKEg, 0 as KodeSKPD,0 as IDRek, 'Kegiatan: ' + mKegiatan.sNamaKegiatan as Nama,SUM(tANggaranRekening_A.cJumlahOlah) as JumlahInput " +
                    " ,SUM(tANggaranRekening_A.cPlafon) as JumlahPagu from mKegiatan INNER JOIN tAnggaranRekening_A ON mKegiatan.IDProgram = tAnggaranRekening_A.IDProgram % 100 " +
                    " AND mKegiatan.ID = tAnggaranRekening_A.IDKegiatan % 10000 " +
                    "INNER JOIN tKegiatan_A A On tAnggaranRekening_A.iTahun = A.iTahun and tAnggaranRekening_A.IDDInas = A.IDDInas and tAnggaranRekening_A.IDUrusan =A.IDUrusan " +
                        " AND tAnggaranRekening_A.IDProgram = A.IDProgram and tAnggaranRekening_A.IDKegiatan = A.IDKegiatan and " +
                        " tAnggaranRekening_A.btJenis = A.btJenis  WHERE tAnggaranRekening_A.iTAhun =" + _pTahun.ToString();
                if (_idDinas > 0)
                {
                    SSQL = SSQL + " AND tAnggaranRekening_A.IDDinas=" + _idDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY mKegiatan.IDProgram , mKegiatan.ID , mKegiatan.sNamaKegiatan ";

                SSQL = SSQL + "  UNION ALL";

                SSQL = SSQL + "  Select 0 as Visible,1 as Header, mProgram.ID as IDPRG, mKegiatan.ID as IDKeg, mSKPD.ID as KodeSKPD, 0 as IDRek, 'Dinas: ' + mSKPD.sNamaSKPD as Nama, SUM(tANggaranRekening_A.cJumlahOlah) as JumlahInput " +
                    ",SUM(tANggaranRekening_A.cPlafon) as JumlahPagu from mProgram INNER JOIN tAnggaranRekening_A ON mProgram.ID = tAnggaranRekening_A.IDProgram % 100 " +
                        " INNER JOIN mSKPD ON mSKPD.ID= tAnggaranRekening_A.IDDInas " +
                        " INNER JOIN mKegiatan ON mKegiatan.ID = tAnggaranRekening_A.IDKegiatan % 10000 " +
                        " INNER JOIN tKegiatan_A A On tAnggaranRekening_A.iTahun = A.iTahun and tAnggaranRekening_A.IDDInas = A.IDDInas and tAnggaranRekening_A.IDUrusan =A.IDUrusan " +
                        " AND tAnggaranRekening_A.IDProgram = A.IDProgram and tAnggaranRekening_A.IDKegiatan = A.IDKegiatan and " +
                        " tAnggaranRekening_A.btJenis = A.btJenis  WHERE tAnggaranRekening_A.iTAhun =" + _pTahun.ToString();
                if (_idDinas > 0)
                {
                    SSQL = SSQL + " AND tAnggaranRekening_A.IDDinas=" + _idDinas.ToString();
                }
                SSQL = SSQL + " Group BY mProgram.ID, mKegiatan.ID,mSKPD.ID , mSKPD.sNamaSKPD";

                SSQL = SSQL + " UNION ALL Select 0 as Visible,0 as Header, mProgram.ID as IDPRG, mKegiatan.ID as IDKeg, mSKPD.ID as KodeSKPD, mRekening.IIDRekening as IDRek,'Rekening: ' + mRekening.sNamaRekening as Nama, SUM(tANggaranRekening_A.cJumlahOlah) as JumlahInput ,SUM(tANggaranRekening_A.cPlafon) as JumlahPagu " +
	                    " from mProgram INNER JOIN tAnggaranRekening_A ON mProgram.ID = tAnggaranRekening_A.IDProgram % 100  " +
	                    " INNER JOIN mSKPD ON mSKPD.ID= tAnggaranRekening_A.IDDInas   " +
	                    " INNER JOIN mKegiatan ON mKegiatan.ID = tAnggaranRekening_A.IDKegiatan % 10000  " +
	                    " INNER JOIN mRekening ON mRekening.IIDrekening= tANggaranRekening_A.IIDRekening " +
                        " INNER JOIN tKegiatan_A A On tAnggaranRekening_A.iTahun = A.iTahun and tAnggaranRekening_A.IDDInas = A.IDDInas and tAnggaranRekening_A.IDUrusan =A.IDUrusan " +
                        " AND tAnggaranRekening_A.IDProgram = A.IDProgram and tAnggaranRekening_A.IDKegiatan = A.IDKegiatan and " +
                        " tAnggaranRekening_A.btJenis = A.btJenis WHERE tAnggaranRekening_A.iTAhun =" + _pTahun.ToString();
                if (_idDinas > 0)
                {
                    SSQL = SSQL + " AND tAnggaranRekening_A.IDDinas=" + _idDinas.ToString();
                }
                SSQL = SSQL + "  Group BY mProgram.ID, mKegiatan.ID,mSKPD.ID , mRekening.sNamaRekening, mRekening.IIDRekening ";
                SSQL = SSQL + " ORDER BY IDPRG,IDKeg,KodeSKPD , IDREK";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RekapProgramKegiatanUmum()
                                {
                                    Visible = DataFormat.GetSingle(dr["Visible"]),
                                    Header = DataFormat.GetSingle(dr["Header"]),
                                    KodeProgram =DataFormat.GetInteger(dr["IDPRG"]),
                                    KodeKegiatan=DataFormat.GetInteger(dr["IDKeg"]),
                                    KodeDinas = DataFormat.GetLong(dr["KodeSKPD"]).ToKodeDinas(),
                                    KodeRekening = DataFormat.GetLong(dr["IDRek"]).ToKodeRekening(m_ProfileRekening),
                                    Nama = DataFormat.GetLong(dr["KodeSKPD"]) == 0 ? DataFormat.GetString(dr["Nama"]).ToUpper() : DataFormat.GetString(dr["Nama"]),
                                    JumlahInput=DataFormat.GetDecimal(dr["JumlahInput"]).ToRupiahInReport(),
                                    JumlahPagu=DataFormat.GetDecimal(dr["JumlahPagu"]).ToRupiahInReport(),
                                    Selisih = DataFormat.GetDecimal(dr["JumlahPagu"]) - DataFormat.GetDecimal(dr["JumlahInput"])                                     
                                }).ToList();
                    }
                }
                return _lst;
            }

            catch (Exception ex)
            {
               _isError= true;
               _lastError = ex.Message;
               return null;
            }
            

        }

        public List<RekapDashBoard> GetRekapII(int _pTahun, int iAnggaranPerubahan, int? _IDDinas=0)
        {
            List<RekapDashBoard> _lst = new List<RekapDashBoard>();
            try
            {
                SSQL = "";

                if (Tahun >= 2020)
                {


                    SSQL = "Select 1 as Level, A.ID AS IDDinas,0 as IDUrusan, 0 as IDProgram,0 as IDkegiatan, 0 as IDRekening, A.sNamaSKPD AS Nama, 0 as btJenis, " +
                         " SUM(CASE WHEN B.IIDRekening /1000000=4 THEN B.cJumlahOlah ELSE 0 END)AS PENDAPATANINPUT, " +
                        " SUM(CASE WHEN B.IIDRekening /100000=51 THEN B.cJumlahOlah ELSE 0 END)AS BTLINPUT , " +
                        " SUM(CASE WHEN B.IIDRekening /100000=52 THEN B.cJumlahOlah ELSE 0 END)AS BLINPUT, " +
                        "   ( SELECT PaguMurni from PaguSKPD where IDDInas = A.ID  and iTahun = " + Tahun.ToString() + " and Jenis = 1)AS PENDAPATANPAGU, " +
                      " (SELECT PaguMurni from PaguSKPD where IDDInas = A.ID  and iTahun = " + Tahun.ToString() + " and Jenis = 2)AS BTLPAGU , " +
                      " (SELECT PaguMurni from PaguSKPD where IDDInas = A.ID  and iTahun = " + Tahun.ToString() + " and Jenis = 3 )AS BLPAGU from mSKPD A " +
                      " INNER JOIN tANGGARANREKENING_A B ON A.ID = B.IDDInas " +
                      " INNER JOIN tKegiatan_A C on B.IDDInas = C.IDDInas and B.IDUrusan = C.IDUrusan   and B.IDProgram= C.IDProgram AND " +
                      " B.IDKegiatan = C.IDKegiatan and B.btJenis = C.BTJenis WHERE B.iTahun =   " + _pTahun.ToString() +
                      " GROUP BY A.ID ,A.sNamaSKPD  ";
    

                    SSQL = SSQL + "UNION " +
                " Select 2 as Level, B.IDDInas  AS IDDinas, A.ID as IDUrusan, 0  as IDProgram,0 as IDkegiatan, 0 as IDRekening, A.sNamaUrusan AS Nama, 0 as btJenis ," +
                " SUM(CASE WHEN B.IIDRekening /1000000=4 THEN cJumlahOlah ELSE 0 END)AS PENDAPATANINPUT, " +
                " SUM(CASE WHEN B.IIDRekening /100000=51 THEN cJumlahOlah ELSE 0 END)AS BTLINPUT , " +
                " SUM(CASE WHEN B.IIDRekening /100000=52 THEN cJumlahOlah ELSE 0 END)AS BLINPUT, " +
                "   ( SELECT PaguMurni from PaguSKPD where IDDInas = B.IDDInas and iTahun = " + Tahun.ToString() + " and Jenis = 1)AS PENDAPATANPAGU, " +
                      " (SELECT PaguMurni from PaguSKPD where IDDInas = B.IDDInas and iTahun = " + Tahun.ToString() + " and Jenis = 2)AS BTLPAGU , " +
                      " (SELECT SUM(jumlahMurni) from tKUA WHERE IDDInas = B.IDDInas AND IDUrusan = A.ID )  AS BLPAGU from " +
                    " mUrusan A " +
                " INNER JOIN tANGGARANREKENING_A B ON A.ID= B.IDUrusan " +
                " WHERE B.iTahun = " + _pTahun.ToString() +
                " GROUP BY B.IDDInas  , A.ID , A.sNamaUrusan ";
                    SSQL = SSQL + " UNION ALL " +
                        " Select 3 as Level, B.IDDInas  AS IDDinas, A.IDUrusan as IDUrusan, A.IDProgram  as IDProgram,0 as IDkegiatan, 0 as IDRekening,  " +
                        " A.sNamaProgram AS Nama, B.btJenis, " +
                        "  SUM(CASE WHEN B.IIDRekening /1000000=4 THEN cJumlahOlah ELSE 0 END)AS PENDAPATANINPUT, " +
                        " SUM(CASE WHEN B.IIDRekening /100000=51 THEN cJumlahOlah ELSE 0 END)AS BTLINPUT , " +
                        " SUM(CASE WHEN B.IIDRekening /100000=52 THEN cJumlahOlah ELSE 0 END)AS BLINPUT, " +
                        "  0 AS PENDAPATANPAGU, " +
                      " 0 AS BTLPAGU , " +
                       " (SELECT SUM(jumlahMurni) from tKUA WHERE IDDInas = B.IDDInas AND IDUrusan = A.IDUrusan and IDProgram = A.IDProgram  )  AS BLPAGU " +
                        " from tPrograms_A A " +
                        " INNER JOIN tANGGARANREKENING_A B ON A.iTahun = B.iTahun AND A.IDUrusan= B.IDUrusan AND A.IDDinas= B.IDDInas AND A.IDProgram= B.IDProgram and A.btJenis=B.btJenis " +
                        " WHERE B.iTahun = " + _pTahun.ToString() +
                        " GROUP BY B.IDDInas  , A.IDUrusan , A.IDProgram  ,A.sNamaProgram,B.btJenis  ";

                    SSQL = SSQL + "  UNION ALL " +
                    " Select 4 as Level, B.IDDInas  AS IDDinas, A.IDUrusan as IDUrusan, A.IDProgram  as IDProgram,A.IDkegiatan as IDkegiatan, 0 as IDRekening,  " +
                    " A.sNama AS Nama, B.btJenis as btJenis," +
                    " SUM(CASE WHEN B.IIDRekening /1000000=4 THEN cJumlahOlah ELSE 0 END)AS PENDAPATANINPUT, " +
                    " SUM(CASE WHEN B.IIDRekening /100000=51 THEN cJumlahOlah ELSE 0 END)AS BTLINPUT , " +
                    " SUM(CASE WHEN B.IIDRekening /100000=52 THEN cJumlahOlah ELSE 0 END)AS BLINPUT, " +
                    "   0 AS PENDAPATANPAGU, " +
                      " 0 AS BTLPAGU , " +
                      "  (SELECT SUM(tKUA.JumlahMurni) from tKUA where IDDInas = B.IDDinas  and iTahun = 2020 and btJenis = 3  " +
                     " AND IDUrusan = A.IDurusan AND IDProgram =A.IDProgram AND IDKEgiatan = A.IDKegiatan)AS BLPAGU from tKegiatan_A A " +
                    " INNER JOIN tANGGARANREKENING_A B ON A.iTahun = B.iTahun AND A.IDUrusan= B.IDUrusan AND A.IDDinas= B.IDDInas AND A.IDProgram= B.IDProgram " +
                    " AND A.IDKegiatan=B.IDKegiatan AND A.btJenis=B.btJenis  " +
                    " WHERE B.iTahun = " + _pTahun.ToString() +
                    " GROUP BY B.IDDInas  , A.IDUrusan , A.IDProgram ,A.IDKegiatan ,A.sNama,B.btJenis  ";
                    

                //    SSQL = SSQL + "  UNION ALL " +
                //    " Select 5 as Level, B.IDDInas  AS IDDinas, B.IDUrusan as IDUrusan, B.IDProgram  as IDProgram,B.IDkegiatan as IDkegiatan, A.IIDRekening as IDRekening,  " +
                //    " A.sNamaRekening AS Nama,B.btJenis," +
                //    " SUM(CASE WHEN B.IIDRekening /1000000=4 THEN cJumlahOlah ELSE 0 END)AS PENDAPATANINPUT, " +
                //    " SUM(CASE WHEN B.IIDRekening /100000=51 THEN cJumlahOlah ELSE 0 END)AS BTLINPUT , " +
                //    " SUM(CASE WHEN B.IIDRekening /100000=52 THEN cJumlahOlah ELSE 0 END)AS BLINPUT, " +
                //    "   0 AS PENDAPATANPAGU, " +
                //    " 0 AS BTLPAGU , " +
                //    " 0 as BLPAGU from mRekening  A " +
                //    " INNER JOIN tANGGARANREKENING_A B ON A.IIDRekening= B.IIDRekening  " +
                //    " WHERE B.iTahun = " + _pTahun.ToString() +
                //    " GROUP BY B.IDDInas  , B.IDUrusan , B.IDProgram  ,B.IDkegiatan , A.IIDRekening , A.sNamaRekening,B.btJenis  " +
                //    " ORDER BY IDDInas  , IDUrusan , IDProgram  ,IDkegiatan , IDRekening ";
                

                }
                else
                {
                    string sNamaKolom = "";
                    if (iAnggaranPerubahan == 0)
                        sNamaKolom = "cJumlahRKA";
                    else
                        sNamaKolom = "cJumlahRKAP";

                    if (_IDDinas == 0)
                    {
                        SSQL = "Select 1 as Level, A.ID AS IDDinas,0 as IDUrusan, 0 as IDProgram,0 as IDkegiatan, 0 as IDRekening, A.sNamaSKPD AS Nama, 0 as btJenis, " +
                        " SUM(CASE WHEN B.IIDRekening /1000000=4 THEN B." + sNamaKolom .Trim() + " ELSE 0 END)AS PENDAPATANINPUT, " +
                        " SUM(CASE WHEN B.IIDRekening /100000=51 THEN B." + sNamaKolom.Trim() + " ELSE 0 END)AS BTLINPUT , " +
                        " SUM(CASE WHEN B.IIDRekening /100000=52 THEN B." + sNamaKolom.Trim() + " ELSE 0 END)AS BLINPUT, " +
                        "   SUM(CASE WHEN B.IIDRekening /1000000=4 THEN B.cPlafon ELSE 0 END)AS PENDAPATANPAGU, " +
                        " SUM(CASE WHEN B.IIDRekening /100000=51 THEN B.cPlafon ELSE 0 END)AS BTLPAGU , " +
                        " SUM(CASE WHEN B.IIDRekening /100000=52 THEN B.cPlafon ELSE 0 END)AS BLPAGU from mSKPD A " +
                        " INNER JOIN tANGGARANREKENING_A B ON A.ID = B.IDDInas " +
                        " WHERE B.iTahun =   " + _pTahun.ToString() +
                        " GROUP BY A.ID ,A.sNamaSKPD  ";

                        SSQL = SSQL + " UNION ALL ";
                    }


                    SSQL = SSQL + "UNION " +
                    " Select 2 as Level, B.IDDInas  AS IDDinas, A.ID as IDUrusan, 0  as IDProgram,0 as IDkegiatan, 0 as IDRekening, A.sNamaUrusan AS Nama, 0 as btJenis ," +
                    " SUM(CASE WHEN B.IIDRekening /1000000=4 THEN " + sNamaKolom.Trim() + " ELSE 0 END)AS PENDAPATANINPUT, " +
                    " SUM(CASE WHEN B.IIDRekening /100000=51 THEN " + sNamaKolom.Trim() + " ELSE 0 END)AS BTLINPUT , " +
                    " SUM(CASE WHEN B.IIDRekening /100000=52 THEN " + sNamaKolom.Trim() + " ELSE 0 END)AS BLINPUT, " +
                    "   SUM(CASE WHEN B.IIDRekening /1000000=4 THEN B.cPlafon ELSE 0 END)AS PENDAPATANPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /100000=51 THEN B.cPlafon ELSE 0 END)AS BTLPAGU , " +
                    " SUM(CASE WHEN B.IIDRekening /100000=52 THEN B.cPlafon ELSE 0 END)AS BLPAGU  from mUrusan A " +
                    " INNER JOIN tANGGARANREKENING_A B ON A.ID= B.IDUrusan " +
                    " WHERE B.iTahun = " + _pTahun.ToString() +
                    " GROUP BY B.IDDInas  , A.ID , A.sNamaUrusan ";
                    SSQL = SSQL + " UNION ALL " +
                        " Select 3 as Level, B.IDDInas  AS IDDinas, A.IDUrusan as IDUrusan, A.IDProgram  as IDProgram,0 as IDkegiatan, 0 as IDRekening,  " +
                        " A.sNamaProgram AS Nama, B.btJenis, " +
                        "  SUM(CASE WHEN B.IIDRekening /1000000=4 THEN " + sNamaKolom.Trim() + " ELSE 0 END)AS PENDAPATANINPUT, " +
                        " SUM(CASE WHEN B.IIDRekening /100000=51 THEN " + sNamaKolom.Trim() + " ELSE 0 END)AS BTLINPUT , " +
                        " SUM(CASE WHEN B.IIDRekening /100000=52 THEN " + sNamaKolom.Trim() + " ELSE 0 END)AS BLINPUT, " +
                        "   SUM(CASE WHEN B.IIDRekening /1000000=4 THEN B.cPlafon ELSE 0 END)AS PENDAPATANPAGU, " +
                        " SUM(CASE WHEN B.IIDRekening /100000=51 THEN B.cPlafon ELSE 0 END)AS BTLPAGU , " +
                        " SUM(CASE WHEN B.IIDRekening /100000=52 THEN B.cPlafon ELSE 0 END)AS BLPAGU  from tPrograms_A A " +
                        " INNER JOIN tANGGARANREKENING_A B ON A.iTahun = B.iTahun AND A.IDUrusan= B.IDUrusan AND A.IDDinas= B.IDDInas AND A.IDProgram= B.IDProgram and A.btJenis=B.btJenis " +
                        " WHERE B.iTahun = " + _pTahun.ToString() +
                        " GROUP BY B.IDDInas  , A.IDUrusan , A.IDProgram  ,A.sNamaProgram,B.btJenis  ";

                    SSQL = SSQL + "  UNION ALL " +
                    " Select 4 as Level, B.IDDInas  AS IDDinas, A.IDUrusan as IDUrusan, A.IDProgram  as IDProgram,A.IDkegiatan as IDkegiatan, 0 as IDRekening,  " +
                    " A.sNama AS Nama, B.btJenis as btJenis," +
                    " SUM(CASE WHEN B.IIDRekening /1000000=4 THEN " + sNamaKolom.Trim() + " ELSE 0 END)AS PENDAPATANINPUT, " +
                    " SUM(CASE WHEN B.IIDRekening /100000=51 THEN " + sNamaKolom.Trim() + " ELSE 0 END)AS BTLINPUT , " +
                    " SUM(CASE WHEN B.IIDRekening /100000=52 THEN " + sNamaKolom.Trim() + " ELSE 0 END)AS BLINPUT, " +
                    "   SUM(CASE WHEN B.IIDRekening /1000000=4 THEN B.cPlafon ELSE 0 END)AS PENDAPATANPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /100000=51 THEN B.cPlafon ELSE 0 END)AS BTLPAGU , " +
                    " SUM(CASE WHEN B.IIDRekening /100000=52 THEN B.cPlafon ELSE 0 END)AS BLPAGU  from tKegiatan_A A " +
                    " INNER JOIN tANGGARANREKENING_A B ON A.iTahun = B.iTahun AND A.IDUrusan= B.IDUrusan AND A.IDDinas= B.IDDInas AND A.IDProgram= B.IDProgram " +
                    " AND A.IDKegiatan=B.IDKegiatan AND A.btJenis=B.btJenis  " +
                    " WHERE B.iTahun = " + _pTahun.ToString() +
                    " GROUP BY B.IDDInas  , A.IDUrusan , A.IDProgram ,A.IDKegiatan ,A.sNama,B.btJenis  ";
                    SSQL = SSQL + "  UNION ALL " +
                    " Select 5 as Level, B.IDDInas  AS IDDinas, B.IDUrusan as IDUrusan, B.IDProgram  as IDProgram,B.IDkegiatan as IDkegiatan, A.IIDRekening as IDRekening,  " +
                    " A.sNamaRekening AS Nama,B.btJenis," +
                    " SUM(CASE WHEN B.IIDRekening /1000000=4 THEN " + sNamaKolom.Trim() + " ELSE 0 END)AS PENDAPATANINPUT, " +
                    " SUM(CASE WHEN B.IIDRekening /100000=51 THEN " + sNamaKolom.Trim() + " ELSE 0 END)AS BTLINPUT , " +
                    " SUM(CASE WHEN B.IIDRekening /100000=52 THEN " + sNamaKolom.Trim() + " ELSE 0 END)AS BLINPUT, " +
                    "   SUM(CASE WHEN B.IIDRekening /1000000=4 THEN B.cPlafon ELSE 0 END)AS PENDAPATANPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /100000=51 THEN B.cPlafon ELSE 0 END) AS BTLPAGU , " +
                    " SUM(CASE WHEN B.IIDRekening /100000=52 THEN B.cPlafon ELSE 0 END)AS BLPAGU  from mRekening  A " +
                    " INNER JOIN tANGGARANREKENING_A B ON A.IIDRekening= B.IIDRekening  " +
                    " WHERE B.iTahun = " + _pTahun.ToString() +
                    " GROUP BY B.IDDInas  , B.IDUrusan , B.IDProgram  ,B.IDkegiatan , A.IIDRekening , A.sNamaRekening,B.btJenis  ";
                }
                SSQL = SSQL + " ORDER BY IDDInas  , IDUrusan , IDProgram  ,IDkegiatan , IDRekening ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RekapDashBoard()
                                {

                                    Level = DataFormat.GetInteger(dr["Level"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Kode = GetKode(DataFormat.GetInteger(dr["Level"]),
                                            DataFormat.GetInteger(dr["IDDInas"]),
                                            DataFormat.GetInteger(dr["IDUrusan"]),
                                            DataFormat.GetInteger(dr["IDProgram"]),
                                            DataFormat.GetInteger(dr["IDKegiatan"]),
                                            DataFormat.GetLong(dr["IDRekening"])),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDkegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    IDRekening = DataFormat.GetLong(dr["IDRekening"]),
                                    Nama=DataFormat.GetString(dr["Nama"]).ToUpper(),
                                    PENDAPATANINPUT = DataFormat.GetDecimal(dr["PENDAPATANINPUT"]).ToRupiahInReport(),
                                    BTLINPUT = DataFormat.GetDecimal(dr["BTLINPUT"]).ToRupiahInReport(),
                                    BLINPUT = DataFormat.GetDecimal(dr["BLINPUT"]).ToRupiahInReport(),
                                    PENDAPATANPAGU = DataFormat.GetDecimal(dr["PENDAPATANPAGU"]).ToRupiahInReport(),
                                    BTLPAGU = DataFormat.GetDecimal(dr["BTLPAGU"]).ToRupiahInReport(),
                                    BLPAGU = DataFormat.GetDecimal(dr["BLPAGU"]).ToRupiahInReport(),
                                   SelisihBelanja= DataFormat.GetDecimal(  DataFormat.GetDecimal(dr["BTLPAGU"]) + DataFormat.GetDecimal(dr["BLPAGU"]) -
                                                  DataFormat.GetDecimal(dr["BTLINPUT"]) -DataFormat.GetDecimal(dr["BLINPUT"])).ToRupiahInReport(),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"])
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

       
        public List<DashboardIII> GetRekapIII(int _pTahun, long pIDRekening)
        {
            List<DashboardIII> _lst = new List<DashboardIII>();
            try
            {
                SSQL = "select 1 as Level, A.ID as IDDInas, 0 as IDProgram, 0 as IDKegiatan, A.sNamaSKPD as Nama, SUM(B.cJumlahOlah) as JumlahInput, " +
                        " SUM(B.cPlafon) as Pagu FROM mSKPD A INNER JOIN tANGGARANRekening_A B ON A.ID = B.IDDInas " +
                        " INNER JOIN tKegiatan_A C On B.iTahun = C.iTahun and B.IDDInas = C.IDDInas and B.IDUrusan =C.IDUrusan " +
                        " AND B.IDProgram = C.IDProgram and B.IDKegiatan = C.IDKegiatan " +
                        " WHERE B.iTahun =" + _pTahun.ToString() + " AND B.IIDRekening = " + pIDRekening.ToString().Trim() +
                        " GROUP BY A.ID,A.sNamaSKPD " +
                        " HAVING SUM(B.cJumlahOlah) >0 or SUM(B.cPlafon) >0 " +
                        " UNION " +
                        " select 3 as Level, A.IDDInas as IDDInas, A.IDProgram as IDProgram, 0 as IDKegiatan, A.sNamaProgram as Nama, SUM(B.cJumlahOlah) as JumlahInput, " +
                        " SUM(B.cPlafon) as Pagu FROM tPrograms_A A INNER JOIN tANGGARANRekening_A B ON A.IDDInas = B.IDDInas AND A.iTahun = B.iTahun " +
                        " AND A.IDProgram= B.IDProgram and A.IDUrusan= B.IDUrusan AND A.btJenis= B.btJenis " +
                        " INNER JOIN tKegiatan_A C On B.iTahun = C.iTahun and B.IDDInas = C.IDDInas and B.IDUrusan =C.IDUrusan " +
                        " AND B.IDProgram = C.IDProgram and B.IDKegiatan = C.IDKegiatan and " +
                        " B.btJenis = A.btJenis WHERE B.iTahun =" + _pTahun.ToString() + " AND B.IIDRekening = " + pIDRekening.ToString().Trim() +
                        " GROUP BY A.IDDInas , A.IDProgram,A.sNamaProgram " +
                        " HAVING SUM(B.cJumlahOlah) >0 or SUM(B.cPlafon) >0 " +
                        " UNION " +
                        " select 4 as Level, A.IDDInas as IDDInas, A.IDProgram as IDProgram, A.IDkegiatan as IDKegiatan, A.sNama as Nama, SUM(B.cJumlahOlah) as JumlahInput, " +
                        " SUM(B.cPlafon) as Pagu FROM tKegiatan_A A INNER JOIN tANGGARANRekening_A B ON A.IDDInas = B.IDDInas AND A.iTahun = B.iTahun " +
                        " AND A.IDProgram= B.IDProgram and A.IDUrusan= B.IDUrusan AND A.btJenis= B.btJenis AND A.IDkegiatan = B.IDkegiatan " +
                        " INNER JOIN tKegiatan_A C On B.iTahun = C.iTahun and B.IDDInas = C.IDDInas and B.IDUrusan =C.IDUrusan " +
                        " AND B.IDProgram = C.IDProgram and B.IDKegiatan = C.IDKegiatan and " +
                        " B.btJenis = A.btJenis  WHERE B.iTahun =" + _pTahun.ToString() + " AND B.IIDRekening = " + pIDRekening.ToString().Trim() +
                        " GROUP BY A.IDDInas , A.IDProgram,A.IDKegiatan,A.sNama " +
                        " HAVING SUM(B.cJumlahOlah) >0 or SUM(B.cPlafon) >0 " +
                        " ORDER BY IDDInas, IDProgram,IDKegiatan ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new DashboardIII()
                                {

                                    Level = DataFormat.GetInteger(dr["Level"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Kode = GetKodeEx(DataFormat.GetInteger(dr["Level"]),
                                            DataFormat.GetInteger(dr["IDDInas"]),
                                            0,
                                            DataFormat.GetInteger(dr["IDProgram"]),
                                            DataFormat.GetInteger(dr["IDKegiatan"]),
                                            0),                                    
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDkegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),                                    
                                    Nama = DataFormat.GetString(dr["Nama"]).ToUpper(),
                                    Input = DataFormat.GetDecimal(dr["JumlahInput"]).ToRupiahInReport(),
                                    Pagu = DataFormat.GetDecimal(dr["Pagu"]).ToRupiahInReport(),
                                    Selisih = (DataFormat.GetDecimal(dr["Pagu"]) - DataFormat.GetDecimal(dr["JumlahInput"])).ToRupiahInReport()                                    
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
        private string GetKode(int Level, int idDInas, int idUrusan, int idProgram, int idKegiatan, long idRekening)
        {
            string sRet="";

            switch (Level){
                case 1:
                    sRet=idDInas.ToKodeDinas();
                    break;
                case 2:
                    sRet= idUrusan.ToKodeUrusan();
                break;
                case 3:
                sRet = idProgram.ToSimpleKodeProgram();
                    break;
                case 4 :
                    sRet=idKegiatan.ToSimpleKodeKegiatan();
                    break;
                case 5 :
                    sRet =idRekening.ToKodeRekening(m_ProfileRekening);
                    break;
            }
              return sRet;
        }
        private string GetKodeEx(int Level, int idDInas, int idUrusan, int idProgram, int idKegiatan, long idRekening)
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
                    sRet = idRekening.ToKodeRekening(m_ProfileRekening);
                    break;
            }
            return sRet;
        }
        public List<RekapPerJenis> GetRekapPerJenis(int _tahun)
        {
            List<RekapPerJenis> _lst = new List<RekapPerJenis>();
            try
            {
                SSQL = "";

                SSQL = "Select 0 as Level, 0 AS IDDinas,0 as IDUrusan, 0 as IDProgram,0 as IDkegiatan, 0 as IDRekening, 'KABUPATEN KETAPANG' AS Nama, 0 as btJenis, " +
                    " SUM(CASE WHEN B.IIDRekening /1000000=4 THEN B.cJumlahOlah ELSE 0 END)AS PENDAPATANINPUT, " +
                    " SUM(CASE WHEN B.IIDRekening /100000=51 THEN B.cJumlahOlah ELSE 0 END)AS BTLINPUT , " +
                    " SUM(CASE WHEN B.IIDRekening /100000=52 THEN B.cJumlahOlah ELSE 0 END)AS BLINPUT, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=521 THEN B.cJumlahOlah ELSE 0 END)AS BELANJAPEGAWAI, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=522 THEN B.cJumlahOlah ELSE 0 END)AS BELANJABARANGJASA, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=523 THEN B.cJumlahOlah ELSE 0 END)AS BELANJAMODAL, " +
                    " SUM(CASE WHEN B.IIDRekening /1000000=4 THEN B.cPlafon ELSE 0 END)AS PENDAPATANPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /100000=51 THEN B.cPlafon ELSE 0 END)AS BTLPAGU , " +
                    " SUM(CASE WHEN B.IIDRekening /100000=52 THEN B.cPlafon ELSE 0 END)AS BLPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=521 THEN B.cPlafon ELSE 0 END)AS BELANJAPEGAWAIPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=522 THEN B.cPlafon ELSE 0 END)AS BELANJABARANGJASAPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=523 THEN B.cPlafon ELSE 0 END)AS BELANJAMODALPAGU from mSKPD A " +
                    " INNER JOIN tANGGARANREKENING_A B ON A.ID = B.IDDInas " +
                    " WHERE B.iTahun = " + _tahun.ToString();
                    SSQL = SSQL + " UNION ALL ";

                
                    SSQL =SSQL +  "Select 1 as Level, A.ID AS IDDinas,0 as IDUrusan, 0 as IDProgram,0 as IDkegiatan, 0 as IDRekening, A.sNamaSKPD AS Nama, 0 as btJenis, " +
                    " SUM(CASE WHEN B.IIDRekening /1000000=4 THEN B.cJumlahOlah ELSE 0 END)AS PENDAPATANINPUT, " +
                    " SUM(CASE WHEN B.IIDRekening /100000=51 THEN B.cJumlahOlah ELSE 0 END)AS BTLINPUT , " +
                    " SUM(CASE WHEN B.IIDRekening /100000=52 THEN B.cJumlahOlah ELSE 0 END)AS BLINPUT, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=521 THEN B.cJumlahOlah ELSE 0 END)AS BELANJAPEGAWAI, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=522 THEN B.cJumlahOlah ELSE 0 END)AS BELANJABARANGJASA, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=523 THEN B.cJumlahOlah ELSE 0 END)AS BELANJAMODAL, " +
                    " SUM(CASE WHEN B.IIDRekening /1000000=4 THEN B.cPlafon ELSE 0 END)AS PENDAPATANPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /100000=51 THEN B.cPlafon ELSE 0 END)AS BTLPAGU , " +
                    " SUM(CASE WHEN B.IIDRekening /100000=52 THEN B.cPlafon ELSE 0 END)AS BLPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=521 THEN B.cPlafon ELSE 0 END)AS BELANJAPEGAWAIPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=522 THEN B.cPlafon ELSE 0 END)AS BELANJABARANGJASAPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=523 THEN B.cPlafon ELSE 0 END)AS BELANJAMODALPAGU from mSKPD A " +
                    " INNER JOIN tANGGARANREKENING_A B ON A.ID = B.IDDInas " +
                    " WHERE B.iTahun = " + _tahun.ToString() + " GROUP BY A.ID ,A.sNamaSKPD  ";

                    SSQL = SSQL + " UNION ALL ";
                

                SSQL = SSQL +
                " Select 2 as Level, B.IDDInas  AS IDDinas, A.ID as IDUrusan, 0  as IDProgram,0 as IDkegiatan, 0 as IDRekening, A.sNamaUrusan AS Nama, 0 as btJenis ," +
                " SUM(CASE WHEN B.IIDRekening /1000000=4 THEN B.cJumlahOlah ELSE 0 END)AS PENDAPATANINPUT, " +
                    " SUM(CASE WHEN B.IIDRekening /100000=51 THEN B.cJumlahOlah ELSE 0 END)AS BTLINPUT , " +
                    " SUM(CASE WHEN B.IIDRekening /100000=52 THEN B.cJumlahOlah ELSE 0 END)AS BLINPUT, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=521 THEN B.cJumlahOlah ELSE 0 END)AS BELANJAPEGAWAI, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=522 THEN B.cJumlahOlah ELSE 0 END)AS BELANJABARANGJASA, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=523 THEN B.cJumlahOlah ELSE 0 END)AS BELANJAMODAL, " +
                    " SUM(CASE WHEN B.IIDRekening /1000000=4 THEN B.cPlafon ELSE 0 END)AS PENDAPATANPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /100000=51 THEN B.cPlafon ELSE 0 END)AS BTLPAGU , " +
                    " SUM(CASE WHEN B.IIDRekening /100000=52 THEN B.cPlafon ELSE 0 END)AS BLPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=521 THEN B.cPlafon ELSE 0 END)AS BELANJAPEGAWAIPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=522 THEN B.cPlafon ELSE 0 END)AS BELANJABARANGJASAPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=523 THEN B.cPlafon ELSE 0 END)AS BELANJAMODALPAGU from mUrusan A " +
                    " INNER JOIN tANGGARANREKENING_A B ON A.ID = B.IDUrusan " +
                    " WHERE B.iTahun = " + _tahun.ToString() +
                    " GROUP BY B.IDDInas  , A.ID , A.sNamaUrusan ";

                SSQL = SSQL + "  UNION ALL " +
                " Select 3 as Level, B.IDDInas  AS IDDinas, A.IDUrusan as IDUrusan, A.IDProgram  as IDProgram,0 as IDkegiatan, 0 as IDRekening,  " +
                " A.sNamaProgram AS Nama, B.btJenis as btJenis," +
                " SUM(CASE WHEN B.IIDRekening /1000000=4 THEN B.cJumlahOlah ELSE 0 END)AS PENDAPATANINPUT, " +
                    " SUM(CASE WHEN B.IIDRekening /100000=51 THEN B.cJumlahOlah ELSE 0 END)AS BTLINPUT , " +
                    " SUM(CASE WHEN B.IIDRekening /100000=52 THEN B.cJumlahOlah ELSE 0 END)AS BLINPUT, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=521 THEN B.cJumlahOlah ELSE 0 END)AS BELANJAPEGAWAI, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=522 THEN B.cJumlahOlah ELSE 0 END)AS BELANJABARANGJASA, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=523 THEN B.cJumlahOlah ELSE 0 END)AS BELANJAMODAL, " +
                    " SUM(CASE WHEN B.IIDRekening /1000000=4 THEN B.cPlafon ELSE 0 END)AS PENDAPATANPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /100000=51 THEN B.cPlafon ELSE 0 END)AS BTLPAGU , " +
                    " SUM(CASE WHEN B.IIDRekening /100000=52 THEN B.cPlafon ELSE 0 END)AS BLPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=521 THEN B.cPlafon ELSE 0 END)AS BELANJAPEGAWAIPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=522 THEN B.cPlafon ELSE 0 END)AS BELANJABARANGJASAPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=523 THEN B.cPlafon ELSE 0 END)AS BELANJAMODALPAGU from tPrograms_A A " +
                    " INNER JOIN tANGGARANREKENING_A B ON A.iTahun= B.iTahun AND A.IDUrusan = B.IDurusan AND A.IDDInas=B.IDDInas AND A.IDProgram=B.IDProgram " +
                    " WHERE B.iTahun = " + _tahun.ToString() +
                    " GROUP BY B.IDDInas  , A.IDUrusan , A.IDProgram,A.sNamaProgram,b.btJenis ";
                SSQL = SSQL + "  UNION ALL " +
                " Select 4 as Level, B.IDDInas  AS IDDinas, A.IDUrusan as IDUrusan, A.IDProgram  as IDProgram,A.IDkegiatan as IDkegiatan, 0 as IDRekening,  " +
                " A.sNama AS Nama, B.btJenis as btJenis," +
                " SUM(CASE WHEN B.IIDRekening /1000000=4 THEN B.cJumlahOlah ELSE 0 END)AS PENDAPATANINPUT, " +
                    " SUM(CASE WHEN B.IIDRekening /100000=51 THEN B.cJumlahOlah ELSE 0 END)AS BTLINPUT , " +
                    " SUM(CASE WHEN B.IIDRekening /100000=52 THEN B.cJumlahOlah ELSE 0 END)AS BLINPUT, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=521 THEN B.cJumlahOlah ELSE 0 END)AS BELANJAPEGAWAI, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=522 THEN B.cJumlahOlah ELSE 0 END)AS BELANJABARANGJASA, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=523 THEN B.cJumlahOlah ELSE 0 END)AS BELANJAMODAL, " +
                    " SUM(CASE WHEN B.IIDRekening /1000000=4 THEN B.cPlafon ELSE 0 END)AS PENDAPATANPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /100000=51 THEN B.cPlafon ELSE 0 END)AS BTLPAGU , " +
                    " SUM(CASE WHEN B.IIDRekening /100000=52 THEN B.cPlafon ELSE 0 END)AS BLPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=521 THEN B.cPlafon ELSE 0 END)AS BELANJAPEGAWAIPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=522 THEN B.cPlafon ELSE 0 END)AS BELANJABARANGJASAPAGU, " +
                    " SUM(CASE WHEN B.IIDRekening /10000=523 THEN B.cPlafon ELSE 0 END)AS BELANJAMODALPAGU from tKegiatan_A A " +
                    " INNER JOIN tANGGARANREKENING_A B ON A.iTahun= B.iTahun AND A.IDUrusan = B.IDurusan AND A.IDDInas=B.IDDInas AND A.IDProgram=B.IDProgram and a.IDKegiatan=B.IDKegiatan " +
                    " WHERE B.iTahun = " + _tahun.ToString() +
                    " GROUP BY B.IDDInas  , A.IDUrusan , A.IDProgram ,A.IDkegiatan,A.sNama,b.btJenis " +
                    " ORDER BY IDDInas  , IDUrusan , IDProgram  ,IDkegiatan , IDRekening ";



                //SSQL = SSQL + "  UNION ALL " +
                //" Select 5 as Level, B.IDDInas  AS IDDinas, B.IDUrusan as IDUrusan, B.IDProgram  as IDProgram,B.IDkegiatan as IDkegiatan, A.IIDRekening as IDRekening,  " +
                //" A.sNamaRekening AS Nama,B.btJenis," +
                //" SUM(CASE WHEN B.IIDRekening /1000000=4 THEN cJumlahOlah ELSE 0 END)AS PENDAPATANINPUT, " +
                //" SUM(CASE WHEN B.IIDRekening /100000=51 THEN cJumlahOlah ELSE 0 END)AS BTLINPUT , " +
                //" SUM(CASE WHEN B.IIDRekening /100000=52 THEN cJumlahOlah ELSE 0 END)AS BLINPUT, " +
                //"   SUM(CASE WHEN B.IIDRekening /1000000=4 THEN B.cPlafon ELSE 0 END)AS PENDAPATANPAGU, " +
                //" SUM(CASE WHEN B.IIDRekening /100000=51 THEN B.cPlafon ELSE 0 END) AS BTLPAGU , " +
                //" SUM(CASE WHEN B.IIDRekening /100000=52 THEN B.cPlafon ELSE 0 END)AS BLPAGU  from mRekening  A " +
                //" INNER JOIN tANGGARANREKENING_A B ON A.IIDRekening= B.IIDRekening  " +
                //" WHERE B.iTahun = 2017 " +
                //" GROUP BY B.IDDInas  , B.IDUrusan , B.IDProgram  ,B.IDkegiatan , A.IIDRekening , A.sNamaRekening,B.btJenis  " +
             



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RekapPerJenis()
                                {

                                    Level = DataFormat.GetInteger(dr["Level"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Kode = GetKode(DataFormat.GetInteger(dr["Level"]),
                                            DataFormat.GetInteger(dr["IDDInas"]),
                                            DataFormat.GetInteger(dr["IDUrusan"]),
                                            DataFormat.GetInteger(dr["IDProgram"]),
                                            DataFormat.GetInteger(dr["IDKegiatan"] ),
                                            DataFormat.GetLong(dr["IDRekening"])),
                                    IDUrusan=DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram= DataFormat.GetInteger(dr["IDProgram"]),
                                    IDkegiatam=DataFormat.GetInteger(dr["IDKegiatan"]),
                                    BelanjaLangsung = DataFormat.GetDecimal(dr["BLINPUT"]),
                                    BelanjaPegawai = DataFormat.GetDecimal(dr["BELANJAPEGAWAI"]),
                                    BelanjaBarangJasa = DataFormat.GetDecimal(dr["BELANJABARANGJASA"]),
                                    BelanjaModal = DataFormat.GetDecimal(dr["BELANJAMODAL"]),
                                    BelanjaTidakLangsung = DataFormat.GetDecimal(dr["BTLINPUT"]),
                                    BelanjaLangsungPagu = DataFormat.GetDecimal(dr["BLPAGU"]),
                                    BelanjaPegawaiPagu = DataFormat.GetDecimal(dr["BELANJAPEGAWAIPAGU"]),
                                    BelanjaBarangJasaPagu = DataFormat.GetDecimal(dr["BELANJABARANGJASAPAGU"]),
                                    BelanjaModalPagu = DataFormat.GetDecimal(dr["BELANJAMODALPAGU"]),
                                    BelanjaTidakLangsungPagu = DataFormat.GetDecimal(dr["BTLPAGU"]),                                    
                                    Nama = DataFormat.GetString(dr["Nama"]).ToUpper()                                    //SelisihBelanja = DataFormat.GetDecimal(DataFormat.GetDecimal(dr["BTLPAGU"]) + DataFormat.GetDecimal(dr["BLPAGU"]) -
                                    //               DataFormat.GetDecimal(dr["BTLINPUT"]) - DataFormat.GetDecimal(dr["BLINPUT"])).ToRupiahInReport(),
                                            
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
        public List<RekapPerJenis> GetRekapPerJenisDinas(int _tahun,int _pDinas)
        {
            List<RekapPerJenis> _lst = new List<RekapPerJenis>();
            try
            {
                SSQL = "";

                
                SSQL = SSQL + "Select 1 as Level, A.ID AS IDDinas,0 as IDUrusan, 0 as IDProgram,0 as IDkegiatan, 0 as IDRekening, A.sNamaSKPD AS Nama, 0 as btJenis, " +
                " SUM(B.cPlafon)AS PAGU from mSKPD A " +
                " INNER JOIN tANGGARANREKENING_A B ON A.ID = B.IDDInas " +
                " WHERE B.iTahun = " + _tahun.ToString() + " AND B.IDDinas =" + _pDinas.ToString () + " GROUP BY A.ID ,A.sNamaSKPD  ";

                SSQL = SSQL + " UNION ALL ";


                SSQL = SSQL +
                " Select 2 as Level, B.IDDInas  AS IDDinas, A.ID as IDUrusan, 0  as IDProgram,0 as IDkegiatan, 0 as IDRekening, A.sNamaUrusan AS Nama, 0 as btJenis ," +
                    " SUM(B.cPlafon)AS PAGU from mUrusan A " +
                    " INNER JOIN tANGGARANREKENING_A B ON A.ID = B.IDUrusan " +
                    " WHERE B.iTahun = " + _tahun.ToString() + " AND B.IDDinas =" + _pDinas.ToString () +  
                    " GROUP BY B.IDDInas  , A.ID , A.sNamaUrusan ";

                SSQL = SSQL + "  UNION ALL " +
                " Select 3 as Level, B.IDDInas  AS IDDinas, A.IDUrusan as IDUrusan, A.IDProgram  as IDProgram,0 as IDkegiatan, 0 as IDRekening,  " +
                " A.sNamaProgram AS Nama, B.btJenis as btJenis," +
                " SUM(cPlafon)AS PAGU  from tPrograms_A A " +
                    " INNER JOIN tANGGARANREKENING_A B ON A.iTahun= B.iTahun AND A.IDUrusan = B.IDurusan AND A.IDDInas=B.IDDInas AND A.IDProgram=B.IDProgram  and A.btJenis= B.btJenis  " +
                    " WHERE B.iTahun = " + _tahun.ToString() + " AND B.IDDinas =" + _pDinas.ToString () + 
                    " GROUP BY B.IDDInas  , A.IDUrusan , A.IDProgram,A.sNamaProgram,b.btJenis ";
                SSQL = SSQL + "  UNION ALL " +
                " Select 4 as Level, B.IDDInas  AS IDDinas, A.IDUrusan as IDUrusan, A.IDProgram  as IDProgram,A.IDkegiatan as IDkegiatan, 0 as IDRekening,  " +
                " A.sNama AS Nama, B.btJenis as btJenis," +
                " SUM(b.cPlafon)AS PAGU from tKegiatan_A A " +
                    " INNER JOIN tANGGARANREKENING_A B ON A.iTahun= B.iTahun AND A.IDUrusan = B.IDurusan AND A.IDDInas=B.IDDInas AND A.IDProgram=B.IDProgram and a.IDKegiatan=B.IDKegiatan   and A.btJenis= B.btJenis  " +
                    " WHERE B.iTahun = " + _tahun.ToString() + " AND B.IDDinas =" + _pDinas.ToString () + 
                    " GROUP BY B.IDDInas  , A.IDUrusan , A.IDProgram ,A.IDkegiatan,A.sNama,b.btJenis " +
                    " ORDER BY IDDInas  , IDUrusan , IDProgram ,btJenis ,IDkegiatan , IDRekening ";

                //SSQL = SSQL + "  UNION ALL " +

                //    " Select 5 as Level, B.IDDInas  AS IDDinas, B.IDUrusan as IDUrusan, B.IDProgram  as IDProgram,B.IDkegiatan as IDkegiatan, A.IIDRekening as IDRekening,  " +
                //" A.sNamaRekening AS Nama,B.btJenis," +
                //" SUM(CASE WHEN B.IIDRekening /1000000=4 THEN cJumlahOlah ELSE 0 END)AS PENDAPATANINPUT, " +
                //" SUM(CASE WHEN B.IIDRekening /100000=51 THEN cJumlahOlah ELSE 0 END)AS BTLINPUT , " +
                //" SUM(CASE WHEN B.IIDRekening /100000=52 THEN cJumlahOlah ELSE 0 END)AS BLINPUT, " +
                //"   SUM(CASE WHEN B.IIDRekening /1000000=4 THEN B.cPlafon ELSE 0 END)AS PENDAPATANPAGU, " +
                //" SUM(CASE WHEN B.IIDRekening /100000=51 THEN B.cPlafon ELSE 0 END) AS BTLPAGU , " +
                //" SUM(CASE WHEN B.IIDRekening /100000=52 THEN B.cPlafon ELSE 0 END)AS BLPAGU  from mRekening  A " +
                //" INNER JOIN tANGGARANREKENING_A B ON A.IIDRekening= B.IIDRekening  " +
                //" WHERE B.iTahun = 2017 " +
                //" GROUP BY B.IDDInas  , B.IDUrusan , B.IDProgram  ,B.IDkegiatan , A.IIDRekening , A.sNamaRekening,B.btJenis  ";





                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RekapPerJenis()
                                {

                                    Level = DataFormat.GetInteger(dr["Level"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Kode = GetKode(DataFormat.GetInteger(dr["Level"]),
                                            DataFormat.GetInteger(dr["IDDInas"]),
                                            DataFormat.GetInteger(dr["IDUrusan"]),
                                            DataFormat.GetInteger(dr["IDProgram"]),
                                            DataFormat.GetInteger(dr["IDKegiatan"]),
                                            DataFormat.GetLong(dr["IDRekening"])),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDkegiatam = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    Pagu = DataFormat.GetDecimal(dr["PAGU"]).ToRupiahInReport(),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    Nama = DataFormat.GetString(dr["Nama"]).ToUpper()                                    //SelisihBelanja = DataFormat.GetDecimal(DataFormat.GetDecimal(dr["BTLPAGU"]) + DataFormat.GetDecimal(dr["BLPAGU"]) -
                                    //               DataFormat.GetDecimal(dr["BTLINPUT"]) - DataFormat.GetDecimal(dr["BLINPUT"])).ToRupiahInReport(),

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

        public List<RekapPerJenis> GetRekapPerJenisDinasDispenda(int _tahun, int _pDinas)
        {
            List<RekapPerJenis> _lst = new List<RekapPerJenis>();
            try
            {
                SSQL = "";


                SSQL = SSQL + "Select 1 as Level, A.ID AS IDDinas,0 as IDUrusan, 0 as IDProgram,0 as IDkegiatan, 0 as IDRekening, A.sNamaSKPD AS Nama, 0 as btJenis, " +
                " SUM(B.cJumlahMurni)AS PAGU ,SUM(B.cJumlahRKAP)AS PAGUABT  from mSKPD A " +
                " INNER JOIN tANGGARANREKENING_A B ON A.ID = B.IDDInas " +
                " WHERE B.iTahun = " + _tahun.ToString() + " AND B.IDDinas =" + _pDinas.ToString() + " GROUP BY A.ID ,A.sNamaSKPD  ";

                SSQL = SSQL + " UNION ALL ";


                SSQL = SSQL +
                " Select 2 as Level, B.IDDInas  AS IDDinas, A.ID as IDUrusan, 0  as IDProgram,0 as IDkegiatan, 0 as IDRekening, A.sNamaUrusan AS Nama, 0 as btJenis ," +
                    " SUM(B.cJumlahMurni)AS PAGU ,SUM(B.cJumlahRKAP) as PAGUABT from mUrusan A " +
                    " INNER JOIN tANGGARANREKENING_A B ON A.ID = B.IDUrusan " +
                    " WHERE B.iTahun = " + _tahun.ToString() + " AND B.IDDinas =" + _pDinas.ToString() +
                    " GROUP BY B.IDDInas  , A.ID , A.sNamaUrusan ";

                SSQL = SSQL + "  UNION ALL " +
                " Select 3 as Level, B.IDDInas  AS IDDinas, A.IDUrusan as IDUrusan, A.IDProgram  as IDProgram,0 as IDkegiatan, 0 as IDRekening,  " +
                " A.sNamaProgram AS Nama, B.btJenis as btJenis," +
                " SUM(B.cJumlahMurni)AS PAGU ,SUM(B.cJumlahRKAP) as PAGUABT from tPrograms_A A " +
                    " INNER JOIN tANGGARANREKENING_A B ON A.iTahun= B.iTahun AND A.IDUrusan = B.IDurusan AND A.IDDInas=B.IDDInas AND A.IDProgram=B.IDProgram  and A.btJenis= B.btJenis  " +
                    " WHERE B.iTahun = " + _tahun.ToString() + " AND B.IDDinas =" + _pDinas.ToString() +
                    " GROUP BY B.IDDInas  , A.IDUrusan , A.IDProgram,A.sNamaProgram,b.btJenis ";
                SSQL = SSQL + "  UNION ALL " +
                " Select 4 as Level, B.IDDInas  AS IDDinas, A.IDUrusan as IDUrusan, A.IDProgram  as IDProgram,A.IDkegiatan as IDkegiatan, 0 as IDRekening,  " +
                " A.sNama AS Nama, B.btJenis as btJenis," +
                " SUM(B.cJumlahMurni)AS PAGU ,SUM(B.cJumlahRKAP) as PAGUABT from tKegiatan_A A " +
                    " INNER JOIN tANGGARANREKENING_A B ON A.iTahun= B.iTahun AND A.IDUrusan = B.IDurusan AND A.IDDInas=B.IDDInas AND A.IDProgram=B.IDProgram and a.IDKegiatan=B.IDKegiatan   and A.btJenis= B.btJenis  " +
                    " WHERE B.iTahun = " + _tahun.ToString() + " AND B.IDDinas =" + _pDinas.ToString() +
                    " GROUP BY B.IDDInas  , A.IDUrusan , A.IDProgram ,A.IDkegiatan,A.sNama,b.btJenis " +
                    " ORDER BY IDDInas  , IDUrusan , IDProgram ,btJenis ,IDkegiatan , IDRekening ";

         


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RekapPerJenis()
                                {

                                    Level = DataFormat.GetInteger(dr["Level"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Kode = GetKode(DataFormat.GetInteger(dr["Level"]),
                                            DataFormat.GetInteger(dr["IDDInas"]),
                                            DataFormat.GetInteger(dr["IDUrusan"]),
                                            DataFormat.GetInteger(dr["IDProgram"]),
                                            DataFormat.GetInteger(dr["IDKegiatan"]),
                                            DataFormat.GetLong(dr["IDRekening"])),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDkegiatam = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    Pagu = DataFormat.GetDecimal(dr["PAGU"]).ToRupiahInReport(),
                                    PaguABT = DataFormat.GetDecimal(dr["PAGUABT"]).ToRupiahInReport(),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    Nama = DataFormat.GetString(dr["Nama"]).ToUpper()                                    //SelisihBelanja = DataFormat.GetDecimal(DataFormat.GetDecimal(dr["BTLPAGU"]) + DataFormat.GetDecimal(dr["BLPAGU"]) -
                                    //               DataFormat.GetDecimal(dr["BTLINPUT"]) - DataFormat.GetDecimal(dr["BLINPUT"])).ToRupiahInReport(),

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
    
    }
}
