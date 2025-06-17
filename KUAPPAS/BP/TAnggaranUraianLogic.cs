using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using BP;
using DataAccess;
using Formatting;
using System.Data;


namespace BP
{
    public class TAnggaranUraianLogic:BP 
    {
        public TAnggaranUraianLogic(int _pTahun,int profile)
            : base(_pTahun, 0, profile)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tAnggaranUraian_A";

        }
        private void CekKolomID()
        {

        }
        public TAnggaranUraianLogic(int _pDInas, int _pTahun, Single _pTahap, int profile)
            : base(_pTahun, 0, profile)
        {
            m_sNamaTabel = "tAnggaranUraian_A";
            UpdateTableUraian(_pDInas, _pTahun, _pTahap);


        }
        private void UpdateTableUraian(int _pDInas, int _pTahun, Single _pTahap)
        {
            
            int maxID=GetMaxIDEx(_pDInas, _pTahun);

            if (maxID == 1)
            {
                List<TAnggaranUraian> _lst = new List<TAnggaranUraian>();
               
                    //if (_pJenis == 3)
                    //{
                    SSQL = "SELECT tANggaranUraian_A.* FROM tANggaranUraian_A WHERE iTahun =" + _pTahun.ToString() + " AND IDDInas=" + _pDInas.ToString()  + " AND iTahap=" + _pTahap.ToString() +
                            " ORDER BY Jenis, IDUrusan,IDProgram, IDKegiatan,IIDRekening, btUrut";


                    
                    DataTable dt = new DataTable();
                    dt = _dbHelper.ExecuteDataTable(SSQL);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            _lst = (from DataRow dr in dt.Rows
                                    select new TAnggaranUraian()
                                    {

                                        IDKegiatan = DataFormat.GetInteger (dr["IDKegiatan"]),
                                        IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                        IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                        IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                        Jenis = DataFormat.GetSingle(dr["Jenis"]),
                                        IDUraian = DataFormat.GetSingle(dr["ID"]),
                                        NoUrut= DataFormat.GetInteger(dr["btUrut"]),   
                                        Tahap= _pTahap,                                        
                                        StatusUpdate = 1
                                    }).ToList();
                        }
                    }
                    foreach (TAnggaranUraian tu in _lst)
                    {
                        maxID++;
                        SSQL = "UPDATE " + m_sNamaTabel + " SET ID=" + maxID.ToString() + "  WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
                        " AND IDUrusan=@pIDUrusan AND IIDRekening=@pIIDRekening and Jenis=@pbtJenis and btUrut = @pUrut AND iTahap = @piTahap";
                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@piTahun", _pTahun));
                        paramCollection.Add(new DBParameter("@pIDDInas", _pDInas));
                        paramCollection.Add(new DBParameter("@pIDProgram", tu.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", tu.IDKegiatan));
                        paramCollection.Add(new DBParameter("@pIDUrusan", tu.IDUrusan));
                        paramCollection.Add(new DBParameter("@pIIDRekening", tu.IDRekening));
                        paramCollection.Add(new DBParameter("@pbtJenis", tu.Jenis));
                        paramCollection.Add(new DBParameter("@pUrut", tu.NoUrut));
                        paramCollection.Add(new DBParameter("@piTahap", _pTahap, DbType.Int16));

                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                    }
                
                
            }

        }
        public bool Simpan(List<TAnggaranUraian> _lst, int _pDInas, int _pTahun, int _pTahap)
        {


            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;

            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSKPD;
            int _KodeUK;
            int _pIDUrusan;
            int maxID = GetMaxIDEx(_pDInas, _pTahun);

            foreach (TAnggaranUraian o in _lst)
            {
                if (o.IDKegiatan > 0)
                {
                    _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, m_ProfileProgKegiatan.KodeProgram));
                    _KodeKegiatan = DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, m_ProfileProgKegiatan.KodeKegiatan));
                    _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                    _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                    _pIDUrusan = o.IDUrusan;
                }
                else
                {
                    _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                    _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                    _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                    _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    _pIDUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 3));

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
                if (o.NoUrut == 0)
                {
                    _KodeUK = 0;
                }
                if (o.StatusUpdate == 0)
                {
                    maxID++;

                    //_namaKolomUraian1 = "sUraianRKA";   // +
                    //_namaKolomUraian2 = "sUraianRKA"; // +
                    //_namaKolomvolume1 = "VolRKA";  // +
                    //_namaKolomvolume2 = "VolRKA";  // +
                    //_namaKolomharga1 = "cHargaRKA";  // +
                    //_namaKolomharga2 = "cHargaRKA";  // +
                    //_namaKolomjumlah1 = "JumlahRKA";   // /+
                    //_namaKolomjumlah2 = "JumlahRKA";  

                    switch (_pTahap) {

                        case -1:

                            SSQL = "INSERT INTO tAnggaranUraian_A (iTahun,IDDInas,IDProgram,IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK, btIDProgram, btIDKegiatan, btTahapInput, " +
                                   "IIDRekening,btUrut, ID,IDLOkasi,VolPraRKA, VolOlah,VolRKA,VolMurni, volGeser,volRKAP, volABT,sSatuanPRaRKA,sSatuan," +
                                   " cHargaPraRKA, cHargaOlah, cHargaRKA,cHargaMurni, cHargaGeser,cHargaRKAP, cHargaABT,bPPKD,sUraian,sUraianRKA,sUraianMurni, sUraianGeser, sUraianRKAP, sUraianABT, Level,IDstandardHarga,Jenis," +
                                   "JumlahOlah , JumlahRKA,JumlahMurni,JumlahGeser,JumlahRKAP,JumlahABT, iID,showinreport,sLabel,cJumlahYAD, iTahap,cStandardHarga,IDSubKegiatan,IDBarang,IDRKBMD,IDRKBMDBARANG)  values ( " +
                                    "@piTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana, " +
                                   " @pbtKodeUrusanPelaksana,@pbtKodeKategori,@pbtKodeUrusan, @pbtKodeSKPD, @pbtKodeuK, @pbtIDrogram, @pbtIDKegiatan, @pbtTahapInput, " +
                                   "@pIIDRekening,@piNoUrut,@pID,@pIDLOkasi, @piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah," +
                                   " @psSatuan,@psSatuan,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah," +
                                   "@pbPPKD,@psUraian,@psUraian,@psUraian, @psUraian,@psUraian, @psUraian,@psUraian, @pLevel,@pIDstandardHarga,@pJenis," +
                                   " @pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah, " +
                                   "@piID,@pshowinreport,@psLabel,@pcJumlahYAD, @piTahap,@pcStandardHarga,@pIDSubKegiatan,@pIDBarang,@pIDRKBMD,@pIDRKBMDBARANG )";
                            break;

                        case 1:
                                               
                            SSQL = "INSERT INTO tAnggaranUraian_A (iTahun,IDDInas,IDProgram,IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK, btIDProgram, btIDKegiatan, btTahapInput, " +
                                   "IIDRekening,btUrut, ID,IDLOkasi, VolOlah,VolRKA,VolMurni, volGeser,volRKAP, volABT,sSatuan," +
                                   " cHargaOlah, cHargaRKA,cHargaMurni, cHargaGeser,cHargaRKAP, cHargaABT,bPPKD,sUraian,sUraianRKA,sUraianMurni, sUraianGeser, sUraianRKAP, sUraianABT, Level,IDstandardHarga,Jenis," +
                                   "JumlahOlah , JumlahRKA,JumlahMurni,JumlahGeser,JumlahRKAP,JumlahABT, iID,showinreport,sLabel,cJumlahYAD, iTahap,cStandardHarga,IDSubKegiatan,PPNOlah,PPNRKA,PPNMurni,PPNGeser,PPNRKAP,PPNABT,IDBarang,IDRKBMD,IDRKBMDBARANG)  values ( " +
                                    "@piTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana, " +
                                   " @pbtKodeUrusanPelaksana,@pbtKodeKategori,@pbtKodeUrusan, @pbtKodeSKPD, @pbtKodeuK, @pbtIDrogram, @pbtIDKegiatan, @pbtTahapInput, " +
                                   "@pIIDRekening,@piNoUrut,@pID,@pIDLOkasi, @piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah," +
                                   " @psSatuan,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah," +
                                   "@pbPPKD,@psUraian,@psUraian, @psUraian,@psUraian, @psUraian,@psUraian, @pLevel,@pIDstandardHarga,@pJenis," +
                                   " @pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah, " +
                                   "@piID,@pshowinreport,@psLabel,@pcJumlahYAD, @piTahap,@pcStandardHarga,@pIDSubKegiatan,@pPPNOlah,@pPPNOlah,@pPPNOlah,@pPPNOlah,@pPPNOlah,@pPPNOlah,@pIDBarang,@pIDRKBMD,@pIDRKBMDBARANG)";
                            break;
                        case 2:

                            SSQL = "INSERT INTO tAnggaranUraian_A (iTahun,IDDInas,IDProgram,IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK, btIDProgram, btIDKegiatan, btTahapInput, " +
                                   "IIDRekening,btUrut, ID,IDLOkasi, VolOlah,VolRKA,VolMurni, volGeser,volRKAP, volABT,sSatuan," +
                                   " cHargaOlah, cHargaRKA,cHargaMurni, cHargaGeser,cHargaRKAP, cHargaABT,bPPKD,sUraian,sUraianRKA,sUraianMurni, sUraianGeser, sUraianRKAP, sUraianABT, Level,IDstandardHarga,Jenis," +
                                   "JumlahOlah , JumlahRKA,JumlahMurni,JumlahGeser,JumlahRKAP,JumlahABT, iID,showinreport,sLabel,cJumlahYAD, iTahap,cStandardHarga,IDSubKegiatan,PPNOlah,PPNRKA,PPNMurni,PPNGeser,PPNRKAP,PPNABT,IDBarang,IDRKBMD,IDRKBMDBARANG)  values ( " +
                                    "@piTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana, " +
                                   " @pbtKodeUrusanPelaksana,@pbtKodeKategori,@pbtKodeUrusan, @pbtKodeSKPD, @pbtKodeuK, @pbtIDrogram, @pbtIDKegiatan, @pbtTahapInput, " +
                                   "@pIIDRekening,@piNoUrut,@pID,@pIDLOkasi, @piVolOlah,0,@piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah," +
                                   " @psSatuan,@pcHargaOlah,0,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah," +
                                   "@pbPPKD,@psUraian,@psUraian, @psUraian,@psUraian, @psUraian,@psUraian, @pLevel,@pIDstandardHarga,@pJenis," +
                                   " @pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah, " +
                                   "@piID,@pshowinreport,@psLabel,@pcJumlahYAD, @piTahap,@pcStandardHarga ,@pIDSubKegiatan,0,0,@pPPNOlah,@pPPNOlah,@pPPNOlah,@pPPNOlah,@pIDBarang,@pIDRKBMD,@pIDRKBMDBARANG)";
                            break;
                        case 3:
                            //VolOlah,VolRKA,VolMurni, volGeser,volRKAP, volABT

                            SSQL = "INSERT INTO tAnggaranUraian_A (iTahun,IDDInas,IDProgram,IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK, btIDProgram, btIDKegiatan, btTahapInput, " +
                                   "IIDRekening,btUrut, ID,IDLOkasi, VolOlah,VolRKA,VolMurni, volGeser,volRKAP, volABT,sSatuan," +
                                   " cHargaOlah, cHargaRKA,cHargaMurni, cHargaGeser,cHargaRKAP, cHargaABT,bPPKD,sUraian,sUraianRKA,sUraianMurni, sUraianGeser, sUraianRKAP, sUraianABT,Level,IDstandardHarga,Jenis," +
                                   "JumlahOlah , JumlahRKA,JumlahMurni,JumlahGeser,JumlahRKAP,JumlahABT, iID,showinreport,sLabel,cJumlahYAD, iTahap,cStandardHarga ,IDSubKegiatan,PPNOlah,PPNRKA,PPNMurni,PPNGeser,PPNRKAP,PPNABT,IDBarang,IDRKBMD,IDRKBMDBARANG)  values ( " +
                                    "@piTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana, " +
                                   " @pbtKodeUrusanPelaksana,@pbtKodeKategori,@pbtKodeUrusan, @pbtKodeSKPD, @pbtKodeuK, @pbtIDrogram, @pbtIDKegiatan, @pbtTahapInput, " +
                                   "@pIIDRekening,@piNoUrut,@pID,@pIDLOkasi, @piVolOlah,0,0,@piVolOlah,@piVolOlah,@piVolOlah," +
                                   " @psSatuan,@pcHargaOlah,0,0,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah," +
                                   "@pbPPKD,'','', '',@psUraian, @psUraian,@psUraian, @pLevel,@pIDstandardHarga,@pJenis," +
                                   " @pJumlahOlah,0,0,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah, " +
                                   "@piID,@pshowinreport,@psLabel,@pcJumlahYAD, @piTahap,@pcStandardHarga ,@pIDSubKegiatan ,0,0,0,@pPPNOlah,@pPPNOlah,@pPPNOlah,@pIDBarang,@pIDRKBMD,@pIDRKBMDBARANG)";

                            break;

                        case 4:

                            SSQL = "INSERT INTO tAnggaranUraian_A (iTahun,IDDInas,IDProgram,IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK, btIDProgram, btIDKegiatan, btTahapInput, " +
                                   "IIDRekening,btUrut, ID,IDLOkasi, VolOlah,VolRKA,VolMurni, volGeser,volRKAP, volABT,sSatuan," +
                                   " cHargaOlah, cHargaRKA,cHargaMurni, cHargaGeser,cHargaRKAP, cHargaABT,bPPKD,sUraian,sUraianRKA,sUraianMurni,sUraianGEser,sUraianRKAP,sUraianABT, Level,IDstandardHarga,Jenis," +
                                   "JumlahOlah , JumlahRKA,JumlahMurni,JumlahGeser,JumlahRKAP,JumlahABT, iID,showinreport,sLabel,cJumlahYAD, iTahap,cStandardHarga ,IDSubKegiatan,PPNOlah,PPNRKA,PPNMurni,PPNGeser,PPNRKAP,PPNABT,IDBarang,IDRKBMD,IDRKBMDBARANG)  values ( " +
                                    "@piTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana, " +
                                   " @pbtKodeUrusanPelaksana,@pbtKodeKategori,@pbtKodeUrusan, @pbtKodeSKPD, @pbtKodeuK, @pbtIDrogram, @pbtIDKegiatan, @pbtTahapInput, " +
                                   "@pIIDRekening,@piNoUrut,@pID,@pIDLOkasi, @piVolOlah,0,0,0,@piVolOlah,@piVolOlah," +
                                   " @psSatuan,@pcHargaOlah,0,0,0,@pcHargaOlah,@pcHargaOlah," +
                                   "@pbPPKD,'','','','',@psUraian,@psUraian, @pLevel,@pIDstandardHarga,@pJenis," +
                                   " @pJumlahOlah,0,0,0,@pJumlahOlah,@pJumlahOlah, " +
                                   "@piID,@pshowinreport,@psLabel,@pcJumlahYAD, @piTahap,@pcStandardHarga ,@pIDSubKegiatan,0,0,0,0,@pPPNOlah,@pPPNOlah,@pIDBarang,@pIDRKBMD,@pIDRKBMDBARANG )";

                             break;
                    }
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@piTahun", o.Tahun, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pIDUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening, DbType.Int64));
                    paramCollection.Add(new DBParameter("@piNoUrut", o.NoUrut, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pID", maxID, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDLOkasi", o.IDLokasi, DbType.Int32));
                    paramCollection.Add(new DBParameter("@piVolOlah", o.VolOlah, DbType.Decimal ));
                    paramCollection.Add(new DBParameter("@psSatuan", o.Satuan, DbType.String));
                    paramCollection.Add(new DBParameter("@pcHargaOlah", o.HargaOlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));
                    paramCollection.Add(new DBParameter("@psUraian", o.Uraian, DbType.String));
                    paramCollection.Add(new DBParameter("@pLevel", o.Level, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pIDstandardHarga", o.IDStandardHarga, DbType.String));
                    paramCollection.Add(new DBParameter("@pJenis", o.Jenis, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pJumlahOlah", o.JumlahOlah, DbType.Decimal));                    
                    paramCollection.Add(new DBParameter("@piID", o.ID, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pshowinreport", o.ShowInReport, DbType.Int16));
                    paramCollection.Add(new DBParameter("@psLabel", o.Label, DbType.String));
                    paramCollection.Add(new DBParameter("@pcJumlahYAD", o.JumlahYAD, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@piTahap", o.Tahap, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pcStandardHarga", o.StandardHarga, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pIDSubKegiatan", o.IDSubKegiatan, DbType.Int64));
                    paramCollection.Add(new DBParameter("@pPPNOlah", o.PPNOlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pIDBarang", o.IDBarang, DbType.Int64));
                    paramCollection.Add(new DBParameter("@pIDRKBMD", o.IDRKBMD, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDRKBMDBARANG", o.IDRKBMDBArang, DbType.Int32));




                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                }
                else
                {
                    //cHargaMurni= cHarga, VolMurni=VolOlah,JumlahMurni= Jumlah, sUraianMurni= sUraianAPBD,sLabelMurni= sLabelDPA " +
                    //", cHargaGeser= cHarga, VolGeser=VolOlah,JumlahGeser= Jumlah, sUraianGeser= sUraianAPBD,sLabelGeser= sLabelDPA

                    switch (_pTahap)
                    {

                        case 1:
                            //VolOlah,VolRKA,VolMurni, volGeser,volRKAP, volABT
                            SSQL = "UPDATE  tAnggaranUraian_A  SET " +
                                    "btUrut=@pbtUrut,VolOlah=@pVolOlah,VolRKA=@pVolOlah,VolMurni=@pVolOlah,VolGESER=@pVolOlah,VolRKAP=@pVolOlah,VolABT=@pVolOlah," +
                                    "cHargaOlah=@pcHargaOlah, cHargaRKA=@pcHargaOlah, cHargaMurni=@pcHargaOlah, cHargaGeser=@pcHargaOlah, cHargaRKAP=@pcHargaOlah, cHargaABT=@pcHargaOlah, " +
                                    "sSatuan=@psSatuan,sUraianOlah=@psUraian,sUraianRKA=@psUraian,sUraianMurni=@psUraian,sUraianGeser=@psUraian,sUraianRKAP=@psUraian,sUraianABT=@psUraian,Level=@pLevel,IDstandardHarga=@pIDstandardHarga,showinreport=@pshowinreport,sLabel=@psLabel,cJumlahYAD=@pcJumlahYAD, " +
                                    "JumlahOlah =@pJumlahOlah,JumlahRKA =@pJumlahOlah,JumlahMurni =@pJumlahOlah,JumlahGeser =@pJumlahOlah,JumlahRKAP =@pJumlahOlah,JumlahABT =@pJumlahOlah,PPNOlah=@pPPNOlah,PPNRKA=@pPPNOlah,PPNMurni=@pPPNOlah, PPNGeser=@pPPNOlah,PPNRKAP=@pPPNOlah,PPNABT=@pPPNOlah, IDLOkasi=@pIDLOKASI  WHERE " +
                                    "iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDKegiatan= @pIDkegiatan AND IDUrusan=@pIDUrusan and ID=@piID AND IIDRekening=@pIIDRekening  AND Jenis=@pJenis AND bPPKD=@bPPKD AND IDSubKegiatan=@pIDSWubKegiatan";// AND isnull(iTahap,0)=@piTahap";

                            break;
                        case 2:
                            //VolOlah,VolRKA,VolMurni, volGeser,volRKAP, volABT
                            SSQL = "UPDATE  tAnggaranUraian_A  SET " +
                                    "btUrut=@pbtUrut,VolOlah=@pVolOlah,VolRKA=@pVolOlah,VolMurni=@pVolOlah,VolGESER=@pVolOlah,VolRKAP=@pVolOlah,VolABT=@pVolOlah," +
                                    "cHargaOlah=@pcHargaOlah,  cHargaMurni=@pcHargaOlah, cHargaGeser=@pcHargaOlah, cHargaRKAP=@pcHargaOlah, cHargaABT=@pcHargaOlah, " +
                                    "sSatuan=@psSatuan,sUraianOlah=@psUraian,sUraianMurni=@psUraian,sUraianGeser=@psUraian,sUraianRKAP=@psUraian,sUraianABT=@psUraian,Level=@pLevel,IDstandardHarga=@pIDstandardHarga,showinreport=@pshowinreport,sLabel=@psLabel,cJumlahYAD=@pcJumlahYAD, " +
                                    "JumlahOlah =@pJumlahOlah,JumlahRKA =@pJumlahOlah,JumlahMurni =@pJumlahOlah,JumlahGeser =@pJumlahOlah,JumlahRKAP =@pJumlahOlah,JumlahABT =@pJumlahOlah,PPNMurni=@pPPNOlah,PPNGeser=@pPPNOlah,PPNRKAP=@pPPNOlah,PPNABT=@pPPNOlah WHERE " +
                                    "iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDKegiatan= @pIDkegiatan AND IDUrusan=@pIDUrusan and ID=@piID AND IIDRekening=@pIIDRekening  AND Jenis=@pJenis AND bPPKD=@bPPKD  AND IDSubKegiatan=@pIDSWubKegiatan";// AND isnull(iTahap,0)=@piTahap";

                            break;
                        case 3:
                            SSQL = "UPDATE  tAnggaranUraian_A  SET " +
                                    "btUrut=@pbtUrut,VolOlah=@pVolOlah,VolGESER=@pVolOlah,VolRKAP=@pVolOlah,VolABT=@pVolOlah," +
                                    "cHargaOlah=@pcHargaOlah,  cHargaGeser=@pcHargaOlah, cHargaRKAP=@pcHargaOlah, cHargaABT=@pcHargaOlah, " +
                                    "sSatuan=@psSatuan,sUraianGeser=@psUraian,sUraianRKAP=@psUraian,sUraianABT=@psUraian,Level=@pLevel,IDstandardHarga=@pIDstandardHarga,showinreport=@pshowinreport,sLabel=@psLabel,cJumlahYAD=@pcJumlahYAD, " +
                                    "JumlahOlah =@pJumlahOlah,JumlahGeser =@pJumlahOlah,JumlahRKAP =@pJumlahOlah,JumlahABT =@pJumlahOlah,PPNGeser=@pPPNOlah,PPNRKAP=@pPPNOlah,PPNABT=@pPPNOlah  WHERE " +
                                    "iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDKegiatan= @pIDkegiatan AND IDUrusan=@pIDUrusan and ID=@piID AND IIDRekening=@pIIDRekening  AND Jenis=@pJenis AND bPPKD=@bPPKD  AND IDSubKegiatan=@pIDSWubKegiatan";// AND isnull(iTahap,0)=@piTahap";

                            break;
                            
                        case 4:
                            
                            SSQL = "UPDATE  tAnggaranUraian_A  SET " +
                                    "btUrut=@pbtUrut,VolOlah=@pVolOlah,VolRKAP=@pVolOlah,VolABT=@pVolOlah," +
                                    "cHargaOlah=@pcHargaOlah, cHargaRKAP=@pcHargaOlah, cHargaABT=@pcHargaOlah, " +
                                    "sSatuan=@psSatuan,sUraianOlah=@psUraian,sUraianRKAP=@psUraian,sUraianABT=@psUraian,Level=@pLevel,IDstandardHarga=@pIDstandardHarga," +
                                    " showinreport=@pshowinreport,sLabel=@psLabel,cJumlahYAD=@pcJumlahYAD, " +
                                    "JumlahOlah =@pJumlahOlah,JumlahRKAP =@pJumlahOlah,JumlahABT =@pJumlahOlah,PPNRKAP=@pPPNOlah,PPNABT=@pPPNOlah WHERE " +
                                    "iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDKegiatan= @pIDkegiatan AND IDUrusan=@pIDUrusan and ID=@piID AND IIDRekening=@pIIDRekening  AND Jenis=@pJenis AND bPPKD=@bPPKD  AND IDSubKegiatan=@pIDSWubKegiatan";// AND isnull(iTahap,0)=@piTahap";



                            break;
                            
                    }
                    
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pbtUrut", o.NoUrut, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pVolOlah", o.VolOlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcHargaOlah", o.HargaOlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@psSatuan", o.Satuan, DbType.String));
                    paramCollection.Add(new DBParameter("@psUraian", o.Uraian, DbType.String));
                    paramCollection.Add(new DBParameter("@pLevel", o.Level, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pIDstandardHarga", o.IDStandardHarga==null?"":o.IDStandardHarga , DbType.String));
                    paramCollection.Add(new DBParameter("@pJumlahOlah", o.JumlahOlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pshowinreport", o.ShowInReport, DbType.Int32));
                    paramCollection.Add(new DBParameter("@psLabel", o.Label, DbType.String));
                    paramCollection.Add(new DBParameter("@pcJumlahYAD", o.JumlahYAD, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pIDLOKASI", o.IDLokasi, DbType.Int32));
                    paramCollection.Add(new DBParameter("@piTahun", o.Tahun, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pIDUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@piID", o.IDUraian, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening, DbType.Int64));
                    paramCollection.Add(new DBParameter("@pJenis", o.Jenis, DbType.Int16));
                    paramCollection.Add(new DBParameter("@bPPKD", o.PPKD, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pIDSWubKegiatan", o.IDSubKegiatan, DbType.Int64));
                 //   paramCollection.Add(new DBParameter("@pbtIDSubKegiatan", o.IDSubKegiatan % 100, DbType.UInt32));


                    paramCollection.Add(new DBParameter("@pPPNOlah", o.PPNOlah, DbType.Decimal));



                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                }
            }
            return true;
        }
        public bool SimpanSIPD(TAnggaranUraian o, int _pDInas, int _pTahun, int _pTahap)
        {


            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;

            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSKPD;
            int _KodeUK;
            int _pIDUrusan;
            int maxID = GetMaxIDEx(_pDInas, _pTahun);

            
            if (o.IDKegiatan > 0)
                {
                    _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, m_ProfileProgKegiatan.KodeProgram));
                    _KodeKegiatan = DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, m_ProfileProgKegiatan.KodeKegiatan));
                    _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                    _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                    _pIDUrusan = o.IDUrusan;
                }
                else
                {
                    _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                    _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                    _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                    _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    _pIDUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 3));

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
                if (o.NoUrut == 0)
                {
                    _KodeUK = 0;
                }
                if (o.StatusUpdate == 0)
                {
                    maxID++;

                    //_namaKolomUraian1 = "sUraianRKA";   // +
                    //_namaKolomUraian2 = "sUraianRKA"; // +
                    //_namaKolomvolume1 = "VolRKA";  // +
                    //_namaKolomvolume2 = "VolRKA";  // +
                    //_namaKolomharga1 = "cHargaRKA";  // +
                    //_namaKolomharga2 = "cHargaRKA";  // +
                    //_namaKolomjumlah1 = "JumlahRKA";   // /+
                    //_namaKolomjumlah2 = "JumlahRKA";  

                    switch (_pTahap)
                    {

                        case -1:

                            SSQL = "INSERT INTO tAnggaranUraian_A (iTahun,IDDInas,IDProgram,IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK, btIDProgram, btIDKegiatan, btTahapInput, " +
                                   "IIDRekening,btUrut, ID,IDLOkasi,VolPraRKA, VolOlah,VolRKA,VolMurni, volGeser,volRKAP, volABT,sSatuanPRaRKA,sSatuan," +
                                   " cHargaPraRKA, cHargaOlah, cHargaRKA,cHargaMurni, cHargaGeser,cHargaRKAP, cHargaABT,bPPKD,sUraian,sUraianRKA,sUraianMurni, sUraianGeser, sUraianRKAP, sUraianABT, Level,IDstandardHarga,Jenis," +
                                   "JumlahOlah , JumlahRKA,JumlahMurni,JumlahGeser,JumlahRKAP,JumlahABT, iID,showinreport,sLabel,cJumlahYAD, iTahap,cStandardHarga,IDSubKegiatan,IDBarang,IDRKBMD,IDRKBMDBARANG)  values ( " +
                                    "@piTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana, " +
                                   " @pbtKodeUrusanPelaksana,@pbtKodeKategori,@pbtKodeUrusan, @pbtKodeSKPD, @pbtKodeuK, @pbtIDrogram, @pbtIDKegiatan, @pbtTahapInput, " +
                                   "@pIIDRekening,@piNoUrut,@pID,@pIDLOkasi, @piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah," +
                                   " @psSatuan,@psSatuan,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah," +
                                   "@pbPPKD,@psUraian,@psUraian,@psUraian, @psUraian,@psUraian, @psUraian,@psUraian, @pLevel,@pIDstandardHarga,@pJenis," +
                                   " @pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah, " +
                                   "@piID,@pshowinreport,@psLabel,@pcJumlahYAD, @piTahap,@pcStandardHarga,@pIDSubKegiatan,@pIDBarang,@pIDRKBMD,@pIDRKBMDBARANG )";
                            break;

                        case 1:

                            SSQL = "INSERT INTO tAnggaranUraian_A (iTahun,IDDInas,IDProgram,IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK, btIDProgram, btIDKegiatan, btTahapInput, " +
                                   "IIDRekening,btUrut, ID,IDLOkasi, VolOlah,VolRKA,VolMurni, volGeser,volRKAP, volABT,sSatuan," +
                                   " cHargaOlah, cHargaRKA,cHargaMurni, cHargaGeser,cHargaRKAP, cHargaABT,bPPKD,sUraian,sUraianRKA,sUraianMurni, sUraianGeser, sUraianRKAP, sUraianABT, Level,IDstandardHarga,Jenis," +
                                   "JumlahOlah , JumlahRKA,JumlahMurni,JumlahGeser,JumlahRKAP,JumlahABT, iID,showinreport,sLabel,cJumlahYAD, iTahap,cStandardHarga,IDSubKegiatan,PPNOlah,PPNRKA,PPNMurni,PPNGeser,PPNRKAP,PPNABT,IDBarang,IDRKBMD,IDRKBMDBARANG,KodeSKPD,KodeUnit,KodeSH)  values ( " +
                                    "@piTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana, " +
                                   " @pbtKodeUrusanPelaksana,@pbtKodeKategori,@pbtKodeUrusan, @pbtKodeSKPD, @pbtKodeuK, @pbtIDrogram, @pbtIDKegiatan, @pbtTahapInput, " +
                                   "@pIIDRekening,@piNoUrut,@pID,@pIDLOkasi, @piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah," +
                                   " @psSatuan,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah," +
                                   "@pbPPKD,@psUraian,@psUraian, @psUraian,@psUraian, @psUraian,@psUraian, @pLevel,@pIDstandardHarga,@pJenis," +
                                   " @pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah, " +
                                   "@piID,@pshowinreport,@psLabel,@pcJumlahYAD, @piTahap,@pcStandardHarga,@pIDSubKegiatan,@pPPNOlah,@pPPNOlah,@pPPNOlah,@pPPNOlah,@pPPNOlah,@pPPNOlah,@pIDBarang,@pIDRKBMD,@pIDRKBMDBARANG,@pKodeSKPD,@pKodeUnit,@pKodeSH)";
                            break;
                        case 2:

                            SSQL = "INSERT INTO tAnggaranUraian_A (iTahun,IDDInas,IDProgram,IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK, btIDProgram, btIDKegiatan, btTahapInput, " +
                                   "IIDRekening,btUrut, ID,IDLOkasi, VolOlah,VolRKA,VolMurni, volGeser,volRKAP, volABT,sSatuan," +
                                   " cHargaOlah, cHargaRKA,cHargaMurni, cHargaGeser,cHargaRKAP, cHargaABT,bPPKD,sUraian,sUraianRKA,sUraianMurni, sUraianGeser, sUraianRKAP, sUraianABT, Level,IDstandardHarga,Jenis," +
                                   "JumlahOlah , JumlahRKA,JumlahMurni,JumlahGeser,JumlahRKAP,JumlahABT, iID,showinreport,sLabel,cJumlahYAD, iTahap,cStandardHarga,IDSubKegiatan,PPNOlah,PPNRKA,PPNMurni,PPNGeser,PPNRKAP,PPNABT,IDBarang,IDRKBMD,IDRKBMDBARANG)  values ( " +
                                    "@piTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana, " +
                                   " @pbtKodeUrusanPelaksana,@pbtKodeKategori,@pbtKodeUrusan, @pbtKodeSKPD, @pbtKodeuK, @pbtIDrogram, @pbtIDKegiatan, @pbtTahapInput, " +
                                   "@pIIDRekening,@piNoUrut,@pID,@pIDLOkasi, @piVolOlah,0,@piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah," +
                                   " @psSatuan,@pcHargaOlah,0,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah," +
                                   "@pbPPKD,@psUraian,@psUraian, @psUraian,@psUraian, @psUraian,@psUraian, @pLevel,@pIDstandardHarga,@pJenis," +
                                   " @pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah, " +
                                   "@piID,@pshowinreport,@psLabel,@pcJumlahYAD, @piTahap,@pcStandardHarga ,@pIDSubKegiatan,0,0,@pPPNOlah,@pPPNOlah,@pPPNOlah,@pPPNOlah,@pIDBarang,@pIDRKBMD,@pIDRKBMDBARANG)";
                            break;
                        case 3:
                            //VolOlah,VolRKA,VolMurni, volGeser,volRKAP, volABT

                            SSQL = "INSERT INTO tAnggaranUraian_A (iTahun,IDDInas,IDProgram,IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK, btIDProgram, btIDKegiatan, btTahapInput, " +
                                   "IIDRekening,btUrut, ID,IDLOkasi, VolOlah,VolRKA,VolMurni, volGeser,volRKAP, volABT,sSatuan," +
                                   " cHargaOlah, cHargaRKA,cHargaMurni, cHargaGeser,cHargaRKAP, cHargaABT,bPPKD,sUraian,sUraianRKA,sUraianMurni, sUraianGeser, sUraianRKAP, sUraianABT,Level,IDstandardHarga,Jenis," +
                                   "JumlahOlah , JumlahRKA,JumlahMurni,JumlahGeser,JumlahRKAP,JumlahABT, iID,showinreport,sLabel,cJumlahYAD, iTahap,cStandardHarga ,IDSubKegiatan,PPNOlah,PPNRKA,PPNMurni,PPNGeser,PPNRKAP,PPNABT,IDBarang,IDRKBMD,IDRKBMDBARANG)  values ( " +
                                    "@piTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana, " +
                                   " @pbtKodeUrusanPelaksana,@pbtKodeKategori,@pbtKodeUrusan, @pbtKodeSKPD, @pbtKodeuK, @pbtIDrogram, @pbtIDKegiatan, @pbtTahapInput, " +
                                   "@pIIDRekening,@piNoUrut,@pID,@pIDLOkasi, @piVolOlah,0,0,@piVolOlah,@piVolOlah,@piVolOlah," +
                                   " @psSatuan,@pcHargaOlah,0,0,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah," +
                                   "@pbPPKD,'','', '',@psUraian, @psUraian,@psUraian, @pLevel,@pIDstandardHarga,@pJenis," +
                                   " @pJumlahOlah,0,0,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah, " +
                                   "@piID,@pshowinreport,@psLabel,@pcJumlahYAD, @piTahap,@pcStandardHarga ,@pIDSubKegiatan ,0,0,0,@pPPNOlah,@pPPNOlah,@pPPNOlah,@pIDBarang,@pIDRKBMD,@pIDRKBMDBARANG)";

                            break;

                        case 4:

                            SSQL = "INSERT INTO tAnggaranUraian_A (iTahun,IDDInas,IDProgram,IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK, btIDProgram, btIDKegiatan, btTahapInput, " +
                                   "IIDRekening,btUrut, ID,IDLOkasi, VolOlah,VolRKA,VolMurni, volGeser,volRKAP, volABT,sSatuan," +
                                   " cHargaOlah, cHargaRKA,cHargaMurni, cHargaGeser,cHargaRKAP, cHargaABT,bPPKD,sUraian,sUraianRKA,sUraianMurni,sUraianGEser,sUraianRKAP,sUraianABT, Level,IDstandardHarga,Jenis," +
                                   "JumlahOlah , JumlahRKA,JumlahMurni,JumlahGeser,JumlahRKAP,JumlahABT, iID,showinreport,sLabel,cJumlahYAD, iTahap,cStandardHarga ,IDSubKegiatan,PPNOlah,PPNRKA,PPNMurni,PPNGeser,PPNRKAP,PPNABT,IDBarang,IDRKBMD,IDRKBMDBARANG)  values ( " +
                                    "@piTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana, " +
                                   " @pbtKodeUrusanPelaksana,@pbtKodeKategori,@pbtKodeUrusan, @pbtKodeSKPD, @pbtKodeuK, @pbtIDrogram, @pbtIDKegiatan, @pbtTahapInput, " +
                                   "@pIIDRekening,@piNoUrut,@pID,@pIDLOkasi, @piVolOlah,0,0,0,@piVolOlah,@piVolOlah," +
                                   " @psSatuan,@pcHargaOlah,0,0,0,@pcHargaOlah,@pcHargaOlah," +
                                   "@pbPPKD,'','','','',@psUraian,@psUraian, @pLevel,@pIDstandardHarga,@pJenis," +
                                   " @pJumlahOlah,0,0,0,@pJumlahOlah,@pJumlahOlah, " +
                                   "@piID,@pshowinreport,@psLabel,@pcJumlahYAD, @piTahap,@pcStandardHarga ,@pIDSubKegiatan,0,0,0,0,@pPPNOlah,@pPPNOlah,@pIDBarang,@pIDRKBMD,@pIDRKBMDBARANG )";

                            break;
                    }
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@piTahun", o.Tahun, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pIDUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening, DbType.Int64));
                    paramCollection.Add(new DBParameter("@piNoUrut", o.NoUrut, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pID", maxID, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDLOkasi", o.IDLokasi, DbType.Int32));
                    paramCollection.Add(new DBParameter("@piVolOlah", o.VolOlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@psSatuan", o.Satuan, DbType.String));
                    paramCollection.Add(new DBParameter("@pcHargaOlah", o.HargaOlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));
                    paramCollection.Add(new DBParameter("@psUraian", o.Uraian, DbType.String));
                    paramCollection.Add(new DBParameter("@pLevel", 1, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pIDstandardHarga", o.KodeSH, DbType.String));
                    paramCollection.Add(new DBParameter("@pJenis", o.Jenis, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pJumlahOlah", o.JumlahOlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@piID", o.ID, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pshowinreport", o.ShowInReport, DbType.Int16));
                    paramCollection.Add(new DBParameter("@psLabel", "-", DbType.String));
                    paramCollection.Add(new DBParameter("@pcJumlahYAD",0, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@piTahap", o.Tahap, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pcStandardHarga", o.StandardHarga, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pIDSubKegiatan", o.IDSubKegiatan, DbType.Int64));
                    paramCollection.Add(new DBParameter("@pPPNOlah", o.PPNOlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pIDBarang", o.IDBarang, DbType.Int64));
                    paramCollection.Add(new DBParameter("@pIDRKBMD", o.IDRKBMD, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDRKBMDBARANG", o.IDRKBMDBArang, DbType.Int32));

                    paramCollection.Add(new DBParameter("@pKodeSKPD", o.KodeSKPDSIPD, DbType.String));
                    paramCollection.Add(new DBParameter("@pKodeUnit", o.KodeUnit , DbType.String));
                    paramCollection.Add(new DBParameter("@pKodeSH", o.KodeSH , DbType.String));



                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                }
                else
                {
                    //cHargaMurni= cHarga, VolMurni=VolOlah,JumlahMurni= Jumlah, sUraianMurni= sUraianAPBD,sLabelMurni= sLabelDPA " +
                    //", cHargaGeser= cHarga, VolGeser=VolOlah,JumlahGeser= Jumlah, sUraianGeser= sUraianAPBD,sLabelGeser= sLabelDPA

                    switch (_pTahap)
                    {

                        case 1:
                            //VolOlah,VolRKA,VolMurni, volGeser,volRKAP, volABT
                            SSQL = "UPDATE  tAnggaranUraian_A  SET " +
                                    "btUrut=@pbtUrut,VolOlah=@pVolOlah,VolRKA=@pVolOlah,VolMurni=@pVolOlah,VolGESER=@pVolOlah,VolRKAP=@pVolOlah,VolABT=@pVolOlah," +
                                    "cHargaOlah=@pcHargaOlah, cHargaRKA=@pcHargaOlah, cHargaMurni=@pcHargaOlah, cHargaGeser=@pcHargaOlah, cHargaRKAP=@pcHargaOlah, cHargaABT=@pcHargaOlah, " +
                                    "sSatuan=@psSatuan,sUraianOlah=@psUraian,sUraianRKA=@psUraian,sUraianMurni=@psUraian,sUraianGeser=@psUraian,sUraianRKAP=@psUraian,sUraianABT=@psUraian,Level=@pLevel,IDstandardHarga=@pIDstandardHarga,showinreport=@pshowinreport,sLabel=@psLabel,cJumlahYAD=@pcJumlahYAD, " +
                                    "JumlahOlah =@pJumlahOlah,JumlahRKA =@pJumlahOlah,JumlahMurni =@pJumlahOlah,JumlahGeser =@pJumlahOlah,JumlahRKAP =@pJumlahOlah,JumlahABT =@pJumlahOlah,PPNOlah=@pPPNOlah,PPNRKA=@pPPNOlah,PPNMurni=@pPPNOlah, PPNGeser=@pPPNOlah,PPNRKAP=@pPPNOlah,PPNABT=@pPPNOlah, IDLOkasi=@pIDLOKASI  WHERE " +
                                    "iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDKegiatan= @pIDkegiatan AND IDUrusan=@pIDUrusan and ID=@piID AND IIDRekening=@pIIDRekening  AND Jenis=@pJenis AND bPPKD=@bPPKD AND IDSubKegiatan=@pIDSWubKegiatan";// AND isnull(iTahap,0)=@piTahap";

                            break;
                        case 2:
                            //VolOlah,VolRKA,VolMurni, volGeser,volRKAP, volABT
                            SSQL = "UPDATE  tAnggaranUraian_A  SET " +
                                    "btUrut=@pbtUrut,VolOlah=@pVolOlah,VolRKA=@pVolOlah,VolMurni=@pVolOlah,VolGESER=@pVolOlah,VolRKAP=@pVolOlah,VolABT=@pVolOlah," +
                                    "cHargaOlah=@pcHargaOlah,  cHargaMurni=@pcHargaOlah, cHargaGeser=@pcHargaOlah, cHargaRKAP=@pcHargaOlah, cHargaABT=@pcHargaOlah, " +
                                    "sSatuan=@psSatuan,sUraianOlah=@psUraian,sUraianMurni=@psUraian,sUraianGeser=@psUraian,sUraianRKAP=@psUraian,sUraianABT=@psUraian,Level=@pLevel,IDstandardHarga=@pIDstandardHarga,showinreport=@pshowinreport,sLabel=@psLabel,cJumlahYAD=@pcJumlahYAD, " +
                                    "JumlahOlah =@pJumlahOlah,JumlahRKA =@pJumlahOlah,JumlahMurni =@pJumlahOlah,JumlahGeser =@pJumlahOlah,JumlahRKAP =@pJumlahOlah,JumlahABT =@pJumlahOlah,PPNMurni=@pPPNOlah,PPNGeser=@pPPNOlah,PPNRKAP=@pPPNOlah,PPNABT=@pPPNOlah WHERE " +
                                    "iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDKegiatan= @pIDkegiatan AND IDUrusan=@pIDUrusan and ID=@piID AND IIDRekening=@pIIDRekening  AND Jenis=@pJenis AND bPPKD=@bPPKD  AND IDSubKegiatan=@pIDSWubKegiatan";// AND isnull(iTahap,0)=@piTahap";

                            break;
                        case 3:
                            SSQL = "UPDATE  tAnggaranUraian_A  SET " +
                                    "btUrut=@pbtUrut,VolOlah=@pVolOlah,VolGESER=@pVolOlah,VolRKAP=@pVolOlah,VolABT=@pVolOlah," +
                                    "cHargaOlah=@pcHargaOlah,  cHargaGeser=@pcHargaOlah, cHargaRKAP=@pcHargaOlah, cHargaABT=@pcHargaOlah, " +
                                    "sSatuan=@psSatuan,sUraianGeser=@psUraian,sUraianRKAP=@psUraian,sUraianABT=@psUraian,Level=@pLevel,IDstandardHarga=@pIDstandardHarga,showinreport=@pshowinreport,sLabel=@psLabel,cJumlahYAD=@pcJumlahYAD, " +
                                    "JumlahOlah =@pJumlahOlah,JumlahGeser =@pJumlahOlah,JumlahRKAP =@pJumlahOlah,JumlahABT =@pJumlahOlah,PPNGeser=@pPPNOlah,PPNRKAP=@pPPNOlah,PPNABT=@pPPNOlah  WHERE " +
                                    "iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDKegiatan= @pIDkegiatan AND IDUrusan=@pIDUrusan and ID=@piID AND IIDRekening=@pIIDRekening  AND Jenis=@pJenis AND bPPKD=@bPPKD  AND IDSubKegiatan=@pIDSWubKegiatan";// AND isnull(iTahap,0)=@piTahap";

                            break;

                        case 4:

                            SSQL = "UPDATE  tAnggaranUraian_A  SET " +
                                    "btUrut=@pbtUrut,VolOlah=@pVolOlah,VolRKAP=@pVolOlah,VolABT=@pVolOlah," +
                                    "cHargaOlah=@pcHargaOlah, cHargaRKAP=@pcHargaOlah, cHargaABT=@pcHargaOlah, " +
                                    "sSatuan=@psSatuan,sUraianOlah=@psUraian,sUraianRKAP=@psUraian,sUraianABT=@psUraian,Level=@pLevel,IDstandardHarga=@pIDstandardHarga," +
                                    " showinreport=@pshowinreport,sLabel=@psLabel,cJumlahYAD=@pcJumlahYAD, " +
                                    "JumlahOlah =@pJumlahOlah,JumlahRKAP =@pJumlahOlah,JumlahABT =@pJumlahOlah,PPNRKAP=@pPPNOlah,PPNABT=@pPPNOlah WHERE " +
                                    "iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDKegiatan= @pIDkegiatan AND IDUrusan=@pIDUrusan and ID=@piID AND IIDRekening=@pIIDRekening  AND Jenis=@pJenis AND bPPKD=@bPPKD  AND IDSubKegiatan=@pIDSWubKegiatan";// AND isnull(iTahap,0)=@piTahap";



                            break;

                    }

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pbtUrut", o.NoUrut, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pVolOlah", o.VolOlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcHargaOlah", o.HargaOlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@psSatuan", o.Satuan, DbType.String));
                    paramCollection.Add(new DBParameter("@psUraian", o.Uraian, DbType.String));
                    paramCollection.Add(new DBParameter("@pLevel", o.Level, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pIDstandardHarga", o.IDStandardHarga == null ? "" : o.IDStandardHarga, DbType.String));
                    paramCollection.Add(new DBParameter("@pJumlahOlah", o.JumlahOlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pshowinreport", o.ShowInReport, DbType.Int32));
                    paramCollection.Add(new DBParameter("@psLabel", o.Label, DbType.String));
                    paramCollection.Add(new DBParameter("@pcJumlahYAD", o.JumlahYAD, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pIDLOKASI", o.IDLokasi, DbType.Int32));
                    paramCollection.Add(new DBParameter("@piTahun", o.Tahun, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pIDUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@piID", o.IDUraian, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening, DbType.Int64));
                    paramCollection.Add(new DBParameter("@pJenis", o.Jenis, DbType.Int16));
                    paramCollection.Add(new DBParameter("@bPPKD", o.PPKD, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pIDSWubKegiatan", o.IDSubKegiatan, DbType.Int64));
                    //   paramCollection.Add(new DBParameter("@pbtIDSubKegiatan", o.IDSubKegiatan % 100, DbType.UInt32));


                    paramCollection.Add(new DBParameter("@pPPNOlah", o.PPNOlah, DbType.Decimal));



                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                }
            
            return true;
        }
        public void ExportKeAnggaranRekening(int idDinas, int Tahun)
        {
            try
            {
                SSQL="insert into tAnggaranRekening_A(iTahun, IDDInas,IDUnit , IDUrusan,IDprogram, IDkegiatan,IDSUbKegiatan,"+
                " btKodeKategori, btKodeUrusan,btKodeSKPD, btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana,"+
                " btIDprogram, btIDKegiatan , btIDSUBKegiatan, btJenis, IIDRekening,cJumlahMurni, cJumlahGeser,cJumlahRKAP,cJumlahABT,cDPA)"+
                " Select iTahun, IDDInas,IDDInas + btKodeUK as IDUnit , IDUrusan,IDprogram, IDkegiatan,IDSUbKegiatan,"+
                " btKodeKategori, btKodeUrusan,btKodeSKPD, btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana,"+
                " btIDprogram, btIDKegiatan , IDSUbKegiatan %1000 as btIDSUBKegiatan, 3 as btJenis, IIDRekening,"+
                " Sum(JumlahMurni) as cJumlahMurni, Sum(JumlahMurni) as cJumlahGeser,Sum(JumlahMurni) as cJumlahRKAP,Sum(JumlahMurni) as cJumlahABT,"+
                " Sum(JumlahMurni) as cDPA  from tAnggaranUraian_A  " +
                " where IDDInas =" + idDinas.ToString() + "and iTahun = " + Tahun.ToString() +
                " group by iTahun, IDDInas,IDUrusan,IDprogram, IDkegiatan,IDSUbKegiatan,"+
                " btKodeKategori, btKodeUrusan,btKodeSKPD, btKodeUK, btKodeKategoriPelaksana, btKodeUrusanPelaksana,btIDprogram, btIDKegiatan ,  IIDRekening";

                _dbHelper.ExecuteNonQuery(SSQL);


            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
            }
        }

        private bool CekUraian (TAnggaranUraian o){
        
            try
            {
                SSQL = "Select count(*) from tAnggaranUraian_A  WHERE " +
                   "iTahun=@Tahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDKegiatan= @pIDkegiatan " +
                    " AND IDUrusan=@pIDUrusan  AND IIDRekening=@pIIDRekening  AND Jenis=@pJenis " +
                    " AND btKodeUK =@KODEUK AND IDSubKegiatan=@pIDSubKegiatan AND IDstandardHarga=@STANDARDHARGA";


 DBParameterCollection paramCollection = new DBParameterCollection();

 paramCollection.Add(new DBParameter("@Tahun", o.Tahun, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan, DbType.Int32));
   
                paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening, DbType.Int64));
                paramCollection.Add(new DBParameter("@pJenis", o.Jenis, DbType.Int16));
    
                paramCollection.Add(new DBParameter("@pIDSubKegiatan", o.IDSubKegiatan, DbType.Int64));
                paramCollection.Add(new DBParameter("@KODEUK", o.KodeUK, DbType.Int32));
                paramCollection.Add(new DBParameter("@STANDARDHARGA", o.IDStandardHarga, DbType.String));

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
                        return false ;
                    }
                }

                return false;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }


        }
        public bool SimpanSIPD2024(TAnggaranUraian o, int tahap=2)
        {


            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;

            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSKPD;
            int _KodeUK;
            int _pIDUrusan;
            //int maxID =  GetMaxIDEx(_pDInas, _pTahun);


            if (o.IDKegiatan > 0)
            {
                _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, m_ProfileProgKegiatan.KodeProgram));
                _KodeKegiatan = DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, m_ProfileProgKegiatan.KodeKegiatan));
                _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                _pIDUrusan = o.IDUrusan;
            }
            else
            {
                _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                _pIDUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 3));

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
            if (o.NoUrut == 0)
            {
                _KodeUK = 0;
            }

            if (CekUraian (o)== false)
            {



                if (tahap == 2)
                {

                    SSQL = "INSERT INTO tAnggaranUraian_A (iTahun,IDDInas,IDProgram,IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                                 " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK, btIDProgram, btIDKegiatan, btTahapInput, " +//14
                                  "IIDRekening,btUrut, ID,IDLOkasi, VolOlah,VolRKA,VolMurni, volGeser,volRKAP, volABT,sSatuan," +//11
                                  "cHargaOlah, cHargaRKA,cHargaMurni, cHargaGeser,cHargaRKAP, cHargaABT,bPPKD,sUraian,sUraianRKA,sUraianMurni," +//10
                                  "sUraianGeser, sUraianRKAP, sUraianABT, Level,IDstandardHarga,Jenis," +
                                  "JumlahOlah , JumlahRKA,JumlahMurni,JumlahGeser,JumlahRKAP,JumlahABT, iID,showinreport,sLabel,cJumlahYAD, iTahap,cStandardHarga,IDSubKegiatan,IDBarang,IDRKBMD,IDRKBMDBARANG)  values ( " +
                                   "@piTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana, " +
                                  " @pbtKodeUrusanPelaksana,@pbtKodeKategori,@pbtKodeUrusan, @pbtKodeSKPD, @pbtKodeuK, @pbtIDrogram, @pbtIDKegiatan, @pbtTahapInput, " +
                                  "@pIIDRekening,@piNoUrut,@pID,@pIDLOkasi, @piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah,@psSatuan," + //11
                                  " @pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pbPPKD,@psUraian,@psUraian,@psUraian, " + //10
                                   "@psUraian,@psUraian, @psUraian, @pLevel,@pIDstandardHarga,@pJenis," +
                                  " @pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah, " +
                                  "@piID,@pshowinreport,@psLabel,@pcJumlahYAD, @piTahap,@pcStandardHarga,@pIDSubKegiatan,@pIDBarang,@pIDRKBMD,@pIDRKBMDBARANG )";
                }
                if (tahap == 3)
                {
                    SSQL = "INSERT INTO tAnggaranUraian_A (iTahun,IDDInas,IDProgram,IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                                 " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK, btIDProgram, btIDKegiatan, btTahapInput, " +//14
                                  "IIDRekening,btUrut, ID,IDLOkasi, VolOlah,VolRKA,VolMurni, volGeser,volRKAP, volABT,sSatuan," +//11
                                  "cHargaOlah, cHargaRKA,cHargaMurni, cHargaGeser,cHargaRKAP, cHargaABT,bPPKD,sUraian,sUraianRKA,sUraianMurni," +//10
                                  "sUraianGeser, sUraianRKAP, sUraianABT, Level,IDstandardHarga,Jenis," +
                                  "JumlahOlah , JumlahRKA,JumlahMurni,JumlahGeser,JumlahRKAP,JumlahABT, iID,showinreport,sLabel,cJumlahYAD, iTahap,cStandardHarga,IDSubKegiatan,IDBarang,IDRKBMD,IDRKBMDBARANG)  values ( " +
                                   "@piTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana, " +
                                  " @pbtKodeUrusanPelaksana,@pbtKodeKategori,@pbtKodeUrusan, @pbtKodeSKPD, @pbtKodeuK, @pbtIDrogram, @pbtIDKegiatan, @pbtTahapInput, " +
                                  "@pIIDRekening,@piNoUrut,@pID,@pIDLOkasi, 0,0,0,@piVolOlah,@piVolOlah,@piVolOlah,@psSatuan," + //11
                                  " 0,0,0,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pbPPKD,'','','', " + //10
                                   "@psUraian,@psUraian, @psUraian, @pLevel,@pIDstandardHarga,@pJenis," +
                                  " 0,0,0,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah, " +
                                  "@piID,@pshowinreport,@psLabel,@pcJumlahYAD, @piTahap,@pcStandardHarga,@pIDSubKegiatan,@pIDBarang,@pIDRKBMD,@pIDRKBMDBARANG )";

                }
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@piTahun", o.Tahun, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDUrusan", _pIDUrusan, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtKodeuK", o.KodeUK , DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput, DbType.Int16));
                paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening, DbType.Int64));
                paramCollection.Add(new DBParameter("@piNoUrut", o.NoUrut, DbType.Int32));
                paramCollection.Add(new DBParameter("@pID",o.ID,DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDLOkasi", o.IDLokasi, DbType.Int32));
                paramCollection.Add(new DBParameter("@piVolOlah", o.VolOlah, DbType.Decimal));
                paramCollection.Add(new DBParameter("@psSatuan", o.Satuan, DbType.String));
                paramCollection.Add(new DBParameter("@pcHargaOlah", o.HargaOlah, DbType.Decimal));
                paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));
                paramCollection.Add(new DBParameter("@psUraian", o.Uraian, DbType.String));
                paramCollection.Add(new DBParameter("@pLevel", 1, DbType.Int16));
                paramCollection.Add(new DBParameter("@pIDstandardHarga", o.KodeSH, DbType.String));
                paramCollection.Add(new DBParameter("@pJenis", o.Jenis, DbType.Int16));
                paramCollection.Add(new DBParameter("@pJumlahOlah", o.JumlahOlah, DbType.Decimal));
                paramCollection.Add(new DBParameter("@piID", o.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("@pshowinreport", o.ShowInReport, DbType.Int16));
                paramCollection.Add(new DBParameter("@psLabel", "-", DbType.String));
                paramCollection.Add(new DBParameter("@pcJumlahYAD", 0, DbType.Decimal));
                paramCollection.Add(new DBParameter("@piTahap", o.Tahap, DbType.Int16));
                paramCollection.Add(new DBParameter("@pcStandardHarga", o.StandardHarga, DbType.Decimal));
                paramCollection.Add(new DBParameter("@pIDSubKegiatan", o.IDSubKegiatan, DbType.Int64));
                paramCollection.Add(new DBParameter("@pPPNOlah", o.PPNOlah, DbType.Decimal));
                paramCollection.Add(new DBParameter("@pIDBarang", o.IDBarang, DbType.Int64));
                paramCollection.Add(new DBParameter("@pIDRKBMD", o.IDRKBMD, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDRKBMDBARANG", o.IDRKBMDBArang, DbType.Int32));

                paramCollection.Add(new DBParameter("@pKodeSKPD", o.KodeSKPDSIPD, DbType.String));
                paramCollection.Add(new DBParameter("@pKodeUnit", o.KodeUnit, DbType.String));
                paramCollection.Add(new DBParameter("@pKodeSH", o.KodeSH, DbType.String));



                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
            }
            else
            {
                        //VolOlah,VolRKA,VolMurni, volGeser,volRKAP, volABT
                if (tahap == 2)
                {
                    SSQL = "UPDATE  tAnggaranUraian_A  SET " +
                                    "btUrut=@pbtUrut,VolOlah=@pVolOlah,VolRKA=@pVolOlah,VolMurni=@pVolOlah,VolGESER=@pVolOlah,VolRKAP=@pVolOlah,VolABT=@pVolOlah," +
                                    "cHargaOlah=@pcHargaOlah, cHargaRKA=@pcHargaOlah, cHargaMurni=@pcHargaOlah, cHargaGeser=@pcHargaOlah, cHargaRKAP=@pcHargaOlah, cHargaABT=@pcHargaOlah, " +
                                    "sSatuan=@psSatuan,sUraianOlah=@psUraian,sUraianRKA=@psUraian,sUraianMurni=@psUraian,sUraianGeser=@psUraian,sUraianRKAP=@psUraian,sUraianABT=@psUraian,Level=@pLevel,IDstandardHarga=@pIDstandardHarga,showinreport=@pshowinreport,sLabel=@psLabel,cJumlahYAD=@pcJumlahYAD, " +
                                    "JumlahOlah =@pJumlahOlah,JumlahRKA =@pJumlahOlah,JumlahMurni =@pJumlahOlah,JumlahGeser =@pJumlahOlah,JumlahRKAP =@pJumlahOlah,JumlahABT =@pJumlahOlah,PPNOlah=@pPPNOlah,PPNRKA=@pPPNOlah,PPNMurni=@pPPNOlah, PPNGeser=@pPPNOlah,PPNRKAP=@pPPNOlah,PPNABT=@pPPNOlah, IDLOkasi=@pIDLOKASI  WHERE " +
                                    "iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDKegiatan= @pIDkegiatan AND IDUrusan=@pIDUrusan and ID=@piID AND IIDRekening=@pIIDRekening  AND Jenis=@pJenis AND bPPKD=@bPPKD AND IDSubKegiatan=@pIDSWubKegiatan";// AND isnull(iTahap,0)=@piTahap";

                }
                if (tahap == 3)
                {

                    SSQL = "UPDATE  tAnggaranUraian_A  SET " +
                                    "btUrut=@pbtUrut,VolGESER=@pVolOlah,VolRKAP=@pVolOlah,VolABT=@pVolOlah," +
                                    "cHargaGeser=@pcHargaOlah, cHargaRKAP=@pcHargaOlah, cHargaABT=@pcHargaOlah, " +
                                    "sUraianGeser=@psUraian,sUraianRKAP=@psUraian,sUraianABT=@psUraian,Level=@pLevel,showinreport=@pshowinreport,sLabel=@psLabel,cJumlahYAD=@pcJumlahYAD, " +
                                    "JumlahGeser =@pJumlahOlah,JumlahRKAP =@pJumlahOlah,JumlahABT =@pJumlahOlah,PPNGeser=@pPPNOlah,PPNRKAP=@pPPNOlah,PPNABT=@pPPNOlah, IDLOkasi=@pIDLOKASI  WHERE " +
                                    "iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDKegiatan= @pIDkegiatan AND IDUrusan=@pIDUrusan and ID=@piID AND IIDRekening=@pIIDRekening "+
                                    " AND Jenis=@pJenis AND bPPKD=@bPPKD AND IDSubKegiatan=@pIDSWubKegiatan AND IDstandardHarga=@STANDARDHARGA";



                 //   "iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDKegiatan= @pIDkegiatan " +
                 //" AND IDUrusan=@pIDUrusan and ID=@piID AND IIDRekening=@pIIDRekening  AND Jenis=@pJenis " +
                 //"AND bPPKD=@bPPKD AND IDSubKegiatan=@pIDSubKegiatan AND IDstandardHarga=@STANDARDHARGA";
                }

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pbtUrut", o.NoUrut, DbType.Int32));
                paramCollection.Add(new DBParameter("@pVolOlah", o.VolOlah, DbType.Decimal));
                paramCollection.Add(new DBParameter("@pcHargaOlah", o.HargaOlah, DbType.Decimal));
                paramCollection.Add(new DBParameter("@psSatuan", o.Satuan, DbType.String));
                paramCollection.Add(new DBParameter("@psUraian", o.Uraian, DbType.String));
                paramCollection.Add(new DBParameter("@pLevel", o.Level, DbType.Int16));
             
                paramCollection.Add(new DBParameter("@pJumlahOlah", o.JumlahOlah, DbType.Decimal));
                paramCollection.Add(new DBParameter("@pshowinreport", o.ShowInReport, DbType.Int32));
                paramCollection.Add(new DBParameter("@psLabel", o.Label, DbType.String));
                paramCollection.Add(new DBParameter("@pcJumlahYAD", o.JumlahYAD, DbType.Decimal));
                paramCollection.Add(new DBParameter("@pIDLOKASI", o.IDLokasi, DbType.Int32));
                paramCollection.Add(new DBParameter("@piTahun", o.Tahun, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDUrusan", _pIDUrusan, DbType.Int32));
                paramCollection.Add(new DBParameter("@piID", o.IDUraian, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening, DbType.Int64));
                paramCollection.Add(new DBParameter("@pJenis", o.Jenis, DbType.Int16));
                paramCollection.Add(new DBParameter("@bPPKD", o.PPKD, DbType.Int16));
                paramCollection.Add(new DBParameter("@pIDSWubKegiatan", o.IDSubKegiatan, DbType.Int64));
                //   paramCollection.Add(new DBParameter("@pbtIDSubKegiatan", o.IDSubKegiatan % 100, DbType.UInt32));


                paramCollection.Add(new DBParameter("@pPPNOlah", o.PPNOlah, DbType.Decimal));
                paramCollection.Add(new DBParameter("@STANDARDHARGA", o.IDStandardHarga, DbType.String));


                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

            }

            return true;
        }
        public bool SimpanSumberDanaSIPD2024(TAnggaranUraian o)
        {

            try
            {
                int _KodeProgram;
                int _KodeKegiatan;
                int _KodeKategoriPelaksana;
                int _kodeUrusanPelaksana;

                int _KodeKategori;
                int _KodeUrusan;
                int _KodeSKPD;
                int _KodeUK;
                int _pIDUrusan;
                //int maxID =  GetMaxIDEx(_pDInas, _pTahun);


                if (o.IDKegiatan > 0)
                {
                    _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, m_ProfileProgKegiatan.KodeProgram));
                    _KodeKegiatan = DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, m_ProfileProgKegiatan.KodeKegiatan));
                    _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                    _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                    _pIDUrusan = o.IDUrusan;
                }
                else
                {
                    _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                    _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                    _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                    _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    _pIDUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 3));

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
                if (o.NoUrut == 0)
                {
                    _KodeUK = 0;
                }

                //VolOlah,VolRKA,VolMurni, volGeser,volRKAP, volABT
                SSQL = "UPDATE  tAnggaranUraian_A  SET " +
                        "kodeSumberdana =@KodeSumberDana, namaSumberDana=@namaSumberDana where IDstandardHarga =@pIDstandardHarga " +
                        " and cHargaMurni=@pcHargaOlah and iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram " +
                        " AND IDKegiatan= @pIDkegiatan AND IDUrusan=@pIDUrusan and  IIDRekening=@pIIDRekening " +
                         " AND IDSubKegiatan=@pIDSWubKegiatan";// AND isnull(iTahap,0)=@piTahap";




                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@KodeSumberDana", o.KodeSumberDana, DbType.String));
                paramCollection.Add(new DBParameter("@namaSumberDana", o.NamaSumberDana.Trim(), DbType.String));

                paramCollection.Add(new DBParameter("@pIDstandardHarga", o.IDStandardHarga == null ? "" : o.IDStandardHarga, DbType.String));

                paramCollection.Add(new DBParameter("@pcHargaOlah", o.HargaOlah, DbType.Decimal));

                paramCollection.Add(new DBParameter("@piTahun", o.Tahun, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDUrusan", _pIDUrusan, DbType.Int32));

                paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening, DbType.Int64));
                paramCollection.Add(new DBParameter("@pIDSWubKegiatan", o.IDSubKegiatan, DbType.Int64));





                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

            

        }
        public bool SimpanDariPerencanaan(List<TAnggaranUraian> _lst)
        {


            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;

            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSKPD;
            int _KodeUK;
            int _pIDUrusan;

            SSQL = "DELETE from tAnggaranUraian_A ";
            _dbHelper.ExecuteNonQuery(SSQL);

            foreach (TAnggaranUraian o in _lst)
            {
                if (o.IDKegiatan > 0)
                {
                    _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, m_ProfileProgKegiatan.KodeProgram));
                    _KodeKegiatan = DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, m_ProfileProgKegiatan.KodeKegiatan));
                    _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                    _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                    _pIDUrusan = o.IDUrusan;
                }
                else
                {
                    _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                    _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                    _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                    _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    _pIDUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 3));

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
                if (o.NoUrut == 0)
                {
                    _KodeUK = 0;
                }
            
                SSQL = "INSERT INTO tAnggaranUraian_A (iTahun,IDDInas,IDProgram,IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                                   " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK, btIDProgram, btIDKegiatan, btTahapInput, " +
                                   "IIDRekening,btUrut, ID,IDLOkasi,VolPraRKA, VolOlah,VolRKA,VolMurni, volGeser,volRKAP, volABT,sSatuanPRaRKA,sSatuan," +
                                   " cHargaPraRKA, cHargaOlah, cHargaRKA,cHargaMurni, cHargaGeser,cHargaRKAP, cHargaABT,bPPKD,sUraian,sUraianRKA,sUraianMurni, sUraianGeser, sUraianRKAP, sUraianABT, Level,IDstandardHarga,Jenis," +
                                   "JumlahOlah , JumlahRKA,JumlahMurni,JumlahGeser,JumlahRKAP,JumlahABT, iID,showinreport,sLabel,cJumlahYAD, iTahap,cStandardHarga)  values ( " +
                                    "@piTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana, " +
                                   " @pbtKodeUrusanPelaksana,@pbtKodeKategori,@pbtKodeUrusan, @pbtKodeSKPD, @pbtKodeuK, @pbtIDrogram, @pbtIDKegiatan, @pbtTahapInput, " +
                                   "@pIIDRekening,@piNoUrut,@pID,@pIDLOkasi, @piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah," +
                                   " @psSatuan,@psSatuan,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah," +
                                   "@pbPPKD,@psUraian,@psUraian,@psUraian, @psUraian,@psUraian, @psUraian,@psUraian, @pLevel,@pIDstandardHarga,@pJenis," +
                                   " @pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah, " +
                                   "@piID,@pshowinreport,@psLabel,@pcJumlahYAD, @piTahap,@pcStandardHarga )";
                
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@piTahun", o.Tahun, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pIDUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening, DbType.Int64));
                    paramCollection.Add(new DBParameter("@piNoUrut", o.NoUrut, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pID", o.ID, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDLOkasi", o.IDLokasi, DbType.Int32));
                    paramCollection.Add(new DBParameter("@piVolOlah", o.VolOlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@psSatuan", o.Satuan, DbType.String));
                    paramCollection.Add(new DBParameter("@pcHargaOlah", o.HargaOlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD, DbType.Int16));
                    paramCollection.Add(new DBParameter("@psUraian", o.Uraian, DbType.String));
                    paramCollection.Add(new DBParameter("@pLevel", o.Level, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pIDstandardHarga", o.IDStandardHarga, DbType.String));
                    paramCollection.Add(new DBParameter("@pJenis", o.Jenis, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pJumlahOlah", o.JumlahOlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@piID", o.ID, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pshowinreport", o.ShowInReport, DbType.Int16));
                    paramCollection.Add(new DBParameter("@psLabel", o.Label, DbType.String));
                    paramCollection.Add(new DBParameter("@pcJumlahYAD", o.JumlahYAD, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@piTahap", o.Tahap, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pcStandardHarga", o.StandardHarga, DbType.Decimal));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                }
               
            
            return true;
        }
        public bool SimpanNoUrut(List<TAnggaranUraian> _lst, int _pTahun, int _pIDDInas,int _pIDUrusan , int idProgram, int IDKegiatan, int Jenis, int bPPKD)
        {

            try{
            foreach (TAnggaranUraian o in _lst)
            {
                  
                
                SSQL = "UPDATE  tAnggaranUraian_A  SET " +
                                    "btUrut=@pbtUrut WHERE " +
                                    "iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDKegiatan= @pIDkegiatan AND IDUrusan=@pIDUrusan and ID=@piID AND IIDRekening=@pIIDRekening  AND Jenis=@pJenis AND bPPKD=@bPPKD ";// AND isnull(iTahap,0)=@piTahap";

                            
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pbtUrut", o.NoUrut, DbType.Int32));
                    paramCollection.Add(new DBParameter("@piTahun", _pTahun, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDDInas", _pIDDInas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDProgram", idProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", IDKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pIDUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@piID", o.IDUraian, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pJenis", Jenis, DbType.Int16));
                    paramCollection.Add(new DBParameter("@bPPKD", bPPKD, DbType.Int16));



                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                }
            return true;
       
            }
            catch (Exception ex)
            {
                return false;
                _isError = true;
                _lastError = ex.Message;


            }
            
        }

        public bool SimpanRKA(List<TAnggaranUraian> _lst, int _pDInas, int _pTahun)
        {


            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;

            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSKPD;
            int _KodeUK;
            int _pIDUrusan;
            int maxID = GetMaxIDEx(_pDInas, _pTahun);

            foreach (TAnggaranUraian o in _lst)
            {
                

                if (o.IDKegiatan> 0)
                {
                    _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, m_ProfileProgKegiatan.KodeProgram));
                    _KodeKegiatan = DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, m_ProfileProgKegiatan.KodeKegiatan));
                    _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                    _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                    _pIDUrusan = o.IDUrusan;
                }
                else
                {
                    _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                    _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                    _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                    _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    _pIDUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 3));

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
                if (o.NoUrut== 0)
                {
                    _KodeUK = 0;
                }
                if (o.StatusUpdate == 0)
                {
                    maxID++;
                    
                    SSQL = "INSERT INTO tAnggaranUraian_A (iTahun,IDDInas,IDProgram,IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                               " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK, btIDProgram, btIDKegiatan, btTahapInput, " +
                               "IIDRekening,btUrut, ID,IDLOkasi,volRKA, volMurni,VolOlah,VolGeser,VolABT,volRKAP,sSatuan,cHargaOlah,cHargaRKA,cHargaMurni, cHargaRKAP,cHargaGeser,cHargaABT, bPPKD,sUraian,Level,IDstandardHarga,Jenis,JumlahOlah , iID,showinreport,sLabel,cJumlahYAD, iTahap)  values ( " +
                                "@piTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana, " +
                               " @pbtKodeUrusanPelaksana,@pbtKodeKategori,@pbtKodeUrusan, @pbtKodeSKPD, @pbtKodeuK, @pbtIDrogram, @pbtIDKegiatan, @pbtTahapInput, " +
                               "@pIIDRekening,@piNoUrut,@pID,@pIDLOkasi, @pvolRKA,@pvolMurni,@piVolOlah,@piVolGEser,@piVolABT,@pvolRKAP,@psSatuan,@pcHargaOlah,@pcHargaRKA,@pcHargaMurni, @pcHargaRKAP,  @pcHargaGeser,@pcHargaABT, @pbPPKD,@psUraian, @pLevel,@pIDstandardHarga,@pJenis,@pJumlahOlah,@piID,@pshowinreport,@psLabel,@pcJumlahYAD, @piTahap )";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                    paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                    paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pIDUrusan));
                    paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana));
                    paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan));
                    paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD));
                    paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK));
                    paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram));
                    paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan));
                    paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput));
                    paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                    paramCollection.Add(new DBParameter("@piNoUrut", o.NoUrut));
                    paramCollection.Add(new DBParameter("@pID", maxID));
                    paramCollection.Add(new DBParameter("@pIDLOkasi", o.IDLokasi));
                    paramCollection.Add(new DBParameter("@pvolRKA", o.VolOlah));                                        
                    paramCollection.Add(new DBParameter("@pvolMurni", o.VolOlah));
                    paramCollection.Add(new DBParameter("@piVolOlah", o.VolOlah));
                    paramCollection.Add(new DBParameter("@piVolGEser", o.VolPergeseran));
                    paramCollection.Add(new DBParameter("@piVolABT", o.VolABT));
                    paramCollection.Add(new DBParameter("@pvolRKA", o.VolOlah));
                    paramCollection.Add(new DBParameter("@psSatuan", o.Satuan));
                    paramCollection.Add(new DBParameter("@pcHargaOlah", o.HargaOlah));
                    paramCollection.Add(new DBParameter("@pcHargaRKA", o.HargaOlah));
                    paramCollection.Add(new DBParameter("@pcHargaMurni", o.HargaOlah));
                    paramCollection.Add(new DBParameter("@pcHargaRKAP", o.HargaOlah));
                    paramCollection.Add(new DBParameter("@pcHargaGeser", o.HargaPergeseran));
                    paramCollection.Add(new DBParameter("@pcHargaABT", o.HargaABT));
                    paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD));
                    paramCollection.Add(new DBParameter("@psUraian", o.Uraian));
                    paramCollection.Add(new DBParameter("@pLevel", o.Level));
                    paramCollection.Add(new DBParameter("@pIDstandardHarga", o.IDStandardHarga));
                    paramCollection.Add(new DBParameter("@pJenis", o.Jenis));
                    paramCollection.Add(new DBParameter("@pJumlahOlah", o.JumlahOlah));
                    paramCollection.Add(new DBParameter("@piID", o.ID));
                    paramCollection.Add(new DBParameter("@pshowinreport", o.ShowInReport));
                    paramCollection.Add(new DBParameter("@psLabel", o.Label));
                    paramCollection.Add(new DBParameter("@pcJumlahYAD", o.JumlahYAD));
                    paramCollection.Add(new DBParameter("@piTahap", o.Tahap, DbType.Int16));



                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                }
                else
                {
                    SSQL = "UPDATE  tAnggaranUraian_A  SET " +
                               "btUrut=@pbtUrut,VolOlah=@pVolOlah,cHargaOlah=@pcHargaOlah, sSatuan=@psSatuan,sUraian=@psUraian,Level=@pLevel,IDstandardHarga=@pIDstandardHarga,JumlahOlah =@pJumlahOlah,showinreport=@pshowinreport,sLabel=@psLabel,cJumlahYAD=@pcJumlahYAD WHERE " +
                               "iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDKegiatan= @pIDkegiatan AND IDUrusan=@pIDUrusan and ID=@piID AND IIDRekening=@pIIDRekening  AND Jenis=@pJenis AND bPPKD=@bPPKD ";// AND isnull(iTahap,0)=@piTahap";

                    DBParameterCollection paramCollection = new DBParameterCollection();


                    paramCollection.Add(new DBParameter("@pbtUrut", o.NoUrut));
                    paramCollection.Add(new DBParameter("@pVolOlah", o.VolOlah));
                    paramCollection.Add(new DBParameter("@pcHargaOlah", o.HargaOlah));
                    paramCollection.Add(new DBParameter("@psSatuan", o.Satuan));
                    paramCollection.Add(new DBParameter("@psUraian", o.Uraian));
                    paramCollection.Add(new DBParameter("@pLevel", o.Level));
                    paramCollection.Add(new DBParameter("@pIDstandardHarga", o.IDStandardHarga));                    
                    paramCollection.Add(new DBParameter("@pJumlahOlah", o.JumlahOlah));
                    paramCollection.Add(new DBParameter("@pshowinreport", o.ShowInReport));
                    paramCollection.Add(new DBParameter("@psLabel", o.Label));
                    //paramCollection.Add(new DBParameter("@pcPlafon", o.Plafon));
                    paramCollection.Add(new DBParameter("@pcJumlahYAD", o.JumlahYAD));
                    paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                    paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                    paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pIDUrusan));
                    paramCollection.Add(new DBParameter("@piID", o.IDUraian));
                    paramCollection.Add(new DBParameter("@pIIDRekening",o.IDRekening));
                    paramCollection.Add(new DBParameter("@pJenis",o.Jenis));
                    paramCollection.Add(new DBParameter("@bPPKD", o.PPKD));
                    //paramCollection.Add(new DBParameter("@piTahap", o.Tahap, DbType.Int16));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                }
            }
            return true;

        }
        //public bool BetulkanUraian()
        //{
            
            
        //    SSQL = "UPDATE  tAnggaranUraian_A  SET " +
        //           " btUrut=@pbtUrut,VolOlah=@pVolOlah,VolRKA=@pVolOlah,VolMurni=@pVolOlah,VolGESER=@pVolOlah,VolRKAP=@pVolOlah,VolABT=@pVolOlah," +
        //                            "cHargaOlah=@pcHargaOlah, cHargaRKA=@pcHargaOlah, cHargaMurni=@pcHargaOlah, cHargaGeser=@pcHargaOlah, cHargaRKAP=@pcHargaOlah, cHargaABT=@pcHargaOlah, " +
        //                            "sSatuan=@psSatuan,sUraianOlah=@psUraian,sUraianRKA=@psUraian,sUraianMurni=@psUraian,sUraianGeser=@psUraian,sUraianRKAP=@psUraian,sUraianABT=@psUraian,Level=@pLevel,IDstandardHarga=@pIDstandardHarga,showinreport=@pshowinreport,sLabel=@psLabel,cJumlahYAD=@pcJumlahYAD, " +
        //                            "JumlahOlah =@pJumlahOlah,JumlahRKA =@pJumlahOlah,JumlahMurni =@pJumlahOlah,JumlahGeser =@pJumlahOlah,JumlahRKAP =@pJumlahOlah,JumlahABT =@pJumlahOlah WHERE " +
        //                            "iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDKegiatan= @pIDkegiatan AND IDUrusan=@pIDUrusan and ID=@piID AND IIDRekening=@pIIDRekening  AND Jenis=@pJenis AND bPPKD=@bPPKD ";// AND isnull(iTahap,0)=@piTahap";

        //                    break;

        //}

        public bool SimpanUraianImport(List<TAnggaranUraian> lst)
        {


            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;

            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSKPD;
            int _KodeUK;
            int _pIDUrusan;
            
            try
            {
                foreach (TAnggaranUraian o in lst)
                {


                
                    if (o.IDKegiatan > 0)
                    {
                        _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, m_ProfileProgKegiatan.KodeProgram));
                        _KodeKegiatan =  DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5));
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                        _pIDUrusan = o.IDUrusan;
                    }
                    else
                    {
                        _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                        _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                        _pIDUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 3));

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
                    if (o.NoUrut == 0)
                    {
                        _KodeUK = 0;
                    }

                    SSQL = "INSERT INTO tAnggaranUraian_A (iTahun,IDDInas,IDProgram,IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                                 " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK, btIDProgram, btIDKegiatan, btTahapInput, " +
                                 "IIDRekening,btUrut, ID,IDLOkasi, VolOlah,VolRKA,VolMurni, volGeser,volRKAP, volABT,sSatuan," +
                                 " cHargaOlah, cHargaRKA,cHargaMurni, cHargaGeser,cHargaRKAP, cHargaABT,bPPKD,sUraian,sUraianRKA,Level,IDstandardHarga,Jenis," +
                                 "JumlahOlah , JumlahRKA,JumlahMurni,JumlahGeser,JumlahRKAP,JumlahABT, iID,showinreport,sLabel,cJumlahYAD, iTahap)  values ( " +
                                  "@piTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana, " +
                                 " @pbtKodeUrusanPelaksana,@pbtKodeKategori,@pbtKodeUrusan, @pbtKodeSKPD, @pbtKodeuK, @pbtIDrogram, @pbtIDKegiatan, @pbtTahapInput, " +
                                 "@pIIDRekening,@piNoUrut,@pID,@pIDLOkasi, @piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah,@piVolOlah," +
                                 " @psSatuan,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah,@pcHargaOlah," +
                                 "@pbPPKD,@psUraian,@psUraian, @pLevel,@pIDstandardHarga,@pJenis," +
                                 " @pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah, " +
                                 "@piID,@pshowinreport,@psLabel,@pcJumlahYAD, @piTahap )";

                    
                    //SSQL = "INSERT INTO tAnggaranUraian_A (iTahun,IDDInas,IDProgram,IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                    //               " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK, btIDProgram, btIDKegiatan, btTahapInput, " +
                    //               "IIDRekening,btUrut, ID,IDLOkasi,"+
                    //                "volRKA, volMurni,VolOlah,VolGeser,VolABT,volRKAP," +
                    //                "sSatuan," +
                    //                "cHargaOlah,cHargaRKA,cHargaMurni, cHargaRKAP,cHargaGeser,cHargaABT, " +
                    //                " bPPKD,sUraian,Level,IDstandardHarga,Jenis," +
                    //                 "JumlahOlah,JumlahRKA,JumlahMurni ,JumlahGeser ,JumlahRKAP ,JumlahABT,  iID,showinreport,sLabel,cJumlahYAD, iTahap) values ( " + //,IDRincianBapeda , IDUraianBapeda,volBapeda , sUraianBapeda , satuanBapeda , cHargaBapeda , cJumlahBapeda )  values ( " +
                    //                "@piTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana, " +
                    //               " @pbtKodeUrusanPelaksana,@pbtKodeKategori,@pbtKodeUrusan, @pbtKodeSKPD, @pbtKodeuK, @pbtIDrogram, @pbtIDKegiatan, @pbtTahapInput, " +
                    //               "@pIIDRekening,@piNoUrut,@pID,@pIDLOkasi, @pvolRKA,@pvolMurni,@piVolOlah,@piVolGEser,@piVolABT,@pvolRKAP,@psSatuan,@pcHargaOlah," +
                    //               "@pcHargaRKA,@pcHargaMurni, @pcHargaRKAP,  @pcHargaGeser,@pcHargaABT, @pbPPKD,@psUraian, " +
                    //               "@pLevel,@pIDstandardHarga,@pJenis,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@pJumlahOlah,@piID,@pshowinreport,@psLabel,@pcJumlahYAD, @piTahap )";//@pIDRincianBapeda , @pIDUraianBapeda ,@pvolBapeda , @psUraianBapeda , @psatuanBapeda , @pcHargaBapeda , @pcJumlahBapeda )";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                    paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                    paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pIDUrusan));
                    paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana));
                    paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan));
                    paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD));
                    paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK));
                    paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram));
                    paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan));
                    paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput));
                    paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                    paramCollection.Add(new DBParameter("@piNoUrut", o.NoUrut));
                    paramCollection.Add(new DBParameter("@pID", o.uraianID));
                    paramCollection.Add(new DBParameter("@pIDLOkasi", o.IDLokasi));
                    paramCollection.Add(new DBParameter("@pvolRKAP", o.VolOlah));
                    paramCollection.Add(new DBParameter("@pvolMurni", o.VolOlah));
                    paramCollection.Add(new DBParameter("@piVolOlah", o.VolOlah));
                    paramCollection.Add(new DBParameter("@piVolGEser", o.VolOlah));
                    paramCollection.Add(new DBParameter("@piVolABT", o.VolOlah));
                    paramCollection.Add(new DBParameter("@pvolRKA", o.VolOlah));
                    paramCollection.Add(new DBParameter("@psSatuan", o.Satuan));
                    paramCollection.Add(new DBParameter("@pcHargaOlah", o.HargaOlah));
                    paramCollection.Add(new DBParameter("@pcHargaRKA", o.HargaOlah));
                    paramCollection.Add(new DBParameter("@pcHargaMurni", o.HargaOlah));
                    paramCollection.Add(new DBParameter("@pcHargaRKAP", o.HargaOlah));
                    paramCollection.Add(new DBParameter("@pcHargaGeser", o.HargaOlah));
                    paramCollection.Add(new DBParameter("@pcHargaABT", o.HargaOlah));
                    paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD));
                    paramCollection.Add(new DBParameter("@psUraian", o.Uraian));
                    paramCollection.Add(new DBParameter("@pLevel", o.Level));
                    paramCollection.Add(new DBParameter("@pIDstandardHarga", o.IDStandardHarga));
                    paramCollection.Add(new DBParameter("@pJenis", o.Jenis));
                    paramCollection.Add(new DBParameter("@pJumlahOlah", o.JumlahOlah));
                    paramCollection.Add(new DBParameter("@piID", o.ID));
                    paramCollection.Add(new DBParameter("@pshowinreport", o.ShowInReport));
                    paramCollection.Add(new DBParameter("@psLabel", o.Label));
                    paramCollection.Add(new DBParameter("@pcJumlahYAD", o.JumlahYAD));
                    paramCollection.Add(new DBParameter("@piTahap", o.Tahap, DbType.Int32));
                    //paramCollection.Add(new DBParameter("@pIDRincianBapeda", o.rincianID, DbType.Int32));
                    //paramCollection.Add(new DBParameter("@pIDUraianBapeda", o.uraianID, DbType.Int32));
                    //paramCollection.Add(new DBParameter("@pvolBapeda", o.VolOlah));
                    //paramCollection.Add(new DBParameter("@psUraianBapeda", o.Uraian));
                    //paramCollection.Add(new DBParameter("@psatuanBapeda", o.Satuan));
                    //paramCollection.Add(new DBParameter("@pcHargaBapeda", o.HargaOlah));
                    //paramCollection.Add(new DBParameter("@pcJumlahBapeda", o.JumlahOlah));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                }
                return true;

            } catch(Exception ex){
                _isError=true;
                _lastError= ex.Message;
                return false;
            }

        }

      
        public bool SimpanUraianMurni(List<TAnggaranUraian> _lst, int _pDInas, int _pTahun)
        {


            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;

            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSKPD;
            int _KodeUK;
            int _pIDUrusan;
            

            foreach (TAnggaranUraian o in _lst)
            {


                if (o.IDKegiatan > 0)
                {
                    _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, m_ProfileProgKegiatan.KodeProgram));
                    _KodeKegiatan = DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, m_ProfileProgKegiatan.KodeKegiatan));
                    _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                    _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                    _pIDUrusan = o.IDUrusan;
                }
                else
                {
                    _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                    _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                    _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                    _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    _pIDUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 3));

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
                if (o.NoUrut == 0)
                {
                    _KodeUK = 0;
                }
                
                   SSQL = "UPDATE  tAnggaranUraian_A  SET " +
                           " VolRKA=@pvoolMurni,cHargaRKA=@pchargaMurni,JumlahRKA=@pJumlahMurni,Volmurni=@pvoolMurni,cHargaMurni=@pchargaMurni,JumlahMurni=@pJumlahMurni WHERE " +
                            " iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDKegiatan= @pIDkegiatan AND IDUrusan=@pIDUrusan and ID=@piID AND IIDRekening=@pIIDRekening  AND Jenis=@pJenis AND bPPKD=@bPPKD ";// AND isnull(iTahap,0)=@piTahap";

                    DBParameterCollection paramCollection = new DBParameterCollection();


                    
                    paramCollection.Add(new DBParameter("@pvoolMurni", o.VolMurni));
                    paramCollection.Add(new DBParameter("@pchargaMurni", o.HargaMurni ));                    
                    paramCollection.Add(new DBParameter("@pJumlahMurni", o.JumlahMurni));
                    paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                    paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                    paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pIDUrusan));
                    paramCollection.Add(new DBParameter("@piID", o.IDUraian));
                    paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                    paramCollection.Add(new DBParameter("@pJenis", o.Jenis));
                    paramCollection.Add(new DBParameter("@bPPKD", o.PPKD));
                    //paramCollection.Add(new DBParameter("@piTahap", o.Tahap, DbType.Int16));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                
            }
            return true;

        }

        public bool SimpanDPA(List<TAnggaranUraian> _lst, int _pDInas, int _pTahun)
        {


            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;

            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSKPD;
            int _KodeUK;
            int _pIDUrusan;
            int maxID = GetMaxIDEx(_pDInas, _pTahun);

            foreach (TAnggaranUraian o in _lst)
            {


                if (o.IDKegiatan > 0)
                {
                    _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, m_ProfileProgKegiatan.KodeProgram));
                    _KodeKegiatan = DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, m_ProfileProgKegiatan.KodeKegiatan));
                    _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                    _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                    _pIDUrusan = o.IDUrusan;
                }
                else
                {
                    _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                    _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                    _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                    _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    _pIDUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 3));

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
                    maxID++;

                    SSQL = "INSERT INTO tAnggaranUraian_A (iTahun,IDDInas,IDProgram,IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                               " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK, btIDProgram, btIDKegiatan, btUrut, btTahapInput, " +
                               "IIDRekening,btUrutDPA, ID,IDLOkasi, Vol,sSatuanDPA,cHarga,bPPKD,sUraianAPBD,IDstandardHarga,Jenis, iID,showinreport,sLabelDPA,cJumlahYAD, iTahap, Jumlah,JumlahYADAPBD,iLevelDPA )  values ( " +
                                "@piTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana, " +
                               " @pbtKodeUrusanPelaksana,@pbtKodeKategori,@pbtKodeUrusan, @pbtKodeSKPD, @pbtKodeuK, @pbtIDrogram, @pbtIDKegiatan,0, 1, " +
                               "@pIIDRekening,@piNoUrut,@pID,@pIDLOkasi, @piVol,@psSatuan, @pcHarga,  @pbPPKD,@psUraian, @pIDstandardHarga,@pJenis,@piID,@pshowinreport,@psLabel,@pcJumlahYAD, @piTahap, @pJumlah,@pJumlahYADAPBD,@piLevelDPA )";

                           


                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                    paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                    paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pIDUrusan));
                    paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana));
                    paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan));
                    paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD));
                    paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK));
                    paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram));
                    paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan));                    
                    paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                    paramCollection.Add(new DBParameter("@piNoUrut", o.NoUrutDPA));
                    paramCollection.Add(new DBParameter("@pID", maxID));
                    paramCollection.Add(new DBParameter("@pIDLOkasi", o.IDLokasi));
                    paramCollection.Add(new DBParameter("@piVol", o.Vol));                                        
                    paramCollection.Add(new DBParameter("@psSatuan", o.SatuanDPA));                                     
                    paramCollection.Add(new DBParameter("@pcHarga", o.Harga));
                    paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD));
                    paramCollection.Add(new DBParameter("@psUraian", o.UraianAPBD));                    
                    paramCollection.Add(new DBParameter("@pIDstandardHarga", o.IDStandardHarga));
                    paramCollection.Add(new DBParameter("@pJenis", o.Jenis));                    
                    paramCollection.Add(new DBParameter("@piID", o.ID));
                    paramCollection.Add(new DBParameter("@pshowinreport", o.ShowInReport));
                    paramCollection.Add(new DBParameter("@psLabel", o.LabelDPA));                    
                    paramCollection.Add(new DBParameter("@pcJumlahYAD", o.JumlahYAD));
                    paramCollection.Add(new DBParameter("@piTahap", o.Tahap, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pJumlah", o.Jumlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pJumlahYADAPBD", o.JumlahYADAPBD, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@piLevelDPA", o.LevelDPA, DbType.Int16));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                }
                else
                {

                    SSQL = "UPDATE  tAnggaranUraian_A  SET " +
                               "cHarga= @pcHarga, Vol=@pVol,Jumlah= @pJumlah,btUrutDPA=@pbtUrutDPA  , iLevelDPA=@piLevelDPA , sSatuanDPA=@psSatuanDPA ,sUraianAPBD=@psUraian,IDstandardHarga=@pIDstandardHarga,showinreport=@pshowinreport,sLabelDPA=@psLabel, JumlahYADAPBD =@pJumlahYADAPBD WHERE " +
                               "iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDKegiatan= @pIDkegiatan AND IDUrusan=@pIDUrusan and ID=@piID AND IIDRekening=@pIIDRekening  AND Jenis=@pJenis AND isnull(bPPKD,0)=@bPPKD ";
                    DBParameterCollection paramCollection = new DBParameterCollection();


                    //paramCollection.Add(new DBParameter("@pbtUrut", o.NoUrut));
                    paramCollection.Add(new DBParameter("@pcHarga", o.Harga));                    
                    paramCollection.Add(new DBParameter("@pVol", o.Vol));
                    paramCollection.Add(new DBParameter("@pJumlah", o.Jumlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pbtUrutDPA",o.NoUrutDPA,DbType.Int32)); 
                    paramCollection.Add(new DBParameter("@piLevelDPA",o.LevelDPA,DbType.Int16));
                    paramCollection.Add(new DBParameter("@psSatuanDPA", o.SatuanDPA, DbType.String));                    
                    paramCollection.Add(new DBParameter("@psUraian", o.UraianAPBD, DbType.String));                                        
                    paramCollection.Add(new DBParameter("@pIDstandardHarga", o.IDStandardHarga));                    
                    paramCollection.Add(new DBParameter("@pshowinreport", o.ShowInReport));
                    paramCollection.Add(new DBParameter("@psLabel", o.LabelDPA));
                    paramCollection.Add(new DBParameter("@pJumlahYADAPBD", o.JumlahYADAPBD, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                    paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                    paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pIDUrusan));
                    paramCollection.Add(new DBParameter("@piID", o.IDUraian));
                    paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                    paramCollection.Add(new DBParameter("@pJenis", o.Jenis));
                    paramCollection.Add(new DBParameter("@bPPKD", o.PPKD));
                    //paramCollection.Add(new DBParameter("@piTahap", o.Tahap, DbType.Int16));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                }
            }
            return true;
        }

        public bool SimpanPlafon(List<TAnggaranUraian> _lst, int _pDInas, int _pTahun,Single pTahap )
        {
            int _KodeProgram;
            int _KodeKegiatan;
            int _KodeKategoriPelaksana;
            int _kodeUrusanPelaksana;
            int _KodeKategori;
            int _KodeUrusan;
            int _KodeSKPD;
            int _KodeUK;
            int _pIDUrusan;
            int maxID = GetMaxIDEx(_pDInas, _pTahun);
            foreach (TAnggaranUraian o in _lst)
            {
                if (o.IDKegiatan > 0)
                {
                    _KodeProgram = DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, m_ProfileProgKegiatan.KodeProgram));
                    _KodeKegiatan = DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, m_ProfileProgKegiatan.KodeKegiatan));
                    _KodeKategoriPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(0, 1));
                    _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDUrusan.ToString().Substring(1, 2));
                    _pIDUrusan = o.IDUrusan;
                }
                else
                {
                    _KodeProgram = 0;// DataFormat.GetInteger(o.IDProgram.ToString().Substring(3, 2));
                    _KodeKegiatan = 0;// DataFormat.GetInteger(o.IDKegiatan.ToString().Substring(5, 2));
                    _KodeKategoriPelaksana =  DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 1));
                    _kodeUrusanPelaksana = DataFormat.GetInteger(o.IDDinas.ToString().Substring(1, 2));
                    _pIDUrusan = DataFormat.GetInteger(o.IDDinas.ToString().Substring(0, 3));

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
                    maxID++;
                    SSQL = "INSERT INTO tAnggaranUraian_A (iTahun,IDDInas,IDProgram,IDkegiatan,IDUrusan,btKodekategoriPelaksana, " +
                               " btKodeUrusanPelaksana,btKodeKategori,btKodeUrusan, btKodeSKPD, btKodeuK, btIDProgram, btIDKegiatan, btTahapInput, " +
                               "IIDRekening,btUrut, ID,IDLOkasi, VolOlah,Vol,VolGeser,VolABT,sSatuan,cHargaOlah, cHarga, cHargaGeser,cHargaABT, bPPKD,sUraian,Level,IDstandardHarga,Jenis,JumlahOlah , iID,showinreport,sLabel,cPlafon, iTahap)  values ( " +
                                "@piTahun,@pIDDInas,@pIDProgram,@pIDkegiatan,@pIDUrusan,@pbtKodekategoriPelaksana, " +
                               " @pbtKodeUrusanPelaksana,@pbtKodeKategori,@pbtKodeUrusan, @pbtKodeSKPD, @pbtKodeuK, @pbtIDrogram, @pbtIDKegiatan, @pbtTahapInput, " +
                               "@pIIDRekening,@piNoUrut,@pID,@pIDLOkasi, @piVolOlah,@piVol,@piVolGEser,@piVolABT,@psSatuan,@pcHargaOlah, @pcHarga, @pcHargaGeser,@pcHargaABT, @pbPPKD,@psUraian, @pLevel,@pIDstandardHarga,@pJenis,@pJumlahOlah,@piID,@pshowinreport,@psLabel,@pcPlafon, @piTahap )";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                    paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                    paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pIDUrusan));
                    paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _KodeKategoriPelaksana));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _kodeUrusanPelaksana));
                    paramCollection.Add(new DBParameter("@pbtKodeKategori", _KodeKategori));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusan", _KodeUrusan));
                    paramCollection.Add(new DBParameter("@pbtKodeSKPD", _KodeSKPD));
                    paramCollection.Add(new DBParameter("@pbtKodeuK", _KodeUK));
                    paramCollection.Add(new DBParameter("@pbtIDrogram", _KodeProgram));
                    paramCollection.Add(new DBParameter("@pbtIDKegiatan", _KodeKegiatan));
                    paramCollection.Add(new DBParameter("@pbtTahapInput", o.TahapInput));
                    paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                    paramCollection.Add(new DBParameter("@piNoUrut", o.NoUrut));
                    paramCollection.Add(new DBParameter("@pID", maxID));
                    paramCollection.Add(new DBParameter("@pIDLOkasi", o.IDLokasi));
                    paramCollection.Add(new DBParameter("@piVolOlah", o.VolOlah));
                    paramCollection.Add(new DBParameter("@piVol", o.Vol));
                    paramCollection.Add(new DBParameter("@piVolGEser", o.VolPergeseran));
                    paramCollection.Add(new DBParameter("@piVolABT", o.VolABT));
                    paramCollection.Add(new DBParameter("@psSatuan", o.Satuan));
                    paramCollection.Add(new DBParameter("@pcHargaOlah", o.HargaOlah));
                    paramCollection.Add(new DBParameter("@pcHarga", o.Harga));
                    paramCollection.Add(new DBParameter("@pcHargaGeser", o.HargaPergeseran));
                    paramCollection.Add(new DBParameter("@pcHargaABT", o.HargaABT));
                    paramCollection.Add(new DBParameter("@pbPPKD", o.PPKD));
                    paramCollection.Add(new DBParameter("@psUraian", o.Uraian));
                    paramCollection.Add(new DBParameter("@pLevel", o.Level));
                    paramCollection.Add(new DBParameter("@pIDstandardHarga", o.IDStandardHarga));
                    paramCollection.Add(new DBParameter("@pJenis", o.Jenis));
                    paramCollection.Add(new DBParameter("@pJumlahOlah", o.JumlahOlah));
                    paramCollection.Add(new DBParameter("@piID", o.ID));
                    paramCollection.Add(new DBParameter("@pshowinreport", o.ShowInReport));
                    paramCollection.Add(new DBParameter("@psLabel", o.Label));
                    paramCollection.Add(new DBParameter("@pcPlafon", o.Plafon));
                    paramCollection.Add(new DBParameter("@piTahap", o.Tahap, DbType.Int16));



                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                }
                else
                {
                    SSQL = "UPDATE  tAnggaranUraian_A  SET " +
                               "btUrut=@pbtUrut,sUraian=@psUraian,showinreport=1 ,sLabel=@psLabel,cPlafon=@pcPlafon  WHERE " +
                               "iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDKegiatan= @pIDkegiatan AND IDUrusan=@pIDUrusan and ID=@piID AND IIDRekening=@pIIDRekening  AND Jenis=@pJenis AND bPPKD=@bPPKD and iTahap=@piTahap";

                    DBParameterCollection paramCollection = new DBParameterCollection();


                    paramCollection.Add(new DBParameter("@pbtUrut", o.NoUrut));
                    paramCollection.Add(new DBParameter("@psUraian", o.Uraian));
                    paramCollection.Add(new DBParameter("@psLabel", o.Label));
                    paramCollection.Add(new DBParameter("@pcPlafon", o.Plafon));
                    paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                    paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                    paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pIDUrusan));
                    paramCollection.Add(new DBParameter("@piID", o.IDUraian));
                    paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                    paramCollection.Add(new DBParameter("@pJenis", o.Jenis));
                    paramCollection.Add(new DBParameter("@bPPKD", o.PPKD));
                    paramCollection.Add(new DBParameter("@piTahap", o.Tahap, DbType.Int16));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                }
            }
            return true;

        }
        //public bool Update(TAnggaranRekening o)
        //{
        //    SSQL = "UPDATE " + m_sNamaTabel + " SET cJumlahOlah=@pcJumlahOlah WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
        //            " AND IDUrusan=@pIDUrusan AND btKodekategoriPelaksana=@pbtKodekategoriPelaksana AND btKodeUrusanPelaksana=@pbtKodeUrusanPelaksana AND btKodeKategori =@pbtKodeKategori " +
        //            " AND btKodeUrusan=@pbtKodeUrusan AND btKodeSKPD=@pbtKodeSKPD  AND btKodeUK=@pbtKodeUK AND btIDrogram=@pbtIDrogram " +
        //            " AND btIDKegiatan=@pbtIDKegiatan, IIDRekening=@pIIDRekening,btJenis=@pbtJenis";
        //    DBParameterCollection paramCollection = new DBParameterCollection();

        //    paramCollection.Add(new DBParameter("@pcJumlahOlah", o.JumlahOlah));
        //    paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
        //    paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
        //    paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
        //    paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
        //    paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
        //    paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", o.KodeKategoriPelaksana));
        //    paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", o.KodeUrusanPelaksana));
        //    paramCollection.Add(new DBParameter("@pbtKodeKategori", o.KodeKategori));
        //    paramCollection.Add(new DBParameter("@pbtKodeUrusan", o.KodeUrusan));
        //    paramCollection.Add(new DBParameter("@pbtKodeSKPD", o.KodeSKPD));
        //    paramCollection.Add(new DBParameter("@pbtKodeUK", o.KodeUK));
        //    paramCollection.Add(new DBParameter("@pbtIDrogram", o.KodeProgram));
        //    paramCollection.Add(new DBParameter("@pbtIDKegiatan", o.KodeKegiatan));
        //    paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
        //    paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
        //    if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }


        //}
        public bool Hapus(TAnggaranUraian o)
        {
            try { 
                SSQL = "DELETE from " + m_sNamaTabel + " WHERE iTahun=@piTahun AND IDDInas=@pIDDInas AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan " +
                        " AND IDUrusan=@pIDUrusan AND IIDRekening=@pIIDRekening and Jenis=@pbtJenis and ID = @pID ";

                DBParameterCollection paramCollection = new DBParameterCollection();                
                paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                paramCollection.Add(new DBParameter("@pIDDInas", o.IDDinas));
                paramCollection.Add(new DBParameter("@pIDProgram", o.IDProgram));
                paramCollection.Add(new DBParameter("@pIDkegiatan", o.IDKegiatan));
                paramCollection.Add(new DBParameter("@pIDUrusan", o.IDUrusan));
                paramCollection.Add(new DBParameter("@pIIDRekening", o.IDRekening));
                paramCollection.Add(new DBParameter("@pbtJenis", o.Jenis));
                paramCollection.Add(new DBParameter("@pID", o.IDUraian));
              //  paramCollection.Add(new DBParameter("@piTahap", o.Tahap, DbType.Int16));
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                //DELETE from tAnggaranUraian_A WHERE iTahun=2017 AND IDDInas=1020200 AND IDProgram=0
                //AND IDkegiatan=0  AND IDUrusan=102 AND IIDRekening=5110101 and Jenis=2 and ID = @pID and iTahap=btTahapInput and iTahap= @piTahap
                // jika daari rkbmdaa; 
               
                return true;
            } catch(Exception ex)
            
            {
                _isError = true;
                _lastError = ex.Message;
                return false;

            }
        }
        private int GetMaxIDEx(int _pIDDInas, int _pTahun)
        {
            try
            {
                SSQL = "SELECT MAX(ID) from " + m_sNamaTabel + " WHERE iTahun =" + _pTahun.ToString() + " AND IDDInas=" + _pIDDInas.ToString();
                object objMax = _dbHelper.ExecuteScalar(SSQL);//

                if (objMax.ToString().Length == 0)
                {
                    return 1;
                }
                return Convert.ToInt32(objMax.ToString()) + 1;


            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return 1;
            }
        }
    }
}
