using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using DTO;
using Formatting;
using BP;

namespace BP
{
    public class PejabatLogic : BP
    {
        public PejabatLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mPejabat";
        }
        public bool PerbaikiTabelPejabat() {

            try
            {
                SSQL = "SELECT distinct *  into PejabatX FROM mPejabat";
                _dbHelper.ExecuteNonQuery(SSQL);
                

                List<Pejabat> lstPejabat = new List<Pejabat>();
                SSQL = "SELect * from PejabatX ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lstPejabat = (from DataRow dr in dt.Rows
                                      select new Pejabat()
                                      {
                                          ID = DataFormat.GetInteger(dr["ID"]),
                                          Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                          IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                          Jabatan = DataFormat.GetString(dr["sJabatan"]),
                                          Unit = DataFormat.GetInteger(dr["btKOdeUK"]),
                                          Nama = DataFormat.GetString(dr["sNama"]),
                                          NIP = DataFormat.GetString(dr["sNIP"]),
                                          Active = DataFormat.GetSingle(dr["bActive"]),
                                          PPKD = DataFormat.GetSingle(dr["PPKD"]),
                                          NoRekening = DataFormat.GetString(dr["sRek"]),
                                          NPWP = DataFormat.GetString(dr["sNPWP"]),
                                          NamaBank = DataFormat.GetString(dr["sBank"]),
                                          NamaDalamRekeningBank = DataFormat.GetString(dr["NamaDlmRekeningBank"]),

                                      }).ToList();
                    }
                    SSQL = "DELETE mPejabat";
                    _dbHelper.ExecuteNonQuery(SSQL);

