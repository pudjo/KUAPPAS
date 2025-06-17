using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DTO;
using System.Data;
using DataAccess;
using Formatting;

namespace BP.Bendahara
{
    public class AnggaranKasLogic : BP
    {
        private int mprofile;
        public AnggaranKasLogic(int pTahun, int profile = 2)
            : base(pTahun, 0, profile)
        {

            m_sNamaTabel = "tAnggaranKas";
            mprofile = profile;

        }
        public List<AnggaranKas> GetOnPeriode(int dinas,int Jenis)
        {
            try
            {
                List<AnggaranKas> lst = new List<AnggaranKas>();
                SSQL = "SELECT  * from dbo.fnGetAKPerPeriode (dinas, Jenis) order by Rekening";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                                select new AnggaranKas()
                                {
                                 
                       
                                    IDRekening = DataFormat.GetLong(dr["Rekening"]),
                                    Bulan1 = DataFormat.GetDecimal(dr["Januari"]),
                                    Bulan2 = DataFormat.GetDecimal(dr["februari"]),
                                    Bulan3 = DataFormat.GetDecimal(dr["maret"]),
                                    Bulan4 = DataFormat.GetDecimal(dr["april"]),
                                    Bulan5 = DataFormat.GetDecimal(dr["mei"]),
                                    Bulan6 = DataFormat.GetDecimal(dr["juni"]),
                                    Bulan7 = DataFormat.GetDecimal(dr["juli"]),
                                    Bulan8 = DataFormat.GetDecimal(dr["agustus"]),
                                    Bulan9 = DataFormat.GetDecimal(dr["september"]),
                                    Bulan10 = DataFormat.GetDecimal(dr["oktober"]),
                                    Bulan11 = DataFormat.GetDecimal(dr["november"]),
                                    Bulan12 = DataFormat.GetDecimal(dr["desember"]),
                                    

                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Anggaran = DataFormat.GetDecimal(dr["Anggaran"]),
                       
                                }).ToList();
                    }
                }
               
