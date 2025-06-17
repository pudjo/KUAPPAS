using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DTO.Bendahara;
using BP;
using DataAccess;
using System.Data;
using Formatting;



namespace BP
{
    public class TProgramAPBDLogic:BP 
    {
        public TProgramAPBDLogic(int _pTahun, int pkepmen=2)
            : base(_pTahun,0,pkepmen)
        {
            Tahun = _pTahun;
            
            m_sNamaTabel = "tPrograms_A";

        }
        public List<TProgramAPBD> GetByDinasAndUrusan(int pITahun, int pDinas, int pIDUrusan){
        
            List<TProgramAPBD> _lst = new List<TProgramAPBD>();

            try
            {

                SSQL = "Select A.IDUrusan, A.IDDinas,A.IDProgram, A.btIDProgram,A.sNamaProgram FROM tPrograms_A A  " +
                         " where A.iTahun =@Tahun AND A.IDDInas= @Dinas ";
                        

               DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("@Tahun", pITahun));
                paramCollection.Add(new DBParameter("@Dinas", pDinas));

                if (pIDUrusan>0){
                     SSQL=SSQL + " and A.IDUrusan= @IDUrusan " ;
                     paramCollection.Add(new DBParameter("@IDUrusan", pIDUrusan)); 
                        
                }
                SSQL=SSQL + " ORDER BY A.IDUrusan, A.IDDinas,A.IDProgram";;
                        
                
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL,paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TProgramAPBD()
                                {
                                    TampilanKode= DataFormat.GetInteger(dr["IDProgram"]).ToKodeProgram(),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    Nama= DataFormat.GetString(dr["sNamaProgram"]),
                                    KodePendek = DataFormat.GetInteger(dr["IDProgram"]).ToString().Length > 3 ? DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(3) : "00",//.Length
                           
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
        public List<TProgramAPBD> GetByDinas(int pITahun, int pDinas)
        {

            List<TProgramAPBD> _lst = new List<TProgramAPBD>();

            try
            {

                SSQL = "Select A.IDUrusan, A.IDDinas,A.IDProgram, A.btIDProgram,A.sNamaProgram FROM tPrograms_A A  " +
                         " where A.iTahun =@Tahun ";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@Tahun", pITahun));

                if (pDinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDInas= @Dinas ";
                    paramCollection.Add(new DBParameter("@Dinas", pDinas));
                }
                
                SSQL = SSQL + " ORDER BY A.IDUrusan, A.IDDinas,A.IDProgram"; ;


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TProgramAPBD()
                                {
                                    IDDinas = DataFormat.GetInteger(dr["IDDinas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    Nama = DataFormat.GetString(dr["sNamaProgram"]),
                                    KodePendek = DataFormat.GetInteger(dr["IDProgram"]).ToString().Length > 3 ? DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(3) : "00",//.Length

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
        public List<TProgramAPBD> Get(int pITahun)
        {

            List<TProgramAPBD> _lst = new List<TProgramAPBD>();

            try
            {

                SSQL = "Select A.iTahun, A.IDUnit,A.btKodeUK,A.IDUrusan, A.IDDinas,A.IDProgram, A.btIDProgram,A.sNamaProgram FROM tPrograms_A A  " +
                         " where A.iTahun =@Tahun ";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@Tahun", pITahun));


                SSQL = SSQL + " ORDER BY A.IDUrusan, A.IDDinas,A.IDProgram"; ;


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TProgramAPBD()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    IDUnit = DataFormat.GetInteger(dr["IDUNIT"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDinas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    Nama = DataFormat.GetString(dr["sNamaProgram"]),
                                    KodePendek = DataFormat.GetInteger(dr["IDProgram"]).ToString().Length > 3 ? DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(3) : "00",//.Length

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

        public List<TProgramAPBD> GetBySPJ(int _pITahun, int _pDinas, DateTime tanggalAwal, DateTime tanggalAkhir, string sNoUrut, long noUrutSPJUP=0)
        {
            List<TProgramAPBD> _lst = new List<TProgramAPBD>();
            try
            {

                if (noUrutSPJUP == 0)
                {
                       SSQL ="SELECT IDDInas, idurusan,IDProgram,sNamaProgram, SUM(Jumlah) as Jumlah from ( ";
                       SSQL=SSQL +   "Select A.IDDInas,A.IDUrusan,A.IDProgram, A.sNamaProgram, SUM(C.cJumlah) as Jumlah FROM tPrograms_A A  INNER JOIN tPanjar B ON B.IDDINas= A.IDDInas and " +
                            " A.IDUrusan = B.IDurusan and A.IDProgram = B.IDPRogram INNER JOIN tPanjarRekening C on B.iNourut= C.inourut " +
                             " where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() +
                             " and B.dtBukukas between " + tanggalAwal.ToSQLFormat() + " AND " + tanggalAkhir.ToSQLFormat();
                       if (sNoUrut.Length > 0)
                           SSQL = SSQL + " AND B.inourut in ( " + sNoUrut + ")";

                       SSQL = SSQL + " AND B.inourut in ( " + sNoUrut + ")" +
                             " group by A.IDDInas, A.IDUrusan, A.IDProgram, A.btIDProgram,A.sNamaProgram " +
                             " UNION ";
                        SSQL = SSQL + " Select A.IDDInas,A.IDUrusan, A.IDProgram, A.sNamaProgram, " +
                             " SUM(-1 * C.iDebet1 * C.cJumlah1 )  as Jumlah FROM tPrograms_A A  INNER JOIN tKoreksi B "+
                             "  ON A.iTahun= b.iTahun and B.IDDINas= A.IDDInas INNER jOIN  tKoreksiDetail C on A.IDUrusan = C.IDurusan and A.IDProgram = C.IDPRogram " +
                             " AND B.iNourut= C.inourut where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() +
                            " and B.dtKoreksi between " + tanggalAwal.ToSQLFormat() + " AND " + tanggalAkhir.ToSQLFormat();
                        if (sNoUrut.Length > 0)
                            SSQL = SSQL + " AND B.inourut in ( " + sNoUrut + ")";
                        SSQL = SSQL + " AND B.inourut in ( " + sNoUrut + ")" +
                              " group by A.IDDInas,A.IDUrusan, A.IDProgram, A.sNamaProgram";
                    SSQL=SSQL + ") A Group by A.IDDInas, A.IDProgram order by A.IDDInas, A.IDProgram ";

                         
                            


                }
                else
                {


                       SSQL =" SELECT IDDInas, idurusan,IDProgram,sNamaProgram, SUM(Jumlah) as Jumlah from ( ";
                       SSQL = SSQL + "Select A.IDDInas,A.IDUrusan,A.IDProgram, A.sNamaProgram, SUM(C.cJumlah) as Jumlah FROM tPrograms_A A  INNER JOIN tPanjar B ON B.IDDINas= A.IDDInas and " +
                             " A.IDUrusan = B.IDurusan and A.IDProgram = B.IDPRogram INNER JOIN tPanjarRekening C on B.iNourut= C.inourut " +
                             " where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() +
                             " AND B.inourutspjup  =  " + noUrutSPJUP.ToString() +
                             " group by A.IDDInas, A.IDUrusan, A.IDProgram, A.btIDProgram,A.sNamaProgram " +
                              " UNION ";
                        SSQL = SSQL + " Select A.IDDInas,A.IDUrusan, A.IDProgram, A.sNamaProgram, " +
                                " SUM(-1 * C.iDebet1 * C.cJumlah1 )  as Jumlah FROM tPrograms_A A  INNER JOIN tKoreksi B "+
                                "  ON A.iTahun= b.iTahun and B.IDDINas= A.IDDInas INNER jOIN  tKoreksiDetail C on A.IDUrusan = C.IDurusan and A.IDProgram = C.IDPRogram " +
                                " AND B.iNourut= C.inourut where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() +
                                 " AND B.inourutspjup  =  " + noUrutSPJUP.ToString () +  
                                 " group by A.IDDInas,A.IDUrusan, A.IDProgram, A.sNamaProgram";
                    SSQL=SSQL + ") A Group by A.IDDInas, A.IDProgram order by A.IDDInas, A.IDProgram ";

                }

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TProgramAPBD()
                                {
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    Nama = DataFormat.GetString(dr["sNamaProgram"]),
                                    Realisasi = DataFormat.GetDecimal(dr["Jumlah"]),
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


        public TProgramAPBD GetByID(int pTahun, int pIDUrusan, int _pDinas, int _pIDProgram)
        {
            TProgramAPBD oPrg = new TProgramAPBD();

            try
            {
                
               SSQL = "Select * FROM tPrograms_A A " +
                         " where A.iTahun =" + pTahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() + "  and A.IDUrusan= " + pIDUrusan.ToString() + " AND A.IDProgram=" + _pIDProgram.ToString() ;//+


                      
         

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        
                        if (Tahun<=2020){
                        oPrg = new TProgramAPBD()
                                {
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    TampilanKode = DataFormat.GetInteger(dr["IDProgram"]).ToKodeProgram(),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    Nama = DataFormat.GetString(dr["sNamaProgram"]),
                                    KodePendek = DataFormat.GetInteger(dr["IDProgram"]).ToString().Length > 3 ? DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(3) : "00",//.Length
                                    Pagu = 0,//DataFormat.GetDecimal(dr["Jumlah"]),
                                    JumlahDiInput ="0",// DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
                                    Outcome = DataFormat.GetString(dr["Outcome"]),
                                    Keluaran = DataFormat.GetString(dr["Keluaran"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    Target = DataFormat.GetDecimal(dr["Target"]),
                                    SatuanTarget = DataFormat.GetString(dr["SatuanTarget"]),

                                };
                        } else {
                            oPrg = new TProgramAPBD()
                                {
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    TampilanKode = DataFormat.GetInteger(dr["IDProgram"]).ToKodeProgram(),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    Outcome = DataFormat.GetString(dr["Outcome"]),
                                    Keluaran = DataFormat.GetString(dr["Keluaran"]),
                                    Target = DataFormat.GetDecimal(dr["Target"]),
                                    Nama = DataFormat.GetString(dr["sNamaProgram"]),
                                    KodePendek = DataFormat.GetInteger(dr["IDProgram"]).ToString().Length > 3 ? DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(3) : "00",//.Length
                                    Pagu = 0,//DataFormat.GetDecimal(dr["Jumlah"]),
                                    JumlahDiInput ="0",// DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport(),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    SatuanTarget = DataFormat.GetString(dr["SatuanTarget"]),

                                };
                        }
                    }
                    else
                    {

                        SKPDLogic oLogic = new SKPDLogic(Tahun);
                        List<SKPD> lstSKPD = new List<SKPD>();
                        lstSKPD = oLogic.GetByParent(_pDinas);
                        foreach (SKPD s in lstSKPD)
                        {
                            int jml = 0;
                            jml = CekAda(Tahun, pIDUrusan, s.ID, _pIDProgram);
                            if (jml > 0)
                            {
                                
                                break;
                            }

                        }

                    }
                }
                return oPrg;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }
        }
        public int CekAda(int pTahun, int pIDUrusan, int _pDinas, int _pIDProgram, int IDUnit=0)
        {
            //TProgramAPBD oPrg = new TProgramAPBD();
            int jml = 0;
            try
            {
                // ID Urusan untuk queri -> IDUrusan
                // IDUrusan untuk Nama -> IDUrusanMaster

                SSQL = "Select A.IDUrusan, A.IDDinas,A.IDProgram, A.sNamaProgram FROM tPrograms_A A " +
                         " where A.iTahun = " + pTahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() +
                         " and A.IDUnit = " + IDUnit.ToString() + 
                         " and A.IDUrusan= " + pIDUrusan.ToString() + " AND IDProgram=" + _pIDProgram.ToString();



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        jml = dt.Rows.Count;
                     }
                    
                }
                return jml;
            }

            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return 0;
            }
        }
        public int CekAdaFromKUA(int pTahun, int pIDUrusanM, int _pDinas, int _pIDProgramM)
        {
            //TProgramAPBD oPrg = new TProgramAPBD();
            int jml = 0;
            try
            {
                SSQL = "Select A.IDUrusan, A.IDDinas,A.IDProgram, A.sNamaProgram FROM tPrograms_A A " +
                         " where A.iTahun = " + pTahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() +
                         " and A.IDUrusanM= " + pIDUrusanM.ToString() + " AND IDProgramM=" + _pIDProgramM.ToString();


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        jml = dt.Rows.Count;
                    }

                }
                return jml;
            }

            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return 0;
            }
        }
        public bool SimpanImport(List<TProgramAPBD> _lst, int pIDDInas, int _itahun)
        {
            try
            {
                SSQL = "UPDATE tPrograms_A SET sNama2='' WHERE iTahun = " + _itahun.ToString() + " AND IDDInas=" + pIDDInas.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                foreach (TProgramAPBD t in _lst)
                {


                    int j = CekAda((int)t.Tahun, t.IDUrusan,pIDDInas, t.IDProgram);

                    if (j == 0)
                    {
                        SSQL = "INSERT INTO tPrograms_A (iTahun,IDDInas,IDUrusan, IDProgram, sNamaProgram, sNama2) values (" +
                         " @piTahun,@pIDDInas,@pIDUrusan, @pIDProgram, @psNama, @psNama2)";
                        
                        DBParameterCollection paramCollection = new DBParameterCollection();

                         paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
                        paramCollection.Add(new DBParameter("@pIDDInas", t.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
                        paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
                        paramCollection.Add(new DBParameter("@psNama", t.Nama2 == null ? "" : t.Nama2));
                        paramCollection.Add(new DBParameter("@psNama2", t.Nama2 == null ? "" : t.Nama2));

                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                    }
                    else
                    {



                        SSQL = "UPDATE " + m_sNamaTabel + " SET sNama2=@psNama  WHERE IDDinas=@pIDDinas AND IDUrusan=@pIDUrusan " +
                             " AND IDProgram=@pIDProgram AND iTahun =@piTahun";

                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@psNama", t.Nama2));
                        paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
                        paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));                        
                        paramCollection.Add(new DBParameter("@piTahun", t.Tahun));                        
                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Simpan(List<TProgramAPBD> _lst)
        {
            try
            {
                foreach (TProgramAPBD t in _lst)
                {
                   // TProgramAPBD o = GetByID((int)t.Tahun, t.IDDinas, t.IDUrusan, t.IDProgram);
                    int ix = CekAda((int)t.Tahun,t.IDUrusan, t.IDDinas,t.IDProgram);
                    if (ix == 0)
                    {
                        SSQL = "INSERT INTO tPrograms_A (iTahun,IDDInas,IDUrusan, IDProgram, sNamaProgram,btJenis) values (" +
                         " @piTahun,@pIDDInas,@pIDUrusan, @pIDProgram, @psNama,3)";

                        DBParameterCollection paramCollection = new DBParameterCollection();

                        paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
                        paramCollection.Add(new DBParameter("@pIDDInas", t.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
                        paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
                        paramCollection.Add(new DBParameter("@psNama", t.Nama == null ? "" : t.Nama));
                      //  paramCollection.Add(new DBParameter("@psNama2", t.Nama2 == null ? "" : t.Nama2));

                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                    }
                    else
                    {

                        if (t.Nama.Length > 0)
                        {

                            SSQL = "UPDATE " + m_sNamaTabel + " SET sNamaProgram=@psNama WHERE IDDinas=@pIDDinas AND IDUrusan=@pIDUrusan " +
                                 " AND IDProgram=@pIDProgram AND iTahun =@piTahun";

                            DBParameterCollection paramCollection = new DBParameterCollection();
                            paramCollection.Add(new DBParameter("@psNama", t.Nama));

                            paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas));
                            paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
                            paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
                            paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
                            _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                        }


                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //public bool Simpan(TProgramAPBD t)
        //{
        //    try
        //    {
                
        //            // TProgramAPBD o = GetByID((int)t.Tahun, t.IDDinas, t.IDUrusan, t.IDProgram);
        //            int ix = CekAda((int)t.Tahun, t.IDUrusan, t.IDDinas, t.IDProgram);
        //            if (ix == 0)
        //            {
        //                SSQL = "INSERT INTO tPrograms_A (iTahun,IDDInas,IDUrusan, IDProgram, sNamaProgram,btJenis) values (" +
        //                 " @piTahun,@pIDDInas,@pIDUrusan, @pIDProgram, @psNama,3)";


        //                //SSQL = "INSERT INTO tKegiatan_A (iTahun, IDDinas,IDUrusan," +
        //                //           " IDProgram,IDkegiatan ,btKodekategori, btKodeURusan, btKodeSKPD, btKodeUK, btKodekategoriPelaksana, btKodeUrusanPelaksana,btIDprogram, btIDKegiatan,btJenis,sNama,cPlafon,cPlafonABT, IDUrusanM, IDProgramM, IDKegiatanM) Select tKUA.iTahun, tKUA.IDDInas, " +
        //                //           " tKUA.IDurusan, tKUA.IDProgram,tKUA.IDkegiatan, tKUA.IDDInAS/1000000 AS btKodekategori,(tKUA.IDDInAS/10000)%100 AS btKodeUrusan,(tKUA.IDDInAS/100)%100 AS btKodeSKPD,0 as btKodeUK, " +
        //                //           " tKUA.IDuRUSAN/100 AS btKodekategoriPelaksana,tKUA.IDuRUSAN %100 AS btKodeUrusanPelaksana,tKUA.IDProgram % 100 as btIDProgram, tKUA.IDKegiatan % 1000 as btIDKegiatan , 3 as btJenis, tKUA.Usulan as sNama,tKUA.JumlahMurni as cPlafon,tKUA.JumlahPerubahan as cPlafonABT, IDUrusanMaster, IDProgramMaster, IDKegiatanMaster " +
        //                //           " FROM tKUA  " +
        //                //           " where tKUA.iTahun= " + _pKUA.Tahun.ToString() + " AND tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pKUA.IDDinas.ToString() +
        //                //            " AND tKUA.IDKEgiatan = " + _pKUA.IDKegiatan.ToString();

        //                DBParameterCollection paramCollection = new DBParameterCollection();

        //                paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
        //                paramCollection.Add(new DBParameter("@pIDDInas", t.IDDinas));
        //                paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
        //                paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
        //                paramCollection.Add(new DBParameter("@psNama", t.Nama == null ? "" : t.Nama));
        //                //  paramCollection.Add(new DBParameter("@psNama2", t.Nama2 == null ? "" : t.Nama2));

        //                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

        //                SSQL = "UPDATE tPrograms_A SET btKodekategori= IDDInAS/1000000 , btKodeURusan= (IDDInAS/10000)%100,btKodeSKPD =  (IDDInAS/100)%100,0 as btKodeUK ," +
        //                    "btKodekategoriPelaksana =IDuRUSAN/100,btKodeUrusanPelaksana= IDuRUSAN %100 ,btIDProgram IDProgram % 100   WHERE iTahun = " + t.Tahun.ToString() +
        //                    " AND IDDInas = " + t.IDDinas.ToString() + " AND IDProgram =" + t.IDProgram.ToString();
        //                _dbHelper.ExecuteNonQuery(SSQL);

        //            }
        //            else
        //            {

        //                if (t.Nama.Length > 0)
        //                {

        //                    SSQL = "UPDATE " + m_sNamaTabel + " SET sNamaProgram=@psNama WHERE IDDinas=@pIDDinas AND IDUrusan=@pIDUrusan " +
        //                         " AND IDProgram=@pIDProgram AND iTahun =@piTahun";

        //                    DBParameterCollection paramCollection = new DBParameterCollection();
        //                    paramCollection.Add(new DBParameter("@psNama", t.Nama));

        //                    paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas));
        //                    paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
        //                    paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
        //                    paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
        //                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
        //                }


        //            }
                
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        public bool Simpan(TProgramAPBD t)
        {
            try
            {

                // TProgramAPBD o = GetByID((int)t.Tahun, t.IDDinas, t.IDUrusan, t.IDProgram);
                //int kodedinas = t.IDDinas
                int ix = CekAda((int)t.Tahun, t.IDUrusan, t.IDDinas, t.IDProgram, t.IDUnit);


                if (ix == 0)
                {
                
                    int _KodeProgram =DataFormat.GetInteger(t.IDProgram.ToString().Substring(3, 2));
                      
                     int    _KodeKategoriPelaksana = DataFormat.GetInteger(t.IDUrusan.ToString().Substring(0, 1));
                      int   _kodeUrusanPelaksana = DataFormat.GetInteger(t.IDUrusan.ToString().Substring(1, 2));



                      int _KodeKategori = DataFormat.GetInteger(t.IDDinas.ToString().Substring(0, 1));
                      int _KodeUrusan = DataFormat.GetInteger(t.IDDinas.ToString().Substring(1, 2));
                      int _KodeSKPD = DataFormat.GetInteger(t.IDDinas.ToString().Substring(3, 2));
                     // int _KodeProgram  =DataFormat.GetInteger(t.IDProgram.ToString().Substring(3, 2));


                      SSQL = "INSERT INTO tPrograms_A (iTahun,IDDInas,IDUrusan, IDProgram, btkodekategori,btkodeurusan, btkodeSKPD, btkodeUK, btkodekategoriPelaksana,btkodeurusanPelaksana,btIDProgram,sNamaProgram,btJenis,Outcome ,Keluaran ,RPJMD , PrioritasNAsional , Target, idunit ) values (" +
                     " @piTahun,@pIDDInas,@pIDUrusan, @pIDProgram, @pbtkodekategori,@Pbtkodeurusan, @pbtkodeSKPD, @pbtkodeUK, @pbtkodekategoriPelaksana,@pbtkodeurusanPelaksana,@pbtIDProgram, @psNama,3,@pOutcome ,@pKeluaran ,@pRPJMD , @pPrioritasNAsional , @pTarget,@pUnit)";

                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
                    paramCollection.Add(new DBParameter("@pIDDInas", t.IDDinas));
                    paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
                    paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
                    paramCollection.Add(new DBParameter("@pbtkodekategori",_KodeKategori));
                    paramCollection.Add(new DBParameter("@Pbtkodeurusan",_KodeUrusan)); 
                    paramCollection.Add(new DBParameter("@pbtkodeSKPD",_KodeSKPD));
                    paramCollection.Add(new DBParameter("@pbtkodeUK",t.KodeUK ));
                    paramCollection.Add(new DBParameter("@pbtkodekategoriPelaksana",_KodeKategoriPelaksana));
                    paramCollection.Add(new DBParameter("@pbtkodeurusanPelaksana", _kodeUrusanPelaksana));
                    paramCollection.Add(new DBParameter("@pbtIDProgram",_KodeProgram));

                        
                    paramCollection.Add(new DBParameter("@psNama", t.Nama == null ? "" : t.Nama));
                    paramCollection.Add(new DBParameter("@pOutcome", t.Outcome));
                    paramCollection.Add(new DBParameter("@pKeluaran", t.Keluaran));
                    paramCollection.Add(new DBParameter("@pRPJMD", t.RPJMD,DbType.Currency));
                    paramCollection.Add(new DBParameter("@pPrioritasNAsional", t.PrioritasNasional)); 
                    paramCollection.Add(new DBParameter("@pTarget", t.Target));
                    paramCollection.Add(new DBParameter("@pUnit", t.IDUnit));
                    


                    //  paramCollection.Add(new DBParameter("@psNama2", t.Nama2 == null ? "" : t.Nama2));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                     ////SSQL = "UPDATE tPrograms_A SET btKodekategori= IDDInAS/1000000 , btKodeURusan= (IDDInAS/10000)%100,btKodeSKPD =  (IDDInAS/100)%100,0 as btKodeUK ," +
                     ////       "btKodekategoriPelaksana =IDuRUSAN/100,btKodeUrusanPelaksana= IDuRUSAN %100 ,btIDProgram IDProgram % 100   WHERE iTahun = " + t.Tahun.ToString() +
                     ////       " AND IDDInas = " + t.IDDinas.ToString() + " AND IDProgram =" + t.IDProgram.ToString();
                     ////_dbHelper.ExecuteNonQuery(SSQL);

                }
                else
                {

                    if (t.Nama.Length > 0)
                    {

                        SSQL = "UPDATE " + m_sNamaTabel + " SET sNamaProgram=@psNama,Outcome=@pOutcome ,Keluaran =@pKeluaran,RPJMD =@pRPJMD, PrioritasNAsional=@pPrioritasNAsional , Target =@pTarget WHERE IDDinas=@pIDDinas AND IDUrusan=@pIDUrusan " +
                             " AND IDProgram=@pIDProgram AND iTahun =@piTahun and IDUnit =@pUnit";

                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@psNama", t.Nama));
                        paramCollection.Add(new DBParameter("@pOutcome", t.Outcome));
                        paramCollection.Add(new DBParameter("@pKeluaran", t.Keluaran));
                        paramCollection.Add(new DBParameter("@pRPJMD", t.RPJMD,DbType.Currency));
                        paramCollection.Add(new DBParameter("@pPrioritasNAsional", t.PrioritasNasional)); 
                        paramCollection.Add(new DBParameter("@pTarget", t.Target));
                        paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
                        
                        paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
                        paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
                        paramCollection.Add(new DBParameter("@pUnit", t.IDUnit));

                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    }


                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool SimpanOutcome(TProgramAPBD t)
        {
            try
            {

                int ix = CekAda((int)t.Tahun, t.IDUrusan, t.IDDinas, t.IDProgram);


                if (ix == 0)
                {

                }
                else
                {

                    if (t.Nama.Length > 0)
                    {

                        SSQL = "UPDATE " + m_sNamaTabel + " SET sNamaProgram=@psNama,Outcome=@pOutcome ,Keluaran =@pKeluaran,Target=@pTarget , SatuanTarget =@pSatuanTarget , RPJMD =@pRPJMD, PrioritasNAsional=@pPrioritasNAsional  WHERE IDDinas=@pIDDinas AND IDUrusan=@pIDUrusan " +
                             " AND IDProgram=@pIDProgram AND iTahun =@piTahun";

                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@psNama", t.Nama));
                        paramCollection.Add(new DBParameter("@pOutcome", t.Outcome));
                        paramCollection.Add(new DBParameter("@pKeluaran", t.Keluaran));
                        paramCollection.Add(new DBParameter("@pTarget", t.Target));
                        paramCollection.Add(new DBParameter("@pSatuanTarget", t.SatuanTarget));
                        paramCollection.Add(new DBParameter("@pRPJMD", t.RPJMD, DbType.Currency));
                        paramCollection.Add(new DBParameter("@pPrioritasNAsional", t.PrioritasNasional));
                        paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));

                        paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
                        paramCollection.Add(new DBParameter("@piTahun", t.Tahun));

                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    }


                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool SimpandariKUA(int pIDDInas)
        {

            try
            {
                List<KUA> _lst = new List<KUA>();
                SSQL = "Select tKUA.iTahun, tKUA.IDDInas,tKUA.IDUrusan, tKUA.IDProgram, tKUA.IDUrusanMaster, " +
                   "tKUA.IDProgramMaster, mProgram.sNamaProgram,SUM(JumlahMurni) as PaguMurni, SUm(JumlahPerubahan) as PaguPerubahan from tKUA " +
                   "INNER JOIN mProgram ON mProgram.ID= tKUA.IDProgramMaster WHERE IDDInas = " + pIDDInas.ToString() + " AND iTahun= " + Tahun.ToString() +
                   " GROUP BY tKUA.iTahun, tKUA.IDDInas,tKUA.IDUrusan, tKUA.IDProgram, " +
                   " tKUA.IDUrusanMaster, tKUA.IDProgramMaster, mProgram.sNamaProgram";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new KUA()
                                {
                                    /// ID = DataFormat.GetInteger(dr["ID"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDinas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    JumlahMurni = dr["PaguMurni"] == null ? 0 : DataFormat.GetDecimal(dr["PaguMurni"]),
                                    JumlahPerubahan = dr["PaguPerubahan"] == null ? 0 : DataFormat.GetDecimal(dr["PaguPerubahan"]),
                                    // Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),
                                    IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
                                    namaprogram = DataFormat.GetString(dr["sNamaProgram"])

                                }).ToList();
                    }
                }
                if (_lst != null)
                {
                    foreach (KUA oKUA in _lst)
                    {
                        TProgramAPBD oProgram = new TProgramAPBD();
                        oProgram.Tahun = oKUA.Tahun;
                        oProgram.IDDinas = oKUA.IDDinas;
                        oProgram.IDUrusan = oKUA.IDUrusan;
                        oProgram.IDProgram = oKUA.IDProgram;
                        //oProgram.IDIDUrusanMaster = oKUA.IDUrusanMaster;
                        // oProgram.IDProgramMaster = oKUA.IDProgramMaster;
                        oProgram.Nama = oKUA.namaprogram;
                        Simpan(oProgram);


                    }


                    return true;
                }
                else
                    return false;


            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }
        }

        public bool SimpanKUA(List<TProgramAPBD> _lst)
        {
            try
            {
                foreach (TProgramAPBD t in _lst)
                {
                    int ix = CekAda((int)t.Tahun, t.IDUrusan , t.IDDinas, t.IDProgram );
                    if (ix == 0)
                    {
                        if (Tahun < 2020)
                        {
                            SSQL = "INSERT INTO tPrograms_A (iTahun,IDDInas,IDUrusan, IDProgram,  sNamaProgram,btJenis, IDUrusanM, IDProgramM) " +
                             "SELECT " + t.Tahun.ToString() + " as iTahun, " + t.IDDinas.ToString() + " as IDDInas ," + t.IDUrusan.ToString() + " as IDUrusan , " + t.IDProgram.ToString() + " as IDProgram, sNamaProgram ,3 as btJenis, IDurusan as IDUrusanM, ID as IDProgramM from mProgram where id = " + t.KodeProgramM.ToString() + " AND IDUrusan = " + t.KodeUrusanM.ToString();



                            _dbHelper.ExecuteNonQuery(SSQL);
                        }
                        else
                        {
                            SSQL = "INSERT INTO tPrograms_A (iTahun,IDDInas,IDUrusan, IDProgram,  sNamaProgram,btJenis, IDUrusanM, IDProgramM) " +
                             "SELECT " + t.Tahun.ToString() + " as iTahun, " + t.IDDinas.ToString() + " as IDDInas ," + t.IDUrusan.ToString() + " as IDUrusan , " + t.IDProgram.ToString() + " as IDProgram, sNamaProgram ,3 as btJenis, IDurusan as IDUrusanM, ID as IDProgramM from mProgram where id = " + t.KodeProgramM.ToString() + " AND IDUrusan = " + t.KodeUrusanM.ToString();
                            _dbHelper.ExecuteNonQuery(SSQL);


                        }
                        SSQL = "UPDATE tPrograms_A SET btKodeKategori= IDDInAS/1000000 ,btKodeUrusan=(IDDInAS/10000)%100 ,btKodeSKPD=(IDDInAS/100)%100 ,btKodeUK=0 , " +
                            " btKodekategoriPelaksana= IDuRUSAN/100 ,btKodeUrusanPelaksana=IDuRUSAN %100 ,btIDProgram = IDProgram % 100  where iTahun = " + t.Tahun.ToString() + " AND IDDInas= " + t.IDDinas.ToString() + "  and IDProgram =" + t.IDProgram.ToString();


                        _dbHelper.ExecuteNonQuery(SSQL);


                    }
                    else
                    {
                        if (Tahun < 2020)
                        {

                            SSQL = "UPDATE tPrograms_A SET sNamaProgram=mProgram.sNamaProgram from tPrograms_A inner join mProgram ON tPrograms_A.IDProgramM= mProgram.ID and tPrograms_A.IDUrusanM = mProgram.IDUrusan  WHERE tPrograms_A.IDDinas=" + t.IDDinas.ToString() + " AND mProgram.id =" + t.KodeProgramM.ToString() + " AND mProgram.IDUrusan= " + t.KodeUrusanM.ToString() +
                                 " AND tPrograms_A.IDProgram=" + t.IDProgram.ToString() + " AND iTahun =" + Tahun.ToString();
                            _dbHelper.ExecuteNonQuery(SSQL);

                            SSQL = "UPDATE tPrograms_A SET btKodeKategori= IDDInAS/1000000 ,btKodeUrusan=(IDDInAS/10000)%100 ,btKodeSKPD=(IDDInAS/100)%100 ,btKodeUK=0 , " +
                                " btKodekategoriPelaksana= IDuRUSAN/100 ,btKodeUrusanPelaksana=IDuRUSAN %100 ,btIDProgram = IDProgram % 100  where iTahun = " + t.Tahun.ToString() + " AND IDDInas= " + t.IDDinas.ToString() + "  and IDProgram =" + t.IDProgram.ToString();

                            _dbHelper.ExecuteNonQuery(SSQL);
                        }
                        else
                        {
                            SSQL = "UPDATE tPrograms_A SET sNamaProgram='"  + t.Nama.ToString() + "' WHERE tPrograms_A.IDDinas=" + t.IDDinas.ToString() +
                                 "  AND tPrograms_A.IDUrusan=" + t.IDUrusan.ToString() + "  AND tPrograms_A.IDProgram=" + t.IDProgram.ToString() + " AND iTahun =" + Tahun.ToString();
                            _dbHelper.ExecuteNonQuery(SSQL);

                            SSQL = "UPDATE tPrograms_A SET btKodeKategori= IDDInAS/1000000 ,btKodeUrusan=(IDDInAS/10000)%100 ,btKodeSKPD=(IDDInAS/100)%100 ,btKodeUK=0 , " +
                                " btKodekategoriPelaksana= IDuRUSAN/100 ,btKodeUrusanPelaksana=IDuRUSAN %100 ,btIDProgram = IDProgram % 100  where iTahun = " + t.Tahun.ToString() + " AND IDDInas= " + t.IDDinas.ToString() + "  and IDProgram =" + t.IDProgram.ToString();

                            _dbHelper.ExecuteNonQuery(SSQL);
                        }

                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool SimpanImport(List<TProgramAPBD> _lst)
        {
            try
            {
                foreach (TProgramAPBD t in _lst)
                {
                    //TProgramAPBD o = GetByID((int)t.Tahun, t.IDDinas, t.IDUrusan, t.IDProgram);
                    //if (o == null)
                    //{
                        SSQL = "INSERT INTO tPrograms_A (iTahun,IDDInas,IDUrusan, IDProgram, sNamaProgram,btjenis, btKodekategoriPelaksana,btKodeUrusanPelaksana, btKodekategori,btKodeUrusan,btKodeSKPD, btKodeUK, btIDProgram) values (" +
                         " @piTahun,@pIDDInas,@pIDUrusan, @pIDProgram, @psNama,@pbtjenis, @pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana, @pbtKodekategori,@pbtKodeUrusan,@pbtKodeSKPD, @pbtKodeUK, @pbtIDProgram)";
                        DBParameterCollection paramCollection = new DBParameterCollection();

                        paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
                        paramCollection.Add(new DBParameter("@pIDDInas", t.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
                        paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
                        paramCollection.Add(new DBParameter("@psNama", t.Nama == null ? "" : t.Nama));
                        paramCollection.Add(new DBParameter("@pbtjenis", t.Jenis));                                                
                        paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", t.KodeKategoriPelaksana));
                        paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", t.KodeUrusanPelaksana)); 
                        paramCollection.Add(new DBParameter("@pbtKodekategori", t.KodeKategori));
                        paramCollection.Add(new DBParameter("@pbtKodeUrusan", t.KodeUrusan));
                        paramCollection.Add(new DBParameter("@pbtKodeSKPD", t.KodeSKPD));
                        paramCollection.Add(new DBParameter("@pbtKodeUK", t.KodeUK));
                        paramCollection.Add(new DBParameter("@pbtIDProgram", t.KodeProgram));



                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                    //}
                    //else
                    //{



                    //    SSQL = "UPDATE " + m_sNamaTabel + " SET sNama=@psNama,sNama2=@psNama2  WHERE IDDinas=@pIDDinas AND IDUrusan=@pIDUrusan " +
                    //         " AND IDProgram=@pIDProgram AND iTahun =@piTahun";

                    //    DBParameterCollection paramCollection = new DBParameterCollection();
                    //    paramCollection.Add(new DBParameter("@psNama", t.Nama));
                    //    paramCollection.Add(new DBParameter("@psNama2", t.Nama2));
                    //    paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas));
                    //    paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
                    //    paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
                    //    paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
                    //    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                    //}
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SimpanUrusanBaru(int idUrusan, int idDinas, int idProgram, int idUrusanBaru)
        {
            try
            {
                SSQL = "UPDATE tPrograms_A SET IDurusanBaru =" + idUrusanBaru.ToString() + " WHERE IDUrusan=" + idUrusan.ToString() + " AND IDDInas=" + idDinas.ToString() + " AND IDProgram=" + idProgram.ToString();
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
       

        public bool ImportFormKUA(int _pTahun)
        {
            //SSQL 
            return true;
        }
        public List<TProgramAPBD> GetByDinasAndUrusanAndSPD(int _pITahun, int _pDinas, int _pIDUrusan, long inoururtSPD)
        {
            List<TProgramAPBD> _lst = new List<TProgramAPBD>();

            try
            {
                // ID Urusan untuk queri -> IDUrusan
                // IDUrusan untuk Nama -> IDUrusanMaster
                /*
                SSQL = "Select A.IDUrusan, A.IDDinas,A.IDProgram, A.btIDProgram,A.sNamaProgram,SUM(B.cJumlahOlah) AS JumlahOlah, SUM(B.cPlafon)  FROM tPrograms_A A  LEFT OUTER JOIN  tANGGARANREKENING_A B" +
                        " ON A.iTahun = B.iTahun AND A.IDUrusan = B.idUrusan AND A.IDDInas= B.IDDInas AND A.IDPRogram = B.IDProgram " +
                         " where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() + "  and A.IDUrusan= " + _pIDUrusan.ToString() +
                        " GROUP BY A.IDUrusan, A.IDDinas,A.IDProgram, A.btIDProgram,A.sNamaProgram,A.IDUrusanBaru, mUrusanBaru.sNamaUrusan " +
                        " ORDER BY A.IDUrusan, A.IDDinas,A.IDProgram, A.sNamaProgram ";

                */
                SSQL = "Select A.IDUrusan, A.IDDinas,A.IDProgram, A.btIDProgram,A.sNamaProgram FROM tPrograms_A A  " +
                         " where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() + "  and A.IDUrusan= " + _pIDUrusan.ToString() +
                        " AND IDProgram in (SElECT IDProgram from tSPDKegiatan WHERE inourut <=" + inoururtSPD.ToString() + ") ORDER BY A.IDUrusan, A.IDDinas,A.IDProgram";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TProgramAPBD()
                                {
                                    TampilanKode = DataFormat.GetInteger(dr["IDProgram"]).ToKodeProgram(),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    Nama = DataFormat.GetString(dr["sNamaProgram"]),
                                    KodePendek = DataFormat.GetInteger(dr["IDProgram"]).ToString().Length > 3 ? DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(3) : "00",//.Length
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

        public List<TProgramAPBD> GetByDinasAndUrusanAndSP2D(int _pITahun, int _pDinas, int _pIDUrusan, long inoururtSP2D)
        {
            List<TProgramAPBD> _lst = new List<TProgramAPBD>();

            try
            {
                // ID Urusan untuk queri -> IDUrusan
                // IDUrusan untuk Nama -> IDUrusanMaster
                /*
                SSQL = "Select A.IDUrusan, A.IDDinas,A.IDProgram, A.btIDProgram,A.sNamaProgram,SUM(B.cJumlahOlah) AS JumlahOlah, SUM(B.cPlafon)  FROM tPrograms_A A  LEFT OUTER JOIN  tANGGARANREKENING_A B" +
                        " ON A.iTahun = B.iTahun AND A.IDUrusan = B.idUrusan AND A.IDDInas= B.IDDInas AND A.IDPRogram = B.IDProgram " +
                         " where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() + "  and A.IDUrusan= " + _pIDUrusan.ToString() +
                        " GROUP BY A.IDUrusan, A.IDDinas,A.IDProgram, A.btIDProgram,A.sNamaProgram,A.IDUrusanBaru, mUrusanBaru.sNamaUrusan " +
                        " ORDER BY A.IDUrusan, A.IDDinas,A.IDProgram, A.sNamaProgram ";

                */
                SSQL = "Select A.IDUrusan, A.IDDinas,A.IDProgram, A.btIDProgram,A.sNamaProgram FROM tPrograms_A A  " +
                         " where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() + "  and A.IDUrusan= " + _pIDUrusan.ToString() +
                        " AND IDProgram in (SElECT IDProgram from tSPPRekening WHERE inourut <=" + inoururtSP2D.ToString() + ") ORDER BY A.IDUrusan, A.IDDinas,A.IDProgram";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TProgramAPBD()
                                {
                                    TampilanKode = DataFormat.GetInteger(dr["IDProgram"]).ToKodeProgram(),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    Nama = DataFormat.GetString(dr["sNamaProgram"]),
                                    KodePendek = DataFormat.GetInteger(dr["IDProgram"]).ToString().Length > 3 ? DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(3) : "00",//.Length
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
        public List<TProgramAPBD> GetByDinasAndUrusanAndBAST(int _pITahun, int _pDinas, int _pIDUrusan, long inoururtBAST)
        {
            List<TProgramAPBD> _lst = new List<TProgramAPBD>();

            try
            {
                // ID Urusan untuk queri -> IDUrusan
                // IDUrusan untuk Nama -> IDUrusanMaster
                /*
                SSQL = "Select A.IDUrusan, A.IDDinas,A.IDProgram, A.btIDProgram,A.sNamaProgram,SUM(B.cJumlahOlah) AS JumlahOlah, SUM(B.cPlafon)  FROM tPrograms_A A  LEFT OUTER JOIN  tANGGARANREKENING_A B" +
                        " ON A.iTahun = B.iTahun AND A.IDUrusan = B.idUrusan AND A.IDDInas= B.IDDInas AND A.IDPRogram = B.IDProgram " +
                         " where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() + "  and A.IDUrusan= " + _pIDUrusan.ToString() +
                        " GROUP BY A.IDUrusan, A.IDDinas,A.IDProgram, A.btIDProgram,A.sNamaProgram,A.IDUrusanBaru, mUrusanBaru.sNamaUrusan " +
                        " ORDER BY A.IDUrusan, A.IDDinas,A.IDProgram, A.sNamaProgram ";

                */
                SSQL = "Select A.IDUrusan, A.IDDinas,A.IDProgram, A.btIDProgram,A.sNamaProgram FROM tPrograms_A A  " +
                         " where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() + "  and A.IDUrusan= " + _pIDUrusan.ToString() +
                        " AND IDProgram in (SElECT IDProgram from tBAST WHERE inourut =" + inoururtBAST.ToString() + ") ORDER BY A.IDUrusan, A.IDDinas,A.IDProgram";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TProgramAPBD()
                                {
                                    TampilanKode = DataFormat.GetInteger(dr["IDProgram"]).ToKodeProgram(),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    Nama = DataFormat.GetString(dr["sNamaProgram"]),
                                    KodePendek = DataFormat.GetInteger(dr["IDProgram"]).ToString().Length > 3 ? DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(3) : "00",//.Length
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
        public List<TProgramAPBD> GetByDinasAndUrusanAndSPJ(int _pITahun, int _pDinas, int _pIDUrusan, long inoururtSPJ)
        {
            List<TProgramAPBD> _lst = new List<TProgramAPBD>();

            try
            {
                // ID Urusan untuk queri -> IDUrusan
                // IDUrusan untuk Nama -> IDUrusanMaster
                /*
                SSQL = "Select A.IDUrusan, A.IDDinas,A.IDProgram, A.btIDProgram,A.sNamaProgram,SUM(B.cJumlahOlah) AS JumlahOlah, SUM(B.cPlafon)  FROM tPrograms_A A  LEFT OUTER JOIN  tANGGARANREKENING_A B" +
                        " ON A.iTahun = B.iTahun AND A.IDUrusan = B.idUrusan AND A.IDDInas= B.IDDInas AND A.IDPRogram = B.IDProgram " +
                         " where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() + "  and A.IDUrusan= " + _pIDUrusan.ToString() +
                        " GROUP BY A.IDUrusan, A.IDDinas,A.IDProgram, A.btIDProgram,A.sNamaProgram,A.IDUrusanBaru, mUrusanBaru.sNamaUrusan " +
                        " ORDER BY A.IDUrusan, A.IDDinas,A.IDProgram, A.sNamaProgram ";

                */
                SSQL = "Select A.IDUrusan, A.IDDinas,A.IDProgram, A.btIDProgram,A.sNamaProgram FROM tPrograms_A A  " +
                         " where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() + "  and A.IDUrusan= " + _pIDUrusan.ToString() +
                        " AND IDProgram in (SElECT IDProgram from tSPJRekening WHERE inourut <=" + inoururtSPJ.ToString() + ") ORDER BY A.IDUrusan, A.IDDinas,A.IDProgram";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TProgramAPBD()
                                {
                                    TampilanKode = DataFormat.GetInteger(dr["IDProgram"]).ToKodeProgram(),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    Nama = DataFormat.GetString(dr["sNamaProgram"]),
                                    KodePendek = DataFormat.GetInteger(dr["IDProgram"]).ToString().Length > 3 ? DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(3) : "00",//.Length
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


    }
}