                    foreach (Pejabat p in lstPejabat)
                    {
                        p.ID = 0;
                        p.TanggalAktiv = new DateTime(2020, 1, 1);// DateTime.Now.Date;

                        Simpan(  p);
                    }
                    SSQL = "DROP TABLE PejabatX ";
                    _dbHelper.ExecuteNonQuery(SSQL);

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
        public List<Pejabat> Get()
        {
            List<Pejabat> _lst = new List<Pejabat>();
            try
            {



                SSQL = "SELECT * FROM " + m_sNamaTabel + " ORDER BY ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Pejabat()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Jabatan = DataFormat.GetString(dr["sJabatan"]),
                                    Unit = DataFormat.GetInteger(dr["btKOdeUK"]),
                                    Nama = DataFormat.GetString(dr["sNama"]),
                                    NIP = DataFormat.GetString(dr["sNIP"]),
                                    Active = DataFormat.GetSingle(dr["bActive"]),
                                    PPKD = DataFormat.GetSingle(dr["PPKD"]),
                                    NoRekening = DataFormat.GetString(dr["sRek"]),
                                    NPWP = DataFormat.GetString(dr["sNPWP"]),
                                    NamaBank = DataFormat.GetString(dr["sBank"]),
                                    NamaDalamRekeningBank = DataFormat.GetString(dr["NamaDlmRekeningBank"]),

       

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

        public Pejabat GetByJenis(int iJenis,int pSKPD=0, int PPKD =0, int punit =0 , Single bActive = 0 )
        {
            Pejabat p = new Pejabat();
            try
            {
                DateTime sekarang = DateTime.Now.Date;
                return GetByJenisInDate(iJenis, pSKPD, PPKD, punit, sekarang);
                
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }
        }
        public Pejabat GetByJenisInDate(int iJenis,int pSKPD, int PPKD, int punit , DateTime tanggal )
        {
            Pejabat p = new Pejabat();
            try
            {
                SSQL = "SELECT  *  FROM mPejabat mp INNER JOIN " +
                    "(Select btJenis,max(TanggalAktiv) as maxdate " +
                    "from mPejabat where TanggalAktiv <=@TANGGAL and btJenis=@JENIS ";
                
               DBParameterCollection paramCollection = new DBParameterCollection();

               paramCollection.Add(new DBParameter("@TANGGAL", tanggal,DbType.Date));
               paramCollection.Add(new DBParameter("@JENIS", iJenis));
                    SSQL = SSQL + " AND IDDInas=@DINAS " ;
                    paramCollection.Add(new DBParameter("@DINAS", pSKPD));
                    
                   if (pSKPD >0)
                    {
                        SSQL = SSQL + " AND (isnull(btKodeUK,0) =@KODEUK";

                        if (punit == 0)
                        {
                            SSQL = SSQL + " or btKodeUK =1 ";
                        }
                        if (punit == 1)
                        {
                            SSQL = SSQL + " or btKodeUK =0 ";
                        }

                        paramCollection.Add(new DBParameter("@KODEUK", punit));
                    SSQL = SSQL + ")";
                    }
                 SSQL = SSQL + " Group by btJenis ) a  ON mp.btJenis = a.btJenis and a.maxdate = mp.TanggalAktiv ";
                SSQL = SSQL + "  WHERE 1>0  ";
                    SSQL = SSQL + " AND IDDInas=@DINAS ";

                    //SSQL = SSQL + "  WHERE mp.IDDInas =@DINAS ";
                    if (punit > -1 && pSKPD>0)
                    {


                        SSQL = SSQL + " and (isnull(btKodeUK,0) =@KODEUK ";
                        if (punit == 0)
                        {
                            SSQL = SSQL + " or btKodeUK =1 ";
                        }
                        if (punit == 1)
                        {
                            SSQL = SSQL + " or btKodeUK =0 ";
                        }
                        SSQL = SSQL + ")";
                    }
                
                    
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {

                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        p = new Pejabat()
                        {
                            ID = DataFormat.GetInteger(dr["ID"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"]),
                            IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                            Jabatan = DataFormat.GetString(dr["sJabatan"]),
                            Unit = DataFormat.GetInteger(dr["btKOdeUK"]),
                            Nama = DataFormat.GetString(dr["sNama"]),
                            NIP = DataFormat.GetString(dr["sNIP"]),
                            Active = DataFormat.GetSingle(dr["bActive"]),
                            PPKD = DataFormat.GetSingle(dr["PPKD"]),
                            NoRekening = DataFormat.GetString(dr["sRek"]),
                            NPWP = DataFormat.GetString(dr["sNPWP"]),
                            NamaBank = DataFormat.GetString(dr["sBank"]),
                            NamaDalamRekeningBank= DataFormat.GetString(dr["NamaDlmRekeningBank"]),
                            TanggalAktiv=DataFormat.GetDateTime(dr["TanggalAktiv"])

                        };
                    }
                    else
                    {
                        p = GetFromOPDAtauUK( iJenis, pSKPD, PPKD, punit );
        
                    }
                }

                return p;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }
        }



        private Pejabat GetFromOPDAtauUK(int iJenis, int pSKPD = 0, int PPKD = 0, int punit = 0)
        {
            Pejabat p = new Pejabat();
            if (punit <= 1)
            {
                SKPDLogic oSKPDLogic = new SKPDLogic(Tahun);
                SKPD oSKPD = new SKPD();
                oSKPD = oSKPDLogic.GetByID(pSKPD);
                p = new Pejabat();
                if (iJenis == 6)
                {
                    p.ID = 0;// oSKPD.ID;
                    p.Nama = oSKPD.NamaPimpinan;
                    p.Jabatan = oSKPD.JabatanPimpinan;
                    p.NIP = oSKPD.NIPPimpinan;
                    p.Jenis = 6;
                    p.IDDInas = pSKPD;
                    p.Unit = punit;
                    p.TanggalAktiv = new DateTime(Tahun, 1, 1);

                }

                if (iJenis == 8)
                {
                    p.ID = 0;// oSKPD.ID;
                    p.Nama = oSKPD.NamaBendahara;
                    p.Jabatan = "BENDAHARA PENGELUARAN";
                    p.NIP = oSKPD.NIPBendahara;
                    p.NamaBank = "123";
                    p.NoRekening = oSKPD.NoRekening;
                    p.NPWP = oSKPD.NPWP;
                    p.Jenis = 8;
                    p.IDDInas = pSKPD;
                    p.Unit = punit;
                    p.TanggalAktiv = new DateTime(Tahun, 1, 1);

                }
                //PPK
                if (iJenis == 7)
                {
                    p.ID = 0;// oSKPD.ID;
                    p.Nama = oSKPD.NamaPPK ;
                    p.Jabatan = "PPK";
                    p.NIP = oSKPD.NIPPPK;
                    p.Jenis = 7;
                    p.IDDInas = pSKPD;
                    p.Unit = punit;
                    p.TanggalAktiv = new DateTime(Tahun, 1, 1);

                }
                p.Jenis = iJenis;
                p.ID = 0;
                Simpan(ref p);
               // p.ID = 0;
            }
            else
            {
                UnitKerjaLogic oSKPDLogic = new UnitKerjaLogic(Tahun);
                Unit unitkerja = new Unit();
                unitkerja = oSKPDLogic.GetByID(pSKPD + punit);
                p.Unit = punit;
                p = new Pejabat();
                if (iJenis == 6)
                {
                    p.ID = 0;// oSKPD.ID;
                    p.Nama = unitkerja.NamaPimpinan ;
                    p.Jabatan = unitkerja.JabatanPimpinan;
                    p.NIP = unitkerja.NIPPimpinan;
                    p.Unit = punit;
                    p.Jenis = 6;
                    p.IDDInas = pSKPD;
                    p.TanggalAktiv = new DateTime(Tahun, 1, 1);


                }
                if (iJenis == 8)
                {
                    p.ID = 0;// oSKPD.ID;
                    p.Nama = unitkerja.NamaBendahara;
                    p.Jabatan = "BENDAHARA PENGELUARAN PEMBANTU";
                    p.NIP = unitkerja.NIPBendahara;
                    p.NamaBank = "123";
                    p.NoRekening = unitkerja.NoRekening;
                    p.NPWP = unitkerja.NPWP;
                    p.Unit = punit;
                    p.Jenis = 8;
                    p.IDDInas = pSKPD;
                    p.TanggalAktiv = new DateTime(Tahun, 1, 1);
                }

                p.Jenis = iJenis;
                p.ID = 0;
                Simpan(ref p);
               // p.ID = 0;
            }
            return p;
        }

        public List<Pejabat> GetKabidPerbend()
        {
            List<Pejabat> _lst = new List<Pejabat>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " where btJenis= 4 ";// +(JENIS_JABATAN.ID_JENIS_KABIDPERBEND).ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Pejabat()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Jabatan = DataFormat.GetString(dr["sJabatan"]),
                                    Nama = DataFormat.GetString(dr["sNama"]),
                                    NIP = DataFormat.GetString(dr["sNIP"]),
                                    Active = DataFormat.GetSingle(dr["bActive"]),
                                    PPKD = DataFormat.GetSingle(dr["PPKD"]),
                                    NoRekening = DataFormat.GetString(dr["sRek"]),
                                    NPWP = DataFormat.GetString(dr["sNPWP"]),
                                    NamaBank = DataFormat.GetString(dr["sBank"])

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
        public List<Pejabat> GetByJenisAndDinas(int iJenis, int pDinas, int KodeUK)
        {
            List<Pejabat> _lst = new List<Pejabat>();
            try
            {
                SSQL = "SELECT * FROM mPejabat where iddinas=@DINAS " ;

               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@DINAS", pDinas));
               if (KodeUK > 0)
               {
                   SSQL = SSQL + " AND btKodeUK =@KODEUK ";
                   paramCollection.Add(new DBParameter("@KODEUK", KodeUK));
               }
               
               if (iJenis > 0)
               {
                   SSQL = SSQL + " AND btJenis= @JENIS ";
                   paramCollection.Add(new DBParameter("@JENIS", iJenis));
               

               }
               SSQL = SSQL + " ORDER BY sJabatan";
               DataTable dt = new DataTable();

                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Pejabat()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Unit = DataFormat.GetInteger(dr["btKodeUK"]),
                                    Jabatan = DataFormat.GetString(dr["sJabatan"]),
                                    Nama = DataFormat.GetString(dr["sNama"]),
                                    NIP = DataFormat.GetString(dr["sNIP"]),
                                    Active = DataFormat.GetSingle(dr["bActive"]),
                                    PPKD = DataFormat.GetSingle(dr["PPKD"]),
                                    NoRekening = DataFormat.GetString(dr["sRek"]),
                                    NPWP = DataFormat.GetString(dr["sNPWP"]),
                                    NamaBank = DataFormat.GetString(dr["sBank"]),
                                    TanggalAktiv = DataFormat.GetDate(dr["TanggalAktiv"]),
                                    NamaDalamRekeningBank = DataFormat.GetString(dr["NamaDlmRekeningBank"]),

                                }).ToList();
                    }
                    else
                    {
                        if (iJenis == 8)
                        {
                            SKPD oSKPD = new SKPD();
                            SKPDLogic oSKPDLogic = new SKPDLogic(Tahun);
                            oSKPD = oSKPDLogic.GetByID(pDinas);
                            Pejabat p = new Pejabat();
                            p.ID = 0;
                            p.Jenis = 8;
                            p.IDDInas = pDinas;
                            p.Unit = KodeUK;
                            p.Jabatan = "Bendahara Pengeluaran";
                            p.NIP = oSKPD.NIPBendahara;
                            p.Nama = oSKPD.NamaBendahara;
                            p.NoRekening = oSKPD.NoRekening;
                            p.NPWP = oSKPD.NPWP;
                            p.NamaBank = oSKPD.NamaBank;
                            p.TanggalAktiv = new DateTime(Tahun, 1, 1);
                            p.NamaDalamRekeningBank = oSKPD.NamaBendahara;
                            if (Simpan(ref p) == true)
                            {
                                _lst.Add(p);
                            }
                        }

                        if (iJenis == 10)
                        {
                            UnitKerjaLogic oSKPDLogic = new UnitKerjaLogic(Tahun);
                            Unit unitkerja = new Unit();
                            unitkerja = oSKPDLogic.GetByID(pDinas + KodeUK);
                            Pejabat p = new Pejabat();
                            
                            p.Unit = KodeUK;
                            
                            
                            p.ID = 0;
                            p.Jenis = 10;
                            p.IDDInas = pDinas;
                            p.Unit = KodeUK;
                            p.Jabatan = "Bendahara Pengeluaran Pembantu";
                            p.NIP = unitkerja.NIPBendahara ;
                            p.Nama = unitkerja.NamaBendahara ;
                            p.NoRekening = unitkerja.NoRekening;
                            p.NPWP = unitkerja.NPWP;
                            p.NamaBank = "123";
                            p.TanggalAktiv = new DateTime(Tahun, 1, 1);
                            p.NamaDalamRekeningBank = unitkerja.NamaBendahara;
                            if (Simpan(ref p) == true)
                            {
                                _lst.Add(p);
                            }
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
        public Pejabat GetKepalaDaerah()
        {
            Pejabat p = new Pejabat();
            p = GetByJenis(1);
            return p;


        }

        
        public Pejabat GetKepalaPPKD()
        {
            Pejabat p = new Pejabat();
            p = GetByJenis((int)JENIS_JABATAN.ID_JENIS_KEPALAPPKD);
            return p;

        }
        public Pejabat GetSEKDA()
        {
            Pejabat p = new Pejabat();
            p = GetByJenis((int)JENIS_JABATAN.ID_JENIS_SEKDA);
            return p;

        }
        public Pejabat GetKepalaDinas(int IDDInas, int PPKD, DateTime d ,int Unit =0 )
        {
            Pejabat p = new Pejabat();
            
            p = GetByJenisInDate((int)JENIS_JABATAN.ID_JENIS_KEPALADINAS, IDDInas, PPKD, Unit,d);

            return p;
        }

        public Pejabat GetKuaaPenggunaAnggaranPenerimaan(int IDDInas, int PPKD, DateTime d, int Unit = 0)
        {
            Pejabat p = new Pejabat();

            p = GetByJenisInDate((int)JENIS_JABATAN.ID_JENIS_KUAASAPENGGUNAANGGARANPENDAPATAN, IDDInas, PPKD, Unit, d);

            return p;
        }
        
        public Pejabat GetBendaharaDinas(int IDDInas, int PPKD, DateTime d, int Unit = 0)
        {
            Pejabat p = new Pejabat();

            if (Unit <= 1)
            {
                p = GetByJenisInDate((int)JENIS_JABATAN.ID_JENIS_BEMDAHARAPENGELUARAN, IDDInas, PPKD, Unit, d);
            }
            else
            {
                p=  GetByJenisInDate((int)JENIS_JABATAN.ID_JENIS_BEMDAHARAPENGELUARANPEMBANTU, IDDInas, PPKD, Unit, d);
            }
            return p;
        }

        public Pejabat GetBendaharaPenerimaan(int IDDInas, int PPKD, DateTime d, int Unit = 0  )
        {
            Pejabat p = new Pejabat();
            p = GetByJenisInDate((int)JENIS_JABATAN.ID_JENIS_BEMDAHARAPENERIMAAN, IDDInas, PPKD, Unit, d);
            return p;
        }
        public Pejabat GetPPKSKPD(int IDDInas, int PPKD, DateTime d, int unit=0 )
        {
            Pejabat p = new Pejabat();
            p = GetByJenisInDate((int)JENIS_JABATAN.ID_JENIS_PPK, IDDInas, PPKD, unit, d);
            return p;
        }


        public Pejabat SimpanBendaharaPenerimaan(Pejabat p)
        {
            p.Jenis = (int)JENIS_JABATAN.ID_JENIS_BEMDAHARAPENERIMAAN;
            Simpan(ref p);
            return p;

            
        }
        public Pejabat SimpanPPKSKPD(Pejabat p)
        {
            p.Jenis = (int)JENIS_JABATAN.ID_JENIS_PPK;
            Simpan(ref p);
            return p;
        }



        public Pejabat GetBendaharaPPKD()
        {

            Pejabat p = new Pejabat();
            p = GetByJenis((int)JENIS_JABATAN.ID_JENIS_BENDAHARAPPKD);
            return p;
        }
      
        
        public Pejabat SimpanKepalaDaerah(Pejabat p)
        {
            p.Jenis = (int)JENIS_JABATAN.ID_JENIS_KADA;
            Simpan(ref p);
            return p;

        }
        public Pejabat SimpanSEKDA(Pejabat p)
        {
            p.Jenis = (int)JENIS_JABATAN.ID_JENIS_SEKDA;
            Simpan(ref p);
            return p;
        }

        public Pejabat SimpanKepalaPPKD(Pejabat p)
        {
            p.Jenis = (int)JENIS_JABATAN.ID_JENIS_KEPALAPPKD;
            Simpan(ref p);
            return p;
        }

        public Pejabat SimpanKepalaDinas(Pejabat p)
        {
            p.Jenis = (int)JENIS_JABATAN.ID_JENIS_KEPALADINAS ;
            Simpan(ref p);
            return p;
        }

        public Pejabat SimpanBendaharaPengeluaran(Pejabat p)
        {
            p.Jenis = (int)JENIS_JABATAN.ID_JENIS_BEMDAHARAPENGELUARAN ;
            Simpan(ref p);
            return p;
        }
        public Pejabat SimpanBendaharaPPKD(Pejabat p)
        {
            p.Jenis = (int)JENIS_JABATAN.ID_JENIS_BENDAHARAPPKD;
            Simpan(ref p);
            return p;
        }





        public Pejabat GetByID(int pID)
        {
            Pejabat _oPejabat = new Pejabat();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE ID =@ID ";
                
               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@ID", pID));

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        _oPejabat = new Pejabat()
                        {
                            ID = DataFormat.GetInteger(dr["ID"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"]),
                            IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                            Jabatan = DataFormat.GetString(dr["sJabatan"]),
                            Nama = DataFormat.GetString(dr["sNama"]),
                            NIP = DataFormat.GetString(dr["sNIP"]),
                            Active = DataFormat.GetSingle(dr["bActive"]),
                            PPKD = DataFormat.GetSingle(dr["PPKD"]),
                            NoRekening = DataFormat.GetString(dr["sRek"]),
                            NPWP = DataFormat.GetString(dr["sNPWP"]),
                            NamaBank = DataFormat.GetString(dr["sBank"]),
                            Unit = DataFormat.GetInteger(dr["btKOdeUK"]),

                        };
                    }
                }
                return _oPejabat;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _oPejabat;
            }
        }
        private bool DeactivatePejabat(Pejabat _pPejabat)
        {
            try
            {
                SSQL = "UPDATE mPejabat Set bActive=0 WHERE IDDinas=@DINAS AND btKodeUK =@Unit AND btJenis =@Jenis AND bActive= 1 ";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@DINAS", _pPejabat.IDDInas));
                paramCollection.Add(new DBParameter("@Unit", _pPejabat.Unit));
                paramCollection.Add(new DBParameter("@Jenis", _pPejabat.Jenis));

                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return false;
            }
            



        }
        public bool Simpan(ref Pejabat _pPejabat)
        {


            try
            {
                int _newID;

                if (_pPejabat.ID == 0)
                {

                    _newID = GetMaxID();
                    _newID++;
                   

                    SSQL = "INSERT INTO " + m_sNamaTabel + " (ID,btJenis,IDDInas,sJabatan,sNama,sNIP,bActive,PPKD,sRek,sNPWP,sBank,btKodeUK,NamaDlmRekeningBank,TanggalAktiv) values " +
                        "(@pID,@pbtJenis,@pIDDInas,@psJabatan,@psNama,@psNIP,@pbActive,@pBPPD,@psRek,@psNPWP,@psBank,@KodeUK,@NamaDlmRekeningBank,@TANGGAL)";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pID", _newID));
                    paramCollection.Add(new DBParameter("@pbtJenis", _pPejabat.Jenis));
                    paramCollection.Add(new DBParameter("@pIDDInas", _pPejabat.IDDInas));
                    paramCollection.Add(new DBParameter("@psJabatan", _pPejabat.Jabatan));
                    paramCollection.Add(new DBParameter("@psNama", _pPejabat.Nama));
                    paramCollection.Add(new DBParameter("@psNIP", _pPejabat.NIP));
                    paramCollection.Add(new DBParameter("@pbActive", 1));
                    paramCollection.Add(new DBParameter("@pBPPD", _pPejabat.PPKD));
                    paramCollection.Add(new DBParameter("@psRek", _pPejabat.NoRekening == null ? "" : _pPejabat.NoRekening));
                    paramCollection.Add(new DBParameter("@psNPWP", _pPejabat.NPWP == null ? "" : _pPejabat.NPWP));
                    paramCollection.Add(new DBParameter("@psBank", _pPejabat.NamaBank == null ? "" : _pPejabat.NamaBank));
                    paramCollection.Add(new DBParameter("@KodeUK", _pPejabat.Unit));
                    paramCollection.Add(new DBParameter("@NamaDlmRekeningBank", _pPejabat.NamaDalamRekeningBank ));
                    paramCollection.Add(new DBParameter("@TANGGAL", _pPejabat.TanggalAktiv,DbType.Date));
                    
                    



                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    _pPejabat.ID = _newID;

                }
                else
                {
                    _newID = _pPejabat.ID;

                    SSQL = "UPDATE " + m_sNamaTabel + " SET btJenis=@pbtJenis ,sJabatan=@psJabatan,sNama=@psNama,"+
                        " sNIP=@psNIP,bActive=@pbActive, PPKD=@pPPKD,sRek=@psRek,sNPWP=@sNPWP,sBank=@sBank,"+
                        " NamaDlmRekeningBank=@NamaDlmRekeningBank,TanggalAktiv=@TANGGAL, btKodeUK=@KodeUK WHERE ID=@pID";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pbtJenis", _pPejabat.Jenis));
                    paramCollection.Add(new DBParameter("@pIDDInas", _pPejabat.IDDInas));
                    paramCollection.Add(new DBParameter("@psJabatan", _pPejabat.Jabatan));
                    paramCollection.Add(new DBParameter("@psNama", _pPejabat.Nama));
                    paramCollection.Add(new DBParameter("@psNIP", _pPejabat.NIP));
                    paramCollection.Add(new DBParameter("@pbActive", _pPejabat.Active));
                    paramCollection.Add(new DBParameter("@pPPKD", _pPejabat.PPKD));
                    paramCollection.Add(new DBParameter("@psRek", _pPejabat.NoRekening == null ? "" : _pPejabat.NoRekening));
                    paramCollection.Add(new DBParameter("@sNPWP", _pPejabat.NPWP == null ? "" : _pPejabat.NPWP));
                    paramCollection.Add(new DBParameter("@sBank", _pPejabat.NamaBank == null ? "" : _pPejabat.NamaBank));
                    paramCollection.Add(new DBParameter("@KodeUK", _pPejabat.Unit));
                    paramCollection.Add(new DBParameter("@NamaDlmRekeningBank", _pPejabat.NamaDalamRekeningBank));
                    paramCollection.Add(new DBParameter("@TANGGAL", _pPejabat.TanggalAktiv,DbType.Date));
                    paramCollection.Add(new DBParameter("@pID", _newID));
                    
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
        public bool Simpan(Pejabat _pPejabat)
        {


            try
            {
                int _newID;

                if (_pPejabat.ID == 0)
                {

                    _newID = GetMaxID();
                    _newID++;


                    SSQL = "INSERT INTO " + m_sNamaTabel + " (ID,btJenis,IDDInas,sJabatan,sNama,sNIP,bActive,PPKD,sRek,sNPWP,sBank,btKodeUK,NamaDlmRekeningBank,TanggalAktiv) values " +
                        "(@pID,@pbtJenis,@pIDDInas,@psJabatan,@psNama,@psNIP,@pbActive,@pBPPD,@psRek,@psNPWP,@psBank,@KodeUK,@NamaDlmRekeningBank,@TanggalAktiv)";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pID", _newID));
                    paramCollection.Add(new DBParameter("@pbtJenis", _pPejabat.Jenis));
                    paramCollection.Add(new DBParameter("@pIDDInas", _pPejabat.IDDInas));
                    paramCollection.Add(new DBParameter("@psJabatan", _pPejabat.Jabatan));
                    paramCollection.Add(new DBParameter("@psNama", _pPejabat.Nama));
                    paramCollection.Add(new DBParameter("@psNIP", _pPejabat.NIP));
                    paramCollection.Add(new DBParameter("@pbActive", 1));
                    paramCollection.Add(new DBParameter("@pBPPD", _pPejabat.PPKD));
                    paramCollection.Add(new DBParameter("@psRek", _pPejabat.NoRekening == null ? "" : _pPejabat.NoRekening));
                    paramCollection.Add(new DBParameter("@psNPWP", _pPejabat.NPWP == null ? "" : _pPejabat.NPWP));
                    paramCollection.Add(new DBParameter("@psBank", _pPejabat.NamaBank == null ? "" : _pPejabat.NamaBank));
                    paramCollection.Add(new DBParameter("@KodeUK", _pPejabat.Unit));
                    paramCollection.Add(new DBParameter("@TanggalAktiv", _pPejabat.TanggalAktiv));
                    paramCollection.Add(new DBParameter("@NamaDlmRekeningBank", _pPejabat.NamaDalamRekeningBank));





                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    _pPejabat.ID = _newID;

                }
                else
                {
                    _newID = _pPejabat.ID;

                    SSQL = "UPDATE " + m_sNamaTabel + " SET btJenis=@pbtJenis ,sJabatan=@psJabatan,sNama=@psNama,sNIP=@psNIP,bActive=@pbActive, PPKD=@pPPKD,sRek=@psRek,sNPWP=@sNPWP,sBank=@sBank,NamaDlmRekeningBank=@NamaDlmRekeningBank WHERE ID=@pID";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pbtJenis", _pPejabat.Jenis));
                    paramCollection.Add(new DBParameter("@pIDDInas", _pPejabat.IDDInas));
                    paramCollection.Add(new DBParameter("@psJabatan", _pPejabat.Jabatan));
                    paramCollection.Add(new DBParameter("@psNama", _pPejabat.Nama));
                    paramCollection.Add(new DBParameter("@psNIP", _pPejabat.NIP));
                    paramCollection.Add(new DBParameter("@pbActive", _pPejabat.Active));
                    paramCollection.Add(new DBParameter("@pPPKD", _pPejabat.PPKD));
                    paramCollection.Add(new DBParameter("@psRek", _pPejabat.NoRekening == null ? "" : _pPejabat.NoRekening));
                    paramCollection.Add(new DBParameter("@sNPWP", _pPejabat.NPWP == null ? "" : _pPejabat.NPWP));
                    paramCollection.Add(new DBParameter("@sBank", _pPejabat.NamaBank == null ? "" : _pPejabat.NamaBank));
                    paramCollection.Add(new DBParameter("@KodeUK", _pPejabat.Unit));
                    paramCollection.Add(new DBParameter("@NamaDlmRekeningBank", _pPejabat.NamaDalamRekeningBank));
                    paramCollection.Add(new DBParameter("@pID", _newID));

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
        public bool Hapus(int _pIDUnit)
        {
            try
            {

                SSQL = "DELETE FROM " + m_sNamaTabel + "  WHERE ID=@pID";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDUnit));
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
        public bool Hapus(Pejabat _oPejabat )
        {
            try
            {

                SSQL = "DELETE FROM mPejabat  WHERE ID=@pID and SNama=@Nama";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _oPejabat.ID));
                paramCollection.Add(new DBParameter("@Nama", _oPejabat.Nama));

                if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
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


    }
}