                return lst;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return null;

            }

        }
        private string GetNamaKolom(int pTahap)
        {
            string sKolom = "cJumlahOlah";

            switch (pTahap)
            {
                case 1:
                    sKolom = "cJumlahRKA";
                    break;
                case 2:
                    sKolom = "cJumlahMurni";
                    break;
                case 3:
                    sKolom = "cJumlahGeser";
                    break;
                case 4:
                    sKolom = "cJumlahRKAP";
                    break;


            }
            return sKolom;

        }

        private void CekView(int _pTahap)
        {
            string sNamaKolom;
            try
            {

                if (HapusView("vwAnggaranKasDanAnggaran") == true){
                    SSQL = "CREATE VIEW ";
                } 
                else{
                    SSQL = "ALTER VIEW ";
                }
               
                    //string sNamaKolom;


                    sNamaKolom = "cJumlahGeser";

                    switch (_pTahap)
                    {
                        case 2:
                            sNamaKolom = "cJumlahMurni";
                            break;
                        case 3:
                            sNamaKolom = "cJumlahGeser";
                            break;
                        case 4:
                            sNamaKolom = "cJumlahRKAP";
                            break;
                        case 5:
                            sNamaKolom = "cJumlahABT";
                            break;

                    }
                    if (Tahun <= 2020)
                    {
                        SSQL = SSQL + " vwAnggaranKasDanAnggaran AS   " +
                                " Select A.iTahun,A.IDDInas, A.IDUrusan, A.IDProgram,  A.IDKegiatan, A.btJEnis, A.IIDRekening,A." + sNamaKolom + " AS cJumlahMurni,A." + sNamaKolom + " as cJumlahOlah,A." + sNamaKolom + " as cJumlah,A." + sNamaKolom + " AS cJumlahABT, B.iTahap,  " +
                                " B.cBulan1,B.cBulan2,B.cBulan3, B.cBulan4, B.cBulan5,B.cBulan6, B.cBulan7, B.cBulan8, B.cBulan9,B.cbulan10, B.cBulan11, B.cBulan12,   " +
                                "  case when (A.IIDRekening like '4%') Or (A.IIDRekening like '61%')   " +
                                " THEN 1 ELSE -1 END AS Debet    FROM tAnggaranRekening_A A LEFT  JOIN tAnggaranKas B   " +
                                " ON A.iTahun = B.iTahun AND A.IDDInas = B.IDDinas  AND A.IDUrusan= B.IDUrusan and A.IIDRekening=B.IIDRekening    " +
                                " AND A.IDProgram = B.IDProgram AND A.IDKegiatan = B.IDkegiatan  AND A.btJenis = B.btJenis where A.idsubkegiatan=0   ";

                    }
                    else
                    {
                        SSQL = SSQL + "  vwAnggaranKasDanAnggaran AS   " +
                            " Select A.iTahun,A.IDDInas, A.IDUnit,A.IDUrusan, A.IDProgram,  A.IDKegiatan,A.IDSubKegiatan, A.btJEnis, A.IIDRekening,A." + sNamaKolom + " AS cJumlahMurni,A." + sNamaKolom + " as cJumlahOlah,A." + sNamaKolom + " as cJumlah,A." + sNamaKolom + " AS cJumlahABT, B.iTahap,  " +
                            " B.cBulan1,B.cBulan2,B.cBulan3, B.cBulan4, B.cBulan5,B.cBulan6, B.cBulan7, B.cBulan8, B.cBulan9,B.cbulan10, B.cBulan11, B.cBulan12,   " +
                            "  case when (A.IIDRekening like '4%') Or (A.IIDRekening like '61%')   " +
                            " THEN 1 ELSE -1 END AS Debet    FROM tAnggaranRekening_A A LEFT JOIN tAnggaranKas B   " +
                            " ON A.iTahun = B.iTahun AND A.IDDInas = B.IDDinas  AND A.btKodeUK = B.btKodeUK   AND A.IDUrusan= B.IDUrusan and A.IIDRekening=B.IIDRekening    " +
                            " AND A.IDProgram = B.IDProgram AND A.IDKegiatan = B.IDkegiatan  AND A.IDSubkegiatan  = B.IDSubkegiatan  AND A.btJenis = B.btJenis  and B.iTahap = "+_pTahap.ToString() ;

                    }


                    _dbHelper.ExecuteNonQuery(SSQL);
                
                return;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;


            }
        }

        public bool Simpan(List<AnggaranKas> _lst, int _tahun, int _idDinas, int idUnit, int _idUrusan, int _idProgram, int _IDkegiatan, int _jenis, int _iTahap, long idsubkegiatan)
        {
            try
            {
                DBParameterCollection paramHapus = new DBParameterCollection();



                SSQL = "DELETE " + m_sNamaTabel + " WHERE iTahun =@tahun And IDDInas=@idDinas and btKodeuk =@dUnit And IDUrusan=@idUrusan " +
                    " AND IDProgram =@idProgram and IDKegiatan = @IDkegiatan  and IDSubKegiatan = @idsubkegiatan and btJenis= @jenis and iTahap =@iTahap";

                paramHapus.Add(new DBParameter("@tahun", _tahun));
                paramHapus.Add(new DBParameter("@idDinas", _idDinas));
                paramHapus.Add(new DBParameter("@dUnit", idUnit));
                paramHapus.Add(new DBParameter("@idUrusan", _idUrusan));
                paramHapus.Add(new DBParameter("@idProgram", _idProgram));
                paramHapus.Add(new DBParameter("@IDkegiatan", _IDkegiatan));
                paramHapus.Add(new DBParameter("@idsubkegiatan", idsubkegiatan));
                paramHapus.Add(new DBParameter("@jenis", _jenis));
                paramHapus.Add(new DBParameter("@iTahap", _iTahap));



                _dbHelper.ExecuteNonQuery(SSQL, paramHapus);

                SSQL = "DELETE " + m_sNamaTabel + " WHERE iTahun =" + _tahun.ToString() + "   And IDDInas=" + _idDinas.ToString() + " And IDUrusan=" + _idUrusan.ToString() +
                        " and btKodeUK =" + idUnit.ToString() + "AND IDProgram =" + _idProgram.ToString() + " and IDKegiatan = " + _IDkegiatan.ToString() + "  and IDSubKegiatan = " + idsubkegiatan.ToString() + "  and btJenis=" + _jenis.ToString() + " and iTahap =" + _iTahap.ToString();

                _dbHelper.ExecuteNonQuery(SSQL);



                foreach (AnggaranKas ak in _lst)
                {

                    if (Cek(ak) == false)
                    {

                        SSQL = "INSERT INTO " + m_sNamaTabel + " (cBulan1,cBulan2,cBulan3,cBulan4," +
                           "cBulan5, cBulan6, cBulan7, cBulan8, cBulan9,cBulan10," +
                           " cBulan11, cBulan12 , iTahun ,IDDInas,IDUrusan, " +
                           " IDProgram ,IDKegiatan ,idsubkegiatan,IIDRekening, btJenis,iTahap ,btidsubkegiatan,idunit,btKodeUK) values ( @pcBulan1,@pcBulan2,@pcBulan3,@pcBulan4," +
                           "@pcBulan5, @pcBulan6, @pcBulan7, @pcBulan8, @pcBulan9,@pcBulan10" +
                           ", @pcBulan11, @pcBulan12 , @piTahun ,@pIDDInas,@pIDUrusan, " +
                           " @pIDProgram ,@pIDKegiatan ,@idsubkegiatan,@pIIDRekening,@pbtJenis, @piTahap, @pbtidsubkegiatan, @idunit,@KodeUK)";




                    }
                    else
                    {
                        SSQL = "UPDATE " + m_sNamaTabel + " SET cBulan1=@pcBulan1 ,cBulan2= @pcBulan2,cBulan3=@pcBulan3 ,cBulan4=@pcBulan4 " +
                           ",cBulan5=@pcBulan5 , cBulan6=@pcBulan6 , cBulan7=@pcBulan7 , cBulan8= @pcBulan8 , cBulan9=@pcBulan9 ,cBulan10=@pcBulan10 " +
                           ", cBulan11=@pcBulan11 , cBulan12=@pcBulan12  WHERE iTahun =@piTahun   And IDDInas=@pIDDInas  And IDUrusan=@pIDUrusan  " +
                           " AND IDProgram =@pIDProgram   and IDKegiatan = @pIDKegiatan and idsubkegiatan =@idsubkegiatan and IIDRekening =@pIIDRekening AND btJenis=@pbtJenis AND iTahap = @piTahap and isnull(IdUnit,0) = @idunit ";
                    }
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    //paramCollection.Add(new DBParameter("@pID", _newID));
                    paramCollection.Add(new DBParameter("@pcBulan1", ak.Bulan1, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcBulan2", ak.Bulan2, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcBulan3", ak.Bulan3, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcBulan4", ak.Bulan4, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcBulan5", ak.Bulan5, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcBulan6", ak.Bulan6, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcBulan7", ak.Bulan7, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcBulan8", ak.Bulan8, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcBulan9", ak.Bulan9, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcBulan10", ak.Bulan10, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcBulan11", ak.Bulan11, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcBulan12", ak.Bulan12, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@piTahun", ak.Tahun, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDDInas", ak.IDDinas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDUrusan", ak.IDUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDProgram", ak.IDProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDKegiatan", ak.IDKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@idsubkegiatan", ak.IdSubKegiatan, DbType.Int64));

                    paramCollection.Add(new DBParameter("@pIIDRekening", ak.IDRekening, DbType.Int64));
                    paramCollection.Add(new DBParameter("@pbtJenis", ak.Jenis, DbType.Int16));
                    paramCollection.Add(new DBParameter("@piTahap", ak.Tahap, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pbtidsubkegiatan", ak.IdSubKegiatan % 100, DbType.Int32));
                    paramCollection.Add(new DBParameter("@idunit", ak.IDDinas+ idUnit, DbType.Int32));
                    paramCollection.Add(new DBParameter("@KodeUK", idUnit , DbType.Int32));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);







                }

                SSQL = "UPDATE tANggaranKAs SET   btKodeKategori= CAST(  SUBSTRING(Convert(varchar(7),IDDinas),1,1) as smallint)  ,btKodeUrusan= CAST(  SUBSTRING(Convert(varchar(7),IDDinas),2,2) as smallint), " +
                    "btKodeSKPD= CAST(  SUBSTRING(Convert(varchar(7),IDDinas),4,2) as smallint) , " +
                    "btKodekategoriPelaksana= CAST(  SUBSTRING(Convert(varchar(3),IDUrusan ),1,1) as smallint) ," +
                    "btKodeUrusanPelaksana= CAST(  SUBSTRING(Convert(varchar(3),IDUrusan ),2,2) as smallint)," +
                    "btIDProgram =CAST(  SUBSTRING(Convert(varchar(5),IDProgram),4,2) as smallint), " +
                    "btIDKegiatan=CAST(  SUBSTRING(Convert(varchar(8),IDKEgiatan),6,3) as smallint) WHERE iTahun =@tahun And IDDInas=@idDinas and idunit =@dUnit And IDUrusan=@idUrusan " +
                     " AND IDProgram =@idProgram and IDKegiatan = @IDkegiatan  and IDSubKegiatan = @idsubkegiatan and btJenis= @jenis and iTahap =@iTahap";


                _dbHelper.ExecuteNonQuery(SSQL, paramHapus);
                return true;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;

            }
        }


        public bool SimpanPenyempurnaan(List<AnggaranKas> _lst, int _tahun, int _idDinas, int _idUrusan, int _idProgram, int _IDkegiatan, int _jenis)
        {
            try
            {


                foreach (AnggaranKas ak in _lst)
                {
                    if (Cek(ak) == false)
                    {

                        SSQL = "INSERT INTO " + m_sNamaTabel + " (cBulan1,cBulan2,cBulan3,cBulan4," +
                           "cBulan5, cBulan6, cBulan7, cBulan8, cBulan9,cBulan10," +
                           " cBulan11, cBulan12 , iTahun ,IDDInas,IDUrusan, " +
                           " IDProgram ,IDKegiatan ,IIDRekening, btJenis ) values ( @pcBulan1,@pcBulan2,@pcBulan3,@pcBulan4," +
                           "@pcBulan5, @pcBulan6, @pcBulan7, @pcBulan8, @pcBulan9,@pcBulan10" +
                           ", @pcBulan11, @pcBulan12 , @piTahun ,@pIDDInas,@pIDUrusan, " +
                           " @pIDProgram ,@pIDKegiatan ,@pIIDRekening,@pbtJenis)";






                    }
                    else
                    {
                        SSQL = "UPDATE " + m_sNamaTabel + " SET cBulan1=@pcBulan1 ,cBulan2= @pcBulan2,cBulan3=@pcBulan3 ,cBulan4=@pcBulan4 " +
                           ",cBulan5=@pcBulan5 , cBulan6=@pcBulan6 , cBulan7=@pcBulan7 , cBulan8= @pcBulan8 , cBulan9=@pcBulan9 ,cBulan10=@pcBulan10 " +
                           ", cBulan11=@pcBulan11 , cBulan12=@pcBulan12  WHERE iTahun =@piTahun   And IDDInas=@pIDDInas  And IDUrusan=@pIDUrusan  " +
                           " AND IDProgram =@pIDProgram   and IDKegiatan = @pIDKegiatan and IIDRekening =@pIIDRekening AND btJenis=@pbtJenis";
                    }
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    //paramCollection.Add(new DBParameter("@pID", _newID));
                    paramCollection.Add(new DBParameter("@pcBulan1", ak.Bulan1, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcBulan2", ak.Bulan2, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcBulan3", ak.Bulan3, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcBulan4", ak.Bulan4, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcBulan5", ak.Bulan5, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcBulan6", ak.Bulan6, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcBulan7", ak.Bulan7, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcBulan8", ak.Bulan8, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcBulan9", ak.Bulan9, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcBulan10", ak.Bulan10, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcBulan11", ak.Bulan11, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcBulan12", ak.Bulan12, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@piTahun", ak.Tahun, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDDInas", ak.IDDinas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDUrusan", ak.IDUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDProgram", ak.IDProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDKegiatan", ak.IDKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIIDRekening", ak.IDRekening, DbType.Int64));
                    paramCollection.Add(new DBParameter("@pbtJenis", ak.Jenis, DbType.Int16));
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
        private bool Cek(AnggaranKas ak)
        {
            // true jika sudah ada 


            SSQL = "Select * FROM " + m_sNamaTabel + " WHERE iTahun =@Tahun And IDDInas=@IDDinas" +
                    " and idUnit =@IDUnit And IDUrusan=@IDUrusan AND IDProgram =@IDProgram  and IDKegiatan =@IDKegiatan " +
                    " and idsubkegiatan =@IdSubKegiatan  AND IIDRekening =@IDRekening and iTahap =@Tahap";


            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@Tahun", ak.Tahun));
            paramCollection.Add(new DBParameter("@IDDinas", ak.IDDinas));
            paramCollection.Add(new DBParameter("@IDUnit", ak.IDUnit));
            paramCollection.Add(new DBParameter("@IDUrusan", ak.IDUrusan));
            paramCollection.Add(new DBParameter("@IDProgram", ak.IDProgram));
            paramCollection.Add(new DBParameter("@IDKegiatan", ak.IDKegiatan));
            paramCollection.Add(new DBParameter("@IdSubKegiatan", ak.IdSubKegiatan));
            paramCollection.Add(new DBParameter("@IDRekening", ak.IDRekening));
            paramCollection.Add(new DBParameter("@Tahap", ak.Tahap));


            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            } return false;

        }


        public List<AnggaranKas> Prepare(int pTahun, int pIDDInas, int idUnit, int pIDUrusan, int pIDKegiatan, int _pJenis, int iTahap, long idsubkegiatan)
        {
            List<AnggaranKas> _lst = new List<AnggaranKas>();
            try
            {
                string sKolom = "cJumlahOlah";
                switch (iTahap)
                {
                    case 1:
                        sKolom = "cJumlahRKA";
                        break;
                    case 2:
                        sKolom = "cJumlahMurni";
                        break;
                    case 3:
                        sKolom = "cJumlahGeser";
                        break;
                    case 4:
                        sKolom = "cJumlahRKAP";
                        break;
                    case 5:
                        sKolom = "cJumlahABT";
                        break;


                }

                if (_pJenis == 0)
                {


                    SSQL = "Select 0 as Level,A.IDUrusan,A.IDProgram, A.IDKegiatan , A.btJenis, 0 AS IDrekening, A.sNama as Nama ,SUM(b." + sKolom + " ) as Anggaran FROM tKegiatan_A A INNER JOIN tANggaranRekening_A B " +
                        " ON A.iTahun= B.iTahun ANd A.IDurusan= B.IDurusan and A.IDDInas= B.IDDinas and A.IDkegiatan=B.IDkegiatan  and A.IDProgram=B.IDProgram  " +
                        " WHERE A.iTahun = " + pTahun.ToString() + " AND A.IDDInas=" + pIDDInas.ToString() + " GROUP BY  A.IDUrusan,A.IDProgram,A.IDKegiatan , A.btJenis, A.sNama ";
                    SSQL = SSQL + " UNION " +
                         " SELECT 1 as Level, a.IDUrusan,a.IDProgram, a.IDKegiatan,a.btJenis, b.IIDRekening as IDrekening, b.sNamaRekening as Nama , a." + sKolom + " AS Anggaran FROM mRekening b INNER JOIN tAnggaranRekening_A a " +
                         " ON A.IIDRekening =B.IIDRekening  WHERE a.iTahun = " + pTahun.ToString() + " AND a.IDDInas=" + pIDDInas.ToString();
                    SSQL = SSQL + " ORDER BY A.IDUrusan,a.IDProgram,a.IDKegiatan, a.btJenis,a.IDRekening";
                }
                else
                {
                    if (Tahun <= 2020)
                    {
                        SSQL = "Select 0 as Level,A.IDUrusan,A.IDProgram, A.IDKegiatan , A.btJenis, 0 AS IDrekening, A.sNama as Nama ,SUM(B." + sKolom + ") as Anggaran FROM tKegiatan_A A INNER JOIN tANggaranRekening_A B " +
                            " ON A.iTahun= B.iTahun ANd A.IDurusan= B.IDurusan and A.IDDInas= B.IDDinas and A.IDkegiatan=B.IDkegiatan AND A.btJenis= B.btJenis AND A.IDUrusan= B.IDurusan AND A.IDProgram= B.IDProgram " +
                            " WHERE A.iTahun = " + pTahun.ToString() + " AND A.IDDInas=" + pIDDInas.ToString() + "   and b.idsubkegiatan=0  AND A.IDUrusan= " + pIDUrusan.ToString() + "  AND A.IDKegiatan= " + pIDKegiatan.ToString() + " AND A.btJenis=" + _pJenis.ToString() + "  GROUP BY  A.IDUrusan,A.IDProgram,A.IDKegiatan , A.btJenis, A.sNama ";
                        SSQL = SSQL + " UNION " +
                             " SELECT 1 as Level, A.IDUrusan,A.IDProgram, A.IDKegiatan,A.btJenis, B.IIDRekening as IDrekening, B.sNamaRekening as Nama , A." + sKolom + " AS Anggaran FROM mRekening B INNER JOIN tAnggaranRekening_A A " +
                             " ON A.IIDRekening =B.IIDRekening  WHERE A.iTahun = " + pTahun.ToString() + "   and A.idsubkegiatan=0 AND A.IDDInas=" + pIDDInas.ToString() + " AND a.IDUrusan= " + pIDUrusan.ToString() + "  AND a.IDKegiatan= " + pIDKegiatan.ToString() + " AND A.btJenis=" + _pJenis.ToString();

                        SSQL = SSQL + " ORDER BY a.IDUrusan,a.IDProgram,a.IDKegiatan, a.btJenis,IDRekening";
                    }
                    else
                    {

                        if (_pJenis == 3)
                        {
                            //and A.btKodeuk = " + idUnit.ToString() + "
                            //  and btKodeuk = " + idUnit.ToString() + "
                                SSQL = "Select 0 as Level,A.IDUnit,A.IDUrusan,A.IDProgram, A.IDKegiatan , A.IDSubKegiatan,A.btJenis, 0 AS IDrekening, A.Nama as Nama ,SUM(B." + sKolom + ") as Anggaran FROM TSubkegiatan A INNER JOIN tANggaranRekening_A B " +
                               " ON A.iTahun= B.iTahun ANd A.IDurusan= B.IDurusan and A.IDDInas= B.IDDinas and A.IDkegiatan=B.IDkegiatan and A.btKodeUK=B.btKodeUK and A.IDsubkegiatan=B.IDsubkegiatan AND A.btJenis= B.btJenis AND A.btkodeuk= B.btkodeuk AND A.IDUrusan= B.IDurusan AND A.IDProgram= B.IDProgram " +
                               " WHERE A.iTahun = " + pTahun.ToString() + " AND A.IDDInas=" + pIDDInas.ToString() + "  AND A.btKodeUK="+idUnit.ToString() +" AND A.IDDInas=" + pIDDInas.ToString() + "    and b.idsubkegiatan=" + idsubkegiatan.ToString() + "  AND A.IDUrusan= " + pIDUrusan.ToString() + "  AND A.IDKegiatan= " + pIDKegiatan.ToString() + " AND A.btJenis=" + _pJenis.ToString() +
                               "  GROUP BY  A.IDUrusan,A.IDProgram,A.IDKegiatan , A.btJenis,A.IDSubKegiatan, A.Nama ,A.idunit";
                                SSQL = SSQL + " UNION " +
                                     " SELECT 1 as Level,A.IDUnit,  A.IDUrusan,A.IDProgram, A.IDKegiatan,A.IDSubKegiatan,A.btJenis, B.IIDRekening as IDrekening, B.sNamaRekening as Nama , A." + sKolom + " AS Anggaran FROM mRekening B INNER JOIN tAnggaranRekening_A A " +
                                     " ON A.IIDRekening =B.IIDRekening  WHERE A.iTahun = " + pTahun.ToString() + " and A.idsubkegiatan=" + idsubkegiatan.ToString() + " AND A.IDDInas=" + pIDDInas.ToString() + " AND a.IDUrusan= " + pIDUrusan.ToString() + "  AND a.IDKegiatan= " + pIDKegiatan.ToString() + " AND A.btJenis=" + _pJenis.ToString();

                                SSQL = SSQL + " AND A.btKodeUK=" + idUnit.ToString() + "  ORDER BY level,IDUrusan, IDProgram,IDKegiatan, IDSubKegiatan,IDRekening";
                            
                        }
                        else
                        {
                            SSQL = " SELECT 1 as Level, A.IDUnit, A.IDUrusan,A.IDProgram, A.IDKegiatan,A.IDSubKegiatan,A.btJenis, B.IIDRekening as IDrekening, B.sNamaRekening as Nama , A." + sKolom + " AS Anggaran FROM mRekening B INNER JOIN tAnggaranRekening_A A " +
                                     " ON A.IIDRekening =B.IIDRekening  WHERE A.iTahun = " + pTahun.ToString() + " and A.IDDInas=" + pIDDInas.ToString() + " AND A.btJenis=" + _pJenis.ToString();

                            SSQL = SSQL + " ORDER BY a.IIDRekening";


                        }


                    }
                }

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new AnggaranKas()
                                {
                                    IDDinas = pIDDInas,
                                    Tahun = pTahun,
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDRekening = DataFormat.GetLong(dr["IDRekening"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Anggaran = DataFormat.GetDecimal(dr["Anggaran"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    Level = DataFormat.GetSingle(dr["Level"])
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
        public List<AnggaranKas> Get(int pTahun, int pIDDInas, int pTahap)
        {
            List<AnggaranKas> _lst = new List<AnggaranKas>();
            try
            {

                SSQL = "Select * FROM tANggaranKas A " +
                     " WHERE A.iTahun = @Tahun AND A.IDDInas=@IDDInas AND iTahap = @iTahap " +
                     " ORDER BY  A.IDUrusan,A.IDProgram,A.IDKegiatan , A.btJenis,A.IIDRekening";


                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("@Tahun", pTahun));
                paramCollection.Add(new DBParameter("@IDDInas", pIDDInas));
                paramCollection.Add(new DBParameter("@iTahap", pTahap));

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new AnggaranKas()
                                {
                                    IDDinas = pIDDInas,
                                    Tahun = pTahun,
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDPRogram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IdSubKegiatan = DataFormat.GetLong(dr["IDSUBKEGIATAN"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    Bulan1 = DataFormat.GetDecimal(dr["cBulan1"]),
                                    Bulan2 = DataFormat.GetDecimal(dr["cBulan2"]),
                                    Bulan3 = DataFormat.GetDecimal(dr["cBulan3"]),
                                    Bulan4 = DataFormat.GetDecimal(dr["cBulan4"]),
                                    Bulan5 = DataFormat.GetDecimal(dr["cBulan5"]),
                                    Bulan6 = DataFormat.GetDecimal(dr["cBulan6"]),
                                    Bulan7 = DataFormat.GetDecimal(dr["cBulan7"]),
                                    Bulan8 = DataFormat.GetDecimal(dr["cBulan8"]),
                                    Bulan9 = DataFormat.GetDecimal(dr["cBulan9"]),
                                    Bulan10 = DataFormat.GetDecimal(dr["cBulan10"]),
                                    Bulan11 = DataFormat.GetDecimal(dr["cBulan11"]),
                                    Bulan12 = DataFormat.GetDecimal(dr["cBulan12"]),
                                    Tahap = DataFormat.GetInteger(dr["iTahap"]),
                                    IDUnit = DataFormat.GetInteger(dr["IDUnit"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),


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
        public List<AnggaranKas> Get(int pTahun, int pIDDInas, int pUnit, int pIDKegiatan, int pJenis, int _iTahap, long idsubkegiatan = 0)
        {
            List<AnggaranKas> _lst = new List<AnggaranKas>();
            try
            {
                if (Tahun <= 2020)
                {
                    SSQL = "Select * FROM tANggaranKas A " +
                         " WHERE A.iTahun = " + pTahun.ToString() + " AND A.IDDInas=" + pIDDInas.ToString() + " AND iTahap = " + _iTahap.ToString() +
                         " AND IDKegiatan = " + pIDKegiatan.ToString() + " AND btJenis =" + pJenis.ToString() + " ORDER BY  A.IDUrusan,A.IDProgram,A.IDKegiatan , A.btJenis,A.IIDRekening";

                }
                else
                {
        

                        SSQL = "Select * FROM tANggaranKas A " +
                        " WHERE A.iTahun = " + pTahun.ToString() + " AND A.btKodeUK = " + pUnit.ToString() + " and A.IDDInas=" + pIDDInas.ToString() + " AND iTahap = " + _iTahap.ToString() +
                        " AND IDKegiatan = " + pIDKegiatan.ToString() + " AND IDSubKegiatan =" + idsubkegiatan.ToString() + " AND btJenis =" + pJenis.ToString() + " ORDER BY  A.IDUrusan,A.IDProgram,A.IDKegiatan , A.btJenis,A.IIDRekening";

                }
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new AnggaranKas()
                                {
                                    IDDinas = pIDDInas,
                                    Tahun = pTahun,
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDPRogram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    Bulan1 = DataFormat.GetDecimal(dr["cBulan1"]),
                                    Bulan2 = DataFormat.GetDecimal(dr["cBulan2"]),
                                    Bulan3 = DataFormat.GetDecimal(dr["cBulan3"]),
                                    Bulan4 = DataFormat.GetDecimal(dr["cBulan4"]),
                                    Bulan5 = DataFormat.GetDecimal(dr["cBulan5"]),
                                    Bulan6 = DataFormat.GetDecimal(dr["cBulan6"]),
                                    Bulan7 = DataFormat.GetDecimal(dr["cBulan7"]),
                                    Bulan8 = DataFormat.GetDecimal(dr["cBulan8"]),
                                    Bulan9 = DataFormat.GetDecimal(dr["cBulan9"]),
                                    Bulan10 = DataFormat.GetDecimal(dr["cBulan10"]),
                                    Bulan11 = DataFormat.GetDecimal(dr["cBulan11"]),
                                    Bulan12 = DataFormat.GetDecimal(dr["cBulan12"]),
                                    Tahap = DataFormat.GetInteger(dr["iTahap"])


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

        private string AkumulasiKolom(DateTime d)
        {
            string sNamaKolom = "";
            for (int bulan = 1; bulan <= d.Month; bulan++)
            {
                sNamaKolom = sNamaKolom + "cBulan" + bulan.ToString().Trim() + "+";
            }
            sNamaKolom = sNamaKolom.Substring(0, sNamaKolom.Length - 1);
            return sNamaKolom;

        }

        public List<AnggaranKas> GetAKumulasiAnggaranKas(int pTahun, int pIDDInas,int KodeUK, int jenis, DateTime batas, int pTahap, long idsubkegiatan)
        {
            List<AnggaranKas> _lst = new List<AnggaranKas>();
            try
            {

                string akumulasibulan = AkumulasiKolom(batas);


                SSQL = "Select A.iTahun,A.IDDInas, A.IDUrusan,A.IDProgram ,A.IDKegiatan,A.IIDRekening,A.IDSUbKegiatan, mr.sNamaRekening, sum(" + akumulasibulan + ") as Jumlah FROM tANggaranKas A " +
                     " INNER JOIN mRekening mr on mr.iidrekening = A.iidrekening WHERE A.iTahun = @Tahun AND A.IDDInas=@IDDInas" +
                     " and btJEnis = @jenis and iTahap =@pTahap and idsubkegiatan = @idsubkegiatan AND btKOdeUK in (0,@KodeUK) GROUP BY A.iTahun,A.IDDInas, A.IDUrusan,A.IDProgram ,A.IDKegiatan,A.IIDRekening,mr.sNamaRekening,A.IDSubKegiatan " +
                     " ORDER BY  A.IIDRekening";

                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("@Tahun", pTahun));
                paramCollection.Add(new DBParameter("@IDDInas", pIDDInas));
                paramCollection.Add(new DBParameter("@KodeUK", KodeUK));

                paramCollection.Add(new DBParameter("@jenis", jenis));
                paramCollection.Add(new DBParameter("@pTahap", pTahap));

                paramCollection.Add(new DBParameter("@idsubkegiatan", idsubkegiatan));





                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {

                    if (dt.Rows.Count > 0)
                    {

                        _lst = (from DataRow dr in dt.Rows
                                select new AnggaranKas()
                                {
                                    IDDinas = pIDDInas,
                                    Tahun = pTahun,
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDPRogram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    //  Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    Bulan1 = DataFormat.GetDecimal(dr["Jumlah"]),// + DataFormat.GetDecimal(dr["cBulan2"]) + DataFormat.GetDecimal(dr["cBulan3"]),
                                    Nama = DataFormat.GetString(dr["sNamaRekening"]),
                                    IdSubKegiatan = DataFormat.GetLong(dr["IDSUbKegiatan"]),
                                    // Tahap = DataFormat.GetInteger(dr["iTahap"])



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
        //******

        public List<TriwulanDPAAnggaranKas> GetTriWulan(int pTahun, int pIDDInas, int pIDKegiatan, int pIDUrusan, int _jenis, int iTahap, List<SKPD> lstSKPD = null)
        {
            List<TriwulanDPAAnggaranKas> _lst = new List<TriwulanDPAAnggaranKas>();
            try
            {


                string strDinas;
                if (lstSKPD != null)
                {
                    strDinas = "(";
                    foreach (SKPD d in lstSKPD)
                    {
                        strDinas = strDinas + d.ID.ToString() + ",";
                    }
                    strDinas = strDinas + "99)";
                }
                else
                {
                    strDinas = "(" + pIDDInas.ToString() + ")";

                }

                if (iTahap > 3)
                    iTahap = 4;



                SSQL = "Select 1 as No, 'Triwulan I ' as Nama , SUM(cBulan1 + cBulan2+cBulan3) as Jumlah FROM tANggaranKas A  " +
                     " WHERE A.iTahun = " + pTahun.ToString() + " AND A.IDDInas in " + strDinas + " AND IDUrusan = " + pIDUrusan.ToString() +
                     " AND IDKEgiatan =" + pIDKegiatan.ToString() + "  and btJEnis =" + _jenis.ToString() + " AND A.IIDRekening> 0 and iTahap = " + iTahap.ToString();
                SSQL = SSQL + " UNION ALL ";
                SSQL = SSQL + "Select 2 as No, 'Triwulan II ' as Nama , SUM(cBulan4 + cBulan5+cBulan6) as Jumlah FROM tANggaranKas A  " +
                     " WHERE A.iTahun = " + pTahun.ToString() + " AND A.IDDInas in " + strDinas + "   AND IDUrusan = " + pIDUrusan.ToString() +
                     " AND IDKEgiatan =" + pIDKegiatan.ToString() + "  and btJEnis =" + _jenis.ToString() + " AND A.IIDRekening> 0  and iTahap = " + iTahap.ToString();
                SSQL = SSQL + " UNION ALL ";
                SSQL = SSQL + "Select 3 as No, 'Triwulan III ' as Nama , SUM(cBulan7 + cBulan8+cBulan9) as Jumlah FROM tANggaranKas A  " +
                     " WHERE A.iTahun = " + pTahun.ToString() + " AND A.IDDInas in " + strDinas + "  AND IDUrusan = " + pIDUrusan.ToString() +
                     " AND IDKEgiatan =" + pIDKegiatan.ToString() + "  and btJEnis =" + _jenis.ToString() + " AND A.IIDRekening> 0  and iTahap = " + iTahap.ToString();
                SSQL = SSQL + " UNION ALL ";
                SSQL = SSQL + "Select 4 as No, 'Triwulan IV ' as Nama , SUM(cBulan10 + cBulan11+cBulan12 ) as Jumlah FROM tANggaranKas A  " +
                     " WHERE A.iTahun = " + pTahun.ToString() + " AND A.IDDInas in " + strDinas + "  AND IDUrusan = " + pIDUrusan.ToString() +
                     " AND IDKEgiatan =" + pIDKegiatan.ToString() + "  and btJEnis =" + _jenis.ToString() + " AND A.IIDRekening> 0  and iTahap = " + iTahap.ToString();

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TriwulanDPAAnggaranKas()
                                {
                                    No = DataFormat.GetInteger(dr["No"]).ToString(),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Nilai = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport()


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

        //public List<AnggaranKas>LoadAnggaranKas (){
        //    SUM(cBulan1) as Bulan1,SUM(cBulan2) as Bulan2,SUM(cBulan3) as Bulan3, " +
        //        "SUM(cBulan4) as Bulan4,SUM(cBulan5) as Bulan5,SUM(cBulan6) as Bulan6,SUM(cBulan7) as Bulan7,SUM(cBulan8) as Bulan8," +
        //        " SUM(cBulan9) as Bulan9,SUM(cBulan10) as Bulan10,SUM(cBulan11) as Bulan11,SUM(cBulan12) as Bulan12  
        //}


        //Select 2 as KElompok,1 as Bold, 1 as Sifat,IDRekening as Kode, 'Nama Rekening'
        //Select 3 as Kelompok,1 as Bold, 1 as Sifat,'' as Kode, 'Jumlah Pendapatan'
        //Select 4 as Kelompok,1 as Bold, 2 as Sifat,'' as Kode,'Jumlah alokasi kas yang tersedia untuk pengeluaran'

        //Select 11 as Kelompok,1 as Bold, 1 as Sifat,'52' as Kode 'Belanja Langsung'
        //Select 12 as Kelompok,1 as Bold, 1 as Sifat,IDDinas as Kode, 'Dinas'
        //Select 14 as Kelompok,1 as Bold, 1 as Sifat,IDUrusan as Kode , 'Urusan Pemerintahan'
        //Select 15 as Kelompok,1 as Bold, 1 as Sifat,IDProgram as Kode ,'Program'
        //Select 16 as Kelompok,1 as Bold, 1 as Sifat,IDkegiatan as Kode,'Kegiatan'
        //Select 17 as Kelompok,1 as Bold, 1 as Sifat,IDRekening as Kode,'Nama Rekening'
        //Select 18 as Kelompok,1 as Bold, 1 as Sifat,'' as Kode,'Jumlah Alokasi Belanja Langsung Per Bulan'
        //Select 19 as Kelompok,1 as Bold, 2 as Sifat,'' as Kode,'Jumlah Alokasi Belanja Langsung Per TriWulan'
        //Select 20 as Kelompok,1 as Bold, 2 as Sifat,'' as Kode,'Sisa kas setelah dikurangi belanja langsung per triwulan'
        //Select 21 as Kelompok,1 as Bold, 2 as Sifat,'' as Kode,'Jumlah alokasi belanja tidak langsung dan belanja langsung'
        //Select 22 as Kelompok,1 as Bold, 2 as Sifat,'' as Kode,'Sisa kas setalah dikurangi belanja tidak langsung dan belanja langsung'
        //public List<AnggaranKas> GetAnggaranKas(int _pTahun, int _pIDDInas)
        //{
        //    List<AnggaranKas> mListUnit = new List<AnggaranKas>();
        //    return mListUnit;

        //    //       Select 1 as Kelompok,1 as Bold, 1 as Sifat, '4' as Kode , 'Pendapatan Daerah' 



        //}
        public bool HilangkanDoble(int iddinas, long idsubkegiatan)
        {
            try
            {
                
                

              

                SSQL = "select distinct * into AnggaranKasDobel from tAnggaranKas where iddinas = @pDinas and idsubkegiatan = @idsubkegiatan";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pDinas", iddinas, DbType.Int32));
                paramCollection.Add(new DBParameter("@idsubkegiatan", idsubkegiatan, DbType.Int64));
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                SSQL = "delete tAnggaranKas where iddinas = @pDinas and idsubkegiatan = @idsubkegiatan";
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                SSQL = " insert into tANggaranKas  select * from AnggaranKasDobel";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "drop table AnggaranKasDobel";
                _dbHelper.ExecuteNonQuery(SSQL);
                

                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;

                return false;
            }

        }
        public List<AnggaranKas> GetAnggaranKasByIDSubKegiatan(int _pTahun, int _pIDDInas, int _pTahap, long idsubKegiatan)
        {
            List<AnggaranKas> _lst = new List<AnggaranKas>();
            
            try{
                SSQL = "SELECT * from tANggaranKas where iTahun = " + _pTahun.ToString() +
                    " AND iddinas = " + _pIDDInas.ToString() + " and idsubkegiatan =" + idsubKegiatan.ToString();

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new AnggaranKas()
                                {


                                   
                                   
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IdSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                           




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
        public List<AnggaranKas> GetAnggaranKas(ParameterLaporan p, int _pTahun, int _pIDDInas, int _pTahap, List<SKPD> lstSKPD = null)
        {
            List<AnggaranKas> _lst = new List<AnggaranKas>();
            try
            {
                CekView(_pTahap);
                SetProfileRekening(mprofile);
                SSQL = "";
                _isError = false;
                switch (p.JenisAnggaran)
                {
                    case 0:
                        //SSQL = GetQuerySaldoKas(_pTahun, _pIDDInas, p.IDunit, _pTahap, lstSKPD);
                        SSQL =  GetQueryPendapatan(_pTahun, _pIDDInas, _pTahap, lstSKPD);
                        SSQL = SSQL + " UNION " + GetQueryPenerimaanPembiayaan(_pTahun, _pIDDInas, _pTahap, lstSKPD);
                        if (Tahun < 2021)
                        {
                            SSQL = SSQL + "UNION ALL " + GetQueryBelanjaTidakLangsung(_pTahun, _pIDDInas, _pTahap, lstSKPD);

                            SSQL = SSQL + "UNION ALL " + GetQueryBelanjaLangsung(p, _pTahun, _pIDDInas, _pTahap, lstSKPD);
                        }
                        else
                        {
                            SSQL = SSQL + "UNION ALL " + GetQueryBelanjaLangsung2021(p, _pTahun, _pIDDInas, p.IDunit, _pTahap, lstSKPD);
                        }
                        SSQL = SSQL + "UNION ALL " + GetQueryPengeluaranPembiayaan(_pTahun, _pIDDInas, _pTahap, lstSKPD);

                        break;

                    case 1:

                        SSQL = SSQL + GetQueryPendapatan(_pTahun, _pIDDInas, _pTahap, lstSKPD);
                        // SSQL = SSQL + " UNION " + GetQueryPenerimaanPembiayaan(_pTahun, _pIDDInas);
                        //SSQL = SSQL + "UNION " + GetQueryBelanjaTidakLangsung(_pTahun, _pIDDInas);

                        break;
                    case 2:
                        SSQL = SSQL + GetQueryBelanjaTidakLangsung(_pTahun, _pIDDInas, _pTahap, lstSKPD);
                        break;
                    case 3:
                        if (Tahun < 2021)
                        {
                            SSQL = SSQL + GetQueryBelanjaLangsung(p, _pTahun, _pIDDInas, _pTahap, lstSKPD);
                        }
                        else
                        {
                            SSQL = SSQL + GetQueryBelanjaLangsung2021(p, _pTahun, _pIDDInas, p.IDunit, _pTahap, lstSKPD);
                        }
                        break;
                    case 4:
                        SSQL = SSQL + GetQueryPendapatan(_pTahun, _pIDDInas, _pTahap, lstSKPD);
                        break;
                    case 5:
                        SSQL = SSQL + GetQueryPendapatan(_pTahun, _pIDDInas, _pTahap, lstSKPD);
                        break;
                }


                //SSQL = GetQueryPendapatan(_pTahun, _pIDDInas);
                //SSQL = SSQL + " UNION " + GetQueryBelanjaTidakLangsung(_pTahun, _pIDDInas);

                //SSQL = SSQL + " UNION " + GetQueryBelanjaLangsung(_pTahun, _pIDDInas);
                SSQL = SSQL + " ORDER BY XGroup,KElompok,IDURusan,IDProgram, IDkegiatan,IDsubkegiatan, IIDRekening";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new AnggaranKas()
                                {


                                    Kelompok = DataFormat.GetSingle(dr["Kelompok"]),
                                    Anggaran = DataFormat.GetDecimal(dr["Anggaran"]),
                                    Bold = DataFormat.GetSingle(dr["Bold"]),
                                    Sifat = DataFormat.GetSingle(dr["Sifat"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IdSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    Kode = GetKodeAnggaranKas(DataFormat.GetInteger(dr["IDUrusan"]), _pIDDInas, DataFormat.GetInteger(dr["IDProgram"]), DataFormat.GetInteger(dr["IDkegiatan"]), DataFormat.GetLong(dr["IDSubKegiatan"]), DataFormat.GetLong(dr["IIDRekening"]), false),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Bulan1 = DataFormat.GetDecimal(dr["Januari"]),
                                    Bulan2 = DataFormat.GetDecimal(dr["Februari"]),
                                    Bulan3 = DataFormat.GetDecimal(dr["Maret"]),
                                    Bulan4 = DataFormat.GetDecimal(dr["April"]),
                                    Bulan5 = DataFormat.GetDecimal(dr["Mei"]),
                                    Bulan6 = DataFormat.GetDecimal(dr["Juni"]),
                                    Bulan7 = DataFormat.GetDecimal(dr["Juli"]),
                                    Bulan8 = DataFormat.GetDecimal(dr["Agustus"]),
                                    Bulan9 = DataFormat.GetDecimal(dr["September"]),
                                    Bulan10 = DataFormat.GetDecimal(dr["Oktober"]),
                                    Bulan11 = DataFormat.GetDecimal(dr["November"]),
                                    Bulan12 = DataFormat.GetDecimal(dr["Desember"])




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

            //       Select 1 as Kelompok,1 as Bold, 1 as Sifat, '4' as Kode , 'Pendapatan Daerah' 



        }
        //public List<AnggaranKas> GetAnggaranKasSimpatik()
        //{
        //    List<AnggaranKas> mListUnit = new List<AnggaranKas>();
        //    try
        //    {
        //        AnggaranKas ak = new AnggaranKas();
        //        ak = GetOnKode("53");
        //        ak.Nama = "Belanja Hibah";
        //        return mListUnit;

        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message;
        //        return null;
        //    }

        //    //       Select 1 as Kelompok,1 as Bold, 1 as Sifat, '4' as Kode , 'Pendapatan Daerah' 



        //}
        public List<AnggaranKasTW> GetAnggaranKasTW(ParameterLaporan p, int _pTahun, int _pIDDInas, int _pTahap, List<SKPD> lstSKPD = null)
        {
            List<AnggaranKasTW> _lst = new List<AnggaranKasTW>();
            try
            {

                CekView(_pTahap);
                SetProfileRekening(mprofile);
                SSQL = "";

                switch (p.JenisAnggaran)
                {

                    case 0:
                        SSQL = GetQuerySaldoKasTW(_pTahun, _pIDDInas, _pTahap, lstSKPD);
                        SSQL = SSQL + " UNION " + GetQueryPendapatanTW(_pTahun, _pIDDInas, _pTahap, lstSKPD);
                        SSQL = SSQL + "UNION ALL " + GetQueryBelanjaTidakLangsungTW(_pTahun, _pIDDInas, _pTahap, lstSKPD);
                        SSQL = SSQL + " UNION " + GetQueryPenerimaanPembiayaanTW(_pTahun, _pIDDInas, _pTahap, lstSKPD);

                        if (Tahun < 2021)
                        {
                            SSQL = SSQL + "UNION ALL " + GetQueryBelanjaTidakLangsungTW(_pTahun, _pIDDInas, _pTahap, lstSKPD);
                            //SSQL = SSQL + "UNION ALL " + GetQueryPengeluaranPembiayaanTW(_pTahun, _pIDDInas, _pTahap, lstSKPD);
                            SSQL = SSQL + "UNION ALL  " + GetQueryBelanjaLangsungTW(p, _pTahun, _pIDDInas, _pTahap, lstSKPD);
                        }
                        SSQL = SSQL + "UNION ALL " + GetQueryPengeluaranPembiayaanTW(_pTahun, _pIDDInas, _pTahap, lstSKPD);
                        //else
                        //{
                        //    SSQL = SSQL + "UNION ALL " + GetQueryPengeluaranPembiayaanTW(_pTahun, _pIDDInas, _pTahap, lstSKPD);
                        //    SSQL = SSQL + "UNION ALL  " + GetQueryBelanjaLangsungTW2021(p, _pTahun, _pIDDInas, _pTahap, lstSKPD);

                        //}

                        SSQL = SSQL + " UNION ALL  " + GetQuerySaldoAkhirTW(_pTahun, _pIDDInas, _pTahap, lstSKPD);
                        break;

                    case 1:

                        SSQL = SSQL + GetQueryPendapatan(_pTahun, _pIDDInas, _pTahap, lstSKPD);
                        // SSQL = SSQL + " UNION " + GetQueryPenerimaanPembiayaan(_pTahun, _pIDDInas);
                        //SSQL = SSQL + "UNION " + GetQueryBelanjaTidakLangsung(_pTahun, _pIDDInas);

                        break;
                    case 2:
                        SSQL = SSQL + GetQueryBelanjaTidakLangsung(_pTahun, _pIDDInas, _pTahap, lstSKPD);
                        break;
                    case 3:
                        SSQL = SSQL + GetQueryBelanjaLangsung(p, _pTahun, _pIDDInas, _pTahap, lstSKPD);
                        break;
                    case 4:
                        SSQL = SSQL + GetQueryPenerimaanPembiayaan(_pTahun, _pIDDInas, _pTahap, lstSKPD);
                        break;
                    case 5:
                        SSQL = SSQL + GetQueryPengeluaranPembiayaan(_pTahun, _pIDDInas, _pTahap, lstSKPD);
                        break;
                }

                //SSQL = GetQueryPendapatan(_pTahun, _pIDDInas);
                //SSQL = SSQL + " UNION " + GetQueryBelanjaTidakLangsung(_pTahun, _pIDDInas);

                //SSQL = SSQL + " UNION " + GetQueryBelanjaLangsung(_pTahun, _pIDDInas);
                //SSQL = SSQL + " ORDER BY XGroup,KElompok,IDURusan,A.IDProgram, A.IDkegiatan, A.IIDRekening";
                SSQL = SSQL + " ORDER BY XGroup,KElompok,IDURusan,IDProgram, IDkegiatan, IIDRekening";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new AnggaranKasTW()
                                {


                                    Kelompok = DataFormat.GetSingle(dr["Kelompok"]),
                                    Anggaran = DataFormat.GetDecimal(dr["Anggaran"]).ToRupiahInReport(),
                                    Bold = DataFormat.GetSingle(dr["Bold"]),
                                    Sifat = DataFormat.GetSingle(dr["Sifat"]),
                                    //IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    //IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    //IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    //IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    Kode = GetKode(DataFormat.GetInteger(dr["IDUrusan"]), _pIDDInas, DataFormat.GetInteger(dr["IDProgram"]), DataFormat.GetInteger(dr["IDkegiatan"]), DataFormat.GetLong(dr["IIDRekening"]), true),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Level = DataFormat.GetSingle(dr["Level"]),
                                    Tw1 = DataFormat.GetDecimal(dr["TW1"]).ToRupiahInReport(),
                                    Tw2 = DataFormat.GetDecimal(dr["TW2"]).ToRupiahInReport(),
                                    Tw3 = DataFormat.GetDecimal(dr["TW3"]).ToRupiahInReport(),
                                    Tw4 = DataFormat.GetDecimal(dr["TW4"]).ToRupiahInReport()

                                }).ToList();
                    }
                }
                _isError = false;
                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }


        }
        public List<RekapAnggaranKas> GetRekapPerTriwulan(int _Tahun, int _IDDInas, int Tahap, List<SKPD> lstSKPD = null)
        {

            string strDinas = "(";

            if (lstSKPD == null)
            {
                strDinas = strDinas + _IDDInas.ToString() + ")";
            }
            else
            {

                foreach (SKPD d in lstSKPD)
                {
                    strDinas = strDinas + d.ID.ToString() + ",";
                }
                strDinas = strDinas + "99)";

            }

            List<RekapAnggaranKas> _lst = new List<RekapAnggaranKas>();
            try
            {
                //SSQL="SELECT 1 as No, 'Pendapatan' as Nama, SUM(cBulan1, cBulan2, 

                SSQL = "SELECT 1 as No ,'Pendapatan' as Nama,SUM(cBulan1 + cBulan2+cBulan3) as Triwulan1,SUM(cBulan4 + cBulan5 + cBulan6) as Triwulan2, SUM(cBulan7 + cBulan8 + cBulan9) as Triwulan3 ,SUM(cBulan10 + cBulan11 + cBulan12) as Triwulan4  " +
                        " from tAnggaranKas where iTahun =" + _Tahun.ToString() + " AND IDDInas in " + strDinas + " AND IIDRekening like '4%' and iTahap = " + Tahap.ToString();

                SSQL = SSQL + " UNION ALL SELECT 2 as No ,'Belanja Tidak Langsung' as Nama,SUM(cBulan1 + cBulan2+cBulan3) as Triwulan1,SUM(cBulan4 + cBulan5 + cBulan6) as Triwulan2, SUM(cBulan7 + cBulan8 + cBulan9) as Triwulan3 ,SUM(cBulan10 + cBulan11 + cBulan12) as Triwulan4  " +
                        " from tAnggaranKas where iTahun =" + _Tahun.ToString() + " AND IDDInas in " + strDinas + " AND IIDRekening like '51%'  and iTahap = " + Tahap.ToString();
                SSQL = SSQL + " UNION ALL  SELECT 3 as No ,'Belanja Langsung' as Nama,SUM(cBulan1 + cBulan2+cBulan3) as Triwulan1,SUM(cBulan4 + cBulan5 + cBulan6) as Triwulan2, SUM(cBulan7 + cBulan8 + cBulan9) as Triwulan3 ,SUM(cBulan10 + cBulan11 + cBulan12) as Triwulan4  " +
                                        " from tAnggaranKas where iTahun =" + _Tahun.ToString() + " AND IDDInas in " + strDinas + " AND IIDRekening like '52%'  and iTahap = " + Tahap.ToString();
                SSQL = SSQL + " UNION ALL   SELECT 4 as No ,'Penerimaan Pembiayaan' as Nama,SUM(cBulan1 + cBulan2+cBulan3) as Triwulan1,SUM(cBulan4 + cBulan5 + cBulan6) as Triwulan2, SUM(cBulan7 + cBulan8 + cBulan9) as Triwulan3 ,SUM(cBulan10 + cBulan11 + cBulan12) as Triwulan4  " +
                                        " from tAnggaranKas where iTahun =" + _Tahun.ToString() + " AND IDDInas in " + strDinas + " AND IIDRekening like '61%' and iTahap = " + Tahap.ToString();
                SSQL = SSQL + " UNION ALL  SELECT 5 as No ,'Pengeluaran Pembiayaan' as Nama,SUM(cBulan1 + cBulan2+cBulan3) as Triwulan1,SUM(cBulan4 + cBulan5 + cBulan6) as Triwulan2, SUM(cBulan7 + cBulan8 + cBulan9) as Triwulan3 ,SUM(cBulan10 + cBulan11 + cBulan12) as Triwulan4  " +
                                        " from tAnggaranKas where iTahun =" + _Tahun.ToString() + " AND IDDInas in " + strDinas + " AND IIDRekening like '62%' and iTahap = " + Tahap.ToString();

                // SSQL = SSQL + " UNION ALL  SELECT 9 as No ,'Jumlah' as Nama,SUM(cBulan1 + cBulan2+cBulan3) as Triwulan1,SUM(cBulan4 + cBulan5 + cBulan6) as Triwulan2, SUM(cBulan7 + cBulan8 + cBulan9) as Triwulan3 ,SUM(cBulan10 + cBulan11 + cBulan12) as Triwulan4  " +
                //                        " from tAnggaranKas where iTahun =" + _Tahun.ToString() + " AND IDDInas =" + _IDDInas.ToString();

                SSQL = SSQL + " ORDER BY No";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RekapAnggaranKas()
                                {
                                    No = DataFormat.GetInteger(dr["No"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Triwulan1 = DataFormat.GetDecimal(dr["Triwulan1"]).ToRupiahInReport(),
                                    Triwulan2 = DataFormat.GetDecimal(dr["Triwulan2"]).ToRupiahInReport(),
                                    Triwulan3 = DataFormat.GetDecimal(dr["Triwulan3"]).ToRupiahInReport(),
                                    Triwulan4 = DataFormat.GetDecimal(dr["Triwulan4"]).ToRupiahInReport(),
                                    Jumlah = (DataFormat.GetDecimal(dr["Triwulan1"]) + DataFormat.GetDecimal(dr["Triwulan2"]) + DataFormat.GetDecimal(dr["Triwulan3"]) + DataFormat.GetDecimal(dr["Triwulan4"])).ToRupiahInReport(),
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
        private string GetQrySUmBasedBatas(int _pBatas)
        {
            switch (_pBatas)
            {
                case 1:
                    return " Sum(C.cBulan1)";
                //  break;
                case 2:
                    return " Sum(C.cBulan1+C.cBulan2)";
                // break;
                case 3:
                    return " Sum(C.cBulan1+C.cBulan2+C.cBulan3)";
                //break;
                case 4:
                    return " Sum(C.cBulan1+C.cBulan2+C.cBulan3+C.cBulan4)";
                // break;
                case 5:
                    return " Sum(C.cBulan1+C.cBulan2+C.cBulan3+C.cBulan4+C.cBulan5)";
                // break;
                case 6:
                    return " Sum(C.cBulan1+C.cBulan2+C.cBulan3+C.cBulan4+C.cBulan5+C.cBulan6)";
                // break;
                case 7:
                    return " Sum(C.cBulan1+C.cBulan2+C.cBulan3+C.cBulan4+C.cBulan5+C.cBulan6+C.cBulan7)";
                //break;
                case 8:
                    return " Sum(C.cBulan1+C.cBulan2+C.cBulan3+C.cBulan4+C.cBulan5+C.cBulan6+C.cBulan7+C.cBulan8)";
                //break;
                case 9:
                    return " Sum(C.cBulan1+C.cBulan2+C.cBulan3+C.cBulan4+C.cBulan5+C.cBulan6+C.cBulan7+C.cBulan8 +C.cBulan9)";
                //break;
                case 10:
                    return " Sum(C.cBulan1+C.cBulan2+C.cBulan3+C.cBulan4+C.cBulan5+C.cBulan6+C.cBulan7+C.cBulan8 +C.cBulan9+C.cBulan10)";
                //break;
                case 11:
                    return " Sum(C.cBulan1+C.cBulan2+C.cBulan3+C.cBulan4+C.cBulan5+C.cBulan6+C.cBulan7+C.cBulan8 +C.cBulan9+C.cBulan10+C.cBulan11)";
                //break;
                default:
                    return " Sum(C.cBulan1+C.cBulan2+C.cBulan3+C.cBulan4+C.cBulan5+C.cBulan6+C.cBulan7+C.cBulan8 +C.cBulan9+C.cBulan10+C.cBulan11+C.cBulan12)";
                //break;

            }
        }
        public List<AnggaranKas> GetStatusJumlahKas(int _pBatas, int _pTahun, int _pIDDInas)
        {
            List<AnggaranKas> _lst = new List<AnggaranKas>();
            try
            {
                string sumBaseBatas = GetQrySUmBasedBatas(_pBatas);

                SSQL = "Select A.IDDInas, A.idUrusan,A.IDProgram,A.IDKegiatan, 0 as IIDRekening," + sumBaseBatas + " as Anggaran from tKegiatan_A A inner join tAnggaranKas B On A.iTahun = B.iTahun AND A.iddinas= B.IDDInas and A.idurusan = B.IDUrusan and A.IDprogram =B.IDProgram and a.idkegiatan= B.IDKegiatan   " +
                        " where A.iTahun = " + _pTahun.ToString() + " AND A.IDDInas=" + _pIDDInas.ToString() + "  Group BY A.IDDInas, A.idUrusan,A.IDProgram,A.IDKegiatan ,  A.sNama";

                SSQL = SSQL + " UNION ALL Select B.IDDInas, B.idUrusan,B.IDProgram,B.IDKegiatan, B.IIDRekening as IIDRekening," + sumBaseBatas + " as Anggaran from tAnggaranKas B " +
                     " where B.iTahun = " + _pTahun.ToString() + " AND B.IDDInas=" + _pIDDInas.ToString() +
                      "  Group BY B.IDDInas, B.idUrusan,B.IDProgram,B.IDKegiatan, B.IIDRekening " +
                     " order by IDDInas, idUrusan,IDProgram,IDKegiatan, IIDRekening ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new AnggaranKas()
                                {
                                    Anggaran = DataFormat.GetDecimal(dr["Anggaran"]),
                                    Bold = DataFormat.GetSingle(dr["Bold"]),
                                    Sifat = DataFormat.GetSingle(dr["Sifat"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    Kode = GetKode(DataFormat.GetInteger(dr["IDUrusan"]), _pIDDInas, DataFormat.GetInteger(dr["IDProgram"]), DataFormat.GetInteger(dr["IDkegiatan"]), DataFormat.GetLong(dr["IIDRekening"]), true),
                                    Nama = DataFormat.GetString(dr["Nama"])



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

        public void UpdateKode()
        {

            SSQL = "UPDATE tSPP SET IDDInas = (btKodekategori * 1000000) + (btKodeUrusan * 10000) + (btKodeSKPD * 100) ";
            _dbHelper.ExecuteNonQuery(SSQL);


            SSQL = "UPDATE tSPPRekening SET IDUrusan = (btKodekategoriPelaksana * 100) + btKodeUrusanPelaksana ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "UPDATE tSPPRekening SET IDProgram = (btKodekategoriPelaksana * 10000) + (btKodeUrusanPelaksana * 100) + btIDProgram ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "UPDATE tSPPRekening SET IDkegiatan = (btKodekategoriPelaksana * 10000000) + (btKodeUrusanPelaksana * 100000) + (btIDProgram *1000) + btIDkegiatan ";
            _dbHelper.ExecuteNonQuery(SSQL);


            SSQL = "UPDATE tPanjar SET IDDInas = (btKodekategori * 1000000) + (btKodeUrusan * 10000) + (btKodeSKPD * 100) ";
            _dbHelper.ExecuteNonQuery(SSQL);


            SSQL = "UPDATE tPanjar SET IDUrusan = (btKodekategoriPelaksana * 100) + btKodeUrusanPelaksana ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "UPDATE tPanjar SET IDProgram = (btKodekategoriPelaksana * 10000) + (btKodeUrusanPelaksana * 100) + btIDProgram ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "UPDATE tPanjar SET IDkegiatan = (btKodekategoriPelaksana * 10000000) + (btKodeUrusanPelaksana * 100000) + (btIDProgram *1000) + btIDkegiatan ";
            _dbHelper.ExecuteNonQuery(SSQL);


            SSQL = "UPDATE tSetor SET IDDInas = (btKodekategori * 1000000) + (btKodeUrusan * 10000) + (btKodeSKPD * 100) ";
            _dbHelper.ExecuteNonQuery(SSQL);


            SSQL = "UPDATE tSetor SET IDUrusan = (btKodekategoriPelaksana * 100) + btKodeUrusanPelaksana ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "UPDATE tSetor SET IDProgram = (btKodekategoriPelaksana * 10000) + (btKodeUrusanPelaksana * 100) + btIDProgram ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "UPDATE tSetor SET IDkegiatan = (btKodekategoriPelaksana * 10000000) + (btKodeUrusanPelaksana * 100000) + (btIDProgram *1000) + btIDkegiatan ";
            _dbHelper.ExecuteNonQuery(SSQL);

        }

        public List<AnggaranKas> GetProgram(int _pBatas, int _pTahun, int _pIDDInas, List<SKPD> lstSKPD = null)
        {
            List<AnggaranKas> _lst = new List<AnggaranKas>();

            string sDinas;
            sDinas = GetStringDinas(_pIDDInas, lstSKPD);

            UpdateKode();
            try
            {
                string sumBaseBatas = GetQrySUmBasedBatas(_pBatas);

                SSQL = " Select 1 as Bold, A.IDDInas, A.idUrusan,A.IDProgram,0 as IDKegiatan, 0 as IIDRekening,  A.sNamaProgram as Nama, SUM(B.cDPA) as Anggaran,0 as AnggaranKas, 0 as Realisasi from tPrograms_A A inner join tAnggaranRekening_A B On A.iTahun = B.iTahun AND A.iddinas= B.IDDInas and A.idurusan = B.IDUrusan and A.IDprogram =B.IDProgram    " +
                        " where A.iTahun = " + _pTahun.ToString() + " AND A.IDDInas=" + _pIDDInas.ToString() + "  Group BY A.IDDInas, A.idUrusan,A.IDProgram, A.sNamaProgram" +
                        " order by IDDInas, idUrusan,IDProgram,IDKegiatan, IIDRekening ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new AnggaranKas()
                                {
                                    Anggaran = DataFormat.GetDecimal(dr["Anggaran"]),
                                    Bold = DataFormat.GetSingle(dr["Bold"]),
                                    // Sifat = DataFormat.GetSingle(dr["Sifat"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    Bulan1 = DataFormat.GetDecimal(dr["AnggaranKas"]),
                                    Bulan2 = DataFormat.GetDecimal(dr["Realisasi"]),
                                    Kode = GetKode(DataFormat.GetInteger(dr["IDUrusan"]), _pIDDInas, DataFormat.GetInteger(dr["IDProgram"]), DataFormat.GetInteger(dr["IDkegiatan"]), DataFormat.GetLong(dr["IIDRekening"]), true),
                                    Nama = DataFormat.GetString(dr["Nama"])



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

        public List<AnggaranKas> GetAnggaran(int _pBatas, int _pTahun, int _pIDDInas, List<SKPD> lstSKPD = null)
        {
            List<AnggaranKas> _lst = new List<AnggaranKas>();

            UpdateKode();
            try
            {

                string sDinas;
                sDinas = GetStringDinas(_pIDDInas, lstSKPD);

                string sumBaseBatas = GetQrySUmBasedBatas(_pBatas);

                //SSQL = " Select 1 as Bold, A.IDDInas, A.idUrusan,A.IDProgram,0 as IDKegiatan, 0 as IIDRekening,  A.sNamaProgram as Nama, SUM(B.cDPA) as Anggaran," + sumBaseBatas + " as AnggaranKas, SUM(D.cJumlah) as Realisasi from tPrograms_A A inner join tAnggaranRekening_A B On A.iTahun = B.iTahun AND A.iddinas= B.IDDInas and A.idurusan = B.IDUrusan and A.IDprogram =B.IDProgram   " +
                //      " INNER JOIN tAnggaranKas C ON  A.iTahun = C.iTahun AND A.iddinas= C.IDDInas and A.idurusan = C.IDUrusan and A.IDprogram =C.IDProgram " +
                //     " INNER JOIN REALISASI04AK D ON  A.iTahun = D.iTahun AND A.iddinas= D.IDDInas and A.idurusan = D.IDUrusan and A.IDprogram =D.IDProgram " +
                //       " where A.iTahun = " + _pTahun.ToString() + " AND A.IDDInas=" + _pIDDInas.ToString() + "  Group BY A.IDDInas, A.idUrusan,A.IDProgram, A.sNamaProgram";

                // string sNamaKolom;

                SSQL = " Select 1 as Bold, A.IDDInas, A.idUrusan,A.IDProgram,A.IDKegiatan, 0 as IIDRekening,  A.sNama as Nama,SUM(B.cDPA) as Anggaran," + sumBaseBatas + " as AnggaranKas,0 as Realisasi from tKegiatan_A A inner join tAnggaranRekening_A B On A.iTahun = B.iTahun AND A.iddinas= B.IDDInas and A.idurusan = B.IDUrusan and A.IDprogram =B.IDProgram and a.idkegiatan= B.IDKegiatan  AND a.btJenis= B.btJenis " +
                      " INNER JOIN tAnggaranKas C ON  A.iTahun = C.iTahun AND A.iddinas= C.IDDInas and A.idurusan = C.IDUrusan and A.IDprogram =C.IDProgram and a.idkegiatan= C.IDKegiatan  AND a.btJenis= C.btJenis" +
                      " where A.iTahun = " + _pTahun.ToString() + " AND A.IDDInas in " + sDinas + "  Group BY A.IDDInas, A.idUrusan,A.IDProgram,A.IDKegiatan ,  A.sNama";

                SSQL = SSQL + " UNION ALL Select 2 as Bold, A.IDDInas, A.idUrusan,A.IDProgram,A.IDKegiatan, A.IIDRekening,  B.sNamaRekening as Nama,SUM(A.cDPA) as Anggaran," + sumBaseBatas + " as AnggaranKas,0 as Realisasi  from tAnggaranRekening_A A inner join mRekening B On A.IIDRekening= B.IIDRekening " +
                     " INNER JOIN tAnggaranKas C ON  A.iTahun = C.iTahun AND A.iddinas= C.IDDInas and A.idurusan = C.IDUrusan and A.IDprogram =C.IDProgram and a.idkegiatan= C.IDKegiatan and A.IIDRekening= C.IIDRekening and A.btJenis= C.btjenis" +
                      " where A.iTahun = " + _pTahun.ToString() + " AND A.IDDInas  in " + sDinas + "  Group BY A.IDDInas, A.idUrusan,A.IDProgram,A.IDKegiatan, A.IIDRekening,  B.sNamaRekening " +
                      " order by IDDInas, idUrusan,IDProgram,IDKegiatan, IIDRekening ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new AnggaranKas()
                                {
                                    Anggaran = DataFormat.GetDecimal(dr["Anggaran"]),
                                    Bold = DataFormat.GetSingle(dr["Bold"]),
                                    // Sifat = DataFormat.GetSingle(dr["Sifat"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    Bulan1 = DataFormat.GetDecimal(dr["AnggaranKas"]),
                                    Bulan2 = DataFormat.GetDecimal(dr["Realisasi"]),
                                    Kode = GetKode(DataFormat.GetInteger(dr["IDUrusan"]), _pIDDInas, DataFormat.GetInteger(dr["IDProgram"]), DataFormat.GetInteger(dr["IDkegiatan"]), DataFormat.GetLong(dr["IIDRekening"]), true),
                                    Nama = DataFormat.GetString(dr["Nama"])



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

        public List<AnggaranKas> GetRealisasi(int _pTahun, int _pIDDInas, List<SKPD> lstSKPD = null)
        {
            List<AnggaranKas> _lst = new List<AnggaranKas>();
            try
            {

                string sDinas;
                sDinas = GetStringDinas(_pIDDInas, lstSKPD);

                SSQL = " Select 1 as Bold, A.IDDInas, A.idUrusan,A.IDProgram,A.IDKegiatan, 0 as IIDRekening,  A.sNama as Nama, SUM(D.cJumlah) as Realisasi from tKegiatan_A A INNER JOIN REALISASI04AK D ON  A.iTahun = D.iTahun AND A.iddinas= D.IDDInas and A.idurusan = D.IDUrusan and A.IDprogram =D.IDProgram and a.idkegiatan= D.IDKegiatan " +
                        " where A.iTahun = " + _pTahun.ToString() + " AND A.IDDInas  in " + sDinas + "  Group BY A.IDDInas, A.idUrusan,A.IDProgram,A.IDKegiatan ,  A.sNama";

                SSQL = SSQL + " UNION ALL Select 2 as Bold, A.IDDInas, A.idUrusan,A.IDProgram,A.IDKegiatan, A.IIDRekening,  B.sNamaRekening as Nama,SUM(D.cJumlah) as Realisasi  from tAnggaranRekening_A A inner join mRekening B On A.IIDRekening= B.IIDRekening " +
                        " INNER JOIN REALISASI04AK D ON  A.iTahun = D.iTahun AND A.iddinas= D.IDDInas and A.idurusan = D.IDUrusan and A.IDprogram =D.IDProgram and a.idkegiatan= D.IDKegiatan AND A.IIDRekening = D.IIDRekening " +
                     " where A.iTahun = " + _pTahun.ToString() + " AND A.IDDInas  in " + sDinas + "  Group BY A.IDDInas, A.idUrusan,A.IDProgram,A.IDKegiatan, A.IIDRekening,  B.sNamaRekening " +
                      " order by IDDInas, idUrusan,IDProgram,IDKegiatan, IIDRekening ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new AnggaranKas()
                                {
                                    //  Anggaran = DataFormat.GetDecimal(dr["Anggaran"]),
                                    Bold = DataFormat.GetSingle(dr["Bold"]),
                                    // Sifat = DataFormat.GetSingle(dr["Sifat"]),

                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    Bulan2 = DataFormat.GetDecimal(dr["Realisasi"])




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
        private string GetKode(int _idURusan, int _iddinas, int _idProgram, int _idKegiatan, long idRekening, bool isLengkap)
        {
            string s = "";

            //s= _idURusan.ToKodeUrusan();
            if (_idProgram > 0)
            {
                s = "5.2";
                s = s + "." + _idURusan.ToKodeUrusan() + "." + _idProgram.ToSimpleKodeProgram();
            }
            else
            {
                if (idRekening > 62000000)
                {
                    s = "6.2.00.00";
                }
                if (idRekening > 61000000 && idRekening < 62000000)
                {
                    s = "6.1.00.00";
                }
                if (idRekening > 51000000 && idRekening < 52000000)
                {
                    s = "5.1." + "00.00";
                }
                if (idRekening < 50000000)
                {
                    s = "4.1.00.00";
                }


            }

            if (_idKegiatan > 0)
                s = s + "." + _idKegiatan.ToSimpleKodeKegiatan();

            if (idRekening > 0)
            {
                if (isLengkap == false)
                {
                    s = idRekening.ToKodeRekening(m_ProfileRekening);
                }
                else
                {
                    s = s + "." + idRekening.ToKodeRekening(m_ProfileRekening);
                }
            }


            return s;

        }
        private string GetKodeAnggaranKas(int _idURusan, int _iddinas, int _idProgram, int _idKegiatan, long _idSubKegiatan, long idRekening, bool isLengkap)
        {
            string s = "";

            ////s= _idURusan.ToKodeUrusan();
            //if (_idProgram > 0)
            //{
            //    s = "5.2";
            //    s = s + "." + _idURusan.ToKodeUrusan() + "." + _idProgram.ToSimpleKodeProgram();
            //}
            //else
            //{
            //    if (idRekening > 62000000)
            //    {
            //        s = "6.2.00.00";
            //    }
            //    if (idRekening > 61000000 && idRekening < 62000000)
            //    {
            //        s = "6.1.00.00";
            //    }
            //    if (idRekening > 51000000 && idRekening < 52000000)
            //    {
            //        s = "5.1." + "00.00";
            //    }
            //    if (idRekening < 50000000)
            //    {
            //        s = "4.1.00.00";
            //    }


            //}
            if (_idProgram>0)
                s= _idProgram.ToSimpleKodeProgram();

            if (_idKegiatan > 0)
                s = _idProgram.ToSimpleKodeProgram() + "." + _idKegiatan.ToSimpleKodeKegiatan();
            if (_idSubKegiatan > 0)
                s = _idProgram.ToSimpleKodeProgram() + "." + _idKegiatan.ToSimpleKodeKegiatan() + "." + _idSubKegiatan.ToString().Substring(8);//.ToSimpleKodeKegiatan();
            if (idRekening > 0)
            {
                if (isLengkap == false)
                {
                    s = idRekening.ToKodeRekening(m_ProfileRekening);
                }
                else
                {
                    s = s + "." + idRekening.ToKodeRekening(m_ProfileRekening);
                }
            }


            return s;

        }
        private string GetQueryPendapatan(int _pTahun, int _pDinas, int pTahap, List<SKPD> lstSKPD = null)
        {
            // isnull(SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
            // " isnull(SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, isnull(SUM(cBulan7+cBulan8+cBulan9) as Tw3, isnull(SUM(cBUlan10+cBulan11+cBUlan12) as Tw4 


            string sDinas;
            sDinas = GetStringDinas(_pDinas, lstSKPD);

            string sRet = "";

            //sRet = "Select  1 as XGroup,1 as Kelompok,1 as Bold, 1 as Sifat, 0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, LEFT(B.IIDRekening,1) as Kode ,  0 as idsubkegiatan, A.IIDRekening, LEFT(B.IIDRekening,1) as IDX,'Alokasi Pendapatan ' AS NAMA, isnull(SUM(B.cJumlahOlah) as Anggaran, isnull(SUM(cBulan1) as Januari, isnull(SUM (cBulan2) as Februari, isnull(SUM(cBulan3) as Maret, " +
            //            "isnull(SUM(cBulan4) as APril, isnull(SUM(cBUlan5) as Mei, isnull(SUM(cBUlan6)as Juni, isnull(SUM(cBulan7) as Juli, isnull(SUM(cBulan8) as Agustus, " +
            //            "isnull(SUM(cBulan9) as September, isnull(SUM(cBUlan10) as Oktober, isnull(SUM (cBulan11) as November , isnull(SUM (cBUlan12) as Desember" +
            //                " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,1)= LEFT(B.IIDRekening,1) " +
            //                " WHERE A.btRoot=1 AND A.IIDRekening like '4%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas in " + sDinas +
            //                " AND b.iTahap =" + pTahap.ToString() + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,1)" +


            //                " UNION  Select 1 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, LEFT( B.IIDRekening,2) as Kode ,  0 as idsubkegiatan, A.IIDRekening, LEFT(B.IIDRekening,2) as IDX,A.sNamaRekening AS NAMA, isnull(SUM(B.cJumlahOlah) as Anggaran, isnull(SUM(cBulan1) as Januari, isnull(SUM (cBulan2) as Februari, isnull(SUM(cBulan3) as Maret, " +
            //                " isnull(SUM(cBulan4) as APril, isnull(SUM(cBUlan5) as Mei, isnull(SUM(cBUlan6)as Juni, isnull(SUM(cBulan7) as Juli, isnull(SUM(cBulan8) as Agustus, " +
            //                " isnull(SUM(cBulan9) as September, isnull(SUM(cBUlan10) as Oktober, isnull(SUM (cBulan11) as November , isnull(SUM (cBUlan12) as Desember" +
            //                " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,2)= LEFT(B.IIDRekening,2) " +
            //                " WHERE A.btRoot=2 AND A.IIDRekening like '4%'  AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas in " + sDinas + "  AND b.iTahap =" + pTahap.ToString() + "  GROUP BY A.IIDRekening, LEFT(B.IIDRekening,2),A.sNamaRekening " +


            //                " UNION  Select 1 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat, 0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  LEFT(B.IIDRekening,3) as Kode ,  0 as idsubkegiatan, A.IIDRekening, LEFT(B.IIDRekening,3) as IDX,A.sNamaRekening AS NAMA,  " +
            //                " isnull(SUM(B.cJumlahOlah) as Anggaran, isnull(SUM(cBulan1) as Januari, isnull(SUM (cBulan2) as Februari, isnull(SUM(cBulan3) as Maret, " +
            //                " isnull(SUM(cBulan4) as APril, isnull(SUM(cBUlan5) as Mei, isnull(SUM(cBUlan6)as Juni, isnull(SUM(cBulan7) as Juli, isnull(SUM(cBulan8) as Agustus,  " +
            //                " isnull(SUM(cBulan9) as September, isnull(SUM(cBUlan10) as Oktober, isnull(SUM (cBulan11) as November , isnull(SUM (cBUlan12) as Desember " +
            //                " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,3)= LEFT(B.IIDRekening,3)  " +
            //                " WHERE A.btRoot=3 AND A.IIDRekening like '4%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas in " + sDinas + " AND b.iTahap =" + pTahap.ToString() + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,3),A.sNamaRekening	 " +

            //              " UNION  Select 1 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  LEFT(B.IIDRekening,5) as Kode , 0 as idsubkegiatan, A.IIDRekening, LEFT(B.IIDRekening,5) as IDX,A.sNamaRekening AS NAMA,  " +
            //           "  isnull(SUM(B.cJumlahOlah) as Anggaran, isnull(SUM(cBulan1) as Januari, isnull(SUM (cBulan2) as Februari, isnull(SUM(cBulan3) as Maret, " +
            //            " isnull(SUM(cBulan4) as APril, isnull(SUM(cBUlan5) as Mei, isnull(SUM(cBUlan6)as Juni, isnull(SUM(cBulan7) as Juli, isnull(SUM(cBulan8) as Agustus,  " +
            //                " isnull(SUM(cBulan9) as September, isnull(SUM(cBUlan10) as Oktober, isnull(SUM (cBulan11) as November , isnull(SUM (cBUlan12) as Desember " +
            //                " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,5)= LEFT(B.IIDRekening,5)  " +
            //                " WHERE A.btRoot=4 AND A.IIDRekening like '4%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas in " + sDinas + " AND b.iTahap =" + pTahap.ToString() + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,5),A.sNamaRekening " +

            //                " UNION  Select 1 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  B.IIDRekening as Kode , 0 as idsubkegiatan, A.IIDRekening, B.IIDRekening as IDX,A.sNamaRekening AS NAMA,  " +
            //  " isnull(SUM(B.cJumlahOlah) as Anggaran, isnull(SUM(cBulan1) as Januari, isnull(SUM (cBulan2) as Februari, isnull(SUM(cBulan3) as Maret, " +
            //   " isnull(SUM(cBulan4) as APril, isnull(SUM(cBUlan5) as Mei, isnull(SUM(cBUlan6)as Juni, isnull(SUM(cBulan7) as Juli, isnull(SUM(cBulan8) as Agustus,  " +
            //   " isnull(SUM(cBulan9) as September, isnull(SUM(cBUlan10) as Oktober, isnull(SUM (cBulan11) as November , isnull(SUM (cBUlan12) as Desember " +
            //   " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON A.IIDRekening = B.IIDRekening " +
            //   " WHERE A.btRoot=5 AND A.IIDRekening like '4%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas in " + sDinas + " AND b.iTahap =" + pTahap.ToString() + " GROUP BY A.IIDRekening, B.IIDRekening,A.sNamaRekening " +

            //            " UNION  Select 1 as XGroup,3 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode,  0 as idsubkegiatan, 0 as IIDRekening, 0 as IDX,'Jumlah Pendapatan ' AS NAMA, isnull(SUM(B.cJumlahOlah) as Anggaran, isnull(SUM(cBulan1) as Januari, isnull(SUM (cBulan2) as Februari, isnull(SUM(cBulan3) as Maret, " +
            //            " isnull(SUM(cBulan4) as APril, isnull(SUM(cBUlan5) as Mei, isnull(SUM(cBUlan6)as Juni, isnull(SUM(cBulan7) as Juli, isnull(SUM(cBulan8) as Agustus,  " +
            //                " isnull(SUM(cBulan9) as September, isnull(SUM(cBUlan10) as Oktober, isnull(SUM (cBulan11) as November , isnull(SUM (cBUlan12) as Desember " +
            //                " FROM vwAnggaranKasDanAnggaran B  " +
            //                " WHERE B.IIDRekening like '4%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.iTahap =" + pTahap.ToString() + " AND B.IDDInas  in " + sDinas  +
            //    " UNION Select   1 as XGroup,4 as Kelompok,1 as Bold, 2 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  '' as Kode, 0 as idsubkegiatan, 0 as IIDRekening, 0 as IDX,'JUMLAH ALOKASI YANG TERSEDIA UNTUK PENGELUARAN PER TRIWULAN ' AS NAMA, isnull(SUM(B.cJumlahOlah) as Anggaran, isnull(SUM(cBulan1+ cBulan2+cBulan3) as Januari, 0 as Februari, 0 as Maret, " +
            //        " isnull(SUM(cBulan4+cBulan5+cBulan6) as April, 0 as Mei, 0 as Juni, isnull(SUM(cBulan7+cBulan8+cBulan9) as Juli, 0 as Agustus,  " +
            //            " 0 as September, isnull(SUM(cBUlan10+cBUlan11+cBUlan12) as Oktober, 0 as November , 0 as Desember " +
            //            " FROM vwAnggaranKasDanAnggaran B " +
            //            " WHERE B.IIDRekening like '4%' AND B.iTAhun=" + _pTahun.ToString() + " AND b.iTahap =" + pTahap.ToString() + " AND B.IDDInas  in " + sDinas;

            sRet = " Select  1 as XGroup,1 as Kelompok,1 as Bold, 3 as Sifat,0 AS IDDInas, 0 as IDUrusan, " +
    " 0 as IDProgram, 0 as IDkegiatan,  0 as Kode ,0 as idsubkegiatan , 0 as IIDRekening, 0 as IDX,'ALOKASI PENDAPATAN DAN PENERIMAAN PEMBIAYAAN' AS NAMA, " +
    " 0 as   Anggaran,0 as   Januari, 0 as   Februari," +
    " 0 as   Maret, " +
              " 0 as   APril, 0 as   Mei, " +
   "0 as Juni,  0 as   Juli, 0 as   Agustus, " +
              " 0 as   September, 0 as   Oktober, 0 as   November , 0 as   Desember" +
              " FROM vwAnggaranKasDanAnggaran B ";
              

            sRet = sRet + " UNION Select  1 as XGroup,1 as Kelompok,1 as Bold, 1 as Sifat, 0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, LEFT(B.IIDRekening,1) as Kode ,  0 as idsubkegiatan, A.IIDRekening, LEFT(B.IIDRekening,1) as IDX,'ALOKASI PENDAPATAN ' AS NAMA, isnull(SUM(B.cJumlahOlah) ,0) as  Anggaran, isnull(SUM(cBulan1) ,0) as  Januari, isnull(SUM (cBulan2) ,0) as  Februari, isnull(SUM(cBulan3) ,0) as  Maret, " +
                        "isnull(SUM(cBulan4) ,0) as  APril, isnull(SUM(cBUlan5) ,0) as  Mei, isnull(SUM(cBUlan6),0) as  Juni, isnull(SUM(cBulan7) ,0) as  Juli, isnull(SUM(cBulan8) ,0) as  Agustus, " +
                        "isnull(SUM(cBulan9) ,0) as  September, isnull(SUM(cBUlan10) ,0) as  Oktober, isnull(SUM (cBulan11) ,0) as  November , isnull(SUM (cBUlan12) ,0) as  Desember" +
                            " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,1)= LEFT(B.IIDRekening,1) " +
                            " WHERE A.btRoot=1 AND A.IIDRekening like '4%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas in " + sDinas +
                            " AND b.iTahap =" + pTahap.ToString() + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,1)" +


                            " UNION  Select 1 as XGroup,2 as Kelompok,0 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, LEFT( B.IIDRekening,2) as Kode ,  0 as idsubkegiatan, A.IIDRekening, LEFT(B.IIDRekening,2) as IDX,A.sNamaRekening AS NAMA, isnull(SUM(B.cJumlahOlah) ,0) as  Anggaran, isnull(SUM(cBulan1) ,0) as  Januari, isnull(SUM (cBulan2) ,0) as  Februari, isnull(SUM(cBulan3) ,0) as  Maret, " +
                            " isnull(SUM(cBulan4) ,0) as  APril, isnull(SUM(cBUlan5) ,0) as  Mei, isnull(SUM(cBUlan6),0) as  Juni, isnull(SUM(cBulan7) ,0) as  Juli, isnull(SUM(cBulan8) ,0) as  Agustus, " +
                            " isnull(SUM(cBulan9) ,0) as  September, isnull(SUM(cBUlan10) ,0) as  Oktober, isnull(SUM (cBulan11) ,0) as  November , isnull(SUM (cBUlan12) ,0) as  Desember" +
                            " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,2)= LEFT(B.IIDRekening,2) " +
                            " WHERE A.btRoot=2 AND A.IIDRekening like '4%'  AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas in " + sDinas + "  AND b.iTahap =" + pTahap.ToString() + "  GROUP BY A.IIDRekening, LEFT(B.IIDRekening,2),A.sNamaRekening " +


                            " UNION  Select 1 as XGroup,2 as Kelompok,0 as Bold, 1 as Sifat, 0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  LEFT(B.IIDRekening,3) as Kode ,  0 as idsubkegiatan, A.IIDRekening, LEFT(B.IIDRekening,3) as IDX,A.sNamaRekening AS NAMA,  " +
                            " isnull(SUM(B.cJumlahOlah) ,0) as  Anggaran, isnull(SUM(cBulan1) ,0) as  Januari, isnull(SUM (cBulan2) ,0) as  Februari, isnull(SUM(cBulan3) ,0) as  Maret, " +
                            " isnull(SUM(cBulan4) ,0) as  APril, isnull(SUM(cBUlan5) ,0) as  Mei, isnull(SUM(cBUlan6),0) as  Juni, isnull(SUM(cBulan7) ,0) as  Juli, isnull(SUM(cBulan8) ,0) as  Agustus,  " +
                            " isnull(SUM(cBulan9) ,0) as  September, isnull(SUM(cBUlan10) ,0) as  Oktober, isnull(SUM (cBulan11) ,0) as  November , isnull(SUM (cBUlan12) ,0) as  Desember " +
                            " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,3)= LEFT(B.IIDRekening,3)  " +
                            " WHERE A.btRoot=3 AND A.IIDRekening like '4%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas in " + sDinas + " AND b.iTahap =" + pTahap.ToString() + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,3),A.sNamaRekening	 " +

                          " UNION  Select 1 as XGroup,2 as Kelompok,0 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  LEFT(B.IIDRekening,5) as Kode , 0 as idsubkegiatan, A.IIDRekening, LEFT(B.IIDRekening,5) as IDX,A.sNamaRekening AS NAMA,  " +
                       "  isnull(SUM(B.cJumlahOlah) ,0) as  Anggaran, isnull(SUM(cBulan1) ,0) as  Januari, isnull(SUM (cBulan2) ,0) as  Februari, isnull(SUM(cBulan3) ,0) as  Maret, " +
                        " isnull(SUM(cBulan4) ,0) as  APril, isnull(SUM(cBUlan5) ,0) as  Mei, isnull(SUM(cBUlan6),0) as  Juni, isnull(SUM(cBulan7) ,0) as  Juli, isnull(SUM(cBulan8) ,0) as  Agustus,  " +
                            " isnull(SUM(cBulan9) ,0) as  September, isnull(SUM(cBUlan10) ,0) as  Oktober, isnull(SUM (cBulan11) ,0) as  November , isnull(SUM (cBUlan12) ,0) as  Desember " +
                            " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,5)= LEFT(B.IIDRekening,5)  " +
                            " WHERE A.btRoot=4 AND A.IIDRekening like '4%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas in " + sDinas + " AND b.iTahap =" + pTahap.ToString() + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,5),A.sNamaRekening " +

                            " UNION  Select 1 as XGroup,2 as Kelompok,0 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  B.IIDRekening as Kode , 0 as idsubkegiatan, A.IIDRekening, B.IIDRekening as IDX,A.sNamaRekening AS NAMA,  " +
              " isnull(SUM(B.cJumlahOlah) ,0) as  Anggaran, isnull(SUM(cBulan1) ,0) as  Januari, isnull(SUM (cBulan2) ,0) as  Februari, isnull(SUM(cBulan3) ,0) as  Maret, " +
               " isnull(SUM(cBulan4) ,0) as  APril, isnull(SUM(cBUlan5) ,0) as  Mei, isnull(SUM(cBUlan6),0) as  Juni, isnull(SUM(cBulan7) ,0) as  Juli, isnull(SUM(cBulan8) ,0) as  Agustus,  " +
               " isnull(SUM(cBulan9) ,0) as  September, isnull(SUM(cBUlan10) ,0) as  Oktober, isnull(SUM (cBulan11) ,0) as  November , isnull(SUM (cBUlan12) ,0) as  Desember " +
               " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON A.IIDRekening = B.IIDRekening " +
               " WHERE A.btRoot=5 AND A.IIDRekening like '4%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas in " + sDinas + " AND b.iTahap =" + pTahap.ToString() + " GROUP BY A.IIDRekening, B.IIDRekening,A.sNamaRekening " +

                        " UNION  Select 1 as XGroup,3 as Kelompok,0 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode,  0 as idsubkegiatan, 0 as IIDRekening, 0 as IDX,'JUMLAH PENDAPATAN ' AS NAMA, isnull(SUM(B.cJumlahOlah) ,0) as  Anggaran, isnull(SUM(cBulan1) ,0) as  Januari, isnull(SUM (cBulan2) ,0) as  Februari, isnull(SUM(cBulan3) ,0) as  Maret, " +
                        " isnull(SUM(cBulan4) ,0) as  APril, isnull(SUM(cBUlan5) ,0) as  Mei, isnull(SUM(cBUlan6),0) as  Juni, isnull(SUM(cBulan7) ,0) as  Juli, isnull(SUM(cBulan8) ,0) as  Agustus,  " +
                            " isnull(SUM(cBulan9) ,0) as  September, isnull(SUM(cBUlan10) ,0) as  Oktober, isnull(SUM (cBulan11) ,0) as  November , isnull(SUM (cBUlan12) ,0) as  Desember " +
                            " FROM vwAnggaranKasDanAnggaran B  " +
                            " WHERE B.IIDRekening like '4%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.iTahap =" + pTahap.ToString() + " AND B.IDDInas  in " + sDinas +
                " UNION Select   1 as XGroup,4 as Kelompok,1 as Bold, 2 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  '' as Kode, 0 as idsubkegiatan, 0 as IIDRekening, 0 as IDX,'JUMLAH ALOKASI YANG TERSEDIA UNTUK PENGELUARAN PER TRIWULAN ' AS NAMA, isnull(SUM(B.cJumlahOlah) ,0) as  Anggaran, isnull(SUM(cBulan1+ cBulan2+cBulan3) ,0) as  Januari, 0 as  Februari, 0  as  Maret, " +
                    " isnull(SUM(cBulan4+cBulan5+cBulan6) ,0) as  April, 0  as  Mei, 0  as  Juni, isnull(SUM(cBulan7+cBulan8+cBulan9) ,0) as  Juli, 0  as  Agustus,  " +
                        " 0  as  September, isnull(SUM(cBUlan10+cBUlan11+cBUlan12) ,0) as  Oktober, 0  as  November , 0  as  Desember " +
                        " FROM vwAnggaranKasDanAnggaran B " +
                        " WHERE B.IIDRekening like '4%' AND B.iTAhun=" + _pTahun.ToString() + " AND b.iTahap =" + pTahap.ToString() + " AND B.IDDInas  in " + sDinas;





            return sRet.Replace("\t", "");

        }
        private string GetQueryPendapatanTW(int _pTahun, int _pDinas, int pTahap, List<SKPD> lstSKPD = null)
        {
            // SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
            //   " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4 

            string sRet = "";


            string sDinas;
            sDinas = GetStringDinas(_pDinas, lstSKPD);

            //sRet = "Select 2 as Level,  1 as XGroup,1 as Kelompok,1 as Bold, 1 as Sifat, 0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, LEFT(B.IIDRekening,1) as Kode , A.IIDRekening, LEFT(B.IIDRekening,1) as IDX,'Alokasi Pendapatan ' AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
            //    " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4 " +
            //                " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,1)= LEFT(B.IIDRekening,1) " +
            //                " WHERE A.btRoot=1 AND A.IIDRekening like '4%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas=" + _pDinas.ToString() +
            //                " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,1)" +


            sRet = "Select 2 as Level,1 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, LEFT( B.IIDRekening,2) as Kode , A.IIDRekening, LEFT(B.IIDRekening,2) as IDX,A.sNamaRekening AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
          " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                      " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,2)= LEFT(B.IIDRekening,2) " +
                      " WHERE B.iTahap=" + pTahap.ToString() + " AND  A.btRoot=2 AND A.IIDRekening like '4%'  and B.iTahap= " + pTahap.ToString() + "  AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas in " + sDinas + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,2),A.sNamaRekening " +


                      " UNION  Select 5 as Level, 1 as XGroup,2 as Kelompok,0 as Bold, 1 as Sifat, 0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  LEFT(B.IIDRekening,3) as Kode , A.IIDRekening, LEFT(B.IIDRekening,3) as IDX,A.sNamaRekening AS NAMA,  " +
                      " SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
          " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                      " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,3)= LEFT(B.IIDRekening,3)  " +
                      " WHERE  B.iTahap=" + pTahap.ToString() + " AND A.btRoot=3 AND A.IIDRekening like '4%'  and B.iTahap= " + pTahap.ToString() + "  AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,3),A.sNamaRekening	 " +

                    " UNION  Select 6 as Level, 1 as XGroup,2 as Kelompok,0 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  LEFT(B.IIDRekening,5) as Kode , A.IIDRekening, LEFT(B.IIDRekening,5) as IDX,A.sNamaRekening AS NAMA,  " +
                 "  SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
          " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                      " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,5)= LEFT(B.IIDRekening,5)  " +
                      " WHERE  B.iTahap=" + pTahap.ToString() + " AND A.btRoot=4 AND A.IIDRekening like '4%'  and B.iTahap= " + pTahap.ToString() + "  AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas in " + sDinas + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,5),A.sNamaRekening " +

                      " UNION  Select 7 as Level, 1 as XGroup,2 as Kelompok,0 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  B.IIDRekening as Kode , A.IIDRekening, B.IIDRekening as IDX,A.sNamaRekening AS NAMA,  " +
        " SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
          " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
         " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON A.IIDRekening = B.IIDRekening " +
         " WHERE  B.iTahap=" + pTahap.ToString() + " AND A.btRoot=5 AND A.IIDRekening like '4%'  and B.iTahap= " + pTahap.ToString() + "  AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, B.IIDRekening,A.sNamaRekening " +

                  " UNION  Select 2 as Level, 1 as XGroup,3 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode,  0 as IIDRekening, 0 as IDX,'JUMLAH PENDAPATAN ' AS NAMA, SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
          " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                      " FROM vwAnggaranKasDanAnggaran B  " +
                      " WHERE  B.iTahap=" + pTahap.ToString() + " AND B.IIDRekening like '4%'  and B.iTahap= " + pTahap.ToString() + "  AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas in " + sDinas;


            return sRet;

        }

        private string GetQueryBelanjaTidakLangsungTW(int _pTahun, int _pDinas, int pTahap, List<SKPD> lstSKPD = null)
        {
            // SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
            //   " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4 

            string sRet = "";

            string sDinas;
            sDinas = GetStringDinas(_pDinas, lstSKPD);

            //sRet = "Select 2 as Level,  1 as XGroup,1 as Kelompok,1 as Bold, 1 as Sifat, 0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, LEFT(B.IIDRekening,1) as Kode , A.IIDRekening, LEFT(B.IIDRekening,1) as IDX,'Alokasi Pendapatan ' AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
            //    " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4 " +
            //                " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,1)= LEFT(B.IIDRekening,1) " +
            //                " WHERE A.btRoot=1 AND A.IIDRekening like '4%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas=" + _pDinas.ToString() +
            //                " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,1)" +

            if (_pTahun < 2021)
            {
                sRet = "Select 1 as Level,3 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, 0 as Kode , 0 as IIDRekening, 0 as IDX,'Alokasi Belanja dan Pembiayaan ' AS NAMA, 0 as Anggaran, 0 as Tw1, " +
              " 0 as Tw2, 0 as Tw3, 0 as Tw4  " +
                          " FROM vwAnggaranKasDanAnggaran   " +
                    //   " WHERE  B.iTahap=" + pTahap.ToString() + " AND  A.btRoot=2 AND A.IIDRekening like '512%'  AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas=" + _pDinas.ToString() + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,2),A.sNamaRekening " +



             //   sRet = "Select 1 as Level,3 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, LEFT( B.IIDRekening,2) as Kode , 0 as IIDRekening, 0 as IDX,'Alokasi Belanja dan Pembiayaan ' AS NAMA, SUM(B.cJumlahOlah) as Anggaran, 0 as Tw1, " +
                    // " 0 as Tw2, 0 as Tw3, 0 as Tw4  " +
                    //           " FROM vwAnggaranKasDanAnggaran  " +

                          //" UNION  Select 3 as Level, 3 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat, 0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  LEFT(B.IIDRekening,3) as Kode , A.IIDRekening, LEFT(B.IIDRekening,3) as IDX,A.sNamaRekening AS NAMA,  " +
                    //" SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +


                 " union all Select 2 as Level,3 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, LEFT( B.IIDRekening,2) as Kode , A.IIDRekening, LEFT(B.IIDRekening,2) as IDX,A.sNamaRekening AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
              " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                          " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,2)= LEFT(B.IIDRekening,2) " +
                          " WHERE  B.iTahap=" + pTahap.ToString() + " AND A.btRoot=2 AND A.IIDRekening like '51%'  AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,2),A.sNamaRekening " +


                          " UNION  Select 5 as Level, 3 as XGroup,2 as Kelompok,0 as Bold, 1 as Sifat, 0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  LEFT(B.IIDRekening,3) as Kode , A.IIDRekening, LEFT(B.IIDRekening,3) as IDX,A.sNamaRekening AS NAMA,  " +
                          " SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
              " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                          " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,3)= LEFT(B.IIDRekening,3)  " +
                          " WHERE  B.iTahap=" + pTahap.ToString() + " AND A.btRoot=3 AND A.IIDRekening like '51%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas in " + sDinas + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,3),A.sNamaRekening	 " +

                        " UNION  Select 6 as Level, 3 as XGroup,2 as Kelompok,0 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  LEFT(B.IIDRekening,5) as Kode , A.IIDRekening, LEFT(B.IIDRekening,5) as IDX,A.sNamaRekening AS NAMA,  " +
                     "  SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
              " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                          " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,5)= LEFT(B.IIDRekening,5)  " +
                          " WHERE  B.iTahap=" + pTahap.ToString() + " AND A.btRoot=4 AND A.IIDRekening like '51%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,5),A.sNamaRekening " +

                          " UNION  Select 7 as Level, 3 as XGroup,2 as Kelompok,0 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  B.IIDRekening as Kode , A.IIDRekening, B.IIDRekening as IDX,A.sNamaRekening AS NAMA,  " +
                          " SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
                          " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                          " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON A.IIDRekening = B.IIDRekening " +
                          " WHERE  B.iTahap=" + pTahap.ToString() + " AND A.btRoot=5 AND A.IIDRekening like '51%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, B.IIDRekening,A.sNamaRekening ";

            }
            else
            {

                sRet = "Select 1 as Level,3 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, 0 as Kode , 0 as IIDRekening, 0 as IDX,'Alokasi Belanja dan Pembiayaan ' AS NAMA, 0 as Anggaran, 0 as Tw1, " +
" 0 as Tw2, 0 as Tw3, 0 as Tw4  " +
          " FROM vwAnggaranKasDanAnggaran   " +


 " union all Select 2 as Level,3 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, LEFT( B.IIDRekening,2) as Kode , A.IIDRekening, LEFT(B.IIDRekening,2) as IDX,A.sNamaRekening AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
" SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
          " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,2)= LEFT(B.IIDRekening,2) " +
          " WHERE  B.iTahap=" + pTahap.ToString() + " AND A.btRoot=2 AND A.IIDRekening like '5%'  AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,2),A.sNamaRekening " +


          " UNION  Select 3 as Level, 3 as XGroup,2 as Kelompok,0 as Bold, 1 as Sifat, 0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  LEFT(B.IIDRekening,3) as Kode , A.IIDRekening, LEFT(B.IIDRekening,3) as IDX,A.sNamaRekening AS NAMA,  " +
          " SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
" SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
          " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,4)= LEFT(B.IIDRekening,4)  " +
          " WHERE  B.iTahap=" + pTahap.ToString() + " AND A.btRoot=3 AND A.IIDRekening like '5%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas in " + sDinas + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,3),A.sNamaRekening	 " +

        " UNION  Select 4 as Level, 3 as XGroup,2 as Kelompok,0 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  LEFT(B.IIDRekening,5) as Kode , A.IIDRekening, LEFT(B.IIDRekening,5) as IDX,A.sNamaRekening AS NAMA,  " +
     "  SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
" SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
          " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,6)= LEFT(B.IIDRekening,6)  " +
          " WHERE  B.iTahap=" + pTahap.ToString() + " AND A.btRoot=4 AND A.IIDRekening like '5%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,5),A.sNamaRekening " +

        " UNION  Select 5 as Level, 3 as XGroup,2 as Kelompok,0 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  LEFT(B.IIDRekening,5) as Kode , A.IIDRekening, LEFT(B.IIDRekening,5) as IDX,A.sNamaRekening AS NAMA,  " +
     "  SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
" SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
          " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,8)= LEFT(B.IIDRekening,8)  " +
          " WHERE  B.iTahap=" + pTahap.ToString() + " AND A.btRoot=5 AND A.IIDRekening like '5%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,5),A.sNamaRekening " +


          " UNION  Select 7 as Level, 3 as XGroup,2 as Kelompok,0 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  B.IIDRekening as Kode , A.IIDRekening, B.IIDRekening as IDX,A.sNamaRekening AS NAMA,  " +
          " SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
          " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
          " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON A.IIDRekening = B.IIDRekening " +
          " WHERE  B.iTahap=" + pTahap.ToString() + " AND A.btRoot=6 AND A.IIDRekening like '5%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, B.IIDRekening,A.sNamaRekening ";


            }

            return sRet;

        }


        private string GetQueryPenerimaanPembiayaan(int _pTahun, int _pDinas, int pTahap, List<SKPD> lstSKPD = null)
        {
            /*
             * 
             * isnull(SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
                " isnull(SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, isnull(SUM(cBulan7+cBulan8+cBulan9) as Tw3, isnull(SUM(cBUlan10+cBulan11+cBUlan12) as Tw4 
             * */



            string sDinas;
            sDinas = GetStringDinas(_pDinas, lstSKPD);

            string sRet = "";


            sRet = "         Select 2 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, LEFT( B.IIDRekening,2) as Kode , 0 as idsubkegiatan, A.IIDRekening, LEFT(B.IIDRekening,2) as IDX,A.sNamaRekening AS NAMA, isnull(SUM(B.cJumlahOlah) ,0) as Anggaran, isnull(SUM(cBulan1) ,0) as Januari, isnull(SUM (cBulan2) ,0) as Februari, isnull(SUM(cBulan3) ,0) as Maret, " +
                            " isnull(SUM(cBulan4) ,0) as APril, isnull(SUM(cBUlan5) ,0) as Mei, isnull(SUM(cBUlan6),0) as Juni, isnull(SUM(cBulan7) ,0) as Juli, isnull(SUM(cBulan8) ,0) as Agustus, " +
                            " isnull(SUM(cBulan9) ,0) as September, isnull(SUM(cBUlan10) ,0) as Oktober, isnull(SUM (cBulan11) ,0) as November , isnull(SUM (cBUlan12) ,0) as Desember" +
                            " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,2)= LEFT(B.IIDRekening,2) " +
                            " WHERE A.btRoot=2 AND A.IIDRekening like '61%'   and B.iTahap= " + pTahap.ToString() + " AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,2),A.sNamaRekening UNION Select  2 as XGroup, 2 as Kelompok,0 as Bold, 1 as Sifat, 0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  LEFT(B.IIDRekening,3) as Kode , 0 as idsubkegiatan, A.IIDRekening, LEFT(B.IIDRekening,3) as IDX,A.sNamaRekening AS NAMA,  " +
                            " isnull(SUM(B.cJumlahOlah) ,0) as Anggaran, isnull(SUM(cBulan1) ,0) as Januari, isnull(SUM (cBulan2) ,0) as Februari, isnull(SUM(cBulan3) ,0) as Maret, " +
                            " isnull(SUM(cBulan4) ,0) as APril, isnull(SUM(cBUlan5) ,0) as Mei, isnull(SUM(cBUlan6),0) as Juni, isnull(SUM(cBulan7) ,0) as Juli, isnull(SUM(cBulan8) ,0) as Agustus,  " +
                            " isnull(SUM(cBulan9) ,0) as September, isnull(SUM(cBUlan10) ,0) as Oktober, isnull(SUM (cBulan11) ,0) as November , isnull(SUM (cBUlan12) ,0) as Desember " +
                            " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,3)= LEFT(B.IIDRekening,3)  " +
                            " WHERE A.btRoot=3 AND A.IIDRekening like '61%'  and B.iTahap= " + pTahap.ToString() + " AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,3),A.sNamaRekening	 " +

                          " UNION Select  2 as XGroup,2 as Kelompok,0 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  LEFT(B.IIDRekening,5) as Kode , 0 as idsubkegiatan, A.IIDRekening, LEFT(B.IIDRekening,5) as IDX,A.sNamaRekening AS NAMA,  " +
                       "  isnull(SUM(B.cJumlahOlah) ,0) as Anggaran, isnull(SUM(cBulan1) ,0) as Januari, isnull(SUM (cBulan2) ,0) as Februari, isnull(SUM(cBulan3) ,0) as Maret, " +
                        " isnull(SUM(cBulan4) ,0) as APril, isnull(SUM(cBUlan5) ,0) as Mei, isnull(SUM(cBUlan6),0) as Juni, isnull(SUM(cBulan7) ,0) as Juli, isnull(SUM(cBulan8) ,0) as Agustus,  " +
                            " isnull(SUM(cBulan9) ,0) as September, isnull(SUM(cBUlan10) ,0) as Oktober, isnull(SUM (cBulan11) ,0) as November , isnull(SUM (cBUlan12) ,0) as Desember " +
                            " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,5)= LEFT(B.IIDRekening,5)  " +
                            " WHERE A.btRoot=4 AND A.IIDRekening like '61%'  and B.iTahap= " + pTahap.ToString() + " AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,5),A.sNamaRekening " +

                            " UNION Select  2 as XGroup,2 as Kelompok,0 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  B.IIDRekening as Kode , 0 as idsubkegiatan , A.IIDRekening, B.IIDRekening as IDX,A.sNamaRekening AS NAMA,  " +
              " isnull(SUM(B.cJumlahOlah) ,0) as Anggaran, isnull(SUM(cBulan1) ,0) as Januari, isnull(SUM (cBulan2) ,0) as Februari, isnull(SUM(cBulan3) ,0) as Maret, " +
               " isnull(SUM(cBulan4) ,0) as APril, isnull(SUM(cBUlan5) ,0) as Mei, isnull(SUM(cBUlan6),0) as Juni, isnull(SUM(cBulan7) ,0) as Juli, isnull(SUM(cBulan8) ,0) as Agustus,  " +
               " isnull(SUM(cBulan9) ,0) as September, isnull(SUM(cBUlan10) ,0) as Oktober, isnull(SUM (cBulan11) ,0) as November , isnull(SUM (cBUlan12) ,0) as Desember " +
               " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON A.IIDRekening = B.IIDRekening " +
               " WHERE A.btRoot=5 AND A.IIDRekening like '61%'  and B.iTahap= " + pTahap.ToString() + " AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, B.IIDRekening,A.sNamaRekening " +

                        " UNION Select  2 as XGroup,3 as Kelompok,0 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode , 0 as idsubkegiatan,  0 as IIDRekening, 0 as IDX,'JUMLAH PENERIMAAN PEMBAYARAN  ' AS NAMA, isnull(SUM(B.cJumlahOlah) ,0) as Anggaran, isnull(SUM(cBulan1) ,0) as Januari, isnull(SUM (cBulan2) ,0) as Februari, isnull(SUM(cBulan3) ,0) as Maret, " +
                        " isnull(SUM(cBulan4) ,0) as APril, isnull(SUM(cBUlan5) ,0) as Mei, isnull(SUM(cBUlan6),0) as Juni, isnull(SUM(cBulan7) ,0) as Juli, isnull(SUM(cBulan8) ,0) as Agustus,  " +
                            " isnull(SUM(cBulan9) ,0) as September, isnull(SUM(cBUlan10) ,0) as Oktober, isnull(SUM (cBulan11) ,0) as November , isnull(SUM (cBUlan12) ,0) as Desember " +
                            " FROM vwAnggaranKasDanAnggaran B  " +
                            " WHERE B.IIDRekening like '61%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas +
                " UNION Select   2 as XGroup, 4 as Kelompok,1 as Bold, 2 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  '' as Kode , 0 as idsubkegiatan, 0 as IIDRekening, 0 as IDX,'JUMLAH ALOKASI YANG TERSEDIA UNTUK PENGELUARAN PER TRIWULAN ' AS NAMA, isnull(SUM(B.cJumlahOlah) ,0) as Anggaran, isnull(SUM(cBulan1+ cBulan2+cBulan3) ,0) as Januari, 0 as Februari, 0 as Maret, " +
                    " isnull(SUM(cBulan4+cBulan5+cBulan6) ,0) as April, 0  as Mei, 0  as Juni, isnull(SUM(cBulan7+cBulan8+cBulan9) ,0) as Juli, 0 as Agustus,  " +
                        " 0 as September, isnull(SUM(cBUlan10+cBUlan11+cBUlan12) ,0) as Oktober, 0  as November , 0 as Desember " +
                        " FROM vwAnggaranKasDanAnggaran B " +
                        " WHERE (B.IIDRekening like '61%' or B.IIDRekening like '4%')  and B.iTahap= " + pTahap.ToString() + "  AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas;

            return sRet;

        }
        private string GetQueryPenerimaanPembiayaanTW(int _pTahun, int _pDinas, int pTahap, List<SKPD> lstSKPD = null)
        {


            string sDinas;
            sDinas = GetStringDinas(_pDinas, lstSKPD);


            string sRet = "";

            //sRet = "Select 2 as Level,  1 as XGroup,1 as Kelompok,1 as Bold, 1 as Sifat, 0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, LEFT(B.IIDRekening,1) as Kode , A.IIDRekening, LEFT(B.IIDRekening,1) as IDX,'Alokasi Pendapatan ' AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
            //    " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4 " +
            //                " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,1)= LEFT(B.IIDRekening,1) " +
            //                " WHERE A.btRoot=1 AND A.IIDRekening like '4%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas=" + _pDinas.ToString() +
            //                " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,1)" +


            sRet = "Select 2 as Level,2 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, LEFT( B.IIDRekening,2) as Kode , A.IIDRekening, LEFT(B.IIDRekening,2) as IDX,A.sNamaRekening AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
          " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                      " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,2)= LEFT(B.IIDRekening,2) " +
                      " WHERE A.btRoot=2 AND A.IIDRekening like '61%'  and B.iTahap= " + pTahap.ToString() + "  AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,2),A.sNamaRekening " +


                      " UNION  Select 5 as Level, 2 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat, 0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  LEFT(B.IIDRekening,3) as Kode , A.IIDRekening, LEFT(B.IIDRekening,3) as IDX,A.sNamaRekening AS NAMA,  " +
                      " SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
          " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                      " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,3)= LEFT(B.IIDRekening,3)  " +
                      " WHERE A.btRoot=3 AND A.IIDRekening like '61%'  and B.iTahap= " + pTahap.ToString() + "  AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,3),A.sNamaRekening	 " +

                    " UNION  Select 6 as Level, 2 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  LEFT(B.IIDRekening,5) as Kode , A.IIDRekening, LEFT(B.IIDRekening,5) as IDX,A.sNamaRekening AS NAMA,  " +
                 "  SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
          " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                      " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,5)= LEFT(B.IIDRekening,5)  " +
                      " WHERE A.btRoot=4 AND A.IIDRekening like '61%'  and B.iTahap= " + pTahap.ToString() + "  AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,5),A.sNamaRekening " +

                      " UNION  Select 7 as Level, 2 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  B.IIDRekening as Kode , A.IIDRekening, B.IIDRekening as IDX,A.sNamaRekening AS NAMA,  " +
        " SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
          " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
         " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON A.IIDRekening = B.IIDRekening " +
         " WHERE A.btRoot=5 AND A.IIDRekening like '61%'  and B.iTahap= " + pTahap.ToString() + "  AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, B.IIDRekening,A.sNamaRekening " +

                  " UNION  Select 2 as Level, 2 as XGroup,3 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode,  0 as IIDRekening, 0 as IDX,'UMLAH PENERIMAAN PEMBAYARAN' AS NAMA, SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
          " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                      " FROM vwAnggaranKasDanAnggaran B  " +
                      " WHERE B.IIDRekening like '61%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas +
                   " UNION  Select 2 as Level, 2 as XGroup,4 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode,  0 as IIDRekening, 0 as IDX,'JUMLAH ALOKASI PENDPATAN DAN PENERIMAAN PEMBIAYAAN' AS NAMA, SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
                " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                " FROM vwAnggaranKasDanAnggaran B  " +
                " WHERE (B.IIDRekening like '61%' or B.IIDRekening like '4%')  and B.iTahap= " + pTahap.ToString() + "  AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas +


            " UNION  Select 0 as Level, 2 as XGroup,5 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode,  0 as IIDRekening, 0 as IDX,'JUMLAH ALOKASI KAS YANG TERSEDIA UNTUK PENGELUARAN' AS NAMA, SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
                " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                " FROM vwAnggaranKasDanAnggaran B  " +
                " WHERE (B.IIDRekening like '61%' or B.IIDRekening like '4%')  and B.iTahap= " + pTahap.ToString() + "  AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas;

            return sRet;


        }
        private string GetQueryPengeluaranPembiayaanTW(int _pTahun, int _pDinas, int pTahap, List<SKPD> lstSKPD = null)
        {

            string sDinas;
            sDinas = GetStringDinas(_pDinas, lstSKPD);


            string sRet = "";

            //sRet = "Select 2 as Level,  1 as XGroup,1 as Kelompok,1 as Bold, 1 as Sifat, 0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, LEFT(B.IIDRekening,1) as Kode , A.IIDRekening, LEFT(B.IIDRekening,1) as IDX,'Alokasi Pendapatan ' AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
            //    " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4 " +
            //                " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,1)= LEFT(B.IIDRekening,1) " +
            //                " WHERE A.btRoot=1 AND A.IIDRekening like '4%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas=" + _pDinas.ToString() +
            //                " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,1)" +


            sRet = "Select 2 as Level,5 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, LEFT( B.IIDRekening,2) as Kode , A.IIDRekening, LEFT(B.IIDRekening,2) as IDX,A.sNamaRekening AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
          " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                      " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,2)= LEFT(B.IIDRekening,2) " +
                      " WHERE A.btRoot=2 AND A.IIDRekening like '62%'  and B.iTahap= " + pTahap.ToString() + "   AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,2),A.sNamaRekening " +


                      " UNION  Select 5 as Level, 5 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat, 0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  LEFT(B.IIDRekening,3) as Kode , A.IIDRekening, LEFT(B.IIDRekening,3) as IDX,A.sNamaRekening AS NAMA,  " +
                      " SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
          " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                      " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,3)= LEFT(B.IIDRekening,3)  " +
                      " WHERE A.btRoot=3 AND A.IIDRekening like '62%'  and B.iTahap= " + pTahap.ToString() + "   AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,3),A.sNamaRekening	 " +

                    " UNION  Select 6 as Level, 5 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  LEFT(B.IIDRekening,5) as Kode , A.IIDRekening, LEFT(B.IIDRekening,5) as IDX,A.sNamaRekening AS NAMA,  " +
                 "  SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
          " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                      " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,5)= LEFT(B.IIDRekening,5)  " +
                      " WHERE A.btRoot=4 AND A.IIDRekening like '62%'  and B.iTahap= " + pTahap.ToString() + "   AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,5),A.sNamaRekening " +

                      " UNION  Select 7 as Level, 5 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  B.IIDRekening as Kode , A.IIDRekening, B.IIDRekening as IDX,A.sNamaRekening AS NAMA,  " +
        " SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
          " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
         " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON A.IIDRekening = B.IIDRekening " +
         " WHERE A.btRoot=5 AND A.IIDRekening like '62%'  and B.iTahap= " + pTahap.ToString() + "   AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, B.IIDRekening,A.sNamaRekening " +

                  " UNION  Select 2 as Level, 5 as XGroup,3 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode,  0 as IIDRekening, 0 as IDX,'JUMLAH PENGELUARAN PEMBIAYAAN   ' AS NAMA, SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
          " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                      " FROM vwAnggaranKasDanAnggaran B  " +
                      " WHERE B.IIDRekening like '62%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas +
                   " UNION  Select 2 as Level, 5 as XGroup,3 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode,  0 as IIDRekening, 0 as IDX," +
                   "'JUMLAH ALOKAASI BELANJA DAN PENGELUARAN PEMBIAYAAN ' AS NAMA, SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
                " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                " FROM vwAnggaranKasDanAnggaran B  " +
                " WHERE (B.IIDRekening like '62%' or B.IIDRekening like '5%')   and B.iTahap= " + pTahap.ToString() + "   AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas;

            return sRet;


        }
        private string GetQuerySaldoKas(int _pTahun, int _pDinas, int pUnit, int pTahap, List<SKPD> lstSKPD = null)
        {


            string sDinas;
            sDinas = GetStringDinas(_pDinas, lstSKPD);

            string sRet = "";
            string sWhere = "";

            if (pUnit > 0)
            {

                sWhere = "B.iTAhun=" + _pTahun.ToString() + "  and B.iTahap= " + pTahap.ToString() + "   AND B.IDDInas  in " + sDinas + " AND B.IDunit  = " + pUnit.ToString();
            }
            else
            {
                sWhere = "B.iTAhun=" + _pTahun.ToString() + "  and B.iTahap= " + pTahap.ToString() + "   AND B.IDDInas  in " + sDinas;
            }


            sRet = " Select 0 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, 0 as Kode , 0 as idsubkegiatan, 0 as IIDRekening, " +
                            " 0 as IDX,'SALDO KAS' AS NAMA, 0 as Anggaran, 0 as Januari, " +
                            " (SELECT isnull(SUM (cBulan1),0) from vwAnggaranKasDanAnggaran B where " + sWhere + " and BTJENIS in (1,4) ) - " +
                              " (SELECT isnull(SUM  (cBulan1),0) from vwAnggaranKasDanAnggaran  B where " + sWhere + "   and btJenis in (2,3,5) )    as Februari," +
                            " (SELECT isnull(SUM (cBulan1+ cBulan2),0) from vwAnggaranKasDanAnggaran  B where " + sWhere + "  and BTJENIS in (1,4)  ) - " +
                              " (SELECT isnull(SUM  (cBulan1+ cBulan2),0) from vwAnggaranKasDanAnggaran  B where " + sWhere + "   and btJenis in (2,3,5) )    as Maret," +
                            " (SELECT  isnull(SUM (cBulan1+ cBulan2+cBulan3),0) from vwAnggaranKasDanAnggaran  B where " + sWhere + "    and BTJENIS in (1,4)  ) - " +
                              " (SELECT isnull(SUM (cBulan1+ cBulan2+cBulan3),0) from vwAnggaranKasDanAnggaran  B where " + sWhere + "   and btJenis in (2,3,5) )    as April," +
                            " (SELECT  isnull(SUM (cBulan1+ cBulan2+cBulan3+cBulan4),0) from vwAnggaranKasDanAnggaran  B where " + sWhere + "   and  BTJENIS in (1,4) ) - " +
                              " (SELECT isnull(SUM  (cBulan1+ cBulan2+cBulan3+cBulan4),0) from vwAnggaranKasDanAnggaran  B  where " + sWhere + "    and btJenis in (2,3,5) )    as Mei," +
                            " (SELECT  isnull(SUM (cBulan1+ cBulan2+cBulan3+cBulan4+ cBulan5),0) from vwAnggaranKasDanAnggaran  B where " + sWhere + "  and  BTJENIS in (1,4)  ) - " +
                              " (SELECT isnull(SUM  (cBulan1+ cBulan2+cBulan3+cBulan4+cBulan5),0) from vwAnggaranKasDanAnggaran  B where " + sWhere + "    and btJenis in (2,3,5) )    as Juni," +

                            " (SELECT isnull(SUM  (cBulan1+ cBulan2+cBulan3+cBulan4+cBulan5+cBulan6),0) from vwAnggaranKasDanAnggaran  B  where " + sWhere + "   and BTJENIS in (1,4)  ) - " +
                              " (SELECT isnull( SUM (cBulan1+ cBulan2+cBulan3+cBulan4+cBulan5+cBulan6),0) from vwAnggaranKasDanAnggaran  B where " + sWhere + "   and btJenis in (2,3,5) )    as Juli," +

                              " (SELECT isnull(SUM (cBulan1+ cBulan2+cBulan3+cBulan4+cBulan5+ cBulan6 +cBulan7),0) from vwAnggaranKasDanAnggaran  B  where " + sWhere + "    and BTJENIS in (1,4)  ) - " +
                              " (SELECT isnull( SUM (cBulan1+ cBulan2+cBulan3+cBulan4+cBulan5+ cBulan6 +cBulan7),0) from vwAnggaranKasDanAnggaran  B where " + sWhere + "  and btJenis in (2,3,5) )    as Agustus," +
                            " (SELECT isnull(SUM (cBulan1+ cBulan2+cBulan3+cBulan4+cBulan5+ cBulan6 +cBulan7 + cBulan8),0) from vwAnggaranKasDanAnggaran  B  where " + sWhere + "   and BTJENIS in (1,4) ) - " +
                              " (SELECT isnull(SUM  (cBulan1+ cBulan2+cBulan3+cBulan4+cBulan5+ cBulan6 +cBulan7 + cBulan8),0) from vwAnggaranKasDanAnggaran  B  where " + sWhere + "   and btJenis in (2,3,5) )    as September," +

                              " (SELECT isnull(SUM  (cBulan1+ cBulan2+cBulan3+cBulan4+cBulan5+ cBulan6 +cBulan7 + cBulan8 +cBulan9),0) from vwAnggaranKasDanAnggaran  B  where " + sWhere + "    and BTJENIS in (1,4) ) - " +
                              " (SELECT isnull(SUM  (cBulan1+ cBulan2+cBulan3+cBulan4+cBulan5+ cBulan6 +cBulan7 + cBulan8 +cBulan9),0) from vwAnggaranKasDanAnggaran  B where " + sWhere + "    and btJenis in (2,3,5) )    as Oktober," +

                              " (SELECT isnull(SUM  (cBulan1+ cBulan2+cBulan3+cBulan4+cBulan5+ cBulan6 +cBulan7 + cBulan8 +cBulan9 + cBulan10),0) from vwAnggaranKasDanAnggaran  B  where " + sWhere + "    and  BTJENIS in (1,4)  ) - " +
                              " (SELECT isnull(SUM  (cBulan1+ cBulan2+cBulan3+cBulan4+cBulan5+ cBulan6 +cBulan7 + cBulan8 +cBulan9 + cBulan10),0) from vwAnggaranKasDanAnggaran  B where " + sWhere + "    and btJenis in (2,3,5) )    as November," +

                              " (SELECT isnull(SUM  (cBulan1+ cBulan2+cBulan3+cBulan4+cBulan5+ cBulan6 +cBulan7 + cBulan8 +cBulan9 + cBulan10+ cBulan11),0) from vwAnggaranKasDanAnggaran  B where " + sWhere + "   and  BTJENIS in (1,4)  ) - " +
                              " (SELECT isnull(SUM  (cBulan1+ cBulan2+cBulan3+cBulan4+cBulan5+ cBulan6 +cBulan7 + cBulan8 +cBulan9 + cBulan10+ cBulan11 ),0) from vwAnggaranKasDanAnggaran  B  where " + sWhere + "   and btJenis in (2,3,5) )    as Desember";


            return sRet;

        }
        private string GetQuerySaldoKasTW(int _pTahun, int _pDinas, int pTahap, List<SKPD> lstSKPD = null)
        {


            string sDinas;
            sDinas = GetStringDinas(_pDinas, lstSKPD);


            string sRet = "";


            sRet = " Select 1 as Level,0 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, 0 as Kode , 0 as IIDRekening, " +
                            " 0 as IDX,'SALDO AWAL KAS ' AS NAMA, 0 as Anggaran, 0 as Tw1, " +
                            " (SELECT SUM (isnull(cBulan1,0)+cBulan2+cBulan3) from vwAnggaranKasDanAnggaran B where B.iTahap=" + pTahap.ToString() + " AND  B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + "  and BTJENIS in (1,4) ) - " +
                              " (SELECT SUM (cBulan1+cBulan2+cBulan3) from vwAnggaranKasDanAnggaran  B where  B.iTahap=" + pTahap.ToString() + " AND  B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + "  and btJenis in (2,3,5) )    as Tw2," +
                            " (SELECT SUM (cBulan1+cBulan2+cBulan3 +cBulan4+ cBulan5 + cBulan6) from vwAnggaranKasDanAnggaran  B where  B.iTahap=" + pTahap.ToString() + " AND  B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + "  and BTJENIS in (1,4)  ) - " +
                            " (SELECT SUM (cBulan1+cBulan2+cBulan3 +cBulan4+ cBulan5 + cBulan6) from vwAnggaranKasDanAnggaran  B where  B.iTahap=" + pTahap.ToString() + " AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + "  and btJenis in (2,3,5) )    as Tw3," +
                              " (SELECT SUM (cBulan1+ cBulan2+cBulan3+cBulan4+cBulan5+ cBulan6 +cBulan7 + cBulan8 +cBulan9) from vwAnggaranKasDanAnggaran  B  where  B.iTahap=" + pTahap.ToString() + " AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + "  and BTJENIS in (1,4) ) - " +
                              " (SELECT SUM (cBulan1+ cBulan2+cBulan3+cBulan4+cBulan5+ cBulan6 +cBulan7 + cBulan8 +cBulan9) from vwAnggaranKasDanAnggaran  B where  B.iTahap=" + pTahap.ToString() + " AND  B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + "  and btJenis in (2,3,5) )    as Tw4";



            return sRet;

        }

        private string GetQuerySaldoAkhirTW(int _pTahun, int _pDinas, int pTahap, List<SKPD> lstSKPD = null)
        {


            string sDinas;
            sDinas = GetStringDinas(_pDinas, lstSKPD);

            string sRet;
            sRet = " Select 1 as Level,8 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, 0 as Kode , 0 as IIDRekening, " +
                            " 0 as IDX,'Sisa Kas Setelah dikurangi Belanja Tidak Langsung, Belanja Langsung serta Pengeluaran Pembiayaan' AS NAMA, 0 as Anggaran, " +
                            " (SELECT SUM (cBulan1+cBulan2+cBulan3) from vwAnggaranKasDanAnggaran B where B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + "  and BTJENIS in (1,4) ) - " +
                              " (SELECT SUM (cBulan1+cBulan2+cBulan3) from vwAnggaranKasDanAnggaran  B where B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + "  and btJenis in (2,3,5) )    as Tw1," +
                            " (SELECT SUM (cBulan1+cBulan2+cBulan3 +cBulan4+ cBulan5 + cBulan6) from vwAnggaranKasDanAnggaran  B where B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + "  and BTJENIS in (1,4)  ) - " +
                            " (SELECT SUM (cBulan1+cBulan2+cBulan3 +cBulan4+ cBulan5 + cBulan6) from vwAnggaranKasDanAnggaran  B where B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + "  and btJenis in (2,3,5) )    as Tw2," +
                              " (SELECT SUM (cBulan1+ cBulan2+cBulan3+cBulan4+cBulan5+ cBulan6 +cBulan7 + cBulan8 +cBulan9) from vwAnggaranKasDanAnggaran  B  where B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + "  and BTJENIS in (1,4) ) - " +
                              " (SELECT SUM (cBulan1+ cBulan2+cBulan3+cBulan4+cBulan5+ cBulan6 +cBulan7 + cBulan8 +cBulan9) from vwAnggaranKasDanAnggaran  B where B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + "  and btJenis in (2,3,5) )    as Tw3 ," +
            " (SELECT SUM (cBulan1+ cBulan2+cBulan3+cBulan4+cBulan5+ cBulan6 +cBulan7 + cBulan8 +cBulan9 +cBulan10 + cBulan11 +cBulan12 ) from vwAnggaranKasDanAnggaran  B  where B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + "  and BTJENIS in (1,4) ) - " +
           " (SELECT SUM (cBulan1+ cBulan2+cBulan3+cBulan4+cBulan5+ cBulan6 +cBulan7 + cBulan8 +cBulan9+cBulan10 + cBulan11 +cBulan12 ) from vwAnggaranKasDanAnggaran  B where B.iTAhun=" + _pTahun.ToString() + " AND B.iTahap=" + pTahap.ToString() + " AND B.IDDInas  in " + sDinas + "  and btJenis in (2,3,5) )    as Tw4";
            return sRet;

        }
        private string GetQueryPengeluaranPembiayaan(int _pTahun, int _pDinas, int pTahap, List<SKPD> lstSKPD = null)
        {


            string sDinas;
            sDinas = GetStringDinas(_pDinas, lstSKPD);


            string sRet = "";


            sRet = "         Select 5 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, LEFT( B.IIDRekening,2) as Kode , 0 as idsubkegiatan, A.IIDRekening, LEFT(B.IIDRekening,2) as IDX,A.sNamaRekening AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                            " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus, " +
                            " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember" +
                            " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,2)= LEFT(B.IIDRekening,2) " +
                            " WHERE A.btRoot=2 AND A.IIDRekening like '61%'  AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " AND B.iTahap=" + pTahap.ToString() + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,2),A.sNamaRekening " +
                            " UNION Select  5 as XGroup, 2 as Kelompok,1 as Bold, 1 as Sifat, 0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  LEFT(B.IIDRekening,3) as Kode , 0 as idsubkegiatan, A.IIDRekening, LEFT(B.IIDRekening,3) as IDX,A.sNamaRekening AS NAMA,  " +
                            " SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                            " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus,  " +
                            " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember " +
                            " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,3)= LEFT(B.IIDRekening,3)  " +
                            " WHERE A.btRoot=3 AND A.IIDRekening like '61%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " AND B.iTahap=" + pTahap.ToString() + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,3),A.sNamaRekening" +
                          " UNION Select  5 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  LEFT(B.IIDRekening,5) as Kode , 0 as idsubkegiatan, A.IIDRekening, LEFT(B.IIDRekening,5) as IDX,A.sNamaRekening AS NAMA,  " +
                       "  SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                        " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus,  " +
                            " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember " +
                            " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,5)= LEFT(B.IIDRekening,5)  " +
                            " WHERE A.btRoot=4 AND A.IIDRekening like '61%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " AND B.iTahap=" + pTahap.ToString() + "  GROUP BY A.IIDRekening, LEFT(B.IIDRekening,5),A.sNamaRekening " +
                            " UNION Select  5 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  B.IIDRekening as Kode , 0 as idsubkegiatan , A.IIDRekening, B.IIDRekening as IDX,A.sNamaRekening AS NAMA,  " +
              " SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
               " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus,  " +
               " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember " +
               " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON A.IIDRekening = B.IIDRekening " +
               " WHERE A.btRoot=5 AND A.IIDRekening like '61%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas  in " + sDinas + " AND B.iTahap=" + pTahap.ToString() + " GROUP BY A.IIDRekening, B.IIDRekening,A.sNamaRekening " +
                                    " UNION Select  5 as XGroup,3 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode , 0 as idsubkegiatan,  0 as IIDRekening, 0 as IDX,'JUMLAH PENGELUARAN PEMBIAYAAN  ' AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                        " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus,  " +
                            " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember " +
                            " FROM vwAnggaranKasDanAnggaran B  " +
                            " WHERE B.IIDRekening like '61%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.iTahap=" + pTahap.ToString() + " AND B.IDDInas  in " + sDinas +
                " UNION Select   2 as XGroup, 4 as Kelompok,1 as Bold, 2 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  '' as Kode , 0 as idsubkegiatan, 0 as IIDRekening, 0 as IDX,'JUMLAH ALOKASI YANG TERSEDIA UNTUK PENGELUARAN PER TRIWULAN ' AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1+ cBulan2+cBulan3) as Januari, 0 as Februari, 0 as Maret, " +
                    " SUM(cBulan4+cBulan5+cBulan6) as April, 0 as Mei, 0 as Juni, SUM(cBulan7+cBulan8+cBulan9) as Juli, 0 as Agustus,  " +
                        " 0 as September, SUM(cBUlan10+cBUlan11+cBUlan12) as Oktober, 0 as November , 0 as Desember " +
                        " FROM vwAnggaranKasDanAnggaran B " +
                        " WHERE (B.IIDRekening like '61%' or B.IIDRekening like '4%') AND B.iTAhun=" + _pTahun.ToString() + " AND B.iTahap=" + pTahap.ToString() + " AND B.IDDInas  in " + sDinas;

            return sRet;

        }

        private string GetQueryBelanjaTidakLangsung(int _pTahun, int _pDinas, int pTahap, List<SKPD> lstSKPD = null)
        {
            //Select 5 as Kelompok,1 as Bold, 1 as Sifat, '' as Kode,'Alokasi Belanja Tidak Langsung' 
            //Select 6 as KElompok,1 as Bold, 1 as Sifat,IDRekening as Kode,'Nama Rekening'
            //Select 7 as Kelompok,1 as Bold, 1 as Sifat,'' as Kode,'Jumlah alokasi belanja tidak langsung per bulan'
            //Select 8 as Kelompok,1 as Bold, 2 as Sifat,'' as Kode,'Jumlah alokasi belanja tidak langsung dan pembiayaan pengeluaran per triwulan'
            //Select 9 as Kelompok,1 as Bold, 2 as Sifat,'' as Kode,'Sisa kas setelah dikurangi belanja tidak langsung per triwulan'
            //Select 10 as Kelompok,1 as Bold, 2 as Sifat,'' as Kode,'Sisa kas setelah dikurangi belanja tidak langsung per triwulan'

            string sDinas;
            sDinas = GetStringDinas(_pDinas, lstSKPD);


            string sRet = "";

            sRet = "Select  3 as XGroup,1 as Kelompok,1 as Bold, 1 as Sifat, 0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, LEFT(B.IIDRekening,1) as Kode , 0 as idsubkegiatan , A.IIDRekening, LEFT(B.IIDRekening,1) as IDX,'Alokasi Belanja Tidak Langsung ' AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                       "SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus, " +
                       "SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember" +
                           " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,1)= LEFT(B.IIDRekening,1) " +
                           " WHERE A.btRoot=1 AND A.IIDRekening like '51%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.iTahap=" + pTahap.ToString() + " AND B.IDDInas  in " + sDinas +
                           " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,1)" +



            " UNION Select 3 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, LEFT( B.IIDRekening,2) as Kode , 0 as idsubkegiatan, A.IIDRekening, LEFT(B.IIDRekening,2) as IDX,A.sNamaRekening AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                            " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus, " +
                            " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember" +
                            " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,2)= LEFT(B.IIDRekening,2) " +
                            " WHERE A.btRoot=2 AND A.IIDRekening like '51%'  AND B.iTAhun=" + _pTahun.ToString() + " AND B.iTahap=" + pTahap.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,2),A.sNamaRekening " +


                            " UNION Select  3 as XGroup, 2 as Kelompok,1 as Bold, 1 as Sifat, 0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  LEFT(B.IIDRekening,3) as Kode , 0 as idsubkegiatan, A.IIDRekening, LEFT(B.IIDRekening,3) as IDX,A.sNamaRekening AS NAMA,  " +
                            " SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                            " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus,  " +
                            " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember " +
                            " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,3)= LEFT(B.IIDRekening,3)  " +
                            " WHERE A.btRoot=3 AND A.IIDRekening like '51%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.iTahap=" + pTahap.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,3),A.sNamaRekening	 " +

                          " UNION Select  3 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  LEFT(B.IIDRekening,5) as Kode , 0 as idsubkegiatan, A.IIDRekening, LEFT(B.IIDRekening,5) as IDX,A.sNamaRekening AS NAMA,  " +
                       "  SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                        " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus,  " +
                            " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember " +
                            " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,5)= LEFT(B.IIDRekening,5)  " +
                            " WHERE A.btRoot=4 AND A.IIDRekening like '51%' AND B.iTAhun=" + _pTahun.ToString() + "  AND B.iTahap=" + pTahap.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, LEFT(B.IIDRekening,5),A.sNamaRekening " +

                            " UNION Select  3 as XGroup,2 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  B.IIDRekening as Kode , 0 as idsubkegiatan, A.IIDRekening, B.IIDRekening as IDX,A.sNamaRekening AS NAMA,  " +
              " SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
               " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus,  " +
               " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember " +
               " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON A.IIDRekening = B.IIDRekening " +
               " WHERE A.btRoot=5 AND A.IIDRekening like '51%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.iTahap=" + pTahap.ToString() + " AND B.IDDInas  in " + sDinas + " GROUP BY A.IIDRekening, B.IIDRekening,A.sNamaRekening " +

                        " UNION Select  3 as XGroup,3 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode,0 as idsubkegiatan,  0 as IIDRekening, 0 as IDX,'JUMLAH PENERIMAAN PEMBIAYAAN  ' AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                        " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus,  " +
                            " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember " +
                            " FROM vwAnggaranKasDanAnggaran B  " +
                            " WHERE B.IIDRekening like '61%' AND B.iTAhun=" + _pTahun.ToString() + " AND B.iTahap=" + pTahap.ToString() + " AND B.IDDInas  in " + sDinas +
                " UNION Select   3 as XGroup, 4 as Kelompok,1 as Bold, 2 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  '' as Kode,0 as idsubkegiatan, 0 as IIDRekening, 0 as IDX,'JUMLAH ALOKASI YANG TERSEDIA UNTUK PENGELUARAN PER TRIWULAN ' AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1+ cBulan2+cBulan3) as Januari, 0 as Februari, 0 as Maret, " +
                    " SUM(cBulan4+cBulan5+cBulan6) as April, 0 as Mei, 0 as Juni, SUM(cBulan7+cBulan8+cBulan9) as Juli, 0 as Agustus,  " +
                        " 0 as September, SUM(cBUlan10+cBUlan11+cBUlan12) as Oktober, 0 as November , 0 as Desember " +
                        " FROM vwAnggaranKasDanAnggaran B " +
                        " WHERE B.IIDRekening like '51%'  AND B.iTAhun=" + _pTahun.ToString() + " AND B.iTahap=" + pTahap.ToString() + "  AND B.IDDInas  in " + sDinas;


            // +" " +
            //" UNION Select  10 as Kelompok,1 as Bold, 2 as Sifat,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode,   0 as IIDRekening, 0 as IDX,'Jumlah alokasi belanja tidak langsung dan pembiayaan pengeluaran per triwulan' AS NAMA, SUM(B.Debet * B.cJumlahOlah) as Anggaran, SUM(B.Debet * cBulan1+ B.Debet * cBulan2+B.Debet * cBulan3) as Januari, 0 as Februari, 0 as Maret, " +
            //   " SUM(B.Debet * cBulan4+ B.Debet * cBulan5+B.Debet * cBulan6) as April, 0 as Mei, 0 as Juni, SUM(B.Debet * cBulan7+B.Debet * cBulan8+B.Debet * cBulan9) as Juli, 0 as Agustus,  " +
            //       " 0 as September, SUM(B.Debet * cBUlan10+B.Debet * cBUlan11+B.Debet * cBUlan12) as Oktober, 0 as November , 0 as Desember " +
            //       " FROM vwAnggaranKasDanAnggaran B " +
            //       " WHERE (B.IIDRekening like '51%' or B.IIDRekening like '4%') AND B.iTAhun=" + _pTahun.ToString() + " AND B.IDDInas=" + _pDinas.ToString(); 


            return sRet;

        }

        private string GetQueryBelanjaLangsung(ParameterLaporan p, int _pTahun, int _pDinas, int iTahap, List<SKPD> lstSKPD = null, long idsubkegiatan = 0)
        {
            //Select 11 as Kelompok,1 as Bold, 1 as Sifat, '' as Kode,'Alokasi Belanja Tidak Langsung' 
            //Select 12 as KElompok,1 as Bold, 1 as Sifat,IDRekening as Kode,'Nama Rekening'
            //Select 13 as Kelompok,1 as Bold, 1 as Sifat,'' as Kode,'Jumlah alokasi belanja tidak langsung per bulan'
            //Select 14 as Kelompok,1 as Bold, 2 as Sifat,'' as Kode,'Jumlah alokasi belanja tidak langsung dan pembiayaan pengeluaran per triwulan'
            //Select 15 as Kelompok,1 as Bold, 2 as Sifat,'' as Kode,'Sisa kas setelah dikurangi belanja tidak langsung per triwulan'
            //Select 16 as Kelompok,1 as Bold, 2 as Sifat,'' as Kode,'Sisa kas setelah dikurangi belanja tidak langsung per triwulan'

            string sDinas;
            sDinas = GetStringDinas(_pDinas, lstSKPD);

            string sWhere = "";
            //if () { 
            if (p.JenisAnggaran == 0)
            {
                if (lstSKPD == null)
                    sWhere = " and b.iTahap =" + iTahap.ToString() + "  and  B.IDDInas =" + p.IDDinas.ToString();
                else
                    sWhere = " and b.iTahap =" + iTahap.ToString() + "  and  B.IDDInas in " + sDinas;

            }

            if (p.IDKegiatan > 0)
            {
                if (Tahun <= 2020)
                {
                    if (lstSKPD == null)
                    {
                        sWhere = "  and b.iTahap =" + iTahap.ToString() + " AND B.IDDInas =" + p.IDDinas.ToString() + "   AND B.IDkegiatan = " + p.IDKegiatan.ToString() + " AND B.IDUrusan =" + p.IDUrusan.ToString() + " AND B.IDProgram = " + p.IDProgram.ToString();
                    }
                    else
                        sWhere = "  and b.iTahap =" + iTahap.ToString() + " AND B.IDDInas in " + sDinas + "   AND B.IDkegiatan = " + p.IDKegiatan.ToString() + " AND B.IDUrusan =" + p.IDUrusan.ToString() + " AND B.IDProgram = " + p.IDProgram.ToString();
                }
                else
                {

                    if (lstSKPD == null)
                    {
                        sWhere = "  and b.iTahap =" + iTahap.ToString() + " AND B.IDDInas =" + p.IDDinas.ToString() + "   AND B.IDkegiatan = " + p.IDKegiatan.ToString() + "   AND B.idsubkegiatan = " + p.IDSubKegiatan.ToString() + " AND B.IDUrusan =" + p.IDUrusan.ToString() + " AND B.IDProgram = " + p.IDProgram.ToString();
                    }
                    else
                        sWhere = "  and b.iTahap =" + iTahap.ToString() + " AND B.IDDInas in " + sDinas + "   AND B.IDkegiatan = " + p.IDKegiatan.ToString() + "   AND B.idsubkegiatan = " + p.IDSubKegiatan.ToString() + " AND B.IDUrusan =" + p.IDUrusan.ToString() + " AND B.IDProgram = " + p.IDProgram.ToString();

                }
            }

            string sRet = "";
            //'if (p.IDKegiatan == 0)


            sRet = " Select  4 as XGroup,11 as Kelompok,1 as Bold, 1 as Sifat,0 AS IDDInas, 0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  0 as Kode , 0 as IIDRekening, 0 as IDX,'BELANJA' AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                       " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus, " +
                       " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember" +
                       " FROM vwAnggaranKasDanAnggaran B " +
                       " WHERE B.btJenis= 3 AND  B.IDProgram > 0 and B.iTAhun= " + _pTahun.ToString() + sWhere;
            //and B.IIDRekening like '52%' 

            //sRet = sRet + " UNION ALL Select 11 as Kelompok,1 as Bold, 1 as Sifat,B.IDDinas, 0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  0 as Kode , 0 as IIDRekening, 0 as IDX,A.sNamaSKPD AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
            //    " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus, " +
            //    " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember" +
            //    " FROM vwAnggaranKasDanAnggaran B INNER JOIN mSKPD A ON B.IDDinas = A.ID " +
            //    " WHERE B.btJenis= 3 and B.IIDRekening like '52%'  and B.iTAhun= " + _pTahun.ToString() + sWhere + " GROUP BY B.IDDInas, A.sNamaSKPD ";

            if (lstSKPD == null)
            {

                sRet = sRet + " UNION ALL Select  4 as XGroup,11 as Kelompok,1 as Bold, 1 as Sifat,B.IDDInas, B.IDurusan as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  0 as Kode , 0 as IIDRekening, 0 as IDX,A.sNamaUrusan AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                           " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus, " +
                           " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember" +
                           " FROM mUrusan A INNER JOIN vwAnggaranKasDanAnggaran B ON A.ID = B.IDurusan " +
                           " WHERE B.btJenis= 3  and B.iTAhun= " + _pTahun.ToString() + sWhere + " GROUP BY B.IDDInas, B.IDurusan , A.sNamaUrusan ";


                sRet = sRet + " UNION ALL   SELECT 4 as XGroup, 11 as Kelomk,1 as Bold, 1 as Sifat,B.IDDinas, B.IDurusan as IDUrusan, B.IDProgram as IDProgram, 0 as IDkegiatan,  A.IDPRogram as Kode , 0 as IIDRekening, 0 as IDX,A.sNamaProgram AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                                " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus, " +
                                " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember" +
                                " FROM tPrograms_A A INNER JOIN vwAnggaranKasDanAnggaran B ON A.iTahun = B.iTahun and A.IDurusan = A.IDurusan and A.IDDinas = B.IDDinas AND A.IDProgram = B.IDProgram " +
                                " WHERE B.btJenis= 3  and B.IDProgram > 0 and B.iTAhun=" + _pTahun.ToString() + sWhere + " GROUP BY B.IDDinas, B.IDurusan , B.IDProgram ,  A.IDPRogram,A.sNamaProgram  ";
                sRet = sRet + " UNION Select  4 as XGroup,11 as Kelompok,1 as Bold, 1 as Sifat,B.IDDInas, B.IDurusan as IDUrusan, B.IDProgram as IDProgram, B.IDkegiatan as IDkegiatan,  B.IDKegiatan as Kode , 0 as IIDRekening, 0 as IDX,A.sNama AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                                  " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus, " +
                                  " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember" +
                                  " FROM tKegiatan_A A INNER JOIN vwAnggaranKasDanAnggaran B ON A.iTahun = B.iTahun and A.IDurusan = A.IDurusan and A.IDDinas = B.IDDinas AND A.IDKegiatan=B.IDKegiatan " +
                                  " WHERE B.btJenis= 3  and B.IDProgram > 0  and  B.iTAhun=" + _pTahun.ToString() + sWhere + " GROUP BY B.IDDInas, B.IDurusan, B.IDProgram , B.IDkegiatan ,  B.IDKegiatan,A.sNama ";


                sRet = sRet + " UNION Select  4 as XGroup,11 as Kelompok,1 as Bold, 1 as Sifat,B.IDDInas,B.IDUrusan, B.IDProgram as IDProgram, B.IDkegiatan,  B.IIDRekening as Kode , B.IIDRekening, LEFT(B.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ") as IDX,A.sNamaRekening AS NAMA,  " +
                         " SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                          " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus,  " +
                          " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember " +
                          " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ," + m_ProfileRekening.LEN5.ToString() + ")= LEFT(B.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ")  " +
                          " WHERE A.btRoot=5   AND B.iTAhun=" + _pTahun.ToString() + sWhere + " GROUP BY B.IDDInas,B.IDUrusan, B.IDProgram , B.IDkegiatan,  B.IIDRekening , LEFT(B.IIDRekening," + m_ProfileRekening.LEN5.ToString() + "),A.sNamaRekening ";

                sRet = sRet + "  UNION Select  4 as XGroup,15 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode,  0 as IIDRekening, 0 as IDX,'JUMLAH ALOKASI BELANJA LANGSUNG PER BULAN ' AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                            " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus,  " +
                                " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember " +
                                " FROM vwAnggaranKasDanAnggaran B " +
                                " WHERE  B.iTAhun=" + _pTahun.ToString() + sWhere;

                sRet = sRet + " UNION Select   4 as XGroup,16 as Kelompok,1 as Bold, 2 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode,  0 as IIDRekening, 0 as IDX,'JUMLAH ALOKASI BELANJA LANGSUNG PER TRIWULAN ' AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1+ cBulan2+cBulan3) as Januari, 0 as Februari, 0 as Maret, " +
                    " SUM(cBulan4+cBulan5+cBulan6) as April, 0 as Mei, 0 as Juni, SUM(cBulan7+cBulan8+cBulan9) as Juli, 0 as Agustus,  " +
                        " 0 as September, SUM(cBUlan10+cBUlan11+cBUlan12) as Oktober, 0 as November , 0 as Desember " +
                        " FROM vwAnggaranKasDanAnggaran B " +
                        " WHERE   B.iTAhun=" + _pTahun.ToString() + sWhere;



                ////sRet = sRet + "  UNION Select  17 as Kelompok,1 as Bold, 2 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode,   0 as IIDRekening, 0 as IDX,'JUMLAH ALOKASI ' AS NAMA, SUM(B.Debet * B.cJumlahOlah) as Anggaran, SUM(B.Debet * cBulan1+ B.Debet * cBulan2+B.Debet * cBulan3) as Januari, 0 as Februari, 0 as Maret, " +
                //// " SUM(B.Debet * cBulan4+ B.Debet * cBulan5+B.Debet * cBulan6) as April, 0 as Mei, 0 as Juni, SUM(B.Debet * cBulan7+B.Debet * cBulan8+B.Debet * cBulan9) as Juli, 0 as Agustus,  " +
                ////     " 0 as September, SUM(B.Debet * cBUlan10+B.Debet * cBUlan11+B.Debet * cBUlan12) as Oktober, 0 as November , 0 as Desember " +
                ////     " FROM vwAnggaranKasDanAnggaran B " +
                ////     " WHERE (B.IIDRekening like '51%' or B.IIDRekening like '4%') AND B.iTAhun=" + _pTahun.ToString() + sWhere;
                //sRet = sRet + " ORDER BY XGroup , Kelompok,IDProgram, IDKegiatan, IIDRekening";

            }
            else
            {


                sRet = sRet + " UNION ALL Select  4 as XGroup,11 as Kelompok,1 as Bold, 1 as Sifat," + _pDinas.ToString() + " as IDDInas, B.IDurusan as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  0 as Kode , 0 as IIDRekening, 0 as IDX,A.sNamaUrusan AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                           " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus, " +
                           " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember" +
                           " FROM mUrusan A INNER JOIN vwAnggaranKasDanAnggaran B ON A.ID = B.IDurusan " +
                           " WHERE B.btJenis= 3   and B.iTAhun= " + _pTahun.ToString() + sWhere + " GROUP BY  B.IDurusan , A.sNamaUrusan ";
                //and B.IIDRekening like '52%'

                sRet = sRet + " UNION ALL   SELECT 4 as XGroup, 11 as Kelomk,1 as Bold, 1 as Sifat," + _pDinas.ToString() + " as IDDinas, B.IDurusan as IDUrusan, B.IDProgram as IDProgram, 0 as IDkegiatan,  A.IDPRogram as Kode , 0 as IIDRekening, 0 as IDX,A.sNamaProgram AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                                " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus, " +
                                " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember" +
                                " FROM tPrograms_A A INNER JOIN vwAnggaranKasDanAnggaran B ON A.iTahun = B.iTahun and A.IDurusan = A.IDurusan and A.IDDinas = B.IDDinas AND A.IDProgram = B.IDProgram " +
                                " WHERE B.btJenis= 3  and B.IDProgram > 0 and B.iTAhun=" + _pTahun.ToString() + sWhere + " GROUP BY  B.IDurusan , B.IDProgram ,  A.IDPRogram,A.sNamaProgram  ";
                sRet = sRet + " UNION Select  4 as XGroup,11 as Kelompok,1 as Bold, 1 as Sifat," + _pDinas.ToString() + " AS IDDInas, B.IDurusan as IDUrusan, B.IDProgram as IDProgram, B.IDkegiatan as IDkegiatan,  B.IDKegiatan as Kode , 0 as IIDRekening, 0 as IDX,A.sNama AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                                  " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus, " +
                                  " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember" +
                                  " FROM tKegiatan_A A INNER JOIN vwAnggaranKasDanAnggaran B ON A.iTahun = B.iTahun and A.IDurusan = A.IDurusan and A.IDDinas = B.IDDinas AND A.IDKegiatan=B.IDKegiatan " +
                                  " WHERE B.btJenis= 3  and B.IDProgram > 0  and  B.iTAhun=" + _pTahun.ToString() + sWhere + " GROUP BY  B.IDurusan, B.IDProgram , B.IDkegiatan ,  B.IDKegiatan,A.sNama ";

                sRet = sRet + " UNION Select  4 as XGroup,11 as Kelompok,1 as Bold, 1 as Sifat," + _pDinas.ToString() + " AS IDDInas,B.IDUrusan, B.IDProgram as IDProgram, B.IDkegiatan,  B.IIDRekening as Kode , B.IIDRekening, LEFT(B.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ") as IDX,A.sNamaRekening AS NAMA,  " +
                         " SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                          " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus,  " +
                          " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember " +
                          " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ," + m_ProfileRekening.LEN5.ToString() + ")= LEFT(B.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ")  " +
                          " WHERE A.btRoot=5    AND B.iTAhun=" + _pTahun.ToString() + sWhere + " GROUP BY B.IDUrusan, B.IDProgram , B.IDkegiatan,  B.IIDRekening , LEFT(B.IIDRekening," + m_ProfileRekening.LEN5.ToString() + "),A.sNamaRekening ";
                //AND A.IIDRekening like '52%'
                sRet = sRet + "  UNION Select  4 as XGroup,15 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode,  0 as IIDRekening, 0 as IDX,'JUMLAH ALOKASI BELANJA LANGSUNG PER BULAN ' AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                            " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus,  " +
                                " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember " +
                                " FROM vwAnggaranKasDanAnggaran B " +
                                " WHERE   B.iTAhun=" + _pTahun.ToString() + sWhere;

                sRet = sRet + " UNION Select   4 as XGroup,16 as Kelompok,1 as Bold, 2 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode,  0 as IIDRekening, 0 as IDX,'JUMLAH ALOKASI BELANJA LANGSUNG PER TRIWULAN ' AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1+ cBulan2+cBulan3) as Januari, 0 as Februari, 0 as Maret, " +
                    " SUM(cBulan4+cBulan5+cBulan6) as April, 0 as Mei, 0 as Juni, SUM(cBulan7+cBulan8+cBulan9) as Juli, 0 as Agustus,  " +
                        " 0 as September, SUM(cBUlan10+cBUlan11+cBUlan12) as Oktober, 0 as November , 0 as Desember " +
                        " FROM vwAnggaranKasDanAnggaran B " +
                        " WHERE  B.iTAhun=" + _pTahun.ToString() + sWhere;


            }
            return sRet;

        }
        private string GetQueryBelanjaLangsung2021(ParameterLaporan p, int _pTahun, int _pDinas, int pUnit, int iTahap, List<SKPD> lstSKPD = null, long idsubkegiatan = 0)
        {

            string sDinas;
            sDinas = GetStringDinas(_pDinas, lstSKPD);

            string sWhere = "";
            //if () { 
            if (p.JenisAnggaran == 0)
            {
                if (lstSKPD == null)
                    if (pUnit == 0)
                    {
                        sWhere = "  and  B.IDDInas =" + p.IDDinas.ToString();
                    }
                    else
                    {
                        sWhere =  "  and b.IDUnit =" + pUnit.ToString() + " and  B.IDDInas =" + p.IDDinas.ToString();
                    }

                else
                    if (pUnit == 0)

                        sWhere = " and b.iTahap =" + iTahap.ToString() + "  and  B.IDDInas in " + sDinas;
                    else
                        sWhere = " and b.iTahap =" + iTahap.ToString() + "  and b.IDUnit =" + pUnit.ToString() + "   and  B.IDDInas in " + sDinas;

            }

            if (p.IDKegiatan > 0)
            {
                if (Tahun <= 2020)
                {
                    if (lstSKPD == null)
                    {
                        sWhere =  " AND B.IDDInas =" + p.IDDinas.ToString() + "   AND B.IDkegiatan = " + p.IDKegiatan.ToString() + " AND B.IDUrusan =" + p.IDUrusan.ToString() + " AND B.IDProgram = " + p.IDProgram.ToString();
                    }
                    else
                        sWhere =  " AND B.IDDInas in " + sDinas + "   AND B.IDkegiatan = " + p.IDKegiatan.ToString() + " AND B.IDUrusan =" + p.IDUrusan.ToString() + " AND B.IDProgram = " + p.IDProgram.ToString();
                }
                else
                {

                    if (lstSKPD == null)
                    {
                        sWhere = " AND B.IDDInas =" + p.IDDinas.ToString() + "  and b.IDUnit =" + pUnit.ToString() + "     AND B.IDkegiatan = " + p.IDKegiatan.ToString() + "   AND B.idsubkegiatan = " + p.IDSubKegiatan.ToString() + " AND B.IDUrusan =" + p.IDUrusan.ToString() + " AND B.IDProgram = " + p.IDProgram.ToString();
                    }
                    else
                        sWhere =" AND B.IDDInas in " + sDinas + "   and b.IDUnit =" + pUnit.ToString() + "   AND B.IDkegiatan = " + p.IDKegiatan.ToString() + "   AND B.idsubkegiatan = " + p.IDSubKegiatan.ToString() + " AND B.IDUrusan =" + p.IDUrusan.ToString() + " AND B.IDProgram = " + p.IDProgram.ToString();

                }
            }

            string sRet = "";
            //'if (p.IDKegiatan == 0)
            

            //sRet = " Select  4 as XGroup,11 as Kelompok,1 as Bold, 1 as Sifat,0 AS IDDInas, 0 as IDUrusan, " +
            // " 0 as IDProgram, 0 as IDkegiatan,  0 as Kode ,0 as idsubkegiatan , 0 as IIDRekening, 0 as IDX,'ALOKASI BELANJA DAN PENGELUARAN PEMBIAYAAN' AS NAMA, "+
            // " isnull(SUM(B.cJumlahOlah),0) as   Anggaran, isnull(SUM(cBulan1),0) as   Januari, isnull(SUM (cBulan2),0) as   Februari,"+
            // " isnull(SUM(cBulan3),0) as   Maret, " +
            //           " isnull(SUM(cBulan4),0) as   APril, isnull(SUM(cBUlan5),0) as   Mei, "+
            //"isnull(SUM(cBUlan6),0) as Juni,  isnull(SUM(cBulan7),0) as   Juli, isnull(SUM(cBulan8),0) as   Agustus, " +
            //           " isnull(SUM(cBulan9),0) as   September, isnull(SUM(cBUlan10),0) as   Oktober, isnull(SUM (cBulan11),0) as   November , isnull(SUM (cBUlan12),0) as   Desember" +
            //           " FROM vwAnggaranKasDanAnggaran B " +
            //           " WHERE B.btJenis= 3 AND  B.IDProgram > 0 and B.iTAhun= " + _pTahun.ToString() + sWhere;
            sRet = " Select  4 as XGroup,11 as Kelompok,1 as Bold, 3 as Sifat,0 AS IDDInas, 0 as IDUrusan, " +
         " 0 as IDProgram, 0 as IDkegiatan,  0 as Kode ,0 as idsubkegiatan , 0 as IIDRekening, 0 as IDX,'ALOKASI BELANJA DAN PENGELUARAN PEMBIAYAAN' AS NAMA, " +
         " 0   Anggaran,0 as   Januari, 0 as   Februari," +
         " 0 as   Maret, " +
                   " 0 as   APril, 0 as   Mei, " +
        "0 as Juni,  0 as   Juli, 0 as   Agustus, " +
                   " 0 as   September, 0 as   Oktober, 0 as   November , 0 as   Desember" +
                   " FROM vwAnggaranKasDanAnggaran B " +
                   " WHERE B.btJenis= 3 AND  B.IDProgram > 0 and B.iTAhun= " + _pTahun.ToString() + sWhere;
            if (lstSKPD == null)
            {


                sRet = sRet + " UNION ALL Select  4 as XGroup,11 as Kelompok,1 as Bold, 1 as Sifat,B.IDDInas, 0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  0 as Kode , 0 as idsubkegiatan,0 as IIDRekening, 0 as IDX,'S K P D' AS NAMA, isnull(SUM(B.cJumlahOlah),0) as  Anggaran, isnull(SUM(cBulan1),0) as  Januari, isnull(SUM (cBulan2),0) as  Februari, isnull(SUM(cBulan3),0) as  Maret, " +
                           " isnull(SUM(cBulan4),0) as  APril, isnull(SUM(cBUlan5),0) as  Mei, isnull(SUM(cBUlan6),0) as Juni, isnull(SUM(cBulan7),0) as  Juli, isnull(SUM(cBulan8),0) as  Agustus, " +
                           " isnull(SUM(cBulan9),0) as  September, isnull(SUM(cBUlan10),0) as  Oktober, isnull(SUM (cBulan11),0) as  November , isnull(SUM (cBUlan12),0) as  Desember" +
                           " FROM mUrusan A INNER JOIN vwAnggaranKasDanAnggaran B ON A.ID = B.IDurusan " +
                           " WHERE B.btJenis= 3  and B.iTAhun= " + _pTahun.ToString() + sWhere + " GROUP BY B.IDDInas";

                sRet = sRet + " UNION ALL Select  4 as XGroup,11 as Kelompok,1 as Bold, 1 as Sifat,B.IDDInas, B.IDurusan as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  0 as Kode , 0 as idsubkegiatan,0 as IIDRekening, 0 as IDX,A.sNamaUrusan AS NAMA, isnull(SUM(B.cJumlahOlah),0) as  Anggaran, isnull(SUM(cBulan1),0) as  Januari, isnull(SUM (cBulan2),0) as  Februari, isnull(SUM(cBulan3),0) as  Maret, " +
                           " isnull(SUM(cBulan4),0) as  APril, isnull(SUM(cBUlan5),0) as  Mei, isnull(SUM(cBUlan6),0) as Juni, isnull(SUM(cBulan7),0) as  Juli, isnull(SUM(cBulan8),0) as  Agustus, " +
                           " isnull(SUM(cBulan9),0) as  September, isnull(SUM(cBUlan10),0) as  Oktober, isnull(SUM (cBulan11),0) as  November , isnull(SUM (cBUlan12),0) as  Desember" +
                           " FROM mUrusan A INNER JOIN vwAnggaranKasDanAnggaran B ON A.ID = B.IDurusan " +
                           " WHERE B.btJenis= 3  and B.iTAhun= " + _pTahun.ToString() + sWhere + " GROUP BY B.IDDInas, B.IDurusan , A.sNamaUrusan ";


                sRet = sRet + " UNION ALL   SELECT 4 as XGroup, 11 as Kelomk,1 as Bold, 1 as Sifat,B.IDDinas, B.IDurusan as IDUrusan, B.IDProgram as IDProgram, 0 as IDkegiatan,  A.IDPRogram as Kode ,0 as idsubkegiatan, 0 as IIDRekening, 0 as IDX,A.sNamaProgram AS NAMA, isnull(SUM(B.cJumlahOlah),0) as  Anggaran, isnull(SUM(cBulan1),0) as  Januari, isnull(SUM (cBulan2),0) as  Februari, isnull(SUM(cBulan3),0) as  Maret, " +
                                " isnull(SUM(cBulan4),0) as  APril, isnull(SUM(cBUlan5),0) as  Mei, isnull(SUM(cBUlan6),0)as Juni, isnull(SUM(cBulan7),0) as  Juli, isnull(SUM(cBulan8),0) as  Agustus, " +
                                " isnull(SUM(cBulan9),0) as  September, isnull(SUM(cBUlan10),0) as  Oktober, isnull(SUM (cBulan11),0) as  November , isnull(SUM (cBUlan12),0) as  Desember" +
                                " FROM tPrograms_A A INNER JOIN vwAnggaranKasDanAnggaran B ON A.iTahun = B.iTahun and A.IDurusan = A.IDurusan and A.IDDinas = B.IDDinas AND A.IDProgram = B.IDProgram " +
                                " WHERE B.btJenis= 3  and B.IDProgram > 0 and B.iTAhun=" + _pTahun.ToString() + sWhere + " GROUP BY B.IDDinas, B.IDurusan , B.IDProgram ,  A.IDPRogram,A.sNamaProgram  ";
                sRet = sRet + " UNION Select  4 as XGroup,11 as Kelompok,1 as Bold, 1 as Sifat,B.IDDInas, B.IDurusan as IDUrusan, B.IDProgram as IDProgram, B.IDkegiatan as IDkegiatan,  B.IDKegiatan as Kode ,0 as idsubkegiatan, 0 as IIDRekening, 0 as IDX,A.sNama AS NAMA, isnull(SUM(B.cJumlahOlah),0) as  Anggaran, isnull(SUM(cBulan1),0) as  Januari, isnull(SUM (cBulan2),0) as  Februari, isnull(SUM(cBulan3),0) as  Maret, " +
                                  " isnull(SUM(cBulan4),0) as  APril, isnull(SUM(cBUlan5),0) as  Mei, isnull(SUM(cBUlan6),0)as Juni, isnull(SUM(cBulan7),0) as  Juli, isnull(SUM(cBulan8),0) as  Agustus, " +
                                  " isnull(SUM(cBulan9),0) as  September, isnull(SUM(cBUlan10),0) as  Oktober, isnull(SUM (cBulan11),0) as  November , isnull(SUM (cBUlan12),0) as  Desember" +
                                  " FROM tKegiatan_A A INNER JOIN vwAnggaranKasDanAnggaran B ON A.iTahun = B.iTahun and A.IDurusan = A.IDurusan and A.IDDinas = B.IDDinas AND A.IDKegiatan=B.IDKegiatan " +
                                  " WHERE B.btJenis= 3  and B.IDProgram > 0  and  B.iTAhun=" + _pTahun.ToString() + sWhere + " GROUP BY B.IDDInas, B.IDurusan, B.IDProgram , B.IDkegiatan ,  B.IDKegiatan,A.sNama ";
                
                sRet = sRet + " UNION Select  4 as XGroup,11 as Kelompok,1 as Bold, 1 as Sifat,B.IDDInas, B.IDurusan as IDUrusan, B.IDProgram as IDProgram, B.IDkegiatan as IDkegiatan,  B.IDKegiatan as Kode ,B.IDSubKegiatan as idsubkegiatan, 0 as IIDRekening, 0 as IDX,A.Nama AS NAMA, isnull(SUM(B.cJumlahOlah),0) as  Anggaran, isnull(SUM(cBulan1),0) as  Januari, isnull(SUM (cBulan2),0) as  Februari, isnull(SUM(cBulan3),0) as  Maret, " +
                                  " isnull(SUM(cBulan4),0) as  APril, isnull(SUM(cBUlan5),0) as  Mei, isnull(SUM(cBUlan6),0)as Juni, isnull(SUM(cBulan7),0) as  Juli, isnull(SUM(cBulan8),0) as  Agustus, " +
                                  " isnull(SUM(cBulan9),0) as  September, isnull(SUM(cBUlan10),0) as  Oktober, isnull(SUM (cBulan11),0) as  November , isnull(SUM (cBUlan12),0) as  Desember" +
                                  " FROM tSubkegiatan A INNER JOIN vwAnggaranKasDanAnggaran B ON A.iTahun = B.iTahun and A.IDurusan = A.IDurusan and A.IDDinas = B.IDDinas AND A.IDKegiatan=B.IDKegiatan  AND A.IDsubKegiatan=B.IDsubKegiatan " +
                                  " WHERE B.btJenis= 3  and B.IDProgram > 0  and  B.iTAhun=" + _pTahun.ToString() + sWhere + " GROUP BY B.IDDInas, B.IDurusan, B.IDProgram , B.IDkegiatan ,  B.IDSubKegiatan,A.Nama ";


            

                sRet = sRet + " UNION Select  4 as XGroup,11 as Kelompok,0 as Bold, 1 as Sifat,B.IDDInas,B.IDUrusan, B.IDProgram as IDProgram, B.IDkegiatan,  A.IIDRekening as Kode , B.IDsubKEgiatan,A.IIDRekening, cast(LEFT(B.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ") as bigint) as  IDX,A.sNamaRekening AS NAMA,  " +
                         " isnull(SUM(B.cJumlahOlah),0) as  Anggaran, isnull(SUM(cBulan1),0) as  Januari, isnull(SUM (cBulan2),0) as  Februari, isnull(SUM(cBulan3),0) as  Maret, " +
                          " isnull(SUM(cBulan4),0) as  APril, isnull(SUM(cBUlan5),0) as  Mei, isnull(SUM(cBUlan6),0)as Juni, isnull(SUM(cBulan7),0) as  Juli, isnull(SUM(cBulan8),0) as  Agustus,  " +
                          " isnull(SUM(cBulan9),0) as  September, isnull(SUM(cBUlan10),0) as  Oktober, isnull(SUM (cBulan11),0) as  November , isnull(SUM (cBUlan12),0) as  Desember " +
                          " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ," + m_ProfileRekening.LEN5.ToString() + ")= LEFT(B.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ")  " +
                          " WHERE A.btRoot=5   AND B.iTAhun=" + _pTahun.ToString() + sWhere + " GROUP BY B.IDDInas,B.IDUrusan, B.IDProgram , B.IDkegiatan, B.IDsubKEgiatan, A.IIDRekening , LEFT(B.IIDRekening," + m_ProfileRekening.LEN5.ToString() + "),A.sNamaRekening ";

                sRet = sRet + " UNION Select  4 as XGroup,11 as Kelompok,0 as Bold, 1 as Sifat,B.IDDInas,B.IDUrusan, B.IDProgram as IDProgram, B.IDkegiatan,  A.IIDRekening as Kode , B.IDsubKEgiatan,A.IIDRekening, cast(LEFT(A.IIDRekening," + m_ProfileRekening.LEN6.ToString() + ") as bigint) as  IDX,A.sNamaRekening AS NAMA,  " +
                         " isnull(SUM(B.cJumlahOlah),0) as  Anggaran, isnull(SUM(cBulan1),0) as  Januari, isnull(SUM (cBulan2),0) as  Februari, isnull(SUM(cBulan3),0) as  Maret, " +
                          " isnull(SUM(cBulan4),0) as  APril, isnull(SUM(cBUlan5),0) as  Mei, isnull(SUM(cBUlan6),0)as Juni, isnull(SUM(cBulan7),0) as  Juli, isnull(SUM(cBulan8),0) as  Agustus,  " +
                          " isnull(SUM(cBulan9),0) as  September, isnull(SUM(cBUlan10),0) as  Oktober, isnull(SUM (cBulan11),0) as  November , isnull(SUM (cBUlan12),0) as  Desember " +
                          " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ," + m_ProfileRekening.LEN6.ToString() + ")= LEFT(B.IIDRekening," + m_ProfileRekening.LEN6.ToString() + ")  " +
                          " WHERE A.btRoot=6   AND B.iTAhun=" + _pTahun.ToString() + sWhere + " GROUP BY B.IDDInas,B.IDUrusan, B.IDProgram , B.IDkegiatan, B.IDsubKEgiatan, A.IIDRekening , LEFT(A.IIDRekening," + m_ProfileRekening.LEN6.ToString() + "),A.sNamaRekening ";


                sRet = sRet + "  UNION Select  4 as XGroup,15 as Kelompok,0 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode, 0 as IDsubKEgiatan,  0 as IIDRekening, 0 as IDX,'JUMLAH ALOKASI BELANJA PER BULAN ' AS NAMA, isnull(SUM(B.cJumlahOlah),0) as  Anggaran, isnull(SUM(cBulan1),0) as  Januari, isnull(SUM (cBulan2),0) as  Februari, isnull(SUM(cBulan3),0) as  Maret, " +
                            " isnull(SUM(cBulan4),0) as  APril, isnull(SUM(cBUlan5),0) as  Mei, isnull(SUM(cBUlan6),0)as Juni, isnull(SUM(cBulan7),0) as  Juli, isnull(SUM(cBulan8),0) as  Agustus,  " +
                                " isnull(SUM(cBulan9),0) as  September, isnull(SUM(cBUlan10),0) as  Oktober, isnull(SUM (cBulan11),0) as  November , isnull(SUM (cBUlan12),0) as  Desember " +
                                " FROM vwAnggaranKasDanAnggaran B " +
                                " WHERE  B.iTAhun=" + _pTahun.ToString() + sWhere + " AND B.btJenis=3 ";

                sRet = sRet + " UNION Select   4 as XGroup,16 as Kelompok,1 as Bold, 2 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode, 0 as IDsubKEgiatan, 0 as IIDRekening, 0 as IDX,'JUMLAH ALOKASI BELANJA  PER TRIWULAN ' AS NAMA, isnull(SUM(B.cJumlahOlah),0) as  Anggaran, isnull(SUM(cBulan1+ cBulan2+cBulan3),0) as  Januari, 0 as Februari, 0 as Maret, " +
                    " isnull(SUM(cBulan4+cBulan5+cBulan6),0) as  April, 0 as Mei, 0 as Juni, isnull(SUM(cBulan7+cBulan8+cBulan9),0) as  Juli, 0 as Agustus,  " +
                        " 0 as September, isnull(SUM(cBUlan10+cBUlan11+cBUlan12),0) as  Oktober, 0 as November , 0 as Desember " +
                        " FROM vwAnggaranKasDanAnggaran B " +
                        " WHERE   B.iTAhun=" + _pTahun.ToString() + sWhere + " AND B.btJenis=3";



                ////sRet = sRet + "  UNION Select  17 as Kelompok,1 as Bold, 2 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode,   0 as IIDRekening, 0 as IDX,'JUMLAH ALOKASI ' AS NAMA, isnull(SUM(B.Debet * B.cJumlahOlah),0) as  Anggaran, isnull(SUM(B.Debet * cBulan1+ B.Debet * cBulan2+B.Debet * cBulan3),0) as  Januari, 0 as Februari, 0 as Maret, " +
                //// " isnull(SUM(B.Debet * cBulan4+ B.Debet * cBulan5+B.Debet * cBulan6),0) as  April, 0 as Mei, 0 as Juni, isnull(SUM(B.Debet * cBulan7+B.Debet * cBulan8+B.Debet * cBulan9),0) as  Juli, 0 as Agustus,  " +
                ////     " 0 as September, isnull(SUM(B.Debet * cBUlan10+B.Debet * cBUlan11+B.Debet * cBUlan12),0) as  Oktober, 0 as November , 0 as Desember " +
                ////     " FROM vwAnggaranKasDanAnggaran B " +
                ////     " WHERE (B.IIDRekening like '51%' or B.IIDRekening like '4%') AND B.iTAhun=" + _pTahun.ToString() + sWhere;
                //sRet = sRet + " ORDER BY XGroup , Kelompok,IDProgram, IDKegiatan, IIDRekening";

            }
            else
            {


                sRet = sRet + " UNION ALL Select  4 as XGroup,11 as Kelompok,1 as Bold, 1 as Sifat," + _pDinas.ToString() + " as IDDInas," +
                    " B.IDurusan as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  0 as Kode ,0 as idsubkegiatan, 0 as IIDRekening, "+
                    "0 as IDX,A.sNamaUrusan AS NAMA, isnull(SUM(B.cJumlahOlah),0) as  Anggaran, isnull(SUM(cBulan1),0) as  Januari, isnull(SUM (cBulan2),0) as  Februari, isnull(SUM(cBulan3),0) as  Maret, " +
                           " isnull(SUM(cBulan4),0) as  APril, isnull(SUM(cBUlan5),0) as  Mei, isnull(SUM(cBUlan6),0)as Juni, isnull(SUM(cBulan7),0) as  Juli, isnull(SUM(cBulan8),0) as  Agustus, " +
                           " isnull(SUM(cBulan9),0) as  September, isnull(SUM(cBUlan10),0) as  Oktober, isnull(SUM (cBulan11),0) as  November , "+
                "isnull(SUM (cBUlan12),0) as  Desember" +
                           " FROM mUrusan A INNER JOIN vwAnggaranKasDanAnggaran B ON A.ID = B.IDurusan " +
                           " WHERE B.btJenis= 3   and B.iTAhun= " + _pTahun.ToString() + sWhere + " GROUP BY  B.IDurusan , A.sNamaUrusan ";
                //and B.IIDRekening like '52%'

                sRet = sRet + " UNION ALL   SELECT 4 as XGroup, 11 as Kelomk,1 as Bold, 1 as Sifat," + _pDinas.ToString() + " as IDDinas," +
                " B.IDurusan as IDUrusan, B.IDProgram as IDProgram, 0 as IDkegiatan,  A.IDPRogram as Kode, 0 as idsubkegiatan, "+
                    "0 as IIDRekening, 0 as IDX,A.sNamaProgram AS NAMA, isnull(SUM(B.cJumlahOlah),0) as  Anggaran, isnull(SUM(cBulan1),0) as  Januari, isnull(SUM (cBulan2),0) as  Februari, isnull(SUM(cBulan3),0) as  Maret, " +
                                " isnull(SUM(cBulan4),0) as  APril, isnull(SUM(cBUlan5),0) as  Mei, isnull(SUM(cBUlan6),0) as Juni, isnull(SUM(cBulan7),0) as  Juli, isnull(SUM(cBulan8),0) as  Agustus, " +
                                " isnull(SUM(cBulan9),0) as  September, isnull(SUM(cBUlan10),0) as  Oktober, isnull(SUM (cBulan11),0) as  November , isnull(SUM (cBUlan12),0) as  Desember" +
                                " FROM tPrograms_A A INNER JOIN vwAnggaranKasDanAnggaran B ON A.iTahun = B.iTahun and A.IDurusan = A.IDurusan and A.IDDinas = B.IDDinas AND A.IDProgram = B.IDProgram " +
                                " WHERE B.btJenis= 3  and B.IDProgram > 0 and B.iTAhun=" + _pTahun.ToString() + sWhere + " GROUP BY  B.IDurusan , B.IDProgram ,  A.IDPRogram,A.sNamaProgram  ";
                sRet = sRet + " UNION Select  4 as XGroup,11 as Kelompok,1 as Bold, 1 as Sifat," + _pDinas.ToString() + " AS IDDInas, B.IDurusan as IDUrusan, B.IDProgram as IDProgram, B.IDkegiatan as IDkegiatan,  B.IDKegiatan as Kode , 0 as idsubkegiatan, 0 as IIDRekening, 0 as IDX,A.sNama AS NAMA, isnull(SUM(B.cJumlahOlah),0) as  Anggaran, isnull(SUM(cBulan1),0) as  Januari, isnull(SUM (cBulan2),0) as  Februari, isnull(SUM(cBulan3),0) as  Maret, " +
                                  " isnull(SUM(cBulan4),0) as  APril, isnull(SUM(cBUlan5),0) as  Mei, isnull(SUM(cBUlan6),0) as Juni, isnull(SUM(cBulan7),0) as  Juli, isnull(SUM(cBulan8),0) as  Agustus, " +
                                  " isnull(SUM(cBulan9),0) as  September, isnull(SUM(cBUlan10),0) as  Oktober, isnull(SUM (cBulan11),0) as  November , isnull(SUM (cBUlan12),0) as  Desember" +
                                  " FROM tKegiatan_A A INNER JOIN vwAnggaranKasDanAnggaran B ON A.iTahun = B.iTahun and A.IDurusan = A.IDurusan and A.IDDinas = B.IDDinas AND A.IDKegiatan=B.IDKegiatan " +
                                  " WHERE B.btJenis= 3  and B.IDProgram > 0  and  B.iTAhun=" + _pTahun.ToString() + sWhere + " GROUP BY  B.IDurusan, B.IDProgram , B.IDkegiatan ,  B.IDKegiatan,A.sNama ";
                sRet = sRet + " UNION Select  4 as XGroup,11 as Kelompok,1 as Bold, 1 as Sifat,B.IDDInas, B.IDurusan as IDUrusan, B.IDProgram as IDProgram, B.IDkegiatan as IDkegiatan,  B.IDKegiatan as Kode ,A.IDSubKegiatan as idsubkegiatan, 0 as IIDRekening, 0 as IDX,A.Nama AS NAMA, isnull(SUM(B.cJumlahOlah),0) as  Anggaran, isnull(SUM(cBulan1),0) as  Januari, isnull(SUM (cBulan2),0) as  Februari, isnull(SUM(cBulan3),0) as  Maret, " +
                               " isnull(SUM(cBulan4),0) as  APril, isnull(SUM(cBUlan5),0) as  Mei, isnull(SUM(cBUlan6),0) as Juni, isnull(SUM(cBulan7),0) as  Juli, isnull(SUM(cBulan8),0) as  Agustus, " +
                               " isnull(SUM(cBulan9),0) as  September, isnull(SUM(cBUlan10),0) as  Oktober, isnull(SUM (cBulan11),0) as  November , isnull(SUM (cBUlan12),0) as  Desember" +
                               " FROM tSubkegiatan A INNER JOIN vwAnggaranKasDanAnggaran B ON A.iTahun = B.iTahun and A.IDurusan = A.IDurusan and A.IDDinas = B.IDDinas AND A.IDKegiatan=B.IDKegiatan  AND A.IDsubKegiatan=B.IDsubKegiatan " +
                               " WHERE B.btJenis= 3  and B.IDProgram > 0  and  B.iTAhun=" + _pTahun.ToString() + sWhere + " GROUP BY B.IDDInas, B.IDurusan, B.IDProgram , B.IDkegiatan ,  B.IDKegiatan,A.IDSubKegiatan,A.Nama ";

                int Root = 5;
                int Left = 8;
                if (Tahun >= 2021)
                {
                    Root = 6;
                    Left = 12;
                }
                sRet = sRet + " UNION Select  4 as XGroup,11 as Kelompok,1 as Bold, 1 as Sifat," + _pDinas.ToString() + " AS IDDInas,B.IDUrusan, B.IDProgram as IDProgram, B.IDkegiatan, B.IDSUbkegiatan as Kode, B.IDSUbkegiatan, B.IIDRekening, LEFT(B.IIDRekening," + Left.ToString() + ") as  IDX,A.sNamaRekening AS NAMA,  " +
                         " isnull(SUM(B.cJumlahOlah),0) as  Anggaran, isnull(SUM(cBulan1),0) as  Januari, isnull(SUM (cBulan2),0) as  Februari, isnull(SUM(cBulan3),0) as  Maret, " +
                          " isnull(SUM(cBulan4),0) as  APril, isnull(SUM(cBUlan5),0) as  Mei, isnull(SUM(cBUlan6),0)as Juni, isnull(SUM(cBulan7),0) as  Juli, isnull(SUM(cBulan8),0) as  Agustus,  " +
                          " isnull(SUM(cBulan9),0) as  September, isnull(SUM(cBUlan10),0) as  Oktober, isnull(SUM (cBulan11),0) as  November , isnull(SUM (cBUlan12),0) as  Desember " +
                          " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ," + Left.ToString() + ")= LEFT(B.IIDRekening," + Left.ToString() + ")  " +
                          " WHERE A.btRoot="+Root.ToString() +"    AND B.iTAhun=" + _pTahun.ToString() + sWhere + " GROUP BY B.IDUrusan, B.IDProgram , B.IDkegiatan,B.IDSUbkegiatan,  B.IIDRekening , LEFT(B.IIDRekening," + Left.ToString() + "),A.sNamaRekening ";
                //AND A.IIDRekening like '520%'
                sRet = sRet + "  UNION Select  4 as XGroup,15 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode,0 as idsubkegiatan,  0 as IIDRekening, 0 as IDX,'JUMLAH ALOKASI BELANJA PER BULAN ' AS NAMA, isnull(SUM(B.cJumlahOlah),0) as  Anggaran, isnull(SUM(cBulan1),0) as  Januari, isnull(SUM (cBulan2),0) as  Februari, isnull(SUM(cBulan3),0) as  Maret, " +
                            " isnull(SUM(cBulan4),0) as  APril, isnull(SUM(cBUlan5),0) as  Mei, isnull(SUM(cBUlan6),0) as Juni, isnull(SUM(cBulan7),0) as  Juli, isnull(SUM(cBulan8),0) as  Agustus,  " +
                                " isnull(SUM(cBulan9),0) as  September, isnull(SUM(cBUlan10),0) as  Oktober, isnull(SUM (cBulan11),0) as  November , isnull(SUM (cBUlan12),0) as  Desember " +
                                " FROM vwAnggaranKasDanAnggaran B " +
                                " WHERE   B.iTAhun=" + _pTahun.ToString() + sWhere;

                sRet = sRet + " UNION Select   4 as XGroup,16 as Kelompok,1 as Bold, 2 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode, 0 as idsubkegiatan, 0 as IIDRekening, 0 as IDX,'JUMLAH ALOKASI BELANJA PER TRIWULAN ' AS NAMA, isnull(SUM(B.cJumlahOlah),0) as  Anggaran, isnull(SUM(cBulan1+ cBulan2+cBulan3),0) as  Januari, 0 as Februari, 0 as Maret, " +
                    " isnull(SUM(cBulan4+cBulan5+cBulan6),0) as  April, 0 as Mei, 0 as Juni, isnull(SUM(cBulan7+cBulan8+cBulan9),0) as  Juli, 0 as Agustus,  " +
                        " 0 as September, isnull(SUM(cBUlan10+cBUlan11+cBUlan12),0) as  Oktober, 0 as November , 0 as Desember " +
                        " FROM vwAnggaranKasDanAnggaran B " +
                        " WHERE  B.iTAhun=" + _pTahun.ToString() + sWhere;


            }
            return sRet;

        }
        public List<AnggaranKas> GetUTKSimpatik(string Kode)
        {
            List<AnggaranKas> lst = new List<AnggaranKas>();
            //"  Select  left (mRekening.iidrEKENING,5) AS rEK, MrEKENING.sNamaRekening as Nama, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
            //     " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus, " +
            //     " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember " +
            //     " FROM tAnggaranKas  INNER JOIN mRekening On Left(tAnggaranKas.IIDRekening,5)= left(mRekening.IIDRekening,5)  " +
            //     " WHERE iTahap= 2 AND iTAhun= 2018 and mRekening.btRoot=4 and mRekening.IIDRekening like '523%' " +
            //     " Group by left (mRekening.iidrEKENING,5) , MrEKENING.sNamaRekening " +
            //     " UNION " +


            SSQL = " Select  left (mRekening.iidrEKENING,3) AS rEK, MrEKENING.sNamaRekening as Nama, SUM(cBulan1) as Januari, " +
                    " SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus, " +
                    " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember " +
                    " FROM tAnggaranKas  INNER JOIN mRekening On Left(tAnggaranKas.IIDRekening,3)= left(mRekening.IIDRekening,3)  " +
                    "inner join tKegiatan_A ON tAnggaranKas.iTahun  = tKegiatan_A.iTahun and  tAnggaranKas.IDDInas = tKegiatan_A.IDDInas and tAnggaranKas.IDUrusan = tKegiatan_A.IDUrusan " +
                    " AND tAnggaranKas.IDProgram = tKegiatan_A.IDProgram and tAnggaranKas.IDKegiatan = tKegiatan_A.IDKegiatan  and tAnggaranKas.btJenis = tKegiatan_A.btJenis  " +
                    "  WHERE iTahap= 2 AND tKegiatan_A.iTAhun= 2018 and mRekening.btRoot=3 and (mRekening.IIDRekening like '5%' or mRekening.IIDRekening like '6%') " +
                    " Group by left (mRekening.iidrEKENING,3) , MrEKENING.sNamaRekening " +
                    " UNION " +
                    "  Select  '99991' AS rEK, 'Tanah' as Nama, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                    " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus, " +
                    " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember " +
                    " FROM tAnggaranKas    " +
                    " WHERE iTahap= 2 AND iTAhun= 2018 and IIDRekening between 52301000 and 52313999  " +

                    " UNION " +
                    "  Select   '99992' AS rEK, 'Peralatan dan Mesin' as Nama, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                    " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus, " +
                    " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember " +
                    " FROM tAnggaranKas  " +
                " WHERE iTahap= 2 AND iTAhun= 2018 and IIDRekening between 52314000  and 52348999  " +
                    " UNION " +
                    "  Select  '99993' AS rEK, 'Gedung dan Bangunan' as Nama, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                    " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus, " +
                    " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember " +
                    " FROM tAnggaranKas  " +
                " WHERE iTahap= 2 AND iTAhun= 2018 and IIDRekening between 52349000 and  52358999 " +
                    " UNION " +
                    "  Select  '99994' AS rEK, 'Jaringan Irigasi ..' as Nama, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                    " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus, " +
                    " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember " +
                    " FROM tAnggaranKas  " +
                " WHERE iTahap= 2 AND iTAhun= 2018 and IIDRekening between 52359000 and 52381999  " +
                    " UNION " +
                    "  Select  '99995' AS rEK, 'Aset Tetap Lainnya ' as Nama, SUM(cBulan1) as Januari, SUM (cBulan2) as Februari, SUM(cBulan3) as Maret, " +
                    " SUM(cBulan4) as APril, SUM(cBUlan5) as Mei, SUM(cBUlan6)as Juni, SUM(cBulan7) as Juli, SUM(cBulan8) as Agustus, " +
                    " SUM(cBulan9) as September, SUM(cBUlan10) as Oktober, SUM (cBulan11) as November , SUM (cBUlan12) as Desember " +
                    " FROM tAnggaranKas   " +
                " WHERE iTahap= 2 AND iTAhun= 2018 and IIDRekening between 52382000 and 52389999  " +

                    " order by Rek ";


            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL);
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    lst = (from DataRow dr in dt.Rows
                           select new AnggaranKas()
                           {
                               Kode = DataFormat.GetString(dr["Rek"]),
                               Nama = DataFormat.GetString(dr["Nama"]),
                               Bulan1 = DataFormat.GetDecimal(dr["Januari"]),
                               Bulan2 = DataFormat.GetDecimal(dr["Februari"]),
                               Bulan3 = DataFormat.GetDecimal(dr["Maret"]),
                               Bulan4 = DataFormat.GetDecimal(dr["April"]),
                               Bulan5 = DataFormat.GetDecimal(dr["Mei"]),
                               Bulan6 = DataFormat.GetDecimal(dr["Juni"]),
                               Bulan7 = DataFormat.GetDecimal(dr["Juli"]),
                               Bulan8 = DataFormat.GetDecimal(dr["Agustus"]),
                               Bulan9 = DataFormat.GetDecimal(dr["September"]),
                               Bulan10 = DataFormat.GetDecimal(dr["Oktober"]),
                               Bulan11 = DataFormat.GetDecimal(dr["November"]),
                               Bulan12 = DataFormat.GetDecimal(dr["Desember"]),
                               Anggaran = DataFormat.GetDecimal(dr["Januari"]) +
                                       DataFormat.GetDecimal(dr["Februari"]) +
                                DataFormat.GetDecimal(dr["Maret"]) +
                               DataFormat.GetDecimal(dr["April"]) +
                               DataFormat.GetDecimal(dr["Mei"]) +
                               DataFormat.GetDecimal(dr["Juni"]) +
                               DataFormat.GetDecimal(dr["Juli"]) +
                               DataFormat.GetDecimal(dr["Agustus"]) +
                               DataFormat.GetDecimal(dr["September"]) +
                               DataFormat.GetDecimal(dr["Oktober"]) +
                               DataFormat.GetDecimal(dr["November"]) +
                               DataFormat.GetDecimal(dr["Desember"])
                           }).ToList();
                }
            }
            return lst;

        }

        public bool PersiapkanAKPerubahan(int awal, int akhir, List<int> lstiSKPD = null)
        {


            //List<SKPD> lstSKPD = new List<SKPD>();
            //SKPDLogic oSKPDLogic = new SKPDLogic(Tahun);
            //if (iSKPD == 0)
            //{

            //    lstSKPD = oSKPDLogic.Get(Tahun);

            //}
            //else
            //{
            //    SKPD oSKPD = oSKPDLogic.GetByID(iSKPD);
            //    lstSKPD.Add(oSKPD);
            //}
            foreach (int id in lstiSKPD)
            {
                List<AnggaranKas> lAK = new List<AnggaranKas>();
                lAK = Get(Tahun, id, awal);
                if (lAK != null)
                {
                    foreach (AnggaranKas ak in lAK)
                    {
                        if (Cek(ak, akhir) == false)
                        {
                            ak.Tahap = akhir;
                            if (SimpanAsIt(ak) == false)
                            {
                                return false;
                            }
                        }



                    }

                }
            }
            return true;



        }
        public bool PersiapkanAKPergeseran()
        {

            List<SKPD> lstSKPD = new List<SKPD>();
            SKPDLogic oSKPDLogic = new SKPDLogic(Tahun);
            lstSKPD = oSKPDLogic.Get(Tahun);
            foreach (SKPD s in lstSKPD)
            {
                List<AnggaranKas> lAK = new List<AnggaranKas>();
                lAK = Get(Tahun, s.ID, 2);
                if (lAK != null)
                {
                    foreach (AnggaranKas ak in lAK)
                    {
                        if (Cek(ak, 3) == false)
                        {
                            ak.Tahap = 3;
                            if (SimpanAsIt(ak) == false)
                            {
                                return false;
                            }
                        }



                    }

                }
            }
            return true;



        }
        public bool PersiapkanAKPerubahan(int iddinas, int idkegiatan, int jenis)
        {

            //List<SKPD> lstSKPD = new List<SKPD>();
            //SKPDLogic oSKPDLogic = new SKPDLogic(Tahun);
            //lstSKPD = oSKPDLogic.Get(Tahun);
            //foreach (SKPD s in lstSKPD)
            //{
            List<AnggaranKas> lAK = new List<AnggaranKas>();
            //   lAK = Get(Tahun, s.ID, 2);
            lAK = Get(Tahun, iddinas, 0, idkegiatan, jenis, 2);
            if (lAK != null)
            {
                foreach (AnggaranKas ak in lAK)
                {
                    if (Cek(ak, 4) == false)
                    {
                        ak.Tahap = 4;
                        if (SimpanAsIt(ak) == false)
                        {
                            return false;
                        }
                    }



                }

            }
            // }
            return true;



        }
        public bool SimpanAsIt(AnggaranKas ak)
        {
            try
            {

                int ikodekategori = DataFormat.GetInteger(ak.IDDinas.ToString().Substring(0, 1));
                int ikodeurusan = DataFormat.GetInteger(ak.IDDinas.ToString().Substring(1, 2));
                int ikodeSKPD = DataFormat.GetInteger(ak.IDDinas.ToString().Substring(3, 2));
                int ikodeUK = 0;
                if (ak.IDUnit > 0)
                {
                    ikodeUK = DataFormat.GetInteger(ak.IDUnit.ToString().Substring(5, 2));
                }


                int ikodekategoriPelaksana = DataFormat.GetInteger(ak.IDDinas.ToString().Substring(0, 1));
                int ikodeurusanPelaksana = DataFormat.GetInteger(ak.IDDinas.ToString().Substring(1, 2));
                int idprogram = 0;
                int idkegiatan = 0;
                int idSUBkegiatan = 0;

                if (ak.Jenis == 3 && ak.IdSubKegiatan > 0)
                {
                    ikodekategoriPelaksana = DataFormat.GetInteger(ak.IdSubKegiatan.ToString().Substring(0, 1));
                    ikodeurusanPelaksana = DataFormat.GetInteger(ak.IdSubKegiatan.ToString().Substring(1, 2));
                    idprogram = DataFormat.GetInteger(ak.IdSubKegiatan.ToString().Substring(3, 2));
                    idkegiatan = DataFormat.GetInteger(ak.IdSubKegiatan.ToString().Substring(5, 3));
                    idSUBkegiatan = DataFormat.GetInteger(ak.IdSubKegiatan.ToString().Substring(8, 2));
                }

                SSQL = "INSERT INTO " + m_sNamaTabel + " (cBulan1,cBulan2,cBulan3,cBulan4," +
                           "cBulan5, cBulan6, cBulan7, cBulan8, cBulan9,cBulan10," +
                           " cBulan11, cBulan12 , iTahun ,IDDInas,IDUrusan, " +
                           " IDProgram ,IDKegiatan ,idsubkegiatan,IIDRekening, btJenis,iTahap," +
                "btkodekategori, btkodeurusan, btkodeskpd, btkodeuk, btkodekategoripelaksana, " +
                "btkodeurusanpelaksana, btidprogram, btidkegiatan, btidsubkegiatan,IDunit) values ( @pcBulan1,@pcBulan2,@pcBulan3,@pcBulan4," +
                           "@pcBulan5, @pcBulan6, @pcBulan7, @pcBulan8, @pcBulan9,@pcBulan10" +
                           ", @pcBulan11, @pcBulan12 , @piTahun ,@pIDDInas,@pIDUrusan, " +
                           " @pIDProgram ,@pIDKegiatan,@pIDSubKegiatan ,@pIIDRekening,@pbtJenis, @piTahap," +
                                "@pbtkodekategori, @pbtkodeurusan, @pbtkodeskpd, @pbtkodeuk, @pbtkodekategoripelaksana, " +
                                "@pbtkodeurusanpelaksana, @pbtidprogram, @pbtidkegiatan, @pbtidsubkegiatan,@pIDUnit)";


                //SSQL = "INSERT INTO " + m_sNamaTabel + " (cBulan1,cBulan2,cBulan3,cBulan4," +
                //           "cBulan5, cBulan6, cBulan7, cBulan8, cBulan9,cBulan10," +
                //           " cBulan11, cBulan12 , iTahun ,IDDInas,IDUrusan, " +
                //           " IDProgram ,IDKegiatan ,idsubkegiatan, IIDRekening, btJenis,iTahap) values ( @pcBulan1,@pcBulan2,@pcBulan3,@pcBulan4," +
                //           "@pcBulan5, @pcBulan6, @pcBulan7, @pcBulan8, @pcBulan9,@pcBulan10" +
                //           ", @pcBulan11, @pcBulan12 , @piTahun ,@pIDDInas,@pIDUrusan, " +
                //           " @pIDProgram ,@pIDKegiatan,@pIDSubKegiatan ,@pIIDRekening,@pbtJenis, @piTahap)";


                DBParameterCollection paramCollection = new DBParameterCollection();
                //paramCollection.Add(new DBParameter("@pID", _newID));
                paramCollection.Add(new DBParameter("@pcBulan1", ak.Bulan1, DbType.Decimal));
                paramCollection.Add(new DBParameter("@pcBulan2", ak.Bulan2, DbType.Decimal));
                paramCollection.Add(new DBParameter("@pcBulan3", ak.Bulan3, DbType.Decimal));
                paramCollection.Add(new DBParameter("@pcBulan4", ak.Bulan4, DbType.Decimal));
                paramCollection.Add(new DBParameter("@pcBulan5", ak.Bulan5, DbType.Decimal));
                paramCollection.Add(new DBParameter("@pcBulan6", ak.Bulan6, DbType.Decimal));
                paramCollection.Add(new DBParameter("@pcBulan7", ak.Bulan7, DbType.Decimal));
                paramCollection.Add(new DBParameter("@pcBulan8", ak.Bulan8, DbType.Decimal));
                paramCollection.Add(new DBParameter("@pcBulan9", ak.Bulan9, DbType.Decimal));
                paramCollection.Add(new DBParameter("@pcBulan10", ak.Bulan10, DbType.Decimal));
                paramCollection.Add(new DBParameter("@pcBulan11", ak.Bulan11, DbType.Decimal));
                paramCollection.Add(new DBParameter("@pcBulan12", ak.Bulan12, DbType.Decimal));
                paramCollection.Add(new DBParameter("@piTahun", ak.Tahun, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDDInas", ak.IDDinas, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDUrusan", ak.IDUrusan, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDProgram", ak.IDProgram, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDKegiatan", ak.IDKegiatan, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDSubKegiatan", ak.IdSubKegiatan, DbType.Int64));
                paramCollection.Add(new DBParameter("@pIIDRekening", ak.IDRekening, DbType.Int64));
                paramCollection.Add(new DBParameter("@pbtJenis", ak.Jenis, DbType.Int16));
                paramCollection.Add(new DBParameter("@piTahap", ak.Tahap, DbType.Int16));
                paramCollection.Add(new DBParameter("@pIDUnit", ak.IDUnit, DbType.Int32));

                //                                int ikodekategori = DataFormat.GetInteger(ak.IDDinas.ToString().Substring(0,1));
                //                int ikodeurusan = DataFormat.GetInteger(ak.IDDinas.ToString().Substring(1,2));
                //int ikodeSKPD = DataFormat.GetInteger(ak.IDDinas.ToString().Substring(3,2));
                //int ikodeUK  = DataFormat.GetInteger(ak.IDDinas.ToString().Substring(5,2));
                //                int ikodekategoriPelaksana = DataFormat.GetInteger(ak.IdSubKegiatan.ToString().Substring(0,1));
                //                int ikodeurusanPelaksana = DataFormat.GetInteger(ak.IdSubKegiatan.ToString().Substring(1,2));
                //                  int idprogram = DataFormat.GetInteger(ak.IdSubKegiatan.ToString().Substring(3,2));
                //                int idkegiatan = DataFormat.GetInteger(ak.IdSubKegiatan.ToString().Substring(5,3));
                //                int idSUBkegiatan = DataFormat.GetInteger(ak.IdSubKegiatan.ToString().Substring(8,2));

                paramCollection.Add(new DBParameter("@pbtkodekategori", ikodekategori, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtkodeurusan", ikodeurusan, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtkodeskpd", ikodeSKPD, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtkodeuk", ikodeUK, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtkodekategoripelaksana", ikodekategoriPelaksana, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtkodeurusanpelaksana", ikodeurusanPelaksana, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtidprogram", idprogram, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtidkegiatan", idkegiatan, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtidsubkegiatan", idSUBkegiatan, DbType.Int32));

                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                return true;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;

            }
        }
        private bool Cek(AnggaranKas ak, int iTahap)
        {
            // true jika sudah ada 


            SSQL = "Select * FROM " + m_sNamaTabel + " WHERE iTahun =" + ak.Tahun.ToString() + " And IDDInas=" + ak.IDDinas.ToString() +
                    " And IDUrusan=" + ak.IDUrusan.ToString() + " AND IDProgram =" + ak.IDProgram.ToString() + " and IDKegiatan =" + ak.IDKegiatan.ToString() +
                    " and idunit = " + ak.IDUnit.ToString() + " and IDSUBKEGIATAN= " + ak.IdSubKegiatan.ToString() + " AND IIDRekening =" + ak.IDRekening.ToString() + " and btJenis =" + ak.Jenis.ToString() + "  and iTahap =" + iTahap.ToString();
            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL);
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            } return false;

        }

        private string GetQueryBelanjaLangsungTW(ParameterLaporan p, int _pTahun, int _pDinas, int pTahap, List<SKPD> lstSKPD = null)
        {
            //Select 11 as Kelompok,1 as Bold, 1 as Sifat, '' as Kode,'Alokasi Belanja Tidak Langsung' 
            //Select 12 as KElompok,1 as Bold, 1 as Sifat,IDRekening as Kode,'Nama Rekening'
            //Select 13 as Kelompok,1 as Bold, 1 as Sifat,'' as Kode,'Jumlah alokasi belanja tidak langsung per bulan'
            //Select 14 as Kelompok,1 as Bold, 2 as Sifat,'' as Kode,'Jumlah alokasi belanja tidak langsung dan pembiayaan pengeluaran per triwulan'
            //Select 15 as Kelompok,1 as Bold, 2 as Sifat,'' as Kode,'Sisa kas setelah dikurangi belanja tidak langsung per triwulan'
            //Select 16 as Kelompok,1 as Bold, 2 as Sifat,'' as Kode,'Sisa kas setelah dikurangi belanja tidak langsung per triwulan'


            string sDinas;
            sDinas = GetStringDinas(_pDinas, lstSKPD);

            string sWhere = "";
            //if () { 
            if (p.JenisAnggaran == 0)
            {
                sWhere = " AND B.iTahap=" + pTahap.ToString() + "  AND B.IDDInas in " + sDinas;
            }

            if (p.IDKegiatan > 0)
            {
                sWhere = " AND  B.iTahap=" + pTahap.ToString() + "AND  B.IDDInas in " + sDinas + " AND B.IDkegiatan = " + p.IDKegiatan.ToString() + " AND B.IDUrusan =" + p.IDUrusan.ToString() + " AND B.IDProgram = " + p.IDProgram.ToString();

            }

            string sRet = "";
            //'if (p.IDKegiatan == 0)


            sRet = " Select 2 as Level, 4 as XGroup,10 as Kelompok,1 as Bold, 1 as Sifat,0 AS IDDInas, 0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  0 as Kode , 52000000 as IIDRekening, 0 as IDX,'Belanja Langsung' AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
                       " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4 " +
                       " FROM vwAnggaranKasDanAnggaran B " +
                       " WHERE B.btJenis= 3 and B.IIDRekening like '52%' and B.IDProgram > 0 and B.iTAhun= " + _pTahun.ToString() + sWhere;

            // "";


            /*  sRet = sRet + " UNION ALL Select  2 as Level,  4 as XGroup,11 as Kelompok,1 as Bold, 1 as Sifat,B.IDDInas, B.IDurusan as IDUrusan, 0 as IDProgram, 0 as IDkegiatan,  0 as Kode , 52000000 as IIDRekening, 0 as IDX,A.sNamaUrusan AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
                         " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4 " +
                         " FROM mUrusan A INNER JOIN vwAnggaranKasDanAnggaran B ON A.ID = B.IDurusan " +
                         " WHERE B.btJenis= 3 and B.IIDRekening like '52%' and B.iTAhun= " + _pTahun.ToString() + sWhere + " GROUP BY B.IDDInas, B.IDurusan , A.sNamaUrusan ";
            
             */
            sRet = sRet + " UNION ALL  SELECT 3 as Level,  4 as XGroup, 11 as Kelompok,1 as Bold, 1 as Sifat,B.IDDinas, B.IDurusan as IDUrusan, B.IDProgram as IDProgram, 0 as IDkegiatan,  A.IDPRogram as Kode , 0 as IIDRekening, 0 as IDX,A.sNamaProgram AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
                       " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4 " +
                            " FROM tPrograms_A A INNER JOIN vwAnggaranKasDanAnggaran B ON A.iTahun = B.iTahun and A.IDurusan = A.IDurusan and A.IDDinas = B.IDDinas AND A.IDProgram = B.IDProgram " +
                            " WHERE B.btJenis= 3 and B.IDProgram > 0 and B.iTAhun=" + _pTahun.ToString() + sWhere + " GROUP BY B.IDDinas, B.IDurusan , B.IDProgram ,  A.IDPRogram,A.sNamaProgram  ";


            sRet = sRet + " UNION Select  4 as Level, 4 as XGroup,11 as Kelompok,1 as Bold, 1 as Sifat,B.IDDInas, B.IDurusan as IDUrusan, B.IDProgram as IDProgram, B.IDkegiatan as IDkegiatan,  B.IDKegiatan as Kode , 0 as IIDRekening, 0 as IDX,A.sNama AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
                      " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                             " FROM tKegiatan_A A INNER JOIN vwAnggaranKasDanAnggaran B ON A.iTahun = B.iTahun and A.IDurusan = A.IDurusan and A.IDDinas = B.IDDinas AND A.IDKegiatan=B.IDKegiatan " +
                             " WHERE B.btJenis= 3 and B.IDProgram > 0  and  B.iTAhun=" + _pTahun.ToString() + sWhere + " GROUP BY B.IDDInas, B.IDurusan, B.IDProgram , B.IDkegiatan ,  B.IDKegiatan,A.sNama ";

            sRet = sRet + " UNION Select  5 as Level, 4 as XGroup,11 as Kelompok,1 as Bold, 1 as Sifat,B.IDDInas,B.IDUrusan, B.IDProgram as IDProgram, B.IDkegiatan,  A.IIDRekening as Kode , A.IIDRekening, LEFT(B.IIDRekening,3) as IDX,A.sNamaRekening AS NAMA,  " +
                    " SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
                      " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                     " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,3)= LEFT(B.IIDRekening,3)  " +
                     " WHERE A.btRoot=3 AND A.IIDRekening like '52%' AND B.iTAhun=" + _pTahun.ToString() + sWhere + " GROUP BY B.IDDInas,B.IDUrusan, B.IDProgram , B.IDkegiatan,  A.IIDRekening , LEFT(B.IIDRekening,3),A.sNamaRekening ";

            sRet = sRet + " UNION Select  6 as Level, 4 as XGroup,11 as Kelompok,1 as Bold, 1 as Sifat,B.IDDInas,B.IDUrusan, B.IDProgram as IDProgram, B.IDkegiatan,  A.IIDRekening as Kode , A.IIDRekening, LEFT(B.IIDRekening,5) as IDX,A.sNamaRekening AS NAMA,  " +
                    " SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
                      " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                     " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON LEFT(A.IIDRekening ,5)= LEFT(B.IIDRekening,5)  " +
                     " WHERE A.btRoot=4 AND A.IIDRekening like '52%' AND B.iTAhun=" + _pTahun.ToString() + sWhere + " GROUP BY B.IDDInas,B.IDUrusan, B.IDProgram , B.IDkegiatan,  A.IIDRekening , LEFT(B.IIDRekening,5),A.sNamaRekening ";

            sRet = sRet + " UNION Select  7 as Level, 4 as XGroup,11 as Kelompok,1 as Bold, 1 as Sifat,B.IDDInas,B.IDUrusan, B.IDProgram as IDProgram, B.IDkegiatan, A.IIDRekening as Kode , A.IIDRekening, B.IIDRekening as IDX,A.sNamaRekening AS NAMA,  " +
                    " SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
                      " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                     " FROM mRekening A INNER JOIN vwAnggaranKasDanAnggaran B ON A.IIDRekening = B.IIDRekening " +
                     " WHERE A.btRoot=5 AND A.IIDRekening like '52%' AND B.iTAhun=" + _pTahun.ToString() + sWhere + " GROUP BY B.IDDInas,B.IDUrusan, B.IDProgram , B.IDkegiatan,  A.IIDRekening , B.IIDRekening,A.sNamaRekening ";

            sRet = sRet + "  UNION Select  2 as Level, 4 as XGroup,15 as Kelompok,1 as Bold, 1 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode,  0 as IIDRekening, 0 as IDX,'JUMLAH ALOKASI BELANJA LANGSUNG ' AS NAMA, SUM(B.cJumlahOlah) as Anggaran,SUM(cBulan1+cBulan2+cBulan3) as Tw1, " +
                       " SUM(cBulan4+cBUlan5+cBUlan6)as Tw2, SUM(cBulan7+cBulan8+cBulan9) as Tw3, SUM(cBUlan10+cBulan11+cBUlan12) as Tw4  " +
                            " FROM vwAnggaranKasDanAnggaran B " +
                            " WHERE B.IIDRekening like '52%' AND B.iTAhun=" + _pTahun.ToString() + sWhere;

            /*
                       //sRet = sRet + " UNION Select   4 as XGroup,16 as Kelompok,1 as Bold, 2 as Sifat,0 as IDDInas,0 as IDUrusan, 0 as IDProgram, 0 as IDkegiatan, '' as Kode,  0 as IIDRekening, 0 as IDX,'JUMLAH ALOKASI BELANJA LANGSUNG PER TRIWULAN ' AS NAMA, SUM(B.cJumlahOlah) as Anggaran, SUM(cBulan1+ cBulan2+cBulan3) as Januari, 0 as Februari, 0 as Maret, " +
                       //    " SUM(cBulan4+cBulan5+cBulan6) as April, 0 as Mei, 0 as Juni, SUM(cBulan7+cBulan8+cBulan9) as Juli, 0 as Agustus,  " +
                       //        " 0 as September, SUM(cBUlan10+cBUlan11+cBUlan12) as Oktober, 0 as November , 0 as Desember " +
                       //        " FROM vwAnggaranKasDanAnggaran B " +
                       //        " WHERE B.IIDRekening like '52%' AND B.iTAhun=" + _pTahun.ToString() + sWhere;

         
                       */
            return sRet;

        }
        private string GetStringDinas(int idDinas, List<SKPD> lstSKPD)
        {
            string strDinas = "(";
            if (lstSKPD != null)
            {
                foreach (SKPD d in lstSKPD)
                {
                    strDinas = strDinas + d.ID.ToString() + ",";
                }
                strDinas = strDinas + "99)";

            }
            else
                strDinas = "(" + idDinas.ToString() + ")";


            return strDinas;

        }
    }
}




