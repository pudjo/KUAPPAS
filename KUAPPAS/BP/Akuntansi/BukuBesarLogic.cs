using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP;
using DataAccess;
using System.Data;
using Formatting;
using DTO.Akuntansi;
namespace BP.Akuntansi
{
    public class BukuBesarLogic : BP
    {
        private long CON_IDREKENING_SURPLUSDEEFISIT =310102010001;
        private long CON_IDREKENING_EQUITESAWAL = 310101010001;

        public BukuBesarLogic(int tahun)
            : base(tahun)
        {

        }
        public List<BukuBesar> GetBukuBesar(int iddinas, DateTime tanggal, long idrekening)
        {
            List<BukuBesar> _lst = new List<BukuBesar>();
            try
            {

                DBParameterCollection paramCollection = new DBParameterCollection();


                SSQL = "SELECT tBukubesar.* , mRekening.IDebet  as saldonormal FROM tBukubesar "+
                    "  inner join mRekening on mRekening.IIDrekening = tBukuBesar.IIDrekening WHERE tBukubesar.dtTransaksi<= @TANGGAL and tBukubesar.iidrekening = @IDREKENING ";//

                if (iddinas > 0)
                {
                    SSQL = SSQL + " AND  IDDInas =@DINAS  ";
                    paramCollection.Add(new DBParameter("@DINAS", iddinas));
                }


                paramCollection.Add(new DBParameter("@TANGGAL", tanggal, DbType.Date));
                paramCollection.Add(new DBParameter("@IDREKENING", idrekening));

                SSQL = SSQL + " order BY tBukubesar.dtTransaksi  , inoJurnal  ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new BukuBesar()
                                {

                                    IDRekening = DataFormat.GetLong(dr["IIDrekening"]),
                                    Debet = DataFormat.GetInteger(dr["iDebet"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    NoBukti = DataFormat.GetString(dr["sNobukti"]),
                                    Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                    TanggalTransaksi = DataFormat.GetDate(dr["dtTransaksi"]),
                                    NoSumber = DataFormat.GetLong(dr["iSumber"]),
                                    SaldoNormal = DataFormat.GetInteger(dr["SaldoNormal"]),

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
        public List<BukuBesar> GetSKPDBukuBesar(int iddinas, DateTime tanggal, string idrekening)
        {
            List<BukuBesar> _lst = new List<BukuBesar>();
            try
            {

                DBParameterCollection paramCollection = new DBParameterCollection();


                SSQL = "SELECT tBukubesar.*, mSKPD.sNamaSKPD, mRekening.IDebet  as saldonormal  FROM tBukubesar "+
                    " inner join mSKPD on mSKPD.ID= tBUKUBESAR.IDDINas  inner join mRekening on mRekening.IIDrekening = tBukuBesar.IIDrekening WHERE "+
                    "tBukubesar.dtTransaksi<= " + tanggal.ToSQLFormat() + " and tBukubesar.iidrekening like '" + idrekening.Replace(".","")  + "%' ";//

                if (iddinas > 0)
                {
                    SSQL = SSQL + " AND  IDDInas =@DINAS  ";
                    paramCollection.Add(new DBParameter("@DINAS", iddinas));
                }


                
                SSQL = SSQL + " order BY tBukubesar.dtTransaksi  , inoJurnal  ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new BukuBesar()
                                {

                                    IDRekening = DataFormat.GetLong(dr["IIDrekening"]),
                                    Debet = DataFormat.GetInteger(dr["iDebet"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    NoBukti = DataFormat.GetString(dr["sNobukti"]),
                                    Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                    TanggalTransaksi = DataFormat.GetDate(dr["dtTransaksi"]),
                                    NoSumber = DataFormat.GetLong(dr["iSumber"]),
                                    NamaSKPD = DataFormat.GetString(dr["sNamaSKPD"]),
                                    SaldoNormal = DataFormat.GetInteger(dr["SaldoNormal"]),

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
        public bool Bersihkan1_dan2Lama(int SKPD, int PPKD)
        {
            try
            {
                _isError = false;

                //If chkGabunganPPKDdanBukan.Value = vbUnchecked Then
                if (PPKD != -1)
                {
                    if (PPKD == 1)
                    {

                        SSQL = "DELETE FROM tBukubesar where inojurnal in (1,2) and year(dtTransaksi) =" + Tahun.ToString();
                        SSQL = SSQL + " and bPPKD = 1 ";
                    }
                    else
                    {
                        SSQL = "DELETE FROM tBukubesar where inojurnal in (1,2) and year(dtTransaksi) =" + Tahun.ToString();
                        SSQL = SSQL + " AND IDDINAS= " + SKPD.ToString() + "  and bPPKD = 0 ";
                    }

                }
                else
                {

                    SSQL = "DELETE FROM tBukubesar where inojurnal in (1,2) and year(dtTransaksi) =" + Tahun.ToString();
                    // SSQL =  SSQL + " AND (bPPKD = 1 or ( 1>0 " & sKetDinas & "  )) "
                }
                _dbHelper.ExecuteNonQuery(SSQL);
                return true;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }

        }
        public bool Bersihkan1_dan2(int SKPD, int PPKD, string sKetDinas )
        {
            // ppkd = -1  => chkGabunganPPKDDanBukan di check
            
            try
            {
                _isError = false;

                //If chkGabunganPPKDdanBukan.Value = vbUnchecked Then
                if (PPKD !=-1 )
                {


                    if (SKPD == 0)
                    {
                        SSQL = "DELETE FROM tBukubesar where inojurnal in (1,2) and year(dtTransaksi) =" + Tahun.ToString();
                       // SSQL = SSQL + " AND IDDINAS= " + SKPD.ToString() + "  and bPPKD = 0 ";

                    }
                    else
                    {
                        if (PPKD == 1)
                        {

                            SSQL = "DELETE FROM tBukubesar where inojurnal in (1,2) and year(dtTransaksi) =" + Tahun.ToString();
                            SSQL = SSQL + " and bPPKD = 1 ";
                        }
                        else
                        {
                            SSQL = "DELETE FROM tBukubesar where inojurnal in (1,2) and year(dtTransaksi) =" + Tahun.ToString();
                            SSQL = SSQL + " AND IDDINAS= " + SKPD.ToString() + "  and bPPKD = 0 ";
                        }
                    }

                }
                else
                {

                    SSQL = "DELETE FROM tBukubesar where inojurnal in (1,2) and year(dtTransaksi) =" + Tahun.ToString() +
                         " AND (bPPKD = 1 or ( 1>0 " + sKetDinas + "  )) ";
                }

                _dbHelper.ExecuteNonQuery(SSQL);
                return true;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }

        }
        private string KetKeteranganDias(int SKPD, int ppkd)
        {
            string sRet="";
            if (ppkd == -1)
            {
                sRet = " ";
                sRet = sRet + " AND (( bPPKD = 0 and iddinas= 5020200)" +
                               " or  (bPPKD = 1))";

            }
            else
            {
                if (SKPD > 0)
                {
                    sRet = " ";
                    sRet = sRet + " AND iddinas= " + SKPD.ToString() + " ";
                    sRet = sRet + " AND bPPKD= " + ppkd.ToString();
                }
                else
                {
                    sRet = " ";
                  
                    sRet = sRet + " AND bPPKD= " + ppkd.ToString();
                }
            }

            return sRet;
        }

        private string KetKeteranganDiasEkuitasAwal(int SKPD, int ppkd)
        {
            string sRet = "";
            if (ppkd == -1)
            {
                sRet = " ";
                sRet = sRet + " AND (( iddinas= " + SKPD.ToString() + ")" +
                               " or  (bPPKD= 1))";

            }
            else
            {
                if (SKPD > 0)
                {
                    sRet = " ";
                    sRet = sRet + " AND iddinas= " + SKPD.ToString() + " ";
                
                }
                else
                {
                    sRet = " ";

                 
                }
            }

            return sRet;
        }
        private decimal GetSurplusdefisit(int SKPD, int ppkd,
            DateTime tanggalAwal, DateTime tanggalAkhir)
        {
            try
            {
                string SSQL = "";
                string sKetDinas =   KetKeteranganDias(SKPD,  ppkd);

                //If chkSemuaDinas.Value = vbUnchecked Then
                //    sKetDinas = GetKeteanganDinasEkuitas("")
                //Else
                //    sKetDinas = GetKeteanganDinas("")

                //End If


                //Dim lcount As Long

                //Dim rsSD As New ADODB.Recordset
                //Dim lRet As Currency

        //           SSQL = "SELECT  sum (case when IIDRekening like '8%' Then iDebet * cJumlah else 0 end) as Beban, " & _
        //" sum (case when IIDRekening like '7%' Then -1 * iDebet * cJumlah else 0 end) as Pendapatan  from tBukubesar where iTahun = " & g_nTahun & "  " & sKetDinas & "  and ( dtTransaksi between " & ctrlPilihanwaktu1.GetSQLAwal & " AND " & ctrlPilihanwaktu1.GetSQLAkhir & " )" & _
        //" AND btJenisJurnal < " & 10
                SSQL = "SELECT  sum (case when IIDRekening like '8%' Then iDebet * cJumlah else 0 end) as Beban, " +
                        " sum (case when IIDRekening like '7%' Then -1 * iDebet * cJumlah else 0 end) as Pendapatan " +
                        "from tBukubesar where iTahun = @TAHUN  " + sKetDinas + " and ( dtTransaksi between " + tanggalAwal.ToSQLFormat() + 
                        " AND " + tanggalAkhir.ToSQLFormat() + ") AND btJenisJurnal <  10";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@TAHUN", Tahun));
       

                DataTable dt = new DataTable();
                decimal SuplusDefisit = 0L;
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        SuplusDefisit = DataFormat.GetDecimal(dr["Pendapatan"]) -
                                         DataFormat.GetDecimal(dr["Beban"]);
                    }
                }
                return SuplusDefisit;



            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return 0;
            }

        }
        public bool MasukkanSurplusdefisit(int SKPD, int ppkd,
            DateTime tanggalAwal, DateTime tanggalAkhir, bool gabunganPPDdanBukan)
        {
            try
            {
                _isError = false;

                decimal surplusDeficit = GetSurplusdefisit(SKPD, ppkd,
                                        tanggalAwal, tanggalAkhir);

                BukuBesar bb = new BukuBesar();
                bb.IDDinas = SKPD;
                bb.PPKD = ppkd;
                bb.NoJurnal =1;
                bb.Tahun = Tahun;
                bb.IDDinas= SKPD;
                bb.TanggalTransaksi = new DateTime(Tahun,1, 1);
       
                bb.KodeKategori = bb.IDDinas.ToString().ToKodeKategori();
                bb.KodeUrusan = bb.IDDinas.ToString().ToKodeUrusan();
                bb.IDDinas.ToString().ToKodeSKPD();
                bb.IDRekening=CON_IDREKENING_SURPLUSDEEFISIT;
                bb.Jumlah= surplusDeficit;
                bb.Debet=-1;
                bb.JenisJurnal=99;
                bb.NoJurnal = 1;
                bb.PPKD = ppkd ;
                if (ppkd == -1)
                {
                    bb.PPKD = 0;
                }

                if (Simpan(bb) == false)
                {
                    _lastError = "Kesalahanmenimpan SUrplus defisit";

                }
                
                decimal cJumlahRKSKPDTahunLalu = GetRKSKPDTAhunLalu(SKPD, ppkd,gabunganPPDdanBukan);
                decimal ekuitasAwal = GetEkuitasAwal(SKPD, ppkd);
                      //            decimal akuitasAwal=GetEkuitasAwal(SKPD,ppkd);
    //SSQL = "insert into tBukuBesar values (1," & g_nTahun & "," & m_iKK & "," & m_iKU & "," & m_iKSKPD & "," & m_iKUK & "," & m_iKK & "," & m_iKU & ",0,0," + CStr(m_KodeSurplusDefisit) + "," & SQLDateFormat(DateSerial(g_nTahun, 1, 1)) & ",-1," & Replace(CStr(cEquitas), ",", ".") & ",90,'','',90," & m_iPPKD & ",0,0,0,0)"
            //ExecuteEx SSQL
                bb = new BukuBesar();
                bb.IDDinas = SKPD;
                bb.PPKD = ppkd;
                bb.NoJurnal = 2;
                bb.Tahun = Tahun;
                bb.IDDinas = SKPD;
                bb.TanggalTransaksi = new DateTime(Tahun, 1, 1);
                bb.KodeKategori = bb.IDDinas.ToString().ToKodeKategori();
                bb.KodeUrusan = bb.IDDinas.ToString().ToKodeUrusan();
                bb.IDDinas.ToString().ToKodeSKPD();
                bb.IDRekening = CON_IDREKENING_EQUITESAWAL;
                bb.Jumlah = ekuitasAwal - cJumlahRKSKPDTahunLalu;
                bb.Debet = -1;
                bb.JenisJurnal = 99;
                bb.NoJurnal = 2;
                bb.PPKD = ppkd;
                if (ppkd == -1)
                {
                    bb.PPKD = 0;
                }
                Simpan(bb);
                return true;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }
        }

        
        private decimal GetRKSKPDTAhunLalu(int SKPD, int ppkd, bool gabunganPPDdanBukan)
        {
           

                string  sKetDinas;// As String
                sKetDinas = KetKeteranganDias(SKPD, ppkd);
                SSQL = "";

                decimal rkSKPDAwal = 0L;
//                    If m_iPPKD = 1 Or (chkGabunganPPKDdanBukan.Value = vbChecked) Then
                if (ppkd == 1 || gabunganPPDdanBukan==false)
                {

                    SSQL = "SELECT sum(mRekening.iDebet * tSaldoAwalRek.iDebet * tsaldoAwalRek.cJumlah) as Jumlah from tSaldoAwalRek inner join mRekening " +
                       " ON tSaldoAwalRek.IIDRekening= mRekening.IIDRekening where 1>0  " + sKetDinas + " AND tSaldoAwalRek.IIDRekening like '118%'";


                    //       End If


                    DataTable dt = new DataTable();

                    dt = _dbHelper.ExecuteDataTable(SSQL);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            rkSKPDAwal = DataFormat.GetDecimal(dr["Jumlah"]);



                        }
                    }
                }
                return rkSKPDAwal;          

        }
//        Private Function GetRKSKPDTAhunLalu() As Currency
//    Dim SSQL As String
//    Dim sKetDinas As String
//    sKetDinas = GetKeteanganDinasEkuitas("")
//    Dim lcount As Long
    
//    Dim rsSD As New ADODB.Recordset
//    Dim lRet As Currency
//    lRet = 0
    
//    If m_iPPKD = 1 Or (chkGabunganPPKDdanBukan.Value = vbChecked) Then
      
    
//       SSQL = "SELECT sum(mRekening_SAP.iDebet * tSaldoAwalRek.iDebet * tsaldoAwalRek.cJumlah) as Jumlah from tSaldoAwalRek inner join mRekening_SAP " & _
//           " ON tSaldoAwalRek.IIDRekening= mRekening_SAP.IIDRekening where 1>0  " & sKetDinas & " AND tSaldoAwalRek.IIDRekening like '118%'"
        
    
//    End If
    
    
        
//    lcount = openRS(rsSD, SSQL)
//    If lcount > 0 Then
    
//        GetRKSKPDTAhunLalu = IIf(IsNull(rsSD!Jumlah), 0, rsSD!Jumlah)
        
        
//    Else
//        GetRKSKPDTAhunLalu = 0
//    End If
   
//End Function

        private decimal GetEkuitasAwal(int SKPD, int ppkd)
        {
           

                string  sKetDinas;// As String
                sKetDinas = KetKeteranganDiasEkuitasAwal(SKPD, ppkd);
   
                if (SKPD !=0) {    
                   SSQL = "SELECT sum(mRekening.iDebet * tSaldoAwalRek.iDebet * tsaldoAwalRek.cJumlah) as Jumlah from tSaldoAwalRek inner join mRekening " +
                        " ON tSaldoAwalRek.IIDRekening= mRekening.IIDRekening where 1>0  " + sKetDinas + " AND tSaldoAwalRek.IIDRekening like '31%'";         
                } else{

                   //If m_iPPKD <> 0 Then
                if (ppkd !=0){
                    SSQL = "SELECT SUM(Jumlah ) as Jumlah from ( ";
                    SSQL = SSQL + "SELECT sum(mRekening.iDebet * tSaldoAwalRek.iDebet * tsaldoAwalRek.cJumlah) as Jumlah from tSaldoAwalRek inner join mRekening " +
                      " ON tSaldoAwalRek.IIDRekening= mRekening.IIDRekening where 1>0  " + sKetDinas + " AND tSaldoAwalRek.IIDRekening like '31%'";
                     SSQL = SSQL + " UNION SELECT sum(mRekening.iDebet * tBukubesar.iDebet * tBukubesar.cJumlah) as Jumlah from tBukubesar inner join mRekening " +
                      " ON tBukubesar.IIDRekening= mRekening.IIDRekening where 1>0  " + sKetDinas + " AND tBukubesar.IIDRekening = '310101010001' and btjenisjurnal< 90 and inojurnal  not in (1,2)";
                     SSQL = SSQL + ") A";
                 } else{

                     SSQL = "SELECT SUM(Jumlah ) as Jumlah from ( ";
                     SSQL =SSQL+ "SELECT sum(mRekening.iDebet * tSaldoAwalRek.iDebet * tsaldoAwalRek.cJumlah) as Jumlah from tSaldoAwalRek inner join mRekening " +
                     " ON tSaldoAwalRek.IIDRekening= mRekening.IIDRekening where 1>0 " + sKetDinas + " AND tSaldoAwalRek.IIDRekening like '31%'";
                     SSQL = SSQL + " UNION SELECT sum(mRekening.iDebet * tBukubesar.iDebet * tBukubesar.cJumlah) as Jumlah from tBukubesar inner join mRekening " +
                      " ON tBukubesar.IIDRekening= mRekening.IIDRekening where 1>0 " + sKetDinas + " AND tBukubesar.IIDRekening = '310101010001' and btjenisjurnal< 90 and inojurnal  not in (1,2)";
                     SSQL = SSQL + ") A";
                }
      
                }


                decimal sldoawal = 0L;
                List<decimal> lst = new List<decimal>();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {DataRow dr = dt.Rows[0];
                    sldoawal = DataFormat.GetDecimal(dr["Jumlah"]);
                


                    }
                }
                return sldoawal;          

        }
        public bool Simpan(BukuBesar bb)
        {

            try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                ////       //IDSUbkegiatan
                SSQL = "insert into tBukuBesar  (iNoJurnal,iTahun,btKodeKategori,btKodeUrusan,btKodeSKPD,btKodeUK," +
                    "btKodeKategoripelaksana,btKodeUrusanPelaksana,btIDProgram,btIDKegiatan,iidRekening,dtTransaksi,iDebet," +
                    "cJumlah ,bPPKD,btJenisJurnal, sNoBukti,sKeterangan,IDDinas,IDUrusan, iDPROgram,idKegiatan,IDSUbkegiatan)values(" +
                    " @NoJurnal  ,@Tahun,@KodeKategori,@KodeUrusan,@KodeSKPD,@KodeUK," +
                    "@KodeKategoripelaksana,@KodeUrusanPelaksana,@IDProgram,@IDKegiatan,@idRekening," + bb.TanggalTransaksi.ToSQLFormat() + ",@Debet," +
                    "@Jumlah ,@PPKD,@JenisJurnal, @NoBukti,@Keterangan,@Dinas,@Urusan, @PROgram,@Kegiatan,@SUbkegiatan)";

                paramCollection.Add(new DBParameter("@NoJurnal", bb.NoJurnal));
                paramCollection.Add(new DBParameter("@Tahun", bb.Tahun));
                paramCollection.Add(new DBParameter("@KodeKategori", bb.KodeKategori));
                paramCollection.Add(new DBParameter("@KodeUrusan", bb.KodeUrusan));
                paramCollection.Add(new DBParameter("@KodeSKPD", bb.KodeSKPD));
                paramCollection.Add(new DBParameter("@KodeUK", bb.KodeUK));
                paramCollection.Add(new DBParameter("@KodeKategoripelaksana", bb.KodeKategoriPelaksana));
                paramCollection.Add(new DBParameter("@KodeUrusanPelaksana", bb.KodeUrusanPelaksana));
                paramCollection.Add(new DBParameter("@IDProgram", bb.KodeProgram));
                paramCollection.Add(new DBParameter("@IDKegiatan", bb.KodeKegiatan));
                paramCollection.Add(new DBParameter("@idRekening", bb.IDRekening));
                paramCollection.Add(new DBParameter("@TanggalTransaksi", bb.TanggalTransaksi, DbType.Date));
                paramCollection.Add(new DBParameter("@Debet", bb.Debet));

                paramCollection.Add(new DBParameter("@Jumlah", bb.Jumlah, DbType.Decimal));
                paramCollection.Add(new DBParameter("@PPKD", bb.PPKD));
                paramCollection.Add(new DBParameter("@JenisJurnal", bb.JenisJurnal));
                paramCollection.Add(new DBParameter("@NoBukti", bb.NoBukti));
                paramCollection.Add(new DBParameter("@Keterangan", bb.Keterangan));
                paramCollection.Add(new DBParameter("@Dinas", bb.IDDinas));
                paramCollection.Add(new DBParameter("@Urusan", bb.IDUrusan));
                paramCollection.Add(new DBParameter("@PROgram", bb.IDProgram));
                paramCollection.Add(new DBParameter("@Kegiatan", bb.IDKegiatan));
                paramCollection.Add(new DBParameter("@SUbkegiatan", bb.IDSubKegiatan));

                ////SSQL = "insert into tBukuBesar  (iNoJurnal,iTahun,btKodeKategori,btKodeUrusan,btKodeSKPD,btKodeUK," +
                ////    "btKodeKategoripelaksana,btKodeUrusanPelaksana,btIDProgram,btIDKegiatan,iidRekening,dtTransaksi,iDebet," +
                ////    "cJumlah ,bPPKD,btJenisJurnal, sNoBukti,sKeterangan,IDDinas,IDUrusan, iDPROgram,idKegiatan,IDSUbkegiatan)values(" +
                ////      bb.NoJurnal.ToString()  +  "," + bb.Tahun.ToString() + ",0,0," + bb.KodeSKPD.ToString()   + "," + bb.KodeUK.ToString() + "," +
                ////    "0,0,0,0," + bb.IDRekening + "," + bb.TanggalTransaksi.ToSQLFormat() + "," + bb.Debet.ToString()  + "," +
                ////    "@Jumlah ," + bb.PPKD.ToString()  + "," + bb.JenisJurnal.ToString()  + ", '',''," + bb.IDDinas.ToString() +
                ////    "," + bb.IDUrusan.ToString() +", " + bb.IDProgram.ToString() +"," + bb.IDKegiatan.ToString() +"," +  bb.IDSubKegiatan.ToString() +")";


                _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                // //   SSQL="insert into tBukuBesar values (1," & g_nTahun & "," & m_iKK & "," & m_iKU & "," & m_iKSKPD & "," & m_iKUK & "," & m_iKK & "," & m_iKU & ",0,0," + CStr(m_KodeSurplusDefisit) + "," & SQLDateFormat(DateSerial(g_nTahun, 1, 1)) & ",-1," & Replace(CStr(cEquitas), ",", ".") & ",90,'','',90," & m_iPPKD & ",0,0,0,0)""
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
