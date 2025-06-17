using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP;
using DTO;
using System.Data;
using DataAccess;
using Formatting;
using DTO.Bendahara;
using BP.Bendahara;
namespace BP
{

    public class TSubKegiatanLogic : BP
    {
        public TSubKegiatanLogic(int _pTahun, int  pProfile)
            : base(_pTahun, 0, pProfile)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tSubKegiatan";

        }
        public bool SimpanSIPD(TSubKegiatan t)
        {
            try
            {
                int _KodeProgram;
                int _KodeKegiatan;
                int _KodeKategoriPelaksana;
                int _kodeUrusanPelaksana;
                int _KodeKategori;
                int _KodeUrusan;
                int _KodeSubKegiatan;
                int _KodeSKPD;
                int _KodeUK;

                //TSubKegiatan o = GetSubKegiatan((int)t.Tahun, t.IDDinas, t.IDUrusan, t.IDProgram, t.IDKegiatan, t.IDSubKegiatan);
                if (CekAda((int)t.Tahun, t.IDDinas, t.IDUrusan, t.IDProgram, t.IDKegiatan, t.IDSubKegiatan,t.KodeUK)==0)
                //if (o == null)
                {
                    _KodeProgram = DataFormat.GetInteger(t.IDProgram.ToString().Substring(3, 2));
                    _KodeKegiatan = DataFormat.GetInteger(t.IDKegiatan.ToString().Substring(5, 3));
                    _KodeSubKegiatan = DataFormat.GetInteger(t.IDSubKegiatan.ToString().Substring(8, 4));
                    _KodeKategoriPelaksana = DataFormat.GetInteger(t.IDUrusan.ToString().Substring(0, 1));
                    _kodeUrusanPelaksana = DataFormat.GetInteger(t.IDUrusan.ToString().Substring(1, 2));
                    _KodeKategori = DataFormat.GetInteger(t.IDDinas.ToString().Substring(0, 1));
                    _KodeUrusan = DataFormat.GetInteger(t.IDDinas.ToString().Substring(1, 2));
                    _KodeSKPD = DataFormat.GetInteger(t.IDDinas.ToString().Substring(3, 2));
                    _KodeUK = t.KodeUK;// DataFormat.GetInteger(t.IDDinas.ToString().Substring(5, 2));

                    SSQL = "INSERT INTO " + m_sNamaTabel + " (iTahun, IDDinas,IDUnit,IDUrusan," +
                     " IDProgram,IDkegiatan,IDSubKegiatan,IDUrusanMaster," +
                     " btkodekategori,btkodeurusan, btkodeSKPD, btkodeUK, btkodekategoriPelaksana,btkodeurusanPelaksana,btidprogram, btidkegiatan, btidsubkegiatan," +
                     " IDProgramMaster,IDkegiatanMaster,IDSubKegiatanMaster, Nama, btJenis) values (" +
                     " @piTahun,@pIDDinas ,@pUnit,@pIDUrusan " +
                     " ,@pIDProgram ,@pIDkegiatan ,@pIDSubKegiatan,@pIDUrusanMaster," +
                     "@pbtkodekategori,@pbtkodeurusan, @pbtkodeSKPD, @KodeUK, @pbtkodekategoriPelaksana,@pbtkodeurusanPelaksana,@pbtidprogram, @pbtidkegiatan, @pbtidsubkegiatan," +
                     " @pIDProgramMaster,@pIDkegiatanMaster,@pIDSubKegiatanMaster, @psNama, @btJenis)";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
                    paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas));
                    paramCollection.Add(new DBParameter("@pUnit", t.IDDinas + t.KodeUk));
                    paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
                    paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan));
                    paramCollection.Add(new DBParameter("@pIDSubKegiatan", t.IDSubKegiatan));

                    paramCollection.Add(new DBParameter("@pIDUrusanMaster", 0));

                    paramCollection.Add(new DBParameter("@pbtkodekategori",_KodeKategori));
                    paramCollection.Add(new DBParameter("@pbtkodeurusan",_KodeUrusan));
                    paramCollection.Add(new DBParameter("@pbtkodeSKPD",_KodeSKPD));
                    paramCollection.Add(new DBParameter("@KodeUK", _KodeUK));
               
                    paramCollection.Add(new DBParameter("@pbtkodekategoriPelaksana",_KodeKategoriPelaksana));
                    paramCollection.Add(new DBParameter("@pbtkodeurusanPelaksana",_kodeUrusanPelaksana));
                    paramCollection.Add(new DBParameter("@pbtidprogram",_KodeProgram));
                    paramCollection.Add(new DBParameter("@pbtidkegiatan",_KodeKegiatan));
                    paramCollection.Add(new DBParameter("@pbtidsubkegiatan", _KodeSubKegiatan));
                     

                    paramCollection.Add(new DBParameter("@pIDProgramMaster", 0));
                    paramCollection.Add(new DBParameter("@pIDkegiatanMaster", 0));
                    paramCollection.Add(new DBParameter("@pIDSubKegiatanMaster", 0));
                    paramCollection.Add(new DBParameter("@psNama", t.Nama == null ? "" : t.Nama, DbType.String));

                    paramCollection.Add(new DBParameter("@btJenis", 3));
                    
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                    ProgramKegiatanLogiccs pkLogic = new ProgramKegiatanLogiccs(Tahun);
                    pkLogic.Simpan(t.PK);




                }
                else
                {
                    SSQL = "UPDATE " + m_sNamaTabel + " SET Nama =@psNama,Plafon=@pPlafon, PlafonABT=@pPlafonABT, " +
                    "Outcome =@pOutcome " +
                        " WHERE IDDinas=@pIDDinas AND IDUrusan=@pIDUrusan " +
                         " AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan AND btKodeUK = @pUnit AND iTahun =@piTahun AND IDSubKegiatan=@pbtIDSubKegiatan";

                    //@pWaktuPelaksanaan, @pLokasi ,
                    // @pKecamatan ,@pDesa .@p.Keluaran ,@pOutcome ,@pSUmberPendanaan ,@pKeterangan 


                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("@psNama", t.Nama));
                    paramCollection.Add(new DBParameter("@pPlafon", 0));
                    paramCollection.Add(new DBParameter("@pPlafonABT",0));
                    //paramCollection.Add(new DBParameter("@pLokasi", t.Lokasi));
                    //paramCollection.Add(new DBParameter("@pKecamatan", t.Kecamatan));
                    //paramCollection.Add(new DBParameter("@pDesa", t.Desa));
                    //paramCollection.Add(new DBParameter("@pKeluaran", t.Keluaran));
                    paramCollection.Add(new DBParameter("@pOutcome",""));

                    paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas));
                    paramCollection.Add(new DBParameter("@pUnit",  t.KodeUk));
                    paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
                    paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan));
                    paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
                    paramCollection.Add(new DBParameter("@pbtIDSubKegiatan", t.IDSubKegiatan));
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
        public void SimpanKeluaran(int dinas,
            int idprogram, long idsubkegiatan, 
            string kp,string tp, string sp,
            string ks,string ts, string ss
            )
        {
            try
            {
                SSQL = "UPDATE ProgramKegiatan Set " +
                    "KeLuaransub='" + ks + "' , targetsub='" + ts + "', satuansub='" + ss + "' where iddinas = " + dinas.ToString() +
                        " and idprogram= " + idprogram.ToString() + " AND IDSUBKegiatan =" + idsubkegiatan.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
            }
            catch (Exception ex)
            {

            }
        }
        public void SimpanKeluaranProgram(int dinas,
            int idprogram, long idsubkegiatan,
            string kp, string tp, string sp,
            string ks, string ts, string ss
            )
        {
            try
            {
                SSQL = "UPDATE ProgramKegiatan Set KeLuaranProgram='" + kp + "' , targetProgram='" + tp + "', satuanProgram='" + sp + "' " +
                    " where iddinas = " + dinas.ToString() +
                        " and idprogram= " + idprogram.ToString() ;
                _dbHelper.ExecuteNonQuery(SSQL);
            }
            catch (Exception ex)
            {

            }
        }
        public bool Simpan(TSubKegiatan t)
        {
            try
            {
                TSubKegiatan o = GetSubKegiatan((int)t.Tahun, t.IDDinas,0, t.IDUrusan, t.IDProgram, t.IDKegiatan, t.IDSubKegiatan);
                if (o == null)
                {

                    SSQL = "INSERT INTO " + m_sNamaTabel + " (iTahun, IDDinas,IDUrusan," +
                     " IDProgram,IDkegiatan,IDSubKegiatan,IDUrusanMaster," +
                     " IDProgramMaster,IDkegiatanMaster,IDSubKegiatanMaster, Nama,Plafon,PlafonABT,btJenis,Keluaran,Outcome) values (" +
                     " @piTahun,@pIDDinas ,@pIDUrusan " +
                     " ,@pIDProgram ,@pIDkegiatan ,@pIDSubKegiatan,@pIDUrusanMaster," +
                     " @pIDProgramMaster,@pIDkegiatanMaster,@pIDSubKegiatanMaster, @psNama,@pPlafon,@pPlafonABT,3,@pKeluaran,@pOutcome)";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
                    paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas));
                    paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
                    paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan));
                    paramCollection.Add(new DBParameter("@pIDSubKegiatan", t.IDSubKegiatan));
                    paramCollection.Add(new DBParameter("@pIDUrusanMaster", t.IDUrusanMaster));
                    paramCollection.Add(new DBParameter("@pIDProgramMaster", t.IDProgramMaster));
                    paramCollection.Add(new DBParameter("@pIDkegiatanMaster", t.IDKegiatanMaster));
                    paramCollection.Add(new DBParameter("@pIDSubKegiatanMaster", t.IDSubKegiatanMaster));
                    paramCollection.Add(new DBParameter("@psNama", t.Nama == null ? "" : t.Nama,DbType.String ));
                    paramCollection.Add(new DBParameter("@pPlafon", t.Pagu,DbType.Currency));
                    paramCollection.Add(new DBParameter("@pPlafonABT", t.PaguABT,DbType.Currency));
                    paramCollection.Add(new DBParameter("@pKeluaran", t.Keluaran== null ? "" : t.Keluaran,DbType.String ));
                    paramCollection.Add(new DBParameter("@pOutcome", t.Outcome== null ? "" : t.Outcome,DbType.String ));


                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                }
                else
                {
                    SSQL = "UPDATE " + m_sNamaTabel + " SET Nama =@psNama,Plafon=@pPlafon, PlafonABT=@pPlafonABT, " +
                    "Outcome =@pOutcome "+
                        " WHERE IDDinas=@pIDDinas AND IDUrusan=@pIDUrusan " +
                         " AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan AND iTahun =@piTahun AND IDSubKegiatan=@pbtIDSubKegiatan";

                    //@pWaktuPelaksanaan, @pLokasi ,
                    // @pKecamatan ,@pDesa .@p.Keluaran ,@pOutcome ,@pSUmberPendanaan ,@pKeterangan 


                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("@psNama", t.Nama));
                    paramCollection.Add(new DBParameter("@pPlafon", t.Pagu,DbType.Currency));
                    paramCollection.Add(new DBParameter("@pPlafonABT", t.PaguABT, DbType.Currency));
                    //paramCollection.Add(new DBParameter("@pLokasi", t.Lokasi));
                    //paramCollection.Add(new DBParameter("@pKecamatan", t.Kecamatan));
                    //paramCollection.Add(new DBParameter("@pDesa", t.Desa));
                    //paramCollection.Add(new DBParameter("@pKeluaran", t.Keluaran));
                    paramCollection.Add(new DBParameter("@pOutcome", t.Outcome));
                    
                    paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas));
                    paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
                    paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan));
                    paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
                    paramCollection.Add(new DBParameter("@pbtIDSubKegiatan", t.IDSubKegiatan));
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

        public List<TSubKegiatan> GetByDInasDanBidangAnggaran(int dinas, int kodeUK, int UnitAnggaran)
        {
            List<TSubKegiatan> _lst = new List<TSubKegiatan>();
            try
            {
                //SubKegiatanBidang
                SSQL = "SELECT t.* FROM SubKegiatanBidang SKB INNER JOIN tSubKegiatan t ON SKB.iTahun = SKB.iTahun " +
                   " AND t.IDDInas= SKB.IDDInas and t.btKodeUK = SKB.UnitAnggaran   and t.IDSUBKegiatan = SKB.IDSUBKegiatan " +
                       " WHERE SKB.IDDInas = @DINAS  and SKB.btKodeUK =@KODEUK  ORDER BY t.IDSubKegiatan";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@DINAS", dinas));
                paramCollection.Add(new DBParameter("KODEUK", kodeUK));
                DataTable dt = new DataTable();


                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TSubKegiatan()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDsubkegiatan"]),
                                    IDUrusanMaster = DataFormat.GetInteger(dr["IDurusanMaster"]),
                                    IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
                                    IDKegiatanMaster = DataFormat.GetInteger(dr["IDkegiatanMaster"]),
                                    IDSubKegiatanMaster = DataFormat.GetLong(dr["IDsubkegiatanMaster"]),
                                    Lokasi = DataFormat.GetString(dr["Lokasi"]),
                                    SUmberPendanaan = DataFormat.GetString(dr["SUmberPendanaan"]),
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

        public bool UpdateUK(int KodeUKLama ,long idsubkegiatan, int IDDinas, int KodeUK, string Nama)
        {
            try
            {
                    
                SSQL = "UPDATE tSubKegiatan SET btKOdeUK = @KodeUK where IDDInas =@dinas and btKOdeuk =@KodeUKlama and IDSUbKegiatan =@idsubkegiatan";
                
                
                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("@KodeUK", KodeUK));
                    paramCollection.Add(new DBParameter("@dinas", IDDinas));
                    paramCollection.Add(new DBParameter("@idsubkegiatan", idsubkegiatan));
                    paramCollection.Add(new DBParameter("@KodeUKlama", KodeUKLama));



                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                    SSQL = "UPDATE tAnggaranRekening_A SET btKOdeUK = @KodeUK where IDDInas =@dinas  and btKOdeuk =@KodeUKlama  and IDSUbKegiatan =@idsubkegiatan";
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    SSQL = "UPDATE tAnggaranUraian_A SET btKOdeUK = @KodeUK where IDDInas =@dinas and btKOdeuk =@KodeUKlama and IDSUbKegiatan =@idsubkegiatan";
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    SSQL = "UPDATE tAnggaranKas  SET btKOdeUK = @KodeUK where IDDInas =@dinas and btKOdeuk =@KodeUKlama and IDSUbKegiatan =@idsubkegiatan";
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                    SSQL = "Update ProgramKegiatan set KodeUK  = @KodeUK ,NamaOrganisasi= @Nama  where IDDInas =@dinas and KOdeuk =@KodeUKlama and IDSUbKegiatan =@idsubkegiatan";
                    paramCollection.Add(new DBParameter("@Nama", Nama));
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
        public bool SImpanUmum(TSubKegiatan t)
        {
            try
            {
                TSubKegiatan o = GetSubKegiatan((int)t.Tahun, t.IDDinas, t.KodeUk, t.IDUrusan, t.IDProgram, t.IDKegiatan, t.IDSubKegiatan);
                    SSQL = "UPDATE " + m_sNamaTabel + " SET Lokasi=@pLokasi ,Mulai=@pMUlai, AKhir =@pAkhir, " +
                    "Kecamatan=@pKecamatan ,Desa=@pDesa , SumberPendanaan=@pSumberPendanaan" +
                        ", Keterangan =@pKeterangan WHERE IDDinas=@pIDDinas AND iTahun =@piTahun AND IDSubKegiatan=@pbtIDSubKegiatan";

                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("@pLokasi", t.Lokasi));
                    paramCollection.Add(new DBParameter("@pMUlai",t.Mulai));
                    paramCollection.Add(new DBParameter("@pAkhir", t.Akhir));
                    paramCollection.Add(new DBParameter("@pKecamatan", t.Kecamatan));
                    paramCollection.Add(new DBParameter("@pDesa", t.Desa));
                    paramCollection.Add(new DBParameter("@pSumberPendanaan", t.SUmberPendanaan));

                    paramCollection.Add(new DBParameter("@pKeterangan", t.Keterangan));


                    paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas));
                    paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
                    paramCollection.Add(new DBParameter("@pbtIDSubKegiatan", t.IDSubKegiatan));
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

        /*
         *  paramCollection.Add(new DBParameter("@pWaktuPelaksanaan", t.WaktuPelaksanaan));
                   
         * paramCollection.Add(new DBParameter("@pSUmberPendanaan", t.SUmberPendanaan));
                    paramCollection.Add(new DBParameter("@pKeterangan", t.Keterangan));

         */
        public bool SimpanDariKUA(KUA oKUA)
        {

            TSubKegiatan oSubKegiatan = new TSubKegiatan();
            oSubKegiatan.Tahun = oKUA.Tahun;
            oSubKegiatan.IDDinas = oKUA.IDDinas;
            oSubKegiatan.IDUrusan = oKUA.IDUrusan;
            oSubKegiatan.IDProgram = oKUA.IDProgram;
            oSubKegiatan.IDKegiatan = oKUA.IDKegiatan;
            oSubKegiatan.IDSubKegiatan = oKUA.IDSubKegiatan;

            oSubKegiatan.IDUrusanMaster = oKUA.IDUrusanMaster;
            oSubKegiatan.IDProgramMaster = oKUA.IDProgramMaster;
            oSubKegiatan.IDKegiatanMaster = oKUA.IDKegiatanMaster;
            oSubKegiatan.IDSubKegiatanMaster = oKUA.IDSubKegiatanMaster;
            oSubKegiatan.Nama = oKUA.NamaUsulan;
            oSubKegiatan.Pagu = oKUA.JumlahMurni;
            oSubKegiatan.PaguABT = oKUA.JumlahPerubahan;


            return Simpan(oSubKegiatan);

        }



        public TSubKegiatan GetSubKegiatan(int pTahun, int pIDDinas,int idUnit, int pIDUrusan=0, int pIDProgram=0, int pIDKegiatan=0, long _idSubKegiatan=0)
        {
            TSubKegiatan tKAPBD = new TSubKegiatan();
            try
            {


                    SSQL = "SELECT * , 0 as PaguPerubahan " +
               "  ,(Select  sum (cJumlahMurni) from tAnggaranRekening_A where iddinas = tSubKegiatan.IDDInas and IDSUbKegiatan = tSUbKEgiatan.IDSUbKegiatan ) as PaguMurni   FROM " + m_sNamaTabel + "  WHERE iTAhun =" + pTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString() +
                " AND btKodeUK = " + idUnit.ToString() + " AND IDSubKegiatan=" + _idSubKegiatan.ToString();


               // count = (int)_dbHelper.ExecuteScalar(SSQL);

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        tKAPBD = new TSubKegiatan()
                        {
                            Tahun = DataFormat.GetInteger(dr["iTahun"]),
                            IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                            IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                            IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                            IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                            IDSubKegiatan = DataFormat.GetLong(dr["IDsubkegiatan"]),
                            IDUrusanMaster = DataFormat.GetInteger(dr["IDurusanMaster"]),
                            IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
                            IDKegiatanMaster = DataFormat.GetInteger(dr["IDkegiatanMaster"]),
                            IDSubKegiatanMaster = DataFormat.GetLong(dr["IDsubkegiatanMaster"]),
                            KodePendek = DataFormat.GetInteger(dr["IDsubkegiatan"]).ToString().Length > 8 ? DataFormat.GetInteger(dr["IDsubkegiatan"]).ToString().Substring(8) : "00",//.Length
                            Kecamatan = DataFormat.GetInteger(dr["Kecamatan"]),
                            Desa = DataFormat.GetInteger(dr["Desa"]),
                            Lokasi = DataFormat.GetString(dr["Lokasi"]),
                            SUmberPendanaan = DataFormat.GetString(dr["SUmberPendanaan"]),
                            Nama = DataFormat.GetString(dr["Nama"]),
                            Pagu = DataFormat.GetDecimal(dr["pagumurni"]),
                            PaguABT = DataFormat.GetDecimal(dr["paguperubahan"]),
                            Outcome = DataFormat.GetString(dr["Outcome"]),
                            Keluaran = DataFormat.GetString(dr["Keluaran"]),
                            Mulai = DataFormat.GetString(dr["Mulai"]),
                            Akhir = DataFormat.GetString(dr["Akhir"]),
                            Status = DataFormat.GetInteger(dr["Status"]),
                            Keterangan = DataFormat.GetString(dr["Keterangan"]),
                            Target = DataFormat.GetDecimal(dr["Target"]),
                            SatuanTarget = DataFormat.GetString(dr["SatuanTarget"]),

                        };
                        return tKAPBD;
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
        public TSubKegiatan GetSubKegiatanEx(int pTahun, int pIDDinas, long _idSubKegiatan)
        {
            TSubKegiatan tKAPBD = new TSubKegiatan();
            try
            {
                int count = 0;

                SSQL = "SELECT * FROM " + m_sNamaTabel + "  WHERE iTAhun =" + pTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString() +
                       " AND IDSubKegiatan=" + _idSubKegiatan.ToString();

                // count = (int)_dbHelper.ExecuteScalar(SSQL);

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        tKAPBD = new TSubKegiatan()
                        {
                            Tahun = DataFormat.GetInteger(dr["iTahun"]),
                            IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                            IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                            IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                            IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                            IDSubKegiatan = DataFormat.GetLong(dr["IDsubkegiatan"]),
                            IDUrusanMaster = DataFormat.GetInteger(dr["IDurusanMaster"]),
                            IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
                            IDKegiatanMaster = DataFormat.GetInteger(dr["IDkegiatanMaster"]),
                            IDSubKegiatanMaster = DataFormat.GetLong(dr["IDsubkegiatanMaster"]),
                            KodePendek = DataFormat.GetInteger(dr["IDsubkegiatan"]).ToString().Length > 8 ? DataFormat.GetInteger(dr["IDsubkegiatan"]).ToString().Substring(8) : "00",//.Length
                            Kecamatan = DataFormat.GetInteger(dr["Kecamatan"]),
                            Desa = DataFormat.GetInteger(dr["Desa"]),
                            Lokasi = DataFormat.GetString(dr["Lokasi"]),
                            SUmberPendanaan = DataFormat.GetString(dr["SUmberPendanaan"]),
                            Nama = DataFormat.GetString(dr["Nama"]),
                            Pagu = DataFormat.GetDecimal(dr["Plafon"]),
                            PaguABT = DataFormat.GetDecimal(dr["PlafonABT"]),
                            Outcome = DataFormat.GetString(dr["Outcome"]),
                            Keluaran = DataFormat.GetString(dr["Keluaran"]),
                            Mulai = DataFormat.GetString(dr["Mulai"]),
                            Akhir = DataFormat.GetString(dr["Akhir"]),
                            Status = DataFormat.GetInteger(dr["Status"]),
                            Keterangan = DataFormat.GetString(dr["Keterangan"]),

                        };
                        return tKAPBD;
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
        public int CekAda(int pTahun, int pIDDinas, int pIDUrusan, int pIDProgram, int pIDKegiatan, long  pSubKegiatan,int idUnit=0)
        {

            TKegiatanAPBD tKAPBD = new TKegiatanAPBD();
            int jumlahRecord = 0;
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE iTAhun =" + pTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString() +
                       " AND IDUrusan =" + pIDUrusan.ToString() + " AND IDProgram = " + pIDProgram.ToString() +
                       " AND btKodeUK = "+ idUnit.ToString() +" AND IDKegiatan=" + pIDKegiatan.ToString() + " AND IDSUbKegiatan=" + pSubKegiatan.ToString();

                _dbHelper.ExecuteNonQuery(SSQL);
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        jumlahRecord = dt.Rows.Count;
                    }

                }

                return jumlahRecord;
            }
            catch (Exception ex)
            {

                _isError = true;
                _lastError = ex.Message;

                return 0;
            }


        }

        public bool Hapus(int pTahun, int pIDDinas, int pIDUrusan, int pIDProgram, int pIDKegiatan, long pSubKegiatan)
        {

           // TKegiatanAPBD tKAPBD = new TKegiatanAPBD();
      
            try
            {
                SSQL = "DELETE  FROM " + m_sNamaTabel + " WHERE iTAhun =" + pTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString() +
                       " AND IDUrusan =" + pIDUrusan.ToString() + " AND IDProgram = " + pIDProgram.ToString() +
                       " AND IDKegiatan=" + pIDKegiatan.ToString() + " AND IDSUbKegiatan=" + pSubKegiatan.ToString();

                _dbHelper.ExecuteNonQuery(SSQL);


                SSQL = "DELETE FROM tAnggaranRekening_A WHERE iTAhun =" + pTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString() +
                       " AND IDUrusan =" + pIDUrusan.ToString() + " AND IDProgram = " + pIDProgram.ToString() +
                       " AND IDKegiatan=" + pIDKegiatan.ToString() + " AND IDSUbKegiatan=" + pSubKegiatan.ToString();

                _dbHelper.ExecuteNonQuery(SSQL);



                SSQL = "DELETE FROM tAnggaranUraian_A WHERE iTAhun =" + pTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString() +
                       " AND IDUrusan =" + pIDUrusan.ToString() + " AND IDProgram = " + pIDProgram.ToString() +
                       " AND IDKegiatan=" + pIDKegiatan.ToString() + " AND IDSUbKegiatan=" + pSubKegiatan.ToString();

                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "DELETE  FROM tKUA WHERE iTAhun =" + pTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString() +
                       " AND IDUrusan =" + pIDUrusan.ToString() + " AND IDProgram = " + pIDProgram.ToString() +
                       " AND IDKegiatan=" + pIDKegiatan.ToString() + " AND IDSUbKegiatan=" + pSubKegiatan.ToString();

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

        public List<TSubKegiatan> GetSubKegiatanByKegiatan(int pTahun, int pIDDinas, int pIDUrusan, int pIDProgram, int pIDKegiatan)
        {


            List<TSubKegiatan> _lst = new List<TSubKegiatan>();
            try
            {
                SSQL = "SELECT A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.IDSUbKegiatan,A.Nama,A.Plafon,A.PlafonABT  " +
                    " FROM tSUbKegiatan A   inner join vwOrganisasi B on a.IDDInas = B.SKPD and A.btKodeuk = b.btKodeUK " + //LEFT OUTER JOIN tANGGaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDINas = B.IDDINas  and A.idUrusan = B.IDurusan AND A.IDProgram = B.IDProgram and A.IDKegiatan =B.IDKegiatan " +
                    " WHERE A.iTAhun =@TAHUN AND A.IDDInas =@DINAS "+
         //           " AND A.IDUrusan =" + pIDUrusan.ToString() + " AND A.IDProgram = " + pIDProgram.ToString() +
                    " AND A.IDKegiatan  =@IDKEGIATAN " +
                      " ORDER BY A.IDSUbKegiatan ";


               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@TAHUN", pTahun));
               paramCollection.Add(new DBParameter("@DINAS", pIDDinas));
               paramCollection.Add(new DBParameter("@IDKEGIATAN", pIDKegiatan));




                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TSubKegiatan()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Pagu = DataFormat.GetDecimal(dr["Plafon"]),
                                    PaguABT = DataFormat.GetDecimal(dr["PlafonABT"]),
                                    KodePendek = DataFormat.GetInteger(dr["IDsubkegiatan"]).ToString().Length > 8 ? DataFormat.GetInteger(dr["IDsubkegiatan"]).ToString().Substring(8) : "00",//.Length
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
        public List<TSubKegiatan> GetSubKegiatanByDinasUKKegiatan(int pTahun, int pIDDinas, int KodeUK, int pIDKegiatan)
        {


            List<TSubKegiatan> _lst = new List<TSubKegiatan>();



            try
            {
                
                SSQL = "SELECT A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.IDSUbKegiatan,A.Nama,A.Plafon,A.PlafonABT  " +
                    " FROM tSUbKegiatan A  " + //LEFT OUTER JOIN tANGGaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDINas = B.IDDINas  and A.idUrusan = B.IDurusan AND A.IDProgram = B.IDProgram and A.IDKegiatan =B.IDKegiatan " +
                    " WHERE A.iTAhun =@TAHUN AND A.IDDInas =@DINAS AND btKodeUK in (@KODEUK,0)" +
                    //           " AND A.IDUrusan =" + pIDUrusan.ToString() + " AND A.IDProgram = " + pIDProgram.ToString() +
                    " AND A.IDKegiatan  =@IDKEGIATAN " +
                      " ORDER BY A.IDSUbKegiatan ";


                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@TAHUN", pTahun));
                paramCollection.Add(new DBParameter("@DINAS", pIDDinas));
                paramCollection.Add(new DBParameter("@KODEUK", KodeUK));
                paramCollection.Add(new DBParameter("@IDKEGIATAN", pIDKegiatan));




                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TSubKegiatan()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Pagu = DataFormat.GetDecimal(dr["Plafon"]),
                                    PaguABT = DataFormat.GetDecimal(dr["PlafonABT"]),
                                    KodePendek = DataFormat.GetInteger(dr["IDsubkegiatan"]).ToString().Length > 8 ? DataFormat.GetInteger(dr["IDsubkegiatan"]).ToString().Substring(8) : "00",//.Length
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
        public List<TSubKegiatan> GetSubKegiatanByDinas(int pTahun, int pIDDinas)
        {


            List<TSubKegiatan> _lst = new List<TSubKegiatan>();



            try
            {

                SSQL = "SELECT A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.IDSUbKegiatan,A.Nama,A.Plafon,A.PlafonABT ,btKodeUK " +
                    " FROM tSUbKegiatan A  " + //LEFT OUTER JOIN tANGGaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDINas = B.IDDINas  and A.idUrusan = B.IDurusan AND A.IDProgram = B.IDProgram and A.IDKegiatan =B.IDKegiatan " +
                    " WHERE A.iTAhun =@TAHUN AND A.IDDInas =@DINAS " +
                      " ORDER BY A.IDSUbKegiatan ";


                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@TAHUN", pTahun));
                paramCollection.Add(new DBParameter("@DINAS", pIDDinas));
               




                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TSubKegiatan()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Pagu = DataFormat.GetDecimal(dr["Plafon"]),
                                    PaguABT = DataFormat.GetDecimal(dr["PlafonABT"]),
                                    KodeUK=DataFormat.GetInteger(dr["btKodeUK"]),
                                    KodeUk = DataFormat.GetInteger(dr["btKodeUK"]),
                                    KodePendek = DataFormat.GetInteger(dr["IDsubkegiatan"]).ToString().Length > 8 ? DataFormat.GetInteger(dr["IDsubkegiatan"]).ToString().Substring(8) : "00",//.Length
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

        public List<TSubKegiatan> GetSubKegiatanByKegiatanWithUnit(int pTahun, int pIDDinas, int kodeuk, int pIDUrusan, int pIDProgram, int pIDKegiatan)
        {


            List<TSubKegiatan> _lst = new List<TSubKegiatan>();
            try
            {
                if (pIDDinas == 1020100)
                {
                    SSQL = "SELECT A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.IDSUbKegiatan,A.Nama,A.Plafon,A.PlafonABT  " +
                        " FROM " + m_sNamaTabel + " A  " + //LEFT OUTER JOIN tANGGaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDINas = B.IDDINas  and A.idUrusan = B.IDurusan AND A.IDProgram = B.IDProgram and A.IDKegiatan =B.IDKegiatan " +
                        " WHERE A.iTAhun =" + pTahun.ToString() + " AND A.IDDInas =" + pIDDinas.ToString() +
                        " AND btKodeuk = " + kodeuk.ToString() + "  AND A.IDUrusan =" + pIDUrusan.ToString() + " AND A.IDProgram = " + pIDProgram.ToString() +
                        " AND A.IDKegiatan  =" + pIDKegiatan.ToString() +
                          " ORDER BY A.IDSUbKegiatan ";
                }
                else
                {
                    SSQL = "SELECT A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.IDSUbKegiatan,A.Nama,A.Plafon,A.PlafonABT  " +
                        " FROM " + m_sNamaTabel + " A  " + //LEFT OUTER JOIN tANGGaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDINas = B.IDDINas  and A.idUrusan = B.IDurusan AND A.IDProgram = B.IDProgram and A.IDKegiatan =B.IDKegiatan " +
                        " WHERE A.iTAhun =" + pTahun.ToString() + " AND A.IDDInas =" + pIDDinas.ToString() +

                        " AND btKodeuk = " + kodeuk.ToString() + " AND A.IDUrusan =" + pIDUrusan.ToString() + " AND A.IDProgram = " + pIDProgram.ToString() +
                        " AND A.IDKegiatan  =" + pIDKegiatan.ToString() +
                          " ORDER BY A.IDSUbKegiatan ";
                }

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TSubKegiatan()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Pagu = DataFormat.GetDecimal(dr["Plafon"]),
                                    PaguABT = DataFormat.GetDecimal(dr["PlafonABT"]),
                                    KodePendek = DataFormat.GetInteger(dr["IDsubkegiatan"]).ToString().Length > 8 ? DataFormat.GetInteger(dr["IDsubkegiatan"]).ToString().Substring(8) : "00",//.Length
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


        public List<SPDDetail> GetRekeningBasedSPD(long ID, int iDinas, DateTime dBatas)
        {
            SPDLogic oSPDLOgic = new SPDLogic(Tahun);
            List<SPDDetail> lstSPD = oSPDLOgic.GetDetailSebelum(dBatas, iDinas,ID);
            return lstSPD;

        }
        public List<SPDDetail> GetRekeningBasedSPDEx(long NoUrut, int iDinas, DateTime dBatas, int kodeUK, long IDSubKegiatan)
        {
            SPDLogic oSPDLOgic = new SPDLogic(Tahun);
            List<SPDDetail> lstSPD = oSPDLOgic.GetDetailSebelumNoUrutEx(NoUrut, iDinas, IDSubKegiatan, kodeUK);
            return lstSPD;
            

        }
        public List<AnggaranKas> GetRekeningBasedAnggaranKas ( int iDinas, int kodeUK, int  iTahap, long idsubkegiatan,DateTime dBatas)
        {
            AnggaranKasLogic oAnggaranKasLogic = new AnggaranKasLogic(Tahun);
            List<AnggaranKas> lstAnggaranKas = oAnggaranKasLogic.GetAKumulasiAnggaranKas(Tahun, iDinas, kodeUK, 3, dBatas, iTahap, idsubkegiatan);
            return lstAnggaranKas;
        }
        public List<TKegiatanAPBD> GetBySPJ(int _pITahun, int _pDinas, DateTime tanggalAwal, DateTime tanggalAkhir, string sNoUrut, long noUrutSPJUP = 0)
        {
            List<TKegiatanAPBD> _lst = new List<TKegiatanAPBD>();
            try
            {

                if (noUrutSPJUP == 0)
                {
                    SSQL = "SELECT IDDInas, idurusan,IDProgram,IDKEGIATAN,sNama, SUM(Jumlah) as Jumlah from ( ";
                    SSQL = SSQL + "Select A.IDDInas,A.IDUrusan,A.IDProgram,A.IDKEGIATAN, A.sNama, SUM(C.cJumlah) as Jumlah FROM tKegiatan_A A  INNER JOIN tPanjar B ON B.IDDINas= A.IDDInas and " +
                         " A.IDUrusan = B.IDurusan and A.IDProgram = B.IDPRogram And a.idkegiatan=b.idkegiatan  INNER JOIN tPanjarRekening C on B.iNourut= C.inourut " +
                          " where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() +
                          " and B.dtBukukas between " + tanggalAwal.ToSQLFormat() + " AND " + tanggalAkhir.ToSQLFormat();
                    if (sNoUrut.Length > 0)
                        SSQL = SSQL + " AND B.inourut in ( " + sNoUrut + ")";

                    SSQL = SSQL + " group by A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKEGIATAN,A.sNama " +
                          " UNION ";
                    SSQL = SSQL + " Select A.IDDInas,A.IDUrusan, A.IDProgram, A.IDKEGIATAN,A.sNama, " +
                         " SUM(-1 * C.iDebet1 * C.cJumlah1 )  as Jumlah FROM tKegiatan_A A  INNER JOIN tKoreksi B " +
                         "  ON A.iTahun= b.iTahun and B.IDDINas= A.IDDInas INNER jOIN  tKoreksiDetail C on A.IDUrusan = C.IDurusan and A.IDProgram = C.IDPRogram " +
                         " And a.idkegiatan=C.idkegiatan  AND B.iNourut= C.inourut where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() +
                         " AND B.inourut in ( " + sNoUrut + ")" +
                          " group by A.IDDInas,A.IDUrusan, A.IDProgram, A.IDKEGIATAN,A.sNama";
                    SSQL = SSQL + ") A Group by A.IDDInas, A.IDProgram,A.IDKEGIATAN order by A.IDDInas, A.IDProgram,A.IDKEGIATAN ";





                }
                else
                {


                    SSQL = "SELECT IDDInas, idurusan,IDProgram,IDKEGIATAN,IDSubkegiatan, Nama, SUM(Jumlah) as Jumlah from ( ";
                    SSQL = SSQL + "Select A.IDDInas,A.IDUrusan,A.IDProgram,A.IDKEGIATAN,A.IDSUbkegiatan, A.Nama, SUM(C.cJumlah) as Jumlah FROM tSubKegiatan A  INNER JOIN tPanjar B ON B.IDDINas= A.IDDInas and " +
                         " A.IDUrusan = B.IDurusan and A.IDProgram = B.IDPRogram And a.idkegiatan=b.idkegiatan a.idsubkegiatan = b.idsubkegiatan  INNER JOIN tPanjarRekening C on B.iNourut= C.inourut " +
                          " where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() +
                          " AND B.inourutspjup  =  " + noUrutSPJUP.ToString() +
                          " group by A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKEGIATAN,A.IDSUbkegiatan, A.Nama " +
                          " UNION ";
                    SSQL = SSQL + " Select A.IDDInas,A.IDUrusan, A.IDProgram, A.IDKEGIATAN,A.IDSUbkegiatan, A.Nama, " +
                         " SUM(-1 * C.iDebet1 * C.cJumlah1 )  as Jumlah FROM tSubKegiatan  A  INNER JOIN tKoreksi B " +
                         "  ON A.iTahun= b.iTahun and B.IDDINas= A.IDDInas INNER jOIN  tKoreksiDetail C on A.IDUrusan = C.IDurusan and A.IDProgram = C.IDPRogram " +
                         " And a.idkegiatan=C.idkegiatan  and a.idsubkegiatan = C.idsubkegiatan AND B.iNourut= C.inourut where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() +
                          " AND B.inourutspjup  =  " + noUrutSPJUP.ToString() +
                          " group by A.IDDInas,A.IDUrusan, A.IDProgram, A.IDKEGIATAN,A.IDSUbkegiatan, A.Nama ";
                    SSQL = SSQL + ") A Group by A.IDDInas, A.IDProgram,A.IDKEGIATAN order by A.IDDInas, A.IDProgram,A.IDKEGIATAN ";




                    SSQL = " SELECT IDDInas, idurusan,IDProgram,A.IDKEGIATAN,sNama, SUM(Jumlah) as Jumlah from ( ";
                    SSQL = SSQL + "Select A.IDDInas,A.IDUrusan,A.IDProgram, A.IDKEGIATAN,A.sNama, SUM(C.cJumlah) as Jumlah FROM tKegiatan_A A  INNER JOIN tPanjar B ON B.IDDINas= A.IDDInas and " +
                          " A.IDUrusan = B.IDurusan and A.IDProgram = B.IDPRogram INNER JOIN tPanjarRekening C on B.iNourut= C.inourut " +
                          " where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() +
                          " group by A.IDDInas, A.IDUrusan, A.IDProgram, A.btIDProgram,A.IDKEGIATAN,A.sNama " +
                           " UNION ";
                    SSQL = SSQL + " Select A.IDDInas,A.IDUrusan, A.IDProgram, A.IDKEGIATAN,A.sNama, " +
                            " SUM(-1 * C.iDebet1 * C.cJumlah1 )  as Jumlah FROM tKegiatan_A A  INNER JOIN tKoreksi B " +
                            "  ON A.iTahun= b.iTahun and B.IDDINas= A.IDDInas INNER jOIN  tKoreksiDetail C on A.IDUrusan = C.IDurusan and A.IDProgram = C.IDPRogram " +
                            " AND B.iNourut= C.inourut where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() +
                             " AND B.inourutspjup  =  " + noUrutSPJUP.ToString() +
                             " group by A.IDDInas,A.IDUrusan, A.IDProgram, A.IDKEGIATAN, A.sNama";
                    SSQL = SSQL + ") A Group by A.IDDInas, A.IDProgram ,A.IDKEGIATAN order by A.IDDInas, A.IDProgram ";

                }

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TKegiatanAPBD()
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
    }
}

//    public class TSubKegiatanLogic:BP 
//    {
//        public TSubKegiatanLogic(int _pTahun)
//            : base(_pTahun)
//        {
//            Tahun = _pTahun;
//            m_sNamaTabel = "tSubKegiatan";

//        }
//        public bool Simpan (TSubKegiatan t)
//        {
//            try
//            {
//                TSubKegiatan o = GetSubKegiatan((int)t.Tahun, t.IDDinas, t.IDUrusan, t.IDProgram, t.IDKegiatan, t.IDSubKegiatan);
//                if (o == null)
//                {

//                    SSQL = "INSERT INTO " + m_sNamaTabel + " (iTahun, IDDinas,IDUrusan," +
//                     " IDProgram,IDkegiatan,btIDSubKegiatan,sNama) values (" +
//                     " @piTahun,@pIDDinas ,@pIDUrusan " +
//                     " ,@pIDProgram ,@pIDkegiatan ,@pbtIDSubKegiatan,@psNama)";

//                    DBParameterCollection paramCollection = new DBParameterCollection();

//                    paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
//                    paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas));
//                    paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
//                    paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
//                    paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan));
//                    paramCollection.Add(new DBParameter("@pbtIDSubKegiatan", t.IDSubKegiatan));                                        
//                    paramCollection.Add(new DBParameter("@psNama", t.Nama == null ? "": t.Nama));

//                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


//                }
//                else
//                {
//                    SSQL = "UPDATE " + m_sNamaTabel + " SET sNama =@psNama WHERE IDDinas=@pIDDinas AND IDUrusan=@pIDUrusan " +
//                         " AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan AND iTahun =@piTahun AND btIDSubKegiatan=@pbtIDSubKegiatan";

//                    DBParameterCollection paramCollection = new DBParameterCollection();

//                    paramCollection.Add(new DBParameter("@psNama", t.Nama));
//                    paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas));
//                    paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
//                    paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
//                    paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan));
//                    paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
//                    paramCollection.Add(new DBParameter("@pbtIDSubKegiatan", t.IDSubKegiatan));
//                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
//                }               
//                return true;
//            }
//            catch (Exception ex)
//            {
//                _isError = true;
//                _lastError = ex.Message;
//                return false;
//            }


//        }



//        public TSubKegiatan GetSubKegiatan(int pTahun, int pIDDinas, int pIDUrusan, int pIDProgram, int pIDKegiatan, long  _idSubKegiatan)
//        {
//            TSubKegiatan tKAPBD = new TSubKegiatan();
//            try
//            {
//                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE iTAhun =" + pTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString() +
//                       " AND IDUrusan =" + pIDUrusan.ToString() + " AND IDProgram = " + pIDProgram.ToString() +
//                       " AND IDKegiatan=" + pIDKegiatan.ToString() + " AND btIDSubKegiatan=" + _idSubKegiatan.ToString();

//                _dbHelper.ExecuteNonQuery(SSQL);
//                DataTable dt = new DataTable();
//                dt = _dbHelper.ExecuteDataTable(SSQL);
//                if (dt != null)
//                {
//                    if (dt.Rows.Count > 0)
//                    {
//                        DataRow dr = dt.Rows[0];
//                        tKAPBD = new TSubKegiatan()
//                         {
//                             Tahun = DataFormat.GetInteger(dr["iTahun"]),
//                             IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
//                             IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
//                             IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
//                             IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
//                             Nama = DataFormat.GetString(dr["sNama"]),
//                             Pagu = DataFormat.GetDecimal(dr["cPlafon"])

//                         };
//                        return tKAPBD;
//                    }
//                }
//                return null;

//            }
//            catch (Exception ex)
//            {

//                _isError = true;
//                _lastError = ex.Message;

//                return null;
//            }


//        }
//        public int CekAda(int pTahun, int pIDDinas, int pIDUrusan, int pIDProgram, int pIDKegiatan, int pSubKegiatan)
//        {

//            TKegiatanAPBD tKAPBD = new TKegiatanAPBD();
//            int jumlahRecord = 0;
//            try
//            {
//                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE iTAhun =" + pTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString() +
//                       " AND IDUrusan =" + pIDUrusan.ToString() + " AND IDProgram = " + pIDProgram.ToString() +
//                       " AND IDKegiatan=" + pIDKegiatan.ToString() + " AND btIDSUbKegiatan=" + pSubKegiatan.ToString();

//                _dbHelper.ExecuteNonQuery(SSQL);
//                DataTable dt = new DataTable();
//                dt = _dbHelper.ExecuteDataTable(SSQL);

//                if (dt != null)
//                {
//                    if (dt.Rows.Count > 0)
//                    {
//                        jumlahRecord= dt.Rows.Count;
//                    }                    

//                }

//                return jumlahRecord;
//            }
//            catch (Exception ex)
//            {

//                _isError = true;
//                _lastError = ex.Message;

//                return 0;
//            }


//        }

//        public List<TSubKegiatan> GetKegiatanByKegiatan(int pTahun, int pIDDinas, int pIDUrusan, int pIDProgram, int pIDKegiatan)
//        {


//            List<TSubKegiatan> mListUnit = new List<TSubKegiatan>();
//            try
//            {
//                SSQL = "SELECT A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.btIDSUbKegiatan,A.sNama  " +
//                    " FROM " + m_sNamaTabel + " A LEFT OUTER JOIN tANGGaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDINas = B.IDDINas  and A.idUrusan = B.IDurusan AND A.IDProgram = B.IDProgram and A.IDKegiatan =B.IDKegiatan " +
//                    " WHERE A.iTAhun =" + pTahun.ToString() + " AND A.IDDInas =" + pIDDinas.ToString() +
//                    " AND A.IDUrusan =" + pIDUrusan.ToString() + " AND A.IDProgram = " + pIDProgram.ToString() +
//                    " GROUP BY A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.sNama2,A.btJenis  " +
//                    " ORDER BY A.IDKegiatan ";                       

//                DataTable dt = new DataTable();
//                dt = _dbHelper.ExecuteDataTable(SSQL);

//                if (dt != null)
//                {
//                    if (dt.Rows.Count > 0)
//                    {
//                         mListUnit = (from DataRow dr in dt.Rows
//                                 select new TSubKegiatan()
//                                {
//                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
//                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
//                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
//                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
//                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
//                                    IDSubKegiatan = DataFormat.GetInteger(dr["btIDSubkegiatan"]),
//                                    Nama = DataFormat.GetString(dr["sNama"])
//                                }).ToList() ;

//                    }

//                }
//                return mListUnit;

//            }
//            catch (Exception ex)
//            {

//                _isError = true;
//                _lastError = ex.Message;

//                return null;
//            }


//        }


//        //public TKegiatanAPBD  GetTempKegiatan(int _tahun, int _iddinas, int _idUrusan, int _idProgram, int _IDKegiatan , int _jenis)
//        //{

//        //    TKegiatanAPBD tKAPBD= new TKegiatanAPBD();
//        //    try
//        //    {
//        //        SSQL = "SELECT * FROM TKegiatan_AB WHERE iTAhun =" + _tahun.ToString() + " AND IDDInas =" + _iddinas.ToString() +
//        //               " AND IDUrusan =" + _idUrusan.ToString() + " AND IDProgram = " + _idProgram.ToString() +
//        //               " AND IDKegiatan=" + _IDKegiatan.ToString() + " AND btJenis=" +_jenis.ToString() + " ORDER By cPlafon DESC ";

//        //        _dbHelper.ExecuteNonQuery(SSQL);
//        //        DataTable dt = new DataTable();
//        //        dt = _dbHelper.ExecuteDataTable(SSQL);

//        //        if (dt != null)
//        //        {
//        //            if (dt.Rows.Count > 0)
//        //            {
//        //                DataRow dr = dt.Rows[0];
//        //                tKAPBD = new TKegiatanAPBD()
//        //                 {
//        //                     Tahun = DataFormat.GetInteger(dr["iTahun"]),
//        //                     IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
//        //                     IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
//        //                     IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
//        //                     IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
//        //                     Nama = DataFormat.GetString(dr["sNama"]),
//        //                     Pagu = DataFormat.GetDecimal(dr["cPlafon"]),
//        //                     Lokasi = DataFormat.GetString(dr["sLokasi"]),
//        //                     Kondisi = DataFormat.GetString(dr["sKondisi"]),
//        //                     Waktu = DataFormat.GetString(dr["sWaktu"]),
//        //                     TanggalPembahasan = DataFormat.GetDateTime(dr["dtPembahasan"]),
//        //                     Keterangan = DataFormat.GetString(dr["sKeterangan"]),
//        //                     TahapInput = DataFormat.GetInteger(dr["btTahapInput"]),
//        //                     AnggaranTahunDepan = DataFormat.GetDecimal(dr["cAnggaranTahunDepan"]),
//        //                     AnggaranTahunLalu = DataFormat.GetDecimal(dr["cAnggaranTahunLalu"]),
//        //                     KelompokSasaran = DataFormat.GetString(dr["sKelompokSasaran"]),
//        //                     Jenis = DataFormat.GetSingle(dr["btJenis"])

//        //                 };
//        //                return tKAPBD;

//        //            }

//        //        }

//        //    }
//        //    catch (Exception ex)            {

//        //        _isError = true;
//        //        _lastError = ex.Message;
//        //        return null;
//        //    }
//        //    return tKAPBD;        
//        //}
//    }
//}
