using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DTO;
using DataAccess;
using Formatting;

namespace BP
{
    public class SKPDLogic: BP
    {
        public SKPDLogic(int _pTahun, int profile=3)
            : base(_pTahun, 0, profile)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mSKPD";
        }

        public List<SKPD> Get(int _Tahun)
        {
            List<SKPD> _lst = new List<SKPD>();
            try
            {
                SSQL = "SELECT mSKPD.* FROM mSKPD ORDER BY mSKPD.ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SKPD()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeURusan"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    Kode = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    Nama = DataFormat.GetString(dr["sNamaSKPD"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                                    Tampilan = DataFormat.GetInteger(dr["ID"]).ToKodeDinas(),
                                    TampilanUrusan = DataFormat.GetInteger(dr["IDurusan"]).ToKodeUrusan(),
                                    NamaUrusan = "",//DataFormat.GetString(dr["sNamaUrusan"]),
                                    NamaBendahara = DataFormat.GetString(dr["sBendPengeluaran"]),
                                    NIPBendahara = DataFormat.GetString(dr["sNIPBendPengeluaran"]),
                                    NamaPimpinan = DataFormat.GetString(dr["sNamaPimpinan"]),
                                    BendaharaPenerimaan = DataFormat.GetString(dr["sBendPenerimaan"]),
                                    NIPBendaharaPenerimaan = DataFormat.GetString(dr["sNIPBendPenerimaan"]),
                                    NamaPPK = DataFormat.GetString(dr["sNamaPPK"]),
                                    NIPPPK = DataFormat.GetString(dr["sNIPPPK"]),
                                    NamaBank = DataFormat.GetString(dr["sBankPenerima"]),
                                    NoRekening = DataFormat.GetString(dr["sBankRekening"]),
                                    NPWP = DataFormat.GetString(dr["sNPWP"]),

                                    //NIPPimpinan = DataFormat.GetString(dr["sNIPPimpinan"]),
                                    //JabatanPimpinan = DataFormat.GetString(dr["sJabatanPimpinan"]),
                                    //UrusanBAru = DataFormat.GetInteger(dr["idUrusanBaru"]),
                                    //Root = DataFormat.GetSingle(dr["Root"]),
                                    Parent = DataFormat.GetInteger(dr["Parent"]),
                                    Level = DataFormat.GetInteger(dr["level"]),
                                    KodeUnit = DataFormat.GetInteger(dr["KodeUnit"]),
                                    KodeParent = DataFormat.ToKodeDinas(DataFormat.GetInteger(dr["Parent"])),
                                    NamaParent = "",//DataFormat.GetInteger(dr["Parent"]) > 0 ? GetNama(DataFormat.GetInteger(dr["Parent"])) : "",
                                    TampilanKode = DataFormat.ToKodeDinas(DataFormat.GetInteger(dr["ID"])),
                                    KodeSIPD= DataFormat.GetString(dr["Kode"])

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
        //public List<int > GetListID (int _Tahun)
        //{
        //    List<int> mListUnit = new List<int>();
        //    try
        //    {

        //        SSQL = "SELECT ID FROM mSKPD ORDER BY mSKPD.ID";

        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                mListUnit = (from DataRow dr in dt.Rows
        //                        select new int ()
        //                        {
        //                            ID = DataFormat.GetInteger(dr["ID"]),
        //                            KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
        //                            KodeUrusan = DataFormat.GetInteger(dr["btKodeURusan"]),
        //                            Tahun = DataFormat.GetInteger(dr["iTahun"]),
        //                            Kode = DataFormat.GetInteger(dr["btKodeSKPD"]),
        //                            Nama = DataFormat.GetString(dr["sNamaSKPD"]),
        //                            IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
        //                            Tampilan = DataFormat.GetInteger(dr["ID"]).ToKodeDinas(),
        //                            TampilanUrusan = DataFormat.GetInteger(dr["IDurusan"]).ToKodeUrusan(),
        //                            NamaUrusan = "",//DataFormat.GetString(dr["sNamaUrusan"]),

        //                            NamaBendahara = DataFormat.GetString(dr["sBendPengeluaran"]),
        //                            NIPBendahara = DataFormat.GetString(dr["sNIPBendPengeluaran"]),
        //                            NamaPimpinan = DataFormat.GetString(dr["sNamaPimpinan"]),
        //                            BendaharaPenerimaan = DataFormat.GetString(dr["sBendPenerimaan"]),
        //                            NIPBendaharaPenerimaan = DataFormat.GetString(dr["sNIPBendPenerimaan"]),
        //                            NamaPPK = DataFormat.GetString(dr["sNamaPPK"]),
        //                            NIPPPK = DataFormat.GetString(dr["sNIPPPK"]),
        //                            NamaBank = DataFormat.GetString(dr["sBankPenerima"]),
        //                            NoRekening = DataFormat.GetString(dr["sBankRekening"]),
        //                            NPWP = DataFormat.GetString(dr["sNPWP"]),

        //                            NIPPimpinan = DataFormat.GetString(dr["sNIPPimpinan"]),
        //                            JabatanPimpinan = DataFormat.GetString(dr["sJabatanPimpinan"]),
        //                            UrusanBAru = DataFormat.GetInteger(dr["idUrusanBaru"]),
        //                            Root = DataFormat.GetSingle(dr["Root"]),
        //                            Parent = DataFormat.GetInteger(dr["Parent"]),
        //                            Level = DataFormat.GetInteger(dr["level"]),
        //                            KodeUnit = DataFormat.GetInteger(dr["KodeUnit"]),
        //                            KodeParent = DataFormat.ToKodeDinas(DataFormat.GetInteger(dr["Parent"])),
        //                            NamaParent = "",//DataFormat.GetInteger(dr["Parent"]) > 0 ? GetNama(DataFormat.GetInteger(dr["Parent"])) : "",
        //                            TampilanKode = DataFormat.ToKodeDinas(DataFormat.GetInteger(dr["ID"]))


        //                        }).ToList();
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
        private bool  GetPejabat(ref SKPD skpd, int ppkd =0)
        {
            try
            {
                PejabatLogic oLogic = new PejabatLogic(Tahun);
                DateTime d = DateTime.Now.Date;
                Pejabat pejabat = new Pejabat();
                pejabat = oLogic.GetKepalaDinas(skpd.ID, ppkd,d);
                skpd.NamaPimpinan = pejabat.Nama;
                skpd.JabatanPimpinan = pejabat.Jabatan;
                skpd.NIPPimpinan = pejabat.NIP;
                pejabat = oLogic.GetPPKSKPD(skpd.ID, ppkd,d);
                skpd.NamaPPK = pejabat.Nama;
                skpd.NIPPPK = pejabat.NIP;

                pejabat = oLogic.GetBendaharaDinas(skpd.ID, ppkd,d);
                skpd.NamaBendahara= pejabat.Nama;
                skpd.NIPBendahara= pejabat.NIP;
                skpd.NamaBank = pejabat.NamaBank;
                skpd.NPWP = pejabat.NPWP;
                skpd.NoRekening = pejabat.NoRekening;
                pejabat = oLogic.GetBendaharaPenerimaan(skpd.ID, ppkd,d);
                skpd.BendaharaPenerimaan = pejabat.Nama;
                skpd.NIPBendaharaPenerimaan = pejabat.NIP;


                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;

            }

        } 
        public List<SKPD> GetByParent(int idParent)
        {
            List<SKPD> _lst = new List<SKPD>();
            try
            {
          //      SSQL = "SELECT * FROM mSKPD WHERE Parent = (SELECT parent from mSKPD where ID = " + idParent.ToString() + ") ORDER BY ID";
                SSQL = "SELECT s.*,u.sNamaUrusan FROM mSKPD s INNER JOIN mUrusan U ON s.IDUrusan = U.ID where s.Parent = (SELECT parent from mSKPD where ID = " + idParent.ToString() + ") ORDER BY ID";
                // SSQL = "SELECT * FROM mSKPD WHERE Parent =" + idParent.ToString() + " ORDER BY ID";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SKPD()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeURusan"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    Kode = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    Nama = DataFormat.GetString(dr["sNamaSKPD"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                                    Tampilan = DataFormat.GetInteger(dr["ID"]).ToKodeDinas(),
                                    TampilanUrusan = DataFormat.GetInteger(dr["IDurusan"]).ToKodeUrusan(),
                                    NamaUrusan = DataFormat.GetString(dr["sNamaUrusan"]),

                                    NamaBendahara = DataFormat.GetString(dr["sBendPengeluaran"]),
                                    NIPBendahara = DataFormat.GetString(dr["sNIPBendPengeluaran"]),
                                    NamaPimpinan = DataFormat.GetString(dr["sNamaPimpinan"]),
                                    BendaharaPenerimaan = DataFormat.GetString(dr["sBendPenerimaan"]),
                                    NIPBendaharaPenerimaan = DataFormat.GetString(dr["sNIPBendPenerimaan"]),
                                    NamaPPK = DataFormat.GetString(dr["sNamaPPK"]),
                                    NIPPPK = DataFormat.GetString(dr["sNIPPPK"]),
                                    NamaBank = DataFormat.GetString(dr["sBankPenerima"]),
                                    NoRekening = DataFormat.GetString(dr["sBankRekening"]),
                                    NPWP = DataFormat.GetString(dr["sNPWP"]),

                                    NIPPimpinan = DataFormat.GetString(dr["sNIPPimpinan"]),
                                    JabatanPimpinan = DataFormat.GetString(dr["sJabatanPimpinan"]),
                                    UrusanBAru = DataFormat.GetInteger(dr["idUrusanBaru"]),
                                    Root = DataFormat.GetSingle(dr["Root"]),
                                    Parent = DataFormat.GetInteger(dr["Parent"]),
                                    Level = DataFormat.GetInteger(dr["level"]),
                                    KodeUnit = DataFormat.GetInteger(dr["KodeUnit"]),
                                    KodeParent = DataFormat.ToKodeDinas(DataFormat.GetInteger(dr["Parent"])),
                                    NamaParent = "",//DataFormat.GetInteger(dr["Parent"]) > 0 ? GetNama(DataFormat.GetInteger(dr["Parent"])) : "",
                                    TampilanKode = DataFormat.ToKodeDinas(DataFormat.GetInteger(dr["ID"]))


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
        public List<SKPD> GetNyUser(int _Tahun, int UserID)
        {
            List<SKPD> _lst = new List<SKPD>();
            try
            {
                SSQL = "SELECT * FROM mSKPD ORDER BY ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SKPD()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeURusan"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    Kode = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    Nama = DataFormat.GetString(dr["sNamaSKPD"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                                    Tampilan = DataFormat.GetInteger(dr["ID"]).ToKodeDinas(),
                                    TampilanUrusan = DataFormat.GetInteger(dr["IDurusan"]).ToKodeUrusan(),
                                    NamaUrusan = DataFormat.GetString(dr["sNamaUrusan"]),

                                    NamaBendahara = DataFormat.GetString(dr["sBendPengeluaran"]),
                                    NIPBendahara = DataFormat.GetString(dr["sNIPBendPengeluaran"]),
                                    NamaPimpinan = DataFormat.GetString(dr["sNamaPimpinan"]),
                                    BendaharaPenerimaan = DataFormat.GetString(dr["sBendPenerimaan"]),
                                    NIPBendaharaPenerimaan = DataFormat.GetString(dr["sNIPBendPenerimaan"]),
                                    NamaPPK = DataFormat.GetString(dr["sNamaPPK"]),
                                    NIPPPK = DataFormat.GetString(dr["sNIPPPK"]),
                                    NamaBank = DataFormat.GetString(dr["sBankPenerima"]),
                                    NoRekening = DataFormat.GetString(dr["sBankRekening"]),
                                    NPWP = DataFormat.GetString(dr["sNPWP"]),

                                    NIPPimpinan = DataFormat.GetString(dr["sNIPPimpinan"]),
                                    JabatanPimpinan = DataFormat.GetString(dr["sJabatanPimpinan"]),
                                    UrusanBAru = DataFormat.GetInteger(dr["idUrusanBaru"]),
                                    Root = DataFormat.GetSingle(dr["Root"]),
                                    Parent = DataFormat.GetInteger(dr["Parent"]),
                                    Level = DataFormat.GetInteger(dr["level"]),
                                    KodeUnit = DataFormat.GetInteger(dr["KodeUnit"]),
                                    KodeParent = DataFormat.ToKodeDinas(DataFormat.GetInteger(dr["Parent"])),
                                    NamaParent = "",//DataFormat.GetInteger(dr["Parent"]) > 0 ? GetNama(DataFormat.GetInteger(dr["Parent"])) : "",
                                    TampilanKode = DataFormat.ToKodeDinas(DataFormat.GetInteger(dr["ID"]))


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
        //public Urusan GetUrusanPokok()
        //{
        //    PelaksanaUrusanLogic oLogic = new PelaksanaUrusanLogic(Tahun);
        //    PelaksanaUrusan p = new P


        //}
        public void RKA2TU()
        {

            SSQL = "UPDATE mUrusan  SET btKodekategori = ID/100, btKodeUrusan= ID%100 ";
            _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = "update mPelaksanaUrusan SET btKodekategori=IDDInas/1000000, btKodeUrusan =(IDDInas/10000)%100, btKodeSKPD =substring(Convert(varchar(7),IDDInas),4,2), btKodeUK =substring(Convert(varchar(7),IDDInas),6,2),btKodekategoriPelaksana = IDurusan/100, btKodeUrusanPElaksana=IDurusan%100  ";
            _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = "update tPrograms_A SET btKodekategori=IDDInas/1000000, btKodeUrusan =(IDDInas/10000)%100, btKodeSKPD =substring(Convert(varchar(7),IDDInas),4,2), btKodeUK =substring(Convert(varchar(7),IDDInas),6,2),btKodekategoriPelaksana = IDurusan/100, btKodeUrusanPElaksana=IDurusan%100 , btIDProgram =substring(Convert(varchar(5),IDProgram),4,2)";
            _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = "update tKegiatan_A SET btKodekategori=IDDInas/1000000, btKodeUrusan =(IDDInas/10000)%100, btKodeSKPD =substring(Convert(varchar(7),IDDInas),4,2), btKodeUK =substring(Convert(varchar(7),IDDInas),6,2),btKodekategoriPelaksana = IDurusan/100, btKodeUrusanPElaksana=IDurusan%100 , btIDProgram =substring(Convert(varchar(5),IDProgram),4,2), btIDKegiatan = btIDProgram =substring(Convert(varchar(8),IDKegiatan),6)";
            _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = "update tANggaranRekening_A SET btKodekategori=IDDInas/1000000, btKodeUrusan =(IDDInas/10000)%100, btKodeSKPD =substring(Convert(varchar(7),IDDInas),4,2), btKodeUK =substring(Convert(varchar(7),IDDInas),6,2),btKodekategoriPelaksana = IDurusan/100, btKodeUrusanPElaksana=IDurusan%100 , btIDProgram =substring(Convert(varchar(5),IDProgram),4,2), btIDKegiatan = btIDProgram =substring(Convert(varchar(8),IDKegiatan),6)";
            _dbHelper.ExecuteNonQuery(SSQL);



        }
        //public List<SKPD> GetByParent(int Parent)
        //{
        //    List<SKPD> mListUnit = new List<SKPD>();
        //    try
        //    {
        //        SSQL = "SELECT * FROM mSKPD where Parent =" + Parent.ToString() + " ORDER BY ID";
        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                mListUnit = (from DataRow dr in dt.Rows
        //                        select new SKPD()
        //                        {
        //                            Tahun = DataFormat.GetInteger(dr["iTahun"]),
        //                            ID = DataFormat.GetInteger(dr["ID"]),
        //                            KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
        //                            KodeUrusan = DataFormat.GetInteger(dr["btKodeURusan"]),
        //                            Kode = DataFormat.GetInteger(dr["btKodeSKPD"]),
        //                            Nama = DataFormat.GetString(dr["sNamaSKPD"]),
        //                            IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
        //                            Tampilan = DataFormat.GetInteger(dr["ID"]).ToKodeDinas(),
        //                            TampilanUrusan = DataFormat.GetInteger(dr["IDurusan"]).ToKodeUrusan(),
        //                            NamaBendahara = DataFormat.GetString(dr["sBendPengeluaran"]),
        //                            NIPBendahara = DataFormat.GetString(dr["sNIPBendPengeluaran"]),
        //                            UrusanBAru = DataFormat.GetInteger(dr["idUrusanBaru"]),
        //                            Root = DataFormat.GetSingle(dr["Root"]),
        //                            Parent = DataFormat.GetInteger(dr["Parent"]),
        //                            Level = DataFormat.GetInteger(dr["level"]),
        //                            KodeUnit = DataFormat.GetInteger(dr["KodeUnit"])

        //                        }).ToList();
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
        public SKPD GetByID(int pID, bool withPejabat = false )
        {
            if (pID < 1)
            {
                return null;
            }

            SKPD olRet = new SKPD();
            try
            {

                SSQL = "SELECT s.*,u.sNamaUrusan FROM mSKPD s INNER JOIN mUrusan U ON s.IDUrusan = U.ID where s.ID =@ID ";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@ID", pID));

                //SELECT s.*,u.sNamaUrusan FROM mSKPD s INNER JOIN mUrusan U ON s.IDUrusan = U.ID where s.ID =1020122
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        olRet = new SKPD()
                        {
                            ID = DataFormat.GetInteger(dr["ID"]),
                            KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                            KodeUrusan = DataFormat.GetInteger(dr["btKodeURusan"]),
                            Tahun = DataFormat.GetInteger(dr["iTahun"]),
                            Kode = DataFormat.GetInteger(dr["btKodeSKPD"]),
                            Nama = DataFormat.GetString(dr["sNamaSKPD"]),
                            IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                            Tampilan = DataFormat.GetInteger(dr["ID"]).ToKodeDinas(),
                            TampilanUrusan = DataFormat.GetInteger(dr["IDurusan"]).ToKodeUrusan(),
                            NamaUrusan = DataFormat.GetString(dr["sNamaUrusan"]),
                         
                            NamaBendahara = DataFormat.GetString(dr["sBendPengeluaran"]),
                            NIPBendahara = DataFormat.GetString(dr["sNIPBendPengeluaran"]),
                            NamaPimpinan = DataFormat.GetString(dr["sNamaPimpinan"]),
                            BendaharaPenerimaan = DataFormat.GetString(dr["sBendPenerimaan"]),
                            NIPBendaharaPenerimaan = DataFormat.GetString(dr["sNIPBendPenerimaan"]),
                            NamaPPK = DataFormat.GetString(dr["sNamaPPK"]),
                            NIPPPK = DataFormat.GetString(dr["sNIPPPK"]),
                            NamaBank = DataFormat.GetString(dr["sBankPenerima"]),
                            NoRekening = DataFormat.GetString(dr["sBankRekening"]),
                            NPWP = DataFormat.GetString(dr["sNPWP"]),
                            
                            NIPPimpinan = DataFormat.GetString(dr["sNIPPimpinan"]),
                            JabatanPimpinan = DataFormat.GetString(dr["sJabatanPimpinan"]),
                            UrusanBAru = DataFormat.GetInteger(dr["idUrusanBaru"]),
                            Root = DataFormat.GetSingle(dr["Root"]),
                            Parent = DataFormat.GetInteger(dr["Parent"]),
                            Level = DataFormat.GetInteger(dr["level"]),
                            KodeUnit = DataFormat.GetInteger(dr["KodeUnit"]),
                            KodeParent = DataFormat.ToKodeDinas(DataFormat.GetInteger(dr["Parent"])),
                            NamaParent = "",//DataFormat.GetInteger(dr["Parent"]) > 0 ? GetNama(DataFormat.GetInteger(dr["Parent"])) : "",
                            TampilanKode = DataFormat.ToKodeDinas(DataFormat.GetInteger(dr["ID"])),
                            KodeSIPD = DataFormat.GetString(dr["Kode"]),
                            IDKantor = DataFormat.GetInteger(dr["IdKantor"]),

                        };
                    }
                }
                if (withPejabat == true)
                {
                    GetPejabat(ref olRet);
                }
                return olRet;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return olRet;
            }
        }
        private string GetNama(int pID)
        {
            try
            {
                string sNama = "";
                if (pID == 0)
                    return "";

                SKPD oSKPD = new SKPD();
                oSKPD = GetByID(pID);
                sNama = oSKPD.Nama;

                return oSKPD.Nama;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return "";
            }

        }
        public bool Simpan(ref SKPD _pSKPD)
        {
            try
            {
                if (_pSKPD.ID == 0)
                {
                    int _newID;
                    _newID = Convert.ToInt32(DataFormat.IntToStringWithLeftPad(_pSKPD.KodeKategori, 1) + DataFormat.IntToStringWithLeftPad(_pSKPD.KodeUrusan, 2) + DataFormat.IntToStringWithLeftPad(_pSKPD.Kode, 2) + DataFormat.IntToStringWithLeftPad(_pSKPD.KodeUnit, 2));
                    _pSKPD.ID = _newID;
                    SSQL = "INSERT INTO mSKPD (ID, btKodeKategori, btKodeUrusan,btKodeSKPD, sNamaSKPD, iTahun,IDUrusan, Parent,Root,level,kodeunit) values (" +
                           "@pID, @pbtKodeKategori, @pbtKodeUrusan,@pbtKodeSKPD, @psNamaSKPD,@psiTahun,@psIDUrusan, @Parent,@Root, @pLevel, @pKodeUnit)";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pID", _pSKPD.ID));
                    paramCollection.Add(new DBParameter("@pbtKodeKategori", _pSKPD.KodeKategori));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusan", _pSKPD.KodeUrusan));
                    paramCollection.Add(new DBParameter("@pbtKodeSKPD", _pSKPD.Kode));
                    paramCollection.Add(new DBParameter("@psNamaSKPD", _pSKPD.Nama));
                    paramCollection.Add(new DBParameter("@psiTahun", _pSKPD.Tahun));
                    paramCollection.Add(new DBParameter("@psIDUrusan", _pSKPD.IDUrusan));
                    paramCollection.Add(new DBParameter("@Parent", _pSKPD.Parent));
                    paramCollection.Add(new DBParameter("@Root", _pSKPD.Root));
                    paramCollection.Add(new DBParameter("@pLevel", _pSKPD.Level));
                    paramCollection.Add(new DBParameter("@pKodeUnit", _pSKPD.KodeUnit));



                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                }
                else
                {



                    SSQL = "UPDATE mSKPD SET sNamaSKPD = @psNamaSKPD,sNamaPimpinan=@psNamaPimpinan,sNIPPIMPINAN=@psNIPPIMPINAN, sJabatanPimpinan=@psJabatanPimpinan, sBendPengeluaran=@psBendPengeluaran,sNIPBendPengeluaran=@psNIPBendPengeluaran,Root=@Root,level=@pLevel, kodeunit=@pkodeunit WHERE ID=@pID AND iTahun =@piTahun";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@psNamaSKPD", _pSKPD.Nama == null ? "" : _pSKPD.Nama, DbType.String));
                    paramCollection.Add(new DBParameter("@psNamaPimpinan", _pSKPD.NamaPimpinan == null ? "" : _pSKPD.NamaPimpinan, DbType.String));
                    paramCollection.Add(new DBParameter("@psNIPPIMPINAN", _pSKPD.NIPPimpinan == null ? "" : _pSKPD.NIPPimpinan, DbType.String));
                    paramCollection.Add(new DBParameter("@psJabatanPimpinan", _pSKPD.JabatanPimpinan == null ? "" : _pSKPD.JabatanPimpinan, DbType.String));
                    //  paramCollection.Add(new DBParameter("@pIDUrusan", _pSKPD.IDUrusan == 0 ? _pSKPD.ID / 100 : _pSKPD.IDUrusan));
                    paramCollection.Add(new DBParameter("@psBendPengeluaran", _pSKPD.NamaBendahara == null ? "" : _pSKPD.NamaBendahara, DbType.String));
                    paramCollection.Add(new DBParameter("@psNIPBendPengeluaran", _pSKPD.NIPBendahara == null ? "" : _pSKPD.NIPBendahara, DbType.String));
                    // paramCollection.Add(new DBParameter("@Parent", _pSKPD.Parent));
                    paramCollection.Add(new DBParameter("@Root", _pSKPD.Root, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pLevel", _pSKPD.Level, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pkodeunit", _pSKPD.KodeUnit, DbType.Int32));

                    paramCollection.Add(new DBParameter("@pID", _pSKPD.ID, DbType.Int32));
                    paramCollection.Add(new DBParameter("@piTahun", _pSKPD.Tahun, DbType.Int32));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                }


                return true;


            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool SimpanPejabat(ref SKPD _pSKPD)
        {
            try
            {
                

                    SSQL = "UPDATE mSKPD SET sNamaPimpinan=@psNamaPimpinan,sNIPPIMPINAN=@psNIPPIMPINAN, sJabatanPimpinan=@psJabatanPimpinan, sBendPengeluaran=@psBendPengeluaran,sNIPBendPengeluaran=@psNIPBendPengeluaran WHERE ID=@pID ";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@psNamaPimpinan", _pSKPD.NamaPimpinan == null ? "" : _pSKPD.NamaPimpinan, DbType.String));
                    paramCollection.Add(new DBParameter("@psNIPPIMPINAN", _pSKPD.NIPPimpinan == null ? "" : _pSKPD.NIPPimpinan, DbType.String));
                    paramCollection.Add(new DBParameter("@psJabatanPimpinan", _pSKPD.JabatanPimpinan == null ? "" : _pSKPD.JabatanPimpinan, DbType.String));
                    paramCollection.Add(new DBParameter("@psBendPengeluaran", _pSKPD.NamaBendahara == null ? "" : _pSKPD.NamaBendahara, DbType.String));
                    paramCollection.Add(new DBParameter("@psNIPBendPengeluaran", _pSKPD.NIPBendahara == null ? "" : _pSKPD.NIPBendahara, DbType.String));
                    paramCollection.Add(new DBParameter("@pID", _pSKPD.ID, DbType.Int32));


                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                return true;


            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool SimpanUrusanPemerintahan(List<UrusanDinas> pList)
        {
            try
            {

                UrusanDinasLogic oLogic = new UrusanDinasLogic(Tahun,3);
                int i = 0;
                UrusanDinas udx = new UrusanDinas();
                if (pList.Count > 0)
                {

                    udx = pList[0];
                    if (oLogic.HapusDinas(udx))
                    {
                        foreach (UrusanDinas ud in pList)
                        {
                            oLogic.Simpan(ud);
                        }

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool Hapus(int _pID, int _tahun)
        {
            try
            {

                SSQL = "DELETE from mSKPD WHERE ID=" + _pID.ToString() + " AND iTahun =" + _tahun.ToString();
                //DBParameterCollection paramCollection = new DBParameterCollection();
                //paramCollection.Add(new DBParameter("@pID", _pID));
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
        public bool SetUrusanBaru(SKPD oSKPD, int UrusanBaru)
        {
            try
            {
                SSQL = "update mSKPD set IDURusanBaru =" + UrusanBaru.ToString() + " WHERE ID=" + oSKPD.ID.ToString();
                if (_dbHelper.ExecuteNonQuery(SSQL) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }


        }
        public bool SetUrusanBaru(int _skpd, int _urusanBaru)
        {
            try
            {
                SSQL = "Update mSKPD set IDUrusanBAru =" + _urusanBaru.ToString() + " WHERE ID=" + _skpd.ToString();
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
        public bool GantiDinas(int dinaslama, int dinasbaru)
        {
            SSQL = "Update tkua set iddinas =" + dinasbaru.ToString() + " WHERE iDdinas =" + dinaslama.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "Update tprograms_A set iddinas =" + dinasbaru.ToString() + " WHERE iDdinas =" + dinaslama.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "Update tKegiatan_A set iddinas =" + dinasbaru.ToString() + " WHERE iDdinas =" + dinaslama.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);


            SSQL = "Update tANggaranRekening_A set iddinas =" + dinasbaru.ToString() + " WHERE iDdinas =" + dinaslama.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "Update tANggaranuraian_A set iddinas =" + dinasbaru.ToString() + " WHERE iDdinas =" + dinaslama.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);


            return true;

        }
        //public List<SKPD> Get(int _Tahun )
        //{
        //    List<SKPD> mListUnit = new List<SKPD>();
        //    try
        //    {
        //        SSQL = "SELECT * FROM mSKPD ORDER BY ID";
        //        DataTable dt = new DataTable();
        //        dt=_dbHelper.ExecuteDataTable(SSQL);
        //        if (dt != null){
        //           if (dt.Rows.Count > 0)
        //            {
        //                mListUnit = (from DataRow dr in dt.Rows                                
        //                        select new SKPD()
        //                        {
        //                            Tahun = DataFormat.GetInteger(dr["iTahun"]),
        //                            ID = DataFormat.GetInteger(dr["ID"]),
        //                            KodeKategori= DataFormat.GetInteger(dr["btKodeKategori"]),
        //                            KodeUrusan= DataFormat.GetInteger(dr["btKodeURusan"]),
        //                            Kode= DataFormat.GetInteger(dr["btKodeSKPD"]),
        //                            Nama= DataFormat.GetString(dr["sNamaSKPD"]),
        //                            IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
        //                            Tampilan = DataFormat.GetInteger(dr["ID"]).ToKodeDinas(),
        //                            TampilanUrusan = DataFormat.GetInteger(dr["IDurusan"]).ToKodeUrusan(),
        //                            NamaBendahara= DataFormat.GetString(dr["sBendPengeluaran"]),
        //                            NIPBendahara = DataFormat.GetString(dr["sNIPBendPengeluaran"]),
        //                            UrusanBAru = DataFormat.GetInteger(dr["idUrusanBaru"]),
        //                            Root  = DataFormat.GetSingle(dr["Root"]),
        //                            Parent= DataFormat.GetInteger(dr["Parent"]),
        //                            Level= DataFormat.GetInteger(dr["level"]),
        //                           //odeUnit = DataFormat.GetInteger(dr["KodeUnit"])

        //                        }).ToList();
        //            }
        //        }
        //        return mListUnit;
        //    } catch(Exception ex){
        //        _isError = true;
        //        _lastError = ex.Message;
        //        return mListUnit;
        //    }
        //}
        //public List<SKPD> GetByParent(int Parent)
        //{
        //    List<SKPD> mListUnit = new List<SKPD>();
        //    try
        //    {
        //        SSQL = "SELECT * FROM mSKPD where Parent =" + Parent.ToString() + " ORDER BY ID";
        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                mListUnit = (from DataRow dr in dt.Rows
        //                        select new SKPD()
        //                        {
        //                            Tahun = DataFormat.GetInteger(dr["iTahun"]),
        //                            ID = DataFormat.GetInteger(dr["ID"]),
        //                            KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
        //                            KodeUrusan = DataFormat.GetInteger(dr["btKodeURusan"]),
        //                            Kode = DataFormat.GetInteger(dr["btKodeSKPD"]),
        //                            Nama = DataFormat.GetString(dr["sNamaSKPD"]),
        //                            IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
        //                            Tampilan = DataFormat.GetInteger(dr["ID"]).ToKodeDinas(),
        //                            TampilanUrusan = DataFormat.GetInteger(dr["IDurusan"]).ToKodeUrusan(),
        //                            NamaBendahara = DataFormat.GetString(dr["sBendPengeluaran"]),
        //                            NIPBendahara = DataFormat.GetString(dr["sNIPBendPengeluaran"]),
        //                            UrusanBAru = DataFormat.GetInteger(dr["idUrusanBaru"]),
        //                            Root = DataFormat.GetSingle(dr["Root"]),
        //                            Parent = DataFormat.GetInteger(dr["Parent"]),
        //                            Level = DataFormat.GetInteger(dr["level"]),
        //                            KodeUnit = DataFormat.GetInteger(dr["KodeUnit"])

        //                        }).ToList();
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
        //public SKPD GetByID(int pID)
        //{
        //    SKPD olRet = new SKPD();
        //    try
        //    {
        //        SSQL = "SELECT s.*,u.sNamaUrusan FROM mSKPD s INNER JOIN mUrusan U ON s.IDUrusan = U.ID where s.ID =" + pID.ToString();
        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                DataRow dr = dt.Rows[0];
        //                olRet = new SKPD()
        //                {
        //                    ID = DataFormat.GetInteger(dr["ID"]),
        //                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
        //                    KodeUrusan = DataFormat.GetInteger(dr["btKodeURusan"]),
        //                    Tahun = DataFormat.GetInteger(dr["iTahun"]),                                    
        //                    Kode = DataFormat.GetInteger(dr["btKodeSKPD"]),
        //                    Nama = DataFormat.GetString(dr["sNamaSKPD"]),
        //                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
        //                    Tampilan = DataFormat.GetInteger(dr["ID"]).ToKodeDinas(),
        //                    TampilanUrusan = DataFormat.GetInteger(dr["IDurusan"]).ToKodeUrusan(),
        //                    NamaUrusan = DataFormat.GetString(dr["sNamaUrusan"]),
        //                    NamaBendahara = DataFormat.GetString(dr["sBendPengeluaran"]),
        //                    NIPBendahara = DataFormat.GetString(dr["sNIPBendPengeluaran"]),
        //                    NamaPimpinan = DataFormat.GetString(dr["sNamaPimpinan"]),
        //                    NIPPimpinan = DataFormat.GetString(dr["sNIPPimpinan"]),
        //                    JabatanPimpinan = DataFormat.GetString(dr["sJabatanPimpinan"]),
        //                    UrusanBAru = DataFormat.GetInteger(dr["idUrusanBaru"]),
        //                    Root = DataFormat.GetSingle(dr["Root"]),
        //                    Parent = DataFormat.GetInteger(dr["Parent"]),
        //                    Level = DataFormat.GetInteger(dr["level"]),
        //                    KodeUnit = DataFormat.GetInteger(dr["KodeUnit"])

        //                 };      
        //            }
        //        }
        //        return olRet;
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message;
        //        return olRet;
        //    }
        //}
        //public bool Simpan(ref SKPD _pSKPD)
        //{
        //    try
        //    {
        //        if (_pSKPD.ID == 0)
        //        {
        //            int _newID;
        //            _newID = Convert.ToInt32(DataFormat.IntToStringWithLeftPad(_pSKPD.KodeKategori, 1) + DataFormat.IntToStringWithLeftPad(_pSKPD.KodeUrusan, 2) + DataFormat.IntToStringWithLeftPad(_pSKPD.Kode, 2) + DataFormat.IntToStringWithLeftPad(_pSKPD.KodeUnit , 2));
        //            _pSKPD.ID = _newID;
        //            SSQL = "INSERT INTO mSKPD (ID, btKodeKategori, btKodeUrusan,btKodeSKPD, sNamaSKPD, iTahun,IDUrusan, Parent,Root,level,kodeunit) values (" +
        //                   "@pID, @pbtKodeKategori, @pbtKodeUrusan,@pbtKodeSKPD, @psNamaSKPD,@psiTahun,@psIDUrusan, @Parent,@Root, @pLevel, @pKodeUnit)";

        //            DBParameterCollection paramCollection = new DBParameterCollection();
        //            paramCollection.Add(new DBParameter("@pID", _pSKPD.ID));
        //            paramCollection.Add(new DBParameter("@pbtKodeKategori", _pSKPD.KodeKategori));
        //            paramCollection.Add(new DBParameter("@pbtKodeUrusan", _pSKPD.KodeUrusan));
        //            paramCollection.Add(new DBParameter("@pbtKodeSKPD", _pSKPD.Kode));
        //            paramCollection.Add(new DBParameter("@psNamaSKPD", _pSKPD.Nama));
        //            paramCollection.Add(new DBParameter("@psiTahun",_pSKPD.Tahun));
        //            paramCollection.Add(new DBParameter("@psIDUrusan",_pSKPD.IDUrusan));
        //            paramCollection.Add(new DBParameter("@Parent",_pSKPD.Parent));
        //            paramCollection.Add(new DBParameter("@Root", _pSKPD.Root));
        //            paramCollection.Add(new DBParameter("@pLevel", _pSKPD.Level));
        //            paramCollection.Add(new DBParameter("@pKodeUnit", _pSKPD.KodeUnit));



        //            _dbHelper.ExecuteNonQuery(SSQL,paramCollection);
        //        }
        //        else
        //        {



        //            SSQL = "UPDATE mSKPD SET sNamaSKPD = @psNamaSKPD,sNamaPimpinan=@psNamaPimpinan,sNIPPIMPINAN=@psNIPPIMPINAN, sJabatanPimpinan=@psJabatanPimpinan, IDUrusan=@pIDUrusan,sBendPengeluaran=@psBendPengeluaran,sNIPBendPengeluaran=@psNIPBendPengeluaran,Parent=@Parent,Root=@Root,level=@pLevel, kodeunit=@pkodeunit WHERE ID=@pID AND iTahun =@piTahun";
        //            DBParameterCollection paramCollection = new DBParameterCollection();
        //            paramCollection.Add(new DBParameter("@psNamaSKPD", _pSKPD.Nama==null?"":_pSKPD.Nama));
        //            paramCollection.Add(new DBParameter("@psNamaPimpinan", _pSKPD.NamaPimpinan==null?"":_pSKPD.NamaPimpinan));
        //            paramCollection.Add(new DBParameter("@psNIPPIMPINAN", _pSKPD.NIPPimpinan==null?"":_pSKPD.NIPPimpinan));
        //            paramCollection.Add(new DBParameter("@psJabatanPimpinan", _pSKPD.JabatanPimpinan==null?"":_pSKPD.JabatanPimpinan));
        //            paramCollection.Add(new DBParameter("@pIDUrusan", _pSKPD.IDUrusan == 0 ? _pSKPD.ID / 100 : _pSKPD.IDUrusan));
        //            paramCollection.Add(new DBParameter("@psBendPengeluaran", _pSKPD.NamaBendahara == null ? "" : _pSKPD.NamaBendahara));
        //            paramCollection.Add(new DBParameter("@psNIPBendPengeluaran", _pSKPD.NIPBendahara == null ? "" : _pSKPD.NIPBendahara));
        //            paramCollection.Add(new DBParameter("@Parent", _pSKPD.Parent));
        //            paramCollection.Add(new DBParameter("@Root", _pSKPD.Root));
        //            paramCollection.Add(new DBParameter("@pLevel", _pSKPD.Level));
        //            paramCollection.Add(new DBParameter("@pkodeunit", _pSKPD.KodeUnit));

        //            paramCollection.Add(new DBParameter("@pID", _pSKPD.ID));
        //            paramCollection.Add(new DBParameter("@piTahun", _pSKPD.Tahun));
                    
        //            _dbHelper.ExecuteNonQuery(SSQL,paramCollection);


        //        }
                
                
        //        return true;
                

        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message + " " + SSQL;
        //        return false;

        //    }

        //}
        //public bool SimpanUrusanPemerintahan(List<UrusanDinas> pList)
        //{
        //    try
        //    {

        //        UrusanDinasLogic oLogic = new UrusanDinasLogic(Tahun);
        //        int i = 0 ;   
        //        UrusanDinas udx = new UrusanDinas();
        //        if (pList.Count>0){

        //            udx = pList[0];
        //            if (oLogic.HapusDinas(udx))
        //            {
        //                 foreach (UrusanDinas ud in pList){
        //                    oLogic.Simpan( ud);
        //                }
                    
        //            }
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message + " " + SSQL;
        //        return false;

        //    }

        //}
        //public bool Hapus(int _pID, int _tahun )
        //{
        //    try
        //    {
                 
        //        SSQL = "DELETE from mSKPD WHERE ID="  +_pID.ToString() + " AND iTahun =" + _tahun.ToString();                
        //        //DBParameterCollection paramCollection = new DBParameterCollection();
        //        //paramCollection.Add(new DBParameter("@pID", _pID));
        //        _dbHelper.ExecuteNonQuery(SSQL) ;
                
        //            return true;
        //    } catch(Exception ex)
        //    {

        //         _isError = true;
        //        _lastError = ex.Message ;
        //        return false;

        //    }

        //}
        //public bool SetUrusanBaru(SKPD oSKPD, int UrusanBaru)
        //{
        //    try
        //    {
        //        SSQL = "update mSKPD set IDURusanBaru =" + UrusanBaru.ToString() + " WHERE ID=" + oSKPD.ID.ToString(); 
        //        if (_dbHelper.ExecuteNonQuery(SSQL) > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message + " " + SSQL;
        //        return false;

        //    }


        //}
        //public bool SetUrusanBaru(int _skpd, int _urusanBaru)
        //{
        //    try
        //    {
        //        SSQL = "Update mSKPD set IDUrusanBAru =" + _urusanBaru.ToString() + " WHERE ID=" + _skpd.ToString();
        //        _dbHelper.ExecuteNonQuery(SSQL);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message;
        //        return false;

        //    }


        //}
    }
}
