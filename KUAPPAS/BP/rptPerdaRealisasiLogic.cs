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
    public class rptPerdaRealisasiLogic:BP
    {
        public rptPerdaRealisasiLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            PrepareFunction();
         //   BersihkanNonKegiatan();
            //CekViewAnggaranAllLevel();
        }
        private void CekViewAnggaranAllLevel()
        {
           
             HapusView("vwAnggaranAllLevel") ;

            _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = " CREATE VIEW vwAnggaranAllLevel AS " +
                "Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, A.IIDRekening as Rek,b.IIDRekening, b.btRoot as Root,b.sNamaRekening ,  A.cPlafon AS JumlahOlah,A.cJumlah AS Jumlah, A.cJumlahMurni AS JumlahMurni ,b.iDebet as Debet  " +
                " FROM tAnggaranRekening_A A inner join mRekening b on a.IIDRekening=b.IIDRekening    where b.btRoot=5  " +
                " UNION ALL  " +
                  " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening, " + m_ProfileRekening.LEN4.ToString() +" ) as Rek,b.IIDRekening,b.btRoot as Root, b.sNamaRekening ,  SUM(A.cPlafon) as JumlahOlah,SUM(A.cJumlah) AS JUMLAH, SUM(A.cJumlahMurni) AS JumlahMurni ,b.iDebet as Debet  " +
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
        private void PrepareFunction(){
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
            _dbHelper.ExecuteNonQuery (SSQL);

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
        
        private string CreateViewPerdaII(ParameterLaporan _p, bool create){

            string namaView = "viewPerdaII" +_p.NamaUser.Trim();

            HapusView(namaView);
            
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

        public List<PerdaIIDB> GetPerda (int tahap)
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
        private string  CreateViewPerdaII_B(ParameterLaporan _p, bool create)

        {
            string namaView = "viewPerdaII" +_p.NamaUser.Trim();            
            HapusView(namaView);

           
            if (create == true)
            {
                //GetKolom(_p.Tahap);

                SSQL = "CREATE VIEW " + namaView + " AS Select tAnggaranRekening_A.iTAhun, tAnggaranRekening_A.IDDInas,tAnggaranRekening_A.IDUrusan as IDurusan, " +
                    " 0 as IDProgram, 0 as IDKegiatan, tAnggaranRekening_A.IIDRekening, tAnggaranRekening_A.btJenis,tAnggaranRekening_A.cJumlahRKAP  AS Anggaran,   " +
                    "  tAnggaranRekening_A.cRealisasi AS Realisasi  from tAnggaranRekening_A INNER JOIN mSKPD ON tAnggaranRekening_A.IDDInas = mSKPD.ID inner JOIn tKegiatan_A On tAnggaranRekening_A.IDKegiatan= tKegiatan_A.IDkegiatan and tAnggaranRekening_A.IDDInas= tKegiatan_A.IDDinas " +
                    " AND tAnggaranRekening_A.iTahun = tKegiatan_A.iTahun AND tAnggaranRekening_A.btJenis = tKegiatan_A.btJenis where tAnggaranRekening_A.btJenis  in (1,2,4,5)    " +
                    " and mskpd.root = 1 and tAnggaranRekening_A.iTahun = " + _p.Tahun.ToString() +

                " UNION ALL Select tAnggaranRekening_A.iTAhun, tAnggaranRekening_A.IDDInas,tAnggaranRekening_A.IDUrusan as IDurusan, tAnggaranRekening_A.IDProgram, tAnggaranRekening_A.IDKegiatan, tAnggaranRekening_A.IIDRekening, " +
                        "tAnggaranRekening_A.btJenis," +
                        "tAnggaranRekening_A.cJumlahRKAP  AS Anggaran, " +
                         " tAnggaranRekening_A.cRealisasi AS Realisasi " +
                        " from tAnggaranRekening_A INNER JOIN mSKPD ON tAnggaranRekening_A.IDDInas = mSKPD.ID INNER JOIN tkegiatan_A ON " +
                        " tAnggaranRekening_A.iTahun = tKegiatan_A.iTahun and tAnggaranRekening_A.IDUrusan = tKegiatan_A.IDUrusan and tAnggaranRekening_A.IDDInas = tKegiatan_A.IDDInas and  tAnggaranRekening_A.IDKegiatan = tKegiatan_A.IDKegiatan and  tAnggaranRekening_A.btJenis = tKegiatan_A.btJenis where mskpd.root = 1  and tAnggaranRekening_A.btJenis = 3  and tAnggaranRekening_A.iTahun = " + _p.Tahun.ToString();


                _dbHelper.ExecuteNonQuery(SSQL);

            }
            return namaView;
        }

        private string CreateViewPerdaII_BB(ParameterLaporan _p, bool create)
        {
            string namaView = "viewPerdaIIB" + _p.NamaUser.Trim();
            HapusView(namaView);


            if (create == true)
            {
                //GetKolom(_p.Tahap);

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

               
                _dbHelper.ExecuteNonQuery(SSQL);

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
                string namaview=CreateViewPerdaII(_p,true);

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
                            Kode= DataFormat.GetInteger(dr["IDDinas"])==0? DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan():DataFormat.GetInteger(dr["IDDinas"]).ToKodeDinas(),
                            Nama = DataFormat.GetString(dr["Nama"]).ToUpper(),
                            Pendapatan= DataFormat.GetDecimal (dr["SumPendapatanOlah"]).ToRupiahInReport(),
                            BelanjaTidakLangsung = DataFormat.GetDecimal(dr["SumBTLOlah"]).ToRupiahInReport(),
                            BelanjaLangsung = DataFormat.GetDecimal(dr["SumBLOlah"]).ToRupiahInReport(),
                            JumlahBelanja= (DataFormat.GetDecimal (dr["SumBTLOlah"])+DataFormat.GetDecimal (dr["SumBLOlah"])).ToRupiahInReport(),
                            PendapatanMurni = DataFormat.GetDecimal(dr["SumPendapatan"]).ToRupiahInReport(),
                            BelanjaTidakLangsungMurni = DataFormat.GetDecimal(dr["SumBTL"]).ToRupiahInReport(),
                            BelanjaLangsungMurni = DataFormat.GetDecimal(dr["SumBL"]).ToRupiahInReport(),
                            JumlahBelanjaMurni = (DataFormat.GetDecimal(dr["SumBTL"]) + DataFormat.GetDecimal(dr["SumBL"])).ToRupiahInReport()

                      }).ToList();
                        
                    }
                }
                CreateViewPerdaII(_p,false);
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
                 
              string namaView = CreateViewPerdaII_B(_p, true);

                // PENDAPATAN 

                 //GetKodeII

                 SSQL = " select  1 as KelompokBesar, 0 AS kelompok, 0 as Level, 0  as Urusan , 0  as Urutan,0 AS IDUrusan, 0 As IDDinas, mRekening.IIDRekening as IDRekening, B.IIDRekening/10000000 as Rek,mRekening.sNamaRekening as Nama,  " +
                     " SUM (B.Realisasi)  as SUMBTL,  SUM (Anggaran)  as SUMBTLMUrni,  " +
                     " 0 as SUMBL , 0 as SUMBLMUrni " +
                     " from mSKPD A INNER JOIN " + namaView + " B ON A.ID= B.IDDinas  INNER JOIN mRekening ON mRekening.IIDRekening/1000000=B.IIDRekening/1000000   " +
                     " WHERE B.IDUrusan< 499  AND b.btJenis =1 AND a.Root = 0  AND B.iTahun = " + _p.Tahun.ToString() + " AND mRekening.btroot = 1 " +
                     " group by mRekening.IIDRekening, mRekening.sNamaRekening ,B.IIDRekening/1000000 ";
                SSQL = SSQL + " UNION select 1 as KelompokBesar,1 AS kelompok,0 as Level, A. btKodekategori * 100  as Urusan,  0 as Urutan, A.btKodeKategori AS IDUrusan, 0 as IDDInas,0 as IDRekening,0 as Rek,  'URUSAN ' + A.sNamaKategori as Nama ,  " +
                      " SUM (B.Realisasi)  as SUMBTL,  SUM (Anggaran)  as SUMBTLMUrni,  " +
                      " 0 as SUMBL , 0 as SUMBLMUrni from mKategori A INNER JOIN " + namaView + " B ON A.btKodeKategori= B.IDUrusan/100   " +
                      " WHere  B.iTahun =" + _p.Tahun.ToString() + " AND b.btJenis = 1 GROUP BY A.btKodeKategori , A.sNamaKategori ";
                 
                SSQL = SSQL + " UNION ALL select  1 as KelompokBesar, 2 AS kelompok, 1 as Level, B.IDUrusan as Urusan, 1 as Urutan ,A.ID AS IDUrusan, 0 as IDDInas,0 as IDRekening,0 as Rek,  A.sNamaUrusan as Nama , " +
                    " SUM (B.Realisasi)  as SUMBTL,  SUM (Anggaran)  as SUMBTLMUrni,  " +
                    " 0 as SUMBL , 0 as SUMBLMUrni  " +
                    " from mUrusan A INNER JOIN " + namaView + " B ON A.ID= B.IDUrusan  " +
                    " WHERE A.ID < 499  AND b.btJenis = 1 and B.iTahun = " + _p.Tahun.ToString() +
                    " GROUP BY  A.ID , A.sNamaUrusan,B.IDUrusan   " +
                    " UNION ALL  " +
                    " select  1 as KelompokBesar,3 AS kelompok,  2 as Level, B.IDURusan  as Urusan , A.ID  as Urutan,B.IDURusan AS IDUrusan, A.ID As IDDinas,0 as IDRekening,0 as Rek,  A.sNamaSKPD as Nama,  " +
                    " SUM (B.Realisasi)  as SUMBTL,  SUM (Anggaran)  as SUMBTLMUrni,  " +
                    " 0 as SUMBL , 0 as SUMBLMUrni  " +
                    " from mSKPD A INNER JOIN " + namaView + " B ON A.ID= B.IDDinas " +
                    " WHERE B.IDUrusan< 499  AND a.Root = 1  AND B.iTahun = " + _p.Tahun.ToString() +
                    " group by A.ID,A.ID, B.IDUrusan, A.sNamaSKPD";


/*
                 SSQL = SSQL + " UNION ALL  " +
                     " select  2 as KelompokBesar, 0 AS kelompok, 0 as Level, 0  as Urusan , 0  as Urutan,0 AS IDUrusan, 0 As IDDinas, mRekening.IIDRekening as IDRekening, B.IIDRekening/10000000 as Rek,mRekening.sNamaRekening as Nama,  " +
                     " SUM (B.Realisasi)  as SUMBTL,  SUM (Anggaran)  as SUMBTLMUrni,  " +
                     " 0 as SUMBL , 0 as SUMBLMUrni " +
                     " from mSKPD A INNER JOIN " + namaView + " B ON A.ID/100= B.IDDinas/100  INNER JOIN mRekening ON mRekening.IIDRekening/10000000=B.IIDRekening/10000000   " +
                     " WHERE B.IDUrusan< 499  AND b.btJenis in (2,3) AND a.Root = 0  AND B.iTahun = " + _p.Tahun.ToString() + " AND mRekening.btroot = 1 " +
                     " group by mRekening.IIDRekening, mRekening.sNamaRekening ,B.IIDRekening/10000000 ";
                SSQL = SSQL + " UNION select  2 as KelompokBesar, 1 AS kelompok,0 as Level, A. btKodekategori * 100  as Urusan,  0 as Urutan, A.btKodeKategori AS IDUrusan, 0 as IDDInas, 0 as IDRekening,0 as Rek, 'URUSAN ' + A.sNamaKategori as Nama ,  " +
                        " SUM (B.Realisasi)  as SUMBTL,  SUM (Anggaran)  as SUMBTLMUrni,  " +
                        "  0 as SUMBL , 0 as SUMBLMUrni from mKategori A INNER JOIN " + namaView + " B ON A.btKodeKategori= B.IDUrusan/100   " +
                        " WHere  B.iTahun =" + _p.Tahun.ToString() + "  AND b.btJenis in (2,3)  GROUP BY A.btKodeKategori , A.sNamaKategori ";
                 SSQL = SSQL + " UNION ALL select  2 as KelompokBesar, 2 AS kelompok,1 as Level, B.IDUrusan as Urusan, 1 as Urutan ,A.ID AS IDUrusan, 0 as IDDInas, 0 as IDRekening,0 as Rek, A.sNamaUrusan as Nama , " +
                    " SUM (B.Realisasi)  as SUMBTL,  SUM (Anggaran)  as SUMBTLMUrni,  " +
                        " 0 as SUMBL , 0 as SUMBLMUrni " +
                    " from mUrusan A INNER JOIN " + namaView + " B ON A.ID= B.IDUrusan  " +
                    " WHERE A.ID < 499  AND b.btJenis in (2,3) and B.iTahun = " + _p.Tahun.ToString() +
                    " GROUP BY  A.ID , A.sNamaUrusan,B.IDUrusan   " +
                    " UNION ALL  " +
                    " select  2 as KelompokBesar, 3 AS kelompok,  2 as Level, B.IDURusan  as Urusan , A.ID  as Urutan,B.IDURusan AS IDUrusan, A.ID As IDDinas, 0 as IDRekening,0 as Rek, A.sNamaSKPD as Nama,  " +
                    " SUM (B.Realisasi)  as SUMBTL,  SUM (Anggaran)  as SUMBTLMUrni,  " +
                        " 0 as SUMBL , 0 as SUMBLMUrni " +
                    " from mSKPD A INNER JOIN " + namaView + " B ON A.ID/100= B.IDDinas/100  " +
                    " WHERE  b.btJenis in (2,3) AND  B.IDUrusan< 499 AND a.Root = 0  AND B.iTahun = " + _p.Tahun.ToString() +
                    " group by A.ID/100,A.ID, B.IDUrusan, A.sNamaSKPD";


                 SSQL = SSQL + " UNION ALL  " +
                     " select  2 as KelompokBesar, 4 AS kelompok, 3 as Level, B.IDURusan  as Urusan , A.ID  as Urutan,B.IDURusan AS IDUrusan, A.ID As IDDinas, mRekening.IIDRekening as IDRekening, B.IIDRekening/1000000 as Rek,mRekening.sNamaRekening as Nama,  " +
                     " SUM (B.Realisasi)  as SUMBTL,  SUM (Anggaran)  as SUMBTLMUrni,  " +
                     " 0 as SUMBL , 0 as SUMBLMUrni " +
                     " from mSKPD A INNER JOIN " + namaView + " B ON A.ID/100= B.IDDinas/100  INNER JOIN mRekening ON mRekening.IIDRekening/1000000=B.IIDRekening/1000000   " +
                     " WHERE B.IDUrusan< 499  AND b.btJenis in (2,3) AND a.Root = 0  AND B.iTahun = " + _p.Tahun.ToString() + " AND mRekening.btroot = 2 " +
                     " group by A.ID/100,A.ID, B.IDUrusan, mRekening.IIDRekening, mRekening.sNamaRekening ,B.IIDRekening/1000000 ";

                SSQL=SSQL+ " UNION ALL  " +
                    " select  2 as KelompokBesar, 5 AS kelompok, 4 as Level,  B.IDURusan  as Urusan , A.ID  as Urutan,B.IDURusan AS IDUrusan, A.ID As IDDinas, mRekening.IIDRekening as IDRekening, B.IIDRekening/100000 as Rek,mRekening.sNamaRekening as Nama,  " +
                    " SUM (B.Realisasi)  as SUMBTL,  SUM (Anggaran)  as SUMBTLMUrni,  " +
                    " 0 as SUMBL , 0 as SUMBLMUrni " +
                    " from mSKPD A INNER JOIN " + namaView + " B ON A.ID/100= B.IDDinas/100  INNER JOIN mRekening ON mRekening.IIDRekening/100000=B.IIDRekening/100000   " +
                    " WHERE B.IDUrusan< 499  AND b.btJenis in (2,3) AND a.Root = 0  AND B.iTahun = " + _p.Tahun.ToString() + " AND mRekening.btroot = 3 " +
                    " group by A.ID/100,A.ID, B.IDUrusan, mRekening.IIDRekening, mRekening.sNamaRekening ,B.IIDRekening/100000 ";
        SSQL = SSQL + " UNION ALL  " +
              " select  2 as KelompokBesar,  4 AS kelompok, 6 as Level, 888 as Urusan, 1 as Urutan,0 AS IDUrusan, 0 As IDDinas, 99999999 as IIDRekening, 0 as Rek, 'JUMLAH BELANJA ' as Nama,  " +
              " SUM (B.Realisasi)  as SUMBTL,  SUM (Anggaran)  as SUMBTLMUrni,  " +
                        "0 as SUMBL , 0 as SUMBLMUrni " +
              " from " + namaView + " B " +
              " inner join mUrusan ON  mUrusan.ID= B.IDUrusan  inner join mSKPD ON mSKPD.ID= B.IDDInas WHERE   b.btJenis in (2,3) and B.iTahun = " + _p.Tahun.ToString();






        SSQL = SSQL + " UNION ALL  " +
               " select 3 as KelompokBesar, 6 AS kelompok, 6 as Level, 999 as Urusan, 1 as Urutan ,0 AS IDUrusan, 0 As IDDinas, 0 as IDRekening, 0 as Rek,'SURPLUS/DEFISIT ' as Nama,  " +
                " (SELECT SUM (Realisasi)  from " + namaView + "  WHERE iTahun = " + _p.Tahun.ToString() + " and btJenis =1 )  - " +
                "  (SELECT SUM (Realisasi)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + " and btJenis in (2,3) )  as SUMBTL , " +
                " (SELECT SUM (Anggaran)  from " + namaView + "  WHERE iTahun = " + _p.Tahun.ToString() + " and btJenis =1 )  - " +
                "  (SELECT SUM (Anggaran)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + " and btJenis in (2,3) )  as SUMBTL , " +
                 "0 as SUMBL , 0 as SUMBLMUrni ";// +
                
                
         //       SSQL=SSQL +  " Order by KelompokBesar, Urusan, Urutan,IDRekening ";

           //                   " 0 as SUMBL , 0 as SUMBLMUrni ;



        //         SSQL = "";
        SSQL = SSQL + " UNION ALL select  4 as KelompokBesar, 0 AS kelompok,0 as Level,  0  as Urusan , 0  as Urutan,0 AS IDUrusan, 0 As IDDinas, mRekening.IIDRekening as IDRekening, B.IIDRekening/1000000 as Rek,mRekening.sNamaRekening as Nama,  " +
            " SUM (B.Realisasi)  as SUMBTL,  SUM (Anggaran)  as SUMBTLMUrni,  " +
            " 0 as SUMBL , 0 as SUMBLMUrni " +
            " from mSKPD A INNER JOIN " + namaView + " B ON A.ID/100= B.IDDinas/100  INNER JOIN mRekening ON mRekening.IIDRekening/1000000=B.IIDRekening/1000000   " +
            " WHERE B.IDUrusan< 499  AND b.btJenis =4 AND a.Root = 0  AND B.iTahun = " + _p.Tahun.ToString() + " AND mRekening.btroot = 2 " +
           " group by mRekening.IIDRekening, mRekening.sNamaRekening ,B.IIDRekening/1000000 ";


        SSQL = SSQL + " UNION select 4 as KelompokBesar,1 AS kelompok,0 as Level, A. btKodekategori * 100  as Urusan,  0 as Urutan, A.btKodeKategori AS IDUrusan, 0 as IDDInas,0 as IDRekening,0 as Rek,  'URUSAN ' + A.sNamaKategori as Nama ,  " +
               " SUM (B.Realisasi)  as SUMBTL,  SUM (Anggaran)  as SUMBTLMUrni,  " +
               " 0 as SUMBL , 0 as SUMBLMUrni from mKategori A INNER JOIN " + namaView + " B ON A.btKodeKategori= B.IDUrusan/100   " +
               " WHere  B.iTahun =" + _p.Tahun.ToString() + " AND b.btJenis = 4 GROUP BY A.btKodeKategori , A.sNamaKategori ";
        SSQL = SSQL + " UNION ALL select  4 as KelompokBesar, 2 AS kelompok,1 as Level, B.IDUrusan as Urusan, 1 as Urutan ,A.ID AS IDUrusan, 0 as IDDInas,0 as IDRekening,0 as Rek,  A.sNamaUrusan as Nama , " +
           " SUM (B.Realisasi)  as SUMBTL,  SUM (Anggaran)  as SUMBTLMUrni,  " +
                   " 0 as SUMBL , 0 as SUMBLMUrni  " +
           " from mUrusan A INNER JOIN " + namaView + " B ON A.ID= B.IDUrusan  " +
           " WHERE A.ID < 499  AND b.btJenis = 4 and B.iTahun = " + _p.Tahun.ToString() +
           " GROUP BY  A.ID , A.sNamaUrusan,B.IDUrusan   " +
           " UNION ALL  " +
           " select  4 as KelompokBesar,3 AS kelompok, 2 as Level, B.IDURusan  as Urusan , A.ID  as Urutan,B.IDURusan AS IDUrusan, A.ID As IDDinas,0 as IDRekening,0 as Rek,  A.sNamaSKPD as Nama,  " +
           " SUM (B.Realisasi)  as SUMBTL,  SUM (Anggaran)  as SUMBTLMUrni,  " +
                   " 0 as SUMBL , 0 as SUMBLMUrni  " +
           " from mSKPD A INNER JOIN " + namaView + " B ON A.ID/100= B.IDDinas/100  " +
           " WHERE B.IDUrusan< 499  AND b.btJenis = 4 AND a.Root = 0  AND B.iTahun = " + _p.Tahun.ToString() +
           " group by A.ID/100,A.ID, B.IDUrusan, A.sNamaSKPD";


        SSQL = SSQL + " UNION ALL select  5 as KelompokBesar, 0 AS kelompok, 0 as Level, 0  as Urusan , 0  as Urutan,0 AS IDUrusan, 0 As IDDinas, mRekening.IIDRekening as IDRekening, B.IIDRekening/1000000 as Rek,mRekening.sNamaRekening as Nama,  " +
               " SUM (B.Realisasi)  as SUMBTL,  SUM (Anggaran)  as SUMBTLMUrni,  " +
               " 0 as SUMBL , 0 as SUMBLMUrni " +
               " from mSKPD A INNER JOIN " + namaView + " B ON A.ID/100= B.IDDinas/100  INNER JOIN mRekening ON mRekening.IIDRekening/1000000=B.IIDRekening/1000000   " +
               " WHERE B.IDUrusan< 499  AND b.btJenis =5 AND a.Root = 0  AND B.iTahun = " + _p.Tahun.ToString() + " AND mRekening.btroot = 2 " +
              " group by mRekening.IIDRekening, mRekening.sNamaRekening ,B.IIDRekening/1000000 ";



        SSQL = SSQL + " UNION select  5 as KelompokBesar, 1 AS kelompok,0 as Level, A. btKodekategori * 100  as Urusan,  0 as Urutan, A.btKodeKategori AS IDUrusan, 0 as IDDInas, 0 as IDRekening,0 as Rek, 'URUSAN ' + A.sNamaKategori as Nama ,  " +
                " SUM (B.Realisasi)  as SUMBTL,  SUM (Anggaran)  as SUMBTLMUrni,  " +
                "  0 as SUMBL , 0 as SUMBLMUrni from mKategori A INNER JOIN " + namaView + " B ON A.btKodeKategori= B.IDUrusan/100   " +
                " WHere  B.iTahun =" + _p.Tahun.ToString() + "  AND b.btJenis  =5   GROUP BY A.btKodeKategori , A.sNamaKategori ";
        SSQL = SSQL + " UNION ALL select  5 as KelompokBesar, 2 AS kelompok,1 as Level , B.IDUrusan as Urusan, 1 as Urutan ,A.ID AS IDUrusan, 0 as IDDInas, 0 as IDRekening,0 as Rek, A.sNamaUrusan as Nama , " +
           " SUM (B.Realisasi)  as SUMBTL,  SUM (Anggaran)  as SUMBTLMUrni,  " +
               " 0 as SUMBL , 0 as SUMBLMUrni " +
           " from mUrusan A INNER JOIN " + namaView + " B ON A.ID= B.IDUrusan  " +
           " WHERE A.ID < 499  AND b.btJenis  =5  and B.iTahun = " + _p.Tahun.ToString() +
           " GROUP BY  A.ID , A.sNamaUrusan,B.IDUrusan   " +
           " UNION ALL  " +
           " select  5 as KelompokBesar, 3 AS kelompok, 2 as Level,  B.IDURusan  as Urusan , A.ID  as Urutan,B.IDURusan AS IDUrusan, A.ID As IDDinas, 0 as IDRekening,0 as Rek, A.sNamaSKPD as Nama,  " +
           " SUM (B.Realisasi)  as SUMBTL,  SUM (Anggaran)  as SUMBTLMUrni,  " +
               " 0 as SUMBL , 0 as SUMBLMUrni " +
           " from mSKPD A INNER JOIN " + namaView + " B ON A.ID/100= B.IDDinas/100  " +
           " WHERE  b.btJenis  =5  AND  B.IDUrusan< 499 AND a.Root = 0  AND B.iTahun = " + _p.Tahun.ToString() +
           " group by A.ID/100,A.ID, B.IDUrusan, A.sNamaSKPD";


        

        SSQL = SSQL + " UNION ALL  " +
               " select 6 as KelompokBesar, 6 AS kelompok,6 as Level, 999 as Urusan, 1 as Urutan ,0 AS IDUrusan, 0 As IDDinas, 0 as IDRekening, 0 as Rek,'PEMBIAYAAN NETTO' as Nama,  " +
                " (SELECT SUM (Realisasi)  from " + namaView + "  WHERE iTahun = " + _p.Tahun.ToString() + " and btJenis =4 )  - " +
                "  (SELECT SUM (Realisasi)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + " and btJenis  =5  )  as SUMBTL , " +
                " (SELECT SUM (Anggaran)  from " + namaView + "  WHERE iTahun = " + _p.Tahun.ToString() + " and btJenis =4 )  - " +
                "  (SELECT SUM (Anggaran)  from " + namaView + "  WHERE iTahun =  " + _p.Tahun.ToString() + " and btJenis  =5  )  as SUMBTL , " +
                 "0 as SUMBL , 0 as SUMBLMUrni " +
                */
             SSQL=SSQL +   "   Order by KelompokBesar, Urusan, Urutan,IDRekening ";
                



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
                                Kode = GetKodeII(DataFormat.GetInteger(dr["Level"]),
                                            DataFormat.GetInteger(dr["IDUrusan"]) , 
                                            DataFormat.GetInteger(dr["IDDinas"]),DataFormat.GetInteger(dr["IDRekening"]) ),
                                
                                Nama = DataFormat.GetString(dr["Nama"]).ToUpper(),
                                //Pendapatan = DataFormat.GetDecimal(dr["SumPendapatan"]).ToRupiahInReport(),
                                //BelanjaTidakLangsung = DataFormat.GetDecimal(dr["SumBTL"]).ToRupiahInReport(),
                                //BelanjaLangsung = DataFormat.GetDecimal(dr["SumBL"]).ToRupiahInReport(),
                                JumlahBelanjaMurni = (DataFormat.GetDecimal(dr["SumBTL"]) + DataFormat.GetDecimal(dr["SumBL"])).ToRupiahInReport(),
                                //PendapatanMurni = DataFormat.GetDecimal(dr["SumPendapatanMurni"]).ToRupiahInReport(),
                                //BelanjaTidakLangsungMurni = DataFormat.GetDecimal(dr["SumBTLMurni"]).ToRupiahInReport(),
                          //      BelanjaLangsungMurni = DataFormat.GetDecimal(dr["SumBLMurni"]).ToRupiahInReport(),
                                JumlahBelanja = (DataFormat.GetDecimal(dr["SumBTLMurni"]) + DataFormat.GetDecimal(dr["SumBLMurni"])).ToRupiahInReport(),
                                SelisihBelanja = (DataFormat.GetDecimal(dr["SumBTL"]) + DataFormat.GetDecimal(dr["SumBL"]) -  DataFormat.GetDecimal(dr["SumBTLMurni"]) + DataFormat.GetDecimal(dr["SumBLMurni"]) ).ToRupiahInReport(),
                                PersenBelanja = DataFormat.GetProsentase (DataFormat.GetDecimal(dr["SumBTL"]) + DataFormat.GetDecimal(dr["SumBL"]),DataFormat.GetDecimal(dr["SumBTLMurni"]) + DataFormat.GetDecimal(dr["SumBLMurni"]))
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
        public List<PerdaII> GetPerdaIIRealisasi(ParameterLaporan _p)
        {
            List<PerdaII> _lst = new List<PerdaII>();
            try
            {
                // Cek View 
                // CreateViewPerdaII(_p.Tahap);

                // var query = from q in lstDB  group by ()
            
                string namaView = CreateViewPerdaII_B(_p, true);

                // PENDAPATAN 

                //GetKodeII
                SSQL = "";
                                
                SSQL = "Select 1 as Level,'4' as Kelompok,1 as KelompokBesar,0 as KodeKategori,0 as KodeUrusan,0 as KodeSKPD, 0 as KOdeRekening, 0 as Rek, 'Pendapatan' as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                        " from viewPerdaII  where btJenis = 1 ";
                SSQL = SSQL + " UNION Select 2 as Level,'4' as Kelompok, 1 as KelompokBesar,mKategori.btKodeKategori  as KodeKategori,0 as KodeUrusan,0 as KodeSKPD, 0 as KOdeRekening,  " +
                            " 0 as Rek,mKategori.sNamaKategori as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " + 
                            " from viewPerdaII  inner join mKategori on viewPerdaII.IDUrusan/100= mKategori.btKodeKategori where viewPerdaII.btJenis = 1 " +
                            " group by mKategori.btKodeKategori,mKategori.sNamaKategori ";
                SSQL=SSQL + " UNION  " ;

                SSQL = SSQL + "  Select 3 as Level, '4' as Kelompok, 1 as KelompokBesar ,mUrusan.btKodeKategori  as KodeKategori , mUrusan.btKodeUrusan as KodeUrusan, 0 as KodeSKPD, 0 as KOdeRekening,   " +
                            " 0 as Rek, mUrusan.sNamaUrusan as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi from viewPerdaII  inner join mUrusan on viewPerdaII.IDUrusan= mUrusan.ID  where viewPerdaII.btJenis = 1 " +
                            " group by mUrusan.btKodeKategori,mUrusan.btKodeUrusan,mUrusan.sNamaUrusan ";
                SSQL=SSQL + " UNION  " ;
                SSQL = SSQL + "  Select 4 as Level, '4' as Kelompok, 1 as KelompokBesar ,mSKPD.btKodeKategori  as KodeKategori , mSKPD.btKodeUrusan as KodeUrusan, mSKPD.ID   as KodeSKPD, 0 as KOdeRekening,   " +
                            " 0 as Rek, mSKPD.sNamaSKPD  as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                            " from viewPerdaII  inner join mSKPD on viewPerdaII.IDDInas= mSKPD.ID  where viewPerdaII.btJenis = 1 " +
                            " group by mSKPD.btKodeKategori , mSKPD.btKodeUrusan , mSKPD.ID , mskpd.sNamaSKPD ";
                
                SSQL=SSQL + " UNION  " ;

                SSQL = SSQL + " Select 1 as Level,'5' as Kelompok,  2 as KelompokBesar,0 as KodeKategori,0 as KodeUrusan,0 as KodeSKPD, 0 as KOdeRekening,0 as Rek,  'BelANJA' as Nama, " +
                            " SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                            " from viewPerdaII  where btJenis in  (2,3) ";
                SSQL=SSQL + " UNION  " ;

                SSQL = SSQL + " Select 2 as Level, '5' as Kelompok, 2 as KelompokBesar,mKategori.btKodeKategori  as KodeKategori,0 as KodeUrusan,0 as KodeSKPD, 0 as KOdeRekening, 0 as Rek, mKategori.sNamaKategori as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                            " from viewPerdaII  inner join mKategori on viewPerdaII.IDUrusan/100= mKategori.btKodeKategori where viewPerdaII.btJenis IN (2,3) " +
                            " group by mKategori.btKodeKategori,mKategori.sNamaKategori ";

                SSQL=SSQL + "  UNION ";
                SSQL = SSQL + " Select 3 as Level, '5' as Kelompok, 2 as KelompokBesar ,mUrusan.btKodeKategori as KodeKategori, mUrusan.btKodeUrusan as KodeUrusan, 0 as KodeSKPD, 0 as KOdeRekening, 0 as Rek,  mUrusan.sNamaUrusan as Nama, SUM(ANggaran ) as Anggaran, SUM (Realisasi) as Realisasi " +
                            " from viewPerdaII  inner join mUrusan on viewPerdaII.IDUrusan= mUrusan.ID  where viewPerdaII.btJenis  IN (2,3)  " +
                            " group by mUrusan.btKodeKategori,mUrusan.btKodeUrusan,mUrusan.sNamaUrusan  " ;
                SSQL=SSQL + "  UNION ";
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
                                    Kode = DataFormat.GetInteger(dr["Level"])==1? DataFormat.GetString (dr["Kelompok"]) : DataFormat.GetString (dr["Kelompok"])  + "." +   (DataFormat.GetInteger(dr["Level"])==2? DataFormat.GetInteger(dr["Kodekategori"]).ToString ("#"): 
                                           (DataFormat.GetInteger(dr["Level"])==3? DataFormat.GetInteger(dr["Kodekategori"]).ToString ("#") +"." + DataFormat.GetInteger(dr["KodeUrusan"]).ToString ("##") :
                                           (DataFormat.GetInteger(dr["Level"])==4?  DataFormat.GetInteger(dr["Kodekategori"]).ToString ("#") +"." + DataFormat.GetInteger(dr["KodeUrusan"]).ToString ("##") + "." +DataFormat.GetInteger(dr["kodeskpd"]).ToKodeDinas (): 
                                            DataFormat.GetInteger(dr["Kodekategori"]).ToString ("#") +"." + DataFormat.GetInteger(dr["KodeUrusan"]).ToString ("##") + "." +DataFormat.GetInteger(dr["kodeskpd"]).ToKodeDinas () + "."  + DataFormat.GetLong(dr["KodeRekening"]).ToKodeRekening(m_ProfileRekening )))), //GetKodeII(DataFormat.GetInteger(dr["Level"]),
                                                //DataFormat.GetInteger(dr["IDUrusan"]),
                                                //DataFormat.GetInteger(dr["IDDinas"]), DataFormat.GetInteger(dr["IDRekening"])),

                                    Nama = DataFormat.GetString(dr["Nama"]).ToUpper(),
                                    JumlahBelanjaMurni = ( DataFormat.GetDecimal(dr["Anggaran"])).ToRupiahInReport(),
                                    JumlahBelanja =(DataFormat.GetDecimal(dr["Realisasi"])).ToRupiahInReport(),
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
        public List<PerdaIIB> GetPerdaIIRealisasiB(ParameterLaporan _p)
        {
            List<PerdaIIB> _lst = new List<PerdaIIB>();
            try
            {
                // Cek View 
                // CreateviewPerdaIIB (_p.Tahap);

                // var query = from q in lstDB  group by ()

                string namaView = CreateViewPerdaII_BB(_p, true);

                // PENDAPATAN 

                //GetKodeII
                SSQL = "";
                /*
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
                */
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
        /*        SSQL = SSQL + " UNION Select 3 as Level,'6' as Kelompok, 4 as KelompokBesar,1  as KodeKategori,0 as KodeUrusan,0 as KodeSKPD, 0 as KOdeRekening,  " +
                            " 0 as Rek,mKategori.sNamaKategori as Nama, " +
                            " SUM(AnggaranPegawai) as AnggaranPegawai,SUM(AnggaranBarangJasa) as AnggaranBarangJasa,SUM(AnggaranPemeliharaan) as AnggaranPemeliharaan,SUM(AnggaranModal) as AnggaranModal," +
                            "SUM(RealisasiPegawai) as RealisasiPegawai,SUM(RealisasiBarangJasa) as RealisasiBarangJasa,SUM(RealisasiPemeliharaan) as RealisasiPemeliharaan,SUM(RealisasiModal) as RealisasiModal,SUM(JumlahBelanja) as JumlahBelanja,SUM(RealisasiBelanja) as RealisasiBelanja" +
                            " from viewPerdaIIB   inner join mKategori on viewPerdaIIB.IDUrusan/100= mKategori.btKodeKategori where viewPerdaIIB.btJenis = 4 " +
                            " group by mKategori.btKodeKategori,mKategori.sNamaKategori ";
                
                SSQL = SSQL + " UNION  ";

                SSQL = SSQL + "  Select 4 as Level, '6.1' as Kelompok, 4 as KelompokBesar ,1 as KodeKategori , 20 as KodeUrusan, 0 as KodeSKPD, 0 as KOdeRekening,   " +
                            " 0 as Rek, mUrusan.sNamaUrusan as Nama, " +
                            " SUM(AnggaranPegawai) as AnggaranPegawai,SUM(AnggaranBarangJasa) as AnggaranBarangJasa,SUM(AnggaranPemeliharaan) as AnggaranPemeliharaan,SUM(AnggaranModal) as AnggaranModal," +
                            "SUM(RealisasiPegawai) as RealisasiPegawai,SUM(RealisasiBarangJasa) as RealisasiBarangJasa,SUM(RealisasiPemeliharaan) as RealisasiPemeliharaan,SUM(RealisasiModal) as RealisasiModal,SUM(JumlahBelanja) as JumlahBelanja,SUM(RealisasiBelanja) as RealisasiBelanja " +
                            " from viewPerdaIIB  inner join mUrusan on viewPerdaIIB.IDUrusan= mUrusan.ID  where viewPerdaIIB.btJenis = 4 group by mUrusan.btKodeKategori,mUrusan.btKodeUrusan,mUrusan.sNamaUrusan ";

                SSQL = SSQL + " UNION  ";
                SSQL = SSQL + "  Select 5 as Level, '6.1' as Kelompok, 4 as KelompokBesar ,1 as KodeKategori , 20 as KodeUrusan, mSKPD.ID   as KodeSKPD, 0 as KOdeRekening,   " +
                            " 0 as Rek, mSKPD.sNamaSKPD  as Nama, " +
                            " SUM(AnggaranPegawai) as AnggaranPegawai,SUM(AnggaranBarangJasa) as AnggaranBarangJasa,SUM(AnggaranPemeliharaan) as AnggaranPemeliharaan,SUM(AnggaranModal) as AnggaranModal," +
                            "SUM(RealisasiPegawai) as RealisasiPegawai,SUM(RealisasiBarangJasa) as RealisasiBarangJasa,SUM(RealisasiPemeliharaan) as RealisasiPemeliharaan,SUM(RealisasiModal) as RealisasiModal,SUM(JumlahBelanja) as JumlahBelanja,SUM(RealisasiBelanja) as RealisasiBelanja" +
                            " from viewPerdaIIB   inner join mSKPD on viewPerdaIIB.IDDInas= mSKPD.ID  where viewPerdaIIB.btJenis = 4 " +
                            " group by mSKPD.ID , mskpd.sNamaSKPD ";

                
                SSQL = SSQL + " UNION Select 2 as Level, '6' as Kelompok, 5 as KelompokBesar ,1 AS  KodeKategori , 20  " +
                        " as KodeUrusan, Mskpd.id as KodeSKPD, mRekening.IIDRekening as KOdeRekening, LEFT(viewPerdaIIB.IIDRekening,2) Rek,  MrEKENING.sNamaRekening  as Nama, " +
                        " SUM(AnggaranPegawai) as AnggaranPegawai,SUM(AnggaranBarangJasa) as AnggaranBarangJasa,SUM(AnggaranPemeliharaan) as AnggaranPemeliharaan,SUM(AnggaranModal) as AnggaranModal," +
                            "SUM(RealisasiPegawai) as RealisasiPegawai,SUM(RealisasiBarangJasa) as RealisasiBarangJasa,SUM(RealisasiPemeliharaan) as RealisasiPemeliharaan,SUM(RealisasiModal) as RealisasiModal ,SUM(JumlahBelanja) as JumlahBelanja,SUM(RealisasiBelanja) as RealisasiBelanja" +
                         " from viewPerdaIIB   inner join mSKPD  on viewPerdaIIB.iddINAS= mSKPD.ID  " +
                        " INNER JOIN mRekening on Left(mRekening.IIDRekening,2) = LEFT(viewPerdaIIB.IIDRekening,2) " +
                        " where viewPerdaIIB.btJenis  = 5  and mRekening.btRoot in (2) " +
                        " group by Mskpd.id,Mskpd.sNamaskpd,mRekening.IIDRekening ," +
                        " LEFT(viewPerdaIIB.IIDRekening,2),mRekening.sNamaRekening ";


                SSQL = SSQL + " UNION ALL Select 3 as Level,'6.2' as Kelompok, 6 as KelompokBesar,mKategori.btKodeKategori  as KodeKategori,0 as KodeUrusan,0 as KodeSKPD, 0 as KOdeRekening,  " +
                            " 0 as Rek,mKategori.sNamaKategori as Nama, " +
                            " SUM(AnggaranPegawai) as AnggaranPegawai,SUM(AnggaranBarangJasa) as AnggaranBarangJasa,SUM(AnggaranPemeliharaan) as AnggaranPemeliharaan,SUM(AnggaranModal) as AnggaranModal," +
                            "SUM(RealisasiPegawai) as RealisasiPegawai,SUM(RealisasiBarangJasa) as RealisasiBarangJasa,SUM(RealisasiPemeliharaan) as RealisasiPemeliharaan,SUM(RealisasiModal) as RealisasiModal,SUM(JumlahBelanja) as JumlahBelanja,SUM(RealisasiBelanja) as RealisasiBelanja " +
                            " from viewPerdaIIB   inner join mKategori on viewPerdaIIB.IDUrusan/100= mKategori.btKodeKategori where viewPerdaIIB.btJenis = 5 " +
                            " group by mKategori.btKodeKategori,mKategori.sNamaKategori ";
                SSQL = SSQL + " UNION  ";

                SSQL = SSQL + "  Select 4 as Level, '6.2' as Kelompok, 6 as KelompokBesar ,mUrusan.btKodeKategori  as KodeKategori , mUrusan.btKodeUrusan as KodeUrusan, 0 as KodeSKPD, 0 as KOdeRekening,   " +
                            " 0 as Rek, mUrusan.sNamaUrusan as Nama, " +
                            " SUM(AnggaranPegawai) as AnggaranPegawai,SUM(AnggaranBarangJasa) as AnggaranBarangJasa,SUM(AnggaranPemeliharaan) as AnggaranPemeliharaan,SUM(AnggaranModal) as AnggaranModal," +
                            "SUM(RealisasiPegawai) as RealisasiPegawai,SUM(RealisasiBarangJasa) as RealisasiBarangJasa,SUM(RealisasiPemeliharaan) as RealisasiPemeliharaan,SUM(RealisasiModal) as RealisasiModal,SUM(JumlahBelanja) as JumlahBelanja,SUM(RealisasiBelanja) as RealisasiBelanja" +
                            " from viewPerdaIIB   inner join mUrusan on viewPerdaIIB.IDUrusan= mUrusan.ID  where viewPerdaIIB.btJenis = 5 " +
                            " group by mUrusan.btKodeKategori,mUrusan.btKodeUrusan,mUrusan.sNamaUrusan ";
                SSQL = SSQL + " UNION  ";
                SSQL = SSQL + "  Select 5 as Level, '6.2' as Kelompok, 6 as KelompokBesar ,mSKPD.btKodeKategori  as KodeKategori , mSKPD.btKodeUrusan as KodeUrusan, mSKPD.ID   as KodeSKPD, 0 as KOdeRekening,   " +
                            " 0 as Rek, mSKPD.sNamaSKPD  as Nama, " +
                            " SUM(AnggaranPegawai) as AnggaranPegawai,SUM(AnggaranBarangJasa) as AnggaranBarangJasa,SUM(AnggaranPemeliharaan) as AnggaranPemeliharaan,SUM(AnggaranModal) as AnggaranModal," +
                            " SUM(RealisasiPegawai) as RealisasiPegawai,SUM(RealisasiBarangJasa) as RealisasiBarangJasa,SUM(RealisasiPemeliharaan) as RealisasiPemeliharaan,SUM(RealisasiModal) as RealisasiModal,SUM(JumlahBelanja) as JumlahBelanja,SUM(RealisasiBelanja) as RealisasiBelanja " +
                            " from viewPerdaIIB   inner join mSKPD on viewPerdaIIB.IDDInas= mSKPD.ID  where viewPerdaIIB.btJenis = 5 " +
                            " group by mSKPD.btKodeKategori , mSKPD.btKodeUrusan , mSKPD.ID , mskpd.sNamaSKPD ";

                */

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
                                    RealisasiPemeliharaan= DataFormat.GetDecimal(dr["RealisasiPemeliharaan"]).ToRupiahInReport(),
                                    RealisasiModal = DataFormat.GetDecimal(dr["RealisasiModal"]).ToRupiahInReport(),
                                    JumlahRealisasi = DataFormat.GetDecimal(dr["RealisasiBelanja"]).ToRupiahInReport(),
                                    SelisihBelanja = (DataFormat.GetDecimal(dr["JumlahBelanja"])-DataFormat.GetDecimal(dr["RealisasiBelanja"])).ToRupiahInReport(),
                                    PersenBelanja = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["RealisasiBelanja"]),DataFormat.GetDecimal(dr["JumlahBelanja"]))
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
                   " WHERE B.IDUrusan< 499 AND a.Root = 0  AND B.iTahun = " + _p.Tahun.ToString() +
                   " group by B.IDDInas/100,  A.sNamaSKPD  ";

                SSQL = SSQL + " UNION ALL select 2 AS kelompok, D.ID/100  as Dinas,A.ID  as Urutan ,E.IsPokok as Pokok,B.IDurusan AS IDUrusan,0 as IDDInas, C.sNamaKategori + ' ' + A.sNamaUrusan as Nama  , " +
                       " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni, " +
                       " SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni, " +
                       " SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni " +
                       " from mUrusan A INNER JOIN " + namaView + " B ON A.ID= B.IDUrusan Inner join mKategori C ON A.btKodekategori = C.btKodekategori  " +
                       " INNER JOIN mSKPD D ON B.IDDInas/100 = D.ID/100  " +
                       " INNER JOIN mPelaksanaUrusan E ON E.IDDInas/100 = B.IDDinas/100 AND E.IDUrusan= B.IDUrusan AND E.IDDinas/100=D.ID/100 " +
                       " WHERE A.ID < 499 and B.iTahun = " + _p.Tahun.ToString() + " and d.Root=0 " +
                       " GROUP BY  d.ID/100, a.id, B.IDurusan,C.sNamaKategori,A.sNamaUrusan,E.IsPokok ";
                   
                
                SSQL = SSQL + " UNION ALL  " +
                      " select 5 AS kelompok, 55555 as dinas, 999 as Urutan,0 as Pokok ,0 AS IDUrusan, 0 As IDDinas, 'Jumlah ' as Nama,  " +
                      " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni, " +
                      " SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni, " +
                      " SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni " +
                      " from " + namaView + " B " +
                      " inner join mUrusan ON  mUrusan.ID= B.IDUrusan  inner join mSKPD ON mSKPD.ID= B.IDDInas WHERE  B.iTahun = " + _p.Tahun.ToString();


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
                                    Kode = DataFormat.GetInteger(dr["urutan"]) == 0 ? DataFormat.GetInteger(dr["dinas"]).ToKodeDinas() : (DataFormat.GetInteger(dr["urutan"]) > 0 && DataFormat.GetInteger(dr["urutan"]) < 999  ? DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan() : " "),
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
        public List<PerdaII> GetPerdaPembiayaanIIByOrg(ParameterLaporan _p)
        {
            List<PerdaII> _lst = new List<PerdaII>();
            try
            {
                // Cek View 
                string namaView;

                namaView = CreateViewPerdaII_B(_p, true);


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
                   " from mSKPD A INNER JOIN " + namaView + " B ON A.ID/100= B.IDDinas/100  " +
                   " WHERE B.IDUrusan< 499 AND a.Root = 0  AND B.iTahun = " + _p.Tahun.ToString() +
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
                   " WHERE A.ID < 499 and B.iTahun = " + _p.Tahun.ToString() +
                   " GROUP BY  A.ID , B.IDDInas, A.sNamaUrusan,C.sNamaKategori having SUM (B.Terima)>0 or SUM (B.TerimaMurni)>0 or SUM (B.Bayar)>0  " +
                   "   ";


                //SSQL = SSQL + " UNION ALL  " +
                //      " select 5 AS kelompok, '99999999999999999' as Urutan,0 AS IDUrusan, 0 As IDDinas, 'Jumlah ' as Nama,  " +
                //      " SUM (B.Pendapatan) as SUMPendapatan , SUM (PendapatanMUrni) as SUMPendapatanMUrni, " +
                //      " SUM (BTL) as SUMBTL , SUM (BTLMUrni) as SUMBTLMUrni, " +
                //      " SUM (BL) as SUMBL , SUM (BLMUrni) as SUMBLMUrni " +
                //      " from " + namaView + " B " +
                //      " inner join mUrusan ON  mUrusan.ID= B.IDUrusan  inner join mSKPD ON mSKPD.ID= B.IDDInas WHERE  B.iTahun = " + _p.Tahun.ToString();


                SSQL = SSQL + " UNION ALL  " +
                        " select 6 AS kelompok, '999999999999999991' as Urutan ,0 AS IDUrusan, 0 As IDDinas, 'SURPLUS/DEFISIT ' as Nama,  " +
                        "  SUM (Terima+Pendapatan- BTL-BL-Bayar)  as SUMPendapatan , " +
                        " SUM (TerimaMUrni+PendapatanMurni- BTLMUrni-BLMurni-BayarMurni)   as SUMPendapatanMUrni, " +
                        " 0 as SUMBTL , 0  as SUMBTLMUrni, " +
                                      " 0 as SUMBL , 0 as SUMBLMUrni   from  " + namaView + "  having SUM (Terima)>0 or SUM (TerimaMurni)>0 or SUM (Bayar)>0  Order by Urutan ";






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

        public List<Penjabaran> GetPenjabaran(ParameterLaporan _p)
        {
            List<Penjabaran> _lst = new List<Penjabaran>();
            try
            {
                // Cek View 
                //CreateViewPerdaII();

                SSQL = "Select 0 as Urutan , A.iTahun, A.IDDInas,A.IDUrusan,A.btJenis ,A.IDPRogram, A.IDKEgiatan, A.IIDRekening,A.Rek as IDRek, A.Root as btRoot, " +
                        " sNamaRekening,  JumlahOlah, Jumlah, JumlahMurni,  '' as Label, '' as sUraian, 0 as JumlahUraian, 0 as btUrut,0 as Level from [vwAnggaranAllLevel] A " +
                        " WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND A.IDDInas=" + _p.IDDinas.ToString() + " AND A.btJenis =1 AND A.btJenis  in " + _p.Jenis  ;

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
                            " WHERE B.iTahun = " + _p.Tahun.ToString() + " AND B.IDDInas=" + _p.IDDinas.ToString() + " AND B.btJenis =3   AND B.btJenis  in " + _p.Jenis  + " GROUP BY B.iTahun ,B.IDDInas,A.ID , A.sNamaUrusan ";

                
                SSQL = SSQL + " UNION ALL Select 7 as Urutan,A.iTahun ,A.IDDInas,A.IDurusan,3 as btJenis, A.IDProgram , 0 as IDkegiatan,0 as Rek, 0 as IIDRekening,0 as Root, " +
                                " A.sNamaProgram  as sNamaRekening, SUM(b.cJumlahOlah) as JumlahOlah, 0 as Jumlah, 0 as JumlahMurni,'' as Label, '' as sUraian, 0 as JumlahUraian, 0 as btUrut, 0 as Level   " +
                                " from tPrograms_A a Inner JOIN tAnggaranRekening_A B ON A.iTahun = B.iTahun and A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan   " +
                                " AND A.IDProgram= B.IDProgram WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND A.IDDInas=" + _p.IDDinas.ToString() + " AND A.btJenis =3  AND A.btJenis  in " + _p.Jenis  +
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
                        " WHERE iTAhun = " + _p.Tahun.ToString() + " AND IDDInas=" + _p.IDDinas.ToString() + " AND Jenis =3  AND Jenis  in " + _p.Jenis ;

                                

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
                                                                DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]),m_ProfileProgKegiatan,m_ProfileRekening),
                                        Urutan= DataFormat.GetInteger(dr["Urutan"]),
                                        Level = DataFormat.GetInteger(dr["Level"]),                                        
                                        IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                        IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
                                        IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                        IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                        JenisAnggaran= DataFormat.GetInteger(dr["btJenis"]),
                                        Root = DataFormat.GetInteger(dr["btRoot"]),                                                                              
                                        Nama = DataFormat.GetString(dr["sNamaRekening"]),
                                        Jumlah= DataFormat.GetDecimal(dr["JumlahOlah"]).ToRupiahInReport(),
                                        JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport(),
                                        Keterangan1= DataFormat.GetString(dr["sUraian"]).Trim(),
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
                                    Anggaran  = DataFormat.GetDecimal(dr["Anggaran"]).ToRupiahInReport(),
                                    Realisasi= DataFormat.GetDecimal(dr["Realisasi"]).ToRupiahInReport(),
                                    Selisih = (DataFormat.GetDecimal(dr["Anggaran"]) - DataFormat.GetDecimal( dr["Realisasi"])).ToRupiahInReport(),
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
        //public List<PerdaV> GetPerdaV(ParameterLaporan _p)
        //{
        //    List<PerdaV> mListUnit = new List<PerdaV>();
        //    try
        //    {
        //        // Cek View 
        //        //CreateViewPerdaII();
        //        //if (_p.Tahap == 0 || _p.Tahap == 2)
        //        //{
        //            SSQL = " Select  A.btKodeFungsi, 0 as IDURusan,A.sNamaFungsi as Nama, " +
        //                " SUM(CASE WHEN C.btJenis= 2 and C.IIDRekening like '511%' THEN C.cJumlahRKA ELSE 0 END) AS JumlahBTLPegawai," +
        //                " SUM(CASE WHEN C.btJenis= 2 and C.IIDRekening > 51200000 THEN C.cJumlahRKA ELSE 0 END) AS JumlahBTLNonPegawai," +
        //                " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '521%' THEN C.cJumlahRKA ELSE 0 END) AS JumlahBLPegawai," +
        //                " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '522%' THEN C.cJumlahRKA ELSE 0 END) AS JumlahBLBarangJasa," +
        //                " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '523%' THEN C.cJumlahRKA ELSE 0 END) AS JumlahBLModal, " +
        //                " SUM(CASE WHEN C.btJenis in (2,3) THEN C.cJumlahRKA ELSE 0 END) AS Jumlah " +
        //                " from mFUngsi A INNER JOIN mUrusan B ON A.btKodeFungsi = B.btKodeFungsi " +
        //                " INNER JOIN tAnggaranRekening_A C ON B.ID= C.IDUrusan " +
        //                " INNER JOIN tKegiatan_A D ON C.iTahun = D.iTahun AND C.IDDInas = D.IDDInas and C.IDUrusan = D.IDurusan AND C.IDProgram = D.IDProgram AND C.IDKegiatan = D.IDkegiatan And C.btJenis = D.btJenis WHERE C.iTAhun = " + _p.Tahun.ToString()  + 
        //                " GROUP BY A.btKodeFungsi ,A.sNamaFungsi " +
        //                " UNION ALL " +
        //                " Select  A.btKodeFungsi, B.ID as IDURusan,B.sNamaUrusan as Nama, " +
        //                " SUM(CASE WHEN C.btJenis= 2 and C.IIDRekening like '511%' THEN C.cJumlahRKA ELSE 0 END) AS JumlahBTLPegawai," +
        //                " SUM(CASE WHEN C.btJenis= 2 and C.IIDRekening > 51200000 THEN C.cJumlahRKA ELSE 0 END) AS JumlahBTLNonPegawai," +
        //                " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '521%' THEN C.cJumlahRKA ELSE 0 END) AS JumlahBLPegawai," +
        //                " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '522%' THEN C.cJumlahRKA ELSE 0 END) AS JumlahBLBarangJasa," +
        //                " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '523%' THEN C.cJumlahRKA ELSE 0 END) AS JumlahBLModal," +
        //                " SUM(CASE WHEN C.btJenis in (2,3) THEN C.cJumlahRKA ELSE 0 END) AS Jumlah " +
        //                " from mFUngsi A INNER JOIN mUrusan B ON A.btKodeFungsi = B.btKodeFungsi " +
        //                " INNER JOIN tAnggaranRekening_A C ON B.ID= C.IDUrusan " +
        //                " INNER JOIN tKegiatan_A D ON C.iTahun = D.iTahun AND C.IDDInas = D.IDDInas and C.IDUrusan = D.IDurusan AND C.IDProgram = D.IDProgram AND C.IDKegiatan = D.IDkegiatan And C.btJenis = D.btJenis WHERE C.iTAhun = " + _p.Tahun.ToString() + 
        //                    " GROUP BY A.btKodeFungsi,B.ID,B.sNamaUrusan" +
        //                " UNION ALL " +
        //                " Select  99 as btKodeFungsi, 999  as IDURusan,'JUMLAH' as Nama, " +
        //                " SUM(CASE WHEN C.btJenis= 2 and C.IIDRekening like '511%' THEN C.cJumlahRKA ELSE 0 END) AS JumlahBTLPegawai," +
        //                " SUM(CASE WHEN C.btJenis= 2 and C.IIDRekening > 51200000 THEN C.cJumlahRKA ELSE 0 END) AS JumlahBTLNonPegawai," +
        //                " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '521%' THEN C.cJumlahRKA ELSE 0 END) AS JumlahBLPegawai," +
        //                " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '522%' THEN C.cJumlahRKA ELSE 0 END) AS JumlahBLBarangJasa," +
        //                " SUM(CASE WHEN C.btJenis= 3 and C.IIDRekening like '523%' THEN C.cJumlahRKA ELSE 0 END) AS JumlahBLModal," +
        //                " SUM(CASE WHEN C.btJenis in (2,3) THEN C.cJumlahRKA ELSE 0 END) AS Jumlah " +
        //                " from tAnggaranRekening_A C " +
        //                " WHERE C.iTAhun = " + _p.Tahun.ToString() +
        //                " ORDER BY A.btKodeFungsi,A.IDUrusan,Nama ";
                    
        //        //}
                
        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //              //  if (_p.Tahap == 0 || _p.Tahap == 2)
        //               // {
        //                    mListUnit = (from DataRow dr in dt.Rows
        //                            select new PerdaV()
        //                            {
        //                                KodeFungsi = DataFormat.GetInteger(dr["btKodeFungsi"]).IntToStringWithLeftPad(1),
        //                                KodeUrusan = DataFormat.GetString(dr["IDUrusan"]).Substring(0,1), 
        //                                Kode = DataFormat.GetString(dr["IDUrusan"]).Substring(1) , //DataFormat.GetInteger(dr["btKodeFungsi"]).IntToStringWithLeftPad(1) + (DataFormat.GetInteger(dr["IDUrusan"])>0? "." + DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan():""),
        //                                Nama = DataFormat.GetString(dr["Nama"]),
        //                                BTLNonPegawai = DataFormat.GetDecimal(dr["JumlahBTLNonPegawai"]).ToRupiahInReport(),
        //                                BTLPegawai = DataFormat.GetDecimal(dr["JumlahBTLPegawai"]).ToRupiahInReport(),
        //                                BLPegawai = DataFormat.GetDecimal(dr["JumlahBLPegawai"]).ToRupiahInReport(),
        //                                BLBarangJasa = DataFormat.GetDecimal(dr["JumlahBLBarangJasa"]).ToRupiahInReport(),
        //                                BLModal = DataFormat.GetDecimal(dr["JumlahBLModal"]).ToRupiahInReport(),
        //                                A= DataFormat.GetDecimal(dr["JumlahBTLNonPegawai"]).ToRupiahInReport(),
        //                                B = DataFormat.GetDecimal(dr["JumlahBTLPegawai"]).ToRupiahInReport(),
        //                                C = DataFormat.GetDecimal(dr["JumlahBLPegawai"]).ToRupiahInReport(),
        //                                D = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
                                        

        //                            }).ToList();
        
        //            }
        //        }
        //        return mListUnit;
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message;
        //        return mListUnit;
        //    }
        //}

        public List<PerdaIV> GetPerdaIV(ParameterLaporan _p)
        {
            List<PerdaIV> _lst = new List<PerdaIV>();
            try
            {
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
                    " WHERE A.iTahun = " + _p.Tahun.ToString() +  " AND c.root=1 " +
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
                SSQL = SSQL + " Select 4 as Level,  A.IDDInas/100 as IDDInas,A.IDUrusan,A.IDProgram, 0 as IDKegiatan, B.sNamaProgram as Nama, "+
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
                                        Kode = GetKodeIV(DataFormat.GetInteger(dr["Level"]),DataFormat.GetInteger(dr["IDUrusan"]), DataFormat.GetInteger(dr["IDDInas"]),DataFormat.GetInteger(dr["IDProgram"]),DataFormat.GetInteger(dr["IDKegiatan"])),
                                        IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                        IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                        IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                        IDkegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                        Nama = DataFormat.GetInteger(dr["Level"]) < 5 ? DataFormat.GetString(dr["Nama"]).ToUpper() : DataFormat.GetString(dr["Nama"]),                                        
                                        BP = DataFormat.GetDecimal(dr["BP"]).ToRupiahInReport(),
                                        BBJ = DataFormat.GetDecimal(dr["BBJ"]).ToRupiahInReport(),
                                        BM = DataFormat.GetDecimal(dr["BM"]).ToRupiahInReport(),
                                        Jumlah = (DataFormat.GetDecimal(dr["BM"]) + DataFormat.GetDecimal(dr["BBJ"]) + DataFormat.GetDecimal(dr["BP"])).ToRupiahInReport(),
                                        BPMurni =  DataFormat.GetDecimal(dr["BPM"]).ToRupiahInReport(),
                                        BBJMurni = DataFormat.GetDecimal(dr["BBJM"]).ToRupiahInReport(),
                                        BMMurni = DataFormat.GetDecimal(dr["BMM"]).ToRupiahInReport(),
                                        JunmlahMurni = (DataFormat.GetDecimal(dr["BMM"]) + DataFormat.GetDecimal(dr["BBJM"]) + DataFormat.GetDecimal(dr["BPM"])).ToRupiahInReport(),
                                        Selisih = ((DataFormat.GetDecimal(dr["BMM"]) + DataFormat.GetDecimal(dr["BBJM"]) + DataFormat.GetDecimal(dr["BPM"]))-(DataFormat.GetDecimal(dr["BM"]) + DataFormat.GetDecimal(dr["BBJ"]) + DataFormat.GetDecimal(dr["BP"]))).ToRupiahInReport(),
                                        persentase =DataFormat.GetProsentaseRealisasi((DataFormat.GetDecimal(dr["BMM"]) + DataFormat.GetDecimal(dr["BBJM"]) + DataFormat.GetDecimal(dr["BPM"])), (DataFormat.GetDecimal(dr["BM"]) + DataFormat.GetDecimal(dr["BBJ"]) + DataFormat.GetDecimal(dr["BP"])))
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
                _lst.Insert(ihasrussisip+2, blanjPerda);
                return _lst;
                
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
            
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
                                     Kode = DataFormat.GetInteger(dr["IDUrusan"]).ToKodeUrusan() + "."  +DataFormat.GetInteger(dr["IDDInas"]).ToKodeDinas (),        
                                    APBDLalu = DataFormat.GetDecimal(dr["cAnggaranTahunlalu"]).ToRupiahInReport(),
                                    APBDPLalu = DataFormat.GetDecimal(dr["APBDPLAlu"]).ToRupiahInReport(),
                                    Realisasi = DataFormat.GetDecimal(dr["cRealisasi"]).ToRupiahInReport(),
                                    APBDKini = DataFormat.GetDecimal(dr["APBDSekarang"]).ToRupiahInReport(),
                                    Perubahan ="0"
                           
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

        private string GetKodeIV(int _level, int _idURusan, int _iddinas, int _idProgram, int _idKegiatan)
        {
            string s="";
            switch (_level)
            {
                case 1:
                    s= _idURusan.ToKodeUrusan();
                    break;
                case 2:
                    s = _idURusan.ToKodeUrusan();
                    break;
                case 3:
                    s = _iddinas.ToKodeDinas();
                    break;
                case 4:
                    s = _idURusan.ToKodeUrusan() +"." +  _iddinas.ToKodeDinas()+"."+ _idProgram.ToSimpleKodeProgram();
                    break;
                case 5:
                    s = _idURusan.ToKodeUrusan() + "." + _iddinas.ToKodeDinas() + "." + _idProgram.ToSimpleKodeProgram() + "." + _idKegiatan.ToSimpleKodeKegiatan();
                    break;

            }
            return s;
        }
        private string GetKodeII(int _level,int _idURusan, int _iddinas, int koderekening)
        {
            string s = "";
            switch (_level)
            {
                case 0:
                    s = "";// (_idURusan/100).ToString("D");
                    break;
                case 1:

                    s = _idURusan.ToKodeUrusan();
                    break;
                case 2:
                    s = _iddinas.ToKodeDinas();
                    break;
                case 3:
                    s = _idURusan.ToKodeUrusan() + "." + _iddinas.ToKodeDinas();
                    break;
                case 4:
                    s = _idURusan.ToKodeUrusan() + "." + _iddinas.ToKodeDinas() + "." + koderekening.ToString().Substring(0, 1) + "." + koderekening.ToString().Substring(2, 1);

                    break;
                case 5:
                    s = _idURusan.ToKodeUrusan() + "." + _iddinas.ToKodeDinas() + "." + koderekening.ToString().Substring(0, 1) + "." + koderekening.ToString().Substring(2, 1) + "." + koderekening.ToString().Substring(3, 1);

                    break;

            }
            return s;
        }

        // CEK View utk Perdan 
        // 
        private string  CreateViewAllLevel(ParameterLaporan _p, bool create)
        {
            string namaView = "viewAnggaran" +_p.NamaUser.Trim().Replace(" ","");

            HapusView(namaView);
            
            if (create == true)
            {
                GetKolom(_p.Tahap);

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


                //SSQL = " CREATE VIEW " + namaView + " AS " +
                //    " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, A.IIDRekening as Rek,b.IIDRekening, b.btRoot as Root," +
                //    " b.sNamaRekening ,  A." + _namaKolom2 + " AS Jumlah, A." + _namaKolom1 + " AS JumlahMurni ,b.iDebet as Debet  " +
                //    " FROM tAnggaranRekening_A A inner join mRekening b on a.IIDRekening=b.IIDRekening  inner join mSKPD C on a.iDDINAS = C.id " +
                //      " where b.btRoot=5 and C.Root =1 " +
                //    " UNION ALL  " +
                //      " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + " ) as Rek,b.IIDRekening,b.btRoot as Root," +
                //      " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                //      " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                //    "  where b.btRoot=4 and c.root = 1 " +
                //      " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening,5), b.btRoot ,b.IIDRekening, b.sNamaRekening ,b.iDebet " +
                //      " UNION ALL  " +
                //      " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root, " +
                //        " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet " +
                //      " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")  " +
                //      " inner join mSKPD C on a.iDDINAS = C.id " +
                //   "  where b.btRoot=3 and c.root = 1  " +
                //      " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet  " +
                //      " UNION ALL  " +
                //      " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ") as Rek,b.IIDRekening, " +
                //      " b.btRoot as Root,b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                //      " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN2.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN2.ToString() + ")   inner join mSKPD C on a.iDDINAS = C.id " +
                //    "    where b.btRoot=2 and c.root = 1  " +
                //      " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet  " +
                //      " UNION ALL  " +
                //      " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root,b.sNamaRekening ,  " +
                //      " SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                //      " FROM tAnggaranRekening_A A inner join mRekening b on LEFT(a.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")    inner join mSKPD C on a.iDDINAS = C.id " +
                //    "  where b.btRoot=1 and c.root = 1  " +
                //      " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet";

                _dbHelper.ExecuteNonQuery(SSQL);
            }
            return namaView;

        }
        private string CreateViewAllLevel64(ParameterLaporan _p, bool create)
        {
            string namaView = "viewAnggaran" + _p.NamaUser.Trim().Replace(" ", "");

            HapusView(namaView);

            if (create == true)
            {
                GetKolom(_p.Tahap);

                SSQL = " CREATE VIEW " + namaView + " AS " +
                    " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, A.IIDRekening64 as Rek,b.IIDRekening, b.btRoot as Root," +
                    " b.sNamaRekening ,  A." + _namaKolom2 + " AS Jumlah, A." + _namaKolom1 + " AS JumlahMurni ,b.iDebet as Debet  " +
                    " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on A.IIDRekening64=b.IIDRekening  inner join mSKPD C on a.iDDINAS = C.id " +
                    " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND " +
                    " A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=5 and C.Root =1 " +
                    " UNION ALL  " +
                      " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64, " + m_ProfileRekening.LEN4.ToString() + " ) as Rek,b.IIDRekening,b.btRoot as Root," +
                      " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                      " , a.bppkd  FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(A.IIDRekening64, " + m_ProfileRekening.LEN4.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN4.ToString() + ")  inner join mSKPD C on a.iDDINAS = C.id " +
                    " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis   where b.btRoot=4 and c.root = 1 " +
                      " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64,5), b.btRoot ,b.IIDRekening, b.sNamaRekening ,b.iDebet, a.bppkd  " +
                      " UNION ALL  " +
                      " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN3.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root, " +
                        " b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet " +
                      " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(A.IIDRekening64, " + m_ProfileRekening.LEN3.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN3.ToString() + ")  " +
                      " inner join mSKPD C on a.iDDINAS = C.id " +
                    " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis  where b.btRoot=3 and c.root = 1  " +
                      " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN3.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet, a.bppkd   " +
                      " UNION ALL  " +
                      " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN2.ToString() + ") as Rek,b.IIDRekening, " +
                      " b.btRoot as Root,b.sNamaRekening ,  SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                      " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(A.IIDRekening64, " + m_ProfileRekening.LEN2.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN2.ToString() + ")   inner join mSKPD C on a.iDDINAS = C.id " +
                    " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=2 and c.root = 1  " +
                      " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN2.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet, a.bppkd   " +
                      " UNION ALL  " +
                      " Select A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN1.ToString() + ") as Rek,b.IIDRekening, b.btRoot as Root,b.sNamaRekening ,  " +
                      " SUM(A." + _namaKolom2 + ") AS JUMLAH, SUM(A." + _namaKolom1 + ") AS JumlahMurni ,b.iDebet as Debet  " +
                      " , a.bppkd FROM tAnggaranRekening_A A inner join mRekening_SAP b on LEFT(A.IIDRekening64, " + m_ProfileRekening.LEN1.ToString() + ")=LEFT(b.IIDRekening, " + m_ProfileRekening.LEN1.ToString() + ")    inner join mSKPD C on a.iDDINAS = C.id " +
                    " INNER JOIN TkEGIATAN_a D on A.iTahun = d.iTahun and A.IDDInas = d.IDDinas and A.IDUrusan = d.IDUrusan  AND A.IDKegiatan = D.IDKegiatan and A.btJenis = D.btJenis    where b.btRoot=1 and c.root = 1  " +
                      " Group BY A.iTahun, A.IDDInas, A.IDurusan,A.btJenis,A.IDprogram,A.IDKegiatan, Left(A.IIDRekening64," + m_ProfileRekening.LEN1.ToString() + "), b.btRoot ,b.IIDRekening, b.sNamaRekening,b.iDebet, a.bppkd ";


                _dbHelper.ExecuteNonQuery(SSQL);
            }
            return namaView;

        }
        public List<PerdaIII> GetPerdaIII(ParameterLaporan _p)
        {

          
            string namaView = CreateViewAllLevel(_p, true);

            BersihkanNonKegiatan();

            List<PerdaIII> _lst = new List<PerdaIII>();
            try
            {

                SSQL = "Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok from " + namaView + "  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root < 3 and (" + namaView + ".Jumlah>=0   or " + namaView + ".JumlahMurni>=0) ";
                    
                if (_p.IDDinas>0 ){
                        SSQL=SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() ;
                        SSQL = SSQL + " AND " + namaView + ".bPPKD=" + _p.bPPKD.ToString();
                     
                }

                //SSQL =SSQL + "UNION ALL Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root+2 as Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok from " + namaView + "  " +
                //    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root = 3 and (" + namaView + ".Jumlah>0   or " + namaView + ".JumlahMurni>0) ";

                //if (_p.IDDinas > 0)
                //{
                //    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                //    SSQL = SSQL + " AND " + namaView + ".bPPKD=" + _p.bPPKD.ToString();

                //}

                    SSQL=SSQL + " UNION ALL    ";

                    if (_p.LastLevel == 3)
                    {
                      SSQL = SSQL + "  Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root+2,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView +
                        "  Left outer JOIN mDasarHukum ON Left(mDasarHukum.IIDRekening,3)=left(" + namaView + ".IIDRekening,3)  " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root = 3 and (" + namaView + ".Jumlah >= 0  or " + namaView + ".JumlahMurni>=0)  ";//and  " +
                        //namaView + ".IDDInas =" + _p.IDDinas.ToString();
                    }
                    else
                    {
                        SSQL = SSQL + "  Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root +2 as Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok from " + namaView +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root >= 3  and " + namaView + ".Root <5 and (" + namaView + ".Jumlah > 0  or " + namaView + ".JumlahMurni>0) ";//and  " +


                        if (_p.IDDinas > 0)
                        {
                            SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                            SSQL = SSQL + " AND " + namaView + ".bPPKD=" + _p.bPPKD.ToString();

                        }
                        SSQL = SSQL + "  UNION Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root +2 as Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView +
                       "  Left Outer  JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                       " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root >= 3  and " + namaView + ".Root =5 and (" + namaView + ".Jumlah > 0  or " + namaView + ".JumlahMurni>0) ";//and  " +
                      
                    }
               
    
                
                if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                       SSQL = SSQL + " AND " + namaView + ".bPPKD=" + _p.bPPKD.ToString();
                         
                }
              //  SSQL = SSQL + "and (( mDasarHukum.IIDRekening in (Select IIDRekening from tAnggaranRekening_A where IDDInas=" + _p.IDDinas.ToString()+ " )) ";
               // SSQL = SSQL + "OR ( mDasarHukum.IIDRekening in (Select IIDRekening/100 * 100 from tAnggaranRekening_A where IDDInas=" + _p.IDDinas.ToString() + " )) ) ";

              //  SSQL = SSQL + "Union ALL Select  " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah," +
              //              "JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok from " + namaView + " WHERE " + namaView + ".iTAhun= " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 " +
              //                  //" and " + namaView + ".Root = 3 and (" + namaView + ".Jumlah > 0  or " + namaView + ".JumlahMurni > 0 )  AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + "  and " + namaView + ".IIDRekening/10000 not in (Select IIDRekening/10000 from mDasarHukum) ";
                
                SSQL = SSQL + " UNION ALL    ";
                SSQL = SSQL + " Select 2 as btJenis," + namaView + ".IDDInas/10000 as IDUrusan ," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  and " + namaView + ".btJenis in (2,3) and " + namaView + ".Root =1 " ;
                    
                    if (_p.IDDinas>0 ){
                        SSQL=SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() ;
                        SSQL = SSQL + " AND " + namaView + ".bPPKD=" + _p.bPPKD.ToString();
                     
                    } 
                   SSQL=SSQL + " GROUP BY  " + namaView + ".IDDInas," + namaView + ".IIDREKENING,ROOT,sNamaRekening, mDasarHukum.iNo, mDasarHukum.sKeterangan  ";
                   SSQL=SSQL + " UNION ALL    ";

                    
            

                SSQL = SSQL + " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis= 2 and " + namaView + ".Root =2  and (" + namaView + ".Jumlah>0 or " + namaView + ".JumlahMurni>0)  and " + namaView + ".Root < 4   ";
               

                
                if (_p.IDDinas>0 ){
                        SSQL=SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() ;
                        SSQL = SSQL + " AND " + namaView + ".bPPKD=" + _p.bPPKD.ToString();
                     
               }
                SSQL = SSQL + " UNION ALL ";
                SSQL = SSQL + " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root+2,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis= 2 and " + namaView + ".Root =3  and (" + namaView + ".Jumlah>0 or " + namaView + ".JumlahMurni>0)  and " + namaView + ".Root < 4   ";



                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                    SSQL = SSQL + " AND " + namaView + ".bPPKD=" + _p.bPPKD.ToString();

                }
                SSQL = SSQL + " UNION ALL ";

                SSQL = SSQL + " Select " + namaView + ".btJenis, " + namaView + ".IDDInas/10000 as  IDUrusan," + namaView + ".IDDInas, 1 AS IDProgram , 1 AS IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening , SUM(Jumlah) AS Jumlah, SUM(JumlahMurni) AS JumlahMurni ,mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as IsPokok  from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 3 and Root =2  " ;
                 if (_p.IDDinas>0 ){
                        SSQL=SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() ;
                        SSQL = SSQL + " AND " + namaView + ".bPPKD=" + _p.bPPKD.ToString();
                     
                 }
                    
                    SSQL=SSQL +" GROUP BY " + namaView + ".btJenis," + namaView + ".IDDInas, " + namaView + ".IIDRekening,Root,sNamaRekening,mDasarHukum.iNo,mDasarHukum.sKeterangan ";

                SSQL = SSQL + "  UNION ALL  ";

                SSQL = SSQL + "  Select 3 as btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, 0 as IDKegiatan,0 as IIDRekening,3 as Root, A.sNamaProgram as sNamaRekening,    " +
                    "  SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan, mPelaksanaURusan.IsPokok FROM tPrograms_A A   " +
                    " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan    " +
                    " AND A.IDProgram = B.IDProgram   " +
                    " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
                    " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE A.iTAhun = " + _p.Tahun.ToString() +  " AND   B.btJEnis= 3 and b.Root = 5 AND (b.Jumlah>0 or B.JumlahMurni>0)   " ;
                    if (_p.IDDinas>0 ){
                        SSQL=SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString() ;
                        SSQL = SSQL + " AND B.bPPKD = " + _p.bPPKD.ToString() ; 

                    }
                    
                    SSQL=SSQL+ " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram, A.sNamaProgram,mPelaksanaURusan.IsPokok" +
                    " UNION ALL    " +
                    " Select 3.btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, A.IDkegiatan  as IDKegiatan,0 as IIDRekening,4 as Root, A.sNama as sNamaRekening,   " +
                    " SUM(B.Jumlah) as Jumlah, SUM(B.JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan,mPelaksanaUrusan.IsPokok FROM tKegiatan_A A   " +
                    " INNER JOIN " + namaView + " B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan   AND A.IDProgram = B.IDProgram and A.IDkegiatan=B.IDKegiatan  " +
                " INNER JOIN mPElaksanaurusan on B.IDDInas = mPelaksanaUrusan.IDDinas and  B.iTahun = mPelaksanaUrusan.iTahun and B.IDUrusan = mPelaksanaUrusan.IDUrusan " +
                " AND A.IDDInas = mPelaksanaUrusan.IDDinas and  A.iTahun = mPelaksanaUrusan.iTahun and A.IDUrusan = mPelaksanaUrusan.IDUrusan    " +
                " WHERE B.iTAhun = " + _p.Tahun.ToString() + " AND B.btJEnis= 3 and B.Root=5 AND (B.Jumlah>0 or B.JumlahMurni>0)" ;
                      if (_p.IDDinas>0 ){
                        SSQL=SSQL + " AND A.IDDinas=" + _p.IDDinas.ToString() ;
                        SSQL = SSQL + " AND B.bPPKD = " + _p.bPPKD.ToString(); 

                        SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok  ";

                      }
                      else
                      {
                          SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDProgram, A.sNama,A.IDkegiatan,mPelaksanaURusan.IsPokok  ";
                      }
   
                
                
                if (_p.LastLevel == 3)
                {
                    SSQL = SSQL + " UNION ALL  " +
                        " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root+2,sNamaRekening, Jumlah, JumlahMurni , mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan ,mPelaksanaUrusan.IsPokok  from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                        " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "   AND  " + namaView + ".btJEnis in ( 3) and Root = 3 and (Jumlah  > 0  or JumlahMurni>0)";
                        if (_p.IDDinas>0 ){
                            SSQL=SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() ;
                            SSQL = SSQL + " AND " + namaView + ".bPPKD=" + _p.bPPKD.ToString();
                     
                        }
   
                }
                else
                {

                    SSQL = SSQL + "  UNION ALL  " +
                    " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root +2 as Root,sNamaRekening, Jumlah, JumlahMurni , mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan ,mPelaksanaUrusan.IsPokok  from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis in (3,2) and Root > 3 and Root < = 5  and (Jumlah  > 0  or JumlahMurni>0)  ";
                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                        SSQL = SSQL + " AND " + namaView + ".bPPKD=" + _p.bPPKD.ToString();
                     
                    }

                }
                 
                //SSQL = SSQL + "  order by btJenis,Ispokok, IDUrusan," + namaView + ".IDDInas, IDProgram,IDkegiatan, " + namaView + ".IIDRekening,Root,iNo  ";
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
                                    Kode = DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                            DataFormat.GetInteger(dr["IDDInas"]),
                                                            DataFormat.GetInteger(dr["IDProgram"]),
                                                            DataFormat.GetInteger(dr["IDkegiatan"]),
                                                            DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) ,
                                    Nama = DataFormat.UpperFirst (DataFormat.GetString(dr["sNamaRekening"]).Trim()) ,
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport(),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"].ToString()).ToRupiahInReport(),
                                    Selisih = (DataFormat.GetDecimal(dr["Jumlah"].ToString()) - DataFormat.GetDecimal(dr["JumlahMurni"].ToString())).ToRupiahInReport(),
                                    Prosentase = DataFormat.GetProsentaseRealisasi (DataFormat.GetDecimal(dr["Jumlah"].ToString()) , DataFormat.GetDecimal(dr["JumlahMurni"].ToString())),
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

                    for (int i = 0; i < _lst.Count;i++ )
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
                            //mListUnit[i].Jumlah = "";
                            cJumlah = curPerdaIII.Jumlah;
                            level = curPerdaIII.Level;

                        }


                    }
                }

                if (_p.IDDinas>0 ){
                        

                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,10 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                        " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 ) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=1 ) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000)  as BLJMURNI,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";
                }else{

                    SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,10 as Root,'SURPLUS/(DEFISIT)' as sNamaRekening, " +
                    " (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis=1 ) as PDPT , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " and Root = 1 and btJenis in(2,3) and IIDRekening> 4000000) as BLJ ,  " +
                        " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() +  " and Root = 1 and btJenis=1 ) as PDPTMURNI , " +
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
                            Selisih =DataFormat.GetSelisih ( DataFormat.GetDecimal(dr["PDPT"]) , DataFormat.GetDecimal(dr["BLJ"]),  DataFormat.GetDecimal(dr["PDPTMURNI"]) , DataFormat.GetDecimal(dr["BLJMURNI"])).ToRupiahInReport(),
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
                    lstpby= GetPerdaIIIPembiayaan(_p);
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

            string namaView=CreateViewAllLevel(_p, true);

            List<PerdaIII> _lst = new List<PerdaIII>();
            try{

                if (_p.IDDinas > 0)
                {
                    SSQL = "Select " + namaView + ".btJenis," + namaView + ".IDDInas/10000 IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, 0 as Jumlah, 0 as JumlahMurni, mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + " AND  " + namaView + ".btJEnis= 4 and " + namaView + ".Root = 1 and " + namaView + ".Jumlah>0   " +
                        "  ";


                    SSQL = SSQL + " UNION ALL Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString() + " AND  " + namaView + ".btJEnis= 4 and " + namaView + ".Root > 1 and " + namaView + ".Root < 4  and " + namaView + ".Jumlah>0   " +
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
                                        Tahun= _p.Tahun,
                                        Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"]==null ? 
                                               DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                                DataFormat.GetInteger(dr["IDDInas"]),
                                                                DataFormat.GetInteger(dr["IDProgram"]),
                                                                DataFormat.GetInteger(dr["IDkegiatan"]),
                                                                DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]),m_ProfileProgKegiatan,m_ProfileRekening) : "",
                                        Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                                        Jumlah =DataFormat.GetDecimal(dr["iNo"])<2?DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport():"",
                                        JumlahMurni = DataFormat.GetInteger(dr["iNo"]) > 0 ? DataFormat.GetInteger(dr["iNo"]).ToString() + "." : DataFormat.GetDecimal(dr["JumlahMurni"].ToString()).ToRupiahInReport(),
                                        Selisih ="0",
                                        Prosentase ="0",
                                        Keterangan = "",//DataFormat.GetString(dr["Keterangan"]),
                                        Level =DataFormat.GetSingle(dr["Root"]),
                                        Jenis= DataFormat.GetSingle(dr["btJenis"])

                                    }).ToList();
                        //}                        
                    }
                }

                for (int idx = 0; idx < _lst.Count;idx++ )
                {
                    if (_lst[idx].Level == 1)
                    {
                        _lst[idx].Jumlah = "";
                        _lst[idx].JumlahMurni = "";
                        _lst[idx].Prosentase = "";
                        _lst[idx].Selisih="";

                    }
                }
             
                SSQL = "  Select 4 as btJenis,555 as IDUrusan,5000000  as IDDInas, 0 AS IDProgram , 0 as IDkegiatan, 9999999  as IIDRekening,8 as Root,'PEMBIAYAAN NETTO' as sNamaRekening, " +
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

                        o= new PerdaIII()
                                {
                                    Tahun = _p.Tahun,
                                    Kode ="",
                                    Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                                    Jumlah = (DataFormat.GetDecimal(dr["PDPT"]) -DataFormat.GetDecimal(dr["BLJ"])).ToRupiahInReport() ,
                                    JumlahMurni = (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])).ToRupiahInReport(),
                                    Selisih = "0",
                                    Prosentase = "0",
                                    Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                    Level = DataFormat.GetSingle(dr["Root"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"])

                                };
                                          
                    }
                }
                  //Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                  //                         DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                  //                                          DataFormat.GetInteger(dr["IDDInas"]),
                  //                                          DataFormat.GetInteger(dr["IDProgram"]),
                  //                                          DataFormat.GetInteger(dr["IDkegiatan"]),
                  //                                          DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                                  

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
            string  _lastLevel = _p.LastLevel.ToString();
            List<PerdaIII> _lst = new List<PerdaIII>();
            try
            {

                SSQL = "Select " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni,0 as iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root <4 and " + namaView + ".Jumlah>0   ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening,mDasarHukum.iNo,mDasarHukum.sKeterangan ";

                // level 4
                SSQL = SSQL + "UNION ALL Select " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root =4 and " + namaView + ".Jumlah>0   ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening,mDasarHukum.iNo,mDasarHukum.sKeterangan ";

                // level 5 yang hukum level 4

                SSQL = SSQL + "Union all Select " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni,0 as iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening/1000=" + namaView + ".IIDRekening/1000 " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root = 5 and " + namaView + ".Jumlah>0   " +
                    " AND mDasarHukum.IIDrekening % 1000 = 0 AND " + namaView + ".IIDrekening in (Select IIDrekening from tAnggaranRekening_A where iTahun = " + _p.Tahun.ToString() + "  and tAnggaranRekening_A.IDDInas =" + namaView + ".IDDInas  ) ";


                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening,mDasarHukum.iNo,mDasarHukum.sKeterangan ";


                // level 5 yang hukum level 5
                SSQL = SSQL + "Union ALL Select " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni,0 as iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
                      " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root = 5 and " + namaView + ".Jumlah>0   " +
                      " AND mDasarHukum.IIDrekening % 1000 > 0 AND " + namaView + ".IIDrekening in (Select IIDrekening from tAnggaranRekening_A where iTahun =" + namaView + ".iTAhun and tAnggaranRekening_A.IDDInas =" + namaView + ".IDDInas  ) ";
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening,mDasarHukum.iNo,mDasarHukum.sKeterangan ";

                // tidak ada hukumnya

                SSQL = SSQL + "Union all Select " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan, 0 as isPokok from " + namaView + "  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root = 5 and " + namaView + ".Jumlah>0   " +
                    " AND " + namaView + ".IIDrekening/1000 not in (Select IIDrekening/1000 from mDasarHukum ) ";


                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening";



                
                
                
                
                
                SSQL = SSQL + " UNION ALL    ";




                SSQL = SSQL + " Select 2 as btJenis, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, 0 as iNo, '' as Keterangan , 0 as isPokok from " + namaView   +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  and " + namaView + ".btJenis in (2,3) and " + namaView + ".Root =1 and " + namaView + ".Jumlah>0    ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY " + namaView + ".IIDRekening,Root,sNamaRekening";



                SSQL = SSQL + " UNION ALL    ";
                // SSQL = SSQL + " UNION ALL ";

                SSQL = SSQL + " Select " + namaView + ".btJenis," + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni,mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis in  (2,3) and " + namaView + ".Root >1  and " + namaView + ".Jumlah>0  and " + namaView + ".Root <= " + _lastLevel + "   ";
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
                                    Nama =DataFormat.GetString(dr["sNamaRekening"]).Trim() ,// DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).Trim() : DataFormat.GetString(dr["sNamaRekening"]).Trim()) : "",
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport() ,
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport(),
                                    Selisih = (DataFormat.GetDecimal(dr["Jumlah"])-DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport() ,
                                    Prosentase = DataFormat.GetProsentase(DataFormat.GetDecimal(dr["JumlahMurni"]) , DataFormat.GetDecimal(dr["Jumlah"])),
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
                            Selisih = "0",
                            Prosentase = "0",
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

                    SSQL = "Select " + namaView + ".btJenis,"+ namaView + ".IIDRekening,Root,sNamaRekening, 0 as Jumlah, 0 as JumlahMurni, mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening " +
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

                SSQL = "  Select 4 as btJenis, 9999999  as IIDRekening,6 as Root,'PEMBIAYAAN NETTO' as sNamaRekening, " +
                    //" (SELECT SUM(Jumlah) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=4 and IIDRekening> 4000000) as Jumlah , " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + "  and Root = 1 and btJenis =4 and IIDRekening> 4000000) as Jumlah ,  " +
                      //  " (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + " AND IDDinas=" + _p.IDDinas.ToString() + " and Root = 1 and btJenis=4 and IIDRekening> 4000000) as PDPTMURNI , " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis =5 and IIDRekening> 4000000)  as JumlahMurni,0 as iNo,''  as Keterangan , " +
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
                            Jumlah = DataFormat.GetDecimal(dr["iNo"]) < 2 ? (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport() : "",
                            JumlahMurni = DataFormat.GetInteger(dr["iNo"]) > 0 ? DataFormat.GetInteger(dr["iNo"]).ToString() + "." : "",
                            Selisih = "0",
                            Prosentase = "0",
                            Keterangan = DataFormat.GetString(dr["Keterangan"]),
                            Level = DataFormat.GetSingle(dr["Root"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"])

                        };

                    }
                }
                _lst.Add(o);


                SSQL = "  Select 4 as btJenis, 9999999  as IIDRekening,6 as Root,'SISA LEBIH PEMBIAYAAN ANGGARAN TAHUN BERKENAN' as sNamaRekening, " +
                       "  (SELECT SUM(Jumlah) from " + namaView + " where iTAhun =  " + _p.Tahun.ToString() + "  and Root = 1 and btJenis in (1,4) and IIDRekening> 4000000) as Jumlah ,  " +
                       "  (SELECT SUM(JumlahMurni) from " + namaView + " where iTAhun = " + _p.Tahun.ToString() + " and Root = 1 and btJenis in (2,3,5) and IIDRekening> 4000000)  as JumlahMurni,0 as iNo,''  as Keterangan , " +
                       " 3 as isPokok  ";

                DataTable dtox = new DataTable();
                PerdaIII ox = new PerdaIII();

                dtox = _dbHelper.ExecuteDataTable(SSQL);
                if (dtox  != null)
                {
                    if (dtox.Rows.Count > 0)
                    {

                        DataRow dr = dtox.Rows[0];

                        ox = new PerdaIII()
                        {
                            Tahun = _p.Tahun,
                            Kode = DataFormat.ToKodeRekening(DataFormat.GetInteger(dr["IIDRekening"]), m_ProfileRekening),
                            Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                            Jumlah = DataFormat.GetDecimal(dr["iNo"]) < 2 ? (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport() : "",
                            JumlahMurni = DataFormat.GetInteger(dr["iNo"]) > 0 ? DataFormat.GetInteger(dr["iNo"]).ToString() + "." : "",
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

        // *******************************************************************************************
        public List<PerdaIII> GetPenjabaran2(ParameterLaporan _p)
        {

            string namaView = CreateViewAllLevel(_p, true);

            List<PerdaIII> _lst = new List<PerdaIII>();
            Single _lastLevel = _p.LastLevel;
            try
            {

                if (_lastLevel == 3)
                {

                    SSQL = "Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON left(mDasarHukum.IIDRekening,3) =Left(" + namaView + ".IIDRekening,3) " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root < 4 and " + namaView + ".Jumlah>0   ";

                }
                else
                {
                    SSQL = "Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 as iNo," +
                        " '' as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening =" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root < 5 and " + namaView + ".Jumlah>0   ";

                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                    }

                    SSQL = SSQL + " UNION ALL Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo," +
                        " mDasarHukum.sKeterangan  as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening =" + namaView + ".IIDRekening " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root =5 and " + namaView + ".Jumlah>0   "+
                        " and mdasarHukum.IIDRekening in (select IIDRekening from " + namaView + " where " + namaView + ".IDDinas= " + _p.IDDinas.ToString() + " and mdasarHukum.IIDRekening % 100 >0)";                    
                    
                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                    }
                    SSQL = SSQL + " UNION ALL Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 AS iNo," +
                        " (Select mDasarHukum.sKeterangan from mDasarHukum where iidrekening =  (" + namaView + ".IIDRekening/1000)* 1000) as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening/1000 =" + namaView + ".IIDRekening/1000 " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root =5 and " + namaView + ".Jumlah>0   " +
                        "  and mdasarHukum.IIDRekening % 100 =0";

                        if (_p.IDDinas > 0)
                            {
                                SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                            }

                        SSQL = SSQL + " UNION ALL Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,0 AS iNo," +
                        " '' as Keterangan, 0 as isPokok from " + namaView + "  " +
                        " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 1 and " + namaView + ".Root =5 and " + namaView + ".Jumlah>0   " +
                        "  and (" + namaView + ".IIDRekening/1000) not in (Select IIDRekening/1000 from mDasarHukum)";

                        if (_p.IDDinas > 0)
                        {
                            SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                        }



                 }
             
                SSQL = SSQL + " UNION ALL    ";

                SSQL = SSQL + " Select 2 as btJenis," + namaView + ".IDDInas/10000 as IDUrusan ," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  and " + namaView + ".btJenis in (2,3) and " + namaView + ".Root =1 and " + namaView + ".Jumlah>0    ";

                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " GROUP BY  " + namaView + ".IDDInas," + namaView + ".IIDREKENING,ROOT,sNamaRekening, mDasarHukum.iNo, mDasarHukum.sKeterangan  ";
                SSQL = SSQL + " UNION ALL    ";




                SSQL = SSQL + " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas, 0 AS IDProgram , 0 as IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni,mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan, 0 as isPokok from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis= 2 and " + namaView + ".Root >1  and " + namaView + ".Jumlah>0  and " + namaView + ".Root <=" + _lastLevel.ToString() ;
                if (_p.IDDinas > 0)
                {
                    SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                }
                SSQL = SSQL + " UNION ALL ";
                SSQL = SSQL + " Select " + namaView + ".btJenis, " + namaView + ".IDDInas/10000 as  IDUrusan," + namaView + ".IDDInas, 1 AS IDProgram , 1 AS IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening , SUM(Jumlah) AS Jumlah, SUM(JumlahMurni) AS JumlahMurni ,mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan , 0 as IsPokok  from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + " AND  " + namaView + ".btJEnis= 3 and Root =2  ";
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

                SSQL = SSQL + " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram, A.sNamaProgram,mPelaksanaURusan.IsPokok" +
                " UNION ALL    " +
                " Select 3.btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, A.IDkegiatan  as IDKegiatan,0 as IIDRekening,0 as Root, A.sNama as sNamaRekening,   " +
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



                if (_p.LastLevel == 3)
                {
                    SSQL = SSQL + " UNION ALL  " +
                        " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, Jumlah, JumlahMurni , mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan ,mPelaksanaUrusan.IsPokok  from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                        " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "   AND  " + namaView + ".btJEnis in ( 3) and Root = 3 and (Jumlah  > 0  or JumlahMurni>0)";
                    if (_p.IDDinas > 0)
                    {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                    }

                }
                else
                {

                   SSQL = SSQL + "  UNION ALL  " +
                    " Select " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas,  IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, SUM(Jumlah) AS jUMLAH, SUM(JumlahMurni ) AS jUMLAHmURNI, mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan ,mPelaksanaUrusan.IsPokok  from " + namaView + "  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=" + namaView + ".IIDRekening  " +
                    " INNER JOIN mPElaksanaurusan on " + namaView + ".IDDInas = mPelaksanaUrusan.IDDinas and  " + namaView + ".iTahun = mPelaksanaUrusan.iTahun and " + namaView + ".IDUrusan = mPelaksanaUrusan.IDUrusan  WHERE " + namaView + ".iTAhun = " + _p.Tahun.ToString() + "  AND  " + namaView + ".btJEnis in (3) and Root >2 and Root  <6  and (Jumlah  > 0  or JumlahMurni>0)  ";
                    if (_p.IDDinas > 0)
                   {
                        SSQL = SSQL + " AND " + namaView + ".IDDinas=" + _p.IDDinas.ToString();
                    }
                    SSQL = SSQL + " group by " + namaView + ".btJenis," + namaView + ".IDUrusan," + namaView + ".IDDInas,  " +
                        " IDProgram , IDkegiatan, " + namaView + ".IIDRekening,Root,sNamaRekening, " +
                        " mDasarHukum.iNo,mDasarHukum.sKeterangan  ,mPelaksanaUrusan.IsPokok ";
                
                   
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
                                    Kode =                                           DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                            DataFormat.GetInteger(dr["IDDInas"]),
                                                            DataFormat.GetInteger(dr["IDProgram"]),
                                                            DataFormat.GetInteger(dr["IDkegiatan"]),
                                                            DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) ,
                                    Nama = DataFormat.GetString(dr["sNamaRekening"]).Trim() ,//DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).Trim() : DataFormat.GetString(dr["sNamaRekening"]).Trim()) : "",
                                    Jumlah =  DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport() ,
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"].ToString()).ToRupiahInReport(),
                                     //DataFormat.GetInteger(dr["iNo"]) > 0 ? DataFormat.GetInteger(dr["iNo"]).ToString() + "." : "",
                                    Selisih = (DataFormat.GetDecimal(dr["Jumlah"].ToString()) - DataFormat.GetDecimal(dr["JumlahMurni"].ToString())).ToRupiahInReport (),
                                    Prosentase = DataFormat.GetProsentase (DataFormat.GetDecimal(dr["JumlahMurni"].ToString()) , DataFormat.GetDecimal(dr["Jumlah"].ToString())),
                                    label = DataFormat.GetInteger(dr["IDkegiatan"])>0 && DataFormat.GetInteger(dr["IIDRekening"])==0?"Lokasi :":DataFormat.GetInteger(dr["iNo"]) >0? DataFormat.GetInteger(dr["iNo"]).ToString():"", 
                                    
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
                            //Tahun = _p.Tahun,
                            //Kode = DataFormat.GetInteger(dr["iNo"]) < 2 || dr["iNo"] == null ?
                            //       DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                            //                        DataFormat.GetInteger(dr["IDDInas"]),
                            //                        DataFormat.GetInteger(dr["IDProgram"]),
                            //                        DataFormat.GetInteger(dr["IDkegiatan"]),
                            //                        DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
                            //Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
                            //Jumlah = (DataFormat.GetDecimal(dr["PDPT"]) - DataFormat.GetDecimal(dr["BLJ"])).ToRupiahInReport(),
                            //JumlahMurni = (DataFormat.GetDecimal(dr["PDPTMURNI"]) - DataFormat.GetDecimal(dr["BLJMURNI"])).ToRupiahInReport(),
                            //Selisih = "0",
                            //Prosentase = "0",
                            //Keterangan = DataFormat.GetString(dr["Keterangan"]),
                            //Level = DataFormat.GetSingle(dr["Root"]),
                            //Jenis = DataFormat.GetSingle(dr["btJenis"])

                            
                            Tahun = _p.Tahun,
                            Kode = DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
                                                    DataFormat.GetInteger(dr["IDDInas"]),
                                                    DataFormat.GetInteger(dr["IDProgram"]),
                                                    DataFormat.GetInteger(dr["IDkegiatan"]),
                                                    DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening),
                            Nama = DataFormat.GetString(dr["sNamaRekening"]) ,
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

                // Pembiayaan 
                //if (_p.IDDinas == 4040601){
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




        //public List<PerdaIII> GetPerdaIII_B(ParameterLaporan _p)
        //{

        //    List<PerdaIII> mListUnit = new List<PerdaIII>();
        //    try
        //    {
        //        SSQL = "Select vwAnggaranAllLevel.btJenis,vwAnggaranAllLevel.IDUrusan,vwAnggaranAllLevel.IDDInas, 0 AS IDProgram , 0 as IDkegiatan, vwAnggaranAllLevel.IIDRekening,Root,sNamaRekening,JumlahOlah , Jumlah, JumlahMurni,mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan from vwAnggaranAllLevel  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=vwAnggaranAllLevel.IIDRekening " +
        //        " WHERE vwAnggaranAllLevel.iTAhun = " + _p.Tahun.ToString() + " AND vwAnggaranAllLevel.IDDinas=" + _p.IDDinas.ToString() + " AND  vwAnggaranAllLevel.btJEnis= 1   " +
        //        " UNION ALL    ";

        //        SSQL = SSQL + " Select 2 as btJenis,0 as IDUrusan,vwAnggaranAllLevel.IDDInas, 0 AS IDProgram , 0 as IDkegiatan, vwAnggaranAllLevel.IIDRekening,Root,sNamaRekening,SUM(JumlahOlah) as JumlahOlah , SUM(Jumlah) as Jumlah, SUM(JumlahMurni) as JumlahMurni, mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan from vwAnggaranAllLevel  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=vwAnggaranAllLevel.IIDRekening  " +
        //        " WHERE vwAnggaranAllLevel.iTAhun = " + _p.Tahun.ToString() + " AND vwAnggaranAllLevel.IDDinas=" + _p.IDDinas.ToString() + "  and vwAnggaranAllLevel.btJenis in (2,3) and vwAnggaranAllLevel.Root =1    " +
        //        " GROUP BY vwAnggaranAllLevel.IDDInas,vwAnggaranAllLevel.IIDREKENING,ROOT,sNamaRekening, mDasarHukum.iNo, mDasarHukum.sKeterangan  ";
        //        SSQL = SSQL + " UNION ALL ";

        //        SSQL = SSQL + " Select vwAnggaranAllLevel.btJenis,vwAnggaranAllLevel.IDUrusan,vwAnggaranAllLevel.IDDInas, 0 AS IDProgram , 0 as IDkegiatan, vwAnggaranAllLevel.IIDRekening,Root,sNamaRekening,JumlahOlah , Jumlah, JumlahMurni,mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan from vwAnggaranAllLevel  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=vwAnggaranAllLevel.IIDRekening  " +
        //        " WHERE vwAnggaranAllLevel.iTAhun = " + _p.Tahun.ToString() + " AND vwAnggaranAllLevel.IDDinas=" + _p.IDDinas.ToString() + "  AND  vwAnggaranAllLevel.btJEnis= 2 and vwAnggaranAllLevel.Root >1    ";

        //        SSQL = SSQL + " UNION ALL ";
        //        SSQL = SSQL + " Select vwAnggaranAllLevel.btJenis,0 as IDUrusan,vwAnggaranAllLevel.IDDInas, 0 AS IDProgram , 0 AS IDkegiatan, vwAnggaranAllLevel.IIDRekening,Root,sNamaRekening,SUM(JumlahOlah) AS JumlahOlah , SUM(Jumlah) AS Jumlah, SUM(JumlahMurni) AS JumlahMurni ,mDasarHukum.iNo, mDasarHukum.sKeterangan as Keterangan from vwAnggaranAllLevel  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=vwAnggaranAllLevel.IIDRekening  " +
        //        " WHERE vwAnggaranAllLevel.iTAhun = " + _p.Tahun.ToString() + " AND vwAnggaranAllLevel.IDDinas=" + _p.IDDinas.ToString() + "  AND  vwAnggaranAllLevel.btJEnis= 3 and Root =2  " +
        //        " GROUP BY vwAnggaranAllLevel.btJenis,vwAnggaranAllLevel.IDDInas, vwAnggaranAllLevel.IIDRekening,Root,sNamaRekening,mDasarHukum.iNo,mDasarHukum.sKeterangan ";
        //        SSQL = SSQL + "  UNION ALL  ";

        //        SSQL = SSQL + "  Select 3 as btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, 0 as IDKegiatan,0 as IIDRekening,0 as Root, A.sNamaProgram as sNamaRekening,    " +
        //        " SUM(B.cPlafon) as JumlahOlah, SUM(B.cJumlah) as Jumlah, SUM(B.cJumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan FROM tPrograms_A A   " +
        //        " INNER JOIN tAnggaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan    " +
        //        " AND A.IDProgram = B.IDProgram   " +
        //        " WHERE A.iTAhun = " + _p.Tahun.ToString() + " AND A.IDDinas=" + _p.IDDinas.ToString() + " AND   B.btJEnis= 3  AND b.cJumlah>0   " +
        //        " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram, A.sNamaProgram" +
        //        " UNION ALL    " +
        //        " Select 3.btJenis,A.IDUrusan, A.IDDInas,A.IDProgram, A.IDkegiatan  as IDKegiatan,0 as IIDRekening,0 as Root, A.sNama2 as sNamaRekening,   " +
        //        " SUM(B.cPlafon) as JumlahOlah, SUM(B.cJumlah) as Jumlah, SUM(B.cJumlahMurni) as JumlahMurni,0 as iNo,'' as Keterangan FROM tKegiatan_A A   " +
        //        " INNER JOIN tAnggaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDinas = B.IDDInas AND A.IDUrusan = B.IDUrusan    " +
        //        " AND A.IDProgram = B.IDProgram and A.IDkegiatan=B.IDKegiatan   " +
        //        " WHERE B.iTAhun = " + _p.Tahun.ToString() + " AND B.IDDinas=" + _p.IDDinas.ToString() + " AND   B.btJEnis= 3  AND b.cJumlah>0 " +
        //        " GROUP BY A.IDUrusan, A.IDDInas,A.IDProgram, A.sNama2,A.IDkegiatan  " +
        //        " UNION ALL  " +
        //        " Select vwAnggaranAllLevel.btJenis,vwAnggaranAllLevel.IDUrusan,vwAnggaranAllLevel.IDDInas,  IDProgram , IDkegiatan, vwAnggaranAllLevel.IIDRekening,Root,sNamaRekening,JumlahOlah , Jumlah, JumlahMurni , mDasarHukum.iNo,mDasarHukum.sKeterangan as Keterangan from vwAnggaranAllLevel  LEFT OUTER JOIN mDasarHukum ON mDasarHukum.IIDRekening=vwAnggaranAllLevel.IIDRekening  " +
        //        " WHERE vwAnggaranAllLevel.iTAhun = " + _p.Tahun.ToString() + " AND vwAnggaranAllLevel.IDDinas=" + _p.IDDinas.ToString() + "  AND  vwAnggaranAllLevel.btJEnis= 3 and Root =5 and Jumlah>0  " +
        //        " UNION ALL  " +
        //        " Select distinct 4 as btJenis,0 as IDUrusan,0 as IDDInas,  0 as IDProgram , 0 as IDkegiatan, 0 as IIDRekening,6 as Root,'SURPLUS/DEFISIT',dbo.SurplusDefisit(vwAnggaranAllLevel.iTahun,vwAnggaranAllLevel.IDDInas)  as JumlahOlah , dbo.SurplusDefisit(vwAnggaranAllLevel.iTahun,vwAnggaranAllLevel.IDDInas) as Jumlah, dbo.SurplusDefisit(vwAnggaranAllLevel.iTahun,vwAnggaranAllLevel.IDDInas) as  JumlahMurni ,0 as iNo, '' as Keterangan from vwAnggaranAllLevel  " +
        //        " WHERE vwAnggaranAllLevel.iTAhun = " + _p.Tahun.ToString() + " AND vwAnggaranAllLevel.IDDinas=" + _p.IDDinas.ToString() +
        //        " order by btJenis,IDUrusan,IDDInas, IDProgram,IDkegiatan, IIDRekening,Root,iNo  ";



        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                if (_p.Tahap == 0 || _p.Tahap == 2)
        //                {
        //                    mListUnit = (from DataRow dr in dt.Rows
        //                            select new PerdaIII()
        //                            {

        //                                Tahun = _p.Tahun,
        //                                Kode = DataFormat.GetInteger(dr["iNo"]) < 2 ?
        //                                     DataFormat.GetKode(DataFormat.GetInteger(dr["IDUrusan"]),
        //                                                        DataFormat.GetInteger(dr["IDDInas"]),
        //                                                        DataFormat.GetInteger(dr["IDProgram"]),
        //                                                        DataFormat.GetInteger(dr["IDkegiatan"]),
        //                                                        DataFormat.GetInteger(dr["IIDRekening"]), DataFormat.GetInteger(dr["btJenis"]), m_ProfileProgKegiatan, m_ProfileRekening) : "",
        //                                Nama = DataFormat.GetInteger(dr["iNo"]) < 2 ? (DataFormat.GetSingle(dr["Root"]) < 2 ? DataFormat.GetString(dr["sNamaRekening"]).ToUpper() : DataFormat.GetString(dr["sNamaRekening"])) : "",
        //                                Jumlah = DataFormat.GetDecimal(dr["iNo"]) < 2 ? DataFormat.GetDecimal(dr["Jumlah"].ToString()).ToRupiahInReport() : "",
        //                                JumlahMurni = DataFormat.GetInteger(dr["iNo"]) > 0 ? DataFormat.GetInteger(dr["iNo"]).ToString() + "." : "",
        //                                Selisih = "0",
        //                                Prosentase = "0",
        //                                Keterangan = DataFormat.GetString(dr["Keterangan"]),
        //                                Level = DataFormat.GetSingle(dr["Root"]),
        //                                Jenis = DataFormat.GetSingle(dr["btJenis"])

        //                            }).ToList();
        //                }
        //            }
        //        }
        //        return mListUnit;
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message;
        //        return mListUnit;
        //    }



        //}

//
// *********RINGKSAN PERDA***********
//

        public  void BersihkanNonKegiatan()
        {
            //SSQL = "DROP TABLE temppendapatan";
            //_dbHelper.ExecuteNonQuery(SSQL);
            SSQL = " IF OBJECT_ID('temppendapatan') IS NOT NULL     DROP TABLE temppendapatan";
            _dbHelper.ExecuteNonQuery(SSQL);


            SSQL = "select distinct IDDinas into temppendapatan FROM tAnggaranRekening_A WHERE iTAhun = " + Tahun.ToString() + " and IIDRekening like '4%'";
            _dbHelper.ExecuteNonQuery(SSQL) ;

            SSQL = "DELETE from tKegiatan_A  WHERE btJenis= 1";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = " INSERT  into tKegiatan_A (iTahun, sNama,btJenis, dtPembahasan,IDDInas, IDUrusan, IDProgram, IDkegiatan) SELECT  " + Tahun.ToString() + "  as iTahun,'Pendapatn',1,'10/31/" + Tahun.ToString() + "', IDDInas, IDDinas/10000 as IDUrusan, 0 as IDProgram, 0 as IDKegiatan from temppendapatan  ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = " IF OBJECT_ID('temppendapatan') IS NOT NULL     DROP TABLE temppendapatan";
            _dbHelper.ExecuteNonQuery(SSQL);

            //SSQL = "DROP TABLE temppendapatan";
            //_dbHelper.ExecuteNonQuery(SSQL);


            SSQL="DELETE from tKegiatan_A  WHERE btJenis= 2";
            _dbHelper.ExecuteNonQuery (SSQL);

            SSQL = " INSERT  into tKegiatan_A (iTahun, sNama,btJenis, dtPembahasan,IDDInas, IDUrusan, IDProgram, IDkegiatan) SELECT  " + Tahun.ToString() + "  as iTahun,'Belanja Tidak Langsung',2,'10/31/" + Tahun.ToString() + "',ID as IDDInas, ID/10000 as IDUrusan, 0 as IDProgram, 0 as IDKegiatan from mSKPD where root =1 ";
            _dbHelper.ExecuteNonQuery(SSQL);


            SSQL = " IF OBJECT_ID('temppendapatan') IS NOT NULL     DROP TABLE temppendapatan";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "select distinct IDDinas into temppendapatan FROM tAnggaranRekening_A WHERE iTAhun = " + Tahun.ToString() + "  and IIDRekening like '61%'";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "DELETE from tKegiatan_A  WHERE btJenis = 4";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = " INSERT  into tKegiatan_A (iTahun, sNama,btJenis, dtPembahasan,IDDInas, IDUrusan, IDProgram, IDkegiatan) SELECT  " + Tahun.ToString() + "  as iTahun,'Penerimaan Pembiayaan',4,'10/31/" + Tahun.ToString() + "', IDDInas, IDDinas/10000 as IDUrusan, 0 as IDProgram, 0 as IDKegiatan from temppendapatan  ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = " IF OBJECT_ID('temppendapatan') IS NOT NULL     DROP TABLE temppendapatan";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "select distinct IDDinas into temppendapatan FROM tAnggaranRekening_A WHERE iTAhun = " + Tahun.ToString() + "  and IIDRekening like '62%'";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "DELETE from tKegiatan_A  WHERE btJenis = 5";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = " INSERT  into tKegiatan_A (iTahun, sNama,btJenis, dtPembahasan,IDDInas, IDUrusan, IDProgram, IDkegiatan) SELECT  " + Tahun.ToString() + "  as iTahun,'Pengeluaran Pembiayaan',5,'10/31/" + Tahun.ToString() + "', IDDInas, IDDinas/10000 as IDUrusan, 0 as IDProgram, 0 as IDKegiatan from temppendapatan  ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = " IF OBJECT_ID('temppendapatan') IS NOT NULL     DROP TABLE temppendapatan";
            _dbHelper.ExecuteNonQuery(SSQL);

        

        }
        private void CreateViewRealisasi(DateTime tanggal)
        {
            

        }
        public void UpdateRealisasi(DateTime d)
        {


            CreateViewRealisasiPerRekening();


            SSQL = " Update tAnggaranRekening_A SET cRealisasi =0 where iTahun  =" + Tahun.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);
         
            SSQL = "update tAnggaranRekening_A set cRealisasi = (select SUM (VR.DEBET * VR.cJumlah) from  Realisasi04Perda VR " +
                  " where iTahun= tAnggaranRekening_A.iTahun and  VR.IDDInas = tAnggaranRekening_A.IDDINAS and  " +
                  " VR.IDURUSAN= tAnggaranRekening_A.IDURUSAN and  " +
                  " VR.IDPROGRAM = tAnggaranRekening_A.IDPROGRAM and  " +
                  " VR.IDKegiatan = tAnggaranRekening_A.IDKegiatan and  " +
                  " VR.IIDRekening = tAnggaranRekening_A.IIDRekening and VR.dtBukukas <=" + d.ToSQLFormat() + " and iTahun =" + Tahun.ToString() + ") where btJenis = 3";


            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "update tAnggaranRekening_A set cRealisasi = (select SUM (VR.DEBET * VR.cJumlah) from Realisasi04Perda VR " +
                  " where iTahun= tAnggaranRekening_A.iTahun and  VR.IDDInas = tAnggaranRekening_A.IDDINAS and  " +
                  " VR.IDURUSAN= tAnggaranRekening_A.IDURUSAN and  " +
                  " VR.IDPROGRAM = 0 and  " +
                  " VR.IDKegiatan = 0 and  " +
                  " VR.IIDRekening = tAnggaranRekening_A.IIDRekening and VR.dtBukukas <=" + d.ToSQLFormat() + " and iTahun =" + Tahun.ToString() + ") where btJenis = 2";


            _dbHelper.ExecuteNonQuery(SSQL);


            SSQL = "update tAnggaranRekening_A set cRealisasi = (select SUM (VR.DEBET * VR.cJumlah) from Realisasi04Perda VR " +
                    " where iTahun= tAnggaranRekening_A.iTahun and  VR.IDDINAS = tAnggaranRekening_A.IDDINAS and  " +
                    " VR.IDPROGRAM = 0 and  " +
                    " VR.IDKegiatan = 0 and  " +
                    " VR.IIDRekening = tAnggaranRekening_A.IIDRekening and VR.dtBukukas <=" + d.ToSQLFormat() + " and iTahun =" + Tahun.ToString() + ") where btJenis in (5)";

            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "update tAnggaranRekening_A set cRealisasi = (select SUM ( VR.cJumlah) from RealisasiSTS VR " +
                    " where iTahun= tAnggaranRekening_A.iTahun and  VR.btKodekategori = tAnggaranRekening_A.btKodekategori and  " +
                    " VR.btKodeUrusan = tAnggaranRekening_A.btKodeUrusan and  " +
                    " VR.btKodeSKPD = tAnggaranRekening_A.btKodeSKPD and  " +
                    " VR.btKodeUK = tAnggaranRekening_A.btKodeUK and  " +
                    " VR.IIDRekening = tAnggaranRekening_A.IIDRekening and VR.dtBukukas <=" + d.ToSQLFormat() + " and iTahun =" + Tahun.ToString() + ")  where btJenis in (1,4)";

            _dbHelper.ExecuteNonQuery(SSQL);
            //HapusView("Realisasi04Perda");



        }
        //rptPerdaLogic oLogic = new rptPerdaLogic(GlobalVar.TahunAnggaran);
        public bool PerbaikiRealisasiBL(int _pIDDInas)
        {

            //SSQL = " Update tAnggaranRekening_A SET cRealisasi =  0 where btJenis = 3 AND IDDInas = " + _pIDDInas.ToString();

            // //   _dbHelper.ExecuteNonQuery(SSQL);




            //SSQL = " Update tAnggaranRekening_A SET cRealisasi =  RealiasiPerRekening.Realisasi  from tAnggaranRekening_A INNER JOIN RealiasiPerRekening ON tAnggaranRekening_A.iTahun = RealiasiPerRekening.iTahun and  " +
            //         " tAnggaranRekening_A.btKodekategori = RealiasiPerRekening.btKodekategori and  tAnggaranRekening_A.btKodeurusan = RealiasiPerRekening.btKodeurusan and " +
            //         " tAnggaranRekening_A.btKodekategoriPelaksana = RealiasiPerRekening.btKodekategoriPelaksana and  tAnggaranRekening_A.btKodeurusanPelaksana = RealiasiPerRekening.btKodeurusanPelaksana  " +
            //         " and tAnggaranRekening_A.btKodeskpd = RealiasiPerRekening.btKodeskpd    " +
            //        " and tAnggaranRekening_A.btKodeUK = RealiasiPerRekening.btKodeUK and tAnggaranRekening_A.btIDprogram = RealiasiPerRekening.btIDprogram and tAnggaranRekening_A.btIDkegiatan = RealiasiPerRekening.btIDkegiatan and tAnggaranRekening_A.IIDRekening = RealiasiPerRekening.IIDRekening  and btJenis=3 where taNGGARANrEKENING_a.iddINAS = " +  _pIDDInas.ToString() ;

            // // _dbHelper.ExecuteNonQuery(SSQL);
              return true;


        }
        public  bool CreateViewRealisasiPerRekening()
        {
            HapusView("Realisasi04Perda");


            SSQL = " CREATE VIEW [dbo].[Realisasi04Perda] AS   select 1 as TABEL, A.inourut, A.btJenis, " +
 " A.btKodeKategori * 1000000 + A.btKodeUrusan * 10000 + A.btKodeSKPD * 100 as IDDInas, " +
 " (B.btKodeKategoriPelaksana * 10000 + B.btKodeUrusanPelaksana * 100 + B.btIDProgram )  AS idpROGRAM," +
 " (B.btKodeKategoriPelaksana * 100 + B.btKodeUrusanPelaksana )  AS iduRUSAN, " +
 " B.btKodeKategoriPelaksana * 10000000 + B.btKodeUrusanPelaksana * 100000 + B.btIDProgram * 1000 + " +
 " B.btIDKegiatan as IDKegiatan, " +
   " btJenis as JenisSP2d, A.iTahun,A.btKodekategori,  A.dtSPP as dtDocument," +
     "        A.dtBukukas,A.btKodeUrusan,A.btKodeSKPD,B.btKodeUK,B.btKodeKategoriPelaksana,  " +
   " B.btKodeURusanPelaksana, B.btIDProgram,B.btIDKegiatan,A.btIDSUBKegiatan,B.IIDRekening,  " +
   " 1 as DEBET, b.cJumlah, A.bPPKD FROM tSPP A INNER join tSPPRekening B ON a.inourut=B.inourut " +
   " Where a.btJenis =3 And a.iStatus = 4   ";
            SSQL = SSQL + " UNION ALL select 1 as TABEL, A.inourut, A.btJenis, " +
 " A.btKodeKategori * 1000000 + A.btKodeUrusan * 10000 + A.btKodeSKPD * 100 as IDDInas, " +
 " 0 AS idpROGRAM," +
 " (A.btKodeKategori * 100 + A.btKodeUrusan )  AS iduRUSAN, " +
 " 0 as IDKegiatan, " +
   " btJenis as JenisSP2d, A.iTahun,A.btKodekategori,  A.dtSPP as dtDocument," +
     "        A.dtBukukas,A.btKodeUrusan,A.btKodeSKPD,B.btKodeUK,B.btKodeKategoriPelaksana,  " +
   " B.btKodeURusanPelaksana, B.btIDProgram,B.btIDKegiatan,A.btIDSUBKegiatan,B.IIDRekening,  " +
   " 1 as DEBET, b.cJumlah, A.bPPKD FROM tSPP A INNER join tSPPRekening B ON a.inourut=B.inourut " +
   " Where a.btJenis  in (4,5)  And a.iStatus = 4   " + 

   " Union ALL select 2 as TABEL, A.inourut,A.btJenis," + 
   " btKodeKategori * 1000000 + btKodeUrusan * 10000 + btKodeSKPD * 100 as IDDInas, " + 
 " case when bppkd=1 then 0 else (btKodeKategoriPelaksana * 10000 + btKodeUrusanPelaksana * 100 + btIDProgram ) end AS idpROGRAM," + 
"  (btKodeKategoriPelaksana * 100 + btKodeUrusanPelaksana )  AS iduRUSAN," +
 " case when bppkd=1 then 0 else ( btKodeKategoriPelaksana * 10000000 + btKodeUrusanPelaksana * 100000 + btIDProgram * 1000 + btIDKegiatan) End as IDKegiatan," + 
 " btJenisBelanja as JenisSP2D,   A.iTahun,A.btKodekategori, A.dtBukukas as dtDocument, A.dtBukukas,A.btKodeUrusan,A.btKodeSKPD,   " + 
 " A.btKodeUK,A.btKodeKategoriPelaksana,A.btKodeURusanPelaksana, A.btIDProgram,A.btIDKegiatan,   " + 
 " A.btIDSUBKegiatan,B.IIDRekening,1 as DEBET, b.cJumlah ,bPPKD FROM tPanjar A INNER join   " + 
 " tPanjarRekening B ON a.inourut=B.inourut Where btJenisBelanja in (1,2) " + 
 "  Union ALL select 2 as TABEL,A.inourut,a.iJenisSumber  as btJenis," + 
 " A.btKodeKategori * 1000000 + btKodeUrusan * 10000 + btKodeSKPD * 100 as IDDInas, " + 
 " (B.btKodeKategoriPelaksana1 * 10000 + B.btKodeUrusanPelaksana1 * 100 + B.btIDProgram1 )  AS idpROGRAM," + 
 " (B.btKodeKategoriPelaksana1 * 100 + B.btKodeUrusanPelaksana1 )  AS iduRUSAN," + 
 " btKodeKategoriPelaksana1 * 10000000 + btKodeUrusanPelaksana1 * 100000 + btIDProgram1 * 1000 + btIDKegiatan1 as IDKegiatan,1 as JenisSP2D, A.iTahun,A.btKodekategori, " + 
 " A.dtKoreksi as dtDocument,A.dtKoreksi as dtBukukas,A.btKodeUrusan,A.btKodeSKPD,B.btKodeUK1,B.btKodeKategoriPelaksana1,B.btKodeURusanPelaksana1," + 
 " B.btIDProgram1,B.btIDKegiatan1, 0 as btIDSUBKegiatan,B.IIDRekening1, -1* b.iDebet1 as DEBET, " + 
 " b.cJumlah1 ,0 as bPPKD  FROM tKoreksi A INNER join tKoreksiDetail B ON a.inourut=B.inourut " + 
 " where A.iJenisSumber <3  " +
  " Union all  select 5 as TABEL, A.inourut, A.btJenis,btKodeKategori * 1000000 + btKodeUrusan * 10000 + btKodeSKPD * 100 as IDDInas, " +
 "  case when btIDProgram=0 then 0 else  (A.btKodeKategoriPelaksana * 10000 + a.btKodeUrusanPelaksana * 100 + a.btIDProgram ) END  AS idpROGRAM," + 
 " (A.btKodeKategoriPelaksana * 100 + A.btKodeUrusanPelaksana )  AS iduRUSAN," +
"   case when btIDKegiatan =0 then 0 else  btKodeKategoriPelaksana * 10000000 + btKodeUrusanPelaksana * 100000 + btIDProgram * 1000 + btIDKegiatan END  as IDKegiatan,btJenisSP2d as JenisSP2D, " + 
 " A.iTahun,A.btKodekategori, A.dtBukukas as dtDocument, A.dtBukukas,A.btKodeUrusan, A.btKodeSKPD,A.btKodeUK," + 
 " A.btKodeKategoriPelaksana,A.btKodeURusanPelaksana,A.btIDProgram, A.btIDKegiatan,A.btIDSUBKegiatan, B.IIDRekening,-1 " +
 " as DEBET, b.cJumlah,bPPKD as bPPKD  FROM tsetor A INNER join tsetorRekening B ON a.inourut=B.inourut " + 
 " WHERE A.btJenis=3  ";


            _dbHelper.ExecuteNonQuery(SSQL);
            return true;

        }
        public List<RingkasanPerda> GetRingkasanPerda(int _iTahun, Single _iTahap, ParameterLaporan _p)
        {

            List<RingkasanPerda> _lsttemp = new List<RingkasanPerda>();
            List<RingkasanPerda> _lst = new List<RingkasanPerda>();
            try
            {
               

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


               // CreateViewAllLevel(_p,false);

                List<RingkasanPerda> lstPembiayaan = new List<RingkasanPerda>();

                lstPembiayaan = GetRingkasanPerdaPembiayaan(_iTahun, _iTahap, _p, sNamaView);
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
        public List<RingkasanPerda> GetRingkasanPerda64(int _iTahun, Single _iTahap, ParameterLaporan _p)
        {

            List<RingkasanPerda> _lsttemp = new List<RingkasanPerda>();
            List<RingkasanPerda> _lst = new List<RingkasanPerda>();
            try
            {
                //BersihkanNonKegiatan();

                GetKolom(_p.Tahap);
                //string sNamaView = CreateViewAllLevel64(_p, true);
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

                //if (_p.LastLevel == 5)
                //{
                //    b = 5;
                //    SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2)  and Root=4  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                //    _lstBelanjaLevel4 = GetBagianRingkasanPerda(SSQL);

                //    foreach (RingkasanPerda p in _lstBelanjaLevel4)
                //    {
                //        mListUnit.Add(p);
                //    }
                //    b = 6;
                //    SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2)  and Root=5  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                //    _lstBelanjaLevel5 = GetBagianRingkasanPerda(SSQL);
                //    foreach (RingkasanPerda p in _lstBelanjaLevel5)
                //    {
                //        mListUnit.Add(p);
                //    }
                //}

                //b = 7;

                //b = 8;
                //SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =3  and Root=2  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                //_lstBelanjaLevel2 = GetBagianRingkasanPerda(SSQL);

                //foreach (RingkasanPerda p in _lstBelanjaLevel2)
                //{
                //    mListUnit.Add(p);
                //}

                //b = 9;
                //SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =3  and Root=3  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";

                //_lstBelanjaLevel3 = GetBagianRingkasanPerda(SSQL);
                //foreach (RingkasanPerda p in _lstBelanjaLevel3)
                //{
                //    mListUnit.Add(p);
                //}
                //if (_p.LastLevel == 5)
                //{
                //    b = 10;
                //    SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =3  and Root=4  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                //    _lstBelanjaLevel4 = GetBagianRingkasanPerda(SSQL);
                //    foreach (RingkasanPerda p in _lstBelanjaLevel4)
                //    {
                //        mListUnit.Add(p);
                //    }
                //    b = 11;
                //    SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =3  and Root=5  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                //    _lstBelanjaLevel5 = GetBagianRingkasanPerda(SSQL);
                //    foreach (RingkasanPerda p in _lstBelanjaLevel5)
                //    {
                //        mListUnit.Add(p);
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

                lstPembiayaan = GetRingkasanPerdaPembiayaan(_iTahun, _iTahap, _p,sNamaView);
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
        public List<RingkasanPerda> GetRingkasanPerdaPembiayaan(int _iTahun, Single _iTahap, ParameterLaporan _p, string pNamaView )
        {

            List<RingkasanPerda> _lsttemp = new List<RingkasanPerda>();
            List<RingkasanPerda> _lst = new List<RingkasanPerda>();
            try
            {
              // BersihkanNonKegiatan();

                GetKolom(_p.Tahap);
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
                    SSQL = "SELECT 1 as Kelompok,0 as Level, Root,IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun=  " + _iTahun.ToString() + "  AND  btJEnis= 4 and Root<4  AND IIDrekening >= 6100000 GROUP BY Root,IIDrekening, sNamaRekening ";
                else
                    SSQL = "SELECT 1 as Kelompok,0 as Level, Root,IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun=  " + _iTahun.ToString() + "  AND  btJEnis= 4 and Root<6  AND IIDrekening >= 6100000 GROUP BY Root,IIDrekening, sNamaRekening ";
                
                _lstPendapatan = GetBagianRingkasanPerda(SSQL);
                _lst = _lstPendapatan;
                b = 1;
                //SSQL = " SELECT 2 as Kelompok,1 as Level,6  as Root, 0 as IIDRekening, 'JUMLAH PENDAPATAN' as sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis= 1  and Root=1 AND IIDrekening > 4000000  ";
                //_JmlPendapatan = GetBagianRingkasanPerda(SSQL);
                //foreach (RingkasanPerda p in _JmlPendapatan)
                //{
                 //   mListUnit.Add(p);
               // }
                b = 2;
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =5  and Root =1  GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel1 = GetBagianRingkasanPerda(SSQL);
                foreach (RingkasanPerda p in _lstBelanjaLevel1)
                {
                    _lst.Add(p);
                }
                b = 3;
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =5  and Root=2  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel2 = GetBagianRingkasanPerda(SSQL);

                foreach (RingkasanPerda p in _lstBelanjaLevel2)
                {
                    _lst.Add(p);
                }
                b = 4;
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =5  and Root=3  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";

                _lstBelanjaLevel3 = GetBagianRingkasanPerda(SSQL);
                foreach (RingkasanPerda p in _lstBelanjaLevel3)
                {
                    _lst.Add(p);
                }

                if (_p.LastLevel == 5)
                {
                    b = 5;
                    SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =5  and Root=4  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel4 = GetBagianRingkasanPerda(SSQL);

                    foreach (RingkasanPerda p in _lstBelanjaLevel4)
                    {
                        _lst.Add(p);
                    }
                    b = 6;
                    SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + namaKolomdiView1 + ") as JumlahMurni ,SUM(" + namaKolomdiView2 + ") as Jumlah  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =5  and Root=5  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel5 = GetBagianRingkasanPerda(SSQL);
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


                
                _JmlBelanjaLangsung = GetBagianRingkasanPerda(SSQL);
                foreach (RingkasanPerda p in _JmlBelanjaLangsung)
                {
                    _lst.Add(p);
                }

                SSQL = " SELECT 5 as Kelompok,1 as Level,1 as Root, 0 as IIDRekening, 'SISA LEBIH PEMBIAYAAN ANGGARAN TAHUN BERKENAN' as sNamaRekening," +
                   " (SELECT SUM(" + namaKolomdiView1 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1,4) and Root=1 ) - " +
                   " (SELECT SUM(" + namaKolomdiView1 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3,5) and Root=1 )  as JumlahMurni, " +
                   " (SELECT SUM(" + namaKolomdiView2 + ")  from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (1,4) and Root=1 ) - " +
                   " (SELECT SUM(" + namaKolomdiView2 + ") from " + sNamaView + " WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3,5) and Root=1 )  as Jumlah";



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
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    Root = DataFormat.GetInteger(dr["Root"]),
                                    Level = DataFormat.GetInteger(dr["Level"]),
                                    Kelompok = DataFormat.GetInteger(dr["Kelompok"]),
                                    Kode = DataFormat.GetLong(dr["IIDRekening"]).ToKodeRekening(m_ProfileRekening),
                                    Nama = DataFormat.GetString(dr["sNamaRekening"]),
                                    SJumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]).ToRupiahInReport(),
                                    SJumlah = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),

                                    Selisih = (DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["JumlahMurni"])).ToRupiahInReport(),
                                    Persen= DataFormat.GetProsentaseRealisasi (DataFormat.GetDecimal(dr["Jumlah"]) , DataFormat.GetDecimal(dr["JumlahMurni"]))

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

    }
}
