using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DTO.Akuntansi;
using Formatting;
using DataAccess;
using System.Data;
using BP;


namespace BP.Akuntansi
{
    public class rptPerdaLogic: BP 
    {
        private int mprofile;

        public rptPerdaLogic(int _pTahun, int _p = 0, int profile = 2)
            : base(_pTahun, _p, profile)
        {
            Tahun = _pTahun;
            mprofile = profile;
            SetProfileRekening(mprofile);
            //  PrepareFunction();
            //  CekViewAnggaranAllLevel();

        }

        public List<ClsPosisiKas> GetPosisiKas(int bulan)
        {
            List<ClsPosisiKas> _lst = new List<ClsPosisiKas>();

            string SSQL = "";

            SSQL = " Select  ItemPosisiKas.*,  (select jumlah from PosisiKas where IDItem = ";
            SSQL = SSQL + "  ItemPosisiKas.IDItem and bulan = " + bulan.ToString() + ") as Jumlah  ";
            SSQL = SSQL + "  from ItemPosisiKas  ";
            SSQL = SSQL + "  order by ItemPosisiKas.kelompok,ItemPosisiKas .IDItem";




            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    _lst = (from DataRow dr in dt.Rows
                            select new ClsPosisiKas()
                            {
                                //public string No { set; get; }
                                Nama = DataFormat.GetString(dr["Nama"]),
                                Root = DataFormat.GetInteger(dr["Parent"]),
                                Label = DataFormat.GetString(dr["Label"]),
                                Isi = DataFormat.GetInteger(dr["Isi"]),
                                Kelompok = DataFormat.GetInteger(dr["Kelompok"]),
                                Jumlah = DataFormat.GetDecimal(dr["jumlah"]),
                                JudulKelompok = GetJudulKelompok(DataFormat.GetInteger(dr["Kelompok"])),



                            }).ToList();

                }
            }
            return _lst;



        }

        public List<EMBelanja> GetEarnMarked(int jenis)
        {
            List<EMBelanja> _lst = new List<EMBelanja>();
            string SSQL = "";
            SSQL = "Select * From fnEMBElanja (" + jenis.ToString() + ")";
            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    _lst = (from DataRow dr in dt.Rows
                            select new EMBelanja()
                            {
                                Kegiatan = DataFormat.GetString(dr["NamaKegiatan"]),
                                SubKegiatan = DataFormat.GetString(dr["NamaSubKegiatan"]),
                                Anggaran = DataFormat.GetDecimal(dr["Anggaran"]),
                                Tahap1 = DataFormat.GetDecimal(dr["Tahap1"]),
                                Tahap2 = DataFormat.GetDecimal(dr["Tahap2"]),
                                Tahap3 = DataFormat.GetDecimal(dr["Tahap3"]),
                                //public decimal JumlahBelanja { set; get; }
                                Keluaran = DataFormat.GetString(dr["Keluaran"]),
                                SatuanTarget = DataFormat.GetString(dr["SatuanTarget"]),
                                Volume = DataFormat.GetInteger(dr["Volume"])
                            }).ToList();
                }
            }
            return _lst;

        }
        public List<EMPendapatan> GetEarnMarkedPenarimaan(int jenis)
        {
            List<EMPendapatan> _lst = new List<EMPendapatan>();
            string SSQL = "";
            SSQL = "Select * From dbo.fnEMPendapatan (" + jenis.ToString() + ")";
            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    _lst = (from DataRow dr in dt.Rows
                            select new EMPendapatan()
                            {

                                Jumlah = DataFormat.GetDecimal(dr["Jumlah"]),

                            }).ToList();
                }
            }
            return _lst;

        }


        public List<ClsPosisiKas> GetPerkiraanBelanja(int bulan)
        {
            List<ClsPosisiKas> _lst = new List<ClsPosisiKas>();

            string SSQL = "";

            SSQL = " Select  ItemPosisiKas.*,  (select jumlah from PosisiKas where IDItem = ";
            SSQL = SSQL + "  ItemPosisiKas.IDItem and bulan = " + bulan.ToString() + ") as Jumlah  ";
            SSQL = SSQL + "  from ItemPosisiKas  ";
            SSQL = SSQL + "  order by ItemPosisiKas.kelompok,ItemPosisiKas .IDItem";




            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    //_lst = (from DataRow dr in dt.Rows
                    //            select new AnggaranKas()
                    //            {


                    _lst = (from DataRow dr in dt.Rows
                            select new ClsPosisiKas()
                            {
                                //public string No { set; get; }
                                Nama = DataFormat.GetString(dr["Nama"]),
                                Root = DataFormat.GetInteger(dr["Parent"]),
                                Label = DataFormat.GetString(dr["Label"]),
                                Isi = DataFormat.GetInteger(dr["Isi"]),
                                Kelompok = DataFormat.GetInteger(dr["Kelompok"]),
                                Jumlah = DataFormat.GetDecimal(dr["jumlah"]),
                                JudulKelompok = GetJudulKelompok(DataFormat.GetInteger(dr["Kelompok"]))


                            }).ToList();


                }
            }
            return _lst;
        }
        private string GetJudulKelompok(int kelompok)
        {
            if (kelompok == 1)
                return "POSISI KAS DAN SETARA KAS";
            if (kelompok == 2)
                return "SILPA TAHUN LALU YANG BERSUMRER DARI DANA EARMARKED";
            if (kelompok == 3)
                return "INFORMASI I,AINYA";

            return "";
        }
        private void CekViewAnggaranAllLevel()
        {
            // SetProfileRekening();
            SSQL = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwAnggaranAllLevel]')) " +
                " DROP VIEW [dbo].[vwAnggaranAllLevel]";

            _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = " CREATE VIEW vwAnggaranAllLevel AS " +
                "Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, A.IIDRekening as Rek,b.IIDRekening, b.btRoot as Root,b.sNamaRekening ,  A.cPlafon AS JumlahOlah,A.cJumlah AS Jumlah, A.cJumlahMurni AS JumlahMurni ,b.iDebet as Debet  " +
                " FROM tAnggaranRekening_A A inner join mRekening b on a.IIDRekening=b.IIDRekening    where b.btRoot=5  " +
                " UNION ALL  " +
                  " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + " ) as Rek,b.IIDRekening,b.btRoot as Root, b.sNamaRekening ,  SUM(A.cPlafon) as JumlahOlah,SUM(A.cJumlah) AS JUMLAH, SUM(A.cJumlahMurni) AS JumlahMurni ,b.iDebet as Debet  " +
                  " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")  where b.btRoot=4  " +
                  " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening,5), b.btRoot ,b.IIDRekening, b.sNamaRekening ,b.iDebet " +
                  " UNION ALL  " +
                  " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root,b.sNamaRekening ,  SUM(A.cPlafon ) as JumlahOlah,SUM(A.cJumlah) AS JUMLAH, SUM(A.cJumlahMurni) AS JumlahMurni ,b.iDebet as Debet " +
                  " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")  where b.btRoot=3  " +
                  " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet  " +
                  " UNION ALL  " +
                  " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root,b.sNamaRekening ,  SUM(A.cPlafon ) as JumlahOlah,SUM(A.cJumlah) AS JUMLAH, SUM(A.cJumlahMurni) AS JumlahMurni ,b.iDebet as Debet  " +
                  " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN2.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN2.ToString() + ")   where b.btRoot=2  " +
                  " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet  " +
                  " UNION ALL  " +
                  " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root,b.sNamaRekening ,  SUM(A.cPlafon ) as JumlahOlah,SUM(A.cJumlah) AS JUMLAH, SUM(A.cJumlahMurni) AS JumlahMurni ,b.iDebet as Debet  " +
                  " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")    where b.btRoot=1 " +
                  " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet";

            _dbHelper.ExecuteNonQuery(SSQL);

        }

        public List<Perda11> GetPerdaXI(ParameterLaporan _p)
        {
            List<Perda11> _lst = new List<Perda11>();
            try
            {
                GetKolom(_p.Tahap);
                SSQL = "select IDUrusan, IDDInas, IDKegiatan, snama, cAnggaranTahunlalu, cRealisasi, APBDPlalu," +
                        "(Select SUM(" + _namaKolom1 + ") from tAnggaranRekening_A WHERE itahun = A.iTahun and iDurusan = A.IDurusan " +
                        " and IDDInas = A.IDDInas and IDProgram =  A.idprogram and IDKegiatan = A.IDKegiatan) as APBDSekarang from " +
                        " tKegiatan_A A where iTahun =" + _p.Tahun.ToString() + " AND cAnggaranTahunlalu > 0 and bLanjutan =1 " +
                         " ORDER BY IDUrusan, IDDInas, IDProgram ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Perda11()
                                {
                                    //public string No { set; get; }
                                    NAma = DataFormat.GetString(dr["sNama"]),
                                    Kode = DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan() + "." + DataFormat.GetInteger(dr["IDDInas"]).ToKodeDinas(),
                                    APBDLalu = DataFormat.GetDecimal(dr["cAnggaranTahunlalu"]).ToRupiahInReport(),
                                    APBDPLalu = DataFormat.GetDecimal(dr["APBDPLAlu"]).ToRupiahInReport(),
                                    Realisasi = DataFormat.GetDecimal(dr["cRealisasi"]).ToRupiahInReport(),
                                    APBDKini = DataFormat.GetDecimal(dr["APBDSekarang"]).ToRupiahInReport(),
                                    Perubahan = "0"

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
        public List<PerdaII> GetPerdaIIBUrutOrg(ParameterLaporan _p)
        {
            List<PerdaII> _lst = new List<PerdaII>();
            try
            {
                // Cek View 
                // CreateViewPerdaII(_p.Tahap);

                // var query = from q in lstDB  group by ()

                string namaView = CreateViewPerdaII_B(_p, true);





                SSQL = " select 1 AS kelompok,  B.IDDInas/100  as dinas , 0   as Urutan,0 as Pokok,0 as IDUrusan, 0 As IDDinas, A.sNamaSKPD as Nama,  " +
                   " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni, " +
                   " SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni, " +
                   " SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni " +
                   " from mSKPD A INNER JOIN " + namaView + " B ON A.ID/100= B.IDDinas/100  " +
                   " inner join tKegiatan_A ON B.iTahun= tKegiatan_A.iTahun and B.IDDInas= tKegiatan_A.IDDInas and B.IDUrusan=tKegiatan_A.IDUrusan and " +
                    " B.IDKegiatan = tKegiatan_A.IDKegiatan  AND tKegiatan_A.btJenis = B.btJenis WHERE B.IDUrusan< 499 AND a.Root = 1  AND B.iTahun = " + _p.Tahun.ToString() +
                   " group by B.IDDInas/100,  A.sNamaSKPD  ";

                SSQL = SSQL + " UNION ALL select 2 AS kelompok, D.ID/100  as Dinas,A.ID  as Urutan ,E.IsPokok as Pokok,B.IDurusan AS IDUrusan,0 as IDDInas, C.sNamaKategori + ' ' + A.sNamaUrusan as Nama  , " +
                       " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni, " +
                       " SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni, " +
                       " SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni " +
                       " from mUrusan A INNER JOIN " + namaView + " B ON A.ID= B.IDUrusan Inner join mKategori C ON A.btKodekategori = C.btKodekategori  " +
                       " INNER JOIN mSKPD D ON B.IDDInas/100 = D.ID/100  " +
                       " INNER JOIN mPelaksanaUrusan E ON E.IDDInas/100 = B.IDDinas/100 AND E.IDUrusan= B.IDUrusan AND E.IDDinas/100=D.ID/100 " +
                       " inner join tKegiatan_A ON B.iTahun= tKegiatan_A.iTahun and  B.IDDInas= tKegiatan_A.IDDInas and B.IDUrusan=tKegiatan_A.IDUrusan and " +
                        " B.IDKegiatan = tKegiatan_A.IDKegiatan AND tKegiatan_A.btJenis = B.btJenis WHERE A.ID < 499 and B.iTahun = " + _p.Tahun.ToString() + " and d.Root=1" +
                       " GROUP BY  d.ID/100, a.id, B.IDurusan,C.sNamaKategori,A.sNamaUrusan,E.IsPokok ";


                SSQL = SSQL + " UNION ALL  " +
                      " select 5 AS kelompok, 55555 as dinas, 999 as Urutan,0 as Pokok ,0 AS IDUrusan, 0 As IDDinas, 'Jumlah ' as Nama,  " +
                      " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni, " +
                      " SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni, " +
                      " SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni " +
                      " from " + namaView + " B " +
                      " inner join mUrusan ON  mUrusan.ID= B.IDUrusan  inner join mSKPD ON mSKPD.ID= B.IDDInas  " +
                      " inner join tKegiatan_A ON B.iTahun= tKegiatan_A.iTahun and  B.IDDInas= tKegiatan_A.IDDInas and B.IDUrusan=tKegiatan_A.IDUrusan and " +
                        " B.IDKegiatan = tKegiatan_A.IDKegiatan  AND tKegiatan_A.btJenis = B.btJenis  WHERE  B.iTahun = " + _p.Tahun.ToString();


                SSQL = SSQL + " UNION ALL  " +
                        " select 6 AS kelompok, 66666 as dinas, 999 as Urutan ,0 as Pokok,0 AS IDUrusan, 0 As IDDinas, 'SURPLUS/DEFISIT ' as Nama,  " +
                        " (SELECT SUM (PendapatanMUrni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + ")  -" +
                        " (SELECT SUM (BTLMUrni)  from " + namaView + "  WHERE iTahun = " + _p.Tahun.ToString() + " )  - " +
                        "  (SELECT SUM (BLMUrni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + " )  as SUMPendapatan , 0 as SUMPendapatanMUrni, " +
                        " 0 as SUMBTL , 0  as SUMBTLMUrni, " +
                                      " 0 as SUMBL , 0 as SUMBLMUrni   Order by dinas, Urutan,Pokok,IDUrusan ";







                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaII()
                                {

                                    //Kode { set; get; }
                                    Level = DataFormat.GetInteger(dr["Kelompok"]),
                                    //IDKategori { set; get; }
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]).IntToStringWithLeftPad(2),
                                    IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
                                    //root { set; get; }
                                    Kode = DataFormat.GetInteger(dr["urutan"]) == 0 ? DataFormat.GetInteger(dr["dinas"]).ToKodeDinas() : (DataFormat.GetInteger(dr["urutan"]) > 0 && DataFormat.GetInteger(dr["urutan"]) < 999 ? DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan() : " "),
                                    Nama = DataFormat.GetString(dr["Nama"]).ToUpper(),
                                    Pendapatan = DataFormat.GetDecimal(dr["SumPendapatan"]).ToRupiahInReport(),
                                    BelanjaTidakLangsung = DataFormat.GetDecimal(dr["SumBTL"]).ToRupiahInReport(),
                                    BelanjaLangsung = DataFormat.GetDecimal(dr["SumBL"]).ToRupiahInReport(),
                                    JumlahBelanja = (DataFormat.GetDecimal(dr["SumBTL"]) + DataFormat.GetDecimal(dr["SumBL"])).ToRupiahInReport(),
                                    PendapatanMurni = DataFormat.GetDecimal(dr["SumPendapatanMurni"]).ToRupiahInReport(),
                                    BelanjaTidakLangsungMurni = DataFormat.GetDecimal(dr["SumBTLMurni"]).ToRupiahInReport(),
                                    BelanjaLangsungMurni = DataFormat.GetDecimal(dr["SumBLMurni"]).ToRupiahInReport(),
                                    JumlahBelanjaMurni = (DataFormat.GetDecimal(dr["SumBTLMurni"]) + DataFormat.GetDecimal(dr["SumBLMurni"])).ToRupiahInReport()

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

        public List<PerdaII> GetPerdaIIBUrutOrg90(ParameterLaporan _p)
        {
            List<PerdaII> _lst = new List<PerdaII>();
            try
            {
                // Cek View 
                // CreateViewPerdaII(_p.Tahap);

                // var query = from q in lstDB  group by ()

                string namaView = CreateViewPerdaII_B90(_p, true);





                SSQL = " select 1 as kelompok,B.IDUrusan /100 AS Kategori,  0  as dinas , 0   as Urutan,0 as Pokok,0 as IDUrusan, 0 As IDDinas, A.sNamakategori as Nama,  " +
                   " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni, " +
                   " SUM (BO) as SUMBO , SUM (BOMUrni) as SUMBOMUrni, " +
                   " SUM (BM) as SUMBM , SUM (BMMUrni) as SUMBMMUrni, " +
                   " SUM (BTT) as SUMBTT , SUM (BTTMUrni) as SUMBTTMUrni, " +
                   " SUM (TRF) as SUMTRF , SUM (TRFMUrni) as SUMTRFMUrni " +
                   " from mKategori A INNER JOIN " + namaView + " B ON A.btkodekategori= B.IDUrusan/100  " +
                   " inner join TSubKegiatan t  ON B.iTahun= t.iTahun and B.IDDInas= t.IDDInas and B.IDUrusan=t.IDUrusan and " +
                    " B.IDKegiatan = t.IDKegiatan   aNd B.IDsubKegiatan = t.IDsubKegiatan  AND t.btJenis = B.btJenis WHERE B.IDUrusan< 999 AND B.iTahun = " + _p.Tahun.ToString();
                if (_p.Tahap == -1 && _p.Tahun == 2021)
                {
                    SSQL = SSQL + " AND B.IIDRekening not in ( 51050701001,54020501001,54010101001,51030101001,51050503001 ) ";
                }
                SSQL = SSQL + " group by B.idurusan/100,  A.sNamakategori  ";


                SSQL = SSQL + " UNION select 3 as kelompok, B.IDUrusan /100 AS Kategori,  B.IDDInas/100  as dinas , 0   as Urutan,0 as Pokok,B.IDUrusan as IDUrusan, 0 As IDDinas, A.sNamaSKPD as Nama,  " +
                   " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni, " +
                   " SUM (BO) as SUMBO , SUM (BOMUrni) as SUMBOMUrni, " +
                   " SUM (BM) as SUMBM , SUM (BMMUrni) as SUMBMMUrni, " +
                   " SUM (BTT) as SUMBTT , SUM (BTTMUrni) as SUMBTTMUrni, " +
                   " SUM (TRF) as SUMTRF , SUM (TRFMUrni) as SUMTRFMUrni " +
                   " from mSKPD A INNER JOIN " + namaView + " B ON A.ID/100= B.IDDinas/100  " +
                   " inner join TSubKegiatan t  ON B.iTahun= t.iTahun and B.IDDInas= t.IDDInas and B.IDUrusan=t.IDUrusan and " +
                    " B.IDKegiatan = t.IDKegiatan   aNd B.IDsubKegiatan = t.IDsubKegiatan  AND t.btJenis = B.btJenis WHERE B.IDUrusan< 999 AND a.Root = 1  AND B.iTahun = " + _p.Tahun.ToString();

                if (_p.Tahap == -1 && _p.Tahun == 2021)
                {
                    SSQL = SSQL + " AND B.IIDRekening not in ( 51050701001,54020501001,54010101001,51030101001,51050503001 ) ";
                }
                SSQL = SSQL + " group by B.IDUrusan/100 ,  B.IDUrusan,B.IDDInas/100,  A.sNamaSKPD  ";

                SSQL = SSQL + " UNION ALL select 2 as kelompok, B.IDUrusan /100 AS Kategori, 0 as Dinas,A.ID  as Urutan ,0 Pokok, B.IDurusan AS IDUrusan,0 as IDDInas,  A.sNamaUrusan as Nama  , " +
                   " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni, " +
                   " SUM (BO) as SUMBO , SUM (BOMUrni) as SUMBOMUrni, " +
                   " SUM (BM) as SUMBM , SUM (BMMUrni) as SUMBMMUrni ," +
                   " SUM (BTT) as SUMBTT , SUM (BTTMUrni) as SUMBTTMUrni, " +
                   " SUM (TRF) as SUMTRF , SUM (TRFMUrni) as SUMTRFMUrni " +
                       " from mUrusan A INNER JOIN " + namaView + " B ON A.ID= B.IDUrusan  " +
                       " INNER JOIN mSKPD D ON B.IDDInas/100 = D.ID/100  " +
                       " INNER JOIN mPelaksanaUrusan E ON E.IDDInas/100 = B.IDDinas/100 AND E.IDUrusan= B.IDUrusan AND E.IDDinas/100=D.ID/100 " +
                       " inner join tSubkegiatan t ON B.iTahun= t.iTahun and  B.IDDInas=t.IDDInas and B.IDUrusan=t.IDUrusan and " +
                        " B.IDKegiatan = t.IDKegiatan AND t.btJenis = B.btJenis  aNd B.IDsubKegiatan = t.IDsubKegiatan   WHERE A.ID < 999 and B.iTahun = " + _p.Tahun.ToString() + " and d.Root=1";
                if (_p.Tahap == -1 && _p.Tahun == 2021)
                {
                    SSQL = SSQL + " AND B.IIDRekening not in ( 51050701001,54020501001,54010101001,51030101001,51050503001 ) ";
                }
                SSQL = SSQL + " GROUP BY  B.IDUrusan/100, a.id, B.IDurusan,A.sNamaUrusan ";


                //SSQL = SSQL + " UNION ALL  " +
                //      " select 5 AS kelompok, 9999900 as dinas, 999 as Urutan,0 as Pokok ,0 AS IDUrusan, 0 As IDDinas, 'Jumlah ' as Nama,  " +
                //    " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni, " +
                //   " SUM (BO) as SUMBO , SUM (BOMUrni) as SUMBOMUrni, " +
                //   " SUM (BM) as SUMBM , SUM (BMMUrni) as SUMBMMUrni, " +
                //   " SUM (BTT) as SUMBTT , SUM (BTTMUrni) as SUMBTTMUrni, " +
                //   " SUM (TRF) as SUMTRF , SUM (TRFMUrni) as SUMTRFMUrni " + " from " + namaView + " B " +
                //      " inner join mUrusan ON  mUrusan.ID= B.IDUrusan  inner join mSKPD ON mSKPD.ID= B.IDDInas  " +
                //      " inner join TSubkegiatan t ON B.iTahun= t.iTahun and  B.IDDInas= t.IDDInas and B.IDUrusan=T.IDUrusan and " +
                //        " B.IDKegiatan = t.IDKegiatan  aNd B.IDsubKegiatan = t.IDsubKegiatan    AND T.btJenis = B.btJenis  WHERE  B.iTahun = " + _p.Tahun.ToString();


                SSQL = SSQL + " UNION ALL  " +
                  " select 6 AS kelompok,999 as kategori ,9999999 as dinas, 999 as Urutan,0 as Pokok ,0 AS IDUrusan, 0 As IDDinas, 'SURPLUS/DEFISIT' as Nama,  " +
                " SUM (B.Pendapatan)  - SUM (BO) - SUM (BM) -SUM (BTT)-SUM (TRF)  as SUMPendapatan , SUM (PendapatanMUrni) - SUM (BOMUrni) -SUM (BMMUrni) -SUM (BTTMUrni)-SUM (TRFMUrni) as SUMPendapatanMUrni, " +
               " SUM (BO) as SUMBO , SUM (BOMUrni) as SUMBOMUrni, " +
               " SUM (BM) as SUMBM , SUM (BMMUrni) as SUMBMMUrni, " +
               " SUM (BTT) as SUMBTT , SUM (BTTMUrni) as SUMBTTMUrni, " +
               " SUM (TRF) as SUMTRF , SUM (TRFMUrni) as SUMTRFMUrni " + " from " + namaView + " B " +
                    //    " inner join mUrusan ON  mUrusan.ID= B.IDUrusan  inner join mSKPD ON mSKPD.ID= B.IDDInas  " +
                  " inner join TSubkegiatan t ON B.iTahun= t.iTahun and  B.IDDInas= t.IDDInas and B.IDUrusan=T.IDUrusan and " +
                    " B.IDKegiatan = t.IDKegiatan  aNd B.IDsubKegiatan = t.IDsubKegiatan    AND T.btJenis = B.btJenis  WHERE  B.iTahun = " + _p.Tahun.ToString();


                //SSQL = SSQL + " UNION ALL  " +
                //        " select 6 AS kelompok, 66666 as dinas, 999 as Urutan ,0 as Pokok,0 AS IDUrusan, 0 As IDDinas, 'SURPLUS/DEFISIT ' as Nama,  " +
                //        " (SELECT SUM (PendapatanMUrni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + ")  -" +
                //        " (SELECT SUM (BTLMUrni)  from " + namaView + "  WHERE iTahun = " + _p.Tahun.ToString() + " )  - " +
                //        "  (SELECT SUM (BLMUrni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + " )  as SUMPendapatan , 0 as SUMPendapatanMUrni, " +
                //        " 0 as SUMBTL , 0  as SUMBTLMUrni, " +
                //                      " 0 as SUMBL , 0 as SUMBLMUrni   Order by dinas, Urutan,Pokok,IDUrusan ";
                SSQL = SSQL + " Order by Kategori,IDUrusan, dinas, Urutan,Pokok";






                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaII()
                                {

                                    //Kode { set; get; }
                                    Level = DataFormat.GetInteger(dr["Kelompok"]),
                                    //IDKategori { set; get; }
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]).IntToStringWithLeftPad(2),
                                    IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
                                    //root { set; get; }
                                    Kode = GetKode(DataFormat.GetInteger(dr["Kelompok"]), DataFormat.GetInteger(dr["Kategori"]),
                                                    DataFormat.GetInteger(dr["IDUrusan"]), DataFormat.GetInteger(dr["dinas"])), //DataFormat.GetInteger(dr["Kategori"]) < 999 ? DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan() : " "),

                                    Nama = DataFormat.GetString(dr["Nama"]).ToUpper(),
                                    Pendapatan = DataFormat.GetDecimal(dr["SumPendapatan"]).ToRupiahInReport(),
                                    PendapatanMurni = DataFormat.GetDecimal(dr["SumPendapatanMurni"]).ToRupiahInReport(),

                                    BelanjaOperasi = DataFormat.GetDecimal(dr["SUMBO"]).ToRupiahInReport(),
                                    BelanjaModal = DataFormat.GetDecimal(dr["SUMBM"]).ToRupiahInReport(),
                                    BelanjaTakTerduga = DataFormat.GetDecimal(dr["SUMBTT"]).ToRupiahInReport(),
                                    BelanjaTransfer = DataFormat.GetDecimal(dr["SUMTRF"]).ToRupiahInReport(),

                                    BelanjaOperasiMurni = DataFormat.GetDecimal(dr["SUMBOMurni"]).ToRupiahInReport(),
                                    BelanjaModalMurni = DataFormat.GetDecimal(dr["SUMBMMurni"]).ToRupiahInReport(),
                                    BelanjaTakTerdugaMurni = DataFormat.GetDecimal(dr["SUMBTTMUrni"]).ToRupiahInReport(),
                                    BelanjaTransferMurni = DataFormat.GetDecimal(dr["SUMTRFMurni"]).ToRupiahInReport(),

                                    JumlahBelanja = (DataFormat.GetDecimal(dr["SUMBO"]) + DataFormat.GetDecimal(dr["SUMBM"]) +
                                                     DataFormat.GetDecimal(dr["SUMBTT"]) + DataFormat.GetDecimal(dr["SUMTRF"])).ToRupiahInReport(),
                                    JumlahBelanjaMurni = (DataFormat.GetDecimal(dr["SUMBOMurni"]) + DataFormat.GetDecimal(dr["SUMBMMurni"]) +
                                                       DataFormat.GetDecimal(dr["SUMBTTMUrni"]) + DataFormat.GetDecimal(dr["SUMTRFMurni"])).ToRupiahInReport()

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
        private string GetKode(int kelompok, int kategori = 0, int urusan = 0, int dinas = 0)
        {
            try
            {
                switch (kelompok)
                {
                    case 1:
                        return kategori.ToString();
                        break;
                    case 2:
                        return urusan.ToString().Substring(0, 1) + "." + urusan.ToString().Substring(1, 2);
                        break;

                    case 3:
                        return dinas.ToString().Substring(0, 1) + "." + dinas.ToString().Substring(1, 2) + "." + dinas.ToString().Substring(3, 2);
                        break;

                }
                return "";

            }
            catch (Exception ex)
            {

                return "";

            }
        }
        public List<PerdaII> GetPerdaIIBUrutOrgxx(ParameterLaporan _p)
        {
            List<PerdaII> _lst = new List<PerdaII>();
            try
            {
                // Cek View 
                // CreateViewPerdaII(_p.Tahap);

                // var query = from q in lstDB  group by ()
                string namaView = CreateViewPerdaII_B(_p, true);





                SSQL = " select 1 AS kelompok,  B.IDDInas/100  as dinas , 0   as Urutan,0 as Pokok,0 as IDUrusan, 0 As IDDinas, A.sNamaSKPD as Nama,  " +
                   " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni, " +
                   " SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni, " +
                   " SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni " +
                   " from mSKPD A INNER JOIN " + namaView + " B ON A.ID/100= B.IDDinas/100  " +
                   " inner join tKegiatan_A ON B.iTahun= tKegiatan_A.iTahun and B.IDDInas= tKegiatan_A.IDDInas and B.IDUrusan=tKegiatan_A.IDUrusan and " +
                    " B.IDKegiatan = tKegiatan_A.IDKegiatan  AND tKegiatan_A.btJenis = B.btJenis WHERE B.IDUrusan< 499 AND a.Root = 1  AND B.iTahun = " + _p.Tahun.ToString() +
                   " group by B.IDDInas/100,  A.sNamaSKPD  ";

                SSQL = SSQL + " UNION ALL select 2 AS kelompok, D.ID/100  as Dinas,A.ID  as Urutan ,E.IsPokok as Pokok,B.IDurusan AS IDUrusan,0 as IDDInas, C.sNamaKategori + ' ' + A.sNamaUrusan as Nama  , " +
                       " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni, " +
                       " SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni, " +
                       " SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni " +
                       " from mUrusan A INNER JOIN " + namaView + " B ON A.ID= B.IDUrusan Inner join mKategori C ON A.btKodekategori = C.btKodekategori  " +
                       " INNER JOIN mSKPD D ON B.IDDInas/100 = D.ID/100  " +
                       " INNER JOIN mPelaksanaUrusan E ON E.IDDInas/100 = B.IDDinas/100 AND E.IDUrusan= B.IDUrusan AND E.IDDinas/100=D.ID/100 " +
                       " inner join tKegiatan_A ON B.iTahun= tKegiatan_A.iTahun and  B.IDDInas= tKegiatan_A.IDDInas and B.IDUrusan=tKegiatan_A.IDUrusan and " +
                        " B.IDKegiatan = tKegiatan_A.IDKegiatan AND tKegiatan_A.btJenis = B.btJenis WHERE A.ID < 499 and B.iTahun = " + _p.Tahun.ToString() + " and d.Root=1" +
                       " GROUP BY  d.ID/100, a.id, B.IDurusan,C.sNamaKategori,A.sNamaUrusan,E.IsPokok ";


                SSQL = SSQL + " UNION ALL  " +
                      " select 5 AS kelompok, 55555 as dinas, 999 as Urutan,0 as Pokok ,0 AS IDUrusan, 0 As IDDinas, 'Jumlah ' as Nama,  " +
                      " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni, " +
                      " SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni, " +
                      " SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni " +
                      " from " + namaView + " B " +
                      " inner join mUrusan ON  mUrusan.ID= B.IDUrusan  inner join mSKPD ON mSKPD.ID= B.IDDInas  " +
                      " inner join tKegiatan_A ON B.iTahun= tKegiatan_A.iTahun and  B.IDDInas= tKegiatan_A.IDDInas and B.IDUrusan=tKegiatan_A.IDUrusan and " +
                        " B.IDKegiatan = tKegiatan_A.IDKegiatan  AND tKegiatan_A.btJenis = B.btJenis  WHERE  B.iTahun = " + _p.Tahun.ToString();


                SSQL = SSQL + " UNION ALL  " +
                        " select 6 AS kelompok, 66666 as dinas, 999 as Urutan ,0 as Pokok,0 AS IDUrusan, 0 As IDDinas, 'SURPLUS/DEFISIT ' as Nama,  " +
                        " (SELECT SUM (PendapatanMUrni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + ")  -" +
                        " (SELECT SUM (BTLMUrni)  from " + namaView + "  WHERE iTahun = " + _p.Tahun.ToString() + " )  - " +
                        "  (SELECT SUM (BLMUrni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + " )  as SUMPendapatan , 0 as SUMPendapatanMUrni, " +
                        " 0 as SUMBTL , 0  as SUMBTLMUrni, " +
                                      " 0 as SUMBL , 0 as SUMBLMUrni   Order by dinas, Urutan,Pokok,IDUrusan ";







                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaII()
                                {

                                    //Kode { set; get; }
                                    Level = DataFormat.GetInteger(dr["Kelompok"]),
                                    //IDKategori { set; get; }
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]).IntToStringWithLeftPad(2),
                                    IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
                                    //root { set; get; }
                                    Kode = DataFormat.GetInteger(dr["urutan"]) == 0 ? DataFormat.GetInteger(dr["dinas"]).ToKodeDinas() : (DataFormat.GetInteger(dr["urutan"]) > 0 && DataFormat.GetInteger(dr["urutan"]) < 999 ? DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan() : " "),
                                    Nama = DataFormat.GetString(dr["Nama"]).ToUpper(),
                                    Pendapatan = DataFormat.GetDecimal(dr["SumPendapatan"]).ToRupiahInReport(),
                                    BelanjaTidakLangsung = DataFormat.GetDecimal(dr["SumBTL"]).ToRupiahInReport(),
                                    BelanjaLangsung = DataFormat.GetDecimal(dr["SumBL"]).ToRupiahInReport(),
                                    JumlahBelanja = (DataFormat.GetDecimal(dr["SumBTL"]) + DataFormat.GetDecimal(dr["SumBL"])).ToRupiahInReport(),
                                    PendapatanMurni = DataFormat.GetDecimal(dr["SumPendapatanMurni"]).ToRupiahInReport(),
                                    BelanjaTidakLangsungMurni = DataFormat.GetDecimal(dr["SumBTLMurni"]).ToRupiahInReport(),
                                    BelanjaLangsungMurni = DataFormat.GetDecimal(dr["SumBLMurni"]).ToRupiahInReport(),
                                    JumlahBelanjaMurni = (DataFormat.GetDecimal(dr["SumBTLMurni"]) + DataFormat.GetDecimal(dr["SumBLMurni"])).ToRupiahInReport()

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


        public List<PerdaII> GetPerdaPembiayaanII(ParameterLaporan _p)
        {
            List<PerdaII> _lst = new List<PerdaII>();
            try
            {
                // Cek View 
                string namaView;

                namaView = CreateViewPerdaII_B(_p, true);


                SSQL = "select 1 AS kelompok,(A.btKodeKategori * 1000000) as Urutan, A.btKodeKategori AS IDUrusan, 0 as IDDInas, 'URUSAN ' + A.sNamaKategori as Nama ,  " +
                       " SUM (B.Terima) as SUMPendapatan , SUM (TerimaMUrni) as SUMPendapatanMUrni, " +
                       " SUM (Bayar) as SUMBTL , SUM (BayarMurni) as SUMBTLMUrni,  " +
                       " SUM (B.Terima)-SUM (Bayar) as SUMBL , SUM (B.TerimaMurni)-SUM (BayarMurni)  as SUMBLMUrni from mKategori A INNER JOIN " + namaView + " B ON A.btKodeKategori= B.IDUrusan/100   " +
                       " WHere  B.iTahun =" + _p.Tahun.ToString() + " GROUP BY A.btKodeKategori , A.sNamaKategori having SUM (B.Terima)>0 or SUM (B.TerimaMurni)>0 or SUM (B.Bayar)>0 ";

                SSQL = SSQL + " UNION ALL select 2 AS kelompok,  (a.id * 10000  ) as Urutan ,A.ID AS IDUrusan, 0 as IDDInas, A.sNamaUrusan as Nama , " +
                    " SUM (B.Terima) as SUMPendapatan , SUM (TerimaMUrni) as SUMPendapatanMUrni, " +
                       " SUM (Bayar) as SUMBTL , SUM (BayarMurni) as SUMBTLMUrni,  " +
                       " SUM (B.Terima)-SUM (Bayar) as SUMBL , SUM (B.TerimaMurni)-SUM (BayarMurni)  as SUMBLMUrni  " +
                   " from mUrusan A INNER JOIN " + namaView + " B ON A.ID= B.IDUrusan  " +
                   " WHERE A.ID < 499 and B.iTahun = " + _p.Tahun.ToString() +
                   " GROUP BY  A.ID , A.sNamaUrusan  having SUM (B.Terima)>0 or SUM (B.TerimaMurni)>0 or SUM (B.Bayar)>0  " +
                   " UNION ALL  " +
                   " select 3 AS kelompok, ( A.ID/100  ) * 100 as Urutan,B.IDURusan AS IDUrusan, A.ID As IDDinas, A.sNamaSKPD as Nama,  " +
                    " SUM (B.Terima) as SUMPendapatan , SUM (TerimaMUrni) as SUMPendapatanMUrni, " +
                       " SUM (Bayar) as SUMBTL , SUM (BayarMurni) as SUMBTLMUrni,  " +
                       " SUM (B.Terima)-SUM (Bayar) as SUMBL , SUM (B.TerimaMurni)-SUM (BayarMurni)  as SUMBLMUrni  " +
                   " from mSKPD A INNER JOIN " + namaView + " B ON A.ID/100= B.IDDinas/100  " +
                   " WHERE B.IDUrusan< 499 AND a.Root = 0  AND B.iTahun = " + _p.Tahun.ToString() +
                   " group by A.ID/100,A.ID, B.IDUrusan, A.sNamaSKPD  having SUM (B.Terima)>0 or SUM (B.TerimaMurni)>0 or SUM (B.Bayar)>0 ";

                SSQL = SSQL + " UNION ALL  " +
           " select 6 AS kelompok, 10000000001 as Urutan ,0 AS IDUrusan, 0 As IDDinas, 'SISA LEBIH PEMBIAYAAN ANGGARAN TAHUN ANGGARAN BERKENAN ' as Nama,  " +
           " (SELECT SUM (PendapatanMurni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + ")  + " +
           " (SELECT SUM (TerimaMurni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + ")  -" +
           " (SELECT SUM (BTLMurni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + ")  -" +
           " (SELECT SUM (BLMurni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + ")  -" +
           " (SELECT SUM (BayarMurni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + ") as SUMPendapatan,  " +
            " 0 as SUMPendapatanMUrni, " +
           " 0 as SUMBTL , 0  as SUMBTLMUrni, " +
                         " 0 as SUMBL , 0 as SUMBLMUrni   Order by Urutan ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaII()
                                {
                                    //Kode { set; get; }
                                    Level = DataFormat.GetInteger(dr["Kelompok"]),
                                    //IDKategori { set; get; }
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]).IntToStringWithLeftPad(2),
                                    IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
                                    //root { set; get; }
                                    Kode = DataFormat.GetInteger(dr["IDDinas"]) == 0 ? DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan() : DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan() + " " + DataFormat.GetInteger(dr["IDDinas"]).ToKodeDinas(),
                                    Nama = DataFormat.GetString(dr["Nama"]).ToUpper(),
                                    Pendapatan = DataFormat.GetDecimal(dr["SumPendapatan"]).ToRupiahInReport(),
                                    BelanjaTidakLangsung = DataFormat.GetDecimal(dr["SumBTL"]).ToRupiahInReport(),
                                    BelanjaLangsung = DataFormat.GetDecimal(dr["SumBL"]).ToRupiahInReport(),
                                    JumlahBelanja = (DataFormat.GetDecimal(dr["SumBTL"]) + DataFormat.GetDecimal(dr["SumBL"])).ToRupiahInReport(),
                                    PendapatanMurni = DataFormat.GetDecimal(dr["SumPendapatanMurni"]).ToRupiahInReport(),
                                    BelanjaTidakLangsungMurni = DataFormat.GetDecimal(dr["SumBTLMurni"]).ToRupiahInReport(),
                                    BelanjaLangsungMurni = DataFormat.GetDecimal(dr["SumBLMurni"]).ToRupiahInReport(),
                                    JumlahBelanjaMurni = (DataFormat.GetDecimal(dr["SumBTLMurni"]) + DataFormat.GetDecimal(dr["SumBLMurni"])).ToRupiahInReport(),
                                    PersenPendapatan = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["SumPendapatan"]), DataFormat.GetDecimal(dr["SumPendapatanMurni"])),
                                    //PersenBelanja


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
        public List<PerdaII> GetPerdaPembiayaanIIByOrg(ParameterLaporan _p)
        {
            List<PerdaII> _lst = new List<PerdaII>();
            try
            {
                // Cek View 
                string namaView;
                SetProfileRekening(mprofile);

                namaView = CreateViewPerdaII_Pembiayaan(_p, true);


                //SSQL = "select 1 AS kelompok, cast( ( A.ID/100  ) * 1000 as varchar(10))  as Urutan,B.IDUrusan, A.ID As IDDinas, A.sNamaSKPD as Nama,   " +
                //       " SUM (B.Terima) as SUMPendapatan , SUM (TerimaMUrni) as SUMPendapatanMUrni, " +
                //       " SUM (Bayar) as SUMBTL , SUM (BayarMurni) as SUMBTLMUrni,  " +
                //       " SUM (B.Terima)-SUM (Bayar) as SUMBL , SUM (B.TerimaMurni)-SUM (BayarMurni)  as SUMBLMUrni from mKategori A INNER JOIN " + namaView + " B ON A.btKodeKategori= B.IDUrusan/100   " +
                //       " WHERE B.IDUrusan< 499 AND a.Root = 0  AND B.iTahun = " + _p.Tahun.ToString() +
                //       " group by A.ID/100,A.ID, B.IDUrusan, A.sNamaSKPD having SUM (B.Terima)>0 or SUM (B.TerimaMurni)>0 or SUM (B.Bayar)>0 ";


                //SSQL = SSQL + " UNION ALL select 1 AS kelompok, cast(A.ID  as varchar(8)) + cast(B.IDUrusan  as char(3)) as Urutan,B.IDUrusan, A.ID As IDDinas, A.sNamaSKPD as Nama,  " +
                //    " SUM (B.Terima) as SUMPendapatan , SUM (TerimaMUrni) as SUMPendapatanMUrni, " +
                //       " SUM (Bayar) as SUMBTL , SUM (BayarMurni) as SUMBTLMUrni,  " +
                //       " SUM (B.Terima)-SUM (Bayar) as SUMBL , SUM (B.TerimaMurni)-SUM (BayarMurni)  as SUMBLMUrni  " +
                //    " from mSKPD A INNER JOIN " + namaView + " B ON A.ID= B.IDDinas  " +
                //   " WHERE B.IDUrusan< 499 AND a.Root = 1  AND B.iTahun = " + _p.Tahun.ToString() + " AND A.ID like '10201%'" +
                //   " group by A.ID,A.ID, B.IDUrusan, A.sNamaSKPD having SUM (B.Terima)>0 or SUM (B.TerimaMurni)>0 or SUM (B.Bayar)>0 "; 

                //SSQL=SSQL +  " UNION ALL select 2 AS kelompok, cast(B.IDDInas  as varchar(8)) + cast(A.ID  as char(3)) +'1'  as Urutan ,A.ID AS IDUrusan, 0 as IDDInas, C.sNamaKategori + ' ' + A.sNamaUrusan as Nama , " +
                //    " SUM (B.Terima) as SUMPendapatan , SUM (TerimaMUrni) as SUMPendapatanMUrni, " +
                //       " SUM (Bayar) as SUMBTL , SUM (BayarMurni) as SUMBTLMUrni,  " +
                //       " SUM (B.Terima)-SUM (Bayar) as SUMBL , SUM (B.TerimaMurni)-SUM (BayarMurni)  as SUMBLMUrni  " +
                //   " from mUrusan A INNER JOIN " + namaView + " B ON A.ID= B.IDUrusan Inner join mKategori C ON A.btKodekategori = C.btKodekategori  " +
                //   " WHERE A.ID < 499 and B.iTahun = " + _p.Tahun.ToString() +
                //   " GROUP BY  A.ID , B.IDDInas, A.sNamaUrusan,C.sNamaKategori  " +
                //   "   Order by Urutan ";




                SSQL = " select 1 AS kelompok, cast( ( A.ID/100  ) * 1000 as varchar(10))  as Urutan,B.IDUrusan, A.ID As IDDinas, A.sNamaSKPD as Nama,  " +
                   " SUM (B.Terima) as SUMPendapatan , SUM (TerimaMUrni) as SUMPendapatanMUrni, " +
                   " SUM (Bayar) as SUMBTL , SUM (BayarMUrni) as SUMBTLMUrni, " +
                   " SUM (Terima-Bayar) as SUMBL , SUM (TerimaMurni-BayarMurni) as SUMBLMUrni " +
                   " from mSKPD A INNER JOIN " + namaView + " B ON A.ID= B.IDDinas  " +
                   " WHERE B.IDUrusan< 10000 AND a.Root = 1  AND B.iTahun = " + _p.Tahun.ToString() +
                   " group by A.ID/100,A.ID, B.IDUrusan, A.sNamaSKPD having SUM (B.Terima)>0 or SUM (B.TerimaMurni)>0 or SUM (B.Bayar)>0 ";


                //SSQL = SSQL + " UNION ALL select 1 AS kelompok, cast(A.ID  as varchar(8)) + cast(B.IDUrusan  as char(3)) as Urutan,B.IDUrusan, A.ID As IDDinas, A.sNamaSKPD as Nama,  " +
                //   " SUM (B.Terima) as SUMPendapatan , SUM (TerimaMUrni) as SUMPendapatanMUrni, " +
                //   " SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni, " +
                //   " SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni " +
                //   " from mSKPD A INNER JOIN " + namaView + " B ON A.ID= B.IDDinas  " +
                //   " WHERE B.IDUrusan< 499 AND a.Root = 1  AND B.iTahun = " + _p.Tahun.ToString() + " AND A.ID like '10201%'" +
                //   " group by A.ID,A.ID, B.IDUrusan, A.sNamaSKPD";


                SSQL = SSQL + " UNION ALL select 2 AS kelompok, cast(B.IDDInas  as varchar(8)) + cast(A.ID  as char(3)) +'1'  as Urutan ,A.ID AS IDUrusan, 0 as IDDInas, C.sNamaKategori + ' ' + A.sNamaUrusan as Nama , " +
                   " SUM (B.Terima) as SUMPendapatan , SUM (TerimaMUrni) as SUMPendapatanMUrni, " +
                   " SUM (Bayar) as SUMBTL , SUM (BayarMUrni) as SUMBTLMUrni, " +
                   " SUM (Terima-Bayar) as SUMBL , SUM (TerimaMurni-BayarMurni) as SUMBLMUrni " +
                   " from mUrusan A INNER JOIN " + namaView + " B ON A.ID= B.IDUrusan Inner join mKategori C ON A.btKodekategori = C.btKodekategori  " +
                   " WHERE  B.iTahun = " + _p.Tahun.ToString() +
                   " GROUP BY  A.ID , B.IDDInas, A.sNamaUrusan,C.sNamaKategori having SUM (B.Terima)>0 or SUM (B.TerimaMurni)>0 or SUM (B.Bayar)>0  " +
                   "   ";


                //SSQL = SSQL + " UNION ALL  " +
                //      " select 5 AS kelompok, '99999999999999999' as Urutan,0 AS IDUrusan, 0 As IDDinas, 'Jumlah ' as Nama,  " +
                //      " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni, " +
                //      " SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni, " +
                //      " SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni " +
                //      " from " + namaView + " B " +
                //      " inner join mUrusan ON  mUrusan.ID= B.IDUrusan  inner join mSKPD ON mSKPD.ID= B.IDDInas WHERE  B.iTahun = " + _p.Tahun.ToString();


                //SSQL = SSQL + " UNION ALL  " +
                //        " select 6 AS kelompok, '999999999999999991' as Urutan ,0 AS IDUrusan, 0 As IDDinas, 'SURPLUS/DEFISIT ' as Nama,  " +
                //        "  SUM (Terima+Pendapatan- BTL-BL-Bayar)  as SUMPendapatan , " +
                //        " SUM (TerimaMUrni+PendapatanMurni- BTLMUrni-BLMurni-BayarMurni)   as SUMPendapatanMUrni, " +
                //        " 0 as SUMBTL , 0  as SUMBTLMUrni, " +
                //                      " 0 as SUMBL , 0 as SUMBLMUrni   from " + namaView + "  having SUM (Terima)>0 or SUM (TerimaMurni)>0 or SUM (Bayar)>0  Order by Urutan ";

                //SSQL = SSQL + " Order by Urutan ";






                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaII()
                                {
                                    //Kode { set; get; }
                                    Level = DataFormat.GetInteger(dr["Kelompok"]),
                                    //IDKategori { set; get; }
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]).IntToStringWithLeftPad(2),
                                    IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
                                    //root { set; get; }
                                    Kode = DataFormat.GetInteger(dr["IDDinas"]) == 0 ? DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan() : DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan() + " " + DataFormat.GetInteger(dr["IDDinas"]).ToKodeDinas(),
                                    Nama = DataFormat.GetString(dr["Nama"]).ToUpper(),
                                    Pendapatan = DataFormat.GetDecimal(dr["SumPendapatan"]).ToRupiahInReport(),
                                    BelanjaTidakLangsung = DataFormat.GetDecimal(dr["SumBTL"]).ToRupiahInReport(),
                                    BelanjaLangsung = DataFormat.GetDecimal(dr["SumBL"]).ToRupiahInReport(),
                                    JumlahBelanja = (DataFormat.GetDecimal(dr["SumBTL"]) + DataFormat.GetDecimal(dr["SumBL"])).ToRupiahInReport(),
                                    PendapatanMurni = DataFormat.GetDecimal(dr["SumPendapatanMurni"]).ToRupiahInReport(),
                                    BelanjaTidakLangsungMurni = DataFormat.GetDecimal(dr["SumBTLMurni"]).ToRupiahInReport(),
                                    BelanjaLangsungMurni = DataFormat.GetDecimal(dr["SumBLMurni"]).ToRupiahInReport(),
                                    JumlahBelanjaMurni = (DataFormat.GetDecimal(dr["SumBTLMurni"]) + DataFormat.GetDecimal(dr["SumBLMurni"])).ToRupiahInReport()


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


        private void PrepareFunction()
        {
            string SSQL;
            SSQL = "";

            SSQL = " IF OBJECT_ID (N'dbo.SurplusDefisitOlah', N'FN') IS NOT NULL  " +
                 " DROP FUNCTION SurplusDefisitOlah;  ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "CREATE FUNCTION SurplusDefisitOlah (@Tahun int, @IDDinas int )" +
                "RETURNS DECIMAL AS " +
                 "BEGIN " +
                        " DECLARE @PENDAPATAN AS DECIMAL " +
                         " DECLARE @BELANJA AS DECIMAL " +
                         " SElect @PENDAPATAN= SUM(cJumlahOlah) from tAnggaranRekening_A WHERE iTahun = @Tahun and IDDInas= @IDDinas AND btJenis = 1; " +
                         " SElect @BELANJA= SUM(cJumlahOlah) from tAnggaranRekening_A WHERE iTahun = @Tahun and IDDInas= @IDDinas  AND btJenis IN (2,3); " +
                         " return @PENDAPATAN - @BELANJA " +
                     " END ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = " IF OBJECT_ID (N'dbo.SurplusDefisit', N'FN') IS NOT NULL  " +
                 " DROP FUNCTION SurplusDefisit;  ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "CREATE FUNCTION SurplusDefisit (@Tahun int, @IDDinas int )" +
                "RETURNS DECIMAL AS " +
                 "BEGIN " +
                        " DECLARE @PENDAPATAN AS DECIMAL" +
                         " DECLARE @BELANJA AS DECIMAL " +
                         " SElect @PENDAPATAN= SUM(cJumlah) from tAnggaranRekening_A WHERE iTahun = @Tahun and IDDInas= @IDDinas AND btJenis = 1; " +
                         " SElect @BELANJA= SUM(cJumlah) from tAnggaranRekening_A WHERE iTahun = @Tahun and IDDInas= @IDDinas  AND btJenis IN (2,3); " +
                         " return @PENDAPATAN - @BELANJA " +
                     " END ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = " IF OBJECT_ID (N'dbo.SurplusDefisitMurni', N'FN') IS NOT NULL  " +
                 " DROP FUNCTION SurplusDefisitMurni;  ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "CREATE FUNCTION SurplusDefisitMurni (@Tahun int, @IDDinas int )" +
                "RETURNS DECIMAL AS " +
                 "BEGIN " +
                        " DECLARE @PENDAPATAN AS DECIMAL" +
                         " DECLARE @BELANJA AS DECIMAL " +
                         " SElect @PENDAPATAN= SUM(cJumlahMurni) from tAnggaranRekening_A WHERE iTahun = @Tahun and IDDInas= @IDDinas AND btJenis = 1; " +
                         " SElect @BELANJA= SUM(cJumlahMurni) from tAnggaranRekening_A WHERE iTahun = @Tahun and IDDInas= @IDDinas  AND btJenis IN (2,3); " +
                         " return @PENDAPATAN - @BELANJA " +
                     " END ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = " IF OBJECT_ID (N'dbo.SurplusDefisitOlahAll', N'FN') IS NOT NULL  " +
                 " DROP FUNCTION SurplusDefisitOlahAll;  ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "CREATE FUNCTION SurplusDefisitOlahAll (@Tahun int)" +
                "RETURNS DECIMAL AS " +
                 "BEGIN " +
                        " DECLARE @PENDAPATAN AS DECIMAL " +
                         " DECLARE @BELANJA AS DECIMAL " +
                         " SElect @PENDAPATAN= SUM(cJumlahOlah) from tAnggaranRekening_A WHERE iTahun = @Tahun AND btJenis = 1; " +
                         " SElect @BELANJA= SUM(cJumlahOlah) from tAnggaranRekening_A WHERE iTahun = @Tahun AND btJenis IN (2,3); " +
                         " return @PENDAPATAN - @BELANJA " +
                     " END ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = " IF OBJECT_ID (N'dbo.SurplusDefisitAll', N'FN') IS NOT NULL  " +
                 " DROP FUNCTION SurplusDefisitAll;  ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "CREATE FUNCTION SurplusDefisitAll (@Tahun int)" +
                "RETURNS DECIMAL AS " +
                 "BEGIN " +
                        " DECLARE @PENDAPATAN AS DECIMAL" +
                         " DECLARE @BELANJA AS DECIMAL " +
                         " SElect @PENDAPATAN= SUM(cJumlah) from tAnggaranRekening_A WHERE iTahun = @Tahun AND btJenis = 1; " +
                         " SElect @BELANJA= SUM(cJumlah) from tAnggaranRekening_A WHERE iTahun = @Tahun AND btJenis IN (2,3); " +
                         " return @PENDAPATAN - @BELANJA " +
                     " END ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = " IF OBJECT_ID (N'dbo.SurplusDefisitMurniAll', N'FN') IS NOT NULL  " +
                 " DROP FUNCTION SurplusDefisitMurniAll;  ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "CREATE FUNCTION SurplusDefisitMurniAll (@Tahun int)" +
                "RETURNS DECIMAL AS " +
                 "BEGIN " +
                        " DECLARE @PENDAPATAN AS DECIMAL" +
                         " DECLARE @BELANJA AS DECIMAL " +
                         " SElect @PENDAPATAN= SUM(cJumlahMurni) from tAnggaranRekening_A WHERE iTahun = @Tahun AND btJenis = 1; " +
                         " SElect @BELANJA= SUM(cJumlahMurni) from tAnggaranRekening_A WHERE iTahun = @Tahun AND btJenis IN (2,3); " +
                         " return @PENDAPATAN - @BELANJA " +
                     " END ";
            _dbHelper.ExecuteNonQuery(SSQL);



        }
        private void BersihkanNonKegiatan()
        {
            if (Tahun <= 2020)
            {
                // SSQL = "DROP TABLE temppendapatan";
                SSQL = "IF OBJECT_ID('dbo.temppendapatan', 'U') IS NOT NULL  DROP TABLE temppendapatan";

                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "select distinct IDDinas into temppendapatan FROM tAnggaranRekening_A WHERE iTAhun =" + Tahun.ToString() + " and IIDRekening like '4%'";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "DELETE from tKegiatan_A  WHERE btJenis= 1";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = " INSERT  into tKegiatan_A (iTahun, sNama,btJenis, dtPembahasan,IDDInas, IDUrusan, IDProgram, IDkegiatan) SELECT " + Tahun.ToString() + "  as iTahun,'Pendapatn',1,'10/31/2017', IDDInas, IDDinas/10000 as IDUrusan, 0 as IDProgram, 0 as IDKegiatan from temppendapatan  ";
                _dbHelper.ExecuteNonQuery(SSQL);




                SSQL = "DELETE from tKegiatan_A  WHERE btJenis= 2";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = " INSERT  into tKegiatan_A (iTahun, sNama,btJenis, dtPembahasan,IDDInas, IDUrusan, IDProgram, IDkegiatan) SELECT " + Tahun.ToString() + "  as iTahun,'Belanja Tidak Langsung',2,'10/31/2017',ID as IDDInas, ID/10000 as IDUrusan, 0 as IDProgram, 0 as IDKegiatan from mSKPD where root =1 ";
                _dbHelper.ExecuteNonQuery(SSQL);

                // pembiayaan 
                SSQL = "DROP TABLE temppendapatan";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "select distinct IDDinas into temppendapatan FROM tAnggaranRekening_A WHERE iTAhun =" + Tahun.ToString() + "  and IIDRekening like '61%'";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "DELETE from tKegiatan_A  WHERE btJenis = 4";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = " INSERT  into tKegiatan_A (iTahun, sNama,btJenis, dtPembahasan,IDDInas, IDUrusan, IDProgram, IDkegiatan) SELECT " + Tahun.ToString() + "  as iTahun,'Penerimaan Pembiayaan',4,'10/31/2017', IDDInas, IDDinas/10000 as IDUrusan, 0 as IDProgram, 0 as IDKegiatan from temppendapatan  ";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "DROP TABLE temppendapatan";
                _dbHelper.ExecuteNonQuery(SSQL);


                SSQL = "select distinct IDDinas into temppendapatan FROM tAnggaranRekening_A WHERE iTAhun =" + Tahun.ToString() + "  and IIDRekening like '62%'";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "DELETE from tKegiatan_A  WHERE btJenis = 5";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = " INSERT  into tKegiatan_A (iTahun, sNama,btJenis, dtPembahasan,IDDInas, IDUrusan, IDProgram, IDkegiatan) SELECT " + Tahun.ToString() + "  as iTahun,'Pengeluaran Pembiayaan',5,'10/31/2017', IDDInas, IDDinas/10000 as IDUrusan, 0 as IDProgram, 0 as IDKegiatan from temppendapatan  ";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "DROP TABLE temppendapatan";
                _dbHelper.ExecuteNonQuery(SSQL);

            }
            else
            {

                // SSQL = "DROP TABLE temppendapatan";
                SSQL = "IF OBJECT_ID('dbo.temppendapatan', 'U') IS NOT NULL  DROP TABLE temppendapatan";

                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "select distinct IDDinas into temppendapatan FROM tAnggaranRekening_A WHERE iTAhun =" + Tahun.ToString() + " and IIDRekening like '4%'";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "DELETE from tSubKegiatan  WHERE btJenis= 1";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = " INSERT  into tSubKegiatan  (iTahun, Nama,btJenis, IDDInas, IDUrusan, IDProgram, IDkegiatan,IDSUbKegiatan,idunit ) SELECT " + Tahun.ToString() + "  as iTahun,'PENDAPATAN' as Nama,1, IDDInas, IDDinas/10000 as IDUrusan, 0 as IDProgram, 0 as IDKegiatan, 0 as IDSubKegiatan,0 as idunit from temppendapatan  ";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "DROP TABLE temppendapatan";
                _dbHelper.ExecuteNonQuery(SSQL);
                // SSQL = "DROP TABLE temppendapatan";
                SSQL = "IF OBJECT_ID('dbo.temppendapatan', 'U') IS NOT NULL  DROP TABLE temppendapatan";

                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "select distinct IDDinas into temppendapatan FROM tAnggaranRekening_A WHERE iTAhun =" + Tahun.ToString() + " and btjenis = 2 ";

                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "DELETE from tSubKegiatan  WHERE btJenis= 2";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = " INSERT  into tSubKegiatan  (iTahun, Nama,btJenis, IDDInas, IDUrusan, IDProgram, IDkegiatan,IDSUbKegiatan,idunit) SELECT " + Tahun.ToString() + "  as iTahun,'Belanja PPKD' as Nama,2, IDDInas, IDDinas/10000 as IDUrusan, 0 as IDProgram, 0 as IDKegiatan, 0 as IDSubKegiatan,0 as idunit from temppendapatan  ";
                _dbHelper.ExecuteNonQuery(SSQL);

                //// pembiayaan 
                SSQL = "DROP TABLE temppendapatan";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "select distinct IDDinas into temppendapatan FROM tAnggaranRekening_A WHERE iTAhun =" + Tahun.ToString() + "  and IIDRekening like '61%'";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "DELETE from tSubKegiatan  WHERE btJenis = 4";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = " INSERT  into tSubKegiatan (iTahun, Nama,btJenis, IDDInas, IDUrusan, IDProgram, IDkegiatan,IDSubKegiatan,idunit) SELECT " + Tahun.ToString() + "  as iTahun,'Penerimaan Pembiayaan',4,IDDInas, IDDinas/10000 as IDUrusan, 0 as IDProgram, 0 as IDKegiatan, 0 as IDSubKegiatan,0 as idunit from temppendapatan  ";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "DROP TABLE temppendapatan";
                _dbHelper.ExecuteNonQuery(SSQL);


                SSQL = "select distinct IDDinas into temppendapatan FROM tAnggaranRekening_A WHERE iTAhun =" + Tahun.ToString() + "  and IIDRekening like '62%'";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "DELETE from tSubKegiatan  WHERE btJenis = 5";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = " INSERT  into tSubKegiatan (iTahun, Nama,btJenis, IDDInas, IDUrusan, IDProgram, IDkegiatan,IDSUBKegiatan,idunit) SELECT " + Tahun.ToString() + "  as iTahun,'Pengeluaran Pembiayaan',5,IDDInas, IDDinas/10000 as IDUrusan, 0 as IDProgram, 0 as IDKegiatan,0 AS IDSUbKegiatan, 0 as idunit from temppendapatan  ";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "DROP TABLE temppendapatan";
                _dbHelper.ExecuteNonQuery(SSQL);

            }

        }
        private string CreateViewPerdaII(ParameterLaporan _p, bool create)
        {

            string namaView = "viewPerdaII" + _p.NamaUser.Trim();
            SSQL = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[" + namaView + "]')) " +
                " DROP VIEW [dbo].[" + namaView + "]";

            _dbHelper.ExecuteNonQuery(SSQL);

            if (create == true)
            {

                GetKolom(_p.Tahap);
                SSQL = "CREATE VIEW " + namaView + " AS Select iTAhun, IDDInas,IDurusan, IDProgram, IDKegiatan, IIDRekening, " +
                        "btJenis,case when btJenis=1 THEN " + _namaKolom1 + " ELSE 0 END AS PEndapatanMurni," +
                            " case when btJenis=1 THEN " + _namaKolom2 + " ELSE 0 END AS PEndapatan, " +
                        " case when btJenis=2 THEN " + _namaKolom1 + " ELSE 0 END AS BTLMurni," +
                        " case when btJenis=2 THEN " + _namaKolom2 + " ELSE 0 END AS BTL," +
                        " case when btJenis=3 THEN " + _namaKolom1 + " ELSE 0 END AS BLMurni," +
                        " case when btJenis=3 THEN " + _namaKolom2 + " ELSE 0 END AS BL," +
                        " case when btJenis=4 THEN " + _namaKolom1 + " ELSE 0 END AS TerimaMurni," +
                        " case when btJenis=4 THEN " + _namaKolom2 + " ELSE 0 END AS Terima," +
                        " case when btJenis=5 THEN " + _namaKolom1 + " ELSE 0 END AS BayarMurni," +
                        " case when btJenis=5 THEN " + _namaKolom2 + " ELSE 0 END AS Bayar " +
                        " from tAnggaranRekening_A ";
                _dbHelper.ExecuteNonQuery(SSQL);

            }
            return namaView;
        }

        public List<PerdaIIDB> GetPerda(int tahap)
        {
            List<PerdaIIDB> _lst = new List<PerdaIIDB>();
            try
            {
                GetKolom(tahap);
                SSQL = "Select iTAhun, IDDInas,IDurusan, IDProgram, IDKegiatan, IIDRekening, " +
                        " btJenis,case when btJenis=1 THEN " + _namaKolom1 + " ELSE 0 END AS PEndapatanOlah," +
                        " case when btJenis=1 THEN " + _namaKolom2 + " ELSE 0 END AS PEndapatan, " +
                        " case when btJenis=2 THEN " + _namaKolom1 + " ELSE 0 END AS BTLOlah," +
                        " case when btJenis=2 THEN " + _namaKolom2 + " ELSE 0 END AS BTL," +
                        " case when btJenis=3 THEN " + _namaKolom1 + " ELSE 0 END AS BLOlah," +
                        " case when btJenis=3 THEN " + _namaKolom2 + " ELSE 0 END AS BL," +
                        " case when btJenis=4 THEN " + _namaKolom1 + " ELSE 0 END AS TerimaOlah," +
                        " case when btJenis=4 THEN " + _namaKolom2 + " ELSE 0 END AS Terima," +
                        " case when btJenis=5 THEN " + _namaKolom1 + " ELSE 0 END AS BayarOlah," +
                        " case when btJenis=5 THEN " + _namaKolom2 + " ELSE 0 END AS Bayar " +
                        " from tAnggaranRekening_A";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIIDB()
                                {
                                    iTAhun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDurusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetLong(dr["IDKegiatan"]),

                                    btJenis = DataFormat.GetInteger(dr["btJenis"]),
                                    PendapatanOlah = DataFormat.GetDecimal(dr["PendapatanOlah"]),
                                    Pendapatan = DataFormat.GetDecimal(dr["Pendapatan"]),
                                    BTLOlah = DataFormat.GetDecimal(dr["BTLOlah"]),
                                    BTL = DataFormat.GetDecimal(dr["BTL"]),
                                    BLOlah = DataFormat.GetDecimal(dr["BLOlah"]),
                                    BL = DataFormat.GetDecimal(dr["BL"]),
                                    TerimaOlah = DataFormat.GetDecimal(dr["TerimaOlah"]),
                                    Terima = DataFormat.GetDecimal(dr["Terima"]),
                                    BayarOlah = DataFormat.GetDecimal(dr["BayarOlah"]),
                                    Bayar = DataFormat.GetDecimal(dr["Bayar"])
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


        private string CreateViewPerdaII_B(ParameterLaporan _p, bool create)
        {
            string namaView = "viewPerdaII" + _p.NamaUser.Trim();
            SSQL = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[" + namaView + "]')) " +
                " DROP VIEW [dbo].[" + namaView + "]";
            _dbHelper.ExecuteNonQuery(SSQL);

            if (create == true)
            {
                GetKolom(_p.Tahap);

                SSQL = "CREATE VIEW " + namaView + " AS Select tAnggaranRekening_A.iTAhun, tAnggaranRekening_A.IDDInas,tAnggaranRekening_A.IDUrusan as IDurusan, tAnggaranRekening_A.IDProgram, tAnggaranRekening_A.IDKegiatan, tAnggaranRekening_A.IIDRekening, " +
                        "tAnggaranRekening_A.btJenis,case when tAnggaranRekening_A.btJenis=1 THEN tAnggaranRekening_A.cJumlahOlah ELSE 0 END AS PEndapatanOlah," +
                        " case when tAnggaranRekening_A.btJenis=1 THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS PEndapatan, " +
                        " case when tAnggaranRekening_A.btJenis=1 THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS PEndapatanMurni," +
                        " case when tAnggaranRekening_A.btJenis=2 THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS BTL," +
                        " case when tAnggaranRekening_A.btJenis=2 THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS BTLMurni," +
                        " case when tAnggaranRekening_A.btJenis=4 THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS TerimaMurni," +
                        " case when tAnggaranRekening_A.btJenis=5 THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS BayarMurni," +
                         " case when tAnggaranRekening_A.btJenis=3 THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS BL," +
                        " case when tAnggaranRekening_A.btJenis=3 THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS BLMurni," +
                        " case when tAnggaranRekening_A.btJenis=4 THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS Terima," +
                        " case when tAnggaranRekening_A.btJenis=5 THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS Bayar," +
                        " mSKPD.Parent from tAnggaranRekening_A INNER JOIN mSKPD ON tAnggaranRekening_A.IDDInas = mSKPD.ID inner join tKegiatan_A " +
                        " ON tAnggaranRekening_A.iTahun= tKegiatan_A.iTahun and tAnggaranRekening_A.IDDInas= tKegiatan_A.IDDInas and tAnggaranRekening_A.IDUrusan=tKegiatan_A.IDUrusan and  " +
                        " tAnggaranRekening_A.IDKegiatan = tKegiatan_A.IDKegiatan and tKegiatan_A.btJenis = tAnggaranRekening_A.btJenis  ";

                //if (_p.HanyaUrusanPokok == true)
                //{
                //    SSQL = SSQL + " inner join mPelaksanaUrusan On mPelaksanaUrusan.IDUrusan = tAnggaranRekening_A.IDUrusan " +
                //                " AND  mPelaksanaUrusan.IDDInas = tAnggaranRekening_A.IDDInas where mPelaksanaUrusan.isPokok =0 ";

                //}
                _dbHelper.ExecuteNonQuery(SSQL);

            }
            return namaView;
        }
        private string CreateViewPerdaII_Pembiayaan(ParameterLaporan _p, bool create)
        {
            string namaView = "viewPerdaII" + _p.NamaUser.Trim();
            SSQL = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[" + namaView + "]')) " +
                " DROP VIEW [dbo].[" + namaView + "]";
            _dbHelper.ExecuteNonQuery(SSQL);

            if (create == true)
            {
                GetKolom(_p.Tahap);

                SSQL = "CREATE VIEW " + namaView + " AS Select tAnggaranRekening_A.iTAhun, tAnggaranRekening_A.IDDInas,tAnggaranRekening_A.IDUrusan as IDurusan, tAnggaranRekening_A.IDProgram, tAnggaranRekening_A.IDKegiatan, tAnggaranRekening_A.IIDRekening, " +
                        "tAnggaranRekening_A.btJenis,case when tAnggaranRekening_A.btJenis=1 THEN tAnggaranRekening_A.cJumlahOlah ELSE 0 END AS PEndapatanOlah," +
                        " case when tAnggaranRekening_A.btJenis=1 THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS PEndapatan, " +
                        " case when tAnggaranRekening_A.btJenis=1 THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS PEndapatanMurni," +
                        " case when tAnggaranRekening_A.btJenis=2 THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS BTL," +
                        " case when tAnggaranRekening_A.btJenis=2 THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS BTLMurni," +
                        " case when tAnggaranRekening_A.btJenis=4 THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS TerimaMurni," +
                        " case when tAnggaranRekening_A.btJenis=5 THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS BayarMurni," +
                        " case when tAnggaranRekening_A.btJenis=3 THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS BLMurni," +
                        " case when tAnggaranRekening_A.btJenis=3 THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS BL," +
                                         " case when tAnggaranRekening_A.btJenis=4 THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS Terima," +
                        " case when tAnggaranRekening_A.btJenis=5 THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS Bayar," +
                        " mSKPD.Parent from tAnggaranRekening_A INNER JOIN mSKPD ON tAnggaranRekening_A.IDDInas = mSKPD.ID inner join tSubkegiatan " +
                        " ON tAnggaranRekening_A.iTahun= tSubkegiatan.iTahun and tAnggaranRekening_A.IDDInas= tSubkegiatan.IDDInas and tAnggaranRekening_A.IDUrusan=tSubkegiatan.IDUrusan and  " +
                        " tAnggaranRekening_A.IDKegiatan = tSubkegiatan.IDKegiatan and tSubkegiatan.btJenis = tAnggaranRekening_A.btJenis Where tAnggaranRekening_A.iTahun = 2021 ";

                //if (_p.HanyaUrusanPokok == true)
                //{
                //    SSQL = SSQL + " inner join mPelaksanaUrusan On mPelaksanaUrusan.IDUrusan = tAnggaranRekening_A.IDUrusan " +
                //                " AND  mPelaksanaUrusan.IDDInas = tAnggaranRekening_A.IDDInas where mPelaksanaUrusan.isPokok =0 ";

                //}
                _dbHelper.ExecuteNonQuery(SSQL);

            }
            return namaView;
        }
        private string CreateViewPerdaII_B90(ParameterLaporan _p, bool create)
        {
            string namaView = "viewPerdaII" + _p.NamaUser.Trim();
            SSQL = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[" + namaView + "]')) " +
                " DROP VIEW [dbo].[" + namaView + "]";
            _dbHelper.ExecuteNonQuery(SSQL);

            if (create == true)
            {
                GetKolom(_p.Tahap);
                if (_p.Tahun >= 2021)
                {

                    SSQL = "CREATE VIEW " + namaView + " AS Select tAnggaranRekening_A.iTAhun, tAnggaranRekening_A.IDDInas,tAnggaranRekening_A.IDUrusan as IDurusan, tAnggaranRekening_A.IDProgram, tAnggaranRekening_A.IDKegiatan,tAnggaranRekening_A.IDSubKegiatan,  tAnggaranRekening_A.IIDRekening, " +
                            "tAnggaranRekening_A.btJenis,case when tAnggaranRekening_A.btJenis=1 THEN tAnggaranRekening_A.cJumlahOlah ELSE 0 END AS PEndapatanOlah," +
                            " case when tAnggaranRekening_A.btJenis=1 THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS PEndapatan, " +
                            " case when tAnggaranRekening_A.btJenis=1 THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS PEndapatanMurni," +
                            " case when tAnggaranRekening_A.IIDRekening like '51%' THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS BO," +
                            " case when tAnggaranRekening_A.IIDRekening like '51%' THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS BOMurni," +
                            " case when tAnggaranRekening_A.IIDRekening like '52%' THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS BM," +
                            " case when tAnggaranRekening_A.IIDRekening like '52%' THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS BMMurni," +
                            " case when tAnggaranRekening_A.IIDRekening like '53%' THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS BTT," +
                            " case when tAnggaranRekening_A.IIDRekening like '53%' THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS BTTMurni," +
                            " case when tAnggaranRekening_A.IIDRekening like '54%' THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS TRF," +
                            " case when tAnggaranRekening_A.IIDRekening like '54%' THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS TRFMurni," +
                            " case when tAnggaranRekening_A.btJenis=4 THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS Terima," +
                            " case when tAnggaranRekening_A.btJenis=4 THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS TerimaMurni," +
                            " case when tAnggaranRekening_A.btJenis=5 THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS Bayar," +
                            " case when tAnggaranRekening_A.btJenis=5 THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS BayarMurni," +
                            " mSKPD.Parent from tAnggaranRekening_A INNER JOIN mSKPD ON tAnggaranRekening_A.IDDInas = mSKPD.ID inner join TSUbKegiatan " +
                            " ON tAnggaranRekening_A.iTahun= TSUbKegiatan.iTahun and tAnggaranRekening_A.IDDInas= TSUbKegiatan.IDDInas and tAnggaranRekening_A.IDUrusan=TSUbKegiatan.IDUrusan and  " +
                            " tAnggaranRekening_A.IDKegiatan = TSUbKegiatan.IDKegiatan and tAnggaranRekening_A.IDSubKegiatan = TSUbKegiatan.IDSubKegiatan and TSUbKegiatan.btJenis = tAnggaranRekening_A.btJenis ";
                    _dbHelper.ExecuteNonQuery(SSQL);
                }
                else
                {
                    SSQL = "CREATE VIEW " + namaView + " AS Select tAnggaranRekening_A.iTAhun, tAnggaranRekening_A.IDDInas,tAnggaranRekening_A.IDUrusan as IDurusan, tAnggaranRekening_A.IDProgram, tAnggaranRekening_A.IDKegiatan,tAnggaranRekening_A.IDSubKegiatan,  tAnggaranRekening_A.IIDRekening, " +
                            "tAnggaranRekening_A.btJenis,case when tAnggaranRekening_A.btJenis=1 THEN tAnggaranRekening_A.cJumlahOlah ELSE 0 END AS PEndapatanOlah," +
                            " case when tAnggaranRekening_A.btJenis=1 THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS PEndapatan, " +
                            " case when tAnggaranRekening_A.btJenis=1 THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS PEndapatanMurni," +
                            " case when tAnggaranRekening_A.IIDRekening like '51%' THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS BO," +
                            " case when tAnggaranRekening_A.IIDRekening like '51%' THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS BOMurni," +
                            " case when tAnggaranRekening_A.IIDRekening like '52%' THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS BM," +
                            " case when tAnggaranRekening_A.IIDRekening like '52%' THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS BMMurni," +
                            " case when tAnggaranRekening_A.IIDRekening like '53%' THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS BTT," +
                            " case when tAnggaranRekening_A.IIDRekening like '53%' THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS BTTMurni," +
                            " case when tAnggaranRekening_A.IIDRekening like '54%' THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS TRF," +
                            " case when tAnggaranRekening_A.IIDRekening like '54%' THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS TRFMurni," +
                            " case when tAnggaranRekening_A.btJenis=4 THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS Terima," +
                            " case when tAnggaranRekening_A.btJenis=4 THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS TerimaMurni," +
                            " case when tAnggaranRekening_A.btJenis=5 THEN tAnggaranRekening_A." + _namaKolom2 + " ELSE 0 END AS Bayar," +
                            " case when tAnggaranRekening_A.btJenis=5 THEN tAnggaranRekening_A." + _namaKolom1 + " ELSE 0 END AS BayarMurni," +
                            " mSKPD.Parent from tAnggaranRekening_A INNER JOIN mSKPD ON tAnggaranRekening_A.IDDInas = mSKPD.ID inner join TSUbKegiatan " +
                            " ON tAnggaranRekening_A.iTahun= TSUbKegiatan.iTahun and tAnggaranRekening_A.IDDInas= TSUbKegiatan.IDDInas and tAnggaranRekening_A.IDUrusan=TSUbKegiatan.IDUrusan and  " +
                            " tAnggaranRekening_A.IDKegiatan = TSUbKegiatan.IDKegiatan and tAnggaranRekening_A.IDSubKegiatan = TSUbKegiatan.IDSubKegiatan and TSUbKegiatan.btJenis = tAnggaranRekening_A.btJenis ";
                    _dbHelper.ExecuteNonQuery(SSQL);

                }

            }
            return namaView;
        }

        private void CreateViewPerdaIV()
        {
            try
            {
                SSQL = "SELECT * from vw_1_PerdaIIV";
                DataTable dt = _dbHelper.ExecuteDataTable(SSQL);

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                SSQL = "CREATE VIEW vw_1_PerdaIV AS Select iTAhun, IDDInas,IDurusan, IDProgram, IDKegiatan, IIDRekening, " +
                        "btJenis,case when btJenis=3 and LEFT(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN cJumlahOlah ELSE 0 END AS BelanjaPegawaiOlah," +
                        "btJenis,case when btJenis=3 and LEFT(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN cJumlah ELSE 0 END AS BelanjaPegawai," +
                        "btJenis,case when btJenis=3 and LEFT(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='521' THEN cJumlahMurni ELSE 0 END AS BelanjaPegawaiMurni," +
                        "btJenis,case when btJenis=3 and LEFT(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN cJumlahOlah ELSE 0 END AS BelanjaBarangJasaOlah," +
                        "btJenis,case when btJenis=3 and LEFT(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN cJumlah ELSE 0 END AS BelanjaBarangJasa," +
                        "btJenis,case when btJenis=3 and LEFT(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='522' THEN cJumlahMurni ELSE 0 END AS BelanjaBarangJasaMurni," +
                        "btJenis,case when btJenis=3 and LEFT(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN cJumlahOlah ELSE 0 END AS BelanjaModalOlah," +
                        "btJenis,case when btJenis=3 and LEFT(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN cJumlah ELSE 0 END AS BelanjaModal," +
                        "btJenis,case when btJenis=3 and LEFT(IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")='523' THEN cJumlahMurni ELSE 0 END AS BelanjaModalMurni " +
                        " from tAnggaranRekening_A";

                _dbHelper.ExecuteNonQuery(SSQL);

            }
        }

        public List<PerdaII> GetPerdaII(ParameterLaporan _p)
        {
            List<PerdaII> _lst = new List<PerdaII>();

            try
            {
                // Cek View 
                string namaview = CreateViewPerdaII(_p, true);

                SSQL = "select 1 AS kelompok, 1 as Urutan,A.btKodeKategori AS IDUrusan, 0 as IDDInas, 'Urusan ' + A.sNamaKategori as Nama ,  " +
                        " SUM (B.PendapatanMurni) AS SUMPendapatanOlah, SUM (B.Pendapatan) as SUMPendapatan ,  " +
                        " SUM (B.BTLMurni) AS SUMBTLOlah, SUM (BTL) as SUMBTL , SUM (B.BLMurni) AS SUMBLOlah,  " +
                        " SUM (BL) as SUMBL  from mKategori A INNER JOIN " + namaview + " B ON A.btKodeKategori= B.IDUrusan/100   " +
                        " WHere A.btKodeKategori =1 AND B.iTahun =" + _p.Tahun.ToString() + " GROUP BY A.btKodeKategori , A.sNamaKategori ";
                SSQL = SSQL + "UNION ALL select 1 AS kelompok, 2 as Urutan,A.ID AS IDUrusan, 0 as IDDInas, A.sNamaUrusan as Nama , " +
                    " SUM (B.PendapatanMurni) AS SUMPendapatanOlah,  " +
                    " SUM (B.Pendapatan) as SUMPendapatan, " +
                    " SUM (B.BTLMurni) AS SUMBTLOlah,  " +
                    " SUM (BTL) as SUMBTL , " +
                    " SUM (B.BLMurni) AS SUMBLOlah,  " +
                    " SUM (BL) as SUMBL  " +
                    " from mUrusan A INNER JOIN " + namaview + " B ON A.ID= B.IDUrusan  " +
                    " WHERE A.ID < 199 and B.iTahun = " + _p.Tahun.ToString() +
                    " GROUP BY  A.ID , A.sNamaUrusan  " +
                    " UNION ALL  " +
                    " select 1 AS kelompok, 3 as Urutan,B.IDURusan AS IDUrusan, A.ID As IDDinas, A.sNamaSKPD as Nama,  " +
                    " SUM (B.PendapatanMurni) AS SUMPendapatanOlah,  " +
                    " SUM (B.Pendapatan) as SUMPendapatan , " +
                    " SUM (B.BTLMurni) AS SUMBTLOlah,  " +
                    " SUM (BTL) as SUMBTL, " +
                    " SUM (B.BLMurni) AS SUMBLOlah,  " +
                    " SUM (BL) as SUMBL " +
                    " from mSKPD A INNER JOIN " + namaview + " B ON A.ID= B.IDDinas  " +
                    " WHERE B.IDUrusan< 199 AND B.iTahun = " + _p.Tahun.ToString() +
                    " GROUP BY  B.IDURusan , A.ID , A.sNamaSKPD  ";

                SSQL = SSQL + "UNION ALL select 2 AS kelompok, 1 as Urutan,A.btKodeKategori AS IDUrusan, 0 as IDDInas, 'Urusan ' + A.sNamaKategori as Nama ,  " +
                        " SUM (B.PendapatanMurni) AS SUMPendapatanOlah, SUM (B.Pendapatan) as SUMPendapatan , " +
                        " SUM (B.BTLMurni) AS SUMBTLOlah, SUM (BTL) as SUMBTL ,SUM (B.BLMurni) AS SUMBLOlah,  " +
                        " SUM (BL) as SUMBL  from mKategori A INNER JOIN " + namaview + " B ON A.btKodeKategori= B.IDUrusan/100   " +
                        " WHere A.btKodeKategori =2 AND B.iTahun =" + _p.Tahun.ToString() + " GROUP BY A.btKodeKategori , A.sNamaKategori ";
                SSQL = SSQL + "UNION ALL select 2 AS kelompok,2 as Urutan,A.ID AS IDUrusan, 0 as IDDInas, A.sNamaUrusan as Nama , " +
                    " SUM (B.PendapatanMurni) AS SUMPendapatanOlah,  " +
                    " SUM (B.Pendapatan) as SUMPendapatan , " +
                    " SUM (B.BTLMurni) AS SUMBTLOlah,  " +
                    " SUM (BTL) as SUMBTL , " +
                    " SUM (B.BLMurni) AS SUMBLOlah,  " +
                    " SUM (BL) as SUMBL  " +
                    " from mUrusan A INNER JOIN " + namaview + " B ON A.ID= B.IDUrusan  " +
                    " WHERE A.ID > 199 and B.iTahun = " + _p.Tahun.ToString() +
                    " GROUP BY  A.ID , A.sNamaUrusan  " +
                    " UNION ALL  " +
                    " select 2 AS kelompok,3 as Urutan,B.IDURusan AS IDUrusan, A.ID As IDDinas, A.sNamaSKPD as Nama,  " +
                    " SUM (B.PendapatanMurni) AS SUMPendapatanOlah,  " +
                    " SUM (B.Pendapatan) as SUMPendapatan , " +
                    " SUM (B.BTLMurni) AS SUMBTLOlah,  " +
                    " SUM (BTL) as SUMBTL , " +
                    " SUM (B.BLMurni) AS SUMBLOlah,  " +
                    " SUM (BL) as SUMBL  " +
                    " from mSKPD A INNER JOIN " + namaview + " B ON A.ID= B.IDDinas  " +
                    " WHERE B.IDUrusan> 199 and B.iTahun = " + _p.Tahun.ToString() +
                    " GROUP BY  B.IDURusan , A.ID , A.sNamaSKPD  " +
                    " UNION ALL  " +
                    " select 4 AS kelompok, 7 as Urutan,0 AS IDUrusan, 0 As IDDinas, 'Jumlah ' as Nama,  " +
                    " SUM (B.PendapatanMurni) AS SUMPendapatanOlah,  " +
                    " SUM (B.Pendapatan) as SUMPendapatan , " +
                    " SUM (B.BTLMurni) AS SUMBTLOlah,  " +
                    " SUM (BTL) as SUMBTL ,  " +
                    " SUM (B.BLMurni) AS SUMBLOlah,  " +
                    " SUM (BL) as SUMBL  " +
                    " from " + namaview + " B " +
                    " WHERE  B.iTahun = " + _p.Tahun.ToString() +
                    " Order by Kelompok,IDURusan , IDDInas ";

                //" UNION ALL  " +
                //    " select 5 AS kelompok, 8 as Urutan,0 AS IDUrusan, 0 As IDDinas, 'Surplus/Defisit ' as Nama,  " +
                //    " 0 AS SUMPendapatanMurni,  " +
                //    " 0 as SUMPendapatan , " +
                //    " dbo.SurplusDefisitOlahAll (" + _p.Tahun.ToString() + ") AS SUMBTLOlah,  " +
                //    " dbo.SurplusDefisitAll (" + _p.Tahun.ToString() + ") as SUMBTL, " +
                //    " 0 AS SUMBLOlah,  " +
                //    " 0 as SUMBL  Order by Kelompok,IDURusan , IDDInas ";








                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaII()
                                {
                                    Level = DataFormat.GetInteger(dr["Urutan"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]).IntToStringWithLeftPad(2),
                                    IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
                                    Kode = DataFormat.GetInteger(dr["IDDinas"]) == 0 ? DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan() : DataFormat.GetInteger(dr["IDDinas"]).ToKodeDinas(),
                                    Nama = DataFormat.GetString(dr["Nama"]).ToUpper(),
                                    Pendapatan = DataFormat.GetDecimal(dr["SumPendapatanOlah"]).ToRupiahInReport(),
                                    BelanjaTidakLangsung = DataFormat.GetDecimal(dr["SumBTLOlah"]).ToRupiahInReport(),
                                    BelanjaLangsung = DataFormat.GetDecimal(dr["SumBLOlah"]).ToRupiahInReport(),
                                    JumlahBelanja = (DataFormat.GetDecimal(dr["SumBTLOlah"]) + DataFormat.GetDecimal(dr["SumBLOlah"])).ToRupiahInReport(),
                                    PendapatanMurni = DataFormat.GetDecimal(dr["SumPendapatan"]).ToRupiahInReport(),
                                    BelanjaTidakLangsungMurni = DataFormat.GetDecimal(dr["SumBTL"]).ToRupiahInReport(),
                                    BelanjaLangsungMurni = DataFormat.GetDecimal(dr["SumBL"]).ToRupiahInReport(),
                                    JumlahBelanjaMurni = (DataFormat.GetDecimal(dr["SumBTL"]) + DataFormat.GetDecimal(dr["SumBL"])).ToRupiahInReport()

                                }).ToList();

                    }
                }
                CreateViewPerdaII(_p, false);
                return _lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        public List<PerdaII> GetPerdaIIB(ParameterLaporan _p)
        {
            List<PerdaII> _lst = new List<PerdaII>();
            try
            {
                // Cek View 
                // CreateViewPerdaII(_p.Tahap);

                // var query = from q in lstDB  group by ()
                BersihkanNonKegiatan();
                string namaView = CreateViewPerdaII_B(_p, true);

                //   string sSyaratHanyaPokok = "";


                //SSQL = "select 1 AS kelompok,A. btKodekategori * 100  as Urusan,  0 as Urutan, A.btKodeKategori AS IDUrusan, 0 as IDDInas, 'URUSAN ' + A.sNamaKategori as Nama ,  " +
                //       " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni, " +
                //       " SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni,  " +
                //       " SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni from mKategori A INNER JOIN " + namaView + " B ON A.btKodeKategori= B.IDUrusan/100  and  and A.btKodeKategori = B.IDDInas/1000000  " +
                //       " WHere  B.iTahun =" + _p.Tahun.ToString() + sSyaratHanyaPokok + " GROUP BY A.btKodeKategori , A.sNamaKategori ";
                //SSQL = SSQL + " UNION ALL select 2 AS kelompok, B.IDUrusan as Urusan, 1 as Urutan ,A.ID AS IDUrusan, 0 as IDDInas, A.sNamaUrusan as Nama , " +
                //   " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni, " +
                //   " SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni, " +
                //   " SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni " +
                //   " from mUrusan A INNER JOIN " + namaView + " B ON A.ID= B.IDUrusan  and A.ID = B.IDDInas /10000  " +
                //   " WHERE A.ID < 499 and B.iTahun = " + _p.Tahun.ToString() + sSyaratHanyaPokok + 
                //   " GROUP BY  A.ID , A.sNamaUrusan,B.IDUrusan   " +
                //   " UNION ALL  " +
                //   " select 3 AS kelompok,  B.IDURusan  as Urusan , A.ID  as Urutan,B.IDURusan AS IDUrusan, A.ID As IDDinas, A.sNamaSKPD as Nama,  " +
                //   " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni, " +
                //   " SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni, " +
                //   " SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni " +
                //   " from mSKPD A INNER JOIN " + namaView + " B ON A.ID= B.IDDinas   AND A.ID/10000 = B.IDDINAS/10000 AND A.ID /10000 =B.IDurusan" +
                //   " WHERE B.IDUrusan< 499 AND a.Root = 1  AND B.iTahun = " + _p.Tahun.ToString() + sSyaratHanyaPokok + 
                //   " group by A.ID/100,A.ID, B.IDUrusan, A.sNamaSKPD";

                //SSQL = SSQL + " UNION ALL  " +
                //      " select 5 AS kelompok,888 as Urusan, 1 as Urutan,0 AS IDUrusan, 0 As IDDinas, 'Jumlah ' as Nama,  " +
                //      " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni, " +
                //      " SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni, " +
                //      " SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni " +
                //      " from " + namaView + " B " +
                //      " inner join mUrusan ON  mUrusan.ID= B.IDUrusan  inner join mSKPD ON mSKPD.ID= B.IDDInas and mUrusan.ID = B.IDDInas /10000  WHERE  B.iTahun = " + _p.Tahun.ToString() + sSyaratHanyaPokok;


                //SSQL = SSQL + " UNION ALL  " +
                //        " select 6 AS kelompok, 999 as Urusan, 1 as Urutan ,0 AS IDUrusan, 0 As IDDinas, 'SURPLUS/DEFISIT ' as Nama,  " +
                //        " (SELECT SUM (PendapatanMUrni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + ")  -" +
                //        " (SELECT SUM (BTLMUrni)  from " + namaView + "  WHERE iTahun = " + _p.Tahun.ToString() + " )  - " +
                //        "  (SELECT SUM (BLMUrni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + " )  as SUMPendapatan , 0 as SUMPendapatanMUrni, " +
                //        " 0 as SUMBTL , 0  as SUMBTLMUrni, " +
                //        " 0 as SUMBL , 0 as SUMBLMUrni   Order by Urusan, Urutan ";


                //  if (_p.HanyaUrusanPokok== false )
                //{
                //    SSQL = "select 1 AS kelompok,A. btKodekategori * 100  as Urusan,  0 as Urutan, A.btKodeKategori AS IDUrusan, 0 as IDDInas, 'URUSAN ' + A.sNamaKategori as Nama ,  " +
                //           " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni, " +
                //           " SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni,  " +
                //           " SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni from mKategori A INNER JOIN " + namaView + " B ON A.btKodeKategori= B.IDUrusan/100  " +
                //           " WHere  B.iTahun =" + _p.Tahun.ToString() + " GROUP BY A.btKodeKategori , A.sNamaKategori ";
                //    SSQL = SSQL + " UNION ALL select 2 AS kelompok, B.IDUrusan as Urusan, 1 as Urutan ,A.ID AS IDUrusan, 0 as IDDInas, A.sNamaUrusan as Nama , " +
                //       " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni, " +
                //       " SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni, " +
                //       " SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni " +
                //       " from mUrusan A INNER JOIN " + namaView + " B ON A.ID= B.IDUrusan  " +

                //       " WHERE A.ID < 499 and B.iTahun = " + _p.Tahun.ToString() +
                //       " GROUP BY  A.ID , A.sNamaUrusan,B.IDUrusan   " +
                //       " UNION ALL  " +
                //       " select 3 AS kelompok,  B.IDURusan  as Urusan , A.ID  as Urutan,B.IDURusan AS IDUrusan, A.ID As IDDinas, A.sNamaSKPD as Nama,  " +
                //       " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni, " +
                //       " SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni, " +
                //       " SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni " +
                //       " from mSKPD A INNER JOIN " + namaView + " B ON A.ID= B.IDDinas   " +
                //       " WHERE B.IDUrusan< 499 AND a.Root = 1  AND B.iTahun = " + _p.Tahun.ToString() +
                //       " group by A.ID/100,A.ID, B.IDUrusan, A.sNamaSKPD";

                //    SSQL = SSQL + " UNION ALL  " +
                //          " select 5 AS kelompok,888 as Urusan, 1 as Urutan,0 AS IDUrusan, 0 As IDDinas, 'Jumlah ' as Nama,  " +
                //          " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni, " +
                //          " SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni, " +
                //          " SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni " +
                //          " from " + namaView + " B " +
                //          " inner join mUrusan ON  mUrusan.ID= B.IDUrusan  inner join mSKPD ON mSKPD.ID= B.IDDInas WHERE  B.iTahun = " + _p.Tahun.ToString();


                //    SSQL = SSQL + " UNION ALL  " +
                //            " select 6 AS kelompok, 999 as Urusan, 1 as Urutan ,0 AS IDUrusan, 0 As IDDinas, 'SURPLUS/DEFISIT ' as Nama,  " +
                //            " (SELECT SUM (PendapatanMUrni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + ")  -" +
                //            " (SELECT SUM (BTLMUrni)  from " + namaView + "  WHERE iTahun = " + _p.Tahun.ToString() + " )  - " +
                //            "  (SELECT SUM (BLMUrni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + " )  as SUMPendapatan , 0 as SUMPendapatanMUrni, " +
                //            " 0 as SUMBTL , 0  as SUMBTLMUrni, " +
                //            " 0 as SUMBL , 0 as SUMBLMUrni   Order by Urusan, Urutan ";


                //}
                //else
                //{
                if (Tahun < 2020)
                {
                    SSQL = "select B.IDDInas/1000000 AS kelompok, 1 as Level, B.IDDInas/1000000  as Urusan,  0 as Urutan, B.IDDInas/1000000 AS IDUrusan, 0 as IDDInas," +
                            " 'Urusan ' + A.sNamaKategori as Nama ,   SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni,  SUM (BTL) as SUMBTL , " +
                            " SUM (BTLMUrni) as SUMBTLMUrni,   SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni from mKategori A INNER JOIN " + namaView + " B ON A.btKodeKategori= B.IDDInas/1000000 " +
                            "  WHere  B.iTahun =" + _p.Tahun.ToString() + "  GROUP BY A.btKodeKategori , B.IDDInas/1000000,A.sNamaKategori ";

                    SSQL = SSQL + "UNION ALL select B.IDDInas/1000000 AS kelompok, 2 as Level, B.IDDInas/10000  as Urusan, 1 as Urutan ,B.IDDInas/10000 AS IDUrusan, 0 as IDDInas, A.sNamaUrusan as Nama ,  " +
                                " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni,  SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni,   " +
                                "  SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni  from mUrusan A INNER JOIN " + namaView + " B ON A.ID= B.IDDInas/10000  " +
                                 " WHERE A.ID < 499 and B.iTahun = " + _p.Tahun.ToString() + " GROUP BY  B.IDDInas/1000000,A.ID , B.IDDInas/10000, A.sNamaUrusan ";

                    SSQL = SSQL + " UNION ALL  " +
                       " select B.IDDInas/1000000 AS kelompok,  3 as Level, B.IDDInas/10000 as Urusan , A.ID  as Urutan,B.IDDInas/10000 AS IDUrusan, A.ID As IDDinas,  " +
                        " A.sNamaSKPD as Nama,   SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni,  SUM (BTL) as SUMBTL ,  " +
                        " SUM (BTLMUrni) as SUMBTLMUrni,  SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni  from mSKPD A INNER JOIN " + namaView + " B  " +
                        " ON A.ID= B.IDDinas    WHERE B.IDUrusan< 499 AND a.Root = 1  AND B.iTahun = " + _p.Tahun.ToString() + " group by B.IDDInas/1000000,A.ID,B.IDDInas/10000,  A.sNamaSKPD ";

                    SSQL = SSQL + " UNION ALL  " +
                                   " select 5 AS kelompok,5 as Level, 888 as Urusan,  1 as Urutan,0 AS IDUrusan, 0 As IDDinas, 'Jumlah ' as Nama,   SUM (B.Pendapatan) as SUMPendapatan ,  " +
                                    " SUM (PendapatanMUrni) as SUMPendapatanMUrni,  SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni,  SUM (BL) as SUMBL , " +
                                    "SUM (BLMUrni) as SUMBLMUrni  from " + namaView + " B  inner join mUrusan ON  mUrusan.ID= B.IDUrusan  inner join mSKPD ON " +
                                    " mSKPD.ID= B.IDDInas WHERE  B.iTahun = " + _p.Tahun.ToString();




                    SSQL = SSQL + " UNION ALL  " +
                            " select 6 AS kelompok, 6 as Level,999 as Urusan,  1 as Urutan ,0 AS IDUrusan, 0 As IDDinas, 'SURPLUS/DEFISIT ' as Nama,  " +
                            " (SELECT SUM (PendapatanMUrni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + ")  -" +
                            " (SELECT SUM (BTLMUrni)  from " + namaView + "  WHERE iTahun = " + _p.Tahun.ToString() + " )  - " +
                            "  (SELECT SUM (BLMUrni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + " )  as SUMPendapatan , 0 as SUMPendapatanMUrni, " +
                            " 0 as SUMBTL , 0  as SUMBTLMUrni, " +
                            " 0 as SUMBL , 0 as SUMBLMUrni    order by Kelompok,IDUrusan,Urutan ";

                }
                else
                {
                    SSQL = "select B.Parent/1000000 AS kelompok, 1 as Level, B.Parent/1000000  as Urusan,  0 as Urutan, B.Parent/1000000 AS IDUrusan, 0 as IDDInas," +
                            " 'Urusan ' + A.sNamaKategori as Nama ,   SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni,  SUM (BTL) as SUMBTL , " +
                            " SUM (BTLMUrni) as SUMBTLMUrni,   SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni from mKategori A INNER JOIN " + namaView + " B ON A.btKodeKategori= B.Parent/1000000 " +
                            "  WHere  B.iTahun =" + _p.Tahun.ToString() + "  GROUP BY A.btKodeKategori , B.Parent/1000000,A.sNamaKategori ";

                    SSQL = SSQL + "UNION ALL select B.Parent/1000000 AS kelompok, 2 as Level, B.Parent/10000  as Urusan, 1 as Urutan ,B.Parent/10000 AS IDUrusan, 0 as IDDInas, A.sNamaUrusan as Nama ,  " +
                                " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni,  SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni,   " +
                                "  SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni  from mUrusan A INNER JOIN " + namaView + " B ON A.ID= B.Parent/10000  " +
                                 " WHERE A.ID < 499 and B.iTahun = " + _p.Tahun.ToString() + " GROUP BY  B.Parent/1000000,A.ID , B.Parent/10000, A.sNamaUrusan ";

                    SSQL = SSQL + " UNION ALL  " +
                       " select B.Parent/1000000 AS kelompok,  3 as Level, B.Parent/10000 as Urusan , A.ID  as Urutan,B.Parent/10000 AS IDUrusan, A.Parent As IDDinas,  " +
                        " A.sNamaSKPD as Nama,   SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni,  SUM (BTL) as SUMBTL ,  " +
                        " SUM (BTLMUrni) as SUMBTLMUrni,  SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni  from mSKPD A INNER JOIN " + namaView + " B  " +
                        " ON A.Parent= B.Parent    WHERE B.IDUrusan< 499 AND a.Root = 1  AND A.ID=A.Parent AND B.iTahun = " + _p.Tahun.ToString() + " group by A.Parent,B.Parent/1000000,A.ID,B.Parent/10000,  A.sNamaSKPD ";

                    SSQL = SSQL + " UNION ALL  " +
                                   " select 5 AS kelompok,5 as Level, 888 as Urusan,  1 as Urutan,0 AS IDUrusan, 0 As IDDinas, 'Jumlah ' as Nama,   SUM (B.Pendapatan) as SUMPendapatan ,  " +
                                    " SUM (PendapatanMUrni) as SUMPendapatanMUrni,  SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni,  SUM (BL) as SUMBL , " +
                                    "SUM (BLMUrni) as SUMBLMUrni  from " + namaView + " B  inner join mUrusan ON  mUrusan.ID= B.IDUrusan  inner join mSKPD ON " +
                                    " mSKPD.ID= B.IDDInas WHERE  B.iTahun = " + _p.Tahun.ToString();




                    SSQL = SSQL + " UNION ALL  " +
                            " select 6 AS kelompok, 6 as Level,999 as Urusan,  1 as Urutan ,0 AS IDUrusan, 0 As IDDinas, 'SURPLUS/DEFISIT ' as Nama,  " +
                            " (SELECT SUM (PendapatanMUrni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + ")  -" +
                            " (SELECT SUM (BTLMUrni)  from " + namaView + "  WHERE iTahun = " + _p.Tahun.ToString() + " )  - " +
                            "  (SELECT SUM (BLMUrni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + " )  as SUMPendapatan , 0 as SUMPendapatanMUrni, " +
                            " 0 as SUMBTL , 0  as SUMBTLMUrni, " +
                            " 0 as SUMBL , 0 as SUMBLMUrni    order by Kelompok,IDUrusan,Urutan ";

                }

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaII()
                                {

                                    //Kode { set; get; }
                                    Level = DataFormat.GetInteger(dr["Level"]),
                                    //IDKategori { set; get; }
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]).IntToStringWithLeftPad(2),
                                    IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
                                    //root { set; get; }
                                    Kode = DataFormat.GetInteger(dr["IDDinas"]) == 0 ? DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan() : DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan() + " " + DataFormat.GetInteger(dr["IDDinas"]).ToKodeDinas(),
                                    Nama = DataFormat.GetString(dr["Nama"]).ToUpper(),
                                    Pendapatan = DataFormat.GetDecimal(dr["SumPendapatan"]).ToRupiahInReport(),
                                    BelanjaTidakLangsung = DataFormat.GetDecimal(dr["SumBTL"]).ToRupiahInReport(),
                                    BelanjaLangsung = DataFormat.GetDecimal(dr["SumBL"]).ToRupiahInReport(),
                                    JumlahBelanja = (DataFormat.GetDecimal(dr["SumBTL"]) + DataFormat.GetDecimal(dr["SumBL"])).ToRupiahInReport(),
                                    PendapatanMurni = DataFormat.GetDecimal(dr["SumPendapatanMurni"]).ToRupiahInReport(),
                                    BelanjaTidakLangsungMurni = DataFormat.GetDecimal(dr["SumBTLMurni"]).ToRupiahInReport(),
                                    BelanjaLangsungMurni = DataFormat.GetDecimal(dr["SumBLMurni"]).ToRupiahInReport(),
                                    JumlahBelanjaMurni = (DataFormat.GetDecimal(dr["SumBTLMurni"]) + DataFormat.GetDecimal(dr["SumBLMurni"])).ToRupiahInReport(),
                                    SelisihPendapatan = (DataFormat.GetDecimal(dr["SumPendapatan"]) - DataFormat.GetDecimal(dr["SumPendapatanMurni"])).ToRupiahInReport(),
                                    SelisihBelanja = (DataFormat.GetDecimal(dr["SumBTL"]) + DataFormat.GetDecimal(dr["SumBL"]) - DataFormat.GetDecimal(dr["SumBLMurni"]) - DataFormat.GetDecimal(dr["SumBTLMurni"])).ToRupiahInReport(),
                                    PersenPendapatan = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["SumPendapatan"]), DataFormat.GetDecimal(dr["SumPendapatanMurni"])),
                                    PersenBelanja = DataFormat.GetProsentase((DataFormat.GetDecimal(dr["SumBTL"]) + DataFormat.GetDecimal(dr["SumBL"])), (DataFormat.GetDecimal(dr["SumBLMurni"]) + DataFormat.GetDecimal(dr["SumBTLMurni"])))

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

        //public List<PerdaII> GetPerdaPembiayaanII(ParameterLaporan _p)
        //{
        //    List<PerdaII> _lst = new List<PerdaII>();
        //    try
        //    {
        //        // Cek View 
        //        string namaView;

        //        namaView = CreateViewPerdaII_B(_p, true);


        //        SSQL = "select 1 AS kelompok,(A.btKodeKategori * 1000000) as Urutan, A.btKodeKategori AS IDUrusan, 0 as IDDInas, 'URUSAN ' + A.sNamaKategori as Nama ,  " +
        //               " SUM (B.Terima) as SUMPendapatan , SUM (TerimaMUrni) as SUMPendapatanMUrni, " +
        //               " SUM (Bayar) as SUMBTL , SUM (BayarMurni) as SUMBTLMUrni,  " +
        //               " SUM (B.Terima)-SUM (Bayar) as SUMBL , SUM (B.TerimaMurni)-SUM (BayarMurni)  as SUMBLMUrni from mKategori A INNER JOIN " + namaView + " B ON A.btKodeKategori= B.IDUrusan/100   " +
        //               " WHere  B.iTahun =" + _p.Tahun.ToString() + " GROUP BY A.btKodeKategori , A.sNamaKategori having SUM (B.Terima)>0 or SUM (B.TerimaMurni)>0 or SUM (B.Bayar)>0 ";

        //        SSQL = SSQL + " UNION ALL select 2 AS kelompok,  (a.id * 10000  ) as Urutan ,A.ID AS IDUrusan, 0 as IDDInas, A.sNamaUrusan as Nama , " +
        //            " SUM (B.Terima) as SUMPendapatan , SUM (TerimaMUrni) as SUMPendapatanMUrni, " +
        //               " SUM (Bayar) as SUMBTL , SUM (BayarMurni) as SUMBTLMUrni,  " +
        //               " SUM (B.Terima)-SUM (Bayar) as SUMBL , SUM (B.TerimaMurni)-SUM (BayarMurni)  as SUMBLMUrni  " +
        //           " from mUrusan A INNER JOIN " + namaView + " B ON A.ID= B.IDUrusan  " +
        //           " WHERE A.ID < 499 and B.iTahun = " + _p.Tahun.ToString() +
        //           " GROUP BY  A.ID , A.sNamaUrusan  having SUM (B.Terima)>0 or SUM (B.TerimaMurni)>0 or SUM (B.Bayar)>0  " +
        //           " UNION ALL  " +
        //           " select 3 AS kelompok, ( A.ID/100  ) * 100 as Urutan,B.IDURusan AS IDUrusan, A.ID As IDDinas, A.sNamaSKPD as Nama,  " +
        //            " SUM (B.Terima) as SUMPendapatan , SUM (TerimaMUrni) as SUMPendapatanMUrni, " +
        //               " SUM (Bayar) as SUMBTL , SUM (BayarMurni) as SUMBTLMUrni,  " +
        //               " SUM (B.Terima)-SUM (Bayar) as SUMBL , SUM (B.TerimaMurni)-SUM (BayarMurni)  as SUMBLMUrni  " +
        //           " from mSKPD A INNER JOIN " + namaView + " B ON A.ID/100= B.IDDinas/100  " +
        //           " WHERE B.IDUrusan< 499 AND a.Root = 0  AND B.iTahun = " + _p.Tahun.ToString() +
        //           " group by A.ID/100,A.ID, B.IDUrusan, A.sNamaSKPD  having SUM (B.Terima)>0 or SUM (B.TerimaMurni)>0 or SUM (B.Bayar)>0 ";

        //        SSQL = SSQL + " UNION ALL  " +
        //   " select 6 AS kelompok, 10000000001 as Urutan ,0 AS IDUrusan, 0 As IDDinas, 'SISA LEBIH PEMBIAYAAN ANGGARAN TAHUN ANGGARAN BERKENAN ' as Nama,  " +
        //   " (SELECT SUM (PendapatanMurni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + ")  + " +
        //   " (SELECT SUM (TerimaMurni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + ")  -" +
        //   " (SELECT SUM (BTLMurni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + ")  -" +
        //   " (SELECT SUM (BLMurni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + ")  -" +
        //   " (SELECT SUM (BayarMurni)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + ") as SUMPendapatan,  " +
        //    " 0 as SUMPendapatanMUrni, " +
        //   " 0 as SUMBTL , 0  as SUMBTLMUrni, " +
        //                 " 0 as SUMBL , 0 as SUMBLMUrni   Order by Urutan ";

        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                _lst = (from DataRow dr in dt.Rows
        //                        select new PerdaII()
        //                        {
        //                            //Kode { set; get; }
        //                            Level = DataFormat.GetInteger(dr["Kelompok"]),
        //                            //IDKategori { set; get; }
        //                            IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
        //                            IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
        //                            //root { set; get; }
        //                            Kode = DataFormat.GetInteger(dr["IDDinas"]) == 0 ? DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan() : DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan() + " " + DataFormat.GetInteger(dr["IDDinas"]).ToKodeDinas(),
        //                            Nama = DataFormat.GetString(dr["Nama"]).ToUpper(),
        //                            Pendapatan = DataFormat.GetDecimal(dr["SumPendapatan"]).ToRupiahInReport(),
        //                            BelanjaTidakLangsung = DataFormat.GetDecimal(dr["SumBTL"]).ToRupiahInReport(),
        //                            BelanjaLangsung = DataFormat.GetDecimal(dr["SumBL"]).ToRupiahInReport(),
        //                            JumlahBelanja = (DataFormat.GetDecimal(dr["SumBTL"]) + DataFormat.GetDecimal(dr["SumBL"])).ToRupiahInReport(),
        //                            PendapatanMurni = DataFormat.GetDecimal(dr["SumPendapatanMurni"]).ToRupiahInReport(),
        //                            BelanjaTidakLangsungMurni = DataFormat.GetDecimal(dr["SumBTLMurni"]).ToRupiahInReport(),
        //                            BelanjaLangsungMurni = DataFormat.GetDecimal(dr["SumBLMurni"]).ToRupiahInReport(),
        //                            JumlahBelanjaMurni = (DataFormat.GetDecimal(dr["SumBTLMurni"]) + DataFormat.GetDecimal(dr["SumBLMurni"])).ToRupiahInReport()


        //                        }).ToList();


        //            }
        //        }
        //        return _lst;
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message;
        //        return _lst;
        //    }
        //}
        //public List<PerdaII> GetPerdaPembiayaanIIByOrg(ParameterLaporan _p)
        //{
        //    List<PerdaII> _lst = new List<PerdaII>();
        //    try
        //    {
        //        // Cek View 
        //        string namaView;

        //        namaView = CreateViewPerdaII_B(_p, true);





        //        SSQL = " select 1 AS kelompok, cast( ( A.ID/100  ) * 1000 as varchar(10))  as Urutan,B.IDUrusan, A.ID As IDDinas, A.sNamaSKPD as Nama,  " +
        //           " SUM (B.Terima) as SUMPendapatan , SUM (TerimaMUrni) as SUMPendapatanMUrni, " +
        //           " SUM (Bayar) as SUMBTL , SUM (BayarMUrni) as SUMBTLMUrni, " +
        //           " SUM (Terima-Bayar) as SUMBL , SUM (TerimaMurni-BayarMurni) as SUMBLMUrni " +
        //           " from mSKPD A INNER JOIN " + namaView + " B ON A.ID/100= B.IDDinas/100  " +
        //           " WHERE B.IDUrusan< 499 AND a.Root = 0  AND B.iTahun = " + _p.Tahun.ToString() +
        //           " group by A.ID/100,A.ID, B.IDUrusan, A.sNamaSKPD having SUM (B.Terima)>0 or SUM (B.TerimaMurni)>0 or SUM (B.Bayar)>0 ";




        //        SSQL = SSQL + " UNION ALL select 2 AS kelompok, cast(B.IDDInas  as varchar(8)) + cast(A.ID  as char(3)) +'1'  as Urutan ,A.ID AS IDUrusan, 0 as IDDInas, C.sNamaKategori + ' ' + A.sNamaUrusan as Nama , " +
        //           " SUM (B.Terima) as SUMPendapatan , SUM (TerimaMUrni) as SUMPendapatanMUrni, " +
        //           " SUM (Bayar) as SUMBTL , SUM (BayarMUrni) as SUMBTLMUrni, " +
        //           " SUM (Terima-Bayar) as SUMBL , SUM (TerimaMurni-BayarMurni) as SUMBLMUrni " +
        //           " from mUrusan A INNER JOIN " + namaView + " B ON A.ID= B.IDUrusan Inner join mKategori C ON A.btKodekategori = C.btKodekategori  " +
        //           " WHERE A.ID < 499 and B.iTahun = " + _p.Tahun.ToString() +
        //           " GROUP BY  A.ID , B.IDDInas, A.sNamaUrusan,C.sNamaKategori having SUM (B.Terima)>0 or SUM (B.TerimaMurni)>0 or SUM (B.Bayar)>0  " +
        //           "   ";


        //        SSQL = SSQL + " UNION ALL  " +
        //                " select 6 AS kelompok, '999999999999999991' as Urutan ,0 AS IDUrusan, 0 As IDDinas, 'SURPLUS/DEFISIT ' as Nama,  " +
        //                "  SUM (Terima+Pendapatan- BTL-BL-Bayar)  as SUMPendapatan , " +
        //                " SUM (TerimaMUrni+PendapatanMurni- BTLMUrni-BLMurni-BayarMurni)   as SUMPendapatanMUrni, " +
        //                " 0 as SUMBTL , 0  as SUMBTLMUrni, " +
        //                              " 0 as SUMBL , 0 as SUMBLMUrni   from viewPerdaIIpudjo having SUM (Terima)>0 or SUM (TerimaMurni)>0 or SUM (Bayar)>0  Order by Urutan ";






        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                _lst = (from DataRow dr in dt.Rows
        //                        select new PerdaII()
        //                        {
        //                            //Kode { set; get; }
        //                            Level = DataFormat.GetInteger(dr["Kelompok"]),
        //                            //IDKategori { set; get; }
        //                            IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
        //                            IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
        //                            //root { set; get; }
        //                            Kode = DataFormat.GetInteger(dr["IDDinas"]) == 0 ? DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan() : DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan() + " " + DataFormat.GetInteger(dr["IDDinas"]).ToKodeDinas(),
        //                            Nama = DataFormat.GetString(dr["Nama"]).ToUpper(),
        //                            Pendapatan = DataFormat.GetDecimal(dr["SumPendapatan"]).ToRupiahInReport(),
        //                            BelanjaTidakLangsung = DataFormat.GetDecimal(dr["SumBTL"]).ToRupiahInReport(),
        //                            BelanjaLangsung = DataFormat.GetDecimal(dr["SumBL"]).ToRupiahInReport(),
        //                            JumlahBelanja = (DataFormat.GetDecimal(dr["SumBTL"]) + DataFormat.GetDecimal(dr["SumBL"])).ToRupiahInReport(),
        //                            PendapatanMurni = DataFormat.GetDecimal(dr["SumPendapatanMurni"]).ToRupiahInReport(),
        //                            BelanjaTidakLangsungMurni = DataFormat.GetDecimal(dr["SumBTLMurni"]).ToRupiahInReport(),
        //                            BelanjaLangsungMurni = DataFormat.GetDecimal(dr["SumBLMurni"]).ToRupiahInReport(),
        //                            JumlahBelanjaMurni = (DataFormat.GetDecimal(dr["SumBTLMurni"]) + DataFormat.GetDecimal(dr["SumBLMurni"])).ToRupiahInReport()


        //                        }).ToList();


        //            }
        //        }
        //        return _lst;
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message;
        //        return _lst;
        //    }
        //}
        //public List<PerdaII> GetPerdaPembiayaanII(ParameterLaporan _p)
        //{
        //    List<PerdaII> _lst = new List<PerdaII>();
        //    try
        //    {
        //        // Cek View 
        //       // CreateViewPerdaII();

        //        SSQL = "select B.btJenis, 1 as Urutan,A.btKodeKategori AS IDUrusan, 0 as IDDInas, 'Urusan ' + A.sNamaKategori as Nama ,  " +
        //                " SUM (B.TerimaOlah) AS SUMPenerimaanOlah, SUM (B.Terima) as SUMPenerimaan , SUM (TerimaMUrni) as SUMPenerimaanMUrni, " +
        //                " SUM (B.BayarOlah) AS SUMPembayaranOlah, SUM (Bayar) as SUMPembayaran , SUM (BayarMUrni) as SUMPembayaranMUrni,SUM (B.TerimaOlah-B.BayarOlah)  AS SUMBLOlah,  " +
        //                " SUM (B.Terima-B.Bayar)   as SUMBL , SUM (B.TerimaMurni-B.BayarMurni)  as SUMBLMUrni,  0 as SilpaTBMurni,  " +
        //                " 0 as SilpaTBOlah,   0 as SilpaTBOlah   from mKategori A INNER JOIN vw_1_PerdaII_B B ON A.btKodeKategori= B.IDUrusan/100   " +
        //                " WHere A.btKodeKategori =1 AND B.iTahun =" + _p.Tahun.ToString() + " AND B.btJenis in (5,6) GROUP BY B.iTahun, B.btJenis,A.btKodeKategori , A.sNamaKategori ";
        //        SSQL = SSQL + "UNION ALL select B.btJenis,  2 as Urutan,A.ID AS IDUrusan, 0 as IDDInas, A.sNamaUrusan as Nama , " +
        //            " SUM (B.TerimaOlah) AS SUMPenerimaanOlah, SUM (B.Terima) as SUMPenerimaan , SUM (TerimaMUrni) as SUMPenerimaanMUrni, " +
        //                " SUM (B.BayarOlah) AS SUMPembayaranOlah, SUM (Bayar) as SUMPembayaran , SUM (BayarMUrni) as SUMPembayaranMUrni,SUM (B.TerimaOlah-B.BayarOlah)  AS SUMBLOlah,  " +
        //                " SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni,  0 as SilpaTBMurni,  " +
        //                " 0 as SilpaTBOlah,   0 as SilpaTBOlah  from mUrusan A INNER JOIN vw_1_PerdaII_B B ON A.ID= B.IDUrusan  " +
        //                " WHERE A.ID < 199 and B.iTahun = " + _p.Tahun.ToString() +
        //                "  AND B.btJenis in (5,6)  GROUP BY  B.btJEnis,B.iTahun,A.ID , A.sNamaUrusan  " +
        //                " UNION ALL  " +
        //                " select B.btJenis,  3 as Urutan,B.IDURusan AS IDUrusan, A.ID As IDDinas, A.sNamaSKPD as Nama,  " +
        //                " SUM (B.TerimaOlah) AS SUMPenerimaanOlah, SUM (B.Terima) as SUMPenerimaan , SUM (TerimaMUrni) as SUMPenerimaanMUrni, " +
        //                " SUM (B.BayarOlah) AS SUMPembayaranOlah, SUM (Bayar) as SUMPembayaran , SUM (BayarMUrni) as SUMPembayaranMUrni,SUM (B.TerimaOlah-B.BayarOlah)  AS SUMBLOlah,  " +
        //                " SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni,  0 as SilpaTBMurni,  " +
        //                " 0 as SilpaTBOlah,   0 as SilpaTBOlah   from mSKPD A INNER JOIN vw_1_PerdaII_B B ON A.ID= B.IDDinas  " +
        //                " WHERE B.IDUrusan< 199 AND B.iTahun = " + _p.Tahun.ToString() +
        //                "  AND B.btJenis in (5,6)  GROUP BY  B.btJenis, B.IDURusan , A.ID , A.sNamaSKPD  " +
        //                " ORDER BY B.btJEnis, Urutan, IDUrusan, IDDinas";

        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                if (_p.Tahap == 0 || _p.Tahap == 2)
        //                {
        //                    _lst = (from DataRow dr in dt.Rows
        //                            select new PerdaII()
        //                            {

        //                                Kode = DataFormat.GetInteger(dr["IDDinas"]) == 0 ? DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan() : DataFormat.GetInteger(dr["IDDinas"]).ToKodeDinas(),
        //                                Level = DataFormat.GetInteger(dr["Urutan"]),
        //                                //IDKategori { set; get; }
        //                                IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
        //                                IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
        //                                //root { set; get; }
        //                                Nama = DataFormat.GetString(dr["Nama"]),
        //                                Penerimaan = DataFormat.GetDecimal(dr["SUMPenerimaan"]).ToRupiahInReport(),
        //                                Pembayaran = DataFormat.GetDecimal(dr["SumPembayaran"]).ToRupiahInReport(),
        //                                BelanjaLangsung ="0",// DataFormat.GetDecimal(dr["SumBLOlah"]).ToRupiahInReport(),
        //                                PenerimaanMurni = DataFormat.GetDecimal(dr["SUMPenerimaanMurni"]).ToRupiahInReport(),
        //                                PembayaranMurni = DataFormat.GetDecimal(dr["SumPembayaranMurni"]).ToRupiahInReport(),
        //                                BelanjaLangsungMurni = "0",//DataFormat.GetDecimal(dr["SumBLMurni"]).ToRupiahInReport(),
        //                                JumlahBelanjaMurni = "0",//(DataFormat.GetDecimal(dr["SumBTLMurni"]) + DataFormat.GetDecimal(dr["SumBLMurni"])).ToRupiahInReport(),
        //                                SilpaTBMurni = (DataFormat.GetDecimal(dr["SilpaTBMurni"]).ToRupiahInReport()),
        //                                SilpaTB = (DataFormat.GetDecimal(dr["SilpaTBOlah"]).ToRupiahInReport())


        //                            }).ToList();
        //                }
        //                else
        //                {
        //                    _lst = (from DataRow dr in dt.Rows
        //                            select new PerdaII()
        //                            {
        //                                Level = DataFormat.GetInteger(dr["Urutan"]),
        //                                Kode = DataFormat.GetInteger(dr["IDDinas"]) == 0 ? DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan() : DataFormat.GetInteger(dr["IDDinas"]).ToKodeDinas(),
        //                                IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
        //                                IDDInas = DataFormat.GetInteger(dr["IDDinas"]),                                        
        //                                Nama = DataFormat.GetString(dr["Nama"]),
        //                                Penerimaan = DataFormat.GetDecimal(dr["SUMPenerimaanOlah"]).ToRupiahInReport(),
        //                                Pembayaran = DataFormat.GetDecimal(dr["SumPembayaranOlah"]).ToRupiahInReport(),
        //                                BelanjaLangsung = "0",// DataFormat.GetDecimal(dr["SumBLOlah"]).ToRupiahInReport(),
        //                                PenerimaanMurni = DataFormat.GetDecimal(dr["SUMPenerimaanMurni"]).ToRupiahInReport(),
        //                                PembayaranMurni = DataFormat.GetDecimal(dr["SumPembayaranMurni"]).ToRupiahInReport(),
        //                                BelanjaLangsungMurni = "0",//DataFormat.GetDecimal(dr["SumBLMurni"]).ToRupiahInReport(),
        //                                JumlahBelanjaMurni = "0",//(DataFormat.GetDecimal(dr["SumBTLMurni"]) + DataFormat.GetDecimal(dr["SumBLMurni"])).ToRupiahInReport(),
        //                                SilpaTBMurni = (DataFormat.GetDecimal(dr["SilpaTBMurni"]).ToRupiahInReport()),
        //                                SilpaTB = (DataFormat.GetDecimal(dr["SilpaTBOlah"]).ToRupiahInReport())


        //                            }).ToList();

        //                }
        //            }
        //        }
        //        return _lst;
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message;
        //        return _lst;
        //    }
        //}

        public List<Penjabaran> GetPenjabaran(ParameterLaporan _p)
        {
            List<Penjabaran> _lst = new List<Penjabaran>();
            try
            {
                // Cek View 
                //CreateViewPerdaII();

                SSQL = "Select 0 as Urutan , A.iTahun, A.IDDInas,A.IDUrusan,A.btJenis ,A.IDPRogram, A.IDKEgiatan, A.IIDRekening,A.Rek as IDRek, A.Root as btRoot, " +
                        " sNamaRekening,  JumlahOlah, Jumlah, JumlahMurni,  '' as Label, '' as sUraian, 0 as JumlahUraian, 0 as btUrut,0 as Level from [vwAnggaranAllLevel] A " +
                        " WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND A.IDDInas=" + _p.IDDinas.ToString() + " AND A.btJenis =1 AND A.btJenis  in " + _p.Jenis;

                SSQL = SSQL + " UNION ALL Select 1 as Urutan, iTahun, IDDInas,IDUrusan,JEnis as btjenis,IDPRogram, IDKEgiatan, IIDRekening,IIDRekening as IDRek,6 as btRoot,'' " +
                        " as sNamaRekening,  0 as JumlahOlah,0 as Jumlah, 0 as JumlahMurni,  sLabel as Label, sUraian,JumlahOlah as JumlahUraian , btUrut  ,Level FROM tAnggaranUraian_A " +
                        " WHERE iTAhun = " + _p.Tahun.ToString() + " AND IDDInas=" + _p.IDDinas.ToString() + " AND Jenis =1  AND Jenis  in " + _p.Jenis;

                //AND ShowInReport=1 

                SSQL = SSQL + " UNION ALL Select 2 as Urutan , A.iTahun, A.IDDInas, 0 AS IDURusan, 2 as btJenis, 0 as IDProgram, 0 as IDKegiatan,  0 as IIDRekening,0 as IDRek,0 as Root, " +
                        " 'BELANJA DAERAH' as sNamaRekening, SUM(A.JumlahOlah) as JumlahOlah, SUM(A.Jumlah) as Jumlah,  SUM(A.JumlahMurni) as JumlahMurni,'' as Label, '' " +
                        " as sUraian , 0 as JumlahUraian, 0 as btUrut ,0 as Level from  [vwAnggaranAllLevel] A  " +
                        " WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND A.IDDInas=" + _p.IDDinas.ToString() + " AND A.btJenis IN (2,3)   AND A.btJenis  in " + _p.Jenis +
                        " GROUP BY A.iTahun, A.IDDInas";


                SSQL = SSQL + " UNION ALL Select 3 as Urutan , A.iTahun, A.IDDInas,A.IDUrusan,A.btJenis ,A.IDPRogram, A.IDKEgiatan, A.IIDRekening,A.Rek as IDRek, A.Root as btRoot, " +
                        " sNamaRekening,  JumlahOlah, Jumlah, JumlahMurni, '' as Label, '' as sUraian, 0 as JumlahUraian, 0 as btUrut,0 as Level from [vwAnggaranAllLevel] A " +
                        " WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND A.IDDInas=" + _p.IDDinas.ToString() + " AND A.btJenis =2 and Root>2  AND A.btJenis  in " + _p.Jenis;

                SSQL = SSQL + " UNION ALL Select 4 as Urutan, iTahun, IDDInas,IDUrusan,JEnis as btjenis,IDPRogram, IDKEgiatan, IIDRekening,IIDRekening as IDRek,6 as btRoot,'' " +
                        " as sNamaRekening,  0 as JumlahOlah,0 as Jumlah, 0 as JumlahMurni,  sLabel as Label, sUraian,JumlahOlah as JumlahUraian , btUrut,Level  FROM tAnggaranUraian_A " +
                        " WHERE iTAhun = " + _p.Tahun.ToString() + " AND IDDInas=" + _p.IDDinas.ToString() + " AND Jenis =2  AND Jenis  in " + _p.Jenis;



                SSQL = SSQL + " Union all Select 6 as Urutan,B.iTahun ,B.IDDInas,A.ID as IDUrusan ,3 as btJenis, 0 as IDProgram , 0 as IDkegiatan,0 as Rek, 0 as IIDRekening,0 as Root,  " +
                            " A.sNamaUrusan  as sNamaRekening, SUM(b.cJumlahOlah) as JumlahOlah, 0 as Jumlah, 0 as JumlahMurni,'' as Label, '' as sUraian, 0 as JumlahUraian, 0 as btUrut,  " +
                            " 0 as Level    from mUrusan a Inner JOIN tAnggaranRekening_A B ON A.ID = B.IDUrusan  " +
                            " WHERE B.iTahun = " + _p.Tahun.ToString() + " AND B.IDDInas=" + _p.IDDinas.ToString() + " AND B.btJenis =3   AND B.btJenis  in " + _p.Jenis + " GROUP BY B.iTahun ,B.IDDInas,A.ID , A.sNamaUrusan ";


                SSQL = SSQL + " UNION ALL Select 7 as Urutan,A.iTahun ,A.IDDInas,A.IDurusan,3 as btJenis, A.IDProgram , 0 as IDkegiatan,0 as Rek, 0 as IIDRekening,0 as Root, " +
                                " A.sNamaProgram  as sNamaRekening, SUM(b.cJumlahOlah) as JumlahOlah, 0 as Jumlah, 0 as JumlahMurni,'' as Label, '' as sUraian, 0 as JumlahUraian, 0 as btUrut, 0 as Level   " +
                                " from tPrograms_A a Inner JOIN tAnggaranRekening_A B ON A.iTahun = B.iTahun and A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan   " +
                                " AND A.IDProgram= B.IDProgram WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND A.IDDInas=" + _p.IDDinas.ToString() + " AND A.btJenis =3  AND A.btJenis  in " + _p.Jenis +
                                " GROUP BY A.iTahun ,A.IDDInas, A.IDurusan,A.IDProgram , A.sNamaProgram ";

                SSQL = SSQL + " UNION ALL Select 8 as Urutan,A.iTahun ,A.IDDInas,A.IDurusan,3 as btJenis, A.IDProgram , A.IDKegiatan as IDkegiatan,0 as Rek, 0 as IIDRekening,0 as Root, " +
                                " A.sNama  as sNamaRekening, SUM(b.cJumlahOlah) as JumlahOlah, 0 as Jumlah, 0 as JumlahMurni,'' as Label, A.sLokasi as sUraian, 0 as JumlahUraian, 0 as btUrut, 0 as Level   " +
                                " from tKegiatan_A a Inner JOIN tAnggaranRekening_A B ON A.iTahun = B.iTahun and A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan   " +
                                " AND A.IDProgram= B.IDProgram AND A.IDKegiatan=B.IDKegiatan WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND A.IDDInas=" + _p.IDDinas.ToString() + " AND A.btJenis =3  AND A.btJenis  in " + _p.Jenis +
                                " GROUP BY A.iTahun ,A.IDDInas, A.IDurusan,A.IDProgram ,A.IDKegiatan, A.sNama,A.sLokasi ";



                SSQL = SSQL + " UNION ALL Select 9 as Urutan , A.iTahun, A.IDDInas,A.IDUrusan,A.btJenis ,A.IDPRogram, A.IDKEgiatan, A.IIDRekening,A.Rek as IDRek, A.Root as btRoot, " +
                        " sNamaRekening,  JumlahOlah, Jumlah, JumlahMurni , '' as Label, '' as sUraian, 0 as JumlahUraian, 0 as btUrut,0 as Level from [vwAnggaranAllLevel] A " +
                        " WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND A.IDDInas=" + _p.IDDinas.ToString() + " AND A.btJenis =3 and Root>2   AND A.btJenis  in " + _p.Jenis;

                SSQL = SSQL + " UNION ALL Select 10 as Urutan, iTahun, IDDInas,IDUrusan,JEnis as btjenis,IDPRogram, IDKEgiatan, IIDRekening,IIDRekening as IDRek,6 as btRoot,'' " +
                        " as sNamaRekening,  0 as JumlahOlah,0 as Jumlah, 0 as JumlahMurni,  sLabel as Label, sUraian,JumlahOlah as JumlahUraian , btUrut ,Level FROM tAnggaranUraian_A " +
                        " WHERE iTAhun = " + _p.Tahun.ToString() + " AND IDDInas=" + _p.IDDinas.ToString() + " AND Jenis =3  AND Jenis  in " + _p.Jenis;



                SSQL = SSQL + " Order by btJEnis, IDurusan, IDprogram,IDKegiatan , IIDRekening,Root, btUrut";



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (_p.Tahap == 0 || _p.Tahap == 2)
                        {
                            _lst = (from DataRow dr in dt.Rows
                                    select new Penjabaran()
                                    {

                                        Kode = DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                                DataFormat.GetInteger(dr["IDDInas"]),
                                                                DataFormat.GetInteger(dr["IDProgram"]),
                                                                DataFormat.GetInteger(dr["IDkegiatan"]),
                                                                DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening),
                                        Urutan = DataFormat.GetInteger(dr["Urutan"]),
                                        Level = DataFormat.GetInteger(dr["Level"]),
                                        IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                        IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
                                        IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                        IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                        JenisAnggaran = DataFormat.GetInteger(dr["btJenis"]),
                                        Root = DataFormat.GetInteger(dr["btRoot"]),
                                        Nama = DataFormat.GetString(dr["sNamaRekening"]),
                                        Jumlah = DataFormat.GetDecimal(dr["JumlahOlah"]).ToRupiahInReport(),
                                        JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport(),
                                        Keterangan1 = DataFormat.GetString(dr["sUraian"]).Trim(),
                                        //Keterangan2 = DataFormat.GetDecimal(dr["JumlahUraian"]) > 0 ? DataFormat.GetString(dr["JumlahUraian"]) + " " + DataFormat.GetString(dr["sSatuan"]) + " x " + DataFormat.GetDecimal(dr["JumlahUraian"]).ToRupiahInReport() : "",
                                        Keterangan2 = DataFormat.GetDecimal(dr["JumlahUraian"]).ToRupiahInReport(),
                                        Keterangan3 = DataFormat.GetString(dr["Label"]).Trim(),


                                    }).ToList();
                        }
                        else
                        {
                            _lst = (from DataRow dr in dt.Rows
                                    select new Penjabaran()
                                    {

                                        //Kode { set; get; }
                                        Urutan = DataFormat.GetInteger(dr["Urutan"]),
                                        Level = DataFormat.GetInteger(dr["Level"]) == null ? 0 : DataFormat.GetInteger(dr["Level"]),
                                        IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                        IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
                                        IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                        IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                        JenisAnggaran = DataFormat.GetInteger(dr["btJenis"]),
                                        Root = DataFormat.GetInteger(dr["btRoot"]),
                                        Nama = DataFormat.GetString(dr["Nama"]),
                                        Jumlah = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
                                        JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport(),
                                        Keterangan1 = DataFormat.GetString(dr["sUraian"]).Trim(),
                                        Keterangan2 = DataFormat.GetString(dr["Vol"]) + " " + DataFormat.GetString(dr["sSatuan"]) + " x " + DataFormat.GetDecimal(dr["cHarga"]).ToRupiahInReport(),

                                    }).ToList();

                        }
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



        public List<PerdaV> GetPerdaV(ParameterLaporan _p)
        {
            List<PerdaV> _lst = new List<PerdaV>();
            try
            {
                // Cek View 
                //CreateViewPerdaII();
                //if (_p.Tahap == 0 || _p.Tahap == 2)
                //{

                BersihkanNonKegiatan();
                //string sNamaKolom = "";
                GetKolom(_p.Tahap);




                SSQL = " Select  A.btKodeFungsi, 0 as IDURusan,A.sNamaFungsi as Nama, " +
                    " SUM(CASE WHEN C.btJenis= 2 and C.IIDRekening like '511%' THEN C." + _namaKolom2 + " ELSE 0 END) AS JumlahBTLPegawai," +
                    " SUM(CASE WHEN C.btJenis= 2 and C.IIDRekening like  '512%' THEN C." + _namaKolom2 + " ELSE 0 END) AS JumlahBTLNonPegawai," +
                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '521%' THEN C." + _namaKolom2 + " ELSE 0 END) AS JumlahBLPegawai," +
                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '522%' THEN C." + _namaKolom2 + " ELSE 0 END) AS JumlahBLBarangJasa," +
                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '523%' THEN C." + _namaKolom2 + " ELSE 0 END) AS JumlahBLModal, " +
                    " SUM(CASE WHEN C.btJenis= 2 and C.IIDRekening like '511%' THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahBTLPegawaiMurni," +
                    " SUM(CASE WHEN C.btJenis= 2 and C.IIDRekening like  '512%' THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahBTLNonPegawaiMurni," +
                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '521%' THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahBLPegawaiMurni," +
                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '522%' THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahBLBarangJasaMurni," +
                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '523%' THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahBLModalMurni, " +
                                        " SUM(CASE WHEN C.btJenis in (2,3) THEN C." + _namaKolom2 + " ELSE 0 END) AS Jumlah, " +
                    " SUM(CASE WHEN C.btJenis in (2,3) THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahMurni " +
                    " from mFUngsi A INNER JOIN mUrusan B ON A.btKodeFungsi = B.btKodeFungsi " +
                    " INNER JOIN tAnggaranRekening_A C ON B.ID= C.IDUrusan " +
                    " INNER JOIN tKegiatan_A D ON C.iTahun = D.iTahun AND C.IDDInas = D.IDDInas and C.IDUrusan = D.IDurusan AND C.IDProgram = D.IDProgram AND C.IDKegiatan = D.IDkegiatan And C.btJenis = D.btJenis WHERE C.iTAhun = " + _p.Tahun.ToString() +
                    " GROUP BY A.btKodeFungsi ,A.sNamaFungsi ";

                SSQL = SSQL + " UNION ALL ";
                SSQL = SSQL + " Select  A.btKodeFungsi, B.ID as IDURusan,B.sNamaUrusan as Nama, " +
                    " SUM(CASE WHEN C.btJenis= 2 and C.IIDRekening like '511%' THEN C." + _namaKolom2 + " ELSE 0 END) AS JumlahBTLPegawai," +
                    " SUM(CASE WHEN C.btJenis= 2 and C.IIDRekening like '512%'THEN C." + _namaKolom2 + " ELSE 0 END) AS JumlahBTLNonPegawai," +
                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '521%' THEN C." + _namaKolom2 + " ELSE 0 END) AS JumlahBLPegawai," +
                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '522%' THEN C." + _namaKolom2 + " ELSE 0 END) AS JumlahBLBarangJasa," +
                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '523%' THEN C." + _namaKolom2 + " ELSE 0 END) AS JumlahBLModal," +
                    " SUM(CASE WHEN C.btJenis= 2 and C.IIDRekening like '511%' THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahBTLPegawaiMurni," +
                    " SUM(CASE WHEN C.btJenis= 2 and C.IIDRekening like  '512%' THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahBTLNonPegawaiMurni," +
                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '521%' THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahBLPegawaiMurni," +
                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '522%' THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahBLBarangJasaMurni," +
                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '523%' THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahBLModalMurni, " +
                    " SUM(CASE WHEN C.btJenis in (2,3) THEN C." + _namaKolom2 + " ELSE 0 END) AS Jumlah, " +
                    " SUM(CASE WHEN C.btJenis in (2,3) THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahMurni " +
                    " from mFUngsi A INNER JOIN mUrusan B ON A.btKodeFungsi = B.btKodeFungsi " +
                    " INNER JOIN tAnggaranRekening_A C ON B.ID= C.IDUrusan " +
                    " INNER JOIN tKegiatan_A D ON C.iTahun = D.iTahun AND C.IDDInas = D.IDDInas and C.IDUrusan = D.IDurusan AND C.IDProgram = D.IDProgram AND C.IDKegiatan = D.IDkegiatan And C.btJenis = D.btJenis WHERE C.iTAhun = " + _p.Tahun.ToString() +
                     " GROUP BY A.btKodeFungsi,B.ID,B.sNamaUrusan";

                SSQL = SSQL + " UNION ALL " +
                  " Select  99 as btKodeFungsi, 999  as IDURusan,'JUMLAH' as Nama, " +
                  " SUM(CASE WHEN C.btJenis= 2 and C.IIDRekening like '511%' THEN C." + _namaKolom2 + " ELSE 0 END) AS JumlahBTLPegawai," +
                  " SUM(CASE WHEN C.btJenis= 2 and C.IIDRekening like '512%' THEN C." + _namaKolom2 + " ELSE 0 END) AS JumlahBTLNonPegawai," +
                  " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '521%' THEN C." + _namaKolom2 + " ELSE 0 END) AS JumlahBLPegawai," +
                  " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '522%' THEN C." + _namaKolom2 + " ELSE 0 END) AS JumlahBLBarangJasa," +
                  " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '523%' THEN C." + _namaKolom2 + " ELSE 0 END) AS JumlahBLModal," +
                  " SUM(CASE WHEN C.btJenis= 2 and C.IIDRekening like '511%' THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahBTLPegawaiMurni," +
                  " SUM(CASE WHEN C.btJenis= 2 and C.IIDRekening like  '512%' THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahBTLNonPegawaiMurni," +
                  " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '521%' THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahBLPegawaiMurni," +
                  " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '522%' THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahBLBarangJasaMurni," +
                  " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '523%' THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahBLModalMurni, " +
                  " SUM(CASE WHEN C.btJenis in (2,3) THEN C." + _namaKolom2 + " ELSE 0 END) AS Jumlah ," +
                  " SUM(CASE WHEN C.btJenis in (2,3) THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahMurni " +
                  " from tAnggaranRekening_A C " +
                  " INNER JOIN tKegiatan_A D ON C.iTahun = D.iTahun AND C.IDDInas = D.IDDInas and C.IDUrusan = D.IDurusan AND C.IDProgram = D.IDProgram AND C.IDKegiatan = D.IDkegiatan And C.btJenis = D.btJenis " +
                  " WHERE C.iTAhun = " + _p.Tahun.ToString() +
                  " ORDER BY btKodeFungsi,IDUrusan,Nama ";

                //}

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        //  if (_p.Tahap == 0 || _p.Tahap == 2)
                        // {
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaV()
                                {
                                    KodeFungsi = DataFormat.GetInteger(dr["btKodeFungsi"]),//.IntToStringWithLeftPad(2),
                                    KodeUrusan = DataFormat.GetInteger(dr["IDUrusan"]),//.Substring(0, 1),
                                    Kode = DataFormat.GetString(dr["IDUrusan"]).Substring(1), //DataFormat.GetInteger(dr["btKodeFungsi"]).IntToStringWithLeftPad(1) + (DataFormat.GetInteger(dr["IDUrusan"])>0? "." + DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan():""),
                                    Nama = DataFormat.GetString(dr["Nama"]),


                                    BTLNonPegawai = DataFormat.GetDecimal(dr["JumlahBTLNonPegawai"]).ToRupiahInReport(),
                                    BTLPegawai = DataFormat.GetDecimal(dr["JumlahBTLPegawai"]).ToRupiahInReport(),
                                    BLPegawai = DataFormat.GetDecimal(dr["JumlahBLPegawai"]).ToRupiahInReport(),
                                    BLBarangJasa = DataFormat.GetDecimal(dr["JumlahBLBarangJasa"]).ToRupiahInReport(),
                                    BLModal = DataFormat.GetDecimal(dr["JumlahBLModal"]).ToRupiahInReport(),

                                    BTLNonPegawaiMurni = DataFormat.GetDecimal(dr["JumlahBTLNonPegawaiMurni"]).ToRupiahInReport(),
                                    BTLPegawaiMurni = DataFormat.GetDecimal(dr["JumlahBTLPegawaiMurni"]).ToRupiahInReport(),
                                    BLPegawaiMurni = DataFormat.GetDecimal(dr["JumlahBLPegawaiMurni"]).ToRupiahInReport(),
                                    BLBarangJasaMurni = DataFormat.GetDecimal(dr["JumlahBLBarangJasaMurni"]).ToRupiahInReport(),
                                    BLModalMurni = DataFormat.GetDecimal(dr["JumlahBLModalMurni"]).ToRupiahInReport(),


                                    A = DataFormat.GetDecimal(dr["JumlahBTLNonPegawai"]).ToRupiahInReport(),
                                    B = DataFormat.GetDecimal(dr["JumlahBLPegawai"]).ToRupiahInReport(),
                                    //DataFormat.GetDecimal(dr["JumlahBTLPegawai"]).ToRupiahInReport(),
                                    C = DataFormat.GetDecimal(dr["JumlahBLPegawai"]).ToRupiahInReport(),
                                    D = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),

                                    BLMurni = (DataFormat.GetDecimal(dr["JumlahBLPegawaiMurni"]) + DataFormat.GetDecimal(dr["JumlahBLModalMurni"]) + +DataFormat.GetDecimal(dr["JumlahBLBarangJasaMurni"])).ToRupiahInReport(),
                                    BTLMurni = (DataFormat.GetDecimal(dr["JumlahBTLNonPegawaiMurni"]) + DataFormat.GetDecimal(dr["JumlahBTLPegawaiMurni"])).ToRupiahInReport(),
                                    BL = (DataFormat.GetDecimal(dr["JumlahBLPegawai"]) + DataFormat.GetDecimal(dr["JumlahBLModal"]) + DataFormat.GetDecimal(dr["JumlahBLBarangJasa"])).ToRupiahInReport(),
                                    BTL = (DataFormat.GetDecimal(dr["JumlahBTLNonPegawai"]) + DataFormat.GetDecimal(dr["JumlahBTLPegawai"])).ToRupiahInReport(),

                                    JumlahMurni = (DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                    Jumlah = (DataFormat.GetDecimal(dr["Jumlah"])).ToRupiahInReport(),
                                    Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                    Persen = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]), DataFormat.GetDecimal(dr["JumlahMurni"]))


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
        public List<PerdaV> GetPerdaV90(ParameterLaporan _p)
        {
            List<PerdaV> _lst = new List<PerdaV>();
            try
            {

                //BersihkanNonKegiatan();
                GetKolom(_p.Tahap);



                /*
                                SSQL = " Select  A.btKodeFungsi, 0 as btKodesubfungsi,0 as IDURusan,A.SNAMAFUNGSI  as Nama, " +
                                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '51%' THEN C." + _namaKolom2 + " ELSE 0 END) AS BelanjaOperasi," +
                                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '52%'  THEN C." + _namaKolom2 + " ELSE 0 END) AS BelanjaModal," +
                                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '53%' THEN C." + _namaKolom2 + " ELSE 0 END) AS BTT," +
                                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '54%' THEN C." + _namaKolom2 + " ELSE 0 END) AS BT," +
                                    " SUM(CASE WHEN C.btJenis in (3) THEN C." + _namaKolom2 + " ELSE 0 END) AS Jumlah, " +
                                    " SUM(CASE WHEN C.btJenis in (3) THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahMurni " +
                                    " from mfungsi  A INNER JOIN mUrusan B ON A.btKodeFungsi = B.btKodeFungsi " +
                                    " INNER JOIN tAnggaranRekening_A C ON B.ID= C.IDUrusan " +
                                    " INNER JOIN TSUbKegiatan D ON C.iTahun = D.iTahun AND C.IDDInas = D.IDDInas and C.IDUrusan = D.IDurusan AND C.IDProgram = D.IDProgram AND C.IDKegiatan = D.IDkegiatan And C.btJenis = D.btJenis AND C.IDSubKegiatan = D.IDSubkegiatan WHERE C.iTAhun = " + _p.Tahun.ToString() +
                                    " GROUP BY A.btKodeFungsi ,A.SNAMAFUNGSI " +
                                    " UNION ALL " +
                                  " Select  A.btKodeFungsi,B.btKodesubfungsi , B.ID as IDURusan,B.sNamaUrusan as Nama, " +
                                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '51%' THEN C." + _namaKolom2 + " ELSE 0 END) AS BelanjaOperasi," +
                                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '52%'  THEN C." + _namaKolom2 + " ELSE 0 END) AS BelanjaModal," +
                                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '53%' THEN C." + _namaKolom2 + " ELSE 0 END) AS BTT," +
                                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '54%' THEN C." + _namaKolom2 + " ELSE 0 END) AS BT," +
                                    " SUM(CASE WHEN C.btJenis in (3) THEN C." + _namaKolom2 + " ELSE 0 END) AS Jumlah, " +
                                    " SUM(CASE WHEN C.btJenis in (3) THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahMurni " +
                                    " from mFUngsi A INNER JOIN mUrusan B ON A.btKodeFungsi = B.btKodeFungsi " +
                                    " INNER JOIN tAnggaranRekening_A C ON B.ID= C.IDUrusan " +
                                    " INNER JOIN TSUbKegiatan D ON C.iTahun = D.iTahun AND C.IDDInas = D.IDDInas and C.IDUrusan = D.IDurusan AND C.IDProgram = D.IDProgram AND C.IDKegiatan = D.IDkegiatan And C.btJenis = D.btJenis AND C.IDSubKegiatan = D.IDSubkegiatan WHERE C.iTAhun = " + _p.Tahun.ToString() +
                                        " GROUP BY A.btKodeFungsi,B.btKodesubfungsi ,B.ID,B.sNamaUrusan" +
                                    " UNION ALL " +
                                    " Select  99 as btKodeFungsi,99 as btkodesubfungsi, 999  as IDURusan,'JUMLAH' as Nama, " +
                                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '51%' THEN C." + _namaKolom2 + " ELSE 0 END) AS BelanjaOperasi," +
                                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '52%'  THEN C." + _namaKolom2 + " ELSE 0 END) AS BelanjaModal," +
                                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '53%' THEN C." + _namaKolom2 + " ELSE 0 END) AS BTT," +
                                    " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '54%' THEN C." + _namaKolom2 + " ELSE 0 END) AS BT," +
                                    " SUM(CASE WHEN C.btJenis in (3) THEN C." + _namaKolom2 + " ELSE 0 END) AS Jumlah ," +
                                    " SUM(CASE WHEN C.btJenis in (3) THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahMurni " +
                                    " from tAnggaranRekening_A C " +
                                    " INNER JOIN TSUbKegiatan D ON C.iTahun = D.iTahun AND C.IDDInas = D.IDDInas and C.IDUrusan = D.IDurusan AND C.IDProgram = D.IDProgram AND C.IDKegiatan = D.IDkegiatan " +
                                    " AND C.IDSubKegiatan = D.IDSubkegiatan And C.btJenis = D.btJenis " +
                                    " WHERE C.iTAhun = " + _p.Tahun.ToString() +
                                    " ORDER BY btKodeFungsi,IDUrusan,Nama ";
                                */

                //}
                SSQL = " Select  1 as Level,A.btKodeFungsi, 0 as btKodesubfungsi,0 as IDURusan,A.SNAMAFUNGSI  as Nama, " +
                   " SUM(CASE WHEN C.btJenis in (2,3) and C.IIDRekening like '51%' THEN C." + _namaKolom1 + " ELSE 0 END) AS BelanjaOperasiMurni," +
                   " SUM(CASE WHEN C.btJenis in (2,3) and C.IIDRekening like '52%'  THEN C." + _namaKolom1 + " ELSE 0 END) AS BelanjaModalMurni," +
                   " SUM(CASE WHEN C.btJenis in (2,3) and C.IIDRekening like '53%' THEN C." + _namaKolom1 + " ELSE 0 END) AS BTTMurni," +
                   " SUM(CASE WHEN C.btJenis in (2,3) and C.IIDRekening like '54%' THEN C." + _namaKolom1 + " ELSE 0 END) AS BTMurni," +
                    " SUM(CASE WHEN C.btJenis in (2,3) and C.IIDRekening like '51%' THEN C." + _namaKolom2 + " ELSE 0 END) AS BelanjaOperasi," +
                   " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '52%'  THEN C." + _namaKolom2 + " ELSE 0 END) AS BelanjaModal," +
                   " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '53%' THEN C." + _namaKolom2 + " ELSE 0 END) AS BTT," +
                   " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '54%' THEN C." + _namaKolom2 + " ELSE 0 END) AS BT," +
                   " SUM(CASE WHEN C.btJenis in (2,3) THEN C." + _namaKolom2 + " ELSE 0 END) AS Jumlah, " +
                   " SUM(CASE WHEN C.btJenis in (2,3) THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahMurni " +
                   " from tanggaranrekening_A C  inner join  FungsiUrusanProgram fpg on fpg.idurusan= C.idurusan and " +
                    "fpg.idprogram= C.idprogram  inner join mfungsi a on a.btkodefungsi = fpg.idfungsi  " +
                   " INNER JOIN TSUbKegiatan D ON C.iTahun = D.iTahun AND C.IDDInas = D.IDDInas and C.IDUrusan = D.IDurusan AND C.IDProgram = D.IDProgram AND C.IDKegiatan = D.IDkegiatan And C.btJenis = D.btJenis AND C.IDSubKegiatan = D.IDSubkegiatan WHERE C.iTAhun = " + _p.Tahun.ToString() +
                   " GROUP BY A.btKodeFungsi ,A.SNAMAFUNGSI ";
                SSQL = SSQL + "union Select  2 as Level, A.btkodefungsi,  btKodesubfungsi,0 as IDURusan,a.Nama  as Nama, " +
                                      " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '51%' THEN C." + _namaKolom1 + " ELSE 0 END) AS BelanjaOperasiMurni," +
                   " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '52%'  THEN C." + _namaKolom1 + " ELSE 0 END) AS BelanjaModalMurni," +
                   " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '53%' THEN C." + _namaKolom1 + " ELSE 0 END) AS BTTMurni," +
                   " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '54%' THEN C." + _namaKolom1 + " ELSE 0 END) AS BTMurni," +
                    " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '51%' THEN C." + _namaKolom2 + " ELSE 0 END) AS BelanjaOperasi," +
                   " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '52%'  THEN C." + _namaKolom2 + " ELSE 0 END) AS BelanjaModal," +
                   " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '53%' THEN C." + _namaKolom2 + " ELSE 0 END) AS BTT," +
                   " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '54%' THEN C." + _namaKolom2 + " ELSE 0 END) AS BT," +
                   " SUM(CASE WHEN C.btJenis in (2,3) THEN C." + _namaKolom2 + " ELSE 0 END) AS Jumlah, " +
                   " SUM(CASE WHEN C.btJenis in (2,3) THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahMurni " +
                   " from tanggaranrekening_A C  inner join  FungsiUrusanProgram fpg on fpg.idurusan= C.idurusan  and " +
                    "fpg.idprogram= C.idprogram  inner join subFungsi a on a.btkodefungsi = fpg.idfungsi  and a.btKodeSubFungsi = fpg.idsubfungsi " +
                   " INNER JOIN TSUbKegiatan D ON C.iTahun = D.iTahun AND C.IDDInas = D.IDDInas and C.IDUrusan = D.IDurusan AND C.IDProgram = D.IDProgram AND C.IDKegiatan = D.IDkegiatan And C.btJenis = D.btJenis AND C.IDSubKegiatan = D.IDSubkegiatan WHERE C.iTAhun = " + _p.Tahun.ToString() +
                   " GROUP BY  A.btkodefungsi,  btKodesubfungsi ,a.nama  ";

                SSQL = SSQL + " union Select  3 as Level, fpg.idfungsi as btkodefungsi,  fpg.idsubfungsi as btKodesubfungsi,fpg.idurusan  as IDURusan,a.snamaurusan as Nama, " +
                  " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '51%' THEN C." + _namaKolom1 + " ELSE 0 END) AS BelanjaOperasiMurni," +
                  " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '52%'  THEN C." + _namaKolom1 + " ELSE 0 END) AS BelanjaModalMurni," +
                  " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '53%' THEN C." + _namaKolom1 + " ELSE 0 END) AS BTTMurni," +
                  " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '54%' THEN C." + _namaKolom1 + " ELSE 0 END) AS BTMurni," +
                    " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '51%' THEN C." + _namaKolom2 + " ELSE 0 END) AS BelanjaOperasi," +
                  " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '52%'  THEN C." + _namaKolom2 + " ELSE 0 END) AS BelanjaModal," +
                  " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '53%' THEN C." + _namaKolom2 + " ELSE 0 END) AS BTT," +
                  " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '54%' THEN C." + _namaKolom2 + " ELSE 0 END) AS BT," +
                  " SUM(CASE WHEN C.btJenis in (2,3) THEN C." + _namaKolom2 + " ELSE 0 END) AS Jumlah, " +
                  " SUM(CASE WHEN C.btJenis in (2,3) THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahMurni " +
                  " from tanggaranrekening_A C  inner join  FungsiUrusanProgram fpg on fpg.idurusan= C.idurusan and " +
                   "fpg.idprogram= C.idprogram  inner join mUrusan a on a.id = fpg.idurusan " +
                  " INNER JOIN TSUbKegiatan D ON C.iTahun = D.iTahun AND C.IDDInas = D.IDDInas and C.IDUrusan = D.IDurusan AND C.IDProgram = D.IDProgram AND C.IDKegiatan = D.IDkegiatan And C.btJenis = D.btJenis AND C.IDSubKegiatan = D.IDSubkegiatan WHERE C.iTAhun = " + _p.Tahun.ToString() +
                  " GROUP BY  fpg.idfungsi ,  fpg.idsubfungsi ,fpg.idurusan, a.snamaurusan   ";//ORDER BY btKodeFungsi,btKodesubfungsi,IDUrusan,Nama ";

                SSQL = SSQL + " UNION ALL " +
                 " Select  4 as Level, 99 as btKodeFungsi,99 as btkodesubfungsi, 999  as IDURusan,'JUMLAH' as Nama, " +
                                 " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '51%' THEN C." + _namaKolom1 + " ELSE 0 END) AS BelanjaOperasiMurni," +
                 " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '52%'  THEN C." + _namaKolom1 + " ELSE 0 END) AS BelanjaModalMurni," +
                 " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '53%' THEN C." + _namaKolom1 + " ELSE 0 END) AS BTTMurni," +
                 " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '54%' THEN C." + _namaKolom1 + " ELSE 0 END) AS BTMurni," +
                 " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '51%' THEN C." + _namaKolom2 + " ELSE 0 END) AS BelanjaOperasi," +
                 " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '52%'  THEN C." + _namaKolom2 + " ELSE 0 END) AS BelanjaModal," +
                 " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '53%' THEN C." + _namaKolom2 + " ELSE 0 END) AS BTT," +
                 " SUM(CASE WHEN C.btJenis in (2,3)  and C.IIDRekening like '54%' THEN C." + _namaKolom2 + " ELSE 0 END) AS BT," +
                 " SUM(CASE WHEN C.btJenis in (2,3) THEN C." + _namaKolom2 + " ELSE 0 END) AS Jumlah ," +
                 " SUM(CASE WHEN C.btJenis in (2,3) THEN C." + _namaKolom1 + " ELSE 0 END) AS JumlahMurni " +
                 " from tAnggaranRekening_A C " +
                 " INNER JOIN TSUbKegiatan D ON C.iTahun = D.iTahun AND C.IDDInas = D.IDDInas and C.IDUrusan = D.IDurusan AND C.IDProgram = D.IDProgram AND C.IDKegiatan = D.IDkegiatan " +
                 " AND C.IDSubKegiatan = D.IDSubkegiatan And C.btJenis = D.btJenis " +
                 " WHERE C.iTAhun = " + _p.Tahun.ToString() +
                 " ORDER BY btKodeFungsi,btkodesubfungsi , IDUrusan,Nama ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        //  if (_p.Tahap == 0 || _p.Tahap == 2)
                        // {
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaV()
                                {
                                    Level = DataFormat.GetInteger(dr["Level"]),
                                    KodeFungsi = DataFormat.GetInteger(dr["btKodeFungsi"]),
                                    KodeSubFungsi = DataFormat.GetInteger(dr["btKodesubfungsi"]),// == 0 ? "" : DataFormat.GetInteger(dr["btKodesubfungsi"]).IntToStringWithLeftPad(2),
                                    KodeUrusan = DataFormat.GetInteger(dr["IDUrusan"]),//.Substring(0, 1),
                                    Kode = DataFormat.GetString(dr["IDUrusan"]).Substring(1), //DataFormat.GetInteger(dr["btKodeFungsi"]).IntToStringWithLeftPad(1) + (DataFormat.GetInteger(dr["IDUrusan"])>0? "." + DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan():""),
                                    Nama = DataFormat.GetString(dr["Nama"]).Trim(),
                                    BOMurni = DataFormat.GetDecimal(dr["BelanjaOperasiMurni"]).ToRupiahInReport(),
                                    BMMurni = DataFormat.GetDecimal(dr["BelanjaModalMurni"]).ToRupiahInReport(),
                                    BTTMurni = DataFormat.GetDecimal(dr["BTTMurni"]).ToRupiahInReport(),
                                    BTMurni = DataFormat.GetDecimal(dr["BTMurni"]).ToRupiahInReport(),
                                    BO = DataFormat.GetDecimal(dr["BelanjaOperasi"]).ToRupiahInReport(),
                                    BM = DataFormat.GetDecimal(dr["BelanjaModal"]).ToRupiahInReport(),
                                    BTT = DataFormat.GetDecimal(dr["BTT"]).ToRupiahInReport(),
                                    BT = DataFormat.GetDecimal(dr["BT"]).ToRupiahInReport(),
                                    JumlahMurni = (Math.Round(DataFormat.GetDecimal(dr["JumlahMurni"]))).ToRupiahInReport(),
                                    Jumlah = (Math.Round(DataFormat.GetDecimal(dr["Jumlah"]))).ToRupiahInReport(),
                                    Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                    Persen = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]), DataFormat.GetDecimal(dr["JumlahMurni"]))


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
        public List<PerdaV> GetPerdaVRealisasi(ParameterLaporan _p)
        {
            List<PerdaV> _lst = new List<PerdaV>();
            try
            {
                // Cek View 
                //CreateViewPerdaII();
                //if (_p.Tahap == 0 || _p.Tahap == 2)
                //{

                SSQL = " Select  A.btKodeFungsi, 0 as IDURusan,A.sNamaFungsi as Nama, " +
                    " SUM(C.cJumlahRKAP ) AS Anggaran," +
                    " SUM(C.cRealisasi ) AS Realisasi" +
                    " from mFUngsi A INNER JOIN mUrusan B ON A.btKodeFungsi = B.btKodeFungsi " +
                    " INNER JOIN tAnggaranRekening_A C ON B.ID= C.IDUrusan " +
                    " INNER JOIN tKegiatan_A D ON C.iTahun = D.iTahun AND C.IDDInas = D.IDDInas and C.IDUrusan = D.IDurusan AND C.IDProgram = D.IDProgram AND C.IDKegiatan = D.IDkegiatan And C.btJenis = D.btJenis WHERE C.iTAhun = " + _p.Tahun.ToString() +
                    " AND C.btJenis in (2,3) GROUP BY A.btKodeFungsi ,A.sNamaFungsi " +
                    " UNION ALL " +
                    " Select  A.btKodeFungsi, B.ID as IDURusan,B.sNamaUrusan as Nama, " +
                    " SUM(C.cJumlahRKAP ) AS Anggaran," +
                    " SUM(C.cRealisasi ) AS Realisasi" +
                    " from mFUngsi A INNER JOIN mUrusan B ON A.btKodeFungsi = B.btKodeFungsi " +
                    " INNER JOIN tAnggaranRekening_A C ON B.ID= C.IDUrusan " +
                    " INNER JOIN tKegiatan_A D ON C.iTahun = D.iTahun AND C.IDDInas = D.IDDInas and C.IDUrusan = D.IDurusan AND C.IDProgram = D.IDProgram AND C.IDKegiatan = D.IDkegiatan And C.btJenis = D.btJenis WHERE C.iTAhun = " + _p.Tahun.ToString() +
                    " AND C.btJenis in (2,3)  GROUP BY A.btKodeFungsi,B.ID,B.sNamaUrusan" +
                    " UNION ALL " +
                    " Select  99 as btKodeFungsi, 999  as IDURusan,'JUMLAH' as Nama, " +
                    " SUM(C.cJumlahRKAP ) AS Anggaran," +
                    " SUM(C.cRealisasi ) AS Realisasi" +
                    " from tAnggaranRekening_A C " +
                    " INNER JOIN tKegiatan_A D ON C.iTahun = D.iTahun AND C.IDDInas = D.IDDInas and C.IDUrusan = D.IDurusan AND C.IDProgram = D.IDProgram AND C.IDKegiatan = D.IDkegiatan And C.btJenis = D.btJenis " +
                    " WHERE C.iTAhun = " + _p.Tahun.ToString() +
                    " AND C.btJenis in (2,3)  order by btKodeFungsi, IDUrusan ";

                //}

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        //  if (_p.Tahap == 0 || _p.Tahap == 2)
                        // {
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaV()
                                {
                                    KodeFungsi = DataFormat.GetInteger(dr["btKodeFungsi"]),//.IntToStringWithLeftPad(2),
                                    KodeUrusan = DataFormat.GetInteger(dr["IDUrusan"]),//.Substring(0, 1),
                                    Kode = DataFormat.GetString(dr["IDUrusan"]).Substring(1), //DataFormat.GetInteger(dr["btKodeFungsi"]).IntToStringWithLeftPad(1) + (DataFormat.GetInteger(dr["IDUrusan"])>0? "." + DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan():""),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Anggaran = DataFormat.GetDecimal(dr["Anggaran"]).ToRupiahInReport(),
                                    Realisasi = DataFormat.GetDecimal(dr["Realisasi"]).ToRupiahInReport(),
                                    Selisih = (DataFormat.GetDecimal(dr["Anggaran"]) - DataFormat.GetDecimal(dr["Realisasi"])).ToRupiahInReport(),
                                    Persen = DataFormat.GetProsentaseRealisasi(DataFormat.GetDecimal(dr["Realisasi"]), DataFormat.GetDecimal(dr["Anggaran"]))
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

        public List<PerdaIV> GetPerdaIVRealisasi(ParameterLaporan _p)
        {
            List<PerdaIV> _lst = new List<PerdaIV>();
            try
            {
                string tanggal = "'" + _p.dTanggal.Month.ToString() + "/" + _p.dTanggal.Day.ToString() + "/" + _p.dTanggal.Year.ToString() + "'";


                GetKolom(_p.Tahap);
                _namaKolom1 = "cJumlahRKAP";
                _namaKolom2 = "cRealisasi";
                SSQL = "";
                SSQL = " Select 1 as Level, 0 as IDDInas,B.btKodekategori * 100 as IDUrusan,0 as IDProgram, 0 as IDKegiatan, B.sNamaKategori as Nama, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom1 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom1 + " ELSE 0 END) as BBJ," +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom1 + " ELSE 0 END) as BM, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom2 + " ELSE 0 END) as BPM, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom2 + " ELSE 0 END) as BBJM," +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom2 + " ELSE 0 END) as BMM " +
                    " FROM tAnggaranRekening_A A INNER JOIN mKategori B ON A.IDUrusan/100 = B.btKodeKategori " +
                    "  INNER JOIN mSKPD C ON A.IDDInas= C.ID " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND c.root=1 " +
                    " group BY B.btKodekategori,B.sNamaKategori ";

                SSQL = SSQL + " UNION ALL Select 2 as Level, 0 as IDDInas,A.IDUrusan,0 as IDProgram, 0 as IDKegiatan, B.sNamaUrusan as Nama, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom1 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom1 + " ELSE 0 END) as BBJ," +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom1 + " ELSE 0 END) as BM, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom2 + " ELSE 0 END) as BPM, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom2 + " ELSE 0 END) as BBJM," +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom2 + " ELSE 0 END) as BMM " +
                    " FROM tAnggaranRekening_A A INNER JOIN mUrusan B ON A.IDUrusan = B.ID " +
                    "  INNER JOIN mSKPD C ON A.IDDInas= C.ID  " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND c.root=1 " +
                    " group BY A.IDUrusan,B.sNamaUrusan ";
                SSQL = SSQL + " UNION ALL ";
                SSQL = SSQL + "  Select 3 as Level,  A.IDDInas/100 as IDDinas,A.IDUrusan as IDUrusan,0 as IDProgram, 0 as IDKegiatan, B.sNamaSKPD  as Nama, " +
                    "SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom1 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom1 + " ELSE 0 END) as BBJ, " +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom1 + " ELSE 0 END) as BM, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom2 + " ELSE 0 END) as BPM, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom2 + " ELSE 0 END) as BBJM," +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom2 + " ELSE 0 END) as BMM " +
                    " FROM tAnggaranRekening_A A INNER JOIN mSKPD  B ON A.IDDInas = B.ID  " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND B.Root= 1 " +
                    "  group BY A.IDDInas/100,A.IDUrusan , B.sNamaSKPD " +
                    " UNION ALL ";
                SSQL = SSQL + " Select 4 as Level,  A.IDDInas/100 as IDDInas,A.IDUrusan,A.IDProgram, 0 as IDKegiatan, B.sNamaProgram as Nama, " +
                     "   SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom1 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom1 + " ELSE 0 END) as BBJ," +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom1 + " ELSE 0 END) as BM ," +
                    " SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom2 + " ELSE 0 END) as BPM, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom2 + " ELSE 0 END) as BBJM," +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom2 + " ELSE 0 END) as BMM " +
                    " FROM tAnggaranRekening_A A INNER JOIN tPrograms_A B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan AND A.btJenis = B.btJenis " +
                    " AND A.IDDInas = B.IDDInas AND A.IDProgram = B.IDProgram " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND A.btJenis> 2 " +
                    " group BY A.IDDInas/100,A.IDUrusan,A.IDProgram,  B.sNamaProgram " +
                    " UNION ALL ";

                SSQL = SSQL + " Select 5 as Level,  A.IDDInas/100 as IDDInas,A.IDUrusan,A.IDProgram, A.IDKegiatan  as IDKegiatan, B.sNama as Nama, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom1 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom1 + " ELSE 0 END) as BBJ, " +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom1 + " ELSE 0 END) as BM ," +
                    " SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom2 + " ELSE 0 END) as BPM, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom2 + " ELSE 0 END) as BBJM," +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom2 + " ELSE 0 END) as BMM " +
                    " FROM tAnggaranRekening_A A INNER JOIN tKegiatan_A B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan " +
                    " AND A.IDDInas = B.IDDInas AND A.IDProgram = B.IDProgram AND A.IDKegiatan= B.IDkegiatan AND A.btJenis = B.btJenis  " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND A.btJenis> 2 " +
                    " group BY A.IDDInas/100,A.IDUrusan,A.IDProgram,A.IDKegiatan ,  B.sNama " +
                    " UNION ALL ";

                SSQL = SSQL + " Select 6 as Level,  0 as IDDInas,999 as IDUrusan,0 as IDProgram, 0 as IDKegiatan, 'JUMLAH' as Nama,  " +
                    " SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom1 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom1 + " ELSE 0 END) as BBJ, " +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom1 + " ELSE 0 END) as BM ," +
                    " SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom2 + " ELSE 0 END) as BPM, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom2 + " ELSE 0 END) as BBJM," +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom2 + " ELSE 0 END) as BMM " +
                    " FROM tAnggaranRekening_A A INNER JOIN tKegiatan_A B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan AND A.btJenis = B.btJenis " +
                    " AND A.IDProgram = B.IDProgram AND A.IDKegiatan= B.IDkegiatan AND A.IDDInas = B.IDDInas " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND A.btJenis> 2 " +
                    " Order BY IDUrusan ,IDDInas, IDProgram ,IDKegiatan ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIV()
                                {

                                    Level = DataFormat.GetInteger(dr["Level"]),
                                    Kode = GetKodeIV(DataFormat.GetInteger(dr["Level"]), DataFormat.GetInteger(dr["IDUrusan"]), DataFormat.GetInteger(dr["IDDInas"]), DataFormat.GetInteger(dr["IDProgram"]), DataFormat.GetInteger(dr["IDKegiatan"])),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDkegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    Nama = DataFormat.GetInteger(dr["Level"]) < 5 ? DataFormat.GetString(dr["Nama"]).ToUpper() : DataFormat.GetString(dr["Nama"]),
                                    BP = DataFormat.GetDecimal(dr["BP"]).ToRupiahInReport(),
                                    BBJ = DataFormat.GetDecimal(dr["BBJ"]).ToRupiahInReport(),
                                    BM = DataFormat.GetDecimal(dr["BM"]).ToRupiahInReport(),
                                    Jumlah = (DataFormat.GetDecimal(dr["BM"]) + DataFormat.GetDecimal(dr["BBJ"]) + DataFormat.GetDecimal(dr["BP"])).ToRupiahInReport(),
                                    BPMurni = DataFormat.GetDecimal(dr["BPM"]).ToRupiahInReport(),
                                    BBJMurni = DataFormat.GetDecimal(dr["BBJM"]).ToRupiahInReport(),
                                    BMMurni = DataFormat.GetDecimal(dr["BMM"]).ToRupiahInReport(),
                                    JunmlahMurni = (DataFormat.GetDecimal(dr["BMM"]) + DataFormat.GetDecimal(dr["BBJM"]) + DataFormat.GetDecimal(dr["BPM"])).ToRupiahInReport(),
                                    Selisih = ((DataFormat.GetDecimal(dr["BMM"]) + DataFormat.GetDecimal(dr["BBJM"]) + DataFormat.GetDecimal(dr["BPM"])) - (DataFormat.GetDecimal(dr["BM"]) + DataFormat.GetDecimal(dr["BBJ"]) + DataFormat.GetDecimal(dr["BP"]))).ToRupiahInReport(),
                                    persentase = DataFormat.GetProsentaseRealisasi((DataFormat.GetDecimal(dr["BMM"]) + DataFormat.GetDecimal(dr["BBJM"]) + DataFormat.GetDecimal(dr["BPM"])), (DataFormat.GetDecimal(dr["BM"]) + DataFormat.GetDecimal(dr["BBJ"]) + DataFormat.GetDecimal(dr["BP"])))
                                }).ToList();

                    }
                }
                PerdaIV blanjPerda = new PerdaIV();

                blanjPerda.Level = 4;
                blanjPerda.Kode = "";
                blanjPerda.IDDInas = 0;
                blanjPerda.IDUrusan = 0;
                blanjPerda.IDProgram = 0;
                blanjPerda.IDkegiatan = 0;
                blanjPerda.Nama = "";
                blanjPerda.BP = "";
                blanjPerda.BBJ = "";
                blanjPerda.BM = "";
                blanjPerda.Jumlah = "";
                blanjPerda.BPMurni = "0";
                blanjPerda.BBJMurni = "0";
                blanjPerda.BMMurni = "0";
                blanjPerda.JunmlahMurni = "0";
                blanjPerda.Selisih = "0";
                blanjPerda.persentase = "0";

                int idxprg = 0;
                int ihasrussisip = 0;
                foreach (PerdaIV p in _lst)
                {
                    if (p.IDUrusan == 406 && p.IDProgram == 40630 && p.IDkegiatan == 0)
                    {
                        ihasrussisip = idxprg;
                    }
                    idxprg++;

                }
                _lst.Insert(ihasrussisip, blanjPerda);
                _lst.Insert(ihasrussisip + 2, blanjPerda);
                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }

        }


        public List<PerdaIV> GetPerdaIVRealisasi77(ParameterLaporan _p)
        {
            List<PerdaIV> _lst = new List<PerdaIV>();
            try
            {
                string tanggal = "'" + _p.dTanggal.Month.ToString() + "/" + _p.dTanggal.Day.ToString() + "/" + _p.dTanggal.Year.ToString() + "'";


                GetKolom(_p.Tahap);
                _namaKolom1 = "cJumlahABT";
                _namaKolom2 = "Realisasi";
                SSQL = "";
                SSQL = " Select 1 as Level, 0 as IDDInas,B.btKodekategori * 100 as IDUrusan,0 as IDProgram, 0 as IDKegiatan,0 as idsubkegiatan, B.sNamaKategori as Nama, " +
                    " SUM (Case WHEN iidrekening like '51%' THEN A." + _namaKolom1 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN A." + _namaKolom1 + " ELSE 0 END) as BBJ," +
                    " SUM (Case WHEN iidrekening like '53%' THEN A." + _namaKolom1 + " ELSE 0 END) as BM, " +
                    " SUM (Case WHEN iidrekening like '54%' THEN A." + _namaKolom1 + " ELSE 0 END) as BT, " +

" SUM (Case WHEN iidrekening like '51%' THEN A." + _namaKolom2 + " ELSE 0 END) as BPM, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN A." + _namaKolom2 + " ELSE 0 END) as BBJM," +
                    " SUM (Case WHEN iidrekening like '53%' THEN A." + _namaKolom2 + " ELSE 0 END) as BMM, " +
                    " SUM (Case WHEN iidrekening like '54%' THEN A." + _namaKolom2 + " ELSE 0 END) as BTM " +
                    " FROM dbo.dboGetRealisasi (" + tanggal + ") A INNER JOIN mKategori B ON A.IDUrusan/100 = B.btKodeKategori " +
                    "  INNER JOIN mSKPD C ON A.IDDInas= C.ID " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND c.root=1 " +
                    " group BY B.btKodekategori,B.sNamaKategori ";

                SSQL = SSQL + " UNION ALL Select 2 as Level, 0 as IDDInas,A.IDUrusan,0 as IDProgram, 0 as IDKegiatan,0 as idsubkegiatan, B.sNamaUrusan as Nama, " +
                    " SUM (Case WHEN iidrekening like '51%' THEN A." + _namaKolom1 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN A." + _namaKolom1 + " ELSE 0 END) as BBJ," +
                    " SUM (Case WHEN iidrekening like '53%' THEN A." + _namaKolom1 + " ELSE 0 END) as BM, " +
                     " SUM (Case WHEN iidrekening like '54%' THEN A." + _namaKolom1 + " ELSE 0 END) as BT, " +
                    " SUM (Case WHEN iidrekening like '51%' THEN A." + _namaKolom2 + " ELSE 0 END) as BPM, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN A." + _namaKolom2 + " ELSE 0 END) as BBJM," +
                    " SUM (Case WHEN iidrekening like '53%' THEN A." + _namaKolom2 + " ELSE 0 END) as BMM, " +
                    " SUM (Case WHEN iidrekening like '54%' THEN A." + _namaKolom2 + " ELSE 0 END) as BTM " +
                    " FROM dbo.dboGetRealisasi (" + tanggal + ")  A INNER JOIN mUrusan B ON A.IDUrusan = B.ID " +
                    "  INNER JOIN mSKPD C ON A.IDDInas= C.ID  " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND c.root=1 " +
                    " group BY A.IDUrusan,B.sNamaUrusan ";
                SSQL = SSQL + " UNION ALL ";
                SSQL = SSQL + "  Select 3 as Level,  A.IDDInas/100 as IDDinas,A.IDUrusan as IDUrusan,0 as IDProgram, 0 as IDKegiatan,0 as idsubkegiatan, B.sNamaSKPD  as Nama, " +
                    "SUM (Case WHEN iidrekening like '51%' THEN A." + _namaKolom1 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN A." + _namaKolom1 + " ELSE 0 END) as BBJ, " +
                    " SUM (Case WHEN iidrekening like '53%' THEN A." + _namaKolom1 + " ELSE 0 END) as BM, " +
                     " SUM (Case WHEN iidrekening like '54%' THEN A." + _namaKolom1 + " ELSE 0 END) as BT, " +
                    " SUM (Case WHEN iidrekening like '51%' THEN A." + _namaKolom2 + " ELSE 0 END) as BPM, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN A." + _namaKolom2 + " ELSE 0 END) as BBJM," +
                    " SUM (Case WHEN iidrekening like '53%' THEN A." + _namaKolom2 + " ELSE 0 END) as BMM , " +
                    " SUM (Case WHEN iidrekening like '54%' THEN A." + _namaKolom2 + " ELSE 0 END) as BTM " +
                    " FROM dbo.dboGetRealisasi (" + tanggal + ")  A INNER JOIN mSKPD  B ON A.IDDInas = B.ID  " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND B.Root= 1 " +
                    "  group BY A.IDDInas/100,A.IDUrusan , B.sNamaSKPD " +
                    " UNION ALL ";
                SSQL = SSQL + " Select 4 as Level,  A.IDDInas/100 as IDDInas,A.IDUrusan,A.IDProgram, 0 as IDKegiatan,0 as idsubkegiatan, B.sNamaProgram as Nama, " +
                     "   SUM (Case WHEN iidrekening like '51%' THEN A." + _namaKolom1 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN A." + _namaKolom1 + " ELSE 0 END) as BBJ," +
                    " SUM (Case WHEN iidrekening like '53%' THEN A." + _namaKolom1 + " ELSE 0 END) as BM ," +
                     " SUM (Case WHEN iidrekening like '54%' THEN A." + _namaKolom1 + " ELSE 0 END) as BT, " +
                    " SUM (Case WHEN iidrekening like '51%' THEN A." + _namaKolom2 + " ELSE 0 END) as BPM, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN A." + _namaKolom2 + " ELSE 0 END) as BBJM," +
                    " SUM (Case WHEN iidrekening like '53%' THEN A." + _namaKolom2 + " ELSE 0 END) as BMM , " +
                    " SUM (Case WHEN iidrekening like '54%' THEN A." + _namaKolom2 + " ELSE 0 END) as BTM " +
                    " FROM dbo.dboGetRealisasi (" + tanggal + ")  A INNER JOIN tPrograms_A B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan AND A.btJenis = B.btJenis " +
                    " AND A.IDDInas = B.IDDInas AND A.IDProgram = B.IDProgram " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND A.btJenis> 2 " +
                    " group BY A.IDDInas/100,A.IDUrusan,A.IDProgram,  B.sNamaProgram " +
                    " UNION ALL ";

                SSQL = SSQL + " Select 5 as Level,  A.IDDInas/100 as IDDInas,A.IDUrusan,A.IDProgram, A.IDKegiatan  as IDKegiatan,0 as idsubkegiatan, B.sNama as Nama, " +
                    " SUM (Case WHEN iidrekening like '51%' THEN A." + _namaKolom1 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN A." + _namaKolom1 + " ELSE 0 END) as BBJ, " +
                    " SUM (Case WHEN iidrekening like '53%' THEN A." + _namaKolom1 + " ELSE 0 END) as BM ," +
                     " SUM (Case WHEN iidrekening like '54%' THEN A." + _namaKolom1 + " ELSE 0 END) as BT, " +
                    " SUM (Case WHEN iidrekening like '51%' THEN A." + _namaKolom2 + " ELSE 0 END) as BPM, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN A." + _namaKolom2 + " ELSE 0 END) as BBJM," +
                    " SUM (Case WHEN iidrekening like '53%' THEN A." + _namaKolom2 + " ELSE 0 END) as BMM , " +
                    " SUM (Case WHEN iidrekening like '54%' THEN A." + _namaKolom2 + " ELSE 0 END) as BTM " +
                    " FROM dbo.dboGetRealisasi (" + tanggal + ")  A INNER JOIN tKegiatan_A B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan " +
                    " AND A.IDDInas = B.IDDInas AND A.IDProgram = B.IDProgram AND A.IDKegiatan= B.IDkegiatan AND A.btJenis = B.btJenis  " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND A.btJenis> 2 " +
                    " group BY A.IDDInas/100,A.IDUrusan,A.IDProgram,A.IDKegiatan ,  B.sNama " +
                    " UNION ALL ";

                SSQL = SSQL + " Select 6 as Level,  A.IDDInas/100 as IDDInas,A.IDUrusan,A.IDProgram, A.IDKegiatan  as IDKegiatan,A.idsubkegiatan, B.Nama as Nama, " +
                    " SUM (Case WHEN iidrekening like '51%' THEN A." + _namaKolom1 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN A." + _namaKolom1 + " ELSE 0 END) as BBJ, " +
                    " SUM (Case WHEN iidrekening like '53%' THEN A." + _namaKolom1 + " ELSE 0 END) as BM ," +
                     " SUM (Case WHEN iidrekening like '54%' THEN A." + _namaKolom1 + " ELSE 0 END) as BT, " +
                    " SUM (Case WHEN iidrekening like '51%' THEN A." + _namaKolom2 + " ELSE 0 END) as BPM, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN A." + _namaKolom2 + " ELSE 0 END) as BBJM," +
                    " SUM (Case WHEN iidrekening like '53%' THEN A." + _namaKolom2 + " ELSE 0 END) as BMM , " +
                    " SUM (Case WHEN iidrekening like '54%' THEN A." + _namaKolom2 + " ELSE 0 END) as BTM " +
                    " FROM dbo.dboGetRealisasi (" + tanggal + ")  A INNER JOIN tSUBkegiatan B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan " +
                    " AND A.IDDInas = B.IDDInas AND A.IDProgram = B.IDProgram AND A.IDKegiatan= B.IDkegiatan AND A.IDSubkegiatan = B.IDSubkegiatan  AND A.btJenis = B.btJenis  " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND A.btJenis> 2 " +
                    " group BY A.IDDInas/100,A.IDUrusan,A.IDProgram,A.IDKegiatan ,A.IDSubKegiatan,  B.Nama " +
                    " UNION ALL ";


                SSQL = SSQL + " Select 7 as Level,  0 as IDDInas,999 as IDUrusan,0 as IDProgram, 0 as IDKegiatan, 0 as idsubkegiatan,'JUMLAH' as Nama,  " +
                    " SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom1 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom1 + " ELSE 0 END) as BBJ, " +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom1 + " ELSE 0 END) as BM ," +
                     " SUM (Case WHEN iidrekening like '54%' THEN A." + _namaKolom1 + " ELSE 0 END) as BT, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom2 + " ELSE 0 END) as BPM, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom2 + " ELSE 0 END) as BBJM," +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom2 + " ELSE 0 END) as BMM , " +
                    " SUM (Case WHEN iidrekening like '54%' THEN A." + _namaKolom2 + " ELSE 0 END) as BTM " +
                    " FROM dbo.dboGetRealisasi (" + tanggal + ")  A INNER JOIN tKegiatan_A B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan AND A.btJenis = B.btJenis " +
                    " AND A.IDProgram = B.IDProgram AND A.IDKegiatan= B.IDkegiatan AND A.IDDInas = B.IDDInas " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND A.btJenis> 2 " +
                    " Order BY IDUrusan ,IDDInas, IDProgram ,IDKegiatan,IDSubKegiatan ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIV()
                                {

                                    Level = DataFormat.GetInteger(dr["Level"]),

                                    Kode = GetKodeIV77(DataFormat.GetInteger(dr["Level"]), DataFormat.GetInteger(dr["IDUrusan"]), DataFormat.GetInteger(dr["IDDInas"]), DataFormat.GetInteger(dr["IDProgram"]), DataFormat.GetInteger(dr["IDKegiatan"]), DataFormat.GetLong(dr["IDSubKegiatan"])),

                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDkegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    Nama = DataFormat.GetInteger(dr["Level"]) < 5 ? DataFormat.GetString(dr["Nama"]).ToUpper() : DataFormat.GetString(dr["Nama"]),
                                    BP = DataFormat.GetDecimal(dr["BP"]).ToRupiahInReport(),
                                    BBJ = DataFormat.GetDecimal(dr["BBJ"]).ToRupiahInReport(),
                                    BM = DataFormat.GetDecimal(dr["BM"]).ToRupiahInReport(),
                                    BT = DataFormat.GetDecimal(dr["BT"]).ToRupiahInReport(),
                                    Jumlah = (DataFormat.GetDecimal(dr["BM"]) + DataFormat.GetDecimal(dr["BBJ"]) + DataFormat.GetDecimal(dr["BP"]) + DataFormat.GetDecimal(dr["BT"])).ToRupiahInReport(),
                                    BPMurni = DataFormat.GetDecimal(dr["BPM"]).ToRupiahInReport(),
                                    BBJMurni = DataFormat.GetDecimal(dr["BBJM"]).ToRupiahInReport(),
                                    BMMurni = DataFormat.GetDecimal(dr["BMM"]).ToRupiahInReport(),
                                    BTMurni = DataFormat.GetDecimal(dr["BTM"]).ToRupiahInReport(),
                                    JunmlahMurni = (DataFormat.GetDecimal(dr["BMM"]) + DataFormat.GetDecimal(dr["BBJM"]) + DataFormat.GetDecimal(dr["BPM"]) + DataFormat.GetDecimal(dr["BTM"])).ToRupiahInReport(),
                                    Selisih = ((DataFormat.GetDecimal(dr["BMM"]) + DataFormat.GetDecimal(dr["BBJM"]) + DataFormat.GetDecimal(dr["BPM"])) - (DataFormat.GetDecimal(dr["BM"]) + DataFormat.GetDecimal(dr["BBJ"]) + DataFormat.GetDecimal(dr["BP"]))).ToRupiahInReport(),
                                    persentase = DataFormat.GetProsentaseRealisasi((DataFormat.GetDecimal(dr["BMM"]) + DataFormat.GetDecimal(dr["BBJM"]) + DataFormat.GetDecimal(dr["BPM"])), (DataFormat.GetDecimal(dr["BM"]) + DataFormat.GetDecimal(dr["BBJ"]) + DataFormat.GetDecimal(dr["BP"])))
                                }).ToList();

                    }
                }
                PerdaIV blanjPerda = new PerdaIV();

                blanjPerda.Level = 4;
                blanjPerda.Kode = "";
                blanjPerda.IDDInas = 0;
                blanjPerda.IDUrusan = 0;
                blanjPerda.IDProgram = 0;
                blanjPerda.IDkegiatan = 0;
                blanjPerda.Nama = "";
                blanjPerda.BP = "";
                blanjPerda.BBJ = "";
                blanjPerda.BM = "";
                blanjPerda.Jumlah = "";
                blanjPerda.BPMurni = "0";
                blanjPerda.BBJMurni = "0";
                blanjPerda.BMMurni = "0";
                blanjPerda.JunmlahMurni = "0";
                blanjPerda.Selisih = "0";
                blanjPerda.persentase = "0";

                int idxprg = 0;
                int ihasrussisip = 0;
                foreach (PerdaIV p in _lst)
                {
                    if (p.IDUrusan == 406 && p.IDProgram == 40630 && p.IDkegiatan == 0)
                    {
                        ihasrussisip = idxprg;
                    }
                    idxprg++;

                }
                _lst.Insert(ihasrussisip, blanjPerda);
                _lst.Insert(ihasrussisip + 2, blanjPerda);
                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }

        }
        public List<PerdaIV> GetPerdaIVRealisasi050(ParameterLaporan _p)
        {
            List<PerdaIV> _lst = new List<PerdaIV>();
            try
            {
                string tanggal = "'" + _p.dTanggal.Month.ToString() + "/" + _p.dTanggal.Day.ToString() + "/" + _p.dTanggal.Year.ToString() + "'";


                GetKolom(_p.Tahap);
                _namaKolom1 = "cJumlahABT";
                _namaKolom2 = "Realisasi";
                SSQL = "";

                if (_p.Tahun < 2022)
                    SSQL = "select * from vwPerda1_4 order by Kelompok, Kodekategori, Kodeurusan, KodeSKPD, KodeProgram, " +
                           "KodeKegiatan, KodeSubkegiatan ";
                else

                    SSQL = "select * from fnPerdaPerUrusan(" + _p.Tahun.ToString() + " ," + _p.TanggalRealisasi.ToSQLFormat() + ")  order by Kelompok, Kodekategori, Kodeurusan, KodeSKPD, KodeProgram, " +
                         "KodeKegiatan, KodeSubkegiatan ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIV()
                                {

                                    Level = DataFormat.GetInteger(dr["Level"]),

                                    Kode = GetKodeIV050(DataFormat.GetInteger(dr["Level"]),
                                           DataFormat.GetInteger(dr["KodeKategori"]),
                                           DataFormat.GetInteger(dr["KodeUrusan"]),
                                           DataFormat.GetInteger(dr["KodeProgram"]),
                                           DataFormat.GetInteger(dr["Kodekegiatan"]),
                                           DataFormat.GetInteger(dr["KodeSubKegiatan"]),
                                           DataFormat.GetString(dr["Kode"])
                                           ),

                                    IDDInas = DataFormat.GetInteger(dr["KodeKategori"]),
                                    IDUrusan = DataFormat.GetInteger(dr["KodeUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["KodeProgram"]),
                                    IDkegiatan = DataFormat.GetInteger(dr["KodeKegiatan"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),  //DataFormat.GetInteger(dr["Level"]) < 5 ? DataFormat.GetString(dr["Nama"]).ToUpper() : DataFormat.GetString(dr["Nama"]),
                                    BP = DataFormat.GetDecimal(dr["AnggaranOperasi"]).ToRupiahInReport(),
                                    BBJ = DataFormat.GetDecimal(dr["AnggaranModal"]).ToRupiahInReport(),
                                    BM = DataFormat.GetDecimal(dr["AnggaranTakTerduga"]).ToRupiahInReport(),
                                    Keluaran = "(" + DataFormat.GetString(dr["Keluaran"]) + ")",
                                    //                " SUM (Case WHEN iidrekening like '51%' THEN A." + _namaKolom1 + " ELSE 0 END) as BP, " +
                                    //" SUM (Case WHEN iidrekening like '52%' THEN A." + _namaKolom1 + " ELSE 0 END) as BBJ, " +
                                    //" SUM (Case WHEN iidrekening like '53%' THEN A." + _namaKolom1 + " ELSE 0 END) as BM ," +
                                    // " SUM (Case WHEN iidrekening like '54%' THEN A." + _namaKolom1 + " ELSE 0 END) as BT, " +
                                    //" SUM (Case WHEN iidrekening like '51%' THEN A." + _namaKolom2 + " ELSE 0 END) as BPM, " +
                                    //" SUM (Case WHEN iidrekening like '52%' THEN A." + _namaKolom2 + " ELSE 0 END) as BBJM," +
                                    //" SUM (Case WHEN iidrekening like '53%' THEN A." + _namaKolom2 + " ELSE 0 END) as BMM , " +
                                    //" SUM (Case WHEN iidrekening like '54%' THEN A." + _namaKolom2 + " ELSE 0 END) as BTM " +

                                    BT = DataFormat.GetDecimal(dr["AnggaranTransfer"]).ToRupiahInReport(),
                                    Jumlah = DataFormat.GetDecimal(dr["RealisasiOperasi"]).ToRupiahInReport(),//+ DataFormat.GetDecimal(dr["BBJ"]) + DataFormat.GetDecimal(dr["BP"]) + DataFormat.GetDecimal(dr["BT"])).ToRupiahInReport(),
                                    BPMurni = DataFormat.GetDecimal(dr["RealisasiOperasi"]).ToRupiahInReport(),
                                    BBJMurni = DataFormat.GetDecimal(dr["RealisasiModal"]).ToRupiahInReport(),
                                    BMMurni = DataFormat.GetDecimal(dr["RealisasiTakTerduga"]).ToRupiahInReport(),
                                    BTMurni = DataFormat.GetDecimal(dr["RealisasiTransfer"]).ToRupiahInReport(),

                                }).ToList();

                    }
                }
                PerdaIV blanjPerda = new PerdaIV();

                blanjPerda.Level = 4;
                blanjPerda.Kode = "";
                blanjPerda.IDDInas = 0;
                blanjPerda.IDUrusan = 0;
                blanjPerda.IDProgram = 0;
                blanjPerda.IDkegiatan = 0;
                blanjPerda.Nama = "";
                blanjPerda.BP = "";
                blanjPerda.BBJ = "";
                blanjPerda.BM = "";
                blanjPerda.Jumlah = "";
                blanjPerda.BPMurni = "0";
                blanjPerda.BBJMurni = "0";
                blanjPerda.BMMurni = "0";
                blanjPerda.JunmlahMurni = "0";
                blanjPerda.Selisih = "0";
                blanjPerda.persentase = "0";

                int idxprg = 0;
                int ihasrussisip = 0;
                foreach (PerdaIV p in _lst)
                {
                    if (p.IDUrusan == 406 && p.IDProgram == 40630 && p.IDkegiatan == 0)
                    {
                        ihasrussisip = idxprg;
                    }
                    idxprg++;

                }
                //    _lst.Insert(ihasrussisip, blanjPerda);
                //   _lst.Insert(ihasrussisip + 2, blanjPerda);
                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }

        }

        public List<PerdaIV> GetPerdaVRealisasi050(ParameterLaporan _p)
        {
            List<PerdaIV> _lst = new List<PerdaIV>();
            try
            {
                string tanggal = "'" + _p.dTanggal.Month.ToString() + "/" + _p.dTanggal.Day.ToString() + "/" + _p.dTanggal.Year.ToString() + "'";


                GetKolom(_p.Tahap);
                _namaKolom1 = "cJumlahABT";
                _namaKolom2 = "Realisasi";

                SSQL = "";
                if (_p.Tahun < 2022)
                {
                    SSQL = "select * from vwPerda_1_V order by KOdeFungsi, KodeSUbFungsi , kodekategori ,KodeUrusan";
                }
                else
                {
                    SSQL = " select * from dbo.fnPerdaFungsi(" + _p.Tahun.ToString() + " ," + _p.TanggalRealisasi.ToSQLFormat() + ") order by KOdeFungsi, KodeSUbFungsi , kodekategori ,KodeUrusan";

                }
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIV()
                                {

                                    Level = DataFormat.GetInteger(dr["Level"]),



                                    IDDInas = DataFormat.GetInteger(dr["KodeFungsi"]),
                                    IDUrusan = DataFormat.GetInteger(dr["KodeSubFungsi"]),
                                    IDProgram = DataFormat.GetInteger(dr["Kodekategori"]),
                                    IDkegiatan = DataFormat.GetInteger(dr["KodeUrusan"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),  //DataFormat.GetInteger(dr["Level"]) < 5 ? DataFormat.GetString(dr["Nama"]).ToUpper() : DataFormat.GetString(dr["Nama"]),
                                    BP = DataFormat.GetDecimal(dr["AnggaranOperasi"]).ToRupiahInReport(),
                                    BBJ = DataFormat.GetDecimal(dr["AnggaranModal"]).ToRupiahInReport(),
                                    BM = DataFormat.GetDecimal(dr["AnggaranTakTerduga"]).ToRupiahInReport(),

                                    BT = DataFormat.GetDecimal(dr["AnggaranTransfer"]).ToRupiahInReport(),
                                    Jumlah = DataFormat.GetDecimal(dr["RealisasiOperasi"]).ToRupiahInReport(),//+ DataFormat.GetDecimal(dr["BBJ"]) + DataFormat.GetDecimal(dr["BP"]) + DataFormat.GetDecimal(dr["BT"])).ToRupiahInReport(),
                                    BPMurni = DataFormat.GetDecimal(dr["RealisasiOperasi"]).ToRupiahInReport(),
                                    BBJMurni = DataFormat.GetDecimal(dr["RealisasiModal"]).ToRupiahInReport(),
                                    BMMurni = DataFormat.GetDecimal(dr["RealisasiTakTerduga"]).ToRupiahInReport(),
                                    BTMurni = DataFormat.GetDecimal(dr["RealisasiTransfer"]).ToRupiahInReport(),

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
        public List<PerdaSPM> GetPerdaSPM050(ParameterLaporan _p)
        {
            List<PerdaSPM> _lst = new List<PerdaSPM>();
            try
            {
                //SSQL = "";
                //if (_p.Tahun < 2022)
                //    SSQL = "select * from vwPerdaSPM order by IDUrusan ,Level, IDkegiatan ";
                //else

                //    SSQL = "select * from dbo.fnPerdaSPM(" + _p.Tahun.ToString() + " ," + _p.TanggalRealisasi.ToSQLFormat() + ") order by IDUrusan ,Level, IDkegiatan ";



                //DataTable dt = new DataTable();
                //dt = _dbHelper.ExecuteDataTable(SSQL);
                //if (dt != null)
                //{
                //    if (dt.Rows.Count > 0)
                //    {
                //        _lst = (from DataRow dr in dt.Rows
                //                select new PerdaSPM()
                //                {

                //                    Level = DataFormat.GetInteger(dr["Level"]),



                //                    No = DataFormat.GetString(dr["No"]),  //DataFormat.GetInteger(dr["Level"]) < 5 ? DataFormat.GetString(dr["Nama"]).ToUpper() : DataFormat.GetString(dr["Nama"]),
                //                    IDUrusan = DataFormat.GetInteger(dr["IdUrusan"]).ToKodeUrusan(),
                //                    KodeKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]).ToKodeKegiatan(m_ProfileProgKegiatan),
                //                    Nama = DataFormat.GetString(dr["Nama"]),  //DataFormat.GetInteger(dr["Level"]) < 5 ? DataFormat.GetString(dr["Nama"]).ToUpper() : DataFormat.GetString(dr["Nama"]),
                //                    Anggaran = DataFormat.GetDecimal(dr["Anggaran"]).ToRupiahInReport(),
                //                    Realisasi = DataFormat.GetDecimal(dr["Realisasi"]).ToRupiahInReport(),
                //                    Jenis = DataFormat.GetString(dr["Jenis"]),
                //                    NoDetail = ""


                //                }).ToList();

                //    }
                //}
                //int No = 0;
                //int idx = 0;
                //string oldidurusan = _lst[0].IDUrusan.Trim();
                //foreach (PerdaSPM s in _lst)
                //{
                //    No = No + 1;
                //    if (s.Level == 1)
                //    {
                //        if (_lst[idx].NoDetail.Trim().Length == 0)
                //            _lst[idx].NoDetail = No.ToString();
                //    }
                //    else
                //    {
                //        No = 0;

                //    }
                //    idx = idx + 1;


                //}
                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }

        }
        public List<PerdaIV> GetPerdaIVRealisasiByOPD(ParameterLaporan _p)
        {
            List<PerdaIV> _lst = new List<PerdaIV>();
            try
            {
                GetKolom(_p.Tahap);
                _namaKolom1 = "cJumlahRKAP";
                _namaKolom2 = "cRealisasi";
                SSQL = "";

                SSQL = "  Select 1 as Level,  A.IDDInas as IDDinas,0 as IDUrusan,0 as IDProgram, 0 as IDKegiatan, B.sNamaSKPD  as Nama, " +
                    "SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom1 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom1 + " ELSE 0 END) as BBJ, " +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom1 + " ELSE 0 END) as BM, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom2 + " ELSE 0 END) as BPM, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom2 + " ELSE 0 END) as BBJM," +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom2 + " ELSE 0 END) as BMM " +
                    " FROM tAnggaranRekening_A A INNER JOIN mSKPD  B ON A.IDDInas = B.ID  " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND B.Root= 1 " +
                    "  group BY A.IDDInas, B.sNamaSKPD " +
                    " UNION ALL ";

                //SSQL = " Select 1 as Level, 0 as IDDInas,B.btKodekategori * 100 as IDUrusan,0 as IDProgram, 0 as IDKegiatan, B.sNamaKategori as Nama, " +
                //    " SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom1 + " ELSE 0 END) as BP, " +
                //    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom1 + " ELSE 0 END) as BBJ," +
                //    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom1 + " ELSE 0 END) as BM, " +
                //    " SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom2 + " ELSE 0 END) as BPM, " +
                //    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom2 + " ELSE 0 END) as BBJM," +
                //    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom2 + " ELSE 0 END) as BMM " +
                //    " FROM tAnggaranRekening_A A INNER JOIN mKategori B ON A.IDUrusan/100 = B.btKodeKategori " +
                //    "  INNER JOIN mSKPD C ON A.IDDInas= C.ID " +
                //    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND c.root=1 " +
                //    " group BY B.btKodekategori,B.sNamaKategori ";

                SSQL = SSQL + " Select 2 as Level, A.IDDInas,A.IDUrusan,0 as IDProgram, 0 as IDKegiatan, B.sNamaUrusan as Nama, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom1 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom1 + " ELSE 0 END) as BBJ," +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom1 + " ELSE 0 END) as BM, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom2 + " ELSE 0 END) as BPM, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom2 + " ELSE 0 END) as BBJM," +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom2 + " ELSE 0 END) as BMM " +
                    " FROM tAnggaranRekening_A A INNER JOIN mUrusan B ON A.IDUrusan = B.ID " +
                    "  INNER JOIN mSKPD C ON A.IDDInas= C.ID  " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND c.root=1 " +
                    " group BY A.IDDinas,A.IDUrusan,B.sNamaUrusan ";
                SSQL = SSQL + " UNION ALL ";


                SSQL = SSQL + " Select 4 as Level,  A.IDDInas as IDDInas,A.IDUrusan,A.IDProgram, 0 as IDKegiatan, B.sNamaProgram as Nama, " +
                     "   SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom1 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom1 + " ELSE 0 END) as BBJ," +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom1 + " ELSE 0 END) as BM ," +
                    " SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom2 + " ELSE 0 END) as BPM, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom2 + " ELSE 0 END) as BBJM," +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom2 + " ELSE 0 END) as BMM " +
                    " FROM tAnggaranRekening_A A INNER JOIN tPrograms_A B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan AND A.btJenis = B.btJenis " +
                    " AND A.IDDInas = B.IDDInas AND A.IDProgram = B.IDProgram " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND A.btJenis> 2 " +
                    " group BY A.IDDInas,A.IDUrusan,A.IDProgram,  B.sNamaProgram " +
                    " UNION ALL ";

                SSQL = SSQL + " Select 5 as Level,  A.IDDInas as IDDInas,A.IDUrusan,A.IDProgram, A.IDKegiatan  as IDKegiatan, B.sNama as Nama, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom1 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom1 + " ELSE 0 END) as BBJ, " +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom1 + " ELSE 0 END) as BM ," +
                    " SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom2 + " ELSE 0 END) as BPM, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom2 + " ELSE 0 END) as BBJM," +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom2 + " ELSE 0 END) as BMM " +
                    " FROM tAnggaranRekening_A A INNER JOIN tKegiatan_A B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan " +
                    " AND A.IDDInas = B.IDDInas AND A.IDProgram = B.IDProgram AND A.IDKegiatan= B.IDkegiatan AND A.btJenis = B.btJenis  " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND A.btJenis> 2 " +
                    " group BY A.IDDInas,A.IDUrusan,A.IDProgram,A.IDKegiatan ,  B.sNama " +
                    " UNION ALL ";

                SSQL = SSQL + " Select 6 as Level,  0 as IDDInas,999 as IDUrusan,0 as IDProgram, 0 as IDKegiatan, 'JUMLAH' as Nama,  " +
                    " SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom1 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom1 + " ELSE 0 END) as BBJ, " +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom1 + " ELSE 0 END) as BM ," +
                    " SUM (Case WHEN iidrekening like '521%' THEN A." + _namaKolom2 + " ELSE 0 END) as BPM, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN A." + _namaKolom2 + " ELSE 0 END) as BBJM," +
                    " SUM (Case WHEN iidrekening like '523%' THEN A." + _namaKolom2 + " ELSE 0 END) as BMM " +
                    " FROM tAnggaranRekening_A A INNER JOIN tKegiatan_A B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan AND A.btJenis = B.btJenis " +
                    " AND A.IDProgram = B.IDProgram AND A.IDKegiatan= B.IDkegiatan AND A.IDDInas = B.IDDInas " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND A.btJenis> 2 " +
                    " Order BY IDDInas, IDProgram ,IDKegiatan ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIV()
                                {

                                    Level = DataFormat.GetInteger(dr["Level"]),
                                    Kode = GetKodeIV(DataFormat.GetInteger(dr["Level"]), DataFormat.GetInteger(dr["IDUrusan"]), DataFormat.GetInteger(dr["IDDInas"]), DataFormat.GetInteger(dr["IDProgram"]), DataFormat.GetInteger(dr["IDKegiatan"])),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDkegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    Nama = DataFormat.GetInteger(dr["Level"]) < 5 ? DataFormat.GetString(dr["Nama"]).ToUpper() : DataFormat.GetString(dr["Nama"]),
                                    BP = DataFormat.GetDecimal(dr["BP"]).ToRupiahInReport(),
                                    BBJ = DataFormat.GetDecimal(dr["BBJ"]).ToRupiahInReport(),
                                    BM = DataFormat.GetDecimal(dr["BM"]).ToRupiahInReport(),
                                    Jumlah = (DataFormat.GetDecimal(dr["BM"]) + DataFormat.GetDecimal(dr["BBJ"]) + DataFormat.GetDecimal(dr["BP"])).ToRupiahInReport(),
                                    BPMurni = DataFormat.GetDecimal(dr["BPM"]).ToRupiahInReport(),
                                    BBJMurni = DataFormat.GetDecimal(dr["BBJM"]).ToRupiahInReport(),
                                    BMMurni = DataFormat.GetDecimal(dr["BMM"]).ToRupiahInReport(),
                                    JunmlahMurni = (DataFormat.GetDecimal(dr["BMM"]) + DataFormat.GetDecimal(dr["BBJM"]) + DataFormat.GetDecimal(dr["BPM"])).ToRupiahInReport(),
                                    Selisih = ((DataFormat.GetDecimal(dr["BMM"]) + DataFormat.GetDecimal(dr["BBJM"]) + DataFormat.GetDecimal(dr["BPM"])) - (DataFormat.GetDecimal(dr["BM"]) + DataFormat.GetDecimal(dr["BBJ"]) + DataFormat.GetDecimal(dr["BP"]))).ToRupiahInReport(),
                                    persentase = DataFormat.GetProsentaseRealisasi((DataFormat.GetDecimal(dr["BMM"]) + DataFormat.GetDecimal(dr["BBJM"]) + DataFormat.GetDecimal(dr["BPM"])), (DataFormat.GetDecimal(dr["BM"]) + DataFormat.GetDecimal(dr["BBJ"]) + DataFormat.GetDecimal(dr["BP"])))
                                }).ToList();

                    }
                }
                PerdaIV blanjPerda = new PerdaIV();

                blanjPerda.Level = 4;
                blanjPerda.Kode = "";
                blanjPerda.IDDInas = 0;
                blanjPerda.IDUrusan = 0;
                blanjPerda.IDProgram = 0;
                blanjPerda.IDkegiatan = 0;
                blanjPerda.Nama = "";
                blanjPerda.BP = "";
                blanjPerda.BBJ = "";
                blanjPerda.BM = "";
                blanjPerda.Jumlah = "";
                blanjPerda.BPMurni = "0";
                blanjPerda.BBJMurni = "0";
                blanjPerda.BMMurni = "0";
                blanjPerda.JunmlahMurni = "0";
                blanjPerda.Selisih = "0";
                blanjPerda.persentase = "0";

                int idxprg = 0;
                int ihasrussisip = 0;
                foreach (PerdaIV p in _lst)
                {
                    if (p.IDUrusan == 406 && p.IDProgram == 40630 && p.IDkegiatan == 0)
                    {
                        ihasrussisip = idxprg;
                    }
                    idxprg++;

                }
                _lst.Insert(ihasrussisip, blanjPerda);
                _lst.Insert(ihasrussisip + 2, blanjPerda);
                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }

        }
        public List<PerdaIV> GetPerdaIV(ParameterLaporan _p)
        {
            List<PerdaIV> _lst = new List<PerdaIV>();
            try
            {

                string namaView = CreateViewAnggaran(_p, true);
                string namaViewKegiatan = CreateViewKegiatan(_p, true);

                string namaViewProgram = CreateViewProgram(_p, true);



                GetKolom(_p.Tahap);
                /*
                SSQL = "";
                SSQL = " Select 1 as Level, 0 as IDDInas,B.btKodekategori * 100 as IDUrusan,0 as IDProgram, 0 as IDKegiatan, B.sNamaKategori as Nama, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom1 + " ELSE 0 END) as BPMurni, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom1 + " ELSE 0 END) as BBJMurni," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom1 + " ELSE 0 END) as BMMurni, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom2 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom2 + " ELSE 0 END) as BBJ," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom2 + " ELSE 0 END) as BM" +
                    " FROM tAnggaranRekening_A A INNER JOIN mKategori B ON A.IDUrusan/100 = B.btKodeKategori " +
                    "  INNER JOIN mSKPD C ON A.IDDInas= C.ID " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() +
                    " group BY B.btKodekategori,B.sNamaKategori ";

                SSQL = SSQL + " UNION ALL Select 2 as Level, 0 as IDDInas,A.IDUrusan,0 as IDProgram, 0 as IDKegiatan, B.sNamaUrusan as Nama, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom1 + " ELSE 0 END) as BPMurni, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom1 + " ELSE 0 END) as BBJMurni," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom1 + " ELSE 0 END) as BMMurni, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom2 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom2 + " ELSE 0 END) as BBJ," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom2 + " ELSE 0 END) as BM" +
                    " FROM tAnggaranRekening_A A INNER JOIN mUrusan B ON A.IDUrusan = B.ID " +
                    "  INNER JOIN mSKPD C ON A.IDDInas= C.ID  " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() +
                    " group BY A.IDUrusan,B.sNamaUrusan ";
                SSQL = SSQL + " UNION ALL ";
                SSQL = SSQL + "  Select 3 as Level,  A.IDDInas/100 as IDDinas,A.IDUrusan as IDUrusan,0 as IDProgram, 0 as IDKegiatan, B.sNamaSKPD  as Nama, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom1 + " ELSE 0 END) as BPMurni, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom1 + " ELSE 0 END) as BBJMurni," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom1 + " ELSE 0 END) as BMMurni, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom2 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom2 + " ELSE 0 END) as BBJ," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom2 + " ELSE 0 END) as BM" +
                    " FROM tAnggaranRekening_A A INNER JOIN mSKPD  B ON A.IDDInas/100 = B.ID/100  " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND B.Root= 0 " +
                    " group BY A.IDDInas/100,A.IDUrusan , B.sNamaSKPD " +
                    " UNION ALL ";
                SSQL = SSQL + " Select 4 as Level,  A.IDDInas/100 as IDDInas,A.IDUrusan,A.IDProgram, 0 as IDKegiatan, B.sNamaProgram as Nama, " +
                     " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom1 + " ELSE 0 END) as BPMurni, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom1 + " ELSE 0 END) as BBJMurni," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom1 + " ELSE 0 END) as BMMurni, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom2 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom2 + " ELSE 0 END) as BBJ," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom2 + " ELSE 0 END) as BM" +
                    " FROM tAnggaranRekening_A A INNER JOIN tPrograms_A B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan AND A.btJenis = B.btJenis " +
                    " AND A.IDDInas = B.IDDInas AND A.IDProgram = B.IDProgram " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND A.btJenis> 2 " +
                    " group BY A.IDDInas/100,A.IDUrusan,A.IDProgram,  B.sNamaProgram " +
                    " UNION ALL ";

                SSQL = SSQL + " Select 5 as Level,  A.IDDInas/100 as IDDInas,A.IDUrusan,A.IDProgram, A.IDKegiatan  as IDKegiatan, B.sNama as Nama, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom1 + " ELSE 0 END) as BPMurni, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom1 + " ELSE 0 END) as BBJMurni," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom1 + " ELSE 0 END) as BMMurni, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom2 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom2 + " ELSE 0 END) as BBJ," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom2 + " ELSE 0 END) as BM" +
                    " FROM tAnggaranRekening_A A INNER JOIN tKegiatan_A B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan " +
                    " AND A.IDDInas = B.IDDInas AND A.IDProgram = B.IDProgram AND A.IDKegiatan= B.IDkegiatan AND A.btJenis = B.btJenis  " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND A.btJenis> 2 " +
                    " group BY A.IDDInas/100,A.IDUrusan,A.IDProgram,A.IDKegiatan ,  B.sNama " +
                    " UNION ALL ";

                SSQL = SSQL + " Select 6 as Level,  0 as IDDInas,999 as IDUrusan,0 as IDProgram, 0 as IDKegiatan, 'JUMLAH' as Nama,  " +
                    " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom1 + " ELSE 0 END) as BPMurni, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom1 + " ELSE 0 END) as BBJMurni," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom1 + " ELSE 0 END) as BMMurni ," +
                    " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom2 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom2 + " ELSE 0 END) as BBJ," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom2 + " ELSE 0 END) as BM" +
                    " FROM tAnggaranRekening_A A INNER JOIN tKegiatan_A B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan AND A.btJenis = B.btJenis " +
                    " AND A.IDProgram = B.IDProgram AND A.IDKegiatan= B.IDkegiatan AND A.IDDInas = B.IDDInas " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND A.btJenis> 2 " +
                    " Order BY IDUrusan ,IDDInas, IDProgram ,IDKegiatan ";

                */
                SSQL = "";
                SSQL = " Select 1 as Level, 0 as IDDInas,B.btKodekategori * 100 as IDUrusan,0 as IDProgram, 0 as IDKegiatan, B.sNamaKategori as Nama, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom1 + " ELSE 0 END) as BPMurni, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom1 + " ELSE 0 END) as BBJMurni," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom1 + " ELSE 0 END) as BMMurni, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom2 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom2 + " ELSE 0 END) as BBJ," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom2 + " ELSE 0 END) as BM" +
                    " FROM " + namaView + " A INNER JOIN mKategori B ON A.IDUrusan/100 = B.btKodeKategori " +
                    "  INNER JOIN mSKPD C ON A.IDDInas= C.ID " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() +
                    " group BY B.btKodekategori,B.sNamaKategori ";

                SSQL = SSQL + " UNION ALL Select 2 as Level, 0 as IDDInas,A.IDUrusan,0 as IDProgram, 0 as IDKegiatan, B.sNamaUrusan as Nama, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom1 + " ELSE 0 END) as BPMurni, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom1 + " ELSE 0 END) as BBJMurni," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom1 + " ELSE 0 END) as BMMurni, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom2 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom2 + " ELSE 0 END) as BBJ," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom2 + " ELSE 0 END) as BM" +
                    " FROM " + namaView + "  A INNER JOIN mUrusan B ON A.IDUrusan = B.ID " +
                    "  INNER JOIN mSKPD C ON A.IDDInas= C.ID  " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() +
                    " group BY A.IDUrusan,B.sNamaUrusan ";
                SSQL = SSQL + " UNION ALL ";
                SSQL = SSQL + "  Select 3 as Level,  A.IDDInas/100 as IDDinas,A.IDUrusan as IDUrusan,0 as IDProgram, 0 as IDKegiatan, B.sNamaSKPD  as Nama, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom1 + " ELSE 0 END) as BPMurni, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom1 + " ELSE 0 END) as BBJMurni," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom1 + " ELSE 0 END) as BMMurni, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom2 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom2 + " ELSE 0 END) as BBJ," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom2 + " ELSE 0 END) as BM" +
                    " FROM " + namaView + "  A INNER JOIN mSKPD  B ON A.IDDInas/100 = B.ID/100  " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND B.ID= B.Parent " +
                    " group BY A.IDDInas/100,A.IDUrusan , B.sNamaSKPD " +
                    " UNION ALL ";
                SSQL = SSQL + " Select 4 as Level,  A.Parent/100 as IDDInas,A.IDUrusan,A.IDProgram, 0 as IDKegiatan, B.sNamaProgram as Nama, " +
                     " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom1 + " ELSE 0 END) as BPMurni, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom1 + " ELSE 0 END) as BBJMurni," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom1 + " ELSE 0 END) as BMMurni, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom2 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom2 + " ELSE 0 END) as BBJ," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom2 + " ELSE 0 END) as BM" +
                    " FROM " + namaView + "  A INNER JOIN " + namaViewProgram + " B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan AND A.btJenis = B.btJenis " +
                    " AND A.IDDInas = B.IDDInas AND A.IDProgram = B.IDProgram " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND A.btJenis> 2 " +
                    " group BY A.Parent/100,A.IDUrusan,A.IDProgram,  B.sNamaProgram " +
                    " UNION ALL ";

                SSQL = SSQL + " Select 5 as Level,  A.PARENT/100 as IDDInas,A.IDUrusan,A.IDProgram, A.IDKegiatan  as IDKegiatan, B.sNama as Nama, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom1 + " ELSE 0 END) as BPMurni, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom1 + " ELSE 0 END) as BBJMurni," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom1 + " ELSE 0 END) as BMMurni, " +
                    " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom2 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom2 + " ELSE 0 END) as BBJ," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom2 + " ELSE 0 END) as BM" +
                    " FROM " + namaView + "  A INNER JOIN  " + namaViewKegiatan + "  B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan " +
                    " AND A.IDDInas = B.IDDInas AND A.IDProgram = B.IDProgram AND A.IDKegiatan= B.IDkegiatan AND A.btJenis = B.btJenis  " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND A.btJenis> 2 " +
                    " group BY A.PARENT/100,A.IDUrusan,A.IDProgram,A.IDKegiatan ,  B.sNama " +
                    " UNION ALL ";

                SSQL = SSQL + " Select 6 as Level,  0 as IDDInas,999 as IDUrusan,0 as IDProgram, 0 as IDKegiatan, 'JUMLAH' as Nama,  " +
                    " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom1 + " ELSE 0 END) as BPMurni, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom1 + " ELSE 0 END) as BBJMurni," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom1 + " ELSE 0 END) as BMMurni ," +
                    " SUM (Case WHEN iidrekening like '521%' THEN " + _namaKolom2 + " ELSE 0 END) as BP, " +
                    " SUM (Case WHEN iidrekening like '522%' THEN " + _namaKolom2 + " ELSE 0 END) as BBJ," +
                    " SUM (Case WHEN iidrekening like '523%' THEN " + _namaKolom2 + " ELSE 0 END) as BM" +
                    " FROM " + namaView + "  A INNER JOIN " + namaViewKegiatan + " B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan AND A.btJenis = B.btJenis " +
                    " AND A.IDProgram = B.IDProgram AND A.IDKegiatan= B.IDkegiatan AND A.IDDInas = B.IDDInas " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND A.btJenis> 2 " +
                    " Order BY IDUrusan ,IDDInas, IDProgram ,IDKegiatan ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIV()
                                {

                                    Level = DataFormat.GetInteger(dr["Level"]),
                                    Kode = GetKodeIV(DataFormat.GetInteger(dr["Level"]), DataFormat.GetInteger(dr["IDUrusan"]), DataFormat.GetInteger(dr["IDDInas"]), DataFormat.GetInteger(dr["IDProgram"]), DataFormat.GetInteger(dr["IDKegiatan"])),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDkegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    Nama = DataFormat.GetInteger(dr["Level"]) < 5 ? DataFormat.GetString(dr["Nama"]).ToUpper() : DataFormat.GetString(dr["Nama"]),
                                    BP = DataFormat.GetDecimal(dr["BP"]).ToRupiahInReport(),
                                    BBJ = DataFormat.GetDecimal(dr["BBJ"]).ToRupiahInReport(),
                                    BM = DataFormat.GetDecimal(dr["BM"]).ToRupiahInReport(),
                                    Jumlah = (DataFormat.GetDecimal(dr["BM"]) + DataFormat.GetDecimal(dr["BBJ"]) + DataFormat.GetDecimal(dr["BP"])).ToRupiahInReport(),
                                    BPMurni = DataFormat.GetDecimal(dr["BPMurni"]).ToRupiahInReport(),
                                    BBJMurni = DataFormat.GetDecimal(dr["BBJMurni"]).ToRupiahInReport(),
                                    BMMurni = DataFormat.GetDecimal(dr["BMMurni"]).ToRupiahInReport(),
                                    JunmlahMurni = (DataFormat.GetDecimal(dr["BMMurni"]) + DataFormat.GetDecimal(dr["BBJMurni"]) + DataFormat.GetDecimal(dr["BPMurni"])).ToRupiahInReport(),
                                    Selisih = ((DataFormat.GetDecimal(dr["BM"]) + DataFormat.GetDecimal(dr["BBJ"]) + DataFormat.GetDecimal(dr["BP"])) - (DataFormat.GetDecimal(dr["BMMurni"]) + DataFormat.GetDecimal(dr["BBJMurni"]) + DataFormat.GetDecimal(dr["BPMurni"]))).ToRupiahInReport(),
                                    persentase = DataFormat.GetProsentase((DataFormat.GetDecimal(dr["BM"]) + DataFormat.GetDecimal(dr["BBJ"]) + DataFormat.GetDecimal(dr["BP"])), (DataFormat.GetDecimal(dr["BMMurni"]) + DataFormat.GetDecimal(dr["BBJMurni"]) + DataFormat.GetDecimal(dr["BPMurni"])))
                                }).ToList();

                    }
                }


                PerdaIV blanjPerda = new PerdaIV();

                blanjPerda.Level = 0;
                blanjPerda.Kode = "";
                blanjPerda.IDDInas = 0;
                blanjPerda.IDUrusan = 0;
                blanjPerda.IDProgram = 0;
                blanjPerda.IDkegiatan = 0;
                blanjPerda.Nama = "";
                blanjPerda.BP = "";
                blanjPerda.BBJ = "";
                blanjPerda.BM = "";
                blanjPerda.Jumlah = "";
                blanjPerda.BPMurni = "0";
                blanjPerda.BBJMurni = "0";
                blanjPerda.BMMurni = "0";
                blanjPerda.JunmlahMurni = "0";
                blanjPerda.Selisih = "0";
                blanjPerda.persentase = "0";

                int idxprg = 0;
                int ihasrussisip = 0;
                foreach (PerdaIV p in _lst)
                {
                    if (p.IDUrusan == 406 && p.IDProgram == 40630 && p.IDkegiatan == 0)
                    {
                        ihasrussisip = idxprg;
                    }
                    idxprg++;

                }
                _lst.Insert(ihasrussisip, blanjPerda);
                return _lst;
            }


            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }

        }
        public List<PerdaIV> GetPerdaIV90(ParameterLaporan _p)
        {
            List<PerdaIV> _lst = new List<PerdaIV>();
            try
            {

                string namaView = CreateViewAnggaran(_p, true);
                string namaViewKegiatan = CreateViewKegiatan(_p, true);

                string namaViewProgram = CreateViewProgram(_p, true);

                string namaViewSubKegiatan = CreateViewSubKegiatan(_p, true);


                GetKolom(_p.Tahap);

                SSQL = "";
                SSQL = " Select 1 as Level, 0 as IDDInas,B.btKodekategori * 100 as IDUrusan,0 as IDProgram, 0 as IDKegiatan, 0 as IDSUBKegiatan,B.sNamaKategori as Nama, " +
                    " SUM (Case WHEN iidrekening like '51%' THEN " + _namaKolom1 + " ELSE 0 END) as BOMurni, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN " + _namaKolom1 + " ELSE 0 END) as BMMurni," +
                    " SUM (Case WHEN iidrekening like '53%' THEN " + _namaKolom1 + " ELSE 0 END) as BTTMurni, " +
                    " SUM (Case WHEN iidrekening like '54%' THEN " + _namaKolom1 + " ELSE 0 END) as BTMurni, " +
                    " SUM (Case WHEN iidrekening like '51%' THEN " + _namaKolom2 + " ELSE 0 END) as BO, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN " + _namaKolom2 + " ELSE 0 END) as BM," +
                    " SUM (Case WHEN iidrekening like '53%' THEN " + _namaKolom2 + " ELSE 0 END) as BTT," +
                    " SUM (Case WHEN iidrekening like '54%' THEN " + _namaKolom2 + " ELSE 0 END) as BT " +
                    " FROM " + namaView + " A INNER JOIN mKategori B ON A.IDUrusan/100 = B.btKodeKategori " +
                    "  INNER JOIN mSKPD C ON A.IDDInas= C.ID " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() +
                    " group BY B.btKodekategori,B.sNamaKategori ";

                SSQL = SSQL + " UNION ALL Select 2 as Level, 0 as IDDInas,A.IDUrusan,0 as IDProgram, 0 as IDKegiatan,0 as IDSUBKegiatan, B.sNamaUrusan as Nama, " +
                    " SUM (Case WHEN iidrekening like '51%' THEN " + _namaKolom1 + " ELSE 0 END) as BOMurni, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN " + _namaKolom1 + " ELSE 0 END) as BMMurni," +
                    " SUM (Case WHEN iidrekening like '53%' THEN " + _namaKolom1 + " ELSE 0 END) as BTTMurni, " +
                    " SUM (Case WHEN iidrekening like '54%' THEN " + _namaKolom1 + " ELSE 0 END) as BTMurni, " +
                    " SUM (Case WHEN iidrekening like '51%' THEN " + _namaKolom2 + " ELSE 0 END) as BO, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN " + _namaKolom2 + " ELSE 0 END) as BM," +
                    " SUM (Case WHEN iidrekening like '53%' THEN " + _namaKolom2 + " ELSE 0 END) as BTT," +
                    " SUM (Case WHEN iidrekening like '54%' THEN " + _namaKolom2 + " ELSE 0 END) as BT " +
                    " FROM " + namaView + "  A INNER JOIN mUrusan B ON A.IDUrusan = B.ID " +
                    "  INNER JOIN mSKPD C ON A.IDDInas= C.ID  " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() +
                    " group BY A.IDUrusan,B.sNamaUrusan ";
                SSQL = SSQL + " UNION ALL ";
                SSQL = SSQL + "  Select 3 as Level,  A.IDDInas/100 as IDDinas,A.IDUrusan as IDUrusan,0 as IDProgram, 0 as IDKegiatan, 0 as IDSUBKegiatan,B.sNamaSKPD  as Nama, " +
                    " SUM (Case WHEN iidrekening like '51%' THEN " + _namaKolom1 + " ELSE 0 END) as BOMurni, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN " + _namaKolom1 + " ELSE 0 END) as BMMurni," +
                    " SUM (Case WHEN iidrekening like '53%' THEN " + _namaKolom1 + " ELSE 0 END) as BTTMurni, " +
                    " SUM (Case WHEN iidrekening like '54%' THEN " + _namaKolom1 + " ELSE 0 END) as BTMurni, " +
                    " SUM (Case WHEN iidrekening like '51%' THEN " + _namaKolom2 + " ELSE 0 END) as BO, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN " + _namaKolom2 + " ELSE 0 END) as BM," +
                    " SUM (Case WHEN iidrekening like '53%' THEN " + _namaKolom2 + " ELSE 0 END) as BTT," +
                    " SUM (Case WHEN iidrekening like '54%' THEN " + _namaKolom2 + " ELSE 0 END) as BT" +
                    " FROM " + namaView + "  A INNER JOIN mSKPD  B ON A.IDDInas/100 = B.ID/100  " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND B.ID= B.Parent " +
                    " group BY A.IDDInas/100,A.IDUrusan , B.sNamaSKPD " +
                    " UNION ALL ";
                SSQL = SSQL + " Select 4 as Level,  A.Parent/100 as IDDInas,A.IDUrusan,A.IDProgram, 0 as IDKegiatan, 0 as IDSUBKegiatan,B.sNamaProgram as Nama, " +
                     " SUM (Case WHEN iidrekening like '51%' THEN " + _namaKolom1 + " ELSE 0 END) as BOMurni, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN " + _namaKolom1 + " ELSE 0 END) as BMMurni," +
                    " SUM (Case WHEN iidrekening like '53%' THEN " + _namaKolom1 + " ELSE 0 END) as BTTMurni, " +
                    " SUM (Case WHEN iidrekening like '54%' THEN " + _namaKolom1 + " ELSE 0 END) as BTMurni, " +
                    " SUM (Case WHEN iidrekening like '51%' THEN " + _namaKolom2 + " ELSE 0 END) as BO, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN " + _namaKolom2 + " ELSE 0 END) as BM," +
                    " SUM (Case WHEN iidrekening like '53%' THEN " + _namaKolom2 + " ELSE 0 END) as BTT," +
                    " SUM (Case WHEN iidrekening like '54%' THEN " + _namaKolom2 + " ELSE 0 END) as BT" +
                    " FROM " + namaView + "  A INNER JOIN " + namaViewProgram + " B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan AND A.btJenis = B.btJenis " +
                    " AND A.IDDInas = B.IDDInas AND A.IDProgram = B.IDProgram " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND A.btJenis in (2,3) " +
                    " group BY A.Parent/100,A.IDUrusan,A.IDProgram,  B.sNamaProgram " +
                    " UNION ALL ";

                SSQL = SSQL + " Select 5 as Level,  A.PARENT/100 as IDDInas,A.IDUrusan,A.IDProgram, A.IDKegiatan  as IDKegiatan,0 as IDSUBKegiatan, B.sNama as Nama, " +
                    " SUM (Case WHEN iidrekening like '51%' THEN " + _namaKolom1 + " ELSE 0 END) as BOMurni, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN " + _namaKolom1 + " ELSE 0 END) as BMMurni," +
                    " SUM (Case WHEN iidrekening like '53%' THEN " + _namaKolom1 + " ELSE 0 END) as BTTMurni, " +
                    " SUM (Case WHEN iidrekening like '54%' THEN " + _namaKolom1 + " ELSE 0 END) as BTMurni, " +
                    " SUM (Case WHEN iidrekening like '51%' THEN " + _namaKolom2 + " ELSE 0 END) as BO, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN " + _namaKolom2 + " ELSE 0 END) as BM," +
                    " SUM (Case WHEN iidrekening like '53%' THEN " + _namaKolom2 + " ELSE 0 END) as BTT," +
                    " SUM (Case WHEN iidrekening like '54%' THEN " + _namaKolom2 + " ELSE 0 END) as BT" +
                    " FROM " + namaView + "  A INNER JOIN  " + namaViewKegiatan + "  B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan " +
                    " AND A.IDDInas = B.IDDInas AND A.IDProgram = B.IDProgram AND A.IDKegiatan= B.IDkegiatan AND A.btJenis = B.btJenis  " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND A.btJenis in (2,3 ) " +
                    " group BY A.PARENT/100,A.IDUrusan,A.IDProgram,A.IDKegiatan ,  B.sNama " +
                    " UNION ALL ";
                SSQL = SSQL + " Select 6 as Level,  A.PARENT/100 as IDDInas,A.IDUrusan,A.IDProgram, A.IDKegiatan  as IDKegiatan, A.IDSubKegiatan,B.Nama as Nama, " +
                    " SUM (Case WHEN iidrekening like '51%' THEN " + _namaKolom1 + " ELSE 0 END) as BOMurni, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN " + _namaKolom1 + " ELSE 0 END) as BMMurni," +
                    " SUM (Case WHEN iidrekening like '53%' THEN " + _namaKolom1 + " ELSE 0 END) as BTTMurni, " +
                    " SUM (Case WHEN iidrekening like '54%' THEN " + _namaKolom1 + " ELSE 0 END) as BTMurni, " +
                    " SUM (Case WHEN iidrekening like '51%' THEN " + _namaKolom2 + " ELSE 0 END) as BO, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN " + _namaKolom2 + " ELSE 0 END) as BM," +
                    " SUM (Case WHEN iidrekening like '53%' THEN " + _namaKolom2 + " ELSE 0 END) as BTT," +
                    " SUM (Case WHEN iidrekening like '54%' THEN " + _namaKolom2 + " ELSE 0 END) as BT" +
                    " FROM " + namaView + "  A INNER JOIN  " + namaViewSubKegiatan + "  B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan " +
                    " AND A.IDDInas = B.IDDInas AND A.IDProgram = B.IDProgram AND A.IDKegiatan= B.IDkegiatan AND A.IDSubKegiatan= B.IDSubkegiatan  AND A.btJenis = B.btJenis  " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND A.btJenis in (2,3 ) " +
                    " group BY A.PARENT/100,A.IDUrusan,A.IDProgram,A.IDKegiatan , A.IDSUbKegiatan, B.Nama " +
                    " UNION ALL ";


                SSQL = SSQL + " Select 7 as Level,  0 as IDDInas,999 as IDUrusan,0 as IDProgram, 0 as IDKegiatan,0 as IDSUBKegiatan, 'JUMLAH' as Nama,  " +
                   " SUM (Case WHEN iidrekening like '51%' THEN " + _namaKolom1 + " ELSE 0 END) as BOMurni, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN " + _namaKolom1 + " ELSE 0 END) as BMMurni," +
                    " SUM (Case WHEN iidrekening like '53%' THEN " + _namaKolom1 + " ELSE 0 END) as BTTMurni, " +
                    " SUM (Case WHEN iidrekening like '54%' THEN " + _namaKolom1 + " ELSE 0 END) as BTMurni, " +
                    " SUM (Case WHEN iidrekening like '51%' THEN " + _namaKolom2 + " ELSE 0 END) as BO, " +
                    " SUM (Case WHEN iidrekening like '52%' THEN " + _namaKolom2 + " ELSE 0 END) as BM," +
                    " SUM (Case WHEN iidrekening like '53%' THEN " + _namaKolom2 + " ELSE 0 END) as BTT," +
                    " SUM (Case WHEN iidrekening like '54%' THEN " + _namaKolom2 + " ELSE 0 END) as BT" +
                    " FROM " + namaView + "  A INNER JOIN " + namaViewSubKegiatan + " B ON A.iTahun = B.iTahun AND A.IDUrusan = B.IDUrusan AND A.btJenis = B.btJenis " +
                    " AND A.IDProgram = B.IDProgram AND A.IDKegiatan= B.IDkegiatan AND A.IDSubKegiatan= B.IDSubkegiatan AND A.IDDInas = B.IDDInas " +
                    " WHERE A.iTahun = " + _p.Tahun.ToString() + " AND A.btJenis in (2,3 ) " +
                    " Order BY IDUrusan ,IDDInas, IDProgram ,IDKegiatan ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIV()
                                {

                                    Level = DataFormat.GetInteger(dr["Level"]),
                                    Kode = GetKodeIV(DataFormat.GetInteger(dr["Level"]), DataFormat.GetInteger(dr["IDUrusan"]), DataFormat.GetInteger(dr["IDDInas"]), DataFormat.GetInteger(dr["IDProgram"]), DataFormat.GetInteger(dr["IDKegiatan"]), DataFormat.GetLong(dr["IDSubKegiatan"])),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDkegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSubkegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),

                                    Nama = DataFormat.GetInteger(dr["Level"]) < 6 ? DataFormat.GetString(dr["Nama"]).ToUpper() : DataFormat.GetString(dr["Nama"]),

                                    BO = DataFormat.GetDecimal(dr["BO"]).ToRupiahInReport(),
                                    BM = DataFormat.GetDecimal(dr["BM"]).ToRupiahInReport(),
                                    BTT = DataFormat.GetDecimal(dr["BTT"]).ToRupiahInReport(),
                                    BT = DataFormat.GetDecimal(dr["BT"]).ToRupiahInReport(),
                                    Jumlah = (DataFormat.GetDecimal(dr["BO"]) + DataFormat.GetDecimal(dr["BM"]) + DataFormat.GetDecimal(dr["BT"]) + DataFormat.GetDecimal(dr["BTT"])).ToRupiahInReport(),
                                    BOMurni = DataFormat.GetDecimal(dr["BOMurni"]).ToRupiahInReport(),
                                    BMMurni = DataFormat.GetDecimal(dr["BMMurni"]).ToRupiahInReport(),
                                    BTTMurni = DataFormat.GetDecimal(dr["BTTMurni"]).ToRupiahInReport(),
                                    BTMurni = DataFormat.GetDecimal(dr["BTMurni"]).ToRupiahInReport(),

                                    JunmlahMurni = (DataFormat.GetDecimal(dr["BOMurni"]) + DataFormat.GetDecimal(dr["BMMurni"]) + DataFormat.GetDecimal(dr["BTMurni"]) + DataFormat.GetDecimal(dr["BTTMurni"])).ToRupiahInReport(),

                                    Selisih = ((DataFormat.GetDecimal(dr["BO"]) + DataFormat.GetDecimal(dr["BM"]) + DataFormat.GetDecimal(dr["BT"]) + DataFormat.GetDecimal(dr["BTT"])) - (DataFormat.GetDecimal(dr["BOMurni"]) + DataFormat.GetDecimal(dr["BMMurni"]) + DataFormat.GetDecimal(dr["BTMurni"]) + DataFormat.GetDecimal(dr["BTTMurni"]))).ToRupiahInReport(),
                                    persentase = DataFormat.GetProsentase((DataFormat.GetDecimal(dr["BO"]) + DataFormat.GetDecimal(dr["BM"]) + DataFormat.GetDecimal(dr["BT"]) + DataFormat.GetDecimal(dr["BTT"])),
                                                                           (DataFormat.GetDecimal(dr["BOMurni"]) + DataFormat.GetDecimal(dr["BMMurni"]) + DataFormat.GetDecimal(dr["BTMurni"]) + DataFormat.GetDecimal(dr["BTTMurni"])))
                                }).ToList();

                    }
                }


                PerdaIV blanjPerda = new PerdaIV();

                blanjPerda.Level = 0;
                blanjPerda.Kode = "";
                blanjPerda.IDDInas = 0;
                blanjPerda.IDUrusan = 0;
                blanjPerda.IDProgram = 0;
                blanjPerda.IDkegiatan = 0;
                blanjPerda.Nama = "";
                blanjPerda.BP = "";
                blanjPerda.BBJ = "";
                blanjPerda.BM = "";
                blanjPerda.Jumlah = "";
                blanjPerda.BPMurni = "0";
                blanjPerda.BBJMurni = "0";
                blanjPerda.BMMurni = "0";
                blanjPerda.JunmlahMurni = "0";
                blanjPerda.Selisih = "0";
                blanjPerda.persentase = "0";

                int idxprg = 0;
                int ihasrussisip = 0;
                foreach (PerdaIV p in _lst)
                {
                    if (p.IDUrusan == 406 && p.IDProgram == 40630 && p.IDkegiatan == 0)
                    {
                        ihasrussisip = idxprg;
                    }
                    idxprg++;


                }
                _lst.Insert(ihasrussisip, blanjPerda);
                CreateViewSubKegiatan(_p, false);
                CreateViewKegiatan(_p, false);
                CreateViewProgram(_p, false);
                return _lst;
            }


            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }

        }

        private string GetKodeIV(int _level, int _idURusan, int _iddinas, int _idProgram, int _idKegiatan, long idSubKegiatan = 0)
        {
            string s = "";
            switch (_level)
            {
                case 1:
                    s = _idURusan.ToKodeUrusan();
                    break;
                case 2:
                    s = _idURusan.ToKodeUrusan();
                    break;
                case 3:
                    s = _iddinas.ToKodeDinas();
                    break;
                case 4:
                    s = _idURusan.ToKodeUrusan() + "." + _iddinas.ToKodeDinas() + "." + _idProgram.ToSimpleKodeProgram();
                    break;
                case 5:
                    s = _idURusan.ToKodeUrusan() + "." + _iddinas.ToKodeDinas() + "." + _idProgram.ToSimpleKodeProgram() + "." + _idKegiatan.ToSimpleKodeKegiatan();
                    break;
                case 6:
                    {
                        if (_idProgram > 0)


                            s = _idURusan.ToKodeUrusan() + "." + _iddinas.ToKodeDinas() + "." + _idProgram.ToSimpleKodeProgram() + "." + _idKegiatan.ToSimpleKodeKegiatan() + "." + idSubKegiatan.ToString().Substring(idSubKegiatan.ToString().Trim().Length - 2);
                        else
                            s = "";
                    }
                    break;


            }
            return s;
        }

        private string GetKodeIV77(int _level, int _idURusan, int _iddinas, int _idProgram, int _idKegiatan, long idSubKegiatan = 0, long IDSUBKEGIATAN = 0)
        {
            string s = "";
            switch (_level)
            {
                case 1:
                    s = _idURusan.ToKodeUrusan();
                    break;
                case 2:
                    s = _idURusan.ToKodeUrusan();
                    break;
                case 3:
                    s = _iddinas.ToKodeDinas();
                    break;
                case 4:
                    s = _idURusan.ToKodeUrusan() + "." + _iddinas.ToKodeDinas() + "." + _idProgram.ToSimpleKodeProgram();
                    break;
                case 5:
                    s = _idURusan.ToKodeUrusan() + "." + _iddinas.ToKodeDinas() + "." + _idProgram.ToSimpleKodeProgram() + "." + _idKegiatan.ToSimpleKodeKegiatan();
                    break;
                case 6:
                    {
                        if (_idProgram > 0)


                            s = _idURusan.ToKodeUrusan() + "." + _iddinas.ToKodeDinas() + "." + _idProgram.ToSimpleKodeProgram() + "." + _idKegiatan.ToSimpleKodeKegiatan() + "." + idSubKegiatan.ToString().Substring(idSubKegiatan.ToString().Trim().Length - 2);
                        else
                            s = "";
                    }
                    break;


            }
            if (IDSUBKEGIATAN > 0)
                s = s + "." + IDSUBKEGIATAN.ToString().Substring(IDSUBKEGIATAN.ToString().Length - 2);
            return s;
        }
        private string GetKodeIV050(int _level, int kodekategori, int kodeurusan = 0, int kodeprogram = 0, int kodekegiatan = 0, int kodesubkegiatan = 0, string kode = "")
        {
            string s = "";
            s = kodekategori.ToString();
            if (kodeurusan > 0)
            {
                s = s + "." + kodeurusan.IntToStringWithLeftPad(2);
                if (kodeprogram > 0)
                    if (kode.Length > 0)
                    {
                        s = s + "." + kode.Trim();
                        {
                            s = s + "." + kodeprogram.IntToStringWithLeftPad(2);
                            if (kodekegiatan > 0)
                            {
                                s = s + "." + kodekegiatan.IntToStringWithLeftPad(3);
                                if (kodesubkegiatan > 0)
                                {
                                    s = s + "." + kodesubkegiatan.IntToStringWithLeftPad(2);


                                }

                            }

                        }
                    }

            }
            return s;
        }


        // CEK View utk Perdan 
        // 
        private string CreateViewAnggaran(ParameterLaporan _p, bool create)
        {
            string namaView = "viewAnggaranOnLevel" + _p.NamaUser.Trim().Replace(" ", "");

            HapusView(namaView);



            if (create == true)
            {
                GetKolom(_p.Tahap);

                //GetKolom(_p.Tahap);
                //if (Tahun < 2020)
                //{
                if (Tahun <= 2020)
                {
                    SSQL = " CREATE VIEW " + namaView + " AS " +
                        " Select C.ID as Parent, A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, A.IIDRekening as Rek,b.IIDRekening, b.btRoot as Root," +
                        " b.sNamaRekening ,  A.cPlafon, a.cplafonabt, A.cJumlahRKA, A.cJumlahMurni,A.cJumlahRKAP, A.cJumlahabt,cjumlahgeser  " +
                        " FROM tAnggaranRekening_A A inner join mRekening b on a.IIDRekening=b.IIDRekening  inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND " +
                        " A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where  b.btRoot=5 and C.Root =1 ";
                }
                else
                {
                    SSQL = " CREATE VIEW " + namaView + " AS " +
                        " Select C.Parent, A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan , A.IDSUBKEGIATAN, A.IIDRekening as Rek,b.IIDRekening, b.btRoot as Root," +
                        " b.sNamaRekening ,  A.cPlafon, a.cplafonabt, A.cJumlahRKA, A.cJumlahMurni,A.cJumlahRKAP, A.cJumlahabt,cjumlahgeser " +
                        " FROM tAnggaranRekening_A A inner join mRekening b on a.IIDRekening=b.IIDRekening  inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TSubKegiatan D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND " +
                        " A.IDKegiatan = D.IDKegiatan and A.IDSubKegiatan = D.IDSubKegiatan and A.btJenis = D.btJenis  where b.btRoot=6 and C.Root =1 ";

                }
                _dbHelper.ExecuteNonQuery(SSQL);
            }
            return namaView;
        }


        private string CreateViewProgram(ParameterLaporan _p, bool create)
        {
            string namaView = "viewProgramOnLevel" + _p.NamaUser.Trim().Replace(" ", "");

            HapusView(namaView);

            GetKolom(_p.Tahap);


            if (create == true)
            {
                //GetKolom(_p.Tahap);
                //if (Tahun < 2020)
                //{
                if (Tahun < 2020)

                    SSQL = " CREATE VIEW " + namaView + " AS " +
                        " Select  distinct  B.ID as Parent, A.IDDinas, A.IDUrusan,A.IDProgram, A.sNamaProgram, A.btJenis,A.iTahun FROM tPrograms_A A inner join mSKPD B on A.iDDINAS = B.id ";

                else
                    SSQL = " CREATE VIEW " + namaView + " AS " +
                        " Select  distinct  B.Parent, A.IDDinas, A.IDUrusan,A.IDProgram, A.sNamaProgram, A.btJenis,A.iTahun FROM tPrograms_A A inner join mSKPD B on A.iDDINAS = B.id ";

                _dbHelper.ExecuteNonQuery(SSQL);
            }
            return namaView;
        }
        private string CreateViewParent(ParameterLaporan _p, bool create)
        {
            string namaView = "viewProgramOnLevel" + _p.NamaUser.Trim().Replace(" ", "");

            HapusView(namaView);

            GetKolom(_p.Tahap);


            if (create == true)
            {
                //GetKolom(_p.Tahap);
                //if (Tahun < 2020)
                //{
                if (Tahun < 2020)

                    SSQL = " CREATE VIEW " + namaView + " AS " +
                        " Select distinct  B.ID as Parent, A.IDDinas, A.IDUrusan,A.IDProgram, A.sNamaProgram, A.btJenis,A.iTahun FROM tPrograms_A A inner join mSKPD B on A.iDDINAS = B.id ";

                else
                    SSQL = " CREATE VIEW " + namaView + " AS " +
                        " Select distinct  B.Parent, A.IDDinas, A.IDUrusan,A.IDProgram, A.sNamaProgram, A.btJenis,A.iTahun FROM tPrograms_A A inner join mSKPD B on A.iDDINAS = B.id ";

                _dbHelper.ExecuteNonQuery(SSQL);
            }
            return namaView;
        }
        private string CreateViewKegiatan(ParameterLaporan _p, bool create)
        {
            string namaView = "viewKegiatanOnParent" + _p.NamaUser.Trim().Replace(" ", "");

            HapusView(namaView);

            GetKolom(_p.Tahap);


            if (create == true)
            {
                //GetKolom(_p.Tahap);
                //if (Tahun < 2020)
                //{
                if (Tahun < 2020)

                    SSQL = " CREATE VIEW " + namaView + " AS " +
                        " Select distinct B.ID as Parent, A.IDDinas, A.IDUrusan,A.IDProgram, A.IDKegiatan,A.sNama, A.btJenis,A.iTahun,sLokasi FROM tKegiatan_A A inner join mSKPD B on A.iDDINAS = B.id ";

                else
                    SSQL = " CREATE VIEW " + namaView + " AS " +
                        "Select distinct B.Parent, A.IDDinas, A.IDUrusan,A.IDProgram, A.IDKegiatan,A.sNama, A.btJenis,A.iTahun,sLokasi FROM tKegiatan_A A inner join mSKPD B on A.iDDINAS = B.id ";

                _dbHelper.ExecuteNonQuery(SSQL);


            }
            return namaView;
        }

        private string CreateViewSubKegiatan(ParameterLaporan _p, bool create)
        {
            string namaView = "viewSubKegiatanOnParent" + _p.NamaUser.Trim().Replace(" ", "");

            HapusView(namaView);

            GetKolom(_p.Tahap);


            if (create == true)
            {
                if (Tahun < 2020)

                    SSQL = " CREATE VIEW " + namaView + " AS " +
                        " Select distinct B.ID as Parent, A.IDDinas, A.IDUrusan,A.IDProgram, A.IDKegiatan,A.IDSUBKegiatan,A.Nama, A.btJenis,A.iTahun,sLokasi FROM TSubKegiatan A inner join mSKPD B on A.iDDINAS = B.id ";

                else
                    SSQL = " CREATE VIEW " + namaView + " AS " +
                        "Select distinct B.Parent, A.IDDinas, A.IDUrusan,A.IDProgram, A.IDKegiatan,A.IDSUBKegiatan,A.Nama, A.btJenis,A.iTahun,sLokasi FROM TSubKegiatan A inner join mSKPD B on A.iDDINAS = B.id ";

                _dbHelper.ExecuteNonQuery(SSQL);


            }
            return namaView;
        }

        public string CreateViewAllLevelRealisasi(ParameterLaporan _p, bool create)
        {
            string namaView = "viewAnggaran" + _p.NamaUser.Trim().Replace(" ", "");

            HapusView(namaView);

            GetKolom(_p.Tahap);
            // GetKolom(1);
            string sDate = _p.TanggalRealisasi.ToSQLFormat();


            if (create == true)
            {
                //d
                // int root;
                //root = 0;
                SSQL = " CREATE VIEW " + namaView + " AS ";

                SSQL = SSQL + " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening, " + m_ProfileRekening.LEN6.ToString() + " ) as Rek,b.IIDRekening,b.btRoot as Root," +
                                       " b.sNamaRekening , SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet ,A.bPPKD " +
                                       " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN6.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN6.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                                     " INNER JOIN TSUBKegiatan D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  AND A.IDSubKegiatan  = D.IDSubKegiatan   where b.btRoot=6 and c.root = 1 ";
                if (_p.IDDinas > 0 && _p.Tahap == -1 && _p.Tahun == 2021)
                    SSQL = SSQL + " AND A.IIDRekening not in (51050701001,54020501001,54010101001,51030101001,51050503001) ";
                SSQL = SSQL + " Group BY C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, A.IIDRekening, b.btRoot ,b.IIDRekening, b.sNamaRekening ,b.iDebet,A.bPPKD ";

                SSQL = SSQL + " UNION ALL  ";

                SSQL = SSQL + " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening, " + m_ProfileRekening.LEN5.ToString() + " ) as Rek,b.IIDRekening,b.btRoot as Root," +
                  " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet ,A.bPPKD " +
                  " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN5.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN5.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                " INNER JOIN TSUBKegiatan D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  AND A.IDSubKegiatan  = D.IDSubKegiatan   where b.btRoot=5 and c.root = 1 ";
                if (_p.IDDinas > 0 && _p.Tahap == -1 && _p.Tahun == 2021)
                    SSQL = SSQL + " AND A.IIDRekening not in (51050701001,54020501001,54010101001,51030101001,51050503001) ";

                SSQL = SSQL + " Group BY C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN5.ToString() + " ), b.btRoot ,b.IIDRekening, b.sNamaRekening ,b.iDebet,A.bPPKD ";

                SSQL = SSQL + " UNION ALL  " +

                  " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + " ) as Rek,b.IIDRekening,b.btRoot as Root," +
                    " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ")  AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet ,A.bPPKD " +
                    " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                  " INNER JOIN TSUBKegiatan D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis AND A.IDSubKegiatan  = D.IDSubKegiatan    where b.btRoot=4 and c.root = 1 ";
                if (_p.IDDinas > 0 && _p.Tahap == -1 && _p.Tahun == 2021)
                    SSQL = SSQL + " AND A.IIDRekening not in (51050701001,54020501001,54010101001,51030101001,51050503001)";

                SSQL = SSQL + " Group BY C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN4.ToString() + " ), b.btRoot ,b.IIDRekening, b.sNamaRekening ,b.iDebet,A.bPPKD ";

                SSQL = SSQL + " UNION ALL  " +
                  " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root, " +
                    " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet,A.bPPKD " +
                  " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")  " +
                  " inner join mSKPD C on a.iDDINAS = C.id " +
                " INNER JOIN TSUBKegiatan D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis AND A.IDSubKegiatan  = D.IDSubKegiatan  where b.btRoot=3 and c.root = 1  ";
                if (_p.IDDinas > 0 && _p.Tahap == -1 && _p.Tahun == 2021)
                    SSQL = SSQL + " AND A.IIDRekening not in (51050701001,54020501001,54010101001,51030101001,51050503001) ";

                SSQL = SSQL + " Group BY C.Parent, A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet,A.bPPKD   ";

                SSQL = SSQL + " UNION ALL  " +

                " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ") as Rek,b.IIDRekening, " +
                " b.btRoot as Root,b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH , SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet, A.bPPKD  " +
                " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN2.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN2.ToString() + ")   inner join mSKPD C on a.iDDINAS = C.id " +
              " INNER JOIN TSUBKegiatan D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis AND A.IDSubKegiatan  = D.IDSubKegiatan    where b.btRoot=2 and c.root = 1 ";

                if (_p.IDDinas > 0 && _p.Tahap == -1 && _p.Tahun == 2021)
                    SSQL = SSQL + " AND A.IIDRekening not in (51050701001,54020501001,54010101001,51030101001,51050503001)";

                SSQL = SSQL + " Group BY C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet, A.bPPKD  ";

                SSQL = SSQL + " UNION ALL  " +

                " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root,b.sNamaRekening ,  " +
                " SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet,A.bPPKD   " +
                " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")    inner join mSKPD C on a.iDDINAS = C.id " +
              " INNER JOIN TSUBKegiatan D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis AND A.IDSubKegiatan  = D.IDSubKegiatan    where b.btRoot=1 and c.root = 1  ";

                if (_p.IDDinas > 0 && _p.Tahap == -1 && _p.Tahun == 2021)
                    SSQL = SSQL + " AND A.IIDRekening not in (51050701001,54020501001,54010101001,51030101001,51050503001) ";

                SSQL = SSQL + " Group BY C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet,A.bPPKD ";



                _dbHelper.ExecuteNonQuery(SSQL);
            }
            return namaView;

        }

        public string CreateViewAllLevel(ParameterLaporan _p, bool create)
        {
            string namaView = "viewAnggaran" + _p.NamaUser.Trim().Replace(" ", "");

            HapusView(namaView);

            GetKolom(_p.Tahap);
            // GetKolom(1);


            if (create == true)
            {

                // int root;
                //root = 0;
                SSQL = " CREATE VIEW " + namaView + " AS ";
                if (Tahun >= 2021)
                {
                    SSQL = SSQL + " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening, " + m_ProfileRekening.LEN6.ToString() + " ) as Rek,b.IIDRekening,b.btRoot as Root," +
                                     " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet ,A.bPPKD " +
                                     " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN6.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN6.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                                   " INNER JOIN TSUBKegiatan D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  AND A.IDSubKegiatan  = D.IDSubKegiatan   where b.btRoot=6 and c.root = 1 ";
                    if (_p.IDDinas > 0 && _p.Tahap == -1 && _p.Tahun == 2021)
                        SSQL = SSQL + " AND A.IIDRekening not in (51050701001,54020501001,54010101001,51030101001,51050503001) ";


                    SSQL = SSQL + " Group BY C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN6.ToString() + " ), b.btRoot ,b.IIDRekening, b.sNamaRekening ,b.iDebet,A.bPPKD ";

                    SSQL = SSQL + " UNION ALL  ";



                    SSQL = SSQL + " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening, " + m_ProfileRekening.LEN5.ToString() + " ) as Rek,b.IIDRekening,b.btRoot as Root," +
                      " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet ,A.bPPKD " +
                      " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN5.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN5.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                    " INNER JOIN TSUBKegiatan D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  AND A.IDSubKegiatan  = D.IDSubKegiatan   where b.btRoot=5 and c.root = 1 ";
                    if (_p.IDDinas > 0 && _p.Tahap == -1 && _p.Tahun == 2021)
                        SSQL = SSQL + " AND A.IIDRekening not in (51050701001,54020501001,54010101001,51030101001,51050503001) ";

                    SSQL = SSQL + " Group BY C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN5.ToString() + " ), b.btRoot ,b.IIDRekening, b.sNamaRekening ,b.iDebet,A.bPPKD ";

                    SSQL = SSQL + " UNION ALL  " +

                      " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + " ) as Rek,b.IIDRekening,b.btRoot as Root," +
                        " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet ,A.bPPKD " +
                        " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                      " INNER JOIN TSUBKegiatan D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis AND A.IDSubKegiatan  = D.IDSubKegiatan    where b.btRoot=4 and c.root = 1 ";
                    if (_p.IDDinas > 0 && _p.Tahap == -1 && _p.Tahun == 2021)
                        SSQL = SSQL + " AND A.IIDRekening not in (51050701001,54020501001,54010101001,51030101001,51050503001)";

                    SSQL = SSQL + " Group BY C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN4.ToString() + " ), b.btRoot ,b.IIDRekening, b.sNamaRekening ,b.iDebet,A.bPPKD ";

                    SSQL = SSQL + " UNION ALL  " +
                      " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root, " +
                        " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet,A.bPPKD " +
                      " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")  " +
                      " inner join mSKPD C on a.iDDINAS = C.id " +
                    " INNER JOIN TSUBKegiatan D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis AND A.IDSubKegiatan  = D.IDSubKegiatan  where b.btRoot=3 and c.root = 1  ";
                    if (_p.IDDinas > 0 && _p.Tahap == -1 && _p.Tahun == 2021)
                        SSQL = SSQL + " AND A.IIDRekening not in (51050701001,54020501001,54010101001,51030101001,51050503001) ";

                    SSQL = SSQL + " Group BY C.Parent, A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet,A.bPPKD   ";

                    SSQL = SSQL + " UNION ALL  " +

                    " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ") as Rek,b.IIDRekening, " +
                    " b.btRoot as Root,b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet, A.bPPKD  " +
                    " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN2.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN2.ToString() + ")   inner join mSKPD C on a.iDDINAS = C.id " +
                  " INNER JOIN TSUBKegiatan D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis AND A.IDSubKegiatan  = D.IDSubKegiatan    where b.btRoot=2 and c.root = 1 ";

                    if (_p.IDDinas > 0 && _p.Tahap == -1 && _p.Tahun == 2021)
                        SSQL = SSQL + " AND A.IIDRekening not in (51050701001,54020501001,54010101001,51030101001,51050503001)";

                    SSQL = SSQL + " Group BY C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet, A.bPPKD  ";

                    SSQL = SSQL + " UNION ALL  " +

                    " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root,b.sNamaRekening ,  " +
                    " SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet,A.bPPKD   " +
                    " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")    inner join mSKPD C on a.iDDINAS = C.id " +
                  " INNER JOIN TSUBKegiatan D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis AND A.IDSubKegiatan  = D.IDSubKegiatan    where b.btRoot=1 and c.root = 1  ";

                    if (_p.IDDinas > 0 && _p.Tahap == -1 && _p.Tahun == 2021)
                        SSQL = SSQL + " AND A.IIDRekening not in (51050701001,54020501001,54010101001,51030101001,51050503001) ";

                    SSQL = SSQL + " Group BY C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet,A.bPPKD ";

                    // 2022****************************************************************
                    if (Tahun >= 2022)
                    {

                        SSQL = " CREATE VIEW " + namaView + " AS ";
                        SSQL = SSQL + " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening, " + m_ProfileRekening.LEN6.ToString() + " ) as Rek,b.IIDRekening,b.btRoot as Root," +
                                      " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet ,A.bPPKD " +
                                      " FROM dbo.dboGetRealisasiRS (" + _p.Tahun.ToString() + " ," + _p.TanggalRealisasi.ToSQLFormat() + ") A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN6.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN6.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                                    " INNER JOIN TSUBKegiatan D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  AND A.IDSubKegiatan  = D.IDSubKegiatan   where b.btRoot=6 and c.root = 1 ";


                        SSQL = SSQL + " Group BY C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN6.ToString() + " ), b.btRoot ,b.IIDRekening, b.sNamaRekening ,b.iDebet,A.bPPKD ";

                        SSQL = SSQL + " UNION ALL  ";



                        SSQL = SSQL + " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening, " + m_ProfileRekening.LEN5.ToString() + " ) as Rek,b.IIDRekening,b.btRoot as Root," +
                          " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet ,A.bPPKD " +
                          " FROM dbo.dboGetRealisasiRS (" + _p.Tahun.ToString() + " ," + _p.TanggalRealisasi.ToSQLFormat() + ") A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN5.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN5.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TSUBKegiatan D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  AND A.IDSubKegiatan  = D.IDSubKegiatan   where b.btRoot=5 and c.root = 1 ";
                        if (_p.IDDinas > 0 && _p.Tahap == -1 && _p.Tahun == 2021)
                            SSQL = SSQL + " AND A.IIDRekening not in (51050701001,54020501001,54010101001,51030101001,51050503001) ";

                        SSQL = SSQL + " Group BY C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN5.ToString() + " ), b.btRoot ,b.IIDRekening, b.sNamaRekening ,b.iDebet,A.bPPKD ";

                        SSQL = SSQL + " UNION ALL  " +

                          " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + " ) as Rek,b.IIDRekening,b.btRoot as Root," +
                            " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet ,A.bPPKD " +
                            " FROM dbo.dboGetRealisasiRS (" + _p.Tahun.ToString() + " ," + _p.TanggalRealisasi.ToSQLFormat() + ") A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                          " INNER JOIN TSUBKegiatan D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis AND A.IDSubKegiatan  = D.IDSubKegiatan    where b.btRoot=4 and c.root = 1 ";
                        if (_p.IDDinas > 0 && _p.Tahap == -1 && _p.Tahun == 2021)
                            SSQL = SSQL + " AND A.IIDRekening not in (51050701001,54020501001,54010101001,51030101001,51050503001)";

                        SSQL = SSQL + " Group BY C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN4.ToString() + " ), b.btRoot ,b.IIDRekening, b.sNamaRekening ,b.iDebet,A.bPPKD ";

                        SSQL = SSQL + " UNION ALL  " +
                          " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root, " +
                            " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet,A.bPPKD " +
                          " FROM dbo.dboGetRealisasiRS (" + _p.Tahun.ToString() + " ," + _p.TanggalRealisasi.ToSQLFormat() + ") A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")  " +
                          " inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TSUBKegiatan D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis AND A.IDSubKegiatan  = D.IDSubKegiatan  where b.btRoot=3 and c.root = 1  ";
                        if (_p.IDDinas > 0 && _p.Tahap == -1 && _p.Tahun == 2021)
                            SSQL = SSQL + " AND A.IIDRekening not in (51050701001,54020501001,54010101001,51030101001,51050503001) ";

                        SSQL = SSQL + " Group BY C.Parent, A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet,A.bPPKD   ";

                        SSQL = SSQL + " UNION ALL  " +

                        " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ") as Rek,b.IIDRekening, " +
                        " b.btRoot as Root,b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet, A.bPPKD  " +
                        " FROM dbo.dboGetRealisasiRS (" + _p.Tahun.ToString() + " ," + _p.TanggalRealisasi.ToSQLFormat() + ") A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN2.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN2.ToString() + ")   inner join mSKPD C on a.iDDINAS = C.id " +
                      " INNER JOIN TSUBKegiatan D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis AND A.IDSubKegiatan  = D.IDSubKegiatan    where b.btRoot=2 and c.root = 1 ";

                        if (_p.IDDinas > 0 && _p.Tahap == -1 && _p.Tahun == 2021)
                            SSQL = SSQL + " AND A.IIDRekening not in (51050701001,54020501001,54010101001,51030101001,51050503001)";

                        SSQL = SSQL + " Group BY C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet, A.bPPKD  ";

                        SSQL = SSQL + " UNION ALL  " +

                        " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root,b.sNamaRekening ,  " +
                        " SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet,A.bPPKD   " +
                        " FROM dbo.dboGetRealisasiRS (" + _p.Tahun.ToString() + " ," + _p.TanggalRealisasi.ToSQLFormat() + ") A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")    inner join mSKPD C on a.iDDINAS = C.id " +
                      " INNER JOIN TSUBKegiatan D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis AND A.IDSubKegiatan  = D.IDSubKegiatan    where b.btRoot=1 and c.root = 1  ";

                        if (_p.IDDinas > 0 && _p.Tahap == -1 && _p.Tahun == 2021)
                            SSQL = SSQL + " AND A.IIDRekening not in (51050701001,54020501001,54010101001,51030101001,51050503001) ";

                        SSQL = SSQL + " Group BY C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSubKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet,A.bPPKD ";


                    }


                }
                else
                {

                    SSQL = SSQL + " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening, " + m_ProfileRekening.LEN5.ToString() + " ) as Rek,b.IIDRekening,b.btRoot as Root," +
                     " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet ,A.bPPKD " +
                     " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN5.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN5.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                   " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis   where b.btRoot=5 and c.root = 1 " +
                     " Group BY C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN5.ToString() + " ), b.btRoot ,b.IIDRekening, b.sNamaRekening ,b.iDebet,A.bPPKD " +
                    " UNION ALL  " +
                   " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + " ) as Rek,b.IIDRekening,b.btRoot as Root," +
                     " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet ,A.bPPKD " +
                     " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                   " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis   where b.btRoot=4 and c.root = 1 " +
                     " Group BY C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN4.ToString() + " ), b.btRoot ,b.IIDRekening, b.sNamaRekening ,b.iDebet,A.bPPKD " +
                     " UNION ALL  " +
                     " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root, " +
                       " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet,A.bPPKD " +
                     " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")  " +
                     " inner join mSKPD C on a.iDDINAS = C.id " +
                   " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=3 and c.root = 1  " +
                     " Group BY C.Parent, A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet,A.bPPKD   " +
                     " UNION ALL  " +
                     " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ") as Rek,b.IIDRekening, " +
                     " b.btRoot as Root,b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet, A.bPPKD  " +
                     " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN2.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN2.ToString() + ")   inner join mSKPD C on a.iDDINAS = C.id " +
                   " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=2 and c.root = 1  " +
                     " Group BY C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet, A.bPPKD  " +
                     " UNION ALL  " +
                     " Select C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root,b.sNamaRekening ,  " +
                     " SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet,A.bPPKD   " +
                     " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")    inner join mSKPD C on a.iDDINAS = C.id " +
                   " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=1 and c.root = 1  " +
                     " Group BY C.Parent,A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet,A.bPPKD ";
                }
                _dbHelper.ExecuteNonQuery(SSQL);
            }
            return namaView;

        }

        private string CreateViewAllLevelLama(ParameterLaporan _p, bool create)
        {
            string namaView = "viewAnggaran" + _p.NamaUser.Trim();


            SSQL = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[" + namaView + "]')) " +
                " DROP VIEW [dbo].[" + namaView + "]";
            _dbHelper.ExecuteNonQuery(SSQL);

            if (create == true)
            {
                GetKolom(_p.Tahap);

                SSQL = " CREATE VIEW " + namaView + " AS " +
                    " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, A.IIDRekening as Rek,b.IIDRekening, b.btRoot as Root," +
                    " b.sNamaRekening ,  A." + _namaKolom2 + " AS Jumlah, A." + _namaKolom1 + " AS JumlahMurni ,b.iDebet as Debet  " +
                    " FROM tAnggaranRekening_A A inner join mRekening b on a.IIDRekening=b.IIDRekening    where b.btRoot=5  " +
                    " UNION ALL  " +
                      " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + " ) as Rek,b.IIDRekening,b.btRoot as Root," +
                      " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                      " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")  where b.btRoot=4  " +
                      " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN4.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening ,b.iDebet " +
                      " UNION ALL  " +
                      " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root, " +
                        " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet " +
                      " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")  " +
                      " where b.btRoot=3  " +
                      " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet  " +
                      " UNION ALL  " +
                      " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ") as Rek,b.IIDRekening, " +
                      " b.btRoot as Root,b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                      " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN2.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN2.ToString() + ")   where b.btRoot=2  " +
                      " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet  " +
                      " UNION ALL  " +
                      " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root,b.sNamaRekening ,  " +
                      " SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                      " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")    where b.btRoot=1 " +
                      " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet";

                _dbHelper.ExecuteNonQuery(SSQL);
            }
            return namaView;

        }


        public List<PerdaIII> GetPerdaIIISampaj(ParameterLaporan _p)
        {


            string namaView = CreateViewAllLevel(_p, true);

            BersihkanNonKegiatan();
            List<PerdaIII> _lst = new List<PerdaIII>();
            try
            {

                SSQL = "Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok from " + namaView + " " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root <  " + _p.LastLevel.ToString() + " and " + namaView + ".Jumlah>0   ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }

                SSQL = SSQL + " UNION ALL    ";

                SSQL = SSQL + "  Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo,''  as Keterangan, 0 as isPokok from " + namaView +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root = 3 and " + namaView + ".Jumlah > 0   ";//and  " +
                //namaView + ".IDDInas =" + _p.IDDinas.ToString();




                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                //  SSQL = SSQL + "and (( mDasarHukum.IIDRekening in (Select IIDRekening from tAnggaranRekening_A where IDDInas=" + _p.IDDinas.ToString() + " )) ";
                //  SSQL = SSQL + "OR ( mDasarHukum.IIDRekening in (Select IIDRekening/100 * 100 from tAnggaranRekening_A where IDDInas=" + _p.IDDinas.ToString() + " )) ) ";

                SSQL = SSQL + "Union ALL Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah," +
                            "JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok from " + namaView + " WHERE " + namaView + ".iTAhun= " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 " +
                                " and " + namaView + ".Root = 3 and " + namaView + ".Jumlah > 0   AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + " ";// and " + namaView + ".IIDRekening/10000 not in (Select IIDRekening/10000 from mDasarHukum) ";

                SSQL = SSQL + " UNION ALL    ";
                SSQL = SSQL + " Select 2 as btJenis," + namaView + ".IDDInas/10000 as IDUrusan ," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, 0 as iNo, '' as Keterangan , 0 as isPokok from " + namaView + "  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  and " + namaView + ".btJenis in (2,3) and " + namaView + ".Root =1 and " + namaView + ".Jumlah>0    ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY  " + namaView + ".IDDInas," + namaView + ".IIDREKENING,ROOT,sNamaRekening, 0 as iNo, '' as sKeterangan  ";
                SSQL = SSQL + " UNION ALL    ";




                SSQL = SSQL + " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo, '' as sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis= 2 and " + namaView + ".Root >2  and " + namaView + ".root<= " + _p.LastLevel + " and " + namaView + ".Jumlah>0   and " + namaView + ".Root <6   ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " UNION ALL ";
                SSQL = SSQL + " Select " + namaView + ".btJenis, " + namaView + ".IDDInas/10000 as  IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 AS IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening , SUM(Jumlah) AS Jumlah, SUM(JumlahMurni) AS JumlahMurni ,0 as iNo, '' as sKeterangan as Keterangan , 0 as IsPokok  from " + namaView + "  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis= 3 and Root =2  ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }

                SSQL = SSQL + " GROUP BY " + namaView + ".btJenis," + namaView + ".IDDInas, " + namaView + ".IIDRekening,Root,sNamaRekening";

                SSQL = SSQL + "  UNION ALL  ";

                SSQL = SSQL + "  Select 3 as btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, 0 as IDKegiatan,0 as IIDRekening,0 as Root, A.sNamaProgram as sNamaRekening,    " +
                    "  SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan, mPelaksanaURusan.IsPokok FROM tPrograms_A A   " +
                    " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan    " +
                    " AND A.IDProgram = B.IDProgram   " +
                    " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
                    " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND   B.btJEnis= 3 and b.Root = 5 AND (b.Jumlah>0 or B.JumlahMurni>0)   ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();
                }

                SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram, A.sNamaProgram,mPelaksanaURusan.IsPokok";
                //if (_p.LastLevel == 5)
                //{

                SSQL = SSQL + "  UNION ALL    " +
            " Select 3.btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, A.IDkegiatan  as IDKegiatan,0 as IIDRekening,0 as Root, A.sNama as sNamaRekening,   " +
            " SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan,mPelaksanaUrusan.IsPokok FROM tKegiatan_A A   " +
            " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan   AND A.IDProgram = B.IDProgram and A.IDkegiatan=B.IDKegiatan  " +
        " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
        " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan    " +
        " WHERE B.iTAhun = " + _p.Tahun.ToString() + " AND B.btJEnis= 3 and B.Root=5 AND (B.Jumlah>0 or B.JumlahMurni>0)";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();
                    SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok  ";

                }
                else
                {
                    SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok  ";
                }

                // }

                if (_p.LastLevel == 3)
                {
                    SSQL = SSQL + " UNION ALL  " +
                        " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni , 0 as iNo,'' as Keterangan ,mPelaksanaUrusan.IsPokok  from " + namaView + "  " +
                        " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "   AND  " + namaView + ".btJEnis in ( 3) and Root in( 3) and (Jumlah  > 0  or JumlahMurni>0)";
                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                    }

                }
                else
                {

                    SSQL = SSQL + "  UNION ALL  " +
                    " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni , 0 as iNo,''  as Keterangan ,mPelaksanaUrusan.IsPokok  from " + namaView + "  " +
                    " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis in (3) and Root = 5 and (Jumlah  > 0  or JumlahMurni>0)  ";
                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                    }

                }
                SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan," + namaView + ".IDDInas, IDProgram,IDkegiatan, " + namaView + ".IIDRekening,Root,iNo  ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIII()
                                {
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    Tahun = _p.Tahun,
                                    Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                                           DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                            DataFormat.GetInteger(dr["IDDInas"]),
                                                            DataFormat.GetInteger(dr["IDProgram"]),
                                                            DataFormat.GetInteger(dr["IDkegiatan"]),
                                                            DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                                    Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).Trim() : DataFormat.GetString(dr["sNamaRekening"]).Trim()) : "",
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport(),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"].ToString()).ToRupiahInReport(),
                                    Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                    Prosentase = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]), DataFormat.GetDecimal(dr["JumlahMurni"])),
                                    Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                    Level = DataFormat.GetSingle(dr["Root"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"])


                                }).ToList();
                        //}                        
                    }
                }

                if (_p.IDDinas > 0)
                {


                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,6 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                        " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }
                else
                {

                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,6 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                    " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }

                DataTable dtx = new DataTable();
                PerdaIII o = new PerdaIII();

                dtx = _dbHelper.ExecuteDataTable(SSQL);
                if (dtx != null)
                {
                    if (dtx.Rows.Count > 0)
                    {

                        DataRow dr = dtx.Rows[0];

                        o = new PerdaIII()
                        {
                            Tahun = _p.Tahun,
                            Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                                   DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                    DataFormat.GetInteger(dr["IDDInas"]),
                                                    DataFormat.GetInteger(dr["IDProgram"]),
                                                    DataFormat.GetInteger(dr["IDkegiatan"]),
                                                    DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                            Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                            Jumlah = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])).ToRupiahInReport(),
                            JumlahMurni = (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])).ToRupiahInReport(),
                            Selisih = "0",
                            Prosentase = "0",
                            Keterangan = DataFormat.GetString(dr["Keterangan"]),
                            Level = DataFormat.GetSingle(dr["Root"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"])

                        };

                    }
                }
                _lst.Add(o);
                //******
                int JumlahPerdaIII = _lst.Count;
                for (int idx = 0; idx < JumlahPerdaIII; idx++)
                {
                    PerdaIII p3 = _lst[idx];

                    if (p3.IDKegiatan == 0 && p3.Jenis == 1)
                    {
                        List<PerdaIII> pDasarHukum = new List<PerdaIII>();
                        pDasarHukum = GetDasarHukum(_p, p3);
                        if (pDasarHukum != null)
                        {
                            if (pDasarHukum.Count > 0)
                            {
                                _lst[idx].label = pDasarHukum[0].label;
                                _lst[idx].Keterangan = pDasarHukum[0].Keterangan;

                                for (int idxHukum = 1; idxHukum < pDasarHukum.Count; idxHukum++)
                                {
                                    idx++;
                                    _lst.Insert(idx, pDasarHukum[idxHukum]);
                                    JumlahPerdaIII++;

                                }
                            }
                        }


                    }
                }
                // Pembiayaan 
                //if (_p.IDDinas == 4040601){
                List<PerdaIII> lstpby = new List<PerdaIII>();
                lstpby = GetPerdaIIIPembiayaan(_p);
                if (lstpby.Count > 1)
                {
                    foreach (PerdaIII pby in lstpby)
                    {
                        _lst.Add(pby);
                    }
                }
                //}
                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        public List<PerdaIII> GetPerdaIIIGabungan(ParameterLaporan _p)
        {

            BersihkanNonKegiatan();


            string namaView = CreateViewAllLevel(_p, true);
            string namaViewProgram = CreateViewProgram(_p, true);
            string namaViewKegiatan = CreateViewKegiatan(_p, true);
            //string namaViewSubKegiatan = CreateViewSubKegiatan(_p, true);

            SKPDLogic oLogic = new SKPDLogic(Tahun);
            List<SKPD> lstSKPD = new List<SKPD>();
            lstSKPD = oLogic.GetByParent(_p.IDDinas);

            string strDinas = "(";
            foreach (SKPD s in lstSKPD)
            {
                strDinas = strDinas + s.ID.ToString() + ",";
            }
            strDinas = strDinas + "99)";

            List<PerdaIII> _lst = new List<PerdaIII>();
            try
            {

                SSQL = "Select " + namaView + ".btJenis," + namaView + ".IDUrusan, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok ,0 as K from " + namaView + " " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root <  " + _p.LastLevel.ToString() + " and " + namaView + ".Jumlah>0   ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas in " + strDinas;
                }

                SSQL = SSQL + " UNION ALL    ";

                SSQL = SSQL + "  Select " + namaView + ".btJenis," + namaView + ".IDUrusan,0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo,''  as Keterangan, 0 as isPokok ,0 as K from " + namaView +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root = 3 and " + namaView + ".Jumlah > 0   ";//and  " +
                //namaView + ".IDDInas =" + _p.IDDinas.ToString();




                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas in " + strDinas;
                }
                //  SSQL = SSQL + "and (( mDasarHukum.IIDRekening in (Select IIDRekening from tAnggaranRekening_A where IDDInas=" + _p.IDDinas.ToString() + " )) ";
                //  SSQL = SSQL + "OR ( mDasarHukum.IIDRekening in (Select IIDRekening/100 * 100 from tAnggaranRekening_A where IDDInas=" + _p.IDDinas.ToString() + " )) ) ";

                SSQL = SSQL + "Union ALL Select " + namaView + ".btJenis," + namaView + ".IDUrusan,0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah," +
                            "JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok,0 as K from " + namaView + " WHERE " + namaView + ".iTAhun= " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 " +
                                " and " + namaView + ".Root = 3  AND " + namaView + ".IDDinas in " + strDinas; // and " + namaView + ".IIDRekening/10000 not in (Select IIDRekening/10000 from mDasarHukum) ";

                SSQL = SSQL + " UNION ALL    ";
                SSQL = SSQL + " Select 2 as btJenis," + _p.IDDinas.ToString().Trim() + "/10000 as IDUrusan ,0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, 0 as iNo, '' as Keterangan , 0 as isPokok ,0 as K from " + namaView + " " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  and " + namaView + ".btJenis in (2,3) and " + namaView + ".Root <3  ";//and " + namaView + ".Jumlah>0    ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas in " + strDinas;
                }
                SSQL = SSQL + " GROUP BY  " + namaView + ".IIDREKENING,ROOT,sNamaRekening ";
                SSQL = SSQL + " UNION ALL    ";




                SSQL = SSQL + " Select " + namaView + ".btJenis," + namaView + ".IDUrusan,0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo, '' as Keterangan, 0 as isPokok,0 as K from " + namaView + "  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis= 2 and " + namaView + ".Root >2  and " + namaView + ".root<= " + _p.LastLevel + "  and " + namaView + ".Root <6   ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas in " + strDinas;
                }
                //SSQL = SSQL + " UNION ALL ";
                //SSQL = SSQL + " Select " + namaView + ".btJenis, " + namaView + ".IDDInas/10000 as  IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 AS IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening , SUM(Jumlah) AS Jumlah, SUM(JumlahMurni) AS JumlahMurni ,0 as iNo, '' as Keterangan , 0 as IsPokok,0 as K  from " + namaView + "  " +
                //    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 3 and Root =2  ";
                //if (_p.IDDinas > 0)
                //{
                //    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                //}

                //SSQL = SSQL + " GROUP BY " + namaView + ".btJenis," + namaView + ".IDDInas, " + namaView + ".IIDRekening,Root,sNamaRekening ";

                SSQL = SSQL + "  UNION ALL  ";

                SSQL = SSQL + "  Select 3 as btJenis,A.IDUrusan, A.IDProgram, 0 as IDKegiatan,0 as IIDRekening,0 as Root, A.sNamaProgram as sNamaRekening,    " +
                    "  SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan, mPelaksanaURusan.IsPokok,0 as K FROM " + namaViewProgram + " A   " +
                    " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan    " +
                    " AND A.IDProgram = B.IDProgram   " +
                    " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
                    " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND   B.btJEnis= 3 and b.Root = 3 AND (b.Jumlah>0 or B.JumlahMurni>0)   ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDinas in " + strDinas;
                }

                SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDProgram, A.sNamaProgram,mPelaksanaURusan.IsPokok";
                //if (_p.LastLevel == 5)
                //{

                SSQL = SSQL + "  UNION ALL    " +
            " Select 3.btJenis,A.IDUrusan, A.IDProgram, A.IDkegiatan  as IDKegiatan,0 as IIDRekening,0 as Root, A.sNama as sNamaRekening,   " +
            " SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan,mPelaksanaUrusan.IsPokok,1 as K FROM " + namaViewKegiatan + " A   " +
            " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan   AND A.IDProgram = B.IDProgram and A.IDkegiatan=B.IDKegiatan  " +
        " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
        " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan    " +
        " WHERE B.iTAhun = " + _p.Tahun.ToString() + " AND B.btJEnis= 3 and B.Root=3 AND (B.Jumlah>0 or B.JumlahMurni>0)";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDinas in " + strDinas;
                    SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok  ";

                }
                else
                {
                    SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok  ";
                }

                // }

                if (_p.LastLevel == 3)
                {
                    SSQL = SSQL + " UNION ALL  " +
                        " Select " + namaView + ".btJenis," + namaView + ".IDUrusan,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni , 0 as iNo,'' as Keterangan ,mPelaksanaUrusan.IsPokok,0 as K  from " + namaView + "  " +
                        " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "   AND  " + namaView + ".btJEnis in ( 3) and Root in( 3) and (Jumlah  > 0  or JumlahMurni>0)";
                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas in " + strDinas;
                    }

                }
                else
                {

                    SSQL = SSQL + "  UNION ALL  " +
                    " Select " + namaView + ".btJenis," + namaView + ".IDUrusan,IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni , 0 as iNo,'' as Keterangan ,mPelaksanaUrusan.IsPokok ,0 as K from " + namaView + "   " +
                    " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis in (3) and Root = 5 and (Jumlah  > 0  or JumlahMurni>0)  ";
                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas in " + strDinas;
                    }

                }
                SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan, IDProgram,IDkegiatan, " + namaView + ".IIDRekening,Root,iNo  ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIII()
                                {
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    Tahun = _p.Tahun,
                                    Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                                           DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                            _p.IDDinas,
                                                            DataFormat.GetInteger(dr["IDProgram"]),
                                                            DataFormat.GetInteger(dr["IDkegiatan"]),
                                                            DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                                    Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).Trim() : DataFormat.GetString(dr["sNamaRekening"]).Trim()) : "",
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport(),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"].ToString()).ToRupiahInReport(),
                                    Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                    Prosentase = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]), DataFormat.GetDecimal(dr["JumlahMurni"])),
                                    Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                    Level = DataFormat.GetSingle(dr["Root"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    K = DataFormat.GetInteger(dr["K"])


                                }).ToList();
                        //}                        
                    }
                }

                if (_p.IDDinas > 0)
                {


                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,6 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                        " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas in " + strDinas + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas in " + strDinas + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas in " + strDinas + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas in " + strDinas + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }
                else
                {

                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,6 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                    " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }

                DataTable dtx = new DataTable();
                PerdaIII o = new PerdaIII();

                dtx = _dbHelper.ExecuteDataTable(SSQL);
                if (dtx != null)
                {
                    if (dtx.Rows.Count > 0)
                    {

                        DataRow dr = dtx.Rows[0];

                        o = new PerdaIII()
                        {
                            Tahun = _p.Tahun,
                            Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                                   DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                    DataFormat.GetInteger(dr["IDDInas"]),
                                                    DataFormat.GetInteger(dr["IDProgram"]),
                                                    DataFormat.GetInteger(dr["IDkegiatan"]),
                                                    DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                            Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                            Jumlah = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])).ToRupiahInReport(),
                            JumlahMurni = (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])).ToRupiahInReport(),
                            Selisih = ((DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])) - (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"]))).ToRupiahInReport(),
                            Prosentase = DataFormat.GetProsentase((DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])), (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"]))), //--DataFormat.GetProsentase(DataFormat.GetDecimal(dr["JumlahMurni"]), DataFormat.GetDecimal(dr["Jumlah"])),
                            Keterangan = DataFormat.GetString(dr["Keterangan"]),
                            Level = DataFormat.GetSingle(dr["Root"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"])

                        };

                    }
                }
                _lst.Add(o);
                //******
                DasarHukumLogic oLogicDasarHukum = new DasarHukumLogic(_p.Tahun);
                List<DasarHukum> _lstDasarHukum = new List<DasarHukum>();
                Single snglOnPerda = 1;

                _lstDasarHukum = oLogicDasarHukum.Get(_p.Tahun, snglOnPerda);



                int JumlahPerdaIII = _lst.Count;

                for (int idx = 0; idx < JumlahPerdaIII; idx++)
                {
                    PerdaIII p3 = _lst[idx];
                    if (idx < _lstDasarHukum.Count)
                    {
                        _lst[idx].label = _lstDasarHukum[idx].NoUrut.ToString();
                        _lst[idx].Keterangan = _lstDasarHukum[idx].Keterangan;
                    }

                }

                // Pembiayaan 
                //if (_p.IDDinas == 4040601){
                List<PerdaIII> lstpby = new List<PerdaIII>();
                lstpby = GetPerdaIIIPembiayaan(_p);
                if (lstpby.Count > 1)
                {
                    foreach (PerdaIII pby in lstpby)
                    {
                        _lst.Add(pby);
                    }
                }
                //}
                //CreateViewAllLevel(_p, false);
                CreateViewProgram(_p, false);
                CreateViewKegiatan(_p, false);
                CreateViewSubKegiatan(_p, false);
                return _lst;


            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        public List<PerdaIII> GetPerdaIII(ParameterLaporan _p)
        {

            BersihkanNonKegiatan();


            string namaView = CreateViewAllLevel(_p, true);
            //string namaViewProgram = CreateViewProgram(_p, true);
            //string namaViewKegiatan = CreateViewKegiatan(_p, true);
            //string namaViewSubKegiatan = CreateViewSubKegiatan(_p, true);

            //SKPDLogic oLogic = new SKPDLogic((int)GlobalVar.TahunAnggaran);
            //List<SKPD> lstSKPD = new List<SKPD>();
            //lstSKPD = oLogic.GetByParent(m_IDDInas);
            //Cetak(lstSKPD, m_IDDInas);



            List<PerdaIII> _lst = new List<PerdaIII>();
            try
            {

                SSQL = "Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok ,0 as K from " + namaView + " " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root <  " + _p.LastLevel.ToString() + " and " + namaView + ".Jumlah>0   ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }

                SSQL = SSQL + " UNION ALL    ";

                SSQL = SSQL + "  Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo,''  as Keterangan, 0 as isPokok ,0 as K from " + namaView +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root = 3 and " + namaView + ".Jumlah > 0   ";//and  " +
                //namaView + ".IDDInas =" + _p.IDDinas.ToString();




                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                //  SSQL = SSQL + "and (( mDasarHukum.IIDRekening in (Select IIDRekening from tAnggaranRekening_A where IDDInas=" + _p.IDDinas.ToString() + " )) ";
                //  SSQL = SSQL + "OR ( mDasarHukum.IIDRekening in (Select IIDRekening/100 * 100 from tAnggaranRekening_A where IDDInas=" + _p.IDDinas.ToString() + " )) ) ";

                SSQL = SSQL + "Union ALL Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah," +
                            "JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok,0 as K from " + namaView + " WHERE " + namaView + ".iTAhun= " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 " +
                                " and " + namaView + ".Root = 3  AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + " ";// and " + namaView + ".IIDRekening/10000 not in (Select IIDRekening/10000 from mDasarHukum) ";

                SSQL = SSQL + " UNION ALL    ";
                SSQL = SSQL + " Select 2 as btJenis," + namaView + ".IDDInas/10000 as IDUrusan ," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, 0 as iNo, '' as Keterangan , 0 as isPokok ,0 as K from " + namaView + " " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  and " + namaView + ".btJenis in (2,3) and " + namaView + ".Root <3  ";//and " + namaView + ".Jumlah>0    ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY  " + namaView + ".IDDInas," + namaView + ".IIDREKENING,ROOT,sNamaRekening ";
                SSQL = SSQL + " UNION ALL    ";




                SSQL = SSQL + " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo, '' as Keterangan, 0 as isPokok,0 as K from " + namaView + "  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis= 2 and " + namaView + ".Root >2  and " + namaView + ".root<= " + _p.LastLevel + "  and " + namaView + ".Root <6   ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                //SSQL = SSQL + " UNION ALL ";
                //SSQL = SSQL + " Select " + namaView + ".btJenis, " + namaView + ".IDDInas/10000 as  IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 AS IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening , SUM(Jumlah) AS Jumlah, SUM(JumlahMurni) AS JumlahMurni ,0 as iNo, '' as Keterangan , 0 as IsPokok,0 as K  from " + namaView + "  " +
                //    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 3 and Root =2  ";
                //if (_p.IDDinas > 0)
                //{
                //    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                //}

                //SSQL = SSQL + " GROUP BY " + namaView + ".btJenis," + namaView + ".IDDInas, " + namaView + ".IIDRekening,Root,sNamaRekening ";

                SSQL = SSQL + "  UNION ALL  ";

                SSQL = SSQL + "  Select 3 as btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, 0 as IDKegiatan,0 as IIDRekening,0 as Root, A.sNamaProgram as sNamaRekening,    " +
                    "  SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan, mPelaksanaURusan.IsPokok,0 as K FROM tPrograms_A A   " +
                    " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan    " +
                    " AND A.IDProgram = B.IDProgram   " +
                    " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
                    " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND   B.btJEnis= 3 and b.Root = 3 AND (b.Jumlah>0 or B.JumlahMurni>0)   ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();
                }

                SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram, A.sNamaProgram,mPelaksanaURusan.IsPokok";
                //if (_p.LastLevel == 5)
                //{

                SSQL = SSQL + "  UNION ALL    " +
            " Select 3.btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, A.IDkegiatan  as IDKegiatan,0 as IIDRekening,0 as Root, A.sNama as sNamaRekening,   " +
            " SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan,mPelaksanaUrusan.IsPokok,1 as K FROM tKegiatan_A A   " +
            " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan   AND A.IDProgram = B.IDProgram and A.IDkegiatan=B.IDKegiatan  " +
        " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
        " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan    " +
        " WHERE B.iTAhun = " + _p.Tahun.ToString() + " AND B.btJEnis= 3 and B.Root=3 AND (B.Jumlah>0 or B.JumlahMurni>0)";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();
                    SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok  ";

                }
                else
                {
                    SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok  ";
                }

                // }

                if (_p.LastLevel == 3)
                {
                    SSQL = SSQL + " UNION ALL  " +
                        " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni , 0 as iNo,'' as Keterangan ,mPelaksanaUrusan.IsPokok,0 as K  from " + namaView + "  " +
                        " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "   AND  " + namaView + ".btJEnis in ( 3) and Root in( 3) and (Jumlah  > 0  or JumlahMurni>0)";
                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                    }

                }
                else
                {

                    SSQL = SSQL + "  UNION ALL  " +
                    " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni , 0 as iNo,'' as Keterangan ,mPelaksanaUrusan.IsPokok ,0 as K from " + namaView + "   " +
                    " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis in (3) and Root = 5 and (Jumlah  > 0  or JumlahMurni>0)  ";
                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                    }

                }
                SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan," + namaView + ".IDDInas, IDProgram,IDkegiatan, " + namaView + ".IIDRekening,Root,iNo  ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIII()
                                {
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    Tahun = _p.Tahun,
                                    Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                                           DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                            DataFormat.GetInteger(dr["IDDInas"]),
                                                            DataFormat.GetInteger(dr["IDProgram"]),
                                                            DataFormat.GetInteger(dr["IDkegiatan"]),
                                                            DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                                    Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).Trim() : DataFormat.GetString(dr["sNamaRekening"]).Trim()) : "",
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport(),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"].ToString()).ToRupiahInReport(),
                                    Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                    Prosentase = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]), DataFormat.GetDecimal(dr["JumlahMurni"])),
                                    Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                    Level = DataFormat.GetSingle(dr["Root"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    K = DataFormat.GetInteger(dr["K"])


                                }).ToList();
                        //}                        
                    }
                }

                if (_p.IDDinas > 0)
                {


                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,6 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                        " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }
                else
                {

                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,6 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                    " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }

                DataTable dtx = new DataTable();
                PerdaIII o = new PerdaIII();

                dtx = _dbHelper.ExecuteDataTable(SSQL);
                if (dtx != null)
                {
                    if (dtx.Rows.Count > 0)
                    {

                        DataRow dr = dtx.Rows[0];

                        o = new PerdaIII()
                        {
                            Tahun = _p.Tahun,
                            Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                                   DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                    DataFormat.GetInteger(dr["IDDInas"]),
                                                    DataFormat.GetInteger(dr["IDProgram"]),
                                                    DataFormat.GetInteger(dr["IDkegiatan"]),
                                                    DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                            Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                            Jumlah = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])).ToRupiahInReport(),
                            JumlahMurni = (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])).ToRupiahInReport(),
                            Selisih = ((DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])) - (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"]))).ToRupiahInReport(),
                            Prosentase = DataFormat.GetProsentase((DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])), (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"]))), //--DataFormat.GetProsentase(DataFormat.GetDecimal(dr["JumlahMurni"]), DataFormat.GetDecimal(dr["Jumlah"])),
                            Keterangan = DataFormat.GetString(dr["Keterangan"]),
                            Level = DataFormat.GetSingle(dr["Root"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"])

                        };

                    }
                }
                _lst.Add(o);
                //******
                DasarHukumLogic oLogicDasarHukum = new DasarHukumLogic(_p.Tahun);
                List<DasarHukum> _lstDasarHukum = new List<DasarHukum>();
                Single snglOnPerda = 1;

                _lstDasarHukum = oLogicDasarHukum.Get(_p.Tahun, snglOnPerda);



                int JumlahPerdaIII = _lst.Count;

                for (int idx = 0; idx < JumlahPerdaIII; idx++)
                {
                    PerdaIII p3 = _lst[idx];
                    if (idx < _lstDasarHukum.Count)
                    {
                        _lst[idx].label = _lstDasarHukum[idx].NoUrut.ToString();
                        _lst[idx].Keterangan = _lstDasarHukum[idx].Keterangan;
                    }

                }

                // Pembiayaan 
                //if (_p.IDDinas == 4040601){
                List<PerdaIII> lstpby = new List<PerdaIII>();
                lstpby = GetPerdaIIIPembiayaan(_p);
                if (lstpby.Count > 1)
                {
                    foreach (PerdaIII pby in lstpby)
                    {
                        _lst.Add(pby);
                    }
                }
                //}
                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        public List<PERDAVII90> GetPerdaVII90(ParameterLaporan _p, RemoteConnection sCOn = null)
        {

            // BersihkanNonKegiatan();
            string namaView = CreateViewAllLevel(_p, true);



            RPJMDProgramLogic pLogic = new RPJMDProgramLogic(2021, 3);
            RenstraKegiatanLogic oKAPBDLogic = new RenstraKegiatanLogic(2021, 3);

            List<RPJMDProgram> _lstPRG = new List<RPJMDProgram>();
            SSQL = "SELECT MURUSAN.BTKODEKATEGORI, 0 as IDURusan, 0 as id,MKATEGORI.SNAMAKATEGORI as Nama,sum (R.targetRP5) " +
               " as jumlah from MKATEGORI INNER JOIN MURUSAN ON MURUSAN.BTKODEKATEGORI = MKATEGORI.BTKODEKATEGORI " +
                " inner join RPJmdProgram50 R on murusan.id = r.idurusan" +
                " WHERE MURUSAN.profile= 3 AND mKategorI.PROFILE= 2" +
               " group by MURUSAN.BTKODEKATEGORI,MKATEGORI.SNAMAKATEGORI" +
                "  UNION ALL " +
               " SELECT MURUSAN.BTKODEKATEGORI, murusan.id as IDURusan, 0 as id, snamaurusan as Nama,sum (R.targetRP5) " +
               " as jumlah from murusan inner join RPJmdProgram50 R on murusan.id = r.idurusan  where murusan.profile= 3 " +
               " group by MURUSAN.BTKODEKATEGORI,murusan.id, murusan.snamaurusan" +
               " UNION ALL " +
               " SELECT IDURUSAN /100 AS BTKODEEKATEGORI,iDURusan, id, Nama,sum (TargetRp5) as jumlah from RPJMDProgram50 " +
                "  group by iDURusan, id,nama";
            SSQL = SSQL + " ORDER BY BTKodekategori, idurusan,id";







            List<PERDAVII90> _lst = new List<PERDAVII90>();
            try
            {
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, sCOn.GetConnection());

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        _lst = (from DataRow dr in dt.Rows
                                select new PERDAVII90()
                                {
                                    Kode = getkodeVII90(
                                        DataFormat.GetInteger(dr["btkodekategori"]),
                                        DataFormat.GetInteger(dr["iDurusan"]),
                                        DataFormat.GetInteger(dr["id"])),
                                    Uraian = DataFormat.GetString(dr["Nama"]),
                                    RPJMD = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
                                    kategori = DataFormat.GetInteger(dr["btkodekategori"]),
                                    urusan = DataFormat.GetInteger(dr["iDurusan"]),
                                    program = DataFormat.GetInteger(dr["id"])

                                }).ToList();
                        //}                        
                    }
                }

                foreach (PERDAVII90 p7 in _lst)
                {
                    _lst[_lst.IndexOf(p7)].RAPBD = (getRapbd(p7.kategori, p7.urusan, p7.program, 0, 0)).ToRupiahInReport();

                }
                return _lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }// V I I   90 ****************************
        private string getkodeVII90(int kk, int idurusan, int idprogram)
        {
            string ret = "";
            ret = kk.ToString();
            if (idurusan > 0)
                ret = ret + "." + idurusan.ToString().Substring(1);

            if (idprogram > 0)
                ret = ret + "." + idprogram.ToString().Substring(3);
            return ret;

        }
        private decimal getRapbd(int kategori, int idurusan, int idprogram, int idkegiatan, long idsubkegiatan)
        {
            SSQL = "select sum(cPlafon) as j from tanggaranrekening_a where itahun = 2021 ";
            if (kategori > 0)
                SSQL = SSQL + " AND IDURUSAN/100=" + kategori.ToString();
            if (idurusan > 0)
                SSQL = SSQL + " AND IDURUSAN=" + idurusan.ToString();
            if (idprogram > 0)
                SSQL = SSQL + " AND idprogram=" + idprogram.ToString();
            if (idkegiatan > 0)
                SSQL = SSQL + " AND IDKEGIATAN=" + idkegiatan.ToString();
            if (idsubkegiatan > 0)
                SSQL = SSQL + " AND IDSubkegiatan=" + idsubkegiatan.ToString();
            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL);
            decimal d = 0l;
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    d = DataFormat.GetDecimal(dr["j"]);

                }
            }
            return d;


        }
        public List<PERDAVIII90> GetPerdaVIII90(ParameterLaporan _p, RemoteConnection sCOn = null)
        {

            // BersihkanNonKegiatan();
            string namaView = CreateViewAllLevel(_p, true);



            RenstraKegiatanLogic pLogic = new RenstraKegiatanLogic(2021, 3);
            RenstraKegiatanLogic oKAPBDLogic = new RenstraKegiatanLogic(2021, 3);

            List<RenstraKegiatan> _lstPRG = new List<RenstraKegiatan>();

            //    SSQL =" SELECT ID, snamaskpd, SUM(Target5RP) as rkpd from mskpd Inner join renstrakegiatan50 on mskpd.id= renstrakegiatan50.skpd "+
            SSQL = "SELECT MURUSAN.BTKODEKATEGORI, 0 as IDURusan, 0 as id,MKATEGORI.SNAMAKATEGORI as Nama,sum (R.targetRP5) " +
               " as jumlah from MKATEGORI INNER JOIN MURUSAN ON MURUSAN.BTKODEKATEGORI = MKATEGORI.BTKODEKATEGORI " +
                " inner join RPJmdProgram50 R on murusan.id = r.idurusan" +
                " WHERE MURUSAN.profile= 3 AND mKategorI.PROFILE= 2" +
               " group by MURUSAN.BTKODEKATEGORI,MKATEGORI.SNAMAKATEGORI" +
                "  UNION ALL " +
               " SELECT MURUSAN.BTKODEKATEGORI, murusan.id as IDURusan, 0 as id, snamaurusan as Nama,sum (R.targetRP5) " +
               " as jumlah from murusan inner join RPJmdProgram50 R on murusan.id = r.idurusan  where murusan.profile= 3 " +
               " group by MURUSAN.BTKODEKATEGORI,murusan.id, murusan.snamaurusan" +
               " UNION ALL " +
               " SELECT IDURUSAN /100 AS BTKODEEKATEGORI,iDURusan, id, Nama,sum (TargetRp5) as jumlah from RPJMDProgram50 " +
                "  group by iDURusan, id,nama";
            SSQL = SSQL + " ORDER BY BTKodekategori, idurusan,id";







            List<PERDAVIII90> _lst = new List<PERDAVIII90>();
            try
            {
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, sCOn.GetConnection());

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        _lst = (from DataRow dr in dt.Rows
                                select new PERDAVIII90()
                                {
                                    //Kode = getkodeVII90(
                                    //    DataFormat.GetInteger(dr["btkodekategori"]),
                                    //    DataFormat.GetInteger(dr["iDurusan"]),
                                    //    DataFormat.GetInteger(dr["id"])),
                                    //Uraian = DataFormat.GetString(dr["Nama"]),
                                    //RPJMD = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
                                    //kategori = DataFormat.GetInteger(dr["btkodekategori"]),
                                    //urusan = DataFormat.GetInteger(dr["iDurusan"]),
                                    //program = DataFormat.GetInteger(dr["id"])

                                }).ToList();
                        //}                        
                    }
                }

                foreach (PERDAVIII90 p7 in _lst)
                {
                    _lst[_lst.IndexOf(p7)].RAPBD = (getRapbd(p7.idkategori, p7.idurusan, p7.idprogram, p7.idkegiatan, p7.idsubkegiatan)).ToRupiahInReport();

                }
                return _lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }// V I I   90 ****************************
        private string getkodeVIII90(int kk, int idurusan, int idprogram)
        {
            string ret = "";
            ret = kk.ToString();
            if (idurusan > 0)
                ret = ret + "." + idurusan.ToString().Substring(1);

            if (idprogram > 0)
                ret = ret + "." + idprogram.ToString().Substring(3);
            return ret;

        }
        private decimal getRapbdperdaVIII(int kategori, int idurusan, int idprogram, int idkegiatan, long idsubkegiatan)
        {
            SSQL = "select sum(cPlafon) as j from tanggaranrekening_a where itahun = 2021 ";
            if (kategori > 0)
                SSQL = SSQL + " AND IDURUSAN/100=" + kategori.ToString();
            if (idurusan > 0)
                SSQL = SSQL + " AND IDURUSAN=" + idurusan.ToString();
            if (idprogram > 0)
                SSQL = SSQL + " AND idprogram=" + idprogram.ToString();
            if (idkegiatan > 0)
                SSQL = SSQL + " AND IDKEGIATAN=" + idkegiatan.ToString();
            if (idsubkegiatan > 0)
                SSQL = SSQL + " AND IDSubkegiatan=" + idsubkegiatan.ToString();
            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL);
            decimal d = 0l;
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    d = DataFormat.GetDecimal(dr["j"]);

                }
            }
            return d;


        }
        public List<PerdaIII> GetPerdaIII90(ParameterLaporan _p)
        {  // vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv

            BersihkanNonKegiatan();
            string namaView = CreateViewAllLevel(_p, true);
            namaView = " dbo.fn_AnggaranAndRealisasiAllLevel (2022 ,'12/31/2022')";


            List<PerdaIII> _lst = new List<PerdaIII>();
            try
            {
                //if (mprofile == 3)
                //    _p.LastLevel = 6;

                SSQL = "Select A.btJenis,A.IDUrusan,A.IDDInas, 0 AS IDProgram , 0 as IDkegiatan,0 as IDSUbKegiatan, A.IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok ,0 as K ,''  as hasil  from " + namaView + " A " +
                    " Left Join mDasarHukum on A.IIDREKENING = mDasarHukum.IIDRekening  WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND  A.btJEnis= 1 and A.Root <=  5 and A.Jumlah>0   ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();
                }
                if (_p.LastLevel > 3)
                {
                    SSQL = SSQL + " UNION Select A.btJenis,A.IDUrusan,A.IDDInas, 0 AS IDProgram , 0 as IDkegiatan,0 as IDSUbKegiatan, A.IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo,mDasarHukum.sKeterangan as Keterangan , 0 as isPokok ,0 as K ,''  as hasil  from " + namaView + " A " +
                        " Left Join mDasarHukum on A.IIDREKENING = mDasarHukum.IIDRekening  WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND  A.btJEnis= 1 and A.Root =  6 and A.Jumlah>0   ";

                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();
                    }
                }



                SSQL = SSQL + " UNION ALL    ";
                // Jumlah Belanja

                SSQL = SSQL + "  Select 2 as btJenis,0 as IDUrusan ,A.IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 0 as IDSUbKegiatan,A.IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, 0 as iNo, '' as Keterangan , 0 as isPokok ,0 as K,''  as hasil  from " + namaView + " A " +
                    " WHERE A.iTAhun = " + _p.Tahun.ToString() + "  and A.btJenis in (2,3) and A.Root =1  ";//and A.Jumlah>0    ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();




                }

                SSQL = SSQL + " GROUP BY  A.IDDInas,A.IIDREKENING,ROOT,sNamaRekening ";

                SSQL = SSQL + " UNION ALL    ";


                SSQL = SSQL + " Select B.btJenis,A.ID as IDUrusan, B.IDDInas ,0 as IDProgram, 0 as IDKegiatan,0 as IDSUbKegiatan,0 as IIDRekening,-11 as Root, A.sNamaUrusan as sNamaRekening,   " +
" SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan, mPelaksanaURusan.IsPokok,0 as K,''  as hasil FROM mUrusan A    " +
"  INNER JOIN " + namaView + "  B ON A.ID = B.IDUrusan  " +
"  INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan  " +
" and A.ID = mPelaksanaUrusan.IDUrusan  WHERE B.iTAhun = " + _p.Tahun.ToString() + "  and B.Root = 3 AND    B.btJEnis in(2,3) and (B.Jumlah>0 or B.JumlahMurni>0) ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND B.IDDinas=" + _p.IDDinas.ToString();


                }

                SSQL = SSQL + " GROUP BY B.btJenis,A.ID, B.IDDInas,  A.sNamaUrusan ,mPelaksanaURusan.IsPokok  ";





                SSQL = SSQL + " UNION ALL  Select B.btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, 0 as IDKegiatan,0 as IDSUbKegiatan,0 as IIDRekening,-1 as Root, A.sNamaProgram as sNamaRekening,    " +
                                    "  SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan, mPelaksanaURusan.IsPokok,0 as K,''  as hasil FROM tPrograms_A A   " +
                                    " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan    " +
                                    " AND A.IDProgram = B.IDProgram   " +
                                    " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
                                    " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND   B.btJEnis in(2,3) and b.Root = 3 AND (b.Jumlah>0 or B.JumlahMurni>0)   ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();


                }

                SSQL = SSQL + " GROUP BY B.btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, A.sNamaProgram,mPelaksanaURusan.IsPokok";
                //if (_p.LastLevel == 5)
                //{

                SSQL = SSQL + "  UNION ALL    " +
            " Select B.btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, A.IDkegiatan  as IDKegiatan,0 as IDSUbKegiatan, 0 as IIDRekening,0 as Root, A.sNama as sNamaRekening,   " +
            " SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan,mPelaksanaUrusan.IsPokok,1 as K, a.outcome   as hasil FROM tKegiatan_A A   " +
            " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan   AND A.IDProgram = B.IDProgram and A.IDkegiatan=B.IDKegiatan  " +
        " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
        " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan    " +
        " WHERE B.iTAhun = " + _p.Tahun.ToString() + " AND B.btJEnis in(3) and B.Root=3 AND (B.Jumlah>0 or B.JumlahMurni>0)";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();


                    SSQL = SSQL + " GROUP BY B.btJenis, A.IDUrusan, A.IDDInas,A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok,a.outcome  ";

                }
                else
                {
                    SSQL = SSQL + " GROUP BY B.btJenis, A.IDUrusan, A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok,a.outcome  ";
                }

                // SUB Kegiatan 
                SSQL = SSQL + "  UNION ALL    " +
            " Select B.btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, A.IDkegiatan  as IDKegiatan,A.IDSUbKegiatan, 0 as IIDRekening,1 as Root, A.Nama as sNamaRekening,   " +
            " SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan,mPelaksanaUrusan.IsPokok,1 as K ,  a.keluaran   as hasil FROM TSubKegiatan A   " +
            " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan   AND A.IDProgram = B.IDProgram and A.IDkegiatan=B.IDKegiatan AND A.IDSubkegiatan=B.IDSubkegiatan " +
        " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
        " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan    " +
        " WHERE B.iTAhun = " + _p.Tahun.ToString() + " AND B.btJEnis in(3) and B.Root=3 AND (B.Jumlah>0 or B.JumlahMurni>0)";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();



                    SSQL = SSQL + " GROUP BY B.btJenis, A.IDUrusan, A.IDDInas,A.IDProgram, A.Nama,A.IDkegiatan,A.IDSUbKegiatan,mPelaksanaURusan.IsPokok ,  a.keluaran  ";

                }
                else
                {
                    SSQL = SSQL + " GROUP BY B.btJenis,A.IDUrusan, A.IDProgram, A.Nama,A.IDkegiatan,A.IDSUbKegiatan,mPelaksanaURusan.IsPokok,  a.keluaran   ";
                }

                // 

                if (_p.LastLevel == 3)
                {
                    SSQL = SSQL + " UNION ALL  " +
                        " Select A.btJenis,A.IDUrusan,A.IDDInas,  IDProgram , IDkegiatan, A.IDSUbKegiatan,A.IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni , 0 as iNo,'' as Keterangan ,mPelaksanaUrusan.IsPokok,0 as K, '' as hasil from " + namaView + "  " +
                        " INNER JOIN mPElaksanaurusan on A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE A.iTAhun = " + _p.Tahun.ToString() + "   AND  A.btJEnis in (2,3) and Root in( 2,3) and (Jumlah  > 0  or JumlahMurni>0)";
                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();


                    }

                }
                else
                {

                    SSQL = SSQL + "  UNION ALL  " +
                    " Select A.btJenis,A.IDUrusan,A.IDDInas,  IDProgram , IDkegiatan, A.IDSUbKegiatan, A.IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni , 0 as iNo,'' as Keterangan ,mPelaksanaUrusan.IsPokok ,0 as K, '' as hasil from " + namaView + " A   " +
                    " INNER JOIN mPElaksanaurusan on A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE A.iTAhun = " + _p.Tahun.ToString() + "  AND  A.btJEnis in (3) and Root in (2,3,4, 5,6) and (Jumlah  > 0  or JumlahMurni>0)  ";
                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();
                    }

                }
                SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan,A.IDDInas, IDProgram,IDkegiatan, IDSUbKegiatan, A.IIDRekening,Root,iNo  ";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIII()
                                {
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    Tahun = _p.Tahun,
                                    Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                                           DataFormat.GetKode90(DataFormat.GetInteger(dr["IDUrusan"]),
                                                            DataFormat.GetInteger(dr["IDDInas"]),
                                                            DataFormat.GetInteger(dr["IDProgram"]),
                                                            DataFormat.GetInteger(dr["IDkegiatan"]),
                                                            DataFormat.GetLong(dr["IDSubkegiatan"]),
                                                            DataFormat.GetLong(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                                    Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).Trim() : DataFormat.GetString(dr["sNamaRekening"]).Trim()) : "",
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport(),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"].ToString()).ToRupiahInReport(),
                                    Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                    Prosentase = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]), DataFormat.GetDecimal(dr["JumlahMurni"])),
                                    Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                    Level = DataFormat.GetSingle(dr["Root"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    K = DataFormat.GetInteger(dr["K"]),
                                    Hasil = DataFormat.GetString(dr["Hasil"]),


                                }).ToList();
                        //}                        
                    }
                }

                if (_p.IDDinas > 0)
                {


                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,3 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                        " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }
                else
                {

                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,3 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                    " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }

                DataTable dtx = new DataTable();
                PerdaIII o = new PerdaIII();

                dtx = _dbHelper.ExecuteDataTable(SSQL);
                if (dtx != null)
                {
                    if (dtx.Rows.Count > 0)
                    {

                        DataRow dr = dtx.Rows[0];

                        o = new PerdaIII()
                        {
                            Tahun = _p.Tahun,
                            Kode = "", //DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                            //DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                            //               DataFormat.GetInteger(dr["IDDInas"]),
                            //              DataFormat.GetInteger(dr["IDProgram"]),
                            //              DataFormat.GetInteger(dr["IDkegiatan"]),
                            //              DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                            Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                            Jumlah = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])).ToRupiahInReport(),
                            JumlahMurni = (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])).ToRupiahInReport(),
                            Selisih = ((DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])) - (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"]))).ToRupiahInReport(),
                            Prosentase = DataFormat.GetProsentase((DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])), (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"]))), //--DataFormat.GetProsentase(DataFormat.GetDecimal(dr["JumlahMurni"]), DataFormat.GetDecimal(dr["Jumlah"])),
                            Keterangan = DataFormat.GetString(dr["Keterangan"]),
                            Level = DataFormat.GetSingle(dr["Root"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"])

                        };

                    }
                }
                _lst.Add(o);
                //******
                DasarHukumLogic oLogicDasarHukum = new DasarHukumLogic(_p.Tahun);
                List<DasarHukum> _lstDasarHukum = new List<DasarHukum>();
                Single snglOnPerda = 1;
                if (_p.LastLevel <= 3)
                {


                    _lstDasarHukum = oLogicDasarHukum.Get(_p.Tahun, snglOnPerda);



                    int JumlahPerdaIII = _lst.Count;
                    int no = 1;
                    for (int idx = 0; idx < JumlahPerdaIII; idx++)
                    {
                        PerdaIII p3 = _lst[idx];
                        if (idx < _lstDasarHukum.Count)
                        {
                            _lst[idx].label = "-"; // (no).ToString();// _lstDasarHukum[idx].NoUrut.ToString();
                            _lst[idx].Keterangan = _lstDasarHukum[idx].Keterangan;
                            no = no + 1;

                        }

                    }
                }
                // Pembiayaan 
                //if (_p.IDDinas == 4040601){
                List<PerdaIII> lstpby = new List<PerdaIII>();
                lstpby = GetPerdaIIIPembiayaan(_p);
                if (lstpby.Count > 1)
                {
                    foreach (PerdaIII pby in lstpby)
                    {
                        _lst.Add(pby);
                    }
                }
                //}
                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }

        public List<PerdaIII> GetPerdaIII902021(ParameterLaporan _p)
        {  // vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv

            BersihkanNonKegiatan();
            string namaView = CreateViewAllLevel(_p, true);
            namaView = " dbo.fn_AnggaranAndRealisasiAllLevel (2022 ,'12/31/2022')";


            List<PerdaIII> _lst = new List<PerdaIII>();
            try
            {
                //if (mprofile == 3)
                //    _p.LastLevel = 6;

                SSQL = "Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan,0 as IDSUbKegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok ,0 as K ,''  as hasil  from " + namaView + " " +
                    " Left Join mDasarHukum on " + namaView + ".IIDREKENING = mDasarHukum.IIDRekening  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root <=  5 and " + namaView + ".Jumlah>0   ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                if (_p.LastLevel > 3)
                {
                    SSQL = SSQL + " UNION Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan,0 as IDSUbKegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo,mDasarHukum.sKeterangan as Keterangan , 0 as isPokok ,0 as K ,''  as hasil  from " + namaView + " " +
                        " Left Join mDasarHukum on " + namaView + ".IIDREKENING = mDasarHukum.IIDRekening  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root =  6 and " + namaView + ".Jumlah>0   ";

                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                    }
                }


                //SSQL = SSQL + " UNION ALL    ";

                //SSQL = SSQL + "  Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 0 as IDSUbKegiatan," + namaView +
                //    ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo,''  as Keterangan, 0 as isPokok ,0 as K ,''  as hasil  from " + namaView +
                //    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root = 3 and " + namaView + ".Jumlah > 0   ";//and  " +
                ////namaView + ".IDDInas =" + _p.IDDinas.ToString();




                //if (_p.IDDinas > 0)
                //{
                //    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                //}
                //  SSQL = SSQL + "and (( mDasarHukum.IIDRekening in (Select IIDRekening from tAnggaranRekening_A where IDDInas=" + _p.IDDinas.ToString() + " )) ";
                //  SSQL = SSQL + "OR ( mDasarHukum.IIDRekening in (Select IIDRekening/100 * 100 from tAnggaranRekening_A where IDDInas=" + _p.IDDinas.ToString() + " )) ) ";

                //SSQL = SSQL + " Union ALL Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 0 as IDSUbKegiatan," + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah," +
                //            "JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok,0 as K from " + namaView + " WHERE " + namaView + ".iTAhun= " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 " +
                //                " and " + namaView + ".Root = 3  AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + " ";// and " + namaView + ".IIDRekening/10000 not in (Select IIDRekening/10000 from mDasarHukum) ";

                SSQL = SSQL + " UNION ALL    ";
                // Jumlah Belanja

                SSQL = SSQL + "  Select 2 as btJenis,0 as IDUrusan ," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 0 as IDSUbKegiatan," + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, 0 as iNo, '' as Keterangan , 0 as isPokok ,0 as K,''  as hasil  from " + namaView + " " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  and " + namaView + ".btJenis in (2,3) and " + namaView + ".Root =1  ";//and " + namaView + ".Jumlah>0    ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();




                }

                SSQL = SSQL + " GROUP BY  " + namaView + ".IDDInas," + namaView + ".IIDREKENING,ROOT,sNamaRekening ";

                SSQL = SSQL + " UNION ALL    ";


                //SSQL = SSQL + "  Select 3 as btJenis,0 as IDUrusan ," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 0 as IDSUbKegiatan," + 
                //namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, 0 as iNo, '' as Keterangan , 0 as isPokok ,0 as K ,''  as hasil  from " + namaView + " " +
                //    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  and " + namaView + ".btJenis in (3) and " + namaView + ".Root =2  ";//and " + namaView + ".Jumlah>0    ";

                //if (_p.IDDinas > 0)
                //{
                //    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                //}

                //SSQL = SSQL + " GROUP BY  " + namaView + ".IDDInas," + namaView + ".IIDREKENING,ROOT,sNamaRekening ";

                //SSQL = SSQL + " UNION ALL    ";
                SSQL = SSQL + " Select B.btJenis,A.ID as IDUrusan, B.IDDInas ,0 as IDProgram, 0 as IDKegiatan,0 as IDSUbKegiatan,0 as IIDRekening,-11 as Root, A.sNamaUrusan as sNamaRekening,   " +
" SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan, mPelaksanaURusan.IsPokok,0 as K,''  as hasil FROM mUrusan A    " +
"  INNER JOIN " + namaView + "  B ON A.ID = B.IDUrusan  " +
"  INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan  " +
" and A.ID = mPelaksanaUrusan.IDUrusan  WHERE B.iTAhun = " + _p.Tahun.ToString() + "  and B.Root = 3 AND    B.btJEnis in(2,3) and (B.Jumlah>0 or B.JumlahMurni>0) ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND B.IDDinas=" + _p.IDDinas.ToString();


                }

                SSQL = SSQL + " GROUP BY B.btJenis,A.ID, B.IDDInas,  A.sNamaUrusan ,mPelaksanaURusan.IsPokok  ";





                SSQL = SSQL + " UNION ALL  Select B.btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, 0 as IDKegiatan,0 as IDSUbKegiatan,0 as IIDRekening,-1 as Root, A.sNamaProgram as sNamaRekening,    " +
                                    "  SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan, mPelaksanaURusan.IsPokok,0 as K,''  as hasil FROM tPrograms_A A   " +
                                    " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan    " +
                                    " AND A.IDProgram = B.IDProgram   " +
                                    " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
                                    " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND   B.btJEnis in(2,3) and b.Root = 3 AND (b.Jumlah>0 or B.JumlahMurni>0)   ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();


                }

                SSQL = SSQL + " GROUP BY B.btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, A.sNamaProgram,mPelaksanaURusan.IsPokok";
                //if (_p.LastLevel == 5)
                //{

                SSQL = SSQL + "  UNION ALL    " +
            " Select B.btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, A.IDkegiatan  as IDKegiatan,0 as IDSUbKegiatan, 0 as IIDRekening,0 as Root, A.sNama as sNamaRekening,   " +
            " SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan,mPelaksanaUrusan.IsPokok,1 as K, a.outcome   as hasil FROM tKegiatan_A A   " +
            " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan   AND A.IDProgram = B.IDProgram and A.IDkegiatan=B.IDKegiatan  " +
        " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
        " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan    " +
        " WHERE B.iTAhun = " + _p.Tahun.ToString() + " AND B.btJEnis in(3) and B.Root=3 AND (B.Jumlah>0 or B.JumlahMurni>0)";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();


                    SSQL = SSQL + " GROUP BY B.btJenis, A.IDUrusan, A.IDDInas,A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok,a.outcome  ";

                }
                else
                {
                    SSQL = SSQL + " GROUP BY B.btJenis, A.IDUrusan, A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok,a.outcome  ";
                }

                // SUB Kegiatan 
                SSQL = SSQL + "  UNION ALL    " +
            " Select B.btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, A.IDkegiatan  as IDKegiatan,A.IDSUbKegiatan, 0 as IIDRekening,1 as Root, A.Nama as sNamaRekening,   " +
            " SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan,mPelaksanaUrusan.IsPokok,1 as K ,  a.keluaran   as hasil FROM TSubKegiatan A   " +
            " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan   AND A.IDProgram = B.IDProgram and A.IDkegiatan=B.IDKegiatan AND A.IDSubkegiatan=B.IDSubkegiatan " +
        " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
        " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan    " +
        " WHERE B.iTAhun = " + _p.Tahun.ToString() + " AND B.btJEnis in(3) and B.Root=3 AND (B.Jumlah>0 or B.JumlahMurni>0)";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();



                    SSQL = SSQL + " GROUP BY B.btJenis, A.IDUrusan, A.IDDInas,A.IDProgram, A.Nama,A.IDkegiatan,A.IDSUbKegiatan,mPelaksanaURusan.IsPokok ,  a.keluaran  ";

                }
                else
                {
                    SSQL = SSQL + " GROUP BY B.btJenis,A.IDUrusan, A.IDProgram, A.Nama,A.IDkegiatan,A.IDSUbKegiatan,mPelaksanaURusan.IsPokok,  a.keluaran   ";
                }

                // 

                if (_p.LastLevel == 3)
                {
                    SSQL = SSQL + " UNION ALL  " +
                        " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas,  IDProgram , IDkegiatan, " + namaView + ".IDSUbKegiatan," + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni , 0 as iNo,'' as Keterangan ,mPelaksanaUrusan.IsPokok,0 as K, '' as hasil from " + namaView + "  " +
                        " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "   AND  " + namaView + ".btJEnis in (2,3) and Root in( 2,3) and (Jumlah  > 0  or JumlahMurni>0)";
                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();


                    }

                }
                else
                {

                    SSQL = SSQL + "  UNION ALL  " +
                    " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas,  IDProgram , IDkegiatan, " + namaView + ".IDSUbKegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni , 0 as iNo,'' as Keterangan ,mPelaksanaUrusan.IsPokok ,0 as K, '' as hasil from " + namaView + "   " +
                    " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis in (3) and Root in (2,3,4, 5,6) and (Jumlah  > 0  or JumlahMurni>0)  ";
                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                    }

                }
                SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan," + namaView + ".IDDInas, IDProgram,IDkegiatan, IDSUbKegiatan, " + namaView + ".IIDRekening,Root,iNo  ";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIII()
                                {
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    Tahun = _p.Tahun,
                                    Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                                           DataFormat.GetKode90(DataFormat.GetInteger(dr["IDUrusan"]),
                                                            DataFormat.GetInteger(dr["IDDInas"]),
                                                            DataFormat.GetInteger(dr["IDProgram"]),
                                                            DataFormat.GetInteger(dr["IDkegiatan"]),
                                                            DataFormat.GetLong(dr["IDSubkegiatan"]),
                                                            DataFormat.GetLong(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                                    Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).Trim() : DataFormat.GetString(dr["sNamaRekening"]).Trim()) : "",
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport(),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"].ToString()).ToRupiahInReport(),
                                    Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                    Prosentase = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]), DataFormat.GetDecimal(dr["JumlahMurni"])),
                                    Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                    Level = DataFormat.GetSingle(dr["Root"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    K = DataFormat.GetInteger(dr["K"]),
                                    Hasil = DataFormat.GetString(dr["Hasil"]),


                                }).ToList();
                        //}                        
                    }
                }

                if (_p.IDDinas > 0)
                {


                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,3 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                        " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }
                else
                {

                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,3 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                    " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }

                DataTable dtx = new DataTable();
                PerdaIII o = new PerdaIII();

                dtx = _dbHelper.ExecuteDataTable(SSQL);
                if (dtx != null)
                {
                    if (dtx.Rows.Count > 0)
                    {

                        DataRow dr = dtx.Rows[0];

                        o = new PerdaIII()
                        {
                            Tahun = _p.Tahun,
                            Kode = "", //DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                            //DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                            //               DataFormat.GetInteger(dr["IDDInas"]),
                            //              DataFormat.GetInteger(dr["IDProgram"]),
                            //              DataFormat.GetInteger(dr["IDkegiatan"]),
                            //              DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                            Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                            Jumlah = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])).ToRupiahInReport(),
                            JumlahMurni = (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])).ToRupiahInReport(),
                            Selisih = ((DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])) - (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"]))).ToRupiahInReport(),
                            Prosentase = DataFormat.GetProsentase((DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])), (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"]))), //--DataFormat.GetProsentase(DataFormat.GetDecimal(dr["JumlahMurni"]), DataFormat.GetDecimal(dr["Jumlah"])),
                            Keterangan = DataFormat.GetString(dr["Keterangan"]),
                            Level = DataFormat.GetSingle(dr["Root"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"])

                        };

                    }
                }
                _lst.Add(o);
                //******
                DasarHukumLogic oLogicDasarHukum = new DasarHukumLogic(_p.Tahun);
                List<DasarHukum> _lstDasarHukum = new List<DasarHukum>();
                Single snglOnPerda = 1;
                if (_p.LastLevel <= 3)
                {


                    _lstDasarHukum = oLogicDasarHukum.Get(_p.Tahun, snglOnPerda);



                    int JumlahPerdaIII = _lst.Count;
                    int no = 1;
                    for (int idx = 0; idx < JumlahPerdaIII; idx++)
                    {
                        PerdaIII p3 = _lst[idx];
                        if (idx < _lstDasarHukum.Count)
                        {
                            _lst[idx].label = "-"; // (no).ToString();// _lstDasarHukum[idx].NoUrut.ToString();
                            _lst[idx].Keterangan = _lstDasarHukum[idx].Keterangan;
                            no = no + 1;

                        }

                    }
                }
                // Pembiayaan 
                //if (_p.IDDinas == 4040601){
                List<PerdaIII> lstpby = new List<PerdaIII>();
                lstpby = GetPerdaIIIPembiayaan(_p);
                if (lstpby.Count > 1)
                {
                    foreach (PerdaIII pby in lstpby)
                    {
                        _lst.Add(pby);
                    }
                }
                //}
                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        ////public List<PerdaIII> GetPerdaIII90(ParameterLaporan _p)
        ////{

        ////    BersihkanNonKegiatan();
        ////    string namaView = CreateViewAllLevel(_p, true);


        ////    List<PerdaIII> _lst = new List<PerdaIII>();
        ////    try
        ////    {
        ////        //if (mprofile == 3)
        ////        //    _p.LastLevel = 6;

        ////        SSQL = "Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan,0 as IDSUbKegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok ,0 as K ,''  as hasil  from " + namaView + " " +
        ////            " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root <=  " + _p.LastLevel.ToString() + " and " + namaView + ".Jumlah>0   ";

        ////        if (_p.IDDinas > 0)
        ////        {
        ////            SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
        ////        }

        ////        SSQL = SSQL + " UNION ALL    ";

        ////        SSQL = SSQL + "  Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 0 as IDSUbKegiatan," + namaView +
        ////            ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo,''  as Keterangan, 0 as isPokok ,0 as K ,''  as hasil  from " + namaView +
        ////            " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root = 3 and " + namaView + ".Jumlah > 0   ";//and  " +
        ////        //namaView + ".IDDInas =" + _p.IDDinas.ToString();




        ////        if (_p.IDDinas > 0)
        ////        {
        ////            SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
        ////        }
        ////        //  SSQL = SSQL + "and (( mDasarHukum.IIDRekening in (Select IIDRekening from tAnggaranRekening_A where IDDInas=" + _p.IDDinas.ToString() + " )) ";
        ////        //  SSQL = SSQL + "OR ( mDasarHukum.IIDRekening in (Select IIDRekening/100 * 100 from tAnggaranRekening_A where IDDInas=" + _p.IDDinas.ToString() + " )) ) ";

        ////        //SSQL = SSQL + " Union ALL Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 0 as IDSUbKegiatan," + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah," +
        ////        //            "JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok,0 as K from " + namaView + " WHERE " + namaView + ".iTAhun= " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 " +
        ////        //                " and " + namaView + ".Root = 3  AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + " ";// and " + namaView + ".IIDRekening/10000 not in (Select IIDRekening/10000 from mDasarHukum) ";

        ////        SSQL = SSQL + " UNION ALL    ";
        ////        // Jumlah Belanja

        ////        SSQL = SSQL + "  Select 2 as btJenis,0 as IDUrusan ," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 0 as IDSUbKegiatan," + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, 0 as iNo, '' as Keterangan , 0 as isPokok ,0 as K,''  as hasil  from " + namaView + " " +
        ////            " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  and " + namaView + ".btJenis in (3) and " + namaView + ".Root =1  ";//and " + namaView + ".Jumlah>0    ";

        ////        if (_p.IDDinas > 0)
        ////        {
        ////            SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
        ////        }

        ////        SSQL = SSQL + " GROUP BY  " + namaView + ".IDDInas," + namaView + ".IIDREKENING,ROOT,sNamaRekening ";

        ////        SSQL = SSQL + " UNION ALL    ";





        ////        SSQL = SSQL + "  Select 3 as btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, 0 as IDKegiatan,0 as IDSUbKegiatan,0 as IIDRekening,-1 as Root, A.sNamaProgram as sNamaRekening,    " +
        ////            "  SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan, mPelaksanaURusan.IsPokok,0 as K,''  as hasil FROM tPrograms_A A   " +
        ////            " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan    " +
        ////            " AND A.IDProgram = B.IDProgram   " +
        ////            " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
        ////            " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND   B.btJEnis= 3 and b.Root = 3 AND (b.Jumlah>0 or B.JumlahMurni>0)   ";
        ////        if (_p.IDDinas > 0)
        ////        {
        ////            SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();
        ////        }

        ////        SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram, A.sNamaProgram,mPelaksanaURusan.IsPokok";
        ////        //if (_p.LastLevel == 5)
        ////        //{

        ////        SSQL = SSQL + "  UNION ALL    " +
        ////    " Select 3.btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, A.IDkegiatan  as IDKegiatan,0 as IDSUbKegiatan, 0 as IIDRekening,0 as Root, A.sNama as sNamaRekening,   " +
        ////    " SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan,mPelaksanaUrusan.IsPokok,1 as K, a.outcome   as hasil FROM tKegiatan_A A   " +
        ////    " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan   AND A.IDProgram = B.IDProgram and A.IDkegiatan=B.IDKegiatan  " +
        ////" INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
        ////" AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan    " +
        ////" WHERE B.iTAhun = " + _p.Tahun.ToString() + " AND B.btJEnis= 3 and B.Root=3 AND (B.Jumlah>0 or B.JumlahMurni>0)";

        ////        if (_p.IDDinas > 0)
        ////        {
        ////            SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();
        ////            SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok,a.outcome  ";

        ////        }
        ////        else
        ////        {
        ////            SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok,a.outcome  ";
        ////        }

        ////        // SUB Kegiatan 
        ////        SSQL = SSQL + "  UNION ALL    " +
        ////    " Select 3.btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, A.IDkegiatan  as IDKegiatan,A.IDSUbKegiatan, 0 as IIDRekening,1 as Root, A.Nama as sNamaRekening,   " +
        ////    " SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan,mPelaksanaUrusan.IsPokok,1 as K ,  a.keluaran   as hasil FROM TSubKegiatan A   " +
        ////    " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan   AND A.IDProgram = B.IDProgram and A.IDkegiatan=B.IDKegiatan AND A.IDSubkegiatan=B.IDSubkegiatan " +
        ////" INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
        ////" AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan    " +
        ////" WHERE B.iTAhun = " + _p.Tahun.ToString() + " AND B.btJEnis= 3 and B.Root=3 AND (B.Jumlah>0 or B.JumlahMurni>0)";

        ////        if (_p.IDDinas > 0)
        ////        {
        ////            SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();
        ////            SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram, A.Nama,A.IDkegiatan,A.IDSUbKegiatan,mPelaksanaURusan.IsPokok ,  a.keluaran  ";

        ////        }
        ////        else
        ////        {
        ////            SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDProgram, A.Nama,A.IDkegiatan,A.IDSUbKegiatan,mPelaksanaURusan.IsPokok,  a.keluaran   ";
        ////        }

        ////        // 

        ////        if (_p.LastLevel == 3)
        ////        {
        ////            SSQL = SSQL + " UNION ALL  " +
        ////                " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas,  IDProgram , IDkegiatan, " + namaView + ".IDSUbKegiatan," + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni , 0 as iNo,'' as Keterangan ,mPelaksanaUrusan.IsPokok,0 as K, '' as hasil from " + namaView + "  " +
        ////                " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "   AND  " + namaView + ".btJEnis in ( 3) and Root in( 2,3) and (Jumlah  > 0  or JumlahMurni>0)";
        ////            if (_p.IDDinas > 0)
        ////            {
        ////                SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
        ////            }

        ////        }
        ////        else
        ////        {

        ////            SSQL = SSQL + "  UNION ALL  " +
        ////            " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas,  IDProgram , IDkegiatan, " + namaView + ".IDSUbKegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni , 0 as iNo,'' as Keterangan ,mPelaksanaUrusan.IsPokok ,0 as K, '' as hasil from " + namaView + "   " +
        ////            " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis in (3) and Root in (2,3,4, 5,6) and (Jumlah  > 0  or JumlahMurni>0)  ";
        ////            if (_p.IDDinas > 0)
        ////            {
        ////                SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
        ////            }

        ////        }
        ////        SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan," + namaView + ".IDDInas, IDProgram,IDkegiatan, IDSUbKegiatan, " + namaView + ".IIDRekening,Root,iNo  ";
        ////        DataTable dt = new DataTable();
        ////        dt = _dbHelper.ExecuteDataTable(SSQL);
        ////        if (dt != null)
        ////        {
        ////            if (dt.Rows.Count > 0)
        ////            {
        ////                // if (_p.Tahap == 0 || _p.Tahap == 2)
        ////                //{
        ////                _lst = (from DataRow dr in dt.Rows
        ////                        select new PerdaIII()
        ////                        {
        ////                            IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
        ////                            Tahun = _p.Tahun,
        ////                            Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
        ////                                   DataFormat.GetKode90(DataFormat.GetInteger(dr["IDUrusan"]),
        ////                                                    DataFormat.GetInteger(dr["IDDInas"]),
        ////                                                    DataFormat.GetInteger(dr["IDProgram"]),
        ////                                                    DataFormat.GetInteger(dr["IDkegiatan"]),
        ////                                                    DataFormat.GetLong(dr["IDSubkegiatan"]),
        ////                                                    DataFormat.GetLong(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
        ////                            Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).Trim() : DataFormat.GetString(dr["sNamaRekening"]).Trim()) : "",
        ////                            Jumlah = DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport(),
        ////                            JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"].ToString()).ToRupiahInReport(),
        ////                            Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
        ////                            Prosentase = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]), DataFormat.GetDecimal(dr["JumlahMurni"])),
        ////                            Keterangan = DataFormat.GetString(dr["Keterangan"]),
        ////                            Level = DataFormat.GetSingle(dr["Root"]),
        ////                            Jenis = DataFormat.GetSingle(dr["btJenis"]),
        ////                            K = DataFormat.GetInteger(dr["K"]),
        ////                            Hasil = DataFormat.GetString(dr["Hasil"]),


        ////                        }).ToList();
        ////                //}                        
        ////            }
        ////        }

        ////        if (_p.IDDinas > 0)
        ////        {


        ////            SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,6 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
        ////                " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPT , " +
        ////               "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
        ////                " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPTMURNI , " +
        ////               "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
        ////               " 3 as isPokok  ";
        ////        }
        ////        else
        ////        {

        ////            SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,6 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
        ////            " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPT , " +
        ////               "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
        ////                " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPTMURNI , " +
        ////               "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
        ////               " 3 as isPokok  ";
        ////        }

        ////        DataTable dtx = new DataTable();
        ////        PerdaIII o = new PerdaIII();

        ////        dtx = _dbHelper.ExecuteDataTable(SSQL);
        ////        if (dtx != null)
        ////        {
        ////            if (dtx.Rows.Count > 0)
        ////            {

        ////                DataRow dr = dtx.Rows[0];

        ////                o = new PerdaIII()
        ////                {
        ////                    Tahun = _p.Tahun,
        ////                    Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
        ////                           DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
        ////                                            DataFormat.GetInteger(dr["IDDInas"]),
        ////                                            DataFormat.GetInteger(dr["IDProgram"]),
        ////                                            DataFormat.GetInteger(dr["IDkegiatan"]),
        ////                                            DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
        ////                    Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
        ////                    Jumlah = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])).ToRupiahInReport(),
        ////                    JumlahMurni = (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])).ToRupiahInReport(),
        ////                    Selisih = ((DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])) - (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"]))).ToRupiahInReport(),
        ////                    Prosentase = DataFormat.GetProsentase((DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])), (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"]))), //--DataFormat.GetProsentase(DataFormat.GetDecimal(dr["JumlahMurni"]), DataFormat.GetDecimal(dr["Jumlah"])),
        ////                    Keterangan = DataFormat.GetString(dr["Keterangan"]),
        ////                    Level = DataFormat.GetSingle(dr["Root"]),
        ////                    Jenis = DataFormat.GetSingle(dr["btJenis"])

        ////                };

        ////            }
        ////        }
        ////        _lst.Add(o);
        ////        //******
        ////        DasarHukumLogic oLogicDasarHukum = new DasarHukumLogic(_p.Tahun);
        ////        List<DasarHukum> _lstDasarHukum = new List<DasarHukum>();
        ////        Single snglOnPerda = 1;

        ////        _lstDasarHukum = oLogicDasarHukum.Get(_p.Tahun, snglOnPerda);



        ////        int JumlahPerdaIII = _lst.Count;

        ////        for (int idx = 0; idx < JumlahPerdaIII; idx++)
        ////        {
        ////            PerdaIII p3 = _lst[idx];
        ////            if (idx < _lstDasarHukum.Count)
        ////            {
        ////                _lst[idx].label = _lstDasarHukum[idx].NoUrut.ToString();
        ////                _lst[idx].Keterangan = _lstDasarHukum[idx].Keterangan;
        ////            }

        ////        }

        ////        // Pembiayaan 
        ////        //if (_p.IDDinas == 4040601){
        ////        List<PerdaIII> lstpby = new List<PerdaIII>();
        ////        lstpby = GetPerdaIIIPembiayaan(_p);
        ////        if (lstpby.Count > 1)
        ////        {
        ////            foreach (PerdaIII pby in lstpby)
        ////            {
        ////                _lst.Add(pby);
        ////            }
        ////        }
        ////        //}
        ////        return _lst;

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        _isError = true;
        ////        _lastError = ex.Message;
        ////        return _lst;
        ////    }
        ////}

        public List<PerdaIII> GetPerdaIIIGabunganx(ParameterLaporan _p)
        {


            string namaView = CreateViewAllLevel(_p, true);

            BersihkanNonKegiatan();
            List<PerdaIII> _lst = new List<PerdaIII>();
            try
            {

                SSQL = "Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".Parent, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok from " + namaView + " " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root <  " + _p.LastLevel.ToString() + " and " + namaView + ".Jumlah>0   ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".Parent=" + _p.IDDinas.ToString();
                }

                SSQL = SSQL + " UNION ALL    ";

                SSQL = SSQL + "  Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".Parent, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo,''  as Keterangan, 0 as isPokok from " + namaView +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root = 3 and " + namaView + ".Jumlah > 0   ";//and  " +
                //namaView + ".IDDInas =" + _p.IDDinas.ToString();




                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".Parent=" + _p.IDDinas.ToString();
                }
                //  SSQL = SSQL + "and (( mDasarHukum.IIDRekening in (Select IIDRekening from tAnggaranRekening_A where IDDInas=" + _p.IDDinas.ToString() + " )) ";
                //  SSQL = SSQL + "OR ( mDasarHukum.IIDRekening in (Select IIDRekening/100 * 100 from tAnggaranRekening_A where IDDInas=" + _p.IDDinas.ToString() + " )) ) ";

                SSQL = SSQL + "Union ALL Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".Parent, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah," +
                            "JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok from " + namaView + " WHERE " + namaView + ".iTAhun= " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 " +
                                " and " + namaView + ".Root = 3 and " + namaView + ".Jumlah > 0   AND " + namaView + ".Parent=" + _p.IDDinas.ToString() + " ";// and " + namaView + ".IIDRekening/10000 not in (Select IIDRekening/10000 from mDasarHukum) ";

                SSQL = SSQL + " UNION ALL    ";
                SSQL = SSQL + " Select 2 as btJenis," + namaView + ".Parent/10000 as IDUrusan ," + namaView + ".Parent, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, 0 as iNo, '' as Keterangan , 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  and mDasarHukum.onPerda=1 and " + namaView + ".btJenis in (2,3) and " + namaView + ".Root =1 and " + namaView + ".Jumlah>0    ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".Parent=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY  " + namaView + ".Parent," + namaView + ".IIDREKENING,ROOT,sNamaRekening ";
                SSQL = SSQL + " UNION ALL    ";


                //++++++++++++++++++++++++++++++++++++++++++

                SSQL = SSQL + " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".Parent, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo, '' as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis= 2 and " + namaView + ".Root >2  and " + namaView + ".root<= " + _p.LastLevel + " and " + namaView + ".Jumlah>0   and " + namaView + ".Root <6   ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".Parent=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " UNION ALL ";
                SSQL = SSQL + " Select " + namaView + ".btJenis, " + namaView + ".Parent/10000 as  IDUrusan," + namaView + ".Parent, 0 AS IDProgram , 0 AS IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening , SUM(Jumlah) AS Jumlah, SUM(JumlahMurni) AS JumlahMurni ,0 as iNo, '' as Keterangan , 0 as IsPokok  from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " and mDasarHukum.onPerda=1 AND  " + namaView + ".btJEnis= 3 and Root =2  ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".Parent=" + _p.IDDinas.ToString();
                }

                SSQL = SSQL + " GROUP BY " + namaView + ".Parent," + namaView + ".IDDInas, " + namaView + ".IIDRekening,Root,sNamaRekening ";

                SSQL = SSQL + "  UNION ALL  ";

                SSQL = SSQL + "  Select 3 as btJenis,A.IDUrusan, A.Parent,A.IDProgram, 0 as IDKegiatan,0 as IIDRekening,0 as Root, A.sNamaProgram as sNamaRekening,    " +
                    "  SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan, mPelaksanaURusan.IsPokok FROM tPrograms_A A   " +
                    " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan    " +
                    " AND A.IDProgram = B.IDProgram   " +
                    " INNER JOIN mPElaksanaurusan on B.Parent = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
                    " AND A.Parent = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND   B.btJEnis= 3 and b.Root = 5 AND (b.Jumlah>0 or B.JumlahMurni>0)   ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.Parent=" + _p.IDDinas.ToString();
                }

                SSQL = SSQL + " GROUP BY A.IDUrusan, A.Parent,A.IDProgram, A.sNamaProgram,mPelaksanaURusan.IsPokok";
                //if (_p.LastLevel == 5)
                //{

                SSQL = SSQL + "  UNION ALL    " +
            " Select 3.btJenis,A.IDUrusan, A.Parent,A.IDProgram, A.IDkegiatan  as IDKegiatan,0 as IIDRekening,0 as Root, A.sNama as sNamaRekening,   " +
            " SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan,mPelaksanaUrusan.IsPokok FROM tKegiatan_A A   " +
            " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.Parent AND A.IDUrusan = B.IDUrusan   AND A.IDProgram = B.IDProgram and A.IDkegiatan=B.IDKegiatan  " +
        " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
        " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan    " +
        " WHERE B.iTAhun = " + _p.Tahun.ToString() + " AND B.btJEnis= 3 and B.Root=5 AND (B.Jumlah>0 or B.JumlahMurni>0)";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();
                    SSQL = SSQL + " GROUP BY A.IDUrusan, A.Parent,A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok  ";

                }
                else
                {
                    SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok  ";
                }

                // }

                if (_p.LastLevel == 3)
                {
                    SSQL = SSQL + " UNION ALL  " +
                        " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".Parent,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni , mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan ,mPelaksanaUrusan.IsPokok  from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                        " INNER JOIN mPElaksanaurusan on " + namaView + ".Parent = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "   AND  " + namaView + ".btJEnis in ( 3) and Root in( 3) and (Jumlah  > 0  or JumlahMurni>0)";
                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".Parent=" + _p.IDDinas.ToString();
                    }

                }
                else
                {

                    SSQL = SSQL + "  UNION ALL  " +
                    " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".Parent,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni , mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan ,mPelaksanaUrusan.IsPokok  from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " INNER JOIN mPElaksanaurusan on " + namaView + ".Parent = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis in (3) and Root = 5 and (Jumlah  > 0  or JumlahMurni>0)  ";
                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".Parent=" + _p.IDDinas.ToString();
                    }

                }
                SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan," + namaView + ".Parent, IDProgram,IDkegiatan, " + namaView + ".IIDRekening,Root,iNo  ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIII()
                                {
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    Tahun = _p.Tahun,
                                    Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                                           DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                            DataFormat.GetInteger(dr["IDDInas"]),
                                                            DataFormat.GetInteger(dr["IDProgram"]),
                                                            DataFormat.GetInteger(dr["IDkegiatan"]),
                                                            DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                                    Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).Trim() : DataFormat.GetString(dr["sNamaRekening"]).Trim()) : "",
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport(),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"].ToString()).ToRupiahInReport(),
                                    Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                    Prosentase = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]), DataFormat.GetDecimal(dr["JumlahMurni"])),
                                    Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                    Level = DataFormat.GetSingle(dr["Root"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"])


                                }).ToList();
                        //}                        
                    }
                }

                if (_p.IDDinas > 0)
                {


                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,6 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                        " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND Parent=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND Parent=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND Parent=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND Parent=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }
                else
                {

                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,6 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                    " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }

                DataTable dtx = new DataTable();
                PerdaIII o = new PerdaIII();

                dtx = _dbHelper.ExecuteDataTable(SSQL);
                if (dtx != null)
                {
                    if (dtx.Rows.Count > 0)
                    {

                        DataRow dr = dtx.Rows[0];

                        o = new PerdaIII()
                        {
                            Tahun = _p.Tahun,
                            Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                                   DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                    DataFormat.GetInteger(dr["IDDInas"]),
                                                    DataFormat.GetInteger(dr["IDProgram"]),
                                                    DataFormat.GetInteger(dr["IDkegiatan"]),
                                                    DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                            Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                            Jumlah = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])).ToRupiahInReport(),
                            JumlahMurni = (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])).ToRupiahInReport(),
                            Selisih = "0",
                            Prosentase = "0",
                            Keterangan = DataFormat.GetString(dr["Keterangan"]),
                            Level = DataFormat.GetSingle(dr["Root"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"])

                        };

                    }
                }
                _lst.Add(o);
                //******
                DasarHukumLogic oLogicDasarHukum = new DasarHukumLogic(_p.Tahun);
                List<DasarHukum> _lstDasarHukum = new List<DasarHukum>();
                Single snglOnPerda = 1;

                _lstDasarHukum = oLogicDasarHukum.Get(_p.Tahun, snglOnPerda);



                int JumlahPerdaIII = _lst.Count;

                for (int idx = 0; idx < JumlahPerdaIII; idx++)
                {
                    PerdaIII p3 = _lst[idx];
                    if (idx < _lstDasarHukum.Count)
                    {
                        _lst[idx].label = _lstDasarHukum[idx].NoUrut.ToString();
                        _lst[idx].Keterangan = _lstDasarHukum[idx].Keterangan;
                    }

                }
                // Pembiayaan 
                //if (_p.IDDinas == 4040601){
                List<PerdaIII> lstpby = new List<PerdaIII>();
                lstpby = GetPerdaIIIPembiayaan(_p);
                if (lstpby.Count > 1)
                {
                    foreach (PerdaIII pby in lstpby)
                    {
                        _lst.Add(pby);
                    }
                }
                //}
                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }

        public List<PerdaIII> GetPerdaIIIBackup(ParameterLaporan _p)
        {


            string namaView = CreateViewAllLevel(_p, true);

            BersihkanNonKegiatan();
            List<PerdaIII> _lst = new List<PerdaIII>();
            try
            {

                SSQL = "Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok from " + namaView + " " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root <  " + _p.LastLevel.ToString() + " and " + namaView + ".Jumlah>0   ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }

                SSQL = SSQL + " UNION ALL    ";

                SSQL = SSQL + "  Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo,''  as Keterangan, 0 as isPokok from " + namaView +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root = 3 and " + namaView + ".Jumlah > 0   ";//and  " +
                //namaView + ".IDDInas =" + _p.IDDinas.ToString();




                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                //  SSQL = SSQL + "and (( mDasarHukum.IIDRekening in (Select IIDRekening from tAnggaranRekening_A where IDDInas=" + _p.IDDinas.ToString() + " )) ";
                //  SSQL = SSQL + "OR ( mDasarHukum.IIDRekening in (Select IIDRekening/100 * 100 from tAnggaranRekening_A where IDDInas=" + _p.IDDinas.ToString() + " )) ) ";

                SSQL = SSQL + "Union ALL Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah," +
                            "JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok from " + namaView + " WHERE " + namaView + ".iTAhun= " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 " +
                                " and " + namaView + ".Root = 3 and " + namaView + ".Jumlah > 0   AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + " ";// and " + namaView + ".IIDRekening/10000 not in (Select IIDRekening/10000 from mDasarHukum) ";

                SSQL = SSQL + " UNION ALL    ";
                SSQL = SSQL + " Select 2 as btJenis," + namaView + ".IDDInas/10000 as IDUrusan ," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  and mDasarHukum.onPerda=1 and " + namaView + ".btJenis in (2,3) and " + namaView + ".Root =1 and " + namaView + ".Jumlah>0    ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY  " + namaView + ".IDDInas," + namaView + ".IIDREKENING,ROOT,sNamaRekening, mDasarHukum.iNo, mDasarHukum.sKeterangan  ";
                SSQL = SSQL + " UNION ALL    ";




                SSQL = SSQL + " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis= 2 and " + namaView + ".Root >2  and " + namaView + ".root<= " + _p.LastLevel + " and " + namaView + ".Jumlah>0   and " + namaView + ".Root <6   ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " UNION ALL ";
                SSQL = SSQL + " Select " + namaView + ".btJenis, " + namaView + ".IDDInas/10000 as  IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 AS IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening , SUM(Jumlah) AS Jumlah, SUM(JumlahMurni) AS JumlahMurni ,mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as IsPokok  from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " and mDasarHukum.onPerda=1 AND  " + namaView + ".btJEnis= 3 and Root =2  ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }

                SSQL = SSQL + " GROUP BY " + namaView + ".btJenis," + namaView + ".IDDInas, " + namaView + ".IIDRekening,Root,sNamaRekening,mDasarHukum.iNo,mDasarHukum.sKeterangan ";

                SSQL = SSQL + "  UNION ALL  ";

                SSQL = SSQL + "  Select 3 as btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, 0 as IDKegiatan,0 as IIDRekening,0 as Root, A.sNamaProgram as sNamaRekening,    " +
                    "  SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan, mPelaksanaURusan.IsPokok FROM tPrograms_A A   " +
                    " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan    " +
                    " AND A.IDProgram = B.IDProgram   " +
                    " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
                    " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND   B.btJEnis= 3 and b.Root = 5 AND (b.Jumlah>0 or B.JumlahMurni>0)   ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();
                }

                SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram, A.sNamaProgram,mPelaksanaURusan.IsPokok";
                //if (_p.LastLevel == 5)
                //{

                SSQL = SSQL + "  UNION ALL    " +
            " Select 3.btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, A.IDkegiatan  as IDKegiatan,0 as IIDRekening,0 as Root, A.sNama as sNamaRekening,   " +
            " SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan,mPelaksanaUrusan.IsPokok FROM tKegiatan_A A   " +
            " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan   AND A.IDProgram = B.IDProgram and A.IDkegiatan=B.IDKegiatan  " +
        " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
        " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan    " +
        " WHERE B.iTAhun = " + _p.Tahun.ToString() + " AND B.btJEnis= 3 and B.Root=5 AND (B.Jumlah>0 or B.JumlahMurni>0)";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();
                    SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok  ";

                }
                else
                {
                    SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok  ";
                }

                // }

                if (_p.LastLevel == 3)
                {
                    SSQL = SSQL + " UNION ALL  " +
                        " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni , mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan ,mPelaksanaUrusan.IsPokok  from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                        " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "   AND  " + namaView + ".btJEnis in ( 3) and Root in( 3) and (Jumlah  > 0  or JumlahMurni>0)";
                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                    }

                }
                else
                {

                    SSQL = SSQL + "  UNION ALL  " +
                    " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni , mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan ,mPelaksanaUrusan.IsPokok  from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis in (3) and Root = 5 and (Jumlah  > 0  or JumlahMurni>0)  ";
                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                    }

                }
                SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan," + namaView + ".IDDInas, IDProgram,IDkegiatan, " + namaView + ".IIDRekening,Root,iNo  ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIII()
                                {
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    Tahun = _p.Tahun,
                                    Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                                           DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                            DataFormat.GetInteger(dr["IDDInas"]),
                                                            DataFormat.GetInteger(dr["IDProgram"]),
                                                            DataFormat.GetInteger(dr["IDkegiatan"]),
                                                            DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                                    Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).Trim() : DataFormat.GetString(dr["sNamaRekening"]).Trim()) : "",
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport(),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"].ToString()).ToRupiahInReport(),
                                    Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                    Prosentase = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]), DataFormat.GetDecimal(dr["JumlahMurni"])),
                                    Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                    Level = DataFormat.GetSingle(dr["Root"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"])


                                }).ToList();
                        //}                        
                    }
                }

                if (_p.IDDinas > 0)
                {


                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,6 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                        " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }
                else
                {

                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,6 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                    " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }

                DataTable dtx = new DataTable();
                PerdaIII o = new PerdaIII();

                dtx = _dbHelper.ExecuteDataTable(SSQL);
                if (dtx != null)
                {
                    if (dtx.Rows.Count > 0)
                    {

                        DataRow dr = dtx.Rows[0];

                        o = new PerdaIII()
                        {
                            Tahun = _p.Tahun,
                            Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                                   DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                    DataFormat.GetInteger(dr["IDDInas"]),
                                                    DataFormat.GetInteger(dr["IDProgram"]),
                                                    DataFormat.GetInteger(dr["IDkegiatan"]),
                                                    DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                            Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                            Jumlah = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])).ToRupiahInReport(),
                            JumlahMurni = (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])).ToRupiahInReport(),
                            Selisih = "0",
                            Prosentase = "0",
                            Keterangan = DataFormat.GetString(dr["Keterangan"]),
                            Level = DataFormat.GetSingle(dr["Root"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"])

                        };

                    }
                }
                _lst.Add(o);
                //******
                int JumlahPerdaIII = _lst.Count;
                for (int idx = 0; idx < JumlahPerdaIII; idx++)
                {
                    PerdaIII p3 = _lst[idx];

                    if (p3.IDKegiatan == 0 && p3.Jenis == 1)
                    {
                        List<PerdaIII> pDasarHukum = new List<PerdaIII>();
                        pDasarHukum = GetDasarHukum(_p, p3);
                        if (pDasarHukum != null)
                        {
                            if (pDasarHukum.Count > 0)
                            {
                                _lst[idx].label = pDasarHukum[0].label;
                                _lst[idx].Keterangan = pDasarHukum[0].Keterangan;

                                for (int idxHukum = 1; idxHukum < pDasarHukum.Count; idxHukum++)
                                {
                                    idx++;
                                    _lst.Insert(idx, pDasarHukum[idxHukum]);
                                    JumlahPerdaIII++;

                                }
                            }
                        }


                    }
                }
                // Pembiayaan 
                //if (_p.IDDinas == 4040601){
                List<PerdaIII> lstpby = new List<PerdaIII>();
                lstpby = GetPerdaIIIPembiayaan(_p);
                if (lstpby.Count > 1)
                {
                    foreach (PerdaIII pby in lstpby)
                    {
                        _lst.Add(pby);
                    }
                }
                //}
                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        public List<PerdaIII> GetPerdaIIIPembiayaan(ParameterLaporan _p)
        {

            string namaView = CreateViewAllLevel(_p, true);

            List<PerdaIII> _lst = new List<PerdaIII>();
            try
            {

                if (_p.IDDinas > 0)
                {
                    SSQL = "Select " + namaView + ".btJenis," + namaView + ".IDDInas/10000 IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, 0 as Jumlah, 0 as JumlahMurni, mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + " AND  " + namaView + ".btJEnis= 4 and " + namaView + ".Root = 1 and " + namaView + ".Jumlah>0   " +
                        "  ";


                    SSQL = SSQL + " UNION ALL Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + " AND  " + namaView + ".btJEnis= 4 and " + namaView + ".Root > 1 and " + namaView + ".Root <= " + _p.LastLevel.ToString() + "  and " + namaView + ".Jumlah>0   " +
                        " UNION ALL    ";

                    SSQL = SSQL + " Select 5 as btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + "  and " + namaView + ".btJenis =5 and " + namaView + ".Root > 1 and " + namaView + ".Root < 4  and " + namaView + ".Jumlah>0    " +
                        " GROUP BY " + namaView + ".IDUrusan," + namaView + ".IDDInas," + namaView + ".IIDREKENING,ROOT,sNamaRekening, mDasarHukum.iNo, mDasarHukum.sKeterangan  ";


                    SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan," + namaView + ".IDDInas, IDProgram,IDkegiatan, " + namaView + ".IIDRekening,Root,iNo  ";
                }
                else
                {

                    SSQL = "Select " + namaView + ".btJenis," + namaView + ".IDDInas/10000 IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, 0 as Jumlah, 0 as JumlahMurni, mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 4 and " + namaView + ".Root = 1 and " + namaView + ".Jumlah>0   " +
                        "  ";


                    SSQL = SSQL + " UNION ALL Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".btJEnis= 4 and " + namaView + ".Root > 1 and " + namaView + ".Root < 4  and " + namaView + ".Jumlah>0   " +
                        " UNION ALL    ";

                    SSQL = SSQL + " Select 5 as btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " and " + namaView + ".btJenis =5 and " + namaView + ".Root > 1 and " + namaView + ".Root < 4  and " + namaView + ".Jumlah>0    " +
                        " GROUP BY " + namaView + ".IDUrusan," + namaView + ".IDDInas," + namaView + ".IIDREKENING,ROOT,sNamaRekening, mDasarHukum.iNo, mDasarHukum.sKeterangan  ";


                    SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan, IDProgram,IDkegiatan, " + namaView + ".IIDRekening,Root,iNo  ";

                }

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIII()
                                {
                                    Tahun = _p.Tahun,
                                    Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                                           DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                            DataFormat.GetInteger(dr["IDDInas"]),
                                                            DataFormat.GetInteger(dr["IDProgram"]),
                                                            DataFormat.GetInteger(dr["IDkegiatan"]),
                                                            DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                                    Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                                    Jumlah = DataFormat.GetDecimal(dr["iNo"]) < 2 ? DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport() : "",
                                    JumlahMurni = DataFormat.GetDecimal(dr["iNo"]) < 2 ? DataFormat.GetDecimal(dr["JumlahMurni"].ToString()).ToRupiahInReport() : "",
                                    Selisih = "0",
                                    Prosentase = "0",
                                    Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                    Level = DataFormat.GetSingle(dr["Root"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"])

                                }).ToList();
                        //}                        
                    }
                }

                SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,6 as Root,'PEMBIAYAAN NETTO' as sNamaRekening, " +
                    " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=4 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis =5 and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=4 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis =5 and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";

                DataTable dtx = new DataTable();
                PerdaIII o = new PerdaIII();

                dtx = _dbHelper.ExecuteDataTable(SSQL);
                if (dtx != null)
                {
                    if (dtx.Rows.Count > 0)
                    {

                        DataRow dr = dtx.Rows[0];

                        o = new PerdaIII()
                        {
                            Tahun = _p.Tahun,
                            Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                                   DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                    DataFormat.GetInteger(dr["IDDInas"]),
                                                    DataFormat.GetInteger(dr["IDProgram"]),
                                                    DataFormat.GetInteger(dr["IDkegiatan"]),
                                                    DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                            Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                            Jumlah = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])).ToRupiahInReport(),
                            JumlahMurni = (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])).ToRupiahInReport(),
                            Selisih = "0",
                            Prosentase = "0",
                            Keterangan = DataFormat.GetString(dr["Keterangan"]),
                            Level = DataFormat.GetSingle(dr["Root"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"])

                        };

                    }
                }
                _lst.Add(o);
                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        public List<PerdaIII> GetPerdaIIIlama(ParameterLaporan _p)
        {

            string namaView = CreateViewAllLevel(_p, true);

            List<PerdaIII> _lst = new List<PerdaIII>();
            try
            {

                SSQL = "Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root < 4 and " + namaView + ".Jumlah>0   " +
                    " UNION ALL    ";

                SSQL = SSQL + " Select 2 as btJenis,0 as IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + "  and " + namaView + ".btJenis in (2,3) and " + namaView + ".Root =1 and " + namaView + ".Jumlah>0    " +
                    " GROUP BY " + namaView + ".IDDInas," + namaView + ".IIDREKENING,ROOT,sNamaRekening, mDasarHukum.iNo, mDasarHukum.sKeterangan  ";

                SSQL = SSQL + " UNION ALL ";

                SSQL = SSQL + " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + "  AND  " + namaView + ".btJEnis= 2 and " + namaView + ".Root >1  and " + namaView + ".Jumlah>0  and " + namaView + ".Root <4   ";
                // Belanja Langsung 
                SSQL = SSQL + " UNION ALL ";
                SSQL = SSQL + " Select " + namaView + ".btJenis, 0 as IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 AS IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening , SUM(Jumlah) AS Jumlah, SUM(JumlahMurni) AS JumlahMurni ,mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as IsPokok  from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + "  AND  " + namaView + ".btJEnis= 3 and Root =2  " +
                    " GROUP BY " + namaView + ".btJenis," + namaView + ".IDDInas, " + namaView + ".IIDRekening,Root,sNamaRekening,mDasarHukum.iNo,mDasarHukum.sKeterangan ";

                SSQL = SSQL + "  UNION ALL  ";

                SSQL = SSQL + "  Select 3 as btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, 0 as IDKegiatan,0 as IIDRekening,0 as Root, A.sNamaProgram as sNamaRekening,    " +
                    "  SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan, mPelaksanaURusan.IsPokok FROM tPrograms_A A   " +
                    " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan    " +
                    " AND A.IDProgram = B.IDProgram   " +
                    " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
                    " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND A.IDDinas=" + _p.IDDinas.ToString() + " AND   B.btJEnis= 3 and b.Root = 5 AND (b.Jumlah>0 or B.JumlahMurni>0)   " +
                    " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram, A.sNamaProgram,mPelaksanaURusan.IsPokok" +
                    " UNION ALL    " +
                    " Select 3.btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, A.IDkegiatan  as IDKegiatan,0 as IIDRekening,0 as Root, A.sNama as sNamaRekening,   " +
                    " SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan,mPelaksanaUrusan.IsPokok FROM tKegiatan_A A   " +
                    " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan   AND A.IDProgram = B.IDProgram and A.IDkegiatan=B.IDKegiatan  " +
                " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
                " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan    " +
                " WHERE B.iTAhun = " + _p.Tahun.ToString() + " AND B.IDDinas=" + _p.IDDinas.ToString() + " AND   B.btJEnis= 3 and B.Root=5 AND (B.Jumlah>0 or B.JumlahMurni>0)" +
                " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok  ";
                if (_p.LastLevel == 3)
                {
                    SSQL = SSQL + " UNION ALL  " +
                        " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni , mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan ,mPelaksanaUrusan.IsPokok  from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                        " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + "  AND  " + namaView + ".btJEnis in (2, 3) and Root = 3 and (Jumlah  > 0  or JumlahMurni>0)";
                }
                else
                {

                    SSQL = SSQL + "  UNION ALL  " +
                    " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni , mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan ,mPelaksanaUrusan.IsPokok  from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + "  AND  " + namaView + ".btJEnis in (2, 3) and Root = 5 and (Jumlah  > 0  or JumlahMurni>0)  ";
                }
                SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan,IDDInas, IDProgram,IDkegiatan, IIDRekening,Root,iNo  ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIII()
                                {
                                    Tahun = _p.Tahun,
                                    Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                                         DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                            DataFormat.GetInteger(dr["IDDInas"]),
                                                            DataFormat.GetInteger(dr["IDProgram"]),
                                                            DataFormat.GetInteger(dr["IDkegiatan"]),
                                                            DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                                    Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                                    Jumlah = DataFormat.GetDecimal(dr["iNo"]) < 2 ? DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport() : "",
                                    JumlahMurni = DataFormat.GetInteger(dr["iNo"]) > 0 ? DataFormat.GetInteger(dr["iNo"]).ToString() + "." : "",
                                    Selisih = "0",
                                    Prosentase = "0",
                                    Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                    Level = DataFormat.GetSingle(dr["Root"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"])

                                }).ToList();
                        //}                        
                    }
                }
                CreateViewAllLevel(_p, false);

                ////PerdaIII p1 = new PerdaIII();
                //_lst[0].label = "1";
                //_lst[0].Keterangan = "Peraturan Pemerintah Nomor 58 Tahun 2005 Tentang Pengelolaan Keuangan Daerah";

                //_lst[1].label = "2";
                //_lst[1].Keterangan = "Peraturan Menteri Dalam Negeri Nomor 21 Tahun 2011 Tentang Perubahan Kedua Atas Peraturan Menteri Dalam Negeri Nomor 13 Tahun 2006 Tentang Pedoman Pengelolaan Keuangan Daerah";

                //_lst[2].label = "3";
                //_lst[2].Keterangan = "Peraturan Menteri Dalam Negeri Nomor 31 Tahun 2016 Tentang Pedoman Penyusunan APBD Tahun Anggaran 2017";




                return _lst;

                //SSQL = "select SUM( cJumlah ) AS Belanja " +
                //        " from tAnggarAnRekening_A WHERE iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + 
                //        " AND btJenis in (2,3) and IIDRekening like '5%'";


                //DataTable dtb = new DataTable();
                //PerdaIII ob = new PerdaIII();
                //dtb = _dbHelper.ExecuteDataTable(SSQL);
                //if (dtb != null)
                //{
                //    if (dtb.Rows.Count > 0)
                //    {
                //        if (_p.Tahap == 0 || _p.Tahap == 2)
                //        {
                //            DataRow dr = dtb.Rows[0];
                //            ob = new PerdaIII()
                //            {
                //                Tahun = _p.Tahun,
                //                Kode = "",
                //                Nama = "JUMLAH BELANJA ",
                //                Jumlah = DataFormat.GetDecimal(dr["BELANJA"]).ToRupiahInReport(),
                //                JumlahMurni = "0",
                //                Selisih = "0",
                //                Prosentase = "0",
                //                Keterangan = "",
                //                Level = 6,
                //                Jenis = 4

                //            };
                //            _lst.Add(ob);
                //        }
                //    }
                //}

                //SSQL = "select SUM( case when btJenis= 1 THEN (cJumlah)ELSE 0 END ) AS Pendapatan,SUM( case when btJenis in (2,3) THEN (cJumlah)ELSE 0 END ) AS Belanja " +
                //        " from tAnggarAnRekening_A WHERE iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString(); 

                //DataTable dtx = new DataTable();
                //PerdaIII o = new PerdaIII();
                //dtx = _dbHelper.ExecuteDataTable(SSQL);
                //if (dtx != null)
                //{
                //    if (dtx.Rows.Count > 0)
                //    {
                //        if (_p.Tahap == 0 || _p.Tahap == 2)
                //        {
                //            DataRow dr = dtx.Rows[0];
                //            o = new PerdaIII()
                //                    {
                //                        Tahun = _p.Tahun,
                //                        Kode = "",
                //                        Nama = "SURPLUS/DEFISIT",
                //                        Jumlah = DataFormat.GetDecimal(DataFormat.GetDecimal(dr["Pendapatan"]) - DataFormat.GetDecimal(dr["BELANJA"])).ToRupiahInReport(),
                //                        JumlahMurni = "0",
                //                        Selisih = "0",
                //                        Prosentase = "0",
                //                        Keterangan = "",
                //                        Level = 6,
                //                        Jenis = 4

                //                    };
                //            _lst.Add(o);
                //        }
                //    }
                //}



                //SSQL=" Select vwAnggaranAllLevel.btJenis +2 as btJenis,vwAnggaranAllLevel.IDUrusan,vwAnggaranAllLevel.IDDInas,  0 as IDProgram , 0 as IDkegiatan, vwAnggaranAllLevel.IIDRekening,Root,sNamaRekening,JumlahOlah , Jumlah, JumlahMurni , 0 as iNo,'' as Keterangan from vwAnggaranAllLevel  " +
                //    " WHERE vwAnggaranAllLevel.iTAhun = " + _p.Tahun.ToString() + " AND vwAnggaranAllLevel.IDDinas=" + _p.IDDinas.ToString() + "  AND  vwAnggaranAllLevel.btJEnis in (4,5) and Root >0 and Jumlah>0  " +
                //    " order by btJenis,IDUrusan,IDDInas, IDProgram,IDkegiatan, IIDRekening,Root,iNo  ";
                //List<PerdaIII> lsp = new List<PerdaIII>();
                //DataTable dtp = new DataTable();
                //dtp = _dbHelper.ExecuteDataTable(SSQL);
                //if (dt != null)
                //{
                //    if (dt.Rows.Count > 0)
                //    {
                //        if (_p.Tahap == 0 || _p.Tahap == 2)
                //        {
                //            lsp = (from DataRow dr in dtp.Rows
                //                    select new PerdaIII()
                //                    {

                //                        Tahun = _p.Tahun,
                //                        Kode = DataFormat.GetInteger(dr["iNo"]) < 2 ?
                //                             DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                //                                                DataFormat.GetInteger(dr["IDDInas"]),
                //                                                DataFormat.GetInteger(dr["IDProgram"]),
                //                                                DataFormat.GetInteger(dr["IDkegiatan"]),
                //                                                DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                //                        Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                //                        Jumlah = DataFormat.GetDecimal(dr["iNo"]) < 2 ? DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport() : "",
                //                        JumlahMurni = DataFormat.GetInteger(dr["iNo"]) > 0 ? DataFormat.GetInteger(dr["iNo"]).ToString() + "." : "",
                //                        Selisih = "0",
                //                        Prosentase = "0",
                //                        Keterangan = DataFormat.GetString(dr["Keterangan"]),
                //                        Level = DataFormat.GetSingle(dr["Root"]),
                //                        Jenis = DataFormat.GetSingle(dr["btJenis"])

                //                    }).ToList();
                //        }
                //    }
                //}

                //foreach (PerdaIII p in lsp)
                //{
                //    _lst.Add(p);

                //}
                //if (lsp.Count > 0)
                //{

                //    SSQL = "select SUM( case when btJenis IN  (1,4) THEN (cJumlah)ELSE 0 END ) AS Pendapatan,SUM( case when btJenis in (2,3,6) THEN (cJumlah)ELSE 0 END ) AS Belanja " +
                //        " from tAnggarAnRekening_A WHERE iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString();

                //    DataTable dts = new DataTable();
                //    PerdaIII stb = new PerdaIII();
                //    dts = _dbHelper.ExecuteDataTable(SSQL);
                //    if (dts != null)
                //    {
                //        if (dts.Rows.Count > 0)
                //        {
                //            if (_p.Tahap == 0 || _p.Tahap == 2)
                //            {
                //                DataRow dr = dts.Rows[0];
                //                stb = new PerdaIII()
                //                {
                //                    Tahun = _p.Tahun,
                //                    Kode = "",
                //                    Nama = "SILPA Tahun Berjalan",
                //                    Jumlah = DataFormat.GetDecimal(DataFormat.GetDecimal(dr["Pendapatan"]) - DataFormat.GetDecimal(dr["BELANJA"])).ToRupiahInReport(),
                //                    JumlahMurni = "0",
                //                    Selisih = "0",
                //                    Prosentase = "0",
                //                    Keterangan = "",
                //                    Level = 6,
                //                    Jenis = 4

                //                };
                //                _lst.Add(stb);
                //            }
                //        }
                //    }

                //}


                //return _lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        public List<PerdaIII> GetPerdaIII_B(ParameterLaporan _p)
        {

            List<PerdaIII> _lst = new List<PerdaIII>();
            try
            {
                SSQL = "Select vwAnggaranAllLevel.btJenis,vwAnggaranAllLevel.IDUrusan,vwAnggaranAllLevel.IDDInas, 0 AS IDProgram , 0 as IDkegiatan, vwAnggaranAllLevel.IIDRekening,Root,sNamaRekening,JumlahOlah , Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan from vwAnggaranAllLevel  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=vwAnggaranAllLevel.IIDRekening " +
                " WHERE vwAnggaranAllLevel.iTAhun = " + _p.Tahun.ToString() + " AND vwAnggaranAllLevel.IDDinas=" + _p.IDDinas.ToString() + " AND  vwAnggaranAllLevel.btJEnis= 1   " +
                " UNION ALL    ";

                SSQL = SSQL + " Select 2 as btJenis,0 as IDUrusan,vwAnggaranAllLevel.IDDInas, 0 AS IDProgram , 0 as IDkegiatan, vwAnggaranAllLevel.IIDRekening,Root,sNamaRekening,SUM(JumlahOlah) as JumlahOlah , SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan from vwAnggaranAllLevel  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=vwAnggaranAllLevel.IIDRekening  " +
                " WHERE vwAnggaranAllLevel.iTAhun = " + _p.Tahun.ToString() + " AND vwAnggaranAllLevel.IDDinas=" + _p.IDDinas.ToString() + "  and vwAnggaranAllLevel.btJenis in (2,3) and vwAnggaranAllLevel.Root =1    " +
                " GROUP BY vwAnggaranAllLevel.IDDInas,vwAnggaranAllLevel.IIDREKENING,ROOT,sNamaRekening, mDasarHukum.iNo, mDasarHukum.sKeterangan  ";
                SSQL = SSQL + " UNION ALL ";

                SSQL = SSQL + " Select vwAnggaranAllLevel.btJenis,vwAnggaranAllLevel.IDUrusan,vwAnggaranAllLevel.IDDInas, 0 AS IDProgram , 0 as IDkegiatan, vwAnggaranAllLevel.IIDRekening,Root,sNamaRekening,JumlahOlah , Jumlah, JumlahMurni,mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan from vwAnggaranAllLevel  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=vwAnggaranAllLevel.IIDRekening  " +
                " WHERE vwAnggaranAllLevel.iTAhun = " + _p.Tahun.ToString() + " AND vwAnggaranAllLevel.IDDinas=" + _p.IDDinas.ToString() + "  AND  vwAnggaranAllLevel.btJEnis= 2 and vwAnggaranAllLevel.Root >1    ";

                SSQL = SSQL + " UNION ALL ";
                SSQL = SSQL + " Select vwAnggaranAllLevel.btJenis,0 as IDUrusan,vwAnggaranAllLevel.IDDInas, 0 AS IDProgram , 0 AS IDkegiatan, vwAnggaranAllLevel.IIDRekening,Root,sNamaRekening,SUM(JumlahOlah) AS JumlahOlah , SUM(Jumlah) AS Jumlah, SUM(JumlahMurni) AS JumlahMurni ,mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan from vwAnggaranAllLevel  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=vwAnggaranAllLevel.IIDRekening  " +
                " WHERE vwAnggaranAllLevel.iTAhun = " + _p.Tahun.ToString() + " AND vwAnggaranAllLevel.IDDinas=" + _p.IDDinas.ToString() + "  AND  vwAnggaranAllLevel.btJEnis= 3 and Root =2  " +
                " GROUP BY vwAnggaranAllLevel.btJenis,vwAnggaranAllLevel.IDDInas, vwAnggaranAllLevel.IIDRekening,Root,sNamaRekening,mDasarHukum.iNo,mDasarHukum.sKeterangan ";
                SSQL = SSQL + "  UNION ALL  ";

                SSQL = SSQL + "  Select 3 as btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, 0 as IDKegiatan,0 as IIDRekening,0 as Root, A.sNamaProgram as sNamaRekening,    " +
                " SUM(B.cPlafon) as JumlahOlah, SUM(B.cJumlah) as Jumlah, SUM(B.cJumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan FROM tPrograms_A A   " +
                " INNER JOIN tAnggaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan    " +
                " AND A.IDProgram = B.IDProgram   " +
                " WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND A.IDDinas=" + _p.IDDinas.ToString() + " AND   B.btJEnis= 3  AND b.cJumlah>0   " +
                " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram, A.sNamaProgram" +
                " UNION ALL    " +
                " Select 3.btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, A.IDkegiatan  as IDKegiatan,0 as IIDRekening,0 as Root, A.sNama2 as sNamaRekening,   " +
                " SUM(B.cPlafon) as JumlahOlah, SUM(B.cJumlah) as Jumlah, SUM(B.cJumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan FROM tKegiatan_A A   " +
                " INNER JOIN tAnggaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan    " +
                " AND A.IDProgram = B.IDProgram and A.IDkegiatan=B.IDKegiatan   " +
                " WHERE B.iTAhun = " + _p.Tahun.ToString() + " AND B.IDDinas=" + _p.IDDinas.ToString() + " AND   B.btJEnis= 3  AND b.cJumlah>0 " +
                " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram, A.sNama2,A.IDkegiatan  " +
                " UNION ALL  " +
                " Select vwAnggaranAllLevel.btJenis,vwAnggaranAllLevel.IDUrusan,vwAnggaranAllLevel.IDDInas,  IDProgram , IDkegiatan, vwAnggaranAllLevel.IIDRekening,Root,sNamaRekening,JumlahOlah , Jumlah, JumlahMurni , mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan from vwAnggaranAllLevel  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=vwAnggaranAllLevel.IIDRekening  " +
                " WHERE vwAnggaranAllLevel.iTAhun = " + _p.Tahun.ToString() + " AND vwAnggaranAllLevel.IDDinas=" + _p.IDDinas.ToString() + "  AND  vwAnggaranAllLevel.btJEnis= 3 and Root =5 and Jumlah>0  " +
                " UNION ALL  " +
                " Select distinct 4 as btJenis,0 as IDUrusan,0 as IDDInas,  0 as IDProgram , 0 as IDkegiatan, 0 as IIDRekening,6 as Root,'SURPLUS/DEFISIT',dbo.SurplusDefisit(vwAnggaranAllLevel.iTahun,vwAnggaranAllLevel.IDDInas)  as JumlahOlah , dbo.SurplusDefisit(vwAnggaranAllLevel.iTahun,vwAnggaranAllLevel.IDDInas) as Jumlah, dbo.SurplusDefisit(vwAnggaranAllLevel.iTahun,vwAnggaranAllLevel.IDDInas) as  JumlahMurni ,0 as iNo, '' as Keterangan from vwAnggaranAllLevel  " +
                " WHERE vwAnggaranAllLevel.iTAhun = " + _p.Tahun.ToString() + " AND vwAnggaranAllLevel.IDDinas=" + _p.IDDinas.ToString() +
                " order by btJenis,IDUrusan,IDDInas, IDProgram,IDkegiatan, IIDRekening,Root,iNo  ";



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (_p.Tahap == 0 || _p.Tahap == 2)
                        {
                            _lst = (from DataRow dr in dt.Rows
                                    select new PerdaIII()
                                    {

                                        Tahun = _p.Tahun,
                                        Kode = DataFormat.GetInteger(dr["iNo"]) < 2 ?
                                             DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                                DataFormat.GetInteger(dr["IDDInas"]),
                                                                DataFormat.GetInteger(dr["IDProgram"]),
                                                                DataFormat.GetInteger(dr["IDkegiatan"]),
                                                                DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                                        Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                                        Jumlah = DataFormat.GetDecimal(dr["iNo"]) < 2 ? DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport() : "",
                                        JumlahMurni = DataFormat.GetInteger(dr["iNo"]) > 0 ? DataFormat.GetInteger(dr["iNo"]).ToString() + "." : "",
                                        Selisih = "0",
                                        Prosentase = "0",
                                        Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                        Level = DataFormat.GetSingle(dr["Root"]),
                                        Jenis = DataFormat.GetSingle(dr["btJenis"])

                                    }).ToList();
                        }
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

        //
        // *********RINGKSAN PERDA***********
        //

        public List<RingkasanPerda> GetRingkasanPerda(int _iTahun, Single _iTahap, ParameterLaporan _p)
        {

            List<RingkasanPerda> _lsttemp = new List<RingkasanPerda>();
            List<RingkasanPerda> _lst = new List<RingkasanPerda>();
            try
            {
                BersihkanNonKegiatan();
                SetProfileRekening(mprofile);
                string sNamaView = "";
                GetKolom(_p.Tahap);
                if (_p.Tahap < 7)
                    sNamaView = CreateViewAllLevel(_p, true);
                else

                    if (_iTahun < 2022)
                    {
                        sNamaView = CreateViewAllLevelRealisasi(_p, true);
                    }
                    else
                    {


                        sNamaView = " dbo.fn_AnggaranAndRealisasiAllLevel (" + _iTahun.ToString() + " ," + _p.TanggalRealisasi.ToSQLFormat() + ") ";
                    }

                List<RingkasanPerda> _lstPendapatan = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlPendapatan = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaTidakLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlBelanjaTidakLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlBelanjaLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel1 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel2 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel3 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel4 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel5 = new List<RingkasanPerda>();

                string namaKolomdiView1 = "JumlahMurni";
                string namaKolomdiView2 = "Jumlah";
                string pengurang = "3000000";
                string rekeningpdt = "4000000";


                if (Tahun >= 2021)
                {
                    pengurang = "300000000000";
                    rekeningpdt = "400000000000";
                }

                int b = 0;
                if (_p.LastLevel == 3)
                    SSQL = "SELECT 1 as Kelompok,0 as Level, Root,IIDrekening-" + pengurang + " as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun=  " + _iTahun.ToString() + "  AND  btJEnis= 1 and Root<4  AND IIDrekening >= " + rekeningpdt + " GROUP BY Root,IIDrekening, sNamaRekening ";
                else
                    SSQL = "SELECT 1 as Kelompok,0 as Level, Root,IIDrekening-" + pengurang + " as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun=  " + _iTahun.ToString() + "  AND  btJEnis= 1 and Root<=6  AND IIDrekening >= " + rekeningpdt + "  GROUP BY Root,IIDrekening, sNamaRekening ";

                _lstPendapatan = GetBagianRingkasanPerda(SSQL);
                _lst = _lstPendapatan;
                b = 1;

                b = 2;
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + pengurang + " as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3)  and Root =1  AND IIDrekening > " + rekeningpdt + "   GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel1 = GetBagianRingkasanPerda(SSQL);
                foreach (RingkasanPerda p in _lstBelanjaLevel1)
                {
                    _lst.Add(p);
                }
                b = 3;
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + pengurang + " as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =2  and Root=2  AND IIDrekening > " + rekeningpdt + "   GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel2 = GetBagianRingkasanPerda(SSQL);

                foreach (RingkasanPerda p in _lstBelanjaLevel2)
                {
                    //        _lst.Add(p);
                }
                b = 4;
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + pengurang + " as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =2  and Root=3  AND IIDrekening > " + rekeningpdt + "   GROUP BY Root, IIDrekening, sNamaRekening ";

                _lstBelanjaLevel3 = GetBagianRingkasanPerda(SSQL);
                foreach (RingkasanPerda p in _lstBelanjaLevel3)
                {
                    //       _lst.Add(p);
                }

                if (_p.LastLevel == 5)
                {
                    b = 5;
                    SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + pengurang + " as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2)  and Root=4  AND IIDrekening > " + rekeningpdt + "   GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel4 = GetBagianRingkasanPerda(SSQL);

                    foreach (RingkasanPerda p in _lstBelanjaLevel4)
                    {
                        //                 _lst.Add(p);
                    }
                    b = 6;
                    SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + pengurang + " as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2)  and Root=5  AND IIDrekening > " + rekeningpdt + "   GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel5 = GetBagianRingkasanPerda(SSQL);
                    foreach (RingkasanPerda p in _lstBelanjaLevel5)
                    {
                        //               _lst.Add(p);
                    }
                }
                if (_p.LastLevel == 6)
                {
                    b = 5;
                    SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + pengurang + " as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2)  and Root=4  AND IIDrekening > " + rekeningpdt + "   GROUP BY Root, IIDrekening, sNamaRekening ";


                    _lstBelanjaLevel4 = GetBagianRingkasanPerda(SSQL);

                    foreach (RingkasanPerda p in _lstBelanjaLevel4)
                    {
                        //                 _lst.Add(p);
                    }
                    b = 6;
                    SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + pengurang + " as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2)  and Root=5  AND IIDrekening > " + rekeningpdt + "   GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel5 = GetBagianRingkasanPerda(SSQL);
                    foreach (RingkasanPerda p in _lstBelanjaLevel5)
                    {
                        //             _lst.Add(p);
                    }
                    b = 6;
                    SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + pengurang + " as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2)  and Root=6  AND IIDrekening > " + rekeningpdt + "   GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel5 = GetBagianRingkasanPerda(SSQL);
                    foreach (RingkasanPerda p in _lstBelanjaLevel5)
                    {
                        //            _lst.Add(p);
                    }

                }

                b = 7;

                b = 8;
                SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-" + pengurang + " as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  in (2,3)  and Root=2  AND IIDrekening > " + rekeningpdt + "   GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel2 = GetBagianRingkasanPerda(SSQL);

                foreach (RingkasanPerda p in _lstBelanjaLevel2)
                {
                    _lst.Add(p);
                }

                b = 9;
                SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-" + pengurang + " as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  in (2,3)  and Root=3  AND IIDrekening > " + rekeningpdt + "   GROUP BY Root, IIDrekening, sNamaRekening ";

                _lstBelanjaLevel3 = GetBagianRingkasanPerda(SSQL);
                foreach (RingkasanPerda p in _lstBelanjaLevel3)
                {
                    _lst.Add(p);
                }
                if (_p.LastLevel == 5)
                {
                    b = 10;
                    SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-" + pengurang + " as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3)  and Root=4  AND IIDrekening > " + rekeningpdt + "   GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel4 = GetBagianRingkasanPerda(SSQL);
                    foreach (RingkasanPerda p in _lstBelanjaLevel4)
                    {
                        _lst.Add(p);
                    }
                    b = 11;
                    SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-" + pengurang + " as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3)  and Root=5  AND IIDrekening > " + rekeningpdt + "   GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel5 = GetBagianRingkasanPerda(SSQL);
                    foreach (RingkasanPerda p in _lstBelanjaLevel5)
                    {
                        _lst.Add(p);
                    }
                }

                if (_p.LastLevel == 6)
                {
                    b = 10;
                    SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-" + pengurang + " as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3)  and Root=4  AND IIDrekening > " + rekeningpdt + "   GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel4 = GetBagianRingkasanPerda(SSQL);
                    foreach (RingkasanPerda p in _lstBelanjaLevel4)
                    {
                        _lst.Add(p);
                    }
                    b = 11;
                    SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-" + pengurang + " as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3)  and Root=5  AND IIDrekening > " + rekeningpdt + "   GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel5 = GetBagianRingkasanPerda(SSQL);
                    foreach (RingkasanPerda p in _lstBelanjaLevel5)
                    {
                        _lst.Add(p);
                    }

                    SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-" + pengurang + " as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3)  and Root=6  AND IIDrekening > " + rekeningpdt + "   GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel5 = GetBagianRingkasanPerda(SSQL);

                    foreach (RingkasanPerda p in _lstBelanjaLevel5)
                    {
                        _lst.Add(p);
                    }

                }


                //b = 11;
                //if (Tahun<2021)
                //SSQL = " SELECT 5 as Kelompok,1 as Level,1 as Root, 0 as IIDRekening, 'SURPLUS/DEFISIT' as sNamaRekening," +
                //    " (SELECT SUM(" + namaKolomdiView1 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1) and Root=1 AND IIDrekening > "+ rekeningpdt+ " ) - " +
                //    " (SELECT SUM(" + namaKolomdiView1 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1 AND IIDrekening > " + rekeningpdt + " )  as JumlahMurni, " +
                //    " (SELECT SUM(" + namaKolomdiView2 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1) and Root=1 AND IIDrekening > " + rekeningpdt + " ) - " +
                //    " (SELECT SUM(" + namaKolomdiView2 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1 AND IIDrekening > " + rekeningpdt + " )  as Jumlah";

                //else
                //    SSQL = " SELECT 5 as Kelompok,1 as Level,1 as Root, 0 as IIDRekening, 'SURPLUS/DEFISIT' as sNamaRekening," +
                //    " (SELECT SUM(" + namaKolomdiView1 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1) and Root=1 ) - " +
                //    " (SELECT SUM(" + namaKolomdiView1 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1 )  as JumlahMurni, " +
                //    " (SELECT SUM(" + namaKolomdiView2 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1) and Root=1 ) - " +
                //    " (SELECT SUM(" + namaKolomdiView2 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1 )  as Jumlah";
                if (Tahun < 2021)

                    SSQL = " SELECT 5 as Kelompok,1 as Level,1 as Root, 0 as IIDRekening, 'SURPLUS/DEFISIT' as sNamaRekening," +
                    " (SELECT SUM(" + namaKolomdiView1 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1) and Root=1  ) - " +
                    " (SELECT SUM(" + namaKolomdiView1 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1  )  as JumlahMurni, " +
                    " (SELECT SUM(" + namaKolomdiView2 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1) and Root=1  ) - " +
                    " (SELECT SUM(" + namaKolomdiView2 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1)  as Jumlah";

                else
                    SSQL = " SELECT 5 as Kelompok,1 as Level,1 as Root, 0 as IIDRekening, 'SURPLUS/DEFISIT' as sNamaRekening," +
                    " (SELECT SUM(" + namaKolomdiView1 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1) and Root=1 ) - " +
                    " (SELECT SUM(" + namaKolomdiView1 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1 )  as JumlahMurni, " +
                    " (SELECT SUM(" + namaKolomdiView2 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1) and Root=1 ) - " +
                    " (SELECT SUM(" + namaKolomdiView2 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1 )  as Jumlah";


                //" SELECT SUM(" + namaKolomdiView1 + ") as JumlahMurni,SUM(" + namaKolomdiView2 + ") as Jumlah from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1) and Root=1 AND IIDrekening > 4000000 ) - " +
                //    " - SUM(" + namaKolomdiView1 + ") as JumlahMurni,SUM(" + namaKolomdiView2 + ") as Jumlah from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1 AND IIDrekening > 4000000 ) ";

                _JmlBelanjaLangsung = GetBagianRingkasanPerda(SSQL);
                foreach (RingkasanPerda p in _JmlBelanjaLangsung)
                {
                    _lst.Add(p);
                }

                var cats = from c in _lst
                             .OrderBy(i => i.Kelompok).ThenBy(i => i.IDRekening)
                           select c;
                _lsttemp = cats.ToList<RingkasanPerda>();



                CreateViewAllLevel(_p, false);

                List<RingkasanPerda> lstPembiayaan = new List<RingkasanPerda>();

                lstPembiayaan = GetRingkasanPerdaPembiayaan(_iTahun, _iTahap, _p);
                foreach (RingkasanPerda p in lstPembiayaan)
                {
                    _lsttemp.Add(p);
                }


                return _lsttemp;

            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<RingkasanPerda> GetRingkasanPerda90(int _iTahun, Single _iTahap, ParameterLaporan _p)
        {

            List<RingkasanPerda> _lsttemp = new List<RingkasanPerda>();
            List<RingkasanPerda> _lst = new List<RingkasanPerda>();
            try
            {
                BersihkanNonKegiatan();
                SetProfileRekening(mprofile);

                GetKolom(_p.Tahap);
                string sNamaView = CreateViewAllLevel(_p, true);

                List<RingkasanPerda> _lstPendapatan = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlPendapatan = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaTidakLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlBelanjaTidakLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlBelanjaLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel1 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel2 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel3 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel4 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel5 = new List<RingkasanPerda>();

                string namaKolomdiView1 = "JumlahMurni";
                string namaKolomdiView2 = "Jumlah";

                int b = 0;
                if (_p.LastLevel == 3)
                    SSQL = "SELECT 1 as Kelompok,0 as Level, Root,IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun=  " + _iTahun.ToString() + "  AND  btJEnis= 1 and Root<4  AND IIDrekening >= 4000000 GROUP BY Root,IIDrekening, sNamaRekening ";
                else
                    SSQL = "SELECT 1 as Kelompok,0 as Level, Root,IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun=  " + _iTahun.ToString() + "  AND  btJEnis= 1 and Root<6  AND IIDrekening >= 4000000 GROUP BY Root,IIDrekening, sNamaRekening ";

                _lstPendapatan = GetBagianRingkasanPerda(SSQL);
                _lst = _lstPendapatan;
                b = 1;

                b = 2;
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3)  and Root =1  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel1 = GetBagianRingkasanPerda(SSQL);
                foreach (RingkasanPerda p in _lstBelanjaLevel1)
                {
                    _lst.Add(p);
                }
                b = 3;
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =2  and Root=2  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel2 = GetBagianRingkasanPerda(SSQL);

                foreach (RingkasanPerda p in _lstBelanjaLevel2)
                {
                    _lst.Add(p);
                }
                b = 4;
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =2  and Root=3  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";

                _lstBelanjaLevel3 = GetBagianRingkasanPerda(SSQL);
                foreach (RingkasanPerda p in _lstBelanjaLevel3)
                {
                    _lst.Add(p);
                }

                if (_p.LastLevel == 5)
                {
                    b = 5;
                    SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2)  and Root=4  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel4 = GetBagianRingkasanPerda(SSQL);

                    foreach (RingkasanPerda p in _lstBelanjaLevel4)
                    {
                        _lst.Add(p);
                    }
                    b = 6;
                    SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2)  and Root=5  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel5 = GetBagianRingkasanPerda(SSQL);
                    foreach (RingkasanPerda p in _lstBelanjaLevel5)
                    {
                        _lst.Add(p);
                    }
                }

                b = 7;

                b = 8;
                SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =3  and Root=2  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel2 = GetBagianRingkasanPerda(SSQL);

                foreach (RingkasanPerda p in _lstBelanjaLevel2)
                {
                    _lst.Add(p);
                }

                b = 9;
                SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =3  and Root=3  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";

                _lstBelanjaLevel3 = GetBagianRingkasanPerda(SSQL);
                foreach (RingkasanPerda p in _lstBelanjaLevel3)
                {
                    _lst.Add(p);
                }
                if (_p.LastLevel == 5)
                {
                    b = 10;
                    SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =3  and Root=4  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel4 = GetBagianRingkasanPerda(SSQL);
                    foreach (RingkasanPerda p in _lstBelanjaLevel4)
                    {
                        _lst.Add(p);
                    }
                    b = 11;
                    SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =3  and Root=5  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel5 = GetBagianRingkasanPerda(SSQL);
                    foreach (RingkasanPerda p in _lstBelanjaLevel5)
                    {
                        _lst.Add(p);
                    }
                }

                //b = 11;
                SSQL = " SELECT 5 as Kelompok,1 as Level,1 as Root, 0 as IIDRekening, 'SURPLUS/DEFISIT' as sNamaRekening," +
                    " (SELECT SUM(" + namaKolomdiView1 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1) and Root=1 AND IIDrekening > 400000 ) - " +
                    " (SELECT SUM(" + namaKolomdiView1 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1 AND IIDrekening > 400000 )  as JumlahMurni, " +
                    " (SELECT SUM(" + namaKolomdiView2 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1) and Root=1 AND IIDrekening > 400000 ) - " +
                    " (SELECT SUM(" + namaKolomdiView2 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1 AND IIDrekening > 400000 )  as Jumlah";


                //" SELECT SUM(" + namaKolomdiView1 + ") as JumlahMurni,SUM(" + namaKolomdiView2 + ") as Jumlah from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1) and Root=1 AND IIDrekening > 4000000 ) - " +
                //    " - SUM(" + namaKolomdiView1 + ") as JumlahMurni,SUM(" + namaKolomdiView2 + ") as Jumlah from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1 AND IIDrekening > 4000000 ) ";

                _JmlBelanjaLangsung = GetBagianRingkasanPerda(SSQL);
                foreach (RingkasanPerda p in _JmlBelanjaLangsung)
                {
                    _lst.Add(p);
                }

                var cats = from c in _lst
                             .OrderBy(i => i.Kelompok).ThenBy(i => i.IDRekening)
                           select c;
                _lsttemp = cats.ToList<RingkasanPerda>();


                CreateViewAllLevel(_p, false);

                List<RingkasanPerda> lstPembiayaan = new List<RingkasanPerda>();

                lstPembiayaan = GetRingkasanPerdaPembiayaan(_iTahun, _iTahap, _p);
                foreach (RingkasanPerda p in lstPembiayaan)
                {
                    _lsttemp.Add(p);
                }

                return _lsttemp;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }

        }
        public List<RingkasanPerda> GetRingkasanPerdaPembiayaan(int _iTahun, Single _iTahap, ParameterLaporan _p)
        {

            List<RingkasanPerda> _lsttemp = new List<RingkasanPerda>();
            List<RingkasanPerda> _lst = new List<RingkasanPerda>();
            try
            {
                //BersihkanNonKegiatan();

                GetKolom(_p.Tahap);
                string sNamaView = "";// CreateViewAllLevel(_p, true);

                if (_iTahun < 2022)
                {
                    sNamaView = CreateViewAllLevelRealisasi(_p, true);
                }
                else
                {


                    sNamaView = " dbo.fn_AnggaranAndRealisasiAllLevel (" + _iTahun.ToString() + " ," + _p.TanggalRealisasi.ToSQLFormat() + ") ";
                }


                List<RingkasanPerda> _lstPendapatan = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlPendapatan = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaTidakLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlBelanjaTidakLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlBelanjaLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel1 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel2 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel3 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel4 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel5 = new List<RingkasanPerda>();

                string namaKolomdiView1 = "JumlahMurni";
                string namaKolomdiView2 = "Jumlah";

                string pengurang = "3000000";
                string rekeningpdt = "6000000";


                if (Tahun >= 2021)
                {
                    pengurang = "300000000000";
                    rekeningpdt = "600000000000";
                }

                int b = 0;
                if (_p.LastLevel == 3)
                    SSQL = "SELECT 1 as Kelompok,0 as Level, Root,IIDrekening-" + pengurang + " as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun=  " + _iTahun.ToString() + "  AND  btJEnis= 4 and Root<4  AND IIDrekening > 6100000 GROUP BY Root,IIDrekening, sNamaRekening ";
                else
                    SSQL = "SELECT 1 as Kelompok,0 as Level, Root,IIDrekening-" + pengurang + " as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun=  " + _iTahun.ToString() + "  AND  btJEnis= 4 and Root<6  AND IIDrekening > 6100000 GROUP BY Root,IIDrekening, sNamaRekening ";

                //  _lstPendapatan = GetBagianRingkasanPerda(SSQL);
                // _lst = _lstPendapatan;
                b = 1;
                //SSQL = " SELECT 2 as Kelompok,1 as Level,6  as Root, 0 as IIDRekening, 'JUMLAH PENDAPATAN' as sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis= 1  and Root=1 AND IIDrekening > 4000000  ";
                //_JmlPendapatan = GetBagianRingkasanPerda(SSQL);
                //foreach (RingkasanPerda p in _JmlPendapatan)
                //{
                //   _lst.Add(p);
                // }
                b = 2;
                //SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + pengurang + " as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (4,5)  and Root =1  AND IIDrekening > 400000  GROUP BY Root, IIDrekening, sNamaRekening ";
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + pengurang + " as IIDrekening, sNamaRekening,0 as JumlahMurni ,0 as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (4,5)  and Root =1  AND IIDrekening > 400000  GROUP BY Root, IIDrekening, sNamaRekening ";

                _lstBelanjaLevel1 = GetBagianRingkasanPerda(SSQL);
                foreach (RingkasanPerda p in _lstBelanjaLevel1)
                {
                    _lst.Add(p);
                }
                b = 3;
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + pengurang + " as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (4,5)  and Root=2  AND IIDrekening > 400000  GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel2 = GetBagianRingkasanPerda(SSQL);

                foreach (RingkasanPerda p in _lstBelanjaLevel2)
                {
                    _lst.Add(p);
                }
                b = 4;
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + pengurang + "  as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  in (4,5) and Root=3  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";

                _lstBelanjaLevel3 = GetBagianRingkasanPerda(SSQL);
                foreach (RingkasanPerda p in _lstBelanjaLevel3)
                {
                    _lst.Add(p);
                }

                if (_p.LastLevel == 5)
                {
                    b = 5;
                    SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + pengurang + "  as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  in (4,5)  and Root=4  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel4 = GetBagianRingkasanPerda(SSQL);

                    foreach (RingkasanPerda p in _lstBelanjaLevel4)
                    {
                        _lst.Add(p);
                    }
                    b = 6;
                    SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + pengurang + "  as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  in (4,5)  and Root=5  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel5 = GetBagianRingkasanPerda(SSQL);
                    foreach (RingkasanPerda p in _lstBelanjaLevel5)
                    {
                        _lst.Add(p);
                    }
                }


                //b = 11;
                //SSQL = " SELECT 5 as Kelompok,1 as Level,1 as Root, 0 as IIDRekening, 'PEMBIAYAAN NETTO' as sNamaRekening," +
                //    " (SELECT SUM(" + namaKolomdiView1 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (4) and Root=1 AND IIDrekening >= 4000000 ) - " +
                //    " (SELECT SUM(" + namaKolomdiView1 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (5) and Root=1 AND IIDrekening >= 4000000 )  as JumlahMurni, " +
                //    " (SELECT SUM(" + namaKolomdiView2 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (4) and Root=1 AND IIDrekening >= 4000000 ) - " +
                //    " (SELECT SUM(" + namaKolomdiView2 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (5) and Root=1 AND IIDrekening >= 4000000 )  as Jumlah";

                SSQL = " SELECT 5 as Kelompok,1 as Level,1 as Root, 0 as IIDRekening, 'PEMBIAYAAN NETTO' as sNamaRekening," +
                    " (SELECT SUM(" + namaKolomdiView1 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (4) and Root=1 ) - " +
                    " (SELECT SUM(" + namaKolomdiView1 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (5) and Root=1 )  as JumlahMurni, " +
                    " (SELECT SUM(" + namaKolomdiView2 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (4) and Root=1) - " +
                    " (SELECT SUM(" + namaKolomdiView2 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (5) and Root=1 )  as Jumlah";



                _JmlBelanjaLangsung = GetBagianRingkasanPerda(SSQL);
                foreach (RingkasanPerda p in _JmlBelanjaLangsung)
                {
                    _lst.Add(p);
                }

                //SSQL = " SELECT 5 as Kelompok,1 as Level,1 as Root, 0 as IIDRekening, 'SISA LEBIH PEMBIAYAAN ANGGARAN TAHUN BERKENAN' as sNamaRekening," +
                //   " (SELECT SUM(" + namaKolomdiView1 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1,4) and Root=1 AND IIDrekening >= 4000000 ) - " +
                //   " (SELECT SUM(" + namaKolomdiView1 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3,5) and Root=1 AND IIDrekening >= 4000000 )  as JumlahMurni, " +
                //   " (SELECT SUM(" + namaKolomdiView2 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1,4) and Root=1 AND IIDrekening >= 4000000 ) - " +
                //   " (SELECT SUM(" + namaKolomdiView2 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3,5) and Root=1 AND IIDrekening >= 4000000 )  as Jumlah";

                SSQL = " SELECT 5 as Kelompok,1 as Level,1 as Root, 0 as IIDRekening, 'SISA LEBIH PEMBIAYAAN ANGGARAN TAHUN BERKENAN' as sNamaRekening," +
                   " (SELECT SUM(" + namaKolomdiView1 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1,4) and Root=1 ) - " +
                   " (SELECT SUM(" + namaKolomdiView1 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3,5) and Root=1)  as JumlahMurni, " +
                   " (SELECT SUM(" + namaKolomdiView2 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1,4) and Root=1 ) - " +
                   " (SELECT SUM(" + namaKolomdiView2 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3,5) and Root=1)  as Jumlah";


                _JmlBelanjaLangsung = GetBagianRingkasanPerda(SSQL);
                foreach (RingkasanPerda p in _JmlBelanjaLangsung)
                {
                    _lst.Add(p);
                }

                b = 24;
                var cats = from c in _lst
                             .OrderBy(i => i.Kelompok).ThenBy(i => i.IDRekening)
                           select c;
                //.ThenBy(i => i.IDRekening)
                _lsttemp = cats.ToList<RingkasanPerda>();
                return _lsttemp;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public List<RingkasanPerda> GetBagianRingkasanPerda(string SSQL)
        {

            List<RingkasanPerda> _lst = new List<RingkasanPerda>();

            try
            {
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RingkasanPerda()
                                {
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    Root = DataFormat.GetInteger(dr["Root"]),
                                    Level = DataFormat.GetInteger(dr["Level"]),
                                    Kelompok = DataFormat.GetInteger(dr["Kelompok"]),
                                    Kode = DataFormat.GetLong(dr["IIDRekening"]).ToKodeRekening(m_ProfileRekening),
                                    Nama = DataFormat.GetString(dr["sNamaRekening"]),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]),
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"]),
                                    Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                    Persen = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]), DataFormat.GetDecimal(dr["JumlahMurni"]))


                                }).ToList();
                    }

                }

                return _lst;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }
        }

        public List<PerdaIII> GetPenjabaran2(ParameterLaporan _p)
        {

            string namaView = CreateViewAllLevel(_p, true);

            List<PerdaIII> _lst = new List<PerdaIII>();
            Single _lastLevel = _p.LastLevel;
            try
            {

                if (_lastLevel == 3)
                {

                    SSQL = "Select 0 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON left(mDasarHukum.IIDRekening,3) =Left(" + namaView + ".IIDRekening,3) " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root < 4 and " + namaView + ".Jumlah>0   ";

                }
                else
                {
                    SSQL = "Select 0 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo," +
                        " '' as Keterangan, 0 as isPokok from " + namaView + "  " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root < 5 and " + namaView + ".Jumlah>0   ";

                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                    }

                    //SSQL = SSQL + " UNION ALL Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo," +
                    //    " mDasarHukum.sKeterangan  as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening =" + namaView + ".IIDRekening " +
                    //    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root =5 and " + namaView + ".Jumlah>0   " +
                    //    " and mdasarHukum.IIDRekening in (select IIDRekening from " + namaView + " where " + namaView + ".IDDinas= " + _p.IDDinas.ToString() + " and mdasarHukum.IIDRekening % 100 >0)";

                    SSQL = SSQL + " UNION ALL Select 0 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo," +
                        " '' as Keterangan, 0 as isPokok from " + namaView +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root =5 and " + namaView + ".Jumlah>0   ";



                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                    }
                    SSQL = SSQL + " UNION ALL Select 0 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 AS iNo," +
                        " (Select mDasarHukum.sKeterangan from mDasarHukum where iidrekening =  (" + namaView + ".IIDRekening/1000)* 1000) as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening/1000 =" + namaView + ".IIDRekening/1000 " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root =5 and " + namaView + ".Jumlah>0   " +
                        "  and mdasarHukum.IIDRekening % 100 =0";

                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                    }

                    //SSQL = SSQL + " UNION ALL Select 0 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 AS iNo," +
                    //" '' as Keterangan, 0 as isPokok from " + namaView + "  " +
                    //" WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root =5 and " + namaView + ".Jumlah>0   " +
                    //"  and (" + namaView + ".IIDRekening/1000) not in (Select IIDRekening/1000 from mDasarHukum)";

                    //if (_p.IDDinas > 0)
                    //{
                    //    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                    //}



                }

                SSQL = SSQL + " UNION ALL    ";

                SSQL = SSQL + " Select 1 as K,2 as btJenis," + namaView + ".IDDInas/10000 as IDUrusan ," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  and " + namaView + ".btJenis in (2,3) and " + namaView + ".Root =1 and " + namaView + ".Jumlah>0    ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY  " + namaView + ".IDDInas," + namaView + ".IIDREKENING,ROOT,sNamaRekening, mDasarHukum.iNo, mDasarHukum.sKeterangan  ";
                SSQL = SSQL + " UNION ALL    ";


                SSQL = SSQL + " Select 1 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis= 2 and " + namaView + ".Root in (2)  and " + namaView + ".Jumlah>0  and " + namaView + ".Root <=" + _lastLevel.ToString();
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " UNION ALL ";


                SSQL = SSQL + " Select 0 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis= 2 and " + namaView + ".Root >2  and " + namaView + ".Jumlah>0  and " + namaView + ".Root <=" + _lastLevel.ToString();
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " UNION ALL ";
                SSQL = SSQL + " Select 1 as K," + namaView + ".btJenis, " + namaView + ".IDDInas/10000 as  IDUrusan," + namaView + ".IDDInas, 1 AS IDProgram , 1 AS IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening , SUM(Jumlah) AS Jumlah, SUM(JumlahMurni) AS JumlahMurni ,mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as IsPokok  from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 3 and Root =2  ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }

                SSQL = SSQL + " GROUP BY " + namaView + ".btJenis," + namaView + ".IDDInas, " + namaView + ".IIDRekening,Root,sNamaRekening,mDasarHukum.iNo,mDasarHukum.sKeterangan ";

                SSQL = SSQL + "  UNION ALL  ";

                SSQL = SSQL + "  Select 1 as K,3 as btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, 0 as IDKegiatan,0 as IIDRekening,0 as Root, A.sNamaProgram as sNamaRekening,    " +
                    "  SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan, mPelaksanaURusan.IsPokok FROM tPrograms_A A   " +
                    " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan    " +
                    " AND A.IDProgram = B.IDProgram   " +
                    " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
                    " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND   B.btJEnis= 3 and b.Root = 5 AND (b.Jumlah>0 or B.JumlahMurni>0)   ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();
                }

                SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram, A.sNamaProgram,mPelaksanaURusan.IsPokok" +
                " UNION ALL    " +
                " Select 1 as K,3 as btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, A.IDkegiatan  as IDKegiatan,0 as IIDRekening,0 as Root, A.sNama as sNamaRekening,   " +
                " SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo, A.sLokasi as Keterangan,mPelaksanaUrusan.IsPokok FROM tKegiatan_A A   " +
                " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan   AND A.IDProgram = B.IDProgram and A.IDkegiatan=B.IDKegiatan  " +
            " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
            " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan    " +
            " WHERE B.iTAhun = " + _p.Tahun.ToString() + " AND B.btJEnis= 3 and B.Root=5 AND (B.Jumlah>0 or B.JumlahMurni>0)";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString();
                    SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok,A.sLokasi  ";

                }
                else
                {
                    SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok,A.sLokasi  ";
                }



                SSQL = SSQL + " UNION ALL  " +
                    " Select 0 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni , 0 as iNo, (Select sSumberDana from tKegiatan_A where iTahun =" + Tahun.ToString() + " and IDDInas =" + namaView + ".IDDInas and IDKegiatan =" + namaView + ".IDKegiatan ) as Keterangan ,mPelaksanaUrusan.IsPokok  from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "   AND  " + namaView + ".btJEnis in ( 3) and Root = 3 and (Jumlah  > 0  or JumlahMurni>0)";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }

                if (_p.LastLevel == 5)
                {


                    SSQL = SSQL + "  UNION ALL  " +
                     " Select 0 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) AS jUMLAH, SUM(JumlahMurni ) AS jUMLAHmURNI, 0 as iNo,'' as Keterangan ,mPelaksanaUrusan.IsPokok  from " + namaView + "  " +
                     " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis in (3) and Root >3 and Root  <6  and (Jumlah  > 0  or JumlahMurni>0)  ";
                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                    }
                    SSQL = SSQL + " group by " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas,  " +
                        " IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, " +
                        " mPelaksanaUrusan.IsPokok ";


                }
                SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan," + namaView + ".IDDInas, IDProgram,IDkegiatan, " + namaView + ".IIDRekening,Root,iNo  ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIII()
                                {
                                    Tahun = _p.Tahun,
                                    Kode = DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                            DataFormat.GetInteger(dr["IDDInas"]),
                                                            DataFormat.GetInteger(dr["IDProgram"]),
                                                            DataFormat.GetInteger(dr["IDkegiatan"]),
                                                            DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening),
                                    Nama = DataFormat.GetString(dr["sNamaRekening"]).Trim(),//DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).Trim() : DataFormat.GetString(dr["sNamaRekening"]).Trim()) : "",
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport(),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"].ToString()).ToRupiahInReport(),
                                    //DataFormat.GetInteger(dr["iNo"]) > 0 ? DataFormat.GetInteger(dr["iNo"]).ToString() + "." : "",
                                    Selisih = (DataFormat.GetDecimal(dr["Jumlah"].ToString()) - DataFormat.GetDecimal(dr["JumlahMurni"].ToString())).ToRupiahInReport(),
                                    Prosentase = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"].ToString()), DataFormat.GetDecimal(dr["JumlahMurni"].ToString())),
                                    label = DataFormat.GetInteger(dr["IDkegiatan"]) > 0 && DataFormat.GetInteger(dr["IIDRekening"]) == 0 ? "Lokasi :" : DataFormat.GetInteger(dr["iNo"]) > 0 ? DataFormat.GetInteger(dr["iNo"]).ToString() : "",

                                    Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                    Level = DataFormat.GetSingle(dr["Root"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    K = DataFormat.GetInteger(dr["K"]),
                                }).ToList();
                        //}                        
                    }
                }

                // Tambahkan rincian 

                //foreach (PerdaIII pIII in _lst)
                int numRecord = _lst.Count;

                for (int i = 0; i < numRecord; i++)
                {

                    if (_lst[i].Level == 5 && _lst[i].IDRekening > 5230000 && DataFormat.GetInteger(_lst[i].IDRekening.ToString().Substring(5, 2)) > 0)
                    {


                        List<PerdaIII> perdaIIIPaket = new List<PerdaIII>();
                        perdaIIIPaket = GetPaket(_p, _lst[i]);
                        if (perdaIIIPaket != null)
                        {
                            //int oldLokasi = 0;
                            //  _lst.InsertRange(i, perdaIIIPaket);

                            foreach (PerdaIII p3 in perdaIIIPaket)
                            {
                                i++;
                                _lst.Insert(i, p3);
                                numRecord++;
                            }
                        }


                    }

                }
                //   if (_p.bPPKD == 1)
                //   {
                for (int i = 0; i < numRecord; i++)
                {

                    if (_lst[i].Level == 5 && _lst[i].IDRekening > 5180000 && DataFormat.GetInteger(_lst[i].IDRekening.ToString().Substring(5, 2)) > 0)
                    {


                        List<PerdaIII> perdaIIIPaket = new List<PerdaIII>();
                        perdaIIIPaket = GetHibah(_p, _lst[i]);
                        if (perdaIIIPaket != null)
                        {
                            foreach (PerdaIII p3 in perdaIIIPaket)
                            {
                                i++;
                                _lst.Insert(i, p3);
                                numRecord++;
                            }
                        }


                    }

                }
                //   }


                if (_p.IDDinas > 0)
                {


                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,7 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                        " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }
                else
                {

                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,7 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                    " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }

                DataTable dtx = new DataTable();
                PerdaIII o = new PerdaIII();

                dtx = _dbHelper.ExecuteDataTable(SSQL);
                if (dtx != null)
                {
                    if (dtx.Rows.Count > 0)
                    {

                        DataRow dr = dtx.Rows[0];

                        o = new PerdaIII()
                        {



                            Tahun = _p.Tahun,
                            Kode = DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                    DataFormat.GetInteger(dr["IDDInas"]),
                                                    DataFormat.GetInteger(dr["IDProgram"]),
                                                    DataFormat.GetInteger(dr["IDkegiatan"]),
                                                    DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening),
                            Nama = DataFormat.GetString(dr["sNamaRekening"]),
                            Jumlah = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])).ToRupiahInReport(),
                            JumlahMurni = (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])).ToRupiahInReport(),

                            Selisih = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"]) - (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"]))).ToRupiahInReport(),
                            Prosentase = DataFormat.GetProsentase((DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])), (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"]))),


                            Keterangan = DataFormat.GetString(dr["Keterangan"]),
                            Level = DataFormat.GetSingle(dr["Root"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"])


                        };

                    }
                }
                _lst.Add(o);
                //g
                int oldKegiatan = 0;
                string oldKeterangan = "";
                long oldIDRekening = 0;
                int JumlahPerdaIII = _lst.Count;
                for (int idx = 0; idx < JumlahPerdaIII; idx++)
                {
                    PerdaIII p3 = _lst[idx];

                    if (p3.IDKegiatan == 0 && p3.Jenis == 1)
                    {
                        List<PerdaIII> pDasarHukum = new List<PerdaIII>();
                        pDasarHukum = GetDasarHukum(_p, p3);
                        int i;
                        i = 0;
                        if (pDasarHukum != null)
                        {
                            if (pDasarHukum.Count > 0)
                            {
                                i++;
                                _lst[idx].label = i.ToString();// pDasarHukum[0].label;
                                _lst[idx].Keterangan = pDasarHukum[0].Keterangan;

                                for (int idxHukum = 1; idxHukum < pDasarHukum.Count; idxHukum++)
                                {
                                    idx++;
                                    i++;
                                    pDasarHukum[idxHukum].label = i.ToString();

                                    _lst.Insert(idx, pDasarHukum[idxHukum]);
                                    JumlahPerdaIII++;

                                }
                            }
                        }


                    }

                    if (p3.IDKegiatan > 0 && (p3.IDKegiatan != oldKegiatan || oldIDRekening != p3.IDRekening) && (p3.Level == 3 || p3.Level == 0))
                    {
                        if (oldIDRekening != p3.IDRekening && p3.IDRekening > 0)
                        {
                            _lst[idx].Keterangan = "";

                        }
                        //else
                        //{
                        if (p3.IDRekening > 0 && p3.Level == 3 && p3.IDKegiatan != oldKegiatan)
                        {
                            oldKeterangan = GetSumberDana(_p, p3);

                            p3.Keterangan = "  " + oldKeterangan;
                            p3.label = "Sumber Dana ";
                            oldIDRekening = p3.IDRekening;
                            oldKegiatan = p3.IDKegiatan;
                        }
                        else
                        {
                            if (oldKeterangan == p3.Keterangan)
                            {
                                p3.Keterangan = "  ";
                                p3.label = "";
                            }


                        }

                        // }


                    }
                    if (p3.IDKegiatan > 0 && p3.IDRekening == 0 && p3.Level == 0)
                    {
                        if (p3.Keterangan.Trim().Length == 0)
                        {
                            //oldKeterangan = _p.NamaDinas;
                            //oldKegiatan = p3.IDKegiatan;
                            _lst[idx].Keterangan = _p.NamaDinas;
                            _lst[idx].label = "Lokasi";
                        }
                    }
                }


                //
                List<PerdaIII> lstpby = new List<PerdaIII>();
                lstpby = GetPenjabarabPembiayaan(_p);
                if (lstpby.Count > 1)
                {
                    foreach (PerdaIII pby in lstpby)
                    {
                        _lst.Add(pby);
                    }
                }
                //}
                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        // *******************************************************************************************
        public List<PerdaIII> GetPenjabaran2020(ParameterLaporan _p)
        {

            string namaView = CreateViewAllLevel(_p, true);
            string namaViewProgram = CreateViewProgram(_p, true);

            string namaViewKegiatan = CreateViewKegiatan(_p, true);


            List<PerdaIII> _lst = new List<PerdaIII>();
            Single _lastLevel = _p.LastLevel;
            try
            {

                SSQL = "Select 0 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening, 0 as Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON left(mDasarHukum.IIDRekening,3) =Left(" + namaView + ".IIDRekening,3) " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root =1  and " + namaView + ".Jumlah>0   ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".Parent=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " UNION ALL Select 0 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening, 1 as Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON left(mDasarHukum.IIDRekening,3) =Left(" + namaView + ".IIDRekening,3) " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root =2  and " + namaView + ".Jumlah>0   ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".Parent=" + _p.IDDinas.ToString();
                }

                if (_lastLevel == 3)
                {

                    SSQL = SSQL + " UNion all Select 0 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON left(mDasarHukum.IIDRekening,3) =Left(" + namaView + ".IIDRekening,3) " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root > 2   and " + namaView + ".Root < 4 and " + namaView + ".Jumlah>0   ";

                }
                else
                {
                    SSQL = SSQL + " UNion all  Select  0 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo," +
                        " '' as Keterangan, 0 as isPokok from " + namaView + "  " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root > 2  and " + namaView + ".Root < 5 and " + namaView + ".Jumlah>0   ";

                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".Parent=" + _p.IDDinas.ToString();
                    }

                    SSQL = SSQL + " UNION ALL Select  0 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo," +
                        " '' as Keterangan, 0 as isPokok from " + namaView +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root =5 and " + namaView + ".Jumlah>0   ";



                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".Parent=" + _p.IDDinas.ToString();
                    }


                }

                SSQL = SSQL + " UNION ALL    ";

                SSQL = SSQL + " Select  1 as K, 2 as btJenis," + namaView + ".Parent/10000 as IDUrusan ," + namaView + ".Parent, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  and " + namaView + ".btJenis in (2,3) and " + namaView + ".Root =1 and  (" + namaView + ".Jumlah>0  or " + namaView + ".JumlahMurni>0)";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".Parent=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY  " + namaView + ".Parent," + namaView + ".IIDREKENING,ROOT,sNamaRekening, mDasarHukum.iNo, mDasarHukum.sKeterangan  ";
                SSQL = SSQL + " UNION ALL    ";




                SSQL = SSQL + " Select  1 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".Parent, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni," +
                " 0 as iNo, '' as Keterangan, 0 as isPokok from " + namaView + "  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis= 2 and " + namaView + ".Root =2  and " + namaView + ".Jumlah>0  and " + namaView + ".Root <=" + _lastLevel.ToString();
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".Parent=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".Parent, " + namaView + ".IIDRekening,Root,sNamaRekening ";

                SSQL = SSQL + " UNION ALL ";

                SSQL = SSQL + " Select  0 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".Parent, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni ,0 as iNo, '' as Keterangan, 0 as isPokok from " + namaView + "  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis= 2 and " + namaView + ".Root > 2  and " + namaView + ".Jumlah>0  and " + namaView + ".Root <=" + _lastLevel.ToString();
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".Parent=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + "GROUP BY " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".Parent, " + namaView + ".IIDRekening,Root,sNamaRekening";

                SSQL = SSQL + " UNION ALL ";

                SSQL = SSQL + " Select 1 as K," + namaView + ".btJenis, " + namaView + ".Parent/10000 as  IDUrusan," + namaView + ".Parent as IDDInas, 1 AS IDProgram , 1 AS IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening , SUM(Jumlah) AS Jumlah, SUM(JumlahMurni) AS JumlahMurni ,0 as iNo, ''  as Keterangan , 0 as IsPokok  from " + namaView + "  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 3 and Root =2  ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".Parent=" + _p.IDDinas.ToString();
                }


                SSQL = SSQL + " GROUP BY " + namaView + ".btJenis," + namaView + ".Parent, " + namaView + ".IIDRekening,Root,sNamaRekening ";



                SSQL = SSQL + "  UNION ALL  ";

                SSQL = SSQL + "  Select 1 as K,3 as btJenis,A.IDUrusan, A.Parent,A.IDProgram, 0 as IDKegiatan,0 as IIDRekening,0 as Root, A.sNamaProgram as sNamaRekening,    " +
                    "  SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan, mPelaksanaURusan.IsPokok FROM " + namaViewProgram + " A   " +
                    " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan    " +
                    " AND A.IDProgram = B.IDProgram   " +
                    " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
                    " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND   B.btJEnis= 3 and b.Root = 5 AND (b.Jumlah>0 or B.JumlahMurni>0)   ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.Parent=" + _p.IDDinas.ToString();
                }

                SSQL = SSQL + " GROUP BY A.IDUrusan, A.Parent,A.IDProgram, A.sNamaProgram,mPelaksanaURusan.IsPokok" +
                " UNION ALL    " +
                " Select 1 as K,3.btJenis,A.IDUrusan, A.Parent,A.IDProgram, A.IDkegiatan  as IDKegiatan,0 as IIDRekening,1 as Root, A.sNama as sNamaRekening,   " +
                " SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo, sLOkasi  as Keterangan,mPelaksanaUrusan.IsPokok FROM " + namaViewKegiatan + " A   " +
                " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan   AND A.IDProgram = B.IDProgram and A.IDkegiatan=B.IDKegiatan  " +
            " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
            " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan    " +
            " WHERE B.iTAhun = " + _p.Tahun.ToString() + " AND B.btJEnis= 3 and B.Root=5 AND (B.Jumlah>0 or B.JumlahMurni>0)";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.Parent=" + _p.IDDinas.ToString();
                    SSQL = SSQL + " GROUP BY A.IDUrusan, A.Parent,A.IDProgram, A.sNama,A.IDkegiatan,A.sLokasi ,mPelaksanaURusan.IsPokok  ";

                }
                else
                {
                    SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDProgram, A.sNama,A.IDkegiatan,A.SLokasi,mPelaksanaURusan.IsPokok";
                }



                SSQL = SSQL + " UNION ALL  " +
                    " Select 0 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".Parent,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni  , 0 as iNo, '' as Keterangan ,mPelaksanaUrusan.IsPokok  from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "   AND  " + namaView + ".btJEnis in ( 3) and Root = 3 and (Jumlah  > 0  or JumlahMurni>0)";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".Parent=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + "Group BY " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".Parent,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening,mPelaksanaUrusan.IsPokok ";
                if (_p.LastLevel == 5)
                {


                    SSQL = SSQL + "  UNION ALL  " +
                     " Select 0 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".Parent,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) AS jUMLAH, SUM(JumlahMurni ) AS jUMLAHmURNI, 0 as iNo,'' as Keterangan ,mPelaksanaUrusan.IsPokok  from " + namaView + "  " +
                     " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis in (3) and Root >3 and Root  <6  and (Jumlah  > 0  or JumlahMurni>0)  ";
                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".Parent=" + _p.IDDinas.ToString();
                    }
                    SSQL = SSQL + " group by " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".Parent,  " +
                        " IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, " +
                        " mPelaksanaUrusan.IsPokok ";


                }
                SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan,IDDinas, IDProgram,IDkegiatan, " + namaView + ".IIDRekening,Root,iNo  ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIII()
                                {
                                    Tahun = _p.Tahun,
                                    Kode = DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                            DataFormat.GetInteger(dr["IDDinas"]),
                                                            DataFormat.GetInteger(dr["IDProgram"]),
                                                            DataFormat.GetInteger(dr["IDkegiatan"]),
                                                            DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening),
                                    Nama = DataFormat.GetString(dr["sNamaRekening"]).Trim(),//DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).Trim() : DataFormat.GetString(dr["sNamaRekening"]).Trim()) : "",
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport(),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"].ToString()).ToRupiahInReport(),
                                    //DataFormat.GetInteger(dr["iNo"]) > 0 ? DataFormat.GetInteger(dr["iNo"]).ToString() + "." : "",
                                    Selisih = (DataFormat.GetDecimal(dr["Jumlah"].ToString()) - DataFormat.GetDecimal(dr["JumlahMurni"].ToString())).ToRupiahInReport(),
                                    Prosentase = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"].ToString()), DataFormat.GetDecimal(dr["JumlahMurni"].ToString())),
                                    label = DataFormat.GetInteger(dr["IDkegiatan"]) > 0 && DataFormat.GetInteger(dr["IIDRekening"]) == 0 ? "Lokasi :" : DataFormat.GetInteger(dr["iNo"]) > 0 ? DataFormat.GetInteger(dr["iNo"]).ToString() : "",

                                    Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                    Level = DataFormat.GetSingle(dr["Root"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    K = DataFormat.GetInteger(dr["K"]),
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekening"])

                                }).ToList();
                        //}                        
                    }
                }

                // Tambahkan rincian 
                if (_p.DenganPaket == 1)
                {
                    //foreach (PerdaIII pIII in _lst)
                    int numRecord = _lst.Count;

                    for (int i = 0; i < numRecord; i++)
                    {

                        if (_lst[i].IDKegiatan == 10302002)
                        {
                            _lst[i].IDKegiatan = 10302002;
                        }

                        //if (_lst[i].Level == 5 && _lst[i].IDRekening > 5230000 && DataFormat.GetInteger(_lst[i].IDRekening.ToString().Substring(5,2) )>0 )
                        if (_lst[i].Level >= 5 && _lst[i].IDRekening > 5230000 && DataFormat.GetInteger(_lst[i].IDRekening.ToString().Substring(5, 2)) > 0)
                        {
                            if (_lst[i].IDKegiatan == 10324010)
                            {
                                _lst[i].IDRekening = _lst[i].IDRekening;

                            }

                            List<PerdaIII> perdaIIIPaket = new List<PerdaIII>();
                            perdaIIIPaket = GetPaket(_p, _lst[i]);
                            if (perdaIIIPaket != null)
                            {
                                //int oldLokasi = 0;
                                //  _lst.InsertRange(i, perdaIIIPaket);

                                foreach (PerdaIII p3 in perdaIIIPaket)
                                {
                                    // if (p3.Nama.Trim() != "")
                                    // {
                                    i++;
                                    _lst.Insert(i, p3);
                                    numRecord++;
                                    //}
                                }
                            }


                        }

                    }
                    //   if (_p.bPPKD == 1)
                    //   {
                    for (int i = 0; i < numRecord; i++)
                    {

                        if (_lst[i].Level == 5 && _lst[i].IDRekening > 5120000 && DataFormat.GetInteger(_lst[i].IDRekening.ToString().Substring(5, 2)) > 0)
                        {


                            List<PerdaIII> perdaIIIPaket = new List<PerdaIII>();
                            perdaIIIPaket = GetHibah(_p, _lst[i]);
                            if (perdaIIIPaket != null)
                            {
                                foreach (PerdaIII p3 in perdaIIIPaket)
                                {
                                    i++;
                                    _lst.Insert(i, p3);
                                    numRecord++;
                                }
                            }


                        }

                    }
                }
                //   }


                if (_p.IDDinas > 0)
                {


                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,7 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND Parent=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND Parent=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND Parent=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND Parent=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }
                else
                {

                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,7 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                    " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }

                DataTable dtx = new DataTable();
                PerdaIII o = new PerdaIII();

                dtx = _dbHelper.ExecuteDataTable(SSQL);
                if (dtx != null)
                {
                    if (dtx.Rows.Count > 0)
                    {

                        DataRow dr = dtx.Rows[0];

                        o = new PerdaIII()
                        {



                            Tahun = _p.Tahun,
                            Kode = DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                    DataFormat.GetInteger(dr["IDDInas"]),
                                                    DataFormat.GetInteger(dr["IDProgram"]),
                                                    DataFormat.GetInteger(dr["IDkegiatan"]),
                                                    DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening),
                            Nama = DataFormat.GetString(dr["sNamaRekening"]),
                            Jumlah = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])).ToRupiahInReport(),
                            JumlahMurni = (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])).ToRupiahInReport(),

                            Selisih = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"]) - (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"]))).ToRupiahInReport(),
                            Prosentase = DataFormat.GetProsentase((DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])), (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"]))),


                            Keterangan = DataFormat.GetString(dr["Keterangan"]),
                            Level = DataFormat.GetSingle(dr["Root"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"])


                        };

                    }
                }
                _lst.Add(o);
                //g
                int oldKegiatan = 0;
                string oldKeterangan = "";
                long oldIDRekening = 0;
                int JumlahPerdaIII = _lst.Count;
                for (int idx = 0; idx < JumlahPerdaIII; idx++)
                {
                    PerdaIII p3 = _lst[idx];

                    if (p3.IDKegiatan == 0 && p3.Jenis == 1)
                    {
                        List<PerdaIII> pDasarHukum = new List<PerdaIII>();
                        pDasarHukum = GetDasarHukum(_p, p3);
                        if (pDasarHukum != null)
                        {
                            if (pDasarHukum.Count > 0)
                            {
                                _lst[idx].label = pDasarHukum[0].label;
                                _lst[idx].Keterangan = pDasarHukum[0].Keterangan;

                                for (int idxHukum = 1; idxHukum < pDasarHukum.Count; idxHukum++)
                                {
                                    idx++;
                                    _lst.Insert(idx, pDasarHukum[idxHukum]);
                                    JumlahPerdaIII++;

                                }
                            }
                        }


                    }

                    if (p3.IDKegiatan > 0 && (p3.IDKegiatan != oldKegiatan || oldIDRekening != p3.IDRekening) && (p3.Level == 3 || p3.Level == 0))
                    {
                        if (oldIDRekening != p3.IDRekening && p3.IDRekening > 0)
                        {
                            _lst[idx].Keterangan = "";

                        }
                        //else
                        //{
                        if (p3.IDRekening > 0 && p3.Level == 3 && p3.IDKegiatan != oldKegiatan)
                        {
                            oldKeterangan = GetSumberDana(_p, p3);

                            p3.Keterangan = "  " + oldKeterangan;
                            p3.label = "Sumber Dana ";
                            oldIDRekening = p3.IDRekening;
                            oldKegiatan = p3.IDKegiatan;
                        }
                        else
                        {
                            if (oldKeterangan == p3.Keterangan)
                            {
                                p3.Keterangan = "  ";
                                p3.label = "";
                            }


                        }

                        // }


                    }
                    if (p3.IDKegiatan > 0 && p3.IDRekening == 0 && p3.Level == 0)
                    {
                        if (p3.Keterangan.Trim().Length == 0)
                        {
                            //oldKeterangan = _p.NamaDinas;
                            //oldKegiatan = p3.IDKegiatan;
                            _lst[idx].Keterangan = _p.NamaDinas;
                            _lst[idx].label = "Lokasi";
                        }
                    }
                }


                //
                List<PerdaIII> lstpby = new List<PerdaIII>();
                lstpby = GetPenjabarabPembiayaan(_p);
                if (lstpby.Count > 1)
                {
                    foreach (PerdaIII pby in lstpby)
                    {
                        _lst.Add(pby);
                    }
                }
                //}
                CreateViewProgram(_p, false);
                CreateViewKegiatan(_p, false);
                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        public List<PerdaIII> GetPenjabaran202090(ParameterLaporan _p)
        {

            string namaView = CreateViewAllLevel(_p, true);
            string namaViewProgram = CreateViewProgram(_p, true);

            string namaViewKegiatan = CreateViewKegiatan(_p, true);
            string namaViewSubKegiatan = CreateViewSubKegiatan(_p, true);

            List<PerdaIII> _lst = new List<PerdaIII>();

            //if (getpaketUmberDana(_p.IDDinas) == false)
            //    return _lst;

            Single _lastLevel = _p.LastLevel;
            SetProfileRekening(mprofile);
            try
            {

                if (_lastLevel == 3)
                {

                    SSQL = "Select 0 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 0 as IDSubKegiatan," + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON left(mDasarHukum.IIDRekening,3) =Left(" + namaView + ".IIDRekening,3) " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root < 4 and " + namaView + ".Jumlah>0   ";

                }
                else
                {
                    SSQL = "Select  0 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 0 as IDSubKegiatan," + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo," +
                        " '' as Keterangan, 0 as isPokok from " + namaView + "  " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root <= 6 and " + namaView + ".Jumlah>0   ";

                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".Parent=" + _p.IDDinas.ToString();
                    }

                    //SSQL = SSQL + " UNION ALL Select  0 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 0 as IDSubKegiatan," + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo," +
                    //    " '' as Keterangan, 0 as isPokok from " + namaView +
                    //    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root =5 and " + namaView + ".Jumlah>0   ";



                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".Parent=" + _p.IDDinas.ToString();
                    }


                }

                SSQL = SSQL + " UNION ALL    ";

                SSQL = SSQL + " Select  1 as K, 2 as btJenis," + namaView + ".Parent/10000 as IDUrusan ," + namaView + ".Parent, 0 AS IDProgram , 0 as IDkegiatan, 0 as IDSubKegiatan,-1 as IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  and " + namaView + ".btJenis in (2,3) and " + namaView + ".Root = 1 and " + namaView + ".Jumlah>0    ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".Parent=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY  " + namaView + ".Parent," + namaView + ".IIDREKENING,ROOT,sNamaRekening, mDasarHukum.iNo, mDasarHukum.sKeterangan  ";


                // Program 


                SSQL = SSQL + "  UNION ALL  ";

                SSQL = SSQL + "  Select 1 as K,B.btJenis,A.IDUrusan, A.Parent,A.IDProgram, 0 as IDKegiatan,0 as IDSubKegiatan,0 as IIDRekening,-1 as Root, A.sNamaProgram as sNamaRekening,    " +
                    "  SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan, mPelaksanaURusan.IsPokok FROM " + namaViewProgram + " A   " +
                    " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan    " +
                    " AND A.IDProgram = B.IDProgram  and a.btjenis= b.btjenis " +
                    " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
                    " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND   B.btJEnis in (2, 3) and b.Root = 5 AND (b.Jumlah>0 or B.JumlahMurni>0)   ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.Parent=" + _p.IDDinas.ToString();

                }

                SSQL = SSQL + " GROUP BY B.btJenis,A.IDUrusan, A.Parent,A.IDProgram, A.sNamaProgram,mPelaksanaURusan.IsPokok" +
                " UNION ALL    " +
                " Select 1 as K,B.btJenis,A.IDUrusan, A.Parent,A.IDProgram, A.IDkegiatan  as IDKegiatan,0 as IDSubKegiatan,0 as IIDRekening,0 as Root, A.sNama as sNamaRekening,   " +
                " SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo, '' as Keterangan,mPelaksanaUrusan.IsPokok FROM " + namaViewKegiatan + " A   " +
                " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan   AND A.IDProgram = B.IDProgram and A.IDkegiatan=B.IDKegiatan  " +
                " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
                " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan    " +
                " WHERE B.iTAhun = " + _p.Tahun.ToString() + " AND B.btJEnis in ( 3) and B.Root=5 AND (B.Jumlah>0 or B.JumlahMurni>0)";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.Parent=" + _p.IDDinas.ToString();
                    SSQL = SSQL + " GROUP BY B.btJenis,A.IDUrusan, A.Parent,A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok  ";

                }
                else
                {
                    SSQL = SSQL + " GROUP BY B.btJenis,A.IDUrusan, A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok";
                }

                SSQL = SSQL + " UNION ALL    " +
                        " Select 1 as K,B.btJenis,A.IDUrusan, A.Parent,A.IDProgram, A.IDkegiatan  as IDKegiatan,A.IDSubKegiatan,0 as IIDRekening,1 as Root, A.Nama as sNamaRekening,   " +
                        " SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo, '' as Keterangan,mPelaksanaUrusan.IsPokok FROM " + namaViewSubKegiatan + " A   " +
                        " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan   AND A.IDProgram = B.IDProgram and A.IDkegiatan=B.IDKegiatan  and A.IDSubKegiatan=B.IDSubKegiatan  " +
                        " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
                        " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan    " +
                        " WHERE B.iTAhun = " + _p.Tahun.ToString() + " AND B.btJEnis in ( 3) and B.Root=5 AND (B.Jumlah>0 or B.JumlahMurni>0)";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.Parent=" + _p.IDDinas.ToString();
                    SSQL = SSQL + " GROUP BY B.btJenis,A.IDUrusan, A.Parent,A.IDProgram, A.Nama,A.IDkegiatan,A.IDSUBKegiatan,mPelaksanaURusan.IsPokok  ";

                }
                else
                {
                    SSQL = SSQL + " GROUP BY B.btJenis,A.IDUrusan, A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok";
                }



                SSQL = SSQL + " UNION ALL  " +
                    " Select 0 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".Parent,  IDProgram , IDkegiatan, IDSubKegiatan," + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni  , 0 as iNo, '' as Keterangan ,mPelaksanaUrusan.IsPokok  from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "   AND  " + namaView + ".btJEnis in ( 3) and Root in(2,3) and (Jumlah  > 0  or JumlahMurni>0)";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".Parent=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + "Group BY " + namaView + ".btJenis , " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".Parent,  IDProgram , IDkegiatan, IDSubKegiatan," + namaView + ".IIDRekening,Root,sNamaRekening,mPelaksanaUrusan.IsPokok ";

                if (_p.LastLevel >= 5)
                {


                    SSQL = SSQL + "  UNION ALL  " +
                     " Select 0 as K," + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".Parent,  IDProgram , IDkegiatan, IDSubKegiatan," + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) AS jUMLAH, SUM(JumlahMurni ) AS jUMLAHmURNI, 0 as iNo,'' as Keterangan ,mPelaksanaUrusan.IsPokok  from " + namaView + "  " +
                     " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis in (2,3) and Root >3 and Root  <=6  and (Jumlah  > 0  or JumlahMurni>0)  ";
                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".Parent=" + _p.IDDinas.ToString();
                    }
                    SSQL = SSQL + " group by " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".Parent,  " +
                        " IDProgram , IDkegiatan,IDSubKegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, " +
                        " mPelaksanaUrusan.IsPokok ";


                }
                SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan, IDProgram,IDkegiatan, IDSubKegiatan," + namaView + ".IIDRekening,Root,iNo  ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIII()
                                {
                                    Tahun = _p.Tahun,
                                    Kode = DataFormat.GetKode90(DataFormat.GetInteger(dr["IDUrusan"]),
                                                            DataFormat.GetInteger(dr["IDDinas"]),
                                                            DataFormat.GetInteger(dr["IDProgram"]),
                                                            DataFormat.GetInteger(dr["IDkegiatan"]), DataFormat.GetLong(dr["IDSubkegiatan"]),
                                                            DataFormat.GetLong(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening),
                                    Nama = DataFormat.GetString(dr["sNamaRekening"]).Trim(),//DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).Trim() : DataFormat.GetString(dr["sNamaRekening"]).Trim()) : "",
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport(),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"].ToString()).ToRupiahInReport(),
                                    //DataFormat.GetInteger(dr["iNo"]) > 0 ? DataFormat.GetInteger(dr["iNo"]).ToString() + "." : "",
                                    Selisih = (DataFormat.GetDecimal(dr["Jumlah"].ToString()) - DataFormat.GetDecimal(dr["JumlahMurni"].ToString())).ToRupiahInReport(),
                                    Prosentase = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"].ToString()), DataFormat.GetDecimal(dr["JumlahMurni"].ToString())),
                                    label = DataFormat.GetInteger(dr["IDkegiatan"]) > 0 && DataFormat.GetInteger(dr["IIDRekening"]) == 0 ? "Lokasi :" : DataFormat.GetInteger(dr["iNo"]) > 0 ? DataFormat.GetInteger(dr["iNo"]).ToString() : "",

                                    Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                    Level = DataFormat.GetSingle(dr["Root"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    K = DataFormat.GetInteger(dr["K"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),

                                }).ToList();
                        //}                        
                    }
                }

                // Tambahkan rincian 

                //foreach (PerdaIII pIII in _lst)
                int numRecord = _lst.Count;

                if (_p.DenganPaket == 1)
                {
                    for (int i = 0; i < numRecord; i++)
                    {



                        //if (_lst[i].Level == 5 && _lst[i].IDRekening > 5230000 && DataFormat.GetInteger(_lst[i].IDRekening.ToString().Substring(5,2) )>0 )
                        if (_lst[i].Level == 6)// > 5200000000 ) //&& DataFormat.GetInteger(_lst[i].IDRekening.ToString().Substring(5, 2)) > 0)
                        {
                            //if (_lst[i].IDKegiatan == 10324010)
                            //{
                            //    _lst[i].IDRekening = _lst[i].IDRekening;

                            //}

                            List<PerdaIII> perdaIIIPaket = new List<PerdaIII>();
                            perdaIIIPaket = GetPaket(_p, _lst[i]);
                            if (perdaIIIPaket != null)
                            {
                                //int oldLokasi = 0;
                                //  _lst.InsertRange(i, perdaIIIPaket);

                                foreach (PerdaIII p3 in perdaIIIPaket)
                                {
                                    i++;
                                    _lst.Insert(i, p3);
                                    numRecord++;
                                }
                            }


                        }


                    }

                    //   if (_p.bPPKD == 1)
                    //   {
                    long rekmodal = 0;
                    if (Tahun < 2021)
                        rekmodal = 5230000;
                    else
                        rekmodal = 5200000000;


                    if (_p.DenganPaket == 1)
                    {
                        for (int i = 0; i < numRecord; i++)
                        {

                            if (_lst[i].Level == 6 && _lst[i].IDRekening > rekmodal) //&& DataFormat.GetInteger(_lst[i].IDRekening.ToString().Substring(5, 2)) > 0)
                            {


                                List<PerdaIII> perdaIIIPaket = new List<PerdaIII>();
                                perdaIIIPaket = GetHibah(_p, _lst[i]);
                                if (perdaIIIPaket != null)
                                {
                                    foreach (PerdaIII p3 in perdaIIIPaket)
                                    {
                                        i++;
                                        _lst.Insert(i, p3);
                                        numRecord++;
                                    }
                                }


                            }

                        }
                    }
                }
                //   }


                if (_p.IDDinas > 0)
                {


                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,7 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND Parent=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000000) as PDPT , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND Parent=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND Parent=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND Parent=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }
                else
                {

                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,7 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                    " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000000) as PDPT , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }

                DataTable dtx = new DataTable();
                PerdaIII o = new PerdaIII();

                dtx = _dbHelper.ExecuteDataTable(SSQL);
                if (dtx != null)
                {
                    if (dtx.Rows.Count > 0)
                    {

                        DataRow dr = dtx.Rows[0];

                        o = new PerdaIII()
                        {



                            Tahun = _p.Tahun,
                            Kode = DataFormat.GetKode90(DataFormat.GetInteger(dr["IDUrusan"]),
                                                    DataFormat.GetInteger(dr["IDDInas"]),
                                                    DataFormat.GetInteger(dr["IDProgram"]),
                                                    DataFormat.GetInteger(dr["IDkegiatan"]), 0,
                                                    DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening),
                            Nama = DataFormat.GetString(dr["sNamaRekening"]),
                            Jumlah = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])).ToRupiahInReport(),
                            JumlahMurni = (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])).ToRupiahInReport(),

                            Selisih = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"]) - (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"]))).ToRupiahInReport(),
                            Prosentase = DataFormat.GetProsentase((DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])), (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"]))),


                            Keterangan = DataFormat.GetString(dr["Keterangan"]),
                            Level = DataFormat.GetSingle(dr["Root"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"])


                        };

                    }
                }
                _lst.Add(o);
                //g
                int oldKegiatan = 0;
                string oldKeterangan = "";
                long oldIDRekening = 0;
                int JumlahPerdaIII = _lst.Count;
                for (int idx = 0; idx < JumlahPerdaIII; idx++)
                {
                    PerdaIII p3 = _lst[idx];

                    if (p3.IDKegiatan == 0 && p3.Jenis == 1)
                    {
                        List<PerdaIII> pDasarHukum = new List<PerdaIII>();
                        pDasarHukum = GetDasarHukum(_p, p3);
                        if (pDasarHukum != null)
                        {
                            if (pDasarHukum.Count > 0)
                            {
                                _lst[idx].label = pDasarHukum[0].label;
                                _lst[idx].Keterangan = pDasarHukum[0].Keterangan;

                                for (int idxHukum = 1; idxHukum < pDasarHukum.Count; idxHukum++)
                                {
                                    idx++;
                                    _lst.Insert(idx, pDasarHukum[idxHukum]);
                                    JumlahPerdaIII++;

                                }
                            }
                        }


                    }

                    if (p3.IDKegiatan > 0 && (p3.IDKegiatan != oldKegiatan || oldIDRekening != p3.IDRekening) && (p3.Level == 3 || p3.Level == 0))
                    {
                        if (oldIDRekening != p3.IDRekening && p3.IDRekening > 0)
                        {
                            _lst[idx].Keterangan = "";

                        }
                        //else
                        //{
                        if (p3.IDRekening > 0 && p3.Level == 3 && p3.IDKegiatan != oldKegiatan)
                        {
                            oldKeterangan = GetSumberDana(_p, p3);

                            p3.Keterangan = "  " + oldKeterangan;
                            p3.label = "Sumber Dana ";
                            oldIDRekening = p3.IDRekening;
                            oldKegiatan = p3.IDKegiatan;
                        }
                        else
                        {
                            if (oldKeterangan == p3.Keterangan)
                            {
                                p3.Keterangan = "  ";
                                p3.label = "";
                            }


                        }

                        // }


                    }
                    if (p3.IDKegiatan > 0 && p3.IDRekening == 0 && p3.Level == 0)
                    {
                        if (p3.Keterangan.Trim().Length == 0)
                        {
                            //oldKeterangan = _p.NamaDinas;
                            //oldKegiatan = p3.IDKegiatan;
                            _lst[idx].Keterangan = _p.NamaDinas;
                            _lst[idx].label = "Lokasi";
                        }
                    }
                }


                //
                List<PerdaIII> lstpby = new List<PerdaIII>();
                lstpby = GetPenjabarabPembiayaan90(_p);
                if (lstpby.Count > 1)
                {
                    foreach (PerdaIII pby in lstpby)
                    {
                        _lst.Add(pby);
                    }
                }
                //}
                CreateViewProgram(_p, false);
                CreateViewKegiatan(_p, false);
                CreateViewSubKegiatan(_p, false);
                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        private bool getpaketUmberDana(int iddinas)
        {
            try
            {
                SSQL = "IF OBJECT_ID('dbo.SumberdanaPaket') IS NOT NULL drop table dbo.SumberdanaPaket";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "select m.id ,m.namarka as namapaket,msd.snama into SumberdanaPaket from perencanaan.dbo.msumberDana msd inner join perencanaan.dbo.musrenbang m on msd.id= m.sumberdana " +
                     " inner join perencanaan.dbo.mskpd mskpd on mskpd.id =m.iddinas  where msd.id <> 2 and mskpd.profile= 1  and  mskpd.parent =" + iddinas.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<PerdaIII> GetPaket(ParameterLaporan _p, PerdaIII _perdaIII)
        {
            List<PerdaIII> _lst = new List<PerdaIII>();

            if (_perdaIII.IDSubKegiatan == 1031020101)
            {
                _perdaIII.IDSubKegiatan = 1031020101;
            }
            if (_p.IDKegiatan == 90216015)
            {
                _p.IDKegiatan = 90216015;
            }
            List<PerdaIII> _extrackedList = new List<PerdaIII>();
            try
            {

                decimal sumRekening = 0L;
                decimal sumRekeningMurni = 0L;

                sumRekening = DataFormat.GetString(_perdaIII.Jumlah).FormatUangReportKeDecimal();
                sumRekeningMurni = DataFormat.GetString(_perdaIII.JumlahMurni).FormatUangReportKeDecimal();
                //SSQL = "SELECT musrenbang.Nama , tAnggaranUraian_A.JumlahMurni, tAnggaranUraian_A.IDLokasi  from TAnggaranUraian_A  " +
                //" INNER JOIN musrenbang On musrenbang.ID= tAnggaranUraian_A.IDLokasi  where TAnggaranUraian_A.IDDInas = " + _p.IDDinas.ToString() +
                //" AND TAnggaranUraian_A.IDKegiatan =" + _perdaIII.IDKegiatan.ToString() + " AND TAnggaranUraian_A.IIDRekening =" + _perdaIII.IDRekening.ToString() +
                //" AND tAnggaranUraian_A.IDLokasi >0 AND tAnggaranUraian_A.VolOlah >0 and tAnggaranUraian_A.JumlahMurni>0  and idStandardHarga='99' ORDER By btUrut";

                if (Tahun < 2021)
                {

                    SSQL = "SELECT cast(tAnggaranUraian_A.suraian as varchar(300)) as Nama , JumlahMurni, JumlahRKAP,tAnggaranUraian_A.IDLokasi, '' as namasumberdana,bturut from TAnggaranUraian_A  " +
                        " where TAnggaranUraian_A.IDDInas = " + _p.IDDinas.ToString() + "  AND TAnggaranUraian_A.IDSUBKEGIATAN =" + _perdaIII.IDSubKegiatan.ToString() +
                        "  AND TAnggaranUraian_A.IDKegiatan =" + _perdaIII.IDKegiatan.ToString() + " AND TAnggaranUraian_A.IIDRekening =" + _perdaIII.IDRekening.ToString() +
                        " AND cast(tAnggaranUraian_A.suraian as varchar(300)) <> '' AND tAnggaranUraian_A.IDLokasi >0 AND cAST(TAnggaranUraian_A.sSatuan AS CHAR(10))='Paket' and (idStandardHarga='99' or idStandardHarga='991') ";

                    SSQL = SSQL + " UNION SELECT cast(tAnggaranUraian_A.suraianOlah as varchar(300)) as nama, JumlahMurni,JumlahRKAP, tAnggaranUraian_A.IDLokasi, '' as namasumberdana ,bturut from TAnggaranUraian_A  " +
                        " where TAnggaranUraian_A.IDDInas = " + _p.IDDinas.ToString() + "  AND TAnggaranUraian_A.IDSUBKEGIATAN =" + _perdaIII.IDSubKegiatan.ToString() +
                        "  AND TAnggaranUraian_A.IDKegiatan =" + _perdaIII.IDKegiatan.ToString() + " AND TAnggaranUraian_A.IIDRekening =" + _perdaIII.IDRekening.ToString() +
                        " AND cast(tAnggaranUraian_A.suraian as varchar(300)) = '' AND tAnggaranUraian_A.IDLokasi >0 AND cAST(TAnggaranUraian_A.sSatuan AS CHAR(10))='Paket' and (idStandardHarga='99' or idStandardHarga='991') ORDER By btUrut";
                }

                else
                    SSQL = "SELECT tAnggaranUraian_A.suraian as nama, JumlahMurni, JumlahRKAP,tAnggaranUraian_A.IDLokasi, SumberdanaPaket.sNama as namasumberdana from TAnggaranUraian_A  " +
                            " left join SumberdanaPaket on SumberdanaPaket.id= tanggaranuraian_a.idlokasi where TAnggaranUraian_A.IDDInas = " + _p.IDDinas.ToString() + "  AND TAnggaranUraian_A.IDSUBKEGIATAN =" + _perdaIII.IDSubKegiatan.ToString() +
                            "  AND TAnggaranUraian_A.IDKegiatan =" + _perdaIII.IDKegiatan.ToString() + " AND TAnggaranUraian_A.IIDRekening =" + _perdaIII.IDRekening.ToString() +
                            " AND tAnggaranUraian_A.IDLokasi >0 AND cAST(TAnggaranUraian_A.sSatuan AS CHAR(10))='Paket' and (idStandardHarga='99' or idStandardHarga='991') ORDER By btUrut";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIII()
                                {
                                    Tahun = _p.Tahun,
                                    Kode = "",
                                    Nama = "",
                                    Jumlah = DataFormat.GetDecimal(dr["JumlahRKAP"]).ToRupiahInReport(),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport(),
                                    Selisih = (DataFormat.GetDecimal(dr["JumlahRKAP"].ToString()) - DataFormat.GetDecimal(dr["JumlahMurni"].ToString())).ToRupiahInReport(),
                                    Prosentase = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["JumlahRKAP"].ToString()), DataFormat.GetDecimal(dr["JumlahMurni"].ToString())),

                                    Keterangan = DataFormat.GetString(dr["Nama"]) + (DataFormat.GetString(dr["namasumberdana"]) == "" ? "" : "     (" + DataFormat.GetString(dr["namasumberdana"]) + ")"),
                                    Level = 6,
                                    Jenis = 0,
                                    IDLokasi = DataFormat.GetInteger(dr["IDLokasi"])

                                }).ToList();


                        int oldLokasi = 0;
                        decimal SumPaket = 0L;
                        decimal supaketurni = 0L;
                        int nourut = 1;
                        List<int> lOldLokasi = new List<int>();
                        for (int idx = 0; idx < _lst.Count; idx++)
                        {
                            if (oldLokasi != _lst[idx].IDLokasi && FIndInList(lOldLokasi, _lst[idx].IDLokasi) == false)
                            {

                                _lst[idx].label = nourut.ToString();
                                _lst[idx].Keterangan = _lst[idx].Keterangan.Replace("Kegiatan Pd", "").Replace("Kegiatan PD", "");
                                _extrackedList.Add(_lst[idx]);
                                oldLokasi = _lst[idx].IDLokasi;
                                lOldLokasi.Add(_lst[idx].IDLokasi);
                                SumPaket = SumPaket + DataFormat.GetString(_lst[idx].Jumlah).FormatUangReportKeDecimal();
                                supaketurni = supaketurni + DataFormat.GetString(_lst[idx].JumlahMurni).FormatUangReportKeDecimal();
                                nourut++;
                            }

                        }
                        if (sumRekening - SumPaket > 0)
                        {
                            PerdaIII pAdm = new PerdaIII();
                            pAdm.Level = 6;
                            pAdm.label = nourut.ToString();
                            pAdm.Keterangan = "Administrasi";
                            pAdm.Jumlah = (sumRekening - SumPaket).ToRupiahInReport();
                            pAdm.JumlahMurni = (sumRekeningMurni - supaketurni).ToRupiahInReport();
                            pAdm.Selisih = ((sumRekening - SumPaket) - (sumRekeningMurni - supaketurni)).ToRupiahInReport();
                            pAdm.Prosentase = DataFormat.GetProsentase((sumRekening - SumPaket), (sumRekeningMurni - supaketurni));

                            _extrackedList.Add(pAdm);
                        }


                    }
                }
                return _extrackedList;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        //private string GetSumberdana(int idlokasi)
        //{

        //    SSQL = "select sNama from msumberdana inner join musrenbang on musrenbang.sumberdana = msumberdana.id  where musrenbang.id= " + idlokasi.ToString();

        //     DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {


        //            }
        //        }

        //}


        //private void LoadLokasi()
        //{


        //    MusrenmbangLogic oLogic = new MusrenmbangLogic(Tahun);
        //    List<Musrenmbang> _lst = new List<Musrenmbang>();
        //    //if (GlobalVar.PP90 == false)
        //    //{


        //    //    _lst = oLogic.GetTanpaKegiatanByIDDInas(m_IDDInas, m_IDUrusan, m_IDProgram, m_IDKegiatan);
        //    //}
        //    //else
        //    //{
        //    RemoteConnection rCon = new RemoteConnection();
        //    RemoteConnectionLogic rConLogic = new RemoteConnectionLogic(2021,3);
        //    rCon = rConLogic.GetByJenis(1, 2);
        //    rCon.Decrypt();

        //    //_lst = oLogic.GetAssignedByIDDInas2021(m_IDDInas,m_IDSubKegiatan,rCon);
        //    int x = 0;

        //    _lst = oLogic.GetAssignedByIDDInas2021(m_IDDInas, m_IDUrusan, m_IDProgram, m_IDKegiatan,
        //                                            m_IDSubKegiatan, rCon, mProfile);
        //    // }
        //    decimal cJumlah = 0L;

        //    gridRincian.Rows.Clear();
        //    if (_lst != null)
        //    {

        //        foreach (Musrenmbang m in _lst)
        //        {


        //            string[] row = { 
        //                            m.id.ToString(),
        //                            m.keteranganlokasi,
        //                            m.nama + " "  +  m.keteranganlokasi,
        //                            m.cRKA.ToRupiahInReport(),m.cRKA.ToRupiahInReport(),
        //                            ">>","","SImpan PAgu"};



        //        }
        //    }

        //}

        public List<PerdaIII> GetHibah(ParameterLaporan _p, PerdaIII _perdaIII)
        {
            List<PerdaIII> _lst = new List<PerdaIII>();
            List<PerdaIII> _extrackedList = new List<PerdaIII>();
            try
            {

                decimal sumRekening = 0L;
                sumRekening = DataFormat.GetString(_perdaIII.Jumlah).FormatUangReportKeDecimal();

                SSQL = "SELECT tANggaranUraian_A.sUraian , tAnggaranUraian_A.JumlahMurni from TAnggaranUraian_A where IDDInas = " + _p.IDDinas.ToString() +
                    " AND IDKegiatan =0 and Jenis = 2   AND IIDRekening =" + _perdaIII.IDRekening.ToString() + " and sUraian like '%Hibah%' ORDER By btUrut";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIII()
                                {
                                    Tahun = _p.Tahun,
                                    Kode = "",
                                    Nama = "",
                                    Jumlah = DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport(),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport(),
                                    Selisih = "0",
                                    Prosentase = "0",
                                    Keterangan = DataFormat.GetString(dr["sUraian"]),
                                    Level = 6,
                                    Jenis = 0,
                                    IDLokasi = 0 //DataFormat.GetInteger(dr["IDLokasi"])

                                }).ToList();


                        int oldLokasi = 0;
                        decimal SumPaket = 0L;
                        int nourut = 1;
                        //    List<int> lOldLokasi = new List<int>();
                        for (int idx = 0; idx < _lst.Count; idx++)
                        {
                            //if (oldLokasi != _lst[idx].IDLokasi && FIndInList(lOldLokasi, _lst[idx].IDLokasi) == false)
                            //{

                            _lst[idx].label = nourut.ToString();
                            _lst[idx].Keterangan = _lst[idx].Keterangan.Replace("Kegiatan Pd", "").Replace("Kegiatan PD", "");
                            _extrackedList.Add(_lst[idx]);
                            //          oldLokasi = _lst[idx].IDLokasi;
                            //         lOldLokasi.Add(_lst[idx].IDLokasi);
                            SumPaket = SumPaket + DataFormat.GetString(_lst[idx].Jumlah).FormatUangReportKeDecimal();
                            nourut++;
                            //}

                        }
                        if (sumRekening - SumPaket > 0)
                        {
                            PerdaIII pAdm = new PerdaIII();
                            pAdm.Level = 6;
                            pAdm.label = nourut.ToString();
                            pAdm.Keterangan = "Administrasi";
                            pAdm.Jumlah = (sumRekening - SumPaket).ToRupiahInReport();
                            _extrackedList.Add(pAdm);
                        }


                    }
                }
                return _extrackedList;
            }
            catch (Exception ex)
            {
                return null;
            }


        }
        public List<PerdaIII> GetDasarHukum(ParameterLaporan _p, PerdaIII _perdaIII)
        {
            List<PerdaIII> _lst = new List<PerdaIII>();

            try
            {
                long idrekbtas;
                if (Tahun < 2021)
                {
                    idrekbtas = 4000000;
                }
                else
                {
                    idrekbtas = 40000000000;
                }
                // --  decimal sumRekening = 0L;
                // -- sumRekening = DataFormat.GetString(_perdaIII.Jumlah).FormatUangReportKeDecimal();
                SSQL = "";

                if (_perdaIII.IDRekening == idrekbtas && _perdaIII.Level == 1)
                {
                    SSQL = "SELECT mDasarHukum.sKeterangan,mDasarHukum.ino from mDasarHukum where iTahun  = " + Tahun.ToString() +
                   " AND IIDRekening=  " + idrekbtas.ToString() + " ORDER By ino";
                }
                else
                {
                    //if (_p.LastLevel == 5 && _perdaIII.Level == 5 && _perdaIII.IDRekening != 4000000)
                    if (_perdaIII.Level >= 5 && _perdaIII.IDRekening != idrekbtas)
                    {
                        SSQL = "SELECT mDasarHukum.sKeterangan,mDasarHukum.ino from mDasarHukum where iTahun  = " + Tahun.ToString() +
                             " AND IIDRekening = " + _perdaIII.IDRekening.ToString() + " ORDER By ino";
                    }
                    else
                    {
                        ////if (_p.LastLevel == 4 && _perdaIII.Level == 4 && _perdaIII.IDRekening != 4000000)
                        //  if (_perdaIII.Level == 4 && _perdaIII.IDRekening != 4000000)

                        //{
                        //    SSQL = "SELECT mDasarHukum.sKeterangan,mDasarHukum.ino from mDasarHukum where iTahun  = " + Tahun.ToString() +
                        //    " AND Left(IIDRekening,5) = Left(" + _perdaIII.IDRekening.ToString() + ",5) ORDER By ino";
                        //}
                        //else
                        //{

                        if (_p.LastLevel == 3 && _perdaIII.Level == 3 && _perdaIII.IDRekening != idrekbtas)
                            SSQL = "SELECT mDasarHukum.sKeterangan,mDasarHukum.ino from mDasarHukum where iTahun  = " + Tahun.ToString() +
                            " AND Left(IIDRekening,3) = " + _perdaIII.IDRekening.ToString().Substring(0, 3) + " ORDER By IIDRekening,ino";
                        //  }
                    }
                }


                if (SSQL == "") return null;
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{

                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIII()
                                {
                                    Tahun = _p.Tahun,
                                    Kode = "",
                                    Nama = "",
                                    Jumlah = "",
                                    JumlahMurni = "",
                                    Selisih = "0",
                                    Prosentase = "0",
                                    Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                    label = DataFormat.GetString(dr["ino"])
                                }).ToList();



                    }

                }
                return _lst;
            }
            catch (Exception ex)
            {
                return null;
            }


        }
        private bool FIndInList(List<int> _l, int toFind)
        {
            foreach (int i in _l)
            {
                if (i == toFind)
                    return true;
            }
            return false;
        }

        //private string GetStringSumberDana()
        //{
        //    string sSumberDana = "";
        //    for (int i = 0; i < gridSumberDana.Rows.Count; i++)
        //    {



        //        DataGridViewRow row = gridSumberDana.Rows[i];
        //        DataGridViewCheckBoxCell chk = row.Cells[1] as DataGridViewCheckBoxCell;
        //        if (chk.Value != null)
        //        {
        //            if (DataFormat.GetBoolean(row.Cells[1].Value) == true)
        //            {
        //                sSumberDana = sSumberDana + DataFormat.GetString((gridSumberDana.Rows[i].Cells[2].Value)) + ",";


        //            }
        //        }
        //    }
        //    if (sSumberDana.Length > 1)
        //    {
        //        if (sSumberDana.Substring(0, sSumberDana.Length) == ",")
        //            sSumberDana = sSumberDana.Substring(0, sSumberDana.Length - 1);
        //    }
        //    return sSumberDana;

        //}
        private string GetSumberDana(ParameterLaporan _p, PerdaIII _pIII)
        {
            TSumberDanaLogic oLogic = new TSumberDanaLogic(_p.Tahun, mprofile);
            List<TSumberDana> _lst = new List<TSumberDana>();
            _lst = oLogic.Get((int)_p.Tahun, _p.IDDinas, _pIII.IDKegiatan);
            string sSumberDana = "";
            int idx = 0;
            if (_lst != null)
            {
                foreach (TSumberDana ts in _lst)
                {

                    if (idx == 0)
                        sSumberDana = sSumberDana + ts.Nama;
                    else
                        sSumberDana = sSumberDana + ", " + ts.Nama;
                    idx++;





                }
            }
            return sSumberDana;

        }


        public List<PerdaIII> GetPenjabarabPembiayaan(ParameterLaporan _p)
        {

            string namaView = CreateViewAllLevel(_p, true);

            List<PerdaIII> _lst = new List<PerdaIII>();
            try
            {

                if (_p.IDDinas > 0)
                {
                    SSQL = "Select " + namaView + ".btJenis," + namaView + ".IDDInas/10000 IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, 0 as Jumlah, 0 as JumlahMurni, mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + " AND  " + namaView + ".btJEnis= 4 and " + namaView + ".Root = 1 and " + namaView + ".Jumlah>0   " +
                        "  ";


                    SSQL = SSQL + " UNION ALL Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + " AND  " + namaView + ".btJEnis= 4 and " + namaView + ".Root > 1 and " + namaView + ".Root <= 5  and " + namaView + ".Jumlah>0   " +
                        " UNION ALL    ";

                    SSQL = SSQL + " Select 5 as btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + "  and " + namaView + ".btJenis =5 and " + namaView + ".Root > 1 and " + namaView + ".Root <= 5  and " + namaView + ".Jumlah>0    " +
                        " GROUP BY " + namaView + ".IDUrusan," + namaView + ".IDDInas," + namaView + ".IIDREKENING,ROOT,sNamaRekening, mDasarHukum.iNo, mDasarHukum.sKeterangan  ";


                    SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan," + namaView + ".IDDInas, IDProgram,IDkegiatan, " + namaView + ".IIDRekening,Root,iNo  ";
                }
                else
                {

                    SSQL = "Select " + namaView + ".btJenis," + namaView + ".IDDInas/10000 IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, 0 as Jumlah, 0 as JumlahMurni, mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 4 and " + namaView + ".Root = 1 and " + namaView + ".Jumlah>0   " +
                        "  ";


                    SSQL = SSQL + " UNION ALL Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".btJEnis= 4 and " + namaView + ".Root > 1 and " + namaView + ".Root < 4  and " + namaView + ".Jumlah>0   " +
                        " UNION ALL    ";

                    SSQL = SSQL + " Select 5 as btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " and " + namaView + ".btJenis =5 and " + namaView + ".Root > 1 and " + namaView + ".Root < 4  and " + namaView + ".Jumlah>0    " +
                        " GROUP BY " + namaView + ".IDUrusan," + namaView + ".IDDInas," + namaView + ".IIDREKENING,ROOT,sNamaRekening, mDasarHukum.iNo, mDasarHukum.sKeterangan  ";


                    SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan, IDProgram,IDkegiatan, " + namaView + ".IIDRekening,Root,iNo  ";

                }

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIII()
                                {
                                    Tahun = _p.Tahun,
                                    Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                                           DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                            DataFormat.GetInteger(dr["IDDInas"]),
                                                            DataFormat.GetInteger(dr["IDProgram"]),
                                                            DataFormat.GetInteger(dr["IDkegiatan"]),
                                                            DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                                    Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                                    Jumlah = DataFormat.GetDecimal(dr["iNo"]) < 2 ? DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport() : "",
                                    JumlahMurni = DataFormat.GetInteger(dr["iNo"]) > 0 ? DataFormat.GetInteger(dr["iNo"]).ToString() + "." : "",
                                    Selisih = "0",
                                    Prosentase = "0",
                                    Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                    Level = DataFormat.GetSingle(dr["Root"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"])

                                }).ToList();
                        //}                        
                    }
                }

                SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,6 as Root,'PEMBIAYAAN NETTO' as sNamaRekening, " +
                    " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=4 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis =5 and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=4 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis =5 and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";

                DataTable dtx = new DataTable();
                PerdaIII o = new PerdaIII();

                dtx = _dbHelper.ExecuteDataTable(SSQL);
                if (dtx != null)
                {
                    if (dtx.Rows.Count > 0)
                    {

                        DataRow dr = dtx.Rows[0];

                        o = new PerdaIII()
                        {
                            Tahun = _p.Tahun,
                            Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                                   DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                    DataFormat.GetInteger(dr["IDDInas"]),
                                                    DataFormat.GetInteger(dr["IDProgram"]),
                                                    DataFormat.GetInteger(dr["IDkegiatan"]),
                                                    DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                            Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                            Jumlah = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])).ToRupiahInReport(),
                            JumlahMurni = (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])).ToRupiahInReport(),
                            Selisih = "0",
                            Prosentase = "0",
                            Keterangan = DataFormat.GetString(dr["Keterangan"]),
                            Level = DataFormat.GetSingle(dr["Root"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"])

                        };

                    }
                }
                _lst.Add(o);
                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        public List<PerdaIII> GetPenjabarabPembiayaan90(ParameterLaporan _p)
        {

            string namaView = CreateViewAllLevel(_p, true);

            List<PerdaIII> _lst = new List<PerdaIII>();
            try
            {

                if (_p.IDDinas > 0)
                {
                    SSQL = "Select " + namaView + ".btJenis," + namaView + ".IDDInas/10000 IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 0 as IDSUBKegiatan," + namaView + ".IIDRekening,Root,sNamaRekening, 0 as Jumlah, 0 as JumlahMurni, mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + " AND  " + namaView + ".btJEnis= 4 and " + namaView + ".Root = 1 and " + namaView + ".Jumlah>0   " +
                        "  ";


                    SSQL = SSQL + " UNION ALL Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 0 as IDSUBKegiatan," + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + " AND  " + namaView + ".btJEnis= 4 and " + namaView + ".Root > 1 and " + namaView + ".Root <= 5  and " + namaView + ".Jumlah>0   " +
                        " UNION ALL    ";

                    SSQL = SSQL + " Select 5 as btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,0 as IDSUBKegiatan,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + "  and " + namaView + ".btJenis =5 and " + namaView + ".Root > 1 and " + namaView + ".Root <= 5  and " + namaView + ".Jumlah>0    " +
                        " GROUP BY " + namaView + ".IDUrusan," + namaView + ".IDDInas," + namaView + ".IIDREKENING,ROOT,sNamaRekening, mDasarHukum.iNo, mDasarHukum.sKeterangan  ";


                    SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan," + namaView + ".IDDInas, IDProgram,IDkegiatan, " + namaView + ".IIDRekening,Root,iNo  ";
                }
                else
                {

                    SSQL = "Select " + namaView + ".btJenis," + namaView + ".IDDInas/10000 IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan,0 as IDSUBKegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, 0 as Jumlah, 0 as JumlahMurni, mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 4 and " + namaView + ".Root = 1 and " + namaView + ".Jumlah>0   " +
                        "  ";


                    SSQL = SSQL + " UNION ALL Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan,0 as IDSUBKegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".btJEnis= 4 and " + namaView + ".Root > 1 and " + namaView + ".Root < 4  and " + namaView + ".Jumlah>0   " +
                        " UNION ALL    ";

                    SSQL = SSQL + " Select 5 as btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan,0 as IDSUBKegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " and " + namaView + ".btJenis =5 and " + namaView + ".Root > 1 and " + namaView + ".Root < 4  and " + namaView + ".Jumlah>0    " +
                        " GROUP BY " + namaView + ".IDUrusan," + namaView + ".IDDInas," + namaView + ".IIDREKENING,ROOT,sNamaRekening, mDasarHukum.iNo, mDasarHukum.sKeterangan  ";


                    SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan, IDProgram,IDkegiatan, " + namaView + ".IIDRekening,Root,iNo  ";

                }

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIII()
                                {
                                    Tahun = _p.Tahun,
                                    Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                                           DataFormat.GetKode90(DataFormat.GetInteger(dr["IDUrusan"]),
                                                            DataFormat.GetInteger(dr["IDDInas"]),
                                                            DataFormat.GetInteger(dr["IDProgram"]),
                                                            DataFormat.GetInteger(dr["IDkegiatan"]), 0,
                                                            DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                                    Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                                    Jumlah = DataFormat.GetDecimal(dr["iNo"]) < 2 ? DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport() : "",
                                    JumlahMurni = DataFormat.GetInteger(dr["iNo"]) > 0 ? DataFormat.GetInteger(dr["iNo"]).ToString() + "." : "",
                                    Selisih = "0",
                                    Prosentase = "0",
                                    Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                    Level = DataFormat.GetSingle(dr["Root"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"])

                                }).ToList();
                        //}                        
                    }
                }

                SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan,0 as IDSUbKegiatan, 9999999  as IIDRekening,6 as Root,'PEMBIAYAAN NETTO' as sNamaRekening, " +
                    " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=4 and IIDRekening> 4000000000) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis =5 and IIDRekening> 4000000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=4 and IIDRekening> 4000000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis =5 and IIDRekening> 4000000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";

                DataTable dtx = new DataTable();
                PerdaIII o = new PerdaIII();

                dtx = _dbHelper.ExecuteDataTable(SSQL);
                if (dtx != null)
                {
                    if (dtx.Rows.Count > 0)
                    {

                        DataRow dr = dtx.Rows[0];

                        o = new PerdaIII()
                        {
                            Tahun = _p.Tahun,
                            Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                                   DataFormat.GetKode90(DataFormat.GetInteger(dr["IDUrusan"]),
                                                    DataFormat.GetInteger(dr["IDDInas"]),
                                                    DataFormat.GetInteger(dr["IDProgram"]),
                                                    DataFormat.GetInteger(dr["IDkegiatan"]), 0,
                                                    DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                            Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                            Jumlah = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])).ToRupiahInReport(),
                            JumlahMurni = (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])).ToRupiahInReport(),
                            Selisih = "0",
                            Prosentase = "0",
                            Keterangan = DataFormat.GetString(dr["Keterangan"]),
                            Level = DataFormat.GetSingle(dr["Root"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"])

                        };

                    }
                }
                _lst.Add(o);
                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++PENJABARAN
        public List<PerdaIII> GetPenjabaran1a(ParameterLaporan _p)
        {

            string namaView = CreateViewAllLevel(_p, true);
            string _lastLevel = _p.LastLevel.ToString();
            List<PerdaIII> _lst = new List<PerdaIII>();
            try
            {

                SSQL = "Select " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni,0 as iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root <4 and (" + namaView + ".Jumlah>0  or " + namaView + ".JumlahMurni>0 )  ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening,mDasarHukum.iNo,mDasarHukum.sKeterangan ";

                // level 4
                SSQL = SSQL + "UNION ALL Select " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root =4 and (" + namaView + ".Jumlah> 0   or " + namaView + ".JumlahMurni>0 ) ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening,mDasarHukum.iNo,mDasarHukum.sKeterangan ";

                // level 5 yang hukum level 4

                SSQL = SSQL + "Union all Select " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni,0 as iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening/1000=" + namaView + ".IIDRekening/1000 " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root = 5 and (" + namaView + ".Jumlah>0    or " + namaView + ".JumlahMurni>0 )   " +
                    " AND mDasarHukum.IIDrekening % 1000 = 0 AND " + namaView + ".IIDrekening in (Select IIDrekening from tAnggaranRekening_A where iTahun = " + _p.Tahun.ToString() + "  and tAnggaranRekening_A.IDDInas =" + namaView + ".IDDInas  ) ";


                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening,mDasarHukum.iNo,mDasarHukum.sKeterangan ";


                // level 5 yang hukum level 5
                SSQL = SSQL + "Union ALL Select " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni,0 as iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                      " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root = 5 and (" + namaView + ".Jumlah>0   or " + namaView + ".JumlahMurni>0 )   " +
                      " AND mDasarHukum.IIDrekening % 1000 > 0 AND " + namaView + ".IIDrekening in (Select IIDrekening from tAnggaranRekening_A where iTahun =" + namaView + ".iTAhun and tAnggaranRekening_A.IDDInas =" + namaView + ".IDDInas  ) ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening,mDasarHukum.iNo,mDasarHukum.sKeterangan ";

                // tidak ada hukumnya

                SSQL = SSQL + "Union all Select " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok from " + namaView + "  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root = 5 and ( " + namaView + ".Jumlah>0 or " + namaView + ".JumlahMurni>0 )   " +
                    " AND " + namaView + ".IIDrekening/1000 not in (Select IIDrekening/1000 from mDasarHukum ) ";


                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening";








                SSQL = SSQL + " UNION ALL    ";




                SSQL = SSQL + " Select 2 as btJenis, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, 0 as iNo, '' as Keterangan , 0 as isPokok from " + namaView +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  and " + namaView + ".btJenis in (2,3) and " + namaView + ".Root =1 and (" + namaView + ".Jumlah>0  or " + namaView + ".JumlahMurni>0 )   ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY " + namaView + ".IIDRekening,Root,sNamaRekening";



                SSQL = SSQL + " UNION ALL    ";
                // SSQL = SSQL + " UNION ALL ";

                SSQL = SSQL + " Select " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni,mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis in  (2,3) and " + namaView + ".Root >1  and (" + namaView + ".Jumlah>0  or " + namaView + ".JumlahMurni>0 )   and " + namaView + ".Root <= " + _lastLevel + "   ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening,mDasarHukum.iNo,mDasarHukum.sKeterangan ";
                /*
                SSQL = SSQL + " UNION ALL ";
                SSQL = SSQL + " Select " + namaView + ".btJenis, " + namaView + ".IIDRekening,Root,sNamaRekening , SUM(Jumlah) AS Jumlah, SUM(JumlahMurni) AS JumlahMurni ,0 as iNo, '' as Keterangan , 0 as IsPokok  from " + namaView +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 3 and Root =2  ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }

                SSQL = SSQL + " GROUP BY " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening";
                */
                //    SSQL = SSQL + "  UNION ALL  ";

                //    SSQL = SSQL + "  Select 3 as btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, 0 as IDKegiatan,0 as IIDRekening,0 as Root, A.sNamaProgram as sNamaRekening,    " +
                //        "  SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan, mPelaksanaURusan.IsPokok FROM tPrograms_A A   " +
                //        " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan    " +
                //        " AND A.IDProgram = B.IDProgram   " +
                //        " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
                //        " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND   B.btJEnis= 3 and b.Root = 5 AND (b.Jumlah>0 or B.JumlahMurni>0)   ";
                //    if (_p.IDDinas > 0)
                //    {
                //        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                //    }

                //    SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram, A.sNamaProgram,mPelaksanaURusan.IsPokok" +
                //    " UNION ALL    " +
                //    " Select 3.btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, A.IDkegiatan  as IDKegiatan,0 as IIDRekening,0 as Root, A.sNama as sNamaRekening,   " +
                //    " SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan,mPelaksanaUrusan.IsPokok FROM tKegiatan_A A   " +
                //    " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan   AND A.IDProgram = B.IDProgram and A.IDkegiatan=B.IDKegiatan  " +
                //" INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
                //" AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan    " +
                //" WHERE B.iTAhun = " + _p.Tahun.ToString() + " AND B.btJEnis= 3 and B.Root=5 AND (B.Jumlah>0 or B.JumlahMurni>0)";
                //    if (_p.IDDinas > 0)
                //    {
                //        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                //        SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok  ";

                //    }
                //    else
                //    {
                //        SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok  ";
                //    }



                //if (_p.LastLevel == 3)
                //{
                //    SSQL = SSQL + " UNION ALL  " +
                //        " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni , mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan ,mPelaksanaUrusan.IsPokok  from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                //        " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "   AND  " + namaView + ".btJEnis in ( 3) and Root = 3 and (Jumlah  > 0  or JumlahMurni>0)";
                //    if (_p.IDDinas > 0)
                //    {
                //        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                //    }

                //}
                //else
                //{

                //    SSQL = SSQL + "  UNION ALL  " +
                //    " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni , mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan ,mPelaksanaUrusan.IsPokok  from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                //    " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis in (3) and Root = 5 and (Jumlah  > 0  or JumlahMurni>0)  ";
                //    if (_p.IDDinas > 0)
                //    {
                //        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                //    }

                //}
                SSQL = SSQL + "  order by btJenis,Ispokok," + namaView + ".IIDRekening,Root,iNo  ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIII()
                                {
                                    Tahun = _p.Tahun,
                                    Kode = DataFormat.ToKodeRekening(DataFormat.GetInteger(dr["IIDRekening"]), m_ProfileRekening),
                                    Nama = DataFormat.GetString(dr["sNamaRekening"]).Trim(),// DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).Trim() : DataFormat.GetString(dr["sNamaRekening"]).Trim()) : "",
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport(),
                                    Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                    Prosentase = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]), DataFormat.GetDecimal(dr["JumlahMurni"])),
                                    Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                    Level = DataFormat.GetSingle(dr["Root"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"])

                                }).ToList();
                        //}                        
                    }
                }

                if (_p.IDDinas > 0)
                {


                    SSQL = "  Select 4 as btJenis, 9999999  as IIDRekening,6 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                        " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }
                else
                {

                    SSQL = "  Select 4 as btJenis, 9999999  as IIDRekening,6 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                    " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }

                DataTable dtx = new DataTable();
                PerdaIII o = new PerdaIII();

                dtx = _dbHelper.ExecuteDataTable(SSQL);
                if (dtx != null)
                {
                    if (dtx.Rows.Count > 0)
                    {

                        DataRow dr = dtx.Rows[0];

                        o = new PerdaIII()
                        {
                            Tahun = _p.Tahun,
                            Kode = DataFormat.ToKodeRekening(DataFormat.GetInteger(dr["IIDRekening"]), m_ProfileRekening),
                            Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                            Jumlah = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])).ToRupiahInReport(),
                            JumlahMurni = (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])).ToRupiahInReport(),
                            Selisih = ((DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])) - (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"]))).ToRupiahInReport(),
                            Prosentase = DataFormat.GetProsentase((DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])), (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"]))),
                            Keterangan = DataFormat.GetString(dr["Keterangan"]),
                            Level = DataFormat.GetSingle(dr["Root"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"])

                        };

                    }
                }
                _lst.Add(o);

                // Pembiayaan 
                //if (_p.IDDinas == 4040601){
                List<PerdaIII> lstpby = new List<PerdaIII>();
                lstpby = GetPenjabaranIIIPembiayaan(_p);
                if (lstpby.Count > 1)
                {
                    foreach (PerdaIII pby in lstpby)
                    {
                        _lst.Add(pby);
                    }
                }
                //}
                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        public List<PerdaIII> GetPenjabaranIIIPembiayaan(ParameterLaporan _p)
        {

            string namaView = CreateViewAllLevel(_p, true);

            List<PerdaIII> _lst = new List<PerdaIII>();
            try
            {

                if (_p.IDDinas > 0)
                {
                    SSQL = "Select " + namaView + ".btJenis," + namaView + ".IDDInas/10000 IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, 0 as Jumlah, 0 as JumlahMurni, mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + " AND  " + namaView + ".btJEnis= 4 and " + namaView + ".Root = 1 and " + namaView + ".Jumlah>0   " +
                        "  ";


                    SSQL = SSQL + " UNION ALL Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + " AND  " + namaView + ".btJEnis= 4 and " + namaView + ".Root > 1 and " + namaView + ".Root < 4  and " + namaView + ".Jumlah>0   " +
                        " UNION ALL    ";

                    SSQL = SSQL + " Select 5 as btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + "  and " + namaView + ".btJenis =5 and " + namaView + ".Root > 1 and " + namaView + ".Root < 4  and " + namaView + ".Jumlah>0    " +
                        " GROUP BY " + namaView + ".IDUrusan," + namaView + ".IDDInas," + namaView + ".IIDREKENING,ROOT,sNamaRekening, mDasarHukum.iNo, mDasarHukum.sKeterangan  ";


                    SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan," + namaView + ".IDDInas, IDProgram,IDkegiatan, " + namaView + ".IIDRekening,Root,iNo  ";
                }
                else
                {

                    SSQL = "Select " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening, 0 as Jumlah, 0 as JumlahMurni, mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 4 and " + namaView + ".Root = 1 and " + namaView + ".Jumlah>0   " +
                        "  ";


                    SSQL = SSQL + " UNION ALL Select " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".btJEnis= 4 and " + namaView + ".Root > 1 and " + namaView + ".Root < 6  and " + namaView + ".Jumlah>0   " +
                        " UNION ALL    ";

                    SSQL = SSQL + " Select 5 as btJenis," + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " and " + namaView + ".btJenis =5 and " + namaView + ".Root > 1 and " + namaView + ".Root < 6  and " + namaView + ".Jumlah>0    " +
                        " GROUP BY " + namaView + ".IIDREKENING,ROOT,sNamaRekening, mDasarHukum.iNo, mDasarHukum.sKeterangan  ";


                    SSQL = SSQL + "  order by " + namaView + ".IIDRekening,Root,iNo  ";

                }
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIII()
                                {
                                    Tahun = _p.Tahun,
                                    Kode = DataFormat.ToKodeRekening(DataFormat.GetInteger(dr["IIDRekening"]), m_ProfileRekening),
                                    Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                                    Jumlah = DataFormat.GetDecimal(dr["iNo"]) < 2 ? DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport() : "",
                                    JumlahMurni = DataFormat.GetDecimal(dr["iNo"]) < 2 ? DataFormat.GetDecimal(dr["JumlahMurni"].ToString()).ToRupiahInReport() : "",
                                    Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                    Prosentase = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]), DataFormat.GetDecimal(dr["JumlahMurni"])),
                                    Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                    Level = DataFormat.GetSingle(dr["Root"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"])

                                }).ToList();
                        //}                        
                    }
                }

                SSQL = "  Select 4 as btJenis, 9999999  as IIDRekening,6 as Root,'PEMBIAYAAN NETTO' as sNamaRekening, " +
                    " ((SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + "  and Root = 1 and btJenis=4 and IIDRekening> 4000000) -  " +
                    " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + "  and Root = 1 and btJenis=5 and IIDRekening> 4000000) ) as JumlahMurni , " +
                       "  ((SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + "  and Root = 1 and btJenis =4 and IIDRekening> 4000000) - " +
                " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + "  and Root = 1 and btJenis =5 and IIDRekening> 4000000) ) as Jumlah ,  " +
                      " 0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";

                DataTable dtx = new DataTable();
                PerdaIII o = new PerdaIII();

                dtx = _dbHelper.ExecuteDataTable(SSQL);
                if (dtx != null)
                {
                    if (dtx.Rows.Count > 0)
                    {

                        DataRow dr = dtx.Rows[0];

                        o = new PerdaIII()
                        {
                            Tahun = _p.Tahun,
                            Kode = DataFormat.ToKodeRekening(DataFormat.GetInteger(dr["IIDRekening"]), m_ProfileRekening),
                            Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                            Jumlah = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
                            JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport(),
                            Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                            Prosentase = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["Jumlah"]), DataFormat.GetDecimal(dr["JumlahMurni"])),
                            Keterangan = DataFormat.GetString(dr["Keterangan"]),
                            Level = DataFormat.GetSingle(dr["Root"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"])

                        };

                    }
                }
                _lst.Add(o);

                SSQL = "  Select 4 as btJenis, 9999999  as IIDRekening,6 as Root,'SISA LEBIH PEMBIAYAAN ANGGARAN TAHUN BERKENAN' as sNamaRekening, " +
                 " ((SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + "  and Root = 1 and btJenis in(1,4)  and IIDRekening> 4000000) -  " +
                 " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + "  and Root = 1 and btJenis in (2,3,5) and IIDRekening> 4000000) ) as JumlahMurni , " +
                    "  ((SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + "  and Root = 1 and btJenis in (1,4) and IIDRekening> 4000000) - " +
             " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + "  and Root = 1 and btJenis in (2,3,5) and IIDRekening> 4000000) ) as Jumlah ,  " +
                   " 0 as iNo,''  as Keterangan , " +
                    " 3 as isPokok  ";



                //SSQL = "  Select 4 as btJenis, 9999999  as IIDRekening,6 as Root,'SISA LEBIH PEMBIAYAAN ANGGARAN TAHUN BERKENAN' as sNamaRekening, " +
                //       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + "  and Root = 1 and btJenis in (1,4) and IIDRekening> 4000000) as Jumlah ,  " +
                //       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis in (1,4) and IIDRekening> 4000000)  as JumlahMurni,0 as iNo,''  as Keterangan , " +
                //       " 3 as isPokok  ";

                DataTable dtox = new DataTable();
                PerdaIII ox = new PerdaIII();

                dtox = _dbHelper.ExecuteDataTable(SSQL);
                if (dtox != null)
                {
                    if (dtox.Rows.Count > 0)
                    {

                        DataRow dr = dtox.Rows[0];

                        ox = new PerdaIII()
                        {
                            Tahun = _p.Tahun,
                            Kode = DataFormat.ToKodeRekening(DataFormat.GetInteger(dr["IIDRekening"]), m_ProfileRekening),
                            Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                            Jumlah = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
                            JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport(),
                            Selisih = "0",
                            Prosentase = "0",
                            Keterangan = DataFormat.GetString(dr["Keterangan"]),
                            Level = DataFormat.GetSingle(dr["Root"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"])

                        };

                    }
                }
                _lst.Add(ox);

                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }

        public List<RingkasanPerda> GetRingkasanPerdaRealisasi(int _iTahun, Single _iTahap, ParameterLaporan _p)
        {

            List<RingkasanPerda> _lsttemp = new List<RingkasanPerda>();
            List<RingkasanPerda> _lst = new List<RingkasanPerda>();
            try
            {


                GetKolom(_p.Tahap);
                string sNamaView;
                string justrek;
                if (_p.Tahun <= 2020)
                {
                    sNamaView = CreateViewRealisasiAllLevel(_p, true);//nmnbnbono
                    justrek = "3000000";
                }
                else
                {
                    sNamaView = CreateViewRealisasiAllLevelpp77(_p, true);
                    justrek = "300000000000";
                }
                List<RingkasanPerda> _lstPendapatan = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlPendapatan = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaTidakLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlBelanjaTidakLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlBelanjaLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel1 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel2 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel3 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel4 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel5 = new List<RingkasanPerda>();

                string namaKolomdiView1 = "Jumlah";
                string namaKolomdiView2 = "JumlahMurni";
                int b = 0;


                if (_p.LastLevel == 3)
                    SSQL = "SELECT 1 as Kelompok,0 as Level, Root,IIDrekening-" + justrek + " as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun=  " + _iTahun.ToString() + "  AND  btJEnis= 1 and Root<4  GROUP BY Root,IIDrekening, sNamaRekening ";
                else
                    SSQL = "SELECT 1 as Kelompok,0 as Level, Root,IIDrekening-" + justrek + "  as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun=  " + _iTahun.ToString() + "  AND  btJEnis= 1 and Root<6  GROUP BY Root,IIDrekening, sNamaRekening ";
                _lstPendapatan = GetBagianRingkasanPerdaRealisasi(SSQL);
                _lst = _lstPendapatan;
                b = 1;

                b = 2;
                SSQL = " SELECT 2 as Kelompok,0 as Level, Root, IIDrekening-" + justrek + "  as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3)  and Root =1  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel1 = GetBagianRingkasanPerdaRealisasi(SSQL);
                foreach (RingkasanPerda p in _lstBelanjaLevel1)
                {
                    _lst.Add(p);
                }
                b = 3;
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + justrek + "  as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =2  and Root=2  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel2 = GetBagianRingkasanPerdaRealisasi(SSQL);

                foreach (RingkasanPerda p in _lstBelanjaLevel2)
                {
                    _lst.Add(p);
                }
                b = 4;
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + justrek + "  as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =2  and Root=3  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";

                _lstBelanjaLevel3 = GetBagianRingkasanPerdaRealisasi(SSQL);

                foreach (RingkasanPerda p in _lstBelanjaLevel3)
                {
                    _lst.Add(p);
                }

                if (_p.LastLevel == 5)
                {
                    b = 5;
                    SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + justrek + "  as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (3)  and Root=3  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel4 = GetBagianRingkasanPerdaRealisasi(SSQL);

                    foreach (RingkasanPerda p in _lstBelanjaLevel4)
                    {
                        _lst.Add(p);
                    }
                    b = 6;
                    SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + justrek + "  as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (3)  and Root=3  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel5 = GetBagianRingkasanPerdaRealisasi(SSQL);
                    foreach (RingkasanPerda p in _lstBelanjaLevel5)
                    {
                        _lst.Add(p);
                    }
                }

                //b = 7;

                //b = 8;
                //SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-" + justrek + "  as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =3  and Root=2  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                //_lstBelanjaLevel2 = GetBagianRingkasanPerdaRealisasi(SSQL);

                //foreach (RingkasanPerda p in _lstBelanjaLevel2)
                //{
                //    _lst.Add(p);
                //}

                //b = 9;
                //SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-" + justrek + "  as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =3  and Root=3  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";

                //_lstBelanjaLevel3 = GetBagianRingkasanPerdaRealisasi(SSQL);
                //foreach (RingkasanPerda p in _lstBelanjaLevel3)
                //{
                //    _lst.Add(p);
                //}
                //if (_p.LastLevel == 5)
                //{
                //    b = 10;
                //    SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-" + justrek + "  as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =3  and Root=4  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                //    _lstBelanjaLevel4 = GetBagianRingkasanPerdaRealisasi(SSQL);
                //    foreach (RingkasanPerda p in _lstBelanjaLevel4)
                //    {
                //        _lst.Add(p);
                //    }
                //    b = 11;
                //    SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-" + justrek + "  as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =3  and Root=5  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                //    _lstBelanjaLevel5 = GetBagianRingkasanPerdaRealisasi(SSQL);
                //    foreach (RingkasanPerda p in _lstBelanjaLevel5)
                //    {
                //        _lst.Add(p);
                //    }
                //}

                //b = 11;
                SSQL = " SELECT 5 as Kelompok,1 as Level,1 as Root, 0 as IIDRekening, 'SURPLUS/DEFISIT' as sNamaRekening," +
                    " (SELECT SUM(" + namaKolomdiView1 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1) and Root=1 ) - " +
                    " (SELECT SUM(" + namaKolomdiView1 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1 )  as JumlahMurni, " +
                    " (SELECT SUM(" + namaKolomdiView2 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1) and Root=1 ) - " +
                    " (SELECT SUM(" + namaKolomdiView2 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1 )  as Jumlah";


                //" SELECT SUM(" + namaKolomdiView1 + ") as JumlahMurni,SUM(" + namaKolomdiView2 + ") as Jumlah from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1) and Root=1 AND IIDrekening > 4000000 ) - " +
                //    " - SUM(" + namaKolomdiView1 + ") as JumlahMurni,SUM(" + namaKolomdiView2 + ") as Jumlah from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1 AND IIDrekening > 4000000 ) ";

                _JmlBelanjaLangsung = GetBagianRingkasanPerdaRealisasi(SSQL);

                foreach (RingkasanPerda p in _JmlBelanjaLangsung)
                {
                    _lst.Add(p);
                }

                var cats = from c in _lst
                             .OrderBy(i => i.Kelompok).ThenBy(i => i.IDRekening)
                           select c;
                _lsttemp = cats.ToList<RingkasanPerda>();


                // CreateViewAllLevel(_p,false);

                List<RingkasanPerda> lstPembiayaan = new List<RingkasanPerda>();

                lstPembiayaan = GetRingkasanPerdaRealisasiPembiayaan(_iTahun, _iTahap, _p, sNamaView);
                foreach (RingkasanPerda p in lstPembiayaan)
                {
                    _lsttemp.Add(p);
                }



                return _lsttemp;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }


            //    if (_p.LastLevel == 3)
            //        SSQL = "SELECT 1 as Kelompok,0 as Level, Root,IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun=  " + _iTahun.ToString() + "  AND  btJEnis= 1 and Root<4  AND IIDrekening > 4000000 GROUP BY Root,IIDrekening, sNamaRekening ";
            //    else
            //        SSQL = "SELECT 1 as Kelompok,0 as Level, Root,IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun=  " + _iTahun.ToString() + "  AND  btJEnis= 1 and Root<6  AND IIDrekening > 4000000 GROUP BY Root,IIDrekening, sNamaRekening ";

            //    _lstPendapatan = GetBagianRingkasanPerda(SSQL);
            //    _lst = _lstPendapatan;

            //    SSQL = " SELECT 2 as Kelompok,1 as Level,6  as Root, 0 as IIDRekening, 'JUMLAH PENDAPATAN' as sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis= 1  and Root=1 AND IIDrekening > 4000000  ";
            //    _JmlPendapatan = GetBagianRingkasanPerda(SSQL);
            //    foreach (RingkasanPerda p in _JmlPendapatan)
            //    {
            //        _lst.Add(p);
            //    }
            //    SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =2  and Root=1  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
            //    _lstBelanjaLevel1 = GetBagianRingkasanPerda(SSQL);
            //    foreach (RingkasanPerda p in _lstBelanjaLevel1)
            //    {
            //        _lst.Add(p);
            //    }

            //    SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =2  and Root=2  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
            //    _lstBelanjaLevel2 = GetBagianRingkasanPerda(SSQL);

            //    foreach (RingkasanPerda p in _lstBelanjaLevel2)
            //    {
            //        _lst.Add(p);
            //    }
            //    SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =2  and Root=3  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";

            //    _lstBelanjaLevel3 = GetBagianRingkasanPerda(SSQL);
            //    foreach (RingkasanPerda p in _lstBelanjaLevel3)
            //    {
            //        _lst.Add(p);
            //    }

            //    if (_p.LastLevel == 5)
            //    {
            //        SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2)  and Root=4  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
            //        _lstBelanjaLevel4 = GetBagianRingkasanPerda(SSQL);

            //        foreach (RingkasanPerda p in _lstBelanjaLevel4)
            //        {
            //            _lst.Add(p);
            //        }

            //        SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2)  and Root=5  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
            //        _lstBelanjaLevel5 = GetBagianRingkasanPerda(SSQL);
            //        foreach (RingkasanPerda p in _lstBelanjaLevel5)
            //        {
            //            _lst.Add(p);
            //        }
            //    }

            //    SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =3  and Root=1  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
            //    _lstBelanjaLevel1 = GetBagianRingkasanPerda(SSQL);
            //    foreach (RingkasanPerda p in _lstBelanjaLevel1)
            //    {
            //        _lst.Add(p);
            //    }

            //    SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =3  and Root=2  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
            //    _lstBelanjaLevel2 = GetBagianRingkasanPerda(SSQL);

            //    foreach (RingkasanPerda p in _lstBelanjaLevel2)
            //    {
            //        _lst.Add(p);
            //    }

            //    SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =3  and Root=3  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";

            //    _lstBelanjaLevel3 = GetBagianRingkasanPerda(SSQL);
            //    foreach (RingkasanPerda p in _lstBelanjaLevel3)
            //    {
            //        _lst.Add(p);
            //    }
            //    if (_p.LastLevel == 5)
            //    {
            //        SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =3  and Root=4  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
            //        _lstBelanjaLevel4 = GetBagianRingkasanPerda(SSQL);
            //        foreach (RingkasanPerda p in _lstBelanjaLevel4)
            //        {
            //            _lst.Add(p);
            //        }
            //        SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =3  and Root=5  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
            //        _lstBelanjaLevel5 = GetBagianRingkasanPerda(SSQL);
            //        foreach (RingkasanPerda p in _lstBelanjaLevel5)
            //        {
            //            _lst.Add(p);
            //        }
            //    }

            //    SSQL = " SELECT 5 as Kelompok,1 as Level,6 as Root, 0 as IIDRekening, 'JUMLAH BELANJA' as sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni,SUM(cDPA) as Jumlah from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1 AND IIDrekening > 4000000  ";
            //    _JmlBelanjaLangsung = GetBagianRingkasanPerda(SSQL);
            //    foreach (RingkasanPerda p in _JmlBelanjaLangsung)
            //    {
            //        _lst.Add(p);
            //    }
            //    SSQL = " SELECT 6 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =4  and Root=1  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
            //    _lstBelanjaLevel1 = GetBagianRingkasanPerda(SSQL);
            //    foreach (RingkasanPerda p in _lstBelanjaLevel1)
            //    {
            //        _lst.Add(p);
            //    }
            //    SSQL = " SELECT 6 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =4  and Root=2  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
            //    _lstBelanjaLevel2 = GetBagianRingkasanPerda(SSQL);
            //    foreach (RingkasanPerda p in _lstBelanjaLevel2)
            //    {
            //        _lst.Add(p);
            //    }


            //    SSQL = " SELECT 6 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =4  and Root=3  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
            //    _lstBelanjaLevel3 = GetBagianRingkasanPerda(SSQL);
            //    foreach (RingkasanPerda p in _lstBelanjaLevel3)
            //    {
            //        _lst.Add(p);
            //    }

            //    if (_p.LastLevel == 5)
            //    {


            //        SSQL = " SELECT 6 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =4  and Root=4  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
            //        _lstBelanjaLevel4 = GetBagianRingkasanPerda(SSQL);
            //        foreach (RingkasanPerda p in _lstBelanjaLevel4)
            //        {
            //            _lst.Add(p);
            //        }


            //        SSQL = " SELECT 6 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =4  and Root=5  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
            //        _lstBelanjaLevel5 = GetBagianRingkasanPerda(SSQL);
            //        foreach (RingkasanPerda p in _lstBelanjaLevel5)
            //        {
            //            _lst.Add(p);
            //        }
            //    }

            //    SSQL = " SELECT 7 as Kelompok,1 as Level,6 as Root, 0 as IIDRekening, 'JUMLAH PENERIMAAN PEMBIAYAAN' as sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni,SUM(cDPA) as Jumlah from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =4  and Root=1 AND IIDrekening > 4000000  ";
            //    _JmlBelanjaLangsung = GetBagianRingkasanPerda(SSQL);
            //    foreach (RingkasanPerda p in _JmlBelanjaLangsung)
            //    {
            //        _lst.Add(p);
            //    }


            //    SSQL = " SELECT 8 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =5  and Root=1  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
            //    _lstBelanjaLevel1 = GetBagianRingkasanPerda(SSQL);
            //    foreach (RingkasanPerda p in _lstBelanjaLevel1)
            //    {
            //        _lst.Add(p);
            //    }
            //    SSQL = " SELECT 8 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =5  and Root=2  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
            //    _lstBelanjaLevel2 = GetBagianRingkasanPerda(SSQL);
            //    foreach (RingkasanPerda p in _lstBelanjaLevel2)
            //    {
            //        _lst.Add(p);
            //    }

            //    SSQL = " SELECT 8 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =5  and Root=3  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
            //    _lstBelanjaLevel2 = GetBagianRingkasanPerda(SSQL);
            //    foreach (RingkasanPerda p in _lstBelanjaLevel2)
            //    {
            //        _lst.Add(p);
            //    }

            //    if (_p.LastLevel == 5)
            //    {


            //        SSQL = " SELECT 8 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =5  and Root=4  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
            //        _lstBelanjaLevel4 = GetBagianRingkasanPerda(SSQL);
            //        foreach (RingkasanPerda p in _lstBelanjaLevel4)
            //        {
            //            _lst.Add(p);
            //        }

            //        SSQL = " SELECT 8 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =5  and Root=5  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
            //        _lstBelanjaLevel5 = GetBagianRingkasanPerda(SSQL);
            //        foreach (RingkasanPerda p in _lstBelanjaLevel5)
            //        {
            //            _lst.Add(p);
            //        }

            //    }


            //    SSQL = " SELECT 9 as Kelompok,1 as Level,6 as Root, 0 as IIDRekening, 'JUMLAH PENGELUARAN PEMBIAYAAN' as sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni,SUM(cDPA) as Jumlah from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =5  and Root=1 AND IIDrekening > 4000000  ";
            //    _JmlBelanjaLangsung = GetBagianRingkasanPerda(SSQL);
            //    foreach (RingkasanPerda p in _JmlBelanjaLangsung)
            //    {
            //        _lst.Add(p);
            //    }



            //    var cats = from c in _lst
            //                 .OrderBy(i => i.Kelompok).ThenBy(i => i.IDRekening)
            //               select c;
            //    //.ThenBy(i => i.IDRekening)
            //    _lsttemp = cats.ToList<RingkasanPerda>();
            //    return _lsttemp;
            //    /*
            //                    SSQL = SSQL + " ORDER BY Kelompok, IIDRekening ";

            //                    DataTable dt = new DataTable();
            //                    dt = _dbHelper.ExecuteDataTable(SSQL);
            //                    if (dt != null)
            //                    {
            //                        if (dt.Rows.Count > 0)
            //                        {
            //                           _lst = (from DataRow dr in dt.Rows
            //                                   select new RingkasanPerda()
            //                                   {

            //                                       Root = DataFormat.GetInteger(dr["Root"]),
            //                                       Level = DataFormat.GetSingle(dr["Level"]),
            //                                       Kelompok= DataFormat.GetSingle(dr["Kelompok"]),
            //                                       Kode= DataFormat.GetLong(dr["IIDRekening"]).ToKodeRekening(m_ProfileRekening),
            //                                       Nama = DataFormat.GetString(dr["sNamaRekening"]),                               
            //                                       JumlahMurni = DataFormat.GetDecimal(dr[_namaKolom1]),
            //                                       Jumlah = DataFormat.GetDecimal(dr[_namaKolom2])


            //                                   }).ToList();
            //                            } 

            //                    }

            //                    return _lst;*/
            //}
            //catch (Exception ex)
            //{
            //    _lastError = ex.Message;
            //    return null;
            //}


        }

        public List<RingkasanPerda> GetRingkasanPerdaRealisasi2021(int _iTahun, Single _iTahap, ParameterLaporan _p)
        {

            List<RingkasanPerda> _lsttemp = new List<RingkasanPerda>();
            List<RingkasanPerda> _lst = new List<RingkasanPerda>();
            try
            {


                GetKolom(_p.Tahap);
                string sNamaView = CreateViewRealisasiAllLevel(_p, true);//nmnbnbono

                List<RingkasanPerda> _lstPendapatan = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlPendapatan = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaTidakLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlBelanjaTidakLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlBelanjaLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel1 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel2 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel3 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel4 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel5 = new List<RingkasanPerda>();

                string namaKolomdiView1 = "JumlahMurni";
                string namaKolomdiView2 = "Jumlah";

                int b = 0;
                if (_p.LastLevel == 3)
                    SSQL = "SELECT 1 as Kelompok,0 as Level, Root,IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun=  " + _iTahun.ToString() + "  AND  btJEnis= 1 and Root<4  GROUP BY Root,IIDrekening, sNamaRekening ";
                else
                    SSQL = "SELECT 1 as Kelompok,0 as Level, Root,IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun=  " + _iTahun.ToString() + "  AND  btJEnis= 1 and Root<6  GROUP BY Root,IIDrekening, sNamaRekening ";
                //SSQL = "SELECT 1 as Kelompok,0 as Level, Root,IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun=  " + _iTahun.ToString() + "  AND  btJEnis= 1 and Root<6  GROUP BY Root,IIDrekening, sNamaRekening ";

                _lstPendapatan = GetBagianRingkasanPerdaRealisasi(SSQL);
                _lst = _lstPendapatan;
                b = 1;

                b = 2;
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3)  and Root =1  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel1 = GetBagianRingkasanPerdaRealisasi(SSQL);
                foreach (RingkasanPerda p in _lstBelanjaLevel1)
                {
                    _lst.Add(p);
                }
                b = 3;
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =2  and Root=2  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel2 = GetBagianRingkasanPerdaRealisasi(SSQL);

                foreach (RingkasanPerda p in _lstBelanjaLevel2)
                {
                    _lst.Add(p);
                }
                b = 4;
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =2  and Root=3  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";

                _lstBelanjaLevel3 = GetBagianRingkasanPerdaRealisasi(SSQL);
                foreach (RingkasanPerda p in _lstBelanjaLevel3)
                {
                    _lst.Add(p);
                }

                if (_p.LastLevel == 5)
                {
                    b = 5;
                    SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2)  and Root=4  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel4 = GetBagianRingkasanPerdaRealisasi(SSQL);

                    foreach (RingkasanPerda p in _lstBelanjaLevel4)
                    {
                        _lst.Add(p);
                    }
                    b = 6;
                    SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2)  and Root=5  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel5 = GetBagianRingkasanPerdaRealisasi(SSQL);
                    foreach (RingkasanPerda p in _lstBelanjaLevel5)
                    {
                        _lst.Add(p);
                    }
                }

                b = 7;

                b = 8;
                SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =3  and Root=2  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel2 = GetBagianRingkasanPerdaRealisasi(SSQL);

                foreach (RingkasanPerda p in _lstBelanjaLevel2)
                {
                    _lst.Add(p);
                }

                b = 9;
                SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =3  and Root=3  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";

                _lstBelanjaLevel3 = GetBagianRingkasanPerdaRealisasi(SSQL);
                foreach (RingkasanPerda p in _lstBelanjaLevel3)
                {
                    _lst.Add(p);
                }
                if (_p.LastLevel == 5)
                {
                    b = 10;
                    SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =3  and Root=4  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel4 = GetBagianRingkasanPerdaRealisasi(SSQL);
                    foreach (RingkasanPerda p in _lstBelanjaLevel4)
                    {
                        _lst.Add(p);
                    }
                    b = 11;
                    SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =3  and Root=5  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel5 = GetBagianRingkasanPerdaRealisasi(SSQL);
                    foreach (RingkasanPerda p in _lstBelanjaLevel5)
                    {
                        _lst.Add(p);
                    }
                }

                //b = 11;
                SSQL = " SELECT 5 as Kelompok,1 as Level,1 as Root, 0 as IIDRekening, 'SURPLUS/DEFISIT' as sNamaRekening," +
                    " (SELECT SUM(" + namaKolomdiView1 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1) and Root=1 ) - " +
                    " (SELECT SUM(" + namaKolomdiView1 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1 )  as JumlahMurni, " +
                    " (SELECT SUM(" + namaKolomdiView2 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1) and Root=1 ) - " +
                    " (SELECT SUM(" + namaKolomdiView2 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1 )  as Jumlah";


                //" SELECT SUM(" + namaKolomdiView1 + ") as JumlahMurni,SUM(" + namaKolomdiView2 + ") as Jumlah from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1) and Root=1 AND IIDrekening > 4000000 ) - " +
                //    " - SUM(" + namaKolomdiView1 + ") as JumlahMurni,SUM(" + namaKolomdiView2 + ") as Jumlah from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1 AND IIDrekening > 4000000 ) ";

                _JmlBelanjaLangsung = GetBagianRingkasanPerdaRealisasi(SSQL);
                foreach (RingkasanPerda p in _JmlBelanjaLangsung)
                {
                    _lst.Add(p);
                }

                var cats = from c in _lst
                             .OrderBy(i => i.Kelompok).ThenBy(i => i.IDRekening)
                           select c;
                _lsttemp = cats.ToList<RingkasanPerda>();


                // CreateViewAllLevel(_p,false);

                List<RingkasanPerda> lstPembiayaan = new List<RingkasanPerda>();

                lstPembiayaan = GetRingkasanPerdaRealisasiPembiayaan(_iTahun, _iTahap, _p, sNamaView);
                foreach (RingkasanPerda p in lstPembiayaan)
                {
                    _lsttemp.Add(p);
                }



                return _lsttemp;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }

        }
        public void UpdateRealisasiGabungan()
        {

            List<TAnggaranRekening> lst = new List<TAnggaranRekening>();
            SSQL = " UPDATE tanggaranrekening_A SET cRealisasi = 0 WHERE iddinas in (4010500,4012500,4012500,4012600,4012700,4013000,4013100)  ";
            _dbHelper.ExecuteNonQuery(SSQL);


            SSQL = "select iTahun,btkodekategori ,btkodeurusan ,btkodeSKPD ,btKodekategoripelaksana,btKodeurusanpelaksana ,btkodeuk,btidprogram," +
                  " btidkegiatan ,IIDRekening  from tanggaranrekening_A WHERE iddinas in (4010500,4012500,4012500,4012600,4012700,4013000,4013100)" +
                  " ORDER BY tanggaranrekening_A.iTahun,tanggaranrekening_A.btkodekategori ,tanggaranrekening_A.btkodeurusan " +
            ",tanggaranrekening_A.btkodeSKPD ,tanggaranrekening_A.btKodekategoripelaksana ,tanggaranrekening_A.btKodeurusanpelaksana,tanggaranrekening_A.btidprogram," +
                  " tanggaranrekening_A.btidkegiatan ,tanggaranrekening_A.IIDRekening,tanggaranrekening_A.btKodeUK ";


            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL);
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    lst = (from DataRow dr in dt.Rows
                           select new TAnggaranRekening()
                           {
                               IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                               KodeKategori = DataFormat.GetInteger(dr["btKodekategori"]),
                               KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                               KodeSKPD = DataFormat.GetInteger(dr["btKodeskpd"]),
                               KodeUK = DataFormat.GetInteger(dr["btKodeuk"]),

                               KodeProgram = DataFormat.GetInteger(dr["btidprogram"]),
                               KodeKegiatan = DataFormat.GetInteger(dr["btidKegiatan"]),
                               KodeKategoriPelaksana = DataFormat.GetInteger(dr["btKodekategoripelaksana"]),

                               KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanpelaksana"]),

                           }).ToList();
                }
            }
            TAnggaranRekening olta = new TAnggaranRekening();
            foreach (TAnggaranRekening ta in lst)
            {

                //if (ta.KodeKategori != olta.KodeKategori && ta.KodeUrusan != olta.KodeUrusan && ta.KodeUrusan != olta.KodeSKPD && ta.KodeUK != olta.KodeUK && 
                //    ta.KodeKategoriPelaksana != olta.KodeKategoriPelaksana && ta.KodeUrusanPelaksana != olta.KodeUrusanPelaksana && ta.KodeProgram != olta.KodeProgram &&
                //      ta.KodeKegiatan != olta.KodeKegiatan && ta.IDRekening != olta.IDRekening)
                if (ta.KodeKategori != olta.KodeKategori || ta.KodeUrusan != olta.KodeUrusan || ta.KodeSKPD != olta.KodeSKPD ||
                    ta.KodeKategoriPelaksana != olta.KodeKategoriPelaksana || ta.KodeUrusanPelaksana != olta.KodeUrusanPelaksana || ta.KodeProgram != olta.KodeProgram ||
                      ta.KodeKegiatan != olta.KodeKegiatan || ta.IDRekening != olta.IDRekening)
                {
                    SSQL = "update tAnggaranRekening_A set cRealisasi = (select SUM (VR.DEBET * VR.cJumlah) from  Realisasi04Perda VR " +
                  " where iTahun= 2020 and  VR.btKodekategori = " + ta.KodeKategori.ToString() +
                  " and  VR.btKodeurusan  = " + ta.KodeUrusan.ToString() +
                  " and  VR.btKodeskpd  = " + ta.KodeSKPD.ToString() +
                  " and  VR.btKodeurusanpelaksana  = " + ta.KodeUrusanPelaksana.ToString() +
                  " and  VR.btKodekategoripelaksana = " + ta.KodeKategoriPelaksana.ToString() +
                  " and  VR.btIDprogram  = " + ta.KodeProgram.ToString() +
                  " and  VR.btIDkegiatan= " + ta.KodeKegiatan.ToString() +
                  " and  VR.IIDRekening = " + ta.IDRekening.ToString() + ")  where iTahun= 2020 and  btKodekategori = " + ta.KodeKategori.ToString() +
                  " and  btKodeurusan  = " + ta.KodeUrusan.ToString() +
                  " and  btKodeskpd  = " + ta.KodeSKPD.ToString() +
                  " and  btKOdeuk = " + ta.KodeUK.ToString() +
                  " and  btKodeurusanpelaksana  = " + ta.KodeUrusanPelaksana.ToString() +
                  " and  btKodekategoripelaksana = " + ta.KodeKategoriPelaksana.ToString() +
                  " and  btIDprogram  = " + ta.KodeProgram.ToString() +
                  " and  btIDkegiatan= " + ta.KodeKegiatan.ToString() +
                  " and  IIDRekening = " + ta.IDRekening.ToString();

                    _dbHelper.ExecuteNonQuery(SSQL);

                    olta.KodeKategori = ta.KodeKategori;
                    olta.KodeUrusan = ta.KodeUrusan;
                    olta.KodeSKPD = ta.KodeSKPD;
                    olta.KodeUK = ta.KodeUK;
                    olta.KodeKategoriPelaksana = ta.KodeKategoriPelaksana;
                    olta.KodeUrusanPelaksana = ta.KodeUrusanPelaksana;
                    olta.KodeProgram = ta.KodeProgram;
                    olta.KodeKegiatan = ta.KodeKegiatan;
                    olta.IDRekening = ta.IDRekening;


                }

            }



        }
        public void UpdateRealisasiGabunganBenuaKayong()
        {

            List<TAnggaranRekening> lst = new List<TAnggaranRekening>();
            SSQL = " UPDATE tanggaranrekening_A SET cRealisasi = 0 WHERE iddinas in (4010800,4012800,4012900,4013200,4013300)  ";
            _dbHelper.ExecuteNonQuery(SSQL);


            SSQL = "select iTahun,btkodekategori ,btkodeurusan ,btkodeSKPD ,btKodekategoripelaksana,btKodeurusanpelaksana ,btkodeuk,btidprogram," +
                  " btidkegiatan ,IIDRekening  from tanggaranrekening_A WHERE iddinas in (4010800,4012800,4012900,4013200,4013300)" +
                  " ORDER BY tanggaranrekening_A.iTahun,tanggaranrekening_A.btkodekategori ,tanggaranrekening_A.btkodeurusan " +
            ",tanggaranrekening_A.btkodeSKPD ,tanggaranrekening_A.btKodekategoripelaksana ,tanggaranrekening_A.btKodeurusanpelaksana,tanggaranrekening_A.btidprogram," +
                  " tanggaranrekening_A.btidkegiatan ,tanggaranrekening_A.IIDRekening,tanggaranrekening_A.btKodeUK ";


            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL);
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    lst = (from DataRow dr in dt.Rows
                           select new TAnggaranRekening()
                           {
                               IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                               KodeKategori = DataFormat.GetInteger(dr["btKodekategori"]),
                               KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                               KodeSKPD = DataFormat.GetInteger(dr["btKodeskpd"]),
                               KodeUK = DataFormat.GetInteger(dr["btKodeuk"]),

                               KodeProgram = DataFormat.GetInteger(dr["btidprogram"]),
                               KodeKegiatan = DataFormat.GetInteger(dr["btidKegiatan"]),
                               KodeKategoriPelaksana = DataFormat.GetInteger(dr["btKodekategoripelaksana"]),

                               KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanpelaksana"]),

                           }).ToList();
                }
            }
            TAnggaranRekening olta = new TAnggaranRekening();
            foreach (TAnggaranRekening ta in lst)
            {

                //if (ta.KodeKategori != olta.KodeKategori && ta.KodeUrusan != olta.KodeUrusan && ta.KodeUrusan != olta.KodeSKPD && ta.KodeUK != olta.KodeUK && 
                //    ta.KodeKategoriPelaksana != olta.KodeKategoriPelaksana && ta.KodeUrusanPelaksana != olta.KodeUrusanPelaksana && ta.KodeProgram != olta.KodeProgram &&
                //      ta.KodeKegiatan != olta.KodeKegiatan && ta.IDRekening != olta.IDRekening)
                if (ta.KodeKategori != olta.KodeKategori || ta.KodeUrusan != olta.KodeUrusan || ta.KodeSKPD != olta.KodeSKPD ||
                    ta.KodeKategoriPelaksana != olta.KodeKategoriPelaksana || ta.KodeUrusanPelaksana != olta.KodeUrusanPelaksana || ta.KodeProgram != olta.KodeProgram ||
                      ta.KodeKegiatan != olta.KodeKegiatan || ta.IDRekening != olta.IDRekening)
                {
                    SSQL = "update tAnggaranRekening_A set cRealisasi = (select SUM (VR.DEBET * VR.cJumlah) from  Realisasi04Perda VR " +
                  " where iTahun= 2020 and  VR.btKodekategori = " + ta.KodeKategori.ToString() +
                  " and  VR.btKodeurusan  = " + ta.KodeUrusan.ToString() +
                  " and  VR.btKodeskpd  = " + ta.KodeSKPD.ToString() +
                  " and  VR.btKodeurusanpelaksana  = " + ta.KodeUrusanPelaksana.ToString() +
                  " and  VR.btKodekategoripelaksana = " + ta.KodeKategoriPelaksana.ToString() +
                  " and  VR.btIDprogram  = " + ta.KodeProgram.ToString() +
                  " and  VR.btIDkegiatan= " + ta.KodeKegiatan.ToString() +
                  " and  VR.IIDRekening = " + ta.IDRekening.ToString() + ")  where iTahun= 2020 and  btKodekategori = " + ta.KodeKategori.ToString() +
                  " and  btKodeurusan  = " + ta.KodeUrusan.ToString() +
                  " and  btKodeskpd  = " + ta.KodeSKPD.ToString() +
                  " and  btKOdeuk = " + ta.KodeUK.ToString() +
                  " and  btKodeurusanpelaksana  = " + ta.KodeUrusanPelaksana.ToString() +
                  " and  btKodekategoripelaksana = " + ta.KodeKategoriPelaksana.ToString() +
                  " and  btIDprogram  = " + ta.KodeProgram.ToString() +
                  " and  btIDkegiatan= " + ta.KodeKegiatan.ToString() +
                  " and  IIDRekening = " + ta.IDRekening.ToString();

                    _dbHelper.ExecuteNonQuery(SSQL);

                    olta.KodeKategori = ta.KodeKategori;
                    olta.KodeUrusan = ta.KodeUrusan;
                    olta.KodeSKPD = ta.KodeSKPD;
                    olta.KodeUK = ta.KodeUK;
                    olta.KodeKategoriPelaksana = ta.KodeKategoriPelaksana;
                    olta.KodeUrusanPelaksana = ta.KodeUrusanPelaksana;
                    olta.KodeProgram = ta.KodeProgram;
                    olta.KodeKegiatan = ta.KodeKegiatan;
                    olta.IDRekening = ta.IDRekening;


                }

            }



        }
        public void UpdateRealisasi(DateTime d)
        {


            // CreateViewRealisasiPerRekening();
            try
            {

                //SSQL = " Update tAnggaranRekening_A SET cRealisasi =0 where iTahun  =" + Tahun.ToString() + "and btJenis in (1,4)";
                //_dbHelper.ExecuteNonQuery(SSQL);


                SSQL = " Update tAnggaranRekening_A SET cRealisasi =0  where  iTahun  =" + Tahun.ToString() + " and btJenis in (1,2,3,5)";
                _dbHelper.ExecuteNonQuery(SSQL);

                //SSQL = " update tanggaranrekening_A set cRealisasi= (select sum(Realisasi04ak.Debet * Realisasi04ak.cJumlah) " +
                //      " from Realisasi04ak  where iTahun= tAnggaranRekening_A.iTahun and  Realisasi04ak.btKodekategori = tAnggaranRekening_A.btKodekategori and  " +
                //        " Realisasi04ak.btKodeUrusan = tAnggaranRekening_A.btKodeUrusan " +
                //        " and  Realisasi04ak.btKodekategoriPelaksana = tAnggaranRekening_A.btKodekategoriPelaksana and  " +
                //        " Realisasi04ak.btKodeUrusanPelaksana = tAnggaranRekening_A.btKodeUrusanPelaksana and  " +
                //        " Realisasi04ak.btIDProgram = tAnggaranRekening_A.btIDProgram and  " +
                //        " Realisasi04ak.btIDKegiatan = tAnggaranRekening_A.btIDKegiatan and  " +
                //        " Realisasi04ak.btIDSubKegiatan = tAnggaranRekening_A.btIDSubKegiatan and  " +
                //        " Realisasi04ak.btKodeSKPD = tAnggaranRekening_A.btKodeSKPD and  " +
                //        " Realisasi04ak.IIDRekening = tAnggaranRekening_A.IIDRekening and Realisasi04ak.dtBukukas <= " + d.ToSQLFormat() + " and iTahun =" + Tahun.ToString() + ")  where btJenis in (2,3,5)";

                SSQL = " update tanggaranrekening_A set cRealisasi= (select sum(Realisasi04ak.Debet * Realisasi04ak.cJumlah) " +
                     " from Realisasi04ak  where iTahun= tAnggaranRekening_A.iTahun and  "+
                       " Realisasi04ak.IDDInas  = 1020100  " +
                        " and UnitAnggaran=1 "+
                       "  and Realisasi04ak.IDSUbKegiatan  = tAnggaranRekening_A.IDSUbKegiatan AND "+
                       " Realisasi04ak.IIDRekening = tAnggaranRekening_A.IIDRekening and Realisasi04ak.dtBukukas <= " +
                       d.ToSQLFormat() + " and iTahun =" + Tahun.ToString() + ")  where IDDINAS=1020100 and btJenis in (2,3,5) and UnitANggaran=1 ";


                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = " update tanggaranrekening_A set cRealisasi= (select sum(Realisasi04ak.Debet * Realisasi04ak.cJumlah) " +
                     " from Realisasi04ak  where iTahun= tAnggaranRekening_A.iTahun and  " +
                       " Realisasi04ak.IDDInas  = tAnggaranRekening_A.iddinas  and  " +

                       "  Realisasi04ak.IDSUbKegiatan  = tAnggaranRekening_A.IDSUbKegiatan AND " +
                       " Realisasi04ak.IIDRekening = tAnggaranRekening_A.IIDRekening and Realisasi04ak.dtBukukas <= " +
                       d.ToSQLFormat() + " and iTahun =" + Tahun.ToString() + ")  where IDDINAS <> 1020100 and  btJenis in (2,3,5) ";//and btkodeUK=1 ";


                _dbHelper.ExecuteNonQuery(SSQL);

                //SSQL = " update tanggaranrekening_A set cRealisasi= (select sum(Realisasi04ak.Debet * Realisasi04ak.cJumlah) " +
                //     " from Realisasi04ak  where iTahun= tAnggaranRekening_A.iTahun and  " +
                //       " Realisasi04ak.IDDInas  = tAnggaranRekening_A.Iddinas " +
                //       " and Realisasi04ak.iddinas <>1020100  " +
                //       " and  Realisasi04ak.IDSUbKegiatan  = tAnggaranRekening_A.IDSUbKegiatan AND " +
                //       " Realisasi04ak.IIDRekening = tAnggaranRekening_A.IIDRekening and Realisasi04ak.dtBukukas <= " + d.ToSQLFormat() + " and iTahun =" + Tahun.ToString() + ")  where btJenis in (2,3,5)";

                //_dbHelper.ExecuteNonQuery(SSQL);


                SSQL = "update tAnggaranRekening_A set cRealisasi = (select SUM ( VR.cJumlah) from RealisasiSTS VR " +
                        " where iTahun= tAnggaranRekening_A.iTahun and  VR.IDDInas = tAnggaranRekening_A.IDDInas  and  " +
                        " VR.IIDRekening = tAnggaranRekening_A.IIDRekening and VR.dtBukukas <=" + d.ToSQLFormat() + " and iTahun =" + Tahun.ToString() + ")  where btJenis in (1,4)";

                _dbHelper.ExecuteNonQuery(SSQL);
       

            }
            catch (Exception ex)
            {

            }

        }


        public void BersihkanNonKegiatanRealisasi()
        {
            //SSQL = "DROP TABLE temppendapatan";
            //_dbHelper.ExecuteNonQuery(SSQL);
            SSQL = " IF OBJECT_ID('temppendapatan') IS NOT NULL     DROP TABLE temppendapatan";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = " IF OBJECT_ID('temppenerimaan') IS NOT NULL     DROP TABLE temppenerimaan";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = " IF OBJECT_ID('temppengeluaran') IS NOT NULL     DROP TABLE temppengeluaran";
            _dbHelper.ExecuteNonQuery(SSQL);


            SSQL = "select distinct IDDinas into temppendapatan FROM tAnggaranRekening_A WHERE iTAhun = " + Tahun.ToString() + " and IIDRekening like '4%'";
            _dbHelper.ExecuteNonQuery(SSQL);


            SSQL = "select distinct IDDinas into temppenerimaan FROM tAnggaranRekening_A WHERE iTAhun = " + Tahun.ToString() + " and IIDRekening like '61%'";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "select distinct IDDinas into temppengeluaran FROM tAnggaranRekening_A WHERE iTAhun = " + Tahun.ToString() + " and IIDRekening like '62%'";
            _dbHelper.ExecuteNonQuery(SSQL);


            if (Tahun < 2021)
            {


                SSQL = "DELETE from tKegiatan_A  WHERE btJenis in ( 1,4,5)";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = " INSERT  into tKegiatan_A (iTahun, sNama,btJenis, dtPembahasan,IDDInas, IDUrusan, IDProgram, IDkegiatan) SELECT  " + Tahun.ToString() + "  as iTahun,'Pendapatan',1,'10/31/" + Tahun.ToString() + "', IDDInas, IDDinas/10000 as IDUrusan, 0 as IDProgram, 0 as IDKegiatan  from temppendapatan  ";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = " INSERT  into tKegiatan_A (iTahun, sNama,btJenis, dtPembahasan,IDDInas, IDUrusan, IDProgram, IDkegiatan) SELECT  " + Tahun.ToString() + "  as iTahun,'Penerimaan Pembiayaan',4,'10/31/" + Tahun.ToString() + "', IDDInas, IDDinas/10000 as IDUrusan, 0 as IDProgram, 0 as IDKegiatan  from temppenerimaan  ";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = " INSERT  into tKegiatan_A (iTahun, sNama,btJenis, dtPembahasan,IDDInas, IDUrusan, IDProgram, IDkegiatan) SELECT  " + Tahun.ToString() + "  as iTahun,'Pengeluaran Pembiayaan',5,'10/31/" + Tahun.ToString() + "', IDDInas, IDDinas/10000 as IDUrusan, 0 as IDProgram, 0 as IDKegiatan  from temppengeluaran  ";
                _dbHelper.ExecuteNonQuery(SSQL);


                SSQL = " INSERT  into tKegiatan_A (iTahun, sNama,btJenis, dtPembahasan,IDDInas, IDUrusan, IDProgram, IDkegiatan) SELECT  " + Tahun.ToString() + "  as iTahun,'Belanja Tidak Langsung',2,'10/31/" + Tahun.ToString() + "',ID as IDDInas, ID/10000 as IDUrusan, 0 as IDProgram, 0 as IDKegiatan from mSKPD where root =1 ";
                _dbHelper.ExecuteNonQuery(SSQL);

            }
            else
            {

                SSQL = "DELETE from tSubKegiatan  WHERE btJenis in ( 1,4,5)";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = " INSERT  into tSubKegiatan (iTahun, Nama,btJenis, IDDInas, IDUnit,IDUrusan, IDProgram, IDkegiatan,IDSubkegiatan  ) SELECT  " + Tahun.ToString() + "  as iTahun,'Pendapatan',1, IDDInas,IDDInas as idunit, IDDinas/10000 as IDUrusan, 0 as IDProgram, 0 as IDKegiatan ,0 as IDSUbKegiatan from temppendapatan  ";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = " INSERT  into tSubKegiatan (iTahun, Nama,btJenis, IDDInas, IDUnit,IDUrusan, IDProgram, IDkegiatan,IDSubkegiatan) SELECT  " + Tahun.ToString() + "  as iTahun,'Penerimaan Pembiayaan',4, IDDInas,IDDInas as idunit, IDDinas/10000 as IDUrusan, 0 as IDProgram, 0 as IDKegiatan,0 as IDSUbKegiatan   from temppenerimaan  ";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = " INSERT  into tSubKegiatan (iTahun, Nama,btJenis, IDDInas, IDUnit,IDUrusan, IDProgram, IDkegiatan,IDSubkegiatan) SELECT  " + Tahun.ToString() + "  as iTahun,'Pengeluaran Pembiayaan',5, IDDInas, IDDInas as idunit,IDDinas/10000 as IDUrusan, 0 as IDProgram, 0 as IDKegiatan,0 as IDSUbKegiatan   from temppengeluaran  ";
                _dbHelper.ExecuteNonQuery(SSQL);




            }


            //SSQL = " IF OBJECT_ID('temppendapatan') IS NOT NULL     DROP TABLE temppendapatan";
            //_dbHelper.ExecuteNonQuery(SSQL);




            //SSQL = " INSERT  into tKegiatan_A (iTahun, sNama,btJenis, dtPembahasan,IDDInas, IDUrusan, IDProgram, IDkegiatan) SELECT  " + Tahun.ToString() + "  as iTahun,'Belanja Tidak Langsung',2,'10/31/" + Tahun.ToString() + "',ID as IDDInas, ID/10000 as IDUrusan, 0 as IDProgram, 0 as IDKegiatan from mSKPD where root =1 ";
            //_dbHelper.ExecuteNonQuery(SSQL);




            SSQL = " IF OBJECT_ID('temppendapatan') IS NOT NULL     DROP TABLE temppendapatan";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = " IF OBJECT_ID('temppenerimaan') IS NOT NULL     DROP TABLE temppenerimaan";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = " IF OBJECT_ID('temppengeluaran') IS NOT NULL     DROP TABLE temppengeluaran";
            _dbHelper.ExecuteNonQuery(SSQL);





        }
        public List<RingkasanPerda> GetRingkasanPerdaRealisasiPembiayaan(int _iTahun, Single _iTahap, ParameterLaporan _p, string pNamaView)
        {

            List<RingkasanPerda> _lsttemp = new List<RingkasanPerda>();
            List<RingkasanPerda> _lst = new List<RingkasanPerda>();
            try
            {
                // BersihkanNonKegiatan();

                GetKolom(_p.Tahap);
                string justrek;

                if (_p.Tahun <= 2020)
                {
                    justrek = "3000000";
                }
                else
                {
                    justrek = "300000000000";
                }

                string sNamaView = pNamaView;// CreateViewAllLevel64(_p, true);

                List<RingkasanPerda> _lstPendapatan = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlPendapatan = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaTidakLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlBelanjaTidakLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlBelanjaLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel1 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel2 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel3 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel4 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel5 = new List<RingkasanPerda>();

                string namaKolomdiView1 = "JumlahMurni";
                string namaKolomdiView2 = "Jumlah";

                int b = 0;
                if (_p.LastLevel == 3)
                    SSQL = "SELECT 1 as Kelompok,0 as Level, Root,IIDrekening-" + justrek + "  as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun=  " + _iTahun.ToString() + "  AND  btJEnis= 4 and Root<4  AND IIDrekening >= 6100000 GROUP BY Root,IIDrekening, sNamaRekening ";
                else
                    SSQL = "SELECT 1 as Kelompok,0 as Level, Root,IIDrekening-" + justrek + "  as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun=  " + _iTahun.ToString() + "  AND  btJEnis= 4 and Root<6  AND IIDrekening >= 6100000 GROUP BY Root,IIDrekening, sNamaRekening ";

                _lstPendapatan = GetBagianRingkasanPerdaRealisasi(SSQL);
                _lst = _lstPendapatan;
                b = 1;
                //SSQL = " SELECT 2 as Kelompok,1 as Level,6  as Root, 0 as IIDRekening, 'JUMLAH PENDAPATAN' as sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis= 1  and Root=1 AND IIDrekening > 4000000  ";
                //_JmlPendapatan = GetBagianRingkasanPerda(SSQL);
                //foreach (RingkasanPerda p in _JmlPendapatan)
                //{
                //   _lst.Add(p);
                // }
                b = 2;
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + justrek + "  as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =5  and Root =1  GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel1 = GetBagianRingkasanPerdaRealisasi(SSQL);
                foreach (RingkasanPerda p in _lstBelanjaLevel1)
                {
                    _lst.Add(p);
                }
                b = 3;
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + justrek + "  as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =5  and Root=2  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel2 = GetBagianRingkasanPerdaRealisasi(SSQL);

                foreach (RingkasanPerda p in _lstBelanjaLevel2)
                {
                    _lst.Add(p);
                }
                b = 4;
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + justrek + "  as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =5  and Root=3  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";

                _lstBelanjaLevel3 = GetBagianRingkasanPerdaRealisasi(SSQL);
                foreach (RingkasanPerda p in _lstBelanjaLevel3)
                {
                    _lst.Add(p);
                }

                if (_p.LastLevel == 5)
                {
                    b = 5;
                    SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + justrek + "  as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =5  and Root=4  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel4 = GetBagianRingkasanPerdaRealisasi(SSQL);

                    foreach (RingkasanPerda p in _lstBelanjaLevel4)
                    {
                        _lst.Add(p);
                    }
                    b = 6;
                    SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-" + justrek + "  as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =5  and Root=5  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel5 = GetBagianRingkasanPerdaRealisasi(SSQL);
                    foreach (RingkasanPerda p in _lstBelanjaLevel5)
                    {
                        _lst.Add(p);
                    }
                }


                //b = 11;
                SSQL = " SELECT 5 as Kelompok,1 as Level,1 as Root, 0 as IIDRekening, 'PEMBIAYAAN NETTO' as sNamaRekening," +
                    " (SELECT SUM(" + namaKolomdiView1 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (4) and Root=1)- " +
                    " (SELECT SUM(" + namaKolomdiView1 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (5) and Root=1 )  as JumlahMurni, " +
                    " (SELECT SUM(" + namaKolomdiView2 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (4) and Root=1 ) - " +
                    " (SELECT SUM(" + namaKolomdiView2 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (5) and Root=1 )  as Jumlah";



                _JmlBelanjaLangsung = GetBagianRingkasanPerdaRealisasi(SSQL);
                foreach (RingkasanPerda p in _JmlBelanjaLangsung)
                {
                    _lst.Add(p);
                }

                SSQL = " SELECT 5 as Kelompok,1 as Level,1 as Root, 0 as IIDRekening, 'SISA LEBIH PEMBIAYAAN ANGGARAN TAHUN BERKENAN' as sNamaRekening," +
                   " (SELECT SUM(" + namaKolomdiView1 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1,4) and Root=1 ) - " +
                   " (SELECT SUM(" + namaKolomdiView1 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3,5) and Root=1 )  as JumlahMurni, " +
                   " (SELECT SUM(" + namaKolomdiView2 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1,4) and Root=1 ) - " +
                   " (SELECT SUM(" + namaKolomdiView2 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3,5) and Root=1 )  as Jumlah";



                _JmlBelanjaLangsung = GetBagianRingkasanPerdaRealisasi(SSQL);
                foreach (RingkasanPerda p in _JmlBelanjaLangsung)
                {
                    _lst.Add(p);
                }

                b = 24;
                var cats = from c in _lst
                             .OrderBy(i => i.Kelompok).ThenBy(i => i.IDRekening)
                           select c;
                //.ThenBy(i => i.IDRekening)
                _lsttemp = cats.ToList<RingkasanPerda>();
                return _lsttemp;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }

        }
        public List<RingkasanPerda> GetBagianRingkasanPerdaRealisasi(string SSQL)
        {

            List<RingkasanPerda> _lst = new List<RingkasanPerda>();

            try
            {
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RingkasanPerda()
                                {
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    Root = DataFormat.GetInteger(dr["Root"]),
                                    Level = DataFormat.GetInteger(dr["Level"]),
                                    Kelompok = DataFormat.GetInteger(dr["Kelompok"]),
                                    Kode = DataFormat.GetLong(dr["IIDRekening"]).ToKodeRekening(m_ProfileRekening),
                                    Nama = DataFormat.GetString(dr["sNamaRekening"]),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]),
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"]),
                                    Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                    Persen = DataFormat.GetProsentaseRealisasi(DataFormat.GetDecimal(dr["Jumlah"]), DataFormat.GetDecimal(dr["JumlahMurni"]))

                                }).ToList();
                    }

                }

                return _lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<RingkasanPerda> GetRingkasanPerda64(int _iTahun, Single _iTahap, ParameterLaporan _p)
        {

            List<RingkasanPerda> _lsttemp = new List<RingkasanPerda>();
            List<RingkasanPerda> _lst = new List<RingkasanPerda>();
            try
            {
                //BersihkanNonKegiatan();

                GetKolom(_p.Tahap);
                //string sNamaView = CreateViewAllLevel64(_p, true);
                string sNamaView = CreateViewRealisasiAllLevel(_p, true);

                List<RingkasanPerda> _lstPendapatan = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlPendapatan = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaTidakLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlBelanjaTidakLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlBelanjaLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel1 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel2 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel3 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel4 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel5 = new List<RingkasanPerda>();

                string namaKolomdiView1 = "JumlahMurni";
                string namaKolomdiView2 = "Jumlah";

                int b = 0;
                if (_p.LastLevel == 3)
                    SSQL = "SELECT 1 as Kelompok,0 as Level, Root,IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun=  " + _iTahun.ToString() + "  AND  btJEnis= 1 and Root<4  GROUP BY Root,IIDrekening, sNamaRekening ";
                else
                    SSQL = "SELECT 1 as Kelompok,0 as Level, Root,IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun=  " + _iTahun.ToString() + "  AND  btJEnis= 1 and Root<6  GROUP BY Root,IIDrekening, sNamaRekening ";

                _lstPendapatan = GetBagianRingkasanPerda(SSQL);
                _lst = _lstPendapatan;
                b = 1;

                b = 2;
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3)  and Root =1  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel1 = GetBagianRingkasanPerda(SSQL);
                foreach (RingkasanPerda p in _lstBelanjaLevel1)
                {
                    _lst.Add(p);
                }
                b = 3;
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  in (2,3)  and Root=2  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel2 = GetBagianRingkasanPerda(SSQL);

                foreach (RingkasanPerda p in _lstBelanjaLevel2)
                {
                    _lst.Add(p);
                }
                b = 4;
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  in (2,3)  and Root=3  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";

                _lstBelanjaLevel3 = GetBagianRingkasanPerda(SSQL);
                foreach (RingkasanPerda p in _lstBelanjaLevel3)
                {
                    _lst.Add(p);
                }

                SSQL = " SELECT 5 as Kelompok,1 as Level,1 as Root, 0 as IIDRekening, 'SURPLUS/DEFISIT' as sNamaRekening," +
                    " (SELECT SUM(" + namaKolomdiView1 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1) and Root=1 ) - " +
                    " (SELECT SUM(" + namaKolomdiView1 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1 )  as JumlahMurni, " +
                    " (SELECT SUM(" + namaKolomdiView2 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1) and Root=1 ) - " +
                    " (SELECT SUM(" + namaKolomdiView2 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1 )  as Jumlah";


                //" SELECT SUM(" + namaKolomdiView1 + ") as JumlahMurni,SUM(" + namaKolomdiView2 + ") as Jumlah from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1) and Root=1 AND IIDrekening > 4000000 ) - " +
                //    " - SUM(" + namaKolomdiView1 + ") as JumlahMurni,SUM(" + namaKolomdiView2 + ") as Jumlah from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1 AND IIDrekening > 4000000 ) ";

                _JmlBelanjaLangsung = GetBagianRingkasanPerda(SSQL);
                foreach (RingkasanPerda p in _JmlBelanjaLangsung)
                {
                    _lst.Add(p);
                }

                var cats = from c in _lst
                             .OrderBy(i => i.Kelompok).ThenBy(i => i.IDRekening)
                           select c;
                _lsttemp = cats.ToList<RingkasanPerda>();


                CreateViewAllLevel(_p, false);

                List<RingkasanPerda> lstPembiayaan = new List<RingkasanPerda>();

                lstPembiayaan = GetRingkasanPerdaRealisasiPembiayaan(_iTahun, _iTahap, _p, sNamaView);
                foreach (RingkasanPerda p in lstPembiayaan)
                {
                    _lsttemp.Add(p);
                }



                return _lsttemp;

            }
            catch (Exception ex)
            {
                return null;
            }

        }
        private string CreateViewRealisasiAllLevel(ParameterLaporan _p, bool create)
        {
            string namaView = "viewAnggaran" + _p.NamaUser.Trim().Replace(" ", "");

            HapusView(namaView);

            if (create == true)
            {
                GetKolom(_p.Tahap);
                if (_p.JenisRekening == 1)
                {
                    SSQL = " CREATE VIEW " + namaView + " AS " +
                        " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, A.IIDRekening as Rek,b.IIDRekening, b.btRoot as Root," +
                        " b.sNamaRekening ,  A." + _namaKolom2 + " AS Jumlah, A." + _namaKolom1 + " AS JumlahMurni ,b.iDebet as Debet  " +
                        " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening b on a.IIDRekening=b.IIDRekening  inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND " +
                        " A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=5 and C.Root =1 " +
                        " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + " ) as Rek,b.IIDRekening,b.btRoot as Root," +
                          " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd  FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis   where b.btRoot=4 and c.root = 1 " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening,5), b.btRoot ,b.IIDRekening, b.sNamaRekening ,b.iDebet, a.bppkd  " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root, " +
                            " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet " +
                          " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")  " +
                          " inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=3 and c.root = 1  " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet, a.bppkd   " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ") as Rek,b.IIDRekening, " +
                          " b.btRoot as Root,b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN2.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN2.ToString() + ")   inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=2 and c.root = 1  " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet, a.bppkd   " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root,b.sNamaRekening ,  " +
                          " SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")    inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=1 and c.root = 1  " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet, a.bppkd ";
                }
                else
                {
                    SSQL = " CREATE VIEW " + namaView + " AS " +
                        " Select A.iTahun, A.IDDInas, A.IDurusan,1 as btJenis,A.IDprogram,A.IDKegiatan, A.IIDRekening64 as Rek,b.IIDRekening  , b.btRoot as Root," +
                        " b.sNamaRekening ,  A." + _namaKolom2 + " AS Jumlah, A." + _namaKolom1 + " AS JumlahMurni ,b.iDebet as Debet  " +
                        " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on a.IIDRekening64=b.IIDRekening  inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND " +
                        " A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=5 and C.Root =1 and A.btJenis=1 " +
                        " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,1 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64, " + m_ProfileRekening.LEN4.ToString() + " ) as Rek,b.IIDRekening ,b.btRoot as Root," +
                          " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd  FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN4.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis   where b.btRoot=4 and c.root = 1 and A.btJenis=1 " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64,5), b.btRoot ,b.IIDRekening , b.sNamaRekening ,b.iDebet, a.bppkd  " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,1 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN3.ToString() + ") as Rek,b.IIDRekening , b.btRoot as Root, " +
                            " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet " +
                          " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN3.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")  " +
                          " inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=3 and c.root = 1  and A.btJenis=1 " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN3.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd   " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,1 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN2.ToString() + ") as Rek,b.IIDRekening, " +
                          " b.btRoot as Root,b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN2.ToString() + ")=LEFT(b.IIDRekening , " + m_ProfileRekening.LEN2.ToString() + ")   inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=2 and c.root = 1  and A.btJenis=1 " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN2.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd   " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,1 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN1.ToString() + ") as Rek,b.IIDRekening , b.btRoot as Root,b.sNamaRekening ,  " +
                          " SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN1.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")    inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=1 and c.root = 1  and A.btJenis=1 " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN1.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd ";


                    SSQL = SSQL + "  UNION ALL Select A.iTahun, A.IDDInas, A.IDurusan,3 as btJenis,A.IDprogram,A.IDKegiatan, A.IIDRekening64 as Rek,b.IIDRekening  , b.btRoot as Root," +
                        " b.sNamaRekening ,  A." + _namaKolom2 + " AS Jumlah, A." + _namaKolom1 + " AS JumlahMurni ,b.iDebet as Debet  " +
                        " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on a.IIDRekening64=b.IIDRekening  inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND " +
                        " A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=5 and C.Root =1  and A.btJenis in(2,3)" +
                        " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,3 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64, " + m_ProfileRekening.LEN4.ToString() + " ) as Rek,b.IIDRekening ,b.btRoot as Root," +
                          " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd  FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN4.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis   where b.btRoot=4 and c.root = 1 and A.btJenis  in(2,3)" +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64,5), b.btRoot ,b.IIDRekening , b.sNamaRekening ,b.iDebet, a.bppkd  " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,3 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN3.ToString() + ") as Rek,b.IIDRekening , b.btRoot as Root, " +
                            " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet " +
                          " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN3.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")  " +
                          " inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=3 and c.root = 1 and A.btJenis  in(2,3) " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN3.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd   " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,3 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN2.ToString() + ") as Rek,b.IIDRekening, " +
                          " b.btRoot as Root,b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN2.ToString() + ")=LEFT(b.IIDRekening , " + m_ProfileRekening.LEN2.ToString() + ")   inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=2 and c.root = 1 and A.btJenis  in(2,3) " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN2.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd   " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,3 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN1.ToString() + ") as Rek,b.IIDRekening , b.btRoot as Root,b.sNamaRekening ,  " +
                          " SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN1.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")    inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=1 and c.root = 1 and A.btJenis  in(2,3)" +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan,Left(A.IIDRekening64,1), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd ";//
                    //Left(A.IIDRekening64," + m_ProfileRekening.LEN1.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd ";

                    SSQL = SSQL + "  UNION ALL Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, A.IIDRekening64 as Rek,b.IIDRekening  , b.btRoot as Root," +
                       " b.sNamaRekening ,  A." + _namaKolom2 + " AS Jumlah, A." + _namaKolom1 + " AS JumlahMurni ,b.iDebet as Debet  " +
                       " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on a.IIDRekening64=b.IIDRekening  inner join mSKPD C on a.iDDINAS = C.id " +
                       " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND " +
                       " A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=5 and C.Root =1  and A.btJenis  >3 " +
                       " UNION ALL  " +
                         " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64, " + m_ProfileRekening.LEN4.ToString() + " ) as Rek,b.IIDRekening ,b.btRoot as Root," +
                         " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                         " , a.bppkd  FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN4.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                       " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis   where b.btRoot=4 and c.root = 1 and A.btJenis  >3 " +
                         " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64,5), b.btRoot ,b.IIDRekening , b.sNamaRekening ,b.iDebet, a.bppkd  " +
                         " UNION ALL  " +
                         " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN3.ToString() + ") as Rek,b.IIDRekening , b.btRoot as Root, " +
                           " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet " +
                         " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN3.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")  " +
                         " inner join mSKPD C on a.iDDINAS = C.id " +
                       " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=3 and c.root = 1 and A.btJenis  >3 " +
                         " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN3.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd   " +
                         " UNION ALL  " +
                         " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN2.ToString() + ") as Rek,b.IIDRekening, " +
                         " b.btRoot as Root,b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                         " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN2.ToString() + ")=LEFT(b.IIDRekening , " + m_ProfileRekening.LEN2.ToString() + ")   inner join mSKPD C on a.iDDINAS = C.id " +
                       " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=2 and c.root = 1 and A.btJenis >3 " +
                         " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN2.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd   " +
                         " UNION ALL  " +
                         " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN1.ToString() + ") as Rek,b.IIDRekening , b.btRoot as Root,b.sNamaRekening ,  " +
                         " SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                         " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN1.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")    inner join mSKPD C on a.iDDINAS = C.id " +
                       " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=1 and c.root = 1 and A.btJenis >3 " +
                         " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,Left(A.IIDRekening64,1), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd ";//

                }

                _dbHelper.ExecuteNonQuery(SSQL);
            }
            return namaView;

        }
        private string CreateViewRealisasiAllLevelpp77(ParameterLaporan _p, bool create)
        {
            string namaView = "viewAnggaran" + _p.NamaUser.Trim().Replace(" ", "");
            string tanggal = "'" + _p.TanggalRealisasi.Month.ToString() + "/" + _p.TanggalRealisasi.Day.ToString() + "/" + _p.TanggalRealisasi.Year.ToString() + "'";

            HapusView(namaView);

            if (create == true)
            {
                GetKolom(_p.Tahap);
                if (_p.JenisRekening == 1)
                {
                    SSQL = " CREATE VIEW " + namaView + " AS " +
                        " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, A.IDSUbkegiatan,A.IIDRekening as Rek,b.IIDRekening, b.btRoot as Root," +
                        " b.sNamaRekening ,  A." + _namaKolom2 + " AS Jumlah, A." + _namaKolom1 + " AS JumlahMurni ,b.iDebet as Debet  " +
                        " , a.bppkd FROM dbo.dboGetRealisasiRS (" + tanggal + ") A inner join mRekening b on a.IIDRekening=b.IIDRekening  inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN tSUBKEGIATAN D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND " +
                        " A.IDKegiatan = D.IDKegiatan and A.IDsubKegiatan = D.IDsubKegiatan and A.btJenis = D.btJenis  where b.btRoot=6 and C.Root =1 " +
                        " UNION ALL  " +
                        " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, A.IDSUbkegiatan,Left(A.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root," +
                        " b.sNamaRekening ,  sum(A." + _namaKolom2 + ") AS Jumlah, sum(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                        " , a.bppkd FROM dbo.dboGetRealisasiRS (" + tanggal + ") A inner join mRekening b  on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN5.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN5.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN tSUBKEGIATAN D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND " +
                        " A.IDKegiatan = D.IDKegiatan and A.IDsubKegiatan = D.IDsubKegiatan and A.btJenis = D.btJenis  where b.btRoot=5 and C.Root =1 " +
                        " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSUbkegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN5.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening ,b.iDebet, a.bppkd  " +
                        " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, A.IDSUbkegiatan,Left(A.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + " ) as Rek,b.IIDRekening,b.btRoot as Root," +
                          " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd  FROM dbo.dboGetRealisasiRS (" + tanggal + ") A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN tSUBKEGIATAN D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.IDsubKegiatan = D.IDsubKegiatan  and A.btJenis = D.btJenis   where b.btRoot=4 and c.root = 1 " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, A.IDSUbkegiatan,Left(A.IIDRekening," + m_ProfileRekening.LEN4.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening ,b.iDebet, a.bppkd  " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, A.IDSUbkegiatan,Left(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root, " +
                            " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet " +
                          " , a.bppkd FROM dbo.dboGetRealisasiRS (" + tanggal + ") A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")  " +
                          " inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN tSUBKEGIATAN D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.IDsubKegiatan = D.IDsubKegiatan and A.btJenis = D.btJenis  where b.btRoot=3 and c.root = 1  " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, A.IDSUbkegiatan,Left(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet, a.bppkd   " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSUbkegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ") as Rek,b.IIDRekening, " +
                          " b.btRoot as Root,b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd FROM dbo.dboGetRealisasiRS (" + tanggal + ") A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN2.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN2.ToString() + ")   inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN tSUBKEGIATAN D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.IDsubKegiatan = D.IDsubKegiatan and A.btJenis = D.btJenis    where b.btRoot=2 and c.root = 1  " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSUbkegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet, a.bppkd   " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, A.IDSUbkegiatan,Left(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root,b.sNamaRekening ,  " +
                          " SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd FROM dbo.dboGetRealisasiRS (" + tanggal + ") A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")    inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN tSUBKEGIATAN D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.IDsubKegiatan = D.IDsubKegiatan and A.btJenis = D.btJenis    where b.btRoot=1 and c.root = 1  " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,A.IDSUbkegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet, a.bppkd ";
                }
                else
                {
                    SSQL = " CREATE VIEW " + namaView + " AS " +
                        " Select A.iTahun, A.IDDInas, A.IDurusan,1 as btJenis,A.IDprogram,A.IDKegiatan, A.IIDRekening64 as Rek,b.IIDRekening  , b.btRoot as Root," +
                        " b.sNamaRekening ,  A." + _namaKolom2 + " AS Jumlah, A." + _namaKolom1 + " AS JumlahMurni ,b.iDebet as Debet  " +
                        " , a.bppkd FROM dbo.dboGetRealisasiRS (" + tanggal + ") A inner join mRekening_SAP b on a.IIDRekening64=b.IIDRekening  inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND " +
                        " A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=5 and C.Root =1 and A.btJenis=1 " +
                        " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,1 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64, " + m_ProfileRekening.LEN4.ToString() + " ) as Rek,b.IIDRekening ,b.btRoot as Root," +
                          " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd  FROM dbo.dboGetRealisasiRS (" + tanggal + ") A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN4.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis   where b.btRoot=4 and c.root = 1 and A.btJenis=1 " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64,5), b.btRoot ,b.IIDRekening , b.sNamaRekening ,b.iDebet, a.bppkd  " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,1 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN3.ToString() + ") as Rek,b.IIDRekening , b.btRoot as Root, " +
                            " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet " +
                          " , a.bppkd FROM dbo.dboGetRealisasiRS (" + tanggal + ") A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN3.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")  " +
                          " inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=3 and c.root = 1  and A.btJenis=1 " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN3.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd   " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,1 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN2.ToString() + ") as Rek,b.IIDRekening, " +
                          " b.btRoot as Root,b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd FROM dbo.dboGetRealisasiRS (" + tanggal + ") A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN2.ToString() + ")=LEFT(b.IIDRekening , " + m_ProfileRekening.LEN2.ToString() + ")   inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=2 and c.root = 1  and A.btJenis=1 " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN2.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd   " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,1 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN1.ToString() + ") as Rek,b.IIDRekening , b.btRoot as Root,b.sNamaRekening ,  " +
                          " SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd FROM dbo.dboGetRealisasiRS (" + tanggal + ") A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN1.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")    inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=1 and c.root = 1  and A.btJenis=1 " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN1.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd ";


                    SSQL = SSQL + "  UNION ALL Select A.iTahun, A.IDDInas, A.IDurusan,3 as btJenis,A.IDprogram,A.IDKegiatan, A.IIDRekening64 as Rek,b.IIDRekening  , b.btRoot as Root," +
                        " b.sNamaRekening ,  A." + _namaKolom2 + " AS Jumlah, A." + _namaKolom1 + " AS JumlahMurni ,b.iDebet as Debet  " +
                        " , a.bppkd FROM dbo.dboGetRealisasiRS (" + tanggal + ") A inner join mRekening_SAP b on a.IIDRekening64=b.IIDRekening  inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND " +
                        " A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=5 and C.Root =1  and A.btJenis in(2,3)" +
                        " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,3 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64, " + m_ProfileRekening.LEN4.ToString() + " ) as Rek,b.IIDRekening ,b.btRoot as Root," +
                          " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd  FROM dbo.dboGetRealisasiRS (" + tanggal + ") A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN4.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis   where b.btRoot=4 and c.root = 1 and A.btJenis  in(2,3)" +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64,5), b.btRoot ,b.IIDRekening , b.sNamaRekening ,b.iDebet, a.bppkd  " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,3 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN3.ToString() + ") as Rek,b.IIDRekening , b.btRoot as Root, " +
                            " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet " +
                          " , a.bppkd FROM dbo.dboGetRealisasiRS (" + tanggal + ") A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN3.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")  " +
                          " inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=3 and c.root = 1 and A.btJenis  in(2,3) " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN3.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd   " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,3 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN2.ToString() + ") as Rek,b.IIDRekening, " +
                          " b.btRoot as Root,b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd FROM dbo.dboGetRealisasiRS (" + tanggal + ") A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN2.ToString() + ")=LEFT(b.IIDRekening , " + m_ProfileRekening.LEN2.ToString() + ")   inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=2 and c.root = 1 and A.btJenis  in(2,3) " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN2.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd   " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,3 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN1.ToString() + ") as Rek,b.IIDRekening , b.btRoot as Root,b.sNamaRekening ,  " +
                          " SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd FROM dbo.dboGetRealisasiRS (" + tanggal + ") A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN1.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")    inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=1 and c.root = 1 and A.btJenis  in(2,3)" +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan,Left(A.IIDRekening64,1), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd ";//
                    //Left(A.IIDRekening64," + m_ProfileRekening.LEN1.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd ";

                    SSQL = SSQL + "  UNION ALL Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, A.IIDRekening64 as Rek,b.IIDRekening  , b.btRoot as Root," +
                       " b.sNamaRekening ,  A." + _namaKolom2 + " AS Jumlah, A." + _namaKolom1 + " AS JumlahMurni ,b.iDebet as Debet  " +
                       " , a.bppkd FROM dbo.dboGetRealisasiRS (" + tanggal + ") A inner join mRekening_SAP b on a.IIDRekening64=b.IIDRekening  inner join mSKPD C on a.iDDINAS = C.id " +
                       " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND " +
                       " A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=5 and C.Root =1  and A.btJenis  >3 " +
                       " UNION ALL  " +
                         " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64, " + m_ProfileRekening.LEN4.ToString() + " ) as Rek,b.IIDRekening ,b.btRoot as Root," +
                         " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                         " , a.bppkd  FROM dbo.dboGetRealisasiRS (" + tanggal + ") A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN4.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                       " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis   where b.btRoot=4 and c.root = 1 and A.btJenis  >3 " +
                         " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64,5), b.btRoot ,b.IIDRekening , b.sNamaRekening ,b.iDebet, a.bppkd  " +
                         " UNION ALL  " +
                         " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN3.ToString() + ") as Rek,b.IIDRekening , b.btRoot as Root, " +
                           " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet " +
                         " , a.bppkd FROM dbo.dboGetRealisasiRS (" + tanggal + ") A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN3.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")  " +
                         " inner join mSKPD C on a.iDDINAS = C.id " +
                       " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=3 and c.root = 1 and A.btJenis  >3 " +
                         " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN3.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd   " +
                         " UNION ALL  " +
                         " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN2.ToString() + ") as Rek,b.IIDRekening, " +
                         " b.btRoot as Root,b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                         " , a.bppkd FROM dbo.dboGetRealisasiRS (" + tanggal + ") A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN2.ToString() + ")=LEFT(b.IIDRekening , " + m_ProfileRekening.LEN2.ToString() + ")   inner join mSKPD C on a.iDDINAS = C.id " +
                       " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=2 and c.root = 1 and A.btJenis >3 " +
                         " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN2.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd   " +
                         " UNION ALL  " +
                         " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN1.ToString() + ") as Rek,b.IIDRekening , b.btRoot as Root,b.sNamaRekening ,  " +
                         " SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                         " , a.bppkd FROM dbo.dboGetRealisasiRS (" + tanggal + ") A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN1.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")    inner join mSKPD C on a.iDDINAS = C.id " +
                       " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=1 and c.root = 1 and A.btJenis >3 " +
                         " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,Left(A.IIDRekening64,1), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd ";//

                }

                _dbHelper.ExecuteNonQuery(SSQL);
            }
            return namaView;

        }

        private string ViewRealisasiAllLevel(ParameterLaporan _p, bool create)
        {
            string namaView = "viewRealisasi" + _p.NamaUser.Trim().Replace(" ", "");

            HapusView(namaView);

            if (create == true)
            {
                GetKolom(_p.Tahap);
                if (_p.JenisRekening == 1)
                {
                    SSQL = " CREATE VIEW " + namaView + " AS " +
                        " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, A.IIDRekening as Rek,b.IIDRekening, b.btRoot as Root," +
                        " b.sNamaRekening ,  A." + _namaKolom2 + " AS Jumlah, (Select SUM(iDebet * cJumlah) from Realisasi04AK WHERE iTahun = A.iTahun AND IDDInas = A.IDDInas and  " +
                        " AND IDKegiatan = A.IDKegiatan AND  IIDRekening = A.IIDRekening  ) AS  JumlahMurni   ,b.iDebet as Debet  " +
                        " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening b on a.IIDRekening=b.IIDRekening  inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND " +
                        " A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=5 and C.Root =1 " +
                        " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + " ) as Rek,b.IIDRekening,b.btRoot as Root," +
                          " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, (Select SUM(iDebet * cJumlah) from Realisasi04AK WHERE iTahun = A.iTahun AND IDDInas = A.IDDInas and  " +
                        " AND IDKegiatan = A.IDKegiatan AND  IIDRekening = A.IIDRekening  ) AS  JumlahMurni    ,b.iDebet as Debet  " +
                          " , a.bppkd  FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis   where b.btRoot=4 and c.root = 1 " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening,5), b.btRoot ,b.IIDRekening, b.sNamaRekening ,b.iDebet, a.bppkd  " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root, " +
                            " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, (Select SUM(iDebet * cJumlah) from Realisasi04AK WHERE iTahun = A.iTahun AND IDDInas = A.IDDInas and  " +
                        " AND IDKegiatan = A.IDKegiatan AND  IIDRekening = A.IIDRekening  ) AS  JumlahMurni  ,b.iDebet as Debet " +
                          " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")  " +
                          " inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=3 and c.root = 1  " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet, a.bppkd   " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ") as Rek,b.IIDRekening, " +
                          " b.btRoot as Root,b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, (Select SUM(iDebet * cJumlah) from Realisasi04AK WHERE iTahun = A.iTahun AND IDDInas = A.IDDInas and  " +
                        " AND IDKegiatan = A.IDKegiatan AND  IIDRekening = A.IIDRekening  ) AS  JumlahMurni  ,b.iDebet as Debet  " +
                          " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN2.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN2.ToString() + ")   inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=2 and c.root = 1  " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet, a.bppkd   " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root,b.sNamaRekening ,  " +
                          " SUM(A." + _namaKolom2 + ") AS JUMLAH, (Select SUM(iDebet * cJumlah) from Realisasi04AK WHERE iTahun = A.iTahun AND IDDInas = A.IDDInas and  " +
                        " AND IDKegiatan = A.IDKegiatan AND  IIDRekening = A.IIDRekening  ) AS  JumlahMurni   ,b.iDebet as Debet  " +
                          " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")    inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=1 and c.root = 1  " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet, a.bppkd ";
                }
                else
                {

                    SSQL = " CREATE VIEW " + namaView + " AS " +
                        " Select A.iTahun, A.IDDInas, A.IDurusan,1 as btJenis,A.IDprogram,A.IDKegiatan, A.IIDRekening64 as Rek,b.IIDRekening  , b.btRoot as Root," +
                        " b.sNamaRekening ,  A." + _namaKolom2 + " AS Jumlah, (Select SUM(iDebet * cJumlah) from Realisasi04AK WHERE iTahun = A.iTahun AND IDDInas = A.IDDInas and  " +
                        " AND IIDRekening = A.IIDRekening  ) AS JumlahMurni , b.iDebet as Debet  " +
                        " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on a.IIDRekening64=b.IIDRekening  inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND " +
                        " A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=5 and C.Root =1 and A.btJenis=1 " +
                        " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,1 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64, " + m_ProfileRekening.LEN4.ToString() + " ) as Rek,b.IIDRekening ,b.btRoot as Root," +
                          " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, (Select SUM(iDebet * cJumlah) from Realisasi04AK WHERE iTahun = A.iTahun AND IDDInas = A.IDDInas and  " +
                        " AND IIDRekening = A.IIDRekening  ) AS JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd  FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN4.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis   where b.btRoot=4 and c.root = 1 and A.btJenis=1 " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64,5), b.btRoot ,b.IIDRekening , b.sNamaRekening ,b.iDebet, a.bppkd  " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,1 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN3.ToString() + ") as Rek,b.IIDRekening , b.btRoot as Root, " +
                            " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, (Select SUM(iDebet * cJumlah) from Realisasi04AK WHERE iTahun = A.iTahun AND IDDInas = A.IDDInas and  " +
                        " AND IIDRekening = A.IIDRekening  )  AS JumlahMurni ,b.iDebet as Debet " +
                          " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN3.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")  " +
                          " inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=3 and c.root = 1  and A.btJenis=1 " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN3.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd   " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,1 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN2.ToString() + ") as Rek,b.IIDRekening, " +
                          " b.btRoot as Root,b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, (Select SUM(iDebet * cJumlah) from Realisasi04AK WHERE iTahun = A.iTahun AND IDDInas = A.IDDInas and  " +
                        " AND IIDRekening = A.IIDRekening  ) AS  JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN2.ToString() + ")=LEFT(b.IIDRekening , " + m_ProfileRekening.LEN2.ToString() + ")   inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=2 and c.root = 1  and A.btJenis=1 " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN2.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd   " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,1 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN1.ToString() + ") as Rek,b.IIDRekening , b.btRoot as Root,b.sNamaRekening ,  " +
                          " SUM(A." + _namaKolom2 + ") AS JUMLAH, (Select SUM(iDebet * cJumlah) from Realisasi04AK WHERE iTahun = A.iTahun AND IDDInas = A.IDDInas and  " +
                        " AND IIDRekening = A.IIDRekening  ) AS  JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN1.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")    inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=1 and c.root = 1  and A.btJenis=1 " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN1.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd ";


                    SSQL = SSQL + "  UNION ALL Select A.iTahun, A.IDDInas, A.IDurusan,3 as btJenis,A.IDprogram,A.IDKegiatan, A.IIDRekening64 as Rek,b.IIDRekening  , b.btRoot as Root," +
                        " b.sNamaRekening ,  A." + _namaKolom2 + " AS Jumlah, (Select SUM(iDebet * cJumlah) from Realisasi04AK WHERE iTahun = A.iTahun AND IDDInas = A.IDDInas and  " +
                        " AND IDKegiatan = A/IDKegiatan AND IIDRekening = A.IIDRekening  ) AS  JumlahMurni ,b.iDebet as Debet  " +
                        " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on a.IIDRekening64=b.IIDRekening  inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND " +
                        " A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=5 and C.Root =1  and A.btJenis in(2,3)" +
                        " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,3 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64, " + m_ProfileRekening.LEN4.ToString() + " ) as Rek,b.IIDRekening ,b.btRoot as Root," +
                          " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, (Select SUM(iDebet * cJumlah) from Realisasi04AK WHERE iTahun = A.iTahun AND IDDInas = A.IDDInas and  " +
                        " AND IDKegiatan = A/IDKegiatan AND IIDRekening = A.IIDRekening  ) AS  JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd  FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN4.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis   where b.btRoot=4 and c.root = 1 and A.btJenis  in(2,3)" +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64,5), b.btRoot ,b.IIDRekening , b.sNamaRekening ,b.iDebet, a.bppkd  " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,3 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN3.ToString() + ") as Rek,b.IIDRekening , b.btRoot as Root, " +
                            " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, (Select SUM(iDebet * cJumlah) from Realisasi04AK WHERE iTahun = A.iTahun AND IDDInas = A.IDDInas and  " +
                        " AND IDKegiatan = A/IDKegiatan AND IIDRekening = A.IIDRekening  ) AS  JumlahMurni  ,b.iDebet as Debet " +
                          " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN3.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")  " +
                          " inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=3 and c.root = 1 and A.btJenis  in(2,3) " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN3.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd   " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,3 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN2.ToString() + ") as Rek,b.IIDRekening, " +
                          " b.btRoot as Root,b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, (Select SUM(iDebet * cJumlah) from Realisasi04AK WHERE iTahun = A.iTahun AND IDDInas = A.IDDInas and  " +
                        " AND IDKegiatan = A/IDKegiatan AND IIDRekening = A.IIDRekening  ) AS  JumlahMurni ,b.iDebet as Debet  " +
                          " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN2.ToString() + ")=LEFT(b.IIDRekening , " + m_ProfileRekening.LEN2.ToString() + ")   inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=2 and c.root = 1 and A.btJenis  in(2,3) " +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN2.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd   " +
                          " UNION ALL  " +
                          " Select A.iTahun, A.IDDInas, A.IDurusan,3 as btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN1.ToString() + ") as Rek,b.IIDRekening , b.btRoot as Root,b.sNamaRekening ,  " +
                          " SUM(A." + _namaKolom2 + ") AS JUMLAH, (Select SUM(iDebet * cJumlah) from Realisasi04AK WHERE iTahun = A.iTahun AND IDDInas = A.IDDInas and  " +
                        " AND IDKegiatan = A/IDKegiatan AND IIDRekening = A.IIDRekening  ) AS  JumlahMurni  ,b.iDebet as Debet  " +
                          " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN1.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")    inner join mSKPD C on a.iDDINAS = C.id " +
                        " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=1 and c.root = 1 and A.btJenis  in(2,3)" +
                          " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.IDprogram,A.IDKegiatan,Left(A.IIDRekening64,1), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd ";//
                    //Left(A.IIDRekening64," + m_ProfileRekening.LEN1.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd ";

                    SSQL = SSQL + "  UNION ALL Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, A.IIDRekening64 as Rek,b.IIDRekening  , b.btRoot as Root," +
                       " b.sNamaRekening ,  A." + _namaKolom2 + " AS Jumlah, (Select SUM(iDebet * cJumlah) from Realisasi04AK WHERE iTahun = A.iTahun AND IDDInas = A.IDDInas and  " +
                        " IIDRekening = A.IIDRekening  ) AS  JumlahMurni , b.iDebet as Debet  " +
                       " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on a.IIDRekening64=b.IIDRekening  inner join mSKPD C on a.iDDINAS = C.id " +
                       " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND " +
                       " A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=5 and C.Root =1  and A.btJenis  >3 " +
                       " UNION ALL  " +
                         " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64, " + m_ProfileRekening.LEN4.ToString() + " ) as Rek,b.IIDRekening ,b.btRoot as Root," +
                         " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, (Select SUM(iDebet * cJumlah) from Realisasi04AK WHERE iTahun = A.iTahun AND IDDInas = A.IDDInas and  " +
                        " IIDRekening = A.IIDRekening  ) AS  JumlahMurni ,b.iDebet as Debet  " +
                         " , a.bppkd  FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN4.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                       " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis   where b.btRoot=4 and c.root = 1 and A.btJenis  >3 " +
                         " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64,5), b.btRoot ,b.IIDRekening , b.sNamaRekening ,b.iDebet, a.bppkd  " +
                         " UNION ALL  " +
                         " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN3.ToString() + ") as Rek,b.IIDRekening , b.btRoot as Root, " +
                           " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, (Select SUM(iDebet * cJumlah) from Realisasi04AK WHERE iTahun = A.iTahun AND IDDInas = A.IDDInas and  " +
                        " IIDRekening = A.IIDRekening  ) AS  JumlahMurni ,b.iDebet as Debet " +
                         " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN3.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")  " +
                         " inner join mSKPD C on a.iDDINAS = C.id " +
                       " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=3 and c.root = 1 and A.btJenis  >3 " +
                         " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN3.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd   " +
                         " UNION ALL  " +
                         " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN2.ToString() + ") as Rek,b.IIDRekening, " +
                         " b.btRoot as Root,b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, (Select SUM(iDebet * cJumlah) from Realisasi04AK WHERE iTahun = A.iTahun AND IDDInas = A.IDDInas and  " +
                        " IIDRekening = A.IIDRekening  ) AS  JumlahMurni  ,b.iDebet as Debet  " +
                         " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN2.ToString() + ")=LEFT(b.IIDRekening , " + m_ProfileRekening.LEN2.ToString() + ")   inner join mSKPD C on a.iDDINAS = C.id " +
                       " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=2 and c.root = 1 and A.btJenis >3 " +
                         " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN2.ToString() + "), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd   " +
                         " UNION ALL  " +
                         " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN1.ToString() + ") as Rek,b.IIDRekening , b.btRoot as Root,b.sNamaRekening ,  " +
                         " SUM(A." + _namaKolom2 + ") AS JUMLAH, (Select SUM(iDebet * cJumlah) from Realisasi04AK WHERE iTahun = A.iTahun AND IDDInas = A.IDDInas and  " +
                        " IIDRekening = A.IIDRekening  ) AS  JumlahMurni ,b.iDebet as Debet  " +
                         " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(a.IIDRekening64, " + m_ProfileRekening.LEN1.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")    inner join mSKPD C on a.iDDINAS = C.id " +
                       " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=1 and c.root = 1 and A.btJenis >3 " +
                         " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan,Left(A.IIDRekening64,1), b.btRoot ,b.IIDRekening , b.sNamaRekening,b.iDebet, a.bppkd ";//

                }

                _dbHelper.ExecuteNonQuery(SSQL);
            }
            return namaView;

        }
        public List<PerdaII> GetPerdaIIRealisasi(ParameterLaporan _p)
        {
            List<PerdaII> _lst = new List<PerdaII>();
            try
            {
                // Cek View 
                // CreateViewPerdaII(_p.Tahap);

                // var query = from q in lstDB  group by ()

                //   string namaView = CreateViewPerdaII_B(_p, true);

                string namaView = CreateViewPerdaII(_p, true);
                // PENDAPATAN 

                //GetKodeII
                SSQL = "";

                SSQL = "Select 1 as Level,'4' as Kelompok,1 as KelompokBesar,0 as KodeKategori,0 as KodeUrusan,0 as KodeSKPD, 0 as KOdeRekening, 0 as Rek, 'Pendapatan' as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                        " from viewPerdaII  where btJenis = 1 ";
                SSQL = SSQL + " UNION Select 2 as Level,'4' as Kelompok, 1 as KelompokBesar,mKategori.btKodeKategori  as KodeKategori,0 as KodeUrusan,0 as KodeSKPD, 0 as KOdeRekening,  " +
                            " 0 as Rek,mKategori.sNamaKategori as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                            " from viewPerdaII  inner join mKategori on viewPerdaII.IDUrusan/100= mKategori.btKodeKategori where viewPerdaII.btJenis = 1 " +
                            " group by mKategori.btKodeKategori,mKategori.sNamaKategori ";
                SSQL = SSQL + " UNION  ";

                SSQL = SSQL + "  Select 3 as Level, '4' as Kelompok, 1 as KelompokBesar ,mUrusan.btKodeKategori  as KodeKategori , mUrusan.btKodeUrusan as KodeUrusan, 0 as KodeSKPD, 0 as KOdeRekening,   " +
                            " 0 as Rek, mUrusan.sNamaUrusan as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi from viewPerdaII  inner join mUrusan on viewPerdaII.IDUrusan= mUrusan.ID  where viewPerdaII.btJenis = 1 " +
                            " group by mUrusan.btKodeKategori,mUrusan.btKodeUrusan,mUrusan.sNamaUrusan ";
                SSQL = SSQL + " UNION  ";
                SSQL = SSQL + "  Select 4 as Level, '4' as Kelompok, 1 as KelompokBesar ,mSKPD.btKodeKategori  as KodeKategori , mSKPD.btKodeUrusan as KodeUrusan, mSKPD.ID   as KodeSKPD, 0 as KOdeRekening,   " +
                            " 0 as Rek, mSKPD.sNamaSKPD  as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                            " from viewPerdaII  inner join mSKPD on viewPerdaII.IDDInas= mSKPD.ID  where viewPerdaII.btJenis = 1 " +
                            " group by mSKPD.btKodeKategori , mSKPD.btKodeUrusan , mSKPD.ID , mskpd.sNamaSKPD ";

                SSQL = SSQL + " UNION  ";

                SSQL = SSQL + " Select 1 as Level,'5' as Kelompok,  2 as KelompokBesar,0 as KodeKategori,0 as KodeUrusan,0 as KodeSKPD, 0 as KOdeRekening,0 as Rek,  'BelANJA' as Nama, " +
                            " SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                            " from viewPerdaII  where btJenis in  (2,3) ";
                SSQL = SSQL + " UNION  ";

                SSQL = SSQL + " Select 2 as Level, '5' as Kelompok, 2 as KelompokBesar,mKategori.btKodeKategori  as KodeKategori,0 as KodeUrusan,0 as KodeSKPD, 0 as KOdeRekening, 0 as Rek, mKategori.sNamaKategori as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                            " from viewPerdaII  inner join mKategori on viewPerdaII.IDUrusan/100= mKategori.btKodeKategori where viewPerdaII.btJenis IN (2,3) " +
                            " group by mKategori.btKodeKategori,mKategori.sNamaKategori ";

                SSQL = SSQL + "  UNION ";
                SSQL = SSQL + " Select 3 as Level, '5' as Kelompok, 2 as KelompokBesar ,mUrusan.btKodeKategori as KodeKategori, mUrusan.btKodeUrusan as KodeUrusan, 0 as KodeSKPD, 0 as KOdeRekening, 0 as Rek,  mUrusan.sNamaUrusan as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                            " from viewPerdaII  inner join mUrusan on viewPerdaII.IDUrusan= mUrusan.ID  where viewPerdaII.btJenis  IN (2,3)  " +
                            " group by mUrusan.btKodeKategori,mUrusan.btKodeUrusan,mUrusan.sNamaUrusan  ";
                SSQL = SSQL + "  UNION ";
                SSQL = SSQL + " Select 4 as Level, '5' as Kelompok, 2 as KelompokBesar ,MpELAKSANAuRUSAN.btKodeKategoriPelaksana AS KodeKategori , " +
                            " MpELAKSANAuRUSAN.btKodeUrusanPELAKSANA  as KodeUrusan, Mskpd.id as KodeSKPD, " +
                            " 0 as KOdeRekening,  0 as Rek, Mskpd.sNamaskpd as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                            " from viewPerdaII  inner join mSKPD  on viewPerdaII.iddINAS= mSKPD.ID  " +
                            " INNER JOIN MpELAKSANAURUSAN ON MpELAKSANAuRUSAN.iduRUSAN =viewPerdaII.iduRUSAN AND mPelaksanaUrusan.IDDInas =viewPerdaII.iddiNAS   " +
                            "  and viewPerdaII.iTAhun = MpELAKSANAURUSAN.iTahun  where viewPerdaII.btJenis  IN (2,3) " +
                            " group by MpELAKSANAuRUSAN.btKodeKategoriPelaksana , MpELAKSANAuRUSAN.btKodeUrusanPELAKSANA, Mskpd.id,Mskpd.sNamaskpd ";
                //////SSQL=SSQL + "  UNION ";
                //////SSQL = SSQL + "  Select 5 as Level,'5' as Kelompok, 2 as KelompokBesar ,MpELAKSANAuRUSAN.btKodeKategoriPelaksana AS  KodeKategori , MpELAKSANAuRUSAN.btKodeUrusanPELAKSANA  " +
                //////            " as KodeUrusan, Mskpd.id as KodeSKPD, mRekening.IIDRekening as KOdeRekening, LEFT(viewPerdaII.IIDRekening,2) Rek,  MrEKENING.sNamaRekening  as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                //////            " from viewPerdaII  inner join mSKPD  on viewPerdaII.iddINAS= mSKPD.ID  " +
                //////           " INNER JOIN MpELAKSANAURUSAN ON MpELAKSANAuRUSAN.iduRUSAN =viewPerdaII.iduRUSAN AND mPelaksanaUrusan.IDDInas =viewPerdaII.iddiNAS   " +
                //////            "   AND mPelaksanaUrusan.iTAhun  =viewPerdaII.iTAhun  INNER JOIN mRekening on Left(mRekening.IIDRekening,2) = LEFT(viewPerdaII.IIDRekening,2) " +
                //////            " where viewPerdaII.btJenis  IN (2,3) and mRekening.btRoot = 2 " +
                //////            " group by MpELAKSANAuRUSAN.btKodeKategoriPelaksana , MpELAKSANAuRUSAN.btKodeUrusanPELAKSANA, Mskpd.id,Mskpd.sNamaskpd,mRekening.IIDRekening , " +
                //////            " LEFT(viewPerdaII.IIDRekening,2),mRekening.sNamaRekening ";
                //////SSQL=SSQL + "  UNION ";
                //////SSQL = SSQL + " Select 6 as Level, '5' as Kelompok, 2 as KelompokBesar ,MpELAKSANAuRUSAN.btKodeKategoriPelaksana AS  KodeKategori , MpELAKSANAuRUSAN.btKodeUrusanPELAKSANA  " +
                //////            " as KodeUrusan, Mskpd.id as KodeSKPD, mRekening.IIDRekening as KOdeRekening, LEFT(viewPerdaII.IIDRekening,3) Rek,  MrEKENING.sNamaRekening  as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                //////             " from viewPerdaII  inner join mSKPD  on viewPerdaII.iddINAS= mSKPD.ID  " +
                //////            " INNER JOIN MpELAKSANAURUSAN ON MpELAKSANAuRUSAN.iduRUSAN =viewPerdaII.iduRUSAN AND mPelaksanaUrusan.IDDInas =viewPerdaII.iddiNAS   " +
                //////            "  AND mPelaksanaUrusan.iTAhun  =viewPerdaII.iTAhun  INNER JOIN mRekening on Left(mRekening.IIDRekening,3) = LEFT(viewPerdaII.IIDRekening,3) " +
                //////            " where viewPerdaII.btJenis  IN (2,3) and mRekening.btRoot = 3 " +
                //////            " group by MpELAKSANAuRUSAN.btKodeKategoriPelaksana , MpELAKSANAuRUSAN.btKodeUrusanPELAKSANA, Mskpd.id,Mskpd.sNamaskpd,mRekening.IIDRekening ," +
                //////            " LEFT(viewPerdaII.IIDRekening,3),mRekening.sNamaRekening ";




                //  SSQL = SSQL + " UNION  Select 1 as Level,'6' as Kelompok,3 as KelompokBesar,0 as KodeKategori,0 as KodeUrusan,0 as KodeSKPD, 0 as KOdeRekening, 0 as Rek, 'PEMBIAYAAN' as Nama, 0 as Anggaran, 0 as Realisasi " +
                //         " from viewPerdaII  ";
                //////SSQL = SSQL + " UNION Select 2 as Level, '6' as Kelompok, 3 as KelompokBesar ,1 AS  KodeKategori , 20  " +
                //////        " as KodeUrusan, Mskpd.id as KodeSKPD, mRekening.IIDRekening as KOdeRekening, LEFT(viewPerdaII.IIDRekening,2) Rek,  MrEKENING.sNamaRekening  as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                //////         " from viewPerdaII  inner join mSKPD  on viewPerdaII.iddINAS= mSKPD.ID  " +
                //////        " INNER JOIN mRekening on Left(mRekening.IIDRekening,2) = LEFT(viewPerdaII.IIDRekening,2) " +
                //////        " where viewPerdaII.btJenis  = 4  and mRekening.btRoot in (2) " +
                //////        " group by Mskpd.id,Mskpd.sNamaskpd,mRekening.IIDRekening ," +
                //////        " LEFT(viewPerdaII.IIDRekening,2),mRekening.sNamaRekening ";

                SSQL = SSQL + " UNION Select 3 as Level,'6' as Kelompok, 4 as KelompokBesar,1  as KodeKategori,0 as KodeUrusan,0 as KodeSKPD, 0 as KOdeRekening,  " +
                            " 0 as Rek,mKategori.sNamaKategori as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                            " from viewPerdaII  inner join mKategori on viewPerdaII.IDUrusan/100= mKategori.btKodeKategori where viewPerdaII.btJenis = 4 " +
                            " group by mKategori.btKodeKategori,mKategori.sNamaKategori ";
                SSQL = SSQL + " UNION  ";

                SSQL = SSQL + "  Select 4 as Level, '6.1' as Kelompok, 4 as KelompokBesar ,1 as KodeKategori , 20 as KodeUrusan, 0 as KodeSKPD, 0 as KOdeRekening,   " +
                            " 0 as Rek, mUrusan.sNamaUrusan as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi from viewPerdaII  inner join mUrusan on viewPerdaII.IDUrusan= mUrusan.ID  where viewPerdaII.btJenis = 4 " +
                            " group by mUrusan.btKodeKategori,mUrusan.btKodeUrusan,mUrusan.sNamaUrusan ";

                SSQL = SSQL + " UNION  ";
                SSQL = SSQL + "  Select 5 as Level, '6.1' as Kelompok, 4 as KelompokBesar ,1 as KodeKategori , 20 as KodeUrusan, mSKPD.ID   as KodeSKPD, 0 as KOdeRekening,   " +
                            " 0 as Rek, mSKPD.sNamaSKPD  as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                            " from viewPerdaII  inner join mSKPD on viewPerdaII.IDDInas= mSKPD.ID  where viewPerdaII.btJenis = 4 " +
                            " group by mSKPD.ID , mskpd.sNamaSKPD ";

                //  SSQL = "";
                //////SSQL = SSQL + " UNION ALL ";
                //////SSQL = SSQL + " Select 2 as Level, '6.2' as Kelompok, 6 as KelompokBesar ,MpELAKSANAuRUSAN.btKodeKategoriPelaksana AS  KodeKategori , MpELAKSANAuRUSAN.btKodeUrusanPELAKSANA  " +
                //////        " as KodeUrusan, Mskpd.id as KodeSKPD, mRekening.IIDRekening as KOdeRekening, LEFT(viewPerdaII.IIDRekening,2) Rek,  MrEKENING.sNamaRekening  as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                //////         " from viewPerdaII  inner join mSKPD  on viewPerdaII.iddINAS= mSKPD.ID  " +
                //////        " INNER JOIN MpELAKSANAURUSAN ON MpELAKSANAuRUSAN.iduRUSAN =viewPerdaII.iduRUSAN AND mPelaksanaUrusan.IDDInas =viewPerdaII.iddiNAS   " +
                //////        " INNER JOIN mRekening on Left(mRekening.IIDRekening,2) = LEFT(viewPerdaII.IIDRekening,2) " +
                //////        " where viewPerdaII.btJenis  = 5  and mRekening.btRoot = 2 " +
                //////        " group by MpELAKSANAuRUSAN.btKodeKategoriPelaksana , MpELAKSANAuRUSAN.btKodeUrusanPELAKSANA, Mskpd.id,Mskpd.sNamaskpd,mRekening.IIDRekening ," +
                //////        " LEFT(viewPerdaII.IIDRekening,2),mRekening.sNamaRekening ";

                SSQL = SSQL + " UNION Select 2 as Level, '6' as Kelompok, 5 as KelompokBesar ,1 AS  KodeKategori , 20  " +
                        " as KodeUrusan, Mskpd.id as KodeSKPD, mRekening.IIDRekening as KOdeRekening, LEFT(viewPerdaII.IIDRekening,2) Rek,  MrEKENING.sNamaRekening  as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                         " from viewPerdaII  inner join mSKPD  on viewPerdaII.iddINAS= mSKPD.ID  " +
                        " INNER JOIN mRekening on Left(mRekening.IIDRekening,2) = LEFT(viewPerdaII.IIDRekening,2) " +
                        " where viewPerdaII.btJenis  = 5  and mRekening.btRoot in (2) " +
                        " group by Mskpd.id,Mskpd.sNamaskpd,mRekening.IIDRekening ," +
                        " LEFT(viewPerdaII.IIDRekening,2),mRekening.sNamaRekening ";


                SSQL = SSQL + " UNION ALL Select 3 as Level,'6.2' as Kelompok, 6 as KelompokBesar,mKategori.btKodeKategori  as KodeKategori,0 as KodeUrusan,0 as KodeSKPD, 0 as KOdeRekening,  " +
                            " 0 as Rek,mKategori.sNamaKategori as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                            " from viewPerdaII  inner join mKategori on viewPerdaII.IDUrusan/100= mKategori.btKodeKategori where viewPerdaII.btJenis = 5 " +
                            " group by mKategori.btKodeKategori,mKategori.sNamaKategori ";
                SSQL = SSQL + " UNION  ";

                SSQL = SSQL + "  Select 4 as Level, '6.2' as Kelompok, 6 as KelompokBesar ,mUrusan.btKodeKategori  as KodeKategori , mUrusan.btKodeUrusan as KodeUrusan, 0 as KodeSKPD, 0 as KOdeRekening,   " +
                            " 0 as Rek, mUrusan.sNamaUrusan as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi from viewPerdaII  inner join mUrusan on viewPerdaII.IDUrusan= mUrusan.ID  where viewPerdaII.btJenis = 5 " +
                            " group by mUrusan.btKodeKategori,mUrusan.btKodeUrusan,mUrusan.sNamaUrusan ";
                SSQL = SSQL + " UNION  ";
                SSQL = SSQL + "  Select 5 as Level, '6.2' as Kelompok, 6 as KelompokBesar ,mSKPD.btKodeKategori  as KodeKategori , mSKPD.btKodeUrusan as KodeUrusan, mSKPD.ID   as KodeSKPD, 0 as KOdeRekening,   " +
                            " 0 as Rek, mSKPD.sNamaSKPD  as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                            " from viewPerdaII  inner join mSKPD on viewPerdaII.IDDInas= mSKPD.ID  where viewPerdaII.btJenis = 5 " +
                            " group by mSKPD.btKodeKategori , mSKPD.btKodeUrusan , mSKPD.ID , mskpd.sNamaSKPD ";

                ////////SSQL = SSQL + " UNION Select 6 as Level, '6.2' as Kelompok, 7 as KelompokBesar ,MpELAKSANAuRUSAN.btKodeKategoriPelaksana AS  KodeKategori , MpELAKSANAuRUSAN.btKodeUrusanPELAKSANA  " +
                ////////           " as KodeUrusan, Mskpd.id as KodeSKPD, mRekening.IIDRekening as KOdeRekening, LEFT(viewPerdaII.IIDRekening,3) Rek,  MrEKENING.sNamaRekening  as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                ////////            " from viewPerdaII  inner join mSKPD  on viewPerdaII.iddINAS= mSKPD.ID  " +
                ////////           " INNER JOIN MpELAKSANAURUSAN ON MpELAKSANAuRUSAN.iduRUSAN =viewPerdaII.iduRUSAN AND mPelaksanaUrusan.IDDInas =viewPerdaII.iddiNAS  AND mPelaksanaUrusan.iTahun =viewPerdaII.iTahun  " +
                ////////           " INNER JOIN mRekening on Left(mRekening.IIDRekening,3) = LEFT(viewPerdaII.IIDRekening,3) " +
                ////////           " where viewPerdaII.btJenis  IN (5) and mRekening.btRoot  in (3) " +
                ////////           " group by MpELAKSANAuRUSAN.btKodeKategoriPelaksana , MpELAKSANAuRUSAN.btKodeUrusanPELAKSANA, Mskpd.id,Mskpd.sNamaskpd,mRekening.IIDRekening ," +
                ////////           " LEFT(viewPerdaII.IIDRekening,3),mRekening.sNamaRekening ";
                ////////SSQL = SSQL + " UNION Select 6 as Level, '6.1' as Kelompok, 5 as KelompokBesar ,MpELAKSANAuRUSAN.btKodeKategoriPelaksana AS  KodeKategori , MpELAKSANAuRUSAN.btKodeUrusanPELAKSANA  " +
                ////////      " as KodeUrusan, Mskpd.id as KodeSKPD, mRekening.IIDRekening as KOdeRekening, LEFT(viewPerdaII.IIDRekening,3) Rek,  MrEKENING.sNamaRekening  as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                ////////       " from viewPerdaII  inner join mSKPD  on viewPerdaII.iddINAS= mSKPD.ID  " +
                ////////      " INNER JOIN MpELAKSANAURUSAN ON MpELAKSANAuRUSAN.iduRUSAN =viewPerdaII.iduRUSAN AND mPelaksanaUrusan.IDDInas =viewPerdaII.iddiNAS   AND mPelaksanaUrusan.iTahun =viewPerdaII.iTahun  " +
                ////////      " INNER JOIN mRekening on Left(mRekening.IIDRekening,3) = LEFT(viewPerdaII.IIDRekening,3) " +
                ////////      " where viewPerdaII.btJenis  IN (4) and mRekening.btRoot  in (3) " +
                ////////      " group by MpELAKSANAuRUSAN.btKodeKategoriPelaksana , MpELAKSANAuRUSAN.btKodeUrusanPELAKSANA, Mskpd.id,Mskpd.sNamaskpd,mRekening.IIDRekening ," +
                ////////      " LEFT(viewPerdaII.IIDRekening,3),mRekening.sNamaRekening ";

                //SSQL = SSQL + " UNION Select 6 as Level, '6.1' as Kelompok, 4 as KelompokBesar ,MpELAKSANAuRUSAN.btKodeKategoriPelaksana AS  KodeKategori , MpELAKSANAuRUSAN.btKodeUrusanPELAKSANA  " +
                //                    " as KodeUrusan, Mskpd.id as KodeSKPD, mRekening.IIDRekening as KOdeRekening, LEFT(viewPerdaII.IIDRekening,3) Rek,  MrEKENING.sNamaRekening  as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                //                     " from viewPerdaII  inner join mSKPD  on viewPerdaII.iddINAS= mSKPD.ID  " +
                //                    " INNER JOIN MpELAKSANAURUSAN ON MpELAKSANAuRUSAN.iduRUSAN =viewPerdaII.iduRUSAN AND mPelaksanaUrusan.IDDInas =viewPerdaII.iddiNAS   " +
                //                    " INNER JOIN mRekening on Left(mRekening.IIDRekening,3) = LEFT(viewPerdaII.IIDRekening,3) " +
                //                    " where viewPerdaII.btJenis  IN (4) and mRekening.btRoot = 3 " +
                //                    " group by MpELAKSANAuRUSAN.btKodeKategoriPelaksana , MpELAKSANAuRUSAN.btKodeUrusanPELAKSANA, Mskpd.id,Mskpd.sNamaskpd,mRekening.IIDRekening ," +
                //                    " LEFT(viewPerdaII.IIDRekening,3),mRekening.sNamaRekening ";

                SSQL = SSQL + "   ORDER BY KelompokBesar ,KodeKategori , KodeUrusan, KodeSKPD, KOdeRekening";




                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaII()
                                {

                                    //Kode { set; get; }
                                    Level = DataFormat.GetInteger(dr["Level"]),
                                    //IDKategori { set; get; }
                                    IDUrusan = DataFormat.GetInteger(dr["kodeurusan"]).IntToStringWithLeftPad(2),
                                    IDDInas = DataFormat.GetInteger(dr["kodeskpd"]),
                                    //root { set; get; }
                                    Kode = DataFormat.GetInteger(dr["Level"]) == 1 ? DataFormat.GetString(dr["Kelompok"]) : DataFormat.GetString(dr["Kelompok"]) + "." + (DataFormat.GetInteger(dr["Level"]) == 2 ? DataFormat.GetInteger(dr["Kodekategori"]).ToString("#") :
                                           (DataFormat.GetInteger(dr["Level"]) == 3 ? DataFormat.GetInteger(dr["Kodekategori"]).ToString("#") + "." + DataFormat.GetInteger(dr["KodeUrusan"]).ToString("##") :
                                           (DataFormat.GetInteger(dr["Level"]) == 4 ? DataFormat.GetInteger(dr["Kodekategori"]).ToString("#") + "." + DataFormat.GetInteger(dr["KodeUrusan"]).ToString("##") + "." + DataFormat.GetInteger(dr["kodeskpd"]).ToKodeDinas() :
                                            DataFormat.GetInteger(dr["Kodekategori"]).ToString("#") + "." + DataFormat.GetInteger(dr["KodeUrusan"]).ToString("##") + "." + DataFormat.GetInteger(dr["kodeskpd"]).ToKodeDinas() + "." + DataFormat.GetLong(dr["KodeRekening"]).ToKodeRekening(m_ProfileRekening)))), //GetKodeII(DataFormat.GetInteger(dr["Level"]),
                                    //DataFormat.GetInteger(dr["IDUrusan"]),
                                    //DataFormat.GetInteger(dr["IDDinas"]), DataFormat.GetInteger(dr["IDRekening"])),

                                    Nama = DataFormat.GetString(dr["Nama"]).ToUpper(),
                                    JumlahBelanjaMurni = (DataFormat.GetDecimal(dr["Anggaran"])).ToRupiahInReport(),
                                    JumlahBelanja = (DataFormat.GetDecimal(dr["Realisasi"])).ToRupiahInReport(),
                                    SelisihBelanja = (DataFormat.GetDecimal(dr["Realisasi"]) - DataFormat.GetDecimal(dr["Anggaran"])).ToRupiahInReport(),
                                    PersenBelanja = DataFormat.GetProsentaseRealisasi(DataFormat.GetDecimal(dr["Realisasi"]), DataFormat.GetDecimal(dr["Anggaran"]))
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

        public List<PerdaII> GetPerdaRealisasiPerUrusan(ParameterLaporan _p)
        {
            List<PerdaII> _lst = new List<PerdaII>();
            try
            {



                //GetKodeII
                if (_p.Tahun < 2022)
                {
                    SSQL = "select * From [vwPerurusanPendapatan] " +
                           " UNION select * from [vwPerurusanBelanja] where Anggaran>0 " +
                           " UNION Select * from vwPerurusanSurplusDefisit " +
                           " order by Kelompok, kodeKategori, KodeUrusan, KodeSKPD, Rek ";
                }
                else
                {


                    SSQL = "select * from dbo.fnPerurusanPendapatan(" + _p.Tahun.ToString() + " ," + _p.TanggalRealisasi.ToSQLFormat() + ")  " +
                           " UNION select * from dbo.fnPerurusanBelanja(" + _p.Tahun.ToString() + " ," + _p.TanggalRealisasi.ToSQLFormat() + ") where Anggaran>0 " +
                           " UNION Select * from dbo.fnPerurusanSurplusDefisit(" + _p.Tahun.ToString() + " ," + _p.TanggalRealisasi.ToSQLFormat() + ")   " +
                           " order by Kelompok, kodeKategori, KodeUrusan, KodeSKPD, Rek ";


                }





                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaII()
                                {

                                    //Kode { set; get; }
                                    Level = DataFormat.GetInteger(dr["Level"]),
                                    //IDKategori { set; get; }
                                    IDUrusan = DataFormat.GetInteger(dr["kodeurusan"]).IntToStringWithLeftPad(2),
                                    IDDInas = DataFormat.GetInteger(dr["kodeskpd"]),
                                    IDKategori = DataFormat.GetInteger(dr["KodeKategori"]).IntToStringWithLeftPad(1),
                                    Kode = DataFormat.GetString(dr["Kode"]),

                                    Nama = DataFormat.GetString(dr["Nama"]).ToUpper(),
                                    JumlahBelanjaMurni = (DataFormat.GetDecimal(dr["Anggaran"])).ToRupiahInReport(),
                                    JumlahBelanja = (DataFormat.GetDecimal(dr["Realisasi"])).ToRupiahInReport(),
                                    SelisihBelanja = (DataFormat.GetDecimal(dr["Realisasi"]) - DataFormat.GetDecimal(dr["Anggaran"])).ToRupiahInReport(),
                                    PersenBelanja = DataFormat.GetProsentaseRealisasi(DataFormat.GetDecimal(dr["Realisasi"]), DataFormat.GetDecimal(dr["Anggaran"]))
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

        public List<PerdaRealisasi1_1> GetPerdaIIRealisasiB2(ParameterLaporan _p, int pAnggaran)
        {
            List<PerdaRealisasi1_1> _lst = new List<PerdaRealisasi1_1>();
            try
            {
                // Cek View 
                // CreateviewPerdaIIB (_p.Tahap);

                // var query = from q in lstDB  group by ()

                string namaView = CreateViewPerdaII_BBRealisasi2(_p, true);

                // PENDAPATAN 

                //GetKodeII
                SSQL = "";
                if (_p.Tahun <= 2020)
                {

                    SSQL = SSQL + " Select 1 as Level,'5' as Kelompok,  2 as KelompokBesar,0 as KodeKategori,0 as KodeUrusan,0 as KodeSKPD, 0 as KOdeRekening,0 as Rek,  'BelANJA' as Nama, " +
                                " SUM(AnggaranPendapatan) as AnggaranPendapatan, SUM(AnggaranPegawai) as AnggaranPegawai,SUM(AnggaranBarangJasa) as AnggaranBarangJasa," +
                                " SUM(AnggaranBunga) as AnggaranBunga,SUM(AnggaranSubsidi) as AnggaranSubsidi,SUM(AnggaranHibah) as AnggaranHibah, SUM(AnggaranBansos) as AnggaranBansos,SUM(AnggaranBagiHasil) as AnggaranBagiHasil," +
                                " SUM(AnggaranBantuanKeuangan) as AnggaranBantuanKeuangan,SUM(AnggaranModal) as AnggaranModal,SUM(AnggaranTakTerduga) as AnggaranTakTerduga,SUM(RealisasiPendapatan) as RealisasiPendapatan," +
                                "SUM(RealisasiPegawai) as RealisasiPegawai,SUM(RealisasiBarangJasa) as RealisasiBarangJasa,SUM(RealisasiBunga) as RealisasiBunga,SUM(RealisasiSubsidi) as RealisasiSubsidi,SUM(RealisasiHibah) as RealisasiHibah," +
                                "SUM(RealisasiBansos) as RealisasiBansos,SUM(RealisasiBagiHasil) as RealisasiBagiHasil,SUM(RealisasiBantuanKeuangan) as RealisasiBantuanKeuangan,SUM(RealisasiModal) as RealisasiModal," +
                                "SUM(RealisasiTakTerduga) as RealisasiTakTerduga,SUM(AnggaranBelanja) as AnggaranBelanja ,SUM(RealisasiBelanja) as RealisasiBelanja " +
                                " from " + namaView;
                    SSQL = SSQL + " UNION  ";

                    SSQL = SSQL + " Select 2 as Level, '5' as Kelompok, 2 as KelompokBesar,mKategori.btKodeKategori  as KodeKategori,0 as KodeUrusan,0 as KodeSKPD, 0 as KOdeRekening, 0 as Rek, mKategori.sNamaKategori as Nama, " +
                                          " SUM(AnggaranPendapatan) as AnggaranPendapatan, SUM(AnggaranPegawai) as AnggaranPegawai,SUM(AnggaranBarangJasa) as AnggaranBarangJasa," +
                                " SUM(AnggaranBunga) as AnggaranBunga,SUM(AnggaranSubsidi) as AnggaranSubsidi,SUM(AnggaranHibah) as AnggaranHibah, SUM(AnggaranBansos) as AnggaranBansos,SUM(AnggaranBagiHasil) as AnggaranBagiHasil," +
                                " SUM(AnggaranBantuanKeuangan) as AnggaranBantuanKeuangan,SUM(AnggaranModal) as AnggaranModal,SUM(AnggaranTakTerduga) as AnggaranTakTerduga " +
                                " from " + namaView + " inner join mKategori on viewPerdaIIB.IDUrusan/100= mKategori.btKodeKategori where viewPerdaIIB.btJenis IN (2,3) " +
                                " group by mKategori.btKodeKategori,mKategori.sNamaKategori ";

                    SSQL = SSQL + "  UNION ";
                    SSQL = SSQL + " Select 3 as Level, '5' as Kelompok, 2 as KelompokBesar ,mUrusan.btKodeKategori as KodeKategori, mUrusan.btKodeUrusan as KodeUrusan, 0 as KodeSKPD, 0 as KOdeRekening, 0 as Rek,  mUrusan.sNamaUrusan as Nama, " +
                                  " SUM(AnggaranPendapatan) as AnggaranPendapatan, SUM(AnggaranPegawai) as AnggaranPegawai,SUM(AnggaranBarangJasa) as AnggaranBarangJasa," +
                                " SUM(AnggaranBunga) as AnggaranBunga,SUM(AnggaranSubsidi) as AnggaranSubsidi,SUM(AnggaranHibah) as AnggaranHibah, SUM(AnggaranBansos) as AnggaranBansos,SUM(AnggaranBagiHasil) as AnggaranBagiHasil," +
                                " SUM(AnggaranBantuanKeuangan) as AnggaranBantuanKeuangan,SUM(AnggaranModal) as AnggaranModal,SUM(AnggaranTakTerduga) as AnggaranTakTerduga " +
                                " from " + namaView + "   inner join mUrusan on viewPerdaIIB.IDUrusan= mUrusan.ID  where viewPerdaIIB.btJenis  IN (2,3)  " +
                                " group by mUrusan.btKodeKategori,mUrusan.btKodeUrusan,mUrusan.sNamaUrusan  ";
                    SSQL = SSQL + "  UNION ";
                    SSQL = SSQL + " Select 4 as Level, '5' as Kelompok, 2 as KelompokBesar ,MpELAKSANAuRUSAN.btKodeKategoriPelaksana AS KodeKategori , " +
                                " MpELAKSANAuRUSAN.btKodeUrusanPELAKSANA  as KodeUrusan, Mskpd.id as KodeSKPD, " +
                                " 0 as KOdeRekening,  0 as Rek, Mskpd.sNamaskpd as Nama, " +
                                  " SUM(AnggaranPendapatan) as AnggaranPendapatan, SUM(AnggaranPegawai) as AnggaranPegawai,SUM(AnggaranBarangJasa) as AnggaranBarangJasa," +
                                " SUM(AnggaranBunga) as AnggaranBunga,SUM(AnggaranSubsidi) as AnggaranSubsidi,SUM(AnggaranHibah) as AnggaranHibah, SUM(AnggaranBansos) as AnggaranBansos,SUM(AnggaranBagiHasil) as AnggaranBagiHasil," +
                                " SUM(AnggaranBantuanKeuangan) as AnggaranBantuanKeuangan,SUM(AnggaranModal) as AnggaranModal,SUM(AnggaranTakTerduga) as AnggaranTakTerduga " +
                                " from " + namaView + "   inner join mSKPD  on viewPerdaIIB.iddINAS= mSKPD.ID  " +
                                " INNER JOIN MpELAKSANAURUSAN ON MpELAKSANAuRUSAN.iduRUSAN =viewPerdaIIB.iduRUSAN AND mPelaksanaUrusan.IDDInas =viewPerdaIIB.iddiNAS   " +
                                "  and viewPerdaIIB.iTAhun = MpELAKSANAURUSAN.iTahun  where viewPerdaIIB.btJenis  IN (2,3) " +
                                " group by MpELAKSANAuRUSAN.btKodeKategoriPelaksana , MpELAKSANAuRUSAN.btKodeUrusanPELAKSANA, Mskpd.id,Mskpd.sNamaskpd ";

                    SSQL = SSQL + "   ORDER BY KelompokBesar ,KodeKategori , KodeUrusan, KodeSKPD, KOdeRekening";


                }
                else
                {

                    SSQL = " Select 1 as Level,'5' as Kelompok,  2 as KelompokBesar,0 as KodeKategori,0 as KodeUrusan,0 as KodeSKPD, 0 as KOdeRekening,0 as Rek,  'BelANJA' as Nama, " +
                                 " SUM(AnggaranPendapatan) as AnggaranPendapatan, SUM(Anggaranoperasi) as AnggaranPegawai,SUM(AnggaranModal) as AnggaranBarangJasa," +
                                " SUM(AnggaranBTT) as AnggaranBunga,SUM(AnggaranTransfer) as AnggaranSubsidi,SUM(RealisasiPendapatan) as RealisasiPendapatan," +
                                "SUM(RealisasiOperasi) as RealisasiPegawai,SUM(RealisasiModal) as RealisasiBarangJasa,SUM(RealisasiBTT) as RealisasiBunga,SUM(RealisasiTransfer) as RealisasiSubsidi " +
                               " from " + namaView;

                    SSQL = SSQL + " UNION  ";

                    SSQL = SSQL + " Select 2 as Level, '5' as Kelompok, 2 as KelompokBesar,mKategori.btKodeKategori  as KodeKategori,0 as KodeUrusan,0 as KodeSKPD, 0 as KOdeRekening, 0 as Rek, mKategori.sNamaKategori as Nama, " +
                      " SUM(AnggaranPendapatan) as AnggaranPendapatan, SUM(Anggaranoperasi) as AnggaranPegawai,SUM(AnggaranModal) as AnggaranBarangJasa," +
                                " SUM(AnggaranBTT) as AnggaranBunga,SUM(AnggaranTransfer) as AnggaranSubsidi,SUM(RealisasiPendapatan) as RealisasiPendapatan," +
                                "SUM(RealisasiOperasi) as RealisasiPegawai,SUM(RealisasiModal) as RealisasiBarangJasa,SUM(RealisasiBTT) as RealisasiBunga,SUM(RealisasiTransfer) as RealisasiSubsidi " +
                                " from " + namaView + " inner join mKategori on viewPerdaIIB.IDUrusan/100= mKategori.btKodeKategori where viewPerdaIIB.btJenis IN (1,2,3) " +
                                " group by mKategori.btKodeKategori,mKategori.sNamaKategori ";

                    SSQL = SSQL + "  UNION ";
                    SSQL = SSQL + " Select 3 as Level, '5' as Kelompok, 2 as KelompokBesar ,mUrusan.btKodeKategori as KodeKategori, mUrusan.btKodeUrusan as KodeUrusan, 0 as KodeSKPD, 0 as KOdeRekening, 0 as Rek,  mUrusan.sNamaUrusan as Nama, " +
                      " SUM(AnggaranPendapatan) as AnggaranPendapatan, SUM(Anggaranoperasi) as AnggaranPegawai,SUM(AnggaranModal) as AnggaranBarangJasa," +
                                " SUM(AnggaranBTT) as AnggaranBunga,SUM(AnggaranTransfer) as AnggaranSubsidi,SUM(RealisasiPendapatan) as RealisasiPendapatan," +
                                "SUM(RealisasiOperasi) as RealisasiPegawai,SUM(RealisasiModal) as RealisasiBarangJasa,SUM(RealisasiBTT) as RealisasiBunga,SUM(RealisasiTransfer) as RealisasiSubsidi " +
                                " from " + namaView + "   inner join mUrusan on viewPerdaIIB.IDUrusan= mUrusan.ID  where viewPerdaIIB.btJenis  IN (1,2,3)  " +
                                " group by mUrusan.btKodeKategori,mUrusan.btKodeUrusan,mUrusan.sNamaUrusan  ";
                    SSQL = SSQL + "  UNION ";
                    SSQL = SSQL + " Select 4 as Level, '5' as Kelompok, 2 as KelompokBesar ,MpELAKSANAuRUSAN.btKodeKategoriPelaksana AS KodeKategori , " +
                                " MpELAKSANAuRUSAN.btKodeUrusanPELAKSANA  as KodeUrusan, Mskpd.id as KodeSKPD, " +
                                " 0 as KOdeRekening,  0 as Rek, Mskpd.sNamaskpd as Nama, " +
                               " SUM(AnggaranPendapatan) as AnggaranPendapatan, SUM(Anggaranoperasi) as AnggaranPegawai,SUM(AnggaranModal) as AnggaranBarangJasa," +
                                " SUM(AnggaranBTT) as AnggaranBunga,SUM(AnggaranTransfer) as AnggaranSubsidi,SUM(RealisasiPendapatan) as RealisasiPendapatan," +
                                "SUM(RealisasiOperasi) as RealisasiPegawai,SUM(RealisasiModal) as RealisasiBarangJasa,SUM(RealisasiBTT) as RealisasiBunga,SUM(RealisasiTransfer) as RealisasiSubsidi " +
                                " from " + namaView + "   inner join mSKPD  on viewPerdaIIB.iddINAS= mSKPD.ID  " +
                                " INNER JOIN MpELAKSANAURUSAN ON MpELAKSANAuRUSAN.iduRUSAN =viewPerdaIIB.iduRUSAN AND mPelaksanaUrusan.IDDInas =viewPerdaIIB.iddiNAS   " +
                                "  and viewPerdaIIB.iTAhun = MpELAKSANAURUSAN.iTahun  where viewPerdaIIB.btJenis  IN (1,2,3) " +
                                " group by MpELAKSANAuRUSAN.btKodeKategoriPelaksana , MpELAKSANAuRUSAN.btKodeUrusanPELAKSANA, Mskpd.id,Mskpd.sNamaskpd ";

                    SSQL = SSQL + "   ORDER BY KelompokBesar ,KodeKategori , KodeUrusan, KodeSKPD, KOdeRekening";


                }

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);


                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaRealisasi1_1()
                                {

                                    //Kode { set; get; }
                                    Level = DataFormat.GetInteger(dr["Level"]),
                                    //IDKategori { set; get; }
                                    IDUrusan = DataFormat.GetInteger(dr["kodeurusan"]),
                                    IDDInas = DataFormat.GetInteger(dr["kodeskpd"]),
                                    //root { set; get; }
                                    Kode = DataFormat.GetInteger(dr["Level"]) == 1 ? DataFormat.GetString(dr["Kelompok"]) : DataFormat.GetString(dr["Kelompok"]) + "." + (DataFormat.GetInteger(dr["Level"]) == 2 ? DataFormat.GetInteger(dr["Kodekategori"]).ToString("#") :
                                           (DataFormat.GetInteger(dr["Level"]) == 3 ? DataFormat.GetInteger(dr["Kodekategori"]).ToString("#") + "." + DataFormat.GetInteger(dr["KodeUrusan"]).ToString("##") :
                                           (DataFormat.GetInteger(dr["Level"]) == 4 ? DataFormat.GetInteger(dr["Kodekategori"]).ToString("#") + "." + DataFormat.GetInteger(dr["KodeUrusan"]).ToString("##") + "." + DataFormat.GetInteger(dr["kodeskpd"]).ToKodeDinas() :
                                            DataFormat.GetInteger(dr["Kodekategori"]).ToString("#") + "." + DataFormat.GetInteger(dr["KodeUrusan"]).ToString("##") + "." + DataFormat.GetInteger(dr["kodeskpd"]).ToKodeDinas() + "." + DataFormat.GetLong(dr["KodeRekening"]).ToKodeRekening(m_ProfileRekening)))), //GetKodeII(DataFormat.GetInteger(dr["Level"]),

                                    Nama = DataFormat.GetString(dr["Nama"]).ToUpper(),

                                    AnggaranPendapatan = DataFormat.GetDecimal(dr["AnggaranPendapatan"]).ToRupiahInReport(),
                                    AnggaranPegawai = DataFormat.GetDecimal(dr["AnggaranPegawai"]).ToRupiahInReport(),

                                    AnggaranBarangJasa = DataFormat.GetDecimal(dr["AnggaranBarangJasa"]).ToRupiahInReport(),
                                    AnggaranBunga = DataFormat.GetDecimal(dr["AnggaranBunga"]).ToRupiahInReport(),
                                    AnggaranSubsidi = DataFormat.GetDecimal(dr["AnggaranSubsidi"]).ToRupiahInReport(),
                                    AnggaranHibah = DataFormat.GetDecimal(dr["AnggaranHibah"]).ToRupiahInReport(),
                                    AnggaranBansos = DataFormat.GetDecimal(dr["AnggaranBansos"]).ToRupiahInReport(),
                                    AnggaranBagiHasil = DataFormat.GetDecimal(dr["AnggaranBagiHasil"]).ToRupiahInReport(),
                                    AnggaranBantuanKeuangan = DataFormat.GetDecimal(dr["AnggaranBantuanKeuangan"]).ToRupiahInReport(),
                                    AnggaranModal = DataFormat.GetDecimal(dr["AnggaranModal"]).ToRupiahInReport(),
                                    AnggaranTakTerduga = DataFormat.GetDecimal(dr["AnggaranTakTerduga"]).ToRupiahInReport(),
                                    AnggaranBelanja = DataFormat.GetDecimal(dr["AnggaranBelanja"]).ToRupiahInReport(),

                                    RealisasiPendapatan = DataFormat.GetDecimal(dr["RealisasiPendapatan"]).ToRupiahInReport(),
                                    RealisasiPegawai = DataFormat.GetDecimal(dr["RealisasiPegawai"]).ToRupiahInReport(),
                                    RealisasiBarangJasa = DataFormat.GetDecimal(dr["RealisasiBarangJasa"]).ToRupiahInReport(),
                                    RealisasiBunga = DataFormat.GetDecimal(dr["RealisasiBunga"]).ToRupiahInReport(),
                                    RealisasiSubsidi = DataFormat.GetDecimal(dr["RealisasiSubsidi"]).ToRupiahInReport(),
                                    RealisasiHibah = DataFormat.GetDecimal(dr["RealisasiHibah"]).ToRupiahInReport(),
                                    RealisasiBansos = DataFormat.GetDecimal(dr["RealisasiBansos"]).ToRupiahInReport(),
                                    RealisasiBagiHasil = DataFormat.GetDecimal(dr["RealisasiBagiHasil"]).ToRupiahInReport(),
                                    RealisasiBantuanKeuangan = DataFormat.GetDecimal(dr["RealisasiBantuanKeuangan"]).ToRupiahInReport(),
                                    RealisasiModal = DataFormat.GetDecimal(dr["RealisasiModal"]).ToRupiahInReport(),
                                    RealisasiTakTerduga = DataFormat.GetDecimal(dr["RealisasiTakTerduga"]).ToRupiahInReport(),
                                    RealisasiBelanja = DataFormat.GetDecimal(dr["RealisasiBelanja"]).ToRupiahInReport(),
                                    SelisihBelanja = (DataFormat.GetDecimal(dr["AnggaranBelanja"]) - DataFormat.GetDecimal(dr["RealisasiBelanja"])).ToRupiahInReport(),
                                    PersenBelanja = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["AnggaranBelanja"]), DataFormat.GetDecimal(dr["RealisasiBelanja"])),
                                    //                         JumlahBelanjaMurni = (DataFormat.GetDecimal(dr["Anggaran"])).ToRupiahInReport(),
                                    //                         JumlahBelanja = (DataFormat.GetDecimal(dr["Realisasi"])).ToRupiahInReport(),
                                    //SelisihBelanja = (DataFormat.GetDecimal(dr["Realisasi"]) - DataFormat.GetDecimal(dr["Anggaran"])).ToRupiahInReport(),
                                    //PersenBelanja = DataFormat.GetProsentaseRealisasi(DataFormat.GetDecimal(dr["Realisasi"]), DataFormat.GetDecimal(dr["Anggaran"]))
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
        private string CreateViewPerdaII_BBRealisasi(ParameterLaporan _p, bool create)
        {
            string namaView = "viewPerdaIIB" + _p.NamaUser.Trim();
            HapusView(namaView);


            if (create == true)
            {
                //GetKolom(_p.Tahap);
                if (_p.JenisRekening == 1)
                {
                    SSQL = "CREATE VIEW " + namaView + " AS Select tAnggaranRekening_A.iTAhun, tAnggaranRekening_A.IDDInas,tAnggaranRekening_A.IDUrusan as IDurusan, " +
                        " 0 as IDProgram, 0 as IDKegiatan, tAnggaranRekening_A.IIDRekening, tAnggaranRekening_A.btJenis," +
                        " case when iidRekening like '51%' or iidRekening like '521%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranPegawai,   " +
                         " case when iidRekening like '522%' and iidRekening not like '5222%'  Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranBarangJasa,   " +
                         " case when iidRekening like '5222%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranPemeliharaan," +
                         " case when iidRekening like '523%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranModal,   " +
                         " case when iidRekening like '5%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS JumlahBelanja,   " +
                         " case when iidRekening like '51%' or iidRekening like '521%' Then tAnggaranRekening_A.cRealisasi ELSE 0 END AS RealisasiPegawai,   " +
                         " case when iidRekening like '522%' and iidRekening not like '5222%' Then tAnggaranRekening_A.cRealisasi ELSE 0 END AS RealisasiBarangJasa,   " +
                         " case when iidRekening like '5222%' Then tAnggaranRekening_A.cRealisasi ELSE 0 END AS RealisasiPemeliharaan,   " +
                         " case when iidRekening like '523%' Then tAnggaranRekening_A.cRealisasi  ELSE 0 END AS RealisasiModal,   " +
                         " case when iidRekening like '5%' Then tAnggaranRekening_A.cRealisasi  ELSE 0 END AS RealisasiBelanja   " +
                         " from tAnggaranRekening_A INNER JOIN mSKPD ON tAnggaranRekening_A.IDDInas = mSKPD.ID inner JOIn tKegiatan_A On tAnggaranRekening_A.IDKegiatan= tKegiatan_A.IDkegiatan and tAnggaranRekening_A.IDDInas= tKegiatan_A.IDDinas " +
                        " AND tAnggaranRekening_A.iTahun = tKegiatan_A.iTahun AND tAnggaranRekening_A.btJenis = tKegiatan_A.btJenis where tAnggaranRekening_A.btJenis  in (1,2,4,5)    " +
                        " and mskpd.root = 1 and tAnggaranRekening_A.iTahun = " + _p.Tahun.ToString() +
                    " UNION ALL Select tAnggaranRekening_A.iTAhun, tAnggaranRekening_A.IDDInas,tAnggaranRekening_A.IDUrusan as IDurusan, tAnggaranRekening_A.IDProgram, tAnggaranRekening_A.IDKegiatan, tAnggaranRekening_A.IIDRekening, " +
                                    "tAnggaranRekening_A.btJenis," +
                        " case when iidRekening like '51%' or iidRekening like '521%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranPegawai,   " +
                         " case when iidRekening like '522%' and iidRekening not like '5222%'  Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranBarangJasa,   " +
                         " case when iidRekening like '5222%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranPemeliharaan," +
                         " case when iidRekening like '523%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranModal,   " +
                         " case when iidRekening like '5%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS JumlahBelanja,   " +
                         " case when iidRekening like '51%' or iidRekening like '521%' Then tAnggaranRekening_A.cRealisasi ELSE 0 END AS RealisasiPegawai,   " +
                         " case when iidRekening like '522%' and iidRekening not like '5222%' Then tAnggaranRekening_A.cRealisasi ELSE 0 END AS RealisasiBarangJasa,   " +
                         " case when iidRekening like '5222%' Then tAnggaranRekening_A.cRealisasi ELSE 0 END AS RealisasiPemeliharaan,   " +
                         " case when iidRekening like '523%' Then tAnggaranRekening_A.cRealisasi  ELSE 0 END AS RealisasiModal,   " +
                         " case when iidRekening like '5%' Then tAnggaranRekening_A.cRealisasi  ELSE 0 END AS RealisasiBelanja   " +
                         " from tAnggaranRekening_A INNER JOIN mSKPD ON tAnggaranRekening_A.IDDInas = mSKPD.ID INNER JOIN tkegiatan_A ON " +
                            " tAnggaranRekening_A.iTahun = tKegiatan_A.iTahun and tAnggaranRekening_A.IDUrusan = tKegiatan_A.IDUrusan and tAnggaranRekening_A.IDDInas = tKegiatan_A.IDDInas and  tAnggaranRekening_A.IDKegiatan = tKegiatan_A.IDKegiatan and  tAnggaranRekening_A.btJenis = tKegiatan_A.btJenis where mskpd.root = 1  and tAnggaranRekening_A.btJenis = 3  and tAnggaranRekening_A.iTahun = " + _p.Tahun.ToString();
                }
                else
                {
                    SSQL = "CREATE VIEW " + namaView + " AS Select tAnggaranRekening_A.iTAhun, tAnggaranRekening_A.IDDInas,tAnggaranRekening_A.IDUrusan as IDurusan, " +
                      " 0 as IDProgram, 0 as IDKegiatan, tAnggaranRekening_A.IIDRekening, tAnggaranRekening_A.btJenis," +
                      " case when iidRekening64 like '51%' or iidRekening like '521%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranPegawai,   " +
                       " case when iidRekening64 like '52%' and iidRekening not like '5222%'  Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranBarangJasa,   " +
                       " case when iidRekening64 like '53%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranPemeliharaan," +
                       " case when iidRekening64 like '53%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranModal,   " +
                       " case when iidRekening64 like '5%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS JumlahBelanja,   " +
                       " case when iidRekening64 like '51%' or iidRekening like '521%' Then tAnggaranRekening_A.cRealisasi ELSE 0 END AS RealisasiPegawai,   " +
                       " case when iidRekening64 like '52%' and iidRekening not like '5222%' Then tAnggaranRekening_A.cRealisasi ELSE 0 END AS RealisasiBarangJasa,   " +
                       " case when iidRekening64 like '53%' Then tAnggaranRekening_A.cRealisasi ELSE 0 END AS RealisasiPemeliharaan,   " +
                       " case when iidRekening64 like '53%' Then tAnggaranRekening_A.cRealisasi  ELSE 0 END AS RealisasiModal,   " +
                       " case when iidRekening64 like '5%' Then tAnggaranRekening_A.cRealisasi  ELSE 0 END AS RealisasiBelanja   " +
                       " from tAnggaranRekening_A INNER JOIN mSKPD ON tAnggaranRekening_A.IDDInas = mSKPD.ID inner JOIn tKegiatan_A On tAnggaranRekening_A.IDKegiatan= tKegiatan_A.IDkegiatan and tAnggaranRekening_A.IDDInas= tKegiatan_A.IDDinas " +
                      " AND tAnggaranRekening_A.iTahun = tKegiatan_A.iTahun AND tAnggaranRekening_A.btJenis = tKegiatan_A.btJenis where tAnggaranRekening_A.btJenis  in (1,2,4,5)    " +
                      " and mskpd.root = 1 and tAnggaranRekening_A.iTahun = " + _p.Tahun.ToString() +
                  " UNION ALL Select tAnggaranRekening_A.iTAhun, tAnggaranRekening_A.IDDInas,tAnggaranRekening_A.IDUrusan as IDurusan, tAnggaranRekening_A.IDProgram, tAnggaranRekening_A.IDKegiatan, tAnggaranRekening_A.IIDRekening, " +
                                  "tAnggaranRekening_A.btJenis," +
                      " case when iidRekening64 like '51%' or iidRekening like '521%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranPegawai,   " +
                       " case when iidRekening64 like '52%' and iidRekening not like '5222%'  Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranBarangJasa,   " +
                       " case when iidRekening64 like '53%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranPemeliharaan," +
                       " case when iidRekening64 like '53%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranModal,   " +
                       " case when iidRekening64 like '5%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS JumlahBelanja,   " +
                       " case when iidRekening64 like '51%' or iidRekening like '521%' Then tAnggaranRekening_A.cRealisasi ELSE 0 END AS RealisasiPegawai,   " +
                       " case when iidRekening64 like '52%' and iidRekening not like '5222%' Then tAnggaranRekening_A.cRealisasi ELSE 0 END AS RealisasiBarangJasa,   " +
                       " case when iidRekening64 like '53%' Then tAnggaranRekening_A.cRealisasi ELSE 0 END AS RealisasiPemeliharaan,   " +
                       " case when iidRekening64 like '53%' Then tAnggaranRekening_A.cRealisasi  ELSE 0 END AS RealisasiModal,   " +
                       " case when iidRekening64 like '5%' Then tAnggaranRekening_A.cRealisasi  ELSE 0 END AS RealisasiBelanja   " +
                       " from tAnggaranRekening_A INNER JOIN mSKPD ON tAnggaranRekening_A.IDDInas = mSKPD.ID INNER JOIN tkegiatan_A ON " +
                          " tAnggaranRekening_A.iTahun = tKegiatan_A.iTahun and tAnggaranRekening_A.IDUrusan = tKegiatan_A.IDUrusan and tAnggaranRekening_A.IDDInas = tKegiatan_A.IDDInas and  tAnggaranRekening_A.IDKegiatan = tKegiatan_A.IDKegiatan and  tAnggaranRekening_A.btJenis = tKegiatan_A.btJenis where mskpd.root = 1  and tAnggaranRekening_A.btJenis = 3  and tAnggaranRekening_A.iTahun = " + _p.Tahun.ToString();

                }
                _dbHelper.ExecuteNonQuery(SSQL);

            }
            return namaView;
        }
        private string CreateViewPerdaII_BBRealisasi2(ParameterLaporan _p, bool create)
        {
            string namaView = "viewPerdaIIB" + _p.NamaUser.Trim();
            HapusView(namaView);
            string sTanggal = "'" + _p.dTanggal.Month.ToString() + "/" + _p.dTanggal.Day.ToString() + "/" + _p.dTanggal.Year.ToString() + "'";


            if (create == true)
            {

                //GetKolom(_p.Tahap);
                if (_p.Tahun <= 2020)
                {
                    if (_p.JenisRekening == 1)
                    {

                        SSQL = " CREATE VIEW " + namaView + " AS  Select A.iTAhun, A.IDDInas, " +
    "A.IDUrusan as IDurusan,  0 as IDProgram, 0 as IDKegiatan, A.IIDRekening, A.btJenis, " +
    " case when iidRekening like '4%' Then A.cJumlahABT ELSE 0  end as anggaranpendapatan," +
    " case when iidRekening like '511%' or iidRekening like '521%' Then A.cJumlahABT ELSE 0 END AS AnggaranPegawai,    " +
    " case when iidRekening like '522%' Then A.cJumlahABT ELSE 0 END AS AnggaranBarangJasa, " +
    " case when iidRekening like '512%' Then A.cJumlahABT ELSE 0 END AS AnggaranBunga, " +
    " case when iidRekening like '513%' Then A.cJumlahABT ELSE 0 END AS AnggaranSubsidi, " +
    " case when iidRekening like '514%' Then A.cJumlahABT ELSE 0 END AS AnggaranHibah, " +
    " case when iidRekening like '515%' Then A.cJumlahABT ELSE 0 END AS AnggaranBansos, " +
    " case when iidRekening like '516%' Then A.cJumlahABT ELSE 0 END AS AnggaranBagihasil, " +
    " case when iidRekening like '517%' Then A.cJumlahABT ELSE 0 END AS AnggaranBantuanKeuangan, " +
    " case when iidRekening like '518%' Then A.cJumlahABT ELSE 0 END AS AnggaranTakterduga, " +
    " case when iidRekening like '523%' Then A.cJumlahABT ELSE 0 END AS AnggaranModal," +
    " case when iidRekening like '5%' Then A.cJumlahABT ELSE 0 END AS AnggaranBelanja," +
    " case when iidRekening like '1%' Then A.cRealisasi ELSE 0 end as realisasipendapatan," +
    " case when iidRekening like '511%' or iidRekening like '521%' Then A.cRealisasi ELSE 0 END AS realisasiPegawai, " +
    " case when iidRekening like '522%' Then dbo.GetRealisasi (A.IDDInas, A.IDSUBKEGIATAN,A.IIDREkening, sTanggal) ELSE 0 END AS realisasiBarangJasa, " +
    " case when iidRekening like '512%' Then dbo.GetRealisasi (A.IDDInas, A.IDSUBKEGIATAN,A.IIDREkening, sTanggal)  ELSE 0 END AS realisasiBunga, " +
    " case when iidRekening like '513%' Then dbo.GetRealisasi (A.IDDInas, A.IDSUBKEGIATAN,A.IIDREkening, sTanggal) ELSE 0 END AS realisasiSubsidi, " +
    " case when iidRekening like '514%' Then dbo.GetRealisasi (A.IDDInas, A.IDSUBKEGIATAN,A.IIDREkening, sTanggal) ELSE 0 END AS realisasiHibah, " +
    " case when iidRekening like '515%' Then dbo.GetRealisasi (A.IDDInas, A.IDSUBKEGIATAN,A.IIDREkening, sTanggal) ELSE 0 END AS realisasiBansos, " +
    " case when iidRekening like '516%' Then dbo.GetRealisasi (A.IDDInas, A.IDSUBKEGIATAN,A.IIDREkening, sTanggal) ELSE 0 END AS realisasiBagihasil, " +
    " case when iidRekening like '517%' Then dbo.GetRealisasi (A.IDDInas, A.IDSUBKEGIATAN,A.IIDREkening, sTanggal) ELSE 0 END AS realisasiBantuanKeuangan," +
    " case when iidRekening like '518%' Then dbo.GetRealisasi (A.IDDInas, A.IDSUBKEGIATAN,A.IIDREkening, sTanggal) ELSE 0 END AS realisasiTakterduga, " +
    " case when iidRekening like '523%' Then dbo.GetRealisasi (A.IDDInas, A.IDSUBKEGIATAN,A.IIDREkening, sTanggal) ELSE 0 END AS realisasiModal," +
    " case when iidRekening like '5%' Then dbo.GetRealisasi (A.IDDInas, A.IDSUBKEGIATAN,A.IIDREkening, sTanggal) ELSE 0 END AS realisasiBelanja" +
    "  from tanggaranrekening_A A INNER JOIN mSKPD ON A.IDDInas = mSKPD.ID INNER JOIN tkegiatan_A " +
    "  ON  A.iTahun = tKegiatan_A.iTahun and A.IDUrusan = tKegiatan_A.IDUrusan and A.IDDInas = tKegiatan_A.IDDInas " +
    "  and  A.IDKegiatan = tKegiatan_A.IDKegiatan and  A.btJenis = tKegiatan_A.btJenis where mskpd.root = 1  and A.iTahun = " + Tahun.ToString();





                    }
                    else
                    {
                        SSQL = "CREATE VIEW " + namaView + " AS Select tAnggaranRekening_A.iTAhun, tAnggaranRekening_A.IDDInas,tAnggaranRekening_A.IDUrusan as IDurusan, " +
                          " 0 as IDProgram, 0 as IDKegiatan, tAnggaranRekening_A.IIDRekening, tAnggaranRekening_A.btJenis," +
                          " case when iidRekening64 like '51%' or iidRekening like '521%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranPegawai,   " +
                           " case when iidRekening64 like '52%' and iidRekening not like '5222%'  Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranBarangJasa,   " +
                           " case when iidRekening64 like '53%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranPemeliharaan," +
                           " case when iidRekening64 like '53%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranModal,   " +
                           " case when iidRekening64 like '5%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS JumlahBelanja,   " +
                           " case when iidRekening64 like '51%' or iidRekening like '521%' Then tAnggaranRekening_A.cRealisasi ELSE 0 END AS RealisasiPegawai,   " +
                           " case when iidRekening64 like '52%' and iidRekening not like '5222%' Then tAnggaranRekening_A.cRealisasi ELSE 0 END AS RealisasiBarangJasa,   " +
                           " case when iidRekening64 like '53%' Then tAnggaranRekening_A.cRealisasi ELSE 0 END AS RealisasiPemeliharaan,   " +
                           " case when iidRekening64 like '53%' Then tAnggaranRekening_A.cRealisasi  ELSE 0 END AS RealisasiModal,   " +
                           " case when iidRekening64 like '5%' Then tAnggaranRekening_A.cRealisasi  ELSE 0 END AS RealisasiBelanja   " +
                           " from tAnggaranRekening_A INNER JOIN mSKPD ON tAnggaranRekening_A.IDDInas = mSKPD.ID inner JOIn tKegiatan_A On tAnggaranRekening_A.IDKegiatan= tKegiatan_A.IDkegiatan and tAnggaranRekening_A.IDDInas= tKegiatan_A.IDDinas " +
                          " AND tAnggaranRekening_A.iTahun = tKegiatan_A.iTahun AND tAnggaranRekening_A.btJenis = tKegiatan_A.btJenis where tAnggaranRekening_A.btJenis  in (1,2,4,5)    " +
                          " and mskpd.root = 1 and tAnggaranRekening_A.iTahun = " + _p.Tahun.ToString() +
                      " UNION ALL Select tAnggaranRekening_A.iTAhun, tAnggaranRekening_A.IDDInas,tAnggaranRekening_A.IDUrusan as IDurusan, tAnggaranRekening_A.IDProgram, tAnggaranRekening_A.IDKegiatan, tAnggaranRekening_A.IIDRekening, " +
                                      "tAnggaranRekening_A.btJenis," +
                          " case when iidRekening64 like '51%' or iidRekening like '521%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranPegawai,   " +
                           " case when iidRekening64 like '52%' and iidRekening not like '5222%'  Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranBarangJasa,   " +
                           " case when iidRekening64 like '53%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranPemeliharaan," +
                           " case when iidRekening64 like '53%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS AnggaranModal,   " +
                           " case when iidRekening64 like '5%' Then tAnggaranRekening_A.cJumlahRKAP ELSE 0 END AS JumlahBelanja,   " +
                           " case when iidRekening64 like '51%' or iidRekening like '521%' Then tAnggaranRekening_A.cRealisasi ELSE 0 END AS RealisasiPegawai,   " +
                           " case when iidRekening64 like '52%' and iidRekening not like '5222%' Then tAnggaranRekening_A.cRealisasi ELSE 0 END AS RealisasiBarangJasa,   " +
                           " case when iidRekening64 like '53%' Then tAnggaranRekening_A.cRealisasi ELSE 0 END AS RealisasiPemeliharaan,   " +
                           " case when iidRekening64 like '53%' Then tAnggaranRekening_A.cRealisasi  ELSE 0 END AS RealisasiModal,   " +
                           " case when iidRekening64 like '5%' Then tAnggaranRekening_A.cRealisasi  ELSE 0 END AS RealisasiBelanja   " +
                           " from tAnggaranRekening_A INNER JOIN mSKPD ON tAnggaranRekening_A.IDDInas = mSKPD.ID INNER JOIN tkegiatan_A ON " +
                              " tAnggaranRekening_A.iTahun = tKegiatan_A.iTahun and tAnggaranRekening_A.IDUrusan = tKegiatan_A.IDUrusan and tAnggaranRekening_A.IDDInas = tKegiatan_A.IDDInas and  tAnggaranRekening_A.IDKegiatan = tKegiatan_A.IDKegiatan and  tAnggaranRekening_A.btJenis = tKegiatan_A.btJenis where mskpd.root = 1  and tAnggaranRekening_A.btJenis = 3  and tAnggaranRekening_A.iTahun = " + _p.Tahun.ToString();

                    }
                }
                else
                {

                    SSQL = " CREATE VIEW " + namaView + " AS  Select A.iTAhun, A.IDDInas, " +
    "A.IDUrusan as IDurusan,  A.IDprogram as IDProgram, A.IDKegiatan as IDKegiatan,A.IDSUbKegiatan as idsubkegiatan, A.IIDRekening, A.btJenis, " +
    " case when iidRekening like '4%' Then A.cJumlahGeser ELSE 0  end as anggaranpendapatan," +
    " case when iidRekening like '51%' Then A.cJumlahGeser ELSE 0 END AS AnggaranOperasi,    " +
    " case when iidRekening like '52%' Then A.cJumlahGeser ELSE 0 END AS AnggaranModal, " +
    " case when iidRekening like '53%' Then A.cJumlahGeser ELSE 0 END AS AnggaranBTT, " +
    " case when iidRekening like '54%' Then A.cJumlahGeser ELSE 0 END AS ANggaranTransfer, " +
    " case when iidRekening like '5%' Then A.cJumlahGeser ELSE 0 END AS AnggaranBelanja," +
    " case when iidRekening like '4%' Then dbo.GetRealisasiSTS (A.IDDInas, A.IIDREkening, '9/15/2021') ELSE 0 end as realisasipendapatan," +
    " case when iidRekening like '51%' Then dbo.GetRealisasi (A.IDDInas, A.IDSUBKEGIATAN,A.IIDREkening, '9/15/2021') ELSE 0 END AS realisasiOperasi, " +
    " case when iidRekening like '52%' Then dbo.GetRealisasi (A.IDDInas, A.IDSUBKEGIATAN,A.IIDREkening, '9/15/2021') ELSE 0 END AS realisasiModal, " +
    " case when iidRekening like '53%' Then dbo.GetRealisasi (A.IDDInas, A.IDSUBKEGIATAN,A.IIDREkening, '9/15/2021')  ELSE 0 END AS realisasiBTT, " +
    " case when iidRekening like '54%' Then dbo.GetRealisasi (A.IDDInas, A.IDSUBKEGIATAN,A.IIDREkening, '9/15/2021') ELSE 0 END AS realisasiTransfer, " +
    " case when iidRekening like '5%' Then dbo.GetRealisasi (A.IDDInas, A.IDSUBKEGIATAN,A.IIDREkening, '9/15/2021') ELSE 0 END AS realisasiBelanja" +
    "  from tanggaranrekening_A A INNER JOIN mSKPD ON A.IDDInas = mSKPD.ID INNER JOIN tSUbKegiatan " +
    "  ON  A.iTahun = tSUbKegiatan.iTahun and A.IDUrusan = tSUbKegiatan.IDUrusan and A.IDDInas = tSUbKegiatan.IDDInas " +
    "  and  A.IDKegiatan = tSUbKegiatan.IDKegiatan and  A.IDSUbKegiatan = tSubKegiatan.IDSUbKegiatan  and  A.btJenis = tSubKegiatan.btJenis  where mskpd.root = 1  and A.iTahun = " + Tahun.ToString();



                }
                _dbHelper.ExecuteNonQuery(SSQL);

            }
            return namaView;
        }

        public List<PerdaIIB> GetPerdaIIRealisasiB(ParameterLaporan _p)
        {
            //realisasai
            List<PerdaIIB> _lst = new List<PerdaIIB>();
            try
            {
                // Cek View 
                // CreateviewPerdaIIB (_p.Tahap);

                // var query = from q in lstDB  group by ()

                string namaView = CreateViewPerdaII_BBRealisasi(_p, true);

                // PENDAPATAN 

                //GetKodeII
                SSQL = "";
                SSQL = SSQL + " Select 1 as Level,'5' as Kelompok,  2 as KelompokBesar,0 as KodeKategori,0 as KodeUrusan,0 as KodeSKPD, 0 as KOdeRekening,0 as Rek,  'BelANJA' as Nama, " +
                            " SUM(AnggaranPegawai) as AnggaranPegawai,SUM(AnggaranBarangJasa) as AnggaranBarangJasa,SUM(AnggaranPemeliharaan) as AnggaranPemeliharaan,SUM(AnggaranModal) as AnggaranModal," +
                            "SUM(RealisasiPegawai) as RealisasiPegawai,SUM(RealisasiBarangJasa) as RealisasiBarangJasa,SUM(RealisasiPemeliharaan) as RealisasiPemeliharaan,SUM(RealisasiModal)   as RealisasiModal,SUM(JumlahBelanja) as JumlahBelanja,SUM(RealisasiBelanja) as RealisasiBelanja " +
                            " from viewPerdaIIB  where btJenis in  (2,3) ";
                SSQL = SSQL + " UNION  ";

                SSQL = SSQL + " Select 2 as Level, '5' as Kelompok, 2 as KelompokBesar,mKategori.btKodeKategori  as KodeKategori,0 as KodeUrusan,0 as KodeSKPD, 0 as KOdeRekening, 0 as Rek, mKategori.sNamaKategori as Nama, " +
                " SUM(AnggaranPegawai) as AnggaranPegawai,SUM(AnggaranBarangJasa) as AnggaranBarangJasa,SUM(AnggaranPemeliharaan) as AnggaranPemeliharaan,SUM(AnggaranModal) as AnggaranModal," +
                            "SUM(RealisasiPegawai) as RealisasiPegawai,SUM(RealisasiBarangJasa) as RealisasiBarangJasa,SUM(RealisasiPemeliharaan) as RealisasiPemeliharaan,SUM(RealisasiModal) as RealisasiModal,SUM(JumlahBelanja) as JumlahBelanja,SUM(RealisasiBelanja) as RealisasiBelanja" +
                            " from viewPerdaIIB   inner join mKategori on viewPerdaIIB.IDUrusan/100= mKategori.btKodeKategori where viewPerdaIIB.btJenis IN (2,3) " +
                            " group by mKategori.btKodeKategori,mKategori.sNamaKategori ";

                SSQL = SSQL + "  UNION ";
                SSQL = SSQL + " Select 3 as Level, '5' as Kelompok, 2 as KelompokBesar ,mUrusan.btKodeKategori as KodeKategori, mUrusan.btKodeUrusan as KodeUrusan, 0 as KodeSKPD, 0 as KOdeRekening, 0 as Rek,  mUrusan.sNamaUrusan as Nama, " +
                            " SUM(AnggaranPegawai) as AnggaranPegawai,SUM(AnggaranBarangJasa) as AnggaranBarangJasa,SUM(AnggaranPemeliharaan) as AnggaranPemeliharaan,SUM(AnggaranModal) as AnggaranModal," +
                            "SUM(RealisasiPegawai) as RealisasiPegawai,SUM(RealisasiBarangJasa) as RealisasiBarangJasa,SUM(RealisasiPemeliharaan) as RealisasiPemeliharaan,SUM(RealisasiModal) as RealisasiModal ,SUM(JumlahBelanja) as JumlahBelanja,SUM(RealisasiBelanja) as RealisasiBelanja" +
                            " from viewPerdaIIB   inner join mUrusan on viewPerdaIIB.IDUrusan= mUrusan.ID  where viewPerdaIIB.btJenis  IN (2,3)  " +
                            " group by mUrusan.btKodeKategori,mUrusan.btKodeUrusan,mUrusan.sNamaUrusan  ";
                SSQL = SSQL + "  UNION ";
                SSQL = SSQL + " Select 4 as Level, '5' as Kelompok, 2 as KelompokBesar ,MpELAKSANAuRUSAN.btKodeKategoriPelaksana AS KodeKategori , " +
                            " MpELAKSANAuRUSAN.btKodeUrusanPELAKSANA  as KodeUrusan, Mskpd.id as KodeSKPD, " +
                            " 0 as KOdeRekening,  0 as Rek, Mskpd.sNamaskpd as Nama, " +
                            " SUM(AnggaranPegawai) as AnggaranPegawai,SUM(AnggaranBarangJasa) as AnggaranBarangJasa,SUM(AnggaranPemeliharaan) as AnggaranPemeliharaan,SUM(AnggaranModal) as AnggaranModal," +
                            "SUM(RealisasiPegawai) as RealisasiPegawai,SUM(RealisasiBarangJasa) as RealisasiBarangJasa,SUM(RealisasiPemeliharaan) as RealisasiPemeliharaan,SUM(RealisasiModal) as RealisasiModal,SUM(JumlahBelanja) as JumlahBelanja,SUM(RealisasiBelanja) as RealisasiBelanja " +
                            " from viewPerdaIIB  inner join mSKPD  on viewPerdaIIB.iddINAS= mSKPD.ID  " +
                            " INNER JOIN MpELAKSANAURUSAN ON MpELAKSANAuRUSAN.iduRUSAN =viewPerdaIIB.iduRUSAN AND mPelaksanaUrusan.IDDInas =viewPerdaIIB.iddiNAS   " +
                            "  and viewPerdaIIB.iTAhun = MpELAKSANAURUSAN.iTahun  where viewPerdaIIB.btJenis  IN (2,3) " +
                            " group by MpELAKSANAuRUSAN.btKodeKategoriPelaksana , MpELAKSANAuRUSAN.btKodeUrusanPELAKSANA, Mskpd.id,Mskpd.sNamaskpd ";

                SSQL = SSQL + "   ORDER BY KelompokBesar ,KodeKategori , KodeUrusan, KodeSKPD, KOdeRekening";




                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIIB()
                                {

                                    //Kode { set; get; }
                                    Level = DataFormat.GetInteger(dr["Level"]),
                                    //IDKategori { set; get; }
                                    IDUrusan = DataFormat.GetInteger(dr["kodeurusan"]),
                                    IDDInas = DataFormat.GetInteger(dr["kodeskpd"]),
                                    //root { set; get; }
                                    Kode = DataFormat.GetInteger(dr["Level"]) == 1 ? DataFormat.GetString(dr["Kelompok"]) : DataFormat.GetString(dr["Kelompok"]) + "." + (DataFormat.GetInteger(dr["Level"]) == 2 ? DataFormat.GetInteger(dr["Kodekategori"]).ToString("#") :
                                           (DataFormat.GetInteger(dr["Level"]) == 3 ? DataFormat.GetInteger(dr["Kodekategori"]).ToString("#") + "." + DataFormat.GetInteger(dr["KodeUrusan"]).ToString("##") :
                                           (DataFormat.GetInteger(dr["Level"]) == 4 ? DataFormat.GetInteger(dr["Kodekategori"]).ToString("#") + "." + DataFormat.GetInteger(dr["KodeUrusan"]).ToString("##") + "." + DataFormat.GetInteger(dr["kodeskpd"]).ToKodeDinas() :
                                            DataFormat.GetInteger(dr["Kodekategori"]).ToString("#") + "." + DataFormat.GetInteger(dr["KodeUrusan"]).ToString("##") + "." + DataFormat.GetInteger(dr["kodeskpd"]).ToKodeDinas() + "." + DataFormat.GetLong(dr["KodeRekening"]).ToKodeRekening(m_ProfileRekening)))), //GetKodeII(DataFormat.GetInteger(dr["Level"]),

                                    Nama = DataFormat.GetString(dr["Nama"]).ToUpper(),

                                    AnggaranPegawai = DataFormat.GetDecimal(dr["AnggaranPegawai"]).ToRupiahInReport(),
                                    AnggaranBarangJasa = DataFormat.GetDecimal(dr["AnggaranBarangJasa"]).ToRupiahInReport(),
                                    AnggaranPemeliharaan = DataFormat.GetDecimal(dr["AnggaranPemeliharaan"]).ToRupiahInReport(),
                                    AnggaranModal = DataFormat.GetDecimal(dr["AnggaranModal"]).ToRupiahInReport(),
                                    JumlahAnggaran = DataFormat.GetDecimal(dr["JumlahBelanja"]).ToRupiahInReport(),
                                    //JumlahBelanja = DataFormat.GetInteger(dr["kodeurusan"]),


                                    //PendapatanMurni = DataFormat.GetInteger(dr["kodeurusan"]),
                                    RealisasiPegawai = DataFormat.GetDecimal(dr["RealisasiPegawai"]).ToRupiahInReport(),
                                    RealisasiBarangJasa = DataFormat.GetDecimal(dr["RealisasiBarangJasa"]).ToRupiahInReport(),
                                    RealisasiPemeliharaan = DataFormat.GetDecimal(dr["RealisasiPemeliharaan"]).ToRupiahInReport(),
                                    RealisasiModal = DataFormat.GetDecimal(dr["RealisasiModal"]).ToRupiahInReport(),
                                    JumlahRealisasi = DataFormat.GetDecimal(dr["RealisasiBelanja"]).ToRupiahInReport(),
                                    SelisihBelanja = (DataFormat.GetDecimal(dr["JumlahBelanja"]) - DataFormat.GetDecimal(dr["RealisasiBelanja"])).ToRupiahInReport(),
                                    PersenBelanja = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["JumlahBelanja"]), DataFormat.GetDecimal(dr["RealisasiBelanja"]))
                                    //                            JumlahBelanjaMurni = (DataFormat.GetDecimal(dr["Anggaran"])).ToRupiahInReport(),
                                    //                            JumlahBelanja = (DataFormat.GetDecimal(dr["Realisasi"])).ToRupiahInReport(),
                                    //                            SelisihBelanja = (DataFormat.GetDecimal(dr["Realisasi"]) - DataFormat.GetDecimal(dr["Anggaran"])).ToRupiahInReport(),
                                    //                            PersenBelanja = DataFormat.GetProsentaseRealisasi(DataFormat.GetDecimal(dr["Realisasi"]), DataFormat.GetDecimal(dr["Anggaran"]))
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
        //public List<PerdaIIB> GetPerdaIIRealisasiB2(ParameterLaporan _p)
        //{
        //    //realisasai
        //    List<PerdaIIB> _lst = new List<PerdaIIB>();
        //    try
        //    {
        //        // Cek View 
        //        // CreateviewPerdaIIB (_p.Tahap);

        //        // var query = from q in lstDB  group by ()

        //        string namaView = CreateViewPerdaII_BBRealisasi2(_p, true);

        //        // PENDAPATAN 

        //        //GetKodeII
        //        SSQL = "";
        //        SSQL = SSQL + " Select 1 as Level,'5' as Kelompok,  2 as KelompokBesar,0 as KodeKategori,0 as KodeUrusan,0 as KodeSKPD, 0 as KOdeRekening,0 as Rek,  'BelANJA' as Nama, " +
        //                    " SUM(AnggaranPegawai) as AnggaranPegawai,SUM(AnggaranBarangJasa) as AnggaranBarangJasa,SUM(AnggaranPemeliharaan) as AnggaranPemeliharaan,SUM(AnggaranModal) as AnggaranModal," +
        //                    "SUM(RealisasiPegawai) as RealisasiPegawai,SUM(RealisasiBarangJasa) as RealisasiBarangJasa,SUM(RealisasiPemeliharaan) as RealisasiPemeliharaan,SUM(RealisasiModal)   as RealisasiModal,SUM(JumlahBelanja) as JumlahBelanja,SUM(RealisasiBelanja) as RealisasiBelanja " +
        //                    " from viewPerdaIIB  where btJenis in  (2,3) ";
        //        SSQL = SSQL + " UNION  ";

        //        SSQL = SSQL + " Select 2 as Level, '5' as Kelompok, 2 as KelompokBesar,mKategori.btKodeKategori  as KodeKategori,0 as KodeUrusan,0 as KodeSKPD, 0 as KOdeRekening, 0 as Rek, mKategori.sNamaKategori as Nama, " +
        //        " SUM(AnggaranPegawai) as AnggaranPegawai,SUM(AnggaranBarangJasa) as AnggaranBarangJasa,SUM(AnggaranPemeliharaan) as AnggaranPemeliharaan,SUM(AnggaranModal) as AnggaranModal," +
        //                    "SUM(RealisasiPegawai) as RealisasiPegawai,SUM(RealisasiBarangJasa) as RealisasiBarangJasa,SUM(RealisasiPemeliharaan) as RealisasiPemeliharaan,SUM(RealisasiModal) as RealisasiModal,SUM(JumlahBelanja) as JumlahBelanja,SUM(RealisasiBelanja) as RealisasiBelanja" +
        //                    " from viewPerdaIIB   inner join mKategori on viewPerdaIIB.IDUrusan/100= mKategori.btKodeKategori where viewPerdaIIB.btJenis IN (2,3) " +
        //                    " group by mKategori.btKodeKategori,mKategori.sNamaKategori ";

        //        SSQL = SSQL + "  UNION ";
        //        SSQL = SSQL + " Select 3 as Level, '5' as Kelompok, 2 as KelompokBesar ,mUrusan.btKodeKategori as KodeKategori, mUrusan.btKodeUrusan as KodeUrusan, 0 as KodeSKPD, 0 as KOdeRekening, 0 as Rek,  mUrusan.sNamaUrusan as Nama, " +
        //                    " SUM(AnggaranPegawai) as AnggaranPegawai,SUM(AnggaranBarangJasa) as AnggaranBarangJasa,SUM(AnggaranPemeliharaan) as AnggaranPemeliharaan,SUM(AnggaranModal) as AnggaranModal," +
        //                    "SUM(RealisasiPegawai) as RealisasiPegawai,SUM(RealisasiBarangJasa) as RealisasiBarangJasa,SUM(RealisasiPemeliharaan) as RealisasiPemeliharaan,SUM(RealisasiModal) as RealisasiModal ,SUM(JumlahBelanja) as JumlahBelanja,SUM(RealisasiBelanja) as RealisasiBelanja" +
        //                    " from viewPerdaIIB   inner join mUrusan on viewPerdaIIB.IDUrusan= mUrusan.ID  where viewPerdaIIB.btJenis  IN (2,3)  " +
        //                    " group by mUrusan.btKodeKategori,mUrusan.btKodeUrusan,mUrusan.sNamaUrusan  ";
        //        SSQL = SSQL + "  UNION ";
        //        SSQL = SSQL + " Select 4 as Level, '5' as Kelompok, 2 as KelompokBesar ,MpELAKSANAuRUSAN.btKodeKategoriPelaksana AS KodeKategori , " +
        //                    " MpELAKSANAuRUSAN.btKodeUrusanPELAKSANA  as KodeUrusan, Mskpd.id as KodeSKPD, " +
        //                    " 0 as KOdeRekening,  0 as Rek, Mskpd.sNamaskpd as Nama, " +
        //                    " SUM(AnggaranPegawai) as AnggaranPegawai,SUM(AnggaranBarangJasa) as AnggaranBarangJasa,SUM(AnggaranPemeliharaan) as AnggaranPemeliharaan,SUM(AnggaranModal) as AnggaranModal," +
        //                    "SUM(RealisasiPegawai) as RealisasiPegawai,SUM(RealisasiBarangJasa) as RealisasiBarangJasa,SUM(RealisasiPemeliharaan) as RealisasiPemeliharaan,SUM(RealisasiModal) as RealisasiModal,SUM(JumlahBelanja) as JumlahBelanja,SUM(RealisasiBelanja) as RealisasiBelanja " +
        //                    " from viewPerdaIIB  inner join mSKPD  on viewPerdaIIB.iddINAS= mSKPD.ID  " +
        //                    " INNER JOIN MpELAKSANAURUSAN ON MpELAKSANAuRUSAN.iduRUSAN =viewPerdaIIB.iduRUSAN AND mPelaksanaUrusan.IDDInas =viewPerdaIIB.iddiNAS   " +
        //                    "  and viewPerdaIIB.iTAhun = MpELAKSANAURUSAN.iTahun  where viewPerdaIIB.btJenis  IN (2,3) " +
        //                    " group by MpELAKSANAuRUSAN.btKodeKategoriPelaksana , MpELAKSANAuRUSAN.btKodeUrusanPELAKSANA, Mskpd.id,Mskpd.sNamaskpd ";

        //        SSQL = SSQL + "   ORDER BY KelompokBesar ,KodeKategori , KodeUrusan, KodeSKPD, KOdeRekening";




        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {

        //                _lst = (from DataRow dr in dt.Rows
        //                        select new PerdaIIB()
        //                        {

        //                            //Kode { set; get; }
        //                            Level = DataFormat.GetInteger(dr["Level"]),
        //                            //IDKategori { set; get; }
        //                            IDUrusan = DataFormat.GetInteger(dr["kodeurusan"]),
        //                            IDDInas = DataFormat.GetInteger(dr["kodeskpd"]),
        //                            //root { set; get; }
        //                            Kode = DataFormat.GetInteger(dr["Level"]) == 1 ? DataFormat.GetString(dr["Kelompok"]) : DataFormat.GetString(dr["Kelompok"]) + "." + (DataFormat.GetInteger(dr["Level"]) == 2 ? DataFormat.GetInteger(dr["Kodekategori"]).ToString("#") :
        //                                   (DataFormat.GetInteger(dr["Level"]) == 3 ? DataFormat.GetInteger(dr["Kodekategori"]).ToString("#") + "." + DataFormat.GetInteger(dr["KodeUrusan"]).ToString("##") :
        //                                   (DataFormat.GetInteger(dr["Level"]) == 4 ? DataFormat.GetInteger(dr["Kodekategori"]).ToString("#") + "." + DataFormat.GetInteger(dr["KodeUrusan"]).ToString("##") + "." + DataFormat.GetInteger(dr["kodeskpd"]).ToKodeDinas() :
        //                                    DataFormat.GetInteger(dr["Kodekategori"]).ToString("#") + "." + DataFormat.GetInteger(dr["KodeUrusan"]).ToString("##") + "." + DataFormat.GetInteger(dr["kodeskpd"]).ToKodeDinas() + "." + DataFormat.GetLong(dr["KodeRekening"]).ToKodeRekening(m_ProfileRekening)))), //GetKodeII(DataFormat.GetInteger(dr["Level"]),

        //                            Nama = DataFormat.GetString(dr["Nama"]).ToUpper(),

        //                            AnggaranPegawai = DataFormat.GetDecimal(dr["AnggaranPegawai"]).ToRupiahInReport(),
        //                            AnggaranBarangJasa = DataFormat.GetDecimal(dr["AnggaranBarangJasa"]).ToRupiahInReport(),
        //                            AnggaranPemeliharaan = DataFormat.GetDecimal(dr["AnggaranPemeliharaan"]).ToRupiahInReport(),
        //                            AnggaranModal = DataFormat.GetDecimal(dr["AnggaranModal"]).ToRupiahInReport(),
        //                            JumlahAnggaran = DataFormat.GetDecimal(dr["JumlahBelanja"]).ToRupiahInReport(),
        //                            //JumlahBelanja = DataFormat.GetInteger(dr["kodeurusan"]),


        //                            //PendapatanMurni = DataFormat.GetInteger(dr["kodeurusan"]),
        //                            RealisasiPegawai = DataFormat.GetDecimal(dr["RealisasiPegawai"]).ToRupiahInReport(),
        //                            RealisasiBarangJasa = DataFormat.GetDecimal(dr["RealisasiBarangJasa"]).ToRupiahInReport(),
        //                            RealisasiPemeliharaan = DataFormat.GetDecimal(dr["RealisasiPemeliharaan"]).ToRupiahInReport(),
        //                            RealisasiModal = DataFormat.GetDecimal(dr["RealisasiModal"]).ToRupiahInReport(),
        //                            JumlahRealisasi = DataFormat.GetDecimal(dr["RealisasiBelanja"]).ToRupiahInReport(),
        //                            SelisihBelanja = (DataFormat.GetDecimal(dr["JumlahBelanja"]) - DataFormat.GetDecimal(dr["RealisasiBelanja"])).ToRupiahInReport(),
        //                            PersenBelanja = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["JumlahBelanja"]), DataFormat.GetDecimal(dr["RealisasiBelanja"]))
        //                            //                            JumlahBelanjaMurni = (DataFormat.GetDecimal(dr["Anggaran"])).ToRupiahInReport(),
        //                            //                            JumlahBelanja = (DataFormat.GetDecimal(dr["Realisasi"])).ToRupiahInReport(),
        //                            //                            SelisihBelanja = (DataFormat.GetDecimal(dr["Realisasi"]) - DataFormat.GetDecimal(dr["Anggaran"])).ToRupiahInReport(),
        //                            //                            PersenBelanja = DataFormat.GetProsentaseRealisasi(DataFormat.GetDecimal(dr["Realisasi"]), DataFormat.GetDecimal(dr["Anggaran"]))
        //                        }).ToList();
        //            }

        //        }



        //        return _lst;


        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message;
        //        return _lst;
        //    }
        //}
        public List<PerdaIII> GetPerdaIIIRealisasi(ParameterLaporan _p)
        {


            //string namaView = CreateViewRealisasiAllLevel(_p, true);
            string namaView = CreateViewAllLevel(_p, true);

            BersihkanNonKegiatan();
            if (_p.IDDinas == 4010500)
                UpdateRealisasiGabungan();

            if (_p.IDDinas == 4010800)
                UpdateRealisasiGabunganBenuaKayong();


            SKPDLogic oLogic = new SKPDLogic(Tahun);
            List<SKPD> lstSKPD = new List<SKPD>();
            lstSKPD = oLogic.GetByParent(_p.IDDinas);

            string strDinas = "(";
            foreach (SKPD s in lstSKPD)
            {
                strDinas = strDinas + s.ID.ToString() + ",";
            }
            strDinas = strDinas + "99)";

            List<PerdaIII> _lst = new List<PerdaIII>();
            //" + _p.IDDinas.ToSTring + "
            try
            {

                SSQL = "Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + _p.IDDinas.ToString() + " as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok from " + namaView + "  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root < 3 and (" + namaView + ".Jumlah>=0   or " + namaView + ".JumlahMurni>=0) ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas in " + strDinas;
                    SSQL = SSQL + " AND " + namaView + ".bPPKD=" + _p.bPPKD.ToString();

                }



                SSQL = SSQL + " UNION ALL    ";

                if (_p.LastLevel == 3)
                {
                    SSQL = SSQL + "  Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + _p.IDDinas.ToString() + " as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root+2,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView +
                      "  Left outer JOIN mDasarHukum ON Left(mDasarHukum.IIDRekening,3)=left(" + namaView + ".IIDRekening,3)  " +
                      " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root = 3 and (" + namaView + ".Jumlah >= 0  or " + namaView + ".JumlahMurni>=0)  ";//and  " +
                    //namaView + ".IDDInas =" + _p.IDDinas.ToString();
                }
                else
                {
                    SSQL = SSQL + "  Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + _p.IDDinas.ToString() + " as  IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root +2 as Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok from " + namaView +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root >= 3  and " + namaView + ".Root <5 and (" + namaView + ".Jumlah > 0  or " + namaView + ".JumlahMurni>0) ";//and  " +


                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas in " + strDinas;// +_p.IDDinas.ToString();
                        SSQL = SSQL + " AND " + namaView + ".bPPKD=" + _p.bPPKD.ToString();

                    }
                    SSQL = SSQL + "  UNION Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + _p.IDDinas.ToString() + " as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root +2 as Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView +
                   "  Left Outer  JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                   " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root >= 3  and " + namaView + ".Root =5 and (" + namaView + ".Jumlah > 0  or " + namaView + ".JumlahMurni>0) ";//and  " +

                }



                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas in " + strDinas;//+ _p.IDDinas.ToString();
                    SSQL = SSQL + " AND " + namaView + ".bPPKD=" + _p.bPPKD.ToString();

                }

                SSQL = SSQL + " UNION ALL    ";
                SSQL = SSQL + " Select 2 as btJenis," + _p.IDDinas.ToString() + "/10000 as IDUrusan ," + _p.IDDinas.ToString() + " as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  and " + namaView + ".btJenis in (2,3) and " + namaView + ".Root =1 ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas in " + strDinas;// + _p.IDDinas.ToString();
                    SSQL = SSQL + " AND " + namaView + ".bPPKD=" + _p.bPPKD.ToString();

                }
                SSQL = SSQL + " GROUP BY  " + namaView + ".IIDREKENING,ROOT,sNamaRekening, mDasarHukum.iNo, mDasarHukum.sKeterangan  ";
                SSQL = SSQL + " UNION ALL    ";




                SSQL = SSQL + " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + _p.IDDinas.ToString() + " as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis= 2 and " + namaView + ".Root =2  and (" + namaView + ".Jumlah>0 or " + namaView + ".JumlahMurni>0)  and " + namaView + ".Root < 4   ";



                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas in " + strDinas;// + _p.IDDinas.ToString();
                    SSQL = SSQL + " AND " + namaView + ".bPPKD=" + _p.bPPKD.ToString();

                }
                SSQL = SSQL + " UNION ALL ";
                SSQL = SSQL + " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + _p.IDDinas.ToString() + " as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root+2,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis= 2 and " + namaView + ".Root =3  and (" + namaView + ".Jumlah>0 or " + namaView + ".JumlahMurni>0)  and " + namaView + ".Root < 4   ";



                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas in " + strDinas;// + _p.IDDinas.ToString();
                    SSQL = SSQL + " AND " + namaView + ".bPPKD=" + _p.bPPKD.ToString();

                }
                SSQL = SSQL + " UNION ALL ";

                SSQL = SSQL + " Select " + namaView + ".btJenis, " + _p.IDDinas.ToString() + "/10000 as  IDUrusan," + _p.IDDinas.ToString() + " as IDDInas, 1 AS IDProgram , 1 AS IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening , SUM(Jumlah) AS Jumlah, SUM(JumlahMurni) AS JumlahMurni ,mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as IsPokok  from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 3 and Root =2  ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas in " + strDinas;// + _p.IDDinas.ToString();
                    SSQL = SSQL + " AND " + namaView + ".bPPKD=" + _p.bPPKD.ToString();

                }

                SSQL = SSQL + " GROUP BY " + namaView + ".btJenis, " + namaView + ".IIDRekening,Root,sNamaRekening,mDasarHukum.iNo,mDasarHukum.sKeterangan ";

                SSQL = SSQL + "  UNION ALL  ";

                SSQL = SSQL + "  Select 3 as btJenis,A.IDUrusan, " + _p.IDDinas.ToString() + " as  IDDInas,A.IDProgram, 0 as IDKegiatan,0 as IIDRekening,3 as Root, A.sNamaProgram as sNamaRekening,    " +
                    "  SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan, mPelaksanaURusan.IsPokok FROM tPrograms_A A   " +
                    " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan    " +
                    " AND A.IDProgram = B.IDProgram   " +
                    " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
                    " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND   B.btJEnis= 3 and b.Root = 5 AND (b.Jumlah>0 or B.JumlahMurni>0)   ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDinas in " + strDinas;// + _p.IDDinas.ToString();
                    SSQL = SSQL + " AND B.bPPKD = " + _p.bPPKD.ToString();

                }

                SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDProgram, A.sNamaProgram,mPelaksanaURusan.IsPokok" +
                " UNION ALL    " +
                " Select 3.btJenis,A.IDUrusan, " + _p.IDDinas.ToString() + " as  IDDInas,A.IDProgram, A.IDkegiatan  as IDKegiatan,0 as IIDRekening,4 as Root, A.sNama as sNamaRekening,   " +
                " SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan,mPelaksanaUrusan.IsPokok FROM tKegiatan_A A   " +
                " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan   AND A.IDProgram = B.IDProgram and A.IDkegiatan=B.IDKegiatan  " +
            " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
            " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan    " +
            " WHERE B.iTAhun = " + _p.Tahun.ToString() + " AND B.btJEnis= 3 and B.Root=5 AND (B.Jumlah>0 or B.JumlahMurni>0)";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDinas in " + strDinas;//+ _p.IDDinas.ToString();
                    SSQL = SSQL + " AND B.bPPKD = " + _p.bPPKD.ToString();

                    SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok  ";

                }
                else
                {
                    SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok  ";
                }



                if (_p.LastLevel == 3)
                {
                    SSQL = SSQL + " UNION ALL  " +
                        " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + _p.IDDinas.ToString() + " as  IDDInas,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root+2,sNamaRekening, SUM(Jumlah) as JUMLAH ,SUM( JumlahMurni ) as JumlahMurni, 0 as iNo,'' as Keterangan ,mPelaksanaUrusan.IsPokok  from " + namaView + "  " +
                        " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "   AND  " + namaView + ".btJEnis in ( 3) and Root = 3 and (Jumlah  > 0  or JumlahMurni>0)";
                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas in " + strDinas;// + _p.IDDinas.ToString();
                        SSQL = SSQL + " AND " + namaView + ".bPPKD=" + _p.bPPKD.ToString();

                    }
                    SSQL = SSQL + " group by viewAnggaran.btJenis,viewAnggaran.IDUrusan,IDProgram , " +
                             " IDkegiatan, viewAnggaran.IIDRekening,Root,sNamaRekening,mPelaksanaUrusan.IsPokok,viewAnggaran.btJenis";
                }
                else
                {

                    SSQL = SSQL + "  UNION ALL  " +
                    " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + _p.IDDinas.ToString() + ".IDDInas,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root +2 as Root,sNamaRekening, sum(Jumlah) as Jumlah, SUm(JumlahMurni) as JUmlahMurni , 0 as iNo,''  as Keterangan ,mPelaksanaUrusan.IsPokok  from " + namaView + "  " +
                    " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis in (3,2) and Root > 3 and Root < = 5  and (Jumlah  > 0  or JumlahMurni>0)  ";
                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas in " + strDinas;// + _p.IDDinas.ToString();
                        SSQL = SSQL + " AND " + namaView + ".bPPKD=" + _p.bPPKD.ToString();

                    }
                    SSQL = SSQL + " group by viewAnggaran.btJenis,viewAnggaran.IDUrusan,IDProgram , " +
                             " IDkegiatan, viewAnggaran.IIDRekening,Root,sNamaRekening,mPelaksanaUrusan.IsPokok,viewAnggaran.btJenis";
                }

                //SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan," + namaView + ".IDDInas, IDProgram,IDkegiatan, " + namaView + ".IIDRekening,Root,iNo  ";
                SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan,IDProgram,IDkegiatan, IIDRekening,Root,iNo  ";
                DataTable dt = new DataTable();


                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIII()
                                {
                                    Tahun = _p.Tahun,
                                    Kode = DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                            DataFormat.GetInteger(dr["IDDInas"]),
                                                            DataFormat.GetInteger(dr["IDProgram"]),
                                                            DataFormat.GetInteger(dr["IDkegiatan"]),
                                                            DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening),
                                    Nama = DataFormat.UpperFirst(DataFormat.GetString(dr["sNamaRekening"]).Trim()),
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport(),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"].ToString()).ToRupiahInReport(),
                                    Selisih = (DataFormat.GetDecimal(dr["Jumlah"].ToString()) - DataFormat.GetDecimal(dr["JumlahMurni"].ToString())).ToRupiahInReport(),
                                    Prosentase = DataFormat.GetProsentaseRealisasi(DataFormat.GetDecimal(dr["Jumlah"].ToString()), DataFormat.GetDecimal(dr["JumlahMurni"].ToString())),
                                    Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                    Level = DataFormat.GetSingle(dr["Root"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"])
                                    /*
                                    Tahun = _p.Tahun,
                                    Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                                           DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                            DataFormat.GetInteger(dr["IDDInas"]),
                                                            DataFormat.GetInteger(dr["IDProgram"]),
                                                            DataFormat.GetInteger(dr["IDkegiatan"]),
                                                            DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                                    Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).Trim() : DataFormat.GetString(dr["sNamaRekening"]).Trim()) : "",
                                    Jumlah = DataFormat.GetDecimal(dr["iNo"]) < 2 ? DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport() : "",
                                    JumlahMurni = DataFormat.GetInteger(dr["iNo"]) > 0 ? DataFormat.GetInteger(dr["iNo"]).ToString() + "." : "",
                                    Selisih = "0",
                                    Prosentase = "0",
                                    Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                    Level = DataFormat.GetSingle(dr["Root"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"])
                                    */

                                }).ToList();
                        //}                        
                    }
                    string oldKode = "";
                    string cJumlah = "";
                    Single level = 0;

                    for (int i = 0; i < _lst.Count; i++)
                    {
                        PerdaIII curPerdaIII = _lst[i];
                        if (oldKode == curPerdaIII.Kode && level == curPerdaIII.Level && cJumlah == curPerdaIII.Jumlah && curPerdaIII.Jenis == 1)
                        {
                            string tempKode = curPerdaIII.Kode;
                            string tempJumlah = curPerdaIII.Jumlah;
                            Single tempLevel = curPerdaIII.Level;
                            _lst[i].Kode = "";
                            _lst[i].Jumlah = "";
                            _lst[i].JumlahMurni = "";
                            _lst[i].Nama = "";
                            _lst[i].Selisih = "";
                            _lst[i].Prosentase = "";

                            oldKode = tempKode;
                            cJumlah = tempJumlah;
                            level = tempLevel;
                        }
                        else
                        {
                            oldKode = curPerdaIII.Kode;
                            //_lst[i].Jumlah = "";
                            cJumlah = curPerdaIII.Jumlah;
                            level = curPerdaIII.Level;

                        }


                    }
                }

                if (_p.IDDinas > 0)
                {


                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,10 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                        " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas  in " + strDinas + " and Root = 1 and btJenis=1 and bPPKD=" + _p.bPPKD.ToString() + ") as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas  in " + strDinas + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000  and bPPKD=" + _p.bPPKD.ToString() + ") as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas in " + strDinas + " and Root = 1 and btJenis=1  and bPPKD=" + _p.bPPKD.ToString() + ") as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas in " + strDinas + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000  and bPPKD=" + _p.bPPKD.ToString() + " )  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }
                else
                {

                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,10 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                    " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 ) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 ) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }

                DataTable dtx = new DataTable();
                PerdaIII o = new PerdaIII();

                dtx = _dbHelper.ExecuteDataTable(SSQL);
                if (dtx != null)
                {
                    if (dtx.Rows.Count > 0)
                    {

                        DataRow dr = dtx.Rows[0];

                        o = new PerdaIII()
                        {
                            Tahun = _p.Tahun,
                            Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                                   DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                    DataFormat.GetInteger(dr["IDDInas"]),
                                                    DataFormat.GetInteger(dr["IDProgram"]),
                                                    DataFormat.GetInteger(dr["IDkegiatan"]),
                                                    DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                            Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                            Jumlah = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])).ToRupiahInReport(),
                            JumlahMurni = (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])).ToRupiahInReport(),
                            DJumlah = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])),
                            DJumlahMurni = (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])),

                            Selisih = DataFormat.GetSelisih(DataFormat.GetDecimal(dr["PDPT"]), DataFormat.GetDecimal(dr["BLJ"]), DataFormat.GetDecimal(dr["PDPTMURNI"]), DataFormat.GetDecimal(dr["BLJMURNI"])).ToRupiahInReport(),
                            Prosentase = DataFormat.GetProsentaseRealisasi(DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"]), DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])),
                            Keterangan = "",
                            Level = DataFormat.GetSingle(dr["Root"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"])

                        };

                    }
                }
                _lst.Add(o);

                // Pembiayaan 
                //if (_p.IDDinas == 4040601){
                List<PerdaIII> lstpby = new List<PerdaIII>();
                lstpby = GetPerdaIIIRealisasiPembiayaan(_p, o);
                if (lstpby.Count > 1)
                {
                    foreach (PerdaIII pby in lstpby)
                    {
                        _lst.Add(pby);
                    }
                }
                //}
                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        public List<PerdaIII> GetPerdaIIIRealisasiPembiayaan(ParameterLaporan _p, PerdaIII sd)
        {

            string namaView = CreateViewRealisasiAllLevel(_p, true);
            decimal anggaransilpa = 0L;
            decimal realisasisilpa = 0L;

            List<PerdaIII> _lst = new List<PerdaIII>();
            try
            {

                if (_p.IDDinas > 0)
                {
                    SSQL = "Select " + namaView + ".btJenis," + namaView + ".IDDInas/10000 IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, 0 as Jumlah, 0 as JumlahMurni, mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + " AND  " + namaView + ".btJEnis= 4 and " + namaView + ".Root = 1 and " + namaView + ".Jumlah>0   " +
                        "  ";


                    SSQL = SSQL + " UNION ALL Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + " AND  " + namaView + ".btJEnis= 4 and " + namaView + ".Root > 1 and " + namaView + ".Root <= 5   and " + namaView + ".Jumlah>0   " +
                        " UNION ALL    ";

                    SSQL = SSQL + " Select 5 as btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + "  and " + namaView + ".btJenis =5 and " + namaView + ".Root > 1 and " + namaView + ".Root <= 5  and " + namaView + ".Jumlah>0    " +
                        " GROUP BY " + namaView + ".IDUrusan," + namaView + ".IDDInas," + namaView + ".IIDREKENING,ROOT,sNamaRekening, mDasarHukum.iNo, mDasarHukum.sKeterangan  ";


                    SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan," + namaView + ".IDDInas, IDProgram,IDkegiatan, " + namaView + ".IIDRekening,Root,iNo  ";
                }
                else
                {

                    SSQL = "Select " + namaView + ".btJenis," + namaView + ".IDDInas/10000 IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, 0 as Jumlah, 0 as JumlahMurni, mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 4 and " + namaView + ".Root = 1 and " + namaView + ".Jumlah>0   " +
                        "  ";


                    SSQL = SSQL + " UNION ALL Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".btJEnis= 4 and " + namaView + ".Root > 1 and " + namaView + ".Root <= 5   and " + namaView + ".Jumlah>0   " +
                        " UNION ALL    ";

                    SSQL = SSQL + " Select 5 as btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " and " + namaView + ".btJenis =5 and " + namaView + ".Root > 1 and " + namaView + ".Root <= 5  and " + namaView + ".Jumlah>0    " +
                        " GROUP BY " + namaView + ".IDUrusan," + namaView + ".IDDInas," + namaView + ".IIDREKENING,ROOT,sNamaRekening, mDasarHukum.iNo, mDasarHukum.sKeterangan  ";


                    SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan, IDProgram,IDkegiatan, " + namaView + ".IIDRekening,Root,iNo  ";

                }

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaIII()
                                {
                                    Tahun = _p.Tahun,
                                    Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                                           DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                            DataFormat.GetInteger(dr["IDDInas"]),
                                                            DataFormat.GetInteger(dr["IDProgram"]),
                                                            DataFormat.GetInteger(dr["IDkegiatan"]),
                                                            DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                                    Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                                    Jumlah = DataFormat.GetDecimal(dr["iNo"]) < 2 ? DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport() : "",
                                    JumlahMurni = DataFormat.GetInteger(dr["iNo"]) > 0 ? DataFormat.GetInteger(dr["iNo"]).ToString() + "." : DataFormat.GetDecimal(dr["JumlahMurni"].ToString()).ToRupiahInReport(),
                                    Selisih = "0",
                                    Prosentase = "0",
                                    Keterangan = "",//DataFormat.GetString(dr["Keterangan"]),
                                    Level = DataFormat.GetSingle(dr["Root"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"])

                                }).ToList();
                        //}                        
                    }
                }

                anggaransilpa = 0L;
                realisasisilpa = 0L;


                for (int idx = 0; idx < _lst.Count; idx++)
                {
                    if (_lst[idx].Level == 1)
                    {
                        _lst[idx].Jumlah = "";
                        _lst[idx].JumlahMurni = "";
                        _lst[idx].Prosentase = "";
                        _lst[idx].Selisih = "";

                    }
                }

                SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,8 as Root,'PEMBIAYAAN NETTO' as sNamaRekening, " +
                    " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 2 and btJenis=4 and IIDRekening> 4000000) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 2 and btJenis =5 and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 2 and btJenis=4 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 2 and btJenis =5 and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";

                DataTable dtx = new DataTable();
                PerdaIII o = new PerdaIII();

                dtx = _dbHelper.ExecuteDataTable(SSQL);
                if (dtx != null)
                {
                    if (dtx.Rows.Count > 0)
                    {

                        DataRow dr = dtx.Rows[0];

                        o = new PerdaIII()
                        {
                            Tahun = _p.Tahun,
                            Kode = "",
                            Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                            Jumlah = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])).ToRupiahInReport(),
                            JumlahMurni = (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])).ToRupiahInReport(),
                            DJumlah = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])),
                            DJumlahMurni = (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])),
                            Selisih = "0",
                            Prosentase = "0",
                            Keterangan = DataFormat.GetString(dr["Keterangan"]),
                            Level = DataFormat.GetSingle(dr["Root"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"])

                        };

                    }
                }

                _lst.Add(o);


                SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 1 as IDkegiatan, 9999999  as IIDRekening,9 as Root,'SISA LEBIH PEMBIAYAAN ANGGARAN' as sNamaRekening, " +
                    " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 2 and btJenis=4 and iidrekening  like '61%') + (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening like '4%')as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 2 and btJenis =5 and IIDRekening like '62%') + (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening like '5%') as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 2 and btJenis=4 and IIDRekening like '61%')+(SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 and IIDRekening like '4%') as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 2 and btJenis =5 and IIDRekening like '62%') + (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in (2,3) and IIDRekening like '5%')  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";

                DataTable dtx2 = new DataTable();
                PerdaIII o2 = new PerdaIII();
                o2.Tahun = _p.Tahun;

                o2.Kode = "";
                o2.Tahun = _p.Tahun;
                o2.JumlahMurni = (sd.DJumlahMurni + o.DJumlahMurni).ToRupiahInReport();
                o2.Jumlah = (sd.DJumlah + o.DJumlah).ToRupiahInReport();
                o2.Selisih = "0";
                o2.Prosentase = "0";
                o2.Keterangan = "";
                o2.Level = 11;
                o2.Jenis = 0;

                //dtx2 = _dbHelper.ExecuteDataTable(SSQL);
                //if (dtx2 != null)
                //{
                //    if (dtx2.Rows.Count > 0)
                //    {

                //        DataRow dr = dtx2.Rows[0];

                //        o2 = new PerdaIII()
                //        {
                //            Tahun = _p.Tahun,
                //            Kode = "",
                //            Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                //            JumlahMurni = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])).ToRupiahInReport(),
                //            Jumlah = (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])).ToRupiahInReport(),
                //            Selisih = "0",
                //            Prosentase = "0",
                //            Keterangan = DataFormat.GetString(dr["Keterangan"]),
                //            Level = DataFormat.GetSingle(dr["Root"]),
                //            Jenis = DataFormat.GetSingle(dr["btJenis"])

                //        };

                //    }
                //}


                _lst.Add(o2);
                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
    }
}
