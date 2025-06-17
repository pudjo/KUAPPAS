using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using DataAccess;
using DTO.Bendahara;
using Formatting;
using BP;

namespace BP.Bendahara
{
    public class STSLogic:BP 
    {
        IDbConnection m_connection;
        IDbTransaction m_objTrans;

        public STSLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tSTS";
        //    PerbaikiTable();
        }



        public bool BersihkanBKUPendapatanImport(int iddinas, string namaSheet)
        {
            try
            {

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@NAMAFILE", namaSheet, DbType.String));
                paramCollection.Add(new DBParameter("@DINAS", iddinas, DbType.String));

                SSQL = "DELETE tSTSREkening " +
                     " from tSTSrekening inner join tSTS on tSTSrekening.inourut = tSTS.inourut " +
                     " where tSTS.IDDInas=@DINAS and tSTS.namafile = @NAMAFILE";
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                
                SSQL = "DELETE tSTS where IDDInas=@DINAS and namafile = @NAMAFILE";

                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                return true;

            }
            catch (Exception ex)
            {
                _lastError = "Kesalahan membersihkan data BKU Imporet Pendapatan " + ex.Message;

                return false;
            }
        }

       

        public bool BKUKanSTS(STS sts, int IDDinas)
        {
            string messageError = "";
            bool berhasilBKU = true;
            try
            {
                long _lNoUrut = 0L;

                List<BKU> lstBKU = new List<BKU>();
                BKU MaxNoBKU = new BKU();

                BKU MaxNoBKUBUD = new BKU();

                BKULogic oBKULogic = new BKULogic(Tahun);
                List<long> lstNoUrut = new List<long>();
                lstNoUrut.Add(sts.NoUrut);
    

          

            

                lstBKU = oBKULogic.GetBKUByNoUrutSumber(lstNoUrut, -1);
      




                
                MaxNoBKU = oBKULogic.GetBKUDenganMaxNoBKU(IDDinas, 0,1);
                MaxNoBKUBUD = oBKULogic.GetBKUDenganMaxNoBKU(IDDinas, 0, 0);
                if (MaxNoBKU == null)
                {

                    _lastError = "Kesahan mengambil informasi BKU..";
                    return false;

                }
                lstBKU = oBKULogic.GetBKUByNoUrutSumber(lstNoUrut,1);
             
               
                int jenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_TSTS;
             
                Console.WriteLine(sts.NoUrut.ToString());
                if (sts.NoUrut == 245029901100381)
                {
                        sts.NoUrut = 245029901100381;
                }
                        bool berhasilBKUPerSTS = true;
                        _isError = false;
                        _lastError = "";

                        m_connection = _dbHelper.CreateCOnnection();
                        m_objTrans = m_connection.BeginTransaction();

                        if (SimpanBKU(sts, 1, lstBKU, ref MaxNoBKU, jenisSumber,
                                          m_connection, m_objTrans) == false)
                        {

                            berhasilBKUPerSTS = berhasilBKUPerSTS && false;
                            messageError = messageError + " " + sts.NoSTS;

                        }
                        else
                        {
                            if (sts.Jenis == 1)
                            {

                                if (SimpanBKU(sts, -1, lstBKU, ref MaxNoBKU, jenisSumber,
                                              m_connection, m_objTrans) == false)
                                {
                                    berhasilBKUPerSTS = berhasilBKUPerSTS && false;
                                    messageError = messageError + " " + sts.NoSTS;
                                }


                               

                            }
                        }
                        if (berhasilBKUPerSTS == true)
                        {


                            m_objTrans.Commit();
                            m_connection.Close();
                        }
                        else
                        {
                            m_objTrans.Rollback();
                            m_connection.Close();
                        }
                berhasilBKU = berhasilBKU && berhasilBKUPerSTS;
                
                if (berhasilBKU== false ){
                    _isError = true;
                    _lastError=_lastError = messageError = messageError + " Gagal BKU ";

                } 
                return berhasilBKU;
            }
            catch (Exception ex)
            {
                _isError =true;
                _lastError = messageError = messageError + " Gagal BKU ";
                return false;

            }
        }
        public List<STS> GetPenerimaanDiKasda(int dinas,
            DateTime tanggalAwal, DateTime tanggalAkhir)
        {
            try
            {
                List<STS> lst = new List<STS>();

                SSQL = "Select 1 as Jenis ,tSetor.Iddinas, tSetor.btKodeUK,tSetor.inourutKasda, tSetor.inourut,mSKPD.sNamaSKPD, tSetor.sNoBukti,tsetor.dtBukukas, " +
                     " tSetor.sKeterangan, tsetor.cJumlah, iStatus from tsetor INNER JOIN mSKPD ON tSetor.iddinas = mSKPD.id " +
                     " WHERE tSetor.dtbukukas between " + tanggalAwal.ToSQLFormat() + " AND " + tanggalAkhir.ToSQLFormat() + " and tSetor.BTjENIS in (" + ((int)E_JENIS_SETOR.E_SETOR_STS).ToString() + ")";

                if (dinas > 0)
                {
                    SSQL = SSQL + " AND tSetor.iddinas = " + dinas.ToString();

                }
                SSQL = SSQL + " Union All ";
                SSQL = SSQL + " Select 3 as Jenis ,tSetor.iddinas, tSetor.btKodeUK,tSetor.inourutKasda, tSetor.inourut,mSKPD.sNamaSKPD, tSetor.sNoBukti,tsetor.dtBukukas, " +
                    " tSetor.sKeterangan, tsetor.cJumlah, iStatus from tsetor INNER JOIN mSKPD ON tSetor.iddinas= mskpd.id " +
                        "  WHERE tSetor.dtbukukas between " + tanggalAwal.ToSQLFormat() + " AND " + tanggalAkhir.ToSQLFormat() + " and tSetor.BTjENIS in (" + ((int)E_JENIS_SETOR.E_SETOR_CP).ToString() + "," + ((int)E_JENIS_SETOR.E_SETOR_UYHD).ToString() + ")";
                if (dinas > 0)
                {
                    SSQL = SSQL + " AND tSetor.iddinas = " + dinas.ToString();
                }
                SSQL = SSQL + " Union All ";

                SSQL = SSQL + "  select  2 as Jenis ,tSTS.IDDinas, tSTS.btKodeUK,tSTS.inourutKasda, tSTS.inourut,mSKPD.sNamaSKPD, tsts.sNoSTS as sNoBukti,tSTS.dtSTS as dtBukukas, " +
                    " tSTS.sKeterangan, tSTS.cJumlah, iStatus from tSTS INNER JOIN mSKPD ON tSTS.iddinas =mSKPD.id WHERE tSTS.dtsts between " + tanggalAwal.ToSQLFormat() + " AND " + tanggalAkhir.ToSQLFormat() + "  and tSTS.BTjENIS = 1";

                if (dinas > 0)
                {
                    SSQL = SSQL + " AND tSTS.iddinas = " + dinas.ToString();

                }

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new STS()
                               {

                                   NoUrut = DataFormat.GetLong(dr["inourut"]),
                                   Jenis = DataFormat.GetInteger(dr["Jenis"]),
                                   NoUrutKasda = DataFormat.GetInteger(dr["inourutKasda"]),
                                   KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                   NamaSKPD = DataFormat.GetString(dr["sNamaSKPD"]),
                                   IDDinas = DataFormat.GetInteger(dr["IDDINAS"]),


                                   NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                   Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                   dtBukuKas = DataFormat.GetDateTime(dr["dtBukukas"]),
                                   Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                   Status = DataFormat.GetInteger(dr["iStatus"]),

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


        public bool UbahKeBank(long noUrutSumber,
                               int bank)
        {
            try
            {

                SSQL = "Update tSTS set iBank= " + bank.ToString() + ", btJenis = " + bank.ToString() + " where inourut= " + noUrutSumber.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);

                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }

        }

        public long Simpan(STS sts)
        {
            long _lNoUrut = 0L;

            List<BKU> lstBKU = new List<BKU>();
            BKU MaxNoBKU = new BKU();
            BKU MaxNoBKUBUD = new BKU();

            BKULogic oBKULogic = new BKULogic(Tahun);
            List<long> lstNoUrut = new List<long>();
            lstNoUrut.Add(sts.NoUrut);

            lstBKU = oBKULogic.GetBKUByNoUrutSumber(lstNoUrut,-1);
            MaxNoBKU = oBKULogic.GetBKUDenganMaxNoBKU(sts.IDDinas, sts.KodeUK,1);
            MaxNoBKUBUD = oBKULogic.GetBKUDenganMaxNoBKU(sts.IDDinas, sts.KodeUK, 0);
            
            m_connection = _dbHelper.CreateCOnnection();
            m_objTrans = m_connection.BeginTransaction();



            try {
            DBParameterCollection paramCollection = new DBParameterCollection();                

            if (sts.NoUrut == 0)
            {
        
                _lNoUrut = ReadNo(E_KOLOM_NOURUT.CON_URUT_STS ,sts.IDDinas );

    
              SSQL  = "INSERT INTO tSTS (iTahun, IDDInas, btKodeKategori, btKodeUrusan, btKodeSKPD, btKodeUK, " + 
                  "iNoUrut, sNoSTS, dtSTS, sNamaBank, sNoRekening, sNoBukti, dtBukuKas, sKeterangan, " + 
                  "cJumlah, iStatus, btJenis, sPenyetor, sAlamat, sInstitusiPenyetor, sJabatanPenyetor, " +
                  "iBankBUD, bPPKD,  iNoSKR, iBank,NamaFile,idcrt, dcrt) " +
                  "VALUES  ( @piTahun, @pIDDInas,@pbtKodeKategori, @pbtKodeUrusan, @pbtKodeSKPD, @pbtKodeUK, " + 
                  "@piNoUrut, @psNoSTS, @pdtSTS, @psNamaBank, @psNoRekening, @psNoBukti, @pdtBukuKas, @psKeterangan, " + 
                  "@pcJumlah, @piStatus, @pbtJenis, @psPenyetor, @psAlamat, @psInstitusiPenyetor, @psJabatanPenyetor, " +
                  "@piBankBUD, @pbPPKD,  @piNoSKR, @piBank,@Namafile,@idcrt, @dcrt)";


              
                paramCollection.Add(new DBParameter("@piTahun",sts.Tahun,DbType.Int32));

                paramCollection.Add(new DBParameter("@pbtKodeKategori", sts.KodeKategori, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDDInas", sts.IDDinas, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtKodeUrusan", sts.KodeUrusan, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtKodeSKPD", sts.KodeSKPD, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtKodeUK", sts.KodeUK, DbType.Int32));
                paramCollection.Add(new DBParameter("@piNoUrut", _lNoUrut, DbType.Int64));
                paramCollection.Add(new DBParameter("@psNoSTS", sts.NoSTS, DbType.String));
                paramCollection.Add(new DBParameter("@pdtSTS",sts.TanggalSTS, DbType.Date ));
                paramCollection.Add(new DBParameter("@psNamaBank", sts.NamaBank, DbType.String));
                paramCollection.Add(new DBParameter("@psNoRekening", sts.NoRekening, DbType.String));
                paramCollection.Add(new DBParameter("@psNoBukti",sts.NoBukti,DbType.String));
                paramCollection.Add(new DBParameter("@pdtBukuKas", sts.TanggalSTS, DbType.Date));
                paramCollection.Add(new DBParameter("@psKeterangan",sts.Keterangan,DbType.String));
                 paramCollection.Add(new DBParameter("@pcJumlah",sts.Jumlah,DbType.Decimal));
                paramCollection.Add(new DBParameter("@piStatus",sts.Status,DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtJenis",sts.Jenis,DbType.Int32)); 
                paramCollection.Add(new DBParameter("@psPenyetor",sts.Penyetor)); 
                paramCollection.Add(new DBParameter("@psAlamat",sts.Alamat,DbType.String));
                paramCollection.Add(new DBParameter("@psInstitusiPenyetor",sts.InstitusiPenyetor,DbType.String)); 
                paramCollection.Add(new DBParameter("@psJabatanPenyetor",sts.JabatanPenyetor));
                paramCollection.Add(new DBParameter("@piBankBUD",sts.BankBUD,DbType.Int32));
                paramCollection.Add(new DBParameter("@pbPPKD",sts.PPKD,DbType.Int32));  
                paramCollection.Add(new DBParameter("@piNoSKR",sts.NoSKR,DbType.Int64)); 
                paramCollection.Add(new DBParameter("@piBank",sts.Bank,DbType.Int32));
                paramCollection.Add(new DBParameter("@Namafile", sts.NamaFile,DbType.String));

                paramCollection.Add(new DBParameter("@idcrt", sts.idcrt,DbType.Int32));
                paramCollection.Add(new DBParameter("@dcrt", DateTime.Now.Date, DbType.Date));
                sts.NoUrut = _lNoUrut;
            } else {
                _lNoUrut = sts.NoUrut;

                  SSQL  = "UPDATE tSTS SET sNoSTS=@psNoSTS, dtSTS=@pdtSTS, sNamaBank=@psNamaBank, sNoRekening=@psNoRekening, sNoBukti=@psNoBukti," +
                       "dtBukuKas=@pdtBukuKas, sKeterangan=@psKeterangan, " +
                  "cJumlah=@pcJumlah,btJenis=@pbtJenis, iStatus=@piStatus,  sPenyetor=@psPenyetor, sAlamat=@psAlamat, sInstitusiPenyetor=@psInstitusiPenyetor, " +
                  "sJabatanPenyetor=@psJabatanPenyetor, iBankBUD=@piBankBUD, bPPKD=@pbPPKD,  iNoSKR=@piNoSKR, iBank=@piBank WHERE iNoUrut=@piNoUrut";
                
              paramCollection.Add(new DBParameter("@psNoSTS",sts.NoSTS));
                paramCollection.Add(new DBParameter("@pdtSTS",sts.TanggalSTS,DbType.Date ));
                paramCollection.Add(new DBParameter("@psNamaBank",sts.NamaBank));
                paramCollection.Add(new DBParameter("@psNoRekening",sts.NoRekening));
                paramCollection.Add(new DBParameter("@psNoBukti",sts.NoBukti));
                paramCollection.Add(new DBParameter("@pdtBukuKas", sts.TanggalSTS, DbType.Date));
                paramCollection.Add(new DBParameter("@psKeterangan",sts.Keterangan));
                 paramCollection.Add(new DBParameter("@pcJumlah",sts.Jumlah,DbType.Decimal));
                paramCollection.Add(new DBParameter("@piStatus",sts.Status));
                paramCollection.Add(new DBParameter("@pbtJenis",sts.Jenis)); 
                paramCollection.Add(new DBParameter("@psPenyetor",sts.Penyetor)); 
                paramCollection.Add(new DBParameter("@psAlamat",sts.Alamat));
                paramCollection.Add(new DBParameter("@psInstitusiPenyetor",sts.InstitusiPenyetor)); 
                paramCollection.Add(new DBParameter("@psJabatanPenyetor",sts.JabatanPenyetor));
                paramCollection.Add(new DBParameter("@piBankBUD",sts.BankBUD));
                paramCollection.Add(new DBParameter("@pbPPKD",sts.PPKD));  
                paramCollection.Add(new DBParameter("@piNoSKR",sts.NoSKR)); 
                paramCollection.Add(new DBParameter("@piBank",sts.Bank));
                paramCollection.Add(new DBParameter("@piNoUrut",_lNoUrut));


            }
             

            //_dbHelper.ExecuteNonQuery(SSQL,paramCollection);
            _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);
            
            SSQL = "DELETE tSTSRekening WHERE inoUrut = " + _lNoUrut.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);
            foreach (STSRekening sr in sts.Rekenings)
            {
                SSQL = "INSERT INTO tSTSRekening (inourut, IIDRekening, cJumlah) values ( @pinourut, @pIIDRekening, @pcJumlah )";
                DBParameterCollection paramCollectionRek = new DBParameterCollection();
                paramCollectionRek.Add(new DBParameter("@pinourut", _lNoUrut));
                paramCollectionRek.Add(new DBParameter("@pIIDRekening", sr.IDRekening));
                paramCollectionRek.Add(new DBParameter("@pcJumlah", sr.Jumlah,DbType.Decimal));
                _dbHelper.ExecuteNonQuery(SSQL, paramCollectionRek, m_connection, m_objTrans);
            }
            // SImpan BKU 
            int jenisSumber = 0;
          
            jenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_TSTS;
            bool berhasilSImpanBKU = true;
            if (SimpanBKU(sts, 1, lstBKU, ref MaxNoBKU, jenisSumber,
                              m_connection, m_objTrans) == false)
            {
                berhasilSImpanBKU = berhasilSImpanBKU && false;
            }
            if (sts.Jenis == 1)
            {

                if (SimpanBKU(sts, -1, lstBKU, ref MaxNoBKU, jenisSumber,
                              m_connection, m_objTrans) == false)
                {
                    berhasilSImpanBKU = berhasilSImpanBKU && false;
                }


               
                  
            }
            
            if ( berhasilSImpanBKU == true){
                m_objTrans.Commit();
                m_connection.Close();
            }
            else
            {
                m_objTrans.Rollback();
                m_connection.Close();
            }
          
             return _lNoUrut;

            } catch (Exception ex){
                _isError = true;
                _lastError = ex.Message;
                m_objTrans.Rollback();
                return 0;

            }
            
        }
        public BKU GetOldBKU(Setor setor, List<BKU> lstBKU,
                               E_JENISBENDAHARA JenisBendahara, E_JENIS_REFERENSIBKU JenisSumber, int debet)
        {

            BKU oldBKU = lstBKU.FirstOrDefault(b => b.NourutSumber == setor.NoUrut &&
                                                        b.JenisBendahara == JenisBendahara &&
                                                        b.JenisSumber == (int)JenisSumber &&
                                                        b.Debet == debet);

            return oldBKU;


        }
        private bool SimpanBKU(STS sts, int debet, List<BKU> lstBKU, ref BKU MaxNoBKU, int JenisSumber,
                              IDbConnection connection, IDbTransaction odbTrans)
        {
            try
            { // BKU 
                BKU oBKU = new BKU();

                BKULogic oBKULogic = new BKULogic(Tahun);
                oBKU.CreateFormSTS(sts,debet , JenisSumber, (int)E_JENISBENDAHARA.BENDAHARA_PENERIMAAN);
               
          

                 BKU oldBKU = lstBKU.FirstOrDefault(b => b.NourutSumber == sts.NoUrut &&
                                                       b.JenisBendahara ==E_JENISBENDAHARA.BENDAHARA_PENERIMAAN &&
                                                       b.JenisSumber == (int)JenisSumber &&
                                                       b.Debet == debet);


                 oBKU.JenisBendahara = E_JENISBENDAHARA.BENDAHARA_PENERIMAAN;

                 if (oldBKU != null)
                 {
                        oBKU.NoUrut = oldBKU.NoUrut;
                        oBKU.NoBKU = oldBKU.NoBKU;
                        oBKU.NoBKUSKPD = oldBKU.NoBKUSKPD;

                 }
                 else
                 {
                     MaxNoBKU.NoBKU++;
                     MaxNoBKU.NoBKUSKPD++;
                     MaxNoBKU.NoUrutSaja++;

                        oBKU.NoUrut = 0;
                        oBKU.NoBKU = MaxNoBKU.NoBKU ;
                        oBKU.NoBKUSKPD = MaxNoBKU.NoBKUSKPD ;
                        oBKU.NoUrutSaja = MaxNoBKU.NoUrutSaja ;

                 }
                 if ( oBKULogic.Simpan(ref oBKU, connection, odbTrans)== true )
                      return true;
                 else
                     return false ;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return false;
            }
        }
        private bool SimpanBKUBUD(STS sts, int debet, List<BKU> lstBKU, ref BKU MaxNoBKU, int noBKU, int JenisSumber,
                              IDbConnection connection, IDbTransaction odbTrans)
        {
            try
            { // BKU 
                BKU oBKU = new BKU();

                BKULogic oBKULogic = new BKULogic(Tahun);
                oBKU.CreateFormSTS(sts, debet, JenisSumber, (int)E_JENISBENDAHARA.BENDAHARA_BUD);

                if (oBKU.NourutSumber == 0)
                {
                    return false;
                }

                BKU oldBKU = lstBKU.FirstOrDefault(b => b.NourutSumber == sts.NoUrut &&
                                                      b.JenisBendahara == (int)E_JENISBENDAHARA.BENDAHARA_BUD &&
                                                      b.JenisSumber == (int)JenisSumber &&
                                                      b.Debet == debet);


                oBKU.JenisBendahara = (int)E_JENISBENDAHARA.BENDAHARA_BUD;

                if (oldBKU != null)
                {
                    oBKU.NoUrut = oldBKU.NoUrut;
                    oBKU.NoBKU = noBKU;
                    oBKU.NoBKUSKPD = noBKU;

                }
                else
                {
                
              
                    MaxNoBKU.NoUrutSaja++;

                    oBKU.NoUrut = 0;
                    oBKU.NoBKU = noBKU;
                    oBKU.NoBKUSKPD = noBKU;
                    oBKU.NoUrutSaja = MaxNoBKU.NoUrutSaja;

                }
                if (oBKULogic.Simpan(ref oBKU, connection, odbTrans) == true)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return false;
            }
        }
        public List<STS> GetDataImport(int iddinas, string namaSheet)
        {
            List<STS> _lst = new List<STS>();
            try
            {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@NAMAFILE", namaSheet, DbType.String));
                    paramCollection.Add(new DBParameter("@DINAS", iddinas, DbType.String));

            
                    SSQL = "Select * from tSTS where IDDInas=@DINAS and namafile = @NAMAFILE";
                    DataTable dt = new DataTable();
                    dt = _dbHelper.ExecuteDataTable(SSQL,paramCollection);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            _lst = (from DataRow dr in dt.Rows
                                    select new STS()
                                    {

                                        NoUrut = DataFormat.GetLong(dr["inourut"]),
                                        Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                        KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                        KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                        KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                        KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                        NoSTS = DataFormat.GetString(dr["sNoSTS"]),
                                        IDDinas = DataFormat.GetInteger(dr["IDDINAS"]),

                                        TanggalSTS = DataFormat.GetDateTime(dr["dtSTS"]),
                                        NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                        dtBukuKas = DataFormat.GetDateTime(dr["dtBukukas"]),
                                        Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                        Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                        NamaBank = DataFormat.GetString(dr["sNamaBank"]),
                                        NoRekening = DataFormat.GetString(dr["sNoRekening"]),
                                        Status = DataFormat.GetSingle(dr["iStatus"]),
                                        Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                        Penyetor = DataFormat.GetString(dr["sPenyetor"]),
                                        Alamat = DataFormat.GetString(dr["sAlamat"]),
                                        JabatanPenyetor = DataFormat.GetString(dr["sJabatanPenyetor"]),
                                        PPKD = DataFormat.GetInteger(dr["bppkd"]),
                                        dtInput = DataFormat.GetDateTime(dr["dtInput"]),
                                        BankBUD = DataFormat.GetInteger(dr["iBankBUD"]),
                                        NPWP = DataFormat.GetString(dr["sNPWP"]),
                                        NoSKR = DataFormat.GetLong(dr["inoskr"]),
                                        StatusJurnal = DataFormat.GetInteger(dr["iStatusJurnal"]),
                                        Bank = DataFormat.GetInteger(dr["iBank"]),
                                        Rekenings = GetDetail(DataFormat.GetLong(dr["inourut"]))

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
        public List<STS> GetByDinas( int _idDinas)
          {
              List<STS> _lst = new List<STS>();
              try
              {
                  SSQL = "SELECT tSTS.* FROM tSTS WHERE IDDInas =" + _idDinas.ToString() + " Order by inourut";//
                  DataTable dt = new DataTable();
                  dt = _dbHelper.ExecuteDataTable(SSQL);
                  if (dt != null)
                  {
                      if (dt.Rows.Count > 0)
                      {
                          _lst = (from DataRow dr in dt.Rows
                                  select new STS()
                                  {

                                      NoUrut = DataFormat.GetLong(dr["inourut"]),
                                      Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                      KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                      KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                      KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                      KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                      NoSTS = DataFormat.GetString(dr["sNoSTS"]),
                                      IDDinas = DataFormat.GetInteger(dr["IDDINAS"]),

                                      TanggalSTS = DataFormat.GetDateTime(dr["dtSTS"]),
                                      NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                      dtBukuKas = DataFormat.GetDateTime(dr["dtBukukas"]),
                                      Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                      Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                      NamaBank = DataFormat.GetString(dr["sNamaBank"]),
                                      NoRekening = DataFormat.GetString(dr["sNoRekening"]),
                                      Status = DataFormat.GetSingle(dr["iStatus"]),
                                      Jenis  = DataFormat.GetInteger(dr["btJenis"]),
                                      Penyetor = DataFormat.GetString(dr["sPenyetor"]),
                                      Alamat = DataFormat.GetString(dr["sAlamat"]),
                                      JabatanPenyetor = DataFormat.GetString(dr["sJabatanPenyetor"]),
                                      PPKD = DataFormat.GetInteger(dr["bppkd"]),
                                      dtInput = DataFormat.GetDateTime(dr["dtInput"]),
                                      BankBUD = DataFormat.GetInteger(dr["iBankBUD"]),
                                      NPWP = DataFormat.GetString(dr["sNPWP"]),
                                      NoSKR = DataFormat.GetLong(dr["inoskr"]),
                                      StatusJurnal = DataFormat.GetInteger(dr["iStatusJurnal"]),
                                      Bank = DataFormat.GetInteger(dr["iBank"]),
                                      idcrt = DataFormat.GetInteger(dr["iBank"]),
                                      tcrt = DataFormat.GetDate(dr["dcrt"]),
                                      Rekenings = GetDetail(DataFormat.GetLong(dr["inourut"]))

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
        public List<STS> GetByDinas(int _idDinas, DateTime tanggalAwal, DateTime tanggalAkhir, int jenis)
        {
            List<STS> _lst = new List<STS>();
            try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();

                SSQL = "SELECT tSTS.* FROM tSTS WHERE IDDInas =@DINAS AND dtSTS between @TANGGALAWAL " +
                    " AND @TANGGALAKHIR ";//

                paramCollection.Add(new DBParameter("@DINAS", _idDinas, DbType.Int32));
                paramCollection.Add(new DBParameter("@TANGGALAWAL", tanggalAwal, DbType.Date));
                paramCollection.Add(new DBParameter("@TANGGALAKHIR", tanggalAkhir, DbType.Date));

                if (jenis > -1)
                {
                    SSQL = SSQL + " AND btJenis =@JENIS";
                    paramCollection.Add(new DBParameter("@JENIS", jenis, DbType.Int32));
                }
                else
                {
                    SSQL = SSQL + " AND btJenis <=1";


                }

                SSQL = SSQL + " Order by  dtSTS,inourut ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new STS()
                                {

                                    NoUrut = DataFormat.GetLong(dr["inourut"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    NoSTS = DataFormat.GetString(dr["sNoSTS"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDINAS"]),

                                    TanggalSTS = DataFormat.GetDateTime(dr["dtSTS"]),
                                    NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                    dtBukuKas = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    NamaBank = DataFormat.GetString(dr["sNamaBank"]),
                                    NoRekening = DataFormat.GetString(dr["sNoRekening"]),
                                    Status = DataFormat.GetSingle(dr["iStatus"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    Penyetor = DataFormat.GetString(dr["sPenyetor"]),
                                    Alamat = DataFormat.GetString(dr["sAlamat"]),
                                    JabatanPenyetor = DataFormat.GetString(dr["sJabatanPenyetor"]),
                                    PPKD = DataFormat.GetInteger(dr["bppkd"]),
                                    dtInput = DataFormat.GetDateTime(dr["dtInput"]),
                                    BankBUD = DataFormat.GetInteger(dr["iBankBUD"]),
                                    NPWP = DataFormat.GetString(dr["sNPWP"]),
                                    NoSKR = DataFormat.GetLong(dr["inoskr"]),
                                    StatusJurnal = DataFormat.GetInteger(dr["iStatusJurnal"]),
                                    Bank = DataFormat.GetInteger(dr["iBank"]),
                                    idcrt = DataFormat.GetInteger(dr["idcrt"]),
                                    tcrt = DataFormat.GetDate(dr["dcrt"]),
                                    NoUrutSetor = DataFormat.GetLong(dr["inourutSetor"]),
                                    NamaFile = DataFormat.GetString(dr["NamaFile"]),

                                    // Rekenings = GetDetail(DataFormat.GetLong(dr["inourut"]))

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
        public List<STS> GetByNamaFile(string namaFIle)
        {
            List<STS> _lst = new List<STS>();
            try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();

                SSQL = "SELECT tSTS.* FROM tSTS WHERE NamaFile =@NAMAFILE";//

                paramCollection.Add(new DBParameter("@NAMAFILE", namaFIle));
                
                SSQL = SSQL + " Order by  dtSTS,inourut ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new STS()
                                {

                                    NoUrut = DataFormat.GetLong(dr["inourut"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    NoSTS = DataFormat.GetString(dr["sNoSTS"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDINAS"]),

                                    TanggalSTS = DataFormat.GetDateTime(dr["dtSTS"]),
                                    NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                    dtBukuKas = DataFormat.GetDateTime(dr["dtBukukas"]),
                                    Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    NamaBank = DataFormat.GetString(dr["sNamaBank"]),
                                    NoRekening = DataFormat.GetString(dr["sNoRekening"]),
                                    Status = DataFormat.GetSingle(dr["iStatus"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    Penyetor = DataFormat.GetString(dr["sPenyetor"]),
                                    Alamat = DataFormat.GetString(dr["sAlamat"]),
                                    JabatanPenyetor = DataFormat.GetString(dr["sJabatanPenyetor"]),
                                    PPKD = DataFormat.GetInteger(dr["bppkd"]),
                                    dtInput = DataFormat.GetDateTime(dr["dtInput"]),
                                    BankBUD = DataFormat.GetInteger(dr["iBankBUD"]),
                                    NPWP = DataFormat.GetString(dr["sNPWP"]),
                                    NoSKR = DataFormat.GetLong(dr["inoskr"]),
                                    StatusJurnal = DataFormat.GetInteger(dr["iStatusJurnal"]),
                                    Bank = DataFormat.GetInteger(dr["iBank"]),
                                    idcrt = DataFormat.GetInteger(dr["idcrt"]),
                                    tcrt = DataFormat.GetDate(dr["dcrt"]),
                                    NoUrutSetor = DataFormat.GetLong(dr["inourutSetor"]),


                                   // Rekenings = GetDetail(DataFormat.GetLong(dr["inourut"]))

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
        public List<FileSTS> GetNamaFileByDinas(int _idDinas, DateTime tanggalAwal, DateTime tanggalAkhir)
            // import data ada kolom nama file
            // untuk cek data import
        {
            List<FileSTS> _lst = new List<FileSTS>();
            try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();

                SSQL = "SELECT distinct tSTS.NamaFile  FROM tSTS WHERE IDDInas =@DINAS AND dtSTS between @TANGGALAWAL " +
                    " AND @TANGGALAKHIR ";//

                paramCollection.Add(new DBParameter("@DINAS", _idDinas, DbType.Int32));
                paramCollection.Add(new DBParameter("@TANGGALAWAL", tanggalAwal, DbType.Date));
                paramCollection.Add(new DBParameter("@TANGGALAKHIR", tanggalAkhir, DbType.Date));


                SSQL = SSQL + " Order by  NamaFile";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new FileSTS()
                                {
                                    NamaFIle = DataFormat.GetString(dr["NamaFIle"]),
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
          public List<STS> GetByDinasToJurnal(int _idDinas, DateTime tanggalAwal, DateTime tanggalAkhir, int jenis )
          {
              List<STS> _lst = new List<STS>();
              try
              {
               //DBParameterCollection paramCollection = new DBParameterCollection();


               //SSQL = "SELECT IDDINAS,iNoUrut,dtSTS,sNoSTS,sKeterangan, sPenyetor,cJumlah, btJenis,inoskr, dbo.IsInJurnal(tSTS.inourut) as InJurnal FROM tSTS WHERE IDDInas =@DINAS AND dtSTS between @TANGGALAWAL " +
               //       " AND @TANGGALAKHIR ";//

               //   paramCollection.Add(new DBParameter("@DINAS", _idDinas, DbType.Int32));
               //   paramCollection.Add(new DBParameter("@TANGGALAWAL", tanggalAwal,DbType.Date));
               //   paramCollection.Add(new DBParameter("@TANGGALAKHIR", tanggalAkhir, DbType.Date));

                  SSQL = "SELECT IDDINAS,iNoUrut,dtSTS,sNoSTS,sKeterangan, sPenyetor,tSTS.cJumlah, btJenis,inoskr, " +
                      " vwJurnalRekeningAnggaran.KodeSKPD as InJurnal FROM tSTS  LEFT JOIN vwJurnalRekeningAnggaran "+
                      " ON tSTS.inourut = vwJurnalRekeningAnggaran.iSumber  WHERE tSTS.IDDInas = " + _idDinas.ToString() +
                      " AND tSTS.dtSTS between " + tanggalAwal.ToSQLFormat() + " AND " + tanggalAkhir.ToSQLFormat();



                  if (jenis > -1)
                  {
                      //SSQL = SSQL + " AND btJenis =@JENIS";
                      //paramCollection.Add(new DBParameter("@JENIS", jenis, DbType.Int32));
                      SSQL = SSQL + " AND btJenis =" + jenis.ToString();
                      //paramCollection.Add(new DBParameter("@JENIS", jenis, DbType.Int32));
                  }
                  else
                  {
                      SSQL = SSQL + " AND btJenis <=1";
                      

                  }

                  SSQL=SSQL +" Order by  dtSTS,inourut ";

                  DataTable dt = new DataTable();
                  //dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                  
                  dt = _dbHelper.ExecuteDataTable(SSQL);
                  if (dt != null)
                  {
                      if (dt.Rows.Count > 0)
                      {
                          _lst = (from DataRow dr in dt.Rows
                                  select new STS()
                                  {

                                      NoUrut = DataFormat.GetLong(dr["inourut"]),
                                      //Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                      //KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                      //KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                      //KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                      //KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                      NoSTS = DataFormat.GetString(dr["sNoSTS"]),
                                      IDDinas = DataFormat.GetInteger(dr["IDDINAS"]),

                                      TanggalSTS = DataFormat.GetDateTime(dr["dtSTS"]),
                                      ///NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                      //dtBukuKas = DataFormat.GetDateTime(dr["dtBukukas"]),
                                      Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                      Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                      //NamaBank = DataFormat.GetString(dr["sNamaBank"]),
                                      //NoRekening = DataFormat.GetString(dr["sNoRekening"]),
                                      //Status = DataFormat.GetSingle(dr["iStatus"]),
                                      Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                      Penyetor = DataFormat.GetString(dr["sPenyetor"]),
                                    //  Alamat = DataFormat.GetString(dr["sAlamat"]),
                                      //JabatanPenyetor = DataFormat.GetString(dr["sJabatanPenyetor"]),
                                     // PPKD = DataFormat.GetInteger(dr["bppkd"]),
                                     // dtInput = DataFormat.GetDateTime(dr["dtInput"]),
                                     // BankBUD = DataFormat.GetInteger(dr["iBankBUD"]),
                                     // NPWP = DataFormat.GetString(dr["sNPWP"]),
                                      NoSKR = DataFormat.GetLong(dr["inoskr"]),
                                     // StatusJurnal = DataFormat.GetInteger(dr["iStatusJurnal"]),
                                      //Bank = DataFormat.GetInteger(dr["iBank"]),
                                      //idcrt = DataFormat.GetInteger(dr["idcrt"]),
                                      //tcrt= DataFormat.GetDate (dr["dcrt"]),
                                      //NoUrutSetor = DataFormat.GetLong(dr["inourutSetor"]),

                                      Status = DataFormat.GetInteger(dr["InJurnal"])
                                    //  Rekenings = GetDetail(DataFormat.GetLong(dr["inourut"]))
          
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
          public List<STS> GetPenerimaanBOSBLUD(int _idDinas, DateTime tanggalAwal, DateTime tanggalAkhir, int jenis)
          {
              List<STS> _lst = new List<STS>();
              try
              {
                  DBParameterCollection paramCollection = new DBParameterCollection();

                  SSQL = "SELECT tSTS.* FROM tSTS WHERE IDDInas =@DINAS AND dtSTS between @TANGGALAWAL " +
                      " AND @TANGGALAKHIR ";//

                  paramCollection.Add(new DBParameter("@DINAS", _idDinas, DbType.Int32));
                  paramCollection.Add(new DBParameter("@TANGGALAWAL", tanggalAwal, DbType.Date));
                  paramCollection.Add(new DBParameter("@TANGGALAKHIR", tanggalAkhir, DbType.Date));

                  
                  SSQL = SSQL + " AND btJenis =@JENIS";
                  paramCollection.Add(new DBParameter("@JENIS", jenis, DbType.Int32));
                  

                  SSQL = SSQL + " Order by inourut ";

                  DataTable dt = new DataTable();
                  dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                  if (dt != null)
                  {
                      if (dt.Rows.Count > 0)
                      {
                          _lst = (from DataRow dr in dt.Rows
                                  select new STS()
                                  {

                                      NoUrut = DataFormat.GetLong(dr["inourut"]),
                                      Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                      KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                      KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                      KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                      KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                      NoSTS = DataFormat.GetString(dr["sNoSTS"]),
                                      IDDinas = DataFormat.GetInteger(dr["IDDINAS"]),

                                      TanggalSTS = DataFormat.GetDateTime(dr["dtSTS"]),
                                      NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                      dtBukuKas = DataFormat.GetDateTime(dr["dtBukukas"]),
                                      Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                      Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                      NamaBank = DataFormat.GetString(dr["sNamaBank"]),
                                      NoRekening = DataFormat.GetString(dr["sNoRekening"]),
                                      Status = DataFormat.GetSingle(dr["iStatus"]),
                                      Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                      Penyetor = DataFormat.GetString(dr["sPenyetor"]),
                                      Alamat = DataFormat.GetString(dr["sAlamat"]),
                                      JabatanPenyetor = DataFormat.GetString(dr["sJabatanPenyetor"]),
                                      PPKD = DataFormat.GetInteger(dr["bppkd"]),
                                      dtInput = DataFormat.GetDateTime(dr["dtInput"]),
                                      BankBUD = DataFormat.GetInteger(dr["iBankBUD"]),
                                      NPWP = DataFormat.GetString(dr["sNPWP"]),
                                      NoSKR = DataFormat.GetLong(dr["inoskr"]),
                                      StatusJurnal = DataFormat.GetInteger(dr["iStatusJurnal"]),
                                      Bank = DataFormat.GetInteger(dr["iBank"]),
                                      Rekenings = GetDetail(DataFormat.GetLong(dr["inourut"]))

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

        public List<STSDisetor> GetTBPDinas(int idDinas, DateTime dBatas, long noUrutSetor = 0)
          {
              List<STSDisetor> _lst = new List<STSDisetor>();
              try
              {
               //   SSQL = "SELECT tSTS.* FROM tSTS WHERE IDDInas =" + _idDinas.ToString() + " Order by inourut";//
                  SSQL = "SELECT tSTS.iNoUrut,tSTS.inourutsetor,tSTS.iStatus,tSTS.sNoSTS,tSTS.sKEterangan,tSTS.dtSTS,tSTSRekening.cJumlah, tSTSRekening.IIDRekening, tSTS.inourut , mRekening.sNamaRekening as NamaRekening FROM " +
                        " tSTS INNER JOIN tSTSRekening ON tSTS.inourut= tSTSRekening.inourut INNER JOIN MREKENING On mRekening.IIDRekening = tSTSRekening.IIDRekening WHERE tSTS.iTahun =" + Tahun.ToString() + " AND tSTS.btJenis = 0 " +
                        " AND TSTS.dtSTS<= @TANGGALBATAS and tSTS.IDDInas = @DINAS";                 
                 
               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@TANGGALBATAS", dBatas, DbType.Date));
               paramCollection.Add(new DBParameter("@DINAS", idDinas, DbType.Int32));


                      

                  SSQL = SSQL +  "  ORDER BY tSTS.dtSTS, tSTS.inourut";

 

                  DataTable dt = new DataTable();
                  dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                  if (dt != null)
                  {
                      if (dt.Rows.Count > 0)
                      {
                          _lst = (from DataRow dr in dt.Rows
                                  select new STSDisetor()
                                  {

                                      NoUrut = DataFormat.GetLong(dr["inourut"]),
                                      NoSTS = DataFormat.GetString(dr["sNoSTS"]),
                                      TanggalSTS = DataFormat.GetDateTime(dr["dtSTS"]),
                                      Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                      Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                      IIDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                      Status = DataFormat.GetSingle(dr["iStatus"]),
                                      NoUrutSetor = DataFormat.GetLong(dr["inourutsetor"]),
                                      NamaRekneing = DataFormat.GetString(dr["NamaRekening"]),

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
          public STS GetByID(long _inourut)
          {
              STS oSTS = new STS();
              try
              {
                  
                  DBParameterCollection paramCollection = new DBParameterCollection();

                  SSQL = "SELECT tSTS.* FROM tSTS WHERE inourut= @NOURUT";

                  paramCollection.Add(new DBParameter("@NOURUT", _inourut, DbType.Int64));

                  //

                  DataTable dt = new DataTable();
                  dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                  if (dt != null)
                  {
                      if (dt.Rows.Count > 0)
                      {
                          DataRow dr = dt.Rows[0];

                          oSTS = new STS()
                          {
                              NoUrut = DataFormat.GetLong(dr["inourut"]),
                              Tahun = DataFormat.GetInteger(dr["iTahun"]),
                              KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                              KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                              KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                              KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                              NoSTS = DataFormat.GetString(dr["sNoSTS"]),
                              IDDinas = DataFormat.GetInteger(dr["IDDINAS"]),

                              TanggalSTS = DataFormat.GetDateTime(dr["dtSTS"]),
                              NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                              dtBukuKas = DataFormat.GetDateTime(dr["dtBukukas"]),
                              Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                              Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                              NamaBank = DataFormat.GetString(dr["sNamaBank"]),
                              NoRekening = DataFormat.GetString(dr["sNoRekening"]),
                              Status = DataFormat.GetSingle(dr["iStatus"]),
                              Jenis = DataFormat.GetInteger(dr["btJenis"]),
                              Penyetor = DataFormat.GetString(dr["sPenyetor"]),
                              Alamat = DataFormat.GetString(dr["sAlamat"]),
                              JabatanPenyetor = DataFormat.GetString(dr["sJabatanPenyetor"]),
                              PPKD = DataFormat.GetInteger(dr["bppkd"]),
                              dtInput = DataFormat.GetDateTime(dr["dtInput"]),
                              BankBUD = DataFormat.GetInteger(dr["iBankBUD"]),
                              NPWP = DataFormat.GetString(dr["sNPWP"]),
                              NoSKR = DataFormat.GetLong(dr["inoskr"]),
                              StatusJurnal = DataFormat.GetInteger(dr["iStatusJurnal"]),
                              Bank = DataFormat.GetInteger(dr["iBank"]),
                              idcrt = DataFormat.GetInteger(dr["idcrt"]),
                              tcrt = DataFormat.GetDate(dr["dcrt"]),
                              NoUrutSetor = DataFormat.GetLong(dr["inourutSetor"]),


                               Rekenings = GetDetail(DataFormat.GetLong(dr["inourut"]))
                             

                                  };

                          };
                      }
                  
                  return oSTS;
              }
              catch (Exception ex)
              {
                  _isError = true;
                  _lastError = ex.Message;
                  return null;
              }

          }
          public bool Hapus(long  iNourut)
          {

              try
              {
                  SSQL = "DELETE  FROM " + m_sNamaTabel + " WHERE  iNourut = @NOURUT "; 

                 DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@NOURUT",  iNourut));


                  _dbHelper.ExecuteNonQuery(SSQL,paramCollection);

                  SSQL = "DELETE  FROM tSTSRekening WHERE  iNourut = @NOURUT ";
                  _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                  

                  SSQL = "DELETE  FROM tBKURekening WHERE  inourut in (Select inourut from tbku where iNourutSumber =  @NOURUT)";


                  _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                  SSQL = "DELETE  FROM tBKU WHERE  iNourutSumber =@NOURUT";

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
          public List<STSRekening> GetDetail(long NoUrut)
          {
              List<STSRekening> _lst = new List<STSRekening>();
              try
              {
                  SSQL = "SELECT TSTSRekening.*, mRekening.snamaRekening as Nama FROM tSTSREkening INNER JOIN mRekening ON tSTSRekening.IIDRekening = mRekening.IIDRekening WHERE tSTSRekening.inourut = " + NoUrut.ToString();

                  DataTable dt = new DataTable();
                  dt = _dbHelper.ExecuteDataTable(SSQL);
                  if (dt != null)
                  {
                      if (dt.Rows.Count > 0)
                      {
                          _lst = (from DataRow dr in dt.Rows
                                  select new STSRekening()
                                  {
                                      NoUrut = DataFormat.GetLong(dr["inourut"]),
                                      IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                      Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                      Nama = DataFormat.GetString(dr["Nama"]),

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
          public bool TerimaKasda (long NoUrut, int noUrutKasda)
          {
              List<STSRekening> _lst = new List<STSRekening>();
              try
              {
                  //SSQL = "SELECT TSTSRekening.*, mRekening.snamaRekening as Nama FROM tSTSREkening INNER JOIN mRekening ON tSTSRekening.IIDRekening = mRekening.IIDRekening WHERE tSTSRekening.inourut = " + NoUrut.ToString();
                  SSQL = "UPDATE tSTS set inourutkasda = " + noUrutKasda.ToString() + ", iStatus = 5 WHERE inourut = " + NoUrut.ToString();
                  if (_dbHelper.ExecuteNonQuery(SSQL) > 0)
                  {
                      CatatBKUKasda(NoUrut, noUrutKasda);
                  }

                  
                  return true ;
              }
              catch (Exception ex)
              {
                  _isError = true;
                  _lastError = ex.Message;
                  return false;
              }

          }
          private bool CatatBKUKasda(long NoUrut, int noUrutKasda)
          {
              try
              {
            
                  STS sts = new STS();
                  sts = GetByID(NoUrut);
                  if (sts == null || sts.NoUrut==0)
                  {
                      _lastError = _lastError;
                      return false;
                  }
                  List<BKU> lstBKU = new List<BKU>();
                  BKU MaxNoBKU = new BKU();
                  BKU MaxNoBKUBUD = new BKU();

                  BKULogic oBKULogic = new BKULogic(Tahun);
                  List<long> lstNoUrut = new List<long>();

                  lstNoUrut.Add(sts.NoUrut);

                  lstBKU = oBKULogic.GetBKUByNoUrutSumber(lstNoUrut,0);
                  MaxNoBKU = oBKULogic.GetBKUDenganMaxNoBKU(sts.IDDinas, sts.KodeUK, 0);
                  MaxNoBKUBUD = oBKULogic.GetBKUDenganMaxNoBKU(sts.IDDinas, sts.KodeUK, 0);
                  m_connection = _dbHelper.CreateCOnnection();
                  m_objTrans = m_connection.BeginTransaction();
                  int jenisSumber = 0;

                  jenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_TSTS;
                  bool berhasilSImpanBKU = true;
                  MaxNoBKUBUD.NoUrutSaja = MaxNoBKU.NoUrutSaja + 1;
                  if (SimpanBKUBUD(sts, 1, lstBKU, ref MaxNoBKUBUD,noUrutKasda, jenisSumber,
                             m_connection, m_objTrans) == false)
                  {
                          berhasilSImpanBKU = berhasilSImpanBKU && false;
                  }

                  

                  if (berhasilSImpanBKU == true)
                  {
                      m_objTrans.Commit();
                      m_connection.Close();
                  }
                  else
                  {
                      m_objTrans.Rollback();
                      m_connection.Close();
                  }

                  return true;
              }
              catch (Exception ex)
              {
                  m_objTrans.Rollback();
                  m_connection.Close();
                  return false;
              }

             
            }

        
    }
}
