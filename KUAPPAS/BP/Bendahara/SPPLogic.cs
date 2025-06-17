using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using DTO;
using DTO.Bendahara;
using Formatting;
using BP;
using DataAccess;
using Newtonsoft.Json;
using DTO.SP2DOnLine;
using DTO.Akuntansi;
using System.Net.Http;
using System.Net.Http.Headers;
using BP.Akuntansi;
using System.Threading;

namespace BP.Bendahara
{
    public class SPPLogic:BP
    {
        IDbConnection m_connection;
        IDbTransaction m_objTrans ;

        
        public SPPLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tSPP";
        }
        public bool UpdateUK(long nourut, int uk, string nama, string jabatan, string nip)
        {
            try
            {
                SSQL = "UPDATE tSPP SET  btKodeuk= " + uk.ToString() + 
                     ",NamaPenandaTanganSPM= '"+ nama + "',JabatanPenandaTanganSPM='"+ jabatan +"'," +
                     "NIPPenandaTanganSPM='"+ nip +"' WHERE inourut = "+ nourut.ToString() +" AND iStatus not in (3,4) ";

                _dbHelper.ExecuteNonQuery(SSQL);
                return true;
                
            } catch (Exception ex)
            {
                _lastError = ex.Message;

                return false;
            }

        }
        public List<RegisterSP2DBPK> GetRegisterSP2DBPK(){

            List<RegisterSP2DBPK> lst = new List<RegisterSP2DBPK>();
            return lst;

        
// SSQL = " select convert(varchar(1), vwSPPRekening.btKodekategoriPelaksana) + '.' +  Replicate('0',2-len(convert(varchar(2), vwSPPRekening.btKodeUrusanPelaksana))) + convert(varchar(2), vwSPPRekening.btKodeUrusan) as Kd_Urusan,"
// SSQL = SSQL & "   mUrusan.sNamaUrusan as Nama_Urusan,convert(varchar(1), vwSPPRekening.btKodekategori) + '.' +  Replicate('0',2-len(convert(varchar(2),  vwSPPRekening.btKodeUrusan))) + convert(varchar(2), vwSPPRekening.btKodeUrusan) as kd_bidang,"
//SSQL = SSQL & " (select mUrusan.sNamaUrusan from mUrusan where mUrusan.btKodeKategori= vwSPPRekening.btKodekategori and mUrusan.btKodeUrusan = vwSPPRekening.btKodeUrusan) as nama_bidang,"
//SSQL = SSQL & " (select id from  mSKPD where  mSKPD.ID = vwSPPRekening.IDDInas) as kd_SKPD ,"
//SSQL = SSQL & " (select  mSKPD.sNamaSKPD  from  mSKPD where  mSKPD.ID = vwSPPRekening.IDDinas ) as nama_SKPD,"
//SSQL = SSQL & " substring(CAST ( cast (vwSPPRekening.IIDRekening as bigint) as char(12)),1,1) as kd_Rek_1,"
//SSQL = SSQL & " (select sNamaRekening from mRekening where IIDRekening = ((cast(vwSPPRekening.IIDRekening/100000000000 as bigint) * 100000000000)) and btRoot = 1) as Nama_Rek_1,"
//SSQL = SSQL & " substring(CAST (cast (vwSPPRekening.IIDRekening as bigint) as char(12)),2,1) as kd_Rek_2,"
//SSQL = SSQL & " (select sNamaRekening from mRekening where IIDRekening = (cast(vwSPPRekening.IIDRekening/10000000000 as bigint) *10000000000)) as Nama_Rek_2,"
//SSQL = SSQL & " substring(CAST (cast (vwSPPRekening.IIDRekening as bigint) as char(12)),3,2) as kd_Rek_3,"
//SSQL = SSQL & " (select sNamaRekening from mRekening where IIDRekening = (cast(vwSPPRekening.IIDRekening/100000000 as bigint) *100000000)) as Nama_Rek_3,"
//SSQL = SSQL & " substring(CAST (cast (vwSPPRekening.IIDRekening as bigint) as char(12)),5,2) as kd_Rek_4,"
//SSQL = SSQL & " (select sNamaRekening from mRekening where IIDRekening = (cast(vwSPPRekening.IIDRekening/1000000 as bigint) *1000000)) as Nama_Rek_4,"
//SSQL = SSQL & " substring(CAST (cast (vwSPPRekening.IIDRekening as bigint) as char(12)),7,2) as kd_Rek_5 ,"
//SSQL = SSQL & " (select sNamaRekening from mRekening where IIDRekening = (cast(vwSPPRekening.IIDRekening/10000 as bigint) *10000)) as Nama_Rek_5,"
//SSQL = SSQL & " substring(convert(varchar(12),vwSPPRekening.IIDRekening),9,4) as kd_Rek_6,"
//SSQL = SSQL & " (select sNamaRekening from mRekening where IIDRekening = vwSPPRekening.IIDRekening)  as Nama_Rek_6,"
//SSQL = SSQL & " Replicate('0',2-len(convert(varchar(2), vwSPPRekening.btIDProgram))) + convert(varchar(2), vwSPPRekening.btIDProgram) as kd_prog,"
//SSQL = SSQL & " (select top 1 tPrograms_A.sNamaProgram  from tPrograms_A where tPrograms_A.iTahun= vwSPPRekening.iTahun and tPrograms_A.IDDINAS= vwSPPRekening.IDDINAS and tPrograms_A.IDUrusan= vwSPPRekening.IDUrusan"
//SSQL = SSQL & "  and tPrograms_A.IDProgram = vwSPPRekening.IDProgram)  as nama_prog,convert(varchar(5), vwSPPRekening.IDsubKegiatan  % 100000 ) as kd_keg,"
//SSQL = SSQL & " vwKegiatan.sNama + ' | ' + vwSubKegiatan.nama as nama_Keg,vwSPPRekening.sNoBukti as No_SPM,"
//SSQL = SSQL & " vwSPPRekening.dtSPM as Tgl_SPM,vwSPPRekening.sNoSP2d as No_SP2D,vwSPPRekening.dtSP2d as Tgl_SP2D,vwSPPRekening.cJumlah as Nilai_SP2D,"
//SSQL = SSQL & " vwSPPRekening.sPeruntukan  as Uraian_sp2d,case when vwSPPRekening.btJenis= 0 then 'UP'when vwSPPRekening.btJenis= 1 then 'GU'WHEN vwSPPRekening.btJenis= 2 then 'TU'WHEN vwSPPRekening.btJenis= 3 then 'LS'WHEN vwSPPRekening.btJenis= 4 then 'GAJI dan Tunjangan' end as Jenis_SP2d,"
//SSQL = SSQL & " (select sNamaRekening from mRekening where IIDRekening = (cast(vwSPPRekening.IIDRekening/100000000 as bigint) *100000000)) as Jenis_belanja,(select sNamaRekening from mRekening where IIDRekening = (cast(vwSPPRekening.IIDRekening/1000000 as bigint) *1000000)) as Objek_Belanja,"
//SSQL = SSQL & "  case when btPenerima =0 then mskpd.sBankRekening else sNoRek  end as  NoRekPenerima ,"
//SSQL = SSQL & " case when btPenerima =0 then mskpd.sBendPengeluaran else sNamaPenerima end as namaPemilikRekeningPenerima ,"
//SSQL = SSQL & " case when btPenerima =0 then mskpd.sNamaSKPD  else sNamaPerusahaan end as NamaPerusahaan,"
//SSQL = SSQL & " vwSPPRekening.sNokontrak as No_Kontrak,"
//SSQL = SSQL & " tKontrak.dtKontrak as Tgl_kontrak,"
//SSQL = SSQL & " tKontrak.swaktuPelaksanaan  as  waktu_kontrak,'' as No_adendum,'' as tgl_adendum,'' as waktu_adendum,0 as nilai_adendum,"
//SSQL = SSQL & " tKontrak.sUraian as keperluan_kontrak,tKontrak.cJumlah as nilai_kontrak,0 as IWP1, 0 as IWP2, 0 as IWP3,vwPajakSPP.taperum as Taperum,vwPajakSPP.PPH21 as PPH21,"
//SSQL = SSQL & "  vwPajakSPP.Lain_Lain as LAIN2,0 as JKK,vwPajakSPP.JKM as JKM,vwPajakSPP.BPjs1 as BPJs1,0 as sewarumah,0 as LBHTUNJ,0 as IUJK,vwPajakSPP.BPJS4 as BPJS4,0 as IWP3,vwPajakSPP.PPN as PPN,0 as PPH21PJK,vwPajakSPP.PPH22 as PPH22,vwPajakSPP.PPH23 as PPH23,0 as PPHFInal"
//SSQL = SSQL & " from vwSPPRekening INNER JOIN mUrusan ON vwSPPRekening.btKodeKategoriPelaksana = mUrusan.btKodeKategori  and vwSPPRekening.btKodeUrusanPelaksana = mUrusan.btKodeUrusan INNER JOIN  mSKPD  ON vwSPPRekening.IDDInas =  mSKPD.ID inner  join  vwKegiatan on vwKegiatan.iTahun= vwSPPRekening.iTahun"
//SSQL = SSQL & " and  vwKegiatan.iddinas = vwSPPRekening.IDdinas and vwSPPRekening.IDKegiatan = vwKegiatan.IDKegiataninner join  vwSubKegiatan oN vwSubKegiatan.iTahun= vwSPPRekening.iTahun and vwSPPRekening.IDDinas   = vwSubKegiatan.IDDinas and vwSubKegiatan.IDSubKegiatan = vwSPPRekening.IDSubKegiatan"
//SSQL = SSQL & " left join vwPajakSPP on vwPajakSPP.inourut =vwSPPRekening.inourut left join tKontrak on tKontrak.inourut = vwSPPRekening.inourutkontrak Where vwSPPRekening.iTahun = 2023 and vwSPPRekening.dtSP2D<='9/15/2023' and vwSPPRekening.iStatus=4 order by vwSPPRekening.dtBukuKas ,inosp2d"


        }
        public List<SPPRekening> GetSPPRekening(List<long> lstNoUrut)
        {
            List<SPPRekening> _lst = new List<SPPRekening>();
            try
            {
               
                int id = 0;
                string sNamaParameter = "";
                DBParameterCollection paramCollection = new DBParameterCollection();

                SSQL = "  select tSPPRekening.* from tSPPRekening ";
                SSQL = SSQL + " where inourut in ( ";
                foreach (long nu in lstNoUrut)
                {
                        sNamaParameter="@NoUrut" +id.ToString() ;
                        SSQL = SSQL + sNamaParameter + ",";
                        paramCollection.Add(new DBParameter(sNamaParameter, nu,DbType.Int64));
                        id++;
                    }

                    sNamaParameter="@NoUrut" +id.ToString()  ;
                    SSQL = SSQL + sNamaParameter + ")";
                     paramCollection.Add(new DBParameter(sNamaParameter, 0,DbType.Int64));


                
                SSQL = SSQL + " ORDER BY tSPPRekening.inourut,tSPPRekening.IIDRekening";


     




                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SPPRekening()
                                {

                                    NoUrut = DataFormat.GetLong(dr["iNOurut"]),
                                    UnitKerja = DataFormat.GetInteger(dr["btKodeUK"]),

                                    KodeProgram = DataFormat.GetInteger(dr["btIDprogram"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIdkegiatan"]),
                                    KodeSubKegiatan = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                                    KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),


                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),

                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                
                           
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
        public bool VerifikasiSPM(long noUrut)
        {
            try
            {
         
        _isError = false;
        DBParameterCollection paramCollection = new DBParameterCollection();

        SSQL = "UPDATE tSPP SET  iStatus =6 ,dtVerifikasi =@DATE WHERE inourut =  @NoUrut";

         paramCollection.Add(new DBParameter("@NoUrut", noUrut,DbType.Int64));
         paramCollection.Add(new DBParameter("@DATE", DateTime.Now.Date, DbType.DateTime));
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
        public List<TemplateAlasan> GetDaftarPenolakanByNoUrut(long nourut)
        {
            try
            {
                List<TemplateAlasan> lstAlasan = new List<TemplateAlasan>();
                SSQL = "SELECT * FROM tSPPPenolakan where inourut=@NOURUT";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@NOURUT", nourut));
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lstAlasan = (from DataRow dr in dt.Rows
                                select new TemplateAlasan()
                                {
                                   Alasan = DataFormat.GetString(dr["sAlasan"])
                                    

                                }).ToList();
                    }
                }

                return lstAlasan;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return null;
            }
        }
        public bool TolakSPM(long noUrut, DateTime tanggal, List<string> lstAlasan, int jenis)
        {
            try
            {

                _isError = false;
               
                int i = 0;
                foreach (string alasan in lstAlasan)
                {
                    SSQL = "insert into tSPPPenolakan (inourut,btNoAlasan,sAlasan,TanggalPenolakan,Jenis,StatusPenolakan) values(" +
                        "@nourut,@NoAlasan,@Alasan,@TanggalPenolakan,@Jenis,@StatusPenolakan)";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@nourut", noUrut));
                    paramCollection.Add(new DBParameter("@NoAlasan", i++));
                    paramCollection.Add(new DBParameter("@Alasan", alasan));
                    paramCollection.Add(new DBParameter("@TanggalPenolakan", tanggal, DbType.Date));
                    paramCollection.Add(new DBParameter("@Jenis", jenis));
                    paramCollection.Add(new DBParameter("@StatusPenolakan", 0));
                    
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                }
                SSQL = "UPDATE tSPP SET  iStatus =10 ,dtVerifikasi =@DATE WHERE inourut =  @NoUrut";

                DBParameterCollection paramUpdate = new DBParameterCollection();
                paramUpdate.Add(new DBParameter("@DATE", tanggal, DbType.Date));
                paramUpdate.Add(new DBParameter("@nourut", noUrut));
                _dbHelper.ExecuteNonQuery(SSQL, paramUpdate);

                return true;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return false;
            }
        }
        public bool CatatBKU(SPP spp)
        {
            List<BKU> lstBKU = new List<BKU>();
            BKULogic oBKULogic = new BKULogic(Tahun);
            List<long> lstNoUrut = new List<long>();                      
            
            lstNoUrut.Add(spp.NoUrut);            
            lstBKU = oBKULogic.GetBKUByNoUrutSumber(lstNoUrut,2);           
            
            
            // mengambi no bku yang sudah ada... 
            BKU MaxNoBKU = new BKU();
            MaxNoBKU = oBKULogic.GetBKUDenganMaxNoBKU(spp.IDDInas, spp.KodeUK,2);
            // 
            if (MaxNoBKU == null)
            {
                MaxNoBKU.NoBKUSKPD = 1;
                MaxNoBKU.NoBKU = 1;
                MaxNoBKU.NoUrutSaja = 1;
                
            }


            // BKU 
            //if (BersihkanBKU(spp.NoUrut, m_connection, m_objTrans) == false)
            

            //}

            //BKUKASDA(spp, lstBKU, lstMaxNoBKU, m_connection, m_objTrans);

            switch (spp.Jenis)
            {
                case 0:
                case 1:
                case 2:
                    //BKU Hanya Penerimaan saja
                    //BKUUPGUTU(spp, lstBKU, MaxNoBKU, m_connection, m_objTrans);
                    BKUUPGUTU(spp, lstBKU, MaxNoBKU);

                    break;
                case 3:
                case 4:
                case 5:
                    List<SPPRekening> lstSPPRekening = new List<SPPRekening>();
                    List<PotonganSPP> lstPotonganSPP = new List<PotonganSPP>();
                    lstSPPRekening = GetSPPRekening(lstNoUrut);
                    lstPotonganSPP = GetPotongan(lstNoUrut);

                    List<SPPRekening> lSPPRekening = new List<SPPRekening>();
                    lSPPRekening = lstSPPRekening.FindAll(s => s.NoUrut == spp.NoUrut);

                    spp.Rekenings = new List<SPPRekening>();
                    spp.Rekenings = lSPPRekening;
                    spp.Potongans = lstPotonganSPP;

                    BKULS(spp, lstBKU, MaxNoBKU);//, m_connection, m_objTrans);

                    break;
            }


          



            return true;

        }
        public bool CatatBKUKasda(SPP spp)
        {
            List<BKU> lstBKU = new List<BKU>();
            BKULogic oBKULogic = new BKULogic(Tahun);
            List<long> lstNoUrut = new List<long>();

            lstNoUrut.Add(spp.NoUrut);
            lstBKU = oBKULogic.GetBKUByNoUrutSumber(lstNoUrut,0);



            // mengambi no bku yang sudah ada... 
            BKU MaxNoBKU = new BKU();
            MaxNoBKU = oBKULogic.GetBKUDenganMaxNoBKU(spp.IDDInas, spp.KodeUK, 0);
            // 
            if (MaxNoBKU == null)
            {
                MaxNoBKU.NoBKUSKPD = 1;
                MaxNoBKU.NoBKU = 1;
                MaxNoBKU.NoUrutSaja = 1;

            }
          


            // BKU 
            //if (BersihkanBKU(spp.NoUrut, m_connection, m_objTrans) == false)


            //}

            //BKUKASDA(spp, lstBKU, lstMaxNoBKU, m_connection, m_objTrans);
            BKUKASDA(spp, lstBKU, MaxNoBKU);
            






            return true;

        }
        public List<string> SetTanggalCair(List<SPP> lstSPPToCair )

        {

            try
            {
                
                List<string> lst = new List<string>();
                foreach (SPP spp in lstSPPToCair)
                {
                   
                  SSQL = "UPDATE tSPP Set InoUrutKasda =@noUrutKasda, dtBukukas =@tanggalCair, iStatus =4 where inourut =@NOURUT ";
                  DBParameterCollection paramCollection = new DBParameterCollection();
                  paramCollection.Add(new DBParameter("@noUrutKasda", spp.NoUrutKasda));
                  paramCollection.Add(new DBParameter("@tanggalCair", spp.dtCair,DbType.Date ));
                  paramCollection.Add(new DBParameter("@NOURUT", spp.NoUrut));
                  _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                  CatatBKUKasda(spp);

                  if (CatatSilakan(spp.NoUrut))
                  {
                      lst.Add(spp.NoSP2D);
                  }
                    // Jurnal ..


                  long NoUrut = spp.NoUrut;
                  int jenis = spp.Jenis;
                  ProsesJurnalLogic JurnalLogic = new ProsesJurnalLogic(Tahun, spp.IDDInas);

                  JurnalLogic.Jurnal(NoUrut.ToString(), JENIS_TABLE.TABLE_SPP);
                  // Create a thread and call a background method
                  //JurnalData jd = new JurnalData();
                  //jd.Nourut = NoUrut.ToString();
                  //jd.Table = JENIS_TABLE.TABLE_SPP;
                  //jd.iJenisSPP = jenis;
                  // jd.bPotongan=0;
                  //  jd.bppkd = 0;
                  //  jd.bTHL = false;
                  //  jd.fromSKPD = 0;



                  ////  Thread backgroundThread = new Thread(new ThreadStart(JurnalLogic.JurnalInThread));
                  ////// Start thread
                  ////backgroundThread.Start(jd);

                }

                return lst;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }

        }
        private bool CatatSilakan(long noUrut)
        {
            try
            {




                string Cadena = "https://api-silakan.seibutomasua.xyz/api/sp2d/update_status/" + noUrut.ToString();
                SilakanVerifikasi oSilakan = new SilakanVerifikasi();

                oSilakan.status = "accept";
                oSilakan.catatan_verifikator = "-";


                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(oSilakan);
                    var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+
                    HttpContent jsonContent = new StringContent(JsonConvert.SerializeObject(oSilakan), System.Text.Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "3|ajVZEgNjgFuSDy1c3edVPhFLtoILOEAf8A1DgmoY");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    try
                    {
                        var result = client.PostAsync(Cadena, stringContent).Result;
                        if (result.IsSuccessStatusCode == true)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (AggregateException err)
                    {
                        foreach (var errInner in err.InnerExceptions)
                        {
                            Console.WriteLine(errInner); //this will call ToString() on the inner execption and get you message, stacktrace and you could perhaps drill down further into the inner exception of it if necessary 
                        }
                        return false;
                    }

                }

            }
            catch (Exception ex)
            {
                return false;
            }

        }
        private bool BersihkanBKU(long NoUrut, IDbConnection connection, IDbTransaction odbTrans)
        {
            try
            {
                SSQL = "DELETE tBKU WHERE inourutSUmber =@noUrut AND (JENISSUMBER = 1 or JENISSUMBER = 10) ";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@noUrut", NoUrut));
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);
 
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }
        public bool CekApakahSPJUPSudahDipakai(long noUrutSPJ)
        {
            try
            {
                SSQL = "SELECT * from tSPP where cast(sNoSPJUP as bigint)=@NOURUTSPJ";
 
             
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@NOURUTSPJ", noUrutSPJ, DbType.Int64));
                

                DataTable dt = new DataTable();

                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                return true;


            }
            catch (Exception ex)
            {
                return false;

            }
        }
        public bool Hapus(long NoUrut)
        {
             m_connection = _dbHelper.CreateCOnnection();
             m_objTrans = m_connection.BeginTransaction();
            
        try
            {

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@noUrut", NoUrut));
            
                SSQL = "UPDATE TSPJ set iStatus = 0 from  tSPJ inner join tSPP on tSPJ.inourut = tSPP.sNoSPJUP  where tSPP.inourut=@noUrut";
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);




                SSQL = "DELETE tSPPRekening WHERE inourut =@noUrut ";
                
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);

                SSQL = "DELETE tSPP WHERE inourut =@noUrut ";
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);
                
                SSQL = "DELETE tSPPPotongan WHERE inourut =@noUrut ";
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);


                SSQL = "DELETE tJurnalRekening from tJurnal INNER JOIN tJurnalRekening  on tJurnalRekening.inojurnal = tJurnal.inojurnal " +
                     " WHERE iSumber =@NoUrut";
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);


