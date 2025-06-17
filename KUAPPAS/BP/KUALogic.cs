using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DataAccess;
using Formatting;
using System.Data;


namespace BP
{
    public class KUALogic:BP 
    {
        int mprofile;
        public KUALogic(int _pTahun, int profile=1)
            : base(_pTahun,0,profile)
        {
            Tahun = _pTahun;
            mprofile = profile; 

            m_sNamaTabel = "tkua";
        }
        public List<KUA> Get()
        {
            List<KUA> _lst = new List<KUA>();
            try
            {
                SSQL = "SELECT tKUa.* FROM tKUA ";//where profile =" + mprofile.ToString() ;
                
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new KUA()
                                {
                                ID = DataFormat.GetInteger(dr["ID"]),
                                Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                IDDinas = DataFormat.GetInteger(dr["IDDinas"]),
                                IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                IDLokasi = DataFormat.GetInteger(dr["IDLokasi"]),
                                KodeKategori = DataFormat.GetInteger(dr["btKodekategori"]),
                                KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                KodeKategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                KodeKegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                                KodeRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]),
                                JumlahOlah = DataFormat.GetDecimal(dr["JumlahOlah"]),
                                JumlahPerubahan = DataFormat.GetDecimal(dr["JumlahPerubahan"]),
                                Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                NamaUsulan = DataFormat.GetString(dr["Usulan"]),
                                Desa = DataFormat.GetInteger(dr["Desa"]),
                                Dusun = DataFormat.GetInteger(dr["Dusun"]),
                                Kecamatan = DataFormat.GetInteger(dr["Kecamatan"]),
                                KeteranganLokasi = DataFormat.GetString(dr["KeteranganLokasi"]),
                                JumlahRKPD = DataFormat.GetDecimal(dr["JumlahRKPD"]),
                                IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),
                                IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
                                IDKegiatanMaster = DataFormat.GetInteger(dr["IDKegiatanMaster"])

                                }).ToList();
                    }
                }
                return _lst;
            } catch(Exception ex){
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        public decimal GetBTL(int _pIDDInas, int _tahun, int jenis, int bPPKD, int Tahap )
        {
            decimal dc = 0L;
            try
            {

                if (Tahap <4 )

                    SSQL = "select SUM(JumlahMurni)  as Jumlah FROM tKUA WHERE iTahun =  " + _tahun.ToString() + "  and tKUA.IDDinas=" + _pIDDInas.ToString() + " AND btJenis= " + jenis.ToString() + " AND bPPKD=" + bPPKD.ToString();
                else
                    SSQL = "select SUM(JumlahPerubahan)  as Jumlah FROM tKUA WHERE iTahun =  " + _tahun.ToString() + "  and tKUA.IDDinas=" + _pIDDInas.ToString() + " AND btJenis= " + jenis.ToString() + " AND bPPKD=" + bPPKD.ToString();

               // SSQL = SSQL + " and profile=" + mprofile.ToString();

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        dc = DataFormat.GetDecimal(row["Jumlah"]);
                    }
                }

                return dc;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return 0;
            }
        }
        public List<CekInpuPagu> CekKUARKA(int iddinas)
        {
            List<CekInpuPagu> _lst = new List<CekInpuPagu>();
            
            try{
                SSQL = "select IDSUbKegiatan , Usulan, JumlahMurni, (Select SUM(cJumlahMurni) from tAnggaranRekening_A where IDDInas = tkua.iddinas " +
                " and IDSUbKegiatan= tkua.IDSUbKegiatan )  from tKUA where iddinas =" + iddinas.ToString();

           
            DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new CekInpuPagu()
                                {
                                    id= DataFormat.GetLong (dr["IDSUbKegiatan"]),
                                    nama= DataFormat.GetString(dr["Nama"]),
                                    kua= DataFormat.GetDecimal(dr["kua"]),
                                    rka= DataFormat.GetDecimal(dr["rka"]),
                                    selisih= DataFormat.GetDecimal(dr["kua"])-DataFormat.GetDecimal(dr["rka"])
                                }).ToList();
                   }
                }

            
             return _lst;
            } catch(Exception ex){
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }




        }
        public KUA GetByIDKegiatanInduk(int _pIDDInas, int pIDurusan, int pIDProgram, int pIDkegiatan, int _tahun)
        {
            KUA oKUA = new KUA();
            try
            {
                SSQL = " select tKUA.*,mKegiatan.sNamaKegiatan as NamaKegiatan from tKUA INNER JOIN mKegiatan ON tKUA.IDkegiatanMaster =mKegiatan.ID " +
                        " WHERE tKUA.iTahun =  " + _tahun.ToString() + "  and tKUA.IDDinas=" + _pIDDInas.ToString() + " AND isnull(tKUA.Status,0)<9 " +
                         " AND  tKUA.IDUrusan=" + pIDurusan.ToString() + " AND tKUA.IDProgram=" + pIDProgram.ToString() + " AND tKUA.IDKegiatan=" + pIDkegiatan.ToString() + " AND tKUA.IDLokasi =0 ";
            //    SSQL = SSQL + " and profile=" + mprofile.ToString();


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        oKUA = new KUA()
                        {
                            ID = DataFormat.GetInteger(dr["ID"]),
                            Tahun = DataFormat.GetInteger(dr["iTahun"]),
                            IDDinas = DataFormat.GetInteger(dr["IDDinas"]),
                            IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                            IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                            IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                            IDLokasi = DataFormat.GetInteger(dr["IDLokasi"]),
                            KodeKategori = DataFormat.GetInteger(dr["btKodekategori"]),
                            KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                            KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                            KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                            KodeKategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                            KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                            KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                            KodeKegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                            KodeRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                            JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]),
                            JumlahOlah = DataFormat.GetDecimal(dr["JumlahOlah"]),
                            JumlahPerubahan = DataFormat.GetDecimal(dr["JumlahPerubahan"]),
                            Jenis = DataFormat.GetInteger(dr["btJenis"]),
                            NamaKegiatan = DataFormat.GetString(dr["NamaKegiatan"]),
                            NamaUsulan = DataFormat.GetString(dr["Usulan"]),
                            Desa = DataFormat.GetInteger(dr["Desa"]),
                            Dusun = DataFormat.GetInteger(dr["Dusun"]),
                            Kecamatan = DataFormat.GetInteger(dr["Kecamatan"]),
                            KeteranganLokasi = DataFormat.GetString(dr["KeteranganLokasi"]),
                            JumlahRKPD = DataFormat.GetDecimal(dr["JumlahRKPD"]),
                            IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),
                            IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
                            IDKegiatanMaster = DataFormat.GetInteger(dr["IDKegiatanMaster"])

                        };
                    }
                }
                return oKUA;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return oKUA;
            }
        }


        public List<KUA> GetByIDDInas(int _pIDDInas, int _tahun, int iPPKD)
        {
            List<KUA> _lst = new List<KUA>();
            try
            {
                /*SSQL = "select tKUA.*, '' as NamaKegiatan from tKUA where tKUA.IDKEgiatan=0 and  tKUA.iTahun = "+ _tahun.ToString() +" and tKUA.IDDinas=" + _pIDDInas.ToString() + " AND isnull(tKUA.Status,0)<9 " +
                        " UNION ALL " +
                        " select tKUA.*,Musrenbang.nama_kegiatan as NamaKegiatan from tKUA INNER JOIN Musrenbang ON tKUA.IDkegiatan =Musrenbang.IDKegiatan " +
                        " and tKUA.IDlokasi= Musrenbang.IDLokasi WHERE tKUA.iTahun =  " + _tahun.ToString() + "  and tKUA.IDDinas=" + _pIDDInas.ToString() + " AND isnull(tKUA.Status,0)<9 " + 
                        " UNION ALL " +*/
                if (_tahun == 2020 )
                SSQL = " select tKUA.* from tKUA " +
                         " WHERE tKUA.iTahun =  " + _tahun.ToString() + "  and tKUA.IDDinas=" + _pIDDInas.ToString() + " and idsubkegiatan=0  AND isnull(tKUA.Status,0)<9  and bPPKD= " + iPPKD.ToString() +
                         "  ORDER BY IDKegiatan , IDSubKEgiatan ";
                else
                {
                    SSQL = " select tKUA.* from tKUA " +
                 " WHERE tKUA.iTahun =  " + _tahun.ToString() + "  and tKUA.IDDinas=" + _pIDDInas.ToString() + "AND isnull(tKUA.Status,0)<9  and bPPKD= " + iPPKD.ToString() +

                 " ORDER BY IDKegiatan , IDSubKEgiatan ";
        
                }
              
                
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new KUA()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDinas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),

                                    //IDLokasi = DataFormat.GetInteger(dr["IDLokasi"]),

                                    //KodeKategori = DataFormat.GetInteger(dr["btKodekategori"]),
                                    //KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    //KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    //KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    //KodeKategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    //KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    //KodeProgram = DataFormat.GetInteger(DataFormat.GetInteger(dr["IDProgram"]).ToSimpleKodeProgram()), //).ToString().Substring(3)),
                                    //KodeKegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                                    KodeRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    JumlahMurni = dr["JumlahMurni"] == null ? 0 : DataFormat.GetDecimal(dr["JumlahMurni"]),
                                    JumlahOlah = dr["JumlahOlah"] == null ? 0 : DataFormat.GetDecimal(dr["JumlahOlah"]),
                                    JumlahPerubahan = dr["JumlahPerubahan"] == null ? 0 : DataFormat.GetDecimal(dr["JumlahPerubahan"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    //       NamaKegiatan = DataFormat.GetString(dr["Usulan"]),
                                    NamaUsulan = DataFormat.GetString(dr["Usulan"]),
                                    //Desa = DataFormat.GetInteger(dr["Desa"]),
                                    //Dusun = DataFormat.GetInteger(dr["Dusun"]),
                                    //Kecamatan = DataFormat.GetInteger(dr["Kecamatan"]),
                                    //KeteranganLokasi = DataFormat.GetString(dr["KeteranganLokasi"]),
                                    //JumlahRKPD = dr["JumlahRKPD"]==null?0:DataFormat.GetDecimal(dr["JumlahRKPD"]),
                                    IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),
                                    IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
                                    IDKegiatanMaster = DataFormat.GetInteger(dr["IDKegiatanMaster"]),
                                    IDSubKegiatanMaster = DataFormat.GetLong(dr["IDSubKegiatanMaster"])
                                    //ID = DataFormat.GetInteger(dr["ID"]),
                                    //Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    //IDDinas= DataFormat.GetInteger(dr["IDDinas"]),
                                    //IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    //IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    //IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    //IDLokasi = DataFormat.GetInteger(dr["IDLokasi"]),
                                    //KodeKategori = DataFormat.GetInteger(dr["btKodekategori"]),
                                    //KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    //KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    //KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    //KodeKategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    //KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    //KodeProgram = DataFormat.GetInteger(DataFormat.GetInteger(dr["IDProgram"]).ToSimpleKodeProgram()), //).ToString().Substring(3)),
                                    //KodeKegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                                    //KodeRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    //JumlahMurni =dr["JumlahMurni"]==null?0: DataFormat.GetDecimal(dr["JumlahMurni"]),
                                    //JumlahOlah = dr["JumlahOlah"]==null?0: DataFormat.GetDecimal(dr["JumlahOlah"]),
                                    //JumlahPerubahan = dr["JumlahPerubahan"]==null?0:DataFormat.GetDecimal(dr["JumlahPerubahan"]),
                                    //Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    //NamaKegiatan = DataFormat.GetString(dr["Usulan"]),
                                    //NamaUsulan = DataFormat.GetString(dr["Usulan"]),
                                    //Desa = DataFormat.GetInteger(dr["Desa"]),
                                    //Dusun = DataFormat.GetInteger(dr["Dusun"]),
                                    //Kecamatan = DataFormat.GetInteger(dr["Kecamatan"]),
                                    //KeteranganLokasi = DataFormat.GetString(dr["KeteranganLokasi"]),
                                    //JumlahRKPD = dr["JumlahRKPD"]==null?0:DataFormat.GetDecimal(dr["JumlahRKPD"]),
                                    //IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),
                                    //IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
                                    //IDKegiatanMaster = DataFormat.GetInteger(dr["IDKegiatanMaster"])

                                }).ToList();
                    }
                }
                return _lst;
            } catch(Exception ex){
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }

        public List<KUA> GetByIDDInasAndIDKegiatan(int _pIDDInas, int _tahun, int idKegiatan)
        {
            List<KUA> _lst = new List<KUA>();
            try
            {
         
                SSQL = " select tKUA.* from tKUA " +
                         " WHERE tKUA.iTahun =  " + _tahun.ToString() + "  and tKUA.IDDinas=" + _pIDDInas.ToString() + " AND isnull(tKUA.Status,0)<9 and IDLokasi=0 and IDKegiatan= " + idKegiatan.ToString() +
                         "  ORDER BY IDKEgiatan,NoUrut, IDLokasi ";



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new KUA()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDinas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDLokasi = DataFormat.GetInteger(dr["IDLokasi"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodekategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    KodeKategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    KodeProgram = DataFormat.GetInteger(DataFormat.GetInteger(dr["IDProgram"]).ToSimpleKodeProgram()), //).ToString().Substring(3)),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                                    KodeRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    JumlahMurni = dr["JumlahMurni"] == null ? 0 : DataFormat.GetDecimal(dr["JumlahMurni"]),
                                    JumlahOlah = dr["JumlahOlah"] == null ? 0 : DataFormat.GetDecimal(dr["JumlahOlah"]),
                                    JumlahPerubahan = dr["JumlahPerubahan"] == null ? 0 : DataFormat.GetDecimal(dr["JumlahPerubahan"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    NamaKegiatan = DataFormat.GetString(dr["Usulan"]),
                                    NamaUsulan = DataFormat.GetString(dr["Usulan"]),
                                    Desa = DataFormat.GetInteger(dr["Desa"]),
                                    Dusun = DataFormat.GetInteger(dr["Dusun"]),
                                    Kecamatan = DataFormat.GetInteger(dr["Kecamatan"]),
                                    KeteranganLokasi = DataFormat.GetString(dr["KeteranganLokasi"]),
                                    JumlahRKPD = dr["JumlahRKPD"] == null ? 0 : DataFormat.GetDecimal(dr["JumlahRKPD"]),
                                    IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),
                                    IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
                                    IDKegiatanMaster = DataFormat.GetInteger(dr["IDKegiatanMaster"])

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

        public List<KUA> GetByIDKegiatan(int _pIDDInas, int pIDurusan, int pIDProgram, int pIDkegiatan, int _tahun)
        {
            List<KUA> _lst = new List<KUA>();
            try
            {/*
                SSQL = "select tKUA.*, '' as NamaKegiatan from tKUA where tKUA.IDKEgiatan=0 and  tKUA.iTahun =  " + _tahun.ToString() + "  and tKUA.IDDinas=" + _pIDDInas.ToString() + " AND isnull(tKUA.Status,0)<9 " +
                        " AND  tKUA.IDUrusan=" + pIDurusan.ToString() + " AND tKUA.IDProgram=" + pIDProgram.ToString() + " AND tKUA.IDKegiatan=" + pIDkegiatan.ToString() + " AND tKUA.IDLokasi>0 " +
                        " UNION ALL " +
                        " select tKUA.*,Musrenbang.nama_kegiatan as NamaKegiatan from tKUA INNER JOIN Musrenmbang ON tKUA.IDkegiatan =Musrenmbang.IDKegiatan " +
                        " and tKUA.IDlokasi= Musrenmbang.IDLokasi WHERE tKUA.iTahun =  " + _tahun.ToString() + "  and tKUA.IDDinas=" + _pIDDInas.ToString() + " AND isnull(tKUA.Status,0)<9 " +
                        " AND  tKUA.IDUrusan=" + pIDurusan.ToString() + " AND tKUA.IDProgram=" + pIDProgram.ToString() + " AND tKUA.IDKegiatan=" + pIDkegiatan.ToString() + " AND tKUA.IDLokasi>0 " +
                        " UNION ALL " +
                */
                    //    SSQL=        " select tKUA.*,mKegiatan.sNamaKegiatan as NamaKegiatan from tKUA INNER JOIN mKegiatan ON tKUA.IDkegiatanMaster =mKegiatan.ID " +
                    //    " WHERE tKUA.iTahun =  " + _tahun.ToString() + "  and tKUA.IDDinas=" + _pIDDInas.ToString() + " AND isnull(tKUA.Status,0)<9 " +
                    //     " AND  tKUA.IDUrusan=" + pIDurusan.ToString() + " AND tKUA.IDProgram=" + pIDProgram.ToString() + " AND tKUA.IDKegiatan=" + pIDkegiatan.ToString() + " AND tKUA.IDLokasi>0 " +
                     //   "ORDER BY IDKEgiatan,NoUrut, IDLokasi ";



                SSQL = " select tKua.* ,tKua.Usulan as NamaKegiatan from tKUA " +
                        " WHERE IDDinas=" + _pIDDInas.ToString() +
                        " AND  IDUrusan=" + pIDurusan.ToString() + " AND IDProgram=" + pIDProgram.ToString() + " AND IDKegiatan=" + pIDkegiatan.ToString() + " AND IDLokasi >0 " +
                        "  ORDER BY IDKEgiatan ";



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new KUA()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDinas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDLokasi = DataFormat.GetInteger(dr["IDLokasi"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodekategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    KodeKategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                                    KodeRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]),
                                    JumlahOlah = DataFormat.GetDecimal(dr["JumlahOlah"]),
                                    JumlahPerubahan = DataFormat.GetDecimal(dr["JumlahPerubahan"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    NamaKegiatan = DataFormat.GetString(dr["NamaKegiatan"]),
                                    NamaUsulan = DataFormat.GetString(dr["Usulan"]),
                                    Desa = DataFormat.GetInteger(dr["Desa"]),
                                    Dusun = DataFormat.GetInteger(dr["Dusun"]),
                                    Kecamatan = DataFormat.GetInteger(dr["Kecamatan"]),
                                    KeteranganLokasi = DataFormat.GetString(dr["KeteranganLokasi"]),
                                    JumlahRKPD = DataFormat.GetDecimal(dr["JumlahRKPD"]),
                                    IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),
                                    IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
                                    IDKegiatanMaster = DataFormat.GetInteger(dr["IDKegiatanMaster"])

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

        private void CatatHistory(KUA oKUA, Single _pAction)
        {
        //    KUAHistory oHistory = new KUAHistory();
        //    KUAHistoryLogic oLogic = new KUAHistoryLogic(Tahun);
        //    oHistory.IDKUA = oKUA.ID;
        //    oHistory.UserID = oKUA.UserID;
        //    oHistory.Value = oKUA.ToStrin();
        //    oHistory.Action = _pAction;
        //    //oHistory.Tanggal= DateTime.Now.Date;
        //    oHistory.Jam = DateTime.Now.Hour;
        //    oHistory.Menit = DateTime.Now.Minute;
        //    oHistory.Jumlah = oKUA.JumlahOlah;
        //    oLogic.Simpan(oHistory);
            
        }
        public KUA GetByID( string pID, int _tahun)
        {
            KUA _object = new KUA();
            try
            {
                
                SSQL = " select tKUA.*,mKegiatan.sNamaKegiatan as NamaKegiatan from tKUA INNER JOIN mKegiatan ON tKUA.IDkegiatan =mKegiatan.ID " +
                        " WHERE tKUA.iTahun =  " + _tahun.ToString() + " and ID =" + pID.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    DataRow dr = null;
                    if (dt.Rows.Count > 0)
                    {
                        dr= dt.Rows[0];
                        _object = new KUA()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDinas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDLokasi = DataFormat.GetInteger(dr["IDLokasi"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodekategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    KodeKategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    KodeProgram = DataFormat.GetInteger(dr["btIDProgram"]),
                                    KodeKegiatan = DataFormat.GetInteger(dr["btIDkegiatan"]),
                                    KodeRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]),
                                    JumlahOlah = DataFormat.GetDecimal(dr["JumlahOlah"]),
                                    JumlahPerubahan = DataFormat.GetDecimal(dr["JumlahPerubahan"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    NamaKegiatan = DataFormat.GetString(dr["NamaKegiatan"]),
                                    NamaUsulan = DataFormat.GetString(dr["Usulan"]),
                                    Desa = DataFormat.GetInteger(dr["Desa"]),
                                    Dusun = DataFormat.GetInteger(dr["Dusun"]),
                                    Kecamatan = DataFormat.GetInteger(dr["Kecamatan"]),
                                    KeteranganLokasi = DataFormat.GetString(dr["KeteranganLokasi"]),
                                    JumlahRKPD = DataFormat.GetDecimal(dr["JumlahRKPD"])
                                };
                    }
                }
                return _object;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _object;
            }


        }
        public bool ClearAll(int _pJenis, int _pIDDInas, int _tahun)
        {
            try
            {
                SSQL = "DELETE from tKUA WHERE iTahun=" + _tahun.ToString() +  "   and btJenis=" + _pJenis.ToString() + " AND IDDInas=" + _pIDDInas.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                return true;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }

        public int  ApakahAda(KUA _pKUA)
        {
       
            //TProgramAPBD oPrg = new TProgramAPBD();
            int jml = 0;
            try
            {
                if (_pKUA.Jenis == 3)
                {
                    if (Tahun == 2020)
                    {
                        SSQL = "Select * FROM tKUA " +
                              " where iTahun = " + _pKUA.Tahun.ToString() + " AND IDDInas= " + _pKUA.IDDinas.ToString() +
                              " and IDUrusan= " + _pKUA.IDUrusan.ToString() + " and IDProgram= " + _pKUA.IDProgram.ToString() +

                              "  AND IIDRekening = " + _pKUA.KodeRekening.ToString() + "  and IDkegiatan = " + _pKUA.IDKegiatan.ToString() + " AND IDSubKegiatan =0 ";// +_pKUA.IDSubKegiatan.ToString();

                    }
                    else
                    {
                        SSQL = "Select * FROM tKUA " +
                                 " where iTahun = " + _pKUA.Tahun.ToString() + " AND IDDInas= " + _pKUA.IDDinas.ToString() +
                                 " and IDUrusan= " + _pKUA.IDUrusan.ToString() + " and IDProgram= " + _pKUA.IDProgram.ToString() +
                                 "  AND IIDRekening = " + _pKUA.KodeRekening.ToString() + "  and IDkegiatan = " + _pKUA.IDKegiatan.ToString() + " AND IDSubKegiatan =" + _pKUA.IDSubKegiatan.ToString();
                    }

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
                else
                {
                    SSQL = "Select * FROM tKUA " +
                             " where iTahun = " + _pKUA.Tahun.ToString() + " AND IDDInas= " + _pKUA.IDDinas.ToString() +
                             " and btJenis=  " + _pKUA.Jenis.ToString() +
                            "  AND IIDRekening = " + _pKUA.KodeRekening.ToString();

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
            } 

            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return 0;
            }
        
        
        }

        public bool Clean(int iDinas)
        {

            try
            {
                //SSQL = "";

                SSQL = "UPDATE  tKUA SET JumlahMurni = 0 , JumlahOlah = 0 , JumlahRKPD=0 where iTahun=" + Tahun.ToString() + "  and IDDInas =" + iDinas.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "DELETE tPrograms_A where iTahun=" + Tahun.ToString() +  " and IDDInas =" + iDinas.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "DELETE tKegiatan_A where iTahun=" + Tahun.ToString() +  "  and IDDInas =" + iDinas.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                return true ;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return false;
            }
        }
        public bool Simpan(ref KUA _pKUA, int iDinas,  bool? dariMusrenbang= false )
        {
            try
            {
                          
                    int  _newID;




                    //if (_pKUA.Jenis == 3 && _pKUA.IDKegiatan == 0)
                     //   return false;

                //     RenstraKegiatanLogic oRenstraLogic = new RenstraKegiatanLogic(Tahun);
                  
                 
                    
                    if (_pKUA.IDSubKegiatan > 0 || _pKUA.IDKegiatan > 0)
                     {
                         if (ApakahAda(_pKUA) == 0)
                         {
                             _newID = GetMaxID() + 1;
                             SSQL = "INSERT INTO tKUA(iTahun,ID, IDDInas, IDUrusan, IDProgram, IDkegiatan,IDSubKegiatan,IIDRekening,JumlahOlah,JumlahMurni,JumlahPerubahan,Usulan,btjenis,bPPKD) values (" +
                                 "@piTahun,@pID, @pIDDInas, @pIDUrusan,@pIDProgram, @pIDkegiatan,@pIDSubKegiatan,@pIIDRekening,@pJumlahOlah,@pJumlahOlah,@pJumlahPerubahan,@pUsulan,@pbtjenis, 0)";

                             DBParameterCollection paramCollection = new DBParameterCollection();
                             paramCollection.Add(new DBParameter("@piTahun", _pKUA.Tahun));
                             paramCollection.Add(new DBParameter("@pID", _newID, DbType.Int32));
                             paramCollection.Add(new DBParameter("@pIDDInas", _pKUA.IDDinas, DbType.Int32));
                             paramCollection.Add(new DBParameter("@pIDUrusan", _pKUA.IDUrusan, DbType.Int32));
                             
                             paramCollection.Add(new DBParameter("@pIDProgram", _pKUA.IDProgram, DbType.Int32));
                             paramCollection.Add(new DBParameter("@pIDkegiatan", _pKUA.IDKegiatan, DbType.Int32));

                             paramCollection.Add(new DBParameter("@pIDSubKegiatan", _pKUA.IDSubKegiatan, DbType.Int64));



                             paramCollection.Add(new DBParameter("@pIIDRekening", _pKUA.KodeRekening, DbType.Int64));
                             paramCollection.Add(new DBParameter("@pJumlahOlah", _pKUA.JumlahOlah, DbType.Decimal));
                             paramCollection.Add(new DBParameter("@pJumlahPerubahan", _pKUA.JumlahPerubahan, DbType.Decimal));
    
                             paramCollection.Add(new DBParameter("@pUsulan", _pKUA.NamaUsulan, DbType.String));
                             paramCollection.Add(new DBParameter("@pbtjenis", _pKUA.Jenis, DbType.Int16));


                    
                             paramCollection.Add(new DBParameter("@pbPPKD", _pKUA.PPKD, DbType.Int32));
                            // paramCollection.Add(new DBParameter("@pprofile", mprofile, DbType.Int16));

                             _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                             //CatatHistory(_pKUA, 1);
                             _pKUA.ID = _newID;
                         }
                         else
                         {
                             _newID = _pKUA.ID;
                             SSQL = "UPDATE tKUA SET JumlahOlah = @pJumlahMurni,JumlahMurni=@pJumlahMurni,JumlahPerubahan =@pJumlahPerubahan,Usulan=@pUsulan WHERE  " +
                                     "iTahun=@piTahun and IDDInas = @pIDDInas and IDUrusan =@pIDUrusan and IDProgram = @pIDProgram and IDKegiatan =@pIDkegiatan and  IDSubKegiatan =@pIDSubkegiatan ";


                             DBParameterCollection paramCollection = new DBParameterCollection();



                             paramCollection.Add(new DBParameter("@pIIDRekening", _pKUA.KodeRekening));
                             paramCollection.Add(new DBParameter("@pJumlahMurni", _pKUA.JumlahOlah, DbType.Decimal));
                             paramCollection.Add(new DBParameter("@pJumlahPerubahan", _pKUA.JumlahPerubahan, DbType.Decimal));

                             paramCollection.Add(new DBParameter("@pUsulan", _pKUA.NamaUsulan, DbType.String));
                             //  paramCollection.Add(new DBParameter("@pKeteranganLokasi", _pKUA.KeteranganLokasi));
                             paramCollection.Add(new DBParameter("@piTahun", _pKUA.Tahun, DbType.Int32));

                             paramCollection.Add(new DBParameter("@pIDDInas", _pKUA.IDDinas, DbType.Int32));
                             paramCollection.Add(new DBParameter("@pIDUrusan", _pKUA.IDUrusan));
                             paramCollection.Add(new DBParameter("@pIDProgram", _pKUA.IDProgram));
                             paramCollection.Add(new DBParameter("@pIDkegiatan", _pKUA.IDKegiatan));
                             paramCollection.Add(new DBParameter("@pIDSubkegiatan", _pKUA.IDSubKegiatan));
                             paramCollection.Add(new DBParameter("@pPPKD", _pKUA.PPKD));
                           //  paramCollection.Add(new DBParameter("@pprofile", mprofile, DbType.Int16));



                             //   if (dariMusrenbang == false)
                             _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                             // _pKUA.ID = _newID;
                         } 
                    }
                   

                        

                    //        if (ApakahAda(_pKUA) == 0)
                    //     {
                    //         _newID = GetMaxID() + 1;
                    //         SSQL = "INSERT INTO tKUA(iTahun,ID, IDDInas, IDUrusan, IDProgram, IDkegiatan,IDSubKegiatan,IIDRekening,JumlahOlah,JumlahMurni,JumlahPerubahan,Usulan,btjenis, IDProgramMaster, IDkegiatanMaster,IDSubKegiatanMaster,bPPKD) values (" +
                    //             "@piTahun,@pID, @pIDDInas, @pIDUrusan, @pIDProgram, @pIDkegiatan,@pIDSubKegiatan,@pIIDRekening,@pJumlahOlah,@pJumlahOlah,@pJumlahPerubahan,@pUsulan,@pIDUrusanMaster,@pbtjenis, @pIDProgramMaster, @pIDkegiatanMaster,@pIDSubKegiatanMaster, 0 )";

                    //         DBParameterCollection paramCollection = new DBParameterCollection();
                    //         paramCollection.Add(new DBParameter("@piTahun", _pKUA.Tahun));
                    //         paramCollection.Add(new DBParameter("@pID", _newID, DbType.Int32));
                    //         paramCollection.Add(new DBParameter("@pIDDInas", _pKUA.IDDinas, DbType.Int32));
                    //         paramCollection.Add(new DBParameter("@pIDUrusan", _pKUA.IDUrusan, DbType.Int32));
                    //         paramCollection.Add(new DBParameter("@pIDProgram", _pKUA.IDProgram, DbType.Int32));
                    //         paramCollection.Add(new DBParameter("@pIDkegiatan", _pKUA.IDKegiatan, DbType.Int32));
                    //         paramCollection.Add(new DBParameter("@pIDSubKegiatan", _pKUA.IDSubKegiatan, DbType.Int64));
                    //         paramCollection.Add(new DBParameter("@pIIDRekening", _pKUA.KodeRekening, DbType.Int64));
                    //         paramCollection.Add(new DBParameter("@pJumlahOlah", _pKUA.JumlahOlah, DbType.Decimal));
                    //         paramCollection.Add(new DBParameter("@pJumlahPerubahan", _pKUA.JumlahPerubahan, DbType.Decimal));
                    //         //paramCollection.Add(new DBParameter("@JumlahRKPD", _pKUA.JumlahRKPD, DbType.Decimal));
                    //         paramCollection.Add(new DBParameter("@pbtjenis", _pKUA.Jenis, DbType.Int16));
                    //         paramCollection.Add(new DBParameter("@pUsulan", _pKUA.NamaUsulan, DbType.String));
                    //         paramCollection.Add(new DBParameter("@pIDUrusanMaster", _pKUA.IDUrusanMaster, DbType.Int32));
                    //         paramCollection.Add(new DBParameter("@pIDProgramMaster", _pKUA.IDProgramMaster, DbType.Int32));
                    //         paramCollection.Add(new DBParameter("@pIDkegiatanMaster", _pKUA.IDKegiatanMaster, DbType.Int64));
                    //         paramCollection.Add(new DBParameter("@pIDSubKegiatanMaster", _pKUA.IDSubKegiatanMaster, DbType.Int64));
                    //         paramCollection.Add(new DBParameter("@pNoUrut", _pKUA.NoUrut, DbType.Int32));
                    //         paramCollection.Add(new DBParameter("@pbPPKD", _pKUA.PPKD, DbType.Int32));

                    //         _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    //         //CatatHistory(_pKUA, 1);
                    //         _pKUA.ID = _newID;
                    //     }
                    //     else
                    //     {
                    //         _newID = _pKUA.ID;
                    //         SSQL = "UPDATE tKUA SET JumlahOlah = @pJumlahMurni,JumlahMurni=@pJumlahMurni,JumlahPerubahan =@pJumlahPerubahan,Usulan=@pUsulan WHERE  " +
                    //                 "iTahun=@piTahun and IDDInas = @pIDDInas and IDUrusan =@pIDUrusan and IDProgram = @pIDProgram and IDKegiatan =@pIDkegiatan and  IDSubKegiatan =@pIDSubkegiatan";


                    //         DBParameterCollection paramCollection = new DBParameterCollection();



                    //         paramCollection.Add(new DBParameter("@pIIDRekening", _pKUA.KodeRekening));
                    //         paramCollection.Add(new DBParameter("@pJumlahMurni", _pKUA.JumlahOlah, DbType.Decimal));
                    //         paramCollection.Add(new DBParameter("@pJumlahPerubahan", _pKUA.JumlahPerubahan, DbType.Decimal));

                    //         paramCollection.Add(new DBParameter("@pUsulan", _pKUA.NamaUsulan, DbType.String));
                    //         //  paramCollection.Add(new DBParameter("@pKeteranganLokasi", _pKUA.KeteranganLokasi));
                    //         paramCollection.Add(new DBParameter("@piTahun", _pKUA.Tahun, DbType.Int32));

                    //         paramCollection.Add(new DBParameter("@pIDDInas", _pKUA.IDDinas, DbType.Int32));
                    //         paramCollection.Add(new DBParameter("@pIDUrusan", _pKUA.IDUrusan));
                    //         paramCollection.Add(new DBParameter("@pIDProgram", _pKUA.IDProgram));
                    //         paramCollection.Add(new DBParameter("@pIDkegiatan", _pKUA.IDKegiatan));
                    //         paramCollection.Add(new DBParameter("@pIDSubkegiatan", _pKUA.IDSubKegiatan));
                    //         paramCollection.Add(new DBParameter("@pPPKD", _pKUA.PPKD));

                    //         //   if (dariMusrenbang == false)
                    //         _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                    //}
                    return true;  
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool SamakanPerubahanDenganMurni(int iDinas)
        {
            try
            {

                SSQL= "UPDATE tKUA SET JumlahPerubahan= JumlahMurni WHERE IDDinas = " + iDinas.ToString();

               _dbHelper.ExecuteNonQuery(SSQL);
                return true;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool UpdateID ()
        {
            try
            {

                
                List<KUA> _lst = Get();
                int i = 0;
                foreach (KUA k in _lst){

                    i++;
                SSQL = "UPDATE tKUA SET ID = " + i.ToString() +"  WHERE  " +
                            "iTahun=@piTahun and IDDInas = @pIDDInas and IDUrusan =@pIDUrusan and IDProgram = @pIDProgram and IIDRekening =@pIIDRekening and IDKegiatan =@pIDkegiatan and  IDLokasi=@pIDlokasi  "; //;//and IDUrusan= @pIDUrusan and IDPRogram= @pIDProgram and IDkegiatan= @pIDkegiatan and IIDRekening=@pIIDRekening and IDLokasi=@pIDlokasi and btJenis=@pbtJenis";


                    DBParameterCollection paramCollection = new DBParameterCollection();
                    //paramCollection.Add(new DBParameter("@pID", _newID));
                    paramCollection.Add(new DBParameter("@piTahun", k.Tahun));
                    paramCollection.Add(new DBParameter("@pIDDInas", k.IDDinas));

                    paramCollection.Add(new DBParameter("@pIDUrusan", k.IDUrusan));
                    paramCollection.Add(new DBParameter("@pIDProgram", k.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", k.IDKegiatan));
                    paramCollection.Add(new DBParameter("@pIIDRekening", k.KodeRekening));
                    paramCollection.Add(new DBParameter("@pIDlokasi", k.IDLokasi));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    //  CatatHistory(_pKUA, 2);
                    
                }
                //  CatatProgramKeAPBD(_pKUA);
                // CatatKegiatanDrKUA(_pKUA);
                return true;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }

        public bool SimpanDariPerencanaan(List<KUA> _lst)
        {
                
            try
            {
                SSQL = "DELETE from tKUA ";//where profile= "+ mprofile.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                
                    
                foreach (KUA _pKUA in _lst){    
                    int  _newID;
                    
                    _newID = GetMaxID() + 1;
                     SSQL = "INSERT INTO tKUA(iTahun,ID, IDDInas, IDUrusan, IDProgram, IDkegiatan,IIDRekening,JumlahOlah,JumlahMurni,JumlahPerubahan,JumlahRKPD,IDlokasi,btJenis, btIDProgram, btIDkegiatan, btKodekategoriPelaksana, btKodeUrusanPelaksana, btKodeKategori, btKodeURusan, btKodeSKPD, btKodeUK,Usulan,Kecamatan,Desa,Dusun,IDUrusanMaster, IDProgramMaster, IDkegiatanMaster,NoUrut,Status, bPPKD) values (" +
                            "@piTahun,@pID, @pIDDInas, @pIDUrusan, @pIDProgram, @pIDkegiatan,@pIIDRekening,@pJumlahOlah,@pJumlahOlah,@pJumlahPerubahan,@pJumlahPerubahan,@pIDlokasi,@pbtJenis, @pbtIDProgram, @pbtIDkegiatan, @pbtKodekategoriPelaksana, @pbtKodeUrusanPalaksana, @pbtKodeKategori, @pbtKodeURusan, @pbtKodeSKPD, @pbtKodeUK,@pUsulan,@pKecamatan,@pDesa,@pDusun,@pIDUrusanMaster, @pIDProgramMaster, @pIDkegiatanMaster,@pNoUrut,0, 0)";


                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@piTahun", _pKUA.Tahun));
                    paramCollection.Add(new DBParameter("@pID", _newID, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDDInas", _pKUA.IDDinas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pKUA.IDUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDProgram", _pKUA.IDProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", _pKUA.IDKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIIDRekening", _pKUA.KodeRekening, DbType.Int64));
                    paramCollection.Add(new DBParameter("@pJumlahOlah", _pKUA.JumlahOlah, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pJumlahPerubahan", _pKUA.JumlahPerubahan, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pIDlokasi", _pKUA.IDLokasi, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtJenis", _pKUA.Jenis, DbType.Int16));

                    paramCollection.Add(new DBParameter("@pbtIDProgram", _pKUA.KodeProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtIDkegiatan", _pKUA.KodeKegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", _pKUA.KodeKategoriPelaksana, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusanPalaksana", _pKUA.KodeUrusanPelaksana, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeKategori", _pKUA.KodeKategori, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeURusan", _pKUA.KodeUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeSKPD", _pKUA.KodeSKPD, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeUK", _pKUA.KodeUK, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pUsulan", _pKUA.NamaUsulan, DbType.String));
                    //paramCollection.Add(new DBParameter("@pKeteranganLokasi", _pKUA.KeteranganLokasi));
                    paramCollection.Add(new DBParameter("@pKecamatan", _pKUA.Kecamatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pDesa", _pKUA.Desa, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pDusun", _pKUA.Dusun, DbType.String));
                    paramCollection.Add(new DBParameter("@pIDUrusanMaster", _pKUA.IDUrusanMaster, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDProgramMaster", _pKUA.IDProgramMaster, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDkegiatanMaster", _pKUA.IDKegiatanMaster, DbType.Int64));
                    paramCollection.Add(new DBParameter("@pNoUrut", _pKUA.NoUrut, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbPPKD", _pKUA.PPKD, DbType.Int32));
                    //paramCollection.Add(new DBParameter("@pprofile",mprofile, DbType.Int16));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                
                }

                return true;
            } catch(Exception ex){
                _isError = true;
                _lastError = ex.Message;
                return false;


            }



        }
        public bool SimpanKeRKANonBL(ref KUA _pKUA, int Jenis)
        {
            try
            {

                 SSQL="DELETE tKegiatan_A where IDDInas = " + _pKUA.IDDinas.ToString() + " AND btJenis = " + Jenis.ToString() + " AND bPPKD= " + _pKUA.PPKD.ToString();
                _dbHelper.ExecuteNonQuery (SSQL);
                SSQL="INSERT INTO tKegiatan_A (IDDInas, IDurusan, iTahun , IDProgram, IDKegiatan, btJenis, cPlafon,bPPKD) values( " + _pKUA.IDDinas.ToString() + 
                       "," + _pKUA.IDDinas.ToString().Substring(0,3) + ",0,0," + Jenis.ToString() + "," + _pKUA.JumlahMurni.ToString() + "," + " AND bPPKD= " + _pKUA.PPKD.ToString() + ")";
                _dbHelper.ExecuteNonQuery(SSQL);
                return true;
            }
            
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }

        public bool SimpanDariMusrenbang(int idDInas, int IDUrusan, int idProgram, int idKegiatan,  int IDUrusanMaster, int idProgramMaster, int idKegiatanMaster,  string namaKegiatan)
        {
            return true;
            //try
            //{


            //    SSQL = "DELETE tKUA where iTahun = " + Tahun.ToString() + " AND IDDInas =" + idDInas.ToString() + " AND IDUrusan = " + IDUrusan.ToString() + " AND IDProgram = " + idProgram.ToString() + " AND IDKegiatan = " + idKegiatan.ToString() + " AND IDLokasi > 0 "; 


            //     _dbHelper.ExecuteNonQuery(SSQL);
            //    List <Musrenmbang> _mst = new List<Musrenmbang>();
            //    MusrenmbangLogic oMLogic = new MusrenmbangLogic(Tahun);
            //    _mst = oMLogic.GetUnAssignedByIDDInas(idDInas);//, IDUrusan, idProgram, idKegiatan);
            //    long _newID =0L;
            //    int noUrut =0;
            //    decimal cJumlah =0L;
            //    foreach ( Musrenmbang m in _mst ){

            //       _newID =DataFormat.GetLong( DataFormat.GetString(m.TahunAPBD ).Substring(2, 2) +
            //                   DataFormat.GetString(m.IDDInas) +
            //                    DataFormat.GetString(m.IDKegiatan) +
            //                   DataFormat.IntToStringWithLeftPad(m.id, 5));
                    
            //        noUrut ++;

            //        SSQL = "INSERT INTO tKUA(iTahun,ID, IDDInas, IDUrusan, IDProgram, IDkegiatan,IIDRekening,JumlahMurni,JumlahRKPD,IDlokasi,btJenis, btIDProgram, btIDkegiatan, btKodekategoriPelaksana, btKodeUrusanPelaksana, btKodeKategori, btKodeURusan, btKodeSKPD, btKodeUK,Usulan,Kecamatan,Desa,Dusun,IDUrusanMaster, IDProgramMaster, IDkegiatanMaster,NoUrut,Status) values (" +
            //            "@piTahun,@pID, @pIDDInas, @pIDUrusan, @pIDProgram, @pIDkegiatan,@pIIDRekening,@pJumlahOlah,@pJumlahPerubahan,@pIDlokasi,@pbtJenis, @pbtIDProgram, @pbtIDkegiatan, @pbtKodekategoriPelaksana, @pbtKodeUrusanPalaksana, @pbtKodeKategori, @pbtKodeURusan, @pbtKodeSKPD, @pbtKodeUK,@pUsulan,@pKecamatan,@pDesa,@pDusun,@pIDUrusanMaster, @pIDProgramMaster, @pIDkegiatanMaster,@pNoUrut,0)";


            //        DBParameterCollection paramCollection = new DBParameterCollection();
            //        paramCollection.Add(new DBParameter("@piTahun", m.TahunAPBD));
            //        paramCollection.Add(new DBParameter("@pID", _newID, DbType.String));
            //        paramCollection.Add(new DBParameter("@pIDDInas", m.IDDInas , DbType.Int32));
            //        paramCollection.Add(new DBParameter("@pIDUrusan", m.IDUrusan, DbType.Int32));
            //        paramCollection.Add(new DBParameter("@pIDProgram", m.IDProgram, DbType.Int32));
            //        paramCollection.Add(new DBParameter("@pIDkegiatan", m.IDKegiatan, DbType.Int32));
            //        paramCollection.Add(new DBParameter("@pIIDRekening", 0, DbType.Int64));
            //        paramCollection.Add(new DBParameter("@pJumlahOlah", m.Pagu2, DbType.Decimal));
            //        paramCollection.Add(new DBParameter("@pJumlahPerubahan", m.Pagu2, DbType.Decimal));
            //        paramCollection.Add(new DBParameter("@pIDlokasi", m.id , DbType.Int32));
            //        paramCollection.Add(new DBParameter("@pbtJenis", 3, DbType.Int16));

            //        paramCollection.Add(new DBParameter("@pbtIDProgram",DataFormat.GetInteger ( m.IDKegiatan.ToString ().Substring(5)) ,DbType.Int32)); 
            //        paramCollection.Add(new DBParameter("@pbtIDkegiatan",DataFormat.GetInteger ( m.IDKegiatan.ToString ().Substring(5)) , DbType.Int32));
            //        paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana",DataFormat.GetInteger ( m.IDUrusan.ToString ().Substring(0,1)), DbType.Int32));
            //        paramCollection.Add(new DBParameter("@pbtKodeUrusanPalaksana", DataFormat.GetInteger ( m.IDUrusan.ToString ().Substring(1,2)), DbType.Int32));
            //        paramCollection.Add(new DBParameter("@pbtKodeKategori", DataFormat.GetInteger ( m.IDDInas.ToString ().Substring(0,1)), DbType.Int32));
            //        paramCollection.Add(new DBParameter("@pbtKodeURusan", DataFormat.GetInteger ( m.IDDInas.ToString ().Substring(1,2)), DbType.Int32));
            //        paramCollection.Add(new DBParameter("@pbtKodeSKPD", DataFormat.GetInteger ( m.IDDInas.ToString ().Substring(3,2)) , DbType.Int32));
            //        paramCollection.Add(new DBParameter("@pbtKodeUK", 0, DbType.Int32));
            //        paramCollection.Add(new DBParameter("@pUsulan", m.nama , DbType.String));
            //        //paramCollection.Add(new DBParameter("@pKeteranganLokasi", _pKUA.KeteranganLokasi));
            //        paramCollection.Add(new DBParameter("@pKecamatan", m.kecamatan_id , DbType.Int32));
            //        paramCollection.Add(new DBParameter("@pDesa", m.desa_id , DbType.Int32));
            //        paramCollection.Add(new DBParameter("@pDusun", m.dusun_id, DbType.Int32));
            //        paramCollection.Add(new DBParameter("@pIDUrusanMaster", IDUrusanMaster, DbType.Int32));
            //        paramCollection.Add(new DBParameter("@pIDProgramMaster", idProgramMaster, DbType.Int32));
            //        paramCollection.Add(new DBParameter("@pIDkegiatanMaster", idKegiatanMaster  , DbType.Int32));
            //        paramCollection.Add(new DBParameter("@pNoUrut", noUrut, DbType.Int32));
                    

            //        cJumlah = cJumlah+ m.Pagu2;
            //        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

            //    }
            //    KUA  kuaKegiatan = new KUA();
            //    kuaKegiatan.Tahun= Tahun;
            //    kuaKegiatan.IDDinas = idDInas;
            //    kuaKegiatan.IDKegiatan=idKegiatan;
            //    kuaKegiatan.IDProgram = idProgram;
            //    kuaKegiatan.IDUrusan=IDUrusan ;
            //    kuaKegiatan.Jenis =3;
            //    kuaKegiatan.JumlahRKPD = cJumlah;
            //    kuaKegiatan.JumlahOlah =cJumlah;
            //    kuaKegiatan.IDKegiatanMaster =idKegiatanMaster;
            //    kuaKegiatan.IDProgramMaster = idProgramMaster;
            //    kuaKegiatan.IDUrusanMaster =IDUrusanMaster ;
            //    kuaKegiatan.Kecamatan=0;
            //    kuaKegiatan.Desa =0;
            //    kuaKegiatan.Dusun=0;
            //    kuaKegiatan.IDLokasi =0;
            //    kuaKegiatan.NamaUsulan =namaKegiatan;
            //    kuaKegiatan.KeteranganLokasi ="";
            //    kuaKegiatan.KodeProgram = DataFormat.GetInteger (idKegiatan.ToString ().Substring(3,2))  ;
            //    kuaKegiatan.KodeKegiatan = DataFormat.GetInteger (idKegiatan.ToString ().Substring(5))  ;
            //    kuaKegiatan.KodeKategoriPelaksana =DataFormat.GetInteger ( IDUrusan.ToString ().Substring(0,1));
            //    kuaKegiatan.KodeUrusanPelaksana = DataFormat.GetInteger ( IDUrusan.ToString ().Substring(1,2));
            //    kuaKegiatan.KodeKategori= DataFormat.GetInteger (idDInas.ToString ().Substring(0,1));
            //    kuaKegiatan.KodeUrusan = DataFormat.GetInteger (idDInas.ToString ().Substring(1,2));
            //    kuaKegiatan.KodeSKPD = DataFormat.GetInteger (idDInas.ToString ().Substring(3,2));
            //    kuaKegiatan.KodeUK=0;
            //    Simpan(ref kuaKegiatan,idDInas, true );
            //    return true;


            //}catch(Exception ex){
            //    _lastError = ex.Message;
            //    _isError = true;
            //    return false;

            //}
        }
        
        private bool CatatKegiatanDiRKA(KUA _pKUA)
        {
             
                            SSQL = "Select * FROM tKegiatan_A where iTahun=" + _pKUA.Tahun.ToString() + " and IDDInas= " + _pKUA.IDDinas.ToString() + "  and IDUrusan= " +
                                _pKUA.IDUrusan.ToString() + " AND IDProgram=" + _pKUA.IDProgram.ToString() + "   AND IDKegiatan=" + _pKUA.IDKegiatan.ToString() ;

                            DataTable dtx = new DataTable();
                            dtx = _dbHelper.ExecuteDataTable(SSQL);
                            if (dtx != null)
                            {
                                if (dtx.Rows.Count == 0)
                                {
                                    SSQL = "INSERT INTO tKegiatan_A (iTahun, IDDinas,IDUrusan," +
                                            " IDProgram,IDkegiatan ,btJenis,sNama,cPlafon,profile) Select tKUA.iTahun, tKUA.IDDInas,tKUA.IDurusan, tKUA.IDProgram,tKUA.IDkegiatan, 3 as btJenis, tKUA.Usulan as sNama,tKUA.JumlahOlah as cPlafon " +
                                            " , profile FROM tKUA " +
                                            " where tKUA.iTahun= " + _pKUA.Tahun.ToString() + " AND tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pKUA.IDDinas.ToString() + " AND tKUA.IDUrusan = " + _pKUA.IDUrusan.ToString() + "" +
                                             " AND tKUA.IDProgram = " + _pKUA.IDProgram.ToString() + " AND tKUA.IDkegiatan = " + _pKUA.IDKegiatan.ToString() + " " +
                                             " AND tKUA.IDlokasi=0   AND isnull(tKUA.Status,0)<9 and profile=" + mprofile.ToString() ;
                                    _dbHelper.ExecuteNonQuery(SSQL);







                                } else {

                                

                                    SSQL = "UPDATE tKegiatan_A SET cPlafonABT = tKua.Jumlahperubahan from tKegiatan_A INNER JOIN tKUA ON tKegiatan_A.iTahun = tKUA.iTahun and tKegiatan_A.IDDInas = tKUA.IDDInas and tKegiatan_A.IDUrusan = tKUA.IDUrusan and tKUA.IDProgram = tKegiatan_A.IDProgram " +
                                            " where tKUA.iTahun= " + _pKUA.Tahun.ToString() + " AND tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pKUA.IDDinas.ToString() + " AND tKUA.IDUrusan = " + _pKUA.IDUrusan.ToString() + "" +
                                            " AND tKUA.IDProgram = " + _pKUA.IDProgram.ToString() + " AND tKUA.IDkegiatan = " + _pKUA.IDKegiatan.ToString() + " " +
                                            " AND tKUA.IDlokasi=0   AND isnull(tKUA.Status,0)<9  and profile=" + mprofile.ToString();

                                    _dbHelper.ExecuteNonQuery(SSQL);

                                    
                                     
                                }
                           }
                           return true;
        }

        private bool CatatKegiatanDrKUA(KUA _pKUA)
        {

           // SSQL = "DELETE FROM tKegiatan_A where iTahun=" + _pKUA.Tahun.ToString() + " and IDDInas= " + _pKUA.IDDinas.ToString() + "  and IDUrusan= " +
           //     _pKUA.IDUrusan.ToString() + " AND IDProgram=" + _pKUA.IDProgram.ToString() + " AND IDKegiatan=" + _pKUA.IDKegiatan.ToString();
            
           //_dbHelper.ExecuteDataTable(SSQL);
            try
            {
                if (Tahun >= 2020)
                {
                    //MasterKegiatanLogic oLogic = new MasterKegiatanLogic(Tahun);
                    //MasterKegiatan mk = new MasterKegiatan();
                    //mk.ID = _pKUA.IDKegiatanMaster;
                    //mk.KategoriPelaksana = _pKUA.IDUrusanMaster / 100;
                    //mk.UrusanPelaksana = _pKUA.IDUrusanMaster % 100;
                    //mk.IDUrusan = _pKUA.IDUrusanMaster;
                    //mk.IDProgram = _pKUA.IDProgramMaster;
                    //mk.Program = _pKUA.IDProgramMaster % 100;
                    //mk.Kode = _pKUA.IDKegiatanMaster % 1000;
                    //mk.Nama = _pKUA.NamaUsulan;

                    //if (oLogic.Simpan(ref mk) == false)
                    //{
                    //    //    return false;
                    //}
                }





                SSQL = "Select * FROM tKegiatan_A where iTahun=" + _pKUA.Tahun.ToString() + " and IDDInas= " + _pKUA.IDDinas.ToString() + "  and IDUrusan= " +
                    _pKUA.IDUrusan.ToString() + " AND IDProgram=" + _pKUA.IDProgram.ToString() + " AND IDKegiatan=" + _pKUA.IDKegiatan.ToString() + " AND profile= " + mprofile.ToString();

                DataTable dtx = new DataTable();

                dtx = _dbHelper.ExecuteDataTable(SSQL);
                if (dtx != null)
                {

                    if (dtx.Rows.Count == 0)
                    {
                        if (Tahun < 2020)
                        {
                            SSQL = "INSERT INTO tKegiatan_A (iTahun, IDDinas,IDUrusan," +
                                    " IDProgram,IDkegiatan ,btKodekategori, btKodeURusan, btKodeSKPD, btKodeUK, btKodekategoriPelaksana, btKodeUrusanPelaksana,btIDprogram, btIDKegiatan,btJenis,sNama,cPlafon,cPlafonABT, IDUrusanM, IDProgramM, IDKegiatanM, profile) Select tKUA.iTahun, tKUA.IDDInas, " +
                                    " tKUA.IDurusan, tKUA.IDProgram,tKUA.IDkegiatan, tKUA.IDDInAS/1000000 AS btKodekategori,(tKUA.IDDInAS/10000)%100 AS btKodeUrusan,(tKUA.IDDInAS/100)%100 AS btKodeSKPD,0 as btKodeUK, " +
                                    " tKUA.IDuRUSAN/100 AS btKodekategoriPelaksana,tKUA.IDuRUSAN %100 AS btKodeUrusanPelaksana,tKUA.IDProgram % 100 as btIDProgram, tKUA.IDKegiatan % 1000 as btIDKegiatan , 3 as btJenis, tKUA.Usulan as sNama,tKUA.JumlahMurni as cPlafon,tKUA.JumlahPerubahan as cPlafonABT, IDUrusanMaster, IDProgramMaster, IDKegiatanMaster, profile " +
                                    " FROM tKUA inner join mKegiatan ON tKUA.IDKegiatanMaster= mKegiatan.ID " +
                                    " where tKUA.iTahun= " + _pKUA.Tahun.ToString() + " AND tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pKUA.IDDinas.ToString() + " AND tKUA.IDUrusanMaster = " + _pKUA.IDUrusanMaster.ToString() + "" +
                                     " AND tKUA.IDProgramMaster = " + _pKUA.IDProgramMaster.ToString() + " AND tKUA.IDkegiatanMaster = " + _pKUA.IDKegiatanMaster.ToString() + " " +
                                     " AND tKUA.IDlokasi=0   AND isnull(tKUA.Status,0)<9  and tkuA.profile=" + mprofile.ToString();
                            _dbHelper.ExecuteNonQuery(SSQL);
                        }
                        else
                        {
                            if (_pKUA.JumlahMurni > 0 || _pKUA.JumlahPerubahan  > 0 )
                            {
                                SSQL = "INSERT INTO tKegiatan_A (iTahun, IDDinas,IDUrusan," +
                                        " IDProgram,IDkegiatan ,btKodekategori, btKodeURusan, btKodeSKPD, btKodeUK, btKodekategoriPelaksana, btKodeUrusanPelaksana,btIDprogram, btIDKegiatan,btJenis,sNama,cPlafon,cPlafonABT, IDUrusanM, IDProgramM, IDKegiatanM, profile) Select tKUA.iTahun, tKUA.IDDInas, " +
                                        " tKUA.IDurusan, tKUA.IDProgram,tKUA.IDkegiatan, tKUA.IDDInAS/1000000 AS btKodekategori,(tKUA.IDDInAS/10000)%100 AS btKodeUrusan,(tKUA.IDDInAS/100)%100 AS btKodeSKPD,0 as btKodeUK, " +
                                        " tKUA.IDuRUSAN/100 AS btKodekategoriPelaksana,tKUA.IDuRUSAN %100 AS btKodeUrusanPelaksana,tKUA.IDProgram % 100 as btIDProgram, tKUA.IDKegiatan % 1000 as btIDKegiatan , 3 as btJenis, tKUA.Usulan as sNama,tKUA.JumlahMurni as cPlafon,tKUA.JumlahPerubahan as cPlafonABT, IDUrusanMaster, IDProgramMaster, IDKegiatanMaster,profile " +
                                        " FROM tKUA  " +
                                        " where tKUA.iTahun= " + _pKUA.Tahun.ToString() + " AND tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pKUA.IDDinas.ToString() +
                                        " AND tKUA.IDKEgiatan = " + _pKUA.IDKegiatan.ToString()+ "  and tkuA.profile=" + mprofile.ToString();
                                _dbHelper.ExecuteNonQuery(SSQL);
                            }
                        }
                    }
                    else
                    {



                        SSQL = "UPDATE tKegiatan_A SET cPlafon = tKua.JumlahMurni, cPlafonABT = tKua.JumlahPerubahan from tKegiatan_A INNER JOIN tKUA ON tKegiatan_A.iTahun = tKUA.iTahun and tKegiatan_A.IDDInas = tKUA.IDDInas and tKegiatan_A.IDUrusan = tKUA.IDUrusan and tKUA.IDProgram = tKegiatan_A.IDProgram and  tKUA.IDKegiatan = tKegiatan_A.IDKegiatan " +
                                " and tKUA.profile= tkegiatan_a.profile where tKUA.iTahun= " + _pKUA.Tahun.ToString() + " AND tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pKUA.IDDinas.ToString() + " AND tKUA.IDUrusan = " + _pKUA.IDUrusan.ToString() + "" +
                                " AND tKUA.IDProgram = " + _pKUA.IDProgram.ToString() + " AND tKUA.IDkegiatan = " + _pKUA.IDKegiatan.ToString() + " " +
                                " AND tKUA.IDlokasi=0   AND isnull(tKUA.Status,0)<9  and tkuA.profile=" + mprofile.ToString();

                        //       _dbHelper.ExecuteNonQuery(SSQL);



                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return false;
            }
        }
        public List<KUA> GetKUA(int _pKUA)
        {
            return null;

        }
        private bool CatatProgramKeAPBD(KUA _pKUA)
        {

            TProgramAPBD tp = new TProgramAPBD();
            TProgramAPBDLogic oLogic = new TProgramAPBDLogic(Tahun);
            tp.Tahun = _pKUA.Tahun;
            tp.IDDinas = _pKUA.IDDinas;
            tp.IDUrusan = _pKUA.IDUrusan;
            tp.IDProgram = _pKUA.IDProgram;
            tp.KodeProgramM = _pKUA.IDProgramMaster;
            tp.KodeUrusanM = _pKUA.IDUrusanMaster;
            tp.Nama = _pKUA.NamaUsulan;

            oLogic.Simpan(tp);
            //MasterProgram mp = new MasterProgram();
            //MasterProgramLogic mpLogic = new MasterProgramLogic(Tahun);

            //mp.ID = _pKUA.IDProgramMaster;
            //mp.IDUrusan = _pKUA.IDUrusanMaster;
            //mp.Nama = _pKUA.NamaUsulan;
            //mp.IDLama = _pKUA.IDProgramMaster;
            //mp.Kode = _pKUA.IDUrusanMaster == 0 ? _pKUA.IDProgramMaster : mp.ID % 100;

            //if (mpLogic.Simpan(ref mp) == true)
            //{
                //List<TProgramAPBD> lp = new List<TProgramAPBD>();
                //lp.Add(tp);
                //oLogic.SimpanKUA(lp);
           // }
            return true;

        }
     

        public void CatatProgramKUAKeAPBD(int pIDDInas)
        {
           
            SSQL = " update tKUA  Set IDUrusanMaster=0, IDProgramMaster= IDProgram % 100, IDKegiatanMaster= IDKegiatan % 10000  where IDDInas = " + pIDDInas.ToString () + " and  IDProgram % 100 < 10 and profile= " + mprofile.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "update tKUA  Set IDUrusanMaster=IDUrusan, IDProgramMaster= IDProgram , IDKegiatanMaster= IDKegiatan where IDDInas = " + pIDDInas.ToString() + " and   IDProgram % 100 > 10 and profile= " + mprofile.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);



            List<KUA> _lst = GetByIDDInas(pIDDInas, Tahun,0);
            
            foreach (KUA k in _lst)
            {
                if (k.IDLokasi == 0)
                {

                    CatatProgramKeAPBD(k);
                    CatatKegiatanDrKUA(k);
                }

            }

            

        }
        public bool  CatatNilaiKUAdarAnggaran (int pIDDInas)
        {
            try
            {
                SSQL = "UPDATE tKUA SET JumlahMurni  = (SELECT SUM(cJumlahMurni) from tANggaranRekening_A where iTahun = tKUA.iTahun and " +
                        " IDDInas = tKUA.IDDInas and IDKegiatan= tKUA.IDKegiatan), " +
                        " JumlahPerubahan = (SELECT SUM(cJumlahMurni) from tANggaranRekening_A where iTahun = tKUA.iTahun and " +
                        " IDDInas = tKUA.IDDInas and IDKegiatan= tKUA.IDKegiatan) " +
                       "where tKUA.IDDInas = " + pIDDInas.ToString() + " AND  tKUA.iTahun =" + Tahun.ToString() + " AND tKUA.IDLokasi=0  and profile =" + mprofile.ToString();
                       
                _dbHelper.ExecuteNonQuery(SSQL);

                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }



        }
        public void CatatProgramKUAKeAPBD(int pIDDInas, int IDKegiatan)
        {

            SSQL = " update tKUA  Set IDUrusanMaster=0, IDProgramMaster= IDProgram % 100, IDKegiatanMaster= IDKegiatan % 10000  where IDDInas = " + pIDDInas.ToString() + " and  IDProgram % 100 < 10  and IDKegiatan="+ IDKegiatan.ToString() + " AND profile="+ mprofile.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "update tKUA  Set IDUrusanMaster=IDUrusan, IDProgramMaster= IDProgram , IDKegiatanMaster= IDKegiatan where IDDInas = " + pIDDInas.ToString() + " and   IDProgram % 100 > 10   and IDKegiatan=" + IDKegiatan.ToString() + " AND profile=" + mprofile.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);

            List<KUA> _lst = GetByIDDInasAndIDKegiatan (pIDDInas, Tahun,IDKegiatan);
            foreach (KUA k in _lst)
            {
                if (k.IDKegiatan == IDKegiatan)
                {
                    if (k.IDLokasi == 0)
                    {
                        if (k.IDKegiatan == 20615046)
                            k.IDKegiatan = k.IDKegiatan;

//                       ' CatatProgramKeAPBD(k);
//                       ' CatatKegiatanDrKUA(k);
                    }
                }

            }



        }



        private int GetMaxIDLokasi(int _pIDKEgiatan, int _pIDDInas, int _pTahun)
        {
            int _maxID = 0;
            try
            {
                SSQL = "SELECT max(IDLokasi) as MAXID FROM tKUA WHERE iTahun =" + _pTahun.ToString() + " AND IDKegiatan=" + _pIDKEgiatan.ToString() + " AND IDDinas=" + _pIDDInas.ToString() + " AND profile= " + mprofile.ToString();


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    DataRow dr = null;
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        _maxID = DataFormat.GetInteger(dr["MAXID"]);


                    }
                }
                return _maxID;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return 0;
            }
        }
        private int GetCount(int _pIDKEgiatan, int _pIDDInas, int _pTahun)
        {
            int _maxID = 0;
            try
            {
                SSQL = "SELECT count(*) as JUMLAHDATA FROM tKUA WHERE iTahun =" + _pTahun.ToString() + " AND IDKegiatan=" + _pIDKEgiatan.ToString() + " AND IDDinas=" + _pIDDInas.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    DataRow dr = null;
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        _maxID = DataFormat.GetInteger(dr["JUMLAHDATA"]);
                    }
                }
                return _maxID;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return 0;
            }
        }
        public bool Hapus(KUA _pKUA)
        {
            try
            {
                

                SSQL = "DELETE tKUA WHERE ID=@pID and profile =@pprofile";                

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pKUA.ID));
                paramCollection.Add(new DBParameter("@pprofile", mprofile));
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                SSQL="DELETE FROM tKegiatan_A WHERE iTAhun =" +_pKUA.Tahun.ToString() + " AND IDDInas="  + _pKUA.IDDinas.ToString() + " AND IDKegiatan =" + _pKUA.IDKegiatan.ToString()+ " AND profile= " + mprofile.ToString();
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                SSQL="DELETE FROM tAnggaranRekening_A WHERE iTAhun =" +_pKUA.Tahun.ToString() + " AND IDDInas="  +  _pKUA.IDDinas.ToString() + " AND IDKegiatan ="  + _pKUA.IDKegiatan.ToString() + " AND profile= " + mprofile.ToString();
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                SSQL = "DELETE FROM tAnggaranUraian_A WHERE iTAhun =" + _pKUA.Tahun.ToString() + " AND IDDInas=" + _pKUA.IDDinas.ToString() + " AND IDKegiatan =" + _pKUA.IDKegiatan.ToString() + " AND profile= " + mprofile.ToString();
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

        public List<clsPerbandinganKUAPlafon> PerbandinganKUAPlafon(int IDDInas)
        {
            List<clsPerbandinganKUAPlafon> _lst = new List<clsPerbandinganKUAPlafon>();

            try
            {
                SSQL = "select B.IDKegiatan, B.Usulan as sNama,SUM(A.cJumlahMurni) as cPlafon , B.JumlahMurni from tKUA B  LEFT OUTER JOIN tAnggaranRekening_A A " +
                     " ON A.iTahun = B.iTahun " +
                    " AND A.IDDInas = B.IDDInas AND A.IDUrusan = B.IDurusan and A.IDProgram = B.IDProgram AND A.IDkegiatan = B.IDKegiatan  " +
                    "where B.IDDInas = " + IDDInas.ToString() + " AND  B.iTahun =" + Tahun.ToString() + " AND B.IDLokasi=0  " +
                    " GROUP BY B.IDKegiatan, B.Usulan, B.JumlahMurni ";




                       
                
                DataTable dt = new DataTable();
                //dt=_dbHelper.ExecuteDataTable(SSQL);
                dt = _dbHelper.ExecuteDataTable(SSQL);

                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows        


                ////DataTable dt = new DataTable();
                ////dt = _dbHelper.ExecuteDataTable(SSQL);
                ////if (dt != null)
                ////{
                ////    if (dt.Rows.Count > 0)
                ////    {
                //        mListUnit = (from DataRow dr in dt.Rows
                                select new clsPerbandinganKUAPlafon()
                                {
                                   
                                   //  IDDInas =DataFormat.GetInteger(dr["IDDInas"]),
                                     IDKegiatan =DataFormat.GetInteger (dr["IDKegiatan"]),
                                     Nama = DataFormat.GetString(dr["sNama"]),
                                     KUA  =DataFormat.GetDecimal(dr["JumlahMurni"]),
                                     Plafon =DataFormat.GetDecimal(dr["cPlafon"])
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
        public bool PerbaikiKodeProgram(int dinas, int uL, int pl, long kl, int ub, int pb, long kb){
            // RPJMDProgram     
            // Renstra Kegiatan 
            // musrenbang 

            RemoteConnection rCOn = new RemoteConnection();
          

            
            SSQL = "UPDATE RPJMDProgram SET ID=" + pb.ToString() + ",IDUrusan =" + ub.ToString() + "  Where ID = " + pl.ToString() + " AND SKPD =" + dinas.ToString();
            _dbHelper.ExecuteNonQuery(SSQL, rCOn.GetConnection());

            SSQL = "UPDATE RenstraKegiatan SET ID=" + kb.ToString() + ",IDProgram = " + pb.ToString() + " , IDUrusan =" + ub.ToString() + "  Where ID = " + kl.ToString() + " AND IDProgram = " + pl.ToString() + " AND SKPD =" + dinas.ToString();
            _dbHelper.ExecuteNonQuery(SSQL, rCOn.GetConnection());
            SSQL = "UPDATE musrenbang SET IDKegiatan=" + kb.ToString() + ",IDProgram = " + pb.ToString() + " ,IDUrusan =" + ub.ToString() + "  Where IDKegiatan = " + kl.ToString() + " AND IDProgram = " + pl.ToString() + " AND iddinas  =" + dinas.ToString();
            _dbHelper.ExecuteNonQuery(SSQL, rCOn.GetConnection());

            SSQL = "UPDATE tKUA SET IDProgram=" + pb.ToString() + ",IDUrusan =" + ub.ToString() + " , IDKegiatan =" + kb.ToString() + " Where IDKegiatan = " + kl.ToString() + " AND IDProgram  = " + pl.ToString() + " AND IDDInas =" + dinas.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = "UPDATE tPrograms_A SET IDProgram=" + pb.ToString() + ",IDUrusan =" + ub.ToString() + " Where  IDProgram  = " + pl.ToString() + " AND IDDInas =" + dinas.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = "UPDATE tKegiatan_A SET IDKegiatan =" + kb.ToString() + ",IDProgram = " + pb.ToString() + " , IDUrusan =" + ub.ToString() + "  Where IDKegiatan = " + kl.ToString() + " AND IDProgram = " + pl.ToString() + " AND IDDInas =" + dinas.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = "UPDATE tAnggaranRekening_A SET IDKegiatan =" + kb.ToString() + ",IDProgram = " + pb.ToString() + " ,IDUrusan =" + ub.ToString() + "  Where IDKegiatan = " + kl.ToString() + " AND IDProgram = " + pl.ToString() + " AND IDDInas =" + dinas.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = "UPDATE tAnggaranUraian_A SET IDKegiatan =" + kb.ToString() + ",IDProgram = " + pb.ToString() + " , IDUrusan =" + ub.ToString() + "  Where IDKegiatan = " + kl.ToString() + " AND IDProgram = " + pl.ToString() + " AND IDDInas =" + dinas.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = "UPDATE tIndikator SET IDKegiatan =" + kb.ToString() + ",IDProgram = " + pb.ToString() + " ,IDUrusan =" + ub.ToString() + "  Where IDKegiatan = " + kl.ToString() + " AND IDProgram = " + pl.ToString() + " AND IDDInas =" + dinas.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = "UPDATE musrenbang SET IDKegiatan=" + kb.ToString() + ",IDProgram = " + pb.ToString() + " , IDUrusan =" + ub.ToString() + "  Where IDKegiatan = " + kl.ToString() + " AND IDProgram = " + pl.ToString() + " AND iddinas  =" + dinas.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);
            return true;



            //tKUA
            // tPrograms_A
            //tKegiatan_A
            // tAnggaranRekening_A
            // tAnggaranUraian_A 
            //

        }
    }
}
