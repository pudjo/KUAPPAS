using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Formatting;
using System.Data;
using DataAccess;
using DTO;

namespace BP
{
    public class rptRKALogic: BP 
    {

        int mprofile;
        public rptRKALogic(int _pTahun, int profile=2)
            : base(_pTahun,0,profile)
        {
            mprofile = profile;
            Tahun = _pTahun;
        }
        public List<RPTRKA> GetDataRKA1(int _iTahun, int _idDinas, int _pTahap, int? bPPKD = 0, List<SKPD> lstSKPD = null)
        {

            string strDinas = "(";
            SetProfileRekening(mprofile);
            if (lstSKPD == null)
            {
                strDinas = strDinas + _idDinas.ToString() + ")";
            }
            else
            {

                foreach (SKPD d in lstSKPD)
                {
                    strDinas = strDinas + d.ID.ToString() + ",";
                }
                strDinas = strDinas + "99)";

            }
            GetKolom(_pTahap);



            List<RPTRKA> lst = new List<RPTRKA>();
            try
            {

                string sWhere = "";

                sWhere = " AND  iTahun =" + _iTahun.ToString() + " AND IDDInas in  " + strDinas;
                SSQL = " Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah," +
                    " SUM (" + _namaKolom2.Trim() + ") as Jumlah ,1 as Level, 1 as Nourut,0 as LevelUraian,'' as Label from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN1.ToString() + " ) " +
                    " WHERE B.btRoot = 1 " + sWhere + " and btJenis= 1 and bPPKD=" + bPPKD.ToString() + " Group by B.IIDRekening,B.sNamaRekening ";


                SSQL = SSQL + "  UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah," +
                    " SUM (" + _namaKolom2.Trim() + ") as Jumlah,2 as Level , 1 as Nourut ,0 as LevelUraian ,'' as Label  from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")" +
                    " WHEre B.btRoot = 2   " + sWhere + " and btJenis= 1 and bPPKD=" + bPPKD.ToString() + " Group by B.IIDRekening,B.sNamaRekening ";



                SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah, " +
                    "SUM (" + _namaKolom2.Trim() + ") as Jumlah,3 as Level , 1 as Nourut, 0 as LevelUraian ,'' as Label from tAnggaranRekening_A A inner join mRekening B  " +
                    " ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")  WHEre B.btRoot = 3  " + sWhere + " and btJenis= 1 and bPPKD=" + bPPKD.ToString() + " Group by B.IIDRekening,B.sNamaRekening  ";

                //SSQL = SSQL + " UNION ALL Select 0 as IIDRekening ,'JUMLAH PENDAPATAN' as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah," +
                //    " SUM (" + _namaKolom2.Trim() + ") as Jumlah ,1 as Level, 2 as Nourut,0 as LevelUraian,'' as Label from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN1.ToString() + " ) " +
                //    " WHERE B.btRoot = 1 " + sWhere + " and btJenis= 1 ";