                SSQL = "DELETE tBukubesar  WHERE tBukuBesar.iSumber =@NoUrut";
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);

                SSQL = "DELETE tJurnal  WHERE iSumber =@NoUrut";
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);
            



                m_objTrans.Commit();

                return true;
            }
            catch (Exception ex)
            {

                m_objTrans.Rollback();

                return false;
            }

        }
        private bool BKUUPGUTU(SPP spp, List<BKU> lstBKU, 
                               BKU bkuLama) 
//                               IDbConnection connection, IDbTransaction odbTrans)
        {
            try
            { // BKU 
                BKU oBKU = new BKU();
                
                oBKU.CreateFormSPP(spp, 1,(int)E_JENISBENDAHARA.BENDAHARA_PENGELUARAN);
                // Cari bku masuk
                BKU oldBKU = GetOldBKU(spp, lstBKU,  
                             E_JENISBENDAHARA.BENDAHARA_PENGELUARAN,
                             E_JENIS_REFERENSIBKU.REFERENSI_SP2D,1);
                
                oBKU.JenisBendahara = E_JENISBENDAHARA.BENDAHARA_PENGELUARAN;
                // Jika ada ditemukan
                if (oldBKU.NoUrut != 0)
                {
                    // pakai no urut lama, no bku lama,
                    oBKU.NoUrut = oldBKU.NoUrut;
                    oBKU.NoBKU = oldBKU.NoBKU;
                    oBKU.NoBKUSKPD = oldBKU.NoBKUSKPD;
                    
                }
                else
                {
                    oBKU.NoUrut = 0;
                    oBKU.NoBKU = bkuLama.NoBKU+1;
                    oBKU.NoBKUSKPD = bkuLama.NoBKUSKPD+1;
                    oBKU.NoUrutSaja = bkuLama.NoUrutSaja+5;
               }
                oBKU.Kodebank = 1;
                BKULogic oBKULogic = new BKULogic(Tahun);
                oBKULogic.Simpan(ref oBKU);

                return true;
            }
            catch (Exception ex)
            {
              
                return false;
            }

        }
        private bool BKUKASDA(SPP spp, List<BKU> lstBKU,
                              BKU bkuLama)
        //                               IDbConnection connection, IDbTransaction odbTrans)
        {
            try
            { // BKU 
                BKU oBKU = new BKU();

                oBKU.CreateFormSPP(spp, -1, (int)E_JENISBENDAHARA.BENDAHARA_BUD);
                // Cari bku masuk
                BKU oldBKU = GetOldBKU(spp, lstBKU,
                             E_JENISBENDAHARA.BENDAHARA_BUD,
                             E_JENIS_REFERENSIBKU.REFERENSI_SP2D,-1 );

                oBKU.JenisBendahara = E_JENISBENDAHARA.BENDAHARA_BUD;
                oBKU.LevelTampilan = E_LEVLETAMPILANBKU.eBKUHeader;
                // Jika ada ditemukan
                if (oldBKU == null )
                {
                    // pakai no urut lama, no bku lama,
                    oBKU.NoUrut = oldBKU.NoUrut;
                    oBKU.NoBKU = oldBKU.NoBKU;
                    oBKU.NoBKUSKPD = oldBKU.NoBKUSKPD;

                }
                else
                {
                    oBKU.NoUrut = 0;
                    oBKU.NoBKU = bkuLama.NoBKU + 1;
                    oBKU.NoBKUSKPD = bkuLama.NoBKUSKPD + 1;
                    oBKU.NoUrutSaja = bkuLama.NoUrutSaja+1;
                }
                oBKU.Kodebank = 1;
                BKULogic oBKULogic = new BKULogic(Tahun);
                oBKULogic.Simpan(ref oBKU);

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }
        private bool BKUKASDA(SPP spp, List<BKU> lstBKU,
                               List<BKU> lstMaxNoBKU,
                               IDbConnection connection, IDbTransaction odbTrans)
        {
            try
            { // BKU 
                BKU oBKU = new BKU();
                //oBKU.CreateFormSPP(spp, -1, 0);

                //BKU oldBKU = GetOldBKU(spp, lstBKU, lstMaxNoBKU,
                //             E_JENISBENDAHARA.BENDAHARA_BUD,
                //             E_JENIS_REFERENSIBKU.REFERENSI_SP2D, -1);

                //oBKU.JenisBendahara = E_JENISBENDAHARA.BENDAHARA_BUD;
                //if (oldBKU.NoUrut != 0)
                //{
                //    oBKU.NoUrut = oldBKU.NoUrut;

                //    oBKU.NoBKU = spp.NoUrutKasda;
                //    oBKU.NoBKUSKPD = spp.NoUrutKasda;
                //}
                //else
                //{
                //    oBKU.NoUrut = oldBKU.NoUrut;
                //    oBKU.NoBKU = spp.NoUrutKasda;
                //    oBKU.NoBKUSKPD = spp.NoUrutKasda;
                //    UpdateMxBKU(lstMaxNoBKU, oBKU);
                //}

                //BKULogic oBKULogic = new BKULogic(Tahun);
                //oBKULogic.Simpan(ref oBKU, connection, odbTrans, true);

                return true;
            }
            catch (Exception ex)
            {

                return false;

            }

        }

        private BKU GetOldBKU(SPP spp, List<BKU> lstBKU,
                               E_JENISBENDAHARA JenisBendahara, E_JENIS_REFERENSIBKU   JenisSumber, int debet)
        {

            BKU oldBKU = new BKU();
          
            oldBKU = lstBKU.FirstOrDefault(b => b.NourutSumber == spp.NoUrut && 
                                                        b.JenisBendahara == JenisBendahara &&
                                                        b.JenisSumber == (int) JenisSumber && 
                                                        b.Debet == debet );
            

            if (oldBKU == null)
            {
                oldBKU = new BKU();
               oldBKU.NoUrut = 0;

            }

            return oldBKU;

        }
        private bool UpdateMxBKU(List<BKU> lstMaxNoBKU,
                               BKU bku)
        {
            try
            {
                int index = lstMaxNoBKU.FindIndex(b => b.IDDinas == bku.IDDinas && b.KodeUk == bku.KodeUk);
                if (index >= 0)
                {
                    lstMaxNoBKU[index] = bku;
                }
                else
                {
                    lstMaxNoBKU.Add(bku);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private bool BKULS(SPP spp, List<BKU> lstBKU,  BKU bkuLama
                           )
        {
            try
            { // BKU 
                BKU oBKUMasuk = new BKU();

                oBKUMasuk.CreateFormSPP(spp, 1, (int)E_JENISBENDAHARA.BENDAHARA_PENGELUARAN);

                BKU oldBKU = GetOldBKU(spp, lstBKU,
                             E_JENISBENDAHARA.BENDAHARA_PENGELUARAN,
                             E_JENIS_REFERENSIBKU.REFERENSI_SP2D, 1);


                oBKUMasuk.JenisBendahara = E_JENISBENDAHARA.BENDAHARA_PENGELUARAN;
                bool bkubaru ;
                if (oldBKU.NoUrut != 0)
                {
                    oBKUMasuk.NoUrut = oldBKU.NoUrut;
                    oBKUMasuk.NoBKU = oldBKU.NoBKU;
                    oBKUMasuk.NoBKUSKPD = oldBKU.NoBKUSKPD;
                    bkubaru=false;

                }
                else
                {
                    bkuLama.NoBKU ++;
                    bkuLama.NoBKUSKPD++;
                    bkuLama.NoUrutSaja++;

                    oBKUMasuk.NoUrut = 0;
                    oBKUMasuk.NoBKU = bkuLama.NoBKU ;
                    oBKUMasuk.NoBKUSKPD = bkuLama.NoBKUSKPD ;
                    oBKUMasuk.NoUrutSaja = bkuLama.NoUrutSaja;

                    bkubaru= true;

                   
                }
                BKULogic oBKULogic = new BKULogic(Tahun);
                oBKULogic.Simpan(ref oBKUMasuk);
                
                //if (bkubaru== true){
                //    bkuLama.NoBKU ++;
                //    bkuLama.NoBKUSKPD++;
                //    bkuLama.NoUrutSaja++;
                //}
                BKU oBKUKeluar = new BKU();

                oBKUKeluar.CreateFormSPP(spp, -1,(int)E_JENISBENDAHARA.BENDAHARA_PENGELUARAN);
                
                oBKUKeluar.JenisBendahara = E_JENISBENDAHARA.BENDAHARA_PENGELUARAN;
                BKU oldBKU2 = GetOldBKU(spp, lstBKU, 
                             E_JENISBENDAHARA.BENDAHARA_PENGELUARAN,
                             E_JENIS_REFERENSIBKU.REFERENSI_SP2D, -1);


       
                if (oldBKU2.NoUrut != 0)
                {
                    oBKUKeluar.NoUrut = oldBKU2.NoUrut;

                    oBKUKeluar.NoBKU = oldBKU2.NoBKU;
                    oBKUKeluar.NoBKUSKPD = oldBKU2.NoBKUSKPD;
                }
                else
                {
                    bkuLama.NoBKU ++;
                    bkuLama.NoBKUSKPD++;
                    bkuLama.NoUrutSaja++;

                    oBKUKeluar.NoUrut = 0;
                    oBKUKeluar.NoBKU = bkuLama.NoBKU ;
                    oBKUKeluar.NoBKUSKPD = bkuLama.NoBKUSKPD ;
                    oBKUKeluar.NoUrutSaja = bkuLama.NoUrutSaja;
                    
                }
                oBKULogic.Simpan(ref oBKUKeluar);
                if (spp.Potongans.Count > 0)
                {
                    BKUPotonganLS(spp, lstBKU, bkuLama);
                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }
        private bool BKUPotonganLS(SPP spp, List<BKU> lstBKU, BKU lstMaxNoBKU
                           )
        {
            try
            { // BKU 
                BKU oBKUMasuk = new BKU();

                oBKUMasuk.CreateFormPotonganSPP(spp, 1, 2);
                BKU oldBKU = GetOldBKU(spp, lstBKU,
                             E_JENISBENDAHARA.BENDAHARA_PENGELUARAN,
                             E_JENIS_REFERENSIBKU.REFERENSI_POTONGANSP2D, 1);

                oBKUMasuk.JenisBendahara = E_JENISBENDAHARA.BENDAHARA_PENGELUARAN;
                if (oldBKU.NoUrut != 0)
                {
                    oBKUMasuk.NoUrut = oldBKU.NoUrut;
                    oBKUMasuk.NoBKU = oldBKU.NoBKU;
                    oBKUMasuk.NoBKUSKPD = oldBKU.NoBKUSKPD;


                }
                else
                {
                    lstMaxNoBKU.NoBKU++;
                    lstMaxNoBKU.NoBKUSKPD ++;
                    lstMaxNoBKU.NoUrutSaja++;
                    oBKUMasuk.NoUrut = 0;
                    oBKUMasuk.NoBKU = lstMaxNoBKU.NoBKU;
                    oBKUMasuk.NoBKUSKPD = lstMaxNoBKU.NoBKUSKPD;
                    oBKUMasuk.NoUrutSaja = lstMaxNoBKU.NoUrutSaja;


                }

                BKULogic oBKULogic = new BKULogic(Tahun);

                oBKULogic.Simpan(ref oBKUMasuk );
                BKU oBKUKeluar = new BKU();

                oBKUKeluar.CreateFormPotonganSPP(spp, -1, 2);

                oBKUKeluar.JenisBendahara = E_JENISBENDAHARA.BENDAHARA_PENGELUARAN;
                BKU oldBKU2 = GetOldBKU(spp, lstBKU,
                             E_JENISBENDAHARA.BENDAHARA_PENGELUARAN,
                             E_JENIS_REFERENSIBKU.REFERENSI_POTONGANSP2D, -1);


                oBKUKeluar.JenisBendahara = E_JENISBENDAHARA.BENDAHARA_PENGELUARAN;
                if (oldBKU2.NoUrut != 0)
                {
                    oBKUKeluar.NoUrut = oldBKU2.NoUrut;

                    oBKUKeluar.NoBKU = oldBKU2.NoBKU;
                    oBKUKeluar.NoBKUSKPD = oldBKU2.NoBKUSKPD;
                }
                else
                {
                    oBKUKeluar.NoUrut = 0;
                    lstMaxNoBKU.NoBKU++;
                    lstMaxNoBKU.NoBKUSKPD++;
                    lstMaxNoBKU.NoUrutSaja++;

                    oBKUKeluar.NoBKU = lstMaxNoBKU.NoBKU ;
                    oBKUKeluar.NoBKUSKPD = lstMaxNoBKU.NoBKUSKPD;
                    oBKUKeluar.NoUrutSaja = lstMaxNoBKU.NoUrutSaja;

                }
                oBKULogic.Simpan(ref oBKUKeluar);




                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }
        public bool SimpanSPM(SPP oSPP)
        {
            try
            {
                SSQL = "UPDATE tSPP SET  istatus= 1, sNoBukti=@NOSPM, dtSPM=@TANGGALSPM, "+
                     "NamaPenandaTanganSPM=@NAMAPENADATANGAN,JabatanPenandaTanganSPM=@JABATANPENANDATANGAN,"+
                     "NIPPenandaTanganSPM=@NIPPENANDATANGAN WHERE inourut = @NOURUT AND iStatus not in (3,4) ";

               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@NOSPM", oSPP.NoSPM));
               paramCollection.Add(new DBParameter("@TANGGALSPM", oSPP.dtSPM ,DbType.Date));
               paramCollection.Add(new DBParameter("@NAMAPENADATANGAN", oSPP.NamaPenandaTanganSPM));
               paramCollection.Add(new DBParameter("@JABATANPENANDATANGAN", oSPP.JabatanPenandaTanganSPM));
               paramCollection.Add(new DBParameter("@NIPPENANDATANGAN", oSPP.NIPPenandaTanganSPM));
               paramCollection.Add(new DBParameter("@NOURUT", oSPP.NoUrut));
               _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
               return true;
                


            }
            catch (Exception ex)
            {

                _lastError = ex.Message;
                return false ;
            }

        }
        public bool BatalSPM(SPP oSPP)
        {
            try
            {
                SSQL = "UPDATE tSPP SET  istatus= 0, " +
                     "NamaPenandaTanganSPM='',JabatanPenandaTanganSPM=''," +
                     "NIPPenandaTanganSPM='' WHERE inourut = @NOURUT AND iStatus =1";

                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("@NOURUT", oSPP.NoUrut));
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                return true;



            }
            catch (Exception ex)
            {

                _lastError = ex.Message;
                return false;
            }

        }
        public bool CekNoSP2D(long NoUrut ,int iNoSP2D, string sNomorSP2DSaja, string sNoSP2d)
        {
             try
            {
                if (sNoSP2d.Contains("GJ") == true)
                     SSQL = "SELECT * from tSPP where ( sNoSP2d like '%" + sNomorSP2DSaja + "%') AND btJenis= 4 and inourut <> @NOURUT AND iStatus <> 9 AND iStatus >2 ";
                 else
                     SSQL = "SELECT * from tSPP where ( sNoSP2d like '%" + sNomorSP2DSaja + "%') AND btJenis<>  4  and inourut <> @NOURUT AND iStatus <> 9 AND iStatus >2 ";

                 DBParameterCollection paramCollection = new DBParameterCollection();
                 paramCollection.Add(new DBParameter("@NOBARU", iNoSP2D, DbType.Int32));
                 paramCollection.Add(new DBParameter("@NOURUT", NoUrut, DbType.Int64));

                 DataTable dt = new DataTable();

                 dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true ;
                    }
                }
                return true ;

                 
            } catch(Exception ex){
                return false;

            }

        }
        //public bool CekNegatif(long NoUrut, int iNoSP2D)
        //{
        //    try
        //    {
        //        SSQL= "select  btKodeUK, IDsubkegiatan , iidrekening, sum(debet * cJumlah) from Realisasi04ak \r\n\t\t\twhere iddinas = 5020200 and idsubkegiatan = 502012020001 group by btKodeUK, IDsubkegiatan , iidrekening"
        //        if (sNoSP2d.Contains("GJ") == true)
        //            SSQL = "SELECT * from tSPP where ( sNoSP2d like '%" + sNomorSP2DSaja + "%') AND btJenis= 4 and inourut <> @NOURUT AND iStatus <> 9 AND iStatus >2 ";
        //        else
        //            SSQL = "SELECT * from tSPP where ( sNoSP2d like '%" + sNomorSP2DSaja + "%') AND btJenis<>  4  and inourut <> @NOURUT AND iStatus <> 9 AND iStatus >2 ";

        //        DBParameterCollection paramCollection = new DBParameterCollection();
        //        paramCollection.Add(new DBParameter("@NOBARU", iNoSP2D, DbType.Int32));
        //        paramCollection.Add(new DBParameter("@NOURUT", NoUrut, DbType.Int64));

        //        DataTable dt = new DataTable();

        //        dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                return false;
        //            }
        //            else
        //            {
        //                return true;
        //            }
        //        }
        //        return true;


        //    }
        //    catch (Exception ex)
        //    {
        //        return false;

        //    }

        //}
        public bool BatalkanSP2D(long NoUrut)
        {
            try
            {
                SPP oSPP = new SPP();
                oSPP = GetByID(NoUrut);
                if (oSPP == null)
                {
                    _lastError = "SPP Tidak ada..";
                    return false;


                }
                if (oSPP.NoUrutKasda > 0)
                {
                    _lastError = "Sudah di verifikasi Kasda";
                    return false;

                }
                  SSQL = "UPDATE tSPP SET  sNoSP2d='',dtSP2D=dtSPM, dtTerbitSP2D=dtSPM,iStatus = 0,inosp2d=0" +
                      ", iBankBUD =0,idPenandaTanganBUD=0,sNoRekBUD='',sNamaBank=''  WHERE inourut = @NOURUT";
                
               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@NOURUT", NoUrut, DbType.Int64));
           

                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                return true;


            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;

            }

        }
        public bool UpdatePPTK(long nourut, int idPPTK)
        {
            try
            {

                SSQL = "UPDATE tSPP SET  idpptk= @idPPTK WHERE inourut = @NOURUT";


                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@idPPTK", idPPTK));
                paramCollection.Add(new DBParameter("@NOURUT", nourut));


                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                return true;



            }
            catch (Exception ex)
            {

                _lastError = ex.Message;
                return false;
            }

        }
        public bool SimpanSP2D(SPP oSPP)
        {
            try
            {
                
                SSQL = "UPDATE tSPP SET  sNoSP2d=@NOSP2D,dtSP2D=@TANGGALSP2D, dtTerbitSP2D=@TANGGALSP2D,iStatus = 3,inosp2d=@INOSP2D" +
                ", iBankBUD =@BANKBUD,idPenandaTanganBUD=@PENANDATANGAN,sNoRekBUD=@NOREKENINGBUD,inourutRef = dbo.GetMaxNoUrutRef()+3,JenisTransfer=@JenisTransfer WHERE inourut = @NOURUT";


                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@NOSP2D",oSPP.NoSP2D));
                paramCollection.Add(new DBParameter("@TANGGALSP2D", oSPP.dtTerbit, DbType.Date));
                
                paramCollection.Add(new DBParameter("@INOSP2D",oSPP.iNOSP2D));
                paramCollection.Add(new DBParameter("@BANKBUD",oSPP.BankBUD));
                paramCollection.Add(new DBParameter("@PENANDATANGAN",oSPP.PenandatanganBUD));
                paramCollection.Add(new DBParameter("@NOREKENINGBUD",oSPP.NoRekBUD));
                //paramCollection.Add(new DBParameter("@NAMABANKBUD",oSPP.NamaBankBUD));
                paramCollection.Add(new DBParameter("@JenisTransfer",oSPP.JenisTransfer));
                paramCollection.Add(new DBParameter("@NOURUT", oSPP.NoUrut));


                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                return true;



            }
            catch (Exception ex)
            {

                _lastError = ex.Message;
                return false;
            }

        }
        public List<SPP> Get(ParameterBendahara oParemeter )
        {
            List<SPP> _lst = new List<SPP>();
            try{
                
                SSQL = "SELECT *, (SELECT sum(cJumlah) as Jumlah from tSPPRekening WHERE inourut = tSPP.inourut) as Jumlah,"+
                    "(SELECT sum(cJumlah) as Jumlah from tspppotongan WHERE inourut = tSPP.inourut) as JumlahPotongan from tSPP " +
                "  where IDDInas =@DINAS " ;

               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@DINAS", oParemeter.IDDInas));

               if (oParemeter.Status > -1)
               {
                   SSQL = SSQL + " AND tSPP.iStatus =@STATUS";
                   paramCollection.Add(new DBParameter("@STATUS", oParemeter.Status));

               }
              
               if (oParemeter.KodeUK > 0)
               {

                   SSQL=SSQL + " AND btKodeUK =@KODEUK  ";
                   paramCollection.Add(new DBParameter("@KODEUK", oParemeter.KodeUK));

               }
        


                if (oParemeter.Jenis> -1)
                {
                    SSQL = SSQL + " AND tSPP.btJenis= @JENIS ";
                    paramCollection.Add(new DBParameter("@JENIS", oParemeter.Jenis));

                }

                if (oParemeter.NoSP2D != null)
                {
                    if (oParemeter.NoSP2D.Trim().Length > 0)
                    {
                        SSQL = SSQL + " and sNoSP2D like '%" + oParemeter.NoSP2D + "%'";

                    }
                }

                SSQL=SSQL + " ORDER BY tSPP.inourut, tSPP.btJenis,tSPP.dtSPP,tSPP.sNoSPP,tSPP.dtSPM,tSPP.sNoBukti,tSPP.dtBukukas";//,A.dtTerbitSP2D,A.sNoSP2D, A.sPeruntukan,C.sNamaUK,A.iStatus , isnull(iStatusBKU,0), mJenisGaji.sNama 
          

              DataTable dt = new DataTable();
              dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SPP()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    NoUrut = DataFormat.GetLong(dr["iNOurut"]),
                                    NoSPP = DataFormat.GetString(dr["sNoSPP"]),
                                    NoSPM = DataFormat.GetString(dr["sNoBukti"]),
                                    NoSP2D = DataFormat.GetString(dr["sNoSP2d"]),
                                    dtSPP = DataFormat.GetDateTime(dr["dtSPP"]),
                                    dtSPM = DataFormat.GetDateTime(dr["dtSPM"]),
                                    dtCair = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    dtTerbit = DataFormat.GetDateTime(dr["dtTerbitSP2D"]),
                                    Peruntukan = DataFormat.GetString(dr["sPeruntukan"]),
                                    Status = DataFormat.GetInteger(dr["iStatus"]),
                                    Kodekategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    Keterangan = DataFormat.GetString(dr["sKeteranganPekerjaan"]),
                                    PPKD = DataFormat.GetInteger(dr["bPPKD"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDprogram"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIdkegiatan"]),
                                    KodeSubKegiatan = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                                    kodeKategoripelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    Kodeurusanpelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    JenisKegiatan = DataFormat.GetInteger(dr["btJenisKegiatan"]),
                                

                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    NoSPJUP = DataFormat.GetString(dr["sNoSPJUP"]),
                                    dtSPJUP = DataFormat.GetDateTime(dr["dtSPJUP"]),
                                    NoUrutSPD = DataFormat.GetLong(dr["iNoUrutSPD"]),
                 

                                    NoRek = DataFormat.GetString(dr["sNoRek"]),
                                    KodeBank =  DataFormat.GetString(dr["KodeBank"]),
                                    NamaBank = DataFormat.GetString(dr["sNamaBank"]),
                                    Penerima = DataFormat.GetInteger(dr["btPenerima"]),
                                    NamaPenerima = DataFormat.GetString (dr["sNamaPenerima"]),                           

                                    NamaPerusahaan = DataFormat.GetString(dr["sNamaPerusahaan"]),
                                    NoKontrak = DataFormat.GetString(dr["sNoKontrak"]),
                                    SSPSetor = DataFormat.GetInteger(dr["bSSPSetor"]),
                                    WaktuPelaksanaan = DataFormat.GetString(dr["swaktuPelaksanaan"]),
                                    dtSSP = DataFormat.GetDateTime(dr["dtSSP"]),
                                    NoSSP = DataFormat.GetString(dr["sNoSSP"]),
                                    dtKontrak = DataFormat.GetDateTime(dr["dtKontrak"]),//------
                                    Bulan = DataFormat.GetString(dr["btBulan"]),
                                    NoBAST = DataFormat.GetString(dr["inobast"]),
                                    NoSPPAT = DataFormat.GetString(dr["sNoSPPAT"]),
                                    JenisDocSumber = DataFormat.GetInteger(dr["iJenisDocSumber"]),
                                    SUmberDana = DataFormat.GetInteger(dr["iSumberDana"]),
                                    SubSumberDana = DataFormat.GetInteger(dr["iSUbSumberDana"]),
                                    SifatPajak = DataFormat.GetInteger(dr["iSifatPAjak"]),
                                    BankBUD = DataFormat.GetInteger(dr["iBankBUD"]),
                                    PenandatanganSP2d = DataFormat.GetInteger(dr["idPenandaTanganBUD"]),
                    
                                    PenandatanganBUD = DataFormat.GetInteger(dr["idPenandaTanganBUD"]),
                                    iNOSPP = DataFormat.GetInteger(dr["INoSPP"]),
                                    iNOSPM = DataFormat.GetInteger(dr["INoSPM"]),
                                    iNOSP2D = DataFormat.GetInteger(dr["INoSP2D"]),
                                    Bendahara = DataFormat.GetInteger(dr["IDBEndahara"]),//--
                                    JenisGaji = DataFormat.GetInteger(dr["iJenisGaji"]),
                                    NoUrutKasda = DataFormat.GetInteger(dr["inourutkasda"]),
                                    INoUrutKontrak = DataFormat.GetLong(dr["inoUrutkontrak"]),
                                    SifatKegiatan = DataFormat.GetInteger(dr["btsifatkegiatan"]),
                                    IDPPTK = DataFormat.GetString(dr["IDPPTK"]),
                                    NIPPPTK = DataFormat.GetString(dr["sNIPPPTK"]),
                                    NamaPPTK = DataFormat.GetString(dr["sNamaPPTK"]),
                                    JabatanPPTK = DataFormat.GetString(dr["sJabatanPPTK"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    NoNPWP = DataFormat.GetString(dr["sNPWP"]),                                  
                                    JumlahPotongan = DataFormat.GetDecimal(dr["JumlahPotongan"]),
                                    KeteranganNamaBank = DataFormat.GetString(dr["KeteranganNamaBank"]),
                                 //   Rekenings = GetDetail(DataFormat.GetLong(dr["iNOurut"]))

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

        public bool SPMdiTerima(long noUrut, int ino, int jenis, DateTime d )
        {
            try{
                DBParameterCollection paramCollection = new DBParameterCollection();
                if (jenis == 1)
                {
                    SSQL = "update tSPP SET iSTatus= 2,   dtTerima = @Date , iNoRegSPM =@NO where Inourut =@Nourut ";
                    paramCollection.Add(new DBParameter("@Date", d,DbType.DateTime));
                }
                else
                {
                    SSQL = "update tSPP SET inourutkasda =@NO where Inourut =@Nourut ";

                }
                paramCollection.Add(new DBParameter("@Nourut", noUrut));
                paramCollection.Add(new DBParameter("@NO", ino));

                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                return true;
            }
            catch (Exception ex)
            {


                _lastError = ex.Message;
                return false;

            }


        }
        public int GetMaxNoRegSPM()
        {

            try{
                 SSQL = "SELECT max(iNoRegSPM) as maxNum from tSPP where itahun = " + Tahun.ToString();
                 DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr =dt.Rows[0];
                        return DataFormat.GetInteger(dr["maxNum"]);

                        
                    }
                    else
                    { return 0;
                        
                    }
                } else 
                return 0;

                 
            } catch(Exception ex){
                return 0;

            }


        }
        public int GetMaxNoUrutKasda()
        {

            try
            {
                SSQL = "SELECT max(inourutkasda) as maxNum from tSPP where itahun = " + Tahun.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        return DataFormat.GetInteger(dr["maxNum"]);


                    }
                    else
                    {
                        return 0;

                    }
                }
                else
                    return 0;


            }
            catch (Exception ex)
            {
                return 0;

            }


        }
        public bool ProcessSP2DOnlneResult(long noUrut, DataTransaksiSP2DOnline510ResponseEx data)
        {
            try
            {
                SSQL = "";
                DBParameterCollection paramCollection = new DBParameterCollection();
                if (data.error_kode == "00")
                {
                    SSQL = "update tSPP SET iSTatus= 4 ,dtbukukas = @tanggalcair,CaraCair=1 where Inourut =@Nourut ";
                    paramCollection.Add(new DBParameter("@tanggalcair", data.tanggalTransaksi.ToDate(), DbType.Date));
                    paramCollection.Add(new DBParameter("@Nourut", noUrut));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    foreach (DetailPotonganMpnResponse pot in data.detailPotonganMpn)
                    {
                        SSQL = "UPDATE  tSPPPotongan  SET NTPN = @NTPN where Inourut =@Nourut  AND KodeBilling =@idbilling";
                        DBParameterCollection param = new DBParameterCollection();
                        param.Add(new DBParameter("@NTPN", pot.ntpn));
                        param.Add(new DBParameter("@Nourut", noUrut));
                        param.Add(new DBParameter("@idbilling", pot.idBilling));


                        _dbHelper.ExecuteNonQuery(SSQL, param);


                    }


                }
                else
                {
                    SSQL = "update tSPP SET iSTatus= 7 where Inourut =@Nourut ";
                    paramCollection.Add(new DBParameter("@Nourut", noUrut));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                }

                
               
               



                return true;
            }
            catch (Exception ex)
            {

                _lastError = ex.Message;
                return false;

            }
        }
        public List<SP2DOnLineLog> GetLog(long noUrut)
        {
            try
            {
                List<SP2DOnLineLog> lst = new List<SP2DOnLineLog>();
                SSQL = "SELECT * from SP2dOnlineLog where inourut = " + noUrut.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                                select new SP2DOnLineLog()
                                {
                                    NoUrut = DataFormat.GetLong(dr["iNOurut"]),
                                    Waktu =DataFormat.GetDateTime(dr["waktu"]),
                                    pesan = DataFormat.GetString(dr["pesan"]),
                                    responseKode = DataFormat.GetString(dr["resposekode"]),

                                }).ToList();
                    }
                }
                return lst;


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool LogSP2DOnline (SP2DOnLineLog log)
        {
            try
            {
                SSQL = "";
                DBParameterCollection paramCollection = new DBParameterCollection();
               
                   SSQL = "Insert into SP2dOnlineLog (inourut, waktu,otp, resposekode, pesan ) values (" +
                          "@nourut,   @waktu,@otp, @resposekode, @pesan)";

                    paramCollection.Add(new DBParameter("@nourut",log.NoUrut)); 
                    paramCollection.Add(new DBParameter("@waktu",log.Waktu));
                    paramCollection.Add(new DBParameter("@otp",log.otp));
                    paramCollection.Add(new DBParameter("@resposekode",log.responseKode));
                    paramCollection.Add(new DBParameter("@pesan", log.pesan));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                 
                 return true;
            }
            catch (Exception ex)
            {

                _lastError = ex.Message;
                return false;

            }
        }
        public List<SPP> GetSP2DOnline(ParameterBendahara oParemeter)
        {
            List<SPP> _lst = new List<SPP>();
            try
            {
                _isError = false;
                SSQL = "SELECT iNOurut,sNoSPP,sNoBukti,sNoSP2d,dtTerbitSP2D,sPeruntukan,iStatus,btJenis,sNoRek,KodeBank,sNamaBank,btPenerima,sNamaPenerima,mSKPD.sNamaSKPD as Namadinas," +
                     " tSPP.sNPWP,IDDInas,tSPP.NamaDlmRekeningBank, (SELECT sum(cJumlah) as Jumlah from tSPPRekening WHERE inourut = tSPP.inourut) as Jumlah," +
                     " (SELECT sum(cJumlah) as Jumlah from tspppotongan WHERE inourut = tSPP.inourut  and bInformasi =0 ) as JumlahPotonganMPN, "+
                     " (SELECT sum(cJumlah) as Jumlah from tspppotongan WHERE inourut = tSPP.inourut  and bInformasi =1 ) as JumlahPotonganNonMPN,inourutRef, " +
                     " dbo.GetStatusSP2DOnlineTerakhir(tSPP.inourut) as StatusOnline,JenisTransfer " +
                     " from tSPP INNER JOIN mSKPD on mSKPD.ID= tSPP.IDDInas  " +
                     "  where 1>0  ";

                SSQL = SSQL + " and tSPP.iStatus= @Status ";


               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@Status", oParemeter.Status));


                if (oParemeter.Jenis > -1)
                {
                    SSQL = SSQL + " AND tSPP.btJenis= @Jenis ";
                    paramCollection.Add(new DBParameter("@Jenis", oParemeter.Jenis));

                }


                if (oParemeter.Status == 3 || oParemeter.Status == 7 )
                {
                    SSQL = SSQL + " and dtsp2d between  @TanggalAwal and @TanggalAkhir ";
                    paramCollection.Add(new DBParameter("@TanggalAwal", oParemeter.TanggalAwal, DbType.Date));
                    paramCollection.Add(new DBParameter("@TanggalAkhir", oParemeter.TanggalAkhir, DbType.Date));
                }
                else
                {
                    SSQL = SSQL + " and dtbukukas between  @TanggalAwal and @TanggalAkhir ";
                    paramCollection.Add(new DBParameter("@TanggalAwal", oParemeter.TanggalAwal, DbType.Date));
                    paramCollection.Add(new DBParameter("@TanggalAkhir", oParemeter.TanggalAkhir, DbType.Date));

                }

                if (oParemeter.IDDInas > 0)
                {
                    SSQL = SSQL + " and IDDINAS = " + oParemeter.IDDInas.ToString() + "";
                }

                if (oParemeter.NoSP2D.Trim().Length > 0)
                {
                    SSQL = SSQL + " and sNoSP2D like '%"+ oParemeter.NoSP2D.Trim()+ "%'";
                   // paramCollection.Add(new DBParameter("@NomorSP2D", oParemeter.NoSP2D.Trim()));

                }

                SSQL = SSQL + " ORDER BY tSPP.inourut, tSPP.btJenis,tSPP.dtSPP,tSPP.sNoSPP,tSPP.dtSPM,tSPP.sNoBukti,tSPP.dtBukukas";//,A.dtTerbitSP2D,A.sNoSP2D, A.sPeruntukan,C.sNamaUK,A.iStatus , isnull(iStatusBKU,0), mJenisGaji.sNama 


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SPP()
                                {
                                    NoUrut = DataFormat.GetLong(dr["iNOurut"]),
                         
                                    NoSPM = DataFormat.GetString(dr["sNoBukti"]),
                                    NoSP2D = DataFormat.GetString(dr["sNoSP2d"]),
                                    dtTerbit = DataFormat.GetDateTime(dr["dtTerbitSP2D"]),
                                    Peruntukan = DataFormat.GetString(dr["sPeruntukan"]),
                                    Status = DataFormat.GetInteger(dr["iStatus"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    NamaDinas = DataFormat.GetString(dr["Namadinas"]),
                                    NoReferensiBankOnline = DataFormat.GetInteger(dr["inourutRef"]),
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"]),                                   
                                    NoRek = DataFormat.GetString(dr["sNoRek"]),
                                    KodeBank = DataFormat.GetString(dr["KodeBank"]),
                                    NamaBank = DataFormat.GetString(dr["sNamaBank"]),
                                    Penerima = DataFormat.GetInteger(dr["btPenerima"]),
                                    NamaPenerima = DataFormat.GetString(dr["NamaDlmRekeningBank"]),
                                    NoNPWP = DataFormat.GetString(dr["sNPWP"]),

                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    JumlahBayar = DataFormat.GetDecimal(dr["Jumlah"])- (DataFormat.GetDecimal(dr["JumlahPotonganMPN"]) + DataFormat.GetDecimal(dr["JumlahPotonganNonMPN"])),
                                    JumlahPotongan = DataFormat.GetDecimal(dr["JumlahPotonganMPN"]) + DataFormat.GetDecimal(dr["JumlahPotonganNonMPN"]),

                                    JumlahPotonganMPN = DataFormat.GetDecimal(dr["JumlahPotonganMPN"]),
                                    JumlahPotonganNonMPN = DataFormat.GetDecimal(dr["JumlahPotonganNonMPN"]),

                                    Potongans = GetPotongan(DataFormat.GetLong(dr["iNOurut"])),
                                    JenisTransfer = DataFormat.GetString(dr["JenisTransfer"]),
                                   statusOnline= DataFormat.GetString(dr["StatusOnline"]),

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
        private List<PotonganSPP> GetPotongan(long noUrutSPP=0)
        {
            List<PotonganSPP> lst = new List<PotonganSPP>();
            PotonganSPPLogic oLogic = new PotonganSPPLogic(Tahun );

            List<long> lstNoUrut = new List<long>();
            lstNoUrut.Add(noUrutSPP);
            lst = oLogic.Get(lstNoUrut);
            return lst;

        }

        private List<PotonganSPP> GetPotongan(List<long>lstNoUrut )
        {
            List<PotonganSPP> lst = new List<PotonganSPP>();
            PotonganSPPLogic oLogic = new PotonganSPPLogic(Tahun);
            if (lstNoUrut.Count > 2000)
            {
                lstNoUrut= new List<long>();
                lst = oLogic.Get(lstNoUrut);
            }
            else
            {
                lst = oLogic.Get(lstNoUrut);
            }
            
            return lst;

        }

        public List<SPP> GetSPP(ParameterBendahara oParemeter)
        {
            List<SPP> _lst = new List<SPP>();
            List<long> lstNoUrut = new List<long>();
            try
            {

//#if DEBUG

                SSQL = "SELECT tSPP.*, 1 as diBKU , mSumberDana.sNama as NamaSumberDana  from tSPP INNER join mSKPD  ON tSPP.IDDInas = mSKPD.ID " +
                " left  join mperusahaan on mperusahaan.idperusahaan = tspp.btpenerima  left  join mSumberDana on mSumberDana.ID= tspp.iSUmberDana " +
                " where 1> 0  ";
//#else
                          SSQL = "SELECT tSPP.*, dbo.IsInBKU(tSPP.inourut,2) as diBKU,mSumberDana.sNama as NamaSumberDana   from tSPP INNER join mSKPD  ON tSPP.IDDInas = mSKPD.ID " +
                " left  join mperusahaan on mperusahaan.idperusahaan = tspp.btpenerima left  join mSumberDana on mSumberDana.ID= tspp.iSUmberDana  where 1> 0  ";
//#endif
                DBParameterCollection paramCollection = new DBParameterCollection();

                if (oParemeter.LstStatus.Count > 0)
                {
                    int id = 0;
                    string sNamaParameter = "";

                    SSQL = SSQL + " AND  tSPP.iStatus in ( ";

                    foreach (int status in oParemeter.LstStatus)
                    {
                      
                            sNamaParameter = "@status" + id.ToString();
                            SSQL = SSQL + sNamaParameter + ",";

                            paramCollection.Add(new DBParameter(sNamaParameter, status, DbType.Int32));
                            id++;
                    
                   

                    }
                   
                    sNamaParameter = "@status" + id.ToString();
                    SSQL = SSQL + sNamaParameter + ")";
                    paramCollection.Add(new DBParameter(sNamaParameter, 99, DbType.Int32));

                    SSQL = SSQL + "  AND  tSPP.iStatus  <> 99";

                }


                if (oParemeter.Jenis >= 0)
                {
                    SSQL = SSQL + " AND btJenis= @Jenis";
                    paramCollection.Add(new DBParameter("@Jenis", oParemeter.Jenis));
                }

                if (oParemeter.IDDInas > 0)
                {
                    SSQL = SSQL + " AND tSPP.IDDInas= @IDDInas";
                    paramCollection.Add(new DBParameter("@IDDInas", oParemeter.IDDInas));
                    if (oParemeter.KodeUK > 0)
                    {
                        SSQL = SSQL + " AND tSPP.btKodeuk= @KodeUK";
                        paramCollection.Add(new DBParameter("@KodeUK", oParemeter.KodeUK));

                    }
                }

                if (oParemeter.NoSP2D.Length > 0)
                {

                    SSQL = SSQL + " AND sNoSP2D like  '%" + oParemeter.NoSP2D + "%'";
                    // paramCollection.Add(new DBParameter("@NoSP2D", oParemeter.NoSP2D));
                }
                if (oParemeter.NoSPP.Length > 0)
                {

                    SSQL = SSQL + " AND sNoSPP like  '%" + oParemeter.NoSPP + "%'";
              //      paramCollection.Add(new DBParameter("@NoSPP", oParemeter.NoSPP));
                }

                if (oParemeter.NoSPM.Length > 0)
                {
                    SSQL = SSQL + " AND sNoBukti like  '%" + oParemeter.NoSPM.Trim()  + "%'";
                   // paramCollection.Add(new DBParameter("@NoSPM", oParemeter.NoSPM));
                }
                if (oParemeter.Status > -1)
                {
                    // SSQL = SSQL + " AND iStatus = @STATUS";
                    //paramCollection.Add(new DBParameter("@STATUS", oParemeter.Status));

                    if (oParemeter.Status == 3 || oParemeter.LstStatus.Contains(3))
                    {
                        SSQL = SSQL + " AND ( iSTatus = 3 or iSTatus = 4) ";
                        SSQL = SSQL + " AND cast(dtSP2D  as DATE)  between  @TANGGALAWAL AND @TANGGALAKHIR ";
                         paramCollection.Add(new DBParameter("@TANGGALAWAL", oParemeter.TanggalAwal, DbType.Date));
                    paramCollection.Add(new DBParameter("@TANGGALAKHIR", oParemeter.TanggalAkhir, DbType.Date));

                    }
                    if (oParemeter.Status == 4 || oParemeter.LstStatus.Contains(4))
                    {
                        SSQL = SSQL + " AND iSTatus = 4";
                        SSQL = SSQL + " AND cast(dtBukukas   as DATE) between  @TANGGALAWAL AND @TANGGALAKHIR ";
                        paramCollection.Add(new DBParameter("@TANGGALAWAL", oParemeter.TanggalAwal, DbType.Date));
                         paramCollection.Add(new DBParameter("@TANGGALAKHIR", oParemeter.TanggalAkhir, DbType.Date));
                    
                    }
                   
                   
                }
                else
                {
                    string keteranganTanggal = "";

                    string keteranganTanggalditerima = "";



                    if (oParemeter.LstStatus.Contains(3) == true)
                    {
                        keteranganTanggal = keteranganTanggal + " ( cast(dtSP2D  as DATE)  between  @TANGGALAWAL AND @TANGGALAKHIR ) ";
                    }
                    if (oParemeter.LstStatus.Contains(4) == true)
                    {
                        if (keteranganTanggal.Length > 0)
                            keteranganTanggal = keteranganTanggal + " or ";

                        keteranganTanggal = keteranganTanggal + " cast(dtBukukas  as DATE)   between  @TANGGALAWAL AND @TANGGALAKHIR  ";
                    }

                    if (oParemeter.LstStatus.Contains(1) == true)
                    {

                        if (keteranganTanggal.Length > 0)
                            keteranganTanggal = keteranganTanggal + " or ";
                        keteranganTanggal = keteranganTanggal + " cast(dtSPM  as DATE)   between  @TANGGALAWAL AND @TANGGALAKHIR ";
                    }

                    if (oParemeter.LstStatus.Contains(0) == true)
                    {

                        if (keteranganTanggal.Length > 0)
                            keteranganTanggal = keteranganTanggal + " or ";
                        keteranganTanggal = keteranganTanggal + " cast(dtSPP as DATE)     between  @TANGGALAWAL AND @TANGGALAKHIR ";
                    }
                    if (oParemeter.LstStatus.Contains(6) == true)
                    {

                        if (keteranganTanggal.Length > 0)
                            keteranganTanggal = keteranganTanggal + " or ";
                        keteranganTanggal = keteranganTanggal + " cast(dtVerifikasi as DATE)     between  @TANGGALAWAL AND @TANGGALAKHIR ";
                    }
                    if (oParemeter.LstStatus.Contains(10) == true)
                    {

                        if (keteranganTanggal.Length > 0)
                            keteranganTanggal = keteranganTanggal + " or ";
                        keteranganTanggal = keteranganTanggal + " cast(dtVerifikasi as DATE)     between  @TANGGALAWAL AND @TANGGALAKHIR ";
                    }

                    if (oParemeter.LstStatus.Contains(2))
                    {
                        
                        string sawal = oParemeter.TanggalAwal.ToSQLFormat2Angka();
                        string sakhir = oParemeter.TanggalAkhir.ToSQLFormat2Angka();
                        if (keteranganTanggal.Length > 0)
                            keteranganTanggal = keteranganTanggal + " or ";
                        keteranganTanggal = keteranganTanggal + " (FORMAT(dtTerima, 'MM/dd/yyyy') >=" + sawal + " and  FORMAT(dtTerima, 'MM/dd/yyyy') <=" + sakhir + " ) ";
                    }

                    if (keteranganTanggal.Length > 0)
                    {
                        SSQL = SSQL + " AND (" + keteranganTanggal + ")";

                        paramCollection.Add(new DBParameter("@TANGGALAWAL", oParemeter.TanggalAwal, DbType.Date));
                        paramCollection.Add(new DBParameter("@TANGGALAKHIR", oParemeter.TanggalAkhir, DbType.Date));
                    }
                 
                }
                //if (oParemeter.TidakdiBKU == 1)
                //{
                //    SSQL = SSQL + " ADND tSPP.inourut not in (Select inourutSUmber from tBKU )";
                //}
                if (oParemeter.OrderBy.Trim().Length == 0)
                {

                    SSQL = SSQL + " ORDER BY tSPP.inourut, tSPP.btJenis,tSPP.dtSPP,tSPP.sNoSPP,tSPP.dtSPM,tSPP.sNoBukti,tSPP.dtBukukas";//,A.dtTerbitSP2D,A.sNoSP2D, A.sPeruntukan,C.sNamaUK,A.iStatus , isnull(iStatusBKU,0), mJenisGaji.sNama 
                }
                else
                {
                    SSQL = SSQL + " ORDER BY  " + oParemeter.OrderBy.Trim();//tSPP.inourut, tSPP.btJenis,tSPP.dtSPP,tSPP.sNoSPP,tSPP.dtSPM,tSPP.sNoBukti,tSPP.dtBukukas";//,A.dtTerbitSP2D,A.sNoSP2D, A.sPeruntukan,C.sNamaUK,A.iStatus , isnull(iStatusBKU,0), mJenisGaji.sNama 
                }



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SPP()
                                {

                                    PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    NoUrut = DataFormat.GetLong(dr["iNourut"]),
                                    NoSPP = DataFormat.GetString(dr["sNoSPP"]),
                                    NoSPM = DataFormat.GetString(dr["sNoBukti"]),
                                    NoSP2D = DataFormat.GetString(dr["sNoSP2d"]),
                                    dtSPP = DataFormat.GetDateTime(dr["dtSPP"]),
                                    dtSPM = DataFormat.GetDateTime(dr["dtSPM"]),
                                    dtCair = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    dtTerbit = DataFormat.GetDateTime(dr["dtSP2D"]),
                                    Peruntukan = DataFormat.GetString(dr["sPeruntukan"]),
                                    Status = DataFormat.GetInteger(dr["iStatus"]),
                                    Kodekategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    Keterangan = DataFormat.GetString(dr["sKeteranganPekerjaan"]),
                                    NamaDalamRekeningBank = DataFormat.GetString(dr["NamaDlmRekeningBank"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDprogram"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIdkegiatan"]),
                                    KodeSubKegiatan = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                                    kodeKategoripelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    Kodeurusanpelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    JenisKegiatan = DataFormat.GetInteger(dr["btJenisKegiatan"]),

                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    NoSPJUP = DataFormat.GetString(dr["sNoSPJUP"]),
                                    dtSPJUP = DataFormat.GetDateTime(dr["dtSPJUP"]),
                                    KeteranganNamaBank = DataFormat.GetString(dr["KeteranganNamaBank"]),
                                    NoUrutSPD = DataFormat.GetLong(dr["iNoUrutSPD"]),

                                    NoBpp = DataFormat.GetInteger(dr["iNoBPP"]),
                                    Alamat = DataFormat.GetString(dr["sAlamat"]),
                                    NoNPWP = DataFormat.GetString(dr["sNPWP"]),

                                    NamaPenerima = DataFormat.GetString(dr["sNamaPenerima"]),
                                    NoRek = DataFormat.GetString(dr["sNoRek"]),
                                    NamaBank = DataFormat.GetString(dr["sNamaBank"]),
                                    Penerima = DataFormat.GetInteger(dr["btPenerima"]),
                                    KodeBank = DataFormat.GetString(dr["KodeBank"]),

                                    JabatanPenerima = DataFormat.GetString(dr["sjabatanpenerima"]),
                                    NamaPerusahaan = DataFormat.GetString(dr["sNamaPerusahaan"]),
                                    NoKontrak = DataFormat.GetString(dr["sNoKontrak"]),
                                    SSPSetor = DataFormat.GetInteger(dr["bSSPSetor"]),
                                    WaktuPelaksanaan = DataFormat.GetString(dr["swaktuPelaksanaan"]),
                                    dtSSP = DataFormat.GetDateTime(dr["dtSSP"]),
                                    NoSSP = DataFormat.GetString(dr["sNoSSP"]),
                                    dtKontrak = DataFormat.GetDateTime(dr["dtKontrak"]),
                                    INoUrutKontrak = DataFormat.GetLong(dr["inoUrutkontrak"]),
                                    Bulan = DataFormat.GetString(dr["btBulan"]),
                                    NoBAST = DataFormat.GetString(dr["inobast"]),
                                    NoSPPAT = DataFormat.GetString(dr["sNoSPPAT"]),
                                    JenisDocSumber = DataFormat.GetInteger(dr["iJenisDocSumber"]),
                                    SUmberDana = DataFormat.GetInteger(dr["iSumberDana"]),
                                    SubSumberDana = DataFormat.GetInteger(dr["iSUbSumberDana"]),
                                    SifatPajak = DataFormat.GetInteger(dr["iSifatPAjak"]),
                                    BankBUD = DataFormat.GetInteger(dr["iBankBUD"]),
                                    PenandatanganSP2d = DataFormat.GetInteger(dr["idPenandaTanganBUD"]),


                                    PenandatanganBUD = DataFormat.GetInteger(dr["idPenandaTanganBUD"]),
                                    iNOSPP = DataFormat.GetInteger(dr["INoSPP"]),
                                    iNOSP2D = DataFormat.GetInteger(dr["INoSP2D"]),
                                    Bendahara = DataFormat.GetInteger(dr["IDBEndahara"]),
                                    JenisGaji = DataFormat.GetInteger(dr["iJenisGaji"]),
                                    NoUrutKasda = DataFormat.GetInteger(dr["inourutkasda"]),
                                    iNOSPM = DataFormat.GetInteger(dr["iNoSPM"]),



                                    SifatKegiatan = DataFormat.GetInteger(dr["btsifatkegiatan"]),
                                    IDPPTK = DataFormat.GetString(dr["IDPPTK"]),
                                    NIPPPTK = DataFormat.GetString(dr["sNIPPPTK"]),
                                    NamaPPTK = DataFormat.GetString(dr["sNamaPPTK"]),
                                    JabatanPPTK = DataFormat.GetString(dr["sJabatanPPTK"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),

                                    //Rekenings = GetDetail(DataFormat.GetLong(dr["iNOurut"])),
                                    NamaPenandaTanganSPM = DataFormat.GetString(dr["NamaPenandaTanganSPM"]),
                                    JabatanPenandaTanganSPM = DataFormat.GetString(dr["JabatanPenandaTanganSPM"]),
                                    NIPPenandaTanganSPM = DataFormat.GetString(dr["NIPPenandaTanganSPM"]),
                                    idcrt = DataFormat.GetInteger(dr["idcrt"]),
                                    tcrt = DataFormat.GetDateTime(dr["dcrt"]),
                                    UnitAnggaran = DataFormat.GetInteger(dr["UnitANggaran"]),
                                    BanyakKegiatan = DataFormat.GetInteger(dr["BanyakKegiatan"]),
                                    diBKU = DataFormat.GetInteger(dr["diBKU"]),
                                    NamaSumberDana = DataFormat.GetString(dr["NamaSumberDana"]),
                                }).ToList();
                    }

                    if (oParemeter.WithPotongan)
                    {
                        PotonganLogic oPotonganLogic = new PotonganLogic(Tahun);

                        foreach (SPP s in _lst)
                        {
                            lstNoUrut.Add(s.NoUrut);
                        }

                        List<PotonganSPP> lstPotongan = GetPotongan(lstNoUrut);
                        List<PotonganSPP> lstPotonganPerSPP;
                        if (lstPotongan != null)
                        {
                            foreach (SPP s in _lst)
                            {
                                lstPotonganPerSPP = new List<PotonganSPP>();

                                lstPotonganPerSPP = lstPotongan.FindAll(pot => pot.NoUrut == s.NoUrut);

                                var TotalPotongan = (from spp in lstPotonganPerSPP
                                                     select spp).Sum(e => e.Jumlah);
                                _lst[_lst.IndexOf(s)].Potongans = lstPotonganPerSPP;
                                _lst[_lst.IndexOf(s)].JumlahPotongan = (decimal)TotalPotongan;




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
                return null;
            }
        }
        //===
        public List<SPP> GetPenerimaanSPM(string tanggal)
        {
            List<SPP> _lst = new List<SPP>();
            List<long> lstNoUrut = new List<long>();
            try
            {


                SSQL = "SELECT tSPP.* , mSKPD.sNamaSKPD from tSPP INNER join mSKPD  ON tSPP.IDDInas = mSKPD.ID ";
                //" where FORMAT(dtTerima, 'M/dd/yyyy')  =@TANGGAL ";
                //DBParameterCollection paramCollection = new DBParameterCollection();


                //paramCollection.Add(new DBParameter("@TANGGAL", tanggal, DbType.String));
                 

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SPP()
                                {


                                    Keterangan = DataFormat.GetString(dr["sKeteranganPekerjaan"]),
                                    Status = DataFormat.GetInteger(dr["iStatus"]),
                                    NoSPM = DataFormat.GetString(dr["sNoBukti"]),
                                    NamaDinas = DataFormat.GetString(dr["sNamaSKPD"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    NoBpp = DataFormat.GetInteger(dr["iNoRegSPM"]),
                                    TanggalTerimaSPM = DataFormat.GetDateTime(dr["dtTerima"]),
                                 
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
        /// <summary>
        /// ==========
        /// </summary>
        /// <param name="oParemeter"></param>
        /// <returns></returns>
        public List<SPP> GetSPPForJurnal(ParameterBendahara oParemeter)
        {
            List<SPP> _lst = new List<SPP>();
            List<long> lstNoUrut = new List<long>();
            try
            {


                SSQL = "SELECT tSPP.*,vwJurnalRekeningAnggaran.KodeSKPD as InJurnal FROM tSPP LEFT JOIN vwJurnalRekeningAnggaran " +
                      " ON tSPP.inourut = vwJurnalRekeningAnggaran.iSumber " +
                "  where tSPP.iStatus = 4  ";
                DBParameterCollection paramCollection = new DBParameterCollection();

                
                if (oParemeter.IDDInas > 0)
                {
                    SSQL = SSQL + " AND tSPP.IDDInas= @IDDInas";
                    paramCollection.Add(new DBParameter("@IDDInas", oParemeter.IDDInas));
                   
                }

                if (oParemeter.NoSP2D.Length > 0)
                {

                    SSQL = SSQL + " AND tSPP.sNoSP2D like  '%" + oParemeter.NoSP2D + "%'";
                    // paramCollection.Add(new DBParameter("@NoSP2D", oParemeter.NoSP2D));
                }




                SSQL = SSQL + " AND cast(tSPP.dtBukukas   as DATE) between  @TANGGALAWAL AND @TANGGALAKHIR ";
                    
                paramCollection.Add(new DBParameter("@TANGGALAWAL", oParemeter.TanggalAwal, DbType.Date));
                paramCollection.Add(new DBParameter("@TANGGALAKHIR", oParemeter.TanggalAkhir, DbType.Date));


                SSQL = SSQL + " ORDER BY  tSPP.dtBukukas,tSPP.inourut ";
                



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SPP()
                                {

                                    PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    NoUrut = DataFormat.GetLong(dr["iNourut"]),
                                    NoSPP = DataFormat.GetString(dr["sNoSPP"]),
                                    NoSPM = DataFormat.GetString(dr["sNoBukti"]),
                                    NoSP2D = DataFormat.GetString(dr["sNoSP2d"]),
                                    dtSPP = DataFormat.GetDateTime(dr["dtSPP"]),
                                    dtSPM = DataFormat.GetDateTime(dr["dtSPM"]),
                                    dtCair = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    dtTerbit = DataFormat.GetDateTime(dr["dtSP2D"]),
                                    Peruntukan = DataFormat.GetString(dr["sPeruntukan"]),

                                    Status = DataFormat.GetInteger(dr["InJurnal"]),
                                    
                                    Kodekategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    Keterangan = DataFormat.GetString(dr["sKeteranganPekerjaan"]),
                                    NamaDalamRekeningBank = DataFormat.GetString(dr["NamaDlmRekeningBank"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDprogram"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIdkegiatan"]),
                                    KodeSubKegiatan = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                                    kodeKategoripelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    Kodeurusanpelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    JenisKegiatan = DataFormat.GetInteger(dr["btJenisKegiatan"]),

                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    NoSPJUP = DataFormat.GetString(dr["sNoSPJUP"]),
                                    dtSPJUP = DataFormat.GetDateTime(dr["dtSPJUP"]),

                                    
                                    //NoBpp = DataFormat.GetInteger(dr["iNoBPP"]),
                                    //Alamat = DataFormat.GetString(dr["sAlamat"]),
                                    //NoNPWP = DataFormat.GetString(dr["sNPWP"]),

                                    //NamaPenerima = DataFormat.GetString(dr["sNamaPenerima"]),
                                    //NoRek = DataFormat.GetString(dr["sNoRek"]),
                                    //NamaBank = DataFormat.GetString(dr["sNamaBank"]),
                                    //Penerima = DataFormat.GetInteger(dr["btPenerima"]),
                                    //KodeBank = DataFormat.GetString(dr["KodeBank"]),

                                    //JabatanPenerima = DataFormat.GetString(dr["sjabatanpenerima"]),
                                    //NamaPerusahaan = DataFormat.GetString(dr["sNamaPerusahaan"]),
                                    //NoKontrak = DataFormat.GetString(dr["sNoKontrak"]),
                                    //SSPSetor = DataFormat.GetInteger(dr["bSSPSetor"]),
                                    //WaktuPelaksanaan = DataFormat.GetString(dr["swaktuPelaksanaan"]),
                                    //dtSSP = DataFormat.GetDateTime(dr["dtSSP"]),
                                    //NoSSP = DataFormat.GetString(dr["sNoSSP"]),
                                    //dtKontrak = DataFormat.GetDateTime(dr["dtKontrak"]),
                                    //INoUrutKontrak = DataFormat.GetLong(dr["inoUrutkontrak"]),
                                    //Bulan = DataFormat.GetString(dr["btBulan"]),
                                    //NoBAST = DataFormat.GetString(dr["inobast"]),
                                    //NoSPPAT = DataFormat.GetString(dr["sNoSPPAT"]),
                                    //JenisDocSumber = DataFormat.GetInteger(dr["iJenisDocSumber"]),
                                    //SUmberDana = DataFormat.GetInteger(dr["iSumberDana"]),
                                    //SubSumberDana = DataFormat.GetInteger(dr["iSUbSumberDana"]),
                                    //SifatPajak = DataFormat.GetInteger(dr["iSifatPAjak"]),
                                    //BankBUD = DataFormat.GetInteger(dr["iBankBUD"]),
                                    //PenandatanganSP2d = DataFormat.GetInteger(dr["idPenandaTanganBUD"]),


                                    //PenandatanganBUD = DataFormat.GetInteger(dr["idPenandaTanganBUD"]),
                                    //iNOSPP = DataFormat.GetInteger(dr["INoSPP"]),
                                    //iNOSP2D = DataFormat.GetInteger(dr["INoSP2D"]),
                                    //Bendahara = DataFormat.GetInteger(dr["IDBEndahara"]),
                                    //JenisGaji = DataFormat.GetInteger(dr["iJenisGaji"]),
                                    //NoUrutKasda = DataFormat.GetInteger(dr["inourutkasda"]),
                                    //iNOSPM = DataFormat.GetInteger(dr["iNoSPM"]),



                                    //SifatKegiatan = DataFormat.GetInteger(dr["btsifatkegiatan"]),
                                    //IDPPTK = DataFormat.GetString(dr["IDPPTK"]),
                                    //NIPPPTK = DataFormat.GetString(dr["sNIPPPTK"]),
                                    //NamaPPTK = DataFormat.GetString(dr["sNamaPPTK"]),
                                    //JabatanPPTK = DataFormat.GetString(dr["sJabatanPPTK"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),

                                    ////Rekenings = GetDetail(DataFormat.GetLong(dr["iNOurut"])),
                                    //NamaPenandaTanganSPM = DataFormat.GetString(dr["NamaPenandaTanganSPM"]),
                                    //JabatanPenandaTanganSPM = DataFormat.GetString(dr["JabatanPenandaTanganSPM"]),
                                    //NIPPenandaTanganSPM = DataFormat.GetString(dr["NIPPenandaTanganSPM"]),
                                    //idcrt = DataFormat.GetInteger(dr["idcrt"]),
                                    //tcrt = DataFormat.GetDateTime(dr["dcrt"]),
                                 //   UnitAnggaran = DataFormat.GetInteger(dr["UnitANggaran"]),
                                  //  BanyakKegiatan = DataFormat.GetInteger(dr["BanyakKegiatan"]),
                                 //   diBKU = DataFormat.GetInteger(dr["diBKU"]),
                                }).ToList();
                    }

                    if (oParemeter.WithPotongan)
                    {
                        PotonganLogic oPotonganLogic = new PotonganLogic(Tahun);

                        foreach (SPP s in _lst)
                        {
                            lstNoUrut.Add(s.NoUrut);
                        }

                        List<PotonganSPP> lstPotongan = GetPotongan(lstNoUrut);
                        List<PotonganSPP> lstPotonganPerSPP;
                        if (lstPotongan != null)
                        {
                            foreach (SPP s in _lst)
                            {
                                lstPotonganPerSPP = new List<PotonganSPP>();

                                lstPotonganPerSPP = lstPotongan.FindAll(pot => pot.NoUrut == s.NoUrut);

                                var TotalPotongan = (from spp in lstPotonganPerSPP
                                                     select spp).Sum(e => e.Jumlah);
                                _lst[_lst.IndexOf(s)].Potongans = lstPotonganPerSPP;
                                _lst[_lst.IndexOf(s)].JumlahPotongan = (decimal)TotalPotongan;




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
                return null;
            }
        }

        public List<SPP> GetSPPBelumBKU(int iddinas)
        {
            List<SPP> _lst = new List<SPP>();
            List<long> lstNoUrut = new List<long>(); 
            try
            {


                SSQL = "SELECT tSPP.*  from tSPP INNER join mSKPD  ON tSPP.IDDInas = mSKPD.ID " +
                " Where IDDInas= @DINAS and inoUrut not in (Select inourutSumber from tBKU where btJenisbendahara = 2) and iStatus = 4 ";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@DINAS", iddinas, DbType.Int32));
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SPP()
                                {

                                    PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    NoUrut = DataFormat.GetLong(dr["iNourut"]),
                                    NoSPP = DataFormat.GetString(dr["sNoSPP"]),
                                    NoSPM = DataFormat.GetString(dr["sNoBukti"]),
                                    NoSP2D = DataFormat.GetString(dr["sNoSP2d"]),
                                    dtSPP = DataFormat.GetDateTime(dr["dtSPP"]),
                                    dtSPM = DataFormat.GetDateTime(dr["dtSPM"]),
                                    dtCair = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    dtTerbit = DataFormat.GetDateTime(dr["dtSP2D"]),
                                    Peruntukan = DataFormat.GetString(dr["sPeruntukan"]),
                                    Status = DataFormat.GetInteger(dr["iStatus"]),
                                    Kodekategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    Keterangan = DataFormat.GetString(dr["sKeteranganPekerjaan"]),
                                    NamaDalamRekeningBank = DataFormat.GetString(dr["NamaDlmRekeningBank"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDprogram"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIdkegiatan"]),
                                    KodeSubKegiatan = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                                    kodeKategoripelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    Kodeurusanpelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    JenisKegiatan = DataFormat.GetInteger(dr["btJenisKegiatan"]),

                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    NoSPJUP = DataFormat.GetString(dr["sNoSPJUP"]),
                                    dtSPJUP = DataFormat.GetDateTime(dr["dtSPJUP"]),

                                    NoUrutSPD = DataFormat.GetLong(dr["iNoUrutSPD"]),

                                    NoBpp = DataFormat.GetInteger(dr["iNoBPP"]),
                                    Alamat = DataFormat.GetString(dr["sAlamat"]),
                                    NoNPWP = DataFormat.GetString(dr["sNPWP"]),

                                    NamaPenerima = DataFormat.GetString(dr["sNamaPenerima"]),
                                    NoRek = DataFormat.GetString(dr["sNoRek"]),
                                    NamaBank = DataFormat.GetString(dr["sNamaBank"]),
                                    Penerima = DataFormat.GetInteger(dr["btPenerima"]),
                                    KodeBank = DataFormat.GetString(dr["KodeBank"]),

                                    JabatanPenerima = DataFormat.GetString(dr["sjabatanpenerima"]),
                                    NamaPerusahaan = DataFormat.GetString(dr["sNamaPerusahaan"]),
                                    NoKontrak = DataFormat.GetString(dr["sNoKontrak"]),
                                    SSPSetor = DataFormat.GetInteger(dr["bSSPSetor"]),
                                    WaktuPelaksanaan = DataFormat.GetString(dr["swaktuPelaksanaan"]),
                                    dtSSP = DataFormat.GetDateTime(dr["dtSSP"]),
                                    NoSSP = DataFormat.GetString(dr["sNoSSP"]),
                                    dtKontrak = DataFormat.GetDateTime(dr["dtKontrak"]),
                                    INoUrutKontrak = DataFormat.GetLong(dr["inoUrutkontrak"]),
                                    Bulan = DataFormat.GetString(dr["btBulan"]),
                                    NoBAST = DataFormat.GetString(dr["inobast"]),
                                    NoSPPAT = DataFormat.GetString(dr["sNoSPPAT"]),
                                    JenisDocSumber = DataFormat.GetInteger(dr["iJenisDocSumber"]),
                                    SUmberDana = DataFormat.GetInteger(dr["iSumberDana"]),
                                    SubSumberDana = DataFormat.GetInteger(dr["iSUbSumberDana"]),
                                    SifatPajak = DataFormat.GetInteger(dr["iSifatPAjak"]),
                                    BankBUD = DataFormat.GetInteger(dr["iBankBUD"]),
                                    PenandatanganSP2d = DataFormat.GetInteger(dr["idPenandaTanganBUD"]),


                                    PenandatanganBUD = DataFormat.GetInteger(dr["idPenandaTanganBUD"]),
                                    iNOSPP = DataFormat.GetInteger(dr["INoSPP"]),
                                    iNOSP2D = DataFormat.GetInteger(dr["INoSP2D"]),
                                    Bendahara = DataFormat.GetInteger(dr["IDBEndahara"]),
                                    JenisGaji = DataFormat.GetInteger(dr["iJenisGaji"]),
                                    NoUrutKasda = DataFormat.GetInteger(dr["inourutkasda"]),
                                    iNOSPM = DataFormat.GetInteger(dr["iNoSPM"]),
                                   
                            

                                    SifatKegiatan = DataFormat.GetInteger(dr["btsifatkegiatan"]),
                                    IDPPTK = DataFormat.GetString(dr["IDPPTK"]),
                                    NIPPPTK = DataFormat.GetString(dr["sNIPPPTK"]),
                                    NamaPPTK = DataFormat.GetString(dr["sNamaPPTK"]),
                                    JabatanPPTK = DataFormat.GetString(dr["sJabatanPPTK"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),

                                    //Rekenings = GetDetail(DataFormat.GetLong(dr["iNOurut"])),
                                    NamaPenandaTanganSPM = DataFormat.GetString(dr["NamaPenandaTanganSPM"]),
                                    JabatanPenandaTanganSPM = DataFormat.GetString(dr["JabatanPenandaTanganSPM"]),
                                    NIPPenandaTanganSPM = DataFormat.GetString(dr["NIPPenandaTanganSPM"]),
                                    idcrt =DataFormat.GetInteger(dr["idcrt"]),
                                    tcrt = DataFormat.GetDateTime(dr["dcrt"]),
                                    UnitAnggaran = DataFormat.GetInteger(dr["UnitANggaran"]),
                                    BanyakKegiatan = DataFormat.GetInteger(dr["BanyakKegiatan"]),
                                    KeteranganNamaBank = DataFormat.GetString(dr["KeteranganNamaBank"]),
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
        public List<SPPRekening> GetRingkasanGU(long nourut)
        {
            try
            {
                List<SPPRekening> lst = new List<SPPRekening>();
                SSQL = "select mRekening.IIDRekening, mRekening.sNamaRekening,Left(tSPPRekening.IIDRekening,4) as rek ," +
                        " Sum(tSPPRekening.cJumlah) as Jumlah from tSPPRekening inner join mRekening " +
                        " on Left(mRekening.IIDRekening,4)= Left(tSPPRekening.IIDrekening,4) " +
                        " where mRekening.btRoot= 3  and tSPPRekening.inourut =@NOURUT " +
                        " group by mRekening.IIDRekening, Left(tSPPRekening.IIDRekening,4) ," +
                        "mRekening.sNamaRekening order by mRekening.iidrekening";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@NOURUT", nourut));
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new SPPRekening()
                               {
                                   IDRekening = DataFormat.GetLong(dr["IIDrekening"]),
                                   NamaRekening = DataFormat.GetString(dr["sNamaRekening"]),
                                   Jumlah = DataFormat.GetDecimal(dr["Jumlah"]),


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
        public decimal GetJumlah( int idDinas, int JenisSumber, int jenisSPP , DateTime tanggalAwal, DateTime tanggalakhir)
        {
            decimal dRet = 0;
            try
            {

                SSQL = "SELECT sum(tSPP.cJumlah) as Jumlah from tSPP " +
                    " WHERE tSPP.IDDInas =" + idDinas.ToString() +
                     " AND tSPP.dtBukukas >=" + tanggalAwal.ToSQLFormat() + " AND tSPP.dtBukukas <=" + tanggalakhir.ToSQLFormat() +
                     " AND tSPP.btJenis in (3,4,5) ";
                  
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        dRet = DataFormat.GetDecimal(dr["Jumlah"]);
                    }
                }
                return dRet;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return dRet;

            }
        }
        public decimal GetJumlahDetail(int idDinas, int JenisSumber, int jenisSPP, DateTime tanggalAwal, DateTime tanggalakhir)
        {
            decimal dRet = 0;
            try
            {

                SSQL = "SELECT sum(tSPPRekening.cJumlah) as Jumlah from tSPP " +
                    " INNER JOIN tSPPRekening on tSPP.inoUrut= tSPPRekening.inourut " +
                    " WHERE tSPP.IDDInas =" + idDinas.ToString() +
                     " AND tSPP.dtBukukas >=" + tanggalAwal.ToSQLFormat() + " AND tSPP.dtBukukas <=" + tanggalakhir.ToSQLFormat() +
                     " AND tSPP.btJenis in (3,4,5) ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        dRet = DataFormat.GetDecimal(dr["Jumlah"]);
                    }
                }
                return dRet;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return dRet;

            }
        }
        public List<SPP> GetForSP2DOnline(ParameterBendahara oParemeter)
        {
            List<SPP> _lst = new List<SPP>();
            try
            {
                SSQL = "SELECT sNoSP2D , mskpd.snamaskpd ,(SELECT sum(cJumlah) as Jumlah from tSPPRekening WHERE inourut = tSPP.inourut) as Jumlah, "+
                    " sum(cJumlah)  from tSPPPotongan WHERE inourut = tSPP.inourut)  as JumlahPotongan from tSPP " + 
                    " INNER JOIN mskpd on mskpd.id= tspp.iddinas WHERE 1>0 ";



                SSQL = SSQL + " AND tspp.btstatus = 4 and dtTerbitSP2D between @tanggal1 ans @tanggal2 order by snosp2d";

                //SSQL = SSQL + "ORDER BY tSPP.inourut, tSPP.btJenis,tSPP.dtSPP,tSPP.sNoSPP,tSPP.dtSPM,tSPP.sNoBukti,tSPP.dtBukukas";//,A.dtTerbitSP2D,A.sNoSP2D, A.sPeruntukan,C.sNamaUK,A.iStatus , isnull(iStatusBKU,0), mJenisGaji.sNama 


                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@tanggal1", oParemeter.TanggalAwal, DbType.Date));
                paramCollection.Add(new DBParameter("@tanggal2", oParemeter.TanggalAkhir,DbType.Date));
               
                DataTable dt = new DataTable();
               
                dt = _dbHelper.ExecuteDataTable(SSQL,paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SPP()
                                {
                                    NoUrut = DataFormat.GetLong(dr["iNOurut"]),
                                    NoSPP = DataFormat.GetString(dr["sNoSPP"]),
                                    NoSPM = DataFormat.GetString(dr["sNoBukti"]),
                                    NoSP2D = DataFormat.GetString(dr["sNoSP2d"]),
                                    dtSPP = DataFormat.GetDateTime(dr["dtSPP"]),
                                    dtSPM = DataFormat.GetDateTime(dr["dtSPM"]),
                                    dtCair = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    dtTerbit = DataFormat.GetDateTime(dr["dtTerbitSP2D"]),
                                    Peruntukan = DataFormat.GetString(dr["sPeruntukan"]),
                                    Status = DataFormat.GetInteger(dr["iStatus"]),
                                    Kodekategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    Keterangan = DataFormat.GetString(dr["sKeteranganPekerjaan"]),
                                    PPKD = DataFormat.GetInteger(dr["bPPKD"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDprogram"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIdkegiatan"]),
                                    KodeSubKegiatan = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                                    kodeKategoripelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    Kodeurusanpelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    JenisKegiatan = DataFormat.GetInteger(dr["btJenisKegiatan"]),
                                    NamaDalamRekeningBank = DataFormat.GetString(dr["NamaDlmRekeningBank"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    NoSPJUP = DataFormat.GetString(dr["sNoSPJUP"]),
                                    dtSPJUP = DataFormat.GetDateTime(dr["dtSPJUP"]),
                                    NoUrutSPD = DataFormat.GetLong(dr["iNoUrutSPD"]),
                                    NoBpp = DataFormat.GetInteger(dr["iNoBPP"]),
                                    Alamat = DataFormat.GetString(dr["sAlamat"]),

                                    NamaPenerima = DataFormat.GetString(dr["sNamaPenerima"]),
                                    NoRek = DataFormat.GetString(dr["sNoRek"]),
                                    NamaBank = DataFormat.GetString(dr["sNamaBank"]),
                                    Penerima = DataFormat.GetInteger(dr["btPenerima"]),
                                    JabatanPenerima = DataFormat.GetString(dr["sjabatanpenerima"]),
                                    
                                    NamaPerusahaan = DataFormat.GetString(dr["sNamaPerusahaan"]),
                                    NoKontrak = DataFormat.GetString(dr["sNoKontrak"]),
                                    SSPSetor = DataFormat.GetInteger(dr["bSSPSetor"]),
                                    WaktuPelaksanaan = DataFormat.GetString(dr["swaktuPelaksanaan"]),
                                    dtSSP = DataFormat.GetDateTime(dr["dtSSP"]),
                                    NoSSP = DataFormat.GetString(dr["sNoSSP"]),
                                    dtKontrak = DataFormat.GetDateTime(dr["dtKontrak"]),
                                    Bulan = DataFormat.GetString(dr["btBulan"]),
                                    NoBAST = DataFormat.GetString(dr["inobast"]),
                                    NoSPPAT = DataFormat.GetString(dr["sNoSPPAT"]),
                                    JenisDocSumber = DataFormat.GetInteger(dr["iJenisDocSumber"]),
                                    SUmberDana = DataFormat.GetInteger(dr["iSumberDana"]),
                                    SubSumberDana = DataFormat.GetInteger(dr["iSUbSumberDana"]),
                                    SifatPajak = DataFormat.GetInteger(dr["iSifatPAjak"]),
                                    BankBUD = DataFormat.GetInteger(dr["iBankBUD"]),
                                    PenandatanganSP2d = DataFormat.GetInteger(dr["idPenandaTanganBUD"]),
                     
                                    PenandatanganBUD = DataFormat.GetInteger(dr["idPenandaTanganBUD"]),
                                    iNOSPP = DataFormat.GetInteger(dr["INoSPP"]),
                                    iNOSPM = DataFormat.GetInteger(dr["INoSPM"]),
                                    iNOSP2D = DataFormat.GetInteger(dr["INoSP2D"]),
                                    Bendahara = DataFormat.GetInteger(dr["IDBEndahara"]),
                                    JenisGaji = DataFormat.GetInteger(dr["iJenisGaji"]),
                                    NoUrutKasda = DataFormat.GetInteger(dr["inourutkasda"]),
                                    INoUrutKontrak = DataFormat.GetLong(dr["inoUrutkontrak"]),
                                    SifatKegiatan = DataFormat.GetInteger(dr["btsifatkegiatan"]),
                                    IDPPTK = DataFormat.GetString(dr["IDPPTK"]),
                                    NIPPPTK = DataFormat.GetString(dr["sNIPPPTK"]),
                                    NamaPPTK = DataFormat.GetString(dr["sNamaPPTK"]),
                                    JabatanPPTK = DataFormat.GetString(dr["sJabatanPPTK"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    idcrt = DataFormat.GetInteger(dr["idcrt"]),
                                    tcrt = DataFormat.GetDateTime(dr["dcrt"]),
                                    UnitAnggaran = DataFormat.GetInteger(dr["UnitANggaran"]),
                               
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
        public bool ApaAdaDiSPP(long inourutSPJ)
        {
            SPP oSPP = new SPP();
            try
            {

                SSQL = "SELECT * FROM TSPP where sNoSPJUP ='" + inourutSPJ.ToString() +"'";
                DataTable dt = new DataTable();

                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public SPP GetByID(long inourut, bool withDetail= false)
        {
            SPP oSPP = new SPP();
            try
            {

                SSQL = "SELECT * FROM TSPP where iNoUrut =" + inourut.ToString() ;
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        DataRow dr = dt.Rows[0];
                        oSPP = new SPP
                        {
                            PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                            Tahun = DataFormat.GetInteger(dr["iTahun"]),
                            NoUrut = DataFormat.GetLong(dr["iNourut"]),
                            NoSPP = DataFormat.GetString(dr["sNoSPP"]),
                            NoSPM = DataFormat.GetString(dr["sNoBukti"]),
                            NoSP2D = DataFormat.GetString(dr["sNoSP2d"]),
                            dtSPP = DataFormat.GetDateTime(dr["dtSPP"]),
                            dtSPM = DataFormat.GetDateTime(dr["dtSPM"]),
                            dtCair = DataFormat.GetDateTime(dr["dtBukukas"]),
                            dtTerbit = DataFormat.GetDateTime(dr["dtTerbitSP2D"]),
                            Peruntukan = DataFormat.GetString(dr["sPeruntukan"]),
                            Status = DataFormat.GetInteger(dr["iStatus"]),
                            Kodekategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                            KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                            KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                            KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                            Keterangan = DataFormat.GetString(dr["sKeteranganPekerjaan"]),
                            NamaDalamRekeningBank = DataFormat.GetString(dr["NamaDlmRekeningBank"]),
                            Jenis = DataFormat.GetInteger(dr["btJenis"]),
                            KodeProgram = DataFormat.GetInteger(dr["btIDprogram"]),
                            KodeKegiatan = DataFormat.GetInteger(dr["btIdkegiatan"]),
                            KodeSubKegiatan = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                            kodeKategoripelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                            Kodeurusanpelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                            JenisKegiatan = DataFormat.GetInteger(dr["btJenisKegiatan"]),
                            
                            Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                            NoSPJUP = DataFormat.GetString(dr["sNoSPJUP"]),
                            dtSPJUP = DataFormat.GetDateTime(dr["dtSPJUP"]),

                            NoUrutSPD = DataFormat.GetLong(dr["iNoUrutSPD"]),
                            
                            NoBpp = DataFormat.GetInteger(dr["iNoBPP"]),
                            Alamat = DataFormat.GetString(dr["sAlamat"]),
                            NoNPWP = DataFormat.GetString(dr["sNPWP"]),                            
                            
                            NamaPenerima = DataFormat.GetString(dr["sNamaPenerima"]),
                            NoRek = DataFormat.GetString(dr["sNoRek"]),
                            NamaBank = DataFormat.GetString(dr["sNamaBank"]),
                            Penerima = DataFormat.GetInteger(dr["btPenerima"]),
                            KodeBank = DataFormat.GetString(dr["KodeBank"]),
                            
                            JabatanPenerima = DataFormat.GetString(dr["sjabatanpenerima"]),
                            NamaPerusahaan = DataFormat.GetString(dr["sNamaPerusahaan"]),
                            NoKontrak = DataFormat.GetString(dr["sNoKontrak"]),
                            SSPSetor = DataFormat.GetInteger(dr["bSSPSetor"]),
                            WaktuPelaksanaan = DataFormat.GetString(dr["swaktuPelaksanaan"]),
                            dtSSP = DataFormat.GetDateTime(dr["dtSSP"]),
                            NoSSP = DataFormat.GetString(dr["sNoSSP"]),
                            dtKontrak = DataFormat.GetDateTime(dr["dtKontrak"]),
                            INoUrutKontrak = DataFormat.GetLong(dr["inoUrutkontrak"]),
                            Bulan = DataFormat.GetString(dr["btBulan"]),
                            NoBAST = DataFormat.GetString(dr["inobast"]),
                            NoSPPAT = DataFormat.GetString(dr["sNoSPPAT"]),
                            JenisDocSumber = DataFormat.GetInteger(dr["iJenisDocSumber"]),
                            SUmberDana = DataFormat.GetInteger(dr["iSumberDana"]),
                            SubSumberDana = DataFormat.GetInteger(dr["iSUbSumberDana"]),
                            SifatPajak = DataFormat.GetInteger(dr["iSifatPAjak"]),
                            BankBUD = DataFormat.GetInteger(dr["iBankBUD"]),
                            PenandatanganSP2d = DataFormat.GetInteger(dr["idPenandaTanganBUD"]),
                            
              
                            PenandatanganBUD = DataFormat.GetInteger(dr["idPenandaTanganBUD"]),
                            iNOSPP = DataFormat.GetInteger(dr["INoSPP"]),
                            iNOSP2D = DataFormat.GetInteger(dr["INoSP2D"]),
                            Bendahara = DataFormat.GetInteger(dr["IDBEndahara"]),
                            JenisGaji = DataFormat.GetInteger(dr["iJenisGaji"]),
                            NoUrutKasda = DataFormat.GetInteger(dr["inourutkasda"]),
                            iNOSPM = DataFormat.GetInteger(dr["iNoSPM"]),
                          
                            SifatKegiatan = DataFormat.GetInteger(dr["btsifatkegiatan"]),
                            IDPPTK = DataFormat.GetString(dr["IDPPTK"]),
                            NIPPPTK = DataFormat.GetString(dr["sNIPPPTK"]),
                            NamaPPTK = DataFormat.GetString(dr["sNamaPPTK"]),
                            JabatanPPTK = DataFormat.GetString(dr["sJabatanPPTK"]),
                            IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                            NamaPenandaTanganSPM = DataFormat.GetString(dr["NamaPenandaTanganSPM"]),
                            JabatanPenandaTanganSPM = DataFormat.GetString(dr["JabatanPenandaTanganSPM"]),
                            NIPPenandaTanganSPM = DataFormat.GetString(dr["NIPPenandaTanganSPM"]),                                 

                            Rekenings = GetDetail(DataFormat.GetLong(dr["iNOurut"])),
                            idcrt = DataFormat.GetInteger(dr["idcrt"]),
                            tcrt = DataFormat.GetDateTime(dr["dcrt"]),
                            UnitAnggaran = DataFormat.GetInteger(dr["UnitANggaran"]),

                            
                            
                        };

                        //if (withDetail)
                        //{
                        //   GetProgramKegiatan(ref oSPP);
                        //}
                        return oSPP;
                    }
                }
                return null;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }
        }
        private List<int> GetListProgram(List<SPPRekening> lst)
        {
            List<int> ret= new List<int>();
            foreach (SPPRekening  r in lst)
            {
                if (ret.Contains(r.IDProgram) == false)
                {
                    ret.Add(r.IDProgram);
                }
                
            }
            return ret;
        }

        private List<int> GetListUrusan(List<SPPRekening> lst)
        {
            List<int> ret = new List<int>();
            foreach (SPPRekening r in lst)
            {
                if (ret.Contains(r.IDUrusan) == false)
                {
                    ret.Add(r.IDUrusan);
                }

            }
            return ret;
        }
       
        private List<long > GetListSubKegiatan(List<SPPRekening> lst)
        {
            List<long> ret = new List<long>();
            foreach (SPPRekening r in lst)
            {
                if (ret.Contains(r.IDSubKegiatan ) == false)
                {
                    ret.Add(r.IDSubKegiatan);
                }

            }
            return ret;
        }
        //public List<SPPRekening> 
        public bool GetProgramKegiatan(ref SPP oSPP)
        {
            try
            {
                //TKegiatanAPBDLogic oLogic = new TKegiatanAPBDLogic(Tahun);
                //TKegiatanAPBD keg = new TKegiatanAPBD();
                //keg = oLogic.GetKegiatan(Tahun, oSPP.IDDInas, oSPP.IDUrusan, oSPP.IDProgram, oSPP.IDKegiatan, 3, 0);
                //if (keg != null)
                //{
                //    oSPP.NamaKegiatan = keg.Nama;
                //    oSPP.IDKegiatan = keg.IDKegiatan;
                //}
                //TProgramAPBDLogic oPrgLogic = new TProgramAPBDLogic(Tahun);
                //TProgramAPBD prg = new TProgramAPBD();
                //prg = oPrgLogic.GetByID(Tahun, oSPP.IDUrusan, oSPP.IDDInas, oSPP.IDProgram);
                //if (prg != null)
                //{
                //    oSPP.NamaProgram = prg.Nama;
                //    oSPP.IDProgram = prg.IDProgram;
                //}
                //UrusanLogic urLogic = new UrusanLogic(Tahun);
                //Urusan ur = new Urusan();
                //ur = urLogic.GetByID(oSPP.IDUrusan);
                //if (prg != null)
                //{
                //    oSPP.NamaUrusan = ur.Nama;
                //    oSPP.IDProgram = prg.IDProgram;
                //}
                //oSPP.Rekenings = GetDetail(oSPP.NoUrut);

                //oSPP.Rb = GetRingkasanBelanja(oSPP);
                //oSPP.Kelengkapaan = GetKelengkapan(oSPP);


                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }
        public  List<SPPRekening> GetDetail(long iNoUrut=0, long SubKegiatan =0 )
        {
            List<SPPRekening> _lst = new List<SPPRekening>();
            try
            {
                //SSQL = " SELECT tSPPRekening.*, mRekening.sNamaRekening as Nama from tSPPRekening INNER jOIN mRekening ON tSPPRekening.IIDRekening = mRekening.IIDRekening WHERE tSPPRekening.inourut = " + iNoUrut.ToString() ;
                //SSQL = SSQL + " ORDER BY IDKegiatan, IIDRekening";

                //SSQL ="  select tSPPRekening.*,tPrograms_A.sNamaProgram as NamaProgram,";

                //SSQL = SSQL + "  tKegiatan_A.sNama as NamaKegiatan, tSUbKegiatan.Nama as NamaSUbKegiatan,";
                //SSQL = SSQL + " mRekening.sNamaRekening as NamaRekening,";
                //SSQL = SSQL + " mUrusan.sNamaUrusan as NamaUrusan ";
                //SSQL = SSQL + " from tSPPRekening inner join tSPP On tSPPRekening.inourut = tSPP.inourut ";
                //SSQL = SSQL + " INNER join tPrograms_A on tPrograms_A.iTahun = tSPP.iTahun  and tPrograms_A.IDDinas = tSPP.IDDInas ";
                //SSQL = SSQL + " AND tPrograms_A.IDProgram = tSPPRekening.IDProgram    ";
                //SSQL = SSQL + " INNER join tKegiatan_A on tKegiatan_A.iTahun = tSPP.iTahun and tKegiatan_A.IDDinas = tSPP.IDDInas ";
                //SSQL = SSQL + " AND tKegiatan_A.IDKegiatan = tSPPRekening.IDKegiatan  AND tKegiatan_A.btKOdeUK = tSPPRekening.btKodeuk ";
                //SSQL = SSQL + " INNER join tSUbKegiatan on tSUbKegiatan.iTahun = tSPP.iTahun and tSUbKegiatan.IDDinas = tSPP.IDDInas ";
                //SSQL = SSQL + " AND tSUbKegiatan.IDSubKegiatan = tSPPRekening.IDSubKegiatan and tSUbKegiatan.btKOdeUK = tSPPRekening.btKOdeUK ";
                //SSQL = SSQL + " INNER join mRekening on mRekening.IIDRekening= tSPPRekening.IIDRekening  ";
                //SSQL = SSQL + " INNER join mUrusan  on mUrusan.ID = tSPPRekening.IDUrusan ";



                SSQL = "  select tSPPRekening.*,'' as NamaProgram,";

                SSQL = SSQL + "  '' as NamaKegiatan, '' as NamaSUbKegiatan,";
                SSQL = SSQL + " mRekening.sNamaRekening as NamaRekening,";
                SSQL = SSQL + " '' as NamaUrusan ";
                SSQL = SSQL + " from tSPPRekening inner join tSPP On tSPPRekening.inourut = tSPP.inourut ";
                //SSQL = SSQL + " INNER join tPrograms_A on tPrograms_A.iTahun = tSPP.iTahun  and tPrograms_A.IDDinas = tSPP.IDDInas ";
                //SSQL = SSQL + " AND tPrograms_A.IDProgram = tSPPRekening.IDProgram    ";
                //SSQL = SSQL + " INNER join tKegiatan_A on tKegiatan_A.iTahun = tSPP.iTahun and tKegiatan_A.IDDinas = tSPP.IDDInas ";
                //SSQL = SSQL + " AND tKegiatan_A.IDKegiatan = tSPPRekening.IDKegiatan  AND tKegiatan_A.btKOdeUK = tSPPRekening.btKodeuk ";
                //SSQL = SSQL + " INNER join tSUbKegiatan on tSUbKegiatan.iTahun = tSPP.iTahun and tSUbKegiatan.IDDinas = tSPP.IDDInas ";
                //SSQL = SSQL + " AND tSUbKegiatan.IDSubKegiatan = tSPPRekening.IDSubKegiatan and tSUbKegiatan.btKOdeUK = tSPPRekening.btKOdeUK ";
                SSQL = SSQL + " INNER join mRekening on mRekening.IIDRekening= tSPPRekening.IIDRekening  ";

                //SSQL = SSQL + " INNER join mUrusan  on mUrusan.ID = tSPPRekening.IDUrusan ";



                DBParameterCollection paramCollection = new DBParameterCollection();
                if (iNoUrut > 0)
                {
                    paramCollection.Add(new DBParameter("@NOURUT", iNoUrut));
                    SSQL = SSQL + " where tSPP.inourut = @NOURUT and tSPPRekening.cJumlah> 0 ";

                    if (SubKegiatan > 0)
                    {
                        paramCollection.Add(new DBParameter("@IDSUBKEGIATAN", SubKegiatan));
                        SSQL = SSQL + " AND tSPPRekening.IDSUBKEGIATAN = @IDSUBKEGIATAN and tSPPRekening.cJumlah> 0 ";
                    }
                }
                else
                {
                    if (SubKegiatan > 0)
                    {
                        paramCollection.Add(new DBParameter("@IDSUBKEGIATAN", SubKegiatan));
                        SSQL = SSQL + " WHERE tSPP.inourut = @NOURUT and tSPPRekening.cJumlah> 0 ";
                    }
                }
                SSQL = SSQL + " ORDER BY tSPPRekening.IIDRekening";


                
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL,paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SPPRekening()
                                {

                                    NoUrut = DataFormat.GetLong(dr["iNOurut"]),
                                    UnitKerja = DataFormat.GetInteger(dr["btKodeUK"]),
                                 
                                    KodeProgram = DataFormat.GetInteger(dr["btIDprogram"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIdkegiatan"]),
                                    KodeSubKegiatan = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                                    KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    

                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSubKegiatan=  DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    NamaRekening = DataFormat.GetString(dr["NamaRekening"]),
                                    NamaProgram = DataFormat.GetString(dr["NamaProgram"]),
                                    NamaSubKegiatan=  DataFormat.GetString(dr["NamaSubKegiatan"]),
                                    NamaKegiatan  = DataFormat.GetString(dr["NamaKegiatan"]),
                                    NamaUrusan = DataFormat.GetString(dr["NamaUrusan"]),
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
        public List<SPPRekening> GetDetail(List<long> lstNoUrut)
        {
            List<SPPRekening> _lst = new List<SPPRekening>();
            try
            {
                int id = 0;
                string sNamaParameter = "";
                DBParameterCollection paramCollection = new DBParameterCollection();
                SSQL = "  select tSPPRekening.*,tPrograms_A.sNamaProgram as NamaProgram,";

                SSQL = SSQL + "  tKegiatan_A.sNama as NamaKegiatan, tSUbKegiatan.Nama as NamaSUbKegiatan,";
                SSQL = SSQL + " mRekening.sNamaRekening as NamaRekening,";
                SSQL = SSQL + " mUrusan.sNamaUrusan as NamaUrusan ";
                SSQL = SSQL + " from tSPPRekening inner join tSPP On tSPPRekening.inourut = tSPP.inourut ";
                SSQL = SSQL + " INNER join tPrograms_A on tPrograms_A.iTahun = tSPP.iTahun  and tPrograms_A.IDDinas = tSPP.IDDInas ";
                SSQL = SSQL + " AND tPrograms_A.IDProgram = tSPPRekening.IDProgram    ";
                SSQL = SSQL + " INNER join tKegiatan_A on tKegiatan_A.iTahun = tSPP.iTahun and tKegiatan_A.IDDinas = tSPP.IDDInas ";
                SSQL = SSQL + " AND tKegiatan_A.IDKegiatan = tSPPRekening.IDKegiatan  AND tKegiatan_A.btKOdeUK = tSPPRekening.btKodeuk ";
                SSQL = SSQL + " INNER join tSUbKegiatan on tSUbKegiatan.iTahun = tSPP.iTahun and tSUbKegiatan.IDDinas = tSPP.IDDInas ";
                SSQL = SSQL + " AND tSUbKegiatan.IDSubKegiatan = tSPPRekening.IDSubKegiatan and tSUbKegiatan.btKOdeUK = tSPPRekening.btKOdeUK ";
                SSQL = SSQL + " INNER join mRekening on mRekening.IIDRekening= tSPPRekening.IIDRekening  ";
                SSQL = SSQL + " INNER join mUrusan  on mUrusan.ID = tSPPRekening.IDUrusan ";
        

                SSQL = SSQL + " where tSPP.inourut in ( ";
                foreach (long nu in lstNoUrut)
                {
                    sNamaParameter = "@NoUrut" + id.ToString();
                    SSQL = SSQL + sNamaParameter + ",";
                    paramCollection.Add(new DBParameter(sNamaParameter, nu, DbType.Int64));
                    id++;
                }

                sNamaParameter = "@NoUrut" + id.ToString();
                SSQL = SSQL + sNamaParameter + ")";
                paramCollection.Add(new DBParameter(sNamaParameter, 0, DbType.Int64));
                SSQL = SSQL + " ORDER BY tSPPRekening.inOurut, tSPPRekening.IIDRekening";
            
                //SSQL = " SELECT tSPPRekening.*, mRekening.sNamaRekening as Nama from tSPPRekening INNER jOIN mRekening ON tSPPRekening.IIDRekening = mRekening.IIDRekening WHERE tSPPRekening.inourut = " + iNoUrut.ToString() ;
                //SSQL = SSQL + " ORDER BY IDKegiatan, IIDRekening";

            



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SPPRekening()
                                {

                                    NoUrut = DataFormat.GetLong(dr["iNOurut"]),
                                    UnitKerja = DataFormat.GetInteger(dr["btKodeUK"]),

                                    KodeProgram = DataFormat.GetInteger(dr["btIDprogram"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIdkegiatan"]),
                                    KodeSubKegiatan = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                                    KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),


                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),

                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    NamaRekening = DataFormat.GetString(dr["NamaRekening"]),
                                    NamaProgram = DataFormat.GetString(dr["NamaProgram"]),
                                    NamaSubKegiatan = DataFormat.GetString(dr["NamaSubKegiatan"]),
                                    NamaKegiatan = DataFormat.GetString(dr["NamaKegiatan"]),
                                    NamaUrusan = DataFormat.GetString(dr["NamaUrusan"]),
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
        //private string GetNamaUnit(int iddinas, int KodeUK)
        //{
        //    if (KodeUK == 0)
        //    {
        //        SKPDLogic oLogic = new SKPDLogic(Tahun);
                
        //    }

        //}
        public  List<SPPRekening> GetSPPDetail(int dinas)
        {
            List<SPPRekening> _lst = new List<SPPRekening>();
            try
            {
                //SSQL = " SELECT tSPPRekening.*, mRekening.sNamaRekening as Nama from tSPPRekening INNER jOIN mRekening ON tSPPRekening.IIDRekening = mRekening.IIDRekening WHERE tSPPRekening.inourut = " + iNoUrut.ToString() ;
                //SSQL = SSQL + " ORDER BY IDKegiatan, IIDRekening";

                SSQL ="  select tSPPRekening.*,tPrograms_A.sNamaProgram as NamaProgram,";

                SSQL = SSQL + "  tKegiatan_A.sNama as NamaKegiatan, tSUbKegiatan.Nama as NamaSUbKegiatan,";
                SSQL = SSQL + " mRekening.sNamaRekening as NamaRekening,";
                SSQL = SSQL + " mUrusan.sNamaUrusan as NamaUrusan, tSPP.IDDinas ";
                SSQL = SSQL + " from tSPPRekening inner join tSPP On tSPPRekening.inourut = tSPP.inourut ";
                SSQL = SSQL + " INNER join tPrograms_A on tPrograms_A.iTahun = tSPP.iTahun  and tPrograms_A.IDDinas = tSPP.IDDInas ";
                SSQL = SSQL + " AND tPrograms_A.IDProgram = tSPPRekening.IDProgram  ";
                SSQL = SSQL + " INNER join tKegiatan_A on tKegiatan_A.iTahun = tSPP.iTahun and tKegiatan_A.IDDinas = tSPP.IDDInas ";
                SSQL = SSQL + " AND tKegiatan_A.IDKegiatan = tSPPRekening.IDKegiatan ";
                SSQL = SSQL + " INNER join tSUbKegiatan on tSUbKegiatan.iTahun = tSPP.iTahun and tSUbKegiatan.IDDinas = tSPP.IDDInas ";
                SSQL = SSQL + " AND tSUbKegiatan.IDSubKegiatan = tSPPRekening.IDSubKegiatan ";
                SSQL = SSQL + " INNER join mRekening on mRekening.IIDRekening= tSPPRekening.IIDRekening  ";
                SSQL = SSQL + " INNER join mUrusan  on mUrusan.ID = tSPPRekening.IDUrusan ";
                SSQL = SSQL + " where tSPP.IDDInas = @DINAS ";
                SSQL = SSQL + " ORDER BY tSPPRekening.IIDRekening";

               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@DINAS", dinas));


                

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL,paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SPPRekening()
                                {

                                    NoUrut = DataFormat.GetLong(dr["iNOurut"]),
                                    UnitKerja = DataFormat.GetInteger(dr["btKodeUK"]),
                                    IDDinas = DataFormat.GetInteger(dr["IdDinas"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDprogram"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIdkegiatan"]),
                                    KodeSubKegiatan = DataFormat.GetInteger(dr["btIDSubKegiatan"]),
                                    KodekategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    

                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSubKegiatan=  DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    NamaRekening = DataFormat.GetString(dr["NamaRekening"]),
                                    NamaProgram = DataFormat.GetString(dr["NamaProgram"]),
                                    NamaSubKegiatan=  DataFormat.GetString(dr["NamaSubKegiatan"]),
                                    NamaKegiatan  = DataFormat.GetString(dr["NamaKegiatan"]),
                                    NamaUrusan = DataFormat.GetString(dr["NamaUrusan"]),
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
        public int GetNoPeguji()
        {
            return ReadNoPenguji(Tahun);
        }
        public bool SimpanUbahUraian(long inourut, string sUraian)
        {
            try
            {
                SSQL = "UPDATE tSPP SET sKeteranganPekerjaan =@KeteranganPekerjaan " +
                       " WHERE iNoUrut=@NoUrut ";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@KeteranganPekerjaan", sUraian));
                paramCollection.Add(new DBParameter("@NoUrut", inourut));
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;

                return false;
            }
        }
        public long Simpan(ref SPP oSPP)
        {
            try
            {
                long mNoUrut = 0;
                if (oSPP.NoUrut == 0)
                {
                   
                    mNoUrut =ReadNo(E_KOLOM_NOURUT.CON_URUT_SPP, oSPP.IDDInas);



                                                    
                    SSQL = "INSERT INTO tSPP (iNoUrut, iTAhun, btKodekategori, btKodeUrusan, btKodeSKPD, " +
                         " btKodeUK, sNoSPP,dtSPP, iStatus,snamaPenerima,btJenis, btJenisKegiatan,cJumlah,sNoSPJUP," +
                         " inoUrutSPD, sKeteranganPekerjaan , sAlamat ,sNoRek ,sNamaBank,btPenerima ,sJabatanPenerima,sNamaPerusahaan ,sNoKontrak ," +
                         "sWaktuPelaksanaan ,dtKontrak ,bPPKD ,btBulan ,sNPWP,iMultiYear , btBulan2 , iNoBAST , " +
                         "iJenisDocSumber ,inokontrak, iSUmberDana,btIDbank,iNoSPP,idBendahara, iJenisGaji, inoUrutkontrak,idDinas , " +
                         " btSIfatkegiatan,idpptk , sNamaPPTK , sNIPPPTK , sJabatanPPTK , NamaDlmRekeningBank,KodeBank,Banyakkegiatan ,TahapEM, RealisasiFisik,"+
                    " idcrt, dcrt,UnitAnggaran,KeteranganNamaBank ) " + 
                    " values (" +
                     "@NoUrut, @TAhun, @Kodekategori, @odeUrusan, @KodeSKPD, @KodeUK, @NoSPP,   @dtSPP, @iStatus,@namaPenerima,@Jenis, @JenisKegiatan,@Jumlah,@NoSPJUP," +
                        "@noUrutSPD, @KeteranganPekerjaan , @Alamat ,@NoRek ,@NamaBank,@btPenerima ,@JabatanPenerima,@NamaPerusahaan ,@NoKontrak," +
                    "@WaktuPelaksanaan ,@dtKontrak ,@PPKD ,@Bulan ,@NPWP,@MultiYear , @Bulan2 , @NoBAST , " +
                         "@JenisDocSumber ,@inokontrak, @SUmberDana,@IDbank,@iNoSPP,@idBendahara, @iJenisGaji, @inoUrutkontrak,@idDinas, "+
                        "@btSIfatkegiatan,@idpptk , @NamaPPTK , @NIPPPTK , @sJabatanPPTK , @NamaDlmRekeningBank,@KodeBank ,@Banyakkegiatan ,@TahapEM, @RealisasiFisik,@idcrt, @dcrt,@UnitAnggaran,@KeteranganNamaBank )";

        
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@NoUrut", mNoUrut,DbType.Int64 ));
                    paramCollection.Add(new DBParameter("@TAhun", oSPP.Tahun, DbType.Int32));
                    paramCollection.Add(new DBParameter("@Kodekategori", oSPP.Kodekategori, DbType.Int32));
                    paramCollection.Add(new DBParameter("@odeUrusan", oSPP.KodeUrusan, DbType.Int32 ));
                    paramCollection.Add(new DBParameter("@KodeSKPD",oSPP.KodeSKPD, DbType.Int32));
                  
                    paramCollection.Add(new DBParameter("@KodeUK",oSPP.KodeUK, DbType.Int32));
                    paramCollection.Add(new DBParameter("@NoSPP",oSPP.NoSPP,DbType.String ));
                    paramCollection.Add(new DBParameter("@dtSPP",oSPP.dtSPP,DbType.Date  ));
                    paramCollection.Add(new DBParameter("@iStatus",0));
                    
                    paramCollection.Add(new DBParameter("@namaPenerima",oSPP.NamaPenerima.Replace("'","''''") ));
                    paramCollection.Add(new DBParameter("@Jenis",oSPP.Jenis ));
                    paramCollection.Add(new DBParameter("@JenisKegiatan",oSPP.JenisKegiatan));
                    paramCollection.Add(new DBParameter("@Jumlah",oSPP.Jumlah,DbType.Decimal));
                    paramCollection.Add(new DBParameter("@NoSPJUP",oSPP.NoSPJUP));
                    
                    //paramCollection.Add(new DBParameter("@dtSPJUP",oSPP.dtSPJUP));
                    paramCollection.Add(new DBParameter("@noUrutSPD",oSPP.NoUrutSPD));
                    paramCollection.Add(new DBParameter("@KeteranganPekerjaan",oSPP.Keterangan ));
                    paramCollection.Add(new DBParameter("@Alamat",oSPP.Alamat));
                    paramCollection.Add(new DBParameter("@NoRek",oSPP.NoRek ));
                    paramCollection.Add(new DBParameter("@NamaBank",oSPP.NamaBank ));
                    paramCollection.Add(new DBParameter("@btPenerima",oSPP.Penerima ));
                    paramCollection.Add(new DBParameter("@JabatanPenerima",oSPP.JabatanPenerima ));
                    paramCollection.Add(new DBParameter("@NamaPerusahaan",oSPP.NamaPerusahaan ));
                    paramCollection.Add(new DBParameter("@NoKontrak",oSPP.NoKontrak ));
                  
                    paramCollection.Add(new DBParameter("@WaktuPelaksanaan",oSPP.WaktuPelaksanaan));
                    paramCollection.Add(new DBParameter("@dtKontrak",oSPP.dtKontrak,DbType.Date));
                    paramCollection.Add(new DBParameter("@PPKD",oSPP.PPKD));
                    paramCollection.Add(new DBParameter("@Bulan",oSPP.Bulan));
                    paramCollection.Add(new DBParameter("@NPWP",oSPP.NoNPWP ));
                    paramCollection.Add(new DBParameter("@MultiYear",oSPP.MultiYear));
                    paramCollection.Add(new DBParameter("@Bulan2",oSPP.Bulan2));
              
                    paramCollection.Add(new DBParameter("@NoBAST",oSPP.NoBAST ));
                    
                  paramCollection.Add(new DBParameter("@JenisDocSumber",oSPP.JenisDocSumber));
                  paramCollection.Add(new DBParameter("@inokontrak",oSPP.NoKontrak));
                 // paramCollection.Add(new DBParameter("@IDSUBKegiatan",oSPP.ListIDSubKegiatan));
                  paramCollection.Add(new DBParameter("@SUmberDana",oSPP.SUmberDana));
                  paramCollection.Add(new DBParameter("@IDbank",oSPP.IDBank));
                  paramCollection.Add(new DBParameter("@iNoSPP",oSPP.iNOSPP));
                  paramCollection.Add(new DBParameter("@idBendahara",oSPP.Bendahara ));
                  paramCollection.Add(new DBParameter("@iJenisGaji",oSPP.JenisGaji));
                  paramCollection.Add(new DBParameter("@inoUrutkontrak",oSPP.INoUrutKontrak));
                  paramCollection.Add(new DBParameter("@idDinas",oSPP.IDDInas ));
                    
                  paramCollection.Add(new DBParameter("@btSIfatkegiatan",oSPP.SifatKegiatan));
                    
                  paramCollection.Add(new DBParameter("@idpptk",oSPP.IDPPTK));
                  paramCollection.Add(new DBParameter("@NamaPPTK",oSPP.NamaPPTK));
                  paramCollection.Add(new DBParameter("@NIPPPTK",oSPP.NIPPPTK));

                  paramCollection.Add(new DBParameter("@sJabatanPPTK",oSPP.JabatanPPTK));
                  paramCollection.Add(new DBParameter("@NamaDlmRekeningBank",oSPP.NamaDalamRekeningBank.Replace("'","''''")));
                  paramCollection.Add(new DBParameter("@KodeBank", oSPP.KodeBank));
                  paramCollection.Add(new DBParameter("@Banyakkegiatan", oSPP.BanyakKegiatan));
                  paramCollection.Add(new DBParameter("@TahapEM", oSPP.TahapEM));
                  paramCollection.Add(new DBParameter("@RealisasiFisik", oSPP.RealisasiFisik));
                    paramCollection.Add(new DBParameter("@idcrt", oSPP.idcrt));
                    paramCollection.Add(new DBParameter("@dcrt", DateTime.Now.Date,DbType.Date));
                    paramCollection.Add(new DBParameter("@UnitAnggaran", oSPP.UnitAnggaran));
                    paramCollection.Add(new DBParameter("@KeteranganNamaBank", oSPP.KeteranganNamaBank));
                 
                  
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    oSPP.NoUrut = mNoUrut;

                }
                else
                {
                    SSQL = "UPDATE tSPP SET btKodekategori=@Kodekategori, btKodeUrusan=@odeUrusan, btKodeSKPD=@KodeSKPD, " +
                       " btKodeUK=@KodeUK, sNoSPP=@NoSPP,dtSPP=@dtSPP, iStatus=@iStatus," +
                       "snamaPenerima=@namaPenerima,btJenis=@Jenis, btJenisKegiatan=@JenisKegiatan,cJumlah=@Jumlah,sNoSPJUP=@NoSPJUP," +
                       " inoUrutSPD=@noUrutSPD, sKeteranganPekerjaan =@KeteranganPekerjaan, sAlamat =@Alamat," +
                       "sNoRek=@NoRek  ,sNamaBank=@NamaBank,btPenerima =@btPenerima ,sJabatanPenerima=@JabatanPenerima,sNamaPerusahaan=@NamaPerusahaan ,sNoKontrak=@NoKontrak ," +
                       "sWaktuPelaksanaan =@WaktuPelaksanaan ,dtKontrak=@dtKontrak  ,bPPKD=@PPKD ,btBulan=@Bulan ," +
                       "sNPWP=@NPWP,iMultiYear=@MultiYear  , btBulan2 =@Bulan2, iNoBAST =@NoBAST , " +
                       "iJenisDocSumber =@JenisDocSumber,inokontrak=@inokontrak, iSUmberDana=@SUmberDana," +
                       "btIDbank=@IDbank,iNoSPP=@iNoSPP,idBendahara=@idBendahara, iJenisGaji=@iJenisGaji, inoUrutkontrak=@inoUrutkontrak,idDinas =@idDinas, " +
                       "btSIfatkegiatan=@btSIfatkegiatan,idpptk=@idpptk , sNamaPPTK =@NamaPPTK, sNIPPPTK =@NIPPPTK, " +
                       "sJabatanPPTK =@sJabatanPPTK, NamaDlmRekeningBank=@NamaDlmRekeningBank ,KodeBank=@KodeBank ,Banyakkegiatan =@Banyakkegiatan,TahapEM=@TahapEM," +
                       "RealisasiFisik =@RealisasiFisik  ,UnitAnggaran=@UnitAnggaran,KeteranganNamaBank=@KeteranganNamaBank " +
                       " WHERE iNoUrut=@NoUrut AND  iTAhun=@TAhun ";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    
                    paramCollection.Add(new DBParameter("@Kodekategori", oSPP.Kodekategori));
                    paramCollection.Add(new DBParameter("@odeUrusan", oSPP.KodeUrusan));
                    paramCollection.Add(new DBParameter("@KodeSKPD", oSPP.KodeSKPD));
             
                    paramCollection.Add(new DBParameter("@KodeUK", oSPP.KodeUK));
                    paramCollection.Add(new DBParameter("@NoSPP", oSPP.NoSPP));
                    paramCollection.Add(new DBParameter("@dtSPP", oSPP.dtSPP, DbType.Date));
                    paramCollection.Add(new DBParameter("@iStatus", oSPP.Status));
                    
                    paramCollection.Add(new DBParameter("@namaPenerima", oSPP.NamaPenerima.Replace("'","''''")));
                    paramCollection.Add(new DBParameter("@Jenis", oSPP.Jenis));
                    paramCollection.Add(new DBParameter("@JenisKegiatan", oSPP.JenisKegiatan));
                    paramCollection.Add(new DBParameter("@Jumlah", oSPP.Jumlah,DbType.Decimal));
                    paramCollection.Add(new DBParameter("@NoSPJUP", oSPP.NoSPJUP));

                    paramCollection.Add(new DBParameter("@noUrutSPD", oSPP.NoUrutSPD));
                    paramCollection.Add(new DBParameter("@KeteranganPekerjaan", oSPP.Keterangan));
                    paramCollection.Add(new DBParameter("@Alamat", oSPP.Alamat));
                    paramCollection.Add(new DBParameter("@NoRek", oSPP.NoRek));
                    paramCollection.Add(new DBParameter("@NamaBank", oSPP.NamaBank));
                    paramCollection.Add(new DBParameter("@btPenerima", oSPP.Penerima));
                    paramCollection.Add(new DBParameter("@JabatanPenerima", oSPP.JabatanPenerima));
                    paramCollection.Add(new DBParameter("@NamaPerusahaan", oSPP.NamaPerusahaan));
                    paramCollection.Add(new DBParameter("@NoKontrak", oSPP.NoKontrak));

                    paramCollection.Add(new DBParameter("@WaktuPelaksanaan", oSPP.WaktuPelaksanaan));
                    paramCollection.Add(new DBParameter("@dtKontrak", oSPP.dtKontrak, DbType.Date));
                    paramCollection.Add(new DBParameter("@PPKD", oSPP.PPKD));
                    paramCollection.Add(new DBParameter("@Bulan", oSPP.Bulan));
                    paramCollection.Add(new DBParameter("@NPWP", oSPP.NoNPWP));
                    paramCollection.Add(new DBParameter("@MultiYear", oSPP.MultiYear));
                    paramCollection.Add(new DBParameter("@Bulan2", oSPP.Bulan2));
                    paramCollection.Add(new DBParameter("@NoBAST", oSPP.NoBAST));
                    
                    paramCollection.Add(new DBParameter("@JenisDocSumber", oSPP.JenisDocSumber));
                    paramCollection.Add(new DBParameter("@inokontrak", oSPP.NoKontrak));
                    //paramCollection.Add(new DBParameter("@IDSUBKegiatan", oSPP.ListIDSubKegiatan));
                    paramCollection.Add(new DBParameter("@SUmberDana", oSPP.SUmberDana));
                    paramCollection.Add(new DBParameter("@IDbank", oSPP.IDBank));
                    paramCollection.Add(new DBParameter("@iNoSPP", oSPP.iNOSPP));
                    paramCollection.Add(new DBParameter("@idBendahara", oSPP.Bendahara));
                    paramCollection.Add(new DBParameter("@iJenisGaji", oSPP.JenisGaji));
                    paramCollection.Add(new DBParameter("@inoUrutkontrak", oSPP.INoUrutKontrak));
                    paramCollection.Add(new DBParameter("@idDinas", oSPP.IDDInas));
                    
                    paramCollection.Add(new DBParameter("@btSIfatkegiatan", oSPP.SifatKegiatan));
                    paramCollection.Add(new DBParameter("@idpptk", oSPP.IDPPTK));
                    paramCollection.Add(new DBParameter("@NamaPPTK", oSPP.NamaPPTK));
                    paramCollection.Add(new DBParameter("@NIPPPTK", oSPP.NIPPPTK));
                    paramCollection.Add(new DBParameter("@sJabatanPPTK", oSPP.JabatanPPTK));
                    paramCollection.Add(new DBParameter("@NamaDlmRekeningBank", oSPP.NamaDalamRekeningBank.Replace("'","''''")));
                    paramCollection.Add(new DBParameter("@KodeBank", oSPP.KodeBank));
                    paramCollection.Add(new DBParameter("@Banyakkegiatan", oSPP.BanyakKegiatan));
                    paramCollection.Add(new DBParameter("@TahapEM", oSPP.TahapEM));
                    paramCollection.Add(new DBParameter("@RealisasiFisik", oSPP.RealisasiFisik));
                    paramCollection.Add(new DBParameter("@UnitAnggaran", oSPP.UnitAnggaran));

                    
                    paramCollection.Add(new DBParameter("@NoUrut", oSPP.NoUrut));
                    paramCollection.Add(new DBParameter("@TAhun", oSPP.Tahun));
                    paramCollection.Add(new DBParameter("@KeteranganNamaBank", oSPP.KeteranganNamaBank));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    mNoUrut = oSPP.NoUrut;

                
                }
                // SPP Rekening 

                SSQL = "DELETE tSPPRekening WHERE inourut=@NOURUT";
                DBParameterCollection paramNolkan = new DBParameterCollection();
                paramNolkan.Add(new DBParameter("@NOURUT", mNoUrut));

                _dbHelper.ExecuteNonQuery(SSQL, paramNolkan);

                foreach (SPPRekening spprekening in oSPP.Rekenings)
                {
                    SSQL= "INSERT INTO tSPPRekening (iNoUrut, btKodekategoriPelaksana, btKodeUrusanPelaksana, btKodeUK, btIDprogram, btIDkegiatan,btIDSUBKegiatan,IIDrekening, cJumlah,IDUrusan ,idProgram ,IDkegiatan, IDSUBKEGIATAN ) values ( " +  
                           " @NoURUT,@KodeKategoripelaksana,@KodeUrusanPelaksana,@KodeUK,@KodeProgram,@KodeKegiatan,@KodeSubKegiatan,@IDRekening,@Jumlah,@IDUrusan,@IDprogram,@IdKegiatan,@IDSUbKegiatan) ";
                    DBParameterCollection insertparam  = new DBParameterCollection();
                     insertparam.Add(new DBParameter("@NoURUT",mNoUrut));
                         insertparam.Add(new DBParameter("@KodeKategoripelaksana",spprekening.KodekategoriPelaksana ));
                        insertparam.Add(new DBParameter("@KodeUrusanPelaksana",spprekening.KodeUrusanPelaksana));
                        insertparam.Add(new DBParameter("@KodeUK",spprekening.UnitKerja));
                        insertparam.Add(new DBParameter("@KodeProgram",spprekening.KodeProgram  ));
                        insertparam.Add(new DBParameter("@KodeKegiatan",spprekening.KodeKegiatan));
                        insertparam.Add(new DBParameter("@KodeSubKegiatan",spprekening.KodeSubKegiatan));
                        insertparam.Add(new DBParameter("@IDRekening",spprekening.IDRekening ));
                        insertparam.Add(new DBParameter("@Jumlah", spprekening.Jumlah, DbType.Decimal));
                        insertparam.Add(new DBParameter("@IDUrusan",spprekening.IDUrusan));
                        insertparam.Add(new DBParameter("@IDprogram",spprekening.IDProgram));
                        insertparam.Add(new DBParameter("@IdKegiatan", spprekening.IDKegiatan));
                        insertparam.Add(new DBParameter("@IDSUbKegiatan", spprekening.IDSubKegiatan));

                       _dbHelper.ExecuteNonQuery(SSQL,insertparam);


                    }
                if (oSPP.Jenis == 1)
                {
                    SPJLogic spjLogic = new SPJLogic(oSPP.Tahun);
                    if ( spjLogic.KunciSPJ(DataFormat.GetLong(oSPP.NoSPJUP))== false){
                       
                        _lastError="Kesalahan mengunci LPJ";
                        _isError= true;

                    }
                }
                return mNoUrut;
            }
            catch (Exception e)
            {
                _isError = true;
                _lastError = e.Message;
                return 0;
            }
        }
        public bool SimpanPenerima(ref SPP oSPP)
        {
            try
            {
            

                SSQL = "UPDATE tSPP SET snamaPenerima=@namaPenerima,"+
                     "sAlamat =@Alamat," +
                      "sNoRek=@NoRek ,"+
                      "sNamaBank=@NamaBank,"+
                      "sJabatanPenerima=@JabatanPenerima," +
                      "sNPWP=@NPWP,"+
                      "NamaDlmRekeningBank=@NamaDlmRekeningBank ,"+
                      "KodeBank=@KodeBank  WHERE iNoUrut=@NoUrut ";

                DBParameterCollection paramCollection = new DBParameterCollection();

          

                paramCollection.Add(new DBParameter("@namaPenerima", oSPP.NamaPenerima));
                paramCollection.Add(new DBParameter("@Alamat", oSPP.Alamat));
                paramCollection.Add(new DBParameter("@NoRek", oSPP.NoRek));
                paramCollection.Add(new DBParameter("@NamaBank", oSPP.NamaBank));
                paramCollection.Add(new DBParameter("@btPenerima", oSPP.Penerima));
                paramCollection.Add(new DBParameter("@JabatanPenerima", oSPP.JabatanPenerima));
                paramCollection.Add(new DBParameter("@NPWP", oSPP.NoNPWP));
                paramCollection.Add(new DBParameter("@NamaDlmRekeningBank", oSPP.NamaDalamRekeningBank));
                paramCollection.Add(new DBParameter("@KodeBank", oSPP.KodeBank));
        



                paramCollection.Add(new DBParameter("@NoUrut", oSPP.NoUrut));
         

                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
        private List<KelengkapanSPM> GetKelengkapan(SPP oSPP)
        {
            KelengkapanSPMLogic kelLogic = new KelengkapanSPMLogic(oSPP.Tahun);
            List<KelengkapanSPM> lst = new List<KelengkapanSPM>();
            lst = kelLogic.GetByJenisSPP(oSPP.Jenis);
            if (lst != null)
                return lst;
            else
            {
                _lastError = kelLogic.LastError();
                return null;

            }

        }
        private RingkasanBelanja GetRingkasanBelanja(SPP oSPP)
        {
            RingkasanBelanja rb = new RingkasanBelanja();
            try
            {
                if (oSPP.JenisGaji == 62)
                {
                    SSQL = SSQL + "SELECT tSPP.btJenis,SUM(tsppRekening.cJumlah) as Jml FROM tSPP INNER JOIN tSPPRekening " +
                         " ON tSPP.inourut = tspprekening.inourut WHERE iTahun = " + oSPP.Tahun.ToString() +
                         " AND tSPP.IDDInas = " + oSPP.IDDInas.ToString();
                    SSQL = SSQL + "  and tSPP.btJENis>1  AND tSPP.iNourut <= " + oSPP.NoUrut.ToString();
                    SSQL = SSQL + " AND tSPP.dtSPP <= " + oSPP.dtSPP.ToSQLFormat() + " AND tSPP.bPPKD=" + oSPP.PPKD.ToString();
                    SSQL = SSQL + " AND tSPP.iStatus >=0 and tSPP.iStatus <> 9 and tSPP.iJenisGaji =  62 GROUP BY tSPP.btJEnis ";

                }
                else
                {
                    SSQL = "SELECT 1 AS btJenis,SUM(tspp.cJumlah) as Jml FROM tSPP WHERE iTahun = " + oSPP.Tahun.ToString() +
                         " AND tSPP.IDDInas = " + oSPP.IDDInas.ToString();
                    SSQL = SSQL + "  and tSPP.btJENis =0 AND tSPP.iNourut <= " + oSPP.NoUrut.ToString();
                    SSQL = SSQL + " AND tSPP.dtSPP <= " + oSPP.dtSPP.ToSQLFormat() + " AND tSPP.bPPKD=" + oSPP.PPKD.ToString();
                    SSQL = SSQL + " AND tSPP.iStatus >=0 and tSPP.iStatus <> 9 ";
                    SSQL = SSQL + "UNION ALL SELECT tSPP.btJenis,SUM(tsppRekening.cJumlah) as Jml FROM tSPP INNER JOIN tSPPRekening " +
                           " ON tSPP.inourut = tspprekening.inourut WHERE iTahun = " + oSPP.Tahun.ToString() +
                           " AND tSPP.IDDInas = " + oSPP.IDDInas.ToString();
                    SSQL = SSQL + "  and tSPP.btJENis > =1  AND tSPP.iNourut <= " + oSPP.NoUrut.ToString();
                    SSQL = SSQL + " AND tSPP.dtSPP <= " + oSPP.dtSPP.ToSQLFormat() + " AND tSPP.bPPKD=" + oSPP.PPKD.ToString();
                    SSQL = SSQL + " AND tSPP.iStatus >=0 and tSPP.iStatus <> 9  GROUP BY tSPP.btJEnis ";
                }

                IDictionary<int, decimal> dict = new Dictionary<int, decimal>();

               

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            switch (DataFormat.GetInteger(dr["btJenis"]))
                            {
                                case 1:
                                   if (oSPP.Jenis !=4)
                                    rb.JumlahGU = rb.JumlahGU + DataFormat.GetDecimal(dr["Jml"]);

                                    break;
                                case 2:
                                    if (oSPP.Jenis != 4)

                                        rb.JumlahTU = rb.JumlahTU  + DataFormat.GetDecimal(dr["Jml"]);
                                    break;
                                case 3:
                                    if (oSPP.Jenis != 4)
                                   
                                         rb.JumlahLS = rb.JumlahLS + DataFormat.GetDecimal(dr["Jml"]);
                                    break;
                                case 4:
                                    if (oSPP.Jenis == 4)

                                        rb.JumlahGaji = rb.JumlahGaji  + DataFormat.GetDecimal(dr["Jml"]);
                                    break;
                                case 5:
                                    if (oSPP.Jenis != 4)

                                        rb.JumlahPPKD = rb.JumlahPPKD  + DataFormat.GetDecimal(dr["Jml"]);
                                    break;



                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;


            }      
            return rb;
            
            

        }

    }
}