                SSQL = SSQL + " UNION ALL  Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah," +
                    " SUM (" + _namaKolom2.Trim() + ") as Jumlah ,1 as Level, 3 as Nourut,0 as LevelUraian,'' as Label from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN1.ToString() + " ) " +
                    "  WHERE B.btRoot = 1 " + sWhere + " and A.btJenis in (2,3) and bPPKD=" + bPPKD.ToString() + " Group by B.IIDRekening,B.sNamaRekening ";


                SSQL = SSQL + "  UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah," +
                    " SUM (" + _namaKolom2.Trim() + ") as Jumlah,2 as Level , 3 as Nourut ,0 as LevelUraian ,'' as Label  from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")" +
                    " WHEre B.btRoot = 2   " + sWhere + " and btJenis in (2,3) and bPPKD=" + bPPKD.ToString() + "  Group by B.IIDRekening,B.sNamaRekening ";



                SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah, " +
                    "SUM (" + _namaKolom2.Trim() + ") as Jumlah,3 as Level , 3 as Nourut, 0 as LevelUraian ,'' as Label from tAnggaranRekening_A A inner join mRekening B  " +
                    " ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ") " +
                    "    WHEre B.btRoot = 3  " + sWhere + " and A.btJenis in (2,3) and A.bPPKD=" + bPPKD.ToString() + " Group by B.IIDRekening,B.sNamaRekening  ";

                //SSQL  = SSQL + " UNION ALL Select 0 as IIDRekening ,'JUMLAH BELANJA' as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah," +
                //    " SUM (" + _namaKolom2.Trim() + ") as Jumlah ,1 as Level, 4 as Nourut,0 as LevelUraian,'' as Label from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN1.ToString() + " ) " +
                //    " WHERE B.btRoot = 1 " + sWhere + " and btJenis  in (2,3)  ";

                SSQL = SSQL + " UNION ALL  Select 0 as IIDRekening ,'SURPLUS/(DEFISIT)' as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga," +
                    "(SELECT isnull(SUM(" + _namaKolom1.Trim() + "),0) from tAnggaranRekening_A WHERE 1>0 " + sWhere + " AND btJenis = 1 and bPPKD=" + bPPKD.ToString() + " ) - (SELECT isnull(SUM(" + _namaKolom1.Trim() + "),0) from tAnggaranRekening_A WHERE 1>0  " + sWhere + " AND  btJenis in (2,3) and bPPKD=" + bPPKD.ToString() + ")  as JumlahOlah," +
                   "(SELECT isnull( SUM(" + _namaKolom2.Trim() + "),0) from tAnggaranRekening_A WHERE 1>0 " + sWhere + " AND btJenis = 1 and bPPKD=" + bPPKD.ToString() + " )- (SELECT isnull( SUM(" + _namaKolom2.Trim() + "),0) from tAnggaranRekening_A WHERE 1>0  " + sWhere + " AND  btJenis in (2,3) and bPPKD=" + bPPKD.ToString() + ")  as Jumlah , " +
                   " 1 as Level, 5 as Nourut,0 as LevelUraian,'' as Label ";



                SSQL = SSQL + " order by NoUrut,B.IIDRekening,Level ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new RPTRKA()
                               {

                                   KodeRekening = DataFormat.GetSingle(dr["Level"]) > 5 ? "" : DataFormat.GetLong(dr["IIDRekening"]).ToKodeRekening(m_ProfileRekening),
                                   Nama = DataFormat.GetString(dr["Nama"]),
                                   Vol = DataFormat.GetString(dr["Volume"]),
                                   Satuan = DataFormat.GetString(dr["Satuan"]),
                                   Harga = DataFormat.GetString(DataFormat.GetDecimal(dr["JumlahOlah"])),
                                   JumlahMurni = DataFormat.GetDecimal(dr["JumlahOlah"]).ToRupiahInReport(),
                                   VolMurni = "0",
                                   HargaMurni = DataFormat.GetString(DataFormat.GetDecimal(dr["Jumlah"])),
                                   Jumlah = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
                                   Level = DataFormat.GetSingle(dr["Level"]),
                                   NoUrut = DataFormat.GetSingle(dr["NoUrut"]),
                                   LevelUraian = DataFormat.GetSingle(dr["LevelUraian"]),
                                   Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahOlah"])).ToRupiahInReport(),
                                   Persen = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]),DataFormat.GetDecimal(dr["JumlahOlah"]) )


                               }).ToList();
                    }
                }
                if (bPPKD == 1)
                {
                    List<RPTRKA> lstPembiayaan = GetDataRKA1Pembiayaan(_iTahun, _idDinas, _pTahap, bPPKD);
                    if (lstPembiayaan != null)
                    {
                        foreach (RPTRKA r in lstPembiayaan)
                        {
                            lst.Add(r);
                        }
                    }


                }
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }
        }
        //public List<RPTRKA> GetDataRKA1(int _iTahun, int _idDinas, int _pTahap, int? bPPKD = 0, List<SKPD> lstSKPD = null)
        //{

        //    string strDinas = "(";

        //    if (lstSKPD == null)
        //    {
        //        strDinas = strDinas + _idDinas.ToString() + ")";
        //    }
        //    else
        //    {

        //        foreach (SKPD d in lstSKPD)
        //        {
        //            strDinas = strDinas + d.ID.ToString() + ",";
        //        }
        //        strDinas = strDinas + "99)";

        //    }
        //    GetKolom(_pTahap);



        //    List<RPTRKA> lst = new List<RPTRKA>();
        //    try
        //    {

        //        string sWhere = "";

        //        sWhere = " AND  iTahun =" + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString();
        //        SSQL = " Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah," +
        //            " SUM (" + _namaKolom2.Trim() + ") as Jumlah ,1 as Level, 1 as Nourut,0 as LevelUraian,'' as Label from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN1.ToString() + " ) " +
        //            " WHERE B.btRoot = 1 " + sWhere + " and btJenis= 1 and bPPKD=" + bPPKD.ToString() + " Group by B.IIDRekening,B.sNamaRekening ";


        //        SSQL = SSQL + "  UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah," +
        //            " SUM (" + _namaKolom2.Trim() + ") as Jumlah,2 as Level , 1 as Nourut ,0 as LevelUraian ,'' as Label  from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")" +
        //            " WHEre B.btRoot = 2   " + sWhere + " and btJenis= 1 and bPPKD=" + bPPKD.ToString() + " Group by B.IIDRekening,B.sNamaRekening ";



        //        SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah, " +
        //            "SUM (" + _namaKolom2.Trim() + ") as Jumlah,3 as Level , 1 as Nourut, 0 as LevelUraian ,'' as Label from tAnggaranRekening_A A inner join mRekening B  " +
        //            " ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")  WHEre B.btRoot = 3  " + sWhere + " and btJenis= 1 and bPPKD=" + bPPKD.ToString() + " Group by B.IIDRekening,B.sNamaRekening  ";

        //        //SSQL = SSQL + " UNION ALL Select 0 as IIDRekening ,'JUMLAH PENDAPATAN' as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah," +
        //        //    " SUM (" + _namaKolom2.Trim() + ") as Jumlah ,1 as Level, 2 as Nourut,0 as LevelUraian,'' as Label from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN1.ToString() + " ) " +
        //        //    " WHERE B.btRoot = 1 " + sWhere + " and btJenis= 1 ";

        //        SSQL = SSQL + " UNION ALL  Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah," +
        //            " SUM (" + _namaKolom2.Trim() + ") as Jumlah ,1 as Level, 3 as Nourut,0 as LevelUraian,'' as Label from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN1.ToString() + " ) " +
        //            "  WHERE B.btRoot = 1 " + sWhere + " and A.btJenis in (2,3) and bPPKD=" + bPPKD.ToString() + " Group by B.IIDRekening,B.sNamaRekening ";


        //        SSQL = SSQL + "  UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah," +
        //            " SUM (" + _namaKolom2.Trim() + ") as Jumlah,2 as Level , 3 as Nourut ,0 as LevelUraian ,'' as Label  from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")" +
        //            " WHEre B.btRoot = 2   " + sWhere + " and btJenis in (2,3) and bPPKD=" + bPPKD.ToString() + "  Group by B.IIDRekening,B.sNamaRekening ";



        //        SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah, " +
        //            "SUM (" + _namaKolom2.Trim() + ") as Jumlah,3 as Level , 3 as Nourut, 0 as LevelUraian ,'' as Label from tAnggaranRekening_A A inner join mRekening B  " +
        //            " ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ") " +
        //            "    WHEre B.btRoot = 3  " + sWhere + " and A.btJenis in (2,3) and A.bPPKD=" + bPPKD.ToString() + " Group by B.IIDRekening,B.sNamaRekening  ";

        //        //SSQL  = SSQL + " UNION ALL Select 0 as IIDRekening ,'JUMLAH BELANJA' as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah," +
        //        //    " SUM (" + _namaKolom2.Trim() + ") as Jumlah ,1 as Level, 4 as Nourut,0 as LevelUraian,'' as Label from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN1.ToString() + " ) " +
        //        //    " WHERE B.btRoot = 1 " + sWhere + " and btJenis  in (2,3)  ";

        //        SSQL = SSQL + " UNION ALL  Select 0 as IIDRekening ,'SURPLUS/(DEFISIT)' as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga," +
        //            "(SELECT isnull(SUM(" + _namaKolom1.Trim() + "),0) from tAnggaranRekening_A WHERE 1>0 " + sWhere + " AND btJenis = 1 and bPPKD=" + bPPKD.ToString() + " ) - (SELECT isnull(SUM(" + _namaKolom1.Trim() + "),0) from tAnggaranRekening_A WHERE 1>0  " + sWhere + " AND  btJenis in (2,3) and bPPKD=" + bPPKD.ToString() + ")  as JumlahOlah," +
        //           "(SELECT isnull( SUM(" + _namaKolom2.Trim() + "),0) from tAnggaranRekening_A WHERE 1>0 " + sWhere + " AND btJenis = 1 and bPPKD=" + bPPKD.ToString() + " )- (SELECT isnull( SUM(" + _namaKolom2.Trim() + "),0) from tAnggaranRekening_A WHERE 1>0  " + sWhere + " AND  btJenis in (2,3) and bPPKD=" + bPPKD.ToString() + ")  as Jumlah , " +
        //           " 1 as Level, 5 as Nourut,0 as LevelUraian,'' as Label ";



        //        SSQL = SSQL + " order by NoUrut,B.IIDRekening,Level ";

        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                lst = (from DataRow dr in dt.Rows
        //                       select new RPTRKA()
        //                       {

        //                           KodeRekening = DataFormat.GetSingle(dr["Level"]) > 5 ? "" : DataFormat.GetLong(dr["IIDRekening"]).ToKodeRekening(m_ProfileRekening),
        //                           Nama = DataFormat.GetString(dr["Nama"]),
        //                           Vol = DataFormat.GetString(dr["Volume"]),
        //                           Satuan = DataFormat.GetString(dr["Satuan"]),
        //                           Harga = DataFormat.GetString(DataFormat.GetDecimal(dr["JumlahOlah"])),
        //                           JumlahMurni = DataFormat.GetDecimal(dr["JumlahOlah"]).ToRupiahInReport(),
        //                           VolMurni = "0",
        //                           HargaMurni = DataFormat.GetString(DataFormat.GetDecimal(dr["Jumlah"])),
        //                           Jumlah = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
        //                           Level = DataFormat.GetSingle(dr["Level"]),
        //                           NoUrut = DataFormat.GetSingle(dr["NoUrut"]),
        //                           LevelUraian = DataFormat.GetSingle(dr["LevelUraian"]),
        //                           Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahOlah"])).ToRupiahInReport(),
        //                           Persen = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]), DataFormat.GetDecimal(dr["JumlahOlah"]))


        //                       }).ToList();
        //            }
        //        }
        //        if (bPPKD == 1)
        //        {
        //            List<RPTRKA> lstPembiayaan = GetDataRKA1Pembiayaan(_iTahun, _idDinas, _pTahap, bPPKD);
        //            if (lstPembiayaan != null)
        //            {
        //                foreach (RPTRKA r in lstPembiayaan)
        //                {
        //                    lst.Add(r);
        //                }
        //            }


        //        }
        //        return lst;
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message;
        //        return lst;
        //    }
        //}

        public List<RPTRKA> GetDataRKA1Pembiayaan(int _iTahun, int _idDinas, int _pTahap, int? bPPKD = 0)
        {


            GetKolom(_pTahap);


            List<RPTRKA> lst = new List<RPTRKA>();
            try
            {

                string sWhere = "";

                sWhere = " AND  iTahun =" + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString();
                SSQL = " Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah," +
                    " SUM (" + _namaKolom2.Trim() + ") as Jumlah ,1 as Level, 1 as Nourut,0 as LevelUraian,'' as Label from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN1.ToString() + " ) " +
                    " WHERE B.btRoot = 1 " + sWhere + " and btJenis= 4 and bPPKD=" + bPPKD.ToString() + " Group by B.IIDRekening,B.sNamaRekening ";


                SSQL = SSQL + "  UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah," +
                    " SUM (" + _namaKolom2.Trim() + ") as Jumlah,2 as Level , 1 as Nourut ,0 as LevelUraian ,'' as Label  from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")" +
                    " WHEre B.btRoot = 2   " + sWhere + " and btJenis= 4 and bPPKD=" + bPPKD.ToString() + " Group by B.IIDRekening,B.sNamaRekening ";



                SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah, " +
                    "SUM (" + _namaKolom2.Trim() + ") as Jumlah,3 as Level , 1 as Nourut, 0 as LevelUraian ,'' as Label from tAnggaranRekening_A A inner join mRekening B  " +
                    " ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")  WHEre B.btRoot = 3  " + sWhere + " and btJenis= 4 and bPPKD=" + bPPKD.ToString() + " Group by B.IIDRekening,B.sNamaRekening  ";

                //SSQL = SSQL + " UNION ALL Select 0 as IIDRekening ,'JUMLAH PENDAPATAN' as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah," +
                //    " SUM (" + _namaKolom2.Trim() + ") as Jumlah ,1 as Level, 2 as Nourut,0 as LevelUraian,'' as Label from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN1.ToString() + " ) " +
                //    " WHERE B.btRoot = 1 " + sWhere + " and btJenis= 1 ";

                SSQL = SSQL + " UNION ALL  Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah," +
                    " SUM (" + _namaKolom2.Trim() + ") as Jumlah ,1 as Level, 3 as Nourut,0 as LevelUraian,'' as Label from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN1.ToString() + " ) " +
                    " WHERE B.btRoot = 1 " + sWhere + " and btJenis =5 and bPPKD=" + bPPKD.ToString() + " Group by B.IIDRekening,B.sNamaRekening ";


                SSQL = SSQL + "  UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah," +
                    " SUM (" + _namaKolom2.Trim() + ") as Jumlah,2 as Level , 3 as Nourut ,0 as LevelUraian ,'' as Label  from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")" +
                    " WHEre B.btRoot = 2   " + sWhere + " and btJenis =5 and bPPKD=" + bPPKD.ToString() + "  Group by B.IIDRekening,B.sNamaRekening ";



                SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah, " +
                    "SUM (" + _namaKolom2.Trim() + ") as Jumlah,3 as Level , 3 as Nourut, 0 as LevelUraian ,'' as Label from tAnggaranRekening_A A inner join mRekening B  " +
                    " ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")  WHEre B.btRoot = 3  " + sWhere + " and btJenis =5 and bPPKD=" + bPPKD.ToString() + " Group by B.IIDRekening,B.sNamaRekening  ";

                //SSQL  = SSQL + " UNION ALL Select 0 as IIDRekening ,'JUMLAH BELANJA' as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A." + _namaKolom1.Trim() + ") as JumlahOlah," +
                //    " SUM (" + _namaKolom2.Trim() + ") as Jumlah ,1 as Level, 4 as Nourut,0 as LevelUraian,'' as Label from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN1.ToString() + " ) " +
                //    " WHERE B.btRoot = 1 " + sWhere + " and btJenis  in (2,3)  ";

                SSQL = SSQL + " UNION ALL  Select 0 as IIDRekening ,'Pembiayaan Netto)' as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga," +
                    "(SELECT isnull(SUM(" + _namaKolom1.Trim() + "),0) from tAnggaranRekening_A WHERE 1>0 " + sWhere + " AND btJenis = 4 and bPPKD=" + bPPKD.ToString() + " ) - (SELECT isnull(SUM(" + _namaKolom1.Trim() + "),0) from tAnggaranRekening_A WHERE 1>0  " + sWhere + " AND  btJenis =5 and bPPKD=" + bPPKD.ToString() + ")  as JumlahOlah," +
                   "(SELECT isnull( SUM(" + _namaKolom2.Trim() + "),0) from tAnggaranRekening_A WHERE 1>0 " + sWhere + " AND btJenis = 4 and bPPKD=" + bPPKD.ToString() + " )- (SELECT isnull( SUM(" + _namaKolom2.Trim() + "),0) from tAnggaranRekening_A WHERE 1>0  " + sWhere + " AND  btJenis =5 and bPPKD=" + bPPKD.ToString() + ")  as Jumlah , " +
                   " 1 as Level, 5 as Nourut,0 as LevelUraian,'' as Label ";



                SSQL = SSQL + " order by NoUrut,B.IIDRekening,Level ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new RPTRKA()
                               {

                                   KodeRekening = DataFormat.GetSingle(dr["Level"]) > 5 ? "" : DataFormat.GetLong(dr["IIDRekening"]).ToKodeRekening(m_ProfileRekening),
                                   Nama = DataFormat.GetString(dr["Nama"]),
                                   Vol = DataFormat.GetString(dr["Volume"]),
                                   Satuan = DataFormat.GetString(dr["Satuan"]),
                                   Harga = DataFormat.GetString(DataFormat.GetDecimal(dr["JumlahOlah"])),
                                   JumlahMurni = DataFormat.GetDecimal(dr["JumlahOlah"]).ToRupiahInReport(),
                                   VolMurni = "0",
                                   HargaMurni = DataFormat.GetString(DataFormat.GetDecimal(dr["Jumlah"])),
                                   Jumlah = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
                                   Level = DataFormat.GetSingle(dr["Level"]),
                                   NoUrut = DataFormat.GetSingle(dr["NoUrut"]),
                                   LevelUraian = DataFormat.GetSingle(dr["LevelUraian"]),
                                   Selisih = (DataFormat.GetDecimal(dr["JumlahOlah"]) - DataFormat.GetDecimal(dr["Jumlah"])).ToRupiahInReport(),
                                   Persen = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]),DataFormat.GetDecimal(dr["JumlahOlah"]) )


                               }).ToList();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }
        }

        public List<RPTRKA> GetDataRKA1ABT(int _iTahun, int _idDinas)
        {

            List<RPTRKA> lst = new List<RPTRKA>();
            try
            {

                string sWhere = "";

                sWhere = " AND  iTahun =" + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString();

                SSQL = " Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A.cJumlahOlah) as JumlahOlah," +
                        " SUM (cJumlahMurni) as Jumlah ,1 as Level, 0 as Nourut,0 as LevelUraian,'' as Label from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ") " +
                        " WHERE B.btRoot = 1 " + sWhere + " Group by B.IIDRekening,B.sNamaRekening ";
                SSQL = SSQL + "  UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A.cJumlahOlah) as JumlahOlah," +
                        " SUM (cJumlahMurni) as Jumlah,2 as Level , 0 as Nourut ,0 as LevelUraian ,'' as Label  from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")" +
                        " WHEre B.btRoot = 2   " + sWhere + " Group by B.IIDRekening,B.sNamaRekening ";


                SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A.cJumlahOlah) as JumlahOlah, " +
                        "SUM (cJumlahMurni) as Jumlah,3 as Level , 0 as Nourut, 0 as LevelUraian ,'' as Label from tAnggaranRekening_A A inner join mRekening B  " +
                        " ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")  WHEre B.btRoot = 3  " + sWhere + " Group by B.IIDRekening,B.sNamaRekening  ";


                SSQL = SSQL + " order by IIDRekening,Level, Nourut ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new RPTRKA()
                               {

                                   KodeRekening = DataFormat.GetSingle(dr["Level"]) > 5 ? "" : DataFormat.GetLong(dr["IIDRekening"]).ToKodeRekening(m_ProfileRekening),
                                   Nama = DataFormat.GetString(dr["Nama"]),
                                   Vol = DataFormat.GetString(dr["Volume"]),
                                   Satuan = DataFormat.GetString(dr["Satuan"]),
                                   Harga = DataFormat.GetDecimal(dr["Harga"]).ToRupiahInReport(),
                                   Jumlah = DataFormat.GetDecimal(dr["JumlahOlah"]).ToRupiahInReport(),
                                   VolMurni = "0",
                                   HargaMurni = "0",
                                   JumlahMurni = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
                                   Level = DataFormat.GetSingle(dr["Level"]),
                                   NoUrut = DataFormat.GetSingle(dr["NoUrut"]),
                                   LevelUraian = DataFormat.GetSingle(dr["LevelUraian"]),
                                   Selisih = (DataFormat.GetDecimal (dr["JumlahOlah"])-DataFormat.GetDecimal(dr["Jumlah"])).ToRupiahInReport(),
                                   Persen = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]),DataFormat.GetDecimal(dr["JumlahOlah"])  )
                               }).ToList();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }

        }
     

        public decimal JumlahMurni(int _iTahun, int _idDinas, int _idUrusan, int idProgram, int idKegiatan, int iJenis,int _iPPKD, int pTahap)
        {
            try
            {
                decimal d = 0L;

                SSQL = "SELECT SUM (cJumlahMurni) as Jumlah from tANggaranRekening_A where iTahun = " + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString() +
                            " AND IDurusan =" + _idUrusan.ToString() + " AND IDProgram =" + idProgram.ToString() + " AND IDKegiatan =" + idKegiatan.ToString() +
                            " AND isnull(bPPKD,0) =" + _iPPKD.ToString() + " AND btJenis = " + iJenis.ToString();


                
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                //Nama = DataFormat.MakeSpace( DataFormat.GetSingle(dr["LevelUraian"])) +  DataFormat.GetString(dr["Nama"]),
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr= dt.Rows[0];

                        d= DataFormat.GetDecimal(dr["Jumlah"]);
                    }
                }
                return d;

            }
            catch (Exception ex)
            {

                return 0;
            }
            
        }

        public decimal JumlahPerubahan(int _iTahun, int _idDinas, int _idUrusan, int idProgram, int idKegiatan, int iJenis,int _iPPKD, int pTahap)
        {
            try
            {
                decimal d = 0L;

                SSQL = "SELECT SUM (cJumlahRKAP) as Jumlah from tANggaranRekening_A where iTahun = " + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString() +
                            " AND IDurusan =" + _idUrusan.ToString() + " AND IDProgram =" + idProgram.ToString() + " AND IDKegiatan =" + idKegiatan.ToString() +
                            " AND isnull(bPPKD,0) =" + _iPPKD.ToString() + " AND btJenis = " + iJenis.ToString();


                
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                //Nama = DataFormat.MakeSpace( DataFormat.GetSingle(dr["LevelUraian"])) +  DataFormat.GetString(dr["Nama"]),
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr= dt.Rows[0];

                        d= DataFormat.GetDecimal(dr["Jumlah"]);
                    }
                }
                return d;

            }
            catch (Exception ex)
            {

                return 0;
            }
            
        }
        public List<RPTRKA> GetDataGabungan(int _iTahun, List<SKPD> lstSKPD, int _idUrusan, int idProgram, int idKegiatan, int iJenis,int _iPPKD, int pTahap )

        {

           
            string strDinas = "(";
            foreach (SKPD d in lstSKPD)
            {
                strDinas = strDinas + d.ID.ToString() + ",";
            }
            strDinas = strDinas + "99)";

            
            List<RPTRKA> lst = new List<RPTRKA>();
            try
            {
                GetKolom(pTahap);


                string sWhere = "";
                if (iJenis == 0)
                {
                    sWhere = " AND iTahun =" + _iTahun.ToString() + " AND IDDInas in  " + strDinas;
                }
                else
                {
                    sWhere = " AND  iTahun =" + _iTahun.ToString() + " AND IDDInas in  " + strDinas + " AND IDProgram=" + idProgram.ToString() +
                            " AND IDKegiatan=" + idKegiatan.ToString() + " AND btJenis=" + iJenis.ToString() + "  AND isnull(bPPKD,0)= " + _iPPKD.ToString();
                }

                

                SSQL = " Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, 0 as VolumeMurni, '' as SatuanMurni, 0 as HargaMurni," +
                    " SUM (" + _namaKolom2 + ") as Jumlah, SUM(" + _namaKolom1 + ") as JumlahMurni,1 as Level, 0 as Nourut,0 as LevelUraian,'' as Label, sum(A.cJumlahYAD) as YAD , 0 as idlokasi  from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ") " +
                    " WHERE B.btRoot = 1 " + sWhere + " Group by B.IIDRekening,B.sNamaRekening ";
                SSQL = SSQL + "  UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, 0 as VolumeMurni, '' as SatuanMurni, 0 as HargaMurni," +
                      " SUM (" + _namaKolom2 + ") as Jumlah,SUM(" + _namaKolom1 + ") as JumlahMurni,2 as Level , 0 as Nourut ,0 as LevelUraian,'' as Label,sum(A.cJumlahYAD) as YAD, 0 as idlokasi  from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")" +
                      " WHEre B.btRoot = 2   " + sWhere + " Group by B.IIDRekening,B.sNamaRekening ";

                SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, 0 as VolumeMurni, '' as SatuanMurni, 0 as HargaMurni," +
                      "SUM (" + _namaKolom2 + ") as Jumlah,SUM(" + _namaKolom1 + ") as JumlahMurni,3 as Level , 0 as Nourut, 0 as LevelUraian,'' as Label,sum(A.cJumlahYAD) as YAD, 0 as idlokasi    from tAnggaranRekening_A A inner join mRekening B  " +
                      " ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")  WHEre B.btRoot = 3  " + sWhere + " Group by B.IIDRekening,B.sNamaRekening  ";

                if (iJenis > 0)
                {
                    SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga,  0 as VolumeMurni, '' as SatuanMurni, 0 as HargaMurni," +
                                " SUM (" + _namaKolom2 + ") as Jumlah,SUM(" + _namaKolom1 + ") as JumlahMurni, 4 as Level , 0 as Nourut, 0 as LevelUraian ,'' as Label,sum(A.cJumlahYAD) as YAD, 0 as idlokasi     from tAnggaranRekening_A A inner join mRekening B  " +
                                " ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN4.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN4.ToString() + ") WHEre B.btRoot = 4  " + sWhere + " Group by B.IIDRekening,B.sNamaRekening ";

                    SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, 0 as VolumeMurni, '' as SatuanMurni, 0 as HargaMurni, " +
                               " SUM(" + _namaKolom2 + ") as Jumlah,SUM(" + _namaKolom1 + ") as JumlahMurni,5 as Level , 0 as Nourut,0 as LevelUraian ,'' as Label,SUM(A.cJumlahYAD) as YAD , 0 as idlokasi  from tAnggaranRekening_A A " +
                               " inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ") WHEre B.btRoot = 5  " + sWhere + " Group by B.IIDRekening,B.sNamaRekening ";


                    ////   " Group by B.IIDRekening,B.sNamaRekening ";

                    //sWhere = " AND  B.iTahun =" + _iTahun.ToString() + " AND B.IDDInas = " + _idDinas.ToString() + " AND B.IDProgram=" + idProgram.ToString() +
                    //            " AND B.IDKegiatan=" + idKegiatan.ToString() + " AND B.Jenis=" + iJenis.ToString() + " AND isnull(B.bPPKD,0)= " + _iPPKD.ToString();
                    ////cHargaMurni= cHarga, VolMurni=VolOlah,JumlahMurni= Jumlah, sUraianMurni= sUraianAPBD,sLabelMurni
                    //SSQL = SSQL + " UNION ALL Select B.IIDRekening,B."+ _namaKolomUraian2 + " as Nama,B.btUrut as Urut, B." + _namaKolomvolume2 + "  as Volume, B.sSatuan as Satuan, B." + _namaKolomharga2 + "   as Harga, B." + _namaKolomvolume1 + "  as VolumeMurni, B.sSatuan as SatuanMurni, B." + _namaKolomharga1 + " as HargaMurni,  " +
                    //               " " + _namaKolomjumlahuraian2 + "  as Jumlah, B." + _namaKolomjumlahuraian1 + "  as JumlahMurni, 5 +level as Level , btUrut as Nourut, Level as LevelUraian,sLabel as Label,B.cJumlahYAD AS YAD,  idlokasi   from tANggaranUraian_A B   INNER JOIN tANggaranRekening_A D " +
                    //               " ON B.iTahun = D.iTahun AND B.IDDInas = D.IDDInas AND B.IDUrusan = D.IDUrusan AND B.IDProgram = D.IDProgram AND B.IDKegiatan = D.IDKegiatan AND B.IIDRekening = D.IIDRekening  and B.bPPKD= D.bPPKD WHERE  1> 0 " + sWhere + "  ";
                }


                SSQL = SSQL + " order by IIDRekening, Nourut ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                //Nama = DataFormat.MakeSpace( DataFormat.GetSingle(dr["LevelUraian"])) +  DataFormat.GetString(dr["Nama"]),
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new RPTRKA()
                               {

                                   KodeRekening = DataFormat.GetSingle(dr["Level"]) > 5 ? "" : DataFormat.GetLong(dr["IIDRekening"]).ToKodeRekening(m_ProfileRekening),
                                   Nama = DataFormat.GetString(dr["Nama"]),
                                   Vol = DataFormat.GetDecimal(dr["Volume"])== 0 ? "" : DataFormat.GetDecimal(dr["Volume"]).ToRupiahInReportNoSen(),
                                   Satuan = dr["Satuan"]==null?"":dr["Satuan"].ToString(),
                                   SatuanMurni = DataFormat.GetDecimal(dr["JumlahMurni"]) ==0?  "" : dr["SatuanMurni"].ToString(),
                                   //SatuanMurni = dr["Satuan"] == null ? "" : dr["Satuan"].ToString(),
                                    //DataFormat.GetString(dr["Satuan"]),
                                   Harga = DataFormat.GetDecimal(dr["Harga"])==0?"":DataFormat.GetDecimal(dr["Harga"]).ToRupiahInReport(),
                                   Jumlah = DataFormat.GetDecimal(dr["Jumlah"]) >0 ?DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport():"",
                                   
                                   VolMurni = DataFormat.GetDecimal(dr["JumlahMurni"]) ==0? "":DataFormat.GetDecimal(dr["VolumeMurni"]).ToRupiahInReportNoSen(),
                                   
                                   HargaMurni = DataFormat.GetDecimal(dr["HargaMurni"])==0?"":DataFormat.GetDecimal(dr["HargaMurni"]).ToRupiahInReport(),
                                   JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]) >0? DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport():"",
                                   Level = DataFormat.GetSingle(dr["Level"]),
                                   NoUrut = DataFormat.GetSingle(dr["NoUrut"]),
                                   LevelUraian = DataFormat.GetSingle(dr["LevelUraian"]),
                                   Label =DataFormat.GetInteger (dr["idlokasi"])>0?"-": DataFormat.GetString(dr["Label"]),
                                   Root = DataFormat.GetSingle(dr["Level"]),
                                   YAD = DataFormat.GetDecimal(dr["YAD"]).ToRupiahInReport(),
                                   Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                   Persen = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]),DataFormat.GetDecimal(dr["JumlahMurni"]))

                               }).ToList();
                    }
                }
                // Kepentingan cetak agar pada halaman terakhir dan tidak ada datadetail,maka tidak cetak header.

                int idx=0;
                int count = lst.Count;
                
                for  (idx = 0 ; idx<count;idx++)
                {
                    RPTRKA r = lst[idx];
                    if (r.Level == 5)
                    {
                        long idRekening;
                        idRekening = DataFormat.GetLong(r.KodeRekening.Replace(".", ""));
                        if (idRekening == 5520602)
                        {
                            idRekening = 5520602;
                        }
                        foreach (SKPD skpd in lstSKPD){
                          
                            List<RPTRKA> lstUraian = GetUraianGabungan(_iTahun, skpd, _idUrusan, idProgram, idKegiatan, iJenis, _iPPKD, pTahap, idRekening);
                            if (lstUraian != null)
                            {                               
                                foreach (RPTRKA inserted in lstUraian)
                                {
                                    lst.Insert(idx+1, inserted);
                                    idx++;
                                    count++;
                                }                               

                            }


                        }
                    }

                    //idx++;
                    


                }
                RPTRKA blankRKA = new RPTRKA();

                blankRKA.KodeRekening = "";
                blankRKA.Nama = "";
                blankRKA.Vol = "";
                blankRKA.Satuan = "";
                blankRKA.Harga = "0";
                blankRKA.Jumlah = "";
                blankRKA.VolMurni = "0";
                blankRKA.HargaMurni = "0";
                blankRKA.JumlahMurni = "0";
                blankRKA.Level = 1;
                blankRKA.NoUrut = 1;
                blankRKA.LevelUraian = 1;
                blankRKA.Label = "";


              //  lst.Add(blankRKA);
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }
        }
        
        private List<RPTRKA> GetSisa(List<RPTRKA> lst, int index)
        {
            List<RPTRKA> lstReturn = new List<RPTRKA>();
            for (int idx = 0; idx <= lst.Count;idx++ )
            {
                if (idx >= index)
                {
                    lstReturn.Add(lst[idx]);

                }
            }
            return lstReturn;
           

        }

        public List<RPTRKA> GetUraianGabungan(int _iTahun, SKPD oSKPD, int _idUrusan, int idProgram, int idKegiatan, int iJenis, int _iPPKD, int pTahap, long pIIDREKENING)

        {


            List<RPTRKA> lst = new List<RPTRKA>();
            try
            {
                GetKolom(pTahap);


                string sWhere = "";
                if (iJenis == 0)
                {
                    sWhere = " AND IIDRekening = " + pIIDREKENING.ToString() + " AND iTahun =" + _iTahun.ToString() + " AND IDDInas =  " + oSKPD.ID.ToString();
                }
                else
                {
                    sWhere = "  AND IIDRekening = " + pIIDREKENING.ToString() + " AND  iTahun =" + _iTahun.ToString() + " AND IDDInas =  " + oSKPD.ID.ToString() + " AND IDProgram=" + idProgram.ToString() +
                            " AND IDKegiatan=" + idKegiatan.ToString() + " AND btJenis=" + iJenis.ToString() + "  AND isnull(bPPKD,0)= " + _iPPKD.ToString();
                }


                SSQL = " Select IIDRekening,'" + oSKPD.Nama + "' as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, 0 as VolumeMurni, '' as SatuanMurni, 0 as HargaMurni, " +
                               " " + _namaKolom2 + " as Jumlah," + _namaKolom1 + " as JumlahMurni,6 as Level , 0 as Nourut,0 as LevelUraian ,'' as Label,A.cJumlahYAD as YAD , 0 as idlokasi  from tAnggaranRekening_A A " +
                               " WHEre 1 >0  " + sWhere + " ";



                sWhere = " AND  B.iTahun =" + _iTahun.ToString() + " AND B.IDDInas =  " + oSKPD.ID.ToString() + " AND B.IDProgram=" + idProgram.ToString() +
                                " AND B.IDKegiatan=" + idKegiatan.ToString() + " AND B.Jenis=" + iJenis.ToString() + " AND isnull(B.bPPKD,0)= " + _iPPKD.ToString();
                //cHargaMurni= cHarga, VolMurni=VolOlah,JumlahMurni= Jumlah, sUraianMurni= sUraianAPBD,sLabelMurni
                SSQL = SSQL + " UNION ALL Select B.IIDRekening,B." + _namaKolomUraian2 + " as Nama,B.btUrut as Urut, B." + _namaKolomvolume2 + "  as Volume, B.sSatuan as Satuan, B." + _namaKolomharga2 + "   as Harga, B." + _namaKolomvolume1 + "  as VolumeMurni, B.sSatuan as SatuanMurni, B." + _namaKolomharga1 + " as HargaMurni,  " +
                               " " + _namaKolomjumlahuraian2 + "  as Jumlah, B." + _namaKolomjumlahuraian1 + "  as JumlahMurni, 5 +level as Level , btUrut as Nourut, Level +1 as LevelUraian,sLabel as Label,B.cJumlahYAD AS YAD,  idlokasi   from tANggaranUraian_A B   INNER JOIN tANggaranRekening_A D " +
                               " ON B.iTahun = D.iTahun AND B.IDDInas = D.IDDInas AND B.IDUrusan = D.IDUrusan AND B.IDProgram = D.IDProgram AND B.IDKegiatan = D.IDKegiatan AND B.IIDRekening = D.IIDRekening  and B.bPPKD= D.bPPKD WHERE  1> 0 " + sWhere + " AND B.IIDRekening = " + pIIDREKENING.ToString() + "  ";
                SSQL = SSQL + " order by IIDRekening, Nourut ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                //Nama = DataFormat.MakeSpace( DataFormat.GetSingle(dr["LevelUraian"])) +  DataFormat.GetString(dr["Nama"]),
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new RPTRKA()
                               {

                                   KodeRekening = DataFormat.GetSingle(dr["Level"]) > 5 ? "" : DataFormat.GetLong(dr["IIDRekening"]).ToKodeRekening(m_ProfileRekening),
                                   Nama = DataFormat.GetString(dr["Nama"]),
                                   Vol = DataFormat.GetDecimal(dr["Volume"]) == 0 ? "" : DataFormat.GetDecimal(dr["Volume"]).ToRupiahInReportNoSen(),
                                   Satuan = dr["Satuan"] == null ? "" : dr["Satuan"].ToString(),
                                   SatuanMurni = DataFormat.GetDecimal(dr["JumlahMurni"]) == 0 ? "" : dr["SatuanMurni"].ToString(),
                                   //SatuanMurni = dr["Satuan"] == null ? "" : dr["Satuan"].ToString(),
                                   //DataFormat.GetString(dr["Satuan"]),
                                   Harga = DataFormat.GetDecimal(dr["Harga"]) == 0 ? "" : DataFormat.GetDecimal(dr["Harga"]).ToRupiahInReport(),
                                   Jumlah = DataFormat.GetDecimal(dr["Jumlah"]) > 0 ? DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport() : "",

                                   VolMurni = DataFormat.GetDecimal(dr["JumlahMurni"]) == 0 ? "" : DataFormat.GetDecimal(dr["VolumeMurni"]).ToRupiahInReportNoSen(),

                                   HargaMurni = DataFormat.GetDecimal(dr["HargaMurni"]) == 0 ? "" : DataFormat.GetDecimal(dr["HargaMurni"]).ToRupiahInReport(),
                                   JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]) > 0 ? DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport() : "",
                                   Level = DataFormat.GetSingle(dr["Level"]),
                                   NoUrut = DataFormat.GetSingle(dr["NoUrut"]),
                                   LevelUraian = DataFormat.GetSingle(dr["LevelUraian"]),
                                   Label = DataFormat.GetInteger(dr["idlokasi"]) > 0 ? "-" : DataFormat.GetString(dr["Label"]),
                                   Root = DataFormat.GetSingle(dr["Level"]),
                                   YAD = DataFormat.GetDecimal(dr["YAD"]).ToRupiahInReport(),
                                   Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                   Persen = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]),DataFormat.GetDecimal(dr["JumlahMurni"]) )

                               }).ToList();
                    }
                }


                return lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;


            }
         }

        public List<ListSubKegiatanRPT> GetListSubKegiatan(int _iTahun, int _idDinas, int _idUrusan, int idProgram, int idKegiatan, int iJenis, int _iPPKD, int pTahap, long pIDSubKegiatan = 0)
        {

            int maxRoot = 5;
            List<ListSubKegiatanRPT> lst = new List<ListSubKegiatanRPT>();
            GetKolom(pTahap);
            try
            {
                   string sWhere = "";

                   sWhere = " AND  TS.iTahun =" + _iTahun.ToString() + " AND TS.IDDInas = " + _idDinas.ToString() + " AND TS.IDProgram=" + idProgram.ToString() +
                            " AND TS.IDKegiatan=" + idKegiatan.ToString() ;// +"  AND isnull(bPPKD,0)= " + _iPPKD.ToString();


                   SSQL = " Select tS.keterangan,  TS.IDSUbKegiatan, TS.Nama, TS.Lokasi,TS.SumberPendanaan,TS.Keluaran, TS.outcome,TS.Mulai,TS.Akhir, SUM(A." + _namaKolom1 + ") AS JumlahMurni,SUM(A." + _namaKolom2 + ") AS Jumlah from tSubKegiatan TS " +
                        " INNER JOIN tANggaranRekening_A A on TS.iTahun = A.iTahun AND TS.IDDInas= A.IDDInas AND TS.IDSUBKEGIATAN= A.IDSUBkegiatan " +
                        " WHERE 1>0  " + sWhere  +
                        " GROUP BY TS.IDSUbKegiatan, tS.keterangan,TS.Nama, TS.Lokasi,TS.SumberPendanaan,TS.Keluaran, TS.outcome,TS.Mulai,TS.Akhir " +  
                        " Order BY TS.IDSUbKegiatan ";

                 DataTable dt = new DataTable();
                 dt = _dbHelper.ExecuteDataTable(SSQL);
                //Nama = DataFormat.MakeSpace( DataFormat.GetSingle(dr["LevelUraian"])) +  DataFormat.GetString(dr["Nama"]),
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new ListSubKegiatanRPT()
                               {
                                   IDSubKegiatan = DataFormat.GetLong (dr["IDSubKegiatan"]),
                                   Nama = DataFormat.GetString(dr["Nama"]),
                                   Lokasi = DataFormat.GetString(dr["Lokasi"]),
                                   SumberPendanaan = DataFormat.GetString(dr["SumberPendanaan"]),
                                   Keluaran = DataFormat.GetString(dr["Keluaran"]),
                                   Mulai =DataFormat.GetString(dr["Mulai"]),
                                   Akhir =DataFormat.GetString(dr["Akhir"]),
                                   Jumlah = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
                                   JumlahMurni = DataFormat.GetDecimal(dr["JumlahMUrni"]).ToRupiahInReport(),

                                   KodeSub = DataFormat.GetLong(dr["IDSubKegiatan"]).ToString ().Insert(8,".").Insert(5,".").Insert (3,".").Insert (1,"."),
                                  Keterangan =DataFormat.GetString(dr["Keterangan"]),
                                   Selisih=(DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMUrni"])).ToRupiahInReport(),
                                   Persen = GetPresentase(DataFormat.GetDecimal(dr["JumlahMUrni"]), DataFormat.GetDecimal(dr["Jumlah"]))
                               }).ToList();
                    }
                }
                
                return lst;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }
        }
        public List<RPTRKA> GetData90(int _iTahun, int _idDinas, int _idUrusan, int idProgram, int idKegiatan, int iJenis, int _iPPKD, int pTahap, long pIDSubKegiatan = 0)
        {

            int maxRoot = 5;
            List<RPTRKA> lst = new List<RPTRKA>();
            try
            {
                GetKolom(pTahap);


                string sWhere = "";
                if (iJenis == 0)
                {
                    sWhere = " AND iTahun =" + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString();
                }
                else
                {
                    sWhere = " AND  iTahun =" + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString() + " AND IDProgram=" + idProgram.ToString() +
                            " AND IDKegiatan=" + idKegiatan.ToString() + " AND btJenis=" + iJenis.ToString() + "  AND isnull(bPPKD,0)= " + _iPPKD.ToString();
                      //      " AND IDSUBKegiatan=" + pIDSubKegiatan.ToString();
                }

                if (Tahun < 2021)
                {

                    SSQL = " Select A.IDSUbKegiatan,B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, 0 as VolumeMurni, '' as SatuanMurni, 0 as HargaMurni," +
                        " SUM (" + _namaKolom2 + ") as Jumlah, SUM(" + _namaKolom1 + ") as JumlahMurni,1 as Level, 0 as Nourut,0 as LevelUraian,'' as Label, sum(A.cJumlahYAD) as YAD , 0 as idlokasi  from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ") " +
                        " WHERE B.btRoot = 1 " + sWhere + " Group by B.IIDRekening,B.sNamaRekening ";
                    SSQL = SSQL + "  UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, 0 as VolumeMurni, '' as SatuanMurni, 0 as HargaMurni," +
                          " SUM (" + _namaKolom2 + ") as Jumlah,SUM(" + _namaKolom1 + ") as JumlahMurni,2 as Level , 0 as Nourut ,0 as LevelUraian,'' as Label,sum(A.cJumlahYAD) as YAD, 0 as idlokasi  from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")" +
                          " WHEre B.btRoot = 2   " + sWhere + " Group by A.IDSUbKegiatan,B.IIDRekening,B.sNamaRekening ";

                    SSQL = SSQL + " UNION ALL Select A.IDSUbKegiatan, B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, 0 as VolumeMurni, '' as SatuanMurni, 0 as HargaMurni," +
                          "SUM (" + _namaKolom2 + ") as Jumlah,SUM(" + _namaKolom1 + ") as JumlahMurni,3 as Level , 0 as Nourut, 0 as LevelUraian,'' as Label,sum(A.cJumlahYAD) as YAD, 0 as idlokasi    from tAnggaranRekening_A A inner join mRekening B  " +
                          " ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")  WHEre B.btRoot = 3  " + sWhere + " Group by A.IDSUbKegiatan,B.IIDRekening,B.sNamaRekening  ";

                    if (iJenis > 0)
                    {
                        SSQL = SSQL + " UNION ALL Select A.IDSUbKegiatan, B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga,  0 as VolumeMurni, '' as SatuanMurni, 0 as HargaMurni," +
                                    " SUM (" + _namaKolom2 + ") as Jumlah,SUM(" + _namaKolom1 + ") as JumlahMurni, 4 as Level , 0 as Nourut, 0 as LevelUraian ,'' as Label,sum(A.cJumlahYAD) as YAD, 0 as idlokasi     from tAnggaranRekening_A A inner join mRekening B  " +
                                    " ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN4.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN4.ToString() + ") WHEre B.btRoot = 4  " + sWhere + " Group by A.IDSUbKegiatan,B.IIDRekening,B.sNamaRekening ";

                         SSQL = SSQL + " UNION ALL Select A.IDSUbKegiatan,B.IIDRekening,B." + _namaKolomUraian2 + " as Nama,B.btUrut as Urut, B." + _namaKolomvolume2 + "  as Volume, B.sSatuan as Satuan, B." + _namaKolomharga2 + "   as Harga, B." + _namaKolomvolume1 + "  as VolumeMurni, B.sSatuan as SatuanMurni, B." + _namaKolomharga1 + " as HargaMurni,  " +
                                       " " + _namaKolomjumlahuraian2 + "  as Jumlah, B." + _namaKolomjumlahuraian1 + "  as JumlahMurni, 5 +level as Level , btUrut as Nourut, Level as LevelUraian,sLabel as Label,B.cJumlahYAD AS YAD,  idlokasi   from tANggaranUraian_A B   INNER JOIN tANggaranRekening_A D " +
                                       " ON B.iTahun = D.iTahun AND B.IDDInas = D.IDDInas AND B.IDUrusan = D.IDUrusan AND B.IDProgram = D.IDProgram AND B.IDKegiatan = D.IDKegiatan AND B.IIDRekening = D.IIDRekening  and B.bPPKD= D.bPPKD WHERE  1> 0 " + sWhere + "  ";
                    }
                }
                else
                {

                    maxRoot = 6;
                    SSQL = " Select A.IDSUbKegiatan, B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, 0 as VolumeMurni, '' as SatuanMurni, 0 as HargaMurni," +
                        " SUM (" + _namaKolom2 + ") as Jumlah, SUM(" + _namaKolom1 + ") as JumlahMurni,1 as Level, 0 as Nourut,0 as LevelUraian,'' as Label, sum(A.cJumlahYAD) as YAD , 0 as idlokasi ,0 as PPN from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ") " +
                        " WHERE B.btRoot = 1 " + sWhere + " Group by A.IDSUbKegiatan,B.IIDRekening,B.sNamaRekening ";
                    SSQL = SSQL + "  UNION ALL Select A.IDSUbKegiatan,B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, 0 as VolumeMurni, '' as SatuanMurni, 0 as HargaMurni," +
                          " SUM (" + _namaKolom2 + ") as Jumlah,SUM(" + _namaKolom1 + ") as JumlahMurni,2 as Level , 0 as Nourut ,0 as LevelUraian,'' as Label,sum(A.cJumlahYAD) as YAD, 0 as idlokasi  ,0 as PPN from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")" +
                          " WHEre B.btRoot = 2   " + sWhere + " Group by A.IDSUbKegiatan, B.IIDRekening,B.sNamaRekening ";

                    SSQL = SSQL + " UNION ALL Select A.IDSUbKegiatan,B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, 0 as VolumeMurni, '' as SatuanMurni, 0 as HargaMurni," +
                          "SUM (" + _namaKolom2 + ") as Jumlah,SUM(" + _namaKolom1 + ") as JumlahMurni,3 as Level , 0 as Nourut, 0 as LevelUraian,'' as Label,sum(A.cJumlahYAD) as YAD, 0 as idlokasi    ,0 as PPN from tAnggaranRekening_A A inner join mRekening B  " +
                          " ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")  WHEre B.btRoot = 3  " + sWhere + " Group by A.IDSUbKegiatan, B.IIDRekening,B.sNamaRekening  ";

                    if (iJenis > 0)
                    {
                        SSQL = SSQL + " UNION ALL Select A.IDSUbKegiatan,B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga,  0 as VolumeMurni, '' as SatuanMurni, 0 as HargaMurni," +
                                    " SUM (" + _namaKolom2 + ") as Jumlah,SUM(" + _namaKolom1 + ") as JumlahMurni, 4 as Level , 0 as Nourut, 0 as LevelUraian ,'' as Label,sum(A.cJumlahYAD) as YAD, 0 as idlokasi ,0 as PPN    from tAnggaranRekening_A A inner join mRekening B  " +
                                    " ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN4.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN4.ToString() + ") WHEre B.btRoot = 4  " + sWhere + " Group by A.IDSUbKegiatan,B.IIDRekening,B.sNamaRekening ";

                        SSQL = SSQL + " UNION ALL Select A.IDSUbKegiatan,B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, 0 as VolumeMurni, '' as SatuanMurni, 0 as HargaMurni, " +
                                   " SUM (" + _namaKolom2 + ") as Jumlah,SUM(" + _namaKolom1 + ") as JumlahMurni,5 as Level , 0 as Nourut,0 as LevelUraian ,'' as Label,SUM(A.cJumlahYAD) as YAD , 0 as idlokasi ,0 as PPN from tAnggaranRekening_A A " +
                                   " inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ") WHEre B.btRoot = 5  " + sWhere + " Group by A.IDSUbKegiatan, B.IIDRekening,B.sNamaRekening ";

                        SSQL = SSQL + " UNION ALL Select A.IDSUbKegiatan,B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, 0 as VolumeMurni, '' as SatuanMurni, 0 as HargaMurni, " +
                                   " " + _namaKolom2 + " as Jumlah," + _namaKolom1 + " as JumlahMurni,6 as Level , 0 as Nourut,0 as LevelUraian ,'' as Label,A.cJumlahYAD as YAD , 0 as idlokasi ,0 as PPN from tAnggaranRekening_A A " +
                                   " inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN6.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN6.ToString() + ") WHEre B.btRoot = 6  " + sWhere;

                        //   " Group by B.IIDRekening,B.sNamaRekening ";

                        sWhere = " AND  B.iTahun =" + _iTahun.ToString() + " AND B.IDDInas = " + _idDinas.ToString() + " AND B.IDProgram=" + idProgram.ToString() +
                                    " AND B.IDKegiatan=" + idKegiatan.ToString() + " AND B.Jenis=" + iJenis.ToString() + " AND isnull(B.bPPKD,0)= " + _iPPKD.ToString();
                        //cHargaMurni= cHarga, VolMurni=VolOlah,JumlahMurni= Jumlah, sUraianMurni= sUraianAPBD,sLabelMurni
                        SSQL = SSQL + " UNION ALL Select B.IDSUbKegiatan,B.IIDRekening,B." + _namaKolomUraian2 + " as Nama,B.btUrut as Urut, B." + _namaKolomvolume2 + "  as Volume, B.sSatuan as Satuan, B." + _namaKolomharga2 + "   as Harga, B." + _namaKolomvolume1 + "  as VolumeMurni, B.sSatuan as SatuanMurni, B." + _namaKolomharga1 + " as HargaMurni,  " +
                                       " " + _namaKolomjumlahuraian2 + "  as Jumlah, B." + _namaKolomjumlahuraian1 + "  as JumlahMurni, 6 +level as Level , btUrut as Nourut, Level as LevelUraian,sLabel as Label,B.cJumlahYAD AS YAD,  idlokasi, B.PPNRKA as PPN   from tANggaranUraian_A B   INNER JOIN tANggaranRekening_A D " +
                                       " ON B.iTahun = D.iTahun AND B.IDDInas = D.IDDInas AND B.IDUrusan = D.IDUrusan AND B.IDProgram = D.IDProgram AND B.IDKegiatan = D.IDKegiatan AND B.IDSubKegiatan = D.IDSubKegiatan  AND B.IIDRekening = D.IIDRekening  and B.bPPKD= D.bPPKD WHERE  1> 0 " + sWhere + "  ";
                   }

                }

                SSQL = SSQL + " order by IDSubKegiatan,IIDRekening, Nourut ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                //Nama = DataFormat.MakeSpace( DataFormat.GetSingle(dr["LevelUraian"])) +  DataFormat.GetString(dr["Nama"]),
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new RPTRKA()
                               {
                                   IDSUbKegiatan = DataFormat.GetLong (dr["IDSubKegiatan"]),
                                   KodeRekening = DataFormat.GetSingle(dr["Level"]) > maxRoot ? "" : DataFormat.GetLong(dr["IIDRekening"]).ToKodeRekening(m_ProfileRekening),
                                   Nama = DataFormat.GetString(dr["Nama"]),
                                   Vol = DataFormat.GetDecimal(dr["Volume"]) == 0 ? "" : DataFormat.GetDecimal(dr["Volume"]).ToRupiahInReportNoSen(),
                                   Satuan = dr["Satuan"] == null ? "" : dr["Satuan"].ToString(),
                                   SatuanMurni = DataFormat.GetDecimal(dr["JumlahMurni"]) == 0 ? "" : dr["SatuanMurni"].ToString(),
                                   //SatuanMurni = dr["Satuan"] == null ? "" : dr["Satuan"].ToString(),
                                   //DataFormat.GetString(dr["Satuan"]),
                                   Harga = DataFormat.GetDecimal(dr["Harga"]) == 0 ? "" : DataFormat.GetDecimal(dr["Harga"]).ToRupiahInReport(),
                                   Jumlah = DataFormat.GetDecimal(dr["Jumlah"]) > 0 ? DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport() : "",

                                   VolMurni = DataFormat.GetDecimal(dr["JumlahMurni"]) == 0 ? "" : DataFormat.GetDecimal(dr["VolumeMurni"]).ToRupiahInReportNoSen(),
                                   PPN = DataFormat.GetDecimal(dr["PPN"]) == 0 ? "" : DataFormat.GetDecimal(dr["PPN"]).ToRupiahInReport(),
                                   HargaMurni = DataFormat.GetDecimal(dr["HargaMurni"]) == 0 ? "" : DataFormat.GetDecimal(dr["HargaMurni"]).ToRupiahInReport(),
                                   JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]) > 0 ? DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport() : "",
                                   Level = DataFormat.GetSingle(dr["Level"]),
                                   NoUrut = DataFormat.GetSingle(dr["NoUrut"]),
                                   LevelUraian = DataFormat.GetSingle(dr["LevelUraian"]),
                                   Label = DataFormat.GetInteger(dr["idlokasi"]) > 0 ? "-" : DataFormat.GetString(dr["Label"]),
                                   Root = DataFormat.GetSingle(dr["Level"]),
                                   YAD = DataFormat.GetDecimal(dr["YAD"]).ToRupiahInReport(),
                                   Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                   Persen = GetPresentase(DataFormat.GetDecimal(dr["JumlahMurni"]), DataFormat.GetDecimal(dr["Jumlah"])),
                                   DJumlah = DataFormat.GetDecimal(dr["Jumlah"]),// > 0 ? DataFormat.GetDecimal(dr["Jumlah"])
                               }).ToList();
                    }
                }
                // Kepentingan cetak agar pada halaman terakhir dan tidak ada datadetail,maka tidak cetak header.
                RPTRKA blankRKA = new RPTRKA();

                blankRKA.KodeRekening = "";
                blankRKA.Nama = "";
                blankRKA.Vol = "";
                blankRKA.Satuan = "";
                blankRKA.Harga = "0";
                blankRKA.Jumlah = "";
                blankRKA.VolMurni = "0";
                blankRKA.HargaMurni = "0";
                blankRKA.JumlahMurni = "0";
                blankRKA.Level = 1;
                blankRKA.NoUrut = 1;
                blankRKA.LevelUraian = 1;
                blankRKA.Label = "";

                //SSQL = "SELECT  IDSUBKEgiatan ,SUM (" + _namaKolom2 + ") as Jumlah,SUM(" + _namaKolom1 + ") as JumlahMurni  from tAnggaranRekening_A   WHERE 1>0 ";
                // if (iJenis == 0)
                //{
                //    sWhere = " AND iTahun =" + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString();
                //}
                //else
                //{
                //    sWhere = " AND  iTahun =" + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString() + " AND IDProgram=" + idProgram.ToString() +
                //            " AND IDKegiatan=" + idKegiatan.ToString() + " AND btJenis=" + iJenis.ToString() + "  AND isnull(bPPKD,0)= " + _iPPKD.ToString();
                //      //      " AND IDSUBKegiatan=" + pIDSubKegiatan.ToString();
                //}

                //SSQL = SSQL + sWhere + " GROUP BY TANGGARANREKENING_A.IDSUBKEGIATAN";

                //List<RPTRKA> lstJumlah = new List<RPTRKA>();
                //DataTable dtj = new DataTable();
                //dtj = _dbHelper.ExecuteDataTable(SSQL);
                ////Nama = DataFormat.MakeSpace( DataFormat.GetSingle(dr["LevelUraian"])) +  DataFormat.GetString(dr["Nama"]),
                //if (dtj != null)
                //{
                //    if (dtj.Rows.Count > 0)
                //    {
                //        lstJumlah = (from DataRow dr in dt.Rows
                //               select new RPTRKA()
                //               {
                //                   IDSUbKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                //                   Jumlah = DataFormat.GetDecimal(dr["Jumlah"]) > 0 ? DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport() : "",
                //                   JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]) > 0 ? DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport() : "",
                                   
                //               }).ToList();
                //    }
                //}
                //for (int idx = 0; idx < lst.Count; idx++)
                //{

                //    for (int idxj = 0; idxj < lstJumlah.Count; idxj++)
                //    {
                //        if (lst[idx].IDSUbKegiatan == lstJumlah[idxj].IDSUbKegiatan)
                //        {

                //            lst[idx].Persen = lstJumlah[idxj].Jumlah;
                //        }

                //    }
                //}


                    //  lst.Add(blankRKA);
                    return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }
        }
        public List<RPTRKA> GetData(int _iTahun, int _idDinas, int _idUrusan, int idProgram, int idKegiatan, int iJenis, int _iPPKD, int pTahap)
        {
            SetProfileRekening(mprofile);

            List<RPTRKA> lst = new List<RPTRKA>();
            try
            {
                GetKolom(pTahap);


                string sWhere = "";
                if (iJenis == 0)
                {
                    sWhere = " AND iTahun =" + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString();
                }
                else
                {
                    sWhere = " AND  iTahun =" + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString() + " AND IDProgram=" + idProgram.ToString() +
                            " AND IDKegiatan=" + idKegiatan.ToString() + "  AND IDsubKegiatan=0 AND btJenis=" + iJenis.ToString() + "  AND isnull(bPPKD,0)= " + _iPPKD.ToString();
                }



                SSQL = " Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, 0 as VolumeMurni, '' as SatuanMurni, 0 as HargaMurni," +
                    " SUM (" + _namaKolom2 + ") as Jumlah, SUM(" + _namaKolom1 + ") as JumlahMurni,1 as Level, 0 as Nourut,0 as LevelUraian,'' as Label, sum(A.cJumlahYAD) as YAD , 0 as idlokasi  from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ") " +
                    " WHERE B.btRoot = 1 " + sWhere + " Group by B.IIDRekening,B.sNamaRekening ";
                SSQL = SSQL + "  UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, 0 as VolumeMurni, '' as SatuanMurni, 0 as HargaMurni," +
                      " SUM (" + _namaKolom2 + ") as Jumlah,SUM(" + _namaKolom1 + ") as JumlahMurni,2 as Level , 0 as Nourut ,0 as LevelUraian,'' as Label,sum(A.cJumlahYAD) as YAD, 0 as idlokasi  from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")" +
                      " WHEre B.btRoot = 2   " + sWhere + " Group by B.IIDRekening,B.sNamaRekening ";

                SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, 0 as VolumeMurni, '' as SatuanMurni, 0 as HargaMurni," +
                      "SUM (" + _namaKolom2 + ") as Jumlah,SUM(" + _namaKolom1 + ") as JumlahMurni,3 as Level , 0 as Nourut, 0 as LevelUraian,'' as Label,sum(A.cJumlahYAD) as YAD, 0 as idlokasi    from tAnggaranRekening_A A inner join mRekening B  " +
                      " ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")  WHEre B.btRoot = 3  " + sWhere + " Group by B.IIDRekening,B.sNamaRekening  ";

                if (iJenis > 0)
                {
                    SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga,  0 as VolumeMurni, '' as SatuanMurni, 0 as HargaMurni," +
                                " SUM (" + _namaKolom2 + ") as Jumlah,SUM(" + _namaKolom1 + ") as JumlahMurni, 4 as Level , 0 as Nourut, 0 as LevelUraian ,'' as Label,sum(A.cJumlahYAD) as YAD, 0 as idlokasi     from tAnggaranRekening_A A inner join mRekening B  " +
                                " ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN4.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN4.ToString() + ") WHEre B.btRoot = 4  " + sWhere + " Group by B.IIDRekening,B.sNamaRekening ";

                    SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, 0 as VolumeMurni, '' as SatuanMurni, 0 as HargaMurni, " +
                               " " + _namaKolom2 + " as Jumlah," + _namaKolom1 + " as JumlahMurni,5 as Level , 0 as Nourut,0 as LevelUraian ,'' as Label,A.cJumlahYAD as YAD , 0 as idlokasi  from tAnggaranRekening_A A " +
                              " inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ") WHEre B.btRoot = 5  " + sWhere;
                     if (Tahun >= 2021)
                    {
                        SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, 0 as VolumeMurni, '' as SatuanMurni, 0 as HargaMurni, " +
                               " " + _namaKolom2 + " as Jumlah," + _namaKolom1 + " as JumlahMurni,5 as Level , 0 as Nourut,0 as LevelUraian ,'' as Label,A.cJumlahYAD as YAD , 0 as idlokasi  from tAnggaranRekening_A A " +
                              " inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN6.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN6.ToString() + ") WHEre B.btRoot = 6  " + sWhere;
                

                    }
                     sWhere = " AND  B.iTahun =" + _iTahun.ToString() + " AND B.IDDInas = " + _idDinas.ToString() + " AND B.IDProgram=" + idProgram.ToString() +
                                " AND B.IDKegiatan=" + idKegiatan.ToString() + " AND B.Jenis=" + iJenis.ToString() + " AND isnull(B.bPPKD,0)= " + _iPPKD.ToString();
                  
                    SSQL = SSQL + " UNION ALL Select B.IIDRekening,B." + _namaKolomUraian2 + " as Nama,B.btUrut as Urut, B." + _namaKolomvolume2 + "  as Volume, B.sSatuan as Satuan, B." + _namaKolomharga2 + "   as Harga, B." + _namaKolomvolume1 + "  as VolumeMurni, B.sSatuan as SatuanMurni, B." + _namaKolomharga1 + " as HargaMurni,  " +
                                   " " + _namaKolomjumlahuraian2 + "  as Jumlah, B." + _namaKolomjumlahuraian1 + "  as JumlahMurni, 6 +level as Level , btUrut as Nourut, Level as LevelUraian,sLabel as Label,B.cJumlahYAD AS YAD,  idlokasi   from tANggaranUraian_A B   INNER JOIN tANggaranRekening_A D " +
                                   " ON B.iTahun = D.iTahun AND B.IDDInas = D.IDDInas AND B.IDUrusan = D.IDUrusan AND B.IDProgram = D.IDProgram AND B.IDKegiatan = D.IDKegiatan AND B.IIDRekening = D.IIDRekening  and B.bPPKD= D.bPPKD     and B.jenis= d.btJenis and B.idSubkegiatan= d.idSubkegiatan  WHERE  1> 0 " + sWhere + "  ";
                }


                SSQL = SSQL + " order by IIDRekening, Nourut ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                //Nama = DataFormat.MakeSpace( DataFormat.GetSingle(dr["LevelUraian"])) +  DataFormat.GetString(dr["Nama"]),
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new RPTRKA()
                               {

                                   KodeRekening = DataFormat.GetSingle(dr["Level"]) > 5 ? "" : DataFormat.GetLong(dr["IIDRekening"]).ToKodeRekening(m_ProfileRekening),
                                   Nama = DataFormat.GetString(dr["Nama"]),
                                   Vol = DataFormat.GetDecimal(dr["Volume"]) == 0 ? "" : DataFormat.GetDecimal(dr["Volume"]).ToRupiahInReportNoSen(),
                                   Satuan = dr["Satuan"] == null ? "" : dr["Satuan"].ToString(),
                                   SatuanMurni = DataFormat.GetDecimal(dr["JumlahMurni"]) == 0 ? "" : dr["SatuanMurni"].ToString(),
                                   //SatuanMurni = dr["Satuan"] == null ? "" : dr["Satuan"].ToString(),
                                   //DataFormat.GetString(dr["Satuan"]),
                                   Harga = DataFormat.GetDecimal(dr["Harga"]) == 0 ? "" : DataFormat.GetDecimal(dr["Harga"]).ToRupiahInReport(),
                                   Jumlah = DataFormat.GetDecimal(dr["Jumlah"]) > 0 ? DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport() : "",

                                   VolMurni = DataFormat.GetDecimal(dr["JumlahMurni"]) == 0 ? "" : DataFormat.GetDecimal(dr["VolumeMurni"]).ToRupiahInReportNoSen(),

                                   HargaMurni = DataFormat.GetDecimal(dr["HargaMurni"]) == 0 ? "" : DataFormat.GetDecimal(dr["HargaMurni"]).ToRupiahInReport(),
                                   JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]) > 0 ? DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport() : "",
                                   Level = DataFormat.GetSingle(dr["Level"]),
                                   NoUrut = DataFormat.GetSingle(dr["NoUrut"]),
                                   LevelUraian = DataFormat.GetSingle(dr["LevelUraian"]),
                                   Label = DataFormat.GetInteger(dr["idlokasi"]) > 0 ? "-" : DataFormat.GetString(dr["Label"]),
                                   Root = DataFormat.GetSingle(dr["Level"]),
                                   YAD = DataFormat.GetDecimal(dr["YAD"]).ToRupiahInReport(),
                                   Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                   Persen = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]),DataFormat.GetDecimal(dr["JumlahMurni"]) )

                               }).ToList();
                    }
                }
                // Kepentingan cetak agar pada halaman terakhir dan tidak ada datadetail,maka tidak cetak header.
                RPTRKA blankRKA = new RPTRKA();

                blankRKA.KodeRekening = "";
                blankRKA.Nama = "";
                blankRKA.Vol = "";
                blankRKA.Satuan = "";
                blankRKA.Harga = "0";
                blankRKA.Jumlah = "";
                blankRKA.VolMurni = "0";
                blankRKA.HargaMurni = "0";
                blankRKA.JumlahMurni = "0";
                blankRKA.Level = 1;
                blankRKA.NoUrut = 1;
                blankRKA.LevelUraian = 1;
                blankRKA.Label = "";


                //  lst.Add(blankRKA);
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }
        }

        //public Jumlah GetJumlah(int _iTahun, int _idDinas, int _idUrusan, int idProgram, int idKegiatan, int iJenis, int _iPPKD, int pTahap)
        //{
        //    try
        //    {
        //        GetKolom(pTahap);
        //        Jumlah dJumlah = new Jumlah();

        //        string sWhere = "";
        //        if (iJenis == 0)
        //        {
        //            sWhere = " AND iTahun =" + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString();
        //        }
        //        else
        //        {
        //            sWhere = " AND  iTahun =" + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString() + " AND IDProgram=" + idProgram.ToString() +
        //                    " AND IDKegiatan=" + idKegiatan.ToString() + " AND btJenis=" + iJenis.ToString() + " AND isnull(bPPKD,0)= " + _iPPKD.ToString();
        //        }
        //        SSQL = " Select  SUM (" + _namaKolom2 + ") as Jumlah, SUM(" + _namaKolom1 + ") as JumlahMurni from tAnggaranRekening_A WHERE 2>  1 " + sWhere;

        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);
        //        //Nama = DataFormat.MakeSpace( DataFormat.GetSingle(dr["LevelUraian"])) +  DataFormat.GetString(dr["Nama"]),
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {

        //                DataRow dr = dt.Rows[0];
        //                dJumlah = new Jumlah ()
        //                       {
                                  
        //                           jumlah = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
        //                           jumlahmurni = DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport(),
        //                           seleisih=(DataFormat.GetDecimal(dr["Jumlah"])-DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
        //                           persen= DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]),DataFormat.GetDecimal(dr["JumlahMurni"]))


        //                       };

        //            }
        //        }

        //        return dJumlah;


        //    }
        //    catch (Exception ex)
        //    {
        //        return null;

        //    }

        //}

        public RPTRKA GetJumlah(int _iTahun, int _idDinas, int _idUrusan, int idProgram, int idKegiatan, int iJenis, int _iPPKD, int pTahap, List<SKPD> lstSKPD = null)
        {
            RPTRKA oRptRKA = new RPTRKA();
            _isError = false;
            _lastError = "";
            try
            {


                string strDinas = "(";
                if (lstSKPD != null)
                {
                    foreach (SKPD d in lstSKPD)
                    {
                        strDinas = strDinas + d.ID.ToString() + ",";
                    }
                    strDinas = strDinas + "99)";

                } else
                    strDinas = "(" + _idDinas.ToString() + ")";



                GetKolom(pTahap);


                string sWhere = "";
                if (iJenis == 0)
                {
                    sWhere = " AND iTahun =" + _iTahun.ToString() + " AND IDDInas in " + strDinas ;
                }
                else
                {
                    if (Tahun <=2020)
                    sWhere = " AND  iTahun =" + _iTahun.ToString() + " AND IDDInas in " + strDinas + " AND IDProgram=" + idProgram.ToString() +
                           " AND IDKegiatan=" + idKegiatan.ToString() + " and idsubkegiatan=0 AND btJenis=" + iJenis.ToString() + " AND isnull(bPPKD,0)= " + _iPPKD.ToString();
                    else

                        sWhere = " AND  iTahun =" + _iTahun.ToString() + " AND IDDInas in " + strDinas + " AND IDProgram=" + idProgram.ToString() +
                           " AND IDKegiatan=" + idKegiatan.ToString() + " and  btJenis=" + iJenis.ToString() + " AND isnull(bPPKD,0)= " + _iPPKD.ToString();
                    
                    //sWhere = " AND  iTahun =" + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString() + " AND IDProgram=" + idProgram.ToString() +
                     //       " AND IDKegiatan=" + idKegiatan.ToString() + " AND btJenis=" + iJenis.ToString() + " AND isnull(bPPKD,0)= " + _iPPKD.ToString();
                }
                SSQL = " Select SUM (" + _namaKolom2 + ") as Jumlah, SUM(" + _namaKolom1 + ") as JumlahMurni from tAnggaranRekening_A A " +
                    " WHERE 1> 0  " + sWhere;

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                //Nama = DataFormat.MakeSpace( DataFormat.GetSingle(dr["LevelUraian"])) +  DataFormat.GetString(dr["Nama"]),
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        oRptRKA = new RPTRKA()
                        {

                            KodeRekening = "",
                            Nama = "Jumlah",
                            Vol = "",
                            Satuan = "",
                            Harga = "",
                            Jumlah = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
                            DJumlah=  DataFormat.GetDecimal(dr["Jumlah"]),
                            VolMurni = "",
                            HargaMurni = "",
                            JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport(),
                            DJumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]),
                            Level = 0,
                            NoUrut = 0,
                            LevelUraian = 0,
                            Label = "",
                            Root = 0,
                            YAD = "",
                            Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                            Persen = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]),DataFormat.GetDecimal(dr["JumlahMurni"]) )

                        };
                    }
                }
                return oRptRKA;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }
        }
        public List<RPTRKA> GetDataDPA(int _iTahun, int _idDinas, int _idUrusan, int idProgram, int idKegiatan, int iJenis, int _iPPKD)
        {
            List<RPTRKA> lst = new List<RPTRKA>();
            try
            {

                string sWhere = "";
                if (iJenis == 0)
                {
                    sWhere = " AND cJumlahMurni> 0 AND iTahun =" + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString();
                }
                else
                {
                    sWhere = "  AND cJumlahMurni> 0  AND  iTahun =" + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString() + " AND IDProgram=" + idProgram.ToString() +
                        " AND IDKegiatan=" + idKegiatan.ToString() + " AND btJenis=" + iJenis.ToString() + " AND bPPKD= " + _iPPKD.ToString();

                }

                SSQL = " Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A.cJumlahMurni) as JumlahOlah," +
                    " SUM (cJumlah) as Jumlah ,1 as Level, 0 as Nourut,0 as LevelUraian,'' as Label, sum(A.cJumlahYAD) as YAD  from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ") " +
                    " WHERE B.btRoot = 1 " + sWhere + "  Group by B.IIDRekening,B.sNamaRekening ";
                SSQL = SSQL + "  UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A.cJumlahMurni) as JumlahOlah," +
                      " SUM (cJumlah) as Jumlah,2 as Level , 0 as Nourut ,0 as LevelUraian,'' as Label,sum(A.cJumlahYAD) as YAD  from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")" +
                      " WHEre B.btRoot = 2   " + sWhere + " Group by B.IIDRekening,B.sNamaRekening ";

                SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A.cJumlahMurni) as JumlahOlah, " +
                      "SUM (cJumlah) as Jumlah,3 as Level , 0 as Nourut, 0 as LevelUraian,'' as Label,sum(A.cJumlahYAD) as YAD   from tAnggaranRekening_A A inner join mRekening B  " +
                      " ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")  WHEre B.btRoot = 3  " + sWhere + " Group by B.IIDRekening,B.sNamaRekening  ";

                if (iJenis > 0)
                {
                    SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A.cJumlahMurni) as JumlahOlah, " +
                                " SUM (cJumlah) as Jumlah,4 as Level , 0 as Nourut, 0 as LevelUraian ,'' as Label,sum(A.cJumlahYAD) as YAD    from tAnggaranRekening_A A inner join mRekening B  " +
                                " ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN4.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN4.ToString() + ") WHEre B.btRoot = 4  " + sWhere + " Group by B.IIDRekening,B.sNamaRekening ";

                    SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, " +
                               "A.cJumlahMurni as JumlahOlah,cJumlah as Jumlah,5 as Level , 0 as Nourut,0 as LevelUraian ,'' as Label,A.cJumlahYAD as YAD  from tAnggaranRekening_A A " +
                               " inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ") WHEre B.btRoot = 5  " + sWhere;

                    //           " Group by B.IIDRekening,B.sNamaRekening ";

                    sWhere = " AND  iTahun =" + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString() + " AND IDProgram=" + idProgram.ToString() +
                             " AND IDKegiatan=" + idKegiatan.ToString() + " AND Jenis=" + iJenis.ToString() + " AND bPPKD= " + _iPPKD.ToString();


                    SSQL = SSQL + " UNION ALL Select A.IIDRekening,A.sUraianMurni as Nama,A.btUrut as Urut, A.VolMurni as Volume, A.sSatuan as Satuan, A.cHargaMurni  as Harga, " +
                                   "A.JumlahMurni as JumlahOlah, 0 as Jumlah,6 as Level , btUrut as Nourut, Level as LevelUraian ,sLabel as Label,A.cJumlahYAD AS YAD  from tANggaranUraian_A A  WHERE  (A.JumlahMurni > 0 or A.JumlahRKAP > 0 )" + sWhere + "  ";
                }
                SSQL = SSQL + " order by IIDRekening,Level, Nourut ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                //Nama = DataFormat.MakeSpace( DataFormat.GetSingle(dr["LevelUraian"])) +  DataFormat.GetString(dr["Nama"]),
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new RPTRKA()
                               {

                                   KodeRekening = DataFormat.GetSingle(dr["Level"]) > 5 ? "" : DataFormat.GetLong(dr["IIDRekening"]).ToKodeRekening(m_ProfileRekening),
                                   Nama = DataFormat.GetString(dr["Nama"]),
                                   Vol = DataFormat.GetDecimal(dr["Volume"])>0?DataFormat.GetDecimal(dr["Volume"]).ToRupiahInReportNoSen ():"",
                                   Satuan = DataFormat.GetString(dr["Satuan"]),
                                   Harga = DataFormat.GetDecimal(dr["Harga"]).ToRupiahInReport(),
                                   Jumlah = DataFormat.GetDecimal(dr["JumlahOlah"]).ToRupiahInReport(),
                                   VolMurni = "0",
                                   HargaMurni = "0",
                                   JumlahMurni = "0",
                                   Level = DataFormat.GetSingle(dr["Level"]),
                                   NoUrut = DataFormat.GetSingle(dr["NoUrut"]),
                                   LevelUraian = DataFormat.GetSingle(dr["LevelUraian"]),
                                   Label = DataFormat.GetString(dr["Label"]),
                                   Root = DataFormat.GetSingle(dr["Level"]),
                                   YAD = DataFormat.GetDecimal(dr["YAD"]).ToRupiahInReport(),



                               }).ToList();
                    }
                }
                // Kepentingan cetak agar pada halaman terakhir dan tidak ada datadetail,maka tidak cetak header.
                RPTRKA blankRKA = new RPTRKA();

                blankRKA.KodeRekening = "";
                blankRKA.Nama = "";
                blankRKA.Vol = "";
                blankRKA.Satuan = "";
                blankRKA.Harga = "0";
                blankRKA.Jumlah = "0";
                blankRKA.VolMurni = "0";
                blankRKA.HargaMurni = "0";
                blankRKA.JumlahMurni = "0";
                blankRKA.Level = 1;
                blankRKA.NoUrut = 1;
                blankRKA.LevelUraian = 1;
                blankRKA.Label = "";


                //lst.Add(blankRKA);
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }
        }
        public List<RPTRKA> GetDataDPAPenyempurnaanI(int _iTahun, int _idDinas, int _idUrusan, int idProgram, int idKegiatan, int iJenis, int _iPPKD)
        {
            List<RPTRKA> lst = new List<RPTRKA>();
            try
            {

                string sWhere = "";
                if (iJenis == 0)
                {
                    sWhere = " AND cDPA> 0 AND iTahun =" + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString();
                }
                else
                {
                    sWhere = "  AND cDPA> 0  AND  iTahun =" + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString() + " AND IDProgram=" + idProgram.ToString() +
                        " AND IDKegiatan=" + idKegiatan.ToString() + " AND btJenis=" + iJenis.ToString() + " AND bPPKD= " + _iPPKD.ToString();

                }

                SSQL = " Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A.cDPA) as JumlahOlah," +
                    " SUM (cJumlah) as Jumlah ,0 as VolumeMurni, '' as SatuanMUrni, 0 as HargaMurni, SUM(A.cJumlahMurni) as JumlahMurni, 1 as Level, 0 as Nourut,0 as LevelUraian,'' as Label, sum(A.cJumlahYAD) as YAD  from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ") " +
                    " WHERE B.btRoot = 1 " + sWhere + "  Group by B.IIDRekening,B.sNamaRekening ";
                SSQL = SSQL + "  UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A.cDPA) as JumlahOlah," +
                      " SUM (cJumlah) as Jumlah,0 as VolumeMurni, '' as SatuanMUrni, 0 as HargaMurni, SUM(A.cJumlahMurni) as JumlahMurni,2 as Level , 0 as Nourut ,0 as LevelUraian,'' as Label,sum(A.cJumlahYAD) as YAD  from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")" +
                      " WHEre B.btRoot = 2   " + sWhere + " Group by B.IIDRekening,B.sNamaRekening ";

                SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A.cDPA) as JumlahOlah, " +
                      "SUM (cJumlah) as Jumlah,0 as VolumeMurni, '' as SatuanMUrni, 0 as HargaMurni, SUM(A.cJumlahMurni) as JumlahMurni,3 as Level , 0 as Nourut, 0 as LevelUraian,'' as Label,sum(A.cJumlahYAD) as YAD   from tAnggaranRekening_A A inner join mRekening B  " +
                      " ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")  WHEre B.btRoot = 3  " + sWhere + " Group by B.IIDRekening,B.sNamaRekening  ";

                if (iJenis > 0)
                {
                    SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A.cDPA) as JumlahOlah, " +
                                " SUM (cJumlah) as Jumlah,0 as VolumeMurni, '' as SatuanMUrni, 0 as HargaMurni, SUM(A.cJumlahMurni) as JumlahMurni,4 as Level , 0 as Nourut, 0 as LevelUraian ,'' as Label,sum(A.cJumlahYAD) as YAD    from tAnggaranRekening_A A inner join mRekening B  " +
                                " ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN4.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN4.ToString() + ") WHEre B.btRoot = 4  " + sWhere + " Group by B.IIDRekening,B.sNamaRekening ";

                    SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, " +
                               "A.cDPA as JumlahOlah,cJumlah as Jumlah,0 as VolumeMurni, '' as SatuanMUrni, 0 as HargaMurni, A.cJumlahMurni as JumlahMurni,5 as Level , 0 as Nourut,0 as LevelUraian ,'' as Label,A.cJumlahYAD as YAD  from tAnggaranRekening_A A " +
                               " inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ") WHEre B.btRoot = 5  " + sWhere;

                    //           " Group by B.IIDRekening,B.sNamaRekening ";

                    sWhere = " AND  iTahun =" + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString() + " AND IDProgram=" + idProgram.ToString() +
                                " AND IDKegiatan=" + idKegiatan.ToString() + " AND Jenis=" + iJenis.ToString() + " AND bPPKD= " + _iPPKD.ToString();

                    SSQL = SSQL + " UNION ALL Select A.IIDRekening,A.sUraianAPBD as Nama,A.btUrut as Urut, A.Vol as Volume, A.sSatuanDPA as Satuan, A.cHarga  as Harga, " +
                                   "A.Jumlah as JumlahOlah, 0 as Jumlah,A.VolMurni as VolumeMurni, A.sSatuan as SatuanMUrni, A.cHargaMurni as HargaMurni, A.JumlahMurni as JumlahMurni, 6 as Level , btUrutDPA as Nourut, iLevelDPA as LevelUraian ,sLabelDPA as Label,A.cJumlahYAD AS YAD  from tANggaranUraian_A A  WHERE  A.JumlahOlah > 0 " + sWhere + "  ";

                    //'SSQL = SSQL + " UNION ALL Select A.IIDRekening,A.sUraian as Nama,A.btUrut as Urut, A.Vol as Volume, A.sSatuanDPA as Satuan, A.cHarga  as Harga, " +
                    //'               "A.Jumlah as JumlahOlah, 0 as Jumlah,A.VolMurni as VolumeMurni, A.sSatuan as SatuanMUrni, A.cHargaMurni as HargaMurni, A.JumlahMurni as JumlahMurni, 6 as Level , btUrutDPA as Nourut, iLevelDPA as LevelUraian ,sLabelDPA as Label,A.cJumlahYAD AS YAD  from tANggaranUraian_A A  WHERE  A.JumlahOlah > 0 " + sWhere + "  ";
                }
                SSQL = SSQL + " order by IIDRekening,Level, Nourut ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                //Nama = DataFormat.MakeSpace( DataFormat.GetSingle(dr["LevelUraian"])) +  DataFormat.GetString(dr["Nama"]),
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new RPTRKA()
                               {

                                   KodeRekening = DataFormat.GetSingle(dr["Level"]) > 5 ? "" : DataFormat.GetLong(dr["IIDRekening"]).ToKodeRekening(m_ProfileRekening),
                                   Nama = DataFormat.GetString(dr["Nama"]),
                                   Vol = DataFormat.GetString(dr["Volume"]),
                                   Satuan = DataFormat.GetString(dr["Satuan"]),
                                   Harga = DataFormat.GetDecimal(dr["Harga"]).ToRupiahInReport(),
                                   Jumlah = DataFormat.GetDecimal(dr["JumlahOlah"]).ToRupiahInReport(),
                                   VolMurni = DataFormat.GetDecimal (dr["VolumeMurni"]).ToString().Replace (".00000",""),
                                   HargaMurni = DataFormat.GetDecimal(dr["HargaMurni"]).ToRupiahInReport(),
                                   JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport(),
                                   Level = DataFormat.GetSingle(dr["Level"]),
                                   NoUrut = DataFormat.GetSingle(dr["NoUrut"]),
                                   LevelUraian = DataFormat.GetSingle(dr["LevelUraian"]),
                                   Label = DataFormat.GetString(dr["Label"]),
                                   Root = DataFormat.GetSingle(dr["Level"]),
                                   YAD = DataFormat.GetDecimal(dr["YAD"]).ToRupiahInReport(),
                                   Selisih = (DataFormat.GetDecimal(dr["JumlahOlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                   Persen = GetPresentase (DataFormat.GetDecimal(dr["JumlahMurni"]) ,DataFormat.GetDecimal(dr["JumlahOlah"]) )


                               }).ToList();
                    }
                }
                // Kepentingan cetak agar pada halaman terakhir dan tidak ada datadetail,maka tidak cetak header.
                RPTRKA blankRKA = new RPTRKA();

                blankRKA.KodeRekening = "";
                blankRKA.Nama = "";
                blankRKA.Vol = "";
                blankRKA.Satuan = "";
                blankRKA.Harga = "0";
                blankRKA.Jumlah = "0";
                blankRKA.VolMurni = "0";
                blankRKA.HargaMurni = "0";
                blankRKA.JumlahMurni = "0";
                blankRKA.Level = 1;
                blankRKA.NoUrut = 1;
                blankRKA.LevelUraian = 1;
                blankRKA.Label = "";


                //lst.Add(blankRKA);
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }
        }
        private string GetPresentase(decimal Murni, decimal Perubahan)
        {
            if (Murni == Perubahan)
                return "0.00";
            if (Murni == 0)
                return "100.00";

            decimal persen = 100 * ((Perubahan - Murni) / Murni);

            return  persen.ToString("#.##");
        }
        public List<RPTRKA> GetDataRKAPenyempurnaanI(int _iTahun, int _idDinas, int _idUrusan, int idProgram, int idKegiatan, int iJenis, int _iPPKD)
        {
            List<RPTRKA> lst = new List<RPTRKA>();
            try
            {

                string sWhere = "";
                if (iJenis == 0)
                {
                    sWhere = " AND cDPA> 0 AND iTahun =" + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString();
                }
                else
                {
                    sWhere = "  AND cDPA> 0  AND  iTahun =" + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString() + " AND IDProgram=" + idProgram.ToString() +
                        " AND IDKegiatan=" + idKegiatan.ToString() + " AND btJenis=" + iJenis.ToString() + " AND bPPKD= " + _iPPKD.ToString();

                }

                SSQL = " Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A.cJumlahOlah) as JumlahOlah," +
                    " SUM (cJumlah) as Jumlah ,0 as VolumeMurni, '' as SatuanMUrni, 0 as HargaMurni, SUM(A.cJumlahMurni) as JumlahMurni, 1 as Level, 0 as Nourut,0 as LevelUraian,'' as Label, sum(A.cJumlahYAD) as YAD  from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ") " +
                    " WHERE B.btRoot = 1 " + sWhere + "  Group by B.IIDRekening,B.sNamaRekening ";
                SSQL = SSQL + "  UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A.cJumlahOlah) as JumlahOlah," +
                      " SUM (cJumlah) as Jumlah,0 as VolumeMurni, '' as SatuanMUrni, 0 as HargaMurni, SUM(A.cJumlahMurni) as JumlahMurni,2 as Level , 0 as Nourut ,0 as LevelUraian,'' as Label,sum(A.cJumlahYAD) as YAD  from tAnggaranRekening_A A inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")" +
                      " WHEre B.btRoot = 2   " + sWhere + " Group by B.IIDRekening,B.sNamaRekening ";

                SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A.cJumlahOlah) as JumlahOlah, " +
                      "SUM (cJumlah) as Jumlah,0 as VolumeMurni, '' as SatuanMUrni, 0 as HargaMurni, SUM(A.cJumlahMurni) as JumlahMurni,3 as Level , 0 as Nourut, 0 as LevelUraian,'' as Label,sum(A.cJumlahYAD) as YAD   from tAnggaranRekening_A A inner join mRekening B  " +
                      " ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")  WHEre B.btRoot = 3  " + sWhere + " Group by B.IIDRekening,B.sNamaRekening  ";

                if (iJenis > 0)
                {
                    SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, SUM(A.cJumlahOlah) as JumlahOlah, " +
                                " SUM (cJumlah) as Jumlah,0 as VolumeMurni, '' as SatuanMUrni, 0 as HargaMurni, SUM(A.cJumlahMurni) as JumlahMurni,4 as Level , 0 as Nourut, 0 as LevelUraian ,'' as Label,sum(A.cJumlahYAD) as YAD    from tAnggaranRekening_A A inner join mRekening B  " +
                                " ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN4.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN4.ToString() + ") WHEre B.btRoot = 4  " + sWhere + " Group by B.IIDRekening,B.sNamaRekening ";

                    SSQL = SSQL + " UNION ALL Select B.IIDRekening,B.sNamaRekening as Nama,0 as Urut, 0 as Volume, '' as Satuan, 0 as Harga, " +
                               "A.cDPA as JumlahOlah,cJumlah as Jumlah,0 as VolumeMurni, '' as SatuanMUrni, 0 as HargaMurni, A.cJumlahMurni as JumlahMurni,5 as Level , 0 as Nourut,0 as LevelUraian ,'' as Label,A.cJumlahYAD as YAD  from tAnggaranRekening_A A " +
                               " inner join mRekening B ON LEFT(A.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ")= Left(B.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ") WHEre B.btRoot = 5  " + sWhere;

                    //           " Group by B.IIDRekening,B.sNamaRekening ";

                    sWhere = " AND  iTahun =" + _iTahun.ToString() + " AND IDDInas = " + _idDinas.ToString() + " AND IDProgram=" + idProgram.ToString() +
                                " AND IDKegiatan=" + idKegiatan.ToString() + " AND Jenis=" + iJenis.ToString() + " AND bPPKD= " + _iPPKD.ToString();

                    //btUrutDPA = btUrut,cHarga= cHargaOlah, Vol=VolOlah,Jumlah= JumlahOlah, iLevelDPA=Level , sSatuanDPA=sSatuan,sUraianAPBD=sUraian,sLabelDPA=sLabel 

                    SSQL = SSQL + " UNION ALL Select A.IIDRekening,A.sUraian as Nama,A.btUrut as Urut, A.VolOlah as Volume, A.sSatuan as Satuan, A.cHargaOlah  as Harga, " +
                                   "A.JumlahOlah as JumlahOlah, 0 as Jumlah,A.VolMurni as VolumeMurni, A.sSatuan as SatuanMUrni, A.cHargaMurni as HargaMurni, A.JumlahMurni as JumlahMurni, 6 as Level , btUrutDPA as Nourut, iLevelDPA as LevelUraian ,sLabelDPA as Label,A.cJumlahYAD AS YAD  from tANggaranUraian_A A  WHERE  A.JumlahOlah > 0 " + sWhere + "  ";

//                    JumlahMurni
//sUraianMurni
//sLabelMurni
//VolMurni
//cHargaMurni

                    //'SSQL = SSQL + " UNION ALL Select A.IIDRekening,A.sUraian as Nama,A.btUrut as Urut, A.Vol as Volume, A.sSatuanDPA as Satuan, A.cHarga  as Harga, " +
                    //'               "A.Jumlah as JumlahOlah, 0 as Jumlah,A.VolMurni as VolumeMurni, A.sSatuan as SatuanMUrni, A.cHargaMurni as HargaMurni, A.JumlahMurni as JumlahMurni, 6 as Level , btUrutDPA as Nourut, iLevelDPA as LevelUraian ,sLabelDPA as Label,A.cJumlahYAD AS YAD  from tANggaranUraian_A A  WHERE  A.JumlahOlah > 0 " + sWhere + "  ";
                }
                SSQL = SSQL + " order by IIDRekening,Level, Nourut ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                //Nama = DataFormat.MakeSpace( DataFormat.GetSingle(dr["LevelUraian"])) +  DataFormat.GetString(dr["Nama"]),
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new RPTRKA()
                               {

                                   KodeRekening = DataFormat.GetSingle(dr["Level"]) > 5 ? "" : DataFormat.GetLong(dr["IIDRekening"]).ToKodeRekening(m_ProfileRekening),
                                   Nama = DataFormat.GetString(dr["Nama"]),
                                   Vol = DataFormat.GetString(dr["Volume"]),
                                   Satuan = DataFormat.GetString(dr["Satuan"]),
                                   Harga = DataFormat.GetDecimal(dr["Harga"]).ToRupiahInReport(),
                                   Jumlah = DataFormat.GetDecimal(dr["JumlahOlah"]).ToRupiahInReport(),
                                   VolMurni = DataFormat.GetString(dr["VolumeMurni"]),
                                   HargaMurni = DataFormat.GetDecimal(dr["HargaMurni"]).ToRupiahInReport(),
                                   JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport(),
                                   Level = DataFormat.GetSingle(dr["Level"]),
                                   NoUrut = DataFormat.GetSingle(dr["NoUrut"]),
                                   LevelUraian = DataFormat.GetSingle(dr["LevelUraian"]),
                                   Label = DataFormat.GetString(dr["Label"]),
                                   Root = DataFormat.GetSingle(dr["Level"]),
                                   YAD = DataFormat.GetDecimal(dr["YAD"]).ToRupiahInReport(),
                                   Selisih = (DataFormat.GetDecimal(dr["JumlahOlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                   Persen = DataFormat.GetDecimal(dr["JumlahMurni"])!=0? ((((DataFormat.GetDecimal(dr["JumlahOlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])))/DataFormat.GetDecimal(dr["JumlahMurni"]))*100).ToString("000"):""



                               }).ToList();
                    }
                }
                // Kepentingan cetak agar pada halaman terakhir dan tidak ada datadetail,maka tidak cetak header.
                RPTRKA blankRKA = new RPTRKA();

                blankRKA.KodeRekening = "";
                blankRKA.Nama = "";
                blankRKA.Vol = "";
                blankRKA.Satuan = "";
                blankRKA.Harga = "0";
                blankRKA.Jumlah = "0";
                blankRKA.VolMurni = "0";
                blankRKA.HargaMurni = "0";
                blankRKA.JumlahMurni = "0";
                blankRKA.Level = 1;
                blankRKA.NoUrut = 1;
                blankRKA.LevelUraian = 1;
                blankRKA.Label = "";


                //lst.Add(blankRKA);
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }
        }
        public List<RKA22Murni> GetRekapRKA22Murni(int _pTahun, int _IDDInas, int pTahap)
        {
            List<RKA22Murni> lst = new List<RKA22Murni>();
            GetKolom(pTahap);

            try{

                //SSQL = " Select A.IDUrusan, A.IDDInas,0 as IDProgram,0 as IDKegiatan, 'Urusan ' + B.sNamaUrusan as Nama, '' as sLokasi,  " +
                //        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaPegawaiMurni,  " +
                //        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaBarangJasaMurni,  " +
                //        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaModalMurni,  " +
                //        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom1 + " else 0 END) AS JumlahMurni ," +
                //        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom2 + " else 0 END ) AS BelanjaPegawai,  " +
                //        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaBarangJasa,  " +
                //        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaModal,  " +
                //        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom2 + " else 0 END) AS Jumlah from tANggaranRekening_A A  " +
                //        "INNER JOIN mUrusan  B ON A.IDUrusan= B.ID  where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas =" + _IDDInas.ToString() +
                //        "GROUP BY A.IDUrusan, A.IDDInas,B.sNamaUrusan ";


                //SSQL =SSQL +  " UNION ALL Select A.IDUrusan, A.IDDInas,A.IDProgram,0 as IDKegiatan, B.sNamaProgram as Nama, '' as sLokasi, " +
                //        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaPegawaiMurni, " +
                //        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaBarangJasaMurni, " +
                //        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaModalMurni,  " +
                //        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom1 + " else 0 END) AS JumlahMurni, " +
                //        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom2 + " else 0 END ) AS BelanjaPegawai, " +
                //        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaBarangJasa, " +
                //        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaModal,  " +
                //        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom2 + " else 0 END) AS Jumlah " +
                //        " from tANggaranRekening_A A  INNER JOIN tPrograms_A B ON A.iTahun= B.iTahun and A.IDUrusan= B.IDUrusan " +
                //        " and A.IDProgram= B.IDProgram  AND A.IDDInas= B.IDDInas  where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas =" + _IDDInas.ToString() +
                //        " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram,B.sNamaProgram ";


                //SSQL = SSQL + "UNION ALL Select A.IDUrusan, A.IDDInas,A.IDProgram,A.IDKegiatan, B.sNama as Nama, B.sLokasi, " +
                //        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaPegawaiMurni, " +
                //        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaBarangJasaMurni, " +
                //        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaModalMurni, " +
                //        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom1 + " else 0 END) AS JumlahMurni, " +
                //        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom2 + " else 0 END ) AS BelanjaPegawai, " +
                //        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaBarangJasa, " +
                //        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaModal, " +
                //        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom2 + " else 0 END) AS Jumlah from tANggaranRekening_A A " +
                //        " INNER JOIN tKegiatan_A B ON A.iTahun= B.iTahun and A.IDUrusan= B.IDUrusan AND A.IDKegiatan= B.IDkegiatan " +
                //        " AND A.IDDInas= B.IDDInas where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas =" + _IDDInas.ToString() +
                //          " GROUP BY A.IDUrusan,A.IDDInas,A.IDProgram,A.IDKegiatan, B.sNama , B.sLokasi ";

                //SSQL = SSQL + "UNION ALL Select 999 as IDUrusan, A.IDDInas,1000 as IDProgram,1000 as IDKegiatan, 1 as IsPokok, ' J U M L A H ' as Nama, '' as sLokasi, " +
                //         "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaPegawaiMurni, " +
                //         "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaBarangJasaMurni, " +
                //         "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaModalMurni, " +
                //         " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom1 + " else 0 END) AS JumlahMurni, " +
                //         "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom2 + " else 0 END ) AS BelanjaPegawai, " +
                //         "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaBarangJasa, " +
                //         "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaModal, " +
                //         " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom2 + " else 0 END) AS Jumlah from tANggaranRekening_A A " +
                //         "  inner join TkEGIATAN_a b On A.iTahun = B.iTahun and A.IDDInas = B.IDDInas and A.IDKegiatan = B.IDKegiatan and a.IDUrusan = B.IDurusan " +
                //         "INNER JOIN MPelaksanaUrusan D ON D.IDUrusan= A.IDurusan AND D.IDUrusan=B.IDurusan   and D.iTAhun= A.ITahun AND D.iTahun=B.iTahun " +
                //        " AND D.IDDInas= A.IDDInas AND D.IDDInas=B.IDDInas  where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas =" + _IDDInas.ToString() +
                //           " GROUP by A.IDDInas ";

                //SSQL = SSQL + " ORDER BY IDDInas, IDUrusan,IDProgram,IDKegiatan";

                SSQL = " Select A.IDUrusan, A.IDDInas,0 as IDProgram,0 as IDKegiatan,D.IsPokok as isPokok, 'Urusan ' + B.sNamaUrusan as Nama, '' as sLokasi,  " +
                       "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaPegawaiMurni,  " +
                       "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaBarangJasaMurni,  " +
                       "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaModalMurni,  " +
                       "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom1 + " else 0 END) AS JumlahMurni ," +
                       "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom2 + " else 0 END ) AS BelanjaPegawai,  " +
                       "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaBarangJasa,  " +
                       "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaModal,  " +
                       "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom2 + " else 0 END) AS Jumlah " +
                       " from tANggaranRekening_A A  " +
                       "INNER JOIN mUrusan  B ON A.IDUrusan= B.ID INNER JOIN MPelaksanaUrusan D ON D.IDurusan = B.ID  and A.iTahun = D.iTahun AND A.IDUrusan = D.IDurusan  and A.IDDInas = D.IDDInas  where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas =" + _IDDInas.ToString() +
                       "  GROUP BY A.IDUrusan, A.IDDInas,B.sNamaUrusan,D.IsPokok ";

                SSQL = SSQL + " UNION ALL Select A.IDUrusan, A.IDDInas,A.IDProgram,0 as IDKegiatan,D.IsPokok  as IsPokok, B.sNamaProgram as Nama, '' as sLokasi, " +
                        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaPegawaiMurni, " +
                        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaBarangJasaMurni, " +
                        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaModalMurni,  " +
                        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom1 + " else 0 END) AS JumlahMurni, " +
                        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom2 + " else 0 END ) AS BelanjaPegawai, " +
                        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaBarangJasa, " +
                        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaModal,  " +
                        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom2 + " else 0 END) AS Jumlah " +
                        " from tANggaranRekening_A A  INNER JOIN tPrograms_A B ON A.iTahun= B.iTahun and A.IDUrusan= B.IDUrusan " +
                        " and A.IDProgram= B.IDProgram  AND A.IDDInas= B.IDDInas INNER JOIN MPelaksanaUrusan D ON A.iTahun=D.iTahun AND B.iTahun = D.iTahun AND D.IDUrusan= A.IDurusan AND D.IDUrusan=B.IDurusan " +
                        " AND A.IDDInas = D.IDDInas and B.IDDInas = D.IDDinas where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas =" + _IDDInas.ToString() +
                        " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram,B.sNamaProgram,D.IsPokok  ";


                SSQL = SSQL + "UNION ALL Select A.IDUrusan, A.IDDInas,A.IDProgram,A.IDKegiatan, D.IsPokok as IsPokok,B.sNama as Nama, B.sLokasi, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaPegawaiMurni, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaBarangJasaMurni, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaModalMurni, " +
                        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom1 + " else 0 END) AS JumlahMurni, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom2 + " else 0 END ) AS BelanjaPegawai, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaBarangJasa, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaModal, " +
                        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom2 + " else 0 END) AS Jumlah from tANggaranRekening_A A " +
                        " INNER JOIN tKegiatan_A B ON A.iTahun= B.iTahun and A.IDUrusan= B.IDUrusan AND A.IDKegiatan= B.IDkegiatan " +
                        " AND A.IDDInas= B.IDDInas   INNER JOIN MPelaksanaUrusan D ON D.IDUrusan= A.IDurusan AND D.IDUrusan=B.IDurusan and D.iTAhun= A.ITahun AND D.iTahun=B.iTahun " +
                        " AND D.IDDInas= A.IDDInas AND D.IDDInas=B.IDDInas    where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas =" + _IDDInas.ToString() +
                          " GROUP BY A.IDUrusan,A.IDDInas,A.IDProgram,A.IDKegiatan, B.sNama , B.sLokasi,D.IsPokok ";

                SSQL = SSQL + "UNION ALL Select 999 as IDUrusan, A.IDDInas,1000 as IDProgram,1000 as IDKegiatan, 1 as IsPokok, ' J U M L A H ' as Nama, '' as sLokasi, " +
                         "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaPegawaiMurni, " +
                         "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaBarangJasaMurni, " +
                         "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaModalMurni, " +
                         " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom1 + " else 0 END) AS JumlahMurni, " +
                         "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom2 + " else 0 END ) AS BelanjaPegawai, " +
                         "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaBarangJasa, " +
                         "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaModal, " +
                         " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom2 + " else 0 END) AS Jumlah from tANggaranRekening_A A " +
                         "  inner join TkEGIATAN_a b On A.iTahun = B.iTahun and A.IDDInas = B.IDDInas and A.IDKegiatan = B.IDKegiatan and a.IDUrusan = B.IDurusan " +
                         "INNER JOIN MPelaksanaUrusan D ON D.IDUrusan= A.IDurusan AND D.IDUrusan=B.IDurusan   and D.iTAhun= A.ITahun AND D.iTahun=B.iTahun " +
                        " AND D.IDDInas= A.IDDInas AND D.IDDInas=B.IDDInas  where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas =" + _IDDInas.ToString() +
                           " GROUP by A.IDDInas ";

                SSQL = SSQL + " ORDER BY IDDInas,IsPokok, IDUrusan,IDProgram,IDKegiatan";

                //SSQL = SSQL + " ORDER BY A.IDDInas,IsPokok, A.IDUrusan,A.IDProgram,A.IDKegiatan";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new RKA22Murni()
                               {
                                  
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDkegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    KodeProgram = GetKodeProgram(DataFormat.GetInteger(dr["IDProgram"]), DataFormat.GetInteger(dr["IDKegiatan"])),
                                    KodeKegiatan = GetKodeKegiatan(DataFormat.GetInteger(dr["IDKegiatan"])),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Lokasi = DataFormat.GetString(dr["sLokasi"]),
                                    Target = GetTarget(2018, _IDDInas, DataFormat.GetInteger(dr["IDUrusan"]), DataFormat.GetInteger(dr["IDProgram"]), DataFormat.GetInteger(dr["IDKegiatan"]), pTahap,null),
                                    BelanjaPegawai = DataFormat.GetDecimal(dr["BelanjaPegawai"]).ToRupiahInReport(),
                                    BelanjaBarangJasa = DataFormat.GetDecimal(dr["BelanjaBarangJasa"]).ToRupiahInReport(),
                                    BelanjaModal = DataFormat.GetDecimal(dr["BelanjaModal"]).ToRupiahInReport(),
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
                                    BelanjaPegawaiMurni = DataFormat.GetDecimal(dr["BelanjaPegawaiMurni"]).ToRupiahInReport(),
                                    BelanjaBarangJasaMurni = DataFormat.GetDecimal(dr["BelanjaBarangJasaMurni"]).ToRupiahInReport(),
                                    BelanjaModalMurni = DataFormat.GetDecimal(dr["BelanjaModalMurni"]).ToRupiahInReport(),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport(),
                                    Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                    Persen= DataFormat.GetProsentase (DataFormat.GetDecimal(dr["Jumlah"]) ,DataFormat.GetDecimal(dr["JumlahMurni"]) )
                                
                               }).ToList();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }
        }
        public List<RKA22Murni> GetRekapRKA22Murni90(int _pTahun, int _IDDInas, int pTahap)
        {
            List<RKA22Murni> lst = new List<RKA22Murni>();
            GetKolom(pTahap);
            SetProfileRekening(mprofile);
            try
            {

         
                SSQL = " Select A.IDUrusan, A.IDDInas,0 as IDProgram,0 as IDKegiatan,0 as KodeSubKegiatan,D.IsPokok as isPokok, 'Urusan ' + B.sNamaUrusan as Nama, '' as SumberPendanaan,'' as sLokasi,  " +

                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='51' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaPegawaiMurni, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5102' THEN A." + _namaKolom1 + " else 0 END ) AS BBJMurni, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5103' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBungaMurni, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5104' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaSubsidiMurni, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5105' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaHibahMurni, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5106' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBansosMurni, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaModalMurni, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='53' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaTTMurni, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5401' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBagiHasilMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5402' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBantuanKeuanganMurni, " +                        

                       "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")='5' THEN A." + _namaKolom2 + " else 0 END) AS Jumlah " +
                       " from tANggaranRekening_A A  " +
                       "INNER JOIN mUrusan  B ON A.IDUrusan= B.ID INNER JOIN MPelaksanaUrusan D ON D.IDurusan = B.ID  and A.iTahun = D.iTahun AND A.IDUrusan = D.IDurusan  and A.IDDInas = D.IDDInas  where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas =" + _IDDInas.ToString() +
                       "  GROUP BY A.IDUrusan, A.IDDInas,B.sNamaUrusan,D.IsPokok ";

                SSQL = SSQL + " UNION ALL Select A.IDUrusan, A.IDDInas,A.IDProgram,0 as IDKegiatan,0 as KodeSubKegiatan,D.IsPokok  as IsPokok, B.sNamaProgram as Nama, '' as SumberPendanaan,'' as sLokasi, " +

                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='51' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaPegawaiMurni, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5102' THEN A." + _namaKolom1 + " else 0 END ) AS BBJMurni, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5103' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBungaMurni, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5104' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaSubsidiMurni, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5105' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaHibahMurni, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5106' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBansosMurni, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaModalMurni, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='53' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaTTMurni, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5401' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBagiHasilMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5402' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBantuanKeuanganMurni, " +

                       "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")='5' THEN A." + _namaKolom2 + " else 0 END) AS Jumlah " +
                        " from tANggaranRekening_A A  INNER JOIN tPrograms_A B ON A.iTahun= B.iTahun and A.IDUrusan= B.IDUrusan " +
                        " and A.IDProgram= B.IDProgram  AND A.IDDInas= B.IDDInas INNER JOIN MPelaksanaUrusan D ON A.iTahun=D.iTahun AND B.iTahun = D.iTahun AND D.IDUrusan= A.IDurusan AND D.IDUrusan=B.IDurusan " +
                        " AND A.IDDInas = D.IDDInas and B.IDDInas = D.IDDinas where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas =" + _IDDInas.ToString() +
                        " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram,B.sNamaProgram,D.IsPokok  ";
                
                //SSQL = SSQL + " UNION ALL Select A.IDUrusan, A.IDDInas,A.IDProgram,0 as IDKegiatan,0 as KodeSubKegiatan,D.IsPokok  as IsPokok, B.sNamaProgram as Nama, '' as sLokasi, " +

                //            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5101' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaPegawaiMurni, " +
                //            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5102' THEN A." + _namaKolom1 + " else 0 END ) AS BBJMurni, " +
                //            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5103' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBungaMurni, " +
                //            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5104' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaSubsidiMurni, " +
                //            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5105' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaHibahMurni, " +
                //            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5106' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBansosMurni, " +
                //            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaModalMurni, " +
                //            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='53' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaTTMurni, " +
                //            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5401' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBagiHasilMurni, " +
                //            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5402' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBantuanKeuanganMurni, " +                        

                //        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom2 + " else 0 END) AS Jumlah " +
                //        " from tANggaranRekening_A A  INNER JOIN tPrograms_A B ON A.iTahun= B.iTahun and A.IDUrusan= B.IDUrusan " +
                //        " and A.IDProgram= B.IDProgram  AND A.IDDInas= B.IDDInas INNER JOIN MPelaksanaUrusan D ON A.iTahun=D.iTahun AND B.iTahun = D.iTahun AND D.IDUrusan= A.IDurusan AND D.IDUrusan=B.IDurusan " +
                //        " AND A.IDDInas = D.IDDInas and B.IDDInas = D.IDDinas where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas =" + _IDDInas.ToString() +
                //        " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram,B.sNamaProgram,D.IsPokok ";


                SSQL = SSQL + " UNION ALL Select A.IDUrusan, A.IDDInas,A.IDProgram,A.IDKegiatan,0  as KodeSubKegiatan, D.IsPokok as IsPokok,B.sNama as Nama, '' as SumberPendanaan, '' as sLokasi, " +

                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='51' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaPegawaiMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5102' THEN A." + _namaKolom1 + " else 0 END ) AS BBJMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5103' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBungaMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5104' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaSubsidiMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5105' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaHibahMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5106' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBansosMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaModalMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='53' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaTTMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5401' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBagiHasilMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5402' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBantuanKeuanganMurni, " +                        

                        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")='5' THEN A." + _namaKolom2 + " else 0 END) AS Jumlah from tANggaranRekening_A A " +
                        " INNER JOIN tKegiatan_A B ON A.iTahun= B.iTahun and A.IDUrusan= B.IDUrusan AND A.IDKegiatan= B.IDkegiatan " +
                        " AND A.IDDInas= B.IDDInas   INNER JOIN MPelaksanaUrusan D ON D.IDUrusan= A.IDurusan AND D.IDUrusan=B.IDurusan and D.iTAhun= A.ITahun AND D.iTahun=B.iTahun " +
                        " AND D.IDDInas= A.IDDInas AND D.IDDInas=B.IDDInas    where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas =" + _IDDInas.ToString() +
                          " GROUP BY A.IDUrusan,A.IDDInas,A.IDProgram,A.IDKegiatan, B.sNama , B.sLokasi,D.IsPokok";

                SSQL = SSQL + " UNION ALL Select A.IDUrusan, A.IDDInas,A.IDProgram,A.IDKegiatan,A.IDSubKegiatan  as KodeSubKegiatan, D.IsPokok as IsPokok,B.Nama as Nama, SumberPendanaan,Lokasi as sLokasi, " +

                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='51' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaPegawaiMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5102' THEN A." + _namaKolom1 + " else 0 END ) AS BBJMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5103' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBungaMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5104' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaSubsidiMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5105' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaHibahMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5106' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBansosMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaModalMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='53' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaTTMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5401' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBagiHasilMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5402' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBantuanKeuanganMurni, " +

                        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")='5' THEN A." + _namaKolom2 + " else 0 END) AS Jumlah from tANggaranRekening_A A " +
                        " INNER JOIN tSubKegiatan B ON A.iTahun= B.iTahun and A.IDUrusan= B.IDUrusan AND A.IDKegiatan= B.IDkegiatan " +
                        " AND A.IDDInas= B.IDDInas AND A.IDSubKegiatan = B.IDSUbKegiatan  INNER JOIN MPelaksanaUrusan D ON D.IDUrusan= A.IDurusan AND D.IDUrusan=B.IDurusan and D.iTAhun= A.ITahun AND D.iTahun=B.iTahun " +
                        " AND D.IDDInas= A.IDDInas AND D.IDDInas=B.IDDInas    where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas =" + _IDDInas.ToString() +
                          " GROUP BY A.IDUrusan,A.IDDInas,A.IDProgram,A.IDKegiatan, D.IsPokok,A.IDSubKegiatan,B.Nama ,SumberPendanaan,Lokasi ";
                //B.sNama , B.sLokasi
                SSQL = SSQL + " UNION ALL Select 999 as IDUrusan, A.IDDInas,1000 as IDProgram,1000 as IDKegiatan,0 as KodeSubKegiatan, 1 as IsPokok, ' J U M L A H ' as Nama, '' AS SumberPendanaan, '' as sLokasi, " +
                             "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='51' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaPegawaiMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5102' THEN A." + _namaKolom1 + " else 0 END ) AS BBJMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5103' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBungaMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5104' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaSubsidiMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5105' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaHibahMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5106' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBansosMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaModalMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='53' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaTTMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5401' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBagiHasilMurni, " +
                            "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='5402' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaBantuanKeuanganMurni, " +
                         " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")='5' THEN A." + _namaKolom2 + " else 0 END) AS Jumlah from tANggaranRekening_A A " +
                         "  inner join TkEGIATAN_a b On A.iTahun = B.iTahun and A.IDDInas = B.IDDInas and A.IDKegiatan = B.IDKegiatan and a.IDUrusan = B.IDurusan " +
                         "INNER JOIN MPelaksanaUrusan D ON D.IDUrusan= A.IDurusan AND D.IDUrusan=B.IDurusan   and D.iTAhun= A.ITahun AND D.iTahun=B.iTahun " +
                        " AND D.IDDInas= A.IDDInas AND D.IDDInas=B.IDDInas  where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas =" + _IDDInas.ToString() +
                           " GROUP by A.IDDInas ";

                SSQL = SSQL + " ORDER BY IDDInas,IsPokok, IDUrusan,IDProgram,IDKegiatan,KodeSubKegiatan";

                //SSQL = SSQL + " ORDER BY A.IDDInas,IsPokok, A.IDUrusan,A.IDProgram,A.IDKegiatan";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new RKA22Murni()
                               {

                                   IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                   IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                   IDkegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                   KodeProgram = GetKodeProgram(DataFormat.GetInteger(dr["IDProgram"]), DataFormat.GetInteger(dr["IDKegiatan"])),
                                   KodeKegiatan = GetKodeKegiatan(DataFormat.GetInteger(dr["IDKegiatan"])),
                                   KodeSubKegiatan = DataFormat.GetLong(dr["KodeSubKegiatan"])>0? DataFormat.GetLong(dr["KodeSubKegiatan"]).ToString().Substring(8):"",
                                   Nama = DataFormat.GetString(dr["Nama"]),
                                   Lokasi = DataFormat.GetString(dr["sLokasi"]),
                                   SumberPendanaan = DataFormat.GetString(dr["SumberPendanaan"]),
                                   //Target = GetTarget(2018, _IDDInas, DataFormat.GetInteger(dr["IDUrusan"]), DataFormat.GetInteger(dr["IDProgram"]), DataFormat.GetInteger(dr["IDKegiatan"])),
                                   BelanjaPegawai = DataFormat.GetDecimal(dr["BelanjaPegawaiMurni"]).ToRupiahInReport(),
                                   BelanjaBarangJasa = DataFormat.GetDecimal(dr["BBJMurni"]).ToRupiahInReport(),
                                   BelanjaModal = DataFormat.GetDecimal(dr["BelanjaModalMurni"]).ToRupiahInReport(),
                                   BelanjaBTTMurni = DataFormat.GetDecimal(dr["BelanjaTTMurni"]).ToRupiahInReport(),
                                   BelanjaBunga = DataFormat.GetDecimal(dr["BelanjaBungaMurni"]).ToRupiahInReport(),
                                   BelanjaSubsidi = DataFormat.GetDecimal(dr["BelanjaSubsidiMurni"]).ToRupiahInReport(),
                                   BelanjaHibah = DataFormat.GetDecimal(dr["BelanjaHibahMurni"]).ToRupiahInReport(),
                                   BelanjaBantuanSosial = DataFormat.GetDecimal(dr["BelanjaBansosMurni"]).ToRupiahInReport(),
                                   BelanjaBTT = DataFormat.GetDecimal(dr["BelanjaTTMurni"]).ToRupiahInReport(),
                                   BelanjaBagiHasil = DataFormat.GetDecimal(dr["BelanjaBagiHasilMurni"]).ToRupiahInReport(),
                                   BelanjaBantuanKeuangan = DataFormat.GetDecimal(dr["BelanjaBantuanKeuanganMurni"]).ToRupiahInReport(),
                                   Jumlah = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
                                   
                                   //BelanjaPegawaiMurni = DataFormat.GetDecimal(dr["BelanjaPegawaiMurni"]).ToRupiahInReport(),
                                   //BelanjaBarangJasaMurni = DataFormat.GetDecimal(dr["BelanjaBarangJasaMurni"]).ToRupiahInReport(),
                                   //BelanjaModalMurni = DataFormat.GetDecimal(dr["BelanjaModalMurni"]).ToRupiahInReport(),
                                   //JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport(),
                                   //Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                   //Persen = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]), DataFormat.GetDecimal(dr["JumlahMurni"]))

                               }).ToList();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }
        }
        public List<RKA22Murni> GetRekapRKA22GabunganMurni(int _pTahun, List<SKPD> lstSKPD, int pTahap)
        {
            List<RKA22Murni> lst = new List<RKA22Murni>();


            string strDinas = "(";
            foreach (SKPD d in lstSKPD)
            {
                strDinas = strDinas + d.ID.ToString() + ",";
            }
            strDinas = strDinas + "99)";
            GetKolom(pTahap);

            try
            {

         
                SSQL = " Select A.IDUrusan, 0 as IDProgram,0 as IDKegiatan,D.IsPokok as isPokok, 'Urusan ' + B.sNamaUrusan as Nama,  " +
                       "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaPegawaiMurni,  " +
                       "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaBarangJasaMurni,  " +
                       "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaModalMurni,  " +
                       "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom1 + " else 0 END) AS JumlahMurni ," +
                       "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom2 + " else 0 END ) AS BelanjaPegawai,  " +
                       "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaBarangJasa,  " +
                       "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaModal,  " +
                       "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom2 + " else 0 END) AS Jumlah " +
                       " from tANggaranRekening_A A  " +
                       "INNER JOIN mUrusan  B ON A.IDUrusan= B.ID INNER JOIN MPelaksanaUrusan D ON D.IDurusan = B.ID  and A.iTahun = D.iTahun AND A.IDUrusan = D.IDurusan  and A.IDDInas = D.IDDInas  where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas in " + strDinas +
                       "  GROUP BY A.IDUrusan, B.sNamaUrusan,D.IsPokok ";

                SSQL = SSQL + " UNION ALL Select A.IDUrusan, A.IDProgram,0 as IDKegiatan,D.IsPokok  as IsPokok, B.sNamaProgram as Nama, " +
                        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaPegawaiMurni, " +
                        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaBarangJasaMurni, " +
                        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaModalMurni,  " +
                        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom1 + " else 0 END) AS JumlahMurni, " +
                        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom2 + " else 0 END ) AS BelanjaPegawai, " +
                        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaBarangJasa, " +
                        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaModal,  " +
                        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom2 + " else 0 END) AS Jumlah " +
                        " from tANggaranRekening_A A  INNER JOIN tPrograms_A B ON A.iTahun= B.iTahun and A.IDUrusan= B.IDUrusan " +
                        " and A.IDProgram= B.IDProgram  AND A.IDDInas= B.IDDInas INNER JOIN MPelaksanaUrusan D ON A.iTahun=D.iTahun AND B.iTahun = D.iTahun AND D.IDUrusan= A.IDurusan AND D.IDUrusan=B.IDurusan " +
                        " AND A.IDDInas = D.IDDInas and B.IDDInas = D.IDDinas where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas in " + strDinas +
                        " GROUP BY A.IDUrusan, A.IDProgram,B.sNamaProgram,D.IsPokok  ";


                SSQL = SSQL + "UNION ALL Select A.IDUrusan, A.IDProgram,A.IDKegiatan, D.IsPokok as IsPokok,B.sNama as Nama, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaPegawaiMurni, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaBarangJasaMurni, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaModalMurni, " +
                        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom1 + " else 0 END) AS JumlahMurni, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom2 + " else 0 END ) AS BelanjaPegawai, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaBarangJasa, " +
                        "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaModal, " +
                        " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom2 + " else 0 END) AS Jumlah from tANggaranRekening_A A " +
                        " INNER JOIN tKegiatan_A B ON A.iTahun= B.iTahun and A.IDUrusan= B.IDUrusan AND A.IDKegiatan= B.IDkegiatan " +
                        " AND A.IDDInas= B.IDDInas   INNER JOIN MPelaksanaUrusan D ON D.IDUrusan= A.IDurusan AND D.IDUrusan=B.IDurusan and D.iTAhun= A.ITahun AND D.iTahun=B.iTahun " +
                        " AND D.IDDInas= A.IDDInas AND D.IDDInas=B.IDDInas    where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas  in " + strDinas  +
                          " GROUP BY A.IDUrusan,A.IDProgram,A.IDKegiatan, B.sNama , D.IsPokok ";

                SSQL = SSQL + "UNION ALL Select 999 as IDUrusan, 1000 as IDProgram,1000 as IDKegiatan, 1 as IsPokok, ' J U M L A H ' as Nama,  " +
                         "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom1 + " else 0 END ) AS BelanjaPegawaiMurni, " +
                         "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaBarangJasaMurni, " +
                         "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom1 + " else 0 END) AS BelanjaModalMurni, " +
                         " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom1 + " else 0 END) AS JumlahMurni, " +
                         "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN A." + _namaKolom2 + " else 0 END ) AS BelanjaPegawai, " +
                         "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaBarangJasa, " +
                         "SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN A." + _namaKolom2 + " else 0 END) AS BelanjaModal, " +
                         " SUM(case when Left(IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")='52' THEN A." + _namaKolom2 + " else 0 END) AS Jumlah from tANggaranRekening_A A " +
                         "  inner join TkEGIATAN_a b On A.iTahun = B.iTahun and A.IDDInas = B.IDDInas and A.IDKegiatan = B.IDKegiatan and a.IDUrusan = B.IDurusan " +
                         "INNER JOIN MPelaksanaUrusan D ON D.IDUrusan= A.IDurusan AND D.IDUrusan=B.IDurusan   and D.iTAhun= A.ITahun AND D.iTahun=B.iTahun " +
                        " AND D.IDDInas= A.IDDInas AND D.IDDInas=B.IDDInas  where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas  in " + strDinas  +
                           "";// GROUP by A.IDDInas ";

                SSQL = SSQL + " ORDER BY IsPokok, IDUrusan,IDProgram,IDKegiatan";

                //SSQL = SSQL + " ORDER BY A.IDDInas,IsPokok, A.IDUrusan,A.IDProgram,A.IDKegiatan";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new RKA22Murni()
                               {

                                   IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                   IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                   IDkegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                   KodeProgram = GetKodeProgram(DataFormat.GetInteger(dr["IDProgram"]), DataFormat.GetInteger(dr["IDKegiatan"])),
                                   KodeKegiatan = GetKodeKegiatan(DataFormat.GetInteger(dr["IDKegiatan"])),
                                   Nama = DataFormat.GetString(dr["Nama"]),
                                   Lokasi ="",
                                   Target = "",//GetTarget(Tahun, _IDDInas, DataFormat.GetInteger(dr["IDUrusan"]), DataFormat.GetInteger(dr["IDProgram"]), DataFormat.GetInteger(dr["IDKegiatan"])),
                                   BelanjaPegawai = DataFormat.GetDecimal(dr["BelanjaPegawai"]).ToRupiahInReport(),
                                   BelanjaBarangJasa = DataFormat.GetDecimal(dr["BelanjaBarangJasa"]).ToRupiahInReport(),
                                   BelanjaModal = DataFormat.GetDecimal(dr["BelanjaModal"]).ToRupiahInReport(),
                                   Jumlah = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
                                   BelanjaPegawaiMurni = DataFormat.GetDecimal(dr["BelanjaPegawaiMurni"]).ToRupiahInReport(),
                                   BelanjaBarangJasaMurni = DataFormat.GetDecimal(dr["BelanjaBarangJasaMurni"]).ToRupiahInReport(),
                                   BelanjaModalMurni = DataFormat.GetDecimal(dr["BelanjaModalMurni"]).ToRupiahInReport(),
                                   JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport(),
                                   Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                   Persen = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]),DataFormat.GetDecimal(dr["JumlahMurni"]) )

                               }).ToList();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }
        }
        private string GetTarget(int Tahun, int pIDDInas, int pIDUrusan, int pIDProgram, int idKegiatan, int tahap, List<SKPD> lstSKPD=null)
        {
            if (idKegiatan == 10215008)
                idKegiatan = 10215008;
            IndikatorLogic oLOgic = new IndikatorLogic(Tahun,mprofile);
            List<Indikator> ind = new List<Indikator>();
            string sTarget = "";
            ind = oLOgic.Get(Tahun, pIDDInas, pIDUrusan, pIDProgram, idKegiatan, tahap, lstSKPD);
            if (ind != null)
            {
                foreach (Indikator i in ind)
                {
                    {
                        if (i.iJenis == 3)
                        {
                            sTarget = i.Target;// TargetMurni;
                        }
                    }
                }
            }
            return sTarget;

        }


        public List<DPA22> GetDPA22Murni(int _pTahun, int _IDDInas, List<SKPD> lstSKPD=null )
        {
            List<DPA22> lst = new List<DPA22>();

            /*SSQL = "UPDATE tKegiatan_A SET T1 =SUM(cBUlan1 + cBulan2+ cBulan3, " +
                      " T2=  cBUlan4 + cBulan5 + cBulan6,  T3= cBUlan7 + cBulan8+ cBulan9, " +
                    " T4= cBUlan10 + cBulan11+ cBulan12 from tKegiatan_A A  inner join tAnggaranKas  B ON A.iTahun = B.iTahun and A.IDDinas = B.IDDinas and A.IDUrusan = b.IDUrusan and " +
                    " A.IDProgram = B.IDProgram and A.IDkegiatan = B.IDkegiatan ANd A.btJenis=B.btJenis  where A.IDDInas = " + _IDDInas.ToString() + " AND A.iTahun = " + _pTahun.ToString();// +" group by A.iTahun,A.IDDinas, A.IDProgram, A.IDurusan, A.IDKegiatan, A.sNama ";
            */

            string strDinas = "(";

            if (lstSKPD == null)
            {
                SSQL = "UPDATE tKegiatan_A SET T1 =BUlan1 + Bulan2+ Bulan3, " +
                   " T2=  BUlan4 + Bulan5 + Bulan6,  T3= BUlan7 + Bulan8+ Bulan9, " +
                 " T4= BUlan10 + Bulan11+ Bulan12 from tKegiatan_A A  inner join vwAnggaranKasPerKegiatan  B ON A.iTahun = B.iTahun and A.IDDinas = B.IDDinas and A.IDUrusan = b.IDUrusan and " +
                 " A.IDProgram = B.IDProgram and A.IDkegiatan = B.IDkegiatan ANd A.btJenis=B.btJenis " +
                 " where A.IDDInas = " + _IDDInas.ToString() + " AND A.iTahun = " + _pTahun.ToString();// +" group 
                _dbHelper.ExecuteNonQuery(SSQL);


                strDinas = strDinas + _IDDInas.ToString() + ",";
            }
            else
            {
                foreach (SKPD d in lstSKPD)
                {

                    SSQL = "UPDATE tKegiatan_A SET T1 =BUlan1 + Bulan2+ Bulan3, " +
                           " T2=  BUlan4 + Bulan5 + Bulan6,  T3= BUlan7 + Bulan8+ Bulan9, " +
                         " T4= BUlan10 + Bulan11+ Bulan12 from tKegiatan_A A  inner join vwAnggaranKasPerKegiatan  B ON A.iTahun = B.iTahun and A.IDDinas = B.IDDinas and A.IDUrusan = b.IDUrusan and " +
                         " A.IDProgram = B.IDProgram and A.IDkegiatan = B.IDkegiatan ANd A.btJenis=B.btJenis " +
                         " where A.IDDInas = " + d.ID.ToString() + " AND A.iTahun = " + _pTahun.ToString();// +" group 
                    _dbHelper.ExecuteNonQuery(SSQL);

                    ///_dbHelper.ExecuteNonQuery(SSQL);


                    strDinas = strDinas + d.ID.ToString() + ",";
                }
            }
            strDinas = strDinas + "99)";



            //SSQL = "UPDATE tKegiatan_A SET T1 =BUlan1 + Bulan2+ Bulan3, " +
            //            " T2=  BUlan4 + Bulan5 + Bulan6,  T3= BUlan7 + Bulan8+ Bulan9, " +
            //          " T4= BUlan10 + Bulan11+ Bulan12 from tKegiatan_A A  inner join vwAnggaranKasPerKegiatan  B ON A.iTahun = B.iTahun and A.IDDinas = B.IDDinas and A.IDUrusan = b.IDUrusan and " +
            //          " A.IDProgram = B.IDProgram and A.IDkegiatan = B.IDkegiatan ANd A.btJenis=B.btJenis " +
            //          " where A.IDDInas in " + strDinas + " AND A.iTahun = " + _pTahun.ToString();// +" group 

            
            //_dbHelper.ExecuteNonQuery(SSQL);



            try
            {
                if (lstSKPD!=null ) {



                    SSQL = " Select 1 as Level, A.IDUrusan, " + _IDDInas.ToString() + " as IDDInas,0 as IDProgram,0 as IDKegiatan, 'Urusan ' + B.sNamaUrusan as Nama, '' as sLokasi,  " +
                            "'' as sSUmberDana, SUM(A.T1) as TW1,SUM(A.T2) as TW2,SUM(A.T3) as TW3,SUM(A.T4) as TW4,SUM(A.T1 + A.T2 + A.T3 + A.T4 ) as  Jumlah  from tKegiatan_A A  " +
                            "INNER JOIN mUrusan  B ON A.IDUrusan= B.ID  where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas in " + strDinas +
                            "GROUP BY A.IDUrusan, B.sNamaUrusan ";


                    SSQL = SSQL + " UNION ALL Select 2 as Level,A.IDUrusan," + _IDDInas.ToString() + " as IDDInas,A.IDProgram,0 as IDKegiatan, B.sNamaProgram as Nama, '' as sLokasi,  " +
                            " '' as sSUmberDana, SUM(A.T1) as TW1,SUM(A.T2) as TW2,SUM(A.T3) as TW3,SUM(A.T4) as TW4,SUM(A.T1 + A.T2 + A.T3 + A.T4 ) as  Jumlah  from tKegiatan_A A  " +
                            "INNER JOIN tPrograms_A B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan and A.IDProgram= B.IDProgram and A.IDDInas = B.IDDInas where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas in " + strDinas +
                            " GROUP BY A.IDUrusan, A.IDProgram,B.sNamaProgram ";


                    SSQL = SSQL + " UNION ALL Select 3 as Level, A.IDUrusan, " + _IDDInas.ToString() + " as IDDInas,A.IDProgram,A.IDKegiatan, A.sNama as Nama, ' ' as sLokasi,  " +
                            " ' ' as sSUmberDana, SUM(A.T1 )as TW1,SUM(A.T2) as TW2,SUM(A.T3) as TW3,SUM(A.T4) as TW4, SUM(A.T1 + A.T2 + A.T3 + A.T4)  as  Jumlah  from tKegiatan_A A  " +
                            " where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas in " + strDinas + " group by A.IDUrusan,A.IDProgram,A.IDKegiatan, A.sNama ";

                    //        " ";//GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram,B.sNamaProgram ";
                    SSQL = SSQL + " UNION ALL Select 5 as Level, 999 as IDUrusan, 99999999 AS IDDInas,999 as IDProgram,999 as IDKegiatan, 'Jumlah'  as Nama, '' as sLokasi, '' as SumberDana, " +
                             " sum(A.T1) as TW1,SUM(A.T2) as TW2,SUM(A.T3) as TW3,SUM(A.T4) as TW4,SUM(A.T1 + A.T2 + A.T3 + A.T4 ) as  Jumlah  from tKegiatan_A A  " +
                             " where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas in " + strDinas +
                             "  AND A.T1 + A.T2 + A.T3 + A.T4 > 0 ";


                    SSQL = SSQL + " ORDER BY IDUrusan,IDProgram,IDKegiatan";
                }

                else
                {

                    SSQL = "";

                SSQL = " Select 1 as Level, A.IDUrusan, A.IDDInas,0 as IDProgram,0 as IDKegiatan, 'Urusan ' + B.sNamaUrusan as Nama, '' as sLokasi,  " +
                        "'' as sSUmberDana, SUM(A.T1) as TW1,SUM(A.T2) as TW2,SUM(A.T3) as TW3,SUM(A.T4) as TW4,SUM(A.T1 + A.T2 + A.T3 + A.T4 ) as  Jumlah  from tKegiatan_A A  " +
                        "INNER JOIN mUrusan  B ON A.IDUrusan= B.ID  where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas in " + strDinas +
                        "GROUP BY A.IDUrusan, A.IDDInas,B.sNamaUrusan ";


                SSQL = SSQL + " UNION ALL Select 2 as Level,A.IDUrusan, A.IDDInas,A.IDProgram,0 as IDKegiatan, B.sNamaProgram as Nama, '' as sLokasi,  " +
                        " '' as sSUmberDana, SUM(A.T1) as TW1,SUM(A.T2) as TW2,SUM(A.T3) as TW3,SUM(A.T4) as TW4,SUM(A.T1 + A.T2 + A.T3 + A.T4 ) as  Jumlah  from tKegiatan_A A  " +
                        "INNER JOIN tPrograms_A B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan and A.IDProgram= B.IDProgram and A.IDDInas = B.IDDInas where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas in " + strDinas +                        
                        " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram,B.sNamaProgram ";


                SSQL = SSQL + " UNION ALL Select 3 as Level, A.IDUrusan, A.IDDInas,A.IDProgram,A.IDKegiatan, A.sNama as Nama, A.sLokasi,  " +
                        " A.sSUmberDana, A.T1 as TW1,A.T2 as TW2,A.T3 as TW3,A.T4 as TW4,A.T1 + A.T2 + A.T3 + A.T4  as  Jumlah  from tKegiatan_A A  " +
                        " where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas in " + strDinas +
                        " ";//GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram,B.sNamaProgram ";
                SSQL = SSQL + " UNION ALL Select 5 as Level, 999 as IDUrusan, 99999999 AS IDDInas,999 as IDProgram,999 as IDKegiatan, 'Jumlah'  as Nama, '' as sLokasi, '' as SumberDana, " +
                         " sum(A.T1) as TW1,SUM(A.T2) as TW2,SUM(A.T3) as TW3,SUM(A.T4) as TW4,SUM(A.T1 + A.T2 + A.T3 + A.T4 ) as  Jumlah  from tKegiatan_A A  " +
                         " where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas in " + strDinas +
                         "  AND A.T1 + A.T2 + A.T3 + A.T4 > 0 ";
                       

                SSQL = SSQL + " ORDER BY IDDInas, IDUrusan,IDProgram,IDKegiatan";
                }
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new DPA22()
                               {
                                   Kode = DataFormat.GetInteger(dr["IDurusan"]).ToKodeUrusan(),

                                   Kode2 = DataFormat.GetInteger(dr["IDurusan"]).ToKodeUrusan() + "." + ( DataFormat.GetSingle(dr["Level"])==2?DataFormat.GetInteger(dr["IDProgram"]).ToSimpleKodeProgram(): DataFormat.GetInteger(dr["IDProgram"]).ToSimpleKodeProgram() + "." + DataFormat.GetInteger(dr["IDKegiatan"]).ToSimpleKodeKegiatan()),
                                   Level = DataFormat.GetSingle(dr["Level"]),
                                   TW1 = DataFormat.GetDecimal(dr["TW1"]).ToRupiahInReport(),
                                   TW2 = DataFormat.GetDecimal(dr["TW2"]).ToRupiahInReport(),
                                   TW3 = DataFormat.GetDecimal(dr["TW3"]).ToRupiahInReport(),
                                   TW4 = DataFormat.GetDecimal(dr["TW4"]).ToRupiahInReport(),
                                   Nama = DataFormat.GetString(dr["Nama"]),
                                   Lokasi =  DataFormat.GetString(dr["sLokasi"]),
                                   Target = GetTarget(Tahun, _IDDInas, DataFormat.GetInteger(dr["IDUrusan"]), DataFormat.GetInteger(dr["IDProgram"]), DataFormat.GetInteger(dr["IDKegiatan"]),1),
                                   SumberDana = DataFormat.GetString(dr["sSumberDana"]),
                                   Jumlah = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport()

                               }).ToList();         
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }
        }

        //public List<DPA22> GetDPA22Murni(int _pTahun, int _IDDInas)
        //{
        //    List<DPA22> lst = new List<DPA22>();

        //    SSQL = "UPDATE tKegiatan_A SET T1 =cBUlan1 + cBulan2+ cBulan3, " +
        //              " T2=  cBUlan4 + cBulan5 + cBulan6,  T3= cBUlan7 + cBulan8+ cBulan9, " +
        //            " T4= cBUlan10 + cBulan11+ cBulan12 from tKegiatan_A A  inner join tAnggaranKas  B ON A.iTahun = B.iTahun and A.IDDinas = B.IDDinas and A.IDUrusan = b.IDUrusan and " +
        //            " A.IDProgram = B.IDProgram and A.IDkegiatan = B.IDkegiatan ANd A.btJenis=B.btJenis  where A.IDDInas = " + _IDDInas.ToString() + " AND A.iTahun = " + _pTahun.ToString();// +" group by A.iTahun,A.IDDinas, A.IDProgram, A.IDurusan, A.IDKegiatan, A.sNama ";

        //    _dbHelper.ExecuteNonQuery(SSQL);



        //    try
        //    {
        //        SSQL = " Select 1 as Level, A.IDUrusan, A.IDDInas,0 as IDProgram,0 as IDKegiatan, 'Urusan ' + B.sNamaUrusan as Nama, '' as sLokasi,  " +
        //                " SUM(A.T1) as TW1,SUM(A.T2) as TW2,SUM(A.T3) as TW3,SUM(A.T4) as TW4,SUM(A.T1 + A.T2 + A.T3 + A.T4 ) as  Jumlah  from tKegiatan_A A  " +
        //                "INNER JOIN mUrusan  B ON A.IDUrusan= B.ID  where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas =" + _IDDInas.ToString() +
        //                "GROUP BY A.IDUrusan, A.IDDInas,B.sNamaUrusan ";


        //        SSQL = SSQL + " UNION ALL Select 2 as Level,A.IDUrusan, A.IDDInas,A.IDProgram,0 as IDKegiatan, B.sNamaProgram as Nama, '' as sLokasi,  " +
        //                " SUM(A.T1) as TW1,SUM(A.T2) as TW2,SUM(A.T3) as TW3,SUM(A.T4) as TW4,SUM(A.T1 + A.T2 + A.T3 + A.T4 ) as  Jumlah  from tKegiatan_A A  " +
        //                "INNER JOIN tPrograms_A B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan and A.IDProgram= B.IDProgram and A.IDDInas = B.IDDInas where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas =" + _IDDInas.ToString() +
        //                " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram,B.sNamaProgram ";


        //        SSQL = SSQL + " UNION ALL Select 3 as Level, A.IDUrusan, A.IDDInas,A.IDProgram,A.IDKegiatan, A.sNama as Nama, '' as sLokasi,  " +
        //                " A.T1 as TW1,A.T2 as TW2,A.T3 as TW3,A.T4 as TW4,A.T1 + A.T2 + A.T3 + A.T4  as  Jumlah  from tKegiatan_A A  " +
        //                " where A.btJenis=3 and a.iTahun =" + _pTahun.ToString() + " AND A.IDDInas =" + _IDDInas.ToString() +
        //                " ";//GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram,B.sNamaProgram ";

        //        SSQL = SSQL + " ORDER BY IDDInas, IDUrusan,IDProgram,IDKegiatan";

        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                lst = (from DataRow dr in dt.Rows
        //                       select new DPA22()
        //                       {
        //                           Kode = DataFormat.GetInteger(dr["IDurusan"]).ToKodeUrusan(),

        //                           Kode2 = DataFormat.GetSingle(dr["Level"]) == 2 ? DataFormat.GetInteger(dr["IDProgram"]).ToSimpleKodeProgram() : DataFormat.GetInteger(dr["IDKegiatan"]).ToSimpleKodeKegiatan(),
        //                           Level = DataFormat.GetSingle(dr["Level"]),
        //                           TW1 = DataFormat.GetDecimal(dr["TW1"]).ToRupiahInReport(),
        //                           TW2 = DataFormat.GetDecimal(dr["TW2"]).ToRupiahInReport(),
        //                           TW3 = DataFormat.GetDecimal(dr["TW3"]).ToRupiahInReport(),
        //                           TW4 = DataFormat.GetDecimal(dr["TW4"]).ToRupiahInReport(),
        //                           Nama = DataFormat.GetString(dr["Nama"]),
        //                           Lokasi = "",
        //                           Target = "", //DataFormat.GetString(dr["Nama"]),                                   
        //                           Jumlah = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport()

        //                       }).ToList();
        //            }
        //        }
        //        return lst;
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message;
        //        return lst;
        //    }
        //}
        public List<DPA22> GetDPA22ABT(int _pTahun, int _IDDInas, int _pTahap,List<SKPD> lstSKPD=null)
        {
            List<DPA22> lst = new List<DPA22>();


            GetKolom(_pTahap);

            string strDinas = "(";

            if (lstSKPD == null)
            {

                strDinas = strDinas + _IDDInas.ToString() + ",";
            }
            else
            {
                foreach (SKPD d in lstSKPD)
                {

             
                    strDinas = strDinas + d.ID.ToString() + ",";
                }
            }
            strDinas = strDinas + "99)";


            try
            {
                SSQL = " Select 1 as Level, B.ID as IDUrusan, 0 as IDProgram,0 as IDKegiatan, 0 as IDSUbKegiatan, 'Urusan ' + B.sNamaUrusan as Nama, '' as sLokasi,  " +
                        " SUM(C." + _namaKolom1 + ") as Murni,SUM(C." + _namaKolom2 + ") as Jumlah from " +
                        " mUrusan  B INNER JOIN tANggaranRekening_A C ON B.ID =C.IDUrusan where C.btJenis=3 and C.iTahun =" + _pTahun.ToString() + " AND C.IDDInas in" + strDinas +
                        "GROUP BY B.ID,B.sNamaUrusan ";

                SSQL = SSQL + " UNION ALL Select 2 as Level,B.IDUrusan, B.IDProgram,0 as IDKegiatan,0 as IDSUbKegiatan, B.sNamaProgram as Nama, '' as sLokasi,  " +
                        " SUM(C." + _namaKolom1 + ") as Murni,SUM(C." + _namaKolom2 + ") as Jumlah  from tPrograms_A B INNER JOIN tANggaranRekening_A C ON B.iTahun = C.iTahun AND B.IDDInas= C.IDDInas and B.IDUrusan =C.IDUrusan AND B.IDProgram=C.IDProgram where B.iTahun =" + _pTahun.ToString() + " AND B.IDDInas in " + strDinas +
                        " GROUP BY B.IDUrusan, B.IDProgram,B.sNamaProgram ";

                SSQL = SSQL + " UNION ALL Select 3 as Level, A.IDUrusan, A.IDProgram,A.IDKegiatan,0 as IDSUbKegiatan,  A.sNama as Nama,  A.sLokasi,  " +
                        "  SUM(C." + _namaKolom1 + ") as Murni,SUM(C." + _namaKolom2 + ") as Jumlah from tKegiatan_A A  " +
                        " INNER JOIN tANggaranRekening_A C ON A.iTahun = C.iTahun AND A.IDDInas= C.IDDInas and A.IDUrusan =C.IDUrusan AND A.IDKegiatan=C.IDKegiatan " +
                        " where A.btJenis=3 and A.iTahun =" + _pTahun.ToString() + " AND A.IDDInas in " + strDinas +
                        " GROUP BY A.IDUrusan, A.IDProgram,A.IDKegiatan,A.sNama,A.sLokasi";

                SSQL = SSQL + " UNION ALL Select 4 as Level, A.IDUrusan, A.IDProgram,A.IDKegiatan,A.IDSUbKegiatan, A.Nama as Nama,  A.sLokasi,  " +
                        "  SUM(C." + _namaKolom1 + ") as Murni,SUM(C." + _namaKolom2 + ") as Jumlah from tSubKegiatan A  " +
                        " INNER JOIN tANggaranRekening_A C ON A.iTahun = C.iTahun AND A.IDDInas= C.IDDInas and A.IDUrusan =C.IDUrusan AND A.IDKegiatan=C.IDKegiatan AND A.IDSubKegiatan=C.IDSubKegiatan " +
                        " where A.btJenis=3 and A.iTahun =" + _pTahun.ToString() + " AND A.IDDInas in " + strDinas +
                        " GROUP BY A.IDUrusan, A.IDProgram,A.IDKegiatan,A.IDsubKegiatan,A.Nama,A.sLokasi";

                SSQL = SSQL + " ORDER BY  IDUrusan,IDProgram,IDKegiatan,IDsubKegiatan";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new DPA22()
                               {
                                   Kode = DataFormat.GetInteger(dr["IDurusan"]).ToKodeUrusan(),

                                   Kode2 = GetKode22(DataFormat.GetInteger(dr["Level"]),
                                                         DataFormat.GetInteger(dr["IDProgram"]),                                                       
                                                         DataFormat.GetInteger(dr["IDKegiatan"]),
                                                         DataFormat.GetLong(dr["IDSubKegiatan"]))
                                                         ,
                                   Level = DataFormat.GetSingle(dr["Level"]),                          
                                   Nama = DataFormat.GetString(dr["Nama"]),
                                   Lokasi = DataFormat.GetString(dr["sLokasi"]),
                                   Target = GetTarget(Tahun, _IDDInas, DataFormat.GetInteger(dr["IDurusan"]), DataFormat.GetInteger(dr["IDProgram"]), DataFormat.GetInteger(dr["IDKegiatan"]), _pTahap,lstSKPD),
                                   Jumlah = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
                                   JumlahMurni = DataFormat.GetDecimal(dr["Murni"]).ToRupiahInReport(),
                                   //Selisih= DataFormat.GetSe
                                   Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["Murni"])).ToRupiahInReport(),
                                   Persen = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]),DataFormat.GetDecimal(dr["Murni"]))


                               }).ToList();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }
        }
        private string GetKode22(int level,int program, int Kegiatan, long subkegiatan)
        {
            if (level ==2 ){
                return program.ToSimpleKodeProgram() ;
            }

            if (level ==3 ){
                return program.ToSimpleKodeProgram() +"." + Kegiatan.ToSimpleKodeKegiatan (); 
            }

            if (level ==4 ){
                return program.ToSimpleKodeProgram() +"." +  Kegiatan.ToSimpleKodeKegiatan() + "." + subkegiatan.ToString().Substring(8);

            }
            return "";

        }
        public List<DPA22> GetDPA22ABTAsli_BeumGabungan(int _pTahun, int _IDDInas, int _pTahap, List<SKPD> lstSKPD = null)
        {
            List<DPA22> lst = new List<DPA22>();


            GetKolom(_pTahap);

            try
            {
                SSQL = " Select 1 as Level, B.ID as IDUrusan, 0 as IDProgram,0 as IDKegiatan, 'Urusan ' + B.sNamaUrusan as Nama, '' as sLokasi,  " +
                        " SUM(C." + _namaKolom1 + ") as Murni,SUM(C." + _namaKolom2 + ") as Jumlah from " +
                        " mUrusan  B INNER JOIN tANggaranRekening_A C ON B.ID =C.IDUrusan where C.btJenis=3 and C.iTahun =" + _pTahun.ToString() + " AND C.IDDInas =" + _IDDInas.ToString() +
                        "GROUP BY B.ID,B.sNamaUrusan ";


                SSQL = SSQL + " UNION ALL Select 2 as Level,B.IDUrusan, B.IDProgram,0 as IDKegiatan, B.sNamaProgram as Nama, '' as sLokasi,  " +
                        " SUM(C." + _namaKolom1 + ") as Murni,SUM(C." + _namaKolom2 + ") as Jumlah  from tPrograms_A B INNER JOIN tANggaranRekening_A C ON B.iTahun = C.iTahun AND B.IDDInas= C.IDDInas and B.IDUrusan =C.IDUrusan AND B.IDProgram=C.IDProgram where B.iTahun =" + _pTahun.ToString() + " AND B.IDDInas =" + _IDDInas.ToString() +
                        " GROUP BY B.IDUrusan, B.IDProgram,B.sNamaProgram ";


                SSQL = SSQL + " UNION ALL Select 3 as Level, A.IDUrusan, A.IDProgram,A.IDKegiatan, A.sNama as Nama, '' as sLokasi,  " +
                        "  SUM(C." + _namaKolom1 + ") as Murni,SUM(C." + _namaKolom2 + ") as Jumlah from tKegiatan_A A  " +
                        " INNER JOIN tANggaranRekening_A C ON A.iTahun = C.iTahun AND A.IDDInas= C.IDDInas and A.IDUrusan =C.IDUrusan AND A.IDKegiatan=C.IDKegiatan " +
                        " where A.btJenis=3 and A.iTahun =" + _pTahun.ToString() + " AND A.IDDInas =" + _IDDInas.ToString() +
                        " GROUP BY A.IDUrusan, A.IDProgram,A.IDKegiatan,A.sNama";

                SSQL = SSQL + " ORDER BY  IDUrusan,IDProgram,IDKegiatan";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new DPA22()
                               {
                                   Kode = DataFormat.GetInteger(dr["IDurusan"]).ToKodeUrusan(),

                                   Kode2 = DataFormat.GetSingle(dr["Level"]) == 2 ? DataFormat.GetInteger(dr["IDProgram"]).ToSimpleKodeProgram() : DataFormat.GetInteger(dr["IDKegiatan"]).ToSimpleKodeKegiatan(),
                                   Level = DataFormat.GetSingle(dr["Level"]),
                                   Nama = DataFormat.GetString(dr["Nama"]),
                                   Lokasi = "",
                                   Target = "", //DataFormat.GetString(dr["Nama"]),                                   
                                   Jumlah = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
                                   JumlahMurni = DataFormat.GetDecimal(dr["Murni"]).ToRupiahInReport(),
                                   //Selisih= DataFormat.GetSe
                                   Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["Murni"])).ToRupiahInReport(),
                                   Persen = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]), DataFormat.GetDecimal(dr["Murni"]))


                               }).ToList();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }
        }

        private string GetKodeKegiatan(int x)
        {
            if (x > 0)
            {
                if (x.ToString().Length > 5)
                {
                    return x.ToString().Substring(5, 3);
                }
                else return "";
            }
            else return "";

        }
        private string GetKodeProgram(int p, int x)
        {
            //if (x > 0)
            //{
            //    return "";
            //}
                
            //else
                if (p.ToString().Length >3)
                {
                    return p.ToString().Substring(3);
                } return "";

        }

    }
}
